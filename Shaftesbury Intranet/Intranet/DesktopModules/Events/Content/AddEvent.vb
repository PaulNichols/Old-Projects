

Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports ASPNET.StarterKit.Communities

Namespace Events

'*********************************************************************
'
' AddEvent Class
'
' Represents the Add Event page. Enables users to list new events.
'
'*********************************************************************

Public Class AddEvent
    Inherits ContentAddPage

    Private _skinFileName As String = "Events_AddEvent.ascx"
    Private _sectionContent As String = "ASPNET.StarterKit.Communities.Events.EventSection"


    Private txtEventTitle As TextBox
    Private dropTopics As TopicPicker
    Private txtEventLink As TextBox
    Private txtEventBriefDescription As TextBox
    Private txtEventFullDescription As HtmlTextBox
    Private txtEventLocation As TextBox
    Private txtEventSpeaker As TextBox
    Private txtEventSpeakerBiography As HtmlTextBox
    Private txtImageFile As HtmlInputFile
    Private imgEventImage As Image
    Private ctlEventDate As DatePicker
    Private ctlEventDateVisible As DatePicker
    Private lblInvalidDisplayDate As Label
    Private lblInvalidEventDate As Label
    Private lblInvalidDateCompare As Label



    '*********************************************************************
    '
    ' SkinLoadEvent
    '
    ' The skin load event happens after a page skin has been loaded.
    ' Here, we grab the necessary controls from the page skin.
    '
    '*********************************************************************
    Sub SkinLoadEvent(ByVal s As [Object], ByVal e As SkinLoadEventArgs)

        ' Find the Form so that we can change the EncType of the post.
        Dim form As HtmlForm = CType(Page.FindControl("PageForm"), HtmlForm)
        form.Enctype = "multipart/form-data"

        ' Find Topic Picker
        dropTopics = CType(GetControl(e.Skin, "dropTopics"), TopicPicker)

        ' Find Event Title
        txtEventTitle = CType(GetControl(e.Skin, "txtEventTitle"), TextBox)

        ' Find Event Link
        txtEventLink = CType(GetControl(e.Skin, "txtEventLink"), TextBox)

        ' Find Event Brief Description
        txtEventBriefDescription = CType(GetControl(e.Skin, "txtEventBriefDescription"), TextBox)

        ' Find Event Full Description
        txtEventFullDescription = CType(GetControl(e.Skin, "txtEventFullDescription"), HtmlTextBox)


        ' Find Event Location
        txtEventLocation = CType(GetControl(e.Skin, "txtEventLocation"), TextBox)

        ' Find Event Speaker
        txtEventSpeaker = CType(GetControl(e.Skin, "txtEventSpeaker"), TextBox)


        ' Find Event Speaker Biography
        txtEventSpeakerBiography = CType(GetControl(e.Skin, "txtEventSpeakerBiography"), HtmlTextBox)


        ' Find Event Date
        ctlEventDate = CType(GetControl(e.Skin, "ctlEventDate"), DatePicker)

        ' Find Event Date Visible
        ctlEventDateVisible = CType(GetControl(e.Skin, "ctlEventDateVisible"), DatePicker)

        ' Find the image upload control  
        txtImageFile = CType(GetControl(e.Skin, "txtImageFile"), HtmlInputFile)

        ' Find and hide the event image control
        imgEventImage = CType(GetControl(e.Skin, "imgEventImage"), Image)
        imgEventImage.Visible = False

        ' Find Error Labels
        lblInvalidDisplayDate = CType(GetControl(e.Skin, "lblInvalidDisplayDate"), Label)
        lblInvalidEventDate = CType(GetControl(e.Skin, "lblInvalidEventDate"), Label)
        lblInvalidDateCompare = CType(GetControl(e.Skin, "lblInvalidDateCompare"), Label)

        ' Hide Error Labels by default
        lblInvalidEventDate.Visible = False
        lblInvalidDisplayDate.Visible = False
        lblInvalidDateCompare.Visible = False
    End Sub 'SkinLoadEvent






    '*********************************************************************
    '
    ' SubmitEvent Method
    '
    ' This method is raised by clicking the Add button in the Add 
    ' Event form. It adds the event to the database.
    '
    '*********************************************************************
    Private Sub SubmitEvent(ByVal s As [Object], ByVal e As EventArgs)
        ' Check for valid dates
        Dim eventDate As DateTime
        Dim displayDate As DateTime

        Try
            eventDate = ctlEventDate.Date.ToUniversalTime()
        Catch
            lblInvalidEventDate.Visible = True
            Return
        End Try


        Try
            displayDate = ctlEventDateVisible.Date.ToUniversalTime()
        Catch
            lblInvalidDisplayDate.Visible = True
            Return
        End Try

        If eventDate < displayDate Then
            lblInvalidDateCompare.Visible = True
            Return
        End If


        If Page.IsValid Then
            ' Check moderation status
            Dim moderationStatus As Integer = 1
            If objSectionInfo.EnableModeration AndAlso Not objUserInfo.MayModerate Then
                moderationStatus = 0
            End If
            ' Get Topic
            Dim topicID As Integer = -1
            If objSectionInfo.EnableTopics Then
                topicID = Int32.Parse(dropTopics.SelectedItem.Value)
            End If

            ' Add the Event
            Dim contentPageID As Integer = EventUtility.AddEvent(objSectionInfo.ID, objUserInfo.Username, txtEventTitle.Text, txtEventLink.Text, txtEventBriefDescription.Text, txtEventFullDescription.Text, txtEventLocation.Text, txtEventSpeaker.Text, txtEventSpeakerBiography.Text, eventDate, displayDate, topicID, moderationStatus)


            ' Add the Image
            ImageUtility.AddSectionImage(contentPageID, txtImageFile.PostedFile)


            ' Show warning message if moderation enabled
            If objSectionInfo.EnableModeration AndAlso Not objUserInfo.MayModerate Then
                Context.Response.Redirect(CommunityGlobals.CalculatePath("Messages_Message.aspx?message=moderation"))
            End If
            ' Otherwise, redirect to default page and send notifications
            Context.Server.ScriptTimeout = 10 * 60
            Context.Response.Redirect(CommunityGlobals.CalculatePath("Default.aspx"), False)
            Context.Response.Flush()
            Context.Response.Close()

            NotifyUtility.SendNotifications(objSectionInfo.ID, contentPageID, txtEventTitle.Text, objUserInfo.Username)
            Context.Response.End()
        End If
    End Sub 'SubmitEvent




    '*********************************************************************
    '
    ' AddEvent Constructor
    '
    ' Calls the base SkinnedCommunityControl constructor
    ' and assigns the default page skin. 
    '
    '*********************************************************************
    Public Sub New()
        ' Assign a default skin file name
        SkinFileName = _skinFileName

        ' Specify Event Section Content
        SectionContent = _sectionContent

        ' Wire-up event handlers
        AddHandler Me.SkinLoad, AddressOf SkinLoadEvent
        AddHandler Me.Submit, AddressOf SubmitEvent
    End Sub 'New
End Class 'AddEvent 

End Namespace


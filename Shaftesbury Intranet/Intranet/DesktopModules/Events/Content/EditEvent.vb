

Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports ASPNET.StarterKit.Communities

Namespace Events

'*********************************************************************
'
' EditEvent Class
'
' Represents the Edit Event page. Enables users to edit existing events.
'
'*********************************************************************

Public Class EditEvent
    Inherits ContentEditPage

    Private _skinFileName As String = "Events_AddEvent.ascx"
    Private _sectionContent As String = "ASPNET.StarterKit.Communities.Events.EventSection"


    Private txtEventTitle As TextBox
    Private dropTopics As TopicPicker
    Private txtEventLink As TextBox
    Private txtEventBriefDescription As TextBox
    Private txtEventFullDescription As HtmlTextBox
    Private txtImageFile As HtmlInputFile
    Private txtEventLocation As TextBox
    Private txtEventSpeaker As TextBox
    Private txtEventSpeakerBiography As HtmlTextBox
    Private imgEventImage As Image
    Private ctlEventDate As DatePicker
    Private ctlEventDateVisible As DatePicker
    Private lblInvalidDisplayDate As Label
    Private lblInvalidEventDate As Label
    Private lblInvalidDateCompare As Label




    '*********************************************************************
    '
    ' ContentPageID Property
    '
    ' Stores the ID of the content being edited in View State
    '
    '*********************************************************************

    Property ContentPageID() As Integer
        Get
            Return Fix(ViewState("ContentPageID"))
        End Get
        Set(ByVal Value As Integer)
            ViewState("ContentPageID") = Value
        End Set
    End Property


    '*********************************************************************
    '
    ' ImageID Property
    '
    ' Represents the Image ID of the book being edited.
    '
    '*********************************************************************

    Property ImageID() As Integer
        Get
            Return Fix(ViewState("ImageID"))
        End Get
        Set(ByVal Value As Integer)
            ViewState("ImageID") = Value
        End Set
    End Property





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
    ' OnLoad Method
    '
    ' Assigns values to the controls from the page skin.
    '
    '*********************************************************************
    Protected Overrides Sub OnLoad(ByVal e As EventArgs)

        If Not Page.IsPostBack Then
            EnsureChildControls()

            ' Get the ContentPageID from QueryString
            ContentPageID = Int32.Parse(Page.Request.QueryString("id"))

            ' Get the event information
            Dim _eventInfo As EventInfo = CType(EventUtility.GetEventInfo(objUserInfo.Username, ContentPageID), EventInfo)

            ' Store the original Image ID
            ImageID = _eventInfo.ImageID

            ' Assign value to topics
            dropTopics.SelectedTopicID = _eventInfo.TopicID

            ' Assign data
            txtEventTitle.Text = _eventInfo.Title
            txtEventLink.Text = _eventInfo.Link
            txtEventBriefDescription.Text = _eventInfo.BriefDescription
            txtEventFullDescription.Text = _eventInfo.FullDescription
            txtEventLocation.Text = _eventInfo.Location
            txtEventSpeaker.Text = _eventInfo.Speaker
            txtEventSpeakerBiography.Text = _eventInfo.SpeakerBiography
            ctlEventDate.Date = _eventInfo.Date.ToLocalTime()
            ctlEventDateVisible.Date = _eventInfo.DateVisible.ToLocalTime()
        End If
    End Sub 'OnLoad



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
            ' Get Topic
            Dim _topicID As Integer = -1
            If objSectionInfo.EnableTopics Then
                _topicID = Int32.Parse(dropTopics.SelectedItem.Value)
            End If

            ' Update the Event
            EventUtility.EditEvent(objUserInfo.Username, objSectionInfo.ID, ContentPageID, txtEventTitle.Text, txtEventLink.Text, txtEventBriefDescription.Text, txtEventFullDescription.Text, txtEventLocation.Text, txtEventSpeaker.Text, txtEventSpeakerBiography.Text, eventDate, displayDate, _topicID)

            ' Update Image
            If ImageID = -1 Then
                ImageUtility.AddSectionImage(ContentPageID, txtImageFile.PostedFile)
            Else
                ImageUtility.UpdateSectionImage(ImageID, txtImageFile.PostedFile)
            End If
            ' Redirect back to Event Page
            Context.Response.Redirect(CommunityGlobals.CalculatePath(String.Format("{0}.aspx", ContentPageID)))
        End If
    End Sub 'SubmitEvent




    '*********************************************************************
    '
    ' EditEvent Constructor
    '
    ' Calls the base SkinnedCommunityControl constructor
    ' and assigns the default page skin. 
    '
    '*********************************************************************
    Public Sub New()
        ' Assign a default skin file name
        SkinFileName = _skinFileName

        ' Specify Section Content
        SectionContent = _sectionContent

        ' Wire-up event handlers
        AddHandler Me.SkinLoad, AddressOf SkinLoadEvent
        AddHandler Me.Submit, AddressOf SubmitEvent
    End Sub 'New
End Class 'EditEvent 

End Namespace


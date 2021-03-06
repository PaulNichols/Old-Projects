Namespace ASPNET.StarterKit.Portal

    Public Class EditEvents
        Inherits System.Web.UI.Page

        Protected WithEvents TitleField As System.Web.UI.WebControls.TextBox
        Protected WithEvents Requiredfieldvalidator1 As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents DescriptionField As System.Web.UI.WebControls.TextBox
        Protected WithEvents Requiredfieldvalidator2 As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents WhereWhenField As System.Web.UI.WebControls.TextBox
        Protected WithEvents Requiredfieldvalidator3 As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents ExpireField As System.Web.UI.WebControls.TextBox
        Protected WithEvents RequiredExpireDate As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents VerifyExpireDate As System.Web.UI.WebControls.CompareValidator
        Protected WithEvents updateButton As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cancelButton As System.Web.UI.WebControls.LinkButton
        Protected WithEvents deleteButton As System.Web.UI.WebControls.LinkButton
        Protected WithEvents CreatedBy As System.Web.UI.WebControls.Label
        Protected WithEvents CreatedDate As System.Web.UI.WebControls.Label

        Private itemId As Integer = 0
        Protected WithEvents txtUrl As System.Web.UI.WebControls.TextBox
        Private moduleId As Integer = 0

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        '****************************************************************
        '
        ' The Page_Load event on this Page is used to obtain the ModuleId
        ' and ItemId of the event to edit.
        '
        ' It then uses the ASPNET.StarterKit.PortalEventsDB() data component
        ' to populate the page's edit controls with the event details.
        '
        '****************************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            ' Determine ModuleId of Events Portal Module
            moduleId = Int32.Parse(Request.Params("Mid"))

            ' Verify that the current user has access to edit this module
            If PortalSecurity.HasEditPermissions(moduleId) = False Then
                Response.Redirect("~/Admin/EditAccessDenied.aspx")
            End If

            ' Determine ItemId of Events to Update
            If Not (Request.Params("ItemId") Is Nothing) Then
                itemId = Int32.Parse(Request.Params("ItemId"))
            End If

            ' If the page is being requested the first time, determine if an
            ' event itemId value is specified, and if so populate page
            ' contents with the event details
            If Page.IsPostBack = False Then

                If itemId <> 0 Then

                    ' Obtain a single row of event information
                    Dim events As New ASPNET.StarterKit.Portal.EventsDB()
                    Dim dr As SqlDataReader = events.GetSingleEvent(itemId)

                    ' Read first row from database
                    dr.Read()

                    ' Security check.  verify that itemid is within the module.
                    Dim dbModuleID As Integer = Convert.ToInt32(dr("ModuleID"))
                    If dbModuleID <> moduleId Then
                        dr.Close()
                        Response.Redirect("~/Admin/EditAccessDenied.aspx")
                    End If

                    TitleField.Text = CType(dr("Title"), String)
                    DescriptionField.Text = CType(dr("Description"), String)
                    ExpireField.Text = CType(dr("ExpireDate"), DateTime).ToShortDateString()
                    CreatedBy.Text = CType(dr("CreatedByUser"), String)
                    WhereWhenField.Text = CType(dr("WhereWhen"), String)
                    CreatedDate.Text = CType(dr("CreatedDate"), DateTime).ToShortDateString()
                    txtUrl.Text = CType(dr("URL"), String)
                    dr.Close()

                End If

                ' Store URL Referrer to return to portal
                ViewState("UrlReferrer") = Request.UrlReferrer.ToString()

            End If

        End Sub


        '****************************************************************
        '
        ' The UpdateBtn_Click event handler on this Page is used to either
        ' create or update an event.  It uses the ASPNET.StarterKit.PortalEventsDB()
        ' data component to encapsulate all data functionality.
        '
        '****************************************************************

        Private Sub UpdateBtn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles updateButton.Click

            ' Only Update if the Entered Data is Valid
            If Page.IsValid = True Then

                ' Create an instance of the Event DB component
                Dim events As New ASPNET.StarterKit.Portal.EventsDB()

                If itemId = 0 Then

                    ' Add the event within the Events table
                    events.AddEvent(moduleId, itemId, CType(Session("User"), ASPNET.StarterKit.Portal.UserDetails).Id, TitleField.Text, DateTime.Parse(ExpireField.Text), DescriptionField.Text, WhereWhenField.Text, txtUrl.Text)

                Else

                    ' Update the event within the Events table
                    events.UpdateEvent(moduleId, itemId, CType(Session("User"), ASPNET.StarterKit.Portal.UserDetails).Id, TitleField.Text, DateTime.Parse(ExpireField.Text), DescriptionField.Text, WhereWhenField.Text, txtUrl.Text)

                End If

                ' Redirect back to the portal home page
                Response.Redirect(CType(ViewState("UrlReferrer"), String))

            End If

        End Sub


        '****************************************************************
        '
        ' The DeleteBtn_Click event handler on this Page is used to delete an
        ' an event.  It  uses the ASPNET.StarterKit.PortalEventsDB() data component to
        ' encapsulate all data functionality.
        '
        '****************************************************************

        Private Sub DeleteBtn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles deleteButton.Click

            ' Only attempt to delete the item if it is an existing item
            ' (new items will have "ItemId" of 0)
            If itemId <> 0 Then

                Dim events As New ASPNET.StarterKit.Portal.EventsDB()
                events.DeleteEvent(itemId)

            End If

            ' Redirect back to the portal home page
            Response.Redirect(CType(ViewState("UrlReferrer"), String))

        End Sub


        '****************************************************************
        '
        ' The CancelBtn_Click event handler on this Page is used to cancel
        ' out of the page, and return the user back to the portal home
        ' page.
        '
        '****************************************************************'

        Private Sub CancelBtn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cancelButton.Click

            ' Redirect back to the portal home page
            Response.Redirect(CType(ViewState("UrlReferrer"), String))

        End Sub

    End Class

End Namespace
Namespace ASPNET.StarterKit.Portal

    Public Class EditLinks
        Inherits System.Web.UI.Page

        Protected WithEvents TitleField As System.Web.UI.WebControls.TextBox
        Protected WithEvents Req1 As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents UrlField As System.Web.UI.WebControls.TextBox
        Protected WithEvents Req2 As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents DescriptionField As System.Web.UI.WebControls.TextBox
        Protected WithEvents ViewOrderField As System.Web.UI.WebControls.TextBox
        Protected WithEvents RequiredViewOrder As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents VerifyViewOrder As System.Web.UI.WebControls.CompareValidator
        Protected WithEvents updateButton As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cancelButton As System.Web.UI.WebControls.LinkButton
        Protected WithEvents deleteButton As System.Web.UI.WebControls.LinkButton
        Protected WithEvents CreatedBy As System.Web.UI.WebControls.Label
        Protected WithEvents CreatedDate As System.Web.UI.WebControls.Label

        Private itemId As Integer = 0
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
        ' The Page_Load event on this Page is used to obtain the
        ' ItemId of the link to edit.
        '
        ' It then uses the ASPNET.StarterKit.PortalLinkDB() data component
        ' to populate the page's edit controls with the links details.
        '
        '****************************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            ' Determine ModuleId of Links Portal Module
            moduleId = Int32.Parse(Request.Params("Mid"))

            ' Verify that the current user has access to edit this module
            If PortalSecurity.HasEditPermissions(moduleId) = False Then
                Response.Redirect("~/Admin/EditAccessDenied.aspx")
            End If

            ' Determine ItemId of Link to Update
            If Not (Request.Params("ItemId") Is Nothing) Then
                itemId = Int32.Parse(Request.Params("ItemId"))
            End If

            ' If the page is being requested the first time, determine if an
            ' link itemId value is specified, and if so populate page
            ' contents with the link details
            If Page.IsPostBack = False Then

                If itemId <> 0 Then

                    ' Obtain a single row of link information
                    Dim links As New LinkDB()
                    Dim dr As SqlDataReader = links.GetSingleLink(itemId)

                    ' Read in first row from database
                    dr.Read()

                    ' Security check.  verify that itemid is within the module.
                    Dim dbModuleID As Integer = Convert.ToInt32(dr("ModuleID"))
                    If dbModuleID <> moduleId Then
                        dr.Close()
                        Response.Redirect("~/Admin/EditAccessDenied.aspx")
                    End If

                    TitleField.Text = CStr(dr("Title"))
                    DescriptionField.Text = CStr(dr("Description"))
                    UrlField.Text = CStr(dr("Url"))
                    ViewOrderField.Text = dr("ViewOrder").ToString()
                    CreatedBy.Text = CStr(dr("CreatedByUser"))
                    CreatedDate.Text = CType(dr("CreatedDate"), DateTime).ToShortDateString()

                    ' Close datareader
                    dr.Close()

                End If

                ' Store URL Referrer to return to portal
                ViewState("UrlReferrer") = Request.UrlReferrer.ToString()

            End If

        End Sub


        '****************************************************************
        '
        ' The UpdateBtn_Click event handler on this Page is used to either
        ' create or update a link.  It  uses the ASPNET.StarterKit.PortalLinkDB()
        ' data component to encapsulate all data functionality.
        '
        '****************************************************************

        Private Sub UpdateBtn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles updateButton.Click

            If Page.IsValid = True Then

                ' Create an instance of the Link DB component
                Dim links As New ASPNET.StarterKit.Portal.LinkDB()

                If itemId = 0 Then

                    ' Add the link within the Links table
                    links.AddLink(moduleId, itemId, CType(Session("User"), ASPNET.StarterKit.Portal.UserDetails).Id, TitleField.Text, UrlField.Text, "", Int32.Parse(ViewOrderField.Text), DescriptionField.Text)

                Else

                    ' Update the link within the Links table
                    links.UpdateLink(moduleId, itemId, CType(Session("User"), ASPNET.StarterKit.Portal.UserDetails).Id, TitleField.Text, UrlField.Text, "", Int32.Parse(ViewOrderField.Text), DescriptionField.Text)

                End If

                ' Redirect back to the portal home page
                Response.Redirect(CStr(ViewState("UrlReferrer")))

            End If

        End Sub


        '****************************************************************
        '
        ' The DeleteBtn_Click event handler on this Page is used to delete
        ' a link.  It  uses the ASPNET.StarterKit.PortalLinksDB()
        ' data component to encapsulate all data functionality.
        '
        '****************************************************************

        Private Sub DeleteBtn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles deleteButton.Click

            ' Only attempt to delete the item if it is an existing item
            ' (new items will have "ItemId" of 0)
            If itemId <> 0 Then

                Dim links As New ASPNET.StarterKit.Portal.LinkDB()
                links.DeleteLink(itemId)

            End If

            ' Redirect back to the portal home page
            Response.Redirect(CStr(ViewState("UrlReferrer")))

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
            Response.Redirect(CStr(ViewState("UrlReferrer")))

        End Sub

    End Class

End Namespace
Namespace ASPNET.StarterKit.Portal

    Public Class EditEmployees
        Inherits System.Web.UI.Page

        Protected WithEvents NameField As System.Web.UI.WebControls.TextBox
        Protected WithEvents Requiredfieldvalidator1 As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents RoleField As System.Web.UI.WebControls.TextBox
        Protected WithEvents EmailField As System.Web.UI.WebControls.TextBox
        Protected WithEvents updateButton As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cancelButton As System.Web.UI.WebControls.LinkButton
        Protected WithEvents deleteButton As System.Web.UI.WebControls.LinkButton
        Protected WithEvents CreatedBy As System.Web.UI.WebControls.Label
        Protected WithEvents CreatedDate As System.Web.UI.WebControls.Label

        Private itemId As Integer = 0
        Protected WithEvents RadioButton1 As System.Web.UI.WebControls.RadioButton
        Protected WithEvents RadioButton2 As System.Web.UI.WebControls.RadioButton
        Protected WithEvents txtphone As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtaddress As System.Web.UI.WebControls.TextBox
        Protected WithEvents Regularexpressionvalidator2 As System.Web.UI.WebControls.RegularExpressionValidator
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
        ' and ItemId of the contact to edit.
        '
        ' It then uses the ASPNET.StarterKit.PortalContactsDB() data component
        ' to populate the page's edit controls with the contact details.
        '
        '****************************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            ' Determine ModuleId of Contacts Portal Module
            moduleId = Int32.Parse(Request.Params("Mid"))

            ' Verify that the current user has access to edit this module
            If PortalSecurity.HasEditPermissions(moduleId) = False Then
                Response.Redirect("~/Admin/EditAccessDenied.aspx")
            End If

            ' Determine ItemId of Contacts to Update
            If Not (Request.Params("ItemId") Is Nothing) Then
                itemId = Int32.Parse(Request.Params("ItemId"))
            End If

            ' If the page is being requested the first time, determine if an
            ' contact itemId value is specified, and if so populate page
            ' contents with the contact details
            If Page.IsPostBack = False Then

                If itemId <> 0 Then

                    ' Obtain a single row of contact information
                    Dim contacts As New ASPNET.StarterKit.Portal.ContactsDB
                    Dim dr As SqlDataReader = contacts.GetSingleContact(itemId)

                    ' Read first row from database
                    dr.Read()

                    ' Security check.  verify that itemid is within the module.
                    Dim dbModuleID As Integer = Convert.ToInt32(dr("ModuleID"))
                    If dbModuleID <> moduleId Then
                        dr.Close()
                        Response.Redirect("~/Admin/EditAccessDenied.aspx")
                    End If

                    NameField.Text = CType(dr("FullName"), String)
                    RoleField.Text = CType(dr("Role"), String)
                    EmailField.Text = CType(dr("Email"), String)
                    Me.txtaddress.Text = CType(dr("homeAddress"), String)
                    Me.txtphone.Text = CType(dr("homePhone"), String)
                    CreatedBy.Text = CType(dr("CreatedByUser"), String)
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
        ' create or update a contact.  It  uses the ASPNET.StarterKit.PortalContactsDB()
        ' data component to encapsulate all data functionality.
        '
        '****************************************************************

        Private Sub UpdateBtn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles updateButton.Click

            ' Only Update if Entered data is Valid
            If Page.IsValid = True Then

                ' Create an instance of the ContactsDB component
                Dim contacts As New ASPNET.StarterKit.Portal.ContactsDB

                If itemId = 0 Then

                    ' Add the contact within the contacts table
                    contacts.AddContact( _
                        moduleId, True, _
                         "", Me.txtphone.Text, _
                        Context.User.Identity.Name, "", _
                        Me.NameField.Text, "", "", _
                        Me.RoleField.Text, "", "", _
                        Me.EmailField.Text, Me.txtaddress.Text, "", _
                        "", "", _
                        "", "", "", "", "", "")
                Else

                    ' Update the contact within the contacts table
                    contacts.UpdateContact( _
                      moduleId, True, _
                      itemId, "", Me.txtphone.Text, _
                      Context.User.Identity.Name, "", _
                       Me.NameField.Text, "", "", _
                        Me.RoleField.Text, "", "", _
                       Me.EmailField.Text, Me.txtaddress.Text, "", _
                       "", "", _
                      "", "", "", "", "", "")
                End If

                ' Redirect back to the portal home page
                Response.Redirect(CType(ViewState("UrlReferrer"), String))

            End If

        End Sub


        '****************************************************************
        '
        ' The DeleteBtn_Click event handler on this Page is used to delete an
        ' a contact.  It  uses the ASPNET.StarterKit.PortalContactsDB()
        ' data component to encapsulate all data functionality.
        '
        '****************************************************************

        Private Sub DeleteBtn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles deleteButton.Click

            ' Only attempt to delete the item if it is an existing item
            ' (new items will have "ItemId" of 0)
            If itemId <> 0 Then

                Dim contacts As New ASPNET.StarterKit.Portal.ContactsDB
                contacts.DeleteContact(itemId)

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
        '****************************************************************

        Private Sub CancelBtn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cancelButton.Click

            ' Redirect back to the portal home page
            Response.Redirect(CType(ViewState("UrlReferrer"), String))

        End Sub


    End Class

End Namespace
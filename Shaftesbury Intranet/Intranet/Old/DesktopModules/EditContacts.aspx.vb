Namespace ASPNET.StarterKit.Portal

    Public Class EditContacts
        Inherits System.Web.UI.Page


        Private itemId As Integer = 0
        Protected WithEvents butGenaral As System.Web.UI.WebControls.LinkButton
        Protected WithEvents butBuiness As System.Web.UI.WebControls.LinkButton
        Protected WithEvents butHome As System.Web.UI.WebControls.LinkButton
        Protected WithEvents txtMobile As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtBusinessPhone As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtBusinessFax As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtHomePage As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtAddress As System.Web.UI.WebControls.TextBox
        Protected WithEvents Requiredfieldvalidator3 As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents pnlBusiness As System.Web.UI.WebControls.Panel
        Protected WithEvents Requiredfieldvalidator4 As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents pnlHome As System.Web.UI.WebControls.Panel
        Protected WithEvents txtHomeAddress As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtHomePhone As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtHomeFax As System.Web.UI.WebControls.TextBox
        Protected WithEvents RadioButtonList1 As System.Web.UI.WebControls.RadioButtonList
        Protected WithEvents linkHomeAddress As System.Web.UI.WebControls.HyperLink
        Protected WithEvents pnlBasicDetails As System.Web.UI.WebControls.Panel
        Protected WithEvents txtFileAs As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtFullName As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtCompanyName As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtRole As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtFirstName As System.Web.UI.WebControls.TextBox
        Protected WithEvents Requiredfieldvalidator1 As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents txtSurname As System.Web.UI.WebControls.TextBox
        Protected WithEvents linkBusinessEMail As System.Web.UI.WebControls.HyperLink
        Protected WithEvents txtEmail As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtSalutation As System.Web.UI.WebControls.TextBox
        Protected WithEvents deleteButton As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cancelButton As System.Web.UI.WebControls.LinkButton
        Protected WithEvents updateButton As System.Web.UI.WebControls.LinkButton
        Protected WithEvents CreatedDate As System.Web.UI.WebControls.Label
        Protected WithEvents CreatedBy As System.Web.UI.WebControls.Label
        Protected WithEvents pnlTemapltes As System.Web.UI.WebControls.Panel
        Protected WithEvents lblCreatedText As System.Web.UI.WebControls.Label
        Protected WithEvents lblCreatedText2 As System.Web.UI.WebControls.Label
        Protected WithEvents RegularExpressionValidator1 As System.Web.UI.WebControls.RegularExpressionValidator
        Protected WithEvents Requiredfieldvalidator5 As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents cboTitle As System.Web.UI.WebControls.DropDownList
        Protected WithEvents cboSuffix As System.Web.UI.WebControls.DropDownList
        Protected WithEvents txtPhoneAbbr As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtFaxAbbr As System.Web.UI.WebControls.TextBox
        Protected WithEvents Requiredfieldvalidator2 As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents Regularexpressionvalidator2 As System.Web.UI.WebControls.RegularExpressionValidator
        Protected WithEvents RegularExpressionValidator3 As System.Web.UI.WebControls.RegularExpressionValidator
        Protected WithEvents RegularExpressionValidator4 As System.Web.UI.WebControls.RegularExpressionValidator
        Protected WithEvents RegularExpressionValidator5 As System.Web.UI.WebControls.RegularExpressionValidator
        Protected WithEvents RegularExpressionValidator6 As System.Web.UI.WebControls.RegularExpressionValidator
        Protected WithEvents RegularExpressionValidator7 As System.Web.UI.WebControls.RegularExpressionValidator
        Protected WithEvents RegularExpressionValidator8 As System.Web.UI.WebControls.RegularExpressionValidator
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

        Private Sub AddHandlers()
            Try
                AddHandler cancelButton.Click, AddressOf CancelBtn_Click
            Catch ex As Exception

            End Try
            Try
                AddHandler deleteButton.Click, AddressOf DeleteBtn_Click
            Catch ex As Exception

            End Try
            Try
                AddHandler updateButton.Click, AddressOf UpdateBtn_Click
            Catch ex As Exception

            End Try

            AddHandler cancelButton.Click, AddressOf CancelBtn_Click
            AddHandler deleteButton.Click, AddressOf DeleteBtn_Click
            AddHandler updateButton.Click, AddressOf UpdateBtn_Click
        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Addhandlers()
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
            Dim dr As SqlDataReader
            If Page.IsPostBack = False Then

                RadioButtonList1.DataSource = CreateRadioDataSource()
                RadioButtonList1.DataTextField = "StringValue"
                RadioButtonList1.DataValueField = "IntegerValue"
                RadioButtonList1.DataBind()

                If itemId <> 0 Then

                    ' Obtain a single row of contact information
                    Dim contacts As New ASPNET.StarterKit.Portal.ContactsDB
                    dr = contacts.GetSingleContact(itemId)

                    ' Read first row from database
                    dr.Read()

                    ' Security check.  verify that itemid is within the module.
                    Dim dbModuleID As Integer = Convert.ToInt32(dr("ModuleID"))
                    If dbModuleID <> moduleId Then
                        dr.Close()
                        Response.Redirect("~/Admin/EditAccessDenied.aspx")
                    End If

                    txtFileAs.Text = CType(dr("FileAs"), String)
                    txtFullName.Text = CType(dr("fullname"), String)
                    txtRole.Text = CType(dr("Role"), String)
                    txtEmail.Text = CType(dr("Email"), String)
                    txtFirstName.Text = CType(dr("FirstName"), String)
                    txtSurname.Text = CType(dr("LastName"), String)
                    CreatedBy.Text = CType(dr("CreatedByUser"), String)
                    CreatedDate.Text = CType(dr("CreatedDate"), DateTime).ToShortDateString()
                    txtCompanyName.Text = CType(dr("Company Name"), String)
                    txtBusinessPhone.Text = CType(dr("BusinessPhone"), String)
                    txtMobile.Text = CType(dr("Mobile"), String)
                    txtAddress.Text = CType(dr("BusinessAddress"), String)
                    txtHomeAddress.Text = CType(dr("HomeAddress"), String)
                    txtPhoneAbbr.Text = CType(dr("PhoneAbbreviation"), String)
                    txtFaxAbbr.Text = CType(dr("FaxAbbreviation"), String)
                    If Not CType(dr("BusinessHomePage"), String) = "" Then
                        linkHomeAddress.NavigateUrl = "http://" & CType(dr("BusinessHomePage"), String)
                    End If
                    If Not CType(dr("Email"), String) = "" Then
                        Me.linkBusinessEMail.NavigateUrl = "mailto:" & CType(dr("Email"), String)
                    End If
                    Dim Item As ListItem
                    For Each Item In cboSuffix.Items
                        If Item.Text.IndexOf(CType(dr("Suffix"), String)) > -1 Then
                            Item.Selected = True
                            Exit For
                        End If
                    Next
                    For Each Item In cboTitle.Items
                        If Item.Text.IndexOf(CType(dr("Title"), String)) > -1 Then
                            Item.Selected = True
                            Exit For
                        End If
                    Next
                    Item = Nothing

                    txtHomePhone.Text = CType(dr("HomePhone"), String)
                    txtHomeFax.Text = CType(dr("HomeFax"), String)
                    txtBusinessFax.Text = CType(dr("BusinessFax"), String)
                    txtSalutation.Text = CType(dr("Salutation"), String)
                    txtHomePage.Text = CType(dr("BusinessHomePage"), String)

                    If dr("userid") Is System.Convert.DBNull Then
                        RadioButtonList1.Items(0).Selected = True
                    Else
                        RadioButtonList1.Items(1).Selected = True
                    End If

                    Dim ContactDetails As New ContactDetails

                    With ContactDetails
                        .CompanyName = CType(dr("Company Name"), String)
                        .ContactAddress = CType(dr("BusinessAddress"), String)
                        .ContactSalutation = CType(dr("Salutation"), String)
                        .UserRef = CType(Session.Item("User"), ASPNET.StarterKit.Portal.UserDetails).Ref
                        .UserFullName = CType(Session.Item("User"), ASPNET.StarterKit.Portal.UserDetails).FullName
                        .ContactFax = CType(dr("BusinessFax"), String)
                        .AuthorName = CType(Session.Item("User"), ASPNET.StarterKit.Portal.UserDetails).AuthorName
                        .contactFullName = CType(dr("Fullname"), String)
                        .ContactFaxAbbr = CType(dr("FaxAbbreviation"), String)
                        .Authortitle = CType(Session.Item("User"), ASPNET.StarterKit.Portal.UserDetails).AuthorTitle
                        .AuthorInitials = CType(Session.Item("User"), ASPNET.StarterKit.Portal.UserDetails).AuthorInitials
                    End With
                    Session.Add("CurrentContact", ContactDetails)

                    dr.Close()
                    dr = Nothing
                    ' Close datareader

                Else
                    RadioButtonList1.Items(0).Selected = True
                    Me.CreatedBy.Visible = False
                    Me.CreatedDate.Visible = False
                    Me.deleteButton.Visible = False
                    Me.updateButton.Text = "Add"
                    Me.pnlTemapltes.Visible = False
                    Me.CreatedDate.Visible = False
                    Me.CreatedBy.Visible = False
                    Me.lblCreatedText.Visible = False
                    Me.lblCreatedText2.Visible = False
                End If

                ' Store URL Referrer to return to portal
                ViewState("UrlReferrer") = Request.UrlReferrer.ToString()
                General_Click()
            End If
         
        End Sub

        Function CreateRadioDataSource() As ICollection
            Dim dt As New DataTable
            Dim dr As DataRow

            dt.Columns.Add(New DataColumn("IntegerValue", GetType(Int32)))
            dt.Columns.Add(New DataColumn("StringValue", GetType(String)))
            dr = dt.NewRow()
            dr(0) = 1
            dr(1) = "Public"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr(0) = 0
            dr(1) = "Private"
            dt.Rows.Add(dr)


            Dim dv As New DataView(dt)
            Return dv
        End Function

        '****************************************************************
        '
        ' The UpdateBtn_Click event handler on this Page is used to either
        ' create or update a contact.  It  uses the ASPNET.StarterKit.PortalContactsDB()
        ' data component to encapsulate all data functionality.
        '
        '****************************************************************

        Private Sub UpdateBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

            ' Only Update if Entered data is Valid
            If Page.IsValid = True Then

                ' Create an instance of the ContactsDB component
                Dim contacts As New ASPNET.StarterKit.Portal.ContactsDB
                Dim UserId As Object
                If Not CType(RadioButtonList1.SelectedValue, Boolean) Then
                    UserId = CType(Session("User"), UserDetails).Id
                End If
                If itemId = 0 Then

                    ' Add the contact within the contacts table
                    contacts.AddContact( _
                        moduleId, UserId, _
                         cboTitle.SelectedItem.Text, txtHomePhone.Text, _
                        Context.User.Identity.Name, txtCompanyName.Text, _
                        txtFullName.Text, txtMobile.Text, txtSalutation.Text, _
                        txtRole.Text, txtAddress.Text, txtBusinessFax.Text, _
                        txtEmail.Text, txtHomeAddress.Text, txtHomePage.Text, _
                        txtFirstName.Text, cboSuffix.SelectedItem.Text, _
                        txtSurname.Text, txtFileAs.Text, txtBusinessPhone.Text, txtHomeFax.Text, _
                        txtFaxAbbr.Text, txtPhoneAbbr.Text)

                Else

                    ' Update the contact within the contacts table

                    contacts.UpdateContact( _
                        moduleId, UserId, _
                        itemId, cboTitle.SelectedItem.Text, txtHomePhone.Text, _
                        Context.User.Identity.Name, txtCompanyName.Text, _
                        txtFullName.Text, txtMobile.Text, txtSalutation.Text, _
                        txtRole.Text, txtAddress.Text, txtBusinessFax.Text, _
                        txtEmail.Text, txtHomeAddress.Text, txtHomePage.Text, _
                        txtFirstName.Text, cboSuffix.SelectedItem.Text, _
                        txtSurname.Text, txtFileAs.Text, txtBusinessPhone.Text, txtHomeFax.Text, _
                        txtFaxAbbr.Text, txtPhoneAbbr.Text)
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

        Private Sub DeleteBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

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

        Private Sub CancelBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

            ' Redirect back to the portal home page
            Response.Redirect(CType(ViewState("UrlReferrer"), String))

        End Sub


        Private Sub General_Click()
            pnlBasicDetails.Visible = True
            pnlBusiness.Visible = False
            pnlHome.Visible = False
        End Sub

        Private Sub butBuiness_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butBuiness.Click
            pnlBasicDetails.Visible = False
            pnlBusiness.Visible = True
            pnlHome.Visible = False
        End Sub

        Private Sub butGenaral_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butGenaral.Click
            General_Click()
        End Sub

        Private Sub butHome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butHome.Click
            pnlBasicDetails.Visible = False
            pnlBusiness.Visible = False
            pnlHome.Visible = True
        End Sub

        Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Unload
            '   Session.Remove("CurrentContact")
        End Sub

        Private Sub txtFirstName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFirstName.TextChanged
          FillNames
        End Sub

        Private Sub txtSurname_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSurname.TextChanged
            FillNames()
        End Sub

        Private Sub cboSuffix_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSuffix.SelectedIndexChanged
            FillNames()
        End Sub

        Private Sub FillNames()
            If cboTitle.SelectedItem.Text = "-" And cboSuffix.SelectedItem.Text = "-" Then
                txtFullName.Text = String.Concat(txtFirstName.Text, " ", txtSurname.Text)
            ElseIf cboTitle.SelectedItem.Text = "-" And Not cboSuffix.SelectedItem.Text = "-" Then
                txtFullName.Text = String.Concat(txtFirstName.Text, " ", txtSurname.Text, " ", cboSuffix.SelectedItem.Text)
            ElseIf cboSuffix.SelectedItem.Text = "-" And Not cboTitle.SelectedItem.Text = "-" Then
                txtFullName.Text = String.Concat(cboTitle.SelectedItem.Text, " ", txtFirstName.Text, " ", txtSurname.Text)
            Else
                txtFullName.Text = String.Concat(cboTitle.SelectedItem.Text, " ", txtFirstName.Text, " ", txtSurname.Text, " ", cboSuffix.SelectedItem.Text)
            End If
            txtFileAs.Text = txtSurname.Text & "," & txtFirstName.Text
        End Sub

        Private Sub cboTitle_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTitle.SelectedIndexChanged
            FillNames()
        End Sub
    End Class

    Public Class ContactDetails
        Public ContactAddress As String
        Public ContactSalutation As String
        Public UserRef As String
        Public CompanyName As String
        Public UserFullName As String
        Public ContactFax As String
        Public AuthorName As String
        Public contactFullName As String
        Public ContactFaxAbbr As String
        Public Authortitle As String
        Public AuthorInitials As String
        Public UserId As Object
    End Class
End Namespace
Namespace ASPNET.StarterKit.Portal

    Public Class DocumentUsers
        Inherits ASPNET.StarterKit.Portal.PortalModuleControl

        Protected WithEvents Message As System.Web.UI.WebControls.Label
        Protected WithEvents windowsUserName As System.Web.UI.WebControls.TextBox
        Protected WithEvents addNew As System.Web.UI.WebControls.LinkButton
        Protected WithEvents allUsers As System.Web.UI.WebControls.DropDownList
        Protected WithEvents addExisting As System.Web.UI.WebControls.LinkButton
        Protected WithEvents DocumentUsers As System.Web.UI.WebControls.DataList
        Protected WithEvents saveBtn As System.Web.UI.WebControls.LinkButton
        Protected WithEvents title As System.Web.UI.HtmlControls.HtmlGenericControl

        Private docId As Integer = -1
        '    Private docName As String
       

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

        '*******************************************************
        '
        ' The Page_Load server event handler on this page is used
        ' to populate the role information for the page
        '
        '*******************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            ' Verify that the current user has access to access this page
            If PortalSecurity.IsInRoles("Admins") = False Then
                Response.Redirect("~/Admin/EditAccessDenied.aspx")
            End If

            If Not (Request.Params("itemId") Is Nothing) Then
                docId = Int32.Parse(Request.Params("itemId"))
            End If
            'If Not (Request.Params("docname") Is Nothing) Then
            '    docName = Int32.Parse(Request.Params("docname"))
            'End If


            ' If this is the first visit to the page, bind the role data to the datalist
            If Page.IsPostBack = False Then
                BindData()
            End If

        End Sub


        Private Sub AddUser_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles addExisting.Click

            ' get user id
            Dim userId As Integer = Int32.Parse(allUsers.SelectedItem.Value)

            ' Add a new userRole to the database
            Dim documents As New DocumentDB
            documents.AddDocumentUser(docId, userId)

            ' Rebind list
            BindData()

        End Sub


        '*******************************************************
        '
        ' The usersInRole_ItemCommand server event handler on this page 
        ' is used to handle the user editing and deleting roles
        ' from the usersInRole asp:datalist control
        '
        '*******************************************************

        Private Sub associatedUsers_ItemCommand(ByVal sender As Object, ByVal e As DataListCommandEventArgs) Handles DocumentUsers.ItemCommand

            Dim Documents As New DocumentDB
            Dim userId As Integer = CInt(DocumentUsers.DataKeys(e.Item.ItemIndex))

            If e.CommandName = "delete" Then

                ' update database
                Documents.RemoveDocumentUser(docId, userId)

                ' Ensure that item is not editable
                DocumentUsers.EditItemIndex = -1

                ' Repopulate list
                BindData()
            End If

        End Sub


        Sub BindData()

            ' add the role name to the title
            'If docName <> "" Then
            '    title.InnerText = "Role Membership: " & docName
            'End If

            ' Get the portal's roles from the database
            Dim Document As New DocumentDB

            ' bind users in role to DataList
            DocumentUsers.DataSource = Document.GetDocumentUsers(docId)
            DocumentUsers.DataBind()

            ' bind all portal users to dropdownlist
            allUsers.DataSource = Document.GetUnassignedUsers(docId)
            allUsers.DataBind()

        End Sub

    End Class

End Namespace

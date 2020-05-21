Namespace ASPNET.StarterKit.Portal

    Public MustInherit Class Documents
        Inherits ASPNET.StarterKit.Portal.PortalModuleControl

        Protected WithEvents documentList As System.Web.UI.WebControls.DataList
        Protected WithEvents AddDocumentBtn As System.Web.UI.WebControls.LinkButton

        Private tabIndex As Integer = 0
        Private tabId As Integer = 0

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


        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            ' Verify that the current user has access to access this page
            If PortalSecurity.IsInRoles("Admins") = False AndAlso Not PortalSecurity.IsInRoles("Shaftesbury Admin") Then
                Response.Redirect("~/Admin/EditAccessDenied.aspx")
            End If

            If Not (Request.Params("tabid") Is Nothing) Then
                tabId = Int32.Parse(Request.Params("tabid"))
            End If
            If Not (Request.Params("tabindex") Is Nothing) Then
                tabIndex = Int32.Parse(Request.Params("tabindex"))
            End If

            If Page.IsPostBack = False Then
                BindData()
            End If

        End Sub




        Private Sub AddDocument_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles AddDocumentBtn.Click

            Response.Redirect(("~/desktopmodules/EditDocs.aspx?ItemId=" & 0 & "&mid=" & ModuleId))

        End Sub



        Private Sub DocumentList_ItemCommand(ByVal sender As Object, ByVal e As DataListCommandEventArgs) Handles documentList.ItemCommand

            Dim Documents As New DocumentDB
            Dim documentId As Integer = CInt(documentList.DataKeys(e.Item.ItemIndex))

            If e.CommandName = "edit" Then

                ' Set editable list item index if "edit" button clicked next to the item

                ' redirect to edit page
                Response.Redirect(("~/desktopmodules/EditDocs.aspx?ItemId=" & documentId & "&mid=" & ModuleId))

            ElseIf e.CommandName = "delete" Then

                ' update database
                Documents.DeleteDocument(documentId)

                ' Ensure that item is not editable
                documentList.EditItemIndex = -1

                ' Repopulate list
                BindData()

            End If
        End Sub

        Sub BindData()

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(Context.Items("PortalSettings"), PortalSettings)

            Dim Documents As New DocumentDB

            documentList.DataSource = Documents.GetDocuments(0, 0)
            documentList.DataBind()

        End Sub


    End Class

End Namespace

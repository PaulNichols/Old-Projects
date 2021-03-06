Namespace ASPNET.StarterKit.Portal

    Public Class DiscussDetails
        Inherits System.Web.UI.Page

        Protected WithEvents ReplyBtn As System.Web.UI.WebControls.LinkButton
        Protected WithEvents ButtonPanel As System.Web.UI.WebControls.Panel
        Protected WithEvents TitleField As System.Web.UI.WebControls.TextBox
        Protected WithEvents BodyField As System.Web.UI.WebControls.TextBox
        Protected WithEvents updateButton As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cancelButton As System.Web.UI.WebControls.LinkButton
        Protected WithEvents EditPanel As System.Web.UI.WebControls.Panel
        Protected WithEvents Title As System.Web.UI.WebControls.Label
        Protected WithEvents CreatedByUser As System.Web.UI.WebControls.Label
        Protected WithEvents CreatedDate As System.Web.UI.WebControls.Label
        Protected WithEvents Body As System.Web.UI.WebControls.Label
        Protected WithEvents prevItem As System.Web.UI.HtmlControls.HtmlAnchor
        Protected WithEvents nextItem As System.Web.UI.HtmlControls.HtmlAnchor

        Private moduleId As Integer = 0
        Private itemId As Integer = 0

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
        ' to obtain the ModuleId and ItemId of the discussion list,
        ' and to then display the message contents.
        '
        '*******************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            ' Obtain moduleId and ItemId from QueryString
            moduleId = Int32.Parse(Request.Params("Mid"))

            If Not (Request.Params("ItemId") Is Nothing) Then

                itemId = Int32.Parse(Request.Params("ItemId"))

            Else

                itemId = 0
                EditPanel.Visible = True
                ButtonPanel.Visible = False

            End If

            ' Populate message contents if this is the first visit to the page
            If Page.IsPostBack = False And itemId <> 0 Then
                BindData()
            End If

            If PortalSecurity.HasEditPermissions(moduleId) = False Then

                If itemId = 0 Then
                    Response.Redirect("~/Admin/EditAccessDenied.aspx")
                Else
                    ReplyBtn.Visible = False
                End If

            End If

        End Sub


        '*******************************************************
        '
        ' The ReplyBtn_Click server event handler on this page is used
        ' to handle the scenario where a user clicks the message's
        ' "Reply" button to perform a post.
        '
        '*******************************************************

        Private Sub ReplyBtn_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles ReplyBtn.Click

            EditPanel.Visible = True
            ButtonPanel.Visible = False

        End Sub


        '*******************************************************
        '
        ' The UpdateBtn_Click server event handler on this page is used
        ' to handle the scenario where a user clicks the "update"
        ' button after entering a response to a message post.
        '
        '*******************************************************

        Private Sub UpdateBtn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles updateButton.Click

            ' Create new discussion database component
            Dim discuss As New DiscussionDB()

            ' Add new message (updating the "itemId" on the page)
            itemId = discuss.AddMessage(moduleId, itemId, User.Identity.Name, Server.HtmlEncode(TitleField.Text), Server.HtmlEncode(BodyField.Text))

            ' Update visibility of page elements
            EditPanel.Visible = False
            ButtonPanel.Visible = True

            ' Repopulate page contents with new message
            BindData()

        End Sub


        '*******************************************************
        '
        ' The CancelBtn_Click server event handler on this page is used
        ' to handle the scenario where a user clicks the "cancel"
        ' button to discard a message post and toggle out of
        ' edit mode.
        '
        '*******************************************************

        Private Sub CancelBtn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cancelButton.Click

            ' Update visibility of page elements
            EditPanel.Visible = False
            ButtonPanel.Visible = True

        End Sub


        '*******************************************************
        '
        ' The BindData method is used to obtain details of a message
        ' from the Discussion table, and update the page with
        ' the message content.
        '
        '*******************************************************

        Sub BindData()

            ' Obtain the selected item from the Discussion table
            Dim discuss As New ASPNET.StarterKit.Portal.DiscussionDB()
            Dim dr As SqlDataReader = discuss.GetSingleMessage(itemId)

            ' Load first row from database
            dr.Read()

            ' Security check.  verify that itemid is within the module.
            Dim dbModuleID As Integer = Convert.ToInt32(dr("ModuleID"))
            If dbModuleID <> moduleId Then
                dr.Close()
                Response.Redirect("~/Admin/EditAccessDenied.aspx")
            End If

            ' Update labels with message contents
            Title.Text = CType(dr("Title"), String)
            Body.Text = CType(dr("Body"), String)
            CreatedByUser.Text = CType(dr("CreatedByUser"), String)
            CreatedDate.Text = String.Format("{0:d}", dr("CreatedDate"))
            TitleField.Text = ReTitle(Title.Text)

            Dim prevId As Integer = 0
            Dim nextId As Integer = 0

            ' Update next and preview links
            Dim id1 As Object = dr("PrevMessageID")

            If Not id1 Is DBNull.Value Then

                prevId = CInt(id1)
                prevItem.HRef = Request.Path & "?ItemId=" & prevId & "&mid=" & moduleId

            End If

            Dim id2 As Object = dr("NextMessageID")

            If Not id2 Is DBNull.Value Then

                nextId = CInt(id2)
                nextItem.HRef = Request.Path & "?ItemId=" & nextId & "&mid=" & moduleId

            End If

            ' close the datareader
            dr.Close()

            ' Show/Hide Next/Prev Button depending on whether there is a next/prev message
            If prevId <= 0 Then
                prevItem.Visible = False
            End If

            If nextId <= 0 Then
                nextItem.Visible = False
            End If

        End Sub


        '*******************************************************
        '
        ' The ReTitle helper method is used to create the subject
        ' line of a response post to a message.
        '
        '*******************************************************

        Function ReTitle(ByVal title As String) As String

            If title.Length > 0 And title.IndexOf("Re: ", 0) = -1 Then
                title = "Re: " & title
            End If

            Return title

        End Function

    End Class

End Namespace
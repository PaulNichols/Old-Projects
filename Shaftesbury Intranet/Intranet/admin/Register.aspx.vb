Imports System.Web.Security

Namespace ASPNET.StarterKit.Portal

    Public Class Register
        Inherits System.Web.UI.Page

        Protected WithEvents Name As System.Web.UI.WebControls.TextBox
        Protected WithEvents fullName As System.Web.UI.WebControls.TextBox
        Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents Email As System.Web.UI.WebControls.TextBox
        Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents Password As System.Web.UI.WebControls.TextBox
        Protected WithEvents RequiredFieldValidator3 As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents ConfirmPassword As System.Web.UI.WebControls.TextBox
        Protected WithEvents RequiredFieldValidator4 As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents CompareValidator1 As System.Web.UI.WebControls.CompareValidator
        Protected WithEvents RegisterBtn As System.Web.UI.WebControls.LinkButton
        Protected WithEvents txtFullName As System.Web.UI.WebControls.TextBox
        Protected WithEvents Requiredfieldvalidator5 As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents Message As System.Web.UI.WebControls.Label

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

        Private Sub RegisterBtn_Click(ByVal sender As Object, ByVal E As EventArgs) Handles RegisterBtn.Click

            ' Only attempt a login if all form fields on the page are valid
            If Page.IsValid = True Then

                ' Add New User to Portal User Database
                Dim accountSystem As New ASPNET.StarterKit.Portal.UsersDB()

                If accountSystem.AddUser(Name.Text, fullName.Text, Email.Text, PortalSecurity.Encrypt(Password.Text)) Then

                    ' Set the user's authentication name to the userId
                    FormsAuthentication.SetAuthCookie(Email.Text, False)

                    ' Redirect browser back to home page'
                    Response.Redirect("~/DesktopDefault.aspx")

                Else

                    Message.Text = "Registration Failed!  <" & "u" & ">" & Email.Text & "<" & "/u" & "> is already registered." & "<" & "br" & ">" & "Please register using a different email address."

                End If
            End If

        End Sub


    End Class

End Namespace

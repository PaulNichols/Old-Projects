Public Class Memo
    Inherits BaseTemplate


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents cmdNext As System.Web.UI.WebControls.Button
    Protected WithEvents Requiredfieldvalidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator6 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblTo As System.Web.UI.WebControls.Label
    Protected WithEvents txtTo As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblCopy As System.Web.UI.WebControls.Label
    Protected WithEvents txtCopyTo As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblHeader As System.Web.UI.WebControls.Label
    Protected WithEvents txtFrom As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblFrom As System.Web.UI.WebControls.Label
    Protected WithEvents lblSubject As System.Web.UI.WebControls.Label
    Protected WithEvents txtSubject As System.Web.UI.WebControls.TextBox
    Protected WithEvents Requiredfieldvalidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtTo2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCopyTo2 As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub FillDetails()
        If Not Session("CurrentContact") Is Nothing AndAlso Request.QueryString("General") Is Nothing Then
            With CType(Session("CurrentContact"), ASPNET.StarterKit.Portal.ContactDetails)
                txtFrom.Text = .AuthorName
                txtTo.Text = .contactFullName
            End With
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then FillDetails()
        AddHandler cmdNext.Click, AddressOf MyBase.SetUpTemplate
    End Sub

    Protected Overrides Sub SetBookMarks(ByRef doc As Object)
        With doc.Bookmarks
            .Item("bmTo1").Range.Text = txtTo.Text
            .Item("bmTo2").Range.Text = txtTo2.Text
            .Item("bmCopies1").Range.Text = txtCopyTo.Text
            .Item("bmCopies2").Range.Text = txtCopyTo2.Text
            .Item("bmDate").Range.Text = Format(Date.Today, "long date")
            .Item("bmSubject").Range.Text = txtSubject.Text
            .Item("bmFrom").Range.Text = txtFrom.Text
            .Item("bmFrom2").Range.Text = txtFrom.Text
        End With
    End Sub

    Protected Overrides Sub SetSummary(ByRef doc As Object)
        Dim Title As String
        If txtTo.Text.IndexOf(Environment.NewLine) = -1 Then
            Title = "Memo to " & txtTo.Text
        Else
            Title = "Memo to " & txtTo.Text.Substring(0, txtTo.Text.IndexOf(Environment.NewLine) - 1)
        End If

        ASPNET.StarterKit.Portal.Global.SetSummary(doc, Title, txtSubject.Text, txtFrom.Text, "MEMO")
    End Sub

    Protected Overrides Sub UpdateUser(ByVal user As ASPNET.StarterKit.Portal.UserDetails)

        With CType(Session("User"), ASPNET.StarterKit.Portal.UserDetails)
            .AuthorName = txtFrom.Text
            If Not Session("CurrentContact") Is Nothing AndAlso Request.QueryString("General") Is Nothing Then
                CType(Session("CurrentContact"), ASPNET.StarterKit.Portal.ContactDetails).AuthorName = txtFrom.Text
            End If
        End With
        MyBase.UpdateUser(user)

    End Sub
End Class

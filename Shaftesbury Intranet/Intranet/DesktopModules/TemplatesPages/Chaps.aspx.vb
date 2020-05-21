Public Class Chaps
    Inherits BaseTemplate

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents cmdNext As System.Web.UI.WebControls.Button
   
    Protected WithEvents txtYourRef As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblYourRef As System.Web.UI.WebControls.Label
    Protected WithEvents lblTheirFax As System.Web.UI.WebControls.Label
    Protected WithEvents txtRef As System.Web.UI.WebControls.TextBox
    Protected WithEvents Requiredfieldvalidator6 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblHeader As System.Web.UI.WebControls.Label
    Protected WithEvents txtBankName As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblName As System.Web.UI.WebControls.Label
    Protected WithEvents txtSortCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSortCode As System.Web.UI.WebControls.Label
    Protected WithEvents txtAccountName As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblAccountName As System.Web.UI.WebControls.Label
    Protected WithEvents txtTransferDate As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblTransferDate As System.Web.UI.WebControls.Label
    Protected WithEvents txtAccountNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblAccountNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblTransferAccount As System.Web.UI.WebControls.Label
    Protected WithEvents txtTransferAmount As System.Web.UI.WebControls.TextBox
    Protected WithEvents Requiredfieldvalidator5 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator4 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator7 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator8 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator9 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblTheirRef As System.Web.UI.WebControls.Label

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
        txtTransferDate.Text = Date.Today.ToShortDateString ', "d mmmm yyyy")
        txtYourRef.Text = CType(Session.Item("User"), ASPNET.StarterKit.Portal.UserDetails).Ref
    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then FillDetails()
        AddHandler cmdNext.Click, AddressOf SetUpTemplate
        ' checkforvowel???
    End Sub

    Protected Overrides Sub SetBookMarks(ByRef doc As Object)
        With doc.Bookmarks
            .Item("bmDate").Range.Text = Format(Now, "d mmmm yyyy")
            .Item("bmTransferDate").Range.Text = Me.txtTransferDate.Text
            .Item("bmBank").Range.Text = Me.txtBankName.Text
            .Item("bmSortCode").Range.Text = Me.txtSortCode.Text
            .Item("bmAccountName").Range.Text = Me.txtAccountName.Text
            .Item("bmAccountNumber").Range.Text = Me.txtAccountNumber.Text
            .Item("bmAmount").Range.Text = Me.txtTransferAmount.Text
            .Item("bmOtherReference").Range.Text = Me.txtRef.Text
            .Item("bmOurRef").Range.Text = Me.txtYourRef.Text
        End With
    End Sub

    Protected Overrides Sub SetSummary(ByRef doc As Object)

    End Sub
End Class

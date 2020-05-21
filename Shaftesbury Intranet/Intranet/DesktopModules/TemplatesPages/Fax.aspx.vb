Public Class Fax
    Inherits BaseTemplate

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents cmdNext As System.Web.UI.WebControls.Button
    Protected WithEvents txtCompany As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblHeader As System.Web.UI.WebControls.Label
    Protected WithEvents txtYourRef As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblYourRef As System.Web.UI.WebControls.Label
    Protected WithEvents lblName As System.Web.UI.WebControls.Label
    Protected WithEvents lblCompany As System.Web.UI.WebControls.Label
    Protected WithEvents lblTheirRef As System.Web.UI.WebControls.Label
    Protected WithEvents lblTheirFax As System.Web.UI.WebControls.Label
    Protected WithEvents lblFrom As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoOfPages As System.Web.UI.WebControls.Label
    Protected WithEvents lblJobTitle As System.Web.UI.WebControls.Label
    Protected WithEvents lblCopiesTo As System.Web.UI.WebControls.Label
    Protected WithEvents lblSubject As System.Web.UI.WebControls.Label
    Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFrom As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtJobTitle As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNoOfPages As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCopiesTo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSubject As System.Web.UI.WebControls.TextBox
    Protected WithEvents Requiredfieldvalidator5 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator4 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator6 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtRef As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTheirFax As System.Web.UI.WebControls.TextBox

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
                txtName.Text = .ContactSalutation
                txtCompany.Text = .CompanyName
                txtYourRef.Text = .UserRef
                txtTheirFax.Text = .ContactFax
                txtFrom.Text = .AuthorName
            End With

        End If
    End Sub


    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then FillDetails()
        AddHandler cmdNext.Click, AddressOf SetUpTemplate
    End Sub

    Protected Overrides Sub SetBookMarks(ByRef doc As Object)
        With doc.Bookmarks
            .Item("bmDate").Range.Text = Format(Now, "long date")
            .Item("bmCompany").Range.Text = txtCompany.Text
            .Item("bmCopies").Range.Text = txtCopiesTo.Text
            .Item("bmFrom").Range.Text = txtFrom.Text
            .Item("bmAddressee").Range.Text = txtName.Text
            .Item("bmPages").Range.Text = txtNoOfPages.Text
            .Item("bmSubject").Range.Text = txtSubject.Text
            If Not Session("CurrentContact") Is Nothing AndAlso Request.QueryString("General") Is Nothing Then
                .Item("bmFaxNumber").Range.Text = txtTheirFax.Text & _
                    IIf(CType(Session("CurrentContact"), ASPNET.StarterKit.Portal.ContactDetails).ContactFaxAbbr.Trim.Length = 0, "", " (" & _
                    Format(CType(Session("CurrentContact"), ASPNET.StarterKit.Portal.ContactDetails).ContactFaxAbbr, "0#") & ")")
            Else
                .Item("bmFaxNumber").Range.Text = txtTheirFax.Text
            End If
            .Item("bmTheirRef").Range.Text = txtRef.Text
            '            .Item("bmReference").Range.Text = txtYourRef.Text
            .Item("bmSignOff").Range.Text = "Yours sincerely" & Environment.NewLine & Environment.NewLine & _
                    Environment.NewLine & Environment.NewLine & txtFrom.Text & Environment.NewLine & txtJobTitle.Text
        End With
    End Sub

    Protected Overrides Sub SetSummary(ByRef doc As Object)
        ASPNET.StarterKit.Portal.Global.SetSummary(doc, "Fax to " + txtName.Text + " " + txtCompany.Text, _
            txtSubject.Text, txtFrom.Text, "Fax")
    End Sub

    Protected Overrides Sub UpdateUser(ByVal user As ASPNET.StarterKit.Portal.UserDetails)

        With user
            If Not Session("CurrentContact") Is Nothing AndAlso Request.QueryString("General") Is Nothing Then
                CType(Session("CurrentContact"), ASPNET.StarterKit.Portal.ContactDetails).UserRef = txtYourRef.Text
            End If
            .Ref = txtYourRef.Text
        End With
        MyBase.UpdateUser(user)

    End Sub
End Class

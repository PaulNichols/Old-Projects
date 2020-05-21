Public Class LetterControl
    Inherits System.Web.UI.UserControl

    '#Region " Web Form Designer Generated Code "

    '    'This call is required by the Web Form Designer.
    '    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    '    End Sub
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents txtAddress As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
    Protected WithEvents lstDeliveredBy As System.Web.UI.WebControls.ListBox
    Protected WithEvents lblHeader As System.Web.UI.WebControls.Label
    Protected WithEvents cmdNext As System.Web.UI.WebControls.Button
    Protected WithEvents txtcompany As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSalutation As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtHeading As System.Web.UI.WebControls.TextBox
    Protected WithEvents radYours As System.Web.UI.WebControls.RadioButtonList
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents radSignedBy As System.Web.UI.WebControls.RadioButtonList
    Protected WithEvents txtInitials As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents txtSignatoryName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTitle As System.Web.UI.WebControls.TextBox
    Protected WithEvents chkEnc As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtEnc As System.Web.UI.WebControls.TextBox
    Protected WithEvents chkCc As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtCC As System.Web.UI.WebControls.TextBox
    Protected WithEvents txttheirRef As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtyourref As System.Web.UI.WebControls.TextBox

    '    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    '    'Do not delete or move it.
    '    Private designerPlaceholderDeclaration As System.Object

    '    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
    '        'CODEGEN: This method call is required by the Web Form Designer
    '        'Do not modify it using the code editor.
    '        InitializeComponent()
    '    End Sub

    '#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then FillDetails()
        AddHandler cmdNext.Click, AddressOf MyBase.SetUpTemplate
    End Sub

    Protected Sub FillDetails()
        lstDeliveredBy.Items.Add("Post")
        lstDeliveredBy.Items.Add("Fax")
        lstDeliveredBy.Items.Add("Fax & Post")
        lstDeliveredBy.Items.Add("Courier")
        lstDeliveredBy.Items.Add("By Hand")
        lstDeliveredBy.Items.Add("Urgent")
        lstDeliveredBy.Items.Add("Recorded")
        lstDeliveredBy.Items.Add("Registered")
        lstDeliveredBy.Items.Add("Email")
        lstDeliveredBy.Items.Add("Airmail")

        With CType(Session("CurrentContact"), ASPNET.StarterKit.Portal.ContactDetails)
            txtName.Text = .contactFullName
            txtAddress.Text = .ContactAddress
            txtcompany.Text = .CompanyName
            txtyourref.Text = .UserRef
            txtyourref.TabIndex = 0
            txtSalutation.Text = .ContactSalutation
            txtSignatoryName.Text = .AuthorName
            txtInitials.Text = .AuthorInitials
            txtTitle.Text = .Authortitle
            With radYours
                .DataSource = CreateDataSource()
                .DataTextField = "StringValue"
                .DataValueField = "IntegerValue"
                .DataBind()
                .Items(0).Selected = True
            End With
            With Me.radSignedBy
                .DataSource = CreateDataSource2()
                .DataTextField = "StringValue"
                .DataValueField = "IntegerValue"
                .DataBind()
                .Items(0).Selected = True
            End With
        End With
    End Sub

    Function CreateDataSource() As ICollection
        Dim dt As New DataTable
        Dim dr As DataRow

        dt.Columns.Add(New DataColumn("IntegerValue", GetType(Int32)))
        dt.Columns.Add(New DataColumn("StringValue", GetType(String)))
        dr = dt.NewRow()
        dr(0) = 1
        dr(1) = "Faithfully"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr(0) = 0
        dr(1) = "Sincerely"
        dt.Rows.Add(dr)

        Dim dv As New DataView(dt)
        Return dv
    End Function

    Function CreateDataSource2() As ICollection
        Dim dt As New DataTable
        Dim dr As DataRow

        dt.Columns.Add(New DataColumn("IntegerValue", GetType(Int32)))
        dt.Columns.Add(New DataColumn("StringValue", GetType(String)))
        dr = dt.NewRow()
        dr(0) = 2
        dr(1) = "Directors"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr(0) = 1
        dr(1) = "Initials"
        dt.Rows.Add(dr)

        Dim dv As New DataView(dt)
        Return dv
    End Function

    Private Sub chkEnc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnc.CheckedChanged
        txtEnc.Enabled = chkEnc.Checked
    End Sub

    Private Sub chkCc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCc.CheckedChanged
        txtCC.Enabled = chkCc.Checked
    End Sub

    Private Function ToggleFields(ByVal enabled As Boolean)
        txtInitials.Enabled = enabled
        txtSignatoryName.Enabled = enabled
        txtTitle.Enabled = enabled
    End Function

    Private Sub radSignedBy_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radSignedBy.SelectedIndexChanged
        ToggleFields(radSignedBy.SelectedIndex = 1)
    End Sub

End Class
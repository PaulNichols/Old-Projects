Public Class TomsLetter
    Inherits Letter

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    'Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '    'Put user code to initialize the page here
    '    If Not IsPostBack Then FillDetails()
    '    AddHandler cmdNext.Click, AddressOf MyBase.SetUpTemplate
    'End Sub

    'Protected Sub FillDetails()
    '    lstDeliveredBy.Items.Add("Post")
    '    lstDeliveredBy.Items.Add("Fax")
    '    lstDeliveredBy.Items.Add("Fax & Post")
    '    lstDeliveredBy.Items.Add("Courier")
    '    lstDeliveredBy.Items.Add("By Hand")
    '    lstDeliveredBy.Items.Add("Urgent")
    '    lstDeliveredBy.Items.Add("Recorded")
    '    lstDeliveredBy.Items.Add("Registered")
    '    lstDeliveredBy.Items.Add("Email")
    '    lstDeliveredBy.Items.Add("Airmail")

    '    With CType(Session("CurrentContact"), ASPNET.StarterKit.Portal.ContactDetails)
    '        txtName.Text = .contactFullName
    '        txtAddress.Text = .ContactAddress
    '        txtcompany.Text = .CompanyName
    '        txtyourref.Text = .UserRef
    '        txtyourref.TabIndex = 0
    '        txtSalutation.Text = .ContactSalutation
    '        txtSignatoryName.Text = .AuthorName
    '        txtInitials.Text = .AuthorInitials
    '        txtTitle.Text = .Authortitle
    '        With radYours
    '            .DataSource = CreateDataSource()
    '            .DataTextField = "StringValue"
    '            .DataValueField = "IntegerValue"
    '            .DataBind()
    '            .Items(0).Selected = True
    '        End With
    '        With Me.radSignedBy
    '            .DataSource = CreateDataSource2()
    '            .DataTextField = "StringValue"
    '            .DataValueField = "IntegerValue"
    '            .DataBind()
    '            .Items(0).Selected = True
    '        End With
    '    End With
    'End Sub

    'Function CreateDataSource() As ICollection
    '    Dim dt As New DataTable
    '    Dim dr As DataRow

    '    dt.Columns.Add(New DataColumn("IntegerValue", GetType(Int32)))
    '    dt.Columns.Add(New DataColumn("StringValue", GetType(String)))
    '    dr = dt.NewRow()
    '    dr(0) = 1
    '    dr(1) = "Faithfully"
    '    dt.Rows.Add(dr)

    '    dr = dt.NewRow()
    '    dr(0) = 0
    '    dr(1) = "Sincerely"
    '    dt.Rows.Add(dr)

    '    Dim dv As New DataView(dt)
    '    Return dv
    'End Function

    'Function CreateDataSource2() As ICollection
    '    Dim dt As New DataTable
    '    Dim dr As DataRow

    '    dt.Columns.Add(New DataColumn("IntegerValue", GetType(Int32)))
    '    dt.Columns.Add(New DataColumn("StringValue", GetType(String)))
    '    dr = dt.NewRow()
    '    dr(0) = 2
    '    dr(1) = "Directors"
    '    dt.Rows.Add(dr)

    '    dr = dt.NewRow()
    '    dr(0) = 1
    '    dr(1) = "Initials"
    '    dt.Rows.Add(dr)

    '    Dim dv As New DataView(dt)
    '    Return dv
    'End Function

    'Private Sub chkEnc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnc.CheckedChanged
    '    txtEnc.Enabled = chkEnc.Checked
    'End Sub

    'Private Sub chkCc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCc.CheckedChanged
    '    txtCC.Enabled = chkCc.Checked
    'End Sub

    'Private Function ToggleFields(ByVal enabled As Boolean)
    '    txtInitials.Enabled = enabled
    '    txtSignatoryName.Enabled = enabled
    '    txtTitle.Enabled = enabled
    'End Function

    'Private Sub radSignedBy_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radSignedBy.SelectedIndexChanged
    '    ToggleFields(radSignedBy.SelectedIndex = 1)
    'End Sub

    'Protected Overrides Sub SetBookMarks(ByVal doc As Object)
    '    With doc.Bookmarks
    '        .Item("bmAddress").Range.Text = txtAddress.Text
    '        .Item("bmDate").Range.Text = Format(Now(), "dd mmmm yyyy")

    '        If lstDeliveredBy.SelectedIndex <> -1 Then .Item("bmDelivery").Range.Text = lstDeliveredBy.SelectedItem.Text
    '        .Item("bmHeading").Range.bold = (txtHeading.Text <> "")
    '        .Item("bmHeading").Range.Text = txtHeading.Text
    '        Dim sRef As String = "Our ref:  " & txtyourref.Text & Environment.NewLine
    '        sRef = sRef & IIf(txttheirRef.Text.Trim.Length > 0, "Your ref:  " & txttheirRef.Text, "")
    '        .Item("bmReference").Range.Text = sRef
    '        .Item("bmTo").Range.Text = txtName.Text
    '        .Item("bmSalutation").Range.Text = txtSalutation.Text
    '        .Item("bmCompany").Range.Text = txtcompany.Text
    '        If radSignedBy.SelectedIndex = 0 Then
    '            .Item("bmSignatory").Range.Text = String.Concat(" DIRECTOR".PadLeft(40, "."c), _
    '                Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, " DIRECTOR".PadLeft(40, "."c))
    '        Else
    '            .Item("bmSignatory").Range.Text = CType(Session("CurrentContact"), ASPNET.StarterKit.Portal.ContactDetails).AuthorName & _
    '                Environment.NewLine & CType(Session("CurrentContact"), ASPNET.StarterKit.Portal.ContactDetails).Authortitle
    '        End If
    '        .Item("bmSignOff").Range.Text = IIf(radYours.SelectedValue = 1, "Yours faithfully", "Yours sincerely")
    '        .Item("bmCompany").Range.Text = txtcompany.Text
    '    End With
    'End Sub

    'Protected Overrides Sub SetSummary(ByRef doc As Object)
    '    ASPNET.StarterKit.Portal.Global.SetSummary(doc, "Letter to " & txtName.Text & " - " & txtcompany.Text, txtHeading.Text, txtSignatoryName.Text, "LETTER")
    'End Sub

    'Protected Overrides Sub UpdateUser(ByVal user As ASPNET.StarterKit.Portal.UserDetails)
    '    With CType(Session("User"), ASPNET.StarterKit.Portal.UserDetails)
    '        .Ref = txtyourref.Text
    '        .AuthorInitials = txtInitials.Text
    '        .AuthorName = txtSignatoryName.Text
    '        .AuthorTitle = txtTitle.Text
    '        With CType(Session("CurrentContact"), ASPNET.StarterKit.Portal.ContactDetails)
    '            .UserRef = txtyourref.Text
    '            .Authortitle = txtTitle.Text
    '            .AuthorInitials = txtInitials.Text
    '            .AuthorName = txtSignatoryName.Text
    '        End With
    '    End With
    '    MyBase.UpdateUser(user)
    'End Sub
End Class

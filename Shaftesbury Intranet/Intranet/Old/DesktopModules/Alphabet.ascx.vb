Public Class Alphabet
    Inherits System.Web.UI.UserControl

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents HyperLink1 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents HyperLink2 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents Hyperlink3 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents Hyperlink4 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents Hyperlink6 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents Hyperlink9 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents Hyperlink10 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents Hyperlink11 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents Hyperlink12 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents Hyperlink8 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents Hyperlink7 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents Hyperlink5 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents Hyperlink13 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents Hyperlink14 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents Hyperlink15 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents Hyperlink16 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents Hyperlink17 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents Hyperlink18 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents Hyperlink19 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents Hyperlink20 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents Hyperlink21 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents Hyperlink22 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents Hyperlink23 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents Hyperlink24 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents Hyperlink25 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents Hyperlink26 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents Hyperlink27 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lblCurrentLetter As System.Web.UI.WebControls.Label
    Protected WithEvents radPublic As System.Web.UI.WebControls.RadioButtonList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Request.QueryString.Item("Filter") Is Nothing Then
            lblCurrentLetter.Text = "A"
        Else
            lblCurrentLetter.Text = Request.QueryString.Item("Filter")
        End If

        Dim [Public] As Boolean = True

        [Public] = Boolean.Parse(ParentContainer.PublicLabel.Text)
        If Not IsPostBack Then
            With radPublic
                .DataSource = CreateDataSource()
                .DataTextField = "StringValue"
                .DataValueField = "IntegerValue"
                .DataBind()
            End With
            SetPublic([Public])
        Else
            SetPublicText([Public])
        End If
    End Sub

    Private ReadOnly Property ParentContainer() As ASPNET.StarterKit.Portal.Contacts
        Get
            Return CType(NamingContainer, ASPNET.StarterKit.Portal.Contacts)
        End Get
    End Property

    Private Sub SetPublic(ByVal [Public] As Boolean)
        If [Public] Then
            radPublic.Items(0).Selected = True
        Else
            radPublic.Items(1).Selected = True
        End If
        SetPublicText([Public])
    End Sub

    Private Sub SetPublicText(ByVal [Public] As Boolean)
        If [Public] Then
            lblCurrentLetter.Text &= " - Public"
        Else
            lblCurrentLetter.Text &= " - Private"
        End If
    End Sub

    Function CreateDataSource() As ICollection
        Dim dt As New DataTable
        Dim dr As DataRow

        dt.Columns.Add(New DataColumn("IntegerValue", GetType(Int32)))
        dt.Columns.Add(New DataColumn("StringValue", GetType(String)))
        dr = dt.NewRow()

        dr(0) = 0
        dr(1) = "Public"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr(0) = 1
        dr(1) = "Private"
        dt.Rows.Add(dr)

        Dim dv As New DataView(dt)
        Return dv
    End Function


    'Private Sub radPublic_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radPublic.SelectedIndexChanged
    '    ' ParentContainer.GetViewState.Add("public", (radPublic.SelectedItem.Value = 0))
    '    SetPublic((radPublic.SelectedItem.Value = 0))
    '    ParentContainer.PublicLabel.Text = CType(radPublic.SelectedItem.Value = 0, Boolean)
    '    Response.Redirect(Request.Url.ToString)
    'End Sub

End Class

Public Class WebForm1
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents cboFilters As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cboLocation As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cboFurtherFilter As System.Web.UI.WebControls.DropDownList
    Protected WithEvents DataGrid1 As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Const AccurideLocationId As Int32 = 1

    Private ReadOnly Property Filters() As DataSet
        Get
            If viewstate("Filters") Is Nothing Then
                Dim x As New uksql3.EnquiryFilterServiceSoap
                viewstate.Add("Filters", x.GetFilters(AccurideLocationId))
                x = Nothing
            End If
            Return CType(viewstate("Filters"), DataSet)
        End Get
    End Property

    Private ReadOnly Property FurtherFilters() As DataSet
        Get
            If viewstate("FurtherFilters") Is Nothing Then
                Dim EnquiryFilterServiceSoap As New uksql3.EnquiryFilterServiceSoap
                viewstate.Add("FurtherFilters", EnquiryFilterServiceSoap.GetData(CType(cboFilters.SelectedValue, Int32), _
                     Filters.Tables(0).Rows(cboFilters.SelectedIndex).Item("AddAllRecord").ToString.Trim _
                    , "", AccurideLocationId, LocationComboValue))
                EnquiryFilterServiceSoap = Nothing
            End If
            Return CType(viewstate("FurtherFilters"), DataSet)
        End Get
    End Property

    Private ReadOnly Property LocationComboValue() As Int32
        Get
            Return CType(cboLocation.SelectedValue, Int32)
        End Get
    End Property

    Private Sub FillFurtherFilters()
        With cboFurtherFilter
            Dim Filter As Int32 = CType(cboFilters.SelectedValue, Int32)
            If Filter <> 4 Then
                .DataSource = FurtherFilters
                .DataTextField = Filters.Tables(0).Rows(cboFilters.SelectedIndex).Item("DisplayMember").ToString.Trim
                .DataValueField = Filters.Tables(0).Rows(cboFilters.SelectedIndex).Item("ValueMember").ToString.Trim '"EnquiryFilterRulesId"
                .DataBind()
            Else
                .Items.Clear()
            End If
        End With
    End Sub

    Private Sub FillGrid()
        Dim GridData As New uksql3.EnquiryFilterServiceSoap

        Me.DataGrid1.DataSource = GridData.LoadData(New Object() {FurtherFilters.Tables(0).Rows(cboFurtherFilter.SelectedIndex).Item("EnquiryFilterRulesId"), _
            AccurideLocationId, _
            LocationComboValue, 0, _
            CType(cboFilters.SelectedValue, Int32), CType(cboFurtherFilter.SelectedValue, Int32), cboFurtherFilter.SelectedItem.Text})
        DataGrid1.DataBind()

    End Sub

    Private Sub FillFilters()
        With cboFilters
            .DataSource = Filters
            .DataTextField = "Description"
            .DataValueField = "EnquiryFilterId"
            .DataBind()
        End With
    End Sub

    Private Sub FillLocations()
        Dim AccurideLocationServiceSoap As New uksql3.AccurideLocationServiceSoap
        With cboLocation
            .DataSource = AccurideLocationServiceSoap.LoadCombo(0, 1)
            .DataTextField = "DisplayMember"
            .DataValueField = "ValueMember"
            .DataBind()
        End With
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then

            FillFilters()
            FillLocations()
            FillFurtherFilters()
        End If
    End Sub

    Private Sub cboFilters_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboFilters.SelectedIndexChanged
        FillFurtherFilters()
    End Sub

    Private Sub cboFurtherFilter_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboFurtherFilter.SelectedIndexChanged
        FillGrid()

    End Sub
End Class

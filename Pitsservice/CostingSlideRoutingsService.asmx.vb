Imports System.Web.Services
Imports System.Data.SqlClient
<System.Web.Services.WebService(Namespace:="http://www.Accuride.com/CostingSlideRoutingsService")> _
Public Class CostingSlideRoutingsService
    Inherits EPS_Service
    Implements IEPS_Service_Data

#Region " Web Services Designer Generated Code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Web Services Designer.
        InitializeComponent()

        'Add your own initialization code after the InitializeComponent() call

    End Sub
    Friend WithEvents SqlDataAdapter1 As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection

    'Required by the Web Services Designer
    Private components As System.ComponentModel.Container

    'NOTE: The following procedure is required by the Web Services Designer
    'It can be modified using the Web Services Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.SqlDataAdapter1 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
        '
        'SqlDataAdapter1
        '
        Me.SqlDataAdapter1.SelectCommand = Me.SqlSelectCommand1
        Me.SqlDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Select_CostingSlideRoutings", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CompanyName", "CompanyName"), New System.Data.Common.DataColumnMapping("ProjectMasterGroup", "ProjectMasterGroup"), New System.Data.Common.DataColumnMapping("AnnualQuantity", "AnnualQuantity"), New System.Data.Common.DataColumnMapping("BatchSize", "BatchSize"), New System.Data.Common.DataColumnMapping("PlatingCode", "PlatingCode"), New System.Data.Common.DataColumnMapping("AccuridePartNumber", "AccuridePartNumber"), New System.Data.Common.DataColumnMapping("ProductLength", "ProductLength"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("CurrencyToId", "CurrencyToId"), New System.Data.Common.DataColumnMapping("Pairs", "Pairs")}), New System.Data.Common.DataTableMapping("Table1", "Table1", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("PiecesPerHour", "PiecesPerHour"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("TotalToolCost", "TotalToolCost"), New System.Data.Common.DataColumnMapping("ToolingId", "ToolingId"), New System.Data.Common.DataColumnMapping("IsRH", "IsRH"), New System.Data.Common.DataColumnMapping("CostingSlideTypeId", "CostingSlideTypeId"), New System.Data.Common.DataColumnMapping("MachineId", "MachineId"), New System.Data.Common.DataColumnMapping("RoutingId", "RoutingId"), New System.Data.Common.DataColumnMapping("Pairs", "Pairs"), New System.Data.Common.DataColumnMapping("CurrencyToId", "CurrencyToId")}), New System.Data.Common.DataTableMapping("Table2", "Table2", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("PiecesPerHour", "PiecesPerHour"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("TotalToolCost", "TotalToolCost"), New System.Data.Common.DataColumnMapping("ToolingId", "ToolingId"), New System.Data.Common.DataColumnMapping("IsRH", "IsRH"), New System.Data.Common.DataColumnMapping("CostingSlideTypeId", "CostingSlideTypeId"), New System.Data.Common.DataColumnMapping("MachineId", "MachineId"), New System.Data.Common.DataColumnMapping("RoutingId", "RoutingId"), New System.Data.Common.DataColumnMapping("Pairs", "Pairs"), New System.Data.Common.DataColumnMapping("CurrencyToId", "CurrencyToId")}), New System.Data.Common.DataTableMapping("Table3", "Table3", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("PiecesPerHour", "PiecesPerHour"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("TotalToolCost", "TotalToolCost"), New System.Data.Common.DataColumnMapping("ToolingId", "ToolingId"), New System.Data.Common.DataColumnMapping("IsRH", "IsRH"), New System.Data.Common.DataColumnMapping("CostingSlideTypeId", "CostingSlideTypeId"), New System.Data.Common.DataColumnMapping("MachineId", "MachineId"), New System.Data.Common.DataColumnMapping("RoutingId", "RoutingId"), New System.Data.Common.DataColumnMapping("Pairs", "Pairs"), New System.Data.Common.DataColumnMapping("CurrencyToId", "CurrencyToId")}), New System.Data.Common.DataTableMapping("Table4", "Table4", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("PiecesPerHour", "PiecesPerHour"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("TotalToolCost", "TotalToolCost"), New System.Data.Common.DataColumnMapping("ToolingId", "ToolingId"), New System.Data.Common.DataColumnMapping("IsRH", "IsRH"), New System.Data.Common.DataColumnMapping("CostingSlideTypeId", "CostingSlideTypeId"), New System.Data.Common.DataColumnMapping("MachineId", "MachineId"), New System.Data.Common.DataColumnMapping("RoutingId", "RoutingId"), New System.Data.Common.DataColumnMapping("Pairs", "Pairs"), New System.Data.Common.DataColumnMapping("CurrencyToId", "CurrencyToId")}), New System.Data.Common.DataTableMapping("Table5", "Table5", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("PiecesPerHour", "PiecesPerHour"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("TotalToolCost", "TotalToolCost"), New System.Data.Common.DataColumnMapping("ToolingId", "ToolingId"), New System.Data.Common.DataColumnMapping("IsRH", "IsRH"), New System.Data.Common.DataColumnMapping("CostingSlideTypeId", "CostingSlideTypeId"), New System.Data.Common.DataColumnMapping("MachineId", "MachineId"), New System.Data.Common.DataColumnMapping("RoutingId", "RoutingId"), New System.Data.Common.DataColumnMapping("Pairs", "Pairs"), New System.Data.Common.DataColumnMapping("CurrencyToId", "CurrencyToId")}), New System.Data.Common.DataTableMapping("Table6", "Table6", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("PiecesPerHour", "PiecesPerHour"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("TotalToolCost", "TotalToolCost"), New System.Data.Common.DataColumnMapping("ToolingId", "ToolingId"), New System.Data.Common.DataColumnMapping("IsRH", "IsRH"), New System.Data.Common.DataColumnMapping("CostingSlideTypeId", "CostingSlideTypeId"), New System.Data.Common.DataColumnMapping("MachineId", "MachineId"), New System.Data.Common.DataColumnMapping("RoutingId", "RoutingId"), New System.Data.Common.DataColumnMapping("Pairs", "Pairs"), New System.Data.Common.DataColumnMapping("CurrencyToId", "CurrencyToId")}), New System.Data.Common.DataTableMapping("Table7", "Table7", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("PiecesPerHour", "PiecesPerHour"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("TotalToolCost", "TotalToolCost"), New System.Data.Common.DataColumnMapping("ToolingId", "ToolingId"), New System.Data.Common.DataColumnMapping("IsRH", "IsRH"), New System.Data.Common.DataColumnMapping("CostingSlideTypeId", "CostingSlideTypeId"), New System.Data.Common.DataColumnMapping("MachineId", "MachineId"), New System.Data.Common.DataColumnMapping("RoutingId", "RoutingId"), New System.Data.Common.DataColumnMapping("Pairs", "Pairs"), New System.Data.Common.DataColumnMapping("CurrencyToId", "CurrencyToId")}), New System.Data.Common.DataTableMapping("Table8", "Table8", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("PiecesPerHour", "PiecesPerHour"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("TotalToolCost", "TotalToolCost"), New System.Data.Common.DataColumnMapping("ToolingId", "ToolingId"), New System.Data.Common.DataColumnMapping("IsRH", "IsRH"), New System.Data.Common.DataColumnMapping("CostingSlideTypeId", "CostingSlideTypeId"), New System.Data.Common.DataColumnMapping("MachineId", "MachineId"), New System.Data.Common.DataColumnMapping("RoutingId", "RoutingId"), New System.Data.Common.DataColumnMapping("Pairs", "Pairs"), New System.Data.Common.DataColumnMapping("CurrencyToId", "CurrencyToId")}), New System.Data.Common.DataTableMapping("Table9", "Table9", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("PiecesPerHour", "PiecesPerHour"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("TotalToolCost", "TotalToolCost"), New System.Data.Common.DataColumnMapping("ToolingId", "ToolingId"), New System.Data.Common.DataColumnMapping("IsRH", "IsRH"), New System.Data.Common.DataColumnMapping("CostingSlideTypeId", "CostingSlideTypeId"), New System.Data.Common.DataColumnMapping("MachineId", "MachineId"), New System.Data.Common.DataColumnMapping("RoutingId", "RoutingId"), New System.Data.Common.DataColumnMapping("Pairs", "Pairs"), New System.Data.Common.DataColumnMapping("CurrencyToId", "CurrencyToId")}), New System.Data.Common.DataTableMapping("Table10", "Table10", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("PiecesPerHour", "PiecesPerHour"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("TotalToolCost", "TotalToolCost"), New System.Data.Common.DataColumnMapping("ToolingId", "ToolingId"), New System.Data.Common.DataColumnMapping("IsRH", "IsRH"), New System.Data.Common.DataColumnMapping("CostingSlideTypeId", "CostingSlideTypeId"), New System.Data.Common.DataColumnMapping("MachineId", "MachineId"), New System.Data.Common.DataColumnMapping("RoutingId", "RoutingId"), New System.Data.Common.DataColumnMapping("Pairs", "Pairs"), New System.Data.Common.DataColumnMapping("CurrencyToId", "CurrencyToId")})})
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "ps_Select_CostingSlideRoutings"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CostingSlideId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CurrencyToId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@blnAll", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = strConnectionString

    End Sub

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        'CODEGEN: This procedure is required by the Web Services Designer
        'Do not modify it using the code editor.
    End Sub

#End Region

    Public ReadOnly Property TableName() As String Implements IEPS_Service_Data.TableName
        Get
            Return Nothing
        End Get
    End Property

    '<System.Web.Services.WebMethodAttribute()> _
    'Public Function Test() As System.Data.DataSet
    '    Return LoadData(New Object() {10881, 1, 1})
    'End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadData(ByVal pudtParams() As System.Object) As System.Data.DataSet Implements IEPS_Service_Data.LoadData
        ' Dim x As New DataSet()
        SqlDataAdapter1.SelectCommand.Parameters("@CostingSlideId").Value = CType(pudtParams(0), Int32)
        SqlDataAdapter1.SelectCommand.Parameters("@CurrencyToId").Value = CType(pudtParams(1), Int32)
        SqlDataAdapter1.SelectCommand.Parameters("@AccurideLocationId").Value = CType(pudtParams(2), Int32)
        If pudtParams.GetUpperBound(0) = 3 Then
            SqlDataAdapter1.SelectCommand.Parameters("@blnAll").Value = CType(pudtParams(3), Byte)
        End If
        Return LoadDataSet(SqlDataAdapter1)
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function UpdateData(ByVal Changes As System.Data.DataSet) As System.Data.DataSet Implements IEPS_Service_Data.UpdateData
        Return UpdateDataset(Changes, SqlDataAdapter1)
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadCombo(ByVal pintUniqueId As Int32, ByVal pintAccurideLocationId As Int32) As DataSet Implements IEPS_Service_Data.LoadCombo
        Return Nothing
    End Function

	Public Overrides Sub AssignFields()
		With Fields
			.Add("PiecesPerHour", New FieldInfoStructure("PiecesPerHour", 10, False, False))
			.Add("Description", New FieldInfoStructure("Description", 35, True, False))
			.Add("CurrencyDate", New FieldInfoStructure("CurrencyDate", 23, False, False, , "GETDATE"))
			.Add("TotalToolCost", New FieldInfoStructure("TotalToolCost", 19, True, True, "([LabourHours] * [LabourRate] + [WiringHours] * [WiringRate] + [CNCMillHours] * [CNCMillRate] + ([MaterialsCost] + [HardeningCost] + [DieSetCost]))"))
			.Add("ToolingId", New FieldInfoStructure("ToolingId", 10, False, False))
			.Add("IsRH", New FieldInfoStructure("IsRH", 1, False, False, , False))
			.Add("CostingSlideTypeId", New FieldInfoStructure("CostingSlideTypeId", 10, False, False))
			.Add("MachineId", New FieldInfoStructure("MachineId", 10, False, False))
			.Add("RoutingId", New FieldInfoStructure("RoutingId", 10, False, False))
			.Add("Pairs", New FieldInfoStructure("Pairs", 1, False, False, , True))
		End With
	End Sub
End Class

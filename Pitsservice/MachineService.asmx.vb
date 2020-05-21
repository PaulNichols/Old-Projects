Imports System.Web.Services
Imports System.Data.SqlClient
<System.Web.Services.WebService(Namespace:="http://www.Accuride.com/MachineService")> _
Public Class MachineService
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
    Friend WithEvents SqlInsertCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlDeleteCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection

    'Required by the Web Services Designer
    Private components As System.ComponentModel.Container

    'NOTE: The following procedure is required by the Web Services Designer
    'It can be modified using the Web Services Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.SqlDataAdapter1 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlInsertCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlDeleteCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
        '
        'SqlDataAdapter1
        '
        Me.SqlDataAdapter1.DeleteCommand = Me.SqlDeleteCommand1
        Me.SqlDataAdapter1.InsertCommand = Me.SqlInsertCommand1
        Me.SqlDataAdapter1.SelectCommand = Me.SqlSelectCommand1
        Me.SqlDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Select_Machine", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CostPerPart", "CostPerPart"), New System.Data.Common.DataColumnMapping("SetupCost", "SetupCost"), New System.Data.Common.DataColumnMapping("SetupLabourRate", "SetupLabourRate"), New System.Data.Common.DataColumnMapping("LabourRate", "LabourRate"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("MachineId", "MachineId"), New System.Data.Common.DataColumnMapping("NumberOfOperators", "NumberOfOperators"), New System.Data.Common.DataColumnMapping("PiecesPerHour", "PiecesPerHour"), New System.Data.Common.DataColumnMapping("SetupTime", "SetupTime"), New System.Data.Common.DataColumnMapping("AccurideLocationId", "AccurideLocationId"), New System.Data.Common.DataColumnMapping("CurrencyDate", "CurrencyDate"), New System.Data.Common.DataColumnMapping("UserId", "UserId")}), New System.Data.Common.DataTableMapping("Table1", "Table1", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CostPerPart", "CostPerPart"), New System.Data.Common.DataColumnMapping("SetupCost", "SetupCost"), New System.Data.Common.DataColumnMapping("SetupLabourRate", "SetupLabourRate"), New System.Data.Common.DataColumnMapping("LabourRate", "LabourRate"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("MachineId", "MachineId"), New System.Data.Common.DataColumnMapping("NumberOfOperators", "NumberOfOperators"), New System.Data.Common.DataColumnMapping("PiecesPerHour", "PiecesPerHour"), New System.Data.Common.DataColumnMapping("SetupTime", "SetupTime"), New System.Data.Common.DataColumnMapping("AccurideLocationId", "AccurideLocationId"), New System.Data.Common.DataColumnMapping("CurrencyDate", "CurrencyDate"), New System.Data.Common.DataColumnMapping("UserId", "UserId")}), New System.Data.Common.DataTableMapping("Table2", "Table2", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CostPerPart", "CostPerPart"), New System.Data.Common.DataColumnMapping("SetupCost", "SetupCost"), New System.Data.Common.DataColumnMapping("SetupLabourRate", "SetupLabourRate"), New System.Data.Common.DataColumnMapping("LabourRate", "LabourRate"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("MachineId", "MachineId"), New System.Data.Common.DataColumnMapping("NumberOfOperators", "NumberOfOperators"), New System.Data.Common.DataColumnMapping("PiecesPerHour", "PiecesPerHour"), New System.Data.Common.DataColumnMapping("SetupTime", "SetupTime"), New System.Data.Common.DataColumnMapping("AccurideLocationId", "AccurideLocationId"), New System.Data.Common.DataColumnMapping("CurrencyDate", "CurrencyDate"), New System.Data.Common.DataColumnMapping("UserId", "UserId")}), New System.Data.Common.DataTableMapping("Table3", "Table3", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CostPerPart", "CostPerPart"), New System.Data.Common.DataColumnMapping("SetupCost", "SetupCost"), New System.Data.Common.DataColumnMapping("SetupLabourRate", "SetupLabourRate"), New System.Data.Common.DataColumnMapping("LabourRate", "LabourRate"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("MachineId", "MachineId"), New System.Data.Common.DataColumnMapping("NumberOfOperators", "NumberOfOperators"), New System.Data.Common.DataColumnMapping("PiecesPerHour", "PiecesPerHour"), New System.Data.Common.DataColumnMapping("SetupTime", "SetupTime"), New System.Data.Common.DataColumnMapping("AccurideLocationId", "AccurideLocationId"), New System.Data.Common.DataColumnMapping("CurrencyDate", "CurrencyDate"), New System.Data.Common.DataColumnMapping("UserId", "UserId")}), New System.Data.Common.DataTableMapping("Table4", "Table4", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember"), New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember")}), New System.Data.Common.DataTableMapping("Table5", "Table5", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember"), New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember")}), New System.Data.Common.DataTableMapping("Table6", "Table6", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember"), New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember")}), New System.Data.Common.DataTableMapping("Table7", "Table7", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember"), New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember")})})
        Me.SqlDataAdapter1.UpdateCommand = Me.SqlUpdateCommand1
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "ps_Select_Machine"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_Machineid", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"Machineid",System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@LoadCombo", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlInsertCommand1
        '
        Me.SqlInsertCommand1.CommandText = "ps_Insert_Machine"
        Me.SqlInsertCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlInsertCommand1.Connection = Me.SqlConnection1
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.Char, 35, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Description", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@LabourRate", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, True, CType(19, Byte), CType(0, Byte), "LabourRate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@NumberOfOperators", System.Data.SqlDbType.TinyInt, 1, System.Data.ParameterDirection.Input, True, CType(3, Byte), CType(0, Byte), "NumberOfOperators", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PiecesPerHour", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "PiecesPerHour", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@SetupLabourRate", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, True, CType(19, Byte), CType(0, Byte), "SetupLabourRate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@SetupTime", System.Data.SqlDbType.TinyInt, 1, System.Data.ParameterDirection.Input, True, CType(3, Byte), CType(0, Byte), "SetupTime", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "AccurideLocationId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"UserId",System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = "ps_Update_Machine"
        Me.SqlUpdateCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlUpdateCommand1.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.Char, 35, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Description", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@LabourRate", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, True, CType(19, Byte), CType(0, Byte), "LabourRate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@NumberOfOperators", System.Data.SqlDbType.TinyInt, 1, System.Data.ParameterDirection.Input, True, CType(3, Byte), CType(0, Byte), "NumberOfOperators", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PiecesPerHour", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "PiecesPerHour", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@SetupLabourRate", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, True, CType(19, Byte), CType(0, Byte), "SetupLabourRate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@SetupTime", System.Data.SqlDbType.TinyInt, 1, System.Data.ParameterDirection.Input, True, CType(3, Byte), CType(0, Byte), "SetupTime", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "AccurideLocationId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CurrencyDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "CurrencyDate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Description", System.Data.SqlDbType.Char, 35, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"Description",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_LabourRate", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input,True, CType(19, Byte), CType(0, Byte),"LabourRate",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_NumberOfOperators", System.Data.SqlDbType.TinyInt, 1, System.Data.ParameterDirection.Input,True, CType(3, Byte), CType(0, Byte),"NumberOfOperators",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_PiecesPerHour", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"PiecesPerHour",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_SetupLabourRate", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input,True, CType(19, Byte), CType(0, Byte),"SetupLabourRate",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_SetupTime", System.Data.SqlDbType.TinyInt, 1, System.Data.ParameterDirection.Input,True, CType(3, Byte), CType(0, Byte),"SetupTime",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"AccurideLocationId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CurrencyDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"CurrencyDate",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_MachineId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"MachineId",System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"UserId",System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlDeleteCommand1
        '
        Me.SqlDeleteCommand1.CommandText = "ps_Delete_Machine"
        Me.SqlDeleteCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlDeleteCommand1.Connection = Me.SqlConnection1
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Description", System.Data.SqlDbType.Char, 35, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Description", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_LabourRate", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, True, CType(19, Byte), CType(0, Byte), "LabourRate", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_NumberOfOperators", System.Data.SqlDbType.TinyInt, 1, System.Data.ParameterDirection.Input, True, CType(3, Byte), CType(0, Byte), "NumberOfOperators", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_PiecesPerHour", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "PiecesPerHour", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_SetupLabourRate", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, True, CType(19, Byte), CType(0, Byte), "SetupLabourRate", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_SetupTime", System.Data.SqlDbType.TinyInt, 1, System.Data.ParameterDirection.Input, True, CType(3, Byte), CType(0, Byte), "SetupTime", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CurrencyDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "CurrencyDate", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "AccurideLocationId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_MachineId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "MachineId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "UserId", System.Data.DataRowVersion.Current, Nothing))
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
            Return "tMachine"
        End Get
    End Property

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadData(ByVal pudtParams() As System.Object) As System.Data.DataSet Implements IEPS_Service_Data.LoadData
        SqlDataAdapter1.SelectCommand.Parameters("@AccurideLocationId").Value = CType(pudtParams(0), Int32)
        SqlDataAdapter1.SelectCommand.Parameters("@Select_Machineid").Value = CType(pudtParams(1), Int32)
        SqlDataAdapter1.SelectCommand.Parameters("@UserId").Value = CType(pudtParams(2), Int32)
        If pudtParams.GetUpperBound(0) = 2 Then
            SqlDataAdapter1.SelectCommand.Parameters("@LoadCombo").Value = Convert.DBNull
        Else
            SqlDataAdapter1.SelectCommand.Parameters("@LoadCombo").Value = 1
        End If
        Return LoadDataSet(SqlDataAdapter1)
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function UpdateData(ByVal Changes As System.Data.DataSet) As System.Data.DataSet Implements IEPS_Service_Data.UpdateData
        Return UpdateDataset(Changes, SqlDataAdapter1)
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadCombo(ByVal pintUniqueId As Int32, ByVal pintAccurideLocationId As Int32) As DataSet Implements IEPS_Service_Data.LoadCombo
        Return LoadData(New Object() {pintAccurideLocationId, pintUniqueId, 0, 1})
    End Function

	Public Overrides Sub AssignFields()
		With Fields
			.Add("CurrencyDate", New FieldInfoStructure("CurrencyDate", 23, False, False, , "GETDATE"))
			.Add("CostPerPart", New FieldInfoStructure("CostPerPart", 38, True, True, "(convert([NumberOfOperators], 'System.Decimal') * convert([LabourRate], 'System.Decimal') / convert([PiecesPerHour], 'System.Decimal'))"))
			.Add("SetupCost", New FieldInfoStructure("SetupCost", 19, True, True, "(iif( ([SetupTime] = 0) , 0 , ([SetupLabourRate] / (60 / [SetupTime])) ))"))
			.Add("SetupLabourRate", New FieldInfoStructure("SetupLabourRate", 19, False, False))
			.Add("LabourRate", New FieldInfoStructure("LabourRate", 19, False, False))
			.Add("Description", New FieldInfoStructure("Description", 35, True, False))
			.Add("MachineId", New FieldInfoStructure("MachineId", 10, False, False))
			.Add("NumberOfOperators", New FieldInfoStructure("NumberOfOperators", 3, False, False))
			.Add("PiecesPerHour", New FieldInfoStructure("PiecesPerHour", 10, False, False))
			.Add("SetupTime", New FieldInfoStructure("SetupTime", 3, False, False))
			.Add("AccurideLocationId", New FieldInfoStructure("AccurideLocationId", 10, False, False))
		End With
	End Sub
End Class

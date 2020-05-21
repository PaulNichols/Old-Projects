Imports System.Web.Services
Imports System.Data.SqlClient
<System.Web.Services.WebService(Namespace:="http://www.Accuride.com/BracketService")> _
Public Class BracketService
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
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand

    'Required by the Web Services Designer
    Private components As System.ComponentModel.Container

    'NOTE: The following procedure is required by the Web Services Designer
    'It can be modified using the Web Services Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlInsertCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand2 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlDeleteCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.SqlDataAdapter1 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlInsertCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlUpdateCommand2 = New System.Data.SqlClient.SqlCommand()
        Me.SqlDeleteCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
        '
        'SqlDataAdapter1
        '
        Me.SqlDataAdapter1.DeleteCommand = Me.SqlDeleteCommand1
        Me.SqlDataAdapter1.InsertCommand = Me.SqlInsertCommand1
        Me.SqlDataAdapter1.SelectCommand = Me.SqlSelectCommand1
        Me.SqlDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Select_Bracket", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("BracketId", "BracketId"), New System.Data.Common.DataColumnMapping("BracketName", "BracketName"), New System.Data.Common.DataColumnMapping("BracketNumber", "BracketNumber"), New System.Data.Common.DataColumnMapping("MaterialId", "MaterialId"), New System.Data.Common.DataColumnMapping("MaterialQty", "MaterialQty"), New System.Data.Common.DataColumnMapping("NewToolingCost", "NewToolingCost"), New System.Data.Common.DataColumnMapping("Notes", "Notes"), New System.Data.Common.DataColumnMapping("CurrencyDate", "CurrencyDate"), New System.Data.Common.DataColumnMapping("UserId", "UserId")}), New System.Data.Common.DataTableMapping("Table1", "Table1", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("BracketId", "BracketId"), New System.Data.Common.DataColumnMapping("BracketName", "BracketName"), New System.Data.Common.DataColumnMapping("BracketNumber", "BracketNumber"), New System.Data.Common.DataColumnMapping("MaterialId", "MaterialId"), New System.Data.Common.DataColumnMapping("MaterialQty", "MaterialQty"), New System.Data.Common.DataColumnMapping("NewToolingCost", "NewToolingCost"), New System.Data.Common.DataColumnMapping("Notes", "Notes"), New System.Data.Common.DataColumnMapping("CurrencyDate", "CurrencyDate"), New System.Data.Common.DataColumnMapping("UserId", "UserId")}), New System.Data.Common.DataTableMapping("Table2", "Table2", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember"), New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember")}), New System.Data.Common.DataTableMapping("Table3", "Table3", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember"), New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember")})})
        Me.SqlDataAdapter1.UpdateCommand = Me.SqlUpdateCommand2
        '
        'SqlUpdateCommand1
        '
  
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "[ps_Select_Bracket]"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_BracketId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"BracketId",System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@LoadCombo", System.Data.SqlDbType.Bit, 1))
        '
        'SqlInsertCommand1
        '
        Me.SqlInsertCommand1.CommandText = "[ps_Insert_Bracket]"
        Me.SqlInsertCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlInsertCommand1.Connection = Me.SqlConnection1
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@BracketName", System.Data.SqlDbType.VarChar, 40, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "BracketName", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@BracketNumber", System.Data.SqlDbType.VarChar, 25, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "BracketNumber", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@MaterialId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "MaterialId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@MaterialQty", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(2, Byte), "MaterialQty", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@NewToolingCost", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, False, CType(19, Byte), CType(0, Byte), "NewToolingCost", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Notes", System.Data.SqlDbType.VarChar, 20, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "Notes", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"UserId",System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlUpdateCommand2
        '
        Me.SqlUpdateCommand2.CommandText = "[ps_Update_Bracket]"
        Me.SqlUpdateCommand2.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlUpdateCommand2.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@BracketName", System.Data.SqlDbType.VarChar, 40, System.Data.ParameterDirection.Input, True, CType(19, Byte), CType(0, Byte), "BracketName", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@BracketNumber", System.Data.SqlDbType.VarChar, 25, System.Data.ParameterDirection.Input, True, CType(19, Byte), CType(0, Byte), "BracketNumber", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@MaterialId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "MaterialId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@MaterialQty", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, True, CType(15, Byte), CType(0, Byte), "MaterialQty", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@NewToolingCost", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, True, CType(19, Byte), CType(0, Byte), "NewToolingCost", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Notes", System.Data.SqlDbType.VarChar, 20, System.Data.ParameterDirection.Input, True, CType(19, Byte), CType(0, Byte), "Notes", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CurrencyDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, True, CType(19, Byte), CType(0, Byte), "CurrencyDate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_BracketName", System.Data.SqlDbType.VarChar, 40, System.Data.ParameterDirection.Input,True, CType(19, Byte), CType(0, Byte),"BracketName",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_BracketNumber", System.Data.SqlDbType.VarChar, 25, System.Data.ParameterDirection.Input,True, CType(19, Byte), CType(0, Byte),"BracketNumber",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_MaterialId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"MaterialId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_MaterialQty", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input,True, CType(15, Byte), CType(0, Byte),"MaterialQty",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_NewToolingCost", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input,True, CType(19, Byte), CType(0, Byte),"NewToolingCost",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Notes", System.Data.SqlDbType.VarChar, 20, System.Data.ParameterDirection.Input,True, CType(19, Byte), CType(0, Byte),"Notes",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CurrencyDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input,True, CType(19, Byte), CType(0, Byte),"CurrencyDate",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_BracketId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"BracketId",System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"UserId",System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlDeleteCommand1
        '
        Me.SqlDeleteCommand1.CommandText = "[ps_Delete_Bracket]"
        Me.SqlDeleteCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlDeleteCommand1.Connection = Me.SqlConnection1
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@BracketName", System.Data.SqlDbType.VarChar, 40, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "BracketName", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@BracketNumber", System.Data.SqlDbType.VarChar, 25, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "BracketNumber", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@MaterialId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "MaterialId", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@MaterialQty", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, False, CType(15, Byte), CType(0, Byte), "MaterialQty", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@NewToolingCost", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, False, CType(19, Byte), CType(0, Byte), "NewToolingCost", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Notes", System.Data.SqlDbType.VarChar, 20, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "Notes", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CurrencyDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CurrencyDate", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_BracketName", System.Data.SqlDbType.VarChar, 40, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"BracketName",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_BracketNumber", System.Data.SqlDbType.VarChar, 25, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"BracketNumber",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_MaterialId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"MaterialId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_MaterialQty", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input,True, CType(15, Byte), CType(0, Byte),"MaterialQty",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_NewToolingCost", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input,True, CType(19, Byte), CType(0, Byte),"NewToolingCost",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Notes", System.Data.SqlDbType.VarChar, 20, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"Notes",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CurrencyDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"CurrencyDate",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_BracketId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"BracketId",System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"UserId",System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = strCONNECTIONSTRING

    End Sub

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        'CODEGEN: This procedure is required by the Web Services Designer
        'Do not modify it using the code editor.
    End Sub

#End Region

    Public ReadOnly Property TableName() As String Implements IEPS_Service_Data.TableName
        Get
            Return "tBracket"
        End Get
    End Property


    <System.Web.Services.WebMethodAttribute()> _
    Public Function UpdateData(ByVal Changes As System.Data.DataSet) As System.Data.DataSet Implements IEPS_Service_Data.UpdateData
        Return UpdateDataset(Changes, SqlDataAdapter1)
    End Function

    '<System.Web.Services.WebMethodAttribute()> _
    'Public Function Test() As DataSet

    '    Return LoadData(New Object() {0, 67})
    'End Function


    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadCombo(ByVal pintUniqueId As Int32, ByVal pintAccurideLocationId As Int32) As DataSet Implements IEPS_Service_Data.LoadCombo
        Return LoadData(New Object() {pintUniqueId, 0, 1})
    End Function


    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadData(ByVal pudtParams() As Object) As System.Data.DataSet Implements PITSService.IEPS_Service_Data.LoadData
        SqlDataAdapter1.SelectCommand.Parameters("@Select_BracketId").Value = CType(pudtParams(0), Int32)
        SqlDataAdapter1.SelectCommand.Parameters("@UserId").Value = CType(pudtParams(1), Int32)
        If pudtParams.GetUpperBound(0) = 2 Then
            SqlDataAdapter1.SelectCommand.Parameters("@LoadCombo").Value = 1
        Else
            SqlDataAdapter1.SelectCommand.Parameters("@LoadCombo").Value = Convert.DBNull
        End If
        Return LoadDataSet(SqlDataAdapter1)
    End Function
	Public Overrides Sub AssignFields()
		With Fields
			.Add("BracketId", New FieldInfoStructure("BracketId", 10, False, False))
			.Add("BracketName", New FieldInfoStructure("BracketName", 40, True, False))
			.Add("BracketNumber", New FieldInfoStructure("BracketNumber", 25, False, False))
			.Add("MaterialCode", New FieldInfoStructure("MaterialCode", 15, True, False))
			.Add("MaterialQty", New FieldInfoStructure("MaterialQty", 18, False, False))
			.Add("CurrencyDate", New FieldInfoStructure("CurrencyDate", 23, False, False, , "GETDATE"))
			.Add("NewToolingCost", New FieldInfoStructure("NewToolingCost", 19, True, False))
			.Add("Notes", New FieldInfoStructure("Notes", 20, True, False))
			.Add("MaterialId", New FieldInfoStructure("MaterialId", 10, False, False))
			.Add("MaterialDescription", New FieldInfoStructure("MaterialDescription", 50, True, False))
		End With
	End Sub
End Class

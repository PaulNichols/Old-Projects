Imports System.Web.Services
Imports System.Data.SqlClient
<System.Web.Services.WebService(Namespace:="http://www.Accuride.com/CostingSlideDetails_RetainersService")> _
Public Class CostingSlideDetails_RetainersService
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
        Me.SqlDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Select_CostingSlideDetails_Retainers", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("MaterialCode", "MaterialCode"), New System.Data.Common.DataColumnMapping("CostingRetainerId", "CostingRetainerId"), New System.Data.Common.DataColumnMapping("MaterialId", "MaterialId"), New System.Data.Common.DataColumnMapping("NumberOfBalls", "NumberOfBalls"), New System.Data.Common.DataColumnMapping("IsRH", "IsRH"), New System.Data.Common.DataColumnMapping("CostingSlideId", "CostingSlideId"), New System.Data.Common.DataColumnMapping("Pairs", "Pairs")})})
        Me.SqlDataAdapter1.UpdateCommand = Me.SqlUpdateCommand1
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "ps_Select_CostingSlideDetails_Retainers"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CostingSlideId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlInsertCommand1
        '
        Me.SqlInsertCommand1.CommandText = "ps_Insert_CostingSlideDetails_Retainers"
        Me.SqlInsertCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlInsertCommand1.Connection = Me.SqlConnection1
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@MaterialId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "MaterialId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@NumberOfBalls", System.Data.SqlDbType.SmallInt, 2, System.Data.ParameterDirection.Input, True, CType(5, Byte), CType(0, Byte), "NumberOfBalls", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@IsRH", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "IsRH", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CostingSlideId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "CostingSlideId", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = "ps_Update_CostingSlideDetails_Retainers"
        Me.SqlUpdateCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlUpdateCommand1.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@MaterialId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "MaterialId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@NumberOfBalls", System.Data.SqlDbType.SmallInt, 2, System.Data.ParameterDirection.Input, True, CType(5, Byte), CType(0, Byte), "NumberOfBalls", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@IsRH", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "IsRH", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CostingSlideId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "CostingSlideId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_MaterialId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"MaterialId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_NumberOfBalls", System.Data.SqlDbType.SmallInt, 2, System.Data.ParameterDirection.Input,True, CType(5, Byte), CType(0, Byte),"NumberOfBalls",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_IsRH", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"IsRH",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CostingSlideId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"CostingSlideId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_CostingRetainerId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"CostingRetainerId",System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlDeleteCommand1
        '
        Me.SqlDeleteCommand1.CommandText = "ps_Delete_CostingSlideDetails_Retainers"
        Me.SqlDeleteCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlDeleteCommand1.Connection = Me.SqlConnection1
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@MaterialId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "MaterialId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@NumberOfBalls", System.Data.SqlDbType.SmallInt, 2, System.Data.ParameterDirection.Input, True, CType(5, Byte), CType(0, Byte), "NumberOfBalls", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@IsRH", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "IsRH", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CostingSlideId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "CostingSlideId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_MaterialId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"MaterialId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_NumberOfBalls", System.Data.SqlDbType.SmallInt, 2, System.Data.ParameterDirection.Input,True, CType(5, Byte), CType(0, Byte),"NumberOfBalls",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_IsRH", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"IsRH",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CostingSlideId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"CostingSlideId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_CostingRetainerId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"CostingRetainerId",System.Data.DataRowVersion.Current, Nothing))
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

    <System.Web.Services.WebMethodAttribute()> _
    Public Function Test() As System.Data.DataSet
        Return LoadData(New Object() {10793, 1})
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadData(ByVal pudtParams() As System.Object) As System.Data.DataSet Implements IEPS_Service_Data.LoadData
        'Dim x As New DataSet()
        SqlDataAdapter1.SelectCommand.Parameters("@CostingSlideId").Value = CType(pudtParams(0), Int32)
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
			.Add("MaterialCode", New FieldInfoStructure("MaterialCode", 15, True, False))
			.Add("CostingRetainerId", New FieldInfoStructure("CostingRetainerId", 10, False, False))
			.Add("MaterialId", New FieldInfoStructure("MaterialId", 10, True, False))
			.Add("NumberOfBalls", New FieldInfoStructure("NumberOfBalls", 5, True, False))
			.Add("IsRH", New FieldInfoStructure("IsRH", 1, False, False, , False))
			.Add("CostingSlideId", New FieldInfoStructure("CostingSlideId", 10, False, False))
			.Add("Pairs", New FieldInfoStructure("Pairs", 1, False, False, , True))
			.Add("MaterialDescription", New FieldInfoStructure("MaterialDescription", 50, True, False))
		End With
	End Sub
End Class

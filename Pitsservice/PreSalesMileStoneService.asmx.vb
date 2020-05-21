Imports System.Web.Services
Imports System.Data.SqlClient
<System.Web.Services.WebService(Namespace:="http://www.Accuride.com/PreSalesMileStoneService")> _
Public Class PreSalesMileStoneService
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
    Friend WithEvents SqlInsertCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection

    'Required by the Web Services Designer
    Private components As System.ComponentModel.Container

    'NOTE: The following procedure is required by the Web Services Designer
    'It can be modified using the Web Services Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.SqlDataAdapter1 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
        '
        'SqlDataAdapter1
        '
        Me.SqlDataAdapter1.SelectCommand = Me.SqlSelectCommand1
        Me.SqlDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_select_PreSalesMileStones", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("MileStoneDescription", "MileStoneDescription"), New System.Data.Common.DataColumnMapping("Owner", "Owner"), New System.Data.Common.DataColumnMapping("Planned Date", "Planned Date"), New System.Data.Common.DataColumnMapping("Actual Date", "Actual Date"), New System.Data.Common.DataColumnMapping("Customer Date", "Customer Date"), New System.Data.Common.DataColumnMapping("MileStoneId", "MileStoneId"), New System.Data.Common.DataColumnMapping("OwnerId", "OwnerId"), New System.Data.Common.DataColumnMapping("PlannedDate", "PlannedDate"), New System.Data.Common.DataColumnMapping("ActualHours", "ActualHours"), New System.Data.Common.DataColumnMapping("PlannedHours", "PlannedHours"), New System.Data.Common.DataColumnMapping("Qty", "Qty"), New System.Data.Common.DataColumnMapping("CustomerDate", "CustomerDate"), New System.Data.Common.DataColumnMapping("ActualDate", "ActualDate"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("PreSalesId", "PreSalesId"), New System.Data.Common.DataColumnMapping("PreSalesMileStoneId", "PreSalesMileStoneId"), New System.Data.Common.DataColumnMapping("AccurideLocationId", "AccurideLocationId")}), New System.Data.Common.DataTableMapping("Table1", "Table1", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("MileStoneDescription", "MileStoneDescription"), New System.Data.Common.DataColumnMapping("Owner", "Owner"), New System.Data.Common.DataColumnMapping("Planned Date", "Planned Date"), New System.Data.Common.DataColumnMapping("Actual Date", "Actual Date"), New System.Data.Common.DataColumnMapping("Customer Date", "Customer Date"), New System.Data.Common.DataColumnMapping("MileStoneId", "MileStoneId"), New System.Data.Common.DataColumnMapping("OwnerId", "OwnerId"), New System.Data.Common.DataColumnMapping("PlannedDate", "PlannedDate"), New System.Data.Common.DataColumnMapping("ActualHours", "ActualHours"), New System.Data.Common.DataColumnMapping("PlannedHours", "PlannedHours"), New System.Data.Common.DataColumnMapping("Qty", "Qty"), New System.Data.Common.DataColumnMapping("CustomerDate", "CustomerDate"), New System.Data.Common.DataColumnMapping("ActualDate", "ActualDate"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("PreSalesId", "PreSalesId"), New System.Data.Common.DataColumnMapping("PreSalesMileStoneId", "PreSalesMileStoneId"), New System.Data.Common.DataColumnMapping("AccurideLocationId", "AccurideLocationId")})})
        Me.SqlDataAdapter1.UpdateCommand = Me.SqlUpdateCommand1
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "ps_select_PreSalesMileStones"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PreSalesMileStoneId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = "ps_Update_PreSalesMileStone"
        Me.SqlUpdateCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlUpdateCommand1.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ActualDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "ActualDate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CustomerDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "CustomerDate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@OwnerId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "OwnerId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PlannedDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "PlannedDate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ActualHours", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(2, Byte), "ActualHours", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Qty", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "Qty", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PlannedHours", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(2, Byte), "PlannedHours", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ActualDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"ActualDate",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CustomerDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"CustomerDate",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_MileStoneId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"MileStoneId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_OwnerId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"OwnerId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_PlannedDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"PlannedDate",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ActualHours", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input,True, CType(18, Byte), CType(2, Byte),"ActualHours",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Qty", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"Qty",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_PlannedHours", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input,True, CType(18, Byte), CType(2, Byte),"PlannedHours",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_PreSalesMileStoneId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"PreSalesMileStoneId",System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_PreSalesId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"PreSalesId",System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_AccurideLocationid", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"AccurideLocationid",System.Data.DataRowVersion.Current, Nothing))
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
            Return "tPreSalesMileStone"
        End Get
    End Property

    '<System.Web.Services.WebMethodAttribute()> Public Function Test() As DataSet
    '    Dim x As DataSet
    '    x = LoadData(New Object() {46792, 0, 1})
    '    ' Return x
    '    x.Tables(0).Rows(4).Item("PlannedDate") = CType(x.Tables(0).Rows(4).Item("PlannedDate").ToString, Date).AddYears(20)
    '    Return UpdateData(x.GetChanges)
    'End Function


    <System.Web.Services.WebMethodAttribute()> Public Function LoadCombo(ByVal pintUniqueId As Integer, ByVal pintAccurideLocationId As Integer) As System.Data.DataSet Implements IEPS_Service_Data.LoadCombo
        Return Nothing 'LoadData(New Object() {pintAccurideLocationId, pintUniqueId, 1})
    End Function



    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadData(ByVal pudtParams() As System.Object) As System.Data.DataSet Implements IEPS_Service_Data.LoadData
        'Dim x As New DataSet()
        SqlDataAdapter1.SelectCommand.Parameters("@ProjectId").Value = pudtParams(0)
        SqlDataAdapter1.SelectCommand.Parameters("@AccurideLocationId").Value = pudtParams(2)
        SqlDataAdapter1.SelectCommand.Parameters("@PreSalesMileStoneId").Value = pudtParams(1)
        'If pudtParams.GetUpperBound(0) = 3 Then
        '    SqlDataAdapter1.SelectCommand.Parameters("@LoadCombo").Value = 1
        'Else
        '    SqlDataAdapter1.SelectCommand.Parameters("@LoadCombo").Value = Convert.DBNull
        'End If
         Return LoadDataSet(SqlDataAdapter1)
    End Function
    '<System.Web.Services.WebMethodAttribute()> _
    'Public Function Test() As DataSet
    '    Dim x As DataSet = LoadData(New Object() {46826, 0, 1})
    '    x.Tables(0).Rows(1).Item("CustomerDate") = Convert.DBNull
    '    Return UpdateData(x)
    'End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function UpdateData(ByVal Changes As System.Data.DataSet) As System.Data.DataSet Implements IEPS_Service_Data.UpdateData
        If Not (Changes Is Nothing) Then
            Return UpdateDataset(Changes, SqlDataAdapter1)
        Else
            Return Nothing
        End If
    End Function

	Public Overrides Sub AssignFields()
		With Fields
			.Add("Owner", New FieldInfoStructure("Owner", 50, True, False))
			.Add("PlannedDate", New FieldInfoStructure("PlannedDate", 23, True, False))
			.Add("ActualDate", New FieldInfoStructure("ActualDate", 23, True, False))
			.Add("CustomerDate", New FieldInfoStructure("CustomerDate", 23, True, False))
			.Add("ActualHours", New FieldInfoStructure("ActualHours", 18, True, False))
			.Add("PlannedHours", New FieldInfoStructure("PlannedHours", 18, True, False))
			.Add("MileStoneId", New FieldInfoStructure("MileStoneId", 10, False, False))
			.Add("OwnerId", New FieldInfoStructure("OwnerId", 10, True, False))
			.Add("Qty", New FieldInfoStructure("Qty", 10, True, False))
			.Add("PreSalesId", New FieldInfoStructure("PreSalesId", 10, False, False))
			.Add("PreSalesMileStoneId", New FieldInfoStructure("PreSalesMileStoneId", 10, False, False))
		End With
	End Sub
End Class

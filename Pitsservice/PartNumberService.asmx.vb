Imports System.Web.Services
Imports System.Data.SqlClient
<System.Web.Services.WebService(Namespace:="http://www.Accuride.com/PartNumberService")> _
Public Class PartNumberService
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

    'Required by the Web Services Designer
    Private components As System.ComponentModel.Container

    'NOTE: The following procedure is required by the Web Services Designer
    'It can be modified using the Web Services Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.SqlDataAdapter1 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
        '
        'SqlDataAdapter1
        '
        Me.SqlDataAdapter1.SelectCommand = Me.SqlSelectCommand1
        Me.SqlDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Select_Item", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ItemId", "ItemId"), New System.Data.Common.DataColumnMapping("Inactive", "Inactive"), New System.Data.Common.DataColumnMapping("StockItemNo", "StockItemNo"), New System.Data.Common.DataColumnMapping("ModifiedBy", "ModifiedBy"), New System.Data.Common.DataColumnMapping("ModifiedDate", "ModifiedDate"), New System.Data.Common.DataColumnMapping("ItemDescription", "ItemDescription"), New System.Data.Common.DataColumnMapping("SourceNumber", "SourceNumber"), New System.Data.Common.DataColumnMapping("SlideSeries", "SlideSeries"), New System.Data.Common.DataColumnMapping("PlatingCode", "PlatingCode"), New System.Data.Common.DataColumnMapping("VariantNumber", "VariantNumber"), New System.Data.Common.DataColumnMapping("Item", "Item"), New System.Data.Common.DataColumnMapping("UOMId", "UOMId"), New System.Data.Common.DataColumnMapping("UnitPrice", "UnitPrice"), New System.Data.Common.DataColumnMapping("CurrencyDate", "CurrencyDate"), New System.Data.Common.DataColumnMapping("WhereUsed", "WhereUsed"), New System.Data.Common.DataColumnMapping("StockPartNumber", "StockPartNumber"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("Description_Alternative", "Description_Alternative"), New System.Data.Common.DataColumnMapping("Description_Alternative1", "Description_Alternative1"), New System.Data.Common.DataColumnMapping("Description_Alternative2", "Description_Alternative2"), New System.Data.Common.DataColumnMapping("Description_Alternative3", "Description_Alternative3"), New System.Data.Common.DataColumnMapping("AccurideLocationId", "AccurideLocationId")}), New System.Data.Common.DataTableMapping("Table1", "Table1", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ItemId", "ItemId"), New System.Data.Common.DataColumnMapping("Inactive", "Inactive"), New System.Data.Common.DataColumnMapping("StockItemNo", "StockItemNo"), New System.Data.Common.DataColumnMapping("ModifiedBy", "ModifiedBy"), New System.Data.Common.DataColumnMapping("ModifiedDate", "ModifiedDate"), New System.Data.Common.DataColumnMapping("ItemDescription", "ItemDescription"), New System.Data.Common.DataColumnMapping("SourceNumber", "SourceNumber"), New System.Data.Common.DataColumnMapping("SlideSeries", "SlideSeries"), New System.Data.Common.DataColumnMapping("PlatingCode", "PlatingCode"), New System.Data.Common.DataColumnMapping("VariantNumber", "VariantNumber"), New System.Data.Common.DataColumnMapping("Item", "Item"), New System.Data.Common.DataColumnMapping("UOMId", "UOMId"), New System.Data.Common.DataColumnMapping("UnitPrice", "UnitPrice"), New System.Data.Common.DataColumnMapping("CurrencyDate", "CurrencyDate"), New System.Data.Common.DataColumnMapping("WhereUsed", "WhereUsed"), New System.Data.Common.DataColumnMapping("StockPartNumber", "StockPartNumber"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("Description_Alternative", "Description_Alternative"), New System.Data.Common.DataColumnMapping("Description_Alternative1", "Description_Alternative1"), New System.Data.Common.DataColumnMapping("Description_Alternative2", "Description_Alternative2"), New System.Data.Common.DataColumnMapping("Description_Alternative3", "Description_Alternative3"), New System.Data.Common.DataColumnMapping("AccurideLocationId", "AccurideLocationId")}), New System.Data.Common.DataTableMapping("Table2", "Table2", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember"), New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember"), New System.Data.Common.DataColumnMapping("ItemId", "ItemId")})})
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "[ps_Select_Item]"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_ItemId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"ItemId",System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@LoadCombo", System.Data.SqlDbType.Bit, 1))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"AccurideLocationId",System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = strConnectionString

    End Sub


#End Region

    Public ReadOnly Property TableName() As String Implements IEPS_Service_Data.TableName
        Get
            Return "tItem"
        End Get
    End Property

    '<System.Web.Services.WebMethodAttribute()> _
    'Public Function Test() As DataSet
    '    Return LoadData(New Object() {0, 1})
    'End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadData(ByVal pudtParams() As System.Object) As System.Data.DataSet Implements IEPS_Service_Data.LoadData
        SqlDataAdapter1.SelectCommand.Parameters("@Select_ItemId").Value = CType(pudtParams(0), Int32)
        SqlDataAdapter1.SelectCommand.Parameters("@Select_AccurideLocationId").Value = CType(pudtParams(1), Int32)
        If pudtParams.GetUpperBound(0) = 2 Then
            SqlDataAdapter1.SelectCommand.Parameters("@LoadCombo").Value = 1
        Else
            SqlDataAdapter1.SelectCommand.Parameters("@LoadCombo").Value = Convert.DBNull
        End If
        Return LoadDataSet(SqlDataAdapter1)
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function UpdateData(ByVal Changes As System.Data.DataSet) As System.Data.DataSet Implements IEPS_Service_Data.UpdateData
        Return UpdateDataset(Changes, SqlDataAdapter1)
    End Function


    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadCombo(ByVal pintUniqueId As Int32, ByVal pintAccurideLocationId As Int32) As DataSet Implements IEPS_Service_Data.LoadCombo
        Return LoadData(New Object() {pintUniqueId, 1, pintAccurideLocationId})
    End Function

	Public Overrides Sub AssignFields()
		With Fields
			.Add("Item", New FieldInfoStructure("Item", 10, False, False))
			.Add("ItemId", New FieldInfoStructure("ItemId", 10, False, False))
			.Add("Inactive", New FieldInfoStructure("Inactive", 1, False, False, , False))
			.Add("StockItemNo", New FieldInfoStructure("StockItemNo", 250, False, False))
			.Add("ModifiedBy", New FieldInfoStructure("ModifiedBy", 10, True, False))
			.Add("ModifiedDate", New FieldInfoStructure("ModifiedDate", 23, False, False, , "GETDATE"))
			.Add("ItemDescription", New FieldInfoStructure("ItemDescription", 250, False, False))
			.Add("SourceId", New FieldInfoStructure("SourceId", 10, False, False))
			.Add("SlideSeriesId", New FieldInfoStructure("SlideSeriesId", 10, False, False))
			.Add("PlatingId", New FieldInfoStructure("PlatingId", 10, False, False))
			.Add("VariantId", New FieldInfoStructure("VariantId", 10, False, False))
			.Add("UOMId", New FieldInfoStructure("UOMId", 10, True, False))
			.Add("UnitPrice", New FieldInfoStructure("UnitPrice", 19, True, False))
			.Add("CurrencyDate", New FieldInfoStructure("CurrencyDate", 23, True, False, , "GETDATE"))
			.Add("WhereUsed", New FieldInfoStructure("WhereUsed", 1000, True, False))
			.Add("Description_Alternative3", New FieldInfoStructure("Description_Alternative3", 50, True, False))
			.Add("SheetLength", New FieldInfoStructure("SheetLength", 18, True, False))
			.Add("CoilWidth", New FieldInfoStructure("CoilWidth", 18, True, False))
			.Add("Thickness", New FieldInfoStructure("Thickness", 18, True, False))
			.Add("Density", New FieldInfoStructure("Density", 18, True, False, , 7850))
			.Add("Factor", New FieldInfoStructure("Factor", 18, True, False))
		End With
	End Sub
End Class

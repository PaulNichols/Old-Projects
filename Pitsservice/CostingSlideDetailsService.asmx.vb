Imports System.Web.Services
Imports System.Data.SqlClient
<System.Web.Services.WebService(Namespace:="http://www.Accuride.com/CostingSlideDetailsService")> _
Public Class CostingSlideDetailsService
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
        Me.SqlDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Select_CostingSlideDetails", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CompanyName", "CompanyName"), New System.Data.Common.DataColumnMapping("ProjectMasterGroup", "ProjectMasterGroup"), New System.Data.Common.DataColumnMapping("AnnualQuantity", "AnnualQuantity"), New System.Data.Common.DataColumnMapping("BatchSize", "BatchSize"), New System.Data.Common.DataColumnMapping("PlatingCode", "PlatingCode"), New System.Data.Common.DataColumnMapping("AccuridePartNumber", "AccuridePartNumber"), New System.Data.Common.DataColumnMapping("ProductLength", "ProductLength"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("CurrencyToId", "CurrencyToId"), New System.Data.Common.DataColumnMapping("Pairs", "Pairs")}), New System.Data.Common.DataTableMapping("Table1", "Table1", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("MaterialCode", "MaterialCode"), New System.Data.Common.DataColumnMapping("Abbreviation", "Abbreviation"), New System.Data.Common.DataColumnMapping("MaterialId", "MaterialId"), New System.Data.Common.DataColumnMapping("CostingSlideMaterialId", "CostingSlideMaterialId"), New System.Data.Common.DataColumnMapping("Length", "Length"), New System.Data.Common.DataColumnMapping("CostingSlideTypeId", "CostingSlideTypeId"), New System.Data.Common.DataColumnMapping("IsRH", "IsRH"), New System.Data.Common.DataColumnMapping("Pairs", "Pairs"), New System.Data.Common.DataColumnMapping("CostingSlideId", "CostingSlideId")}), New System.Data.Common.DataTableMapping("Table2", "Table2", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("MaterialCode", "MaterialCode"), New System.Data.Common.DataColumnMapping("CostingRetainerId", "CostingRetainerId"), New System.Data.Common.DataColumnMapping("MaterialId", "MaterialId"), New System.Data.Common.DataColumnMapping("NoOfBalls", "NoOfBalls"), New System.Data.Common.DataColumnMapping("IsRH", "IsRH"), New System.Data.Common.DataColumnMapping("CostingSlideId", "CostingSlideId"), New System.Data.Common.DataColumnMapping("Pairs", "Pairs")}), New System.Data.Common.DataTableMapping("Table3", "Table3", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("MouldigCode", "MouldigCode"), New System.Data.Common.DataColumnMapping("CostingSlideMouldingLinkId", "CostingSlideMouldingLinkId"), New System.Data.Common.DataColumnMapping("IsRH", "IsRH"), New System.Data.Common.DataColumnMapping("CostingSlideId", "CostingSlideId"), New System.Data.Common.DataColumnMapping("MouldingId", "MouldingId"), New System.Data.Common.DataColumnMapping("Pairs", "Pairs")}), New System.Data.Common.DataTableMapping("Table4", "Table4", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CostingSlideBracketLinkId", "CostingSlideBracketLinkId"), New System.Data.Common.DataColumnMapping("CostingSlideId", "CostingSlideId"), New System.Data.Common.DataColumnMapping("BracketId", "BracketId"), New System.Data.Common.DataColumnMapping("IsRH", "IsRH"), New System.Data.Common.DataColumnMapping("BracketNumber", "BracketNumber"), New System.Data.Common.DataColumnMapping("Pairs", "Pairs")})})
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "ps_Select_CostingItemLink"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CostingSlideId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Pairs", System.Data.SqlDbType.Bit, 1))
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
    '    Return LoadData(New Object() {10793, 1})
    'End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadData(ByVal pudtParams() As System.Object) As System.Data.DataSet Implements IEPS_Service_Data.LoadData
        'Dim x As New DataSet()
        'SqlDataAdapter1.SelectCommand.Parameters("@CostingSlideId").Value = CType(pudtParams(0), Int32)
        'SqlDataAdapter1.SelectCommand.Parameters("@AccurideLocationId").Value = CType(pudtParams(1), Int32)
        ' Return LoadDataSet(SqlDataAdapter1)
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function test() As String
        Try
            Dim loadDS As DataSet = LoadLinkItems(True, 67, 1, 66)
            Dim ds As New DataSet
            Dim dt As New DataTable("Deletions")
            Dim dt1 As New DataTable("Updates")
            Dim dt2 As New DataTable("Inserts")

            For Each Col As DataColumn In loadDS.Tables(0).Columns
                dt.Columns.Add(Col.ColumnName)
                dt1.Columns.Add(Col.ColumnName)
                dt2.Columns.Add(Col.ColumnName)
            Next

            dt.ImportRow(loadDS.Tables(0).Rows(0))
            'Return dt.Rows(0).Item("Qty", DataRowVersion.Original).ToString
            ds.Tables.Add(dt)
            ds.Tables.Add(dt1)
            ds.Tables.Add(dt2)

            'Return ds.Tables("Updates").Rows.Count.ToString
            SaveCostingItems(True, 67, 77, ds)
        Catch ex As Exception
            Return ex.ToString
        End Try
    End Function

    <System.Web.Services.WebMethodAttribute()> _
     Public Function SaveCostingItems(ByVal pairs As Boolean, ByVal costingSlideId As Int32, _
            ByVal userid As Int32, ByVal changes As DataSet) As Boolean

        'update is just updating by costinglinkid and only the left
        'deletions when in pairs is for lh and associated rh otherwise just normal
        'insert when in pair, 2 record should be added with duplicate data else just add the two records

        Dim Command As SqlCommand
        If Not SqlConnection1.State = ConnectionState.Open Then
            SqlConnection1.Open()
        End If

        Dim Trans As SqlClient.SqlTransaction = SqlConnection1.BeginTransaction

        If Not changes Is Nothing Then
            Dim Row As DataRow
            If Not changes.Tables("Deletions") Is Nothing AndAlso changes.Tables("Deletions").Rows.Count > 0 Then
                Command = New SqlCommand("ps_Delete_CostingItemLink", SqlConnection1, Trans)
                With Command
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add("@Pairs", SqlDbType.Bit)
                    .Parameters.Add("@Original_CostingItemLinkId", SqlDbType.Int)
                    .Parameters.Add("@Original_ItemId", SqlDbType.Int)
                    .Parameters.Add("@Original_UnitPrice", SqlDbType.Money)
                    .Parameters.Add("@Original_NoOfBalls", SqlDbType.Int)
                    .Parameters.Add("@Original_Quantity", SqlDbType.Decimal)
                    .Parameters.Add("@Original_CostingSlideTypeId", SqlDbType.Int)
                    .Parameters.Add("@Original_IsRH", SqlDbType.Bit)
                    .Parameters.Add("@Original_ToolingCost", SqlDbType.Money)
                    .Parameters.Add("@Original_MaterialItemId", SqlDbType.Int)
                    .Parameters.Add("@Original_ItemType", SqlDbType.Int)
                    .Parameters.Add("@Select_CostingSlideId", SqlDbType.Int)
                    .Parameters.Add("@Select_UserId", SqlDbType.Int)

                    For Each Row In changes.Tables("Deletions").Rows
                        .Parameters("@Pairs").Value = pairs
                        .Parameters("@Original_CostingItemLinkId").Value = Row.Item("CostingItemLinkId", DataRowVersion.Original)
                        .Parameters("@Original_ItemId").Value = Row.Item("ItemId", DataRowVersion.Original)
                        .Parameters("@Original_UnitPrice").Value = Row.Item("UnitPrice", DataRowVersion.Original)
                        .Parameters("@Original_NoOfBalls").Value = Row.Item("NoOfBalls", DataRowVersion.Original)
                        .Parameters("@Original_Quantity").Value = Row.Item("Qty", DataRowVersion.Original)
                        .Parameters("@Original_CostingSlideTypeId").Value = Row.Item("CostingSlideTypeId", DataRowVersion.Original)
                        .Parameters("@Original_IsRH").Value = Row.Item("IsRH", DataRowVersion.Original)
                        .Parameters("@Original_ToolingCost").Value = Row.Item("ToolCost", DataRowVersion.Original)
                        .Parameters("@Original_MaterialItemId").Value = Row.Item("MaterialItemId", DataRowVersion.Original)
                        .Parameters("@Original_ItemType").Value = Row.Item("ItemType", DataRowVersion.Original)
                        .Parameters("@Select_CostingSlideId").Value = costingSlideId
                        .Parameters("@Select_UserId").Value = userid
                        .ExecuteNonQuery()
                    Next Row
                End With
            End If
            If changes.Tables("Updates").Rows.Count > 0 Then
                Command = New SqlCommand("ps_Update_CostingItemLink", SqlConnection1, Trans)
                With Command
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add("@Pairs", SqlDbType.Bit)
                    .Parameters.Add("@CostingItemLinkId", SqlDbType.Int)
                    .Parameters.Add("@ItemId", SqlDbType.Int)
                    .Parameters.Add("@UnitPrice", SqlDbType.Money)
                    .Parameters.Add("@NoOfBalls", SqlDbType.Int)
                    .Parameters.Add("@Quantity", SqlDbType.Decimal)
                    .Parameters.Add("@CostingSlideTypeId", SqlDbType.Int)
                    .Parameters.Add("@IsRH", SqlDbType.Bit)
                    .Parameters.Add("@ToolingCost", SqlDbType.Money)
                    .Parameters.Add("@MaterialItemId", SqlDbType.Int)
                    .Parameters.Add("@ItemType", SqlDbType.Int)
                    .Parameters.Add("@Select_UserId", SqlDbType.Int)
                    .Parameters.Add("@Select_CostingSlideId", SqlDbType.Int)

                    .Parameters.Add("@Original_ItemId", SqlDbType.Int)
                    .Parameters.Add("@Original_UnitPrice", SqlDbType.Money)
                    .Parameters.Add("@Original_NoOfBalls", SqlDbType.Int)
                    .Parameters.Add("@Original_Quantity", SqlDbType.Decimal)
                    .Parameters.Add("@Original_CostingSlideTypeId", SqlDbType.Int)
                    .Parameters.Add("@Original_IsRH", SqlDbType.Bit)
                    .Parameters.Add("@Original_ToolingCost", SqlDbType.Money)
                    .Parameters.Add("@Original_MaterialItemId", SqlDbType.Int)
                    .Parameters.Add("@Original_ItemType", SqlDbType.Int)

                    For Each Row In changes.Tables("Updates").Rows
                        .Parameters("@Pairs").Value = pairs
                        .Parameters("@CostingItemLinkId").Value = Row.Item("CostingItemLinkId")
                        .Parameters("@ItemId").Value = Row.Item("ItemId")
                        .Parameters("@UnitPrice") = New SqlParameter("@UnitPrice", SqlDbType.Money, 8, System.Data.ParameterDirection.Input, True, CType(19, Byte), CType(0, Byte), "", DataRowVersion.Current, Nothing)
                        .Parameters("@UnitPrice").Value = Row.Item("UnitPrice", DataRowVersion.Current)
                        .Parameters("@NoOfBalls") = New SqlParameter("@NoOfBalls", SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, 0, 0, "", DataRowVersion.Current, Nothing)
                        .Parameters("@NoOfBalls").Value = Row.Item("NoOfBalls", DataRowVersion.Current)
                        .Parameters("@Quantity") = New SqlParameter("@Quantity", SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, 18, 2, "", DataRowVersion.Current, Nothing)
                        .Parameters("@Quantity").Value = Row.Item("Qty")
                        .Parameters("@CostingSlideTypeId") = New SqlParameter("@CostingSlideTypeId", SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, 0, 0, "", DataRowVersion.Current, Nothing)
                        .Parameters("@CostingSlideTypeId").Value = Row.Item("CostingSlideTypeId")
                        .Parameters("@IsRH") = New SqlParameter("@IsRH", SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, 0, 0, "", DataRowVersion.Current, Nothing)
                        .Parameters("@IsRH").Value = Row.Item("IsRH", DataRowVersion.Current)
                        .Parameters("@ToolingCost") = New SqlParameter("@ToolingCost", SqlDbType.Money, 8, System.Data.ParameterDirection.Input, True, CType(19, Byte), CType(0, Byte), "", DataRowVersion.Current, Nothing)
                        .Parameters("@ToolingCost").Value = Row.Item("ToolCost", DataRowVersion.Current)
                        .Parameters("@MaterialItemId") = New SqlParameter("@MaterialItemId", SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, 0, 0, "", DataRowVersion.Current, Nothing)
                        .Parameters("@MaterialItemId").Value = Row.Item("MaterialItemId")
                        .Parameters("@ItemType") = New SqlParameter("@ItemType", SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, 0, 0, "", DataRowVersion.Current, Nothing)
                        .Parameters("@ItemType").Value = Row.Item("ItemType")
                        .Parameters("@Select_UserId").Value = userid
                        .Parameters("@Select_CostingSlideId").Value = costingSlideId

                        .Parameters("@Original_ItemId").Value = Row.Item("ItemId", DataRowVersion.Original)
                        .Parameters("@Original_UnitPrice") = New SqlParameter("@Original_UnitPrice", SqlDbType.Money, 8, System.Data.ParameterDirection.Input, True, CType(19, Byte), CType(0, Byte), "", DataRowVersion.Original, Nothing)
                        .Parameters("@Original_UnitPrice").Value = Row.Item("UnitPrice", DataRowVersion.Original)
                        .Parameters("@Original_NoOfBalls") = New SqlParameter("@Original_NoOfBalls", SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, 0, 0, "", DataRowVersion.Original, Nothing)
                        .Parameters("@Original_NoOfBalls").Value = Row.Item("NoOfBalls", DataRowVersion.Original)
                        .Parameters("@Original_Quantity") = New SqlParameter("@Original_Quantity", SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, 18, 2, "", DataRowVersion.Original, Nothing)
                        .Parameters("@Original_Quantity").Value = Row.Item("Qty", DataRowVersion.Original)
                        .Parameters("@Original_CostingSlideTypeId") = New SqlParameter("@Original_CostingSlideTypeId", SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, 0, 0, "", DataRowVersion.Original, Nothing)
                        .Parameters("@Original_CostingSlideTypeId").Value = Row.Item("CostingSlideTypeId")
                        .Parameters("@Original_IsRH") = New SqlParameter("@Original_IsRH", SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, 0, 0, "", DataRowVersion.Original, Nothing)
                        .Parameters("@Original_IsRH").Value = Row.Item("IsRH", DataRowVersion.Original)
                        .Parameters("@Original_ToolingCost") = New SqlParameter("@Original_ToolingCost", SqlDbType.Money, 8, System.Data.ParameterDirection.Input, True, CType(19, Byte), CType(0, Byte), "", DataRowVersion.Original, Nothing)
                        .Parameters("@Original_ToolingCost").Value = Row.Item("ToolCost", DataRowVersion.Original)
                        .Parameters("@Original_MaterialItemId") = New SqlParameter("@Original_MaterialItemId", SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, 0, 0, "", DataRowVersion.Original, Nothing)
                        .Parameters("@Original_MaterialItemId").Value = Row.Item("MaterialItemId")
                        .Parameters("@Original_ItemType") = New SqlParameter("@Original_ItemType", SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, 0, 0, "", DataRowVersion.Original, Nothing)
                        .Parameters("@Original_ItemType").Value = Row.Item("ItemType")

                        If .ExecuteNonQuery = 0 Then
                            Trans.Rollback()
                            Return False
                        End If
                    Next Row
                End With
            End If

            If changes.Tables("Inserts").Rows.Count > 0 Then
                Dim LhId As Int32
                Command = New SqlCommand("ps_Insert_CostingItemLink", SqlConnection1, Trans)
                With Command
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add("@ItemId", SqlDbType.Int)
                    .Parameters.Add("@UnitPrice", SqlDbType.Money)
                    .Parameters.Add("@NoOfBalls", SqlDbType.Int)
                    .Parameters.Add("@Quantity", SqlDbType.Decimal)
                    .Parameters.Add("@CostingSlideTypeId", SqlDbType.Int)
                    .Parameters.Add("@IsRH", SqlDbType.Bit)
                    .Parameters.Add("@ToolingCost", SqlDbType.Money)
                    .Parameters.Add("@MaterialItemId", SqlDbType.Int)
                    .Parameters.Add("@ItemType", SqlDbType.Int)
                    .Parameters.Add("@LhId", SqlDbType.Int)
                    .Parameters.Add("@Pairs", SqlDbType.Bit)
                    .Parameters.Add("@Select_CostingSlideId", SqlDbType.Int)
                    .Parameters.Add("@Select_UserId", SqlDbType.Int)

                    For Each Row In changes.Tables("Inserts").Rows
                        .Parameters("@ItemId").Value = Row.Item("ItemId")
                        .Parameters("@UnitPrice").Value = Row.Item("UnitPrice")
                        .Parameters("@NoOfBalls").Value = Row.Item("NoOfBalls")
                        .Parameters("@Quantity").Value = Row.Item("Qty")
                        .Parameters("@CostingSlideTypeId").Value = Row.Item("CostingSlideTypeId")
                        .Parameters("@IsRH").Value = Row.Item("IsRH")
                        .Parameters("@ToolingCost").Value = Row.Item("ToolCost")
                        .Parameters("@MaterialItemId").Value = Row.Item("MaterialItemId")
                        .Parameters("@ItemType").Value = Row.Item("ItemType")
                        .Parameters("@Pairs").Value = pairs
                        .Parameters("@LhId").Value = LhId
                        .Parameters("@Select_CostingSlideId").Value = costingSlideId
                        .Parameters("@Select_UserId").Value = userid

                        LhId = CType(.ExecuteScalar, Int32)
                        If (LhId = 0 AndAlso Not pairs AndAlso CType(.Parameters("@IsRH").Value, Boolean)) Or _
                            (LhId = 0 AndAlso Not pairs AndAlso Not CType(.Parameters("@IsRH").Value, Boolean)) Then
                            'Trans.Rollback()
                            'Return False
                        End If
                    Next Row
                    Row = Nothing
                End With

            End If

            Try
                Trans.Commit()
                SqlConnection1.Close()
            Catch e As SystemException
                SqlConnection1.Close()
                Return False
            End Try
        End If
        Return True
    End Function

    <System.Web.Services.WebMethodAttribute()> _
     Public Function LoadLinkItems(ByVal pairs As Boolean, ByVal costingSlideId As Int32, ByVal accurideLocationId As Int32, _
                ByVal userId As Int32) As System.Data.DataSet

        SqlDataAdapter1.SelectCommand.Parameters("@CostingSlideId").Value = costingSlideId
        SqlDataAdapter1.SelectCommand.Parameters("@AccurideLocationId").Value = accurideLocationId
        SqlDataAdapter1.SelectCommand.Parameters("@UserId").Value = userId
        SqlDataAdapter1.SelectCommand.Parameters("@Pairs").Value = pairs
        SqlDataAdapter1.SelectCommand.Connection.Open()
        Dim DS As New DataSet
        SqlDataAdapter1.Fill(DS)
        Return DS
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function UpdateData(ByVal Changes As System.Data.DataSet) As System.Data.DataSet Implements IEPS_Service_Data.UpdateData
        Return Nothing 'UpdateDataset(Changes, SqlDataAdapter1)
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadCombo(ByVal pintUniqueId As Int32, ByVal pintAccurideLocationId As Int32) As DataSet Implements IEPS_Service_Data.LoadCombo
        Return Nothing
    End Function


End Class

Imports System.Web.Services
Imports System.Data.SqlClient
<System.Web.Services.WebService(Namespace:="http://www.Accuride.com/ItemService")> _
Public Class ItemService
    Inherits EPS_Service
    Implements IEPS_Service_Data

#Region " Web Services Designer Generated Code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Web Services Designer.
        InitializeComponent()

        'Add your own initialization code after the InitializeComponent() call

    End Sub

    'Required by the Web Services Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Web Services Designer
    'It can be modified using the Web Services Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents SqlDataAdapter1 As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlInsertCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.SqlDataAdapter1 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlInsertCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
        '
        'SqlDataAdapter1
        '
        Me.SqlDataAdapter1.InsertCommand = Me.SqlInsertCommand1
        Me.SqlDataAdapter1.SelectCommand = Me.SqlSelectCommand1
        Me.SqlDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Select_Item", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ItemId", "ItemId"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("SourceNumber", "SourceNumber"), New System.Data.Common.DataColumnMapping("AccurideLocationId", "AccurideLocationId")}), New System.Data.Common.DataTableMapping("Table1", "Table1", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ItemId", "ItemId"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("SourceNumber", "SourceNumber"), New System.Data.Common.DataColumnMapping("AccurideLocationId", "AccurideLocationId")}), New System.Data.Common.DataTableMapping("Table2", "Table2", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember"), New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember")}), New System.Data.Common.DataTableMapping("Table3", "Table3", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember"), New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember")})})
        Me.SqlDataAdapter1.UpdateCommand = Me.SqlUpdateCommand1
        '
        '
        Me.SqlSelectCommand1.CommandText = "[ps_Select_Item]"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ItemId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CategoryId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@LoadCombo", System.Data.SqlDbType.Bit, 1))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Just4ThShift", System.Data.SqlDbType.Bit, 1))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@NumberQueryString", System.Data.SqlDbType.VarChar, 255))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@NameQueryString", System.Data.SqlDbType.VarChar, 255))
        '
        'SqlInsertCommand1
        '
        Me.SqlInsertCommand1.CommandText = "[ps_Insert_Item]"
        Me.SqlInsertCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlInsertCommand1.Connection = Me.SqlConnection1
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "InActive", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@StockItemNo", System.Data.SqlDbType.VarChar, 250, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(10, Byte), "StockItemNo", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ModifiedBy", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "ModifiedBy", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ItemDescription", System.Data.SqlDbType.VarChar, 250, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "ItemDescription", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@UOMId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "UOMId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@UnitPrice", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, True, CType(19, Byte), CType(0, Byte), "UnitPrice", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@WhereUsed", System.Data.SqlDbType.VarChar, 1000, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "WhereUsed", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CoilWidth", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(2, Byte), "CoilWidth", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Thickness", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(2, Byte), "Thickness", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Density", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(2, Byte), "Density", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Factor", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(10, Byte), "Factor", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@LengthOfMaterialPerBall", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(2, Byte), "LengthOfMaterialPerBall", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AssociatedFSItem", System.Data.SqlDbType.VarChar, 250, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "AssociatedFSItem", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "AccurideLocationId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ItemType", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "ItemType", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ItemName", System.Data.SqlDbType.VarChar, 255, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(10, Byte), "ItemName", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@MaterialItemId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "MaterialItemId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ToolingCost", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, True, CType(19, Byte), CType(0, Byte), "ToolingCost", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = "[ps_Update_Item]"
        Me.SqlUpdateCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlUpdateCommand1.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "InActive", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@StockItemNo", System.Data.SqlDbType.VarChar, 250, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(10, Byte), "StockItemNo", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ModifiedBy", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "ModifiedBy", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ItemDescription", System.Data.SqlDbType.VarChar, 250, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "ItemDescription", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@UOMId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "UOMId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_ItemId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "ItemId", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@UnitPrice", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, False, CType(19, Byte), CType(0, Byte), "UnitPrice", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@WhereUsed", System.Data.SqlDbType.VarChar, 1000, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "WhereUsed", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CoilWidth", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(2, Byte), "CoilWidth", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Thickness", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(2, Byte), "Thickness", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Density", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(2, Byte), "Density", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Factor", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(10, Byte), "Factor", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@LengthOfMaterialPerBall", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(2, Byte), "LengthOfMaterialPerBall", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AssociatedFSItem", System.Data.SqlDbType.VarChar, 250, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "AssociatedFSItem", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "AccurideLocationId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@MaterialItemId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "MaterialItemId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ItemType", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "ItemType", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ItemName", System.Data.SqlDbType.VarChar, 255, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(10, Byte), "ItemName", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ToolingCost", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, False, CType(19, Byte), CType(0, Byte), "ToolingCost", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_InActive", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "InActive", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_StockItemNo", System.Data.SqlDbType.VarChar, 250, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "StockItemNo", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ItemDescription", System.Data.SqlDbType.VarChar, 250, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "ItemDescription", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_UOMId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "UOMId", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_UnitPrice", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, False, CType(19, Byte), CType(0, Byte), "UnitPrice", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_WhereUsed", System.Data.SqlDbType.VarChar, 1000, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "WhereUsed", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CoilWidth", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(2, Byte), "CoilWidth", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Thickness", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(2, Byte), "Thickness", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Density", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(2, Byte), "Density", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Factor", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(10, Byte), "Factor", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_LengthOfMaterialPerBall", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(2, Byte), "LengthOfMaterialPerBall", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_AssociatedFSItem", System.Data.SqlDbType.VarChar, 250, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "AssociatedFSItem", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "AccurideLocationId", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ItemType", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "ItemType", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ItemName", System.Data.SqlDbType.VarChar, 255, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(10, Byte), "ItemName", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ToolingCost", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, False, CType(19, Byte), CType(0, Byte), "ToolingCost", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_MaterialItemId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "MaterialItemId", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_ModifiedBy", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "ModifiedBy", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_CurrencyDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "CurrencyDate", System.Data.DataRowVersion.Original, Nothing))
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = strConnectionString

    End Sub

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        'CODEGEN: This procedure is required by the Web Services Designer
        'Do not modify it using the code editor.
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

#End Region

    Public ReadOnly Property TableName() As String Implements IEPS_Service_Data.TableName
        Get
            Return "tItem"
        End Get
    End Property

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadCombo(ByVal userId As Integer, ByVal accurideLocationId As Integer) As System.Data.DataSet Implements IEPS_Service_Data.LoadCombo
        Return LoadData(New Object() {userId, 0, accurideLocationId, 1})
    End Function


    <System.Web.Services.WebMethodAttribute()> _
      Public Function SaveMachines(ByVal itemId As Int32, ByVal machinesIds As String) As Boolean
        SqlConnection1.Open()
        Dim Trans As SqlTransaction = SqlConnection1.BeginTransaction
        Dim Command As New SqlCommand("ps_DeleteAllItemMachines", SqlConnection1, Trans)
        With Command
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add("@ItemId", itemId)
        End With

        Try
            Command.ExecuteNonQuery()
        Catch e As SystemException
            Return False
        End Try

        If Not machinesIds Is Nothing Then
            Dim Id As Int32
            Dim Machines As New ArrayList(machinesIds.Split("|"c))
            Command = New SqlCommand("ps_Insert_ItemMachine", SqlConnection1, Trans)
            With Command
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@MachineId", Id)
                .Parameters.Add("@ItemId", itemId)
            End With

            For Each Id In Machines
                Command.Parameters("@MachineId").Value = Id
                Command.Parameters("@ItemId").Value = itemId
                If Command.ExecuteNonQuery = 0 Then
                    Return False
                End If
            Next Id

            Try
                Trans.Commit()
            Catch e As SystemException
                Return False
            End Try
        Else
            Try
                Trans.Commit()
            Catch e As SystemException
                Return False
            End Try
        End If
        Return True
    End Function

    <System.Web.Services.WebMethodAttribute()> _
        Public Function LoadItemTypes(ByVal accurideLocationId As Int32) As String()
        Dim Command As New SqlCommand()
        With Command
            .Connection = SqlConnection1
            SqlConnection1.Open()
            .CommandText = "ps_Select_ItemType"
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add("@AccurideLocationId", accurideLocationId)
        End With
        Dim reader As SqlClient.SqlDataReader = Command.ExecuteReader(CommandBehavior.CloseConnection)
        Dim i As Int32 = 0
        Dim ReturnStrings As String()

        While reader.Read
            ReDim Preserve ReturnStrings(i)
            ReturnStrings(i) = String.Concat(reader.Item("ItemTypeId").ToString, "|", _
                reader.Item("Filter"), "|", _
                reader.Item("Description"))
            i += 1
        End While
        Return ReturnStrings
    End Function

    <System.Web.Services.WebMethodAttribute()> _
       Public Function GetMachines(ByVal itemId As Int32, ByVal accurideLocationId As Int32) As Object()
        Dim Command As New SqlCommand()
        With Command
            .Connection = SqlConnection1
            SqlConnection1.Open()
            .CommandText = "ps_Select_ItemMachines"
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add("@AccurideLocationId", accurideLocationId)
            .Parameters.Add("@ItemId", itemId)
        End With

        Dim ReturnValues As New ArrayList()

        Dim DataReader As SqlClient.SqlDataReader = Command.ExecuteReader(CommandBehavior.CloseConnection)
        While DataReader.Read
            ReturnValues.Add(String.Concat(DataReader.Item("MachineId").ToString, "|", _
                DataReader.Item("Description").ToString, "|", _
                DataReader.Item("Checked")))
        End While
        DataReader = Nothing
        Command = Nothing
        Return ReturnValues.ToArray
    End Function


    <System.Web.Services.WebMethodAttribute()> _
    Public Function UpdateData(ByVal Changes As System.Data.DataSet) As System.Data.DataSet Implements IEPS_Service_Data.UpdateData
        If Not (Changes Is Nothing) Then
            Return UpdateDataset(Changes, SqlDataAdapter1)
        Else
            Return Nothing
        End If
    End Function

    ' <System.Web.Services.WebMethodAttribute()> _
    'Public Function Test() As DataSet
    '     Return LoadData(New Object() {65, 0, 1})
    ' End Function


    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadData(ByVal pudtParams() As System.Object) As System.Data.DataSet Implements IEPS_Service_Data.LoadData
        'SqlDataAdapter1.SelectCommand.Parameters("@ItemId").Value = pudtParams(1)
        'SqlDataAdapter1.SelectCommand.Parameters("@AccurideLocationId").Value = pudtParams(2)
        'SqlDataAdapter1.SelectCommand.Parameters("@UserId").Value = pudtParams(0)
        'If pudtParams.GetUpperBound(0) = 3 Then
        '    SqlDataAdapter1.SelectCommand.Parameters("@LoadCombo").Value = 1
        'Else
        '    SqlDataAdapter1.SelectCommand.Parameters("@LoadCombo").Value = Convert.DBNull
        'End If
        'Return LoadDataSet(SqlDataAdapter1)
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadSeachControlDetails( _
        ByVal currentUserId As Int32, _
        ByVal accurideLocationId As Int32, _
        ByVal itemCategory As Int32, _
        ByVal Just4ThShift As Boolean, _
        ByVal NumberQueryString As String, _
        ByVal NameQueryString As String) As DataSet

        NumberQueryString = NumberQueryString.Replace("*"c, "%"c).Replace("?"c, "%"c)
        NameQueryString = NameQueryString.Replace("*"c, "%"c).Replace("?"c, "%"c)

        SqlDataAdapter1.SelectCommand.Parameters("@ItemId").Value = 0
        SqlDataAdapter1.SelectCommand.Parameters("@AccurideLocationId").Value = accurideLocationId
        SqlDataAdapter1.SelectCommand.Parameters("@LoadCombo").Value = 0
        SqlDataAdapter1.SelectCommand.Parameters("@Just4ThShift").Value = Just4ThShift
        SqlDataAdapter1.SelectCommand.Parameters("@CategoryId").Value = itemCategory
        SqlDataAdapter1.SelectCommand.Parameters("@NumberQueryString").Value = NumberQueryString
        SqlDataAdapter1.SelectCommand.Parameters("@NameQueryString").Value = NameQueryString
        SqlDataAdapter1.SelectCommand.Connection.Open()
        SqlDataAdapter1.SelectCommand.ExecuteNonQuery()

        Dim MaxCount As Int32 = CType(System.Configuration.ConfigurationSettings.AppSettings("SearchMaxCount"), Int32)
        Dim RecordCount As Int32 = CType(SqlDataAdapter1.SelectCommand.Parameters("@RETURN_VALUE").Value, Int32)
        If Not RecordCount > MaxCount Then
            Return LoadDataSet(SqlDataAdapter1)
        Else
            Dim ReturnSet As New DataSet()
            ReturnSet.Tables.Add("To_Many")
            ReturnSet.Tables("To_Many").Columns.Add("RecordCount")
            ReturnSet.Tables("To_Many").Columns.Add("MaxCount")
            ReturnSet.Tables("To_Many").Rows.Add(New Object() {RecordCount, MaxCount})
            Return ReturnSet
        End If
    End Function


    Public Overrides Sub AssignFields()
        With Fields
            .Add("ItemId", New FieldInfoStructure("ItemId", 10, False, False))
            .Add("ItemType", New FieldInfoStructure("ItemType", 10, True, False))
            .Add("Inactive", New FieldInfoStructure("Inactive", 1, True, False, , False))
            .Add("StockItemNo", New FieldInfoStructure("StockItemNo", 250, True, False))
            .Add("ModifiedBy", New FieldInfoStructure("ModifiedBy", 10, True, False))
            .Add("ModifiedDate", New FieldInfoStructure("ModifiedDate", 23, True, False, , "GETDATE"))
            .Add("ItemDescription", New FieldInfoStructure("ItemDescription", 250, True, False))
            .Add("UOMId", New FieldInfoStructure("UOMId", 10, True, False))
            .Add("UnitPrice", New FieldInfoStructure("UnitPrice", 19, True, False))
            .Add("CurrencyDate", New FieldInfoStructure("CurrencyDate", 23, True, False, , "GETDATE"))
            .Add("WhereUsed", New FieldInfoStructure("WhereUsed", 1000, True, False))
            .Add("Description_Alternative", New FieldInfoStructure("Description_Alternative3", 50, True, False))
            .Add("CoilWidth", New FieldInfoStructure("CoilWidth", 18, True, False))
            .Add("Thickness", New FieldInfoStructure("Thickness", 18, True, False))
            .Add("Density", New FieldInfoStructure("Density", 18, True, False, , 7850))
            .Add("Factor", New FieldInfoStructure("Factor", 18, True, False))
            .Add("LengthOfMaterialPerBall", New FieldInfoStructure("LengthOfMaterialPerBall", 18, True, False))
            .Add("ItemName", New FieldInfoStructure("ItemName", 255, False, False))
            .Add("AssociatedFSItem", New FieldInfoStructure("AssociatedFSItem", 250, True, False))
            .Add("AccurideLocationId", New FieldInfoStructure("AccurideLocationId", 10, False, False))
            .Add("ToolingCost", New FieldInfoStructure("ToolingCost", 19, True, False))
            .Add("MaterialItemId", New FieldInfoStructure("MaterialItemId", 10, True, False))
            .Add("Description_Alternative2", New FieldInfoStructure("Description_Alternative2", 50, True, False))
        End With
    End Sub
End Class

Imports System.Web.Services
Imports System.Data.SqlClient
<System.Web.Services.WebService(Namespace:="http://www.Accuride.com/MaterialService")> _
Public Class MaterialService
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
        Me.SqlDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Select_Material", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CoilWidth", "CoilWidth"), New System.Data.Common.DataColumnMapping("CostPerUnit", "CostPerUnit"), New System.Data.Common.DataColumnMapping("Density", "Density"), New System.Data.Common.DataColumnMapping("Factor", "Factor"), New System.Data.Common.DataColumnMapping("MaterialCode", "MaterialCode"), New System.Data.Common.DataColumnMapping("MaterialDescription", "MaterialDescription"), New System.Data.Common.DataColumnMapping("MaterialId", "MaterialId"), New System.Data.Common.DataColumnMapping("Notes", "Notes"), New System.Data.Common.DataColumnMapping("ProductName", "ProductName"), New System.Data.Common.DataColumnMapping("SheetLength", "SheetLength"), New System.Data.Common.DataColumnMapping("Thickness", "Thickness"), New System.Data.Common.DataColumnMapping("UOMId", "UOMId"), New System.Data.Common.DataColumnMapping("CodeAndName", "CodeAndName"), New System.Data.Common.DataColumnMapping("AccurideLocationId", "AccurideLocationId"), New System.Data.Common.DataColumnMapping("UserId", "UserId"), New System.Data.Common.DataColumnMapping("CurrencyDate", "CurrencyDate")}), New System.Data.Common.DataTableMapping("Table1", "Table1", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CoilWidth", "CoilWidth"), New System.Data.Common.DataColumnMapping("CostPerUnit", "CostPerUnit"), New System.Data.Common.DataColumnMapping("Density", "Density"), New System.Data.Common.DataColumnMapping("Factor", "Factor"), New System.Data.Common.DataColumnMapping("MaterialCode", "MaterialCode"), New System.Data.Common.DataColumnMapping("MaterialDescription", "MaterialDescription"), New System.Data.Common.DataColumnMapping("MaterialId", "MaterialId"), New System.Data.Common.DataColumnMapping("Notes", "Notes"), New System.Data.Common.DataColumnMapping("ProductName", "ProductName"), New System.Data.Common.DataColumnMapping("SheetLength", "SheetLength"), New System.Data.Common.DataColumnMapping("Thickness", "Thickness"), New System.Data.Common.DataColumnMapping("UOMId", "UOMId"), New System.Data.Common.DataColumnMapping("CodeAndName", "CodeAndName"), New System.Data.Common.DataColumnMapping("AccurideLocationId", "AccurideLocationId"), New System.Data.Common.DataColumnMapping("UserId", "UserId"), New System.Data.Common.DataColumnMapping("CurrencyDate", "CurrencyDate")}), New System.Data.Common.DataTableMapping("Table2", "Table2", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CoilWidth", "CoilWidth"), New System.Data.Common.DataColumnMapping("CostPerUnit", "CostPerUnit"), New System.Data.Common.DataColumnMapping("Density", "Density"), New System.Data.Common.DataColumnMapping("Factor", "Factor"), New System.Data.Common.DataColumnMapping("MaterialCode", "MaterialCode"), New System.Data.Common.DataColumnMapping("MaterialDescription", "MaterialDescription"), New System.Data.Common.DataColumnMapping("MaterialId", "MaterialId"), New System.Data.Common.DataColumnMapping("Notes", "Notes"), New System.Data.Common.DataColumnMapping("ProductName", "ProductName"), New System.Data.Common.DataColumnMapping("SheetLength", "SheetLength"), New System.Data.Common.DataColumnMapping("Thickness", "Thickness"), New System.Data.Common.DataColumnMapping("UOMId", "UOMId"), New System.Data.Common.DataColumnMapping("CodeAndName", "CodeAndName"), New System.Data.Common.DataColumnMapping("AccurideLocationId", "AccurideLocationId"), New System.Data.Common.DataColumnMapping("UserId", "UserId"), New System.Data.Common.DataColumnMapping("CurrencyDate", "CurrencyDate")}), New System.Data.Common.DataTableMapping("Table3", "Table3", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CoilWidth", "CoilWidth"), New System.Data.Common.DataColumnMapping("CostPerUnit", "CostPerUnit"), New System.Data.Common.DataColumnMapping("Density", "Density"), New System.Data.Common.DataColumnMapping("Factor", "Factor"), New System.Data.Common.DataColumnMapping("MaterialCode", "MaterialCode"), New System.Data.Common.DataColumnMapping("MaterialDescription", "MaterialDescription"), New System.Data.Common.DataColumnMapping("MaterialId", "MaterialId"), New System.Data.Common.DataColumnMapping("Notes", "Notes"), New System.Data.Common.DataColumnMapping("ProductName", "ProductName"), New System.Data.Common.DataColumnMapping("SheetLength", "SheetLength"), New System.Data.Common.DataColumnMapping("Thickness", "Thickness"), New System.Data.Common.DataColumnMapping("UOMId", "UOMId"), New System.Data.Common.DataColumnMapping("CodeAndName", "CodeAndName"), New System.Data.Common.DataColumnMapping("AccurideLocationId", "AccurideLocationId"), New System.Data.Common.DataColumnMapping("UserId", "UserId"), New System.Data.Common.DataColumnMapping("CurrencyDate", "CurrencyDate")}), New System.Data.Common.DataTableMapping("Table4", "Table4", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember"), New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember")}), New System.Data.Common.DataTableMapping("Table5", "Table5", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember"), New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember")}), New System.Data.Common.DataTableMapping("Table6", "Table6", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember"), New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember")}), New System.Data.Common.DataTableMapping("Table7", "Table7", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember"), New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember")})})
        Me.SqlDataAdapter1.UpdateCommand = Me.SqlUpdateCommand1
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "ps_Select_Material"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@MaterialId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@LoadCombo", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlInsertCommand1
        '
        Me.SqlInsertCommand1.CommandText = "ps_Insert_Material"
        Me.SqlInsertCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlInsertCommand1.Connection = Me.SqlConnection1
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CoilWidth", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, True, CType(15, Byte), CType(0, Byte), "CoilWidth", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CostPerUnit", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, True, CType(19, Byte), CType(0, Byte), "CostPerUnit", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Density", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, True, CType(15, Byte), CType(0, Byte), "Density", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Factor", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, True, CType(15, Byte), CType(0, Byte), "Factor", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@MaterialCode", System.Data.SqlDbType.Char, 15, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "MaterialCode", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@MaterialDescription", System.Data.SqlDbType.Char, 50, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "MaterialDescription", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Notes", System.Data.SqlDbType.Char, 50, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Notes", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProductName", System.Data.SqlDbType.Char, 50, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "ProductName", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@SheetLength", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, True, CType(15, Byte), CType(0, Byte), "SheetLength", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Thickness", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, True, CType(15, Byte), CType(0, Byte), "Thickness", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@UOMId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "UOMId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "AccurideLocationId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CurrencyDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "CurrencyDate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"UserId",System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = "ps_Update_Material"
        Me.SqlUpdateCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlUpdateCommand1.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CoilWidth", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, True, CType(15, Byte), CType(0, Byte), "CoilWidth", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CostPerUnit", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, True, CType(19, Byte), CType(0, Byte), "CostPerUnit", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Density", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, True, CType(15, Byte), CType(0, Byte), "Density", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Factor", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, True, CType(15, Byte), CType(0, Byte), "Factor", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@MaterialCode", System.Data.SqlDbType.Char, 15, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "MaterialCode", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@MaterialDescription", System.Data.SqlDbType.Char, 50, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "MaterialDescription", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Notes", System.Data.SqlDbType.Char, 50, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Notes", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProductName", System.Data.SqlDbType.Char, 50, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "ProductName", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@SheetLength", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, True, CType(15, Byte), CType(0, Byte), "SheetLength", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Thickness", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, True, CType(15, Byte), CType(0, Byte), "Thickness", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@UOMId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "UOMId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "AccurideLocationId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CurrencyDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "CurrencyDate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CoilWidth", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input,True, CType(15, Byte), CType(0, Byte),"CoilWidth",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CostPerUnit", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input,True, CType(19, Byte), CType(0, Byte),"CostPerUnit",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Density", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input,True, CType(15, Byte), CType(0, Byte),"Density",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Factor", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input,True, CType(15, Byte), CType(0, Byte),"Factor",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_MaterialCode", System.Data.SqlDbType.Char, 15, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"MaterialCode",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_MaterialDescription", System.Data.SqlDbType.Char, 50, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"MaterialDescription",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Notes", System.Data.SqlDbType.Char, 50, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"Notes",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ProductName", System.Data.SqlDbType.Char, 50, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"ProductName",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_SheetLength", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input,True, CType(15, Byte), CType(0, Byte),"SheetLength",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Thickness", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input,True, CType(15, Byte), CType(0, Byte),"Thickness",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_UOMId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"UOMId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"AccurideLocationId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CurrencyDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"CurrencyDate",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_MaterialId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"MaterialId",System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"UserId",System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlDeleteCommand1
        '
        Me.SqlDeleteCommand1.CommandText = "ps_Delete_Material"
        Me.SqlDeleteCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlDeleteCommand1.Connection = Me.SqlConnection1
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CoilWidth", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, True, CType(15, Byte), CType(0, Byte), "CoilWidth", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CostPerUnit", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, True, CType(19, Byte), CType(0, Byte), "CostPerUnit", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Density", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, True, CType(15, Byte), CType(0, Byte), "Density", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Factor", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, True, CType(15, Byte), CType(0, Byte), "Factor", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@MaterialCode", System.Data.SqlDbType.Char, 15, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "MaterialCode", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@MaterialDescription", System.Data.SqlDbType.Char, 50, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "MaterialDescription", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Notes", System.Data.SqlDbType.Char, 50, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Notes", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProductName", System.Data.SqlDbType.Char, 50, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "ProductName", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@SheetLength", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, True, CType(15, Byte), CType(0, Byte), "SheetLength", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Thickness", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, True, CType(15, Byte), CType(0, Byte), "Thickness", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@UOMId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "UOMId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "AccurideLocationId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CurrencyDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "CurrencyDate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CoilWidth", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input,True, CType(15, Byte), CType(0, Byte),"CoilWidth",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CostPerUnit", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input,True, CType(19, Byte), CType(0, Byte),"CostPerUnit",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Density", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input,True, CType(15, Byte), CType(0, Byte),"Density",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Factor", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input,True, CType(15, Byte), CType(0, Byte),"Factor",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_MaterialCode", System.Data.SqlDbType.Char, 15, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"MaterialCode",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_MaterialDescription", System.Data.SqlDbType.Char, 50, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"MaterialDescription",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Notes", System.Data.SqlDbType.Char, 50, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"Notes",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ProductName", System.Data.SqlDbType.Char, 50, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"ProductName",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_SheetLength", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input,True, CType(15, Byte), CType(0, Byte),"SheetLength",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Thickness", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input,True, CType(15, Byte), CType(0, Byte),"Thickness",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_UOMId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"UOMId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"AccurideLocationId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CurrencyDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"CurrencyDate",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_MaterialId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"MaterialId",System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"UserId",System.Data.DataRowVersion.Current, Nothing))
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
            Return "tMaterial"
        End Get
    End Property

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadData(ByVal pudtParams() As System.Object) As System.Data.DataSet Implements IEPS_Service_Data.LoadData
        'Dim x As New DataSet()
        SqlDataAdapter1.SelectCommand.Parameters("@AccurideLocationId").Value = CType(pudtParams(0), Int32)
        SqlDataAdapter1.SelectCommand.Parameters("@MaterialId").Value = CType(pudtParams(1), Int32)
        SqlDataAdapter1.SelectCommand.Parameters("@UserId").Value = CType(pudtParams(2), Int32)
        If pudtParams.GetUpperBound(0) = 3 Then
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
        Return LoadData(New Object() {pintAccurideLocationId, pintUniqueId, 0, 1})
    End Function
	Public Overrides Sub AssignFields()
		With Fields
			.Add("MaterialCode", New FieldInfoStructure("MaterialCode", 15, True, False))
			.Add("MaterialDescription", New FieldInfoStructure("MaterialDescription", 50, True, False))
			.Add("ProductName", New FieldInfoStructure("ProductName", 50, True, False))
			.Add("CurrencyDate", New FieldInfoStructure("CurrencyDate", 23, True, False, , "GETDATE"))
			.Add("CostPerUnit", New FieldInfoStructure("CostPerUnit", 19, True, False))
			.Add("CoilWidth", New FieldInfoStructure("CoilWidth", 18, True, False))
			.Add("SheetLength", New FieldInfoStructure("SheetLength", 18, True, False))
			.Add("Thickness", New FieldInfoStructure("Thickness", 18, True, False))
			.Add("Density", New FieldInfoStructure("Density", 18, False, False, , 7850))
			.Add("Factor", New FieldInfoStructure("Factor", 18, True, False))
			.Add("Description", New FieldInfoStructure("Description", 50, True, False))
			.Add("Notes", New FieldInfoStructure("Notes", 50, True, False))
			.Add("MaterialId", New FieldInfoStructure("MaterialId", 10, False, False))
			.Add("UOMId", New FieldInfoStructure("UOMId", 10, False, False))
			.Add("AccurideLocationId", New FieldInfoStructure("AccurideLocationId", 10, False, False))
		End With
	End Sub
End Class

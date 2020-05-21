Imports System.Web.Services
Imports System.Data.SqlClient
<System.Web.Services.WebService(Namespace:="http://www.Accuride.com/ToolingService")> _
Public Class ToolingService
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
    Friend WithEvents SqlDataAdapter2 As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlInsertCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlDeleteCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents SqlSelectCommand2 As System.Data.SqlClient.SqlCommand
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.SqlDataAdapter1 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlDataAdapter2 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlInsertCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlDeleteCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
        Me.SqlSelectCommand2 = New System.Data.SqlClient.SqlCommand()
        '
        'SqlDataAdapter1
        '
        Me.SqlDataAdapter1.DeleteCommand = Me.SqlDeleteCommand1
        Me.SqlDataAdapter1.InsertCommand = Me.SqlInsertCommand1
        Me.SqlDataAdapter1.SelectCommand = Me.SqlSelectCommand1
        Me.SqlDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Select_Tooling", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("CNCMillHours", "CNCMillHours"), New System.Data.Common.DataColumnMapping("CNCMillRate", "CNCMillRate"), New System.Data.Common.DataColumnMapping("DieSetCost", "DieSetCost"), New System.Data.Common.DataColumnMapping("HardeningCost", "HardeningCost"), New System.Data.Common.DataColumnMapping("LabourHours", "LabourHours"), New System.Data.Common.DataColumnMapping("LabourRate", "LabourRate"), New System.Data.Common.DataColumnMapping("MaterialsCost", "MaterialsCost"), New System.Data.Common.DataColumnMapping("ToolingId", "ToolingId"), New System.Data.Common.DataColumnMapping("ToolTypeId", "ToolTypeId"), New System.Data.Common.DataColumnMapping("TotalToolCost", "TotalToolCost"), New System.Data.Common.DataColumnMapping("WiringHours", "WiringHours"), New System.Data.Common.DataColumnMapping("WiringRate", "WiringRate"), New System.Data.Common.DataColumnMapping("UserId", "UserId"), New System.Data.Common.DataColumnMapping("CurrencyDate", "CurrencyDate"), New System.Data.Common.DataColumnMapping("AccurideLocationId", "AccurideLocationId")}), New System.Data.Common.DataTableMapping("Table1", "Table1", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("CNCMillHours", "CNCMillHours"), New System.Data.Common.DataColumnMapping("CNCMillRate", "CNCMillRate"), New System.Data.Common.DataColumnMapping("DieSetCost", "DieSetCost"), New System.Data.Common.DataColumnMapping("HardeningCost", "HardeningCost"), New System.Data.Common.DataColumnMapping("LabourHours", "LabourHours"), New System.Data.Common.DataColumnMapping("LabourRate", "LabourRate"), New System.Data.Common.DataColumnMapping("MaterialsCost", "MaterialsCost"), New System.Data.Common.DataColumnMapping("ToolingId", "ToolingId"), New System.Data.Common.DataColumnMapping("ToolTypeId", "ToolTypeId"), New System.Data.Common.DataColumnMapping("TotalToolCost", "TotalToolCost"), New System.Data.Common.DataColumnMapping("WiringHours", "WiringHours"), New System.Data.Common.DataColumnMapping("WiringRate", "WiringRate"), New System.Data.Common.DataColumnMapping("UserId", "UserId"), New System.Data.Common.DataColumnMapping("CurrencyDate", "CurrencyDate"), New System.Data.Common.DataColumnMapping("AccurideLocationId", "AccurideLocationId")}), New System.Data.Common.DataTableMapping("Table2", "Table2", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("CNCMillHours", "CNCMillHours"), New System.Data.Common.DataColumnMapping("CNCMillRate", "CNCMillRate"), New System.Data.Common.DataColumnMapping("DieSetCost", "DieSetCost"), New System.Data.Common.DataColumnMapping("HardeningCost", "HardeningCost"), New System.Data.Common.DataColumnMapping("LabourHours", "LabourHours"), New System.Data.Common.DataColumnMapping("LabourRate", "LabourRate"), New System.Data.Common.DataColumnMapping("MaterialsCost", "MaterialsCost"), New System.Data.Common.DataColumnMapping("ToolingId", "ToolingId"), New System.Data.Common.DataColumnMapping("ToolTypeId", "ToolTypeId"), New System.Data.Common.DataColumnMapping("TotalToolCost", "TotalToolCost"), New System.Data.Common.DataColumnMapping("WiringHours", "WiringHours"), New System.Data.Common.DataColumnMapping("WiringRate", "WiringRate"), New System.Data.Common.DataColumnMapping("UserId", "UserId"), New System.Data.Common.DataColumnMapping("CurrencyDate", "CurrencyDate"), New System.Data.Common.DataColumnMapping("AccurideLocationId", "AccurideLocationId")}), New System.Data.Common.DataTableMapping("Table3", "Table3", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("CNCMillHours", "CNCMillHours"), New System.Data.Common.DataColumnMapping("CNCMillRate", "CNCMillRate"), New System.Data.Common.DataColumnMapping("DieSetCost", "DieSetCost"), New System.Data.Common.DataColumnMapping("HardeningCost", "HardeningCost"), New System.Data.Common.DataColumnMapping("LabourHours", "LabourHours"), New System.Data.Common.DataColumnMapping("LabourRate", "LabourRate"), New System.Data.Common.DataColumnMapping("MaterialsCost", "MaterialsCost"), New System.Data.Common.DataColumnMapping("ToolingId", "ToolingId"), New System.Data.Common.DataColumnMapping("ToolTypeId", "ToolTypeId"), New System.Data.Common.DataColumnMapping("TotalToolCost", "TotalToolCost"), New System.Data.Common.DataColumnMapping("WiringHours", "WiringHours"), New System.Data.Common.DataColumnMapping("WiringRate", "WiringRate"), New System.Data.Common.DataColumnMapping("UserId", "UserId"), New System.Data.Common.DataColumnMapping("CurrencyDate", "CurrencyDate"), New System.Data.Common.DataColumnMapping("AccurideLocationId", "AccurideLocationId")}), New System.Data.Common.DataTableMapping("Table4", "Table4", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember"), New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember")}), New System.Data.Common.DataTableMapping("Table5", "Table5", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember"), New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember")})})
        Me.SqlDataAdapter1.UpdateCommand = Me.SqlUpdateCommand1
        '
        'SqlDataAdapter2
        '
        Me.SqlDataAdapter2.SelectCommand = Me.SqlSelectCommand2
        Me.SqlDataAdapter2.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Report_ToolingDetails", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CNCMillHours", "CNCMillHours"), New System.Data.Common.DataColumnMapping("CNCMillRate", "CNCMillRate"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("DieSetCost", "DieSetCost"), New System.Data.Common.DataColumnMapping("HardeningCost", "HardeningCost"), New System.Data.Common.DataColumnMapping("LabourHours", "LabourHours"), New System.Data.Common.DataColumnMapping("LabourRate", "LabourRate"), New System.Data.Common.DataColumnMapping("MaterialsCost", "MaterialsCost"), New System.Data.Common.DataColumnMapping("ToolingId", "ToolingId"), New System.Data.Common.DataColumnMapping("ToolTypeId", "ToolTypeId"), New System.Data.Common.DataColumnMapping("TotalToolCost", "TotalToolCost"), New System.Data.Common.DataColumnMapping("WiringHours", "WiringHours"), New System.Data.Common.DataColumnMapping("WiringRate", "WiringRate"), New System.Data.Common.DataColumnMapping("UserId", "UserId"), New System.Data.Common.DataColumnMapping("CurrencyDate", "CurrencyDate"), New System.Data.Common.DataColumnMapping("AccurideLocationId", "AccurideLocationId"), New System.Data.Common.DataColumnMapping("Abbreviation", "Abbreviation"), New System.Data.Common.DataColumnMapping("l_Description", "l_Description"), New System.Data.Common.DataColumnMapping("l_Tooling", "l_Tooling"), New System.Data.Common.DataColumnMapping("l_DieSetCost", "l_DieSetCost"), New System.Data.Common.DataColumnMapping("l_MaterialsCost", "l_MaterialsCost"), New System.Data.Common.DataColumnMapping("l_HardingCost", "l_HardingCost"), New System.Data.Common.DataColumnMapping("l_LabourHours", "l_LabourHours"), New System.Data.Common.DataColumnMapping("l_WiringHours", "l_WiringHours"), New System.Data.Common.DataColumnMapping("l_CNCMillHours", "l_CNCMillHours"), New System.Data.Common.DataColumnMapping("l_LabourRate", "l_LabourRate"), New System.Data.Common.DataColumnMapping("l_WiringRate", "l_WiringRate"), New System.Data.Common.DataColumnMapping("l_CNCMillRate", "l_CNCMillRate"), New System.Data.Common.DataColumnMapping("l_TotalToolCost", "l_TotalToolCost"), New System.Data.Common.DataColumnMapping("l_Footer", "l_Footer"), New System.Data.Common.DataColumnMapping("l_Header", "l_Header"), New System.Data.Common.DataColumnMapping("IsRH", "IsRH")})})
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "[ps_Select_Tooling]"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_ToolingId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"ToolingId",System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@LoadCombo", System.Data.SqlDbType.Bit, 1))
        '
        'SqlInsertCommand1
        '
        Me.SqlInsertCommand1.CommandText = "[ps_Insert_Tooling]"
        Me.SqlInsertCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlInsertCommand1.Connection = Me.SqlConnection1
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CNCMillHours", System.Data.SqlDbType.Decimal, 5, System.Data.ParameterDirection.Input, False, CType(6, Byte), CType(2, Byte), "CNCMillHours", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CNCMillRate", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, False, CType(19, Byte), CType(0, Byte), "CNCMillRate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.VarChar, 255,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"Description",System.Data.DataRowVersion.Current,Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@DieSetCost", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, False, CType(19, Byte), CType(0, Byte), "DieSetCost", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@HardeningCost", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, False, CType(19, Byte), CType(0, Byte), "HardeningCost", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@LabourHours", System.Data.SqlDbType.Decimal, 5, System.Data.ParameterDirection.Input, False, CType(6, Byte), CType(2, Byte), "LabourHours", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@LabourRate", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, False, CType(19, Byte), CType(0, Byte), "LabourRate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@MaterialsCost", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, False, CType(19, Byte), CType(0, Byte), "MaterialsCost", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ToolTypeId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "ToolTypeId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@WiringHours", System.Data.SqlDbType.Decimal, 5, System.Data.ParameterDirection.Input, False, CType(6, Byte), CType(2, Byte), "WiringHours", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@WiringRate", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, False, CType(19, Byte), CType(0, Byte), "WiringRate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"UserId",System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"AccurideLocationId",System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = "[ps_Update_Tooling]"
        Me.SqlUpdateCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlUpdateCommand1.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CNCMillHours", System.Data.SqlDbType.Decimal, 5, System.Data.ParameterDirection.Input, False, CType(6, Byte), CType(2, Byte), "CNCMillHours", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CNCMillRate", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, False, CType(19, Byte), CType(0, Byte), "CNCMillRate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.VarChar, 255,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"Description",System.Data.DataRowVersion.Current,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@DieSetCost", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, False, CType(19, Byte), CType(0, Byte), "DieSetCost", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@HardeningCost", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, False, CType(19, Byte), CType(0, Byte), "HardeningCost", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@LabourHours", System.Data.SqlDbType.Decimal, 5, System.Data.ParameterDirection.Input, False, CType(6, Byte), CType(2, Byte), "LabourHours", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@LabourRate", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, False, CType(19, Byte), CType(0, Byte), "LabourRate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@MaterialsCost", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, False, CType(19, Byte), CType(0, Byte), "MaterialsCost", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ToolTypeId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "ToolTypeId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@WiringHours", System.Data.SqlDbType.Decimal, 5, System.Data.ParameterDirection.Input, False, CType(6, Byte), CType(2, Byte), "WiringHours", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@WiringRate", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, False, CType(19, Byte), CType(0, Byte), "WiringRate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CurrencyDate", System.Data.SqlDbType.DateTime, 8,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"CurrencyDate",System.Data.DataRowVersion.Current,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CNCMillHours", System.Data.SqlDbType.Decimal, 5, System.Data.ParameterDirection.Input, True, CType(6, Byte), CType(2, Byte), "CNCMillHours", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CNCMillRate", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input,True, CType(19, Byte), CType(0, Byte),"CNCMillRate",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Description", System.Data.SqlDbType.VarChar, 255,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"Description",System.Data.DataRowVersion.Original,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_DieSetCost", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input,True, CType(19, Byte), CType(0, Byte),"DieSetCost",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_HardeningCost", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input,True, CType(19, Byte), CType(0, Byte),"HardeningCost",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_LabourHours", System.Data.SqlDbType.Decimal, 5, System.Data.ParameterDirection.Input, True, CType(6, Byte), CType(2, Byte), "LabourHours", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_LabourRate", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input,True, CType(19, Byte), CType(0, Byte),"LabourRate",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_MaterialsCost", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input,True, CType(19, Byte), CType(0, Byte),"MaterialsCost",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ToolTypeId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"ToolTypeId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_WiringHours", System.Data.SqlDbType.Decimal, 5, System.Data.ParameterDirection.Input, True, CType(6, Byte), CType(2, Byte), "WiringHours", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_WiringRate", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input,True, CType(19, Byte), CType(0, Byte),"WiringRate",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CurrencyDate", System.Data.SqlDbType.DateTime, 8,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"CurrencyDate",System.Data.DataRowVersion.Original,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_ToolingId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"ToolingId",System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"UserId",System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"AccurideLocationId",System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlDeleteCommand1
        '
        Me.SqlDeleteCommand1.CommandText = "[ps_Delete_Tooling]"
        Me.SqlDeleteCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlDeleteCommand1.Connection = Me.SqlConnection1
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CNCMillHours", System.Data.SqlDbType.Decimal, 5, System.Data.ParameterDirection.Input, True, CType(6, Byte), CType(2, Byte), "CNCMillHours", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CNCMillRate", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, True, CType(19, Byte), CType(0, Byte), "CNCMillRate", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Description", System.Data.SqlDbType.VarChar, 255, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Description", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_DieSetCost", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, True, CType(19, Byte), CType(0, Byte), "DieSetCost", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_HardeningCost", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, True, CType(19, Byte), CType(0, Byte), "HardeningCost", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_LabourHours", System.Data.SqlDbType.Decimal, 5, System.Data.ParameterDirection.Input, True, CType(6, Byte), CType(2, Byte), "LabourHours", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_LabourRate", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, True, CType(19, Byte), CType(0, Byte), "LabourRate", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_MaterialsCost", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, True, CType(19, Byte), CType(0, Byte), "MaterialsCost", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ToolTypeId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "ToolTypeId", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_WiringHours", System.Data.SqlDbType.Decimal, 5, System.Data.ParameterDirection.Input, True, CType(6, Byte), CType(2, Byte), "WiringHours", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_WiringRate", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, True, CType(19, Byte), CType(0, Byte), "WiringRate", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CurrencyDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "CurrencyDate", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_ToolingId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "ToolingId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"UserId",System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = strCONNECTIONSTRING
        '
        'SqlSelectCommand2
        '
        Me.SqlSelectCommand2.CommandText = "[ps_Report_ToolingDetails]"
        Me.SqlSelectCommand2.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand2.Connection = Me.SqlConnection1
        Me.SqlSelectCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CurrencyToId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))

    End Sub

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        'CODEGEN: This procedure is required by the Web Services Designer
        'Do not modify it using the code editor.
    End Sub

#End Region


    <System.Web.Services.WebMethodAttribute()> _
   Public Function LoadReportData(ByVal pProjectId As Int32, ByVal pUserID As Int32, ByVal pAccurideLocationId As Int32, ByVal pCurrencyToId As Int32) As Boolean
        Dim x As New DataSet()
        Dim blnThereIsData As Boolean

        SqlDataAdapter2.SelectCommand.Parameters("@AccurideLocationId").Value = pAccurideLocationId
        SqlDataAdapter2.SelectCommand.Parameters("@UserId").Value = pUserID
        SqlDataAdapter2.SelectCommand.Parameters("@ProjectId").Value = pProjectId
        SqlDataAdapter2.SelectCommand.Parameters("@CurrencyToId").Value = pCurrencyToId
        SqlDataAdapter2.Fill(x)
        If Not x Is Nothing Then
            If x.Tables.Count > 0 Then
                blnThereIsData = x.Tables(0).Rows.Count > 0
            End If
        End If
        Return blnThereIsData
    End Function

    '    <System.Web.Services.WebMethodAttribute()> _
    '    Public Function Test() As DataSet
    '        Return LoadData(New Object() {1, 0, 29, 46344})
    '    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadData(ByVal pudtParams() As System.Object) As System.Data.DataSet Implements IEPS_Service_Data.LoadData
        ' Dim x As New DataSet()
        SqlDataAdapter1.SelectCommand.Parameters("@AccurideLocationId").Value = CType(pudtParams(0), Int32)
        SqlDataAdapter1.SelectCommand.Parameters("@Select_ToolingId").Value = CType(pudtParams(1), Int32)
        SqlDataAdapter1.SelectCommand.Parameters("@UserId").Value = CType(pudtParams(2), Int32)
        SqlDataAdapter1.SelectCommand.Parameters("@ProjectId").Value = CType(pudtParams(3), Int32)
        If pudtParams.GetUpperBound(0) = 4 Then
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
    Public Function LoadCombo(ByVal pintUniqueId As Integer, ByVal pintAccurideLocationId As Integer) As System.Data.DataSet Implements IEPS_Service_Data.LoadCombo
        Return LoadData(New Object() {pintAccurideLocationId, pintUniqueId, 0, 0, 1})
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Sub DeleteTool(ByVal pintToolingId As Integer)
        Try
            SqlConnection1.Open()
            Dim udtSqlCommand As SqlCommand = New SqlCommand("ps_Delete_ToolingId", SqlConnection1)
            With udtSqlCommand
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@ToolId", pintToolingId)
                Try
                    .ExecuteNonQuery()
                    SqlConnection1.Close()
                Catch
                End Try
            End With
            udtSqlCommand = Nothing
        Catch
        End Try
    End Sub

    Public ReadOnly Property TableName() As String Implements IEPS_Service_Data.TableName
        Get
            Return "tTooling"
        End Get
    End Property

	Public Overrides Sub AssignFields()
		With Fields
			.Add("Description", New FieldInfoStructure("Description", 255, False, False))
			.Add("CNCMillHours", New FieldInfoStructure("CNCMillHours", 5, True, False, , 0))
			.Add("CurrencyDate", New FieldInfoStructure("CurrencyDate", 23, False, False, , "GETDATE"))
			.Add("CNCMillRate", New FieldInfoStructure("CNCMillRate", 19, True, False, , 26.79))
			.Add("DieSetCost", New FieldInfoStructure("DieSetCost", 19, True, False, , 0))
			.Add("HardeningCost", New FieldInfoStructure("HardeningCost", 19, True, False, , 0))
			.Add("LabourHours", New FieldInfoStructure("LabourHours", 5, True, False, , 0))
			.Add("LabourRate", New FieldInfoStructure("LabourRate", 19, True, False, , 26.79))
			.Add("MaterialsCost", New FieldInfoStructure("MaterialsCost", 19, True, False, , 0))
			.Add("ToolingId", New FieldInfoStructure("ToolingId", 10, False, False))
			.Add("ToolTypeId", New FieldInfoStructure("ToolTypeId", 10, False, False, , 1))
			.Add("TotalToolCost", New FieldInfoStructure("TotalToolCost", 19, True, True, "([LabourHours] * [LabourRate] + [WiringHours] * [WiringRate] + [CNCMillHours] * [CNCMillRate] + ([MaterialsCost] + [HardeningCost] + [DieSetCost]))"))
			.Add("WiringHours", New FieldInfoStructure("WiringHours", 5, True, False, , 0))
			.Add("WiringRate", New FieldInfoStructure("WiringRate", 19, True, False, , 26.79))
			.Add("Abbreviation", New FieldInfoStructure("Abbreviation", 10, True, False))
			.Add("IsRH", New FieldInfoStructure("IsRH", 1, False, False, , False))
		End With
	End Sub
End Class

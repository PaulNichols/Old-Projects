Imports System.Web.Services
Imports System.Data.SqlClient
<System.Web.Services.WebService(Namespace:="http://www.Accuride.com/PreSalesService")> _
Public Class PreSalesService
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
    Friend WithEvents SqlDataAdapter7 As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlDataAdapter9 As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlDataAdapter8 As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlDataAdapter14 As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlDataAdapter15 As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlDataAdapter2 As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlDataAdapter4 As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlDataAdapter5 As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents SqlSelectCommand2 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlInsertCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand3 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand5 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand6 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlInsertCommand2 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand2 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlDeleteCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand7 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand4 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand8 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand9 As System.Data.SqlClient.SqlCommand

    'Required by the Web Services Designer
    Private components As System.ComponentModel.Container

    'NOTE: The following procedure is required by the Web Services Designer
    'It can be modified using the Web Services Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.SqlDataAdapter14 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlDataAdapter15 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlDataAdapter2 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlDataAdapter1 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlDataAdapter7 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlDataAdapter5 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlDataAdapter4 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlDataAdapter9 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlDataAdapter8 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
        Me.SqlSelectCommand2 = New System.Data.SqlClient.SqlCommand()
        Me.SqlInsertCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlSelectCommand3 = New System.Data.SqlClient.SqlCommand()
        Me.SqlSelectCommand5 = New System.Data.SqlClient.SqlCommand()
        Me.SqlSelectCommand6 = New System.Data.SqlClient.SqlCommand()
        Me.SqlInsertCommand2 = New System.Data.SqlClient.SqlCommand()
        Me.SqlUpdateCommand2 = New System.Data.SqlClient.SqlCommand()
        Me.SqlDeleteCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlSelectCommand7 = New System.Data.SqlClient.SqlCommand()
        Me.SqlSelectCommand4 = New System.Data.SqlClient.SqlCommand()
        Me.SqlSelectCommand8 = New System.Data.SqlClient.SqlCommand()
        Me.SqlSelectCommand9 = New System.Data.SqlClient.SqlCommand()
        '
        'SqlDataAdapter14
        '
        Me.SqlDataAdapter14.SelectCommand = Me.SqlSelectCommand7
        Me.SqlDataAdapter14.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_CanMileStoneBeDeleted", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ID", "ID")}), New System.Data.Common.DataTableMapping("Table1", "Table1", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ID", "ID")})})
        '
        'SqlDataAdapter15
        '
        Me.SqlDataAdapter15.SelectCommand = Me.SqlSelectCommand1
        Me.SqlDataAdapter15.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "Select_ProjectPlanning", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("MileStoneDescription", "MileStoneDescription"), New System.Data.Common.DataColumnMapping("Owner", "Owner"), New System.Data.Common.DataColumnMapping("PlannedDate", "PlannedDate"), New System.Data.Common.DataColumnMapping("ActualDate", "ActualDate"), New System.Data.Common.DataColumnMapping("CustomerDate", "CustomerDate"), New System.Data.Common.DataColumnMapping("ActualHours", "ActualHours"), New System.Data.Common.DataColumnMapping("PlannedHours", "PlannedHours"), New System.Data.Common.DataColumnMapping("MileStoneId", "MileStoneId"), New System.Data.Common.DataColumnMapping("OwnerId", "OwnerId"), New System.Data.Common.DataColumnMapping("Qty", "Qty"), New System.Data.Common.DataColumnMapping("PreSalesId", "PreSalesId"), New System.Data.Common.DataColumnMapping("PreSalesMileStoneId", "PreSalesMileStoneId"), New System.Data.Common.DataColumnMapping("AccurideLocationId", "AccurideLocationId")}), New System.Data.Common.DataTableMapping("Table1", "Table1", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("MileStoneDescription", "MileStoneDescription"), New System.Data.Common.DataColumnMapping("Owner", "Owner"), New System.Data.Common.DataColumnMapping("PlannedDate", "PlannedDate"), New System.Data.Common.DataColumnMapping("ActualDate", "ActualDate"), New System.Data.Common.DataColumnMapping("CustomerDate", "CustomerDate"), New System.Data.Common.DataColumnMapping("ActualHours", "ActualHours"), New System.Data.Common.DataColumnMapping("PlannedHours", "PlannedHours"), New System.Data.Common.DataColumnMapping("MileStoneId", "MileStoneId"), New System.Data.Common.DataColumnMapping("OwnerId", "OwnerId"), New System.Data.Common.DataColumnMapping("Qty", "Qty"), New System.Data.Common.DataColumnMapping("PreSalesId", "PreSalesId"), New System.Data.Common.DataColumnMapping("PreSalesMileStoneId", "PreSalesMileStoneId"), New System.Data.Common.DataColumnMapping("AccurideLocationId", "AccurideLocationId")}), New System.Data.Common.DataTableMapping("Table2", "Table2", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("FullName", "FullName"), New System.Data.Common.DataColumnMapping("PlannedDate", "PlannedDate"), New System.Data.Common.DataColumnMapping("ActualDate", "ActualDate"), New System.Data.Common.DataColumnMapping("CustomerDate", "CustomerDate"), New System.Data.Common.DataColumnMapping("MacroMileStoneId", "MacroMileStoneId"), New System.Data.Common.DataColumnMapping("MileStoneId", "MileStoneId")}), New System.Data.Common.DataTableMapping("Table3", "Table3", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("UsedId", "UsedId"), New System.Data.Common.DataColumnMapping("FullName", "FullName")})})
        '
        'SqlDataAdapter2
        '
        Me.SqlDataAdapter2.SelectCommand = Me.SqlSelectCommand4
        Me.SqlDataAdapter2.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Select_ProjectPlanningItems", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("UniqueId", "UniqueId"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("Checked", "Checked"), New System.Data.Common.DataColumnMapping("ImportanceOrder", "ImportanceOrder"), New System.Data.Common.DataColumnMapping("Insert", "Insert"), New System.Data.Common.DataColumnMapping("Delete", "Delete"), New System.Data.Common.DataColumnMapping("PreSalesId", "PreSalesId")}), New System.Data.Common.DataTableMapping("Table1", "Table1", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("UniqueId", "UniqueId"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("Checked", "Checked"), New System.Data.Common.DataColumnMapping("MileStoneId", "MileStoneId"), New System.Data.Common.DataColumnMapping("PreSalesId", "PreSalesId"), New System.Data.Common.DataColumnMapping("Insert", "Insert"), New System.Data.Common.DataColumnMapping("Delete", "Delete")}), New System.Data.Common.DataTableMapping("Table2", "Table2", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("UniqueId", "UniqueId"), New System.Data.Common.DataColumnMapping("FullName", "FullName"), New System.Data.Common.DataColumnMapping("Checked", "Checked"), New System.Data.Common.DataColumnMapping("Insert", "Insert"), New System.Data.Common.DataColumnMapping("Delete", "Delete")})})
        '
        'SqlDataAdapter1
        '
        Me.SqlDataAdapter1.InsertCommand = Me.SqlInsertCommand1
        Me.SqlDataAdapter1.SelectCommand = Me.SqlSelectCommand2
        Me.SqlDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Select_PreSales", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("PreSalesId", "PreSalesId"), New System.Data.Common.DataColumnMapping("CapitalExpenditureRequired", "CapitalExpenditureRequired"), New System.Data.Common.DataColumnMapping("DesignBriefRequired", "DesignBriefRequired"), New System.Data.Common.DataColumnMapping("ProjectLearderId", "ProjectLearderId"), New System.Data.Common.DataColumnMapping("SpecialInstructions", "SpecialInstructions"), New System.Data.Common.DataColumnMapping("PreSalesTypeId", "PreSalesTypeId"), New System.Data.Common.DataColumnMapping("FitBusinessStrategy", "FitBusinessStrategy"), New System.Data.Common.DataColumnMapping("FitBusinessStrategyNote", "FitBusinessStrategyNote"), New System.Data.Common.DataColumnMapping("InitialInfoReqYesDate", "InitialInfoReqYesDate"), New System.Data.Common.DataColumnMapping("ProjectId", "ProjectId"), New System.Data.Common.DataColumnMapping("InitialInfoReqNoDate", "InitialInfoReqNoDate"), New System.Data.Common.DataColumnMapping("AccurideLocationId", "AccurideLocationId"), New System.Data.Common.DataColumnMapping("ProjectId1", "ProjectId1"), New System.Data.Common.DataColumnMapping("templateid", "templateid")})})
        Me.SqlDataAdapter1.UpdateCommand = Me.SqlUpdateCommand1
        '
        'SqlDataAdapter7
        '
        Me.SqlDataAdapter7.DeleteCommand = Me.SqlDeleteCommand1
        Me.SqlDataAdapter7.InsertCommand = Me.SqlInsertCommand2
        Me.SqlDataAdapter7.SelectCommand = Me.SqlSelectCommand6
        Me.SqlDataAdapter7.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Select_PreSalesISOStandards", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ISOStandardId", "ISOStandardId"), New System.Data.Common.DataColumnMapping("Standard", "Standard"), New System.Data.Common.DataColumnMapping("Checked", "Checked")}), New System.Data.Common.DataTableMapping("Table1", "Table1", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ISOStandardId", "ISOStandardId"), New System.Data.Common.DataColumnMapping("Standard", "Standard"), New System.Data.Common.DataColumnMapping("Checked", "Checked")}), New System.Data.Common.DataTableMapping("Table2", "Table2", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember"), New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember"), New System.Data.Common.DataColumnMapping("Checked", "Checked")}), New System.Data.Common.DataTableMapping("Table3", "Table3", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember"), New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember"), New System.Data.Common.DataColumnMapping("Checked", "Checked")})})
        Me.SqlDataAdapter7.UpdateCommand = Me.SqlUpdateCommand2
        '
        'SqlDataAdapter5
        '
        Me.SqlDataAdapter5.SelectCommand = Me.SqlSelectCommand9
        Me.SqlDataAdapter5.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_CanTeamMemberBeDeleted", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("PreSalesId", "PreSalesId")})})
        '
        'SqlDataAdapter4
        '
        Me.SqlDataAdapter4.SelectCommand = Me.SqlSelectCommand8
        Me.SqlDataAdapter4.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_CanMacroMileStoneBeDeleted", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Id", "Id")}), New System.Data.Common.DataTableMapping("Table1", "Table1", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Id", "Id")})})
        '
        'SqlDataAdapter9
        '
        Me.SqlDataAdapter9.SelectCommand = Me.SqlSelectCommand5
        Me.SqlDataAdapter9.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Select_PreSalesProjectTeam", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("UniqueId", "UniqueId"), New System.Data.Common.DataColumnMapping("FullName", "FullName"), New System.Data.Common.DataColumnMapping("Checked", "Checked"), New System.Data.Common.DataColumnMapping("Insert", "Insert"), New System.Data.Common.DataColumnMapping("Delete", "Delete")})})
        '
        'SqlDataAdapter8
        '
        Me.SqlDataAdapter8.SelectCommand = Me.SqlSelectCommand3
        Me.SqlDataAdapter8.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Select_PreSalesTemplates", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember"), New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember")})})
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "Select_ProjectPlanning"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = strConnectionString
        '
        'SqlSelectCommand2
        '
        Me.SqlSelectCommand2.CommandText = "ps_Select_PreSales"
        Me.SqlSelectCommand2.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand2.Connection = Me.SqlConnection1
        Me.SqlSelectCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlInsertCommand1
        '
        Me.SqlInsertCommand1.CommandText = "ps_Insert_PreSales"
        Me.SqlInsertCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlInsertCommand1.Connection = Me.SqlConnection1
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CapitalExpenditureRequired", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "CapitalExpenditureRequired", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@DesignBriefRequired", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "DesignBriefRequired", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@InitialInfoReq", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@InitialInfoReqDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PreSalesTypeId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "PreSalesTypeId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "ProjectId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectLearderId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "ProjectLearderId", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = "ps_Update_PreSales"
        Me.SqlUpdateCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlUpdateCommand1.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CapitalExpenditureRequired", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "CapitalExpenditureRequired", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@DesignBriefRequired", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "DesignBriefRequired", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@InitialInfoReqYesDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "InitialInfoReqYesDate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@InitialInfoReqNoDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "InitialInfoReqNoDate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@FitBusinessStrategy", System.Data.SqlDbType.SmallInt, 2, System.Data.ParameterDirection.Input, True, CType(5, Byte), CType(0, Byte), "FitBusinessStrategy", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@FitBusinessStrategyNote", System.Data.SqlDbType.VarChar, 1000, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "FitBusinessStrategyNote", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PreSalesTypeId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "PreSalesTypeId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@SpecialInstructions", System.Data.SqlDbType.VarChar, 8000, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "SpecialInstructions", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "ProjectId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectLearderId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "ProjectLearderId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CapitalExpenditureRequired", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"CapitalExpenditureRequired",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_DesignBriefRequired", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"DesignBriefRequired",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_InitialInfoReqYesDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"InitialInfoReqYesDate",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_InitialInfoReqNoDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"InitialInfoReqNoDate",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_FitBusinessStrategy", System.Data.SqlDbType.SmallInt, 2, System.Data.ParameterDirection.Input,True, CType(5, Byte), CType(0, Byte),"FitBusinessStrategy",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_FitBusinessStrategyNote", System.Data.SqlDbType.VarChar, 1000, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "FitBusinessStrategyNote", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_PreSalesTypeId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"PreSalesTypeId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ProjectId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"ProjectId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ProjectLearderId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"ProjectLearderId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_SpecialInstructions", System.Data.SqlDbType.VarChar, 8000, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "SpecialInstructions", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_AccurideLocationID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"AccurideLocationID",System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Templateid", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "Templateid", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Templateid", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"Templateid",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_PreSalesId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"PreSalesId",System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlSelectCommand3
        '
        Me.SqlSelectCommand3.CommandText = "ps_Select_PreSalesTemplates"
        Me.SqlSelectCommand3.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand3.Connection = Me.SqlConnection1
        Me.SqlSelectCommand3.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand3.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlSelectCommand5
        '
        Me.SqlSelectCommand5.CommandText = "ps_Select_PreSalesProjectTeam"
        Me.SqlSelectCommand5.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand5.Connection = Me.SqlConnection1
        Me.SqlSelectCommand5.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand5.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PreSalesId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand5.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlSelectCommand6
        '
        Me.SqlSelectCommand6.CommandText = "ps_Select_PreSalesISOStandards"
        Me.SqlSelectCommand6.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand6.Connection = Me.SqlConnection1
        Me.SqlSelectCommand6.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand6.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PreSalesId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand6.Parameters.Add(New System.Data.SqlClient.SqlParameter("@LoadCombo", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlInsertCommand2
        '
        Me.SqlInsertCommand2.CommandText = "ps_Insert_ISOStandard"
        Me.SqlInsertCommand2.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlInsertCommand2.Connection = Me.SqlConnection1
        Me.SqlInsertCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Standard", System.Data.SqlDbType.Char, 50, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Standard", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlUpdateCommand2
        '
        Me.SqlUpdateCommand2.CommandText = "ps_Update_ISOStandard"
        Me.SqlUpdateCommand2.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlUpdateCommand2.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Standard", System.Data.SqlDbType.Char, 50, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Standard", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Standard", System.Data.SqlDbType.Char, 50, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"Standard",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_ISOStandardId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"ISOStandardId",System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlDeleteCommand1
        '
        Me.SqlDeleteCommand1.CommandText = "ps_Delete_ISOStandard"
        Me.SqlDeleteCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlDeleteCommand1.Connection = Me.SqlConnection1
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Standard", System.Data.SqlDbType.Char, 50, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Standard", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Standard", System.Data.SqlDbType.Char, 50, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"Standard",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_ISOStandardId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"ISOStandardId",System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlSelectCommand7
        '
        Me.SqlSelectCommand7.CommandText = "ps_CanMileStoneBeDeleted"
        Me.SqlSelectCommand7.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand7.Connection = Me.SqlConnection1
        Me.SqlSelectCommand7.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand7.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PreSalesId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand7.Parameters.Add(New System.Data.SqlClient.SqlParameter("@MileStoneId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlSelectCommand4
        '
        Me.SqlSelectCommand4.CommandText = "ps_Select_ProjectPlanningItems"
        Me.SqlSelectCommand4.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand4.Connection = Me.SqlConnection1
        Me.SqlSelectCommand4.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand4.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand4.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlSelectCommand8
        '
        Me.SqlSelectCommand8.CommandText = "ps_CanMacroMileStoneBeDeleted"
        Me.SqlSelectCommand8.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand8.Connection = Me.SqlConnection1
        Me.SqlSelectCommand8.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand8.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PreSalesId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand8.Parameters.Add(New System.Data.SqlClient.SqlParameter("@MacroMileStoneId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlSelectCommand9
        '
        Me.SqlSelectCommand9.CommandText = "ps_CanTeamMemberBeDeleted"
        Me.SqlSelectCommand9.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand9.Connection = Me.SqlConnection1
        Me.SqlSelectCommand9.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand9.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PreSalesId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand9.Parameters.Add(New System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))

    End Sub

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        'CODEGEN: This procedure is required by the Web Services Designer
        'Do not modify it using the code editor.
    End Sub

#End Region

    Public ReadOnly Property TableName() As String Implements IEPS_Service_Data.TableName
        Get
            Return "tPreSales"
        End Get
    End Property

    <WebMethod()> Public Function GetPlannerDetails(ByVal pintProjectId As Int32, ByVal pintAccurideLocationId As Int32) As DataSet
        Dim x As New DataSet()
        Dim SqlCommand1 As New System.Data.SqlClient.SqlCommand()
        With SqlCommand1
            .CommandText = "PlannerDetails"
            .CommandType = System.Data.CommandType.StoredProcedure
            .Connection = Me.SqlConnection1
            .Parameters.Add("@ProjectId", pintProjectId)
            .Parameters.Add("@AccurideLocationId", pintAccurideLocationId)
            Try
                .Connection.Open()
                Dim udtDA As New SqlDataAdapter(SqlCommand1)
                udtDA.Fill(x)
                If Not x Is Nothing Then
                    x.Tables(0).TableName = "Parent"
                    x.Tables(1).TableName = "Child"
                    x.Tables(2).TableName = "WeekdayHours"
                    Dim udtRelation As New DataRelation("Join", x.Tables("Parent").Columns("TaskId"), x.Tables("Child").Columns("TaskId"))
                    x.Relations.Add(udtRelation)
                    udtRelation = Nothing
                End If
                Return x
            Catch
                Return Nothing
            End Try
        End With
    End Function

    <WebMethod()> Public Function ApplyTemplateSettings(ByVal pintTemplateId As Int32, ByVal pintOwnerId As Int32, ByVal pintPreSalesId As Int32) As Boolean
        Dim SqlCommand1 As New System.Data.SqlClient.SqlCommand()
        With SqlCommand1
            .CommandText = "ps_ApplyTemplateSettings"
            .CommandType = System.Data.CommandType.StoredProcedure
            .Connection = SqlConnection1
            .Parameters.Add("@PreSalesId", pintPreSalesId)
            .Parameters.Add("@OwnerId", pintOwnerId)
            .Parameters.Add("@TemplateId", pintTemplateId)
            .Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
            Try
                .Connection.Open()
                .ExecuteNonQuery()
                Return (CType(.Parameters("@RETURN_VALUE").Value.ToString, Int32) = 1)
            Catch
                Return False
            End Try
        End With
    End Function

    <WebMethod()> Public Function GetProjectPlanning(ByVal pintProjectId As Int32, ByVal pintAccurideLocationid As Int32) As DataSet
        'can milestones be deleted
        Dim x As New DataSet()
        SqlDataAdapter15.SelectCommand.Parameters("@ProjectId").Value = pintProjectId
        SqlDataAdapter15.SelectCommand.Parameters("@AccurideLocationId").Value = pintAccurideLocationid
        SqlDataAdapter15.Fill(x)
        Return x
    End Function

    <WebMethod()> Public Function CanMileStoneBeDeleted(ByVal pintPreSalesid As Int32, ByVal pintMilestoneid As Int32) As Boolean
        'can milestones be deleted
        Dim x As New DataSet()
        SqlDataAdapter14.SelectCommand.Parameters("@MileStoneId").Value = pintMilestoneid
        SqlDataAdapter14.SelectCommand.Parameters("@PreSalesId").Value = pintPreSalesid
        SqlDataAdapter14.Fill(x)
        If Not x Is Nothing Then
            If x.Tables.Count > 0 Then
                If x.Tables(0).Rows.Count > 0 Then
                    Return False
                End If
            End If
        End If
        Return True
    End Function

    <WebMethod()> Public Function CanMacroMileStoneBeDeleted(ByVal pintPreSalesid As Int32, ByVal pintMacroMilestoneid As Int32) As Boolean
        'can milestones be deleted
        Dim x As New DataSet()
        SqlDataAdapter4.SelectCommand.Parameters("@MacroMileStoneId").Value = pintMacroMilestoneid
        SqlDataAdapter4.SelectCommand.Parameters("@PreSalesId").Value = pintPreSalesid
        SqlDataAdapter4.Fill(x)
        If Not x Is Nothing Then
            If x.Tables.Count > 0 Then
                If x.Tables(0).Rows.Count > 0 Then
                    Return False
                End If
            End If
        End If
        Return True
    End Function

    <WebMethod()> Public Function CanTeamMemberBeDeleted(ByVal pintPreSalesid As Int32, ByVal pintUserid As Int32) As Boolean
        'can milestones be deleted
        Dim x As New DataSet()
        SqlDataAdapter5.SelectCommand.Parameters("@UserId").Value = pintUserid
        SqlDataAdapter5.SelectCommand.Parameters("@PreSalesId").Value = pintPreSalesid
        SqlDataAdapter5.Fill(x)
        If Not x Is Nothing Then
            If x.Tables.Count > 0 Then
                If x.Tables(0).Rows.Count > 0 Then
                    Return False
                End If
            End If
        End If
        Return True
    End Function

    <WebMethod()> Public Function CanTemplateBeApplied(ByVal pintPreSalesId As Int32) As Boolean
        Dim x As New DataSet()
        Dim SqlCommand1 As New System.Data.SqlClient.SqlCommand()
        With SqlCommand1
            .CommandText = "ps_CanTemplateBeApplied"
            .CommandType = System.Data.CommandType.StoredProcedure
            .Connection = New SqlClient.SqlConnection(strConnectionString)
            .Parameters.Add("@PreSalesId", pintPreSalesId)
            .Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
            Try
                .Connection.Open()
                .ExecuteNonQuery()
                Return (CType(.Parameters("@RETURN_VALUE").Value.ToString, Int32) = 1)
            Catch
                Return False
            End Try
        End With
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadData(ByVal pudtParams() As System.Object) As System.Data.DataSet Implements IEPS_Service_Data.LoadData
        SqlDataAdapter1.SelectCommand.Parameters("@Projectid").Value = pudtParams(0)
        SqlDataAdapter1.SelectCommand.Parameters("@AccurideLocationId").Value = pudtParams(1)
        Return LoadDataSet(SqlDataAdapter1)
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function UpdateData(ByVal Changes As System.Data.DataSet) As System.Data.DataSet Implements IEPS_Service_Data.UpdateData
        Return UpdateDataset(Changes, SqlDataAdapter1)
    End Function



    <WebMethod()> Public Function GetPreSalesISOStandards(ByVal pintPreSalesId As Int32) As DataSet
        Dim x As New DataSet()
        SqlDataAdapter7.SelectCommand.Parameters("@PreSalesId").Value = pintPreSalesId
        SqlDataAdapter7.SelectCommand.Parameters("@LoadCombo").Value = 1
        SqlDataAdapter7.Fill(x)
        Return x
    End Function

    <WebMethod()> Public Function UpdateISOStandards(ByVal ISOChanges As DataSet) As DataSet
        If Not (ISOChanges Is Nothing) Then
            Return UpdateDataset(ISOChanges, SqlDataAdapter7)
        Else
            Return Nothing
        End If
    End Function



    <WebMethod()> Public Function UpdatePreSalesISOStandards(ByVal pintPreSaleId As Int32, ByVal pstrStandardIDs As String, ByVal pintDelete As Byte) As Boolean
        Dim SqlCommand1 As New System.Data.SqlClient.SqlCommand()

        If pstrStandardIDs.EndsWith(",") Then
            pstrStandardIDs = pstrStandardIDs.Substring(0, pstrStandardIDs.Length - 1)
        End If
        With SqlCommand1
            If pstrStandardIDs.Length = 0 Then
                .CommandText = "delete from tPreSalesISOStandards where presalesid=" & pintPreSaleId
            Else
                .CommandText = "delete from tPreSalesISOStandards where presalesid=" & pintPreSaleId & " insert into tPreSalesISOStandards ([PreSalesId], [ISOStandardId]) Select " & pintPreSaleId & ",ISOStandardId  from tISOStandard where ISOStandardId in (" & pstrStandardIDs & ")"
            End If
            .CommandType = CommandType.Text
            .Connection = New SqlClient.SqlConnection(strConnectionString)
            Try
                .Connection.Open()
                Return (.ExecuteNonQuery > 0 Or pstrStandardIDs.Length = 0)
            Catch
                Return False
            End Try
        End With

    End Function


    <WebMethod()> Public Function GetPreSalesTemplates(ByVal pintAccurideLocationId As Int32) As DataSet
        Dim x As New DataSet()
        SqlDataAdapter8.SelectCommand.Parameters("@AccurideLocationId").Value = pintAccurideLocationId
        SqlDataAdapter8.Fill(x)
        Return x
    End Function


    <WebMethod()> Public Function GetProjectPlanningItems(ByVal pintProjectId As Int32, ByVal pintAccurideLocationId As Int32) As DataSet
        Dim x As New DataSet()
        SqlDataAdapter2.SelectCommand.Parameters("@ProjectId").Value = pintProjectId
        SqlDataAdapter2.SelectCommand.Parameters("@AccurideLocationId").Value = pintAccurideLocationId
        SqlDataAdapter2.Fill(x)
        Return x
    End Function

    <WebMethod()> Public Function GetPreSalesProjectTeam(ByVal pintPreSalesId As Int32, ByVal bAllUsers As Byte) As DataSet
        Dim x As New DataSet()
        SqlDataAdapter9.SelectCommand.Parameters("@PreSalesId").Value = pintPreSalesId
        SqlDataAdapter9.SelectCommand.Parameters("@bGetAllUsers").Value = bAllUsers
        SqlDataAdapter9.Fill(x)
        Return x
    End Function


    ''''45849
    '<WebMethod()> _
    '      Public Function RUNTEST() As Boolean
    '    Dim x As DataSet = GetProjectPlanningItems(46800, 1)
    '    x.Tables(0).Rows(0).Item("Insert") = "1"
    '    x.Tables(1).Rows(0).Item("Insert") = "1"
    '    x.Tables(2).Rows(0).Item("Insert") = "1"


    '    Return UpdatePreSalesProjectItems(x.GetChanges, 2279)
    'End Function

    <WebMethod()> Public Function UpdatePreSalesProjectItems(ByVal objDs As DataSet, ByVal pintPreSalesId As Int32) As Boolean
        Dim strInsert As String = ""
        Dim strDelete As String = ""
        Dim blnFailed As Boolean = False
        Dim SqlCommand As New System.Data.SqlClient.SqlCommand()
        Dim objConnection As New SqlClient.SqlConnection(strConnectionString)
        Dim objRow As DataRow
        Dim objDT As DataTable
        Dim intTableIdentifier As Int32 = 0

        Try
            objConnection.Open()
        Catch
            Return False
        End Try

        '  objConnection.BeginTransaction()


        With SqlCommand
            '.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
            .CommandType = System.Data.CommandType.Text
            .Connection = objConnection

        End With
        For Each objDT In objDs.Tables
            strInsert = ""
            strDelete = ""
            For Each objRow In objDT.Rows
                With objRow
                    If .Item("Insert").ToString = "1" Then
                        strInsert &= .Item("UniqueId").ToString & ","
                    ElseIf .Item("Delete").ToString = "1" Then
                        strDelete &= .Item("UniqueId").ToString & ","
                    End If
                End With
            Next objRow

            If strInsert.Length > 0 Then
                strInsert = strInsert.Substring(0, strInsert.Length - 1)
            End If
            If strDelete.Length > 0 Then
                strDelete = strDelete.Substring(0, strDelete.Length - 1)
            End If

            Select Case intTableIdentifier
                Case 0
                    If strInsert.Length > 0 Then
                        blnFailed = Not ExecutePresSalesCommand(SqlCommand, "INSERT INTO tPreSalesMileStone(PreSalesId,MileStoneId) select " & pintPreSalesId & ",MilestoneId from tMileStone where  MilestoneId in (" & strInsert & ")")
                    End If
                    If strDelete.Length > 0 Then
                        blnFailed = Not ExecutePresSalesCommand(SqlCommand, "Delete FROM tPreSalesMileStone WHERE [PreSalesId]=" & pintPreSalesId & " And [MileStoneId] in (" & strDelete & ")")
                    End If
                Case 1
                    If strInsert.Length > 0 Then
                        blnFailed = Not ExecutePresSalesCommand(SqlCommand, "INSERT INTO tPreSalesMacroMileStones(PreSalesId,MacroMileStoneId) select " & pintPreSalesId & ",MacroMilestoneId from tMacroMileStone where MacroMilestoneId in (" & strInsert & ")")
                    End If
                    If strDelete.Length > 0 Then
                        blnFailed = Not ExecutePresSalesCommand(SqlCommand, "Delete FROM tPreSalesMacroMileStones WHERE [PreSalesId]=" & pintPreSalesId & " And [MacroMilestoneId] in (" & strDelete & ")")
                    End If
                Case 2
                    If strInsert.Length > 0 Then
                        blnFailed = Not ExecutePresSalesCommand(SqlCommand, "INSERT INTO tPreSalesProjectTeam(PreSalesId,UsedId) select " & pintPreSalesId & ",UserId from tUser where  UserId in (" & strInsert & ")")
                    End If
                    If strDelete.Length > 0 Then
                        blnFailed = Not ExecutePresSalesCommand(SqlCommand, "Delete FROM tPreSalesProjectTeam WHERE [PreSalesId]=" & pintPreSalesId & " And [UsedId] in (" & strDelete & ")")
                    End If
            End Select
            intTableIdentifier += 1
        Next objDT

        ' SqlCommand.Transaction.Commit()
        SqlCommand.Connection.Close()

        SqlCommand = Nothing
        objConnection = Nothing
        Return Not blnFailed
    End Function

    Private Function ExecutePresSalesCommand(ByVal SqlCommand As System.Data.SqlClient.SqlCommand, ByVal CommandText As String) As Boolean
        With SqlCommand
            .CommandText = CommandText
            Try
                .ExecuteNonQuery()
                Return True
            Catch e As System.Exception
                Return False
            End Try
        End With
    End Function

    <System.Web.Services.WebMethodAttribute()> Public Function LoadCombo(ByVal pintUniqueId As Int32, ByVal pintAccurideLocationId As Int32) As System.Data.DataSet Implements IEPS_Service_Data.LoadCombo
        Return Nothing
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function GetPreSalesId(ByVal ProjectId As Int32) As Int32
        Dim SqlCommand1 As New System.Data.SqlClient.SqlCommand()
        With SqlCommand1
            .CommandText = "ps_GetPreSalesId"
            .CommandType = System.Data.CommandType.StoredProcedure
            .Connection = New SqlClient.SqlConnection(strConnectionString)
            .Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
            .Parameters.Add("@ProjectId", ProjectId)
            Try
                .Connection.Open()
                .ExecuteNonQuery()
                Return CInt(.Parameters("@RETURN_VALUE").Value)
                Return 1
            Catch
                Return -1
            End Try
        End With
    End Function




    Public Overrides Sub AssignFields()
        With Fields
            .Add("PreSalesId", New FieldInfoStructure("PreSalesId", 10, False, False))
            .Add("CapitalExpenditureRequired", New FieldInfoStructure("CapitalExpenditureRequired", 1, False, False, , False))
            .Add("DesignBriefRequired", New FieldInfoStructure("DesignBriefRequired", 1, False, False, , False))
            .Add("ProjectLearderId", New FieldInfoStructure("ProjectLearderId", 10, False, False))
            .Add("SpecialInstructions", New FieldInfoStructure("SpecialInstructions", 8000, True, False))
            .Add("PreSalesTypeId", New FieldInfoStructure("PreSalesTypeId", 10, False, False))
            .Add("FitBusinessStrategy", New FieldInfoStructure("FitBusinessStrategy", 5, False, False, , 0))
            .Add("FitBusinessStrategyNote", New FieldInfoStructure("FitBusinessStrategyNote", 1000, True, False))
            .Add("InitialInfoReqYesDate", New FieldInfoStructure("InitialInfoReqYesDate", 23, True, False))
            .Add("ProjectId", New FieldInfoStructure("ProjectId", 10, True, False))
            .Add("InitialInfoReqNoDate", New FieldInfoStructure("InitialInfoReqNoDate", 23, True, False))
            .Add("TemplateId", New FieldInfoStructure("TemplateId", 10, False, False, , 1))
            .Add("FullName", New FieldInfoStructure("FullName", 50, True, False))
            .Add("ValueMember", New FieldInfoStructure("ValueMember", 10, False, False))
            .Add("UniqueId", New FieldInfoStructure("UniqueId", 10, False, False))
            .Add("ISOStandardId", New FieldInfoStructure("ISOStandardId", 10, False, False))
            .Add("Standard", New FieldInfoStructure("Standard", 50, False, False))
            .Add("ID", New FieldInfoStructure("ID", 10, False, False))
            .Add("Task", New FieldInfoStructure("Task", 250, False, False))
            .Add("TaskId", New FieldInfoStructure("TaskId", 10, False, False))
            .Add("PreSalesMacroMileStonesId", New FieldInfoStructure("PreSalesMacroMileStonesId", 10, False, False))
        End With
    End Sub
End Class

Imports System.Web.Services
Imports System.Data.SqlClient
<System.Web.Services.WebService(Namespace:="http://www.Accuride.com/ProjectAdminService")> _
Public Class ProjectAdminService
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
        Me.SqlDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Select_ProjectAdmin", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ProjectAdminId", "ProjectAdminId"), New System.Data.Common.DataColumnMapping("AnticipatedFirstProductionDate", "AnticipatedFirstProductionDate"), New System.Data.Common.DataColumnMapping("DrawingsApprovedDate", "DrawingsApprovedDate"), New System.Data.Common.DataColumnMapping("DrawingsApprovedIssue", "DrawingsApprovedIssue"), New System.Data.Common.DataColumnMapping("FirstOrderSEID", "FirstOrderSEID"), New System.Data.Common.DataColumnMapping("PhasedQ1", "PhasedQ1"), New System.Data.Common.DataColumnMapping("PhasedQ2", "PhasedQ2"), New System.Data.Common.DataColumnMapping("PhasedQ3", "PhasedQ3"), New System.Data.Common.DataColumnMapping("PhasedQ4", "PhasedQ4"), New System.Data.Common.DataColumnMapping("PartsAvailableDate", "PartsAvailableDate"), New System.Data.Common.DataColumnMapping("SamplesApprovedDate", "SamplesApprovedDate"), New System.Data.Common.DataColumnMapping("SamplesApprovedIssue", "SamplesApprovedIssue"), New System.Data.Common.DataColumnMapping("ToolingCompletionDate", "ToolingCompletionDate"), New System.Data.Common.DataColumnMapping("ActuallFirstProductionDate", "ActuallFirstProductionDate"), New System.Data.Common.DataColumnMapping("CompanyName", "CompanyName"), New System.Data.Common.DataColumnMapping("ProjectMasterGroup", "ProjectMasterGroup"), New System.Data.Common.DataColumnMapping("Issue", "Issue"), New System.Data.Common.DataColumnMapping("BatchSize", "BatchSize"), New System.Data.Common.DataColumnMapping("AnnualQuantity", "AnnualQuantity"), New System.Data.Common.DataColumnMapping("UserId", "UserId"), New System.Data.Common.DataColumnMapping("AccuridePartNumberIssue", "AccuridePartNumberIssue"), New System.Data.Common.DataColumnMapping("AccuridePartNumber", "AccuridePartNumber"), New System.Data.Common.DataColumnMapping("Director", "Director")}), New System.Data.Common.DataTableMapping("Table1", "Table1", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("NoteDate", "NoteDate"), New System.Data.Common.DataColumnMapping("Important", "Important"), New System.Data.Common.DataColumnMapping("FullName", "FullName")})})
        Me.SqlDataAdapter1.UpdateCommand = Me.SqlUpdateCommand1
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "ps_Select_ProjectAdmin"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@projectId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlInsertCommand1
        '
        Me.SqlInsertCommand1.CommandText = "ps_Insert_ProjectAdmin"
        Me.SqlInsertCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlInsertCommand1.Connection = Me.SqlConnection1
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ActuallFirstProductionDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "ActuallFirstProductionDate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AnticipatedFirstProductionDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "AnticipatedFirstProductionDate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@DrawingsApprovedDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "DrawingsApprovedDate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@DrawingsApprovedIssue", System.Data.SqlDbType.Char, 1, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "DrawingsApprovedIssue", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@FirstOrderPrice", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, True, CType(19, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@FirstOrderSEID", System.Data.SqlDbType.Char, 10, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "FirstOrderSEID", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PartsAvailableDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "PartsAvailableDate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PhasedQ1", System.Data.SqlDbType.SmallInt, 2, System.Data.ParameterDirection.Input, True, CType(5, Byte), CType(0, Byte), "PhasedQ1", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PhasedQ2", System.Data.SqlDbType.SmallInt, 2, System.Data.ParameterDirection.Input, True, CType(5, Byte), CType(0, Byte), "PhasedQ2", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PhasedQ3", System.Data.SqlDbType.SmallInt, 2, System.Data.ParameterDirection.Input, True, CType(5, Byte), CType(0, Byte), "PhasedQ3", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PhasedQ4", System.Data.SqlDbType.SmallInt, 2, System.Data.ParameterDirection.Input, True, CType(5, Byte), CType(0, Byte), "PhasedQ4", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@SamplesApprovedDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "SamplesApprovedDate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@SamplesApprovedIssue", System.Data.SqlDbType.Char, 1, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "SamplesApprovedIssue", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ToolingCompletionDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "ToolingCompletionDate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"UserId",System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_CurrencyDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"CurrencyDate",System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = "ps_Update_ProjectAdmin"
        Me.SqlUpdateCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlUpdateCommand1.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ActuallFirstProductionDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "ActuallFirstProductionDate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AnticipatedFirstProductionDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "AnticipatedFirstProductionDate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@DrawingsApprovedDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "DrawingsApprovedDate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@DrawingsApprovedIssue", System.Data.SqlDbType.Char, 1, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "DrawingsApprovedIssue", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@FirstOrderPrice", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, True, CType(19, Byte), CType(0, Byte), "FirstOrderPrice", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@FirstOrderSEID", System.Data.SqlDbType.Char, 10, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "FirstOrderSEID", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PartsAvailableDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "PartsAvailableDate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PhasedQ1", System.Data.SqlDbType.SmallInt, 2, System.Data.ParameterDirection.Input, True, CType(5, Byte), CType(0, Byte), "PhasedQ1", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PhasedQ2", System.Data.SqlDbType.SmallInt, 2, System.Data.ParameterDirection.Input, True, CType(5, Byte), CType(0, Byte), "PhasedQ2", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PhasedQ3", System.Data.SqlDbType.SmallInt, 2, System.Data.ParameterDirection.Input, True, CType(5, Byte), CType(0, Byte), "PhasedQ3", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PhasedQ4", System.Data.SqlDbType.SmallInt, 2, System.Data.ParameterDirection.Input, True, CType(5, Byte), CType(0, Byte), "PhasedQ4", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "ProjectID", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@SamplesApprovedDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "SamplesApprovedDate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@SamplesApprovedIssue", System.Data.SqlDbType.Char, 1, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "SamplesApprovedIssue", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ToolingCompletionDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "ToolingCompletionDate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ActuallFirstProductionDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"ActuallFirstProductionDate",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_AnticipatedFirstProductionDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"AnticipatedFirstProductionDate",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_DrawingsApprovedDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"DrawingsApprovedDate",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_DrawingsApprovedIssue", System.Data.SqlDbType.Char, 1, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"DrawingsApprovedIssue",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_FirstOrderPrice", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input,True, CType(19, Byte), CType(0, Byte),"FirstOrderPrice",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_FirstOrderSEID", System.Data.SqlDbType.Char, 10, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"FirstOrderSEID",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_PartsAvailableDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"PartsAvailableDate",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_PhasedQ1", System.Data.SqlDbType.SmallInt, 2, System.Data.ParameterDirection.Input,True, CType(5, Byte), CType(0, Byte),"PhasedQ1",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_PhasedQ2", System.Data.SqlDbType.SmallInt, 2, System.Data.ParameterDirection.Input,True, CType(5, Byte), CType(0, Byte),"PhasedQ2",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_PhasedQ3", System.Data.SqlDbType.SmallInt, 2, System.Data.ParameterDirection.Input,True, CType(5, Byte), CType(0, Byte),"PhasedQ3",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_PhasedQ4", System.Data.SqlDbType.SmallInt, 2, System.Data.ParameterDirection.Input,True, CType(5, Byte), CType(0, Byte),"PhasedQ4",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ProjectID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"ProjectID",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_SamplesApprovedDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"SamplesApprovedDate",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_SamplesApprovedIssue", System.Data.SqlDbType.Char, 1, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"SamplesApprovedIssue",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ToolingCompletionDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"ToolingCompletionDate",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_ProjectAdminId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"ProjectAdminId",System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"UserId",System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_CurrencyDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"CurrencyDate",System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlDeleteCommand1
        '
        Me.SqlDeleteCommand1.CommandText = "ps_Delete_ProjectAdmin"
        Me.SqlDeleteCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlDeleteCommand1.Connection = Me.SqlConnection1
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ActuallFirstProductionDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "ActuallFirstProductionDate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AnticipatedFirstProductionDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "AnticipatedFirstProductionDate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@DrawingsApprovedDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "DrawingsApprovedDate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@DrawingsApprovedIssue", System.Data.SqlDbType.Char, 1, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "DrawingsApprovedIssue", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@FirstOrderPrice", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, True, CType(19, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@FirstOrderSEID", System.Data.SqlDbType.Char, 10, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "FirstOrderSEID", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PartsAvailableDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "PartsAvailableDate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PhasedQ1", System.Data.SqlDbType.SmallInt, 2, System.Data.ParameterDirection.Input, True, CType(5, Byte), CType(0, Byte), "PhasedQ1", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PhasedQ2", System.Data.SqlDbType.SmallInt, 2, System.Data.ParameterDirection.Input, True, CType(5, Byte), CType(0, Byte), "PhasedQ2", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PhasedQ3", System.Data.SqlDbType.SmallInt, 2, System.Data.ParameterDirection.Input, True, CType(5, Byte), CType(0, Byte), "PhasedQ3", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PhasedQ4", System.Data.SqlDbType.SmallInt, 2, System.Data.ParameterDirection.Input, True, CType(5, Byte), CType(0, Byte), "PhasedQ4", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@SamplesApprovedDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "SamplesApprovedDate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@SamplesApprovedIssue", System.Data.SqlDbType.Char, 1, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "SamplesApprovedIssue", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ToolingCompletionDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "ToolingCompletionDate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_ProjectAdminId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"ProjectAdminId",System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"UserId",System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_CurrencyDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"CurrencyDate",System.Data.DataRowVersion.Current, Nothing))
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
            Return "tProjectAdmin"
        End Get
    End Property

    ' <System.Web.Services.WebMethodAttribute()> _
    'Public Function test() As DataSet
    '    Return LoadData(New Object() {1, 67})
    'End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadData(ByVal pudtParams() As System.Object) As System.Data.DataSet Implements IEPS_Service_Data.LoadData
        'Dim x As New DataSet()
        SqlDataAdapter1.SelectCommand.Parameters("@ProjectId").Value = pudtParams(0)
        SqlDataAdapter1.SelectCommand.Parameters("@UserId").Value = pudtParams(1)
       Return LoadDataSet(SqlDataAdapter1)
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function UpdateData(ByVal Changes As System.Data.DataSet) As System.Data.DataSet Implements IEPS_Service_Data.UpdateData
        Return UpdateDataset(Changes, SqlDataAdapter1)
    End Function

    <System.Web.Services.WebMethodAttribute()> Public Function LoadCombo(ByVal pintUniqueId As Integer, ByVal pintAccurideLocationId As Integer) As System.Data.DataSet Implements IEPS_Service_Data.LoadCombo
        Return Nothing
    End Function

	Public Overrides Sub AssignFields()
		With Fields
			.Add("ProjectAdminId", New FieldInfoStructure("ProjectAdminId", 10, False, False))
			.Add("FirstOrderPrice", New FieldInfoStructure("FirstOrderPrice", 19, True, False))
			.Add("AnticipatedFirstProductionDate", New FieldInfoStructure("AnticipatedFirstProductionDate", 23, True, False))
			.Add("DrawingsApprovedDate", New FieldInfoStructure("DrawingsApprovedDate", 23, True, False))
			.Add("DrawingsApprovedIssue", New FieldInfoStructure("DrawingsApprovedIssue", 1, True, False, , "A"))
			.Add("FirstOrderSEID", New FieldInfoStructure("FirstOrderSEID", 10, True, False))
			.Add("PhasedQ1", New FieldInfoStructure("PhasedQ1", 5, True, False))
			.Add("PhasedQ2", New FieldInfoStructure("PhasedQ2", 5, True, False))
			.Add("PhasedQ3", New FieldInfoStructure("PhasedQ3", 5, True, False))
			.Add("PhasedQ4", New FieldInfoStructure("PhasedQ4", 5, True, False))
			.Add("PartsAvailableDate", New FieldInfoStructure("PartsAvailableDate", 23, True, False))
			.Add("SamplesApprovedDate", New FieldInfoStructure("SamplesApprovedDate", 23, True, False))
			.Add("SamplesApprovedIssue", New FieldInfoStructure("SamplesApprovedIssue", 1, True, False, , "A"))
			.Add("ToolingCompletionDate", New FieldInfoStructure("ToolingCompletionDate", 23, True, False))
			.Add("ActuallFirstProductionDate", New FieldInfoStructure("ActuallFirstProductionDate", 23, True, False))
			.Add("CompanyName", New FieldInfoStructure("CompanyName", 100, True, False))
			.Add("Issue", New FieldInfoStructure("Issue", 1, False, False, , "A"))
			.Add("BatchSize", New FieldInfoStructure("BatchSize", 53, True, False))
			.Add("AnnualQuantity", New FieldInfoStructure("AnnualQuantity", 53, True, False))
			.Add("AccuridePartNumberIssue", New FieldInfoStructure("AccuridePartNumberIssue", 1, True, False))
			.Add("AccuridePartNumber", New FieldInfoStructure("AccuridePartNumber", 50, True, False, , "Unknown"))
			.Add("Director", New FieldInfoStructure("Director", 50, True, False))
			.Add("CurrencyDate", New FieldInfoStructure("CurrencyDate", 23, False, False, , "GETDATE"))
			.Add("ProjectID", New FieldInfoStructure("ProjectID", 10, False, False))
		End With
	End Sub
End Class

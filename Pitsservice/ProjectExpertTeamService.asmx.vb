Imports System.Web.Services
Imports System.Data.SqlClient
<System.Web.Services.WebService(Namespace:="http://www.Accuride.com/ProjectExpertTeamService")> _
Public Class ProjectExpertTeamService
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
        Me.SqlDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Select_ProjectExpertTeam", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ApprovedByExpertTeamID", "ApprovedByExpertTeamID"), New System.Data.Common.DataColumnMapping("DirectorsApprovedPrice", "DirectorsApprovedPrice"), New System.Data.Common.DataColumnMapping("DirectorsApprovedSignatureID", "DirectorsApprovedSignatureID"), New System.Data.Common.DataColumnMapping("ExpertTeamStatusId", "ExpertTeamStatusId"), New System.Data.Common.DataColumnMapping("ProjectExpertTeamId", "ProjectExpertTeamId"), New System.Data.Common.DataColumnMapping("ProjectID", "ProjectID"), New System.Data.Common.DataColumnMapping("CanWe", "CanWe"), New System.Data.Common.DataColumnMapping("ApplicationKnowledge", "ApplicationKnowledge"), New System.Data.Common.DataColumnMapping("Features", "Features"), New System.Data.Common.DataColumnMapping("Remarks", "Remarks"), New System.Data.Common.DataColumnMapping("ShouldWe", "ShouldWe"), New System.Data.Common.DataColumnMapping("CustomerPartNumber", "CustomerPartNumber"), New System.Data.Common.DataColumnMapping("ProjectMasterGroup", "ProjectMasterGroup"), New System.Data.Common.DataColumnMapping("Issue", "Issue"), New System.Data.Common.DataColumnMapping("AnnualQuantity", "AnnualQuantity"), New System.Data.Common.DataColumnMapping("BatchSize", "BatchSize"), New System.Data.Common.DataColumnMapping("ProjectDescription", "ProjectDescription"), New System.Data.Common.DataColumnMapping("ProjectStatusDescription", "ProjectStatusDescription"), New System.Data.Common.DataColumnMapping("ProjectStatusReason", "ProjectStatusReason"), New System.Data.Common.DataColumnMapping("AccurideLocationId", "AccurideLocationId"), New System.Data.Common.DataColumnMapping("UserId", "UserId"), New System.Data.Common.DataColumnMapping("CurrencyDate", "CurrencyDate"), New System.Data.Common.DataColumnMapping("FitBusinessStrategyDescription", "FitBusinessStrategyDescription"), New System.Data.Common.DataColumnMapping("ApprovedPrice", "ApprovedPrice"), New System.Data.Common.DataColumnMapping("TargetMin", "TargetMin"), New System.Data.Common.DataColumnMapping("TargetMax", "TargetMax"), New System.Data.Common.DataColumnMapping("MarginAdjustment", "MarginAdjustment"), New System.Data.Common.DataColumnMapping("FixedPrice", "FixedPrice"), New System.Data.Common.DataColumnMapping("AcceptedQuotePrice", "AcceptedQuotePrice"), New System.Data.Common.DataColumnMapping("CostPercent", "CostPercent"), New System.Data.Common.DataColumnMapping("ToolCostPercent", "ToolCostPercent"), New System.Data.Common.DataColumnMapping("ModifyCost", "ModifyCost"), New System.Data.Common.DataColumnMapping("ModifyToolCost", "ModifyToolCost")})})
        Me.SqlDataAdapter1.UpdateCommand = Me.SqlUpdateCommand1
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "[ps_Select_ProjectExpertTeam]"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))

        '
        'SqlInsertCommand1
        '
        Me.SqlInsertCommand1.CommandText = "[ps_Insert_ProjectExpertTeam]"
        Me.SqlInsertCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlInsertCommand1.Connection = Me.SqlConnection1
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ApprovedByExpertTeamID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "ApprovedByExpertTeamID", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@DirectorsApprovedPrice", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, False, CType(19, Byte), CType(0, Byte), "DirectorsApprovedPrice", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@DirectorsApprovedSignatureID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "DirectorsApprovedSignatureID", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ExpertTeamStatusId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "ExpertTeamStatusId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "ProjectID", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@MarginAdjustment", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, False, CType(15, Byte), CType(0, Byte), "MarginAdjustment", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@FixedPrice", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "FixedPrice", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "AccurideLocationId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AcceptedQuotePrice", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, False, CType(19, Byte), CType(0, Byte), "AcceptedQuotePrice", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CostPercent", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, False, CType(16, Byte), CType(4, Byte), "CostPercent", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ToolCostPercent", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, False, CType(16, Byte), CType(4, Byte), "ToolCostPercent", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ModifyCost", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "ModifyCost", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ModifyToolCost", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "ModifyToolCost", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"UserId",System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_CurrencyDate", System.Data.SqlDbType.DateTime, 8,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"CurrencyDate",System.Data.DataRowVersion.Current,Nothing))
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = "[ps_Update_ProjectExpertTeam]"
        Me.SqlUpdateCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlUpdateCommand1.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "ProjectID", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ApplicationKnowledge", System.Data.SqlDbType.VarChar, 8000, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "ApplicationKnowledge", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ApprovedByExpertTeamID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "ApprovedByExpertTeamID", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@DirectorsApprovedPrice", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, False, CType(19, Byte), CType(0, Byte), "DirectorsApprovedPrice", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@DirectorsApprovedSignatureID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "DirectorsApprovedSignatureID", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ExpertTeamStatusId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "ExpertTeamStatusId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CanWe", System.Data.SqlDbType.VarChar, 8000, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "CanWe", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Features", System.Data.SqlDbType.VarChar, 8000, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "Features", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Remarks", System.Data.SqlDbType.VarChar, 8000, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "Remarks", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ShouldWe", System.Data.SqlDbType.VarChar, 8000, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "ShouldWe", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@MarginAdjustment", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, False, CType(15, Byte), CType(0, Byte), "MarginAdjustment", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@FixedPrice", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "FixedPrice", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AcceptedQuotePrice", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, False, CType(19, Byte), CType(0, Byte), "AcceptedQuotePrice", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CostPercent", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, False, CType(16, Byte), CType(4, Byte), "CostPercent", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ToolCostPercent", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, False, CType(16, Byte), CType(4, Byte), "ToolCostPercent", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ModifyCost", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "ModifyCost", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ModifyToolCost", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "ModifyToolCost", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ProjectID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"ProjectID",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ApplicationKnowledge", System.Data.SqlDbType.VarChar, 8000, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "ApplicationKnowledge", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ApprovedByExpertTeamID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"ApprovedByExpertTeamID",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_DirectorsApprovedPrice", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input,True, CType(19, Byte), CType(0, Byte),"DirectorsApprovedPrice",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_DirectorsApprovedSignatureID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"DirectorsApprovedSignatureID",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ExpertTeamStatusId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"ExpertTeamStatusId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CanWe", System.Data.SqlDbType.VarChar, 8000, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "CanWe", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Features", System.Data.SqlDbType.VarChar, 8000, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "Features", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Remarks", System.Data.SqlDbType.VarChar, 8000, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "Remarks", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ShouldWe", System.Data.SqlDbType.VarChar, 8000, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "ShouldWe", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_MarginAdjustment", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input,True, CType(15, Byte), CType(0, Byte),"MarginAdjustment",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_FixedPrice", System.Data.SqlDbType.Bit, 1,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"FixedPrice",System.Data.DataRowVersion.Original,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_AcceptedQuotePrice", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input,True, CType(19, Byte), CType(0, Byte),"AcceptedQuotePrice",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CostPercent", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input,True, CType(16, Byte), CType(4, Byte),"CostPercent",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ToolCostPercent", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input,True, CType(16, Byte), CType(4, Byte),"ToolCostPercent",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ModifyCost", System.Data.SqlDbType.Bit, 1,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"ModifyCost",System.Data.DataRowVersion.Original,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ModifyToolCost", System.Data.SqlDbType.Bit, 1,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"ModifyToolCost",System.Data.DataRowVersion.Original,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_CurrencyDate", System.Data.SqlDbType.DateTime, 8,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"CurrencyDate",System.Data.DataRowVersion.Current,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"AccurideLocationId",System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_ProjectExpertTeamId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"ProjectExpertTeamId",System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"UserId",System.Data.DataRowVersion.Current, Nothing))
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
            Return "tProjectExpertTeam"
        End Get
    End Property

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadData(ByVal pudtParams() As System.Object) As System.Data.DataSet Implements IEPS_Service_Data.LoadData
        ' Dim x As New DataSet()
        SqlDataAdapter1.SelectCommand.Parameters("@AccurideLocationId").Value = pudtParams(0)
        SqlDataAdapter1.SelectCommand.Parameters("@ProjectId").Value = pudtParams(1)
        SqlDataAdapter1.SelectCommand.Parameters("@UserId").Value = pudtParams(2)
       Return LoadDataSet(SqlDataAdapter1)
    End Function

    <WebMethod()> _
      Public Function runtest() As System.Data.DataSet
        Return LoadData(New Object() {1, 31, 66})
        'Return x
        'x.Tables(0).Rows(0).Item("CanWe") = "Yes"
        'Dim ds As DataSet
        'Try
        '    ds = UpdateData(x.GetChanges)
        'Catch e As Exception
        '    'Return e.Message
        'End Try
        'Return ds '"OK" 'UpdateData(x.GetChanges)
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
            .Add("ApprovedByExpertTeamID", New FieldInfoStructure("ApprovedByExpertTeamID", 10, True, False))
            .Add("CurrencyDate", New FieldInfoStructure("CurrencyDate", 23, False, False, , "GETDATE"))
            .Add("DirectorsApprovedPrice", New FieldInfoStructure("DirectorsApprovedPrice", 19, True, False))
            .Add("DirectorsApprovedSignatureID", New FieldInfoStructure("DirectorsApprovedSignatureID", 10, True, False))
            .Add("ExpertTeamStatusId", New FieldInfoStructure("ExpertTeamStatusId", 10, True, False, , 4))
            .Add("ProjectExpertTeamId", New FieldInfoStructure("ProjectExpertTeamId", 10, False, False))
            .Add("ProjectID", New FieldInfoStructure("ProjectID", 10, False, False))
            .Add("CanWe", New FieldInfoStructure("CanWe", 8000, True, False))
            .Add("ApplicationKnowledge", New FieldInfoStructure("ApplicationKnowledge", 2147483647, True, False))
            .Add("Features", New FieldInfoStructure("Features", 8000, True, False))
            .Add("Remarks", New FieldInfoStructure("Remarks", 8000, True, False))
            .Add("ShouldWe", New FieldInfoStructure("ShouldWe", 8000, True, False))
            .Add("CustomerPartNumber", New FieldInfoStructure("CustomerPartNumber", 50, True, False))
            .Add("Issue", New FieldInfoStructure("Issue", 1, False, False, , "A"))
            .Add("AnnualQuantity", New FieldInfoStructure("AnnualQuantity", 53, True, False))
            .Add("BatchSize", New FieldInfoStructure("BatchSize", 53, True, False))
            .Add("ProjectDescription", New FieldInfoStructure("ProjectDescription", 2500, True, False))
            .Add("ApprovedPrice", New FieldInfoStructure("ApprovedPrice", 19, True, False))
            .Add("MarginAdjustment", New FieldInfoStructure("MarginAdjustment", 53, False, False, , 0))
            .Add("FixedPrice", New FieldInfoStructure("FixedPrice", 1, False, False, , False))
            .Add("AcceptedQuotePrice", New FieldInfoStructure("AcceptedQuotePrice", 19, False, False, , 0))
            .Add("CostPercent", New FieldInfoStructure("CostPercent", 16, False, False, , 0))
            .Add("ToolCostPercent", New FieldInfoStructure("ToolCostPercent", 16, False, False, , 0))
            .Add("ModifyCost", New FieldInfoStructure("ModifyCost", 1, False, False, , False))
            .Add("ModifyToolCost", New FieldInfoStructure("ModifyToolCost", 1, False, False, , False))
            .Add("FullName", New FieldInfoStructure("FullName", 50, True, False))
        End With
    End Sub
End Class

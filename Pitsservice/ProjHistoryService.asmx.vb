Imports System.Web.Services
Imports System.Data.SqlClient
<System.Web.Services.WebService(Namespace:="http://www.Accuride.com/ProjHistoryService")> _
Public Class ProjHistoryService
    Inherits EPS_Service
    Implements IEPS_Service_Data
#Region " Web Services Designer Generated Code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Web Services Designer.
        InitializeComponent()

        'Add your own initialization code after the InitializeComponent() call

    End Sub
    Friend WithEvents SqlDataAdapter2 As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection

    'Required by the Web Services Designer
    Private components As System.ComponentModel.Container

    'NOTE: The following procedure is required by the Web Services Designer
    'It can be modified using the Web Services Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.SqlDataAdapter2 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
        '
        'SqlDataAdapter2
        '
        Me.SqlDataAdapter2.SelectCommand = Me.SqlSelectCommand1
        Me.SqlDataAdapter2.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_GetProjectHistoryList", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("EventHistoryId", "EventHistoryId"), New System.Data.Common.DataColumnMapping("EventRuleId", "EventRuleId"), New System.Data.Common.DataColumnMapping("NewOwnerId", "NewOwnerId"), New System.Data.Common.DataColumnMapping("EventDate", "EventDate"), New System.Data.Common.DataColumnMapping("EventId", "EventId"), New System.Data.Common.DataColumnMapping("NewOwnerName", "NewOwnerName"), New System.Data.Common.DataColumnMapping("EventChangeDescription", "EventChangeDescription"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("IsCurrent", "IsCurrent"), New System.Data.Common.DataColumnMapping("ProjectMasterId", "ProjectMasterId")})})
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "ps_GetProjectHistoryList"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
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
            Return "tEventHistory"
        End Get
    End Property

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadData(ByVal pudtParams() As System.Object) As System.Data.DataSet Implements IEPS_Service_Data.LoadData
        'Dim x As New DataSet()
        'SqlDataAdapter1.SelectCommand.Parameters("@ProjectId").Value = pudtParams(0)
        'SqlDataAdapter1.SelectCommand.Parameters("@AccurideLocationId").Value = (1)
        'SqlDataAdapter1.Fill(x)
        'Return x
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function UpdateData(ByVal Changes As System.Data.DataSet) As System.Data.DataSet Implements IEPS_Service_Data.UpdateData
        'Return UpdateDataset(Changes, SqlDataAdapter1)
    End Function

    <WebMethod(Description:="Returns the project history list for requested project, this is displayed in the history list control.")> _
    Public Function GetHistoryList(ByVal pintProjectId As Int32, ByVal pintLocationId As Int32) As DataSet
        Dim udtDataSet As New DataSet()
        SqlDataAdapter2.SelectCommand.Parameters("@ProjectId").Value = pintProjectId
        SqlDataAdapter2.SelectCommand.Parameters("@AccurideLocationId").Value = pintLocationId
        SqlDataAdapter2.Fill(udtDataSet)
        Return udtDataSet
    End Function



    <System.Web.Services.WebMethodAttribute()> Public Function LoadCombo(ByVal pintUniqueId As Integer, ByVal pintAccurideLocationId As Integer) As System.Data.DataSet Implements IEPS_Service_Data.LoadCombo
        Return Nothing
    End Function
	Public Overrides Sub AssignFields()
		With Fields
			.Add("EventHistoryId", New FieldInfoStructure("EventHistoryId", 10, False, False))
			.Add("EventRuleId", New FieldInfoStructure("EventRuleId", 10, True, False))
            .Add("NewOwnerId", New FieldInfoStructure("NewOwnerId", 10, False, False))
            .Add("EventDate", New FieldInfoStructure("EventDate", 23, False, False))
            .Add("EventId", New FieldInfoStructure("EventId", 10, False, False, , 0))
            .Add("ProjectMasterId", New FieldInfoStructure("ProjectMasterId", 10, False, False))
        End With
	End Sub
End Class

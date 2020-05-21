Imports System.Web.Services
Imports System.Data.SqlClient
<System.Web.Services.WebService(Namespace:="http://www.Accuride.com/EventService")> _
Public Class EventService
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
        Me.SqlDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Select_Event", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("EventId", "EventId"), New System.Data.Common.DataColumnMapping("GroupId", "GroupId"), New System.Data.Common.DataColumnMapping("EventTypeId", "EventTypeId"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("AccurideLocationId", "AccurideLocationId")}), New System.Data.Common.DataTableMapping("Table1", "Table1", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("EventId", "EventId"), New System.Data.Common.DataColumnMapping("GroupId", "GroupId"), New System.Data.Common.DataColumnMapping("EventTypeId", "EventTypeId"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("AccurideLocationId", "AccurideLocationId")}), New System.Data.Common.DataTableMapping("Table2", "Table2", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember"), New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember")}), New System.Data.Common.DataTableMapping("Table3", "Table3", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember"), New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember")})})
        Me.SqlDataAdapter1.UpdateCommand = Me.SqlUpdateCommand1
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "ps_Select_Event"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@EventId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@LoadCombo", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@EventTypeId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlInsertCommand1
        '
        Me.SqlInsertCommand1.CommandText = "ps_Insert_Event"
        Me.SqlInsertCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlInsertCommand1.Connection = Me.SqlConnection1
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@GroupId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "GroupId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@EventTypeId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "EventTypeId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.Char, 255, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Description", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "AccurideLocationId", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = "ps_Update_Event"
        Me.SqlUpdateCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlUpdateCommand1.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.Char, 255, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Description", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@GroupId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "GroupId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@EventTypeId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "EventTypeId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Description", System.Data.SqlDbType.Char, 255, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Description", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_GroupId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "GroupId", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_EventTypeId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "EventTypeId", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_EventId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "EventId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"AccurideLocationId",System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlDeleteCommand1
        '
        Me.SqlDeleteCommand1.CommandText = "ps_Delete_Event"
        Me.SqlDeleteCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlDeleteCommand1.Connection = Me.SqlConnection1
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Description", System.Data.SqlDbType.Char, 255, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Description", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_GroupId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "GroupId", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_EventTypeId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "EventTypeId", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_EventId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "EventId", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "AccurideLocationId", System.Data.DataRowVersion.Original, Nothing))
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

    Public Enum EventTypes
        Standard = 0
        Closure = 1
        Revision = 2
    End Enum

    Public ReadOnly Property TableName() As String Implements IEPS_Service_Data.TableName
        Get
            Return "tEvent"
        End Get
    End Property

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadData(ByVal pudtParams() As System.Object) As System.Data.DataSet Implements IEPS_Service_Data.LoadData
        SqlDataAdapter1.SelectCommand.Parameters("@AccurideLocationId").Value = CType(pudtParams(0), Int32)
        SqlDataAdapter1.SelectCommand.Parameters("@EventId").Value = CType(pudtParams(1), Int32)
        If pudtParams.GetUpperBound(0) = 2 Then
            SqlDataAdapter1.SelectCommand.Parameters("@LoadCombo").Value = 1
        Else
            SqlDataAdapter1.SelectCommand.Parameters("@LoadCombo").Value = Convert.DBNull
        End If
        SqlDataAdapter1.SelectCommand.Parameters("@EventTypeId").Value = Convert.DBNull
        Return LoadDataSet(SqlDataAdapter1)
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadEventsOfAGivenType(ByVal accurideLocationId As Int32, ByVal eventTypeId As EventTypes) As System.Data.DataSet
        With SqlDataAdapter1.SelectCommand
            .Parameters("@AccurideLocationId").Value = accurideLocationId
            .Parameters("@EventId").Value = 0
            .Parameters("@LoadCombo").Value = Convert.DBNull
            .Parameters("@EventTypeId").Value = eventTypeId
        End With
        Return LoadDataSet(SqlDataAdapter1)
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function GetEventType(ByVal accurideLocationId As Int32, ByVal eventId As Int32) As EventTypes
        Dim ds As DataSet = LoadData(New Object() {accurideLocationId, eventId})
        Dim ReturnType As EventTypes = EventTypes.Standard
        If Not ds Is Nothing AndAlso ds.Tables.Count > 0 _
           AndAlso ds.Tables(0).Rows.Count > 0 Then
            ReturnType = CType(ds.Tables(0).Rows(0).Item("EventTypeId"), EventTypes)
        End If
        ds = Nothing
        Return ReturnType
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function UpdateData(ByVal Changes As System.Data.DataSet) As System.Data.DataSet Implements IEPS_Service_Data.UpdateData
        Return UpdateDataset(Changes, SqlDataAdapter1)
    End Function

    <WebMethod()> _
    Public Function CanProjectBeClosed(ByVal pintLocationId As Int32, ByVal pintProjectId As Int32, ByVal pintUserId As Int32) As Boolean
        Return IsProjectOfEventType(pintLocationId, pintProjectId, pintUserId, EventTypes.Closure)
    End Function

    <WebMethod()> _
    Public Function CanProjectBeRevised(ByVal pintLocationId As Int32, ByVal pintProjectId As Int32, ByVal pintUserId As Int32) As Boolean
        Return IsProjectOfEventType(pintLocationId, pintProjectId, pintUserId, EventTypes.Revision)
    End Function

    Private Function IsProjectOfEventType(ByVal pintLocationId As Int32, ByVal pintProjectId As Int32, ByVal pintUserId As Int32, ByVal eventType As EventTypes) As Boolean
        Dim ReturnValue As Boolean = False
        Dim ds As DataSet = GetApplicableEventsForProject(pintLocationId, pintProjectId, pintUserId, True, False)
        If Not ds Is Nothing AndAlso ds.Tables.Count > 0 _
           AndAlso ds.Tables(0).Rows.Count > 0 Then
            ds.Tables(0).DefaultView.RowFilter = "EventTypeId = " & eventType
            If ds.Tables(0).DefaultView.Count > 0 Then
                ReturnValue = True
            End If
        End If
        ds = Nothing
        Return ReturnValue
    End Function

    Public Function GetApplicableEventsForProject(ByVal pintLocationId As Int32, ByVal pintProjectId As Int32, ByVal pintUserId As Int32, ByVal getAll As Boolean, ByVal getCombined As Boolean) As DataSet
        Dim intReturn As Int32 = 0
        Dim udtDS As New DataSet()
        Try
            Me.SqlConnection1.Open()
        Catch
            Return udtDS
        Finally
            Dim udtSelect As New SqlCommand("GetApplicableEventsForProject", Me.SqlConnection1)
            Dim udtDA As New SqlDataAdapter(udtSelect)
            With udtSelect
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
                .Parameters.Add("@AccurideLocationId", pintLocationId)
                .Parameters.Add("@ProjectId", pintProjectId)
                .Parameters.Add("@UserId", pintUserId)
                If getAll Then
                    .Parameters.Add("@GetAllEventTypes", 1)
                Else
                    .Parameters.Add("@GetAllEventTypes", 0)
                End If
                If getCombined Then
                    .Parameters.Add("@CombineOutput", 1)
                Else
                    .Parameters.Add("@CombineOutput", 0)
                End If
                udtDA.Fill(udtDS)
                Try
                    intReturn = CType(.Parameters("@RETURN_VALUE").Value, Int32)
                Catch
                    intReturn = 0
                End Try
            End With
            udtDA = Nothing
            udtSelect = Nothing
        End Try
        Return udtDS
    End Function

    <WebMethod()> Public Function GetApplicableEventsForProject(ByVal pintLocationId As Int32, ByVal pintProjectId As Int32, ByVal pintUserId As Int32, ByVal getCombined As Boolean) As DataSet
        Return GetApplicableEventsForProject(pintLocationId, pintProjectId, pintUserId, False, getCombined)
    End Function

    <WebMethod()> Public Function IsProjectMultiEventing(ByVal pintProjectId As Int32) As Boolean
        Dim intReturn As Int32 = 0
        Try
            Me.SqlConnection1.Open()
        Catch
            Return False
        Finally
            Dim udtSelect As New SqlCommand("IsProjectMultiEventing", Me.SqlConnection1)
            Dim udtDataSet As New DataSet()
            With udtSelect
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
                .Parameters.Add("@ProjectId", pintProjectId)
                Try
                    udtSelect.ExecuteNonQuery()
                    intReturn = CType(.Parameters("@RETURN_VALUE").Value, Int32)
                Catch
                    intReturn = 0
                End Try
            End With
            udtDataSet = Nothing
            udtSelect = Nothing
        End Try
        Return (intReturn > 1)
    End Function

    <WebMethod()> Public Function AmITheRaiserOfAMultiEventProject(ByVal pintProjectId As Int32, ByVal pintUserId As Int32) As Boolean
        Dim intReturn As Int32 = 0
        Try
            Me.SqlConnection1.Open()
        Catch
            Return False
        Finally
            Dim udtSelect As New SqlCommand("AmITheRaiserOfAMultiEventProject", Me.SqlConnection1)
            Dim udtDataSet As New DataSet()
            With udtSelect
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
                .Parameters.Add("@UserId", pintUserId)
                .Parameters.Add("@ProjectId", pintProjectId)
                Try
                    udtSelect.ExecuteNonQuery()
                    intReturn = CType(.Parameters("@RETURN_VALUE").Value, Int32)
                Catch
                    intReturn = 0
                End Try
            End With
            udtDataSet = Nothing
            udtSelect = Nothing
        End Try
        Return (intReturn > 0)
    End Function

    <WebMethod()> Public Function GetMultiEventingRaiser(ByVal pintProjectId As Int32) As Int32
        Dim intReturn As Int32 = 0
        Try
            Me.SqlConnection1.Open()
        Catch
            Return 0
        Finally
            Dim udtSelect As New SqlCommand("GetMultiEventingRaiser", Me.SqlConnection1)
            Dim udtDataSet As New DataSet()
            With udtSelect
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
                .Parameters.Add("@ProjectId", pintProjectId)
                Try
                    udtSelect.ExecuteNonQuery()
                    intReturn = CType(.Parameters("@RETURN_VALUE").Value, Int32)
                Catch
                    intReturn = 0
                End Try
            End With
            udtDataSet = Nothing
            udtSelect = Nothing
        End Try
        Return intReturn
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadCombo(ByVal pintUniqueId As Int32, ByVal pintAccurideLocationId As Int32) As DataSet Implements IEPS_Service_Data.LoadCombo
        Return LoadData(New Object() {pintAccurideLocationId, pintUniqueId, 1})
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function CreateMultiEvent(ByVal pintEventId As Int32, ByVal pintProjectId As Int32, ByVal pintUserId As Int32, ByVal currentUserId As Int32) As Int32
        Return AddProjectEvents(pintEventId, pintProjectId, pintUserId, currentUserId, True)
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function ReassignEvent(ByVal pintEventId As Int32, ByVal pintProjectId As Int32, ByVal pintUserId As Int32, ByVal currentUserId As Int32) As Int32
        Return AddProjectEvents(pintEventId, pintProjectId, pintUserId, currentUserId, False)
    End Function

    Private Function AddProjectEvents(ByVal pintEventId As Int32, ByVal pintProjectId As Int32, ByVal pintUserId As Int32, ByVal currentUserId As Int32, ByVal pblnMultiEvent As Boolean) As Int32
        Dim udtStageRuleService As New StageRuleService()
        Dim intStageRuleId As Int32 = udtStageRuleService.GetStageRuleId(pintEventId, pintProjectId)
        udtStageRuleService = Nothing

        Dim ReturnValue As Int32 = 0
        If intStageRuleId > 0 Then
            Try
                Me.SqlConnection1.Open()
            Catch
                Return 0
            Finally
                Dim tr As SqlTransaction = Me.SqlConnection1.BeginTransaction()
                Dim udtSelect As New SqlCommand("CreateEventItem", Me.SqlConnection1)
                udtSelect.Transaction = tr
                Dim udtDataSet As New DataSet
                With udtSelect
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
                    .Parameters.Add("@EventId", pintEventId)
                    .Parameters.Add("@ProjectId", pintProjectId)
                    .Parameters.Add("@UserId", pintUserId)
                    .Parameters.Add("@MultiEvent", pblnMultiEvent)
                    .Parameters.Add("@StageRuleId", intStageRuleId)
                    .Parameters.Add("@EventDate", Date.Now)
                    .Parameters.Add("@CurrentUserId", currentUserId)
                    Try
                        udtSelect.ExecuteNonQuery()
                        ReturnValue = CType(.Parameters("@RETURN_VALUE").Value, Int32)
                    Catch ex As SqlException
                        ReturnValue = ex.Number
                    End Try
                End With
                If ReturnValue = 1 Then
                    tr.Commit()
                Else
                    tr.Rollback()
                End If
                tr = Nothing
                udtDataSet = Nothing
                udtSelect = Nothing
            End Try
        End If
        Return ReturnValue
    End Function

    '<System.Web.Services.WebMethodAttribute()> _
    'Public Function TestSetEvents() As String
    '    Return SetEvents(1, 1124, New Int32() {1}, 2)
    'End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function SetEvents(ByVal AccurideLocationId As Int32, ByVal EventId As Int32, _
            ByVal GroupIds() As Int32, ByVal eventTypeId As Int32) As Boolean

        Dim EventsWeb As New EventService()
        Dim EventData As DataSet = EventsWeb.LoadData(New Object() {AccurideLocationId, EventId})
        Dim ReturnVal As Boolean = False
        If EventData.Tables.Count > 0 AndAlso EventData.Tables(0).Rows.Count > 0 Then
            Dim EventDesc As String = EventData.Tables(0).Rows(0).Item("Description").ToString
            If Not GroupIds Is Nothing Then

                Dim Trans As SqlTransaction
                Try
                    If Not (SqlConnection1.State Or ConnectionState.Open) = SqlConnection1.State Then
                        SqlConnection1.Open()
                    End If
                    Trans = SqlConnection1.BeginTransaction()

                    Try
                        Dim GroupId As Int32
                        Dim udtInsert As New SqlCommand("ps_Insert_Event", SqlConnection1)
                        With udtInsert
                            .CommandType = CommandType.StoredProcedure
                            .Transaction = Trans
                            .Parameters.Add("@AccurideLocationId", AccurideLocationId)
                            .Parameters.Add("@Description", EventDesc)
                            .Parameters.Add("@GroupId", 0)
                            .Parameters.Add("@EventTypeId", eventTypeId)
                            For Each GroupId In GroupIds
                                .Parameters("@GroupId").Value = GroupId
                                .ExecuteNonQuery()
                            Next GroupId
                        End With
                        udtInsert = Nothing
                        Trans.Commit()
                        ReturnVal = True
                    Catch e As Exception
                        Trans.Rollback()
                    End Try
                Catch e As Exception
                End Try
                Trans = Nothing
            End If
        End If
        Return ReturnVal
    End Function

    Public Overrides Sub AssignFields()
        With Fields
            .Add("EventId", New FieldInfoStructure("EventId", 10, False, False))
            .Add("GroupId", New FieldInfoStructure("GroupId", 10, False, False))
            .Add("EventTypeId", New FieldInfoStructure("EventTypeId", 10, False, False))
        End With
    End Sub

    '<System.Web.Services.WebMethodAttribute()> _
    Private Overloads Function GetEventInfo(ByVal projectId As Int32, ByVal userId As Int32, ByVal accurideLocationId As Int32) As DataSet
        Dim ReturnValue As New DataSet()
        Try
            Me.SqlConnection1.Open()
        Catch
            Return ReturnValue
        Finally
            Dim DA As New SqlDataAdapter("ps_GetEventInfo", SqlConnection1)
            With DA.SelectCommand
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@ProjectId", projectId)
                .Parameters.Add("@UserId", userId)
                .Parameters.Add("@AccurideLocationId", accurideLocationId)
            End With

            DA.Fill(ReturnValue)
            DA = Nothing
        End Try
        Return ReturnValue

        'IsProjectMultiEventing
        'AmITheRaiserOfAMultiEventProject
        'GetMultiEventingRaiser
        'NewUser()
        'Event()
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Overloads Function GetEventInfo(ByVal params() As System.Object) As DataSet
        Return GetEventInfo(CType(params(0), Int32), CType(params(1), Int32), CType(params(1), Int32))
    End Function
End Class

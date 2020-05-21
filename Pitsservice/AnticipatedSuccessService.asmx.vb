Imports System.Web.Services
Imports System.Data.SqlClient
<System.Web.Services.WebService(Namespace:="http://www.Accuride.com/AnticipatedSuccessService")> _
Public Class AnticipatedSuccessService
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
        Me.SqlDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Select_AnticipatedSuccess", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("AnticipatedSuccessId", "AnticipatedSuccessId"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("Percent", "Percent")}), New System.Data.Common.DataTableMapping("Table1", "Table1", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("AnticipatedSuccessId", "AnticipatedSuccessId"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("Percent", "Percent")}), New System.Data.Common.DataTableMapping("Table2", "Table2", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember"), New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember")}), New System.Data.Common.DataTableMapping("Table3", "Table3", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember"), New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember")})})
        Me.SqlDataAdapter1.UpdateCommand = Me.SqlUpdateCommand1
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "ps_Select_AnticipatedSuccess"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_AnticipatedSuccessId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"AnticipatedSuccessId",System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@LoadCombo", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlInsertCommand1
        '
        Me.SqlInsertCommand1.CommandText = "ps_Insert_AnticipatedSuccess"
        Me.SqlInsertCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlInsertCommand1.Connection = Me.SqlConnection1
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.Char, 30, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Description", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Percent", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, True, CType(15, Byte), CType(0, Byte), "Percent", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = "ps_Update_AnticipatedSuccess"
        Me.SqlUpdateCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlUpdateCommand1.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.Char, 30, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Description", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Percent", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, True, CType(15, Byte), CType(0, Byte), "Percent", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Description", System.Data.SqlDbType.Char, 30, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Description", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Percent", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, True, CType(15, Byte), CType(0, Byte), "Percent", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_AnticipatedSuccessId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "AnticipatedSuccessId", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlDeleteCommand1
        '
        Me.SqlDeleteCommand1.CommandText = "ps_Delete_AnticipatedSuccess"
        Me.SqlDeleteCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlDeleteCommand1.Connection = Me.SqlConnection1
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Description", System.Data.SqlDbType.Char, 30, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Description", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Percent", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, True, CType(15, Byte), CType(0, Byte), "Percent", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_AnticipatedSuccessId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "AnticipatedSuccessId", System.Data.DataRowVersion.Current, Nothing))
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
            Return "tAnticipatedSuccess"
        End Get
    End Property

    '<System.Web.Services.WebMethodAttribute()> _
    'Public Function test() As DataSet
    '    Return LoadData(New Object() {0, 1})
    'End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadData(ByVal pudtParams() As System.Object) As System.Data.DataSet Implements IEPS_Service_Data.LoadData
        SqlDataAdapter1.SelectCommand.Parameters("@Select_AnticipatedSuccessId").Value = CType(pudtParams(0), Int32)
        If pudtParams.GetUpperBound(0) = 1 Then
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

    Private Function GetEventsData(ByVal AnticipatedSuccessId As Int32, ByVal Ignore As Boolean) As System.Data.DataSet
        Dim ReturnDS As DataSet
        Try
            SqlConnection1.Open()
        Catch
            ReturnDS = Nothing
        Finally
            Dim udtSelect As New SqlCommand("ps_Select_AnticipatedSuccessEvents", SqlConnection1)
            Dim udtDataSet As New DataSet()
            With udtSelect
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@AnticipatedSuccessId", AnticipatedSuccessId)
                .Parameters.Add("@Ignore", Ignore)
            End With
            Dim udtDataAdapter As New SqlDataAdapter(udtSelect)
            Try
                udtDataAdapter.Fill(udtDataSet)
                ReturnDS = udtDataSet
            Catch
                ReturnDS = Nothing
            End Try
            udtDataSet = Nothing
            udtSelect = Nothing
        End Try
        Return ReturnDS
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function GetEvents(ByVal AnticipatedSuccessId As Int32) As System.Data.DataSet
        Return GetEventsData(AnticipatedSuccessId, False)
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function GetExcludedEvents(ByVal AnticipatedSuccessId As Int32) As System.Data.DataSet
        Return GetEventsData(AnticipatedSuccessId, True)
    End Function

    '<System.Web.Services.WebMethodAttribute()> _
    'Public Function SetEvents2() As Boolean
    '    Dim EventIds() As Int32 = {1191, 1186, 1136}
    '    Return SetEvents(1, EventIds)
    'End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function SetEvents(ByVal AnticipatedSuccessId As Int32, ByVal EventIds() As Int32) As Boolean
        'remove current list of events
        DeleteEvents(AnticipatedSuccessId)

        Dim ReturnVal As Boolean = True
        If Not EventIds Is Nothing Then
            Dim Trans As SqlTransaction
            Try
                If Not (SqlConnection1.State Or ConnectionState.Open) = SqlConnection1.State Then
                    SqlConnection1.Open()
                End If
                Trans = SqlConnection1.BeginTransaction()
                Try
                    Dim EventId As Int32
                    Dim udtInsert As New SqlCommand("ps_Assign_AnticipatedSuccessEvents", SqlConnection1)
                    With udtInsert
                        .CommandType = CommandType.StoredProcedure
                        .Transaction = Trans
                        .Parameters.Add("@AnticipatedSuccessId", AnticipatedSuccessId)
                        .Parameters.Add("@EventId", 0)
                        For Each EventId In EventIds
                            .Parameters("@EventId").Value = EventId
                            .ExecuteNonQuery()
                        Next EventId
                    End With
                    udtInsert = Nothing
                    Trans.Commit()
                Catch e As Exception
                    Trans.Rollback()
                    ReturnVal = False
                End Try
            Catch e As Exception
                ReturnVal = False
            End Try
            Trans = Nothing
        End If
        Return ReturnVal
    End Function

    Private Sub DeleteEvents(ByVal AnticipatedSuccessId As Int32)
        Dim SqlCommand1 As New System.Data.SqlClient.SqlCommand()
        With SqlCommand1
            .CommandText = "ps_Delete_AnticipatedSuccessEvents"
            .CommandType = System.Data.CommandType.StoredProcedure
            .Connection = SqlConnection1
            .Parameters.Add("@AnticipatedSuccessId", AnticipatedSuccessId)
            '            .Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
            Try
                .Connection.Open()
                .ExecuteNonQuery()
            Catch
            End Try
        End With
    End Sub

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadCombo(ByVal pintUniqueId As Int32, ByVal pintAccurideLocationId As Int32) As DataSet Implements IEPS_Service_Data.LoadCombo
        Return LoadData(New Object() {pintUniqueId, pintAccurideLocationId})
    End Function
    Public Overrides Sub AssignFields()
        With Fields
            .Add("AnticipatedSuccessId", New FieldInfoStructure("AnticipatedSuccessId", 10, False, False))
            .Add("Description", New FieldInfoStructure("Description", 30, False, False))
            .Add("Percent", New FieldInfoStructure("Percent", 3, False, False))
            .Add("EventId", New FieldInfoStructure("EventId", 100, False, False))
        End With
    End Sub
End Class

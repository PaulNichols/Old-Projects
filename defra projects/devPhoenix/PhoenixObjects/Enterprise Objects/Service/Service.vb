Imports System.Threading
Imports System.Diagnostics
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.Runtime.Remoting.Messaging

'========================================================
'Service
'--------------------------------------------------------
'Purpose : The class that manages a data object 
'representation.
'
'Author : Steven Sartain (x912595)
'
'Notes : 
'--------------------------------------------------------
'Inherits : ContextBoundObject
'--------------------------------------------------------
'Implements : ICounterProvider
'--------------------------------------------------------
'Revision History
'--------------------------------------------------------
'20 Nov 2003 x912595 : Documented source.
'========================================================
Public Class Service
    Inherits ContextBoundObject
    Implements ICounterProvider, IService

    <Serializable()> _
    Public Enum ErrorList
        Update_ConcurrencyViolation = 1
        Update_TooManyRowsUpdated = 2
        Update_Unknown = 3
    End Enum
    'Public Overridable Function Update(ByVal entity As Entity) As Entity
    '    Throw New NotImplementedException
    'End Function

    'Public Overridable Function Insert(ByVal entity As Entity) As Entity
    '    Throw New NotImplementedException
    'End Function

    Protected Shared _numServiceObjectsCounter As PerformanceCounter
    Protected Shared _getEntitySetRateCounter As PerformanceCounter
    Protected Shared _getAllRateCounter As PerformanceCounter
    Protected Shared _getByIdRateCounter As PerformanceCounter
    Private Shared mLastDBError As SQLError

    Protected Const NumServiceObjectsCounterName As String = "NumServiceObjects"
    Protected Const GetEntitySetRateCounterName As String = "Service.GetEntitySets/sec"
    Protected Const GetAllRateCounterName As String = "Service.GetAll/sec"
    Protected Const GetByIdRateCounterName As String = "Service.GetById/sec"

    Public Sub New()

        ' counter...
        If Not _numServiceObjectsCounter Is Nothing Then
            _numServiceObjectsCounter.Increment()
        End If

    End Sub

    Protected Overrides Sub Finalize()

        ' try to decrement...
        Try
            If Not _numServiceObjectsCounter Is Nothing Then
                _numServiceObjectsCounter.Decrement()
            End If
        Catch
        End Try

    End Sub

    Public Function GetEntitySet(ByVal commandText As String, ByVal entitySetType As Type) As EntitySet
        Return GetEntitySet(commandText, entitySetType, Nothing)
    End Function

    '========================================================
    'GetEntitySet
    '--------------------------------------------------------
    'Purpose : Creates an entity set from the data in the 
    'database
    '
    'Author : Steven Sartain (x912595)
    '
    'Notes : 
    '--------------------------------------------------------
    'Parameters
    '----------
    'commandText (String) : How to get the data 
    'entitySetType (Type) : The type of entity to create
    'tran (SqlTransaction) : For optional transaction handling 
    '--------------------------------------------------------
    'Returns : EntitySet
    '--------------------------------------------------------
    'Revision History
    '--------------------------------------------------------
    '20 Nov 2003 x912595 : Documented source.
    '========================================================  
    Public Function GetEntitySet(ByVal commandText As String, ByVal entitySetType As Type, ByVal tran As SqlClient.SqlTransaction) As EntitySet

        ' create a command...
        Dim command As New SqlCommand(commandText)
        command.Transaction = tran
        Dim results As EntitySet = GetEntitySet(command, entitySetType, tran)
        command.Dispose()

        ' return...
        Return results

    End Function

    Public Function GetEntitySet(ByVal sqlViewName As String, ByVal idColumnName As String, ByVal id As Int32, ByVal entitySetType As Type) As EntitySet
        Return GetEntitySet(sqlViewName, New String() {idColumnName}, New Int32() {id}, entitySetType)
    End Function

    Public Function GetEntitySet(ByVal sqlViewName As String, ByVal idColumnName() As String, ByVal id() As Int32, ByVal entitySetType As Type) As EntitySet
        Return GetEntitySet(sqlViewName, idColumnName, id, entitySetType, Nothing)
    End Function

    '========================================================
    'GetEntitySet
    '--------------------------------------------------------
    'Purpose : Creates an entity set from the data in the 
    'database
    '
    'Author : Steven Sartain (x912595)
    '
    'Notes : 
    '--------------------------------------------------------
    'Parameters
    '----------
    'sqlViewName (String) : The name of the stored proc
    'idColumnName() (String) : the stored proc params
    'id() (Int32) : the stored proc values
    'entitySetType (Type) : The type of entity to create
    'tran (SqlTransaction) : For optional transaction handling 
    '--------------------------------------------------------
    'Returns : EntitySet
    '--------------------------------------------------------
    'Revision History
    '--------------------------------------------------------
    '20 Nov 2003 x912595 : Documented source.
    '========================================================  
    Public Function GetEntitySet(ByVal sqlViewName As String, ByVal idColumnName() As String, ByVal id() As Int32, ByVal entitySetType As Type, ByVal tran As SqlClient.SqlTransaction) As EntitySet

        ' create a command...
        Dim command As New SqlCommand(sqlViewName)
        command.CommandType = CommandType.StoredProcedure
        command.Transaction = tran
        For i As Int32 = 0 To idColumnName.GetUpperBound(0)
            If Not idColumnName(i) Is Nothing AndAlso _
               Not idColumnName(i).StartsWith("@") Then
                idColumnName(i) = idColumnName(i).Insert(0, "@")
            End If
            command.Parameters.Add(idColumnName(i), id(i))
        Next i

        Dim results As EntitySet = GetEntitySet(command, entitySetType, tran)
        command.Dispose()

        ' return...
        Return results

    End Function

    Public Function GetEntitySet(ByVal command As SqlCommand, ByVal entitySetType As Type) As EntitySet
        Return GetEntitySet(command, entitySetType, Nothing)
    End Function

    '========================================================
    'GetEntitySet
    '--------------------------------------------------------
    'Purpose : Creates an entity set from the data in the 
    'database
    '
    'Author : Steven Sartain (x912595)
    '
    'Notes : 
    '--------------------------------------------------------
    'Parameters
    '----------
    'command (SqlCommand) : The name of the stored proc
    'entitySetType (Type) : The type of entity to create
    'tran (SqlTransaction) : For optional transaction handling 
    '--------------------------------------------------------
    'Returns : EntitySet
    '--------------------------------------------------------
    'Revision History
    '--------------------------------------------------------
    '20 Nov 2003 x912595 : Documented source.
    '========================================================  
    Public Function GetEntitySet(ByVal command As SqlCommand, ByVal entitySetType As Type, ByVal tran As SqlClient.SqlTransaction) As EntitySet
        mLastDBError = Nothing

        ' count...
        If Not _getEntitySetRateCounter Is Nothing Then
            _getEntitySetRateCounter.Increment()
        End If

        Dim connection As SqlConnection
        'do we have a tran
        If Not tran Is Nothing Then
            connection = tran.Connection
            command.Transaction = tran
            command.Connection = connection
        ElseIf command.Connection Is Nothing Then
            ' do we have a connection?
            connection = New SqlConnection(EnterpriseApplication.Application.ConnectionString)
            connection.Open()
            command.Connection = connection
        End If


        ' run...
        Dim entityset As entityset
        If Not entitySetType Is Nothing Then
            entityset = CType(System.Activator.CreateInstance(entitySetType), entityset)
        Else
            entityset = New entityset
        End If

        ' adapter...
        'Try                                            'MLD 8/4/5 temporarily commented out to make bugs more explicit during testing
            Dim adapter As New SqlDataAdapter(command)
            adapter.Fill(entityset)
            adapter.Dispose()
        'Catch ex As SqlException
            'Select Case CType(System.Enum.Parse(GetType(ErrorList), ex.State.ToString), ErrorList)
            '    Case ErrorList.Update_ConcurrencyViolation
            '    Case ErrorList.Update_DataMatch
            '        'all was ok
            '    Case ErrorList.Update_DataMismatch
            '    Case ErrorList.Update_TooManyRowsUpdated
            'End Select
            'mLastDBError = New SQLError(ex.Procedure, ex.State, ex.Message)
            'Throw
        'End Try

        ' close the connection...
        If Not connection Is Nothing AndAlso tran Is Nothing Then
            connection.Close()
        End If

        ' return...
        Return entityset

    End Function

    Public Function GetLastDBError() As SQLError Implements IService.GetLastDBError
        Return mLastDBError
    End Function

    '========================================================
    'GetAll
    '--------------------------------------------------------
    'Purpose : Creates an entity set from the data in the 
    'database
    '
    'Author : Steven Sartain (x912595)
    '
    'Notes : 
    '--------------------------------------------------------
    'Parameters
    '----------
    'sqlViewName (String) : The name of the stored proc
    'entitySetType (Type) : The type of entity to create
    'addHyphen (Boolean) : Used on combos to insert a "-" at the top
    'includeInactive (Boolean) : Any tables with fields called "Active"
    'can be selected or not.  Used as a filter
    '--------------------------------------------------------
    'Returns : EntitySet
    '--------------------------------------------------------
    'Revision History
    '--------------------------------------------------------
    '20 Nov 2003 x912595 : Documented source.
    '========================================================  
    Public Function GetAll(ByVal sqlViewName As String, ByVal entitySetType As Type, ByVal addHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As Int32) As EntitySet

        ' count...
        If Not _getAllRateCounter Is Nothing Then
            _getAllRateCounter.Increment()
        End If

        Dim Results As EntitySet
        ' run it...
        Dim command As New SqlCommand(sqlViewName)
        command.CommandType = CommandType.StoredProcedure

        If (HasStates AndAlso Not includeInactive) Then
            command.Parameters.Add("@includeInactive", False)
        End If
        If orderBy > 0 Then
            command.Parameters.Add("@sortOrder", orderBy)
        End If
        Results = GetEntitySet(command, entitySetType)
        command.Dispose()
        command = Nothing

        'Results = GetEntitySet(sqlViewName, entitySetType)
        If addHyphen AndAlso _
           Not Results Is Nothing AndAlso _
           Results.Tables.Count = 1 AndAlso _
           Results.Tables(0).Columns.Count > 0 Then
            Dim HyphenRow As DataRow = Results.Tables(0).NewRow()
            For Each item As DataColumn In Results.Tables(0).Columns
                If item.DataType.Equals(GetType(String)) Then
                    HyphenRow.Item(item) = "-"
                ElseIf item.DataType.Equals(GetType(Int32)) Then
                    HyphenRow.Item(item) = 0
                ElseIf item.DataType.Equals(GetType(Boolean)) Then
                    HyphenRow.Item(item) = False
                End If
            Next item
            Results.Tables(0).Rows.InsertAt(HyphenRow, 0)
        End If

        Return Results

    End Function

    Public Function GetAll(ByVal sqlViewName As String, ByVal entitySetType As Type, ByVal addHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet
        Return GetAll(sqlViewName, entitySetType, addHyphen, includeInactive, -1)
    End Function

    Public Function GetAll(ByVal sqlViewName As String, ByVal entitySetType As Type, ByVal includeInactive As Boolean) As EntitySet
        Return GetAll(sqlViewName, entitySetType, False, includeInactive, -1)
    End Function

    Public Function GetAll(ByVal sqlViewName As String, ByVal entitySetType As Type, ByVal includeInactive As Boolean, ByVal orderBy As Int32) As EntitySet
        Return GetAll(sqlViewName, entitySetType, False, includeInactive, orderBy)
    End Function

    Public Function GetById(ByVal sqlViewName As String, ByVal idColumnName As String, ByVal id As Integer, ByVal entitySetType As Type, ByVal tran As SqlClient.SqlTransaction) As Entity
        Return GetById(sqlViewName, New String() {idColumnName}, New Int32() {id}, entitySetType, tran)
    End Function

    Public Function GetById(ByVal sqlViewName As String, ByVal idColumnName As String, ByVal id As Integer, ByVal entitySetType As Type) As Entity
        Return GetById(sqlViewName, idColumnName, id, entitySetType, Nothing)
    End Function

    Public Function GetById(ByVal sqlViewName As String, ByVal idColumnName() As String, ByVal id() As Integer, ByVal entitySetType As Type) As Entity
        Return GetById(sqlViewName, idColumnName, id, entitySetType, Nothing)
    End Function

    '========================================================
    'GetById
    '--------------------------------------------------------
    'Purpose : Gets a single record using the primary key value
    '
    'Author : Steven Sartain (x912595)
    '
    'Notes : 
    '--------------------------------------------------------
    'Parameters
    '----------
    'sqlViewName (String) : The name of the stored proc
    'idColumnName() (String) : the stored proc params
    'id() (Int32) : the stored proc values
    'entitySetType (Type) : The type of entity to create
    'tran (SqlTransaction) : For optional transaction handling 
    '--------------------------------------------------------
    'Returns : Entity
    '--------------------------------------------------------
    'Revision History
    '--------------------------------------------------------
    '20 Nov 2003 x912595 : Documented source.
    '========================================================  
    Public Function GetById(ByVal sqlViewName As String, ByVal idColumnName() As String, ByVal id() As Integer, ByVal entitySetType As Type, ByVal tran As SqlClient.SqlTransaction) As Entity

        ' count...
        If Not _getByIdRateCounter Is Nothing Then
            _getByIdRateCounter.Increment()
        End If

        ' run...
        Dim entities As EntitySet = GetEntitySet(sqlViewName, idColumnName, id, entitySetType, tran) '"select top 1 * from " & tableName & " where " & idColumnName & "=" & id, entitySetType)
        If entities.Count = 1 Then
            Dim NewEntity As Entity = entities.GetEntity(0)
            If Not NewEntity Is Nothing Then
                If Not NewEntity.GetType.GetInterface(GetType(IUpdatable).ToString) Is Nothing Then
                    'the developer has implemented the IUpdatable interface, this means that 
                    'he/she will want to save, so we need to cache away the results
                    CType(NewEntity, IUpdatable).RawDataset = CType(entities, DataSet)
                End If
            End If
            'If Not entities.GetEntity(0).GetType.GetMethod("SaveChanges", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.DeclaredOnly) Is Nothing Then
            '    'the developer has overridden ther SaveChanges method, this means that he/she
            '    'may want to save, so we need to cache away the results
            'End If
            Return NewEntity
        End If

    End Function

    Public Function DeleteById(ByVal sqlDeleteName As String, ByVal idColumnName As String, ByVal id As Int32) As Boolean
        Return DeleteById(sqlDeleteName, idColumnName, id, 0, Nothing)
    End Function

    Public Function DeleteById(ByVal sqlDeleteName As String, ByVal idColumnName As String, ByVal id As Int32, ByVal checkSum As Int32) As Boolean
        Return DeleteById(sqlDeleteName, New String() {idColumnName}, New Int32() {id}, checkSum, Nothing)
    End Function

    Public Function DeleteById(ByVal sqlDeleteName As String, ByVal idColumnName As String, ByVal id As Int32, ByVal transaction As SqlTransaction) As Boolean
        Return DeleteById(sqlDeleteName, idColumnName, id, 0, transaction)
    End Function

    Public Function DeleteById(ByVal sqlDeleteName As String, ByVal idColumnName As String, ByVal id As Int32, ByVal checkSum As Int32, ByVal transaction As SqlTransaction) As Boolean
        Return DeleteById(sqlDeleteName, New String() {idColumnName}, New Int32() {id}, checkSum, transaction)
    End Function

    Public Function DeleteById(ByVal sqlDeleteName As String, ByVal idColumnName() As String, ByVal id() As Int32, ByVal checkSum As Int32) As Boolean
        Return DeleteById(sqlDeleteName, idColumnName, id, checkSum, Nothing)
    End Function

    '========================================================
    'DeleteById
    '--------------------------------------------------------
    'Purpose : Deletes a single record using the current data
    '
    'Author : Steven Sartain (x912595)
    '
    'Notes : 
    '--------------------------------------------------------
    'Parameters
    '----------
    'sqlDeleteName (String) : The name of the stored proc
    'idColumnName() (String) : the stored proc params
    'id() (Int32) : the stored proc values
    'checkSum (Int32) : For optional checksum comparison
    'transaction (SqlTransaction) : For optional transaction handling 
    '--------------------------------------------------------
    'Returns : Boolean
    '--------------------------------------------------------
    'Revision History
    '--------------------------------------------------------
    '20 Nov 2003 x912595 : Documented source.
    '========================================================  
    Public Function DeleteById(ByVal sqlDeleteName As String, ByVal idColumnName() As String, ByVal id() As Int32, ByVal checkSum As Int32, ByVal transaction As SqlTransaction) As Boolean

        ' create a command...
        Dim command As New SqlCommand(sqlDeleteName)
        command.CommandType = CommandType.StoredProcedure
        For i As Int32 = 0 To idColumnName.GetUpperBound(0)
            If Not idColumnName(i) Is Nothing AndAlso _
               Not idColumnName(i).StartsWith("@") Then
                idColumnName(i) = idColumnName(i).Insert(0, "@")
            End If
            command.Parameters.Add(idColumnName(i), id(i))
        Next i

        If checkSum <> 0 Then
            command.Parameters.Add("@checkSum", checkSum)
        End If

        ' do we have a connection?
        Dim connection As SqlConnection

        If Not transaction Is Nothing Then
            connection = transaction.Connection
            command.Transaction = transaction
            command.Connection = connection
        ElseIf command.Connection Is Nothing Then
            ' do we have a connection?
            connection = New SqlConnection(EnterpriseApplication.Application.ConnectionString)
            connection.Open()
            command.Connection = connection
        End If

        Dim param As SqlParameter = command.Parameters.Add("RETURN_VALUE", SqlDbType.Int)
        param.Direction = ParameterDirection.ReturnValue

        command.ExecuteNonQuery()
        Dim ReturnValue As Boolean
        ReturnValue = (CType(command.Parameters("RETURN_VALUE").Value, Int32) = 0)

        command.Dispose()

        ' return...
        Return ReturnValue

    End Function

    '========================================================
    'BeginTransaction
    '--------------------------------------------------------
    'Purpose : Starts a transaction and returns the transaction
    'that has just been started
    '
    'Author : Steven Sartain (x912595)
    '
    'Notes : 
    '--------------------------------------------------------
    'Parameters
    '----------
    'sqlDeleteName (String) : The name of the stored proc
    'idColumnName() (String) : the stored proc params
    'id() (Int32) : the stored proc values
    'checkSum (Int32) : For optional checksum comparison
    'transaction (SqlTransaction) : For optional transaction handling 
    '--------------------------------------------------------
    'Returns : SqlTransaction
    '--------------------------------------------------------
    'Revision History
    '--------------------------------------------------------
    '20 Nov 2003 x912595 : Documented source.
    '========================================================  
    Public Function BeginTransaction() As SqlTransaction
        Return BeginTransaction(IsolationLevel.ReadCommitted)
    End Function

    Public Function BeginTransaction(ByVal iso As Data.IsolationLevel) As SqlTransaction
        Return BeginTransaction(New SqlConnection(EnterpriseApplication.Application.ConnectionString), iso)
    End Function

    Public Function BeginTransaction(ByVal connection As SqlConnection) As SqlTransaction
        Return BeginTransaction(connection, IsolationLevel.ReadCommitted)
    End Function

    Public Function BeginTransaction(ByVal connection As SqlConnection, ByVal iso As Data.IsolationLevel) As SqlTransaction
        Dim Command As New SqlCommand
        If Not connection Is Nothing Then
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            Dim trans As SqlTransaction = connection.BeginTransaction(iso)
            Return trans
        End If
    End Function

    Public Enum TransactionEndEnum
        Commit
        Rollback
        DoNothing
    End Enum

    '========================================================
    'EndTransaction
    '--------------------------------------------------------
    'Purpose : Starts a transaction and returns the transaction
    'that has just been started
    '
    'Author : Steven Sartain (x912595)
    '
    'Notes : 
    '--------------------------------------------------------
    'Parameters
    '----------
    'tran (SqlTransaction) : The name of the stored proc
    '--------------------------------------------------------
    'Returns : SqlTransaction
    '--------------------------------------------------------
    'Revision History
    '--------------------------------------------------------
    '20 Nov 2003 x912595 : Documented source.
    '========================================================  
    Public Sub EndTransaction(ByVal tran As SqlTransaction)
        EndTransaction(tran, TransactionEndEnum.Commit)
    End Sub

    Public Sub EndTransaction(ByVal tran As SqlTransaction, ByVal endMethod As TransactionEndEnum)
        EndTransaction(tran, endMethod, True)
    End Sub

    Public Sub EndTransaction(ByVal tran As SqlTransaction, ByVal closeConnection As Boolean)
        EndTransaction(tran, TransactionEndEnum.Commit, closeConnection)
    End Sub

    Public Sub EndTransaction(ByVal tran As SqlTransaction, ByVal endMethod As TransactionEndEnum, ByVal closeTransaction As Boolean)
        If Not tran Is Nothing Then
            With tran
                Dim conn As SqlClient.SqlConnection
                If closeTransaction Then conn = .Connection
                Try
                    Select Case endMethod
                        Case TransactionEndEnum.Commit
                            .Commit()
                        Case TransactionEndEnum.Rollback
                            .Rollback()
                        Case TransactionEndEnum.DoNothing
                            '.Connection.Close()
                            '.Connection.Dispose()
                    End Select
                    If closeTransaction Then
                        If conn.State <> ConnectionState.Closed Then
                            conn.Close()
                        End If
                        conn.Dispose()
                        conn = Nothing
                    End If
                Catch ex As Exception
                End Try
            End With
            tran = Nothing
        End If
    End Sub

    Public Function GetDataSet(ByVal commandText As String) As DataSet

        ' create a command...
        Dim command As New SqlCommand(commandText)
        Dim dataset As dataset = GetDataSet(command)
        command.Dispose()

        ' return...
        Return dataset

    End Function

    Public Function GetDataSet(ByVal command As SqlCommand) As DataSet

        ' connect...
        ' do we have a connection?
        Dim connection As SqlConnection
        If command.Connection Is Nothing Then
            connection = New SqlConnection(EnterpriseApplication.Application.ConnectionString)
            connection.Open()
            command.Connection = connection
        End If

        ' run...
        Dim dataset As New dataset
        Dim adapter As New SqlDataAdapter(command)
        adapter.Fill(dataset)
        adapter.Dispose()

        ' close the connection...
        If Not connection Is Nothing Then
            connection.Close()
        End If

        ' return...
        Return dataset

    End Function

    Public Function GetLatestTimestamp(ByVal tableName As String, ByVal idColumnName As String, ByVal id As Integer) As Byte()

        ' changed?
        Dim latest As Byte()

        ' get the latest version...
        Dim connection As New SqlConnection(EnterpriseApplication.Application.ConnectionString)
        connection.Open()
        Dim command As New SqlCommand("select timestamp from " & tableName & " where " & idColumnName & "=" & id.ToString, connection)
        Dim reader As SqlDataReader = command.ExecuteReader()
        If reader.Read() = True Then
            latest = CType(reader(0), Byte())
        End If

        ' close...
        reader.Close()
        connection.Close()

        ' return...
        Return latest

    End Function

    ' Thanks to Sam Liu for the bug fix
    Public Function HasChanged(ByVal timestamp1 As Byte(), ByVal timestamp2 As Byte()) As Boolean

        ' compare...
        Dim result As SqlBoolean = SqlBinary.Equals(SqlBinary.op_Implicit(timestamp1), SqlBinary.op_Implicit(timestamp2))
        Return Not result.Value

    End Function

    Public Function GetSecurityToken() As String

        ' get the data from the call context...
        Dim token As ContextToken = CType(CallContext.GetData(EnterpriseApplication.SecurityTokenSlotName), ContextToken)
        If Not token Is Nothing Then
            Return token.Token
        End If

        ' return nothing...
        Return Nothing

    End Function

    <Conditional("DEBUG")> Public Sub DebugSecurityToken()
        DebugSecurityToken(False)
    End Sub

    <Conditional("DEBUG")> Public Sub DebugSecurityToken(ByVal showExtendedData As Boolean)

        ' render...
        Console.WriteLine("--------------------------------------------------------------------")
        Console.Write("Security token for thread #{0}: ", Thread.CurrentThread.GetHashCode())
        Dim contextToken As String = GetSecurityToken()
        If Not contextToken Is Nothing Then

            ' write the token...
            Console.WriteLine(contextToken)

            ' do we want extended info?
            If showExtendedData = True Then

                ' load the token...
                Dim token As token = token.Load(contextToken)
                If Not token Is Nothing Then
                    Console.WriteLine("Token ID: {0}", token.Id.ToString())
                    Console.WriteLine("User ID: {0}", token.UserId.ToString())
                    Console.WriteLine("NTLM name: {0}", token.NtlmName)
                Else
                    Console.WriteLine("INVALID TOKEN!")
                End If

            End If

        Else
            Console.WriteLine("NOT FOUND!")
        End If
        Console.WriteLine("--------------------------------------------------------------------")

    End Sub

    Public Overridable Sub CreateCounters(ByVal counters As EnterpriseObjects.EnterpriseCounters) Implements EnterpriseObjects.ICounterProvider.CreateCounters

        ' create the counters...
        counters.Counters.Add(New EnterpriseCounter(NumServiceObjectsCounterName, "Number of open service objects", PerformanceCounterType.NumberOfItems32))
        counters.Counters.Add(New EnterpriseCounter(GetEntitySetRateCounterName, "Number of GetEntitySet calls per second", PerformanceCounterType.RateOfCountsPerSecond32))
        counters.Counters.Add(New EnterpriseCounter(GetAllRateCounterName, "Number of GetAll calls per second", PerformanceCounterType.RateOfCountsPerSecond32))
        counters.Counters.Add(New EnterpriseCounter(GetByIdRateCounterName, "Number of GetById calls per second", PerformanceCounterType.RateOfCountsPerSecond32))

    End Sub

    Public Overridable Sub CountersCreated(ByVal counters As EnterpriseObjects.EnterpriseCounters) Implements EnterpriseObjects.ICounterProvider.CountersCreated

        ' save them...
        _numServiceObjectsCounter = counters.Counters.Find(Me.NumServiceObjectsCounterName).Counter
        _getEntitySetRateCounter = counters.Counters.Find(Me.GetEntitySetRateCounterName).Counter
        _getAllRateCounter = counters.Counters.Find(Me.GetAllRateCounterName).Counter
        _getByIdRateCounter = counters.Counters.Find(Me.GetByIdRateCounterName).Counter

    End Sub

    Protected Overridable ReadOnly Property HasStates() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Class SQLError
        Public Enum ErrorList
            Update_ConcurrencyViolation = 1
            Update_DataMatch = 2
            Update_DataMismatch = 3
            Update_TooManyRowsUpdated = 4
            Uknown
        End Enum

        Public Sub New()
        End Sub

        Public Sub New(ByVal procedure As String, ByVal errorNumber As ErrorList, ByVal errorMessage As String)
            SetupError(procedure, errorNumber, errormessage)
        End Sub

        Private Sub SetupError(ByVal procedure As String, ByVal errorNumber As ErrorList, ByVal errorMessage As String)
            Me.Procedure = procedure
            Me.ErrorNumber = errorNumber
            Me.ErrorMessage = errorMessage
        End Sub

        Public Sub New(ByVal procedure As String, ByVal errorNumber As Int32, ByVal errorMessage As String)
            SQLErrorNumber = errorNumber
            If System.Enum.GetName(GetType(Service.ErrorList), errorNumber) Is Nothing Then
                SetupError(procedure, ErrorList.Uknown, errorMessage)
            Else
                SetupError(procedure, CType(System.Enum.Parse(GetType(Service.ErrorList), System.Enum.GetName(GetType(Service.ErrorList), errorNumber)), ErrorList), errorMessage)
            End If
        End Sub

        Public Procedure As String
        Public ErrorNumber As ErrorList
        Public ErrorMessage As String
        Public SQLErrorNumber As Int32
    End Class
End Class

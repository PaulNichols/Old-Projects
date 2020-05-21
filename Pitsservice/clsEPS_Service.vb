'Public Class clsEPS_Service
<System.Web.Services.WebService(Namespace:="http://www.Accuride.com/")> _
Public Class EPS_Service
    Inherits System.Web.Services.WebService
    Implements IEPS_Service

    <System.Web.Services.WebMethod(Description:="Assigns an alternative database to use.")> _
    Public Sub SetDatabaseName(ByVal DBName As String)
        'Application(String.Concat("DatabaseName", IPAddress)) = "PITS"
        If DBName Is Nothing OrElse DBName.Length = 0 Then
            Application(String.Concat("DatabaseName", IPAddress)) = "EPS"
        Else
            Application(String.Concat("DatabaseName", IPAddress)) = DBName
        End If
    End Sub

    <System.Web.Services.WebMethod(Description:="Retrieves the database.")> _
    Public Function GetDatabaseName() As String
        'Return ConfigFile
        If Application(String.Concat("DatabaseName", IPAddress)) Is Nothing Then
            If Application("DatabaseName") Is Nothing Then
                Return ""
            Else
                'use local version
                Return Application("DatabaseName").ToString
            End If
        Else
            Return Application(String.Concat("DatabaseName", IPAddress)).ToString
        End If
    End Function

    <System.Web.Services.WebMethod(Description:="Clears database for IP address of user.")> _
    Public Function ClearDatabaseName() As String
        If Not Application(String.Concat("DatabaseName", IPAddress)) Is Nothing Then
            Application(String.Concat("DatabaseName", IPAddress)) = Nothing
        End If
    End Function

    Friend ReadOnly Property ConfigFile() As String
        Get
            'Return String.Concat("http://", Server.MachineName, Context.Request.ApplicationPath, "/EPSServerConfig.xml")
            Return String.Concat("http://", Server.MachineName, "/PITSSERVICE/EPSServerConfig.xml")
        End Get
    End Property

    Friend Function GetConfigFile() As System.Xml.XmlDocument
        Dim Document As New System.Xml.XmlDocument
        Document.Load(ConfigFile)
        Return Document
    End Function

    Friend Function IPAddress() As String
        Return String.Concat("IP_", Context.Request.UserHostAddress)
    End Function

    Private Property DataRead() As Boolean
        Get
            If Application("DataRead") Is Nothing Then
                Return False
            Else
                Return CType(Application("DataRead"), Boolean)
            End If
        End Get
        Set(ByVal Value As Boolean)
            Application("DataRead") = Value
        End Set
    End Property

    <System.Web.Services.WebMethod(Description:="Used by EPS reports.")> _
Public Function GetServerName() As String
        Return ServerName
    End Function

    Public Property ServerName() As String
        Get
            Return Application("ServerName").ToString
        End Get
        Set(ByVal Value As String)
            If Value Is Nothing OrElse Value.Length = 0 Then
                Application("ServerName") = "UKSQL3"
            Else
                Application("ServerName") = Value
            End If
        End Set
    End Property

    <System.Web.Services.WebMethod(Description:="Used by EPS reports.")> _
    Public Function GetDatabaseUserName() As String
        Return DatabaseUserName
    End Function

    Public Property DatabaseUserName() As String
        Get
            Return Application("DatabaseUserName").ToString
        End Get
        Set(ByVal Value As String)
            If Value Is Nothing OrElse Value.Length = 0 Then
                Application("DatabaseUserName") = ""
            Else
                Application("DatabaseUserName") = Value
            End If
        End Set
    End Property

    <System.Web.Services.WebMethod(Description:="Used by EPS reports.")> _
   Public Function GetDatabasePassword() As String
        Return DatabasePassword
    End Function

    Public Property DatabasePassword() As String
        Get
            Return Application("DatabasePassword").ToString
        End Get
        Set(ByVal Value As String)
            If Value Is Nothing OrElse Value.Length = 0 Then
                Application("DatabasePassword") = ""
            Else
                Application("DatabasePassword") = Value
            End If
        End Set
    End Property

    <System.Web.Services.WebMethod(Description:="Used by EPS reports.")> _
       Public Function ConnectionString() As String
        Return strConnectionString
    End Function

    Public ReadOnly Property strConnectionString() As String
        Get
            If Not DataRead Then
                With GetConfigFile.ChildNodes(0).ChildNodes(0)
                    ServerName = .Attributes("Server").Value
                    Application("DatabaseName") = .Attributes("DatabaseName").Value
                    DatabaseUserName = .Attributes("UserName").Value
                    Dim Decryptor As New Crypt(Crypt.SymmProvEnum.DES)
                    DatabasePassword = Decryptor.Decrypting(.Attributes("Password").Value.ToString, "Iridium")
                    Decryptor = Nothing
                End With
                DataRead = True
            End If
            Return "data source=" & ServerName & _
                           ";initial catalog=" & GetDatabaseName() & _
                           ";persist security info=False;packet size=8192" & _
                           ";user id=" & DatabaseUserName & _
                           ";password=" & DatabasePassword
            '                               ";integrated security=SSPI;persist security info=False;packet size=8192" & _
        End Get
    End Property

    'Friend ReadOnly Property strConnectionString2() As String
    '    Get
    '        Return "data source=" & pstrServerName & _
    '               ";initial catalog=" & pstrDatabaseName & _
    '               ";integrated security=SSPI;persist security info=False;packet size=16384" & _
    '               ";user id=" & pstrDatabaseUserName & _
    '               ";password=" & pstrDatabasePassword
    '        '512,4096,8192,,3332767
    '    End Get
    'End Property
    'Private Const SQLSERVER As String = "UKSQL3" '- "UKSQL3" '
    'Public Const strCONNECTIONSTRING As String = "data source=" & SQLSERVER & ";initial catalog=Pits;integrated security=SSPI;persist security info=False;packet size=4096;user id=sa;password="

    Friend Fields As Collections.Hashtable

    Public Overridable Sub AssignFields()
    End Sub

    Friend Structure FieldInfoStructure
        Friend FieldName As String
        Friend IsNullable As Boolean
        Friend MaxLength As Int32
        Friend Calculated As Boolean
        Friend DefaultValue As Object
        Friend CalculatedText As String
        Friend Sub New(ByVal pFieldName As String, ByVal pMaxLength As Int32, Optional ByVal pIsNullable As Boolean = True, Optional ByVal pCalculated As Boolean = False, Optional ByVal pCalculatedText As String = "", Optional ByVal pDefaultValue As Object = Nothing)
            FieldName = pFieldName
            IsNullable = pIsNullable
            MaxLength = pMaxLength
            Calculated = pCalculated
            DefaultValue = pDefaultValue
            CalculatedText = pCalculatedText
        End Sub
    End Structure
    Private mblnAttemptedAssign As Boolean = False
    Private Sub AttemptedAssign()
        'this will only be called when the info is requested by the error handler
        Fields = New Collections.Hashtable
        AssignFields()
        mblnAttemptedAssign = True
    End Sub

    <System.Web.Services.WebMethodAttribute()> _
    Public Function GetLanguageString(ByVal displayId As Int32, ByVal accurideLocationId As Int32, ByVal uniqueId As Int32) As String
        Dim ReturnValue As String = ""
        Dim sqlConnection As New SqlClient.SqlConnection(strConnectionString)
        Try
            sqlConnection.Open()
        Catch
            Return ReturnValue
        Finally
            Dim SelectFunction As New SqlClient.SqlCommand("select dbo.GiveLanguageString (" & displayId & "," & accurideLocationId & "," & uniqueId & ")", sqlConnection)
            ReturnValue = SelectFunction.ExecuteScalar().ToString
            SelectFunction = Nothing
        End Try
        sqlConnection = Nothing
        Return ReturnValue
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function GetPrimaryKeyColumns() As String
        Dim udtDS As New DataSet
        Dim sqlConnection As New SqlClient.SqlConnection(strConnectionString)
        Dim strResult As String = ""
        If CType(Me, IEPS_Service_Data).TableName Is Nothing Then
            Return ""
        End If
        Try
            sqlConnection.Open()
        Catch
            Return ""
        Finally
            Dim udtSelect As New SqlClient.SqlCommand("sp_pkeys", sqlConnection)
            Dim udtDA As New SqlClient.SqlDataAdapter(udtSelect)
            With udtSelect
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@table_name", CType(Me, IEPS_Service_Data).TableName)
                udtDA.Fill(udtDS)
                Dim udtRow As DataRow
                For Each udtRow In udtDS.Tables(0).Rows
                    If strResult.Length > 0 Then strResult &= ","
                    strResult &= udtRow.Item("Column_Name").ToString
                Next udtRow
                udtRow = Nothing
            End With
            udtDA = Nothing
            udtSelect = Nothing
        End Try
        Return strResult
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function GetCalculatedText(ByVal pstrFieldName As String) As String
        If Not mblnAttemptedAssign Then AttemptedAssign()
        If Fields.ContainsKey(pstrFieldName) Then
            Return CType(Fields(pstrFieldName), FieldInfoStructure).CalculatedText
        Else
            Return ""
        End If
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function GetMaxLength(ByVal pstrFieldName As String) As Int32
        If Not mblnAttemptedAssign Then AttemptedAssign()
        If Fields.ContainsKey(pstrFieldName) Then
            Return CType(Fields(pstrFieldName), FieldInfoStructure).MaxLength
        Else
            Return 0
        End If
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function GetDefaultValue(ByVal pstrFieldName As String) As Object
        If Not mblnAttemptedAssign Then AttemptedAssign()
        If Fields.ContainsKey(pstrFieldName) Then
            Return CType(Fields(pstrFieldName), FieldInfoStructure).DefaultValue
        Else
            Return Nothing
        End If
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function IsCalculated(ByVal pstrFieldName As String) As Boolean
        If Not mblnAttemptedAssign Then AttemptedAssign()
        If Fields.ContainsKey(pstrFieldName) Then
            Return CType(Fields(pstrFieldName), FieldInfoStructure).Calculated
        Else
            Return False
        End If
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function IsNullable(ByVal pstrFieldName As String) As Boolean
        If Not mblnAttemptedAssign Then AttemptedAssign()
        If Fields.ContainsKey(pstrFieldName) Then
            Return CType(Fields(pstrFieldName), FieldInfoStructure).IsNullable
        Else
            Return False
        End If
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function GetFieldInfoStructure() As DataSet
        If Not mblnAttemptedAssign Then AttemptedAssign()
        Dim udtTable As New DataTable("FieldInfoStructure")

        With udtTable.Columns
            Dim udtColumn As New DataColumn("FieldName", GetType(String))
            .Add(udtColumn)
            udtTable.PrimaryKey = New DataColumn() {udtColumn}
            udtColumn = Nothing
            .Add("IsNullable", GetType(Boolean))
            .Add("MaxLength", GetType(Int32))
            .Add("Calculated", GetType(Boolean))
            .Add("DefaultValue", GetType(String))
            .Add("CalculatedText", GetType(String))
        End With
        Dim strValues As Collections.ICollection = Fields.Values
        Dim strKey As String
        Dim udtEnum As Collections.IEnumerator
        Dim udtDataRow As DataRow
        udtEnum = strValues.GetEnumerator
        Do While udtEnum.MoveNext
            udtDataRow = udtTable.NewRow
            With CType(udtEnum.Current, FieldInfoStructure)
                udtDataRow.Item("FieldName") = .FieldName
                udtDataRow.Item("IsNullable") = .IsNullable
                udtDataRow.Item("MaxLength") = .MaxLength
                udtDataRow.Item("Calculated") = .Calculated
                udtDataRow.Item("DefaultValue") = .DefaultValue
                udtDataRow.Item("CalculatedText") = .CalculatedText
            End With
            udtTable.Rows.Add(udtDataRow)
        Loop
        udtDataRow = Nothing
        udtEnum = Nothing
        Dim udtDS As New DataSet
        udtDS.Tables.Add(udtTable)
        Return udtDS
    End Function

    Private mudtError As ErrorDS_DataTable


    Public Function LoadDataSet(ByVal udtDataAdapter As System.Data.SqlClient.SqlDataAdapter) As DataSet
        Dim udtReturnDataset As New DataSet
        udtDataAdapter.Fill(udtReturnDataset)
        Try
            If Not (CType(udtDataAdapter.SelectCommand.Parameters("@LoadCombo").Value, Int16) = 1) Then
                udtReturnDataset.Merge(GetFieldInfoStructure.Tables(0))
            End If
        Catch
            udtReturnDataset.Merge(GetFieldInfoStructure.Tables(0))
        End Try
        Return udtReturnDataset
    End Function



    Public Function UpdateDataset(ByVal pudtDatasetToUpdate As DataSet, ByVal pudtDataAdapter As System.Data.SqlClient.SqlDataAdapter) As DataSet
        Dim intRet As Int32
        If (pudtDatasetToUpdate Is Nothing) Then Return Nothing

        mudtError = New ErrorDS_DataTable
        If pudtDatasetToUpdate.Tables.IndexOf("ErrorTable") <> -1 Then
            pudtDatasetToUpdate.Tables.Remove("ErrorTable")
        End If

        Try
            intRet = pudtDataAdapter.Update(pudtDatasetToUpdate)
            'If pudtDatasetToUpdate.Tables(0).Rows.Count <> intRet Then
            'Throw New System.Exception("iridium")
            'End If
        Catch myException As System.Data.SqlClient.SqlException
            Select Case myException.Number
                Case 515 'we've found a null error


                    Dim udtRow As DataRow
                    Dim udtDataColumn As DataColumn
                    If Not mblnAttemptedAssign Then AttemptedAssign()

                    For Each udtDataColumn In pudtDatasetToUpdate.Tables(0).Columns

                        For Each udtRow In pudtDatasetToUpdate.Tables(0).Rows
                            If Not IsNullable(udtDataColumn.ColumnName) And Convert.IsDBNull(udtRow.Item(udtDataColumn)) Then
                                'not found so add
                                mudtError.Rows.Add(New Object() {1, myException.Number, udtDataColumn.ColumnName.ToString})
                                'drop out of the for loop as we don't need to check the other rows
                                Exit For
                            End If
                        Next udtRow
                    Next udtDataColumn

                    Dim x As New DataSet
                    x.Tables.Add(mudtError)
                    Return x
                Case 2627 'unique key violation
                    Dim intStart As Int32 = myException.Message.IndexOf("'") + 1
                    If intStart > 0 Then
                        Dim strIndexName As String
                        strIndexName = myException.Message.Substring(intStart, myException.Message.IndexOf("'", intStart) - intStart)
                        'pudtDataAdapter.SelectCommand.Connection()
                        'Dim udtSQLConnection As New System.Data.SqlClient.SqlConnection(strCONNECTIONSTRING)
                        Try
                            pudtDataAdapter.SelectCommand.Connection.Open() 'udtSQLConnection.Open()
                        Catch
                            Throw myException
                        Finally

                            Dim udtSelect As New System.Data.SqlClient.SqlCommand("GetIndexColumns", pudtDataAdapter.SelectCommand.Connection) 'udtSQLConnection)
                            Dim udtDataSet As New DataSet
                            With udtSelect
                                .CommandType = CommandType.StoredProcedure
                                .Parameters.Add("@IndexName", strIndexName)
                            End With
                            Dim udtDataAdapter As New System.Data.SqlClient.SqlDataAdapter(udtSelect)
                            Try
                                udtDataAdapter.Fill(udtDataSet)
                            Catch
                                Throw myException
                            End Try
                            If Not udtDataSet Is Nothing Then
                                If udtDataSet.Tables(0).Rows.Count > 0 Then
                                    '                                   s = "Unique constraints have been violated on the following field(s):" & ControlChars.Cr
                                    Dim udtRow As DataRow
                                    For Each udtRow In udtDataSet.Tables(0).Rows
                                        mudtError.Rows.Add(New Object() {2, myException.Number, udtRow(0).ToString})
                                    Next udtrow
                                End If
                            End If
                            udtDataSet = Nothing
                            udtSelect = Nothing
                        End Try
                    Else
                        'don't know what to do so throw it
                        Throw myException
                    End If

                Case 547 'foreign key violation
                    Dim intStart As Int32 = myException.Message.IndexOf("'") + 1
                    If intStart > 0 Then
                        Dim strFKName As String
                        strFKName = myException.Message.Substring(intStart, myException.Message.IndexOf("'", intStart) - intStart)

                        'Dim udtSQLConnection As New System.Data.SqlClient.SqlConnection(strCONNECTIONSTRING)
                        Try
                            pudtDataAdapter.SelectCommand.Connection.Open()
                        Catch
                            Throw myException
                        Finally

                            Dim udtSelect As New System.Data.SqlClient.SqlCommand("GetForeignKeyInfo", pudtDataAdapter.SelectCommand.Connection)
                            Dim udtDataSet As New DataSet
                            With udtSelect
                                .CommandType = CommandType.StoredProcedure
                                .Parameters.Add("@ForeignKeyName", strFKName)
                            End With
                            Dim udtDataAdapter As New System.Data.SqlClient.SqlDataAdapter(udtSelect)
                            udtDataAdapter.Fill(udtDataSet)
                            If Not udtDataSet Is Nothing Then
                                Dim udtDataTable As DataTable
                                's = "Table constraints have been violated on the following tables/field(s):" & ControlChars.Cr
                                For Each udtDataTable In udtDataSet.tables
                                    With udtDataTable.Rows(0)
                                        mudtError.Rows.Add(New Object() {3, myException.Number, .Item("RKTable_Name").ToString, .Item("FKTable_Name").ToString, .Item("RKColumn_Name").ToString, .Item("FKColumn_Name").ToString})
                                        's &= .Item("RKTable_Name") & ControlChars.Cr & _
                                        '    .Item("FKTable_Name") & ControlChars.Cr & _
                                        '    .Item("RKColumn_Name") & ControlChars.Cr & _
                                        '    .Item("FKColumn_Name") & ControlChars.Cr
                                    End With
                                Next udtDataTable
                                udtDataTable = Nothing
                            End If
                            udtDataSet = Nothing
                            udtSelect = Nothing
                        End Try
                    Else
                        'don't know what to do so throw it
                        Throw myException
                    End If

                Case Else
                    '                    Dim i As Int32
                    'For i = 0 To myException.Errors.Count - 1
                    mudtError.Rows.Add(New Object() {500, myException.Number, myException.Message})

                    's &= (("Index #" & i & ControlChars.Cr & _
                    '                 "Source: " & myException.Errors(i).Source & ControlChars.CrLf & _
                    '                 "Number: " & myException.Errors(i).Number.ToString() & ControlChars.CrLf & _
                    '                 "State: " & myException.Errors(i).State.ToString() & ControlChars.CrLf & _
                    '                 "Class: " & myException.Errors(i).Class.ToString() & ControlChars.CrLf & _
                    '                 "Server: " & myException.Errors(i).Server & ControlChars.CrLf & _
                    '                 "Message: " & myException.Errors(i).Message & ControlChars.CrLf & _
                    '                 "Procedure: " & myException.Errors(i).Procedure & ControlChars.CrLf & _
                    '                 "LineNumber: " & myException.Errors(i).LineNumber.ToString())) & ControlChars.CrLf
                    'Next i
                    'Return myException.Message
            End Select
        Catch errDB As DBConcurrencyException
            'no records added
            mudtError.Rows.Add(New Object() {501, 0, errDB.Message})
        Catch e As System.Exception
            '            If e.Message.ToLower = "iridium" Then
            '            mudtError.Rows.Add(New Object() {4, intRet.ToString, "Records Affected"})
            '            Else
            mudtError.Rows.Add(New Object() {502, 0, e.Message, e.ToString})
            '            End If
        End Try
        If mudtError.Rows.Count > 0 Then
            pudtDatasetToUpdate.Tables.Clear()
            pudtDatasetToUpdate.Tables.Add(mudtError)
        End If
        Return pudtDatasetToUpdate
    End Function

    Public Function ErrorCollection() As DataSet Implements IEPS_Service.ErrorCollection
        Dim udtDS As New DataSet
        If Not mudtError Is Nothing Then
            udtDS.Tables.Add(mudtError)
        End If
        Return udtDS
    End Function

    Public Function HasErrors() As Boolean Implements IEPS_Service.HasErrors
        If mudtError Is Nothing Then
            Return False
        Else
            Return (mudtError.Rows.Count > 0)
        End If
    End Function


End Class

Public Interface IEPS_Service
    Function HasErrors() As Boolean
    Function ErrorCollection() As DataSet
End Interface

Public Interface IEPS_Service_Data
    <System.Web.Services.WebMethod()> _
    Function LoadData(ByVal pudtParams() As Object) As DataSet
    <System.Web.Services.WebMethod()> _
    Function UpdateData(ByVal Changes As DataSet) As DataSet
    <System.Web.Services.WebMethodAttribute(CacheDuration:=600)> _
    Function LoadCombo(ByVal pintUniqueId As Int32, ByVal pintAccurideLocationId As Int32) As DataSet
    ReadOnly Property TableName() As String
End Interface

Friend Class ErrorDS_DataTable
    Inherits DataTable

    Public Sub New()
        Me.TableName = "ErrorTable"
        Me.Columns.Add(New DataColumn("ErrorNumber", GetType(System.Int32)))
        Me.Columns.Add(New DataColumn("SQLErrorNumber", GetType(System.Int32)))
        Me.Columns.Add(New DataColumn("Data_1", GetType(System.String)))
        Me.Columns.Add(New DataColumn("Data_2", GetType(System.String)))
        Me.Columns.Add(New DataColumn("Data_3", GetType(System.String)))
        Me.Columns.Add(New DataColumn("Data_4", GetType(System.String)))
    End Sub
End Class

Class clsConnectionString

End Class
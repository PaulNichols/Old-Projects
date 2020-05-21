Imports System
Imports System.IO
Imports System.Text
Imports System.Threading
Imports System.Security
Imports System.Security.Cryptography

Public Class Token

    ' members...
    Private _userId As Integer
    Private _ntlmName As String
    Private _tokenString As String
    Private _tokenId As Integer = 0
    Private _ordinal As Integer
    Private _lock As New ReaderWriterLock()

    ' const...
    Private Const EncryptionKey As String = "helloworld"
    Private Const ExpiryTimeout As Integer = 20

    Private Sub New(ByVal dataset As DataSet)

        ' set it...
        With dataset.Tables(0).Rows(0)
            _tokenId = CType(.Item("tokenid"), Integer)
            _userId = CType(.Item("userid"), Integer)
            _tokenString = .Item("token").ToString
            _ntlmName = .Item("ntlmname").ToString
        End With

        ' update the expiry time...
        UpdateExpiryTime()

    End Sub

    Public Sub New(ByVal userId As Integer)

        ' set...
        _userId = userId

        ' get an ordinal...
        Dim useOrdinal As Integer = 0
        _lock.AcquireWriterLock(-1)
        useOrdinal = _ordinal
        _ordinal += 1
        _lock.ReleaseWriterLock()

        ' create a complex string based on a combination of factors...
        Dim rawToken As String = DateTime.Now.ToString("yyyyMMddHHmmss") & "_" & userId & "_" & useOrdinal

        ' initialize the key...
        Dim keyBytes(EncryptionKey.Length - 1) As Byte
        Dim encoding As New ASCIIEncoding()
        encoding.GetBytes(EncryptionKey, 0, EncryptionKey.Length, keyBytes, 0)

        ' create the new cryptographic service provider...
        Dim provider As New SHA1CryptoServiceProvider()

        ' create a hash of the key... once we have the hash, we need a key and an initialization vector, each 
        ' eight bytes in length.
        Dim hash As Byte() = provider.ComputeHash(keyBytes)
        Dim n As Integer, key(7) As Byte, vector(7) As Byte
        For n = 0 To 7
            key(n) = hash(n)
            vector(n) = hash(n + 8)
        Next

        ' now we can start the actual encryption process.  first, we need to translate the
        ' string into an array of bytes...
        Dim tokenBytes(rawToken.Length - 1) As Byte
        encoding.GetBytes(rawToken, 0, rawToken.Length, tokenBytes, 0)

        ' now, we need a DES service provide and create an object
        ' that will perform the encyrption...
        Dim desProvider As New DESCryptoServiceProvider()
        Dim encrypt As ICryptoTransform = desProvider.CreateEncryptor(key, vector)

        ' we need an input stream, an output stream and a cryptostream. This last
        ' one reads the bytes from the encryptor...
        Dim inStream As New MemoryStream(tokenBytes)
        Dim outStream As New MemoryStream()
        Dim cryptStream As New CryptoStream(inStream, encrypt, CryptoStreamMode.Read)

        ' encrypt...
        Dim buf(1024) As Byte
        Do While True
            Dim bytesRead As Integer = cryptStream.Read(buf, 0, 1024)
            If bytesRead = 0 Then
                Exit Do
            End If
            outStream.Write(buf, 0, bytesRead)
        Loop

        ' finally, if we got some data, we need to convert it to a unicode string...
        If outStream.Length <> 0 Then
            _tokenString = Convert.ToBase64String(outStream.GetBuffer(), 0, CType(outStream.Length, Integer))
        Else
            Throw New Exception("Encryption of security token failed")
        End If

    End Sub

    ' CreateExpiryTime - set the next expiry of the token...
    Private Function CreateExpiryTime() As DateTime
        Return Date.Now.AddMinutes(ExpiryTimeout)
    End Function

    ' Save - save the token to the database...
    Public Sub Save()

        ' save it...
        _tokenId = SprocCreateSecurityToken(UserId, Token, CreateExpiryTime())

    End Sub

    Public Overrides Function ToString() As String
        Return Token
    End Function

    Public ReadOnly Property Id() As Integer
        Get
            Return _tokenId
        End Get
    End Property

    Public ReadOnly Property UserId() As Integer
        Get
            Return _userId
        End Get
    End Property

    Public ReadOnly Property NtlmName() As String
        Get
            Return _ntlmName
        End Get
    End Property

    Public ReadOnly Property Token() As String
        Get
            Return _tokenString
        End Get
    End Property

    Protected Shared Function SprocCreateSecurityToken(ByVal userId As Integer, ByVal token As String, ByVal expires As System.DateTime) As Integer

        ' create a connection...
        Dim connection As New System.Data.SqlClient.SqlConnection(EnterpriseObjects.EnterpriseApplication.Application.ConnectionString)
        connection.Open()
        ' create a command...
        Dim command As New System.Data.SqlClient.SqlCommand("CreateSecurityToken", connection)
        command.CommandType = System.Data.CommandType.StoredProcedure
        ' parameters...
        Dim userIdParam As SqlClient.SqlParameter = command.Parameters.Add("@userId", System.Data.SqlDbType.Int)
        userIdParam.Value = userId
        Dim tokenParam As System.Data.SqlClient.SqlParameter = command.Parameters.Add("@token", System.Data.SqlDbType.VarChar, 256)
        tokenParam.Value = token
        Dim expiresParam As System.Data.SqlClient.SqlParameter = command.Parameters.Add("@expires", System.Data.SqlDbType.DateTime)
        expiresParam.Value = expires
        Dim returnValueParam As System.Data.SqlClient.SqlParameter = command.Parameters.Add("@returnValueParam", System.Data.SqlDbType.Int)
        returnValueParam.Direction = System.Data.ParameterDirection.ReturnValue
        ' execute...
        command.ExecuteNonQuery()
        ' cleanup...
        command.Dispose()
        connection.Close()
        ' return...
        Return CInt(returnValueParam.Value)

    End Function

    ' Load - load the token when given a string...
    Public Shared Function Load(ByVal tokenString As String) As Token

        ' try and find the token...
        Dim dataset As DataSet = SprocGetToken(tokenString)
        If dataset.Tables(0).Rows.Count > 0 Then

            ' create the new token...
            Dim token As New Token(dataset)
            Return token

        Else
            Return Nothing
        End If

    End Function

    Protected Shared Function SprocGetToken(ByVal token As String) As DataSet

        ' create a connection...
        Dim connection As New System.Data.SqlClient.SqlConnection(EnterpriseObjects.EnterpriseApplication.Application.ConnectionString)
        connection.Open()
        ' create a command...
        Dim command As New System.Data.SqlClient.SqlCommand("GetToken", connection)
        command.CommandType = System.Data.CommandType.StoredProcedure
        ' parameters...
        Dim tokenParam As System.Data.SqlClient.SqlParameter = command.Parameters.Add("@token", System.Data.SqlDbType.VarChar, 256)
        tokenParam.Value = token
        ' extract the dataset...
        Dim adapter As New System.Data.SqlClient.SqlDataAdapter(command)
        Dim dataset As New DataSet()
        adapter.Fill(dataset)
        adapter.Dispose()
        ' cleanup...
        command.Dispose()
        connection.Close()
        ' return dataset...
        Return dataset

    End Function

    Public Sub UpdateExpiryTime()

        ' call a sproc...
        SprocSetTokenExpires(Id, CreateExpiryTime())

    End Sub

    Protected Shared Sub SprocSetTokenExpires(ByVal tokenId As Integer, ByVal expires As System.DateTime)

        ' create a connection...
        Dim connection As New System.Data.SqlClient.SqlConnection(EnterpriseObjects.EnterpriseApplication.Application.ConnectionString)
        connection.Open()
        ' create a command...
        Dim Command As New System.Data.SqlClient.SqlCommand("SetTokenExpires", connection)
        Command.CommandType = System.Data.CommandType.StoredProcedure
        ' parameters...
        Dim tokenIdParam As System.Data.SqlClient.SqlParameter = Command.Parameters.Add("@tokenId", System.Data.SqlDbType.Int)
        tokenIdParam.Value = tokenId
        Dim expiresParam As System.Data.SqlClient.SqlParameter = Command.Parameters.Add("@expires", System.Data.SqlDbType.DateTime)
        expiresParam.Value = expires
        ' execute...
        Command.ExecuteNonQuery()
        ' cleanup...
        Command.Dispose()
        connection.Close()

    End Sub

    Public Function HasExpired() As Boolean
        Return False
    End Function

    Public Sub Delete()
        SprocDeleteToken(Id)
    End Sub

    Protected Shared Sub SprocDeleteToken(ByVal tokenId As Integer)

        ' create a connection...
        Dim connection As New System.Data.SqlClient.SqlConnection(EnterpriseObjects.EnterpriseApplication.Application.ConnectionString)
        connection.Open()
        ' create a command...
        Dim Command As New System.Data.SqlClient.SqlCommand("DeleteToken", connection)
        Command.CommandType = System.Data.CommandType.StoredProcedure
        ' parameters...
        Dim tokenIdParam As System.Data.SqlClient.SqlParameter = Command.Parameters.Add("@tokenId", System.Data.SqlDbType.Int)
        tokenIdParam.Value = tokenId
        ' execute...
        Command.ExecuteNonQuery()
        ' cleanup...
        Command.Dispose()
        connection.Close()

    End Sub

End Class

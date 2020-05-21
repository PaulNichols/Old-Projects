Imports System.Diagnostics
Imports System.Runtime.Remoting.Contexts
Imports System.Runtime.Remoting.Messaging

Public Class EnterpriseApplication

    ' members...
    Private Shared _application As EnterpriseApplication
    Private Shared _connectionString As String
    Private _connectionStringParts As Hashtable
    Public ConnectionMode As ConnectionType = ConnectionType.Direct
    Private _serviceObjectFactory As ServiceObjectFactory
    Private _securityToken As String
    'Private _counters As New EnterpriseCounters()
    Public TraceSwitch As New TraceSwitch("WEO Trace", "Trace switch for WEO applications")

    ' const...
    Public Const SecurityTokenSlotName As String = "WeoSecurityToken"

    ' enum...
    Public Enum ConnectionType As Integer
        Direct = 0
        Remoting = 1
    End Enum

    ' new...
    Shared Sub New()
        _application = New EnterpriseApplication()
    End Sub

    ' ConnectionString...
    Public Property ConnectionString() As String
        Get
            If _connectionString Is Nothing Then
                _connectionStringParts = Nothing
                Dim o As Object = ConnectionStringParts()
            End If
            Return _connectionString
        End Get
        Set(ByVal Value As String)

            ' set it...
            _connectionString = Value

            ' reset...
            _serviceObjectFactory = Nothing
            _connectionStringParts = Nothing

        End Set
    End Property

    ' Application property...
    Public Shared ReadOnly Property Application() As EnterpriseApplication
        Get
            Return _application
        End Get
    End Property

    ' ServiceObjectFactory - create a factory...
    Public ReadOnly Property ServiceObjectFactory() As ServiceObjectFactory
        Get

            ' do we have one?
            If _serviceObjectFactory Is Nothing Then

                ' look at the string...
                If ConnectionStringParts.Contains("Enterprise Connection Type") = True Then

                    Dim connectionType As String = ConnectionStringParts("Enterprise Connection Type").ToString().ToLower()
                    Select Case connectionType

                        ' direct...
                    Case "direct"
                            _serviceObjectFactory = New DirectServiceObjectFactory()

                            ' remoting...
                        Case "remoting"
                            _serviceObjectFactory = New RemotingServiceObjectFactory()

                            ' else...
                        Case Else
                            Throw New NotSupportedException("Connection type '" & connectionType & "' not supported.")

                    End Select

                End If

                ' did we get one?  create a default one...
                If _serviceObjectFactory Is Nothing Then
                    _serviceObjectFactory = New DirectServiceObjectFactory()
                End If

            End If

            ' return it...
            Return _serviceObjectFactory

        End Get
    End Property

    Private Function FindConfigFile(ByVal dir As IO.DirectoryInfo, ByVal configFileName As String) As String
        Dim FullFileName As String = IO.Path.Combine(dir.FullName, configFileName)
        If IO.File.Exists(FullFileName) Then
            Return String.Concat(FullFileName)
        Else
            Dim ParentDir As IO.DirectoryInfo = dir.Parent
            If Not ParentDir Is Nothing Then
                Return FindConfigFile(ParentDir, configFileName)
            Else
                Return Nothing
            End If
        End If
    End Function

    Public ReadOnly Property ConfigFileName() As String
        Get
            'check to see if the calling appliation has an
            'appropriate config file.
            Return FindConfigFile(ConfigFileDirectory, "Application.config")
        End Get
    End Property

    Public ReadOnly Property ConfigFileDirectory() As IO.DirectoryInfo
        Get
            Dim URIObject As New Uri(System.Reflection.Assembly.GetExecutingAssembly.CodeBase)
            'SCS - 25 April 2005, Changed AbsolutePath to LocalPath as AbsolutePath 
            'includes the %20 as a space character
            Dim CallingFileName As String = URIObject.LocalPath
            Dim Result As IO.DirectoryInfo = Nothing
            If Not CallingFileName Is Nothing AndAlso IO.File.Exists(CallingFileName) Then
                Dim FileInfo As New IO.FileInfo(CallingFileName)
                Result = FileInfo.Directory
            End If
            Return Result
        End Get
    End Property

    ' ConnectionStringParts - split up the string...
    Public ReadOnly Property ConnectionStringParts() As Hashtable
        Get

            ' do we have it?
            If _connectionStringParts Is Nothing Then

                ' create it...
                _connectionStringParts = New Hashtable

                If _connectionString Is Nothing Then

                    If Not ConfigFileName Is Nothing Then
                        Dim sr As New IO.StreamReader(ConfigFileName)
                        Dim Contents As String = sr.ReadToEnd
                        '                        Do
                        '                       Try
                        'Line = sr.ReadToEnd
                        'If Line.ToLower.StartsWith("<connectionstring") Then
                        Dim ConnectionStringIndex As Int32 = Contents.IndexOf("<connectionString>")
                        Dim QuoteIndex As Int32 = Contents.IndexOf("""", ConnectionStringIndex)

                        _connectionString = Contents.Substring(QuoteIndex + 1, Contents.LastIndexOf("""") - QuoteIndex - 1)
                        'Exit Do
                        'End If
                        'Catch ex As Exception

                        'End Try
                        'Loop
                        sr.Close()
                        sr = Nothing
                    End If
                End If
                ' split it...
                Dim parts() As String = ConnectionString.Split(";"c)
                Dim part As String
                For Each part In parts

                    ' split into value...
                    Dim valueParts() As String = part.Split("="c)
                    _connectionStringParts.Add(valueParts(0), valueParts(1))

                Next

            End If

            ' return...
            Return _connectionStringParts

        End Get
    End Property

    Public Property SecurityToken() As String
        Get
            Return _securityToken
        End Get
        Set(ByVal Value As String)

            ' set...
            _securityToken = Value

            ' store the token in the context...
            ConfigureThread()

        End Set
    End Property

    Public Sub ConfigureThread()

        ' add it...
        CallContext.SetData(SecurityTokenSlotName, New ContextToken(SecurityToken))

    End Sub

    ' Counters...
    'Public ReadOnly Property Counters() As EnterpriseCounters
    '    Get
    '        Return _counters
    '    End Get
    'End Property

End Class

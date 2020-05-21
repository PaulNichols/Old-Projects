Public Class RemotingServiceObjectFactory
    Inherits ServiceObjectFactory

    ' members...
    Public ChannelType As RemotingChannelType = RemotingChannelType.Tcp
    Public RemoteAppName As String
    Public RemoteServerName As String
    Public RemotePort As Integer

    ' enum...
    Public Enum RemotingChannelType As Integer
        Tcp = 0
        Http = 1
    End Enum

    ' Constructor...
    Public Sub New()

        ' go through the parts...
        Dim part As String
        For Each part In EnterpriseApplication.Application.ConnectionStringParts.Keys

            ' check...
            Select Case part.ToLower()

                Case "appname"
                    RemoteAppName = EnterpriseApplication.Application.ConnectionStringParts(part).ToString
                Case "servername"
                    RemoteServerName = EnterpriseApplication.Application.ConnectionStringParts(part).ToString
                Case "port"
                    RemotePort = Integer.Parse(EnterpriseApplication.Application.ConnectionStringParts(part).ToString)
                Case "protocol"
                    Dim Protocol As String = EnterpriseApplication.Application.ConnectionStringParts(part).ToString
                    Select Case Protocol.ToLower()
                        Case "tcp"
                            ChannelType = RemotingChannelType.Tcp
                        Case "http"
                            ChannelType = RemotingChannelType.Http
                        Case Else
                            Throw New NotSupportedException("Channel type '" & Protocol & "' not supported.")
                    End Select

            End Select

        Next

        ' did we get everything?
        If RemoteAppName = "" Then
            Throw New RemotingConnectionException("You must provide a remote application name")
        End If
        If RemoteServerName = "" Then
            Throw New RemotingConnectionException("You must provide a remote server name")
        End If
        If RemotePort = 0 Then
            Throw New RemotingConnectionException("You must provide a remote port")
        End If

    End Sub

    Public Overrides Function Create(ByVal serviceObjectType As System.Type) As EnterpriseObjects.Service

        ' get the url...
        Dim url As String = GetRemotingUrl(serviceObjectType)
        Dim serviceObject As Object = System.Activator.GetObject(serviceObjectType, url)

        ' return it...
        Return CType(serviceObject, Service)

    End Function

    Public Overrides Function ToString() As String
        Return "Remoting"
    End Function

    Public Function GetRemotingUrl(ByVal serviceObjectType As Type) As String

        ' create it...
        Dim url As New System.Text.StringBuilder()
        Select Case ChannelType
            Case RemotingChannelType.Tcp
                url.Append("tcp")
            Case RemotingChannelType.Http
                url.Append("http")
            Case Else
                Throw New NotSupportedException("Remoting channel type '" & ChannelType & "' not supported.")
        End Select

        ' append...
        url.Append("://")
        url.Append(RemoteServerName)
        url.Append(":")
        url.Append(RemotePort.ToString())
        url.Append("/")
        url.Append(RemoteAppName)
        url.Append("/")
        url.Append(serviceObjectType.ToString())
        url.Append(".remote")

        ' return...
        Return url.ToString()

    End Function

End Class

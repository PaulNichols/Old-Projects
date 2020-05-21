Imports System.Runtime.Remoting.Messaging

<Serializable()> Public Class ContextToken
    Implements ILogicalThreadAffinative

    Private _token As String

    Public Sub New(ByVal token As String)
        _token = token
    End Sub

    Public Property Token() As String
        Get
            Return _token
        End Get
        Set(ByVal Value As String)
            _token = Value
        End Set
    End Property

End Class

Public Class ConnectionTypeNotSupportedException
    Inherits ApplicationException

    Public Sub New(ByVal type As Integer)
        MyBase.New("Connection type '" & type & "' not supported")
    End Sub

End Class

Public Class RemotingConnectionException
    Inherits ApplicationException

    Public Sub New(ByVal message As String)
        MyBase.New("A Remoting connection/configuration exception has occured:" & message)
    End Sub

End Class

Public Class SecurityTokenNotFoundException
    Inherits ApplicationException

    Public Sub New()
        MyBase.New("The security token was not found in the current context.")
    End Sub

End Class
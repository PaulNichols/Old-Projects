Public Class BOException

    Public Sub New(ByVal Ex As Exception)
        mEx = Ex
    End Sub

    Private mEx As Exception

    Public ReadOnly Property Exception() As Exception
        Get
            Return mEx
        End Get
    End Property


    Public Function GetInnerExceptionsMessage() As String
        Dim alMessages As New ArrayList
        Dim Ex As Exception = mEx
        Do
            alMessages.Add(Ex.Message)
            Ex = Ex.InnerException
        Loop Until Ex Is Nothing
        Dim aMessages(alMessages.Count - 1) As String
        alMessages.CopyTo(aMessages)
        Return String.Join(" | ", aMessages)
    End Function

End Class

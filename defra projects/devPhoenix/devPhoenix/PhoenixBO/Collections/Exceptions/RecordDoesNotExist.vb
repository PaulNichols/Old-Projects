Public Class RecordDoesNotExist
    Inherits Exception

    Public Sub New(ByVal entity As String, ByVal id As Int32)
        MyBase.New(String.Concat(entity.TrimEnd, " does not contain a primary key value of ", id.ToString))
    End Sub
End Class


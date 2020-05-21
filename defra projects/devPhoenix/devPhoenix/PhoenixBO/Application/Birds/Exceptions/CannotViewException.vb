Namespace Application.Bird
    Public Class CannotViewException
        Inherits Exception

        Public Sub New(ByVal whoCannot As String, ByVal viewWhat As String)
            MyBase.New(String.Concat(whoCannot, " cannot view ", viewWhat))
        End Sub
    End Class
End Namespace
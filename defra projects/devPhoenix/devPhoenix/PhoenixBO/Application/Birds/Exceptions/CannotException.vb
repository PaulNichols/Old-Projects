Namespace Application.Bird
    Public Class CannotException
        Inherits Exception

        Public Sub New(ByVal whoCan As String, ByVal cannotWhat As String)
            MyClass.New(whoCan, cannotWhat, String.Empty)
        End Sub

        Public Sub New(ByVal whoCan As String, ByVal cannotWhat As String, ByVal extra As String)
            MyBase.New(String.Concat("Only ", whoCan, " can ", cannotWhat, " an Application", extra, "."))
        End Sub
    End Class
End Namespace

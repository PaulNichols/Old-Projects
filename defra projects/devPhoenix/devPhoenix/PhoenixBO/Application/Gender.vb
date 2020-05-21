Namespace Application
    Public Class Gender
        Public Shared Function GetGenderById(ByVal id As Int32) As String
            Return System.Enum.GetName(GetType(Application.GenderType), id)
        End Function
    End Class
End Namespace

Namespace Application.Search.Data
    Public MustInherit Class BaseCITESPermitSearchData_BySpecies
        Inherits BaseCITESPermitSearchData
        Implements IScientificName, IApplicationNumber

        Public Sub New()
        End Sub

        Public Property ApplicationNumber() As String Implements IApplicationNumber.ApplicationNumber
            Get
                Return mApplicationNumber
            End Get
            Set(ByVal Value As String)
                mApplicationNumber = Value
            End Set
        End Property
        Private mApplicationNumber As String

        Public Property ScientificName() As String Implements IScientificName.ScientificName
            Get
                Return mScientificName
            End Get
            Set(ByVal Value As String)
                mScientificName = Value
            End Set
        End Property
        Private mScientificName As String
    End Class
End Namespace
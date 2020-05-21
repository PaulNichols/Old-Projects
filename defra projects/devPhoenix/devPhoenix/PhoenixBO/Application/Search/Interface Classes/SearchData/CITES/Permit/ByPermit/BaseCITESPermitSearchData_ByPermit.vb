Namespace Application.Search.Data
    Public MustInherit Class BaseCITESPermitSearchData_ByPermit
        Inherits BaseCITESPermitSearchData
        Implements Application.Search.Data.IPermitId, IScientificName

        Public Sub New()
        End Sub

        Public Property PermitId() As String Implements IPermitId.PermitId
            Get
                Return mPermitId
            End Get
            Set(ByVal Value As String)
                mPermitId = Value
            End Set
        End Property
        Private mPermitId As String

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
Namespace Application.Search.Data
    <Serializable()> _
    Public Class BirdOtherSearchData_Customer
        Inherits BaseBirdSearchData
        Implements ISearchDataSpecimen

        Public Sub New()
        End Sub

        Public Property SpecimenIdNumber() As String Implements ISearchDataSpecimen.SpecimenIdNumber
            Get
                Return mSpecimenIdNumber
            End Get
            Set(ByVal Value As String)
                mSpecimenIdNumber = Value
            End Set
        End Property
        Private mSpecimenIdNumber As String

        Public Property SpecimenIdType() As String Implements ISearchDataSpecimen.SpecimenIdType
            Get
                Return mSpecimenIdType
            End Get
            Set(ByVal Value As String)
                mSpecimenIdType = Value
            End Set
        End Property
        Private mSpecimenIdType As String
    End Class
End Namespace
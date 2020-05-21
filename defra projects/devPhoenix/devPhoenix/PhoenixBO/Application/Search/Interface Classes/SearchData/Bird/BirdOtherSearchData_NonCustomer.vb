Namespace Application.Search.Data
    <Serializable()> _
    Public Class BirdOtherSearchData_NonCustomer
        Inherits BaseBirdSearchData
        Implements ISearchDataSpecimen, ISearchDataKeeper, IAssignedTo

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

        Public Property KeeperId() As String Implements ISearchDataKeeper.KeeperId
            Get
                Return mKeeperId
            End Get
            Set(ByVal Value As String)
                mKeeperId = value
            End Set
        End Property
        Private mKeeperId As String

        Public Property KeeperName() As String Implements ISearchDataKeeper.KeeperName
            Get
                Return mKeeperName
            End Get
            Set(ByVal Value As String)
                mKeeperName = Value
            End Set
        End Property
        Private mKeeperName As String

        Public Property AssignedTo() As String Implements IAssignedTo.AssignedTo
            Get
                Return mAssignedTo
            End Get
            Set(ByVal Value As String)
                mAssignedTo = Value
            End Set
        End Property
        Private mAssignedTo As String
    End Class
End Namespace
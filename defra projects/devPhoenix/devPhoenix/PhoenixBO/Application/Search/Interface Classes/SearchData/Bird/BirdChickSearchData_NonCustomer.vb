Namespace Application.Search.Data
    <Serializable()> _
    Public Class BirdChickSearchData_NonCustomer
        Inherits BirdChickSearchData_Customer
        Implements ISearchDataKeeper, IAssignedTo, IInspectorateAdvice

        Public Sub New()
        End Sub

        Public Property KeeperId() As String Implements ISearchDataKeeper.KeeperId
            Get
                Return mKeeperId
            End Get
            Set(ByVal Value As String)
                mKeeperId = Value
            End Set
        End Property
        Private mKeeperId As String

        Public Property KeeperName() As String Implements ISearchDataKeeper.KeeperName
            Get
                Return mKeeperName
            End Get
            Set(ByVal Value As String)
                mKeeperName = value
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

        Public Property InspectorateAdvice() As String Implements IInspectorateAdvice.InspectorateAdvice
            Get
                Return mInspectorateAdvice
            End Get
            Set(ByVal Value As String)
                mInspectorateAdvice = Value
            End Set
        End Property
        Private mInspectorateAdvice As String
    End Class
End Namespace
Namespace Application.Search
    <Serializable()> _
    Public Class SearchResultGroup
        Public Sub New()
        End Sub

        Public Property Permits() As SearchResults
            Get
                Return mPermits
            End Get
            Set(ByVal Value As SearchResults)
                mPermits = Value
            End Set
        End Property
        Private mPermits As SearchResults

        Public Property ImportNotifications() As SearchResults
            Get
                Return mImportNotifications
            End Get
            Set(ByVal Value As SearchResults)
                mImportNotifications = Value
            End Set
        End Property
        Private mImportNotifications As SearchResults

        Public Property SeizureNotifications() As SearchResults
            Get
                Return mSeizureNotifications
            End Get
            Set(ByVal Value As SearchResults)
                mSeizureNotifications = Value
            End Set
        End Property
        Private mSeizureNotifications As SearchResults

        Public Property ChickDORs() As SearchResults
            Get
                Return mChickDORs
            End Get
            Set(ByVal Value As SearchResults)
                mChickDORs = Value
            End Set
        End Property
        Private mChickDORs As SearchResults

        Public Property AdultDORs() As SearchResults
            Get
                Return mAdultDORs
            End Get
            Set(ByVal Value As SearchResults)
                mAdultDORs = Value
            End Set
        End Property
        Private mAdultDORs As SearchResults

        Public Property BirdRegistrations() As SearchResults
            Get
                Return mBirdRegistrations
            End Get
            Set(ByVal Value As SearchResults)
                mBirdRegistrations = Value
            End Set
        End Property
        Private mBirdRegistrations As SearchResults
    End Class
End Namespace
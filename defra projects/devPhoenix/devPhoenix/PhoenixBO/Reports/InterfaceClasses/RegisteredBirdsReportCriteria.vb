Namespace ReportCriteria
    <Serializable()> _
    Public Class RegisteredBirdsReportCriteria
        Inherits ReportCriteria

        Public Sub New()
            MyBase.new()
        End Sub

        Public Property PartyId() As Int32
            Get
                Return mPartyId
            End Get
            Set(ByVal Value As Int32)
                mPartyId = Value
            End Set
        End Property
        Private mPartyId As Int32

        Public Property CurrentlyRegistered() As Boolean 'MLD added 20/1/5
            Get
                Return mCurrentlyRegistered
            End Get
            Set(ByVal Value As Boolean)
                mCurrentlyRegistered = Value
            End Set
        End Property
        Private mCurrentlyRegistered As Boolean = False

        Public Property PreviouslyRegistered() As Boolean 'MLD added 20/1/5
            Get
                Return mPreviouslyRegistered
            End Get
            Set(ByVal Value As Boolean)
                mPreviouslyRegistered = Value
            End Set
        End Property
        Private mPreviouslyRegistered As Boolean = False

        Friend Overrides ReadOnly Property Report() As RPT.BOReport
            Get
                Return New RPT.RegisteredBirds
            End Get
        End Property
    End Class

End Namespace

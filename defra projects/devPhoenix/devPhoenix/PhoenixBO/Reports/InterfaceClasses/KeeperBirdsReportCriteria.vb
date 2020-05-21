Namespace ReportCriteria
    <Serializable()> _
    Public Class KeeperBirdsReportCriteria
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

        Friend Overrides ReadOnly Property Report() As RPT.BOReport
            Get
                Return New RPT.KeeperBirds
            End Get
        End Property
    End Class

End Namespace


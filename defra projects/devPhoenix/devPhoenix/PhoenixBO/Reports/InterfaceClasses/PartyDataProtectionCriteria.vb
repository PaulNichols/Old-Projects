
Namespace ReportCriteria
    <Serializable()> _
    Public Class PartyDataProtectionCriteria
        Inherits ReportCriteria

        Public Sub New()
            MyBase.new()
        End Sub

        Public Property PartyId() As Integer
            Get
                Return mPartyId
            End Get
            Set(ByVal Value As Integer)
                mPartyId = Value
            End Set
        End Property
        Private mPartyId As Int32

        Friend Overrides ReadOnly Property Report() As RPT.BOReport
            Get
                Return New RPT.PartyDataProtection
            End Get
        End Property
    End Class

End Namespace

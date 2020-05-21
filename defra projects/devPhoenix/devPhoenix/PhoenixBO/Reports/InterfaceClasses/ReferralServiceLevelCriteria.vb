
Namespace ReportCriteria
    <Serializable()> _
    Public Class ReferralServiceLevelCriteria
        Inherits ReportCriteria

        Public Sub New()
            MyBase.new()
        End Sub

        Public FromDate As DateTime
        Public ToDate As DateTime

        Friend Overrides ReadOnly Property Report() As RPT.BOReport
            Get
                Return New RPT.ReferralServiceLevel
            End Get
        End Property
    End Class

End Namespace


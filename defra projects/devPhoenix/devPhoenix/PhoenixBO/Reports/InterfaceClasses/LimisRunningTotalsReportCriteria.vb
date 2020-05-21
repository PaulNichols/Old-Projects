
Namespace ReportCriteria
    <Serializable()> _
    Public Class LimisRunningTotalsReportCriteria
        Inherits ReportCriteria

        Public Sub New()
            MyBase.new()
        End Sub

        Public FinancialYear As Int32

        Friend Overrides ReadOnly Property Report() As RPT.BOReport
            Get
                Return New RPT.LimisRunningTotals
            End Get
        End Property
    End Class

End Namespace
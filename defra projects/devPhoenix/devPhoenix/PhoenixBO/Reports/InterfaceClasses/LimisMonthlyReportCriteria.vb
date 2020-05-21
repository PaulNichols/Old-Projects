
Namespace ReportCriteria
    <Serializable()> _
    Public Class LimisMonthlyReportCriteria
        Inherits ReportCriteria

        Public Sub New()
            MyBase.new()
        End Sub

        Public Month As Int32
        Public Year As Int32

        Friend Overrides ReadOnly Property Report() As RPT.BOReport
            Get
                Return New RPT.LimisMonthly
            End Get
        End Property
    End Class

End Namespace
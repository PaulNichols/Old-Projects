
Namespace ReportCriteria
    <Serializable()> _
    Public Class ViewAuditLogCriteria
        Inherits ReportCriteria

        Public Sub New()
            MyBase.new()
        End Sub

        Public FromDate As DateTime
        Public ToDate As DateTime

        Friend Overrides ReadOnly Property Report() As RPT.BOReport
            Get
                Return New RPT.ViewAuditLog
            End Get
        End Property
    End Class

End Namespace

Namespace ReportCriteria
    <Serializable()> _
    Public Class Schedule4BirdsCriteria
        Inherits ReportCriteria

        Public Sub New()
            MyBase.new()
        End Sub

        Public ApplicationId As Int32

        Friend Overrides ReadOnly Property Report() As RPT.BOReport
            Get
                Return New RPT.Schedule4Birds
            End Get
        End Property
    End Class

End Namespace

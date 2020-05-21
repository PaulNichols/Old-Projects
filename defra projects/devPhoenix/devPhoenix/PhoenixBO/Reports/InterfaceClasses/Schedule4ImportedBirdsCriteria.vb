
Namespace ReportCriteria
    <Serializable()> _
    Public Class Schedule4ImportedBirdsCriteria
        Inherits ReportCriteria

        Public Sub New()
            MyBase.new()
        End Sub

        Public ApplicationId As Int32

        Friend Overrides ReadOnly Property Report() As RPT.BOReport
            Get
                Return New RPT.Schedule4ImportedBirds
            End Get
        End Property
    End Class

End Namespace

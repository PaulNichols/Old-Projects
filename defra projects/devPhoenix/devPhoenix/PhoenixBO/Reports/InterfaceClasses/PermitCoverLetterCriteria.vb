
Namespace ReportCriteria
    <Serializable()> _
    Public Class PermitCoverLetterCriteria
        Inherits ReportCriteria

        Public Sub New()
            MyBase.new()
        End Sub

        Public PermitIds() As Int32

        Friend Overrides ReadOnly Property Report() As RPT.BOReport
            Get
                Return New RPT.PermitCoverLetter
            End Get
        End Property
    End Class

End Namespace


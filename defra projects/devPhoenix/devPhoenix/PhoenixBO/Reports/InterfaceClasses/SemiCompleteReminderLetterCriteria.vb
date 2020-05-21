
Namespace ReportCriteria
    <Serializable()> _
    Public Class SemiCompleteReminderLetterCriteria
        Inherits LetterCriteria

        Public Sub New()
            MyBase.new()
        End Sub

        Public Property PermitInfoIds As Integer()
            Get
                Return mPermitInfoIds
            End Get
            Set
                mPermitInfoIds = Value
            End Set
        End Property
        Private mPermitInfoIds() As Int32

        Friend Overrides ReadOnly Property Report() As RPT.BOReport
            Get
                Return New RPT.SemiCompleteReminderLetter
            End Get
        End Property
    End Class

End Namespace

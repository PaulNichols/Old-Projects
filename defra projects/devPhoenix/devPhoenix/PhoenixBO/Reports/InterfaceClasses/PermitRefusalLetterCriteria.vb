
Namespace ReportCriteria
    <Serializable()> _
    Public Class PermitRefusalLetterCriteria
        Inherits LetterCriteria

        Public Sub New()
            MyBase.new()
        End Sub

        Public Property PermitInfoIds() As Integer()
            Get
                Return mPermitInfoIds
            End Get
            Set(ByVal Value As Integer())
                mPermitInfoIds = Value
            End Set
        End Property
        Private mPermitInfoIds() As Int32

        Friend Overrides ReadOnly Property Report() As RPT.BOReport
            Get
                Return New RPT.PermitRefusalLetter
            End Get
        End Property
    End Class

End Namespace


Namespace ReportCriteria
    <Serializable()> _
    Public Class CertificateRefusalLetterCriteria
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
                Return New RPT.CertificateRefusalLetter
            End Get
        End Property
    End Class

End Namespace

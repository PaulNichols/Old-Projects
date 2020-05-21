
Namespace ReportCriteria
    <Serializable()> _
    Public Class CertificatePermitCoverLetterCriteria
        Inherits LetterCriteria

        Public Sub New()
            MyBase.new()
        End Sub

        Public Property ApplicationId As Integer
            Get
                Return mApplicationId
            End Get
            Set
                mApplicationId = Value
            End Set
        End Property
        Private mApplicationId As Int32

        Friend Overrides ReadOnly Property Report() As RPT.BOReport
            Get
                Return New RPT.PermitCoverLetter
            End Get
        End Property
    End Class
End Namespace

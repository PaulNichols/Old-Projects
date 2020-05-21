Namespace ReportCriteria
    <Serializable()> _
    Public Class PaymentReceiptCriteria
        Inherits ReportCriteria

        Public Sub New()
            MyBase.new()
        End Sub

        Public Property PaymentId() As Integer
            Get
                Return mPaymentId
            End Get
            Set(ByVal Value As Integer)
                mPaymentId = Value
            End Set
        End Property
        Private mPaymentId As Int32

        Friend Overrides ReadOnly Property Report() As RPT.BOReport
            Get
                Return New RPT.ProcessPaymentReceipt
            End Get
        End Property
    End Class

End Namespace



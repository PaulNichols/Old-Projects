Namespace Payments
    Public Interface IBORefund
        Property RefundId() As Integer
        Property PaymentId() As Integer
        Property SSOUserId() As Int64
        Property RefundType() As BORefund.RefundCode
        Property RefundDateTime() As DateTime
        Property Amount() As Decimal
        Property OriginalPaymentAmount() As Decimal
        Property OriginalPartyId() As Integer
        Property NotesComments() As String
        Property ReportDate() As DateTime
        Property GatewayAuthorisation() As String
        Property GatewayReference() As String
        Property BankReference() As String
    End Interface
End Namespace
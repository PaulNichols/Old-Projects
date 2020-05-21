
Namespace Payments
    Public Interface IBOFeeReduction
        Property FeeReductionId() As Integer
        Property ApplicationId() As Integer
        Property PaymentId() As Integer
        Property Reason() As String
        Property SSOUserId() As Int64
        Property DateTime() As DateTime
        Property Amount() As Decimal
        Property OriginalFee() As Decimal
    End Interface
End Namespace
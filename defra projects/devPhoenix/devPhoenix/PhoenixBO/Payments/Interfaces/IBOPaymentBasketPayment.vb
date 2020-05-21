Namespace Payments
    Public Interface IBOPaymentBasketPayment
        Property PaymentBasketId() As Integer
        Property PaymentId() As Integer
        Property PaymentReference() As String
        Property LinkDateTime() As Date
        Property Amount() As Decimal
    End Interface
End Namespace

Namespace Payments
    Public Interface IBOPaymentBasket
        Property PaymentBasketId() As Integer
        Property PartyId() As Integer
        Property TotalFeeCharged() As Decimal
        Property BasketClosed() As Boolean
        Property BasketIsTopup() As Boolean
        Property TopupReference() As String
        Property TopupAmount() As Decimal
        Property RemittanceAdvicePrinted() As Boolean
        Property RemittanceAdviceDateTime() As Date
        Property RemittanceAdvicePaymentId() As Integer
        Property RemittanceAdvicePaymentRef() As String
    End Interface
End Namespace
Namespace Payments
    Public Interface IBOCash
        Property CashId() As Integer
        Property PaymentId() As Integer
        Property SerialNumber() As String
        Property Amount() As Decimal
        Property AmountDisplay() As String
    End Interface
End Namespace
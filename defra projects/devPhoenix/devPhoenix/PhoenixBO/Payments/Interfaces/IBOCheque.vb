Namespace Payments
    Public Interface IBOCheque
        Property ChequeId() As Integer
        Property PaymentId() As Integer
        Property BankSortCode() As String
        Property BankAccountNumber() As String
        Property SerialNumber() As String
        Property AccountName() As String
        Property Amount() As Decimal
        Property AmountDisplay() As String
    End Interface
End Namespace
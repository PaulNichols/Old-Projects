Namespace Payments
    Public Interface IBOPayment
        Property PaymentId() As Integer
        Property PartyId() As Integer
        Property PaymentReference() As String
        Property PaymentMethodName() As String
        Property PaymentMethodId() As BOPayment.PaymentMethod
        Property InitiatedByCustomer() As Boolean
        Property PaymentDate() As String
        Property PaymentDateTime() As Date
        Property PaymentAmount() As Decimal
        Property PaymentAmountDisplay() As String
        Property AvailableBalance() As Decimal
        Property AvailableBalanceDisplay() As String
        Property RefundTotal() As Decimal
        Property RefundTotalDisplay() As String
        Property GatewayAuthorisationCode() As String
        Property GatewayBankReference() As String
        Property EnteredApplicationsCovered() As Integer
        Property ActualApplicationsCovered() As Integer
        Property ReceiptPrinted() As Boolean
        Property ReportDate() As String
        Property ReportDateTime() As Date
        Property Cheques() As BOCheque()
        Property Cash() As BOCash()
        Property PostalOrders() As BOPostalOrder()
    End Interface
End Namespace
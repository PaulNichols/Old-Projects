Namespace Payments
    Public Interface IBOApplicationSummary
        Property ApplicationId() As Integer
        Property DateReceived() As String
        Property ApplicationType() As String
        Property PayStatus() As String
        Property Owner() As String
        Property TrueCost() As Decimal
        Property TrueCostDisplay() As String
        Property Cost() As Decimal
        Property CostDisplay() As String
        Property RemoveX() As String
    End Interface
End Namespace

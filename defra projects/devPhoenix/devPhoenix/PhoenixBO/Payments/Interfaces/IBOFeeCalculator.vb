'''<copyright>Defra 2004</copyright>
'''<author>Mark Lines-Davies</author>
'''<summary>
''' Interface for the BOFeeCalculator class. Unlike the other classes in the Payments BO section, which are
''' entity classes, BOFeeCalculator is a controller class. Its interface, therefore, consists of methods
''' not properties. A separate class has been provided, rather than placing the business logic in
''' an existing entity class, as the calculations span several entities.
'''</summary>
Namespace Payments
    Public Interface IBOFeeCalculator
        Function CalculateStandAloneFee(ByVal applicationId As Int32) As BOCalculationResult
        Sub RecalculateBasketFees(ByVal basketId As Int32)
        Function CalculateBirdFee(ByVal applicationTypeId As Int32, ByVal specimenIds() As Int32) As BOCalculationResult
    End Interface
End Namespace
'''<copyright>Defra 2004</copyright>
'''<author>Mark Lines-Davies</author>
'''<summary>
''' Interface for the BOCalculationResult class. This encapsulates a decimal value
''' for the result of a successful calculation, with a user-friendly error message
''' for unsuccessful calculations. The ErrorMessage property is Nothing for a 
''' successful result
'''</summary>
Namespace Payments
    Public Interface IBOCalculationResult
        Property Amount As Decimal
        Property ErrorMessage As String
        Property AnimalSpeciesCount As Integer
        Property PlantGeneraCount As Integer
        Property MonitoredBirdsOfPrey As Integer
        Property UnmonitoredBirdsOfPrey As Integer
        Property NonBirdsOfPrey As Integer
    End Interface
End Namespace
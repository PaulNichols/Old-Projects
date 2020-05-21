'''<copyright>Defra 2004</copyright>
'''<author>Mark Lines-Davies</author>
'''<summary>
''' Class for a Calculation Result class. This encapsulates a decimal value
''' for the result of a successful calculation, with a user-friendly error message
''' for unsuccessful calculations. The ErrorMessage property is Nothing for a 
''' successful result
'''</summary>
Namespace Payments
    Public Class BOCalculationResult
        Inherits PaymentsBaseBO
        Implements IBOCalculationResult

        Private mAmount As Decimal
        Private mErrorMessage As String = "Not initialised"
        Private mAnimalSpeciesCount As Integer = 0
        Private mPlantGeneraCount As Integer = 0
        Private mMonitoredBirdsOfPrey As Integer = 0
        Private mUnmonitoredBirdsOfPrey As Integer = 0
        Private mNonBirdsOfPrey As Integer = 0

        Public Property Amount As Decimal Implements IBOCalculationResult.Amount
            Get
                Return mAmount
            End Get
            Set
                mAmount = Value
                mErrorMessage = Nothing
            End Set
        End Property

        Public Property ErrorMessage As String Implements IBOCalculationResult.ErrorMessage
            Get
                Return mErrorMessage
            End Get
            Set
                mErrorMessage = Value
            End Set
        End Property

        Public Property AnimalSpeciesCount As Integer Implements IBOCalculationResult.AnimalSpeciesCount
            Get
                Return mAnimalSpeciesCount
            End Get
            Set
                mAnimalSpeciesCount = Value
            End Set
        End Property

        Public Property PlantGeneraCount As Integer Implements IBOCalculationResult.PlantGeneraCount
            Get
                Return mPlantGeneraCount
            End Get
            Set
                mPlantGeneraCount = Value
            End Set
        End Property

        Public Property MonitoredBirdsOfPrey As Integer Implements IBOCalculationResult.MonitoredBirdsOfPrey
            Get
                Return mMonitoredBirdsOfPrey
            End Get
            Set
                mMonitoredBirdsOfPrey = Value
            End Set
        End Property

        Public Property UnmonitoredBirdsOfPrey As Integer Implements IBOCalculationResult.UnmonitoredBirdsOfPrey
            Get
                Return mUnmonitoredBirdsOfPrey
            End Get
            Set
                mUnmonitoredBirdsOfPrey = Value
            End Set
        End Property

        Public Property NonBirdsOfPrey As Integer Implements IBOCalculationResult.NonBirdsOfPrey
            Get
                Return mNonBirdsOfPrey
            End Get
            Set
                mNonBirdsOfPrey = Value
            End Set
        End Property
    End Class
End Namespace
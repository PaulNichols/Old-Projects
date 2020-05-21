Namespace Taxonomy
    <Serializable()> _
        Public Class AnimalStatistics

        Public Sub New()

        End Sub

        Private BirdFeeLevelValue As String
        Public Property BirdFeeLevel() As String
            Get
                Return BirdFeeLevelValue
            End Get
            Set(ByVal Value As String)
                BirdFeeLevelValue = Value
            End Set
        End Property

        Private GestationIncubationDaysValue As String
        Public Property GestationIncubationDays() As String
            Get
                Return GestationIncubationDaysValue
            End Get
            Set(ByVal Value As String)
                GestationIncubationDaysValue = Value
            End Set
        End Property

        Private AverageOffspringValue As String
        Public Property AverageOffspring() As String
            Get
                Return AverageOffspringValue
            End Get
            Set(ByVal Value As String)
                AverageOffspringValue = Value
            End Set
        End Property

        Private MicrochipMinimumSizeValue As String
        Public Property MicrochipMinimumSize() As String
            Get
                Return MicrochipMinimumSizeValue
            End Get
            Set(ByVal Value As String)
                MicrochipMinimumSizeValue = Value
            End Set
        End Property

        Private MinimumAgeForMicrochipValue As String
        Public Property MinimumAgeForMicrochip() As String
            Get
                Return MinimumAgeForMicrochipValue
            End Get
            Set(ByVal Value As String)
                MinimumAgeForMicrochipValue = Value
            End Set
        End Property

        Private AverageLifespanValue As String
        Public Property AverageLifespan() As String
            Get
                Return AverageLifespanValue
            End Get
            Set(ByVal Value As String)
                AverageLifespanValue = Value
            End Set
        End Property

        Private OldestAcceptedAgeValue As String
        Public Property OldestAcceptedAge() As String
            Get
                Return OldestAcceptedAgeValue
            End Get
            Set(ByVal Value As String)
                OldestAcceptedAgeValue = Value
            End Set
        End Property

        Private SexualMaturityAgeValue As String
        Public Property SexualMaturityAge() As String
            Get
                Return SexualMaturityAgeValue
            End Get
            Set(ByVal Value As String)
                SexualMaturityAgeValue = Value
            End Set
        End Property
    End Class
End Namespace

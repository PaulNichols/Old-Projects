Namespace Taxonomy
    <Serializable()> _
        Public Class CountryDistribution

        Public Sub New()
            'This constructor is intentionally empty, but defined for explicitness.
        End Sub

        Private CountryValue As String
        Public Property Country() As String
            Get
                Return CountryValue
            End Get
            Set(ByVal Value As String)
                CountryValue = Value
            End Set
        End Property

        Private BRUValue As String
        Public Property BRU() As String
            Get
                Return BRUValue
            End Get
            Set(ByVal Value As String)
                BRUValue = Value
            End Set
        End Property

        Private CertainValue As String
        Public Property Certain() As String
            Get
                Return CertainValue
            End Get
            Set(ByVal Value As String)
                CertainValue = Value
            End Set
        End Property

        Private ExtinctValue As String
        Public Property Extinct() As String
            Get
                Return ExtinctValue
            End Get
            Set(ByVal Value As String)
                ExtinctValue = Value
            End Set
        End Property

        Private IntroducedValue As String
        Public Property Introduced() As String
            Get
                Return IntroducedValue
            End Get
            Set(ByVal Value As String)
                IntroducedValue = Value
            End Set
        End Property

        Private ReintroducedValue As String
        Public Property Reintroduced() As String
            Get
                Return ReintroducedValue
            End Get
            Set(ByVal Value As String)
                ReintroducedValue = Value
            End Set
        End Property

        Private BreedingValue As String
        Public Property Breeding() As String
            Get
                Return BreedingValue
            End Get
            Set(ByVal Value As String)
                BreedingValue = Value
            End Set
        End Property

        Private VagrantValue As String
        Public Property Vagrant() As String
            Get
                Return VagrantValue
            End Get
            Set(ByVal Value As String)
                VagrantValue = Value
            End Set
        End Property
    End Class
End Namespace

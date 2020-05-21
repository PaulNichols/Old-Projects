Namespace Taxonomy
    Public Interface ISpeciesDistributionDisplay
        Property KingdomID() As Int32
        Property TaxonID() As Int32
        Property DistributionID() As Int32
        Property CountryLocation() As String
        Property BRULocation() As String
        Property IsBRU() As Boolean
        Property NoteID() As Int32
        Property Certain() As String
        Property Extinct() As String
        Property Introduced() As String
        Property ReIntroduced() As String
        Property Breeding() As String
        Property Vagrant() As String
    End Interface
End Namespace
Namespace Application
    Public Enum PaymentKingdomEnum
        NotApplicable
        Animal
        Plant
    End Enum

    Public Enum PaymentTaxonTypeEnum
        NotApplicable
        Family
        Genus
        Species
    End Enum

    Public Interface IBOSpecie

        Inherits IValidate

        Property SpecieId() As Int32
        Property CommonName() As String
        Property ECAnnex() As String
        Property CITESAppendix() As String
        Property ScientificName() As String
        Property Description() As String
        Property AppliedForName() As String
        Property Lineage() As String
        Property KeyedByApplicant() As Boolean
        Property Hybrid() As Boolean
        Property Source() As Int32
        Property CommonNameID() As Int32
        Property Taxa() As BOTaxonIdentifier()
        Property IsCoral() As Boolean
        Property PaymentKingdom() As PaymentKingdomEnum
        Property PaymentTaxonType() As PaymentTaxonTypeEnum

    End Interface
End Namespace

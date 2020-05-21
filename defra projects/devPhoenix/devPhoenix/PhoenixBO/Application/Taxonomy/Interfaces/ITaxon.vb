Namespace Taxonomy

    Public Enum TaxonTypeEnum
        Kingdom = 1
        Phylum = 2
        [Class] = 3
        Order = 4
        Family = 5
        Genus = 6
        Species = 7
        Epithet = 8
        Stock = 9
    End Enum

    Public Enum TaxonStatusEnum
        Accepted = 1
        Synonym = 2
        StockName = 3
        Tentative = 4
        Deleted = 5
        Unknown = 6
        NotInLiterature = 7
    End Enum

    Public Interface ITaxon
        Inherits IValidate
        Property ParentKingdomID() As Int32
        Property ParentTaxonID() As Int32
        Property ParentTaxonTypeID() As Int32
        Property ShortScientificNameHTMLFormatted() As String
        Property ShortScientificNameUnformatted() As String
        Property LongScientificNameHTMLFormatted() As String
        Property LongScientificNameUnformatted() As String
        Property TaxonNameHTMLFormatted() As String
        Property TaxonEpithetHTMLFormatted() As String
        Property TaxonAuthorHTMLFormatted() As String
        Property TaxonNameUnformatted() As String
        Property TaxonEpithetUnformatted() As String
        Property TaxonAuthorUnformatted() As String
        Property TaxonStatus() As Taxonomy.TaxonStatusEnum
        Property CanHaveSynonyms() As Boolean
        Property CanHaveStockNames() As Boolean
        Property CanHaveAcceptedNames() As Boolean
        Property CanHaveAquaticDistribution() As Boolean
        Property ID() As Int32
        Property KingdomID() As Int32
        Property TaxonId() As Int32
        Property Validated() As Object
        Property DisplayTaxonType() As TaxonTypeEnum
        Property DisplayTaxonTypeDescription() As String
        Property TaxonTypeID() As Int32
        Property TaxonType() As TaxonTypeEnum
        Property TaxonTypeDescription() As String
        Property TaxonStatusID() As Int32
        Property TaxonStatusDescription() As String
        Property CITESReference() As String
        Property DistributionComplete() As String
        Property CommonNames() As String
        Property IsCoral() As Boolean
        Property PaymentTaxonType() As Application.PaymentTaxonTypeEnum
        Property PaymentKingdom() As Application.PaymentKingdomEnum
        Overloads Function Validate(ByVal writeFlag As Boolean) As ValidationManager
        Function GetHigherTaxa() As Taxonomy.BOTaxon()
        Function GetLowerTaxa() As Taxonomy.BOTaxon()
        Function GetLowerTaxa(ByVal levels As Int32) As Taxonomy.BOTaxon()
        Function GetSynonyms() As BOTaxon()
        Function GetAcceptedNames() As BOTaxon()
        Function GetCommonNames() As Taxonomy.BOCommonName()
        Function GetConservationSummary() As BOConservationSummary
        Function GetUsage() As Taxonomy.BOUsage()
        Function GetAnimalDelegationAuthority() As Taxonomy.BOAnimalDelegationAuthorityDisplay()
        Function GetDistributionDisplay() As Taxonomy.BOSpeciesDistributionDisplay()
        Function GetAquaticDistributionDisplay() As Taxonomy.BOSpeciesAquaticDistributionDisplay()
        Function GetStockNames() As BOTaxon()
        Function GetParentTaxon() As BOTaxon
        Property CanHaveDecisions() As Boolean
        Property CanHaveQuotas() As Boolean

    End Interface
End Namespace
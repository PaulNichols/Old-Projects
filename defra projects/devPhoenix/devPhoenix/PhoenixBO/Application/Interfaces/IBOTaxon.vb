Namespace Application
    Public Interface IBOTaxon
        Inherits IValidate

        Property ID() As Int32
        Property KingdomID() As Int32
        Property TaxonID() As Int32
        Property TaxonTypeID() As Int32
        Property SpecieID() As Int32
        Property IsCoral() As Boolean
        Property PaymentKingdom() As PaymentKingdomEnum
        Property PaymentTaxonType() As PaymentTaxonTypeEnum
    End Interface
End Namespace
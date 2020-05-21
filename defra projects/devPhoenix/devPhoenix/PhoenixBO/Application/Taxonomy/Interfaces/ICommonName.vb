Namespace Taxonomy
    Public Interface ICommonName
        Inherits IValidate
        Property Validated() As Object
        Property ID() As Int32
        Property Source() As Int32
        Property KingdomID() As Int32
        Property TaxonId() As Int32
        Property TaxonTypeID() As Int32
        Property Name() As String
        Property AreaOfUse() As String
        Property IsProductIndicator() As Boolean
        Property Taxon() As BOTaxon
        Overloads Function Validate(ByVal writeFlag As Boolean) As ValidationManager
    End Interface
End Namespace
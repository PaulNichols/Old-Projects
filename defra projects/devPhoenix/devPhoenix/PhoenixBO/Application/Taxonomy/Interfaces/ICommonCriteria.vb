Namespace SearchTaxonomy

    <Serializable()> Public Enum SearchableTaxonomyComponentEnum
        PhylumTaxon
        ClassTaxon
        OrderTaxon
        FamilyTaxon
        GenusTaxon
        SpeciesTaxon
        CommonName
        Usage
    End Enum

    <Serializable()> Public Enum SearchRestrictionEnum
        None
        Schedule4
        AvesClass
    End Enum

    <Serializable()> Public Enum PickedObjectTypeEnum
        None
        BOTaxon
        BOSpecie
    End Enum

    <Serializable()> Public Enum SearchKingdomTypeEnum
        Any
        Plant
        Animal
        Fungi
        Protozoa
        Bacteria
    End Enum

    <Serializable()> Public Enum SearchForAmountEnum
        One
        Many
    End Enum

    <Serializable()> Public Structure SearchableTaxonomyComponent
        Public Sub New(ByVal NewName As String, ByVal NewValue As SearchTaxonomy.SearchableTaxonomyComponentEnum)
            Name = NewName
            Value = NewValue
        End Sub
        Public Name As String
        Public Value As SearchTaxonomy.SearchableTaxonomyComponentEnum
    End Structure


    Public Interface ICommonCriteria
        Property SearchType() As SearchableTaxonomyComponentEnum
        Property SearchRestriction() As SearchRestrictionEnum
        Property SearchResultObject() As PickedObjectTypeEnum
        Property SearchResultType() As Int32
        Property SearchResultStatus() As Int32
        Property SearchCriteriaDisabled() As Int32
        Property SearchKingdomType() As SearchKingdomTypeEnum
        Property SearchForAmount() As SearchForAmountEnum
    End Interface
End Namespace
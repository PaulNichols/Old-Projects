Imports uk.gov.defra.Phoenix.DO.DataObjects

Namespace Taxonomy.Plant
    Public Class BOTaxonFactory

        Public Shared Function PolymorphicCreate(ByVal taxon As Taxonomy.BOTaxon) As BOTaxonBase
            Select Case taxon.TaxonType
                Case TaxonTypeEnum.Class
                    Return New BOClass(taxon)
                Case TaxonTypeEnum.Family
                    Return New BOFamily(taxon)
                Case TaxonTypeEnum.Genus
                    Return New BOGenus(taxon)
                Case TaxonTypeEnum.Kingdom
                    Return New BOKingdom(taxon)
                Case TaxonTypeEnum.Order
                    Return New BOOrder(taxon)
                Case TaxonTypeEnum.Phylum
                    Return New BOPhylum(taxon)
                Case TaxonTypeEnum.Species
                    Return New BOSpecies(taxon)
                Case TaxonTypeEnum.Epithet
                    Return New BOEpithet(taxon)
            End Select
            Throw New Exception("Unknown Taxon Type")
        End Function
    
    End Class
End Namespace

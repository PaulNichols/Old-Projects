Namespace Taxonomy.Plant
    <Serializable()> _
    Public Class BOTaxonId

            Sub New()
            End Sub

            Sub New(ByVal taxon As Taxonomy.BOTaxon)
                mKingdomId    = taxon.KingdomID
                mTaxonId      = taxon.TaxonId
                mTaxonTypeId  = taxon.TaxonTypeID
            End Sub

            Public Property KingdomId As Int32
                Get
                    Return mKingdomId
                End Get
                Set
                    mKingdomId = Value
                End Set
            End Property
            Private mKingdomId As Int32

            Public Property TaxonId As Int32
                Get
                    Return mTaxonId
                End Get
                Set
                    mTaxonId = Value
                End Set
            End Property
            Private mTaxonId As Int32

            Public Property TaxonTypeId As Int32
                Get
                    Return mTaxonTypeId
                End Get
                Set
                    mTaxonTypeId = Value
                End Set
            End Property
            Private mTaxonTypeId As Int32

    End Class
End Namespace
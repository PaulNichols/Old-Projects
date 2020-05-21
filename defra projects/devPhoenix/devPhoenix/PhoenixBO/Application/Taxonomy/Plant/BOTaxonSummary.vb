Namespace Taxonomy.Plant
    <Serializable()> _
    Public Class BOTaxonSummary
        Inherits BOTaxonId

            Sub New()
            End Sub

            Sub New(ByVal taxon As Taxonomy.BOTaxon)
                MyBase.New(taxon)
                mName         = taxon.TaxonNameUnformatted
                mLongName     = taxon.LongScientificNameHTMLFormatted
                mStatus       = taxon.TaxonStatusDescription
                mType         = taxon.TaxonTypeDescription
            End Sub

            Public Property Type As String
                Get
                    Return mType
                End Get
                Set
                    mType = Value
                End Set
            End Property
            Private mType As String

            Public Property Status As String
                Get
                    Return mStatus
                End Get
                Set
                    mStatus = Value
                End Set
            End Property
            Private mStatus As String

            Public Property LongName As String
                Get
                    Return mLongName
                End Get
                Set
                    mLongName = Value
                End Set
            End Property
            Private mLongName As String

            Public Property Name As String
                Get
                    Return mName
                End Get
                Set
                    mName = Value
                End Set
            End Property
            Private mName As String
    End Class
End Namespace
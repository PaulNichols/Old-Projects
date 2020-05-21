Namespace Taxonomy
    Public Interface ITaxonSearchResults
        Property Taxa() As Taxonomy.BOTaxon()
        Property Messages() As String()
        Property HasTaxa() As Boolean
        Property HasMessages() As Boolean
    End Interface
End Namespace

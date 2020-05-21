Namespace TaxonomyData
    Public Interface ILoad
        Function HandleTaxonomyDataLoadMessage(ByVal Base64Message As String, ByVal NumberOfTimesEncoded As Int32) As String
    End Interface
End Namespace
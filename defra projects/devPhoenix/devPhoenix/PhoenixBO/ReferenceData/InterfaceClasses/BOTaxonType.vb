Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.TaxonomyTaxonTypeService)), _
     EntityMapping(GetType(DataObjects.Entity.TaxonomyTaxonType)), _
     CollectionMapping(GetType(DataObjects.Collection.TaxonomyTaxonTypeBoundCollection)), _
     Serializable()> _
    Public Class BOTaxonType
        Inherits BO.ReferenceData.BOBaseReferenceTable

        Public Sub New()
            'This constructor is intentionally empty, but defined for explicitness.
        End Sub

        Public Sub New(ByVal TaxonTypeId As Int32)
            MyClass.New(TaxonTypeId, Nothing)
        End Sub

        Public Sub New(ByVal TaxonTypeId As Int32, ByVal tran As SqlClient.SqlTransaction)
            InitialiseTaxonType(DataObjects.Entity.TaxonomyTaxonType.GetById(TaxonTypeId, tran))
        End Sub

        Private Sub InitialiseTaxonType(ByVal TaxonType As DataObjects.Entity.TaxonomyTaxonType)
            ConvertDataObjectTOBO(Me, TaxonType)
        End Sub

        Protected Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
            Return DataObjects.Entity.TaxonomyTaxonType.GetAll(includeHyphen, includeInactive, DataObjects.Base.TaxonomyTaxonTypeServiceBase.OrderBy.IX_TaxonomyTaxonType)
        End Function
    End Class
End Namespace
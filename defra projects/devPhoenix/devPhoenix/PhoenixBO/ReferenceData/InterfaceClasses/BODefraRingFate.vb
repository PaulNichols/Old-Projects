
Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.DEFRARingFateService)), _
     EntityMapping(GetType(DataObjects.Entity.DEFRARingFate)), _
     CollectionMapping(GetType(DataObjects.Collection.DEFRARingFateBoundCollection)), _
     Serializable()> _
    Public Class BODefraRingFate
        Inherits BO.ReferenceData.BOBaseReferenceTable

        Protected Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
            Return DataObjects.Entity.DEFRARingFate.GetAll(includeHyphen, includeInactive, DataObjects.Base.DEFRARingFateServiceBase.OrderBy.IX_DEFRARingFate)
        End Function
    End Class

End Namespace

Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.SpecimenAgeStatusService)), _
     EntityMapping(GetType(DataObjects.Entity.SpecimenAgeStatus)), _
     CollectionMapping(GetType(DataObjects.Collection.SpecimenAgeStatusBoundCollection)), _
     Serializable()> _
    Public Class BOSpecimenAgeStatus
        Inherits BO.ReferenceData.BOBaseReferenceTable

        Protected Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
            Return DataObjects.Entity.SpecimenAgeStatus.GetAll(includeHyphen, includeInactive, DataObjects.Base.SpecimenAgeStatusServiceBase.OrderBy.IX_SpecimenAgeStatus)
        End Function
    End Class

End Namespace
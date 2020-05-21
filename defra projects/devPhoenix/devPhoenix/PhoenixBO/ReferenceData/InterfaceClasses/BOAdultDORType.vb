Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.AdultDORTypeService)), _
     EntityMapping(GetType(DataObjects.Entity.AdultDORType)), _
     CollectionMapping(GetType(DataObjects.Collection.AdultDORTypeBoundCollection)), _
     Serializable()> _
    Public Class BOAdultDORType
        Inherits BO.ReferenceData.BOBaseReferenceTable

        Protected Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
            Return DataObjects.Entity.AdultDORType.GetAll(includeHyphen, includeInactive, DataObjects.Base.AdultDORTypeServiceBase.OrderBy.IX_AdultDORType)
        End Function
    End Class

End Namespace

<ServiceMapping(GetType(DataObjects.Service.TitleService)), _
 EntityMapping(GetType(DataObjects.Entity.Title)), _
 CollectionMapping(GetType(DataObjects.Collection.TitleBoundCollection)), _
 Serializable()> _
Public Class BOTitles
    Inherits BO.ReferenceData.BOBaseReferenceTable

    Protected Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
        Return DataObjects.Entity.Title.GetAll(includeHyphen, includeInactive, DataObjects.Base.TitleServiceBase.OrderBy.IX_Titles)
    End Function
End Class

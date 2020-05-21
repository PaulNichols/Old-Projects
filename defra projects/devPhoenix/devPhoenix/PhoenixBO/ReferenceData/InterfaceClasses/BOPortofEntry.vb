
Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.PortOfEntryService)), _
     EntityMapping(GetType(DataObjects.Entity.PortOfEntry)), _
     CollectionMapping(GetType(DataObjects.Collection.PortOfEntryBoundCollection)), _
     Serializable()> _
    Public Class BOPortofEntry
        Inherits BO.ReferenceData.BOBaseReferenceTable

        Public Shared Shadows Function GetAll(ByVal includeHyphen As Boolean) As [DO].DataObjects.Collection.PortOfEntryBoundCollection
            Return DataObjects.Entity.PortOfEntry.GetAll(includeHyphen).Entities
        End Function
        Public Sub New()

        End Sub

        Protected Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
            Return DataObjects.Entity.PortOfEntry.GetAll(includeHyphen, includeInactive, DataObjects.Base.PortOfEntryServiceBase.OrderBy.IX_PortOfEntry)
        End Function
    End Class

End Namespace



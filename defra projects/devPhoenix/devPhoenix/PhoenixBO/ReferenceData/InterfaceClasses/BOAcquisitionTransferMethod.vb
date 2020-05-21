Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.AcquisitionTransferMethodService)), _
     EntityMapping(GetType(DataObjects.Entity.AcquisitionTransferMethod)), _
     CollectionMapping(GetType(DataObjects.Collection.AcquisitionTransferMethodBoundCollection)), _
     Serializable()> _
    Public Class BOAcquisitionTransferMethod
        Inherits BO.ReferenceData.BOBaseReferenceTable

        Protected Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
            Return DataObjects.Entity.AcquisitionTransferMethod.GetAll(includeHyphen, includeInactive, DataObjects.Base.AcquisitionTransferMethodServiceBase.OrderBy.IX_AcquisitionTransferMethod)
        End Function
    End Class

End Namespace

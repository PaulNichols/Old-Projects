Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.PaymentMethodService)), _
     EntityMapping(GetType(DataObjects.Entity.PaymentMethod)), _
     CollectionMapping(GetType(DataObjects.Collection.PaymentMethodBoundCollection)), _
     Serializable()> _
    Public Class BOPaymentMethod
        Inherits BO.ReferenceData.BOBaseReferenceTable

        Protected Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
            Return DataObjects.Entity.PaymentMethod.GetAll(includeHyphen, includeInactive, DataObjects.Base.PaymentMethodServiceBase.OrderBy.IX_PaymentMethod)
        End Function
    End Class

End Namespace

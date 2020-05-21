
Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.CustomsDocumentTypeService)), _
     EntityMapping(GetType(DataObjects.Entity.CustomsDocumentType)), _
     CollectionMapping(GetType(DataObjects.Collection.CustomsDocumentTypeBoundCollection))> _
    Public Class BOCustomsDocumentType
        Inherits BO.ReferenceData.BOBaseReferenceTable

        Public Sub New()

        End Sub

        Protected Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
            Return DataObjects.Entity.CustomsDocumentType.GetAll(includeHyphen, includeInactive, DataObjects.Base.CustomsDocumentTypeServiceBase.OrderBy.IX_CustomsDocumentType_Index)
        End Function
    End Class

End Namespace

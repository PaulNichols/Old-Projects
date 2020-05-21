
Namespace Party
    <ServiceMapping(GetType(DataObjects.Service.ContactTypeService)), _
     EntityMapping(GetType(DataObjects.Entity.ContactType)), _
     CollectionMapping(GetType(DataObjects.Collection.ContactTypeBoundCollection)), _
     Serializable()> _
    Public Class BOContactType
        Inherits BO.ReferenceData.BOBaseReferenceTable

        Private _ContactTypeGroup As Int32
        <DOtoBOMapping("GroupId")> _
        Public Property ContactTypeGroup() As Int32
            Get
                Return _ContactTypeGroup
            End Get
            Set(ByVal Value As Int32)
                _ContactTypeGroup = Value
            End Set
        End Property

        Protected Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
            Return DataObjects.Entity.ContactType.GetAll(includeHyphen, includeInactive, DataObjects.Base.ContactTypeServiceBase.OrderBy.IX_ContactType)
        End Function
    End Class
End Namespace
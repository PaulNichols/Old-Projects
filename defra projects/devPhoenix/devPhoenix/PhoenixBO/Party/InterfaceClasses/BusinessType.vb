
Namespace Party
    <ServiceMapping(GetType(DataObjects.Service.BusinessTypeService)), _
      EntityMapping(GetType(DataObjects.Entity.BusinessType)), _
      CollectionMapping(GetType(DataObjects.Collection.BusinessTypeBoundCollection)), _
      Serializable()> _
    Public Class BOBusinessType
        Inherits BO.ReferenceData.BOBaseReferenceTable

        Public Enum BusinessTypes
            None = 0
            Import_Agency
            Management_Authority
            Migrated
            Nursery
            Veterinary_Services
            Zoo
        End Enum

        Private _Default As Boolean
        <DOtoBOMapping("Default")> _
        Public Property [Default]() As Boolean
            Get
                Return _Default
            End Get
            Set(ByVal Value As Boolean)
                _Default = Value
            End Set
        End Property

        Protected Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
            Return DataObjects.Entity.BusinessType.GetAll(includeHyphen, includeInactive, DataObjects.Base.BusinessTypeServiceBase.OrderBy.IX_BusinessType)
        End Function
    End Class
End Namespace
Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.TaxonomyUsageTypeService)), _
     EntityMapping(GetType(DataObjects.Entity.TaxonomyUsageType)), _
     CollectionMapping(GetType(DataObjects.Collection.TaxonomyUsageTypeBoundCollection)), _
     Serializable()> _
    Public Class BOTaxonomyUsageType
        Inherits BO.ReferenceData.BOBaseReferenceTable
        Private _UsageTypeDescription As String

        <DOtoBOMapping("UsageTypeDescription")> _
        Public Property UsageTypeDescription() As String
            Get
                Return _UsageTypeDescription
            End Get
            Set(ByVal Value As String)
                _UsageTypeDescription = Value
            End Set
        End Property

        Public Sub New()
            'This constructor is intentionally empty, but defined for explicitness.
        End Sub

        Public Sub New(ByVal UsageTypeId As Int32)
            MyClass.New(UsageTypeId, Nothing)
        End Sub

        Public Sub New(ByVal UsageTypeId As Int32, ByVal tran As SqlClient.SqlTransaction)
            InitialiseUsageType(DataObjects.Entity.TaxonomyUsageType.GetById(UsageTypeId, tran))
        End Sub

        Private Sub InitialiseUsageType(ByVal UsageType As DataObjects.Entity.TaxonomyUsageType)
            ConvertDataObjectTOBO(Me, UsageType)
        End Sub

        Protected Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
            Return DataObjects.Entity.TaxonomyUsageType.GetAll(includeHyphen, includeInactive, DataObjects.Base.TaxonomyUsageTypeServiceBase.OrderBy.DefaultOrder)
        End Function
    End Class
End Namespace
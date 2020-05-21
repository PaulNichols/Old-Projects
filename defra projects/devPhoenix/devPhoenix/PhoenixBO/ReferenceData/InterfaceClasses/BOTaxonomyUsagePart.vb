Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.TaxonomyPartService)), _
     EntityMapping(GetType(DataObjects.Entity.TaxonomyPart)), _
     CollectionMapping(GetType(DataObjects.Collection.TaxonomyPartBoundCollection)), _
     Serializable()> _
    Public Class BOTaxonomyUsagePart
        Inherits BO.ReferenceData.BOBaseReferenceTable

        Private _PartDescription As String
        <DOtoBOMapping("PartDescription")> _
            Public Property PartDescription() As String
            Get
                Return _PartDescription
            End Get
            Set(ByVal Value As String)
                _PartDescription = Value
            End Set
        End Property

        Public Sub New()
            'This constructor is intentionally empty, but defined for explicitness.
        End Sub

        Public Sub New(ByVal PartId As Int32)
            MyClass.New(PartId, Nothing)
        End Sub

        Public Sub New(ByVal PartId As Int32, ByVal tran As SqlClient.SqlTransaction)
            InitialiseUsageType(DataObjects.Entity.TaxonomyPart.GetById(PartId, tran))
        End Sub

        Private Sub InitialiseUsageType(ByVal Part As DataObjects.Entity.TaxonomyPart)
            ConvertDataObjectTOBO(Me, Part)
        End Sub

        Protected Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
            Return DataObjects.Entity.TaxonomyPart.GetAll(includeHyphen, includeInactive, DataObjects.Base.TaxonomyPartServiceBase.OrderBy.IX_TaxonomyPart)
        End Function
    End Class
End Namespace
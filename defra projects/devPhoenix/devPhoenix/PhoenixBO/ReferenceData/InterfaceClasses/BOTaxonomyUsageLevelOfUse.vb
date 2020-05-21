Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.TaxonomyLevelOfUseService)), _
     EntityMapping(GetType(DataObjects.Entity.TaxonomyLevelOfUse)), _
     CollectionMapping(GetType(DataObjects.Entity.TaxonomyLevelOfUse)), _
     Serializable()> _
    Public Class BOTaxonomyUsageLevelOfUse
        Inherits BO.ReferenceData.BOBaseReferenceTable
        Public Sub New()
            'This constructor is intentionally empty, but defined for explicitness.
        End Sub

        Public Sub New(ByVal LevelOfUseId As Int32)
            MyClass.New(LevelOfUseId, Nothing)
        End Sub

        Public Sub New(ByVal LevelOfUseId As Int32, ByVal tran As SqlClient.SqlTransaction)
            InitialiseUsageType(DataObjects.Entity.TaxonomyLevelOfUse.GetById(LevelOfUseId, tran))
        End Sub

        Private _LevelOfUseDescription As String

        <DOtoBOMapping("LevelOfUseDescription")> _
        Public Property LevelOfUseDescription() As String
            Get
                Return _LevelOfUseDescription
            End Get
            Set(ByVal Value As String)
                _LevelOfUseDescription = Value
            End Set
        End Property

        Private Sub InitialiseUsageType(ByVal LevelOfUse As DataObjects.Entity.TaxonomyLevelOfUse)
            ConvertDataObjectTOBO(Me, LevelOfUse)
        End Sub

        Protected Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
            Return DataObjects.Entity.TaxonomyLevelOfUse.GetAll(includeHyphen, includeInactive, DataObjects.Base.TaxonomyLevelOfUseServiceBase.OrderBy.IX_TaxonomyLevelOfUse)
        End Function
    End Class
End Namespace
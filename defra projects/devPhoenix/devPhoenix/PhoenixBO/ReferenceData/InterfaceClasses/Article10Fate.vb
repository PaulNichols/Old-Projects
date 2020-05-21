Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.Article10FateService)), _
     EntityMapping(GetType(DataObjects.Entity.Article10Fate)), _
     CollectionMapping(GetType(DataObjects.Collection.Article10FateBoundCollection)), _
     Serializable()> _
    Public Class BOArticle10Fate
        Inherits BO.ReferenceData.BOBaseReferenceTable

        Public Sub New()
            MyBase.new()
        End Sub

        Public Sub New(ByVal Article10FateId As Int32)
            MyClass.New(Article10FateId, Nothing)
        End Sub

        Public Sub New(ByVal Article10FateId As Int32, ByVal tran As SqlClient.SqlTransaction)
            'Dim Country As New DataObjects.Entity.Country(countryId)
            InitialiseArticle10Fate(DataObjects.Entity.Article10Fate.GetById(Article10FateId, tran))
        End Sub

        Private Sub InitialiseArticle10Fate(ByVal Article10Fate As DataObjects.Entity.Article10Fate)
            ConvertDataObjectTOBO(Me, Article10Fate)
        End Sub

        Protected Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
            Return DataObjects.Entity.Article10Fate.GetAll(includeHyphen, includeInactive, DataObjects.Base.Article10FateServiceBase.OrderBy.ix_Article10Fate)
        End Function

#Region " DOToBOMapping "
        <DOtoBOMapping("Article10FateId")> _
Public Property Article10FateId() As Int32
            Get
                Return mArticle10FateId
            End Get
            Set(ByVal Value As Int32)
                mArticle10FateId = Value
            End Set
        End Property
        Private mArticle10FateId As Int32
#End Region
    End Class

End Namespace


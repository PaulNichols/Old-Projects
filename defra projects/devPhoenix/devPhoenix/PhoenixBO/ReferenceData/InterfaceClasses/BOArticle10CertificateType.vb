Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.Article10CertificateTypeService)), _
     EntityMapping(GetType(DataObjects.Entity.Article10CertificateType)), _
     CollectionMapping(GetType(DataObjects.Collection.Article10CertificateTypeBoundCollection)), _
     Serializable()> _
    Public Class BOArticle10CertificateType
        Inherits BO.ReferenceData.BOBaseReferenceTable

        Public Sub New()
        End Sub

        Public Sub New(ByVal article10CertificateTypeId As Int32)
            MyClass.New(article10CertificateTypeId, Nothing)
        End Sub

        Public Sub New(ByVal article10CertificateTypeId As Int32, ByVal tran As SqlClient.SqlTransaction)
            'Dim Country As New DataObjects.Entity.Country(countryId)
            InitialiseArticle10CertificateType(DataObjects.Entity.Article10CertificateType.GetById(article10CertificateTypeId, tran))
        End Sub

        Private Sub InitialiseArticle10CertificateType(ByVal article10CertificateType As DataObjects.Entity.Article10CertificateType)
            ConvertDataObjectTOBO(Me, article10CertificateType)
        End Sub

        Protected Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
            Return DataObjects.Entity.Article10CertificateType.GetAll(includeHyphen, includeInactive, DataObjects.Base.Article10CertificateTypeServiceBase.OrderBy.IX_Article10CertificateType)
        End Function
    End Class

End Namespace


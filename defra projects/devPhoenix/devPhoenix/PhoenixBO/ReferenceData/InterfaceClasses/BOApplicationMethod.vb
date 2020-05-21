Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.ApplicationMethodService)), _
     EntityMapping(GetType(DataObjects.Entity.ApplicationMethod)), _
     CollectionMapping(GetType(DataObjects.Collection.ApplicationMethodBoundCollection)), _
     Serializable()> _
    Public Class BOApplicationMethod
        Inherits BO.ReferenceData.BOBaseReferenceTable

        Public Sub New()
            MyBase.new()
        End Sub

        Public Sub New(ByVal applicationMethodId As Int32)
            MyClass.New(applicationMethodId, Nothing)
        End Sub

        Public Sub New(ByVal applicationMethodId As Int32, ByVal tran As SqlClient.SqlTransaction)
            'Dim Country As New DataObjects.Entity.Country(countryId)
            InitialiseApplicationMethod(DataObjects.Entity.ApplicationMethod.GetById(applicationMethodId, tran))
        End Sub

        Private Sub InitialiseApplicationMethod(ByVal applicationMethod As DataObjects.Entity.ApplicationMethod)
            ConvertDataObjectTOBO(Me, applicationMethod)
        End Sub

        Protected Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
            Return DataObjects.Entity.ApplicationMethod.GetAll(includeHyphen, includeInactive, DataObjects.Base.ApplicationMethodServiceBase.OrderBy.IX_ApplicationMethod)
        End Function
    End Class

End Namespace


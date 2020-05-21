
Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.PermitStatusService)), _
     EntityMapping(GetType(DataObjects.Entity.PermitStatus)), _
     CollectionMapping(GetType(DataObjects.Collection.PermitStatusBoundCollection)), _
     Serializable()> _
    Public Class BOPermitStatus
        Inherits BO.ReferenceData.BOBaseReferenceTable

        Public Sub New()

        End Sub

        Public Sub New(ByVal statusId As Int32)
            MyClass.New(statusId, Nothing)
        End Sub

        Public Sub New(ByVal statusId As Int32, ByVal tran As SqlClient.SqlTransaction)
            'Dim Country As New DataObjects.Entity.Country(countryId)
            InitialiseStatus(DataObjects.Entity.PermitStatus.GetById(statusId, tran))
        End Sub

        Private Sub InitialiseStatus(ByVal status As DataObjects.Entity.PermitStatus)
            ConvertDataObjectTOBO(Me, status)
        End Sub

        Protected Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
            Return DataObjects.Entity.PermitStatus.GetAll(includeHyphen, includeInactive, DataObjects.Base.PermitStatusServiceBase.OrderBy.IX_PermitStatus)
        End Function
    End Class

End Namespace

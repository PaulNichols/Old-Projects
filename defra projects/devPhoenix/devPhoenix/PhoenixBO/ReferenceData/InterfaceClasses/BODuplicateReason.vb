
Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.DuplicateReasonService)), _
     EntityMapping(GetType(DataObjects.Entity.DuplicateReason)), _
     CollectionMapping(GetType(DataObjects.Collection.DuplicateReasonBoundCollection)), _
     Serializable()> _
    Public Class BODuplicateReason
        Inherits BO.ReferenceData.BOBaseReferenceTable

        Public Sub New()
        End Sub

        Public Sub New(ByVal duplicateReasonId As Int32)
            MyClass.New(duplicateReasonId, Nothing)
        End Sub

        Public Sub New(ByVal duplicateReasonId As Int32, ByVal tran As SqlClient.SqlTransaction)
            InitialiseDuplicateReason(DataObjects.Entity.DuplicateReason.GetById(duplicateReasonId, tran))
        End Sub

        Private Sub InitialiseDuplicateReason(ByVal duplicateReason As DataObjects.Entity.DuplicateReason)
            ConvertDataObjectTOBO(Me, duplicateReason)
        End Sub

        Protected Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
            Return DataObjects.Entity.DuplicateReason.GetAll(includeHyphen, includeInactive, DataObjects.Base.DuplicateReasonServiceBase.OrderBy.IX_DuplicateReason_Description)
        End Function
    End Class
End Namespace



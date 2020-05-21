
Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.SpecimenFateService)), _
     EntityMapping(GetType(DataObjects.Entity.SpecimenFate)), _
     CollectionMapping(GetType(DataObjects.Collection.SpecimenFateBoundCollection)), _
     Serializable()> _
    Public Class BOSpecimenFate
        Inherits BO.ReferenceData.BOBaseReferenceTable

        Public Sub New()

        End Sub

        Public Sub New(ByVal specimenFateId As Int32)
            MyClass.New(specimenFateId, Nothing)
        End Sub

        Public Sub New(ByVal specimenFateId As Int32, ByVal tran As SqlClient.SqlTransaction)
            InitialiseFate(DataObjects.Entity.SpecimenFate.GetById(specimenFateId, tran))
        End Sub

        Private Sub InitialiseFate(ByVal specimenFate As DataObjects.Entity.SpecimenFate)
            ConvertDataObjectTOBO(Me, specimenFate)
        End Sub

        Protected Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
            Return DataObjects.Entity.SpecimenFate.GetAll(includeHyphen, includeInactive, DataObjects.Base.SpecimenFateServiceBase.OrderBy.IX_SpecimenFate)
        End Function
    End Class

End Namespace
Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.SpecimenReportOriginService)), _
     EntityMapping(GetType(DataObjects.Entity.SpecimenReportOrigin)), _
     CollectionMapping(GetType(DataObjects.Collection.SpecimenReportOriginBoundCollection)), _
     Serializable()> _
    Public Class BOSpecimenReportOrigin
        Inherits BO.ReferenceData.BOBaseReferenceTable

        Public Sub New()

        End Sub

        Public Sub New(ByVal specimenGenderId As Int32)
            MyClass.New(specimenGenderId, Nothing)
        End Sub

        Public Sub New(ByVal specimenReportOriginId As Int32, ByVal tran As SqlClient.SqlTransaction)
            Initialise(DataObjects.Entity.SpecimenReportOrigin.GetById(specimenReportOriginId, tran))
        End Sub

        Private Sub Initialise(ByVal reportOrigin As DataObjects.Entity.SpecimenReportOrigin)
            ConvertDataObjectTOBO(Me, reportOrigin)
        End Sub
    End Class
End Namespace

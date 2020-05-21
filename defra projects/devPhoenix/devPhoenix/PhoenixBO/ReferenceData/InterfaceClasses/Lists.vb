Imports uk.gov.defra.Phoenix.BO.ReferenceData
Imports uk.gov.defra.Phoenix.DO.DataObjects
Imports uk.gov.defra.Phoenix.DO.DataObjects.Collection

Namespace ReferenceData
    Public Class Lists
        Public Shared Function GetBusinessTypes(ByVal includeHyphen As Boolean) As BusinessTypeBoundCollection
            Dim BO As New Party.BOBusinessType
            Return CType(BO.GetAll(includeHyphen), BusinessTypeBoundCollection)
        End Function

        Public Shared Function GetContactTypes(ByVal includeHyphen As Boolean) As ContactTypeBoundCollection
            Dim BO As New Party.BOContactType
            Return CType(BO.GetAll(includeHyphen), ContactTypeBoundCollection)
        End Function

        Public Shared Function GetCountries(ByVal includeHyphen As Boolean, ByVal isoOnly As Boolean) As Entity.Country()
            '   Dim country As New ReferenceData.BOCountry
            Dim collection As CountrySet = CType([DO].DataObjects.Entity.Country.GetAll(includeHyphen, False, Base.CountryServiceBase.OrderBy.CodeDescription), CountrySet)
            Dim results(collection.Entities.Count - 1) As Entity.Country
            Dim i As Int32 = 0

            For Each item As Entity.Country In collection
                If Not isoOnly OrElse (i = 0 AndAlso includeHyphen) OrElse Not item.IsISO2CountryCodeNull Then
                    results(i) = item
                    i += 1
                End If
            Next
            ReDim Preserve results(i - 1)
            Return results
        End Function

        Public Shared Function GetCountriesForManagementCountry(ByVal ManagementCountryId As Int32) As CountryBoundCollection
            Dim CountryService As New Service.CountryService
            Dim CountryBoundCollection As New CountryBoundCollection
            CountryBoundCollection.EntitySet = CountryService.GetByIndex_IX_Country_1(ManagementCountryId, False)
            If CountryBoundCollection Is Nothing Then
                Return Nothing
            Else
                Return CountryBoundCollection
            End If

        End Function

        Public Shared Function GetSpecimenMarkType(ByVal includeHyphen As Boolean) As IDMarkTypeBoundCollection
            Dim BO As New ReferenceData.BOIDMarkType
            Return CType(BO.GetAll(includeHyphen), IDMarkTypeBoundCollection)
        End Function

        Public Shared Function GetSpecimenAgeStatus(ByVal includeHyphen As Boolean) As SpecimenAgeStatusBoundCollection
            Dim BO As New ReferenceData.BOSpecimenAgeStatus
            Return CType(BO.GetAll(includeHyphen), SpecimenAgeStatusBoundCollection)
        End Function

        Public Shared Function GetAcquisitionMethod(ByVal includeHyphen As Boolean) As AcquisitionTransferMethodBoundCollection
            Dim BO As New ReferenceData.BOAcquisitionTransferMethod
            Return CType(BO.GetAll(includeHyphen), AcquisitionTransferMethodBoundCollection)
        End Function

        Public Shared Function GetIDMarkTypes(ByVal includeHyphen As Boolean) As IDMarkTypeBoundCollection
            Dim BO As New ReferenceData.BOIDMarkType
            Return CType(BO.GetAll(includeHyphen), IDMarkTypeBoundCollection)
        End Function

        Public Shared Function GetCitesUnitOfMeasure(ByVal includeHyphen As Boolean, ByVal bIncludeInActive As Boolean) As CITESUnitOfMeasurementBoundCollection

            ' SZ This is used by the GWD Reference Data grid (cites_Parts/Derivatives) to
            ' populate the units of measure combo's
            Try
                Return BO.Application.CITES.BOCITESUnitOfMeasurementGroup.GetGridData(includeHyphen, bIncludeInActive)
            Catch ex As Exception

            End Try
        End Function

        Public Shared Function GetAllTaxonTypes() As TaxonomyTaxonTypeBoundCollection
            Dim BO As New ReferenceData.BOTaxonType
            Return CType(BO.GetAll(), TaxonomyTaxonTypeBoundCollection)
        End Function

        Public Shared Function GetAllUsageTypes() As TaxonomyUsageTypeBoundCollection
            Dim BO As New ReferenceData.BOTaxonomyUsageType
            Return CType(BO.GetAll(), TaxonomyUsageTypeBoundCollection)
        End Function

        Public Shared Function GetAllUsageParts() As TaxonomyPartBoundCollection
            Dim BO As New ReferenceData.BOTaxonomyUsagePart
            Return CType(BO.GetAll(), TaxonomyPartBoundCollection)
        End Function

        Public Shared Function GetAllUsageLevelsOfUse() As TaxonomyLevelOfUseBoundCollection
            Dim BO As New ReferenceData.BOTaxonomyUsageLevelOfUse
            Return CType(BO.GetAll(), TaxonomyLevelOfUseBoundCollection)
        End Function





        Public Shared Function GetApplicationTypes(ByVal blnIncludeHyphen As Boolean) As ApplicationTypeBoundCollection
            ' returns the visible columns for the tableid passedin
            Dim bo As New ReferenceData.BOApplicationType
            Return CType(bo.GetAll(blnIncludeHyphen, True), ApplicationTypeBoundCollection)
        End Function

        Public Shared Function GetApplicationMethods(ByVal includeHyphen As Boolean, ByVal includeActive As Boolean) As ApplicationMethodBoundCollection
            Dim BO As New ReferenceData.BOApplicationMethod
            Return CType(BO.GetAll(includeHyphen, includeActive), ApplicationMethodBoundCollection)
        End Function

        Public Shared Function GetReportCategory(ByVal includeHyphen As Boolean, ByVal includeActive As Boolean) As ReportCategoryBoundCollection
            Dim bc As New ReportCategoryBoundCollection
            bc.EntitySet = Entity.ReportCategory.GetAll(includeHyphen, includeActive, Base.ReportCategoryServiceBase.OrderBy.IX_ReportCategory)
            Return bc
        End Function

        Public Shared Function GetReportType(ByVal reportCategoryId As Int32, ByVal includeHyphen As Boolean, ByVal includeActive As Boolean) As ReportTypeBoundCollection
            Dim bc As New ReportTypeBoundCollection
            bc.EntitySet = Entity.ReportType.GetForReportCategory(reportCategoryId)
            Return bc
        End Function

        Public Shared Function GetArticle10Fate(ByVal includeHyphen As Boolean, ByVal includeActive As Boolean) As Article10FateBoundCollection
            Dim BO As New ReferenceData.BOArticle10Fate
            Return CType(BO.GetAll(includeHyphen, includeActive), Article10FateBoundCollection)
        End Function

        Public Shared Function GetSpecimenFate(ByVal includeHyphen As Boolean, ByVal includeActive As Boolean) As SpecimenFateBoundCollection
            Dim BO As New ReferenceData.BOSpecimenFate
            Return CType(BO.GetAll(includeHyphen, includeActive), SpecimenFateBoundCollection)
        End Function

        Private Shared Sub AddHyphen(ByVal Ds As DataSet)
            If Not Ds Is Nothing AndAlso Ds.Tables.Count > 0 Then
                Ds.Tables(0).ImportRow(Ds.Tables(0).Rows(0))
                Ds.Tables(0).Rows(Ds.Tables(0).Rows.Count - 1).Item("FullName") = "-"
            End If
        End Sub

        Public Shared Function GetAllUsers(ByVal appId As Int32, ByVal includeHyphen As Boolean) As DataSet
            Dim UsersDs As DataSet = uk.gov.defra.Phoenix.BO.Common.GetUsers(appId, uk.gov.defra.Phoenix.BO.Common.RolesList.All)
            If includeHyphen Then AddHyphen(UsersDs)
            Return UsersDs
        End Function

        Public Shared Function GetAllCOs(ByVal appId As Int32, ByVal includeHyphen As Boolean) As DataSet
            Dim Ds As DataSet = uk.gov.defra.Phoenix.BO.Common.GetUsers(appId, uk.gov.defra.Phoenix.BO.Common.RolesList.CaseOfficer)
            If includeHyphen Then AddHyphen(Ds)
            Return Ds
        End Function

        Public Shared Function GetDuplicateReasons(ByVal includeHyphen As Boolean, ByVal includeActive As Boolean) As DuplicateReasonBoundCollection
            Dim BO As New ReferenceData.BODuplicateReason
            Return CType(BO.GetAll(includeHyphen), DuplicateReasonBoundCollection)
        End Function

        Public Shared Function GetCustomsDocumentType(ByVal includeHyphen As Boolean) As CustomsDocumentTypeBoundCollection
            Dim BO As New ReferenceData.BOCustomsDocumentType
            Return CType(BO.GetAll(includeHyphen), CustomsDocumentTypeBoundCollection)
        End Function

        Public Shared Function GetRejectionReasons(ByVal includeHyphen As Boolean) As RefusalReasonBoundCollection
            Dim BO As New ReferenceData.BORefusalReason
            Return CType(BO.GetAll(includeHyphen), RefusalReasonBoundCollection)
        End Function

        Public Shared Function GetSpecimenReportOrigin(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As SpecimenReportOriginBoundCollection
            Dim BO As New ReferenceData.BOSpecimenReportOrigin
            Return CType(BO.GetAll(includeHyphen, includeInactive), SpecimenReportOriginBoundCollection)
        End Function

        Public Shared Function GetReferredToParties(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As StatusAssignedToGroupBoundCollection
            Dim BO As New ReferenceData.BOStatusAssignedToGroup
            Return CType(BO.GetAll(includeHyphen, includeInactive, Base.StatusAssignedToGroupServiceBase.OrderBy.IX_StatusAssignedToGroup), StatusAssignedToGroupBoundCollection)
        End Function

        Public Shared Function GetScientificAdvice(ByVal includeHyphen As Boolean) As ScientificAdviceBoundCollection
            Dim BO As New ReferenceData.BOScientificAdvice
            Return CType(BO.GetAll(includeHyphen), ScientificAdviceBoundCollection)
        End Function
    End Class
End Namespace

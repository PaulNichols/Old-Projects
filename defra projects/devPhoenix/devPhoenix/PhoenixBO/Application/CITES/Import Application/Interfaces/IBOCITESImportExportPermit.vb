Namespace Application.CITES.Applications
    Public Interface IBOCITESImportExportPermit
        Property CITESImportExportPermitId() As Int32
        Property CofLExportNumber() As String 'Article 10 - country of import date
        Property CofLExportIssueDate() As Object 'Article 10 - country of import number
        Property CofLExportPermitExpiryDate() As Object
        Property EULicenseNumber() As Object
        Property CITESImportExportPermitChecksum() As Int32
        Property CofLExport() As ReferenceData.BOCountry 'Article 10 - country of import
        Property LatestSAAdvice() As BO.Application.CITES.Applications.BOPermitScientificAdvice
        Property PreviousCertificateNumber() As Object
        Property PreviousCertificateIssueDate() As Object
    End Interface
End Namespace

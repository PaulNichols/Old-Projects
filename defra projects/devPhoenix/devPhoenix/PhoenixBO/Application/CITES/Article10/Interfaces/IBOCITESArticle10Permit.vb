Namespace Application.CITES.Applications
    Public Interface IBOCITESArticle10Permit
        Inherits IBOCITESImportExportPermit

       

        Property MemberStateOfImport() As ReferenceData.BOCountry
        Property MemberStateOfImportDocumentNumber() As String
        Property MemberStateOfImportDateOfIssue() As Object
        Property TranOrSpecType() As String

        Property IsTransactionSpecific() As Object
    End Interface
End Namespace
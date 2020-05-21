Namespace Application.CITES.ImportNotification
    Public Interface IBOImportSpecie
        Inherits IBOSpecie

        Property ExportPermitNumber() As Object
        Property ExportNumberDateOfIssue() As Date
        Property CertificateOfOriginNumber() As Object
        Property Section() As String
        Property CheckSum() As Int32
        Property Id() As Int32
    End Interface
End Namespace

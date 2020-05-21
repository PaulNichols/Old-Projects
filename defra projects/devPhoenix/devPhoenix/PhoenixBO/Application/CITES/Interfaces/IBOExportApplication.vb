Namespace Application.CITES
    Public Interface IBOExportApplication

        Property Importer() As BOApplicationPartyDetails
        Property Exporter() As BOApplicationPartyDetails
        Property ContryOfExport() As ReferenceData.BOCountry
        Property ContryOfImport() As ReferenceData.BOCountry
        Property Reexport() As Boolean
        Property NoOfCopies() As Int32
    End Interface
End Namespace
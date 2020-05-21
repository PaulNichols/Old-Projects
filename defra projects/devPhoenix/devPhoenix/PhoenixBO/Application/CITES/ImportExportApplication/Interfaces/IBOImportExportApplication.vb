Namespace Application.CITES.Applications
    Public Interface IBOImportExportApplication

        Property Importer() As BOApplicationPartyDetails
        Property Exporter() As BOApplicationPartyDetails
        Property CountryOfExport() As BO.ReferenceData.BOCountry

        Property ImportExportApplicationId() As Int32
        Property ImportExportApplicationCheckSum() As Int32

        Property RegionOfExport() As BO.ReferenceData.BOUKCountry
        Property RegionOfImport() As BO.ReferenceData.BOUKCountry
        'Overloads Function Validate(ByVal writeFlag As Boolean, ByVal ignoreWarnings As Boolean) As ValidationManager

    End Interface
End Namespace

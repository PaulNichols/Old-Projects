Namespace Application.CITES.Applications
    Public Interface IBOExportApplication
        Inherits IBOImportExportApplication

        Property ExportApplicationId() As Int32
        Property ExportApplicationCheckSum() As Int32

        Property ReExport() As Boolean

        'Overloads Function Validate(ByVal writeFlag As Boolean, ByVal ignoreWarnings As Boolean) As BaseBO
    End Interface
End Namespace
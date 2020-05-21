Namespace Application.CITES.Applications
    Public Interface IBOImportApplication
        Inherits IBOImportExportApplication

        Property ImportApplicationId() As Int32
        Property ImportApplicationCheckSum() As Int32

        'Overloads Function Validate(ByVal writeFlag As Boolean, ByVal ignoreWarnings As Boolean) As Object
    End Interface
End Namespace
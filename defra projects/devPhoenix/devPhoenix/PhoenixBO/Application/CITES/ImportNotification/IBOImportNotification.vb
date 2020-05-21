Namespace Application.CITES
    Public Interface IBOImportNotification
        Inherits IBOCITESNotification

        Property ImportNotificationId() As Int32
        Property RegionOfImportId() As Object
        Property RegionOfImport() As String
        Property CountryOfOriginRegionId() As Object
        Property CountryOfOriginRegion() As String
        Property CheckSum() As Int32

        ' Overloads Function Validate(ByVal writeFlag As Boolean) As ValidationManager
    End Interface
End Namespace
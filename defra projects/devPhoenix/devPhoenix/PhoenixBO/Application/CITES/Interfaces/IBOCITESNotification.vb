Namespace Application.CITES
    Public Interface IBOCITESNotification
        Inherits IValidate

        Property Agent() As Application.BOApplicationPartyDetails
        Property CountryOfOrigin() As ReferenceData.BOCountry
        Property NotificationDate() As Date
        Property CountryOfExport() As ReferenceData.BOCountry
        Property CountryOfImport() As ReferenceData.BOCountry
        Property Id() As Int32
        Property Active() As Boolean
        Property Validated() As Boolean
        Property Party() As Application.BOApplicationPartyDetails
        Property Specie() As Application.CITES.BONotificationSpecie()
        'Property DerivitiveId() As Object
        'Property DerivitiveName() As String
        Property Reference() As String
        '  Property UOM() As BOMeasurement
        Property DateUnknown() As Boolean
        Property RecievedDate() As Object
        Property UnknownCountryOfExport() As Boolean


    End Interface
End Namespace
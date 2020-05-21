Namespace Party
    Public Interface IAddress
        Inherits IValidate

        Property HouseNumber() As String
        Property AddressId() As Int32
        Property Address1() As String
        Property Address2() As String
        Property Address3() As String
        Property Address4() As String
        Property Town() As String
        Property County() As String
        Property Postcode() As String
        Property IsTemporary() As Boolean
        Property Active() As Boolean
        Property ActiveString() As String
        Property ContactName() As Object
        Property CountryId() As Int32
        Property Country() As String
        Property RegionId() As Object
        Property Region() As String
        Property IsMailing() As Boolean
        Property IsMailingString() As String
        Property PartyId() As Int32
        Property DisplayAddress() As String
        Property ReportAddress() As String
        Property BuildingName() As String
        Property OrganisationName() As String

    End Interface
End Namespace
Public Interface IBusiness
    Property BusinessId() As Int32
    Property BusinessTypeId() As Int32
    Property BusinessName() As String
    Property CITESRegisteredNumber() As String
    Property BusinessType() As String
    Property DisplayName() As String
    Property DefaultMAForCountry() As Boolean
    Function GetRepresentatives() As Party.BOPerson()

    Property CheckSum() As Int32
End Interface

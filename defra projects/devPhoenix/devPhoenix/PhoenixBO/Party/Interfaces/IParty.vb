Public Interface IParty
    Inherits IValidate

    Property PartyId() As Int32
    Property ExcludeFromMailingList() As Boolean
    Property MailingAddressId() As Object
    Property GatewayPreferredContactDetailId() As Object

    Property AllowSemicompleteCitesImport() As Boolean
    Property AllowSemicompleteCitesExport() As Boolean
    Property AllowSemicompleteCitesArticle10() As Boolean
    Property AllowIncompleteImportApplications() As Boolean

    Property RequireKnownFacts() As Boolean                 'MLD 14/3/5 spelling corrected
    Property Validated() As Object
    Property PreviousName() As Object
    Overloads Function Validate(ByVal writeFlag As Boolean) As ValidationManager

    Property AuthorisedPartyId() As Int32
End Interface

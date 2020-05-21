Public Interface IPerson
    Inherits IValidate

    Property PersonId() As Int32
    Property Title() As String
    Property Forename() As Object
    Property Surname() As Object
    Property PreferredContactId() As Object
    Property TitleId() As Int32
    Property PartyId() As Object
    Property DisplayName() As String

    Overloads Function Validate(ByVal userid As Int64, ByVal secure As Boolean) As ValidationManager
    Function GetContacts() As Party.BOContact()
    Function Save() As Party.BOPerson
    Function Save(ByVal tran As SqlClient.SqlTransaction) As Party.BOPerson
End Interface


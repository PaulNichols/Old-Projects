Public Interface IContact
    Inherits IValidate

    Property ContactId() As Int32
    Property Detail() As String
    Property Location() As String
    Property Active() As Boolean
    Property ActiveString() As String
    Property PersonId() As Int32

    Property ContactTypeId() As Int32
    Property ContactType() As String
    Property IsPreferredString() As String
    Property IsPreferred() As Boolean
End Interface

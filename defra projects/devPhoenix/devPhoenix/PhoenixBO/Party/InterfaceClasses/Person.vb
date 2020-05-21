Namespace Party
    Public Class BOPerson
        Inherits BaseBO
        Implements IPerson

        Private mDisplayName As String

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal data As DataObjects.Entity.Person)
            MyClass.New()
            InitialisePerson(data)
        End Sub

        Public Sub New(ByVal data As DataObjects.Entity.Person, ByVal partyId As Int32)
            MyClass.New(data)
            mPartyId = partyId
        End Sub

        Public Property DisplayName() As String Implements IPerson.DisplayName
            Get
                Return mDisplayName
            End Get
            Set(ByVal Value As String)
                mDisplayName = Value
            End Set
        End Property

        <BusinessPropertyInformation(True)> _
        Public Property Forename() As Object Implements IPerson.Forename
            Get
                Return mForename
            End Get
            Set(ByVal Value As Object)
                mForename = Value
            End Set
        End Property
        Private mForename As Object

        Public Property PersonId() As Integer Implements IPerson.PersonId
            Get
                Return mPersonId
            End Get
            Set(ByVal Value As Integer)
                mPersonId = Value
            End Set
        End Property
        Private mPersonId As Int32

        <BusinessPropertyInformation(True)> _
        Public Property preferredContactId() As Object Implements IPerson.preferredContactId
            Get
                Return mpreferredContactId
            End Get
            Set(ByVal Value As Object)
                mpreferredContactId = Value
            End Set
        End Property
        Private mpreferredContactId As Object

        <BusinessPropertyInformation(True)> _
        Public Property Surname() As Object Implements IPerson.Surname
            Get
                Return mSurname
            End Get
            Set(ByVal Value As Object)
                mSurname = Value
            End Set
        End Property
        Private mSurname As Object

        Public Property Title() As String Implements IPerson.Title
            Get
                If mTitle Is Nothing Then
                    Dim TitleData As DataObjects.Entity.Title = DataObjects.Entity.Title.GetById(mTitleId)
                    If Not TitleData Is Nothing Then mTitle = TitleData.Description
                End If
                Return mTitle
            End Get
            Set(ByVal Value As String)
                mTitle = Value
            End Set
        End Property
        Private mTitle As String

        <BusinessPropertyInformation(True)> _
        Public Property TitleId() As Integer Implements IPerson.TitleId
            Get
                Return mTitleId
            End Get
            Set(ByVal Value As Integer)
                mTitleId = Value
            End Set
        End Property
        Private mTitleId As Int32

        Public Property PartyId() As Object Implements IPerson.PartyId
            Get
                Return mPartyId
            End Get
            Set(ByVal Value As Object)
                mPartyId = Value
            End Set
        End Property
        Private mPartyId As Object

        Public Function GetContacts() As BOContact() Implements IPerson.GetContacts
            Return GetContacts(PersonId)
        End Function

        Shared Function GetContacts(ByVal personId As Int32) As BOContact()
            Try
                Dim Person As New DataObjects.Entity.Person(personId)
                Return GetContacts(Person)
            Catch
            End Try
        End Function

        Shared Function GetContacts(ByVal person As DataObjects.Entity.Person) As BOContact()
            Dim Contacts As DataObjects.EntitySet.ContactSet = person.GetRelatedContacts

            If Not Contacts Is Nothing AndAlso _
               Contacts.Count > 0 Then
                Dim ContactsList(Contacts.Count - 1) As BOContact
                Dim Index As Int32 = 0
                For Each contact As DataObjects.Entity.Contact In Contacts
                    ContactsList(Index) = New Party.BOContact(contact, person.PreferredContactId, person.Id)
                    Index += 1
                Next contact
                Return ContactsList
            Else
                Return Nothing
            End If
        End Function

        Private Sub InitialisePerson(ByVal person As DataObjects.Entity.Person)
            With person
                mPersonId = .Id
                If Not .IsDisplayNameNull Then mDisplayName = .DisplayName

                mTitleId = .TitleId
                mTitle = Nothing

                'If Not .IsPartyIdNull Then mPartyId = .PartyId
                If Not .IsSurnameNull Then mSurname = .Surname
                If Not .IsForenameNull Then mForename = .Forename
                If Not .IsPreferredContactIdNull Then mpreferredContactId = .PreferredContactId

                CheckSum = .CheckSum
            End With
        End Sub

        Public Shadows Function Save() As BOPerson Implements IPerson.Save
            Return Me.Save(Nothing)
        End Function

        Public Shadows Function Save(ByVal tran As SqlClient.SqlTransaction) As BOPerson Implements IPerson.Save
            Created = (PersonId = 0)
            Dim NewPerson As New DataObjects.Entity.Person
            Dim service As DataObjects.Service.PersonService = NewPerson.ServiceObject

            If Created Then
                NewPerson = service.Insert(mPartyId, _
                                           mTitleId, _
                                           mForename, _
                                           mSurname, _
                                           mpreferredContactId, _
                                           tran)
            Else
                NewPerson = service.Update(mPersonId, _
                                           mPartyId, _
                                           mTitleId, _
                                           mForename, _
                                           mSurname, _
                                           mpreferredContactId, _
                                           CheckSum, _
                                           tran)
            End If
            If NewPerson Is Nothing AndAlso Not DataObjects.Sprocs.LastError Is Nothing Then
                'TODO: Use errors collection to check to see if the problem was concurrency
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSavePerson)
            ElseIf NewPerson Is Nothing Then
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSavePerson)
            Else
                InitialisePerson(NewPerson)
            End If
            Return Me
        End Function

        Public Overloads Function Validate() As ValidationManager Implements IValidate.Validate

        End Function

        Public Overloads Function Validate(ByVal userid As Int64, ByVal secure As Boolean) As ValidationManager Implements IPerson.Validate
            Dim ErrorList As New ArrayList

            ''does the party have at least one contact
            Dim PersonContacts As BO.Party.BOContact() = Me.GetContacts
            If (PersonContacts Is Nothing OrElse PersonContacts.Length = 0) AndAlso Not secure Then
                ErrorList.Add(New ValidationError(ValidationError.ValidationCodes.ContactCountMismatch))
            End If

            If Not PersonContacts Is Nothing AndAlso PersonContacts.Length = 1 Then
                preferredContactId = PersonContacts(0).ContactId
            End If


            If (preferredContactId Is Nothing OrElse CType(preferredContactId, Int32) = 0) And Not secure Then
                ErrorList.Add(New ValidationError(ValidationError.ValidationCodes.NoPrimaryContact))
            ElseIf Not (preferredContactId Is Nothing OrElse CType(preferredContactId, Int32) = 0) Then
                'they have a contact - verify that it exists :o)
                If New DataObjects.Entity.Contact(CType(preferredContactId, Int32)) Is Nothing Then
                    ErrorList.Add(New ValidationError(ValidationError.ValidationCodes.ContactNoLongerExists))
                End If
            End If

            'ensure that at least one of their contacts is active
            '...and...
            'ensure that all of the contacts are valid
            Dim FoundActive As Boolean = False
            Dim FoundInvalid As Boolean = False
            Dim HasEmail As Boolean
            If Not PersonContacts Is Nothing Then
                For Each contact As BO.Party.BOContact In PersonContacts
                    'replace with enum?
                    If Not HasEmail Then HasEmail = (New [DO].DataObjects.Entity.ContactType(contact.ContactTypeId).GroupId = 3)
                    If Not FoundActive AndAlso contact.Active Then
                        'inactive address
                        FoundActive = True
                    End If
                    If Not FoundInvalid AndAlso Not contact.Validate Is Nothing Then
                        'invalid address
                        FoundInvalid = True
                    End If
                Next contact
            End If

            If (userid = 0 And Not HasEmail) AndAlso Not secure Then ErrorList.Add(New ValidationError(ValidationError.ValidationCodes.NeedOneEmailAddress))
            If Not FoundActive AndAlso Not secure Then ErrorList.Add(New ValidationError(ValidationError.ValidationCodes.NeedOneActiveContact))
            If FoundInvalid Then ErrorList.Add(New ValidationError(ValidationError.ValidationCodes.ContactValidationError))

            PersonContacts = Nothing

            If ErrorList.Count > 0 Then
                ' Dim Errors As ValidationError() = CType(, ValidationError())
                Dim Validation As New ValidationManager(ErrorList.ToArray, ValidationManager.ValidationTitles.CannotSavePerson)
                Return Validation
            Else
                Return Nothing
            End If
        End Function

    End Class
End Namespace
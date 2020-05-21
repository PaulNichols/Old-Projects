Namespace Party
    '<Serializable(), Xml.Serialization.XmlInclude(GetType(Party.BOParty.Defaults))> _
    Public Class BOParty
        Inherits BaseBO
        ', Xml.Serialization.XmlRoot("#BOParty=IsBusiness=True")
        ', System.Xml.Serialization.XmlRoot("Fred")> _
        ', Xml.Serialization.XmlType(Namespace:="#BOParty=IsBusiness=True")> _
        Implements IParty

        Public Class Defaults
            Public IsBusiness_True As Boolean
        End Class

        Public Function GetPaymentAccountDetails() As PartyBankDetail()

            Dim paymentSet As DataObjects.EntitySet.PaymentSet
            Dim chequeSet As DataObjects.EntitySet.ChequeSet
            Dim partyBankDetails(-1) As partyBankDetail
            Dim partyBankDetail As partyBankDetail
            Dim hsTable As New Hashtable
            Dim key As String

            paymentSet = DataObjects.Entity.Payment.GetForParty(mPartyId)
            Dim idx As Int32 = -1
            For Each payment As DataObjects.Entity.Payment In paymentSet
                chequeSet = payment.GetRelatedCheque()
                For Each cheque As DataObjects.Entity.Cheque In chequeSet
                    partyBankDetail = New partyBankDetail
                    partyBankDetail.SortCode = cheque.BankSortCode()
                    partyBankDetail.AccountNumber = cheque.BankAccountNumber()
                    key = partyBankDetail.AccountNumber & "%*%" & partyBankDetail.SortCode
                    If Not hsTable.ContainsKey(key) Then
                        hsTable.Add(key, partyBankDetail)
                        idx += 1
                        ReDim Preserve partyBankDetails(idx)
                        partyBankDetails(idx) = partyBankDetail
                    End If
                Next
            Next

            Return partyBankDetails
        End Function

        Public Function SavePartyWithoutValidation() As BOParty
            Me.Validated = True
            Return Me.Save
            'reset the validate flag to false?
        End Function

        Public Function GetGridNotes(ByVal partyId As Int32) As Note.GridNote()
            Dim Notes As Note() = GetNotes(partyId)
            Dim GridNotes(Notes.Length - 1) As Note.GridNote

            Dim index As Int32
            For Each Note As Note In Notes
                GridNotes(index) = New Note.GridNote(Note)
                index += 1
            Next

            Return GridNotes
        End Function

        Private mKnownFacts As Party.BOKnownFacts

        Public Shared Function RetrievePartyByKnownFacts(ByVal fact1 As Object, ByVal fact2 As Object) As BO.Party.BOParty
            ' Should check if Recorded before loading from object, as postcode may have changed.
            Dim myEntSet As DataObjects.EntitySet.tblKnownFactsSet = DataObjects.Sprocs.eosp_SelecttblKnownFacts(Nothing, fact1, fact2, Nothing, Nothing, Nothing, Nothing, Nothing)
            Dim myEnt As DataObjects.Entity.tblKnownFacts = Nothing
            If Not myEntSet Is Nothing Then
                myEnt = myEntSet.Entities(0)
            End If
            If Not myEntSet Is Nothing AndAlso Not myEnt Is Nothing Then Return PolymorphicCreate(myEnt.PartyId)
        End Function

        Public Shared Function RetrievePartyByKnownFactId(ByVal id As Long) As BO.Party.BOParty
            ' Should check if Recorded before loading from object, as postcode may have changed.
            Dim myEntSet As DataObjects.EntitySet.tblKnownFactsSet = DataObjects.Sprocs.eosp_SelecttblKnownFacts(Nothing, Nothing, Nothing, id, Nothing, Nothing, Nothing, Nothing)
            Dim myEnt As DataObjects.Entity.tblKnownFacts = Nothing
            If Not myEntSet Is Nothing Then
                myEnt = myEntSet.Entities(0)
            End If
            If Not myEntSet Is Nothing AndAlso Not myEnt Is Nothing Then Return PolymorphicCreate(myEnt.PartyId)
        End Function

        Private Sub RetrieveKnownFacts(ByVal userId As Int64, ByVal tran As SqlClient.SqlTransaction)

            ' Should check if Recorded before loading from object, as postcode may have changed.
            Dim myEnt As DataObjects.Entity.tblKnownFacts

            Try
                myEnt = DataObjects.Entity.tblKnownFacts.GetForParty(mPartyId, tran).Entities(0)
                mKnownFacts = New Party.BOKnownFacts(myEnt)
            Catch ex As Exception
            End Try

            Dim blnEntNothing As Boolean = True
            Dim blnKF1Len As Boolean = False
            Dim blnKF2Len As Boolean = False

            Dim strKF1 As String
            Dim strKF2 As String

            Try
                blnEntNothing = (myEnt Is Nothing)
            Catch ex As Exception
                blnEntNothing = True
            End Try

            Try
                blnKF1Len = (myEnt.KnownFact1.ToString.Length > 0)
                ' If it exists, then it's true
                strKF1 = myEnt.KnownFact1
            Catch ex As Exception
                ' This is where it gets populated for the first time ...
                If AuthorisedPartyId > 0 Then
                    strKF1 = SetFirstKnownFact(AuthorisedPartyId)
                    blnKF1Len = (strKF1.ToString.Length > 0)
                Else
                    mKnownFacts = Nothing
                    Return
                End If
            End Try

            Dim strKFPostcode As String
            Dim blnUsePostcode As Boolean = False

            Try
                blnKF2Len = (myEnt.KnownFact2.Length > 0)
                strKF2 = myEnt.KnownFact2
            Catch ex As Exception
                ' Will be false if not to be reset.
                blnUsePostcode = True
            End Try

            ' If they did not have a Postcode, and not UK, then retrieve a Default
            Dim intCountryId As Integer
            Dim lstrPostcode As String

            Dim Config As New BOConfiguration
            Dim Result As Object = Config.GetValue("DefaultPostcodeForKnownFactIfNonUK")
            If Not Result Is Nothing AndAlso TypeOf Result Is String Then lstrPostcode = Result.ToString()

            Result = Config.GetValue("UKCountryIdFromDatabase")
            If Not Result Is Nothing AndAlso TypeOf Result Is Int32 Then intCountryId = CType(Result, Int32)

            If blnUsePostcode AndAlso blnKF1Len Then
                If blnKF1Len AndAlso Not blnKF2Len Then
                    ' we have a first known fact, but not a second.
                    Try
                        Dim strPC As String = GetMailingAddress(tran).Postcode()
                        Dim intPCLen As Integer = 0
                        Try
                            intPCLen = strPC.Length
                        Catch ex As Exception ' No Mailing Address postcode.
                        End Try

                        If GetMailingAddress(tran).CountryId <> intCountryId And intPCLen = 0 Then
                            ' If it is not UK, take the default
                            strKFPostcode = RemoveSpace(lstrPostcode)
                        Else
                            ' Only required if the Known Fact doesn't exist.
                            strKFPostcode = RemoveSpace(strPC)
                        End If

                        Dim intKFPostcodeLength As Int32 = strKFPostcode.Length
                        If intKFPostcodeLength > 7 Then intKFPostcodeLength = 7
                        strKF2 = strKFPostcode.Substring(0, intKFPostcodeLength)
                        blnKF2Len = (strKF2.Length > 0)
                    Catch ex As Exception
                        strKFPostcode = String.Empty
                        blnKF2Len = False
                    End Try
                Else
                    ' We don't have either known fact, so we do nothing.

                End If
            Else
                ' We don't have either known fact, so we do nothing.
            End If


            If blnEntNothing AndAlso (blnKF1Len AndAlso blnKF2Len) Then
                ' blnEntNothing is false if it already exists in the database
                ' Other booleans are if we have data in them.
                Try

                    ' Brand new Known Facts - will need to be inserted.
                    Dim myBO As Party.BOKnownFacts = New Party.BOKnownFacts(strKF1, strKF2, mPartyId)
                    myBO.UpdateKnownFact = True
                    mKnownFacts = myBO

                Catch ex As Exception
                    mKnownFacts = Nothing
                End Try

            End If

        End Sub

        Private Function SetFirstKnownFact(ByVal id As Integer) As String
            Dim Result As String = String.Empty
            If id > 0 Then
                Try
                    Dim strVal As String = id.ToString
                    If strVal.Length < 10 Then
                        Dim strPadded As String = "1" & LeftPad(id, 9)
                        Result = strPadded
                    Else
                        Result = strVal
                    End If
                Catch ex As Exception
                End Try
            End If

            Return Result
        End Function

        Private Function LeftPad(ByVal id As Integer, ByVal intLen As Integer) As String
            Dim strB As System.Text.StringBuilder = New System.Text.StringBuilder("")
            Dim str As String = id.ToString
            strB.Append("0"c, intLen - str.Length)
            strB.Append(str)
            Return strB.ToString

        End Function
        Private Function RemoveSpace(ByVal strValue As String) As String
            Dim strValNoSpace As String(), intLoop As Integer
            Dim strRet As New System.Text.StringBuilder("")

            strValNoSpace = strValue.Split(" "c)
            For intLoop = 0 To strValNoSpace.Length - 1
                strRet.Append(strValNoSpace(intLoop))
            Next

            Return strRet.ToString

        End Function

        Public Property KnownFacts() As Party.BOKnownFacts
            Set(ByVal Value As Party.BOKnownFacts)
                mKnownFacts = Value
            End Set
            Get
                '#TODO Identify Known Facts.
                ' Will probably replace this with specific code
                ' to return just the specific known facts, rather than 
                ' this array. 
                If mKnownFacts Is Nothing Then
                    'If mKnownFacts.CheckSum = 0 Then
                    'Should be populated by the load from tblKnownFacts ... if not, 
                    'check if we have the data in the object.
                    RetrieveKnownFacts(0, Nothing)
                    'End If
                End If

                Return mKnownFacts
            End Get
        End Property

        Public Property IsBusiness() As Boolean
            Get
                Return mIsBusiness
            End Get
            Set(ByVal Value As Boolean)
                mIsBusiness = Value
            End Set
        End Property
        Private mIsBusiness As Boolean

        Public Sub New()
        End Sub

        Public Sub New(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction)
            LoadParty(id, tran)
        End Sub

        Public Sub New(ByVal id As Int32)
            LoadParty(id, Nothing)
        End Sub

        Private Function LoadParty(ByVal id As Int32) As DataObjects.Entity.Party
            Return Me.LoadParty(id, Nothing)
        End Function

        Private Function LoadParty(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.Party
            Dim NewParty As DataObjects.Entity.Party = DataObjects.Entity.Party.GetById(id, tran)
            If NewParty Is Nothing Then
                Throw New RecordDoesNotExist("Party", id)
            Else
                InitialiseParty(NewParty, tran)
                Return NewParty
            End If
        End Function

#Region " Properties "
        Private mAllowIncompleteCitesImport As Boolean
        Private mAllowIncompleteCitesExport As Boolean
        Private mAllowIncompleteCitesArticle10 As Boolean
        Private mAllowIncompleteImportApplications As Boolean

        Public Property AllowSemicompleteCitesImport() As Boolean Implements IParty.AllowSemicompleteCitesImport
            Get
                Return mAllowIncompleteCitesImport
            End Get
            Set(ByVal Value As Boolean)
                mAllowIncompleteCitesImport = Value
            End Set
        End Property

        Public Property AllowSemicompleteCitesExport() As Boolean Implements IParty.AllowSemicompleteCitesExport
            Get
                Return mAllowIncompleteCitesExport
            End Get
            Set(ByVal Value As Boolean)
                mAllowIncompleteCitesExport = Value
            End Set
        End Property

        Public Property RequireKnownFacts() As Boolean Implements IParty.RequireKnownFacts
            Get
                Return mRequireKnownFacts
            End Get
            Set(ByVal Value As Boolean)
                mRequireKnownFacts = Value
            End Set
        End Property
        Private mRequireKnownFacts As Boolean

        Public Property AllowSemicompleteCitesArticle10() As Boolean Implements IParty.AllowSemicompleteCitesArticle10
            Get
                Return mAllowIncompleteCitesArticle10
            End Get
            Set(ByVal Value As Boolean)
                mAllowIncompleteCitesArticle10 = Value
            End Set
        End Property


        Public Property AllowIncompleteImportApplications() As Boolean Implements IParty.AllowIncompleteImportApplications
            Get
                Return mAllowIncompleteImportApplications
            End Get
            Set(ByVal Value As Boolean)
                mAllowIncompleteImportApplications = Value
            End Set
        End Property

        Public Property ExcludeFromMailingList() As Boolean Implements IParty.ExcludeFromMailingList
            Get
                Return mExcludeFromMailingList
            End Get
            Set(ByVal Value As Boolean)
                mExcludeFromMailingList = Value
            End Set
        End Property
        Private mExcludeFromMailingList As Boolean

        Public Overridable Property DisplayName() As String
            Get
                Return Nothing
            End Get
            Set(ByVal Value As String)

            End Set
        End Property

        Public Overridable Property PartyId() As Integer Implements IParty.PartyId
            Get
                Return mPartyId
            End Get
            Set(ByVal Value As Integer)
                mPartyId = Value
            End Set
        End Property
        Private mPartyId As Int32

        Public Property MailingAddressId() As Object Implements IParty.MailingAddressId
            Get
                Return mMailingAddressId
            End Get
            Set(ByVal Value As Object)
                mMailingAddressId = Value
            End Set
        End Property
        Private mMailingAddressId As Object

        Public Property PreviousName() As Object Implements IParty.PreviousName
            Get
                Return mPreviousName
            End Get
            Set(ByVal Value As Object)
                mPreviousName = Value
            End Set
        End Property
        Private mPreviousName As Object

        Public Property Validated() As Object Implements IParty.Validated
            Get
                Return mValidated
            End Get
            Set(ByVal Value As Object)
                mValidated = Value
            End Set
        End Property
        Private mValidated As Object


#End Region

        Protected Overridable Overloads Sub InitialiseParty(ByVal party As DataObjects.Entity.Party, ByVal tran As SqlClient.SqlTransaction)
            InitialiseParty(party, 0, tran)
        End Sub

        Protected Overridable Overloads Sub InitialiseParty(ByVal party As DataObjects.Entity.Party, ByVal userId As Int64, ByVal tran As SqlClient.SqlTransaction)
            With party
                Me.PartyId = .Id
                mRequireKnownFacts = .RequireKnownFacts
                mExcludeFromMailingList = .ExcludeFromMailingList
                If Not .IsValidatedNull Then Me.Validated = .Validated
                If Not .IsMailingAddressIdNull Then Me.MailingAddressId = .MailingAddressId
                IsBusiness = (.GetRelatedBusiness(tran).Count > 0)
                CheckSum = .CheckSum
                mPreviousName = .PreviousName
                AllowSemicompleteCitesArticle10 = .AllowSemicompleteCITESArticle10
                AllowSemicompleteCitesExport = .AllowSemicompleteCITESExport
                AllowSemicompleteCitesImport = .AllowSemicompleteCITESImport
                AllowIncompleteImportApplications = .AllowIncompleteImportApplications
                GatewayPreferredContactDetailId = .GateWayPreferredContactDetailId

                Dim AuthPartySet As [DO].DataObjects.EntitySet.AuthorisedPartySet = .GetRelatedAuthorisedParty(tran)
                If AuthPartySet.Entities.Count > 0 Then
                    AuthorisedPartyId = CType(AuthPartySet.GetEntity(0), DataObjects.Entity.AuthorisedParty).Id
                End If
                AuthPartySet = Nothing
                If Not Validated Is Nothing AndAlso Not CType(Validated, Boolean) Then Validated = False

                ' Now that Object has been retrieved, load the Known Facts into Object
                RetrieveKnownFacts(userId, tran)

            End With
        End Sub

        'Prisvate Sub SetupContacts(ByVal contactEntity As DataObjects.Entity.Contact, ByRef contact As IContact)
        '    If Not contactEntity Is Nothing Then
        '        With contactEntity
        '            contact.ContactId = .Id

        '            If Not .IsContactDetailNull Then contact.Detail = .ContactDetail
        '            contact.Active = .Active

        '            contact.ContactTypeId = .ContactTypeId
        '            Dim ContactData As DataObjects.Entity.ContactType = DataObjects.Entity.ContactType.GetById(contact.ContactTypeId)
        '            If Not ContactData Is Nothing Then contact.ContactType = ContactData.Description
        '        End With
        '    End If
        'End Sub

        Public Function GetNotes() As Party.Note()
            Return GetNotes(PartyId)
        End Function




        Public Function GetNotes(ByVal partyId As Int32) As Party.Note()
            Dim Party As New DataObjects.Entity.Party(partyId)
            Return GetNotes(Party)
        End Function

        Shared Function GetNotes(ByVal party As DataObjects.Entity.Party) As Party.Note()
            Dim Notes As DataObjects.EntitySet.NoteSet = party.GetRelatedNotes
            If Not Notes Is Nothing AndAlso _
                Notes.Count > 0 Then
                Dim NoteList(Notes.Count - 1) As party.Note
                Dim Index As Int32 = 0
                For Each note As DataObjects.Entity.Note In Notes
                    NoteList(Index) = New party.Note(note, party.Id)
                    Index += 1
                    'TODO: Sort Notes
                Next note
                Return NoteList
            Else
                Return Nothing
            End If

        End Function

        Private m_GatewayPreferreddetails As BOGatewayPreferredDetails
        Private m_gatewaypreferredContactDetailid As Object

        Public Property GatewayPreferredDetails() As BOGatewayPreferredDetails
            Get
                If m_GatewayPreferreddetails Is Nothing Then
                    GetGatewayPreferredDetails(Nothing)
                    Return m_GatewayPreferreddetails
                End If
            End Get
            Set(ByVal Value As BOGatewayPreferredDetails)
                m_GatewayPreferreddetails = Value
            End Set
        End Property

        Public Function GatewayDetailsList(ByVal tran As SqlClient.SqlTransaction) As BO.Party.BOGatewayPreferredDetails()
            Try

                Dim myD As BOGatewayPreferredDetails() = Party.BOGatewayPreferredDetails.Load(PartyId, False, tran)
                Return myD

            Catch ex As Exception
                Return Nothing
            End Try

        End Function

        Public Function GetGatewayPreferredDetails(ByVal tran As SqlClient.SqlTransaction) As BOGatewayPreferredDetails
            Try

                GatewayPreferredDetails = New BOGatewayPreferredDetails(PartyId, True, tran)

            Catch ex As Exception
                Return Nothing
            End Try

        End Function

        Public Function GetMailingAddress(ByVal tran As SqlClient.SqlTransaction) As BOAddress
            Dim Addresses As BOAddress() = GetAddresses(PartyId, tran)
            For Each Address As BOAddress In Addresses
                Address.PartyId = PartyId
                If Address.IsMailing Then Return Address
            Next Address
        End Function

        Public Function GetAddresses(ByVal tran As SqlClient.SqlTransaction) As BOAddress()
            Return GetAddresses(PartyId, tran)
        End Function

        Public Function GetAddresses(ByVal partyId As Int32, ByVal tran As SqlClient.SqlTransaction) As BOAddress()
            Try
                Dim Party As New DataObjects.Entity.Party(partyId, tran)
                Return GetAddresses(Party, tran)
            Catch ex As Exception
                ' Stop
            End Try
        End Function

        Shared Function GetAddresses(ByVal party As DataObjects.Entity.Party, ByVal tran As SqlClient.SqlTransaction) As BOAddress()
            Dim Addresses As DataObjects.EntitySet.AddressSet = party.GetRelatedAddresses(tran)

            If Not Addresses Is Nothing AndAlso _
               Addresses.Count > 0 Then
                Dim AddressesList(Addresses.Count - 1) As BOAddress
                Dim Index As Int32 = 0
                For Each address As DataObjects.Entity.Address In Addresses
                    Dim ConfirmPartyId As Int32
                    Dim MailingAddressId As Object
                    If Not party.IsMailingAddressIdNull AndAlso party.MailingAddressId = address.Id Then
                        'this address is the party address so don't give the id to the class...
                        'this prevents the address class from maintaining the link table
                        ConfirmPartyId = 0
                    Else
                        ConfirmPartyId = party.Id
                    End If
                    If party.IsMailingAddressIdNull Then
                        MailingAddressId = Nothing
                    Else
                        MailingAddressId = party.MailingAddressId
                    End If
                    AddressesList(Index) = New party.BOAddress(address, MailingAddressId, ConfirmPartyId)
                    Index += 1
                Next address
                Return AddressesList
            Else
                Return Nothing
            End If
        End Function

        Protected Overridable Function PreSave(ByVal tran As SqlClient.SqlTransaction) As Boolean
            Return True
        End Function

        Public Overrides Function Delete() As Object
            Dim NewParty As New DataObjects.Entity.Party
            Dim service As DataObjects.Service.PartyService = NewParty.ServiceObject

            Dim tran As SqlClient.SqlTransaction = service.BeginTransaction
            If Not service.DeleteById(PartyId, CheckSum, tran) Then
                If Not DataObjects.Sprocs.LastError Is Nothing Then
                    service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                Else
                    service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                End If
            Else
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Commit)
                'Dim ErrorObject As BOError
                'ErrorObject = New BOError(BOError.ErrorCodes.AddressCountMismatch)

                'Dim Manager As New ErrorManager(New BOError() {ErrorObject}, ErrorManager.Titles.test)
                'Stop
                Return Nothing
            End If

        End Function

        Public Overridable Shadows Function Save() As BOParty
            Return MyClass.Save(False)
        End Function

        Public Overridable Shadows Function Save(ByVal forceValidate As Boolean) As BOParty
            Return MyClass.Save(forceValidate, 0)
        End Function

        Public Overridable Shadows Function Save(ByVal forceValidate As Boolean, ByVal userId As Int64) As BOParty
            Try
                If Not RequireKnownFacts Then
                    GatewayPreferredContactDetailId = Nothing
                Else
                    If CType(GatewayPreferredContactDetailId, Int32) < 1 Then
                        GatewayPreferredContactDetailId = Nothing
                    End If
                End If

            Catch ex As Exception
                ' Will fail if it's nothing, but set it anyway.
                GatewayPreferredContactDetailId = Nothing
            End Try

            If forceValidate OrElse (Not Validated Is Nothing AndAlso Validated.GetType.Equals(GetType(Boolean)) AndAlso CType(Validated, Boolean)) Then
                'as this record has been considered valid - we must ensure this continues by prevalidating
                Dim ValidateResults As ValidationManager
                If forceValidate Then
                    ValidateResults = Validate(True, userId, (userId <> 0))
                Else
                    ValidateResults = Validate()
                End If
                If Not ValidateResults Is Nothing Then
                    'validation failed, so bail
                    ValidationErrors = ValidateResults
                    Me.Validated = False
                Else
                    ValidationErrors = Nothing
                    Me.Validated = True
                End If
            End If

            Created = (PartyId = 0)
            Dim NewParty As New DataObjects.Entity.Party
            Dim service As DataObjects.Service.PartyService = NewParty.ServiceObject

            Dim tran As SqlClient.SqlTransaction = service.BeginTransaction

            If mValidated Is Nothing Then mValidated = False




            If Created Then
                'If mMailingAddressId Is Nothing OrElse CType(mMailingAddressId, Int32) = 0 Then
                '    'must ensure that a mailing address exists
                'End If
                NewParty = service.Insert(mExcludeFromMailingList, _
                                           mMailingAddressId, _
                                          mValidated, _
                                           mPreviousName, _
                                           GatewayPreferredContactDetailId, _
                                           AllowSemicompleteCitesImport, _
                                           AllowSemicompleteCitesExport, _
                                           AllowSemicompleteCitesArticle10, _
                                           AllowIncompleteImportApplications, _
                                           RequireKnownFacts, _
                                           tran)
            Else
                NewParty = service.Update(mPartyId, _
                                          mExcludeFromMailingList, _
                                          mMailingAddressId, _
                                          mValidated, _
                                          mPreviousName, _
                                          GatewayPreferredContactDetailId, _
                                          AllowSemicompleteCitesImport, _
                                          AllowSemicompleteCitesExport, _
                                          AllowSemicompleteCitesArticle10, _
                                          AllowIncompleteImportApplications, _
                                          RequireKnownFacts, _
                                          CheckSum, _
                                          tran)
            End If

            ''if the KnownFacts exist in the object (and are not saved, 
            ''then save to the database, otherwise we ignore them

            If Not mKnownFacts Is Nothing Then
                Try
                    mKnownFacts.Save(tran)
                Catch ex As Exception

                End Try
            End If

            'check to see if any SQL errors have occured
            If NewParty Is Nothing AndAlso Not DataObjects.Sprocs.LastError Is Nothing Then
                'TODO: Use errors collection to check to see if the problem was concurrency
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveParty)
                Return Me
            ElseIf NewParty Is Nothing Then
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveParty)
                Return Me
            ElseIf Created And Not NewParty Is Nothing Then
                mPartyId = NewParty.Id
            End If
            If Not PreSave(tran) Then
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.PreSaveFailed)}, ValidationManager.ValidationTitles.CannotSaveParty)
                Return Me
            Else
                If NewParty.CheckSum <> CheckSum Then
                    'no point in initialising unless things have changed
                    InitialiseParty(NewParty, userId, tran)
                End If
                service.EndTransaction(tran)
                ' Not sure ... if Validation Errors are generated, they are ignored.
                'ValidationErrors = Nothing
                Return Me
            End If
        End Function

        Public Function GetPersons() As BOPerson()
            Return GetPersons(PartyId)
        End Function

        Shared Function GetPersons(ByVal partyId As Int32) As BOPerson()
            Return GetPersons(partyId, Nothing)
        End Function

        Shared Function GetPersons(ByVal partyId As Int32, ByVal tran As SqlClient.SqlTransaction) As BOPerson()
            Dim Persons As DataObjects.EntitySet.PersonSet = DataObjects.Entity.Person.GetForParty(partyId, tran)

            If Not Persons Is Nothing AndAlso _
               Persons.Count > 0 Then
                Dim PersonsList(Persons.Count - 1) As BOPerson
                Dim Index As Int32 = 0
                For Each person As DataObjects.Entity.Person In Persons
                    PersonsList(Index) = New BOPerson(person, partyId)
                    Index += 1
                Next person
                Return PersonsList
            Else
                Return Nothing
            End If
        End Function

        Shared Function GetPersons(ByVal party As DataObjects.Entity.Party) As BOPerson()
            Return GetPersons(party.Id)
        End Function

        Protected Overridable Overloads Function Validate() As ValidationManager Implements IValidate.Validate
            Return Validate(True, 0)
        End Function

        Protected Overloads Function Validate(ByVal writeFlag As Boolean) As ValidationManager Implements IParty.Validate
            Return Validate(writeFlag, 0)
        End Function

        Protected Overridable Overloads Function Validate(ByVal userid As Int64) As ValidationManager
            Return Validate(True, userid)
        End Function

        Protected Overloads Function Validate(ByVal writeFlag As Boolean, ByVal userid As Int64) As ValidationManager
            Return Validate(writeFlag, userid, False)
        End Function

        Protected Overridable Overloads Function Validate(ByVal writeFlag As Boolean, ByVal userid As Int64, ByVal secure As Boolean) As ValidationManager
            Dim ErrorList As New ArrayList

            'does the party have at least one address
            Dim PartyAddresses As BO.Party.BOAddress() = Me.GetAddresses(Nothing)
            If PartyAddresses Is Nothing OrElse PartyAddresses.Length = 0 Then
                ErrorList.Add(New ValidationError(ValidationError.ValidationCodes.AddressCountMismatch))
            End If

            'does the party have a mailing address
            If mMailingAddressId Is Nothing OrElse CType(mMailingAddressId, Int32) = 0 Then
                ErrorList.Add(New ValidationError(ValidationError.ValidationCodes.NoMailingAddress))
            Else
                'they have an address - verify that it exists :o)
                If New DataObjects.Entity.Address(CType(mMailingAddressId, Int32)) Is Nothing Then
                    ErrorList.Add(New ValidationError(ValidationError.ValidationCodes.MailingAddressNoLongerExists))
                End If
            End If

            'ensure that at least one of their addresses is active
            '...and...
            'ensure that all of the addresses are valid
            Dim FoundActive As Boolean = False
            Dim FoundInvalid As Boolean = False
            If Not PartyAddresses Is Nothing Then
                For Each address As BO.Party.BOAddress In PartyAddresses
                    If address.Active AndAlso Not FoundActive Then
                        'inactive address
                        FoundActive = True
                        'If FoundInvalid Then Exit For
                    End If
                    If Not address.Validate Is Nothing AndAlso Not FoundInvalid Then
                        'invalid address
                        FoundInvalid = True
                        ' If FoundInActive Then Exit For
                    End If
                Next address
            End If
            If Not FoundActive Then ErrorList.Add(New ValidationError(ValidationError.ValidationCodes.NeedOneActiveAddress))
            If FoundInvalid Then ErrorList.Add(New ValidationError(ValidationError.ValidationCodes.AddressValidationError))


            Dim PartyPersons As BO.Party.BOPerson() = Me.GetPersons
            'ensure that the persons are valid
            If Not PartyPersons Is Nothing AndAlso userid <> 0 Then
                For Each person As BO.Party.BOPerson In PartyPersons
                    Dim PersonValidationManager As ValidationManager
                    PersonValidationManager = person.Validate(person.PersonId, secure)
                    If Not PersonValidationManager Is Nothing Then
                        For Each ve As ValidationError In PersonValidationManager.Errors
                            ErrorList.Add(ve)
                        Next
                        ' ErrorList.Add(New ValidationError(ValidationError.ValidationCodes.PersonValidationError))
                        Exit For
                    End If
                Next person
            Else
                'ErrorList.Add(New ValidationError(ValidationError.ValidationCodes.PersonValidationError))
            End If

            'if the party is an individual, they must have a person record
            If Not Me.IsBusiness AndAlso PartyPersons.Length = 0 Then
                ErrorList.Add(New ValidationError(ValidationError.ValidationCodes.MissingPerson))
            End If

            If Me.RequireKnownFacts Then
                Dim myIntGWContact As Integer = 0
                Try
                    myIntGWContact = Integer.Parse(Me.GatewayPreferredContactDetailId.ToString)
                Catch ex As Exception
                    myIntGWContact = 0
                End Try

                If Not myIntGWContact > 0 Then
                    ErrorList.Add(New ValidationError(ValidationError.ValidationCodes.NeedOneEmailAddress))
                End If
            End If

            'if the party is a business, they must have a business record
            If Me.IsBusiness AndAlso DataObjects.Entity.Business.GetForParty(Me.PartyId) Is Nothing Then
                ErrorList.Add(New ValidationError(ValidationError.ValidationCodes.MissingBusiness))
            End If
            PartyAddresses = Nothing
            PartyPersons = Nothing

            If ErrorList.Count > 0 Then
                If writeFlag Then
                    mValidated = False
                End If
                Dim Validation As New ValidationManager(CType(ErrorList.ToArray(GetType(ValidationError)), ValidationError()), ValidationManager.ValidationTitles.CannotSaveParty)
                Return Validation
            Else
                If writeFlag Then
                    mValidated = True
                End If
                Return Nothing
            End If
        End Function

        Public Function IsAuthorised() As Boolean
            Dim AuthPartySet As DataObjects.EntitySet.AuthorisedPartySet = GetAuthorised()

            If AuthPartySet Is Nothing Then
                Return False
            Else
                Return (AuthPartySet.Count > 0)
            End If
        End Function

        Private Function GetAuthorised() As DataObjects.EntitySet.AuthorisedPartySet
            Dim AuthService As New DataObjects.Service.AuthorisedPartyService
            Dim AuthPartySet As DataObjects.EntitySet.AuthorisedPartySet = AuthService.GetByIndex_PartyId(Me.PartyId)
            AuthService = Nothing

            Return AuthPartySet
        End Function


        Public Function Authorise(ByVal authorisedBy As Int64) As Int32
            If IsAuthorised() Then
                'no point in adding it again :o)
                Return GetAuthorised.Entities(0).Id
            Else
                Dim AuthService As New DataObjects.Service.AuthorisedPartyService
                Dim AuthParty As DataObjects.Entity.AuthorisedParty = AuthService.Insert(Date.Now, authorisedBy, PartyId)

                'did it insert ok?
                Dim authpartyid As Int32
                If AuthParty Is Nothing Then
                    authpartyid = 0
                Else
                    authpartyid = AuthParty.Id
                    ' Get the Known Facts ... if we have all of the known facts (which we should have) for the party, 
                    ' then save the known facts to the database.
                    RetrieveKnownFacts(0, Nothing)
                    If Not mKnownFacts Is Nothing Then
                        Try
                            mKnownFacts.Save(Nothing)
                        Catch ex As Exception
                            ' we want to be able to re-authorise the Party, to kick the process
                            ' off again, so delete the Authorisation.
                            AuthService.DeleteById(AuthParty.Id, AuthParty.CheckSum, Nothing)
                            authpartyid = 0
                        End Try
                        ' Not sure if we should have this, so commented out at present
                        '' ==========================================================
                        '' If we do not have Known Facts, then we should not be able to authorise.
                        'Else '... 
                        '' we want to be able to re-authorise the Party, to kick the process
                        '' off again, so delete the Authorisation.
                        '    AuthService.DeleteById(AuthParty.Id, AuthParty.CheckSum, Nothing)
                        '    authpartyid = 0
                        '' ==========================================================
                    End If

                End If
                Return authpartyid
            End If
        End Function

        Public Property AuthorisedPartyId() As Int32 Implements IParty.AuthorisedPartyId
            Get
                If mAuthorisedPartyId > 0 Then
                    Return mAuthorisedPartyId
                Else
                    Dim AuthPartySet As DataObjects.EntitySet.AuthorisedPartySet = GetAuthorised()
                    If Not AuthPartySet Is Nothing AndAlso _
                       AuthPartySet.Count = 1 Then
                        mAuthorisedPartyId = AuthPartySet.Entities(0).Id
                    Else
                        mAuthorisedPartyId = 0
                    End If
                    Return mAuthorisedPartyId
                End If
            End Get
            Set(ByVal Value As Int32)
                mAuthorisedPartyId = Value
            End Set
        End Property
        Private mAuthorisedPartyId As Int32

        Public Shared Function CreateByAuthorisedPartyId(ByVal authorisedPartyId As Int32) As BOParty
            Dim AuthService As [DO].DataObjects.Service.AuthorisedPartyService = [DO].DataObjects.Entity.AuthorisedParty.ServiceObject
            Dim AuthParty As [DO].DataObjects.Entity.AuthorisedParty = AuthService.GetById(authorisedPartyId)
            If Not AuthParty Is Nothing Then
                Return New BOParty(AuthParty.PartyId)
            Else
                Return Nothing
            End If
        End Function

        Public Property GatewayPreferredContactDetailId() As Object Implements IParty.GatewayPreferredContactDetailId
            Get
                Return m_gatewaypreferredContactDetailid
            End Get
            Set(ByVal Value As Object)
                m_gatewaypreferredContactDetailid = Value
            End Set
        End Property

        Public Shared Function PolymorphicCreate(ByVal partyId As Int32) As BOParty 'MLD 2/12/4
            Return PolymorphicCreate(partyId, Nothing)
        End Function

        Public Shared Function PolymorphicCreate(ByVal partyId As Int32, ByVal tran As SqlClient.SqlTransaction) As BOParty    'MLD 24/11/4 & 2/12/4 (transaction added)
            Dim result As New BOParty(partyId, tran)
            If Not result Is Nothing Then
                If result.IsBusiness Then
                    result = New BOPartyBusiness(partyId, tran)
                Else
                    result = New BOPartyIndividual(partyId, tran)
                End If
            End If
            Return result
        End Function


    End Class
End Namespace

Namespace Party
    Public Class BOContact
        Inherits BaseBO
        Implements IContact



        Private mpreferredContactId As Object
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal pContactID As Object)
            MyClass.New()
            Dim myD As DataObjects.EntitySet.ContactSet = DataObjects.Sprocs.eosp_SelectContact(pContactID, Nothing)
            mpreferredContactId = pContactID
            InitialiseContact(CType(myD.GetEntity(0), DataObjects.Entity.Contact))

        End Sub
        Public Sub New(ByVal data As DataObjects.Entity.Contact, ByVal preferredContactId As Object, ByVal personId As Int32)
            MyClass.New()
            mpreferredContactId = preferredContactId
            mPersonId = personId
            InitialiseContact(data)
        End Sub

        Public Property ActiveString() As String Implements IContact.ActiveString
            Get
                Return Application.Search.ApplicationSearch.ConvertToEnglishBoolean(Active)
            End Get
            Set(ByVal Value As String)

            End Set
        End Property

        Public Property Active() As Boolean Implements IContact.Active
            Get
                Return mActive
            End Get
            Set(ByVal Value As Boolean)
                mActive = Value
            End Set
        End Property
        Private mActive As Boolean

        Public Property Location() As String Implements IContact.location
            Get
                Return mlocation
            End Get
            Set(ByVal Value As String)
                mlocation = Value
            End Set
        End Property
        Private mlocation As String

        Public Property ContactId() As Integer Implements IContact.ContactId
            Get
                Return mContactId
            End Get
            Set(ByVal Value As Integer)
                mContactId = Value
            End Set
        End Property
        Private mContactId As Int32

        Public Property ContactType() As String Implements IContact.ContactType
            Get
                If mContactType Is Nothing Then
                    Dim ContactTypeData As DataObjects.Entity.ContactType = DataObjects.Entity.ContactType.GetById(mContactTypeId)
                    If Not ContactTypeData Is Nothing Then
                        mContactType = ContactTypeData.Description
                        ContactTypeData = Nothing
                    End If
                End If
                Return mContactType
            End Get
            Set(ByVal Value As String)
                mContactType = Value
            End Set
        End Property
        Private mContactType As String

        Public Property ContactTypeId() As Integer Implements IContact.ContactTypeId
            Get
                Return mContactTypeId
            End Get
            Set(ByVal Value As Integer)
                mContactTypeId = Value
            End Set
        End Property
        Private mContactTypeId As Int32

        Public Property Detail() As String Implements IContact.Detail
            Get
                Return mDetail
            End Get
            Set(ByVal Value As String)
                mDetail = Value
            End Set
        End Property
        Private mDetail As String

        Public Property IsPreferredString() As String Implements IContact.IsPreferredString
            Get
                Return Application.Search.ApplicationSearch.ConvertToEnglishBoolean(Ispreferred)
            End Get
            Set(ByVal Value As String)

            End Set
        End Property

        Public Property Ispreferred() As Boolean Implements IContact.Ispreferred
            Get
                Return mIspreferred
            End Get
            Set(ByVal Value As Boolean)
                mIspreferred = Value
            End Set
        End Property
        Private mIspreferred As Boolean

        Public Property PersonId() As Int32 Implements IContact.PersonId
            Get
                Return mPersonId
            End Get
            Set(ByVal Value As Int32)
                mPersonId = Value
            End Set
        End Property
        Private mPersonId As Int32

        Private Sub InitialiseContact(ByVal contact As DataObjects.Entity.Contact)
            With contact
                mContactId = .Id
                If Not .IsContactDetailNull Then mDetail = .ContactDetail
                mContactTypeId = .ContactTypeId
                mContactType = Nothing
                mActive = .Active
                If Not mpreferredContactId Is Nothing AndAlso CType(mpreferredContactId, Int32) > 0 Then
                    mIspreferred = (CType(mpreferredContactId, Int32) = mContactId)
                Else
                    mIspreferred = False
                End If
                CheckSum = .CheckSum
                Location = .Location
            End With
        End Sub

        Public Shadows Function Delete() As Boolean
            Dim NewContact As New DataObjects.Entity.Contact
            Dim service As DataObjects.Service.ContactService = NewContact.ServiceObject
            Dim Deleted As Boolean
            Deleted = service.DeleteById(mContactId, CheckSum)
            Return Deleted
        End Function

        Public Shadows Function Save() As BOContact
            Created = (ContactId = 0)
            Dim NewContact As New DataObjects.Entity.Contact
            Dim service As DataObjects.Service.ContactService = NewContact.ServiceObject

            If Created Then
                NewContact = service.Insert(mDetail, _
                                            mActive, _
                                            mContactTypeId, _
                                            mlocation)
            Else
                NewContact = service.Update(mContactId, _
                                            mDetail, _
                                            mActive, _
                                            mContactTypeId, _
                                            mlocation, _
                                            CheckSum)
            End If
            If NewContact Is Nothing Then
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveContact)
                Return Me
            Else
                InitialiseContact(NewContact)
                If Created AndAlso mPersonId > 0 Then
                    Dim ContactJoin As New DataObjects.Entity.PersonContacts
                    Dim JoinService As DataObjects.Service.PersonContactsService = ContactJoin.ServiceObject
                    JoinService.Insert(mContactId, mPersonId)
                End If
                Return Me
            End If
        End Function

        Public Function Validate() As ValidationManager Implements IValidate.Validate
            Return Nothing
        End Function



    End Class
End Namespace
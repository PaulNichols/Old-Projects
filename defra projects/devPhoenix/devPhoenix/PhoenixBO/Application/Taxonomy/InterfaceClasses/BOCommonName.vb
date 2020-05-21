Namespace Taxonomy

    <Serializable()> _
    Public Class BOCommonName
        Inherits Taxonomy.TaxonomyBaseBO
        Implements ICommonName

#Region " Prelim code "
        Public Sub New()
            'This constructor is intentionally empty.
        End Sub

        Public Sub New(ByVal Source As Int32, ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction)
            LoadTaxon(Source, id, tran)
        End Sub

        Public Sub New(ByVal Source As Int32, ByVal id As Int32)
            LoadTaxon(Source, id, Nothing)
        End Sub

        Private Overloads Function LoadTaxon(ByVal Source As Int32, ByVal id As Int32) As DataObjects.Entity.TaxonomyCommonName
            Return Me.LoadTaxon(Source, id, Nothing)
        End Function

        Private Overloads Function LoadTaxon(ByVal Source As Int32, ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.TaxonomyCommonName
            Dim NewCommonName As DataObjects.Entity.TaxonomyCommonName = DataObjects.Entity.TaxonomyCommonName.GetById(Source, id, tran)
            If NewCommonName Is Nothing Then
                Throw New RecordDoesNotExist("Common Name", id)
            Else
                InitialiseMe(NewCommonName, tran)
                Return NewCommonName
            End If
        End Function

        Protected Overridable Sub InitialiseMe(ByVal CommonName As DataObjects.Entity.TaxonomyCommonName, ByVal tran As SqlClient.SqlTransaction)
            With CommonName
                Me.Source = .SourceTable
                Me.Id = .CommonNameID
                Me.KingdomID = .KingdomID
                Me.TaxonId = .TaxonId
                Me.TaxonTypeID = .TaxonTypeID
                Me.CheckSum = .CheckSum
                Me.Name = .Name
                'Me.AreaOfUse = .areaofuse
                Me.IsProductIndicator = .ProductIndicator
                If Not Validated Is Nothing AndAlso Not CType(Validated, Boolean) Then Validated = False
            End With
        End Sub

#End Region

#Region " Properties "

        Public Property Validated() As Object Implements ICommonName.Validated
            Get
                Return mValidated
            End Get
            Set(ByVal Value As Object)
                mValidated = Value
            End Set
        End Property
        Private mValidated As Object

        Public Property Taxon() As BOTaxon Implements ICommonName.Taxon
            Get
                Return mTaxon
            End Get
            Set(ByVal Value As BOTaxon)
                mTaxon = Value
            End Set
        End Property
        Private mTaxon As BOTaxon

        Public Overridable Property KingdomID() As Int32 Implements ICommonName.KingdomID
            Get
                Return mKingdomID
            End Get
            Set(ByVal Value As Int32)
                mKingdomID = Value
            End Set
        End Property
        Private mKingdomID As Int32

        Public Overridable Property Source() As Int32 Implements ICommonName.Source
            Get
                Return mSource
            End Get
            Set(ByVal Value As Integer)
                mSource = Value
            End Set
        End Property
        Private mSource As Int32

        Public Overridable Property Id() As Int32 Implements ICommonName.ID
            Get
                Return mId
            End Get
            Set(ByVal Value As Integer)
                mId = Value
            End Set
        End Property
        Private mId As Int32

        Public Overridable Property TaxonTypeID() As Integer Implements ICommonName.TaxonTypeID
            Get
                Return mTaxonTypeID
            End Get
            Set(ByVal Value As Integer)
                mTaxonTypeID = Value
            End Set
        End Property
        Private mTaxonTypeID As Int32

        Public Overridable Property TaxonId() As Integer Implements ICommonName.TaxonId
            Get
                Return mTaxonId
            End Get
            Set(ByVal Value As Integer)
                mTaxonId = Value
            End Set
        End Property
        Private mTaxonId As Int32

        Public Property Name() As String Implements ICommonName.Name
            Get
                Return (mName)
            End Get
            Set(ByVal Value As String)
                mName = Value
            End Set
        End Property
        Private mName As String

        Public Property AreaOfUse() As String Implements ICommonName.AreaOfUse
            Get
                Return mAreaOfUse
            End Get
            Set(ByVal Value As String)
                mAreaOfUse = Value
            End Set
        End Property
        Private mAreaOfUse As String

        Public Property IsProductIndicator() As Boolean Implements ICommonName.IsProductIndicator
            Get
                Return mIsProductIndicator
            End Get
            Set(ByVal Value As Boolean)
                mIsProductIndicator = Value
            End Set
        End Property
        Private mIsProductIndicator As Boolean

#End Region

#Region " Validate "
        Protected Overridable Overloads Function Validate() As ValidationManager Implements IValidate.Validate
            Return Validate(True, 0)
        End Function

        Protected Overloads Function Validate(ByVal writeFlag As Boolean) As ValidationManager Implements ICommonName.Validate
            Return Validate(writeFlag, 0)
        End Function

        Protected Overridable Overloads Function Validate(ByVal userid As Int32) As ValidationManager
            Return Validate(True, userid)
        End Function

        Protected Overloads Function Validate(ByVal writeFlag As Boolean, ByVal userid As Int32) As ValidationManager
            Return Validate(writeFlag, userid, False)
        End Function

        Protected Overridable Overloads Function Validate(ByVal writeFlag As Boolean, ByVal userid As Int32, ByVal secure As Boolean) As ValidationManager
            Dim ErrorList As New ArrayList

            'TODO: Nick - delete this validation code if it is not needed.
            ''does the party have at least one address
            'Dim PartyAddresses As BO.Party.BOAddress() = Me.GetAddresses(Nothing)
            'If PartyAddresses Is Nothing OrElse PartyAddresses.Length = 0 Then
            '    ErrorList.Add(New ValidationError(ValidationError.ValidationCodes.AddressCountMismatch))
            'End If

            ''does the party have a mailing address
            'If mMailingAddressId Is Nothing OrElse CType(mMailingAddressId, Int32) = 0 Then
            '    ErrorList.Add(New ValidationError(ValidationError.ValidationCodes.NoMailingAddress))
            'Else
            '    'they have an address - verify that it exists :o)
            '    If New DataObjects.Entity.Address(CType(mMailingAddressId, Int32)) Is Nothing Then
            '        ErrorList.Add(New ValidationError(ValidationError.ValidationCodes.MailingAddressNoLongerExists))
            '    End If
            'End If

            ''ensure that at least one of their addresses is active
            ''...and...
            ''ensure that all of the addresses are valid
            'Dim FoundActive As Boolean = False
            'Dim FoundInvalid As Boolean = False
            'If Not PartyAddresses Is Nothing Then
            '    For Each address As BO.Party.BOAddress In PartyAddresses
            '        If address.Active AndAlso Not FoundActive Then
            '            'inactive address
            '            FoundActive = True
            '            'If FoundInvalid Then Exit For
            '        End If
            '        If Not address.Validate Is Nothing AndAlso Not FoundInvalid Then
            '            'invalid address
            '            FoundInvalid = True
            '            ' If FoundInActive Then Exit For
            '        End If
            '    Next address
            'End If
            'If Not FoundActive Then ErrorList.Add(New ValidationError(ValidationError.ValidationCodes.NeedOneActiveAddress))
            'If FoundInvalid Then ErrorList.Add(New ValidationError(ValidationError.ValidationCodes.AddressValidationError))


            'Dim PartyPersons As BO.Party.BOPerson() = Me.GetPersons
            ''ensure that the persons are valid
            'If Not PartyPersons Is Nothing AndAlso userid <> 0 Then
            '    For Each person As BO.Party.BOPerson In PartyPersons
            '        Dim PersonValidationManager As ValidationManager
            '        PersonValidationManager = person.Validate(person.PersonId, secure)
            '        If Not PersonValidationManager Is Nothing Then
            '            For Each ve As ValidationError In PersonValidationManager.Errors
            '                ErrorList.Add(ve)
            '            Next
            '            ' ErrorList.Add(New ValidationError(ValidationError.ValidationCodes.PersonValidationError))
            '            Exit For
            '        End If
            '    Next person
            'Else
            '    'ErrorList.Add(New ValidationError(ValidationError.ValidationCodes.PersonValidationError))
            'End If

            ''if the party is an individual, they must have a person record
            'If Not Me.IsBusiness AndAlso PartyPersons.Length = 0 Then
            'ErrorList.Add(New ValidationError(ValidationError.ValidationCodes.MissingPerson))
            'End If

            ''if the party is a business, they must have a business record
            'If Me.IsBusiness AndAlso DataObjects.Entity.Business.GetForParty(Me.PartyId) Is Nothing Then
            '    ErrorList.Add(New ValidationError(ValidationError.ValidationCodes.MissingBusiness))
            'End If
            'PartyAddresses = Nothing
            'PartyPersons = Nothing

            Dim Validation As ValidationManager = Nothing
            If ErrorList.Count > 0 Then
                If writeFlag Then
                    mValidated = False
                End If
                Validation = New ValidationManager(CType(ErrorList.ToArray, ValidationError()), ValidationManager.ValidationTitles.CannotSaveParty)
            Else
                If writeFlag Then
                    mValidated = True
                End If
            End If
            Return Validation
        End Function
#End Region

    End Class
End Namespace


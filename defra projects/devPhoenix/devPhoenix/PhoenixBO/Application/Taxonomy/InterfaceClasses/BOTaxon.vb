Namespace Taxonomy
    <Serializable()> Public Class BOTaxon
        Inherits TaxonomyBaseBO
        Implements ITaxon


#Region " Prelim code "
        Public Sub New()
            'This constructor is intentionally empty.
        End Sub

        Public Sub New(ByVal KingdomID As Int32, ByVal TaxonID As Int32, ByVal TaxonTypeID As Int32, ByVal tran As SqlClient.SqlTransaction)
            LoadTaxon(KingdomID, TaxonID, TaxonTypeID, tran)
        End Sub

        Public Sub New(ByVal KingdomID As Int32, ByVal TaxonID As Int32, ByVal TaxonTypeID As Int32)
            LoadTaxon(KingdomID, TaxonID, TaxonTypeID, Nothing)
        End Sub

        Public Sub New(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction)
            LoadTaxon(id, tran)
        End Sub

        Public Sub New(ByVal id As Int32)
            LoadTaxon(id, Nothing)
        End Sub

        Private Overloads Function LoadTaxon(ByVal id As Int32) As DataObjects.Entity.TaxonomyTaxon
            Return Me.LoadTaxon(id, Nothing)
        End Function

        Private Overloads Function LoadTaxon(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.TaxonomyTaxon
            Dim NewTaxon As DataObjects.Entity.TaxonomyTaxon = DataObjects.Entity.TaxonomyTaxon.GetById(id, tran)
            If NewTaxon Is Nothing Then
                Throw New RecordDoesNotExist("Taxon", id)
            Else
                InitialiseTaxonomyTaxon(NewTaxon, tran)
                Return NewTaxon
            End If
        End Function

        Private Overloads Function LoadTaxon(ByVal KingdomID As Int32, ByVal TaxonID As Int32, ByVal TaxonTypeID As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.TaxonomyTaxon

            Dim TaxonomyService As New DataObjects.Service.TaxonomyTaxonService
            Dim TaxonomySet As DataObjects.EntitySet.TaxonomyTaxonSet = _
                TaxonomyService.GetByIndex_IX_TaxonomyTaxon(KingdomID:=KingdomID, TaxonID:=TaxonID, TaxonTypeID:=TaxonTypeID)
            Dim NewTaxon As DataObjects.Entity.TaxonomyTaxon = TaxonomySet.Entities(0)

            If NewTaxon Is Nothing Then
                Throw New RecordDoesNotExist("Taxon", Id)
            Else
                InitialiseTaxonomyTaxon(NewTaxon, tran)
                Return NewTaxon
            End If
        End Function


        Protected Overridable Sub InitialiseTaxonomyTaxon(ByVal Taxon As DataObjects.Entity.TaxonomyTaxon, ByVal tran As SqlClient.SqlTransaction)
            With Taxon
                Me.Id = .Id
                Me.KingdomID = .KingdomID
                Me.TaxonId = .TaxonID
                Me.TaxonStatusID = .TaxonStatusID
                Me.CheckSum = .CheckSum
                Me.TaxonNameUnformatted = .TaxonName
                Me.TaxonEpithetUnformatted = .EpithetType
                Me.TaxonAuthorUnformatted = .TaxonAuthor
                Me.TaxonTypeID = .TaxonTypeID
                Me.CITESReference = .CITESReference
                Me.DistributionComplete = .DistributionComplete
                Me.ParentKingdomID = .ParentKingdomID
                Me.ParentTaxonID = .ParentTaxonID
                Me.ParentTaxonTypeID = .ParentTaxonTypeID

                Dim DOType As DataObjects.Entity.TaxonomyTaxonType
                DOType = DataObjects.Entity.TaxonomyTaxonType.GetById(.TaxonTypeID)
                'NT 10/05/05'Me.TaxonType = CType(System.Enum.Parse(GetType(Taxonomy.TaxonTypeEnum), DOType.Description), Taxonomy.TaxonTypeEnum)
                Me.TaxonType = CType(Taxon.TaxonTypeID, Taxonomy.TaxonTypeEnum)
                'Get the display Taxontype.
                'NT 10/05/05'DOType = DataObjects.Entity.TaxonomyTaxonType.GetById(DOType.DisplayID)
                'NT 10/05/05'Me.DisplayTaxonType = CType(System.Enum.Parse(GetType(Taxonomy.TaxonTypeEnum), DOType.Description), Taxonomy.TaxonTypeEnum)
                Me.DisplayTaxonType = CType(DOType.DisplayID, Taxonomy.TaxonTypeEnum)

                Me.TaxonStatusDescription = _
                     DataObjects.Entity.TaxonomyTaxonStatus.GetById(Me.TaxonStatusID).Description
                Me.TaxonStatus = CType(Me.TaxonStatusID, Taxonomy.TaxonStatusEnum)
                'NT 10/05/05'Me.TaxonStatus = _
                '    CType(System.Enum.Parse(GetType(Taxonomy.TaxonStatusEnum), Me.TaxonStatusDescription), Taxonomy.TaxonStatusEnum)
                Me.CommonNames = GetCommonNamesString()

                Dim TaxonomyService As New DataObjects.Views.Service.TaxonomyTaxonAllService
                Dim TaxonomySet As DataObjects.Views.Collection.TaxonomyTaxonAllBoundCollection = _
                    TaxonomyService.GetTaxonByID(Me.Id)
                Me.IsCoral = TaxonomySet(0).IsCoral

                'Load and set the scientific names.
                SetScientificNames(TaxonType, .ParentKingdomID, .ParentTaxonID, .ParentTaxonTypeID, tran)

                If Not Validated Is Nothing AndAlso Not CType(Validated, Boolean) Then Validated = False
            End With
        End Sub

#End Region

#Region " Properties "

        Public Overridable Property ParentKingdomID() As Int32 Implements ITaxon.ParentKingdomID
            Get
                Return mParentKingdomID
            End Get
            Set(ByVal Value As Int32)
                mParentKingdomID = Value
            End Set
        End Property
        Private mParentKingdomID As Int32

        Public Overridable Property ParentTaxonID() As Int32 Implements ITaxon.ParentTaxonID
            Get
                Return mParentTaxonID
            End Get
            Set(ByVal Value As Int32)
                mParentTaxonID = Value
            End Set
        End Property
        Private mParentTaxonID As Int32

        Public Overridable Property ParentTaxonTypeID() As Int32 Implements ITaxon.ParentTaxonTypeID
            Get
                Return mParentTaxonTypeID
            End Get
            Set(ByVal Value As Int32)
                mParentTaxonTypeID = Value
            End Set
        End Property
        Private mParentTaxonTypeID As Int32

        Public Overridable Property ShortScientificNameHTMLFormatted() As String Implements ITaxon.ShortScientificNameHTMLFormatted
            Get
                Return mShortScientificNameHTMLFormatted
            End Get
            Set(ByVal Value As String)
                If Value Is Nothing Then
                    mShortScientificNameHTMLFormatted = String.Empty
                Else
                    mShortScientificNameHTMLFormatted = Value
                End If
            End Set
        End Property
        Private mShortScientificNameHTMLFormatted As String

        Public Overridable Property ShortScientificNameUnformatted() As String Implements ITaxon.ShortScientificNameUnformatted
            Get
                Return mShortScientificNameUnformatted
            End Get
            Set(ByVal Value As String)
                If Value Is Nothing Then
                    mShortScientificNameUnformatted = String.Empty
                Else
                    mShortScientificNameUnformatted = Value
                End If
            End Set
        End Property
        Private mShortScientificNameUnformatted As String

        Public Overridable Property LongScientificNameHTMLFormatted() As String Implements ITaxon.LongScientificNameHTMLFormatted
            Get
                Return mLongScientificNameHTMLFormatted
            End Get
            Set(ByVal Value As String)
                If Value Is Nothing Then
                    mLongScientificNameHTMLFormatted = String.Empty
                Else
                    mLongScientificNameHTMLFormatted = Value
                End If
            End Set
        End Property
        Private mLongScientificNameHTMLFormatted As String

        Public Overridable Property LongScientificNameUnformatted() As String Implements ITaxon.LongScientificNameUnformatted
            Get
                Return mLongScientificNameUnformatted
            End Get
            Set(ByVal Value As String)
                If Value Is Nothing Then
                    mLongScientificNameUnformatted = String.Empty
                Else
                    mLongScientificNameUnformatted = Value
                End If
            End Set
        End Property
        Private mLongScientificNameUnformatted As String

        Public Overridable Property TaxonNameHTMLFormatted() As String Implements ITaxon.TaxonNameHTMLFormatted
            Get
                Select Case Me.TaxonType
                    Case TaxonTypeEnum.Epithet, _
                    TaxonTypeEnum.Genus, _
                    TaxonTypeEnum.Species, _
                    TaxonTypeEnum.Stock
                        Return "<I>" & TaxonNameUnformatted & "</I>"
                    Case Else
                        Return TaxonNameUnformatted
                End Select

            End Get
            Set(ByVal Value As String)
                Value = Value 'Throw New ApplicationException("This property is readonly")
            End Set
        End Property

        Public Overridable Property TaxonEpithetHTMLFormatted() As String Implements ITaxon.TaxonEpithetHTMLFormatted
            Get
                Return TaxonEpithetUnformatted
            End Get
            Set(ByVal Value As String)
                Value = Value 'Throw New ApplicationException("This property is readonly")
            End Set
        End Property
        Private mTaxonEpithetHTMLFormatted As String

        Public Overridable Property TaxonAuthorHTMLFormatted() As String Implements ITaxon.TaxonAuthorHTMLFormatted
            Get
                Return TaxonAuthorUnformatted
            End Get
            Set(ByVal Value As String)
                Value = Value 'Throw New ApplicationException("This property is readonly")
            End Set
        End Property
        Private mTaxonAuthorHTMLFormatted As String

        Public Overridable Property TaxonNameUnformatted() As String Implements ITaxon.TaxonNameUnformatted
            Get
                Return mTaxonNameUnformatted
            End Get
            Set(ByVal Value As String)
                If Value Is Nothing Then
                    mTaxonNameUnformatted = String.Empty
                Else
                    mTaxonNameUnformatted = Value
                End If
            End Set
        End Property
        Private mTaxonNameUnformatted As String

        Public Overridable Property TaxonEpithetUnformatted() As String Implements ITaxon.TaxonEpithetUnformatted
            Get
                Return mTaxonEpithetUnformatted
            End Get
            Set(ByVal Value As String)
                If Value Is Nothing Then
                    mTaxonEpithetUnformatted = String.Empty
                Else
                    mTaxonEpithetUnformatted = Value
                End If
            End Set
        End Property
        Private mTaxonEpithetUnformatted As String

        Public Overridable Property TaxonAuthorUnformatted() As String Implements ITaxon.TaxonAuthorUnformatted
            Get
                Return mTaxonAuthorUnformatted
            End Get
            Set(ByVal Value As String)
                If Value Is Nothing Then
                    mTaxonAuthorUnformatted = String.Empty
                Else
                    mTaxonAuthorUnformatted = Value
                End If
            End Set
        End Property
        Private mTaxonAuthorUnformatted As String

        Public Overridable Property CanHaveAquaticDistribution() As Boolean Implements ITaxon.CanHaveAquaticDistribution
            Get
                Return Not TaxonomySearch.GetAnimalKingdom().KingdomID = Me.KingdomID
            End Get
            Set(ByVal Value As Boolean)
                Value = Value 'Throw New ApplicationException("This property is readonly")
            End Set
        End Property
        Public Overridable Property CanHaveAcceptedNames() As Boolean Implements ITaxon.CanHaveAcceptedNames
            Get
                If Me.TaxonStatus = TaxonStatusEnum.Accepted Then
                    Return False
                Else
                    Return True
                End If
            End Get
            Set(ByVal Value As Boolean)
                Value = Value 'Throw New ApplicationException("This property is readonly")
            End Set
        End Property

        Public Overridable Property CanHaveDecisions() As Boolean Implements ITaxon.CanHaveDecisions
            Get
                Select Case Me.TaxonType
                    Case TaxonTypeEnum.Epithet, _
                    TaxonTypeEnum.Stock, _
                    TaxonTypeEnum.Species, _
                    TaxonTypeEnum.Genus, _
                    TaxonTypeEnum.Family
                        Return True
                    Case Else
                        Return False
                End Select
            End Get
            Set(ByVal Value As Boolean)
                Value = Value 'Throw New ApplicationException("This property is readonly")
            End Set
        End Property

        Public Overridable Property CanHaveQuotas() As Boolean Implements ITaxon.CanHaveQuotas
            Get
                Select Case Me.TaxonType
                    Case TaxonTypeEnum.Epithet, _
                    TaxonTypeEnum.Stock, _
                    TaxonTypeEnum.Species, _
                    TaxonTypeEnum.Genus, _
                    TaxonTypeEnum.Family
                        Return True
                    Case Else
                        Return False
                End Select
            End Get
            Set(ByVal Value As Boolean)
                Value = Value 'Throw New ApplicationException("This property is readonly")
            End Set
        End Property
        Public Overridable Property CanHaveSynonyms() As Boolean Implements ITaxon.CanHaveSynonyms
            Get
                If Me.TaxonType = TaxonTypeEnum.Epithet _
                Or Me.TaxonType = TaxonTypeEnum.Species _
                Or Me.TaxonType = TaxonTypeEnum.Genus _
                Or Me.TaxonType = TaxonTypeEnum.Family Then
                    If Me.TaxonStatus = TaxonStatusEnum.Accepted Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            End Get
            Set(ByVal Value As Boolean)
                Value = Value 'Throw New ApplicationException("This property is readonly")
            End Set
        End Property

        Public Overridable Property CanHaveStockNames() As Boolean Implements ITaxon.CanHaveStockNames
            Get
                If Me.TaxonType = TaxonTypeEnum.Species _
                Or Me.TaxonType = TaxonTypeEnum.Epithet Then
                    If Me.TaxonStatus = TaxonStatusEnum.Accepted Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            End Get
            Set(ByVal Value As Boolean)
                Value = Value 'Throw New ApplicationException("This property is readonly")
            End Set
        End Property

        Public Overridable Property TaxonStatus() As Taxonomy.TaxonStatusEnum Implements ITaxon.TaxonStatus
            Get
                Return mTaxonStatus
            End Get
            Set(ByVal Value As Taxonomy.TaxonStatusEnum)
                mTaxonStatus = Value
            End Set
        End Property
        Private mTaxonStatus As Taxonomy.TaxonStatusEnum

        Public Overridable Property TaxonStatusID() As Int32 Implements ITaxon.TaxonStatusID
            Get
                Return mTaxonStatusID
            End Get
            Set(ByVal Value As Int32)
                mTaxonStatusID = Value
            End Set
        End Property
        Private mTaxonStatusID As Int32

        Public Overridable Property TaxonStatusDescription() As String Implements ITaxon.TaxonStatusDescription
            Get
                Return mTaxonStatusDescription
            End Get
            Set(ByVal Value As String)
                mTaxonStatusDescription = Value
            End Set
        End Property
        Private mTaxonStatusDescription As String

        Public Overridable Property KingdomID() As Int32 Implements ITaxon.KingdomID
            Get
                Return mKingdomID
            End Get
            Set(ByVal Value As Int32)
                mKingdomID = Value
            End Set
        End Property
        Private mKingdomID As Int32

        Public Overridable Property TaxonTypeID() As Int32 Implements ITaxon.TaxonTypeID
            Get
                Return mTaxonTypeID
            End Get
            Set(ByVal Value As Int32)
                mTaxonTypeID = Value
            End Set
        End Property
        Private mTaxonTypeID As Int32

        Public Overridable Property Id() As Int32 Implements ITaxon.ID
            Get
                Return mId
            End Get
            Set(ByVal Value As Integer)
                mId = Value
            End Set
        End Property
        Private mId As Int32

        Public Overridable Property TaxonId() As Integer Implements ITaxon.TaxonId
            Get
                Return mTaxonId
            End Get
            Set(ByVal Value As Integer)
                mTaxonId = Value
            End Set
        End Property
        Private mTaxonId As Int32

        Public Overridable Property DisplayTaxonType() As Taxonomy.TaxonTypeEnum Implements ITaxon.DisplayTaxonType
            Get
                Return mDisplayTaxonType
            End Get
            Set(ByVal Value As Taxonomy.TaxonTypeEnum)
                mDisplayTaxonType = Value
            End Set
        End Property
        Private mDisplayTaxonType As Taxonomy.TaxonTypeEnum

        Public Overridable Property DisplayTaxonTypeDescription() As String Implements ITaxon.DisplayTaxonTypeDescription
            Get
                Return GetTaxonTypeDescription(DisplayTaxonType)
            End Get
            Set(ByVal Value As String)
                Value = Value 'Throw New ApplicationException("This property is readonly")
            End Set
        End Property

        Public Overridable Property TaxonType() As Taxonomy.TaxonTypeEnum Implements ITaxon.TaxonType
            Get
                Return mTaxonType
            End Get
            Set(ByVal Value As Taxonomy.TaxonTypeEnum)
                mTaxonType = Value
            End Set
        End Property
        Private mTaxonType As Taxonomy.TaxonTypeEnum

        Public Overridable Property TaxonTypeDescription() As String Implements ITaxon.TaxonTypeDescription
            Get
                Return GetTaxonTypeDescription(TaxonType)
            End Get
            Set(ByVal Value As String)
                Value = Value 'Throw New ApplicationException("This property is readonly")
            End Set
        End Property

        Public Property Validated() As Object Implements ITaxon.Validated
            Get
                Return mValidated
            End Get
            Set(ByVal Value As Object)
                mValidated = Value
            End Set
        End Property
        Private mValidated As Object

        Public Overridable Property CITESReference() As String Implements ITaxon.CITESReference
            Get
                Return mCITESReference
            End Get
            Set(ByVal Value As String)
                mCITESReference = Value
            End Set
        End Property
        Private mCITESReference As String

        Public Overridable Property DistributionComplete() As String Implements ITaxon.DistributionComplete
            Get
                Return mDistributionComplete
            End Get
            Set(ByVal Value As String)
                mDistributionComplete = Value
            End Set
        End Property
        Private mDistributionComplete As String

        Public Overridable Property CommonNames() As String Implements ITaxon.CommonNames
            Get
                Return mCommonNames
            End Get
            Set(ByVal Value As String)
                mCommonNames = Value
            End Set
        End Property
        Private mCommonNames As String

        Public Overridable Property IsCoral() As Boolean Implements ITaxon.IsCoral
            Get
                Return mIsCoral
            End Get
            Set(ByVal Value As Boolean)
                mIsCoral = Value
            End Set
        End Property
        Private mIsCoral As Boolean

        Public Property PaymentKingdom() As Application.PaymentKingdomEnum Implements ITaxon.PaymentKingdom
            Get
                Select Case Me.KingdomID
                    Case Taxonomy.TaxonomySearch.GetAnimalKingdom.KingdomID
                        Return Application.PaymentKingdomEnum.Animal
                    Case Taxonomy.TaxonomySearch.GetPlantKingdom.KingdomID
                        Return Application.PaymentKingdomEnum.Plant
                    Case Else
                        Return Application.PaymentKingdomEnum.NotApplicable
                End Select
            End Get
            Set(ByVal Value As Application.PaymentKingdomEnum)

            End Set
        End Property

        Public Property PaymentTaxonType() As Application.PaymentTaxonTypeEnum Implements ITaxon.PaymentTaxonType
            Get
                Select Case Me.TaxonType
                    Case TaxonTypeEnum.Epithet, TaxonTypeEnum.Species
                        Return Application.PaymentTaxonTypeEnum.Species
                    Case TaxonTypeEnum.Genus
                        Return Application.PaymentTaxonTypeEnum.Genus
                    Case TaxonTypeEnum.Family
                        Return Application.PaymentTaxonTypeEnum.Family
                    Case Else
                        Return Application.PaymentTaxonTypeEnum.NotApplicable
                End Select
            End Get
            Set(ByVal Value As Application.PaymentTaxonTypeEnum)

            End Set
        End Property

#End Region

#Region " Helper Functions "

        Private Overloads Function BuildShortScientificName(ByVal TaxonName As String) As String
            Return TaxonName.Trim
        End Function

        Private Overloads Function BuildShortScientificName(ByVal GenusName As String, ByVal SpeciesName As String) As String
            Return (GenusName & " " & SpeciesName).Trim
        End Function

        Private Overloads Function BuildShortScientificName(ByVal GenusName As String, ByVal SpeciesName As String, ByVal EpithetType As String, ByVal EpithetName As String) As String
            Return (GenusName & " " & SpeciesName & " " & EpithetType & " " & EpithetName).Trim
        End Function

        Private Overloads Function BuildShortScientificName(ByVal GenusName As String, ByVal SpeciesName As String, ByVal EpithetType As String, ByVal EpithetName As String, ByVal StockName As String) As String
            Return (GenusName & " " & SpeciesName & " " & EpithetType & " " & EpithetName & " " & StockName).Trim
        End Function

        Private Overloads Function BuildLongScientificName(ByVal TaxonName As String) As String
            Return TaxonName.Trim
        End Function

        Private Overloads Function BuildLongScientificName(ByVal TaxonName As String, ByVal TaxonAuthor As String) As String
            Return (TaxonName & " " & TaxonAuthor).Trim
        End Function

        Private Overloads Function BuildLongScientificName(ByVal GenusName As String, ByVal SpeciesName As String, ByVal SpeciesAuthor As String) As String
            Return (GenusName & " " & SpeciesName & " " & SpeciesAuthor).Trim
        End Function

        Private Overloads Function BuildLongScientificName(ByVal GenusName As String, ByVal SpeciesName As String, ByVal EpithetType As String, ByVal EpithetName As String, ByVal EpithetAuthor As String) As String
            Return (GenusName & " " & SpeciesName & " " & EpithetType & " " & EpithetName & " " & EpithetAuthor).Trim
        End Function

        Private Overloads Function BuildLongScientificName(ByVal GenusName As String, ByVal SpeciesName As String, ByVal EpithetType As String, ByVal EpithetName As String, ByVal EpithetAuthor As String, ByVal StockName As String) As String
            Return (GenusName & " " & SpeciesName & " " & EpithetType & " " & EpithetName & " " & EpithetAuthor & " " & StockName).Trim
        End Function

        Private Sub SetScientificNames(ByVal TaxonType As TaxonTypeEnum, ByVal ParentKingdomID As Int32, ByVal ParentTaxonID As Int32, ByVal ParentTaxonTypeID As Int32, ByVal tran As SqlClient.SqlTransaction)
            Select Case TaxonType
                Case TaxonTypeEnum.Kingdom, _
                TaxonTypeEnum.Phylum, _
                TaxonTypeEnum.Class, _
                TaxonTypeEnum.Order
                    Me.LongScientificNameUnformatted = BuildLongScientificName(Me.TaxonNameUnformatted)
                    Me.LongScientificNameHTMLFormatted = BuildLongScientificName(Me.TaxonNameHTMLFormatted)
                    Me.ShortScientificNameUnformatted = BuildShortScientificName(Me.TaxonNameUnformatted)
                    Me.ShortScientificNameHTMLFormatted = BuildShortScientificName(Me.TaxonNameHTMLFormatted)
                Case TaxonTypeEnum.Family, _
                TaxonTypeEnum.Genus
                    Me.LongScientificNameUnformatted = BuildLongScientificName(Me.TaxonNameUnformatted, Me.TaxonAuthorUnformatted)
                    Me.LongScientificNameHTMLFormatted = BuildLongScientificName(Me.TaxonNameHTMLFormatted, Me.TaxonAuthorHTMLFormatted)
                    Me.ShortScientificNameUnformatted = BuildShortScientificName(Me.TaxonNameUnformatted)
                    Me.ShortScientificNameHTMLFormatted = BuildShortScientificName(Me.TaxonNameHTMLFormatted)
                Case TaxonTypeEnum.Species
                    Dim GenusTaxon As New BOTaxon(ParentKingdomID, ParentTaxonID, ParentTaxonTypeID, tran)
                    Me.LongScientificNameUnformatted = BuildLongScientificName(GenusTaxon.TaxonNameUnformatted, Me.TaxonNameUnformatted, Me.TaxonAuthorUnformatted)
                    Me.LongScientificNameHTMLFormatted = BuildLongScientificName(GenusTaxon.TaxonNameHTMLFormatted, Me.TaxonNameHTMLFormatted, Me.TaxonAuthorHTMLFormatted)
                    Me.ShortScientificNameUnformatted = BuildShortScientificName(GenusTaxon.TaxonNameUnformatted, Me.TaxonNameUnformatted)
                    Me.ShortScientificNameHTMLFormatted = BuildShortScientificName(GenusTaxon.TaxonNameHTMLFormatted, Me.TaxonNameHTMLFormatted)
                Case TaxonTypeEnum.Epithet
                    Dim EpithetParent As New BOTaxon(Me.ParentKingdomID, Me.ParentTaxonID, Me.ParentTaxonTypeID, tran)
                    Dim Genus As BOTaxon
                    Dim Species As BOTaxon
                    'Check whether the parent of the epithet is a species or taxon.
                    Select Case EpithetParent.TaxonType
                        Case TaxonTypeEnum.Species
                            'The parent of the epithet is a species so get the genus.
                            Species = EpithetParent
                            Genus = New BOTaxon(Species.ParentKingdomID, Species.ParentTaxonId, Species.ParentTaxonTypeID, tran)
                            Me.LongScientificNameUnformatted = BuildLongScientificName(Genus.TaxonNameUnformatted, Species.TaxonNameUnformatted, Me.TaxonEpithetUnformatted, Me.TaxonNameUnformatted, Me.TaxonAuthorUnformatted)
                            Me.LongScientificNameHTMLFormatted = BuildLongScientificName(Genus.TaxonNameHTMLFormatted, Species.TaxonNameHTMLFormatted, Me.TaxonEpithetHTMLFormatted, Me.TaxonNameHTMLFormatted, Me.TaxonAuthorHTMLFormatted)
                            Me.ShortScientificNameUnformatted = BuildShortScientificName(Genus.TaxonNameUnformatted, Species.TaxonNameUnformatted, Me.TaxonEpithetUnformatted, Me.TaxonNameUnformatted)
                            Me.ShortScientificNameHTMLFormatted = BuildShortScientificName(Genus.TaxonNameHTMLFormatted, Species.TaxonNameHTMLFormatted, Me.TaxonEpithetHTMLFormatted, Me.TaxonNameHTMLFormatted)
                        Case TaxonTypeEnum.Genus
                            Genus = EpithetParent
                            'There is no Species.
                            Me.LongScientificNameUnformatted = BuildLongScientificName(Genus.TaxonNameUnformatted, String.Empty, Me.TaxonEpithetUnformatted, Me.TaxonNameUnformatted, Me.TaxonAuthorUnformatted)
                            Me.LongScientificNameHTMLFormatted = BuildLongScientificName(Genus.TaxonNameHTMLFormatted, String.Empty, Me.TaxonEpithetHTMLFormatted, Me.TaxonNameHTMLFormatted, Me.TaxonAuthorHTMLFormatted)
                            Me.ShortScientificNameUnformatted = BuildShortScientificName(Genus.TaxonNameUnformatted, String.Empty, Me.TaxonEpithetUnformatted, Me.TaxonNameUnformatted)
                            Me.ShortScientificNameHTMLFormatted = BuildShortScientificName(Genus.TaxonNameHTMLFormatted, String.Empty, Me.TaxonEpithetHTMLFormatted, Me.TaxonNameHTMLFormatted)
                        Case Else
                            Throw New Exception("Business rule violation - only Genera or Species can have associated epithets")
                    End Select
                Case TaxonTypeEnum.Stock
                    Dim StockParent As New BOTaxon(Me.ParentKingdomID, Me.ParentTaxonID, Me.ParentTaxonTypeID, tran)
                    Dim Genus As BOTaxon
                    Dim Species As BOTaxon
                    Dim Epithet As BOTaxon
                    Select Case StockParent.TaxonType
                        Case TaxonTypeEnum.Epithet
                            'The parent of the stock is an epithet so get the species.
                            Epithet = StockParent
                            Dim EpithetParent As BOTaxon = New BOTaxon(Epithet.ParentKingdomID, Epithet.ParentTaxonID, Epithet.ParentTaxonTypeID)
                            Select Case EpithetParent.TaxonType
                                Case TaxonTypeEnum.Species
                                    'The parent of the epithet is a species so get the genus.
                                    Species = EpithetParent
                                    Genus = New BOTaxon(Species.ParentKingdomID, Species.ParentTaxonId, Species.ParentTaxonTypeID, tran)
                                    Me.LongScientificNameUnformatted = BuildLongScientificName(Genus.TaxonNameUnformatted, Species.TaxonNameUnformatted, Epithet.TaxonEpithetUnformatted, Epithet.TaxonNameUnformatted, Epithet.TaxonAuthorUnformatted, Me.TaxonNameUnformatted)
                                    Me.LongScientificNameHTMLFormatted = BuildLongScientificName(Genus.TaxonNameHTMLFormatted, Species.TaxonNameHTMLFormatted, Epithet.TaxonEpithetHTMLFormatted, Epithet.TaxonNameHTMLFormatted, Epithet.TaxonAuthorHTMLFormatted, Me.TaxonNameHTMLFormatted)
                                    Me.ShortScientificNameUnformatted = BuildShortScientificName(Genus.TaxonNameUnformatted, Species.TaxonNameUnformatted, Epithet.TaxonEpithetUnformatted, Epithet.TaxonNameUnformatted, Me.TaxonNameUnformatted)
                                    Me.ShortScientificNameHTMLFormatted = BuildShortScientificName(Genus.TaxonNameHTMLFormatted, Species.TaxonNameHTMLFormatted, Epithet.TaxonEpithetHTMLFormatted, Epithet.TaxonNameHTMLFormatted, Me.TaxonNameHTMLFormatted)
                                Case TaxonTypeEnum.Genus
                                    Genus = EpithetParent
                                    'There is no Species.
                                    Me.LongScientificNameUnformatted = BuildLongScientificName(Genus.TaxonNameUnformatted, String.Empty, Epithet.TaxonEpithetUnformatted, Epithet.TaxonNameUnformatted, Epithet.TaxonAuthorUnformatted, Me.TaxonNameUnformatted)
                                    Me.LongScientificNameHTMLFormatted = BuildLongScientificName(Genus.TaxonNameHTMLFormatted, String.Empty, Epithet.TaxonEpithetHTMLFormatted, Epithet.TaxonNameHTMLFormatted, Epithet.TaxonAuthorHTMLFormatted, Me.TaxonNameHTMLFormatted)
                                    Me.ShortScientificNameUnformatted = BuildShortScientificName(Genus.TaxonNameUnformatted, String.Empty, Epithet.TaxonEpithetUnformatted, Epithet.TaxonNameUnformatted, Me.TaxonNameUnformatted)
                                    Me.ShortScientificNameHTMLFormatted = BuildShortScientificName(Genus.TaxonNameHTMLFormatted, String.Empty, Epithet.TaxonEpithetHTMLFormatted, Epithet.TaxonNameHTMLFormatted, Me.TaxonNameHTMLFormatted)
                                Case Else
                                    Throw New Exception("Business rule violation - only Genera or Species can have associated epithets")
                            End Select
                        Case TaxonTypeEnum.Species
                            Species = StockParent
                            Genus = New BOTaxon(Species.ParentKingdomID, Species.ParentTaxonId, Species.ParentTaxonTypeID, tran)
                            Me.LongScientificNameUnformatted = BuildLongScientificName(Genus.TaxonNameUnformatted, Species.TaxonNameUnformatted, String.Empty, String.Empty, Species.TaxonAuthorUnformatted, Me.TaxonNameUnformatted)
                            Me.LongScientificNameHTMLFormatted = BuildLongScientificName(Genus.TaxonNameHTMLFormatted, Species.TaxonNameHTMLFormatted, String.Empty, String.Empty, Species.TaxonAuthorHTMLFormatted, Me.TaxonNameHTMLFormatted)
                            Me.ShortScientificNameUnformatted = BuildShortScientificName(Genus.TaxonNameUnformatted, Species.TaxonNameUnformatted, String.Empty, String.Empty, Me.TaxonNameUnformatted)
                            Me.ShortScientificNameHTMLFormatted = BuildShortScientificName(Genus.TaxonNameHTMLFormatted, Species.TaxonNameHTMLFormatted, String.Empty, String.Empty, Me.TaxonNameHTMLFormatted)
                            'There is no Species.
                        Case Else
                            Throw New Exception("Business rule violation - only Species or Epithets can have associated stock names")
                    End Select
                Case Else
                    Throw New ApplicationException("The taxon type " & CStr(TaxonType) & " is not recognised.")
            End Select
        End Sub

        Private Function GetCommonNamesString() As String
            Dim CommonNames As Taxonomy.BOCommonName()
            CommonNames = Me.GetCommonNames()
            Dim CommonNameString As New Text.StringBuilder
            If CommonNames Is Nothing = False Then
                For Each CommonName As Taxonomy.BOCommonName In CommonNames
                    If CommonNameString.Length > 0 Then
                        CommonNameString.Append(", ")
                    End If
                    CommonNameString.Append(CommonName.Name)
                Next
            End If
            Return CommonNameString.ToString
        End Function

        Private Function GetTaxonTypeDescription(ByVal TaxonType As TaxonTypeEnum) As String
            Dim TaxonTypeDescriptionValue As String
            Select Case TaxonType
                Case TaxonTypeEnum.Species
                    TaxonTypeDescriptionValue = "Species"
                Case TaxonTypeEnum.Genus
                    TaxonTypeDescriptionValue = "Genus"
                Case TaxonTypeEnum.Family
                    TaxonTypeDescriptionValue = "Family"
                Case TaxonTypeEnum.Order
                    TaxonTypeDescriptionValue = "Order"
                Case TaxonTypeEnum.Class
                    TaxonTypeDescriptionValue = "Class"
                Case TaxonTypeEnum.Phylum
                    TaxonTypeDescriptionValue = "Phylum"
                Case TaxonTypeEnum.Kingdom
                    TaxonTypeDescriptionValue = "Kingdom"
                Case TaxonTypeEnum.Epithet
                    TaxonTypeDescriptionValue = "Sub-Category"
                Case TaxonTypeEnum.Stock
                    TaxonTypeDescriptionValue = "Stock"
                Case Else
                    TaxonTypeDescriptionValue = String.Empty
            End Select
            Return TaxonTypeDescriptionValue
        End Function

        Private Function GetSource() As TaxonomyData.TaxonomyRowSourceEnum
            If Me.KingdomID <> 0 Then
                Dim SourceDS As DataSet = [DO].DataObjects.Sprocs.dbo_usp_GetPlantKingdom(Nothing, GetType(DataSet))
                If Not SourceDS Is Nothing AndAlso SourceDS.Tables.Count > 0 AndAlso SourceDS.Tables(0).Rows.Count > 0 Then
                    If CType(SourceDS.Tables(0).Rows(0).Item("KingdomID"), Int32) = Me.KingdomID Then
                        Return TaxonomyData.TaxonomyRowSourceEnum.Kew
                    Else
                        Select Case Me.TaxonType
                            Case TaxonTypeEnum.Stock, TaxonTypeEnum.Species, TaxonTypeEnum.Epithet
                                Return TaxonomyData.TaxonomyRowSourceEnum.Standard
                            Case Else
                                Return TaxonomyData.TaxonomyRowSourceEnum.Higher
                        End Select
                    End If
                End If
            End If
        End Function
#End Region

#Region " Save "

#End Region

#Region " Validate "
        Protected Overridable Overloads Function Validate() As ValidationManager Implements IValidate.Validate
            Return Validate(True, 0)
        End Function

        Protected Overloads Function Validate(ByVal writeFlag As Boolean) As ValidationManager Implements ITaxon.Validate
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

#Region " Operations "

        Public Function GetNotes() As Taxonomy.TaxonomyNote()
            Return GetNotes(Me.Id)
        End Function

        Public Function GetNotes(ByVal TaxonomyId As Int32) As Taxonomy.TaxonomyNote()
            Dim Taxon As New DataObjects.Entity.TaxonomyTaxon(TaxonomyId)
            Return GetNotes(Taxon)
        End Function

        Function GetNotes(ByVal Taxon As DataObjects.Entity.TaxonomyTaxon) As Taxonomy.TaxonomyNote()
            Dim Notes As DataObjects.EntitySet.NoteSet = Taxon.GetRelatedNotes
            If Not Notes Is Nothing AndAlso _
                Notes.Count > 0 Then
                Dim NoteList(Notes.Count - 1) As Taxonomy.TaxonomyNote
                Dim Index As Int32 = 0
                For Each note As DataObjects.Entity.Note In Notes
                    NoteList(Index) = New Taxonomy.TaxonomyNote(note, Taxon.Id)
                    Index += 1
                    'TODO: Sort Notes
                Next note
                Return NoteList
            Else
                Dim NoteList(-1) As Taxonomy.TaxonomyNote
                Return NoteList
            End If

        End Function

        Public Function GetSummaryQuotaAll() As Taxonomy.BOQuotaDisplay()
            Try
                Dim DOQuotas As [DO].dataobjects.Views.Collection.TaxonomyExportQuotaAllBoundCollection
                'Check if the selected taxon is a stock name.
                DOQuotas = Me.GetSummaryQuotaDO()
                If DOQuotas Is Nothing = False _
                AndAlso DOQuotas.Count > 0 Then
                    Dim BOQuotas(DOQuotas.Count - 1) As Taxonomy.BOQuotaDisplay
                    Dim DOQuotaIdx As Int32
                    For DOQuotaIdx = 0 To DOQuotas.Count - 1
                        BOQuotas(DOQuotaIdx) = New Taxonomy.BOQuotaDisplay(DOQuotas(DOQuotaIdx))
                    Next
                    Return BOQuotas
                Else
                    Dim BOQuotas(-1) As Taxonomy.BOQuotaDisplay
                    Return BOQuotas
                    'NT 10/05/05 - Best practise is to return an empty array. 'Return Nothing
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetHistoricAllQuota() As Taxonomy.BOQuotaDisplay()

            Dim DOQuotas As DataObjects.Views.Collection.TaxonomyExportQuotaAllBoundCollection
            Dim ThisTaxon As BOTaxon
            ThisTaxon = Me
            Do
                DOQuotas = DataObjects.Views.Service.TaxonomyExportQuotaAllService.GetHistoricByTaxon( _
                    ThisTaxon.KingdomID, ThisTaxon.TaxonId, ThisTaxon.TaxonTypeID)
                If DOQuotas Is Nothing = True _
                OrElse DOQuotas.Count = 0 Then
                    ThisTaxon = ThisTaxon.GetParentTaxon
                End If
            Loop Until (DOQuotas Is Nothing = False AndAlso DOQuotas.Count > 0) _
            Or ThisTaxon.TaxonType = TaxonTypeEnum.Order
            Dim QuotaArrayList As New ArrayList
            For Each DOQuota As DataObjects.Views.Entity.TaxonomyExportQuotaAll In DOQuotas
                Dim BOQuota As New BOQuotaDisplay(DOQuota)
                QuotaArrayList.Add(BOQuota)
            Next
            If QuotaArrayList.Count > 0 Then
                Dim BOQuotas(QuotaArrayList.Count) As BOQuotaDisplay
                QuotaArrayList.CopyTo(BOQuotas)
                Return BOQuotas
            Else
                Dim BOQuotas(-1) As BOQuotaDisplay
                Return BOQuotas
            End If
        End Function



        Public Function GetSummaryDecisionAll() As Taxonomy.BODecisionDisplay()

            Dim DODecisions As [DO].dataobjects.Views.Collection.TaxonomyDecisionAllBoundCollection
            'It is a stock name so get the EU Decision.
            DODecisions = Me.GetSummaryDecisionDO()
            If DODecisions Is Nothing = False _
            AndAlso DODecisions.Count > 0 Then
                Dim BODecisions(DODecisions.Count - 1) As Taxonomy.BODecisionDisplay
                Dim DODecisionIdx As Int32
                For DODecisionIdx = 0 To DODecisions.Count - 1
                    BODecisions(DODecisionIdx) = New Taxonomy.BODecisionDisplay(DODecisions(DODecisionIdx))
                Next
                Return BODecisions
            Else
                Dim BODecisions(-1) As Taxonomy.BODecisionDisplay
                Return BODecisions
            End If
        End Function

        Public Function GetHistoricAllDecision() As Taxonomy.BODecisionDisplay()

            Dim DODecisions As DataObjects.Views.Collection.TaxonomyDecisionAllBoundCollection
            Dim ThisTaxon As BOTaxon
            ThisTaxon = Me
            Do
                DODecisions = DataObjects.Views.Service.TaxonomyDecisionAllService.GetHistoricByTaxon( _
                    ThisTaxon.KingdomID, ThisTaxon.TaxonId, ThisTaxon.TaxonTypeID)
                If DODecisions Is Nothing = True _
                OrElse DODecisions.Count = 0 Then
                    ThisTaxon = ThisTaxon.GetParentTaxon
                End If
            Loop Until (DODecisions Is Nothing = False AndAlso DODecisions.Count > 0) _
            Or ThisTaxon.TaxonType = TaxonTypeEnum.Order
            Dim DecisionArrayList As New ArrayList
            For Each DODecision As DataObjects.Views.Entity.TaxonomyDecisionAll In DODecisions
                Dim BODecision As New BODecisionDisplay(DODecision)
                DecisionArrayList.Add(BODecision)
            Next
            If DecisionArrayList.Count > 0 Then
                Dim BODecisions(DecisionArrayList.Count) As BODecisionDisplay
                DecisionArrayList.CopyTo(BODecisions)
                Return BODecisions
            Else
                Dim BODecisions(-1) As BODecisionDisplay
                Return BODecisions
            End If
        End Function

        Public Function GetHistoricAllLegislation() As Taxonomy.BOLegislationDisplay()
            Dim DOLegs As DataObjects.Views.Collection.TaxonomyLegislationAllBoundCollection
            Dim ThisTaxon As BOTaxon
            ThisTaxon = Me

            DOLegs = DataObjects.Views.Service.TaxonomyLegislationAllService.GetHistoricByTaxon( _
                ThisTaxon.KingdomID, ThisTaxon.TaxonId, ThisTaxon.TaxonTypeID)
            Dim LegArrayList As New ArrayList
            For Each DOLeg As DataObjects.Views.Entity.TaxonomyLegislationAll In DOLegs
                Dim BOLeg As New BOLegislationDisplay(DOLeg)
                LegArrayList.Add(BOLeg)
            Next
            If LegArrayList.Count > 0 Then
                Dim BOLegs(LegArrayList.Count) As BOLegislationDisplay
                LegArrayList.CopyTo(BOLegs)
                Return BOLegs
            Else
                Dim BOLegs(-1) As BOLegislationDisplay
                Return BOLegs
            End If
        End Function

        Public Function GetCurrentAllLegislation() As Taxonomy.BOLegislationDisplay()
            Dim DOLegs As DataObjects.Views.Collection.TaxonomyLegislationAllBoundCollection
            DOLegs = DataObjects.Views.Service.TaxonomyLegislationAllService.GetCurrentByTaxon(Me.KingdomID, Me.TaxonId, Me.TaxonTypeID)
            Dim LegArrayList As New ArrayList
            For Each DOLeg As DataObjects.Views.Entity.TaxonomyLegislationAll In DOLegs
                Dim BOLeg As New BOLegislationDisplay(DOLeg)
                LegArrayList.Add(BOLeg)
            Next
            If LegArrayList.Count > 0 Then
                Dim BOLegs(LegArrayList.Count) As BOLegislationDisplay
                LegArrayList.CopyTo(BOLegs)
                Return BOLegs
            Else
                Dim BOLegs(-1) As BOLegislationDisplay
                Return BOLegs
            End If
        End Function

        Public Function GetCurrentEULegislation() As Taxonomy.BOLegislationDisplay()
            Dim DOLegs As DataObjects.Views.Collection.TaxonomyLegislationEUBoundCollection
            DOLegs = DataObjects.Views.Service.TaxonomyLegislationEUService.GetCurrentByTaxon(Me.KingdomID, Me.TaxonId, Me.TaxonTypeID)
            If DOLegs Is Nothing = False AndAlso DOLegs.Count > 0 Then
                Dim BOArrayList As New ArrayList
                For Each DOLeg As [DO].DataObjects.Views.Entity.TaxonomyLegislationEU In DOLegs
                    BOArrayList.Add(New Taxonomy.BOLegislationDisplay(DOLeg))
                Next
                Dim BOLegs(DOLegs.Count - 1) As Taxonomy.BOLegislationDisplay
                BOArrayList.CopyTo(BOLegs)
                Return BOLegs
            Else
                Dim BOLegs(-1) As Taxonomy.BOLegislationDisplay
                Return BOLegs
            End If
        End Function

        Public Function GetSummaryEULegislationAll(ByVal AcceptedTaxon As BOTaxon) As Taxonomy.BOLegislationDisplay()

            Dim DOLegs As [DO].dataobjects.Views.Collection.TaxonomyLegislationEUBoundCollection
            'Check if the selected taxon is a stock name.
            If Me.TaxonType = TaxonTypeEnum.Stock Then
                'It is a stock name so get the EU legislation.
                DOLegs = Me.GetSummaryEULegislationDO()
            End If
            If DOLegs Is Nothing = True _
            OrElse DOLegs.Count = 0 Then
                If AcceptedTaxon Is Nothing = False Then
                    DOLegs = AcceptedTaxon.GetSummaryEULegislationDO()
                End If
            End If
            If DOLegs Is Nothing = False _
            AndAlso DOLegs.Count > 0 Then
                Dim BOLegs(DOLegs.Count - 1) As Taxonomy.BOLegislationDisplay
                Dim DOLegIdx As Int32
                For DOLegIdx = 0 To DOLegs.Count - 1
                    BOLegs(DOLegIdx) = New Taxonomy.BOLegislationDisplay(DOLegs(DOLegIdx))
                Next
                Return BOLegs
            Else
                Dim BOLegs(-1) As Taxonomy.BOLegislationDisplay
                Return BOLegs
            End If
        End Function

        Public Function GetSummaryEULegislationTop(ByVal AcceptedTaxon As BOTaxon) As Taxonomy.BOLegislationDisplay
            Dim BOTaxonEUValue As BOLegislationDisplay
            Dim DOTaxonEUValues As [DO].dataobjects.Views.Collection.TaxonomyLegislationEUBoundCollection
            'Check if the selected taxon is a stock name.
            If Me.TaxonType = TaxonTypeEnum.Stock Then
                'It is a stock name so get the EU legislation.
                DOTaxonEUValues = Me.GetSummaryEULegislationDO
                'Check if there is any EU legislation for the stock name.
                If DOTaxonEUValues Is Nothing = False _
                AndAlso DOTaxonEUValues.Count > 0 Then
                    'There is legislation for the stock name so use the top one.
                    BOTaxonEUValue = New BOLegislationDisplay(DOTaxonEUValues(0))
                End If
            End If
            If BOTaxonEUValue Is Nothing = True Then
                If AcceptedTaxon Is Nothing = False Then
                    DOTaxonEUValues = AcceptedTaxon.GetSummaryEULegislationDO()
                    'Check if there is any EU legislation for the accepted name.
                    If DOTaxonEUValues Is Nothing = False _
                    AndAlso DOTaxonEUValues.Count > 0 Then
                        'There is legislation for the accepted name so use the top one.
                        BOTaxonEUValue = New BOLegislationDisplay(DOTaxonEUValues(0))
                    End If
                End If
            End If
            Return BOTaxonEUValue
        End Function

        Private Function GetSummaryQuotaDO() As [DO].Dataobjects.Views.Collection.TaxonomyExportQuotaAllBoundCollection
            Dim DOQuotas As [DO].DataObjects.Views.Collection.TaxonomyExportQuotaAllBoundCollection
            Dim TestTaxon As BOTaxon = Me

            If TestTaxon.TaxonType = TaxonTypeEnum.Epithet Then
                DOQuotas = DataObjects.Views.Service.TaxonomyExportQuotaAllService.GetSummaryByTaxon(TestTaxon.KingdomID, TestTaxon.TaxonId, TestTaxon.TaxonTypeID)
                If DOQuotas Is Nothing = True Then
                    TestTaxon = TestTaxon.GetParentTaxon
                End If

            End If

            If TestTaxon.TaxonType = TaxonTypeEnum.Species Then
                DOQuotas = DataObjects.Views.Service.TaxonomyExportQuotaAllService.GetSummaryByTaxon(TestTaxon.KingdomID, TestTaxon.TaxonId, TestTaxon.TaxonTypeID)
                If DOQuotas Is Nothing = True Then
                    TestTaxon = TestTaxon.GetParentTaxon
                End If
            End If

            If TestTaxon.TaxonType = TaxonTypeEnum.Genus Then
                DOQuotas = DataObjects.Views.Service.TaxonomyExportQuotaAllService.GetSummaryByTaxon(TestTaxon.KingdomID, TestTaxon.TaxonId, TestTaxon.TaxonTypeID)
                If DOQuotas Is Nothing = True Then
                    TestTaxon = TestTaxon.GetParentTaxon
                End If
            End If

            If TestTaxon.TaxonType = TaxonTypeEnum.Family Then
                DOQuotas = DataObjects.Views.Service.TaxonomyExportQuotaAllService.GetSummaryByTaxon(TestTaxon.KingdomID, TestTaxon.TaxonId, TestTaxon.TaxonTypeID)
            End If

            Return DOQuotas
        End Function

        Private Function GetSummaryDecisionDO() As [DO].Dataobjects.Views.Collection.TaxonomyDecisionAllBoundCollection
            Dim DOLegs As [DO].DataObjects.Views.Collection.TaxonomyDecisionAllBoundCollection
            Dim TestTaxon As BOTaxon = Me

            If TestTaxon.TaxonType = TaxonTypeEnum.Epithet Then
                DOLegs = DataObjects.Views.Service.TaxonomyDecisionAllService.GetSummaryByTaxon(TestTaxon.KingdomID, TestTaxon.TaxonId, TestTaxon.TaxonTypeID)
                If DOLegs Is Nothing = True Then
                    TestTaxon = TestTaxon.GetParentTaxon
                End If

            End If

            If TestTaxon.TaxonType = TaxonTypeEnum.Species Then
                DOLegs = DataObjects.Views.Service.TaxonomyDecisionAllService.GetSummaryByTaxon(TestTaxon.KingdomID, TestTaxon.TaxonId, TestTaxon.TaxonTypeID)
                If DOLegs Is Nothing = True Then
                    TestTaxon = TestTaxon.GetParentTaxon
                End If
            End If

            If TestTaxon.TaxonType = TaxonTypeEnum.Genus Then
                DOLegs = DataObjects.Views.Service.TaxonomyDecisionAllService.GetSummaryByTaxon(TestTaxon.KingdomID, TestTaxon.TaxonId, TestTaxon.TaxonTypeID)
                If DOLegs Is Nothing = True Then
                    TestTaxon = TestTaxon.GetParentTaxon
                End If
            End If

            If TestTaxon.TaxonType = TaxonTypeEnum.Family Then
                DOLegs = DataObjects.Views.Service.TaxonomyDecisionAllService.GetSummaryByTaxon(TestTaxon.KingdomID, TestTaxon.TaxonId, TestTaxon.TaxonTypeID)
            End If

            Return DOLegs
        End Function

        Private Function GetSummaryEULegislationDO() As [DO].Dataobjects.Views.Collection.TaxonomyLegislationEUBoundCollection
            Dim DOLegs As [DO].DataObjects.Views.Collection.TaxonomyLegislationEUBoundCollection
            Dim TestTaxon As BOTaxon = Me

            If TestTaxon.TaxonType = TaxonTypeEnum.Epithet Then
                DOLegs = DataObjects.Views.Service.TaxonomyLegislationEUService.GetSummaryByTaxon(TestTaxon.KingdomID, TestTaxon.TaxonId, TestTaxon.TaxonTypeID)
                If DOLegs Is Nothing = True Then
                    TestTaxon = TestTaxon.GetParentTaxon
                End If

            End If

            If TestTaxon.TaxonType = TaxonTypeEnum.Species Then
                DOLegs = DataObjects.Views.Service.TaxonomyLegislationEUService.GetSummaryByTaxon(TestTaxon.KingdomID, TestTaxon.TaxonId, TestTaxon.TaxonTypeID)
                If DOLegs Is Nothing = True Then
                    TestTaxon = TestTaxon.GetParentTaxon
                End If
            End If

            If TestTaxon.TaxonType = TaxonTypeEnum.Genus Then
                DOLegs = DataObjects.Views.Service.TaxonomyLegislationEUService.GetSummaryByTaxon(TestTaxon.KingdomID, TestTaxon.TaxonId, TestTaxon.TaxonTypeID)
                If DOLegs Is Nothing = True Then
                    TestTaxon = TestTaxon.GetParentTaxon
                End If
            End If

            If TestTaxon.TaxonType = TaxonTypeEnum.Family Then
                DOLegs = DataObjects.Views.Service.TaxonomyLegislationEUService.GetSummaryByTaxon(TestTaxon.KingdomID, TestTaxon.TaxonId, TestTaxon.TaxonTypeID)
            End If

            Return DOLegs
        End Function


        Public Function GetCurrentCITESLegislation() As Taxonomy.BOLegislationDisplay()
            Dim DOLegs As DataObjects.Views.Collection.TaxonomyLegislationCITESBoundCollection
            DOLegs = DataObjects.Views.Service.TaxonomyLegislationCITESService.GetCurrentByTaxon(Me.KingdomID, Me.TaxonId, Me.TaxonTypeID)
            If DOLegs Is Nothing = False AndAlso DOLegs.Count > 0 Then
                Dim BOArrayList As New ArrayList
                For Each DOLeg As [DO].DataObjects.Views.Entity.TaxonomyLegislationCITES In DOLegs
                    BOArrayList.Add(New Taxonomy.BOLegislationDisplay(DOLeg))
                Next
                Dim BOLegs(DOLegs.Count - 1) As Taxonomy.BOLegislationDisplay
                BOArrayList.CopyTo(BOLegs)
                Return BOLegs
            Else
                Dim BOLegs(-1) As Taxonomy.BOLegislationDisplay
                Return BOLegs
            End If
        End Function

        Public Shared Function GetSummaryECLegislationAllList(ByVal Specie As Application.BOSpecie) As String()
            Try
                Dim alLeg As New ArrayList
                If Specie Is Nothing = False _
                AndAlso Specie.Taxa Is Nothing = False Then
                    For Each AppTaxon As Application.BOTaxonIdentifier In Specie.Taxa
                        Dim TaxTaxon As New BOTaxon(AppTaxon.KingdomID, AppTaxon.TaxonID, AppTaxon.TaxonTypeID)
                        Dim arLegDisplay() As Taxonomy.BOLegislationDisplay = TaxTaxon.GetSummaryEULegislationAll(TaxTaxon)
                        If arLegDisplay Is Nothing = False Then
                            For LegDisplayIdx As Int32 = 0 To arLegDisplay.Length - 1
                                If alLeg.Contains(arLegDisplay(LegDisplayIdx).Listing) = False Then
                                    alLeg.Add(arLegDisplay(LegDisplayIdx).Listing)
                                End If
                            Next
                        End If
                    Next
                    alLeg.Sort()
                End If
                Dim arLegs(alLeg.Count - 1) As String
                alLeg.CopyTo(arLegs)
                Return arLegs
            Catch ex As Exception
                Throw New Exception("Cannot get summary EC legislation all list", ex)
            End Try
        End Function

        Public Shared Function GetSummaryCITESLegislationAllList(ByVal Specie As Application.BOSpecie) As String()
            Try
                Dim alLeg As New ArrayList
                If Specie Is Nothing = False _
                AndAlso Specie.Taxa Is Nothing = False Then
                    For Each AppTaxon As Application.BOTaxonIdentifier In Specie.Taxa
                        Dim TaxTaxon As New BOTaxon(AppTaxon.KingdomID, AppTaxon.TaxonID, AppTaxon.TaxonTypeID)
                        Dim arLegDisplay() As Taxonomy.BOLegislationDisplay = TaxTaxon.GetSummaryCITESLegislationAll(TaxTaxon)
                        If arLegDisplay Is Nothing = False Then
                            For LegDisplayIdx As Int32 = 0 To arLegDisplay.Length - 1
                                If alLeg.Contains(arLegDisplay(LegDisplayIdx).Listing) = False Then
                                    alLeg.Add(arLegDisplay(LegDisplayIdx).Listing)
                                End If
                            Next
                        End If
                    Next
                    alLeg.Sort()
                End If
                Dim arLegs(alLeg.Count - 1) As String
                alLeg.CopyTo(arLegs)
                Return arLegs
            Catch ex As Exception
                Throw New Exception("Cannot get summary CITES legislation all list", ex)
            End Try
        End Function

        Public Function GetSummaryCITESLegislationAll(ByVal AcceptedTaxon As BOTaxon) As Taxonomy.BOLegislationDisplay()
            Dim DOLegs As [DO].dataobjects.Views.Collection.TaxonomyLegislationCITESBoundCollection
            'Check if the selected taxon is a stock name.
            If Me.TaxonType = TaxonTypeEnum.Stock Then
                'It is a stock name so get the CITES legislation.
                DOLegs = Me.GetSummaryCITESLegislationDO()
            End If
            If DOLegs Is Nothing = True _
            OrElse DOLegs.Count = 0 Then
                If AcceptedTaxon Is Nothing = False Then
                    DOLegs = AcceptedTaxon.GetSummaryCITESLegislationDO()
                End If
            End If
            If DOLegs Is Nothing = False _
            AndAlso DOLegs.Count > 0 Then
                Dim BOLegs(DOLegs.Count - 1) As Taxonomy.BOLegislationDisplay
                Dim DOLegIdx As Int32
                For DOLegIdx = 0 To DOLegs.Count - 1
                    BOLegs(DOLegIdx) = New Taxonomy.BOLegislationDisplay(DOLegs(DOLegIdx))
                Next
                Return BOLegs
            Else
                Dim BOLegs(-1) As Taxonomy.BOLegislationDisplay
                Return BOLegs
            End If

        End Function

        Public Function GetSummaryCITESLegislationTop(ByVal AcceptedTaxon As BOTaxon) As Taxonomy.BOLegislationDisplay
            Dim BOTaxonCITESValue As BOLegislationDisplay = Nothing
            Dim DOTaxonCITESValues As [DO].dataobjects.Views.Collection.TaxonomyLegislationCITESBoundCollection
            'Check if the selected taxon is a stock name.
            If Me.TaxonType = TaxonTypeEnum.Stock Then
                'It is a stock name so get the CITES legislation.
                DOTaxonCITESValues = Me.GetSummaryCITESLegislationDO
                'Check if there is any CITES legislation for the stock name.
                If DOTaxonCITESValues Is Nothing = False _
                AndAlso DOTaxonCITESValues.Count > 0 Then
                    'There is legislation for the stock name so use the top one.
                    BOTaxonCITESValue = New BOLegislationDisplay(DOTaxonCITESValues(0))
                End If
            End If
            If BOTaxonCITESValue Is Nothing = True Then
                If AcceptedTaxon Is Nothing = False Then
                    DOTaxonCITESValues = AcceptedTaxon.GetSummaryCITESLegislationDO()
                    'Check if there is any CITES legislation for the accepted name.
                    If DOTaxonCITESValues Is Nothing = False _
                    AndAlso DOTaxonCITESValues.Count > 0 Then
                        'There is legislation for the accepted name so use the top one.
                        BOTaxonCITESValue = New BOLegislationDisplay(DOTaxonCITESValues(0))
                    End If
                End If
            End If
            Return BOTaxonCITESValue
        End Function

        Private Function GetSummaryCITESLegislationDO() As [DO].dataobjects.Views.Collection.TaxonomyLegislationCITESBoundCollection
            Dim DOLegs As [DO].DataObjects.Views.Collection.TaxonomyLegislationCITESBoundCollection
            Dim TestTaxon As BOTaxon = Me

            If TestTaxon.TaxonType = TaxonTypeEnum.Epithet Then
                DOLegs = DataObjects.Views.Service.TaxonomyLegislationCITESService.GetSummaryByTaxon(TestTaxon.KingdomID, TestTaxon.TaxonId, TestTaxon.TaxonTypeID)
                If DOLegs Is Nothing = True Then
                    TestTaxon = TestTaxon.GetParentTaxon
                End If

            End If

            If TestTaxon.TaxonType = TaxonTypeEnum.Species Then
                DOLegs = DataObjects.Views.Service.TaxonomyLegislationCITESService.GetSummaryByTaxon(TestTaxon.KingdomID, TestTaxon.TaxonId, TestTaxon.TaxonTypeID)
                If DOLegs Is Nothing = True Then
                    TestTaxon = TestTaxon.GetParentTaxon
                End If
            End If

            If TestTaxon.TaxonType = TaxonTypeEnum.Genus Then
                DOLegs = DataObjects.Views.Service.TaxonomyLegislationCITESService.GetSummaryByTaxon(TestTaxon.KingdomID, TestTaxon.TaxonId, TestTaxon.TaxonTypeID)
                If DOLegs Is Nothing = True Then
                    TestTaxon = TestTaxon.GetParentTaxon
                End If
            End If

            If TestTaxon.TaxonType = TaxonTypeEnum.Family Then
                DOLegs = DataObjects.Views.Service.TaxonomyLegislationCITESService.GetSummaryByTaxon(TestTaxon.KingdomID, TestTaxon.TaxonId, TestTaxon.TaxonTypeID)
            End If

            Return DOLegs

        End Function

        Public Function GetAquaticDistributionDisplay() As Taxonomy.BOSpeciesAquaticDistributionDisplay() Implements ITaxon.GetAquaticDistributionDisplay
            Dim SearchResults As DataObjects.Views.Collection.DisplayAquaticDistributionBoundCollection
            SearchResults = DataObjects.Views.Service.DisplayAquaticDistributionService.GetByTaxon(Me.KingdomID, Me.TaxonId, Me.TaxonTypeID)
            If SearchResults Is Nothing = False _
            AndAlso SearchResults.Count > 0 Then
                Dim BOArray(SearchResults.Count - 1) As BOSpeciesAquaticDistributionDisplay
                For FoundID As Int32 = 0 To SearchResults.Count - 1
                    With SearchResults(FoundID)
                        BOArray(FoundID) = _
                            New BOSpeciesAquaticDistributionDisplay(.SpeciesAquaticDistributionID)
                    End With
                Next
                Return BOArray
            Else
                Dim BOArray(-1) As BOSpeciesAquaticDistributionDisplay
                Return BOArray
            End If
        End Function

        Public Function GetDistributionDisplay() As Taxonomy.BOSpeciesDistributionDisplay() Implements ITaxon.GetDistributionDisplay
            Dim SearchResults As DataObjects.Views.Collection.DisplayDistributionBoundCollection
            SearchResults = DataObjects.Views.Service.DisplayDistributionService.GetByTaxon(Me.KingdomID, Me.TaxonId, Me.TaxonTypeID)
            If SearchResults Is Nothing = False _
            AndAlso SearchResults.Count > 0 Then
                Dim BOArray(SearchResults.Count - 1) As BOSpeciesDistributionDisplay
                For FoundID As Int32 = 0 To SearchResults.Count - 1
                    With SearchResults(FoundID)
                        BOArray(FoundID) = _
                            New BOSpeciesDistributionDisplay(.SpeciesBRUDistributionID, CType(.IsBRU, Boolean))
                    End With
                Next
                Return BOArray
            Else
                Dim BOArray(-1) As BOSpeciesDistributionDisplay
                Return BOArray
            End If
        End Function

        Public Function GetAnimalDelegationAuthority() As Taxonomy.BOAnimalDelegationAuthorityDisplay() Implements ITaxon.GetAnimalDelegationAuthority
            Dim SearchResults As DataObjects.Views.Collection.DisplayAnimalDelegationAuthorityBoundCollection
            SearchResults = DataObjects.Views.Service.DisplayAnimalDelegationAuthorityService.GetByTaxon(Me.KingdomID, Me.TaxonId, Me.TaxonTypeID)
            If SearchResults Is Nothing = False _
            AndAlso SearchResults.Count > 0 Then
                Dim BOArray(SearchResults.Count - 1) As BOAnimalDelegationAuthorityDisplay
                For FoundID As Int32 = 0 To SearchResults.Count - 1
                    BOArray(FoundID) = New BOAnimalDelegationAuthorityDisplay
                    With BOArray(FoundID)
                        .AnimalDelegationAuthorityID = SearchResults(FoundID).AnimalDelegationAuthorityID
                        .DelegationCode = SearchResults(FoundID).Code
                        .DelegationSubject = SearchResults(FoundID).Subject
                        .RTARoadmap = SearchResults(FoundID).HyperlinkRTARoadMap
                        .ApplicationTypeDescription = SearchResults(FoundID).Description
                    End With
                Next
                Return BOArray
            Else
                Dim BOArray(-1) As BOAnimalDelegationAuthorityDisplay
                Return BOArray
            End If
        End Function

        Public Function GetAnimalLicensing() As Taxonomy.BOAnimalLicensingDisplay
            Dim DOAnimalLicensingSet As [DO].DataObjects.EntitySet.AnimalLicensingDetailSet
            Dim DOAnimalLicenseDetail As New DataObjects.Service.AnimalLicensingDetailService
            DOAnimalLicensingSet = _
                DOAnimalLicenseDetail.GetByIndex_IX_TaxonomyAnimalLicensing_2(KingdomID, TaxonId, TaxonTypeID) 'MLD 22/4/5 param order changed

            If DOAnimalLicensingSet Is Nothing = False Then
                Dim BOAnimalLicensing As New Taxonomy.BOAnimalLicensingDisplay(DOAnimalLicensingSet.Entities(0))
                Return BOAnimalLicensing
            Else
                Return Nothing
            End If
        End Function

        Public Function GetLowerTaxa() As Taxonomy.BOTaxon() Implements ITaxon.GetLowerTaxa
            Select Case Me.TaxonType
                Case TaxonTypeEnum.Genus
                    Return GetLowerTaxa(2)
                Case Else
                    Return GetLowerTaxa(1)
            End Select
        End Function


        Public Function GetLowerTaxa(ByVal levels As Int32) As Taxonomy.BOTaxon() Implements ITaxon.GetLowerTaxa
            Dim SearchResults As DataObjects.Views.Collection.SearchTaxonomyTaxonBoundCollection

            SearchResults = DataObjects.Views.Service.SearchTaxonomyTaxonService.GetLowerTaxaByID(Id, levels)
            If SearchResults Is Nothing = False _
            AndAlso SearchResults.Count > 0 Then
                Dim TaxonArray(SearchResults.Count - 1) As BOTaxon
                For FoundTaxonID As Int32 = 0 To SearchResults.Count - 1
                    TaxonArray(FoundTaxonID) = New BOTaxon(SearchResults.Item(FoundTaxonID).Id)
                Next
                Return TaxonArray
            Else
                Dim TaxonArray(-1) As BOTaxon
                Return TaxonArray
            End If
        End Function

        Public Function GetHigherTaxa() As Taxonomy.BOTaxon() Implements ITaxon.GetHigherTaxa
            Dim SearchResults As DataObjects.Views.Collection.SearchTaxonomyTaxonBoundCollection
            SearchResults = DataObjects.Views.Service.SearchTaxonomyTaxonService.GetHigherTaxaByID(Id)
            If SearchResults Is Nothing = False _
            AndAlso SearchResults.Count > 0 Then
                Dim TaxonArray(SearchResults.Count - 1) As BOTaxon
                For FoundTaxonID As Int32 = 0 To SearchResults.Count - 1
                    TaxonArray(FoundTaxonID) = New BOTaxon(SearchResults.Item(FoundTaxonID).Id)
                Next
                Return TaxonArray
            Else
                Dim TaxonArray(-1) As BOTaxon
                Return TaxonArray
            End If
        End Function

        Public Function GetParentTaxon() As Taxonomy.BOTaxon Implements ITaxon.GetParentTaxon
            Dim ThisHigherTaxa() As BOTaxon = Me.GetHigherTaxa
            If ThisHigherTaxa.Length - 2 >= 0 Then
                Return ThisHigherTaxa(ThisHigherTaxa.Length - 2)
            Else
                Return Nothing
            End If
        End Function

        Public Function GetCommonNames() As Taxonomy.BOCommonName() Implements ITaxon.GetCommonNames
            'TODO: Nick - Get the common names for the accepted species if the current species is stock.
            Dim CommonNamesService As DataObjects.Service.TaxonomyCommonNameService = DataObjects.Entity.TaxonomyCommonName.ServiceObject
            Dim CommonNamesSet As DataObjects.EntitySet.TaxonomyCommonNameSet = CommonNamesService.GetByIndex_IX_TaxonomyCommonName(TaxonTypeID:=Me.TaxonTypeID, TaxonId:=Me.TaxonId, KingdomID:=Me.KingdomID)
            If CommonNamesSet Is Nothing = False _
                        AndAlso CommonNamesSet.Count > 0 Then
                Dim CommonNameArray(CommonNamesSet.Count - 1) As BOCommonName
                For FoundNameID As Int32 = 0 To CommonNamesSet.Count - 1
                    CommonNameArray(FoundNameID) = New BOCommonName(source:=CommonNamesSet.Entities(FoundNameID).SourceTable, Id:=CommonNamesSet.Entities(FoundNameID).CommonNameID)
                Next
                Return CommonNameArray
            Else
                Dim CommonNameArray(-1) As BOCommonName
                Return CommonNameArray
            End If
        End Function

        Public Function GetUsage() As BOUsage() Implements ITaxon.GetUsage

            Dim UsageService As DataObjects.Service.TaxonomyUsageService = DataObjects.Entity.TaxonomyUsage.ServiceObject
            Dim UsageSet As DataObjects.EntitySet.TaxonomyUsageSet = UsageService.GetByIndex_IX_TaxonomyUsage(TaxonTypeID:=Me.TaxonTypeID, TaxonId:=Me.TaxonId, KingdomID:=Me.KingdomID)
            If UsageSet Is Nothing = False _
            AndAlso UsageSet.Count > 0 Then
                Dim UsageArray(UsageSet.Count - 1) As BOUsage
                For FoundID As Int32 = 0 To UsageSet.Count - 1
                    UsageArray(FoundID) = New BOUsage(source:=GetSource, Id:=UsageSet.Entities(FoundID).Id)
                Next
                Return UsageArray
            Else
                Dim UsageArray(-1) As BOUsage
                Return UsageArray
            End If
        End Function

        Public Function GetStockNames() As BOTaxon() Implements ITaxon.GetStockNames
            Dim SearchResults As DataObjects.Views.Collection.SearchTaxonomyTaxonBoundCollection
            SearchResults = DataObjects.Views.Service.SearchTaxonomyTaxonService.GetStockNamesByTaxon(KingdomID:=Me.KingdomID, TaxonId:=Me.TaxonId, TaxonTypeID:=Me.TaxonTypeID)
            If SearchResults Is Nothing = False _
            AndAlso SearchResults.Count > 0 Then
                Dim TaxonArray(SearchResults.Count - 1) As BOTaxon
                For FoundTaxonID As Int32 = 0 To SearchResults.Count - 1
                    TaxonArray(FoundTaxonID) = New BOTaxon(SearchResults.Item(FoundTaxonID).Id)
                Next
                Return TaxonArray
            Else
                Dim TaxonArray(-1) As BOTaxon
                Return TaxonArray
            End If
        End Function

        Public Function GetSynonyms() As BOTaxon() Implements ITaxon.GetSynonyms
            Dim SearchResults As DataObjects.Views.Collection.SearchTaxonomyTaxonBoundCollection
            SearchResults = DataObjects.Views.Service.SearchTaxonomyTaxonService.GetSynonymsByID(Id:=Me.Id)
            If SearchResults Is Nothing = False _
            AndAlso SearchResults.Count > 0 Then
                Dim TaxonArray(SearchResults.Count - 1) As BOTaxon
                For FoundTaxonID As Int32 = 0 To SearchResults.Count - 1
                    TaxonArray(FoundTaxonID) = New BOTaxon(SearchResults.Item(FoundTaxonID).Id)
                Next
                Return TaxonArray
            Else
                Dim TaxonArray(-1) As BOTaxon
                Return TaxonArray
            End If
        End Function

        Public Function GetAcceptedNames() As BOTaxon() Implements ITaxon.GetAcceptedNames
            Dim SearchResults As DataObjects.Views.Collection.SearchTaxonomyTaxonBoundCollection
            SearchResults = DataObjects.Views.Service.SearchTaxonomyTaxonService.GetAcceptedTaxaByID(Me.Id)
            If SearchResults Is Nothing = False _
            AndAlso SearchResults.Count > 0 Then
                Dim TaxonArray(SearchResults.Count - 1) As BOTaxon
                For FoundTaxonID As Int32 = 0 To SearchResults.Count - 1
                    TaxonArray(FoundTaxonID) = New BOTaxon(SearchResults.Item(FoundTaxonID).Id)
                Next
                Return TaxonArray
            Else
                Dim TaxonArray(-1) As BOTaxon
                Return TaxonArray
            End If
        End Function

        Public Function GetConservationSummary() As BOConservationSummary Implements ITaxon.GetConservationSummary
            Dim TaxonConservationSummary As New BOConservationSummary
            With TaxonConservationSummary
                Dim Legislations As New [DO].DataObjects.Service.TaxonomyLegislationService
                Dim TaxonLegislations As [DO].DataObjects.EntitySet.TaxonomyLegislationSet
                TaxonLegislations = Legislations.GetByIndex_IX_TaxonomyLegislation(Me.TaxonTypeID, Me.TaxonId, Me.KingdomID)
                .HasLegislation = Application.Search.ApplicationSearch.ConvertToEnglishBoolean(Not (TaxonLegislations Is Nothing OrElse TaxonLegislations.Count <= 0))

                Dim Decisions As New [DO].DataObjects.Service.TaxonomyDecisionService
                Dim TaxonDecisions As [DO].DataObjects.EntitySet.TaxonomyDecisionSet
                TaxonDecisions = Decisions.GetByIndex_IX_TaxonomyDecision(Me.TaxonTypeID, Me.TaxonId, Me.KingdomID)
                .HasDecisions = Application.Search.ApplicationSearch.ConvertToEnglishBoolean(Not (TaxonDecisions Is Nothing OrElse TaxonDecisions.Count <= 0))

                Dim ExportQuotas As New [DO].DataObjects.Service.TaxonomyExportQuotaService
                Dim TaxonExportQuotas As [DO].DataObjects.EntitySet.TaxonomyExportQuotaSet
                TaxonExportQuotas = ExportQuotas.GetByIndex_IX_TaxonomyExportQuota(Me.TaxonTypeID, Me.TaxonId, Me.KingdomID)
                .HasExportQuotas = Application.Search.ApplicationSearch.ConvertToEnglishBoolean(Not (TaxonExportQuotas Is Nothing OrElse TaxonExportQuotas.Count <= 0))
            End With
            Return TaxonConservationSummary
        End Function
#End Region


    End Class
End Namespace
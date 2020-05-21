Namespace Taxonomy
    Public Class BOSpeciesDistributionDisplay
        Implements ISpeciesDistributionDisplay

#Region " Prelim code "
        Public Sub New()
            'This constructor is intentionally empty.
        End Sub

        Public Sub New(ByVal id As Int32, ByVal IsBRU As Boolean)
            LoadObject(id, IsBRU, Nothing)
        End Sub

        Public Sub New(ByVal id As Int32, ByVal IsBRU As Boolean, ByVal tran As SqlClient.SqlTransaction)
            LoadObject(id, IsBRU, tran)
        End Sub

        Private Overloads Function LoadObject(ByVal id As Int32, ByVal IsBRU As Boolean) As DataObjects.Views.Entity.DisplayDistribution
            Return Me.LoadObject(id, Nothing)
        End Function

        Private Overloads Function LoadObject(ByVal id As Int32, ByVal IsBRU As Boolean, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Views.Entity.DisplayDistribution

            Dim Service As New DataObjects.Views.Service.DisplayDistributionService
            Dim Result As DataObjects.Views.Entity.DisplayDistribution
            Result = Service.GetByDistributionID(id, IsBRU)
            If Result Is Nothing Then
                Throw New RecordDoesNotExist("DisplayDistribution", id)
            Else
                InitialiseMe(Result, tran)
                Return Result
            End If
        End Function

        Protected Overridable Sub InitialiseMe(ByVal Result As DataObjects.Views.Entity.DisplayDistribution, ByVal tran As SqlClient.SqlTransaction)
            With Result
                Me.Breeding = .Breeding
                Me.Certain = .Certain
                Me.Extinct = .Extinct
                Me.Introduced = .Introduced
                Me.KingdomID = .KingdomID
                Me.CountryLocation = .CountryLocation
                Me.BRULocation = .BRULocation
                Me.NoteID = .NoteID
                Me.Reintroduced = .ReIntroduced
                Me.TaxonID = .TaxonID
                Me.DistributionID = .SpeciesBRUDistributionID
                Me.Vagrant = .Vagrant
                Me.IsBRU = CType(.IsBRU, Boolean)
            End With
        End Sub

#End Region

#Region " Properties "

        Public Property IsBRU() As Boolean Implements ISpeciesDistributionDisplay.IsBRU
            Get
                Return mIsBRU
            End Get
            Set(ByVal Value As Boolean)
                mIsBRU = Value
            End Set
        End Property
        Private mIsBRU As Boolean

        Public Property Breeding() As String Implements ISpeciesDistributionDisplay.Breeding
            Get
                Return mBreeding
            End Get
            Set(ByVal Value As String)
                mBreeding = Value
            End Set
        End Property
        Private mBreeding As String

        Public Property Certain() As String Implements ISpeciesDistributionDisplay.Certain
            Get
                Return mCertain
            End Get
            Set(ByVal Value As String)
                mCertain = Value
            End Set
        End Property
        Private mCertain As String

        Public Property CountryLocation() As String Implements ISpeciesDistributionDisplay.CountryLocation
            Get
                Return mCountryLocation
            End Get
            Set(ByVal Value As String)
                mCountryLocation = Value
            End Set
        End Property
        Private mCountryLocation As String

        Public Property BRULocation() As String Implements ISpeciesDistributionDisplay.BRULocation
            Get
                Return mBRULocation
            End Get
            Set(ByVal Value As String)
                mBRULocation = Value
            End Set
        End Property
        Private mBRULocation As String

        Public Property DistributionID() As Int32 Implements ISpeciesDistributionDisplay.DistributionID
            Get
                Return mDistributionID
            End Get
            Set(ByVal Value As Int32)
                mDistributionID = Value
            End Set
        End Property
        Private mDistributionID As Int32

        Public Property Extinct() As String Implements ISpeciesDistributionDisplay.Extinct
            Get
                Return mExtinct
            End Get
            Set(ByVal Value As String)
                mExtinct = Value
            End Set
        End Property
        Private mExtinct As String

        Public Property Introduced() As String Implements ISpeciesDistributionDisplay.Introduced
            Get
                Return mIntroduced
            End Get
            Set(ByVal Value As String)
                mIntroduced = Value
            End Set
        End Property
        Private mIntroduced As String

        Public Property KingdomID() As Int32 Implements ISpeciesDistributionDisplay.KingdomID
            Get
                Return mKingdomID
            End Get
            Set(ByVal Value As Int32)
                mKingdomID = Value
            End Set
        End Property
        Private mKingdomID As Int32

        Public Property NoteID() As Int32 Implements ISpeciesDistributionDisplay.NoteID
            Get
                Return mNoteId
            End Get
            Set(ByVal Value As Int32)
                mNoteId = Value
            End Set
        End Property
        Private mNoteId As Int32

        Public Property Reintroduced() As String Implements ISpeciesDistributionDisplay.Reintroduced
            Get
                Return mReintroduced
            End Get
            Set(ByVal Value As String)
                mReintroduced = Value
            End Set
        End Property
        Private mReintroduced As String

        Public Property TaxonID() As Int32 Implements ISpeciesDistributionDisplay.TaxonID
            Get
                Return mTaxonID
            End Get
            Set(ByVal Value As Int32)
                mTaxonID = Value
            End Set
        End Property
        Private mTaxonID As Int32

        Public Property Vagrant() As String Implements ISpeciesDistributionDisplay.Vagrant
            Get
                Return mVagrant
            End Get
            Set(ByVal Value As String)
                mVagrant = Value
            End Set
        End Property
        Private mVagrant As String
#End Region
    End Class
End Namespace
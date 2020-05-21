Namespace Taxonomy
    Public Class BOSpeciesAquaticDistributionDisplay
        Implements ISpeciesAquaticDistributionDisplay

#Region " Prelim code "
        Public Sub New()
            'This constructor is intentionally empty.
        End Sub

        Public Sub New(ByVal id As Int32)
            LoadObject(id, Nothing)
        End Sub

        Public Sub New(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction)
            LoadObject(id, tran)
        End Sub

        Private Overloads Function LoadObject(ByVal id As Int32) As DataObjects.Views.Entity.DisplayAquaticDistribution
            Return Me.LoadObject(id, Nothing)
        End Function

        Private Overloads Function LoadObject(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Views.Entity.DisplayAquaticDistribution
            Dim Service As New DataObjects.Views.Service.DisplayAquaticDistributionService
            Dim Result As DataObjects.Views.Entity.DisplayAquaticDistribution 
            Result = Service.GetByDistributionID(id)
            If Result Is Nothing Then
                Throw New RecordDoesNotExist("DisplayAquaticDistribution", id)
            Else
                InitialiseMe(Result, tran)
                Return Result
            End If
        End Function

        Protected Overridable Sub InitialiseMe(ByVal Result As DataObjects.Views.Entity.DisplayAquaticDistribution, ByVal tran As SqlClient.SqlTransaction)
            With Result
                Me.Certain = .Certain
                Me.Extinct = .Extinct
                Me.Introduced = .Introduced
                Me.KingdomID = .KingdomID
                Me.Location = .RegionName
                Me.SubLocation = .RegionSubName
                Me.NoteID = .SpeciesAquaticDistributionNoteID
                Me.ReIntroduced = .ReIntroduced
                Me.TaxonID = .TaxonID
                Me.DistributionID = .SpeciesAquaticDistributionID
            End With
        End Sub

#End Region

#Region "Properties"

        Public Property Certain() As String Implements ISpeciesAquaticDistributionDisplay.Certain
            Get
                Return mCertain
            End Get
            Set(ByVal Value As String)
                mCertain = Value
            End Set
        End Property
        Private mCertain As String

        Public Property DistributionID() As Integer Implements ISpeciesAquaticDistributionDisplay.DistributionID
            Get
                Return mDistributionID
            End Get
            Set(ByVal Value As Integer)
                mDistributionID = Value
            End Set
        End Property
        Private mDistributionID As Int32

        Public Property Extinct() As String Implements ISpeciesAquaticDistributionDisplay.Extinct
            Get
                Return mExtinct
            End Get
            Set(ByVal Value As String)
                mExtinct = Value
            End Set
        End Property
        Private mExtinct As String

        Public Property Introduced() As String Implements ISpeciesAquaticDistributionDisplay.Introduced
            Get
                Return mIntroduced
            End Get
            Set(ByVal Value As String)
                mIntroduced = Value
            End Set
        End Property
        Private mIntroduced As String

        Public Property KingdomID() As Integer Implements ISpeciesAquaticDistributionDisplay.KingdomID
            Get
                Return mKingdomID
            End Get
            Set(ByVal Value As Integer)
                mKingdomID = Value
            End Set
        End Property
        Private mKingdomID As Int32

        Public Property Location() As String Implements ISpeciesAquaticDistributionDisplay.Location
            Get
                Return mLocation
            End Get
            Set(ByVal Value As String)
                mLocation = Value
            End Set
        End Property
        Private mLocation As String

        Public Property SubLocation() As String Implements ISpeciesAquaticDistributionDisplay.SubLocation
            Get
                Return mSubLocation
            End Get
            Set(ByVal Value As String)
                mSubLocation = Value
            End Set
        End Property
        Private mSubLocation As String

        Public Property NoteID() As Integer Implements ISpeciesAquaticDistributionDisplay.NoteID
            Get
                Return mNoteID
            End Get
            Set(ByVal Value As Integer)
                mNoteID = Value
            End Set
        End Property
        Private mNoteID As Int32

        Public Property Reintroduced() As String Implements ISpeciesAquaticDistributionDisplay.Reintroduced
            Get
                Return mReintroduced
            End Get
            Set(ByVal Value As String)
                mReintroduced = Value
            End Set
        End Property
        Private mReintroduced As String

        Public Property TaxonID() As Integer Implements ISpeciesAquaticDistributionDisplay.TaxonID
            Get
                Return mTaxonID
            End Get
            Set(ByVal Value As Integer)
                mTaxonID = Value
            End Set
        End Property
        Private mTaxonID As Int32

#End Region
    End Class
End Namespace
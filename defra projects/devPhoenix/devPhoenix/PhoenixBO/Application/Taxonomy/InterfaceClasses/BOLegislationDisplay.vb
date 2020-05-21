Namespace Taxonomy
    <Serializable()> _
    Public Class BOLegislationDisplay
        Implements ILegislationDisplay
        Implements IComparable

        Public Sub New()

        End Sub

        Public Sub NewObject(ByVal Source As TaxonomyData.TaxonomyRowSourceEnum, ByVal ID As Int32, ByVal ISO2CountryCode As String, ByVal Listing As String, ByVal DateListed As Object, ByVal LegislationShortName As String, ByVal IsSplitListed As String)
            CountryValue = ISO2CountryCode
            ListingValue = Listing
            If DateListed Is Nothing OrElse Not TypeOf DateListed Is DateTime Then
                ListingDateValue = ""
            Else
                ListingDateValue = CType(DateListed.ToString, System.DateTime).ToShortDateString
            End If

            NoticeValue = LegislationShortName
            SplitListedValue = IsSplitListed
            mID = ID
            Me.Source = Source
            Dim DOLegNote As DataObjects.Entity.TaxonomyLegislation = DataObjects.Entity.TaxonomyLegislation.GetById(Source, ID)
            mNote = DOLegNote.Note
        End Sub

        Public Sub New(ByVal DOLegislation As DataObjects.Views.Entity.TaxonomyLegislationAll)
            With DOLegislation
                If DOLegislation.IsDateListedNull Then
                    NewObject(CType(System.Enum.Parse(GetType(TaxonomyData.TaxonomyRowSourceEnum), .Source.ToString), TaxonomyData.TaxonomyRowSourceEnum), .LegislationID, .ISO2CountryCode, .Listing, Nothing, .LegislationShortName, .IsSplitListed.ToString)
                Else
                    NewObject(CType(System.Enum.Parse(GetType(TaxonomyData.TaxonomyRowSourceEnum), .Source.ToString), TaxonomyData.TaxonomyRowSourceEnum), .LegislationID, .ISO2CountryCode, .Listing, .DateListed, .LegislationShortName, .IsSplitListed.ToString)
                End If
            End With
        End Sub

        Public Sub New(ByVal DOLegislation As [DO].DataObjects.Views.Entity.TaxonomyLegislationCITES)
            With DOLegislation
                NewObject(CType(System.Enum.Parse(GetType(TaxonomyData.TaxonomyRowSourceEnum), .Source.ToString), TaxonomyData.TaxonomyRowSourceEnum), .LegislationID, .ISO2CountryCode, .Listing, .DateListed, .LegislationShortName, .IsSplitListed.ToString)
            End With
        End Sub

        Public Sub New(ByVal DOLegislation As [DO].DataObjects.Views.Entity.TaxonomyLegislationEU)
            With DOLegislation
                NewObject(CType(System.Enum.Parse(GetType(TaxonomyData.TaxonomyRowSourceEnum), .Source.ToString), TaxonomyData.TaxonomyRowSourceEnum), .LegislationID, .ISO2CountryCode, .Listing, .DateListed, .LegislationShortName, .IsSplitListed.ToString)
            End With
        End Sub

        Public Property Source() As TaxonomyData.TaxonomyRowSourceEnum
            Get
                Return mSource
            End Get
            Set(ByVal Value As TaxonomyData.TaxonomyRowSourceEnum)
                mSource = Value
            End Set
        End Property
        Private mSource As TaxonomyData.TaxonomyRowSourceEnum

        Public Property ID() As Int32 Implements ILegislationDisplay.ID
            Get
                Return mID
            End Get
            Set(ByVal Value As Int32)
                mID = Value
            End Set
        End Property
        Private mID As Int32

        Private NoticeValue As String
        Public Property Notice() As String Implements ILegislationDisplay.Notice
            Get
                Return NoticeValue
            End Get
            Set(ByVal Value As String)
                NoticeValue = Value
            End Set
        End Property

        Private ListingValue As String
        Public Property Listing() As String Implements ILegislationDisplay.Listing
            Get
                Return ListingValue
            End Get
            Set(ByVal Value As String)
                ListingValue = Value
            End Set
        End Property

        Private ListingDateValue As String
        Public Property ListingDate() As String Implements ILegislationDisplay.ListingDate
            Get
                Return ListingDateValue
            End Get
            Set(ByVal Value As String)
                ListingDateValue = Value
            End Set
        End Property

        Private SplitListedValue As String
        Public Property SplitListed() As String Implements ILegislationDisplay.SplitListed
            Get
                Return SplitListedValue
            End Get
            Set(ByVal Value As String)
                SplitListedValue = Value
            End Set
        End Property

        Private CountryValue As String
        Public Property Country() As String Implements ILegislationDisplay.Country
            Get
                Return CountryValue
            End Get
            Set(ByVal Value As String)
                CountryValue = Value
            End Set
        End Property

        Public Property Note() As String Implements ILegislationDisplay.Note
            Get
                Return mNote
            End Get
            Set(ByVal Value As String)
                mNote = Value
            End Set
        End Property
        Private mNote As String

        Public Property TruncatedNote() As String Implements ILegislationDisplay.TruncatedNote
            Get
                'If the note is less than or equal to 20 characters then display the whole note otherwise display up to the first 5 words.
                Dim stringtoreturn As String
                If Note Is Nothing = False Then
                    If Note.Length <= 20 Then
                        stringtoreturn = Note
                    Else
                        'Return the first 5 words.
                        Dim splitnote() As String = Note.Split((" ").ToCharArray)
                        stringtoreturn = String.Join(" ", splitnote, 0, System.Math.Min(5, splitnote.Length)) & " (more)"
                    End If
                Else
                    stringtoreturn = ""
                End If
                Return stringtoreturn
            End Get
            Set(ByVal Value As String)

            End Set
        End Property

        Public Function CompareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
            Try
                If TypeOf obj Is BOLegislationDisplay Then
                    Return String.Compare(Me.Listing, CType(obj, BOLegislationDisplay).Listing)
                Else
                    Throw New Exception("obj is not a BOLegislationDisplay object")
                End If
            Catch ex As Exception
                Throw New Exception("CompareTo failed", ex)
            End Try
        End Function
    End Class
End Namespace

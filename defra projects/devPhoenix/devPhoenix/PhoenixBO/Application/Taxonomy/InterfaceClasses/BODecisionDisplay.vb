Namespace Taxonomy
    <Serializable()> _
    Public Class BODecisionDisplay
        Implements IDecisionDisplay

        Public Sub New()

        End Sub

        Public Sub NewObject(ByVal Source As TaxonomyData.TaxonomyRowSourceEnum, ByVal DecisionID As Int32, ByVal ISO2CountryCode As String, ByVal Article4Point6ImportRestriction As String, ByVal DecisionDate As String, ByVal DecisionLevel As String, ByVal DecisionMiscellaneous As String, ByVal SRGOpinion As String, ByVal Note As String)
            mDecisionID = DecisionID
            mArticle4Point6ImportRestriction = Article4Point6ImportRestriction
            mDecisionDate = DecisionDate
            mDecisionLevel = DecisionLevel
            mDecisionMiscellaneous = DecisionMiscellaneous
            mISO2CountryCode = ISO2CountryCode
            mSRGOpinion = SRGOpinion
            mNote = Note
            mSource = Source
        End Sub

        Public Sub New(ByVal DODecision As DataObjects.Views.Entity.TaxonomyDecisionAll)
            With DODecision
                NewObject(CType(System.Enum.Parse(GetType(TaxonomyData.TaxonomyRowSourceEnum), .Source.ToString), TaxonomyData.TaxonomyRowSourceEnum), .DecisionID, .ISO2CountryCode, .Article4Point6ImportRestriction, .DecisionDate.ToString, .DecisionLevel, .DecisionMiscellaneous, .SRGOpinion, .Note)
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

        Public Property Article4Point6ImportRestriction() As String Implements IDecisionDisplay.Article4Point6ImportRestriction
            Get
                Return mArticle4Point6ImportRestriction
            End Get
            Set(ByVal Value As String)
                mArticle4Point6ImportRestriction = Value
            End Set
        End Property
        Private mArticle4Point6ImportRestriction As String

        Public Property DecisionDate() As String Implements IDecisionDisplay.DecisionDate
            Get
                Return CType(mDecisionDate, DateTime).ToShortDateString
            End Get
            Set(ByVal Value As String)
                mDecisionDate = Value
            End Set
        End Property
        Private mDecisionDate As String

        Public Property DecisionID() As Int32 Implements IDecisionDisplay.DecisionID
            Get
                Return mDecisionID
            End Get
            Set(ByVal Value As Integer)
                mDecisionID = Value
            End Set
        End Property
        Private mDecisionID As Int32

        Public Property DecisionLevel() As String Implements IDecisionDisplay.DecisionLevel
            Get
                Return mDecisionLevel
            End Get
            Set(ByVal Value As String)
                mDecisionLevel = Value
            End Set
        End Property
        Private mDecisionLevel As String

        Public Property DecisionMiscellaneous() As String Implements IDecisionDisplay.DecisionMiscellaneous
            Get
                Return mDecisionMiscellaneous
            End Get
            Set(ByVal Value As String)
                mDecisionMiscellaneous = Value
            End Set
        End Property
        Private mDecisionMiscellaneous As String

        Public Property ISO2CountryCode() As String Implements IDecisionDisplay.ISO2CountryCode
            Get
                Return mISO2CountryCode
            End Get
            Set(ByVal Value As String)
                mISO2CountryCode = Value
            End Set
        End Property
        Private mISO2CountryCode As String

        Public Property SRGOpinion() As String Implements IDecisionDisplay.SRGOpinion
            Get
                Return mSRGOpinion
            End Get
            Set(ByVal Value As String)
                mSRGOpinion = Value
            End Set
        End Property
        Private mSRGOpinion As String

        Public Property Note() As String Implements IDecisionDisplay.Note
            Get
                Return mNote
            End Get
            Set(ByVal Value As String)
                mNote = Value
            End Set
        End Property
        Private mNote As String

        Public Property TruncatedNote() As String Implements IDecisionDisplay.TruncatedNote
            Get
                Dim stringtoreturn As String
                If Note Is Nothing = False Then
                    If Note.Length <= 20 Then
                        stringtoreturn = Note
                    Else
                        'Return the first 5 words.
                        Dim splitnote() As String = Note.Split((" ").ToCharArray)
                        stringtoreturn = String.Join(" ", splitnote, 0, 5) & " (more)"
                    End If
                Else
                    stringtoreturn = ""
                End If
                Return stringtoreturn
            End Get
            Set(ByVal Value As String)

            End Set
        End Property
    End Class
End Namespace
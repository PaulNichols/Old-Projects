Namespace Application.Bird.Registration.Search
    <Serializable()> _
    Public Class Article10SearchResult
        Inherits BaseSearchResult

        Public Sub New()
        End Sub

        Public Property Article10Numbers() As String
            Get
                Return mArticle10Numbers
            End Get
            Set(ByVal Value As String)
                mArticle10Numbers = Value
            End Set
        End Property
        Private mArticle10Numbers As String

        Public Property IssueDate() As String
            Get
                Return mIssueDate
            End Get
            Set(ByVal Value As String)
                mIssueDate = Value
            End Set
        End Property
        Private mIssueDate As String

        Public Property PermitId() As Int32
            Get
                Return mPermitId
            End Get
            Set(Byval Value As Int32)
                mPermitId = Value
            End Set
        End Property
        Private mPermitId As Int32
    End Class
End Namespace
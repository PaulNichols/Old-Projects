Namespace Application.Bird.Registration.Search
    <Serializable()> _
    Public Class PermitSearchResult
        Inherits BaseSearchResult

        Public Sub New()
        End Sub

        Public Property PermitNumber() As String
            Get
                Return mPermitNumber
            End Get
            Set(ByVal Value As String)
                mPermitNumber = Value
            End Set
        End Property
        Private mPermitNumber As String

        Public Property IssueDate() As String
            Get
                Return mIssueDate
            End Get
            Set(ByVal Value As String)
                mIssueDate = Value
            End Set
        End Property
        Private mIssueDate As String

        Public Property PermitInfoId() As Int32
            Get
                Return mPermitInfoId
            End Get
            Set(Byval Value As Int32)
                mPermitInfoId = Value
            End Set
        End Property
        Private mPermitInfoId As Int32
        
    End Class
End Namespace
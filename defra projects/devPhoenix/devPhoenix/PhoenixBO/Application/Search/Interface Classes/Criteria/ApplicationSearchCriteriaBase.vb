Namespace Application.Search
    <Serializable()> _
    Public Class ApplicationSearchCriteriaBase
        Public Enum SortDirection
            Asc
            Desc
        End Enum

        Public Sub New()
        End Sub

        Public Property OrderColumn() As Int32
            Get
                Return mOrderColumn
            End Get
            Set(ByVal Value As int32)
                mOrderColumn = Value
            End Set
        End Property
        Private mOrderColumn As Int32

        Public Property OrderDirection() As SortDirection
            Get
                Return mOrderDirection
            End Get
            Set(ByVal Value As SortDirection)
                mOrderDirection = Value
            End Set
        End Property
        Private mOrderDirection As SortDirection

        Friend ReadOnly Property Sort() As String
            Get
                If mOrderColumn = 0 Then
                    Return Nothing
                Else
                    If mOrderDirection = SortDirection.Asc Then
                        Return "ASC"
                    Else
                        Return "DESC"
                    End If
                End If
            End Get
        End Property
    End Class
End Namespace
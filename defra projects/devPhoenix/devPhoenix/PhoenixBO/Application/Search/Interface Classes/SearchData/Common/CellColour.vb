Namespace Application.Search.Data
    <Serializable()> _
        Public Class CellColour
        Public Sub New()
        End Sub

        Friend Sub New(ByVal cellCol As System.Drawing.Color, ByVal propertyName As String)
            mCellCol = cellCol
            mPropertyName = propertyName
        End Sub

        Public Property CellCol() As System.Drawing.Color
            Get
                Return mCellCol
            End Get
            Set(ByVal Value As System.Drawing.Color)
                mCellCol = Value
            End Set
        End Property
        Private mCellCol As System.Drawing.Color

        Public Property PropertyName() As String
            Get
                Return mPropertyName
            End Get
            Set(ByVal Value As String)
                mPropertyName = Value
            End Set
        End Property
        Private mPropertyName As String

    End Class
End Namespace
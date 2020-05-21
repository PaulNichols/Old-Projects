Namespace Application.Search.Data
    <Serializable()> _
    Public Class BaseSearchData
        Implements IApplicationId

        Public Sub New()
        End Sub

        Public Property RowColour() As Drawing.Color
            Get
                Return mRowColour
            End Get
            Set(ByVal Value As Drawing.Color)
                mRowColour = Value
            End Set
        End Property
        Private mRowColour As Drawing.Color

        Public Property CellColour() As CellColour()
            Get
                Return mCellColour
            End Get
            Set(ByVal Value As CellColour())
                mCellColour = Value
            End Set
        End Property
        Private mCellColour As CellColour()

        Public Property LinkId() As Int32
            Get
                Return mLinkId
            End Get
            Set(ByVal Value As Int32)
                mLinkId = Value
            End Set
        End Property
        Private mLinkId As Int32

        Public Property ApplicationId() As Int32 Implements IApplicationId.ApplicationId
            Get
                Return mApplicationId
            End Get
            Set(ByVal Value As Int32)
                mApplicationId = Value
            End Set
        End Property
        Private mApplicationId As Int32

        'Public Property Rows() As EntityRow()
        '    Get
        '        Return mRows
        '    End Get
        '    Set(ByVal Value As EntityRow())
        '        mRows = Value
        '    End Set
        'End Property
        'Private mRows() As EntityRow

        'Friend Sub AddRow(ByVal row As EntityRow)
        '    AddNewRow(row)
        'End Sub

        'Private Sub AddNewRow(ByVal newRow As EntityRow)
        '    ReDim Preserve mRows(mRows.Length + 1)
        '    'mRows = new EntityRow(0 CType(Resize(mRows), EntityRow())
        '    mRows(mRows.Length - 1) = newRow
        'End Sub

        'Friend Shared Function Resize(ByVal array() As Object) As Array
        '    Return Resize(array, array.Length + 1)
        'End Function

        'Friend Shared Function Resize(ByVal array() As Object, ByVal newSize As Int32) As Array
        '    Dim type As System.Type = array.GetType()
        '    Dim newArray As array = System.Array.CreateInstance(type.GetElementType(), newSize)
        '    If array.Length > 0 Then
        '        System.Array.Copy(array, 0, newArray, 0, Math.Min(array.Length, newSize))
        '    End If
        '    Return newArray
        'End Function

    End Class
End Namespace
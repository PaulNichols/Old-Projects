Public Interface IConcurrentEntity

    Function GetTimestamp() As System.Data.SqlTypes.SqlBinary
    Sub RefreshTimestamp()
    Function HasChanged() As Boolean

End Interface

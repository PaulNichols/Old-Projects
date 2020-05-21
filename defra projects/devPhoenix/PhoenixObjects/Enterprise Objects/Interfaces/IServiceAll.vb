Public Interface IServiceAll
    Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet
End Interface

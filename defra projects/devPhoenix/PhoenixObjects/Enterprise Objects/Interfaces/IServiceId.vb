Public Interface IServiceId
    Function GetByIdInternal(ByVal id As Int32) As Entity
    Function GetByIdInternal(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As Entity
End Interface

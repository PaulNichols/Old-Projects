Public Interface IService
    'Function GetByIdInternal(ByVal id As Int32) As Entity
    'Function GetByIdInternal(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As Entity
    Function GetLastDBError() As Service.SQLError
End Interface

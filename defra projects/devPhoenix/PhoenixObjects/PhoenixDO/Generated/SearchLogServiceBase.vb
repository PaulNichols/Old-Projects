'------------------------------------------------------------------------------
' <autogenerated>
'     This code was generated by a tool.
'     Runtime Version: 1.1.4322.2032
'
'     Changes to this file may cause incorrect behavior and will be lost if 
'     the code is regenerated.
' </autogenerated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On


Namespace DataObjects.Base
    
    'Service base implementation for table 'SearchLog'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class SearchLogServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.SearchLogSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.SearchLogSet
            Return CType(MyBase.GetAll("eosp_SelectSearchLog", GetType(EntitySet.SearchLogSet), includeHyphen, includeInactive, orderBy),EntitySet.SearchLogSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.SearchLogSet
            Return Me.GetAll(includeHyphen, includeInactive, SearchLogServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, SearchLogServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal searchLogId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectSearchLog", "SearchLogId", searchLogId, GetType(EntitySet.SearchLogSet), tran),Entity.SearchLog)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal searchLogId As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(searchLogId, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal searchLogId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.SearchLog
            Return CType(MyBase.GetById("eosp_SelectSearchLog", "SearchLogId", searchLogId, GetType(EntitySet.SearchLogSet), tran),Entity.SearchLog)
        End Function
        
        Public Overloads Function GetById(ByVal searchLogId As Integer) As Entity.SearchLog
            Return Me.GetById(searchLogId, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal searchLogId As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(searchLogId, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal searchLogId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeleteSearchLog", "SearchLogId", searchLogId, checkSum, transaction)
        End Function
        
        Public Overloads Function Insert(ByVal userId As Decimal, ByVal searchDate As Object, ByVal criteria As String, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.SearchLog
            Return Me.GetById(Sprocs.eosp_CreateSearchLog(userId, searchDate, criteria, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal userId As Decimal, ByVal searchDate As Object, ByVal criteria As String) As Entity.SearchLog
            Return Me.Insert(userId, searchDate, criteria, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal searchLog As Entity.SearchLog) As Entity.SearchLog
            Return Me.Insert(searchLog(1), searchLog(2), searchLog(3))
        End Function
        
        Public Overloads Function Insert(ByVal searchLog As Entity.SearchLog, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.SearchLog
            Return Me.Insert(searchLog(1), searchLog(2), searchLog(3), transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal userId As Decimal, ByVal searchDate As Object, ByVal criteria As String, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.SearchLog
            Return Sprocs.eosp_UpdateSearchLog(id, userId, searchDate, criteria, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal userId As Decimal, ByVal searchDate As Object, ByVal criteria As String) As Entity.SearchLog
            Return Me.Update(id, userId, searchDate, criteria, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal userId As Decimal, ByVal searchDate As Object, ByVal criteria As String, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.SearchLog
            Return Me.Update(id, userId, searchDate, criteria, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal userId As Decimal, ByVal searchDate As Object, ByVal criteria As String, ByVal checkSum As Integer) As Entity.SearchLog
            Return Me.Update(id, userId, searchDate, criteria, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal searchLog As Entity.SearchLog) As Entity.SearchLog
            Return Me.Update(searchLog.id, searchLog(1), searchLog(2), searchLog(3))
        End Function
        
        Public Overloads Function Update(ByVal searchLog As Entity.SearchLog, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.SearchLog
            Return Me.Update(searchLog.id, searchLog(1), searchLog(2), searchLog(3), transaction)
        End Function
        
        Public Overloads Function Update(ByVal searchLog As Entity.SearchLog, ByVal checkSum As Integer) As Entity.SearchLog
            Return Me.Update(searchLog.id, searchLog(1), searchLog(2), searchLog(3), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal searchLog As Entity.SearchLog, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.SearchLog
            Return Me.Update(searchLog.id, searchLog(1), searchLog(2), searchLog(3), checkSum, transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
        End Enum
    End Class
End Namespace

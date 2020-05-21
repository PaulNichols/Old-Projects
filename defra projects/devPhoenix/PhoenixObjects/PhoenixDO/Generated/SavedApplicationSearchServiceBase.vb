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
    
    'Service base implementation for table 'SavedApplicationSearch'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class SavedApplicationSearchServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.SavedApplicationSearchSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.SavedApplicationSearchSet
            Return CType(MyBase.GetAll("eosp_SelectSavedApplicationSearch", GetType(EntitySet.SavedApplicationSearchSet), includeHyphen, includeInactive, orderBy),EntitySet.SavedApplicationSearchSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.SavedApplicationSearchSet
            Return Me.GetAll(includeHyphen, includeInactive, SavedApplicationSearchServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, SavedApplicationSearchServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal savedSearchId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectSavedApplicationSearch", "SavedSearchId", savedSearchId, GetType(EntitySet.SavedApplicationSearchSet), tran),Entity.SavedApplicationSearch)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal savedSearchId As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(savedSearchId, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal savedSearchId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.SavedApplicationSearch
            Return CType(MyBase.GetById("eosp_SelectSavedApplicationSearch", "SavedSearchId", savedSearchId, GetType(EntitySet.SavedApplicationSearchSet), tran),Entity.SavedApplicationSearch)
        End Function
        
        Public Overloads Function GetById(ByVal savedSearchId As Integer) As Entity.SavedApplicationSearch
            Return Me.GetById(savedSearchId, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal savedSearchId As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(savedSearchId, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal savedSearchId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeleteSavedApplicationSearch", "SavedSearchId", savedSearchId, checkSum, transaction)
        End Function
        
        Public Overloads Function Insert(ByVal criteria As String, ByVal modified As Date, ByVal userId As Decimal, ByVal description As String, ByVal workList As Boolean, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.SavedApplicationSearch
            Return Me.GetById(Sprocs.eosp_CreateSavedApplicationSearch(criteria, modified, userId, description, workList, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal criteria As String, ByVal modified As Date, ByVal userId As Decimal, ByVal description As String, ByVal workList As Boolean) As Entity.SavedApplicationSearch
            Return Me.Insert(criteria, modified, userId, description, workList, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal savedApplicationSearch As Entity.SavedApplicationSearch) As Entity.SavedApplicationSearch
            Return Me.Insert(savedApplicationSearch(1), savedApplicationSearch(2), savedApplicationSearch(3), savedApplicationSearch(4), savedApplicationSearch(5))
        End Function
        
        Public Overloads Function Insert(ByVal savedApplicationSearch As Entity.SavedApplicationSearch, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.SavedApplicationSearch
            Return Me.Insert(savedApplicationSearch(1), savedApplicationSearch(2), savedApplicationSearch(3), savedApplicationSearch(4), savedApplicationSearch(5), transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal criteria As String, ByVal modified As Date, ByVal userId As Decimal, ByVal description As String, ByVal workList As Boolean, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.SavedApplicationSearch
            Return Sprocs.eosp_UpdateSavedApplicationSearch(id, criteria, modified, userId, description, workList, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal criteria As String, ByVal modified As Date, ByVal userId As Decimal, ByVal description As String, ByVal workList As Boolean) As Entity.SavedApplicationSearch
            Return Me.Update(id, criteria, modified, userId, description, workList, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal criteria As String, ByVal modified As Date, ByVal userId As Decimal, ByVal description As String, ByVal workList As Boolean, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.SavedApplicationSearch
            Return Me.Update(id, criteria, modified, userId, description, workList, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal criteria As String, ByVal modified As Date, ByVal userId As Decimal, ByVal description As String, ByVal workList As Boolean, ByVal checkSum As Integer) As Entity.SavedApplicationSearch
            Return Me.Update(id, criteria, modified, userId, description, workList, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal savedApplicationSearch As Entity.SavedApplicationSearch) As Entity.SavedApplicationSearch
            Return Me.Update(savedApplicationSearch.id, savedApplicationSearch(1), savedApplicationSearch(2), savedApplicationSearch(3), savedApplicationSearch(4), savedApplicationSearch(5))
        End Function
        
        Public Overloads Function Update(ByVal savedApplicationSearch As Entity.SavedApplicationSearch, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.SavedApplicationSearch
            Return Me.Update(savedApplicationSearch.id, savedApplicationSearch(1), savedApplicationSearch(2), savedApplicationSearch(3), savedApplicationSearch(4), savedApplicationSearch(5), transaction)
        End Function
        
        Public Overloads Function Update(ByVal savedApplicationSearch As Entity.SavedApplicationSearch, ByVal checkSum As Integer) As Entity.SavedApplicationSearch
            Return Me.Update(savedApplicationSearch.id, savedApplicationSearch(1), savedApplicationSearch(2), savedApplicationSearch(3), savedApplicationSearch(4), savedApplicationSearch(5), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal savedApplicationSearch As Entity.SavedApplicationSearch, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.SavedApplicationSearch
            Return Me.Update(savedApplicationSearch.id, savedApplicationSearch(1), savedApplicationSearch(2), savedApplicationSearch(3), savedApplicationSearch(4), savedApplicationSearch(5), checkSum, transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
        End Enum
    End Class
End Namespace
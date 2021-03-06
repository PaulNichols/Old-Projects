'------------------------------------------------------------------------------
' <autogenerated>
'     This code was generated by a tool.
'     Runtime Version: 1.1.4322.573
'
'     Changes to this file may cause incorrect behavior and will be lost if 
'     the code is regenerated.
' </autogenerated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On


Namespace DataObjects.Base
    
    'Service base implementation for table 'TaxonomyDataLoadRequestHistory'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class TaxonomyDataLoadRequestHistoryServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.TaxonomyDataLoadRequestHistorySet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.TaxonomyDataLoadRequestHistorySet
            Return CType(MyBase.GetAll("eosp_SelectTaxonomyDataLoadRequestHistory", GetType(EntitySet.TaxonomyDataLoadRequestHistorySet), includeHyphen, includeInactive, orderBy),EntitySet.TaxonomyDataLoadRequestHistorySet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.TaxonomyDataLoadRequestHistorySet
            Return Me.GetAll(includeHyphen, includeInactive, TaxonomyDataLoadRequestHistoryServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, TaxonomyDataLoadRequestHistoryServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal taxonomyDataLoadRequestHistoryID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectTaxonomyDataLoadRequestHistory", "TaxonomyDataLoadRequestHistoryID", taxonomyDataLoadRequestHistoryID, GetType(EntitySet.TaxonomyDataLoadRequestHistorySet), tran),Entity.TaxonomyDataLoadRequestHistory)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal taxonomyDataLoadRequestHistoryID As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(taxonomyDataLoadRequestHistoryID, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal taxonomyDataLoadRequestHistoryID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyDataLoadRequestHistory
            Return CType(MyBase.GetById("eosp_SelectTaxonomyDataLoadRequestHistory", "TaxonomyDataLoadRequestHistoryID", taxonomyDataLoadRequestHistoryID, GetType(EntitySet.TaxonomyDataLoadRequestHistorySet), tran),Entity.TaxonomyDataLoadRequestHistory)
        End Function
        
        Public Overloads Function GetById(ByVal taxonomyDataLoadRequestHistoryID As Integer) As Entity.TaxonomyDataLoadRequestHistory
            Return Me.GetById(taxonomyDataLoadRequestHistoryID, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal taxonomyDataLoadRequestHistoryID As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(taxonomyDataLoadRequestHistoryID, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal taxonomyDataLoadRequestHistoryID As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeleteTaxonomyDataLoadRequestHistory", "TaxonomyDataLoadRequestHistoryID", taxonomyDataLoadRequestHistoryID, checkSum, transaction)
        End Function
        
        'GetForTaxonomyDataLoadRequest - links to the TaxonomyDataLoadRequest table...
        Public Overloads Function GetForTaxonomyDataLoadRequest(ByVal TaxonomyDataLoadRequestID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.TaxonomyDataLoadRequestHistorySet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from TaxonomyDataLoadRequestHistory wher"& _ 
"e TaxonomyDataloadRequestID=" + TaxonomyDataLoadRequestID.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.TaxonomyDataLoadRequestHistorySet), tran),EntitySet.TaxonomyDataLoadRequestHistorySet)
        End Function
        
        'GetForTaxonomyDataLoadRequest - links to the TaxonomyDataLoadRequest table...
        Public Overloads Function GetForTaxonomyDataLoadRequest(ByVal TaxonomyDataLoadRequestID As Integer) As EntitySet.TaxonomyDataLoadRequestHistorySet
            Return Me.GetForTaxonomyDataLoadRequest(TaxonomyDataLoadRequestID, Nothing)
        End Function
        
        'GetForTaxonomyDataLoadData - links to the TaxonomyDataLoadData table...
        Public Overloads Function GetForTaxonomyDataLoadData(ByVal TaxonomyDataLoadDataID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.TaxonomyDataLoadRequestHistorySet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from TaxonomyDataLoadRequestHistory wher"& _ 
"e TaxonomyDataLoadDataID=" + TaxonomyDataLoadDataID.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.TaxonomyDataLoadRequestHistorySet), tran),EntitySet.TaxonomyDataLoadRequestHistorySet)
        End Function
        
        'GetForTaxonomyDataLoadData - links to the TaxonomyDataLoadData table...
        Public Overloads Function GetForTaxonomyDataLoadData(ByVal TaxonomyDataLoadDataID As Integer) As EntitySet.TaxonomyDataLoadRequestHistorySet
            Return Me.GetForTaxonomyDataLoadData(TaxonomyDataLoadDataID, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal taxonomyDataloadRequestID As Integer, ByVal stage As Integer, ByVal status As Integer, ByVal [date] As Date, ByVal message As Object, ByVal diagnostics As Object, ByVal taxonomyDataLoadDataID As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyDataLoadRequestHistory
            Return Me.GetById(Sprocs.eosp_CreateTaxonomyDataLoadRequestHistory(taxonomyDataloadRequestID, stage, status, [date], message, diagnostics, taxonomyDataLoadDataID, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal taxonomyDataloadRequestID As Integer, ByVal stage As Integer, ByVal status As Integer, ByVal [date] As Date, ByVal message As Object, ByVal diagnostics As Object, ByVal taxonomyDataLoadDataID As Object) As Entity.TaxonomyDataLoadRequestHistory
            Return Me.Insert(taxonomyDataloadRequestID, stage, status, [date], message, diagnostics, taxonomyDataLoadDataID, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal taxonomyDataLoadRequestHistory As Entity.TaxonomyDataLoadRequestHistory) As Entity.TaxonomyDataLoadRequestHistory
            Return Me.Insert(taxonomyDataLoadRequestHistory(1), taxonomyDataLoadRequestHistory(2), taxonomyDataLoadRequestHistory(3), taxonomyDataLoadRequestHistory(4), taxonomyDataLoadRequestHistory(5), taxonomyDataLoadRequestHistory(6), taxonomyDataLoadRequestHistory(7))
        End Function
        
        Public Overloads Function Insert(ByVal taxonomyDataLoadRequestHistory As Entity.TaxonomyDataLoadRequestHistory, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyDataLoadRequestHistory
            Return Me.Insert(taxonomyDataLoadRequestHistory(1), taxonomyDataLoadRequestHistory(2), taxonomyDataLoadRequestHistory(3), taxonomyDataLoadRequestHistory(4), taxonomyDataLoadRequestHistory(5), taxonomyDataLoadRequestHistory(6), taxonomyDataLoadRequestHistory(7), transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal taxonomyDataloadRequestID As Integer, ByVal stage As Integer, ByVal status As Integer, ByVal [date] As Date, ByVal message As Object, ByVal diagnostics As Object, ByVal taxonomyDataLoadDataID As Object, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyDataLoadRequestHistory
            'Return Sprocs.eosp_UpdateTaxonomyDataLoadRequestHistory(id, taxonomyDataloadRequestID, stage, status, [date], message, diagnostics, taxonomyDataLoadDataID, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal taxonomyDataloadRequestID As Integer, ByVal stage As Integer, ByVal status As Integer, ByVal [date] As Date, ByVal message As Object, ByVal diagnostics As Object, ByVal taxonomyDataLoadDataID As Object) As Entity.TaxonomyDataLoadRequestHistory
            Return Me.Update(id, taxonomyDataloadRequestID, stage, status, [date], message, diagnostics, taxonomyDataLoadDataID, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal taxonomyDataloadRequestID As Integer, ByVal stage As Integer, ByVal status As Integer, ByVal [date] As Date, ByVal message As Object, ByVal diagnostics As Object, ByVal taxonomyDataLoadDataID As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyDataLoadRequestHistory
            Return Me.Update(id, taxonomyDataloadRequestID, stage, status, [date], message, diagnostics, taxonomyDataLoadDataID, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal taxonomyDataloadRequestID As Integer, ByVal stage As Integer, ByVal status As Integer, ByVal [date] As Date, ByVal message As Object, ByVal diagnostics As Object, ByVal taxonomyDataLoadDataID As Object, ByVal checkSum As Integer) As Entity.TaxonomyDataLoadRequestHistory
            Return Me.Update(id, taxonomyDataloadRequestID, stage, status, [date], message, diagnostics, taxonomyDataLoadDataID, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal taxonomyDataLoadRequestHistory As Entity.TaxonomyDataLoadRequestHistory) As Entity.TaxonomyDataLoadRequestHistory
            Return Me.Update(taxonomyDataLoadRequestHistory.id, taxonomyDataLoadRequestHistory(1), taxonomyDataLoadRequestHistory(2), taxonomyDataLoadRequestHistory(3), taxonomyDataLoadRequestHistory(4), taxonomyDataLoadRequestHistory(5), taxonomyDataLoadRequestHistory(6), taxonomyDataLoadRequestHistory(7))
        End Function
        
        Public Overloads Function Update(ByVal taxonomyDataLoadRequestHistory As Entity.TaxonomyDataLoadRequestHistory, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyDataLoadRequestHistory
            Return Me.Update(taxonomyDataLoadRequestHistory.id, taxonomyDataLoadRequestHistory(1), taxonomyDataLoadRequestHistory(2), taxonomyDataLoadRequestHistory(3), taxonomyDataLoadRequestHistory(4), taxonomyDataLoadRequestHistory(5), taxonomyDataLoadRequestHistory(6), taxonomyDataLoadRequestHistory(7), transaction)
        End Function
        
        Public Overloads Function Update(ByVal taxonomyDataLoadRequestHistory As Entity.TaxonomyDataLoadRequestHistory, ByVal checkSum As Integer) As Entity.TaxonomyDataLoadRequestHistory
            Return Me.Update(taxonomyDataLoadRequestHistory.id, taxonomyDataLoadRequestHistory(1), taxonomyDataLoadRequestHistory(2), taxonomyDataLoadRequestHistory(3), taxonomyDataLoadRequestHistory(4), taxonomyDataLoadRequestHistory(5), taxonomyDataLoadRequestHistory(6), taxonomyDataLoadRequestHistory(7), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal taxonomyDataLoadRequestHistory As Entity.TaxonomyDataLoadRequestHistory, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyDataLoadRequestHistory
            Return Me.Update(taxonomyDataLoadRequestHistory.id, taxonomyDataLoadRequestHistory(1), taxonomyDataLoadRequestHistory(2), taxonomyDataLoadRequestHistory(3), taxonomyDataLoadRequestHistory(4), taxonomyDataLoadRequestHistory(5), taxonomyDataLoadRequestHistory(6), taxonomyDataLoadRequestHistory(7), checkSum, transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
        End Enum
    End Class
End Namespace

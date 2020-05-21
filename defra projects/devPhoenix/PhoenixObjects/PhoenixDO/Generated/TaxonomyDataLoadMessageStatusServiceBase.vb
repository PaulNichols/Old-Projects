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
    
    'Service base implementation for table 'TaxonomyDataLoadMessageStatus'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class TaxonomyDataLoadMessageStatusServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.TaxonomyDataLoadMessageStatusSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.TaxonomyDataLoadMessageStatusSet
            Return CType(MyBase.GetAll("eosp_SelectTaxonomyDataLoadMessageStatus", GetType(EntitySet.TaxonomyDataLoadMessageStatusSet), includeHyphen, includeInactive, orderBy),EntitySet.TaxonomyDataLoadMessageStatusSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.TaxonomyDataLoadMessageStatusSet
            Return Me.GetAll(includeHyphen, includeInactive, TaxonomyDataLoadMessageStatusServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, TaxonomyDataLoadMessageStatusServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal orderBy As OrderBy) As EntitySet.TaxonomyDataLoadMessageStatusSet
            Return Me.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal iD As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectTaxonomyDataLoadMessageStatus", "ID", iD, GetType(EntitySet.TaxonomyDataLoadMessageStatusSet), tran),Entity.TaxonomyDataLoadMessageStatus)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal iD As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(iD, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal iD As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyDataLoadMessageStatus
            Return CType(MyBase.GetById("eosp_SelectTaxonomyDataLoadMessageStatus", "ID", iD, GetType(EntitySet.TaxonomyDataLoadMessageStatusSet), tran),Entity.TaxonomyDataLoadMessageStatus)
        End Function
        
        Public Overloads Function GetById(ByVal iD As Integer) As Entity.TaxonomyDataLoadMessageStatus
            Return Me.GetById(iD, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal iD As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(iD, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal iD As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeleteTaxonomyDataLoadMessageStatus", "ID", iD, checkSum, transaction)
        End Function
        
        'GetForTaxonomyDataLoadMessage - links to the TaxonomyDataLoadMessage table...
        Public Overloads Function GetForTaxonomyDataLoadMessage(ByVal TaxonomyDataLoadMessageID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.TaxonomyDataLoadMessageStatusSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from TaxonomyDataLoadMessageStatus where"& _ 
" TaxonomyDataLoadMessageID=" + TaxonomyDataLoadMessageID.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.TaxonomyDataLoadMessageStatusSet), tran),EntitySet.TaxonomyDataLoadMessageStatusSet)
        End Function
        
        'GetForTaxonomyDataLoadMessage - links to the TaxonomyDataLoadMessage table...
        Public Overloads Function GetForTaxonomyDataLoadMessage(ByVal TaxonomyDataLoadMessageID As Integer) As EntitySet.TaxonomyDataLoadMessageStatusSet
            Return Me.GetForTaxonomyDataLoadMessage(TaxonomyDataLoadMessageID, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal taxonomyDataLoadMessageID As Integer, ByVal statusDateTime As Date, ByVal status As Integer, ByVal diagnostics As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyDataLoadMessageStatus
            Return Me.GetById(Sprocs.eosp_CreateTaxonomyDataLoadMessageStatus(taxonomyDataLoadMessageID, statusDateTime, status, diagnostics, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal taxonomyDataLoadMessageID As Integer, ByVal statusDateTime As Date, ByVal status As Integer, ByVal diagnostics As Object) As Entity.TaxonomyDataLoadMessageStatus
            Return Me.Insert(taxonomyDataLoadMessageID, statusDateTime, status, diagnostics, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal taxonomyDataLoadMessageStatus As Entity.TaxonomyDataLoadMessageStatus) As Entity.TaxonomyDataLoadMessageStatus
            Return Me.Insert(taxonomyDataLoadMessageStatus(1), taxonomyDataLoadMessageStatus(2), taxonomyDataLoadMessageStatus(3), taxonomyDataLoadMessageStatus(4))
        End Function
        
        Public Overloads Function Insert(ByVal taxonomyDataLoadMessageStatus As Entity.TaxonomyDataLoadMessageStatus, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyDataLoadMessageStatus
            Return Me.Insert(taxonomyDataLoadMessageStatus(1), taxonomyDataLoadMessageStatus(2), taxonomyDataLoadMessageStatus(3), taxonomyDataLoadMessageStatus(4), transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal taxonomyDataLoadMessageID As Integer, ByVal statusDateTime As Date, ByVal status As Integer, ByVal diagnostics As Object, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyDataLoadMessageStatus
            Return Sprocs.eosp_UpdateTaxonomyDataLoadMessageStatus(id, taxonomyDataLoadMessageID, statusDateTime, status, diagnostics, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal taxonomyDataLoadMessageID As Integer, ByVal statusDateTime As Date, ByVal status As Integer, ByVal diagnostics As Object) As Entity.TaxonomyDataLoadMessageStatus
            Return Me.Update(id, taxonomyDataLoadMessageID, statusDateTime, status, diagnostics, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal taxonomyDataLoadMessageID As Integer, ByVal statusDateTime As Date, ByVal status As Integer, ByVal diagnostics As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyDataLoadMessageStatus
            Return Me.Update(id, taxonomyDataLoadMessageID, statusDateTime, status, diagnostics, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal taxonomyDataLoadMessageID As Integer, ByVal statusDateTime As Date, ByVal status As Integer, ByVal diagnostics As Object, ByVal checkSum As Integer) As Entity.TaxonomyDataLoadMessageStatus
            Return Me.Update(id, taxonomyDataLoadMessageID, statusDateTime, status, diagnostics, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal taxonomyDataLoadMessageStatus As Entity.TaxonomyDataLoadMessageStatus) As Entity.TaxonomyDataLoadMessageStatus
            Return Me.Update(taxonomyDataLoadMessageStatus.id, taxonomyDataLoadMessageStatus(1), taxonomyDataLoadMessageStatus(2), taxonomyDataLoadMessageStatus(3), taxonomyDataLoadMessageStatus(4))
        End Function
        
        Public Overloads Function Update(ByVal taxonomyDataLoadMessageStatus As Entity.TaxonomyDataLoadMessageStatus, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyDataLoadMessageStatus
            Return Me.Update(taxonomyDataLoadMessageStatus.id, taxonomyDataLoadMessageStatus(1), taxonomyDataLoadMessageStatus(2), taxonomyDataLoadMessageStatus(3), taxonomyDataLoadMessageStatus(4), transaction)
        End Function
        
        Public Overloads Function Update(ByVal taxonomyDataLoadMessageStatus As Entity.TaxonomyDataLoadMessageStatus, ByVal checkSum As Integer) As Entity.TaxonomyDataLoadMessageStatus
            Return Me.Update(taxonomyDataLoadMessageStatus.id, taxonomyDataLoadMessageStatus(1), taxonomyDataLoadMessageStatus(2), taxonomyDataLoadMessageStatus(3), taxonomyDataLoadMessageStatus(4), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal taxonomyDataLoadMessageStatus As Entity.TaxonomyDataLoadMessageStatus, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyDataLoadMessageStatus
            Return Me.Update(taxonomyDataLoadMessageStatus.id, taxonomyDataLoadMessageStatus(1), taxonomyDataLoadMessageStatus(2), taxonomyDataLoadMessageStatus(3), taxonomyDataLoadMessageStatus(4), checkSum, transaction)
        End Function
        
        Public Overloads Function GetByIndex_IX_TaxonomyDataLoadMessageStatus(ByVal taxonomyDataLoadMessageID As Integer, ByVal statusDateTime As Date) As EntitySet.TaxonomyDataLoadMessageStatusSet
            Return Sprocs.eosp_SelectTaxonomyDataLoadMessageStatus(iD:=Nothing, Index_TaxonomyDataLoadMessageID:=[taxonomyDataLoadMessageID], Index_StatusDateTime:=[statusDateTime], transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_IX_TaxonomyDataLoadMessageStatus(ByVal taxonomyDataLoadMessageID As Integer, ByVal statusDateTime As Date, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.TaxonomyDataLoadMessageStatusSet
            Return Sprocs.eosp_SelectTaxonomyDataLoadMessageStatus(iD:=Nothing, Index_TaxonomyDataLoadMessageID:=[taxonomyDataLoadMessageID], Index_StatusDateTime:=[statusDateTime], sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
            
            IX_TaxonomyDataLoadMessageStatus
            
            
        End Enum
    End Class
End Namespace

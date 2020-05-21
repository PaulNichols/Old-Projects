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
    
    'Service base implementation for table 'DuplicateReason'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class DuplicateReasonServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return true
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.DuplicateReasonSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.DuplicateReasonSet
            Return CType(MyBase.GetAll("eosp_SelectDuplicateReason", GetType(EntitySet.DuplicateReasonSet), includeHyphen, includeInactive, orderBy),EntitySet.DuplicateReasonSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.DuplicateReasonSet
            Return Me.GetAll(includeHyphen, includeInactive, DuplicateReasonServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, DuplicateReasonServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal orderBy As OrderBy) As EntitySet.DuplicateReasonSet
            Return Me.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal duplicateReasonId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectDuplicateReason", "DuplicateReasonId", duplicateReasonId, GetType(EntitySet.DuplicateReasonSet), tran),Entity.DuplicateReason)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal duplicateReasonId As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(duplicateReasonId, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal duplicateReasonId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.DuplicateReason
            Return CType(MyBase.GetById("eosp_SelectDuplicateReason", "DuplicateReasonId", duplicateReasonId, GetType(EntitySet.DuplicateReasonSet), tran),Entity.DuplicateReason)
        End Function
        
        Public Overloads Function GetById(ByVal duplicateReasonId As Integer) As Entity.DuplicateReason
            Return Me.GetById(duplicateReasonId, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal duplicateReasonId As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(duplicateReasonId, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal duplicateReasonId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeleteDuplicateReason", "DuplicateReasonId", duplicateReasonId, checkSum, transaction)
        End Function
        
        Public Overloads Function Insert(ByVal description As String, ByVal active As Boolean, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.DuplicateReason
            Return Me.GetById(Sprocs.eosp_CreateDuplicateReason(description, active, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal description As String, ByVal active As Boolean) As Entity.DuplicateReason
            Return Me.Insert(description, active, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal duplicateReason As Entity.DuplicateReason) As Entity.DuplicateReason
            Return Me.Insert(duplicateReason(1), duplicateReason(2))
        End Function
        
        Public Overloads Function Insert(ByVal duplicateReason As Entity.DuplicateReason, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.DuplicateReason
            Return Me.Insert(duplicateReason(1), duplicateReason(2), transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal description As String, ByVal active As Boolean, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.DuplicateReason
            Return Sprocs.eosp_UpdateDuplicateReason(id, description, active, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal description As String, ByVal active As Boolean) As Entity.DuplicateReason
            Return Me.Update(id, description, active, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal description As String, ByVal active As Boolean, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.DuplicateReason
            Return Me.Update(id, description, active, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal description As String, ByVal active As Boolean, ByVal checkSum As Integer) As Entity.DuplicateReason
            Return Me.Update(id, description, active, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal duplicateReason As Entity.DuplicateReason) As Entity.DuplicateReason
            Return Me.Update(duplicateReason.id, duplicateReason(1), duplicateReason(2))
        End Function
        
        Public Overloads Function Update(ByVal duplicateReason As Entity.DuplicateReason, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.DuplicateReason
            Return Me.Update(duplicateReason.id, duplicateReason(1), duplicateReason(2), transaction)
        End Function
        
        Public Overloads Function Update(ByVal duplicateReason As Entity.DuplicateReason, ByVal checkSum As Integer) As Entity.DuplicateReason
            Return Me.Update(duplicateReason.id, duplicateReason(1), duplicateReason(2), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal duplicateReason As Entity.DuplicateReason, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.DuplicateReason
            Return Me.Update(duplicateReason.id, duplicateReason(1), duplicateReason(2), checkSum, transaction)
        End Function
        
        Public Overloads Function GetByIndex_IX_DuplicateReason_Description(ByVal description As String, ByVal includeInactive As Boolean) As EntitySet.DuplicateReasonSet
            Return Sprocs.eosp_SelectDuplicateReason(duplicateReasonId:=Nothing, Index_Description:=[description], includeInactive:=includeInactive, transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_IX_DuplicateReason_Description(ByVal description As String, ByVal includeInactive As Boolean, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.DuplicateReasonSet
            Return Sprocs.eosp_SelectDuplicateReason(duplicateReasonId:=Nothing, Index_Description:=[description], includeInactive:=includeInactive, sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
            
            IX_DuplicateReason_Description
            
            
        End Enum
    End Class
End Namespace

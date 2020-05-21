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
    
    'Service base implementation for table 'ProgressStatusReIssued'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class ProgressStatusReIssuedServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.ProgressStatusReIssuedSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.ProgressStatusReIssuedSet
            Return CType(MyBase.GetAll("eosp_SelectProgressStatusReIssued", GetType(EntitySet.ProgressStatusReIssuedSet), includeHyphen, includeInactive, orderBy),EntitySet.ProgressStatusReIssuedSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.ProgressStatusReIssuedSet
            Return Me.GetAll(includeHyphen, includeInactive, ProgressStatusReIssuedServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, ProgressStatusReIssuedServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal orderBy As OrderBy) As EntitySet.ProgressStatusReIssuedSet
            Return Me.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal progressStatusReIssuedId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectProgressStatusReIssued", "ProgressStatusReIssuedId", progressStatusReIssuedId, GetType(EntitySet.ProgressStatusReIssuedSet), tran),Entity.ProgressStatusReIssued)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal progressStatusReIssuedId As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(progressStatusReIssuedId, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal progressStatusReIssuedId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.ProgressStatusReIssued
            Return CType(MyBase.GetById("eosp_SelectProgressStatusReIssued", "ProgressStatusReIssuedId", progressStatusReIssuedId, GetType(EntitySet.ProgressStatusReIssuedSet), tran),Entity.ProgressStatusReIssued)
        End Function
        
        Public Overloads Function GetById(ByVal progressStatusReIssuedId As Integer) As Entity.ProgressStatusReIssued
            Return Me.GetById(progressStatusReIssuedId, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal progressStatusReIssuedId As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(progressStatusReIssuedId, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal progressStatusReIssuedId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeleteProgressStatusReIssued", "ProgressStatusReIssuedId", progressStatusReIssuedId, checkSum, transaction)
        End Function
        
        Public Overloads Function Insert(ByVal description As String, ByVal code As String, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ProgressStatusReIssued
            Return Me.GetById(Sprocs.eosp_CreateProgressStatusReIssued(description, code, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal description As String, ByVal code As String) As Entity.ProgressStatusReIssued
            Return Me.Insert(description, code, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal progressStatusReIssued As Entity.ProgressStatusReIssued) As Entity.ProgressStatusReIssued
            Return Me.Insert(progressStatusReIssued(1), progressStatusReIssued(2))
        End Function
        
        Public Overloads Function Insert(ByVal progressStatusReIssued As Entity.ProgressStatusReIssued, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ProgressStatusReIssued
            Return Me.Insert(progressStatusReIssued(1), progressStatusReIssued(2), transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal description As String, ByVal code As String, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ProgressStatusReIssued
            Return Sprocs.eosp_UpdateProgressStatusReIssued(id, description, code, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal description As String, ByVal code As String) As Entity.ProgressStatusReIssued
            Return Me.Update(id, description, code, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal description As String, ByVal code As String, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ProgressStatusReIssued
            Return Me.Update(id, description, code, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal description As String, ByVal code As String, ByVal checkSum As Integer) As Entity.ProgressStatusReIssued
            Return Me.Update(id, description, code, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal progressStatusReIssued As Entity.ProgressStatusReIssued) As Entity.ProgressStatusReIssued
            Return Me.Update(progressStatusReIssued.id, progressStatusReIssued(1), progressStatusReIssued(2))
        End Function
        
        Public Overloads Function Update(ByVal progressStatusReIssued As Entity.ProgressStatusReIssued, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ProgressStatusReIssued
            Return Me.Update(progressStatusReIssued.id, progressStatusReIssued(1), progressStatusReIssued(2), transaction)
        End Function
        
        Public Overloads Function Update(ByVal progressStatusReIssued As Entity.ProgressStatusReIssued, ByVal checkSum As Integer) As Entity.ProgressStatusReIssued
            Return Me.Update(progressStatusReIssued.id, progressStatusReIssued(1), progressStatusReIssued(2), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal progressStatusReIssued As Entity.ProgressStatusReIssued, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ProgressStatusReIssued
            Return Me.Update(progressStatusReIssued.id, progressStatusReIssued(1), progressStatusReIssued(2), checkSum, transaction)
        End Function
        
        Public Overloads Function GetByIndex_IX_ProgressStatusReIssued(ByVal description As String) As EntitySet.ProgressStatusReIssuedSet
            Return Sprocs.eosp_SelectProgressStatusReIssued(progressStatusReIssuedId:=Nothing, Index_Description:=[description], Index_Code:=Nothing, transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_IX_ProgressStatusReIssued(ByVal description As String, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.ProgressStatusReIssuedSet
            Return Sprocs.eosp_SelectProgressStatusReIssued(progressStatusReIssuedId:=Nothing, Index_Description:=[description], Index_Code:=Nothing, sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Overloads Function GetByIndex_IX_ProgressStatusReIssued_1(ByVal code As String) As EntitySet.ProgressStatusReIssuedSet
            Return Sprocs.eosp_SelectProgressStatusReIssued(progressStatusReIssuedId:=Nothing, Index_Code:=[code], Index_Description:=Nothing, transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_IX_ProgressStatusReIssued_1(ByVal code As String, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.ProgressStatusReIssuedSet
            Return Sprocs.eosp_SelectProgressStatusReIssued(progressStatusReIssuedId:=Nothing, Index_Code:=[code], Index_Description:=Nothing, sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
            
            IX_ProgressStatusReIssued
            
            IX_ProgressStatusReIssued_1
            
            
        End Enum
    End Class
End Namespace

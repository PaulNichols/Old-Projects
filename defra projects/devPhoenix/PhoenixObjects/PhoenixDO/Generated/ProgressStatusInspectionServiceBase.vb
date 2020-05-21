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
    
    'Service base implementation for table 'ProgressStatusInspection'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class ProgressStatusInspectionServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.ProgressStatusInspectionSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.ProgressStatusInspectionSet
            Return CType(MyBase.GetAll("eosp_SelectProgressStatusInspection", GetType(EntitySet.ProgressStatusInspectionSet), includeHyphen, includeInactive, orderBy),EntitySet.ProgressStatusInspectionSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.ProgressStatusInspectionSet
            Return Me.GetAll(includeHyphen, includeInactive, ProgressStatusInspectionServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, ProgressStatusInspectionServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal orderBy As OrderBy) As EntitySet.ProgressStatusInspectionSet
            Return Me.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal progressStatusInspectionId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectProgressStatusInspection", "ProgressStatusInspectionId", progressStatusInspectionId, GetType(EntitySet.ProgressStatusInspectionSet), tran),Entity.ProgressStatusInspection)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal progressStatusInspectionId As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(progressStatusInspectionId, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal progressStatusInspectionId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.ProgressStatusInspection
            Return CType(MyBase.GetById("eosp_SelectProgressStatusInspection", "ProgressStatusInspectionId", progressStatusInspectionId, GetType(EntitySet.ProgressStatusInspectionSet), tran),Entity.ProgressStatusInspection)
        End Function
        
        Public Overloads Function GetById(ByVal progressStatusInspectionId As Integer) As Entity.ProgressStatusInspection
            Return Me.GetById(progressStatusInspectionId, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal progressStatusInspectionId As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(progressStatusInspectionId, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal progressStatusInspectionId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeleteProgressStatusInspection", "ProgressStatusInspectionId", progressStatusInspectionId, checkSum, transaction)
        End Function
        
        Public Overloads Function Insert(ByVal description As String, ByVal code As String, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ProgressStatusInspection
            Return Me.GetById(Sprocs.eosp_CreateProgressStatusInspection(description, code, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal description As String, ByVal code As String) As Entity.ProgressStatusInspection
            Return Me.Insert(description, code, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal progressStatusInspection As Entity.ProgressStatusInspection) As Entity.ProgressStatusInspection
            Return Me.Insert(progressStatusInspection(1), progressStatusInspection(2))
        End Function
        
        Public Overloads Function Insert(ByVal progressStatusInspection As Entity.ProgressStatusInspection, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ProgressStatusInspection
            Return Me.Insert(progressStatusInspection(1), progressStatusInspection(2), transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal description As String, ByVal code As String, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ProgressStatusInspection
            Return Sprocs.eosp_UpdateProgressStatusInspection(id, description, code, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal description As String, ByVal code As String) As Entity.ProgressStatusInspection
            Return Me.Update(id, description, code, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal description As String, ByVal code As String, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ProgressStatusInspection
            Return Me.Update(id, description, code, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal description As String, ByVal code As String, ByVal checkSum As Integer) As Entity.ProgressStatusInspection
            Return Me.Update(id, description, code, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal progressStatusInspection As Entity.ProgressStatusInspection) As Entity.ProgressStatusInspection
            Return Me.Update(progressStatusInspection.id, progressStatusInspection(1), progressStatusInspection(2))
        End Function
        
        Public Overloads Function Update(ByVal progressStatusInspection As Entity.ProgressStatusInspection, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ProgressStatusInspection
            Return Me.Update(progressStatusInspection.id, progressStatusInspection(1), progressStatusInspection(2), transaction)
        End Function
        
        Public Overloads Function Update(ByVal progressStatusInspection As Entity.ProgressStatusInspection, ByVal checkSum As Integer) As Entity.ProgressStatusInspection
            Return Me.Update(progressStatusInspection.id, progressStatusInspection(1), progressStatusInspection(2), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal progressStatusInspection As Entity.ProgressStatusInspection, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ProgressStatusInspection
            Return Me.Update(progressStatusInspection.id, progressStatusInspection(1), progressStatusInspection(2), checkSum, transaction)
        End Function
        
        Public Overloads Function GetByIndex_IX_ProgressStatusInspection(ByVal description As String) As EntitySet.ProgressStatusInspectionSet
            Return Sprocs.eosp_SelectProgressStatusInspection(progressStatusInspectionId:=Nothing, Index_Description:=[description], Index_Code:=Nothing, transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_IX_ProgressStatusInspection(ByVal description As String, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.ProgressStatusInspectionSet
            Return Sprocs.eosp_SelectProgressStatusInspection(progressStatusInspectionId:=Nothing, Index_Description:=[description], Index_Code:=Nothing, sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Overloads Function GetByIndex_IX_ProgressStatusInspection_1(ByVal code As String) As EntitySet.ProgressStatusInspectionSet
            Return Sprocs.eosp_SelectProgressStatusInspection(progressStatusInspectionId:=Nothing, Index_Code:=[code], Index_Description:=Nothing, transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_IX_ProgressStatusInspection_1(ByVal code As String, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.ProgressStatusInspectionSet
            Return Sprocs.eosp_SelectProgressStatusInspection(progressStatusInspectionId:=Nothing, Index_Code:=[code], Index_Description:=Nothing, sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
            
            IX_ProgressStatusInspection
            
            IX_ProgressStatusInspection_1
            
            
        End Enum
    End Class
End Namespace

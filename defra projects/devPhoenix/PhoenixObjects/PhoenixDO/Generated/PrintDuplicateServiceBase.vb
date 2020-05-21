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
    
    'Service base implementation for table 'PrintDuplicate'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class PrintDuplicateServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.PrintDuplicateSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.PrintDuplicateSet
            Return CType(MyBase.GetAll("eosp_SelectPrintDuplicate", GetType(EntitySet.PrintDuplicateSet), includeHyphen, includeInactive, orderBy),EntitySet.PrintDuplicateSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.PrintDuplicateSet
            Return Me.GetAll(includeHyphen, includeInactive, PrintDuplicateServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, PrintDuplicateServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal orderBy As OrderBy) As EntitySet.PrintDuplicateSet
            Return Me.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal printDuplicateId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectPrintDuplicate", "PrintDuplicateId", printDuplicateId, GetType(EntitySet.PrintDuplicateSet), tran),Entity.PrintDuplicate)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal printDuplicateId As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(printDuplicateId, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal printDuplicateId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.PrintDuplicate
            Return CType(MyBase.GetById("eosp_SelectPrintDuplicate", "PrintDuplicateId", printDuplicateId, GetType(EntitySet.PrintDuplicateSet), tran),Entity.PrintDuplicate)
        End Function
        
        Public Overloads Function GetById(ByVal printDuplicateId As Integer) As Entity.PrintDuplicate
            Return Me.GetById(printDuplicateId, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal printDuplicateId As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(printDuplicateId, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal printDuplicateId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeletePrintDuplicate", "PrintDuplicateId", printDuplicateId, checkSum, transaction)
        End Function
        
        'GetForPermit - links to the Permit table...
        Public Overloads Function GetForPermit(ByVal PermitId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PrintDuplicateSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from PrintDuplicate where PermitId=" + PermitId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.PrintDuplicateSet), tran),EntitySet.PrintDuplicateSet)
        End Function
        
        'GetForPermit - links to the Permit table...
        Public Overloads Function GetForPermit(ByVal PermitId As Integer) As EntitySet.PrintDuplicateSet
            Return Me.GetForPermit(PermitId, Nothing)
        End Function
        
        'GetForDuplicateReason - links to the DuplicateReason table...
        Public Overloads Function GetForDuplicateReason(ByVal DuplicateReasonId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PrintDuplicateSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from PrintDuplicate where DuplicateReaso"& _ 
"nId=" + DuplicateReasonId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.PrintDuplicateSet), tran),EntitySet.PrintDuplicateSet)
        End Function
        
        'GetForDuplicateReason - links to the DuplicateReason table...
        Public Overloads Function GetForDuplicateReason(ByVal DuplicateReasonId As Integer) As EntitySet.PrintDuplicateSet
            Return Me.GetForDuplicateReason(DuplicateReasonId, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal [date] As Object, ByVal authorisedDate As Date, ByVal authorisedUserId As Object, ByVal permitId As Object, ByVal duplicateReasonId As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PrintDuplicate
            Return Me.GetById(Sprocs.eosp_CreatePrintDuplicate([date], authorisedDate, authorisedUserId, permitId, duplicateReasonId, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal [date] As Object, ByVal authorisedDate As Date, ByVal authorisedUserId As Object, ByVal permitId As Object, ByVal duplicateReasonId As Object) As Entity.PrintDuplicate
            Return Me.Insert([date], authorisedDate, authorisedUserId, permitId, duplicateReasonId, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal printDuplicate As Entity.PrintDuplicate) As Entity.PrintDuplicate
            Return Me.Insert(printDuplicate(1), printDuplicate(2), printDuplicate(3), printDuplicate(4), printDuplicate(5))
        End Function
        
        Public Overloads Function Insert(ByVal printDuplicate As Entity.PrintDuplicate, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PrintDuplicate
            Return Me.Insert(printDuplicate(1), printDuplicate(2), printDuplicate(3), printDuplicate(4), printDuplicate(5), transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal [date] As Object, ByVal authorisedDate As Date, ByVal authorisedUserId As Object, ByVal permitId As Object, ByVal duplicateReasonId As Object, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PrintDuplicate
            Return Sprocs.eosp_UpdatePrintDuplicate(id, [date], authorisedDate, authorisedUserId, permitId, duplicateReasonId, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal [date] As Object, ByVal authorisedDate As Date, ByVal authorisedUserId As Object, ByVal permitId As Object, ByVal duplicateReasonId As Object) As Entity.PrintDuplicate
            Return Me.Update(id, [date], authorisedDate, authorisedUserId, permitId, duplicateReasonId, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal [date] As Object, ByVal authorisedDate As Date, ByVal authorisedUserId As Object, ByVal permitId As Object, ByVal duplicateReasonId As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PrintDuplicate
            Return Me.Update(id, [date], authorisedDate, authorisedUserId, permitId, duplicateReasonId, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal [date] As Object, ByVal authorisedDate As Date, ByVal authorisedUserId As Object, ByVal permitId As Object, ByVal duplicateReasonId As Object, ByVal checkSum As Integer) As Entity.PrintDuplicate
            Return Me.Update(id, [date], authorisedDate, authorisedUserId, permitId, duplicateReasonId, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal printDuplicate As Entity.PrintDuplicate) As Entity.PrintDuplicate
            Return Me.Update(printDuplicate.id, printDuplicate(1), printDuplicate(2), printDuplicate(3), printDuplicate(4), printDuplicate(5))
        End Function
        
        Public Overloads Function Update(ByVal printDuplicate As Entity.PrintDuplicate, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PrintDuplicate
            Return Me.Update(printDuplicate.id, printDuplicate(1), printDuplicate(2), printDuplicate(3), printDuplicate(4), printDuplicate(5), transaction)
        End Function
        
        Public Overloads Function Update(ByVal printDuplicate As Entity.PrintDuplicate, ByVal checkSum As Integer) As Entity.PrintDuplicate
            Return Me.Update(printDuplicate.id, printDuplicate(1), printDuplicate(2), printDuplicate(3), printDuplicate(4), printDuplicate(5), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal printDuplicate As Entity.PrintDuplicate, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PrintDuplicate
            Return Me.Update(printDuplicate.id, printDuplicate(1), printDuplicate(2), printDuplicate(3), printDuplicate(4), printDuplicate(5), checkSum, transaction)
        End Function
        
        Public Overloads Function GetByIndex_UQ__PrintDuplicate__5F7E2DAC(ByVal printDuplicateId As Integer, ByVal permitId As Integer, ByVal duplicateReasonId As Integer) As EntitySet.PrintDuplicateSet
            Return Sprocs.eosp_SelectPrintDuplicate(printDuplicateId:=Nothing, Index_PrintDuplicateId:=[printDuplicateId], Index_PermitId:=[permitId], Index_DuplicateReasonId:=[duplicateReasonId], transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_UQ__PrintDuplicate__5F7E2DAC(ByVal printDuplicateId As Integer, ByVal permitId As Integer, ByVal duplicateReasonId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.PrintDuplicateSet
            Return Sprocs.eosp_SelectPrintDuplicate(printDuplicateId:=Nothing, Index_PrintDuplicateId:=[printDuplicateId], Index_PermitId:=[permitId], Index_DuplicateReasonId:=[duplicateReasonId], sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
            
            UQ__PrintDuplicate__5F7E2DAC
            
            
        End Enum
    End Class
End Namespace

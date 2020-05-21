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
    
    'Service base implementation for table 'DelegationGuideline'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class DelegationGuidelineServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.DelegationGuidelineSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.DelegationGuidelineSet
            Return CType(MyBase.GetAll("eosp_SelectDelegationGuideline", GetType(EntitySet.DelegationGuidelineSet), includeHyphen, includeInactive, orderBy),EntitySet.DelegationGuidelineSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.DelegationGuidelineSet
            Return Me.GetAll(includeHyphen, includeInactive, DelegationGuidelineServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, DelegationGuidelineServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal orderBy As OrderBy) As EntitySet.DelegationGuidelineSet
            Return Me.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal delegationGuidelineID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectDelegationGuideline", "DelegationGuidelineID", delegationGuidelineID, GetType(EntitySet.DelegationGuidelineSet), tran),Entity.DelegationGuideline)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal delegationGuidelineID As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(delegationGuidelineID, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal delegationGuidelineID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.DelegationGuideline
            Return CType(MyBase.GetById("eosp_SelectDelegationGuideline", "DelegationGuidelineID", delegationGuidelineID, GetType(EntitySet.DelegationGuidelineSet), tran),Entity.DelegationGuideline)
        End Function
        
        Public Overloads Function GetById(ByVal delegationGuidelineID As Integer) As Entity.DelegationGuideline
            Return Me.GetById(delegationGuidelineID, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal delegationGuidelineID As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(delegationGuidelineID, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal delegationGuidelineID As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeleteDelegationGuideline", "DelegationGuidelineID", delegationGuidelineID, checkSum, transaction)
        End Function
        
        'GetForApplicationType - links to the ApplicationType table...
        Public Overloads Function GetForApplicationType(ByVal ApplicationTypeId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.DelegationGuidelineSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from DelegationGuideline where Applicati"& _ 
"onTypeCode=" + ApplicationTypeId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.DelegationGuidelineSet), tran),EntitySet.DelegationGuidelineSet)
        End Function
        
        'GetForApplicationType - links to the ApplicationType table...
        Public Overloads Function GetForApplicationType(ByVal ApplicationTypeId As Integer) As EntitySet.DelegationGuidelineSet
            Return Me.GetForApplicationType(ApplicationTypeId, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal applicationTypeCode As Integer, ByVal code As Integer, ByVal subject As String, ByVal reason As String, ByVal active As Boolean, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.DelegationGuideline
            Return Me.GetById(Sprocs.eosp_CreateDelegationGuideline(applicationTypeCode, code, subject, reason, active, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal applicationTypeCode As Integer, ByVal code As Integer, ByVal subject As String, ByVal reason As String, ByVal active As Boolean) As Entity.DelegationGuideline
            Return Me.Insert(applicationTypeCode, code, subject, reason, active, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal delegationGuideline As Entity.DelegationGuideline) As Entity.DelegationGuideline
            Return Me.Insert(delegationGuideline(1), delegationGuideline(2), delegationGuideline(3), delegationGuideline(4), delegationGuideline(5))
        End Function
        
        Public Overloads Function Insert(ByVal delegationGuideline As Entity.DelegationGuideline, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.DelegationGuideline
            Return Me.Insert(delegationGuideline(1), delegationGuideline(2), delegationGuideline(3), delegationGuideline(4), delegationGuideline(5), transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal applicationTypeCode As Integer, ByVal code As Integer, ByVal subject As String, ByVal reason As String, ByVal active As Boolean, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.DelegationGuideline
            Return Sprocs.eosp_UpdateDelegationGuideline(id, applicationTypeCode, code, subject, reason, active, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal applicationTypeCode As Integer, ByVal code As Integer, ByVal subject As String, ByVal reason As String, ByVal active As Boolean) As Entity.DelegationGuideline
            Return Me.Update(id, applicationTypeCode, code, subject, reason, active, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal applicationTypeCode As Integer, ByVal code As Integer, ByVal subject As String, ByVal reason As String, ByVal active As Boolean, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.DelegationGuideline
            Return Me.Update(id, applicationTypeCode, code, subject, reason, active, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal applicationTypeCode As Integer, ByVal code As Integer, ByVal subject As String, ByVal reason As String, ByVal active As Boolean, ByVal checkSum As Integer) As Entity.DelegationGuideline
            Return Me.Update(id, applicationTypeCode, code, subject, reason, active, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal delegationGuideline As Entity.DelegationGuideline) As Entity.DelegationGuideline
            Return Me.Update(delegationGuideline.id, delegationGuideline(1), delegationGuideline(2), delegationGuideline(3), delegationGuideline(4), delegationGuideline(5))
        End Function
        
        Public Overloads Function Update(ByVal delegationGuideline As Entity.DelegationGuideline, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.DelegationGuideline
            Return Me.Update(delegationGuideline.id, delegationGuideline(1), delegationGuideline(2), delegationGuideline(3), delegationGuideline(4), delegationGuideline(5), transaction)
        End Function
        
        Public Overloads Function Update(ByVal delegationGuideline As Entity.DelegationGuideline, ByVal checkSum As Integer) As Entity.DelegationGuideline
            Return Me.Update(delegationGuideline.id, delegationGuideline(1), delegationGuideline(2), delegationGuideline(3), delegationGuideline(4), delegationGuideline(5), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal delegationGuideline As Entity.DelegationGuideline, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.DelegationGuideline
            Return Me.Update(delegationGuideline.id, delegationGuideline(1), delegationGuideline(2), delegationGuideline(3), delegationGuideline(4), delegationGuideline(5), checkSum, transaction)
        End Function
        
        Public Overloads Function GetByIndex_IX_ApplicationTypeCode(ByVal applicationTypeCode As Integer) As EntitySet.DelegationGuidelineSet
            Return Sprocs.eosp_SelectDelegationGuideline(delegationGuidelineID:=Nothing, Index_ApplicationTypeCode:=[applicationTypeCode], Index_Code:=Nothing, transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_IX_ApplicationTypeCode(ByVal applicationTypeCode As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.DelegationGuidelineSet
            Return Sprocs.eosp_SelectDelegationGuideline(delegationGuidelineID:=Nothing, Index_ApplicationTypeCode:=[applicationTypeCode], Index_Code:=Nothing, sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Overloads Function GetByIndex_IX_DelegationGuideLineAndCode(ByVal applicationTypeCode As Integer, ByVal code As Integer) As EntitySet.DelegationGuidelineSet
            Return Sprocs.eosp_SelectDelegationGuideline(delegationGuidelineID:=Nothing, Index_ApplicationTypeCode:=[applicationTypeCode], Index_Code:=[code], transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_IX_DelegationGuideLineAndCode(ByVal applicationTypeCode As Integer, ByVal code As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.DelegationGuidelineSet
            Return Sprocs.eosp_SelectDelegationGuideline(delegationGuidelineID:=Nothing, Index_ApplicationTypeCode:=[applicationTypeCode], Index_Code:=[code], sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
            
            IX_ApplicationTypeCode
            
            IX_DelegationGuideLineAndCode
            
            
        End Enum
    End Class
End Namespace
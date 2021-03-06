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
    
    'Service base implementation for table 'ContactTypeGroup'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class ContactTypeGroupServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return true
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.ContactTypeGroupSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.ContactTypeGroupSet
            Return CType(MyBase.GetAll("eosp_SelectContactTypeGroup", GetType(EntitySet.ContactTypeGroupSet), includeHyphen, includeInactive, orderBy),EntitySet.ContactTypeGroupSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.ContactTypeGroupSet
            Return Me.GetAll(includeHyphen, includeInactive, ContactTypeGroupServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, ContactTypeGroupServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal orderBy As OrderBy) As EntitySet.ContactTypeGroupSet
            Return Me.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal contactTypeGroupId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectContactTypeGroup", "ContactTypeGroupId", contactTypeGroupId, GetType(EntitySet.ContactTypeGroupSet), tran),Entity.ContactTypeGroup)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal contactTypeGroupId As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(contactTypeGroupId, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal contactTypeGroupId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.ContactTypeGroup
            Return CType(MyBase.GetById("eosp_SelectContactTypeGroup", "ContactTypeGroupId", contactTypeGroupId, GetType(EntitySet.ContactTypeGroupSet), tran),Entity.ContactTypeGroup)
        End Function
        
        Public Overloads Function GetById(ByVal contactTypeGroupId As Integer) As Entity.ContactTypeGroup
            Return Me.GetById(contactTypeGroupId, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal contactTypeGroupId As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(contactTypeGroupId, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal contactTypeGroupId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeleteContactTypeGroup", "ContactTypeGroupId", contactTypeGroupId, checkSum, transaction)
        End Function
        
        Public Overloads Function Insert(ByVal expression As String, ByVal groupName As String, ByVal active As Boolean, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ContactTypeGroup
            Return Me.GetById(Sprocs.eosp_CreateContactTypeGroup(expression, groupName, active, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal expression As String, ByVal groupName As String, ByVal active As Boolean) As Entity.ContactTypeGroup
            Return Me.Insert(expression, groupName, active, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal contactTypeGroup As Entity.ContactTypeGroup) As Entity.ContactTypeGroup
            Return Me.Insert(contactTypeGroup(1), contactTypeGroup(2), contactTypeGroup(3))
        End Function
        
        Public Overloads Function Insert(ByVal contactTypeGroup As Entity.ContactTypeGroup, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ContactTypeGroup
            Return Me.Insert(contactTypeGroup(1), contactTypeGroup(2), contactTypeGroup(3), transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal expression As String, ByVal groupName As String, ByVal active As Boolean, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ContactTypeGroup
            Return Sprocs.eosp_UpdateContactTypeGroup(id, expression, groupName, active, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal expression As String, ByVal groupName As String, ByVal active As Boolean) As Entity.ContactTypeGroup
            Return Me.Update(id, expression, groupName, active, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal expression As String, ByVal groupName As String, ByVal active As Boolean, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ContactTypeGroup
            Return Me.Update(id, expression, groupName, active, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal expression As String, ByVal groupName As String, ByVal active As Boolean, ByVal checkSum As Integer) As Entity.ContactTypeGroup
            Return Me.Update(id, expression, groupName, active, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal contactTypeGroup As Entity.ContactTypeGroup) As Entity.ContactTypeGroup
            Return Me.Update(contactTypeGroup.id, contactTypeGroup(1), contactTypeGroup(2), contactTypeGroup(3))
        End Function
        
        Public Overloads Function Update(ByVal contactTypeGroup As Entity.ContactTypeGroup, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ContactTypeGroup
            Return Me.Update(contactTypeGroup.id, contactTypeGroup(1), contactTypeGroup(2), contactTypeGroup(3), transaction)
        End Function
        
        Public Overloads Function Update(ByVal contactTypeGroup As Entity.ContactTypeGroup, ByVal checkSum As Integer) As Entity.ContactTypeGroup
            Return Me.Update(contactTypeGroup.id, contactTypeGroup(1), contactTypeGroup(2), contactTypeGroup(3), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal contactTypeGroup As Entity.ContactTypeGroup, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ContactTypeGroup
            Return Me.Update(contactTypeGroup.id, contactTypeGroup(1), contactTypeGroup(2), contactTypeGroup(3), checkSum, transaction)
        End Function
        
        Public Overloads Function GetByIndex_IX_ContactTypeGroup_GroupName(ByVal groupName As String, ByVal includeInactive As Boolean) As EntitySet.ContactTypeGroupSet
            Return Sprocs.eosp_SelectContactTypeGroup(contactTypeGroupId:=Nothing, Index_GroupName:=[groupName], includeInactive:=includeInactive, transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_IX_ContactTypeGroup_GroupName(ByVal groupName As String, ByVal includeInactive As Boolean, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.ContactTypeGroupSet
            Return Sprocs.eosp_SelectContactTypeGroup(contactTypeGroupId:=Nothing, Index_GroupName:=[groupName], includeInactive:=includeInactive, sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
            
            IX_ContactTypeGroup_GroupName
            
            
        End Enum
    End Class
End Namespace

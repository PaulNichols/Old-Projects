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
    
    'Service base implementation for table 'Business'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class BusinessServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.BusinessSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.BusinessSet
            Return CType(MyBase.GetAll("eosp_SelectBusiness", GetType(EntitySet.BusinessSet), includeHyphen, includeInactive, orderBy),EntitySet.BusinessSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.BusinessSet
            Return Me.GetAll(includeHyphen, includeInactive, BusinessServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, BusinessServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal businessId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectBusiness", "BusinessId", businessId, GetType(EntitySet.BusinessSet), tran),Entity.Business)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal businessId As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(businessId, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal businessId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.Business
            Return CType(MyBase.GetById("eosp_SelectBusiness", "BusinessId", businessId, GetType(EntitySet.BusinessSet), tran),Entity.Business)
        End Function
        
        Public Overloads Function GetById(ByVal businessId As Integer) As Entity.Business
            Return Me.GetById(businessId, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal businessId As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(businessId, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal businessId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeleteBusiness", "BusinessId", businessId, checkSum, transaction)
        End Function
        
        'GetForBusinessType - links to the BusinessType table...
        Public Overloads Function GetForBusinessType(ByVal BusinessTypeId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.BusinessSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from Business where BusinessTypeId=" + BusinessTypeId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.BusinessSet), tran),EntitySet.BusinessSet)
        End Function
        
        'GetForBusinessType - links to the BusinessType table...
        Public Overloads Function GetForBusinessType(ByVal BusinessTypeId As Integer) As EntitySet.BusinessSet
            Return Me.GetForBusinessType(BusinessTypeId, Nothing)
        End Function
        
        'GetForParty - links to the Party table...
        Public Overloads Function GetForParty(ByVal PartyId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.BusinessSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from Business where PartyId=" + PartyId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.BusinessSet), tran),EntitySet.BusinessSet)
        End Function
        
        'GetForParty - links to the Party table...
        Public Overloads Function GetForParty(ByVal PartyId As Integer) As EntitySet.BusinessSet
            Return Me.GetForParty(PartyId, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal businessName As Object, ByVal businessTypeId As Integer, ByVal partyId As Integer, ByVal cITESRegisteredNumber As Object, ByVal defaultManagementForCountry As Boolean, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Business
            Return Me.GetById(Sprocs.eosp_CreateBusiness(businessName, businessTypeId, partyId, cITESRegisteredNumber, defaultManagementForCountry, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal businessName As Object, ByVal businessTypeId As Integer, ByVal partyId As Integer, ByVal cITESRegisteredNumber As Object, ByVal defaultManagementForCountry As Boolean) As Entity.Business
            Return Me.Insert(businessName, businessTypeId, partyId, cITESRegisteredNumber, defaultManagementForCountry, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal business As Entity.Business) As Entity.Business
            Return Me.Insert(business(1), business(2), business(3), business(4), business(5))
        End Function
        
        Public Overloads Function Insert(ByVal business As Entity.Business, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Business
            Return Me.Insert(business(1), business(2), business(3), business(4), business(5), transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal businessName As Object, ByVal businessTypeId As Integer, ByVal partyId As Integer, ByVal cITESRegisteredNumber As Object, ByVal defaultManagementForCountry As Boolean, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Business
            Return Sprocs.eosp_UpdateBusiness(id, businessName, businessTypeId, partyId, cITESRegisteredNumber, defaultManagementForCountry, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal businessName As Object, ByVal businessTypeId As Integer, ByVal partyId As Integer, ByVal cITESRegisteredNumber As Object, ByVal defaultManagementForCountry As Boolean) As Entity.Business
            Return Me.Update(id, businessName, businessTypeId, partyId, cITESRegisteredNumber, defaultManagementForCountry, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal businessName As Object, ByVal businessTypeId As Integer, ByVal partyId As Integer, ByVal cITESRegisteredNumber As Object, ByVal defaultManagementForCountry As Boolean, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Business
            Return Me.Update(id, businessName, businessTypeId, partyId, cITESRegisteredNumber, defaultManagementForCountry, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal businessName As Object, ByVal businessTypeId As Integer, ByVal partyId As Integer, ByVal cITESRegisteredNumber As Object, ByVal defaultManagementForCountry As Boolean, ByVal checkSum As Integer) As Entity.Business
            Return Me.Update(id, businessName, businessTypeId, partyId, cITESRegisteredNumber, defaultManagementForCountry, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal business As Entity.Business) As Entity.Business
            Return Me.Update(business.id, business(1), business(2), business(3), business(4), business(5))
        End Function
        
        Public Overloads Function Update(ByVal business As Entity.Business, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Business
            Return Me.Update(business.id, business(1), business(2), business(3), business(4), business(5), transaction)
        End Function
        
        Public Overloads Function Update(ByVal business As Entity.Business, ByVal checkSum As Integer) As Entity.Business
            Return Me.Update(business.id, business(1), business(2), business(3), business(4), business(5), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal business As Entity.Business, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Business
            Return Me.Update(business.id, business(1), business(2), business(3), business(4), business(5), checkSum, transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
        End Enum
    End Class
End Namespace

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
    
    'Service base implementation for table 'CITESApplication'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class CITESApplicationServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.CITESApplicationSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.CITESApplicationSet
            Return CType(MyBase.GetAll("eosp_SelectCITESApplication", GetType(EntitySet.CITESApplicationSet), includeHyphen, includeInactive, orderBy),EntitySet.CITESApplicationSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.CITESApplicationSet
            Return Me.GetAll(includeHyphen, includeInactive, CITESApplicationServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, CITESApplicationServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal orderBy As OrderBy) As EntitySet.CITESApplicationSet
            Return Me.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal citesApplicationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectCITESApplication", "CitesApplicationId", citesApplicationId, GetType(EntitySet.CITESApplicationSet), tran),Entity.CITESApplication)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal citesApplicationId As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(citesApplicationId, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal citesApplicationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.CITESApplication
            Return CType(MyBase.GetById("eosp_SelectCITESApplication", "CitesApplicationId", citesApplicationId, GetType(EntitySet.CITESApplicationSet), tran),Entity.CITESApplication)
        End Function
        
        Public Overloads Function GetById(ByVal citesApplicationId As Integer) As Entity.CITESApplication
            Return Me.GetById(citesApplicationId, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal citesApplicationId As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(citesApplicationId, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal citesApplicationId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeleteCITESApplication", "CitesApplicationId", citesApplicationId, checkSum, transaction)
        End Function
        
        'GetForApplication - links to the Application table...
        Public Overloads Function GetForApplication(ByVal ApplicationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESApplicationSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from CITESApplication where ApplicationI"& _ 
"d=" + ApplicationId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.CITESApplicationSet), tran),EntitySet.CITESApplicationSet)
        End Function
        
        'GetForApplication - links to the Application table...
        Public Overloads Function GetForApplication(ByVal ApplicationId As Integer) As EntitySet.CITESApplicationSet
            Return Me.GetForApplication(ApplicationId, Nothing)
        End Function
        
        'GetForManagementAuthorityIdPartyLink - links to the PartyLink table...
        Public Overloads Function GetForManagementAuthorityIdPartyLink(ByVal ManagementAuthorityId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESApplicationSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from CITESApplication where ManagementAu"& _ 
"thorityId=" + ManagementAuthorityId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.CITESApplicationSet), tran),EntitySet.CITESApplicationSet)
        End Function
        
        'GetForManagementAuthorityIdPartyLink - links to the PartyLink table...
        Public Overloads Function GetForManagementAuthorityIdPartyLink(ByVal ManagementAuthorityId As Integer) As EntitySet.CITESApplicationSet
            Return Me.GetForManagementAuthorityIdPartyLink(ManagementAuthorityId, Nothing)
        End Function
        
        'GetForSecondPartyIdPartyLink - links to the PartyLink table...
        Public Overloads Function GetForSecondPartyIdPartyLink(ByVal SecondPartyId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESApplicationSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from CITESApplication where SecondPartyI"& _ 
"d=" + SecondPartyId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.CITESApplicationSet), tran),EntitySet.CITESApplicationSet)
        End Function
        
        'GetForSecondPartyIdPartyLink - links to the PartyLink table...
        Public Overloads Function GetForSecondPartyIdPartyLink(ByVal SecondPartyId As Integer) As EntitySet.CITESApplicationSet
            Return Me.GetForSecondPartyIdPartyLink(SecondPartyId, Nothing)
        End Function
        
        'GetForForeignManagementAuthorityIdPartyLink - links to the PartyLink table...
        Public Overloads Function GetForForeignManagementAuthorityIdPartyLink(ByVal ForeignManagementAuthorityId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESApplicationSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from CITESApplication where ForeignManag"& _ 
"ementAuthorityId=" + ForeignManagementAuthorityId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.CITESApplicationSet), tran),EntitySet.CITESApplicationSet)
        End Function
        
        'GetForForeignManagementAuthorityIdPartyLink - links to the PartyLink table...
        Public Overloads Function GetForForeignManagementAuthorityIdPartyLink(ByVal ForeignManagementAuthorityId As Integer) As EntitySet.CITESApplicationSet
            Return Me.GetForForeignManagementAuthorityIdPartyLink(ForeignManagementAuthorityId, Nothing)
        End Function
        
        'GetForCountry - links to the Country table...
        Public Overloads Function GetForCountry(ByVal CountryId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESApplicationSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from CITESApplication where CountryOfImp"& _ 
"ortId=" + CountryId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.CITESApplicationSet), tran),EntitySet.CITESApplicationSet)
        End Function
        
        'GetForCountry - links to the Country table...
        Public Overloads Function GetForCountry(ByVal CountryId As Integer) As EntitySet.CITESApplicationSet
            Return Me.GetForCountry(CountryId, Nothing)
        End Function
        
        'GetForAddress - links to the Address table...
        Public Overloads Function GetForAddress(ByVal AddressId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESApplicationSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from CITESApplication where LocationAddr"& _ 
"essId=" + AddressId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.CITESApplicationSet), tran),EntitySet.CITESApplicationSet)
        End Function
        
        'GetForAddress - links to the Address table...
        Public Overloads Function GetForAddress(ByVal AddressId As Integer) As EntitySet.CITESApplicationSet
            Return Me.GetForAddress(AddressId, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal applicationId As Integer, ByVal managementAuthorityId As Object, ByVal secondPartyId As Object, ByVal foreignManagementAuthorityId As Object, ByVal countryOfImportId As Object, ByVal locationAddressId As Object, ByVal consignment As Object, ByVal isComposite As Boolean, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.CITESApplication
            Return Me.GetById(Sprocs.eosp_CreateCITESApplication(applicationId, managementAuthorityId, secondPartyId, foreignManagementAuthorityId, countryOfImportId, locationAddressId, consignment, isComposite, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal applicationId As Integer, ByVal managementAuthorityId As Object, ByVal secondPartyId As Object, ByVal foreignManagementAuthorityId As Object, ByVal countryOfImportId As Object, ByVal locationAddressId As Object, ByVal consignment As Object, ByVal isComposite As Boolean) As Entity.CITESApplication
            Return Me.Insert(applicationId, managementAuthorityId, secondPartyId, foreignManagementAuthorityId, countryOfImportId, locationAddressId, consignment, isComposite, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal cITESApplication As Entity.CITESApplication) As Entity.CITESApplication
            Return Me.Insert(cITESApplication(1), cITESApplication(2), cITESApplication(3), cITESApplication(4), cITESApplication(5), cITESApplication(6), cITESApplication(7), cITESApplication(8))
        End Function
        
        Public Overloads Function Insert(ByVal cITESApplication As Entity.CITESApplication, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.CITESApplication
            Return Me.Insert(cITESApplication(1), cITESApplication(2), cITESApplication(3), cITESApplication(4), cITESApplication(5), cITESApplication(6), cITESApplication(7), cITESApplication(8), transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal applicationId As Integer, ByVal managementAuthorityId As Object, ByVal secondPartyId As Object, ByVal foreignManagementAuthorityId As Object, ByVal countryOfImportId As Object, ByVal locationAddressId As Object, ByVal consignment As Object, ByVal isComposite As Boolean, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.CITESApplication
            Return Sprocs.eosp_UpdateCITESApplication(id, applicationId, managementAuthorityId, secondPartyId, foreignManagementAuthorityId, countryOfImportId, locationAddressId, consignment, isComposite, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal applicationId As Integer, ByVal managementAuthorityId As Object, ByVal secondPartyId As Object, ByVal foreignManagementAuthorityId As Object, ByVal countryOfImportId As Object, ByVal locationAddressId As Object, ByVal consignment As Object, ByVal isComposite As Boolean) As Entity.CITESApplication
            Return Me.Update(id, applicationId, managementAuthorityId, secondPartyId, foreignManagementAuthorityId, countryOfImportId, locationAddressId, consignment, isComposite, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal applicationId As Integer, ByVal managementAuthorityId As Object, ByVal secondPartyId As Object, ByVal foreignManagementAuthorityId As Object, ByVal countryOfImportId As Object, ByVal locationAddressId As Object, ByVal consignment As Object, ByVal isComposite As Boolean, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.CITESApplication
            Return Me.Update(id, applicationId, managementAuthorityId, secondPartyId, foreignManagementAuthorityId, countryOfImportId, locationAddressId, consignment, isComposite, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal applicationId As Integer, ByVal managementAuthorityId As Object, ByVal secondPartyId As Object, ByVal foreignManagementAuthorityId As Object, ByVal countryOfImportId As Object, ByVal locationAddressId As Object, ByVal consignment As Object, ByVal isComposite As Boolean, ByVal checkSum As Integer) As Entity.CITESApplication
            Return Me.Update(id, applicationId, managementAuthorityId, secondPartyId, foreignManagementAuthorityId, countryOfImportId, locationAddressId, consignment, isComposite, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal cITESApplication As Entity.CITESApplication) As Entity.CITESApplication
            Return Me.Update(cITESApplication.id, cITESApplication(1), cITESApplication(2), cITESApplication(3), cITESApplication(4), cITESApplication(5), cITESApplication(6), cITESApplication(7), cITESApplication(8))
        End Function
        
        Public Overloads Function Update(ByVal cITESApplication As Entity.CITESApplication, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.CITESApplication
            Return Me.Update(cITESApplication.id, cITESApplication(1), cITESApplication(2), cITESApplication(3), cITESApplication(4), cITESApplication(5), cITESApplication(6), cITESApplication(7), cITESApplication(8), transaction)
        End Function
        
        Public Overloads Function Update(ByVal cITESApplication As Entity.CITESApplication, ByVal checkSum As Integer) As Entity.CITESApplication
            Return Me.Update(cITESApplication.id, cITESApplication(1), cITESApplication(2), cITESApplication(3), cITESApplication(4), cITESApplication(5), cITESApplication(6), cITESApplication(7), cITESApplication(8), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal cITESApplication As Entity.CITESApplication, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.CITESApplication
            Return Me.Update(cITESApplication.id, cITESApplication(1), cITESApplication(2), cITESApplication(3), cITESApplication(4), cITESApplication(5), cITESApplication(6), cITESApplication(7), cITESApplication(8), checkSum, transaction)
        End Function
        
        Public Overloads Function GetByIndex_UQ__CITESApplication__55009F39(ByVal citesApplicationId As Integer, ByVal applicationId As Integer) As EntitySet.CITESApplicationSet
            Return Sprocs.eosp_SelectCITESApplication(citesApplicationId:=Nothing, Index_CitesApplicationId:=[citesApplicationId], Index_ApplicationId:=[applicationId], Index_SecondPartyId:=Nothing, transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_UQ__CITESApplication__55009F39(ByVal citesApplicationId As Integer, ByVal applicationId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESApplicationSet
            Return Sprocs.eosp_SelectCITESApplication(citesApplicationId:=Nothing, Index_CitesApplicationId:=[citesApplicationId], Index_ApplicationId:=[applicationId], Index_SecondPartyId:=Nothing, sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Overloads Function GetByIndex_IX_CITESApplication(ByVal secondPartyId As Integer) As EntitySet.CITESApplicationSet
            Return Sprocs.eosp_SelectCITESApplication(citesApplicationId:=Nothing, Index_SecondPartyId:=[secondPartyId], Index_CitesApplicationId:=Nothing, Index_ApplicationId:=Nothing, transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_IX_CITESApplication(ByVal secondPartyId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESApplicationSet
            Return Sprocs.eosp_SelectCITESApplication(citesApplicationId:=Nothing, Index_SecondPartyId:=[secondPartyId], Index_CitesApplicationId:=Nothing, Index_ApplicationId:=Nothing, sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
            
            UQ__CITESApplication__55009F39
            
            IX_CITESApplication
            
            
        End Enum
    End Class
End Namespace

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
    
    'Service base implementation for table 'ImportNotification'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class ImportNotificationServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.ImportNotificationSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.ImportNotificationSet
            Return CType(MyBase.GetAll("eosp_SelectImportNotification", GetType(EntitySet.ImportNotificationSet), includeHyphen, includeInactive, orderBy),EntitySet.ImportNotificationSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.ImportNotificationSet
            Return Me.GetAll(includeHyphen, includeInactive, ImportNotificationServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, ImportNotificationServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal importNotificationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectImportNotification", "ImportNotificationId", importNotificationId, GetType(EntitySet.ImportNotificationSet), tran),Entity.ImportNotification)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal importNotificationId As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(importNotificationId, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal importNotificationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.ImportNotification
            Return CType(MyBase.GetById("eosp_SelectImportNotification", "ImportNotificationId", importNotificationId, GetType(EntitySet.ImportNotificationSet), tran),Entity.ImportNotification)
        End Function
        
        Public Overloads Function GetById(ByVal importNotificationId As Integer) As Entity.ImportNotification
            Return Me.GetById(importNotificationId, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal importNotificationId As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(importNotificationId, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal importNotificationId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeleteImportNotification", "ImportNotificationId", importNotificationId, checkSum, transaction)
        End Function
        
        'GetForRegionIdUKCountry - links to the UKCountry table...
        Public Overloads Function GetForRegionIdUKCountry(ByVal RegionId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ImportNotificationSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from ImportNotification where RegionId=" + RegionId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.ImportNotificationSet), tran),EntitySet.ImportNotificationSet)
        End Function
        
        'GetForRegionIdUKCountry - links to the UKCountry table...
        Public Overloads Function GetForRegionIdUKCountry(ByVal RegionId As Integer) As EntitySet.ImportNotificationSet
            Return Me.GetForRegionIdUKCountry(RegionId, Nothing)
        End Function
        
        'GetForCITESNotification - links to the CITESNotification table...
        Public Overloads Function GetForCITESNotification(ByVal CITESNotificationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ImportNotificationSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from ImportNotification where CITESNotif"& _ 
"icationId=" + CITESNotificationId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.ImportNotificationSet), tran),EntitySet.ImportNotificationSet)
        End Function
        
        'GetForCITESNotification - links to the CITESNotification table...
        Public Overloads Function GetForCITESNotification(ByVal CITESNotificationId As Integer) As EntitySet.ImportNotificationSet
            Return Me.GetForCITESNotification(CITESNotificationId, Nothing)
        End Function
        
        'GetForCountryOfOriginRegionIdUKCountry - links to the UKCountry table...
        Public Overloads Function GetForCountryOfOriginRegionIdUKCountry(ByVal CountryOfOriginRegionId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ImportNotificationSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from ImportNotification where CountryOfO"& _ 
"riginRegionId=" + CountryOfOriginRegionId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.ImportNotificationSet), tran),EntitySet.ImportNotificationSet)
        End Function
        
        'GetForCountryOfOriginRegionIdUKCountry - links to the UKCountry table...
        Public Overloads Function GetForCountryOfOriginRegionIdUKCountry(ByVal CountryOfOriginRegionId As Integer) As EntitySet.ImportNotificationSet
            Return Me.GetForCountryOfOriginRegionIdUKCountry(CountryOfOriginRegionId, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal regionId As Object, ByVal cITESNotificationId As Object, ByVal countryOfOriginRegionId As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ImportNotification
            Return Me.GetById(Sprocs.eosp_CreateImportNotification(regionId, cITESNotificationId, countryOfOriginRegionId, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal regionId As Object, ByVal cITESNotificationId As Object, ByVal countryOfOriginRegionId As Object) As Entity.ImportNotification
            Return Me.Insert(regionId, cITESNotificationId, countryOfOriginRegionId, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal importNotification As Entity.ImportNotification) As Entity.ImportNotification
            Return Me.Insert(importNotification(1), importNotification(2), importNotification(3))
        End Function
        
        Public Overloads Function Insert(ByVal importNotification As Entity.ImportNotification, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ImportNotification
            Return Me.Insert(importNotification(1), importNotification(2), importNotification(3), transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal regionId As Object, ByVal cITESNotificationId As Object, ByVal countryOfOriginRegionId As Object, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ImportNotification
            Return Sprocs.eosp_UpdateImportNotification(id, regionId, cITESNotificationId, countryOfOriginRegionId, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal regionId As Object, ByVal cITESNotificationId As Object, ByVal countryOfOriginRegionId As Object) As Entity.ImportNotification
            Return Me.Update(id, regionId, cITESNotificationId, countryOfOriginRegionId, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal regionId As Object, ByVal cITESNotificationId As Object, ByVal countryOfOriginRegionId As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ImportNotification
            Return Me.Update(id, regionId, cITESNotificationId, countryOfOriginRegionId, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal regionId As Object, ByVal cITESNotificationId As Object, ByVal countryOfOriginRegionId As Object, ByVal checkSum As Integer) As Entity.ImportNotification
            Return Me.Update(id, regionId, cITESNotificationId, countryOfOriginRegionId, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal importNotification As Entity.ImportNotification) As Entity.ImportNotification
            Return Me.Update(importNotification.id, importNotification(1), importNotification(2), importNotification(3))
        End Function
        
        Public Overloads Function Update(ByVal importNotification As Entity.ImportNotification, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ImportNotification
            Return Me.Update(importNotification.id, importNotification(1), importNotification(2), importNotification(3), transaction)
        End Function
        
        Public Overloads Function Update(ByVal importNotification As Entity.ImportNotification, ByVal checkSum As Integer) As Entity.ImportNotification
            Return Me.Update(importNotification.id, importNotification(1), importNotification(2), importNotification(3), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal importNotification As Entity.ImportNotification, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ImportNotification
            Return Me.Update(importNotification.id, importNotification(1), importNotification(2), importNotification(3), checkSum, transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
        End Enum
    End Class
End Namespace

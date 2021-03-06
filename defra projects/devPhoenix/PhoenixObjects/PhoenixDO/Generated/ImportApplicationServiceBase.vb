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
    
    'Service base implementation for table 'ImportApplication'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class ImportApplicationServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.ImportApplicationSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.ImportApplicationSet
            Return CType(MyBase.GetAll("eosp_SelectImportApplication", GetType(EntitySet.ImportApplicationSet), includeHyphen, includeInactive, orderBy),EntitySet.ImportApplicationSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.ImportApplicationSet
            Return Me.GetAll(includeHyphen, includeInactive, ImportApplicationServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, ImportApplicationServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal importApplicationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectImportApplication", "ImportApplicationId", importApplicationId, GetType(EntitySet.ImportApplicationSet), tran),Entity.ImportApplication)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal importApplicationId As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(importApplicationId, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal importApplicationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.ImportApplication
            Return CType(MyBase.GetById("eosp_SelectImportApplication", "ImportApplicationId", importApplicationId, GetType(EntitySet.ImportApplicationSet), tran),Entity.ImportApplication)
        End Function
        
        Public Overloads Function GetById(ByVal importApplicationId As Integer) As Entity.ImportApplication
            Return Me.GetById(importApplicationId, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal importApplicationId As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(importApplicationId, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal importApplicationId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeleteImportApplication", "ImportApplicationId", importApplicationId, checkSum, transaction)
        End Function
        
        'GetForCITESApplication - links to the CITESApplication table...
        Public Overloads Function GetForCITESApplication(ByVal CitesApplicationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ImportApplicationSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from ImportApplication where CitesApplic"& _ 
"ationId=" + CitesApplicationId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.ImportApplicationSet), tran),EntitySet.ImportApplicationSet)
        End Function
        
        'GetForCITESApplication - links to the CITESApplication table...
        Public Overloads Function GetForCITESApplication(ByVal CitesApplicationId As Integer) As EntitySet.ImportApplicationSet
            Return Me.GetForCITESApplication(CitesApplicationId, Nothing)
        End Function
        
        'GetForCountry - links to the Country table...
        Public Overloads Function GetForCountry(ByVal CountryId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ImportApplicationSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from ImportApplication where CountryOfEx"& _ 
"portId=" + CountryId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.ImportApplicationSet), tran),EntitySet.ImportApplicationSet)
        End Function
        
        'GetForCountry - links to the Country table...
        Public Overloads Function GetForCountry(ByVal CountryId As Integer) As EntitySet.ImportApplicationSet
            Return Me.GetForCountry(CountryId, Nothing)
        End Function
        
        'GetForRegionOfExportIdUKCountry - links to the UKCountry table...
        Public Overloads Function GetForRegionOfExportIdUKCountry(ByVal RegionOfExportId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ImportApplicationSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from ImportApplication where RegionOfExp"& _ 
"ortId=" + RegionOfExportId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.ImportApplicationSet), tran),EntitySet.ImportApplicationSet)
        End Function
        
        'GetForRegionOfExportIdUKCountry - links to the UKCountry table...
        Public Overloads Function GetForRegionOfExportIdUKCountry(ByVal RegionOfExportId As Integer) As EntitySet.ImportApplicationSet
            Return Me.GetForRegionOfExportIdUKCountry(RegionOfExportId, Nothing)
        End Function
        
        'GetForRegionOfImportIdUKCountry - links to the UKCountry table...
        Public Overloads Function GetForRegionOfImportIdUKCountry(ByVal RegionOfImportId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ImportApplicationSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from ImportApplication where RegionOfImp"& _ 
"ortId=" + RegionOfImportId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.ImportApplicationSet), tran),EntitySet.ImportApplicationSet)
        End Function
        
        'GetForRegionOfImportIdUKCountry - links to the UKCountry table...
        Public Overloads Function GetForRegionOfImportIdUKCountry(ByVal RegionOfImportId As Integer) As EntitySet.ImportApplicationSet
            Return Me.GetForRegionOfImportIdUKCountry(RegionOfImportId, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal citesApplicationId As Integer, ByVal countryOfExportId As Object, ByVal regionOfExportId As Object, ByVal regionOfImportId As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ImportApplication
            Return Me.GetById(Sprocs.eosp_CreateImportApplication(citesApplicationId, countryOfExportId, regionOfExportId, regionOfImportId, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal citesApplicationId As Integer, ByVal countryOfExportId As Object, ByVal regionOfExportId As Object, ByVal regionOfImportId As Object) As Entity.ImportApplication
            Return Me.Insert(citesApplicationId, countryOfExportId, regionOfExportId, regionOfImportId, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal importApplication As Entity.ImportApplication) As Entity.ImportApplication
            Return Me.Insert(importApplication(1), importApplication(2), importApplication(3), importApplication(4))
        End Function
        
        Public Overloads Function Insert(ByVal importApplication As Entity.ImportApplication, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ImportApplication
            Return Me.Insert(importApplication(1), importApplication(2), importApplication(3), importApplication(4), transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal citesApplicationId As Integer, ByVal countryOfExportId As Object, ByVal regionOfExportId As Object, ByVal regionOfImportId As Object, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ImportApplication
            Return Sprocs.eosp_UpdateImportApplication(id, citesApplicationId, countryOfExportId, regionOfExportId, regionOfImportId, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal citesApplicationId As Integer, ByVal countryOfExportId As Object, ByVal regionOfExportId As Object, ByVal regionOfImportId As Object) As Entity.ImportApplication
            Return Me.Update(id, citesApplicationId, countryOfExportId, regionOfExportId, regionOfImportId, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal citesApplicationId As Integer, ByVal countryOfExportId As Object, ByVal regionOfExportId As Object, ByVal regionOfImportId As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ImportApplication
            Return Me.Update(id, citesApplicationId, countryOfExportId, regionOfExportId, regionOfImportId, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal citesApplicationId As Integer, ByVal countryOfExportId As Object, ByVal regionOfExportId As Object, ByVal regionOfImportId As Object, ByVal checkSum As Integer) As Entity.ImportApplication
            Return Me.Update(id, citesApplicationId, countryOfExportId, regionOfExportId, regionOfImportId, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal importApplication As Entity.ImportApplication) As Entity.ImportApplication
            Return Me.Update(importApplication.id, importApplication(1), importApplication(2), importApplication(3), importApplication(4))
        End Function
        
        Public Overloads Function Update(ByVal importApplication As Entity.ImportApplication, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ImportApplication
            Return Me.Update(importApplication.id, importApplication(1), importApplication(2), importApplication(3), importApplication(4), transaction)
        End Function
        
        Public Overloads Function Update(ByVal importApplication As Entity.ImportApplication, ByVal checkSum As Integer) As Entity.ImportApplication
            Return Me.Update(importApplication.id, importApplication(1), importApplication(2), importApplication(3), importApplication(4), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal importApplication As Entity.ImportApplication, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ImportApplication
            Return Me.Update(importApplication.id, importApplication(1), importApplication(2), importApplication(3), importApplication(4), checkSum, transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
        End Enum
    End Class
End Namespace

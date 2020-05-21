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
    
    'Service base implementation for table 'ExportApplication'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class ExportApplicationServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.ExportApplicationSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.ExportApplicationSet
            Return CType(MyBase.GetAll("eosp_SelectExportApplication", GetType(EntitySet.ExportApplicationSet), includeHyphen, includeInactive, orderBy),EntitySet.ExportApplicationSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.ExportApplicationSet
            Return Me.GetAll(includeHyphen, includeInactive, ExportApplicationServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, ExportApplicationServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal exportApplicationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectExportApplication", "ExportApplicationId", exportApplicationId, GetType(EntitySet.ExportApplicationSet), tran),Entity.ExportApplication)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal exportApplicationId As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(exportApplicationId, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal exportApplicationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.ExportApplication
            Return CType(MyBase.GetById("eosp_SelectExportApplication", "ExportApplicationId", exportApplicationId, GetType(EntitySet.ExportApplicationSet), tran),Entity.ExportApplication)
        End Function
        
        Public Overloads Function GetById(ByVal exportApplicationId As Integer) As Entity.ExportApplication
            Return Me.GetById(exportApplicationId, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal exportApplicationId As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(exportApplicationId, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal exportApplicationId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeleteExportApplication", "ExportApplicationId", exportApplicationId, checkSum, transaction)
        End Function
        
        'GetForCITESApplication - links to the CITESApplication table...
        Public Overloads Function GetForCITESApplication(ByVal CitesApplicationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ExportApplicationSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from ExportApplication where CitesApplic"& _ 
"ationId=" + CitesApplicationId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.ExportApplicationSet), tran),EntitySet.ExportApplicationSet)
        End Function
        
        'GetForCITESApplication - links to the CITESApplication table...
        Public Overloads Function GetForCITESApplication(ByVal CitesApplicationId As Integer) As EntitySet.ExportApplicationSet
            Return Me.GetForCITESApplication(CitesApplicationId, Nothing)
        End Function
        
        'GetForCountry - links to the Country table...
        Public Overloads Function GetForCountry(ByVal CountryId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ExportApplicationSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from ExportApplication where CountryOfEx"& _ 
"portId=" + CountryId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.ExportApplicationSet), tran),EntitySet.ExportApplicationSet)
        End Function
        
        'GetForCountry - links to the Country table...
        Public Overloads Function GetForCountry(ByVal CountryId As Integer) As EntitySet.ExportApplicationSet
            Return Me.GetForCountry(CountryId, Nothing)
        End Function
        
        'GetForRegionOfExportIdUKCountry - links to the UKCountry table...
        Public Overloads Function GetForRegionOfExportIdUKCountry(ByVal RegionOfExportId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ExportApplicationSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from ExportApplication where RegionOfExp"& _ 
"ortId=" + RegionOfExportId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.ExportApplicationSet), tran),EntitySet.ExportApplicationSet)
        End Function
        
        'GetForRegionOfExportIdUKCountry - links to the UKCountry table...
        Public Overloads Function GetForRegionOfExportIdUKCountry(ByVal RegionOfExportId As Integer) As EntitySet.ExportApplicationSet
            Return Me.GetForRegionOfExportIdUKCountry(RegionOfExportId, Nothing)
        End Function
        
        'GetForRegionOfImportIdUKCountry - links to the UKCountry table...
        Public Overloads Function GetForRegionOfImportIdUKCountry(ByVal RegionOfImportId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ExportApplicationSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from ExportApplication where RegionOfImp"& _ 
"ortId=" + RegionOfImportId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.ExportApplicationSet), tran),EntitySet.ExportApplicationSet)
        End Function
        
        'GetForRegionOfImportIdUKCountry - links to the UKCountry table...
        Public Overloads Function GetForRegionOfImportIdUKCountry(ByVal RegionOfImportId As Integer) As EntitySet.ExportApplicationSet
            Return Me.GetForRegionOfImportIdUKCountry(RegionOfImportId, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal citesApplicationId As Integer, ByVal countryOfExportId As Object, ByVal reExport As Boolean, ByVal regionOfExportId As Object, ByVal regionOfImportId As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ExportApplication
            Return Me.GetById(Sprocs.eosp_CreateExportApplication(citesApplicationId, countryOfExportId, reExport, regionOfExportId, regionOfImportId, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal citesApplicationId As Integer, ByVal countryOfExportId As Object, ByVal reExport As Boolean, ByVal regionOfExportId As Object, ByVal regionOfImportId As Object) As Entity.ExportApplication
            Return Me.Insert(citesApplicationId, countryOfExportId, reExport, regionOfExportId, regionOfImportId, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal exportApplication As Entity.ExportApplication) As Entity.ExportApplication
            Return Me.Insert(exportApplication(1), exportApplication(2), exportApplication(3), exportApplication(4), exportApplication(5))
        End Function
        
        Public Overloads Function Insert(ByVal exportApplication As Entity.ExportApplication, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ExportApplication
            Return Me.Insert(exportApplication(1), exportApplication(2), exportApplication(3), exportApplication(4), exportApplication(5), transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal citesApplicationId As Integer, ByVal countryOfExportId As Object, ByVal reExport As Boolean, ByVal regionOfExportId As Object, ByVal regionOfImportId As Object, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ExportApplication
            Return Sprocs.eosp_UpdateExportApplication(id, citesApplicationId, countryOfExportId, reExport, regionOfExportId, regionOfImportId, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal citesApplicationId As Integer, ByVal countryOfExportId As Object, ByVal reExport As Boolean, ByVal regionOfExportId As Object, ByVal regionOfImportId As Object) As Entity.ExportApplication
            Return Me.Update(id, citesApplicationId, countryOfExportId, reExport, regionOfExportId, regionOfImportId, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal citesApplicationId As Integer, ByVal countryOfExportId As Object, ByVal reExport As Boolean, ByVal regionOfExportId As Object, ByVal regionOfImportId As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ExportApplication
            Return Me.Update(id, citesApplicationId, countryOfExportId, reExport, regionOfExportId, regionOfImportId, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal citesApplicationId As Integer, ByVal countryOfExportId As Object, ByVal reExport As Boolean, ByVal regionOfExportId As Object, ByVal regionOfImportId As Object, ByVal checkSum As Integer) As Entity.ExportApplication
            Return Me.Update(id, citesApplicationId, countryOfExportId, reExport, regionOfExportId, regionOfImportId, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal exportApplication As Entity.ExportApplication) As Entity.ExportApplication
            Return Me.Update(exportApplication.id, exportApplication(1), exportApplication(2), exportApplication(3), exportApplication(4), exportApplication(5))
        End Function
        
        Public Overloads Function Update(ByVal exportApplication As Entity.ExportApplication, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ExportApplication
            Return Me.Update(exportApplication.id, exportApplication(1), exportApplication(2), exportApplication(3), exportApplication(4), exportApplication(5), transaction)
        End Function
        
        Public Overloads Function Update(ByVal exportApplication As Entity.ExportApplication, ByVal checkSum As Integer) As Entity.ExportApplication
            Return Me.Update(exportApplication.id, exportApplication(1), exportApplication(2), exportApplication(3), exportApplication(4), exportApplication(5), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal exportApplication As Entity.ExportApplication, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ExportApplication
            Return Me.Update(exportApplication.id, exportApplication(1), exportApplication(2), exportApplication(3), exportApplication(4), exportApplication(5), checkSum, transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
        End Enum
    End Class
End Namespace
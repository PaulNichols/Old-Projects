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
    
    'Service base implementation for table 'Country'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class CountryServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return true
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.CountrySet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.CountrySet
            Return CType(MyBase.GetAll("eosp_SelectCountry", GetType(EntitySet.CountrySet), includeHyphen, includeInactive, orderBy),EntitySet.CountrySet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.CountrySet
            Return Me.GetAll(includeHyphen, includeInactive, CountryServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, CountryServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal orderBy As OrderBy) As EntitySet.CountrySet
            Return Me.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal countryId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectCountry", "CountryId", countryId, GetType(EntitySet.CountrySet), tran),Entity.Country)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal countryId As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(countryId, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal countryId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.Country
            Return CType(MyBase.GetById("eosp_SelectCountry", "CountryId", countryId, GetType(EntitySet.CountrySet), tran),Entity.Country)
        End Function
        
        Public Overloads Function GetById(ByVal countryId As Integer) As Entity.Country
            Return Me.GetById(countryId, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal countryId As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(countryId, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal countryId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeleteCountry", "CountryId", countryId, checkSum, transaction)
        End Function
        
        'GetForCountry - links to the Country table...
        Public Overloads Function GetForCountry(ByVal CountryId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CountrySet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from Country where ManagementCountryId=" + CountryId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.CountrySet), tran),EntitySet.CountrySet)
        End Function
        
        'GetForCountry - links to the Country table...
        Public Overloads Function GetForCountry(ByVal CountryId As Integer) As EntitySet.CountrySet
            Return Me.GetForCountry(CountryId, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal iSO2CountryCode As Object, ByVal iSO3CountryCode As Object, ByVal shortName As String, ByVal longName As String, ByVal active As Boolean, ByVal countryBRU As Boolean, ByVal iSO3166 As Boolean, ByVal managementCountryId As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Country
            Return Me.GetById(Sprocs.eosp_CreateCountry(iSO2CountryCode, iSO3CountryCode, shortName, longName, active, countryBRU, iSO3166, managementCountryId, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal iSO2CountryCode As Object, ByVal iSO3CountryCode As Object, ByVal shortName As String, ByVal longName As String, ByVal active As Boolean, ByVal countryBRU As Boolean, ByVal iSO3166 As Boolean, ByVal managementCountryId As Object) As Entity.Country
            Return Me.Insert(iSO2CountryCode, iSO3CountryCode, shortName, longName, active, countryBRU, iSO3166, managementCountryId, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal country As Entity.Country) As Entity.Country
            Return Me.Insert(country(1), country(2), country(3), country(4), country(6), country(7), country(8), country(9))
        End Function
        
        Public Overloads Function Insert(ByVal country As Entity.Country, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Country
            Return Me.Insert(country(1), country(2), country(3), country(4), country(6), country(7), country(8), country(9), transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal iSO2CountryCode As Object, ByVal iSO3CountryCode As Object, ByVal shortName As String, ByVal longName As String, ByVal active As Boolean, ByVal countryBRU As Boolean, ByVal iSO3166 As Boolean, ByVal managementCountryId As Object, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Country
            Return Sprocs.eosp_UpdateCountry(id, iSO2CountryCode, iSO3CountryCode, shortName, longName, active, countryBRU, iSO3166, managementCountryId, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal iSO2CountryCode As Object, ByVal iSO3CountryCode As Object, ByVal shortName As String, ByVal longName As String, ByVal active As Boolean, ByVal countryBRU As Boolean, ByVal iSO3166 As Boolean, ByVal managementCountryId As Object) As Entity.Country
            Return Me.Update(id, iSO2CountryCode, iSO3CountryCode, shortName, longName, active, countryBRU, iSO3166, managementCountryId, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal iSO2CountryCode As Object, ByVal iSO3CountryCode As Object, ByVal shortName As String, ByVal longName As String, ByVal active As Boolean, ByVal countryBRU As Boolean, ByVal iSO3166 As Boolean, ByVal managementCountryId As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Country
            Return Me.Update(id, iSO2CountryCode, iSO3CountryCode, shortName, longName, active, countryBRU, iSO3166, managementCountryId, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal iSO2CountryCode As Object, ByVal iSO3CountryCode As Object, ByVal shortName As String, ByVal longName As String, ByVal active As Boolean, ByVal countryBRU As Boolean, ByVal iSO3166 As Boolean, ByVal managementCountryId As Object, ByVal checkSum As Integer) As Entity.Country
            Return Me.Update(id, iSO2CountryCode, iSO3CountryCode, shortName, longName, active, countryBRU, iSO3166, managementCountryId, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal country As Entity.Country) As Entity.Country
            Return Me.Update(country.id, country(1), country(2), country(3), country(4), country(6), country(7), country(8), country(9))
        End Function
        
        Public Overloads Function Update(ByVal country As Entity.Country, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Country
            Return Me.Update(country.id, country(1), country(2), country(3), country(4), country(6), country(7), country(8), country(9), transaction)
        End Function
        
        Public Overloads Function Update(ByVal country As Entity.Country, ByVal checkSum As Integer) As Entity.Country
            Return Me.Update(country.id, country(1), country(2), country(3), country(4), country(6), country(7), country(8), country(9), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal country As Entity.Country, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Country
            Return Me.Update(country.id, country(1), country(2), country(3), country(4), country(6), country(7), country(8), country(9), checkSum, transaction)
        End Function
        
        Public Overloads Function GetByIndex_IX_Country(ByVal iSO2CountryCode As String, ByVal includeInactive As Boolean) As EntitySet.CountrySet
            Return Sprocs.eosp_SelectCountry(countryId:=Nothing, Index_ISO2CountryCode:=[iSO2CountryCode], Index_ManagementCountryId:=Nothing, Index_ISO3CountryCode:=Nothing, Index_CodeDescription:=Nothing, includeInactive:=includeInactive, transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_IX_Country(ByVal iSO2CountryCode As String, ByVal includeInactive As Boolean, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.CountrySet
            Return Sprocs.eosp_SelectCountry(countryId:=Nothing, Index_ISO2CountryCode:=[iSO2CountryCode], Index_ManagementCountryId:=Nothing, Index_ISO3CountryCode:=Nothing, Index_CodeDescription:=Nothing, includeInactive:=includeInactive, sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Overloads Function GetByIndex_IX_Country_1(ByVal managementCountryId As Integer, ByVal includeInactive As Boolean) As EntitySet.CountrySet
            Return Sprocs.eosp_SelectCountry(countryId:=Nothing, Index_ManagementCountryId:=[managementCountryId], Index_ISO2CountryCode:=Nothing, Index_ISO3CountryCode:=Nothing, Index_CodeDescription:=Nothing, includeInactive:=includeInactive, transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_IX_Country_1(ByVal managementCountryId As Integer, ByVal includeInactive As Boolean, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.CountrySet
            Return Sprocs.eosp_SelectCountry(countryId:=Nothing, Index_ManagementCountryId:=[managementCountryId], Index_ISO2CountryCode:=Nothing, Index_ISO3CountryCode:=Nothing, Index_CodeDescription:=Nothing, includeInactive:=includeInactive, sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Overloads Function GetByIndex_IX_Country_2(ByVal iSO3CountryCode As String, ByVal includeInactive As Boolean) As EntitySet.CountrySet
            Return Sprocs.eosp_SelectCountry(countryId:=Nothing, Index_ISO3CountryCode:=[iSO3CountryCode], Index_ISO2CountryCode:=Nothing, Index_ManagementCountryId:=Nothing, Index_CodeDescription:=Nothing, includeInactive:=includeInactive, transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_IX_Country_2(ByVal iSO3CountryCode As String, ByVal includeInactive As Boolean, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.CountrySet
            Return Sprocs.eosp_SelectCountry(countryId:=Nothing, Index_ISO3CountryCode:=[iSO3CountryCode], Index_ISO2CountryCode:=Nothing, Index_ManagementCountryId:=Nothing, Index_CodeDescription:=Nothing, includeInactive:=includeInactive, sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Overloads Function GetByIndex_CodeDescription(ByVal codeDescription As String, ByVal includeInactive As Boolean) As EntitySet.CountrySet
            Return Sprocs.eosp_SelectCountry(countryId:=Nothing, Index_CodeDescription:=[codeDescription], Index_ISO2CountryCode:=Nothing, Index_ManagementCountryId:=Nothing, Index_ISO3CountryCode:=Nothing, includeInactive:=includeInactive, transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_CodeDescription(ByVal codeDescription As String, ByVal includeInactive As Boolean, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.CountrySet
            Return Sprocs.eosp_SelectCountry(countryId:=Nothing, Index_CodeDescription:=[codeDescription], Index_ISO2CountryCode:=Nothing, Index_ManagementCountryId:=Nothing, Index_ISO3CountryCode:=Nothing, includeInactive:=includeInactive, sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
            
            IX_Country
            
            IX_Country_1
            
            IX_Country_2
            
            CodeDescription
            
            
        End Enum
    End Class
End Namespace

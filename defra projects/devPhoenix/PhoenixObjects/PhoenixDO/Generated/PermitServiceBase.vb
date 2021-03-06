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
    
    'Service base implementation for table 'Permit'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class PermitServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.PermitSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.PermitSet
            Return CType(MyBase.GetAll("eosp_SelectPermit", GetType(EntitySet.PermitSet), includeHyphen, includeInactive, orderBy),EntitySet.PermitSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.PermitSet
            Return Me.GetAll(includeHyphen, includeInactive, PermitServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, PermitServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal orderBy As OrderBy) As EntitySet.PermitSet
            Return Me.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal permitId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectPermit", "PermitId", permitId, GetType(EntitySet.PermitSet), tran),Entity.Permit)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal permitId As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(permitId, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal permitId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.Permit
            Return CType(MyBase.GetById("eosp_SelectPermit", "PermitId", permitId, GetType(EntitySet.PermitSet), tran),Entity.Permit)
        End Function
        
        Public Overloads Function GetById(ByVal permitId As Integer) As Entity.Permit
            Return Me.GetById(permitId, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal permitId As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(permitId, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal permitId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeletePermit", "PermitId", permitId, checkSum, transaction)
        End Function
        
        'GetForCountry - links to the Country table...
        Public Overloads Function GetForCountry(ByVal CountryId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from Permit where CountryOfOriginId=" + CountryId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.PermitSet), tran),EntitySet.PermitSet)
        End Function
        
        'GetForCountry - links to the Country table...
        Public Overloads Function GetForCountry(ByVal CountryId As Integer) As EntitySet.PermitSet
            Return Me.GetForCountry(CountryId, Nothing)
        End Function
        
        'GetForSpecie - links to the Specie table...
        Public Overloads Function GetForSpecie(ByVal SpecieId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from Permit where SpecieId=" + SpecieId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.PermitSet), tran),EntitySet.PermitSet)
        End Function
        
        'GetForSpecie - links to the Specie table...
        Public Overloads Function GetForSpecie(ByVal SpecieId As Integer) As EntitySet.PermitSet
            Return Me.GetForSpecie(SpecieId, Nothing)
        End Function
        
        'GetForApplication - links to the Application table...
        Public Overloads Function GetForApplication(ByVal ApplicationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from Permit where ApplicationId=" + ApplicationId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.PermitSet), tran),EntitySet.PermitSet)
        End Function
        
        'GetForApplication - links to the Application table...
        Public Overloads Function GetForApplication(ByVal ApplicationId As Integer) As EntitySet.PermitSet
            Return Me.GetForApplication(ApplicationId, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal countryOfOriginId As Object, ByVal countryOfOriginPermitDate As Object, ByVal countryOfOriginPermitNumber As Object, ByVal permitDate As Object, ByVal description As Object, ByVal numberOfCopies As Object, ByVal specieId As Object, ByVal expiryDate As Object, ByVal applicationId As Integer, ByVal permitNumber As Integer, ByVal kewAdvice As Object, ByVal jNCCAdvice As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Permit
            Return Me.GetById(Sprocs.eosp_CreatePermit(countryOfOriginId, countryOfOriginPermitDate, countryOfOriginPermitNumber, permitDate, description, numberOfCopies, specieId, expiryDate, applicationId, permitNumber, kewAdvice, jNCCAdvice, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal countryOfOriginId As Object, ByVal countryOfOriginPermitDate As Object, ByVal countryOfOriginPermitNumber As Object, ByVal permitDate As Object, ByVal description As Object, ByVal numberOfCopies As Object, ByVal specieId As Object, ByVal expiryDate As Object, ByVal applicationId As Integer, ByVal permitNumber As Integer, ByVal kewAdvice As Object, ByVal jNCCAdvice As Object) As Entity.Permit
            Return Me.Insert(countryOfOriginId, countryOfOriginPermitDate, countryOfOriginPermitNumber, permitDate, description, numberOfCopies, specieId, expiryDate, applicationId, permitNumber, kewAdvice, jNCCAdvice, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal permit As Entity.Permit) As Entity.Permit
            Return Me.Insert(permit(1), permit(2), permit(3), permit(4), permit(5), permit(6), permit(7), permit(8), permit(9), permit(10), permit(11), permit(12))
        End Function
        
        Public Overloads Function Insert(ByVal permit As Entity.Permit, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Permit
            Return Me.Insert(permit(1), permit(2), permit(3), permit(4), permit(5), permit(6), permit(7), permit(8), permit(9), permit(10), permit(11), permit(12), transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal countryOfOriginId As Object, ByVal countryOfOriginPermitDate As Object, ByVal countryOfOriginPermitNumber As Object, ByVal permitDate As Object, ByVal description As Object, ByVal numberOfCopies As Object, ByVal specieId As Object, ByVal expiryDate As Object, ByVal applicationId As Integer, ByVal permitNumber As Integer, ByVal kewAdvice As Object, ByVal jNCCAdvice As Object, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Permit
            Return Sprocs.eosp_UpdatePermit(id, countryOfOriginId, countryOfOriginPermitDate, countryOfOriginPermitNumber, permitDate, description, numberOfCopies, specieId, expiryDate, applicationId, permitNumber, kewAdvice, jNCCAdvice, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal countryOfOriginId As Object, ByVal countryOfOriginPermitDate As Object, ByVal countryOfOriginPermitNumber As Object, ByVal permitDate As Object, ByVal description As Object, ByVal numberOfCopies As Object, ByVal specieId As Object, ByVal expiryDate As Object, ByVal applicationId As Integer, ByVal permitNumber As Integer, ByVal kewAdvice As Object, ByVal jNCCAdvice As Object) As Entity.Permit
            Return Me.Update(id, countryOfOriginId, countryOfOriginPermitDate, countryOfOriginPermitNumber, permitDate, description, numberOfCopies, specieId, expiryDate, applicationId, permitNumber, kewAdvice, jNCCAdvice, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal countryOfOriginId As Object, ByVal countryOfOriginPermitDate As Object, ByVal countryOfOriginPermitNumber As Object, ByVal permitDate As Object, ByVal description As Object, ByVal numberOfCopies As Object, ByVal specieId As Object, ByVal expiryDate As Object, ByVal applicationId As Integer, ByVal permitNumber As Integer, ByVal kewAdvice As Object, ByVal jNCCAdvice As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Permit
            Return Me.Update(id, countryOfOriginId, countryOfOriginPermitDate, countryOfOriginPermitNumber, permitDate, description, numberOfCopies, specieId, expiryDate, applicationId, permitNumber, kewAdvice, jNCCAdvice, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal countryOfOriginId As Object, ByVal countryOfOriginPermitDate As Object, ByVal countryOfOriginPermitNumber As Object, ByVal permitDate As Object, ByVal description As Object, ByVal numberOfCopies As Object, ByVal specieId As Object, ByVal expiryDate As Object, ByVal applicationId As Integer, ByVal permitNumber As Integer, ByVal kewAdvice As Object, ByVal jNCCAdvice As Object, ByVal checkSum As Integer) As Entity.Permit
            Return Me.Update(id, countryOfOriginId, countryOfOriginPermitDate, countryOfOriginPermitNumber, permitDate, description, numberOfCopies, specieId, expiryDate, applicationId, permitNumber, kewAdvice, jNCCAdvice, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal permit As Entity.Permit) As Entity.Permit
            Return Me.Update(permit.id, permit(1), permit(2), permit(3), permit(4), permit(5), permit(6), permit(7), permit(8), permit(9), permit(10), permit(11), permit(12))
        End Function
        
        Public Overloads Function Update(ByVal permit As Entity.Permit, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Permit
            Return Me.Update(permit.id, permit(1), permit(2), permit(3), permit(4), permit(5), permit(6), permit(7), permit(8), permit(9), permit(10), permit(11), permit(12), transaction)
        End Function
        
        Public Overloads Function Update(ByVal permit As Entity.Permit, ByVal checkSum As Integer) As Entity.Permit
            Return Me.Update(permit.id, permit(1), permit(2), permit(3), permit(4), permit(5), permit(6), permit(7), permit(8), permit(9), permit(10), permit(11), permit(12), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal permit As Entity.Permit, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Permit
            Return Me.Update(permit.id, permit(1), permit(2), permit(3), permit(4), permit(5), permit(6), permit(7), permit(8), permit(9), permit(10), permit(11), permit(12), checkSum, transaction)
        End Function
        
        Public Overloads Function GetByIndex_IX_Permit(ByVal permitNumber As Integer, ByVal applicationId As Integer) As EntitySet.PermitSet
            Return Sprocs.eosp_SelectPermit(permitId:=Nothing, Index_PermitNumber:=[permitNumber], Index_ApplicationId:=[applicationId], transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_IX_Permit(ByVal permitNumber As Integer, ByVal applicationId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitSet
            Return Sprocs.eosp_SelectPermit(permitId:=Nothing, Index_PermitNumber:=[permitNumber], Index_ApplicationId:=[applicationId], sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Overloads Function GetByIndex_ApplicationId(ByVal applicationId As Integer) As EntitySet.PermitSet
            Return Sprocs.eosp_SelectPermit(permitId:=Nothing, Index_ApplicationId:=[applicationId], Index_PermitNumber:=Nothing, transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_ApplicationId(ByVal applicationId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitSet
            Return Sprocs.eosp_SelectPermit(permitId:=Nothing, Index_ApplicationId:=[applicationId], Index_PermitNumber:=Nothing, sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
            
            IX_Permit
            
            ApplicationId
            
            
        End Enum
    End Class
End Namespace

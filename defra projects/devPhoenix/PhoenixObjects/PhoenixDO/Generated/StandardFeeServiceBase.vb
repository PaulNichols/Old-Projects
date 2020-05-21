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
    
    'Service base implementation for table 'StandardFee'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class StandardFeeServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return true
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.StandardFeeSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.StandardFeeSet
            Return CType(MyBase.GetAll("eosp_SelectStandardFee", GetType(EntitySet.StandardFeeSet), includeHyphen, includeInactive, orderBy),EntitySet.StandardFeeSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.StandardFeeSet
            Return Me.GetAll(includeHyphen, includeInactive, StandardFeeServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, StandardFeeServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal orderBy As OrderBy) As EntitySet.StandardFeeSet
            Return Me.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal standardFeeID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectStandardFee", "StandardFeeID", standardFeeID, GetType(EntitySet.StandardFeeSet), tran),Entity.StandardFee)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal standardFeeID As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(standardFeeID, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal standardFeeID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.StandardFee
            Return CType(MyBase.GetById("eosp_SelectStandardFee", "StandardFeeID", standardFeeID, GetType(EntitySet.StandardFeeSet), tran),Entity.StandardFee)
        End Function
        
        Public Overloads Function GetById(ByVal standardFeeID As Integer) As Entity.StandardFee
            Return Me.GetById(standardFeeID, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal standardFeeID As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(standardFeeID, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal standardFeeID As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeleteStandardFee", "StandardFeeID", standardFeeID, checkSum, transaction)
        End Function
        
        Public Overloads Function Insert(ByVal applicationTypeCode As Integer, ByVal linkedApplicationTypeCode As Object, ByVal plantOrCoral As Integer, ByVal commercialPurpose As Integer, ByVal birdFeeLevel As Short, ByVal minimumNumberOfSpecies As Integer, ByVal maximumNumberOfSpecies As Object, ByVal fee As Decimal, ByVal active As Boolean, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.StandardFee
            Return Me.GetById(Sprocs.eosp_CreateStandardFee(applicationTypeCode, linkedApplicationTypeCode, plantOrCoral, commercialPurpose, birdFeeLevel, minimumNumberOfSpecies, maximumNumberOfSpecies, fee, active, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal applicationTypeCode As Integer, ByVal linkedApplicationTypeCode As Object, ByVal plantOrCoral As Integer, ByVal commercialPurpose As Integer, ByVal birdFeeLevel As Short, ByVal minimumNumberOfSpecies As Integer, ByVal maximumNumberOfSpecies As Object, ByVal fee As Decimal, ByVal active As Boolean) As Entity.StandardFee
            Return Me.Insert(applicationTypeCode, linkedApplicationTypeCode, plantOrCoral, commercialPurpose, birdFeeLevel, minimumNumberOfSpecies, maximumNumberOfSpecies, fee, active, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal standardFee As Entity.StandardFee) As Entity.StandardFee
            Return Me.Insert(standardFee(1), standardFee(2), standardFee(3), standardFee(4), standardFee(5), standardFee(6), standardFee(7), standardFee(8), standardFee(9))
        End Function
        
        Public Overloads Function Insert(ByVal standardFee As Entity.StandardFee, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.StandardFee
            Return Me.Insert(standardFee(1), standardFee(2), standardFee(3), standardFee(4), standardFee(5), standardFee(6), standardFee(7), standardFee(8), standardFee(9), transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal applicationTypeCode As Integer, ByVal linkedApplicationTypeCode As Object, ByVal plantOrCoral As Integer, ByVal commercialPurpose As Integer, ByVal birdFeeLevel As Short, ByVal minimumNumberOfSpecies As Integer, ByVal maximumNumberOfSpecies As Object, ByVal fee As Decimal, ByVal active As Boolean, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.StandardFee
            Return Sprocs.eosp_UpdateStandardFee(id, applicationTypeCode, linkedApplicationTypeCode, plantOrCoral, commercialPurpose, birdFeeLevel, minimumNumberOfSpecies, maximumNumberOfSpecies, fee, active, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal applicationTypeCode As Integer, ByVal linkedApplicationTypeCode As Object, ByVal plantOrCoral As Integer, ByVal commercialPurpose As Integer, ByVal birdFeeLevel As Short, ByVal minimumNumberOfSpecies As Integer, ByVal maximumNumberOfSpecies As Object, ByVal fee As Decimal, ByVal active As Boolean) As Entity.StandardFee
            Return Me.Update(id, applicationTypeCode, linkedApplicationTypeCode, plantOrCoral, commercialPurpose, birdFeeLevel, minimumNumberOfSpecies, maximumNumberOfSpecies, fee, active, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal applicationTypeCode As Integer, ByVal linkedApplicationTypeCode As Object, ByVal plantOrCoral As Integer, ByVal commercialPurpose As Integer, ByVal birdFeeLevel As Short, ByVal minimumNumberOfSpecies As Integer, ByVal maximumNumberOfSpecies As Object, ByVal fee As Decimal, ByVal active As Boolean, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.StandardFee
            Return Me.Update(id, applicationTypeCode, linkedApplicationTypeCode, plantOrCoral, commercialPurpose, birdFeeLevel, minimumNumberOfSpecies, maximumNumberOfSpecies, fee, active, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal applicationTypeCode As Integer, ByVal linkedApplicationTypeCode As Object, ByVal plantOrCoral As Integer, ByVal commercialPurpose As Integer, ByVal birdFeeLevel As Short, ByVal minimumNumberOfSpecies As Integer, ByVal maximumNumberOfSpecies As Object, ByVal fee As Decimal, ByVal active As Boolean, ByVal checkSum As Integer) As Entity.StandardFee
            Return Me.Update(id, applicationTypeCode, linkedApplicationTypeCode, plantOrCoral, commercialPurpose, birdFeeLevel, minimumNumberOfSpecies, maximumNumberOfSpecies, fee, active, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal standardFee As Entity.StandardFee) As Entity.StandardFee
            Return Me.Update(standardFee.id, standardFee(1), standardFee(2), standardFee(3), standardFee(4), standardFee(5), standardFee(6), standardFee(7), standardFee(8), standardFee(9))
        End Function
        
        Public Overloads Function Update(ByVal standardFee As Entity.StandardFee, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.StandardFee
            Return Me.Update(standardFee.id, standardFee(1), standardFee(2), standardFee(3), standardFee(4), standardFee(5), standardFee(6), standardFee(7), standardFee(8), standardFee(9), transaction)
        End Function
        
        Public Overloads Function Update(ByVal standardFee As Entity.StandardFee, ByVal checkSum As Integer) As Entity.StandardFee
            Return Me.Update(standardFee.id, standardFee(1), standardFee(2), standardFee(3), standardFee(4), standardFee(5), standardFee(6), standardFee(7), standardFee(8), standardFee(9), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal standardFee As Entity.StandardFee, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.StandardFee
            Return Me.Update(standardFee.id, standardFee(1), standardFee(2), standardFee(3), standardFee(4), standardFee(5), standardFee(6), standardFee(7), standardFee(8), standardFee(9), checkSum, transaction)
        End Function
        
        Public Overloads Function GetByIndex_IX_StandardFee(ByVal applicationTypeCode As Integer, ByVal linkedApplicationTypeCode As Integer, ByVal plantOrCoral As Integer, ByVal commercialPurpose As Integer, ByVal birdFeeLevel As Short, ByVal minimumNumberOfSpecies As Integer, ByVal includeInactive As Boolean) As EntitySet.StandardFeeSet
            Return Sprocs.eosp_SelectStandardFee(standardFeeID:=Nothing, Index_ApplicationTypeCode:=[applicationTypeCode], Index_LinkedApplicationTypeCode:=[linkedApplicationTypeCode], Index_PlantOrCoral:=[plantOrCoral], Index_CommercialPurpose:=[commercialPurpose], Index_BirdFeeLevel:=[birdFeeLevel], Index_MinimumNumberOfSpecies:=[minimumNumberOfSpecies], includeInactive:=includeInactive, transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_IX_StandardFee(ByVal applicationTypeCode As Integer, ByVal linkedApplicationTypeCode As Integer, ByVal plantOrCoral As Integer, ByVal commercialPurpose As Integer, ByVal birdFeeLevel As Short, ByVal minimumNumberOfSpecies As Integer, ByVal includeInactive As Boolean, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.StandardFeeSet
            Return Sprocs.eosp_SelectStandardFee(standardFeeID:=Nothing, Index_ApplicationTypeCode:=[applicationTypeCode], Index_LinkedApplicationTypeCode:=[linkedApplicationTypeCode], Index_PlantOrCoral:=[plantOrCoral], Index_CommercialPurpose:=[commercialPurpose], Index_BirdFeeLevel:=[birdFeeLevel], Index_MinimumNumberOfSpecies:=[minimumNumberOfSpecies], includeInactive:=includeInactive, sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
            
            IX_StandardFee
            
            
        End Enum
    End Class
End Namespace

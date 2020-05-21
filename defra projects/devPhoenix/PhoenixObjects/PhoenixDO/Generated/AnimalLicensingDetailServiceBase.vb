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
    
    'Service base implementation for table 'AnimalLicensingDetail'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class AnimalLicensingDetailServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.AnimalLicensingDetailSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.AnimalLicensingDetailSet
            Return CType(MyBase.GetAll("eosp_SelectAnimalLicensingDetail", GetType(EntitySet.AnimalLicensingDetailSet), includeHyphen, includeInactive, orderBy),EntitySet.AnimalLicensingDetailSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.AnimalLicensingDetailSet
            Return Me.GetAll(includeHyphen, includeInactive, AnimalLicensingDetailServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, AnimalLicensingDetailServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal orderBy As OrderBy) As EntitySet.AnimalLicensingDetailSet
            Return Me.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal animalLicensingID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectAnimalLicensingDetail", "AnimalLicensingID", animalLicensingID, GetType(EntitySet.AnimalLicensingDetailSet), tran),Entity.AnimalLicensingDetail)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal animalLicensingID As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(animalLicensingID, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal animalLicensingID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.AnimalLicensingDetail
            Return CType(MyBase.GetById("eosp_SelectAnimalLicensingDetail", "AnimalLicensingID", animalLicensingID, GetType(EntitySet.AnimalLicensingDetailSet), tran),Entity.AnimalLicensingDetail)
        End Function
        
        Public Overloads Function GetById(ByVal animalLicensingID As Integer) As Entity.AnimalLicensingDetail
            Return Me.GetById(animalLicensingID, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal animalLicensingID As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(animalLicensingID, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal animalLicensingID As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeleteAnimalLicensingDetail", "AnimalLicensingID", animalLicensingID, checkSum, transaction)
        End Function
        
        Public Overloads Function Insert(ByVal kingdomID As Integer, ByVal taxonID As Integer, ByVal taxonTypeID As Integer, ByVal birdFeeLevel As Object, ByVal incubationOrGestationDays As Object, ByVal averageNumberOfOffspring As Object, ByVal minimumMicrochipSize As Object, ByVal minimumMicrochipAge As Object, ByVal averageLifespan As Object, ByVal oldestAcceptedAge As Object, ByVal sexualMaturityAge As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.AnimalLicensingDetail
            Return Me.GetById(Sprocs.eosp_CreateAnimalLicensingDetail(kingdomID, taxonID, taxonTypeID, birdFeeLevel, incubationOrGestationDays, averageNumberOfOffspring, minimumMicrochipSize, minimumMicrochipAge, averageLifespan, oldestAcceptedAge, sexualMaturityAge, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal kingdomID As Integer, ByVal taxonID As Integer, ByVal taxonTypeID As Integer, ByVal birdFeeLevel As Object, ByVal incubationOrGestationDays As Object, ByVal averageNumberOfOffspring As Object, ByVal minimumMicrochipSize As Object, ByVal minimumMicrochipAge As Object, ByVal averageLifespan As Object, ByVal oldestAcceptedAge As Object, ByVal sexualMaturityAge As Object) As Entity.AnimalLicensingDetail
            Return Me.Insert(kingdomID, taxonID, taxonTypeID, birdFeeLevel, incubationOrGestationDays, averageNumberOfOffspring, minimumMicrochipSize, minimumMicrochipAge, averageLifespan, oldestAcceptedAge, sexualMaturityAge, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal animalLicensingDetail As Entity.AnimalLicensingDetail) As Entity.AnimalLicensingDetail
            Return Me.Insert(animalLicensingDetail(1), animalLicensingDetail(2), animalLicensingDetail(3), animalLicensingDetail(4), animalLicensingDetail(5), animalLicensingDetail(6), animalLicensingDetail(7), animalLicensingDetail(8), animalLicensingDetail(9), animalLicensingDetail(10), animalLicensingDetail(11))
        End Function
        
        Public Overloads Function Insert(ByVal animalLicensingDetail As Entity.AnimalLicensingDetail, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.AnimalLicensingDetail
            Return Me.Insert(animalLicensingDetail(1), animalLicensingDetail(2), animalLicensingDetail(3), animalLicensingDetail(4), animalLicensingDetail(5), animalLicensingDetail(6), animalLicensingDetail(7), animalLicensingDetail(8), animalLicensingDetail(9), animalLicensingDetail(10), animalLicensingDetail(11), transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal kingdomID As Integer, ByVal taxonID As Integer, ByVal taxonTypeID As Integer, ByVal birdFeeLevel As Object, ByVal incubationOrGestationDays As Object, ByVal averageNumberOfOffspring As Object, ByVal minimumMicrochipSize As Object, ByVal minimumMicrochipAge As Object, ByVal averageLifespan As Object, ByVal oldestAcceptedAge As Object, ByVal sexualMaturityAge As Object, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.AnimalLicensingDetail
            Return Sprocs.eosp_UpdateAnimalLicensingDetail(id, kingdomID, taxonID, taxonTypeID, birdFeeLevel, incubationOrGestationDays, averageNumberOfOffspring, minimumMicrochipSize, minimumMicrochipAge, averageLifespan, oldestAcceptedAge, sexualMaturityAge, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal kingdomID As Integer, ByVal taxonID As Integer, ByVal taxonTypeID As Integer, ByVal birdFeeLevel As Object, ByVal incubationOrGestationDays As Object, ByVal averageNumberOfOffspring As Object, ByVal minimumMicrochipSize As Object, ByVal minimumMicrochipAge As Object, ByVal averageLifespan As Object, ByVal oldestAcceptedAge As Object, ByVal sexualMaturityAge As Object) As Entity.AnimalLicensingDetail
            Return Me.Update(id, kingdomID, taxonID, taxonTypeID, birdFeeLevel, incubationOrGestationDays, averageNumberOfOffspring, minimumMicrochipSize, minimumMicrochipAge, averageLifespan, oldestAcceptedAge, sexualMaturityAge, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal kingdomID As Integer, ByVal taxonID As Integer, ByVal taxonTypeID As Integer, ByVal birdFeeLevel As Object, ByVal incubationOrGestationDays As Object, ByVal averageNumberOfOffspring As Object, ByVal minimumMicrochipSize As Object, ByVal minimumMicrochipAge As Object, ByVal averageLifespan As Object, ByVal oldestAcceptedAge As Object, ByVal sexualMaturityAge As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.AnimalLicensingDetail
            Return Me.Update(id, kingdomID, taxonID, taxonTypeID, birdFeeLevel, incubationOrGestationDays, averageNumberOfOffspring, minimumMicrochipSize, minimumMicrochipAge, averageLifespan, oldestAcceptedAge, sexualMaturityAge, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal kingdomID As Integer, ByVal taxonID As Integer, ByVal taxonTypeID As Integer, ByVal birdFeeLevel As Object, ByVal incubationOrGestationDays As Object, ByVal averageNumberOfOffspring As Object, ByVal minimumMicrochipSize As Object, ByVal minimumMicrochipAge As Object, ByVal averageLifespan As Object, ByVal oldestAcceptedAge As Object, ByVal sexualMaturityAge As Object, ByVal checkSum As Integer) As Entity.AnimalLicensingDetail
            Return Me.Update(id, kingdomID, taxonID, taxonTypeID, birdFeeLevel, incubationOrGestationDays, averageNumberOfOffspring, minimumMicrochipSize, minimumMicrochipAge, averageLifespan, oldestAcceptedAge, sexualMaturityAge, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal animalLicensingDetail As Entity.AnimalLicensingDetail) As Entity.AnimalLicensingDetail
            Return Me.Update(animalLicensingDetail.id, animalLicensingDetail(1), animalLicensingDetail(2), animalLicensingDetail(3), animalLicensingDetail(4), animalLicensingDetail(5), animalLicensingDetail(6), animalLicensingDetail(7), animalLicensingDetail(8), animalLicensingDetail(9), animalLicensingDetail(10), animalLicensingDetail(11))
        End Function
        
        Public Overloads Function Update(ByVal animalLicensingDetail As Entity.AnimalLicensingDetail, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.AnimalLicensingDetail
            Return Me.Update(animalLicensingDetail.id, animalLicensingDetail(1), animalLicensingDetail(2), animalLicensingDetail(3), animalLicensingDetail(4), animalLicensingDetail(5), animalLicensingDetail(6), animalLicensingDetail(7), animalLicensingDetail(8), animalLicensingDetail(9), animalLicensingDetail(10), animalLicensingDetail(11), transaction)
        End Function
        
        Public Overloads Function Update(ByVal animalLicensingDetail As Entity.AnimalLicensingDetail, ByVal checkSum As Integer) As Entity.AnimalLicensingDetail
            Return Me.Update(animalLicensingDetail.id, animalLicensingDetail(1), animalLicensingDetail(2), animalLicensingDetail(3), animalLicensingDetail(4), animalLicensingDetail(5), animalLicensingDetail(6), animalLicensingDetail(7), animalLicensingDetail(8), animalLicensingDetail(9), animalLicensingDetail(10), animalLicensingDetail(11), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal animalLicensingDetail As Entity.AnimalLicensingDetail, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.AnimalLicensingDetail
            Return Me.Update(animalLicensingDetail.id, animalLicensingDetail(1), animalLicensingDetail(2), animalLicensingDetail(3), animalLicensingDetail(4), animalLicensingDetail(5), animalLicensingDetail(6), animalLicensingDetail(7), animalLicensingDetail(8), animalLicensingDetail(9), animalLicensingDetail(10), animalLicensingDetail(11), checkSum, transaction)
        End Function
        
        Public Overloads Function GetByIndex_IX_TaxonomyAnimalLicensing_2(ByVal kingdomID As Integer, ByVal taxonID As Integer, ByVal taxonTypeID As Integer) As EntitySet.AnimalLicensingDetailSet
            Return Sprocs.eosp_SelectAnimalLicensingDetail(animalLicensingID:=Nothing, Index_KingdomID:=[kingdomID], Index_TaxonID:=[taxonID], Index_TaxonTypeID:=[taxonTypeID], transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_IX_TaxonomyAnimalLicensing_2(ByVal kingdomID As Integer, ByVal taxonID As Integer, ByVal taxonTypeID As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.AnimalLicensingDetailSet
            Return Sprocs.eosp_SelectAnimalLicensingDetail(animalLicensingID:=Nothing, Index_KingdomID:=[kingdomID], Index_TaxonID:=[taxonID], Index_TaxonTypeID:=[taxonTypeID], sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
            
            IX_TaxonomyAnimalLicensing_2
            
            
        End Enum
    End Class
End Namespace

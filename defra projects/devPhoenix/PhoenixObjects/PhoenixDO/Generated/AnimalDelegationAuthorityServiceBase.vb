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
    
    'Service base implementation for table 'AnimalDelegationAuthority'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class AnimalDelegationAuthorityServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.AnimalDelegationAuthoritySet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.AnimalDelegationAuthoritySet
            Return CType(MyBase.GetAll("eosp_SelectAnimalDelegationAuthority", GetType(EntitySet.AnimalDelegationAuthoritySet), includeHyphen, includeInactive, orderBy),EntitySet.AnimalDelegationAuthoritySet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.AnimalDelegationAuthoritySet
            Return Me.GetAll(includeHyphen, includeInactive, AnimalDelegationAuthorityServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, AnimalDelegationAuthorityServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal orderBy As OrderBy) As EntitySet.AnimalDelegationAuthoritySet
            Return Me.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal animalDelegationAuthorityID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectAnimalDelegationAuthority", "AnimalDelegationAuthorityID", animalDelegationAuthorityID, GetType(EntitySet.AnimalDelegationAuthoritySet), tran),Entity.AnimalDelegationAuthority)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal animalDelegationAuthorityID As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(animalDelegationAuthorityID, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal animalDelegationAuthorityID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.AnimalDelegationAuthority
            Return CType(MyBase.GetById("eosp_SelectAnimalDelegationAuthority", "AnimalDelegationAuthorityID", animalDelegationAuthorityID, GetType(EntitySet.AnimalDelegationAuthoritySet), tran),Entity.AnimalDelegationAuthority)
        End Function
        
        Public Overloads Function GetById(ByVal animalDelegationAuthorityID As Integer) As Entity.AnimalDelegationAuthority
            Return Me.GetById(animalDelegationAuthorityID, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal animalDelegationAuthorityID As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(animalDelegationAuthorityID, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal animalDelegationAuthorityID As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeleteAnimalDelegationAuthority", "AnimalDelegationAuthorityID", animalDelegationAuthorityID, checkSum, transaction)
        End Function
        
        Public Overloads Function Insert(ByVal delegationCode As Integer, ByVal applicationTypeID As Integer, ByVal speciesKingdomID As Integer, ByVal speciesTaxonomyID As Integer, ByVal speciesTaxonTypeID As Integer, ByVal hyperlinkRTARoadMap As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.AnimalDelegationAuthority
            Return Me.GetById(Sprocs.eosp_CreateAnimalDelegationAuthority(delegationCode, applicationTypeID, speciesKingdomID, speciesTaxonomyID, speciesTaxonTypeID, hyperlinkRTARoadMap, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal delegationCode As Integer, ByVal applicationTypeID As Integer, ByVal speciesKingdomID As Integer, ByVal speciesTaxonomyID As Integer, ByVal speciesTaxonTypeID As Integer, ByVal hyperlinkRTARoadMap As Object) As Entity.AnimalDelegationAuthority
            Return Me.Insert(delegationCode, applicationTypeID, speciesKingdomID, speciesTaxonomyID, speciesTaxonTypeID, hyperlinkRTARoadMap, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal animalDelegationAuthority As Entity.AnimalDelegationAuthority) As Entity.AnimalDelegationAuthority
            Return Me.Insert(animalDelegationAuthority(1), animalDelegationAuthority(2), animalDelegationAuthority(3), animalDelegationAuthority(4), animalDelegationAuthority(5), animalDelegationAuthority(6))
        End Function
        
        Public Overloads Function Insert(ByVal animalDelegationAuthority As Entity.AnimalDelegationAuthority, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.AnimalDelegationAuthority
            Return Me.Insert(animalDelegationAuthority(1), animalDelegationAuthority(2), animalDelegationAuthority(3), animalDelegationAuthority(4), animalDelegationAuthority(5), animalDelegationAuthority(6), transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal delegationCode As Integer, ByVal applicationTypeID As Integer, ByVal speciesKingdomID As Integer, ByVal speciesTaxonomyID As Integer, ByVal speciesTaxonTypeID As Integer, ByVal hyperlinkRTARoadMap As Object, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.AnimalDelegationAuthority
            Return Sprocs.eosp_UpdateAnimalDelegationAuthority(id, delegationCode, applicationTypeID, speciesKingdomID, speciesTaxonomyID, speciesTaxonTypeID, hyperlinkRTARoadMap, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal delegationCode As Integer, ByVal applicationTypeID As Integer, ByVal speciesKingdomID As Integer, ByVal speciesTaxonomyID As Integer, ByVal speciesTaxonTypeID As Integer, ByVal hyperlinkRTARoadMap As Object) As Entity.AnimalDelegationAuthority
            Return Me.Update(id, delegationCode, applicationTypeID, speciesKingdomID, speciesTaxonomyID, speciesTaxonTypeID, hyperlinkRTARoadMap, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal delegationCode As Integer, ByVal applicationTypeID As Integer, ByVal speciesKingdomID As Integer, ByVal speciesTaxonomyID As Integer, ByVal speciesTaxonTypeID As Integer, ByVal hyperlinkRTARoadMap As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.AnimalDelegationAuthority
            Return Me.Update(id, delegationCode, applicationTypeID, speciesKingdomID, speciesTaxonomyID, speciesTaxonTypeID, hyperlinkRTARoadMap, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal delegationCode As Integer, ByVal applicationTypeID As Integer, ByVal speciesKingdomID As Integer, ByVal speciesTaxonomyID As Integer, ByVal speciesTaxonTypeID As Integer, ByVal hyperlinkRTARoadMap As Object, ByVal checkSum As Integer) As Entity.AnimalDelegationAuthority
            Return Me.Update(id, delegationCode, applicationTypeID, speciesKingdomID, speciesTaxonomyID, speciesTaxonTypeID, hyperlinkRTARoadMap, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal animalDelegationAuthority As Entity.AnimalDelegationAuthority) As Entity.AnimalDelegationAuthority
            Return Me.Update(animalDelegationAuthority.id, animalDelegationAuthority(1), animalDelegationAuthority(2), animalDelegationAuthority(3), animalDelegationAuthority(4), animalDelegationAuthority(5), animalDelegationAuthority(6))
        End Function
        
        Public Overloads Function Update(ByVal animalDelegationAuthority As Entity.AnimalDelegationAuthority, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.AnimalDelegationAuthority
            Return Me.Update(animalDelegationAuthority.id, animalDelegationAuthority(1), animalDelegationAuthority(2), animalDelegationAuthority(3), animalDelegationAuthority(4), animalDelegationAuthority(5), animalDelegationAuthority(6), transaction)
        End Function
        
        Public Overloads Function Update(ByVal animalDelegationAuthority As Entity.AnimalDelegationAuthority, ByVal checkSum As Integer) As Entity.AnimalDelegationAuthority
            Return Me.Update(animalDelegationAuthority.id, animalDelegationAuthority(1), animalDelegationAuthority(2), animalDelegationAuthority(3), animalDelegationAuthority(4), animalDelegationAuthority(5), animalDelegationAuthority(6), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal animalDelegationAuthority As Entity.AnimalDelegationAuthority, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.AnimalDelegationAuthority
            Return Me.Update(animalDelegationAuthority.id, animalDelegationAuthority(1), animalDelegationAuthority(2), animalDelegationAuthority(3), animalDelegationAuthority(4), animalDelegationAuthority(5), animalDelegationAuthority(6), checkSum, transaction)
        End Function
        
        Public Overloads Function GetByIndex_IX_AnimalDelegationAuthority(ByVal applicationTypeID As Integer, ByVal delegationCode As Integer) As EntitySet.AnimalDelegationAuthoritySet
            Return Sprocs.eosp_SelectAnimalDelegationAuthority(animalDelegationAuthorityID:=Nothing, Index_ApplicationTypeID:=[applicationTypeID], Index_DelegationCode:=[delegationCode], Index_SpeciesKingdomID:=Nothing, Index_SpeciesTaxonomyID:=Nothing, Index_SpeciesTaxonTypeID:=Nothing, transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_IX_AnimalDelegationAuthority(ByVal applicationTypeID As Integer, ByVal delegationCode As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.AnimalDelegationAuthoritySet
            Return Sprocs.eosp_SelectAnimalDelegationAuthority(animalDelegationAuthorityID:=Nothing, Index_ApplicationTypeID:=[applicationTypeID], Index_DelegationCode:=[delegationCode], Index_SpeciesKingdomID:=Nothing, Index_SpeciesTaxonomyID:=Nothing, Index_SpeciesTaxonTypeID:=Nothing, sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Overloads Function GetByIndex_IX_AnimalDelegationAuthority_1(ByVal speciesKingdomID As Integer, ByVal speciesTaxonomyID As Integer, ByVal speciesTaxonTypeID As Integer) As EntitySet.AnimalDelegationAuthoritySet
            Return Sprocs.eosp_SelectAnimalDelegationAuthority(animalDelegationAuthorityID:=Nothing, Index_SpeciesKingdomID:=[speciesKingdomID], Index_SpeciesTaxonomyID:=[speciesTaxonomyID], Index_SpeciesTaxonTypeID:=[speciesTaxonTypeID], Index_ApplicationTypeID:=Nothing, Index_DelegationCode:=Nothing, transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_IX_AnimalDelegationAuthority_1(ByVal speciesKingdomID As Integer, ByVal speciesTaxonomyID As Integer, ByVal speciesTaxonTypeID As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.AnimalDelegationAuthoritySet
            Return Sprocs.eosp_SelectAnimalDelegationAuthority(animalDelegationAuthorityID:=Nothing, Index_SpeciesKingdomID:=[speciesKingdomID], Index_SpeciesTaxonomyID:=[speciesTaxonomyID], Index_SpeciesTaxonTypeID:=[speciesTaxonTypeID], Index_ApplicationTypeID:=Nothing, Index_DelegationCode:=Nothing, sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Overloads Function GetByIndex_IX_AnimalDelegationAuthority_2(ByVal delegationCode As Integer, ByVal applicationTypeID As Integer, ByVal speciesKingdomID As Integer, ByVal speciesTaxonomyID As Integer, ByVal speciesTaxonTypeID As Integer) As EntitySet.AnimalDelegationAuthoritySet
            Return Sprocs.eosp_SelectAnimalDelegationAuthority(animalDelegationAuthorityID:=Nothing, Index_DelegationCode:=[delegationCode], Index_ApplicationTypeID:=[applicationTypeID], Index_SpeciesKingdomID:=[speciesKingdomID], Index_SpeciesTaxonomyID:=[speciesTaxonomyID], Index_SpeciesTaxonTypeID:=[speciesTaxonTypeID], transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_IX_AnimalDelegationAuthority_2(ByVal delegationCode As Integer, ByVal applicationTypeID As Integer, ByVal speciesKingdomID As Integer, ByVal speciesTaxonomyID As Integer, ByVal speciesTaxonTypeID As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.AnimalDelegationAuthoritySet
            Return Sprocs.eosp_SelectAnimalDelegationAuthority(animalDelegationAuthorityID:=Nothing, Index_DelegationCode:=[delegationCode], Index_ApplicationTypeID:=[applicationTypeID], Index_SpeciesKingdomID:=[speciesKingdomID], Index_SpeciesTaxonomyID:=[speciesTaxonomyID], Index_SpeciesTaxonTypeID:=[speciesTaxonTypeID], sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
            
            IX_AnimalDelegationAuthority
            
            IX_AnimalDelegationAuthority_1
            
            IX_AnimalDelegationAuthority_2
            
            
        End Enum
    End Class
End Namespace

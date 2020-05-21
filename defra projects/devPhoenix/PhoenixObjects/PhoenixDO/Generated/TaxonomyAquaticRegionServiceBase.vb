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
    
    'Service base implementation for table 'TaxonomyAquaticRegion'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class TaxonomyAquaticRegionServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.TaxonomyAquaticRegionSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.TaxonomyAquaticRegionSet
            Return CType(MyBase.GetAll("eosp_SelectTaxonomyAquaticRegion", GetType(EntitySet.TaxonomyAquaticRegionSet), includeHyphen, includeInactive, orderBy),EntitySet.TaxonomyAquaticRegionSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.TaxonomyAquaticRegionSet
            Return Me.GetAll(includeHyphen, includeInactive, TaxonomyAquaticRegionServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, TaxonomyAquaticRegionServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal orderBy As OrderBy) As EntitySet.TaxonomyAquaticRegionSet
            Return Me.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal aquaticRegionID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectTaxonomyAquaticRegion", "AquaticRegionID", aquaticRegionID, GetType(EntitySet.TaxonomyAquaticRegionSet), tran),Entity.TaxonomyAquaticRegion)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal aquaticRegionID As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(aquaticRegionID, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal aquaticRegionID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyAquaticRegion
            Return CType(MyBase.GetById("eosp_SelectTaxonomyAquaticRegion", "AquaticRegionID", aquaticRegionID, GetType(EntitySet.TaxonomyAquaticRegionSet), tran),Entity.TaxonomyAquaticRegion)
        End Function
        
        Public Overloads Function GetById(ByVal aquaticRegionID As Integer) As Entity.TaxonomyAquaticRegion
            Return Me.GetById(aquaticRegionID, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal aquaticRegionID As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(aquaticRegionID, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal aquaticRegionID As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeleteTaxonomyAquaticRegion", "AquaticRegionID", aquaticRegionID, checkSum, transaction)
        End Function
        
        Public Overloads Function Insert(ByVal aquaticRegionID As Integer, ByVal regionName As Object, ByVal regionSubName As Object, ByVal regionType As Object, ByVal aquaticRegionNoteID As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyAquaticRegion
            Return Me.GetById(Sprocs.eosp_CreateTaxonomyAquaticRegion(aquaticRegionID, regionName, regionSubName, regionType, aquaticRegionNoteID, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal aquaticRegionID As Integer, ByVal regionName As Object, ByVal regionSubName As Object, ByVal regionType As Object, ByVal aquaticRegionNoteID As Object) As Entity.TaxonomyAquaticRegion
            Return Me.Insert(aquaticRegionID, regionName, regionSubName, regionType, aquaticRegionNoteID, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal taxonomyAquaticRegion As Entity.TaxonomyAquaticRegion) As Entity.TaxonomyAquaticRegion
            Return Me.Insert(taxonomyAquaticRegion(0), taxonomyAquaticRegion(1), taxonomyAquaticRegion(2), taxonomyAquaticRegion(3), taxonomyAquaticRegion(4))
        End Function
        
        Public Overloads Function Insert(ByVal taxonomyAquaticRegion As Entity.TaxonomyAquaticRegion, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyAquaticRegion
            Return Me.Insert(taxonomyAquaticRegion(0), taxonomyAquaticRegion(1), taxonomyAquaticRegion(2), taxonomyAquaticRegion(3), taxonomyAquaticRegion(4), transaction)
        End Function
        
        Public Overloads Function Update(ByVal aquaticRegionID As Integer, ByVal regionName As Object, ByVal regionSubName As Object, ByVal regionType As Object, ByVal aquaticRegionNoteID As Object, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyAquaticRegion
            Return Sprocs.eosp_UpdateTaxonomyAquaticRegion(aquaticRegionID, regionName, regionSubName, regionType, aquaticRegionNoteID, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal aquaticRegionID As Integer, ByVal regionName As Object, ByVal regionSubName As Object, ByVal regionType As Object, ByVal aquaticRegionNoteID As Object) As Entity.TaxonomyAquaticRegion
            Return Me.Update(aquaticRegionID, regionName, regionSubName, regionType, aquaticRegionNoteID, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal aquaticRegionID As Integer, ByVal regionName As Object, ByVal regionSubName As Object, ByVal regionType As Object, ByVal aquaticRegionNoteID As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyAquaticRegion
            Return Me.Update(aquaticRegionID, regionName, regionSubName, regionType, aquaticRegionNoteID, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal aquaticRegionID As Integer, ByVal regionName As Object, ByVal regionSubName As Object, ByVal regionType As Object, ByVal aquaticRegionNoteID As Object, ByVal checkSum As Integer) As Entity.TaxonomyAquaticRegion
            Return Me.Update(aquaticRegionID, regionName, regionSubName, regionType, aquaticRegionNoteID, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal taxonomyAquaticRegion As Entity.TaxonomyAquaticRegion) As Entity.TaxonomyAquaticRegion
            Return Me.Update(taxonomyAquaticRegion(0), taxonomyAquaticRegion(1), taxonomyAquaticRegion(2), taxonomyAquaticRegion(3), taxonomyAquaticRegion(4))
        End Function
        
        Public Overloads Function Update(ByVal taxonomyAquaticRegion As Entity.TaxonomyAquaticRegion, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyAquaticRegion
            Return Me.Update(taxonomyAquaticRegion(0), taxonomyAquaticRegion(1), taxonomyAquaticRegion(2), taxonomyAquaticRegion(3), taxonomyAquaticRegion(4), transaction)
        End Function
        
        Public Overloads Function Update(ByVal taxonomyAquaticRegion As Entity.TaxonomyAquaticRegion, ByVal checkSum As Integer) As Entity.TaxonomyAquaticRegion
            Return Me.Update(taxonomyAquaticRegion(0), taxonomyAquaticRegion(1), taxonomyAquaticRegion(2), taxonomyAquaticRegion(3), taxonomyAquaticRegion(4), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal taxonomyAquaticRegion As Entity.TaxonomyAquaticRegion, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyAquaticRegion
            Return Me.Update(taxonomyAquaticRegion(0), taxonomyAquaticRegion(1), taxonomyAquaticRegion(2), taxonomyAquaticRegion(3), taxonomyAquaticRegion(4), checkSum, transaction)
        End Function
        
        Public Overloads Function GetByIndex_IX_TaxonomyAquaticRegion(ByVal aquaticRegionNoteID As Integer) As EntitySet.TaxonomyAquaticRegionSet
            Return Sprocs.eosp_SelectTaxonomyAquaticRegion(aquaticRegionID:=Nothing, Index_AquaticRegionNoteID:=[aquaticRegionNoteID], transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_IX_TaxonomyAquaticRegion(ByVal aquaticRegionNoteID As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.TaxonomyAquaticRegionSet
            Return Sprocs.eosp_SelectTaxonomyAquaticRegion(aquaticRegionID:=Nothing, Index_AquaticRegionNoteID:=[aquaticRegionNoteID], sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
            
            IX_TaxonomyAquaticRegion
            
            
        End Enum
    End Class
End Namespace
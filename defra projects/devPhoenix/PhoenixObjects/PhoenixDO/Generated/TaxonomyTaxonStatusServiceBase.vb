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
    
    'Service base implementation for table 'TaxonomyTaxonStatus'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class TaxonomyTaxonStatusServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.TaxonomyTaxonStatusSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.TaxonomyTaxonStatusSet
            Return CType(MyBase.GetAll("eosp_SelectTaxonomyTaxonStatus", GetType(EntitySet.TaxonomyTaxonStatusSet), includeHyphen, includeInactive, orderBy),EntitySet.TaxonomyTaxonStatusSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.TaxonomyTaxonStatusSet
            Return Me.GetAll(includeHyphen, includeInactive, TaxonomyTaxonStatusServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, TaxonomyTaxonStatusServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal iD As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectTaxonomyTaxonStatus", "ID", iD, GetType(EntitySet.TaxonomyTaxonStatusSet), tran),Entity.TaxonomyTaxonStatus)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal iD As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(iD, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal iD As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyTaxonStatus
            Return CType(MyBase.GetById("eosp_SelectTaxonomyTaxonStatus", "ID", iD, GetType(EntitySet.TaxonomyTaxonStatusSet), tran),Entity.TaxonomyTaxonStatus)
        End Function
        
        Public Overloads Function GetById(ByVal iD As Integer) As Entity.TaxonomyTaxonStatus
            Return Me.GetById(iD, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal iD As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(iD, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal iD As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeleteTaxonomyTaxonStatus", "ID", iD, checkSum, transaction)
        End Function
        
        Public Overloads Function Insert(ByVal description As String, ByVal active As Boolean, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyTaxonStatus
            Return Me.GetById(Sprocs.eosp_CreateTaxonomyTaxonStatus(description, active, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal description As String, ByVal active As Boolean) As Entity.TaxonomyTaxonStatus
            Return Me.Insert(description, active, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal taxonomyTaxonStatus As Entity.TaxonomyTaxonStatus) As Entity.TaxonomyTaxonStatus
            Return Me.Insert(taxonomyTaxonStatus(1), taxonomyTaxonStatus(2))
        End Function
        
        Public Overloads Function Insert(ByVal taxonomyTaxonStatus As Entity.TaxonomyTaxonStatus, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyTaxonStatus
            Return Me.Insert(taxonomyTaxonStatus(1), taxonomyTaxonStatus(2), transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal description As String, ByVal active As Boolean, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyTaxonStatus
            Return Sprocs.eosp_UpdateTaxonomyTaxonStatus(id, description, active, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal description As String, ByVal active As Boolean) As Entity.TaxonomyTaxonStatus
            Return Me.Update(id, description, active, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal description As String, ByVal active As Boolean, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyTaxonStatus
            Return Me.Update(id, description, active, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal description As String, ByVal active As Boolean, ByVal checkSum As Integer) As Entity.TaxonomyTaxonStatus
            Return Me.Update(id, description, active, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal taxonomyTaxonStatus As Entity.TaxonomyTaxonStatus) As Entity.TaxonomyTaxonStatus
            Return Me.Update(taxonomyTaxonStatus.id, taxonomyTaxonStatus(1), taxonomyTaxonStatus(2))
        End Function
        
        Public Overloads Function Update(ByVal taxonomyTaxonStatus As Entity.TaxonomyTaxonStatus, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyTaxonStatus
            Return Me.Update(taxonomyTaxonStatus.id, taxonomyTaxonStatus(1), taxonomyTaxonStatus(2), transaction)
        End Function
        
        Public Overloads Function Update(ByVal taxonomyTaxonStatus As Entity.TaxonomyTaxonStatus, ByVal checkSum As Integer) As Entity.TaxonomyTaxonStatus
            Return Me.Update(taxonomyTaxonStatus.id, taxonomyTaxonStatus(1), taxonomyTaxonStatus(2), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal taxonomyTaxonStatus As Entity.TaxonomyTaxonStatus, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyTaxonStatus
            Return Me.Update(taxonomyTaxonStatus.id, taxonomyTaxonStatus(1), taxonomyTaxonStatus(2), checkSum, transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
        End Enum
    End Class
End Namespace

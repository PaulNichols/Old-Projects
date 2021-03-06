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
    
    'Service base implementation for table 'PermitTaxSpecie'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class PermitTaxSpecieServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.PermitTaxSpecieSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.PermitTaxSpecieSet
            Return CType(MyBase.GetAll("eosp_SelectPermitTaxSpecie", GetType(EntitySet.PermitTaxSpecieSet), includeHyphen, includeInactive, orderBy),EntitySet.PermitTaxSpecieSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.PermitTaxSpecieSet
            Return Me.GetAll(includeHyphen, includeInactive, PermitTaxSpecieServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, PermitTaxSpecieServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal orderBy As OrderBy) As EntitySet.PermitTaxSpecieSet
            Return Me.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal permitTaxSpecie As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectPermitTaxSpecie", "PermitTaxSpecie", permitTaxSpecie, GetType(EntitySet.PermitTaxSpecieSet), tran),Entity.PermitTaxSpecie)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal permitTaxSpecie As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(permitTaxSpecie, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal permitTaxSpecie As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.PermitTaxSpecie
            Return CType(MyBase.GetById("eosp_SelectPermitTaxSpecie", "PermitTaxSpecie", permitTaxSpecie, GetType(EntitySet.PermitTaxSpecieSet), tran),Entity.PermitTaxSpecie)
        End Function
        
        Public Overloads Function GetById(ByVal permitTaxSpecie As Integer) As Entity.PermitTaxSpecie
            Return Me.GetById(permitTaxSpecie, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal permitTaxSpecie As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(permitTaxSpecie, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal permitTaxSpecie As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeletePermitTaxSpecie", "PermitTaxSpecie", permitTaxSpecie, checkSum, transaction)
        End Function
        
        Public Overloads Function Insert(ByVal commonName As Object, ByVal aCAnnex As String, ByVal cITESAppendix As String, ByVal scientificName As String, ByVal description As String, ByVal appliedforName As String, ByVal specieId As Object, ByVal permitSpecie As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PermitTaxSpecie
            Return Me.GetById(Sprocs.eosp_CreatePermitTaxSpecie(commonName, aCAnnex, cITESAppendix, scientificName, description, appliedforName, specieId, permitSpecie, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal commonName As Object, ByVal aCAnnex As String, ByVal cITESAppendix As String, ByVal scientificName As String, ByVal description As String, ByVal appliedforName As String, ByVal specieId As Object, ByVal permitSpecie As Object) As Entity.PermitTaxSpecie
            Return Me.Insert(commonName, aCAnnex, cITESAppendix, scientificName, description, appliedforName, specieId, permitSpecie, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal permitTaxSpecie As Entity.PermitTaxSpecie) As Entity.PermitTaxSpecie
            Return Me.Insert(permitTaxSpecie(1), permitTaxSpecie(2), permitTaxSpecie(3), permitTaxSpecie(4), permitTaxSpecie(5), permitTaxSpecie(6), permitTaxSpecie(7), permitTaxSpecie(8))
        End Function
        
        Public Overloads Function Insert(ByVal permitTaxSpecie As Entity.PermitTaxSpecie, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PermitTaxSpecie
            Return Me.Insert(permitTaxSpecie(1), permitTaxSpecie(2), permitTaxSpecie(3), permitTaxSpecie(4), permitTaxSpecie(5), permitTaxSpecie(6), permitTaxSpecie(7), permitTaxSpecie(8), transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal commonName As Object, ByVal aCAnnex As String, ByVal cITESAppendix As String, ByVal scientificName As String, ByVal description As String, ByVal appliedforName As String, ByVal specieId As Object, ByVal permitSpecie As Object, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PermitTaxSpecie
            Return Sprocs.eosp_UpdatePermitTaxSpecie(id, commonName, aCAnnex, cITESAppendix, scientificName, description, appliedforName, specieId, permitSpecie, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal commonName As Object, ByVal aCAnnex As String, ByVal cITESAppendix As String, ByVal scientificName As String, ByVal description As String, ByVal appliedforName As String, ByVal specieId As Object, ByVal permitSpecie As Object) As Entity.PermitTaxSpecie
            Return Me.Update(id, commonName, aCAnnex, cITESAppendix, scientificName, description, appliedforName, specieId, permitSpecie, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal commonName As Object, ByVal aCAnnex As String, ByVal cITESAppendix As String, ByVal scientificName As String, ByVal description As String, ByVal appliedforName As String, ByVal specieId As Object, ByVal permitSpecie As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PermitTaxSpecie
            Return Me.Update(id, commonName, aCAnnex, cITESAppendix, scientificName, description, appliedforName, specieId, permitSpecie, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal commonName As Object, ByVal aCAnnex As String, ByVal cITESAppendix As String, ByVal scientificName As String, ByVal description As String, ByVal appliedforName As String, ByVal specieId As Object, ByVal permitSpecie As Object, ByVal checkSum As Integer) As Entity.PermitTaxSpecie
            Return Me.Update(id, commonName, aCAnnex, cITESAppendix, scientificName, description, appliedforName, specieId, permitSpecie, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal permitTaxSpecie As Entity.PermitTaxSpecie) As Entity.PermitTaxSpecie
            Return Me.Update(permitTaxSpecie.id, permitTaxSpecie(1), permitTaxSpecie(2), permitTaxSpecie(3), permitTaxSpecie(4), permitTaxSpecie(5), permitTaxSpecie(6), permitTaxSpecie(7), permitTaxSpecie(8))
        End Function
        
        Public Overloads Function Update(ByVal permitTaxSpecie As Entity.PermitTaxSpecie, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PermitTaxSpecie
            Return Me.Update(permitTaxSpecie.id, permitTaxSpecie(1), permitTaxSpecie(2), permitTaxSpecie(3), permitTaxSpecie(4), permitTaxSpecie(5), permitTaxSpecie(6), permitTaxSpecie(7), permitTaxSpecie(8), transaction)
        End Function
        
        Public Overloads Function Update(ByVal permitTaxSpecie As Entity.PermitTaxSpecie, ByVal checkSum As Integer) As Entity.PermitTaxSpecie
            Return Me.Update(permitTaxSpecie.id, permitTaxSpecie(1), permitTaxSpecie(2), permitTaxSpecie(3), permitTaxSpecie(4), permitTaxSpecie(5), permitTaxSpecie(6), permitTaxSpecie(7), permitTaxSpecie(8), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal permitTaxSpecie As Entity.PermitTaxSpecie, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PermitTaxSpecie
            Return Me.Update(permitTaxSpecie.id, permitTaxSpecie(1), permitTaxSpecie(2), permitTaxSpecie(3), permitTaxSpecie(4), permitTaxSpecie(5), permitTaxSpecie(6), permitTaxSpecie(7), permitTaxSpecie(8), checkSum, transaction)
        End Function
        
        Public Overloads Function GetByIndex_UQ__PermitTaxSpecie__3F898CC2(ByVal commonName As String, ByVal scientificName As String, ByVal appliedforName As String, ByVal specieId As Integer, ByVal permitSpecie As Integer) As EntitySet.PermitTaxSpecieSet
            Return Sprocs.eosp_SelectPermitTaxSpecie(permitTaxSpecie:=Nothing, Index_CommonName:=[commonName], Index_ScientificName:=[scientificName], Index_AppliedforName:=[appliedforName], Index_SpecieId:=[specieId], Index_PermitSpecie:=[permitSpecie], transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_UQ__PermitTaxSpecie__3F898CC2(ByVal commonName As String, ByVal scientificName As String, ByVal appliedforName As String, ByVal specieId As Integer, ByVal permitSpecie As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitTaxSpecieSet
            Return Sprocs.eosp_SelectPermitTaxSpecie(permitTaxSpecie:=Nothing, Index_CommonName:=[commonName], Index_ScientificName:=[scientificName], Index_AppliedforName:=[appliedforName], Index_SpecieId:=[specieId], Index_PermitSpecie:=[permitSpecie], sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
            
            UQ__PermitTaxSpecie__3F898CC2
            
            
        End Enum
    End Class
End Namespace

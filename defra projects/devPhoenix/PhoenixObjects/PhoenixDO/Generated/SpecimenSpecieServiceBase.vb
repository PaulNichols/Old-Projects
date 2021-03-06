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
    
    'Service base implementation for table 'SpecimenSpecie'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class SpecimenSpecieServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.SpecimenSpecieSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.SpecimenSpecieSet
            Return CType(MyBase.GetAll("eosp_SelectSpecimenSpecie", GetType(EntitySet.SpecimenSpecieSet), includeHyphen, includeInactive, orderBy),EntitySet.SpecimenSpecieSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.SpecimenSpecieSet
            Return Me.GetAll(includeHyphen, includeInactive, SpecimenSpecieServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, SpecimenSpecieServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Function GetById(ByVal idColumns() As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.SpecimenSpecie
            Return CType(MyBase.GetById("eosp_SelectSpecimenSpecie", New String() {"SpecimenId", "SpecieId"}, idColumns, GetType(EntitySet.SpecimenSpecieSet), tran),Entity.SpecimenSpecie)
        End Function
        
        Public Overloads Function GetById(ByVal idColumns() As Integer) As Entity.SpecimenSpecie
            Return Me.GetById(idColumns, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal idColumns() As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(idColumns, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal idColumns() As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeleteSpecimenSpecie", New String() {"SpecimenId", "SpecieId"}, idColumns, checkSum, transaction)
        End Function
        
        'GetForSpecimen - links to the Specimen table...
        Public Overloads Function GetForSpecimen(ByVal SpecimenId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.SpecimenSpecieSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from SpecimenSpecie where SpecimenId=" + SpecimenId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.SpecimenSpecieSet), tran),EntitySet.SpecimenSpecieSet)
        End Function
        
        'GetForSpecimen - links to the Specimen table...
        Public Overloads Function GetForSpecimen(ByVal SpecimenId As Integer) As EntitySet.SpecimenSpecieSet
            Return Me.GetForSpecimen(SpecimenId, Nothing)
        End Function
        
        'GetForSpecie - links to the Specie table...
        Public Overloads Function GetForSpecie(ByVal SpecieId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.SpecimenSpecieSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from SpecimenSpecie where SpecieId=" + SpecieId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.SpecimenSpecieSet), tran),EntitySet.SpecimenSpecieSet)
        End Function
        
        'GetForSpecie - links to the Specie table...
        Public Overloads Function GetForSpecie(ByVal SpecieId As Integer) As EntitySet.SpecimenSpecieSet
            Return Me.GetForSpecie(SpecieId, Nothing)
        End Function
        
        Public Overloads Sub Insert(ByVal specimenId As Integer, ByVal specieId As Integer, ByVal hybridSequence As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction)
            Sprocs.eosp_CreateSpecimenSpecie(specimenId, specieId, hybridSequence, transaction)
        End Sub
        
        Public Overloads Sub Insert(ByVal specimenId As Integer, ByVal specieId As Integer, ByVal hybridSequence As Integer)
            Me.Insert(specimenId, specieId, hybridSequence, Nothing)
        End Sub
        
        Public Overloads Sub Insert(ByVal specimenSpecie As Entity.SpecimenSpecie)
            Me.Insert(specimenSpecie(0), specimenSpecie(1), specimenSpecie(2))
        End Sub
        
        Public Overloads Sub Insert(ByVal specimenSpecie As Entity.SpecimenSpecie, ByVal transaction As System.Data.SqlClient.SqlTransaction)
            Me.Insert(specimenSpecie(0), specimenSpecie(1), specimenSpecie(2), transaction)
        End Sub
        
        Public Overloads Function Update(ByVal specimenId As Integer, ByVal specieId As Integer, ByVal hybridSequence As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.SpecimenSpecie
            Return Sprocs.eosp_UpdateSpecimenSpecie(specimenId, specieId, hybridSequence, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal specimenId As Integer, ByVal specieId As Integer, ByVal hybridSequence As Integer) As Entity.SpecimenSpecie
            Return Me.Update(specimenId, specieId, hybridSequence, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal specimenId As Integer, ByVal specieId As Integer, ByVal hybridSequence As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.SpecimenSpecie
            Return Me.Update(specimenId, specieId, hybridSequence, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal specimenId As Integer, ByVal specieId As Integer, ByVal hybridSequence As Integer, ByVal checkSum As Integer) As Entity.SpecimenSpecie
            Return Me.Update(specimenId, specieId, hybridSequence, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal specimenSpecie As Entity.SpecimenSpecie) As Entity.SpecimenSpecie
            Return Me.Update(specimenSpecie(0), specimenSpecie(1), specimenSpecie(2))
        End Function
        
        Public Overloads Function Update(ByVal specimenSpecie As Entity.SpecimenSpecie, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.SpecimenSpecie
            Return Me.Update(specimenSpecie(0), specimenSpecie(1), specimenSpecie(2), transaction)
        End Function
        
        Public Overloads Function Update(ByVal specimenSpecie As Entity.SpecimenSpecie, ByVal checkSum As Integer) As Entity.SpecimenSpecie
            Return Me.Update(specimenSpecie(0), specimenSpecie(1), specimenSpecie(2), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal specimenSpecie As Entity.SpecimenSpecie, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.SpecimenSpecie
            Return Me.Update(specimenSpecie(0), specimenSpecie(1), specimenSpecie(2), checkSum, transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
        End Enum
    End Class
End Namespace

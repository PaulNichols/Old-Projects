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
    
    'Service base implementation for table 'TaxonomyNote'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class TaxonomyNoteServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.TaxonomyNoteSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.TaxonomyNoteSet
            Return CType(MyBase.GetAll("eosp_SelectTaxonomyNote", GetType(EntitySet.TaxonomyNoteSet), includeHyphen, includeInactive, orderBy),EntitySet.TaxonomyNoteSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.TaxonomyNoteSet
            Return Me.GetAll(includeHyphen, includeInactive, TaxonomyNoteServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, TaxonomyNoteServiceBase.OrderBy.DefaultOrder)
        End Function
        
        'GetForTaxonomyTaxon - links to the TaxonomyTaxon table...
        Public Overloads Function GetForTaxonomyTaxon(ByVal ID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.TaxonomyNoteSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from TaxonomyNote where TaxonomyId=" + ID.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.TaxonomyNoteSet), tran),EntitySet.TaxonomyNoteSet)
        End Function
        
        'GetForTaxonomyTaxon - links to the TaxonomyTaxon table...
        Public Overloads Function GetForTaxonomyTaxon(ByVal ID As Integer) As EntitySet.TaxonomyNoteSet
            Return Me.GetForTaxonomyTaxon(ID, Nothing)
        End Function
        
        'GetForNote - links to the Notes table...
        Public Overloads Function GetForNote(ByVal NoteId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.TaxonomyNoteSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from TaxonomyNote where NoteId=" + NoteId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.TaxonomyNoteSet), tran),EntitySet.TaxonomyNoteSet)
        End Function
        
        'GetForNote - links to the Notes table...
        Public Overloads Function GetForNote(ByVal NoteId As Integer) As EntitySet.TaxonomyNoteSet
            Return Me.GetForNote(NoteId, Nothing)
        End Function
        
        Public Overloads Sub Insert(ByVal taxonomyId As Integer, ByVal noteId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction)
            Sprocs.eosp_CreateTaxonomyNote(taxonomyId, noteId, transaction)
        End Sub
        
        Public Overloads Sub Insert(ByVal taxonomyId As Integer, ByVal noteId As Integer)
            Me.Insert(taxonomyId, noteId, Nothing)
        End Sub
        
        Public Overloads Sub Insert(ByVal taxonomyNote As Entity.TaxonomyNote)
            Me.Insert(taxonomyNote(0), taxonomyNote(1))
        End Sub
        
        Public Overloads Sub Insert(ByVal taxonomyNote As Entity.TaxonomyNote, ByVal transaction As System.Data.SqlClient.SqlTransaction)
            Me.Insert(taxonomyNote(0), taxonomyNote(1), transaction)
        End Sub
        
        Public Overloads Sub Update(ByVal taxonomyId As Integer, ByVal noteId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction)
            Sprocs.eosp_UpdateTaxonomyNote(taxonomyId, noteId, checkSum, transaction)
        End Sub
        
        Public Overloads Sub Update(ByVal taxonomyId As Integer, ByVal noteId As Integer)
            Me.Update(taxonomyId, noteId, 0, Nothing)
        End Sub
        
        Public Overloads Sub Update(ByVal taxonomyId As Integer, ByVal noteId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction)
            Me.Update(taxonomyId, noteId, 0, transaction)
        End Sub
        
        Public Overloads Sub Update(ByVal taxonomyId As Integer, ByVal noteId As Integer, ByVal checkSum As Integer)
            Me.Update(taxonomyId, noteId, checkSum, Nothing)
        End Sub
        
        Public Overloads Sub Update(ByVal taxonomyNote As Entity.TaxonomyNote)
            Me.Update(taxonomyNote(0), taxonomyNote(1))
        End Sub
        
        Public Overloads Sub Update(ByVal taxonomyNote As Entity.TaxonomyNote, ByVal transaction As System.Data.SqlClient.SqlTransaction)
            Me.Update(taxonomyNote(0), taxonomyNote(1), transaction)
        End Sub
        
        Public Overloads Sub Update(ByVal taxonomyNote As Entity.TaxonomyNote, ByVal checkSum As Integer)
            Me.Update(taxonomyNote(0), taxonomyNote(1), checkSum)
        End Sub
        
        Public Overloads Sub Update(ByVal taxonomyNote As Entity.TaxonomyNote, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction)
            Me.Update(taxonomyNote(0), taxonomyNote(1), checkSum, transaction)
        End Sub
        
        Public Enum OrderBy
            
            DefaultOrder
        End Enum
    End Class
End Namespace
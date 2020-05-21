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
    
    'Service base implementation for table 'Notes'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class NoteServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return true
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.NoteSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.NoteSet
            Return CType(MyBase.GetAll("eosp_SelectNote", GetType(EntitySet.NoteSet), includeHyphen, includeInactive, orderBy),EntitySet.NoteSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.NoteSet
            Return Me.GetAll(includeHyphen, includeInactive, NoteServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, NoteServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal noteId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectNote", "NoteId", noteId, GetType(EntitySet.NoteSet), tran),Entity.Note)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal noteId As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(noteId, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal noteId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.Note
            Return CType(MyBase.GetById("eosp_SelectNote", "NoteId", noteId, GetType(EntitySet.NoteSet), tran),Entity.Note)
        End Function
        
        Public Overloads Function GetById(ByVal noteId As Integer) As Entity.Note
            Return Me.GetById(noteId, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal noteId As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(noteId, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal noteId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeleteNote", "NoteId", noteId, checkSum, transaction)
        End Function
        
        Public Overloads Function Insert(ByVal [date] As Date, ByVal content As String, ByVal isReadOnly As Boolean, ByVal important As Boolean, ByVal subject As String, ByVal modifiedBy As Object, ByVal modifiedDate As Object, ByVal createdBy As Object, ByVal createdDate As Date, ByVal active As Boolean, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Note
            Return Me.GetById(Sprocs.eosp_CreateNote([date], content, isReadOnly, important, subject, modifiedBy, modifiedDate, createdBy, createdDate, active, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal [date] As Date, ByVal content As String, ByVal isReadOnly As Boolean, ByVal important As Boolean, ByVal subject As String, ByVal modifiedBy As Object, ByVal modifiedDate As Object, ByVal createdBy As Object, ByVal createdDate As Date, ByVal active As Boolean) As Entity.Note
            Return Me.Insert([date], content, isReadOnly, important, subject, modifiedBy, modifiedDate, createdBy, createdDate, active, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal note As Entity.Note) As Entity.Note
            Return Me.Insert(note(1), note(2), note(3), note(4), note(5), note(6), note(7), note(8), note(9), note(11))
        End Function
        
        Public Overloads Function Insert(ByVal note As Entity.Note, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Note
            Return Me.Insert(note(1), note(2), note(3), note(4), note(5), note(6), note(7), note(8), note(9), note(11), transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal [date] As Date, ByVal content As String, ByVal isReadOnly As Boolean, ByVal important As Boolean, ByVal subject As String, ByVal modifiedBy As Object, ByVal modifiedDate As Object, ByVal createdBy As Object, ByVal createdDate As Date, ByVal active As Boolean, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Note
            Return Sprocs.eosp_UpdateNote(id, [date], content, isReadOnly, important, subject, modifiedBy, modifiedDate, createdBy, createdDate, active, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal [date] As Date, ByVal content As String, ByVal isReadOnly As Boolean, ByVal important As Boolean, ByVal subject As String, ByVal modifiedBy As Object, ByVal modifiedDate As Object, ByVal createdBy As Object, ByVal createdDate As Date, ByVal active As Boolean) As Entity.Note
            Return Me.Update(id, [date], content, isReadOnly, important, subject, modifiedBy, modifiedDate, createdBy, createdDate, active, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal [date] As Date, ByVal content As String, ByVal isReadOnly As Boolean, ByVal important As Boolean, ByVal subject As String, ByVal modifiedBy As Object, ByVal modifiedDate As Object, ByVal createdBy As Object, ByVal createdDate As Date, ByVal active As Boolean, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Note
            Return Me.Update(id, [date], content, isReadOnly, important, subject, modifiedBy, modifiedDate, createdBy, createdDate, active, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal [date] As Date, ByVal content As String, ByVal isReadOnly As Boolean, ByVal important As Boolean, ByVal subject As String, ByVal modifiedBy As Object, ByVal modifiedDate As Object, ByVal createdBy As Object, ByVal createdDate As Date, ByVal active As Boolean, ByVal checkSum As Integer) As Entity.Note
            Return Me.Update(id, [date], content, isReadOnly, important, subject, modifiedBy, modifiedDate, createdBy, createdDate, active, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal note As Entity.Note) As Entity.Note
            Return Me.Update(note.id, note(1), note(2), note(3), note(4), note(5), note(6), note(7), note(8), note(9), note(11))
        End Function
        
        Public Overloads Function Update(ByVal note As Entity.Note, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Note
            Return Me.Update(note.id, note(1), note(2), note(3), note(4), note(5), note(6), note(7), note(8), note(9), note(11), transaction)
        End Function
        
        Public Overloads Function Update(ByVal note As Entity.Note, ByVal checkSum As Integer) As Entity.Note
            Return Me.Update(note.id, note(1), note(2), note(3), note(4), note(5), note(6), note(7), note(8), note(9), note(11), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal note As Entity.Note, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Note
            Return Me.Update(note.id, note(1), note(2), note(3), note(4), note(5), note(6), note(7), note(8), note(9), note(11), checkSum, transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
        End Enum
    End Class
End Namespace
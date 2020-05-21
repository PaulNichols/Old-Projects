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
    
    'Base entity implementation for table 'PartyNote'
    '*DO NOT* modify this file.
    'Add new properties and methods to PartyNote instead.
    Public MustInherit Class PartyNoteBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal partyId As Integer, ByVal noteId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(partyId, noteId, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal partyId As Integer, ByVal noteId As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(partyId, noteId).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property PartyId As Integer
            Get
                Return CType(Me(0),Integer)
            End Get
            Set
                Me(0) = value
            End Set
        End Property
        
        Public Property NoteId As Integer
            Get
                Return CType(Me(1),Integer)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        Public Overrides Property CheckSum As Integer Implements EnterpriseObjects.IUpdatable.CheckSum
            Get
                If (Me.IsCheckSumNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(2),Integer)
                End If
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.PartyNoteService
            Get
                Return CType(GetServiceObject(GetType(Service.PartyNoteService)),Service.PartyNoteService)
            End Get
        End Property
        
        Public Overridable Property RawDataset As System.Data.DataSet Implements EnterpriseObjects.IUpdatable.RawDataset
            Get
                Return mRawDataset
            End Get
            Set
                mRawDataset = value
            End Set
        End Property
        
        Public Overrides Sub GetObjectData(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.GetObjectData(info, context)
        End Sub
        
        Public Function IsCheckSumNull() As Boolean
            Return Me.IsNull(2)
        End Function
        
        Public Sub SetCheckSumToNull()
            Me(2) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(3)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.PartyNoteSet
            Return PartyNoteBase.GetAll(false, false, PartyNoteServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.PartyNoteSet
            Return PartyNoteBase.GetAll(includeHyphen, false, PartyNoteServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As PartyNoteServiceBase.OrderBy) As EntitySet.PartyNoteSet
            Dim service As Service.PartyNoteService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As PartyNoteServiceBase.OrderBy) As EntitySet.PartyNoteSet
            Return PartyNoteBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal partyId As Integer, ByVal noteId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.PartyNote
            Dim service As Service.PartyNoteService
            service = ServiceObject
            Return service.GetById(New Integer() {partyId, noteId}, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal partyId As Integer, ByVal noteId As Integer) As Entity.PartyNote
            Dim service As Service.PartyNoteService
            service = ServiceObject
            Return service.GetById(New Integer() {partyId, noteId})
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal partyId As Integer, ByVal noteId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.PartyNoteService
            service = ServiceObject
            Return service.DeleteById(New Integer() {partyId, noteId}, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal partyId As Integer, ByVal noteId As Integer) As Boolean
            Return PartyNoteBase.DeleteById(partyId, noteId, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal partyId As Integer, ByVal noteId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return PartyNoteBase.DeleteById(partyId, noteId, 0, transaction)
        End Function
        
        Public Overloads Shared Function GetForParty(ByVal partyId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PartyNoteSet
            Dim service As Service.PartyNoteService
            service = ServiceObject
            Return service.GetForParty(partyId, tran)
        End Function
        
        Public Overloads Shared Function GetForParty(ByVal partyId As Integer) As EntitySet.PartyNoteSet
            Return PartyNoteBase.GetForParty(partyId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForNote(ByVal noteId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PartyNoteSet
            Dim service As Service.PartyNoteService
            service = ServiceObject
            Return service.GetForNote(noteId, tran)
        End Function
        
        Public Overloads Shared Function GetForNote(ByVal noteId As Integer) As EntitySet.PartyNoteSet
            Return PartyNoteBase.GetForNote(noteId, Nothing)
        End Function
        
        Public Shared Sub Insert(ByVal partyId As Integer, ByVal noteId As Integer)
            Entity.PartyNote.ServiceObject.Insert(partyId, noteId)
        End Sub
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim partyIdParam As Integer = Me.PartyId
            Dim noteIdParam As Integer = Me.NoteId
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.PartyNote.ServiceObject.Update(partyIdParam, noteIdParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace
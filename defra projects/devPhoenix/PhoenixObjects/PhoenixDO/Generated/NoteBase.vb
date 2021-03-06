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
    
    'Base entity implementation for table 'Notes'
    '*DO NOT* modify this file.
    'Add new properties and methods to Note instead.
    Public MustInherit Class NoteBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal noteId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(noteId, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal noteId As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(noteId).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property NoteId As Integer
            Get
                Return CType(Me(0),Integer)
            End Get
            Set
                Me(0) = value
            End Set
        End Property
        
        Public Overrides Property Id As Integer
            Get
                Return CType(Me(0),Integer)
            End Get
            Set
                Me(0) = value
            End Set
        End Property
        
        Public Property [Date] As Date
            Get
                Return CType(Me(1),Date)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(8000)>  _
        Public Property Content As String
            Get
                Return CType(Me(2),String)
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        Public Property IsReadOnly As Boolean
            Get
                Return CType(Me(3),Boolean)
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        Public Property Important As Boolean
            Get
                Return CType(Me(4),Boolean)
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(100)>  _
        Public Property Subject As String
            Get
                Return CType(Me(5),String)
            End Get
            Set
                Me(5) = value
            End Set
        End Property
        
        Public Property ModifiedBy As Integer
            Get
                If (Me.IsModifiedByNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(6),Integer)
                End If
            End Get
            Set
                Me(6) = value
            End Set
        End Property
        
        Public Property ModifiedDate As Date
            Get
                If (Me.IsModifiedDateNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(7),Date)
                End If
            End Get
            Set
                Me(7) = value
            End Set
        End Property
        
        Public Property CreatedBy As Integer
            Get
                If (Me.IsCreatedByNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(8),Integer)
                End If
            End Get
            Set
                Me(8) = value
            End Set
        End Property
        
        Public Property CreatedDate As Date
            Get
                Return CType(Me(9),Date)
            End Get
            Set
                Me(9) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property ShortContent As String
            Get
                If (Me.IsShortContentNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(10),String)
                End If
            End Get
            Set
                Me(10) = value
            End Set
        End Property
        
        Public Property Active As Boolean
            Get
                Return CType(Me(11),Boolean)
            End Get
            Set
                Me(11) = value
            End Set
        End Property
        
        Public Overrides Property CheckSum As Integer Implements EnterpriseObjects.IUpdatable.CheckSum
            Get
                If (Me.IsCheckSumNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(12),Integer)
                End If
            End Get
            Set
                Me(12) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.NoteService
            Get
                Return CType(GetServiceObject(GetType(Service.NoteService)),Service.NoteService)
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
        
        Public Function IsModifiedByNull() As Boolean
            Return Me.IsNull(6)
        End Function
        
        Public Sub SetModifiedByToNull()
            Me(6) = System.DBNull.Value
        End Sub
        
        Public Function IsModifiedDateNull() As Boolean
            Return Me.IsNull(7)
        End Function
        
        Public Sub SetModifiedDateToNull()
            Me(7) = System.DBNull.Value
        End Sub
        
        Public Function IsCreatedByNull() As Boolean
            Return Me.IsNull(8)
        End Function
        
        Public Sub SetCreatedByToNull()
            Me(8) = System.DBNull.Value
        End Sub
        
        Public Function IsShortContentNull() As Boolean
            Return Me.IsNull(10)
        End Function
        
        Public Sub SetShortContentToNull()
            Me(10) = System.DBNull.Value
        End Sub
        
        Public Function IsCheckSumNull() As Boolean
            Return Me.IsNull(12)
        End Function
        
        Public Sub SetCheckSumToNull()
            Me(12) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(13)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.NoteSet
            Return NoteBase.GetAll(false, false, NoteServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.NoteSet
            Return NoteBase.GetAll(includeHyphen, false, NoteServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As NoteServiceBase.OrderBy) As EntitySet.NoteSet
            Dim service As Service.NoteService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As NoteServiceBase.OrderBy) As EntitySet.NoteSet
            Return NoteBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal noteId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.Note
            Dim service As Service.NoteService
            service = ServiceObject
            Return service.GetById(NoteId, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal noteId As Integer) As Entity.Note
            Dim service As Service.NoteService
            service = ServiceObject
            Return service.GetById(NoteId)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal noteId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.NoteService
            service = ServiceObject
            Return service.DeleteById(noteId, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal noteId As Integer) As Boolean
            Return NoteBase.DeleteById(noteId, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal noteId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return NoteBase.DeleteById(noteId, 0, transaction)
        End Function
        
        Public Overloads Function GetRelatedPartyNote(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PartyNoteSet
            Return Entity.PartyNote.GetForNote(Me.NoteId, tran)
        End Function
        
        Public Overloads Function GetRelatedPartyNote() As EntitySet.PartyNoteSet
            Return Me.GetRelatedPartyNote(Nothing)
        End Function
        
        Public Overloads Function GetRelatedTaxonomyNote(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.TaxonomyNoteSet
            Return Entity.TaxonomyNote.GetForNote(Me.NoteId, tran)
        End Function
        
        Public Overloads Function GetRelatedTaxonomyNote() As EntitySet.TaxonomyNoteSet
            Return Me.GetRelatedTaxonomyNote(Nothing)
        End Function
        
        Public Overloads Function GetRelatedTaxonomySpeciesBRUDistribution(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.TaxonomySpeciesBRUDistributionSet
            Return Entity.TaxonomySpeciesBRUDistribution.GetForNote(Me.NoteId, tran)
        End Function
        
        Public Overloads Function GetRelatedTaxonomySpeciesBRUDistribution() As EntitySet.TaxonomySpeciesBRUDistributionSet
            Return Me.GetRelatedTaxonomySpeciesBRUDistribution(Nothing)
        End Function
        
        Public Overloads Function GetRelatedApplicationNote(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ApplicationNoteSet
            Return Entity.ApplicationNote.GetForNote(Me.NoteId, tran)
        End Function
        
        Public Overloads Function GetRelatedApplicationNote() As EntitySet.ApplicationNoteSet
            Return Me.GetRelatedApplicationNote(Nothing)
        End Function
        
        Public Shared Function Insert(ByVal [date] As Date, ByVal content As String, ByVal isReadOnly As Boolean, ByVal important As Boolean, ByVal subject As String, ByVal modifiedBy As Object, ByVal modifiedDate As Object, ByVal createdBy As Object, ByVal createdDate As Date, ByVal active As Boolean) As Entity.Note
            Return Entity.Note.ServiceObject.Insert([date], content, isReadOnly, important, subject, modifiedBy, modifiedDate, createdBy, createdDate, active)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim dateParam As Date = Me.Date
            Dim contentParam As String = Me.Content
            Dim isReadOnlyParam As Boolean = Me.IsReadOnly
            Dim importantParam As Boolean = Me.Important
            Dim subjectParam As String = Me.Subject
            Dim modifiedByParam As Object
            If (Me.IsModifiedByNull = false) Then
                modifiedByParam = Me.ModifiedBy
            Else
                modifiedByParam = System.DBNull.Value
            End If
            Dim modifiedDateParam As Object
            If (Me.IsModifiedDateNull = false) Then
                modifiedDateParam = Me.ModifiedDate
            Else
                modifiedDateParam = System.DBNull.Value
            End If
            Dim createdByParam As Object
            If (Me.IsCreatedByNull = false) Then
                createdByParam = Me.CreatedBy
            Else
                createdByParam = System.DBNull.Value
            End If
            Dim createdDateParam As Date = Me.CreatedDate
            Dim activeParam As Boolean = Me.Active
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.Note.ServiceObject.Update(Me.Id, dateParam, contentParam, isReadOnlyParam, importantParam, subjectParam, modifiedByParam, modifiedDateParam, createdByParam, createdDateParam, activeParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace

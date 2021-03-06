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
    
    'Base entity implementation for table 'Contact'
    '*DO NOT* modify this file.
    'Add new properties and methods to Contact instead.
    Public MustInherit Class ContactBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal contactId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(contactId, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal contactId As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(contactId).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property ContactId As Integer
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
        
        <EnterpriseObjects.Attributes.FieldSize(100)>  _
        Public Property ContactDetail As String
            Get
                If (Me.IsContactDetailNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(1),String)
                End If
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        Public Property Active As Boolean
            Get
                Return CType(Me(2),Boolean)
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        Public Property ContactTypeId As Integer
            Get
                Return CType(Me(3),Integer)
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property Location As String
            Get
                If (Me.IsLocationNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(4),String)
                End If
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        Public Overrides Property CheckSum As Integer Implements EnterpriseObjects.IUpdatable.CheckSum
            Get
                If (Me.IsCheckSumNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(5),Integer)
                End If
            End Get
            Set
                Me(5) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.ContactService
            Get
                Return CType(GetServiceObject(GetType(Service.ContactService)),Service.ContactService)
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
        
        Public Function IsContactDetailNull() As Boolean
            Return Me.IsNull(1)
        End Function
        
        Public Sub SetContactDetailToNull()
            Me(1) = System.DBNull.Value
        End Sub
        
        Public Function IsLocationNull() As Boolean
            Return Me.IsNull(4)
        End Function
        
        Public Sub SetLocationToNull()
            Me(4) = System.DBNull.Value
        End Sub
        
        Public Function IsCheckSumNull() As Boolean
            Return Me.IsNull(5)
        End Function
        
        Public Sub SetCheckSumToNull()
            Me(5) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(6)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.ContactSet
            Return ContactBase.GetAll(false, false, ContactServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.ContactSet
            Return ContactBase.GetAll(includeHyphen, false, ContactServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As ContactServiceBase.OrderBy) As EntitySet.ContactSet
            Dim service As Service.ContactService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As ContactServiceBase.OrderBy) As EntitySet.ContactSet
            Return ContactBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal contactId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.Contact
            Dim service As Service.ContactService
            service = ServiceObject
            Return service.GetById(ContactId, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal contactId As Integer) As Entity.Contact
            Dim service As Service.ContactService
            service = ServiceObject
            Return service.GetById(ContactId)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal contactId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.ContactService
            service = ServiceObject
            Return service.DeleteById(contactId, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal contactId As Integer) As Boolean
            Return ContactBase.DeleteById(contactId, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal contactId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return ContactBase.DeleteById(contactId, 0, transaction)
        End Function
        
        Public Overloads Shared Function GetForContactType(ByVal idContactType As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ContactSet
            Dim service As Service.ContactService
            service = ServiceObject
            Return service.GetForContactType(idContactType, tran)
        End Function
        
        Public Overloads Shared Function GetForContactType(ByVal idContactType As Integer) As EntitySet.ContactSet
            Return ContactBase.GetForContactType(idContactType, Nothing)
        End Function
        
        Public Overloads Function GetRelatedPerson(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PersonSet
            Return Entity.Person.GetForContact(Me.ContactId, tran)
        End Function
        
        Public Overloads Function GetRelatedPerson() As EntitySet.PersonSet
            Return Me.GetRelatedPerson(Nothing)
        End Function
        
        Public Overloads Function GetRelatedPersonContacts(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PersonContactsSet
            Return Entity.PersonContacts.GetForContact(Me.ContactId, tran)
        End Function
        
        Public Overloads Function GetRelatedPersonContacts() As EntitySet.PersonContactsSet
            Return Me.GetRelatedPersonContacts(Nothing)
        End Function
        
        Public Shared Function Insert(ByVal contactDetail As Object, ByVal active As Boolean, ByVal contactTypeId As Integer, ByVal location As Object) As Entity.Contact
            Return Entity.Contact.ServiceObject.Insert(contactDetail, active, contactTypeId, location)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim contactDetailParam As Object
            If (Me.IsContactDetailNull = false) Then
                contactDetailParam = EnterpriseObjects.Common.ParseSQLText(Me.ContactDetail)
            Else
                contactDetailParam = System.DBNull.Value
            End If
            Dim activeParam As Boolean = Me.Active
            Dim contactTypeIdParam As Integer = Me.ContactTypeId
            Dim locationParam As Object
            If (Me.IsLocationNull = false) Then
                locationParam = EnterpriseObjects.Common.ParseSQLText(Me.Location)
            Else
                locationParam = System.DBNull.Value
            End If
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.Contact.ServiceObject.Update(Me.Id, contactDetailParam, activeParam, contactTypeIdParam, locationParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace

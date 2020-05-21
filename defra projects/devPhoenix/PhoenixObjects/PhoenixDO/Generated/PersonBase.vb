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
    
    'Base entity implementation for table 'Person'
    '*DO NOT* modify this file.
    'Add new properties and methods to Person instead.
    Public MustInherit Class PersonBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal personId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(personId, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal personId As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(personId).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property PersonId As Integer
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
        
        Public Property PartyId As Integer
            Get
                If (Me.IsPartyIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(1),Integer)
                End If
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        Public Property TitleId As Integer
            Get
                Return CType(Me(2),Integer)
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(255)>  _
        Public Property Forename As String
            Get
                If (Me.IsForenameNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(3),String)
                End If
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(100)>  _
        Public Property Surname As String
            Get
                If (Me.IsSurnameNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(4),String)
                End If
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(8000)>  _
        Public Property DisplayName As String
            Get
                If (Me.IsDisplayNameNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(5),String)
                End If
            End Get
            Set
                Me(5) = value
            End Set
        End Property
        
        Public Property PreferredContactId As Integer
            Get
                If (Me.IsPreferredContactIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(6),Integer)
                End If
            End Get
            Set
                Me(6) = value
            End Set
        End Property
        
        Public Overrides Property CheckSum As Integer Implements EnterpriseObjects.IUpdatable.CheckSum
            Get
                If (Me.IsCheckSumNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(7),Integer)
                End If
            End Get
            Set
                Me(7) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.PersonService
            Get
                Return CType(GetServiceObject(GetType(Service.PersonService)),Service.PersonService)
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
        
        Public Function IsPartyIdNull() As Boolean
            Return Me.IsNull(1)
        End Function
        
        Public Sub SetPartyIdToNull()
            Me(1) = System.DBNull.Value
        End Sub
        
        Public Function IsForenameNull() As Boolean
            Return Me.IsNull(3)
        End Function
        
        Public Sub SetForenameToNull()
            Me(3) = System.DBNull.Value
        End Sub
        
        Public Function IsSurnameNull() As Boolean
            Return Me.IsNull(4)
        End Function
        
        Public Sub SetSurnameToNull()
            Me(4) = System.DBNull.Value
        End Sub
        
        Public Function IsDisplayNameNull() As Boolean
            Return Me.IsNull(5)
        End Function
        
        Public Sub SetDisplayNameToNull()
            Me(5) = System.DBNull.Value
        End Sub
        
        Public Function IsPreferredContactIdNull() As Boolean
            Return Me.IsNull(6)
        End Function
        
        Public Sub SetPreferredContactIdToNull()
            Me(6) = System.DBNull.Value
        End Sub
        
        Public Function IsCheckSumNull() As Boolean
            Return Me.IsNull(7)
        End Function
        
        Public Sub SetCheckSumToNull()
            Me(7) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(8)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.PersonSet
            Return PersonBase.GetAll(false, false, PersonServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.PersonSet
            Return PersonBase.GetAll(includeHyphen, false, PersonServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As PersonServiceBase.OrderBy) As EntitySet.PersonSet
            Dim service As Service.PersonService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As PersonServiceBase.OrderBy) As EntitySet.PersonSet
            Return PersonBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal personId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.Person
            Dim service As Service.PersonService
            service = ServiceObject
            Return service.GetById(PersonId, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal personId As Integer) As Entity.Person
            Dim service As Service.PersonService
            service = ServiceObject
            Return service.GetById(PersonId)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal personId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.PersonService
            service = ServiceObject
            Return service.DeleteById(personId, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal personId As Integer) As Boolean
            Return PersonBase.DeleteById(personId, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal personId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return PersonBase.DeleteById(personId, 0, transaction)
        End Function
        
        Public Overloads Shared Function GetForParty(ByVal partyId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PersonSet
            Dim service As Service.PersonService
            service = ServiceObject
            Return service.GetForParty(partyId, tran)
        End Function
        
        Public Overloads Shared Function GetForParty(ByVal partyId As Integer) As EntitySet.PersonSet
            Return PersonBase.GetForParty(partyId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForTitle(ByVal id As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PersonSet
            Dim service As Service.PersonService
            service = ServiceObject
            Return service.GetForTitle(id, tran)
        End Function
        
        Public Overloads Shared Function GetForTitle(ByVal id As Integer) As EntitySet.PersonSet
            Return PersonBase.GetForTitle(id, Nothing)
        End Function
        
        Public Overloads Shared Function GetForContact(ByVal contactId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PersonSet
            Dim service As Service.PersonService
            service = ServiceObject
            Return service.GetForContact(contactId, tran)
        End Function
        
        Public Overloads Shared Function GetForContact(ByVal contactId As Integer) As EntitySet.PersonSet
            Return PersonBase.GetForContact(contactId, Nothing)
        End Function
        
        Public Overloads Function GetRelatedPersonContacts(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PersonContactsSet
            Return Entity.PersonContacts.GetForPerson(Me.PersonId, tran)
        End Function
        
        Public Overloads Function GetRelatedPersonContacts() As EntitySet.PersonContactsSet
            Return Me.GetRelatedPersonContacts(Nothing)
        End Function
        
        Public Shared Function Insert(ByVal partyId As Object, ByVal titleId As Integer, ByVal forename As Object, ByVal surname As Object, ByVal preferredContactId As Object) As Entity.Person
            Return Entity.Person.ServiceObject.Insert(partyId, titleId, forename, surname, preferredContactId)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim partyIdParam As Object
            If (Me.IsPartyIdNull = false) Then
                partyIdParam = Me.PartyId
            Else
                partyIdParam = System.DBNull.Value
            End If
            Dim titleIdParam As Integer = Me.TitleId
            Dim forenameParam As Object
            If (Me.IsForenameNull = false) Then
                forenameParam = EnterpriseObjects.Common.ParseSQLText(Me.Forename)
            Else
                forenameParam = System.DBNull.Value
            End If
            Dim surnameParam As Object
            If (Me.IsSurnameNull = false) Then
                surnameParam = EnterpriseObjects.Common.ParseSQLText(Me.Surname)
            Else
                surnameParam = System.DBNull.Value
            End If
            Dim preferredContactIdParam As Object
            If (Me.IsPreferredContactIdNull = false) Then
                preferredContactIdParam = Me.PreferredContactId
            Else
                preferredContactIdParam = System.DBNull.Value
            End If
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.Person.ServiceObject.Update(Me.Id, partyIdParam, titleIdParam, forenameParam, surnameParam, preferredContactIdParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace
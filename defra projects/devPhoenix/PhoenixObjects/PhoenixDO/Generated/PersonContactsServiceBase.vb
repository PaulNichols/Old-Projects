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
    
    'Service base implementation for table 'PersonContacts'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class PersonContactsServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.PersonContactsSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.PersonContactsSet
            Return CType(MyBase.GetAll("eosp_SelectPersonContacts", GetType(EntitySet.PersonContactsSet), includeHyphen, includeInactive, orderBy),EntitySet.PersonContactsSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.PersonContactsSet
            Return Me.GetAll(includeHyphen, includeInactive, PersonContactsServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, PersonContactsServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Function GetById(ByVal idColumns() As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.PersonContacts
            Return CType(MyBase.GetById("eosp_SelectPersonContacts", New String() {"ContactId", "PersonId"}, idColumns, GetType(EntitySet.PersonContactsSet), tran),Entity.PersonContacts)
        End Function
        
        Public Overloads Function GetById(ByVal idColumns() As Integer) As Entity.PersonContacts
            Return Me.GetById(idColumns, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal idColumns() As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(idColumns, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal idColumns() As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeletePersonContacts", New String() {"ContactId", "PersonId"}, idColumns, checkSum, transaction)
        End Function
        
        'GetForContact - links to the Contact table...
        Public Overloads Function GetForContact(ByVal ContactId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PersonContactsSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from PersonContacts where ContactId=" + ContactId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.PersonContactsSet), tran),EntitySet.PersonContactsSet)
        End Function
        
        'GetForContact - links to the Contact table...
        Public Overloads Function GetForContact(ByVal ContactId As Integer) As EntitySet.PersonContactsSet
            Return Me.GetForContact(ContactId, Nothing)
        End Function
        
        'GetForPerson - links to the Person table...
        Public Overloads Function GetForPerson(ByVal PersonId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PersonContactsSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from PersonContacts where PersonId=" + PersonId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.PersonContactsSet), tran),EntitySet.PersonContactsSet)
        End Function
        
        'GetForPerson - links to the Person table...
        Public Overloads Function GetForPerson(ByVal PersonId As Integer) As EntitySet.PersonContactsSet
            Return Me.GetForPerson(PersonId, Nothing)
        End Function
        
        Public Overloads Sub Insert(ByVal contactId As Integer, ByVal personId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction)
            Sprocs.eosp_CreatePersonContacts(contactId, personId, transaction)
        End Sub
        
        Public Overloads Sub Insert(ByVal contactId As Integer, ByVal personId As Integer)
            Me.Insert(contactId, personId, Nothing)
        End Sub
        
        Public Overloads Sub Insert(ByVal personContacts As Entity.PersonContacts)
            Me.Insert(personContacts(0), personContacts(1))
        End Sub
        
        Public Overloads Sub Insert(ByVal personContacts As Entity.PersonContacts, ByVal transaction As System.Data.SqlClient.SqlTransaction)
            Me.Insert(personContacts(0), personContacts(1), transaction)
        End Sub
        
        Public Overloads Function Update(ByVal contactId As Integer, ByVal personId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PersonContacts
            Return Sprocs.eosp_UpdatePersonContacts(contactId, personId, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal contactId As Integer, ByVal personId As Integer) As Entity.PersonContacts
            Return Me.Update(contactId, personId, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal contactId As Integer, ByVal personId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PersonContacts
            Return Me.Update(contactId, personId, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal contactId As Integer, ByVal personId As Integer, ByVal checkSum As Integer) As Entity.PersonContacts
            Return Me.Update(contactId, personId, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal personContacts As Entity.PersonContacts) As Entity.PersonContacts
            Return Me.Update(personContacts(0), personContacts(1))
        End Function
        
        Public Overloads Function Update(ByVal personContacts As Entity.PersonContacts, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PersonContacts
            Return Me.Update(personContacts(0), personContacts(1), transaction)
        End Function
        
        Public Overloads Function Update(ByVal personContacts As Entity.PersonContacts, ByVal checkSum As Integer) As Entity.PersonContacts
            Return Me.Update(personContacts(0), personContacts(1), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal personContacts As Entity.PersonContacts, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PersonContacts
            Return Me.Update(personContacts(0), personContacts(1), checkSum, transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
        End Enum
    End Class
End Namespace

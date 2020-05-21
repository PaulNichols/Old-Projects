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
    
    'Base entity implementation for table 'PartyAddresses'
    '*DO NOT* modify this file.
    'Add new properties and methods to PartyAddress instead.
    Public MustInherit Class PartyAddressBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal addressId As Integer, ByVal partyId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(addressId, partyId, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal addressId As Integer, ByVal partyId As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(addressId, partyId).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property AddressId As Integer
            Get
                Return CType(Me(0),Integer)
            End Get
            Set
                Me(0) = value
            End Set
        End Property
        
        Public Property PartyId As Integer
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
        
        Public Shared ReadOnly Property ServiceObject As Service.PartyAddressService
            Get
                Return CType(GetServiceObject(GetType(Service.PartyAddressService)),Service.PartyAddressService)
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
        
        Public Overloads Shared Function GetAll() As EntitySet.PartyAddressSet
            Return PartyAddressBase.GetAll(false, false, PartyAddressServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.PartyAddressSet
            Return PartyAddressBase.GetAll(includeHyphen, false, PartyAddressServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As PartyAddressServiceBase.OrderBy) As EntitySet.PartyAddressSet
            Dim service As Service.PartyAddressService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As PartyAddressServiceBase.OrderBy) As EntitySet.PartyAddressSet
            Return PartyAddressBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal addressId As Integer, ByVal partyId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.PartyAddress
            Dim service As Service.PartyAddressService
            service = ServiceObject
            Return service.GetById(New Integer() {addressId, partyId}, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal addressId As Integer, ByVal partyId As Integer) As Entity.PartyAddress
            Dim service As Service.PartyAddressService
            service = ServiceObject
            Return service.GetById(New Integer() {addressId, partyId})
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal addressId As Integer, ByVal partyId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.PartyAddressService
            service = ServiceObject
            Return service.DeleteById(New Integer() {addressId, partyId}, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal addressId As Integer, ByVal partyId As Integer) As Boolean
            Return PartyAddressBase.DeleteById(addressId, partyId, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal addressId As Integer, ByVal partyId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return PartyAddressBase.DeleteById(addressId, partyId, 0, transaction)
        End Function
        
        Public Overloads Shared Function GetForAddress(ByVal addressId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PartyAddressSet
            Dim service As Service.PartyAddressService
            service = ServiceObject
            Return service.GetForAddress(addressId, tran)
        End Function
        
        Public Overloads Shared Function GetForAddress(ByVal addressId As Integer) As EntitySet.PartyAddressSet
            Return PartyAddressBase.GetForAddress(addressId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForParty(ByVal partyId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PartyAddressSet
            Dim service As Service.PartyAddressService
            service = ServiceObject
            Return service.GetForParty(partyId, tran)
        End Function
        
        Public Overloads Shared Function GetForParty(ByVal partyId As Integer) As EntitySet.PartyAddressSet
            Return PartyAddressBase.GetForParty(partyId, Nothing)
        End Function
        
        Public Shared Sub Insert(ByVal addressId As Integer, ByVal partyId As Integer)
            Entity.PartyAddress.ServiceObject.Insert(addressId, partyId)
        End Sub
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim addressIdParam As Integer = Me.AddressId
            Dim partyIdParam As Integer = Me.PartyId
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.PartyAddress.ServiceObject.Update(addressIdParam, partyIdParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace

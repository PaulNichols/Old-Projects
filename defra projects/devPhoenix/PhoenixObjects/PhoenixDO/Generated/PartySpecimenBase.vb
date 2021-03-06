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
    
    'Base entity implementation for table 'PartySpecimen'
    '*DO NOT* modify this file.
    'Add new properties and methods to PartySpecimen instead.
    <EnterpriseObjects.Attributes.TableDescription("Provides a link between a Party and a Specimen.")>  _
    Public MustInherit Class PartySpecimenBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        <EnterpriseObjects.Attributes.FieldDescription("FK to Specimen")>  _
        Public Property SpecimenID As Integer
            Get
                Return CType(Me(0),Integer)
            End Get
            Set
                Me(0) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldDescription("FK to Party")>  _
        Public Property PartyID As Integer
            Get
                Return CType(Me(1),Integer)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldDescription("FK to address table")>  _
        Public Property AddressId As Integer
            Get
                Return CType(Me(2),Integer)
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldDescription("Association start date")>  _
        Public Property StartDate As Date
            Get
                Return CType(Me(3),Date)
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldDescription("Association end date")>  _
        Public Property EndDate As Date
            Get
                If (Me.IsEndDateNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(4),Date)
                End If
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldDescription("Type 1 = keeper. Others may be added later.")>  _
        Public Property RoleType As Integer
            Get
                Return CType(Me(5),Integer)
            End Get
            Set
                Me(5) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldDescription("See enumeration PartySpecimenStatus for allowed values.")>  _
        Public Property PartySpecimenStatus As Integer
            Get
                If (Me.IsPartySpecimenStatusNull = true) Then
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
        
        Public Shared ReadOnly Property ServiceObject As Service.PartySpecimenService
            Get
                Return CType(GetServiceObject(GetType(Service.PartySpecimenService)),Service.PartySpecimenService)
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
        
        Public Function IsEndDateNull() As Boolean
            Return Me.IsNull(4)
        End Function
        
        Public Sub SetEndDateToNull()
            Me(4) = System.DBNull.Value
        End Sub
        
        Public Function IsPartySpecimenStatusNull() As Boolean
            Return Me.IsNull(6)
        End Function
        
        Public Sub SetPartySpecimenStatusToNull()
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
        
        Public Overloads Shared Function GetAll() As EntitySet.PartySpecimenSet
            Return PartySpecimenBase.GetAll(false, false, PartySpecimenServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.PartySpecimenSet
            Return PartySpecimenBase.GetAll(includeHyphen, false, PartySpecimenServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As PartySpecimenServiceBase.OrderBy) As EntitySet.PartySpecimenSet
            Dim service As Service.PartySpecimenService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As PartySpecimenServiceBase.OrderBy) As EntitySet.PartySpecimenSet
            Return PartySpecimenBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetForSpecimen(ByVal specimenId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PartySpecimenSet
            Dim service As Service.PartySpecimenService
            service = ServiceObject
            Return service.GetForSpecimen(specimenId, tran)
        End Function
        
        Public Overloads Shared Function GetForSpecimen(ByVal specimenId As Integer) As EntitySet.PartySpecimenSet
            Return PartySpecimenBase.GetForSpecimen(specimenId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForParty(ByVal partyId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PartySpecimenSet
            Dim service As Service.PartySpecimenService
            service = ServiceObject
            Return service.GetForParty(partyId, tran)
        End Function
        
        Public Overloads Shared Function GetForParty(ByVal partyId As Integer) As EntitySet.PartySpecimenSet
            Return PartySpecimenBase.GetForParty(partyId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForAddress(ByVal addressId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PartySpecimenSet
            Dim service As Service.PartySpecimenService
            service = ServiceObject
            Return service.GetForAddress(addressId, tran)
        End Function
        
        Public Overloads Shared Function GetForAddress(ByVal addressId As Integer) As EntitySet.PartySpecimenSet
            Return PartySpecimenBase.GetForAddress(addressId, Nothing)
        End Function
        
        Public Shared Sub Insert(ByVal specimenID As Integer, ByVal partyID As Integer, ByVal addressId As Integer, ByVal startDate As Date, ByVal endDate As Object, ByVal roleType As Integer, ByVal partySpecimenStatus As Object)
            Entity.PartySpecimen.ServiceObject.Insert(specimenID, partyID, addressId, startDate, endDate, roleType, partySpecimenStatus)
        End Sub
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim specimenIDParam As Integer = Me.SpecimenID
            Dim partyIDParam As Integer = Me.PartyID
            Dim addressIdParam As Integer = Me.AddressId
            Dim startDateParam As Date = Me.StartDate
            Dim endDateParam As Object
            If (Me.IsEndDateNull = false) Then
                endDateParam = Me.EndDate
            Else
                endDateParam = System.DBNull.Value
            End If
            Dim roleTypeParam As Integer = Me.RoleType
            Dim partySpecimenStatusParam As Object
            If (Me.IsPartySpecimenStatusNull = false) Then
                partySpecimenStatusParam = Me.PartySpecimenStatus
            Else
                partySpecimenStatusParam = System.DBNull.Value
            End If
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Return true
        End Function
    End Class
End Namespace

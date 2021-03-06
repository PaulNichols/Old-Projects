'------------------------------------------------------------------------------
' <autogenerated>
'     This code was generated by a tool.
'     Runtime Version: 1.1.4322.573
'
'     Changes to this file may cause incorrect behavior and will be lost if 
'     the code is regenerated.
' </autogenerated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On


Namespace DataObjects.Base
    
    'Base entity implementation for table 'RingApplication'
    '*DO NOT* modify this file.
    'Add new properties and methods to RingApplication instead.
    Public MustInherit Class RingApplicationBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal ringApplicationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(ringApplicationId, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal ringApplicationId As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(ringApplicationId).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property ApplicationId As Integer
            Get
                Return CType(Me(0),Integer)
            End Get
            Set
                Me(0) = value
            End Set
        End Property
        
        Public Property ProvisionalXML As String
            Get
                Return CType(Me(1),String)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        Public Property RingApplicationId As Integer
            Get
                Return CType(Me(2),Integer)
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        Public Overrides Property Id As Integer
            Get
                Return CType(Me(2),Integer)
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        Public Overrides Property CheckSum As Integer Implements EnterpriseObjects.IUpdatable.CheckSum
            Get
                If (Me.IsCheckSumNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(3),Integer)
                End If
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.RingApplicationService
            Get
                Return CType(GetServiceObject(GetType(Service.RingApplicationService)),Service.RingApplicationService)
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
            Return Me.IsNull(3)
        End Function
        
        Public Sub SetCheckSumToNull()
            Me(3) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(4)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.RingApplicationSet
            Return RingApplicationBase.GetAll(false, false, RingApplicationServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.RingApplicationSet
            Return RingApplicationBase.GetAll(includeHyphen, false, RingApplicationServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As RingApplicationServiceBase.OrderBy) As EntitySet.RingApplicationSet
            Dim service As Service.RingApplicationService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As RingApplicationServiceBase.OrderBy) As EntitySet.RingApplicationSet
            Return RingApplicationBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal ringApplicationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.RingApplication
            Dim service As Service.RingApplicationService
            service = ServiceObject
            Return service.GetById(RingApplicationId, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal ringApplicationId As Integer) As Entity.RingApplication
            Dim service As Service.RingApplicationService
            service = ServiceObject
            Return service.GetById(RingApplicationId)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal ringApplicationId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.RingApplicationService
            service = ServiceObject
            Return service.DeleteById(ringApplicationId, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal ringApplicationId As Integer) As Boolean
            Return RingApplicationBase.DeleteById(ringApplicationId, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal ringApplicationId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return RingApplicationBase.DeleteById(ringApplicationId, 0, transaction)
        End Function
        
        Public Overloads Shared Function GetForApplication(ByVal applicationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.RingApplicationSet
            Dim service As Service.RingApplicationService
            service = ServiceObject
            Return service.GetForApplication(applicationId, tran)
        End Function
        
        Public Overloads Shared Function GetForApplication(ByVal applicationId As Integer) As EntitySet.RingApplicationSet
            Return RingApplicationBase.GetForApplication(applicationId, Nothing)
        End Function
        
        Public Shared Function Insert(ByVal applicationId As Integer, ByVal provisionalXML As String) As Entity.RingApplication
            Return Entity.RingApplication.ServiceObject.Insert(applicationId, provisionalXML)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim applicationIdParam As Integer = Me.ApplicationId
            Dim provisionalXMLParam As String = Me.ProvisionalXML
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.RingApplication.ServiceObject.Update(Me.Id, applicationIdParam, provisionalXMLParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace

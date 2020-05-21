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
    
    'Base entity implementation for table 'ApplicationType'
    '*DO NOT* modify this file.
    'Add new properties and methods to ApplicationType instead.
    <EnterpriseObjects.Attributes.TableDescription("Application or Notification Type")>  _
    Public MustInherit Class ApplicationTypeBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal applicationTypeId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(applicationTypeId, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal applicationTypeId As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(applicationTypeId).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property ApplicationTypeId As Integer
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
        
        <EnterpriseObjects.Attributes.FieldSize(10),  _
         EnterpriseObjects.Attributes.FieldDescription("Code")>  _
        Public Property Code As String
            Get
                Return CType(Me(1),String)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(255),  _
         EnterpriseObjects.Attributes.FieldDescription("Description")>  _
        Public Property Description As String
            Get
                Return CType(Me(2),String)
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        Public Property ServiceLevel As Integer
            Get
                Return CType(Me(3),Integer)
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        Public Property CitesYesNo As Boolean
            Get
                Return CType(Me(4),Boolean)
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        Public Property Active As Boolean
            Get
                Return CType(Me(5),Boolean)
            End Get
            Set
                Me(5) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldDescription("WLRS Service Level(Days)")>  _
        Public Property WLRSService As Integer
            Get
                Return CType(Me(6),Integer)
            End Get
            Set
                Me(6) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldDescription("JNCC Service Level1(Days)")>  _
        Public Property JNCCService1 As Integer
            Get
                Return CType(Me(7),Integer)
            End Get
            Set
                Me(7) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldDescription("JNCC Service Level2(Days)")>  _
        Public Property JNCCService2 As Integer
            Get
                Return CType(Me(8),Integer)
            End Get
            Set
                Me(8) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldDescription("KEW Service Level1(Days)")>  _
        Public Property KewService1 As Integer
            Get
                Return CType(Me(9),Integer)
            End Get
            Set
                Me(9) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldDescription("KEW Service Level2(Days)")>  _
        Public Property KewService2 As Integer
            Get
                Return CType(Me(10),Integer)
            End Get
            Set
                Me(10) = value
            End Set
        End Property
        
        Public Overrides Property CheckSum As Integer Implements EnterpriseObjects.IUpdatable.CheckSum
            Get
                If (Me.IsCheckSumNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(11),Integer)
                End If
            End Get
            Set
                Me(11) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.ApplicationTypeService
            Get
                Return CType(GetServiceObject(GetType(Service.ApplicationTypeService)),Service.ApplicationTypeService)
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
            Return Me.IsNull(11)
        End Function
        
        Public Sub SetCheckSumToNull()
            Me(11) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(12)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.ApplicationTypeSet
            Return ApplicationTypeBase.GetAll(false, false, ApplicationTypeServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.ApplicationTypeSet
            Return ApplicationTypeBase.GetAll(includeHyphen, false, ApplicationTypeServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As ApplicationTypeServiceBase.OrderBy) As EntitySet.ApplicationTypeSet
            Dim service As Service.ApplicationTypeService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As ApplicationTypeServiceBase.OrderBy) As EntitySet.ApplicationTypeSet
            Return ApplicationTypeBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal applicationTypeId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.ApplicationType
            Dim service As Service.ApplicationTypeService
            service = ServiceObject
            Return service.GetById(ApplicationTypeId, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal applicationTypeId As Integer) As Entity.ApplicationType
            Dim service As Service.ApplicationTypeService
            service = ServiceObject
            Return service.GetById(ApplicationTypeId)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal applicationTypeId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.ApplicationTypeService
            service = ServiceObject
            Return service.DeleteById(applicationTypeId, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal applicationTypeId As Integer) As Boolean
            Return ApplicationTypeBase.DeleteById(applicationTypeId, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal applicationTypeId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return ApplicationTypeBase.DeleteById(applicationTypeId, 0, transaction)
        End Function
        
        Public Overloads Function GetRelatedDelegationGuideline(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.DelegationGuidelineSet
            Return Entity.DelegationGuideline.GetForApplicationType(Me.ApplicationTypeId, tran)
        End Function
        
        Public Overloads Function GetRelatedDelegationGuideline() As EntitySet.DelegationGuidelineSet
            Return Me.GetRelatedDelegationGuideline(Nothing)
        End Function
        
        Public Shared Function Insert(ByVal code As String, ByVal description As String, ByVal serviceLevel As Integer, ByVal citesYesNo As Boolean, ByVal active As Boolean, ByVal wLRSService As Integer, ByVal jNCCService1 As Integer, ByVal jNCCService2 As Integer, ByVal kewService1 As Integer, ByVal kewService2 As Integer) As Entity.ApplicationType
            Return Entity.ApplicationType.ServiceObject.Insert(code, description, serviceLevel, citesYesNo, active, wLRSService, jNCCService1, jNCCService2, kewService1, kewService2)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim codeParam As String = Me.Code
            Dim descriptionParam As String = Me.Description
            Dim serviceLevelParam As Integer = Me.ServiceLevel
            Dim citesYesNoParam As Boolean = Me.CitesYesNo
            Dim activeParam As Boolean = Me.Active
            Dim wLRSServiceParam As Integer = Me.WLRSService
            Dim jNCCService1Param As Integer = Me.JNCCService1
            Dim jNCCService2Param As Integer = Me.JNCCService2
            Dim kewService1Param As Integer = Me.KewService1
            Dim kewService2Param As Integer = Me.KewService2
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.ApplicationType.ServiceObject.Update(Me.Id, codeParam, descriptionParam, serviceLevelParam, citesYesNoParam, activeParam, wLRSServiceParam, jNCCService1Param, jNCCService2Param, kewService1Param, kewService2Param, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace

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
    
    'Base entity implementation for table 'SpecialCondition'
    '*DO NOT* modify this file.
    'Add new properties and methods to SpecialCondition instead.
    <EnterpriseObjects.Attributes.TableDescription("Standard Special Condition")>  _
    Public MustInherit Class SpecialConditionBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal specialConditionId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(specialConditionId, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal specialConditionId As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(specialConditionId).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property SpecialConditionId As Integer
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
        
        <EnterpriseObjects.Attributes.FieldSize(50),  _
         EnterpriseObjects.Attributes.FieldDescription("Special Condition Code")>  _
        Public Property Code As String
            Get
                Return CType(Me(1),String)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(899),  _
         EnterpriseObjects.Attributes.FieldDescription("Special Condition Standard Text")>  _
        Public Property Description As String
            Get
                Return CType(Me(2),String)
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        Public Property BFDateRequired As Boolean
            Get
                Return CType(Me(3),Boolean)
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        Public Property Active As Boolean
            Get
                Return CType(Me(4),Boolean)
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(200),  _
         EnterpriseObjects.Attributes.FieldDescription("Special condition Subject")>  _
        Public Property Subject As String
            Get
                Return CType(Me(5),String)
            End Get
            Set
                Me(5) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(253)>  _
        Public Property CodeDescription As String
            Get
                Return CType(Me(6),String)
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
        
        Public Shared ReadOnly Property ServiceObject As Service.SpecialConditionService
            Get
                Return CType(GetServiceObject(GetType(Service.SpecialConditionService)),Service.SpecialConditionService)
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
            Return Me.IsNull(7)
        End Function
        
        Public Sub SetCheckSumToNull()
            Me(7) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(8)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.SpecialConditionSet
            Return SpecialConditionBase.GetAll(false, false, SpecialConditionServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.SpecialConditionSet
            Return SpecialConditionBase.GetAll(includeHyphen, false, SpecialConditionServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As SpecialConditionServiceBase.OrderBy) As EntitySet.SpecialConditionSet
            Dim service As Service.SpecialConditionService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As SpecialConditionServiceBase.OrderBy) As EntitySet.SpecialConditionSet
            Return SpecialConditionBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal specialConditionId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.SpecialCondition
            Dim service As Service.SpecialConditionService
            service = ServiceObject
            Return service.GetById(SpecialConditionId, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal specialConditionId As Integer) As Entity.SpecialCondition
            Dim service As Service.SpecialConditionService
            service = ServiceObject
            Return service.GetById(SpecialConditionId)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal specialConditionId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.SpecialConditionService
            service = ServiceObject
            Return service.DeleteById(specialConditionId, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal specialConditionId As Integer) As Boolean
            Return SpecialConditionBase.DeleteById(specialConditionId, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal specialConditionId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return SpecialConditionBase.DeleteById(specialConditionId, 0, transaction)
        End Function
        
        Public Overloads Function GetRelatedPermitSpecialCondition(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitSpecialConditionSet
            Return Entity.PermitSpecialCondition.GetForSpecialCondition(Me.SpecialConditionId, tran)
        End Function
        
        Public Overloads Function GetRelatedPermitSpecialCondition() As EntitySet.PermitSpecialConditionSet
            Return Me.GetRelatedPermitSpecialCondition(Nothing)
        End Function
        
        Public Shared Function Insert(ByVal code As String, ByVal description As String, ByVal bFDateRequired As Boolean, ByVal active As Boolean, ByVal subject As String) As Entity.SpecialCondition
            Return Entity.SpecialCondition.ServiceObject.Insert(code, description, bFDateRequired, active, subject)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim codeParam As String = Me.Code
            Dim descriptionParam As String = Me.Description
            Dim bFDateRequiredParam As Boolean = Me.BFDateRequired
            Dim activeParam As Boolean = Me.Active
            Dim subjectParam As String = Me.Subject
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.SpecialCondition.ServiceObject.Update(Me.Id, codeParam, descriptionParam, bFDateRequiredParam, activeParam, subjectParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace

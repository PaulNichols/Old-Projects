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
    
    'Base entity implementation for table 'PhoenixParameter'
    '*DO NOT* modify this file.
    'Add new properties and methods to PhoenixParameter instead.
    <EnterpriseObjects.Attributes.TableDescription("Phoenix Parameter")>  _
    Public MustInherit Class PhoenixParameterBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal phoenixParameterID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(phoenixParameterID, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal phoenixParameterID As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(phoenixParameterID).RawDataset.Tables(0).Rows(0))
        End Sub
        
        <EnterpriseObjects.Attributes.FieldDescription("Unique ID")>  _
        Public Property PhoenixParameterID As Integer
            Get
                Return CType(Me(0),Integer)
            End Get
            Set
                Me(0) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldDescription("Unique ID")>  _
        Public Overrides Property Id As Integer
            Get
                Return CType(Me(0),Integer)
            End Get
            Set
                Me(0) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50),  _
         EnterpriseObjects.Attributes.FieldDescription("Parameter")>  _
        Public Property ParamName As String
            Get
                Return CType(Me(1),String)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(255),  _
         EnterpriseObjects.Attributes.FieldDescription(" Value")>  _
        Public Property ParamValue As String
            Get
                Return CType(Me(2),String)
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldDescription("Active flag")>  _
        Public Property Active As Boolean
            Get
                Return CType(Me(3),Boolean)
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldDescription("ID of the validation regular expression table entry to be used to validate the Pa"& _ 
"ramValue")>  _
        Public Property ValidationRegExID As Integer
            Get
                If (Me.IsValidationRegExIDNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(4),Integer)
                End If
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property ValidationMessage As String
            Get
                If (Me.IsValidationMessageNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(5),String)
                End If
            End Get
            Set
                Me(5) = value
            End Set
        End Property
        
        Public Overrides Property CheckSum As Integer Implements EnterpriseObjects.IUpdatable.CheckSum
            Get
                If (Me.IsCheckSumNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(6),Integer)
                End If
            End Get
            Set
                Me(6) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.PhoenixParameterService
            Get
                Return CType(GetServiceObject(GetType(Service.PhoenixParameterService)),Service.PhoenixParameterService)
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
        
        Public Function IsValidationRegExIDNull() As Boolean
            Return Me.IsNull(4)
        End Function
        
        Public Sub SetValidationRegExIDToNull()
            Me(4) = System.DBNull.Value
        End Sub
        
        Public Function IsValidationMessageNull() As Boolean
            Return Me.IsNull(5)
        End Function
        
        Public Sub SetValidationMessageToNull()
            Me(5) = System.DBNull.Value
        End Sub
        
        Public Function IsCheckSumNull() As Boolean
            Return Me.IsNull(6)
        End Function
        
        Public Sub SetCheckSumToNull()
            Me(6) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(7)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.PhoenixParameterSet
            Return PhoenixParameterBase.GetAll(false, false, PhoenixParameterServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.PhoenixParameterSet
            Return PhoenixParameterBase.GetAll(includeHyphen, false, PhoenixParameterServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As PhoenixParameterServiceBase.OrderBy) As EntitySet.PhoenixParameterSet
            Dim service As Service.PhoenixParameterService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As PhoenixParameterServiceBase.OrderBy) As EntitySet.PhoenixParameterSet
            Return PhoenixParameterBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal phoenixParameterID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.PhoenixParameter
            Dim service As Service.PhoenixParameterService
            service = ServiceObject
            Return service.GetById(PhoenixParameterID, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal phoenixParameterID As Integer) As Entity.PhoenixParameter
            Dim service As Service.PhoenixParameterService
            service = ServiceObject
            Return service.GetById(PhoenixParameterID)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal phoenixParameterID As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.PhoenixParameterService
            service = ServiceObject
            Return service.DeleteById(phoenixParameterID, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal phoenixParameterID As Integer) As Boolean
            Return PhoenixParameterBase.DeleteById(phoenixParameterID, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal phoenixParameterID As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return PhoenixParameterBase.DeleteById(phoenixParameterID, 0, transaction)
        End Function
        
        Public Shared Function Insert(ByVal paramName As String, ByVal paramValue As String, ByVal active As Boolean, ByVal validationRegExID As Object, ByVal validationMessage As Object) As Entity.PhoenixParameter
            Return Entity.PhoenixParameter.ServiceObject.Insert(paramName, paramValue, active, validationRegExID, validationMessage)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim paramNameParam As String = Me.ParamName
            Dim paramValueParam As String = Me.ParamValue
            Dim activeParam As Boolean = Me.Active
            Dim validationRegExIDParam As Object
            If (Me.IsValidationRegExIDNull = false) Then
                validationRegExIDParam = Me.ValidationRegExID
            Else
                validationRegExIDParam = System.DBNull.Value
            End If
            Dim validationMessageParam As Object
            If (Me.IsValidationMessageNull = false) Then
                validationMessageParam = EnterpriseObjects.Common.ParseSQLText(Me.ValidationMessage)
            Else
                validationMessageParam = System.DBNull.Value
            End If
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.PhoenixParameter.ServiceObject.Update(Me.Id, paramNameParam, paramValueParam, activeParam, validationRegExIDParam, validationMessageParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace
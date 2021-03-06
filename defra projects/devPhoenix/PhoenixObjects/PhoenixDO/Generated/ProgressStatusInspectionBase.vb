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
    
    'Base entity implementation for table 'ProgressStatusInspection'
    '*DO NOT* modify this file.
    'Add new properties and methods to ProgressStatusInspection instead.
    Public MustInherit Class ProgressStatusInspectionBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal progressStatusInspectionId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(progressStatusInspectionId, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal progressStatusInspectionId As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(progressStatusInspectionId).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property ProgressStatusInspectionId As Integer
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
        
        <EnterpriseObjects.Attributes.FieldSize(30)>  _
        Public Property Description As String
            Get
                Return CType(Me(1),String)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(2)>  _
        Public Property Code As String
            Get
                Return CType(Me(2),String)
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
        
        Public Shared ReadOnly Property ServiceObject As Service.ProgressStatusInspectionService
            Get
                Return CType(GetServiceObject(GetType(Service.ProgressStatusInspectionService)),Service.ProgressStatusInspectionService)
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
        
        Public Overloads Shared Function GetAll() As EntitySet.ProgressStatusInspectionSet
            Return ProgressStatusInspectionBase.GetAll(false, false, ProgressStatusInspectionServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.ProgressStatusInspectionSet
            Return ProgressStatusInspectionBase.GetAll(includeHyphen, false, ProgressStatusInspectionServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As ProgressStatusInspectionServiceBase.OrderBy) As EntitySet.ProgressStatusInspectionSet
            Dim service As Service.ProgressStatusInspectionService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As ProgressStatusInspectionServiceBase.OrderBy) As EntitySet.ProgressStatusInspectionSet
            Return ProgressStatusInspectionBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal progressStatusInspectionId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.ProgressStatusInspection
            Dim service As Service.ProgressStatusInspectionService
            service = ServiceObject
            Return service.GetById(ProgressStatusInspectionId, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal progressStatusInspectionId As Integer) As Entity.ProgressStatusInspection
            Dim service As Service.ProgressStatusInspectionService
            service = ServiceObject
            Return service.GetById(ProgressStatusInspectionId)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal progressStatusInspectionId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.ProgressStatusInspectionService
            service = ServiceObject
            Return service.DeleteById(progressStatusInspectionId, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal progressStatusInspectionId As Integer) As Boolean
            Return ProgressStatusInspectionBase.DeleteById(progressStatusInspectionId, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal progressStatusInspectionId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return ProgressStatusInspectionBase.DeleteById(progressStatusInspectionId, 0, transaction)
        End Function
        
        Public Shared Function Insert(ByVal description As String, ByVal code As String) As Entity.ProgressStatusInspection
            Return Entity.ProgressStatusInspection.ServiceObject.Insert(description, code)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim descriptionParam As String = Me.Description
            Dim codeParam As String = Me.Code
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.ProgressStatusInspection.ServiceObject.Update(Me.Id, descriptionParam, codeParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace

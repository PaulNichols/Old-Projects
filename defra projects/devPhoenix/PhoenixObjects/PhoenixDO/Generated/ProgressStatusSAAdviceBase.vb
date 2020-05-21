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
    
    'Base entity implementation for table 'ProgressStatusSAAdvice'
    '*DO NOT* modify this file.
    'Add new properties and methods to ProgressStatusSAAdvice instead.
    Public MustInherit Class ProgressStatusSAAdviceBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal progressStatusSAAdviceId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(progressStatusSAAdviceId, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal progressStatusSAAdviceId As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(progressStatusSAAdviceId).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property ProgressStatusSAAdviceId As Integer
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
        
        Public Shared ReadOnly Property ServiceObject As Service.ProgressStatusSAAdviceService
            Get
                Return CType(GetServiceObject(GetType(Service.ProgressStatusSAAdviceService)),Service.ProgressStatusSAAdviceService)
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
        
        Public Overloads Shared Function GetAll() As EntitySet.ProgressStatusSAAdviceSet
            Return ProgressStatusSAAdviceBase.GetAll(false, false, ProgressStatusSAAdviceServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.ProgressStatusSAAdviceSet
            Return ProgressStatusSAAdviceBase.GetAll(includeHyphen, false, ProgressStatusSAAdviceServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As ProgressStatusSAAdviceServiceBase.OrderBy) As EntitySet.ProgressStatusSAAdviceSet
            Dim service As Service.ProgressStatusSAAdviceService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As ProgressStatusSAAdviceServiceBase.OrderBy) As EntitySet.ProgressStatusSAAdviceSet
            Return ProgressStatusSAAdviceBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal progressStatusSAAdviceId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.ProgressStatusSAAdvice
            Dim service As Service.ProgressStatusSAAdviceService
            service = ServiceObject
            Return service.GetById(ProgressStatusSAAdviceId, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal progressStatusSAAdviceId As Integer) As Entity.ProgressStatusSAAdvice
            Dim service As Service.ProgressStatusSAAdviceService
            service = ServiceObject
            Return service.GetById(ProgressStatusSAAdviceId)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal progressStatusSAAdviceId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.ProgressStatusSAAdviceService
            service = ServiceObject
            Return service.DeleteById(progressStatusSAAdviceId, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal progressStatusSAAdviceId As Integer) As Boolean
            Return ProgressStatusSAAdviceBase.DeleteById(progressStatusSAAdviceId, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal progressStatusSAAdviceId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return ProgressStatusSAAdviceBase.DeleteById(progressStatusSAAdviceId, 0, transaction)
        End Function
        
        Public Overloads Function GetRelatedPermitHistory(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitHistorySet
            Return Entity.PermitHistory.GetForProgressStatusSAAdvice(Me.ProgressStatusSAAdviceId, tran)
        End Function
        
        Public Overloads Function GetRelatedPermitHistory() As EntitySet.PermitHistorySet
            Return Me.GetRelatedPermitHistory(Nothing)
        End Function
        
        Public Overloads Function GetRelatedPermitInfo(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitInfoSet
            Return Entity.PermitInfo.GetForProgressStatusSAAdvice(Me.ProgressStatusSAAdviceId, tran)
        End Function
        
        Public Overloads Function GetRelatedPermitInfo() As EntitySet.PermitInfoSet
            Return Me.GetRelatedPermitInfo(Nothing)
        End Function
        
        Public Overloads Function GetRelatedScientificAdvice(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ScientificAdviceSet
            Return Entity.ScientificAdvice.GetForProgressStatusSAAdvice(Me.ProgressStatusSAAdviceId, tran)
        End Function
        
        Public Overloads Function GetRelatedScientificAdvice() As EntitySet.ScientificAdviceSet
            Return Me.GetRelatedScientificAdvice(Nothing)
        End Function
        
        Public Shared Function Insert(ByVal description As String, ByVal code As String) As Entity.ProgressStatusSAAdvice
            Return Entity.ProgressStatusSAAdvice.ServiceObject.Insert(description, code)
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
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.ProgressStatusSAAdvice.ServiceObject.Update(Me.Id, descriptionParam, codeParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace

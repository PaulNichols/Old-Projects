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
    
    'Base entity implementation for table 'ProgressStatusReferralHistory'
    '*DO NOT* modify this file.
    'Add new properties and methods to ProgressStatusReferralHistory instead.
    Public MustInherit Class ProgressStatusReferralHistoryBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal progressStatusReferralHistoryId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(progressStatusReferralHistoryId, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal progressStatusReferralHistoryId As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(progressStatusReferralHistoryId).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property ProgressStatusReferralHistoryId As Integer
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
        
        Public Shared ReadOnly Property ServiceObject As Service.ProgressStatusReferralHistoryService
            Get
                Return CType(GetServiceObject(GetType(Service.ProgressStatusReferralHistoryService)),Service.ProgressStatusReferralHistoryService)
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
        
        Public Overloads Shared Function GetAll() As EntitySet.ProgressStatusReferralHistorySet
            Return ProgressStatusReferralHistoryBase.GetAll(false, false, ProgressStatusReferralHistoryServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.ProgressStatusReferralHistorySet
            Return ProgressStatusReferralHistoryBase.GetAll(includeHyphen, false, ProgressStatusReferralHistoryServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As ProgressStatusReferralHistoryServiceBase.OrderBy) As EntitySet.ProgressStatusReferralHistorySet
            Dim service As Service.ProgressStatusReferralHistoryService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As ProgressStatusReferralHistoryServiceBase.OrderBy) As EntitySet.ProgressStatusReferralHistorySet
            Return ProgressStatusReferralHistoryBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal progressStatusReferralHistoryId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.ProgressStatusReferralHistory
            Dim service As Service.ProgressStatusReferralHistoryService
            service = ServiceObject
            Return service.GetById(ProgressStatusReferralHistoryId, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal progressStatusReferralHistoryId As Integer) As Entity.ProgressStatusReferralHistory
            Dim service As Service.ProgressStatusReferralHistoryService
            service = ServiceObject
            Return service.GetById(ProgressStatusReferralHistoryId)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal progressStatusReferralHistoryId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.ProgressStatusReferralHistoryService
            service = ServiceObject
            Return service.DeleteById(progressStatusReferralHistoryId, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal progressStatusReferralHistoryId As Integer) As Boolean
            Return ProgressStatusReferralHistoryBase.DeleteById(progressStatusReferralHistoryId, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal progressStatusReferralHistoryId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return ProgressStatusReferralHistoryBase.DeleteById(progressStatusReferralHistoryId, 0, transaction)
        End Function
        
        Public Overloads Function GetRelatedPermitHistory(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitHistorySet
            Return Entity.PermitHistory.GetForProgressStatusReferralHistory(Me.ProgressStatusReferralHistoryId, tran)
        End Function
        
        Public Overloads Function GetRelatedPermitHistory() As EntitySet.PermitHistorySet
            Return Me.GetRelatedPermitHistory(Nothing)
        End Function
        
        Public Overloads Function GetRelatedPermitInfo(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitInfoSet
            Return Entity.PermitInfo.GetForProgressStatusReferralHistory(Me.ProgressStatusReferralHistoryId, tran)
        End Function
        
        Public Overloads Function GetRelatedPermitInfo() As EntitySet.PermitInfoSet
            Return Me.GetRelatedPermitInfo(Nothing)
        End Function
        
        Public Shared Function Insert(ByVal description As String, ByVal code As String) As Entity.ProgressStatusReferralHistory
            Return Entity.ProgressStatusReferralHistory.ServiceObject.Insert(description, code)
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
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.ProgressStatusReferralHistory.ServiceObject.Update(Me.Id, descriptionParam, codeParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace
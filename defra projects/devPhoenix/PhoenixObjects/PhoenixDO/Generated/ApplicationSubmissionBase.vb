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
    
    'Base entity implementation for table 'ApplicationSubmission'
    '*DO NOT* modify this file.
    'Add new properties and methods to ApplicationSubmission instead.
    Public MustInherit Class ApplicationSubmissionBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal submittedApplicationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(submittedApplicationId, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal submittedApplicationId As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(submittedApplicationId).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property SubmittedApplicationId As Integer
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
        
        Public Property SubmissionDate As Date
            Get
                Return CType(Me(1),Date)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        Public Property CitesApplicationId As Integer
            Get
                If (Me.IsCitesApplicationIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(2),Integer)
                End If
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        Public Property SubmittedBy As Decimal
            Get
                If (Me.IsSubmittedByNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(3),Decimal)
                End If
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        Public Overrides Property CheckSum As Integer Implements EnterpriseObjects.IUpdatable.CheckSum
            Get
                If (Me.IsCheckSumNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(4),Integer)
                End If
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.ApplicationSubmissionService
            Get
                Return CType(GetServiceObject(GetType(Service.ApplicationSubmissionService)),Service.ApplicationSubmissionService)
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
        
        Public Function IsCitesApplicationIdNull() As Boolean
            Return Me.IsNull(2)
        End Function
        
        Public Sub SetCitesApplicationIdToNull()
            Me(2) = System.DBNull.Value
        End Sub
        
        Public Function IsSubmittedByNull() As Boolean
            Return Me.IsNull(3)
        End Function
        
        Public Sub SetSubmittedByToNull()
            Me(3) = System.DBNull.Value
        End Sub
        
        Public Function IsCheckSumNull() As Boolean
            Return Me.IsNull(4)
        End Function
        
        Public Sub SetCheckSumToNull()
            Me(4) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(5)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.ApplicationSubmissionSet
            Return ApplicationSubmissionBase.GetAll(false, false, ApplicationSubmissionServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.ApplicationSubmissionSet
            Return ApplicationSubmissionBase.GetAll(includeHyphen, false, ApplicationSubmissionServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As ApplicationSubmissionServiceBase.OrderBy) As EntitySet.ApplicationSubmissionSet
            Dim service As Service.ApplicationSubmissionService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As ApplicationSubmissionServiceBase.OrderBy) As EntitySet.ApplicationSubmissionSet
            Return ApplicationSubmissionBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal submittedApplicationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.ApplicationSubmission
            Dim service As Service.ApplicationSubmissionService
            service = ServiceObject
            Return service.GetById(SubmittedApplicationId, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal submittedApplicationId As Integer) As Entity.ApplicationSubmission
            Dim service As Service.ApplicationSubmissionService
            service = ServiceObject
            Return service.GetById(SubmittedApplicationId)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal submittedApplicationId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.ApplicationSubmissionService
            service = ServiceObject
            Return service.DeleteById(submittedApplicationId, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal submittedApplicationId As Integer) As Boolean
            Return ApplicationSubmissionBase.DeleteById(submittedApplicationId, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal submittedApplicationId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return ApplicationSubmissionBase.DeleteById(submittedApplicationId, 0, transaction)
        End Function
        
        Public Overloads Shared Function GetForCITESApplication(ByVal citesApplicationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ApplicationSubmissionSet
            Dim service As Service.ApplicationSubmissionService
            service = ServiceObject
            Return service.GetForCITESApplication(citesApplicationId, tran)
        End Function
        
        Public Overloads Shared Function GetForCITESApplication(ByVal citesApplicationId As Integer) As EntitySet.ApplicationSubmissionSet
            Return ApplicationSubmissionBase.GetForCITESApplication(citesApplicationId, Nothing)
        End Function
        
        Public Shared Function Insert(ByVal submissionDate As Date, ByVal citesApplicationId As Object, ByVal submittedBy As Object) As Entity.ApplicationSubmission
            Return Entity.ApplicationSubmission.ServiceObject.Insert(submissionDate, citesApplicationId, submittedBy)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim submissionDateParam As Date = Me.SubmissionDate
            Dim citesApplicationIdParam As Object
            If (Me.IsCitesApplicationIdNull = false) Then
                citesApplicationIdParam = Me.CitesApplicationId
            Else
                citesApplicationIdParam = System.DBNull.Value
            End If
            Dim submittedByParam As Object
            If (Me.IsSubmittedByNull = false) Then
                submittedByParam = Me.SubmittedBy
            Else
                submittedByParam = System.DBNull.Value
            End If
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.ApplicationSubmission.ServiceObject.Update(Me.Id, submissionDateParam, citesApplicationIdParam, submittedByParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace
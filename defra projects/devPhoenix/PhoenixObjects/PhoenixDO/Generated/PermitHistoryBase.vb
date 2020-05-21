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
    
    'Base entity implementation for table 'PermitHistory'
    '*DO NOT* modify this file.
    'Add new properties and methods to PermitHistory instead.
    Public MustInherit Class PermitHistoryBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal permitHistoryId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(permitHistoryId, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal permitHistoryId As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(permitHistoryId).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property PermitHistoryId As Integer
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
        
        Public Property PermitInfoId As Integer
            Get
                Return CType(Me(1),Integer)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        Public Property ProgressStatusPaymentId As Integer
            Get
                If (Me.IsProgressStatusPaymentIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(2),Integer)
                End If
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        Public Property ProgressStatusInspectionId As Integer
            Get
                If (Me.IsProgressStatusInspectionIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(3),Integer)
                End If
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        Public Property ProgressStatusSAAdviceId As Integer
            Get
                If (Me.IsProgressStatusSAAdviceIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(4),Integer)
                End If
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        Public Property ProgressStatusReferralHistoryId As Integer
            Get
                If (Me.IsProgressStatusReferralHistoryIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(5),Integer)
                End If
            End Get
            Set
                Me(5) = value
            End Set
        End Property
        
        Public Property ProgressStatusReIssuedId As Integer
            Get
                If (Me.IsProgressStatusReIssuedIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(6),Integer)
                End If
            End Get
            Set
                Me(6) = value
            End Set
        End Property
        
        Public Property PermitStatusId As Integer
            Get
                If (Me.IsPermitStatusIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(7),Integer)
                End If
            End Get
            Set
                Me(7) = value
            End Set
        End Property
        
        Public Property ChangeDate As Date
            Get
                Return CType(Me(8),Date)
            End Get
            Set
                Me(8) = value
            End Set
        End Property
        
        Public Property ChangedByUserId As Decimal
            Get
                Return CType(Me(9),Decimal)
            End Get
            Set
                Me(9) = value
            End Set
        End Property
        
        Public Property AssignedTo As Integer
            Get
                If (Me.IsAssignedToNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(10),Integer)
                End If
            End Get
            Set
                Me(10) = value
            End Set
        End Property
        
        Public Property NextActionDate As Date
            Get
                If (Me.IsNextActionDateNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(11),Date)
                End If
            End Get
            Set
                Me(11) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(255)>  _
        Public Property CancelReason As String
            Get
                If (Me.IsCancelReasonNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(12),String)
                End If
            End Get
            Set
                Me(12) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(255)>  _
        Public Property CancelPendingReason As String
            Get
                If (Me.IsCancelPendingReasonNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(13),String)
                End If
            End Get
            Set
                Me(13) = value
            End Set
        End Property
        
        Public Property ReportPrintJobId As Integer
            Get
                If (Me.IsReportPrintJobIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(14),Integer)
                End If
            End Get
            Set
                Me(14) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(255)>  _
        Public Property CancelPendingDeclineReason As String
            Get
                If (Me.IsCancelPendingDeclineReasonNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(15),String)
                End If
            End Get
            Set
                Me(15) = value
            End Set
        End Property
        
        Public Property CoveringLetterReportId As Integer
            Get
                If (Me.IsCoveringLetterReportIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(16),Integer)
                End If
            End Get
            Set
                Me(16) = value
            End Set
        End Property
        
        Public Overrides Property CheckSum As Integer Implements EnterpriseObjects.IUpdatable.CheckSum
            Get
                If (Me.IsCheckSumNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(17),Integer)
                End If
            End Get
            Set
                Me(17) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.PermitHistoryService
            Get
                Return CType(GetServiceObject(GetType(Service.PermitHistoryService)),Service.PermitHistoryService)
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
        
        Public Function IsProgressStatusPaymentIdNull() As Boolean
            Return Me.IsNull(2)
        End Function
        
        Public Sub SetProgressStatusPaymentIdToNull()
            Me(2) = System.DBNull.Value
        End Sub
        
        Public Function IsProgressStatusInspectionIdNull() As Boolean
            Return Me.IsNull(3)
        End Function
        
        Public Sub SetProgressStatusInspectionIdToNull()
            Me(3) = System.DBNull.Value
        End Sub
        
        Public Function IsProgressStatusSAAdviceIdNull() As Boolean
            Return Me.IsNull(4)
        End Function
        
        Public Sub SetProgressStatusSAAdviceIdToNull()
            Me(4) = System.DBNull.Value
        End Sub
        
        Public Function IsProgressStatusReferralHistoryIdNull() As Boolean
            Return Me.IsNull(5)
        End Function
        
        Public Sub SetProgressStatusReferralHistoryIdToNull()
            Me(5) = System.DBNull.Value
        End Sub
        
        Public Function IsProgressStatusReIssuedIdNull() As Boolean
            Return Me.IsNull(6)
        End Function
        
        Public Sub SetProgressStatusReIssuedIdToNull()
            Me(6) = System.DBNull.Value
        End Sub
        
        Public Function IsPermitStatusIdNull() As Boolean
            Return Me.IsNull(7)
        End Function
        
        Public Sub SetPermitStatusIdToNull()
            Me(7) = System.DBNull.Value
        End Sub
        
        Public Function IsAssignedToNull() As Boolean
            Return Me.IsNull(10)
        End Function
        
        Public Sub SetAssignedToToNull()
            Me(10) = System.DBNull.Value
        End Sub
        
        Public Function IsNextActionDateNull() As Boolean
            Return Me.IsNull(11)
        End Function
        
        Public Sub SetNextActionDateToNull()
            Me(11) = System.DBNull.Value
        End Sub
        
        Public Function IsCancelReasonNull() As Boolean
            Return Me.IsNull(12)
        End Function
        
        Public Sub SetCancelReasonToNull()
            Me(12) = System.DBNull.Value
        End Sub
        
        Public Function IsCancelPendingReasonNull() As Boolean
            Return Me.IsNull(13)
        End Function
        
        Public Sub SetCancelPendingReasonToNull()
            Me(13) = System.DBNull.Value
        End Sub
        
        Public Function IsReportPrintJobIdNull() As Boolean
            Return Me.IsNull(14)
        End Function
        
        Public Sub SetReportPrintJobIdToNull()
            Me(14) = System.DBNull.Value
        End Sub
        
        Public Function IsCancelPendingDeclineReasonNull() As Boolean
            Return Me.IsNull(15)
        End Function
        
        Public Sub SetCancelPendingDeclineReasonToNull()
            Me(15) = System.DBNull.Value
        End Sub
        
        Public Function IsCoveringLetterReportIdNull() As Boolean
            Return Me.IsNull(16)
        End Function
        
        Public Sub SetCoveringLetterReportIdToNull()
            Me(16) = System.DBNull.Value
        End Sub
        
        Public Function IsCheckSumNull() As Boolean
            Return Me.IsNull(17)
        End Function
        
        Public Sub SetCheckSumToNull()
            Me(17) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(18)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.PermitHistorySet
            Return PermitHistoryBase.GetAll(false, false, PermitHistoryServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.PermitHistorySet
            Return PermitHistoryBase.GetAll(includeHyphen, false, PermitHistoryServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As PermitHistoryServiceBase.OrderBy) As EntitySet.PermitHistorySet
            Dim service As Service.PermitHistoryService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As PermitHistoryServiceBase.OrderBy) As EntitySet.PermitHistorySet
            Return PermitHistoryBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal permitHistoryId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.PermitHistory
            Dim service As Service.PermitHistoryService
            service = ServiceObject
            Return service.GetById(PermitHistoryId, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal permitHistoryId As Integer) As Entity.PermitHistory
            Dim service As Service.PermitHistoryService
            service = ServiceObject
            Return service.GetById(PermitHistoryId)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal permitHistoryId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.PermitHistoryService
            service = ServiceObject
            Return service.DeleteById(permitHistoryId, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal permitHistoryId As Integer) As Boolean
            Return PermitHistoryBase.DeleteById(permitHistoryId, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal permitHistoryId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return PermitHistoryBase.DeleteById(permitHistoryId, 0, transaction)
        End Function
        
        Public Overloads Shared Function GetForPermitInfo(ByVal permitInfoId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitHistorySet
            Dim service As Service.PermitHistoryService
            service = ServiceObject
            Return service.GetForPermitInfo(permitInfoId, tran)
        End Function
        
        Public Overloads Shared Function GetForPermitInfo(ByVal permitInfoId As Integer) As EntitySet.PermitHistorySet
            Return PermitHistoryBase.GetForPermitInfo(permitInfoId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForProgressStatusPayment(ByVal progressStatusPaymentId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitHistorySet
            Dim service As Service.PermitHistoryService
            service = ServiceObject
            Return service.GetForProgressStatusPayment(progressStatusPaymentId, tran)
        End Function
        
        Public Overloads Shared Function GetForProgressStatusPayment(ByVal progressStatusPaymentId As Integer) As EntitySet.PermitHistorySet
            Return PermitHistoryBase.GetForProgressStatusPayment(progressStatusPaymentId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForProgressStatusSAAdvice(ByVal progressStatusSAAdviceId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitHistorySet
            Dim service As Service.PermitHistoryService
            service = ServiceObject
            Return service.GetForProgressStatusSAAdvice(progressStatusSAAdviceId, tran)
        End Function
        
        Public Overloads Shared Function GetForProgressStatusSAAdvice(ByVal progressStatusSAAdviceId As Integer) As EntitySet.PermitHistorySet
            Return PermitHistoryBase.GetForProgressStatusSAAdvice(progressStatusSAAdviceId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForProgressStatusReferralHistory(ByVal progressStatusReferralHistoryId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitHistorySet
            Dim service As Service.PermitHistoryService
            service = ServiceObject
            Return service.GetForProgressStatusReferralHistory(progressStatusReferralHistoryId, tran)
        End Function
        
        Public Overloads Shared Function GetForProgressStatusReferralHistory(ByVal progressStatusReferralHistoryId As Integer) As EntitySet.PermitHistorySet
            Return PermitHistoryBase.GetForProgressStatusReferralHistory(progressStatusReferralHistoryId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForProgressStatusReIssued(ByVal progressStatusReIssuedId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitHistorySet
            Dim service As Service.PermitHistoryService
            service = ServiceObject
            Return service.GetForProgressStatusReIssued(progressStatusReIssuedId, tran)
        End Function
        
        Public Overloads Shared Function GetForProgressStatusReIssued(ByVal progressStatusReIssuedId As Integer) As EntitySet.PermitHistorySet
            Return PermitHistoryBase.GetForProgressStatusReIssued(progressStatusReIssuedId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForPermitStatus(ByVal permitStatusId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitHistorySet
            Dim service As Service.PermitHistoryService
            service = ServiceObject
            Return service.GetForPermitStatus(permitStatusId, tran)
        End Function
        
        Public Overloads Shared Function GetForPermitStatus(ByVal permitStatusId As Integer) As EntitySet.PermitHistorySet
            Return PermitHistoryBase.GetForPermitStatus(permitStatusId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForStatusAssignedToGroup(ByVal statusAssignedToGroupId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitHistorySet
            Dim service As Service.PermitHistoryService
            service = ServiceObject
            Return service.GetForStatusAssignedToGroup(statusAssignedToGroupId, tran)
        End Function
        
        Public Overloads Shared Function GetForStatusAssignedToGroup(ByVal statusAssignedToGroupId As Integer) As EntitySet.PermitHistorySet
            Return PermitHistoryBase.GetForStatusAssignedToGroup(statusAssignedToGroupId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForReportPrintJob(ByVal reportPrintJobId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitHistorySet
            Dim service As Service.PermitHistoryService
            service = ServiceObject
            Return service.GetForReportPrintJob(reportPrintJobId, tran)
        End Function
        
        Public Overloads Shared Function GetForReportPrintJob(ByVal reportPrintJobId As Integer) As EntitySet.PermitHistorySet
            Return PermitHistoryBase.GetForReportPrintJob(reportPrintJobId, Nothing)
        End Function
        
        Public Shared Function Insert( _
                    ByVal permitInfoId As Integer,  _
                    ByVal progressStatusPaymentId As Object,  _
                    ByVal progressStatusInspectionId As Object,  _
                    ByVal progressStatusSAAdviceId As Object,  _
                    ByVal progressStatusReferralHistoryId As Object,  _
                    ByVal progressStatusReIssuedId As Object,  _
                    ByVal permitStatusId As Object,  _
                    ByVal changeDate As Date,  _
                    ByVal changedByUserId As Decimal,  _
                    ByVal assignedTo As Object,  _
                    ByVal nextActionDate As Object,  _
                    ByVal cancelReason As Object,  _
                    ByVal cancelPendingReason As Object,  _
                    ByVal reportPrintJobId As Object,  _
                    ByVal cancelPendingDeclineReason As Object,  _
                    ByVal coveringLetterReportId As Object) As Entity.PermitHistory
            Return Entity.PermitHistory.ServiceObject.Insert(permitInfoId, progressStatusPaymentId, progressStatusInspectionId, progressStatusSAAdviceId, progressStatusReferralHistoryId, progressStatusReIssuedId, permitStatusId, changeDate, changedByUserId, assignedTo, nextActionDate, cancelReason, cancelPendingReason, reportPrintJobId, cancelPendingDeclineReason, coveringLetterReportId)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim permitInfoIdParam As Integer = Me.PermitInfoId
            Dim progressStatusPaymentIdParam As Object
            If (Me.IsProgressStatusPaymentIdNull = false) Then
                progressStatusPaymentIdParam = Me.ProgressStatusPaymentId
            Else
                progressStatusPaymentIdParam = System.DBNull.Value
            End If
            Dim progressStatusInspectionIdParam As Object
            If (Me.IsProgressStatusInspectionIdNull = false) Then
                progressStatusInspectionIdParam = Me.ProgressStatusInspectionId
            Else
                progressStatusInspectionIdParam = System.DBNull.Value
            End If
            Dim progressStatusSAAdviceIdParam As Object
            If (Me.IsProgressStatusSAAdviceIdNull = false) Then
                progressStatusSAAdviceIdParam = Me.ProgressStatusSAAdviceId
            Else
                progressStatusSAAdviceIdParam = System.DBNull.Value
            End If
            Dim progressStatusReferralHistoryIdParam As Object
            If (Me.IsProgressStatusReferralHistoryIdNull = false) Then
                progressStatusReferralHistoryIdParam = Me.ProgressStatusReferralHistoryId
            Else
                progressStatusReferralHistoryIdParam = System.DBNull.Value
            End If
            Dim progressStatusReIssuedIdParam As Object
            If (Me.IsProgressStatusReIssuedIdNull = false) Then
                progressStatusReIssuedIdParam = Me.ProgressStatusReIssuedId
            Else
                progressStatusReIssuedIdParam = System.DBNull.Value
            End If
            Dim permitStatusIdParam As Object
            If (Me.IsPermitStatusIdNull = false) Then
                permitStatusIdParam = Me.PermitStatusId
            Else
                permitStatusIdParam = System.DBNull.Value
            End If
            Dim changeDateParam As Date = Me.ChangeDate
            Dim changedByUserIdParam As Decimal = Me.ChangedByUserId
            Dim assignedToParam As Object
            If (Me.IsAssignedToNull = false) Then
                assignedToParam = Me.AssignedTo
            Else
                assignedToParam = System.DBNull.Value
            End If
            Dim nextActionDateParam As Object
            If (Me.IsNextActionDateNull = false) Then
                nextActionDateParam = Me.NextActionDate
            Else
                nextActionDateParam = System.DBNull.Value
            End If
            Dim cancelReasonParam As Object
            If (Me.IsCancelReasonNull = false) Then
                cancelReasonParam = EnterpriseObjects.Common.ParseSQLText(Me.CancelReason)
            Else
                cancelReasonParam = System.DBNull.Value
            End If
            Dim cancelPendingReasonParam As Object
            If (Me.IsCancelPendingReasonNull = false) Then
                cancelPendingReasonParam = EnterpriseObjects.Common.ParseSQLText(Me.CancelPendingReason)
            Else
                cancelPendingReasonParam = System.DBNull.Value
            End If
            Dim reportPrintJobIdParam As Object
            If (Me.IsReportPrintJobIdNull = false) Then
                reportPrintJobIdParam = Me.ReportPrintJobId
            Else
                reportPrintJobIdParam = System.DBNull.Value
            End If
            Dim cancelPendingDeclineReasonParam As Object
            If (Me.IsCancelPendingDeclineReasonNull = false) Then
                cancelPendingDeclineReasonParam = EnterpriseObjects.Common.ParseSQLText(Me.CancelPendingDeclineReason)
            Else
                cancelPendingDeclineReasonParam = System.DBNull.Value
            End If
            Dim coveringLetterReportIdParam As Object
            If (Me.IsCoveringLetterReportIdNull = false) Then
                coveringLetterReportIdParam = Me.CoveringLetterReportId
            Else
                coveringLetterReportIdParam = System.DBNull.Value
            End If
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.PermitHistory.ServiceObject.Update(Me.Id, permitInfoIdParam, progressStatusPaymentIdParam, progressStatusInspectionIdParam, progressStatusSAAdviceIdParam, progressStatusReferralHistoryIdParam, progressStatusReIssuedIdParam, permitStatusIdParam, changeDateParam, changedByUserIdParam, assignedToParam, nextActionDateParam, cancelReasonParam, cancelPendingReasonParam, reportPrintJobIdParam, cancelPendingDeclineReasonParam, coveringLetterReportIdParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace

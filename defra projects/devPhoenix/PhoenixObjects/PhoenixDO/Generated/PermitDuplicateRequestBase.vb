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
    
    'Base entity implementation for table 'PermitDuplicateRequest'
    '*DO NOT* modify this file.
    'Add new properties and methods to PermitDuplicateRequest instead.
    Public MustInherit Class PermitDuplicateRequestBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal permitDuplicateRequestId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(permitDuplicateRequestId, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal permitDuplicateRequestId As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(permitDuplicateRequestId).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property PermitDuplicateRequestId As Integer
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
        
        Public Property DuplicateReasonId As Integer
            Get
                Return CType(Me(2),Integer)
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(255)>  _
        Public Property DuplicateReasonDetails As String
            Get
                If (Me.IsDuplicateReasonDetailsNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(3),String)
                End If
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        Public Property DuplicateIssueDate As Date
            Get
                Return CType(Me(4),Date)
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        Public Property ReportPrintJobId As Integer
            Get
                If (Me.IsReportPrintJobIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(5),Integer)
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
        
        Public Shared ReadOnly Property ServiceObject As Service.PermitDuplicateRequestService
            Get
                Return CType(GetServiceObject(GetType(Service.PermitDuplicateRequestService)),Service.PermitDuplicateRequestService)
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
        
        Public Function IsDuplicateReasonDetailsNull() As Boolean
            Return Me.IsNull(3)
        End Function
        
        Public Sub SetDuplicateReasonDetailsToNull()
            Me(3) = System.DBNull.Value
        End Sub
        
        Public Function IsReportPrintJobIdNull() As Boolean
            Return Me.IsNull(5)
        End Function
        
        Public Sub SetReportPrintJobIdToNull()
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
        
        Public Overloads Shared Function GetAll() As EntitySet.PermitDuplicateRequestSet
            Return PermitDuplicateRequestBase.GetAll(false, false, PermitDuplicateRequestServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.PermitDuplicateRequestSet
            Return PermitDuplicateRequestBase.GetAll(includeHyphen, false, PermitDuplicateRequestServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As PermitDuplicateRequestServiceBase.OrderBy) As EntitySet.PermitDuplicateRequestSet
            Dim service As Service.PermitDuplicateRequestService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As PermitDuplicateRequestServiceBase.OrderBy) As EntitySet.PermitDuplicateRequestSet
            Return PermitDuplicateRequestBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal permitDuplicateRequestId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.PermitDuplicateRequest
            Dim service As Service.PermitDuplicateRequestService
            service = ServiceObject
            Return service.GetById(PermitDuplicateRequestId, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal permitDuplicateRequestId As Integer) As Entity.PermitDuplicateRequest
            Dim service As Service.PermitDuplicateRequestService
            service = ServiceObject
            Return service.GetById(PermitDuplicateRequestId)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal permitDuplicateRequestId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.PermitDuplicateRequestService
            service = ServiceObject
            Return service.DeleteById(permitDuplicateRequestId, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal permitDuplicateRequestId As Integer) As Boolean
            Return PermitDuplicateRequestBase.DeleteById(permitDuplicateRequestId, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal permitDuplicateRequestId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return PermitDuplicateRequestBase.DeleteById(permitDuplicateRequestId, 0, transaction)
        End Function
        
        Public Overloads Shared Function GetForPermitInfo(ByVal permitInfoId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitDuplicateRequestSet
            Dim service As Service.PermitDuplicateRequestService
            service = ServiceObject
            Return service.GetForPermitInfo(permitInfoId, tran)
        End Function
        
        Public Overloads Shared Function GetForPermitInfo(ByVal permitInfoId As Integer) As EntitySet.PermitDuplicateRequestSet
            Return PermitDuplicateRequestBase.GetForPermitInfo(permitInfoId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForDuplicateReason(ByVal duplicateReasonId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitDuplicateRequestSet
            Dim service As Service.PermitDuplicateRequestService
            service = ServiceObject
            Return service.GetForDuplicateReason(duplicateReasonId, tran)
        End Function
        
        Public Overloads Shared Function GetForDuplicateReason(ByVal duplicateReasonId As Integer) As EntitySet.PermitDuplicateRequestSet
            Return PermitDuplicateRequestBase.GetForDuplicateReason(duplicateReasonId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForReportPrintJob(ByVal reportPrintJobId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitDuplicateRequestSet
            Dim service As Service.PermitDuplicateRequestService
            service = ServiceObject
            Return service.GetForReportPrintJob(reportPrintJobId, tran)
        End Function
        
        Public Overloads Shared Function GetForReportPrintJob(ByVal reportPrintJobId As Integer) As EntitySet.PermitDuplicateRequestSet
            Return PermitDuplicateRequestBase.GetForReportPrintJob(reportPrintJobId, Nothing)
        End Function
        
        Public Shared Function Insert(ByVal permitInfoId As Integer, ByVal duplicateReasonId As Integer, ByVal duplicateReasonDetails As Object, ByVal duplicateIssueDate As Date, ByVal reportPrintJobId As Object) As Entity.PermitDuplicateRequest
            Return Entity.PermitDuplicateRequest.ServiceObject.Insert(permitInfoId, duplicateReasonId, duplicateReasonDetails, duplicateIssueDate, reportPrintJobId)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim permitInfoIdParam As Integer = Me.PermitInfoId
            Dim duplicateReasonIdParam As Integer = Me.DuplicateReasonId
            Dim duplicateReasonDetailsParam As Object
            If (Me.IsDuplicateReasonDetailsNull = false) Then
                duplicateReasonDetailsParam = EnterpriseObjects.Common.ParseSQLText(Me.DuplicateReasonDetails)
            Else
                duplicateReasonDetailsParam = System.DBNull.Value
            End If
            Dim duplicateIssueDateParam As Date = Me.DuplicateIssueDate
            Dim reportPrintJobIdParam As Object
            If (Me.IsReportPrintJobIdNull = false) Then
                reportPrintJobIdParam = Me.ReportPrintJobId
            Else
                reportPrintJobIdParam = System.DBNull.Value
            End If
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.PermitDuplicateRequest.ServiceObject.Update(Me.Id, permitInfoIdParam, duplicateReasonIdParam, duplicateReasonDetailsParam, duplicateIssueDateParam, reportPrintJobIdParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace

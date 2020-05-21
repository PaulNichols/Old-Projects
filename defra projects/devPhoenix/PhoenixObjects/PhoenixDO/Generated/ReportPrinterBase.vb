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
    
    'Base entity implementation for table 'ReportPrinter'
    '*DO NOT* modify this file.
    'Add new properties and methods to ReportPrinter instead.
    Public MustInherit Class ReportPrinterBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal reportPrinterId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(reportPrinterId, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal reportPrinterId As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(reportPrinterId).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property ReportPrinterId As Integer
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
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property Name As String
            Get
                Return CType(Me(1),String)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(255)>  _
        Public Property NetworkPath As String
            Get
                Return CType(Me(2),String)
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        Public Property PausedDate As Date
            Get
                If (Me.IsPausedDateNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(3),Date)
                End If
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property PausedBy As String
            Get
                If (Me.IsPausedByNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(4),String)
                End If
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        Public Overrides Property CheckSum As Integer Implements EnterpriseObjects.IUpdatable.CheckSum
            Get
                If (Me.IsCheckSumNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(5),Integer)
                End If
            End Get
            Set
                Me(5) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.ReportPrinterService
            Get
                Return CType(GetServiceObject(GetType(Service.ReportPrinterService)),Service.ReportPrinterService)
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
        
        Public Function IsPausedDateNull() As Boolean
            Return Me.IsNull(3)
        End Function
        
        Public Sub SetPausedDateToNull()
            Me(3) = System.DBNull.Value
        End Sub
        
        Public Function IsPausedByNull() As Boolean
            Return Me.IsNull(4)
        End Function
        
        Public Sub SetPausedByToNull()
            Me(4) = System.DBNull.Value
        End Sub
        
        Public Function IsCheckSumNull() As Boolean
            Return Me.IsNull(5)
        End Function
        
        Public Sub SetCheckSumToNull()
            Me(5) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(6)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.ReportPrinterSet
            Return ReportPrinterBase.GetAll(false, false, ReportPrinterServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.ReportPrinterSet
            Return ReportPrinterBase.GetAll(includeHyphen, false, ReportPrinterServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As ReportPrinterServiceBase.OrderBy) As EntitySet.ReportPrinterSet
            Dim service As Service.ReportPrinterService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As ReportPrinterServiceBase.OrderBy) As EntitySet.ReportPrinterSet
            Return ReportPrinterBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal reportPrinterId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.ReportPrinter
            Dim service As Service.ReportPrinterService
            service = ServiceObject
            Return service.GetById(ReportPrinterId, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal reportPrinterId As Integer) As Entity.ReportPrinter
            Dim service As Service.ReportPrinterService
            service = ServiceObject
            Return service.GetById(ReportPrinterId)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal reportPrinterId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.ReportPrinterService
            service = ServiceObject
            Return service.DeleteById(reportPrinterId, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal reportPrinterId As Integer) As Boolean
            Return ReportPrinterBase.DeleteById(reportPrinterId, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal reportPrinterId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return ReportPrinterBase.DeleteById(reportPrinterId, 0, transaction)
        End Function
        
        Public Overloads Function GetRelatedReportAuthorisedQ(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ReportAuthorisedQSet
            Return Entity.ReportAuthorisedQ.GetForReportPrinter(Me.ReportPrinterId, tran)
        End Function
        
        Public Overloads Function GetRelatedReportAuthorisedQ() As EntitySet.ReportAuthorisedQSet
            Return Me.GetRelatedReportAuthorisedQ(Nothing)
        End Function
        
        Public Overloads Function GetRelatedReportType(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ReportTypeSet
            Return Entity.ReportType.GetForReportPrinter(Me.ReportPrinterId, tran)
        End Function
        
        Public Overloads Function GetRelatedReportType() As EntitySet.ReportTypeSet
            Return Me.GetRelatedReportType(Nothing)
        End Function
        
        Public Overloads Function GetRelatedReportTypeReportPrinter(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ReportTypeReportPrinterSet
            Return Entity.ReportTypeReportPrinter.GetForReportPrinter(Me.ReportPrinterId, tran)
        End Function
        
        Public Overloads Function GetRelatedReportTypeReportPrinter() As EntitySet.ReportTypeReportPrinterSet
            Return Me.GetRelatedReportTypeReportPrinter(Nothing)
        End Function
        
        Public Shared Function Insert(ByVal name As String, ByVal networkPath As String, ByVal pausedDate As Object, ByVal pausedBy As Object) As Entity.ReportPrinter
            Return Entity.ReportPrinter.ServiceObject.Insert(name, networkPath, pausedDate, pausedBy)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim nameParam As String = Me.Name
            Dim networkPathParam As String = Me.NetworkPath
            Dim pausedDateParam As Object
            If (Me.IsPausedDateNull = false) Then
                pausedDateParam = Me.PausedDate
            Else
                pausedDateParam = System.DBNull.Value
            End If
            Dim pausedByParam As Object
            If (Me.IsPausedByNull = false) Then
                pausedByParam = EnterpriseObjects.Common.ParseSQLText(Me.PausedBy)
            Else
                pausedByParam = System.DBNull.Value
            End If
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.ReportPrinter.ServiceObject.Update(Me.Id, nameParam, networkPathParam, pausedDateParam, pausedByParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace
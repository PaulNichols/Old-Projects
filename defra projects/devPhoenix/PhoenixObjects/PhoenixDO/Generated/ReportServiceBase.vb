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
    
    'Service base implementation for table 'Report'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class ReportServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.ReportSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.ReportSet
            Return CType(MyBase.GetAll("eosp_SelectReport", GetType(EntitySet.ReportSet), includeHyphen, includeInactive, orderBy),EntitySet.ReportSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.ReportSet
            Return Me.GetAll(includeHyphen, includeInactive, ReportServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, ReportServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal reportId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectReport", "ReportId", reportId, GetType(EntitySet.ReportSet), tran),Entity.Report)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal reportId As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(reportId, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal reportId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.Report
            Return CType(MyBase.GetById("eosp_SelectReport", "ReportId", reportId, GetType(EntitySet.ReportSet), tran),Entity.Report)
        End Function
        
        Public Overloads Function GetById(ByVal reportId As Integer) As Entity.Report
            Return Me.GetById(reportId, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal reportId As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(reportId, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal reportId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeleteReport", "ReportId", reportId, checkSum, transaction)
        End Function
        
        'GetForReportType - links to the ReportType table...
        Public Overloads Function GetForReportType(ByVal ReportTypeId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ReportSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from Report where ReportTypeId=" + ReportTypeId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.ReportSet), tran),EntitySet.ReportSet)
        End Function
        
        'GetForReportType - links to the ReportType table...
        Public Overloads Function GetForReportType(ByVal ReportTypeId As Integer) As EntitySet.ReportSet
            Return Me.GetForReportType(ReportTypeId, Nothing)
        End Function
        
        'GetForReportPrintJob - links to the ReportPrintJob table...
        Public Overloads Function GetForReportPrintJob(ByVal ReportPrintJobId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ReportSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from Report where ReportPrintJobId=" + ReportPrintJobId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.ReportSet), tran),EntitySet.ReportSet)
        End Function
        
        'GetForReportPrintJob - links to the ReportPrintJob table...
        Public Overloads Function GetForReportPrintJob(ByVal ReportPrintJobId As Integer) As EntitySet.ReportSet
            Return Me.GetForReportPrintJob(ReportPrintJobId, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal createdDate As Date, ByVal reportTypeId As Integer, ByVal version As Integer, ByVal searchReference As String, ByVal databaseId As Object, ByVal reportPrintJobId As Integer, ByVal printSequence As Integer, ByVal expiryDate As Object, ByVal size As Integer, ByVal reportPrinterId As Integer, ByVal staple As Integer, ByVal description As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Report
            Return Me.GetById(Sprocs.eosp_CreateReport(createdDate, reportTypeId, version, searchReference, databaseId, reportPrintJobId, printSequence, expiryDate, size, reportPrinterId, staple, description, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal createdDate As Date, ByVal reportTypeId As Integer, ByVal version As Integer, ByVal searchReference As String, ByVal databaseId As Object, ByVal reportPrintJobId As Integer, ByVal printSequence As Integer, ByVal expiryDate As Object, ByVal size As Integer, ByVal reportPrinterId As Integer, ByVal staple As Integer, ByVal description As Object) As Entity.Report
            Return Me.Insert(createdDate, reportTypeId, version, searchReference, databaseId, reportPrintJobId, printSequence, expiryDate, size, reportPrinterId, staple, description, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal report As Entity.Report) As Entity.Report
            Return Me.Insert(report(1), report(2), report(3), report(4), report(5), report(6), report(7), report(8), report(9), report(10), report(11), report(12))
        End Function
        
        Public Overloads Function Insert(ByVal report As Entity.Report, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Report
            Return Me.Insert(report(1), report(2), report(3), report(4), report(5), report(6), report(7), report(8), report(9), report(10), report(11), report(12), transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal createdDate As Date, ByVal reportTypeId As Integer, ByVal version As Integer, ByVal searchReference As String, ByVal databaseId As Object, ByVal reportPrintJobId As Integer, ByVal printSequence As Integer, ByVal expiryDate As Object, ByVal size As Integer, ByVal reportPrinterId As Integer, ByVal staple As Integer, ByVal description As Object, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Report
            Return Sprocs.eosp_UpdateReport(id, createdDate, reportTypeId, version, searchReference, databaseId, reportPrintJobId, printSequence, expiryDate, size, reportPrinterId, staple, description, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal createdDate As Date, ByVal reportTypeId As Integer, ByVal version As Integer, ByVal searchReference As String, ByVal databaseId As Object, ByVal reportPrintJobId As Integer, ByVal printSequence As Integer, ByVal expiryDate As Object, ByVal size As Integer, ByVal reportPrinterId As Integer, ByVal staple As Integer, ByVal description As Object) As Entity.Report
            Return Me.Update(id, createdDate, reportTypeId, version, searchReference, databaseId, reportPrintJobId, printSequence, expiryDate, size, reportPrinterId, staple, description, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal createdDate As Date, ByVal reportTypeId As Integer, ByVal version As Integer, ByVal searchReference As String, ByVal databaseId As Object, ByVal reportPrintJobId As Integer, ByVal printSequence As Integer, ByVal expiryDate As Object, ByVal size As Integer, ByVal reportPrinterId As Integer, ByVal staple As Integer, ByVal description As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Report
            Return Me.Update(id, createdDate, reportTypeId, version, searchReference, databaseId, reportPrintJobId, printSequence, expiryDate, size, reportPrinterId, staple, description, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal createdDate As Date, ByVal reportTypeId As Integer, ByVal version As Integer, ByVal searchReference As String, ByVal databaseId As Object, ByVal reportPrintJobId As Integer, ByVal printSequence As Integer, ByVal expiryDate As Object, ByVal size As Integer, ByVal reportPrinterId As Integer, ByVal staple As Integer, ByVal description As Object, ByVal checkSum As Integer) As Entity.Report
            Return Me.Update(id, createdDate, reportTypeId, version, searchReference, databaseId, reportPrintJobId, printSequence, expiryDate, size, reportPrinterId, staple, description, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal report As Entity.Report) As Entity.Report
            Return Me.Update(report.id, report(1), report(2), report(3), report(4), report(5), report(6), report(7), report(8), report(9), report(10), report(11), report(12))
        End Function
        
        Public Overloads Function Update(ByVal report As Entity.Report, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Report
            Return Me.Update(report.id, report(1), report(2), report(3), report(4), report(5), report(6), report(7), report(8), report(9), report(10), report(11), report(12), transaction)
        End Function
        
        Public Overloads Function Update(ByVal report As Entity.Report, ByVal checkSum As Integer) As Entity.Report
            Return Me.Update(report.id, report(1), report(2), report(3), report(4), report(5), report(6), report(7), report(8), report(9), report(10), report(11), report(12), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal report As Entity.Report, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Report
            Return Me.Update(report.id, report(1), report(2), report(3), report(4), report(5), report(6), report(7), report(8), report(9), report(10), report(11), report(12), checkSum, transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
        End Enum
    End Class
End Namespace

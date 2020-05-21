'------------------------------------------------------------------------------
' <autogenerated>
'     This code was generated by a tool.
'     Runtime Version: 1.1.4322.573
'
'     Changes to this file may cause incorrect behavior and will be lost if 
'     the code is regenerated.
' </autogenerated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On


Namespace DataObjects.Base
    
    'Service base implementation for table 'ReportTypeReportPrinter'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class ReportTypeReportPrinterServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.ReportTypeReportPrinterSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.ReportTypeReportPrinterSet
            Return CType(MyBase.GetAll("eosp_SelectReportTypeReportPrinter", GetType(EntitySet.ReportTypeReportPrinterSet), includeHyphen, includeInactive, orderBy),EntitySet.ReportTypeReportPrinterSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.ReportTypeReportPrinterSet
            Return Me.GetAll(includeHyphen, includeInactive, ReportTypeReportPrinterServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, ReportTypeReportPrinterServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Function GetById(ByVal idColumns() As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.ReportTypeReportPrinter
            Return CType(MyBase.GetById("eosp_SelectReportTypeReportPrinter", New String() {"ReportTypeId", "ReportPrinterId"}, idColumns, GetType(EntitySet.ReportTypeReportPrinterSet), tran),Entity.ReportTypeReportPrinter)
        End Function
        
        Public Overloads Function GetById(ByVal idColumns() As Integer) As Entity.ReportTypeReportPrinter
            Return Me.GetById(idColumns, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal idColumns() As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(idColumns, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal idColumns() As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeleteReportTypeReportPrinter", New String() {"ReportTypeId", "ReportPrinterId"}, idColumns, checkSum, transaction)
        End Function
        
        'GetForReportType - links to the ReportType table...
        Public Overloads Function GetForReportType(ByVal ReportTypeId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ReportTypeReportPrinterSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from ReportTypeReportPrinter where Repor"& _ 
"tTypeId=" + ReportTypeId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.ReportTypeReportPrinterSet), tran),EntitySet.ReportTypeReportPrinterSet)
        End Function
        
        'GetForReportType - links to the ReportType table...
        Public Overloads Function GetForReportType(ByVal ReportTypeId As Integer) As EntitySet.ReportTypeReportPrinterSet
            Return Me.GetForReportType(ReportTypeId, Nothing)
        End Function
        
        'GetForReportPrinter - links to the ReportPrinter table...
        Public Overloads Function GetForReportPrinter(ByVal ReportPrinterId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ReportTypeReportPrinterSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from ReportTypeReportPrinter where Repor"& _ 
"tPrinterId=" + ReportPrinterId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.ReportTypeReportPrinterSet), tran),EntitySet.ReportTypeReportPrinterSet)
        End Function
        
        'GetForReportPrinter - links to the ReportPrinter table...
        Public Overloads Function GetForReportPrinter(ByVal ReportPrinterId As Integer) As EntitySet.ReportTypeReportPrinterSet
            Return Me.GetForReportPrinter(ReportPrinterId, Nothing)
        End Function
        
        Public Overloads Sub Insert(ByVal reportTypeId As Integer, ByVal reportPrinterId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction)
            Sprocs.eosp_CreateReportTypeReportPrinter(reportTypeId, reportPrinterId, transaction)
        End Sub
        
        Public Overloads Sub Insert(ByVal reportTypeId As Integer, ByVal reportPrinterId As Integer)
            Me.Insert(reportTypeId, reportPrinterId, Nothing)
        End Sub
        
        Public Overloads Sub Insert(ByVal reportTypeReportPrinter As Entity.ReportTypeReportPrinter)
            Me.Insert(reportTypeReportPrinter(0), reportTypeReportPrinter(1))
        End Sub
        
        Public Overloads Sub Insert(ByVal reportTypeReportPrinter As Entity.ReportTypeReportPrinter, ByVal transaction As System.Data.SqlClient.SqlTransaction)
            Me.Insert(reportTypeReportPrinter(0), reportTypeReportPrinter(1), transaction)
        End Sub
        
        Public Overloads Function Update(ByVal reportTypeId As Integer, ByVal reportPrinterId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ReportTypeReportPrinter
            Return Sprocs.eosp_UpdateReportTypeReportPrinter(reportTypeId, reportPrinterId, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal reportTypeId As Integer, ByVal reportPrinterId As Integer) As Entity.ReportTypeReportPrinter
            Return Me.Update(reportTypeId, reportPrinterId, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal reportTypeId As Integer, ByVal reportPrinterId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ReportTypeReportPrinter
            Return Me.Update(reportTypeId, reportPrinterId, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal reportTypeId As Integer, ByVal reportPrinterId As Integer, ByVal checkSum As Integer) As Entity.ReportTypeReportPrinter
            Return Me.Update(reportTypeId, reportPrinterId, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal reportTypeReportPrinter As Entity.ReportTypeReportPrinter) As Entity.ReportTypeReportPrinter
            Return Me.Update(reportTypeReportPrinter(0), reportTypeReportPrinter(1))
        End Function
        
        Public Overloads Function Update(ByVal reportTypeReportPrinter As Entity.ReportTypeReportPrinter, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ReportTypeReportPrinter
            Return Me.Update(reportTypeReportPrinter(0), reportTypeReportPrinter(1), transaction)
        End Function
        
        Public Overloads Function Update(ByVal reportTypeReportPrinter As Entity.ReportTypeReportPrinter, ByVal checkSum As Integer) As Entity.ReportTypeReportPrinter
            Return Me.Update(reportTypeReportPrinter(0), reportTypeReportPrinter(1), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal reportTypeReportPrinter As Entity.ReportTypeReportPrinter, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ReportTypeReportPrinter
            Return Me.Update(reportTypeReportPrinter(0), reportTypeReportPrinter(1), checkSum, transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
        End Enum
    End Class
End Namespace
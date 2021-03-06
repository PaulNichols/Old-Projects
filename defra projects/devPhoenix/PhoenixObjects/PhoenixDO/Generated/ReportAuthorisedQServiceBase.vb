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
    
    'Service base implementation for table 'ReportAuthorisedQ'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class ReportAuthorisedQServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.ReportAuthorisedQSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.ReportAuthorisedQSet
            Return CType(MyBase.GetAll("eosp_SelectReportAuthorisedQ", GetType(EntitySet.ReportAuthorisedQSet), includeHyphen, includeInactive, orderBy),EntitySet.ReportAuthorisedQSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.ReportAuthorisedQSet
            Return Me.GetAll(includeHyphen, includeInactive, ReportAuthorisedQServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, ReportAuthorisedQServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal orderBy As OrderBy) As EntitySet.ReportAuthorisedQSet
            Return Me.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal reportAuthorisedQId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectReportAuthorisedQ", "ReportAuthorisedQId", reportAuthorisedQId, GetType(EntitySet.ReportAuthorisedQSet), tran),Entity.ReportAuthorisedQ)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal reportAuthorisedQId As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(reportAuthorisedQId, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal reportAuthorisedQId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.ReportAuthorisedQ
            Return CType(MyBase.GetById("eosp_SelectReportAuthorisedQ", "ReportAuthorisedQId", reportAuthorisedQId, GetType(EntitySet.ReportAuthorisedQSet), tran),Entity.ReportAuthorisedQ)
        End Function
        
        Public Overloads Function GetById(ByVal reportAuthorisedQId As Integer) As Entity.ReportAuthorisedQ
            Return Me.GetById(reportAuthorisedQId, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal reportAuthorisedQId As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(reportAuthorisedQId, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal reportAuthorisedQId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeleteReportAuthorisedQ", "ReportAuthorisedQId", reportAuthorisedQId, checkSum, transaction)
        End Function
        
        'GetForReport - links to the Report table...
        Public Overloads Function GetForReport(ByVal ReportId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ReportAuthorisedQSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from ReportAuthorisedQ where ReportId=" + ReportId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.ReportAuthorisedQSet), tran),EntitySet.ReportAuthorisedQSet)
        End Function
        
        'GetForReport - links to the Report table...
        Public Overloads Function GetForReport(ByVal ReportId As Integer) As EntitySet.ReportAuthorisedQSet
            Return Me.GetForReport(ReportId, Nothing)
        End Function
        
        'GetForReportPrinter - links to the ReportPrinter table...
        Public Overloads Function GetForReportPrinter(ByVal ReportPrinterId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ReportAuthorisedQSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from ReportAuthorisedQ where ReportPrint"& _ 
"erId=" + ReportPrinterId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.ReportAuthorisedQSet), tran),EntitySet.ReportAuthorisedQSet)
        End Function
        
        'GetForReportPrinter - links to the ReportPrinter table...
        Public Overloads Function GetForReportPrinter(ByVal ReportPrinterId As Integer) As EntitySet.ReportAuthorisedQSet
            Return Me.GetForReportPrinter(ReportPrinterId, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal reportId As Integer, ByVal reportPrinterId As Integer, ByVal printSequence As Integer, ByVal pausedBy As Object, ByVal pausedDate As Object, ByVal authorisedBy As String, ByVal authorisedDate As Date, ByVal printingDate As Object, ByVal printedDate As Object, ByVal deletedBy As Object, ByVal deletedDate As Object, ByVal lastStatusMessage As Object, ByVal stapleOff As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ReportAuthorisedQ
            Return Me.GetById(Sprocs.eosp_CreateReportAuthorisedQ(reportId, reportPrinterId, printSequence, pausedBy, pausedDate, authorisedBy, authorisedDate, printingDate, printedDate, deletedBy, deletedDate, lastStatusMessage, stapleOff, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal reportId As Integer, ByVal reportPrinterId As Integer, ByVal printSequence As Integer, ByVal pausedBy As Object, ByVal pausedDate As Object, ByVal authorisedBy As String, ByVal authorisedDate As Date, ByVal printingDate As Object, ByVal printedDate As Object, ByVal deletedBy As Object, ByVal deletedDate As Object, ByVal lastStatusMessage As Object, ByVal stapleOff As Object) As Entity.ReportAuthorisedQ
            Return Me.Insert(reportId, reportPrinterId, printSequence, pausedBy, pausedDate, authorisedBy, authorisedDate, printingDate, printedDate, deletedBy, deletedDate, lastStatusMessage, stapleOff, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal reportAuthorisedQ As Entity.ReportAuthorisedQ) As Entity.ReportAuthorisedQ
            Return Me.Insert(reportAuthorisedQ(1), reportAuthorisedQ(2), reportAuthorisedQ(3), reportAuthorisedQ(4), reportAuthorisedQ(5), reportAuthorisedQ(6), reportAuthorisedQ(7), reportAuthorisedQ(8), reportAuthorisedQ(9), reportAuthorisedQ(10), reportAuthorisedQ(11), reportAuthorisedQ(12), reportAuthorisedQ(13))
        End Function
        
        Public Overloads Function Insert(ByVal reportAuthorisedQ As Entity.ReportAuthorisedQ, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ReportAuthorisedQ
            Return Me.Insert(reportAuthorisedQ(1), reportAuthorisedQ(2), reportAuthorisedQ(3), reportAuthorisedQ(4), reportAuthorisedQ(5), reportAuthorisedQ(6), reportAuthorisedQ(7), reportAuthorisedQ(8), reportAuthorisedQ(9), reportAuthorisedQ(10), reportAuthorisedQ(11), reportAuthorisedQ(12), reportAuthorisedQ(13), transaction)
        End Function
        
        Public Overloads Function Update( _
                    ByVal id As Integer,  _
                    ByVal reportId As Integer,  _
                    ByVal reportPrinterId As Integer,  _
                    ByVal printSequence As Integer,  _
                    ByVal pausedBy As Object,  _
                    ByVal pausedDate As Object,  _
                    ByVal authorisedBy As String,  _
                    ByVal authorisedDate As Date,  _
                    ByVal printingDate As Object,  _
                    ByVal printedDate As Object,  _
                    ByVal deletedBy As Object,  _
                    ByVal deletedDate As Object,  _
                    ByVal lastStatusMessage As Object,  _
                    ByVal stapleOff As Object,  _
                    ByVal checkSum As Integer,  _
                    ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ReportAuthorisedQ
            Return Sprocs.eosp_UpdateReportAuthorisedQ(id, reportId, reportPrinterId, printSequence, pausedBy, pausedDate, authorisedBy, authorisedDate, printingDate, printedDate, deletedBy, deletedDate, lastStatusMessage, stapleOff, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal reportId As Integer, ByVal reportPrinterId As Integer, ByVal printSequence As Integer, ByVal pausedBy As Object, ByVal pausedDate As Object, ByVal authorisedBy As String, ByVal authorisedDate As Date, ByVal printingDate As Object, ByVal printedDate As Object, ByVal deletedBy As Object, ByVal deletedDate As Object, ByVal lastStatusMessage As Object, ByVal stapleOff As Object) As Entity.ReportAuthorisedQ
            Return Me.Update(id, reportId, reportPrinterId, printSequence, pausedBy, pausedDate, authorisedBy, authorisedDate, printingDate, printedDate, deletedBy, deletedDate, lastStatusMessage, stapleOff, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal reportId As Integer, ByVal reportPrinterId As Integer, ByVal printSequence As Integer, ByVal pausedBy As Object, ByVal pausedDate As Object, ByVal authorisedBy As String, ByVal authorisedDate As Date, ByVal printingDate As Object, ByVal printedDate As Object, ByVal deletedBy As Object, ByVal deletedDate As Object, ByVal lastStatusMessage As Object, ByVal stapleOff As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ReportAuthorisedQ
            Return Me.Update(id, reportId, reportPrinterId, printSequence, pausedBy, pausedDate, authorisedBy, authorisedDate, printingDate, printedDate, deletedBy, deletedDate, lastStatusMessage, stapleOff, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal reportId As Integer, ByVal reportPrinterId As Integer, ByVal printSequence As Integer, ByVal pausedBy As Object, ByVal pausedDate As Object, ByVal authorisedBy As String, ByVal authorisedDate As Date, ByVal printingDate As Object, ByVal printedDate As Object, ByVal deletedBy As Object, ByVal deletedDate As Object, ByVal lastStatusMessage As Object, ByVal stapleOff As Object, ByVal checkSum As Integer) As Entity.ReportAuthorisedQ
            Return Me.Update(id, reportId, reportPrinterId, printSequence, pausedBy, pausedDate, authorisedBy, authorisedDate, printingDate, printedDate, deletedBy, deletedDate, lastStatusMessage, stapleOff, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal reportAuthorisedQ As Entity.ReportAuthorisedQ) As Entity.ReportAuthorisedQ
            Return Me.Update(reportAuthorisedQ.id, reportAuthorisedQ(1), reportAuthorisedQ(2), reportAuthorisedQ(3), reportAuthorisedQ(4), reportAuthorisedQ(5), reportAuthorisedQ(6), reportAuthorisedQ(7), reportAuthorisedQ(8), reportAuthorisedQ(9), reportAuthorisedQ(10), reportAuthorisedQ(11), reportAuthorisedQ(12), reportAuthorisedQ(13))
        End Function
        
        Public Overloads Function Update(ByVal reportAuthorisedQ As Entity.ReportAuthorisedQ, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ReportAuthorisedQ
            Return Me.Update(reportAuthorisedQ.id, reportAuthorisedQ(1), reportAuthorisedQ(2), reportAuthorisedQ(3), reportAuthorisedQ(4), reportAuthorisedQ(5), reportAuthorisedQ(6), reportAuthorisedQ(7), reportAuthorisedQ(8), reportAuthorisedQ(9), reportAuthorisedQ(10), reportAuthorisedQ(11), reportAuthorisedQ(12), reportAuthorisedQ(13), transaction)
        End Function
        
        Public Overloads Function Update(ByVal reportAuthorisedQ As Entity.ReportAuthorisedQ, ByVal checkSum As Integer) As Entity.ReportAuthorisedQ
            Return Me.Update(reportAuthorisedQ.id, reportAuthorisedQ(1), reportAuthorisedQ(2), reportAuthorisedQ(3), reportAuthorisedQ(4), reportAuthorisedQ(5), reportAuthorisedQ(6), reportAuthorisedQ(7), reportAuthorisedQ(8), reportAuthorisedQ(9), reportAuthorisedQ(10), reportAuthorisedQ(11), reportAuthorisedQ(12), reportAuthorisedQ(13), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal reportAuthorisedQ As Entity.ReportAuthorisedQ, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.ReportAuthorisedQ
            Return Me.Update(reportAuthorisedQ.id, reportAuthorisedQ(1), reportAuthorisedQ(2), reportAuthorisedQ(3), reportAuthorisedQ(4), reportAuthorisedQ(5), reportAuthorisedQ(6), reportAuthorisedQ(7), reportAuthorisedQ(8), reportAuthorisedQ(9), reportAuthorisedQ(10), reportAuthorisedQ(11), reportAuthorisedQ(12), reportAuthorisedQ(13), checkSum, transaction)
        End Function
        
        Public Overloads Function GetByIndex_IX_ReportAuthorisedQ(ByVal reportPrinterId As Integer) As EntitySet.ReportAuthorisedQSet
            Return Sprocs.eosp_SelectReportAuthorisedQ(reportAuthorisedQId:=Nothing, Index_ReportPrinterId:=[reportPrinterId], Index_ReportId:=Nothing, Index_PrintSequence:=Nothing, transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_IX_ReportAuthorisedQ(ByVal reportPrinterId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.ReportAuthorisedQSet
            Return Sprocs.eosp_SelectReportAuthorisedQ(reportAuthorisedQId:=Nothing, Index_ReportPrinterId:=[reportPrinterId], Index_ReportId:=Nothing, Index_PrintSequence:=Nothing, sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Overloads Function GetByIndex_IX_ReportAuthorisedQ_1(ByVal reportId As Integer, ByVal reportPrinterId As Integer, ByVal printSequence As Integer) As EntitySet.ReportAuthorisedQSet
            Return Sprocs.eosp_SelectReportAuthorisedQ(reportAuthorisedQId:=Nothing, Index_ReportId:=[reportId], Index_ReportPrinterId:=[reportPrinterId], Index_PrintSequence:=[printSequence], transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_IX_ReportAuthorisedQ_1(ByVal reportId As Integer, ByVal reportPrinterId As Integer, ByVal printSequence As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.ReportAuthorisedQSet
            Return Sprocs.eosp_SelectReportAuthorisedQ(reportAuthorisedQId:=Nothing, Index_ReportId:=[reportId], Index_ReportPrinterId:=[reportPrinterId], Index_PrintSequence:=[printSequence], sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
            
            IX_ReportAuthorisedQ
            
            IX_ReportAuthorisedQ_1
            
            
        End Enum
    End Class
End Namespace

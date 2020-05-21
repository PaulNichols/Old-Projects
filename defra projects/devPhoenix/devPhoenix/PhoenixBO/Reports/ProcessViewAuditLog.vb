Imports System
Imports System.data
Imports System.Collections
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Namespace RPT

    Public Class ViewAuditLog
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim reportResults(0) As BOReportResults

            Dim viewAuditLogCriteria As ReportCriteria.ViewAuditLogCriteria = CType(reportCriteria, ReportCriteria.ViewAuditLogCriteria)

            Dim reportDatasets() As DataSet
            reportDatasets = GetViewAuditLogCriteriaDatasets(viewAuditLogCriteria)

            Dim viewAuditLog_RPT As ViewAuditLog_RPT

            Dim parameterValues As New Hashtable
            parameterValues.Add("FromDate", viewAuditLogCriteria.FromDate.ToString("dd/MM/yyyy"))
            parameterValues.Add("ToDate", viewAuditLogCriteria.ToDate.ToString("dd/MM/yyyy"))

            Dim idx As Int32 = -1
            Dim linkId As String = ""
            For Each dataset As DataSet In reportDatasets
                idx += 1
                viewAuditLog_RPT = New ViewAuditLog_RPT
                reportResults(idx) = DoReport(reportCriteria.Description, linkId, 0, dataset, viewAuditLogCriteria, saveReport, _
                viewAuditLog_RPT, reportPrintJobId, printSequence, parameterValues)
            Next

            Return reportResults

        End Function


        Public Function GetViewAuditLogCriteriaDatasets(ByVal viewAuditLogCriteria As ReportCriteria.ViewAuditLogCriteria) As ViewAuditLogData()

            Dim ViewAuditLogDataset As New ViewAuditLogData
            Dim boViewAuditLogRow As ViewAuditLogData.BOViewAuditLogRow

            ' View Audit Log - Row 0
            boViewAuditLogRow = ViewAuditLogDataset.BOViewAuditLog.NewBOViewAuditLogRow
            boViewAuditLogRow.RecordIdx = 0
            boViewAuditLogRow.EventDate = "17/12/03"
            boViewAuditLogRow.EventTime = "08:55:23"
            boViewAuditLogRow.UniqueUser = "x907163"
            boViewAuditLogRow.PartyIdAppId = "x907163"
            boViewAuditLogRow.DeviceAddress = "ITD6040WX.maff.gov.uk"
            boViewAuditLogRow.EventType = "User id added"
            boViewAuditLogRow.RelevantInformation = "x912218 added"
            ViewAuditLogDataset.BOViewAuditLog.AddBOViewAuditLogRow(boViewAuditLogRow)

            ' View Audit Log - Row 1
            boViewAuditLogRow = ViewAuditLogDataset.BOViewAuditLog.NewBOViewAuditLogRow
            boViewAuditLogRow.RecordIdx = 1
            boViewAuditLogRow.EventDate = "18/12/03"
            boViewAuditLogRow.EventTime = "14:15:16"
            boViewAuditLogRow.UniqueUser = "x912218"
            boViewAuditLogRow.PartyIdAppId = "x912218"
            boViewAuditLogRow.DeviceAddress = "ITD8542WX.maff.gov.uk"
            boViewAuditLogRow.EventType = "Role added to User"
            boViewAuditLogRow.RelevantInformation = "GWD Management"
            ViewAuditLogDataset.BOViewAuditLog.AddBOViewAuditLogRow(boViewAuditLogRow)

            ' View Audit Log - Row 2
            boViewAuditLogRow = ViewAuditLogDataset.BOViewAuditLog.NewBOViewAuditLogRow
            boViewAuditLogRow.RecordIdx = 2
            boViewAuditLogRow.EventDate = "19/12/03"
            boViewAuditLogRow.EventTime = "22:08:51"
            boViewAuditLogRow.UniqueUser = "x907163"
            boViewAuditLogRow.PartyIdAppId = "x907163"
            boViewAuditLogRow.DeviceAddress = "ITD6040WX.maff.gov.uk"
            boViewAuditLogRow.EventType = "User id deleted"
            boViewAuditLogRow.RelevantInformation = "x912218 deleted"
            ViewAuditLogDataset.BOViewAuditLog.AddBOViewAuditLogRow(boViewAuditLogRow)

            ViewAuditLogDataset.AcceptChanges()

            Dim viewAuditLogDatasets(0) As ViewAuditLogData
            viewAuditLogDatasets(0) = ViewAuditLogDataset

            Return viewAuditLogDatasets

        End Function

    End Class

End Namespace
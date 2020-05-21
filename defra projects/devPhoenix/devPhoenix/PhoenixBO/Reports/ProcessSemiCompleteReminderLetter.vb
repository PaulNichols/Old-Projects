Imports System
Imports System.data
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Namespace RPT

    Public Class SemiCompleteReminderLetter
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim SemiCompleteReminderLetterCriteria As ReportCriteria.SemiCompleteReminderLetterCriteria = CType(reportCriteria, ReportCriteria.SemiCompleteReminderLetterCriteria)
            Dim SemiCompleteReminderLetterData As New SemiCompleteReminderLetterData

            Dim reportData As New Application.Bird.Reports.ReportData
            Dim reportDataResults As ReportDataResults = reportData.GetSemiCompleteReminderLetterData(SemiCompleteReminderLetterCriteria.PermitInfoIds, SemiCompleteReminderLetterCriteria.SsoUserId, SemiCompleteReminderLetterData.GetXmlSchema)
            Dim ReportDataset As DataSet = reportDataResults.ReportData

            Dim SemiCompleteReminderLetter_RPT As New SemiCompleteReminderLetter_RPT
            Dim reportResults(0) As BOReportResults
            reportResults(0) = DoReport(reportCriteria.Description, reportDataResults.SearchReference, reportDataResults.DatabaseId, reportDataResults.ReportData, SemiCompleteReminderLetterCriteria, saveReport, _
                SemiCompleteReminderLetter_RPT, reportPrintJobId, printSequence)

            Return reportResults

        End Function

    End Class

End Namespace

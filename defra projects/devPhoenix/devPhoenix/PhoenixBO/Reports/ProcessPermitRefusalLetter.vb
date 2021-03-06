Imports System
Imports System.data
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Namespace RPT

    Public Class PermitRefusalLetter
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim permitRefusalLetterCriteria As ReportCriteria.PermitRefusalLetterCriteria = CType(reportCriteria, ReportCriteria.PermitRefusalLetterCriteria)
            Dim permitRefusalLetterData As New PermitRefusalLetterData

            Dim reportData As New Application.Bird.Reports.ReportData
            Dim reportDataResults As ReportDataResults = reportData.GetPermitRefusalLetterData(permitRefusalLetterCriteria.PermitInfoIds, permitRefusalLetterCriteria.SsoUserId, permitRefusalLetterData.GetXmlSchema)
            Dim ReportDataset As DataSet = reportDataResults.ReportData

            Dim PermitRefusalLetter_RPT As New PermitRefusalLetter_RPT
            Dim reportResults(0) As BOReportResults
            reportResults(0) = DoReport(reportCriteria.Description, reportDataResults.SearchReference, reportDataResults.DatabaseId, reportDataResults.ReportData, permitRefusalLetterCriteria, saveReport, _
                PermitRefusalLetter_RPT, reportPrintJobId, printSequence)

            Return reportResults

        End Function

    End Class

End Namespace

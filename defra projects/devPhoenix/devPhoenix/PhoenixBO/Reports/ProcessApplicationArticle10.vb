Imports System
Imports System.data
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Namespace RPT

    Public Class ApplicationArticle10
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim applicationArticle10ReportCriteria As ReportCriteria.ApplicationArticle10ReportCriteria = CType(reportCriteria, ReportCriteria.ApplicationArticle10ReportCriteria)
            Dim applicationArticle10Dataset As New ApplicationArticle10Data

            Dim citesArticle10Permit As New Application.CITES.Applications.BOCITESArticle10Permit
            Dim reportDataResults As ReportDataResults
            If applicationArticle10ReportCriteria.PermitInfoId > 0 Then
                reportDataResults = citesArticle10Permit.GetArticle10ApplicationReportData(applicationArticle10ReportCriteria.PermitInfoId, applicationArticle10Dataset.GetXmlSchema)
            Else
                reportDataResults = citesArticle10Permit.GetArticle10ApplicationReportTestData(applicationArticle10ReportCriteria.PermitInfoId, applicationArticle10Dataset.GetXmlSchema)
            End If
            Dim ReportDataset As DataSet = reportDataResults.ReportData

            Dim applicationArticle10_RPT As New applicationArticle10_RPT
            Dim reportResults(0) As BOReportResults
            reportResults(0) = DoReport(reportCriteria.Description, reportDataResults.SearchReference, reportDataResults.DatabaseId, reportDataResults.ReportData, applicationArticle10ReportCriteria, saveReport, _
                applicationArticle10_RPT, reportPrintJobId, printSequence)

            Return reportResults

        End Function


    End Class

End Namespace

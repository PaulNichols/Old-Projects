Imports System
Imports System.data
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Namespace RPT

    Public Class CertificateRefusalLetter
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim certificateRefusalLetterCriteria As ReportCriteria.CertificateRefusalLetterCriteria = CType(reportCriteria, ReportCriteria.CertificateRefusalLetterCriteria)
            Dim certificateRefusalLetterData As New CertificateRefusalLetterData

            Dim reportData As New Application.Bird.Reports.ReportData
            Dim reportDataResults As ReportDataResults = reportData.GetCertificateRefusalLetterData(certificateRefusalLetterCriteria.PermitInfoIds, certificateRefusalLetterCriteria.SsoUserId, certificateRefusalLetterData.GetXmlSchema)
            Dim ReportDataset As DataSet = reportDataResults.ReportData

            Dim certificateRefusalLetter_RPT As New certificateRefusalLetter_RPT
            Dim reportResults(0) As BOReportResults
            reportResults(0) = DoReport(reportCriteria.Description, reportDataResults.SearchReference, reportDataResults.DatabaseId, reportDataResults.ReportData, certificateRefusalLetterCriteria, saveReport, _
                certificateRefusalLetter_RPT, reportPrintJobId, printSequence)

            Return reportResults

        End Function

    End Class

End Namespace

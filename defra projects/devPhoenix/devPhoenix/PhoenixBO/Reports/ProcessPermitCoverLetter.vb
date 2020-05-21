Imports System
Imports System.data
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Namespace RPT

    Public Class PermitCoverLetter
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim certificatePermitCoverLetterCriteria As ReportCriteria.CertificatePermitCoverLetterCriteria = CType(reportCriteria, ReportCriteria.CertificatePermitCoverLetterCriteria)
            Dim certificatePermitCoverLetterData As New CertificatePermitCoverLetterData

            Dim reportData As New Application.Bird.Reports.ReportData
            Dim reportDataResults As ReportDataResults = reportData.GetCertificatePermitCoverLetterData(certificatePermitCoverLetterCriteria.ApplicationId, certificatePermitCoverLetterCriteria.SsoUserId, certificatePermitCoverLetterData.GetXmlSchema)
            Dim ReportDataset As DataSet = reportDataResults.ReportData

            Dim certificatePermitCoverLetter_RPT As New certificatePermitCoverLetter_RPT
            Dim reportResults(0) As BOReportResults
            reportResults(0) = DoReport(reportCriteria.Description, reportDataResults.SearchReference, certificatePermitCoverLetterCriteria.ApplicationId, reportDataResults.ReportData, certificatePermitCoverLetterCriteria, saveReport, _
                certificatePermitCoverLetter_RPT, reportPrintJobId, printSequence)

            Return reportResults

        End Function

    End Class

End Namespace

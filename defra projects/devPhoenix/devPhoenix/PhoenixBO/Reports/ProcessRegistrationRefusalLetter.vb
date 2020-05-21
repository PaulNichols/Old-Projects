Imports System
Imports System.data
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Namespace RPT

    Public Class RegistrationRefusalLetter
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim RegistrationRefusalLetterCriteria As reportCriteria.RegistrationRefusalLetterCriteria = CType(reportCriteria, reportCriteria.RegistrationRefusalLetterCriteria)
            Dim RegistrationRefusalLetterData As New RegistrationRefusalLetterData

            Dim reportData As New Application.Bird.Reports.ReportData
            Dim reportDataResults As ReportDataResults = reportData.GetRegistrationRefusalLetterData(RegistrationRefusalLetterCriteria.ApplicationId, RegistrationRefusalLetterCriteria.SsoUserId, RegistrationRefusalLetterData.GetXmlSchema)
            Dim ReportDataset As DataSet = reportDataResults.ReportData

            Dim RegistrationRefusalLetter_RPT As New RegistrationRefusalLetter_RPT
            Dim reportResults(0) As BOReportResults
            reportResults(0) = DoReport(reportCriteria.Description, reportDataResults.SearchReference, reportDataResults.DatabaseId, reportDataResults.ReportData, RegistrationRefusalLetterCriteria, saveReport, _
                RegistrationRefusalLetter_RPT, reportPrintJobId, printSequence)

            Return reportResults

        End Function

    End Class

End Namespace

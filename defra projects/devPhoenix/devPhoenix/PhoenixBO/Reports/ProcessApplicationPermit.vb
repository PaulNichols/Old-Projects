Imports System
Imports System.data
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Namespace RPT

    Public Class ApplicationPermit
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim applicationPermitCriteria As reportCriteria.ApplicationPermitCriteria = CType(reportCriteria, reportCriteria.ApplicationPermitCriteria)
            Dim applicationPermitDataset As New BOCITESApplicationPermitData

            Dim citesImportExportPermit As New Application.CITES.Applications.BOCITESImportExportPermit
            Dim reportDataResults As reportDataResults
            If applicationPermitCriteria.ApplicationId > 0 Then
                reportDataResults = citesImportExportPermit.GetPermitApplicationReportData(applicationPermitCriteria.ApplicationId, applicationPermitDataset.GetXmlSchema)
            Else
                reportDataResults = citesImportExportPermit.GetPermitApplicationReportTestData(applicationPermitCriteria.ApplicationId, applicationPermitDataset.GetXmlSchema)
            End If
            Dim ReportDataset As DataSet = reportDataResults.ReportData

            Dim applicationPermit_RPT As New CITESApplicationPermit_RPT
            Dim reportResults(0) As BOReportResults
            reportResults(0) = DoReport(reportCriteria.Description, reportDataResults.SearchReference, applicationPermitCriteria.ApplicationId, reportDataResults.ReportData, applicationPermitCriteria, saveReport, _
                applicationPermit_RPT, reportPrintJobId, printSequence)

            Return reportResults

        End Function
    End Class

End Namespace

Imports System
Imports System.data
'Imports CrystalDecisions.Shared
'Imports CrystalDecisions.CrystalReports.Engine
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Namespace RPT

    Public Class ProcessRemittanceAdvice
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim remittanceAdviceCriteria As reportCriteria.RemittanceAdviceCriteria = CType(reportCriteria, reportCriteria.RemittanceAdviceCriteria)
            Dim remittanceAdviceDataset As New RemittanceAdviceData

            Dim payment As New Payments.BOPayment
            Dim reportDataResults As reportDataResults = payment.GetRemittanceAdviceData(remittanceAdviceCriteria.PaymentId, remittanceAdviceDataset.GetXmlSchema)
            Dim ReportDataset As DataSet = reportDataResults.ReportData

            Dim remittanceAdvice_RPT As New remittanceAdvice_RPT
            Dim reportResults(0) As BOReportResults
            reportResults(0) = DoReport(reportCriteria.Description, reportDataResults.SearchReference, remittanceAdviceCriteria.PaymentId, reportDataResults.ReportData, remittanceAdviceCriteria, saveReport, _
                remittanceAdvice_RPT, reportPrintJobId, printSequence)

            Return reportResults

        End Function
    End Class

End Namespace

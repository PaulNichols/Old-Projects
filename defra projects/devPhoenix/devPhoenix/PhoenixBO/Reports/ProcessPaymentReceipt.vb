Imports System
Imports System.data
'Imports CrystalDecisions.Shared
'Imports CrystalDecisions.CrystalReports.Engine
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do
Namespace RPT

    Public Class ProcessPaymentReceipt
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub


        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim paymentReceiptCriteria As reportCriteria.PaymentReceiptCriteria = CType(reportCriteria, reportCriteria.PaymentReceiptCriteria)
            Dim paymentReceiptDataset As New PaymentReceiptData

            Dim payment As New Payments.BOPayment
            Dim reportDataResults As reportDataResults = payment.GetPaymentReceiptReportData(paymentReceiptCriteria.PaymentId, paymentReceiptDataset.GetXmlSchema)
            Dim ReportDataset As DataSet = reportDataResults.ReportData

            Dim paymentReceipt_RPT As New paymentReceipt_RPT
            Dim reportResults(0) As BOReportResults
            reportResults(0) = DoReport(reportCriteria.Description, reportDataResults.SearchReference, paymentReceiptCriteria.PaymentId, reportDataResults.ReportData, paymentReceiptCriteria, saveReport, _
                paymentReceipt_RPT, reportPrintJobId, printSequence)

            Return reportResults

        End Function


    End Class

End Namespace

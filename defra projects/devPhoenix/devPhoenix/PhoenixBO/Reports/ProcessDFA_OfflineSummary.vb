Imports System
Imports System.data
Imports System.Collections
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Namespace RPT

    Public Class DFA_OfflineSummary
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim reportResults(0) As BOReportResults
            Dim rptCriteria As ReportCriteria.DFA_OfflineSummaryCriteria = CType(reportCriteria, ReportCriteria.DFA_OfflineSummaryCriteria)

            Dim reportDataset As DataSet

            reportDataset = GetDataset(rptCriteria)

            Dim crystalRPT As New DFA_Offline_Summary_RPT

            Dim SearchReference As String = ""

            Dim parameterValues As New Hashtable

            parameterValues.Add("toDate", rptCriteria.ToDate.ToShortDateString & " " & rptCriteria.ToDate.ToLongTimeString)
            parameterValues.Add("costCode", rptCriteria.CostCode)

            reportResults(0) = DoReport(rptCriteria.Description, SearchReference, 0, reportDataset, rptCriteria, saveReport, _
            crystalRPT, reportPrintJobId, printSequence, parameterValues)

            Return reportResults

        End Function


        Public Function GetDataset(ByVal rptCriteria As ReportCriteria.DFA_OfflineSummaryCriteria) As DFA_Offline_SummaryData

            ' Get Offline Summary
            Dim payments As ReportDFA.BOReportOfflineSummary
            Dim paymentSummary As ReportDFA.BOReportOfflineSummary = payments.getReportOfflineSummary(rptCriteria.FromDate, rptCriteria.ToDate)

            Dim aDataset As New DFA_Offline_SummaryData
            Dim aRow As DFA_Offline_SummaryData.BODFA_OffLine_SummaryRow

            ' DFA_OffLine_Summary - Row 
            aRow = aDataset.BODFA_OffLine_Summary.NewBODFA_OffLine_SummaryRow
            aRow.RowId = 1
            aRow.ChequeNum = paymentSummary.ChequeNum.ToString
            aRow.ChequeAmt = paymentSummary.ChequeAmt.ToString("c")
            aRow.CashNum = paymentSummary.CashNum.ToString
            aRow.CashAmt = paymentSummary.CashAmt.ToString("c")
            aRow.PostalOrderNum = paymentSummary.PostalOrderNum.ToString
            aRow.PostalOrderAmt = paymentSummary.PostalOrderAmt.ToString("c")
            aRow.TotalPaymentsNum = paymentSummary.TotalPaymentsNum.ToString
            aRow.TotalPaymentsAmt = paymentSummary.TotalPaymentsAmt.ToString("c")
            aRow.TotalRefundsNum = paymentSummary.TotalRefundsNum.ToString
            aRow.TotalRefundsAmt = paymentSummary.TotalRefundsAmt.ToString("c")
            aRow.TotalAdjustmentsNum = paymentSummary.TotalAdjustmentsNum.ToString
            aRow.TotalAdjustmentsAmt = paymentSummary.TotalAdjustmentsAmt.ToString("c")
            aDataset.BODFA_OffLine_Summary.AddBODFA_OffLine_SummaryRow(aRow)

            Return aDataset

        End Function

    End Class

End Namespace

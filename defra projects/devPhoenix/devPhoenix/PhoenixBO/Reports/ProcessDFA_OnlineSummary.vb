Imports System
Imports System.data
Imports System.Collections
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Namespace RPT

    Public Class DFA_OnlineSummary
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim reportResults(0) As BOReportResults
            Dim rptCriteria As ReportCriteria.DFA_OnlineSummaryCriteria = CType(reportCriteria, ReportCriteria.DFA_OnlineSummaryCriteria)

            Dim reportDataset As DataSet

            reportDataset = GetDataset(rptCriteria)

            Dim crystalRPT As New DFA_Online_Summary_RPT

            Dim SearchReference As String = ""

            Dim parameterValues As New Hashtable

            parameterValues.Add("toDate", rptCriteria.ToDate.ToShortDateString & " " & rptCriteria.ToDate.ToLongTimeString)
            parameterValues.Add("costCode", rptCriteria.CostCode)

            reportResults(0) = DoReport(reportCriteria.Description, SearchReference, 0, reportDataset, rptCriteria, saveReport, _
            crystalRPT, reportPrintJobId, printSequence, parameterValues)

            Return reportResults

        End Function


        Public Function GetDataset(ByVal rptCriteria As ReportCriteria.DFA_OnlineSummaryCriteria) As DFA_Online_SummaryData

            ' Get Online Summary
            Dim payments As ReportDFA.BOReportOnlineSummary
            Dim paymentSummary As ReportDFA.BOReportOnlineSummary = payments.getReportOnlineSummary(rptCriteria.FromDate, rptCriteria.ToDate)

            Dim aDataset As New DFA_Online_SummaryData
            Dim aRow As DFA_Online_SummaryData.BODFA_Online_SummaryRow

            ' DFA_Online_Summary - Row 
            aRow = aDataset.BODFA_Online_Summary.NewBODFA_Online_SummaryRow
            aRow.RowId = 1
            aRow.TotalPaymentsNum = paymentSummary.TotalPaymentsNum.ToString
            aRow.TotalPaymentsAmt = paymentSummary.TotalPaymentsAmt.ToString("c")
            aRow.TotalRefundsNum = paymentSummary.TotalRefundsNum.ToString
            aRow.TotalRefundsAmt = paymentSummary.TotalRefundsAmt.ToString("c")
            aDataset.BODFA_Online_Summary.AddBODFA_Online_SummaryRow(aRow)

            Return aDataset

        End Function

    End Class

End Namespace

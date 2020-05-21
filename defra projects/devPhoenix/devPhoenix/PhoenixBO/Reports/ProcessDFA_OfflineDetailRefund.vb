Imports System
Imports System.data
Imports System.Collections
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Namespace RPT

    Public Class DFA_OfflineDetailRefund
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim reportResults(0) As BOReportResults

            Dim rptCriteria As ReportCriteria.DFA_OfflineDetailRefundCriteria = CType(reportCriteria, ReportCriteria.DFA_OfflineDetailRefundCriteria)

            Dim reportDataset As DataSet

            reportDataset = GetDataset(rptCriteria)

            Dim crystalRPT As New DFA_Offline_Detail_Refund_RPT

            Dim SearchReference As String = ""

            Dim parameterValues As New Hashtable

            parameterValues.Add("toDate", rptCriteria.ToDate.ToShortDateString & " " & rptCriteria.ToDate.ToLongTimeString)
            parameterValues.Add("costCode", rptCriteria.CostCode)

            reportResults(0) = DoReport(rptCriteria.Description, SearchReference, 0, reportDataset, rptCriteria, saveReport, _
            crystalRPT, reportPrintJobId, printSequence, parameterValues)

            Return reportResults

        End Function


        Public Function GetDataset(ByVal rptCriteria As ReportCriteria.DFA_OfflineDetailRefundCriteria) As DFA_OffLine_Detail_RefundData

            Dim aDataset As New DFA_OffLine_Detail_RefundData
            Dim aRow As DFA_OffLine_Detail_RefundData.BODFA_OffLine_Detail_RefundRow
            Dim subTotal As DFA_OffLine_Detail_RefundData.BODFA_OffLine_Detail_RefundRow

            ' DFA_OffLine_Detail_Refund - Row 
            aRow = aDataset.BODFA_OffLine_Detail_Refund.NewBODFA_OffLine_Detail_RefundRow
            aRow.RowId = 1
            aRow.BoldRow = True
            aRow.ColumnData = "Detail - Refunds"
            aRow.PaymentDateTime = ""
            aRow.Reference = ""
            aRow.Amount = ""
            aRow.PartyID = ""
            aRow.PartyName = ""
            aRow.Details = ""
            aRow.Notes = ""
            aDataset.BODFA_OffLine_Detail_Refund.AddBODFA_OffLine_Detail_RefundRow(aRow)

            ' DFA_OffLine_Detail_Refund - Row 
            aRow = aDataset.BODFA_OffLine_Detail_Refund.NewBODFA_OffLine_Detail_RefundRow
            aRow.RowId = 1
            aRow.BoldRow = True
            aRow.ColumnData = "Cheques"
            aRow.PaymentDateTime = ""
            aRow.Reference = ""
            aRow.Amount = ""
            aRow.PartyID = ""
            aRow.PartyName = ""
            aRow.Details = ""
            aRow.Notes = ""
            aDataset.BODFA_OffLine_Detail_Refund.AddBODFA_OffLine_Detail_RefundRow(aRow)

            ' DFA_OffLine_Detail_Refund - Row 
            aRow = aDataset.BODFA_OffLine_Detail_Refund.NewBODFA_OffLine_Detail_RefundRow
            aRow.RowId = 1
            aRow.BoldRow = True
            aRow.ColumnData = "Refund Date & Time"
            aRow.PaymentDateTime = "Payment Date & Time"
            aRow.Reference = "Payment Reference"
            aRow.Amount = "Amount Refunded"
            aRow.PartyID = "Party ID"
            aRow.PartyName = "Party Name"
            aRow.Details = "Refund Details"
            aRow.Notes = "Refund Notes"
            aDataset.BODFA_OffLine_Detail_Refund.AddBODFA_OffLine_Detail_RefundRow(aRow)

            ' Get Offline Detail Refund
            Dim payments As ReportDFA.BOReportOfflineDetailRefund
            Dim detailRefunds(-1) As ReportDFA.BOReportOfflineDetailRefund
            detailRefunds = payments.getReportOfflineDetailRefund(rptCriteria.FromDate, rptCriteria.ToDate)

            ' Loop through Refunds payments and create report rows
            Dim rowId As Int32 = 2
            Dim transDate As String
            Dim transTime As String
            Dim prevTransDate As String = ""
            Dim subTotReference As Int32 = 0
            Dim subTotAmount As Decimal = 0
            Dim grandTotReference As Int32 = 0
            Dim grandTotAmount As Decimal = 0

            For Each detailRefund As ReportDFA.BOReportOfflineDetailRefund In detailRefunds

                transDate = CType(detailRefund.TransactionDateTime, Date).ToShortDateString
                transTime = CType(detailRefund.TransactionDateTime, Date).ToLongTimeString
                If transDate <> prevTransDate Then ' Do we have a change in transaction date
                    If prevTransDate.Length <> 0 Then
                        ' We have a change in transaction date, so create a sub total line
                        'rowId += 1
                        'subTotal = aDataset.BODFA_OffLine_Detail_Refund.NewBODFA_OffLine_Detail_RefundRow
                        'subTotal.RowId = rowId
                        'subTotal.BoldRow = True
                        'subTotal.ColumnData = "Refund Sub Total"
                        'subTotal.PaymentDateTime = " "
                        'subTotal.Reference = subTotReference.ToString
                        'subTotal.Amount = subTotAmount.ToString("c")
                        'subTotal.PartyID = " "
                        'subTotal.PartyName = " "
                        'subTotal.Details = " "
                        'subTotal.Notes = " "
                        'aDataset.BODFA_OffLine_Detail_Refund.AddBODFA_OffLine_Detail_RefundRow(subTotal)

                        'subTotReference = 0
                        'subTotAmount = 0
                    End If
                    prevTransDate = transDate
                End If

                ' We have a transaction date, so populate additional report row details.
                ' Create report default Detail Refund row - Only used for rows with NO transaction date
                rowId += 1
                aRow = aDataset.BODFA_OffLine_Detail_Refund.NewBODFA_OffLine_Detail_RefundRow
                aRow.RowId = rowId
                aRow.BoldRow = False
                aRow.ColumnData = transDate & " " & transTime
                aRow.PaymentDateTime = CType(detailRefund.PaymentDateTime, Date).ToShortDateString & CType(detailRefund.PaymentDateTime, Date).ToLongTimeString
                aRow.Reference = detailRefund.Reference
                subTotReference += 1
                grandTotReference += 1
                aRow.Amount = detailRefund.TotalAmount.ToString("c")
                subTotAmount += detailRefund.TotalAmount
                grandTotAmount += detailRefund.TotalAmount
                aRow.PartyID = detailRefund.PartyId
                aRow.PartyName = detailRefund.PartyName
                aRow.Details = detailRefund.RefundDetails
                aRow.Notes = detailRefund.Notes

                ' Add the row to the Dataset Table
                aDataset.BODFA_OffLine_Detail_Refund.AddBODFA_OffLine_Detail_RefundRow(aRow)

            Next ' Get the next REfund

            ' Output Final Sub Total Report Row
            'rowId += 1
            'subTotal = aDataset.BODFA_OffLine_Detail_Refund.NewBODFA_OffLine_Detail_RefundRow
            'subTotal.RowId = rowId
            'subTotal.BoldRow = True
            'subTotal.ColumnData = "Refund Sub Total"
            'subTotal.PaymentDateTime = " "
            'subTotal.Reference = subTotReference.ToString
            'subTotal.Amount = subTotAmount.ToString("c")
            'subTotal.PartyID = " "
            'subTotal.PartyName = " "
            'subTotal.Details = " "
            'subTotal.Notes = " "
            'aDataset.BODFA_OffLine_Detail_Refund.AddBODFA_OffLine_Detail_RefundRow(subTotal)

            ' Output Rport Grand Totals
            rowId += 1
            subTotal = aDataset.BODFA_OffLine_Detail_Refund.NewBODFA_OffLine_Detail_RefundRow
            subTotal.RowId = rowId
            subTotal.BoldRow = True
            subTotal.ColumnData = "Refund Total"
            subTotal.PaymentDateTime = " "
            subTotal.Reference = grandTotReference.ToString
            subTotal.Amount = grandTotAmount.ToString("c")
            subTotal.PartyID = " "
            subTotal.PartyName = " "
            subTotal.Details = " "
            subTotal.Notes = " "
            aDataset.BODFA_OffLine_Detail_Refund.AddBODFA_OffLine_Detail_RefundRow(subTotal)


            Return aDataset

        End Function
    End Class

End Namespace
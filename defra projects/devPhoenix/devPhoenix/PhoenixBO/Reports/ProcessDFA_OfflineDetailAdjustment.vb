Imports System
Imports System.data
Imports System.Collections
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Namespace RPT

    Public Class DFA_OfflineDetailAdjustment
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim reportResults(0) As BOReportResults

            Dim rptCriteria As ReportCriteria.DFA_OfflineDetailAdjustmentCriteria = CType(reportCriteria, ReportCriteria.DFA_OfflineDetailAdjustmentCriteria)

            Dim reportDataset As DataSet

            reportDataset = GetDataset(rptCriteria)

            Dim crystalRPT As New DFA_Offline_Detail_Adjustment_RPT

            Dim SearchReference As String = ""

            Dim parameterValues As New Hashtable

            parameterValues.Add("toDate", rptCriteria.ToDate.ToShortDateString & " " & rptCriteria.ToDate.ToLongTimeString)
            parameterValues.Add("costCode", rptCriteria.CostCode)

            reportResults(0) = DoReport(rptCriteria.Description, SearchReference, 0, reportDataset, rptCriteria, saveReport, _
            crystalRPT, reportPrintJobId, printSequence, parameterValues)

            Return reportResults

        End Function


        Public Function GetDataset(ByVal rptCriteria As ReportCriteria.DFA_OfflineDetailAdjustmentCriteria) As DFA_OffLine_Detail_AdjustmentData

            Dim aDataset As New DFA_OffLine_Detail_AdjustmentData
            Dim aRow As DFA_OffLine_Detail_AdjustmentData.BODFA_OffLine_Detail_AdjustmentRow
            Dim subTotal As DFA_OffLine_Detail_AdjustmentData.BODFA_OffLine_Detail_AdjustmentRow

            ' DFA_OffLine_Detail_Adjustment - Row 
            aRow = aDataset.BODFA_OffLine_Detail_Adjustment.NewBODFA_OffLine_Detail_AdjustmentRow
            aRow.RowId = 1
            aRow.BoldRow = True
            aRow.ColumnData = "Detail - Adjustments"
            aRow.PaymentDateTime = ""
            aRow.Reference = ""
            aRow.Amount = ""
            aRow.PartyID = ""
            aRow.PartyName = ""
            aRow.Notes = ""
            aDataset.BODFA_OffLine_Detail_Adjustment.AddBODFA_OffLine_Detail_AdjustmentRow(aRow)


            ' DFA_OffLine_Detail_Adjustment - Row 
            aRow = aDataset.BODFA_OffLine_Detail_Adjustment.NewBODFA_OffLine_Detail_AdjustmentRow
            aRow.RowId = 1
            aRow.BoldRow = True
            aRow.ColumnData = "Adjustment Date & Time"
            aRow.PaymentDateTime = "Payment Date & Time"
            aRow.Reference = "Payment Reference"
            aRow.Amount = "Amount Adjustmented"
            aRow.PartyID = "Party ID"
            aRow.PartyName = "Party Name"
            aRow.Notes = "Adjustment Notes"
            aDataset.BODFA_OffLine_Detail_Adjustment.AddBODFA_OffLine_Detail_AdjustmentRow(aRow)

            ' Get Offline Detail Adjustment
            Dim payments As ReportDFA.BOReportOfflineDetailAdjustment
            Dim detailAdjustments(-1) As ReportDFA.BOReportOfflineDetailAdjustment
            detailAdjustments = payments.getReportOfflineDetailAdjustment(rptCriteria.FromDate, rptCriteria.ToDate)

            ' Loop through Adjustments payments and create report rows
            Dim rowId As Int32 = 2
            Dim transDate As String
            Dim transTime As String
            Dim prevTransDate As String = ""
            Dim subTotReference As Int32 = 0
            Dim subTotAmount As Decimal = 0
            Dim grandTotReference As Int32 = 0
            Dim grandTotAmount As Decimal = 0

            For Each detailAdjustment As ReportDFA.BOReportOfflineDetailAdjustment In detailAdjustments

                transDate = CType(detailAdjustment.TransactionDateTime, Date).ToShortDateString
                transTime = CType(detailAdjustment.TransactionDateTime, Date).ToLongTimeString
                If transDate <> prevTransDate Then ' Do we have a change in transaction date
                    If prevTransDate.Length <> 0 Then
                        ' We have a change in transaction date, so create a sub total line
                        'rowId += 1
                        'subTotal = aDataset.BODFA_OffLine_Detail_Adjustment.NewBODFA_OffLine_Detail_AdjustmentRow
                        'subTotal.RowId = rowId
                        'subTotal.BoldRow = True
                        'subTotal.ColumnData = "Adjustment Sub Total"
                        'subTotal.PaymentDateTime = " "
                        'subTotal.Reference = subTotReference.ToString
                        'subTotal.Amount = subTotAmount.ToString("c")
                        'subTotal.PartyID = " "
                        'subTotal.PartyName = " "
                        'subTotal.Notes = " "
                        'aDataset.BODFA_OffLine_Detail_Adjustment.AddBODFA_OffLine_Detail_AdjustmentRow(subTotal)

                        'subTotReference = 0
                        'subTotAmount = 0
                    End If
                    prevTransDate = transDate
                End If

                ' We have a transaction date, so populate additional report row details.
                ' Create report default Detail Adjustment row - Only used for rows with NO transaction date
                rowId += 1
                aRow = aDataset.BODFA_OffLine_Detail_Adjustment.NewBODFA_OffLine_Detail_AdjustmentRow
                aRow.RowId = rowId
                aRow.BoldRow = False
                aRow.ColumnData = transDate & " " & transTime
                aRow.PaymentDateTime = CType(detailAdjustment.TransactionDateTime, Date).ToShortDateString & " " & CType(detailAdjustment.TransactionDateTime, Date).ToLongTimeString
                aRow.Reference = detailAdjustment.Reference
                subTotReference += 1
                grandTotReference += 1
                aRow.Amount = detailAdjustment.TotalAmount.ToString("c")
                If detailAdjustment.TotalAmount < 0 Then
                    aRow.Amount = "(" & detailAdjustment.TotalAmount.ToString("c") & ")"
                    aRow.Amount = aRow.Amount.Replace("-", "")
                End If
                subTotAmount += detailAdjustment.TotalAmount
                grandTotAmount += detailAdjustment.TotalAmount
                aRow.PartyID = detailAdjustment.PartyId
                aRow.PartyName = detailAdjustment.PartyName
                aRow.Notes = detailAdjustment.Notes

                ' Add the row to the Dataset Table
                aDataset.BODFA_OffLine_Detail_Adjustment.AddBODFA_OffLine_Detail_AdjustmentRow(aRow)

            Next ' Get the next Adjustment

            ' Output Final Sub Total Report Row
            'rowId += 1
            'subTotal = aDataset.BODFA_OffLine_Detail_Adjustment.NewBODFA_OffLine_Detail_AdjustmentRow
            'subTotal.RowId = rowId
            'subTotal.BoldRow = True
            'subTotal.ColumnData = "Adjustment Sub Total"
            'subTotal.PaymentDateTime = " "
            'subTotal.Reference = subTotReference.ToString
            'subTotal.Amount = subTotAmount.ToString("c")
            'subTotal.PartyID = " "
            'subTotal.PartyName = " "
            'subTotal.Notes = " "
            'aDataset.BODFA_OffLine_Detail_Adjustment.AddBODFA_OffLine_Detail_AdjustmentRow(subTotal)

            ' Output Rport Grand Totals
            rowId += 1
            subTotal = aDataset.BODFA_OffLine_Detail_Adjustment.NewBODFA_OffLine_Detail_AdjustmentRow
            subTotal.RowId = rowId
            subTotal.BoldRow = True
            subTotal.ColumnData = "Adjustment Total"
            subTotal.PaymentDateTime = " "
            subTotal.Reference = grandTotReference.ToString
            subTotal.Amount = grandTotAmount.ToString("c")
            subTotal.PartyID = " "
            subTotal.PartyName = " "
            subTotal.Notes = " "
            aDataset.BODFA_OffLine_Detail_Adjustment.AddBODFA_OffLine_Detail_AdjustmentRow(subTotal)

            Return aDataset

        End Function
    End Class

End Namespace
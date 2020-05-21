Imports System
Imports System.data
Imports System.Collections
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Namespace RPT

    Public Class DFA_Waiver
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim reportResults(0) As BOReportResults
            Dim rptCriteria As ReportCriteria.DFA_WaiverCriteria = CType(reportCriteria, ReportCriteria.DFA_WaiverCriteria)

            Dim reportDataset As DataSet

            reportDataset = GetDataset(rptCriteria)

            Dim crystalRPT As New DFA_Waiver_RPT

            Dim SearchReference As String = ""

            Dim parameterValues As New Hashtable

            parameterValues.Add("toDate", rptCriteria.ToDate.ToShortDateString & " " & rptCriteria.ToDate.ToLongTimeString)
            parameterValues.Add("costCode", rptCriteria.CostCode)

            reportResults(0) = DoReport(reportCriteria.Description, SearchReference, 0, reportDataset, rptCriteria, saveReport, _
            crystalRPT, reportPrintJobId, printSequence, parameterValues)

            Return reportResults

        End Function


        Public Function GetDataset(ByVal rptCriteria As ReportCriteria.DFA_WaiverCriteria) As DFA_WaiverData

            Dim aDataset As New DFA_WaiverData
            Dim aRow As DFA_WaiverData.BODFA_WaiverRow
            Dim subTotal As DFA_WaiverData.BODFA_WaiverRow

            ' DFA_Waiver - Row 
            aRow = aDataset.BODFA_Waiver.NewBODFA_WaiverRow
            aRow.RowId = 1
            aRow.BoldRow = True
            aRow.ColumnData = "Waiver Date & Time"
            aRow.Reference = "Payment Reference"
            aRow.ApplicationNumber = "Application Number"
            aRow.OriginalAmount = "Original Amount"
            aRow.AmountWaived = "Amount Waived"
            aRow.PartyID = "Party ID"
            aRow.PartyName = "Party Name"
            aRow.UserName = "User Name"
            aDataset.BODFA_Waiver.AddBODFA_WaiverRow(aRow)

            ' Get Detail Waivers
            Dim payments As ReportDFA.BOReportWaiver
            Dim detailWaivers(-1) As ReportDFA.BOReportWaiver
            detailWaivers = payments.getReportWaiver(rptCriteria.FromDate, rptCriteria.ToDate)

            ' Loop through Refunds payments and create report rows
            Dim rowId As Int32 = 2
            Dim transDate As String
            Dim transTime As String
            Dim prevTransDate As String = ""
            Dim subTotReference As Int32 = 0
            Dim subTotAmount As Decimal = 0
            Dim grandTotReference As Int32 = 0
            Dim grandTotAmount As Decimal = 0

            For Each detailWaiver As ReportDFA.BOReportWaiver In detailWaivers

                transDate = CType(detailWaiver.TransactionDateTime, Date).ToShortDateString
                transTime = CType(detailWaiver.TransactionDateTime, Date).ToLongTimeString
                If transDate <> prevTransDate Then ' Do we have a change in transaction date
                    If prevTransDate.Length <> 0 Then
                        ' We have a change in transaction date, so create a sub total line
                        'rowId += 1
                        'subTotal = aDataset.BODFA_Waiver.NewBODFA_WaiverRow
                        'subTotal.RowId = rowId
                        'subTotal.BoldRow = True
                        'subTotal.ColumnData = "Waiver Sub Total"
                        'subTotal.Reference = subTotReference.ToString
                        'subTotal.OriginalAmount = " "
                        'subTotal.AmountWaived = subTotAmount.ToString("c")
                        'subTotal.PartyID = " "
                        'subTotal.PartyName = " "
                        'subTotal.UserName = " "
                        'aDataset.BODFA_Waiver.AddBODFA_WaiverRow(subTotal)

                        'subTotReference = 0
                        'subTotAmount = 0
                    End If
                    prevTransDate = transDate
                End If

                ' We have a transaction date, so populate additional report row details.
                ' Create report default Detail Refund row - Only used for rows with NO transaction date
                rowId += 1
                aRow = aDataset.BODFA_Waiver.NewBODFA_WaiverRow
                aRow.RowId = rowId
                aRow.BoldRow = False
                aRow.ColumnData = transDate & " " & transTime
                aRow.Reference = detailWaiver.Reference
                subTotReference += 1
                grandTotReference += 1
                aRow.OriginalAmount = detailWaiver.TotalAmount.ToString("c")
                aRow.AmountWaived = detailWaiver.AmountWaived.ToString("c")
                subTotAmount += detailWaiver.AmountWaived
                grandTotAmount += detailWaiver.AmountWaived
                aRow.PartyID = detailWaiver.PartyId
                aRow.PartyName = detailWaiver.PartyName
                aRow.UserName = detailWaiver.UserName

                ' Add the row to the Dataset Table
                aDataset.BODFA_Waiver.AddBODFA_WaiverRow(aRow)

            Next ' Get the next REfund

            ' Output Final Sub Total Report Row
            'rowId += 1
            'subTotal = aDataset.BODFA_Waiver.NewBODFA_WaiverRow
            'subTotal.RowId = rowId
            'subTotal.BoldRow = True
            'subTotal.ColumnData = "Waiver Sub Total"
            'subTotal.Reference = subTotReference.ToString
            'subTotal.OriginalAmount = " "
            'subTotal.AmountWaived = subTotAmount.ToString("c")
            'subTotal.PartyID = " "
            'subTotal.PartyName = " "
            'subTotal.UserName = " "
            'aDataset.BODFA_Waiver.AddBODFA_WaiverRow(subTotal)

            ' Output Rport Grand Totals
            rowId += 1
            subTotal = aDataset.BODFA_Waiver.NewBODFA_WaiverRow
            subTotal.RowId = rowId
            subTotal.BoldRow = True
            subTotal.ColumnData = "Waiver Total"
            subTotal.Reference = grandTotReference.ToString
            subTotal.OriginalAmount = " "
            subTotal.AmountWaived = grandTotAmount.ToString("c")
            subTotal.PartyID = " "
            subTotal.PartyName = " "
            subTotal.UserName = " "
            aDataset.BODFA_Waiver.AddBODFA_WaiverRow(subTotal)

            Return aDataset

        End Function

    End Class

End Namespace

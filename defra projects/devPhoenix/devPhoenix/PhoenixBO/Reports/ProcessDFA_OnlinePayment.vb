Imports System
Imports System.data
Imports System.Collections
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Namespace RPT

    Public Class DFA_OnlinePayment
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim reportResults(0) As BOReportResults
            Dim rptCriteria As ReportCriteria.DFA_OnlinePaymentCriteria = CType(reportCriteria, ReportCriteria.DFA_OnlinePaymentCriteria)

            Dim reportDataset As DataSet

            reportDataset = GetDataset(rptCriteria)

            Dim crystalRPT As New DFA_Online_Payment_RPT

            Dim SearchReference As String = ""

            Dim parameterValues As New Hashtable

            parameterValues.Add("toDate", rptCriteria.ToDate.ToShortDateString & " " & rptCriteria.ToDate.ToLongTimeString)
            parameterValues.Add("costCode", rptCriteria.CostCode)

            reportResults(0) = DoReport(rptCriteria.Description, SearchReference, 0, reportDataset, rptCriteria, saveReport, _
            crystalRPT, reportPrintJobId, printSequence, parameterValues)

            Return reportResults

        End Function


        Public Function GetDataset(ByVal rptCriteria As ReportCriteria.DFA_OnlinePaymentCriteria) As DFA_Online_PaymentData

            Dim aDataset As New DFA_Online_PaymentData
            Dim aRow As DFA_Online_PaymentData.BODFA_Online_PaymentRow
            Dim subTotal As DFA_Online_PaymentData.BODFA_Online_PaymentRow

            ' DFA_OffLine_Payment_Cash - Row 
            aRow = aDataset.BODFA_Online_Payment.NewBODFA_Online_PaymentRow
            aRow.RowId = 1
            aRow.BoldRow = True
            aRow.ColumnData = "Detail - Payments"
            aRow.PhoenixReference = ""
            aRow.Amount = ""
            aRow.PartyID = ""
            aRow.PartyName = ""
            aRow.NumOfApplications = ""
            aRow.RemittanceAdvice = ""
            aRow.PaymentDetails = ""
            aDataset.BODFA_Online_Payment.AddBODFA_Online_PaymentRow(aRow)

            ' DFA_OffLine_Payment_Cash - Row 
            aRow = aDataset.BODFA_Online_Payment.NewBODFA_Online_PaymentRow
            aRow.RowId = 1
            aRow.BoldRow = True
            aRow.ColumnData = "Date & Time"
            aRow.PhoenixReference = "Phoenix Reference"
            aRow.Amount = "Amount"
            aRow.PartyID = "Party ID"
            aRow.PartyName = "Party Name"
            aRow.NumOfApplications = "Number of Applications"
            aRow.RemittanceAdvice = "Remittance Advice?"
            aRow.PaymentDetails = "Payment Details"
            aDataset.BODFA_Online_Payment.AddBODFA_Online_PaymentRow(aRow)

            ' Get Online Payment Details
            Dim payments As ReportDFA.BOReportOnlinePayment
            Dim paymentDetails(-1) As ReportDFA.BOReportOnlinePayment
            paymentDetails = payments.getReportOnlinePayment(rptCriteria.FromDate, rptCriteria.ToDate)

            ' Loop through online payments and create report rows
            Dim rowId As Int32 = 2
            Dim transDate As String
            Dim transTime As String
            Dim prevTransDate As String = ""
            Dim subTotReference As Int32 = 0
            Dim subTotAmount As Decimal = 0
            Dim subTotNumOfApplications As Int32 = 0
            Dim grandTotReference As Int32 = 0
            Dim grandTotAmount As Decimal = 0
            Dim grandTotNumOfApplications As Int32 = 0

            For Each paymentDetail As ReportDFA.BOReportOnlinePayment In paymentDetails

                transDate = CType(paymentDetail.TransactionDateTime, Date).ToShortDateString
                transTime = CType(paymentDetail.TransactionDateTime, Date).ToLongTimeString
                If transDate <> prevTransDate Then ' Do we have a change in transaction date
                    If prevTransDate.Length <> 0 Then
                        ' We have a change in transaction date, so create a sub total line
                        rowId += 1
                        subTotal = aDataset.BODFA_Online_Payment.NewBODFA_Online_PaymentRow
                        subTotal.RowId = rowId
                        subTotal.BoldRow = True
                        subTotal.ColumnData = "Payments Sub Total: " & transDate
                        subTotal.PhoenixReference = subTotReference.ToString
                        subTotal.Amount = subTotAmount.ToString("c")
                        subTotal.PartyID = " "
                        subTotal.PartyName = " "
                        aDataset.BODFA_Online_Payment.AddBODFA_Online_PaymentRow(subTotal)

                        subTotReference = 0
                        subTotAmount = 0
                    End If
                    prevTransDate = transDate
                End If

                ' We have a transaction date, so populate additional report row details.
                ' Create report default Detail Refund row - Only used for rows with NO transaction date
                rowId += 1
                aRow = aDataset.BODFA_Online_Payment.NewBODFA_Online_PaymentRow
                aRow.RowId = rowId
                aRow.BoldRow = False
                aRow.ColumnData = transDate & " " & transTime
                aRow.PhoenixReference = paymentDetail.Reference
                subTotReference += 1
                grandTotReference += 1
                aRow.Amount = paymentDetail.TotalAmount.ToString("c")
                subTotAmount += paymentDetail.TotalAmount
                grandTotAmount += paymentDetail.TotalAmount
                aRow.PartyID = paymentDetail.PartyId
                aRow.PartyName = paymentDetail.PartyName
                aRow.NumOfApplications = paymentDetail.NumberOfApplications.ToString
                subTotNumOfApplications += paymentDetail.NumberOfApplications
                grandTotNumOfApplications += paymentDetail.NumberOfApplications
                aRow.RemittanceAdvice = "No"
                If paymentDetail.RemittanceAdvice Then aRow.RemittanceAdvice = "Yes"
                aRow.PaymentDetails = paymentDetail.Details

                ' Add the row to the Dataset Table
                aDataset.BODFA_Online_Payment.AddBODFA_Online_PaymentRow(aRow)

            Next ' Get the next Cash Payment

            ' Output Final Sub Total Report Row
            rowId += 1
            subTotal = aDataset.BODFA_Online_Payment.NewBODFA_Online_PaymentRow
            subTotal.RowId = rowId
            subTotal.BoldRow = True
            subTotal.ColumnData = "Payments Sub Total"
            subTotal.PhoenixReference = subTotReference.ToString
            subTotal.Amount = subTotAmount.ToString("c")
            subTotal.PartyID = " "
            subTotal.PartyName = " "
            subTotal.NumOfApplications = subTotNumOfApplications.ToString
            subTotal.RemittanceAdvice = " "
            subTotal.PaymentDetails = " "
            aDataset.BODFA_Online_Payment.AddBODFA_Online_PaymentRow(subTotal)

            ' Output Rport Grand Totals
            rowId += 1
            subTotal = aDataset.BODFA_Online_Payment.NewBODFA_Online_PaymentRow
            subTotal.RowId = rowId
            subTotal.BoldRow = True
            subTotal.ColumnData = "Payments Total"
            subTotal.PhoenixReference = grandTotReference.ToString
            subTotal.Amount = grandTotAmount.ToString("c")
            subTotal.PartyID = " "
            subTotal.PartyName = " "
            subTotal.NumOfApplications = grandTotNumOfApplications.ToString
            subTotal.RemittanceAdvice = " "
            subTotal.PaymentDetails = " "
            aDataset.BODFA_Online_Payment.AddBODFA_Online_PaymentRow(subTotal)


            Return aDataset

        End Function

    End Class

End Namespace

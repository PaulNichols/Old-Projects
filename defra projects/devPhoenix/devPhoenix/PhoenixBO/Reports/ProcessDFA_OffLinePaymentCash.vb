Imports System
Imports System.data
Imports System.Collections
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Namespace RPT

    Public Class DFA_OfflinePaymentCash
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim reportResults(0) As BOReportResults

            Dim rptCriteria As ReportCriteria.DFA_OfflinePaymentCashCriteria = CType(reportCriteria, ReportCriteria.DFA_OfflinePaymentCashCriteria)

            Dim reportDataset As DataSet

            reportDataset = GetDataset(rptCriteria)

            Dim crystalRPT As New DFA_Offline_Payment_Cash_RPT

            Dim SearchReference As String = ""

            Dim parameterValues As New Hashtable

            parameterValues.Add("toDate", rptCriteria.ToDate.ToShortDateString & " " & rptCriteria.ToDate.ToLongTimeString)
            parameterValues.Add("costCode", rptCriteria.CostCode)

            reportResults(0) = DoReport(rptCriteria.Description, SearchReference, 0, reportDataset, rptCriteria, saveReport, _
            crystalRPT, reportPrintJobId, printSequence, parameterValues)

            Return reportResults

        End Function


        Public Function GetDataset(ByVal rptCriteria As ReportCriteria.DFA_OfflinePaymentCashCriteria) As DFA_OffLine_Payment_CashData

            Dim aDataset As New DFA_OffLine_Payment_CashData
            Dim aRow As DFA_OffLine_Payment_CashData.BODFA_OffLine_Payment_CashRow
            Dim subTotal As DFA_OffLine_Payment_CashData.BODFA_OffLine_Payment_CashRow

            ' DFA_OffLine_Payment_Cash - Row 
            aRow = aDataset.BODFA_OffLine_Payment_Cash.NewBODFA_OffLine_Payment_CashRow
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
            aRow.PaymentAmount = ""
            aDataset.BODFA_OffLine_Payment_Cash.AddBODFA_OffLine_Payment_CashRow(aRow)

            ' DFA_OffLine_Payment_Cash - Row 
            aRow = aDataset.BODFA_OffLine_Payment_Cash.NewBODFA_OffLine_Payment_CashRow
            aRow.RowId = 2
            aRow.BoldRow = True
            aRow.ColumnData = "Cash"
            aRow.PhoenixReference = ""
            aRow.Amount = ""
            aRow.PartyID = ""
            aRow.PartyName = ""
            aRow.NumOfApplications = ""
            aRow.RemittanceAdvice = ""
            aRow.PaymentDetails = ""
            aRow.PaymentAmount = ""
            aDataset.BODFA_OffLine_Payment_Cash.AddBODFA_OffLine_Payment_CashRow(aRow)

            ' DFA_OffLine_Payment_Cash - Row 
            aRow = aDataset.BODFA_OffLine_Payment_Cash.NewBODFA_OffLine_Payment_CashRow
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
            aRow.PaymentAmount = "Note Amount"
            aDataset.BODFA_OffLine_Payment_Cash.AddBODFA_OffLine_Payment_CashRow(aRow)

            ' Get Offline Payment Cash
            Dim payments As ReportDFA.BOReportOfflinePaymentCash
            Dim paymentCashs(-1) As ReportDFA.BOReportOfflinePaymentCash
            paymentCashs = payments.getReportOfflinePaymentCash(rptCriteria.FromDate, rptCriteria.ToDate)

            ' Loop through Cash payments and create report rows
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

            For Each paymentCash As ReportDFA.BOReportOfflinePaymentCash In paymentCashs

                ' Create report default payment Cash row - Only used for rows with NO transaction date
                rowId += 1
                aRow = aDataset.BODFA_OffLine_Payment_Cash.NewBODFA_OffLine_Payment_CashRow
                aRow.RowId = rowId
                aRow.BoldRow = False
                aRow.ColumnData = " "
                aRow.PhoenixReference = " "
                aRow.Amount = " "
                aRow.PartyID = " "
                aRow.PartyName = " "
                aRow.NumOfApplications = " "
                aRow.RemittanceAdvice = " "
                aRow.PaymentDetails = paymentCash.Details
                aRow.PaymentAmount = paymentCash.Amount.ToString("c")

                If Not paymentCash.TransactionDateTime Is Nothing Then ' Do we have a transaction date?
                    transDate = CType(paymentCash.TransactionDateTime, Date).ToShortDateString
                    transTime = CType(paymentCash.TransactionDateTime, Date).ToLongTimeString
                    If transDate <> prevTransDate Then ' Do we have a change in transaction date
                        If prevTransDate.Length <> 0 Then
                            ' We have a change in transaction date, so create a sub total line
                            'rowId += 1
                            'subTotal = aDataset.BODFA_OffLine_Payment_Cash.NewBODFA_OffLine_Payment_CashRow
                            'subTotal.RowId = rowId
                            'subTotal.BoldRow = True
                            'subTotal.ColumnData = "Cash Sub Total"
                            'subTotal.PhoenixReference = subTotReference.ToString
                            'subTotal.Amount = subTotAmount.ToString("c")
                            'subTotal.PartyID = " "
                            'subTotal.PartyName = " "
                            'subTotal.NumOfApplications = subTotNumOfApplications.ToString
                            'subTotal.RemittanceAdvice = " "
                            'subTotal.PaymentDetails = " "
                            'subTotal.PaymentAmount = " "
                            'aDataset.BODFA_OffLine_Payment_Cash.AddBODFA_OffLine_Payment_CashRow(subTotal)

                            'subTotReference = 0
                            'subTotAmount = 0
                            'subTotNumOfApplications = 0
                        End If
                        prevTransDate = transDate
                    End If

                    ' We have a transaction date, so populate additional report row details.
                    aRow.ColumnData = transDate & " " & transTime
                    aRow.PhoenixReference = paymentCash.Reference
                    subTotReference += 1
                    grandTotReference += 1
                    aRow.Amount = paymentCash.TotalAmount.ToString("c")
                    subTotAmount += paymentCash.TotalAmount
                    grandTotAmount += paymentCash.TotalAmount
                    aRow.PartyID = paymentCash.PartyId
                    aRow.PartyName = paymentCash.PartyName
                    aRow.NumOfApplications = paymentCash.NumberOfApplications.ToString
                    subTotNumOfApplications += paymentCash.NumberOfApplications
                    grandTotNumOfApplications += paymentCash.NumberOfApplications
                    aRow.RemittanceAdvice = "No"
                    If paymentCash.RemittanceAdvice Then aRow.RemittanceAdvice = "Yes"

                End If

                ' Add the row to the Dataset Table
                aDataset.BODFA_OffLine_Payment_Cash.AddBODFA_OffLine_Payment_CashRow(aRow)

            Next ' Get the next Cash Payment

            ' Output Final Sub Total Report Row
            'rowId += 1
            'subTotal = aDataset.BODFA_OffLine_Payment_Cash.NewBODFA_OffLine_Payment_CashRow
            'subTotal.RowId = rowId
            'subTotal.BoldRow = True
            'subTotal.ColumnData = "Cash Sub Total"
            'subTotal.PhoenixReference = subTotReference.ToString
            'subTotal.Amount = subTotAmount.ToString("c")
            'subTotal.PartyID = " "
            'subTotal.PartyName = " "
            'subTotal.NumOfApplications = subTotNumOfApplications.ToString
            'subTotal.RemittanceAdvice = " "
            'subTotal.PaymentDetails = " "
            'subTotal.PaymentAmount = " "
            'aDataset.BODFA_OffLine_Payment_Cash.AddBODFA_OffLine_Payment_CashRow(subTotal)

            ' Output Rport Grand Totals
            rowId += 1
            subTotal = aDataset.BODFA_OffLine_Payment_Cash.NewBODFA_OffLine_Payment_CashRow
            subTotal.RowId = rowId
            subTotal.BoldRow = True
            subTotal.ColumnData = "Cash Total"
            subTotal.PhoenixReference = grandTotReference.ToString
            subTotal.Amount = grandTotAmount.ToString("c")
            subTotal.PartyID = " "
            subTotal.PartyName = " "
            subTotal.NumOfApplications = grandTotNumOfApplications.ToString
            subTotal.RemittanceAdvice = " "
            subTotal.PaymentDetails = " "
            subTotal.PaymentAmount = " "
            aDataset.BODFA_OffLine_Payment_Cash.AddBODFA_OffLine_Payment_CashRow(subTotal)

            Return aDataset

        End Function
    End Class

End Namespace
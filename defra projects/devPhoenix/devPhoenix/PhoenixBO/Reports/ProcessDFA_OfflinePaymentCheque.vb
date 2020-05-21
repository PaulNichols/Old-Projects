Imports System
Imports System.data
Imports System.Collections
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Namespace RPT

Public Class DFA_OfflinePaymentCheque
        Inherits BOReport

    Public Sub New()
        MyBase.New()
    End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim reportResults(0) As BOReportResults

            Dim rptCriteria As ReportCriteria.DFA_OfflinePaymentChequeCriteria = CType(reportCriteria, ReportCriteria.DFA_OfflinePaymentChequeCriteria)

            Dim reportDataset As DataSet

            reportDataset = GetDataset(rptCriteria)

            Dim crystalRPT As New DFA_Offline_Payment_Cheques_RPT

            Dim SearchReference As String = ""

            Dim parameterValues As New Hashtable

            parameterValues.Add("toDate", rptCriteria.ToDate.ToShortDateString & " " & rptCriteria.ToDate.ToLongTimeString)
            parameterValues.Add("costCode", rptCriteria.CostCode)

            reportResults(0) = DoReport(rptCriteria.Description, SearchReference, 0, reportDataset, rptCriteria, saveReport, _
            crystalRPT, reportPrintJobId, printSequence, parameterValues)

            Return reportResults

        End Function


        Public Function GetDataset(ByVal rptCriteria As ReportCriteria.DFA_OfflinePaymentChequeCriteria) As DFA_OffLine_Payment_ChequesData

            Dim aDataset As New DFA_OffLine_Payment_ChequesData
            Dim aRow As DFA_OffLine_Payment_ChequesData.BODFA_OffLine_Payment_ChequesRow
            Dim subTotal As DFA_OffLine_Payment_ChequesData.BODFA_OffLine_Payment_ChequesRow

            ' DFA_OffLine_Payment_Cheque - Row 
            aRow = aDataset.BODFA_OffLine_Payment_Cheques.NewBODFA_OffLine_Payment_ChequesRow
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
            aDataset.BODFA_OffLine_Payment_Cheques.AddBODFA_OffLine_Payment_ChequesRow(aRow)

            ' DFA_OffLine_Payment_Cheque - Row 
            aRow = aDataset.BODFA_OffLine_Payment_Cheques.NewBODFA_OffLine_Payment_ChequesRow
            aRow.RowId = 2
            aRow.BoldRow = True
            aRow.ColumnData = "Cheques"
            aRow.PhoenixReference = ""
            aRow.Amount = ""
            aRow.PartyID = ""
            aRow.PartyName = ""
            aRow.NumOfApplications = ""
            aRow.RemittanceAdvice = ""
            aRow.PaymentDetails = ""
            aRow.PaymentAmount = ""
            aDataset.BODFA_OffLine_Payment_Cheques.AddBODFA_OffLine_Payment_ChequesRow(aRow)

            ' DFA_OffLine_Payment_Cheque - Row 
            aRow = aDataset.BODFA_OffLine_Payment_Cheques.NewBODFA_OffLine_Payment_ChequesRow
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
            aRow.PaymentAmount = "Cheque Amount"
            aDataset.BODFA_OffLine_Payment_Cheques.AddBODFA_OffLine_Payment_ChequesRow(aRow)

            ' Get Offline Payment Cheques
            Dim payments As ReportDFA.BOReportOfflinePaymentCheque
            Dim paymentCheques(-1) As ReportDFA.BOReportOfflinePaymentCheque
            paymentCheques = payments.getReportOfflinePaymentCheque(rptCriteria.FromDate, rptCriteria.ToDate)

            ' Loop through cheque payments and create report rows
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

            For Each paymentCheque As ReportDFA.BOReportOfflinePaymentCheque In paymentCheques

                ' Create report default payment cheque row - Only used for rows with NO transaction date
                rowId += 1
                aRow = aDataset.BODFA_OffLine_Payment_Cheques.NewBODFA_OffLine_Payment_ChequesRow
                aRow.RowId = rowId
                aRow.BoldRow = False
                aRow.ColumnData = " "
                aRow.PhoenixReference = " "
                aRow.Amount = " "
                aRow.PartyID = " "
                aRow.PartyName = " "
                aRow.NumOfApplications = " "
                aRow.RemittanceAdvice = " "
                aRow.PaymentDetails = paymentCheque.Details
                aRow.PaymentAmount = paymentCheque.Amount.ToString("c")

                If Not paymentCheque.TransactionDateTime Is Nothing Then ' Do we have a transaction date?
                    transDate = CType(paymentCheque.TransactionDateTime, Date).ToShortDateString
                    transTime = CType(paymentCheque.TransactionDateTime, Date).ToLongTimeString
                    If transDate <> prevTransDate Then ' Do we have a change in transaction date
                        If prevTransDate.Length <> 0 Then
                            ' We have a change in transaction date, so create a sub total line
                            'rowId += 1
                            'subTotal = aDataset.BODFA_OffLine_Payment_Cheques.NewBODFA_OffLine_Payment_ChequesRow
                            'subTotal.RowId = rowId
                            'subTotal.BoldRow = True
                            'subTotal.ColumnData = "Cheque Sub Total"
                            'subTotal.PhoenixReference = subTotReference.ToString
                            'subTotal.Amount = subTotAmount.ToString("c")
                            'subTotal.PartyID = " "
                            'subTotal.PartyName = " "
                            'subTotal.NumOfApplications = subTotNumOfApplications.ToString
                            'subTotal.RemittanceAdvice = " "
                            'subTotal.PaymentDetails = " "
                            'subTotal.PaymentAmount = " "
                            'aDataset.BODFA_OffLine_Payment_Cheques.AddBODFA_OffLine_Payment_ChequesRow(subTotal)

                            'subTotReference = 0
                            'subTotAmount = 0
                            'subTotNumOfApplications = 0
                        End If
                        prevTransDate = transDate
                    End If

                    ' We have a transaction date, so populate additional report row details.
                    aRow.ColumnData = transDate & " " & transTime
                    aRow.PhoenixReference = paymentCheque.Reference
                    subTotReference += 1
                    grandTotReference += 1
                    aRow.Amount = paymentCheque.TotalAmount.ToString("c")
                    subTotAmount += paymentCheque.TotalAmount
                    grandTotAmount += paymentCheque.TotalAmount
                    aRow.PartyID = paymentCheque.PartyId
                    aRow.PartyName = paymentCheque.PartyName
                    aRow.NumOfApplications = paymentCheque.NumberOfApplications.ToString
                    subTotNumOfApplications += paymentCheque.NumberOfApplications
                    grandTotNumOfApplications += paymentCheque.NumberOfApplications
                    aRow.RemittanceAdvice = "No"
                    If paymentCheque.RemittanceAdvice Then aRow.RemittanceAdvice = "Yes"

                End If

                ' Add the row to the Dataset Table
                aDataset.BODFA_OffLine_Payment_Cheques.AddBODFA_OffLine_Payment_ChequesRow(aRow)

            Next ' Get the next Cheque Payment

            ' Output Final Sub Total Report Row
            'rowId += 1
            'subTotal = aDataset.BODFA_OffLine_Payment_Cheques.NewBODFA_OffLine_Payment_ChequesRow
            'subTotal.RowId = rowId
            'subTotal.BoldRow = True
            'subTotal.ColumnData = "Cheque Sub Total"
            'subTotal.PhoenixReference = subTotReference.ToString
            'subTotal.Amount = subTotAmount.ToString("c")
            'subTotal.PartyID = " "
            'subTotal.PartyName = " "
            'subTotal.NumOfApplications = subTotNumOfApplications.ToString
            'subTotal.RemittanceAdvice = " "
            'subTotal.PaymentDetails = " "
            'subTotal.PaymentAmount = " "
            'aDataset.BODFA_OffLine_Payment_Cheques.AddBODFA_OffLine_Payment_ChequesRow(subTotal)

            ' Output Rport Grand Totals
            rowId += 1
            subTotal = aDataset.BODFA_OffLine_Payment_Cheques.NewBODFA_OffLine_Payment_ChequesRow
            subTotal.RowId = rowId
            subTotal.BoldRow = True
            subTotal.ColumnData = "Cheque Total"
            subTotal.PhoenixReference = grandTotReference.ToString
            subTotal.Amount = grandTotAmount.ToString("c")
            subTotal.PartyID = " "
            subTotal.PartyName = " "
            subTotal.NumOfApplications = grandTotNumOfApplications.ToString
            subTotal.RemittanceAdvice = " "
            subTotal.PaymentDetails = " "
            subTotal.PaymentAmount = " "
            aDataset.BODFA_OffLine_Payment_Cheques.AddBODFA_OffLine_Payment_ChequesRow(subTotal)

            Return aDataset

        End Function

End Class

End Namespace


Option Explicit On
Option Strict On
Imports uk.gov.defra.Phoenix.DO.DataObjects.Views.Collection
Imports uk.gov.defra.Phoenix.DO.DataObjects.Views.Service
Imports uk.gov.defra.Phoenix.DO.DataObjects.Views.Entity

Namespace ReportDFA

    Public Class BOReportOfflinePaymentCheque
        Inherits BOReportPayment

        Public Sub New()
            MyBase.New()
        End Sub

        Property Amount() As Decimal
            Get
                Return mAmount
            End Get
            Set(ByVal Value As Decimal)
                mAmount = Value
            End Set
        End Property
        Private mAmount As Decimal

        Public Shared Function getReportOfflinePaymentCheque(ByVal fromDate As Date, ByVal toDate As Date) As BOReportOfflinePaymentCheque()
            Dim collection As SearchChequeBoundCollection = SearchChequeService.GetChequesByDateRange(fromDate, toDate)
            Dim results(collection.Count - 1) As BOReportOfflinePaymentCheque
            Dim obj As New BOReportOfflinePaymentCheque
            Dim index As Integer = 0

            obj.Reference = ""
            For Each item As SearchCheque In collection
                Dim different As Boolean = item.PaymentReference <> obj.Reference
                obj = New BOReportOfflinePaymentCheque
                obj.Amount = item.Amount
                obj.Details = item.BankSortCode + " " + item.BankAccountNumber + " " + item.SerialNumber
                If item.AccountName <> item.DisplayName Then
                    obj.Details += " " + item.AccountName
                End If
                If different Then
                    obj.TransactionDateTime = item.PaymentDateTime
                    obj.Reference = item.PaymentReference
                    obj.TotalAmount = item.PaymentAmount
                    obj.PartyId = item.PartyId.ToString()
                    obj.PartyName = item.DisplayName
                    obj.NumberOfApplications = item.ApplicationCount
                    obj.RemittanceAdvice = item.RemittanceCount > 0
                End If
                results(index) = obj
                index += 1
            Next
            Return results
        End Function


    End Class ' BOReportOfflinePaymentCheque

End Namespace

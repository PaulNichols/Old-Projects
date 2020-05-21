

Option Explicit On
Option Strict On
Imports uk.gov.defra.Phoenix.DO.DataObjects.Views.Collection
Imports uk.gov.defra.Phoenix.DO.DataObjects.Views.Service
Imports uk.gov.defra.Phoenix.DO.DataObjects.Views.Entity

Namespace ReportDFA

    Public Class BOReportOnlineSummary
        Inherits BOReportSummary
        Implements IBOReportOnlineSummary

        Public Shared Function getReportOnlineSummary(ByVal fromDate As Date, ByVal toDate As Date) As BOReportOnlineSummary
            Dim obj As New BOReportOnlineSummary
            Dim collection1 As SearchCardBoundCollection = SearchCardService.GetCardsByDateRange(fromDate, toDate)
            Dim collection2 As SearchRefundBoundCollection = SearchRefundService.GetRefundsByDateRange(fromDate, toDate)
            Dim hashtable As New hashtable

            For Each item As SearchCash In collection1
                obj.TotalPaymentsAmt += item.Amount
                hashtable.Item(item.PaymentReference) = Nothing
            Next
            For Each item As SearchRefund In collection2
                If CType(item.RefundType, Payments.BORefund.RefundCode) = Payments.BORefund.RefundCode.Online Then
                    obj.TotalRefundsNum += 1
                    obj.TotalRefundsAmt += item.Amount
                End If
            Next
            obj.TotalPaymentsNum = hashtable.Count
            Return obj
        End Function

    End Class

End Namespace

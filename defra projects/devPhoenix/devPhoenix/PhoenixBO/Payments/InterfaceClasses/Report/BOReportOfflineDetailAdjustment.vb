
Option Explicit On
Option Strict On
Imports uk.gov.defra.Phoenix.DO.DataObjects.Views.Collection
Imports uk.gov.defra.Phoenix.DO.DataObjects.Views.Service
Imports uk.gov.defra.Phoenix.DO.DataObjects.Views.Entity

Namespace ReportDFA

Public Class BOReportOfflineDetailAdjustment
    Inherits BOReportDetail

    Public Sub New()
        MyBase.New()
    End Sub

        Public Shared Function getReportOfflineDetailAdjustment(ByVal fromDate As Date, ByVal toDate As Date) As BOReportOfflineDetailAdjustment()
            Dim collection As SearchRefundBoundCollection = SearchRefundService.GetRefundsByDateRange(fromDate, toDate)
            Dim results(collection.Count - 1) As BOReportOfflineDetailAdjustment
            Dim obj As New BOReportOfflineDetailAdjustment
            Dim index As Integer = 0

            For Each item As SearchRefund In collection
                If CType(item.RefundType, Payments.BORefund.RefundCode) = Payments.BORefund.RefundCode.Amendment Then
                    obj = New BOReportOfflineDetailAdjustment
                    obj.TransactionDateTime = item.RefundDateTime
                    obj.PaymentDateTime = item.PaymentDateTime
                    obj.Reference = item.PaymentReference
                    obj.TotalAmount = item.Amount
                    obj.PartyId = item.PartyId.ToString()
                    obj.PartyName = item.DisplayName
                    obj.Notes = item.NotesComments
                    results(index) = obj
                    index += 1
                End If
            Next
            ReDim Preserve results(index - 1)
            Return results
        End Function

End Class ' BOReportOfflineDetailAdjustment

End Namespace

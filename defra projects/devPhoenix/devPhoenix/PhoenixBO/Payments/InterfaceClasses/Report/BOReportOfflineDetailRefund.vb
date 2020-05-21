
Option Explicit On
Option Strict On
Imports uk.gov.defra.Phoenix.DO.DataObjects.Views.Collection
Imports uk.gov.defra.Phoenix.DO.DataObjects.Views.Service
Imports uk.gov.defra.Phoenix.DO.DataObjects.Views.Entity

Namespace ReportDFA

    Public Class BOReportOfflineDetailRefund
        Inherits BOReportDetail


        Public Sub New()
            MyBase.New()
        End Sub


        Public Property RefundDetails() As String
            Get
                Return mRefundDetails
            End Get
            Set(ByVal Value As String)
                mRefundDetails = Value
            End Set
        End Property
        Private mRefundDetails As String

        Public Shared Function getReportOfflineDetailRefund(ByVal fromDate As Date, ByVal toDate As Date) As BOReportOfflineDetailRefund()
            Dim collection As SearchRefundBoundCollection = SearchRefundService.GetRefundsByDateRange(fromDate, toDate)
            Dim results(collection.Count - 1) As BOReportOfflineDetailRefund
            Dim obj As New BOReportOfflineDetailRefund
            Dim index As Integer = 0

            For Each item As SearchRefund In collection
                If CType(item.RefundType, Payments.BORefund.RefundCode) = Payments.BORefund.RefundCode.Manual Then
                    obj = New BOReportOfflineDetailRefund
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

    End Class ' BOReportOfflineDetailRefund

End Namespace

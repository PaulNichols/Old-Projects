
Option Explicit On
Option Strict On
Imports uk.gov.defra.Phoenix.DO.DataObjects.Views.Collection
Imports uk.gov.defra.Phoenix.DO.DataObjects.Views.Service
Imports uk.gov.defra.Phoenix.DO.DataObjects.Views.Entity

Namespace ReportDFA

Public Class BOReportOnlinePayment
    Inherits BOReportPayment

    Public Sub New()
        MyBase.New()
    End Sub

    Public Shared Function getReportOnlinePayment(ByVal fromDate As Date, ByVal toDate As Date) As BOReportOnlinePayment()
            Dim collection As SearchCardBoundCollection = SearchCardService.GetCardsByDateRange(fromDate, toDate)
            Dim results(collection.Count - 1) As BOReportOnlinePayment
            Dim obj As New BOReportOnlinePayment
            Dim index As Integer = 0

            For Each item As SearchCard In collection
                obj = New BOReportOnlinePayment
                obj.TransactionDateTime = item.PaymentDateTime
                obj.Reference = item.PaymentReference
                obj.TotalAmount = item.PaymentAmount
                obj.PartyId = item.PartyId.ToString()
                obj.PartyName = item.DisplayName
                obj.NumberOfApplications = item.ApplicationCount
                obj.RemittanceAdvice = item.RemittanceCount > 0
                obj.Details = item.GatewayAuthorisationCode
                results(index) = obj
                index += 1
            Next
            Return results
        End Function

End Class ' BOReportOnlinePayment

End Namespace

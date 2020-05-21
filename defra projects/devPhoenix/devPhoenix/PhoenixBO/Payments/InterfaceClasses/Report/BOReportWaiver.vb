
Option Explicit On
Option Strict On
Imports uk.gov.defra.Phoenix.DO.DataObjects.Views.Collection
Imports uk.gov.defra.Phoenix.DO.DataObjects.Views.Service
Imports uk.gov.defra.Phoenix.DO.DataObjects.Views.Entity


Namespace ReportDFA

    Public Class BOReportWaiver
        Inherits BOReportBase

        Public Sub New()
            MyBase.New()
        End Sub

        Public Property UserName() As String
            Get
                Return mUserName
            End Get
            Set(ByVal Value As String)
                mUserName = Value
            End Set
        End Property
        Private mUserName As String

        Public Property ApplicationNumber() As String
            Get
                Return mApplicationNumber
            End Get
            Set(ByVal Value As String)
                mApplicationNumber = Value
            End Set
        End Property
        Private mApplicationNumber As String

        Public Property AmountWaived() As Decimal
            Get
                Return mAmountWaived
            End Get
            Set(ByVal Value As Decimal)
                mAmountWaived = Value
            End Set
        End Property
        Private mAmountWaived As Decimal

        Public Shared Function getReportWaiver(ByVal fromDate As Date, ByVal toDate As Date) As BOReportWaiver()
            Dim collection As SearchReductionBoundCollection = SearchReductionService.GetReductionsByDateRange(fromDate, toDate)
            Dim results(collection.Count - 1) As BOReportWaiver
            Dim obj As New BOReportWaiver
            Dim index As Integer = 0

            For Each item As SearchReduction In collection
                obj = New BOReportWaiver
                obj.TransactionDateTime = item.ReductionDateTime
                obj.Reference = item.PaymentReference
                obj.TotalAmount = item.OriginalFee
                obj.AmountWaived = item.Amount
                obj.PartyId = item.PartyId.ToString()
                obj.PartyName = item.DisplayName
                obj.UserName = item.Fullname
                results(index) = obj
                index += 1
            Next
            Return results
        End Function

    End Class

End Namespace

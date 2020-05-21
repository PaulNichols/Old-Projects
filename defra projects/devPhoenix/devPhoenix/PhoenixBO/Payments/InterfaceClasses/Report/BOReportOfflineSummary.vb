Option Explicit On 
Option Strict On
Imports uk.gov.defra.Phoenix.DO.DataObjects.Views.Collection
Imports uk.gov.defra.Phoenix.DO.DataObjects.Views.Service
Imports uk.gov.defra.Phoenix.DO.DataObjects.Views.Entity

Namespace ReportDFA

Public Class BOReportOfflineSummary
    Inherits BOReportSummary
    Implements IBOReportOfflineSummary

    Public Sub New()
        MyBase.New()
    End Sub

    Public Property CashAmt() As Decimal Implements IBOReportOfflineSummary.CashAmt
        Get
            Return mCashAmt
        End Get
        Set(ByVal Value As Decimal)
            mCashAmt = Value
        End Set
    End Property
        Private mCashAmt As Decimal = 0

    Public Property CashNum() As Integer Implements IBOReportOfflineSummary.CashNum
        Get
            Return mCashNum
        End Get
        Set(ByVal Value As Integer)
            mCashNum = Value
        End Set
    End Property
        Private mCashNum As Int32 = 0

    Public Property ChequeAmt() As Decimal Implements IBOReportOfflineSummary.ChequeAmt
        Get
            Return mChequeAmt
        End Get
        Set(ByVal Value As Decimal)
            mChequeAmt = Value
        End Set
    End Property
        Private mChequeAmt As Decimal = 0

    Public Property ChequeNum() As Integer Implements IBOReportOfflineSummary.ChequeNum
        Get
            Return mChequeNum
        End Get
        Set(ByVal Value As Integer)
            mChequeNum = Value
        End Set
    End Property
        Private mChequeNum As Int32 = 0

    Public Property PostalOrderAmt() As Decimal Implements IBOReportOfflineSummary.PostalOrderAmt
        Get
            Return mPostalOrderAmt
        End Get
        Set(ByVal Value As Decimal)
            mPostalOrderAmt = Value
        End Set
    End Property
        Private mPostalOrderAmt As Decimal = 0

    Public Property PostalOrderNum() As Integer Implements IBOReportOfflineSummary.PostalOrderNum
        Get
            Return mPostalOrderNum
        End Get
        Set(ByVal Value As Integer)
            mPostalOrderNum = Value
        End Set
    End Property
        Private mPostalOrderNum As Int32 = 0

    Public Property TotalAdjustmentsAmt() As Decimal Implements IBOReportOfflineSummary.TotalAdjustmentsAmt
        Get
            Return mTotalAdjustmentsAmt
        End Get
        Set(ByVal Value As Decimal)
            mTotalAdjustmentsAmt = Value
        End Set
    End Property
        Private mTotalAdjustmentsAmt As Decimal = 0

    Public Property TotalAdjustmentsNum() As Integer Implements IBOReportOfflineSummary.TotalAdjustmentsNum
        Get
            Return mTotalAdjustmentsNum
        End Get
        Set(ByVal Value As Integer)
            mTotalAdjustmentsNum = Value
        End Set
    End Property
        Private mTotalAdjustmentsNum As Int32 = 0

        Public Shared Function getReportOfflineSummary(ByVal fromDate As Date, ByVal toDate As Date) As BOReportOfflineSummary
            Dim obj As New BOReportOfflineSummary
            Dim collection1 As SearchCashBoundCollection = SearchCashService.GetCashByDateRange(fromDate, toDate)
            Dim collection2 As SearchChequeBoundCollection = SearchChequeService.GetChequesByDateRange(fromDate, toDate)
            Dim collection3 As SearchPostalOrderBoundCollection = SearchPostalOrderService.GetPostalOrdersByDateRange(fromDate, toDate)
            Dim collection4 As SearchRefundBoundCollection = SearchRefundService.GetRefundsByDateRange(fromDate, toDate)
            Dim hashtable1 As New Hashtable
            Dim hashtable2 As New Hashtable
            Dim hashtable3 As New Hashtable

            For Each item As SearchCash In collection1
                obj.CashAmt += item.Amount
                hashtable1.Item(item.PaymentReference) = Nothing
            Next
            For Each item As SearchCheque In collection2
                obj.ChequeAmt += item.Amount
                hashtable2.Item(item.PaymentReference) = Nothing
            Next
            For Each item As SearchPostalOrder In collection3
                obj.PostalOrderAmt += item.Amount
                hashtable3.Item(item.PaymentReference) = Nothing
            Next
            For Each item As SearchRefund In collection4
                Select Case CType(item.RefundType, Payments.BORefund.RefundCode)
                    Case Payments.BORefund.RefundCode.Amendment
                        obj.TotalAdjustmentsNum += 1
                        obj.TotalAdjustmentsAmt += item.Amount
                    Case Payments.BORefund.RefundCode.Manual
                        obj.TotalRefundsNum += 1
                        obj.TotalRefundsAmt += item.Amount
                End Select
            Next
            obj.CashNum = hashtable1.Count
            obj.ChequeNum = hashtable2.Count
            obj.PostalOrderNum = hashtable3.Count
            obj.TotalPaymentsNum = obj.ChequeNum + obj.CashNum + obj.PostalOrderNum
            obj.TotalPaymentsAmt = obj.ChequeAmt + obj.CashAmt + obj.PostalOrderAmt
            Return obj
        End Function

    End Class ' BOReportOfflineSummary

End Namespace

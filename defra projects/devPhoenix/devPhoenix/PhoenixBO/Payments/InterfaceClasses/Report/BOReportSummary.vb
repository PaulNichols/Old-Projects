
Option Explicit On
Option Strict On

Namespace ReportDFA

Public MustInherit Class BOReportSummary
    Implements IBOReportSummary


    Public Property TotalPaymentsAmt() As Decimal Implements IBOReportSummary.TotalPaymentsAmt
        Get
            Return mTotalPaymentsAmt
        End Get
        Set(ByVal Value As Decimal)
            mTotalPaymentsAmt = Value
        End Set
    End Property
    Private mTotalPaymentsAmt As Decimal

    Public Property TotalPaymentsNum() As Integer Implements IBOReportSummary.TotalPaymentsNum
        Get
            Return mTotalPaymentsNum
        End Get
        Set(ByVal Value As Integer)
            mTotalPaymentsNum = Value
        End Set
    End Property
    Private mTotalPaymentsNum As Int32

    Public Property TotalRefundsAmt() As Decimal Implements IBOReportSummary.TotalRefundsAmt
        Get
            Return mTotalRefundsAmt
        End Get
        Set(ByVal Value As Decimal)
            mTotalRefundsAmt = Value
        End Set
    End Property
    Private mTotalRefundsAmt As Decimal

    Public Property TotalRefundsNum() As Integer Implements IBOReportSummary.TotalRefundsNum
        Get
            Return mTotalRefundsNum
        End Get
        Set(ByVal Value As Integer)
            mTotalRefundsNum = Value
        End Set
    End Property
    Private mTotalRefundsNum As Int32
End Class

End Namespace

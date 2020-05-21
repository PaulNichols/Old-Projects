
Option Explicit On
Option Strict On

Namespace ReportDFA

    Public Class BOReportDetail
        Inherits BOReportBase
        Implements IBOReportDetail

        Public Property Notes() As String Implements IBOReportDetail.Notes
            Get
                Return mNotes
            End Get
            Set(ByVal Value As String)
                mNotes = Value
            End Set
        End Property
        Private mNotes As String

        Public Property PaymentDateTime() As Date Implements IBOReportDetail.PaymentDateTime
            Get
                Return mPaymentDateTime
            End Get
            Set(ByVal Value As Date)
                mPaymentDateTime = Value
            End Set
        End Property
        Private mPaymentDateTime As Date

    End Class ' BOReportOfflineDetail

End Namespace

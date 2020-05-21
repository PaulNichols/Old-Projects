
Option Explicit On
Option Strict On

Namespace ReportDFA

    Public MustInherit Class BOReportBase
        Implements IBOReportBase

        Public Property PartyId() As String Implements IBOReportBase.PartyId
            Get
                Return mPartyId
            End Get
            Set(ByVal Value As String)
                mPartyId = Value
            End Set
        End Property
        Private mPartyId As String

        Public Property PartyName() As String Implements IBOReportBase.PartyName
            Get
                Return mPartyName
            End Get
            Set(ByVal Value As String)
                mPartyName = Value
            End Set
        End Property
        Private mPartyName As String

        Public Property Reference() As String Implements IBOReportBase.Reference
            Get
                Return mReference
            End Get
            Set(ByVal Value As String)
                mReference = Value
            End Set
        End Property
        Private mReference As String

        Public Property TotalAmount() As Decimal Implements IBOReportBase.TotalAmount
            Get
                Return mTotalAmount
            End Get
            Set(ByVal Value As Decimal)
                mTotalAmount = Value
            End Set
        End Property
        Private mTotalAmount As Decimal

        Public Property TransactionDateTime() As Object Implements IBOReportBase.TransactionDateTime
            Get
                Return mTransactionDateTime
            End Get
            Set(ByVal Value As Object)
                mTransactionDateTime = Value
            End Set
        End Property
        Private mTransactionDateTime As Object

    End Class ' BOReportBase

End Namespace

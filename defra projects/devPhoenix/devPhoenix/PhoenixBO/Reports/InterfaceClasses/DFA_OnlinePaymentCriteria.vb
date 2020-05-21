
Namespace ReportCriteria
    <Serializable()> _
    Public Class DFA_OnlinePaymentCriteria
        Inherits ReportCriteria

        Public Sub New()
            MyBase.new()
        End Sub

        Public Property FromDate() As Date
            Get
                Return mFromDate
            End Get
            Set(ByVal Value As Date)
                mFromDate = Value
            End Set
        End Property
        Private mFromDate As Date

        Public Property ToDate() As Date
            Get
                Return mToDate
            End Get
            Set(ByVal Value As Date)
                mToDate = Value
            End Set
        End Property
        Private mToDate As Date

        Public Property CostCode() As String
            Get
                Return mCostCode
            End Get
            Set(ByVal Value As String)
                mCostCode = Value
            End Set
        End Property
        Private mCostCode As String

        Friend Overrides ReadOnly Property Report() As RPT.BOReport
            Get
                Return New RPT.DFA_OnlinePayment
            End Get
        End Property
    End Class

End Namespace

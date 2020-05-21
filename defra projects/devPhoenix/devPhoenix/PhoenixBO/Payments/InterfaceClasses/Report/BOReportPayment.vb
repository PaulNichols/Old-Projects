
Option Explicit On
Option Strict On

Namespace ReportDFA

    Public MustInherit Class BOReportPayment
        Inherits BOReportBase
        Implements iBOReportPayment

        Public Property Details() As String Implements iBOReportPayment.Details
            Get
                Return mDetails
            End Get
            Set(ByVal Value As String)
                mDetails = Value
            End Set
        End Property
        Private mDetails As String

        Public Property NumberOfApplications() As Integer Implements iBOReportPayment.NumberOfApplications
            Get
                Return mNumberOfApplications
            End Get
            Set(ByVal Value As Integer)
                mNumberOfApplications = Value
            End Set
        End Property
        Private mNumberOfApplications As Integer

        Public Property RemittanceAdvice() As Boolean Implements iBOReportPayment.RemittanceAdvice
            Get
                Return mRemittanceAdvice
            End Get
            Set(ByVal Value As Boolean)
                mRemittanceAdvice = Value
            End Set
        End Property
        Private mRemittanceAdvice As Boolean

    End Class ' BOReportOfflinePayment

End Namespace

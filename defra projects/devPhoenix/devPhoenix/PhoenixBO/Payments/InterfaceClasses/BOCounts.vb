Imports uk.gov.defra.Phoenix.DO.DataObjects

Namespace Payments
    Public Class BOCounts
        Inherits PaymentsBaseBO
        Implements IBOCounts

        Private mApplicationCount As Int32
        Private mOpenBasketApplicationCount As Int32

        Public Property ApplicationCount() As Integer Implements IBOCounts.ApplicationCount
            Get
                Return mApplicationCount
            End Get
            Set(ByVal Value As Integer)
                mApplicationCount = Value
            End Set
        End Property

        Public Property OpenBasketApplicationCount() As Integer Implements IBOCounts.OpenBasketApplicationCount
            Get
                Return mOpenBasketApplicationCount
            End Get
            Set(ByVal Value As Integer)
                mOpenBasketApplicationCount = Value
            End Set
        End Property
    End Class
End Namespace
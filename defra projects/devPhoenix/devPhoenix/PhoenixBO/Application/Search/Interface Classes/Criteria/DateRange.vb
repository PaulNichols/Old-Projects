Namespace Application.Search
    <Serializable()> _
    Public Class DateRange
        Public Sub New()
        End Sub

        Public Property From() As Object
            Get
                Return mFrom
            End Get
            Set(ByVal Value As Object)
                mFrom = Value
            End Set
        End Property
        Private mFrom As Object

        Public Property [To]() As Object
            Get
                Return mTo
            End Get
            Set(ByVal Value As Object)
                mTo = Value
            End Set
        End Property
        Private mTo As Object
    End Class
End Namespace

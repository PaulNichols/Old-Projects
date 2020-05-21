Namespace Application.Bird.Registration
    <Serializable()> _
    Public Class Conviction
        Public Sub New()
        End Sub

        Public Property ConvictionDate() As Date
            Get
                Return mConvictionDate
            End Get
            Set(Byval Value As Date)
                mConvictionDate = Value
            End Set
        End Property
        Private mConvictionDate As Date

        Public Property Court() As String
            Get
                Return mCourt
            End Get
            Set(Byval Value As String)
                mCourt = Value
            End Set
        End Property
        Private mCourt As String

        Public Property Offence() As String
            Get
                Return mOffence
            End Get
            Set(Byval Value As String)
                mOffence = Value
            End Set
        End Property
        Private mOffence As String
        
    End Class
End Namespace

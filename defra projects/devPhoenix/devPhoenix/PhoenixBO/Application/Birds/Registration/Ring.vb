Namespace Application.Bird.Registration
    <Serializable()> _
    Public Class Ring
        Public Sub New()
        End Sub

        Public Property RingNumber() As String
            Get
                Return mRingNumber
            End Get
            Set(Byval Value As String)
                mRingNumber = Value
            End Set
        End Property
        Private mRingNumber As String

        Public Property RingSize() As String
            Get
                Return mRingSize
            End Get
            Set(Byval Value As String)
                mRingSize = Value
            End Set
        End Property
        Private mRingSize As String

        Public Property RingType() As Int32
            Get
                Return mRingType
            End Get
            Set(Byval Value As Int32)
                mRingType = Value
            End Set
        End Property
        Private mRingType As Int32

        Public Property RingFate() As Object
            Get
                Return mRingFate
            End Get
            Set(Byval Value As Object)
                mRingFate = Value
            End Set
        End Property
        Private mRingFate As Object

    End Class
End Namespace

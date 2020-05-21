Namespace Application.Bird.Registration
    <Serializable()> _
    Public Class ClutchSpecimen
        Inherits AdultSpecimenType

        Public Sub New()
        End Sub

        Public Property Egg() As ClutchEgg
            Get
                Return mEgg
            End Get
            Set(ByVal Value As ClutchEgg)
                mEgg = Value
            End Set
        End Property
        Private mEgg As ClutchEgg
    End Class
End Namespace
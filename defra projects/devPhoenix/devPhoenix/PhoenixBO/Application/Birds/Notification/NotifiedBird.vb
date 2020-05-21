Namespace Application.Bird.Notification
    <Serializable()> _
    Public Class NotifiedBird
        Public Sub New()
            Dim s As Registration.SpecimenType
        End Sub

        Public Property NotifiedIdMark() As Registration.IDMark
            Get
                Return mNotifiedIdMark
            End Get
            Set(Byval Value As Registration.IDMark)
                mNotifiedIdMark = Value
            End Set
        End Property
        Private mNotifiedIdMark As Registration.IDMark

        Public Property NotifiedSpecimen() As Registration.SpecimenType
            Get
                Return mNotifiedSpecimen
            End Get
            Set(Byval Value As Registration.SpecimenType)
                mNotifiedSpecimen = Value
            End Set
        End Property
        Private mNotifiedSpecimen As Registration.SpecimenType

        Public Property CustomerEnteredArticle10Reference() As String
            Get
                Return mCustomerEnteredArticle10Reference
            End Get
            Set(ByVal Value As String)
                mCustomerEnteredArticle10Reference = Value
            End Set
        End Property
        Private mCustomerEnteredArticle10Reference As String
    End Class
End Namespace

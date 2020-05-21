Namespace Application.Bird.Registration
    <Serializable()> _
    Public Class AdultBredSpecimen
        Inherits AdultSpecimenType

        Public Sub New()
        End Sub

        Public Property Statements() As Statements
            Get
                Return mStatements
            End Get
            Set(ByVal Value As Statements)
                mStatements = Value
            End Set
        End Property
        Private mStatements As Statements = New Statements
    End Class
End Namespace

Namespace Attributes
    Public Class TableDescription
        Inherits Attribute

        Public Sub New(ByVal description As String)
            mDescription = description
        End Sub

        Public ReadOnly Property Description() As String
            Get
                Return mDescription
            End Get
        End Property
        Private mDescription As String
    End Class
End Namespace

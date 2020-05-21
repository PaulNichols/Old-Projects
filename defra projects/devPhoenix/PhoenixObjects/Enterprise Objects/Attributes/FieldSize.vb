Namespace Attributes
    Public Class FieldSize
        Inherits Attribute

        Public Sub New(ByVal size As Int32)
            mSize = size
        End Sub

        Public ReadOnly Property Size() As Int32
            Get
                Return mSize
            End Get
        End Property
        Private mSize As Int32
    End Class
End Namespace

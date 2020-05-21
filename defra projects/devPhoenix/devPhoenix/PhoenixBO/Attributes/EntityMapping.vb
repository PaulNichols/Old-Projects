<AttributeUsage(AttributeTargets.Class, AllowMultiple:=False)> _
Public Class EntityMapping
    Inherits Attribute
    Public ReadOnly Property Entity() As Type
        Get
            Return mEntity
        End Get
    End Property
    Private mEntity As Type
    Public Sub New(ByVal entity As Type)
        MyBase.new()
        mEntity = entity
    End Sub
End Class

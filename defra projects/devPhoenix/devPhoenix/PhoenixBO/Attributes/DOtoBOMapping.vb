
<AttributeUsage(AttributeTargets.Property, AllowMultiple:=False)> _
Public Class DOtoBOMapping
    Inherits Attribute

    Private mDatafield As String

    Public ReadOnly Property DataField() As String
        Get
            Return mDatafield
        End Get
    End Property

    Public Sub New(ByVal dataField As String)
        MyBase.New()
        mDatafield = dataField
    End Sub

End Class



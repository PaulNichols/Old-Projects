<AttributeUsage(AttributeTargets.Class, AllowMultiple:=False)> _
Public Class CollectionMapping
    Inherits Attribute
    Public ReadOnly Property Collection() As Type
        Get
            Return mCollection
        End Get
    End Property
    Private mCollection As Type
    Public Sub New(ByVal collection As Type)
        MyBase.new()
        mCollection = collection
    End Sub
End Class

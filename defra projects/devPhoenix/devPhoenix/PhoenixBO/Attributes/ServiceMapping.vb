<AttributeUsage(AttributeTargets.Class, AllowMultiple:=False)> _
Public Class ServiceMapping
    Inherits Attribute
    Public ReadOnly Property Service() As Type
        Get
            Return mService
        End Get
    End Property
    Private mService As Type
    Public Sub New(ByVal service As Type)
        MyBase.new()
        mService = service
    End Sub
End Class

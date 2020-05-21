Public Class SystemEnvironment
    Implements IEnvironmental

    Public ReadOnly Property Now() As Date Implements IEnvironmental.Now
        Get
            Return DateTime.Now
        End Get
    End Property
End Class

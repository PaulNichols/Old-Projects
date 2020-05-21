Public Class MockSystemEnvironmental
    Implements IEnvironmental

    Private mCurrentTime As DateTime

    Public Sub New(ByVal [when] As DateTime)
        mCurrentTime = [when]
    End Sub

    Public Sub New()
        MyClass.New(DateTime.Now)
    End Sub

    Public ReadOnly Property Now() As Date Implements IEnvironmental.Now
        Get
            Return mCurrentTime
        End Get
    End Property

    Public Sub IncrementMinutes(ByVal minutes As Int32)
        mCurrentTime = mCurrentTime.AddMinutes(minutes)
    End Sub
End Class

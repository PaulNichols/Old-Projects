<Serializable()> _
Public Class Stamp
    Implements IStamp

    Public Sub New()
        MyBase.new()
    End Sub

    Public Sub New(ByVal userId As Int64, ByVal [date] As Date)
        MyClass.New()
        mDate = [date]
        mUser = New BOAuthorisedUser(userId)
    End Sub

    Public Sub New(ByVal userId As Object, ByVal [date] As Date)
        mDate = [date]
        Try
            If userId Is Nothing OrElse userId.Equals(Convert.DBNull) OrElse CType(userId, Int64) = 0 Then
                mUser = Nothing
            Else
                mUser = New BOAuthorisedUser(CType(userId, Int64))
            End If
        Catch ex As Exception
            mUser = Nothing
        End Try
    End Sub

    Public Property [Date]() As Date Implements IStamp.Date
        Get
            Return mDate
        End Get
        Set(ByVal Value As Date)
            mDate = Value
        End Set
    End Property
    Private mDate As Date

    Public Property User() As BOAuthorisedUser Implements IStamp.User
        Get
            Return mUser
        End Get
        Set(ByVal Value As BOAuthorisedUser)
            mUser = Value
        End Set
    End Property
    Private mUser As BOAuthorisedUser

    Public Property UserName() As String Implements IStamp.UserName
        Get
            If mUser Is Nothing Then
                Return String.Empty
            Else
                Return mUser.FullName
            End If
        End Get
        Set(ByVal Value As String)
            Try
                If mUser Is Nothing Then
                    mUser = New BOAuthorisedUser
                End If
                mUser.FullName = Value
            Catch ex As Exception
                'log4net.Config.XmlConfigurator.Configure(New IO.FileInfo("C:\Visual Studio Projects\devPhoenix\Common Assemblies\App.Config"))
                'Dim Logger As log4net.ILog = log4net.LogManager.GetLogger(GetType(Stamp))
                'Logger.Error("Exception thrown from UserName setter", ex)
            End Try
        End Set
    End Property

    Public Property UserId() As Object Implements IStamp.UserId
        Get
            If mUser Is Nothing Then
                Return Nothing
            Else
                Return mUser.SSOUserid
            End If
        End Get
        Set(ByVal Value As Object)
            Try
                If mUser Is Nothing Then
                    mUser = New BOAuthorisedUser(CType(Value, Int64))
                End If
            Catch
            End Try
        End Set
    End Property
End Class

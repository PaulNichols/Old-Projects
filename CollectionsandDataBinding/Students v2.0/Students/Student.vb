Public Class Student
    Private m_FirstName As String
    Private m_LastName As String
    Private m_Grade As Integer
    Private m_Age As Integer

    Public Event FirstNameChanged As System.EventHandler
    Public Event LastNameChanged As System.EventHandler
    Public Event GradeChanged As System.EventHandler
    Public Event AgeChanged As System.EventHandler

    Public Sub New()
    End Sub

    Public Sub New(ByVal FirstName As String, ByVal LastName As String, _
                    Optional ByVal Grade As Integer = 0, Optional ByVal Age As Integer = 0)
        With Me
            .m_FirstName = FirstName
            .m_LastName = LastName
            .m_Grade = Grade
            .m_Age = Age
        End With
    End Sub

    Public Property FirstName() As String
        Get
            Return m_FirstName
        End Get
        Set(ByVal Value As String)
            m_FirstName = Value
            RaiseEvent FirstNameChanged(Me, EventArgs.Empty)
        End Set
    End Property

    Public Property LastName() As String
        Get
            Return m_LastName
        End Get
        Set(ByVal Value As String)
            m_LastName = Value
            RaiseEvent LastNameChanged(Me, EventArgs.Empty)
        End Set
    End Property

    Public Property Grade() As Integer
        Get
            Return m_Grade
        End Get
        Set(ByVal Value As Integer)
            m_Grade = Value
            RaiseEvent GradeChanged(Me, EventArgs.Empty)
        End Set
    End Property

    Public Property Age() As Integer
        Get
            Return m_Age
        End Get
        Set(ByVal Value As Integer)
            m_Age = Value
            RaiseEvent AgeChanged(Me, EventArgs.Empty)
        End Set
    End Property
End Class

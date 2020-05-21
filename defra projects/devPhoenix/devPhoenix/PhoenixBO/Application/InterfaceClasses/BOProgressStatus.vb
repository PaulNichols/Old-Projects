Namespace Application.ProgressStatus
    <Serializable()> _
    Public MustInherit Class BOProgressStatus
        Implements IBOProgressStatus

        Public Sub New()
        End Sub

        Public Sub Load(ByVal id As Int32, ByVal code As String, ByVal description As String)
            mID = id
            mCode = code
            mDescription = description
        End Sub

        Public Property Code() As String Implements IBOProgressStatus.Code
            Get
                Return mCode
            End Get
            Set(ByVal Value As String)
                mCode = Value
            End Set
        End Property
        Private mCode As String

        Public Property Description() As String Implements IBOProgressStatus.Description
            Get
                Return mDescription
            End Get
            Set(ByVal Value As String)
                mDescription = Value
            End Set
        End Property
        Private mDescription As String

        Public Property ID() As Integer Implements IBOProgressStatus.ID
            Get
                Return mID
            End Get
            Set(ByVal Value As Integer)
                mID = Value
            End Set
        End Property
        Private mID As Int32
    End Class
End Namespace


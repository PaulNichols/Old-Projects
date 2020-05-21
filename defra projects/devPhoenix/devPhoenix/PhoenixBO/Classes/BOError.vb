<Serializable()> Public Class BOError
    <Serializable()> Public Enum ErrorCodes
        AddressCountMismatch
        MissingPerson
        MissingBusiness
        NoMailingAddress
        MailingAddressNoLongerExists
        NeedOneActiveAddress
        AddressValidationError
        PersonValidationError
        ContactCountMismatch
        NoPrimaryContact
        ContactNoLongerExists
        ContactValidationError
        NeedOneActiveContact
        NeedOneEmailAddress
    End Enum
    Public Sub New()
    End Sub

    Public Sub New(ByVal id As ErrorCodes)
        Me.ID = id
    End Sub

    Public Property ID() As ErrorCodes
        Get
            Return mID
        End Get
        Set(ByVal Value As ErrorCodes)
            If mID <> Value Or mErrorMessage Is Nothing Then
                mID = Value
                mErrorMessage = ValidationManager.GetMessage(Value)
            End If
        End Set
    End Property
    Protected mID As ErrorCodes

    Public Property ErrorMessage() As String
        Get
            Return mErrorMessage
        End Get
        Set(ByVal Value As String)
            mErrorMessage = Value
        End Set
    End Property
    Protected mErrorMessage As String

    Public Property Stage() As Int32
        Get
            Return mStage
        End Get
        Set(ByVal Value As Int32)
            mStage = Value
        End Set
    End Property
    Protected mStage As Int32

    Public Property IsWarning() As Boolean
        Get
            Return mIsWarning
        End Get
        Set(ByVal Value As Boolean)
            mIsWarning = Value
        End Set
    End Property
    Protected mIsWarning As Boolean

    Public Property URL() As String
        Get
            Return mURL
        End Get
        Set(ByVal Value As String)
            mURL = Value
        End Set
    End Property
    Protected mURL As String
End Class

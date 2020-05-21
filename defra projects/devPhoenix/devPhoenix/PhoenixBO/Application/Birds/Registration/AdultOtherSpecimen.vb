Namespace Application.Bird.Registration
    <Serializable()> _
    Public Class AdultOtherSpecimen
        Inherits AdultSpecimenType
        Implements IDateAcquired            'MLD 20/1/5 added

        Public Sub New()
        End Sub

        Public Property DateAcquired() As Date Implements IDateAcquired.DateAcquired 'MLD 20/1/5 added
            Get
                If mDateAcquired.Ticks = 0 Then mDateAcquired = Date.Now
                Return mDateAcquired
            End Get
            Set(ByVal Value As Date)
                mDateAcquired = Value
            End Set
        End Property
        Private mDateAcquired As Date

        Public Property Statements() As Statements
            Get
                Return mStatements
            End Get
            Set(ByVal Value As Statements)
                mStatements = Value
            End Set
        End Property
        Private mStatements As Statements = New Statements

        Public Property AcquisitionMethod() As BaseBird.AcquisitionMethodTypes
            Get
                Return mAcquisitionMethod
            End Get
            Set(ByVal Value As BaseBird.AcquisitionMethodTypes)
                mAcquisitionMethod = Value
            End Set
        End Property
        Private mAcquisitionMethod As BaseBird.AcquisitionMethodTypes

        Public Property AcquisitionMethod_Helper() As String
            Get
                Return System.Enum.GetName(GetType(BaseBird.AcquisitionMethodTypes), mAcquisitionMethod)
            End Get
            Set(ByVal Value As String)
                'helper. so not set required.  Just for proxy
            End Set
        End Property

        Friend Sub SetAquisitionMethod(ByVal newValue As String)
            If Not newValue Is Nothing AndAlso newValue.Length > 0 Then
                Dim Result As BaseBird.AcquisitionMethodTypes
                Try
                    Result = CType(System.Enum.Parse(GetType(BaseBird.AcquisitionMethodTypes), newValue), BaseBird.AcquisitionMethodTypes)
                Catch
                    Throw New ArgumentException(newValue & " cannot be parsed into AquisitionMethod")
                End Try
                mAcquisitionMethod = Result
            End If
        End Sub

        Public Property AcquisitionDetails() As String
            Get
                Return mAcquisitionDetails
            End Get
            Set(ByVal Value As String)
                mAcquisitionDetails = Value
            End Set
        End Property
        Private mAcquisitionDetails As String

        'optional
        Public Property PreviousKeeper() As PreviousKeeperAddress
            Get
                Return mPreviousKeeper
            End Get
            Set(ByVal Value As PreviousKeeperAddress)
                mPreviousKeeper = Value
            End Set
        End Property
        Private mPreviousKeeper As PreviousKeeperAddress

        'optional 
        Public Property EvidenceExplanation() As String
            Get
                Return mEvidenceExplanation
            End Get
            Set(ByVal Value As String)
                mEvidenceExplanation = Value
            End Set
        End Property
        Private mEvidenceExplanation As String

        Public Property KeptAddressId() As Object
            Get
                Return mKeptAddressId
            End Get
            Set(ByVal Value As Object)
                mKeptAddressId = Value
            End Set
        End Property
        Private mKeptAddressId As Object

        Public Property KeptAddress_Helper() As String
            Get
                Return AdultFoundSpecimen.KeptAddress(mKeptAddressId)
            End Get
            Set(ByVal Value As String)
                'for proxy
            End Set
        End Property
    End Class
End Namespace

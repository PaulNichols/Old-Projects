Namespace Application.Bird.Registration
    <Serializable()> _
    Public Class AdultFoundSpecimen
        Inherits AdultSpecimenType
        Implements IDateAcquired

        Public Sub New()
        End Sub

        Public Property InjuryDetails() As String
            Get
                Return mInjuryDetails
            End Get
            Set(ByVal Value As String)
                mInjuryDetails = Value
            End Set
        End Property
        Private mInjuryDetails As String

        Public Property AcquisitionDetails() As String
            Get
                Return mAcquisitionDetails
            End Get
            Set(ByVal Value As String)
                mAcquisitionDetails = Value
            End Set
        End Property
        Private mAcquisitionDetails As String

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

        Public Property DateAcquired() As Date Implements IDateAcquired.DateAcquired
            Get
                If mDateAcquired.Ticks = 0 Then mDateAcquired = Date.Today
                Return mDateAcquired
            End Get
            Set(ByVal Value As Date)
                mDateAcquired = Value
            End Set
        End Property
        Private mDateAcquired As Date

        Public Property DateFound() As Date
            Get
                Return mDateFound
            End Get
            Set(ByVal Value As Date)
                mDateFound = Value
            End Set
        End Property
        Private mDateFound As Date

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
                Return KeptAddress(mKeptAddressId)
            End Get
            Set(ByVal Value As String)
                'for proxy
            End Set
        End Property

        Friend Shared Function KeptAddress(ByVal keptAddressId As Object) As String
            Dim Result As String = String.Empty
            If Not (keptAddressId Is Nothing OrElse _
               Not TypeOf keptAddressId Is Int32 OrElse _
               CType(keptAddressId, Int32) <= 0) Then
                Dim Id As Int32 = CType(keptAddressId, Int32)
                Try
                    Dim Address As New BO.Party.BOReadOnlyAddress(Id)
                    If Not Address Is Nothing Then
                        Result = Address.ReportAddress
                    End If
                Catch ex As RecordDoesNotExist
                    'only catch this error.  This means that the record
                    'isn't there so just return an empty string.
                End Try
            End If
            Return Result
        End Function

        Public Property Statements() As Statements
            Get
                Return mStatements
            End Get
            Set(ByVal Value As Statements)
                mStatements = Value
            End Set
        End Property
        Private mStatements As Statements = New Statements
    End Class
End Namespace

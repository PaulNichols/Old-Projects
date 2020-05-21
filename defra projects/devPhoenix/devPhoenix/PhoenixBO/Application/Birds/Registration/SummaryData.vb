Imports uk.gov.defra.Phoenix.BO
Namespace Application.Bird.Registration.SummaryData
    <Serializable()> _
    Public Class BaseSummary
        Public Sub New()
        End Sub

        Public Shared ReadOnly Property ConfigFileDirectory() As String
            Get
                Dim e As New EnterpriseObjects.EnterpriseApplication
                Return e.ConfigFileDirectory.ToString()
            End Get
        End Property

        Public Shared ReadOnly Property ConfigFileName() As String
            Get
                Dim e As New EnterpriseObjects.EnterpriseApplication
                Return e.ConfigFileName
            End Get
        End Property

        Public Shared ReadOnly Property ConnectionString() As String
            Get
                Dim e As New EnterpriseObjects.EnterpriseApplication
                Return e.ConnectionString.ToString()
            End Get
        End Property

        Public Sub New(ByVal birdBO As BirdRegistration, ByVal ssoUserId As Int64)
            MyClass.New()

            Dim IsCustomer As Boolean = Common.IsInRole(ssoUserId, Common.RolesList.Customer)
            With birdBO
                ApplicationId = .ApplicationId
                SubmitDate = .SubmittedDate.ToString("dd/MM/yy")
                Dim Keeper As New Party.BOParty(.PartyId)
                If Keeper.IsBusiness Then
                    Keeper = New Party.BOPartyBusiness(.PartyId)
                Else
                    Keeper = New Party.BOPartyIndividual(.PartyId)
                End If
                KeeperId = Keeper.AuthorisedPartyId.ToString()
                KeeperName = Keeper.DisplayName

                Status = .ApplicationStatus_Internal(ssoUserId)
                AssignedTo = .AssignedTo_Helper
                If .InspectorDecisionMade Then
                    InspectionAdvice = Application.Search.ApplicationSearch.ConvertToEnglishBoolean(.InspectorDecisionMade)
                Else
                    InspectionAdvice = String.Empty
                End If
            End With
        End Sub

        Public Property ApplicationId() As Int32
            Get
                Return mApplicationId
            End Get
            Set(ByVal Value As Int32)
                mApplicationId = Value
            End Set
        End Property
        Private mApplicationId As Int32

        Public Property SubmitDate() As String
            Get
                Return mSubmitDate
            End Get
            Set(ByVal Value As String)
                mSubmitDate = Value
            End Set
        End Property
        Private mSubmitDate As String

        Public Property KeeperId() As String
            Get
                Return mKeeperId
            End Get
            Set(ByVal Value As String)
                mKeeperId = Value
            End Set
        End Property
        Private mKeeperId As String

        Public Property KeeperName() As String
            Get
                Return mKeeperName
            End Get
            Set(ByVal Value As String)
                mKeeperName = Value
            End Set
        End Property
        Private mKeeperName As String

        Public Property Status() As String
            Get
                Return mStatus
            End Get
            Set(ByVal Value As String)
                mStatus = Value
            End Set
        End Property
        Private mStatus As String

        Public Property AssignedTo() As String
            Get
                Return mAssignedTo
            End Get
            Set(ByVal Value As String)
                mAssignedTo = Value
            End Set
        End Property
        Private mAssignedTo As String

        Public Property InspectionAdvice() As String
            Get
                Return mInspectionAdvice
            End Get
            Set(ByVal Value As String)
                mInspectionAdvice = Value
            End Set
        End Property
        Private mInspectionAdvice As String
    End Class

    Public Class AdultSummary
        Inherits BaseSummary

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal birdBO As BirdRegistration, ByVal ssoUserId As Int64)
            MyBase.New(birdBO, ssoUserId)
        End Sub
    End Class

    Public Class ChickSummary
        Inherits BaseSummary

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal birdBO As BirdRegistration, ByVal ssoUserId As Int64)
            MyBase.New(birdBO, ssoUserId)

            ScientificName = birdBO.GetExpectedNames()(0)
            With CType(birdBO.RegistrationApplication, Clutch)
                NumOfEggs = .Eggs.Length
                If Not .LastLaidDate Is Nothing AndAlso TypeOf .LastLaidDate Is Date AndAlso CType(.LastLaidDate, Date).Ticks > 0 Then
                    HatchDate = CType(.LastLaidDate, Date).ToShortDateString()
                Else
                    HatchDate = String.Empty
                End If
            End With
        End Sub

        Public Property ScientificName() As String
            Get
                Return mScientificName
            End Get
            Set(ByVal Value As String)
                mScientificName = Value
            End Set
        End Property
        Private mScientificName As String

        'of which egg??
        'SCS - Should show the last laid date
        Public Property HatchDate() As String
            Get
                Return mHatchDate
            End Get
            Set(ByVal Value As String)
                mHatchDate = Value
            End Set
        End Property
        Private mHatchDate As String

        Public Property NumOfEggs() As Int32
            Get
                Return mNumOfEggs
            End Get
            Set(ByVal Value As Int32)
                mNumOfEggs = Value
            End Set
        End Property
        Private mNumOfEggs As Int32
    End Class
End Namespace
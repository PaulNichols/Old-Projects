Namespace Application.Search
    <Serializable()> _
    Public Class ApplicationSearchCriteriaDetailed
        Inherits ApplicationSearchCriteriaDetailed_Customer
        Implements IApplicationSearchCommon

        Public Sub New()
        End Sub

        Public Enum PriorityList
            Either
            High
            Normal
        End Enum
        Public Enum SNStatusList
            Either
            Active
            Inactive
        End Enum

        Public Property ExpiryDate() As DateRange
            Get
                Return mExpiryDate
            End Get
            Set(ByVal Value As DateRange)
                mExpiryDate = Value
            End Set
        End Property
        Private mExpiryDate As DateRange

        Public Property LoggedById() As Int32
            Get
                Return mLoggedById
            End Get
            Set(ByVal Value As Int32)
                mLoggedById = Value
            End Set
        End Property
        Private mLoggedById As Int32

        Public Property IssuedById() As Int32
            Get
                Return mIssuedLoggedById
            End Get
            Set(ByVal Value As Int32)
                mIssuedLoggedById = Value
            End Set
        End Property
        Private mIssuedLoggedById As Int32

        Public Property Payment() As Application.ProgressStatus.BOProgressStatusPayment
            Get
                Return mPayment
            End Get
            Set(ByVal Value As Application.ProgressStatus.BOProgressStatusPayment)
                mPayment = Value
            End Set
        End Property
        Private mPayment As Application.ProgressStatus.BOProgressStatusPayment

        Public Property Priority() As PriorityList
            Get
                Return mPriority
            End Get
            Set(ByVal Value As PriorityList)
                mPriority = Value
            End Set
        End Property
        Private mPriority As PriorityList

        Public Property OwnerId() As Int32
            Get
                Return mOwnerId
            End Get
            Set(ByVal Value As Int32)
                mOwnerId = Value
            End Set
        End Property
        Private mOwnerId As Int32

        Public Property DateSAAdviceReturnedToGWD() As DateRange
            Get
                Return mDateSAAdviceReturnedToGWD
            End Get
            Set(ByVal Value As DateRange)
                mDateSAAdviceReturnedToGWD = Value
            End Set
        End Property
        Private mDateSAAdviceReturnedToGWD As DateRange

        Public Property DateInspectorateReturnedToGWD() As DateRange
            Get
                Return mDateInspectorateReturnedToGWD
            End Get
            Set(ByVal Value As DateRange)
                mDateInspectorateReturnedToGWD = Value
            End Set
        End Property
        Private mDateInspectorateReturnedToGWD As DateRange

        Public Property PartyAddressIds() As Int32()
            Get
                Return mPartyAddressIds
            End Get
            Set(Byval Value As Int32())
                mPartyAddressIds = Value
            End Set
        End Property
        Private mPartyAddressIds As Int32()

        Public Property LiveWildTakenAddressIds() As Int32()
            Get
                Return mLiveWildTakenAddressIds
            End Get
            Set(ByVal Value As Int32())
                mLiveWildTakenAddressIds = Value
            End Set
        End Property
        Private mLiveWildTakenAddressIds As Int32()

        Public Property MailingAddressIds() As Int32()
            Get
                Return mMailingAddressIds
            End Get
            Set(ByVal Value As Int32())
                mMailingAddressIds = Value
            End Set
        End Property
        Private mMailingAddressIds As Int32()

        Public Property ScientificAdvice() As ReferenceData.BOScientificAdvice
            Get
                Return mScientificAdvice
            End Get
            Set(ByVal Value As ReferenceData.BOScientificAdvice)
                mScientificAdvice = Value
            End Set
        End Property
        Private mScientificAdvice As ReferenceData.BOScientificAdvice

        Public Property AdHocScientificAdvice() As Logical
            Get
                Return mAdHocScientificAdvice
            End Get
            Set(ByVal Value As Logical)
                mAdHocScientificAdvice = Value
            End Set
        End Property
        Private mAdHocScientificAdvice As Logical

        Public Property SpecialCondition() As ReferenceData.BOSpecialCondition
            Get
                Return mSpecialCondition
            End Get
            Set(ByVal Value As ReferenceData.BOSpecialCondition)
                mSpecialCondition = Value
            End Set
        End Property
        Private mSpecialCondition As ReferenceData.BOSpecialCondition

        Public Property AdHocSpecialCondition() As Logical
            Get
                Return mAdHocSpecialCondition
            End Get
            Set(ByVal Value As Logical)
                mAdHocSpecialCondition = Value
            End Set
        End Property
        Private mAdHocSpecialCondition As Logical

        Public Property SNStatus() As SNStatusList
            Get
                Return mSNStatus
            End Get
            Set(Byval Value As SNStatusList)
                mSNStatus = Value
            End Set
        End Property
        Private mSNStatus As SNStatusList

#Region " IApplicationSearchCommon "
        Public Property DateApplicationReceived() As DateRange Implements IApplicationSearchCommon.DateApplicationReceived
            Get
                Return mDateApplicationReceived
            End Get
            Set(ByVal Value As DateRange)
                mDateApplicationReceived = Value
            End Set
        End Property
        Private mDateApplicationReceived As DateRange

        Public Property DateLogged() As DateRange Implements IApplicationSearchCommon.DateLogged
            Get
                Return mDateLogged
            End Get
            Set(ByVal Value As DateRange)
                mDateLogged = Value
            End Set
        End Property
        Private mDateLogged As DateRange

        Public Property Status() As Int32() Implements IApplicationSearchCommon.Status
            Get
                Return mStatus
            End Get
            Set(ByVal Value As Int32())
                mStatus = Value
            End Set
        End Property
        Private mStatus As Int32()

        Public Property AssignedToRole() As Int32() Implements IApplicationSearchCommon.AssignedToRole
            Get
                Return mAssignedToRole
            End Get
            Set(ByVal Value As Int32())
                mAssignedToRole = Value
            End Set
        End Property
        Private mAssignedToRole As Int32()

        Public Property DateOfReferral() As DateRange Implements IApplicationSearchCommon.DateOfReferral
            Get
                Return mDateOfReferral
            End Get
            Set(ByVal Value As DateRange)
                mDateOfReferral = Value
            End Set
        End Property
        Private mDateOfReferral As DateRange

        Public Property SAAdvice() As Application.ProgressStatus.BOProgressStatusSAAdvice Implements IApplicationSearchCommon.SAAdvice
            Get
                Return mSAAdvice
            End Get
            Set(ByVal Value As Application.ProgressStatus.BOProgressStatusSAAdvice)
                mSAAdvice = Value
            End Set
        End Property
        Private mSAAdvice As Application.ProgressStatus.BOProgressStatusSAAdvice

        Public Property InspectorateAdvice() As Application.ProgressStatus.BOProgressStatusInspection Implements IApplicationSearchCommon.InspectorateAdvice
            Get
                Return mInspectorateAdvice
            End Get
            Set(ByVal Value As Application.ProgressStatus.BOProgressStatusInspection)
                mInspectorateAdvice = Value
            End Set
        End Property
        Private mInspectorateAdvice As Application.ProgressStatus.BOProgressStatusInspection

        Public Property PartyId() As Object Implements IApplicationSearchCommon.PartyId
            Get
                Return mPartyId
            End Get
            Set(ByVal Value As Object)
                mPartyId = Value
            End Set
        End Property
        Private mPartyId As Object

        Public Property PartyName() As String Implements IApplicationSearchCommon.PartyName
            Get
                Return mPartyName
            End Get
            Set(ByVal Value As String)
                mPartyName = Value
            End Set
        End Property
        Private mPartyName As String

        Public Property PartyTypeInApplication() As ApplicationSearchCriteriaCommon.PartyTypes Implements IApplicationSearchCommon.PartyTypeInApplication
            Get
                Return mPartyTypeInApplication
            End Get
            Set(ByVal Value As ApplicationSearchCriteriaCommon.PartyTypes)
                mPartyTypeInApplication = Value
            End Set
        End Property
        Private mPartyTypeInApplication As ApplicationSearchCriteriaCommon.PartyTypes

        Public Property AddressTypeInApplication() As ApplicationSearchCriteriaCommon.AddressTypes Implements IApplicationSearchCommon.AddressTypeInApplication
            Get
                Return mAddressTypeInApplication
            End Get
            Set(ByVal Value As ApplicationSearchCriteriaCommon.AddressTypes)
                mAddressTypeInApplication = Value
            End Set
        End Property
        Private mAddressTypeInApplication As ApplicationSearchCriteriaCommon.AddressTypes
#End Region

    End Class
End Namespace

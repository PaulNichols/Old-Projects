Namespace Application.Search
    <Serializable()> _
    Public Class ApplicationSearchCriteriaCommon
        Inherits ApplicationSearchCriteriaCommon_Customer
        Implements IApplicationSearchCommon


        Public Enum PartyTypes
            Unknown
            Agent
            Exporter
            Holder
            Keeper
            Importer
        End Enum

        Public Enum AddressTypes
            Applicant_Address
            LWT_Address
            Mailing_Address
            Management_Authority_Address
        End Enum

        Public Sub New()
        End Sub

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

        Public Property PartyTypeInApplication() As PartyTypes Implements IApplicationSearchCommon.PartyTypeInApplication
            Get
                Return mPartyTypeInApplication
            End Get
            Set(ByVal Value As PartyTypes)
                mPartyTypeInApplication = Value
            End Set
        End Property
        Private mPartyTypeInApplication As PartyTypes

        Public Property AddressTypeInApplication() As ApplicationSearchCriteriaCommon.AddressTypes Implements IApplicationSearchCommon.AddressTypeInApplication
            Get
                Return mAddressTypeInApplication
            End Get
            Set(ByVal Value As ApplicationSearchCriteriaCommon.AddressTypes)
                mAddressTypeInApplication = Value
            End Set
        End Property
        Private mAddressTypeInApplication As ApplicationSearchCriteriaCommon.AddressTypes
    End Class
End Namespace

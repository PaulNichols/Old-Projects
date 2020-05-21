Namespace Application.Search
    Public Interface IApplicationSearchCommon
        Inherits IApplicationSearchCommon_Customer

        Property DateApplicationReceived() As DateRange
        Property DateLogged() As DateRange
        Property Status() As Int32()
        Property AssignedToRole() As Int32()
        Property DateOfReferral() As DateRange
        Property SAAdvice() As Application.ProgressStatus.BOProgressStatusSAAdvice
        Property InspectorateAdvice() As Application.ProgressStatus.BOProgressStatusInspection
        Property PartyId() As Object
        Property PartyName() As String
        Property PartyTypeInApplication() As ApplicationSearchCriteriaCommon.PartyTypes
        Property AddressTypeInApplication() As ApplicationSearchCriteriaCommon.AddressTypes
    End Interface
End Namespace

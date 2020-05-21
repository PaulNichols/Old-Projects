Namespace Application
    Public Interface IBOPermitInfo
        Property PermitId() As Int32

        Property ProgressStatusPayment() As Application.ProgressStatus.BOProgressStatusPayment
        Property ProgressStatusInspection() As Application.ProgressStatus.BOProgressStatusInspection
        Property ProgressStatusReferralHistory() As Application.ProgressStatus.BOProgressStatusReferralHistory
        Property ProgressStatusReIssued() As Application.ProgressStatus.BOProgressStatusReIssued
        Property ProgressStatusSAAdvice() As Application.ProgressStatus.BOProgressStatusSAAdvice
        Property PermitStatus() As ReferenceData.BOPermitStatus
        Property AssignedTo() As ReferenceData.BOStatusAssignedToGroup
        Property DateRefused() As Object
        Property PermitInfoId() As Int32
        Property PrintJobId() As Object
        Property CoveringLetterReportId() As Object

        Property NextActionDate() As Object
        Property CancelReason() As Object
        Property CancelPendingReason() As Object
        Property CancelPendingDeclineReason() As Object
        Property SequenceNumber() As Int32

        Property SemiCompleteSpecimenId() As Object
        Property SemiCompleteSpecieId() As Object
        Property SemiCompleteSecondPartyId() As Object
        Property SemiCompleteCountryId() As Object
        Property SemicompleteUOMId() As Object
    End Interface

        'Public Interface IBOPermitInfoClock
        '    Property GWDClock() As Int32
        '    Property JNCCClock() As Int32
        '    Property KewClock() As Int32
        '    Property InspectorateClock() As Int32

        '    Property GWDClockStartDate() As Object
        '    Property JNCCClockStartDate() As Object
        '    Property KewClockStartDate() As Object
        '    Property InspectorateClockStartDate() As Object
        'End Interface
End Namespace

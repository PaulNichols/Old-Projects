Namespace Application
    Public Interface IBOPermit
        Property PermitId() As Int32
        Property Description() As Object
        Property NumberOfCopies() As Object
        Property Specie() As BOSpecie
        Property CountryOfOrigin() As ReferenceData.BOCountry
        Property CountryOfOriginPermitDate() As Object
        Property CountryOfOriginPermitNumber() As String
        Property ApplicationId() As Int32
        Property PermitDate() As Date
        Property ExpiryDate() As Object
        Property Specimens() As BOSpecimen()
        Property PermitNumber() As Int32
        Property ApplicationPermitNumber() As String
        '  Function Clone() As BOPermit
        Property ProgressStatusPayment() As Application.ProgressStatus.BOProgressStatusPayment
        Property ProgressStatusInspection() As Application.ProgressStatus.BOProgressStatusInspection
        Property ProgressStatusReferralHistory() As Application.ProgressStatus.BOProgressStatusReferralHistory
        Property ProgressStatusReIssued() As Application.ProgressStatus.BOProgressStatusReIssued
        Property ProgressStatusSAAdvice() As Application.ProgressStatus.BOProgressStatusSAAdvice
        Property PermitStatus() As ReferenceData.BOPermitStatus
        Property ReReferredCount() As Int32
        Property InputtedByCustomer() As Boolean
        Property CreatedById() As Int64

        Property JNCCAdvice() As String
        Property KewAdvice() As String
    End Interface
End Namespace

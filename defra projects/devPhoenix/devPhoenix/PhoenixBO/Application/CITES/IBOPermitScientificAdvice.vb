Namespace Application.CITES.Applications
    Public Interface IBOPermitScientificAdvice

        Property PermitScientificAdviceId() As Int32
        Property DateOfAdvice() As Date
        Property DateOfAdviceGrid() As String
        Property ScientificAdviceId() As Int32
        Property ScientificAdvice() As ReferenceData.BOScientificAdvice
        Property PermitId() As Int32
        Property SSOUserId() As Decimal
        Property SpecificAdvice() As String
        Property Current() As Boolean
        Property PermitScientificAdviceCheckSum() As Int32
        Property User() As String
        Property ShortAdviceText() As String

    End Interface
End Namespace
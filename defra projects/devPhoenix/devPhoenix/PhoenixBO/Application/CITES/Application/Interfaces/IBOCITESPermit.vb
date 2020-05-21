Namespace Application.CITES
    Public Interface IBOCITESPermit
        Property CITESPermitId() As Int32

        Property Derivative() As ReferenceData.BOCITESDerivative
        Property Purpose() As ReferenceData.BOCITESPurpose
        Property Source1() As ReferenceData.BOCITESSource
        Property Source2() As ReferenceData.BOCITESSource

        Property AuthorityLocation() As ReferenceData.BOCountry
        Property UnderDerogation() As Boolean
        Property IsRetrospective() As Boolean

        Property CITESPermitChecksum() As Int32
        Property DelegationOfAuthorityGuidline() As BO.ReferenceData.BODelegationGuideLine

        'Function GetPastPermit() As Object
    End Interface
End Namespace
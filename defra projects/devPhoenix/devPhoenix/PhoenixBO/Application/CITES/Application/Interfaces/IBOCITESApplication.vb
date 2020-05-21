Namespace Application.CITES.Applications

    Public Interface IBOCITESApplication
        Property SecondParty() As BO.Application.BOApplicationPartyDetails
        Property ManagementAuthority() As BO.Application.BOApplicationPartyDetails
        Property ForeignManagementAuthority() As BO.Application.BOApplicationPartyDetails
        Property Consignment() As Boolean
        'Property ApplicationType() As Application.CITES.Applications.CITESApplicationTypeEnum
        Property AdditionalDeclaration() As Application.BOAdditionalDeclaration
        Property IsComposite() As Boolean

        ' Function Clone() As BOCITESApplication
        Property CITESChecksum() As Int32
        Property CITESApplicationId() As Int32

        ReadOnly Property IsImport() As Boolean
        ReadOnly Property IsExport() As Boolean
        ReadOnly Property IsArticle10() As Boolean
        ReadOnly Property IsArticle30() As Boolean
        ReadOnly Property IsReExport() As Boolean
        Property LocationAddress() As BO.Party.BOReadOnlyAddress

        Property CountryOfImport() As BO.ReferenceData.BOCountry

        'Overloads Function Validate(ByVal writeFlag As Boolean, ByVal ignoreWarnings As Boolean) As ValidationManager
    End Interface
End Namespace

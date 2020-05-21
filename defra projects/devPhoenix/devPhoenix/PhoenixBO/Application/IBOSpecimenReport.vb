Namespace Application
    Public Interface IBOSpecimenReport

        Property SpecimenReportId() As Int32
        Property SpecimenId() As Int32
        Property PermitId() As Int32
        Property OriginId() As Int32
        Property SeizedByCustoms() As Boolean
        Property ImportNumber() As Object
        Property Importer() As Object
        Property ExportNumber() As Object
        Property CountryOfOriginId() As Object
        Property BreedingFactilities() As String
        Property SpecimenReportDate() As Date
        Property OtherCITESSourceExplanation() As String
        Property BreederNameAddress() As String
        Property CaptivebredGeneration() As String
        Property OtherInformation() As String
        Property WildTakenDate() As Object
        Property DOENumber() As Object
        Property WildTakenDisabilityDetails() As String
        Property BreedingTechniqueDetails() As String
        Property FounderBreedingStockDetails() As String
        Property EstablishmentBredtoF2Details() As String
        Property BreedingwithoutWildAugmentationDetails() As String
        Property BreedingStockSizeDetails() As String
        Property ImportExportPurposeDetails() As String
        Property SpeciesConservationDetails() As String
        Property DeclarationAcknowledged() As Boolean
        Property Role() As String
        Property StudBookNumber() As String
        Property PresentSpecAddress() As String
        Property SourceUnknownExplanantion() As String
        Property ScientificName() As String
        Property CommonName() As String
        Property Sex() As String
        Property Markings() As String
        Property HatchDate() As String
        Property HatchDateExact() As String
        Property AimsObjectives() As String
        Property HowObjectivesAchieved() As String
        Property OtherSpecimensInvolved() As String
        Property BreedingSuccess() As String
    End Interface
End Namespace
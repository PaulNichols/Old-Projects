Namespace Application.CITES
    Public Interface ICareAccommodation

        Property AccommodationAndCareId() As Int32

        Property PermitId() As Int32

        Property EstablishmentDescription() As Object

        Property Enclosures() As Object

        Property SpecimensPerEnclosure() As String

        Property EnclosureFurnishing() As Object

        Property FoodProvisions() As Object

        Property VeterinaryProvisions() As Object

        Property QuarantineApproved() As Boolean

        Property LicensedZoo() As Boolean

        Property LicensedPetShop() As Boolean

        Property DangerousWildAnimalLicenceHeld() As Boolean

        Property DangerousWildAnimalLicenceNumber() As Object

        Property BALAIDeirectiveLicenceHeld() As Boolean

        Property BALAIDeirectiveLicenceNumber() As Object

        Property OtherInformation() As Object

        Property DeclarationAcknowledgement() As Boolean

        Property ReceiptDate() As Object

        Property EntryDate() As Object

        Property PremisesDetails() As String

        Property SpecieId() As Int32

        Property ApplicantName() As String

        Property DeliveryAddress() As String

        Property ScientificName() As String

        Property CommonName() As String

        Property TotalNumberOfSpecimens() As Object

        Property ApplicantId() As Object

        Property Applicationid() As String
    End Interface
End Namespace
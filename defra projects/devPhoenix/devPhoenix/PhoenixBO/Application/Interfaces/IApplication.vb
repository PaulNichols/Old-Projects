Namespace Application
    <Serializable()> _
    Public Enum PermitTypes
        Standard = 1
        Multiple
        Consignment
    End Enum

    <Serializable()> _
    Public Enum PaymentStatusTypes
        Unknown
        Unpaid
        Paid
        Payment_Pending
    End Enum

    <Serializable()> _
    Public Enum ApplicationTypes
        Unknown = 0
        BirdAdd = 1
        BirdDup = 2
        BirdFate = 3
        BirdAdult = 4
        BirdChick = 5
        BirdTrans = 6
        Export = 7
        Article10 = 8
        Article30 = 9
        CitesDup = 10
        Import = 11
        ImpNotif = 12
        Seizure = 13
    End Enum

    Public Interface IApplication
        Property ApplicationId() As Int32
        Property Created() As Stamp
        Property DateOfApplication() As Date
        Property IsSemiComplete() As Boolean
        Property PaymentStatus() As PaymentStatusTypes
        Property Party() As BOApplicationPartyDetails
        Property Agent() As BOApplicationPartyDetails
        Property Retrospective() As Boolean
        Property IsSubmitted() As Boolean
        Property IsInComplete() As Boolean
        Property ApplicationMethod() As ReferenceData.BOApplicationMethod
        'Property PermitType() As PermitTypes
        Property PermitTypeId() As Int32
        Property Permit() As BOPermit()
        Property Validated() As Boolean
        Property ApplicationTypeId() As Int32   'MLD changed 16/12/4
        Property ApplicationType() As String
        Property ReceivedDate() As Date         'MLD 1/2/5 changed to Date, and spelling fixed
        'Property CaseOfficer() As String
        'Property Customer() As String
        Property PaymentBasketId() As Object
        Property StandardFee() As Decimal
        Property FeeCharged() As Decimal
        Property AllowSemiComplete() As Boolean     'MLD added 7/10/4
        Property PaidDate() As Object
        Property OwnerId() As Int64

        Function CalculateFee() As Decimal
        Overloads Function Validate(ByVal writeFlag As Boolean, ByVal ignoreWarnings As Boolean) As BaseBO
        '   Function Clone() As BOApplication
        Function Submit() As Boolean
        Function GetStatus() As Object
    End Interface
End Namespace
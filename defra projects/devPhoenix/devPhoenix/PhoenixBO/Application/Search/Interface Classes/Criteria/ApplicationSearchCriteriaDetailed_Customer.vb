Namespace Application.Search
    <Serializable()> _
    Public Class ApplicationSearchCriteriaDetailed_Customer
        Inherits ApplicationSearchCriteriaBase
        Implements IApplicationSearchCommon_Customer, ISearchApplicationId

        Public Enum SubmittedByWho
            Either
            Customer
            Case_Officer
        End Enum

        Public Enum DocumentTypeList
            'Any = 0 -- Not required now it's a mutiple selection list
            Article_10 = 1
            Import_Application = 2
            Export_Application = 3
            Re_Export_Application = 4
            Import_Notification = 5
            Seizure_Notification = 6
            Article_30 = 7
        End Enum

        Public Enum Logical
            Either
            Yes
            No
        End Enum

        Public Enum Gender
            Either
            Male
            Female
        End Enum

        Public Sub New()
        End Sub

        Public Property DateReturned() As DateRange
            Get
                Return mDateReturned
            End Get
            Set(ByVal Value As DateRange)
                mDateReturned = Value
            End Set
        End Property
        Private mDateReturned As DateRange

        Public Property DateUsed() As DateRange
            Get
                Return mDateUsed
            End Get
            Set(ByVal Value As DateRange)
                mDateUsed = Value
            End Set
        End Property
        Private mDateUsed As DateRange

        Public Property DateComplete() As DateRange
            Get
                Return mDateComplete
            End Get
            Set(ByVal Value As DateRange)
                mDateComplete = Value
            End Set
        End Property
        Private mDateComplete As DateRange

        Public Property DateAuthorised() As DateRange
            Get
                Return mDateAuthorised
            End Get
            Set(ByVal Value As DateRange)
                mDateAuthorised = Value
            End Set
        End Property
        Private mDateAuthorised As DateRange

        Public Property DateRefused() As DateRange
            Get
                Return mDateRefused
            End Get
            Set(ByVal Value As DateRange)
                mDateRefused = Value
            End Set
        End Property
        Private mDateRefused As DateRange

        Public Property DateCancelled() As DateRange
            Get
                Return mDateCancelled
            End Get
            Set(ByVal Value As DateRange)
                mDateCancelled = Value
            End Set
        End Property
        Private mDateCancelled As DateRange

        Public Property SubmittedBy() As SubmittedByWho
            Get
                Return mSubmittedBy
            End Get
            Set(ByVal Value As SubmittedByWho)
                mSubmittedBy = Value
            End Set
        End Property
        Private mSubmittedBy As SubmittedByWho

        Public Property DateReIssued() As DateRange
            Get
                Return mDateReIssued
            End Get
            Set(ByVal Value As DateRange)
                mDateReIssued = Value
            End Set
        End Property
        Private mDateReIssued As DateRange

        Public Property DateFeePaid() As DateRange
            Get
                Return mDateFeePaid
            End Get
            Set(ByVal Value As DateRange)
                mDateFeePaid = Value
            End Set
        End Property
        Private mDateFeePaid As DateRange

        Public Property ManagementAuthorityAddressIds() As Int32()
            Get
                Return mManagementAuthorityAddressIds
            End Get
            Set(ByVal Value As Int32())
                mManagementAuthorityAddressIds = Value
            End Set
        End Property
        Private mManagementAuthorityAddressIds As Int32()

        Public Property ApplicationId() As Int32 Implements ISearchApplicationId.ApplicationId
            Get
                Return mApplicationId
            End Get
            Set(ByVal Value As Int32)
                mApplicationId = Value
            End Set
        End Property
        Private mApplicationId As Int32

        Public Property DocumentType() As DocumentTypeList()
            Get
                Return mDocumentType
            End Get
            Set(ByVal Value As DocumentTypeList())
                mDocumentType = Value
            End Set
        End Property
        Private mDocumentType As DocumentTypeList()

        Public Property Source() As Int32()
            Get
                Return mSource
            End Get
            Set(ByVal Value As Int32())
                mSource = Value
            End Set
        End Property
        Private mSource As Int32()

        Public Property Purpose() As Int32()
            Get
                Return mPurpose
            End Get
            Set(ByVal Value As Int32())
                mPurpose = Value
            End Set
        End Property
        Private mPurpose As Int32()

        Public Property PartDerivative() As Int32()
            Get
                Return mPartDerivative
            End Get
            Set(ByVal Value As Int32())
                mPartDerivative = Value
            End Set
        End Property
        Private mPartDerivative As Int32()

        Public Property UOM() As BO.BOUnitOfMeasurement
            Get
                Return mUOM
            End Get
            Set(ByVal Value As BO.BOUnitOfMeasurement)
                mUOM = Value
            End Set
        End Property
        Private mUOM As BO.BOUnitOfMeasurement

        Public Property NetMass() As NumberRange
            Get
                Return mNetMass
            End Get
            Set(ByVal Value As NumberRange)
                mNetMass = Value
            End Set
        End Property
        Private mNetMass As NumberRange

        Public Property Quantity() As NumberRange
            Get
                Return mQuantity
            End Get
            Set(ByVal Value As NumberRange)
                mQuantity = Value
            End Set
        End Property
        Private mQuantity As NumberRange

        Public Property NetMassUsed() As NumberRange
            Get
                Return mNetMassUsed
            End Get
            Set(ByVal Value As NumberRange)
                mNetMassUsed = Value
            End Set
        End Property
        Private mNetMassUsed As NumberRange

        Public Property QuantityUsed() As NumberRange
            Get
                Return mQuantityUsed
            End Get
            Set(ByVal Value As NumberRange)
                mQuantityUsed = Value
            End Set
        End Property
        Private mQuantityUsed As NumberRange

        Public Property LinkedToSeizureNotification() As Logical
            Get
                Return mLinkedToSeizureNotification
            End Get
            Set(ByVal Value As Logical)
                mLinkedToSeizureNotification = Value
            End Set
        End Property
        Private mLinkedToSeizureNotification As Logical

        Public Property SeizureNotificationId() As Int32
            Get
                Return mSeizureNotificationId
            End Get
            Set(ByVal Value As Int32)
                mSeizureNotificationId = Value
            End Set
        End Property
        Private mSeizureNotificationId As Int32

        Public Property DelegatedAuthority() As BO.ReferenceData.BODelegationGuideLine
            Get
                Return mDelegatedAuthority
            End Get
            Set(ByVal Value As BO.ReferenceData.BODelegationGuideLine)
                mDelegatedAuthority = Value
            End Set
        End Property
        Private mDelegatedAuthority As BO.ReferenceData.BODelegationGuideLine

        Public Property Derogation() As Logical
            Get
                Return mDerogation
            End Get
            Set(ByVal Value As Logical)
                mDerogation = Value
            End Set
        End Property
        Private mDerogation As Logical

        Public Property CertificateFate() As ReferenceData.BOSpecimenFate
            Get
                Return mCertificateFate
            End Get
            Set(ByVal Value As ReferenceData.BOSpecimenFate)
                mCertificateFate = Value
            End Set
        End Property
        Private mCertificateFate As ReferenceData.BOSpecimenFate

        Public Property BillOfLading() As String
            Get
                Return mBillOfLading
            End Get
            Set(ByVal Value As String)
                mBillOfLading = Value
            End Set
        End Property
        Private mBillOfLading As String

        Public Property PermitCertificateNotificationNumber() As String
            Get
                Return mPermitCertificateNotificationNumber
            End Get
            Set(ByVal Value As String)
                mPermitCertificateNotificationNumber = Value
            End Set
        End Property
        Private mPermitCertificateNotificationNumber As String

        Public Property DateSeized() As DateRange
            Get
                Return mDateSeized
            End Get
            Set(ByVal Value As DateRange)
                mDateSeized = Value
            End Set
        End Property
        Private mDateSeized As DateRange

        Public Property PortOfEntry() As ReferenceData.BOPortofEntry
            Get
                Return mPortOfEntry
            End Get
            Set(ByVal Value As ReferenceData.BOPortofEntry)
                mPortOfEntry = Value
            End Set
        End Property
        Private mPortOfEntry As ReferenceData.BOPortofEntry

        Public Property CountryOfExport() As ReferenceData.BOCountry
            Get
                Return mCountryOfExport
            End Get
            Set(ByVal Value As ReferenceData.BOCountry)
                mCountryOfExport = Value
            End Set
        End Property
        Private mCountryOfExport As ReferenceData.BOCountry

        Public Property CountryOfImport() As ReferenceData.BOCountry
            Get
                Return mCountryOfImport
            End Get
            Set(ByVal Value As ReferenceData.BOCountry)
                mCountryOfImport = Value
            End Set
        End Property
        Private mCountryOfImport As ReferenceData.BOCountry

        Public Property OtherCountry() As ReferenceData.BOCountry
            Get
                Return mOtherCountry
            End Get
            Set(ByVal Value As ReferenceData.BOCountry)
                mOtherCountry = Value
            End Set
        End Property
        Private mOtherCountry As ReferenceData.BOCountry

        Public Property CountryOfOrigin() As ReferenceData.BOCountry
            Get
                Return mCountryOfOrigin
            End Get
            Set(ByVal Value As ReferenceData.BOCountry)
                mCountryOfOrigin = Value
            End Set
        End Property
        Private mCountryOfOrigin As ReferenceData.BOCountry

        Public Property CountryOfOriginPermitNumber() As Int32
            Get
                Return mCountryOfOriginPermitNumber
            End Get
            Set(ByVal Value As Int32)
                mCountryOfOriginPermitNumber = Value
            End Set
        End Property
        Private mCountryOfOriginPermitNumber As Int32

        Public Property CountryOfOriginIssueDate() As DateRange
            Get
                Return mCountryOfOriginIssueDate
            End Get
            Set(ByVal Value As DateRange)
                mCountryOfOriginIssueDate = Value
            End Set
        End Property
        Private mCountryOfOriginIssueDate As DateRange

        Public Property CountryOfLastReExport() As ReferenceData.BOCountry
            Get
                Return mCountryOfLastReExport
            End Get
            Set(ByVal Value As ReferenceData.BOCountry)
                mCountryOfLastReExport = Value
            End Set
        End Property
        Private mCountryOfLastReExport As ReferenceData.BOCountry

        Public Property CountryOfLastReExportPermitNumber() As Int32
            Get
                Return mCountryOfLastReExportPermitNumber
            End Get
            Set(ByVal Value As Int32)
                mCountryOfLastReExportPermitNumber = Value
            End Set
        End Property
        Private mCountryOfLastReExportPermitNumber As Int32

        Public Property CountryOfLastReExportPermitIssueDate() As DateRange
            Get
                Return mCountryOfLastReExportPermitIssueDate
            End Get
            Set(ByVal Value As DateRange)
                mCountryOfLastReExportPermitIssueDate = Value
            End Set
        End Property
        Private mCountryOfLastReExportPermitIssueDate As DateRange

        Public Property ManagementAuthorityCountry() As ReferenceData.BOCountry
            Get
                Return mManagementAuthorityCountry
            End Get
            Set(ByVal Value As ReferenceData.BOCountry)
                mManagementAuthorityCountry = Value
            End Set
        End Property
        Private mManagementAuthorityCountry As ReferenceData.BOCountry

        Public Property MemberStateOfImportPermitNumber() As String
            Get
                Return mMemberStateOfImportPermitNumber
            End Get
            Set(ByVal Value As String)
                mMemberStateOfImportPermitNumber = Value
            End Set
        End Property
        Private mMemberStateOfImportPermitNumber As String

        Public Property MemberStateOfImportPermitIssueDate() As DateRange
            Get
                Return mMemberStateOfImportPermitIssueDate
            End Get
            Set(ByVal Value As DateRange)
                mMemberStateOfImportPermitIssueDate = Value
            End Set
        End Property
        Private mMemberStateOfImportPermitIssueDate As DateRange

        Public Property ScientificName() As String
            Get
                Return mScientificName
            End Get
            Set(ByVal Value As String)
                mScientificName = Value
            End Set
        End Property
        Private mScientificName As String

        Public Property CommonName() As String
            Get
                Return mCommonName
            End Get
            Set(ByVal Value As String)
                mCommonName = Value
            End Set
        End Property
        Private mCommonName As String

        Public Property AppliedForName() As String
            Get
                Return mAppliedForName
            End Get
            Set(ByVal Value As String)
                mAppliedForName = Value
            End Set
        End Property
        Private mAppliedForName As String

        Public Property ECAnnex() As String
            Get
                Return mECAnnex
            End Get
            Set(ByVal Value As String)
                mECAnnex = Value
            End Set
        End Property
        Private mECAnnex As String

        Public Property CITESAppendix() As String
            Get
                Return mCITESAppendix
            End Get
            Set(ByVal Value As String)
                mCITESAppendix = Value
            End Set
        End Property
        Private mCITESAppendix As String

        Public Property SpecimenQuantity() As Int32
            Get
                Return mSpecimenQuantity
            End Get
            Set(ByVal Value As Int32)
                mSpecimenQuantity = Value
            End Set
        End Property
        Private mSpecimenQuantity As Int32

        Public Property SpecimenNetMass() As Decimal
            Get
                Return mSpecimenNetMass
            End Get
            Set(ByVal Value As Decimal)
                mSpecimenNetMass = Value
            End Set
        End Property
        Private mSpecimenNetMass As Decimal

        Public Property SpecimenGender() As Gender
            Get
                Return mSpecimenGender
            End Get
            Set(ByVal Value As Gender)
                mSpecimenGender = Value
            End Set
        End Property
        Private mSpecimenGender As Gender

        'Public Property SpecimenIDMarkType() As ReferenceData.BOIDMarkType
        '    Get
        '        Return mSpecimenIDMarkType
        '    End Get
        '    Set(ByVal Value As ReferenceData.BOIDMarkType)
        '        mSpecimenIDMarkType = Value
        '    End Set
        'End Property
        'Private mSpecimenIDMarkType As ReferenceData.BOIDMarkType

        Public Property HatchDate() As DateRange
            Get
                Return mHatchDate
            End Get
            Set(ByVal Value As DateRange)
                mHatchDate = Value
            End Set
        End Property
        Private mHatchDate As DateRange

        Public Property AcquiredDate() As DateRange
            Get
                Return mAcquiredDate
            End Get
            Set(ByVal Value As DateRange)
                mAcquiredDate = Value
            End Set
        End Property
        Private mAcquiredDate As DateRange

        Public Property MovementRestriction() As Logical
            Get
                Return mMovementRestriction
            End Get
            Set(ByVal Value As Logical)
                mMovementRestriction = Value
            End Set
        End Property
        Private mMovementRestriction As Logical

        Public Property Hybrid() As Logical
            Get
                Return mHybrid
            End Get
            Set(ByVal Value As Logical)
                mHybrid = Value
            End Set
        End Property
        Private mHybrid As Logical

#Region " IApplicationSearchCommon_Customer "
        Public Property DateIssued() As DateRange Implements IApplicationSearchCommon_Customer.DateIssued
            Get
                Return mDateIssued
            End Get
            Set(ByVal Value As DateRange)
                mDateIssued = Value
            End Set
        End Property
        Private mDateIssued As DateRange

        Public Property AcceptedScientificName() As String Implements IApplicationSearchCommon_Customer.AcceptedScientificName
            Get
                Return mAcceptedScientificName
            End Get
            Set(ByVal Value As String)
                mAcceptedScientificName = Value
            End Set
        End Property
        Private mAcceptedScientificName As String

        Public Property IDMarkType() As ReferenceData.BOIDMarkType Implements IApplicationSearchCommon_Customer.IDMarkType
            Get
                Return mIDMarkType
            End Get
            Set(ByVal Value As ReferenceData.BOIDMarkType)
                mIDMarkType = Value
            End Set
        End Property
        Private mIDMarkType As ReferenceData.BOIDMarkType

        Public Property IDMarkNumber() As String Implements IApplicationSearchCommon_Customer.IDMarkNumber
            Get
                Return mIDMarkNumber
            End Get
            Set(ByVal Value As String)
                mIDMarkNumber = Value
            End Set
        End Property
        Private mIDMarkNumber As String
#End Region

    End Class
End Namespace

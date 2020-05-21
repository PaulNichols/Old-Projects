Public Class ValidationError
    Inherits BOError

    Public Enum ValidationCodes
        AddressCountMismatch
        MissingPerson
        MissingBusiness
        NoMailingAddress
        MailingAddressNoLongerExists
        NeedOneActiveAddress
        AddressValidationError
        PersonValidationError
        ContactCountMismatch
        NoPrimaryContact
        ContactNoLongerExists
        ContactValidationError
        NeedOneActiveContact
        NeedOneEmailAddress
        ThisPartyDoesNotHaveAssociatedAddress
        NoPartyOrAddress
        TheSelectedPartyHasMismatchedAddress
        NoUnitOfMeasure
        MustHaveAMassIfUOMSelected
        MustHaveAUOMIfMassGreaterThanZero
        MustHaveEitherAQtyOrUOMAndMass
        CannotHaveUOMAndQuantity
        CannotHaveMassAndQuantity
        NoSpecieIdentified
        TooManySpecie
        SpecieMustBeAnnexCOrAnnexD
        ExportPermitNumberMandatoryIfAppendixIII
        CertificateOfOriginMandatoryIfAppendixIII
        ReportUpdateProhibited
        DatabaseErrors
        PreSaveFailed
        OtherIdNotInitialised
        SpecimensCannotBeAnnexD
        SpecimensCannotBeAnnexC
        SpecimensCannotBeAnnexI
        CountryOfImportMustNotBeAnECState
        PartyIsNotAllowedToProcessSemiCompletes
        NonSemiCompleteCountryOfExportNotDefined
        NonSemiCompleteImporterNotDefined
        NonSemiCompleteMassOrQuantityNotDefined
        AnApplicationMustHaveAtLeastOnePermit
        APermitMustHaveAtLeastOneSpecimen
        NonSemiCompleteExporterNotDefined
        SpecimensCannotBeAnnexA
        NotRequiredForSpecimenOnAnnexeCOrD
        PermitLikelyToBeRefused
        ExporterRequired
        ImporterRequired
        UOMDifferBetweenSpecimens
        ScientificNameCannotBeBlank
        CountryOfOriginCannotBeBlank
        CountryOfLastExportCannotBeBlank
        SourceCodeCannotBeBlank
        PurposeCodeCannotBeBlank
        PartDerivativeCannotBeBlank
        AuthorisedLocationCannotBeBlank
        IfLiveBirthDateCannotBeBlank
        IfLiveGenderCannotBeBlank
        HolderRequired
        QuantityMustBeGreaterThanZero
        PreviousArticle10CertificateNumberCannotBeBlank
        PreviousArticle10CertificateIssueDateCannotBeBlank
        MarkIdAndTypeCannotBeBlank
        AcquisitionDateCannotBeBlank
        NumberOfCopiesMustBeOneUnlessCommercialUse
        DeclarationsMustBeAcknowledged
        YouHaveSuppliedTooManySearchWords
        YouHaveSuppliedTooFewSearchWords
        YouMustEnterANumberForFateQty
        YouCannotFateMoreThanPermitQty
        YouMustChooseAFate
        ReferenceNumberUsedBefore
        NoCountryOfOrigin
        NoCountryOfExportNotMarkedUnknown
        NoMemberStateOfImport
        DuplicateReferenceData
        DatabaseUnavailable
        UnexpectedErrorSavingReferenceData
        LinkedCodeMustBeCites
        StandardFeeNoGap
        NoMaximumSet
        MinimumLessThanMaximum
        CountryOfExportMustNotBeAnECState
        CountryOfExportCannotBeGB
        LastLaidDateMustBeEntered
        MustHaveXEggsInAClutch
        MustHaveXRingsForAnEgg
        MustHaveXBirds
        MustHaveXIdMarks
        MustHaveDateAcquiredDate
        MustHaveXMother
        MustHaveXFather
        MustHaveLastLaidDate
        XCannotBeBlank
        IncorrectApplicationType
        MustHaveEggsInAClutch
        MustHaveRingsForAnEgg
        RingOnEggNeedsValue
        RingOnEggNeedsInvalid
        RingOnEggIsUsed
        IfFatedOtherNeedReasonForRing
        IfFatedOtherNeedReasonForIdMark
        GenderOfBirdIsInconsistent
        GenderHasNotBeenPreviouslyConfirmed
        BirdHasBeenFated
        PermanentPossessionInfoRequired
        TheDatesDifferByMoreThanXDays
        MethodsOfTransferNotificationDiffer
        InvalidArticle10
        MustHaveAtLeastOneRingIfNoExtraEggs
        TooManyResults
        GWDContactDetails
        TheEggIsNotASpecimen
        HatchDateEactSettingNotSet
        CommonNameCannotBeBlank
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal id As ValidationCodes)
        MyClass.New()
        mErrorMessage = Nothing
        ValidationID = id
    End Sub

    Public Sub New(ByVal id As ValidationCodes, ByVal extraMessageInfo As Collections.Specialized.NameValueCollection)
        MyClass.New()
        mExtraMessageInfo = extraMessageInfo
        ValidationID = id
    End Sub

    Friend ReadOnly Property ExtraMessageInfo() As Collections.Specialized.NameValueCollection
        Get
            Return mExtraMessageInfo
        End Get
    End Property
    Private mExtraMessageInfo As Collections.Specialized.NameValueCollection

    Public Shadows Property ValidationID() As ValidationCodes
        Get
            Return mID
        End Get
        Set(ByVal Value As ValidationCodes)
            If mID <> Value Then
                mID = Value
                If mErrorMessage Is Nothing OrElse mErrorMessage.Length = 0 Then
                    Dim Info As ValidationManager.Info = ValidationManager.GetInfo(mID)
                    mErrorMessage = Info.Message
                    'do we need to insert some line continuations?
                    mErrorMessage = mErrorMessage.Replace("/n", Environment.NewLine)

                    'now we have the message, do we need to make any replacements?
                    If Not mExtraMessageInfo Is Nothing AndAlso mExtraMessageInfo.Count > 0 Then
                        For Each ExtraInfo As String In mExtraMessageInfo.AllKeys
                            mErrorMessage = mErrorMessage.Replace(ExtraInfo, mExtraMessageInfo(ExtraInfo))
                        Next ExtraInfo
                    End If
                    mStage = Info.Stage
                    mURL = Info.URL
                    mIsWarning = Info.IsWarning
                End If
            End If
        End Set
    End Property
    Protected Shadows mID As ValidationCodes
End Class

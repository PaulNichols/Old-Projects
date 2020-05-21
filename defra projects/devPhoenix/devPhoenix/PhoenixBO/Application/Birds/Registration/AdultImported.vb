Namespace Application.Bird.Registration
    <Serializable()> _
    Public Class AdultImported
        Inherits BaseBird

        Public Sub New()
            ' must have only one specimen so add it...
            'AddSpecimen()
        End Sub

        Public Sub New(ByVal adultImportedBirds As BirdRegistrationDataset.AdultImportedDataTable)
            If Not adultImportedBirds Is Nothing AndAlso _
               adultImportedBirds.Rows.Count > 0 Then

                Dim AdultBirdRow As BirdRegistrationDataset.AdultImportedRow = CType(adultImportedBirds.Rows(0), BirdRegistrationDataset.AdultImportedRow)
                mIsKeeperOfThree = AdultBirdRow.IsKeeperOfThree
                mIsRSPCAInspector = AdultBirdRow.IsKeeperRSPCA
                mIsVet = AdultBirdRow.IsKeeperVet

                For Each ImportedBirdRow As BirdRegistrationDataset.AdultImportedBirdRow In AdultBirdRow.GetAdultImportedBirdRows
                    'create an object to put the items in
                    Dim NewSpecimen As AdultImportedSpecimen = AddSpecimen()
                    'check to see if one is created
                    If Not NewSpecimen Is Nothing Then
                        SpecimenType.CreateSpecimen(CType(NewSpecimen, AdultSpecimenType), ImportedBirdRow.GetImportedSpecimenRows)

                        With NewSpecimen
                            If Not ImportedBirdRow.IsDateAcquiredNull Then .DateAcquired = ImportedBirdRow.DateAcquired
                            If Not ImportedBirdRow.IsDateTakenNull Then .DateTaken = ImportedBirdRow.DateTaken Else .DateTaken = New Date(0)
                            .IsWithinEU = ImportedBirdRow.IsWithinEU
                            If Not ImportedBirdRow.IsKeptAddressNull Then .KeptAddressId = ImportedBirdRow.KeptAddress
                            If Not ImportedBirdRow.IsAcquisitionDetailsNull Then .AcquisitionDetails = ImportedBirdRow.AcquisitionDetails
                            If Not ImportedBirdRow.IsPreviousKeeperNull Then .PreviousKeeper.SetAddress(ImportedBirdRow.PreviousKeeper)
                            If Not ImportedBirdRow.IsImportedDateNull Then .ImportedDate = ImportedBirdRow.ImportedDate
                            If Not ImportedBirdRow.IsPurposeOfImportNull Then .PurposeOfImport = ImportedBirdRow.PurposeOfImport
                            If Not ImportedBirdRow.IsPurposeOfImportExplanationNull Then .PurposeOfImportExplanation = ImportedBirdRow.PurposeOfImportExplanation

                            NewSpecimen.Statements = Statements.CreateStatement(ImportedBirdRow.GetImportedStatementsRows)

                            If Not ImportedBirdRow.IsDateFoundNull Then .DateFound = ImportedBirdRow.DateFound
                            If Not ImportedBirdRow.IsInjuryDetailsNull Then .InjuryDetails = ImportedBirdRow.InjuryDetails Else .InjuryDetails = Nothing

                            .SetAquisitionMethod(ImportedBirdRow.AcquisitionMethod)
                        End With

                        'loop through adding id marks
                        For Each ImportedIdMark As BirdRegistrationDataset.ImportedIDMarksRow In ImportedBirdRow.GetImportedIDMarksRows
                            NewSpecimen.AddIdMark(ImportedIdMark)
                        Next ImportedIdMark

                        'quarantine details
                        For Each QuarantineRow As BirdRegistrationDataset.QuarantineRow In ImportedBirdRow.GetQuarantineRows
                            With QuarantineRow
                                NewSpecimen.KeeperName = .KeeperName
                                NewSpecimen.Address = .Address
                                NewSpecimen.EndDate = .EndDate
                            End With
                            'should only be one row, so bail after
                            Exit For
                        Next QuarantineRow

                        'permit details
                        For Each PermitRow As BirdRegistrationDataset.PermitRow In ImportedBirdRow.GetPermitRows
                            NewSpecimen.Permit = PermitRow.Permit_text
                            'should only be one row, so bail after
                            Exit For
                        Next PermitRow

                        'source details
                        For Each SourceRow As BirdRegistrationDataset.SourceRow In ImportedBirdRow.GetSourceRows
                            With SourceRow
                                If Not .IsIsFromUKNull Then NewSpecimen.IsFromUK = .IsFromUK
                                NewSpecimen.Origin = .Origin
                                NewSpecimen.Origin2 = .Origin2
                                If Not .IsCaptureMethodNull Then
                                    NewSpecimen.CaptureMethod = System.Enum.Parse(GetType(AdultImportedSpecimen.CaptureMethods), .CaptureMethod)
                                End If
                                If Not .IsEULicenseNumberNull Then NewSpecimen.EULicenseNumber = .EULicenseNumber
                                If Not .IsSourceSpecialConditionsNull Then NewSpecimen.SpecialConditions = .SourceSpecialConditions
                                If Not .IsCountryOfOriginNull Then NewSpecimen.CountryOfOrigin = .CountryOfOrigin
                            End With
                            'should only be one row, so bail after
                            Exit For
                        Next SourceRow

                        'loop through adding imported rings
                        For Each ImportedRing As BirdRegistrationDataset.ImportedRingRow In ImportedBirdRow.GetImportedRingRows
                            NewSpecimen.AddRing(ImportedRing)
                        Next ImportedRing

                        SetParentData(CType(adultImportedBirds.DataSet, BirdRegistrationDataset), NewSpecimen)
                    End If
                Next ImportedBirdRow
            End If
        End Sub

         Friend Sub GetData(ByRef adultImportedBirds As BirdRegistrationDataset.AdultImportedDataTable)
            If Not adultImportedBirds Is Nothing Then
                Dim BirdDS As BirdRegistrationDataset = CType(adultImportedBirds.DataSet, BirdRegistrationDataset)
                'update base found data
                Dim AdultImportedRow As BirdRegistrationDataset.AdultImportedRow = adultImportedBirds.NewAdultImportedRow
                AdultImportedRow(adultImportedBirds.AdultImported_IdColumn) = 1
                With AdultImportedRow
                    .IsKeeperOfThree = mIsKeeperOfThree
                    .IsKeeperRSPCA = mIsRSPCAInspector
                    .IsKeeperVet = mIsVet
                End With
                adultImportedBirds.AddAdultImportedRow(AdultImportedRow)

                If Not mSpecimens Is Nothing Then
                    Dim AdultBirdRowId As Int32 = 0
                    For Each OldSpecimen As AdultImportedSpecimen In mSpecimens
                        AdultBirdRowId += 1
                        Dim AdultBirdRow As BirdRegistrationDataset.AdultImportedBirdRow = BirdDS.AdultImportedBird.NewAdultImportedBirdRow()
                        'SCS: 14/4/05 - TR000280.
                        'The column being passed into the adult bird row was incorrect
                        AdultBirdRow(BirdDS.AdultImportedBird.AdultImported_IdColumn) = AdultBirdRowId
                        With AdultBirdRow
                            If OldSpecimen.DateAcquired.Ticks > 0 Then .DateAcquired = OldSpecimen.DateAcquired Else .SetDateAcquiredNull()
                            .IsWithinEU = OldSpecimen.IsWithinEU
                            If Not OldSpecimen.KeptAddressId Is Nothing AndAlso TypeOf OldSpecimen.KeptAddressId Is Int32 Then .KeptAddress = CType(OldSpecimen.KeptAddressId, Int32) Else .SetKeptAddressNull()
                            If Not OldSpecimen.AcquisitionDetails Is Nothing AndAlso TypeOf OldSpecimen.AcquisitionDetails Is String Then .AcquisitionDetails = OldSpecimen.AcquisitionDetails.ToString Else .SetAcquisitionDetailsNull()
                            If OldSpecimen.ImportedDate.Ticks > 0 Then .ImportedDate = OldSpecimen.ImportedDate Else .SetImportedDateNull()
                            If OldSpecimen.PurposeOfImport > 0 Then .PurposeOfImport = OldSpecimen.PurposeOfImport Else .SetPurposeOfImportNull()
                            If Not OldSpecimen.PurposeOfImportExplanation Is Nothing AndAlso OldSpecimen.PurposeOfImportExplanation.Length > 0 Then .PurposeOfImportExplanation = OldSpecimen.PurposeOfImportExplanation Else .SetPurposeOfImportExplanationNull()
                            If OldSpecimen.DateFound.Ticks > 0 Then .DateFound = OldSpecimen.DateFound Else .SetDateFoundNull()
                            If OldSpecimen.DateTaken.Ticks > 0 Then .DateTaken = OldSpecimen.DateTaken Else .SetDateTakenNull()
                            .AcquisitionMethod = OldSpecimen.AcquisitionMethod_Helper
                            If Not OldSpecimen.InjuryDetails Is Nothing Then .InjuryDetails = OldSpecimen.InjuryDetails Else .SetInjuryDetailsNull()
                            .AdultImportedRow = AdultImportedRow
                        End With
                        BirdDS.AdultImportedBird.AddAdultImportedBirdRow(AdultBirdRow)

                        'sort out the specimens
                        If Not OldSpecimen.SpecimenType Is Nothing Then
                            Dim SpecimenRow As BirdRegistrationDataset.ImportedSpecimenRow = BirdDS.ImportedSpecimen.NewImportedSpecimenRow
                            SpecimenRow.AdultImportedBirdRow = AdultBirdRow
                            OldSpecimen.SpecimenType.UpdateSpecimen(SpecimenRow)
                            BirdDS.ImportedSpecimen.Rows.Add(SpecimenRow)
                        End If
                        Dim StatementRow As BirdRegistrationDataset.ImportedStatementsRow = Nothing
                        If Not OldSpecimen.Statements Is Nothing Then
                            StatementRow = BirdDS.ImportedStatements.NewImportedStatementsRow
                            StatementRow.AdultImportedBirdRow = AdultBirdRow
                            OldSpecimen.Statements.UpdateStatement(StatementRow)
                        End If
                        If Not StatementRow Is Nothing Then
                            BirdDS.ImportedStatements.Rows.Add(StatementRow)
                        End If
                        'loop through adding id marks
                        For Each NewMark As IDMark In OldSpecimen.IDMarks  'MLD 19/1/5 enclosing If...End If removed, as no longer necessary
                            Dim IDMarkRow As BirdRegistrationDataset.ImportedIDMarksRow = BirdDS.ImportedIDMarks.NewImportedIDMarksRow
                            IDMarkRow.AdultImportedBirdRow = AdultBirdRow
                            NewMark.PopulateIDMark(CType(IDMarkRow, DataRow))
                            BirdDS.ImportedIDMarks.Rows.Add(IDMarkRow)
                        Next NewMark

                        'quarantine details
                        Dim QuarantineRow As BirdRegistrationDataset.QuarantineRow = BirdDS.Quarantine.NewQuarantineRow
                        With QuarantineRow
                            .AdultImportedBirdRow = AdultBirdRow
                            .KeeperName = OldSpecimen.KeeperName
                            .Address = OldSpecimen.Address
                            .EndDate = OldSpecimen.EndDate
                        End With
                        BirdDS.Quarantine.Rows.Add(QuarantineRow)

                        If Not OldSpecimen.Permit Is Nothing AndAlso TypeOf OldSpecimen.Permit Is String AndAlso OldSpecimen.Permit.ToString.Length > 0 Then
                            'permit details
                            Dim PermitRow As BirdRegistrationDataset.PermitRow = BirdDS.Permit.NewPermitRow
                            With PermitRow
                                .AdultImportedBirdRow = AdultBirdRow
                                .Permit_text = OldSpecimen.Permit.ToString
                            End With
                            BirdDS.Permit.Rows.Add(PermitRow)
                        End If

                        'source details
                        Dim SourceRow As BirdRegistrationDataset.SourceRow = BirdDS.Source.NewSourceRow
                        With SourceRow
                            .AdultImportedBirdRow = AdultBirdRow
                            If Not OldSpecimen.IsFromUK Is Nothing AndAlso TypeOf OldSpecimen.IsFromUK Is Boolean Then .IsFromUK = CType(OldSpecimen.IsFromUK, Boolean) Else .SetIsFromUKNull()
                            .Origin = OldSpecimen.Origin
                            .Origin2 = OldSpecimen.Origin2
                            If OldSpecimen.CaptureMethod_Helper.Length > 0 Then .CaptureMethod = OldSpecimen.CaptureMethod_Helper Else .SetCaptureMethodNull()
                            If Not OldSpecimen.EULicenseNumber Is Nothing AndAlso TypeOf OldSpecimen.EULicenseNumber Is String Then .EULicenseNumber = OldSpecimen.EULicenseNumber.ToString Else .SetEULicenseNumberNull()
                            If Not OldSpecimen.SpecialConditions Is Nothing AndAlso TypeOf OldSpecimen.SpecialConditions Is String Then .SourceSpecialConditions = OldSpecimen.SpecialConditions.ToString Else .SetSourceSpecialConditionsNull()
                            If OldSpecimen.CountryOfOrigin > 0 Then .CountryOfOrigin = OldSpecimen.CountryOfOrigin Else .SetCountryOfOriginNull()
                        End With
                        BirdDS.Source.Rows.Add(SourceRow)

                        'loop through adding imported rings
                        For Each NewRing As IDMark In OldSpecimen.Rings  'MLD 19/1/5 enclosing If...End If removed, as no longer necessary
                            Dim RingRow As BirdRegistrationDataset.ImportedRingRow = BirdDS.ImportedRing.NewImportedRingRow
                            RingRow.AdultImportedBirdRow = AdultBirdRow
                            NewRing.PopulateIDMark(CType(RingRow, DataRow))
                            BirdDS.ImportedRing.Rows.Add(RingRow)
                        Next NewRing
                        GetParentData(BirdDS, OldSpecimen)

                    Next OldSpecimen
                End If
            End If
        End Sub

        Private Sub SetParentData(ByVal birdRegDS As BirdRegistrationDataset, ByVal specimen As AdultImportedSpecimen)
            'sort out the parent infomation
            If Not birdRegDS.ImportedParents Is Nothing AndAlso birdRegDS.ImportedParents.Count > 0 Then
                specimen.Parents = New Parents
                For Each Parent As BirdRegistrationDataset.ImportedParentsRow In birdRegDS.ImportedParents.Rows
                    For Each Father As BirdRegistrationDataset.ImportedFatherRow In Parent.GetImportedFatherRows
                        specimen.Parents.AddFather(Father.GetImportedFatherSpecimenRows, Father.GetImportedFatherIDMarkRows)
                    Next Father
                    For Each Mother As BirdRegistrationDataset.ImportedMotherRow In Parent.GetImportedMotherRows
                        specimen.Parents.AddMother(Mother.GetImportedMotherSpecimenRows, Mother.GetImportedMotherIDMarkRows)
                    Next Mother
                Next Parent
            Else
                specimen.Parents = Nothing
            End If
        End Sub

        Private Sub GetParentData(ByVal birdRegDS As BirdRegistrationDataset, ByVal specimen As AdultImportedSpecimen)
            If Not specimen.Parents Is Nothing AndAlso _
               (specimen.Parents.Father.Length > 0 OrElse specimen.Parents.Mother.Length > 0) Then
                'add the parent base record
                Dim ParentsRow As BirdRegistrationDataset.ImportedParentsRow = birdRegDS.ImportedParents.NewImportedParentsRow
                birdRegDS.ImportedParents.AddImportedParentsRow(ParentsRow)

                For Each SpecimenFather As Parent In specimen.Parents.Father
                    Dim SpecimenFatherRow As BirdRegistrationDataset.ImportedFatherRow = birdRegDS.ImportedFather.NewImportedFatherRow
                    SpecimenFatherRow.ImportedParentsRow = ParentsRow
                    birdRegDS.ImportedFather.AddImportedFatherRow(SpecimenFatherRow)
                    Dim SpecimenFatherRowItem As BirdRegistrationDataset.ImportedFatherSpecimenRow = birdRegDS.ImportedFatherSpecimen.NewImportedFatherSpecimenRow

                    SpecimenFatherRowItem.ImportedFatherRow = SpecimenFatherRow
                    SpecimenFather.UpdateSpecimen(SpecimenFatherRowItem)

                    birdRegDS.ImportedFatherSpecimen.AddImportedFatherSpecimenRow(SpecimenFatherRowItem)

                    For Each FatherMarks As IDMark In SpecimenFather.IdMarks
                        Dim FatherIdMarkRowItem As BirdRegistrationDataset.ImportedFatherIDMarkRow = birdRegDS.ImportedFatherIDMark.NewImportedFatherIDMarkRow
                        FatherIdMarkRowItem.ImportedFatherRow = SpecimenFatherRow
                        FatherMarks.PopulateIDMark(CType(FatherIdMarkRowItem, DataRow))
                        birdRegDS.ImportedFatherIDMark.AddImportedFatherIDMarkRow(FatherIdMarkRowItem)
                    Next FatherMarks
                Next SpecimenFather

                For Each SpecimenMother As Parent In specimen.Parents.Mother
                    Dim SpecimenMotherRow As BirdRegistrationDataset.ImportedMotherRow = birdRegDS.ImportedMother.NewImportedMotherRow
                    SpecimenMotherRow.ImportedParentsRow = ParentsRow
                    birdRegDS.ImportedMother.AddImportedMotherRow(SpecimenMotherRow)
                    Dim SpecimenMotherRowItem As BirdRegistrationDataset.ImportedMotherSpecimenRow = birdRegDS.ImportedMotherSpecimen.NewImportedMotherSpecimenRow

                    SpecimenMotherRowItem.ImportedMotherRow = SpecimenMotherRow
                    SpecimenMother.UpdateSpecimen(SpecimenMotherRowItem)

                    birdRegDS.ImportedMotherSpecimen.AddImportedMotherSpecimenRow(SpecimenMotherRowItem)

                    For Each MotherMarks As IDMark In SpecimenMother.IdMarks
                        Dim MotherIdMarkRowItem As BirdRegistrationDataset.ImportedMotherIDMarkRow = birdRegDS.ImportedMotherIDMark.NewImportedMotherIDMarkRow
                        MotherIdMarkRowItem.ImportedMotherRow = SpecimenMotherRow
                        MotherMarks.PopulateIDMark(CType(MotherIdMarkRowItem, DataRow))
                        birdRegDS.ImportedMotherIDMark.AddImportedMotherIDMarkRow(MotherIdMarkRowItem)
                    Next MotherMarks
                Next SpecimenMother
            Else
                specimen.Parents = Nothing
            End If
        End Sub

        Public Overrides Function AddPolymorphicSpecimen() As AdultSpecimenType
            Return AddSpecimen()
        End Function

        Public Overloads Function AddSpecimen() As AdultImportedSpecimen
            Dim specimen As New AdultImportedSpecimen
            AddSpecimen(specimen)
            Return specimen
        End Function

        'Public Property Specimens() As AdultImportedSpecimen()
        '    Get
        '        Return mSpecimens
        '    End Get
        '    Set(ByVal Value As AdultImportedSpecimen())
        '        mSpecimens = Value
        '    End Set
        'End Property
        'Private mSpecimens(-1) As AdultImportedSpecimen  'MLD modified 7/1/5 so that starts off empty, not Nothing

        'Public Overrides Property BaseSpecimens() As AdultSpecimenType()
        '    Get
        '        Return CType(mSpecimens, AdultSpecimenType())
        '    End Get
        '    Set(ByVal Value As AdultSpecimenType())
        '        If TypeOf Value Is AdultImportedSpecimen() Then
        '            mSpecimens = CType(Value, AdultImportedSpecimen())
        '        End If
        '    End Set
        'End Property

        'Public Overrides Function BaseAddSpecimen() As AdultSpecimenType
        '    Dim Result As Object = AddSpecimen()
        '    If Result Is Nothing Then
        '        Return Nothing
        '    Else
        '        Return CType(Result, AdultSpecimenType)
        '    End If
        'End Function

        Friend Overrides Function IsValid(ByVal addError As BirdRegistration.AddValidationErrorDelegate, ByVal ssoUserId As Int64, ByVal applicationId As Int32) As Boolean
            Dim ValidState As Boolean = MyBase.IsValid(addError, ssoUserId, applicationId)
            Dim MinSpecimens As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.AdultImportedSpecimenMinOccurs), Int32)
            Dim MaxSpecimens As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.AdultImportedSpecimenMaxOccurs), Int32)
            If (mSpecimens Is Nothing AndAlso MinSpecimens > 0) OrElse _
               (Not mSpecimens Is Nothing AndAlso mSpecimens.Length < MinSpecimens) OrElse _
               Not mSpecimens Is Nothing AndAlso mSpecimens.Length > MaxSpecimens Then
                Dim ReplaceVals As New Collections.Specialized.NameValueCollection
                ReplaceVals.Add("%1", MinSpecimens.ToString)
                ReplaceVals.Add("%2", MaxSpecimens.ToString)
                addError(ValidationError.ValidationCodes.MustHaveXBirds, ReplaceVals)
                ValidState = False
            End If
            If Not mSpecimens Is Nothing AndAlso mSpecimens.Length > 0 Then
                Dim MinIdMarks As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.AdultImportedIdMarkMinOccurs), Int32)
                Dim MaxIdMarks As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.AdultImportedIdMarkMaxOccurs), Int32)
                Dim MinRings As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.AdultImportedRingsMinOccurs), Int32)
                Dim MaxRings As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.AdultImportedRingsMaxOccurs), Int32)
                Dim AcquiredDateNillable As Boolean = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.AdultImportedAcquiredDateNillable), Boolean)

                For Each spec As AdultSpecimenType In mSpecimens
                    If spec.IDMarks.Length < MinIdMarks OrElse spec.IDMarks.Length > MaxIdMarks Then   'MLD 19/1/5 simplified, as Nothing no longer possible
                        Dim ReplaceVals As New Collections.Specialized.NameValueCollection
                        ReplaceVals.Add("%1", MinIdMarks.ToString)
                        ReplaceVals.Add("%2", spec.SpecimenType.SpecimenId.ToString)
                        ReplaceVals.Add("%3", MaxIdMarks.ToString)
                        addError(ValidationError.ValidationCodes.MustHaveXIdMarks, ReplaceVals)
                        ValidState = False
                    End If

                    If spec.Rings.Length < MinRings OrElse spec.Rings.Length > MaxRings Then    'MLD 19/1/5 simplified, as Nothing no longer possible
                        Dim ReplaceVals As New Collections.Specialized.NameValueCollection
                        ReplaceVals.Add("%1", MinRings.ToString)
                        ReplaceVals.Add("%2", spec.SpecimenType.SpecimenId.ToString)
                        ReplaceVals.Add("%3", MaxRings.ToString)
                        addError(ValidationError.ValidationCodes.MustHaveXIdMarks, ReplaceVals)
                        ValidState = False
                    End If

                    With CType(spec, AdultImportedSpecimen)
                        If Not AcquiredDateNillable AndAlso .DateAcquired.Ticks = 0 Then
                            Dim ReplaceVals As New Collections.Specialized.NameValueCollection
                            ReplaceVals.Add("%1", spec.SpecimenType.SpecimenId.ToString)
                            addError(ValidationError.ValidationCodes.MustHaveDateAcquiredDate, ReplaceVals)
                            ValidState = False
                        End If
                        'If .AcquisitionDetails Is Nothing OrElse .AcquisitionDetails.Length = 0 Then
                        '    Dim ReplaceVals As New Collections.Specialized.NameValueCollection
                        '    ReplaceVals.Add("%1", "Acquisition Details")
                        '    addError(ValidationError.ValidationCodes.XCannotBeBlank, ReplaceVals)
                        '    ValidState = False
                        'End If
                    End With
                Next spec
            End If

            Return ValidState
        End Function

        Public Property IsVet() As Boolean
            Get
                Return mIsVet
            End Get
            Set(ByVal Value As Boolean)
                mIsVet = Value
            End Set
        End Property
        Private mIsVet As Boolean

        Public Property IsKeeperOfThree() As Boolean
            Get
                Return mIsKeeperOfThree
            End Get
            Set(ByVal Value As Boolean)
                mIsKeeperOfThree = Value
            End Set
        End Property
        Private mIsKeeperOfThree As Boolean

        Public Property IsRSPCAInspector() As Boolean
            Get
                Return mIsRSPCAInspector
            End Get
            Set(ByVal Value As Boolean)
                mIsRSPCAInspector = Value
            End Set
        End Property
        Private mIsRSPCAInspector As Boolean

        Public Overrides Property RingsPicked() As Boolean
            Get
                Return AdultBred.AreRingsPicked(mSpecimens)
            End Get
            Set(ByVal Value As Boolean)
                'mRingsPicked = Value
            End Set
        End Property
    End Class
End Namespace

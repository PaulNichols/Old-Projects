Namespace Application.Bird.Registration
    <Serializable()> _
    Public Class AdultOther
        Inherits BaseBird

        Public Sub New()
        End Sub

        Public Overrides Function AddPolymorphicSpecimen() As AdultSpecimenType
            Return AddSpecimen()
        End Function

        Public Overloads Function AddSpecimen() As AdultOtherSpecimen
            Dim specimen As New AdultOtherSpecimen
            AddSpecimen(specimen)
            specimen.PreviousKeeper = New PreviousKeeperAddress
            Return specimen
        End Function

        'Public Function AddSpecimen() As AdultOtherSpecimen   'MLD modified 7/1/5
        '    Dim len As Int32 = mSpecimens.Length
        '    ReDim Preserve mSpecimens(len)
        '    mSpecimens(len) = New AdultOtherSpecimen
        '    Return mSpecimens(len)
        'End Function

        'Public Property Specimen() As AdultOtherSpecimen()
        '    Get
        '        Return mSpecimens
        '    End Get
        '    Set(ByVal Value As AdultOtherSpecimen())
        '        mSpecimens = Value
        '    End Set
        'End Property
        'Private mSpecimens(-1) As AdultOtherSpecimen  'MLD modified 7/1/5 so that starts off empty, not Nothing

        'Public Overrides Property BaseSpecimens() As AdultSpecimenType()
        '    Get
        '        Return CType(mSpecimens, AdultSpecimenType())
        '    End Get
        '    Set(ByVal Value As AdultSpecimenType())
        '        If TypeOf Value Is AdultOtherSpecimen() Then
        '            mSpecimens = CType(Value, AdultOtherSpecimen())
        '        End If
        '    End Set
        'End Property

        'Public Overrides Function BaseAddSpecimen() As AdultSpecimenType
        '    Dim Result As Object = AddSpecimen()
        '    If Result Is Nothing Then
        '        Return Nothing
        '    Else
        '        Return CType(AddSpecimen(), AdultSpecimenType)
        '    End If
        'End Function

        Public Sub New(ByVal adultOtherBirds As BirdRegistrationDataset.AdultOtherBirdDataTable)
            If Not adultOtherBirds Is Nothing AndAlso _
               adultOtherBirds.Rows.Count > 0 Then
                'Dim AdultOtherRow As BirdRegistrationDataset.AdultOtherRow = CType(adultOtherBirds.Rows(0), BirdRegistrationDataset.AdultOtherRow)

                For Each OtherBirdRow As BirdRegistrationDataset.AdultOtherBirdRow In adultOtherBirds.Rows ' AdultOtherRow.GetAdultOtherBirdRows
                    'create an object to put the items in
                    Dim NewSpecimen As AdultOtherSpecimen = AddSpecimen()
                    'check to see if one is created
                    If Not NewSpecimen Is Nothing Then
                        SpecimenType.CreateSpecimen(CType(NewSpecimen, AdultSpecimenType), OtherBirdRow.GetAdultOtherSpecimenRows)

                        With OtherBirdRow
                            NewSpecimen.SetAquisitionMethod(.AcquisitionMethod)
                            If Not .IsAcquisitionDetailsNull Then NewSpecimen.AcquisitionDetails = .AcquisitionDetails
                            NewSpecimen.DateAcquired = .DateAcquired
                            If Not .IsPreviousKeeperNull Then NewSpecimen.PreviousKeeper = PreviousKeeperAddress.SetAddress(.PreviousKeeper) Else NewSpecimen.PreviousKeeper = New PreviousKeeperAddress
                            If Not .IsEvidenceExplanationNull Then NewSpecimen.EvidenceExplanation = .EvidenceExplanation
                            If Not .IsKeptAddressNull Then NewSpecimen.KeptAddressId = .KeptAddress
                        End With

                        NewSpecimen.Statements = Statements.CreateStatement(OtherBirdRow.GetAdultOtherStatementsRows)

                        'loop through adding id marks
                        For Each OtherIdMark As BirdRegistrationDataset.OtherIDMarksRow In OtherBirdRow.GetOtherIDMarksRows
                            NewSpecimen.AddIdMark(OtherIdMark)
                        Next OtherIdMark

                        'loop through adding imported rings
                        For Each OtherRing As BirdRegistrationDataset.OtherRingRow In OtherBirdRow.GetOtherRingRows
                            NewSpecimen.AddRing(OtherRing)
                        Next OtherRing

                    End If

                    'should only be one row, so bail after
                    'changes so that many birds can be included
                    'Exit For
                Next OtherBirdRow
            End If
        End Sub

        Friend Sub GetData(ByRef adultOtherBirds As BirdRegistrationDataset.AdultOtherBirdDataTable)
            If Not adultOtherBirds Is Nothing Then
                Dim BirdDS As BirdRegistrationDataset = CType(adultOtherBirds.DataSet, BirdRegistrationDataset)
                Dim AdultBirdRowId As Int32 = 0

                'Dim AdultOtherRow As BirdRegistrationDataset.AdultOtherRow = adultOtherBirds.NewAdultOtherRow
                'AdultOtherRow(adultOtherBirds.AdultOther_IdColumn) = 1
                'adultOtherBirds.AddAdultOtherRow(AdultOtherRow)

                If Not mSpecimens Is Nothing Then
                    For Each OldSpecimen As AdultOtherSpecimen In mSpecimens
                        AdultBirdRowId += 1
                        Dim AdultBirdRow As BirdRegistrationDataset.AdultOtherBirdRow = BirdDS.AdultOtherBird.NewAdultOtherBirdRow
                        AdultBirdRow(BirdDS.AdultOtherBird.AdultOtherBird_IdColumn) = AdultBirdRowId

                        'sort out the specimens
                        Dim SpecimenRow As BirdRegistrationDataset.AdultOtherSpecimenRow = Nothing
                        If Not OldSpecimen.SpecimenType Is Nothing Then
                            SpecimenRow = BirdDS.AdultOtherSpecimen.NewAdultOtherSpecimenRow
                            SpecimenRow.AdultOtherBirdRow = AdultBirdRow
                            OldSpecimen.SpecimenType.UpdateSpecimen(SpecimenRow)
                        End If

                        With AdultBirdRow
                            .AcquisitionMethod = OldSpecimen.AcquisitionMethod_Helper
                            If OldSpecimen.AcquisitionDetails Is Nothing OrElse OldSpecimen.AcquisitionDetails.Length = 0 Then .SetAcquisitionDetailsNull() Else .AcquisitionDetails = OldSpecimen.AcquisitionDetails
                            If OldSpecimen.DateAcquired.Ticks > 0 Then .DateAcquired = OldSpecimen.DateAcquired Else .DateAcquired = Date.Today
                            If Not OldSpecimen.PreviousKeeper Is Nothing Then .PreviousKeeper = OldSpecimen.PreviousKeeper.GetAddress() Else .SetPreviousKeeperNull()
                            If Not OldSpecimen.EvidenceExplanation Is Nothing Then .EvidenceExplanation = OldSpecimen.EvidenceExplanation Else .SetEvidenceExplanationNull()
                            If Not OldSpecimen.KeptAddressId Is Nothing AndAlso TypeOf OldSpecimen.KeptAddressId Is Int32 Then .KeptAddress = CType(OldSpecimen.KeptAddressId, Int32) Else .SetKeptAddressNull()
                        End With

                        Dim StatementRow As BirdRegistrationDataset.AdultOtherStatementsRow = Nothing
                        If Not OldSpecimen.Statements Is Nothing Then
                            StatementRow = BirdDS.AdultOtherStatements.NewAdultOtherStatementsRow
                            StatementRow.AdultOtherBirdRow = AdultBirdRow
                            OldSpecimen.Statements.UpdateStatement(StatementRow)
                        End If

                        adultOtherBirds.AddAdultOtherBirdRow(AdultBirdRow)
                        If Not SpecimenRow Is Nothing Then
                            BirdDS.AdultOtherSpecimen.Rows.Add(SpecimenRow)
                        End If
                        If Not StatementRow Is Nothing Then
                            BirdDS.AdultOtherStatements.Rows.Add(StatementRow)
                        End If

                        'loop through adding id marks
                        For Each NewMark As IDMark In OldSpecimen.IDMarks  'MLD 19/1/5 enclosing If...End If removed, as no longer necessary
                            Dim IDMarkRow As BirdRegistrationDataset.OtherIDMarksRow = BirdDS.OtherIDMarks.NewOtherIDMarksRow
                            IDMarkRow.AdultOtherBirdRow = AdultBirdRow
                            NewMark.PopulateIDMark(CType(IDMarkRow, DataRow))
                            BirdDS.OtherIDMarks.Rows.Add(IDMarkRow)
                        Next NewMark

                        'loop through adding imported rings
                        For Each NewRing As IDMark In OldSpecimen.Rings  'MLD 19/1/5 enclosing If...End If removed, as no longer necessary
                            Dim RingRow As BirdRegistrationDataset.OtherRingRow = BirdDS.OtherRing.NewOtherRingRow
                            RingRow.AdultOtherBirdRow = AdultBirdRow
                            NewRing.PopulateIDMark(CType(RingRow, DataRow))
                            BirdDS.OtherRing.Rows.Add(RingRow)
                        Next NewRing
                    Next OldSpecimen
                End If
            End If
        End Sub

        Friend Overrides Function IsValid(ByVal addError As BirdRegistration.AddValidationErrorDelegate, ByVal ssoUserId As Int64, ByVal applicationId As Int32) As Boolean
            Dim ValidState As Boolean = MyBase.IsValid(addError, ssoUserId, applicationId)
            Dim MinSpecimens As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.AdultOtherSpecimenMinOccurs), Int32)
            Dim MaxSpecimens As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.AdultOtherSpecimenMaxOccurs), Int32)
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
                Dim MinIdMarks As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.AdultOtherIdMarkMinOccurs), Int32)
                Dim MaxIdMarks As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.AdultOtherIdMarkMaxOccurs), Int32)
                Dim MinRings As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.AdultOtherRingsMinOccurs), Int32)
                Dim MaxRings As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.AdultOtherRingsMaxOccurs), Int32)

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
                    'With CType(spec, AdultOtherSpecimen)
                    '    If .AcquisitionDetails Is Nothing OrElse .AcquisitionDetails.Length = 0 Then
                    '        Dim ReplaceVals As New Collections.Specialized.NameValueCollection
                    '        ReplaceVals.Add("%1", "Acquisition Details")
                    '        addError(ValidationError.ValidationCodes.XCannotBeBlank, ReplaceVals)
                    '        ValidState = False
                    '    End If
                    'End With
                Next spec
            End If
            'If mAcquisitionDetails Is Nothing OrElse mAcquisitionDetails.Length = 0 Then
            '    Dim ReplaceVals As New Collections.Specialized.NameValueCollection
            '    ReplaceVals.Add("%1", "Acquisition Details")
            '    addError(ValidationError.ValidationCodes.XCannotBeBlank, ReplaceVals)
            '    ValidState = False
            'End If

            Return ValidState
        End Function

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
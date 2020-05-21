Namespace Application.Bird.Registration
    <Serializable()> _
    Public Class AdultFound
        Inherits BaseBird

        Public Sub New()
        End Sub

        Public Overrides Function AddPolymorphicSpecimen() As AdultSpecimenType
            Return AddSpecimen()
        End Function

        Public Overloads Function AddSpecimen() As AdultFoundSpecimen
            Dim specimen As New AdultFoundSpecimen
            AddSpecimen(specimen)
            Return specimen
        End Function

        'Public Function AddSpecimen() As AdultFoundSpecimen   'MLD modified 7/1/5
        '    Dim len As Int32 = mSpecimens.Length
        '    ReDim Preserve mSpecimens(len)
        '    mSpecimens(len) = New AdultFoundSpecimen
        '    Return mSpecimens(len)
        'End Function

        'Public Property Specimens() As AdultFoundSpecimen()
        '    Get
        '        Return mSpecimens
        '    End Get
        '    Set(ByVal Value As AdultFoundSpecimen())
        '        mSpecimens = Value
        '    End Set
        'End Property
        'Private mSpecimens(-1) As AdultFoundSpecimen   'MLD modified 7/1/5 so that starts off empty, not Nothing

        Public Sub New(ByVal adultFoundBirds As BirdRegistrationDataset.AdultFoundDataTable)
            If Not adultFoundBirds Is Nothing AndAlso _
               adultFoundBirds.Rows.Count > 0 Then
                Dim AdultBirdRow As BirdRegistrationDataset.AdultFoundRow = CType(adultFoundBirds.Rows(0), BirdRegistrationDataset.AdultFoundRow)
                mIsKeeperOfThree = AdultBirdRow.IsKeeperOfThree
                mIsRSPCAInspector = AdultBirdRow.IsKeeperRSPCA
                mIsVet = AdultBirdRow.IsKeeperVet

                For Each FoundBirdRow As BirdRegistrationDataset.AdultFoundBirdRow In AdultBirdRow.GetAdultFoundBirdRows
                    'create an object to put the items in
                    Dim NewSpecimen As AdultFoundSpecimen = AddSpecimen()
                    'check to see if one is created
                    If Not NewSpecimen Is Nothing Then
                        SpecimenType.CreateSpecimen(CType(NewSpecimen, AdultSpecimenType), FoundBirdRow.GetFoundBirdSpecimenRows)

                        'loop through adding id marks
                        For Each FoundIdMark As BirdRegistrationDataset.FoundBirdIDMarkRow In FoundBirdRow.GetFoundBirdIDMarkRows
                            NewSpecimen.AddIdMark(FoundIdMark)
                        Next FoundIdMark

                        'loop through adding imported rings
                        For Each FoundRing As BirdRegistrationDataset.FoundRingRow In FoundBirdRow.GetFoundRingRows
                            NewSpecimen.AddRing(FoundRing)
                        Next FoundRing

                        With NewSpecimen
                            .DateFound = FoundBirdRow.DateFound
                            If Not FoundBirdRow.IsDateAcquiredNull Then .DateAcquired = FoundBirdRow.DateAcquired Else .DateAcquired = Nothing
                            .AcquisitionDetails = FoundBirdRow.AcquisitionDetails
                            .SetAquisitionMethod(FoundBirdRow.AcquisitionMethod)
                            .InjuryDetails = FoundBirdRow.InjuryDetails
                            If Not FoundBirdRow.IsKeptAddressNull Then .KeptAddressId = FoundBirdRow.KeptAddress Else .KeptAddressId = Nothing

                            .Statements = Statements.CreateStatement(FoundBirdRow.GetFoundBirdStatementsRows)
                        End With
                    End If
                Next FoundBirdRow
            End If
        End Sub

        Friend Sub GetData(ByRef adultFoundBirds As BirdRegistrationDataset.AdultFoundDataTable)
            If Not adultFoundBirds Is Nothing Then
                Dim BirdDS As BirdRegistrationDataset = CType(adultFoundBirds.DataSet, BirdRegistrationDataset)
                'update base found data
                Dim AdultBirdRow As BirdRegistrationDataset.AdultFoundRow = adultFoundBirds.NewAdultFoundRow
                AdultBirdRow(adultFoundBirds.AdultFound_IdColumn) = 1
                With AdultBirdRow
                    .IsKeeperOfThree = mIsKeeperOfThree
                    .IsKeeperRSPCA = mIsRSPCAInspector
                    .IsKeeperVet = mIsVet
                End With
                adultFoundBirds.AddAdultFoundRow(AdultBirdRow)

                If Not mSpecimens Is Nothing Then
                    Dim AdultBirdRowId As Int32 = 0
                    For Each OldSpecimen As AdultFoundSpecimen In mSpecimens
                        AdultBirdRowId += 1
                        Dim FoundBirdRow As BirdRegistrationDataset.AdultFoundBirdRow = BirdDS.AdultFoundBird.NewAdultFoundBirdRow
                        FoundBirdRow(BirdDS.AdultFoundBird.AdultFound_IdColumn) = AdultBirdRowId
                        FoundBirdRow.DateFound = Date.Now          'MLD 7/1/5 added as this cannot be Null
                        FoundBirdRow.AcquisitionMethod = "Unknown"
                        FoundBirdRow.AcquisitionDetails = String.Empty
                        FoundBirdRow.InjuryDetails = ""            'MLD 7/1/5 added as this cannot be Null
                        FoundBirdRow.AdultFoundRow = AdultBirdRow
                        BirdDS.AdultFoundBird.AddAdultFoundBirdRow(FoundBirdRow)

                        'sort out the specimens
                        If Not OldSpecimen.SpecimenType Is Nothing Then
                            Dim SpecimenRow As BirdRegistrationDataset.FoundBirdSpecimenRow = BirdDS.FoundBirdSpecimen.NewFoundBirdSpecimenRow
                            SpecimenRow.AdultFoundBirdRow = FoundBirdRow
                            OldSpecimen.SpecimenType.UpdateSpecimen(SpecimenRow)
                            BirdDS.FoundBirdSpecimen.Rows.Add(SpecimenRow)
                        End If

                        If Not OldSpecimen.Statements Is Nothing Then
                            Dim StatementRow As BirdRegistrationDataset.FoundBirdStatementsRow = BirdDS.FoundBirdStatements.NewFoundBirdStatementsRow
                            StatementRow.AdultFoundBirdRow = FoundBirdRow
                            OldSpecimen.Statements.UpdateStatement(StatementRow)
                            BirdDS.FoundBirdStatements.Rows.Add(StatementRow)
                        End If

                        'loop through adding id marks
                        For Each NewMark As IDMark In OldSpecimen.IDMarks  'MLD 19/1/5 enclosing If...End If removed, as no longer necessary
                            Dim IDMarkRow As BirdRegistrationDataset.FoundBirdIDMarkRow = BirdDS.FoundBirdIDMark.NewFoundBirdIDMarkRow
                            IDMarkRow.AdultFoundBirdRow = FoundBirdRow
                            NewMark.PopulateIDMark(CType(IDMarkRow, DataRow))
                            BirdDS.FoundBirdIDMark.Rows.Add(IDMarkRow)
                        Next NewMark

                        'loop through adding imported rings
                        For Each NewRing As IDMark In OldSpecimen.Rings  'MLD 19/1/5 enclosing If...End If removed, as no longer necessary
                            Dim RingRow As BirdRegistrationDataset.FoundRingRow = BirdDS.FoundRing.NewFoundRingRow
                            RingRow.AdultFoundBirdRow = FoundBirdRow
                            NewRing.PopulateIDMark(CType(RingRow, DataRow))
                            BirdDS.FoundRing.Rows.Add(RingRow)
                        Next NewRing

                        With FoundBirdRow
                            .DateFound = OldSpecimen.DateFound
                            'not optional so just cast!
                            If OldSpecimen.DateAcquired.Ticks > 0 Then
                                .DateAcquired = CType(OldSpecimen.DateAcquired, Date)
                            Else
                                .SetDateAcquiredNull()
                            End If
                            If Not OldSpecimen.AcquisitionDetails Is Nothing Then    'MLD 7/1/5 test added as property may not be Null
                                .AcquisitionDetails = OldSpecimen.AcquisitionDetails
                            End If
                            .AcquisitionMethod = OldSpecimen.AcquisitionMethod_Helper
                            If Not OldSpecimen.InjuryDetails Is Nothing Then        'MLD 7/1/5 test added as property may not be Null
                                .InjuryDetails = OldSpecimen.InjuryDetails
                            End If
                            If Not OldSpecimen.KeptAddressId Is Nothing AndAlso TypeOf OldSpecimen.KeptAddressId Is Int32 Then
                                .KeptAddress = CType(OldSpecimen.KeptAddressId, Int32)
                            Else
                                .SetKeptAddressNull()
                            End If
                        End With
                    Next OldSpecimen
                End If
            End If
        End Sub

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

        'Public Overrides Property BaseSpecimens() As AdultSpecimenType()
        '    Get
        '        Return CType(mSpecimens, AdultSpecimenType())
        '    End Get
        '    Set(ByVal Value As AdultSpecimenType())
        '        If TypeOf Value Is AdultFoundSpecimen() Then
        '            mSpecimens = CType(Value, AdultFoundSpecimen())
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

        Friend Overrides Function IsValid(ByVal addError As BirdRegistration.AddValidationErrorDelegate, ByVal ssoUserId As Int64, ByVal applicationId As Int32) As Boolean
            Dim ValidState As Boolean = MyBase.IsValid(addError, ssoUserId, applicationId)
            Dim MinSpecimens As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.AdultFoundSpecimenMinOccurs), Int32)
            Dim MaxSpecimens As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.AdultFoundSpecimenMaxOccurs), Int32)
            If (mSpecimens Is Nothing AndAlso mSpecimens.Length > 0) OrElse _
               (Not mSpecimens Is Nothing AndAlso mSpecimens.Length < MinSpecimens) OrElse _
               Not mSpecimens Is Nothing AndAlso mSpecimens.Length > MaxSpecimens Then
                Dim ReplaceVals As New Collections.Specialized.NameValueCollection
                ReplaceVals.Add("%1", MinSpecimens.ToString)
                ReplaceVals.Add("%2", MaxSpecimens.ToString)
                addError(ValidationError.ValidationCodes.MustHaveXBirds, ReplaceVals)
                ValidState = False
            End If
            If Not mSpecimens Is Nothing AndAlso mSpecimens.Length > 0 Then
                Dim MinIdMarks As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.AdultFoundIdMarkMinOccurs), Int32)
                Dim MaxIdMarks As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.AdultFoundIdMarkMaxOccurs), Int32)
                Dim MinRings As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.AdultFoundRingsMinOccurs), Int32)
                Dim MaxRings As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.AdultFoundRingsMaxOccurs), Int32)

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
                    'With CType(spec, AdultFoundSpecimen)
                    '    If .AcquisitionDetails Is Nothing OrElse .AcquisitionDetails.Length = 0 Then
                    '        Dim ReplaceVals As New Collections.Specialized.NameValueCollection
                    '        ReplaceVals.Add("%1", "Acquisition Details")
                    '        addError(ValidationError.ValidationCodes.XCannotBeBlank, ReplaceVals)
                    '        ValidState = False
                    '    End If
                    'End With
                Next spec
            End If

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

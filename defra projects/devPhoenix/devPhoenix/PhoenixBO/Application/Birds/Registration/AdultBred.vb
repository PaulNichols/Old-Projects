Namespace Application.Bird.Registration
    <Serializable()> _
    Public Class AdultBred
        Inherits BaseBird

        Public Sub New()
            MyBase.New()
            ' has only one specimen so add it...
            '            mSpecimen = New AdultBredSpecimen
            'init the parent object...
            'mParents = New Parents
        End Sub

        Public Overrides Function AddPolymorphicSpecimen() As AdultSpecimenType
            Return AddSpecimen()
        End Function

        Public Overloads Function AddSpecimen() As AdultBredSpecimen
            Dim specimen As New AdultBredSpecimen
            AddSpecimen(specimen)
            Return specimen
        End Function

        'Public Function AddSpecimen() As AdultBredSpecimen   'MLD modified 7/1/5
        '    Dim len As Int32 = mSpecimens.Length
        '    ReDim Preserve mSpecimens(len)
        '    mSpecimens(len) = New AdultBredSpecimen
        '    Return mSpecimens(len)
        'End Function

        'Public Property Specimen() As AdultBredSpecimen()
        '    Get
        '        Return mSpecimens
        '    End Get
        '    Set(ByVal Value As AdultBredSpecimen())
        '        mSpecimens = Value
        '    End Set
        'End Property
        'Private mSpecimens(-1) As AdultBredSpecimen  'MLD modified 7/1/5 so that starts off empty, not Nothing

        Public Sub New(ByVal adultBredBirds As BirdRegistrationDataset.AdultBredBirdDataTable)
            If Not adultBredBirds Is Nothing AndAlso _
               adultBredBirds.Rows.Count > 0 Then
                'Dim AdultBredRow As BirdRegistrationDataset.AdultBredRow = CType(adultBredBirds.Rows(0), BirdRegistrationDataset.AdultBredRow)

                For Each BredBirdRow As BirdRegistrationDataset.AdultBredBirdRow In adultBredBirds.Rows 'AdultBredRow.GetAdultBredBirdRows()
                    'create an object to put the items in
                    Dim NewSpecimen As AdultBredSpecimen = AddSpecimen()
                    'check to see if one is created
                    If Not NewSpecimen Is Nothing Then
                        SpecimenType.CreateSpecimen(CType(NewSpecimen, AdultSpecimenType), BredBirdRow.GetBredSpecimenRows)

                        'loop through adding id marks
                        For Each BredIdMark As BirdRegistrationDataset.BredIDMarksRow In BredBirdRow.GetBredIDMarksRows
                            NewSpecimen.AddIdMark(BredIdMark)
                        Next BredIdMark

                        'loop through adding imported rings
                        For Each BredRing As BirdRegistrationDataset.BredRingRow In BredBirdRow.GetBredRingRows
                            NewSpecimen.AddRing(BredRing)
                        Next BredRing

                        NewSpecimen.Statements = Statements.CreateStatement(BredBirdRow.GetBredStatementsRows)
                    End If

                    'should only be one row, so bail after
                    'changes so that many birds can be included
                    'Exit For
                Next BredBirdRow
            End If
        End Sub

        Friend Sub GetData(ByRef adultBredBirds As BirdRegistrationDataset.AdultBredBirdDataTable)
            If Not adultBredBirds Is Nothing Then
                Dim BirdDS As BirdRegistrationDataset = CType(adultBredBirds.DataSet, BirdRegistrationDataset)
                'Dim AdultBirdRowId As Int32 = 1

                'Dim AdultBredRow As BirdRegistrationDataset.AdultBredRow = adultBredBirds.NewAdultBredRow
                'AdultBredRow(adultBredBirds.AdultBred_IdColumn) = 1
                'adultBredBirds.AddAdultBredRow(AdultBredRow)

                If Not mSpecimens Is Nothing Then
                    Dim AdultBirdRowId As Int32 = 0
                    For Each OldSpecimen As AdultBredSpecimen In mSpecimens
                        AdultBirdRowId += 1
                        Dim AdultBirdRow As BirdRegistrationDataset.AdultBredBirdRow = BirdDS.AdultBredBird.NewAdultBredBirdRow
                        AdultBirdRow(BirdDS.AdultBredBird.AdultBredBird_IdColumn) = AdultBirdRowId
                        BirdDS.AdultBredBird.AddAdultBredBirdRow(AdultBirdRow)

                        'sort out the specimens
                        If Not OldSpecimen.SpecimenType Is Nothing Then
                            Dim SpecimenRow As BirdRegistrationDataset.BredSpecimenRow = BirdDS.BredSpecimen.NewBredSpecimenRow
                            SpecimenRow.AdultBredBirdRow = AdultBirdRow
                            OldSpecimen.SpecimenType.UpdateSpecimen(SpecimenRow)
                            BirdDS.BredSpecimen.Rows.Add(SpecimenRow)
                        End If

                        If Not OldSpecimen.Statements Is Nothing Then
                            Dim StatementRow As BirdRegistrationDataset.BredStatementsRow = BirdDS.BredStatements.NewBredStatementsRow
                            StatementRow.AdultBredBirdRow = AdultBirdRow
                            OldSpecimen.Statements.UpdateStatement(StatementRow)
                            BirdDS.BredStatements.Rows.Add(StatementRow)
                        End If

                        'loop through adding id marks
                        For Each NewMark As IDMark In OldSpecimen.IDMarks 'MLD 19/1/5 enclosing If...End If removed, as no longer necessary
                            Dim IDMarkRow As BirdRegistrationDataset.BredIDMarksRow = BirdDS.BredIDMarks.NewBredIDMarksRow
                            IDMarkRow.AdultBredBirdRow = AdultBirdRow
                            NewMark.PopulateIDMark(CType(IDMarkRow, DataRow))
                            BirdDS.BredIDMarks.Rows.Add(IDMarkRow)
                        Next NewMark

                        'loop through adding imported rings
                        For Each NewRing As IDMark In OldSpecimen.Rings 'MLD 19/1/5 enclosing If...End If removed, as no longer necessary
                            Dim RingRow As BirdRegistrationDataset.BredRingRow = BirdDS.BredRing.NewBredRingRow
                            RingRow.AdultBredBirdRow = AdultBirdRow
                            NewRing.PopulateIDMark(CType(RingRow, DataRow))
                            BirdDS.BredRing.Rows.Add(RingRow)
                        Next NewRing
                    Next OldSpecimen
                End If
            End If
        End Sub

        'Public Overrides Property BaseSpecimens() As AdultSpecimenType()
        '    Get
        '        Return CType(mSpecimens, AdultSpecimenType())
        '    End Get
        '    Set(ByVal Value As AdultSpecimenType())
        '        If TypeOf Value Is AdultBredSpecimen() Then
        '            mSpecimens = CType(Value, AdultBredSpecimen())
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
            Dim MinSpecimens As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.AdultBredSpecimenMinOccurs), Int32)
            Dim MaxSpecimens As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.AdultBredSpecimenMaxOccurs), Int32)
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
                Dim MinIdMarks As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.AdultBredIdMarkMinOccurs), Int32)
                Dim MaxIdMarks As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.AdultBredIdMarkMaxOccurs), Int32)
                Dim MinRings As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.AdultBredRingsMinOccurs), Int32)
                Dim MaxRings As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.AdultBredRingsMaxOccurs), Int32)

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
                Next spec
            End If

            Return ValidState
        End Function


        Friend Shared Function AreRingsPicked(ByVal specimens As AdultSpecimenType()) As Boolean
            Dim Result As Boolean = False
            If Not specimens Is Nothing AndAlso specimens.Length > 0 Then
                Result = True

                For Each Adult As AdultSpecimenType In specimens
                    If Adult.Rings Is Nothing OrElse Adult.Rings.Length = 0 Then
                        Result = False
                        Exit For
                    End If
                Next Adult
            End If
            Return Result
        End Function

        Public Overrides Property RingsPicked() As Boolean
            Get
                Return AdultBred.AreRingsPicked(mSpecimens)
            End Get
            Set(ByVal Value As Boolean)
                'mRingsPicked = Value
            End Set
        End Property
        'Private mRingsPicked As Boolean
    End Class
End Namespace

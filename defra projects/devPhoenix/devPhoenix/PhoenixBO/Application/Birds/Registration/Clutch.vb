Namespace Application.Bird.Registration
    <Serializable()> _
    Public Class Clutch
        Inherits BaseBird

        Private ShowMessage As Boolean = False
        Private Const AnnexAMessage As String = "If the chicks are to be sold or used commercially in any way, each chick and each parent of that chick will require an EU Commercial Use Certificate"

        Public Sub New()
            ' must always have at least one ring so add it...
            'AddRing()
            'init the parent object...
            'mParents = New Parents
        End Sub

        Public Overrides Function AddPolymorphicSpecimen() As AdultSpecimenType
            Return AddSpecimen()
        End Function

        Public Overloads Function AddSpecimen() As ClutchSpecimen
            Dim specimen As New ClutchSpecimen
            AddSpecimen(specimen)
            Return specimen
        End Function

        Public Sub New(ByVal clutch As BirdRegistrationDataset.ClutchDataTable)
            ShowMessage = False
            If Not clutch Is Nothing AndAlso _
               clutch.Rows.Count > 0 Then
                Dim ClutchRow As BirdRegistrationDataset.ClutchRow = CType(clutch.Rows(0), BirdRegistrationDataset.ClutchRow)
                mIsHatched = ClutchRow.IsHatched
                If ClutchRow.IsSpecialRequirementsNull Then mSpecialRequirements = Nothing Else mSpecialRequirements = ClutchRow.SpecialRequirements

                If ClutchRow.GetLastLaidDateRows.Length > 0 Then
                    Dim LastLaid As BirdRegistrationDataset.LastLaidDateRow = ClutchRow.GetLastLaidDateRows(0)
                    mIsLastLaidDateExact = LastLaid.IsExact
                    If LastLaid.IsLastLaidDate_textNull Then mLastLaidDate = Nothing Else mLastLaidDate = LastLaid.LastLaidDate_text
                End If

                For Each EggRow As BirdRegistrationDataset.EggRow In ClutchRow.GetEggRows
                    Dim NewEgg As ClutchEgg = AddEgg()
                    If Not NewEgg Is Nothing Then
                        NewEgg.Cloned = EggRow.Cloned
                        For Each EggRingRow As BirdRegistrationDataset.EggRingRow In EggRow.GetEggRingRows
                            Dim NewEggRing As IDMark = NewEgg.AddRing()
                            If Not NewEggRing Is Nothing Then
                                With NewEggRing
                                    .Mark = EggRingRow.IDMarkNumber
                                    .MarkType = EggRingRow.IDMarkType
                                    If Not EggRingRow.IsIDMarkFateNull Then .MarkFate = EggRingRow.IDMarkFate
                                End With
                            End If
                        Next EggRingRow
                    End If
                    Dim NewSpecimen As ClutchSpecimen = AddSpecimen()
                    'check to see if one is created
                    If Not NewSpecimen Is Nothing Then
                        SpecimenType.CreateSpecimen(CType(NewSpecimen, AdultSpecimenType), EggRow.GetEggSpecimenRows)
                        'say which egg it came from
                        NewSpecimen.Egg = NewEgg
                    End If
                Next EggRow
                'TODO: SCS - Please can you create a string property, in the Clutch class, which is set if any parent birds are listed in CITES Annex A.
                Dim Parents As BirdRegistrationDataset.ParentsDataTable = CType(clutch.DataSet, BirdRegistrationDataset).Parents
                If Not Parents Is Nothing AndAlso Parents.Count > 0 Then
                    For Each p As BirdRegistrationDataset.ParentsRow In Parents
                        For Each f As BirdRegistrationDataset.FatherRow In p.GetFatherRows
                            '                            f.
                        Next f
                        For Each m As BirdRegistrationDataset.MotherRow In p.GetMotherRows

                        Next m
                    Next p
                End If
            End If
        End Sub

        Friend Sub GetData(ByRef clutch As BirdRegistrationDataset.ClutchDataTable)
            If Not clutch Is Nothing Then
                Dim BirdDS As BirdRegistrationDataset = CType(clutch.DataSet, BirdRegistrationDataset)

                Dim ClutchRow As BirdRegistrationDataset.ClutchRow = clutch.NewClutchRow
                ClutchRow(clutch.Clutch_IdColumn) = 1
                ClutchRow.IsHatched = mIsHatched
                If Not mSpecialRequirements Is Nothing Then ClutchRow.SpecialRequirements = mSpecialRequirements Else ClutchRow.SetSpecialRequirementsNull()
                clutch.AddClutchRow(ClutchRow)

                'add the data to the laid date table
                Dim LastLaid As BirdRegistrationDataset.LastLaidDateRow = BirdDS.LastLaidDate.NewLastLaidDateRow()
                LastLaid.ClutchRow = ClutchRow
                If Not mLastLaidDate Is Nothing Then
                    LastLaid.LastLaidDate_text = CType(mLastLaidDate, Date)
                Else
                    LastLaid.SetLastLaidDate_textNull()
                End If
                LastLaid.IsExact = mIsLastLaidDateExact
                BirdDS.LastLaidDate.AddLastLaidDateRow(LastLaid)

                'add the eggs
                For Each Egg As ClutchEgg In mEggs
                    If Not Egg Is Nothing Then
                        'add the egg record
                        Dim EggRow As BirdRegistrationDataset.EggRow = BirdDS.Egg.NewEggRow
                        EggRow.ClutchRow = ClutchRow
                        EggRow.Cloned = Egg.Cloned
                        BirdDS.Egg.AddEggRow(EggRow)

                        'add the rings...
                        For Each Ring As IDMark In Egg.Rings
                            Dim RingRow As BirdRegistrationDataset.EggRingRow = BirdDS.EggRing.NewEggRingRow
                            RingRow.EggRow = EggRow
                            RingRow.IDMarkNumber = Ring.Mark
                            RingRow.IDMarkType = Ring.MarkType
                            If Not Ring.MarkFate Is Nothing AndAlso TypeOf Ring.MarkFate Is Int32 Then RingRow.IDMarkFate = CType(Ring.MarkFate, Int32) Else RingRow.SetIDMarkFateNull()
                            If Not Ring.FateReason Is Nothing AndAlso Ring.FateReason.Length > 0 Then RingRow.FateReason = Ring.FateReason Else RingRow.SetFateReasonNull()
                            BirdDS.EggRing.AddEggRingRow(RingRow)
                        Next Ring

                        If Not mSpecimens Is Nothing Then
                            For Each OldSpecimen As ClutchSpecimen In mSpecimens
                                If ReferenceEquals(OldSpecimen.Egg, Egg) Then
                                    'check to see if this specimen is ours
                                    'sort out the specimens
                                    If Not OldSpecimen.SpecimenType Is Nothing Then
                                        Dim SpecimenRow As BirdRegistrationDataset.EggSpecimenRow = BirdDS.EggSpecimen.NewEggSpecimenRow
                                        'reassociate the row
                                        SpecimenRow.EggRow = EggRow
                                        OldSpecimen.SpecimenType.UpdateSpecimen(SpecimenRow)
                                        BirdDS.EggSpecimen.Rows.Add(SpecimenRow)
                                    End If
                                End If
                            Next OldSpecimen
                        End If
                    End If
                Next Egg
            End If
        End Sub

        Public Property IsHatched() As Boolean
            Get
                Return mIsHatched
            End Get
            Set(ByVal Value As Boolean)
                mIsHatched = Value
            End Set
        End Property
        Private mIsHatched As Boolean

        Public Property LastLaidDate() As Object
            Get
                Return mLastLaidDate
            End Get
            Set(ByVal Value As Object)
                mLastLaidDate = Value
            End Set
        End Property
        Private mLastLaidDate As Object

        Public Property IsLastLaidDateExact() As Boolean
            Get
                Return mIsLastLaidDateExact
            End Get
            Set(ByVal Value As Boolean)
                mIsLastLaidDateExact = Value
            End Set
        End Property
        Private mIsLastLaidDateExact As Boolean

        Public Property SpecialRequirements() As String
            Get
                Return mSpecialRequirements
            End Get
            Set(ByVal Value As String)
                mSpecialRequirements = Value
            End Set
        End Property
        Private mSpecialRequirements As String

        Public Function AddEgg() As ClutchEgg   'MLD modified 6/1/5
            Dim len As Int32 = mEggs.Length
            ReDim Preserve mEggs(len)
            mEggs(len) = New ClutchEgg
            Return mEggs(len)
        End Function

        Public Property Eggs() As ClutchEgg()
            Get
                Return mEggs
            End Get
            Set(ByVal Value As ClutchEgg())
                mEggs = Value
            End Set
        End Property
        Private mEggs(-1) As ClutchEgg  'MLD modified 6/1/5 so that starts off empty, not Nothing

        Public Property CITESAnnexAMessage() As String
            Get
                If ShowMessage Then
                    Return AnnexAMessage
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal Value As String)
                'readonly property - this here for proxy only!
            End Set
        End Property

        Friend Overrides Function IsValid(ByVal addError As BirdRegistration.AddValidationErrorDelegate, ByVal ssoUserId As Int64, ByVal applicationId As Int32) As Boolean
            Dim ValidState As Boolean = MyBase.IsValid(addError, ssoUserId, applicationId)
            'managed in here as the the field needed to be made nilable in the XSD in order
            'to allow saves to occur before the user had entered all of the data.
            If Not (Not LastLaidDate Is Nothing AndAlso TypeOf LastLaidDate Is Date) Then
                addError(ValidationError.ValidationCodes.LastLaidDateMustBeEntered, Nothing)
                ValidState = False
            End If
            Dim MinEggs As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.EggMinOccurs), Int32)
            'Dim MaxEggs As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.EggMaxOccurs), Int32)
            If (Eggs Is Nothing AndAlso MinEggs > 0) OrElse _
               (Not Eggs Is Nothing AndAlso Eggs.Length < MinEggs) Then 'OrElse _
                'Not Eggs Is Nothing AndAlso Eggs.Length > MaxEggs Then
                Dim ReplaceVals As New Collections.Specialized.NameValueCollection
                ReplaceVals.Add("%1", MinEggs.ToString)
                'ReplaceVals.Add("%2", MaxEggs.ToString)
                addError(ValidationError.ValidationCodes.MustHaveXEggsInAClutch, ReplaceVals)
                ValidState = False
            End If
            If Common.IsInRole(ssoUserId, Common.RolesList.CaseOfficer) Then
                If Not Eggs Is Nothing AndAlso Eggs.Length > 0 AndAlso RingsPicked Then
                    Dim Counter As Int32 = 1
                    For Each ThisEgg As ClutchEgg In Eggs
                        ThisEgg.IsValid(Counter, addError)
                        Counter += 1
                    Next ThisEgg
                    Dim Errors As New ArrayList
                    ClutchEgg.CheckEggs(Eggs, Errors, applicationId)
                    'if there are any errors, add them to the list
                    If Errors.Count > 0 Then
                        For Each vError As ValidationError In Errors
                            addError(vError.ValidationID, vError.ExtraMessageInfo)
                        Next vError
                    End If
                End If
            End If
            Dim LastLaidDateNillable As Boolean = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.LastLaidDateNillable), Boolean)
            If Not LastLaidDateNillable AndAlso mLastLaidDate Is Nothing Then
                'Dim ReplaceVals As New Collections.Specialized.NameValueCollection
                addError(ValidationError.ValidationCodes.MustHaveLastLaidDate, Nothing)
                ValidState = False

            End If
            Return ValidState
        End Function

        'just for the UI to use
        Public Overrides Property RingsPicked() As Boolean
            Get
                Dim Result As Boolean = False
                If Not mEggs Is Nothing AndAlso mEggs.Length > 0 Then
                    Result = True
                    'check to see if it has cloned eggs
                    Dim ClonedEggCount As Int32 = 0
                    For Each SingleEgg As ClutchEgg In mEggs
                        If SingleEgg.Cloned Then
                            ClonedEggCount += 1
                        End If
                    Next SingleEgg
                    Dim HasClonedEggs As Boolean = ClonedEggCount > 0

                    If HasClonedEggs Then
                        Result = False
                        If ClonedEggCount = mEggs.Length Then
                            For Each SingleEgg As ClutchEgg In mEggs
                                If Not SingleEgg.Rings Is Nothing AndAlso SingleEgg.Rings.Length > 0 Then
                                    Result = True
                                    Exit For
                                End If
                            Next SingleEgg
                        Else
                            'has a mixture of clone and not cloned
                            For Each SingleEgg As ClutchEgg In mEggs
                                If SingleEgg.Cloned Then
                                    If Not SingleEgg.Rings Is Nothing AndAlso SingleEgg.Rings.Length > 0 Then
                                        Result = True
                                        Exit For
                                    End If
                                End If
                            Next SingleEgg
                        End If
                    Else
                        For Each SingleEgg As ClutchEgg In mEggs
                            If SingleEgg.Rings Is Nothing OrElse SingleEgg.Rings.Length = 0 Then
                                Result = False
                                Exit For
                            End If
                        Next SingleEgg
                    End If
                End If
                Return Result
            End Get
            Set(ByVal Value As Boolean)
                'mRingsPicked = Value
            End Set
        End Property
        'Private mRingsPicked As Boolean
    End Class
End Namespace

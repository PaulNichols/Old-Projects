Namespace Application.Bird.Registration
    <Serializable()> _
    Public Class ClutchEgg
        Public Sub New()
        End Sub

        Public Function AddRing() As IDMark     'MLD modified 10/1/5
            Dim len As Int32 = mRings.Length
            ReDim Preserve mRings(len)
            mRings(len) = New IDMark
            Return mRings(len)
        End Function

        Public Property Rings() As IDMark()
            Get
                Return mRings
            End Get
            Set(ByVal Value As IDMark())
                mRings = Value
            End Set
        End Property
        Private mRings(-1) As IDMark  'MLD modified 10/1/5 so that starts off empty, not Nothing

        Public Function CreateSpecimenFrom(ByVal currentClutch As Clutch) As ClutchSpecimen
            Return CreateSpecimenFrom(currentClutch, Me)
        End Function

        Public Shared Function CreateSpecimenFrom(ByVal currentClutch As Clutch, ByVal egg As ClutchEgg) As ClutchSpecimen
            Dim newSpecimen As ClutchSpecimen = currentClutch.AddSpecimen()
            newSpecimen.Egg = egg
            Return newSpecimen
        End Function

        Friend Function GetSpecimen(ByVal specimens() As AdultSpecimenType) As ClutchSpecimen
            Return GetSpecimen(specimens, Me)
        End Function

        Friend Shared Function GetSpecimen(ByVal specimens() As AdultSpecimenType, ByVal egg As ClutchEgg) As ClutchSpecimen
            Dim Result As ClutchSpecimen = Nothing
            For Each Specimen As AdultSpecimenType In specimens
                If TypeOf Specimen Is ClutchSpecimen Then
                    If ReferenceEquals(CType(Specimen, ClutchSpecimen).Egg, egg) Then
                        Result = CType(Specimen, ClutchSpecimen)
                        Exit For
                    End If
                End If
            Next Specimen
            Return Result
        End Function

        Friend Shared Sub SetSpecimenForEgg(ByVal specimens() As AdultSpecimenType, ByVal specimen As ClutchSpecimen, ByVal egg As ClutchEgg)
            Dim ClucthSpec As ClutchSpecimen = GetSpecimen(specimens, egg)
            If Not ClucthSpec Is Nothing Then
                ClucthSpec = specimen
            End If
        End Sub

        Friend Function IsValid(ByVal ringNumber As Int32, ByVal addError As BirdRegistration.AddValidationErrorDelegate) As Boolean
            Dim ValidState As Boolean = True
            Dim MinRings As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.EggMinOccurs), Int32)
            Dim MaxRings As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.EggMaxOccurs), Int32)
            If (Rings Is Nothing AndAlso MinRings > 0) OrElse _
               (Not Rings Is Nothing AndAlso Rings.Length < MinRings) OrElse _
               Not Rings Is Nothing AndAlso Rings.Length > MaxRings Then
                Dim ReplaceVals As New Collections.Specialized.NameValueCollection
                ReplaceVals.Add("%1", MinRings.ToString)
                ReplaceVals.Add("%2", ringNumber.ToString)
                ReplaceVals.Add("%3", MaxRings.ToString)
                addError(ValidationError.ValidationCodes.MustHaveXRingsForAnEgg, ReplaceVals)
                ValidState = False
            End If
            Return ValidState
        End Function

        Public Shared Function AreEggsValid(ByVal birdReg As BirdRegistration) As BirdRegistration
            'initialise the error object before we start
            birdReg.ValidationErrors = Nothing

            'need a collection to store any errors in
            Dim ErrorList As New ArrayList

            Dim ErrorsFound As Boolean = False
            'is the the right application type!
            If birdReg.RegApplicationType <> RegistrationApplicationType.Clutch Then
                Dim List As New Collections.Specialized.NameValueCollection
                List.Add("%1", "clutch")
                ErrorList.Add(New ValidationError(ValidationError.ValidationCodes.IncorrectApplicationType, List))
            Else
                'we can only be here if we have a correct application type
                Dim Clutch As Clutch = CType(birdReg.RegistrationApplication, Clutch)
                'firstly make sure we have eggs
                If Clutch.Eggs Is Nothing OrElse Clutch.Eggs.Length = 0 Then
                    ErrorList.Add(New ValidationError(ValidationError.ValidationCodes.MustHaveEggsInAClutch))
                Else
                    'we have eggs, so check rings
                    CheckEggs(Clutch.Eggs, ErrorList, birdReg.ApplicationId)
                End If
            End If

            'do we have any errors?
            If ErrorList.Count > 0 Then
                'set a new message header
                birdReg.ValidationErrors = New ValidationManager(ValidationManager.ValidationTitles.EggValidationFailed)
                'loop through adding the errors
                For Each Err As ValidationError In ErrorList
                    birdReg.ValidationErrors.AddError(Err)
                Next Err
            End If

            Return birdReg
        End Function

        Friend Shared Sub CheckEggs(ByVal eggs As ClutchEgg(), ByRef errorList As ArrayList, ByVal applicationId As Int32)
            Dim EggNumber As Int32 = 0
            Dim Cloned As Boolean = True
            For Each SingleEgg As ClutchEgg In eggs
                EggNumber += 1
                If Not SingleEgg.Cloned AndAlso Cloned Then Cloned = False

                'dont check for rings on an egg that is produced from a cloned application
                If (SingleEgg.Rings Is Nothing OrElse SingleEgg.Rings.Length = 0) AndAlso Not SingleEgg.Cloned Then
                    Dim List As New Collections.Specialized.NameValueCollection
                    List.Add("%1", EggNumber.ToString)
                    errorList.Add(New ValidationError(ValidationError.ValidationCodes.MustHaveRingsForAnEgg, List))
                Else
                    'we have rings - make sure they are valid and set any types
                    Dim RingNumber As Int32 = 0
                    For Each SingleRing As IDMark In SingleEgg.Rings
                        RingNumber += 1
                        If SingleRing.Mark Is Nothing OrElse SingleRing.Mark.Length = 0 Then
                            Dim List As New Collections.Specialized.NameValueCollection
                            List.Add("%1", RingNumber.ToString)
                            List.Add("%2", EggNumber.ToString)
                            errorList.Add(New ValidationError(ValidationError.ValidationCodes.RingOnEggNeedsValue, List))
                        Else
                            'it has a value, but is it valid?
                            Dim NewMarkType As Int32 = SingleRing.GetMarkType()
                            If NewMarkType > 0 Then
                                'it is valid, so the new mark type will have been updated
                                'automatically by GetMarkType()
                                'As it is valid, we need to check if it has been used elsewhere
                                Dim SpecMarksService As DataObjects.Service.SpecimenIDMarkService = DataObjects.Entity.SpecimenIDMark.ServiceObject
                                Dim SpecMarks As DataObjects.EntitySet.SpecimenIDMarkSet = SpecMarksService.GetByIndex_IdMarkAndType(SingleRing.Mark, SingleRing.MarkType)
                                Dim DuplicatesFound As Boolean = Not SpecMarks Is Nothing AndAlso SpecMarks.Count > 0
                                If Not DuplicatesFound Then
                                    'check to see if another ring has the same values (a non-db ring)
                                    For Each SingleEgg2 As ClutchEgg In eggs
                                        For Each SingleRing2 As IDMark In SingleEgg2.Rings
                                            If Not ReferenceEquals(SingleRing, SingleRing2) Then
                                                'don't check my own ring!
                                                If SingleRing2.MarkType = SingleRing.MarkType AndAlso _
                                                SingleRing2.Mark = SingleRing.Mark Then
                                                    ' another duplicate!
                                                    DuplicatesFound = True
                                                    Exit For
                                                End If
                                            End If
                                        Next SingleRing2
                                    Next SingleEgg2
                                End If
                                If Not DuplicatesFound Then
                                    'still not found, now try the XML
                                    Dim SearchInfo As DataObjects.EntitySet.RingRequestSearchSet = BirdRegistrationSearch.GetSearchInfo(BirdRegistrationSearch.RegistrationSearchTypes.IDMarkAndType, BirdRegistrationSearch.GetConcatenatedMarkInfo(SingleRing))
                                    If Not SearchInfo Is Nothing Then
                                        For Each SearchItem As DataObjects.Entity.RingRequestSearch In SearchInfo
                                            'make sure it's not my application
                                            If SearchItem.RingRequestId <> applicationId Then
                                                'we've found it
                                                DuplicatesFound = True
                                                Exit For
                                            End If
                                        Next SearchItem
                                    End If
                                End If

                                If DuplicatesFound Then
                                    'it has been used elsewhere
                                    Dim List As New Collections.Specialized.NameValueCollection
                                    List.Add("%1", RingNumber.ToString)
                                    List.Add("%2", SingleRing.Mark)
                                    List.Add("%3", EggNumber.ToString)
                                    errorList.Add(New ValidationError(ValidationError.ValidationCodes.RingOnEggIsUsed, List))
                                End If
                            Else
                                'it's invalid
                                Dim List As New Collections.Specialized.NameValueCollection
                                List.Add("%1", RingNumber.ToString)
                                List.Add("%2", SingleRing.Mark)
                                List.Add("%3", EggNumber.ToString)
                                errorList.Add(New ValidationError(ValidationError.ValidationCodes.RingOnEggNeedsInvalid, List))
                            End If
                        End If
                    Next SingleRing
                End If
            Next SingleEgg
            If Cloned Then
                'if all eggs are cloned we must have at least one ring!
                Dim RingCount As Int32 = 0
                For Each SingleEgg As ClutchEgg In eggs
                    RingCount += SingleEgg.Rings.Length
                Next SingleEgg
                'dont check for rings on an egg that is produced from a cloned application
                If RingCount = 0 Then
                    errorList.Add(New ValidationError(ValidationError.ValidationCodes.MustHaveAtLeastOneRingIfNoExtraEggs))
                End If
            End If
        End Sub

        Public Property Cloned() As Boolean
            Get
                Return mCloned
            End Get
            Set(ByVal Value As Boolean)
                mCloned = Value
            End Set
        End Property
        Private mCloned As Boolean
    End Class
End Namespace
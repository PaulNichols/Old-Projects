Namespace Application.Bird.Registration
    <Serializable()> _
    Public Class Parents
        Public Sub New()
            'must always have at least one mother so add it...
            'AddMother()
        End Sub

        Public Function AddMother() As Parent   'MLD 11/1/5 modified (again!)
            Dim len As Int32 = mMother.Length
            ReDim Preserve mMother(len)
            mMother(len) = New Parent(Application.GenderType.Female)
            Return mMother(len)
        End Function

        Public Function AddFather() As Parent   'MLD 11/1/5 modified (again!)
            Dim len As Int32 = mFather.Length
            ReDim Preserve mFather(len)
            mFather(len) = New Parent(Application.GenderType.Male)
            Return mFather(len)
        End Function

        Private Const SPECIMENID As String = "SpecimenID"
        Private Const ISAMENDED As String = "IsAmended"
        Private Const COMMONNAME As String = "CommonName"
        Private Const SCIENTIFICNAME As String = "ScientificName"
        Private Const HATCHDATE As String = "HatchDate"
        Private Const ISHATCHDATEEXACT As String = "IsHatchDateExact"
        Private Const AGESTATUS As String = "AgeStatus"
        Private Const GENDER As String = "Gender"
        Private Const REGDOCREF As String = "RegistrationDocumentReference"
        Private Const ARTICLE10REF As String = "Article10Reference"
        Private Const FATECODE As String = "FateCode"
        Private Const IDMARKTYPE As String = "IDMarkType"
        Private Const IDMARKNUMBER As String = "IDMarkNumber"
        Private Const IDMARKFATE As String = "IDMarkFate"
        Friend Function AddFather(ByVal fatherSpecimenRows() As DataRow, ByVal fatherIDMarks() As DataRow) As Parent
            Dim Success As Parent = AddFather()
            If Not Success Is Nothing Then
                AddParentInt(Success, fatherSpecimenRows, fatherIDMarks)
            End If
            Return Success
        End Function

        Friend Function AddMother(ByVal motherSpecimenRows() As DataRow, ByVal motherIDMarks() As DataRow) As Parent
            Dim Success As Parent = AddMother()
            If Not Success Is Nothing Then
                AddParentInt(Success, motherSpecimenRows, motherIDMarks)
            End If
            Return Success
        End Function

        Private Function AddParentInt(ByVal parent As Parent, ByVal parentSpecimenRows() As DataRow, ByVal parentIDMarks() As DataRow) As Parent
            If Not parent Is Nothing Then
                For Each FatherItem As DataRow In parentSpecimenRows
                    With FatherItem
                        If Not .IsNull(SPECIMENID) AndAlso CType(.Item(SPECIMENID), Int32) > 0 Then parent.SpecimenId = CType(.Item(SPECIMENID), Int32)
                        parent.IsAmended = CType(.Item(ISAMENDED), Boolean)
                        parent.CommonName = .Item(COMMONNAME).ToString
                        parent.ScientificName = .Item(SCIENTIFICNAME).ToString
                        If Not .IsNull(HATCHDATE) Then parent.HatchDate = CType(.Item(HATCHDATE), Date)
                        If Not .IsNull(ISHATCHDATEEXACT) Then parent.IsHatchDateExact = .Item(ISHATCHDATEEXACT)
                        If Not .IsNull(AGESTATUS) Then parent.AgeStatusId = CType(.Item(AGESTATUS), Int32)
                        parent.SetGender(CType(.Item(GENDER), Char))
                        If Not .IsNull(REGDOCREF) Then parent.RegistrationDocumentReference = .Item(REGDOCREF).ToString
                        If Not .IsNull(ARTICLE10REF) Then parent.Article10Reference = .Item(ARTICLE10REF).ToString
                        If Not .IsNull(FATECODE) Then parent.FateCode = .Item(FATECODE)
                    End With
                    Exit For
                Next FatherItem
                For Each FatherIdMarkItem As DataRow In parentIDMarks
                    Dim NewMark As IDMark = parent.AddMarks()
                    If Not NewMark Is Nothing Then
                        With FatherIdMarkItem
                            NewMark.MarkType = CType(.Item(IDMARKTYPE), Int32)
                            NewMark.Mark = .Item(IDMARKNUMBER).ToString
                            If Not .IsNull(IDMARKFATE) Then NewMark.MarkFate = .Item(IDMARKFATE)
                        End With
                    End If
                Next FatherIdMarkItem
            End If
        End Function

        Friend Function AddParent(ByVal father As BirdRegistrationDataset.FatherRow) As Parent
            Dim Success As Parent = AddFather()
            If Not Success Is Nothing Then
                For Each FatherItem As BirdRegistrationDataset.FatherSpecimenRow In father.GetFatherSpecimenRows
                    With FatherItem
                        If Not .IsSpecimenIDNull AndAlso .SpecimenID > 0 Then Success.SpecimenId = .SpecimenID
                        Success.IsAmended = .IsAmended
                        Success.CommonName = .CommonName
                        Success.ScientificName = .ScientificName
                        If Not .IsHatchDateNull Then Success.HatchDate = .HatchDate
                        If Not .IsIsHatchDateExactNull Then Success.IsHatchDateExact = .IsHatchDateExact
                        If Not .IsAgeStatusIdNull() Then Success.AgeStatusId = .AgeStatusId
                        Success.SetGender(.Gender)
                        If Not .IsRegistrationDocumentReferenceNull() Then Success.RegistrationDocumentReference = .RegistrationDocumentReference
                        If Not .IsArticle10ReferenceNull() Then Success.Article10Reference = .Article10Reference
                        If Not .IsFateCodeNull() Then Success.FateCode = .FateCode
                    End With
                    Exit For
                Next FatherItem
                For Each FatherIdMarkItem As BirdRegistrationDataset.FatherIDMarkRow In father.GetFatherIDMarkRows
                    Dim NewMark As IDMark = Success.AddMarks()
                    If Not NewMark Is Nothing Then
                        With FatherIdMarkItem
                            NewMark.MarkType = .IDMarkType
                            NewMark.Mark = .IDMarkNumber
                            If Not .IsIDMarkFateNull Then NewMark.MarkFate = .IDMarkFate
                        End With
                    End If
                Next FatherIdMarkItem
            End If
            Return Success
        End Function

        Friend Function AddParent(ByVal mother As BirdRegistrationDataset.MotherRow) As Parent
            Dim Success As Parent = AddMother()
            If Not Success Is Nothing Then
                For Each MotherItem As BirdRegistrationDataset.MotherSpecimenRow In mother.GetMotherSpecimenRows
                    With MotherItem
                        If Not .IsSpecimenIDNull AndAlso .SpecimenID > 0 Then Success.SpecimenId = .SpecimenID
                        Success.IsAmended = .IsAmended
                        Success.CommonName = .CommonName
                        Success.ScientificName = .ScientificName
                        If Not .IsHatchDateNull Then Success.HatchDate = .HatchDate
                        If Not .IsIsHatchDateExactNull Then Success.IsHatchDateExact = .IsHatchDateExact
                        If Not .IsAgeStatusIdNull Then Success.AgeStatusId = .AgeStatusId
                        Success.SetGender(.Gender)
                        If Not .IsRegistrationDocumentReferenceNull() Then Success.RegistrationDocumentReference = .RegistrationDocumentReference
                        If Not .IsArticle10ReferenceNull() Then Success.Article10Reference = .Article10Reference
                        If Not .IsFateCodeNull() Then Success.FateCode = .FateCode
                    End With
                    Exit For
                Next MotherItem
                For Each MotherIdMarkItem As BirdRegistrationDataset.MotherIDMarkRow In mother.GetMotherIDMarkRows
                    Dim NewMark As IDMark = Success.AddMarks()
                    If Not NewMark Is Nothing Then
                        With MotherIdMarkItem
                            NewMark.MarkType = .IDMarkType
                            NewMark.Mark = .IDMarkNumber
                            If Not .IsIDMarkFateNull Then NewMark.MarkFate = .IDMarkFate
                        End With
                    End If
                Next MotherIdMarkItem
            End If
            Return Success
        End Function

        Public Function AddParent(ByVal birdSearchResult As Search.BirdSearchResult, ByVal gender As Application.GenderType) As Parent
            Dim Success As Parent = Nothing

            'only add when we have searched in case we don't find any results
            If gender = Application.GenderType.Female Then
                Success = AddMother()
            Else
                Success = AddFather()
            End If


            If Not Success Is Nothing Then
                With Success
                    'setting the gender may seem odd, but we may be adding a female to a male
                    'as it is read only, call the internal setting property mechanism
                    .SpecimenId = birdSearchResult.Id
                    .SetGender(birdSearchResult.Gender)
                    .CommonName = birdSearchResult.CommonName
                    .ScientificName = birdSearchResult.ScientificName

                    If birdSearchResult.HatchDateExact.Length > 0 Then
                        'leaves it as null if nothing set - otherwise set to true/false
                        .IsHatchDateExact = (String.Compare(birdSearchResult.HatchDateExact, "y", True) = 0)
                    End If

                    Try
                        .HatchDate = Date.Parse(birdSearchResult.HatchDateExact)
                    Catch ex As Exception
                        .HatchDate = Nothing
                    End Try

                    .Article10Reference = birdSearchResult.A10Ref
                    .RegistrationDocumentReference = birdSearchResult.RegDocRef
                    If birdSearchResult.FateId > 0 Then
                        .FateCode = birdSearchResult.FateId
                    End If
                End With

                'add birdSearchResult as first IDMark
                'loop through all IDMarks for bird add all other IDs that are
                'not birdSearchResult

                'create a basic id mark which is the search result one.
                'it gets populated completely when we iterate through the other id marks
                Dim FirstIdMark As IDMark = Success.AddMarks()
                FirstIdMark.Mark = birdSearchResult.IDMarkNumber

                'get the id marks
                Dim Link As DataObjects.EntitySet.SpecimenIDMarkSet = DataObjects.Entity.SpecimenIDMark.GetForSpecimen(Success.SpecimenId)
                If Not Link Is Nothing AndAlso Link.Count > 0 Then
                    For Each LinkItem As DataObjects.Entity.SpecimenIDMark In Link
                        Dim IdMarkTypeString As String = String.Empty
                        'Get the marks type to see if it is the same as our first marks type
                        Dim MarkType As DataObjects.Entity.IDMarkType = DataObjects.Entity.IDMarkType.GetById(LinkItem.IDMarkTypeId)
                        If Not MarkType Is Nothing Then
                            IdMarkTypeString = MarkType.Description
                        End If

                        'if we haven't set it and it equals the first id then update that instead of
                        'adding a new one
                        If FirstIdMark.MarkType = 0 AndAlso _
                           String.Compare(LinkItem.IdMark, FirstIdMark.Mark) = 0 AndAlso _
                           String.Compare(birdSearchResult.IDMarkType, IdMarkTypeString) = 0 Then
                            'update the first one with new data
                            'setting the marktype ensures we don't come back in here
                            FirstIdMark.MarkType = LinkItem.IDMarkTypeId
                            FirstIdMark.MarkFate = LinkItem.IdMarkFateId
                        Else
                            'create a new one instead
                            Dim AnotherIdMark As IDMark = Success.AddMarks()
                            AnotherIdMark.Mark = LinkItem.IdMark
                            AnotherIdMark.MarkType = LinkItem.IDMarkTypeId
                            'does it have fate?
                            If Not LinkItem.IsIdMarkFateIdNull Then
                                'set the date info
                                AnotherIdMark.MarkFate = LinkItem.IdMarkFateId
                            End If
                        End If
                    Next LinkItem
                ElseIf Not birdSearchResult.IDMarkType Is Nothing AndAlso birdSearchResult.IDMarkType.Length > 0 Then
                    Try
                        Dim Id As Int32 = CType(birdSearchResult.IDMarkType, Int32)
                        FirstIdMark.MarkType = CType(birdSearchResult.IDMarkType, Int32)
                        FirstIdMark.MarkFate = Nothing
                    Catch ex As Exception
                        'if you don't send me a number then tough!
                    End Try
                End If
            End If
            Return Success
        End Function

        Public Property Mother() As Parent()
            Get
                Return mMother
            End Get
            Set(ByVal Value As Parent())
                mMother = Value
            End Set
        End Property
        Private mMother(-1) As Parent  'MLD modified 11/1/5 so that starts off empty, not Nothing

        Public Property Father() As Parent()
            Get
                Return mFather
            End Get
            Set(ByVal Value As Parent())
                mFather = Value
            End Set
        End Property
        Private mFather(-1) As Parent  'MLD modified 11/1/5 so that starts off empty, not Nothing

        Friend Function IsValid(ByVal addError As BirdRegistration.AddValidationErrorDelegate, ByVal birdApplication As BirdRegistration) As Boolean
            Dim ValidState As Boolean = True
            'only a clutch must have parents
            If birdApplication.RegApplicationType = RegistrationApplicationType.Clutch Then
                Dim MinMother As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.MotherMinOccurs), Int32)
                'Dim MaxMother As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.MotherMaxOccurs), Int32)
                If (mMother Is Nothing AndAlso MinMother > 0) OrElse _
                   (Not mMother Is Nothing AndAlso mMother.Length < MinMother) Then ' OrElse _
                    'Not mMother Is Nothing AndAlso mMother.Length > MaxMother Then
                    Dim ReplaceVals As New Collections.Specialized.NameValueCollection
                    ReplaceVals.Add("%1", MinMother.ToString)
                    'ReplaceVals.Add("%2", MaxMother.ToString)
                    addError(ValidationError.ValidationCodes.MustHaveXMother, ReplaceVals)
                    ValidState = False
                End If
            End If
            If Not mMother Is Nothing AndAlso mMother.Length > 0 Then
                Dim MinIdMarks As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.MotherIdMarkMinOccurs), Int32)
                Dim MaxIdMarks As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.MotherIdMarkMaxOccurs), Int32)

                For Each par As Parent In mMother
                    If (par.IdMarks Is Nothing AndAlso par.IdMarks.Length > 0) OrElse _
                       (Not par.IdMarks Is Nothing AndAlso par.IdMarks.Length < MinIdMarks) OrElse _
                       Not par.IdMarks Is Nothing AndAlso par.IdMarks.Length > MaxIdMarks Then
                        Dim ReplaceVals As New Collections.Specialized.NameValueCollection
                        ReplaceVals.Add("%1", MinIdMarks.ToString)
                        ReplaceVals.Add("%2", par.SpecimenId.ToString)
                        ReplaceVals.Add("%3", MaxIdMarks.ToString)
                        addError(ValidationError.ValidationCodes.MustHaveXIdMarks, ReplaceVals)
                        ValidState = False
                    End If
                Next par
            End If

            'only a clutch must have parents
            Dim HasParents As Boolean = False
            If birdApplication.RegApplicationType = RegistrationApplicationType.Clutch Then
                HasParents = True
                Dim MinFather As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.FatherMinOccurs), Int32)
                'Dim MaxFather As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.FatherMaxOccurs), Int32)
                If (mFather Is Nothing AndAlso MinFather > 0) OrElse _
                   (Not mFather Is Nothing AndAlso mFather.Length < MinFather) Then ' OrElse _
                    'Not mFather Is Nothing AndAlso mFather.Length > MaxFather Then
                    Dim ReplaceVals As New Collections.Specialized.NameValueCollection
                    ReplaceVals.Add("%1", MinFather.ToString)
                    'ReplaceVals.Add("%2", MaxFather.ToString)
                    addError(ValidationError.ValidationCodes.MustHaveXFather, ReplaceVals)
                    ValidState = False
                End If
            End If
            If Not mFather Is Nothing AndAlso mFather.Length > 0 Then
                HasParents = True
                Dim MinIdMarks As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.FatherIdMarkMinOccurs), Int32)
                Dim MaxIdMarks As Int32 = CType(Application.ApplicationUtils.GetInfoFromBirdRegistrationXSD(ApplicationUtils.BirdRegistrationXSDType.FatherIdMarkMaxOccurs), Int32)

                For Each par As Parent In mFather
                    If (par.IdMarks Is Nothing AndAlso par.IdMarks.Length > 0) OrElse _
                       (Not par.IdMarks Is Nothing AndAlso par.IdMarks.Length < MinIdMarks) OrElse _
                       Not par.IdMarks Is Nothing AndAlso par.IdMarks.Length > MaxIdMarks Then
                        Dim ReplaceVals As New Collections.Specialized.NameValueCollection
                        ReplaceVals.Add("%1", MinIdMarks.ToString)
                        ReplaceVals.Add("%2", par.SpecimenId.ToString)
                        ReplaceVals.Add("%3", MaxIdMarks.ToString)
                        addError(ValidationError.ValidationCodes.MustHaveXIdMarks, ReplaceVals)
                        ValidState = False
                    End If
                Next par
            End If

            If HasParents Then
                ValidAgainstParents(mFather, ValidState, addError, birdApplication)
                ValidAgainstParents(mMother, ValidState, addError, birdApplication)
            End If

            Return ValidState
        End Function

        Friend Function ValidAgainstParents(ByVal parents As Registration.Parent(), ByRef validState As Boolean, ByVal addError As BirdRegistration.AddValidationErrorDelegate, ByVal birdApplication As BirdRegistration) As Boolean
            'load all addresses for this party
            Dim Addresses As ArrayList = GetAddressesForParty(birdApplication.PartyId)

            If Not mFather Is Nothing AndAlso mFather.Length > 0 Then
                For Each par As Parent In mFather
                    'if the id is not set then it's a new one
                    If par.SpecimenId > 0 Then
                        'only necessary if the gender has been previously defined
                        Dim NewSpecimen As New DataObjects.Entity.Specimen(par.SpecimenId, Nothing)
                        If Not NewSpecimen Is Nothing Then
                            If par.Gender <> GenderType.Unknown Then
                                'load the specimen
                                'load the partys for this specimen
                                Dim PartySpecService As DataObjects.Service.ParentSpecimenService = DataObjects.Entity.ParentSpecimen.ServiceObject()
                                Dim PartySpecs As DataObjects.EntitySet.ParentSpecimenSet = PartySpecService.GetForSpecimenIDSpecimen(par.SpecimenId)
                                If Not PartySpecs Is Nothing AndAlso PartySpecs.Count > 0 Then
                                    'check to see if it's registered at the keepers address(s) or
                                    'if it is registered to the keeper
                                    Dim AddressFound As Boolean = False
                                    For Each PartySpec As DataObjects.Entity.PartySpecimen In PartySpecs
                                        If Addresses.Contains(PartySpec.AddressId) AndAlso _
                                           PartySpec.RoleType = Party.BOPartySpecimen.Role.Keeper AndAlso _
                                           PartySpec.IsEndDateNull Then
                                            AddressFound = True
                                            'check to see if the gender is different
                                            If par.CompareGender(NewSpecimen.GenderId) Then
                                                addError(ValidationError.ValidationCodes.GenderOfBirdIsInconsistent, Nothing)
                                                validState = False
                                            End If
                                        End If
                                    Next PartySpec
                                    If Not AddressFound Then
                                        addError(ValidationError.ValidationCodes.PermanentPossessionInfoRequired, Nothing)
                                        validState = False
                                    End If
                                End If
                            Else
                                'the sex has changed
                                If par.CompareGender(NewSpecimen.GenderId) Then
                                    addError(ValidationError.ValidationCodes.GenderHasNotBeenPreviouslyConfirmed, Nothing)
                                    validState = False
                                End If
                            End If
                            If Not par.FateCode Is Nothing Then
                                Dim Config As New BO.BOConfiguration
                                Dim FateList As New ArrayList(Config.GetValue("BR2989SpecimenFates").ToString.Split(","c))
                                If FateList.Contains(par.FateCode.ToString) Then
                                    'it has been fated (lost, released, died)
                                    addError(ValidationError.ValidationCodes.BirdHasBeenFated, Nothing)
                                    validState = False
                                End If
                            End If
                        End If
                    End If
                Next
            End If
        End Function

        Private Function GetAddressesForParty(ByVal partyId As Int32) As ArrayList
            Dim Result As New ArrayList

            Dim AddressesService As DataObjects.Service.PartyAddressService = DataObjects.Entity.PartyAddress.ServiceObject()
            Dim PartyAddresses As DataObjects.EntitySet.PartyAddressSet = AddressesService.GetForParty(partyId)
            If Not PartyAddresses Is Nothing AndAlso PartyAddresses.Count > 0 Then
                For Each Add As DataObjects.Entity.PartyAddress In PartyAddresses
                    Result.Add(Add.AddressId)
                Next Add
            End If

            Return Result
        End Function
    End Class
End Namespace
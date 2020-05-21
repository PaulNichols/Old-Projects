Imports System.xml

Namespace Application.Bird.Registration

#Region " Enums "
    <Serializable()> _
    Public Enum RegistrationApplicationType
        Clutch
        Bred
        Found
        Imported
        Other
    End Enum

    <Serializable()> _
    Public Enum BirdRegLoadMode
        LoadByParty
        LoadByApplication
        CloneFromExistingApplication
    End Enum
#End Region

    <Serializable()> _
    Public Class BirdRegistration
        Inherits BOApplication
        'Implements IPersist ' BaseBO


        Friend Delegate Sub AddValidationErrorDelegate(ByVal [error] As ValidationError.ValidationCodes, ByVal extraMessageInfo As Collections.Specialized.NameValueCollection)

#Region " Constructors "
        Private mEnv As IEnvironmental = New SystemEnvironment

        Public Sub SetEnvironment(ByVal env As IEnvironmental)
            mEnv = env
        End Sub

        Public Sub New()
        End Sub

        Public Sub New(ByVal id As Int32, ByVal permissions As String)
            'this will fail when loading an application by party!
            MyClass.New(id, BirdRegLoadMode.LoadByApplication, -1, permissions)
        End Sub

        Public Sub New(ByVal id As Int32)
            'this will fail when loading an application by party!
            MyClass.New(id, BirdRegLoadMode.LoadByApplication, -1, String.Empty)
        End Sub

        Public Sub New(ByVal id As Int32, ByVal loadMode As BirdRegLoadMode, ByVal ssoUserId As Int64, ByVal permissions As String)
            'allow through if an internal request is being made
            If ssoUserId <> -1 Then
                'make sure the args are ok for external
                If ssoUserId <= 0 Then
                    Throw New ArgumentOutOfRangeException("ssoUserId")
                    'ElseIf appId <= 0 Then
                    '    Throw New ArgumentOutOfRangeException("appId")
                End If
            End If

            Select Case loadMode
                Case BirdRegLoadMode.LoadByParty
                    LoadByParty(id, ssoUserId)
                Case BirdRegLoadMode.LoadByApplication
                    LoadByApplication(id)
            End Select

            'only check if not internal
            If ssoUserId <> -1 Then
                'check that the loaded app can be viewed by the user
                CheckLoadIsOKForParty(ssoUserId, permissions)
            End If
        End Sub
#End Region

#Region " Methods "

#Region " Load "
        Private Sub LoadByParty(ByVal partyId As Int32, ByVal ssoUserId As Int64)
            'mParty = partyId
            'Create a brand new application
            Dim NewRingApp As DataObjects.Entity.RingApplication = CreateApplication(ssoUserId)
            LoadByApplication(NewRingApp) 'CType(DataObjects.Entity.RingApplication.ServiceObject.GetByIndex_UniqueApplicationId(NewAppId).GetEntity(0), DataObjects.Entity.RingApplication))
            PostCreateApplicationRecord(ssoUserId)
            Me.PartyId = partyId
        End Sub

        Private Function CreateApplication(ByVal ssoUserId As Int64) As DataObjects.Entity.RingApplication
            Dim AppMethodId As Int32
            If ssoUserId = 0 OrElse Common.IsInRole(ssoUserId, Common.RolesList.Customer) Then
                AppMethodId = ReportLimis.BOLimisMethod.MethodType.Internet
            Else
                AppMethodId = ReportLimis.BOLimisMethod.MethodType.Post
            End If
            ReceivedDate = Date.Today
            ApplicationMethodId = AppMethodId
            Dim NewApp As DataObjects.Entity.Application = DataObjects.Entity.Application.Insert(Date.Today, False, AppMethodId, Date.Now, Nothing, False, False, Nothing, Nothing, ssoUserId, False, ReceivedDate, Nothing, Nothing, Nothing, Nothing, Nothing, Application.ApplicationTypes.BirdAdult, Nothing, Nothing)
            Dim RingApp As DataObjects.Entity.RingApplication = DataObjects.Entity.RingApplication.Insert(NewApp.ApplicationId, String.Empty)
            'mRegistrationCheckSum = RingApp.CheckSum
            'mRingApplicationId = RingApp.RingApplicationId
            MyBase.InitialiseApplication(NewApp, Nothing)
            Return RingApp
        End Function

        Private Sub LoadByApplication(ByVal applicationId As Int32)
            Dim RingAppSet As DataObjects.EntitySet.RingApplicationSet = DataObjects.Entity.RingApplication.ServiceObject.GetByIndex_UniqueApplicationId(applicationId)
            If RingAppSet Is Nothing OrElse RingAppSet.Count = 0 Then
                Throw New RecordDoesNotExist("Ring Application", applicationId)
            Else
                MyBase.InitialiseApplication(DataObjects.Entity.Application.ServiceObject().GetById(applicationId, Nothing), Nothing)
                LoadByApplication(CType(RingAppSet.GetEntity(0), DataObjects.Entity.RingApplication))
            End If
        End Sub

        Private Sub LoadByApplication(ByVal application As DataObjects.Entity.RingApplication)
            mRegistrationCheckSum = application.CheckSum
            mRingApplicationId = application.RingApplicationId

            'get the infomation from the xml
            Dim XML As String = application.ProvisionalXML
            'BirdRegDS = New BirdRegistrationDataset
            If Not XML Is Nothing AndAlso XML.Length > 0 Then
                Dim myXmlReader As New System.Xml.XmlTextReader(XML, XmlNodeType.Document, Nothing)
                Dim BirdRegDS As New BirdRegistrationDataset
                Try
                    'an older version only allowed 8000 chars so this may be badly formed
                    BirdRegDS.ReadXml(myXmlReader)
                Catch ex As System.Xml.XmlException
                    'it is badly formed, load with no data
                End Try
                FillObjects(BirdRegDS)
            End If
            ApplicationId = application.ApplicationId
            Dim ApplicationEntity As New DataObjects.Entity.Application(ApplicationId)
            If Not ApplicationEntity Is Nothing AndAlso Not ApplicationEntity.IsPaidDateNull Then
                PaidDate = ApplicationEntity.PaidDate
            End If
            ' Load the schema file.
            'doc.DataSet.ReadXmlSchema("C:\Visual Studio Projects\devPhoenix\PhoenixBO\Application\Birds\Registration\BirdRegistration.xsd")
        End Sub

        Friend Shared Shadows Function IsSubmitted(ByVal applicationId As Int32) As Boolean
            Dim AppRecord As New DataObjects.Entity.Application(applicationId)
            Dim Result As Boolean
            If AppRecord.Submitted Then
                'as it's been submitted - this process needs to load some of the information from the
                'database instead of from the XML
            End If
            Result = AppRecord.Submitted
            Return Result
        End Function

        Private Sub FillObjects(ByVal birdRegDS As BirdRegistrationDataset)
            'setup party info
            If Not birdRegDS.Party Is Nothing AndAlso birdRegDS.Party.Count > 0 Then
                Dim pr As BirdRegistrationDataset.PartyRow = birdRegDS.Party(0)
                mPartyId = pr.PartyID
                If Not pr.IsPartyAddressIDNull Then mPartyAddressId = pr.PartyAddressID Else mPartyAddressId = Nothing
                If Not pr.IsAddressReasonNull Then mAddressReason = pr.AddressReason Else mAddressReason = Nothing
            Else
                mPartyId = 0
                mPartyAddressId = Nothing
                mAddressReason = Nothing
            End If

            If Not birdRegDS.RingApplication Is Nothing AndAlso birdRegDS.RingApplication.Count > 0 Then
                Dim pr As BirdRegistrationDataset.RingApplicationRow = birdRegDS.RingApplication(0)
                ApplicationId = pr.ApplicationReference

                If Not pr.IsApplicationTypeNull Then mRegApplicationType = CType(System.Enum.Parse(GetType(RegistrationApplicationType), pr.ApplicationType.ToString), RegistrationApplicationType) Else mRegApplicationType = RegistrationApplicationType.Bred
                mApplicationMethodId = pr.ApplicationMethodId
                If Not pr.IsDORApplicationMethodIdNull Then mDORApplicationMethodId = pr.DORApplicationMethodId Else mDORApplicationMethodId = 0
                If Not pr.IsKeeperAcknowledgmentNull Then mKeeperAcknowledgment = pr.KeeperAcknowledgment Else mKeeperAcknowledgment = Nothing
                mIsInspectionRequired = pr.IsInspectionRequired
                If Not pr.IsInspectorDecisionMadeNull Then mInspectorDecisionMade = pr.InspectorDecisionMade Else mInspectorDecisionMade = False
                mApplicationStatus = CType(pr.ApplicationStatus, BOPermitInfo.PermitStatusTypes)
                PaymentStatus = CType(pr.PaymentStatus, Application.PaymentStatusTypes)
                If Not pr.IsAssignedToNull Then mAssignedTo = CType(pr.AssignedTo, Common.AssignedToList) Else mAssignedTo = Common.AssignedToList.All
                If Not pr.IsDORReceivedDateNull Then
                    mDORReceivedDate = pr.DORReceivedDate
                    mOrigDORReceivedDate = mDORReceivedDate
                Else
                    mDORReceivedDate = Nothing
                End If
                If Not pr.IsReceivedDateNull Then ReceivedDate = pr.ReceivedDate Else ReceivedDate = New Date(0)
                If Not pr.IsCancellationReasonNull Then mCancellationReason = pr.CancellationReason Else mCancellationReason = Nothing
                If Not pr.IsCancellationRefusalReasonNull Then mCancellationRefusalReason = pr.CancellationRefusalReason Else mCancellationRefusalReason = Nothing
                If Not pr.IsReasonForEggsButNoParentNull Then mReasonForEggsButNoParent = pr.ReasonForEggsButNoParent Else mReasonForEggsButNoParent = Nothing
                If Not pr.IsRefuseLetterReportIdNull Then mRefuseLetterReportId = pr.RefuseLetterReportId Else mRefuseLetterReportId = 0
                If Not pr.IsSubmittedDateNull Then mSubmittedDate = pr.SubmittedDate Else mSubmittedDate = Date.Today
                If Not pr.IsDeclineReasonNull Then mDeclineReason = pr.DeclineReason Else mDeclineReason = Nothing
                If Not pr.IsDORPrintJobIdNull Then mDORPrintJobId = pr.DORPrintJobId Else mDORPrintJobId = 0
                If Not pr.IsNextActionDateNull Then mNextActionDate = pr.NextActionDate Else mNextActionDate = Nothing
                If Not pr.IsRelatedRingApplicationNull Then mRelatedRingApplicationId = pr.RelatedRingApplication Else mRelatedRingApplicationId = 0
                If Not pr.IsSLAClockNull Then mSLAClock = pr.SLAClock Else mSLAClock = 0
                If Not pr.IsSLAStartNull Then mSLAStart = pr.SLAStart Else mSLAStart = Nothing
            Else
                mRegApplicationType = RegistrationApplicationType.Bred
                mApplicationMethodId = 0
                mDORApplicationMethodId = 0
                ApplicationId = 0
                mKeeperAcknowledgment = Nothing
                mIsInspectionRequired = True
                mInspectorDecisionMade = False
                mApplicationStatus = 0
                PaymentStatus = 0
                mRelatedRingApplicationId = 0
            End If
            If Not birdRegDS.Declaration Is Nothing AndAlso birdRegDS.Declaration.Count > 0 Then
                Dim pr As BirdRegistrationDataset.DeclarationRow = birdRegDS.Declaration(0)
                mSpecialRequirements = pr.SpecialRequirements
                mIsUnderEighteen = pr.IsUnderEighteen
                mSpecialPenalty = pr.SpecialPenalty
                mOtherAnimalOffence = pr.OtherAnimalOffence
                mTrueAndCorrect = pr.TrueAndCorrect
            Else
                mSpecialRequirements = String.Empty
                mIsUnderEighteen = False
                mSpecialPenalty = False
                mOtherAnimalOffence = False
                mTrueAndCorrect = False
            End If

            If Not birdRegDS.Conviction Is Nothing AndAlso birdRegDS.Conviction.Count > 0 Then
                For Each Conviction As BirdRegistrationDataset.ConvictionRow In birdRegDS.Conviction
                    Dim ConvictionObject As Conviction = AddConviction()
                    If Not ConvictionObject Is Nothing Then
                        ConvictionObject.Offence = Conviction.Offence
                        ConvictionObject.Court = Conviction.Court
                        ConvictionObject.ConvictionDate = Conviction._Date
                    End If
                Next Conviction
            End If

            Select Case mRegApplicationType
                Case RegistrationApplicationType.Imported
                    mTypeAdultImported = New AdultImported(birdRegDS.AdultImported)
                Case RegistrationApplicationType.Bred
                    mTypeAdultBred = New AdultBred(birdRegDS.AdultBredBird)
                Case RegistrationApplicationType.Clutch
                    mTypeClutch = New Clutch(birdRegDS.Clutch)
                Case RegistrationApplicationType.Found
                    mTypeAdultFound = New AdultFound(birdRegDS.AdultFound)
                Case RegistrationApplicationType.Other
                    mTypeAdultOther = New AdultOther(birdRegDS.AdultOtherBird)
            End Select

            'sort out the parent infomation
            If Not birdRegDS.Parents Is Nothing AndAlso birdRegDS.Parents.Count > 0 Then
                mParents = New Parents
                For Each Parent As BirdRegistrationDataset.ParentsRow In birdRegDS.Parents.Rows
                    For Each Father As BirdRegistrationDataset.FatherRow In Parent.GetFatherRows
                        mParents.AddParent(Father)
                    Next Father
                    For Each Mother As BirdRegistrationDataset.MotherRow In Parent.GetMotherRows
                        mParents.AddParent(Mother)
                    Next Mother
                Next Parent
            Else
                mParents = Nothing
            End If
        End Sub
#End Region

#Region " Save "
        Private Overloads Function Submit(ByVal ssoUserId As Int64) As Boolean
            'clear the error state
            ValidationErrors = Nothing
            'check the validation
            Dim ParentsValid As Boolean = Parents.IsValid(AddressOf AddValidationError, Me)
            Dim ApplicationValid As Boolean = RegistrationApplication.IsValid(AddressOf AddValidationError, ssoUserId, ApplicationId)
            If Not ParentsValid OrElse Not ApplicationValid Then
                'validation failed so the ValidationErrors object should ocntain errors
                'false indicate that the developer should look at this info.
                Return False
            Else
                'all is well
                If mApplicationStatus = ApplicationStatus.BeingInput_CaseOfficer OrElse _
                   mApplicationStatus = ApplicationStatus.BeingInput_Customer Then
                    'update the status
                    If mAssignedTo = Common.AssignedToList.CaseOfficer Then
                        MyBase.OwnerId = ssoUserId
                        mApplicationStatus = BOPermitInfo.PermitStatusTypes.ProgressAllowed
                    Else
                        mApplicationStatus = BOPermitInfo.PermitStatusTypes.Ring_Request_Submitted_By_Customer
                        mAssignedTo = Common.AssignedToList.CaseOfficer
                    End If
                End If
                'set the submitted date
                mSubmittedDate = Date.Now
                Return True
            End If
        End Function

        Public Function GetXML() As String
            Dim Result As String = String.Empty
            Dim BirdRegDS As BirdRegistrationDataset = FillDS()
            If Not BirdRegDS Is Nothing Then
                Dim sw As New IO.StringWriter
                BirdRegDS.WriteXml(sw)
                Result = sw.GetStringBuilder().ToString()
                ' dispose the object
                sw.Close()
                sw = Nothing
            End If
            Return Result
        End Function

        Public Shadows Function Save() As Application.Bird.Registration.BirdRegistration
            Dim SavedBird As Application.Bird.Registration.BirdRegistration = Nothing
            If Not MyBase.Save() Is Nothing Then
                UpdateSLAClock()
                Dim BirdRegDS As BirdRegistrationDataset = FillDS()
                If Not BirdRegDS Is Nothing Then
                    Dim sw As New IO.StringWriter
                    BirdRegDS.WriteXml(sw)
                    Dim XML As String = sw.GetStringBuilder().ToString()
                    ' dispose the object
                    sw.Close()
                    sw = Nothing
                    'ctype(DataObjects.Entity.RingApplication.ServiceObject.GetByIndex_UniqueApplicationId(NewAppId).GetEntity(0),DataObjects.Entity.RingApplication)

                    Dim NewRing As DataObjects.Entity.RingApplication = _
                        DataObjects.Entity.RingApplication.ServiceObject.Update(mRingApplicationId, ApplicationId, XML, mRegistrationCheckSum)

                    If Not NewRing Is Nothing Then
                        SavedBird = Me
                        'save went well, so update search info
                        BirdRegistrationSearch.UpdateSeachInfo(Me)
                        RegistrationCheckSum = NewRing.CheckSum
                        'update primary key
                        ApplicationId = NewRing.ApplicationId

                        'update the application record
                        Dim App As New DataObjects.Entity.Application(ApplicationId)
                        Dim NewAppType As Int32
                        If Me.RegApplicationType = RegistrationApplicationType.Clutch Then
                            NewAppType = Application.ApplicationTypes.BirdChick
                        Else
                            NewAppType = Application.ApplicationTypes.BirdAdult
                        End If
                        If App.ApplicationTypeId <> NewAppType Then
                            App.ApplicationTypeId = NewAppType
                            App.SaveChanges()
                        End If
                    End If
                End If
            End If
            Return SavedBird
        End Function

        Private Function FillDS() As BirdRegistrationDataset
            Dim BirdRegDS As New BirdRegistrationDataset
            If mPartyId > 0 Then
                Dim pr As BirdRegistrationDataset.PartyRow = BirdRegDS.Party.NewPartyRow()
                pr.PartyID = mPartyId
                BirdRegDS.Party.AddPartyRow(pr)

                'sort out the party address
                'it is assumed that you must have a party to have a party address
                If Not mPartyAddressId Is Nothing AndAlso TypeOf mPartyAddressId Is Int32 AndAlso CType(mPartyAddressId, Int32) > 0 Then
                    pr.PartyAddressID = CType(mPartyAddressId, Int32)
                End If

                If Not mAddressReason Is Nothing AndAlso TypeOf mAddressReason Is String AndAlso mAddressReason.ToString.Length > 0 Then
                    pr.AddressReason = mAddressReason.ToString
                End If
            End If

            Dim rar As BirdRegistrationDataset.RingApplicationRow = BirdRegDS.RingApplication.NewRingApplicationRow()
            rar.ApplicationType = System.Enum.GetName(GetType(RegistrationApplicationType), mRegApplicationType)
            rar.ApplicationMethodId = mApplicationMethodId
            If mDORApplicationMethodId > 0 Then rar.DORApplicationMethodId = mDORApplicationMethodId Else rar.SetDORApplicationMethodIdNull()

            If Not mDORReceivedDate Is Nothing AndAlso TypeOf mDORReceivedDate Is Date Then
                rar.DORReceivedDate = CType(mDORReceivedDate, Date)
                'If mOrigDORReceivedDate Is Nothing Then
                '    'wasn't set before but we are now setting the date.
                '    'This implies a status change so check the current status and update if required
                '    If mApplicationStatus = ApplicationStatus.Chick_DOR_Issued OrElse _
                '       mApplicationStatus = ApplicationStatus.Adult_DOR_Issued Then
                '        'the application is in the right status to be updated
                '        'the table's status is set below this point - essential!
                '        mApplicationStatus = ApplicationStatus.DOR_Returned
                '    End If
                'End If
            Else
                rar.SetDORReceivedDateNull()
            End If
            If ReceivedDate.Ticks > 0 Then rar.ReceivedDate = ReceivedDate Else rar.SetReceivedDateNull()
            If Not mCancellationReason Is Nothing Then rar.CancellationReason = mCancellationReason Else rar.SetCancellationReasonNull()
            If Not mCancellationRefusalReason Is Nothing Then rar.CancellationRefusalReason = mCancellationRefusalReason Else rar.SetCancellationRefusalReasonNull()
            If Not mReasonForEggsButNoParent Is Nothing Then rar.ReasonForEggsButNoParent = mReasonForEggsButNoParent Else rar.SetReasonForEggsButNoParentNull()
            rar.SubmittedDate = mSubmittedDate
            If Not mDeclineReason Is Nothing Then rar.DeclineReason = mDeclineReason Else rar.SetDeclineReasonNull()
            If mDORPrintJobId > 0 Then rar.DORPrintJobId = mDORPrintJobId Else rar.SetDORPrintJobIdNull()
            If Not mNextActionDate Is Nothing AndAlso TypeOf mNextActionDate Is Date Then rar.NextActionDate = CType(mNextActionDate, Date) Else rar.SetNextActionDateNull()
            If mRefuseLetterReportId > 0 Then rar.RefuseLetterReportId = mRefuseLetterReportId Else rar.SetRefuseLetterReportIdNull()
            rar.AssignedTo = CType(mAssignedTo, Int32)
            rar.ApplicationReference = ApplicationId
            If Not mKeeperAcknowledgment Is Nothing AndAlso TypeOf mKeeperAcknowledgment Is Boolean Then rar.KeeperAcknowledgment = CType(mKeeperAcknowledgment, Boolean) Else rar.SetKeeperAcknowledgmentNull()
            rar.IsInspectionRequired = mIsInspectionRequired
            rar.InspectorDecisionMade = mInspectorDecisionMade
            rar.ApplicationStatus = mApplicationStatus
            rar.PaymentStatus = PaymentStatus
            BirdRegDS.RingApplication.AddRingApplicationRow(rar)
            If mRelatedRingApplicationId > 0 Then rar.RelatedRingApplication = mRelatedRingApplicationId Else rar.SetRelatedRingApplicationNull()
            If mSLAStart.Ticks > 0 Then rar.SLAStart = mSLAStart Else rar.SetSLAStartNull()
            If mSLAClock > 0 Then rar.SLAClock = mSLAClock Else rar.SetSLAClockNull()

            rar = Nothing

            Dim dar As BirdRegistrationDataset.DeclarationRow = BirdRegDS.Declaration.NewDeclarationRow
            dar.SpecialRequirements = SpecialRequirements
            dar.IsUnderEighteen = mIsUnderEighteen
            dar.SpecialPenalty = mSpecialPenalty
            dar.OtherAnimalOffence = mOtherAnimalOffence
            dar.TrueAndCorrect = mTrueAndCorrect
            BirdRegDS.Declaration.AddDeclarationRow(dar)
            dar = Nothing

            If Not mConvictions Is Nothing AndAlso mConvictions.Length > 0 Then
                For Each Conviction As Conviction In mConvictions
                    Dim cr As BirdRegistrationDataset.ConvictionRow = BirdRegDS.Conviction.NewConvictionRow
                    With cr
                        .Court = Conviction.Court
                        .Offence = Conviction.Offence
                        ._Date = Conviction.ConvictionDate
                    End With
                    BirdRegDS.Conviction.AddConvictionRow(cr)
                Next Conviction
            End If

            If Not BirdRegDS.Conviction Is Nothing AndAlso BirdRegDS.Conviction.Count > 0 Then
                For Each Conviction As BirdRegistrationDataset.ConvictionRow In BirdRegDS.Conviction
                    Dim ConvictionObject As Conviction = AddConviction()
                    If Not ConvictionObject Is Nothing Then
                        ConvictionObject.Offence = Conviction.Offence
                        ConvictionObject.Court = Conviction.Court
                        ConvictionObject.ConvictionDate = Conviction._Date
                    End If
                Next Conviction
            End If

            Select Case mRegApplicationType
                Case RegistrationApplicationType.Imported
                    TypeAdultImported.GetData(BirdRegDS.AdultImported)
                Case RegistrationApplicationType.Bred
                    TypeAdultBred.GetData(BirdRegDS.AdultBredBird)
                Case RegistrationApplicationType.Clutch
                    TypeClutch.GetData(BirdRegDS.Clutch)
                Case RegistrationApplicationType.Found
                    TypeAdultFound.GetData(BirdRegDS.AdultFound)    'MLD 7/1/5 added
                Case RegistrationApplicationType.Other              'MLD 7/1/5 reinstated
                    TypeAdultOther.GetData(BirdRegDS.AdultOtherBird)    'MLD 7/1/5 added
            End Select

            'sort out the parent infomation
            If Not mParents Is Nothing AndAlso _
               (mParents.Father.Length > 0 OrElse mParents.Mother.Length > 0) Then  'MLD 11/1/5 simplified
                'add the parent base record
                Dim ParentsRow As BirdRegistrationDataset.ParentsRow = BirdRegDS.Parents.NewParentsRow()
                BirdRegDS.Parents.AddParentsRow(ParentsRow)

                For Each SpecimenFather As Parent In mParents.Father    'MLD 11/1/5 father row moved inside loop
                    Dim SpecimenFatherRow As BirdRegistrationDataset.FatherRow = BirdRegDS.Father.NewFatherRow
                    SpecimenFatherRow.ParentsRow = ParentsRow
                    BirdRegDS.Father.AddFatherRow(SpecimenFatherRow)
                    Dim SpecimenFatherRowItem As BirdRegistrationDataset.FatherSpecimenRow = BirdRegDS.FatherSpecimen.NewFatherSpecimenRow

                    SpecimenFatherRowItem.FatherRow = SpecimenFatherRow
                    SpecimenFather.UpdateSpecimen(SpecimenFatherRowItem)

                    BirdRegDS.FatherSpecimen.AddFatherSpecimenRow(SpecimenFatherRowItem)

                    For Each FatherMarks As IDMark In SpecimenFather.IdMarks
                        Dim FatherIdMarkRowItem As BirdRegistrationDataset.FatherIDMarkRow = BirdRegDS.FatherIDMark.NewFatherIDMarkRow
                        FatherIdMarkRowItem.FatherRow = SpecimenFatherRow
                        FatherMarks.PopulateIDMark(CType(FatherIdMarkRowItem, DataRow))
                        BirdRegDS.FatherIDMark.AddFatherIDMarkRow(FatherIdMarkRowItem)
                    Next FatherMarks
                Next SpecimenFather

                For Each SpecimenMother As Parent In mParents.Mother        'MLD 11/1/5 mother row moved inside loop
                    Dim SpecimenMotherRow As BirdRegistrationDataset.MotherRow = BirdRegDS.Mother.NewMotherRow
                    SpecimenMotherRow.ParentsRow = ParentsRow
                    BirdRegDS.Mother.AddMotherRow(SpecimenMotherRow)
                    Dim SpecimenMotherRowItem As BirdRegistrationDataset.MotherSpecimenRow = BirdRegDS.MotherSpecimen.NewMotherSpecimenRow

                    SpecimenMotherRowItem.MotherRow = SpecimenMotherRow
                    SpecimenMother.UpdateSpecimen(SpecimenMotherRowItem)

                    BirdRegDS.MotherSpecimen.AddMotherSpecimenRow(SpecimenMotherRowItem)

                    For Each MotherMarks As IDMark In SpecimenMother.IdMarks
                        Dim MotherIdMarkRowItem As BirdRegistrationDataset.MotherIDMarkRow = BirdRegDS.MotherIDMark.NewMotherIDMarkRow
                        MotherIdMarkRowItem.MotherRow = SpecimenMotherRow
                        MotherMarks.PopulateIDMark(CType(MotherIdMarkRowItem, DataRow))
                        BirdRegDS.MotherIDMark.AddMotherIDMarkRow(MotherIdMarkRowItem)
                    Next MotherMarks
                Next SpecimenMother
            Else
                mParents = Nothing
            End If
            'If Not BirdRegDS.Parents Is Nothing AndAlso BirdRegDS.Parents.Count > 0 Then
            '    mParents = New Parents
            '    For Each Parent As BirdRegistrationDataset.ParentsRow In BirdRegDS.Parents.Rows
            '        For Each Father As BirdRegistrationDataset.FatherRow In Parent.GetFatherRows
            '            mParents.AddParent(Father)
            '        Next Father
            '        For Each Mother As BirdRegistrationDataset.MotherRow In Parent.GetMotherRows
            '            mParents.AddParent(Mother)
            '        Next Mother
            '    Next Parent
            'Else
            '    mParents = Nothing
            'End If

            Return BirdRegDS
        End Function
#End Region

#Region " Search "
        Public Shared Function SearchForCancelledRingApplications() As Int32()
            Return BirdRegistrationSearch.GetRingRequestsId(BirdRegistrationSearch.RegistrationSearchTypes.ApplicationStatus, BOPermitInfo.PermitStatusTypes.Cancelled)
        End Function

        Public Shared Function SearchForRingApplicationsByStatus(ByVal statusId As BOPermitInfo.PermitStatusTypes) As Int32()
            Return BirdRegistrationSearch.GetRingRequestsId(BirdRegistrationSearch.RegistrationSearchTypes.ApplicationStatus, statusId)
        End Function

        Public Shared Function SearchForBirds(ByVal ssoUserId As Int64, ByVal regDocRef As String, ByVal partyId As Object) As Search.BirdSearchResult()
            Dim Party As Object = Nothing
            Dim StatusId As Object = Nothing
            'if the user is not a case officer, we need to load using party
            If Common.IsInRole(ssoUserId, Common.RolesList.Customer) Then
                If partyId Is Nothing OrElse CType(partyId, Int32) <= 0 Then    'MLD 8/2/5 fixed
                    'insist that a partyid is passed for non-case officers
                    Throw New ArgumentNullException("partyId")
                End If
                Party = partyId
                StatusId = BO.Party.BOPartySpecimen.Status.Current
            End If
            Return SearchForBirds(DataObjects.Entity.IDMarkType.GetBirdsForApplication(regDocRef, Party, StatusId, Nothing))
        End Function

        Public Shared Function SearchForBirds(ByVal markType As Int32, ByVal markNumber As String) As Search.BirdSearchResult()
            Return SearchForBirds(DataObjects.Entity.IDMarkType.GetBirdsForApplication(markType, markNumber))
        End Function

        Public Shared Function SearchForBirds(ByVal partyId As Int32, ByVal currentOnly As Boolean) As Search.BirdSearchResult()
            Dim StatusId As Int32
            If currentOnly Then
                StatusId = BO.Party.BOPartySpecimen.Status.Current
            Else
                StatusId = -1 'all
            End If
            Return SearchForBirds(DataObjects.Entity.IDMarkType.GetBirdsForApplication(partyId, StatusId))
        End Function

        Private Shared Function SearchForBirds(ByVal birdEntities As EnterpriseObjects.EntitySet) As Search.BirdSearchResult()
            If Not birdEntities Is Nothing AndAlso birdEntities.Count > 0 Then

                'Dim ReturnResults(birdEntities.Count - 1) As Search.BirdSearchResult
                Dim ReturnResults As New ArrayList
                Dim UniqueChecker As New ArrayList
                For Each Bird As EnterpriseObjects.Entity In birdEntities '  Index As Int32 = 0 To birdEntities.Count - 1
                    'Dim Bird As EnterpriseObjects.Entity = birdEntities.GetEntity(Index)
                    Dim ReturnResult As New Search.BirdSearchResult
                    '(Index) = New Search.BirdSearchResult
                    With ReturnResult
                        If Not Bird.IsNull(0) Then
                            Dim SpecimenId As Int32 = CType(Bird(0), Int32) 'SpecimenId
                            If Not UniqueChecker.Contains(SpecimenId) Then
                                'only add one if the specimen id is unique.
                                'the results are ordered using idmarktype permanence so the most
                                'permanent mark is in this list, no others.
                                .Id = SpecimenId
                                UniqueChecker.Add(SpecimenId)
                                If Not Bird.IsNull(1) Then .CommonName = Bird(1).ToString Else .CommonName = String.Empty
                                If Not Bird.IsNull(2) Then .ScientificName = Bird(2).ToString Else .ScientificName = String.Empty
                                If Not Bird.IsNull(3) Then .IDMarkType = Bird(3).ToString Else .IDMarkType = String.Empty
                                If Not Bird.IsNull(4) Then .IDMarkNumber = Bird(4).ToString Else .IDMarkNumber = String.Empty
                                If Not Bird.IsNull(5) AndAlso TypeOf Bird(5) Is Int32 Then .Gender = Application.Gender.GetGenderById(CType(Bird(5), Int32)) Else .Gender = String.Empty
                                If Not Bird.IsNull(6) Then .Fate = Bird(6).ToString Else .Fate = String.Empty
                                If Not Bird.IsNull(7) AndAlso TypeOf Bird(7) Is Date Then .HatchDate = CType(Bird(7), Date).ToShortDateString Else .HatchDate = String.Empty
                                If Not Bird.IsNull(8) Then .HatchDateExact = Bird(8).ToString Else .HatchDateExact = String.Empty
                                If Not Bird.IsNull(9) Then .RegDocRef = Bird(9).ToString Else .RegDocRef = String.Empty
                                If Not Bird.IsNull(10) Then .A10Ref = Bird(10).ToString Else .A10Ref = String.Empty
                                If Not Bird.IsNull(11) AndAlso TypeOf Bird(11) Is Int32 Then .FateId = CType(Bird(11), Int32) Else .FateId = 0
                                If Not Bird.IsNull(12) AndAlso TypeOf Bird(12) Is Date Then .DateAcquired = CType(Bird(12), Date) Else .DateAcquired = Nothing
                                If Not Bird.IsNull(15) AndAlso TypeOf Bird(15) Is String Then .OtherIdMarks = Bird(15).ToString Else .OtherIdMarks = String.Empty
                                If Not Bird.IsNull(16) AndAlso TypeOf Bird(16) Is String Then .OtherIdMarkTypes = Bird(16).ToString Else .OtherIdMarkTypes = String.Empty
                                If Not Bird.IsNull(18) AndAlso TypeOf Bird(18) Is Int32 Then .CITESSourceId = CType(Bird(18), Int32) Else .CITESSourceId = 0
                                If Not Bird.IsNull(19) AndAlso TypeOf Bird(19) Is Int32 Then .CITESSourceId2 = CType(Bird(19), Int32) Else .CITESSourceId2 = 0
                                If Not Bird.IsNull(20) AndAlso TypeOf Bird(20) Is Int32 Then .PermitId = CType(Bird(20), Int32) Else .PermitId = 0
                                ReturnResults.Add(ReturnResult)
                            End If
                        End If
                    End With
                Next Bird
                Return CType(ReturnResults.ToArray(GetType(Search.BirdSearchResult)), Search.BirdSearchResult())
            Else
                Return CType(Array.CreateInstance(GetType(Search.BirdSearchResult), 0), Search.BirdSearchResult())
            End If
        End Function

        Public Function SearchForPermits(ByVal permitNumber As String) As Search.PermitSearchResult()
            Return BirdRegistration.SearchForPermits(permitNumber, PartyId)
        End Function

        Public Shared Function SearchForPermits(ByVal permitNumber As String, ByVal partyId As Int32) As Search.PermitSearchResult()
            'if no permit number, return all permits for this party
            Return SearchForPermits(DataObjects.Entity.Permit.GetPermitsForApplication(permitNumber, partyId))
        End Function

        Public Function SearchForPermits(ByVal markType As Int32, ByVal markNumber As String) As Search.PermitSearchResult()
            Return BirdRegistration.SearchForPermits(markType, markNumber, PartyId)
        End Function

        Public Shared Function SearchForPermits(ByVal markType As Int32, ByVal markNumber As String, ByVal partyId As Int32) As Search.PermitSearchResult()
            'if no permit number, return all permits for this party
            Return SearchForPermits(DataObjects.Entity.Permit.GetPermitsForApplication(markType, markNumber, partyId))
        End Function

        Private Shared Function SearchForPermits(ByVal permitEntities As EnterpriseObjects.EntitySet) As Search.PermitSearchResult()
            If Not permitEntities Is Nothing AndAlso permitEntities.Count > 0 Then
                Dim ReturnResults(permitEntities.Count - 1) As Search.PermitSearchResult
                For Index As Int32 = 0 To permitEntities.Count - 1
                    Dim Permit As EnterpriseObjects.Entity = permitEntities.GetEntity(Index)
                    ReturnResults(Index) = New Search.PermitSearchResult
                    'issue date
                    If Not Permit.IsNull(0) Then ReturnResults(Index).IssueDate = CType(Permit(0), Date).ToShortDateString
                    If Not Permit.IsNull(1) Then ReturnResults(Index).PermitNumber = Permit(1).ToString
                    If Not Permit.IsNull(2) Then ReturnResults(Index).PermitInfoId = CType(Permit(2), Int32)
                Next Index
                Return ReturnResults
            Else
                Return CType(Array.CreateInstance(GetType(Search.PermitSearchResult), 0), Search.PermitSearchResult())
            End If
        End Function

        Public Function ListAllBirdsForKeeper() As Search.BirdSearchResult()
            Return ListAllBirdsForKeeper(PartyId)
        End Function

        Public Shared Function ListAllBirdsForKeeper(ByVal keeperId As Int32) As Search.BirdSearchResult()
            Return SearchForBirds(DataObjects.Entity.Party.GetBirdsForKeeper(keeperId))
        End Function

        Public Function SetSpecimenBySearchResultId(ByVal birdSearchResult As Search.BirdSearchResult) As Boolean
            Dim Result As Boolean = True
            Try
                Dim App As BaseBird = CType(RegistrationApplication(), BaseBird)
                Dim NewSpecimen As AdultSpecimenType = App.AddPolymorphicSpecimen()

                'does is need the dateacquired setting?
                If Not App.GetType().GetInterface(GetType(IDateAcquired).ToString) Is Nothing Then
                    'yes it does
                    If birdSearchResult.DateAcquired.Ticks > 0 Then
                        'only set if it's a valid date
                        CType(App, IDateAcquired).DateAcquired = birdSearchResult.DateAcquired
                    End If
                End If

                Dim Gender As Application.GenderType
                If String.Compare(birdSearchResult.Gender, "male", True) = 0 Then
                    NewSpecimen.SpecimenType = New SpecimenType(Application.GenderType.Male)
                Else
                    NewSpecimen.SpecimenType = New SpecimenType(Application.GenderType.Female)
                End If
                With NewSpecimen.SpecimenType
                    .CommonName = birdSearchResult.CommonName
                    .ScientificName = birdSearchResult.ScientificName
                    If birdSearchResult.HatchDateExact.Length > 0 Then
                        'leaves it as null if nothing set - otherwise set to true/false
                        .IsHatchDateExact = (String.Compare(birdSearchResult.HatchDateExact, "y", True) = 0)
                    End If
                    If birdSearchResult.HatchDate.Length > 0 Then
                        .HatchDate = Date.Parse(birdSearchResult.HatchDate)
                    End If
                    If birdSearchResult.A10Ref.Length > 0 Then
                        .Article10Reference = birdSearchResult.A10Ref
                    End If
                    If birdSearchResult.RegDocRef.Length > 0 Then
                        .RegistrationDocumentReference = birdSearchResult.RegDocRef
                    End If
                    If Not birdSearchResult.ECAnnex Is Nothing AndAlso birdSearchResult.ECAnnex.Length > 0 Then
                        .ECAnnex = birdSearchResult.ECAnnex
                    End If
                    .SpecimenId = birdSearchResult.Id
                End With
                If birdSearchResult.CITESSourceId > 0 Then
                    'does the object have an Origin property
                    Dim OriginProperty As Reflection.PropertyInfo = NewSpecimen.GetType().GetProperty("Origin")
                    If Not OriginProperty Is Nothing Then
                        OriginProperty.SetValue(NewSpecimen, birdSearchResult.CITESSourceId, Nothing)
                    End If
                    OriginProperty = NewSpecimen.GetType().GetProperty("Origin2")
                    If Not OriginProperty Is Nothing Then
                        OriginProperty.SetValue(NewSpecimen, birdSearchResult.CITESSourceId2, Nothing)
                    End If
                End If
                ' get the id marks
                Dim MarkLinks As DataObjects.EntitySet.SpecimenIDMarkSet = DataObjects.Entity.SpecimenIDMark.GetForSpecimen(birdSearchResult.Id)
                If Not MarkLinks Is Nothing AndAlso MarkLinks.Count > 0 Then
                    For Each MarkLink As DataObjects.Entity.SpecimenIDMark In MarkLinks
                        If IDMark.IsIdMarkARing(MarkLink.IDMarkTypeId) Then
                            NewSpecimen.AddRing(MarkLink)
                        Else
                            NewSpecimen.AddIdMark(MarkLink)
                        End If
                    Next MarkLink
                End If
                'is the permit used? FR3015
                Dim ImportedDateProperty As Reflection.PropertyInfo = NewSpecimen.GetType().GetProperty("ImportedDate")
                If Not ImportedDateProperty Is Nothing AndAlso birdSearchResult.PermitId > 0 Then
                    'load the permit infos for this permit
                    Dim PermitInfos As DataObjects.EntitySet.PermitInfoSet = DataObjects.Entity.PermitInfo.ServiceObject.GetByIndex_PermitId(birdSearchResult.PermitId)
                    If Not PermitInfos Is Nothing AndAlso PermitInfos.Count > 0 Then
                        For Each PermitInfo As DataObjects.Entity.PermitInfo In PermitInfos
                            'check to see if it has an entry in the returned table - this means used
                            Dim ReturnedPermits As DataObjects.EntitySet.ReturnedPermitSet = DataObjects.Entity.ReturnedPermit.ServiceObject.GetForPermitInfo(PermitInfo.PermitInfoId)
                            If Not ReturnedPermits Is Nothing AndAlso ReturnedPermits.Count > 0 Then
                                'we have returned permits so check to see if the import date is set
                                For Each ReturnedPermit As DataObjects.Entity.ReturnedPermit In ReturnedPermits
                                    If Not ReturnedPermit.IsImportExportDateNull Then
                                        'it is set seo assign the value to the property
                                        ImportedDateProperty.SetValue(NewSpecimen, ReturnedPermit.ImportExportDate, Nothing)
                                    End If
                                    Exit For
                                Next ReturnedPermit
                            End If
                        Next PermitInfo
                    End If
                End If
            Catch ex As InvalidCastException
                ' can't search for the result so fail
                Result = False
            End Try

            Return Result
        End Function

        Public Shared Function SearchForArticle10(ByVal article10Number As String) As Search.Article10SearchResult()
            Dim Article10Entities As EnterpriseObjects.EntitySet = DataObjects.Entity.Article10.GetArticle10sForApplication(article10Number)
            If Not Article10Entities Is Nothing AndAlso Article10Entities.Count > 0 Then
                Dim ReturnResults(Article10Entities.Count - 1) As Search.Article10SearchResult
                For Index As Int32 = 0 To Article10Entities.Count - 1
                    Dim Article10 As EnterpriseObjects.Entity = Article10Entities.GetEntity(Index)
                    ReturnResults(Index) = New Search.Article10SearchResult
                    'issue date
                    If Not Article10.IsNull(0) Then ReturnResults(Index).IssueDate = CType(Article10(0), Date).ToShortDateString
                    If Not Article10.IsNull(1) Then ReturnResults(Index).Article10Numbers = Article10(1).ToString
                    If Not Article10.IsNull(2) Then ReturnResults(Index).PermitId = CType(Article10(2), Int32)
                Next Index
                Return ReturnResults
            Else
                Return CType(Array.CreateInstance(GetType(Search.Article10SearchResult), 0), Search.Article10SearchResult())
            End If
        End Function

        Public Shared Function SearchForKeepersBySpecimen(ByVal specimenId As Int32) As Search.KeeperSearchResult()
            Dim KeeperEntities As EnterpriseObjects.EntitySet = DataObjects.Entity.Party.GetKeepersBySpecimen(specimenId)
            If Not KeeperEntities Is Nothing AndAlso KeeperEntities.Count > 0 Then

                Dim ReturnResults As New ArrayList
                For Each Keeper As EnterpriseObjects.Entity In KeeperEntities
                    Dim ReturnResult As New Search.KeeperSearchResult
                    With ReturnResult
                        If Not Keeper.IsNull(0) Then .PartyName = Keeper(0).ToString Else .PartyName = String.Empty
                        If Not Keeper.IsNull(1) Then .PartyId = Keeper(1).ToString Else .PartyId = String.Empty
                        If Not Keeper.IsNull(2) Then .DateTransferred = CType(Keeper(2), Date).ToShortDateString Else .DateTransferred = String.Empty
                        If Not Keeper.IsNull(3) Then .AddressLine1 = Keeper(3).ToString Else .AddressLine1 = String.Empty
                        If Not Keeper.IsNull(4) Then .Postcode = Keeper(4).ToString Else .Postcode = String.Empty
                        If Not Keeper.IsNull(5) Then .DateRegistered = CType(Keeper(5), Date).ToShortDateString Else .DateRegistered = String.Empty
                    End With
                    ReturnResults.Add(ReturnResult)
                Next Keeper
                Return CType(ReturnResults.ToArray(GetType(Search.KeeperSearchResult)), Search.KeeperSearchResult())
            Else
                Return CType(Array.CreateInstance(GetType(Search.KeeperSearchResult), 0), Search.KeeperSearchResult())
            End If
        End Function
#End Region

#Region " Delete "
        Public Shared Shadows Function Delete(ByVal applicationId As Int32) As Boolean
            Dim Result As Boolean
            If applicationId > 0 Then
                Result = DataObjects.Entity.RingApplication.DeleteById(applicationId)
                If Result Then Result = DataObjects.Entity.Application.DeleteById(applicationId)
            End If
            Return Result
        End Function
#End Region

#Region " Service "
        Public Shared Function GetAdultSpecimenAgeStatus() As Common.NameValuePair()
            Dim Results As DataObjects.Collection.SpecimenAgeStatusBoundCollection = ReferenceData.Lists.GetSpecimenAgeStatus(True)
            Dim NewItems As New ArrayList
            For Each Item As DataObjects.Entity.SpecimenAgeStatus In Results
                If Not Item.Description.ToLower.StartsWith("date") Then NewItems.Add(New Common.NameValuePair(Item.Description, Item.Id))
            Next Item
            Return CType(NewItems.ToArray(GetType(Common.NameValuePair)), Common.NameValuePair())
        End Function

        Public Shared Function GetKeeperContact(ByVal partyId As Int32) As String
            Try
                Dim person As New BO.Party.BOPartyIndividual(partyId)
                Dim contact As New BO.Party.BOContact(person.PreferredContactId)
                Return contact.Detail
            Catch   'party may not be a person
                Return String.Empty
            End Try
        End Function

        Public Function GetExpectedNames() As String() 'returns 2 strings: scientific name, then common name
            Dim result(1) As String
            result(0) = String.Empty  'scientific name
            result(1) = String.Empty  'common name
            Application.Bird.Reports.ReportData.GetExpectedNames(Me, result(0), result(1))
            Return result
        End Function

        Public Shared Function GetExpectedNames(ByVal birdRegId As Int32, ByVal userPermissions As String) As String() 'returns 2 strings: scientific name, then common name
            Dim birdReg As New BirdRegistration(birdRegId, userPermissions)
            Return birdReg.GetExpectedNames()
        End Function

        Public Function RemoveSpecimen(ByVal specimenId As Int32) As BirdRegistration
            If RegistrationApplication.RemoveSpecimen(specimenId) Then
                'no point in saving if something goes wrong
                Return Save()
            Else
                Return Nothing
            End If
        End Function

        Public Function AddIdMarks(ByVal boIDMark As IDMark) As BirdRegistration
            RegistrationApplication.Specimens(0).AddIdMark(boIDMark)
            Return Save()
        End Function

        Public Function AddParentClutch(ByVal birdSearchResult As Search.BirdSearchResult, ByVal gender As Application.GenderType) As BirdRegistration
            If Parents.AddParent(birdSearchResult, gender) Is Nothing Then
                Return Nothing
            Else
                Return Me
            End If
        End Function

        Public Function SetSpeciesBySearchID(ByVal birdSearchResult As BO.Application.Bird.Registration.Search.BirdSearchResult) As BirdRegistration
            If SetSpecimenBySearchResultId(birdSearchResult) Then   'MLD 16/12/4 driving me doollaly
                Return Me
            Else
                Return Nothing
            End If
        End Function

        Public Function SubmitRegistration(ByVal ssoUserId As Int64) As BirdRegistration
            If Submit(ssoUserId) Then
                Save()
                Return Nothing 'Submitted with no errors
            Else
                Return Me 'Return errors
            End If
        End Function

        Public Function AddSpecimen() As BirdRegistration
            Dim NewSpec As AdultSpecimenType = CType(RegistrationApplication, BaseBird).AddPolymorphicSpecimen()
            Dim NewSpecType As New SpecimenType
            NewSpecType.HatchDate = Date.Today
            NewSpec.SpecimenType = NewSpecType
            Return Save()
        End Function

        Public Shared Function GetKeeperIds(ByVal specimenIds() As Int32) As Int32()
            Dim count As Int32 = specimenIds.Length - 1
            Dim results(count) As Int32
            For i As Int32 = 0 To count
                Dim data As DataSet = DataObjects.Sprocs.dbo_usp_GetKeeperId(specimenIds(i), Nothing, GetType(DataSet))
                results(i) = -1
                If data.Tables(0).Rows.Count > 0 Then
                    Dim row As DataRow = data.Tables(0).Rows(0)
                    results(i) = Integer.Parse(row.Item("PartyId").toString)
                End If
            Next i
            Return results
        End Function

        Public Function GetSummaryGridData(ByVal ssoUserId As Int64) As SummaryData.BaseSummary()
            Return GetSummaryGridData(Me, ssoUserId)
        End Function

        Public Shared Function GetSummaryGridData(ByVal birdBO As BirdRegistration, ByVal ssoUserId As Int64) As SummaryData.BaseSummary()
            If birdBO.RegApplicationType = RegistrationApplicationType.Clutch Then
                Return New SummaryData.BaseSummary() {New SummaryData.ChickSummary(birdBO, ssoUserId)}
            Else
                Return New SummaryData.BaseSummary() {New SummaryData.AdultSummary(birdBO, ssoUserId)}
            End If
        End Function
#End Region

        Public Function SetSpecimenForEgg(ByVal specimen As ClutchSpecimen, ByVal eggIndex As Int32) As BirdRegistration
            Dim CurrentClutch As Clutch = CType(RegistrationApplication, Clutch)
            ClutchEgg.SetSpecimenForEgg(RegistrationApplication.Specimens, specimen, CurrentClutch.Eggs(eggIndex))
            Return Save()
        End Function

        Public Function GetSpecimen(ByVal specimenId As Int32) As SpecimenInfo
            Dim Result As New SpecimenInfo(Nothing, 0, Nothing, Nothing, Nothing)
            Dim Index As Int32 = 0
            For Each Spec As AdultSpecimenType In RegistrationApplication.Specimens
                If Spec.SpecimenType.SpecimenId = specimenId Then
                    Dim EULicenseNumber As String = String.Empty
                    Dim FatherMark As String = String.Empty
                    Dim MotherMark As String = String.Empty
                    If TypeOf Spec Is AdultImportedSpecimen Then
                        Dim ImportedSpec As AdultImportedSpecimen = CType(Spec, AdultImportedSpecimen)

                        If Not ImportedSpec.EULicenseNumber Is Nothing Then EULicenseNumber = ImportedSpec.EULicenseNumber.ToString
                        If ImportedSpec.Parents.Mother.Length > 0 Then
                            MotherMark = ImportedSpec.Parents.Mother(0).GetMostPermanentMark
                        End If
                        If ImportedSpec.Parents.Father.Length > 0 Then
                            FatherMark = ImportedSpec.Parents.Father(0).GetMostPermanentMark
                        End If
                    End If
                    Result = New SpecimenInfo(Spec, Index, EULicenseNumber, FatherMark, MotherMark)
                    Exit For
                End If
                Index += 1
            Next Spec
            Return Result
        End Function

        Public Structure SpecimenInfo
            Friend Sub New(ByVal specimenType As AdultSpecimenType, ByVal specimenNumber As Int32, ByVal eULicenseNumber As String, ByVal mostPermFatherMark As String, ByVal mostPermMotherMark As String)
                Me.SpecimenType = specimenType
                Me.SpecimenNumber = specimenNumber
                Me.EULicenseNumber = eULicenseNumber
                Me.MostPermFatherMark = mostPermFatherMark
                Me.MostPermMotherMark = mostPermMotherMark
            End Sub

            Public SpecimenType As AdultSpecimenType
            Public SpecimenNumber As Int32
            Public EULicenseNumber As String
            Public MostPermFatherMark As String
            Public MostPermMotherMark As String
        End Structure

        Public Function KeptAddress(ByVal specimenId As Int32) As String
            Dim Result As String = String.Empty
            Dim SpecInfo As BirdRegistration.SpecimenInfo = GetSpecimen(specimenId)
            Dim Spec As AdultSpecimenType = SpecInfo.SpecimenType
            If Not Spec Is Nothing Then
                Dim pi As System.Reflection.PropertyInfo = Spec.GetType().GetProperty("KeptAddress_Helper")
                If Not pi Is Nothing Then
                    Dim TempResult As Object = pi.GetValue(Spec, Nothing)
                    If Not TempResult Is Nothing Then Result = TempResult.ToString()
                End If
            End If
            Return Result
        End Function

        Public Shared Function ContactGWDMessage() As String
            Dim Message As New ValidationError(ValidationError.ValidationCodes.GWDContactDetails)
            Return Message.ErrorMessage
        End Function

        Public Function CreateSpecimenFromEgg(ByVal eggIndex As Int32) As BirdRegistration
            Dim CurrentClutch As Clutch = CType(RegistrationApplication, Clutch)
            ClutchEgg.CreateSpecimenFrom(CurrentClutch, CurrentClutch.Eggs(eggIndex))
            Return Me
        End Function

        Public Function GetSpecimenFromEgg(ByVal eggIndex As Int32) As ClutchSpecimen
            Dim CurrentClutch As Clutch = CType(RegistrationApplication, Clutch)
            Return ClutchEgg.GetSpecimen(CurrentClutch.Specimens, CurrentClutch.Eggs(eggIndex))
        End Function

        Public Function SetDORReturned() As BirdRegistration
            If Not RegApplicationType = RegistrationApplicationType.Clutch Then
                Throw New NotImplementedException("Only clutch applications are applicable")
            End If

            'clear the error state
            ValidationErrors = Nothing

            Dim ClutchApp As Clutch = CType(RegistrationApplication, Clutch)

            For Each Egg As ClutchEgg In ClutchApp.Eggs
                Dim Specimen As ClutchSpecimen = Egg.GetSpecimen(RegistrationApplication.Specimens)
                If Specimen Is Nothing Then
                    'the egg never became a specimen so it can't have been ringed!
                    AddValidationError(ValidationError.ValidationCodes.TheEggIsNotASpecimen)
                Else
                    If (Specimen.SpecimenType.ScientificName Is Nothing OrElse Specimen.SpecimenType.ScientificName.Length = 0) OrElse _
                       (Specimen.SpecimenType.CommonName Is Nothing OrElse Specimen.SpecimenType.CommonName.Length = 0) Then
                        'must have a sci name/common name

                        '...maybe we can set it for them?
                        'check to see if the parents are of the same specie
                        If Not Parents Is Nothing Then
                            Dim SciName As String = String.Empty
                            Dim CommonName As String
                            For Each Father As Parent In Parents.Father
                                If Not Father.ScientificName Is Nothing AndAlso Father.ScientificName.Length > 0 Then
                                    If SciName.Length = 0 Then
                                        SciName = Father.ScientificName
                                        CommonName = Father.CommonName
                                    ElseIf Father.ScientificName <> SciName Then
                                        SciName = Nothing
                                        Exit For
                                    End If
                                End If
                            Next Father
                            If Not SciName Is Nothing Then
                                For Each Mother As Parent In Parents.Mother
                                    If Not Mother.ScientificName Is Nothing AndAlso Mother.ScientificName.Length > 0 Then
                                        If SciName.Length = 0 Then
                                            SciName = Mother.ScientificName
                                            CommonName = Mother.CommonName
                                        ElseIf Mother.ScientificName <> SciName Then
                                            SciName = Nothing
                                            Exit For
                                        End If
                                    End If
                                Next Mother
                            End If
                            If Not SciName Is Nothing AndAlso SciName.Length > 0 Then
                                'we have a single sciname!
                                Specimen.SpecimenType.ScientificName = SciName
                                Specimen.SpecimenType.CommonName = CommonName
                            Else
                                'maybe we can do more...?
                                If Parents.Mother.Length = 1 AndAlso Parents.Father.Length = 1 Then
                                    'even though these parents are obviously of different species (see above!),
                                    'we can derive a specie from them
                                    Specimen.SpecimenType.ScientificName = Parents.Mother(0).ScientificName & Parents.Father(0).ScientificName
                                    Specimen.SpecimenType.CommonName = Parents.Mother(0).CommonName & Parents.Father(0).CommonName
                                End If
                            End If
                        End If

                        'recheck
                        If Specimen.SpecimenType.ScientificName Is Nothing OrElse Specimen.SpecimenType.ScientificName.Length = 0 Then
                            AddValidationError(ValidationError.ValidationCodes.ScientificNameCannotBeBlank)
                        End If
                        If Specimen.SpecimenType.CommonName Is Nothing OrElse Specimen.SpecimenType.CommonName.Length = 0 Then
                            AddValidationError(ValidationError.ValidationCodes.CommonNameCannotBeBlank)
                        End If
                    End If

                    If Specimen.SpecimenType.IsHatchDateExact Is Nothing OrElse Not TypeOf Specimen.SpecimenType.IsHatchDateExact Is Boolean Then
                        'the case office must enter a hatch date exact flag setting
                        AddValidationError(ValidationError.ValidationCodes.TheEggIsNotASpecimen)
                    End If
                    If Specimen.SpecimenType.HatchDate Is Nothing OrElse Not TypeOf Specimen.SpecimenType.HatchDate Is Date Then
                        'the case office must enter a hatch date
                        AddValidationError(ValidationError.ValidationCodes.HatchDateEactSettingNotSet)
                    End If
                End If
            Next Egg

            ApplicationStatus = BOPermitInfo.PermitStatusTypes.DOR_Returned

            'save the app in it's current state and reurn it with (or without) the validation errors
            Return Save()
        End Function

        Private Sub UpdateSLAClock()
            If mApplicationStatus = ApplicationStatus.DOR_Returned Then
                'check that the clock isn't still running from a previous save...
                If Not SLARunning Then
                    'only worth coming in here if we aren't already running
                    'set the clock date to now
                    mSLAStart = mEnv.Now
                End If
            Else
                'we are in another status.  If the clock is running we need to stop
                'it and record the time.
                If SLARunning Then
                    'the clock was running
                    'get the minutes
                    Dim SLARunningTime As Int32 = Date.op_Subtraction(mEnv.Now, mSLAStart).Minutes

                    'add it to any existing SLA time
                    mSLAClock += SLARunningTime

                    'stop the date
                    mSLAStart = New Date(0)
                End If
            End If
        End Sub

        Public Function GetRelatedApplication() As BirdRegistration
            If mRelatedRingApplicationId > 0 Then
                Return BirdRegistration.GetRelatedApplication(mRelatedRingApplicationId)
            Else
                Throw New Exception("This application doesn't have a related application!")
            End If
        End Function

        Public Shared Function GetRelatedApplication(ByVal applicationId As Int32) As BirdRegistration
            Return New BirdRegistration(applicationId)
        End Function

        Public Function GetAllApplicationsBasedOnThis() As Int32()
            Dim Result As Int32() = BirdRegistrationSearch.GetIds(BirdRegistrationSearch.RegistrationSearchTypes.RelatedRingApplicationId, ApplicationId)
            Return Result
        End Function

        Public Shared Function GetAllApplicationsBasedOnThis(ByVal applicationId As Int32) As Int32()
            Return New BirdRegistration(applicationId).GetAllApplicationsBasedOnThis()
        End Function

        'Public Function DeclineRingApplication(ByVal ssoUserid As Int64, ByVal cancellationReason As String) As BirdRegistration
        '    If Common.IsInRole(ssoUserId, Common.RolesList.CaseOfficer) Then
        '        mApplicationStatus = ApplicationStatus.Declined
        '        Dim ReportService As New ReportService.ReportServiceWse
        '        Dim ReportCrtieria As ReportService.RegistrationRefusalLetterCriteria = ReportService.GetRegistrationRefusalLetterCriteria()
        '        ReportCrtieria.Description = "DOR Declined Letter"
        '        'actually passing in the application id as don't have permits.
        '        ReportCrtieria.PermitIds = New Int32() {RingApplicationId}
        '        Dim ReportResult As ReportService.BOReportResults() = ReportService.CreateReport(New ReportService.ReportCriteria() {ReportCrtieria}, ReportCrtieria.Description, True)
        '        mDeclineLetterReportId = ReportResult(0).ReportId
        '        'update the data.  The UI can read the value from the above property.
        '        Save()
        '        Return Me
        '    Else
        '        Throw New CannotException("Case Officers", "Decline")
        '    End If
        'End Function

        'Public Function CancelRingApplication(ByVal ssoUserId As Int64, ByVal cancellationReason As String) As BirdRegistration
        '    Return CancelRingApplication(Me, ssouserid, cancellationReason)
        'End Function

        'Public Shared Function CancelRingApplication(ByVal ringApplication As BirdRegistration, ByVal ssoUserId As Int64, ByVal cancellationReason As String) As BirdRegistration
        '    Dim ReturnVal As BirdRegistration = Nothing

        '    If cancellationReason Is Nothing OrElse cancellationReason.Length = 0 Then
        '        Throw New NullReferenceException("Cancellation reason cannot be empty")
        '    Else
        '        If Common.IsInRole(ssoUserId, Common.RolesList.CaseOfficer) OrElse _
        '           Common.IsInRole(ssoUserId, Common.RolesList.Customer) Then
        '            'make sure it's in a status that we can cancel from
        '            Select Case ringApplication.ApplicationStatus
        '                Case Application.ApplicationStatus.Ring_Request_Submitted_Online, _
        '                     Application.ApplicationStatus.Submitted_By_Customer, _
        '                     Application.ApplicationStatus.Progress_Allowed, _
        '                     Application.ApplicationStatus.Referred, _
        '                     Application.ApplicationStatus.Cancel_Pending
        '                Case Else
        '                    Throw New CannotException("Customers and Case Officers", "Cancel", " when its at a particulr status")
        '            End Select
        '            'ok, things are good
        '            If Common.IsInRole(ssoUserId, Common.RolesList.CaseOfficer) Then
        '                ringApplication.ApplicationStatus = Application.ApplicationStatus.Cancelled
        '            Else ' must be a customer
        '                ringApplication.ApplicationStatus = Application.ApplicationStatus.Cancel_Pending
        '            End If
        '            ringApplication.CancellationReason = cancellationReason
        '            ReturnVal = ringApplication.Save()
        '        Else
        '            Throw New CannotException("Customers and Case Officers", "Cancel")
        '        End If
        '    End If

        '    Return ReturnVal
        'End Function

        'Public Function RefuseCancelApplication(ByVal ssoUserId As Int64, ByVal cancellationRefusalReason As String) As BirdRegistration
        '    Return RefuseCancelApplication(Me, ssoUserId, cancellationRefusalReason)
        'End Function

        'Public Shared Function RefuseCancelApplication(ByVal ringApplication As BirdRegistration, ByVal ssoUserId As Int64, ByVal cancellationRefusalReason As String) As BirdRegistration
        '    Dim ReturnVal As BirdRegistration = Nothing

        '    If cancellationRefusalReason Is Nothing OrElse cancellationRefusalReason.Length = 0 Then
        '        Throw New NullReferenceException("Cancellation refusal reason cannot be empty")
        '    Else
        '        If Common.IsInRole(ssoUserId, Common.RolesList.CaseOfficer) Then
        '            'make sure it's in a status that we can cancel from
        '            Select Case ringApplication.ApplicationStatus
        '                Case Application.ApplicationStatus.Ring_Request_Submitted_Online, _
        '                     Application.ApplicationStatus.Submitted_By_Customer, _
        '                     Application.ApplicationStatus.Progress_Allowed, _
        '                     Application.ApplicationStatus.Referred, _
        '                     Application.ApplicationStatus.Cancel_Pending
        '                Case Else
        '                    Throw New CannotException("Customers and Case Officers", "Cancel", " when its at a particulr status")
        '            End Select
        '            'ok, things are good
        '            ringApplication.ApplicationStatus = Application.ApplicationStatus.Progress_Allowed
        '            ringApplication.CancellationRefusalReason = cancellationRefusalReason
        '            ReturnVal = ringApplication.Save()
        '        Else
        '            Throw New CannotException("Case Officers", "Cancel")
        '        End If
        '    End If

        '    Return ReturnVals
        'End Function

        Private Sub CheckLoadIsOKForParty(ByVal ssoUserId As Int64, ByVal permissions As String)
            Dim Success As Boolean = False
            Dim Updatable As Boolean = False
            Dim RingsPickable As Boolean = False
            Dim DocumentsReIssue As Boolean = False
            Dim InspectorateDecisionSetable As Boolean = False
            Dim PaidFlagSetable As Boolean = False
            Dim CreateAdditionalRingRequestSettable As Boolean = False

            Select Case mApplicationStatus
                Case BOPermitInfo.PermitStatusTypes.BeingInput_Customer
                    Success = Common.IsInRole(ssoUserId, Common.RolesList.Customer)
                    PaidFlagSetable = Common.IsInRole(ssoUserId, Common.RolesList.Customer)
                    Updatable = True
                Case BOPermitInfo.PermitStatusTypes.BeingInput_CaseOfficer
                    Success = Common.IsInRole(ssoUserId, Common.RolesList.CaseOfficer)
                    Updatable = True
                    PaidFlagSetable = True
                Case Else
                    Success = True
                    Select Case mApplicationStatus
                        Case BOPermitInfo.PermitStatusTypes.Ring_Request_Submitted_By_Customer, _
                             BOPermitInfo.PermitStatusTypes.SubmittedByCustomer, _
                             BOPermitInfo.PermitStatusTypes.ProgressAllowed
                            Updatable = Common.IsInRole(ssoUserId, Common.RolesList.CaseOfficer)
                            PaidFlagSetable = (mApplicationStatus = BOPermitInfo.PermitStatusTypes.Ring_Request_Submitted_By_Customer) OrElse (mApplicationStatus = BOPermitInfo.PermitStatusTypes.SubmittedByCustomer)
                        Case BOPermitInfo.PermitStatusTypes.Referred
                            Select Case mAssignedTo
                                Case Common.AssignedToList.Customer
                                    Updatable = Common.IsInRole(ssoUserId, Common.RolesList.CaseOfficer)
                                Case Common.AssignedToList.Inspectorate
                                    Updatable = Common.IsInRole(ssoUserId, Common.RolesList.CaseOfficer) OrElse Common.IsInRole(ssoUserId, Common.RolesList.Inspectorate)
                                    RingsPickable = Common.HasPermissions(permissions, Common.UserPermissions.CanPickRings)
                                    InspectorateDecisionSetable = Common.IsInRole(ssoUserId, Common.RolesList.Inspectorate)
                                Case Common.AssignedToList.TeamLeader
                                    Updatable = Common.IsInRole(ssoUserId, Common.RolesList.CaseOfficer) OrElse Common.IsInRole(ssoUserId, Common.RolesList.TeamLeader)
                                Case Common.AssignedToList.Other
                                    Updatable = Common.IsInRole(ssoUserId, Common.RolesList.CaseOfficer)
                                Case Else
                                    Throw New NotImplementedException("Assigned To doesn't exist in this context")
                            End Select
                        Case BOPermitInfo.PermitStatusTypes.Authorised, _
                             BOPermitInfo.PermitStatusTypes.Refused, _
                             BOPermitInfo.PermitStatusTypes.CancelPending, _
                             BOPermitInfo.PermitStatusTypes.Cancelled
                            Updatable = False
                        Case BOPermitInfo.PermitStatusTypes.Chick_DOR_Issued
                            Updatable = Common.IsInRole(ssoUserId, Common.RolesList.CaseOfficer)
                            CreateAdditionalRingRequestSettable = True
                        Case BOPermitInfo.PermitStatusTypes.Adult_DOR_Issued
                            Updatable = (Common.IsInRole(ssoUserId, Common.RolesList.CaseOfficer) AndAlso mAssignedTo = Common.AssignedToList.CaseOfficer) OrElse _
                                        (Common.IsInRole(ssoUserId, Common.RolesList.Customer) AndAlso mAssignedTo = Common.AssignedToList.Customer)
                            PaidFlagSetable = True
                        Case BOPermitInfo.PermitStatusTypes.DOR_Returned
                            Updatable = True
                            PaidFlagSetable = True
                            CreateAdditionalRingRequestSettable = True
                        Case BOPermitInfo.PermitStatusTypes.Registered
                            Updatable = Common.IsInRole(ssoUserId, Common.RolesList.CaseOfficer)
                            DocumentsReIssue = True
                            CreateAdditionalRingRequestSettable = True
                        Case BOPermitInfo.PermitStatusTypes.Closed_By_Customer
                            Updatable = Common.IsInRole(ssoUserId, Common.RolesList.CaseOfficer)
                    End Select
            End Select
            If Not Success Then
                Throw New CannotViewException("User " & ssoUserId, "application " & ApplicationId)
            End If
            mCanUpdate = Updatable
            mCanPickRings = RingsPickable
            mCanReIssueDocuments = DocumentsReIssue
            mCanSetInspectorateDecision = InspectorateDecisionSetable
            mCanSetPaidFlag = PaidFlagSetable
            mCanCreateAdditionalRingRequest = CreateAdditionalRingRequestSettable
        End Sub

        'Public Function WhoCanApplicationBeReferredTo() As Common.AssignedToList()
        '    Dim ReferredToList() As Common.AssignedToList = Nothing
        '    Select Case mApplicationStatus
        '        Case Application.ApplicationStatus.Progress_Allowed
        '            ReferredToList = New Common.AssignedToList() {Common.AssignedToList.Customer, Common.AssignedToList.Inspectorate, Common.AssignedToList.TeamLeader, Common.AssignedToList.Other}
        '        Case Application.ApplicationStatus.Referred
        '            ReferredToList = New Common.AssignedToList() {Common.AssignedToList.CaseOfficer}
        '        Case Application.ApplicationStatus.Chick_DOR_Issued
        '            ReferredToList = New Common.AssignedToList() {Common.AssignedToList.Customer}
        '        Case Application.ApplicationStatus.Adult_DOR_Issued
        '            If mAssignedTo = Common.AssignedToList.CaseOfficer Then
        '                ReferredToList = New Common.AssignedToList() {Common.AssignedToList.Customer}
        '            End If
        '        Case Application.ApplicationStatus.DOR_Returned
        '            ReferredToList = New Common.AssignedToList() {Common.AssignedToList.Customer, Common.AssignedToList.TeamLeader, Common.AssignedToList.Other}
        '        Case Application.ApplicationStatus.Cancel_Pending
        '            If mAssignedTo <> Common.AssignedToList.CaseOfficer Then
        '                ReferredToList = New Common.AssignedToList() {Common.AssignedToList.CaseOfficer}
        '            End If
        '    End Select
        '    Return ReferredToList
        'End Function

        Public Shadows Function Clone() As BirdRegistration
            Throw New NotImplementedException("Clone")
        End Function

        Public Shadows Function Clone(ByVal ssoUserId As Int64) As BirdRegistration
            CreateFromExistingApplication(Me, ssoUserId)
            Return Me
        End Function

        Private Sub CreateFromExistingApplication(ByVal applicationId As Int32, ByVal ssoUserId As Int64)
            'load the current application
            Dim CurrentApplication As New BirdRegistration(applicationId)
            CreateFromExistingApplication(CurrentApplication, ssoUserId)
        End Sub

        Private Sub CreateFromExistingApplication(ByVal currentApplication As BirdRegistration, ByVal ssoUserId As Int64)
            'this method can only be called if it hasn't already been cloned
            If currentApplication.RelatedRingApplicationId > 0 Then
                Throw New Exception("Application has been previously cloned and cannot be cloned again!")
            End If

            'makes ure that this new application is related
            currentApplication.RelatedRingApplicationId = currentApplication.ApplicationId

            Dim CloneApp As DataObjects.Entity.RingApplication = CreateApplication(ssoUserId)
            currentApplication.ApplicationId = CloneApp.ApplicationId
            currentApplication.CheckSum = 0
            currentApplication.RegistrationCheckSum = 0
            currentApplication.RingApplicationId = CloneApp.RingApplicationId

            'if a clutch - remove any eggs
            If mRegApplicationType = RegistrationApplicationType.Clutch Then
                'remove the rings but keep the eggs
                If Not TypeClutch.Eggs Is Nothing AndAlso TypeClutch.Eggs.Length > 0 Then
                    For Each Egg As ClutchEgg In TypeClutch.Eggs
                        'clear all of the rings
                        Egg.Rings = CType(Array.CreateInstance(GetType(IDMark), 0), IDMark())
                        'set it to be marked as cloned
                        Egg.Cloned = True
                    Next Egg
                End If
                'rings we be removed as the eggs are going
                'TypeClutch.Eggs = CType(Array.CreateInstance(GetType(ClutchEgg), 0), ClutchEgg())
                'the specimens associated with the eggs will be cleared below.
            End If
            'remove any specimens
            currentApplication.RegistrationApplication.Specimens = CType(Array.CreateInstance(GetType(AdultSpecimenType), 0), AdultSpecimenType())

            currentApplication.Save()

            'check rights...
            PostCreateApplicationRecord(ssoUserId)
        End Sub

        Private Sub PostCreateApplicationRecord(ByVal ssoUserId As Int64)
            If ssoUserId > 0 Then
                Try
                    If Common.IsInRole(ssoUserId, Common.RolesList.CaseOfficer) Then
                        'user is a case officer so...
                        mApplicationStatus = BOPermitInfo.PermitStatusTypes.BeingInput_CaseOfficer
                        mAssignedTo = Common.AssignedToList.CaseOfficer
                    Else
                        mApplicationStatus = BOPermitInfo.PermitStatusTypes.BeingInput_Customer
                        mAssignedTo = Common.AssignedToList.Customer
                    End If
                Catch ex As ArgumentNullException
                    'this error fires when this code is being executed without a webservice (ie the tokens don't exist)
                    mApplicationStatus = BOPermitInfo.PermitStatusTypes.BeingInput_CaseOfficer
                    mAssignedTo = Common.AssignedToList.CaseOfficer
                End Try
            End If
        End Sub

        Friend Sub AddValidationError(ByVal [error] As ValidationError.ValidationCodes, ByVal extraMessageInfo As Collections.Specialized.NameValueCollection)
            If ValidationErrors Is Nothing Then
                ValidationErrors = New ValidationManager(ValidationManager.ValidationTitles.CannotSaveApplication)
            End If
            ValidationErrors.AddError(New ValidationError([error], extraMessageInfo))
        End Sub

        Friend Sub AddValidationError(ByVal [error] As ValidationError.ValidationCodes)
            AddValidationError([error], Nothing)
        End Sub

        Public Function SetInfoByPermitInfoId(ByVal permitInfoId As Int32) As Boolean
            'TODO: SetInfoByPermitId
            Return True
        End Function

        Public Function AddConviction() As Conviction
            Dim Success As Conviction = Nothing
            Dim Upper As Int32 = 0
            If Not mConvictions Is Nothing Then
                Upper = mConvictions.Length
            End If
            ReDim Preserve mConvictions(Upper)
            mConvictions(Upper) = New Conviction
            Success = mConvictions(Upper)
            Return Success
        End Function
#End Region

#Region " Properties "

#Region " Helper Properties"
        Public Property DORReportId() As Int32
            Get
                Dim Result As Int32 = 0
                If mDORPrintJobId > 0 Then
                    Dim View As ReportData.BOReportView() = ReportData.BOReportView.GetPrintJobViewShared(mDORPrintJobId)
                    'there will be only one!
                    If View.Length > 0 Then Result = View(0).ReportId
                End If
                Return Result
            End Get
            Set(ByVal Value As Int32)
            End Set
        End Property

        Public Property ApplicationStatus_Helper() As String
            Get
                Dim Result As String = String.Empty
                If mApplicationStatus > 0 Then
                    Dim StatusEntity As DataObjects.Entity.PermitStatus = DataObjects.Entity.PermitStatus.GetById(mApplicationStatus)
                    If Not StatusEntity Is Nothing Then
                        Result = StatusEntity.Description
                    End If
                End If
                Return Result
            End Get
            Set(ByVal Value As String)
                'only here for proxy!
            End Set
        End Property

        Public Property ApplicationMethod_Helper() As String
            Get
                Return GetApplicationMethod(mApplicationMethodId)
            End Get
            Set(ByVal Value As String)
                'only here for proxy!
            End Set
        End Property

        Public Property DORApplicationMethod_Helper() As String
            Get
                Return GetApplicationMethod(mDORApplicationMethodId)
            End Get
            Set(ByVal Value As String)
                'only here for proxy!
            End Set
        End Property

        Private Function GetApplicationMethod(ByVal id As Int32) As String
            Dim Result As String = String.Empty
            If id > 0 Then
                Dim ApplicationMethodEntity As DataObjects.Entity.ApplicationMethod = DataObjects.Entity.ApplicationMethod.GetById(id)
                If Not ApplicationMethodEntity Is Nothing Then
                    Result = ApplicationMethodEntity.Description
                End If
            End If
            Return Result
        End Function

        Public Property PaymentStatus_Helper() As String
            Get
                Return System.Enum.GetName(GetType(Application.PaymentStatusTypes), PaymentStatus).ToString.Replace("_", " ")
            End Get
            Set(ByVal Value As String)
                'only here for proxy!
            End Set
        End Property

        Public Property AssignedTo_Helper() As String
            Get
                Return System.Enum.GetName(GetType(Common.AssignedToList), mAssignedTo).ToString.Replace("_", " ")
            End Get
            Set(ByVal Value As String)
                'only here for proxy!
            End Set
        End Property

        Friend ReadOnly Property ApplicationStatus_Internal(ByVal ssoUserId As Int64) As String
            Get
                If Common.IsInRole(ssoUserId, Common.RolesList.Customer) Then
                    Return ApplicationStatusCustomer_Helper
                Else
                    Return ApplicationStatus_Helper
                End If
            End Get
        End Property

        Public Property ApplicationStatusCustomer_Helper() As String
            Get
                Dim Result As String
                Select Case mApplicationStatus
                    Case ApplicationStatus.BeingInput_Customer
                        Result = "Being Input (Customer)"
                    Case ApplicationStatus.Ring_Request_Submitted_By_Customer, ApplicationStatus.SubmittedByCustomer
                        Result = "Submitted"
                    Case ApplicationStatus.ProgressAllowed, ApplicationStatus.Authorised, ApplicationStatus.DOR_Returned
                        Result = "In Progress"
                    Case ApplicationStatus.Referred
                        If mAssignedTo = Common.AssignedToList.Customer Then
                            Result = "Referred to Customer"
                        Else
                            Result = "In Progress"
                        End If
                    Case ApplicationStatus.Chick_DOR_Issued, ApplicationStatus.Adult_DOR_Issued
                        Result = "Registration Application Issued"
                    Case ApplicationStatus.Refused
                        Result = "Declined"
                    Case ApplicationStatus.Registered
                        Result = "Registered"
                    Case ApplicationStatus.Closed_By_Customer, ApplicationStatus.Fate
                        Result = "Fated"
                    Case ApplicationStatus.CancelPending
                        Result = "Pending Cancellation"
                    Case ApplicationStatus.Cancelled
                        Result = "Cancelled"
                    Case Else
                        Result = ApplicationStatus_Helper
                End Select
                Return Result
            End Get
            Set(ByVal Value As String)
                'only here for proxy!
            End Set
        End Property
#End Region

        Public Property RegistrationCheckSum() As Int32
            Get
                Return mRegistrationCheckSum
            End Get
            Set(ByVal Value As Int32)
                mRegistrationCheckSum = Value
            End Set
        End Property
        Private mRegistrationCheckSum As Int32

        Public Property RelatedRingApplicationId() As Int32
            Get
                Return mRelatedRingApplicationId
            End Get
            Set(ByVal Value As Int32)
                mRelatedRingApplicationId = Value
            End Set
        End Property
        Private mRelatedRingApplicationId As Int32

        'Public Overrides Property PaidDate() As Object
        '    Get
        '        Return mPaidDate
        '    End Get
        '    Set(ByVal Value As Object)
        '        'readonly
        '    End Set
        'End Property
        'Private mPaidDate As Object

        Friend ReadOnly Property Paid() As Boolean
            Get
                Return Not (PaidDate Is Nothing)
            End Get
        End Property

        Public Property CanUpdate() As Boolean
            Get
                Return mCanUpdate
            End Get
            Set(ByVal Value As Boolean)
                'readonly
            End Set
        End Property
        Private mCanUpdate As Boolean

        Public Property CanPickRings() As Boolean
            Get
                If mRegApplicationType = RegistrationApplicationType.Clutch AndAlso _
                   CType(RegistrationApplication, Clutch).RingsPicked Then
                    Return False
                Else
                    Return mCanPickRings
                End If
            End Get
            Set(ByVal Value As Boolean)
                'readonly
            End Set
        End Property
        Private mCanPickRings As Boolean

        Public Property CanReIssueDocuments() As Boolean
            Get
                Return mCanReIssueDocuments
            End Get
            Set(ByVal Value As Boolean)
                'readonly
            End Set
        End Property
        Private mCanReIssueDocuments As Boolean

        Public Property CanSetInspectorateDecision() As Boolean
            Get
                Return mCanSetInspectorateDecision
            End Get
            Set(ByVal Value As Boolean)
                'readonly
            End Set
        End Property
        Private mCanSetInspectorateDecision As Boolean

        Public Property CanCreateAdditionalRingRequest() As Boolean
            Get
                Return mCanCreateAdditionalRingRequest
            End Get
            Set(ByVal Value As Boolean)
                'readonly
            End Set
        End Property
        Private mCanCreateAdditionalRingRequest As Boolean

        Public Property CanSetPaidFlag() As Boolean
            Get
                Return mCanSetPaidFlag
            End Get
            Set(ByVal Value As Boolean)
                'readonly
            End Set
        End Property
        Private mCanSetPaidFlag As Boolean

        ' can only be one party
        Public Property PartyId() As Int32
            Get
                Return mPartyId
            End Get
            Set(ByVal Value As Int32)
                mPartyId = Value
            End Set
        End Property
        Private mPartyId As Int32

        Private Function PartyName() As String
            Dim Result As String = String.Empty
            If PartyId > 0 Then
                Dim BOParty As New Party.BOParty(PartyId)
                If BOParty.IsBusiness Then
                    BOParty = New Party.BOPartyBusiness(PartyId)
                Else
                    BOParty = New Party.BOPartyIndividual(PartyId)
                End If
                Result = BOParty.DisplayName().Trim
            End If
            Return Result
        End Function

        Public Property PartyAddress() As String
            Get
                Dim Result As String = String.Empty
                Dim PartyAddresses As BO.Party.BOReadOnlyAddress = GetPartyAddresses()
                If Not PartyAddresses Is Nothing Then
                    Result = String.Concat(PartyName, ". ", PartyAddresses.OrdinaryAddress)
                End If
                Return Result
            End Get
            Set(ByVal Value As String)
                'readonly only for proxy
            End Set
        End Property

        Public Property PartyReportAddress() As String
            Get
                Dim Result As String = String.Empty
                Dim PartyAddresses As BO.Party.BOReadOnlyAddress = GetPartyAddresses()
                If Not PartyAddresses Is Nothing Then
                    Result = PartyAddresses.ReportAddress
                End If
                Return Result
            End Get
            Set(ByVal Value As String)
                'readonly only for proxy
            End Set
        End Property

        Private Function GetPartyAddresses() As BO.Party.BOReadOnlyAddress
            Dim Result As BO.Party.BOReadOnlyAddress = Nothing
            If Not mPartyAddressId Is Nothing AndAlso CType(mPartyAddressId, Int32) > 0 Then
                Dim PartyAdd As New BO.Party.BOReadOnlyAddress(CType(mPartyAddressId, Int32))
                If Not PartyAdd Is Nothing Then Result = PartyAdd
            End If
            Return Result
        End Function

        Public Property PartyAddressId() As Object
            Get
                Return mPartyAddressId
            End Get
            Set(ByVal Value As Object)
                mPartyAddressId = Value
            End Set
        End Property
        Private mPartyAddressId As Object

        Public Property AddressReason() As Object
            Get
                Return mAddressReason
            End Get
            Set(ByVal Value As Object)
                mAddressReason = Value
            End Set
        End Property
        Private mAddressReason As Object

        Public Property RegApplicationType() As RegistrationApplicationType
            Get
                Return mRegApplicationType
            End Get
            Set(ByVal Value As RegistrationApplicationType)
                mRegApplicationType = Value
            End Set
        End Property
        Private mRegApplicationType As RegistrationApplicationType = RegistrationApplicationType.Clutch

        Public Property ApplicationMethodId() As Int32
            Get
                Return mApplicationMethodId
            End Get
            Set(ByVal Value As Int32)
                mApplicationMethodId = Value
            End Set
        End Property
        Private mApplicationMethodId As Int32

        Public Property DORApplicationMethodId() As Int32
            Get
                Return mDORApplicationMethodId
            End Get
            Set(ByVal Value As Int32)
                mDORApplicationMethodId = Value
            End Set
        End Property
        Private mDORApplicationMethodId As Int32

        'Public Overrides Property ApplicationId() As Int32
        '    Get
        '        Return mApplicationId
        '    End Get
        '    Set(ByVal Value As Int32)
        '        mApplicationId = Value
        '    End Set
        'End Property
        'Private mApplicationId As Int32

        Public Property KeeperAcknowledgment() As Object
            Get
                Return mKeeperAcknowledgment
            End Get
            Set(ByVal Value As Object)
                mKeeperAcknowledgment = Value
            End Set
        End Property
        Private mKeeperAcknowledgment As Object

        Public Property IsInspectionRequired() As Boolean
            Get
                Return mIsInspectionRequired
            End Get
            Set(ByVal Value As Boolean)
                mIsInspectionRequired = Value
            End Set
        End Property
        Private mIsInspectionRequired As Boolean = True

        Public Property InspectorDecisionMade() As Boolean
            Get
                Return mInspectorDecisionMade
            End Get
            Set(ByVal Value As Boolean)
                mInspectorDecisionMade = Value
            End Set
        End Property
        Private mInspectorDecisionMade As Boolean

        Public Property ApplicationStatus() As BOPermitInfo.PermitStatusTypes
            Get
                Return mApplicationStatus
            End Get
            Set(ByVal Value As BOPermitInfo.PermitStatusTypes)
                mApplicationStatus = Value
            End Set
        End Property
        Private mApplicationStatus As BOPermitInfo.PermitStatusTypes

        'Public Overrides Property PaymentStatus() As PaymentStatusTypes
        '    Get
        '        Return mPaymentStatus
        '    End Get
        '    Set(ByVal Value As PaymentStatusTypes)
        '        mPaymentStatus = Value
        '    End Set
        'End Property
        'Private mPaymentStatus As PaymentStatusTypes

        Public Property SpecialRequirements() As String
            Get
                If mSpecialRequirements Is Nothing Then
                    mSpecialRequirements = String.Empty
                End If
                Return mSpecialRequirements
            End Get
            Set(ByVal Value As String)
                mSpecialRequirements = Value
            End Set
        End Property
        Private mSpecialRequirements As String

        Public Property IsUnderEighteen() As Boolean
            Get
                Return mIsUnderEighteen
            End Get
            Set(ByVal Value As Boolean)
                mIsUnderEighteen = Value
            End Set
        End Property
        Private mIsUnderEighteen As Boolean

        Public Property SpecialPenalty() As Boolean
            Get
                Return mSpecialPenalty
            End Get
            Set(ByVal Value As Boolean)
                mSpecialPenalty = Value
            End Set
        End Property
        Private mSpecialPenalty As Boolean

        Public Property OtherAnimalOffence() As Boolean
            Get
                Return mOtherAnimalOffence
            End Get
            Set(ByVal Value As Boolean)
                mOtherAnimalOffence = Value
            End Set
        End Property
        Private mOtherAnimalOffence As Boolean

        Public Property TrueAndCorrect() As Boolean
            Get
                Return mTrueAndCorrect
            End Get
            Set(ByVal Value As Boolean)
                mTrueAndCorrect = Value
            End Set
        End Property
        Private mTrueAndCorrect As Boolean

        Public Property Convictions() As Conviction()
            Get
                Return mConvictions
            End Get
            Set(ByVal Value As Conviction())
                mConvictions = Value
            End Set
        End Property
        Private mConvictions As Conviction()

        Public Property RegistrationApplication() As BaseBird
            Get
                Select Case RegApplicationType
                    Case RegistrationApplicationType.Clutch
                        Return TypeClutch
                    Case RegistrationApplicationType.Found
                        Return TypeAdultFound
                    Case RegistrationApplicationType.Imported
                        Return TypeAdultImported
                    Case RegistrationApplicationType.Other  'MLD 7/1/5 uncommented
                        Return TypeAdultOther
                    Case RegistrationApplicationType.Bred
                        Return TypeAdultBred
                    Case Else
                        Throw New NotImplementedException("Type " & ApplicationType.ToString & " does not exist")
                End Select
            End Get
            Set(ByVal Value As BaseBird)
                If Not Value Is Nothing Then
                    If TypeOf Value Is Clutch Then
                        mTypeClutch = CType(Value, Clutch)
                    ElseIf TypeOf Value Is AdultFound Then
                        mTypeAdultFound = CType(Value, AdultFound)
                    ElseIf TypeOf Value Is AdultImported Then
                        mTypeAdultImported = CType(Value, AdultImported)
                    ElseIf TypeOf Value Is AdultOther Then    'MLD 7/1/5 uncommented
                        mTypeAdultOther = CType(Value, AdultOther)
                    ElseIf TypeOf Value Is AdultBred Then
                        mTypeAdultBred = CType(Value, AdultBred)
                    End If
                End If
            End Set
        End Property

        Private Property TypeClutch() As Clutch
            Get
                If mTypeClutch Is Nothing Then mTypeClutch = New Clutch
                Return mTypeClutch
            End Get
            Set(ByVal Value As Clutch)
                mTypeClutch = Value
            End Set
        End Property
        Private mTypeClutch As Clutch

        Private Property TypeAdultImported() As AdultImported
            Get
                If mTypeAdultImported Is Nothing Then mTypeAdultImported = New AdultImported
                Return mTypeAdultImported
            End Get
            Set(ByVal Value As AdultImported)
                mTypeAdultImported = Value
            End Set
        End Property
        Private mTypeAdultImported As AdultImported

        Private Property TypeAdultFound() As AdultFound
            Get
                If mTypeAdultFound Is Nothing Then mTypeAdultFound = New AdultFound
                Return mTypeAdultFound
            End Get
            Set(ByVal Value As AdultFound)
                mTypeAdultFound = Value
            End Set
        End Property
        Private mTypeAdultFound As AdultFound

        Private Property TypeAdultBred() As AdultBred
            Get
                If mTypeAdultBred Is Nothing Then mTypeAdultBred = New AdultBred
                Return mTypeAdultBred
            End Get
            Set(ByVal Value As AdultBred)
                mTypeAdultBred = Value
            End Set
        End Property
        Private mTypeAdultBred As AdultBred

        Private Property TypeAdultOther() As AdultOther
            Get
                If mTypeAdultOther Is Nothing Then mTypeAdultOther = New AdultOther
                Return mTypeAdultOther
            End Get
            Set(ByVal Value As AdultOther)
                mTypeAdultOther = Value
            End Set
        End Property
        Private mTypeAdultOther As AdultOther

        Public Property Parents() As Parents
            Get
                If mParents Is Nothing Then mParents = New Parents
                Return mParents
            End Get
            Set(ByVal Value As Parents)
                mParents = Value
            End Set
        End Property
        Private mParents As Parents

        Public Property AssignedTo() As Common.AssignedToList
            Get
                Return mAssignedTo
            End Get
            Set(ByVal Value As Common.AssignedToList)
                mAssignedTo = Value
            End Set
        End Property
        Private mAssignedTo As Common.AssignedToList

        Public Property DORReceivedDate() As Object
            Get
                Return mDORReceivedDate
            End Get
            Set(ByVal Value As Object)
                mDORReceivedDate = Value
            End Set
        End Property
        Private mDORReceivedDate As Object
        Private mOrigDORReceivedDate As Object

        'Public Overrides Property ReceivedDate() As Date
        '    Get
        '        Return mReceivedDate
        '    End Get
        '    Set(ByVal Value As Date)
        '        mReceivedDate = Value
        '    End Set
        'End Property
        'Private mReceivedDate As Date

        Public Property CancellationReason() As String
            Get
                If mApplicationStatus = ApplicationStatus.Cancelled OrElse _
                   mApplicationStatus = ApplicationStatus.CancelPending Then
                    Return mCancellationReason
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal Value As String)
                mCancellationReason = Value
            End Set
        End Property
        Private mCancellationReason As String

        Public Property CancellationRefusalReason() As String
            Get
                Return mCancellationRefusalReason
            End Get
            Set(ByVal Value As String)
                mCancellationRefusalReason = Value
            End Set
        End Property
        Private mCancellationRefusalReason As String

        Public Property ReasonForEggsButNoParent() As String
            Get
                Return mReasonForEggsButNoParent
            End Get
            Set(ByVal Value As String)
                mReasonForEggsButNoParent = Value
            End Set
        End Property
        Private mReasonForEggsButNoParent As String

        Public Property RefuseLetterReportId() As Int32
            Get
                Return mRefuseLetterReportId
            End Get
            Set(ByVal Value As Int32)
                mRefuseLetterReportId = Value
            End Set
        End Property
        Private mRefuseLetterReportId As Int32

        Public Property SubmittedDate() As Date
            Get
                Return mSubmittedDate
            End Get
            Set(ByVal Value As Date)
                mSubmittedDate = Value
            End Set
        End Property
        Private mSubmittedDate As Date

        Public Property DeclineReason() As String
            Get
                Return mDeclineReason
            End Get
            Set(ByVal Value As String)
                mDeclineReason = Value
            End Set
        End Property
        Private mDeclineReason As String

        Public Property DORPrintJobId() As Int32
            Get
                Return mDORPrintJobId
            End Get
            Set(ByVal Value As Int32)
                mDORPrintJobId = Value
            End Set
        End Property
        Private mDORPrintJobId As Int32

        Public Property NextActionDate() As Object
            Get
                Return mNextActionDate
            End Get
            Set(ByVal Value As Object)
                mNextActionDate = Value
            End Set
        End Property
        Private mNextActionDate As Object

        Public Property SLAStart() As Date
            Get
                Return mSLAStart
            End Get
            Set(ByVal Value As Date)
                mSLAStart = Value
            End Set
        End Property
        Private mSLAStart As Date

        'measured in minutes
        Public Property SLAClock() As Int32
            Get
                Return mSLAClock
            End Get
            Set(ByVal Value As Int32)
                mSLAClock = Value
            End Set
        End Property
        Private mSLAClock As Int32

        'measured in minutes
        Public ReadOnly Property SLARunning() As Boolean
            Get
                Return mSLAStart.Ticks > 0
            End Get
        End Property
#End Region

        '<Serializable()> _
        'Friend Delegate Function GetDSBirdRegDelegate() As BirdRegistrationDataset

        'Friend Function GetDSBirdReg() As BirdRegistrationDataset
        '    Return BirdRegDS
        'End Function

        'Public Property BirdRegDS() As BirdRegistrationDataset
        '    Get
        '        Return mBirdRegDS
        '    End Get
        '    Set(ByVal Value As BirdRegistrationDataset)
        '        mBirdRegDS = Value
        '    End Set
        'End Property
        'Private mBirdRegDS As BirdRegistrationDataset

        'Private Sub ParseXML(ByVal xml As String)
        '    Dim myXmlReader As New System.Xml.XmlTextReader(xml, XmlNodeType.Document, Nothing)
        '    BirdRegDS.ReadXml(myXmlReader)
        'End Sub


        'Private Function GetParty() As BirdRegistrationDataset.PartyRow
        '    'only works if a party exists, otherwise this will error.
        '    Return GetParty(0)
        'End Function

        'Private Function GetParty(ByVal partyId As Int32) As BirdRegistrationDataset.PartyRow
        '    'only adds if one doesn't exist
        '    Dim pr As BirdRegistrationDataset.PartyRow
        '    If BirdRegDS.Party Is Nothing OrElse BirdRegDS.Party.Count = 0 Then
        '        If partyId <= 0 Then
        '            Throw New ArgumentException("Party Id needs to be set")
        '        Else
        '            pr = BirdRegDS.Party.NewPartyRow()
        '            pr.PartyID = partyId
        '            BirdRegDS.Party.AddPartyRow(pr)
        '        End If
        '    Else
        '        pr = BirdRegDS.Party(0)
        '    End If
        '    Return pr
        'End Function

        'Private Function GetRingApplication() As BirdRegistrationDataset.RingApplicationRow
        '    'only works if a application exists, otherwise this will error.
        '    Return GetRingApplication(0)
        'End Function

        'Private Function GetRingApplication(ByVal applicationId As Int32) As BirdRegistrationDataset.RingApplicationRow
        '    'only adds if one doesn't exist
        '    Dim rar As BirdRegistrationDataset.RingApplicationRow
        '    If BirdRegDS.RingApplication Is Nothing OrElse BirdRegDS.RingApplication.Count = 0 Then
        '        If applicationId <= 0 Then
        '            Throw New ArgumentException("Application Id needs to be set")
        '        Else
        '            rar = BirdRegDS.RingApplication.NewRingApplicationRow()
        '            'set defaults
        '            With rar
        '                .Source = System.Enum.GetName(GetType(SourceList), SourceList.Email)
        '                .ApplicationReference = applicationId
        '                .IsInspectionRequired = False
        '                .ApplicationStatus = 0
        '                .PaymentStatus = 0
        '            End With
        '            BirdRegDS.RingApplication.AddRingApplicationRow(rar)
        '        End If
        '    Else
        '        rar = BirdRegDS.RingApplication(0)
        '    End If
        '    Return rar
        'End Function

        'Private Function GetDeclaration() As BirdRegistrationDataset.DeclarationRow
        '    'only adds if one doesn't exist
        '    Dim dr As BirdRegistrationDataset.DeclarationRow
        '    If BirdRegDS.Declaration Is Nothing OrElse BirdRegDS.Declaration.Count = 0 Then
        '        dr = BirdRegDS.Declaration.NewDeclarationRow()
        '        With dr
        '            .SpecialRequirements = String.Empty
        '            .IsUnderEighteen = False
        '            .SpecialPenalty = False
        '            .OtherAnimalOffence = False
        '            .TrueAndCorrect = True
        '        End With
        '        BirdRegDS.Declaration.AddDeclarationRow(dr)
        '    Else
        '        dr = BirdRegDS.Declaration(0)
        '    End If
        '    Return dr
        'End Function

        Public Overrides Property ApplicationTypeId() As Integer
            Get
                If Me.mRegApplicationType = RegistrationApplicationType.Clutch Then
                    Return Application.ApplicationTypes.BirdChick
                Else
                    Return Application.ApplicationTypes.BirdAdult
                End If
            End Get
            Set(ByVal Value As Integer)
            End Set
        End Property

        Public Property RingApplicationId() As Int32
            Get
                Return mRingApplicationId
            End Get
            Set(ByVal Value As Int32)
                mRingApplicationId = Value
            End Set
        End Property
        Private mRingApplicationId As Int32
    End Class
End Namespace

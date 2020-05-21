Imports System.xml
'SCS 04 April 2005 - Removed as the CITES Permit status is being used now.
'Namespace Application
'    <Serializable()> _
'    Public Enum ApplicationStatus
'        DodgyDoNotUseOnlyForProxyGeneration
'        Registered
'        Closed ' Fated
'        Closed_By_Customer ' Fated By Customer
'        Declined
'        Being_Input_By_Customer
'        Being_Input_By_Case_Officer
'        Ring_Request_Submitted_Online
'        Submitted_By_Customer
'        Progress_Allowed
'        Referred
'        Authorised
'        Chick_DOR_Issued
'        Adult_DOR_Issued
'        DOR_Returned
'        Cancel_Pending
'        Cancelled
'    End Enum
'End Namespace

Namespace Application.Bird.Notification
    <Serializable()> _
    Public Class BirdNotification
        Public Sub New()
        End Sub

        Protected Sub New(ByVal id As Int32)    'MLD 2/3/5 now protected
            'load an existing one
            LoadExisting(id)
        End Sub

        'creates a new object and associates the party with it.
        'MLD 9/2/5 added NewApp, 2/3/5 now protected and initialStatus added
        'SCS-7 Mar 2005, Removed initialStatus and replaced with ssoUserId
        Protected Sub LoadByParty(ByVal partyId As Int32, ByVal ssouserId As Int64)
            Dim NewApp As DataObjects.Entity.Application = DataObjects.Entity.Application.Insert(Date.Now, False, Nothing, Date.Now, Nothing, False, False, Nothing, Nothing, Nothing, False, Date.Today, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            Dim NotificationApp As DataObjects.Entity.BirdNotification = DataObjects.Entity.BirdNotification.Insert(String.Empty, NewApp.ApplicationId)

            If Not NotificationApp Is Nothing Then
                LoadNotification(NotificationApp)
                PostCreateApplicationRecord(ssouserId) 'SCS-7 Mar 2005, Added
                Party = New Party(partyId)
            End If
        End Sub

        'SCS-7 Mar 2005, Added to determine initial status
        Private Sub PostCreateApplicationRecord(ByVal ssoUserId As Int64)
            If Common.IsInRole(ssoUserId, Common.RolesList.CaseOfficer) Then
                'user is a case officer so...
                mApplicationStatus = BOPermitInfo.PermitStatusTypes.BeingInput_CaseOfficer
            Else
                mApplicationStatus = BOPermitInfo.PermitStatusTypes.BeingInput_Customer
            End If
        End Sub

        Private Sub LoadExisting(ByVal id As Int32)
            'load an existing one
            Dim ExistingBirdNotification As DataObjects.Entity.BirdNotification = DataObjects.Entity.BirdNotification.GetById(id)
            If Not ExistingBirdNotification Is Nothing Then
                LoadNotification(ExistingBirdNotification)
            End If
        End Sub

        Private Sub LoadNotification(ByVal notification As DataObjects.Entity.BirdNotification)
            Dim XML As String = Nothing
            With notification
                mChecksum = .CheckSum
                mBirdNotificationId = .BirdNotificationId
                mApplicationReference = .ApplicationId          'MLD added 9/2/5
                XML = .ProvisionalXML
            End With

            If Not XML Is Nothing AndAlso XML.Length > 0 Then
                Dim myXmlReader As New System.Xml.XmlTextReader(XML, XmlNodeType.Document, Nothing)
                Dim BirdNotificationDS As New BirdNotificationDataset
                BirdNotificationDS.ReadXml(myXmlReader)
                FillObjects(BirdNotificationDS)
            End If
        End Sub

        Public Shared Function ChangeBirdNotificationFate(ByVal applicationId As Int32, ByVal newFate As Int32, ByVal ssoUserId As Int64) As Boolean
            Dim ReturnStatus As Boolean = False
            'create a notification object as we are shared
            Dim Notification As New BirdNotification
            'load the application
            Notification.LoadExisting(applicationId)

            'they are the same, so presume everything is ok
            If Not Notification.Fate Is Nothing AndAlso Notification.Fate.FateCode = newFate Then
                ReturnStatus = True
            Else
                'replace the existing fate object with a new one
                'default date date to today
                Notification.Fate = New Fate(newFate, Date.Today)

                'save everything
                Notification = Notification.Save()
                '...did it save ok?
                ReturnStatus = (Not Notification Is Nothing)

                'did we save ok and has the application been fated or declined?
                If ReturnStatus AndAlso newFate = BOPermitInfo.PermitStatusTypes.Fate Then
                    ReturnStatus = Notification.Submit()
                End If
            End If
            Return ReturnStatus
        End Function

        Public Shared Function ChangeBirdNotificationStatus(ByVal applicationId As Int32, ByVal newStatus As BOPermitInfo.PermitStatusTypes) As Boolean
            Dim ReturnStatus As Boolean = False
            'create a notification object as we are shared
            Dim Notification As New BirdNotification
            'load the application
            Notification.LoadExisting(applicationId)

            'they are the same, so presume everything is ok
            If Notification.ApplicationStatus = newStatus Then
                ReturnStatus = True
            Else
                'replace the existing status with a new one
                Notification.ApplicationStatus = newStatus

                'save everything
                Notification = Notification.Save()
                '...did it save ok?
                ReturnStatus = (Not Notification Is Nothing)

                'did we save ok and has the application been closed?
                If ReturnStatus AndAlso _
                   (newStatus = BOPermitInfo.PermitStatusTypes.Fate OrElse _
                   newStatus = BOPermitInfo.PermitStatusTypes.Closed_By_Customer OrElse _
                   newStatus = BOPermitInfo.PermitStatusTypes.Refused) Then
                    ReturnStatus = Notification.Submit()
                End If
            End If
            Return ReturnStatus
        End Function

        <Serializable()> _
        Public Class KeeperHistory
            Public Sub New()
            End Sub

            Public Sub New(ByVal keeperId As Int32, ByVal fromDate As Date, ByVal toDate As Date)
                Me.KeeperId = keeperId
                Me.FromDate = fromDate
                Me.ToDate = toDate
            End Sub

            Public KeeperId As Int32
            Public FromDate As Date
            Public ToDate As Date
        End Class

        Public Function GetKeeperHistory() As KeeperHistory()
            'DataObjects.Entity.PartySpecimen.ServiceObject.get()
        End Function

        Public Function Submit() As Boolean
            Dim Result As Boolean = False
            If NotifiedBird.Length > 0 Then
                For Each Bird As Notification.NotifiedBird In NotifiedBird
                    If Bird.NotifiedSpecimen.SpecimenId > 0 Then
                        Dim Specimen As New DataObjects.Entity.Specimen(Bird.NotifiedSpecimen.SpecimenId, Nothing)
                        If Specimen.IsFateIdNull Then
                            'it has already been fated so no point in updating the info
                            If Fate.FateDate Is Nothing OrElse Not TypeOf Fate.FateDate Is Date Then
                                'not been set so set to today
                                Specimen.FateDate = Date.Today
                            Else
                                Specimen.FateDate = CType(Fate.FateDate, Date)
                            End If
                            Specimen.FateId = Fate.FateCode
                        End If
                    End If
                Next Bird
                Result = True
            End If
            Return Result
        End Function

        Public Function Save() As Application.Bird.Notification.BirdNotification
            Dim SavedNotification As Application.Bird.Notification.BirdNotification = Nothing
            Dim BirdNotificationDS As BirdNotificationDataset = FillDS()
            If Not BirdNotificationDS Is Nothing Then
                Dim sw As New IO.StringWriter
                BirdNotificationDS.WriteXml(sw)
                Dim XML As String = sw.GetStringBuilder().ToString()
                ' dispose the object
                sw.Close()
                sw = Nothing
                Dim NewNotification As New DataObjects.Entity.BirdNotification
                NewNotification.CreateEmptyEntity()
                With NewNotification
                    .CheckSum = mChecksum
                    .ProvisionalXML = XML
                    .BirdNotificationId = mBirdNotificationId
                    .ApplicationId = mApplicationReference
                End With
                If NewNotification.SaveChanges() Then SavedNotification = Me
            End If
            Return SavedNotification
        End Function

        Private Function FillDS() As BirdNotificationDataset
            Dim BirdNotificationDS As New BirdNotificationDataset
            If Not mParty Is Nothing Then
                Dim nr As BirdNotificationDataset.BirdNotificationRow = BirdNotificationDS.BirdNotification.NewBirdNotificationRow()
                With nr
                    .ApplicationReference = mApplicationReference
                    .ApplicationStatus = mApplicationStatus
                    .ReleaseDetailsToCaller = mReleaseDetailsToCaller
                    If Not mAdditionalInformation Is Nothing AndAlso TypeOf mAdditionalInformation Is String Then .AdditionalInformation = mAdditionalInformation Else .SetAdditionalInformationNull()
                End With
                BirdNotificationDS.BirdNotification.AddBirdNotificationRow(nr)

                'get the fates
                If Not mFate Is Nothing Then mFate.GetData(nr)
                If Not mKeeper Is Nothing Then mKeeper.GetData(nr)
                If Not mParty Is Nothing Then mParty.GetData(nr)

                If Not mNotifiedBird Is Nothing AndAlso mNotifiedBird.Length > 0 Then
                    Dim NotifiedBird As BirdNotificationDataset.NotifiedBirdRow = BirdNotificationDS.NotifiedBird.NewNotifiedBirdRow
                    NotifiedBird.BirdNotificationRow = nr
                    BirdNotificationDS.NotifiedBird.AddNotifiedBirdRow(NotifiedBird)

                    For Each nBird As NotifiedBird In mNotifiedBird
                        If Not nBird.NotifiedIdMark Is Nothing Then
                            Dim NotifiedIdMarkRow As BirdNotificationDataset.NotifiedIDMarkRow = BirdNotificationDS.NotifiedIDMark.NewNotifiedIDMarkRow
                            NotifiedIdMarkRow.NotifiedBirdRow = NotifiedBird
                            nBird.NotifiedIdMark.PopulateIDMark(CType(NotifiedIdMarkRow, DataRow))
                        End If

                        If Not nBird.NotifiedSpecimen Is Nothing Then
                            Dim NotifiedSpecimenRow As BirdNotificationDataset.NotifiedSpecimenRow = BirdNotificationDS.NotifiedSpecimen.NewNotifiedSpecimenRow
                            NotifiedSpecimenRow.NotifiedBirdRow = NotifiedBird
                            nBird.NotifiedSpecimen.UpdateSpecimen(CType(NotifiedSpecimenRow, DataRow))
                            If Not nBird.CustomerEnteredArticle10Reference Is Nothing Then NotifiedSpecimenRow.CustomerEnteredArticle10Reference = nBird.CustomerEnteredArticle10Reference Else NotifiedSpecimenRow.SetCustomerEnteredArticle10ReferenceNull()
                            BirdNotificationDS.NotifiedSpecimen.AddNotifiedSpecimenRow(NotifiedSpecimenRow)
                        End If
                    Next nBird

                End If
            End If
            Return BirdNotificationDS
        End Function

        Private Sub FillObjects(ByVal birdNotificationDS As BirdNotificationDataset)
            'TODO: Store and retrieve these values

            If Not birdNotificationDS.BirdNotification Is Nothing AndAlso birdNotificationDS.BirdNotification.Count > 0 Then
                Dim bn As BirdNotificationDataset.BirdNotificationRow = birdNotificationDS.BirdNotification(0)
                mReleaseDetailsToCaller = bn.ReleaseDetailsToCaller
                If Not bn.IsAdditionalInformationNull Then mAdditionalInformation = bn.AdditionalInformation.ToString Else mAdditionalInformation = Nothing
                mApplicationReference = bn.ApplicationReference
                mApplicationStatus = CType(bn.ApplicationStatus, BOPermitInfo.PermitStatusTypes)

                Dim fr As BirdNotificationDataset.FateRow() = bn.GetFateRows
                If fr.Length > 0 Then
                    mFate = New Fate(fr(0))
                Else
                    mFate = New Fate
                End If

                Dim bk As BirdNotificationDataset.NewKeeperRow() = bn.GetNewKeeperRows()
                If bk.Length > 0 Then
                    mKeeper = New Keeper(bk(0))
                Else
                    'can be null
                    mKeeper = Nothing
                End If

                'setup party info
                Dim p As BirdNotificationDataset.PartyRow() = bn.GetPartyRows
                If p.Length > 0 Then
                    mParty = New Party(p(0))
                Else
                    'can be null
                    mParty = Nothing
                End If

                Dim nbs As BirdNotificationDataset.NotifiedBirdRow() = bn.GetNotifiedBirdRows
                If nbs.Length > 0 Then
                    For Each nb As BirdNotificationDataset.NotifiedBirdRow In nbs
                        Dim NewBird As NotifiedBird = AddNotifiedBird()
                        With NewBird
                            Dim ids As BirdNotificationDataset.NotifiedIDMarkRow() = nb.GetNotifiedIDMarkRows
                            If ids.Length > 0 Then
                                .NotifiedIdMark = New Registration.IDMark(ids(0))
                            Else
                                .NotifiedIdMark = New Registration.IDMark
                            End If

                            .CustomerEnteredArticle10Reference = String.Empty

                            Dim specs As BirdNotificationDataset.NotifiedSpecimenRow() = nb.GetNotifiedSpecimenRows
                            If specs.Length > 0 Then
                                'create an object to put the items in
                                .NotifiedSpecimen = New Registration.SpecimenType
                                Registration.SpecimenType.CreateSpecimen(.NotifiedSpecimen, specs(0))
                                If Not specs(0).IsCustomerEnteredArticle10ReferenceNull Then .CustomerEnteredArticle10Reference = specs(0).CustomerEnteredArticle10Reference
                            Else
                                .NotifiedSpecimen = New Registration.SpecimenType
                            End If
                        End With
                    Next nb
                Else
                    mNotifiedBird = CType(Array.CreateInstance(GetType(NotifiedBird), 0), NotifiedBird())
                End If
            Else
                'null everything
                mReleaseDetailsToCaller = False
                mFate = New Fate
                mKeeper = Nothing
                mNotifiedBird = CType(Array.CreateInstance(GetType(NotifiedBird), 0), NotifiedBird())
            End If
        End Sub

        Public Function AddNotifiedBird() As NotifiedBird
            Dim Success As NotifiedBird = Nothing
            Dim Upper As Int32 = 0
            If Not mNotifiedBird Is Nothing Then
                Upper = mNotifiedBird.Length
            End If
            ReDim Preserve mNotifiedBird(Upper)
            mNotifiedBird(Upper) = New NotifiedBird
            Success = mNotifiedBird(Upper)
            Return Success
        End Function

#Region " Properties "
        ' can only be one party
        Public Property Party() As Party
            Get
                Return mParty
            End Get
            Set(ByVal Value As Party)
                mParty = Value
            End Set
        End Property
        Private mParty As Party

        Public Property Checksum() As Int32
            Get
                Return mChecksum
            End Get
            Set(ByVal Value As Int32)
                mChecksum = Value
            End Set
        End Property
        Private mChecksum As Int32

        Public Property BirdNotificationId() As Int32
            Get
                Return mBirdNotificationId
            End Get
            Set(ByVal Value As Int32)
                mBirdNotificationId = Value
            End Set
        End Property
        Private mBirdNotificationId As Int32

        Public Property ApplicationReference() As Int32
            Get
                Return mApplicationReference
            End Get
            Set(ByVal Value As Int32)
                mApplicationReference = Value
            End Set
        End Property
        Private mApplicationReference As Int32

        Public Property ApplicationStatus() As BOPermitInfo.PermitStatusTypes
            Get
                Return mApplicationStatus
            End Get
            Set(ByVal Value As BOPermitInfo.PermitStatusTypes)
                mApplicationStatus = Value
            End Set
        End Property
        Private mApplicationStatus As BOPermitInfo.PermitStatusTypes

        Public Property ReleaseDetailsToCaller() As Boolean
            Get
                Return mReleaseDetailsToCaller
            End Get
            Set(ByVal Value As Boolean)
                mReleaseDetailsToCaller = Value
            End Set
        End Property
        Private mReleaseDetailsToCaller As Boolean

        Public Property AdditionalInformation() As String
            Get
                Return mAdditionalInformation
            End Get
            Set(ByVal Value As String)
                mAdditionalInformation = Value
            End Set
        End Property
        Private mAdditionalInformation As String

        Public Property Fate() As Fate
            Get
                Return mFate
            End Get
            Set(ByVal Value As Fate)
                mFate = Value
            End Set
        End Property
        Private mFate As Fate

        Public Property Keeper() As Keeper
            Get
                Return mKeeper
            End Get
            Set(ByVal Value As Keeper)
                mKeeper = Value
            End Set
        End Property
        Private mKeeper As Keeper

        Public Property NotifiedBird() As NotifiedBird()
            Get
                Return mNotifiedBird
            End Get
            Set(ByVal Value As NotifiedBird())
                mNotifiedBird = Value
            End Set
        End Property
        Private mNotifiedBird As NotifiedBird()

#Region " Helper Properties"
        Public Property ApplicationStatus_Helper() As String
            Get
                If mApplicationStatus > 0 Then
                    Return System.Enum.GetName(GetType(BOPermitInfo.PermitStatusTypes), mApplicationStatus).ToString.Replace("_", " ")
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal Value As String)
                'only here for proxy!
            End Set
        End Property
#End Region
#End Region
    End Class

    <Serializable()> _
    Public Class BirdFateNotification
        Inherits BirdNotification               'MLD 2/3/5 new subclass added

        Public Sub New()
        End Sub

        Public Sub New(ByVal id As Int32)
            MyBase.New(id)
        End Sub

        'SCS-7 Mar 2005, Removed initialStatus and replaced with ssoUserId
        Public Shared Function CreateNotification(ByVal partyId As Int32, ByVal ssouserId As Int64) As BirdFateNotification    'MLD 2/3/5 moved to subclass
            Dim app As New BirdFateNotification
            app.LoadByParty(partyId, ssouserId)
            Return app
        End Function
    End Class

    <Serializable()> _
    Public Class BirdTransferNotification
        Inherits BirdNotification               'MLD 2/3/5 new subclass added

        Public Sub New()
        End Sub

        Public Sub New(ByVal id As Int32)
            MyBase.New(id)
        End Sub

        'SCS-7 Mar 2005, Removed initialStatus and replaced with ssoUserId
        Public Shared Function CreateNotification(ByVal partyId As Int32, ByVal ssouserId As Int64) As BirdTransferNotification    'MLD 2/3/5 moved to subclass
            Dim app As New BirdTransferNotification
            app.LoadByParty(partyId, ssouserId)
            Return app
        End Function

        Public Function CheckTransferDetails() As ValidationManager
            If Not Party Is Nothing AndAlso Not Keeper Is Nothing Then
                Dim Result As TimeSpan = Date.op_Subtraction(Party.TransferDate, Keeper.TransferDate)
                Const DayCheck As Int32 = 7
                If Result.Days > DayCheck Or Result.Days < DayCheck Then
                    Dim Extra As New Collections.Specialized.NameValueCollection
                    Extra.Add("%1", "transfer")
                    Extra.Add("%2", DayCheck.ToString)
                    AddValidationError(ValidationError.ValidationCodes.TheDatesDifferByMoreThanXDays, Extra)
                End If

                If String.Compare(Party.TransferMethod_Helper, Keeper.TransferMethod_Helper) <> 0 Then
                    AddValidationError(ValidationError.ValidationCodes.MethodsOfTransferNotificationDiffer)
                End If
            End If

            If Not NotifiedBird Is Nothing Then
                Dim AddMessage As Boolean = True
                For Each Bird As NotifiedBird In NotifiedBird
                    If Not Bird.CustomerEnteredArticle10Reference Is Nothing AndAlso Bird.CustomerEnteredArticle10Reference.Length > 0 Then
                        'the customer has entered an article 10 certificate

                        'load the article 10 certificate
                        Dim Results As Registration.Search.Article10SearchResult() = Registration.BirdRegistration.SearchForArticle10(Bird.CustomerEnteredArticle10Reference)
                        If Results.Length > 0 Then
                            'if we get anything back then it's fine...
                            For Each Item As Registration.Search.Article10SearchResult In Results
                                Dim PermitId As Int32 = Item.PermitId
                                Dim Service As New DataObjects.Service.PermitSpecimenService
                                Dim EntitySet As DataObjects.EntitySet.PermitSpecimenSet = Service.GetForPermit(PermitId)
                                For Each Link As DataObjects.Entity.ParentSpecimen In EntitySet
                                    'check the specimens
                                    If Bird.NotifiedSpecimen.SpecimenId = Link.SpecimenID Then
                                        AddMessage = False
                                        Exit For
                                    End If
                                Next Link
                                'we're trying to bail, so continue out...
                                If Not AddMessage Then Exit For
                            Next Item
                        Else
                            'can't find the article 10
                        End If
                    End If
                Next Bird
                If AddMessage Then AddValidationError(ValidationError.ValidationCodes.InvalidArticle10)
            End If
        End Function

        Friend Sub AddValidationError(ByVal [error] As ValidationError.ValidationCodes)
            AddValidationError([error], Nothing)
        End Sub

        Friend Sub AddValidationError(ByVal [error] As ValidationError.ValidationCodes, ByVal extraMessageInfo As Collections.Specialized.NameValueCollection)
            If mValidationErrors Is Nothing Then
                mValidationErrors = New ValidationManager(ValidationManager.ValidationTitles.CannotSaveApplication)
            End If
            mValidationErrors.AddError(New ValidationError([error], extraMessageInfo))
        End Sub
        Private mValidationErrors As ValidationManager
    End Class
End Namespace
Imports uk.gov.defra.Phoenix.DO.DataObjects
Imports uk.gov.defra.Phoenix.BO.Application.Bird.Registration

Namespace Application
    Public Class BOApplication
        Inherits BaseBO
        Implements IApplication

#Region " Prelim code "
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal applicationId As Int32, ByVal userid As Int32, ByVal tran As SqlClient.SqlTransaction)
            MyClass.New()
            mSSOUserId = userid
            LoadApplication(applicationId, tran)
        End Sub

        Public Sub New(ByVal applicationId As Int32, ByVal userid As Int32)
            MyClass.New(applicationId, userid, Nothing)
            'LoadApplication(applicationId, Nothing)
        End Sub

        Private Function LoadApplication(ByVal id As Int32) As DataObjects.Entity.Application
            Return LoadApplication(id, Nothing)
        End Function

        Protected Function LoadApplication(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.Application
            Dim NewApplication As DataObjects.Entity.Application = DataObjects.Entity.Application.GetById(id, tran)
            If NewApplication Is Nothing Then
                Throw New RecordDoesNotExist("Application", id)
            Else
                InitialiseApplication(NewApplication, tran)
                Return NewApplication
            End If
        End Function

        Protected Overridable Sub InitialiseApplication(ByVal application As DataObjects.Entity.Application, ByVal tran As SqlClient.SqlTransaction)
            With application
                mApplicationId = .Id
                CheckSum = .CheckSum

                If Not .IsAgentLinkIdNull Then
                    mAgent = New application.BOApplicationPartyDetails(.AgentLinkId, tran)
                End If
                If Not .IsPartyLinkIdNull Then
                    mParty = New application.BOApplicationPartyDetails(.PartyLinkId, tran)
                End If

                If Not .IsApplicationMethodIdNull Then
                    mApplicationMethod = New ReferenceData.BOApplicationMethod(.ApplicationMethodId, tran)
                End If

                mDateOfApplication = .ApplicationDate
                mIsSemiComplete = .SemiComplete
                mIsSubmitted = .Submitted
                mRetrospective = .Retrospective
                mValidated = .Validated
                mReceivedDate = .RecievedDate
                If Not .IsPaymentStatusIdNull Then mPaymentStatus = CType(.PaymentStatusId, PaymentStatusTypes)
                If Not .IsStandardFeeNull Then mStandardFee = .StandardFee
                If Not .IsFeeChargedNull Then mFeeCharged = .FeeCharged
                Try
                    mCreatedInfo = New Stamp(.CreatedBy, .CreatedDate)
                Catch ex As Exception
                    ' no created info - so set an empty object
                    mCreatedInfo = Nothing
                End Try
                'Me.ApplicationTypeId = GetApplicationTypeID()
                'TODO: Add specimen load.
                'check for permits
                SetPermits(tran)
                If Not .IsOwnerIdNull Then mOwnerId = CType(.OwnerId, Int64)
            End With
        End Sub



        Public Sub SetPermits(ByVal tran As SqlClient.SqlTransaction)
            Dim Permit As New DataObjects.Entity.Permit
            Dim Permits As DataObjects.EntitySet.PermitSet = Permit.GetForApplication(mApplicationId, tran)
            If Not Permits Is Nothing AndAlso _
            Permits.Count > 0 Then
                ReDim mPermit(Permits.Count - 1)
                Dim Index As Int32 = 0
                For Each Permit In Permits
                    Dim BOPerm As New BOPermit
                    ' BOPerm.InitialisePermit(Permit, tran)
                    mPermit(Index) = BOPerm.PolymorphicCreate(Permit.PermitId, tran)
                    Index += 1
                Next Permit
            Else
                mPermit = Nothing
            End If
            Permit = Nothing
            Permits = Nothing
        End Sub
#End Region

#Region " Properties "

        Public Property SSOUserId() As Int64
            Get
                Return mSSOUserId
            End Get
            Set(ByVal Value As Int64)
                mSSOUserId = Value
            End Set
        End Property

        Private mSSOUserId As Int64

        Public Property CanUpdateApplication() As Boolean
            Get
                If SSOUserId > 0 Then
                    If Permit Is Nothing OrElse Me.Permit.Length = 0 Then
                        '   Me.SetPermits(Nothing)
                    End If
                    If Not Permit Is Nothing AndAlso Me.Permit.Length > 0 Then
                        For Each Permit As BO.Application.BOPermit In Me.Permit
                            If Permit.CanUpdatePermit(SSOUserId, Permit.PermitId) Then
                                Return True
                            End If
                        Next
                    End If
                Else
                    Return False
                End If
            End Get
            Set(ByVal Value As Boolean)

            End Set
        End Property

        Public Property CanViewApplication() As Boolean
            Get
                If SSOUserId() > 0 Then
                    If Permit Is Nothing OrElse Me.Permit.Length = 0 Then
                        '    Me.SetPermits(Nothing)
                    End If
                    If Not Permit Is Nothing AndAlso Me.Permit.Length > 0 Then
                        Dim PIs As BO.Application.BOPermitInfo() = Permit(0).GetPermitInfos(Nothing)
                        Return PIs(0).CanViewApplication(SSOUserId)
                    Else
                        Return False
                    End If
                End If
            End Get
            Set(ByVal Value As Boolean)

            End Set
        End Property

        Public Property PaidDate() As Object Implements IApplication.PaidDate
            Get
                Return mPaidDate
            End Get
            Set(ByVal Value As Object)
                mPaidDate = Value
            End Set
        End Property
        Private mPaidDate As Object

        Public Property ApplicationType() As String Implements IApplication.ApplicationType 'MLD altered 16/12/4
            Get
                Try
                    Dim DOApplicationType As New [DO].DataObjects.Entity.ApplicationType(ApplicationTypeId)
                    Return DOApplicationType.Description
                Catch
                    Return "Unknown"
                End Try
            End Get
            Set(ByVal Value As String)

            End Set
        End Property

        Public Overridable Property OwnerString() As String
            Get
                Dim Result As String = String.Empty
                If mOwnerId > 0 Then
                    Result = New BO.BOAuthorisedUser(mOwnerId).FullName
                End If
                Return Result
            End Get
            Set(ByVal Value As String)

            End Set
        End Property

        Public Overridable Property OwnerId() As Int64 Implements IApplication.OwnerId
            Get
                Return mOwnerId
            End Get
            Set(ByVal Value As Int64)
                mOwnerId = Value
            End Set
        End Property
        Private mOwnerId As Int64

        'Public Property CaseOfficer() As String Implements IApplication.CaseOfficer
        '    Get
        '        Return mCaseOfficer
        '    End Get
        '    Set(ByVal Value As String)
        '        mCaseOfficer = Value
        '    End Set
        'End Property
        'Private mCaseOfficer As String

        'Public Property Customer() As String Implements IApplication.Customer
        '    Get
        '        Return mCustomer
        '    End Get
        '    Set(ByVal Value As String)
        '        mCustomer = Value
        '    End Set
        'End Property
        'Private mCustomer As String

        Public Property ReceivedDate() As Date Implements IApplication.ReceivedDate 'MLD 1/2/5 changed to Date, and spelling fixed
            Get
                Return mReceivedDate
            End Get
            Set(ByVal Value As Date)
                mReceivedDate = Value
            End Set
        End Property
        Private mReceivedDate As Date

        Public Overridable Property ApplicationTypeId() As Int32 Implements IApplication.ApplicationTypeId  'MLD changed 16/12/4
            Get
                Throw New Exception("Subclasses must override ApplicationTypeId")
            End Get
            Set(ByVal Value As Int32)
            End Set
        End Property

        Public Property Validated() As Boolean Implements IApplication.Validated
            Get
                Return mValidated
            End Get
            Set(ByVal Value As Boolean)
                mValidated = Value
            End Set
        End Property
        Private mValidated As Boolean

        Public Property Permit() As BOPermit() Implements IApplication.Permit
            Get
                Return mPermit
            End Get
            Set(ByVal Value() As BOPermit)
                mPermit = Value
            End Set
        End Property
        Private mPermit As BOPermit()

        Public Property IsIncomplete() As Boolean Implements IApplication.IsInComplete
            Get
                Return mIsIncomplete
            End Get
            Set(ByVal Value As Boolean)
                mIsIncomplete = Value
            End Set
        End Property
        Private mIsIncomplete As Boolean

        Public Property IsSubmitted() As Boolean Implements IApplication.IsSubmitted
            Get
                Return mIsSubmitted
            End Get
            Set(ByVal Value As Boolean)
                mIsSubmitted = Value
            End Set
        End Property
        Private mIsSubmitted As Boolean

        Public Property ApplicationMethod() As ReferenceData.BOApplicationMethod Implements IApplication.ApplicationMethod
            Get
                Return mApplicationMethod
            End Get
            Set(ByVal Value As ReferenceData.BOApplicationMethod)
                mApplicationMethod = Value
            End Set
        End Property
        Private mApplicationMethod As ReferenceData.BOApplicationMethod

        Public Property Party() As BOApplicationPartyDetails Implements IApplication.Party
            Get
                Return mParty
            End Get
            Set(ByVal Value As BOApplicationPartyDetails)
                mParty = Value
            End Set
        End Property
        Private mParty As BOApplicationPartyDetails

        Public Property Agent() As BOApplicationPartyDetails Implements IApplication.Agent
            Get
                Return mAgent
            End Get
            Set(ByVal Value As BOApplicationPartyDetails)
                mAgent = Value
            End Set
        End Property
        Private mAgent As BOApplicationPartyDetails

        Public Property IsSemiComplete() As Boolean Implements IApplication.IsSemiComplete
            Get
                Return mIsSemiComplete
            End Get
            Set(ByVal Value As Boolean)
                mIsSemiComplete = Value
            End Set
        End Property
        Private mIsSemiComplete As Boolean

        Public Property Retrospective() As Boolean Implements IApplication.Retrospective
            Get
                Return mRetrospective
            End Get
            Set(ByVal Value As Boolean)
                mRetrospective = Value
            End Set
        End Property
        Private mRetrospective As Boolean

        Public Property PaymentBasketId() As Object Implements IApplication.PaymentBasketId
            Get
                Try
                    If Not Me.mPaymentBasketId Is Nothing AndAlso CType(mPaymentBasketId, Int32) = 0 Then
                        Return Nothing
                    Else
                        Return mPaymentBasketId
                    End If
                Catch ex As Exception
                    Return Nothing
                End Try
            End Get
            Set(ByVal Value As Object)
                mPaymentBasketId = Value
            End Set
        End Property
        Private mPaymentBasketId As Object

        Public Property StandardFee() As Decimal Implements IApplication.StandardFee
            Get
                Return mStandardFee
            End Get
            Set(ByVal Value As Decimal)
                mStandardFee = Value
            End Set
        End Property
        Private mStandardFee As Decimal

        Public Property FeeCharged() As Decimal Implements IApplication.FeeCharged
            Get
                Return mFeeCharged
            End Get
            Set(ByVal Value As Decimal)
                mFeeCharged = Value
            End Set
        End Property
        Private mFeeCharged As Decimal

        Public Property AllowSemiComplete() As Boolean Implements IApplication.AllowSemiComplete 'MLD added 7/10/4
            Get
                Try
                    Dim Party As Party.BOParty
                    If Not mAgent Is Nothing AndAlso Not mAgent.Party Is Nothing Then
                        Party = mAgent.Party
                    End If
                    If Not mParty Is Nothing AndAlso Not mParty.Party Is Nothing Then
                        Party = mParty.Party
                    End If

                    If TypeOf Me Is BO.Application.CITES.Applications.BOCITESArticle10 Then
                        Return Party.AllowSemicompleteCitesArticle10
                    ElseIf TypeOf Me Is BO.Application.CITES.Applications.BOExportApplication Then
                        Return Party.AllowSemicompleteCitesExport
                    ElseIf TypeOf Me Is BO.Application.CITES.Applications.BOImportApplication Then
                        Return Party.AllowSemicompleteCitesImport
                    ElseIf TypeOf Me Is BOApplication Then
                        Return False
                    End If
                Catch ex As Exception
                    Return False
                End Try

            End Get
            Set(ByVal Value As Boolean)
            End Set
        End Property

        Public Property DateOfApplication() As Date Implements IApplication.DateOfApplication
            Get
                Return mDateOfApplication
            End Get
            Set(ByVal Value As Date)
                mDateOfApplication = Value
            End Set
        End Property
        Private mDateOfApplication As Date

        Public Property PaymentStatus() As PaymentStatusTypes Implements IApplication.PaymentStatus
            Get
                Return mPaymentStatus
            End Get
            Set(ByVal Value As PaymentStatusTypes)
                mPaymentStatus = Value
            End Set
        End Property
        Private mPaymentStatus As PaymentStatusTypes = PaymentStatusTypes.Unknown

        Public Property CreatedInfo() As Stamp Implements IApplication.Created
            Get
                Return mCreatedInfo
            End Get
            Set(ByVal Value As Stamp)
                mCreatedInfo = Value
            End Set
        End Property
        Private mCreatedInfo As Stamp

        Public Property ApplicationId() As Int32 Implements IApplication.ApplicationId
            Get
                Return mApplicationId
            End Get
            Set(ByVal Value As Int32)
                mApplicationId = Value
            End Set
        End Property
        Private mApplicationId As Int32

        'Public Property PermitType() As PermitTypes Implements IApplication.PermitType
        '    Get
        '        Return mPermitType
        '    End Get
        '    Set(ByVal Value As PermitTypes)
        '        mPermitType = Value
        '    End Set
        'End Property
        'Private mPermitType As PermitTypes

        Public Property PermitTypeId() As Int32 Implements IApplication.PermitTypeId
            Get
                Return mPermitTypeId
            End Get
            Set(ByVal Value As Int32)
                mPermitTypeId = Value
            End Set
        End Property
        Private mPermitTypeId As Int32
#End Region

#Region " Helper Functions "
        'Private ReadOnly Property CaseOfficerId() As Object
        '    Get
        '        If mCaseOfficer Is Nothing Then
        '            Return Nothing
        '        Else
        '            Return mCaseOfficer.AuthorisedUserId
        '        End If
        '    End Get
        'End Property

        'Private ReadOnly Property CustomerId() As Object
        '    Get
        '        If mCustomer Is Nothing Then
        '            Return Nothing
        '        Else
        '            Return mCustomer.AuthorisedUserId
        '        End If
        '    End Get
        'End Property

        Private ReadOnly Property ApplicationMethodId() As Object
            Get
                If mApplicationMethod Is Nothing Then
                    Return Nothing
                Else
                    Return mApplicationMethod.ID
                End If
            End Get
        End Property

        Private ReadOnly Property CreatedDate() As Date
            Get
                If mCreatedInfo Is Nothing Then
                    Return Date.Today
                Else
                    Return mCreatedInfo.Date
                End If
            End Get
        End Property

        Private ReadOnly Property CreatedById() As Int64
            Get
                If mCreatedInfo Is Nothing OrElse mCreatedInfo.UserId Is Nothing OrElse CType(mCreatedInfo.UserId, Int32) = 0 Then
                    Return Nothing
                Else
                    Return CType(mCreatedInfo.UserId, Int64)
                End If
            End Get
        End Property

        'Private ReadOnly Property PaymentStatusId() As Object
        '    Get
        '        Return Nothing
        '    End Get
        'End Property

        Private ReadOnly Property PartyLinkId() As Object
            Get
                If mParty Is Nothing OrElse mParty.Party.PartyId = 0 Then
                    Return Nothing
                Else
                    Return mParty.LinkId
                End If
            End Get
        End Property

        Private ReadOnly Property AgentLinkId() As Object
            Get
                If mAgent Is Nothing Then
                    Return Nothing
                Else
                    Return mAgent.LinkId
                End If
            End Get
        End Property
#End Region

#Region " Operations "

        Public Shared Function CanUpdateApplicationLevelDetails(ByVal applicationID As Int32, ByVal SSOUserId As Int64) As Boolean
            Dim Application As New BO.Application.BOApplication(applicationID, 0)
            If Not Application.Permit Is Nothing Then
                For Each Permit As BO.Application.BOPermit In Application.Permit
                    If Not Permit.CanUpdatePermit(SSOUserId, Permit.PermitId) Then
                        Return False
                    End If
                Next
            End If
            Return True
        End Function

        'Public Overridable Function GetReportCriteria(ByVal permitinfoIds As Int32(), ByVal newStatus As uk.gov.defra.Phoenix.BO.Application.BOPermitInfo.PermitStatusTypes) As uk.gov.defra.Phoenix.BO.ReportCriteria.ReportCriteria()
        'End Function

        Public Shared Function PolymorphicCreate(ByVal applicationId As Int32) As BOApplication 'MLD 2/12/4
            Return PolymorphicCreate(applicationId, Nothing)
        End Function

        Public Shared Function PolymorphicCreate(ByVal applicationId As Int32, ByVal tran As SqlClient.SqlTransaction) As BOApplication    'MLD 2/12/4
            Dim baseApp As New Entity.Application(applicationId, tran)
            Dim citesApps As EntitySet.CITESApplicationSet = baseApp.GetRelatedCITESApplication(tran)
            Dim appType As ApplicationTypes = CType(baseApp.ApplicationTypeId, ApplicationTypes)
            Dim isCites As Boolean = Not citesApps Is Nothing AndAlso citesApps.Count = 1
            Dim isNotCites As Boolean = appType <= ApplicationTypes.BirdTrans AndAlso appType <> ApplicationTypes.Unknown 'MLD 23/3/5 unknown added 

            If isCites = isNotCites Then
                Dim message As String       'MLD 13/5/5 expanded
                If isCites Then
                    message = " appears to be a Bird application but has a related CITESApplication record"
                Else
                    message = " appears to be a CITES application but has no related CITESApplication record (or multiple records)"
                End If
                Throw New Exception("Internal Consistency Error in BOApplication.PolymorphicCreate. App Id " + applicationId.ToString + message)
            End If
            Select Case appType
                Case ApplicationTypes.Import
                    Dim citesApp As Entity.CITESApplication = citesApps.Entities(0)
                    Dim apps As EntitySet.ImportApplicationSet = citesApp.GetRelatedImportApplication
                    If Not apps Is Nothing AndAlso apps.Count = 1 Then
                        Dim app As Entity.ImportApplication = apps.Entities(0)
                        Return New CITES.Applications.BOImportApplication(app.ImportApplicationId, tran)
                    End If
                    Throw New Exception("ImportApplication missing/duplicated in BOApplication.PolymorphicCreate. App Id " + applicationId.ToString())

                Case ApplicationTypes.Export
                    Dim citesApp As Entity.CITESApplication = citesApps.Entities(0)
                    Dim apps As EntitySet.ExportApplicationSet = citesApp.GetRelatedExportApplication
                    If Not apps Is Nothing AndAlso apps.Count = 1 Then
                        Dim app As Entity.ExportApplication = apps.Entities(0)
                        Return New CITES.Applications.BOExportApplication(app.ExportApplicationId, tran)
                    End If
                    Throw New Exception("ExportApplication missing/duplicated in BOApplication.PolymorphicCreate. App Id " + applicationId.ToString())

                Case ApplicationTypes.Article10
                    Dim citesApp As Entity.CITESApplication = citesApps.Entities(0)
                    Dim apps As EntitySet.Article10Set = citesApp.GetRelatedArticle10
                    If Not apps Is Nothing AndAlso apps.Count = 1 Then
                        Dim app As Entity.Article10 = apps.Entities(0)
                        Return New CITES.Applications.BOCITESArticle10(app.Article10Id, tran)
                    End If
                    Throw New Exception("Article10 missing/duplicated in BOApplication.PolymorphicCreate. App Id " + applicationId.ToString())

                Case ApplicationTypes.Article30 'note: uses Article 10 stuff
                    Dim citesApp As Entity.CITESApplication = citesApps.Entities(0)
                    Dim apps As EntitySet.Article10Set = citesApp.GetRelatedArticle10
                    If Not apps Is Nothing AndAlso apps.Count = 1 Then
                        Dim app As Entity.Article10 = apps.Entities(0)
                        Return New CITES.Applications.BOCITESArticle30(app.Article10Id, tran)
                    End If
                    Throw New Exception("Article30 missing/duplicated in BOApplication.PolymorphicCreate. App Id " + applicationId.ToString())

                Case ApplicationTypes.BirdChick, ApplicationTypes.BirdAdult
                    Return New BirdRegistration(applicationId)

                Case Else
                    Dim ringApps As EntitySet.RingApplicationSet = baseApp.GetRelatedRingApplication(tran)
                    'If Not ringApps Is Nothing AndAlso ringApps.Count = 1 Then
                    '    Dim app As Entity.RingApplication = ringApps.Entities(0)
                    '    Return New BirdRegistration(app.ApplicationId, BirdRegLoadMode.LoadByApplication)
                    'End If
                    Throw New Exception("RingApplication missing/duplicated in BOApplication.PolymorphicCreate. App Id " + applicationId.ToString())
            End Select
        End Function

        Public Shared Function PersistPermitExpiryDates(ByVal dates As Date(), ByVal permitIds As Int32()) As Boolean
            Dim i As Int32
            Dim Success As Boolean = True

            'get a transaction
            Dim NewPermit As New DataObjects.Entity.Permit
            Dim service As DataObjects.Service.PermitService = NewPermit.ServiceObject
            Dim tran As SqlClient.SqlTransaction = service.BeginTransaction


            Dim BOPermit As BO.Application.BOPermit
            For Each permitId As Int32 In permitIds
                'could do with a transaction?
                BOPermit = New BO.Application.BOPermit(permitId, tran)
                BOPermit.ExpiryDate = dates(i)
                If BOPermit.Save(tran) Is Nothing Then
                    service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    Success = False
                    Exit For
                End If
                i += 1
            Next

            If Success Then
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Commit)
            End If

            Return Success
        End Function

        'Public Shared Function AllowUpdateSemiComplete(ByVal applicationId As Int32, ByVal permitInfoId As Int32) As Boolean
        '    Stop
        '    Dim Allowed As Boolean
        '    Dim App As New [DO].DataObjects.Entity.Application(applicationId)
        '    Allowed = True
        '    Dim Permitinfo As [DO].DataObjects.Entity.PermitInfo
        '    With App
        '        If .SemiComplete Then
        '            '  .SetPermits(Nothing)
        '            '   For Each permit As BO.Application.BOPermit In .Permit
        '            '  If Not Allowed Then Exit For
        '            '  For Each permitinfo As BO.Application.BOPermitInfo In Permit.GetPermitInfos(Nothing)
        '            Permitinfo = New [DO].DataObjects.Entity.PermitInfo(permitInfoId)
        '            If Not Permitinfo.PermitStatusId = BO.Application.BOPermitInfo.PermitStatusTypes.Issued AndAlso _
        '                Not Permitinfo.PermitStatusId = BO.Application.BOPermitInfo.PermitStatusTypes.ReturnedUsed Then

        '                Allowed = False
        '                ' Exit For
        '            End If
        '            Permitinfo = Nothing
        '            ' Next
        '            '  Next
        '            'Else
        '            '    Allowed = False
        '        End If
        '    End With
        '    App = Nothing
        '    Return Allowed
        'End Function

        Public Shared Function SavePermitNotes(ByVal permitIds As Int32(), ByVal appId As Int32, ByVal noteId As Int32) As Boolean
            Dim PermitNoteService As New DataObjects.Service.PermitNoteService

            Dim tran As SqlClient.SqlTransaction = PermitNoteService.BeginTransaction
            Return SavePermitNotes(True, permitIds, appId, noteId, tran)
        End Function

        Public Shared Function SavePermitNotes(ByVal commitTran As Boolean, ByVal permitIds As Int32(), ByVal appId As Int32, ByVal noteId As Int32, ByVal tran As SqlClient.SqlTransaction) As Boolean
            Dim PermitNoteService As New DataObjects.Service.PermitNoteService
            Dim PermitNoteSet As DataObjects.EntitySet.PermitNoteSet = PermitNoteService.GetByIndex_IX_PermitNote_1(noteId, tran)

            'remove the old permit note links
            If Not PermitNoteSet Is Nothing AndAlso PermitNoteSet.Entities.Count > 0 Then
                For Each PermitEntity As DataObjects.Entity.PermitNote In PermitNoteSet.Entities
                    PermitNoteService.DeleteById(New Int32() {appId, noteId, PermitEntity.PermitId}, 0, tran)
                Next
            End If

            'add the new links
            For Each permitid As Int32 In permitIds
                PermitNoteService.Insert(appId, noteId, permitid, tran)
                If Not PermitNoteService.GetLastDBError Is Nothing Then

                    PermitNoteService.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    Return False
                End If
            Next
            PermitNoteService.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Commit)
            PermitNoteService = Nothing
            Return True
        End Function

        <Serializable()> _
        Public Class FindCITESPermitCriteria
            Public Property MainPartyId() As Int32
                Get
                    Return mMainPartyId
                End Get
                Set(ByVal Value As Int32)
                    mMainPartyId = Value
                End Set
            End Property
            Private mMainPartyId As Int32



            Public Property ApplicationType() As Application.ApplicationTypes
                Get
                    Return mApplicationType
                End Get
                Set(ByVal Value As Application.ApplicationTypes)
                    mApplicationType = Value
                End Set
            End Property
            Private mApplicationType As Application.ApplicationTypes

            Public Property ApplicationId() As Int32
                Get
                    Return mApplicationId
                End Get
                Set(ByVal Value As Int32)
                    mApplicationId = Value
                End Set
            End Property
            Private mApplicationId As Int32

            Public Property PermitNumber() As Int32
                Get
                    Return mPermitNumber
                End Get
                Set(ByVal Value As Int32)
                    mPermitNumber = Value
                End Set
            End Property
            Private mPermitNumber As Int32


            Public Property CopyNumber() As Int32
                Get
                    Return mCopyNumber
                End Get
                Set(ByVal Value As Int32)
                    mCopyNumber = Value
                End Set
            End Property
            Private mCopyNumber As Int32

            Public Property MarkTypeId() As Int32
                Get
                    Return mMarkTypeId
                End Get
                Set(ByVal Value As Int32)
                    mMarkTypeId = Value
                End Set
            End Property
            Private mMarkTypeId As Int32

            Public Property MarkId() As String
                Get
                    Return mMarkId
                End Get
                Set(ByVal Value As String)
                    mMarkId = Value
                End Set
            End Property
            Private mMarkId As String
        End Class

        <Serializable()> _
        Public Class PermitSearchResults
            Public Property Permits() As BOPermit()
                Get
                    Return mPermits
                End Get
                Set(ByVal Value As BOPermit())
                    mPermits = Value
                End Set
            End Property
            Private mPermits As BOPermit()

            Public Property SpecimenId() As Int32
                Get
                    Return mSpecimenId
                End Get
                Set(ByVal Value As Int32)
                    mSpecimenId = Value
                End Set
            End Property
            Private mSpecimenId As Int32

            Public Property ErrorMessage() As String
                Get
                    Return mErrorMessage
                End Get
                Set(ByVal Value As String)
                    mErrorMessage = Value
                End Set
            End Property
            Private mErrorMessage As String

        End Class

        Public Shared Function FindPermitForEndorcement(ByVal appNumber As Int32, ByVal permitNumber As Int32, ByVal copyNumber As Int32) As BO.Application.BOPermit.ProgressPermitGrid()
            'Load as Application DO
            Dim DOApplication As New DataObjects.Entity.Application(appNumber)
            'Get all Related CITES Applications
            Dim CitesApps As DataObjects.EntitySet.CITESApplicationSet = DOApplication.GetRelatedCITESApplication
            Dim CitesApplication As BO.Application.CITES.Applications.BOCITESApplication

            If Not CitesApps Is Nothing AndAlso CitesApps.Entities.Count > 0 Then
                CitesApplication = New BO.Application.CITES.Applications.BOCITESApplication(CitesApps.Entities(0).CitesApplicationId)
                'get the permit that matches the permit number parameter
                Dim DOPermits As DataObjects.EntitySet.PermitSet = DOApplication.GetRelatedPermit
                For Each permit As DataObjects.Entity.Permit In DOPermits
                    If permit.PermitNumber = permitNumber Then
                        Dim DOPermit As New DataObjects.Entity.Permit(permit.PermitId, Nothing)
                        Dim CitesPermits As DataObjects.EntitySet.CITESPermitSet = DOPermit.GetRelatedCITESPermit
                        If Not CitesPermits Is Nothing AndAlso CitesPermits.Entities.Count > 0 Then
                            Dim BOCITESPermit As New BO.Application.CITES.BOCITESPermit(CitesPermits.Entities(0).CITESPermitId)
                            'get the permitinfos
                            Dim PIs As BO.Application.BOPermitInfo() = BOCITESPermit.GetPermitInfos(Nothing)
                            Dim PermitInfo As BO.Application.BOPermitInfo

                            'if there are multiple copies then use the copy number parameter
                            If Not BOCITESPermit.NumberOfCopies Is Nothing AndAlso CType(BOCITESPermit.NumberOfCopies, Int32) > 1 Then
                                If copyNumber > 0 Then
                                    For Each pi As BO.Application.BOPermitInfo In PIs
                                        If pi.SequenceNumber = copyNumber Then
                                            PermitInfo = New BO.Application.BOPermitInfo(pi.PermitInfoId, Nothing)

                                            Return New BO.Application.BOPermit.ProgressPermitGrid() { _
                                                New BO.Application.BOPermit.ProgressPermitGrid(CitesApplication, (copyNumber > 0), _
                                                PermitInfo, BOCITESPermit)}
                                            'Exit For
                                        End If
                                    Next
                                Else

                                End If
                            Else
                                'this should mean that there is only one permit info so use that
                                PermitInfo = New BO.Application.BOPermitInfo(PIs(0).PermitInfoId, Nothing)
                            End If

                            If Not PermitInfo Is Nothing Then
                                Return New BO.Application.BOPermit.ProgressPermitGrid() { _
                                          New BO.Application.BOPermit.ProgressPermitGrid(CitesApplication, (copyNumber > 0), _
                                          PermitInfo, BOCITESPermit)}
                            End If
                        End If
                        Exit For
                    End If
                Next
            End If
        End Function

        Public Shared Function FindPermits(ByVal criteria As FindCITESPermitCriteria, ByVal isCaseOfficer As Boolean) As PermitSearchResults
            Dim PermitSearchResults As New PermitSearchResults
            With criteria
                If .MarkId = "" And .MarkTypeId = 0 Then
                    'application and/or permit number search or
                    'permit and/or application number search 
                    If .ApplicationId > 0 And .PermitNumber > 0 Then
                        Dim NewApp As New BOApplication(.ApplicationId, 0)
                        If NewApp.Party.Party.PartyId <> .MainPartyId Then
                            'The ApplicationId must be for the party in question
                            PermitSearchResults.ErrorMessage = "The Application Id is not linked to the Party."
                            PermitSearchResults.Permits = Nothing
                            If Not isCaseOfficer Then Return PermitSearchResults
                        End If
                        If Not NewApp.Permit Is Nothing Then
                            For Each permit As BOPermit In NewApp.Permit
                                If permit.PermitNumber = .PermitNumber Then
                                    PermitSearchResults.Permits = New BOPermit() {permit}
                                    If PermitSearchResults.Permits Is Nothing OrElse PermitSearchResults.Permits.Length = 0 Then
                                        PermitSearchResults.ErrorMessage = "No Records Found."
                                    End If
                                    Return PermitSearchResults
                                End If
                            Next permit
                        End If
                    ElseIf .PermitNumber = 0 AndAlso .ApplicationId > 0 Then
                        Dim NewApp As New BOApplication(.ApplicationId, 0)
                        PermitSearchResults.Permits = newapp.Permit
                        If PermitSearchResults.Permits Is Nothing OrElse PermitSearchResults.Permits.Length = 0 Then
                            PermitSearchResults.ErrorMessage = "No Records Found."
                        End If
                        Return PermitSearchResults
                    End If
                Else
                    'mark type search
                    Dim LinkedToHolder As Boolean
                    Dim NewMarks As DataObjects.EntitySet.SpecimenIDMarkSet '= DataObjects.Entity.SpecimenIDMark.GetForIDMarkLocati(1)
                    Dim MarkService As New DataObjects.Service.SpecimenIDMarkService
                    NewMarks = MarkService.GetByIndex_IdMarkAndType(.MarkId, .MarkTypeId)
                    If Not NewMarks Is Nothing Then
                        Dim NewMark As DataObjects.Entity.SpecimenIDMark = CType(NewMarks.GetEntity(0), DataObjects.Entity.SpecimenIDMark)
                        Dim Specimen As New DataObjects.Entity.Specimen(NewMark.SpecimenId, Nothing)
                        If Not Specimen Is Nothing Then
                            Dim PermitSpec As DataObjects.Entity.PermitSpecimen
                            Dim PermitSpecs As DataObjects.EntitySet.PermitSpecimenSet = PermitSpec.GetForSpecimen(Specimen.Id)
                            If Not PermitSpecs Is Nothing AndAlso _
                            PermitSpecs.Count > 0 Then
                                Dim Index As Int32 = 0
                                'If TypeOf Me Is CITES.Applications.BOCITESApplication Then
                                '    If CType(Me, CITES.Applications.BOCITESApplication).IsImport Then
                                For Each ps As DataObjects.Entity.PermitSpecimen In PermitSpecs.Entities
                                    Dim BasePermit As New BOPermit(ps.PermitId)
                                    Dim CITESApps As DataObjects.EntitySet.CITESApplicationSet = DataObjects.Entity.CITESApplication.GetForApplication(BasePermit.ApplicationId)
                                    If Not CITESApps Is Nothing AndAlso _
                                    CITESApps.Count = 1 Then
                                        Dim App As New DataObjects.Entity.Application(CITESApps.Entities(0).ApplicationId)
                                        Dim PartyLink As New DataObjects.Entity.PartyLink(App.PartyLinkId)
                                        Dim Party As New DataObjects.Entity.Party(PartyLink.PartyId)
                                        If Not LinkedToHolder AndAlso Party.Id = .MainPartyId Then
                                            LinkedToHolder = True
                                        End If
                                        App = Nothing
                                        PartyLink = Nothing
                                        Party = Nothing

                                        Dim CITESPermits As DataObjects.EntitySet.CITESPermitSet = DataObjects.Entity.CITESPermit.GetForPermit(ps.PermitId)
                                        If Not CITESPermits Is Nothing AndAlso _
                                            CITESPermits.Count = 1 Then
                                            Dim ImportExportPermits As DataObjects.EntitySet.CITESImportExportPermitSet = DataObjects.Entity.CITESImportExportPermit.GetForCITESPermit(CType(CITESPermits.GetEntity(0), DataObjects.Entity.CITESPermit).Id)
                                            If Not ImportExportPermits Is Nothing AndAlso _
                                            ImportExportPermits.Count = 1 Then
                                                ReDim Preserve PermitSearchResults.Permits(Index)
                                                PermitSearchResults.Permits(Index) = BO.Application.BOPermit.PolymorphicCreate(ps.PermitId)  'New CITES.Applications.BOCITESImportExportPermit(CType(ImportExportPermits.GetEntity(0), DataObjects.Entity.CITESImportExportPermit).Id)
                                                PermitSearchResults.SpecimenId = Specimen.SpecimenId
                                                Index += 1
                                            End If
                                        End If
                                        'End If
                                    End If
                                Next ps
                                'End If
                                'End If
                                If Not LinkedToHolder Then
                                    PermitSearchResults.Permits = Nothing
                                    PermitSearchResults.ErrorMessage = "The Specimen entered has not been linked to the Holder."
                                    Return PermitSearchResults
                                End If
                            End If
                        End If
                    End If
                End If
            End With
            If PermitSearchResults.Permits Is Nothing OrElse PermitSearchResults.Permits.Length = 0 Then
                PermitSearchResults.ErrorMessage = "No Records Found."
            End If
            Return PermitSearchResults
        End Function

        Public Function CalculateFee() As Decimal Implements IApplication.CalculateFee
            Return 0
        End Function

        Public Overrides Function Clone() As Object
            Dim BaseApplication As BOApplication = CType(MyBase.Clone, BOApplication)
            With BaseApplication
                Dim PermitIndex As Int32
                For Each SinglePermit As BOPermit In .Permit
                    .Permit(PermitIndex) = CType(SinglePermit.Clone, BOPermit)
                    Dim Specimenindex As Int32
                    For Each spec As BOSpecimen In SinglePermit.Specimens
                        SinglePermit.Specimens(Specimenindex) = CType(spec.Clone, BOSpecimen)
                        Specimenindex += 1
                    Next
                    PermitIndex += 1
                Next

                .DateOfApplication = Nothing
.ApplicationId=0
            End With
            Return BaseApplication
        End Function

        Public Function GetStatus() As Object Implements IApplication.GetStatus
            Return Nothing
        End Function

        Public Function Submit() As Boolean Implements IApplication.Submit
            Return False
        End Function

        Public Function GetGridNotes(ByVal applicationId As Int32) As Party.Note.GridNote()
            Dim Notes As Application.CITES.Applications.ApplicationNote() = GetNotes(applicationId)
            Dim GridNotes(Notes.Length - 1) As Party.Note.GridNote

            Dim index As Int32
            For Each Note As BOCommon.BaseNote In Notes
                GridNotes(index) = New Party.Note.GridNote(Note)
                index += 1
            Next

            Return GridNotes
        End Function

        Public Function GetNotes(ByVal applicationId As Int32) As Application.CITES.Applications.ApplicationNote()
            Try
                Dim Application As New DataObjects.Entity.Application(applicationId)
                Return GetNotes(Application)
            Catch
            End Try
        End Function

        Shared Function GetNotes(ByVal application As DataObjects.Entity.Application) As Application.CITES.Applications.ApplicationNote()
            Dim Notes As DataObjects.EntitySet.NoteSet = application.GetRelatedNotes

            If Not Notes Is Nothing AndAlso _
               Notes.Count > 0 Then
                Dim NoteList(Notes.Count - 1) As application.CITES.Applications.ApplicationNote
                Dim Index As Int32 = 0
                For Each note As DataObjects.Entity.Note In Notes
                    NoteList(Index) = New application.CITES.Applications.ApplicationNote(note, application.Id)

                    Index += 1
                Next note
                Return NoteList
            Else
                Return Nothing
            End If
        End Function
#End Region

#Region " Save "
        Public Overloads Overrides Function Save() As BaseBO
            Return Save(False)
        End Function

        Public Overridable Overloads Function Save(ByVal tran As SqlClient.SqlTransaction) As BaseBO
            Return Save(tran, False)
        End Function

        Public Overridable Overloads Function Save(ByVal ignoreValidation As Boolean) As BaseBO
            Dim NewApplication As New DataObjects.Entity.Application
            Dim service As DataObjects.Service.ApplicationService = NewApplication.ServiceObject
            Dim tran As SqlClient.SqlTransaction = service.BeginTransaction

            Dim SaveResult As BaseBO = MyClass.Save(tran, ignoreValidation)
            If SaveResult Is Nothing Then
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
            Else
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Commit)
            End If
            Return SaveResult
        End Function

        Public Sub SetApplicationMethod(ByVal isCustomer As Boolean)
            'this method is used to default a new Application to the correct application 
            'method depending on the user inputting this app
            If mApplicationMethod Is Nothing Then
                'should default to internet
                If Not isCustomer Then
                    mApplicationMethod = New ReferenceData.BOApplicationMethod(6)
                Else
                    mApplicationMethod = New ReferenceData.BOApplicationMethod(3)
                End If
            End If
        End Sub

        Protected Overloads Function Save(ByVal tran As SqlClient.SqlTransaction, ByVal ignoreValidation As Boolean) As BaseBO
            If Not ignoreValidation Then
                ' may be a hack!!!!... it'll do for now, prevents the validation collection from being cleared
                MyBase.Save()
            Else
                'even more of a hack by PN :)
                DataObjects.Sprocs.LastError = Nothing
            End If

            If Not ignoreValidation AndAlso Validated Then
                Dim NewObj As BaseBO = Me.Validate(False, False, False)
                If Not NewObj Is Nothing AndAlso _
                Not NewObj.ValidationErrors Is Nothing AndAlso _
                Not NewObj.ValidationErrors.Errors Is Nothing AndAlso _
                NewObj.ValidationErrors.Errors.Length > 0 Then
                    'save, but rest the validated flag
                    Validated = False
                End If
            End If

            Dim NewApplication As New DataObjects.Entity.Application
            Dim service As DataObjects.Service.ApplicationService = NewApplication.ServiceObject

            If Not PartyLinkId Is Nothing Then
                mParty = mParty.Save(tran)
                If Not mParty Is Nothing AndAlso _
                Not mParty.ValidationErrors Is Nothing Then
                    'rollback the transaction
                    If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    'get the problems and assign them locally
                    ValidationErrors = Party.ValidationErrors
                    Return Me
                End If
            End If

            If Not AgentLinkId Is Nothing Then
                mAgent = mAgent.Save(tran)
                If Not mAgent Is Nothing AndAlso _
                Not mAgent.ValidationErrors Is Nothing Then
                    'rollback the transaction
                    If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    'get the problems and assign them locally
                    ValidationErrors = Agent.ValidationErrors
                    Return Me
                End If
            End If

            Created = (mApplicationId = 0)

            If Created Then
                NewApplication = service.Insert(mDateOfApplication, _
                                mIsSemiComplete, _
                                ApplicationMethodId, _
                                CreatedDate, _
                                mPaymentStatus, _
                                mRetrospective, _
                                mIsSubmitted, _
                                PartyLinkId, _
                                AgentLinkId, _
                                CreatedById, _
                                mValidated, _
                                ReceivedDate, _
                                Nothing, _
                                Nothing, _
                                PaymentBasketId, _
                                mStandardFee, _
                                mFeeCharged, _
                                ApplicationTypeId, _
                                mPaidDate, _
                                mownerId, _
                                tran)
            Else
                NewApplication = service.Update(mApplicationId, _
                                                mDateOfApplication, _
                                                mIsSemiComplete, _
                                                ApplicationMethodId, _
                                                CreatedDate, _
                                                mPaymentStatus, _
                                                mRetrospective, _
                                                mIsSubmitted, _
                                                PartyLinkId, _
                                                AgentLinkId, _
                                                CreatedById, _
                                                mValidated, _
                                                mReceivedDate, _
                                                Nothing, _
                                                Nothing, _
                                                PaymentBasketId, _
                                                mStandardFee, _
                                                mFeeCharged, _
                                                ApplicationTypeId, _
                                                mPaidDate, _
                                                mOwnerId, _
                                                CheckSum, _
                                                tran)
            End If


            'check to see if any SQL errors have occured
            If (NewApplication Is Nothing AndAlso Not DataObjects.Sprocs.LastError Is Nothing) Then
                'TODO: Use errors collection to check to see if the problem was concurrency
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSavePermit)
                Return Me
            ElseIf Created AndAlso Not NewApplication Is Nothing Then
                'If Not Me.Permit Is Nothing Then
                '    'change status
                '    If Created Then
                '        'Dim PI As New BO.Application.BOPermitInfo
                '        'If Not Me.Permit Is Nothing Then
                '        '    For Each IndividualPermit As BOPermit In Me.Permit
                '        '        If Not CaseOfficerId Is Nothing Then
                '        '            PI.ChangeStatus(0, BO.Application.BOPermit.GetRelatedPermitInfo(IndividualPermit.PermitId, tran)(0).PermitInfoId, Common.AssignedToList.CaseOfficer, _
                '        '                CType(CaseOfficerId, Int32), New AdditionalInformation_ProgressAllowed, tran)
                '        '        Else
                '        '            PI.ChangeStatus(0, BO.Application.BOPermit.GetRelatedPermitInfo(IndividualPermit.PermitId, tran)(0).PermitInfoId, Common.AssignedToList.CaseOfficer, _
                '        '                CType(CustomerId, Int32), New AdditionalInformation_SubmittedByCustomer, tran)
                '        '        End If
                '        '    Next
                '        'End If
                '        'add error to error list and return me
                '    Else

                '    End If
                'End If
                mApplicationId = NewApplication.Id
            End If

            Try
                If NewApplication.CheckSum <> CheckSum Then
                    InitialiseApplication(NewApplication, tran)
                End If
            Catch ex As Exception
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSavePermit)
            End Try
            Return Me
        End Function
#End Region

#Region " Validate "
        Public Overridable Overloads Function Validate(ByVal writeFlag As Boolean, ByVal ignoreWarnings As Boolean) As BaseBO Implements IApplication.Validate
            Return Validate(writeFlag, ignoreWarnings, True)
        End Function

        Protected Overridable Overloads Function Validate(ByVal writeFlag As Boolean, ByVal ignoreWarnings As Boolean, ByVal saveApplication As Boolean) As BaseBO
            MyBase.ValidationErrors = Nothing
            Return Nothing
        End Function
#End Region


        Public Overrides Function Delete() As Object

        End Function
    End Class
End Namespace
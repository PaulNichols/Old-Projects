Imports uk.gov.defra.Phoenix.BO.ValidationError
Imports uk.gov.defra.Phoenix.BO.ValidationError.ValidationCodes

Namespace Application.CITES.Applications
    Public Class BOCITESApplication
        Inherits BOApplication
        Implements IBOCITESApplication

#Region " Prelim code "

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal citesApplicationId As Int32)
            MyClass.New()
            LoadCITESApplication(citesApplicationId)
        End Sub

        Private Function LoadCITESApplication(ByVal id As Int32) As DataObjects.Entity.CITESApplication
            Return LoadCITESApplication(id, Nothing)
        End Function

        Private Function LoadCITESApplication(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.CITESApplication
            Dim NewCITESApplication As DataObjects.Entity.CITESApplication = DataObjects.Entity.CITESApplication.GetById(id)
            If NewCITESApplication Is Nothing Then
                Throw New RecordDoesNotExist("CITESApplication", id)
            Else
                InitialiseCITESApplication(NewCITESApplication, tran)
                Return NewCITESApplication
            End If
        End Function

        Friend Overridable Sub InitialiseCITESApplicationByApplicationId(ByVal applicationId As Int32)
            InitialiseCITESApplicationByApplicationId(applicationId, Nothing)
        End Sub

        Friend Overridable Sub InitialiseCITESApplicationByApplicationId(ByVal applicationId As Int32, ByVal tran As SqlClient.SqlTransaction)
            If applicationId = 0 Then
                Throw New ArgumentException("Application Id is 0")
            Else                    'MLD 31/3/5 duplicate call removed
                Dim apps As DataObjects.EntitySet.CITESApplicationSet = DataObjects.Entity.CITESApplication.GetForApplication(applicationId, tran)
                If Not apps Is Nothing AndAlso apps.Count = 1 Then
                    InitialiseCITESApplication(CType(apps.GetEntity(0), DataObjects.Entity.CITESApplication), tran)
                Else
                    Throw New ArgumentException("Cannot find CITES application")
                End If
            End If
        End Sub

        Friend Overridable Sub InitialiseCITESApplication(ByVal citesApplication As DataObjects.Entity.CITESApplication, ByVal tran As SqlClient.SqlTransaction)
            'Try
            With citesApplication
                MyBase.LoadApplication(.ApplicationId, tran)
                mIsComposite = .IsComposite
                mCITESChecksum = .CheckSum
                mCITESApplicationId = .Id
                If Not .IsLocationAddressIdNull Then
                    mLocationAddress = New Party.BOReadOnlyAddress(CType(.LocationAddressId, Int32), tran)
                End If
                If Not .IsConsignmentNull Then
                    mConsignment = .Consignment
                End If
                If Not .IsManagementAuthorityIdNull Then
                    mManagementAuthority = New Application.BOApplicationPartyDetails(.ManagementAuthorityId, tran)
                End If
                If Not .IsForeignManagementAuthorityIdNull Then
                    mForeignManagementAuthority = New Application.BOApplicationPartyDetails(.ForeignManagementAuthorityId, tran)
                End If
                If Not .IsSecondPartyIdNull Then
                    mSecondParty = New Application.BOApplicationPartyDetails(.SecondPartyId, tran)
                End If
                If Not .IsCountryOfImportIdNull Then CountryOfImport = New ReferenceData.BOCountry(.CountryOfImportId)

                'check for additional declarations
                Dim AddDec As New DataObjects.Entity.AdditionalDeclaration
                Dim AddDecs As DataObjects.EntitySet.AdditionalDeclarationSet = AddDec.GetForCITESApplication(mCITESApplicationId, tran)
                If Not AddDecs Is Nothing AndAlso _
                   AddDecs.Count > 0 Then
                    Dim BOAddDec As New BOAdditionalDeclaration
                    BOAddDec.InitialiseAdditionalDeclaration(CType(AddDecs.GetEntity(0), DataObjects.Entity.AdditionalDeclaration), tran)
                    mAdditionalDeclaration = BOAddDec
                Else
                    mAdditionalDeclaration = New BOAdditionalDeclaration
                    mAdditionalDeclaration.CITESApplicationId = mCITESApplicationId
                End If
                AddDec = Nothing
            End With
            'Catch ex As Exception
            'End Try
        End Sub

        'Public Overrides Sub SetPermits(ByVal tran As SqlClient.SqlTransaction)
        '    MyBase.SetPermits(tran)
        '    Dim x As BOPermit()
        '    If Not MyBase.Permit Is Nothing Then
        '        ReDim x(MyBase.Permit.Length - 1)
        '        Dim Index As Int32 = 0

        '        For Each permit As BOPermit In MyBase.Permit

        '            Dim CITESPermit As New DataObjects.Entity.CITESPermit
        '            Dim CITESPermits As DataObjects.EntitySet.CITESPermitSet = CITESPermit.GetForPermit(permit.PermitId, tran)
        '            If Not CITESPermits Is Nothing AndAlso _
        '               CITESPermits.Count > 0 Then

        '                For Each CITESPermit In CITESPermits
        '                    Dim BOCITESPerm As New BOCITESPermit
        '                    BOCITESPerm.InitialiseCITESPermit(CITESPermit, tran)
        '                    x(Index) = BOCITESPerm
        '                    Index += 1
        '                Next CITESPermit
        '            Else
        '                MyBase.Permit = Nothing
        '            End If
        '            CITESPermit = Nothing
        '            CITESPermits = Nothing
        '        Next permit
        '        MyBase.Permit = x
        '    End If
        'End Sub
#End Region

#Region " Properties "

        Public Property IsComposite() As Boolean Implements IBOCITESApplication.IsComposite
            Get
                Return mIsComposite
            End Get
            Set(ByVal Value As Boolean)
                mIsComposite = Value
            End Set
        End Property
        Private mIsComposite As Boolean

        Public Property Consignment() As Boolean Implements IBOCITESApplication.Consignment
            Get
                Return mConsignment
            End Get
            Set(ByVal Value As Boolean)
                mConsignment = Value
            End Set
        End Property
        Private mConsignment As Boolean

        Public ReadOnly Property IssuingAuthorityAddress() As String    'MLD added 6/1/5 to replace old shared methods
            Get
                Dim authority As BOApplicationPartyDetails = ManagementAuthority
                If Not authority Is Nothing Then
                    Return authority.Party.DisplayName + Environment.NewLine + authority.Address.ReportAddress
                End If
                Return ""
            End Get
        End Property

        Public Property LocationAddress() As BO.Party.BOReadOnlyAddress Implements IBOCITESApplication.LocationAddress
            Get
                Return mLocationAddress
            End Get
            Set(ByVal Value As BO.Party.BOReadOnlyAddress)
                mLocationAddress = Value
            End Set
        End Property
        Private mLocationAddress As BO.Party.BOReadOnlyAddress


        'Public Property ApplicationType() As Application.CITES.Applications.CITESApplicationTypeEnum Implements IBOCITESApplication.ApplicationType
        '    Get
        '        'TODO - derive from data
        '        Return mApplicationType
        '    End Get
        '    Set(ByVal Value As Application.CITES.Applications.CITESApplicationTypeEnum)
        '        mApplicationType = Value
        '    End Set
        'End Property
        'Private mApplicationType As Application.CITES.Applications.CITESApplicationTypeEnum

        Public Overridable Property CountryOfImport() As ReferenceData.BOCountry Implements IBOCITESApplication.CountryOfImport
            Get
                Return mCountryOfImport
            End Get
            Set(ByVal Value As ReferenceData.BOCountry)
                mCountryOfImport = Value
            End Set
        End Property
        Protected mCountryOfImport As ReferenceData.BOCountry

        Public Overridable Property SecondParty() As BO.Application.BOApplicationPartyDetails Implements IBOCITESApplication.SecondParty
            Get
                Return mSecondParty
            End Get
            Set(ByVal Value As BO.Application.BOApplicationPartyDetails)
                mSecondParty = Value
            End Set
        End Property
        Private mSecondParty As BO.Application.BOApplicationPartyDetails

        Public Property ManagementAuthority() As BOApplicationPartyDetails Implements IBOCITESApplication.ManagementAuthority
            Get
                Return mManagementAuthority
            End Get
            Set(ByVal Value As BOApplicationPartyDetails)
                mManagementAuthority = Value
            End Set
        End Property
        Private mManagementAuthority As BO.Application.BOApplicationPartyDetails

        Public Property ForeignManagementAuthority() As BOApplicationPartyDetails Implements IBOCITESApplication.ForeignManagementAuthority
            Get
                Return mForeignManagementAuthority
            End Get
            Set(ByVal Value As BOApplicationPartyDetails)
                mForeignManagementAuthority = Value
            End Set
        End Property
        Private mForeignManagementAuthority As BO.Application.BOApplicationPartyDetails

        Public Property CITESChecksum() As Integer Implements IBOCITESApplication.CITESChecksum
            Get
                Return mCITESChecksum
            End Get
            Set(ByVal Value As Integer)
                mCITESChecksum = Value
            End Set
        End Property
        Private mCITESChecksum As Int32

        Public Property CITESApplicationId() As Integer Implements IBOCITESApplication.CITESApplicationId
            Get
                Return mCITESApplicationId
            End Get
            Set(ByVal Value As Integer)
                mCITESApplicationId = Value
            End Set
        End Property
        Private mCITESApplicationId As Int32

        Public Property AdditionalDeclaration() As BOAdditionalDeclaration Implements IBOCITESApplication.AdditionalDeclaration
            Get
                Return mAdditionalDeclaration
            End Get
            Set(ByVal Value As BOAdditionalDeclaration)
                mAdditionalDeclaration = Value
            End Set
        End Property
        Private mAdditionalDeclaration As BOAdditionalDeclaration
#End Region

#Region " Helper Functions "

        Public Overridable Function GetNewPermit() As BO.Application.CITES.BOCITESPermit

        End Function

        Private ReadOnly Property LocationAddressId() As Object
            Get
                If LocationAddress Is Nothing OrElse LocationAddress.AddressId = 0 Then
                    Return Nothing
                Else
                    Return LocationAddress.AddressId
                End If
            End Get
        End Property

        Private ReadOnly Property ManagementAuthorityId() As Object
            Get
                If mManagementAuthority Is Nothing Then
                    Return Nothing
                Else
                    Return mManagementAuthority.LinkId
                End If
            End Get
        End Property

        Private ReadOnly Property CountryOfImportId() As Object
            Get
                If CountryOfImport Is Nothing OrElse CountryOfImport.ID = 0 Then
                    Return Nothing
                Else
                    Return CountryOfImport.ID
                End If
            End Get
        End Property



        Private ReadOnly Property ForeignManagementAuthorityId() As Object
            Get
                If mForeignManagementAuthority Is Nothing Then
                    Return Nothing
                Else
                    Return mForeignManagementAuthority.LinkId
                End If
            End Get
        End Property

        Protected ReadOnly Property SecondPartyId() As Object
            Get
                If mSecondParty Is Nothing Then
                    Return Nothing
                Else
                    Return mSecondParty.LinkId
                End If
            End Get
        End Property

        Public ReadOnly Property IsImport() As Boolean Implements IBOCITESApplication.IsImport
            Get
                Return (TypeOf Me Is Application.CITES.Applications.BOImportApplication)
            End Get
        End Property

        Public ReadOnly Property IsExport() As Boolean Implements IBOCITESApplication.IsExport
            Get
                If TypeOf Me Is Application.CITES.Applications.BOExportApplication Then
                    Return Not CType(Me, Application.CITES.Applications.BOExportApplication).ReExport
                Else
                    Return False
                End If
            End Get
        End Property

        Public ReadOnly Property IsReExport() As Boolean Implements IBOCITESApplication.IsReExport
            Get
                If TypeOf Me Is Application.CITES.Applications.BOExportApplication Then
                    Return CType(Me, Application.CITES.Applications.BOExportApplication).ReExport
                Else
                    Return False
                End If
            End Get
        End Property

        Public ReadOnly Property IsArticle10() As Boolean Implements IBOCITESApplication.IsArticle10
            Get
                Return (TypeOf Me Is Application.CITES.Applications.BOCITESArticle10)
            End Get
        End Property

        Public ReadOnly Property IsArticle30() As Boolean Implements IBOCITESApplication.IsArticle30
            Get
                Return (TypeOf Me Is Application.CITES.Applications.BOCITESArticle30)
            End Get
        End Property
#End Region

#Region " Operations "
        Public Shared Function ShowSAWarningBeforeAuthorise(ByVal permitInfos As Object()) As Boolean
            Dim LastPermitId As Int32
            Dim SAs() As BO.Application.CITES.Applications.BOPermitScientificAdvice

            For Each pi As BO.Application.BOPermit.ProgressPermitGrid In permitInfos
                If Not pi.PermitId = LastPermitId Then
                    SAs = BO.Application.CITES.Applications.BOCITESImportExportPermit.GetScientificAdvice(pi.PermitId, False)
                    For Each SA As BO.Application.CITES.Applications.BOPermitScientificAdvice In SAs
                        'Refuse
                        If New Application.ProgressStatus.BOProgressStatusSAAdvice(SA.ScientificAdvice.RecommendedStatus, Nothing).Code = "R" Then Return True
                    Next
                End If
            Next
            Return False
        End Function

        Public Shared Function ShowSCWarningBeforeAuthorise(ByVal permitInfos As Object()) As Boolean
            Dim SCs() As BO.Application.CITES.Applications.BOPermitSpecialCondition
            Dim LastPermitId As Int32

            For Each pi As BO.Application.BOPermit.ProgressPermitGrid In permitInfos
                If Not pi.PermitId = LastPermitId Then
                    SCs = BO.Application.CITES.Applications.BOCITESImportExportPermit.GetSpecialConditions(pi.PermitId, False)
                    For Each SC As BO.Application.CITES.Applications.BOPermitSpecialCondition In SCs
                        If SC.StatusId = BO.Application.CITES.Applications.SpecialConditionStatus.Rcmd_by_SA Then Return True
                    Next
                    LastPermitId = pi.PermitId
                End If
            Next
            Return False
        End Function

        Private Shared Function GetReportCriteriaConsignmentDraft(ByVal newStatus As uk.gov.defra.Phoenix.BO.Application.BOPermitInfo.PermitStatusTypes, ByVal applicationId As Int32) As ArrayList
            'draft consignments
            Dim ReturnArray As New ArrayList

            'add the consignment header page
            Dim PermitReportCriteria As New uk.gov.defra.Phoenix.BO.ReportCriteria.ConsignmentDraftReportCriteria
            PermitReportCriteria.ApplicationId = ApplicationId
            ReturnArray.Add(PermitReportCriteria)

            'add the consignment continuation page
            'Dim ConsignmentDraftPermitsReportCriteria As New uk.gov.defra.Phoenix.BO.ReportCriteria.ConsignmentDraftPermitsReportCriteria
            'ConsignmentDraftPermitsReportCriteria.ApplicationId = ApplicationId

            'ConsignmentDraftPermitsReportCriteria.Duplicate = (newStatus = BO.Application.BOPermitInfo.PermitStatusTypes.Duplicate)

            'ReturnArray.Add(ConsignmentDraftPermitsReportCriteria)

            Return ReturnArray
        End Function

        Private Shared Function GetReportCriteriaConsignmentNonDraft(ByVal applicationId As Int32) As ArrayList
            'non draft consignments
            Dim ReturnArray As New ArrayList

            'add the consignment header page
            Dim PermitReportCriteria As New uk.gov.defra.Phoenix.BO.ReportCriteria.ConsignmentReportCriteria
            PermitReportCriteria.ApplicationId = ApplicationId
            ReturnArray.Add(PermitReportCriteria)

            'add the consignment continuation page      'MLD 26/1/5 no longer needed
            'Dim ConsignmentPermitsReportCriteria As New uk.gov.defra.Phoenix.BO.ReportCriteria.ConsignmentPermitsReportCriteria
            'ConsignmentPermitsReportCriteria.ApplicationId = ApplicationId
            'ReturnArray.Add(ConsignmentPermitsReportCriteria)

            Return ReturnArray
        End Function


        Private Shared Function GetReportCriteriaConsignment(ByVal newStatus As uk.gov.defra.Phoenix.BO.Application.BOPermitInfo.PermitStatusTypes, ByVal applicationId As Int32) As ArrayList
            Dim ReturnArray As New ArrayList

            If newStatus = BOPermitInfo.PermitStatusTypes.IssuedDraft Then
                ReturnArray.AddRange(GetReportCriteriaConsignmentDraft(newStatus, applicationId))
            Else
                ReturnArray.AddRange(GetReportCriteriaConsignmentNonDraft(applicationId))
            End If
            Return ReturnArray
        End Function

        Public Overloads Shared Function GetReportCriteria(ByVal permitinfoIds As Int32(), ByVal newStatus As uk.gov.defra.Phoenix.BO.Application.BOPermitInfo.PermitStatusTypes, _
                ByVal ConsignmentOrIsComposite As Boolean, ByVal applicationId As Int32, ByVal isSemiComplete As Boolean, ByVal art10 As Boolean) As uk.gov.defra.Phoenix.BO.ReportCriteria.ReportCriteria()

            Dim reportCriterias As New ArrayList

            For Each PermitInfoId As Int32 In permitinfoIds
                If ConsignmentOrIsComposite Then
                    reportCriterias.AddRange(GetReportCriteriaConsignment(newStatus, applicationId))
                    Exit For
                Else
                    reportCriterias.AddRange(GetReportCriteriaNonConsignment(PermitInfoId, newStatus, isSemiComplete, art10))
                End If
            Next

            Return CType(reportCriterias.ToArray(GetType(uk.gov.defra.Phoenix.BO.ReportCriteria.ReportCriteria)), uk.gov.defra.Phoenix.BO.ReportCriteria.ReportCriteria())
        End Function

        Private Shared Function GetReportCriteriaNonConsignment(ByVal PermitInfoid As Int32, _
                ByVal newStatus As uk.gov.defra.Phoenix.BO.Application.BOPermitInfo.PermitStatusTypes, _
                ByVal isSemiComplete As Boolean, ByVal isArt10 As Boolean) As ArrayList

            Dim PermitReportCriteria As uk.gov.defra.Phoenix.BO.ReportCriteria.ReportCriteria
            Dim ReturnArray As New ArrayList

            If newStatus = BO.Application.BOPermitInfo.PermitStatusTypes.IssuedDraft Then
                'draft
                PermitReportCriteria = New uk.gov.defra.Phoenix.BO.ReportCriteria.PermitDraftReportCriteria
            Else
                'not draft
                If isSemiComplete Then
                    If isArt10 Then
                        PermitReportCriteria = New uk.gov.defra.Phoenix.BO.ReportCriteria.Article10SemiReportCriteria
                        With CType(PermitReportCriteria, BO.ReportCriteria.Article10SemiReportCriteria)
                            .Duplicate = (newStatus = BO.Application.BOPermitInfo.PermitStatusTypes.Duplicate)
                            .PermitInfoId = PermitInfoid
                        End With
                    Else
                            PermitReportCriteria = New uk.gov.defra.Phoenix.BO.ReportCriteria.PermitSemiReportCriteria
                    End If

                Else
                    If isArt10 Then
                        PermitReportCriteria = New BO.ReportCriteria.Article10ReportCriteria
                        With CType(PermitReportCriteria, BO.ReportCriteria.Article10ReportCriteria)
                            .Duplicate = (newStatus = BO.Application.BOPermitInfo.PermitStatusTypes.Duplicate)
                            .PermitInfoId = PermitInfoid
                        End With
                    Else
                        PermitReportCriteria = New uk.gov.defra.Phoenix.BO.ReportCriteria.PermitReportCriteria
                    End If
                End If

                If Not PermitReportCriteria.GetType.GetInterface(GetType(BO.ReportCriteria.IPermitReportCriteria).ToString) Is Nothing Then
                    With CType(PermitReportCriteria, BO.ReportCriteria.IPermitReportCriteria)
                        .Duplicate = (newStatus = BO.Application.BOPermitInfo.PermitStatusTypes.Duplicate)
                        .PermitInfoId = PermitInfoid
                    End With
                End If
            End If
            SetReportDescription(PermitReportCriteria, newStatus, PermitInfoid)
            ReturnArray.Add(PermitReportCriteria)

            Return ReturnArray
        End Function

        Protected Shared Sub SetReportDescription(ByRef permitReportCriteria As uk.gov.defra.Phoenix.BO.ReportCriteria.ReportCriteria, ByVal newStatus As BOPermitInfo.PermitStatusTypes, ByVal permitInfoId As Int32)
            If newStatus = BO.Application.BOPermitInfo.PermitStatusTypes.Issued OrElse _
             newStatus = BO.Application.BOPermitInfo.PermitStatusTypes.Duplicate OrElse _
             newStatus = BO.Application.BOPermitInfo.PermitStatusTypes.IssuedDraft Then
                'just for the sake of the ViewPermit User control

                Dim DOPermitInfo As New [DO].DataObjects.Entity.PermitInfo(permitInfoId)
                Dim BoPermit As New BO.Application.BOPermit(DOPermitInfo.PermitId)
                If Not BoPermit Is Nothing Then
                    If BoPermit.NumberOfCopies Is Nothing OrElse CType(BoPermit.NumberOfCopies, Int32) < 2 Then
                        permitReportCriteria.Description = BoPermit.ApplicationPermitNumber
                    Else
                        permitReportCriteria.Description = String.Concat(BoPermit.ApplicationPermitNumber, "/", DOPermitInfo.SequenceNumber.ToString.PadLeft(2, CType("0", Char)))
                    End If
                    BoPermit = Nothing
                End If
            End If
        End Sub

        Public Shared Function ResetAdviceAndConditionsCurrentFlag(ByVal permitIds() As Int32) As Boolean
            Return [DO].DataObjects.Entity.Application.ResetAdviceAndConditionsCurrentFlag(permitIds)
        End Function

        Private Shared Function ApplySAsToOtherPermits(ByVal SSOUserId As Long, ByVal mainPermitId As Int32, ByVal permits As BO.Application.BOPermit()) As Boolean
            Dim SAsToApply As BO.Application.CITES.Applications.BOPermitScientificAdvice() = BO.Application.CITES.Applications.BOCITESImportExportPermit.GetScientificAdvice(mainPermitId, False)
            Dim SavedSc As BO.Application.CITES.Applications.BOPermitScientificAdvice
            Dim Sa As BO.Application.CITES.Applications.BOPermitScientificAdvice = SAsToApply(0)
            For Each Permit As BO.Application.BOPermit In permits
                'For Each sa As BO.Application.CITES.Applications.BOPermitScientificAdvice In SAsToApply
                '    'alter the advice so that it is saved and linked to the current permit
                '    'only current advice is applied to other permits
                '    'advice previously added to the permit is left alone
                '    If sa.Current Then
                With Sa
                    .PermitId = Permit.PermitId
                    .DateOfAdvice = Date.Now
                    .SSOUserId = SSOUserId
                    .PermitScientificAdviceId = 0
                    SavedSc = .Save()
                    If Not SavedSc.ValidationErrors Is Nothing Then
                        Return False
                    End If
                End With
                '    End If
                'Next
            Next
        End Function

        Private Shared Function ApplySCsToOtherPermits(ByVal gwdApplying As Boolean, ByVal SSOUserId As Long, ByVal mainPermitId As Int32, ByVal permits As BO.Application.BOPermit()) As Boolean
            Dim SCsToApply As BO.Application.CITES.Applications.BOPermitSpecialCondition() = BO.Application.CITES.Applications.BOCITESImportExportPermit.GetSpecialConditions(mainPermitId, False)
            Dim SavedSc As BO.Application.CITES.Applications.BOPermitSpecialCondition
            For Each Permit As BO.Application.CITES.Applications.BOCITESImportExportPermit In permits
                'Some or all of the conditions previously added to the permit needs to be removed before the new ons are copied accross
                BO.Application.CITES.Applications.BOCITESImportExportPermit.RemoveAllSpecialConditions(gwdApplying, Permit.PermitId)

                For Each sc As BO.Application.CITES.Applications.BOPermitSpecialCondition In SCsToApply
                    'alter the condition so that it is saved and linked to the current permit
                    If Not Permit.HasCondition(sc.SpecialConditionId) Then
                        With sc
                            .PermitId = Permit.PermitId
                            .DateApplied = Date.Now
                            If gwdApplying Then
                                .StatusId = SpecialConditionStatus.Added_By_GWD
                            Else
                                .StatusId = SpecialConditionStatus.Rcmd_by_SA
                            End If
                            .AddedBySA = Not gwdApplying
                            .SSOUserId = SSOUserId
                            .PermitSpecialConditionId = 0
                            SavedSc = .Save()
                            If Not SavedSc.ValidationErrors Is Nothing Then
                                Return False
                            End If
                        End With
                    End If
                Next
            Next
        End Function

        Public Shared Function ApplySCAndSAToOtherPermits(ByVal gwdApplying As Boolean, ByVal SSOUserId As Long, ByVal mainPermitId As Int32, ByVal permitIdsToApplyTo As Int32()) As Boolean
            Dim PermitsToApplyTo As New ArrayList
            For Each id As Int32 In permitIdsToApplyTo
                PermitsToApplyTo.Add(BO.Application.BOPermit.PolymorphicCreate(id))
            Next

            If ApplySCsToOtherPermits(gwdApplying, SSOUserId, mainPermitId, CType(PermitsToApplyTo.ToArray(GetType(BO.Application.BOPermit)), BO.Application.BOPermit())) Then
                Return ApplySAsToOtherPermits(SSOUserId, mainPermitId, CType(PermitsToApplyTo.ToArray(GetType(BO.Application.BOPermit)), BO.Application.BOPermit()))
            End If
        End Function

        Public Function GetPermitInfos(ByVal showMultiplier As Boolean) As BOPermit.ProgressPermitGrid()
            Dim ReturnArray As New ArrayList
            SetPermits(Nothing)
            For Each SinglePermit As CITES.BOCITESPermit In Permit
                Dim PIs As BOPermitInfo() = SinglePermit.GetPermitInfos(Nothing)
                For Each pi As BOPermitInfo In PIs
                    ReturnArray.Add(New BOPermit.ProgressPermitGrid(Me, showMultiplier, pi, SinglePermit))
                Next
            Next
            Return CType(ReturnArray.ToArray(GetType(BOPermit.ProgressPermitGrid)), BOPermit.ProgressPermitGrid())
        End Function

        Friend Shared Function GetArticle10ApplicationDO(ByVal applicationId As Int32) As DataObjects.Entity.Article10
            Return GetArticle10ApplicationDO(applicationId, Nothing)
        End Function

        Friend Shared Function GetArticle10ApplicationDO(ByVal applicationId As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.Article10
            If applicationId = 0 Then
                Throw New ArgumentException("Application Id is 0")
            Else
                Dim CITESApps As DataObjects.EntitySet.CITESApplicationSet = DataObjects.Entity.CITESApplication.GetForApplication(applicationId)
                If Not CITESApps Is Nothing AndAlso _
                   CITESApps.Count = 1 Then
                    Dim Article10Apps As DataObjects.EntitySet.Article10Set = DataObjects.Entity.Article10.GetForCITESApplication(CType(CITESApps.GetEntity(0), DataObjects.Entity.CITESApplication).Id)
                    If Not Article10Apps Is Nothing AndAlso _
                       Article10Apps.Count = 1 Then
                        Return CType(Article10Apps.GetEntity(0), DataObjects.Entity.Article10)
                    End If
                End If
            End If
            Return Nothing
        End Function

        'Public Shared Function GetAccommodationandCare(ByVal citesApplicationId As Int32) As BO.Application.CITES.BOCareAccommodation()
        '    Dim ReturnArray As New ArrayList
        '    Dim App As New BO.Application.CITES.Applications.BOCITESApplication(citesApplicationId)
        '    For Each permit As BO.Application.BOPermit In App.Permit
        '        'Dim DOPertmit As New [DO].DataObjects.Entity.Permit(permit.PermitId)
        '        'DOPertmit.get()
        '        ' ReturnArray.Add(new BO.Application.CITES.BOCareAccommodation(
        '    Next
        'End Function

        Public Shared Function GetSpecimenReport(ByVal permitId As Int32, ByVal specimenId As Int32, ByVal tran As SqlClient.SqlTransaction) As BO.Application.BOSpecimenReport
            Return New BO.Application.BOSpecimenReport(permitId, specimenId, tran)
        End Function

        Public Shared Function GetDuplicates(ByVal permit As BO.Application.CITES.Applications.BOCITESImportExportPermit) As BO.Application.CITES.Applications.BOCITESImportExportPermit()

            Dim AgentId As Object
            Dim ImporterId As Int32
            Dim ExporterId As Int32
            Dim PurposeId As Object
            Dim ScientificName As String
            Dim Qty As Int32
            Dim CountryOfOriginId As Object
            Dim Sourceid1 As Object
            Dim Sourceid2 As Object

            Dim Config As New BO.BOConfiguration
            Dim CITESApp As BOCITESApplication = CType(BO.Application.BOApplication.PolymorphicCreate(permit.ApplicationId), BOCITESApplication)
            'Select Case appType
            '    Case Application.ApplicationTypes.Import
            '        Applic = New BO.Application.CITES.Applications.BOImportApplication(applicationId)
            '    Case Application.ApplicationTypes.Export
            '        Applic = New BO.Application.CITES.Applications.BOExportApplication(applicationId)
            '    Case Application.ApplicationTypes.Article10
            '        Applic = New BO.Application.CITES.Applications.BOCITESArticle10(applicationId)
            'End Select

            With CITESApp
                If Not .Agent Is Nothing AndAlso Not .Agent.Party Is Nothing Then
                    AgentId = .Agent.Party.PartyId
                End If

                If CType(.ApplicationTypeId, Application.ApplicationTypes) = ApplicationTypes.Export Or _
                    CType(.ApplicationTypeId, Application.ApplicationTypes) = ApplicationTypes.Import Then
                    If Not CType(CITESApp, BOImportExportApplication).Importer Is Nothing AndAlso Not CType(CITESApp, BOImportExportApplication).Importer.Party Is Nothing Then
                        ImporterId = CType(CITESApp, BOImportExportApplication).Importer.Party.PartyId
                    End If
                    If Not CType(CITESApp, BOImportExportApplication).Exporter Is Nothing AndAlso Not CType(CITESApp, BOImportExportApplication).Exporter.Party Is Nothing Then
                        ExporterId = CType(CITESApp, BOImportExportApplication).Exporter.Party.PartyId
                    End If
                Else
                    ImporterId = CType(CITESApp, BO.Application.CITES.Applications.BOCITESArticle10).Holder.Party.PartyId
                End If

            End With

            'Dim PermitSet As New ArrayList
            ' Dim NewDOPermitSet As [DO].DataObjects.EntitySet.CITESImportExportPermitSet

            If Not permit.Purpose Is Nothing Then PurposeId = permit.Purpose.ID
            If Not permit.Specie Is Nothing Then ScientificName = permit.Specie.ScientificName
            Qty = permit.Quantity
            If Not permit.CountryOfOrigin Is Nothing Then CountryOfOriginId = permit.CountryOfOrigin.ID
            If Not permit.Source1 Is Nothing Then Sourceid1 = permit.Source1.ID
            If Not permit.Source2 Is Nothing Then Sourceid2 = permit.Source2.ID

            'ScientificName = "1"
            'CountryOfOriginId = 6
            'Qty = 2
            'PurposeId = 4
            'Sourceid1 = 2
            'ImporterId = 1
            'ExporterId = 1

            Dim Result As Object = Config.GetValue("PossibleDuplicatesNumberOfDays")
            If Not Result Is Nothing AndAlso _
                Config.IsInt32(Result) Then
                Dim Results As DataSet = DataObjects.Sprocs.dbo_usp_DuplicateApplications(permit.PermitId, AgentId, ImporterId, _
                                            ExporterId, ScientificName, Sourceid1, Sourceid2, PurposeId, _
                                            CountryOfOriginId, Qty, CType(Result, Int32), _
                                            Nothing, GetType([DO].DataObjects.EntitySet.CITESImportExportPermitSet))


                Config = Nothing

                If Not Results Is Nothing AndAlso _
                    Results.Tables.Count > 0 Then
                    Dim ReturnCollection(CType(Results, [DO].DataObjects.EntitySet.CITESImportExportPermitSet).Entities.Count - 1) As BO.Application.CITES.Applications.BOCITESImportExportPermit
                    Dim [Loop] As Int32 = 0
                    For Each DOPermit As [DO].DataObjects.Entity.CITESImportExportPermit In CType(Results, [DO].DataObjects.EntitySet.CITESImportExportPermitSet).Entities
                        ReturnCollection([Loop]) = New BO.Application.CITES.Applications.BOCITESImportExportPermit(DOPermit.Id)
                        [Loop] += 1
                    Next
                    Return ReturnCollection
                End If
            End If
        End Function

        Protected Function CheckErrorExists(ByVal validationOwner As BaseBO, ByVal [error] As ValidationError.ValidationCodes) As Boolean
            If Not validationOwner Is Nothing AndAlso _
                    Not validationOwner.ValidationErrors Is Nothing AndAlso _
                    Not validationOwner.ValidationErrors.Errors Is Nothing Then

                For Each err As BOError In validationOwner.ValidationErrors.Errors
                    If err.ID = [error] Then
                        Return True
                    End If
                Next err
            End If
            Return False
        End Function

        'Public Function GetDuplicates(ByVal agentLinkId As Object, ByVal partyLinkId As Int32, ByVal importerLinkId As Object, _
        '    ByVal exporterLinkId As Int32, ByVal purposeId As Object, _
        '    ByVal scientificName As String, ByVal qty As Int32, ByVal numberofdays As Int32, ByVal countryoforiginid As Object, ByVal sourceid1 As Object, _
        '    ByVal sourceid2 As Object) As BOCITESApplication()

        '    DataObjects.Sprocs.dbo_usp_DuplicateApplications(agentLinkId, importerLinkId, exporterLinkId, scientificName, sourceid1, sourceid2, _
        '        purposeId, countryoforiginid, qty, numberofdays, Nothing)
        'End Function

        Public Overrides Function Clone() As Object
            Dim BaseApplication As Application.CITES.Applications.BOCITESApplication = CType(MyBase.Clone, Application.CITES.Applications.BOCITESApplication)
            With BaseApplication
                .CITESApplicationId = 0
            End With
            Return BaseApplication
        End Function

#End Region

#Region " Save "
        Public Overloads Overrides Function Save(ByVal ignoreValidation As Boolean) As BaseBO
            Dim NewApplication As New DataObjects.Entity.CITESApplication
            Dim service As DataObjects.Service.CITESApplicationService = NewApplication.ServiceObject
            Dim tran As SqlClient.SqlTransaction = service.BeginTransaction

            Dim SaveResult As BaseBO = MyClass.Save(tran, ignoreValidation)
            If SaveResult Is Nothing Then
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
            Else
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Commit)
            End If
            Return SaveResult
        End Function

        Public Overridable Overloads Function Save(ByVal tran As SqlClient.SqlTransaction, ByVal ignoreValidation As Boolean) As BaseBO
            Dim NewCITESApplication As New DataObjects.Entity.CITESApplication
            Dim service As DataObjects.Service.CITESApplicationService = NewCITESApplication.ServiceObject



            Dim Application As Object = MyBase.Save(tran, ignoreValidation)
            Created = (mCITESApplicationId = 0)
            If Not Application Is Nothing AndAlso _
               Not CType(Application, BaseBO).ValidationErrors Is Nothing Then
                'rollback the transaction
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                'get the problems and assign them locally
                ValidationErrors = CType(Application, BaseBO).ValidationErrors
                'bail
                Return Me
            End If

            If Not ManagementAuthorityId Is Nothing Then
                mManagementAuthority = mManagementAuthority.Save(tran)
                If Not mManagementAuthority Is Nothing AndAlso _
                   Not mManagementAuthority.ValidationErrors Is Nothing Then
                    'rollback the transaction
                    If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    'get the problems and assign them locally
                    ValidationErrors = mManagementAuthority.ValidationErrors
                    Return Me
                End If
            End If

            If Not ForeignManagementAuthorityId Is Nothing Then
                Me.mForeignManagementAuthority = mForeignManagementAuthority.Save(tran)
                If Not mForeignManagementAuthority Is Nothing AndAlso _
                   Not mForeignManagementAuthority.ValidationErrors Is Nothing Then
                    'rollback the transaction
                    If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    'get the problems and assign them locally
                    ValidationErrors = mForeignManagementAuthority.ValidationErrors
                    Return Me
                End If
            End If

            If Not SecondPartyId Is Nothing Then
                mSecondParty = mSecondParty.Save(tran)
                If Not mSecondParty Is Nothing AndAlso _
                   Not mSecondParty.ValidationErrors Is Nothing Then
                    'rollback the transaction
                    If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    'get the problems and assign them locally
                    ValidationErrors = mSecondParty.ValidationErrors
                    Return Me
                End If
            End If

            If Created Then
                NewCITESApplication = service.Insert(ApplicationId, _
                                                     ManagementAuthorityId, _
                                                     SecondPartyId, _
                                                     ForeignManagementAuthorityId, _
                                                     CountryOfImportId, _
                                                     LocationAddressId, _
                                                     Consignment, _
                                                     IsComposite, _
                                                     tran)

            Else
                NewCITESApplication = service.Update(mCITESApplicationId, _
                                                     ApplicationId, _
                                                     ManagementAuthorityId, _
                                                     SecondPartyId, _
                                                     ForeignManagementAuthorityId, _
                                                     CountryOfImportId, _
                                                     LocationAddressId, _
                                                     Consignment, _
                                                     IsComposite, _
                                                      CITESChecksum, _
                                                     tran)
            End If

            ''check to see if any SQL errors have occured
            If (NewCITESApplication Is Nothing AndAlso Not DataObjects.Sprocs.LastError Is Nothing) Then
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSavePermit)
            Else
                If Created And Not NewCITESApplication Is Nothing Then
                    mCITESApplicationId = NewCITESApplication.Id
                End If



                If Not mAdditionalDeclaration Is Nothing Then
                    If mAdditionalDeclaration.CITESApplicationId = 0 Then
                        mAdditionalDeclaration.CITESApplicationId = mCITESApplicationId
                    End If

                    Dim AddDec As Object = mAdditionalDeclaration.Save(tran)
                    If Not AddDec Is Nothing Then
                        If Not CType(AddDec, BaseBO).ValidationErrors Is Nothing Then
                            'rollback the transaction
                            If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                            ValidationErrors = CType(AddDec, BaseBO).ValidationErrors
                            Return Me
                        End If
                    End If
                End If

                Try
                    If NewCITESApplication.CheckSum <> CITESChecksum Then
                        InitialiseCITESApplication(NewCITESApplication, tran)
                    End If
                Catch ex As Exception
                    If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSavePermit)
                End Try
            End If

            Return Me
        End Function
#End Region

#Region " Validate "
        'Refactored MLD 26/10/4
        Protected Overridable Sub CheckMandatory(ByVal owner As BaseBO)
            AddErrorOnCondition(owner, DeclarationsMustBeAcknowledged, Not mAdditionalDeclaration Is Nothing AndAlso Not mAdditionalDeclaration.Confirmed)
        End Sub

        'Added MLD 26/10/4
        Protected Sub AddErrorOnCondition(ByVal owner As BaseBO, ByVal errorType As ValidationCodes, ByVal condition As Boolean)
            If Not CheckErrorExists(owner, errorType) AndAlso condition Then
                owner.ValidationErrors.AddError(New ValidationError(errorType))
            End If
        End Sub
#End Region
        '#Region " Validate "
        '        Public Overridable Overloads Function Validate(ByVal writeFlag As Boolean, ByVal ignoreWarnings As Boolean) As ValidationManager Implements IBOCITESApplication.Validate
        '            Return Validate(writeFlag, ignoreWarnings)
        '        End Function
        '#End Region





    End Class
End Namespace
Namespace Application
    Public Class BOPermitInfo
        Inherits BaseBO
        Implements IBOPermitInfo ', IBOPermitInfoClock


#Region " Enum "
        <Serializable()> _
        Public Enum PermitStatusTypes
            NoStatus = 0 'here so that proxy code works
            BeingInput_Customer = 1
            BeingInput_CaseOfficer = 2
            SubmittedByCustomer = 3
            ProgressAllowed = 4
            Referred = 5
            UnderAppeal = 6
            Authorised = 7
            Issued = 8                          ' Inspection - 1
            Refused = 9
            CancelPending = 10
            Cancelled = 11                      ' Inspection - 3
            UpdatedSemiComplete = 12
            ReturnedUnused = 13
            ReturnedUsed = 14
            ReferredForCAndA = 15
            ReferredForSpecimenReport = 16
            IssuedDraft = 17
            Rejected = 18 'inactive
            Re_Issue = 19 'PJN Added
            Fate = 20 'PJN Added
            Duplicate = 21 'PJN Added
            ErrorCorrection = 22
            Print_Semi_Complete_Reminder_Letter = 23
            Ring_Request_Submitted_By_Customer = 24
            Chick_DOR_Issued = 25
            Adult_DOR_Issued = 26
            DOR_Returned = 27
            Registered = 28
            Closed_By_Customer = 29
            Duplicate_Requested = 30
            AmendmentRequested = 31
        End Enum

        'completed - Inspection - 2
        'declined - Inspection - 4
        'paid - payment - 1
        'unpaid - payment - 2
        'pending - payment - 3

#End Region

#Region " Prelim code "
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal permitInfoId As Int32, ByVal tran As SqlClient.SqlTransaction)
            MyClass.New(New DataObjects.Entity.PermitInfo(permitInfoId, tran), tran)
        End Sub

        Public Sub New(ByVal permitInfoId As Int32)
            MyClass.New(permitInfoId, Nothing)
        End Sub

        'Public Sub New(ByVal permitInfoId() As Int32)
        '    MyClass.New(permitInfoId, Nothing)
        'End Sub

        Public Sub New(ByVal doPermitInfo As DataObjects.Entity.PermitInfo, ByVal tran As SqlClient.SqlTransaction)
            MyClass.New()
            LoadPermitInfo(doPermitInfo, tran)
        End Sub

        Private Function LoadPermitInfo(ByVal doPermitInfo As DataObjects.Entity.PermitInfo) As DataObjects.Entity.PermitInfo
            Return LoadPermitInfo(doPermitInfo, Nothing)
        End Function

        Private Function LoadPermitInfo(ByVal doPermitInfo As DataObjects.Entity.PermitInfo, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.PermitInfo
            '            Dim NewPermitInfo As DataObjects.Entity.PermitInfo = DataObjects.Entity.PermitInfo.GetById(id(0), tran)

            If doPermitInfo Is Nothing Then
                'doesn't exist = so create a blank one
                InitialisePermitInfo(CreateBlank, tran)
            Else
                InitialisePermitInfo(doPermitInfo, tran)
            End If
            Return doPermitInfo
        End Function

        Friend Overridable Sub InitialisePermitInfo(ByVal permitInfo As DataObjects.Entity.PermitInfo, ByVal tran As SqlClient.SqlTransaction)
            Try
                With permitInfo
                    CheckSum = .CheckSum
                    mPermitId = .PermitId
                    mPermitInfoId = .PermitInfoId
                    mReReferredCount = .ReReferralCount
                    If .AssignedTo > 0 Then mAssignedTo = New ReferenceData.BOStatusAssignedToGroup(.AssignedTo, tran)
                    mCreatedDate = .CreatedDate
                    If .CreatedByUserId > 0 Then mCreatedByUser = New BOAuthorisedUser(CType(.CreatedByUserId, Int64))

                    'load the status
                    If Not .IsProgressStatusInspectionIdNull Then mProgressStatusInspection = New Application.ProgressStatus.BOProgressStatusInspection(.ProgressStatusInspectionId, tran)
                    If Not .IsProgressStatusPaymentIdNull Then mProgressStatusPayment = New Application.ProgressStatus.BOProgressStatusPayment(.ProgressStatusPaymentId, tran)
                    If Not .IsProgressStatusReferralHistoryIdNull Then mProgressStatusReferralHistory = New Application.ProgressStatus.BOProgressStatusReferralHistory(.ProgressStatusReferralHistoryId, tran)
                    If Not .IsProgressStatusReIssuedIdNull Then mProgressStatusReIssued = New Application.ProgressStatus.BOProgressStatusReIssued(.ProgressStatusReIssuedId, tran)
                    If Not .IsProgressStatusSAAdviceIdNull Then mProgressStatusSAAdvice = New Application.ProgressStatus.BOProgressStatusSAAdvice(.ProgressStatusSAAdviceId, tran)
                    If Not .IsPermitStatusIdNull Then mPermitStatus = New ReferenceData.BOPermitStatus(.PermitStatusId, tran)

                    'clock
                    'mGWDClock = .GWDClock
                    'mJNCCClock = .JNCCClock
                    'mInspectorateClock = .InspectorateClock
                    'mKewClock = .KewClock
                    'If Not .IsGWDClockStartDateNull Then mGWDClockStartDate = .GWDClockStartDate
                    'If Not .IsJNCCClockStartDateNull Then mJNCCClockStartDate = .JNCCClockStartDate
                    'If Not .IsInspectorateClockStartDateNull Then mInspectorateClockStartDate = .InspectorateClockStartDate
                    'If Not .IsKewClockStartDateNull Then mKewClockStartDate = .KewClockStartDate

                    If Not .IsCancelReasonNull Then mCancelReason = .CancelReason
                    If Not .IsCancelPendingReasonNull Then mCancelPendingReason = .CancelPendingReason
                    If Not .IsCancelPendingDeclineReasonNull Then mCancelPendingReason = .CancelPendingDeclineReason
                    If Not .IsNextActionDateNull Then mNextActionDate = .NextActionDate

                    If Not .IsRefusalReasonIdNull Then mRefusalReason = New ReferenceData.BORefusalReason(.RefusalReasonId, tran)
                    If Not .IsRefusalReasonNull Then mRefusalReasonAdHoc = .RefusalReason
                    If Not .IsDateRefusedNull Then mDateRefused = .DateRefused

                    If Not .IsIssueDateNull Then mIssueDate = .IssueDate
                    If Not .IsDateAuthorisedNull Then Me.mAuthorisationDate = .DateAuthorised
                    If Not .IsIssueNull Then mIssue = .Issue
                    If Not .IsIssuedByNull AndAlso .IssuedBy > 0 Then Me.mIssuedBy = New BOAuthorisedUser(CType(.IssuedBy, Int64))
                    If Not .IsPlaceOfIssueNull Then mPlaceOfIssue = .PlaceOfIssue
                    Me.mSequenceNumber = .SequenceNumber

                    If Not .IsSemiCompleteSpecimenIdNull Then mSemiCompleteSpecimenId = .SemiCompleteSpecimenId
                    If Not .IsSemiCompleteSpecieIdNull Then mSemiCompleteSpecieId = .SemiCompleteSpecieId
                    If Not .IsSemiCompleteSecondPartyIdNull Then mSemiCompleteSecondPartyId = .SemiCompleteSecondPartyId
                    If Not .IsSemiCompleteCountryIdNull Then mSemiCompleteCountryId = .SemiCompleteCountryId
                    If Not .IsSemiCompleteUOMIdNull Then mSemiCompleteUOMId = .SemiCompleteUOMId

                End With
            Catch ex As Exception
            End Try
        End Sub

        Private Function CreateBlank() As DataObjects.Entity.PermitInfo
            Dim Blank As New DataObjects.Entity.PermitInfo
            With Blank
                .CreateEmptyEntity()
                .Id = 0
                .ReReferralCount = 0
                .CreatedDate = Date.Now

                'all the other fields are nullable and we want them that way, so leave them alone
                'as they are null by default.
            End With
            Return Blank
        End Function
#End Region

#Region " Properties "

        Public Property DateRefused() As Object Implements IBOPermitInfo.DateRefused
            Get
                Return DateRefused
            End Get
            Set(ByVal Value As Object)
                mDateRefused = Value
            End Set
        End Property
        Private mDateRefused As Object

        Protected ReadOnly Property IssueDate_String() As String
            Get
                If mIssueDate Is Nothing Then
                    Return String.Empty
                Else
                    Return CType(mIssueDate, Date).ToString("dd MMMM yyyy")
                End If
            End Get
        End Property

        Protected ReadOnly Property AuthoriseDate_String() As String
            Get
                If Me.mAuthorisationDate Is Nothing Then
                    Return String.Empty
                Else
                    Return CType(Me.mAuthorisationDate, Date).ToString("dd MMMM yyyy")
                End If
            End Get
        End Property

        Public Property GridColumn_Payment() As String
            Get
                If Not mProgressStatusPayment Is Nothing Then
                    Return mProgressStatusPayment.Code
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal Value As String)
            End Set
        End Property

        Public Property GridColumn_Inspection() As String
            Get
                If Not mProgressStatusInspection Is Nothing Then
                    Return mProgressStatusInspection.Code
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal Value As String)
            End Set
        End Property

        Public Property GridColumn_SAAdvice() As String
            Get
                If Not mProgressStatusSAAdvice Is Nothing Then
                    Return mProgressStatusSAAdvice.Code
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal Value As String)
            End Set
        End Property

        Public Property GridColumn_ReferralHistory() As String
            Get
                If Not mProgressStatusReferralHistory Is Nothing Then
                    Dim Extra As String = ""
                    If mReReferredCount > 2 Then Extra = (mReReferredCount - 1).ToString
                    Return mProgressStatusReferralHistory.Code & Extra
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal Value As String)
            End Set
        End Property

        Public Property GridColumn_ReIssued() As String
            Get
                If Not mProgressStatusReIssued Is Nothing Then
                    Return mProgressStatusReIssued.Code
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal Value As String)
            End Set
        End Property

        Public Property GridColumn_Status() As String
            Get
                If Not mPermitStatus Is Nothing Then
                    Return mPermitStatus.Description
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal Value As String)
            End Set
        End Property

        Private Const WAIT_DAYS As Int32 = 30
        Public Property GridColumn_Status_Customer() As String
            Get
                mGridColumn_Status_Customer = String.Empty
                If Not mPermitStatus Is Nothing AndAlso _
                   (mGridColumn_Status_Customer Is Nothing OrElse _
                   mGridColumn_Status_Customer.Length = 0) Then
                    Select Case CType(mPermitStatus.ID, PermitStatusTypes)
                        Case PermitStatusTypes.BeingInput_Customer, _
                             PermitStatusTypes.CancelPending, _
                             PermitStatusTypes.Cancelled, _
                             PermitStatusTypes.UpdatedSemiComplete, _
                             PermitStatusTypes.ReturnedUnused, _
                             PermitStatusTypes.ReturnedUsed
                            mGridColumn_Status_Customer = mPermitStatus.Description
                        Case PermitStatusTypes.SubmittedByCustomer
                            mGridColumn_Status_Customer = "Submitted"
                        Case PermitStatusTypes.ProgressAllowed, _
                             PermitStatusTypes.UnderAppeal, _
                             PermitStatusTypes.Authorised, _
                             PermitStatusTypes.IssuedDraft
                            mGridColumn_Status_Customer = "In Progress"
                        Case PermitStatusTypes.ReferredForCAndA, _
                             PermitStatusTypes.ReferredForSpecimenReport
                            mGridColumn_Status_Customer = "Referred To Customer"
                        Case PermitStatusTypes.CancelPending
                            mGridColumn_Status_Customer = "Referred To Customer"
                        Case PermitStatusTypes.Refused
                            If Date.Compare(Date.op_Addition(GetLastDate(), New TimeSpan(WAIT_DAYS, 0, 0, 0)), Date.Now) >= 0 Then
                                mGridColumn_Status_Customer = mPermitStatus.Description
                            Else
                                mGridColumn_Status_Customer = "In Progress"
                            End If
                        Case PermitStatusTypes.Issued
                            If Date.Compare(Date.op_Addition(GetLastDate(), New TimeSpan(WAIT_DAYS, 0, 0, 0)), Date.Now) >= 0 Then
                                mGridColumn_Status_Customer = "Refused"
                            Else
                                mGridColumn_Status_Customer = "In Progress"
                            End If
                        Case PermitStatusTypes.Referred
                            If AssignedTo.AssignedTo = Common.AssignedToList.Customer Then
                                mGridColumn_Status_Customer = "Referred To Customer"
                            Else
                                mGridColumn_Status_Customer = "In Progress"
                            End If
                            'Case PermitStatusTypes.Rejected
                            '    mGridColumn_Status_Customer = "Rejected"
                    End Select
                End If
                Return mGridColumn_Status_Customer
            End Get
            Set(ByVal Value As String)
            End Set
        End Property
        Private mGridColumn_Status_Customer As String = Nothing

        Private ReadOnly Property GetLastDate() As Date
            Get
                Dim History As DataObjects.Entity.PermitHistory = Me.GetFirstHistory(mPermitId)
                If History Is Nothing Then
                    'use the created date as we have no history
                    Return mCreatedDate
                Else
                    Return History.ChangeDate
                End If
            End Get
        End Property

        Public Property AssignedTo() As ReferenceData.BOStatusAssignedToGroup Implements IBOPermitInfo.AssignedTo
            Get
                Return mAssignedTo
            End Get
            Set(ByVal Value As ReferenceData.BOStatusAssignedToGroup)
                mAssignedTo = Value
            End Set
        End Property
        Private mAssignedTo As ReferenceData.BOStatusAssignedToGroup

        Public Property PermitId() As Integer Implements IBOPermitInfo.PermitId
            Get
                Return mPermitId
            End Get
            Set(ByVal Value As Integer)
                mPermitId = Value
            End Set
        End Property
        Private mPermitId As Int32

        Public Property PermitStatus() As ReferenceData.BOPermitStatus Implements IBOPermitInfo.PermitStatus
            Get
                Return mPermitStatus
            End Get
            Set(ByVal Value As ReferenceData.BOPermitStatus)
                mPermitStatus = Value
                mGridColumn_Status_Customer = Nothing
            End Set
        End Property
        Private mPermitStatus As ReferenceData.BOPermitStatus

        Public Property ProgressStatusInspection() As ProgressStatus.BOProgressStatusInspection Implements IBOPermitInfo.ProgressStatusInspection
            Get
                Return mProgressStatusInspection
            End Get
            Set(ByVal Value As ProgressStatus.BOProgressStatusInspection)
                mProgressStatusInspection = Value
            End Set
        End Property
        Private mProgressStatusInspection As ProgressStatus.BOProgressStatusInspection

        Public Property ProgressStatusPayment() As ProgressStatus.BOProgressStatusPayment Implements IBOPermitInfo.ProgressStatusPayment
            Get
                Return mProgressStatusPayment
            End Get
            Set(ByVal Value As ProgressStatus.BOProgressStatusPayment)
                mProgressStatusPayment = Value
            End Set
        End Property
        Private mProgressStatusPayment As ProgressStatus.BOProgressStatusPayment

        Public Property ProgressStatusReferralHistory() As ProgressStatus.BOProgressStatusReferralHistory Implements IBOPermitInfo.ProgressStatusReferralHistory
            Get
                Return mProgressStatusReferralHistory
            End Get
            Set(ByVal Value As ProgressStatus.BOProgressStatusReferralHistory)
                mProgressStatusReferralHistory = Value
            End Set
        End Property
        Private mProgressStatusReferralHistory As ProgressStatus.BOProgressStatusReferralHistory

        Public Property ProgressStatusReIssued() As ProgressStatus.BOProgressStatusReIssued Implements IBOPermitInfo.ProgressStatusReIssued
            Get
                Return mProgressStatusReIssued
            End Get
            Set(ByVal Value As ProgressStatus.BOProgressStatusReIssued)
                mProgressStatusReIssued = Value
            End Set
        End Property
        Private mProgressStatusReIssued As ProgressStatus.BOProgressStatusReIssued

        Public Property ProgressStatusSAAdvice() As ProgressStatus.BOProgressStatusSAAdvice Implements IBOPermitInfo.ProgressStatusSAAdvice
            Get
                Return mProgressStatusSAAdvice
            End Get
            Set(ByVal Value As ProgressStatus.BOProgressStatusSAAdvice)
                mProgressStatusSAAdvice = Value
            End Set
        End Property
        Private mProgressStatusSAAdvice As ProgressStatus.BOProgressStatusSAAdvice

        Public Property ReReferredCount() As Integer
            Get
                Return mReReferredCount
            End Get
            Set(ByVal Value As Integer)
                mReReferredCount = Value
            End Set
        End Property
        Private mReReferredCount As Int32

        Public Property CreatedDate() As Date
            Get
                Return mCreatedDate
            End Get
            Set(ByVal Value As Date)
                mCreatedDate = Value
            End Set
        End Property
        Private mCreatedDate As Date

        Public Property CreatedByUser() As BOAuthorisedUser
            Get
                Return mCreatedByUser
            End Get
            Set(ByVal Value As BOAuthorisedUser)
                mCreatedByUser = Value
            End Set
        End Property
        Private mCreatedByUser As BOAuthorisedUser

        'Public Property GWDClock() As Integer Implements IBOPermitInfoClock.GWDClock
        '    Get
        '        Return mGWDClock
        '    End Get
        '    Set(ByVal Value As Integer)
        '        mGWDClock = Value
        '    End Set
        'End Property
        'Private mGWDClock As Int32

        'Public Property InspectorateClock() As Integer Implements IBOPermitInfoClock.InspectorateClock
        '    Get
        '        Return mInspectorateClock
        '    End Get
        '    Set(ByVal Value As Integer)
        '        mInspectorateClock = Value
        '    End Set
        'End Property
        'Private mInspectorateClock As Int32

        'Public Property JNCCClock() As Integer Implements IBOPermitInfoClock.JNCCClock
        '    Get
        '        Return mJNCCClock
        '    End Get
        '    Set(ByVal Value As Integer)
        '        mJNCCClock = Value
        '    End Set
        'End Property
        'Private mJNCCClock As Int32

        'Public Property KewClock() As Integer Implements IBOPermitInfoClock.KewClock
        '    Get
        '        Return mKewClock
        '    End Get
        '    Set(ByVal Value As Integer)
        '        mKewClock = Value
        '    End Set
        'End Property
        'Private mKewClock As Int32

        Public Property NextActionDate() As Object Implements IBOPermitInfo.NextActionDate
            Get
                Return mNextActionDate
            End Get
            Set(ByVal Value As Object)
                mNextActionDate = Value
            End Set
        End Property
        Private mNextActionDate As Object

        Public Property CancelReason() As Object Implements IBOPermitInfo.CancelReason
            Get
                Return mCancelReason
            End Get
            Set(ByVal Value As Object)
                mCancelReason = Value
            End Set
        End Property
        Private mCancelReason As Object

        Public Property CancelPendingReason() As Object Implements IBOPermitInfo.CancelPendingReason
            Get
                Return mCancelPendingReason
            End Get
            Set(ByVal Value As Object)
                mCancelPendingReason = Value
            End Set
        End Property
        Private mCancelPendingReason As Object

        Public Property CancelPendingDeclineReason() As Object Implements IBOPermitInfo.CancelPendingDeclineReason
            Get
                Return mCancelPendingDeclineReason
            End Get
            Set(ByVal Value As Object)
                mCancelPendingDeclineReason = Value
            End Set
        End Property
        Private mCancelPendingDeclineReason As Object

        Public Property SequenceNumber() As Int32 Implements IBOPermitInfo.SequenceNumber
            Get
                Return mSequenceNumber
            End Get
            Set(ByVal Value As Int32)
                mSequenceNumber = Value
            End Set
        End Property
        Private mSequenceNumber As Int32


        Public Property PermitInfoId() As Int32 Implements IBOPermitInfo.PermitInfoId
            Get
                Return mPermitInfoId
            End Get
            Set(ByVal Value As Int32)
                mPermitInfoId = Value
            End Set
        End Property
        Private mPermitInfoId As Int32

        Public Property PrintJobId() As Object Implements IBOPermitInfo.PrintJobId
            Get
                Return mPrintJobId
            End Get
            Set(ByVal Value As Object)
                mPrintJobId = Value
            End Set
        End Property
        Private mPrintJobId As Object

        Public Property CoveringLetterReportId() As Object Implements IBOPermitInfo.CoveringLetterReportId
            Get
                Return mCoveringLetterReportId
            End Get
            Set(ByVal Value As Object)
                mCoveringLetterReportId = Value
            End Set
        End Property
        Private mCoveringLetterReportId As Object

        Public Property SemiCompleteSpecimenId() As Object Implements IBOPermitInfo.SemiCompleteSpecimenId
            Get
                Return mSemiCompleteSpecimenId
            End Get
            Set(ByVal Value As Object)
                mSemiCompleteSpecimenId = Value
            End Set
        End Property
        Private mSemiCompleteSpecimenId As Object

        Public Property SemiCompleteSpecimen() As BOSpecimen
            Get
                If Not mSemiCompleteSpecimenId Is Nothing Then Return New BOSpecimen(CType(mSemiCompleteSpecimenId, Int32), Nothing)
            End Get
            Set(ByVal Value As BOSpecimen)
            End Set
        End Property

        Public Property IsLiveDerivative() As Boolean
            Get
                Dim Config As New BOConfiguration
                Dim CitesPermit As BO.Application.CITES.BOCITESPermit = BO.Application.CITES.BOCITESPermit.GetByPermitId(Me.PermitId)
                Dim ReturnValue As Boolean = Config.IsLiveDerivative(CitesPermit.Derivative)
                Config = Nothing
                Return ReturnValue
            End Get
            Set(ByVal Value As Boolean)

            End Set
        End Property
        Public Property SemiCompleteSpecieId() As Object Implements IBOPermitInfo.SemiCompleteSpecieId
            Get
                Return mSemiCompleteSpecieId
            End Get
            Set(ByVal Value As Object)
                mSemiCompleteSpecieId = Value
            End Set
        End Property
        Private mSemiCompleteSpecieId As Object

        Public Property SemiCompleteSpecie() As BOSpecie
            Get
                If Not mSemiCompleteSpecieId Is Nothing Then Return New BOSpecie(CType(mSemiCompleteSpecieId, Int32), Nothing)
            End Get
            Set(ByVal Value As BOSpecie)
            End Set
        End Property

        Public Property SemiCompleteSecondPartyId() As Object Implements IBOPermitInfo.SemiCompleteSecondPartyId
            Get
                Return mSemiCompleteSecondPartyId
            End Get
            Set(ByVal Value As Object)
                mSemiCompleteSecondPartyId = Value
            End Set
        End Property
        Private mSemiCompleteSecondPartyId As Object

        Public Property SemiCompleteCountryId() As Object Implements IBOPermitInfo.SemiCompleteCountryId
            Get
                Return mSemiCompleteCountryId
            End Get
            Set(ByVal Value As Object)
                mSemiCompleteCountryId = Value
            End Set
        End Property
        Private mSemiCompleteCountryId As Object

        Public Property SemiCompleteUOMId() As Object Implements IBOPermitInfo.semicompleteuomid
            Get
                Return mSemiCompleteUOMId
            End Get
            Set(ByVal Value As Object)
                mSemiCompleteUOMId = Value
            End Set
        End Property
        Private mSemiCompleteUOMId As Object

        Public Property RefusalReason() As ReferenceData.BORefusalReason
            Get
                Return mRefusalReason
            End Get
            Set(ByVal Value As ReferenceData.BORefusalReason)
                mRefusalReason = Value
            End Set
        End Property
        Private mRefusalReason As ReferenceData.BORefusalReason

        Public Property RefusalReasonAdHoc() As String
            Get
                Return mRefusalReasonAdHoc
            End Get
            Set(ByVal Value As String)
                mRefusalReasonAdHoc = Value
            End Set
        End Property
        Private mRefusalReasonAdHoc As String

        'PN added
        Public Property AuthorisationDate() As Object
            Get
                Return mAuthorisationDate
            End Get
            Set(ByVal Value As Object)
                mAuthorisationDate = Value
            End Set
        End Property
        Private mAuthorisationDate As Object

        Public Property IssueDate() As Object
            Get
                Return mIssueDate
            End Get
            Set(ByVal Value As Object)
                mIssueDate = Value
            End Set
        End Property
        Private mIssueDate As Object

        Public Property Issue() As Object
            Get
                Return mIssue
            End Get
            Set(ByVal Value As Object)
                mIssue = Value
            End Set
        End Property
        Private mIssue As Object

        Public Property IssuedBy() As BOAuthorisedUser
            Get
                Return mIssuedBy
            End Get
            Set(ByVal Value As BOAuthorisedUser)
                mIssuedBy = Value
            End Set
        End Property
        Private mIssuedBy As BOAuthorisedUser

        Public Property PlaceOfIssue() As String
            Get
                Return mPlaceOfIssue
            End Get
            Set(ByVal Value As String)
                mPlaceOfIssue = Value
            End Set
        End Property
        Private mPlaceOfIssue As String
#End Region

#Region " Helper Functions "
        Protected ReadOnly Property AssignedToId() As Int32
            Get
                Return mAssignedTo.ID
            End Get
        End Property

        Protected ReadOnly Property CreatedByUserId() As Int64
            Get
                Return mCreatedByUser.SSOUserid
            End Get
        End Property

        Protected ReadOnly Property IssuedById() As Object
            Get
                If mIssuedBy Is Nothing OrElse mIssuedBy.SSOUserid = 0 Then
                    Return Nothing
                Else
                    Return mIssuedBy.SSOUserid
                End If
            End Get
        End Property

        Protected ReadOnly Property ProgressStatusInspectionId() As Object
            Get
                If mProgressStatusInspection Is Nothing OrElse mProgressStatusInspection.ID = 0 Then
                    Return Nothing
                Else
                    Return mProgressStatusInspection.ID
                End If
            End Get
        End Property

        Protected ReadOnly Property RefusalReasonId() As Object
            Get
                If mRefusalReason Is Nothing OrElse mRefusalReason.ID = 0 Then
                    Return Nothing
                Else
                    Return mRefusalReason.ID
                End If
            End Get
        End Property

        Public ReadOnly Property ProgressStatusPaymentId() As Object
            Get
                If mProgressStatusPayment Is Nothing OrElse mProgressStatusPayment.ID = 0 Then
                    Return Nothing
                Else
                    Return mProgressStatusPayment.ID
                End If
            End Get
        End Property

        Protected ReadOnly Property ProgressStatusReferralHistoryId() As Object
            Get
                If mProgressStatusReferralHistory Is Nothing OrElse mProgressStatusReferralHistory.ID = 0 Then
                    Return Nothing
                Else
                    Return mProgressStatusReferralHistory.ID
                End If
            End Get
        End Property

        Protected ReadOnly Property ProgressStatusReIssuedId() As Object
            Get
                If mProgressStatusReIssued Is Nothing OrElse mProgressStatusReIssued.ID = 0 Then
                    Return Nothing
                Else
                    Return mProgressStatusReIssued.ID
                End If
            End Get
        End Property

        Protected ReadOnly Property ProgressStatusSAAdviceId() As Object
            Get
                If mProgressStatusSAAdvice Is Nothing OrElse mProgressStatusSAAdvice.ID = 0 Then
                    Return Nothing
                Else
                    Return mProgressStatusSAAdvice.ID
                End If
            End Get
        End Property

        Protected ReadOnly Property PermitStatusId() As Object
            Get
                If mPermitStatus Is Nothing OrElse mPermitStatus.ID = 0 Then
                    Return Nothing
                Else
                    Return mPermitStatus.ID
                End If
            End Get
        End Property

        Friend WriteOnly Property SetPermitStatusId() As Object
            Set(ByVal Value As Object)
                If Value Is Nothing OrElse Not TypeOf Value Is Int32 Then
                    mPermitStatus = Nothing
                Else
                    mPermitStatus = New ReferenceData.BOPermitStatus(CType(Value, Int32))
                End If
            End Set
        End Property

        Friend WriteOnly Property SetAssignedToId() As Object
            Set(ByVal Value As Object)
                If Value Is Nothing OrElse Not TypeOf Value Is Int32 Then
                    mAssignedTo = Nothing
                Else
                    mAssignedTo = New ReferenceData.BOStatusAssignedToGroup(CType(Value, Int32))
                End If
            End Set
        End Property

        'Public Property GWDClockStartDate() As Object Implements IBOPermitInfoClock.GWDClockStartDate
        '    Get
        '        Return mGWDClockStartDate
        '    End Get
        '    Set(ByVal Value As Object)
        '        mGWDClockStartDate = Value
        '    End Set
        'End Property
        'Private mGWDClockStartDate As Object

        'Public Property InspectorateClockStartDate() As Object Implements IBOPermitInfoClock.InspectorateClockStartDate
        '    Get
        '        Return mInspectorateClockStartDate
        '    End Get
        '    Set(ByVal Value As Object)
        '        mInspectorateClockStartDate = Value
        '    End Set
        'End Property
        'Private mInspectorateClockStartDate As Object

        'Public Property JNCCClockStartDate() As Object Implements IBOPermitInfoClock.JNCCClockStartDate
        '    Get
        '        Return mJNCCClockStartDate
        '    End Get
        '    Set(ByVal Value As Object)
        '        mJNCCClockStartDate = Value
        '    End Set
        'End Property
        'Private mJNCCClockStartDate As Object

        'Public Property KewClockStartDate() As Object Implements IBOPermitInfoClock.KewClockStartDate
        '    Get
        '        Return mKewClockStartDate
        '    End Get
        '    Set(ByVal Value As Object)
        '        mKewClockStartDate = Value
        '    End Set
        'End Property
        'Private mKewClockStartDate As Object
#End Region

#Region " Operations "

        Public Shared Function BeforeIssued(ByVal status As BO.Application.BOPermitInfo.PermitStatusTypes) As Boolean
            Return (status <> BO.Application.BOPermitInfo.PermitStatusTypes.UpdatedSemiComplete _
              AndAlso status <> BO.Application.BOPermitInfo.PermitStatusTypes.ReturnedUsed AndAlso _
                         status <> BO.Application.BOPermitInfo.PermitStatusTypes.ReturnedUsed)
        End Function

        Public Shared Function ChangeStatus(ByVal permitinfoId As Int32(), _
            ByVal additionalinformation As BO.Application.AdditionalInformation, ByVal assignedToID As Int32, ByVal SSOUserId As Int64) As Boolean

            Dim ReturnStatus As Boolean

            If BeforeIssued(CType(additionalinformation.StatusId, BO.Application.BOPermitInfo.PermitStatusTypes)) Then
                'If we are dealing with permits before they have been issued then
                'we only recieve the first permitinfo id for all the multiple so we need to get 
                'the rest and add to the permitinfo array argument passed in to this method
                If permitinfoId.Length > 0 Then
                    Dim PIs As New ArrayList
                    Dim DOPermitInfo As [DO].DataObjects.Entity.PermitInfo
                    Dim DOPermit As [DO].DataObjects.Entity.Permit
                    Dim DOPermitInfoSet As [DO].DataObjects.EntitySet.PermitInfoSet

                    For Each PI As Int32 In permitinfoId
                        DOPermitInfo = New [DO].DataObjects.Entity.PermitInfo(PI)
                        DOPermit = New [DO].DataObjects.Entity.Permit(DOPermitInfo.PermitId, Nothing)
                        DOPermitInfoSet = DOPermit.GetRelatedPermitInfo
                        For Each PermitInfo As [DO].DataObjects.Entity.PermitInfo In DOPermitInfoSet.Entities
                            PIs.Add(PermitInfo.PermitInfoId)
                        Next
                    Next

                    permitinfoId = CType(PIs.ToArray(GetType(Int32)), Int32())
                Else
                    Return False
                End If
            End If

            If TypeOf additionalinformation Is BO.Application.AdditionalInformation_UC212_Issue Then    'MLD 2/3/5 modified
                Dim FullName As String = New BOAuthorisedUser(SSOUserId).FullName
                RPT.BOReportCommon.AuthorisePrintJob(CType(additionalinformation, BO.Application.AdditionalInformation_UC212_Authorisation).PrintJobId, FullName)
            End If

            'change the status
            ReturnStatus = BO.Application.BOPermitInfo.ChangeStatus(permitinfoId, assignedToID, SSOUserId, additionalinformation)

            Return ReturnStatus
        End Function

        Public Shared Function CheckStatusStillTheSame(ByVal permitInfos As Object) As Object()
            If Not permitInfos Is Nothing Then
                Dim ReturnPermitInfo As New ArrayList
                Dim NewPermitInfo As BO.Application.BOPermitInfo

                For Each PermitGridItem As BO.Application.BOPermit.ProgressPermitGrid In CType(permitInfos, System.Array)
                    NewPermitInfo = New BO.Application.BOPermitInfo(PermitGridItem.PermitInfoId)
                    If NewPermitInfo.PermitStatus.ID = PermitGridItem.StatusId Then
                        Dim DOPermit As New [DO].DataObjects.Entity.Permit(NewPermitInfo.PermitId, Nothing)
                        Dim DOCitesPermitSet As [DO].DataObjects.EntitySet.CITESPermitSet = DOPermit.GetRelatedCITESPermit
                        If Not DOCitesPermitSet Is Nothing AndAlso DOCitesPermitSet.Entities.Count > 0 Then
                            ReturnPermitInfo.Add(New BO.Application.BOPermit.ProgressPermitGrid(Nothing, ShowMultiple(CType(PermitGridItem.StatusId, BO.Application.BOPermitInfo.PermitStatusTypes)), NewPermitInfo, New BO.Application.CITES.BOCITESPermit(DOCitesPermitSet.Entities(0).CITESPermitId)))
                        End If
                    End If
                Next

                Return ReturnPermitInfo.ToArray
            End If
        End Function

        Public Shared Function GetAdditionalInformationObjectFromPermitId(ByVal permitid As Int32, ByVal actionId As Int32) As BO.Application.AdditionalInformation
            Dim PermitInfo As BO.Application.BOPermitInfo() = BO.Application.BOPermit.GetRelatedPermitInfo(permitid, Nothing)
                Dim AdditionalInfo As BO.Application.AdditionalInformation
                Dim ReturnAdditionalInfo As BO.Application.AdditionalInformation
                For Each Info As BO.Application.BOPermitInfo In PermitInfo
                    AdditionalInfo = GetAdditionalInformationObject(Info.PermitInfoId, actionId)
                    If ReturnAdditionalInfo Is Nothing OrElse ReturnAdditionalInfo.GetType.Equals(AdditionalInfo.GetType) Then
                        ReturnAdditionalInfo = AdditionalInfo
                    ElseIf Not ReturnAdditionalInfo.GetType.Equals(AdditionalInfo.GetType) Then
                        Return Nothing
                    End If
                Next

                Return ReturnAdditionalInfo
        End Function

        Private Shared Sub CreateAppObject(ByRef app As BO.Application.CITES.Applications.BOCITESApplication)
            Dim DOCitesApplication As New [DO].DataObjects.Entity.CITESApplication(app.CITESApplicationId)
            Dim DOArticle10Apps As [DO].DataObjects.EntitySet.Article10Set = DOCitesApplication.GetRelatedArticle10
            If Not DOArticle10Apps.Entities Is Nothing AndAlso DOArticle10Apps.Entities.Count > 0 Then
                app = New BO.Application.CITES.Applications.BOCITESArticle10(DOArticle10Apps.Entities(0).Article10Id)
            Else
                Dim DOExportApps As [DO].DataObjects.EntitySet.ExportApplicationSet = DOCitesApplication.GetRelatedExportApplication()
                If Not DOExportApps.Entities Is Nothing AndAlso DOExportApps.Entities.Count > 0 Then
                    app = New BO.Application.CITES.Applications.BOExportApplication(DOExportApps.Entities(0).ExportApplicationId)
                Else
                    Dim DOImportApps As [DO].DataObjects.EntitySet.ImportApplicationSet = DOCitesApplication.GetRelatedImportApplication()
                    If Not DOImportApps.Entities Is Nothing AndAlso DOImportApps.Entities.Count > 0 Then
                        app = New BO.Application.CITES.Applications.BOImportApplication(DOImportApps.Entities(0).ImportApplicationId)
                    End If
                    DOImportApps = Nothing
                End If
                DOExportApps = Nothing
            End If
            DOArticle10Apps = Nothing
        End Sub

        Public Shared Function ChangeStatusFromPermits(ByVal permits As BO.Application.BOPermit(), _
             ByVal additionalinformation As BO.Application.AdditionalInformation, ByVal SSOUserId As Int64) As Boolean

            Dim PermitInfoIds As New ArrayList
            Dim AssignedToID As Int32
            Dim PermitInfo As BO.Application.BOPermitInfo()

            For Each permit As BO.Application.BOPermit In permits
                PermitInfo = BO.Application.BOPermit.GetRelatedPermitInfo(permit.PermitId, Nothing)
                For Each item As BO.Application.BOPermitInfo In PermitInfo
                    AssignedToID = item.AssignedTo.ID
                    PermitInfoIds.Add(item.PermitInfoId)
                Next
            Next

            Return New BO.Application.BOPermitInfo(CType(PermitInfoIds(0), Int32)).ChangeStatus( _
                     CType(PermitInfoIds.ToArray(GetType(Int32)), Int32()), _
                    AssignedToID, SSOUserId, additionalinformation)
        End Function

        Public Shared Function GetGridPermitData(ByVal citesApplicationId As Int32) As BO.Application.BOPermit.ProgressPermitGrid()
            Dim CitesApplication As New BO.Application.cites.Applications.BOCITESApplication(citesApplicationId)
            CreateAppObject(CitesApplication)

            Dim ReturnData As New ArrayList
            CitesApplication.SetPermits(Nothing)
            For Each permit As BO.Application.CITES.BOCITESPermit In CitesApplication.Permit()
                Dim PermitInfos As BO.Application.BOPermitInfo() = permit.GetPermitInfos(Nothing)

                If ShowMultiple(CType(PermitInfos(0).PermitStatus.ID, BO.Application.BOPermitInfo.PermitStatusTypes)) Then
                    For Each permitinfo As BO.Application.BOPermitInfo In PermitInfos
                        ReturnData.Add(New BO.Application.BOPermit.ProgressPermitGrid(CitesApplication, True, permitinfo, permit))
                    Next
                Else
                    ReturnData.Add(New BO.Application.BOPermit.ProgressPermitGrid(CitesApplication, False, PermitInfos(0), permit))
                End If
            Next
            Return CType(ReturnData.ToArray(GetType(BO.Application.BOPermit.ProgressPermitGrid)), BO.Application.BOPermit.ProgressPermitGrid())
        End Function

        Private Shared Function ShowMultiple(ByVal status As BO.Application.BOPermitInfo.PermitStatusTypes) As Boolean
            Select Case status
                Case BO.Application.BOPermitInfo.PermitStatusTypes.Issued, _
                        BO.Application.BOPermitInfo.PermitStatusTypes.UpdatedSemiComplete, BO.Application.BOPermitInfo.PermitStatusTypes.ReturnedUsed, _
                        BO.Application.BOPermitInfo.PermitStatusTypes.ReturnedUsed
                    Return True
                Case Else
                    Return False
            End Select
        End Function

        Public Shared Function GetPermitInfoToFate(ByVal app As BO.Application.BOApplication) As BO.Application.BOPermit.ProgressPermitGrid()
            Dim PermitInfos As New ArrayList
            Dim GridData As New ArrayList

            For Each permit As BO.Application.BOPermit In app.Permit
                PermitInfos.AddRange(permit.GetPermitInfos(Nothing))
            Next

            For Each permitinfo As BO.Application.BOPermitInfo In PermitInfos
                If Not permitinfo.PermitStatus.ID = permitinfo.PermitStatusTypes.Issued Then
                    PermitInfos.Remove(permitinfo)
                Else
                    Dim DOPermit As New [DO].DataObjects.Entity.Permit(permitinfo.PermitId, Nothing)
                    Dim DOCitesPermit As [DO].DataObjects.Entity.CITESPermit = DOPermit.GetRelatedCITESPermit.Entities(0)

                    GridData.Add(New BO.Application.BOPermit.ProgressPermitGrid(Nothing, True, permitinfo, New BO.Application.CITES.BOCITESPermit(DOCitesPermit.CITESPermitId)))
                End If
            Next

            Return CType(GridData.ToArray(GetType(BO.Application.BOPermit.ProgressPermitGrid)), BO.Application.BOPermit.ProgressPermitGrid())
        End Function

        Public Shared Function GetPopulatedUsedInformationObject(ByVal permitInfoId As Int32) As BO.Application.AdditionalInformation_UC216_Used
            Dim AdditionalInformation As New AdditionalInformation_UC216_Used(PermitStatusTypes.ReturnedUsed)

            Dim ReturnedPermits As DataObjects.EntitySet.ReturnedPermitSet = [DO].DataObjects.Entity.ReturnedPermit.GetForPermitInfo(permitInfoId)
            If Not ReturnedPermits Is Nothing AndAlso ReturnedPermits.Entities.Count > 0 Then
                With AdditionalInformation
                    .ActualMass = ReturnedPermits.Entities(0).ActualMass
                    .ActualQuantity = ReturnedPermits.Entities(0).ActualQuantity
                    .AuthorisedBy = ReturnedPermits.Entities(0).AuthorisedUser
                    .AuthorisedDate = ReturnedPermits.Entities(0).AuthorisationDate
                    .BillOfLading = ReturnedPermits.Entities(0).BillOfLading
                    .CustomsDocumentReference = ReturnedPermits.Entities(0).CustomsDocumentReference
                    .CustomsDocumentType = ReturnedPermits.Entities(0).CustomsDocumentTypeId
                    .EndorsedPermitReceiptDate = ReturnedPermits.Entities(0).EndoresedUnusedPermitReceiptDate
                    .ImportExportDate = ReturnedPermits.Entities(0).ImportExportDate
                    .NumberDOA = ReturnedPermits.Entities(0).NumberDOA
                    '.PaymentStatus = ReturnedPermits.Entities(0).pa
                    .UOMId = ReturnedPermits.Entities(0).UOMId
                End With
                ReturnedPermits = Nothing
                'ReturnedPermits.Dispose()
            End If

            Return AdditionalInformation
        End Function

        'Public Shared Function RequestDuplicatePermit(ByVal authorisedUserId As Int64, ByVal permitInfoId() As Int32, ByVal duplicateReason As ReferenceData.BODuplicateReason, ByVal duplicateReasonDetails As String, ByVal reportprintjobid As Int32, ByVal coveringLetterReportId As Int32) As Boolean
        '    Return RequestDuplicatePermit(authorisedUserId, permitInfoId, duplicateReason.ID, duplicateReasonDetails, reportprintjobid, coveringLetterReportId)
        'End Function

        'Public Shared Function RequestDuplicatePermit(ByVal authorisedUserId As Int64, ByVal permitInfoId As Int32, ByVal duplicateReason As ReferenceData.BODuplicateReason, ByVal duplicateReasonDetails As String, ByVal reportprintjobid As Int32, ByVal coveringLetterReportId As Int32) As Boolean
        '    Return RequestDuplicatePermit(authorisedUserId, New Int32() {permitInfoId}, duplicateReason.ID, duplicateReasonDetails, reportprintjobid, coveringLetterReportId)
        'End Function

        'Public Shared Function RequestDuplicatePermit(ByVal authorisedUserId As Int64, ByVal permitInfoId() As Int32, ByVal duplicateReasonId As Int32, ByVal duplicateReasonDetails As String, ByVal reportprintjobid As Int32, ByVal coveringLetterReportId As Int32) As Boolean
        '    Dim Tran As SqlClient.SqlTransaction = DataObjects.Entity.PermitInfo.ServiceObject.BeginTransaction()
        '    For Each PermitInfo As Int32 In permitInfoId
        '        If Not RequestDuplicatePermit(authorisedUserId, PermitInfo, duplicateReasonId, duplicateReasonDetails, reportprintjobid, coveringLetterReportId, Tran) Then
        '            DataObjects.Entity.PermitInfo.ServiceObject.EndTransaction(Tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
        '            Return False
        '        End If
        '    Next PermitInfo
        '    DataObjects.Entity.PermitInfo.ServiceObject.EndTransaction(Tran, EnterpriseObjects.Service.TransactionEndEnum.Commit)
        '    Return True
        'End Function

        'Public Shared Function RequestDuplicatePermit(ByVal authorisedUserId As Int64, ByVal permitInfoId As Int32, ByVal duplicateReasonId As Int32, ByVal duplicateReasonDetails As String, ByVal reportprintjobid As Int32, ByVal coveringLetterReportId As Int32, ByVal tran As SqlClient.SqlTransaction) As Boolean
        '    'load the permitinfo
        '    Dim PI As New BOPermitInfo(permitInfoId, tran)
        '    If Not PI Is Nothing Then
        '        PI.Issue = PI.GetNextIssue()
        '        PI.CoveringLetterReportId = coveringLetterReportId
        '        Dim Result As BaseBO = PI.Save(authorisedUserId, tran)
        '        If Not Result Is Nothing AndAlso (Result.ValidationErrors Is Nothing OrElse Not Result.ValidationErrors.HasErrors) Then
        '            DataObjects.Entity.PermitDuplicateRequest.ServiceObject.Insert(permitInfoId, duplicateReasonId, duplicateReasonDetails, Date.Now, reportprintjobid, tran)
        '            If DataObjects.Sprocs.LastError Is Nothing Then
        '                Return True
        '            End If
        '        End If
        '    End If
        '    Return False
        'End Function

        <Serializable()> _
        Public Structure StatusThatCanBeChanged
            Public Sub New(ByVal saAdvice As Boolean, ByVal inspectorate As Boolean)
                Me.SAAdvice = saAdvice
                Me.Inspectorate = inspectorate
            End Sub
            Public SAAdvice As Boolean
            Public Inspectorate As Boolean
        End Structure

        Public Shared Function GetCancelPendingReasons(ByVal permitIds() As Int32) As String
            'altered because we are not worried about multiples when cancelling, so there will only be one reason per permit

            Dim Reasons As New Hashtable 'Collections.Specialized.StringCollection
            For Each Id As Int32 In permitIds
                'load the permitinfo
                Dim PI As New BOPermitInfo(Id)
                If Not PI.CancelPendingReason Is Nothing AndAlso _
                   PI.CancelPendingReason.ToString.Length > 0 AndAlso _
                   Not Reasons.ContainsKey(PI.PermitId) Then
                    Reasons.Add(PI.PermitId, String.Concat(New BOPermit(PI.PermitId).ApplicationPermitNumber, " - ", PI.CancelPendingReason.ToString))
                End If
            Next Id
            If Reasons.Count > 0 Then
                Dim sb As New Text.StringBuilder
                Dim i As Int32
                For Each Reason As DictionaryEntry In Reasons
                    sb.Append(Reason.Value)
                    If Reasons.Count > 1 AndAlso i <> Reasons.Count - 1 Then sb.Append("<br>")
                    i += 1
                Next Reason
                Return sb.ToString
            End If
            Return String.Empty
        End Function

        Public Shared Function GetNewErrorCorrectionStatus(ByVal currentstatus As Int32, ByVal permitInfoId As Int32) As ReferenceData.BOPermitStatus
            Select Case CType(currentstatus, PermitStatusTypes)
                Case PermitStatusTypes.Authorised
                    Return New ReferenceData.BOPermitStatus(PermitStatusTypes.ProgressAllowed)
                Case PermitStatusTypes.Cancelled
                    Return New ReferenceData.BOPermitStatus(GetFirstHistory(permitInfoId).PermitStatusId)
                Case PermitStatusTypes.UpdatedSemiComplete
                    Return New ReferenceData.BOPermitStatus(PermitStatusTypes.Issued)
                Case PermitStatusTypes.ReturnedUnused
                    Return New ReferenceData.BOPermitStatus(PermitStatusTypes.Issued)
                Case PermitStatusTypes.ReturnedUsed
                    Return New ReferenceData.BOPermitStatus(PermitStatusTypes.Issued)
            End Select
        End Function

        Public Function GetAdditionalInformationObject(ByVal newStatus As Int32) As AdditionalInformation
            Return GetAdditionalInformationObject(newStatus, Me.PermitInfoId)
        End Function

        Public Shared Function GetAdditionalInformationObject(ByVal newStatus As Int32, ByVal permitInfoId As Int32) As AdditionalInformation
            Dim PermitInfo As DataObjects.Entity.PermitInfo
            If permitInfoId > 0 Then
                PermitInfo = DataObjects.Entity.PermitInfo.GetById(permitInfoId)
                If Not PermitInfo Is Nothing AndAlso _
                   Not PermitInfo.IsPermitStatusIdNull Then
                    If CType(PermitInfo.PermitStatusId, PermitStatusTypes) = PermitStatusTypes.Referred Then
                        Select Case CType(PermitInfo.AssignedTo, Common.AssignedToList)
                            Case Common.AssignedToList.Inspectorate
                                Return New AdditionalInformation_UC222_Inspectorate(newStatus)
                            Case Common.AssignedToList.JNCC
                                Dim JNCC As New AdditionalInformation_UC222_JNCC(newStatus)
                                ' Dim Permit As New BOPermit(PermitInfo.PermitId)
                                'JNCC.Advice = Permit.JNCCAdvice
                                Return JNCC
                            Case Common.AssignedToList.Kew
                                Dim Kew As New AdditionalInformation_UC222_Kew(newStatus)
                                Dim Permit As New BOPermit(PermitInfo.PermitId)
                                '  Kew.Advice = Permit.KewAdvice
                                Return Kew
                            Case Common.AssignedToList.TeamLeader
                                Return New AdditionalInformation_UC222_TeamLeader(newStatus)
                        End Select
                    ElseIf CType(PermitInfo.PermitStatusId, PermitStatusTypes) = PermitStatusTypes.Authorised Then
                        'PN added
                        If CType(PermitInfo.AssignedTo, Common.AssignedToList) = Common.AssignedToList.CaseOfficer Then
                            If CType(newStatus, BOPermitInfo.PermitStatusTypes) = PermitStatusTypes.Issued Then
                                Return New AdditionalInformation_UC212_Issue(newStatus)
                            End If
                        End If
                    ElseIf CType(PermitInfo.PermitStatusId, PermitStatusTypes) = PermitStatusTypes.Issued Then
                        'PN added
                        If CType(PermitInfo.AssignedTo, Common.AssignedToList) = Common.AssignedToList.TeamLeader Then
                            If CType(newStatus, BOPermitInfo.PermitStatusTypes) = PermitStatusTypes.Re_Issue Then
                                Return New AdditionalInformation_UC222_ReIssue(newStatus)
                            End If
                        End If

                        If CType(PermitInfo.AssignedTo, Common.AssignedToList) = Common.AssignedToList.CaseOfficer Then
                            If CType(newStatus, BOPermitInfo.PermitStatusTypes) = PermitStatusTypes.Duplicate Then
                                Return New AdditionalInformation_UC222_Duplicate(newStatus)
                            End If
                            If CType(newStatus, BOPermitInfo.PermitStatusTypes) = PermitStatusTypes.ReturnedUsed Then
                                Return New AdditionalInformation_UC216_Used(newStatus)
                            End If
                            If CType(newStatus, BOPermitInfo.PermitStatusTypes) = PermitStatusTypes.ReturnedUnused Then
                                Return New AdditionalInformation_UC216_Unused(newStatus)
                            End If
                        End If
                    End If
                End If
            End If
            Select Case CType(newStatus, PermitStatusTypes)
                Case PermitStatusTypes.ProgressAllowed, PermitStatusTypes.Referred, _
                     PermitStatusTypes.IssuedDraft, PermitStatusTypes.UnderAppeal, PermitStatusTypes.Re_Issue
                    Return New AdditionalInformation_UC222(newStatus)
                Case PermitStatusTypes.Cancelled, PermitStatusTypes.CancelPending
                    Return New AdditionalInformation_UC220(newStatus)
                Case PermitStatusTypes.Authorised
                    Return New AdditionalInformation_UC212_Authorisation(newStatus)
                Case PermitStatusTypes.Refused
                    Return New AdditionalInformation_UC212_Rejection(newStatus)
                Case PermitStatusTypes.SubmittedByCustomer
                    Return New AdditionalInformation_SubmittedByCustomer(newStatus)
                Case PermitStatusTypes.ProgressAllowed
                    Return New AdditionalInformation_ProgressAllowed(newStatus)
                Case PermitStatusTypes.ReferredForCAndA, PermitStatusTypes.ReferredForSpecimenReport
                    Return New AdditionalInformation_UC222(newStatus)
                Case PermitStatusTypes.ErrorCorrection
                    If CType(newStatus, BOPermitInfo.PermitStatusTypes) = PermitStatusTypes.ErrorCorrection Then
                        Return New AdditionalInformation_UC222_ErrorCorrection(GetNewErrorCorrectionStatus(PermitInfo.PermitStatusId, PermitInfo.PermitInfoId))
                    End If
                Case PermitStatusTypes.UpdatedSemiComplete
                    Return New AdditionalInformation_UpdateSemiComplete(newStatus)
            End Select

            Return Nothing
        End Function

        Private Shared Sub AuditOnErrorCorrection(ByVal oldStatus As PermitStatusTypes, ByRef pi As BOPermitInfo)
            'TODO: PJN
            'This is where previously written data is either removed or archived
            'if fields of the permitinfo table need blanking then please change the argument as it is passed byref
            Select Case oldStatus
                Case PermitStatusTypes.Authorised
                    'permitinfo.dateauthorised
                    'pi.AuthorisationDate=nothing
                Case PermitStatusTypes.Cancelled
                    'permitinfo fields date cancelled, cancellation reason, cancel pending
                Case PermitStatusTypes.UpdatedSemiComplete
                    'some returnedpermit fields 
                Case PermitStatusTypes.ReturnedUnused
                    'returnedpermit row
                Case PermitStatusTypes.ReturnedUsed
                    'returnedpermit row
            End Select
        End Sub

        Public Shared Function ChangeStatus(ByVal permitInfoId() As Int32, ByVal assignedToId As Int32, ByVal authorisedUserId As Int64, ByVal additionalInformation As AdditionalInformation) As Boolean
            Dim service As New DataObjects.Service.PermitService
            Dim Tran As SqlClient.SqlTransaction = service.BeginTransaction
            Return ChangeStatus(permitInfoId, assignedToId, authorisedUserId, additionalInformation, Tran)
        End Function

        Public Shared Function ChangeStatus(ByVal permitInfoId() As Int32, ByVal assignedToId As Int32, ByVal authorisedUserId As Int64, ByVal additionalInformation As AdditionalInformation, ByVal tran As SqlClient.SqlTransaction) As Boolean
            Dim service As New DataObjects.Service.PermitService
            Dim NotesAdded As Hashtable
            For Each permitInfo As Int32 In permitInfoId
                If Not ChangeStatus(permitInfo, assignedToId, authorisedUserId, additionalInformation, tran) Then
                    service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    Return False
                Else
                    'changed ok
                End If
            Next permitInfo
            If Not additionalInformation.GetType.GetInterface(GetType(IAdditionalInformationNote).ToString) Is Nothing Then
                Dim AddInfo As IAdditionalInformationNote = CType(additionalInformation, IAdditionalInformationNote)
                If Not AddInfo.Note Is Nothing AndAlso _
                   AddInfo.Note.Length > 0 Then
                    'create an array of permits
                    Dim Permits As New ArrayList
                    Dim ApplicationId As Int32
                    For Each permitInfo As Int32 In permitInfoId
                        Dim PI As New BOPermitInfo(permitInfo, tran)
                        If Not Permits.Contains(PI.PermitId) Then
                            If ApplicationId = 0 Then
                                Dim Permit As New BOPermit(PI.PermitId, tran)
                                If Not Permit Is Nothing Then
                                    ApplicationId = Permit.ApplicationId
                                End If
                            End If
                            Permits.Add(PI.PermitId)
                        End If
                    Next permitinfo

                    'create a note
                    If ApplicationId = 0 OrElse Permits.Count = 0 Then
                        service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                        Return False
                    End If

                    Dim NewApplicationNote As New BO.Application.CITES.Applications.ApplicationNote
                    With NewApplicationNote
                        .Content = AddInfo.Note
                        .CreatedDate = Date.Now
                        .CreatedById = authorisedUserId
                        .Important = False
                        .Active = True
                        .NoteDate = Date.Now
                        Dim Status As New DataObjects.Entity.PermitStatus(additionalInformation.StatusId)
                        If CType(additionalInformation.StatusId, PermitStatusTypes) = PermitStatusTypes.Referred Then
                            .Subject = String.Concat(Status.Description, " To ", New ReferenceData.BOStatusAssignedToGroup(assignedToId).Description)
                        Else
                            .Subject = String.Concat(Status.Description, " Note")
                        End If
                        .OtherId = ApplicationId
                        Dim NewNote As BOCommon.BaseNote = .Save(tran)
                        If NewNote Is Nothing Then
                            service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                            Return False
                        Else
                            If Not BOApplication.SavePermitNotes(False, CType(Permits.ToArray(GetType(Int32)), Int32()), ApplicationId, NewNote.NoteId, tran) Then
                                'no need to rollback as that is done for us.
                                Return False
                            End If
                        End If
                    End With
                End If
            End If
            service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Commit)
            Return True
        End Function

        Public Shared Function ChangeStatus(ByVal permitInfoId As Int32, ByVal assignedToId As Int32, ByVal authorisedUserId As Int64, ByVal additionalInformation As AdditionalInformation) As Boolean
            Return ChangeStatus(permitInfoId, assignedToId, authorisedUserId, additionalInformation, Nothing)
        End Function

        Public Shared Function ChangeStatus(ByVal permitInfoId As Int32, ByVal assignedToId As Int32, ByVal authorisedUserId As Int64, ByVal additionalInformation As AdditionalInformation, ByVal tran As SqlClient.SqlTransaction) As Boolean
            'load the permit info
            'assignedToId is the id of the group which this is now being assigned to.
            'authorisedUserId is the id of the user doing it.
            'Dim SSOUserId As Int64 = GetSSOUserId(authorisedUserId)
            Dim PI As New BOPermitInfo(permitInfoId, tran)
            Dim OldPermitStatus As PermitStatusTypes

            Dim SkipCheck As Boolean
            If Not PI.PermitStatusId Is Nothing Then
                'added to allow status changes that are not shown on screen through
                SkipCheck = (TypeOf additionalInformation Is AdditionalInformation_UC222_ErrorCorrection) OrElse _
                    (CType(PI.PermitStatusId, Int32) = PermitStatusTypes.CancelPending AndAlso additionalInformation.StatusId = PermitStatusTypes.ProgressAllowed) OrElse _
                    (CType(PI.PermitStatusId, Int32) = PermitStatusTypes.BeingInput_CaseOfficer AndAlso additionalInformation.StatusId = PermitStatusTypes.ProgressAllowed) OrElse _
                    (CType(PI.PermitStatusId, Int32) = PermitStatusTypes.BeingInput_Customer AndAlso additionalInformation.StatusId = PermitStatusTypes.SubmittedByCustomer)

                OldPermitStatus = CType(PI.PermitStatusId, PermitStatusTypes)
            End If

            If (Not PI.PermitStatusId Is Nothing AndAlso (SkipCheck OrElse DoesListContainStatus(GetStatusList(False, authorisedUserId, CType(PI.PermitStatusId, Int32), CType(PI.AssignedToId, Common.AssignedToList), PI.PermitInfoId), additionalInformation.StatusId))) Then
                Dim SaveRecord As Boolean = True

                PI.SetPermitStatusId = additionalInformation.StatusId
                If assignedToId > 0 Then
                    PI.SetAssignedToId = assignedToId
                End If

                If TypeOf additionalInformation Is AdditionalInformation_UC222_ErrorCorrection Then
                    AuditOnErrorCorrection(OldPermitStatus, PI)
                ElseIf TypeOf additionalInformation Is AdditionalInformation_UC222 Then
                    PI.NextActionDate = CType(additionalInformation, AdditionalInformation_UC222).NextActionDate
                ElseIf TypeOf additionalInformation Is AdditionalInformation_UC222_Inspectorate Then
                    If CType(additionalInformation, AdditionalInformation_UC222_Inspectorate).ProgressStatusInspectionId > 0 Then
                        PI.ProgressStatusInspection = New Application.ProgressStatus.BOProgressStatusInspection(CType(additionalInformation, AdditionalInformation_UC222_Inspectorate).ProgressStatusInspectionId, tran)
                    End If
                ElseIf TypeOf additionalInformation Is AdditionalInformation_UC222_Kew Then
                    If CType(additionalInformation, AdditionalInformation_UC222_Kew).ProgressStatusSAAdviceId > 0 Then
                        PI.ProgressStatusSAAdvice = New Application.ProgressStatus.BOProgressStatusSAAdvice(CType(additionalInformation, AdditionalInformation_UC222_Kew).ProgressStatusSAAdviceId, tran)
                    End If
                    'write back the notes
                    'Dim Permit As New BOPermit(PI.PermitId, tran)
                    'permit.KewAdvice = CType(additionalInformation, AdditionalInformation_UC222_Kew).Advice
                    'Dim VM As ValidationManager = permit.Save(tran).ValidationErrors
                    'If Not VM Is Nothing AndAlso VM.HasErrors Then
                    '    Return False
                    'End If
                ElseIf TypeOf additionalInformation Is AdditionalInformation_UC222_JNCC Then
                    If CType(additionalInformation, AdditionalInformation_UC222_JNCC).ProgressStatusSAAdviceId > 0 Then
                        PI.ProgressStatusSAAdvice = New Application.ProgressStatus.BOProgressStatusSAAdvice(CType(additionalInformation, AdditionalInformation_UC222_JNCC).ProgressStatusSAAdviceId, tran)
                    End If
                    'write back the notes
                    'Dim Permit As New BOPermit(PI.PermitId, tran)
                    '' permit.JNCCAdvice = CType(additionalInformation, AdditionalInformation_UC222_JNCC).Advice
                    'Dim VM As ValidationManager = permit.Save(tran).ValidationErrors
                    'If Not VM Is Nothing AndAlso VM.HasErrors Then
                    '    Return False
                    'End If
                ElseIf TypeOf additionalInformation Is AdditionalInformation_UC220 Then
                    If additionalInformation.StatusId = PermitStatusTypes.ProgressAllowed AndAlso OldPermitStatus = PermitStatusTypes.CancelPending Then
                        PI.CancelPendingDeclineReason = CType(additionalInformation, AdditionalInformation_UC220).Reason
                    ElseIf additionalInformation.StatusId = PermitStatusTypes.CancelPending Then
                        PI.CancelPendingReason = CType(additionalInformation, AdditionalInformation_UC220).Reason
                    Else
                        PI.CancelReason = CType(additionalInformation, AdditionalInformation_UC220).Reason
                    End If
                ElseIf TypeOf additionalInformation Is AdditionalInformation_UC212_Rejection Then
                    With CType(additionalInformation, AdditionalInformation_UC212_Rejection).RejectionReason
                        If .ReasonId > 0 Then
                            PI.RefusalReason = New ReferenceData.BORefusalReason(.ReasonId, tran)
                        End If
                        If Not .Reason Is Nothing AndAlso _
                                .Reason.Length > 0 Then
                            PI.RefusalReasonAdHoc = .Reason
                        End If
                        PI.DateRefused = Date.Now
                        PI.CoveringLetterReportId = CType(additionalInformation, AdditionalInformation_UC212_Rejection).LetterReportId
                    End With
                ElseIf TypeOf additionalInformation Is AdditionalInformation_UC222_Duplicate OrElse TypeOf additionalInformation Is AdditionalInformation_UC212_Issue OrElse TypeOf additionalInformation Is AdditionalInformation_UC222_ReIssue Then
                    'PN added 
                    With PI
                        .CoveringLetterReportId = CType(additionalInformation, AdditionalInformation_UC212_Issue).LetterReportId
                        .PrintJobId = CType(additionalInformation, AdditionalInformation_UC212_Issue).PrintJobId
                        .IssueDate = Date.Now
                        .Issue = GetNextIssue(PI)
                        .IssuedBy = New BOAuthorisedUser(authorisedUserId)
                        .PlaceOfIssue = CType(additionalInformation, AdditionalInformation_UC212_Issue).MaAddress
                        'when a permit is reissued duplicated we want to leave the status at issued
                        .SetPermitStatusId = CType(PermitStatusTypes.Issued, Int32)
                    End With
                    If TypeOf additionalInformation Is AdditionalInformation_UC222_Duplicate Then
                        With CType(additionalInformation, AdditionalInformation_UC222_Duplicate)
                            DataObjects.Entity.PermitDuplicateRequest.ServiceObject.Insert(permitInfoId, .DuplicateReasonId, .DuplicateReasonDetails, CType(PI.IssueDate, Date), .PrintJobId, tran)
                        End With
                        If Not DataObjects.Sprocs.LastError Is Nothing Then
                            Return False
                        End If
                    End If
                ElseIf TypeOf additionalInformation Is AdditionalInformation_UC212_Authorisation Then
                    PI.AuthorisationDate = Date.Now
                    'Needed PI.AuthorisedBy

                    'PI.Issue = GetNextIssue(PI)
                    'PI.IssuedBy = New BOAuthorisedUser(authorisedUserId)
                    'Dim Permit As New BOPermit(PI.PermitId)
                    'If Not Permit Is Nothing Then
                    '    Dim App As New Application.BOApplication(Permit.ApplicationId)
                    '    If Not App Is Nothing Then
                    '        Dim CITESApp As New Application.CITES.Applications.BOCITESApplication
                    '        CITESApp.InitialiseCITESApplicationByApplicationId(Permit.ApplicationId)
                    '        Dim MA As BOApplicationPartyDetails = CITESApp.GetManagementAuthority()
                    '        If Not MA Is Nothing Then
                    '            PI.PlaceOfIssue = MA.Address.Town
                    '        End If
                    '    End If
                    'End If
                ElseIf TypeOf additionalInformation Is AdditionalInformation_SubmittedByCustomer OrElse _
                       TypeOf additionalInformation Is AdditionalInformation_ProgressAllowed Then
                    'do nothing as these have no args
                ElseIf TypeOf additionalInformation Is AdditionalInformation_UpdateSemiComplete Then
                    With CType(additionalInformation, AdditionalInformation_UpdateSemiComplete)
                        PI.SemiCompleteCountryId = .PI.SemiCompleteCountryId
                        PI.SemiCompleteSecondPartyId = .PI.SemiCompleteSecondPartyId
                        PI.SemiCompleteSpecie = .PI.SemiCompleteSpecie
                        PI.SemiCompleteSpecieId = .PI.SemiCompleteSpecieId
                        PI.SemiCompleteSpecimen = .PI.SemiCompleteSpecimen
                        PI.SemiCompleteSpecimenId = .PI.SemiCompleteSpecimenId
                        PI.SemiCompleteUOMId = .PI.SemiCompleteUOMId
                    End With
                ElseIf TypeOf additionalInformation Is AdditionalInformation_UC216_Used Then
                    'check to see if a retun record exists
                    Dim ReturnRec As DataObjects.EntitySet.ReturnedPermitSet = DataObjects.Entity.ReturnedPermit.GetForPermitInfo(permitInfoId, tran)
                    With CType(additionalInformation, AdditionalInformation_UC216_Used)
                        If Not ReturnRec Is Nothing AndAlso ReturnRec.Count > 0 Then
                            Dim ReturnedPermitId As Int32 = CType(ReturnRec.GetEntity(0), DataObjects.Entity.ReturnedPermit).ReturnedPermitId
                            DataObjects.Entity.ReturnedPermit.ServiceObject.Update(ReturnedPermitId, permitInfoId, Date.Now, .EndorsedPermitReceiptDate, .BillOfLading, .CustomsDocumentType, .CustomsDocumentReference, .ImportExportDate, .ActualQuantity, .ActualMass, .NumberDOA, .AuthorisedBy, .AuthorisedDate, Nothing, Nothing, .UOMId, 0, tran)
                        Else
                            'it doesn't exist, so create one
                            DataObjects.Entity.ReturnedPermit.ServiceObject.Insert(permitInfoId, Date.Now, .EndorsedPermitReceiptDate, .BillOfLading, .CustomsDocumentType, .CustomsDocumentReference, .ImportExportDate, .ActualQuantity, .ActualMass, .NumberDOA, .AuthorisedBy, .AuthorisedDate, Nothing, Nothing, .UOMId, tran)
                        End If
                    End With
                    'SaveRecord = False
                ElseIf TypeOf additionalInformation Is AdditionalInformation_UC216_Unused Then
                    'check to see if a retun record exists
                    Dim ReturnRec As DataObjects.EntitySet.ReturnedPermitSet = DataObjects.Entity.ReturnedPermit.GetForPermitInfo(permitInfoId, tran)
                    If Not ReturnRec Is Nothing AndAlso ReturnRec.Count > 0 Then
                        Dim ReturnedPermitId As Int32 = CType(ReturnRec.GetEntity(0), DataObjects.Entity.ReturnedPermit).ReturnedPermitId
                        DataObjects.Entity.ReturnedPermit.ServiceObject.Update(ReturnedPermitId, permitInfoId, Nothing, Date.Now, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, tran)
                    Else
                        'it doesn't exist, so create one
                        DataObjects.Entity.ReturnedPermit.ServiceObject.Insert(permitInfoId, Nothing, Date.Now, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, tran)
                    End If
                    'SaveRecord = False
                End If
                If SaveRecord Then
                    Dim NewPI As BaseBO = PI.Save(CType(authorisedUserId, Int64), tran)
                    If NewPI.ValidationErrors Is Nothing Then
                        Return True
                    Else
                        Return NewPI.ValidationErrors.HasErrors
                    End If
                Else
                    Return True
                End If
            Else
                'it's been changed before, so all is ok still
                Return True
            End If
        End Function

        'Private Shared Sub DoAuthorisationReport(ByVal printJobId As Int32)
        'End Sub

        Public Function GetStatusThatCanBeChanged(ByVal ssoUserid As Int64) As StatusThatCanBeChanged
            Dim SA As Boolean = ((CType(mPermitStatus.ID, PermitStatusTypes) = PermitStatusTypes.Referred AndAlso _
               (mAssignedTo.AssignedTo = Common.AssignedToList.JNCC AndAlso _
               Common.IsInRole(ssoUserid, Common.RolesList.JNCC)) OrElse _
               (mAssignedTo.AssignedTo = Common.AssignedToList.Kew AndAlso _
               Common.IsInRole(ssoUserid, Common.RolesList.Kew)))) OrElse _
               ((CType(mPermitStatus.ID, PermitStatusTypes) = PermitStatusTypes.CancelPending AndAlso _
               (mAssignedTo.AssignedTo = Common.AssignedToList.JNCC AndAlso _
               Common.IsInRole(ssoUserid, Common.RolesList.JNCC)) OrElse _
               (mAssignedTo.AssignedTo = Common.AssignedToList.Kew AndAlso _
               Common.IsInRole(ssoUserid, Common.RolesList.Kew))))
            Dim Insp As Boolean = (CType(mPermitStatus.ID, PermitStatusTypes) = PermitStatusTypes.Referred OrElse _
               CType(mPermitStatus.ID, PermitStatusTypes) = PermitStatusTypes.CancelPending) AndAlso _
               mAssignedTo.AssignedTo = Common.AssignedToList.Inspectorate AndAlso _
               Common.IsInRole(ssoUserid, Common.RolesList.Inspectorate)
            Return New StatusThatCanBeChanged(SA, Insp)
        End Function

        'Private Sub DoClockWork(ByVal oldPermitInfo As BOPermitInfo, ByVal tran As SqlClient.SqlTransaction)
        '    Dim ThisPermit As DataObjects.Entity.Permit = DataObjects.Entity.Permit.GetById(mPermitId, tran)
        '    If Not ThisPermit Is Nothing Then
        '        Dim ThisApp As DataObjects.Entity.Application = DataObjects.Entity.Application.GetById(ThisPermit.ApplicationId, tran)
        '        If Not ThisApp Is Nothing Then
        '            Dim CurrentPermitStatus As PermitStatusTypes = CType(oldPermitInfo.PermitStatus.ID, PermitStatusTypes)
        '            Dim NewPermitStatus As PermitStatusTypes = CType(mPermitStatus.ID, PermitStatusTypes)
        '            ' start clock actions
        '            Dim FoundGWDClockSet As Boolean = False
        '            Dim FoundJNCCClockSet As Boolean = False
        '            Dim FoundKewClockSet As Boolean = False
        '            Dim FoundInspectorateClockSet As Boolean = False

        '            If NewPermitStatus = PermitStatusTypes.BeingInput_CaseOfficer AndAlso _
        '               CurrentPermitStatus <> PermitStatusTypes.BeingInput_CaseOfficer Then
        '                FoundGWDClockSet = True
        '                If mGWDClockStartDate Is Nothing Then mGWDClockStartDate = ThisApp.RecievedDate
        '            ElseIf NewPermitStatus = PermitStatusTypes.SubmittedByCustomer AndAlso _
        '               CurrentPermitStatus <> PermitStatusTypes.SubmittedByCustomer Then
        '                FoundGWDClockSet = True
        '                If mGWDClockStartDate Is Nothing Then mGWDClockStartDate = ThisApp.CreatedDate
        '            ElseIf ((NewPermitStatus = PermitStatusTypes.ProgressAllowed AndAlso _
        '               CurrentPermitStatus <> PermitStatusTypes.ProgressAllowed) OrElse _
        '               (NewPermitStatus = PermitStatusTypes.Referred AndAlso _
        '               CurrentPermitStatus = PermitStatusTypes.UnderAppeal AndAlso _
        '               mAssignedTo.AssignedTo <> Common.AssignedToList.Customer) OrElse _
        '               (NewPermitStatus = PermitStatusTypes.IssuedDraft AndAlso _
        '               CurrentPermitStatus <> PermitStatusTypes.IssuedDraft) OrElse _
        '               (NewPermitStatus = PermitStatusTypes.CancelPending AndAlso _
        '               CurrentPermitStatus = PermitStatusTypes.UnderAppeal AndAlso _
        '               mAssignedTo.AssignedTo <> Common.AssignedToList.Customer)) Then
        '                FoundGWDClockSet = True
        '                If mGWDClockStartDate Is Nothing Then mGWDClockStartDate = Date.Now
        '            ElseIf NewPermitStatus = PermitStatusTypes.Referred AndAlso _
        '               CurrentPermitStatus <> PermitStatusTypes.Referred AndAlso _
        '               mAssignedTo.AssignedTo = Common.AssignedToList.JNCC Then
        '                FoundJNCCClockSet = True
        '                If mJNCCClockStartDate Is Nothing Then mJNCCClockStartDate = Date.Now
        '            ElseIf NewPermitStatus = PermitStatusTypes.Referred AndAlso _
        '               CurrentPermitStatus <> PermitStatusTypes.Referred AndAlso _
        '               mAssignedTo.AssignedTo = Common.AssignedToList.Kew Then
        '                FoundKewClockSet = True
        '                If mKewClockStartDate Is Nothing Then mKewClockStartDate = Date.Now
        '            ElseIf NewPermitStatus = PermitStatusTypes.Referred AndAlso _
        '               CurrentPermitStatus <> PermitStatusTypes.Referred AndAlso _
        '               mAssignedTo.AssignedTo = Common.AssignedToList.Inspectorate Then
        '                FoundInspectorateClockSet = True
        '                If mInspectorateClockStartDate Is Nothing Then mInspectorateClockStartDate = Date.Now
        '            End If
        '            ' stop clock actions

        '            ' only if the status has changed
        '            If NewPermitStatus <> CurrentPermitStatus Then
        '                If Not FoundGWDClockSet AndAlso _
        '                   Not mGWDClockStartDate Is Nothing Then
        '                    'we've not set the GWD clock, but is was set before

        '                    '...so get the number of hours elapsed and clear the clock start date
        '                    mGWDClock += Date.op_Subtraction(Date.Now, CType(mGWDClockStartDate, Date)).Minutes
        '                    mGWDClockStartDate = Nothing
        '                ElseIf Not FoundJNCCClockSet AndAlso _
        '                   Not mJNCCClockStartDate Is Nothing Then
        '                    'we've not set the JNCC clock, but is was set before

        '                    '...so get the number of hours elapsed and clear the clock start date
        '                    mJNCCClock += Date.op_Subtraction(Date.Now, CType(mJNCCClockStartDate, Date)).Minutes
        '                    mJNCCClockStartDate = Nothing
        '                ElseIf Not FoundKewClockSet AndAlso _
        '                   Not mKewClockStartDate Is Nothing Then
        '                    'we've not set the Kew clock, but is was set before

        '                    '...so get the number of hours elapsed and clear the clock start date
        '                    mKewClock += Date.op_Subtraction(Date.Now, CType(mKewClockStartDate, Date)).Minutes
        '                    mKewClockStartDate = Nothing
        '                ElseIf Not FoundInspectorateClockSet AndAlso _
        '                   Not mInspectorateClockStartDate Is Nothing Then
        '                    'we've not set the Inspectorate clock, but is was set before

        '                    '...so get the number of hours elapsed and clear the clock start date
        '                    mInspectorateClock += Date.op_Subtraction(Date.Now, CType(mInspectorateClockStartDate, Date)).Minutes
        '                    mInspectorateClockStartDate = Nothing
        '                End If
        '            End If
        '        End If
        '    End If
        'End Sub

        Friend Shared Function CreateFirstPermitInfo(ByVal permit As [DO].DataObjects.Entity.Permit, ByVal customer As Boolean, ByVal createdById As Int64, ByVal tran As SqlClient.SqlTransaction) As Boolean
            Dim PermitInfoService As New [DO].DataObjects.Service.PermitInfoService

            'delete any existing info objects
            Dim PIs As [DO].DataObjects.EntitySet.PermitInfoSet = PermitInfoService.GetByIndex_PermitId(permit.PermitId)
            If Not PIs Is Nothing AndAlso PIs.Count > 0 Then
                For Each PI As [DO].DataObjects.Entity.PermitInfo In PIs
                    If Not PI.DeleteById(PI.PermitInfoId, tran) Then
                        'didn't delete, so bail
                        Return False
                    End If
                Next PI
            Else
                'Return True
            End If
            'all are deleted ok, so add the new ones

            Dim EndLoop As Int32 = 1

            If Not permit.IsNumberOfCopiesNull AndAlso permit.NumberOfCopies > 1 Then
                EndLoop = permit.NumberOfCopies
            End If
            'Dim PermitInfoHistory As New BOPermitHistory
            'With PermitInfoHistory
            '.ChangeDate = Date.Now
            '.ChangedByUser = New BOAuthorisedUser(createdById)
            '.PermitId = permitId
            Dim AssignedId As Int32
            Dim StatusId As Int32
            Dim DOPermit As DataObjects.Entity.PermitInfo
            Dim ChangeDate As Date = Date.Now

            For i As Int32 = 1 To EndLoop
                If Not customer Then
                    AssignedId = Common.AssignedToList.CaseOfficer
                    StatusId = BOPermitInfo.PermitStatusTypes.BeingInput_CaseOfficer
                    DOPermit = PermitInfoService.Insert(permit.PermitId, Nothing, Nothing, _
                                             Nothing, Nothing, Nothing, _
                                              StatusId, 0, _
                                              AssignedId, ChangeDate, _
                                                createdById, _
                                               ChangeDate, Nothing, Nothing, i, Nothing, _
                                             Nothing, Nothing, Nothing, _
                                             Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, tran)       'MLD 17/11/4 extra nothing added
                Else
                    AssignedId = Common.AssignedToList.Customer
                    StatusId = BOPermitInfo.PermitStatusTypes.BeingInput_Customer
                    DOPermit = PermitInfoService.Insert(permit.PermitId, Nothing, Nothing, _
                                             Nothing, Nothing, Nothing, _
                                             StatusId, 0, _
                                             AssignedId, Date.Now, _
                                             createdById, _
                                             Nothing, Nothing, i, Nothing, _
                                             Nothing, Nothing, Nothing, Nothing, _
                                             Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, tran)   'MLD 17/11/4 extra nothing added
                End If
                '.AssignedTo = New ReferenceData.BOStatusAssignedToGroup(AssignedId)
                '.PermitStatus = New ReferenceData.BOPermitStatus(StatusId)
                If DOPermit Is Nothing Then
                    If Not tran Is Nothing Then PermitInfoService.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    Return False
                    'Dim NewHistory As BaseBO = .Save(tran)
                    'If Not NewHistory.ValidationErrors Is Nothing AndAlso _
                    '   Not NewHistory.ValidationErrors.HasErrors Then
                    'End If
                End If
            Next i
            'End With
            Return True
        End Function

        'Public Shared Function SetIssued(ByVal permitInfoIds() As Int32) As Boolean
        '    Dim service As New DataObjects.Service.PermitService
        '    Dim Tran As SqlClient.SqlTransaction = service.BeginTransaction
        '    Dim CommitTrans As EnterpriseObjects.Service.TransactionEndEnum = EnterpriseObjects.Service.TransactionEndEnum.Commit
        '    If Not permitInfoIds Is Nothing AndAlso permitInfoIds.Length >= 0 Then
        '        For Each InfoId As Int32 In permitInfoIds
        '            Dim PI As DataObjects.Entity.PermitInfo = DataObjects.Entity.PermitInfo.GetById(InfoId)
        '            If PI Is Nothing Then
        '                CommitTrans = EnterpriseObjects.Service.TransactionEndEnum.Rollback
        '                Exit For
        '            End If
        '            Dim PermitHistory As DataObjects.Entity.PermitHistory = BOPermitHistory.GetLastPermitHistory(PI.PermitInfoId, Tran)
        '            If PermitHistory Is Nothing OrElse Not ChangeStatus("", InfoId, PI.AssignedTo, CType(PermitHistory.ChangedByUserId, Int64), New AdditionalInformation_Default(PermitStatusTypes.Issued), Tran) Then
        '                CommitTrans = EnterpriseObjects.Service.TransactionEndEnum.Rollback
        '                Exit For
        '            End If
        '        Next InfoId
        '        Try
        '            service.EndTransaction(Tran, CommitTrans)
        '        Catch
        '        End Try
        '    End If
        '    Return (CommitTrans = EnterpriseObjects.Service.TransactionEndEnum.Commit)
        'End Function

        'Private Function GetLastDateForAGivenStatus(ByVal findStatus As PermitStatusTypes, ByVal app As DataObjects.Entity.Application, ByVal tran As SqlClient.SqlTransaction) As Object
        '    Dim HistoryItems As DataObjects.Collection.PermitHistoryBoundCollection = GetHistory(tran)
        '    If Not HistoryItems Is Nothing AndAlso HistoryItems.Count > 0 Then
        '        Dim FoundStatus As Boolean = False
        '        For Each HistoryItem As DataObjects.Entity.PermitHistory In HistoryItems
        '            If Not HistoryItem.IsPermitStatusIdNull Then
        '                If FoundStatus Then
        '                    Return HistoryItem.ChangeDate
        '                ElseIf CType(HistoryItem.PermitHistoryId, PermitStatusTypes) = findStatus Then
        '                    'now we've found the status, we need to find the next status change as that would have been the date
        '                    'that the record was changed to that status
        '                    FoundStatus = True
        '                End If
        '            End If
        '        Next HistoryItem
        '        If FoundStatus Then
        '            'we found the status we were after, but nothing previous to it, so we should use the
        '            'permits creation date
        '            Return app.CreatedDate
        '        End If
        '    End If
        '    Return Nothing
        'End Function

        <Serializable()> _
        Public Class StatusList

            Public Sub New()
            End Sub

            Public Sub New(ByVal id As Int32, ByVal description As String, ByVal action As String)  'MLD altered 29/10/4
                mId = id
                mDescription = description
                mAction = action
            End Sub

            Public Property Description() As String
                Get
                    Return mDescription
                End Get
                Set(ByVal Value As String)
                    mDescription = Value
                End Set
            End Property
            Private mDescription As String

            Public Property Action() As String  'MLD added 29/10/4
                Get
                    Return mAction
                End Get
                Set(ByVal Value As String)
                    mAction = Value
                End Set
            End Property
            Private mAction As String

            Public Property Id() As Int32
                Get
                    Return mId
                End Get
                Set(ByVal Value As Int32)
                    mId = Value
                End Set
            End Property
            Private mId As Int32
        End Class

        Public Shared Function CanProgressPermits(ByVal permitId As Int32) As Boolean
            Return True
        End Function

        Public Shared Function CanProgressPermits(ByVal permitIds() As Int32) As Boolean
            Dim StatusId As Int32 = 0
            Dim AssignedToId As Int32 = 0
            Dim SAAdviceId As Int32 = 0
            Dim InspId As Int32 = 0
            For Each permitId As Int32 In permitIds
                Dim Permit As New BO.Application.BOPermitInfo(permitId)
                If (StatusId > 0 AndAlso Not Permit.PermitStatusId Is Nothing AndAlso CType(Permit.PermitStatusId, Int32) <> StatusId) Then
                    Return False
                Else
                    StatusId = CType(Permit.PermitStatusId, Int32)
                End If
                If (AssignedToId > 0 AndAlso Permit.AssignedToId <> AssignedToId) Then
                    Return False
                Else
                    AssignedToId = Permit.AssignedToId
                End If
                If (SAAdviceId > 0 AndAlso Not Permit.ProgressStatusSAAdviceId Is Nothing AndAlso CType(Permit.ProgressStatusSAAdviceId, Int32) <> SAAdviceId) Then
                    Return False
                Else
                    SAAdviceId = CType(Permit.ProgressStatusSAAdviceId, Int32)
                End If
                If (InspId > 0 AndAlso Not Permit.ProgressStatusInspectionId Is Nothing AndAlso CType(Permit.ProgressStatusInspectionId, Int32) <> InspId) Then
                    Return False
                Else
                    InspId = CType(Permit.ProgressStatusInspectionId, Int32)
                End If
            Next
            Return True
        End Function

        Private Shared Function DoesListContainStatus(ByVal list As StatusList(), ByVal status As Int32) As Boolean
            Return DoesListContainStatus(list, CType(status, PermitStatusTypes))
        End Function

        Private Shared Function DoesListContainStatus(ByVal list As StatusList(), ByVal status As PermitStatusTypes) As Boolean
            If list Is Nothing OrElse list.Length = 0 Then
                Return False
            Else
                For Each StatusItem As StatusList In list
                    If StatusItem.Id = status Then
                        Return True
                    End If
                Next StatusItem
            End If
            Return False
        End Function

        Public Function GetStatusList(ByVal moreThanOneSelected As Boolean, ByVal authorisedUserId As Int64) As StatusList()
            If Not mPermitStatus Is Nothing AndAlso _
               mPermitStatus.ID > 0 Then
                Return GetStatusList(moreThanOneSelected, authorisedUserId, mPermitStatus.ID, mAssignedTo.AssignedTo, mPermitInfoId)
            Else
                Return Nothing
            End If
        End Function

        'Private Shared Function GetStatusList(ByVal moreThanOneSelected As Boolean, ByVal appId As Int32, ByVal authorisedUserId As Int64, ByVal permitStatusId As Int32, ByVal assignedTo As Common.AssignedToList, ByVal permitId As Int32) As StatusList()
        '    Return GetStatusList(moreThanOneSelected, appId, GetSSOUserId(authorisedUserId), permitStatusId, assignedTo, permitId)
        'End Function

        Private Shared Function IsImportApplication(ByVal permitInfoId As Int32) As Boolean
            Dim App As New [DO].DataObjects.Entity.Application(GetAppIdFromPermitInfoId(permitInfoId))
            Dim CitesAppSet As [DO].DataObjects.EntitySet.CITESApplicationSet = App.GetRelatedCITESApplication()
            If Not CitesAppSet Is Nothing AndAlso CitesAppSet.Entities.Count > 0 Then
                Dim ImportSet As [DO].DataObjects.EntitySet.ImportApplicationSet = CitesAppSet.Entities(0).GetRelatedImportApplication
                Return (Not ImportSet Is Nothing AndAlso ImportSet.Entities.Count > 0)
            End If
        End Function

        Private Shared Function IsCompositeApplication(ByVal permitinfoid As Int32) As Boolean
            Dim App As New [DO].DataObjects.Entity.Application(GetAppIdFromPermitInfoId(permitinfoid))
            Dim CitesAppSet As [DO].DataObjects.EntitySet.CITESApplicationSet = App.GetRelatedCITESApplication()
            If Not CitesAppSet Is Nothing AndAlso CitesAppSet.Entities.Count > 0 Then
                Return CitesAppSet.Entities(0).IsComposite
            End If
            Return False
        End Function

        Private Shared Function IsASemiComplete(ByVal PermitInfoId As Int32) As Boolean
            Dim App As New [DO].DataObjects.Entity.Application(GetAppIdFromPermitInfoId(PermitInfoId))
            If Not App Is Nothing Then
                Return App.SemiComplete
            End If
            Return False
        End Function

        Public Shared Function GetAppIdFromPermitInfoId(ByVal permitinfoid As Int32) As Int32
            Dim DOPermitInfo As New DataObjects.Entity.PermitInfo(permitinfoid)
            If Not DOPermitInfo Is Nothing AndAlso DOPermitInfo.PermitId <> 0 Then
                Dim DOPermit As New [DO].DataObjects.Entity.Permit(DOPermitInfo.PermitId, Nothing)
                Return DOPermit.ApplicationId
            End If
        End Function

        Private Shared Function GetStatusList(ByVal moreThanOneSelected As Boolean, ByVal ssouserid As Int64, ByVal permitStatusId As Int32, ByVal assignedTo As Common.AssignedToList, ByVal permitInfoId As Int32) As StatusList()
            'Dim SSOUser As BO.SSOUser = Common.GetUserInfoObject(ssoUserId)

            If permitStatusId > 0 Then
                Dim IdList As New ArrayList

                Select Case CType(permitStatusId, PermitStatusTypes)
                    Case PermitStatusTypes.BeingInput_Customer
                    Case PermitStatusTypes.BeingInput_CaseOfficer
                    Case PermitStatusTypes.SubmittedByCustomer
                        If assignedTo = Common.AssignedToList.CaseOfficer Then
                            If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                                IdList.AddRange(New Int32() {PermitStatusTypes.Authorised, _
                                                    PermitStatusTypes.Refused})
                            ElseIf Common.IsInRole(ssouserid, Common.RolesList.Customer) Then
                                IdList.Add(PermitStatusTypes.CancelPending)
                            End If
                        End If
                    Case PermitStatusTypes.ProgressAllowed
                        If assignedTo = Common.AssignedToList.CaseOfficer Then
                            If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                                IdList.AddRange(New Int32() {PermitStatusTypes.Referred, _
                                                    PermitStatusTypes.ReferredForCAndA, _
                                                    PermitStatusTypes.ReferredForSpecimenReport, _
                                                    PermitStatusTypes.Authorised, _
                                                    PermitStatusTypes.Refused, _
                                                    PermitStatusTypes.Cancelled})
                                If IsImportApplication(permitInfoId) Then IdList.Add(PermitStatusTypes.IssuedDraft)
                            ElseIf Common.IsInRole(ssouserid, Common.RolesList.Customer) Then
                                IdList.Add(PermitStatusTypes.CancelPending)
                            End If
                        End If
                    Case PermitStatusTypes.Referred
                        If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then

                            Select Case assignedTo
                                Case Common.AssignedToList.Customer
                                    If IsImportApplication(permitInfoId) Then IdList.Add(PermitStatusTypes.IssuedDraft)
                                    Select Case ProgressAllowedOrUnderAppeal(permitInfoId)
                                        Case StatusResult.ProgressAllowed : IdList.Add(PermitStatusTypes.ProgressAllowed)
                                        Case StatusResult.UnderAppeal : IdList.Add(PermitStatusTypes.UnderAppeal)
                                    End Select
                                    IdList.AddRange(New Int32() {PermitStatusTypes.ReferredForCAndA, _
                                                        PermitStatusTypes.ReferredForSpecimenReport, _
                                                        PermitStatusTypes.Authorised, _
                                                        PermitStatusTypes.Refused, _
                                                        PermitStatusTypes.Cancelled})
                                Case Common.AssignedToList.EC
                                    If IsImportApplication(permitInfoId) Then IdList.Add(PermitStatusTypes.IssuedDraft)
                                    Select Case ProgressAllowedOrUnderAppeal(permitInfoId)
                                        Case StatusResult.ProgressAllowed : IdList.Add(PermitStatusTypes.ProgressAllowed)
                                        Case StatusResult.UnderAppeal : IdList.Add(PermitStatusTypes.UnderAppeal)
                                    End Select
                                    IdList.AddRange(New Int32() {PermitStatusTypes.ReferredForCAndA, _
                                                        PermitStatusTypes.ReferredForSpecimenReport, _
                                                        PermitStatusTypes.Authorised, _
                                                        PermitStatusTypes.Refused, _
                                                        PermitStatusTypes.Cancelled})
                                Case Common.AssignedToList.HMCustomsAndExcise
                                    If IsImportApplication(permitInfoId) Then IdList.Add(PermitStatusTypes.IssuedDraft)
                                    Select Case ProgressAllowedOrUnderAppeal(permitInfoId)
                                        Case StatusResult.ProgressAllowed : IdList.Add(PermitStatusTypes.ProgressAllowed)
                                        Case StatusResult.UnderAppeal : IdList.Add(PermitStatusTypes.UnderAppeal)
                                    End Select
                                    IdList.AddRange(New Int32() {PermitStatusTypes.ReferredForCAndA, _
                                                        PermitStatusTypes.ReferredForSpecimenReport, _
                                                        PermitStatusTypes.Authorised, _
                                                        PermitStatusTypes.Refused, _
                                                        PermitStatusTypes.Cancelled})
                                Case Common.AssignedToList.Policy
                                    If IsImportApplication(permitInfoId) Then IdList.Add(PermitStatusTypes.IssuedDraft)
                                    Select Case ProgressAllowedOrUnderAppeal(permitInfoId)
                                        Case StatusResult.ProgressAllowed : IdList.Add(PermitStatusTypes.ProgressAllowed)
                                        Case StatusResult.UnderAppeal : IdList.Add(PermitStatusTypes.UnderAppeal)
                                    End Select
                                    IdList.AddRange(New Int32() {PermitStatusTypes.ReferredForCAndA, _
                                                        PermitStatusTypes.ReferredForSpecimenReport, _
                                                        PermitStatusTypes.Authorised, _
                                                        PermitStatusTypes.Refused, _
                                                        PermitStatusTypes.Cancelled})
                                Case Common.AssignedToList.CITESSecretariat
                                    If IsImportApplication(permitInfoId) Then IdList.Add(PermitStatusTypes.IssuedDraft)
                                    Select Case ProgressAllowedOrUnderAppeal(permitInfoId)
                                        Case StatusResult.ProgressAllowed : IdList.Add(PermitStatusTypes.ProgressAllowed)
                                        Case StatusResult.UnderAppeal : IdList.Add(PermitStatusTypes.UnderAppeal)
                                    End Select
                                    IdList.AddRange(New Int32() {PermitStatusTypes.ReferredForCAndA, _
                                                        PermitStatusTypes.ReferredForSpecimenReport, _
                                                        PermitStatusTypes.Authorised, _
                                                        PermitStatusTypes.Refused, _
                                                        PermitStatusTypes.Cancelled})
                                Case Common.AssignedToList.Other
                                    If IsImportApplication(permitInfoId) Then IdList.Add(PermitStatusTypes.IssuedDraft)
                                    Select Case ProgressAllowedOrUnderAppeal(permitInfoId)
                                        Case StatusResult.ProgressAllowed : IdList.Add(PermitStatusTypes.ProgressAllowed)
                                        Case StatusResult.UnderAppeal : IdList.Add(PermitStatusTypes.UnderAppeal)
                                    End Select
                                    IdList.AddRange(New Int32() {PermitStatusTypes.ReferredForCAndA, _
                                                        PermitStatusTypes.ReferredForSpecimenReport, _
                                                        PermitStatusTypes.Authorised, _
                                                        PermitStatusTypes.Refused, _
                                                        PermitStatusTypes.Cancelled})
                            End Select
                        ElseIf Common.IsInRole(ssouserid, Common.RolesList.Customer) Then
                            Select Case assignedTo
                                Case Common.AssignedToList.Customer
                                    IdList.Add(PermitStatusTypes.CancelPending)
                                Case Common.AssignedToList.JNCC
                                    IdList.Add(PermitStatusTypes.CancelPending)
                                Case Common.AssignedToList.Kew
                                    IdList.Add(PermitStatusTypes.CancelPending)
                                Case Common.AssignedToList.Inspectorate
                                    IdList.Add(PermitStatusTypes.CancelPending)
                                Case Common.AssignedToList.TeamLeader
                                    IdList.Add(PermitStatusTypes.CancelPending)
                                Case Common.AssignedToList.EC
                                    IdList.Add(PermitStatusTypes.CancelPending)
                                Case Common.AssignedToList.HMCustomsAndExcise
                                    IdList.Add(PermitStatusTypes.CancelPending)
                                Case Common.AssignedToList.Policy
                                    IdList.Add(PermitStatusTypes.CancelPending)
                                Case Common.AssignedToList.CITESSecretariat
                                    IdList.Add(PermitStatusTypes.CancelPending)
                                Case Common.AssignedToList.Other
                                    IdList.Add(PermitStatusTypes.CancelPending)
                            End Select
                        ElseIf Common.IsInRole(ssouserid, Common.RolesList.Inspectorate) Then
                            If assignedTo = Common.AssignedToList.Inspectorate Then
                                Select Case ProgressAllowedOrUnderAppeal(permitInfoId)
                                    Case StatusResult.ProgressAllowed : IdList.Add(PermitStatusTypes.ProgressAllowed)
                                    Case StatusResult.UnderAppeal : IdList.Add(PermitStatusTypes.UnderAppeal)
                                End Select
                            End If
                        ElseIf Common.IsInRole(ssouserid, Common.RolesList.Kew) Then
                            If assignedTo = Common.AssignedToList.Kew Then
                                Select Case ProgressAllowedOrUnderAppeal(permitInfoId)
                                    Case StatusResult.ProgressAllowed : IdList.Add(PermitStatusTypes.ProgressAllowed)
                                    Case StatusResult.UnderAppeal : IdList.Add(PermitStatusTypes.UnderAppeal)
                                End Select
                            End If
                        ElseIf Common.IsInRole(ssouserid, Common.RolesList.JNCC) Then
                            If assignedTo = Common.AssignedToList.JNCC Then
                                Select Case ProgressAllowedOrUnderAppeal(permitInfoId)
                                    Case StatusResult.ProgressAllowed : IdList.Add(PermitStatusTypes.ProgressAllowed)
                                    Case StatusResult.UnderAppeal : IdList.Add(PermitStatusTypes.UnderAppeal)
                                End Select
                            End If
                        ElseIf Common.IsInRole(ssouserid, Common.RolesList.TeamLeader) Then
                            If assignedTo = Common.AssignedToList.TeamLeader Then
                                Select Case ProgressAllowedOrUnderAppeal(permitInfoId)
                                    Case StatusResult.ProgressAllowed : IdList.Add(PermitStatusTypes.ProgressAllowed)
                                    Case StatusResult.UnderAppeal : IdList.Add(PermitStatusTypes.UnderAppeal)
                                End Select
                            End If
                        ElseIf Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                            If assignedTo = Common.AssignedToList.EC OrElse _
                               assignedTo = Common.AssignedToList.HMCustomsAndExcise OrElse _
                               assignedTo = Common.AssignedToList.Policy OrElse _
                               assignedTo = Common.AssignedToList.CITESSecretariat Then
                                Select Case ProgressAllowedOrUnderAppeal(permitInfoId)
                                    Case StatusResult.ProgressAllowed : IdList.Add(PermitStatusTypes.ProgressAllowed)
                                    Case StatusResult.UnderAppeal : IdList.Add(PermitStatusTypes.UnderAppeal)
                                End Select
                            End If
                        End If
                    Case PermitStatusTypes.UnderAppeal
                        If assignedTo = Common.AssignedToList.CaseOfficer Then
                            If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                                IdList.AddRange(New Int32() {PermitStatusTypes.Referred, _
                                                    PermitStatusTypes.ReferredForCAndA, _
                                                    PermitStatusTypes.ReferredForSpecimenReport, _
                                                    PermitStatusTypes.Authorised, _
                                                    PermitStatusTypes.Refused, _
                                                    PermitStatusTypes.Cancelled})
                                If IsImportApplication(permitInfoId) Then IdList.Add(PermitStatusTypes.IssuedDraft)
                            End If
                        End If
                    Case PermitStatusTypes.Authorised
                        If assignedTo = Common.AssignedToList.CaseOfficer Then
                            If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                                IdList.Add(PermitStatusTypes.Issued)
                            End If
                        End If
                        If Common.IsInRole(ssouserid, Common.RolesList.TeamLeader) Then
                            IdList.Add(PermitStatusTypes.ErrorCorrection)
                        End If
                    Case PermitStatusTypes.Issued, PermitStatusTypes.UpdatedSemiComplete
                        'If (common.IsInRole(ssouserid,Common.RolesList.TeamLeader) OrElse common.IsInRole(ssouserid,Common.RolesList.CaseOfficer)) AndAlso CType(permitStatusId, PermitStatusTypes) = PermitStatusTypes.UpdatedSemiComplete Then
                        '    IdList.AddRange(New Int32() {PermitStatusTypes.Re_Issue})
                        'End If
                        If Common.IsInRole(ssouserid, Common.RolesList.TeamLeader) AndAlso CType(permitStatusId, PermitStatusTypes) = PermitStatusTypes.UpdatedSemiComplete Then
                            IdList.Add(PermitStatusTypes.ErrorCorrection)
                        End If
                        If assignedTo = Common.AssignedToList.CaseOfficer Then
                            If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                                If Not moreThanOneSelected OrElse IsCompositeApplication(permitInfoId) Then
                                    If CType(permitStatusId, PermitStatusTypes) = PermitStatusTypes.UpdatedSemiComplete OrElse (CType(permitStatusId, PermitStatusTypes) = PermitStatusTypes.Issued AndAlso Not IsASemiComplete(permitInfoId)) Then
                                        IdList.AddRange(New Int32() {PermitStatusTypes.ReturnedUsed})
                                    End If
                                End If
                                If Not moreThanOneSelected Then
                                    If Not FateAlreadyExists(permitInfoId) Then IdList.AddRange(New Int32() {PermitStatusTypes.Fate})

                                    If CType(permitStatusId, PermitStatusTypes) = PermitStatusTypes.Issued Then
                                        If IsASemiComplete(permitInfoId) Then IdList.Add(PermitStatusTypes.UpdatedSemiComplete)
                                    End If
                                End If
                                If CType(permitStatusId, PermitStatusTypes) = PermitStatusTypes.Issued Then
                                    If IsASemiComplete(permitInfoId) Then IdList.Add(PermitStatusTypes.Print_Semi_Complete_Reminder_Letter)
                                End If
                                IdList.AddRange(New Int32() {PermitStatusTypes.ReturnedUnused, PermitStatusTypes.Duplicate})
                            End If
                        End If
                    Case PermitStatusTypes.Refused
                        If assignedTo = Common.AssignedToList.CaseOfficer Then
                            If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                                IdList.Add(PermitStatusTypes.UnderAppeal)
                            End If
                        End If
                    Case PermitStatusTypes.CancelPending
                        If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                            If assignedTo = Common.AssignedToList.CaseOfficer Then
                                IdList.Add(PermitStatusTypes.Cancelled)
                                Select Case ProgressAllowedOrUnderAppeal(permitInfoId)
                                    Case StatusResult.ProgressAllowed : IdList.Add(PermitStatusTypes.ProgressAllowed)
                                    Case StatusResult.UnderAppeal : IdList.Add(PermitStatusTypes.UnderAppeal)
                                End Select
                            ElseIf assignedTo = Common.AssignedToList.Customer Then
                                IdList.Add(PermitStatusTypes.CancelPending)
                            ElseIf assignedTo = Common.AssignedToList.EC Then
                                IdList.Add(PermitStatusTypes.CancelPending)
                            ElseIf assignedTo = Common.AssignedToList.HMCustomsAndExcise Then
                                IdList.Add(PermitStatusTypes.CancelPending)
                            ElseIf assignedTo = Common.AssignedToList.Policy Then
                                IdList.Add(PermitStatusTypes.CancelPending)
                            ElseIf assignedTo = Common.AssignedToList.CITESSecretariat Then
                                IdList.Add(PermitStatusTypes.CancelPending)
                            ElseIf assignedTo = Common.AssignedToList.Other Then
                                IdList.Add(PermitStatusTypes.CancelPending)
                            End If
                        ElseIf Common.IsInRole(ssouserid, Common.RolesList.JNCC) AndAlso _
                               assignedTo = Common.AssignedToList.JNCC Then
                            IdList.Add(PermitStatusTypes.CancelPending)
                        ElseIf Common.IsInRole(ssouserid, Common.RolesList.Kew) AndAlso _
                               assignedTo = Common.AssignedToList.Kew Then
                            IdList.Add(PermitStatusTypes.CancelPending)
                        ElseIf Common.IsInRole(ssouserid, Common.RolesList.Inspectorate) AndAlso _
                               assignedTo = Common.AssignedToList.Inspectorate Then
                            IdList.Add(PermitStatusTypes.CancelPending)
                        ElseIf Common.IsInRole(ssouserid, Common.RolesList.TeamLeader) AndAlso _
                               assignedTo = Common.AssignedToList.TeamLeader Then
                            IdList.Add(PermitStatusTypes.CancelPending)
                        End If
                    Case PermitStatusTypes.Cancelled
                        'can't be progressed
                        If Common.IsInRole(ssouserid, Common.RolesList.TeamLeader) Then
                            IdList.Add(PermitStatusTypes.ErrorCorrection)
                        End If
                    Case PermitStatusTypes.ReturnedUnused
                        'can't be progressed
                        If Common.IsInRole(ssouserid, Common.RolesList.TeamLeader) Then
                            IdList.Add(PermitStatusTypes.ErrorCorrection)
                        End If
                    Case PermitStatusTypes.ReturnedUsed
                        'PJN changed to allow returned used permits to be editted via the application summary screen
                        'this functionality may need to be added to the progression screen as it will now appear as an action
                        If assignedTo = Common.AssignedToList.CaseOfficer Then
                            If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                                If Not moreThanOneSelected Then
                                    IdList.Add(PermitStatusTypes.ReturnedUsed)
                                End If
                            End If
                        End If
                        If Common.IsInRole(ssouserid, Common.RolesList.TeamLeader) Then
                            IdList.Add(PermitStatusTypes.ErrorCorrection)
                        End If
                    Case PermitStatusTypes.ReferredForCAndA
                        If assignedTo = Common.AssignedToList.Customer Then
                            If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                                Select Case ProgressAllowedOrUnderAppeal(permitInfoId)
                                    Case StatusResult.ProgressAllowed : IdList.Add(PermitStatusTypes.ProgressAllowed)
                                    Case StatusResult.UnderAppeal : IdList.Add(PermitStatusTypes.UnderAppeal)
                                End Select
                            ElseIf Common.IsInRole(ssouserid, Common.RolesList.Customer) Then
                                Select Case ProgressAllowedOrUnderAppeal(permitInfoId)
                                    Case StatusResult.ProgressAllowed : IdList.Add(PermitStatusTypes.ProgressAllowed)
                                    Case StatusResult.UnderAppeal : IdList.Add(PermitStatusTypes.UnderAppeal)
                                End Select
                                IdList.Add(PermitStatusTypes.CancelPending)
                            End If
                        End If
                    Case PermitStatusTypes.ReferredForSpecimenReport
                        If assignedTo = Common.AssignedToList.Customer Then
                            If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                                Select Case ProgressAllowedOrUnderAppeal(permitInfoId)
                                    Case StatusResult.ProgressAllowed : IdList.Add(PermitStatusTypes.ProgressAllowed)
                                    Case StatusResult.UnderAppeal : IdList.Add(PermitStatusTypes.UnderAppeal)
                                End Select
                            ElseIf Common.IsInRole(ssouserid, Common.RolesList.Customer) Then
                                Select Case ProgressAllowedOrUnderAppeal(permitInfoId)
                                    Case StatusResult.ProgressAllowed : IdList.Add(PermitStatusTypes.ProgressAllowed)
                                    Case StatusResult.UnderAppeal : IdList.Add(PermitStatusTypes.UnderAppeal)
                                End Select
                                IdList.Add(PermitStatusTypes.CancelPending)
                            End If
                        End If
                    Case PermitStatusTypes.IssuedDraft
                        If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                            If assignedTo = Common.AssignedToList.CaseOfficer Then
                                IdList.AddRange(New Int32() {PermitStatusTypes.ProgressAllowed, _
                                    PermitStatusTypes.Authorised, _
                                    PermitStatusTypes.Refused, _
                                    PermitStatusTypes.Cancelled, _
                                    PermitStatusTypes.IssuedDraft})
                            End If
                        End If
                End Select
                If IdList.Count > 0 Then
                    'create something to put them in
                    Dim ReturnVal(IdList.Count - 1) As StatusList
                    'load the database statuses

                    Dim LoopId As Int32 = 0
                    For Each Index As Int32 In IdList
                        Dim Status As DataObjects.Entity.PermitStatus = DataObjects.Entity.PermitStatus.GetById(Index)
                        ReturnVal(LoopId) = New StatusList(Status.Id, Status.Description, Status.Action)    'MLD altered 29/10/4
                        LoopId += 1
                    Next Index
                    Return ReturnVal
                End If
            End If
            ' nothing to tell....
            Return Nothing
        End Function


        Private Shared Function FateAlreadyExists(ByVal permitinfoId As Int32) As Boolean
            Dim BOPermit As BOPermit
            Dim BOPermitInfo As New BOPermitInfo(permitinfoId)
            BOPermit = BOPermit.PolymorphicCreate(BOPermitInfo.PermitId)
            If TypeOf BOPermit Is CITES.Applications.BOCITESArticle10Permit Then
                Dim Fate As CITES.Applications.BOArticle10CertificateFate = CType(BOPermit, CITES.Applications.BOCITESArticle10Permit).GetFate(Nothing)
                If Fate Is Nothing OrElse Fate.Article10CertificateFateId = 0 Then
                    'there is no fate already
                    Return False
                End If
            End If
            Return True
        End Function

        Private Shared Function GetFirstHistory(ByVal permitInfoId As Int32) As DataObjects.Entity.PermitHistory
            Dim HistoryItems As [DO].DataObjects.EntitySet.PermitHistorySet = BOPermitHistory.GetHistory(permitInfoId)
            If HistoryItems Is Nothing Then
                Return Nothing
            Else
                'return the first as the history items are retrieved in descending
                'date order.
                Return HistoryItems.Entities(0)
            End If
        End Function

        Public Shared Function RevertBackToLastStatus(ByVal pis As Int32(), ByVal ssoUserId As Int64) As Boolean
            Dim Additional As New BO.Application.AdditionalInformation_UC222_ErrorCorrection
            Dim Success As Boolean = True
            For Each pi As Int32 In pis
                With GetLastHistory(pi)
                    Additional.StatusId = .PermitStatusId
                    If Not ChangeStatus(pis, Additional, .AssignedTo, ssoUserId) Then
                        Success = False
                        Exit For
                    End If
                End With
            Next
            Return Success
        End Function

        Private Shared Function GetLastHistory(ByVal permitInfoId As Int32) As DataObjects.Entity.PermitHistory
            Dim HistoryItems As [DO].DataObjects.EntitySet.PermitHistorySet = BOPermitHistory.GetHistory(permitInfoId)
            If HistoryItems Is Nothing Then
                Return Nothing
            Else
                'return the first as the history items are retrieved in descending
                'date order.
                Return HistoryItems.Entities(HistoryItems.Entities.Count - 1)
            End If
        End Function

        Private Function GetHistory() As [DO].DataObjects.EntitySet.PermitHistorySet
            Return GetHistory(Nothing)
        End Function

        Private Function GetHistory(ByVal tran As SqlClient.SqlTransaction) As [DO].DataObjects.EntitySet.PermitHistorySet

            If mRelatedHistory Is Nothing Then
                mRelatedHistory = BOPermitHistory.GetHistory(mPermitId, tran)
            End If
            Return mRelatedHistory
        End Function
        Private mRelatedHistory As [DO].DataObjects.EntitySet.PermitHistorySet

        'Public Function GetUserList(ByVal permissions As String, ByVal newStatus As Int32) As StatusList()
        '    Return GetUserList(permissions, newStatus)
        'End Function

        Public Function GetUserList(ByVal ssouserid As Int64, ByVal newStatus As Int32) As StatusList()
            If Not mPermitStatus Is Nothing AndAlso _
               mPermitStatus.ID > 0 Then
                Dim IdList As New ArrayList

                Dim ThisStatus As PermitStatusTypes = CType(newStatus, PermitStatusTypes)
                Select Case CType(mPermitStatus.ID, PermitStatusTypes)
                    Case PermitStatusTypes.ProgressAllowed
                        If mAssignedTo.AssignedTo = Common.AssignedToList.CaseOfficer Then
                            If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                                Select Case ThisStatus
                                    Case PermitStatusTypes.Referred
                                        IdList.AddRange(New Int32() {Common.AssignedToList.Customer, _
                                                            Common.AssignedToList.JNCC, _
                                                            Common.AssignedToList.Kew, _
                                                            Common.AssignedToList.Inspectorate, _
                                                            Common.AssignedToList.TeamLeader, _
                                                            Common.AssignedToList.EC, _
                                                            Common.AssignedToList.HMCustomsAndExcise, _
                                                            Common.AssignedToList.Policy, _
                                                            Common.AssignedToList.CITESSecretariat, _
                                                            Common.AssignedToList.Other})
                                    Case PermitStatusTypes.ReferredForCAndA, _
                                         PermitStatusTypes.ReferredForSpecimenReport
                                        IdList.Add(Common.AssignedToList.Customer)
                                    Case PermitStatusTypes.Authorised, _
                                         PermitStatusTypes.Cancelled, _
                                         PermitStatusTypes.IssuedDraft, _
                                         PermitStatusTypes.Refused
                                        IdList.Add(Common.AssignedToList.CaseOfficer)
                                End Select
                            ElseIf Common.IsInRole(ssouserid, Common.RolesList.Customer) Then
                                Select Case ThisStatus
                                    Case PermitStatusTypes.CancelPending
                                        IdList.Add(Common.AssignedToList.CaseOfficer)
                                End Select
                            End If
                        End If
                    Case PermitStatusTypes.UnderAppeal
                        If mAssignedTo.AssignedTo = Common.AssignedToList.CaseOfficer AndAlso _
                           Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                            Select Case ThisStatus
                                Case PermitStatusTypes.Referred
                                    IdList.AddRange(New Int32() {Common.AssignedToList.Customer, _
                                                        Common.AssignedToList.JNCC, _
                                                        Common.AssignedToList.Kew, _
                                                        Common.AssignedToList.Inspectorate, _
                                                        Common.AssignedToList.TeamLeader, _
                                                        Common.AssignedToList.EC, _
                                                        Common.AssignedToList.HMCustomsAndExcise, _
                                                        Common.AssignedToList.Policy, _
                                                        Common.AssignedToList.CITESSecretariat, _
                                                        Common.AssignedToList.Other})
                                Case PermitStatusTypes.ReferredForCAndA, _
                                     PermitStatusTypes.ReferredForSpecimenReport
                                    IdList.Add(Common.AssignedToList.Customer)
                                Case PermitStatusTypes.Authorised, _
                                     PermitStatusTypes.Cancelled, _
                                     PermitStatusTypes.IssuedDraft, _
                                     PermitStatusTypes.Refused
                                    IdList.Add(Common.AssignedToList.CaseOfficer)
                            End Select
                        End If
                    Case PermitStatusTypes.Referred
                        If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                            Select Case mAssignedTo.AssignedTo
                                Case Common.AssignedToList.Customer
                                    IdList.Add(Common.AssignedToList.CaseOfficer)
                                Case Common.AssignedToList.EC
                                    IdList.Add(Common.AssignedToList.CaseOfficer)
                                Case Common.AssignedToList.HMCustomsAndExcise
                                    IdList.Add(Common.AssignedToList.CaseOfficer)
                                Case Common.AssignedToList.Policy
                                    IdList.Add(Common.AssignedToList.CaseOfficer)
                                Case Common.AssignedToList.CITESSecretariat
                                    IdList.Add(Common.AssignedToList.CaseOfficer)
                                Case Common.AssignedToList.Other
                                    IdList.Add(Common.AssignedToList.CaseOfficer)
                            End Select

                        ElseIf Common.IsInRole(ssouserid, Common.RolesList.JNCC) AndAlso _
                            mAssignedTo.AssignedTo = Common.AssignedToList.JNCC Then
                            IdList.Add(Common.AssignedToList.CaseOfficer)
                        ElseIf Common.IsInRole(ssouserid, Common.RolesList.Kew) AndAlso _
                            mAssignedTo.AssignedTo = Common.AssignedToList.Kew Then
                            IdList.Add(Common.AssignedToList.CaseOfficer)
                        ElseIf Common.IsInRole(ssouserid, Common.RolesList.Inspectorate) AndAlso _
                            mAssignedTo.AssignedTo = Common.AssignedToList.Inspectorate Then
                            IdList.Add(Common.AssignedToList.CaseOfficer)
                        ElseIf Common.IsInRole(ssouserid, Common.RolesList.TeamLeader) AndAlso _
                            mAssignedTo.AssignedTo = Common.AssignedToList.TeamLeader Then
                            IdList.Add(Common.AssignedToList.CaseOfficer)
                        End If
                    Case PermitStatusTypes.ReferredForCAndA
                        If mAssignedTo.AssignedTo = Common.AssignedToList.Customer AndAlso _
                        (Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) OrElse _
                       Common.IsInRole(ssouserid, Common.RolesList.Customer)) Then
                            IdList.Add(Common.AssignedToList.CaseOfficer)
                        End If
                    Case PermitStatusTypes.ReferredForSpecimenReport
                        If mAssignedTo.AssignedTo = Common.AssignedToList.Customer AndAlso _
                           (Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) OrElse _
                           Common.IsInRole(ssouserid, Common.RolesList.Customer)) Then
                            IdList.Add(Common.AssignedToList.CaseOfficer)
                        End If
                    Case PermitStatusTypes.CancelPending
                        If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                            Select Case mAssignedTo.AssignedTo
                                Case Common.AssignedToList.Customer
                                    IdList.Add(Common.AssignedToList.CaseOfficer)
                                Case Common.AssignedToList.EC
                                    IdList.Add(Common.AssignedToList.CaseOfficer)
                                Case Common.AssignedToList.HMCustomsAndExcise
                                    IdList.Add(Common.AssignedToList.CaseOfficer)
                                Case Common.AssignedToList.Policy
                                    IdList.Add(Common.AssignedToList.CaseOfficer)
                                Case Common.AssignedToList.CITESSecretariat
                                    IdList.Add(Common.AssignedToList.CaseOfficer)
                                Case Common.AssignedToList.Other
                                    IdList.Add(Common.AssignedToList.CaseOfficer)
                                Case Common.AssignedToList.CaseOfficer
                                    IdList.Add(Common.AssignedToList.CaseOfficer)
                            End Select
                        ElseIf Common.IsInRole(ssouserid, Common.RolesList.JNCC) AndAlso _
                               mAssignedTo.AssignedTo = Common.AssignedToList.JNCC Then
                            IdList.Add(Common.AssignedToList.CaseOfficer)
                        ElseIf Common.IsInRole(ssouserid, Common.RolesList.Kew) AndAlso _
                               mAssignedTo.AssignedTo = Common.AssignedToList.Kew Then
                            IdList.Add(Common.AssignedToList.CaseOfficer)
                        ElseIf Common.IsInRole(ssouserid, Common.RolesList.Kew) AndAlso _
                               mAssignedTo.AssignedTo = Common.AssignedToList.Kew Then
                            IdList.Add(Common.AssignedToList.CaseOfficer)
                        ElseIf Common.IsInRole(ssouserid, Common.RolesList.Inspectorate) AndAlso _
                               mAssignedTo.AssignedTo = Common.AssignedToList.Inspectorate Then
                            IdList.Add(Common.AssignedToList.CaseOfficer)
                        ElseIf Common.IsInRole(ssouserid, Common.RolesList.TeamLeader) AndAlso _
                               mAssignedTo.AssignedTo = Common.AssignedToList.TeamLeader Then
                            IdList.Add(Common.AssignedToList.CaseOfficer)
                        End If
                    Case PermitStatusTypes.BeingInput_Customer
                    Case PermitStatusTypes.BeingInput_CaseOfficer
                    Case PermitStatusTypes.SubmittedByCustomer
                        If mAssignedTo.AssignedTo = Common.AssignedToList.CaseOfficer AndAlso _
                         Common.IsInRole(ssouserid, Common.RolesList.Customer) Then
                            Select Case ThisStatus
                                Case PermitStatusTypes.CancelPending
                                    IdList.AddRange(New Int32() {Common.AssignedToList.CaseOfficer, _
                                        Common.AssignedToList.Customer, _
                                        Common.AssignedToList.JNCC, _
                                        Common.AssignedToList.Kew, _
                                        Common.AssignedToList.Inspectorate, _
                                        Common.AssignedToList.TeamLeader, _
                                        Common.AssignedToList.EC, _
                                        Common.AssignedToList.HMCustomsAndExcise, _
                                        Common.AssignedToList.Policy, _
                                        Common.AssignedToList.CITESSecretariat, _
                                        Common.AssignedToList.Other})
                            End Select
                        ElseIf mAssignedTo.AssignedTo = Common.AssignedToList.CaseOfficer AndAlso _
                         Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                            Select Case ThisStatus
                                Case PermitStatusTypes.ProgressAllowed, PermitStatusTypes.Cancelled, _
                                     PermitStatusTypes.Authorised
                                    IdList.Add(Common.AssignedToList.CaseOfficer)
                                Case PermitStatusTypes.Refused
                                    IdList.Add(Common.AssignedToList.CaseOfficer)
                            End Select
                        End If
                    Case PermitStatusTypes.Authorised
                        If mAssignedTo.AssignedTo = Common.AssignedToList.CaseOfficer AndAlso _
                           Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                            Select Case ThisStatus
                                Case PermitStatusTypes.ReturnedUsed, PermitStatusTypes.Issued
                                    IdList.Add(Common.AssignedToList.CaseOfficer)
                            End Select
                        ElseIf Common.IsInRole(ssouserid, Common.RolesList.TeamLeader) Then
                            Select Case ThisStatus
                                Case PermitStatusTypes.ErrorCorrection
                                    IdList.Add(Common.AssignedToList.CaseOfficer)
                            End Select
                        End If
                    Case PermitStatusTypes.Issued, PermitStatusTypes.UpdatedSemiComplete
                        If mAssignedTo.AssignedTo = Common.AssignedToList.CaseOfficer AndAlso _
                             Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                            Select Case ThisStatus
                                Case PermitStatusTypes.ReturnedUsed, PermitStatusTypes.ReturnedUnused, _
                                    PermitStatusTypes.Duplicate, PermitStatusTypes.Print_Semi_Complete_Reminder_Letter, _
                                    PermitStatusTypes.Fate, PermitStatusTypes.Re_Issue, PermitStatusTypes.UpdatedSemiComplete

                                    IdList.Add(Common.AssignedToList.CaseOfficer)
                            End Select
                        End If
                        If Common.IsInRole(ssouserid, Common.RolesList.TeamLeader) Then
                            Select Case ThisStatus
                                Case PermitStatusTypes.ErrorCorrection
                                    IdList.Add(Common.AssignedToList.CaseOfficer)
                            End Select
                        End If
                    Case PermitStatusTypes.Refused
                        If mAssignedTo.AssignedTo = Common.AssignedToList.CaseOfficer AndAlso _
                             Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                            Select Case ThisStatus
                                Case PermitStatusTypes.UnderAppeal
                                    IdList.Add(Common.AssignedToList.CaseOfficer)
                            End Select
                        End If
                    Case PermitStatusTypes.Cancelled
                        If Common.IsInRole(ssouserid, Common.RolesList.TeamLeader) Then
                            Select Case ThisStatus
                                Case PermitStatusTypes.ErrorCorrection
                                    IdList.Add(Common.AssignedToList.CaseOfficer)
                            End Select
                        End If
                    Case PermitStatusTypes.ReturnedUnused
                        If Common.IsInRole(ssouserid, Common.RolesList.TeamLeader) Then
                            Select Case ThisStatus
                                Case PermitStatusTypes.ErrorCorrection
                                    IdList.Add(Common.AssignedToList.CaseOfficer)
                            End Select
                        End If
                    Case PermitStatusTypes.ReturnedUsed
                        If Common.IsInRole(ssouserid, Common.RolesList.TeamLeader) Then
                            Select Case ThisStatus
                                Case PermitStatusTypes.ErrorCorrection
                                    IdList.Add(Common.AssignedToList.CaseOfficer)
                            End Select
                        End If
                    Case PermitStatusTypes.IssuedDraft
                        If mAssignedTo.AssignedTo = Common.AssignedToList.CaseOfficer AndAlso _
                            Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                            IdList.Add(Common.AssignedToList.CaseOfficer)
                        End If

                End Select
                If IdList.Count > 0 Then
                    'create something to put them in
                    Dim ReturnVal(IdList.Count - 1) As StatusList
                    'load the database statuses

                    Dim LoopId As Int32 = 0
                    For Each Index As Int32 In IdList
                        Dim StatusGroup As DataObjects.Entity.StatusAssignedToGroup = DataObjects.Entity.StatusAssignedToGroup.GetById(Index)
                        ReturnVal(LoopId) = New StatusList(StatusGroup.Id, StatusGroup.Description, "") 'MLD altered 29/10/4
                        LoopId += 1
                    Next Index
                    Return ReturnVal
                End If
            End If
            Return Nothing
        End Function

        <Serializable()> _
        Public Class StatusFlags
            Public Sub New()
            End Sub

            Friend Sub New(ByVal currentPaymentStatus As Int32, ByVal assignedToList As Common.AssignedToList, ByVal ssouserid As Int64)
                Dim GetInspectionFlags As Boolean = False
                Dim GetReferralHistoryFlags As Boolean = False
                Dim GetReIssuedFlags As Boolean = False
                Dim GetSAAdviceFlags As Boolean = False
                Dim GetPaymentsFlags As Boolean = False

                Select Case CType(currentPaymentStatus, PermitStatusTypes)
                    Case PermitStatusTypes.Referred, PermitStatusTypes.CancelPending
                        If ((assignedToList = Common.AssignedToList.JNCC AndAlso _
                           Common.IsInRole(ssouserid, Common.RolesList.JNCC)) OrElse _
                           (assignedToList = Common.AssignedToList.JNCC AndAlso _
                           Common.IsInRole(ssouserid, Common.RolesList.JNCC))) Then
                            GetSAAdviceFlags = True
                            GetReferralHistoryFlags = True
                        ElseIf (assignedToList = Common.AssignedToList.Inspectorate AndAlso _
                           Common.IsInRole(ssouserid, Common.RolesList.Inspectorate)) Then
                            GetInspectionFlags = True
                        End If
                    Case PermitStatusTypes.Issued
                        If (assignedToList = Common.AssignedToList.CaseOfficer AndAlso _
                           Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer)) Then
                            GetReIssuedFlags = True
                        End If
                End Select
            End Sub

            Public InspectionFlags As DataObjects.Collection.ProgressStatusInspectionBoundCollection
            Public ReferralHistoryFlags As DataObjects.Collection.ProgressStatusReferralHistoryBoundCollection
            Public ReIssuedFlags As DataObjects.Collection.ProgressStatusReIssuedBoundCollection
            Public SAAdviceFlags As DataObjects.Collection.ProgressStatusSAAdviceBoundCollection
            Public PaymentsFlags As DataObjects.Collection.ProgressStatusPaymentBoundCollection
        End Class

        'Public Function GetStatusFlags(ByVal appId As Int32, ByVal authorisedUserId As Int32) As StatusFlags
        '    Return GetStatusFlags(appId, GetSSOUserId(authorisedUserId))
        'End Function

        Public Function GetStatusFlags(ByVal ssouserid As Int64) As StatusFlags
            If Not mPermitStatus Is Nothing AndAlso _
               mPermitStatus.ID > 0 Then
                Return New StatusFlags(mPermitStatus.ID, mAssignedTo.AssignedTo, ssouserid)
            End If
        End Function

        'Public Function CanViewApplication(ByVal permissions As String) As Boolean
        '    Return CanViewApplication(permissions)
        'End Function

        Public Function CanViewApplication(ByVal ssouserid As Int64) As Boolean
            If Not mPermitStatus Is Nothing AndAlso _
               mPermitStatus.ID > 0 Then

                Select Case CType(mPermitStatus.ID, PermitStatusTypes)
                    Case PermitStatusTypes.BeingInput_Customer
                        If (mAssignedTo.AssignedTo = Common.AssignedToList.Customer AndAlso _
                           Common.IsInRole(ssouserid, Common.RolesList.Customer)) Then
                            Return True
                        End If
                    Case PermitStatusTypes.BeingInput_CaseOfficer
                        If (mAssignedTo.AssignedTo = Common.AssignedToList.CaseOfficer AndAlso _
                          Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer)) Then
                            Return True
                        End If
                    Case PermitStatusTypes.SubmittedByCustomer
                        If mAssignedTo.AssignedTo = Common.AssignedToList.CaseOfficer Then
                            Return True
                        End If
                    Case PermitStatusTypes.ProgressAllowed
                        If mAssignedTo.AssignedTo = Common.AssignedToList.CaseOfficer Then
                            Return True
                        End If
                    Case PermitStatusTypes.UnderAppeal
                        If mAssignedTo.AssignedTo = Common.AssignedToList.CaseOfficer Then
                            Return True
                        End If
                    Case PermitStatusTypes.Referred
                        Select Case mAssignedTo.AssignedTo
                            Case Common.AssignedToList.Customer, _
                                 Common.AssignedToList.JNCC, _
                                 Common.AssignedToList.Kew, _
                                 Common.AssignedToList.Inspectorate, _
                                 Common.AssignedToList.TeamLeader, _
                                 Common.AssignedToList.EC, _
                                 Common.AssignedToList.HMCustomsAndExcise, _
                                 Common.AssignedToList.Policy, _
                                 Common.AssignedToList.CITESSecretariat, _
                                 Common.AssignedToList.Other
                                Return True
                        End Select
                    Case PermitStatusTypes.ReferredForCAndA
                        If mAssignedTo.AssignedTo = Common.AssignedToList.Customer Then
                            Return True
                        End If
                    Case PermitStatusTypes.ReferredForSpecimenReport
                        If mAssignedTo.AssignedTo = Common.AssignedToList.Customer Then
                            Return True
                        End If
                    Case PermitStatusTypes.Authorised
                        If mAssignedTo.AssignedTo = Common.AssignedToList.CaseOfficer Then
                            Return True
                        End If
                    Case PermitStatusTypes.Issued, PermitStatusTypes.Re_Issue
                        If mAssignedTo.AssignedTo = Common.AssignedToList.CaseOfficer Then
                            Return True
                        End If
                    Case PermitStatusTypes.IssuedDraft
                        If mAssignedTo.AssignedTo = Common.AssignedToList.CaseOfficer Then
                            Return True
                        End If
                    Case PermitStatusTypes.Refused
                        If mAssignedTo.AssignedTo = Common.AssignedToList.CaseOfficer Then
                            Return True
                        End If
                    Case PermitStatusTypes.CancelPending
                        Select Case mAssignedTo.AssignedTo
                            Case Common.AssignedToList.CaseOfficer, _
                                 Common.AssignedToList.Customer, _
                                 Common.AssignedToList.JNCC, _
                                 Common.AssignedToList.Kew, _
                                 Common.AssignedToList.Inspectorate, _
                                 Common.AssignedToList.TeamLeader, _
                                 Common.AssignedToList.EC, _
                                 Common.AssignedToList.HMCustomsAndExcise, _
                                 Common.AssignedToList.Policy, _
                                 Common.AssignedToList.CITESSecretariat, _
                                 Common.AssignedToList.Other
                                Return True
                        End Select
                    Case PermitStatusTypes.Cancelled
                        If mAssignedTo.AssignedTo = Common.AssignedToList.CaseOfficer Then
                            Return True
                        End If
                    Case PermitStatusTypes.UpdatedSemiComplete
                        If mAssignedTo.AssignedTo = Common.AssignedToList.CaseOfficer Then
                            Return True
                        End If
                    Case PermitStatusTypes.ReturnedUnused
                        If mAssignedTo.AssignedTo = Common.AssignedToList.CaseOfficer Then
                            Return True
                        End If
                    Case PermitStatusTypes.ReturnedUsed
                        If mAssignedTo.AssignedTo = Common.AssignedToList.CaseOfficer Then
                            Return True
                        End If
                End Select
            End If
            Return False
        End Function

        'Public Function CanUpdateApplication(ByVal appId As Int32, ByVal authorisedUserId As Int32) As Boolean
        '    Return CanUpdateApplication(appId, GetSSOUserId(authorisedUserId))
        'End Function

        Public Function CanUpdateApplication(ByVal ssouserid As Int64) As Boolean
            If Not mPermitStatus Is Nothing AndAlso _
               mPermitStatus.ID > 0 Then

                Select Case CType(mPermitStatus.ID, PermitStatusTypes)
                    Case PermitStatusTypes.BeingInput_Customer
                        If (mAssignedTo.AssignedTo = Common.AssignedToList.Customer AndAlso _
                           Common.IsInRole(ssouserid, Common.RolesList.Customer)) Then
                            Return True
                        End If
                    Case PermitStatusTypes.BeingInput_CaseOfficer
                        If (mAssignedTo.AssignedTo = Common.AssignedToList.CaseOfficer AndAlso _
                          (Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) OrElse _
                          Common.IsInRole(ssouserid, Common.RolesList.TeamLeader))) Then
                            Return True
                        End If
                    Case PermitStatusTypes.SubmittedByCustomer
                        If (mAssignedTo.AssignedTo = Common.AssignedToList.CaseOfficer AndAlso _
                          (Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) OrElse _
                          Common.IsInRole(ssouserid, Common.RolesList.TeamLeader))) Then
                            Return True
                        End If
                    Case PermitStatusTypes.ProgressAllowed
                        If (mAssignedTo.AssignedTo = Common.AssignedToList.CaseOfficer AndAlso _
                          (Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) OrElse _
                          Common.IsInRole(ssouserid, Common.RolesList.TeamLeader))) Then
                            Return True
                        End If
                    Case PermitStatusTypes.UnderAppeal
                        If (mAssignedTo.AssignedTo = Common.AssignedToList.CaseOfficer AndAlso _
                          (Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) OrElse _
                          Common.IsInRole(ssouserid, Common.RolesList.TeamLeader))) Then
                            Return True
                        End If
                    Case PermitStatusTypes.Referred
                        Select Case mAssignedTo.AssignedTo
                            Case Common.AssignedToList.Customer, _
                                 Common.AssignedToList.TeamLeader, _
                                 Common.AssignedToList.EC, _
                                 Common.AssignedToList.HMCustomsAndExcise, _
                                 Common.AssignedToList.Policy, _
                                 Common.AssignedToList.CITESSecretariat, _
                                 Common.AssignedToList.Other
                                If (Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) OrElse _
                                   Common.IsInRole(ssouserid, Common.RolesList.TeamLeader)) Then
                                    Return True
                                End If
                            Case Common.AssignedToList.JNCC
                                If Common.IsInRole(ssouserid, Common.RolesList.JNCC) Then
                                    Return True
                                End If
                            Case Common.AssignedToList.Kew
                                If Common.IsInRole(ssouserid, Common.RolesList.Kew) Then
                                    Return True
                                End If
                        End Select
                    Case PermitStatusTypes.ReferredForCAndA
                        If (mAssignedTo.AssignedTo = Common.AssignedToList.Customer AndAlso _
                          (Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) OrElse _
                          Common.IsInRole(ssouserid, Common.RolesList.TeamLeader))) Then
                            Return True
                        End If
                    Case PermitStatusTypes.ReferredForSpecimenReport
                        If (mAssignedTo.AssignedTo = Common.AssignedToList.Customer AndAlso _
                          (Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) OrElse _
                          Common.IsInRole(ssouserid, Common.RolesList.TeamLeader))) Then
                            Return True
                        End If
                    Case PermitStatusTypes.IssuedDraft
                        If (mAssignedTo.AssignedTo = Common.AssignedToList.CaseOfficer AndAlso _
                          (Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) OrElse _
                          Common.IsInRole(ssouserid, Common.RolesList.TeamLeader))) Then
                            Return True
                        End If
                End Select
            End If
            Return False
        End Function

        Private Enum StatusResult
            Neither
            ProgressAllowed
            UnderAppeal
        End Enum

        Private Shared Function GetSSOUserId(ByVal authorisedUserId As Int64) As Int64
            Dim AuthUser As BOAuthorisedUser = New BOAuthorisedUser(authorisedUserId)
            Return AuthUser.SSOUserid
        End Function

        Private Function ProgressAllowedOrUnderAppeal() As StatusResult
            Return ProgressAllowedOrUnderAppeal(mPermitId, Nothing)
        End Function

        Private Shared Function ProgressAllowedOrUnderAppeal(ByVal permitId As Int32) As StatusResult
            Return ProgressAllowedOrUnderAppeal(permitId, Nothing)
        End Function

        Private Function ProgressAllowedOrUnderAppeal(ByVal tran As SqlClient.SqlTransaction) As StatusResult
            Return ProgressAllowedOrUnderAppeal(mPermitId, tran)
        End Function

        Private Shared Function ProgressAllowedOrUnderAppeal(ByVal permitinfoId As Int32, ByVal tran As SqlClient.SqlTransaction) As StatusResult
            Dim Result As StatusResult = StatusResult.Neither
            Dim HistoryItems As [DO].DataObjects.EntitySet.PermitHistorySet = BOPermitHistory.GetHistory(permitinfoId, tran)
            If Not HistoryItems Is Nothing Then
                For Each HistoryItem As DataObjects.Entity.PermitHistory In HistoryItems.Entities
                    Select Case HistoryItem.PermitStatusId
                        Case BOPermitInfo.PermitStatusTypes.UnderAppeal
                            Result = StatusResult.UnderAppeal
                            Exit For
                        Case BOPermitInfo.PermitStatusTypes.ProgressAllowed
                            Result = StatusResult.ProgressAllowed
                            Exit For
                    End Select
                Next HistoryItem
            End If
            Return Result
        End Function

        <Serializable()> _
        Public Enum ActionResults
            Done
            UC220   ' cancelled, cancel pending
            UC212   ' authorised, rejected
            UC203   ' referred for c&a
            UC202   ' referred for spec rep
            UC507   ' issued
            UC207   ' updated semi-complete
            UC216   ' returned unused, returned used
            UC218   ' ?????
        End Enum
        Public Function PerformAction(ByVal newStatus As Int32, ByVal tran As SqlClient.SqlTransaction) As ActionResults
            'Dim NewStatusVal As PermitStatusTypes = CType(newStatus, PermitStatusTypes)

            'If Not mPermitStatus Is Nothing AndAlso _
            '   mPermitStatus.ID > 0 Then

            '    Select Case CType(mPermitStatus.ID, PermitStatusTypes)
            '        Case PermitStatusTypes.BeingInput_Customer
            '            If (mAssignedTo.AssignedTo = Common.AssignedToList.Customer AndAlso _
            '               Common.HasPermissions(Common.UserPermissions.IsCustomer)) Then
            '                Return True
            '            End If
            '        Case PermitStatusTypes.BeingInput_CaseOfficer
            '            If (mAssignedTo.AssignedTo = Common.AssignedToList.CaseOfficer AndAlso _
            '              (Common.HasPermissions(Common.UserPermissions.IsCaseOfficer) OrElse _
            '              Common.HasPermissions(Common.UserPermissions.IsTeamLeader))) Then
            '                Return True
            '            End If
            '        Case PermitStatusTypes.SubmittedByCustomer
            '            If (mAssignedTo.AssignedTo = Common.AssignedToList.CaseOfficer AndAlso _
            '              (Common.HasPermissions(Common.UserPermissions.IsCaseOfficer) OrElse _
            '              Common.HasPermissions(Common.UserPermissions.IsTeamLeader))) Then
            '                Return True
            '            End If
            '        Case PermitStatusTypes.ProgressAllowed
            '            If (mAssignedTo.AssignedTo = Common.AssignedToList.CaseOfficer AndAlso _
            '              (Common.HasPermissions(Common.UserPermissions.IsCaseOfficer) OrElse _
            '              Common.HasPermissions(Common.UserPermissions.IsTeamLeader))) Then
            '                Return True
            '            End If
            '        Case PermitStatusTypes.UnderAppeal
            '            If (mAssignedTo.AssignedTo = Common.AssignedToList.CaseOfficer AndAlso _
            '              (Common.HasPermissions(Common.UserPermissions.IsCaseOfficer) OrElse _
            '              Common.HasPermissions(Common.UserPermissions.IsTeamLeader))) Then
            '                Return True
            '            End If
            '        Case PermitStatusTypes.Referred
            '            Select Case mAssignedTo.AssignedTo
            '                Case Common.AssignedToList.Customer, _
            '                     Common.AssignedToList.TeamLeader, _
            '                     Common.AssignedToList.EC, _
            '                     Common.AssignedToList.HMCustomsAndExcise, _
            '                     Common.AssignedToList.Policy, _
            '                     Common.AssignedToList.CITESSecretariat, _
            '                     Common.AssignedToList.Other
            '                    If (Common.HasPermissions(Common.UserPermissions.IsCaseOfficer) OrElse _
            '                       Common.HasPermissions(Common.UserPermissions.IsTeamLeader)) Then
            '                        Return True
            '                    End If
            '                Case Common.AssignedToList.JNCC
            '                    If Common.HasPermissions(Common.UserPermissions.IsJNCC) Then
            '                        Return True
            '                    End If
            '                Case Common.AssignedToList.Kew
            '                    If Common.HasPermissions(Common.UserPermissions.IsKew) Then
            '                        Return True
            '                    End If
            '            End Select
            '        Case PermitStatusTypes.ReferredForCAndA
            '            If (mAssignedTo.AssignedTo = Common.AssignedToList.Customer AndAlso _
            '              (Common.HasPermissions(Common.UserPermissions.IsCaseOfficer) OrElse _
            '              Common.HasPermissions(Common.UserPermissions.IsTeamLeader))) Then
            '                Return True
            '            End If
            '        Case PermitStatusTypes.ReferredForSpecimenReport
            '            If (mAssignedTo.AssignedTo = Common.AssignedToList.Customer AndAlso _
            '              (Common.HasPermissions(Common.UserPermissions.IsCaseOfficer) OrElse _
            '              Common.HasPermissions(Common.UserPermissions.IsTeamLeader))) Then
            '                Return True
            '            End If
            '        Case PermitStatusTypes.IssuedDraft
            '            If (mAssignedTo.AssignedTo = Common.AssignedToList.CaseOfficer AndAlso _
            '              (Common.HasPermissions(Common.UserPermissions.IsCaseOfficer) OrElse _
            '              Common.HasPermissions(Common.UserPermissions.IsTeamLeader))) Then
            '                Return True
            '            End If
            '    End Select
            'End If
        End Function

        Friend Function GetNextIssue() As String
            Return GetNextIssue(Me)
        End Function

        Private Shared Function GetNextIssue(ByVal permitInfo As BOPermitInfo) As String
            If Not permitInfo Is Nothing Then
                If Not permitInfo.Issue Is Nothing AndAlso permitInfo.Issue.ToString.Length > 0 Then
                    Dim CurrentIssue As String = permitInfo.Issue.ToString
                    Dim Ascii As New System.Text.ASCIIEncoding
                    If CurrentIssue.Length = 1 OrElse CurrentIssue.Trim.Length = 1 Then
                        If String.Compare(CurrentIssue, "Z") = 0 Then
                            Return "AA"
                        Else
                            Dim ascVal As Int32 = Ascii.Convert(System.Text.Encoding.ASCII, System.Text.Encoding.Default, Ascii.GetBytes(CurrentIssue))(0)
                            Return Ascii.GetString(New Byte() {CType(ascVal + 1, Byte)})
                        End If
                    Else
                        Dim Chars(1) As Byte
                        If String.Compare(CurrentIssue.Substring(1, 1), "Z") = 0 Then
                            'if it ends in a Z, it should now end in an A and the first char is incemented
                            Chars(0) = CType(Ascii.Convert(System.Text.Encoding.ASCII, System.Text.Encoding.Default, Ascii.GetBytes(CurrentIssue.Substring(0, 1)))(0) + 1, Byte)
                            Chars(1) = Ascii.Convert(System.Text.Encoding.ASCII, System.Text.Encoding.Default, Ascii.GetBytes("A"))(0)
                        Else
                            Chars(0) = Ascii.Convert(System.Text.Encoding.ASCII, System.Text.Encoding.Default, Ascii.GetBytes(CurrentIssue.Substring(0, 1)))(0)
                            Chars(1) = CType(Ascii.Convert(System.Text.Encoding.ASCII, System.Text.Encoding.Default, Ascii.GetBytes(CurrentIssue.Substring(1, 1)))(0) + 1, Byte)
                        End If
                        Return Ascii.GetString(Chars)
                    End If
                Else
                    Return "A"
                End If
            End If
        End Function
#End Region

#Region " Save "
        Public Overridable Overloads Function Save(ByVal authorisedUserId As Int64) As BaseBO
            Dim NewPermitInfo As New DataObjects.Entity.PermitInfo
            Dim service As DataObjects.Service.PermitInfoService = NewPermitInfo.ServiceObject
            Dim tran As SqlClient.SqlTransaction = service.BeginTransaction

            Dim SaveResult As BaseBO = MyClass.Save(authorisedUserId, tran)
            If SaveResult Is Nothing Then
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
            Else
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Commit)
            End If
            Return SaveResult
        End Function

        Public Overridable Overloads Function Save(ByVal authorisedUserId As Int64, ByVal tran As SqlClient.SqlTransaction) As BaseBO
            MyBase.Save()

            Const ReReferredTableId As Int32 = 2

            Dim NewPermitInfo As New DataObjects.Entity.PermitInfo
            Dim service As DataObjects.Service.PermitInfoService = NewPermitInfo.ServiceObject

            Created = (service.GetById(mPermitInfoId, tran) Is Nothing)

            Dim OldPermitInfo As BOPermitInfo
            Dim ReferredCount As Int32 = mReReferredCount

            If Me.CreatedByUser Is Nothing Then
                ' Me.CreatedByUser = New BO.BOAuthorisedUser(authorisedUserId)
            End If

            If Created Then
                mCreatedDate = Date.Now
                'mCreatedbyuserid = 
                NewPermitInfo = service.Insert(mPermitId, _
                                            ProgressStatusPaymentId, _
                                            ProgressStatusInspectionId, _
                                            ProgressStatusSAAdviceId, _
                                            ProgressStatusReferralHistoryId, _
                                            ProgressStatusReIssuedId, _
                                            PermitStatusId, _
                                            ReferredCount, _
                                            AssignedToId, _
                                            CreatedDate, _
                                            Me.CreatedByUserId, _
                                            NextActionDate, _
                                            CancelReason, _
                                            CancelPendingReason, _
                                            SequenceNumber, _
                                            RefusalReasonId, _
                                            RefusalReasonAdHoc, _
                                            mIssueDate, _
                                            mIssue, _
                                            IssuedById, _
                                            mPlaceOfIssue, _
                                            Nothing, _
                                            Me.mAuthorisationDate, _
                                            mDateRefused, _
                                            Nothing, _
                                            mPrintJobId, _
                                            mCancelPendingDeclineReason, _
                                            mCoveringLetterReportId, _
                                            mSemiCompleteSpecimenId, _
                                            mSemiCompleteSpecieId, _
                                            mSemiCompleteSecondPartyId, _
                                            mSemiCompleteCountryId, _
                                            mSemiCompleteUOMId, _
                                            tran)
            Else
                'load the old details
                OldPermitInfo = New BOPermitInfo(mPermitInfoId, tran)
                'check to see if the rereffered count should be changed
                If Not ProgressStatusReferralHistory Is Nothing Then
                    'if the old and current are different and the new is re-referred then increment the count
                    If ProgressStatusReferralHistory.ID = ReReferredTableId AndAlso _
                       (OldPermitInfo.ProgressStatusReferralHistoryId Is Nothing OrElse _
                        (Not OldPermitInfo.ProgressStatusReferralHistory Is Nothing AndAlso _
                         OldPermitInfo.ProgressStatusReferralHistory.ID <> ProgressStatusReferralHistory.ID)) Then
                        ReferredCount += 1
                    End If
                End If

                '                DoClockWork(OldPermitInfo, tran)

                NewPermitInfo = service.Update(mPermitInfoId, _
                                            mPermitId, _
                                            ProgressStatusPaymentId, _
                                            ProgressStatusInspectionId, _
                                            ProgressStatusSAAdviceId, _
                                            ProgressStatusReferralHistoryId, _
                                            ProgressStatusReIssuedId, _
                                            PermitStatusId, _
                                            ReferredCount, _
                                            AssignedToId, _
                                            mCreatedDate, _
                                            CreatedByUserId, _
                                            mNextActionDate, _
                                            mCancelReason, _
                                            mCancelPendingReason, _
                                            mSequenceNumber, _
                                            RefusalReasonId, _
                                            RefusalReasonAdHoc, _
                                            mIssueDate, _
                                            mIssue, _
                                            IssuedById, _
                                            mPlaceOfIssue, _
                                            Nothing, _
                                            mAuthorisationDate, _
                                            mDateRefused, _
                                            Nothing, _
                                            mPrintJobId, _
                                            mCancelPendingDeclineReason, _
                                            mCoveringLetterReportId, _
                                            mSemiCompleteSpecimenId, _
                                            mSemiCompleteSpecieId, _
                                            mSemiCompleteSecondPartyId, _
                                            mSemiCompleteCountryId, _
                                            mSemiCompleteUOMId, _
                                            CheckSum, _
                                            tran)
            End If
            'check to see if any SQL errors have occured
            If NewPermitInfo Is Nothing AndAlso Not DataObjects.Sprocs.LastError Is Nothing Then
                'TODO: Use errors collection to check to see if the problem was concurrency
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSavePermit)

            ElseIf NewPermitInfo Is Nothing Then
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSavePermit)
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
            Else

                If Not SemiCompleteSpecie Is Nothing Then
                    Me.SemiCompleteSpecie = SemiCompleteSpecie.Save(tran)
                    If Not SemiCompleteSpecie.ValidationErrors Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    ValidationErrors = SemiCompleteSpecie.ValidationErrors
                End If
                If Not SemiCompleteSpecimen Is Nothing Then
                    Me.SemiCompleteSpecimen.Save(0, Me.PermitId, tran)
                    If Not SemiCompleteSpecimen.ValidationErrors Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    ValidationErrors = SemiCompleteSpecimen.ValidationErrors
                End If

                If NewPermitInfo.CheckSum <> CheckSum Then
                    mReReferredCount = ReferredCount
                    'insert a CITES clock record
                    If Created Then
                        Dim RecievedDate As Object = Nothing
                        If CType(NewPermitInfo.AssignedTo, Common.AssignedToList) = Common.AssignedToList.CaseOfficer Then
                            'only bother if they are a case officer creating the record
                            Dim Permit As New BOPermit(NewPermitInfo.PermitId)
                            If Not Permit Is Nothing Then
                                Dim App As BOApplication = Permit.GetApplication()
                                If Not App Is Nothing Then
                                    RecievedDate = App.ReceivedDate
                                End If
                            End If
                        End If
                        Dim ClockService As DataObjects.Service.CITESClockService = DataObjects.Entity.CITESClock.ServiceObject
                        ClockService.Insert(NewPermitInfo.PermitInfoId, 0, 0, 0, 0, RecievedDate, Nothing, Nothing, Nothing, tran)
                    End If
                    'before we initialise this item, write a history record
                    'no point in doing so unless the 
                    If Not OldPermitInfo Is Nothing Then
                        BOPermitHistory.MakeHistory(OldPermitInfo, NewPermitInfo, authorisedUserId, tran)
                    End If
                    'no point in initialising unless things have changed
                    InitialisePermitInfo(NewPermitInfo, tran)
                End If
            End If

            Return Me
        End Function
#End Region


    End Class
End Namespace
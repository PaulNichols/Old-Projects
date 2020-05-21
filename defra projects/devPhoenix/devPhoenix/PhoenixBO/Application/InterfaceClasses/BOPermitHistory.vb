Namespace Application
    Public Class BOPermitHistory
        Inherits BaseBO
        Implements IBOPermitHistory




#Region " Prelim code "
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal permitHistoryId As Int32)
            MyClass.New()
            LoadPermitHistory(permitHistoryId)
        End Sub

        Private Function LoadPermitHistory(ByVal id As Int32) As DataObjects.Entity.PermitHistory
            Return LoadPermitHistory(id, Nothing)
        End Function

        Private Function LoadPermitHistory(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.PermitHistory
            Dim NewPermitHistory As DataObjects.Entity.PermitHistory = DataObjects.Entity.PermitHistory.GetById(id)
            If NewPermitHistory Is Nothing Then
                Throw New RecordDoesNotExist("PermitHistory", 0)
            Else
                InitialisePermitHistory(NewPermitHistory, tran)
            End If
            Return NewPermitHistory
        End Function

        Friend Overridable Sub InitialisePermitHistory(ByVal permitHistory As DataObjects.Entity.PermitHistory, ByVal tran As SqlClient.SqlTransaction)
            Try
                With permitHistory
                    CheckSum = .CheckSum
                    mPermitHistoryId = .Id
                    mPermitInfoId = .PermitInfoId
                    mChangeDate = .ChangeDate
                    mChangedByUser = New BOAuthorisedUser(CType(.ChangedByUserId, Int64))
                    mAssignedTo = New ReferenceData.BOStatusAssignedToGroup(.AssignedTo)
                    If Not .IsReportPrintJobIdNull Then mReportPrintJobId = .ReportPrintJobId
                    If Not .IsCoveringLetterReportIdNull Then mCoveringLetterReportId = .CoveringLetterReportId

                    'load the status
                    If Not .IsProgressStatusInspectionIdNull Then mProgressStatusInspection = New Application.ProgressStatus.BOProgressStatusInspection(.ProgressStatusInspectionId, tran)
                    If Not .IsProgressStatusPaymentIdNull Then mProgressStatusPayment = New Application.ProgressStatus.BOProgressStatusPayment(.ProgressStatusPaymentId, tran)
                    If Not .IsProgressStatusReferralHistoryIdNull Then mProgressStatusReferralHistory = New Application.ProgressStatus.BOProgressStatusReferralHistory(.ProgressStatusReferralHistoryId, tran)
                    If Not .IsProgressStatusReIssuedIdNull Then mProgressStatusReIssued = New Application.ProgressStatus.BOProgressStatusReIssued(.ProgressStatusReIssuedId, tran)
                    If Not .IsProgressStatusSAAdviceIdNull Then mProgressStatusSAAdvice = New Application.ProgressStatus.BOProgressStatusSAAdvice(.ProgressStatusSAAdviceId, tran)
                    If Not .IsPermitStatusIdNull Then mPermitStatus = New ReferenceData.BOPermitStatus(.PermitStatusId)
                End With
            Catch ex As Exception
            End Try
        End Sub

        Private Shared Function CreateBlank(ByVal authorisedUserId As Int64) As DataObjects.Entity.PermitHistory
            Dim Blank As New DataObjects.Entity.PermitHistory
            With Blank
                .CreateEmptyEntity()
                .Id = 0
                .ChangedByUserId = CType(authorisedUserId, Decimal)
                .ChangeDate = Date.Now

                'all the other fields are nullable and we want them that way, so leave them alone
                'as they are null by default.
            End With
            Return Blank
        End Function
#End Region

#Region " Properties "
        Public Property SemiCompleteCountryId() As Object Implements IBOPermitInfo.SemiCompleteCountryId
            Get

            End Get
            Set(ByVal Value As Object)

            End Set
        End Property

        Public Property SemiCompleteSecondPartyId() As Object Implements IBOPermitInfo.SemiCompleteSecondPartyId
            Get

            End Get
            Set(ByVal Value As Object)

            End Set
        End Property

        Public Property SemiCompleteSpecieId() As Object Implements IBOPermitInfo.SemiCompleteSpecieId
            Get

            End Get
            Set(ByVal Value As Object)

            End Set
        End Property

        Public Property SemiCompleteSpecimenId() As Object Implements IBOPermitInfo.SemiCompleteSpecimenId
            Get

            End Get
            Set(ByVal Value As Object)

            End Set
        End Property

        Public Property SemicompleteUOMId() As Object Implements IBOPermitInfo.SemicompleteUOMId
            Get

            End Get
            Set(ByVal Value As Object)

            End Set
        End Property

        Public Property DateRefused() As Object Implements IBOPermitInfo.DateRefused
            Get
                Return mDateRefused
            End Get
            Set(ByVal Value As Object)
                mDateRefused = Value
            End Set
        End Property
        Private mDateRefused As Object

        Public Property CoveringLetterReportId() As Object Implements IBOPermitInfo.CoveringLetterReportId
            Get
                Return mCoveringLetterReportId
            End Get
            Set(ByVal Value As Object)
                mCoveringLetterReportId = Value
            End Set
        End Property
        Private mCoveringLetterReportId As Object

        Public Property ReportPrintJobId() As Object Implements IBOPermitInfo.PrintJobId
            Get
                Return mReportPrintJobId
            End Get
            Set(ByVal Value As Object)
                mReportPrintJobId = Value
            End Set
        End Property
        Private mReportPrintJobId As Object

        Public Property AssignedTo() As ReferenceData.BOStatusAssignedToGroup Implements IBOPermitInfo.AssignedTo
            Get
                Return mAssignedTo
            End Get
            Set(ByVal Value As ReferenceData.BOStatusAssignedToGroup)
                mAssignedTo = Value
            End Set
        End Property
        Private mAssignedTo As ReferenceData.BOStatusAssignedToGroup

        Public Property PermitHistoryId() As Integer Implements IBOPermitHistory.PermitHistoryId
            Get
                Return mPermitHistoryId
            End Get
            Set(ByVal Value As Integer)
                mPermitHistoryId = Value
            End Set
        End Property
        Private mPermitHistoryId As Int32

        Public Property PermitId() As Integer Implements IBOPermitInfo.PermitId
            Get
                Throw New NotImplementedException("SequenceNumber")
            End Get
            Set(ByVal Value As Integer)
                Throw New NotImplementedException("SequenceNumber")
            End Set
        End Property

        Public Property PermitInfoId() As Integer Implements IBOPermitHistory.PermitInfoId
            Get
                Return mPermitInfoId
            End Get
            Set(ByVal Value As Integer)
                mPermitInfoId = Value
            End Set
        End Property
        Private mPermitInfoId As Int32

        Public Property PermitStatus() As ReferenceData.BOPermitStatus Implements IBOPermitInfo.PermitStatus
            Get
                Return mPermitStatus
            End Get
            Set(ByVal Value As ReferenceData.BOPermitStatus)
                mPermitStatus = Value
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

        Public Property ChangeDate() As Date Implements IBOPermitHistory.ChangeDate
            Get
                Return mChangeDate
            End Get
            Set(ByVal Value As Date)
                mChangeDate = Value
            End Set
        End Property
        Private mChangeDate As Date

        Public Property ChangedByUser() As BOAuthorisedUser Implements IBOPermitHistory.ChangedByUser
            Get
                Return mChangedByUser
            End Get
            Set(ByVal Value As BOAuthorisedUser)
                mChangedByUser = Value
            End Set
        End Property
        Private mChangedByUser As BOAuthorisedUser

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
                Throw New NotImplementedException("SequenceNumber")
            End Get
            Set(ByVal Value As Int32)
                Throw New NotImplementedException("SequenceNumber")
            End Set
        End Property
#End Region

#Region " Helper Functions "
        Protected ReadOnly Property ChangedByUserId() As Int64
            Get
                Return mChangedByUser.SSOUserid
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

        Protected Friend ReadOnly Property AssignedToId() As Object
            Get
                If mAssignedTo Is Nothing OrElse mAssignedTo.ID = 0 Then
                    Return Nothing
                Else
                    Return mAssignedTo.ID
                End If
            End Get
        End Property

        Protected ReadOnly Property ProgressStatusPaymentId() As Object
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
#End Region

#Region " Save "
        Public Overridable Overloads Function Save(ByRef permitHistory As DataObjects.Entity.PermitHistory, ByVal tran As SqlClient.SqlTransaction) As BaseBO
            Dim ph As New BOPermitHistory
            With ph
                .ChangeDate = permitHistory.ChangeDate
                .ChangedByUser = New BOAuthorisedUser : .ChangedByUser.SSOUserid = CType(permitHistory.ChangedByUserId, Int64)
                .PermitHistoryId = permitHistory.PermitHistoryId

                .ProgressStatusInspection = New ProgressStatus.BOProgressStatusInspection : .ProgressStatusInspection.ID = permitHistory.ProgressStatusInspectionId
                .ProgressStatusPayment = New ProgressStatus.BOProgressStatusPayment : .ProgressStatusPayment.ID = permitHistory.ProgressStatusPaymentId
                .ProgressStatusReferralHistory = New ProgressStatus.BOProgressStatusReferralHistory : .ProgressStatusReferralHistory.ID = permitHistory.ProgressStatusReferralHistoryId
                .ProgressStatusReIssued = New ProgressStatus.BOProgressStatusReIssued : .ProgressStatusReIssued.ID = permitHistory.ProgressStatusReIssuedId
                .ProgressStatusSAAdvice = New ProgressStatus.BOProgressStatusSAAdvice : .ProgressStatusSAAdvice.ID = permitHistory.ProgressStatusSAAdviceId
                .PermitStatus = New ReferenceData.BOPermitStatus : .PermitStatus.ID = permitHistory.PermitStatusId
                .AssignedTo = New ReferenceData.BOStatusAssignedToGroup : .AssignedTo.ID = permitHistory.AssignedTo
                .CancelPendingDeclineReason = permitHistory.CancelPendingDeclineReason
                .CancelPendingReason = permitHistory.CancelPendingReason
                .PermitInfoId = permitHistory.PermitInfoId
                .NextActionDate = permitHistory.NextActionDate
            End With
            Return Save(ph, tran)
        End Function
        Public Overridable Overloads Function Save(ByVal tran As SqlClient.SqlTransaction) As BaseBO
            Return Save(Me, tran)
        End Function

        Public Overridable Overloads Function Save(ByRef permitHistory As BOPermitHistory, ByVal tran As SqlClient.SqlTransaction) As BaseBO
            MyBase.Save()

            Dim NewPermitHistory As New DataObjects.Entity.PermitHistory
            Dim service As DataObjects.Service.PermitHistoryService = NewPermitHistory.ServiceObject

            Created = (mPermitHistoryId = 0)

            With permitHistory
                If Created Then
                    NewPermitHistory = service.Insert(.PermitInfoId, _
                                                      .ProgressStatusPaymentId, _
                                                      .ProgressStatusInspectionId, _
                                                     .ProgressStatusSAAdviceId, _
                                                      .ProgressStatusReferralHistoryId, _
                                                      .ProgressStatusReIssuedId, _
                                                      .PermitStatusId, _
                                                      .ChangeDate, _
                                                      .ChangedByUserId, _
                                                      .AssignedToId, _
                                                      .NextActionDate, _
                                                      .CancelReason, _
                                                      .CancelPendingReason, _
                                                      .ReportPrintJobId, _
                                                       .CancelPendingDeclineReason, _
                                                        .CoveringLetterReportId, _
                                                      tran)
                Else
                    NewPermitHistory = service.Update(.PermitHistoryId, _
                                                      .PermitInfoId, _
                                                      .ProgressStatusPaymentId, _
                                                      .ProgressStatusInspectionId, _
                                                      .ProgressStatusSAAdviceId, _
                                                      .ProgressStatusReferralHistoryId, _
                                                      .ProgressStatusReIssuedId, _
                                                      .PermitStatusId, _
                                                      .ChangeDate, _
                                                      .ChangedByUserId, _
                                                      .AssignedToId, _
                                                      .NextActionDate, _
                                                      .CancelReason, _
                                                      .CancelPendingReason, _
                                                      .ReportPrintJobId, _
                                                      .CancelPendingDeclineReason, _
                                                      .CoveringLetterReportId, _
                                                      CheckSum, _
                                                      tran)
                End If
            End With
            'check to see if any SQL errors have occured
            If NewPermitHistory Is Nothing AndAlso Not DataObjects.Sprocs.LastError Is Nothing Then
                'TODO: Use errors collection to check to see if the problem was concurrency
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSavePermit)

            ElseIf NewPermitHistory Is Nothing Then
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSavePermit)
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
            Else
                If Created And Not NewPermitHistory Is Nothing Then
                    permitHistory.PermitHistoryId = NewPermitHistory.Id
                End If
                If NewPermitHistory.CheckSum <> CheckSum Then
                    'no point in initialising unless things have changed
                    permitHistory.InitialisePermitHistory(NewPermitHistory, tran)
                End If
            End If

            Return permitHistory
        End Function
#End Region

#Region " Operations "
        Friend Function GetLastPermitHistory(ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.PermitHistory
            Return GetLastPermitHistory(mPermitInfoId, tran)
        End Function

        Friend Shared Function GetLastPermitHistory(ByVal permitInfoId As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.PermitHistory
            Dim HistoryItems As [DO].DataObjects.EntitySet.PermitHistorySet = GetHistory(permitInfoId, tran)
            If Not HistoryItems Is Nothing Then
                'return the first as the history items are retrieved in descending
                'date order.
                Return HistoryItems.Entities(0)
            End If
        End Function

        Friend Function GetHistory() As [DO].DataObjects.EntitySet.PermitHistorySet
            Return GetHistory(tran:=Nothing)
        End Function

        Friend Function GetHistory(ByVal tran As SqlClient.SqlTransaction) As [DO].DataObjects.EntitySet.PermitHistorySet
            Return GetHistory(mPermitInfoId, tran)
        End Function

        Friend Shared Function GetHistory(ByVal permitInfoId As Int32) As [DO].DataObjects.EntitySet.PermitHistorySet
            Return GetHistory(permitInfoId, Nothing)
        End Function

        Friend Shared Function GetHistory(ByVal permitInfoId As Int32, ByVal tran As SqlClient.SqlTransaction) As [DO].DataObjects.EntitySet.PermitHistorySet
            Dim PermitHistory As [DO].DataObjects.EntitySet.PermitHistorySet = DataObjects.Entity.PermitHistory.GetForPermitInfoWithOrder(permitInfoId, DataObjects.Base.PermitHistoryServiceBase.OrderBy.ChangeDate, tran)
            If Not PermitHistory Is Nothing AndAlso _
               PermitHistory.Count > 0 Then
                Return PermitHistory
            Else
                Return Nothing
            End If
        End Function

        Public Shared Function MakeHistory(ByVal oldPermitInfo As BOPermitInfo, ByVal newPermitInfo As DataObjects.Entity.PermitInfo, ByVal authorisedUserId As Int64, ByVal tran As SqlClient.SqlTransaction) As Boolean
            If authorisedUserId > 0 Then
                'create a new data object to put the changes into
                Dim HistoryDO As [DO].DataObjects.Entity.PermitHistory = BOPermitHistory.CreateBlank(authorisedUserId)
                HistoryDO.ChangedByUserId = authorisedUserId
                HistoryDO.ChangeDate = Date.Now
                HistoryDO.PermitInfoId = oldPermitInfo.PermitInfoId
                If Not oldPermitInfo.PermitStatus Is Nothing AndAlso _
                   Not newPermitInfo.IsPermitStatusIdNull AndAlso _
                   (oldPermitInfo.PermitStatus.ID <> newPermitInfo.PermitStatusId OrElse _
                   newPermitInfo.PermitStatusId = BOPermitInfo.PermitStatusTypes.IssuedDraft OrElse _
                   newPermitInfo.PermitStatusId = BOPermitInfo.PermitStatusTypes.Re_Issue) Then

                    HistoryDO.PermitStatusId = oldPermitInfo.PermitStatus.ID
                End If
                If Not oldPermitInfo.ProgressStatusInspection Is Nothing AndAlso _
                   Not newPermitInfo.IsProgressStatusInspectionIdNull AndAlso _
                   oldPermitInfo.ProgressStatusInspection.ID <> newPermitInfo.ProgressStatusInspectionId Then
                    HistoryDO.ProgressStatusInspectionId = oldPermitInfo.ProgressStatusInspection.ID
                End If
                If Not oldPermitInfo.ProgressStatusPayment Is Nothing AndAlso _
                   Not newPermitInfo.IsProgressStatusPaymentIdNull AndAlso _
                   oldPermitInfo.ProgressStatusPayment.ID <> newPermitInfo.ProgressStatusPaymentId Then
                    HistoryDO.ProgressStatusPaymentId = oldPermitInfo.ProgressStatusPayment.ID
                End If
                If Not oldPermitInfo.ProgressStatusReferralHistory Is Nothing AndAlso _
                   Not newPermitInfo.IsProgressStatusReferralHistoryIdNull AndAlso _
                   oldPermitInfo.ProgressStatusReferralHistory.ID <> newPermitInfo.ProgressStatusReferralHistoryId Then
                    HistoryDO.ProgressStatusReferralHistoryId = oldPermitInfo.ProgressStatusReferralHistory.ID
                End If
                If Not oldPermitInfo.ProgressStatusReIssued Is Nothing AndAlso _
                   Not newPermitInfo.IsProgressStatusReIssuedIdNull AndAlso _
                   oldPermitInfo.ProgressStatusReIssued.ID <> newPermitInfo.ProgressStatusReIssuedId Then
                    HistoryDO.ProgressStatusReIssuedId = oldPermitInfo.ProgressStatusReIssued.ID
                End If
                If Not oldPermitInfo.ProgressStatusSAAdvice Is Nothing AndAlso _
                   Not newPermitInfo.IsProgressStatusSAAdviceIdNull AndAlso _
                   oldPermitInfo.ProgressStatusSAAdvice.ID <> newPermitInfo.ProgressStatusSAAdviceId Then
                    HistoryDO.ProgressStatusSAAdviceId = oldPermitInfo.ProgressStatusSAAdvice.ID
                End If
                If Not oldPermitInfo.AssignedTo Is Nothing AndAlso oldPermitInfo.AssignedTo.ID <> newPermitInfo.AssignedTo Then
                    HistoryDO.AssignedTo = oldPermitInfo.AssignedTo.ID
                End If
                If Not oldPermitInfo.CancelReason Is Nothing AndAlso _
                   Not newPermitInfo.IsCancelReasonNull AndAlso _
                   oldPermitInfo.CancelReason.ToString <> newPermitInfo.CancelReason.ToString Then
                    HistoryDO.CancelReason = oldPermitInfo.CancelReason.ToString
                End If
                If Not oldPermitInfo.CancelPendingReason Is Nothing AndAlso _
                   Not newPermitInfo.IsCancelPendingReasonNull AndAlso _
                   oldPermitInfo.CancelPendingReason.ToString <> newPermitInfo.CancelPendingReason.ToString Then
                    HistoryDO.CancelPendingReason = oldPermitInfo.CancelPendingReason.ToString
                End If
                If Not oldPermitInfo.CancelPendingDeclineReason Is Nothing AndAlso _
                Not newPermitInfo.IsCancelPendingDeclineReasonNull AndAlso _
                oldPermitInfo.CancelPendingDeclineReason.ToString <> newPermitInfo.CancelPendingDeclineReason.ToString Then
                    HistoryDO.CancelPendingDeclineReason = oldPermitInfo.CancelPendingDeclineReason.ToString
                End If
                If Not oldPermitInfo.PrintJobId Is Nothing AndAlso _
                   Not newPermitInfo.IsReportPrintJobIdNull AndAlso _
                   TypeOf oldPermitInfo.PrintJobId Is Int32 AndAlso _
                   CType(oldPermitInfo.PrintJobId, Int32) <> newPermitInfo.ReportPrintJobId Then
                    HistoryDO.ReportPrintJobId = CType(oldPermitInfo.PrintJobId, Int32)
                End If
                If Not oldPermitInfo.CoveringLetterReportId Is Nothing AndAlso _
                                 Not newPermitInfo.IsCoveringLetterReportIdNull AndAlso _
                                 TypeOf oldPermitInfo.CoveringLetterReportId Is Int32 AndAlso _
                                 CType(oldPermitInfo.CoveringLetterReportId, Int32) <> newPermitInfo.CoveringLetterReportId Then
                    HistoryDO.CoveringLetterReportId = CType(oldPermitInfo.CoveringLetterReportId, Int32)
                End If

                'create a new object to populate
                Dim NewPermitHistory As New BOPermitHistory
                NewPermitHistory = CType(NewPermitHistory.Save(HistoryDO, tran), BOPermitHistory)

                If Not NewPermitHistory.ValidationErrors Is Nothing Then
                    Return Not NewPermitHistory.ValidationErrors.HasErrors
                End If
            End If
            Return True
        End Function
#End Region

      
    End Class
End Namespace

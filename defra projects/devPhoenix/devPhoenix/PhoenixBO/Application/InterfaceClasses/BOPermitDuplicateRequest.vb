Namespace Application
    Public Class BOPermitDuplicateRequest
        Inherits BaseBO

#Region " Prelim code "
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal permitDuplicateRequestId As Int32)
            MyClass.New()
            LoadPermitDuplicateRequestId(permitDuplicateRequestId)
        End Sub

        Private Function LoadPermitDuplicateRequestId(ByVal id As Int32) As DataObjects.Entity.PermitDuplicateRequest
            Return LoadPermitDuplicateRequestId(id, Nothing)
        End Function

        Protected Function LoadPermitDuplicateRequestId(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.PermitDuplicateRequest
            Dim NewEntity As DataObjects.Entity.PermitDuplicateRequest = DataObjects.Entity.PermitDuplicateRequest.GetById(id, tran)
            If NewEntity Is Nothing Then
                Throw New RecordDoesNotExist("PermitDuplicateRequest", id)
            Else
                InitialisePermitDuplicateRequest(NewEntity, tran)
                Return NewEntity
            End If
        End Function

        Friend Overridable Sub InitialisePermitDuplicateRequest(ByVal permitDuplicateRequest As DataObjects.Entity.PermitDuplicateRequest, ByVal tran As SqlClient.SqlTransaction)
            With permitDuplicateRequest
                mPermitDuplicateRequestId = .Id
                CheckSum = .CheckSum

                mPermitInfoId = .PermitInfoId
                mDuplicateReason = New ReferenceData.BODuplicateReason(.DuplicateReasonId)
                If Not .IsDuplicateReasonDetailsNull Then mDuplicateReasonDetails = .DuplicateReasonDetails
            End With
        End Sub
#End Region

#Region " Properties "
        Public Property PermitDuplicateRequestId() As Int32
            Get
                Return mPermitDuplicateRequestId
            End Get
            Set(ByVal Value As Int32)
                mPermitDuplicateRequestId = Value
            End Set
        End Property
        Private mPermitDuplicateRequestId As Int32

        Public Property PermitInfoId() As Int32
            Get
                Return mPermitInfoId
            End Get
            Set(ByVal Value As Int32)
                mPermitInfoId = Value
            End Set
        End Property
        Private mPermitInfoId As Int32

        Public Property DuplicateReason As ReferenceData.BODuplicateReason
            Get
                Return mDuplicateReason
            End Get
            Set(Byval Value As ReferenceData.BODuplicateReason)
                mDuplicateReason = Value
            End Set
        End Property
        Private mDuplicateReason As ReferenceData.BODuplicateReason

        Public Property DuplicateReasonDetails As String
            Get
                Return mDuplicateReasonDetails
            End Get
            Set(Byval Value As String)
                mDuplicateReasonDetails = Value
            End Set
        End Property
        Private mDuplicateReasonDetails As String

        Public Property DuplicateIssueDate As Date
            Get
                Return mDuplicateIssueDate
            End Get
            Set(Byval Value As Date)
                mDuplicateIssueDate = Value
            End Set
        End Property
        Private mDuplicateIssueDate As Date

#End Region

#Region " Helper Properties "
        Private ReadOnly Property DuplicateReasonId() As Int32
            Get
                If Not mDuplicateReason Is Nothing AndAlso mDuplicateReason.ID > 0 Then
                    Return mDuplicateReason.ID
                Else
                    Return 0
                End If
            End Get
        End Property
#End Region

#Region " Save "
        Public Overloads Function Save(ByVal tran As SqlClient.SqlTransaction) As Object
            MyBase.Save()

            Dim NewPermitDuplicateRequest As New DataObjects.Entity.PermitDuplicateRequest
            Dim service As DataObjects.Service.PermitDuplicateRequestService = NewPermitDuplicateRequest.ServiceObject

            Created = (mPermitDuplicateRequestId = 0)

            If Created Then
                NewPermitDuplicateRequest = service.Insert(mPermitInfoId, _
                                                           DuplicateReasonId, _
                                                           mDuplicateReasonDetails, _
                                                           mDuplicateIssueDate, _
                                                           tran)
            Else
                NewPermitDuplicateRequest = service.Update(mPermitDuplicateRequestId, _
                                                           mPermitInfoId, _
                                                           DuplicateReasonId, _
                                                           mDuplicateReasonDetails, _
                                                           mDuplicateIssueDate, _
                                                           CheckSum, _
                                                           tran)
            End If

            'check to see if any SQL errors have occured
            If (NewPermitDuplicateRequest Is Nothing AndAlso Not DataObjects.Sprocs.LastError Is Nothing) Then
                'TODO: Use errors collection to check to see if the problem was concurrency
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveAdditionalDeclaration)
            Else
                If Created And Not NewPermitDuplicateRequest Is Nothing Then
                    mPermitDuplicateRequestId = NewPermitDuplicateRequest.Id
                End If
                Try
                    If NewPermitDuplicateRequest.CheckSum <> CheckSum Then
                        InitialisePermitDuplicateRequest(NewPermitDuplicateRequest, tran)
                    End If
                Catch ex As Exception
                    If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveAdditionalDeclaration)
                End Try
            End If

            Return Me
        End Function
#End Region

    End Class
End Namespace

Namespace Application
    Public Interface IAdditionalInformationNote
        Property Note() As String
    End Interface

    <Serializable()> _
    Public Class AdditionalInformation
        Public Sub New()

        End Sub

        Public Sub New(ByVal statusId As Int32)
            mStatusId = statusId
        End Sub

        Friend Sub New(ByVal status As BOPermitInfo.PermitStatusTypes)
            mStatusId = CType(status, Int32)
        End Sub

        Public Property StatusId() As Int32
            Get
                Return mStatusId
            End Get
            Set(ByVal Value As Int32)
                mStatusId = Value
            End Set
        End Property
        Private mStatusId As Int32

        Public Property PaymentStatus() As Object
            Get
                Return mPaymentStatus
            End Get
            Set(ByVal Value As Object)
                mPaymentStatus = Value
            End Set
        End Property
        Private mPaymentStatus As Object

        Public Property PrintJobId() As Int32
            Get
                Return mPrintJobId
            End Get
            Set(ByVal Value As Int32)
                mPrintJobId = Value
            End Set
        End Property
        Private mPrintJobId As Int32
    End Class

    Public Class AdditionalInformation_UC222_ErrorCorrection
        Inherits AdditionalInformation

        Public Sub New()
        End Sub

        Public Sub New(ByVal statusId As Int32)
            MyBase.New(statusId)
        End Sub

        Friend Sub New(ByVal status As ReferenceData.BOPermitStatus)
            MyBase.New(status.ID)
            mStatusDescription = status.Description
        End Sub

        Public Property StatusDescription() As String
            Get
                Return mStatusDescription
            End Get
            Set(ByVal Value As String)
                mStatusDescription = Value
            End Set
        End Property
        Private mStatusDescription As String
    End Class

    Public Class AdditionalInformation_UC222
        Inherits AdditionalInformation
        Implements IAdditionalInformationNote

        Public Sub New()
        End Sub

        Public Sub New(ByVal statusId As Int32)
            MyBase.New(statusId)
        End Sub

        Friend Sub New(ByVal status As BOPermitInfo.PermitStatusTypes)
            MyBase.New(status)
            If status = BOPermitInfo.PermitStatusTypes.IssuedDraft Then
                NextActionDate = Date.Now.AddMonths(6)
            End If
        End Sub

        Public Property NextActionDate() As Object
            Get
                Return mNextActionDate
            End Get
            Set(ByVal Value As Object)
                mNextActionDate = Value
            End Set
        End Property
        Private mNextActionDate As Object

        Public Property Note() As String Implements IAdditionalInformationNote.Note
            Get
                Return mNote
            End Get
            Set(ByVal Value As String)
                mNote = Value
            End Set
        End Property
        Private mNote As String
    End Class

    Public Class AdditionalInformation_UC222_Inspectorate
        Inherits AdditionalInformation
        Implements IAdditionalInformationNote

        Public Sub New()
        End Sub

        Public Sub New(ByVal statusId As Int32)
            MyBase.New(statusId)
        End Sub

        Friend Sub New(ByVal status As BOPermitInfo.PermitStatusTypes)
            MyBase.New(status)
        End Sub

        Public Property Note() As String Implements IAdditionalInformationNote.Note
            Get
                Return mNote
            End Get
            Set(ByVal Value As String)
                mNote = Value
            End Set
        End Property
        Private mNote As String

        Public Property ProgressStatusInspectionId() As Int32
            Get
                Return mProgressStatusInspectionId
            End Get
            Set(ByVal Value As Int32)
                mProgressStatusInspectionId = Value
            End Set
        End Property
        Private mProgressStatusInspectionId As Int32
    End Class

    Public Class AdditionalInformation_UC222_JNCC
        Inherits AdditionalInformation
        Implements IAdditionalInformationNote

        Public Sub New()
        End Sub

        Public Sub New(ByVal statusId As Int32)
            MyBase.New(statusId)
        End Sub

        Friend Sub New(ByVal status As BOPermitInfo.PermitStatusTypes)
            MyBase.New(status)
        End Sub

        'Public Property Advice() As String
        '    Get
        '        Return mAdvice
        '    End Get
        '    Set(ByVal Value As String)
        '        mAdvice = Value
        '    End Set
        'End Property
        'Private mAdvice As String

        Public Property ProgressStatusSAAdviceId() As Int32
            Get
                Return mProgressStatusSAAdviceId
            End Get
            Set(ByVal Value As Int32)
                mProgressStatusSAAdviceId = Value
            End Set
        End Property
        Private mProgressStatusSAAdviceId As Int32

        Public Property Note() As String Implements IAdditionalInformationNote.Note
            Get
                Return mNote
            End Get
            Set(ByVal Value As String)
                mNote = Value
            End Set
        End Property
        Private mNote As String
    End Class

    Public Class AdditionalInformation_UC222_Kew
        Inherits AdditionalInformation_UC222_JNCC

        Public Sub New()
        End Sub

        Public Sub New(ByVal statusId As Int32)
            MyBase.New(statusId)
        End Sub

        Friend Sub New(ByVal status As BOPermitInfo.PermitStatusTypes)
            MyBase.New(status)
        End Sub
    End Class

    Public Class AdditionalInformation_UC220
        Inherits AdditionalInformation

        Public Sub New()
        End Sub

        Public Sub New(ByVal statusId As Int32)
            MyBase.New(statusId)
        End Sub

        Friend Sub New(ByVal status As BOPermitInfo.PermitStatusTypes)
            MyBase.New(status)
        End Sub

        Public Property Reason() As String
            Get
                Return mReason
            End Get
            Set(ByVal Value As String)
                mReason = Value
            End Set
        End Property
        Private mReason As String
    End Class
    'PN added
    Public Class AdditionalInformation_UC222_Duplicate
        Inherits AdditionalInformation_UC212_Issue

        Public Sub New()
        End Sub

        Public Sub New(ByVal statusId As Int32)
            MyBase.New(statusId)
        End Sub

        Friend Sub New(ByVal status As BOPermitInfo.PermitStatusTypes)
            MyBase.New(status)
        End Sub

        Public Property DuplicateReasonId() As Int32
            Get
                Return mDuplicateReasonId
            End Get
            Set(ByVal Value As Int32)
                mDuplicateReasonId = Value
            End Set
        End Property
        Private mDuplicateReasonId As Int32

        Public Property DuplicateReasonDetails() As String
            Get
                Return mDuplicateReasonDetails
            End Get
            Set(ByVal Value As String)
                mDuplicateReasonDetails = Value
            End Set
        End Property
        Private mDuplicateReasonDetails As String
    End Class

    'PN added
    Public Class AdditionalInformation_UC222_ReIssue
        Inherits AdditionalInformation_UC212_Issue
        Implements IAdditionalInformationNote

        Public Property Note() As String Implements IAdditionalInformationNote.Note
            Get
                Return mNote
            End Get
            Set(ByVal Value As String)
                mNote = Value
            End Set
        End Property
        Private mNote As String

        Public Sub New()
        End Sub

        Public Sub New(ByVal statusId As Int32)
            MyBase.New(statusId)
        End Sub

        Friend Sub New(ByVal status As BOPermitInfo.PermitStatusTypes)
            MyBase.New(status)
        End Sub
    End Class

    'PN added
    Public Class AdditionalInformation_UC212_Issue
        Inherits AdditionalInformation_UC212_Authorisation

        Public Sub New()
        End Sub

        Public Sub New(ByVal statusId As Int32)
            MyBase.New(statusId)
        End Sub

        Friend Sub New(ByVal status As BOPermitInfo.PermitStatusTypes)
            MyBase.New(status)
        End Sub

        Public Property MaAddress() As String
            Get
                Return mMAAddress
            End Get
            Set(ByVal Value As String)
                mMAAddress = Value
            End Set
        End Property
        Private mMAAddress As String
    End Class

    Public Class AdditionalInformation_UC212_Authorisation
        Inherits AdditionalInformation

        Public Sub New()
        End Sub

        Public Sub New(ByVal statusId As Int32)
            MyBase.New(statusId)
        End Sub

        Public Property LetterReportId() As Int32
            Get
                Return mLetterReportId
            End Get
            Set(ByVal Value As Int32)
                mLetterReportId = Value
            End Set
        End Property
        Private mLetterReportId As Int32


        Friend Sub New(ByVal status As BOPermitInfo.PermitStatusTypes)
            MyBase.New(status)
        End Sub

    End Class

    Public Class AdditionalInformation_UC212_Rejection
        Inherits AdditionalInformation_UC212_Authorisation

        Public Sub New()
        End Sub

        Public Sub New(ByVal statusId As Int32)
            MyBase.New(statusId)
        End Sub

        Friend Sub New(ByVal status As BOPermitInfo.PermitStatusTypes)
            MyBase.New(status)
        End Sub

        Public Property RejectionReason() As RejectionReasonClass
            Get
                Return mRejectionReason
            End Get
            Set(ByVal Value As RejectionReasonClass)
                mRejectionReason = Value
            End Set
        End Property
        Private mRejectionReason As RejectionReasonClass

        <Serializable()> _
        Public Class RejectionReasonClass
            Public Sub New()

            End Sub

            Public Sub New(ByVal reason As String)
                mReason = reason
                'mPermitInfoId = permitInfoId
            End Sub

            Public Sub New(ByVal reasonId As Int32)
                mReasonId = reasonId
                '  mPermitInfoId = permitInfoId
            End Sub

            Public Property Reason() As String
                Get
                    Return mReason
                End Get
                Set(ByVal Value As String)
                    mReason = Value
                End Set
            End Property
            Private mReason As String

            Public Property PermitInfoId() As Int32
                Get
                    Return mPermitInfoId
                End Get
                Set(ByVal Value As Int32)
                    mPermitInfoId = Value
                End Set
            End Property
            Private mPermitInfoId As Int32

            Public Property ReasonId() As Int32
                Get
                    Return mReasonId
                End Get
                Set(ByVal Value As Int32)
                    mReasonId = Value
                End Set
            End Property
            Private mReasonId As Int32
        End Class

    End Class

    Public Class AdditionalInformation_UpdateSemiComplete
        Inherits AdditionalInformation
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal statusId As Int32)
            MyBase.New(statusId)
        End Sub

        Friend Sub New(ByVal status As BOPermitInfo.PermitStatusTypes)
            MyBase.New(status)
        End Sub

        Public Property PI() As BO.Application.BOPermitInfo
            Get
                Return mPI
            End Get
            Set(ByVal Value As BO.Application.BOPermitInfo)
                mPI = Value
            End Set
        End Property
        Private mPI As BO.Application.BOPermitInfo
    End Class

    Public Class AdditionalInformation_ProgressAllowed
        Inherits AdditionalInformation
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal statusId As Int32)
            MyBase.New(statusId)
        End Sub

        Friend Sub New(ByVal status As BOPermitInfo.PermitStatusTypes)
            MyBase.New(status)
        End Sub
    End Class

    Public Class AdditionalInformation_SubmittedByCustomer
        Inherits AdditionalInformation
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal statusId As Int32)
            MyBase.New(statusId)
        End Sub

        Friend Sub New(ByVal status As BOPermitInfo.PermitStatusTypes)
            MyBase.New(status)
        End Sub
    End Class

    Public Class AdditionalInformation_Default
        Inherits AdditionalInformation
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal statusId As Int32)
            MyBase.New(statusId)
        End Sub

        Friend Sub New(ByVal status As BOPermitInfo.PermitStatusTypes)
            MyBase.New(status)
        End Sub
    End Class

    Public Class AdditionalInformation_UC203
        Inherits AdditionalInformation

        'notes:
        'if case officer then the submitted date can be changed, othereise just use the default
        Public Sub New()
            MyBase.New()
            mSubmittedDate = Date.Today
        End Sub

        Public Sub New(ByVal statusId As Int32)
            MyBase.New(statusId)
            mSubmittedDate = Date.Today
        End Sub

        Friend Sub New(ByVal status As BOPermitInfo.PermitStatusTypes)
            MyBase.New(status)
            mSubmittedDate = Date.Today
        End Sub

        Public Property SubmittedDate() As Date
            Get
                Return mSubmittedDate
            End Get
            Set(ByVal Value As Date)
                mSubmittedDate = Value
            End Set
        End Property
        Private mSubmittedDate As Date
    End Class

    Public Class AdditionalInformation_UC222_TeamLeader
        Inherits AdditionalInformation
        Implements IAdditionalInformationNote

        Public Sub New()
        End Sub

        Public Sub New(ByVal statusId As Int32)
            MyBase.New(statusId)
        End Sub

        Friend Sub New(ByVal status As BOPermitInfo.PermitStatusTypes)
            MyBase.New(status)
        End Sub

        Public Property Note() As String Implements IAdditionalInformationNote.Note
            Get
                Return mNote
            End Get
            Set(ByVal Value As String)
                mNote = Value
            End Set
        End Property
        Private mNote As String
    End Class

    Public Class AdditionalInformation_UC216_Unused
        Inherits AdditionalInformation

        Public Sub New()
        End Sub

        Public Sub New(ByVal statusId As Int32)
            MyBase.New(statusId)
        End Sub

        Friend Sub New(ByVal status As BOPermitInfo.PermitStatusTypes)
            MyBase.New(status)
        End Sub

        Public Property DateReturned() As Object
            Get
                Return mDateReturned
            End Get
            Set(ByVal Value As Object)
                mDateReturned = Value
            End Set
        End Property
        Private mDateReturned As Object
    End Class

    Public Class AdditionalInformation_UC216_Used
        Inherits AdditionalInformation

        Public Sub New()
        End Sub

        Public Sub New(ByVal statusId As Int32)
            MyBase.New(statusId)
        End Sub

        Friend Sub New(ByVal status As BOPermitInfo.PermitStatusTypes)
            MyBase.New(status)
        End Sub

        Public Property BillOfLading() As String
            Get
                Return mBillOfLading
            End Get
            Set(ByVal Value As String)
                mBillOfLading = Value
            End Set
        End Property
        Private mBillOfLading As String

        Public Property CustomsDocumentType() As Object
            Get
                Return mCustomsDocumentType
            End Get
            Set(ByVal Value As Object)
                mCustomsDocumentType = Value
            End Set
        End Property
        Private mCustomsDocumentType As Object

        Public Property CustomsDocumentReference() As String
            Get
                Return mCustomsDocumentReference
            End Get
            Set(ByVal Value As String)
                mCustomsDocumentReference = Value
            End Set
        End Property
        Private mCustomsDocumentReference As String

        Public Property ImportExportDate() As Object
            Get
                Return mImportExportDate
            End Get
            Set(ByVal Value As Object)
                mImportExportDate = Value
            End Set
        End Property
        Private mImportExportDate As Object

        Public Property ActualQuantity() As Object
            Get
                Try
                    If mActualQuantity.ToString = "" Then mActualQuantity = Nothing
                Catch ex As Exception

                End Try
                Return mActualQuantity
            End Get
            Set(ByVal Value As Object)
                mActualQuantity = Value
            End Set
        End Property
        Private mActualQuantity As Object

        Public Property ActualMass() As Object
            Get
                Try
                    If mActualMass.ToString = "" Then mActualMass = Nothing
                Catch ex As Exception

                End Try
                Return mActualMass
            End Get
            Set(ByVal Value As Object)
                mActualMass = Value
            End Set
        End Property
        Private mActualMass As Object

        Public Property UOMId() As Object
            Get
                If CType(mUOMId, Int32) = 0 Then
                    Return Nothing
                Else
                    Return mUOMId
                End If
            End Get
            Set(ByVal Value As Object)
                mUOMId = Value
            End Set
        End Property
        Private mUOMId As Object

        Public Property NumberDOA() As Object
            Get
                Return mNumberDOA
            End Get
            Set(ByVal Value As Object)
                mNumberDOA = Value
            End Set
        End Property
        Private mNumberDOA As Object

        Public Property AuthorisedBy() As String
            Get
                Return mAuthorisedBy
            End Get
            Set(ByVal Value As String)
                mAuthorisedBy = Value
            End Set
        End Property
        Private mAuthorisedBy As String

        Public Property AuthorisedDate() As Object
            Get
                Return mAuthorisedDate
            End Get
            Set(ByVal Value As Object)
                mAuthorisedDate = Value
            End Set
        End Property
        Private mAuthorisedDate As Object

        Public Property EndorsedPermitReceiptDate() As Object
            Get
                Return mEndorsedPermitReceiptDate
            End Get
            Set(ByVal Value As Object)
                mEndorsedPermitReceiptDate = Value
            End Set
        End Property
        Private mEndorsedPermitReceiptDate As Object
    End Class
End Namespace

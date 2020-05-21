Namespace Application.Bird
    Public Class ProgressBirdStatusInfo
        <Serializable()> _
        Public Class BaseBirdStatusChange
            Public Sub New()
            End Sub

            Friend Sub New(ByVal status As BOPermitInfo.PermitStatusTypes)
                SetStatus(status)
            End Sub

            Public Property Status() As BOPermitInfo.PermitStatusTypes
                Get
                    Return mStatus
                End Get
                Set(ByVal Value As BOPermitInfo.PermitStatusTypes)
                    mStatus = Value
                End Set
            End Property
            Private mStatus As BOPermitInfo.PermitStatusTypes

            Public Property AssignedTo() As Common.AssignedToList
                Get
                    Return mAssignedTo
                End Get
                Set(ByVal Value As Common.AssignedToList)
                    mAssignedTo = Value
                End Set
            End Property
            Private mAssignedTo As Common.AssignedToList

            Protected Sub SetStatus(ByVal newStatus As BOPermitInfo.PermitStatusTypes)
                mStatus = newStatus
            End Sub
        End Class

        Public Class BirdStatusChange_Registered
            Inherits BaseBirdStatusChange
            Implements IReportType

            Public Sub New()
                MyBase.New()
            End Sub

            Friend Sub New(ByVal status As BOPermitInfo.PermitStatusTypes)
                MyBase.New(status)
            End Sub

            Public Property ReportInfo() As PrintResults Implements IReportType.ReportInfo
                Get
                    Return mReportInfo
                End Get
                Set(ByVal Value As PrintResults)
                    mReportInfo = Value
                End Set
            End Property
            Private mReportInfo As PrintResults
        End Class

        <Serializable()> _
        Public Class BirdStatusChange_Referred
            Inherits BaseBirdStatusChange
            Implements Application.IAdditionalInformationNote

            Public Sub New()
                MyBase.New()
            End Sub

            Friend Sub New(ByVal status As BOPermitInfo.PermitStatusTypes)
                MyBase.New(status)
            End Sub

            Public Property NextActionDate() As Date
                Get
                    Return mNextActionDate
                End Get
                Set(ByVal Value As Date)
                    mNextActionDate = Value
                End Set
            End Property
            Private mNextActionDate As Date

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

        Public Class BirdStatusChange_DORReturned
            Inherits BaseBirdStatusChange

            Public Sub New()
                MyBase.New()
            End Sub

            Friend Sub New(ByVal status As BOPermitInfo.PermitStatusTypes)
                MyBase.New(status)
            End Sub

            'Public Property Application() As Registration.BirdRegistration
            '    Get
            '        Return mApplication
            '    End Get
            '    Set(ByVal Value As Registration.BirdRegistration)
            '        mApplication = Value
            '    End Set
            'End Property
            'Private mApplication As Registration.BirdRegistration
        End Class

        Public Class BirdStatusChange_Declined
            Inherits BaseBirdStatusChange
            Implements IReportType
            Implements Application.IAdditionalInformationNote

            Public Sub New()
                MyBase.New()
            End Sub

            Friend Sub New(ByVal status As BOPermitInfo.PermitStatusTypes)
                MyBase.New(status)
            End Sub

            Public Property DeclineReason() As String Implements IAdditionalInformationNote.Note
                Get
                    Return mDeclineReason
                End Get
                Set(ByVal Value As String)
                    mDeclineReason = Value
                End Set
            End Property
            Private mDeclineReason As String

            Public Property ReportInfo() As PrintResults Implements IReportType.ReportInfo
                Get
                    Return mReportInfo
                End Get
                Set(ByVal Value As PrintResults)
                    mReportInfo = Value
                End Set
            End Property
            Private mReportInfo As PrintResults
        End Class

        Public Class BirdStatusChange_Cancel
            Inherits BaseBirdStatusChange
            Implements IReportType, Application.IAdditionalInformationNote

            Public Sub New()
                MyBase.New()
            End Sub

            Friend Sub New(ByVal status As BOPermitInfo.PermitStatusTypes)
                MyBase.New(status)
            End Sub

            Public Property CancellationReason() As String Implements IAdditionalInformationNote.Note
                Get
                    Return mCancellationReason
                End Get
                Set(ByVal Value As String)
                    mCancellationReason = Value
                End Set
            End Property
            Private mCancellationReason As String

            Public Property ReportInfo() As PrintResults Implements IReportType.ReportInfo
                Get
                    Return mReportInfo
                End Get
                Set(ByVal Value As PrintResults)
                    mReportInfo = Value
                End Set
            End Property
            Private mReportInfo As PrintResults
        End Class

        Public Class BirdStatusChange_RefuseCancel
            Inherits BaseBirdStatusChange
            Implements IReportType
            Implements Application.IAdditionalInformationNote

            Public Sub New()
                MyBase.New()
            End Sub

            Friend Sub New(ByVal status As BOPermitInfo.PermitStatusTypes)
                MyBase.New(status)
            End Sub

            Public Property CancellationRefuseReason() As String Implements IAdditionalInformationNote.Note
                Get
                    Return mCancellationRefuseReason
                End Get
                Set(ByVal Value As String)
                    mCancellationRefuseReason = Value
                End Set
            End Property
            Private mCancellationRefuseReason As String

            Public Property ReportInfo() As PrintResults Implements IReportType.ReportInfo
                Get
                    Return mReportInfo
                End Get
                Set(ByVal Value As PrintResults)
                    mReportInfo = Value
                End Set
            End Property
            Private mReportInfo As PrintResults
        End Class

        Public Class BirdStatusChange_Authorise
            Inherits BaseBirdStatusChange
            Implements Application.IAdditionalInformationNote

            Public Sub New()
                MyBase.New()
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

        Public Class BirdStatusChange_Issued
            Inherits BaseBirdStatusChange
            Implements IReportType

            Public Sub New()
                MyBase.New()
            End Sub

            Friend Sub New(ByVal status As BOPermitInfo.PermitStatusTypes)
                MyBase.New(status)
            End Sub

            Public Property ReportInfo() As PrintResults Implements IReportType.ReportInfo
                Get
                    Return mReportInfo
                End Get
                Set(ByVal Value As PrintResults)
                    mReportInfo = Value
                End Set
            End Property
            Private mReportInfo As PrintResults
        End Class

        Public Class BirdStatusChange_ProgressAllowed
            Inherits BaseBirdStatusChange
            Implements Application.IAdditionalInformationNote

            Public Sub New()
                MyBase.New()
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

        Public Interface IReportType
            Property ReportInfo() As PrintResults
        End Interface

        <Serializable()> _
        Public Class PrintResults
            Public Sub New()
            End Sub

            Friend Sub New(ByVal printJobId As Int32, ByVal reportId As Int32)
                mReportId = reportId
                mPrintJobId = printJobId
            End Sub

            Friend Sub New(ByVal printJobId As Int32)
                MyClass.New(printJobId, 0)
            End Sub

            Public Property ReportId() As Int32
                Get
                    Return mReportId
                End Get
                Set(ByVal Value As Int32)
                    mReportId = Value
                End Set
            End Property
            Private mReportId As Int32

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

        '<Serializable()> _
        'Public Enum ReportTypes
        '    BirdRegDocCriteria = 20
        '    RegistrationRefusalLetterCriteria = 53
        'End Enum
    End Class
End Namespace
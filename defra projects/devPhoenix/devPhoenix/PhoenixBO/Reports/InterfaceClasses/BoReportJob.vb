
Imports System.text
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do
Imports uk.gov.defra.Phoenix.DO.DataObjects

Namespace ReportData
    <Serializable()> Public Class BoReportJob
        Inherits BaseBO
        Implements IReportJob

        Public Sub New()
            MyBase.new()

        End Sub

        Public Sub New(ByVal reportJob As DataObjects.Views.Entity.ReportJobs)
            MyBase.new()
            InitialiseReportPrinter(reportJob)
        End Sub

        Private Sub InitialiseReportPrinter(ByVal reportJob As DataObjects.Views.Entity.ReportJobs)

            mReportId = reportJob.ReportId()
            mReportPrinterId = reportJob.ReportPrinterId()
            mPrintSequence = reportJob.PrintSequence()
            mAuthorisedBy = reportJob.AuthorisedBy()
            mAuthorisedDate = reportJob.AuthorisedDate()
            mBOTypeName = reportJob.BOTypeName()
            mStapleStartPage = reportJob.StapleStartPage()
            mStapleEndPage = reportJob.StapleEndPage()
            mSearchReference = reportJob.SearchReference()
            mPausedBy = reportJob.PausedBy()
            mStatusDescription_ToolTip = ""
            mStatusDescription = "Pause"
            If Not reportJob.IsPausedDateNull Then
                mPausedDate = reportJob.PausedDate()
                mStatusDescription_ToolTip = "Paused By - " & mPausedBy & " " & CType(mPausedDate, DateTime).ToString
                mStatusDescription = "Resume"
            End If
            If Not reportJob.IsPrintingDateNull Then mPrintingDate = reportJob.PrintingDate()
            If Not reportJob.IsPrintedDateNull Then mPrintedDate = reportJob.PrintedDate
            mDeletedBy = reportJob.DeletedBy()
            mDeletedByDate = ""
            mDeleteStatus = "Delete"
            If Not reportJob.IsDeletedDateNull Then
                mDeleteStatus = "UnDelete"
                mDeletedDate = reportJob.DeletedDate
                mDeletedByDate = mDeletedBy & " " & CType(mDeletedDate, DateTime).ToString
            End If
            mLastStatusMessage = reportJob.LastStatusMessage()
            mStapleOff = reportJob.StapleOff()
            mReportAuthorisedQId = reportJob.ReportAuthorisedQId()
            mSize = reportJob.Size()
            mStapleBatch = reportJob.StapleBatch()
            mDescription = reportJob.PrintJobDescription & " " & reportJob.ReporTypeDescription
            mDescription_NavigateUrl = "/Modules/Main/CITES/Reports/ReportViewer/ReportViewer.aspx?ReportId=" & reportJob.ReportId.ToString
            mMoveToTop = "Move to Top"
            mAuthorisedByDate = mAuthorisedBy & " " & CType(mAuthorisedDate, DateTime).ToString

            mReQueueStatus = ""
            If Not reportJob.IsPrintingDateNull Then
                'If reportJob.IsDeletedDateNull And reportJob.IsPrintedDateNull Then
                If reportJob.IsDeletedDateNull Then
                    mReQueueStatus = "ReQueue"
                End If
            End If
        End Sub


        Public Property AuthorisedBy() As String Implements IReportJob.AuthorisedBy
            Get
                Return mAuthorisedBy
            End Get
            Set(ByVal Value As String)
                mAuthorisedBy = Value
            End Set
        End Property
        Private mAuthorisedBy As String

        Public Property AuthorisedDate() As Object Implements IReportJob.AuthorisedDate
            Get
                Return mAuthorisedDate
            End Get
            Set(ByVal Value As Object)
                mAuthorisedDate = Value
            End Set
        End Property
        Private mAuthorisedDate As Object

        Public Property BOTypeName() As String Implements IReportJob.BOTypeName
            Get
                Return mBOTypeName
            End Get
            Set(ByVal Value As String)
                mBOTypeName = Value
            End Set
        End Property
        Private mBOTypeName As String

        Public Property DeletedBy() As String Implements IReportJob.DeletedBy
            Get
                Return mDeletedBy
            End Get
            Set(ByVal Value As String)
                mDeletedBy = Value
            End Set
        End Property
        Private mDeletedBy As String

        Public Property DeletedDate() As Object Implements IReportJob.DeletedDate
            Get
                Return mDeletedDate
            End Get
            Set(ByVal Value As Object)
                mDeletedDate = Value
            End Set
        End Property
        Private mDeletedDate As Object

        Public Property Description() As String Implements IReportJob.Description
            Get
                Return mDescription
            End Get
            Set(ByVal Value As String)
                mDescription = Value
            End Set
        End Property
        Private mDescription As String

        Public Property LastStatusMessage() As String Implements IReportJob.LastStatusMessage
            Get
                Return mLastStatusMessage
            End Get
            Set(ByVal Value As String)
                mLastStatusMessage = Value
            End Set
        End Property
        Private mLastStatusMessage As String

        Public Property PausedBy() As String Implements IReportJob.PausedBy
            Get
                Return mPausedBy
            End Get
            Set(ByVal Value As String)
                mPausedBy = Value
            End Set
        End Property
        Private mPausedBy As String

        Public Property PausedDate() As Object Implements IReportJob.PausedDate
            Get
                Return mPausedDate
            End Get
            Set(ByVal Value As Object)
                mPausedDate = Value
            End Set
        End Property
        Private mPausedDate As Object

        Public Property PrintedDate() As Object Implements IReportJob.PrintedDate
            Get
                Return mPrintedDate
            End Get
            Set(ByVal Value As Object)
                mPrintedDate = Value
            End Set
        End Property
        Private mPrintedDate As Object

        Public Property PrintingDate() As Object Implements IReportJob.PrintingDate
            Get
                Return mPrintingDate
            End Get
            Set(ByVal Value As Object)
                mPrintingDate = Value
            End Set
        End Property
        Private mPrintingDate As Object

        Public Property PrintSequence() As Integer Implements IReportJob.PrintSequence
            Get
                Return mPrintSequence
            End Get
            Set(ByVal Value As Integer)
                mPrintSequence = Value
            End Set
        End Property
        Private mPrintSequence As Int32

        Public Property ReportAuthorisedQId() As Integer Implements IReportJob.ReportAuthorisedQId
            Get
                Return mReportAuthorisedQId
            End Get
            Set(ByVal Value As Integer)
                mReportAuthorisedQId = Value
            End Set
        End Property
        Private mReportAuthorisedQId As Int32

        Public Property ReportId() As Integer Implements IReportJob.ReportId
            Get
                Return mReportId
            End Get
            Set(ByVal Value As Integer)
                mReportId = Value
            End Set
        End Property
        Private mReportId As Int32

        Public Property ReportPrinterId() As Integer Implements IReportJob.ReportPrinterId
            Get
                Return mReportPrinterId
            End Get
            Set(ByVal Value As Integer)
                mReportPrinterId = Value
            End Set
        End Property
        Private mReportPrinterId As Int32

        Public Property SearchReference() As String Implements IReportJob.SearchReference
            Get
                Return mSearchReference
            End Get
            Set(ByVal Value As String)
                mSearchReference = Value
            End Set
        End Property
        Private mSearchReference As String

        Public Property Size() As Integer Implements IReportJob.Size
            Get
                Return mSize
            End Get
            Set(ByVal Value As Integer)
                mSize = Value
            End Set
        End Property
        Private mSize As Int32

        Public Property StapleBatch() As Integer Implements IReportJob.StapleBatch
            Get
                Return mStapleBatch
            End Get
            Set(ByVal Value As Integer)
                mStapleBatch = Value
            End Set
        End Property
        Private mStapleBatch As Int32

        Public Property StapleEndPage() As Integer Implements IReportJob.StapleEndPage
            Get
                Return mStapleEndPage
            End Get
            Set(ByVal Value As Integer)
                mStapleEndPage = Value
            End Set
        End Property
        Private mStapleEndPage As Int32

        Public Property StapleOff() As Boolean Implements IReportJob.StapleOff
            Get
                Return mStapleOff
            End Get
            Set(ByVal Value As Boolean)
                mStapleOff = Value
            End Set
        End Property
        Private mStapleOff As Boolean

        Public Property StapleStartPage() As Integer Implements IReportJob.StapleStartPage
            Get
                Return mStapleStartPage
            End Get
            Set(ByVal Value As Integer)
                mStapleStartPage = Value
            End Set
        End Property
        Private mStapleStartPage As Int32

        Public Property Description_NavigateUrl() As String Implements IReportJob.Description_NavigateUrl
            Get
                Return mDescription_NavigateUrl
            End Get
            Set(ByVal Value As String)
                mDescription_NavigateUrl = Value
            End Set
        End Property
        Private mDescription_NavigateUrl As String

        Public Property MoveToTop() As String Implements IReportJob.MoveToTop
            Get
                Return mMoveToTop
            End Get
            Set(ByVal Value As String)
                mMoveToTop = Value
            End Set
        End Property
        Private mMoveToTop As String

        Public Property AuthorisedByDate() As String Implements IReportJob.AuthorisedByDate
            Get
                Return mAuthorisedByDate
            End Get
            Set(ByVal Value As String)
                mAuthorisedByDate = Value
            End Set
        End Property
        Private mAuthorisedByDate As String

        Public Property DeletedByDate() As String Implements IReportJob.DeletedByDate
            Get
                Return mDeletedByDate
            End Get
            Set(ByVal Value As String)
                mDeletedByDate = Value
            End Set
        End Property
        Private mDeletedByDate As String

        Public Property StatusDescription() As String Implements IReportJob.StatusDescription
            Get
                Return mStatusDescription
            End Get
            Set(ByVal Value As String)
                mStatusDescription = Value
            End Set
        End Property
        Private mStatusDescription As String


        Public Property StatusDescription_ToolTip() As String Implements IReportJob.StatusDescription_ToolTip
            Get
                Return mStatusDescription_ToolTip
            End Get
            Set(ByVal Value As String)
                mStatusDescription_ToolTip = Value
            End Set
        End Property
        Private mStatusDescription_ToolTip As String

        Public Property DeleteStatus() As String Implements IReportJob.DeleteStatus
            Get
                Return mDeleteStatus
            End Get
            Set(ByVal Value As String)
                mDeleteStatus = Value
            End Set
        End Property
        Private mDeleteStatus As String

        Public Property ReQueueStatus() As String Implements IReportJob.ReQueueStatus
            Get
                Return mReQueueStatus
            End Get
            Set(ByVal Value As String)
                mReQueueStatus = Value
            End Set
        End Property
        Private mReQueueStatus As String
    End Class
End Namespace
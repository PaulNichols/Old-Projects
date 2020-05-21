Imports System.text
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do
Imports uk.gov.defra.Phoenix.DO.DataObjects

Namespace ReportData
    <Serializable()> Public Class BoReportAuthorisedPrintJob
        Inherits BaseBO
        Implements IReportAuthorisedPrintJob

        Public Sub New()
            MyBase.new()

        End Sub

        Public Sub New(ByVal ReportPrinterId As Int32)
            MyBase.new()
            InitialiseReportPrinter(ReportPrinterId)
        End Sub

        Private Sub InitialiseReportPrinter(ByVal reportPrinterId As Int32)


            Dim printingPrintJobs As DataObjects.Views.Collection.ReportPrintingPrintJobBoundCollection
            printingPrintJobs = DataObjects.Views.Service.ReportPrintingPrintJobService.ReportPrintingPrintJob(reportPrinterId)

            mIsAuthorised = False
            If printingPrintJobs.Count = 0 Then

                Dim results As DataObjects.Views.Collection.ReportAuthorisedPrintJobBoundCollection
                results = DataObjects.Views.Service.ReportAuthorisedPrintJobService.ReportAuthorisedPrintJob(reportPrinterId)

                If results.Count = 1 Then

                    mIsAuthorised = True
                    mReportAuthorisedQId = results.Item(0).ReportAuthorisedQId
                    mAuthorisedBy = results.Item(0).AuthorisedBy
                    mAuthorisedDate = results.Item(0).AuthorisedDate
                    mBOTypeName = results.Item(0).BOTypeName
                    mPrintSequence = results.Item(0).PrintSequence
                    mReportId = results.Item(0).ReportId
                    mReportPrinterId = results.Item(0).ReportPrinterId
                    mSearchReference = results.Item(0).SearchReference
                    mStapleEndPage = results.Item(0).StapleEndPage
                    mStapleStartPage = results.Item(0).StapleStartPage
                    mStapleBatch = results.Item(0).StapleBatch
                    mDeletedBy = results.Item(0).DeletedBy
                    If Not results.Item(0).IsDeletedDateNull Then mDeletedDate = results.Item(0).DeletedDate
                    mLastStatusMessage = results.Item(0).LastStatusMessage
                    mPausedBy = results.Item(0).PausedBy
                    If Not results.Item(0).IsPausedDateNull Then mPausedDate = results.Item(0).PausedDate
                    If Not results.Item(0).IsPrintedDateNull Then mPrintedDate = results.Item(0).PrintedDate
                    If Not results.Item(0).IsPrintingDateNull Then mPrintingDate = results.Item(0).PrintingDate
                    mStapleOff = results.Item(0).StapleOff

                    ' Convert Image Output to Byte Array
                    Dim output As String = results.Item(0).Image
                    Dim unicodeEncoding As unicodeEncoding = New unicodeEncoding
                    mReportOutput = unicodeEncoding.GetBytes(output)
                    mReportOutput = Convert.FromBase64String(output)

                End If

            End If

        End Sub


        Public Sub UpdateReportAuthorisedQ()

            Dim reportAuthorisedQ As New BoReportAuthorisedQ

            reportAuthorisedQ.ReportAuthorisedQId = mReportAuthorisedQId
            reportAuthorisedQ.ReportId = mReportId
            reportAuthorisedQ.ReportPrinterId = mReportPrinterId
            reportAuthorisedQ.PrintSequence = mPrintSequence
            reportAuthorisedQ.PausedBy = mPausedBy
            reportAuthorisedQ.PausedDate = mPausedDate
            reportAuthorisedQ.AuthorisedBy = mAuthorisedBy
            reportAuthorisedQ.AuthorisedDate = mAuthorisedDate
            reportAuthorisedQ.PrintingDate = mPrintingDate
            reportAuthorisedQ.PrintedDate = mPrintedDate
            reportAuthorisedQ.DeletedBy = mDeletedBy
            reportAuthorisedQ.DeletedDate = mDeletedDate
            reportAuthorisedQ.LastStatusMessage = mLastStatusMessage
            reportAuthorisedQ.StapleOff = mStapleOff

            reportAuthorisedQ.Save()


        End Sub


        Public Property AuthorisedBy() As String Implements IReportAuthorisedPrintJob.AuthorisedBy
            Get
                Return mAuthorisedBy
            End Get
            Set(ByVal Value As String)
                mAuthorisedBy = Value
            End Set
        End Property
        Private mAuthorisedBy As String = ""

        Public Property AuthorisedDate() As Date Implements IReportAuthorisedPrintJob.AuthorisedDate
            Get
                Return mAuthorisedDate
            End Get
            Set(ByVal Value As Date)
                mAuthorisedDate = Value
            End Set
        End Property
        Private mAuthorisedDate As Date = Nothing

        Public Property BOTypeName() As String Implements IReportAuthorisedPrintJob.BOTypeName
            Get
                Return mBOTypeName
            End Get
            Set(ByVal Value As String)
                mBOTypeName = Value
            End Set
        End Property
        Private mBOTypeName As String = ""

        Public Property PrintSequence() As Integer Implements IReportAuthorisedPrintJob.PrintSequence
            Get
                Return mPrintSequence
            End Get
            Set(ByVal Value As Integer)
                mPrintSequence = Value
            End Set
        End Property
        Private mPrintSequence As Int32

        Public Property ReportId() As Integer Implements IReportAuthorisedPrintJob.ReportId
            Get
                Return mReportId
            End Get
            Set(ByVal Value As Integer)
                mReportId = Value
            End Set
        End Property
        Private mReportId As Int32

        Public Property ReportOutput() As Byte() Implements IReportAuthorisedPrintJob.ReportOutput
            Get
                Return mReportOutput
            End Get
            Set(ByVal Value() As Byte)
                ReportOutput = Value
            End Set
        End Property
        Private mReportOutput() As Byte

        Public Property ReportPrinterId() As Integer Implements IReportAuthorisedPrintJob.ReportPrinterId
            Get
                Return mReportPrinterId
            End Get
            Set(ByVal Value As Integer)
                mReportPrinterId = Value
            End Set
        End Property
        Private mReportPrinterId As Int32

        Public Property SearchReference() As String Implements IReportAuthorisedPrintJob.SearchReference
            Get
                Return mSearchReference
            End Get
            Set(ByVal Value As String)
                mSearchReference = Value
            End Set
        End Property
        Private mSearchReference As String

        Public Property StapleEndPage() As Integer Implements IReportAuthorisedPrintJob.StapleEndPage
            Get
                Return mStapleEndPage
            End Get
            Set(ByVal Value As Integer)
                mStapleEndPage = Value
            End Set
        End Property
        Private mStapleEndPage As Int32

        Public Property StapleStartPage() As Integer Implements IReportAuthorisedPrintJob.StapleStartPage
            Get
                Return mStapleStartPage
            End Get
            Set(ByVal Value As Integer)
                mStapleStartPage = Value
            End Set
        End Property
        Private mStapleStartPage As Int32

        Public Property IsAuthorised() As Boolean Implements IReportAuthorisedPrintJob.IsAuthorised
            Get
                Return mIsAuthorised
            End Get
            Set(ByVal Value As Boolean)
                mIsAuthorised = Value
            End Set
        End Property
        Private mIsAuthorised As Boolean

        Public Property DeletedBy() As String Implements IReportAuthorisedPrintJob.DeletedBy
            Get
                Return mDeletedBy
            End Get
            Set(ByVal Value As String)
                mDeletedBy = Value
            End Set
        End Property
        Private mDeletedBy As String

        Public Property DeletedDate() As Object Implements IReportAuthorisedPrintJob.DeletedDate
            Get
                Return mDeletedDate
            End Get
            Set(ByVal Value As Object)
                mDeletedDate = Value
            End Set
        End Property
        Private mDeletedDate As Object

        Public Property LastStatusMessage() As String Implements IReportAuthorisedPrintJob.LastStatusMessage
            Get
                Return mLastStatusMessage
            End Get
            Set(ByVal Value As String)
                mLastStatusMessage = Value
            End Set
        End Property
        Private mLastStatusMessage As String

        Public Property PausedBy() As String Implements IReportAuthorisedPrintJob.PausedBy
            Get
                Return mPausedBy
            End Get
            Set(ByVal Value As String)
                mPausedBy = Value
            End Set
        End Property
        Private mPausedBy As String

        Public Property PausedDate() As Object Implements IReportAuthorisedPrintJob.PausedDate
            Get
                Return mPausedDate
            End Get
            Set(ByVal Value As Object)
                mPausedDate = Value
            End Set
        End Property
        Private mPausedDate As Object = Nothing

        Public Property PrintedDate() As Object Implements IReportAuthorisedPrintJob.PrintedDate
            Get
                Return mPrintedDate
            End Get
            Set(ByVal Value As Object)
                mPrintedDate = Value
            End Set
        End Property
        Private mPrintedDate As Object

        Public Property PrintingDate() As Object Implements IReportAuthorisedPrintJob.PrintingDate
            Get
                Return mPrintingDate
            End Get
            Set(ByVal Value As Object)
                mPrintingDate = Value
            End Set
        End Property
        Private mPrintingDate As Object

        Public Property StapleOff() As Boolean Implements IReportAuthorisedPrintJob.StapleOff
            Get
                Return mStapleOff
            End Get
            Set(ByVal Value As Boolean)
                mStapleOff = Value
            End Set
        End Property
        Private mStapleOff As Boolean

        Public Property ReportAuthorisedQId() As Integer Implements IReportAuthorisedPrintJob.ReportAuthorisedQId
            Get
                Return mReportAuthorisedQId
            End Get
            Set(ByVal Value As Integer)
                mReportAuthorisedQId = Value
            End Set
        End Property
        Private mReportAuthorisedQId As Int32


        Public Property StapleBatch() As Integer Implements IReportAuthorisedPrintJob.StapleBatch
            Get
                Return mStapleBatch
            End Get
            Set(ByVal Value As Integer)
                mStapleBatch = Value
            End Set
        End Property
        Private mStapleBatch As Int32
    End Class
End Namespace
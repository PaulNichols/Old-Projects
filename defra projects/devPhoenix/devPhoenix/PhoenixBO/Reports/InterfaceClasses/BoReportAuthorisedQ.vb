Imports System.text
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do
Imports uk.gov.defra.Phoenix.DO.DataObjects

Namespace ReportData
    <Serializable()> Public Class BoReportAuthorisedQ
        Inherits BaseBO
        Implements IReportAuthorisedQ

        Public Sub New()
            MyBase.new()

        End Sub

        Public Sub New(ByVal reportAuthorisedQId As Int32)
            MyBase.new()
            InitialiseReportAuthoriseQ(reportAuthorisedQId)
        End Sub


        Private Sub InitialiseReportAuthoriseQ(ByVal reportAuthorisedQId As Int32)

            'Dim reportAuthorisedQ As New DataObjects.Entity.ReportAuthorisedQ
            Dim reportAuthorisedQ As DataObjects.Entity.ReportAuthorisedQ = reportAuthorisedQ.GetById(reportAuthorisedQId)

            CheckSum = reportAuthorisedQ.CheckSum
            mReportAuthorisedQId = reportAuthorisedQ.ReportAuthorisedQId
            mAuthorisedBy = reportAuthorisedQ.AuthorisedBy
            mAuthorisedDate = reportAuthorisedQ.AuthorisedDate
            mPrintSequence = reportAuthorisedQ.PrintSequence
            mReportId = reportAuthorisedQ.ReportId
            mReportPrinterId = reportAuthorisedQ.ReportPrinterId
            mDeletedBy = reportAuthorisedQ.DeletedBy
            If Not reportAuthorisedQ.IsDeletedDateNull Then mDeletedDate = reportAuthorisedQ.DeletedDate
            mLastStatusMessage = reportAuthorisedQ.LastStatusMessage
            mPausedBy = reportAuthorisedQ.PausedBy
            If Not reportAuthorisedQ.IsPausedDateNull Then mPausedDate = reportAuthorisedQ.PausedDate
            If Not reportAuthorisedQ.IsPrintedDateNull Then mPrintedDate = reportAuthorisedQ.PrintedDate
            If Not reportAuthorisedQ.IsPrintingDateNull Then mPrintingDate = reportAuthorisedQ.PrintingDate
            mStapleOff = reportAuthorisedQ.StapleOff

        End Sub

        Public Overrides Function Save() As BaseBO

            Dim reportAuthorisedQ As New DataObjects.Entity.ReportAuthorisedQ
            Dim reportAuthorisedQService As DataObjects.Service.ReportAuthorisedQService = reportAuthorisedQ.ServiceObject

            Created = (mReportAuthorisedQId = 0)

            ' Clear any errors from stack before we start
            DataObjects.Sprocs.LastError = Nothing

            If Created Then

                'Insert ReportAuthorisedQ table
                reportAuthorisedQService.Insert(mReportId, _
                mReportPrinterId, _
                mPrintSequence, _
                mPausedBy, _
                mPausedDate, _
                mAuthorisedBy, _
                mAuthorisedDate, _
                mPrintingDate, _
                mPrintedDate, _
                mDeletedBy, _
                mDeletedDate, _
                mLastStatusMessage, _
                mStapleOff, _
                Nothing)

            Else

                'Update ReportAuthorisedQ table
                reportAuthorisedQ = reportAuthorisedQService.Update(mReportAuthorisedQId, _
                mReportId, _
                mReportPrinterId, _
                mPrintSequence, _
                mPausedBy, _
                mPausedDate, _
                mAuthorisedBy, _
                mAuthorisedDate, _
                mPrintingDate, _
                mPrintedDate, _
                mDeletedBy, _
                mDeletedDate, _
                mLastStatusMessage, _
                mStapleOff, _
                CheckSum, _
                Nothing)


            End If

            If Not DataObjects.Sprocs.LastError Is Nothing Then
                'TODO: Use errors collection to check to see if the problem was concurrency
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotUpdateReportAuthorisationQ)
            End If

        End Function


        Public Property AuthorisedBy() As String Implements IReportAuthorisedQ.AuthorisedBy
            Get
                Return mAuthorisedBy
            End Get
            Set(ByVal Value As String)
                mAuthorisedBy = Value
            End Set
        End Property
        Private mAuthorisedBy As String = ""

        Public Property AuthorisedDate() As Date Implements IReportAuthorisedQ.AuthorisedDate
            Get
                Return mAuthorisedDate
            End Get
            Set(ByVal Value As Date)
                mAuthorisedDate = Value
            End Set
        End Property
        Private mAuthorisedDate As Date = Nothing

        Public Property PrintSequence() As Integer Implements IReportAuthorisedQ.PrintSequence
            Get
                Return mPrintSequence
            End Get
            Set(ByVal Value As Integer)
                mPrintSequence = Value
            End Set
        End Property
        Private mPrintSequence As Int32

        Public Property ReportId() As Integer Implements IReportAuthorisedQ.ReportId
            Get
                Return mReportId
            End Get
            Set(ByVal Value As Integer)
                mReportId = Value
            End Set
        End Property
        Private mReportId As Int32

        Public Property ReportPrinterId() As Integer Implements IReportAuthorisedQ.ReportPrinterId
            Get
                Return mReportPrinterId
            End Get
            Set(ByVal Value As Integer)
                mReportPrinterId = Value
            End Set
        End Property
        Private mReportPrinterId As Int32

        Public Property DeletedBy() As String Implements IReportAuthorisedQ.DeletedBy
            Get
                Return mDeletedBy
            End Get
            Set(ByVal Value As String)
                mDeletedBy = Value
            End Set
        End Property
        Private mDeletedBy As String

        Public Property DeletedDate() As Object Implements IReportAuthorisedQ.DeletedDate
            Get
                Return mDeletedDate
            End Get
            Set(ByVal Value As Object)
                mDeletedDate = Value
            End Set
        End Property
        Private mDeletedDate As Object

        Public Property LastStatusMessage() As String Implements IReportAuthorisedQ.LastStatusMessage
            Get
                Return mLastStatusMessage
            End Get
            Set(ByVal Value As String)
                mLastStatusMessage = Value
            End Set
        End Property
        Private mLastStatusMessage As String

        Public Property PausedBy() As String Implements IReportAuthorisedQ.PausedBy
            Get
                Return mPausedBy
            End Get
            Set(ByVal Value As String)
                mPausedBy = Value
            End Set
        End Property
        Private mPausedBy As String

        Public Property PausedDate() As Object Implements IReportAuthorisedQ.PausedDate
            Get
                Return mPausedDate
            End Get
            Set(ByVal Value As Object)
                mPausedDate = Value
            End Set
        End Property
        Private mPausedDate As Object = Nothing

        Public Property PrintedDate() As Object Implements IReportAuthorisedQ.PrintedDate
            Get
                Return mPrintedDate
            End Get
            Set(ByVal Value As Object)
                mPrintedDate = Value
            End Set
        End Property
        Private mPrintedDate As Object

        Public Property PrintingDate() As Object Implements IReportAuthorisedQ.PrintingDate
            Get
                Return mPrintingDate
            End Get
            Set(ByVal Value As Object)
                mPrintingDate = Value
            End Set
        End Property
        Private mPrintingDate As Object

        Public Property StapleOff() As Boolean Implements IReportAuthorisedQ.StapleOff
            Get
                Return mStapleOff
            End Get
            Set(ByVal Value As Boolean)
                mStapleOff = Value
            End Set
        End Property
        Private mStapleOff As Boolean

        Public Property ReportAuthorisedQId() As Integer Implements IReportAuthorisedQ.ReportAuthorisedQId
            Get
                Return mReportAuthorisedQId
            End Get
            Set(ByVal Value As Integer)
                mReportAuthorisedQId = Value
            End Set
        End Property
        Private mReportAuthorisedQId As Int32
    End Class
End Namespace
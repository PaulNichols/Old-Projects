Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do
Imports uk.gov.defra.Phoenix.DO.DataObjects

Namespace ReportData
    <Serializable()> Public Class BoReportPrinter
        Inherits BaseBO
        Implements IReportPrinter

        Public Sub New()
            MyBase.new()
        End Sub

        Public Sub New(ByVal ReportPrinterId As Int32)
            MyBase.new()
            InitialiseReportPrinter(ReportPrinterId)
        End Sub

        Private Sub InitialiseReportPrinter(ByVal reportPrinterId As Int32)

            If reportPrinterId = 0 Then
                mName = "No Printer"
            Else
                Dim reportPrinterService As New DataObjects.Service.ReportPrinterService
                Dim reportPrinter As DataObjects.Entity.ReportPrinter = reportPrinterService.GetById(reportPrinterId, Nothing)

                CheckSum = reportPrinter.CheckSum
                mReportPrinterId = reportPrinterId
                mName = reportPrinter.Name
                mNetworkPath = reportPrinter.NetworkPath
                If Not reportPrinter.IsPausedByNull Then mPausedBy = reportPrinter.PausedBy
                If Not reportPrinter.IsPausedDateNull Then mPausedDate = reportPrinter.PausedDate
            End If

        End Sub

        Public Overrides Function Save() As BaseBO
            Dim reportPrinter As New DataObjects.Entity.ReportPrinter
            Dim reportPrinterService As DataObjects.Service.ReportPrinterService = reportPrinter.ServiceObject

            Created = (mReportPrinterId = 0)

            If Created Then

                'Insert ReportPrinter table
                reportPrinterService.Insert(mName, _
                mNetworkPath, _
                mPausedDate, _
                mPausedBy, _
                Nothing)

            Else

                'Update ReportPrinter table
                reportPrinter = reportPrinterService.Update(mReportPrinterId, _
                mName, _
                mNetworkPath, _
                mPausedDate, _
                mPausedBy, _
                CheckSum, _
                Nothing)


            End If

            If DataObjects.Sprocs.LastError Is Nothing Then
                'TODO: Use errors collection to check to see if the problem was concurrency
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotUpdateReportPrinter)
            End If

        End Function

        Public Property Name() As String Implements IReportPrinter.Name
            Get
                Return mName
            End Get
            Set(ByVal Value As String)
                mName = Value
            End Set
        End Property
        Private mName As String

        Public Property NetworkPath() As String Implements IReportPrinter.NetworkPath
            Get
                Return mNetworkPath
            End Get
            Set(ByVal Value As String)
                mNetworkPath = Value
            End Set
        End Property
        Private mNetworkPath As String

        Public Property ReportPrinterId() As Integer Implements IReportPrinter.ReportPrinterId
            Get
                Return mReportPrinterId
            End Get
            Set(ByVal Value As Integer)
                mReportPrinterId = Value
            End Set
        End Property
        Private mReportPrinterId As Int32

        Public Property PausedBy() As Object Implements IReportPrinter.PausedBy
            Get
                Return mPausedBy
            End Get
            Set(ByVal Value As Object)
                mPausedBy = Value
            End Set
        End Property
        Private mPausedBy As Object

        Public Property PausedDate() As Object Implements IReportPrinter.PausedDate
            Get
                Return mPausedDate
            End Get
            Set(ByVal Value As Object)
                mPausedDate = Value
            End Set
        End Property
        Private mPausedDate As Object


        Public Property StatusDescription() As String Implements IReportPrinter.StatusDescription
            Get
                If mPausedDate Is Nothing Then
                    Return String.Concat(mName, " ", mNetworkPath, " - Running")
                Else
                    Return String.Concat(mName, " ", mNetworkPath, " - Paused By ", mPausedBy.ToString, " - Paused On ", CType(mPausedDate, Date).ToString)
                End If

            End Get
            Set(ByVal Value As String)

            End Set
        End Property

        Public Property Status() As String Implements IReportPrinter.Status
            Get
                If mPausedDate Is Nothing Then
                    Return "Pause"
                Else
                    Return "Resume"
                End If
            End Get
            Set(ByVal Value As String)

            End Set
        End Property


        Public Property QueueStatus() As QueueStatuses Implements IReportPrinter.QueueStatus
            Get
                Return mQueueStatus
            End Get
            Set(ByVal Value As QueueStatuses)
                mQueueStatus = Value
            End Set
        End Property
        Private mQueueStatus As QueueStatuses
    End Class
End Namespace
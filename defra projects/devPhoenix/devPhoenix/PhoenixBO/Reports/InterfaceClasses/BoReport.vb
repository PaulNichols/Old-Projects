
Imports System.text
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do
Imports uk.gov.defra.Phoenix.DO.DataObjects

Namespace ReportData
    <Serializable()> Public Class BoReport
        Inherits BaseBO
        Implements IReport

#Region " Prelim code "
        Public Sub New()
            MyBase.new()

        End Sub

        Public Sub New(ByVal reportId As Int32)
            MyBase.new()
            InitialiseReportPrinter(reportId)
        End Sub

        Private Sub InitialiseReportPrinter(ByVal reportJob As Int32)

            Dim report As DataObjects.Entity.Report = report.GetById(reportJob)

            mReportId = report.ReportId
            mCreatedDate = report.CreatedDate
            mReportTypeId = report.ReportTypeId
            mVersion = report.Version
            mSearchReference = report.SearchReference
            mDatabaseId = report.DatabaseId
            mReportPrintJobId = report.ReportPrintJobId
            If report.IsExpiryDateNull Then mExpiryDate = report.ExpiryDate
            mSize = report.Size
            mReportPrinterId = report.ReportPrinterId
            mPrintSequence = report.PrintSequence
            mStaple = report.Staple

        End Sub
#End Region

#Region "Methods"
        Public Overrides Function Save() As BaseBO

            Dim report As New DataObjects.Entity.Report
            Dim reportService As DataObjects.Service.ReportService = report.ServiceObject

            ' Clear any errors from stack before we start
            DataObjects.Sprocs.LastError = Nothing

            Created = (mReportId = 0)

            If Created Then

                ' Do nothing - Insert is not permitted

            Else

                'Update Report table
                report = reportService.Update(mReportId, _
                mCreatedDate, _
                mReportTypeId, _
                mVersion, _
                mSearchReference, _
                mDatabaseId, _
                mReportPrintJobId, _
                mPrintSequence, _
                mExpiryDate, _
                mSize, _
                mReportPrinterId, _
                mStaple, _
                mDescription, _
                CheckSum, _
                Nothing)


            End If

            If Not DataObjects.Sprocs.LastError Is Nothing Then
                'TODO: Use errors collection to check to see if the problem was concurrency
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotUpdateReport)
            End If

        End Function

#End Region

#Region "Properties"


        Public Property CreatedDate() As Date Implements IReport.CreatedDate
            Get
                Return mCreatedDate
            End Get
            Set(ByVal Value As Date)
                mCreatedDate = Value
            End Set
        End Property
        Private mCreatedDate As Date

        Public Property DatabaseId() As Integer Implements IReport.DatabaseId
            Get
                Return mDatabaseId
            End Get
            Set(ByVal Value As Integer)
                mDatabaseId = Value
            End Set
        End Property
        Private mDatabaseId As Int32

        Public Property ExpiryDate() As Object Implements IReport.ExpiryDate
            Get
                Return mExpiryDate
            End Get
            Set(ByVal Value As Object)
                mExpiryDate = Value
            End Set
        End Property
        Private mExpiryDate As Object

        Public Property PrintSequence() As Integer Implements IReport.PrintSequence
            Get
                Return mPrintSequence
            End Get
            Set(ByVal Value As Integer)
                mPrintSequence = Value
            End Set
        End Property
        Private mPrintSequence As Int32

        Public Property ReportId() As Integer Implements IReport.ReportId
            Get
                Return mReportId
            End Get
            Set(ByVal Value As Integer)
                mReportId = Value
            End Set
        End Property
        Private mReportId As Int32

        Public Property ReportPrinterId() As Integer Implements IReport.ReportPrinterId
            Get
                Return mReportPrinterId
            End Get
            Set(ByVal Value As Integer)
                mReportPrinterId = Value
            End Set
        End Property
        Private mReportPrinterId As Int32

        Public Property ReportPrintJobId() As Integer Implements IReport.ReportPrintJobId
            Get
                Return mReportPrintJobId
            End Get
            Set(ByVal Value As Integer)
                mReportPrintJobId = Value
            End Set
        End Property
        Private mReportPrintJobId As Int32

        Public Property ReportTypeId() As Integer Implements IReport.ReportTypeId
            Get
                Return mReportTypeId
            End Get
            Set(ByVal Value As Integer)
                mReportTypeId = Value
            End Set
        End Property
        Private mReportTypeId As Int32

        Public Property SearchReference() As String Implements IReport.SearchReference
            Get
                Return mSearchReference
            End Get
            Set(ByVal Value As String)
                mSearchReference = Value
            End Set
        End Property
        Private mSearchReference As String

        Public Property Size() As Integer Implements IReport.Size
            Get
                Return mSize
            End Get
            Set(ByVal Value As Integer)
                mSize = Value
            End Set
        End Property
        Private mSize As Int32

        Public Property Staple() As Integer Implements IReport.Staple
            Get
                Return mStaple
            End Get
            Set(ByVal Value As Integer)
                mStaple = Value
            End Set
        End Property
        Private mStaple As Int32

        Public Property Version() As Integer Implements IReport.Version
            Get
                Return mVersion
            End Get
            Set(ByVal Value As Integer)
                mVersion = Value
            End Set
        End Property
        Private mVersion As Int32

        Public Property Description() As String Implements IReport.Description
            Get
                Return mDescription
            End Get
            Set(ByVal Value As String)
                mDescription = Value
            End Set
        End Property
        Private mDescription As String
#End Region


    End Class
End Namespace
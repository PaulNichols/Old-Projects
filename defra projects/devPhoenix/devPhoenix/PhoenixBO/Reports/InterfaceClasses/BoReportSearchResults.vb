
Imports System.Data
Imports System.IO
Imports System.Xml
Imports System.Xml.Serialization
Imports System.text
Imports uk.gov.defra
Imports uk.gov.defra.Phoenix.do
Imports uk.gov.defra.Phoenix.bo

Namespace ReportData
    <Serializable()> _
    Public Class BoReportSearchResults
        Inherits BaseBO
        Implements IBoReportSearchResults


#Region " Prelim code "

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal report As DataObjects.Entity.Report)
            MyBase.New()
            mReport = report
            InitilizeReportView(report)

        End Sub
        Private mReport As DataObjects.Entity.Report

        Private Sub InitilizeReportView(ByVal report As DataObjects.Entity.Report)

            CheckSum = report.CheckSum

            ' Get Report for this reportId
            Dim typeId As Int32 = report.ReportTypeId
            mReportId = report.Id
            mSearchReference = report.SearchReference
            mDescription = report.Description
            mCreatedDate = report.CreatedDate
            mCreatedDate_NavigateUrl = "/Modules/Main/CITES/Reports/ReportViewer/ReportViewer.aspx?ReportId=" & mReportId.ToString

            ' Get ReportPrintJob  details from ReportPrintJobId
            Dim reportPrintJobService As New DataObjects.Service.ReportPrintJobService
            Dim reportPrintJobSet As DataObjects.Entity.ReportPrintJob = reportPrintJobService.GetById(report.ReportPrintJobId)
            mPrintJobDescription = reportPrintJobSet.Description

            ' Get ReportType  details from TypeId
            Dim reportTypeService As New DataObjects.Service.ReportTypeService
            Dim reportTypeSet As DataObjects.Entity.ReportType = reportTypeService.GetById(typeId)
            Dim criteriaObjectName As String = reportTypeSet.BOTypeName
            mReportTypeDescription = reportTypeSet.Description
            Dim stapleBatch As Int32 = reportTypeSet.StapleBatch


        End Sub

#End Region


#Region " Methods"
        Public Function GetReportSearchResults(ByVal reportTypeIds() As Integer, ByVal fromDate As Date, ByVal toDate As Date) As BoReportSearchResults() Implements IBoReportSearchResults.GetReportSearchView


            Dim report As New DataObjects.Entity.Report
            Dim reportService As DataObjects.Service.ReportService = report.ServiceObject
            Dim reportSet As DataObjects.EntitySet.ReportSet
            Dim ReportSearchView As BoReportSearchResults
            Dim ReportSearchViews(-1) As BoReportSearchResults
            Dim idx As Int32 = -1
            For Each reportTypeId As Int32 In reportTypeIds ' Loop through Report Type Id's

                reportSet = reportService.GetForReportType(reportTypeId, Nothing) ' Get Report DO for each Report Type Id

                For Each report In reportSet.Entities ' Loop through selected Report DO's for each Report Type Id
                    If CType(report.CreatedDate.ToShortDateString, Date) >= fromDate And CType(report.CreatedDate.ToShortDateString, Date) <= toDate Then
                        ReportSearchView = New BoReportSearchResults(report) ' Get Report Search Results BO for this Report DO.
                        idx += 1
                        ReDim Preserve ReportSearchViews(idx)
                        ReportSearchViews(idx) = ReportSearchView ' Build array of Report Search Result BO's
                    End If
                Next
            Next

            Array.Sort(ReportSearchViews, New ReportSearchResultsComparer)

            Return ReportSearchViews ' Return array of Report Search Result BO's

        End Function
#End Region

#Region " Properties"

        Public Property ReportId() As Integer Implements IBoReportSearchResults.ReportId
            Get
                Return mReportId
            End Get
            Set(ByVal Value As Integer)
                mReportId = Value
            End Set
        End Property
        Private mReportId As Int32 = 0

        Public Property PrintJobDescription() As String Implements IBoReportSearchResults.PrintJobDescription
            Get
                Return mPrintJobDescription
            End Get
            Set(ByVal Value As String)
                mPrintJobDescription = Value
            End Set
        End Property
        Private mPrintJobDescription As String

        Public Property ReportTypeDescription() As String Implements IBoReportSearchResults.ReportTypeDescription
            Get
                Return mReportTypeDescription
            End Get
            Set(ByVal Value As String)
                mReportTypeDescription = Value
            End Set
        End Property
        Private mReportTypeDescription As String

        Public Property ReportViewURL() As String Implements IBoReportSearchResults.ReportViewURL
            Get
                Return mReportViewURL
            End Get
            Set(ByVal Value As String)
                mReportViewURL = Value
            End Set
        End Property
        Private mReportViewURL As String

        Public Property SearchReference() As String Implements IBoReportSearchResults.SearchReference
            Get
                Return mSearchReference
            End Get
            Set(ByVal Value As String)
                mSearchReference = Value
            End Set
        End Property
        Private mSearchReference As String

        Public Property Description() As String Implements IBoReportSearchResults.Description
            Get
                Return mDescription
            End Get
            Set(ByVal Value As String)
                mDescription = Value
            End Set
        End Property
        Private mDescription As String

        Public Property CreatedDate() As Date Implements IBoReportSearchResults.CreatedDate
            Get
                Return mCreatedDate
            End Get
            Set(ByVal Value As Date)
                mCreatedDate = Value
            End Set
        End Property
        Private mCreatedDate As Date

        Public Property CreatedDate_NavigateUrl() As String Implements IBoReportSearchResults.CreatedDate_NavigateUrl
            Get
                Return mCreatedDate_NavigateUrl
            End Get
            Set(ByVal Value As String)
                mCreatedDate_NavigateUrl = Value
            End Set
        End Property
        Private mCreatedDate_NavigateUrl As String

#End Region

        Private Class ReportSearchResultsComparer
            Implements IComparer

            Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
                Dim xinfo As BoReportSearchResults = CType(x, BoReportSearchResults)
                Dim yinfo As BoReportSearchResults = CType(y, BoReportSearchResults)
                Return Date.Compare(xinfo.CreatedDate, yinfo.CreatedDate)
            End Function
        End Class
    End Class
End Namespace

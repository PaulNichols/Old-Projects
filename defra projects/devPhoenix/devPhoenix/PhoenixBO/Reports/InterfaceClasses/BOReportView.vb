

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
    Public Class BOReportView
        Inherits BaseBO
        Implements IBOReportView


#Region " Prelim code "

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal reportId As Int32)
            MyBase.New()

            ' Get Report for this reportId
            Dim reportService As New DataObjects.Service.ReportService
            Dim report As DataObjects.Entity.Report = reportService.GetById(reportId, Nothing)
            mReport = report

            InitilizeReportView(report)

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
            mReportPrinterId = report.ReportPrinterId
            mStaple = report.Staple
            mPrintSequence = report.PrintSequence
            mSearchReference = report.SearchReference
            mDescription = report.Description

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

            mPrinterSelection = GetPrinters(typeId)

            mStapleSelection = GetStaple(stapleBatch)

            mReportViewURL = "/Modules/Main/CITES/Reports/ReportViewer/ReportViewer.aspx?ReportId=" & mReportId.ToString
        End Sub

#End Region

#Region " Helper Functions "

        Private Function GetPrinters(ByVal typeId As Int32) As ReportData.BoReportPrinter()

            Dim reportPrinters(0) As ReportData.BoReportPrinter

            If mReportPrinterId > 0 Then

                Dim reportPrinter As New DataObjects.Entity.ReportPrinter
                Dim reportPrinterService As DataObjects.Service.ReportPrinterService = reportPrinter.ServiceObject

                Dim lz As DataObjects.Entity.ReportTypeReportPrinter
                Dim rtp As New DataObjects.Entity.ReportTypeReportPrinter
                Dim rtps As DataObjects.Service.ReportTypeReportPrinterService = rtp.ServiceObject
                Dim lo As DataObjects.EntitySet.ReportTypeReportPrinterSet = rtps.GetForReportType(typeId)
                Dim idx As Int32 = -1
                For Each lz In lo
                    idx += 1
                    ReDim Preserve reportPrinters(idx)
                    reportPrinters(idx) = New ReportData.BoReportPrinter(lz.ReportPrinterId)
                Next

            Else
                reportPrinters(0) = New ReportData.BoReportPrinter(0)

            End If

            Return reportPrinters

        End Function

        Private Function GetStaple(ByVal stapleBatch As Int32) As ReportData.BoReportStaple()

            Dim reportStaple(0) As ReportData.BoReportStaple

            If stapleBatch > 0 Then
                ReDim reportStaple(1)
                reportStaple(0) = New ReportData.BoReportStaple(1)
                reportStaple(1) = New ReportData.BoReportStaple(2)
            Else
                reportStaple(0) = New ReportData.BoReportStaple(0)

            End If

            Return reportStaple

        End Function
#End Region

#Region " Methods"
        Public Function GetPrintJobView(ByVal PrintJobId As Integer) As BOReportView() Implements IBOReportView.GetPrintJobView
            Dim report As New DataObjects.Entity.Report
            Dim reportService As DataObjects.Service.ReportService = report.ServiceObject
            Dim reportSet As DataObjects.EntitySet.ReportSet = reportService.GetForReportPrintJob(PrintJobId, Nothing)

            Dim reportView As BOReportView

            Dim reportViews(reportSet.Entities.Count - 1) As BOReportView
            For Each report In reportSet.Entities
                reportView = New BOReportView(report)
                reportViews(reportView.PrintSequence) = reportView
            Next

            Return reportViews
        End Function

        Public Shared Function GetPrintJobViewShared(ByVal printJobId As Integer) As BOReportView()
            Return New BOReportView().GetPrintJobView(printJobId)
        End Function
#End Region

#Region " Properties"

        Public Property PrintSequence() As Integer Implements IBOReportView.PrintSequence
            Get
                Return mPrintSequence
            End Get
            Set(ByVal Value As Integer)
                mPrintSequence = Value
            End Set
        End Property
        Private mPrintSequence As Int32

        Public Property ReportId() As Integer Implements IBOReportView.ReportId
            Get
                Return mReportId
            End Get
            Set(ByVal Value As Integer)
                mReportId = Value
            End Set
        End Property
        Private mReportId As Int32 = 0

        Public Property PrintJobDescription() As String Implements IBOReportView.PrintJobDescription
            Get
                Return mPrintJobDescription
            End Get
            Set(ByVal Value As String)
                mPrintJobDescription = Value
            End Set
        End Property
        Private mPrintJobDescription As String

        Public Property ReportTypeDescription() As String Implements IBOReportView.ReportTypeDescription
            Get
                Return mReportTypeDescription
            End Get
            Set(ByVal Value As String)
                mReportTypeDescription = Value
            End Set
        End Property
        Private mReportTypeDescription As String

        Public Property ReportViewURL() As String Implements IBOReportView.ReportViewURL
            Get
                Return mReportViewURL
            End Get
            Set(ByVal Value As String)
                mReportViewURL = Value
            End Set
        End Property
        Private mReportViewURL As String

        Public Property ReportPrinterId() As Integer Implements IBOReportView.ReportPrinterId
            Get
                Return mReportPrinterId
            End Get
            Set(ByVal Value As Integer)
                mReportPrinterId = Value
            End Set
        End Property
        Private mReportPrinterId As Int32


        Public Property PrinterSelection() As uk.gov.defra.Phoenix.BO.ReportData.BoReportPrinter() Implements IBOReportView.PrinterSelection
            Get
                Return mPrinterSelection
            End Get
            Set(ByVal Value() As uk.gov.defra.Phoenix.BO.ReportData.BoReportPrinter)
                mPrinterSelection = Value
            End Set
        End Property
        Private mPrinterSelection() As ReportData.BoReportPrinter

        Public Property StapleSelection() As uk.gov.defra.Phoenix.BO.ReportData.BoReportStaple() Implements IBOReportView.StapleSelection
            Get
                Return mStapleSelection
            End Get
            Set(ByVal Value() As uk.gov.defra.Phoenix.BO.ReportData.BoReportStaple)
                mStapleSelection = Value
            End Set
        End Property
        Private mStapleSelection() As ReportData.BoReportStaple

        Public Property Staple() As Integer Implements IBOReportView.Staple
            Get
                Return mStaple
            End Get
            Set(ByVal Value As Integer)
                mStaple = Value
            End Set
        End Property
        Private mStaple As Int32

        Public Property StapleBatch() As Integer Implements IBOReportView.StapleBatch
            Get
                Return mStapleBatch
            End Get
            Set(ByVal Value As Integer)
                mStapleBatch = Value
            End Set
        End Property
        Private mStapleBatch As Int32

        Public Property SearchReference() As String Implements IBOReportView.SearchReference
            Get
                Return mSearchReference
            End Get
            Set(ByVal Value As String)
                mSearchReference = Value
            End Set
        End Property
        Private mSearchReference As String

        Public Property Description() As String Implements IBOReportView.Description
            Get
                Return mDescription
            End Get
            Set(ByVal Value As String)
                mDescription = Value
            End Set
        End Property
        Private mDescription As String
#End Region

#Region " Save "

        Public Overridable Shadows Function Save(ByVal tran As SqlClient.SqlTransaction) As BOReportView

            Dim report As New DataObjects.Entity.Report
            Dim service As DataObjects.Service.ReportService = report.ServiceObject

            ' Clear any errors from stack before we start
            DataObjects.Sprocs.LastError = Nothing

            Created = (mReportId = 0)

            If Created Then

                ' Do nothing - Insert is not permitted

            Else

                'Update Report table
                mReport.ReportPrinterId = mReportPrinterId
                mReport.Staple = mStaple
                report = service.Update(mReport)

            End If

            'check to see if any SQL errors have occured
            If report Is Nothing AndAlso Not DataObjects.Sprocs.LastError Is Nothing Then
                'TODO: Use errors collection to check to see if the problem was concurrency
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotUpdateReport)

            ElseIf report Is Nothing Then
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotUpdateReport)
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
            Else
                If Created And Not report Is Nothing Then
                    mReportId = report.ReportId
                End If
                'no point in initialising unless things have changed
                If report.CheckSum <> CheckSum Then InitilizeReportView(report)

            End If

            Return Me

        End Function

#End Region



    End Class
End Namespace

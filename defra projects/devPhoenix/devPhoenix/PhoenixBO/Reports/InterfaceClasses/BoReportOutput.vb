Imports System.Data
Imports System.IO
Imports System.text
Imports uk.gov.defra
Imports uk.gov.defra.Phoenix.do
Imports uk.gov.defra.Phoenix.bo

Namespace ReportData
    <Serializable()> _
    Public Class BOReportOutput
        Inherits BaseBO
        Implements IBOReportOutput

#Region " Prelim code "

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal reportId As Int32)
            MyBase.New()
            InitilizeBoReportResults(reportId)

        End Sub


        Protected Overridable Sub InitilizeBoReportResults(ByVal reportId As Int32)

            ' Get Report for this reportId
            Dim reportService As New DataObjects.Service.ReportService
            Dim report As DataObjects.Entity.Report = reportService.GetById(reportId)
            mReportId = report.Id

            ' Get ReportType  details from TypeId
            Dim reportTypeService As New DataObjects.Service.ReportTypeService
            Dim reportTypeSet As DataObjects.Entity.ReportType = reportTypeService.GetById(report.ReportTypeId)
            Dim criteriaObjectName As String = reportTypeSet.BOTypeName
            mContentType = reportTypeSet.ContentType

            ' Get Output Byte Array from New DataObjects.Service.ReportOutputService
            Dim reportOutputService As New DataObjects.Service.ReportOutputService
            Dim reportOutputSet As DataObjects.EntitySet.ReportOutputSet = reportOutputService.GetByIndex_ReportOutputId(reportId)
            Dim output As String = reportOutputSet.Entities(0).Image
            Dim unicodeEncoding As unicodeEncoding = New unicodeEncoding
            mOutput = unicodeEncoding.GetBytes(output)
            mOutput = Convert.FromBase64String(output)

        End Sub

#End Region

#Region " Properties "


        Public Property ReportId() As Integer Implements IBOReportOutput.ReportId
            Get
                Return mReportId
            End Get
            Set(ByVal Value As Integer)
                mReportId = Value
            End Set
        End Property
        Private mReportId As Int32 = 0

        Public Property ReportOutput() As Byte() Implements IBOReportOutput.ReportOutput
            Get
                Return mOutput
            End Get
            Set(ByVal Value() As Byte)
                mOutput = Value
            End Set
        End Property
        Private mOutput() As Byte

        Public Property ContentType() As String Implements IBOReportOutput.ContentType
            Get
                Return mContentType
            End Get
            Set(ByVal Value As String)
                mContentType = Value
            End Set
        End Property
        Private mContentType As String
#End Region


    End Class
End Namespace

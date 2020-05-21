Imports System.Data
Imports System.IO
Imports System.Xml
Imports System.Xml.Serialization
Imports System.text
Imports uk.gov.defra
Imports uk.gov.defra.Phoenix.do
Imports uk.gov.defra.Phoenix.bo
Imports uk.gov.defra.Phoenix.PhoenixReport

Namespace RPT
    <Serializable()> _
    Public Class BOReportResults
        Inherits BaseBO
        Implements IBOReportResults



#Region " Prelim code "

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal reportId As Int32)
            MyBase.New()
            InitilizeBoReportResults(reportId)

        End Sub


        Protected Overridable Sub InitilizeBoReportResults(ByVal reportId As Int32)

            Dim tran As SqlClient.SqlTransaction = Nothing

            ' Get Report for this reportId
            Dim reportService As New DataObjects.Service.ReportService
            Dim report As DataObjects.Entity.Report = reportService.GetById(reportId, tran)
            mCreatedDate = report.CreatedDate
            mTypeId = report.ReportTypeId
            mVersion = report.Version
            mReportId = report.Id
            mSearchReference = report.SearchReference
            mDataBaseId = report.DatabaseId
            mReportPrintJobId = report.ReportPrintJobId
            mPrintSequence = report.PrintSequence
            mExpiryDate = report.ExpiryDate
            mSize = report.Size
            mReportPrinterId = report.ReportPrinterId
            mStaple = report.Staple

            ' Get ReportPrintJob  details from ReportPrintJobId
            Dim reportPrintJobService As New DataObjects.Service.ReportPrintJobService
            Dim reportPrintJobSet As DataObjects.Entity.ReportPrintJob = reportPrintJobService.GetById(mReportPrintJobId)
            mPrintJobDescription = reportPrintJobSet.Description

            ' Get ReportType  details from TypeId
            Dim reportTypeService As New DataObjects.Service.ReportTypeService
            Dim reportTypeSet As DataObjects.Entity.ReportType = reportTypeService.GetById(TypeId)
            Dim criteriaObjectName As String = reportTypeSet.BOTypeName
            mBoTypeName = criteriaObjectName
            mReportTypeDescription = reportTypeSet.Description
            mOutputType = reportTypeSet.OutputType
            mContentType = reportTypeSet.ContentType
            mStapleStartPage = reportTypeSet.StapleStartPage
            mStapleEndPage = reportTypeSet.StapleEndPage
            mStapleBatch = reportTypeSet.StapleBatch
            'mReportPrinterId = reportTypeSet.ReportPrinterId
            'mStaple = 0
            'If reportTypeSet.StapleBatch <> 0 Then mStaple = 1

            ' DeSerialize Criteria XML back into respective object type
            Dim reportCriteriaService As New DataObjects.Service.ReportCriteriaService
            Dim reportCriteriaSet As DataObjects.EntitySet.ReportCriteriaSet = reportCriteriaService.GetByIndex_ReportCriteriaId(reportId)
            Dim criteria As String = reportCriteriaSet.Entities(0).XmlData
            Dim reportCriteria As ReportCriteria.ReportCriteria
            Dim stringReader As New StringReader(criteria)
            Dim xml_serializer As New XmlSerializer(GetReportType(criteriaObjectName, True))
            reportCriteria = CType(xml_serializer.Deserialize(stringReader), reportCriteria.ReportCriteria)
            mCriteria = reportCriteria

            ' DeSerialize Data XML back into respective object type
            Dim reportDataService As New DataObjects.Service.ReportDataService
            Dim reportDataSet As DataObjects.EntitySet.ReportDataSet = reportDataService.GetByIndex_ReportDataId(reportId)
            Dim data As String = reportDataSet.Entities(0).XmlData
            Dim reportData As DataSet
            stringReader = New StringReader(data)
            xml_serializer = New XmlSerializer(GetType(DataSet))
            'xml_serializer = New XmlSerializer(GetReportType(criteriaObjectName, False))
            reportData = CType(xml_serializer.Deserialize(stringReader), DataSet)
            mData = reportData

            ' Get Output Byte Array from New DataObjects.Service.ReportOutputService
            Dim reportOutputService As New DataObjects.Service.ReportOutputService
            Dim reportOutputSet As DataObjects.EntitySet.ReportOutputSet = reportOutputService.GetByIndex_ReportOutputId(reportId)
            Dim output As String = reportOutputSet.Entities(0).Image
            Dim unicodeEncoding As UnicodeEncoding = New UnicodeEncoding
            mOutput = unicodeEncoding.GetBytes(output)
            mOutput = Convert.FromBase64String(output)

            ' Get RPT Byte Array from ReportRPT Table
            Dim reportRPTService As New DataObjects.Service.ReportRPTService
            Dim reportRPTSet As DataObjects.EntitySet.ReportRPTSet = reportRPTService.GetByIndex_ReportRPTId(reportId)
            Dim rpt As String = reportRPTSet.Entities(0).Image
            unicodeEncoding = New UnicodeEncoding
            mRPT = unicodeEncoding.GetBytes(rpt)
            mRPT = Convert.FromBase64String(rpt)

            mPrinterSelection = GetPrinters()

            mStapleSelection = GetStaple()

        End Sub

#End Region

#Region " Helper Functions "
        Private Function GetReportType(ByVal objectTypeName As String, ByVal criteria As Boolean) As Type

            Dim aType As Type

            Select Case objectTypeName

                Case "SpeciesTradePatternCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.SpeciesTradePatternCriteria)
                    Else
                        aType = GetType(SpeciesTradePatternData)
                    End If

                Case "ServiceLevelsReferralCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.ServiceLevelsReferralCriteria)
                    Else
                        aType = GetType(ServiceLevelsReferralData)
                    End If

                Case "PartyDataProtectionCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.PartyDataProtectionCriteria)
                    Else
                        aType = GetType(PartyDataProtectionData)
                    End If

                Case "RegistrationRefusalLetterCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.RegistrationRefusalLetterCriteria)
                    Else
                        aType = GetType(RegistrationRefusalLetterData)
                    End If

                Case "SemiCompleteReminderLetterCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.SemiCompleteReminderLetterCriteria)
                    Else
                        aType = GetType(SemiCompleteReminderLetterData)
                    End If

                Case "PermitRefusalLetterCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.PermitRefusalLetterCriteria)
                    Else
                        aType = GetType(PermitRefusalLetterData)
                    End If

                Case "CertificateRefusalLetterCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.CertificateRefusalLetterCriteria)
                    Else
                        aType = GetType(CertificateRefusalLetterData)
                    End If

                Case "RemittanceAdviceCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.RemittanceAdviceCriteria)
                    Else
                        aType = GetType(RemittanceAdviceData)
                    End If

                Case "PaymentReceiptCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.PaymentReceiptCriteria)
                    Else
                        aType = GetType(PaymentReceiptData)
                    End If


                Case "ViewCaseTypesSpeciesCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.ViewCaseTypesSpeciesCriteria)
                    Else
                        aType = GetType(ViewCaseTypesSpeciesData)
                    End If

                Case "PeriodicFinSumCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.PeriodicFinSumCriteria)
                    Else
                        aType = GetType(PeriodicFinSumData)
                    End If

                Case "DFA_OfflineDetailAdjustmentCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.DFA_OfflineDetailAdjustmentCriteria)
                    Else
                        aType = GetType(DFA_OffLine_Detail_AdjustmentData)
                    End If
                Case "DFA_OfflineDetailRefundCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.DFA_OfflineDetailRefundCriteria)
                    Else
                        aType = GetType(DFA_OffLine_Detail_RefundData)
                    End If
                Case "DFA_OfflinePaymentCashCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.DFA_OfflinePaymentCashCriteria)
                    Else
                        aType = GetType(DFA_OffLine_Payment_CashData)
                    End If
                Case "DFA_OfflinePaymentChequeCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.DFA_OfflinePaymentChequeCriteria)
                    Else
                        aType = GetType(DFA_OffLine_Payment_ChequesData)
                    End If
                Case "DFA_OfflinePaymentPostalOrderCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.DFA_OfflinePaymentPostalOrderCriteria)
                    Else
                        aType = GetType(DFA_OffLine_Payment_PostalOrderData)
                    End If
                Case "DFA_OfflineSummaryCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.DFA_OfflineSummaryCriteria)
                    Else
                        aType = GetType(DFA_Offline_SummaryData)
                    End If
                Case "DFA_OnlinePaymentCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.DFA_OnlinePaymentCriteria)
                    Else
                        aType = GetType(DFA_Online_PaymentData)
                    End If
                Case "DFA_OnlineRefundCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.DFA_OnlineRefundCriteria)
                    Else
                        aType = GetType(DFA_Online_RefundData)
                    End If
                Case "DFA_OnlineSummaryCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.DFA_OnlineSummaryCriteria)
                    Else
                        aType = GetType(DFA_Online_SummaryData)
                    End If
                Case "DFA_WaiverCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.DFA_WaiverCriteria)
                    Else
                        aType = GetType(DFA_WaiverData)
                    End If
                Case "Schedule4ReplacementRingsCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.Schedule4ReplacementRingsCriteria)
                    Else
                        aType = GetType(Schedule4ReplacementRingsData)
                    End If

                Case "Schedule4ChicksCriteria"
                    If criteria Then
                        'aType = Me.GetType.Assembly.GetType("ReportCriteria.Schedule4ChicksCriteria")

                        aType = GetType(ReportCriteria.Schedule4ChicksCriteria)
                    Else
                        aType = GetType(Schedule4ChicksData)
                    End If

                Case "Schedule4BirdsCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.Schedule4BirdsCriteria)
                    Else
                        aType = GetType(Schedule4BirdsData)
                    End If


                Case "Schedule4ImportedBirdsCriteria"

                    If criteria Then
                        aType = GetType(ReportCriteria.Schedule4ImportedBirdsCriteria)
                    Else
                        aType = GetType(Schedule4ImportedBirdsData)
                    End If

                Case "Schedule4FoundCriteria"

                    If criteria Then
                        aType = GetType(ReportCriteria.Schedule4FoundCriteria)
                    Else
                        aType = GetType(Schedule4FoundData)
                    End If

                Case "Schedule4ApplicantBredCriteria"

                    If criteria Then
                        aType = GetType(ReportCriteria.Schedule4ApplicantBredCriteria)
                    Else
                        aType = GetType(Schedule4ApplicantBredData)
                    End If

                Case "BirdRegDocCriteria"

                    If criteria Then
                        aType = GetType(ReportCriteria.BirdRegDocCriteria)
                    Else
                        aType = GetType(BirdRegDocData)
                    End If

                Case "LimisRunningTotalsReportCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.LimisRunningTotalsReportCriteria)
                    Else
                        aType = GetType(LimisRunningTotalsData)
                    End If

                Case "LimisReceivedByReportCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.LimisReceivedByReportCriteria)
                    Else
                        aType = GetType(LimisReceivedByData)
                    End If


                Case "LimisMonthlyReportCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.LimisMonthlyReportCriteria)
                    Else
                        aType = GetType(LimisMonthlyData)
                    End If

                Case "ViewAuditLogCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.ViewAuditLogCriteria)
                    Else
                        aType = GetType(ViewAuditLogData)
                    End If

                Case "ReferralServiceLevelCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.ReferralServiceLevelCriteria)
                    Else
                        aType = GetType(ReferralServiceLevelData)
                    End If

                Case "KeeperBirdsReportCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.KeeperBirdsReportCriteria)
                    Else
                        aType = GetType(KeeperBirdsData)
                    End If

                Case "RegisteredBirdsReportCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.RegisteredBirdsReportCriteria)
                    Else
                        aType = GetType(RegisteredBirdsData)
                    End If

                Case "ApplicationArticle10ReportCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.ApplicationArticle10ReportCriteria)
                    Else
                        aType = GetType(ApplicationArticle10Data)
                    End If

                Case "Article10ReportCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.Article10ReportCriteria)
                    Else
                        aType = GetType(Article10Data)
                    End If

                Case "Article10SemiReportCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.Article10SemiReportCriteria)
                    Else
                        aType = GetType(Article10Data)
                    End If

                Case "ApplicationPermitCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.ApplicationPermitCriteria)
                    Else
                        aType = GetType(BOCITESApplicationPermitData)
                    End If

                Case "CertificatePermitCoverLetterCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.CertificatePermitCoverLetterCriteria)
                    Else
                        aType = GetType(CertificatePermitCoverLetterData)
                    End If

                Case "PermitReportCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.PermitReportCriteria)
                    Else
                        aType = GetType(BOCITESPermitData)
                    End If

                Case "PermitSemiReportCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.PermitSemiReportCriteria)
                    Else
                        aType = GetType(BOCITESPermitData)
                    End If

                Case "PermitDraftReportCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.PermitDraftReportCriteria)
                    Else
                        aType = GetType(BOCITESPermitData)
                    End If

                Case "ConsignmentReportCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.ConsignmentReportCriteria)
                    Else
                        aType = GetType(CITESConsignmentData)
                    End If

                Case "ConsignmentSemiReportCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.ConsignmentSemiReportCriteria)
                    Else
                        aType = GetType(CITESConsignmentData)
                    End If

                Case "ConsignmentDraftReportCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.ConsignmentDraftReportCriteria)
                    Else
                        aType = GetType(CITESConsignmentData)
                    End If

                    'Case "ConsignmentPermitsReportCriteria"          'MLD 26/1/5 no longer needed
                    '    If criteria Then
                    '        aType = GetType(ReportCriteria.ConsignmentPermitsReportCriteria)
                    '    Else
                    '        aType = GetType(CITESConsignmentPermitsData)
                    '    End If

                Case "ConsignmentSemiPermitsReportCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.ConsignmentSemiPermitsReportCriteria)
                    Else
                        aType = GetType(CITESConsignmentPermitsData)
                    End If

                Case "ConsignmentDraftPermitsReportCriteria"
                    If criteria Then
                        aType = GetType(ReportCriteria.ConsignmentDraftPermitsReportCriteria)
                    Else
                        aType = GetType(CITESConsignmentPermitsData)
                    End If

            End Select

            Return aType

        End Function

        Private Function GetPrinters() As ReportData.BoReportPrinter()

            Dim reportPrinters(0) As ReportData.BoReportPrinter

            If mReportPrinterId > 0 Then

                Dim reportPrinter As New DataObjects.Entity.ReportPrinter
                Dim reportPrinterService As DataObjects.Service.ReportPrinterService = reportPrinter.ServiceObject

                Dim lz As DataObjects.Entity.ReportTypeReportPrinter
                Dim rtp As New DataObjects.Entity.ReportTypeReportPrinter
                Dim rtps As DataObjects.Service.ReportTypeReportPrinterService = rtp.ServiceObject
                Dim lo As DataObjects.EntitySet.ReportTypeReportPrinterSet = rtps.GetForReportType(mTypeId)
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

        Private Function GetStaple() As ReportData.BoReportStaple()

            Dim reportStaple(0) As ReportData.BoReportStaple

            If mStapleBatch > 0 Then
                ReDim reportStaple(1)
                reportStaple(0) = New ReportData.BoReportStaple(1)
                reportStaple(1) = New ReportData.BoReportStaple(2)
            Else
                reportStaple(0) = New ReportData.BoReportStaple(0)

            End If

            Return reportStaple

        End Function
#End Region

#Region " Properties "

        Public Property CreatedDate() As Date Implements IBOReportResults.CreatedDate
            Get
                Return mCreatedDate
            End Get
            Set(ByVal Value As Date)
                mCreatedDate = Value
            End Set
        End Property
        Private mCreatedDate As Date

        Friend Property Criteria() As Object Implements IBOReportResults.Criteria
            Get
                Return mCriteria
            End Get
            Set(ByVal Value As Object)
                mCriteria = Value
            End Set
        End Property
        Private mCriteria As Object

        Friend Property Data() As Object Implements IBOReportResults.Data
            Get
                Return mData
            End Get
            Set(ByVal Value As Object)
                mData = Value
            End Set
        End Property
        Private mData As Object

        Public Property TypeId() As Integer Implements IBOReportResults.TypeId
            Get
                Return mTypeId
            End Get
            Set(ByVal Value As Integer)
                mTypeId = Value
            End Set
        End Property
        Private mTypeId As Int32

        Public Property ReportId() As Integer Implements IBOReportResults.ReportId
            Get
                Return mReportId
            End Get
            Set(ByVal Value As Integer)
                mReportId = Value
            End Set
        End Property
        Private mReportId As Int32 = 0


        Public Property RPT() As Byte() Implements IBOReportResults.RPT
            Get
                Return mRPT
            End Get
            Set(ByVal Value() As Byte)
                mRPT = Value
            End Set
        End Property
        Private mRPT() As Byte

        Public Property Version() As Integer Implements IBOReportResults.Version
            Get
                Return mVersion
            End Get
            Set(ByVal Value As Integer)
                mVersion = Value
            End Set
        End Property
        Private mVersion As Int32

        Public Property SearchReference() As String Implements IBOReportResults.SearchReference
            Get
                Return mSearchReference
            End Get
            Set(ByVal Value As String)
                mSearchReference = Value
            End Set
        End Property
        Private mSearchReference As String

        Public Property PrintSequence() As Integer Implements IBOReportResults.PrintSequence
            Get
                Return mPrintSequence
            End Get
            Set(ByVal Value As Integer)
                mPrintSequence = Value
            End Set
        End Property
        Private mPrintSequence As Int32

        Public Property ReportPrintJobId() As Integer Implements IBOReportResults.ReportPrintJobId
            Get
                Return mReportPrintJobId
            End Get
            Set(ByVal Value As Integer)
                mReportPrintJobId = Value
            End Set
        End Property
        Private mReportPrintJobId As Int32

        Public Property BoTypeName() As String Implements IBOReportResults.BoTypeName
            Get
                Return mBoTypeName
            End Get
            Set(ByVal Value As String)
                mBoTypeName = Value
            End Set
        End Property
        Private mBoTypeName As String

        Public Property ReportTypeDescription() As String Implements IBOReportResults.ReportTypeDescription
            Get
                Return mReportTypeDescription
            End Get
            Set(ByVal Value As String)
                mReportTypeDescription = Value
            End Set
        End Property
        Private mReportTypeDescription As String

        Public Property PrintJobDescription() As String Implements IBOReportResults.PrintJobDescription
            Get
                Return mPrintJobDescription
            End Get
            Set(ByVal Value As String)
                mPrintJobDescription = Value
            End Set
        End Property
        Private mPrintJobDescription As String

        Public Property ExpiryDate() As Date Implements IBOReportResults.ExpiryDate
            Get
                Return mExpiryDate
            End Get
            Set(ByVal Value As Date)
                mExpiryDate = Value
            End Set
        End Property
        Private mExpiryDate As Date

        Public Property ReportOutput() As Byte() Implements IBOReportResults.ReportOutput
            Get
                Return mOutput
            End Get
            Set(ByVal Value() As Byte)
                mOutput = Value
            End Set
        End Property
        Private mOutput() As Byte

        Public Property OutputType() As Integer Implements IBOReportResults.OutputType
            Get
                Return mOutputType
            End Get
            Set(ByVal Value As Integer)
                mOutputType = Value
            End Set
        End Property
        Private mOutputType As Int32

        Public Property ContentType() As String Implements IBOReportResults.ContentType
            Get
                Return mContentType
            End Get
            Set(ByVal Value As String)
                mContentType = Value
            End Set
        End Property
        Private mContentType As String

        Public Property ReportPrinterId() As Integer Implements IBOReportResults.ReportPrinterId
            Get
                Return mReportPrinterId
            End Get
            Set(ByVal Value As Integer)
                mReportPrinterId = Value
            End Set
        End Property
        Private mReportPrinterId As Int32

        Public Property DataBaseId() As Integer Implements IBOReportResults.DataBaseId
            Get
                Return mDataBaseId
            End Get
            Set(ByVal Value As Integer)
                mDataBaseId = Value
            End Set
        End Property
        Private mDataBaseId As Int32

        Public Property StapleBatch() As Integer Implements IBOReportResults.StapleBatch
            Get
                Return mStapleBatch
            End Get
            Set(ByVal Value As Integer)
                mStapleBatch = Value
            End Set
        End Property
        Private mStapleBatch As Int32

        Public Property StapleEndPage() As Integer Implements IBOReportResults.StapleEndPage
            Get
                Return mStapleEndPage
            End Get
            Set(ByVal Value As Integer)
                mStapleEndPage = Value
            End Set
        End Property
        Private mStapleEndPage As Int32

        Public Property StapleStartPage() As Integer Implements IBOReportResults.StapleStartPage
            Get
                Return mStapleStartPage
            End Get
            Set(ByVal Value As Integer)
                mStapleStartPage = Value
            End Set
        End Property
        Private mStapleStartPage As Int32

        Public Property Size() As Int32 Implements IBOReportResults.Size
            Get
                Return mSize
            End Get
            Set(ByVal Value As Int32)
                mSize = Value
            End Set
        End Property
        Private mSize As Int32

        Public Property PrinterSelection() As uk.gov.defra.Phoenix.BO.ReportData.BoReportPrinter() Implements IBOReportResults.PrinterSelection
            Get
                Return mPrinterSelection
            End Get
            Set(ByVal Value() As uk.gov.defra.Phoenix.BO.ReportData.BoReportPrinter)
                mPrinterSelection = Value
            End Set
        End Property
        Private mPrinterSelection() As ReportData.BoReportPrinter

        Public Property StapleSelection() As uk.gov.defra.Phoenix.BO.ReportData.BoReportStaple() Implements IBOReportResults.StapleSelection
            Get
                Return mStapleSelection
            End Get
            Set(ByVal Value() As uk.gov.defra.Phoenix.BO.ReportData.BoReportStaple)
                mStapleSelection = Value
            End Set
        End Property
        Private mStapleSelection() As ReportData.BoReportStaple

        Public Property Staple() As Integer Implements IBOReportResults.Staple
            Get
                Return mStaple
            End Get
            Set(ByVal Value As Integer)
                mStaple = Value
            End Set
        End Property
        Private mStaple As Int32

        Public Property Description() As String Implements IBOReportResults.Description
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

        Public Overridable Shadows Function Save() As BOReportResults

            ' Clear any errors from stack before we start
            DataObjects.Sprocs.LastError = Nothing

            ' Get ReportTypeId from  criteriaObjectName object type name
            Dim criteriaObjectName As String = mCriteria.GetType.Name
            Dim reportTypeService As New DataObjects.Service.ReportTypeService
            Dim reportTypeSet As DataObjects.EntitySet.ReportTypeSet = reportTypeService.GetByIndex_ReportType(criteriaObjectName, True)
            mTypeId = reportTypeSet.Entities(0).Id
            mBoTypeName = reportTypeSet.Entities(0).BOTypeName
            mReportTypeDescription = reportTypeSet.Entities(0).Description
            mVersion = reportTypeSet.Entities(0).Version()

            ' Create a Transaction
            Dim report As New DataObjects.Entity.Report
            Dim reportService As DataObjects.Service.ReportService = report.ServiceObject
            'Dim tran As SqlClient.SqlTransaction = reportService.BeginTransaction
            Dim tran As SqlClient.SqlTransaction = Nothing

            Dim reportId As Int32 = 0 ' Should be ReSet to Inserted ReportId
            Created = (reportId = 0)

            If Created Then

                ' Insert into Report table
                If mExpiryDate.ToString = "01/01/0001 00:00:00" Then
                    report = reportService.Insert(mCreatedDate, _
                    mTypeId, _
                    mVersion, _
                    mSearchReference, _
                    mDataBaseId, _
                    mReportPrintJobId, _
                    mPrintSequence, _
                    Nothing, _
                    mSize, _
                    mReportPrinterId, _
                    mStaple, _
                    mDescription, _
                    tran)
                Else
                    report = reportService.Insert(mCreatedDate, _
                    mTypeId, _
                    mVersion, _
                    mSearchReference, _
                    mDataBaseId, _
                    mReportPrintJobId, _
                    mPrintSequence, _
                    mExpiryDate, _
                    mSize, _
                    mReportPrinterId, _
                    mStaple, _
                    mDescription, _
                    tran)
                End If


                reportId = report.Id ' Get newly inserted ReportId

                ' Insert into ReportOutput
                Dim unicodeEncoding As New UnicodeEncoding
                Dim output As String = Convert.ToBase64String(mOutput)
                Dim reportOutput As New DataObjects.Entity.ReportOutput
                Dim reportOutputService As DataObjects.Service.ReportOutputService = reportOutput.ServiceObject
                reportOutputService.Insert(reportId, output, tran) ' Insert output Encoded String into DB
                mOutput = Convert.FromBase64String(output)

                ' Insert into ReportRPT
                unicodeEncoding = New UnicodeEncoding
                Dim rpt As String = Convert.ToBase64String(mRPT)
                Dim reportRPT As New DataObjects.Entity.ReportRPT
                Dim reportrptService As DataObjects.Service.ReportRPTService = reportRPT.ServiceObject
                reportrptService.Insert(reportId, rpt, tran) ' Insert RPT Encoded String into DB
                mRPT = Convert.FromBase64String(rpt)

                ' Insert into ReportCriteria table
                Dim stringWriter As New StringWriter
                Dim xmlSerializer As New XmlSerializer(mCriteria.GetType)
                xmlSerializer.Serialize(stringWriter, mCriteria)
                Dim criteriaXml As String = stringWriter.ToString()
                Dim reportCriteria As New DataObjects.Entity.ReportCriteria
                Dim reportCriteriaService As DataObjects.Service.ReportCriteriaService = reportCriteria.ServiceObject
                reportCriteriaService.Insert(reportId, criteriaXml, tran)

                ' Insert into ReportData table
                stringWriter = New StringWriter
                xmlSerializer = New XmlSerializer(mData.GetType)
                xmlSerializer.Serialize(stringWriter, mData)
                Dim dataXml As String = stringWriter.ToString()                
                Dim reportData As New DataObjects.Entity.ReportData
                Dim reportDataService As DataObjects.Service.ReportDataService = reportData.ServiceObject
                reportDataService.Insert(reportId, dataXml, tran)


                ' Do we have any errors?
                If DataObjects.Sprocs.LastError Is Nothing Then

                    ' No Errors - So Commit Transaction 
                    reportService.EndTransaction(tran)

                    mReportId = reportId

                    Return Me

                Else
                    ' We have Errors - So Rollback Transaction

                    'TODO: Use errors collection to check to see if the problem was concurrency
                    reportService.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)

                    Return Nothing
                End If

            Else

                ' We only allow details to be inserted - So raise error
                MyBase.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.ReportUpdateProhibited))

            End If

        End Function

#End Region


    End Class
End Namespace

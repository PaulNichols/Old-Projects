Imports System
Imports System.data

Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do
Imports System.Collections

Namespace RPT

    Public Class ConsignmentPermits
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Private mSpecimenIdx As Int32 = 0
        Private mPageMM As Int32 = -1
        Private mPageNN As Int32 = -1
        Private mNumSpecimens As Int32 = -1

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Throw New NotImplementedException
        End Function

        'Public Function ProcessConsignmentPermits(ByVal reportCriteria As ReportCriteria.ReportCriteria, _      'MLD 26/1/5 no longer needed
        'ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()

        '    Dim consignmentPermitsReportCriteria As ReportCriteria.ConsignmentPermitsReportCriteria = CType(reportCriteria, ReportCriteria.ConsignmentPermitsReportCriteria)
        '    Dim citesSpecimensDataset As New CITESConsignmentPermitsData

        '    Dim citesImportExportPermit As New Application.CITES.Applications.BOCITESImportExportPermit
        '    Dim reportDataResults As ReportDataResults = citesImportExportPermit.GetConsignmentPermitReportData(consignmentPermitsReportCriteria.ApplicationId, consignmentPermitsReportCriteria.Duplicate, False, citesSpecimensDataset.GetXmlSchema)
        '    Dim ReportDataset As DataSet = reportDataResults.ReportData

        '    Dim executeReport As New ExecuteReport
        '    Dim consignmentPermits_RPT As New CITESConsignmentPermits_RPT
        '    Dim reportResults(0) As BOReportResults

        '    Dim parameterValues As New Hashtable
        '    parameterValues.Add("HideDraftCopy", True)

        '    reportResults(0) = executeReport.DoReport(reportCriteria.Description, reportDataResults.SearchReference, consignmentPermitsReportCriteria.ApplicationId, reportDataResults.ReportData, consignmentPermitsReportCriteria, saveReport, _
        '        consignmentPermits_RPT, reportPrintJobId, printSequence, parameterValues)

        '    Return reportResults

        'End Function

        'Public Function ProcessSemiConsignmentPermits(ByVal reportCriteria As ReportCriteria.ReportCriteria, _      'MLD 26/1/5 no longer needed
        'ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()

        '    Dim consignmentSemiPermitsReportCriteria As ReportCriteria.ConsignmentSemiPermitsReportCriteria = CType(reportCriteria, ReportCriteria.ConsignmentSemiPermitsReportCriteria)
        '    Dim citesSpecimensDataset As New CITESConsignmentPermitsData

        '    Dim citesImportExportPermit As New Application.CITES.Applications.BOCITESImportExportPermit
        '    Dim reportDataResults As ReportDataResults = citesImportExportPermit.GetConsignmentPermitReportData(consignmentSemiPermitsReportCriteria.ApplicationId, consignmentSemiPermitsReportCriteria.Duplicate, False, citesSpecimensDataset.GetXmlSchema)
        '    Dim ReportDataset As DataSet = reportDataResults.ReportData

        '    Dim executeReport As New ExecuteReport
        '    Dim consignmentPermits_RPT As New CITESConsignmentPermits_RPT
        '    Dim reportResults(0) As BOReportResults

        '    Dim parameterValues As New Hashtable
        '    parameterValues.Add("HideDraftCopy", True)

        '    reportResults(0) = executeReport.DoReport(reportCriteria.Description, reportDataResults.SearchReference, consignmentSemiPermitsReportCriteria.ApplicationId, reportDataResults.ReportData, consignmentSemiPermitsReportCriteria, saveReport, _
        '        consignmentPermits_RPT, reportPrintJobId, printSequence, parameterValues)

        '    Return reportResults

        'End Function

        '        Public Function ProcessDraftConsignmentPermits(ByVal reportCriteria As ReportCriteria.ReportCriteria, _      'MLD 26/1/5 no longer needed
        'ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()

        '            Dim consignmentDraftPermitsReportCriteria As ReportCriteria.ConsignmentDraftPermitsReportCriteria = CType(reportCriteria, ReportCriteria.ConsignmentDraftPermitsReportCriteria)
        '            Dim citesSpecimensDataset As New CITESConsignmentPermitsData

        '            Dim citesImportExportPermit As New Application.CITES.Applications.BOCITESImportExportPermit
        '            Dim reportDataResults As ReportDataResults = citesImportExportPermit.GetConsignmentPermitReportData(consignmentDraftPermitsReportCriteria.ApplicationId, consignmentDraftPermitsReportCriteria.Duplicate, True, citesSpecimensDataset.GetXmlSchema)
        '            Dim ReportDataset As DataSet = reportDataResults.ReportData

        '            Dim executeReport As New ExecuteReport
        '            Dim consignmentPermits_RPT As New CITESConsignmentPermits_RPT
        '            Dim reportResults(0) As BOReportResults

        '            Dim parameterValues As New Hashtable
        '            parameterValues.Add("HideDraftCopy", False)

        '            reportResults(0) = executeReport.DoReport(reportCriteria.Description, reportDataResults.SearchReference, consignmentDraftPermitsReportCriteria.ApplicationId, reportDataResults.ReportData, consignmentDraftPermitsReportCriteria, saveReport, _
        '                consignmentPermits_RPT, reportPrintJobId, printSequence, parameterValues)

        '            Return reportResults

        '        End Function
    End Class

End Namespace

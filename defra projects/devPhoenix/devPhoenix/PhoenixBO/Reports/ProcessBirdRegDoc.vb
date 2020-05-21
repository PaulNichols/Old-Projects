Imports System
Imports System.data
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Namespace RPT

    Public Class BirdRegDoc
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim birdRegDocCriteria As ReportCriteria.BirdRegDocCriteria = CType(reportCriteria, ReportCriteria.BirdRegDocCriteria)
            Dim birdRegDocDataset As New BirdRegDocData
            
            Dim reportData As New Application.Bird.Reports.ReportData
            Dim reportDataResults As reportDataResults = reportData.GetBirdRegDocReportData(birdRegDocCriteria.ApplicationId, birdRegDocCriteria.SpecimenId, birdRegDocDataset.GetXmlSchema)

            Dim birdRegDoc_Main_RPT As New birdRegDoc_Main_RPT
            Dim reportResults(0) As BOReportResults
            reportResults(0) = DoReport(reportCriteria.Description, reportDataResults.SearchReference, birdRegDocCriteria.ApplicationId, reportDataResults.ReportData, birdRegDocCriteria, saveReport, _
                birdRegDoc_Main_RPT, reportPrintJobId, printSequence)

            Return reportResults

        End Function


        Public Function GetBirdRegDocCriteriaDatasets(ByVal BirdRegDocCriteria As ReportCriteria.BirdRegDocCriteria) As BirdRegDocData()

            Dim BirdRegDocDataset As New BirdRegDocData
            Dim boBirdRegDoc_MainRow As BirdRegDocData.BOBirdRegDoc_MainRow

            ' BOBirdRegDoc_MainRow - Row 0
            boBirdRegDoc_MainRow = BirdRegDocDataset.BOBirdRegDoc_Main.NewBOBirdRegDoc_MainRow()
            boBirdRegDoc_MainRow.ApplicationId = 1
            boBirdRegDoc_MainRow.IssueDate = "12/04/2004"
            boBirdRegDoc_MainRow.DocumentNo = "123456/00"
            boBirdRegDoc_MainRow.PreviousDocumentNo = ""

            boBirdRegDoc_MainRow.KeeperDetails = "Defra Id: 12345" & Environment.NewLine & _
            "Mr G Figoni" & Environment.NewLine & _
            "Government Buildings" & Environment.NewLine & _
            "Epsom road" & Environment.NewLine & _
            "Guildford" & Environment.NewLine & _
            "Surrey" & Environment.NewLine & _
            "GU1 2LD"

            boBirdRegDoc_MainRow.IdMarksTypes = "ID Mark & Type: DEFRA CLOSED RING 9876A" & Environment.NewLine & _
            "ID Mark & Type: SPLIT RING B3535" & Environment.NewLine & _
            "ID Mark & Type: SWISS RING 1F45450"

            boBirdRegDoc_MainRow.Species = "AVICEDA MADAGASCARIENSIS"
            boBirdRegDoc_MainRow.CommonName = "MADAGASCAR CUCKOO-FALCON"
            boBirdRegDoc_MainRow.Origin = "IMPORTED"
            boBirdRegDoc_MainRow.DateHatched = "01/07/1999"
            boBirdRegDoc_MainRow.DateAcquired = "03/04/2004"
            boBirdRegDoc_MainRow.Sex = "MALE"
            boBirdRegDoc_MainRow.LicenceNumber = ""
            boBirdRegDoc_MainRow.MaleParentIdMarkType = "CLOSED RING 1F5569"
            boBirdRegDoc_MainRow.FemaleParentIdMarkType = "SPLIT RING CD1296"
            boBirdRegDoc_MainRow.ShowRegulation8 = True
            boBirdRegDoc_MainRow.PchIdMarkType = "DEFRA CLOSED RING 9876A"
            boBirdRegDoc_MainRow.pchSpecies = "AVICEDA MADAGASCARIENSIS"
            boBirdRegDoc_MainRow.PchCommonName = "MADAGASCAR CUCKOO-FALCON"
            boBirdRegDoc_MainRow.PchPresentKeeper = "Mr G. Figoni"
            boBirdRegDoc_MainRow.PchIdNo = "12345"

            BirdRegDocDataset.BOBirdRegDoc_Main.AddBOBirdRegDoc_MainRow(boBirdRegDoc_MainRow)

            BirdRegDocDataset.AcceptChanges()

            Dim BirdRegDocDatasets(0) As BirdRegDocData
            BirdRegDocDatasets(0) = BirdRegDocDataset

            Return BirdRegDocDatasets

        End Function

    End Class

End Namespace
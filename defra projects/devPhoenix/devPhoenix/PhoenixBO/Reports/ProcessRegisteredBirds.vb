Imports System
Imports System.data
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Namespace RPT

    Public Class RegisteredBirds
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim registeredBirdsReportCriteria As ReportCriteria.RegisteredBirdsReportCriteria = CType(reportCriteria, ReportCriteria.RegisteredBirdsReportCriteria)
            Dim registeredBirdsData As New RegisteredBirdsData

            Dim reportData As New Application.Bird.Reports.ReportData
            Dim reportDataResults As ReportDataResults = reportData.GetRegisteredBirdsReportData(registeredBirdsReportCriteria.PartyId, registeredBirdsReportCriteria.CurrentlyRegistered, registeredBirdsReportCriteria.PreviouslyRegistered, registeredBirdsData.GetXmlSchema)
            Dim ReportDataset As DataSet = reportDataResults.ReportData

            Dim registeredBirds_RPT As New RegisteredBirds_RPT
            Dim reportResults(0) As BOReportResults
            reportResults(0) = DoReport(reportCriteria.Description, reportDataResults.SearchReference, registeredBirdsReportCriteria.PartyId, reportDataResults.ReportData, registeredBirdsReportCriteria, saveReport, _
                registeredBirds_RPT, reportPrintJobId, printSequence)

            Return reportResults

        End Function


        Public Function GetRegisteredBirdsReportDatasets(ByVal RegisteredBirdsReportCriteria As ReportCriteria.RegisteredBirdsReportCriteria) As RegisteredBirdsData()

            Dim RegBirdsDataset As New RegisteredBirdsData

            Dim boKeeperRow As RegisteredBirdsData.BOKeeperRow
            boKeeperRow = RegBirdsDataset.BOKeeper.NewBOKeeperRow()
            boKeeperRow.HolderId = RegisteredBirdsReportCriteria.PartyId
            boKeeperRow.NameAddress = "FIGONI" & Environment.NewLine & _
            "56 WINNINGTON ROAD" & Environment.NewLine & _
            "ENFIELD" & Environment.NewLine & _
            "EN3 5RJ"
            RegBirdsDataset.BOKeeper.AddBOKeeperRow(boKeeperRow)

            Dim boRegisteredBirdsRow As RegisteredBirdsData.BORegisteredBirdsRow

            boRegisteredBirdsRow = RegBirdsDataset.BORegisteredBirds.NewBORegisteredBirdsRow()
            boRegisteredBirdsRow.HolderId = RegisteredBirdsReportCriteria.PartyId
            boRegisteredBirdsRow.SpeciesName = "GOSHAWK"
            boRegisteredBirdsRow.NumberSpecimens = "2"
            boRegisteredBirdsRow.FateName = "Died"
            boRegisteredBirdsRow.FateDate = "11/06/1995"
            RegBirdsDataset.BORegisteredBirds.AddBORegisteredBirdsRow(boRegisteredBirdsRow)

            boRegisteredBirdsRow = RegBirdsDataset.BORegisteredBirds.NewBORegisteredBirdsRow()
            boRegisteredBirdsRow.HolderId = RegisteredBirdsReportCriteria.PartyId
            boRegisteredBirdsRow.SpeciesName = ""
            boRegisteredBirdsRow.NumberSpecimens = ""
            boRegisteredBirdsRow.FateName = ""
            boRegisteredBirdsRow.FateDate = "13/05/1995"
            RegBirdsDataset.BORegisteredBirds.AddBORegisteredBirdsRow(boRegisteredBirdsRow)

            boRegisteredBirdsRow = RegBirdsDataset.BORegisteredBirds.NewBORegisteredBirdsRow()
            boRegisteredBirdsRow.HolderId = RegisteredBirdsReportCriteria.PartyId
            boRegisteredBirdsRow.SpeciesName = "GOSHAWK"
            boRegisteredBirdsRow.NumberSpecimens = "1"
            boRegisteredBirdsRow.FateName = "Stolen"
            boRegisteredBirdsRow.FateDate = "03/07/1999"
            RegBirdsDataset.BORegisteredBirds.AddBORegisteredBirdsRow(boRegisteredBirdsRow)

            boRegisteredBirdsRow = RegBirdsDataset.BORegisteredBirds.NewBORegisteredBirdsRow()
            boRegisteredBirdsRow.HolderId = RegisteredBirdsReportCriteria.PartyId
            boRegisteredBirdsRow.SpeciesName = "FALCON"
            boRegisteredBirdsRow.NumberSpecimens = "4"
            boRegisteredBirdsRow.FateName = "Died"
            boRegisteredBirdsRow.FateDate = "09/05/2001"
            RegBirdsDataset.BORegisteredBirds.AddBORegisteredBirdsRow(boRegisteredBirdsRow)

            boRegisteredBirdsRow = RegBirdsDataset.BORegisteredBirds.NewBORegisteredBirdsRow()
            boRegisteredBirdsRow.HolderId = RegisteredBirdsReportCriteria.PartyId
            boRegisteredBirdsRow.SpeciesName = ""
            boRegisteredBirdsRow.NumberSpecimens = ""
            boRegisteredBirdsRow.FateName = ""
            boRegisteredBirdsRow.FateDate = "13/06/2003"
            RegBirdsDataset.BORegisteredBirds.AddBORegisteredBirdsRow(boRegisteredBirdsRow)

            boRegisteredBirdsRow = RegBirdsDataset.BORegisteredBirds.NewBORegisteredBirdsRow()
            boRegisteredBirdsRow.HolderId = RegisteredBirdsReportCriteria.PartyId
            boRegisteredBirdsRow.SpeciesName = ""
            boRegisteredBirdsRow.NumberSpecimens = ""
            boRegisteredBirdsRow.FateName = ""
            boRegisteredBirdsRow.FateDate = "17/06/2003"
            RegBirdsDataset.BORegisteredBirds.AddBORegisteredBirdsRow(boRegisteredBirdsRow)

            For idx As Int32 = 0 To 150

                boRegisteredBirdsRow = RegBirdsDataset.BORegisteredBirds.NewBORegisteredBirdsRow()
                boRegisteredBirdsRow.HolderId = RegisteredBirdsReportCriteria.PartyId
                boRegisteredBirdsRow.SpeciesName = "SpeciesName" & idx.ToString
                boRegisteredBirdsRow.NumberSpecimens = idx.ToString
                boRegisteredBirdsRow.FateName = "NA"
                boRegisteredBirdsRow.FateDate = Date.Now.ToString("dd/mm/yyyy")
                RegBirdsDataset.BORegisteredBirds.AddBORegisteredBirdsRow(boRegisteredBirdsRow)

            Next

            RegBirdsDataset.AcceptChanges()

            Dim RegBirdsDatasets(0) As RegisteredBirdsData
            RegBirdsDatasets(0) = RegBirdsDataset

            Return RegBirdsDatasets

        End Function

    End Class

End Namespace

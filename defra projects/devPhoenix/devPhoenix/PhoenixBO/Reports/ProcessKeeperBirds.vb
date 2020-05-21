Imports System
Imports System.data
Imports System.Collections
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Namespace RPT

    Public Class KeeperBirds
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim keeperBirdsReportCriteria As ReportCriteria.KeeperBirdsReportCriteria = CType(reportCriteria, ReportCriteria.KeeperBirdsReportCriteria)
            Dim keeperBirdsDataset As New KeeperBirdsData

            Dim reportData As New Application.Bird.Reports.ReportData
            Dim reportDataResults As ReportDataResults = reportData.GetKeeperBirdReportData(keeperBirdsReportCriteria.PartyId, keeperBirdsDataset.GetXmlSchema)
            Dim ReportDataset As DataSet = reportDataResults.ReportData

            Dim keeperBirdDetails_RPT As New KeeperBirds_RPT
            Dim reportResults(0) As BOReportResults
            reportResults(0) = DoReport(reportCriteria.Description, reportDataResults.SearchReference, keeperBirdsReportCriteria.PartyId, reportDataResults.ReportData, keeperBirdsReportCriteria, saveReport, _
                keeperBirdDetails_RPT, reportPrintJobId, printSequence)

            Return reportResults

        End Function


        Public Function GetKeeperBirdsReportDatasets(ByVal KeeperBirdsReportCriteria As ReportCriteria.KeeperBirdsReportCriteria) As KeeperBirdsData()

            Dim RegBirdsDataset As New KeeperBirdsData
            Dim boKeeperBirdsRow As KeeperBirdsData.BOKeeperBirdsRow
            Dim boKeeperRow As KeeperBirdsData.BOKeeperRow

            ' Primary Address
            boKeeperRow = RegBirdsDataset.BOKeeper.NewBOKeeperRow()
            boKeeperRow.KeeperIdx = 0
            boKeeperRow.KeeperDetails = "PRIMARY ADDDRESS " & Environment.NewLine & _
            "MR GIOVANNI FIGONI" & Environment.NewLine & _
            "ENFIELD" & Environment.NewLine & _
            "MIDDLESEX" & Environment.NewLine & _
            "EN3 5RJ"
            RegBirdsDataset.BOKeeper.AddBOKeeperRow(boKeeperRow)

            ' Primary Bird Details - Row 0
            boKeeperBirdsRow = RegBirdsDataset.BOKeeperBirds.NewBOKeeperBirdsRow()
            boKeeperBirdsRow.KeeperIdx = 0
            boKeeperBirdsRow.IdMarkType = "DEFRA ring"
            boKeeperBirdsRow.IdMarkNumber = "9452V"
            boKeeperBirdsRow.Origin = "C"
            boKeeperBirdsRow.SpeciesName = "GOSHAWK"
            boKeeperBirdsRow.Gender = "M"
            boKeeperBirdsRow.HatchDate = "11/06/1995"
            RegBirdsDataset.BOKeeperBirds.AddBOKeeperBirdsRow(boKeeperBirdsRow)

            ' Primary Bird Details - Row 2
            boKeeperBirdsRow = RegBirdsDataset.BOKeeperBirds.NewBOKeeperBirdsRow()
            boKeeperBirdsRow.KeeperIdx = 0
            boKeeperBirdsRow.IdMarkType = "DEFRA ring"
            boKeeperBirdsRow.IdMarkNumber = "6071V"
            boKeeperBirdsRow.Origin = "C"
            boKeeperBirdsRow.SpeciesName = "GOSHAWK"
            boKeeperBirdsRow.Gender = "M"
            boKeeperBirdsRow.HatchDate = "13/05/1999"
            RegBirdsDataset.BOKeeperBirds.AddBOKeeperBirdsRow(boKeeperBirdsRow)

            ' Primary Bird Details - Row 3
            boKeeperBirdsRow = RegBirdsDataset.BOKeeperBirds.NewBOKeeperBirdsRow()
            boKeeperBirdsRow.KeeperIdx = 0
            boKeeperBirdsRow.IdMarkType = "DEFRA ring"
            boKeeperBirdsRow.IdMarkNumber = "G00293"
            boKeeperBirdsRow.Origin = "IW"
            boKeeperBirdsRow.SpeciesName = "GOSHAWK"
            boKeeperBirdsRow.Gender = "M"
            boKeeperBirdsRow.HatchDate = ""
            RegBirdsDataset.BOKeeperBirds.AddBOKeeperBirdsRow(boKeeperBirdsRow)

            ' Primary Bird Details - Row 4
            boKeeperBirdsRow = RegBirdsDataset.BOKeeperBirds.NewBOKeeperBirdsRow()
            boKeeperBirdsRow.KeeperIdx = 0
            boKeeperBirdsRow.IdMarkType = "Microchip"
            boKeeperBirdsRow.IdMarkNumber = "631848131"
            boKeeperBirdsRow.Origin = ""
            boKeeperBirdsRow.SpeciesName = ""
            boKeeperBirdsRow.Gender = ""
            boKeeperBirdsRow.HatchDate = ""
            RegBirdsDataset.BOKeeperBirds.AddBOKeeperBirdsRow(boKeeperBirdsRow)

            ' ----------

            ' Other Address - Row 1
            boKeeperRow = RegBirdsDataset.BOKeeper.NewBOKeeperRow()
            boKeeperRow.KeeperIdx = 1
            boKeeperRow.KeeperDetails = "OTHER ADDDRESS " & Environment.NewLine & _
            "18 WESTBURY AVENUE" & Environment.NewLine & _
            "WOOD GREEN" & Environment.NewLine & _
            "LONDON" & Environment.NewLine & _
            "N22 6RS"
            RegBirdsDataset.BOKeeper.AddBOKeeperRow(boKeeperRow)

            ' Other Bird Details - Row 1
            boKeeperBirdsRow = RegBirdsDataset.BOKeeperBirds.NewBOKeeperBirdsRow()
            boKeeperBirdsRow.KeeperIdx = 1
            boKeeperBirdsRow.IdMarkType = "DEFRA ring"
            boKeeperBirdsRow.IdMarkNumber = "H00372"
            boKeeperBirdsRow.Origin = "IW"
            boKeeperBirdsRow.SpeciesName = "FALCON"
            boKeeperBirdsRow.Gender = "F"
            boKeeperBirdsRow.HatchDate = ""
            RegBirdsDataset.BOKeeperBirds.AddBOKeeperBirdsRow(boKeeperBirdsRow)

            ' Other Bird Details - Row 2
            boKeeperBirdsRow = RegBirdsDataset.BOKeeperBirds.NewBOKeeperBirdsRow()
            boKeeperBirdsRow.KeeperIdx = 1
            boKeeperBirdsRow.IdMarkType = "Microchip"
            boKeeperBirdsRow.IdMarkNumber = "889222332"
            boKeeperBirdsRow.Origin = ""
            boKeeperBirdsRow.SpeciesName = ""
            boKeeperBirdsRow.Gender = ""
            boKeeperBirdsRow.HatchDate = ""
            RegBirdsDataset.BOKeeperBirds.AddBOKeeperBirdsRow(boKeeperBirdsRow)

            ' Other Bird Details - Row 3
            boKeeperBirdsRow = RegBirdsDataset.BOKeeperBirds.NewBOKeeperBirdsRow()
            boKeeperBirdsRow.KeeperIdx = 1
            boKeeperBirdsRow.IdMarkType = "DEFRA ring"
            boKeeperBirdsRow.IdMarkNumber = "H00388"
            boKeeperBirdsRow.Origin = "IW"
            boKeeperBirdsRow.SpeciesName = "FALCON"
            boKeeperBirdsRow.Gender = "F"
            boKeeperBirdsRow.HatchDate = ""
            RegBirdsDataset.BOKeeperBirds.AddBOKeeperBirdsRow(boKeeperBirdsRow)

            ' ----------

            ' Other Address - Row 2
            boKeeperRow = RegBirdsDataset.BOKeeper.NewBOKeeperRow()
            boKeeperRow.KeeperIdx = 2
            boKeeperRow.KeeperDetails = "OTHER ADDDRESS " & Environment.NewLine & _
            "18 WESTBURY AVENUE" & Environment.NewLine & _
            "WOOD GREEN" & Environment.NewLine & _
            "LONDON" & Environment.NewLine & _
            "N22 6RS"
            RegBirdsDataset.BOKeeper.AddBOKeeperRow(boKeeperRow)

            ' Other Bird Details - Row 1
            boKeeperBirdsRow = RegBirdsDataset.BOKeeperBirds.NewBOKeeperBirdsRow()
            boKeeperBirdsRow.KeeperIdx = 2
            boKeeperBirdsRow.IdMarkType = "DEFRA ring"
            boKeeperBirdsRow.IdMarkNumber = "H00372"
            boKeeperBirdsRow.Origin = "IW"
            boKeeperBirdsRow.SpeciesName = "FALCON"
            boKeeperBirdsRow.Gender = "F"
            boKeeperBirdsRow.HatchDate = ""
            RegBirdsDataset.BOKeeperBirds.AddBOKeeperBirdsRow(boKeeperBirdsRow)

            ' Other Bird Details - Row 2
            boKeeperBirdsRow = RegBirdsDataset.BOKeeperBirds.NewBOKeeperBirdsRow()
            boKeeperBirdsRow.KeeperIdx = 2
            boKeeperBirdsRow.IdMarkType = "Microchip"
            boKeeperBirdsRow.IdMarkNumber = "889222332"
            boKeeperBirdsRow.Origin = ""
            boKeeperBirdsRow.SpeciesName = ""
            boKeeperBirdsRow.Gender = ""
            boKeeperBirdsRow.HatchDate = ""
            RegBirdsDataset.BOKeeperBirds.AddBOKeeperBirdsRow(boKeeperBirdsRow)

            ' Other Bird Details - Row 3
            boKeeperBirdsRow = RegBirdsDataset.BOKeeperBirds.NewBOKeeperBirdsRow()
            boKeeperBirdsRow.KeeperIdx = 2
            boKeeperBirdsRow.IdMarkType = "DEFRA ring"
            boKeeperBirdsRow.IdMarkNumber = "H00388"
            boKeeperBirdsRow.Origin = "IW"
            boKeeperBirdsRow.SpeciesName = "FALCON"
            boKeeperBirdsRow.Gender = "F"
            boKeeperBirdsRow.HatchDate = ""
            RegBirdsDataset.BOKeeperBirds.AddBOKeeperBirdsRow(boKeeperBirdsRow)

            ' ----------

            ' Other Address - Row 2
            boKeeperRow = RegBirdsDataset.BOKeeper.NewBOKeeperRow()
            boKeeperRow.KeeperIdx = 3
            boKeeperRow.KeeperDetails = "OTHER ADDDRESS " & Environment.NewLine & _
            "18 WESTBURY AVENUE" & Environment.NewLine & _
            "WOOD GREEN" & Environment.NewLine & _
            "LONDON" & Environment.NewLine & _
            "N22 6RS"
            RegBirdsDataset.BOKeeper.AddBOKeeperRow(boKeeperRow)

            ' Other Bird Details - Row 1
            boKeeperBirdsRow = RegBirdsDataset.BOKeeperBirds.NewBOKeeperBirdsRow()
            boKeeperBirdsRow.KeeperIdx = 3
            boKeeperBirdsRow.IdMarkType = "DEFRA ring"
            boKeeperBirdsRow.IdMarkNumber = "H00372"
            boKeeperBirdsRow.Origin = "IW"
            boKeeperBirdsRow.SpeciesName = "FALCON"
            boKeeperBirdsRow.Gender = "F"
            boKeeperBirdsRow.HatchDate = ""
            RegBirdsDataset.BOKeeperBirds.AddBOKeeperBirdsRow(boKeeperBirdsRow)

            ' Other Bird Details - Row 2
            boKeeperBirdsRow = RegBirdsDataset.BOKeeperBirds.NewBOKeeperBirdsRow()
            boKeeperBirdsRow.KeeperIdx = 3
            boKeeperBirdsRow.IdMarkType = "Microchip"
            boKeeperBirdsRow.IdMarkNumber = "889222332"
            boKeeperBirdsRow.Origin = ""
            boKeeperBirdsRow.SpeciesName = ""
            boKeeperBirdsRow.Gender = ""
            boKeeperBirdsRow.HatchDate = ""
            RegBirdsDataset.BOKeeperBirds.AddBOKeeperBirdsRow(boKeeperBirdsRow)

            ' Other Bird Details - Row 3
            boKeeperBirdsRow = RegBirdsDataset.BOKeeperBirds.NewBOKeeperBirdsRow()
            boKeeperBirdsRow.KeeperIdx = 3
            boKeeperBirdsRow.IdMarkType = "DEFRA ring"
            boKeeperBirdsRow.IdMarkNumber = "H00388"
            boKeeperBirdsRow.Origin = "IW"
            boKeeperBirdsRow.SpeciesName = "FALCON"
            boKeeperBirdsRow.Gender = "F"
            boKeeperBirdsRow.HatchDate = ""
            RegBirdsDataset.BOKeeperBirds.AddBOKeeperBirdsRow(boKeeperBirdsRow)

            ' ----------

            ' Other Address - Row 2
            boKeeperRow = RegBirdsDataset.BOKeeper.NewBOKeeperRow()
            boKeeperRow.KeeperIdx = 4
            boKeeperRow.KeeperDetails = "OTHER ADDDRESS " & Environment.NewLine & _
            "18 WESTBURY AVENUE" & Environment.NewLine & _
            "WOOD GREEN" & Environment.NewLine & _
            "LONDON" & Environment.NewLine & _
            "N22 6RS"
            RegBirdsDataset.BOKeeper.AddBOKeeperRow(boKeeperRow)

            ' Other Bird Details - Row 1
            boKeeperBirdsRow = RegBirdsDataset.BOKeeperBirds.NewBOKeeperBirdsRow()
            boKeeperBirdsRow.KeeperIdx = 4
            boKeeperBirdsRow.IdMarkType = "DEFRA ring"
            boKeeperBirdsRow.IdMarkNumber = "H00372"
            boKeeperBirdsRow.Origin = "IW"
            boKeeperBirdsRow.SpeciesName = "FALCON"
            boKeeperBirdsRow.Gender = "F"
            boKeeperBirdsRow.HatchDate = ""
            RegBirdsDataset.BOKeeperBirds.AddBOKeeperBirdsRow(boKeeperBirdsRow)

            ' Other Bird Details - Row 2
            boKeeperBirdsRow = RegBirdsDataset.BOKeeperBirds.NewBOKeeperBirdsRow()
            boKeeperBirdsRow.KeeperIdx = 4
            boKeeperBirdsRow.IdMarkType = "Microchip"
            boKeeperBirdsRow.IdMarkNumber = "889222332"
            boKeeperBirdsRow.Origin = ""
            boKeeperBirdsRow.SpeciesName = ""
            boKeeperBirdsRow.Gender = ""
            boKeeperBirdsRow.HatchDate = ""
            RegBirdsDataset.BOKeeperBirds.AddBOKeeperBirdsRow(boKeeperBirdsRow)

            ' Other Bird Details - Row 3
            boKeeperBirdsRow = RegBirdsDataset.BOKeeperBirds.NewBOKeeperBirdsRow()
            boKeeperBirdsRow.KeeperIdx = 4
            boKeeperBirdsRow.IdMarkType = "DEFRA ring"
            boKeeperBirdsRow.IdMarkNumber = "H00388"
            boKeeperBirdsRow.Origin = "IW"
            boKeeperBirdsRow.SpeciesName = "FALCON"
            boKeeperBirdsRow.Gender = "F"
            boKeeperBirdsRow.HatchDate = ""
            RegBirdsDataset.BOKeeperBirds.AddBOKeeperBirdsRow(boKeeperBirdsRow)

            ' ----------

            ' Other Address - Row 2
            boKeeperRow = RegBirdsDataset.BOKeeper.NewBOKeeperRow()
            boKeeperRow.KeeperIdx = 5
            boKeeperRow.KeeperDetails = "OTHER ADDDRESS " & Environment.NewLine & _
            "18 WESTBURY AVENUE" & Environment.NewLine & _
            "WOOD GREEN" & Environment.NewLine & _
            "LONDON" & Environment.NewLine & _
            "N22 6RS"
            RegBirdsDataset.BOKeeper.AddBOKeeperRow(boKeeperRow)

            ' Other Bird Details - Row 1
            boKeeperBirdsRow = RegBirdsDataset.BOKeeperBirds.NewBOKeeperBirdsRow()
            boKeeperBirdsRow.KeeperIdx = 5
            boKeeperBirdsRow.IdMarkType = "DEFRA ring"
            boKeeperBirdsRow.IdMarkNumber = "H00372"
            boKeeperBirdsRow.Origin = "IW"
            boKeeperBirdsRow.SpeciesName = "FALCON"
            boKeeperBirdsRow.Gender = "F"
            boKeeperBirdsRow.HatchDate = ""
            RegBirdsDataset.BOKeeperBirds.AddBOKeeperBirdsRow(boKeeperBirdsRow)

            ' Other Bird Details - Row 2
            boKeeperBirdsRow = RegBirdsDataset.BOKeeperBirds.NewBOKeeperBirdsRow()
            boKeeperBirdsRow.KeeperIdx = 5
            boKeeperBirdsRow.IdMarkType = "Microchip"
            boKeeperBirdsRow.IdMarkNumber = "889222332"
            boKeeperBirdsRow.Origin = ""
            boKeeperBirdsRow.SpeciesName = ""
            boKeeperBirdsRow.Gender = ""
            boKeeperBirdsRow.HatchDate = ""
            RegBirdsDataset.BOKeeperBirds.AddBOKeeperBirdsRow(boKeeperBirdsRow)

            ' Other Bird Details - Row 3
            boKeeperBirdsRow = RegBirdsDataset.BOKeeperBirds.NewBOKeeperBirdsRow()
            boKeeperBirdsRow.KeeperIdx = 5
            boKeeperBirdsRow.IdMarkType = "DEFRA ring"
            boKeeperBirdsRow.IdMarkNumber = "H00388"
            boKeeperBirdsRow.Origin = "IW"
            boKeeperBirdsRow.SpeciesName = "FALCON"
            boKeeperBirdsRow.Gender = "F"
            boKeeperBirdsRow.HatchDate = ""
            RegBirdsDataset.BOKeeperBirds.AddBOKeeperBirdsRow(boKeeperBirdsRow)

            ' ----------

            ' Other Address - Row 2
            boKeeperRow = RegBirdsDataset.BOKeeper.NewBOKeeperRow()
            boKeeperRow.KeeperIdx = 6
            boKeeperRow.KeeperDetails = "OTHER ADDDRESS " & Environment.NewLine & _
            "18 WESTBURY AVENUE" & Environment.NewLine & _
            "WOOD GREEN" & Environment.NewLine & _
            "LONDON" & Environment.NewLine & _
            "N22 6RS"
            RegBirdsDataset.BOKeeper.AddBOKeeperRow(boKeeperRow)

            ' Other Bird Details - Row 1
            boKeeperBirdsRow = RegBirdsDataset.BOKeeperBirds.NewBOKeeperBirdsRow()
            boKeeperBirdsRow.KeeperIdx = 6
            boKeeperBirdsRow.IdMarkType = "DEFRA ring"
            boKeeperBirdsRow.IdMarkNumber = "H00372"
            boKeeperBirdsRow.Origin = "IW"
            boKeeperBirdsRow.SpeciesName = "FALCON"
            boKeeperBirdsRow.Gender = "F"
            boKeeperBirdsRow.HatchDate = ""
            RegBirdsDataset.BOKeeperBirds.AddBOKeeperBirdsRow(boKeeperBirdsRow)

            ' Other Bird Details - Row 2
            boKeeperBirdsRow = RegBirdsDataset.BOKeeperBirds.NewBOKeeperBirdsRow()
            boKeeperBirdsRow.KeeperIdx = 6
            boKeeperBirdsRow.IdMarkType = "Microchip"
            boKeeperBirdsRow.IdMarkNumber = "889222332"
            boKeeperBirdsRow.Origin = ""
            boKeeperBirdsRow.SpeciesName = ""
            boKeeperBirdsRow.Gender = ""
            boKeeperBirdsRow.HatchDate = ""
            RegBirdsDataset.BOKeeperBirds.AddBOKeeperBirdsRow(boKeeperBirdsRow)

            ' Other Bird Details - Row 3
            boKeeperBirdsRow = RegBirdsDataset.BOKeeperBirds.NewBOKeeperBirdsRow()
            boKeeperBirdsRow.KeeperIdx = 6
            boKeeperBirdsRow.IdMarkType = "DEFRA ring"
            boKeeperBirdsRow.IdMarkNumber = "H00388"
            boKeeperBirdsRow.Origin = "IW"
            boKeeperBirdsRow.SpeciesName = "FALCON"
            boKeeperBirdsRow.Gender = "F"
            boKeeperBirdsRow.HatchDate = ""
            RegBirdsDataset.BOKeeperBirds.AddBOKeeperBirdsRow(boKeeperBirdsRow)

            ' ----------

            ' Birds at Primary Address Not Registered - Row 2
            boKeeperRow = RegBirdsDataset.BOKeeper.NewBOKeeperRow()
            boKeeperRow.KeeperIdx = 7
            boKeeperRow.KeeperDetails = "BIRDS AT PRIMARY ADDDRESS NOT REGISTERED TO KEEPER"
            RegBirdsDataset.BOKeeper.AddBOKeeperRow(boKeeperRow)

            ' Birds at Primary Address Not Registered Details - Row 1
            boKeeperBirdsRow = RegBirdsDataset.BOKeeperBirds.NewBOKeeperBirdsRow()
            boKeeperBirdsRow.KeeperIdx = 7
            boKeeperBirdsRow.IdMarkType = "DEFRA ring"
            boKeeperBirdsRow.IdMarkNumber = "13713V"
            boKeeperBirdsRow.Origin = "C"
            boKeeperBirdsRow.SpeciesName = "GOSHAWK"
            boKeeperBirdsRow.Gender = "F"
            boKeeperBirdsRow.HatchDate = "13/06/2003"
            RegBirdsDataset.BOKeeperBirds.AddBOKeeperBirdsRow(boKeeperBirdsRow)

            ' Birds at Primary Address Not Registered Details - Row 2
            boKeeperBirdsRow = RegBirdsDataset.BOKeeperBirds.NewBOKeeperBirdsRow()
            boKeeperBirdsRow.KeeperIdx = 7
            boKeeperBirdsRow.IdMarkType = "Microchip"
            boKeeperBirdsRow.IdMarkNumber = "702300256"
            boKeeperBirdsRow.Origin = ""
            boKeeperBirdsRow.SpeciesName = ""
            boKeeperBirdsRow.Gender = ""
            boKeeperBirdsRow.HatchDate = ""
            RegBirdsDataset.BOKeeperBirds.AddBOKeeperBirdsRow(boKeeperBirdsRow)

            ' Birds at Primary Address Not Registered Details - Row 3
            boKeeperBirdsRow = RegBirdsDataset.BOKeeperBirds.NewBOKeeperBirdsRow()
            boKeeperBirdsRow.KeeperIdx = 7
            boKeeperBirdsRow.IdMarkType = "DEFRA ring"
            boKeeperBirdsRow.IdMarkNumber = "13765V"
            boKeeperBirdsRow.Origin = "C"
            boKeeperBirdsRow.SpeciesName = "GOSHAWK"
            boKeeperBirdsRow.Gender = "M"
            boKeeperBirdsRow.HatchDate = "17/06/2003"
            RegBirdsDataset.BOKeeperBirds.AddBOKeeperBirdsRow(boKeeperBirdsRow)

            RegBirdsDataset.AcceptChanges()

            Dim RegBirdsDatasets(0) As KeeperBirdsData
            RegBirdsDatasets(0) = RegBirdsDataset

            Return RegBirdsDatasets

        End Function

    End Class

End Namespace
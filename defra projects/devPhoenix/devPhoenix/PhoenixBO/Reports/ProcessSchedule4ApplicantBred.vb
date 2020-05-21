
Imports System
Imports System.data

Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Imports uk.gov.defra.Phoenix.PhoenixReport

Namespace RPT
    Public Class Schedule4ApplicantBred
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim schedule4ApplicantBredCriteria As reportCriteria.Schedule4ApplicantBredCriteria = CType(reportCriteria, reportCriteria.Schedule4ApplicantBredCriteria)
            Dim schedule4ApplicantBredDataset As New Schedule4ApplicantBredData

            Dim reportData As New Application.Bird.Reports.ReportData
            Dim reportDataResults As reportDataResults = reportData.GetSchedule4ApplicantBredReportData(schedule4ApplicantBredCriteria.ApplicationId, schedule4ApplicantBredDataset.GetXmlSchema)
            Dim ReportDataset As DataSet = reportDataResults.ReportData

            Dim Schedule4ApplicantBred_Main_RPT As New Schedule4ApplicantBred_Main_RPT
            Dim reportResults(0) As BOReportResults
            reportResults(0) = DoReport(reportCriteria.Description, reportDataResults.SearchReference, schedule4ApplicantBredCriteria.ApplicationId, reportDataResults.ReportData, schedule4ApplicantBredCriteria, saveReport, _
                Schedule4ApplicantBred_Main_RPT, reportPrintJobId, printSequence)

            Return reportResults


        End Function


        Public Function GetSchedule4ApplicantBredCriteriaDatasets(ByVal Schedule4ApplicantBredCriteria As ReportCriteria.Schedule4ApplicantBredCriteria) As Schedule4ApplicantBredData()

            Dim Schedule4ApplicantBredDataset As New Schedule4ApplicantBredData
            Dim boSchedule4ApplicantBred_MainRow As Schedule4ApplicantBredData.BOSchedule4ApplicantBred_MainRow
            Dim boSchedule4ApplicantBred_Sub1Row As Schedule4ApplicantBredData.BOSchedule4ApplicantBred_Sub1Row
            Dim boSchedule4ApplicantBred_Sub2Row As Schedule4ApplicantBredData.BOSchedule4ApplicantBred_Sub2Row

            ' BOSchedule4ApplicantBred_MainRow - Row 0
            boSchedule4ApplicantBred_MainRow = Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Main.NewBOSchedule4ApplicantBred_MainRow()
            boSchedule4ApplicantBred_MainRow.ApplicationId = 1
            boSchedule4ApplicantBred_MainRow.InspectionSection = True
            boSchedule4ApplicantBred_MainRow.KeeperDetails = "Defra Id: 12345" & Environment.NewLine & _
            "Mr G Figoni" & Environment.NewLine & _
            "Government Buildings" & Environment.NewLine & _
            "Epsom road" & Environment.NewLine & _
            "Guildford" & Environment.NewLine & _
            "Surrey" & Environment.NewLine & _
            "GU1 2LD"
            boSchedule4ApplicantBred_MainRow.ApplicationRef = "123457"
            boSchedule4ApplicantBred_MainRow.NumOfEggs = "1"
            boSchedule4ApplicantBred_MainRow.LastEggLaid = "14/04/2004"
            boSchedule4ApplicantBred_MainRow.MaxSignatures = 1
            boSchedule4ApplicantBred_MainRow.InspectionSection = True
            boSchedule4ApplicantBred_MainRow.Convict5YearsNoX = ""
            boSchedule4ApplicantBred_MainRow.Convict5YearsYesX = ""
            boSchedule4ApplicantBred_MainRow.Convict3YearsNoX = ""
            boSchedule4ApplicantBred_MainRow.Convict3YearsYesX = ""
            Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Main.AddBOSchedule4ApplicantBred_MainRow(boSchedule4ApplicantBred_MainRow)

            ' BOSchedule4ApplicantBred_MainRow - Row 0
            boSchedule4ApplicantBred_Sub1Row = Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub1.NewBOSchedule4ApplicantBred_Sub1Row
            boSchedule4ApplicantBred_Sub1Row.ApplicationId = 1
            boSchedule4ApplicantBred_Sub1Row.Gender = "Male 1"
            boSchedule4ApplicantBred_Sub1Row.IdMarkType = "DEFRA CLOSED RING  UR19608"
            boSchedule4ApplicantBred_Sub1Row.Species = "African Orange - Bellied Parrot"
            boSchedule4ApplicantBred_Sub1Row.CommonName = "Poicephalus Rufiventris"
            boSchedule4ApplicantBred_Sub1Row.RegKeeperYes = "X"
            boSchedule4ApplicantBred_Sub1Row.RegKeeperNo = ""
            Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub1.AddBOSchedule4ApplicantBred_Sub1Row(boSchedule4ApplicantBred_Sub1Row)

            ' BOSchedule4ApplicantBred_MainRow - Row 1
            boSchedule4ApplicantBred_Sub1Row = Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub1.NewBOSchedule4ApplicantBred_Sub1Row
            boSchedule4ApplicantBred_Sub1Row.ApplicationId = 1
            boSchedule4ApplicantBred_Sub1Row.Gender = "Male 2"
            boSchedule4ApplicantBred_Sub1Row.IdMarkType = "DEFRA CLOSED RING  UR16634"
            boSchedule4ApplicantBred_Sub1Row.Species = "African Orange - Bellied Parrot"
            boSchedule4ApplicantBred_Sub1Row.CommonName = "Poicephalus Rufiventris"
            boSchedule4ApplicantBred_Sub1Row.RegKeeperYes = "X"
            boSchedule4ApplicantBred_Sub1Row.RegKeeperNo = ""
            Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub1.AddBOSchedule4ApplicantBred_Sub1Row(boSchedule4ApplicantBred_Sub1Row)

            ' BOSchedule4ApplicantBred_MainRow - Row 1
            boSchedule4ApplicantBred_Sub1Row = Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub1.NewBOSchedule4ApplicantBred_Sub1Row
            boSchedule4ApplicantBred_Sub1Row.ApplicationId = 1
            boSchedule4ApplicantBred_Sub1Row.Gender = "Female 1"
            boSchedule4ApplicantBred_Sub1Row.IdMarkType = "DEFRA CLOSED RING  UR19612"
            boSchedule4ApplicantBred_Sub1Row.Species = "African Orange - Bellied Parrot"
            boSchedule4ApplicantBred_Sub1Row.CommonName = "Poicephalus Rufiventris"
            boSchedule4ApplicantBred_Sub1Row.RegKeeperYes = "X"
            boSchedule4ApplicantBred_Sub1Row.RegKeeperNo = ""
            Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub1.AddBOSchedule4ApplicantBred_Sub1Row(boSchedule4ApplicantBred_Sub1Row)

            ' boSchedule4ApplicantBred_Sub2Row - Bird 1
            boSchedule4ApplicantBred_Sub2Row = Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub2.NewBOSchedule4ApplicantBred_Sub2Row
            boSchedule4ApplicantBred_Sub2Row.ApplicationId = 1
            boSchedule4ApplicantBred_Sub2Row.BirdNo = "Bird 1"
            boSchedule4ApplicantBred_Sub2Row.Species = "African Orange - Bellied Parrot"
            boSchedule4ApplicantBred_Sub2Row.CommonName = "Poicephalus Rufiventris"
            boSchedule4ApplicantBred_Sub2Row.HatchDay = ""
            boSchedule4ApplicantBred_Sub2Row.HatchMonth = ""
            boSchedule4ApplicantBred_Sub2Row.HatchYear = ""
            boSchedule4ApplicantBred_Sub2Row.Sex = ""
            boSchedule4ApplicantBred_Sub2Row.KeptAddress = ""
            boSchedule4ApplicantBred_Sub2Row.OtherRingNo = ""
            boSchedule4ApplicantBred_Sub2Row.OtherRingType = ""
            boSchedule4ApplicantBred_Sub2Row.ClosedRing = ""
            boSchedule4ApplicantBred_Sub2Row.Microchip = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo1 = "9652V"
            boSchedule4ApplicantBred_Sub2Row.RingNo2 = "7458W"
            boSchedule4ApplicantBred_Sub2Row.RingNo3 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo4 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo5 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo6 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo7 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo8 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo9 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo10 = ""
            Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub2.AddBOSchedule4ApplicantBred_Sub2Row(boSchedule4ApplicantBred_Sub2Row)

            Schedule4ApplicantBredDataset.AcceptChanges()


            Dim Schedule4ApplicantBredDatasets(0) As Schedule4ApplicantBredData
            Schedule4ApplicantBredDatasets(0) = Schedule4ApplicantBredDataset

            Return Schedule4ApplicantBredDatasets

        End Function

        Public Function GetSchedule4ApplicantBredCriteriaDatasets_Simple(ByVal Schedule4ApplicantBredCriteria As ReportCriteria.Schedule4ApplicantBredCriteria) As Schedule4ApplicantBredData()

            Dim Schedule4ApplicantBredDataset As New Schedule4ApplicantBredData
            Dim boSchedule4ApplicantBred_MainRow As Schedule4ApplicantBredData.BOSchedule4ApplicantBred_MainRow
            Dim boSchedule4ApplicantBred_Sub1Row As Schedule4ApplicantBredData.BOSchedule4ApplicantBred_Sub1Row
            Dim boSchedule4ApplicantBred_Sub2Row As Schedule4ApplicantBredData.BOSchedule4ApplicantBred_Sub2Row

            ' BOSchedule4ApplicantBred_MainRow - Row 0
            boSchedule4ApplicantBred_MainRow = Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Main.NewBOSchedule4ApplicantBred_MainRow()
            boSchedule4ApplicantBred_MainRow.ApplicationId = 1
            boSchedule4ApplicantBred_MainRow.InspectionSection = True
            boSchedule4ApplicantBred_MainRow.KeeperDetails = "Defra Id: 12345" & Environment.NewLine & _
            "Mr G Figoni" & Environment.NewLine & _
            "Government Buildings" & Environment.NewLine & _
            "Epsom road" & Environment.NewLine & _
            "Guildford" & Environment.NewLine & _
            "Surrey" & Environment.NewLine & _
            "GU1 2LD"
            boSchedule4ApplicantBred_MainRow.ApplicationRef = "100001"
            boSchedule4ApplicantBred_MainRow.NumOfEggs = "1"
            boSchedule4ApplicantBred_MainRow.LastEggLaid = "07/04/2002"
            boSchedule4ApplicantBred_MainRow.MaxSignatures = 1
            boSchedule4ApplicantBred_MainRow.InspectionSection = True
            boSchedule4ApplicantBred_MainRow.Convict5YearsNoX = ""
            boSchedule4ApplicantBred_MainRow.Convict5YearsYesX = ""
            boSchedule4ApplicantBred_MainRow.Convict3YearsNoX = ""
            boSchedule4ApplicantBred_MainRow.Convict3YearsYesX = ""
            Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Main.AddBOSchedule4ApplicantBred_MainRow(boSchedule4ApplicantBred_MainRow)

            ' BOSchedule4ApplicantBred_MainRow - Row 0
            boSchedule4ApplicantBred_Sub1Row = Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub1.NewBOSchedule4ApplicantBred_Sub1Row
            boSchedule4ApplicantBred_Sub1Row.ApplicationId = 1
            boSchedule4ApplicantBred_Sub1Row.Gender = "Male 1"
            boSchedule4ApplicantBred_Sub1Row.IdMarkType = "UNRINGED LICENCE UR19608"
            boSchedule4ApplicantBred_Sub1Row.Species = "LOXIA PYTYOPSITTACUS"
            boSchedule4ApplicantBred_Sub1Row.CommonName = "PARROT CROSSBILL"
            boSchedule4ApplicantBred_Sub1Row.RegKeeperYes = "X"
            boSchedule4ApplicantBred_Sub1Row.RegKeeperNo = ""
            Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub1.AddBOSchedule4ApplicantBred_Sub1Row(boSchedule4ApplicantBred_Sub1Row)

            ' BOSchedule4ApplicantBred_MainRow - Row 1
            boSchedule4ApplicantBred_Sub1Row = Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub1.NewBOSchedule4ApplicantBred_Sub1Row
            boSchedule4ApplicantBred_Sub1Row.ApplicationId = 1
            boSchedule4ApplicantBred_Sub1Row.Gender = "Female 1"
            boSchedule4ApplicantBred_Sub1Row.IdMarkType = "UNRINGED LICENCE UR19612"
            boSchedule4ApplicantBred_Sub1Row.Species = "LOXIA PYTYOPSITTACUS"
            boSchedule4ApplicantBred_Sub1Row.CommonName = "PARROT CROSSBILL"
            boSchedule4ApplicantBred_Sub1Row.RegKeeperYes = "X"
            boSchedule4ApplicantBred_Sub1Row.RegKeeperNo = ""
            Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub1.AddBOSchedule4ApplicantBred_Sub1Row(boSchedule4ApplicantBred_Sub1Row)

            ' boSchedule4ApplicantBred_Sub2Row - Bird 1
            boSchedule4ApplicantBred_Sub2Row = Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub2.NewBOSchedule4ApplicantBred_Sub2Row
            boSchedule4ApplicantBred_Sub2Row.ApplicationId = 1
            boSchedule4ApplicantBred_Sub2Row.BirdNo = "Bird 1"
            boSchedule4ApplicantBred_Sub2Row.Species = "LOXIA PYTYOPSITTACUS"
            boSchedule4ApplicantBred_Sub2Row.CommonName = "PARROT CROSSBILL"
            boSchedule4ApplicantBred_Sub2Row.HatchDay = ""
            boSchedule4ApplicantBred_Sub2Row.HatchMonth = ""
            boSchedule4ApplicantBred_Sub2Row.HatchYear = ""
            boSchedule4ApplicantBred_Sub2Row.Sex = ""
            boSchedule4ApplicantBred_Sub2Row.KeptAddress = ""
            boSchedule4ApplicantBred_Sub2Row.OtherRingNo = ""
            boSchedule4ApplicantBred_Sub2Row.OtherRingType = ""
            boSchedule4ApplicantBred_Sub2Row.ClosedRing = ""
            boSchedule4ApplicantBred_Sub2Row.Microchip = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo1 = "9652V"
            boSchedule4ApplicantBred_Sub2Row.RingNo2 = "7458W"
            boSchedule4ApplicantBred_Sub2Row.RingNo3 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo4 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo5 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo6 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo7 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo8 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo9 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo10 = ""
            Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub2.AddBOSchedule4ApplicantBred_Sub2Row(boSchedule4ApplicantBred_Sub2Row)

            Schedule4ApplicantBredDataset.AcceptChanges()

            Dim Schedule4ApplicantBredDatasets(0) As Schedule4ApplicantBredData
            Schedule4ApplicantBredDatasets(0) = Schedule4ApplicantBredDataset

            Return Schedule4ApplicantBredDatasets

        End Function

        Public Function GetSchedule4ApplicantBredCriteriaDatasets_Average(ByVal Schedule4ApplicantBredCriteria As ReportCriteria.Schedule4ApplicantBredCriteria) As Schedule4ApplicantBredData()

            Dim Schedule4ApplicantBredDataset As New Schedule4ApplicantBredData
            Dim boSchedule4ApplicantBred_MainRow As Schedule4ApplicantBredData.BOSchedule4ApplicantBred_MainRow
            Dim boSchedule4ApplicantBred_Sub1Row As Schedule4ApplicantBredData.BOSchedule4ApplicantBred_Sub1Row
            Dim boSchedule4ApplicantBred_Sub2Row As Schedule4ApplicantBredData.BOSchedule4ApplicantBred_Sub2Row

            ' BOSchedule4ApplicantBred_MainRow - Row 0
            boSchedule4ApplicantBred_MainRow = Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Main.NewBOSchedule4ApplicantBred_MainRow()
            boSchedule4ApplicantBred_MainRow.ApplicationId = 1
            boSchedule4ApplicantBred_MainRow.InspectionSection = True
            boSchedule4ApplicantBred_MainRow.KeeperDetails = "Defra Id: 12345" & Environment.NewLine & _
            "Mr G Figoni" & Environment.NewLine & _
            "Government Buildings" & Environment.NewLine & _
            "Epsom road" & Environment.NewLine & _
            "Guildford" & Environment.NewLine & _
            "Surrey" & Environment.NewLine & _
            "GU1 2LD"
            boSchedule4ApplicantBred_MainRow.ApplicationRef = "100002"
            boSchedule4ApplicantBred_MainRow.NumOfEggs = "2"
            boSchedule4ApplicantBred_MainRow.LastEggLaid = "07/04/2002"
            boSchedule4ApplicantBred_MainRow.MaxSignatures = 2
            boSchedule4ApplicantBred_MainRow.InspectionSection = True
            boSchedule4ApplicantBred_MainRow.Convict5YearsNoX = ""
            boSchedule4ApplicantBred_MainRow.Convict5YearsYesX = ""
            boSchedule4ApplicantBred_MainRow.Convict3YearsNoX = ""
            boSchedule4ApplicantBred_MainRow.Convict3YearsYesX = ""
            Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Main.AddBOSchedule4ApplicantBred_MainRow(boSchedule4ApplicantBred_MainRow)

            ' BOSchedule4ApplicantBred_MainRow - Row 0
            boSchedule4ApplicantBred_Sub1Row = Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub1.NewBOSchedule4ApplicantBred_Sub1Row
            boSchedule4ApplicantBred_Sub1Row.ApplicationId = 1
            boSchedule4ApplicantBred_Sub1Row.Gender = "Male 1"
            boSchedule4ApplicantBred_Sub1Row.IdMarkType = "UNRINGED LICENCE UR19608"
            boSchedule4ApplicantBred_Sub1Row.Species = "LOXIA PYTYOPSITTACUS"
            boSchedule4ApplicantBred_Sub1Row.CommonName = "PARROT CROSSBILL"
            boSchedule4ApplicantBred_Sub1Row.RegKeeperYes = "X"
            boSchedule4ApplicantBred_Sub1Row.RegKeeperNo = ""
            Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub1.AddBOSchedule4ApplicantBred_Sub1Row(boSchedule4ApplicantBred_Sub1Row)

            ' BOSchedule4ApplicantBred_MainRow - Row 1
            boSchedule4ApplicantBred_Sub1Row = Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub1.NewBOSchedule4ApplicantBred_Sub1Row
            boSchedule4ApplicantBred_Sub1Row.ApplicationId = 1
            boSchedule4ApplicantBred_Sub1Row.Gender = "Male 1"
            boSchedule4ApplicantBred_Sub1Row.IdMarkType = "DEFRA CLOSED RING 1663H"
            boSchedule4ApplicantBred_Sub1Row.Species = "ACCIPITER GENTILLIS"
            boSchedule4ApplicantBred_Sub1Row.CommonName = "GOSHAWK"
            boSchedule4ApplicantBred_Sub1Row.RegKeeperYes = ""
            boSchedule4ApplicantBred_Sub1Row.RegKeeperNo = "X"
            Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub1.AddBOSchedule4ApplicantBred_Sub1Row(boSchedule4ApplicantBred_Sub1Row)

            ' BOSchedule4ApplicantBred_MainRow - Row 2
            boSchedule4ApplicantBred_Sub1Row = Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub1.NewBOSchedule4ApplicantBred_Sub1Row
            boSchedule4ApplicantBred_Sub1Row.ApplicationId = 1
            boSchedule4ApplicantBred_Sub1Row.Gender = "Female 1"
            boSchedule4ApplicantBred_Sub1Row.IdMarkType = "UNRINGED LICENCE UR19612"
            boSchedule4ApplicantBred_Sub1Row.Species = "LOXIA PYTYOPSITTACUS"
            boSchedule4ApplicantBred_Sub1Row.CommonName = "PARROT CROSSBILL"
            boSchedule4ApplicantBred_Sub1Row.RegKeeperYes = "X"
            boSchedule4ApplicantBred_Sub1Row.RegKeeperNo = ""
            Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub1.AddBOSchedule4ApplicantBred_Sub1Row(boSchedule4ApplicantBred_Sub1Row)

            ' boSchedule4ApplicantBred_Sub2Row - Bird 1
            boSchedule4ApplicantBred_Sub2Row = Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub2.NewBOSchedule4ApplicantBred_Sub2Row
            boSchedule4ApplicantBred_Sub2Row.ApplicationId = 1
            boSchedule4ApplicantBred_Sub2Row.BirdNo = "Bird 1"
            boSchedule4ApplicantBred_Sub2Row.Species = "ACCIPITER GENTILLIS/ LOXIA PYTYOPSITTACUS"
            boSchedule4ApplicantBred_Sub2Row.CommonName = "GOSHAWK/PARROT CROSSBILL"
            boSchedule4ApplicantBred_Sub2Row.HatchDay = ""
            boSchedule4ApplicantBred_Sub2Row.HatchMonth = ""
            boSchedule4ApplicantBred_Sub2Row.HatchYear = ""
            boSchedule4ApplicantBred_Sub2Row.Sex = ""
            boSchedule4ApplicantBred_Sub2Row.KeptAddress = ""
            boSchedule4ApplicantBred_Sub2Row.OtherRingNo = ""
            boSchedule4ApplicantBred_Sub2Row.OtherRingType = ""
            boSchedule4ApplicantBred_Sub2Row.ClosedRing = ""
            boSchedule4ApplicantBred_Sub2Row.Microchip = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo1 = "9652V"
            boSchedule4ApplicantBred_Sub2Row.RingNo2 = "6952V"
            boSchedule4ApplicantBred_Sub2Row.RingNo3 = "1354H"
            boSchedule4ApplicantBred_Sub2Row.RingNo4 = "3468H"
            boSchedule4ApplicantBred_Sub2Row.RingNo5 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo6 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo7 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo8 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo9 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo10 = ""
            Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub2.AddBOSchedule4ApplicantBred_Sub2Row(boSchedule4ApplicantBred_Sub2Row)

            ' boSchedule4ApplicantBred_Sub2Row - Bird 2
            boSchedule4ApplicantBred_Sub2Row = Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub2.NewBOSchedule4ApplicantBred_Sub2Row
            boSchedule4ApplicantBred_Sub2Row.ApplicationId = 1
            boSchedule4ApplicantBred_Sub2Row.BirdNo = "Bird 2"
            boSchedule4ApplicantBred_Sub2Row.Species = "ACCIPITER GENTILLIS/ LOXIA PYTYOPSITTACUS"
            boSchedule4ApplicantBred_Sub2Row.CommonName = "GOSHAWK/PARROT CROSSBILL"
            boSchedule4ApplicantBred_Sub2Row.HatchDay = ""
            boSchedule4ApplicantBred_Sub2Row.HatchMonth = ""
            boSchedule4ApplicantBred_Sub2Row.HatchYear = ""
            boSchedule4ApplicantBred_Sub2Row.Sex = ""
            boSchedule4ApplicantBred_Sub2Row.KeptAddress = ""
            boSchedule4ApplicantBred_Sub2Row.OtherRingNo = ""
            boSchedule4ApplicantBred_Sub2Row.OtherRingType = ""
            boSchedule4ApplicantBred_Sub2Row.ClosedRing = ""
            boSchedule4ApplicantBred_Sub2Row.Microchip = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo1 = "1238V"
            boSchedule4ApplicantBred_Sub2Row.RingNo2 = "7702V"
            boSchedule4ApplicantBred_Sub2Row.RingNo3 = "1374H"
            boSchedule4ApplicantBred_Sub2Row.RingNo4 = "9560H"
            boSchedule4ApplicantBred_Sub2Row.RingNo5 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo6 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo7 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo8 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo9 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo10 = ""
            Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub2.AddBOSchedule4ApplicantBred_Sub2Row(boSchedule4ApplicantBred_Sub2Row)

            Schedule4ApplicantBredDataset.AcceptChanges()

            Dim Schedule4ApplicantBredDatasets(0) As Schedule4ApplicantBredData
            Schedule4ApplicantBredDatasets(0) = Schedule4ApplicantBredDataset

            Return Schedule4ApplicantBredDatasets

        End Function

        Public Function GetSchedule4ApplicantBredCriteriaDatasets_Complex(ByVal Schedule4ApplicantBredCriteria As ReportCriteria.Schedule4ApplicantBredCriteria) As Schedule4ApplicantBredData()

            Dim Schedule4ApplicantBredDataset As New Schedule4ApplicantBredData
            Dim boSchedule4ApplicantBred_MainRow As Schedule4ApplicantBredData.BOSchedule4ApplicantBred_MainRow
            Dim boSchedule4ApplicantBred_Sub1Row As Schedule4ApplicantBredData.BOSchedule4ApplicantBred_Sub1Row
            Dim boSchedule4ApplicantBred_Sub2Row As Schedule4ApplicantBredData.BOSchedule4ApplicantBred_Sub2Row

            ' BOSchedule4ApplicantBred_MainRow - Row 0
            boSchedule4ApplicantBred_MainRow = Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Main.NewBOSchedule4ApplicantBred_MainRow()
            boSchedule4ApplicantBred_MainRow.ApplicationId = 1
            boSchedule4ApplicantBred_MainRow.InspectionSection = True
            boSchedule4ApplicantBred_MainRow.KeeperDetails = "Defra Id: 12345" & Environment.NewLine & _
            "Mr G Figoni" & Environment.NewLine & _
            "Government Buildings" & Environment.NewLine & _
            "Epsom road" & Environment.NewLine & _
            "Guildford" & Environment.NewLine & _
            "Surrey" & Environment.NewLine & _
            "GU1 2LD"
            boSchedule4ApplicantBred_MainRow.ApplicationRef = "100003"
            boSchedule4ApplicantBred_MainRow.NumOfEggs = "4"
            boSchedule4ApplicantBred_MainRow.LastEggLaid = "07/04/2002"
            boSchedule4ApplicantBred_MainRow.MaxSignatures = 4
            boSchedule4ApplicantBred_MainRow.InspectionSection = True
            boSchedule4ApplicantBred_MainRow.Convict5YearsNoX = ""
            boSchedule4ApplicantBred_MainRow.Convict5YearsYesX = ""
            boSchedule4ApplicantBred_MainRow.Convict3YearsNoX = ""
            boSchedule4ApplicantBred_MainRow.Convict3YearsYesX = ""
            Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Main.AddBOSchedule4ApplicantBred_MainRow(boSchedule4ApplicantBred_MainRow)

            ' BOSchedule4ApplicantBred_MainRow - Row 0
            boSchedule4ApplicantBred_Sub1Row = Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub1.NewBOSchedule4ApplicantBred_Sub1Row
            boSchedule4ApplicantBred_Sub1Row.ApplicationId = 1
            boSchedule4ApplicantBred_Sub1Row.Gender = "Male 1"
            boSchedule4ApplicantBred_Sub1Row.IdMarkType = "UNRINGED LICENCE UR19608"
            boSchedule4ApplicantBred_Sub1Row.Species = "LOXIA PYTYOPSITTACUS"
            boSchedule4ApplicantBred_Sub1Row.CommonName = "PARROT CROSSBILL"
            boSchedule4ApplicantBred_Sub1Row.RegKeeperYes = "X"
            boSchedule4ApplicantBred_Sub1Row.RegKeeperNo = ""
            Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub1.AddBOSchedule4ApplicantBred_Sub1Row(boSchedule4ApplicantBred_Sub1Row)

            ' BOSchedule4ApplicantBred_MainRow - Row 1
            boSchedule4ApplicantBred_Sub1Row = Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub1.NewBOSchedule4ApplicantBred_Sub1Row
            boSchedule4ApplicantBred_Sub1Row.ApplicationId = 1
            boSchedule4ApplicantBred_Sub1Row.Gender = "Male 2"
            boSchedule4ApplicantBred_Sub1Row.IdMarkType = "UNRINGED LICENCE UR19612"
            boSchedule4ApplicantBred_Sub1Row.Species = "LOXIA PYTYOPSITTACUS"
            boSchedule4ApplicantBred_Sub1Row.CommonName = "PARROT CROSSBILL"
            boSchedule4ApplicantBred_Sub1Row.RegKeeperYes = "X"
            boSchedule4ApplicantBred_Sub1Row.RegKeeperNo = ""
            Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub1.AddBOSchedule4ApplicantBred_Sub1Row(boSchedule4ApplicantBred_Sub1Row)

            ' BOSchedule4ApplicantBred_MainRow - Row 2
            boSchedule4ApplicantBred_Sub1Row = Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub1.NewBOSchedule4ApplicantBred_Sub1Row
            boSchedule4ApplicantBred_Sub1Row.ApplicationId = 1
            boSchedule4ApplicantBred_Sub1Row.Gender = "Male 3"
            boSchedule4ApplicantBred_Sub1Row.IdMarkType = "DEFRA CLOSED RING 19615RS"
            boSchedule4ApplicantBred_Sub1Row.Species = "ACCIPITER GENTILLIS"
            boSchedule4ApplicantBred_Sub1Row.CommonName = "GOSHAWK"
            boSchedule4ApplicantBred_Sub1Row.RegKeeperYes = "X"
            boSchedule4ApplicantBred_Sub1Row.RegKeeperNo = ""
            Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub1.AddBOSchedule4ApplicantBred_Sub1Row(boSchedule4ApplicantBred_Sub1Row)

            ' BOSchedule4ApplicantBred_MainRow - Row 3
            boSchedule4ApplicantBred_Sub1Row = Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub1.NewBOSchedule4ApplicantBred_Sub1Row
            boSchedule4ApplicantBred_Sub1Row.ApplicationId = 1
            boSchedule4ApplicantBred_Sub1Row.Gender = "Male 4"
            boSchedule4ApplicantBred_Sub1Row.IdMarkType = "DEFRA CLOSED RING 19719R"
            boSchedule4ApplicantBred_Sub1Row.Species = "FALCO PEREGRINUS"
            boSchedule4ApplicantBred_Sub1Row.CommonName = "PEREGRINE FALCON"
            boSchedule4ApplicantBred_Sub1Row.RegKeeperYes = "X"
            boSchedule4ApplicantBred_Sub1Row.RegKeeperNo = ""
            Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub1.AddBOSchedule4ApplicantBred_Sub1Row(boSchedule4ApplicantBred_Sub1Row)


            ' BOSchedule4ApplicantBred_MainRow - Row 4
            boSchedule4ApplicantBred_Sub1Row = Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub1.NewBOSchedule4ApplicantBred_Sub1Row
            boSchedule4ApplicantBred_Sub1Row.ApplicationId = 1
            boSchedule4ApplicantBred_Sub1Row.Gender = "Female 1"
            boSchedule4ApplicantBred_Sub1Row.IdMarkType = "DEFRA CLOSED RING 1962UX"
            boSchedule4ApplicantBred_Sub1Row.Species = "FALCO PEREGRINUS"
            boSchedule4ApplicantBred_Sub1Row.CommonName = "PEREGRINE FALCON"
            boSchedule4ApplicantBred_Sub1Row.RegKeeperYes = "X"
            boSchedule4ApplicantBred_Sub1Row.RegKeeperNo = ""
            Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub1.AddBOSchedule4ApplicantBred_Sub1Row(boSchedule4ApplicantBred_Sub1Row)

            ' BOSchedule4ApplicantBred_MainRow - Row 5
            boSchedule4ApplicantBred_Sub1Row = Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub1.NewBOSchedule4ApplicantBred_Sub1Row
            boSchedule4ApplicantBred_Sub1Row.ApplicationId = 1
            boSchedule4ApplicantBred_Sub1Row.Gender = "Female 2"
            boSchedule4ApplicantBred_Sub1Row.IdMarkType = "DEFRA CLOSED RING 9622X"
            boSchedule4ApplicantBred_Sub1Row.Species = "ACCIPITER GENTILLIS"
            boSchedule4ApplicantBred_Sub1Row.CommonName = "GOSHAWK"
            boSchedule4ApplicantBred_Sub1Row.RegKeeperYes = "X"
            boSchedule4ApplicantBred_Sub1Row.RegKeeperNo = ""
            Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub1.AddBOSchedule4ApplicantBred_Sub1Row(boSchedule4ApplicantBred_Sub1Row)


            ' boSchedule4ApplicantBred_Sub2Row - Bird 1
            boSchedule4ApplicantBred_Sub2Row = Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub2.NewBOSchedule4ApplicantBred_Sub2Row
            boSchedule4ApplicantBred_Sub2Row.ApplicationId = 1
            boSchedule4ApplicantBred_Sub2Row.BirdNo = "Bird 1"
            boSchedule4ApplicantBred_Sub2Row.Species = ""
            boSchedule4ApplicantBred_Sub2Row.CommonName = ""
            boSchedule4ApplicantBred_Sub2Row.HatchDay = ""
            boSchedule4ApplicantBred_Sub2Row.HatchMonth = ""
            boSchedule4ApplicantBred_Sub2Row.HatchYear = ""
            boSchedule4ApplicantBred_Sub2Row.Sex = ""
            boSchedule4ApplicantBred_Sub2Row.KeptAddress = ""
            boSchedule4ApplicantBred_Sub2Row.OtherRingNo = ""
            boSchedule4ApplicantBred_Sub2Row.OtherRingType = ""
            boSchedule4ApplicantBred_Sub2Row.ClosedRing = ""
            boSchedule4ApplicantBred_Sub2Row.Microchip = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo1 = "2586V"
            boSchedule4ApplicantBred_Sub2Row.RingNo2 = "9287V"
            boSchedule4ApplicantBred_Sub2Row.RingNo3 = "5799H"
            boSchedule4ApplicantBred_Sub2Row.RingNo4 = "7805H"
            boSchedule4ApplicantBred_Sub2Row.RingNo5 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo6 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo7 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo8 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo9 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo10 = ""
            Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub2.AddBOSchedule4ApplicantBred_Sub2Row(boSchedule4ApplicantBred_Sub2Row)

            ' boSchedule4ApplicantBred_Sub2Row - Bird 2
            boSchedule4ApplicantBred_Sub2Row = Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub2.NewBOSchedule4ApplicantBred_Sub2Row
            boSchedule4ApplicantBred_Sub2Row.ApplicationId = 1
            boSchedule4ApplicantBred_Sub2Row.BirdNo = "Bird 2"
            boSchedule4ApplicantBred_Sub2Row.Species = ""
            boSchedule4ApplicantBred_Sub2Row.CommonName = ""
            boSchedule4ApplicantBred_Sub2Row.HatchDay = ""
            boSchedule4ApplicantBred_Sub2Row.HatchMonth = ""
            boSchedule4ApplicantBred_Sub2Row.HatchYear = ""
            boSchedule4ApplicantBred_Sub2Row.Sex = ""
            boSchedule4ApplicantBred_Sub2Row.KeptAddress = ""
            boSchedule4ApplicantBred_Sub2Row.OtherRingNo = ""
            boSchedule4ApplicantBred_Sub2Row.OtherRingType = ""
            boSchedule4ApplicantBred_Sub2Row.ClosedRing = ""
            boSchedule4ApplicantBred_Sub2Row.Microchip = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo1 = "9653V"
            boSchedule4ApplicantBred_Sub2Row.RingNo2 = "6954V"
            boSchedule4ApplicantBred_Sub2Row.RingNo3 = "1355H"
            boSchedule4ApplicantBred_Sub2Row.RingNo4 = "3469H"
            boSchedule4ApplicantBred_Sub2Row.RingNo5 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo6 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo7 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo8 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo9 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo10 = ""
            Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub2.AddBOSchedule4ApplicantBred_Sub2Row(boSchedule4ApplicantBred_Sub2Row)

            ' boSchedule4ApplicantBred_Sub2Row - Bird 3
            boSchedule4ApplicantBred_Sub2Row = Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub2.NewBOSchedule4ApplicantBred_Sub2Row
            boSchedule4ApplicantBred_Sub2Row.ApplicationId = 1
            boSchedule4ApplicantBred_Sub2Row.BirdNo = "Bird 3"
            boSchedule4ApplicantBred_Sub2Row.Species = ""
            boSchedule4ApplicantBred_Sub2Row.CommonName = ""
            boSchedule4ApplicantBred_Sub2Row.HatchDay = ""
            boSchedule4ApplicantBred_Sub2Row.HatchMonth = ""
            boSchedule4ApplicantBred_Sub2Row.HatchYear = ""
            boSchedule4ApplicantBred_Sub2Row.Sex = ""
            boSchedule4ApplicantBred_Sub2Row.KeptAddress = ""
            boSchedule4ApplicantBred_Sub2Row.OtherRingNo = ""
            boSchedule4ApplicantBred_Sub2Row.OtherRingType = ""
            boSchedule4ApplicantBred_Sub2Row.ClosedRing = ""
            boSchedule4ApplicantBred_Sub2Row.Microchip = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo1 = "0652V"
            boSchedule4ApplicantBred_Sub2Row.RingNo2 = "7952V"
            boSchedule4ApplicantBred_Sub2Row.RingNo3 = "2352H"
            boSchedule4ApplicantBred_Sub2Row.RingNo4 = "4468H"
            boSchedule4ApplicantBred_Sub2Row.RingNo5 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo6 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo7 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo8 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo9 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo10 = ""
            Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub2.AddBOSchedule4ApplicantBred_Sub2Row(boSchedule4ApplicantBred_Sub2Row)

            ' boSchedule4ApplicantBred_Sub2Row - Bird 4
            boSchedule4ApplicantBred_Sub2Row = Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub2.NewBOSchedule4ApplicantBred_Sub2Row
            boSchedule4ApplicantBred_Sub2Row.ApplicationId = 1
            boSchedule4ApplicantBred_Sub2Row.BirdNo = "Bird 4"
            boSchedule4ApplicantBred_Sub2Row.Species = ""
            boSchedule4ApplicantBred_Sub2Row.CommonName = ""
            boSchedule4ApplicantBred_Sub2Row.HatchDay = ""
            boSchedule4ApplicantBred_Sub2Row.HatchMonth = ""
            boSchedule4ApplicantBred_Sub2Row.HatchYear = ""
            boSchedule4ApplicantBred_Sub2Row.Sex = ""
            boSchedule4ApplicantBred_Sub2Row.KeptAddress = ""
            boSchedule4ApplicantBred_Sub2Row.OtherRingNo = ""
            boSchedule4ApplicantBred_Sub2Row.OtherRingType = ""
            boSchedule4ApplicantBred_Sub2Row.ClosedRing = ""
            boSchedule4ApplicantBred_Sub2Row.Microchip = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo1 = "2238V"
            boSchedule4ApplicantBred_Sub2Row.RingNo2 = "8702V"
            boSchedule4ApplicantBred_Sub2Row.RingNo3 = "2374H"
            boSchedule4ApplicantBred_Sub2Row.RingNo4 = "1560H"
            boSchedule4ApplicantBred_Sub2Row.RingNo5 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo6 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo7 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo8 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo9 = ""
            boSchedule4ApplicantBred_Sub2Row.RingNo10 = ""
            Schedule4ApplicantBredDataset.BOSchedule4ApplicantBred_Sub2.AddBOSchedule4ApplicantBred_Sub2Row(boSchedule4ApplicantBred_Sub2Row)

            Schedule4ApplicantBredDataset.AcceptChanges()


            Dim Schedule4ApplicantBredDatasets(0) As Schedule4ApplicantBredData
            Schedule4ApplicantBredDatasets(0) = Schedule4ApplicantBredDataset

            Return Schedule4ApplicantBredDatasets

        End Function


    End Class

End Namespace
Imports System
Imports System.data
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Namespace RPT

    Public Class Schedule4Found
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim schedule4FoundCriteria As ReportCriteria.Schedule4FoundCriteria = CType(reportCriteria, ReportCriteria.Schedule4FoundCriteria)
            Dim schedule4FoundDataset As New Schedule4FoundData

            Dim reportData As New Application.Bird.Reports.ReportData
            Dim reportDataResults As ReportDataResults = reportData.GetSchedule4FoundReportData(schedule4FoundCriteria.ApplicationId, schedule4FoundDataset.GetXmlSchema)
            Dim ReportDataset As DataSet = reportDataResults.ReportData

            Dim Schedule4Found_Main_RPT As New Schedule4Found_Main_RPT
            Dim reportResults(0) As BOReportResults
            reportResults(0) = DoReport(reportCriteria.Description, reportDataResults.SearchReference, schedule4FoundCriteria.ApplicationId, reportDataResults.ReportData, schedule4FoundCriteria, saveReport, _
                Schedule4Found_Main_RPT, reportPrintJobId, printSequence)

            Return reportResults

        End Function


        Public Function GetSchedule4FoundCriteriaDatasets_Simple(ByVal Schedule4FoundCriteria As ReportCriteria.Schedule4FoundCriteria) As Schedule4FoundData()

            Dim Schedule4FoundDataset As New Schedule4FoundData
            Dim boSchedule4Found_MainRow As Schedule4FoundData.BOSchedule4Found_MainRow
            Dim boSchedule4Found_Sub1Row As Schedule4FoundData.BOSchedule4Found_Sub1Row

            ' BOSchedule4Found_MainRow - Row 0
            boSchedule4Found_MainRow = Schedule4FoundDataset.BOSchedule4Found_Main.NewBOSchedule4Found_MainRow()
            boSchedule4Found_MainRow.ApplicationId = 1
            boSchedule4Found_MainRow.InspectionSection = True
            boSchedule4Found_MainRow.KeeperDetails = "Defra Id: 12345" & Environment.NewLine & _
            "Mr G Figoni" & Environment.NewLine & _
            "Government Buildings" & Environment.NewLine & _
            "Epsom road" & Environment.NewLine & _
            "Guildford" & Environment.NewLine & _
            "Surrey" & Environment.NewLine & _
            "GU1 2LD"
            boSchedule4Found_MainRow.ApplicationRef = "100009"
            boSchedule4Found_MainRow.MaxSignatures = 1
            boSchedule4Found_MainRow.InspectionSection = True
            boSchedule4Found_MainRow.Convict5YearsNoX = ""
            boSchedule4Found_MainRow.Convict5YearsYesX = ""
            boSchedule4Found_MainRow.Convict3YearsNoX = ""
            boSchedule4Found_MainRow.Convict3YearsYesX = ""
            Schedule4FoundDataset.BOSchedule4Found_Main.AddBOSchedule4Found_MainRow(boSchedule4Found_MainRow)

            ' boSchedule4Found_Sub1Row - Bird 1
            boSchedule4Found_Sub1Row = Schedule4FoundDataset.BOSchedule4Found_Sub1.NewBOSchedule4Found_Sub1Row
            boSchedule4Found_Sub1Row.ApplicationId = 1
            boSchedule4Found_Sub1Row.BirdNo = "Bird 1"
            boSchedule4Found_Sub1Row.Species = "FALCO PEREGRINUS"
            boSchedule4Found_Sub1Row.CommonName = "PEREGRINE FALCON"

            boSchedule4Found_Sub1Row.Age = "MATURE"
            boSchedule4Found_Sub1Row.Sex = "MALE"
            boSchedule4Found_Sub1Row.KeptAddress = ""
            boSchedule4Found_Sub1Row.DayIntoCare = "20"
            boSchedule4Found_Sub1Row.MonthIntoCare = "04"
            boSchedule4Found_Sub1Row.YearIntoCare = "2004"
            boSchedule4Found_Sub1Row.WildDisabledYes = "X"
            boSchedule4Found_Sub1Row.WildDisabledNo = ""
            boSchedule4Found_Sub1Row.CloseRingNo = "NO"
            boSchedule4Found_Sub1Row.Possession = "FOUND IN GUILDFORD PARK"
            boSchedule4Found_Sub1Row.Injuries = "BROKEN WING"
            boSchedule4Found_Sub1Row.RspcaYes = "X"
            boSchedule4Found_Sub1Row.RspcaNo = ""
            boSchedule4Found_Sub1Row.VetYes = ""
            boSchedule4Found_Sub1Row.VetNo = "X"
            boSchedule4Found_Sub1Row.NotifiedYes = ""
            boSchedule4Found_Sub1Row.NotifiedNo = "X"
            boSchedule4Found_Sub1Row.OtherRingNo = ""
            boSchedule4Found_Sub1Row.OtherRingType = ""
            boSchedule4Found_Sub1Row.Microchip = ""
            boSchedule4Found_Sub1Row.DayAcquired = "20"
            boSchedule4Found_Sub1Row.MonthAcquired = "04"
            boSchedule4Found_Sub1Row.YearAcquired = "2004"

            boSchedule4Found_Sub1Row.RingNo1 = "36521Z"
            boSchedule4Found_Sub1Row.RingNo2 = "87354Z"
            boSchedule4Found_Sub1Row.RingNo3 = ""
            boSchedule4Found_Sub1Row.RingNo4 = ""
            boSchedule4Found_Sub1Row.RingNo5 = ""
            boSchedule4Found_Sub1Row.RingNo6 = ""
            boSchedule4Found_Sub1Row.RingNo7 = ""
            boSchedule4Found_Sub1Row.RingNo8 = ""
            boSchedule4Found_Sub1Row.RingNo9 = ""
            boSchedule4Found_Sub1Row.RingNo10 = ""
            Schedule4FoundDataset.BOSchedule4Found_Sub1.AddBOSchedule4Found_Sub1Row(boSchedule4Found_Sub1Row)

            Schedule4FoundDataset.AcceptChanges()


            Dim Schedule4FoundDatasets(0) As Schedule4FoundData
            Schedule4FoundDatasets(0) = Schedule4FoundDataset

            Return Schedule4FoundDatasets

        End Function

        Public Function GetSchedule4FoundCriteriaDatasets_Average(ByVal Schedule4FoundCriteria As ReportCriteria.Schedule4FoundCriteria) As Schedule4FoundData()

            Dim Schedule4FoundDataset As New Schedule4FoundData
            Dim boSchedule4Found_MainRow As Schedule4FoundData.BOSchedule4Found_MainRow
            Dim boSchedule4Found_Sub1Row As Schedule4FoundData.BOSchedule4Found_Sub1Row

            ' BOSchedule4Found_MainRow - Row 0
            boSchedule4Found_MainRow = Schedule4FoundDataset.BOSchedule4Found_Main.NewBOSchedule4Found_MainRow()
            boSchedule4Found_MainRow.ApplicationId = 1
            boSchedule4Found_MainRow.InspectionSection = True
            boSchedule4Found_MainRow.KeeperDetails = "Defra Id: 12345" & Environment.NewLine & _
            "Mr G Figoni" & Environment.NewLine & _
            "Government Buildings" & Environment.NewLine & _
            "Epsom road" & Environment.NewLine & _
            "Guildford" & Environment.NewLine & _
            "Surrey" & Environment.NewLine & _
            "GU1 2LD"
            boSchedule4Found_MainRow.ApplicationRef = "100010"
            boSchedule4Found_MainRow.MaxSignatures = 2
            boSchedule4Found_MainRow.InspectionSection = True
            boSchedule4Found_MainRow.Convict5YearsNoX = ""
            boSchedule4Found_MainRow.Convict5YearsYesX = ""
            boSchedule4Found_MainRow.Convict3YearsNoX = ""
            boSchedule4Found_MainRow.Convict3YearsYesX = ""
            Schedule4FoundDataset.BOSchedule4Found_Main.AddBOSchedule4Found_MainRow(boSchedule4Found_MainRow)

            ' boSchedule4Found_Sub1Row - Bird 1
            boSchedule4Found_Sub1Row = Schedule4FoundDataset.BOSchedule4Found_Sub1.NewBOSchedule4Found_Sub1Row
            boSchedule4Found_Sub1Row.ApplicationId = 1
            boSchedule4Found_Sub1Row.BirdNo = "Bird 1"
            boSchedule4Found_Sub1Row.Species = "FALCO PEREGRINUS"
            boSchedule4Found_Sub1Row.CommonName = "PEREGRINE FALCON"
            boSchedule4Found_Sub1Row.Age = "MATURE"
            boSchedule4Found_Sub1Row.Sex = "FEMALE"
            boSchedule4Found_Sub1Row.KeptAddress = ""
            boSchedule4Found_Sub1Row.DayIntoCare = "20"
            boSchedule4Found_Sub1Row.MonthIntoCare = "04"
            boSchedule4Found_Sub1Row.YearIntoCare = "2004"
            boSchedule4Found_Sub1Row.WildDisabledYes = "X"
            boSchedule4Found_Sub1Row.WildDisabledNo = ""
            boSchedule4Found_Sub1Row.CloseRingNo = "NO"
            boSchedule4Found_Sub1Row.Possession = "FOUND IN GUILDFORD PARK"
            boSchedule4Found_Sub1Row.Injuries = "BROKEN WING"
            boSchedule4Found_Sub1Row.RspcaYes = "X"
            boSchedule4Found_Sub1Row.RspcaNo = ""
            boSchedule4Found_Sub1Row.VetYes = ""
            boSchedule4Found_Sub1Row.VetNo = "X"
            boSchedule4Found_Sub1Row.NotifiedYes = ""
            boSchedule4Found_Sub1Row.NotifiedNo = "X"
            boSchedule4Found_Sub1Row.OtherRingNo = ""
            boSchedule4Found_Sub1Row.OtherRingType = ""
            boSchedule4Found_Sub1Row.Microchip = ""
            boSchedule4Found_Sub1Row.DayAcquired = "20"
            boSchedule4Found_Sub1Row.MonthAcquired = "04"
            boSchedule4Found_Sub1Row.YearAcquired = "2004"
            boSchedule4Found_Sub1Row.RingNo1 = "81076Z"
            boSchedule4Found_Sub1Row.RingNo2 = "43910Z"
            boSchedule4Found_Sub1Row.RingNo3 = ""
            boSchedule4Found_Sub1Row.RingNo4 = ""
            boSchedule4Found_Sub1Row.RingNo5 = ""
            boSchedule4Found_Sub1Row.RingNo6 = ""
            boSchedule4Found_Sub1Row.RingNo7 = ""
            boSchedule4Found_Sub1Row.RingNo8 = ""
            boSchedule4Found_Sub1Row.RingNo9 = ""
            boSchedule4Found_Sub1Row.RingNo10 = ""
            Schedule4FoundDataset.BOSchedule4Found_Sub1.AddBOSchedule4Found_Sub1Row(boSchedule4Found_Sub1Row)

            ' boSchedule4Found_Sub1Row - Bird 2
            boSchedule4Found_Sub1Row = Schedule4FoundDataset.BOSchedule4Found_Sub1.NewBOSchedule4Found_Sub1Row
            boSchedule4Found_Sub1Row.ApplicationId = 1
            boSchedule4Found_Sub1Row.BirdNo = "Bird 2"
            boSchedule4Found_Sub1Row.Species = "FALCO PEREGRINUS"
            boSchedule4Found_Sub1Row.CommonName = "PEREGRINE FALCON"
            boSchedule4Found_Sub1Row.Age = "MATURE"
            boSchedule4Found_Sub1Row.Sex = "MALE"
            boSchedule4Found_Sub1Row.KeptAddress = ""
            boSchedule4Found_Sub1Row.DayIntoCare = "20"
            boSchedule4Found_Sub1Row.MonthIntoCare = "04"
            boSchedule4Found_Sub1Row.YearIntoCare = "2004"
            boSchedule4Found_Sub1Row.WildDisabledYes = "X"
            boSchedule4Found_Sub1Row.WildDisabledNo = ""
            boSchedule4Found_Sub1Row.CloseRingNo = "NO"
            boSchedule4Found_Sub1Row.Possession = "FOUND IN GUILDFORD PARK"
            boSchedule4Found_Sub1Row.Injuries = "BROKEN WING"
            boSchedule4Found_Sub1Row.RspcaYes = "X"
            boSchedule4Found_Sub1Row.RspcaNo = ""
            boSchedule4Found_Sub1Row.VetYes = ""
            boSchedule4Found_Sub1Row.VetNo = "X"
            boSchedule4Found_Sub1Row.NotifiedYes = ""
            boSchedule4Found_Sub1Row.NotifiedNo = "X"
            boSchedule4Found_Sub1Row.OtherRingNo = ""
            boSchedule4Found_Sub1Row.OtherRingType = ""
            boSchedule4Found_Sub1Row.Microchip = ""
            boSchedule4Found_Sub1Row.DayAcquired = "20"
            boSchedule4Found_Sub1Row.MonthAcquired = "04"
            boSchedule4Found_Sub1Row.YearAcquired = "2004"
            boSchedule4Found_Sub1Row.RingNo1 = "58743Z"
            boSchedule4Found_Sub1Row.RingNo2 = "21798Z"
            boSchedule4Found_Sub1Row.RingNo3 = ""
            boSchedule4Found_Sub1Row.RingNo4 = ""
            boSchedule4Found_Sub1Row.RingNo5 = ""
            boSchedule4Found_Sub1Row.RingNo6 = ""
            boSchedule4Found_Sub1Row.RingNo7 = ""
            boSchedule4Found_Sub1Row.RingNo8 = ""
            boSchedule4Found_Sub1Row.RingNo9 = ""
            boSchedule4Found_Sub1Row.RingNo10 = ""
            Schedule4FoundDataset.BOSchedule4Found_Sub1.AddBOSchedule4Found_Sub1Row(boSchedule4Found_Sub1Row)

            Schedule4FoundDataset.AcceptChanges()


            Dim Schedule4FoundDatasets(0) As Schedule4FoundData
            Schedule4FoundDatasets(0) = Schedule4FoundDataset

            Return Schedule4FoundDatasets

        End Function

    End Class

End Namespace
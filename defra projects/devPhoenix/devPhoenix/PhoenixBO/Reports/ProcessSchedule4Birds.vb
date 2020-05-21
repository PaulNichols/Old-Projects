Imports System
Imports System.data
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Namespace RPT

    Public Class Schedule4Birds
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim Schedule4BirdsCriteria As ReportCriteria.Schedule4BirdsCriteria = CType(reportCriteria, ReportCriteria.Schedule4BirdsCriteria)
            Dim Schedule4BirdsDataset As New Schedule4BirdsData

            Dim reportData As New Application.Bird.Reports.ReportData
            Dim reportDataResults As ReportDataResults = reportData.GetSchedule4BirdsReportData(Schedule4BirdsCriteria.ApplicationId, Schedule4BirdsDataset.GetXmlSchema)
            Dim ReportDataset As DataSet = reportDataResults.ReportData

            Dim Schedule4Birds_Main_RPT As New Schedule4Birds_Main_RPT
            Dim reportResults(0) As BOReportResults
            reportResults(0) = DoReport(reportCriteria.Description, reportDataResults.SearchReference, Schedule4BirdsCriteria.ApplicationId, reportDataResults.ReportData, Schedule4BirdsCriteria, saveReport, _
                Schedule4Birds_Main_RPT, reportPrintJobId, printSequence)

            Return reportResults

        End Function


        Public Function GetSchedule4BirdsCriteriaDatasets_Simple(ByVal Schedule4BirdsCriteria As ReportCriteria.Schedule4BirdsCriteria) As Schedule4BirdsData()

            Dim Schedule4BirdsDataset As New Schedule4BirdsData
            Dim boSchedule4Birds_MainRow As Schedule4BirdsData.BOSchedule4Birds_MainRow
            Dim boSchedule4Birds_Sub1Row As Schedule4BirdsData.BOSchedule4Birds_Sub1Row

            ' BOSchedule4Birds_MainRow - Row 0
            boSchedule4Birds_MainRow = Schedule4BirdsDataset.BOSchedule4Birds_Main.NewBOSchedule4Birds_MainRow()
            boSchedule4Birds_MainRow.ApplicationId = 1
            boSchedule4Birds_MainRow.InspectionSection = True
            boSchedule4Birds_MainRow.KeeperDetails = "Defra Id: 12345" & Environment.NewLine & _
            "Mr G Figoni" & Environment.NewLine & _
            "Government Buildings" & Environment.NewLine & _
            "Epsom road" & Environment.NewLine & _
            "Guildford" & Environment.NewLine & _
            "Surrey" & Environment.NewLine & _
            "GU1 2LD"
            boSchedule4Birds_MainRow.ApplicationRef = "100011"
            boSchedule4Birds_MainRow.MaxSignatures = 1
            boSchedule4Birds_MainRow.InspectionSection = True
            boSchedule4Birds_MainRow.Convict5YearsYesX = ""
            boSchedule4Birds_MainRow.Convict5YearsNoX = ""
            boSchedule4Birds_MainRow.Convict3YearsYesX = ""
            boSchedule4Birds_MainRow.Convict3YearsNoX = ""
            Schedule4BirdsDataset.BOSchedule4Birds_Main.AddBOSchedule4Birds_MainRow(boSchedule4Birds_MainRow)

            ' boSchedule4Birds_Sub1Row - Bird 1
            boSchedule4Birds_Sub1Row = Schedule4BirdsDataset.BOSchedule4Birds_Sub1.NewBOSchedule4Birds_Sub1Row
            boSchedule4Birds_Sub1Row.ApplicationId = 1
            boSchedule4Birds_Sub1Row.BirdNo = "Bird 1"
            boSchedule4Birds_Sub1Row.Species = "ACCIPITER BICOLOR"
            boSchedule4Birds_Sub1Row.CommonName = "BICOLORED SPARROW HAWK"
            boSchedule4Birds_Sub1Row.HatchDay = "29"
            boSchedule4Birds_Sub1Row.HatchMonth = "06"
            boSchedule4Birds_Sub1Row.HatchYear = "2003"
            boSchedule4Birds_Sub1Row.Sex = "MALE"
            boSchedule4Birds_Sub1Row.KeptAddress = ""
            boSchedule4Birds_Sub1Row.SaleX = "X"
            boSchedule4Birds_Sub1Row.HireX = ""
            boSchedule4Birds_Sub1Row.ExchangeX = ""
            boSchedule4Birds_Sub1Row.BarterX = ""
            boSchedule4Birds_Sub1Row.LoanX = ""
            boSchedule4Birds_Sub1Row.GiftX = ""
            boSchedule4Birds_Sub1Row.ReturnX = ""
            boSchedule4Birds_Sub1Row.OtherRingNo = "65638"
            boSchedule4Birds_Sub1Row.OtherRingType = "SPLIT RING"
            boSchedule4Birds_Sub1Row.ClosedRing = ""
            boSchedule4Birds_Sub1Row.DayAcquired = ""
            boSchedule4Birds_Sub1Row.MonthAcquired = ""
            boSchedule4Birds_Sub1Row.YearAcquired = ""
            boSchedule4Birds_Sub1Row.Article10CertNo = ""
            boSchedule4Birds_Sub1Row.Microchip = ""
            boSchedule4Birds_Sub1Row.PreviousKeeperAddress = ""
            boSchedule4Birds_Sub1Row.Possession = "BOUGHT IT OFF A MAN IN THE PUB"
            boSchedule4Birds_Sub1Row.RingNo1 = "1D25587"
            boSchedule4Birds_Sub1Row.RingNo2 = "1F99520"
            boSchedule4Birds_Sub1Row.RingNo3 = ""
            boSchedule4Birds_Sub1Row.RingNo4 = ""
            boSchedule4Birds_Sub1Row.RingNo5 = ""
            boSchedule4Birds_Sub1Row.RingNo6 = ""
            boSchedule4Birds_Sub1Row.RingNo7 = ""
            boSchedule4Birds_Sub1Row.RingNo8 = ""
            boSchedule4Birds_Sub1Row.RingNo9 = ""
            boSchedule4Birds_Sub1Row.RingNo10 = ""
            Schedule4BirdsDataset.BOSchedule4Birds_Sub1.AddBOSchedule4Birds_Sub1Row(boSchedule4Birds_Sub1Row)


            Dim Schedule4BirdsDatasets(0) As Schedule4BirdsData
            Schedule4BirdsDatasets(0) = Schedule4BirdsDataset

            Return Schedule4BirdsDatasets

        End Function

        Public Function GetSchedule4BirdsCriteriaDatasets_Average(ByVal Schedule4BirdsCriteria As ReportCriteria.Schedule4BirdsCriteria) As Schedule4BirdsData()

            Dim Schedule4BirdsDataset As New Schedule4BirdsData
            Dim boSchedule4Birds_MainRow As Schedule4BirdsData.BOSchedule4Birds_MainRow
            Dim boSchedule4Birds_Sub1Row As Schedule4BirdsData.BOSchedule4Birds_Sub1Row

            ' BOSchedule4Birds_MainRow - Row 0
            boSchedule4Birds_MainRow = Schedule4BirdsDataset.BOSchedule4Birds_Main.NewBOSchedule4Birds_MainRow()
            boSchedule4Birds_MainRow.ApplicationId = 1
            boSchedule4Birds_MainRow.InspectionSection = True
            boSchedule4Birds_MainRow.KeeperDetails = "Defra Id: 12345" & Environment.NewLine & _
            "Mr G Figoni" & Environment.NewLine & _
            "Government Buildings" & Environment.NewLine & _
            "Epsom road" & Environment.NewLine & _
            "Guildford" & Environment.NewLine & _
            "Surrey" & Environment.NewLine & _
            "GU1 2LD"
            boSchedule4Birds_MainRow.ApplicationRef = "100012"
            boSchedule4Birds_MainRow.MaxSignatures = 2
            boSchedule4Birds_MainRow.InspectionSection = True
            boSchedule4Birds_MainRow.Convict5YearsYesX = ""
            boSchedule4Birds_MainRow.Convict5YearsNoX = ""
            boSchedule4Birds_MainRow.Convict3YearsYesX = ""
            boSchedule4Birds_MainRow.Convict3YearsNoX = ""
            Schedule4BirdsDataset.BOSchedule4Birds_Main.AddBOSchedule4Birds_MainRow(boSchedule4Birds_MainRow)

            ' boSchedule4Birds_Sub1Row - Bird 1
            boSchedule4Birds_Sub1Row = Schedule4BirdsDataset.BOSchedule4Birds_Sub1.NewBOSchedule4Birds_Sub1Row
            boSchedule4Birds_Sub1Row.ApplicationId = 1
            boSchedule4Birds_Sub1Row.BirdNo = "Bird 1"
            boSchedule4Birds_Sub1Row.Species = "ACCIPITER BICOLOR"
            boSchedule4Birds_Sub1Row.CommonName = "BICOLORED SPARROW HAWK"
            boSchedule4Birds_Sub1Row.HatchDay = "29"
            boSchedule4Birds_Sub1Row.HatchMonth = "06"
            boSchedule4Birds_Sub1Row.HatchYear = "2003"
            boSchedule4Birds_Sub1Row.Sex = "MALE"
            boSchedule4Birds_Sub1Row.KeptAddress = ""
            boSchedule4Birds_Sub1Row.SaleX = "X"
            boSchedule4Birds_Sub1Row.HireX = ""
            boSchedule4Birds_Sub1Row.ExchangeX = ""
            boSchedule4Birds_Sub1Row.BarterX = ""
            boSchedule4Birds_Sub1Row.LoanX = ""
            boSchedule4Birds_Sub1Row.GiftX = ""
            boSchedule4Birds_Sub1Row.ReturnX = ""
            boSchedule4Birds_Sub1Row.OtherRingNo = "65638"
            boSchedule4Birds_Sub1Row.OtherRingType = "SPLIT RING"
            boSchedule4Birds_Sub1Row.ClosedRing = ""
            boSchedule4Birds_Sub1Row.DayAcquired = ""
            boSchedule4Birds_Sub1Row.MonthAcquired = ""
            boSchedule4Birds_Sub1Row.YearAcquired = ""
            boSchedule4Birds_Sub1Row.Article10CertNo = ""
            boSchedule4Birds_Sub1Row.Microchip = ""
            boSchedule4Birds_Sub1Row.PreviousKeeperAddress = ""
            boSchedule4Birds_Sub1Row.Possession = "BOUGHT IT OFF A MAN IN THE PUB"
            boSchedule4Birds_Sub1Row.RingNo1 = "1D92254"
            boSchedule4Birds_Sub1Row.RingNo2 = "1F77308"
            boSchedule4Birds_Sub1Row.RingNo3 = ""
            boSchedule4Birds_Sub1Row.RingNo4 = ""
            boSchedule4Birds_Sub1Row.RingNo5 = ""
            boSchedule4Birds_Sub1Row.RingNo6 = ""
            boSchedule4Birds_Sub1Row.RingNo7 = ""
            boSchedule4Birds_Sub1Row.RingNo8 = ""
            boSchedule4Birds_Sub1Row.RingNo9 = ""
            boSchedule4Birds_Sub1Row.RingNo10 = ""
            Schedule4BirdsDataset.BOSchedule4Birds_Sub1.AddBOSchedule4Birds_Sub1Row(boSchedule4Birds_Sub1Row)

            ' boSchedule4Birds_Sub1Row - Bird 2
            boSchedule4Birds_Sub1Row = Schedule4BirdsDataset.BOSchedule4Birds_Sub1.NewBOSchedule4Birds_Sub1Row
            boSchedule4Birds_Sub1Row.ApplicationId = 1
            boSchedule4Birds_Sub1Row.BirdNo = "Bird 2"
            boSchedule4Birds_Sub1Row.Species = "ACCIPITER BICOLOR"
            boSchedule4Birds_Sub1Row.CommonName = "BICOLORED SPARROW HAWK"
            boSchedule4Birds_Sub1Row.HatchDay = "29"
            boSchedule4Birds_Sub1Row.HatchMonth = "05"
            boSchedule4Birds_Sub1Row.HatchYear = "2000"
            boSchedule4Birds_Sub1Row.Sex = "FEMALE"
            boSchedule4Birds_Sub1Row.KeptAddress = ""
            boSchedule4Birds_Sub1Row.SaleX = "X"
            boSchedule4Birds_Sub1Row.HireX = ""
            boSchedule4Birds_Sub1Row.ExchangeX = ""
            boSchedule4Birds_Sub1Row.BarterX = ""
            boSchedule4Birds_Sub1Row.LoanX = ""
            boSchedule4Birds_Sub1Row.GiftX = ""
            boSchedule4Birds_Sub1Row.ReturnX = ""
            boSchedule4Birds_Sub1Row.OtherRingNo = "76749"
            boSchedule4Birds_Sub1Row.OtherRingType = "SPLIT RING"
            boSchedule4Birds_Sub1Row.ClosedRing = ""
            boSchedule4Birds_Sub1Row.DayAcquired = ""
            boSchedule4Birds_Sub1Row.MonthAcquired = ""
            boSchedule4Birds_Sub1Row.YearAcquired = ""
            boSchedule4Birds_Sub1Row.Article10CertNo = ""
            boSchedule4Birds_Sub1Row.Microchip = ""
            boSchedule4Birds_Sub1Row.PreviousKeeperAddress = ""
            boSchedule4Birds_Sub1Row.Possession = "BOUGHT IT OFF A MAN IN THE PUB"
            boSchedule4Birds_Sub1Row.RingNo1 = "1D244678"
            boSchedule4Birds_Sub1Row.RingNo2 = "1F88308"
            boSchedule4Birds_Sub1Row.RingNo3 = ""
            boSchedule4Birds_Sub1Row.RingNo4 = ""
            boSchedule4Birds_Sub1Row.RingNo5 = ""
            boSchedule4Birds_Sub1Row.RingNo6 = ""
            boSchedule4Birds_Sub1Row.RingNo7 = ""
            boSchedule4Birds_Sub1Row.RingNo8 = ""
            boSchedule4Birds_Sub1Row.RingNo9 = ""
            boSchedule4Birds_Sub1Row.RingNo10 = ""
            Schedule4BirdsDataset.BOSchedule4Birds_Sub1.AddBOSchedule4Birds_Sub1Row(boSchedule4Birds_Sub1Row)


            Dim Schedule4BirdsDatasets(0) As Schedule4BirdsData
            Schedule4BirdsDatasets(0) = Schedule4BirdsDataset

            Return Schedule4BirdsDatasets

        End Function

    End Class

End Namespace
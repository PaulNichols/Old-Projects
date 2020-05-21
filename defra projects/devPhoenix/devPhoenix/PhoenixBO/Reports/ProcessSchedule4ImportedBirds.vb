Imports System
Imports System.data
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Namespace RPT

    Public Class Schedule4ImportedBirds
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim schedule4ImportedBirdsCriteria As ReportCriteria.Schedule4ImportedBirdsCriteria = CType(reportCriteria, ReportCriteria.Schedule4ImportedBirdsCriteria)
            Dim schedule4ImportedBirdsDataset As New Schedule4ImportedBirdsData

            Dim reportData As New Application.Bird.Reports.ReportData
            'Dim reportDataResults As New ReportDataResults(GetSchedule4ImportedBirdsCriteriaDatasets_Average(schedule4ImportedBirdsCriteria)(0), "")
            Dim reportDataResults As ReportDataResults = reportData.GetSchedule4ImportedBirdsReportData(schedule4ImportedBirdsCriteria.ApplicationId, schedule4ImportedBirdsDataset.GetXmlSchema)

            Dim Schedule4ImportedBirds_Main_RPT As New Schedule4ImportedBirds_Main_RPT
            Dim reportResults(0) As BOReportResults
            reportResults(0) = DoReport(reportCriteria.Description, reportDataResults.SearchReference, schedule4ImportedBirdsCriteria.ApplicationId, reportDataResults.ReportData, schedule4ImportedBirdsCriteria, saveReport, _
                Schedule4ImportedBirds_Main_RPT, reportPrintJobId, printSequence)

            Return reportResults

        End Function


        Public Function GetSchedule4ImportedBirdsCriteriaDatasets_Simple(ByVal Schedule4ImportedBirdsCriteria As ReportCriteria.Schedule4ImportedBirdsCriteria) As Schedule4ImportedBirdsData()

            Dim Schedule4ImportedBirdsDataset As New Schedule4ImportedBirdsData
            Dim boSchedule4ImportedBirds_MainRow As Schedule4ImportedBirdsData.BOSchedule4ImportedBirds_MainRow
            Dim boSchedule4ImportedBirds_Sub1Row As Schedule4ImportedBirdsData.BOSchedule4ImportedBirds_Sub1Row

            ' BOSchedule4ImportedBirds_MainRow - Row 0
            boSchedule4ImportedBirds_MainRow = Schedule4ImportedBirdsDataset.BOSchedule4ImportedBirds_Main.NewBOSchedule4ImportedBirds_MainRow()
            boSchedule4ImportedBirds_MainRow.ApplicationId = 1
            boSchedule4ImportedBirds_MainRow.InspectionSection = False
            boSchedule4ImportedBirds_MainRow.KeeperDetails = "Defra Id: 12345" & Environment.NewLine & _
            "Mr G Figoni" & Environment.NewLine & _
            "Government Buildings" & Environment.NewLine & _
            "Epsom road" & Environment.NewLine & _
            "Guildford" & Environment.NewLine & _
            "Surrey" & Environment.NewLine & _
            "GU1 2LD"
            boSchedule4ImportedBirds_MainRow.ApplicationRef = "100007"
            boSchedule4ImportedBirds_MainRow.MaxSignatures = 1
            boSchedule4ImportedBirds_MainRow.InspectionSection = True
            boSchedule4ImportedBirds_MainRow.Convict5YearsNoX = ""
            boSchedule4ImportedBirds_MainRow.Convict5YearsYesX = ""
            boSchedule4ImportedBirds_MainRow.Convict3YearsNoX = ""
            boSchedule4ImportedBirds_MainRow.Convict3YearsYesX = ""
            Schedule4ImportedBirdsDataset.BOSchedule4ImportedBirds_Main.AddBOSchedule4ImportedBirds_MainRow(boSchedule4ImportedBirds_MainRow)

            ' boSchedule4ImportedBirds_Sub1Row - Bird 1
            boSchedule4ImportedBirds_Sub1Row = Schedule4ImportedBirdsDataset.BOSchedule4ImportedBirds_Sub1.NewBOSchedule4ImportedBirds_Sub1Row
            boSchedule4ImportedBirds_Sub1Row.ApplicationId = 1
            boSchedule4ImportedBirds_Sub1Row.BirdNo = "Bird 1"
            boSchedule4ImportedBirds_Sub1Row.Species = "ACCIPTER GENTILLIS"
            boSchedule4ImportedBirds_Sub1Row.CommonName = "GOSHAWK"
            boSchedule4ImportedBirds_Sub1Row.LicenceNumber = "534531513"
            boSchedule4ImportedBirds_Sub1Row.Article10CertNo = ""
            boSchedule4ImportedBirds_Sub1Row.OtherRingNo = "1G55846"
            boSchedule4ImportedBirds_Sub1Row.OtherRingType = "SWISS RING"
            boSchedule4ImportedBirds_Sub1Row.Microchip = "889222332"
            boSchedule4ImportedBirds_Sub1Row.DayAcquired = "02"
            boSchedule4ImportedBirds_Sub1Row.MonthAcquired = "04"
            boSchedule4ImportedBirds_Sub1Row.YearAcquired = "2004"

            boSchedule4ImportedBirds_Sub1Row.HatchDay = "05"
            boSchedule4ImportedBirds_Sub1Row.HatchMonth = "05"
            boSchedule4ImportedBirds_Sub1Row.HatchYear = "2001"
            boSchedule4ImportedBirds_Sub1Row.Sex = "FEMALE"

            boSchedule4ImportedBirds_Sub1Row.KeptAddress = "Bird Box House, " & _
            "Mr Bird, " & _
            "17 The Tree House, " & _
            "Epsom road, " & _
            "Guildford, " & _
            "Surrey, " & _
            "GU1 2LD"
            boSchedule4ImportedBirds_Sub1Row.ImportedOutsideEuYesX = "X"
            boSchedule4ImportedBirds_Sub1Row.ImportedOutsideEuNoX = ""
            boSchedule4ImportedBirds_Sub1Row.ImportedWithinEuYesX = ""
            boSchedule4ImportedBirds_Sub1Row.ImportedWithinEuNoX = "X"
            boSchedule4ImportedBirds_Sub1Row.ClosedRing = ""
            boSchedule4ImportedBirds_Sub1Row.QuarantineAddress = ""
            boSchedule4ImportedBirds_Sub1Row.QuarantineKeeper = ""
            boSchedule4ImportedBirds_Sub1Row.QuarantineDay = ""
            boSchedule4ImportedBirds_Sub1Row.QuarantineMonth = ""
            boSchedule4ImportedBirds_Sub1Row.QuarantineYear = ""

            boSchedule4ImportedBirds_Sub1Row.RingNo1 = "T5435"
            boSchedule4ImportedBirds_Sub1Row.RingNo2 = "SR6815"
            boSchedule4ImportedBirds_Sub1Row.RingNo3 = ""
            boSchedule4ImportedBirds_Sub1Row.RingNo4 = ""
            boSchedule4ImportedBirds_Sub1Row.RingNo5 = ""
            boSchedule4ImportedBirds_Sub1Row.RingNo6 = ""
            boSchedule4ImportedBirds_Sub1Row.RingNo7 = ""
            boSchedule4ImportedBirds_Sub1Row.RingNo8 = ""
            boSchedule4ImportedBirds_Sub1Row.RingNo9 = ""
            boSchedule4ImportedBirds_Sub1Row.RingNo10 = ""
            Schedule4ImportedBirdsDataset.BOSchedule4ImportedBirds_Sub1.AddBOSchedule4ImportedBirds_Sub1Row(boSchedule4ImportedBirds_Sub1Row)

            Schedule4ImportedBirdsDataset.AcceptChanges()



            Dim Schedule4ImportedBirdsDatasets(0) As Schedule4ImportedBirdsData
            Schedule4ImportedBirdsDatasets(0) = Schedule4ImportedBirdsDataset

            Return Schedule4ImportedBirdsDatasets

        End Function

        Public Function GetSchedule4ImportedBirdsCriteriaDatasets_Average(ByVal Schedule4ImportedBirdsCriteria As ReportCriteria.Schedule4ImportedBirdsCriteria) As Schedule4ImportedBirdsData()

            Dim Schedule4ImportedBirdsDataset As New Schedule4ImportedBirdsData
            Dim boSchedule4ImportedBirds_MainRow As Schedule4ImportedBirdsData.BOSchedule4ImportedBirds_MainRow
            Dim boSchedule4ImportedBirds_Sub1Row As Schedule4ImportedBirdsData.BOSchedule4ImportedBirds_Sub1Row
            Dim boSchedule4InspectorSigRow As Schedule4ImportedBirdsData.BOSchedule4InspectorSigRow


            ' BOSchedule4ImportedBirds_MainRow - Row 0
            boSchedule4ImportedBirds_MainRow = Schedule4ImportedBirdsDataset.BOSchedule4ImportedBirds_Main.NewBOSchedule4ImportedBirds_MainRow()
            boSchedule4ImportedBirds_MainRow.ApplicationId = 1
            boSchedule4ImportedBirds_MainRow.Barcode = "Barcode"
            boSchedule4ImportedBirds_MainRow.InspectionSection = False
            boSchedule4ImportedBirds_MainRow.KeeperDetails = "Defra Id: 12345" & Environment.NewLine & _
            "Mr G Figoni" & Environment.NewLine & _
            "Government Buildings" & Environment.NewLine & _
            "Epsom road" & Environment.NewLine & _
            "Guildford" & Environment.NewLine & _
            "Surrey" & Environment.NewLine & _
            "GU1 2LD"
            boSchedule4ImportedBirds_MainRow.ApplicationRef = "100008"
            boSchedule4ImportedBirds_MainRow.MaxSignatures = 2
            boSchedule4ImportedBirds_MainRow.InspectionSection = True
            boSchedule4ImportedBirds_MainRow.Convict5YearsNoX = ""
            boSchedule4ImportedBirds_MainRow.Convict5YearsYesX = ""
            boSchedule4ImportedBirds_MainRow.Convict3YearsNoX = ""
            boSchedule4ImportedBirds_MainRow.Convict3YearsYesX = ""
            Schedule4ImportedBirdsDataset.BOSchedule4ImportedBirds_Main.AddBOSchedule4ImportedBirds_MainRow(boSchedule4ImportedBirds_MainRow)

            ' BOSchedule4InspectorSigRow - Row 0
            boSchedule4InspectorSigRow = Schedule4ImportedBirdsDataset.BOSchedule4InspectorSig.NewBOSchedule4InspectorSigRow
            boSchedule4InspectorSigRow.ApplicationId = 1
            boSchedule4InspectorSigRow.BirdNo1 = "Bird 1"
            boSchedule4InspectorSigRow.BirdNo2 = "Bird 2"
            boSchedule4InspectorSigRow.BirdNo3 = ""
            boSchedule4InspectorSigRow.BirdNo4 = ""
            boSchedule4InspectorSigRow.BirdNo5 = ""
            Schedule4ImportedBirdsDataset.BOSchedule4InspectorSig.AddBOSchedule4InspectorSigRow(boSchedule4InspectorSigRow)

            ' boSchedule4ImportedBirds_Sub1Row - Bird 1
            boSchedule4ImportedBirds_Sub1Row = Schedule4ImportedBirdsDataset.BOSchedule4ImportedBirds_Sub1.NewBOSchedule4ImportedBirds_Sub1Row
            boSchedule4ImportedBirds_Sub1Row.ApplicationId = 1
            boSchedule4ImportedBirds_Sub1Row.BirdNo = "Bird 1"
            boSchedule4ImportedBirds_Sub1Row.Species = "ACCIPTER GENTILLIS"
            boSchedule4ImportedBirds_Sub1Row.CommonName = "PAPUAN GOSHAWK"
            boSchedule4ImportedBirds_Sub1Row.LicenceNumber = "534531513"
            boSchedule4ImportedBirds_Sub1Row.Article10CertNo = ""
            boSchedule4ImportedBirds_Sub1Row.OtherRingNo = "1G55846"
            boSchedule4ImportedBirds_Sub1Row.OtherRingType = "SWISS RING"
            boSchedule4ImportedBirds_Sub1Row.Microchip = "889222332"
            boSchedule4ImportedBirds_Sub1Row.DayAcquired = ""
            boSchedule4ImportedBirds_Sub1Row.MonthAcquired = ""
            boSchedule4ImportedBirds_Sub1Row.YearAcquired = ""
            boSchedule4ImportedBirds_Sub1Row.HatchDay = "05"
            boSchedule4ImportedBirds_Sub1Row.HatchMonth = "05"
            boSchedule4ImportedBirds_Sub1Row.HatchYear = "2001"
            boSchedule4ImportedBirds_Sub1Row.Sex = "FEMALE"
            boSchedule4ImportedBirds_Sub1Row.KeptAddress = ""
            boSchedule4ImportedBirds_Sub1Row.ImportedOutsideEuYesX = "X"
            boSchedule4ImportedBirds_Sub1Row.ImportedOutsideEuNoX = ""
            boSchedule4ImportedBirds_Sub1Row.ImportedWithinEuYesX = ""
            boSchedule4ImportedBirds_Sub1Row.ImportedWithinEuNoX = "X"
            boSchedule4ImportedBirds_Sub1Row.ClosedRing = ""
            boSchedule4ImportedBirds_Sub1Row.QuarantineAddress = ""
            boSchedule4ImportedBirds_Sub1Row.QuarantineKeeper = ""
            boSchedule4ImportedBirds_Sub1Row.QuarantineDay = ""
            boSchedule4ImportedBirds_Sub1Row.QuarantineMonth = ""
            boSchedule4ImportedBirds_Sub1Row.QuarantineYear = ""
            boSchedule4ImportedBirds_Sub1Row.RingNo1 = "T5435"
            boSchedule4ImportedBirds_Sub1Row.RingNo2 = "SR6815"
            boSchedule4ImportedBirds_Sub1Row.RingNo3 = ""
            boSchedule4ImportedBirds_Sub1Row.RingNo4 = ""
            boSchedule4ImportedBirds_Sub1Row.RingNo5 = ""
            boSchedule4ImportedBirds_Sub1Row.RingNo6 = ""
            boSchedule4ImportedBirds_Sub1Row.RingNo7 = ""
            boSchedule4ImportedBirds_Sub1Row.RingNo8 = ""
            boSchedule4ImportedBirds_Sub1Row.RingNo9 = ""
            boSchedule4ImportedBirds_Sub1Row.RingNo10 = ""
            Schedule4ImportedBirdsDataset.BOSchedule4ImportedBirds_Sub1.AddBOSchedule4ImportedBirds_Sub1Row(boSchedule4ImportedBirds_Sub1Row)

            ' boSchedule4ImportedBirds_Sub1Row - Bird 2
            boSchedule4ImportedBirds_Sub1Row = Schedule4ImportedBirdsDataset.BOSchedule4ImportedBirds_Sub1.NewBOSchedule4ImportedBirds_Sub1Row
            boSchedule4ImportedBirds_Sub1Row.ApplicationId = 1
            boSchedule4ImportedBirds_Sub1Row.BirdNo = "Bird 2"
            boSchedule4ImportedBirds_Sub1Row.Species = "ACCIPTER GENTILLIS"
            boSchedule4ImportedBirds_Sub1Row.CommonName = "PAPUAN GOSHAWK"
            boSchedule4ImportedBirds_Sub1Row.LicenceNumber = "534531513"
            boSchedule4ImportedBirds_Sub1Row.Article10CertNo = ""
            boSchedule4ImportedBirds_Sub1Row.OtherRingNo = "1G99254"
            boSchedule4ImportedBirds_Sub1Row.OtherRingType = "SWISS RING"
            boSchedule4ImportedBirds_Sub1Row.Microchip = "889254100"
            boSchedule4ImportedBirds_Sub1Row.DayAcquired = ""
            boSchedule4ImportedBirds_Sub1Row.MonthAcquired = ""
            boSchedule4ImportedBirds_Sub1Row.YearAcquired = ""
            boSchedule4ImportedBirds_Sub1Row.HatchDay = "07"
            boSchedule4ImportedBirds_Sub1Row.HatchMonth = "05"
            boSchedule4ImportedBirds_Sub1Row.HatchYear = "2002"
            boSchedule4ImportedBirds_Sub1Row.Sex = "MALE"
            boSchedule4ImportedBirds_Sub1Row.KeptAddress = ""
            boSchedule4ImportedBirds_Sub1Row.ImportedOutsideEuYesX = "X"
            boSchedule4ImportedBirds_Sub1Row.ImportedOutsideEuNoX = ""
            boSchedule4ImportedBirds_Sub1Row.ImportedWithinEuYesX = ""
            boSchedule4ImportedBirds_Sub1Row.ImportedWithinEuNoX = "X"
            boSchedule4ImportedBirds_Sub1Row.ClosedRing = ""
            boSchedule4ImportedBirds_Sub1Row.QuarantineAddress = ""
            boSchedule4ImportedBirds_Sub1Row.QuarantineKeeper = ""
            boSchedule4ImportedBirds_Sub1Row.QuarantineDay = ""
            boSchedule4ImportedBirds_Sub1Row.QuarantineMonth = ""
            boSchedule4ImportedBirds_Sub1Row.QuarantineYear = ""
            boSchedule4ImportedBirds_Sub1Row.RingNo1 = "T9879"
            boSchedule4ImportedBirds_Sub1Row.RingNo2 = "SR0259"
            boSchedule4ImportedBirds_Sub1Row.RingNo3 = ""
            boSchedule4ImportedBirds_Sub1Row.RingNo4 = ""
            boSchedule4ImportedBirds_Sub1Row.RingNo5 = ""
            boSchedule4ImportedBirds_Sub1Row.RingNo6 = ""
            boSchedule4ImportedBirds_Sub1Row.RingNo7 = ""
            boSchedule4ImportedBirds_Sub1Row.RingNo8 = ""
            boSchedule4ImportedBirds_Sub1Row.RingNo9 = ""
            boSchedule4ImportedBirds_Sub1Row.RingNo10 = ""
            Schedule4ImportedBirdsDataset.BOSchedule4ImportedBirds_Sub1.AddBOSchedule4ImportedBirds_Sub1Row(boSchedule4ImportedBirds_Sub1Row)

            Schedule4ImportedBirdsDataset.AcceptChanges()

            Dim Schedule4ImportedBirdsDatasets(0) As Schedule4ImportedBirdsData
            Schedule4ImportedBirdsDatasets(0) = Schedule4ImportedBirdsDataset

            Return Schedule4ImportedBirdsDatasets

        End Function

    End Class

End Namespace
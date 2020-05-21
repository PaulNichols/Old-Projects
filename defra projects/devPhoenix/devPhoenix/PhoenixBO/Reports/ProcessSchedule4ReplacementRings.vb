Imports System
Imports System.data
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Namespace RPT

    Public Class Schedule4ReplacementRings
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim schedule4ReplacementRingsCriteria As ReportCriteria.Schedule4ReplacementRingsCriteria = CType(reportCriteria, ReportCriteria.Schedule4ReplacementRingsCriteria)
            Dim schedule4ReplacementRingsDataset As New Schedule4ReplacementRingsData

            Dim reportData As New Application.Bird.Reports.ReportData
            Dim reportDataResults As ReportDataResults = reportData.GetSchedule4ReplacementRingsReportData(schedule4ReplacementRingsCriteria.ApplicationId, schedule4ReplacementRingsDataset.GetXmlSchema)
            Dim ReportDataset As DataSet = reportDataResults.ReportData

            Dim Schedule4ReplacementRings_Main_RPT As New Schedule4ReplacementRings_Main_RPT
            Dim reportResults(0) As BOReportResults
            reportResults(0) = DoReport(reportCriteria.Description, reportDataResults.SearchReference, schedule4ReplacementRingsCriteria.ApplicationId, reportDataResults.ReportData, schedule4ReplacementRingsCriteria, saveReport, _
                Schedule4ReplacementRings_Main_RPT, reportPrintJobId, printSequence)

            Return reportResults

        End Function


        Public Function GetSchedule4ReplacementRingsCriteriaDatasets_Simple(ByVal Schedule4ReplacementRingsCriteria As ReportCriteria.Schedule4ReplacementRingsCriteria) As Schedule4ReplacementRingsData()

            Dim Schedule4ReplacementRingsDataset As New Schedule4ReplacementRingsData
            Dim boSchedule4ReplacementRings_MainRow As Schedule4ReplacementRingsData.BOSchedule4ReplacementRings_MainRow
            Dim boSchedule4ReplacementRings_Sub1Row As Schedule4ReplacementRingsData.BOSchedule4ReplacementRings_Sub1Row

            ' BOSchedule4ReplacementRings_MainRow - Row 0
            boSchedule4ReplacementRings_MainRow = Schedule4ReplacementRingsDataset.BOSchedule4ReplacementRings_Main.NewBOSchedule4ReplacementRings_MainRow()
            boSchedule4ReplacementRings_MainRow.ApplicationId = 1
            boSchedule4ReplacementRings_MainRow.InspectionSection = True
            boSchedule4ReplacementRings_MainRow.KeeperDetails = "Defra Id: 12345" & Environment.NewLine & _
            "Mr G Figoni" & Environment.NewLine & _
            "Government Buildings" & Environment.NewLine & _
            "Epsom road" & Environment.NewLine & _
            "Guildford" & Environment.NewLine & _
            "Surrey" & Environment.NewLine & _
            "GU1 2LD"
            boSchedule4ReplacementRings_MainRow.ApplicationRef = "100013"
            boSchedule4ReplacementRings_MainRow.MaxSignatures = 1
            boSchedule4ReplacementRings_MainRow.InspectionSection = True
            Schedule4ReplacementRingsDataset.BOSchedule4ReplacementRings_Main.AddBOSchedule4ReplacementRings_MainRow(boSchedule4ReplacementRings_MainRow)

            ' boSchedule4ReplacementRings_Sub1Row - Bird 1
            boSchedule4ReplacementRings_Sub1Row = Schedule4ReplacementRingsDataset.BOSchedule4ReplacementRings_Sub1.NewBOSchedule4ReplacementRings_Sub1Row
            boSchedule4ReplacementRings_Sub1Row.ApplicationId = 1
            boSchedule4ReplacementRings_Sub1Row.BirdNo = "Bird 1"
            boSchedule4ReplacementRings_Sub1Row.Species = "ACCIPTER GENTILLIS"
            boSchedule4ReplacementRings_Sub1Row.CommonName = "GOSHAWK"

            boSchedule4ReplacementRings_Sub1Row.KeptAddress = ""
            boSchedule4ReplacementRings_Sub1Row.DateHatched = "29/03/2000"
            boSchedule4ReplacementRings_Sub1Row.Sex = "FEMALE"
            boSchedule4ReplacementRings_Sub1Row.Origin = "CAPTIVE"
            boSchedule4ReplacementRings_Sub1Row.DateAcquired = "29/03/2000"

            boSchedule4ReplacementRings_Sub1Row.RingNo1 = "T5120"
            boSchedule4ReplacementRings_Sub1Row.RingNo2 = "U1584"
            boSchedule4ReplacementRings_Sub1Row.RingNo3 = ""
            boSchedule4ReplacementRings_Sub1Row.RingNo4 = ""
            boSchedule4ReplacementRings_Sub1Row.RingNo5 = ""
            boSchedule4ReplacementRings_Sub1Row.RingNo6 = ""
            boSchedule4ReplacementRings_Sub1Row.RingNo7 = ""
            boSchedule4ReplacementRings_Sub1Row.RingNo8 = ""
            boSchedule4ReplacementRings_Sub1Row.RingNo9 = ""
            boSchedule4ReplacementRings_Sub1Row.RingNo10 = ""

            boSchedule4ReplacementRings_Sub1Row.IdMarkType1 = "DEFRA CLOSED RING T6840"
            boSchedule4ReplacementRings_Sub1Row.IdMarkType2 = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkType3 = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkType4 = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkType5 = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkType6 = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkType7 = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkType8 = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkType9 = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkType10 = ""

            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeFitted1X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeFitted2X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeFitted3X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeFitted4X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeFitted5X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeFitted6X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeFitted7X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeFitted8X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeFitted9X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeFitted10X = ""

            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeRemoved1X = "X"
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeRemoved2X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeRemoved3X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeRemoved4X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeRemoved5X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeRemoved6X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeRemoved7X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeRemoved8X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeRemoved9X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeRemoved10X = ""

            Schedule4ReplacementRingsDataset.BOSchedule4ReplacementRings_Sub1.AddBOSchedule4ReplacementRings_Sub1Row(boSchedule4ReplacementRings_Sub1Row)

            Schedule4ReplacementRingsDataset.AcceptChanges()


            Dim Schedule4ReplacementRingsDatasets(0) As Schedule4ReplacementRingsData
            Schedule4ReplacementRingsDatasets(0) = Schedule4ReplacementRingsDataset

            Return Schedule4ReplacementRingsDatasets

        End Function

        Public Function GetSchedule4ReplacementRingsCriteriaDatasets_Average(ByVal Schedule4ReplacementRingsCriteria As ReportCriteria.Schedule4ReplacementRingsCriteria) As Schedule4ReplacementRingsData()

            Dim Schedule4ReplacementRingsDataset As New Schedule4ReplacementRingsData
            Dim boSchedule4ReplacementRings_MainRow As Schedule4ReplacementRingsData.BOSchedule4ReplacementRings_MainRow
            Dim boSchedule4ReplacementRings_Sub1Row As Schedule4ReplacementRingsData.BOSchedule4ReplacementRings_Sub1Row

            ' BOSchedule4ReplacementRings_MainRow - Row 0
            boSchedule4ReplacementRings_MainRow = Schedule4ReplacementRingsDataset.BOSchedule4ReplacementRings_Main.NewBOSchedule4ReplacementRings_MainRow()
            boSchedule4ReplacementRings_MainRow.ApplicationId = 1
            boSchedule4ReplacementRings_MainRow.InspectionSection = True
            boSchedule4ReplacementRings_MainRow.KeeperDetails = "Defra Id: 12345" & Environment.NewLine & _
            "Mr G Figoni" & Environment.NewLine & _
            "Government Buildings" & Environment.NewLine & _
            "Epsom road" & Environment.NewLine & _
            "Guildford" & Environment.NewLine & _
            "Surrey" & Environment.NewLine & _
            "GU1 2LD"
            boSchedule4ReplacementRings_MainRow.ApplicationRef = "100014"
            boSchedule4ReplacementRings_MainRow.MaxSignatures = 2
            boSchedule4ReplacementRings_MainRow.InspectionSection = True
            Schedule4ReplacementRingsDataset.BOSchedule4ReplacementRings_Main.AddBOSchedule4ReplacementRings_MainRow(boSchedule4ReplacementRings_MainRow)

            ' boSchedule4ReplacementRings_Sub1Row - Bird 1
            boSchedule4ReplacementRings_Sub1Row = Schedule4ReplacementRingsDataset.BOSchedule4ReplacementRings_Sub1.NewBOSchedule4ReplacementRings_Sub1Row
            boSchedule4ReplacementRings_Sub1Row.ApplicationId = 1
            boSchedule4ReplacementRings_Sub1Row.BirdNo = "Bird 1"
            boSchedule4ReplacementRings_Sub1Row.Species = "AVICEDA MADAGASCARIENSIS"
            boSchedule4ReplacementRings_Sub1Row.CommonName = "MADAGASCAR CUCKOO-FALCON"

            boSchedule4ReplacementRings_Sub1Row.KeptAddress = ""
            boSchedule4ReplacementRings_Sub1Row.DateHatched = "01/07/1998"
            boSchedule4ReplacementRings_Sub1Row.Sex = "FEMALE"
            boSchedule4ReplacementRings_Sub1Row.Origin = "IMPORTED"
            boSchedule4ReplacementRings_Sub1Row.DateAcquired = "01/05/2003"

            boSchedule4ReplacementRings_Sub1Row.RingNo1 = "0987B"
            boSchedule4ReplacementRings_Sub1Row.RingNo2 = "C4646"
            boSchedule4ReplacementRings_Sub1Row.RingNo3 = ""
            boSchedule4ReplacementRings_Sub1Row.RingNo4 = ""
            boSchedule4ReplacementRings_Sub1Row.RingNo5 = ""
            boSchedule4ReplacementRings_Sub1Row.RingNo6 = ""
            boSchedule4ReplacementRings_Sub1Row.RingNo7 = ""
            boSchedule4ReplacementRings_Sub1Row.RingNo8 = ""
            boSchedule4ReplacementRings_Sub1Row.RingNo9 = ""
            boSchedule4ReplacementRings_Sub1Row.RingNo10 = ""

            boSchedule4ReplacementRings_Sub1Row.IdMarkType1 = "DEFRA CLOSED RING 3515X"
            boSchedule4ReplacementRings_Sub1Row.IdMarkType2 = "SWISS RING 1F89691"
            boSchedule4ReplacementRings_Sub1Row.IdMarkType3 = "SWISS RING 1F35170"
            boSchedule4ReplacementRings_Sub1Row.IdMarkType4 = "SPLIT RING E3219"
            boSchedule4ReplacementRings_Sub1Row.IdMarkType5 = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkType6 = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkType7 = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkType8 = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkType9 = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkType10 = ""

            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeFitted1X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeFitted2X = "X"
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeFitted3X = "X"
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeFitted4X = "X"
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeFitted5X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeFitted6X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeFitted7X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeFitted8X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeFitted9X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeFitted10X = ""

            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeRemoved1X = "X"
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeRemoved2X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeRemoved3X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeRemoved4X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeRemoved5X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeRemoved6X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeRemoved7X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeRemoved8X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeRemoved9X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeRemoved10X = ""

            Schedule4ReplacementRingsDataset.BOSchedule4ReplacementRings_Sub1.AddBOSchedule4ReplacementRings_Sub1Row(boSchedule4ReplacementRings_Sub1Row)

            ' boSchedule4ReplacementRings_Sub1Row - Bird 2
            boSchedule4ReplacementRings_Sub1Row = Schedule4ReplacementRingsDataset.BOSchedule4ReplacementRings_Sub1.NewBOSchedule4ReplacementRings_Sub1Row
            boSchedule4ReplacementRings_Sub1Row.ApplicationId = 1
            boSchedule4ReplacementRings_Sub1Row.BirdNo = "Bird 2"
            boSchedule4ReplacementRings_Sub1Row.Species = "FALCO PEREGRINUS"
            boSchedule4ReplacementRings_Sub1Row.CommonName = "PEREGRINE FALCON"

            boSchedule4ReplacementRings_Sub1Row.KeptAddress = ""
            boSchedule4ReplacementRings_Sub1Row.DateHatched = "09/02/2001"
            boSchedule4ReplacementRings_Sub1Row.Sex = "MALE"
            boSchedule4ReplacementRings_Sub1Row.Origin = "UNKNOWN"
            boSchedule4ReplacementRings_Sub1Row.DateAcquired = "15/04/2003"

            boSchedule4ReplacementRings_Sub1Row.RingNo1 = "1952B"
            boSchedule4ReplacementRings_Sub1Row.RingNo2 = "C3257"
            boSchedule4ReplacementRings_Sub1Row.RingNo3 = ""
            boSchedule4ReplacementRings_Sub1Row.RingNo4 = ""
            boSchedule4ReplacementRings_Sub1Row.RingNo5 = ""
            boSchedule4ReplacementRings_Sub1Row.RingNo6 = ""
            boSchedule4ReplacementRings_Sub1Row.RingNo7 = ""
            boSchedule4ReplacementRings_Sub1Row.RingNo8 = ""
            boSchedule4ReplacementRings_Sub1Row.RingNo9 = ""
            boSchedule4ReplacementRings_Sub1Row.RingNo10 = ""

            boSchedule4ReplacementRings_Sub1Row.IdMarkType1 = "DEFRA CLOSED RING 4406X"
            boSchedule4ReplacementRings_Sub1Row.IdMarkType2 = "SPLIT RING G4110"
            boSchedule4ReplacementRings_Sub1Row.IdMarkType3 = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkType4 = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkType5 = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkType6 = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkType7 = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkType8 = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkType9 = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkType10 = ""

            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeFitted1X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeFitted2X = "X"
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeFitted3X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeFitted4X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeFitted5X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeFitted6X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeFitted7X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeFitted8X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeFitted9X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeFitted10X = ""

            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeRemoved1X = "X"
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeRemoved2X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeRemoved3X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeRemoved4X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeRemoved5X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeRemoved6X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeRemoved7X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeRemoved8X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeRemoved9X = ""
            boSchedule4ReplacementRings_Sub1Row.IdMarkTypeRemoved10X = ""

            Schedule4ReplacementRingsDataset.BOSchedule4ReplacementRings_Sub1.AddBOSchedule4ReplacementRings_Sub1Row(boSchedule4ReplacementRings_Sub1Row)


            Schedule4ReplacementRingsDataset.AcceptChanges()


            Dim Schedule4ReplacementRingsDatasets(0) As Schedule4ReplacementRingsData
            Schedule4ReplacementRingsDatasets(0) = Schedule4ReplacementRingsDataset

            Return Schedule4ReplacementRingsDatasets

        End Function

    End Class

End Namespace
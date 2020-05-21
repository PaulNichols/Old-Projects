Imports System
Imports System.data
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Namespace RPT

    Public Class Schedule4Chicks
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Integer, ByRef printSequence As Integer, ByVal saveReport As Boolean) As BOReportResults()
            Dim schedule4ChicksCriteria As reportCriteria.Schedule4ChicksCriteria = CType(reportCriteria, reportCriteria.Schedule4ChicksCriteria)
            Dim schedule4ChicksDataset As New Schedule4ChicksData

            Dim reportData As New Application.Bird.Reports.ReportData
            Dim reportDataResults As reportDataResults = reportData.GetSchedule4ChicksReportData(schedule4ChicksCriteria.ApplicationId, schedule4ChicksDataset.GetXmlSchema)
            Dim ReportDataset As DataSet = reportDataResults.ReportData

            Dim Schedule4Chicks_Main_RPT As New Schedule4Chicks_Main_RPT
            Dim reportResults(0) As BOReportResults
            reportResults(0) = DoReport(reportCriteria.Description, reportDataResults.SearchReference, schedule4ChicksCriteria.ApplicationId, reportDataResults.ReportData, schedule4ChicksCriteria, saveReport, _
                Schedule4Chicks_Main_RPT, reportPrintJobId, printSequence)

            Return reportResults

        End Function

        Private Function GetSchedule4ChicksCriteriaDatasets_Simple(ByVal Schedule4ChicksCriteria As ReportCriteria.Schedule4ChicksCriteria) As Schedule4ChicksData()
            Try

                Dim Schedule4ChicksDataset As New Schedule4ChicksData
                Dim boSchedule4Chicks_MainRow As Schedule4ChicksData.BOSchedule4Chicks_MainRow
                Dim boSchedule4Chicks_Sub1Row As Schedule4ChicksData.BOSchedule4Chicks_Sub1Row
                Dim boSchedule4Chicks_Sub2Row As Schedule4ChicksData.BOSchedule4Chicks_Sub2Row

                ' BOSchedule4Chicks_MainRow - Row 0
                boSchedule4Chicks_MainRow = Schedule4ChicksDataset.BOSchedule4Chicks_Main.NewBOSchedule4Chicks_MainRow()
                boSchedule4Chicks_MainRow.ApplicationId = 1
                boSchedule4Chicks_MainRow.InspectionSection = True
                boSchedule4Chicks_MainRow.KeeperDetails = "Defra Id: 12345" & Environment.NewLine & _
                "Mr G Figoni" & Environment.NewLine & _
                "Government Buildings" & Environment.NewLine & _
                "Epsom road" & Environment.NewLine & _
                "Guildford" & Environment.NewLine & _
                "Surrey" & Environment.NewLine & _
                "GU1 2LD"
                boSchedule4Chicks_MainRow.RingRequestRef = "100004"
                boSchedule4Chicks_MainRow.NumOfEggs = "3"
                boSchedule4Chicks_MainRow.LastEggLaid = "14/04/2004"
                boSchedule4Chicks_MainRow.MaxSignatures = 3
                boSchedule4Chicks_MainRow.InspectionSection = True
                boSchedule4Chicks_MainRow.Convict5YearsNoX = ""
                boSchedule4Chicks_MainRow.Convict5YearsYesX = ""
                boSchedule4Chicks_MainRow.Convict3YearsNoX = ""
                boSchedule4Chicks_MainRow.Convict3YearsYesX = ""
                Schedule4ChicksDataset.BOSchedule4Chicks_Main.AddBOSchedule4Chicks_MainRow(boSchedule4Chicks_MainRow)

                ' BOSchedule4Chicks_MainRow - Row 0
                boSchedule4Chicks_Sub1Row = Schedule4ChicksDataset.BOSchedule4Chicks_Sub1.NewBOSchedule4Chicks_Sub1Row
                boSchedule4Chicks_Sub1Row.ApplicationId = 1
                boSchedule4Chicks_Sub1Row.Gender = "Male 1"
                boSchedule4Chicks_Sub1Row.IdMarkType = "DEFRA CLOSED RING 0372H"
                boSchedule4Chicks_Sub1Row.Species = "FALCO PEREGRINUS"
                boSchedule4Chicks_Sub1Row.CommonName = "PEREGRINE FALCON"
                boSchedule4Chicks_Sub1Row.RegKeeperYes = "X"
                boSchedule4Chicks_Sub1Row.RegKeeperNo = ""
                Schedule4ChicksDataset.BOSchedule4Chicks_Sub1.AddBOSchedule4Chicks_Sub1Row(boSchedule4Chicks_Sub1Row)

                ' BOSchedule4Chicks_MainRow - Row 1
                boSchedule4Chicks_Sub1Row = Schedule4ChicksDataset.BOSchedule4Chicks_Sub1.NewBOSchedule4Chicks_Sub1Row
                boSchedule4Chicks_Sub1Row.ApplicationId = 1
                boSchedule4Chicks_Sub1Row.Gender = "Female 1"
                boSchedule4Chicks_Sub1Row.IdMarkType = "DEFRA CLOSED RING  00388H"
                boSchedule4Chicks_Sub1Row.Species = "FALCO PEREGRINUS"
                boSchedule4Chicks_Sub1Row.CommonName = "PEREGRINE FALCON"
                boSchedule4Chicks_Sub1Row.RegKeeperYes = "X"
                boSchedule4Chicks_Sub1Row.RegKeeperNo = ""
                Schedule4ChicksDataset.BOSchedule4Chicks_Sub1.AddBOSchedule4Chicks_Sub1Row(boSchedule4Chicks_Sub1Row)

                ' boSchedule4Chicks_Sub2Row - Bird 1
                boSchedule4Chicks_Sub2Row = Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.NewBOSchedule4Chicks_Sub2Row
                boSchedule4Chicks_Sub2Row.ApplicationId = 1
                boSchedule4Chicks_Sub2Row.BirdNo = "Bird 1"
                boSchedule4Chicks_Sub2Row.ExpectedSpecies = "FALCO PEREGRINUS"
                boSchedule4Chicks_Sub2Row.CommonName = "PEREGRINE FALCON"
                boSchedule4Chicks_Sub2Row.KeptAddress = ""
                boSchedule4Chicks_Sub2Row.RingNo1 = "24583H"
                boSchedule4Chicks_Sub2Row.RingNo2 = "82681H"
                boSchedule4Chicks_Sub2Row.RingNo3 = ""
                boSchedule4Chicks_Sub2Row.RingNo4 = ""
                boSchedule4Chicks_Sub2Row.RingNo5 = ""
                boSchedule4Chicks_Sub2Row.RingNo6 = ""
                boSchedule4Chicks_Sub2Row.RingNo7 = ""
                boSchedule4Chicks_Sub2Row.RingNo8 = ""
                boSchedule4Chicks_Sub2Row.RingNo9 = ""
                boSchedule4Chicks_Sub2Row.RingNo10 = ""
                Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.AddBOSchedule4Chicks_Sub2Row(boSchedule4Chicks_Sub2Row)

                ' boSchedule4Chicks_Sub2Row - Bird 2
                boSchedule4Chicks_Sub2Row = Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.NewBOSchedule4Chicks_Sub2Row
                boSchedule4Chicks_Sub2Row.ApplicationId = 1
                boSchedule4Chicks_Sub2Row.BirdNo = "Bird 2"
                boSchedule4Chicks_Sub2Row.ExpectedSpecies = "FALCO PEREGRINUS"
                boSchedule4Chicks_Sub2Row.CommonName = "PEREGRINE FALCON"
                boSchedule4Chicks_Sub2Row.KeptAddress = ""
                boSchedule4Chicks_Sub2Row.RingNo1 = "26801H"
                boSchedule4Chicks_Sub2Row.RingNo2 = "31354H"
                boSchedule4Chicks_Sub2Row.RingNo3 = ""
                boSchedule4Chicks_Sub2Row.RingNo4 = ""
                boSchedule4Chicks_Sub2Row.RingNo5 = ""
                boSchedule4Chicks_Sub2Row.RingNo6 = ""
                boSchedule4Chicks_Sub2Row.RingNo7 = ""
                boSchedule4Chicks_Sub2Row.RingNo8 = ""
                boSchedule4Chicks_Sub2Row.RingNo9 = ""
                boSchedule4Chicks_Sub2Row.RingNo10 = ""
                Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.AddBOSchedule4Chicks_Sub2Row(boSchedule4Chicks_Sub2Row)

                ' boSchedule4Chicks_Sub2Row - Bird 3
                boSchedule4Chicks_Sub2Row = Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.NewBOSchedule4Chicks_Sub2Row
                boSchedule4Chicks_Sub2Row.ApplicationId = 1
                boSchedule4Chicks_Sub2Row.BirdNo = "Bird 3"
                boSchedule4Chicks_Sub2Row.ExpectedSpecies = "FALCO PEREGRINUS"
                boSchedule4Chicks_Sub2Row.CommonName = "PEREGRINE FALCON"
                boSchedule4Chicks_Sub2Row.KeptAddress = ""
                boSchedule4Chicks_Sub2Row.RingNo1 = "35488H"
                boSchedule4Chicks_Sub2Row.RingNo2 = "11445H"
                boSchedule4Chicks_Sub2Row.RingNo3 = ""
                boSchedule4Chicks_Sub2Row.RingNo4 = ""
                boSchedule4Chicks_Sub2Row.RingNo5 = ""
                boSchedule4Chicks_Sub2Row.RingNo6 = ""
                boSchedule4Chicks_Sub2Row.RingNo7 = ""
                boSchedule4Chicks_Sub2Row.RingNo8 = ""
                boSchedule4Chicks_Sub2Row.RingNo9 = ""
                boSchedule4Chicks_Sub2Row.RingNo10 = ""
                Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.AddBOSchedule4Chicks_Sub2Row(boSchedule4Chicks_Sub2Row)

                Schedule4ChicksDataset.AcceptChanges()

                Dim Schedule4ChicksDatasets(0) As Schedule4ChicksData
                Schedule4ChicksDatasets(0) = Schedule4ChicksDataset

                Return Schedule4ChicksDatasets

            Catch lo_Exception As Exception
                Dim ls As String = lo_Exception.Message
            End Try

        End Function

        Private Function GetSchedule4ChicksCriteriaDatasets_Average(ByVal Schedule4ChicksCriteria As ReportCriteria.Schedule4ChicksCriteria) As Schedule4ChicksData()
            Try

                Dim Schedule4ChicksDataset As New Schedule4ChicksData
                Dim boSchedule4Chicks_MainRow As Schedule4ChicksData.BOSchedule4Chicks_MainRow
                Dim boSchedule4Chicks_Sub1Row As Schedule4ChicksData.BOSchedule4Chicks_Sub1Row
                Dim boSchedule4Chicks_Sub2Row As Schedule4ChicksData.BOSchedule4Chicks_Sub2Row

                ' BOSchedule4Chicks_MainRow - Row 0
                boSchedule4Chicks_MainRow = Schedule4ChicksDataset.BOSchedule4Chicks_Main.NewBOSchedule4Chicks_MainRow()
                boSchedule4Chicks_MainRow.ApplicationId = 1
                boSchedule4Chicks_MainRow.InspectionSection = True
                boSchedule4Chicks_MainRow.KeeperDetails = "Defra Id: 12345" & Environment.NewLine & _
                "Mr G Figoni" & Environment.NewLine & _
                "Government Buildings" & Environment.NewLine & _
                "Epsom road" & Environment.NewLine & _
                "Guildford" & Environment.NewLine & _
                "Surrey" & Environment.NewLine & _
                "GU1 2LD"
                boSchedule4Chicks_MainRow.RingRequestRef = "100005"
                boSchedule4Chicks_MainRow.NumOfEggs = "5"
                boSchedule4Chicks_MainRow.LastEggLaid = "14/04/2004"
                boSchedule4Chicks_MainRow.MaxSignatures = 5
                boSchedule4Chicks_MainRow.InspectionSection = True
                boSchedule4Chicks_MainRow.Convict5YearsNoX = ""
                boSchedule4Chicks_MainRow.Convict5YearsYesX = ""
                boSchedule4Chicks_MainRow.Convict3YearsNoX = ""
                boSchedule4Chicks_MainRow.Convict3YearsYesX = ""
                Schedule4ChicksDataset.BOSchedule4Chicks_Main.AddBOSchedule4Chicks_MainRow(boSchedule4Chicks_MainRow)

                ' BOSchedule4Chicks_MainRow - Row 0
                boSchedule4Chicks_Sub1Row = Schedule4ChicksDataset.BOSchedule4Chicks_Sub1.NewBOSchedule4Chicks_Sub1Row
                boSchedule4Chicks_Sub1Row.ApplicationId = 1
                boSchedule4Chicks_Sub1Row.Gender = "Male 1"
                boSchedule4Chicks_Sub1Row.IdMarkType = "DEFRA CLOSED RING 0372H"
                boSchedule4Chicks_Sub1Row.Species = "FALCO PEREGRINUS"
                boSchedule4Chicks_Sub1Row.CommonName = "PEREGRINE FALCON"
                boSchedule4Chicks_Sub1Row.RegKeeperYes = "X"
                boSchedule4Chicks_Sub1Row.RegKeeperNo = ""
                Schedule4ChicksDataset.BOSchedule4Chicks_Sub1.AddBOSchedule4Chicks_Sub1Row(boSchedule4Chicks_Sub1Row)

                ' BOSchedule4Chicks_MainRow - Row 1
                boSchedule4Chicks_Sub1Row = Schedule4ChicksDataset.BOSchedule4Chicks_Sub1.NewBOSchedule4Chicks_Sub1Row
                boSchedule4Chicks_Sub1Row.ApplicationId = 1
                boSchedule4Chicks_Sub1Row.Gender = "Male 2"
                boSchedule4Chicks_Sub1Row.IdMarkType = "UNRINGED LICENCE UR19608"
                boSchedule4Chicks_Sub1Row.Species = "LOXIA PYTYOPSITTACUS"
                boSchedule4Chicks_Sub1Row.CommonName = "PARROT CROSSBILL"
                boSchedule4Chicks_Sub1Row.RegKeeperYes = "X"
                boSchedule4Chicks_Sub1Row.RegKeeperNo = ""
                Schedule4ChicksDataset.BOSchedule4Chicks_Sub1.AddBOSchedule4Chicks_Sub1Row(boSchedule4Chicks_Sub1Row)

                ' BOSchedule4Chicks_MainRow - Row 2
                boSchedule4Chicks_Sub1Row = Schedule4ChicksDataset.BOSchedule4Chicks_Sub1.NewBOSchedule4Chicks_Sub1Row
                boSchedule4Chicks_Sub1Row.ApplicationId = 1
                boSchedule4Chicks_Sub1Row.Gender = "Female 1"
                boSchedule4Chicks_Sub1Row.IdMarkType = "UNRINGED LICENCE UR19612"
                boSchedule4Chicks_Sub1Row.Species = "LOXIA PYTYOPSITTACUS"
                boSchedule4Chicks_Sub1Row.CommonName = "PARROT CROSSBILL"
                boSchedule4Chicks_Sub1Row.RegKeeperYes = "X"
                boSchedule4Chicks_Sub1Row.RegKeeperNo = ""
                Schedule4ChicksDataset.BOSchedule4Chicks_Sub1.AddBOSchedule4Chicks_Sub1Row(boSchedule4Chicks_Sub1Row)

                ' boSchedule4Chicks_Sub2Row - Bird 1
                boSchedule4Chicks_Sub2Row = Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.NewBOSchedule4Chicks_Sub2Row
                boSchedule4Chicks_Sub2Row.ApplicationId = 1
                boSchedule4Chicks_Sub2Row.BirdNo = "Bird 1"
                boSchedule4Chicks_Sub2Row.ExpectedSpecies = "FALCO PEREGRINUS/ LOXIA PYTYOPSITTACUS"
                boSchedule4Chicks_Sub2Row.CommonName = "PEREGRINE FALCON/ PARROT CROSSBILL"
                boSchedule4Chicks_Sub2Row.KeptAddress = ""
                boSchedule4Chicks_Sub2Row.RingNo1 = "24583H"
                boSchedule4Chicks_Sub2Row.RingNo2 = "82861H"
                boSchedule4Chicks_Sub2Row.RingNo3 = ""
                boSchedule4Chicks_Sub2Row.RingNo4 = ""
                boSchedule4Chicks_Sub2Row.RingNo5 = ""
                boSchedule4Chicks_Sub2Row.RingNo6 = ""
                boSchedule4Chicks_Sub2Row.RingNo7 = ""
                boSchedule4Chicks_Sub2Row.RingNo8 = ""
                boSchedule4Chicks_Sub2Row.RingNo9 = ""
                boSchedule4Chicks_Sub2Row.RingNo10 = ""
                Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.AddBOSchedule4Chicks_Sub2Row(boSchedule4Chicks_Sub2Row)

                ' boSchedule4Chicks_Sub2Row - Bird 2
                boSchedule4Chicks_Sub2Row = Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.NewBOSchedule4Chicks_Sub2Row
                boSchedule4Chicks_Sub2Row.ApplicationId = 1
                boSchedule4Chicks_Sub2Row.BirdNo = "Bird 2"
                boSchedule4Chicks_Sub2Row.ExpectedSpecies = "FALCO PEREGRINUS/ LOXIA PYTYOPSITTACUS"
                boSchedule4Chicks_Sub2Row.CommonName = "PEREGRINE FALCON/ PARROT CROSSBILL"
                boSchedule4Chicks_Sub2Row.KeptAddress = ""
                boSchedule4Chicks_Sub2Row.RingNo1 = "28601H"
                boSchedule4Chicks_Sub2Row.RingNo2 = "31354H"
                boSchedule4Chicks_Sub2Row.RingNo3 = ""
                boSchedule4Chicks_Sub2Row.RingNo4 = ""
                boSchedule4Chicks_Sub2Row.RingNo5 = ""
                boSchedule4Chicks_Sub2Row.RingNo6 = ""
                boSchedule4Chicks_Sub2Row.RingNo7 = ""
                boSchedule4Chicks_Sub2Row.RingNo8 = ""
                boSchedule4Chicks_Sub2Row.RingNo9 = ""
                boSchedule4Chicks_Sub2Row.RingNo10 = ""
                Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.AddBOSchedule4Chicks_Sub2Row(boSchedule4Chicks_Sub2Row)

                ' boSchedule4Chicks_Sub2Row - Bird 3
                boSchedule4Chicks_Sub2Row = Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.NewBOSchedule4Chicks_Sub2Row
                boSchedule4Chicks_Sub2Row.ApplicationId = 1
                boSchedule4Chicks_Sub2Row.BirdNo = "Bird 3"
                boSchedule4Chicks_Sub2Row.ExpectedSpecies = "FALCO PEREGRINUS/ LOXIA PYTYOPSITTACUS"
                boSchedule4Chicks_Sub2Row.CommonName = "PEREGRINE FALCON/ PARROT CROSSBILL"
                boSchedule4Chicks_Sub2Row.KeptAddress = ""
                boSchedule4Chicks_Sub2Row.RingNo1 = "35488H"
                boSchedule4Chicks_Sub2Row.RingNo2 = "11445H"
                boSchedule4Chicks_Sub2Row.RingNo3 = ""
                boSchedule4Chicks_Sub2Row.RingNo4 = ""
                boSchedule4Chicks_Sub2Row.RingNo5 = ""
                boSchedule4Chicks_Sub2Row.RingNo6 = ""
                boSchedule4Chicks_Sub2Row.RingNo7 = ""
                boSchedule4Chicks_Sub2Row.RingNo8 = ""
                boSchedule4Chicks_Sub2Row.RingNo9 = ""
                boSchedule4Chicks_Sub2Row.RingNo10 = ""
                Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.AddBOSchedule4Chicks_Sub2Row(boSchedule4Chicks_Sub2Row)

                ' boSchedule4Chicks_Sub2Row - Bird 4
                boSchedule4Chicks_Sub2Row = Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.NewBOSchedule4Chicks_Sub2Row
                boSchedule4Chicks_Sub2Row.ApplicationId = 1
                boSchedule4Chicks_Sub2Row.BirdNo = "Bird 4"
                boSchedule4Chicks_Sub2Row.ExpectedSpecies = "FALCO PEREGRINUS/ LOXIA PYTYOPSITTACUS"
                boSchedule4Chicks_Sub2Row.CommonName = "PEREGRINE FALCON/ PARROT CROSSBILL"
                boSchedule4Chicks_Sub2Row.KeptAddress = ""
                boSchedule4Chicks_Sub2Row.RingNo1 = "14985H"
                boSchedule4Chicks_Sub2Row.RingNo2 = "68455H"
                boSchedule4Chicks_Sub2Row.RingNo3 = ""
                boSchedule4Chicks_Sub2Row.RingNo4 = ""
                boSchedule4Chicks_Sub2Row.RingNo5 = ""
                boSchedule4Chicks_Sub2Row.RingNo6 = ""
                boSchedule4Chicks_Sub2Row.RingNo7 = ""
                boSchedule4Chicks_Sub2Row.RingNo8 = ""
                boSchedule4Chicks_Sub2Row.RingNo9 = ""
                boSchedule4Chicks_Sub2Row.RingNo10 = ""
                Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.AddBOSchedule4Chicks_Sub2Row(boSchedule4Chicks_Sub2Row)

                ' boSchedule4Chicks_Sub2Row - Bird 5
                boSchedule4Chicks_Sub2Row = Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.NewBOSchedule4Chicks_Sub2Row
                boSchedule4Chicks_Sub2Row.ApplicationId = 1
                boSchedule4Chicks_Sub2Row.BirdNo = "Bird 5"
                boSchedule4Chicks_Sub2Row.ExpectedSpecies = "FALCO PEREGRINUS/ LOXIA PYTYOPSITTACUS"
                boSchedule4Chicks_Sub2Row.CommonName = "PEREGRINE FALCON/ PARROT CROSSBILL"
                boSchedule4Chicks_Sub2Row.KeptAddress = ""
                boSchedule4Chicks_Sub2Row.RingNo1 = "23457H"
                boSchedule4Chicks_Sub2Row.RingNo2 = "78589H"
                boSchedule4Chicks_Sub2Row.RingNo3 = ""
                boSchedule4Chicks_Sub2Row.RingNo4 = ""
                boSchedule4Chicks_Sub2Row.RingNo5 = ""
                boSchedule4Chicks_Sub2Row.RingNo6 = ""
                boSchedule4Chicks_Sub2Row.RingNo7 = ""
                boSchedule4Chicks_Sub2Row.RingNo8 = ""
                boSchedule4Chicks_Sub2Row.RingNo9 = ""
                boSchedule4Chicks_Sub2Row.RingNo10 = ""
                Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.AddBOSchedule4Chicks_Sub2Row(boSchedule4Chicks_Sub2Row)

                Schedule4ChicksDataset.AcceptChanges()

                Dim Schedule4ChicksDatasets(0) As Schedule4ChicksData
                Schedule4ChicksDatasets(0) = Schedule4ChicksDataset

                Return Schedule4ChicksDatasets

            Catch lo_Exception As Exception
                Dim ls As String = lo_Exception.Message
            End Try

        End Function

        Private Function GetSchedule4ChicksCriteriaDatasets_Complex(ByVal Schedule4ChicksCriteria As ReportCriteria.Schedule4ChicksCriteria) As Schedule4ChicksData()
            Try

                Dim Schedule4ChicksDataset As New Schedule4ChicksData
                Dim boSchedule4Chicks_MainRow As Schedule4ChicksData.BOSchedule4Chicks_MainRow
                Dim boSchedule4Chicks_Sub1Row As Schedule4ChicksData.BOSchedule4Chicks_Sub1Row
                Dim boSchedule4Chicks_Sub2Row As Schedule4ChicksData.BOSchedule4Chicks_Sub2Row

                ' BOSchedule4Chicks_MainRow - Row 0
                boSchedule4Chicks_MainRow = Schedule4ChicksDataset.BOSchedule4Chicks_Main.NewBOSchedule4Chicks_MainRow()
                boSchedule4Chicks_MainRow.ApplicationId = 1
                boSchedule4Chicks_MainRow.InspectionSection = True
                boSchedule4Chicks_MainRow.KeeperDetails = "Defra Id: 12345" & Environment.NewLine & _
                "Mr G Figoni" & Environment.NewLine & _
                "Government Buildings" & Environment.NewLine & _
                "Epsom road" & Environment.NewLine & _
                "Guildford" & Environment.NewLine & _
                "Surrey" & Environment.NewLine & _
                "GU1 2LD"
                boSchedule4Chicks_MainRow.RingRequestRef = "100006"
                boSchedule4Chicks_MainRow.NumOfEggs = "12"
                boSchedule4Chicks_MainRow.LastEggLaid = "09/04/2004"
                boSchedule4Chicks_MainRow.MaxSignatures = 12
                boSchedule4Chicks_MainRow.InspectionSection = True
                boSchedule4Chicks_MainRow.Convict5YearsNoX = ""
                boSchedule4Chicks_MainRow.Convict5YearsYesX = ""
                boSchedule4Chicks_MainRow.Convict3YearsNoX = ""
                boSchedule4Chicks_MainRow.Convict3YearsYesX = ""
                Schedule4ChicksDataset.BOSchedule4Chicks_Main.AddBOSchedule4Chicks_MainRow(boSchedule4Chicks_MainRow)

                ' BOSchedule4Chicks_MainRow - Row 0
                boSchedule4Chicks_Sub1Row = Schedule4ChicksDataset.BOSchedule4Chicks_Sub1.NewBOSchedule4Chicks_Sub1Row
                boSchedule4Chicks_Sub1Row.ApplicationId = 1
                boSchedule4Chicks_Sub1Row.Gender = "Male 1"
                boSchedule4Chicks_Sub1Row.IdMarkType = "DEFRA CLOSED RING 0372H"
                boSchedule4Chicks_Sub1Row.Species = "FALCO PEREGRINUS"
                boSchedule4Chicks_Sub1Row.CommonName = "PEREGRINE FALCON"
                boSchedule4Chicks_Sub1Row.RegKeeperYes = "X"
                boSchedule4Chicks_Sub1Row.RegKeeperNo = ""
                Schedule4ChicksDataset.BOSchedule4Chicks_Sub1.AddBOSchedule4Chicks_Sub1Row(boSchedule4Chicks_Sub1Row)

                ' BOSchedule4Chicks_MainRow - Row 1
                boSchedule4Chicks_Sub1Row = Schedule4ChicksDataset.BOSchedule4Chicks_Sub1.NewBOSchedule4Chicks_Sub1Row
                boSchedule4Chicks_Sub1Row.ApplicationId = 1
                boSchedule4Chicks_Sub1Row.Gender = "Male 2"
                boSchedule4Chicks_Sub1Row.IdMarkType = "DEFRA CLOSED RING 0483H"
                boSchedule4Chicks_Sub1Row.Species = "FALCO PEREGRINUS"
                boSchedule4Chicks_Sub1Row.CommonName = "PEREGRINE FALCON"
                boSchedule4Chicks_Sub1Row.RegKeeperYes = "X"
                boSchedule4Chicks_Sub1Row.RegKeeperNo = ""
                Schedule4ChicksDataset.BOSchedule4Chicks_Sub1.AddBOSchedule4Chicks_Sub1Row(boSchedule4Chicks_Sub1Row)

                ' BOSchedule4Chicks_MainRow - Row 2
                boSchedule4Chicks_Sub1Row = Schedule4ChicksDataset.BOSchedule4Chicks_Sub1.NewBOSchedule4Chicks_Sub1Row
                boSchedule4Chicks_Sub1Row.ApplicationId = 1
                boSchedule4Chicks_Sub1Row.Gender = "Male 3"
                boSchedule4Chicks_Sub1Row.IdMarkType = "DEFRA SWISS RING 1G03729"
                boSchedule4Chicks_Sub1Row.Species = "FALCO PEREGRINUS"
                boSchedule4Chicks_Sub1Row.CommonName = "PEREGRINE FALCON"
                boSchedule4Chicks_Sub1Row.RegKeeperYes = "X"
                boSchedule4Chicks_Sub1Row.RegKeeperNo = ""
                Schedule4ChicksDataset.BOSchedule4Chicks_Sub1.AddBOSchedule4Chicks_Sub1Row(boSchedule4Chicks_Sub1Row)

                ' BOSchedule4Chicks_MainRow - Row 3
                boSchedule4Chicks_Sub1Row = Schedule4ChicksDataset.BOSchedule4Chicks_Sub1.NewBOSchedule4Chicks_Sub1Row
                boSchedule4Chicks_Sub1Row.ApplicationId = 1
                boSchedule4Chicks_Sub1Row.Gender = "Male 4"
                boSchedule4Chicks_Sub1Row.IdMarkType = "DEFRA SPLIT RING T1472H"
                boSchedule4Chicks_Sub1Row.Species = "ACCIPTER GENTILLIS"
                boSchedule4Chicks_Sub1Row.CommonName = "GOSHAWK"
                boSchedule4Chicks_Sub1Row.RegKeeperYes = "X"
                boSchedule4Chicks_Sub1Row.RegKeeperNo = ""
                Schedule4ChicksDataset.BOSchedule4Chicks_Sub1.AddBOSchedule4Chicks_Sub1Row(boSchedule4Chicks_Sub1Row)

                ' BOSchedule4Chicks_MainRow - Row 4
                boSchedule4Chicks_Sub1Row = Schedule4ChicksDataset.BOSchedule4Chicks_Sub1.NewBOSchedule4Chicks_Sub1Row
                boSchedule4Chicks_Sub1Row.ApplicationId = 1
                boSchedule4Chicks_Sub1Row.Gender = "Female 1"
                boSchedule4Chicks_Sub1Row.IdMarkType = "DEFRA CLOSED RING 0363H"
                boSchedule4Chicks_Sub1Row.Species = "FALCO PEREGRINUS"
                boSchedule4Chicks_Sub1Row.CommonName = "PEREGRINE FALCON"
                boSchedule4Chicks_Sub1Row.RegKeeperYes = "X"
                boSchedule4Chicks_Sub1Row.RegKeeperNo = ""
                Schedule4ChicksDataset.BOSchedule4Chicks_Sub1.AddBOSchedule4Chicks_Sub1Row(boSchedule4Chicks_Sub1Row)

                ' BOSchedule4Chicks_MainRow - Row 5
                boSchedule4Chicks_Sub1Row = Schedule4ChicksDataset.BOSchedule4Chicks_Sub1.NewBOSchedule4Chicks_Sub1Row
                boSchedule4Chicks_Sub1Row.ApplicationId = 1
                boSchedule4Chicks_Sub1Row.Gender = "Female 2"
                boSchedule4Chicks_Sub1Row.IdMarkType = "DEFRA CLOSED RING 9463H"
                boSchedule4Chicks_Sub1Row.Species = "FALCO PEREGRINUS"
                boSchedule4Chicks_Sub1Row.CommonName = "PEREGRINE FALCON"
                boSchedule4Chicks_Sub1Row.RegKeeperYes = "X"
                boSchedule4Chicks_Sub1Row.RegKeeperNo = ""
                Schedule4ChicksDataset.BOSchedule4Chicks_Sub1.AddBOSchedule4Chicks_Sub1Row(boSchedule4Chicks_Sub1Row)

                ' boSchedule4Chicks_Sub2Row - Bird 1
                boSchedule4Chicks_Sub2Row = Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.NewBOSchedule4Chicks_Sub2Row
                boSchedule4Chicks_Sub2Row.ApplicationId = 1
                boSchedule4Chicks_Sub2Row.BirdNo = "Bird 1"
                boSchedule4Chicks_Sub2Row.ExpectedSpecies = "ACCIPTER GENTILLIS/FALCO PEREGRINUS"
                boSchedule4Chicks_Sub2Row.CommonName = "GOSHAWK/PEREGRINE FALCON"
                boSchedule4Chicks_Sub2Row.KeptAddress = ""
                boSchedule4Chicks_Sub2Row.RingNo1 = "26801H"
                boSchedule4Chicks_Sub2Row.RingNo2 = "31354H"
                boSchedule4Chicks_Sub2Row.RingNo3 = ""
                boSchedule4Chicks_Sub2Row.RingNo4 = ""
                boSchedule4Chicks_Sub2Row.RingNo5 = ""
                boSchedule4Chicks_Sub2Row.RingNo6 = ""
                boSchedule4Chicks_Sub2Row.RingNo7 = ""
                boSchedule4Chicks_Sub2Row.RingNo8 = ""
                boSchedule4Chicks_Sub2Row.RingNo9 = ""
                boSchedule4Chicks_Sub2Row.RingNo10 = ""
                Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.AddBOSchedule4Chicks_Sub2Row(boSchedule4Chicks_Sub2Row)

                ' boSchedule4Chicks_Sub2Row - Bird 2
                boSchedule4Chicks_Sub2Row = Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.NewBOSchedule4Chicks_Sub2Row
                boSchedule4Chicks_Sub2Row.ApplicationId = 1
                boSchedule4Chicks_Sub2Row.BirdNo = "Bird 2"
                boSchedule4Chicks_Sub2Row.ExpectedSpecies = "ACCIPTER GENTILLIS/FALCO PEREGRINUS"
                boSchedule4Chicks_Sub2Row.CommonName = "GOSHAWK/PEREGRINE FALCON"
                boSchedule4Chicks_Sub2Row.KeptAddress = ""
                boSchedule4Chicks_Sub2Row.RingNo1 = "35488H"
                boSchedule4Chicks_Sub2Row.RingNo2 = "11458H"
                boSchedule4Chicks_Sub2Row.RingNo3 = ""
                boSchedule4Chicks_Sub2Row.RingNo4 = ""
                boSchedule4Chicks_Sub2Row.RingNo5 = ""
                boSchedule4Chicks_Sub2Row.RingNo6 = ""
                boSchedule4Chicks_Sub2Row.RingNo7 = ""
                boSchedule4Chicks_Sub2Row.RingNo8 = ""
                boSchedule4Chicks_Sub2Row.RingNo9 = ""
                boSchedule4Chicks_Sub2Row.RingNo10 = ""
                Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.AddBOSchedule4Chicks_Sub2Row(boSchedule4Chicks_Sub2Row)

                ' boSchedule4Chicks_Sub2Row - Bird 3
                boSchedule4Chicks_Sub2Row = Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.NewBOSchedule4Chicks_Sub2Row
                boSchedule4Chicks_Sub2Row.ApplicationId = 1
                boSchedule4Chicks_Sub2Row.BirdNo = "Bird 3"
                boSchedule4Chicks_Sub2Row.ExpectedSpecies = "ACCIPTER GENTILLIS/FALCO PEREGRINUS"
                boSchedule4Chicks_Sub2Row.CommonName = "GOSHAWK/PEREGRINE FALCON"
                boSchedule4Chicks_Sub2Row.KeptAddress = ""
                boSchedule4Chicks_Sub2Row.RingNo1 = "27801H"
                boSchedule4Chicks_Sub2Row.RingNo2 = "31454H"
                boSchedule4Chicks_Sub2Row.RingNo3 = ""
                boSchedule4Chicks_Sub2Row.RingNo4 = ""
                boSchedule4Chicks_Sub2Row.RingNo5 = ""
                boSchedule4Chicks_Sub2Row.RingNo6 = ""
                boSchedule4Chicks_Sub2Row.RingNo7 = ""
                boSchedule4Chicks_Sub2Row.RingNo8 = ""
                boSchedule4Chicks_Sub2Row.RingNo9 = ""
                boSchedule4Chicks_Sub2Row.RingNo10 = ""
                Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.AddBOSchedule4Chicks_Sub2Row(boSchedule4Chicks_Sub2Row)

                ' boSchedule4Chicks_Sub2Row - Bird 4
                boSchedule4Chicks_Sub2Row = Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.NewBOSchedule4Chicks_Sub2Row
                boSchedule4Chicks_Sub2Row.ApplicationId = 1
                boSchedule4Chicks_Sub2Row.BirdNo = "Bird 4"
                boSchedule4Chicks_Sub2Row.ExpectedSpecies = "ACCIPTER GENTILLIS/FALCO PEREGRINUS"
                boSchedule4Chicks_Sub2Row.CommonName = "GOSHAWK/PEREGRINE FALCON"
                boSchedule4Chicks_Sub2Row.KeptAddress = ""
                boSchedule4Chicks_Sub2Row.RingNo1 = "25498H"
                boSchedule4Chicks_Sub2Row.RingNo2 = "10446H"
                boSchedule4Chicks_Sub2Row.RingNo3 = ""
                boSchedule4Chicks_Sub2Row.RingNo4 = ""
                boSchedule4Chicks_Sub2Row.RingNo5 = ""
                boSchedule4Chicks_Sub2Row.RingNo6 = ""
                boSchedule4Chicks_Sub2Row.RingNo7 = ""
                boSchedule4Chicks_Sub2Row.RingNo8 = ""
                boSchedule4Chicks_Sub2Row.RingNo9 = ""
                boSchedule4Chicks_Sub2Row.RingNo10 = ""
                Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.AddBOSchedule4Chicks_Sub2Row(boSchedule4Chicks_Sub2Row)

                ' boSchedule4Chicks_Sub2Row - Bird 5
                boSchedule4Chicks_Sub2Row = Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.NewBOSchedule4Chicks_Sub2Row
                boSchedule4Chicks_Sub2Row.ApplicationId = 1
                boSchedule4Chicks_Sub2Row.BirdNo = "Bird 5"
                boSchedule4Chicks_Sub2Row.ExpectedSpecies = "ACCIPTER GENTILLIS/FALCO PEREGRINUS"
                boSchedule4Chicks_Sub2Row.CommonName = "GOSHAWK/PEREGRINE FALCON"
                boSchedule4Chicks_Sub2Row.KeptAddress = ""
                boSchedule4Chicks_Sub2Row.RingNo1 = "35398H"
                boSchedule4Chicks_Sub2Row.RingNo2 = "11236H"
                boSchedule4Chicks_Sub2Row.RingNo3 = ""
                boSchedule4Chicks_Sub2Row.RingNo4 = ""
                boSchedule4Chicks_Sub2Row.RingNo5 = ""
                boSchedule4Chicks_Sub2Row.RingNo6 = ""
                boSchedule4Chicks_Sub2Row.RingNo7 = ""
                boSchedule4Chicks_Sub2Row.RingNo8 = ""
                boSchedule4Chicks_Sub2Row.RingNo9 = ""
                boSchedule4Chicks_Sub2Row.RingNo10 = ""
                Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.AddBOSchedule4Chicks_Sub2Row(boSchedule4Chicks_Sub2Row)

                ' boSchedule4Chicks_Sub2Row - Bird 6
                boSchedule4Chicks_Sub2Row = Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.NewBOSchedule4Chicks_Sub2Row
                boSchedule4Chicks_Sub2Row.ApplicationId = 1
                boSchedule4Chicks_Sub2Row.BirdNo = "Bird 6"
                boSchedule4Chicks_Sub2Row.ExpectedSpecies = "ACCIPTER GENTILLIS/FALCO PEREGRINUS"
                boSchedule4Chicks_Sub2Row.CommonName = "GOSHAWK/PEREGRINE FALCON"
                boSchedule4Chicks_Sub2Row.KeptAddress = ""
                boSchedule4Chicks_Sub2Row.RingNo1 = "26801H"
                boSchedule4Chicks_Sub2Row.RingNo2 = "31354H"
                boSchedule4Chicks_Sub2Row.RingNo3 = ""
                boSchedule4Chicks_Sub2Row.RingNo4 = ""
                boSchedule4Chicks_Sub2Row.RingNo5 = ""
                boSchedule4Chicks_Sub2Row.RingNo6 = ""
                boSchedule4Chicks_Sub2Row.RingNo7 = ""
                boSchedule4Chicks_Sub2Row.RingNo8 = ""
                boSchedule4Chicks_Sub2Row.RingNo9 = ""
                boSchedule4Chicks_Sub2Row.RingNo10 = ""
                Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.AddBOSchedule4Chicks_Sub2Row(boSchedule4Chicks_Sub2Row)

                ' boSchedule4Chicks_Sub2Row - Bird 7
                boSchedule4Chicks_Sub2Row = Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.NewBOSchedule4Chicks_Sub2Row
                boSchedule4Chicks_Sub2Row.ApplicationId = 1
                boSchedule4Chicks_Sub2Row.BirdNo = "Bird 7"
                boSchedule4Chicks_Sub2Row.ExpectedSpecies = "ACCIPTER GENTILLIS/FALCO PEREGRINUS"
                boSchedule4Chicks_Sub2Row.CommonName = "GOSHAWK/PEREGRINE FALCON"
                boSchedule4Chicks_Sub2Row.KeptAddress = ""
                boSchedule4Chicks_Sub2Row.RingNo1 = "35488H"
                boSchedule4Chicks_Sub2Row.RingNo2 = "11458H"
                boSchedule4Chicks_Sub2Row.RingNo3 = ""
                boSchedule4Chicks_Sub2Row.RingNo4 = ""
                boSchedule4Chicks_Sub2Row.RingNo5 = ""
                boSchedule4Chicks_Sub2Row.RingNo6 = ""
                boSchedule4Chicks_Sub2Row.RingNo7 = ""
                boSchedule4Chicks_Sub2Row.RingNo8 = ""
                boSchedule4Chicks_Sub2Row.RingNo9 = ""
                boSchedule4Chicks_Sub2Row.RingNo10 = ""
                Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.AddBOSchedule4Chicks_Sub2Row(boSchedule4Chicks_Sub2Row)

                ' boSchedule4Chicks_Sub2Row - Bird 8
                boSchedule4Chicks_Sub2Row = Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.NewBOSchedule4Chicks_Sub2Row
                boSchedule4Chicks_Sub2Row.ApplicationId = 1
                boSchedule4Chicks_Sub2Row.BirdNo = "Bird 8"
                boSchedule4Chicks_Sub2Row.ExpectedSpecies = "ACCIPTER GENTILLIS/FALCO PEREGRINUS"
                boSchedule4Chicks_Sub2Row.CommonName = "GOSHAWK/PEREGRINE FALCON"
                boSchedule4Chicks_Sub2Row.KeptAddress = ""
                boSchedule4Chicks_Sub2Row.RingNo1 = "27801H"
                boSchedule4Chicks_Sub2Row.RingNo2 = "31454H"
                boSchedule4Chicks_Sub2Row.RingNo3 = ""
                boSchedule4Chicks_Sub2Row.RingNo4 = ""
                boSchedule4Chicks_Sub2Row.RingNo5 = ""
                boSchedule4Chicks_Sub2Row.RingNo6 = ""
                boSchedule4Chicks_Sub2Row.RingNo7 = ""
                boSchedule4Chicks_Sub2Row.RingNo8 = ""
                boSchedule4Chicks_Sub2Row.RingNo9 = ""
                boSchedule4Chicks_Sub2Row.RingNo10 = ""
                Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.AddBOSchedule4Chicks_Sub2Row(boSchedule4Chicks_Sub2Row)

                ' boSchedule4Chicks_Sub2Row - Bird 9
                boSchedule4Chicks_Sub2Row = Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.NewBOSchedule4Chicks_Sub2Row
                boSchedule4Chicks_Sub2Row.ApplicationId = 1
                boSchedule4Chicks_Sub2Row.BirdNo = "Bird 9"
                boSchedule4Chicks_Sub2Row.ExpectedSpecies = "ACCIPTER GENTILLIS/FALCO PEREGRINUS"
                boSchedule4Chicks_Sub2Row.CommonName = "GOSHAWK/PEREGRINE FALCON"
                boSchedule4Chicks_Sub2Row.KeptAddress = ""
                boSchedule4Chicks_Sub2Row.RingNo1 = "25498H"
                boSchedule4Chicks_Sub2Row.RingNo2 = "10446H"
                boSchedule4Chicks_Sub2Row.RingNo3 = ""
                boSchedule4Chicks_Sub2Row.RingNo4 = ""
                boSchedule4Chicks_Sub2Row.RingNo5 = ""
                boSchedule4Chicks_Sub2Row.RingNo6 = ""
                boSchedule4Chicks_Sub2Row.RingNo7 = ""
                boSchedule4Chicks_Sub2Row.RingNo8 = ""
                boSchedule4Chicks_Sub2Row.RingNo9 = ""
                boSchedule4Chicks_Sub2Row.RingNo10 = ""
                Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.AddBOSchedule4Chicks_Sub2Row(boSchedule4Chicks_Sub2Row)

                ' boSchedule4Chicks_Sub2Row - Bird 10
                boSchedule4Chicks_Sub2Row = Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.NewBOSchedule4Chicks_Sub2Row
                boSchedule4Chicks_Sub2Row.ApplicationId = 1
                boSchedule4Chicks_Sub2Row.BirdNo = "Bird 10"
                boSchedule4Chicks_Sub2Row.ExpectedSpecies = "ACCIPTER GENTILLIS/FALCO PEREGRINUS"
                boSchedule4Chicks_Sub2Row.CommonName = "GOSHAWK/PEREGRINE FALCON"
                boSchedule4Chicks_Sub2Row.KeptAddress = ""
                boSchedule4Chicks_Sub2Row.RingNo1 = "35398H"
                boSchedule4Chicks_Sub2Row.RingNo2 = "11236H"
                boSchedule4Chicks_Sub2Row.RingNo3 = ""
                boSchedule4Chicks_Sub2Row.RingNo4 = ""
                boSchedule4Chicks_Sub2Row.RingNo5 = ""
                boSchedule4Chicks_Sub2Row.RingNo6 = ""
                boSchedule4Chicks_Sub2Row.RingNo7 = ""
                boSchedule4Chicks_Sub2Row.RingNo8 = ""
                boSchedule4Chicks_Sub2Row.RingNo9 = ""
                boSchedule4Chicks_Sub2Row.RingNo10 = ""
                Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.AddBOSchedule4Chicks_Sub2Row(boSchedule4Chicks_Sub2Row)

                ' boSchedule4Chicks_Sub2Row - Bird 11
                boSchedule4Chicks_Sub2Row = Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.NewBOSchedule4Chicks_Sub2Row
                boSchedule4Chicks_Sub2Row.ApplicationId = 1
                boSchedule4Chicks_Sub2Row.BirdNo = "Bird 11"
                boSchedule4Chicks_Sub2Row.ExpectedSpecies = "ACCIPTER GENTILLIS/FALCO PEREGRINUS"
                boSchedule4Chicks_Sub2Row.CommonName = "GOSHAWK/PEREGRINE FALCON"
                boSchedule4Chicks_Sub2Row.KeptAddress = ""
                boSchedule4Chicks_Sub2Row.RingNo1 = "36399H"
                boSchedule4Chicks_Sub2Row.RingNo2 = "11546H"
                boSchedule4Chicks_Sub2Row.RingNo3 = ""
                boSchedule4Chicks_Sub2Row.RingNo4 = ""
                boSchedule4Chicks_Sub2Row.RingNo5 = ""
                boSchedule4Chicks_Sub2Row.RingNo6 = ""
                boSchedule4Chicks_Sub2Row.RingNo7 = ""
                boSchedule4Chicks_Sub2Row.RingNo8 = ""
                boSchedule4Chicks_Sub2Row.RingNo9 = ""
                boSchedule4Chicks_Sub2Row.RingNo10 = ""
                Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.AddBOSchedule4Chicks_Sub2Row(boSchedule4Chicks_Sub2Row)

                ' boSchedule4Chicks_Sub2Row - Bird 12
                boSchedule4Chicks_Sub2Row = Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.NewBOSchedule4Chicks_Sub2Row
                boSchedule4Chicks_Sub2Row.ApplicationId = 1
                boSchedule4Chicks_Sub2Row.BirdNo = "Bird 12"
                boSchedule4Chicks_Sub2Row.ExpectedSpecies = "ACCIPTER GENTILLIS/FALCO PEREGRINUS"
                boSchedule4Chicks_Sub2Row.CommonName = "GOSHAWK/PEREGRINE FALCON"
                boSchedule4Chicks_Sub2Row.KeptAddress = ""
                boSchedule4Chicks_Sub2Row.RingNo1 = "50843H"
                boSchedule4Chicks_Sub2Row.RingNo2 = "77891H"
                boSchedule4Chicks_Sub2Row.RingNo3 = ""
                boSchedule4Chicks_Sub2Row.RingNo4 = ""
                boSchedule4Chicks_Sub2Row.RingNo5 = ""
                boSchedule4Chicks_Sub2Row.RingNo6 = ""
                boSchedule4Chicks_Sub2Row.RingNo7 = ""
                boSchedule4Chicks_Sub2Row.RingNo8 = ""
                boSchedule4Chicks_Sub2Row.RingNo9 = ""
                boSchedule4Chicks_Sub2Row.RingNo10 = ""
                Schedule4ChicksDataset.BOSchedule4Chicks_Sub2.AddBOSchedule4Chicks_Sub2Row(boSchedule4Chicks_Sub2Row)

                Schedule4ChicksDataset.AcceptChanges()

                Dim Schedule4ChicksDatasets(0) As Schedule4ChicksData
                Schedule4ChicksDatasets(0) = Schedule4ChicksDataset

                Return Schedule4ChicksDatasets

            Catch lo_Exception As Exception
                Dim ls As String = lo_Exception.Message
            End Try

        End Function
    End Class

End Namespace
Imports System
Imports System.data
Imports System.Collections
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Namespace RPT

    Public Class ReferralServiceLevel
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim reportResults(0) As BOReportResults

            Dim referralServiceLevelCriteria As ReportCriteria.ReferralServiceLevelCriteria = CType(reportCriteria, ReportCriteria.ReferralServiceLevelCriteria)

            Dim reportDatasets() As DataSet
            reportDatasets = GetReferralServiceLevelCriteriaDatasets(referralServiceLevelCriteria)

            Dim ReferralServiceLevel_RPT As ReferralServiceLevel_RPT

            Dim parameterValues As New Hashtable
            parameterValues.Add("FromDate", referralServiceLevelCriteria.FromDate.ToString("dd/MM/yyyy"))
            parameterValues.Add("ToDate", referralServiceLevelCriteria.ToDate.ToString("dd/MM/yyyy"))

            Dim idx As Int32 = -1
            Dim linkId As String = ""
            For Each dataset As DataSet In reportDatasets
                idx += 1
                ReferralServiceLevel_RPT = New ReferralServiceLevel_RPT
                reportResults(idx) = DoReport(reportCriteria.Description, linkId, 0, dataset, referralServiceLevelCriteria, saveReport, _
                ReferralServiceLevel_RPT, reportPrintJobId, printSequence, parameterValues)
            Next

            Return reportResults

        End Function


        Public Function GetReferralServiceLevelCriteriaDatasets(ByVal referralServiceLevelCriteria As ReportCriteria.ReferralServiceLevelCriteria) As ReferralServiceLevelData()

            Dim ReferralServiceLevelDataset As New ReferralServiceLevelData
            Dim boServiceLevelDetailsRow As ReferralServiceLevelData.BOServiceLevelDetailsRow
            Dim boServiceLevelTypesRow As ReferralServiceLevelData.BOServiceLevelTypesRow

            ' Service Level Type - Row 0 - JNCC TOTALS
            ' ----------------------------------------
            boServiceLevelTypesRow = ReferralServiceLevelDataset.BOServiceLevelTypes.NewBOServiceLevelTypesRow
            boServiceLevelTypesRow.Description = "JNCC TOTALS:"
            boServiceLevelTypesRow.ServiceLevelId = 0
            boServiceLevelTypesRow.ThrowNewPage = False
            ReferralServiceLevelDataset.BOServiceLevelTypes.AddBOServiceLevelTypesRow(boServiceLevelTypesRow)
            ' Service Level Details - Row 0
            boServiceLevelDetailsRow = ReferralServiceLevelDataset.BOServiceLevelDetails.NewBOServiceLevelDetailsRow
            boServiceLevelDetailsRow.ServiceLevelId = 0
            boServiceLevelDetailsRow.ReferralType = "All Referrals"
            boServiceLevelDetailsRow.Processed = "70"
            boServiceLevelDetailsRow.WithinFiveDays = "68"
            boServiceLevelDetailsRow.Between6and10Days = "1"
            boServiceLevelDetailsRow.Over10Days = "1"
            ReferralServiceLevelDataset.BOServiceLevelDetails.AddBOServiceLevelDetailsRow(boServiceLevelDetailsRow)
            ' Service Level Details - Row 1
            boServiceLevelDetailsRow = ReferralServiceLevelDataset.BOServiceLevelDetails.NewBOServiceLevelDetailsRow
            boServiceLevelDetailsRow.ServiceLevelId = 0
            boServiceLevelDetailsRow.ReferralType = "Last Referral"
            boServiceLevelDetailsRow.Processed = ""
            boServiceLevelDetailsRow.WithinFiveDays = "70"
            boServiceLevelDetailsRow.Between6and10Days = "0"
            boServiceLevelDetailsRow.Over10Days = "0"
            ReferralServiceLevelDataset.BOServiceLevelDetails.AddBOServiceLevelDetailsRow(boServiceLevelDetailsRow)
            ' -------
            ' Service Level Type - Row 1 - EXPORT PERMITS
            boServiceLevelTypesRow = ReferralServiceLevelDataset.BOServiceLevelTypes.NewBOServiceLevelTypesRow
            boServiceLevelTypesRow.Description = "EXPORT PERMITS:"
            boServiceLevelTypesRow.ServiceLevelId = 1
            boServiceLevelTypesRow.ThrowNewPage = False
            ReferralServiceLevelDataset.BOServiceLevelTypes.AddBOServiceLevelTypesRow(boServiceLevelTypesRow)
            ' Service Level Details - Row 1
            boServiceLevelDetailsRow = ReferralServiceLevelDataset.BOServiceLevelDetails.NewBOServiceLevelDetailsRow
            boServiceLevelDetailsRow.ServiceLevelId = 1
            boServiceLevelDetailsRow.ReferralType = "All Referrals"
            boServiceLevelDetailsRow.Processed = "52"
            boServiceLevelDetailsRow.WithinFiveDays = "52"
            boServiceLevelDetailsRow.Between6and10Days = "0"
            boServiceLevelDetailsRow.Over10Days = "0"
            ReferralServiceLevelDataset.BOServiceLevelDetails.AddBOServiceLevelDetailsRow(boServiceLevelDetailsRow)
            ' Service Level Details - Row 1
            boServiceLevelDetailsRow = ReferralServiceLevelDataset.BOServiceLevelDetails.NewBOServiceLevelDetailsRow
            boServiceLevelDetailsRow.ServiceLevelId = 1
            boServiceLevelDetailsRow.ReferralType = "Last Referral"
            boServiceLevelDetailsRow.Processed = ""
            boServiceLevelDetailsRow.WithinFiveDays = "52"
            boServiceLevelDetailsRow.Between6and10Days = "0"
            boServiceLevelDetailsRow.Over10Days = "0"
            ReferralServiceLevelDataset.BOServiceLevelDetails.AddBOServiceLevelDetailsRow(boServiceLevelDetailsRow)
            ' -------
            ' Service Level Type - Row 2 - IMPORT PERMITS
            boServiceLevelTypesRow = ReferralServiceLevelDataset.BOServiceLevelTypes.NewBOServiceLevelTypesRow
            boServiceLevelTypesRow.Description = "IMPORT PERMITS:"
            boServiceLevelTypesRow.ServiceLevelId = 2
            boServiceLevelTypesRow.ThrowNewPage = False
            ReferralServiceLevelDataset.BOServiceLevelTypes.AddBOServiceLevelTypesRow(boServiceLevelTypesRow)
            ' Service Level Details - Row 2
            boServiceLevelDetailsRow = ReferralServiceLevelDataset.BOServiceLevelDetails.NewBOServiceLevelDetailsRow
            boServiceLevelDetailsRow.ServiceLevelId = 2
            boServiceLevelDetailsRow.ReferralType = "All Referrals"
            boServiceLevelDetailsRow.Processed = "18"
            boServiceLevelDetailsRow.WithinFiveDays = "16"
            boServiceLevelDetailsRow.Between6and10Days = "1"
            boServiceLevelDetailsRow.Over10Days = "1"
            ReferralServiceLevelDataset.BOServiceLevelDetails.AddBOServiceLevelDetailsRow(boServiceLevelDetailsRow)
            ' Service Level Details - Row 2
            boServiceLevelDetailsRow = ReferralServiceLevelDataset.BOServiceLevelDetails.NewBOServiceLevelDetailsRow
            boServiceLevelDetailsRow.ServiceLevelId = 2
            boServiceLevelDetailsRow.ReferralType = "Last Referral"
            boServiceLevelDetailsRow.Processed = ""
            boServiceLevelDetailsRow.WithinFiveDays = "18"
            boServiceLevelDetailsRow.Between6and10Days = "0"
            boServiceLevelDetailsRow.Over10Days = "0"
            ReferralServiceLevelDataset.BOServiceLevelDetails.AddBOServiceLevelDetailsRow(boServiceLevelDetailsRow)
            ' -------
            ' Service Level Type - Row 3 - ARTICLE 10
            boServiceLevelTypesRow = ReferralServiceLevelDataset.BOServiceLevelTypes.NewBOServiceLevelTypesRow
            boServiceLevelTypesRow.Description = "ARTICLE 10:"
            boServiceLevelTypesRow.ServiceLevelId = 3
            boServiceLevelTypesRow.ThrowNewPage = False
            ReferralServiceLevelDataset.BOServiceLevelTypes.AddBOServiceLevelTypesRow(boServiceLevelTypesRow)
            ' Service Level Details - Row 3
            boServiceLevelDetailsRow = ReferralServiceLevelDataset.BOServiceLevelDetails.NewBOServiceLevelDetailsRow
            boServiceLevelDetailsRow.ServiceLevelId = 3
            boServiceLevelDetailsRow.ReferralType = "All Referrals"
            boServiceLevelDetailsRow.Processed = "0"
            boServiceLevelDetailsRow.WithinFiveDays = "0"
            boServiceLevelDetailsRow.Between6and10Days = "0"
            boServiceLevelDetailsRow.Over10Days = "1"
            ReferralServiceLevelDataset.BOServiceLevelDetails.AddBOServiceLevelDetailsRow(boServiceLevelDetailsRow)
            ' Service Level Details - Row 3
            boServiceLevelDetailsRow = ReferralServiceLevelDataset.BOServiceLevelDetails.NewBOServiceLevelDetailsRow
            boServiceLevelDetailsRow.ServiceLevelId = 3
            boServiceLevelDetailsRow.ReferralType = "Last Referral"
            boServiceLevelDetailsRow.Processed = ""
            boServiceLevelDetailsRow.WithinFiveDays = "0"
            boServiceLevelDetailsRow.Between6and10Days = "0"
            boServiceLevelDetailsRow.Over10Days = "0"
            ReferralServiceLevelDataset.BOServiceLevelDetails.AddBOServiceLevelDetailsRow(boServiceLevelDetailsRow)


            ' Service Level Type - Row 4 - KEW TOTALS
            ' ---------------------------------------
            boServiceLevelTypesRow = ReferralServiceLevelDataset.BOServiceLevelTypes.NewBOServiceLevelTypesRow
            boServiceLevelTypesRow.Description = "KEW TOTALS:"
            boServiceLevelTypesRow.ServiceLevelId = 4
            boServiceLevelTypesRow.ThrowNewPage = True
            ReferralServiceLevelDataset.BOServiceLevelTypes.AddBOServiceLevelTypesRow(boServiceLevelTypesRow)
            ' Service Level Details - Row 4
            boServiceLevelDetailsRow = ReferralServiceLevelDataset.BOServiceLevelDetails.NewBOServiceLevelDetailsRow
            boServiceLevelDetailsRow.ServiceLevelId = 4
            boServiceLevelDetailsRow.ReferralType = "All Referrals"
            boServiceLevelDetailsRow.Processed = "56"
            boServiceLevelDetailsRow.WithinFiveDays = "50"
            boServiceLevelDetailsRow.Between6and10Days = "5"
            boServiceLevelDetailsRow.Over10Days = "1"
            ReferralServiceLevelDataset.BOServiceLevelDetails.AddBOServiceLevelDetailsRow(boServiceLevelDetailsRow)
            ' Service Level Details - Row 4
            boServiceLevelDetailsRow = ReferralServiceLevelDataset.BOServiceLevelDetails.NewBOServiceLevelDetailsRow
            boServiceLevelDetailsRow.ServiceLevelId = 4
            boServiceLevelDetailsRow.ReferralType = "Last Referral"
            boServiceLevelDetailsRow.Processed = ""
            boServiceLevelDetailsRow.WithinFiveDays = "55"
            boServiceLevelDetailsRow.Between6and10Days = "1"
            boServiceLevelDetailsRow.Over10Days = "0"
            ReferralServiceLevelDataset.BOServiceLevelDetails.AddBOServiceLevelDetailsRow(boServiceLevelDetailsRow)
            ' -------
            ' Service Level Type - Row 5 - EXPORT PERMITS
            boServiceLevelTypesRow = ReferralServiceLevelDataset.BOServiceLevelTypes.NewBOServiceLevelTypesRow
            boServiceLevelTypesRow.Description = "EXPORT PERMITS:"
            boServiceLevelTypesRow.ServiceLevelId = 5
            boServiceLevelTypesRow.ThrowNewPage = False
            ReferralServiceLevelDataset.BOServiceLevelTypes.AddBOServiceLevelTypesRow(boServiceLevelTypesRow)
            ' Service Level Details - Row 5
            boServiceLevelDetailsRow = ReferralServiceLevelDataset.BOServiceLevelDetails.NewBOServiceLevelDetailsRow
            boServiceLevelDetailsRow.ServiceLevelId = 5
            boServiceLevelDetailsRow.ReferralType = "All Referrals"
            boServiceLevelDetailsRow.Processed = "32"
            boServiceLevelDetailsRow.WithinFiveDays = "33"
            boServiceLevelDetailsRow.Between6and10Days = "0"
            boServiceLevelDetailsRow.Over10Days = "1"
            ReferralServiceLevelDataset.BOServiceLevelDetails.AddBOServiceLevelDetailsRow(boServiceLevelDetailsRow)
            ' Service Level Details - Row 5
            boServiceLevelDetailsRow = ReferralServiceLevelDataset.BOServiceLevelDetails.NewBOServiceLevelDetailsRow
            boServiceLevelDetailsRow.ServiceLevelId = 5
            boServiceLevelDetailsRow.ReferralType = "Last Referral"
            boServiceLevelDetailsRow.Processed = ""
            boServiceLevelDetailsRow.WithinFiveDays = "33"
            boServiceLevelDetailsRow.Between6and10Days = "0"
            boServiceLevelDetailsRow.Over10Days = "0"
            ReferralServiceLevelDataset.BOServiceLevelDetails.AddBOServiceLevelDetailsRow(boServiceLevelDetailsRow)
            ' -------
            ' Service Level Type - Row 6 - IMPORT PERMITS
            boServiceLevelTypesRow = ReferralServiceLevelDataset.BOServiceLevelTypes.NewBOServiceLevelTypesRow
            boServiceLevelTypesRow.Description = "IMPORT PERMITS:"
            boServiceLevelTypesRow.ServiceLevelId = 6
            boServiceLevelTypesRow.ThrowNewPage = False
            ReferralServiceLevelDataset.BOServiceLevelTypes.AddBOServiceLevelTypesRow(boServiceLevelTypesRow)
            ' Service Level Details - Row 6
            boServiceLevelDetailsRow = ReferralServiceLevelDataset.BOServiceLevelDetails.NewBOServiceLevelDetailsRow
            boServiceLevelDetailsRow.ServiceLevelId = 6
            boServiceLevelDetailsRow.ReferralType = "All Referrals"
            boServiceLevelDetailsRow.Processed = "18"
            boServiceLevelDetailsRow.WithinFiveDays = "16"
            boServiceLevelDetailsRow.Between6and10Days = "2"
            boServiceLevelDetailsRow.Over10Days = "0"
            ReferralServiceLevelDataset.BOServiceLevelDetails.AddBOServiceLevelDetailsRow(boServiceLevelDetailsRow)
            ' Service Level Details - Row 6
            boServiceLevelDetailsRow = ReferralServiceLevelDataset.BOServiceLevelDetails.NewBOServiceLevelDetailsRow
            boServiceLevelDetailsRow.ServiceLevelId = 6
            boServiceLevelDetailsRow.ReferralType = "Last Referral"
            boServiceLevelDetailsRow.Processed = ""
            boServiceLevelDetailsRow.WithinFiveDays = "18"
            boServiceLevelDetailsRow.Between6and10Days = "0"
            boServiceLevelDetailsRow.Over10Days = "0"
            ReferralServiceLevelDataset.BOServiceLevelDetails.AddBOServiceLevelDetailsRow(boServiceLevelDetailsRow)
            ' -------
            ' Service Level Type - Row 7 - ARTICLE 10
            boServiceLevelTypesRow = ReferralServiceLevelDataset.BOServiceLevelTypes.NewBOServiceLevelTypesRow
            boServiceLevelTypesRow.Description = "ARTICLE 10:"
            boServiceLevelTypesRow.ServiceLevelId = 7
            boServiceLevelTypesRow.ThrowNewPage = False
            ReferralServiceLevelDataset.BOServiceLevelTypes.AddBOServiceLevelTypesRow(boServiceLevelTypesRow)
            ' Service Level Details - Row 7
            boServiceLevelDetailsRow = ReferralServiceLevelDataset.BOServiceLevelDetails.NewBOServiceLevelDetailsRow
            boServiceLevelDetailsRow.ServiceLevelId = 7
            boServiceLevelDetailsRow.ReferralType = "All Referrals"
            boServiceLevelDetailsRow.Processed = "5"
            boServiceLevelDetailsRow.WithinFiveDays = "2"
            boServiceLevelDetailsRow.Between6and10Days = "3"
            boServiceLevelDetailsRow.Over10Days = "0"
            ReferralServiceLevelDataset.BOServiceLevelDetails.AddBOServiceLevelDetailsRow(boServiceLevelDetailsRow)
            ' Service Level Details - Row 3
            boServiceLevelDetailsRow = ReferralServiceLevelDataset.BOServiceLevelDetails.NewBOServiceLevelDetailsRow
            boServiceLevelDetailsRow.ServiceLevelId = 7
            boServiceLevelDetailsRow.ReferralType = "Last Referral"
            boServiceLevelDetailsRow.Processed = ""
            boServiceLevelDetailsRow.WithinFiveDays = "4"
            boServiceLevelDetailsRow.Between6and10Days = "1"
            boServiceLevelDetailsRow.Over10Days = "0"
            ReferralServiceLevelDataset.BOServiceLevelDetails.AddBOServiceLevelDetailsRow(boServiceLevelDetailsRow)


            ReferralServiceLevelDataset.AcceptChanges()

            Dim ReferralServiceLevelDatasets(0) As ReferralServiceLevelData
            ReferralServiceLevelDatasets(0) = ReferralServiceLevelDataset

            Return ReferralServiceLevelDatasets

        End Function

    End Class

End Namespace
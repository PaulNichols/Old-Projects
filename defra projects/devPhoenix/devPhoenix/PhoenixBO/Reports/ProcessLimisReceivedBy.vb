Imports System
Imports System.data
Imports System.Collections
Imports uk.gov.defra.Phoenix.DO
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.BO.ReportLimis
Imports uk.gov.defra.Phoenix.BO.Application
Imports uk.gov.defra.Phoenix.PhoenixReport

Namespace RPT

    Public Class LimisReceivedBy
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim reportResults(0) As BOReportResults

            Dim limisReceivedByReportCriteria As ReportCriteria.LimisReceivedByReportCriteria = CType(reportCriteria, ReportCriteria.LimisReceivedByReportCriteria)

            Dim reportDatasets() As DataSet
            reportDatasets = GetLimisReceivedByReportDatasets(limisReceivedByReportCriteria)

            Dim parameterValues As New Hashtable
            Dim dateTimeFormatInfo As New System.Globalization.DateTimeFormatInfo

            Dim monthYear As String = "Totals sheet for " _
            & dateTimeFormatInfo.GetMonthName(limisReceivedByReportCriteria.Month) _
            & " " & limisReceivedByReportCriteria.Year.ToString
            parameterValues.Add("MonthYear", monthYear)

            Dim LimisReceivedBy_RPT As LimisReceivedBy_RPT

            Dim idx As Int32 = -1
            Dim linkId As String = limisReceivedByReportCriteria.Year.ToString.PadLeft(2, CType("0", Char)) & limisReceivedByReportCriteria.Month.ToString.PadLeft(2)
            For Each dataset As dataset In reportDatasets
                idx += 1
                LimisReceivedBy_RPT = New LimisReceivedBy_RPT
                reportResults(idx) = DoReport(reportCriteria.Description, linkId, 0, dataset, limisReceivedByReportCriteria, saveReport, _
                LimisReceivedBy_RPT, reportPrintJobId, printSequence, parameterValues)
            Next

            Return reportResults

        End Function

        Private Sub AddRow(ByRef dataSet As LimisReceivedByData, ByVal id As Int32, ByVal heading As Boolean, ByVal desc As String, ByVal total() As Int32)
            Dim row As LimisReceivedByData.BOAppTypeDetailRow = dataSet.BOAppTypeDetail.NewBOAppTypeDetailRow()
            row.AppTypeId = id
            row.Description = desc
            row.InternetApps = total(BOLimisMethod.MethodType.Internet).ToString()
            row.TelephoneApps = total(BOLimisMethod.MethodType.Phone).ToString()
            row.PostalApps = total(BOLimisMethod.MethodType.Post).ToString()
            row.EMailApps = total(BOLimisMethod.MethodType.Email).ToString()
            row.FaxApps = total(BOLimisMethod.MethodType.Fax).ToString()
            row.PageHeading = False
            row.TotalHeading = heading
            dataSet.BOAppTypeDetail.AddBOAppTypeDetailRow(row)
        End Sub

        Private Function FindData(ByVal totals() As BOLimisMethod, ByVal appType As Int32) As Int32()
            Dim results(7) As Int32
            For Each total As BOLimisMethod In totals
                If total.ApplicationTypeId = appType Then
                    results(total.Method) = total.Total
                End If
            Next
            Return results
        End Function

        Private Function Accumulate(ByVal ParamArray args()() As Int32) As Int32()
            Dim length As Int32 = args(0).Length
            Dim result(length) As Int32
            For Each item() As Int32 In args
                For i As Int32 = 0 To length - 1
                    result(i) += item(i)
                Next
            Next
            Return result
        End Function


        Public Function GetLimisReceivedByReportDatasets(ByVal criterion As ReportCriteria.LimisReceivedByReportCriteria) As LimisReceivedByData()
            Dim dataset As New LimisReceivedByData
            Dim totals() As BOLimisMethod = BOLimisMethod.GetByMonth(criterion.Month, criterion.Year)
            Dim birdAdds() As Int32 = FindData(totals, ApplicationTypes.BirdAdd)
            Dim birdDups() As Int32 = FindData(totals, ApplicationTypes.BirdDup)
            Dim birdFates() As Int32 = FindData(totals, ApplicationTypes.BirdFate)
            Dim birdAdults() As Int32 = FindData(totals, ApplicationTypes.BirdAdult)
            Dim birdChicks() As Int32 = FindData(totals, ApplicationTypes.BirdChick)
            Dim birdTrans() As Int32 = FindData(totals, ApplicationTypes.BirdTrans)
            Dim citesExports() As Int32 = FindData(totals, ApplicationTypes.Export)
            Dim citesDups() As Int32 = FindData(totals, ApplicationTypes.CitesDup)
            Dim citesImports() As Int32 = FindData(totals, ApplicationTypes.Import)
            Dim article10s() As Int32 = FindData(totals, ApplicationTypes.Article10)
            Dim allCites() As Int32 = Accumulate(citesImports, citesExports, citesDups)
            Dim allAdultBirds() As Int32 = Accumulate(birdAdds, birdDups, birdFates, birdAdults, birdTrans)
            Dim row As LimisReceivedByData.BOAppTypeDetailRow

            ' Create Heading
            row = dataset.BOAppTypeDetail.NewBOAppTypeDetailRow()
            row.AppTypeId = 0
            row.Description = "Application Type"
            row.InternetApps = "Internet Apps."
            row.TelephoneApps = "Telephone Apps."
            row.PostalApps = "Postal Apps."
            row.EMailApps = "E-Mail Apps."
            row.FaxApps = "Fax Apps."
            row.PageHeading = True
            row.TotalHeading = False
            dataset.BOAppTypeDetail.AddBOAppTypeDetailRow(row)

            AddRow(dataset, 1, False, "ARTICLE 10s", article10s)
            AddRow(dataset, 2, False, "CITES Permits", allCites)
            AddRow(dataset, 3, False, "Bird Reg. - Chicks", birdChicks)
            AddRow(dataset, 4, False, "Bird Reg. - Adult", allAdultBirds)
            AddRow(dataset, 6, True, "TOTALS", Accumulate(article10s, allCites, birdChicks, allAdultBirds))
            dataset.AcceptChanges()

            Dim LimisReceivedByDatasets(0) As LimisReceivedByData
            LimisReceivedByDatasets(0) = dataset
            Return LimisReceivedByDatasets
        End Function

    End Class

End Namespace
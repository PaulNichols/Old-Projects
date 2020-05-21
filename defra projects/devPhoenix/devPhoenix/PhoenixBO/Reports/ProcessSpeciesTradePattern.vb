Imports System
Imports System.data
'Imports CrystalDecisions.Shared
'Imports CrystalDecisions.CrystalReports.Engine
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do
Imports uk.gov.defra.Phoenix.DO.DataObjects
Imports uk.gov.defra.Phoenix.PhoenixReport

Namespace RPT

    Public Class SpeciesTradePattern
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub


        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim speciesTradePatternCriteria As reportCriteria.SpeciesTradePatternCriteria = CType(reportCriteria, reportCriteria.SpeciesTradePatternCriteria)
            Dim speciesTradePatternDataset As New SpeciesTradePatternData

            Dim reportDataResults As reportDataResults = GetTestData(speciesTradePatternCriteria.FromDate, _
            speciesTradePatternCriteria.ToDate, speciesTradePatternCriteria.KingdomID, speciesTradePatternCriteria.TaxonId, _
            speciesTradePatternCriteria.TaxonTypeID, speciesTradePatternCriteria.DerivativeId, _
            speciesTradePatternCriteria.SourceId, speciesTradePatternCriteria.PurposeId, _
            speciesTradePatternCriteria.CountryOfOriginId, speciesTradePatternDataset.GetXmlSchema)

            'Dim reportDataResults As ReportDataResults = GetLiveData(speciesTradePatternCriteria.FromDate, speciesTradePatternCriteria.ToDate, serviceLevelsReferralDataset.GetXmlSchema)
            Dim ReportDataset As DataSet = reportDataResults.ReportData

            Dim speciesTradePattern_RPT As New speciesTradePattern_RPT
            Dim fromToMonthYear As String
            fromToMonthYear = "From " & speciesTradePatternCriteria.FromDate.ToString("MMMM") & " " & speciesTradePatternCriteria.FromDate.ToString("yyyy") _
            & " To " & speciesTradePatternCriteria.ToDate.ToString("MMMM") & " " & speciesTradePatternCriteria.ToDate.ToString("yyyy")
            PhoenixReport.Common.SetParameterValue(speciesTradePattern_RPT, "FromToMonthYear", fromToMonthYear)
            PhoenixReport.Common.SetParameterValue(speciesTradePattern_RPT, "Family", speciesTradePatternCriteria.FamilyDesciption)
            PhoenixReport.Common.SetParameterValue(speciesTradePattern_RPT, "Genus", speciesTradePatternCriteria.GenusDesciption)
            PhoenixReport.Common.SetParameterValue(speciesTradePattern_RPT, "Species", speciesTradePatternCriteria.SpeciesDesciption)

            Dim reportResults(0) As BOReportResults
            reportResults(0) = DoReport(reportCriteria.Description, reportDataResults.SearchReference, -1, reportDataResults.ReportData, speciesTradePatternCriteria, saveReport, _
                speciesTradePattern_RPT, reportPrintJobId, printSequence)

            Return reportResults
        End Function

        Private Shared Function GetTestData(ByVal fromDate As Date, _
        ByVal toDate As Date, _
        ByVal kingdomId As Int32, ByVal taxonId As Int32, _
        ByVal taxonTypeId As Int32, ByVal derivativeId As Int32, _
        ByVal sourceId As Int32, ByVal purposeId As Int32, _
        ByVal countryOfOriginId As Int32, ByVal schema As String) As ReportDataResults

            'get the DS ready
            Dim ReturnDS As New DataSet
            'create a stream to put the info into
            Dim io As New io.StringReader(schema)

            'set-up the DS schema
            ReturnDS.ReadXmlSchema(io)
            'tidy ...
            io.Close()
            io = Nothing

            'create a new row using the ds schema

            Dim NewRow As DataRow

            ' Test Data

            ' 1st Test Row
            NewRow = ReturnDS.Tables("BOSpeciesTradePattern").NewRow()
            With NewRow

                .Item("PartDerivative") = "LIV"
                .Item("Source") = "C"
                .Item("Purpose") = "B"
                .Item("CountryOfOrigin") = "GBR"
                .Item("CountryFrom") = "GBR"
                .Item("CountryTo") = "SAU"
                .Item("AppImport") = ""
                .Item("AppExport") = "3"
                .Item("AppSeizure") = ""
                .Item("AppTotal") = "3"
                .Item("TradeImport") = ""
                .Item("TradeExport") = "5"
                .Item("TradeSeizure") = ""
                .Item("TradeTotal") = "5"

            End With
            'add the row to the dataset
            ReturnDS.Tables("BOSpeciesTradePattern").Rows.Add(NewRow)

            ' 2nd Test Row
            NewRow = ReturnDS.Tables("BOSpeciesTradePattern").NewRow()
            With NewRow

                .Item("PartDerivative") = "LIV"
                .Item("Source") = "C"
                .Item("Purpose") = "B"
                .Item("CountryOfOrigin") = "KAZ"
                .Item("CountryFrom") = "GBR"
                .Item("CountryTo") = "SAU"
                .Item("AppImport") = ""
                .Item("AppExport") = "1"
                .Item("AppSeizure") = ""
                .Item("AppTotal") = "1"
                .Item("TradeImport") = ""
                .Item("TradeExport") = "3"
                .Item("TradeSeizure") = ""
                .Item("TradeTotal") = "3"

            End With
            'add the row to the dataset
            ReturnDS.Tables("BOSpeciesTradePattern").Rows.Add(NewRow)

            ' 3rd Test Row
            NewRow = ReturnDS.Tables("BOSpeciesTradePattern").NewRow()
            With NewRow

                .Item("PartDerivative") = "LIV"
                .Item("Source") = "C"
                .Item("Purpose") = "B"
                .Item("CountryOfOrigin") = "KAZ"
                .Item("CountryFrom") = "ARE"
                .Item("CountryTo") = "GBR"
                .Item("AppImport") = "2"
                .Item("AppExport") = ""
                .Item("AppSeizure") = "1"
                .Item("AppTotal") = "3"
                .Item("TradeImport") = "4"
                .Item("TradeExport") = ""
                .Item("TradeSeizure") = "1"
                .Item("TradeTotal") = "5"

            End With
            'add the row to the dataset
            ReturnDS.Tables("BOSpeciesTradePattern").Rows.Add(NewRow)

            'return the datset
            Return New ReportDataResults(ReturnDS, "")

        End Function

    End Class
End Namespace

Imports System
Imports System.data
'Imports CrystalDecisions.Shared
'Imports CrystalDecisions.CrystalReports.Engine
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do
Imports uk.gov.defra.Phoenix.DO.DataObjects
Imports uk.gov.defra.Phoenix.PhoenixReport

Namespace RPT

    Public Class ServiceLevelsReferral
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim serviceLevelsReferralCriteria As ReportCriteria.ServiceLevelsReferralCriteria = CType(reportCriteria, ReportCriteria.ServiceLevelsReferralCriteria)
            Dim serviceLevelsReferralDataset As New ServiceLevelsReferralData

            'Dim reportDataResults As ReportDataResults = GetTestData(serviceLevelsReferralCriteria.FromDate, serviceLevelsReferralCriteria.ToDate, serviceLevelsReferralDataset.GetXmlSchema)
            Dim reportDataResults As ReportDataResults = GetLiveData(serviceLevelsReferralCriteria.FromDate, serviceLevelsReferralCriteria.ToDate, serviceLevelsReferralDataset.GetXmlSchema)
            Dim ReportDataset As DataSet = reportDataResults.ReportData

            Dim serviceLevelsReferral_RPT As New serviceLevelsReferral_RPT

            PhoenixReport.Common.SetParameterValue(serviceLevelsReferral_RPT, "FromDate", "FROM " & serviceLevelsReferralCriteria.FromDate.ToShortDateString)
            PhoenixReport.Common.SetParameterValue(serviceLevelsReferral_RPT, "ToDate", "TO " & serviceLevelsReferralCriteria.ToDate.ToShortDateString)

            Dim reportResults(0) As BOReportResults
            reportResults(0) = DoReport(reportCriteria.Description, reportDataResults.SearchReference, -1, reportDataResults.ReportData, serviceLevelsReferralCriteria, saveReport, _
                serviceLevelsReferral_RPT, reportPrintJobId, printSequence)

            Return reportResults
        End Function

        Private Shared Function GetLiveData(ByVal fromDate As Date, ByVal toDate As Date, ByVal schema As String) As ReportDataResults
            Dim returnDS As DataSet = GetSchemaDataSet(schema)
            Dim idset As EntitySet.StatusAssignedToGroupSet = Entity.StatusAssignedToGroup.GetAll()
            Dim import As Entity.ApplicationType = Entity.ApplicationType.GetById(Application.ApplicationTypes.Import)
            Dim export As Entity.ApplicationType = Entity.ApplicationType.GetById(Application.ApplicationTypes.Export)
            Dim arti10 As Entity.ApplicationType = Entity.ApplicationType.GetById(Application.ApplicationTypes.Article10)
            Dim arti30 As Entity.ApplicationType = Entity.ApplicationType.GetById(Application.ApplicationTypes.Article30)
            Dim jnccId As Int32 = GetAuthorityId("JNCC", idset)
            Dim kewId As Int32 = GetAuthorityId("KEW", idset)
            Dim jnccSettings(3) As Settings
            Dim kewSettings(3) As Settings

            jnccSettings(0) = New Settings(export.JNCCService1, export.JNCCService2)
            jnccSettings(1) = New Settings(import.JNCCService1, import.JNCCService2)
            jnccSettings(2) = New Settings(arti10.JNCCService1, arti10.JNCCService2)
            jnccSettings(3) = New Settings(arti30.JNCCService1, arti30.JNCCService2)
            kewSettings(0) = New Settings(export.KewService1, export.KewService2)
            kewSettings(1) = New Settings(import.KewService1, import.KewService2)
            kewSettings(2) = New Settings(arti10.KewService1, arti10.KewService2)
            kewSettings(3) = New Settings(arti30.KewService1, arti30.KewService2)

            ProcessAuthority(returnDS, fromDate, toDate, "JNCC", jnccId, jnccSettings)
            ProcessAuthority(returnDS, fromDate, toDate, "KEW", kewId, kewSettings)
            Return New ReportDataResults(returnDS, "")
        End Function

        Private Shared Function GetAuthorityId(ByVal authority As String, ByVal idset As EntitySet.StatusAssignedToGroupSet) As Int32
            For Each item As Entity.StatusAssignedToGroup In idset
                If item.Description.ToUpper() = authority Then
                    Return item.StatusAssignedToGroupId
                End If
            Next
            Throw New Exception("Cannot decode " + authority)
        End Function

        Private Shared Sub ProcessAuthority(ByRef returnDS As DataSet, ByVal fromDate As Date, ByVal toDate As Date, ByVal authority As String, ByVal authorityId As Int32, ByVal info() As Settings)
            Dim row As DataRow = returnDS.Tables("BOServiceLevelsReferral").NewRow()
            Dim data() As Referral = GetReferrals(authorityId, fromDate, toDate)
            Dim totals(6) As Int32
            Dim export() As Int32 = GetCounts(authorityId, data, info(0), totals, Application.ApplicationTypes.Import)
            Dim import() As Int32 = GetCounts(authorityId, data, info(1), totals, Application.ApplicationTypes.Export)
            Dim arti10() As Int32 = GetCounts(authorityId, data, info(2), totals, Application.ApplicationTypes.Article10)
            Dim arti30() As Int32 = GetCounts(authorityId, data, info(3), totals, Application.ApplicationTypes.Article30)

            row.Item("TotalsHeading") = authority + "TOTALS"
            OutputBlock(row, "Totals", totals, GetTotalHeadings(info))
            OutputBlock(row, "Export", export, GetHeadings(info(0)))
            OutputBlock(row, "Import", import, GetHeadings(info(1)))
            OutputBlock(row, "Article10", arti10, GetHeadings(info(2)))
            OutputBlock(row, "Article30", arti30, GetHeadings(info(3)))
            returnDS.Tables("BOServiceLevelsReferral").Rows.Add(row)
        End Sub

        Private Shared Function Slot(ByVal span As TimeSpan, ByVal info As Settings) As Int32
            Dim days As Int32 = CType(Math.Ceiling(span.TotalDays), Int32)
            If days <= info.Service1 Then Return 1
            If days <= info.Service2 Then Return 2
            Return 3
        End Function

        Private Shared Function GetCounts(ByVal authorityId As Int32, ByVal data() As Referral, ByVal info As Settings, ByRef total() As Int32, ByVal typeId As Int32) As Int32()
            Dim results(6) As Int32
            Dim length As Int32 = data.Length
            Dim lastReferral As Referral
            Dim lastPeriod As TimeSpan = New TimeSpan(0)
            Dim i As Int32 = 0

            While i < length
                Dim currentId As Int32 = data(i).PermitInfoId
                While i < length AndAlso currentId = data(i).PermitInfoId
                    If Not lastReferral Is Nothing Then
                        lastPeriod = data(i).ChangeDate.Subtract(lastReferral.ChangeDate)
                        results(0) += 1
                        results(Slot(lastPeriod, info)) += 1
                    End If
                    If data(i).AssignedTo = authorityId AndAlso data(i).ApplicationTypeId = typeId Then
                        lastReferral = data(i)
                    Else
                        lastReferral = Nothing
                    End If
                    i += 1
                End While
            End While
            If Not lastReferral Is Nothing Then         'still with Authority
                Dim elapsed As TimeSpan = Date.Now.Subtract(lastReferral.ChangeDate)
                If elapsed.TotalDays > info.Service2 Then         'overdue
                    lastPeriod = elapsed
                    results(0) += 1
                    results(Slot(lastPeriod, info)) += 1
                End If
            End If
            If lastPeriod.Ticks > 0 Then
                results(3 + Slot(lastPeriod, info)) += 1    'update Last Referral Counts
            End If
            For i = 0 To 6
                total(i) += results(i)
            Next
            Return results
        End Function

        Private Shared Function GetReferrals(ByVal authorityId As Int32, ByVal fromDate As Date, ByVal toDate As Date) As Referral()
            Dim data As DataSet = Sprocs.dbo_usp_SearchReferrals(authorityId, fromDate, toDate, Nothing, GetType(DataSet))
            Dim count As Int32 = data.Tables(0).Rows.Count
            Dim results(count - 1) As Referral
            Dim i As Int32 = 0

            For Each row As DataRow In data.Tables(0).Rows
                results(i) = New Referral(row)
                i += 1
            Next
            Return results
        End Function

        Private Shared Sub OutputBlock(ByRef row As DataRow, ByVal prefix As String, ByVal totals() As Int32, ByVal heading() As String)
            With row
                .Item(prefix + "WithinHeading") = heading(0)
                .Item(prefix + "BetweenHeading") = heading(1)
                .Item(prefix + "OverHeading") = heading(2)
                .Item(prefix + "AllProcessed") = totals(0).ToString()
                .Item(prefix + "AllWithin") = totals(1).ToString()
                .Item(prefix + "AllBetween") = totals(2).ToString()
                .Item(prefix + "AllOver") = totals(3).ToString()
                .Item(prefix + "LastProcessed") = ""
                .Item(prefix + "LastWithin") = totals(4).ToString()
                .Item(prefix + "LastBetween") = totals(5).ToString()
                .Item(prefix + "LastOver") = totals(6).ToString()
            End With
        End Sub

        Private Shared Function GetHeadings(ByVal info As Settings) As String()
            Dim results(2) As String
            results(0) = "Within " + info.Service1.ToString() + " days"
            results(1) = "Between " + (info.Service1 + 1).ToString() + " & " + info.Service2.ToString() + " days"
            results(2) = "Over " + info.Service2.ToString() + " days"
            Return results
        End Function

        Private Shared Function GetTotalHeadings(ByVal info() As Settings) As String()
            Dim results(2) As String
            Dim match1 As Boolean = info(0).Service1 = info(1).Service1 AndAlso info(1).Service1 = info(2).Service1 AndAlso info(2).Service1 = info(3).Service1
            Dim match2 As Boolean = info(0).Service2 = info(1).Service2 AndAlso info(1).Service2 = info(2).Service2 AndAlso info(2).Service2 = info(3).Service2
            If match1 AndAlso match2 Then
                Return GetHeadings(info(0))
            End If
            results(0) = "Within Level 1"
            results(1) = "Between Level 1 and 2"
            results(2) = "Over Level 2"
            Return results
        End Function

        Public Shared Function GetTestData(ByVal fromDate As Date, ByVal toDate As Date, ByVal schema As String) As ReportDataResults

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

            ' JNCC Test Data
            NewRow = ReturnDS.Tables("BOServiceLevelsReferral").NewRow()

            With NewRow

                .Item("TotalsWithinHeading") = "Within 5 Days"
                .Item("TotalsBetweenHeading") = "Between 6 & 10 days"
                .Item("TotalsOverHeading") = "Over 10 days"

                .Item("TotalsHeading") = "JNCC TOTALS"

                .Item("TotalsAllProcessed") = "70"
                .Item("TotalsAllWithin") = "68"
                .Item("TotalsAllBetween") = "1"
                .Item("TotalsAllOver") = "1"

                .Item("TotalsLastProcessed") = ""
                .Item("TotalsLastWithin") = "70"
                .Item("TotalsLastBetween") = "0"
                .Item("TotalsLastOver") = "0"


                .Item("ExportWithinHeading") = "Within 5 Days"
                .Item("ExportBetweenHeading") = "Between 6 & 10 days"
                .Item("ExportOverHeading") = "Over 10 days"

                .Item("ExportAllProcessed") = "52"
                .Item("ExportAllWithin") = "52"
                .Item("ExportAllBetween") = "0"
                .Item("ExportAllOver") = "0"

                .Item("ExportLastProcessed") = ""
                .Item("ExportLastWithin") = "52"
                .Item("ExportLastBetween") = "0"
                .Item("ExportLastOver") = "0"

                .Item("ImportWithinHeading") = "Within 5 Days"
                .Item("ImportBetweenHeading") = "Between 6 & 10 days"
                .Item("ImportOverHeading") = "Over 10 days"

                .Item("ImportAllProcessed") = "18"
                .Item("ImportAllWithin") = "16"
                .Item("ImportAllBetween") = "1"
                .Item("ImportAllOver") = "1"

                .Item("ImportLastProcessed") = ""
                .Item("ImportLastWithin") = "18"
                .Item("ImportLastBetween") = "0"
                .Item("ImportLastOver") = "0"

                .Item("Article10WithinHeading") = "Within 5 Days"
                .Item("Article10BetweenHeading") = "Between 6 & 10 days"
                .Item("Article10OverHeading") = "Over 10 days"

                .Item("Article10AllProcessed") = "0"
                .Item("Article10AllWithin") = "0"
                .Item("Article10AllBetween") = "0"
                .Item("Article10AllOver") = "0"

                .Item("Article10LastProcessed") = ""
                .Item("Article10LastWithin") = "0"
                .Item("Article10LastBetween") = "0"
                .Item("Article10LastOver") = "0"


                .Item("Article30WithinHeading") = "Within 5 Days"
                .Item("Article30BetweenHeading") = "Between 6 & 10 days"
                .Item("Article30OverHeading") = "Over 10 days"

                .Item("Article30AllProcessed") = "0"
                .Item("Article30AllWithin") = "0"
                .Item("Article30AllBetween") = "0"
                .Item("Article30AllOver") = "0"

                .Item("Article30LastProcessed") = ""
                .Item("Article30LastWithin") = "0"
                .Item("Article30LastBetween") = "0"
                .Item("Article30LastOver") = "0"

            End With

            'add the row to the dataset
            ReturnDS.Tables("BOServiceLevelsReferral").Rows.Add(NewRow)



            ' KEW Test Data
            NewRow = ReturnDS.Tables("BOServiceLevelsReferral").NewRow()

            With NewRow

                .Item("TotalsWithinHeading") = "Within 5 Days"
                .Item("TotalsBetweenHeading") = "Between 6 & 10 days"
                .Item("TotalsOverHeading") = "Over 10 days"

                .Item("TotalsHeading") = "KEW TOTALS"

                .Item("TotalsAllProcessed") = "56"
                .Item("TotalsAllWithin") = "50"
                .Item("TotalsAllBetween") = "5"
                .Item("TotalsAllOver") = "1"

                .Item("TotalsLastProcessed") = ""
                .Item("TotalsLastWithin") = "55"
                .Item("TotalsLastBetween") = "1"
                .Item("TotalsLastOver") = "0"

                .Item("ExportWithinHeading") = "Within 5 Days"
                .Item("ExportBetweenHeading") = "Between 6 & 10 days"
                .Item("ExportOverHeading") = "Over 10 days"

                .Item("ExportAllProcessed") = "33"
                .Item("ExportAllWithin") = "32"
                .Item("ExportAllBetween") = "0"
                .Item("ExportAllOver") = "1"

                .Item("ExportLastProcessed") = ""
                .Item("ExportLastWithin") = "33"
                .Item("ExportLastBetween") = "0"
                .Item("ExportLastOver") = "0"

                .Item("ImportWithinHeading") = "Within 5 Days"
                .Item("ImportBetweenHeading") = "Between 6 & 10 days"
                .Item("ImportOverHeading") = "Over 10 days"

                .Item("ImportAllProcessed") = "18"
                .Item("ImportAllWithin") = "16"
                .Item("ImportAllBetween") = "2"
                .Item("ImportAllOver") = "0"

                .Item("ImportLastProcessed") = ""
                .Item("ImportLastWithin") = "18"
                .Item("ImportLastBetween") = "0"
                .Item("ImportLastOver") = "0"

                .Item("Article10WithinHeading") = "Within 5 Days"
                .Item("Article10BetweenHeading") = "Between 6 & 10 days"
                .Item("Article10OverHeading") = "Over 10 days"

                .Item("Article10AllProcessed") = "5"
                .Item("Article10AllWithin") = "2"
                .Item("Article10AllBetween") = "3"
                .Item("Article10AllOver") = "0"

                .Item("Article10LastProcessed") = ""
                .Item("Article10LastWithin") = "4"
                .Item("Article10LastBetween") = "1"
                .Item("Article10LastOver") = "0"

                .Item("Article30WithinHeading") = "Within 5 Days"
                .Item("Article30BetweenHeading") = "Between 6 & 10 days"
                .Item("Article30OverHeading") = "Over 10 days"

                .Item("Article30AllProcessed") = "5"
                .Item("Article30AllWithin") = "2"
                .Item("Article30AllBetween") = "3"
                .Item("Article30AllOver") = "0"

                .Item("Article30LastProcessed") = ""
                .Item("Article30LastWithin") = "4"
                .Item("Article30LastBetween") = "1"
                .Item("Article30LastOver") = "0"

            End With

            'add the row to the dataset
            ReturnDS.Tables("BOServiceLevelsReferral").Rows.Add(NewRow)



            'return the datset
            Return New ReportDataResults(ReturnDS, "")

        End Function

        Private Class Referral
            Private mPermitInfoId As Int32
            Private mApplicationTypeId As Int32
            Private mChangeDate As Date
            Private mAssignedTo As Int32

            Sub New(ByVal row As DataRow)
                mPermitInfoId = Integer.Parse(row.Item("PermitInfoId").ToString)
                mApplicationTypeId = Integer.Parse(row.Item("ApplicationTypeId").ToString)
                mChangeDate = CType(row.Item("ChangeDate"), Date)
                If Not TypeOf row.Item("AssignedTo") Is DBNull Then
                    mAssignedTo = Integer.Parse(row.Item("AssignedTo").ToString)
                End If
            End Sub

            Public ReadOnly Property PermitInfoId() As Int32
                Get
                    Return mPermitInfoId
                End Get
            End Property

            Public ReadOnly Property ApplicationTypeId() As Int32
                Get
                    Return mApplicationTypeId
                End Get
            End Property

            Public ReadOnly Property ChangeDate() As Date
                Get
                    Return mChangeDate
                End Get
            End Property

            Public ReadOnly Property AssignedTo() As Int32
                Get
                    Return mAssignedTo
                End Get
            End Property
        End Class

        Private Class Settings
            Private mService1 As Int32
            Private mService2 As Int32

            Sub New(ByVal service1 As Int32, ByVal service2 As Int32)
                mService1 = service1
                mService2 = service2
            End Sub

            Public ReadOnly Property Service1() As Int32
                Get
                    Return mService1
                End Get
            End Property

            Public ReadOnly Property Service2() As Int32
                Get
                    Return mService2
                End Get
            End Property
        End Class
    End Class
End Namespace

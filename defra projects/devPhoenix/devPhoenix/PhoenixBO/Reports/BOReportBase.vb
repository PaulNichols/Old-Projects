Namespace RPT
    Public MustInherit Class BOReport
        Inherits BaseBO

        Friend MustOverride Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()

        Public Overridable Function DoReport(ByVal description As String, ByVal searchReference As String, ByVal databaseId As Int32, ByVal reportDataset As DataSet, ByVal criteriaObject As Object, ByVal saveReport As Boolean, ByVal crystal_RPT As CrystalDecisions.CrystalReports.Engine.ReportDocument, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32) As BOReportResults
            Return DoReport(description, searchReference, databaseId, reportDataset, criteriaObject, saveReport, crystal_RPT, reportPrintJobId, printSequence, Nothing)
        End Function

        Public Overridable Function DoReport(ByVal description As String, ByVal searchReference As String, ByVal databaseId As Int32, ByVal reportDataset As DataSet, ByVal criteriaObject As Object, ByVal saveReport As Boolean, ByVal crystal_RPT As CrystalDecisions.CrystalReports.Engine.ReportDocument, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal parameterValues As Hashtable) As BOReportResults
            ' Use Dataset to Create PDF Report Output
            crystal_RPT.SetDataSource(reportDataset)

            ' Set Report Parameter Values
            If Not parameterValues Is Nothing Then
                For Each parameterValue As DictionaryEntry In parameterValues
                    PhoenixReport.Common.SetParameterValue(crystal_RPT, parameterValue.Key.ToString(), parameterValue.Value)
                Next
            End If

            ' Get Report Output Type from  criteriaObjectName object type name
            Dim criteriaObjectName As String = criteriaObject.GetType.Name
            Dim reportTypeService As New DataObjects.Service.ReportTypeService
            Dim reportTypeSet As DataObjects.EntitySet.ReportTypeSet = reportTypeService.GetByIndex_ReportType(criteriaObjectName, True)
            Dim outputType As Int32 = reportTypeSet.Entities(0).OutputType

            If description Is Nothing Then
                description = reportTypeSet.Entities(0).Description()
            End If

            ' Convert Output to Byte Array
            Dim outputStream As IO.MemoryStream
            'If outputType = ExportFormatType.Excel Then
            'Dim excelFormatOptions As New ExcelFormatOptions
            'excelFormatOptions.ExcelUseConstantColumnWidth = False
            'crystal_RPT.ExportOptions.FormatOptions = excelFormatOptions
            'End If
            outputStream = CType(crystal_RPT.ExportToStream(CType(outputType, CrystalDecisions.Shared.ExportFormatType)), IO.MemoryStream)
            Dim outputArray(CType(outputStream.Length, Int32)) As Byte
            outputStream.Read(outputArray, 0, CType(outputStream.Length, Int32))
            outputStream.Close()
            outputStream = Nothing

            ' Convert RPT to Byte Array
            Dim rptStream As IO.MemoryStream = CType(crystal_RPT.ExportToStream(CrystalDecisions.Shared.ExportFormatType.CrystalReport), IO.MemoryStream)
            Dim rptArray(CType(rptStream.Length, Int32)) As Byte
            rptStream.Read(rptArray, 0, CType(rptStream.Length, Int32))
            rptStream.Close()
            rptStream = Nothing

            ' Populate ReportResults Object
            printSequence += 1
            Dim reportResults As New BOReportResults
            reportResults.CreatedDate = Date.Now
            reportResults.Criteria = criteriaObject
            reportResults.Data = reportDataset
            reportResults.RPT = rptArray
            reportResults.ReportOutput = outputArray
            reportResults.ReportPrintJobId = reportPrintJobId
            reportResults.PrintSequence = printSequence
            reportResults.ExpiryDate = DateTime.Today.AddDays(1)
            reportResults.Size = CType(outputArray.Length / 1024, Int32)
            reportResults.ReportPrinterId = reportTypeSet.Entities(0).ReportPrinterId
            If reportTypeSet.Entities(0).StapleBatch = 0 Then reportResults.Staple = 0 Else reportResults.Staple = 1

            If saveReport Then
                reportResults.Description = description
                reportResults.SearchReference = searchReference
                reportResults.DataBaseId = databaseId
                reportResults.ExpiryDate = Nothing ' To Do - Maybe in next phase of Development
            End If

            reportResults.Save() ' oReportResults.ReportId should now contain new Id

            Return reportResults ' Return ReportResults Object

        End Function
    End Class
End Namespace

Imports uk.gov.defra.Phoenix.BO.ReportCriteria
Imports uk.gov.defra.Phoenix.BO.Application
Imports uk.gov.defra.Phoenix.BO.RPT
Imports uk.gov.defra.Phoenix.BO

Namespace RPT
    Public Class BOReportCommon
        Private Enum ReportReturn
            ReportId
            PrintJobId
        End Enum

        Public Shared Function GetCITESPrintJobId(ByVal permitinfoIds As Int32(), ByVal title As String, ByVal newStatus As BOPermitInfo.PermitStatusTypes) As Int32
            If permitinfoIds.Length = 0 Then
                Throw New Exception("No PermitInfo ids found in GetPrintJobId")
            End If
            Try
                Dim App As New [DO].DataObjects.Entity.Application(BOPermitInfo.GetAppIdFromPermitInfoId(permitinfoIds(0)))
                Dim CitesAppSet As [DO].DataObjects.EntitySet.CITESApplicationSet = App.GetRelatedCITESApplication()
                If Not CitesAppSet Is Nothing AndAlso CitesAppSet.Entities.Count > 0 Then
                    'Dim BoApp As BOApplication = BOApplication.PolymorphicCreate(App.ApplicationId)
                    'If BoApp Is Nothing Then
                    '    Throw New Exception("No Application found in GetPrintJobId")
                    'End If
                    Dim CitesApp As [DO].DataObjects.Entity.CITESApplication = CitesAppSet.Entities(0)

                    If newStatus = BOPermitInfo.PermitStatusTypes.Issued Then
                        'if the permit has multipes the add them to the list of permitinfoids
                        'the UI could not pass them in as it gets the array from the progresssion grid
                        'which shouldn't show the multiples until after issue
                        Dim AllPIs As New ArrayList
                        For Each pi As Int32 In permitinfoIds
                            Dim PermitInfos As BO.Application.BOPermitInfo() = New BOPermit(New BOPermitInfo(pi).PermitId).GetPermitInfos(Nothing)
                            For Each permitinfo As BO.Application.BOPermitInfo In PermitInfos
                                AllPIs.Add(permitinfo.PermitInfoId)
                            Next
                        Next
                        permitinfoIds = CType(AllPIs.ToArray(GetType(Int32)), Int32())
                    End If

                    Dim art10 As Boolean = CitesApp.GetRelatedArticle10.Entities.Count > 0
                    Dim criteria() As BO.ReportCriteria.ReportCriteria = _
                        CITES.Applications.BOCITESApplication.GetReportCriteria(permitinfoIds, newStatus, (CitesApp.IsComposite OrElse CitesApp.Consignment), App.ApplicationId, App.SemiComplete, art10)
                    Return GenerateCITESReport(title, criteria, True, ReportReturn.PrintJobId)
                End If
            Catch ex As Exception
                Throw New Exception("No Application found in GetPrintJobId")
            End Try
        End Function

        Private Shared Function GenerateCITESReport(ByVal title As String, ByVal Criteria As BO.ReportCriteria.ReportCriteria(), ByVal saveReport As Boolean, ByVal returnType As ReportReturn) As Int32
            Dim ReportResults As Phoenix.BO.RPT.BOReportResults() = BOReportCommon.CreateReport(Criteria, title, saveReport)

            If Not ReportResults Is Nothing AndAlso ReportResults.Length > 0 Then 'MLD 25/1/5 expanded
                If returnType = ReportReturn.PrintJobId Then
                    Return ReportResults(0).ReportPrintJobId
                End If
                Return ReportResults(0).ReportId
            End If
        End Function

        Public Shared Function GetCostCode() As String
            Dim Config As New BOConfiguration
                Dim Result As Object = Config.GetValue("CostCode")
                If Not Result Is Nothing AndAlso _
                    TypeOf Result Is String Then
                    Return Result.ToString
                End If
        End Function

        Public Shared Function GenerateSemiCompleteReminderLetter(ByVal permitInfoId() As Int32, ByVal ssoUserId As Int64) As Int32
            'Create the covering letter criteria object
            Dim criterion As New SemiCompleteReminderLetterCriteria
            'set the id of the application the letter will cover
            criterion.PermitInfoIds = permitInfoId
            criterion.SsoUserId = ssoUserId
            Return GenerateCITESReport("Semi-Complete Reminder Letter", New BO.ReportCriteria.ReportCriteria() {criterion}, True, ReportReturn.ReportId)
        End Function

        Public Shared Function CreatePermitRefusalLetter(ByVal permitInfoIds() As Int32, ByVal ssoUserId As Int64) As Int32
            'Create the covering letter criteria object
            Dim PermitRefusalLetterCriteria As New PermitRefusalLetterCriteria
            'set the id of the application the letter will cover
            PermitRefusalLetterCriteria.PermitInfoIds = permitInfoIds
            PermitRefusalLetterCriteria.SsoUserId = ssoUserId

            'create a sensible descrition for the report
            Return GenerateCITESReport("Permits Refusal Letter", New BO.ReportCriteria.ReportCriteria() {PermitRefusalLetterCriteria}, True, ReportReturn.ReportId)
        End Function

        Public Shared Function CreateCertificateRefusalLetter(ByVal permitInfoIds() As Int32, ByVal ssoUserId As Int64) As Int32
            'Create the covering letter criteria object
            Dim CertificateRefusalLetterCriteria As New CertificateRefusalLetterCriteria
            'set the id of the application the letter will cover
            CertificateRefusalLetterCriteria.PermitInfoIds = permitInfoIds
            CertificateRefusalLetterCriteria.SsoUserId = ssoUserId
            Return GenerateCITESReport("Certificates Refusal Letter", New BO.ReportCriteria.ReportCriteria() {CertificateRefusalLetterCriteria}, True, ReportReturn.ReportId)
        End Function

        Public Shared Function CreateCoveringLetter(ByVal applicationId As Int32, ByVal newStatus As BOPermitInfo.PermitStatusTypes, ByVal ssoUserId As Int64) As Int32
            'Create the covering letter criteria object
            Dim PermitCoverLetterCriteria As New CertificatePermitCoverLetterCriteria
            'set the id of the application the letter will cover
            PermitCoverLetterCriteria.ApplicationId = applicationId
            PermitCoverLetterCriteria.SsoUserId = SSOUserId             'MLD 31/1/5 added

            'create a sensible descrition for the report
            Dim Title As String = "Permit/Certificate "
            Select Case newStatus
                Case BOPermitInfo.PermitStatusTypes.Duplicate
                    Title = String.Concat(Title, "Duplicate")
                Case BOPermitInfo.PermitStatusTypes.Issued
                    Title = String.Concat(Title, "Issue")
                Case BOPermitInfo.PermitStatusTypes.Re_Issue
                    Title = String.Concat(Title, "Re-Issue")
            End Select
            Title = String.Concat(Title, " Covering Letter for Application - ", applicationId.ToString)

            Return GenerateCITESReport(Title, New BO.ReportCriteria.ReportCriteria() {PermitCoverLetterCriteria}, True, ReportReturn.ReportId)
        End Function

        Public Shared Function CreateArticle10ApplicationForm(ByVal appId As Int32) As Int32
            Dim permits() As BOPermit = BOApplication.PolymorphicCreate(appId).Permit
            Dim criteria(permits.Length - 1) As BO.ReportCriteria.ReportCriteria
            Dim criterion As New ApplicationArticle10ReportCriteria
            Dim i As Int32 = 0

            For Each permit As BOPermit In permits
                Dim info() As BOPermitInfo = permit.GetPermitInfos(Nothing)
                If info.Length > 0 Then
                    criterion.PermitInfoId = info(0).PermitInfoId
                    criteria(i) = criterion
                    i += 1
                End If
            Next
            ReDim Preserve criteria(i - 1)
            Return GenerateCITESReport("Certificate Application Form", criteria, True, ReportReturn.PrintJobId)
        End Function

        Public Shared Function CreatePermitApplicationForm(ByVal appId As Int32) As Int32
            Dim criteria(0) As BO.ReportCriteria.ReportCriteria
            Dim criterion As New ApplicationPermitCriteria
            criterion.ApplicationId = appId
            criteria(0) = criterion
            Return GenerateCITESReport("Permit Application Form", criteria, True, ReportReturn.ReportId)
        End Function

        Public Shared Function CreateReport(ByVal reportCriterias As ReportCriteria.ReportCriteria(), ByVal printJobDescription As String, ByVal saveReport As Boolean) As BOReportResults()
            Dim reportResults(-1) As BOReportResults
            Dim results(-1) As BOReportResults
            Dim reportPrintJobId As Int32 = -1

            ' Insert into ReportPrintJob table
            If saveReport Then
                Dim reportPrintJob As New DataObjects.Entity.ReportPrintJob
                Dim reportPrintJobService As DataObjects.Service.ReportPrintJobService = reportPrintJob.ServiceObject
                reportPrintJob = reportPrintJobService.Insert(Date.Now, printJobDescription, Nothing)
                reportPrintJobId = reportPrintJob.Id()
            End If
            Dim printSequence As Int32 = -1
            Dim idx As Int32

            For Each reportCriteria As reportCriteria.ReportCriteria In reportCriterias
                Dim RepType As RPT.BOReport = reportCriteria.Report
                results = RepType.Process(reportCriteria, reportPrintJobId, printSequence, saveReport)

                idx = reportResults.Length
                ReDim Preserve reportResults((reportResults.Length) + (results.Length - 1))

                results.Copy(results, 0, reportResults, idx, results.Length)

            Next reportCriteria

            ' Save Report
            idx = -1
            Dim ids(reportResults.Length - 1) As Int32
            For Each result As BOReportResults In reportResults
                idx += 1
                ids(idx) = reportResults(idx).ReportId
            Next

            Return reportResults
        End Function

        Public Shared Function GetPrintJob(ByVal printJobId As Int32) As BOReportResults()

            Dim report As New DataObjects.Entity.Report
            Dim reportService As DataObjects.Service.ReportService = report.ServiceObject
            Dim reportSet As DataObjects.EntitySet.ReportSet = reportService.GetForReportPrintJob(printJobId, Nothing)

            Dim reportResults As BOReportResults

            Dim boReportResults(reportSet.Entities.Count - 1) As BOReportResults
            For Each report In reportSet.Entities
                reportResults = New BOReportResults(report.Id)
                boReportResults(reportResults.PrintSequence) = reportResults
            Next

            Return boReportResults

        End Function

        Public Shared Function GetPrintingReportJobs(ByVal reportPrinterId As Int32) As ReportData.BoReportJob()

            Dim boReportJob As ReportData.BoReportJob

            Dim reportJobs As DataObjects.Views.Collection.ReportJobsBoundCollection
            reportJobs = DataObjects.Views.Service.ReportJobsService.ReportPrintingJobs(reportPrinterId)

            Dim boReportJobs(reportJobs.EntitySet.Count - 1) As ReportData.BoReportJob
            Dim idx As Int32 = -1
            For Each reportJob As DataObjects.Views.Entity.ReportJobs In reportJobs.EntitySet
                idx += 1
                boReportJobs(idx) = New ReportData.BoReportJob(reportJob)
            Next

            Return boReportJobs
        End Function

        Public Shared Function GetPrintedReportJobs(ByVal reportPrinterId As Int32) As ReportData.BoReportJob()

            Dim boReportJob As ReportData.BoReportJob

            Dim reportJobs As DataObjects.Views.Collection.ReportJobsBoundCollection
            reportJobs = DataObjects.Views.Service.ReportJobsService.ReportPrintedJobs(reportPrinterId)

            Dim boReportJobs(reportJobs.EntitySet.Count - 1) As ReportData.BoReportJob
            Dim idx As Int32 = -1
            For Each reportJob As DataObjects.Views.Entity.ReportJobs In reportJobs.EntitySet
                idx += 1
                boReportJobs(idx) = New ReportData.BoReportJob(reportJob)
            Next

            Return boReportJobs
        End Function

        Public Shared Function GetDeletedReportJobs(ByVal reportPrinterId As Int32) As ReportData.BoReportJob()

            Dim boReportJob As ReportData.BoReportJob

            Dim reportJobs As DataObjects.Views.Collection.ReportJobsBoundCollection
            reportJobs = DataObjects.Views.Service.ReportJobsService.ReportDeletedJobs(reportPrinterId)

            Dim boReportJobs(reportJobs.EntitySet.Count - 1) As ReportData.BoReportJob
            Dim idx As Int32 = -1
            For Each reportJob As DataObjects.Views.Entity.ReportJobs In reportJobs.EntitySet
                idx += 1
                boReportJobs(idx) = New ReportData.BoReportJob(reportJob)
            Next

            Return boReportJobs
        End Function

        Public Shared Function GetAuthorisedReportJobs(ByVal reportPrinterId As Int32) As ReportData.BoReportJob()

            Dim boReportJob As ReportData.BoReportJob

            Dim reportJobs As DataObjects.Views.Collection.ReportJobsBoundCollection
            reportJobs = DataObjects.Views.Service.ReportJobsService.ReportAuthorisedJobs(reportPrinterId)

            Dim boReportJobs(reportJobs.EntitySet.Count - 1) As ReportData.BoReportJob
            Dim idx As Int32 = -1
            For Each reportJob As DataObjects.Views.Entity.ReportJobs In reportJobs.EntitySet
                idx += 1
                boReportJobs(idx) = New ReportData.BoReportJob(reportJob)
            Next

            Return boReportJobs
        End Function

        Public Shared Function GetPrinterQueues() As ReportData.BoReportPrinter()

            Dim reportPrinter As New DataObjects.Entity.ReportPrinter
            Dim reportPrinterService As DataObjects.Service.ReportPrinterService = reportPrinter.ServiceObject
            Dim reportPrinterSet As DataObjects.EntitySet.ReportPrinterSet = reportPrinterService.GetAll()

            Dim reportPrinters(reportPrinterSet.Entities.Count - 1) As ReportData.BoReportPrinter
            Dim idx As Int32 = -1
            For Each reportPrinter In reportPrinterSet.Entities
                idx += 1
                reportPrinters(idx) = New ReportData.BoReportPrinter(reportPrinter.ReportPrinterId)
            Next

            Return reportPrinters


        End Function

        Public Shared Sub ResumeReportJob(ByVal reportAuthorisedQId As Int32)

            Dim reportPrinter As New ReportData.BoReportAuthorisedQ(reportAuthorisedQId)
            reportPrinter.PausedBy = ""
            reportPrinter.PausedDate = Nothing
            reportPrinter.Save()

        End Sub

        Public Shared Sub MoveReportJobToQueueTop(ByVal reportAuthorisedQId As Int32)

            Dim reportPrintSequence As ReportData.BoReportPrintSequence
            Dim printSequence As Int32 = reportPrintSequence.GetNextReportSequence(1)

            Dim reportPrinter As New ReportData.BoReportAuthorisedQ(reportAuthorisedQId)
            reportPrinter.PrintSequence = printSequence * -1
            reportPrinter.Save()

        End Sub

        Public Shared Sub PauseReportPrinter(ByVal reportPrinterId As Int32, ByVal pausedBy As String)

            Dim reportPrinter As New ReportData.BoReportPrinter(reportPrinterId)
            reportPrinter.PausedBy = pausedBy
            reportPrinter.PausedDate = Date.Now
            reportPrinter.Save()

            Dim sprocs As DataObjects.Sprocs
            sprocs.dbo_usp_ReportPausePrinterQueuedJobs(reportPrinterId, pausedBy, Nothing)


        End Sub

        Public Shared Sub ResumeReportPrinter(ByVal reportPrinterId As Int32)

            Dim reportPrinter As New ReportData.BoReportPrinter(reportPrinterId)
            reportPrinter.PausedBy = ""
            reportPrinter.PausedDate = Nothing
            reportPrinter.Save()

            Dim sprocs As DataObjects.Sprocs
            sprocs.dbo_usp_ReportResumePrinterQueuedJobs(reportPrinterId, Nothing)

        End Sub

        Public Shared Sub PauseReportJob(ByVal reportAuthorisedQId As Int32, ByVal pausedBy As String)

            Dim reportPrinter As New ReportData.BoReportAuthorisedQ(reportAuthorisedQId)
            reportPrinter.PausedBy = pausedBy
            reportPrinter.PausedDate = Date.Now
            reportPrinter.Save()

        End Sub

        Public Shared Sub AuthorisePrintJob(ByVal reportPrintJobId As Int32, ByVal fullAuthorisedUserName As String)
            ' Create Authorised row in ReportAuthorisedQ table
            Dim reportAuthorisedQ As ReportData.BoReportAuthorisedQ
            'Dim reportResults() As BOReportResults = GetPrintJob(reportPrintJobId)
            Dim reportView As New ReportData.BOReportView
            Dim reportResults() As ReportData.BOReportView = reportView.GetPrintJobView(reportPrintJobId)

            'Dim reportResults() As ReportData.BoReportJob = GetAuthorisedReportJobs(reportPrintJobId)
            Dim reportPrinter As ReportData.BoReportPrinter


            Dim reportJobCount As Int32 = reportResults.Length

            If reportJobCount > 0 Then
                Dim reportPrintSequence As ReportData.BoReportPrintSequence
                Dim printSequence As Int32 = reportPrintSequence.GetNextReportSequence(reportJobCount)

                For Each reportResult As ReportData.BOReportView In reportResults


                    reportAuthorisedQ = New ReportData.BoReportAuthorisedQ

                    reportPrinter = New ReportData.BoReportPrinter(reportResult.ReportPrinterId)

                    reportAuthorisedQ.PausedBy = reportPrinter.PausedBy.ToString()
                    reportAuthorisedQ.PausedDate = reportPrinter.PausedDate
                    reportAuthorisedQ.ReportId = reportResult.ReportId()
                    reportAuthorisedQ.ReportPrinterId = reportResult.ReportPrinterId()
                    reportAuthorisedQ.PrintSequence = printSequence
                    reportAuthorisedQ.AuthorisedBy = fullAuthorisedUserName
                    reportAuthorisedQ.AuthorisedDate = Date.Now
                    If reportResult.Staple = 1 Then reportAuthorisedQ.StapleOff = True Else reportAuthorisedQ.StapleOff = False
                    reportAuthorisedQ.Save()
                    printSequence += 1

                Next
            End If

        End Sub

        Public Shared Sub SetAuthoriseQDeleted(ByVal reportAuthorisedQId As Int32, ByVal fullAuthorisedUserName As String)

            Dim reportAuthorisedQ As New ReportData.BoReportAuthorisedQ(reportAuthorisedQId)

            reportAuthorisedQ.DeletedBy = fullAuthorisedUserName
            reportAuthorisedQ.DeletedDate = Date.Now

            Dim boReportPrintSequence As ReportData.BoReportPrintSequence
            reportAuthorisedQ.PrintSequence = boReportPrintSequence.GetNextReportSequence(1)

            reportAuthorisedQ.Save()

        End Sub

        Public Shared Sub SetAuthoriseQUnDeleted(ByVal reportAuthorisedQId As Int32, ByVal fullAuthorisedUserName As String)

            Dim reportAuthorisedQ As New ReportData.BoReportAuthorisedQ(reportAuthorisedQId)

            reportAuthorisedQ.DeletedBy = Nothing
            reportAuthorisedQ.DeletedDate = Nothing

            reportAuthorisedQ.PrintingDate = Nothing
            reportAuthorisedQ.PrintedDate = Nothing

            Dim boReportPrintSequence As ReportData.BoReportPrintSequence
            reportAuthorisedQ.PrintSequence = boReportPrintSequence.GetNextReportSequence(1)

            reportAuthorisedQ.Save()

        End Sub

        Public Shared Sub SetAuthoriseQReQueue(ByVal reportAuthorisedQId As Int32)

            Dim reportAuthorisedQ As New ReportData.BoReportAuthorisedQ(reportAuthorisedQId)

            reportAuthorisedQ.DeletedBy = Nothing
            reportAuthorisedQ.DeletedDate = Nothing

            reportAuthorisedQ.PrintingDate = Nothing
            reportAuthorisedQ.PrintedDate = Nothing

            Dim boReportPrintSequence As ReportData.BoReportPrintSequence
            reportAuthorisedQ.PrintSequence = boReportPrintSequence.GetNextReportSequence(1)

            reportAuthorisedQ.Save()

        End Sub
    End Class
End Namespace

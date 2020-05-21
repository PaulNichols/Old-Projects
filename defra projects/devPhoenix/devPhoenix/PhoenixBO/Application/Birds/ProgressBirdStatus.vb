Imports uk.gov.defra.Phoenix.BO.Application.Bird.Registration

Namespace Application.Bird
    Public Class ProgressBirdStatus
        Public Shared Function GetStatusList(ByVal ssouserid As Int64, ByVal ringApplicationId As Int32) As BirdStatusList()
            Dim Application As New BO.Application.Bird.Registration.BirdRegistration(ringApplicationId)
            Return ProgressBirdStatus.GetStatusList(ssouserid, Application)
        End Function

        Public Shared Function GetStatusList(ByVal ssouserid As Int64, ByVal application As BO.Application.Bird.Registration.BirdRegistration) As BirdStatusList()
            Dim RingsPicked As Boolean = False
            Dim InspectionRequired As Boolean = application.IsInspectionRequired
            If application.RegApplicationType = RegistrationApplicationType.Clutch Then
                RingsPicked = CType(application.RegistrationApplication, Clutch).RingsPicked
            End If
            Dim ChickApp As Boolean = (application.RegApplicationType = RegistrationApplicationType.Clutch)
            Return ProgressBirdStatus.GetStatusList(ssouserid, application.ApplicationStatus, application.AssignedTo, RingsPicked, InspectionRequired, ChickApp)
        End Function

        Public Shared Function GetStatusList(ByVal ssouserid As Int64, ByVal currentApplicationStatus As BOPermitInfo.PermitStatusTypes, ByVal assignedTo As Common.AssignedToList, _
                                             ByVal ringsPicked As Boolean, ByVal inspectionRequired As Boolean, _
                                             ByVal isApplicationChick As Boolean) As BirdStatusList()
            Dim IdList As New ArrayList

            Select Case currentApplicationStatus
                Case BOPermitInfo.PermitStatusTypes.BeingInput_Customer
                    If Common.IsInRole(ssouserid, Common.RolesList.Customer) Then
                        IdList.AddRange(New BOPermitInfo.PermitStatusTypes() { _
                                    BOPermitInfo.PermitStatusTypes.SubmittedByCustomer, _
                                    BOPermitInfo.PermitStatusTypes.Ring_Request_Submitted_By_Customer})
                    End If
                Case BOPermitInfo.PermitStatusTypes.BeingInput_CaseOfficer
                    If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                        IdList.AddRange(New BOPermitInfo.PermitStatusTypes() { _
                                    BOPermitInfo.PermitStatusTypes.ProgressAllowed, _
                                    BOPermitInfo.PermitStatusTypes.Cancelled})
                    End If
                Case BOPermitInfo.PermitStatusTypes.Ring_Request_Submitted_By_Customer, BOPermitInfo.PermitStatusTypes.SubmittedByCustomer
                    If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                        IdList.AddRange(New BOPermitInfo.PermitStatusTypes() { _
                                    BOPermitInfo.PermitStatusTypes.ProgressAllowed, _
                                    BOPermitInfo.PermitStatusTypes.Cancelled})
                    ElseIf Common.IsInRole(ssouserid, Common.RolesList.Customer) Then
                        IdList.Add(Application.BOPermitInfo.PermitStatusTypes.CancelPending)
                    End If
                Case BOPermitInfo.PermitStatusTypes.ProgressAllowed
                    If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                        IdList.AddRange(New BOPermitInfo.PermitStatusTypes() { _
                                    BOPermitInfo.PermitStatusTypes.Referred, _
                                    BOPermitInfo.PermitStatusTypes.Cancelled, _
                                    BOPermitInfo.PermitStatusTypes.Refused, _
                                    BOPermitInfo.PermitStatusTypes.Authorised})
                    ElseIf Common.IsInRole(ssouserid, Common.RolesList.Customer) Then
                        IdList.Add(BOPermitInfo.PermitStatusTypes.CancelPending)
                    End If
                Case BOPermitInfo.PermitStatusTypes.Referred
                    If Common.IsInRole(ssouserid, Common.RolesList.Customer) Then
                        IdList.AddRange(New BOPermitInfo.PermitStatusTypes() { _
                                    BOPermitInfo.PermitStatusTypes.CancelPending, _
                                    BOPermitInfo.PermitStatusTypes.Referred})
                    Else
                        Select Case assignedTo
                            Case Common.AssignedToList.Customer
                                If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                                    IdList.AddRange(New BOPermitInfo.PermitStatusTypes() { _
                                                BOPermitInfo.PermitStatusTypes.ProgressAllowed, _
                                                BOPermitInfo.PermitStatusTypes.Referred, _
                                                BOPermitInfo.PermitStatusTypes.Cancelled, _
                                                BOPermitInfo.PermitStatusTypes.Authorised})
                                End If
                            Case Common.AssignedToList.Inspectorate
                                If Common.IsInRole(ssouserid, Common.RolesList.Inspectorate) Then
                                    'Only set when rings picked and inspection decision made
                                    'IdList.Add(BOPermitInfo.PermitStatusTypes.Referred)
                                    If ringsPicked AndAlso inspectionRequired Then
                                        IdList.Add(BOPermitInfo.PermitStatusTypes.ProgressAllowed)
                                    End If
                                End If
                            Case Common.AssignedToList.TeamLeader
                                If Common.IsInRole(ssouserid, Common.RolesList.TeamLeader) Then
                                    IdList.AddRange(New BOPermitInfo.PermitStatusTypes() { _
                                                BOPermitInfo.PermitStatusTypes.ProgressAllowed, _
                                                BOPermitInfo.PermitStatusTypes.Referred})
                                End If
                            Case Common.AssignedToList.Other
                                If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                                    IdList.AddRange(New BOPermitInfo.PermitStatusTypes() { _
                                                BOPermitInfo.PermitStatusTypes.ProgressAllowed, _
                                                BOPermitInfo.PermitStatusTypes.Cancelled, _
                                                BOPermitInfo.PermitStatusTypes.Referred})
                                End If
                        End Select
                    End If
                Case BOPermitInfo.PermitStatusTypes.Authorised
                    If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                        If isApplicationChick Then
                            IdList.Add(BOPermitInfo.PermitStatusTypes.Chick_DOR_Issued)
                        Else
                            IdList.Add(BOPermitInfo.PermitStatusTypes.Adult_DOR_Issued)
                            IdList.Add(BOPermitInfo.PermitStatusTypes.Registered)
                        End If
                    End If
                Case BOPermitInfo.PermitStatusTypes.Chick_DOR_Issued
                    If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                        IdList.Add(BOPermitInfo.PermitStatusTypes.DOR_Returned)
                    End If
                Case BOPermitInfo.PermitStatusTypes.Adult_DOR_Issued
                    If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                        IdList.Add(BOPermitInfo.PermitStatusTypes.DOR_Returned)
                    End If
                Case BOPermitInfo.PermitStatusTypes.DOR_Returned
                    If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                        IdList.AddRange(New BOPermitInfo.PermitStatusTypes() { _
                                    BOPermitInfo.PermitStatusTypes.Registered, _
                                    BOPermitInfo.PermitStatusTypes.Refused})
                    End If
                Case BOPermitInfo.PermitStatusTypes.Refused, BOPermitInfo.PermitStatusTypes.Fate
                    'nothing to do!
                Case BOPermitInfo.PermitStatusTypes.Registered
                    If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                        If assignedTo = Common.AssignedToList.CaseOfficer Then
                            IdList.Add(BOPermitInfo.PermitStatusTypes.Fate)
                        ElseIf assignedTo = Common.AssignedToList.Customer Then
                            IdList.Add(BOPermitInfo.PermitStatusTypes.Closed_By_Customer)
                        End If
                    End If
                Case BOPermitInfo.PermitStatusTypes.Closed_By_Customer
                    If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                        IdList.Add(BOPermitInfo.PermitStatusTypes.Fate)
                    End If
                Case BOPermitInfo.PermitStatusTypes.CancelPending
                    If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                        If assignedTo = Common.AssignedToList.Customer Then
                            IdList.Add(BOPermitInfo.PermitStatusTypes.Cancelled)
                        ElseIf assignedTo = Common.AssignedToList.CaseOfficer Then
                            IdList.AddRange(New BOPermitInfo.PermitStatusTypes() { _
                                        BOPermitInfo.PermitStatusTypes.Cancelled, _
                                        BOPermitInfo.PermitStatusTypes.ProgressAllowed})
                        End If
                    End If
            End Select
            If IdList.Count > 0 Then
                'create something to put them in
                Dim ReturnVal(IdList.Count - 1) As BirdStatusList

                Dim LoopId As Int32 = 0
                For Each Index As BOPermitInfo.PermitStatusTypes In IdList
                    'Dim Status As DataObjects.Entity.PermitStatus = DataObjects.Entity.PermitStatus.GetById(Index)
                    ReturnVal(LoopId) = New BirdStatusList(Index) ', Status.Action)
                    LoopId += 1
                Next Index
                Return ReturnVal
            Else
                ' nothing to tell....
                Return Nothing
            End If
        End Function

        Public Shared Function GetUserList(ByVal ssouserid As Int64, ByVal newStatus As BOPermitInfo.PermitStatusTypes, ByVal applicationId As Int32) As BirdDataList
            Dim Application As New BO.Application.Bird.Registration.BirdRegistration(applicationId)
            Return ProgressBirdStatus.GetUserList(ssouserid, newStatus, Application)
        End Function

        Public Shared Function GetUserList(ByVal ssouserid As Int64, ByVal newStatus As BOPermitInfo.PermitStatusTypes, ByVal application As BO.Application.Bird.Registration.BirdRegistration) As BirdDataList
            Return ProgressBirdStatus.GetUserList(ssouserid, application.ApplicationStatus, newStatus, application.AssignedTo, application.Paid, application.ApplicationId, application.RegApplicationType)
        End Function

        Public Shared Function GetUserList(ByVal ssouserid As Int64, ByVal oldStatus As BOPermitInfo.PermitStatusTypes, ByVal newStatus As BOPermitInfo.PermitStatusTypes, ByVal assignedTo As Common.AssignedToList, ByVal paid As Boolean, ByVal applicationId As Int32, ByVal applicationType As Registration.RegistrationApplicationType) As BirdDataList
            Dim IdList As New ArrayList

            Select Case oldStatus
                Case BOPermitInfo.PermitStatusTypes.BeingInput_Customer
                    If Common.IsInRole(ssouserid, Common.RolesList.Customer) Then
                        IdList.Add(Common.AssignedToList.CaseOfficer)
                    End If
                Case BOPermitInfo.PermitStatusTypes.BeingInput_CaseOfficer
                    If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                        IdList.Add(Common.AssignedToList.CaseOfficer)
                    End If
                Case BOPermitInfo.PermitStatusTypes.Ring_Request_Submitted_By_Customer, BOPermitInfo.PermitStatusTypes.SubmittedByCustomer
                    If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                        If newStatus = BOPermitInfo.PermitStatusTypes.ProgressAllowed Then
                            IdList.Add(Common.AssignedToList.CaseOfficer)
                        End If
                    ElseIf Common.IsInRole(ssouserid, Common.RolesList.Customer) Then
                        If newStatus = BOPermitInfo.PermitStatusTypes.CancelPending Then
                            IdList.AddRange(GetCancelPendingAssignedToList())
                        End If
                    End If
                Case BOPermitInfo.PermitStatusTypes.ProgressAllowed
                    If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                        If newStatus = BOPermitInfo.PermitStatusTypes.Referred Then
                            IdList.AddRange(New Common.AssignedToList() { _
                                        Common.AssignedToList.Customer, _
                                        Common.AssignedToList.Inspectorate, _
                                        Common.AssignedToList.TeamLeader, _
                                        Common.AssignedToList.Other})
                        ElseIf newStatus = BOPermitInfo.PermitStatusTypes.Authorised Then
                            IdList.Add(Common.AssignedToList.CaseOfficer)
                        ElseIf newStatus = BOPermitInfo.PermitStatusTypes.Refused Then
                            IdList.Add(Common.AssignedToList.CaseOfficer)
                        End If
                    ElseIf Common.IsInRole(ssouserid, Common.RolesList.Customer) Then
                        If newStatus = BOPermitInfo.PermitStatusTypes.CancelPending Then
                            IdList.AddRange(GetCancelPendingAssignedToList())
                        End If
                    End If
                Case BOPermitInfo.PermitStatusTypes.Referred
                    Select Case assignedTo
                        Case Common.AssignedToList.Customer
                            If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                                If newStatus = BOPermitInfo.PermitStatusTypes.ProgressAllowed Then
                                    IdList.Add(Common.AssignedToList.CaseOfficer)
                                ElseIf newStatus = BOPermitInfo.PermitStatusTypes.Authorised Then
                                    IdList.Add(Common.AssignedToList.CaseOfficer)
                                End If
                            ElseIf Common.IsInRole(ssouserid, Common.RolesList.Customer) Then
                                If newStatus = BOPermitInfo.PermitStatusTypes.CancelPending Then
                                    IdList.AddRange(GetCancelPendingAssignedToList())
                                End If
                            End If
                        Case Common.AssignedToList.Inspectorate
                            If Common.IsInRole(ssouserid, Common.RolesList.Inspectorate) Then
                                If newStatus = BOPermitInfo.PermitStatusTypes.ProgressAllowed Then
                                    IdList.Add(Common.AssignedToList.CaseOfficer)
                                End If
                            ElseIf Common.IsInRole(ssouserid, Common.RolesList.Customer) Then
                                If newStatus = BOPermitInfo.PermitStatusTypes.CancelPending Then
                                    IdList.AddRange(GetCancelPendingAssignedToList())
                                End If
                            End If
                        Case Common.AssignedToList.TeamLeader
                            If Common.IsInRole(ssouserid, Common.RolesList.TeamLeader) Then
                                If newStatus = BOPermitInfo.PermitStatusTypes.ProgressAllowed Then
                                    IdList.Add(Common.AssignedToList.CaseOfficer)
                                End If
                            ElseIf Common.IsInRole(ssouserid, Common.RolesList.Customer) Then
                                If newStatus = BOPermitInfo.PermitStatusTypes.CancelPending Then
                                    IdList.AddRange(GetCancelPendingAssignedToList())
                                End If
                            End If
                        Case Common.AssignedToList.Other
                            If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                                If newStatus = BOPermitInfo.PermitStatusTypes.ProgressAllowed Then
                                    IdList.Add(Common.AssignedToList.CaseOfficer)
                                End If
                            ElseIf Common.IsInRole(ssouserid, Common.RolesList.Customer) Then
                                If newStatus = BOPermitInfo.PermitStatusTypes.CancelPending Then
                                    IdList.AddRange(GetCancelPendingAssignedToList())
                                End If
                            End If
                    End Select
                Case BOPermitInfo.PermitStatusTypes.Authorised
                    If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                        If newStatus = BOPermitInfo.PermitStatusTypes.Chick_DOR_Issued Then
                            IdList.Add(Common.AssignedToList.CaseOfficer)
                        ElseIf newStatus = BOPermitInfo.PermitStatusTypes.Adult_DOR_Issued Then
                            IdList.AddRange(New Common.AssignedToList() { _
                                        Common.AssignedToList.Customer, _
                                        Common.AssignedToList.CaseOfficer})
                        ElseIf newStatus = BOPermitInfo.PermitStatusTypes.Registered Then
                            IdList.AddRange(New Common.AssignedToList() { _
                                        Common.AssignedToList.Customer, _
                                        Common.AssignedToList.CaseOfficer})
                        End If
                    End If
                Case BOPermitInfo.PermitStatusTypes.Chick_DOR_Issued
                    If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                        If newStatus = BOPermitInfo.PermitStatusTypes.DOR_Returned Then
                            IdList.Add(Common.AssignedToList.CaseOfficer)
                        End If
                    End If
                Case BOPermitInfo.PermitStatusTypes.Adult_DOR_Issued
                    If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                        If newStatus = BOPermitInfo.PermitStatusTypes.DOR_Returned Then
                            IdList.Add(Common.AssignedToList.CaseOfficer)
                        End If
                    End If
                Case BOPermitInfo.PermitStatusTypes.DOR_Returned
                    If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                        'can only do something if the application has been paid.
                        If paid AndAlso newStatus = BOPermitInfo.PermitStatusTypes.Registered Then
                            IdList.Add(Common.AssignedToList.CaseOfficer)
                        ElseIf newStatus = BOPermitInfo.PermitStatusTypes.Refused Then
                            IdList.Add(Common.AssignedToList.CaseOfficer)
                            'IdList.AddRange(New Common.AssignedToList() { _
                            ''Common.AssignedToList.Customer, _
                            'Common.AssignedToList.CaseOfficer})
                        End If
                    End If
                Case BOPermitInfo.PermitStatusTypes.Refused, BOPermitInfo.PermitStatusTypes.Fate, _
                     BOPermitInfo.PermitStatusTypes.Cancelled
                    'nothing to do when ...
                Case BOPermitInfo.PermitStatusTypes.CancelPending
                    'nothing to do when ...
                    If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                        If newStatus = BOPermitInfo.PermitStatusTypes.Cancelled OrElse newStatus = BOPermitInfo.PermitStatusTypes.ProgressAllowed Then
                            IdList.Add(Common.AssignedToList.CaseOfficer)
                        End If
                    End If
                Case BOPermitInfo.PermitStatusTypes.Registered
                    If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                        If newStatus = BOPermitInfo.PermitStatusTypes.Fate Then
                            IdList.Add(Common.AssignedToList.CaseOfficer)
                        End If
                    ElseIf Common.IsInRole(ssouserid, Common.RolesList.Customer) Then
                        If newStatus = BOPermitInfo.PermitStatusTypes.Closed_By_Customer Then
                            IdList.Add(Common.AssignedToList.CaseOfficer)
                        End If
                    End If
                Case BOPermitInfo.PermitStatusTypes.Closed_By_Customer
                    If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                        If newStatus = BOPermitInfo.PermitStatusTypes.Fate Then
                            IdList.Add(Common.AssignedToList.CaseOfficer)
                        End If
                    End If
            End Select

            If IdList.Count > 0 Then
                'create something to put them in
                Dim ReturnVal(IdList.Count - 1) As BirdAssignedToList
                'load the database statuses

                Dim LoopId As Int32 = 0
                For Each Index As Common.AssignedToList In IdList
                    ReturnVal(LoopId) = New BirdAssignedToList(Index)
                    LoopId += 1
                Next Index
                Return New BirdDataList(applicationId, ReturnVal, newStatus, oldStatus, applicationType)
            Else
                Return Nothing
            End If
        End Function

        Private Shared Function GetCancelPendingAssignedToList() As Common.AssignedToList()
            Return New Common.AssignedToList() { _
                Common.AssignedToList.CaseOfficer, _
                Common.AssignedToList.Customer, _
                Common.AssignedToList.Inspectorate, _
                Common.AssignedToList.TeamLeader, _
                Common.AssignedToList.Other}
        End Function

        Public Shared Function SetBirdStatus(ByVal ssouserid As Int64, ByVal applicationId As Int32, ByVal data As BirdDataList) As BirdStatusChanged
            Dim Application As New BO.Application.Bird.Registration.BirdRegistration(applicationId)
            Return ProgressBirdStatus.SetBirdStatus(ssouserid, Application, data)
        End Function

        Public Shared Function SetBirdStatus(ByVal ssouserid As Int64, ByVal application As BO.Application.Bird.Registration.BirdRegistration, ByVal data As BirdDataList) As BirdStatusChanged
            Dim ReportInfo As ProgressBirdStatusInfo.PrintResults = Nothing

            'change the status & who it will be assignedto
            Dim CurrentStatus As BOPermitInfo.PermitStatusTypes = application.ApplicationStatus
            With data.Info
                application.ApplicationStatus = .Status
                application.AssignedTo = .AssignedTo
            End With

            If TypeOf data.Info Is ProgressBirdStatusInfo.BirdStatusChange_Declined Then
                With CType(data.Info, ProgressBirdStatusInfo.BirdStatusChange_Declined)
                    If .DeclineReason Is Nothing OrElse .DeclineReason.Length = 0 Then
                        Throw New NullReferenceException("Decline reason cannot be empty")
                    Else
                        application.DeclineReason = .DeclineReason
                        ReportInfo = CreateDeclineLetter(application.ApplicationId, ssouserid)
                        application.RefuseLetterReportId = ReportInfo.ReportId
                    End If
                End With
            ElseIf TypeOf data.Info Is ProgressBirdStatusInfo.BirdStatusChange_Registered Then
                'ReportInfo = CreateRegistrationDocument(application)
            ElseIf TypeOf data.Info Is ProgressBirdStatusInfo.BirdStatusChange_Cancel Then
                'make sure it's in a status that we can cancel from - this checks the CURRENT status
                With CType(data.Info, ProgressBirdStatusInfo.BirdStatusChange_Cancel)
                    If .CancellationReason Is Nothing OrElse .CancellationReason.Length = 0 Then
                        Throw New NullReferenceException("CancellationReason reason cannot be empty")
                    Else
                        Select Case CurrentStatus
                            Case application.ApplicationStatus.Ring_Request_Submitted_By_Customer, _
                                 application.ApplicationStatus.SubmittedByCustomer, _
                                 application.ApplicationStatus.ProgressAllowed, _
                                 application.ApplicationStatus.Referred, _
                                 application.ApplicationStatus.CancelPending
                            Case Else
                                Throw New CannotException("Customers and Case Officers", "Cancel", " when its at a particulr status")
                        End Select
                        ''ok, things are good
                        'If Common.IsInRole(ssouserid, Common.RolesList.CaseOfficer) Then
                        '    data.Info.Status = application.ApplicationStatus.Cancelled
                        'Else ' must be a customer
                        '    data.Info.Status = application.ApplicationStatus.Cancel_Pending
                        'End If
                        application.CancellationReason = .CancellationReason
                    End If
                End With
            ElseIf TypeOf data.Info Is ProgressBirdStatusInfo.BirdStatusChange_RefuseCancel Then
                'make sure it's in a status that we can cancel from - this checks the CURRENT status
                With CType(data.Info, ProgressBirdStatusInfo.BirdStatusChange_RefuseCancel)
                    If .CancellationRefuseReason Is Nothing OrElse .CancellationRefuseReason.Length = 0 Then
                        Throw New NullReferenceException("CancellationRefuseReason reason cannot be empty")
                    Else
                        If CurrentStatus = BOPermitInfo.PermitStatusTypes.CancelPending Then
                            'ok, things are good
                            application.CancellationRefusalReason = .CancellationRefuseReason
                        Else
                            Throw New CannotException("Customers and Case Officers", "Cancel", " when its at a particulr status")
                        End If
                    End If
                End With
            ElseIf TypeOf data.Info Is ProgressBirdStatusInfo.BirdStatusChange_Referred Then
                application.NextActionDate = CType(data.Info, ProgressBirdStatusInfo.BirdStatusChange_Referred).NextActionDate
            ElseIf TypeOf data.Info Is ProgressBirdStatusInfo.BirdStatusChange_DORReturned Then
                PersistApplication(application)
            End If

            'if we need to persist a note then do so
            If Not data.Info.GetType.GetInterface(GetType(application.IAdditionalInformationNote).ToString) Is Nothing Then
                Dim AddInfo As application.IAdditionalInformationNote = CType(data.Info, application.IAdditionalInformationNote)
                If Not AddInfo.Note Is Nothing AndAlso _
                   AddInfo.Note.Length > 0 Then
                    'create a note

                    Dim NewApplicationNote As New BO.Application.CITES.Applications.ApplicationNote
                    With NewApplicationNote
                        .Content = AddInfo.Note
                        .CreatedDate = Date.Now
                        .CreatedById = ssouserid
                        .Important = False
                        .Active = True
                        .NoteDate = Date.Now
                        .Subject = String.Concat(BirdStatusList.GetEnglishStatus(data.Info.Status), " Note")
                        .OtherId = application.ApplicationId

                        Dim NewNote As BOCommon.BaseNote = .Save()
                        If Not NewNote Is Nothing Then
                            'save the join
                            Dim Join As New DataObjects.Service.ApplicationNoteService
                            Join.Insert(.OtherId, NewNote.NoteId)
                        End If
                    End With
                End If
            End If

            '...and save
            Return New BirdStatusChanged(application.Save(), ReportInfo)
        End Function

        Private Shared Sub PersistApplication(ByRef application As Registration.BirdRegistration)
            For Each Item As Registration.AdultSpecimenType In application.RegistrationApplication.Specimens
                If Item.SpecimenType.SpecimenId = 0 Then
                    'not persistsed yet so do so
                    Dim AquisitionDate As Object = Nothing
                    Dim FateCode As Object = Item.SpecimenType.FateCode
                    Dim HasRings As Boolean = True
                    If FateCode Is Nothing Then
                        'obly worth creating specimens that aren't fated
                        If application.RegApplicationType = RegistrationApplicationType.Other OrElse _
                           application.RegApplicationType = RegistrationApplicationType.Found OrElse _
                           application.RegApplicationType = RegistrationApplicationType.Imported Then
                            AquisitionDate = CType(Item, Registration.IDateAcquired).DateAcquired()
                        ElseIf application.RegApplicationType = RegistrationApplicationType.Clutch Then
                            HasRings = (Item.Rings.Length > 0)
                        End If
                        If HasRings Then
                            'only if the specimen has rings and its a clutch or if it's an adult
                            Dim NewSpecimen As DataObjects.Entity.Specimen
                            With Item.SpecimenType
                                Dim IsHatchDateExact As Boolean = False
                                If Not .IsHatchDateExact Is Nothing AndAlso TypeOf .IsHatchDateExact Is Boolean Then
                                    IsHatchDateExact = CType(.IsHatchDateExact, Boolean)
                                End If
                                NewSpecimen = NewSpecimen.Insert(CType(.Gender, Int32), _
                                                                 .HatchDate, _
                                                                 FateCode, _
                                                                 Nothing, _
                                                                 Nothing, _
                                                                 AquisitionDate, _
                                                                 Nothing, _
                                                                 IsHatchDateExact, _
                                                                 Nothing)
                            End With
                            'get the new specimen id that's been persisted
                            Item.SpecimenType.SpecimenId = NewSpecimen.SpecimenId

                            'add the link to the items parents
                            If Not application.Parents Is Nothing Then
                                If application.Parents.Father.Length > 0 Then
                                    For Each Father As Parent In application.Parents.Father
                                        If Father.SpecimenId > 0 Then DataObjects.Entity.ParentSpecimen.Insert(Item.SpecimenType.SpecimenId, Father.SpecimenId)
                                    Next Father
                                End If
                                If application.Parents.Mother.Length > 0 Then
                                    For Each Mother As Parent In application.Parents.Mother
                                        If Mother.SpecimenId > 0 Then DataObjects.Entity.ParentSpecimen.Insert(Item.SpecimenType.SpecimenId, Mother.SpecimenId)
                                    Next Mother
                                End If
                            End If

                            'add the id mark links
                            If Item.IDMarks.Length > 0 Then
                                For Each Mark As IDMark In Item.IDMarks
                                    DataObjects.Entity.SpecimenIDMark.Insert(Mark.MarkType, Item.SpecimenType.SpecimenId, Mark.Mark, Mark.MarkFate)
                                Next Mark
                            End If
                            If Item.Rings.Length > 0 Then
                                For Each Ring As IDMark In Item.Rings
                                    DataObjects.Entity.SpecimenIDMark.Insert(Ring.MarkType, Item.SpecimenType.SpecimenId, Ring.Mark, Ring.MarkFate)
                                Next Ring
                            End If

                            'load the application to get extra info
                            Dim App As New DataObjects.Entity.Application(application.ApplicationId)

                            'add the link to party
                            Dim PartySpecLink As New Party.BOPartySpecimen(application.PartyId, Item.SpecimenType.SpecimenId, CType(application.PartyAddressId, Int32), Nothing)
                            PartySpecLink.StartDate = App.CreatedDate
                            PartySpecLink.RoleType = Party.BOPartySpecimen.Role.Owner
                            PartySpecLink.PartySpecimenStatus = Party.BOPartySpecimen.Status.Current
                            PartySpecLink.Save()

                            'update the submitted flag on the application so that we know we've persisted the application
                            App.Submitted = True
                            App.SaveChanges()
                        End If
                    End If
                End If
            Next Item
        End Sub

        Private Shared Function CreateDeclineLetter(ByVal ringApplicationid As Int32, ByVal ssoUserId As Int64) As ProgressBirdStatusInfo.PrintResults
            Dim ReportCrtieria As New ReportCriteria.RegistrationRefusalLetterCriteria
            ReportCrtieria.Description = "DOR Declined Letter"
            'actually passing in the application id as don't have permits.
            ReportCrtieria.ApplicationId = ringApplicationid
            ReportCrtieria.SsoUserId = ssoUserId
            Dim ReportResult As RPT.BOReportResults() = RPT.BOReportCommon.CreateReport(New ReportCriteria.ReportCriteria() {ReportCrtieria}, ReportCrtieria.Description, True)
            Return New ProgressBirdStatusInfo.PrintResults(ReportResult(0).ReportPrintJobId, ReportResult(0).ReportId)
        End Function

        Private Shared Function CreateRegistrationDocument(ByVal applicationId As Int32) As ProgressBirdStatusInfo.PrintResults
            Dim App As New BirdRegistration(applicationId)

            Dim Specimens As New ArrayList

            Const ReportDesc As String = "Bird Registration Document"

            For Each Item As Registration.AdultSpecimenType In App.RegistrationApplication.Specimens
                'if the specimenid is zero then we haven't persisted it.
                'this would imply that the specimen was perhaps fated.
                'only create a report for those specimens that have rings....
                If Item.SpecimenType.SpecimenId > 0 AndAlso Item.HasRings Then
                    Dim ReportCrtieria As New ReportCriteria.BirdRegDocCriteria
                    ReportCrtieria.Description = ReportDesc
                    ReportCrtieria.ApplicationId = App.ApplicationId
                    ReportCrtieria.SpecimenId = Item.SpecimenType.SpecimenId
                    Specimens.Add(ReportCrtieria)
                End If
            Next Item

            If Specimens.Count > 0 Then
                Dim ReportCrtieriaArray(Specimens.Count - 1) As ReportCriteria.ReportCriteria
                Specimens.CopyTo(ReportCrtieriaArray)
                Dim ReportResult As RPT.BOReportResults() = RPT.BOReportCommon.CreateReport(ReportCrtieriaArray, ReportDesc, True)
                Return New ProgressBirdStatusInfo.PrintResults(ReportResult(0).ReportPrintJobId)
            Else
                Return Nothing
            End If
        End Function

        Private Shared Function CreateDOR(ByVal applicationId As Int32, ByVal applicationType As Registration.RegistrationApplicationType) As ProgressBirdStatusInfo.PrintResults
            Dim ReportCrtieria As ReportCriteria.ReportCriteria
            Dim ReportDesc As String = String.Empty

            If applicationType = RegistrationApplicationType.Clutch Then
                ReportDesc = "Schedule 4 Chick DOR"

                ReportCrtieria = New ReportCriteria.Schedule4ChicksCriteria
                With CType(ReportCrtieria, ReportCriteria.Schedule4ChicksCriteria)
                    .Description = ReportDesc
                    .ApplicationId = applicationId
                End With

            Else
                Throw New NotImplementedException("Adult DOR's are not implemented!")
            End If
            Dim ReportResult As RPT.BOReportResults() = RPT.BOReportCommon.CreateReport(New ReportCriteria.ReportCriteria() {ReportCrtieria}, ReportDesc, True)

            'update the DORID on the application
            Dim App As New BirdRegistration(applicationId)
            App.DORPrintJobId = ReportResult(0).ReportPrintJobId

            If App.Save() Is Nothing Then
                Return Nothing
            Else
                Return New ProgressBirdStatusInfo.PrintResults(App.DORPrintJobId)
            End If
        End Function

        <Serializable()> _
        Public Class BirdStatusChanged
            Public Sub New()
            End Sub

            Friend Sub New(ByVal application As BO.Application.Bird.Registration.BirdRegistration)
                MyClass.New(application, Nothing)
            End Sub

            Friend Sub New(ByVal application As BO.Application.Bird.Registration.BirdRegistration, ByVal reportInfo As ProgressBirdStatusInfo.PrintResults)
                mApplication = application
                mReportInfo = reportInfo
            End Sub

            Public Property HasReport() As Boolean
                Get
                    Return Not (mReportInfo Is Nothing)
                End Get
                Set(ByVal Value As Boolean)
                    'readonly
                End Set
            End Property

            Public Property ReportInfo() As ProgressBirdStatusInfo.PrintResults
                Get
                    Return mReportInfo
                End Get
                Set(ByVal Value As ProgressBirdStatusInfo.PrintResults)
                    mReportInfo = Value
                End Set
            End Property
            Private mReportInfo As ProgressBirdStatusInfo.PrintResults

            Public Property Application() As BO.Application.Bird.Registration.BirdRegistration
                Get
                    Return mApplication
                End Get
                Set(ByVal Value As BO.Application.Bird.Registration.BirdRegistration)
                    mApplication = Value
                End Set
            End Property
            Private mApplication As BO.Application.Bird.Registration.BirdRegistration
        End Class

        <Serializable()> _
        Public Class BirdStatusList
            Public Sub New()
            End Sub

            Public Sub New(ByVal id As BOPermitInfo.PermitStatusTypes, ByVal description As String)
                mId = id
                mDescription = description
            End Sub

            Public Sub New(ByVal id As BOPermitInfo.PermitStatusTypes)
                mId = id
                mDescription = GetEnglishStatus(id)
                '                MyClass.New(id, System.Enum.GetName(GetType(ApplicationStatus), id))
            End Sub

            Friend Shared Function GetEnglishStatus(ByVal id As BOPermitInfo.PermitStatusTypes) As String
                Dim Result As String = String.Empty
                Dim Status As New DataObjects.Entity.PermitStatus(CType(id, Int32))
                If Not Status Is Nothing Then
                    Result = Status.Action
                End If
                Return Result
            End Function

            Public Property Description() As String
                Get
                    Return mDescription
                End Get
                Set(ByVal Value As String)
                    mDescription = Value
                End Set
            End Property
            Private mDescription As String

            Public Property Id() As BOPermitInfo.PermitStatusTypes
                Get
                    Return mId
                End Get
                Set(ByVal Value As BOPermitInfo.PermitStatusTypes)
                    mId = Value
                End Set
            End Property
            Private mId As BOPermitInfo.PermitStatusTypes
        End Class

        <Serializable()> _
        Public Class BirdDataList
            Public Sub New()
            End Sub

            Public Sub New(ByVal applicationId As Int32, ByVal assignedTo As BirdAssignedToList(), ByVal newStatus As BOPermitInfo.PermitStatusTypes, ByVal oldStatus As BOPermitInfo.PermitStatusTypes, ByVal applicationType As Registration.RegistrationApplicationType)
                mAssignedTo = assignedTo

                Select Case newStatus
                    Case BOPermitInfo.PermitStatusTypes.Refused
                        mInfo = New ProgressBirdStatusInfo.BirdStatusChange_Declined(newStatus)
                    Case BOPermitInfo.PermitStatusTypes.Registered
                        mInfo = New ProgressBirdStatusInfo.BirdStatusChange_Registered(newStatus)
                        CType(mInfo, ProgressBirdStatusInfo.BirdStatusChange_Registered).ReportInfo = CreateRegistrationDocument(applicationId)
                    Case BOPermitInfo.PermitStatusTypes.CancelPending, BOPermitInfo.PermitStatusTypes.Cancelled
                        mInfo = New ProgressBirdStatusInfo.BirdStatusChange_Cancel(newStatus)
                    Case BOPermitInfo.PermitStatusTypes.ProgressAllowed
                        If oldStatus = BOPermitInfo.PermitStatusTypes.CancelPending Then
                            mInfo = New ProgressBirdStatusInfo.BirdStatusChange_RefuseCancel(newStatus)
                        Else
                            mInfo = New ProgressBirdStatusInfo.BirdStatusChange_ProgressAllowed(newStatus)
                        End If
                    Case BOPermitInfo.PermitStatusTypes.DOR_Returned
                        mInfo = New ProgressBirdStatusInfo.BirdStatusChange_DORReturned(newStatus)
                    Case BOPermitInfo.PermitStatusTypes.Referred
                        mInfo = New ProgressBirdStatusInfo.BirdStatusChange_Referred(newStatus)
                    Case BOPermitInfo.PermitStatusTypes.Authorised
                        mInfo = New ProgressBirdStatusInfo.BirdStatusChange_Authorise(newStatus)
                    Case BOPermitInfo.PermitStatusTypes.Chick_DOR_Issued, BOPermitInfo.PermitStatusTypes.Adult_DOR_Issued
                        mInfo = New ProgressBirdStatusInfo.BirdStatusChange_Issued(newStatus)
                        'pop the report in
                        CType(mInfo, ProgressBirdStatusInfo.BirdStatusChange_Issued).ReportInfo = CreateDOR(applicationId, applicationType)
                    Case Else
                        Throw New NotImplementedException(newStatus.ToString() & " hasn't been yet been implemented")
                        'mInfo = New ProgressBirdStatusInfo.BaseBirdStatusChange(newStatus)
                End Select

                'If Not mInfo Is Nothing AndAlso _
                '   Not mInfo.GetType.GetInterface(GetType(ProgressBirdStatusInfo.IReportType).ToString) Is Nothing Then
                '    With CType(mInfo, ProgressBirdStatusInfo.IReportType)
                '        Select Case status
                '            Case ApplicationStatus.Declined
                '                mInfo = New ProgressBirdStatusInfo.BirdStatusChange_Declined
                '        End Select
                '    End With
                'End If
            End Sub

            'Public Property HasReport() As Boolean
            '    Get
            '        Return (Not mInfo Is Nothing AndAlso _
            '                Not mInfo.GetType.GetInterface(GetType(ProgressBirdStatusInfo.IReportType).ToString) Is Nothing)
            '    End Get
            '    Set(ByVal Value As Boolean)
            '        'readonly
            '    End Set
            'End Property

            Public Property AssignedTo() As BirdAssignedToList()
                Get
                    Return mAssignedTo
                End Get
                Set(ByVal Value As BirdAssignedToList())
                    mAssignedTo = Value
                End Set
            End Property
            Private mAssignedTo As BirdAssignedToList()

            Public Property Info() As ProgressBirdStatusInfo.BaseBirdStatusChange
                Get
                    Return mInfo
                End Get
                Set(ByVal Value As ProgressBirdStatusInfo.BaseBirdStatusChange)
                    mInfo = Value
                End Set
            End Property
            Private mInfo As ProgressBirdStatusInfo.BaseBirdStatusChange
        End Class

        <Serializable()> _
        Public Class BirdAssignedToList
            Public Sub New()
            End Sub

            Public Sub New(ByVal id As Common.AssignedToList)
                mId = id
                mDescription = System.Enum.GetName(GetType(Common.AssignedToList), id)
            End Sub

            Public Property Description() As String
                Get
                    Return mDescription
                End Get
                Set(ByVal Value As String)
                    mDescription = Value
                End Set
            End Property
            Private mDescription As String

            Public Property Id() As Common.AssignedToList
                Get
                    Return mId
                End Get
                Set(ByVal Value As Common.AssignedToList)
                    mId = Value
                End Set
            End Property
            Private mId As Common.AssignedToList
        End Class
    End Class
End Namespace
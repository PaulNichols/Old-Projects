Namespace Application.Bird.Registration
    Public Class BirdRegistrationSearch
        Private Shared ApplicationId As Int32

        Public Shared Sub RemoveExistingSearchInfo(ByVal applicationId As Int32, ByVal tran As SqlClient.SqlTransaction)
            DataObjects.Sprocs.usp_DeleteRingRequestSearchEntries(applicationId, tran)
        End Sub

        Friend Shared Sub UpdateSeachInfo(ByVal birdReg As BirdRegistration)
            'Future Note: if application is "Found" then CITES Source is W (Wild taken)
            Dim SearchEntity As New DataObjects.Entity.RingRequestSearch
            Dim Tran As SqlClient.SqlTransaction = SearchEntity.ServiceObject.BeginTransaction()
            'remove any existing entries
            ApplicationId = birdReg.ApplicationId
            RemoveExistingSearchInfo(ApplicationId, Tran)

            'add the new entries
            If birdReg.RelatedRingApplicationId > 0 Then
                'only worth adding them if it has been related...
                InsertSearchRecord(RegistrationSearchTypes.RelatedRingApplicationId, birdReg.RelatedRingApplicationId)
            End If

            'the application status
            InsertSearchRecord(RegistrationSearchTypes.ApplicationStatus, CType(birdReg.ApplicationStatus, Int32))
            InsertSearchRecord(RegistrationSearchTypes.ApplicationStatusText, birdReg.ApplicationStatus_Helper)
            InsertSearchRecord(RegistrationSearchTypes.AssignedTo, birdReg.AssignedTo_Helper)
            InsertSearchRecord(RegistrationSearchTypes.ApplicationStatusText_Customer, birdReg.ApplicationStatusCustomer_Helper)
            If birdReg.SLAClock > 0 Then InsertSearchRecord(RegistrationSearchTypes.SLA, birdReg.SLAClock)

            'info
            InsertSearchRecord(RegistrationSearchTypes.SubmittedDate, birdReg.SubmittedDate)

            Dim Specimens() As AdultSpecimenType = birdReg.RegistrationApplication.Specimens
            If Not Specimens Is Nothing AndAlso Specimens.Length > 0 Then
                'Check the Scientific Names
                Dim SciName As String = Nothing
                Dim PermitNo As Int32 = 1
                For Each Specimen As AdultSpecimenType In Specimens
                    If Not Specimen.SpecimenType Is Nothing Then
                        If Not SciName Is Nothing AndAlso _
                           SciName <> "*" AndAlso _
                           Not Specimen.SpecimenType.ScientificName Is Nothing AndAlso _
                           Specimen.SpecimenType.ScientificName.Length > 0 Then
                            If SciName Is Nothing Then
                                SciName = Specimen.SpecimenType.ScientificName
                            ElseIf SciName <> Specimen.SpecimenType.ScientificName Then
                                'a different name
                                SciName = "*"
                            End If
                        End If
                        If Specimen.SpecimenType.SpecimenId > 0 Then
                            InsertSearchRecord(RegistrationSearchTypes.SpecimenId, Specimen.SpecimenType.SpecimenId)
                        End If
                    End If
                    'only if the specimen has rings can it have a permit created and thus a permit number
                    If Specimen.HasRings Then
                        InsertSearchRecord(RegistrationSearchTypes.PermitInfoId, PermitNo)
                        'increment for the next one we find
                        PermitNo += 1

                        'if it has rings, record the ring detail
                        For Each Ring As IDMark In Specimen.Rings
                            If Ring.HasMarkType AndAlso Not Ring.Mark Is Nothing AndAlso Ring.Mark.Length > 0 Then
                                'only persist if it has rings
                                InsertSearchRecord(RegistrationSearchTypes.IDMarkAndType, GetConcatenatedMarkInfo(Ring))
                            End If
                        Next Ring
                    End If
                Next Specimen
                If Not SciName Is Nothing Then
                    InsertSearchRecord(RegistrationSearchTypes.SearchScientificName, SciName)
                End If
            End If

            Select Case birdReg.RegApplicationType
                Case RegistrationApplicationType.Clutch
                    Dim Clutch As Clutch = CType(birdReg.RegistrationApplication, Clutch)
                    Dim EggDate As Date
                    If Not Clutch.LastLaidDate Is Nothing Then
                        EggDate = CType(Clutch.LastLaidDate, Date)
                    Else
                        'if we don't have this date, check to see if we have hatch dates
                        If Clutch.Eggs.Length > 0 Then
                            For Each Egg As ClutchSpecimen In Clutch.Specimens
                                If Not Egg.SpecimenType.HatchDate Is Nothing And TypeOf Egg.SpecimenType.HatchDate Is Date Then
                                    'check to see if it's greater than current date
                                    If EggDate.Ticks = 0 OrElse Date.op_GreaterThan(CType(Egg.SpecimenType.HatchDate, Date), EggDate) Then
                                        EggDate = CType(Egg.SpecimenType.HatchDate, Date)
                                    End If
                                End If
                            Next Egg
                        End If
                    End If
                    If EggDate.Ticks > 0 Then
                        InsertSearchRecord(RegistrationSearchTypes.LastLaidDate, Clutch.LastLaidDate)
                    End If

                    ''onlt count eggs that are for this app and not ones from cloned
                    'Dim EggCount As Int32 = 0
                    'For Each Egg As ClutchEgg In Clutch.Eggs
                    '    If Not egg.Cloned Then EggCount += 1
                    'Next egg
                    InsertSearchRecord(RegistrationSearchTypes.EggCount, Clutch.Eggs.Length)
            End Select
            'party
            InsertSearchRecord(RegistrationSearchTypes.PartyId, birdReg.Party)
            Dim PartyObj As New BO.Party.BOParty(birdReg.PartyId)
            If PartyObj.IsBusiness Then
                PartyObj = New Party.BOPartyBusiness(birdReg.PartyId)
            Else
                PartyObj = New Party.BOPartyIndividual(birdReg.PartyId)
            End If
            If Not PartyObj Is Nothing Then
                InsertSearchRecord(RegistrationSearchTypes.PartyName, PartyObj.DisplayName)
                PartyObj = Nothing
            End If

            'commit the tran
            SearchEntity.ServiceObject.EndTransaction(Tran, EnterpriseObjects.Service.TransactionEndEnum.Commit)
        End Sub

        Friend Shared Function GetConcatenatedMarkInfo(ByVal ring As IDMark) As String
            Dim StoredMark As String = String.Concat(ring.MarkType.ToString(), "|", ring.Mark.ToString())
            Return StoredMark
        End Function

        Private Shared Sub InsertSearchRecord(ByVal searchType As RegistrationSearchTypes, ByVal value As Object)
            DataObjects.Entity.RingRequestSearch.Insert(ApplicationId, CType(searchType, Int32), value)
        End Sub

        Friend Enum RegistrationSearchTypes
            ApplicationStatus = 1
            SubmittedDate = 2
            PartyId = 3
            SearchScientificName = 4
            LastLaidDate = 5
            EggCount = 6
            ApplicationStatusText = 7
            ApplicationStatusText_Customer = 8
            AssignedTo = 9
            SpecimenId = 10
            RelatedRingApplicationId = 11
            PartyName = 12
            PermitInfoId = 13
            SLA = 14
            IDMarkAndType = 15
        End Enum

        Friend Shared Function GetSearchInfo(ByVal searchType As RegistrationSearchTypes, ByVal keyWord As Int32) As DataObjects.EntitySet.RingRequestSearchSet
            Return GetSearchInfo(searchType, keyWord.ToString())
        End Function

        Friend Shared Function GetSearchInfo(ByVal searchType As RegistrationSearchTypes, ByVal keyWord As String) As DataObjects.EntitySet.RingRequestSearchSet
            Dim SearchService As DataObjects.Service.RingRequestSearchService = DataObjects.Entity.RingRequestSearch.ServiceObject
            Dim Results As DataObjects.EntitySet.RingRequestSearchSet = SearchService.GetByIndex_IX_KeyWord(CType(searchType, Int32), keyWord)

            If Results Is Nothing OrElse Results.Count = 0 Then
                Return Nothing
            Else
                Return Results
            End If
        End Function

        Friend Shared Function GetSearchInfoByRingApp(ByVal searchType As RegistrationSearchTypes, ByVal applicationId As Int32) As DataObjects.EntitySet.RingRequestSearchSet
            Dim SearchService As DataObjects.Service.RingRequestSearchService = DataObjects.Entity.RingRequestSearch.ServiceObject
            Dim Results As DataObjects.EntitySet.RingRequestSearchSet = SearchService.GetByIndex_IX_RingRequestID(applicationId, CType(searchType, Int32))

            If Results Is Nothing OrElse Results.Count = 0 Then
                Return Nothing
            Else
                Return Results
            End If
        End Function

        'Friend Shared Function GetSearchInfoByRingRequestID(ByVal searchType As RegistrationSearchTypes, ByVal keyWordId As Int32) As DataObjects.EntitySet.RingRequestSearchSet
        '    Dim SearchService As DataObjects.Service.RingRequestSearchService = DataObjects.Entity.RingRequestSearch.ServiceObject
        '    Dim Results As DataObjects.EntitySet.RingRequestSearchSet = SearchService.GetByIndex_IX_KeyWord(keyWordId, CType(searchType, Int32).ToString())

        '    If Results Is Nothing OrElse Results.Count = 0 Then
        '        Return Nothing
        '    Else
        '        Return Results
        '    End If
        'End Function

        Friend Shared Function GetIds(ByVal searchType As RegistrationSearchTypes, ByVal keyWord As Int32) As Int32()
            'initialise the array for the results
            Dim Results(-1) As Int32
            'get the applications for this status
            Dim DataResults As DataObjects.EntitySet.RingRequestSearchSet = BirdRegistrationSearch.GetSearchInfo(searchType, keyWord)
            If Not DataResults Is Nothing Then
                'we have applications so iterate through them getting the list
                Dim ItemArray As New ArrayList
                For Each SearchItem As DataObjects.Entity.RingRequestSearch In DataResults
                    ItemArray.Add(SearchItem.RingRequestId)
                Next SearchItem
                'convert back to an int array for the funtion return
                Results = CType(ItemArray.ToArray(GetType(Int32)), Int32())
            End If
            Return Results
        End Function

        Friend Shared Function GetRingRequestsId(ByVal searchType As RegistrationSearchTypes, ByVal keyWord As BOPermitInfo.PermitStatusTypes) As Int32()
            'initialise the array for the results
            Dim Results(-1) As Int32
            'get the applications for this status
            Dim DataResults As DataObjects.EntitySet.RingRequestSearchSet = BirdRegistrationSearch.GetSearchInfo(BirdRegistrationSearch.RegistrationSearchTypes.ApplicationStatus, CType(keyWord, Int32).ToString)
            If Not DataResults Is Nothing Then
                'we have applications so iterate through them getting the list
                Dim ItemArray As New ArrayList
                For Each SearchItem As DataObjects.Entity.RingRequestSearch In DataResults
                    ItemArray.Add(SearchItem.RingRequestId)
                Next SearchItem
                'convert back to an int array for the funtion return
                Results = CType(ItemArray.ToArray(GetType(Int32)), Int32())
            End If

            'may need to get more results in here from the Application tables.  
            'Currently only used for cancelled which can only be retrieved via 
            'the RingApplication XML.  Anything until Authorised is ok.
            Select Case keyWord
                Case BOPermitInfo.PermitStatusTypes.Chick_DOR_Issued, _
                     BOPermitInfo.PermitStatusTypes.Adult_DOR_Issued, _
                     BOPermitInfo.PermitStatusTypes.Authorised, _
                     BOPermitInfo.PermitStatusTypes.Refused, _
                     BOPermitInfo.PermitStatusTypes.Registered, _
                     BOPermitInfo.PermitStatusTypes.Fate, _
                     BOPermitInfo.PermitStatusTypes.Closed_By_Customer, _
                     BOPermitInfo.PermitStatusTypes.Cancelled
                    'these status will *probably* need to look into other 
                    'tables to get additional data
            End Select

            'throw 'em back
            Return Results
        End Function
    End Class
End Namespace
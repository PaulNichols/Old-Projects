Namespace Application.Search
    <Serializable()> _
    Public Class ApplicationSearch
        Public Enum SearchMode
            Permit
            Application
            Species
            Permit_With_Multiples
        End Enum

        Private Enum SearchGroup
            CaseOfficer
            KewJNCC
            Inspectorate
            Customer
            Other
        End Enum

        Private Enum SearchType
            None = 0
            CITES = 1
            ImportNotification = 2
            SeizureNotification = 3
            Birds = 4
        End Enum

        Public Sub New()
        End Sub

        Public Shared Function SearchForApplications(ByVal criteria As ApplicationSearchCriteriaBase, ByVal type As SearchMode, ByVal ssoUserid As Int64) As SearchResultGroup
            Dim ReturnObj As New SearchResultGroup
            Dim Group As SearchGroup

            'If ssoUserId < 0 Then
            '    Select Case ssoUserId
            '        Case -1
            '            Group = SearchGroup.CaseOfficer
            '        Case -2
            '            Group = SearchGroup.Customer
            '        Case -3
            '            Group = SearchGroup.Inspectorate
            '        Case -4
            '            Group = SearchGroup.KewJNCC
            '        Case -5
            '            Group = SearchGroup.Other
            '    End Select
            'Else
            If Common.IsInRole(ssoUserid, Common.RolesList.CaseOfficer) Then
                Group = SearchGroup.CaseOfficer
            ElseIf Common.IsInRole(ssoUserid, Common.RolesList.Kew) OrElse _
                   Common.IsInRole(ssoUserid, Common.RolesList.JNCC) Then
                Group = SearchGroup.KewJNCC
            ElseIf Common.IsInRole(ssoUserid, Common.RolesList.Inspectorate) Then
                Group = SearchGroup.Inspectorate
            ElseIf Common.IsInRole(ssoUserid, Common.RolesList.Customer) Then
                Group = SearchGroup.Customer
            Else 'others
                Group = SearchGroup.Other
            End If
            ' End If
            ReturnObj.Permits = GetPermitData_Permits(criteria, type, Group)
            If BringBackImportNotifications(criteria) Then ReturnObj.ImportNotifications = GetPermitData_Notifications(criteria, True, Group)
            If BringBackSeizureNotifications(criteria) Then ReturnObj.SeizureNotifications = GetPermitData_Notifications(criteria, False, Group)

            'MLD 11/4/5 Temporary Try/Catch added around bird stuff. Remove when bird searching is complete. 
            Try
                ReturnObj.ChickDORs = GetData_ChickDOR(criteria, Group)
            Catch ex As Exception
            End Try

            Return ReturnObj
        End Function

        Private Shared Function BringBackSeizureNotifications(ByVal criteria As uk.gov.defra.Phoenix.BO.Application.Search.ApplicationSearchCriteriaBase) As Boolean
            If TypeOf criteria Is BO.Application.Search.ApplicationSearchCriteriaDetailed Then
                If Not CType(criteria, BO.Application.Search.ApplicationSearchCriteriaDetailed).DocumentType Is Nothing Then
                    Return System.Array.IndexOf(CType(criteria, BO.Application.Search.ApplicationSearchCriteriaDetailed).DocumentType, uk.gov.defra.Phoenix.BO.Application.Search.ApplicationSearchCriteriaDetailed_Customer.DocumentTypeList.Seizure_Notification) > -1
                Else
                    Return True
                End If
            Else
                Return True
            End If
        End Function

        Private Shared Function BringBackImportNotifications(ByVal criteria As uk.gov.defra.Phoenix.BO.Application.Search.ApplicationSearchCriteriaBase) As Boolean
            If TypeOf criteria Is BO.Application.Search.ApplicationSearchCriteriaDetailed Then
                If Not CType(criteria, BO.Application.Search.ApplicationSearchCriteriaDetailed).DocumentType Is Nothing Then
                    Return System.Array.IndexOf(CType(criteria, BO.Application.Search.ApplicationSearchCriteriaDetailed).DocumentType, uk.gov.defra.Phoenix.BO.Application.Search.ApplicationSearchCriteriaDetailed_Customer.DocumentTypeList.Import_Notification) > -1
                Else
                    Return True
                End If
            Else
                Return True
            End If
        End Function

        ' MORE DETAILED
        Private Shared Function GetCITESPermit(ByVal criteria As ApplicationSearchCriteriaBase, ByVal headerType As SearchResults.ColumnHeaderType, ByVal viewName As String, ByVal searchType As SearchType, ByVal service As EnterpriseObjects.Service, ByVal entitySetType As Type) As EnterpriseObjects.EntitySet
            Dim StatusId() As Int32 = Nothing
            Dim DisplayName As String = Nothing
            Dim PartyId As Int32 = 0
            Dim SAAdviceId As Int32 = 0
            Dim AssignedToRoleId() As Int32 = Nothing
            Dim InspectorateAdviceId As Int32 = 0
            Dim IsAgent As Boolean = False
            Dim IsExporter As Boolean = False
            Dim IsImporter As Boolean = False
            Dim IsHolder As Boolean = False
            Dim IsKeeper As Boolean = False
            Dim DateApplicationReceived As DateRange = Nothing
            Dim DateLogged As DateRange = Nothing
            Dim DateOfReferral As DateRange = Nothing
            Dim DateIssued As DateRange = Nothing

            Dim AppId As Int32 = 0
            Dim IdMarkTypeId As Int32 = 0
            Dim IdMark As Int32 = 0
            Dim ScientificName As String = Nothing

            If Not criteria.getType.GetInterface(GetType(Search.IApplicationSearchCommon_Customer).ToString) Is Nothing Then
                If Not criteria.getType.GetInterface(GetType(Search.IApplicationSearchCommon).ToString) Is Nothing Then
                    With CType(criteria, Search.IApplicationSearchCommon)
                        If Not .PartyId Is Nothing AndAlso .PartyId.toString.Length > 0 Then
                            PartyId = CType(.PartyId, Int32)
                        End If

                        If Not .PartyName Is Nothing AndAlso .PartyName.Length > 0 Then
                            DisplayName = .PartyName
                        End If

                        If Not .Status Is Nothing AndAlso .Status.Length > 0 Then
                            ReDim StatusId(.Status.Length - 1)
                            Dim Index As Int32 = 0
                            For Each Status As Int32 In .Status
                                StatusId(Index) = Status
                                Index += 1
                            Next Status
                        End If

                        If Not .SAAdvice Is Nothing AndAlso .SAAdvice.ID > 0 Then
                            SAAdviceId = .SAAdvice.ID
                        End If

                        If Not .AssignedToRole Is Nothing AndAlso .AssignedToRole.Length > 0 Then
                            ReDim AssignedToRoleId(.AssignedToRole.Length - 1)
                            Dim Index As Int32 = 0
                            For Each AssignedTo As Int32 In .AssignedToRole
                                AssignedToRoleId(Index) = AssignedTo
                                Index += 1
                            Next AssignedTo
                        End If

                        If Not .InspectorateAdvice Is Nothing AndAlso .InspectorateAdvice.ID > 0 Then
                            InspectorateAdviceId = .InspectorateAdvice.ID
                        End If

                        If ApplicationSearchCriteriaCommon.PartyTypes.Agent <> ApplicationSearchCriteriaCommon.PartyTypes.Unknown Then
                            IsAgent = .PartyTypeInApplication = ApplicationSearchCriteriaCommon.PartyTypes.Agent
                            IsExporter = .PartyTypeInApplication = ApplicationSearchCriteriaCommon.PartyTypes.Exporter
                            IsImporter = .PartyTypeInApplication = ApplicationSearchCriteriaCommon.PartyTypes.Importer
                            IsHolder = .PartyTypeInApplication = ApplicationSearchCriteriaCommon.PartyTypes.Holder
                            IsKeeper = .PartyTypeInApplication = ApplicationSearchCriteriaCommon.PartyTypes.Keeper
                        End If

                        DateApplicationReceived = .DateApplicationReceived
                        DateLogged = .DateLogged
                        DateOfReferral = .DateOfReferral
                        DateIssued = .DateIssued
                    End With
                End If
                With CType(criteria, Search.IApplicationSearchCommon_Customer)
                    If Not .IDMarkType Is Nothing AndAlso .IDMarkType.ID > 0 Then
                        IdMarkTypeId = .IDMarkType.ID
                    End If

                    If Not .IDMarkNumber Is Nothing AndAlso .IDMarkNumber.Length > 0 Then
                        IdMark = CType(IdMark, Int32)
                    End If

                    If Not .AcceptedScientificName Is Nothing AndAlso .AcceptedScientificName.Length > 0 Then
                        ScientificName = .AcceptedScientificName
                    End If
                End With
                If Not criteria.getType.GetInterface(GetType(Search.ISearchApplicationId).ToString) Is Nothing Then
                    With CType(criteria, Search.ISearchApplicationId)
                        If .ApplicationId > 0 Then
                            AppId = .ApplicationId
                        End If
                    End With
                End If
            End If

            Dim command As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("dbo.usp_SearchCITESPermitCaseOfficer_ByPermit")
            command.CommandType = System.Data.CommandType.StoredProcedure
            command.Parameters.Add("@ViewName", System.Data.SqlDbType.VarChar, 100).Value = viewName
            command.Parameters.Add("@SearchType", System.Data.SqlDbType.Int).Value = searchType

            If TypeOf criteria Is Search.ApplicationSearchCriteriaDetailed_Customer Then
                Dim DateReturned As DateRange = Nothing
                Dim DateUsed As DateRange = Nothing
                Dim DateComplete As DateRange = Nothing
                Dim DateAuthorised As DateRange = Nothing
                Dim DateRefused As DateRange = Nothing
                Dim DateCancelled As DateRange = Nothing
                Dim ExpiryDate As DateRange = Nothing
                Dim SubmittedBy As ApplicationSearchCriteriaDetailed_Customer.SubmittedByWho = ApplicationSearchCriteriaDetailed_Customer.SubmittedByWho.Either
                Dim LoggedById As Int32 = 0
                Dim IssuedById As Int32 = 0
                Dim PartyAddressIds As Int32() = Nothing
                Dim LWTAddressIds As Int32() = Nothing
                Dim MailingAddressIds As Int32() = Nothing
                Dim DocumentTypeIds As Int32() = Nothing
                Dim MAAddressIds As Int32() = Nothing
                Dim SourceIds As Int32() = Nothing
                Dim PurposeIds As Int32() = Nothing
                Dim PDIds As Int32() = Nothing
                Dim UOMId As Int32 = 0
                Dim Mass As NumberRange = Nothing
                Dim Quantity As NumberRange = Nothing
                Dim MassUsed As NumberRange = Nothing
                Dim LinkedToSeizureNotification As Search.ApplicationSearchCriteriaDetailed_Customer.Logical = Nothing
                Dim DelegationAuthorityId As Int32 = 0
                Dim UnderDerogation As Search.ApplicationSearchCriteriaDetailed_Customer.Logical = ApplicationSearchCriteriaDetailed_Customer.Logical.Either
                Dim ScientificAdviceId As Int32 = 0
                Dim HasAdHocScientificAdvice As Search.ApplicationSearchCriteriaDetailed_Customer.Logical = ApplicationSearchCriteriaDetailed_Customer.Logical.Either
                Dim SpecialConditionId As Int32 = 0
                Dim HasSpecialConditions As Search.ApplicationSearchCriteriaDetailed_Customer.Logical = ApplicationSearchCriteriaDetailed_Customer.Logical.Either
                Dim BillOfLading As String
                Dim DateSeized As DateRange = Nothing
                Dim PortOfEntryId As Int32 = 0
                Dim SNStatus As Search.ApplicationSearchCriteriaDetailed.SNStatusList = ApplicationSearchCriteriaDetailed.SNStatusList.Either
                Dim CountryOfExportId As Int32 = 0
                Dim CountryOfImportId As Int32 = 0
                Dim CountryOfOriginId As Int32 = 0
                Dim CountryOfOriginDate As DateRange = Nothing
                Dim CountryOfOriginNumber As Int32 = 0
                Dim CountryOfLastExportId As Int32 = 0
                Dim CountryOfLastExportDate As DateRange = Nothing
                Dim CountryOfLastExportNumber As Int32 = 0
                Dim ManagementAuthorityCountryId As Int32 = 0
                Dim CommonName As String = Nothing
                Dim AppliedForName As String
                Dim Hybrid As Search.ApplicationSearchCriteriaDetailed_Customer.Logical = ApplicationSearchCriteriaDetailed_Customer.Logical.Either
                Dim ECAnnex As String = Nothing
                Dim CITESAppendix As String = Nothing
                Dim SpecimenQuantity As Int32 = 0
                Dim SpecimenNetMass As Decimal = 0
                Dim SpecimenGender As Search.ApplicationSearchCriteriaDetailed_Customer.Gender = ApplicationSearchCriteriaDetailed_Customer.Gender.Either

                If TypeOf criteria Is Search.ApplicationSearchCriteriaDetailed Then
                    With CType(criteria, Search.ApplicationSearchCriteriaDetailed)
                        ExpiryDate = .ExpiryDate
                        LoggedById = .LoggedById
                        IssuedById = .IssuedById
                        HasAdHocScientificAdvice = .AdHocScientificAdvice
                        HasSpecialConditions = .AdHocSpecialCondition
                        SNStatus = .SNStatus
                        If Not .DocumentType Is Nothing AndAlso .DocumentType.Length > 0 Then
                            ReDim DocumentTypeIds(.DocumentType.Length - 1)
                            Dim Index As Int32 = 0
                            For Each DocumentTypeId As Int32 In .DocumentType
                                DocumentTypeIds(Index) = DocumentTypeId
                                Index += 1
                            Next DocumentTypeId
                        End If
                        If Not .PartyAddressIds Is Nothing AndAlso .PartyAddressIds.Length > 0 Then
                            ReDim PartyAddressIds(.PartyAddressIds.Length - 1)
                            Dim Index As Int32 = 0
                            For Each PartyAddressId As Int32 In .PartyAddressIds
                                PartyAddressIds(Index) = PartyAddressId
                                Index += 1
                            Next PartyAddressId
                        End If
                        If Not .LiveWildTakenAddressIds Is Nothing AndAlso .LiveWildTakenAddressIds.Length > 0 Then
                            ReDim LWTAddressIds(.LiveWildTakenAddressIds.Length - 1)
                            Dim Index As Int32 = 0
                            For Each LWTAddressId As Int32 In .LiveWildTakenAddressIds
                                LWTAddressIds(Index) = LWTAddressId
                                Index += 1
                            Next LWTAddressId
                        End If
                        If Not .MailingAddressIds Is Nothing AndAlso .MailingAddressIds.Length > 0 Then
                            ReDim MailingAddressIds(.MailingAddressIds.Length - 1)
                            Dim Index As Int32 = 0
                            For Each MailingAddressId As Int32 In .MailingAddressIds
                                MailingAddressIds(Index) = MailingAddressId
                                Index += 1
                            Next MailingAddressId
                        End If

                        If Not .ScientificAdvice Is Nothing Then ScientificAdviceId = .ScientificAdvice.ID
                        If Not .SpecialCondition Is Nothing Then SpecialConditionId = .SpecialCondition.ID
                    End With
                End If
                If TypeOf criteria Is Search.ApplicationSearchCriteriaCommon_Customer Then
                    With CType(criteria, Search.ApplicationSearchCriteriaDetailed_Customer)
                        If Not .PartDerivative Is Nothing AndAlso .PartDerivative.Length > 0 Then
                            DateReturned = .DateReturned
                            DateUsed = .DateUsed
                            DateAuthorised = .DateAuthorised
                            DateComplete = .DateComplete
                            DateRefused = .DateRefused
                            DateCancelled = .DateCancelled
                            SubmittedBy = .SubmittedBy
                            LinkedToSeizureNotification = .LinkedToSeizureNotification
                            UnderDerogation = .Derogation
                            DateSeized = .DateSeized

                            If Not .ManagementAuthorityAddressIds Is Nothing AndAlso .ManagementAuthorityAddressIds.Length > 0 Then
                                ReDim MAAddressIds(.ManagementAuthorityAddressIds.Length - 1)
                                Dim Index As Int32 = 0
                                For Each MAAddressId As Int32 In .ManagementAuthorityAddressIds
                                    MAAddressIds(Index) = MAAddressId
                                    Index += 1
                                Next MAAddressId
                            End If
                            If Not .Source Is Nothing AndAlso .Source.Length > 0 Then
                                ReDim SourceIds(.Source.Length - 1)
                                Dim Index As Int32 = 0
                                For Each SourceId As Int32 In .Source
                                    SourceIds(Index) = SourceId
                                    Index += 1
                                Next SourceId
                            End If
                            If Not .Purpose Is Nothing AndAlso .Purpose.Length > 0 Then
                                ReDim PurposeIds(.Purpose.Length - 1)
                                Dim Index As Int32 = 0
                                For Each PurposeId As Int32 In .Purpose
                                    PurposeIds(Index) = PurposeId
                                    Index += 1
                                Next PurposeId
                            End If
                            If Not .PartDerivative Is Nothing AndAlso .PartDerivative.Length > 0 Then
                                ReDim PDIds(.PartDerivative.Length - 1)
                                Dim Index As Int32 = 0
                                For Each PDId As Int32 In .PartDerivative
                                    PDIds(Index) = PDId
                                    Index += 1
                                Next PDId
                            End If
                        End If
                        If Not .UOM Is Nothing Then UOMId = .UOM.ID
                        Mass = .NetMass
                        Quantity = .Quantity
                        MassUsed = .NetMassUsed
                        If Not .DelegatedAuthority Is Nothing Then DelegationAuthorityId = .DelegatedAuthority.ID
                        If Not .BillOfLading Is Nothing AndAlso .BillOfLading.Length > 0 Then BillOfLading = .BillOfLading
                        If Not .PortOfEntry Is Nothing Then PortOfEntryId = .PortOfEntry.ID
                        If Not .CountryOfExport Is Nothing Then CountryOfExportId = .CountryOfExport.ID
                        If Not .CountryOfImport Is Nothing Then CountryOfImportId = .CountryOfImport.ID
                        If Not .CountryOfLastReExport Is Nothing Then CountryOfLastExportId = .CountryOfLastReExport.ID
                        If Not .CountryOfOrigin Is Nothing Then CountryOfOriginId = .CountryOfOrigin.ID
                        CountryOfLastExportNumber = .CountryOfLastReExportPermitNumber
                        CountryOfOriginNumber = .CountryOfOriginPermitNumber
                        CountryOfLastExportDate = .CountryOfLastReExportPermitIssueDate
                        CountryOfOriginDate = .CountryOfLastReExportPermitIssueDate
                        If Not .ManagementAuthorityCountry Is Nothing Then ManagementAuthorityCountryId = .ManagementAuthorityCountry.ID

                        If Not .CommonName Is Nothing AndAlso .CommonName.Length > 0 Then CommonName = .CommonName
                        If Not .AppliedForName Is Nothing AndAlso .AppliedForName.Length > 0 Then AppliedForName = .AppliedForName
                        If Not .ECAnnex Is Nothing AndAlso .ECAnnex.Length > 0 Then ECAnnex = .ECAnnex
                        If Not .CITESAppendix Is Nothing AndAlso .CITESAppendix.Length > 0 Then CITESAppendix = .CITESAppendix
                        Hybrid = .Hybrid
                        SpecimenQuantity = .SpecimenQuantity
                        SpecimenNetMass = .SpecimenNetMass
                        SpecimenGender = .SpecimenGender
                    End With
                End If
                GetCITESPermit(command, criteria.OrderColumn, criteria.OrderDirection, headerType, AppId, PartyId, DisplayName, StatusId, SAAdviceId, AssignedToRoleId, InspectorateAdviceId, IdMarkTypeId, IdMark, ScientificName, IsAgent, IsExporter, IsImporter, IsHolder, IsKeeper, DateApplicationReceived, DateIssued, DateLogged, DateOfReferral, _
                               DateReturned, DateUsed, DateAuthorised, DateComplete, DateRefused, _
                               DateCancelled, ExpiryDate, SubmittedBy, LoggedById, IssuedById, _
                               PartyAddressIds, LWTAddressIds, MailingAddressIds, DocumentTypeIds, _
                               MAAddressIds, SourceIds, PurposeIds, PDIds, UOMId, Mass, Quantity, _
                               MassUsed, LinkedToSeizureNotification, DelegationAuthorityId, _
                               UnderDerogation, ScientificAdviceId, HasAdHocScientificAdvice, _
                               SpecialConditionId, HasSpecialConditions, BillOfLading, DateSeized, _
                               PortOfEntryId, SNStatus, CountryOfExportId, CountryOfImportId, _
                               CountryOfOriginId, CountryOfOriginNumber, CountryOfOriginDate, _
                               CountryOfLastExportId, CountryOfLastExportNumber, CountryOfLastExportDate, _
                               ManagementAuthorityCountryId, CommonName, AppliedForName, ECAnnex, _
                               CITESAppendix, Hybrid, SpecimenQuantity, SpecimenNetMass, SpecimenGender)
            Else
                GetCITESPermit(command, criteria.OrderColumn, criteria.OrderDirection, headerType, AppId, PartyId, DisplayName, StatusId, SAAdviceId, AssignedToRoleId, InspectorateAdviceId, IdMarkTypeId, IdMark, ScientificName, IsAgent, IsExporter, IsImporter, IsHolder, IsKeeper, DateApplicationReceived, DateIssued, DateLogged, DateOfReferral)
            End If
            'MLD 11/4/5 in case of problems with the GetEntitySet call below, uncomment these lines to see the command parameters
            'For Each param As SqlClient.SqlParameter In command.Parameters
            '    Dim name As String = param.ParameterName
            '    Dim val As Object = param.Value
            '    Dim sval As String = val.ToString
            '    Dim xxx As String = name       '***** put a breakpoint here
            'Next
            Return service.GetEntitySet(command, entitySetType)
        End Function

        Private Shared Sub GetCITESPermit(ByRef command As System.Data.SqlClient.SqlCommand, _
                    ByVal orderCol As Int32, ByVal orderDirection As Search.ApplicationSearchCriteriaBase.SortDirection, _
                    ByVal headerType As SearchResults.ColumnHeaderType, _
                    ByVal applicationId As Int32, _
                    ByVal partyId As Int32, ByVal displayName As String, ByVal statusId As Int32(), _
                    ByVal sAAdviceId As Int32, ByVal assignedToRoleId() As Int32, _
                    ByVal inspectorateAdviceId As Int32, ByVal idMarkTypeId As Int32, _
                    ByVal idMark As Int32, ByVal scientificName As String, _
                    ByVal isAgent As Boolean, ByVal isExporter As Boolean, _
                    ByVal isImporter As Boolean, ByVal isHolder As Boolean, _
                    ByVal isKeeper As Boolean, ByVal dateApplicationReceived As Application.Search.DateRange, _
                    ByVal dateIssued As Application.Search.DateRange, _
                    ByVal dateLogged As Application.Search.DateRange, _
                    ByVal dateOfReferral As Application.Search.DateRange, _
                    ByVal dateReturned As Application.Search.DateRange, _
                    ByVal dateUsed As Application.Search.DateRange, _
                    ByVal dateAuthorised As Application.Search.DateRange, _
                    ByVal dateComplete As Application.Search.DateRange, _
                    ByVal dateRefused As Application.Search.DateRange, _
                    ByVal dateCancelled As Application.Search.DateRange, _
                    ByVal expiryDate As Application.Search.DateRange, _
                    ByVal submittedBy As ApplicationSearchCriteriaDetailed_Customer.SubmittedByWho, _
                    ByVal loggedById As Int32, ByVal issuedById As Int32, ByVal partyAddressIds As Int32(), _
                    ByVal lWTAddressIds As Int32(), ByVal mailingAddressIds As Int32(), _
                    ByVal documentTypeIds As Int32(), ByVal mAAddressIds As Int32(), _
                    ByVal sourceIds As Int32(), ByVal purposeIds As Int32(), _
                    ByVal pDIds As Int32(), ByVal uOMId As Int32, ByVal mass As NumberRange, _
                    ByVal quantity As NumberRange, ByVal massUsed As NumberRange, _
                    ByVal linkedToSeizureNotification As Search.ApplicationSearchCriteriaDetailed_Customer.Logical, _
                    ByVal delegationAuthorityId As Int32, ByVal underDerogation As Search.ApplicationSearchCriteriaDetailed_Customer.Logical, _
                    ByVal scientificAdviceId As Int32, ByVal hasAdHocScientificAdvice As Search.ApplicationSearchCriteriaDetailed_Customer.Logical, _
                    ByVal specialConditionId As Int32, ByVal hasSpecialConditions As Search.ApplicationSearchCriteriaDetailed_Customer.Logical, _
                    ByVal billOfLading As String, ByVal dateSeized As DateRange, ByVal portOfEntryId As Int32, _
                    ByVal sNStatus As Search.ApplicationSearchCriteriaDetailed.SNStatusList, _
                    ByVal countryOfExportId As Int32, ByVal countryOfImportId As Int32, _
                    ByVal countryOfOriginId As Int32, ByVal countryOfOriginNumber As Int32, ByVal countryOfOriginDate As DateRange, _
                    ByVal countryOfLastExportId As Int32, ByVal countryOfLastExportNumber As Int32, ByVal countryOfLastExportDate As DateRange, _
                    ByVal managementAuthorityCountryId As Int32, ByVal commonName As String, ByVal appliedForName As String, _
                    ByVal eCAnnex As String, ByVal cITESAppendix As String, ByVal hybrid As Search.ApplicationSearchCriteriaDetailed_Customer.Logical, _
                    ByVal specimenQuantity As Int32, ByVal specimenNetMass As Decimal, ByVal specimenGender As Search.ApplicationSearchCriteriaDetailed_Customer.Gender)


            ' get the common data
            GetCITESPermit(command, orderCol, orderDirection, headerType, applicationId, partyId, displayName, statusId, _
                           sAAdviceId, assignedToRoleId, inspectorateAdviceId, idMarkTypeId, idMark, scientificName, _
                           isAgent, isExporter, isImporter, isHolder, isKeeper, dateApplicationReceived, _
                           dateIssued, dateLogged, dateOfReferral)

            'get the detailed data.
            If Not dateReturned Is Nothing Then
                If Not dateReturned.From Is Nothing AndAlso dateReturned.From.toString.Length > 0 Then command.Parameters.Add("@DateReturned_From", System.Data.SqlDbType.DateTime).Value = dateReturned.From
                If Not dateReturned.To Is Nothing AndAlso dateReturned.To.toString.Length > 0 Then command.Parameters.Add("@DateReturned_To", System.Data.SqlDbType.DateTime).Value = dateReturned.To
            End If
            If Not dateUsed Is Nothing Then
                If Not dateUsed.From Is Nothing AndAlso dateUsed.From.toString.Length > 0 Then command.Parameters.Add("@DateUsed_From", System.Data.SqlDbType.DateTime).Value = dateUsed.From
                If Not dateUsed.To Is Nothing AndAlso dateUsed.To.toString.Length > 0 Then command.Parameters.Add("@DateUsed_To", System.Data.SqlDbType.DateTime).Value = dateUsed.To
            End If
            If Not dateAuthorised Is Nothing Then
                If Not dateAuthorised.From Is Nothing AndAlso dateAuthorised.From.toString.Length > 0 Then command.Parameters.Add("@DateAuthorised_From", System.Data.SqlDbType.DateTime).Value = dateAuthorised.From
                If Not dateAuthorised.To Is Nothing AndAlso dateAuthorised.To.toString.Length > 0 Then command.Parameters.Add("@DateAuthorised_To", System.Data.SqlDbType.DateTime).Value = dateAuthorised.To
            End If
            If Not dateUsed Is Nothing Then
                If Not dateUsed.From Is Nothing AndAlso dateUsed.From.toString.Length > 0 Then command.Parameters.Add("@DateUsed_From", System.Data.SqlDbType.DateTime).Value = dateUsed.From
                If Not dateUsed.To Is Nothing AndAlso dateUsed.To.toString.Length > 0 Then command.Parameters.Add("@DateUsed_To", System.Data.SqlDbType.DateTime).Value = dateUsed.To
            End If
            If Not dateRefused Is Nothing Then
                If Not dateRefused.From Is Nothing AndAlso dateRefused.From.toString.Length > 0 Then command.Parameters.Add("@DateRefused_From", System.Data.SqlDbType.DateTime).Value = dateRefused.From
                If Not dateRefused.To Is Nothing AndAlso dateRefused.To.toString.Length > 0 Then command.Parameters.Add("@DateRefused_To", System.Data.SqlDbType.DateTime).Value = dateRefused.To
            End If
            If Not dateCancelled Is Nothing Then
                If Not dateCancelled.From Is Nothing AndAlso dateCancelled.From.toString.Length > 0 Then command.Parameters.Add("@DateCancelled_From", System.Data.SqlDbType.DateTime).Value = dateCancelled.From
                If Not dateCancelled.To Is Nothing AndAlso dateCancelled.To.toString.Length > 0 Then command.Parameters.Add("@DateCancelled_To", System.Data.SqlDbType.DateTime).Value = dateCancelled.To
            End If
            If Not expiryDate Is Nothing Then
                If Not expiryDate.From Is Nothing AndAlso expiryDate.From.toString.Length > 0 Then command.Parameters.Add("@ExpiryDate_From", System.Data.SqlDbType.DateTime).Value = expiryDate.From
                If Not expiryDate.To Is Nothing AndAlso expiryDate.To.toString.Length > 0 Then command.Parameters.Add("@ExpiryDate_To", System.Data.SqlDbType.DateTime).Value = expiryDate.To
            End If
            If Not submittedBy = ApplicationSearchCriteriaDetailed_Customer.SubmittedByWho.Either Then
                command.Parameters.Add("@SubmittedBy", System.Data.SqlDbType.Int).Value = CType(submittedBy, Int32)
            End If
            If loggedById > 0 Then
                command.Parameters.Add("@LoggedById", System.Data.SqlDbType.Int).Value = loggedById
            End If
            If issuedById > 0 Then
                command.Parameters.Add("@IssuedById", System.Data.SqlDbType.Int).Value = issuedById
            End If
            SetParameterString(command, "@PartyAddressIds", partyAddressIds)
            SetParameterString(command, "@LWTAddressIds", lWTAddressIds)
            SetParameterString(command, "@MailingAddressIds", mailingAddressIds)
            SetParameterString(command, "@DocumentTypeIds", documentTypeIds)
            SetParameterString(command, "@MAAddressIds", mAAddressIds)
            SetParameterString(command, "@SourceIds", sourceIds)
            SetParameterString(command, "@PurposeIds", purposeIds)
            SetParameterString(command, "@PDIds", pDIds)
            If uOMId > 0 Then
                command.Parameters.Add("@UOMId", System.Data.SqlDbType.Int).Value = uOMId
            End If
            SetParameterNumberRange(command, "@Mass", mass)
            SetParameterNumberRange(command, "@Quantity", quantity)
            SetParameterNumberRange(command, "@MassUsed", massUsed)
            Select Case linkedToSeizureNotification
                Case ApplicationSearchCriteriaDetailed_Customer.Logical.Yes
                    command.Parameters.Add("@LinkedToSeizureNotification", System.Data.SqlDbType.Bit).Value = 1
                Case ApplicationSearchCriteriaDetailed_Customer.Logical.No
                    command.Parameters.Add("@LinkedToSeizureNotification", System.Data.SqlDbType.Bit).Value = 0
            End Select
            If delegationAuthorityId > 0 Then
                command.Parameters.Add("@DelegationAuthorityId", System.Data.SqlDbType.Int).Value = delegationAuthorityId
            End If
            Select Case underDerogation
                Case ApplicationSearchCriteriaDetailed_Customer.Logical.Yes
                    command.Parameters.Add("@UnderDerogation", System.Data.SqlDbType.Bit).Value = 1
                Case ApplicationSearchCriteriaDetailed_Customer.Logical.No
                    command.Parameters.Add("@UnderDerogation", System.Data.SqlDbType.Bit).Value = 0
            End Select
            If scientificAdviceId > 0 Then
                command.Parameters.Add("@ScientificAdviceId", System.Data.SqlDbType.Int).Value = scientificAdviceId
            End If
            Select Case hasAdHocScientificAdvice
                Case ApplicationSearchCriteriaDetailed_Customer.Logical.Yes
                    command.Parameters.Add("@HasAdHocScientificAdvice", System.Data.SqlDbType.Bit).Value = 1
                Case ApplicationSearchCriteriaDetailed_Customer.Logical.No
                    command.Parameters.Add("@HasAdHocScientificAdvice", System.Data.SqlDbType.Bit).Value = 0
            End Select
            If specialConditionId > 0 Then
                command.Parameters.Add("@SpecialConditionId", System.Data.SqlDbType.Int).Value = specialConditionId
            End If
            Select Case hasSpecialConditions
                Case ApplicationSearchCriteriaDetailed_Customer.Logical.Yes
                    command.Parameters.Add("@HasAdHocSpecialConditions", System.Data.SqlDbType.Bit).Value = 1
                Case ApplicationSearchCriteriaDetailed_Customer.Logical.No
                    command.Parameters.Add("@HasAdHocSpecialConditions", System.Data.SqlDbType.Bit).Value = 0
            End Select
            If Not billOfLading Is Nothing AndAlso billOfLading.Length > 0 Then
                command.Parameters.Add("@BillOfLading", System.Data.SqlDbType.VarChar, 50).Value = ParseParams(billOfLading)
            End If
            If Not dateSeized Is Nothing Then
                If Not dateSeized.From Is Nothing AndAlso dateSeized.From.toString.Length > 0 Then command.Parameters.Add("@DateSeized_From", System.Data.SqlDbType.DateTime).Value = dateSeized.From
                If Not dateSeized.To Is Nothing AndAlso dateSeized.To.toString.Length > 0 Then command.Parameters.Add("@DateSeized_To", System.Data.SqlDbType.DateTime).Value = dateSeized.To
            End If
            If portOfEntryId > 0 Then
                command.Parameters.Add("@PortOfEntryId", System.Data.SqlDbType.Int).Value = portOfEntryId
            End If
            Select Case sNStatus
                Case ApplicationSearchCriteriaDetailed.SNStatusList.Active
                    command.Parameters.Add("@SNStatus", System.Data.SqlDbType.Bit).Value = 1
                Case ApplicationSearchCriteriaDetailed.SNStatusList.Inactive
                    command.Parameters.Add("@SNStatus", System.Data.SqlDbType.Bit).Value = 0
            End Select
            If countryOfExportId > 0 Then
                command.Parameters.Add("@CountryOfExportId", System.Data.SqlDbType.Int).Value = countryOfExportId
            End If
            If countryOfImportId > 0 Then
                command.Parameters.Add("@CountryOfimportId", System.Data.SqlDbType.Int).Value = countryOfImportId
            End If
            If countryOfOriginId > 0 Then
                command.Parameters.Add("@CountryOfOriginId", System.Data.SqlDbType.Int).Value = countryOfOriginId
            End If
            If countryOfLastExportId > 0 Then
                command.Parameters.Add("@CountryOfLastExportId", System.Data.SqlDbType.Int).Value = countryOfLastExportId
            End If
            If countryOfOriginNumber > 0 Then
                command.Parameters.Add("@CountryOfOriginNumber", System.Data.SqlDbType.Int).Value = countryOfOriginNumber
            End If
            If countryOfLastExportNumber > 0 Then
                command.Parameters.Add("@CountryOfLastExportNumber", System.Data.SqlDbType.Int).Value = countryOfLastExportNumber
            End If
            If Not countryOfOriginDate Is Nothing Then
                If Not countryOfOriginDate.From Is Nothing AndAlso countryOfOriginDate.From.toString.Length > 0 Then command.Parameters.Add("@CountryOfOriginDate_From", System.Data.SqlDbType.DateTime).Value = countryOfOriginDate.From
                If Not countryOfOriginDate.To Is Nothing AndAlso countryOfOriginDate.To.toString.Length > 0 Then command.Parameters.Add("@CountryOfOriginDate_To", System.Data.SqlDbType.DateTime).Value = countryOfOriginDate.To
            End If
            If Not countryOfLastExportDate Is Nothing Then
                If Not countryOfLastExportDate.From Is Nothing AndAlso countryOfLastExportDate.From.toString.Length > 0 Then command.Parameters.Add("@CountryOfLastExportDate_From", System.Data.SqlDbType.DateTime).Value = countryOfLastExportDate.From
                If Not countryOfLastExportDate.To Is Nothing AndAlso countryOfLastExportDate.To.toString.Length > 0 Then command.Parameters.Add("@CountryOfLastExportDate_To", System.Data.SqlDbType.DateTime).Value = countryOfLastExportDate.To
            End If
            If managementAuthorityCountryId > 0 Then
                command.Parameters.Add("@ManagementAuthorityCountryId", System.Data.SqlDbType.Int).Value = managementAuthorityCountryId
            End If
            If Not commonName Is Nothing AndAlso commonName.Length > 0 Then
                command.Parameters.Add("@CommonName", System.Data.SqlDbType.VarChar, 50).Value = ParseParams(commonName)
            End If
            If Not appliedForName Is Nothing AndAlso appliedForName.Length > 0 Then
                command.Parameters.Add("@AppliedForName", System.Data.SqlDbType.VarChar, 50).Value = ParseParams(appliedForName)
            End If
            If Not eCAnnex Is Nothing AndAlso eCAnnex.Length > 0 Then
                command.Parameters.Add("@ECAnnex", System.Data.SqlDbType.VarChar, 50).Value = ParseParams(eCAnnex)
            End If
            If Not cITESAppendix Is Nothing AndAlso cITESAppendix.Length > 0 Then
                command.Parameters.Add("@CITESAppendix", System.Data.SqlDbType.VarChar, 50).Value = ParseParams(cITESAppendix)
            End If
            Select Case hybrid
                Case ApplicationSearchCriteriaDetailed_Customer.Logical.Yes
                    command.Parameters.Add("@Hybrid", System.Data.SqlDbType.Bit).Value = 1
                Case ApplicationSearchCriteriaDetailed_Customer.Logical.No
                    command.Parameters.Add("@Hybrid", System.Data.SqlDbType.Bit).Value = 0
            End Select
            If specimenQuantity > 0 Then
                command.Parameters.Add("@SpecimenQuantity", System.Data.SqlDbType.Int).Value = specimenQuantity
            End If
            If specimenNetMass > 0 Then
                command.Parameters.Add("@SpecimenNetMass", System.Data.SqlDbType.Decimal).Value = specimenQuantity
            End If
            Select Case specimenGender
                Case ApplicationSearchCriteriaDetailed_Customer.Gender.Male
                    command.Parameters.Add("@SpecimenGender", System.Data.SqlDbType.Bit).Value = 1
                Case ApplicationSearchCriteriaDetailed_Customer.Gender.Female
                    command.Parameters.Add("@SpecimenGender", System.Data.SqlDbType.Bit).Value = 0
            End Select
        End Sub

        Private Shared Sub SetParameterNumberRange(ByRef command As SqlClient.SqlCommand, ByVal parameterName As String, ByVal range As NumberRange)
            If Not range Is Nothing Then
                Select Case range.Range
                    Case NumberRangeList.Equal_To
                        command.Parameters.Add(parameterName, System.Data.SqlDbType.VarChar, 255).Value = " = " & range.Number.toString
                    Case NumberRangeList.Greater_Than
                        command.Parameters.Add(parameterName, System.Data.SqlDbType.VarChar, 255).Value = " > " & range.Number.toString
                    Case NumberRangeList.Less_Than
                        command.Parameters.Add(parameterName, System.Data.SqlDbType.VarChar, 255).Value = " < " & range.Number.toString
                End Select
            End If
        End Sub

        Private Shared Sub SetParameterString(ByRef command As SqlClient.SqlCommand, ByVal parameterName As String, ByVal ids As Int32())
            If Not ids Is Nothing AndAlso ids.Length > 0 Then
                Dim IDStr As String
                If ids.Length = 1 Then
                    IDStr = " = " & ids(0).ToString
                Else
                    IDStr = " In ("
                    Dim AddComma As Boolean = False
                    For Each id As Int32 In ids
                        If AddComma Then IDStr &= ", " Else AddComma = True
                        IDStr &= id.ToString
                    Next id
                    IDStr &= ")"
                End If
                command.Parameters.Add(parameterName, System.Data.SqlDbType.VarChar, 255).Value = IDStr
            End If
        End Sub

        Private Shared Sub GetCITESPermit(ByRef command As System.Data.SqlClient.SqlCommand, _
                            ByVal orderCol As Int32, ByVal orderDirection As Search.ApplicationSearchCriteriaBase.SortDirection, _
                            ByVal headerType As SearchResults.ColumnHeaderType, _
                            ByVal applicationId As Int32, _
                            ByVal partyId As Int32, ByVal displayName As String, ByVal statusId As Int32(), _
                            ByVal sAAdviceId As Int32, ByVal assignedToRoleId() As Int32, _
                            ByVal inspectorateAdviceId As Int32, ByVal idMarkTypeId As Int32, _
                            ByVal idMark As Int32, ByVal scientificName As String, _
                            ByVal isAgent As Boolean, ByVal isExporter As Boolean, _
                            ByVal isImporter As Boolean, ByVal isHolder As Boolean, _
                            ByVal isKeeper As Boolean, ByVal dateApplicationReceived As Application.Search.DateRange, _
                            ByVal dateIssued As Application.Search.DateRange, _
                            ByVal dateLogged As Application.Search.DateRange, _
                            ByVal dateOfReferral As Application.Search.DateRange)
            'parameters...
            Dim OrderSQL As String = Application.Search.SearchResults.OrderColSQL(headerType, orderCol, orderDirection)


            If Not OrderSQL Is Nothing AndAlso OrderSQL.Length > 0 Then
                command.Parameters.Add("@OrderSQL", System.Data.SqlDbType.VarChar, 100).Value = OrderSQL
            End If

            If applicationId > 0 Then
                command.Parameters.Add("@ApplicationId", System.Data.SqlDbType.Int).Value = applicationId
            End If

            If partyId > 0 Then
                command.Parameters.Add("@PartyId", System.Data.SqlDbType.Int).Value = partyId
            End If

            If Not displayName Is Nothing AndAlso displayName.Length > 0 Then
                command.Parameters.Add("@DisplayName", System.Data.SqlDbType.VarChar, 255).Value = ParseParams(displayName)
            End If

            If sAAdviceId > 0 Then
                command.Parameters.Add("@SAAdviceId", System.Data.SqlDbType.Int).Value = sAAdviceId
            End If

            If Not statusId Is Nothing AndAlso statusId.Length > 0 Then
                Dim StatusStr As String
                If statusId.Length = 1 Then
                    StatusStr = " = " & statusId(0).ToString
                Else
                    StatusStr = " In ("
                    Dim AddComma As Boolean = False
                    For Each id As Int32 In statusId
                        If AddComma Then StatusStr &= ", " Else AddComma = True
                        StatusStr &= id.ToString
                    Next id
                    StatusStr &= ")"
                End If
                command.Parameters.Add("@StatusId", System.Data.SqlDbType.VarChar, 255).Value = StatusStr
            End If

            If Not assignedToRoleId Is Nothing AndAlso assignedToRoleId.Length > 0 Then
                Dim AssignedToRoleStr As String
                If assignedToRoleId.Length = 1 Then
                    AssignedToRoleStr = " = " & assignedToRoleId(0).ToString
                Else
                    AssignedToRoleStr = " In ("
                    Dim AddComma As Boolean = False
                    For Each id As Int32 In assignedToRoleId
                        If AddComma Then AssignedToRoleStr &= ", " Else AddComma = True
                        AssignedToRoleStr &= id.ToString
                    Next id
                    AssignedToRoleStr &= ")"
                End If
                command.Parameters.Add("@AssignedToRoleId", System.Data.SqlDbType.VarChar, 255).Value = AssignedToRoleStr
            End If

            If inspectorateAdviceId > 0 Then
                command.Parameters.Add("@InspectorateAdviceId", System.Data.SqlDbType.Int).Value = inspectorateAdviceId
            End If

            If idMarkTypeId > 0 Then
                command.Parameters.Add("@IdMarkTypeId", System.Data.SqlDbType.Int).Value = idMarkTypeId
            End If

            If idMark > 0 Then
                command.Parameters.Add("@IdMark", System.Data.SqlDbType.Int).Value = idMark
            End If

            If Not scientificName Is Nothing AndAlso scientificName.Length > 0 Then
                command.Parameters.Add("@ScientificName", System.Data.SqlDbType.VarChar, 50).Value = ParseParams(scientificName)
            End If

            If isAgent Then command.Parameters.Add("@IsAgent", System.Data.SqlDbType.Bit).Value = 1
            If isExporter Then command.Parameters.Add("@IsExporter", System.Data.SqlDbType.Bit).Value = 1
            If isImporter Then command.Parameters.Add("@IsImporter", System.Data.SqlDbType.Bit).Value = 1
            If isKeeper Then command.Parameters.Add("@IsKeeper", System.Data.SqlDbType.Bit).Value = 1
            If isHolder Then command.Parameters.Add("@IsHolder", System.Data.SqlDbType.Bit).Value = 1

            If Not dateApplicationReceived Is Nothing Then
                If Not dateApplicationReceived.From Is Nothing AndAlso dateApplicationReceived.From.toString.Length > 0 Then command.Parameters.Add("@DateApplicationReceived_From", System.Data.SqlDbType.DateTime).Value = dateApplicationReceived.From
                If Not dateApplicationReceived.To Is Nothing AndAlso dateApplicationReceived.To.toString.Length > 0 Then command.Parameters.Add("@DateApplicationReceived_To", System.Data.SqlDbType.DateTime).Value = dateApplicationReceived.To
            End If

            If Not dateIssued Is Nothing Then
                If Not dateIssued.From Is Nothing AndAlso dateIssued.From.toString.Length > 0 Then command.Parameters.Add("@DateIssued_From", System.Data.SqlDbType.DateTime).Value = dateIssued.From
                If Not dateIssued.To Is Nothing AndAlso dateIssued.To.toString.Length > 0 Then command.Parameters.Add("@DateIssued_To", System.Data.SqlDbType.DateTime).Value = dateIssued.To
            End If

            If Not dateLogged Is Nothing Then
                If Not dateLogged.From Is Nothing AndAlso dateLogged.From.toString.Length > 0 Then command.Parameters.Add("@DateLogged_From", System.Data.SqlDbType.DateTime).Value = dateLogged.From
                If Not dateLogged.To Is Nothing AndAlso dateLogged.To.toString.Length > 0 Then command.Parameters.Add("@DateLogged_To", System.Data.SqlDbType.DateTime).Value = dateLogged.To
            End If

            If Not dateOfReferral Is Nothing Then
                If Not dateOfReferral.From Is Nothing AndAlso dateOfReferral.From.toString.Length > 0 Then command.Parameters.Add("@DateOfReferral_From", System.Data.SqlDbType.DateTime).Value = dateOfReferral.From
                If Not dateOfReferral.To Is Nothing AndAlso dateOfReferral.To.toString.Length > 0 Then command.Parameters.Add("@DateOfReferral_To", System.Data.SqlDbType.DateTime).Value = dateOfReferral.To
            End If

            Select Case headerType
                Case SearchResults.ColumnHeaderType.Four
                    command.Parameters.Add("@ImportNotification", System.Data.SqlDbType.Bit).Value = 1
                Case SearchResults.ColumnHeaderType.Five
                    command.Parameters.Add("@SeizureNotification", System.Data.SqlDbType.Bit).Value = 1
            End Select
        End Sub

        Private Function GetFilteredData() As EnterpriseObjects.EntitySet

        End Function

        Private Shared Function GetPermitData_Permits(ByVal criteria As ApplicationSearchCriteriaBase, ByVal mode As SearchMode, ByVal group As SearchGroup) As SearchResults
            ' Load the data
            Dim HeaderType As SearchResults.ColumnHeaderType
            Select Case group
                Case SearchGroup.CaseOfficer, SearchGroup.Other
                    Select Case mode
                        Case SearchMode.Permit, SearchMode.Permit_With_Multiples
                            HeaderType = SearchResults.ColumnHeaderType.One
                        Case SearchMode.Application
                            HeaderType = SearchResults.ColumnHeaderType.Two
                        Case SearchMode.Species
                            HeaderType = SearchResults.ColumnHeaderType.Three
                    End Select
                Case SearchGroup.KewJNCC
                    Select Case mode
                        Case SearchMode.Permit, SearchMode.Permit_With_Multiples
                            HeaderType = SearchResults.ColumnHeaderType.Six
                        Case SearchMode.Application
                            HeaderType = SearchResults.ColumnHeaderType.Seven
                        Case SearchMode.Species
                            HeaderType = SearchResults.ColumnHeaderType.Eight
                    End Select
                Case SearchGroup.Inspectorate
                    Select Case mode
                        Case SearchMode.Permit, SearchMode.Permit_With_Multiples
                            HeaderType = SearchResults.ColumnHeaderType.Nine
                        Case SearchMode.Application
                            HeaderType = SearchResults.ColumnHeaderType.Ten
                        Case SearchMode.Species
                            HeaderType = SearchResults.ColumnHeaderType.Eleven
                    End Select
                Case SearchGroup.Customer
                    Select Case mode
                        Case SearchMode.Permit, SearchMode.Permit_With_Multiples
                            HeaderType = SearchResults.ColumnHeaderType.Fourteen
                        Case SearchMode.Application
                            HeaderType = SearchResults.ColumnHeaderType.Fifteen
                        Case SearchMode.Species
                            HeaderType = SearchResults.ColumnHeaderType.Sixteen
                    End Select
            End Select

            Dim Data As DataObjects.Views.EntitySet.SearchCITESPermitSet = CType(GetCITESPermit(criteria, HeaderType, "vSearchCITESPermit", SearchType.CITES, DataObjects.Views.Entity.SearchCITESPermit.ServiceObject, GetType(DataObjects.Views.EntitySet.SearchCITESPermitSet)), DataObjects.Views.EntitySet.SearchCITESPermitSet)
            Dim DataItems As New ArrayList '(Data.Count - 1) As Data.BaseSearchData
            'ReDim ReturnObj.Permits(data.Count - 1)
            Dim PermitList As New Hashtable

            For Each item As DataObjects.Views.Entity.SearchCITESPermit In Data
                Dim BOItem As Search.Data.BaseCITESPermitSearchData 'Search.Data.CITESPermitSearchDataCaseOfficer_ByPermit

                Dim Key As Object
                Select Case mode
                    Case SearchMode.Permit_With_Multiples
                        Key = Nothing
                    Case SearchMode.Permit
                        Key = item.DisplayNumber
                    Case SearchMode.Application
                        Key = item.ApplicationId
                    Case SearchMode.Species
                        Key = item.SpecieId
                End Select

                If Key Is Nothing OrElse Not PermitList.ContainsKey(Key) Then
                    Select Case group
                        Case SearchGroup.CaseOfficer, SearchGroup.Other
                            Select Case mode
                                Case SearchMode.Permit_With_Multiples
                                    BOItem = New Search.Data.CITESPermitSearchDataCaseOfficer_ByPermit
                                Case SearchMode.Permit
                                    BOItem = New Search.Data.CITESPermitSearchDataCaseOfficer_ByPermit
                                    BOItem.Count = 1
                                    If Not Key Is Nothing Then PermitList.Add(Key, BOItem)
                                Case SearchMode.Application
                                    BOItem = New Search.Data.CITESPermitSearchDataCaseOfficer_ByApplication
                                    BOItem.Count = 1
                                    PermitList.Add(Key, BOItem)
                                Case SearchMode.Species
                                    BOItem = New Search.Data.CITESPermitSearchDataCaseOfficer_BySpecies
                                    BOItem.Count = 1
                                    PermitList.Add(Key, BOItem)
                            End Select
                        Case SearchGroup.KewJNCC
                            Select Case mode
                                Case SearchMode.Permit_With_Multiples
                                    BOItem = New Search.Data.CITESPermitSearchDataKewJNCC_ByPermit
                                Case SearchMode.Permit
                                    BOItem = New Search.Data.CITESPermitSearchDataKewJNCC_ByPermit
                                    BOItem.Count = 1
                                    If Not Key Is Nothing Then PermitList.Add(Key, BOItem)
                                Case SearchMode.Application
                                    BOItem = New Search.Data.CITESPermitSearchDataKewJNCC_ByApplication
                                    BOItem.Count = 1
                                    PermitList.Add(Key, BOItem)
                                Case SearchMode.Species
                                    BOItem = New Search.Data.CITESPermitSearchDataKewJNCC_BySpecies
                                    BOItem.Count = 1
                                    PermitList.Add(Key, BOItem)
                            End Select
                        Case SearchGroup.Inspectorate
                            Select Case mode
                                Case SearchMode.Permit_With_Multiples
                                    BOItem = New Search.Data.CITESPermitSearchDataInspectorate_ByPermit
                                Case SearchMode.Permit
                                    BOItem = New Search.Data.CITESPermitSearchDataInspectorate_ByPermit
                                    BOItem.Count = 1
                                    If Not Key Is Nothing Then PermitList.Add(Key, BOItem)
                                Case SearchMode.Application
                                    BOItem = New Search.Data.CITESPermitSearchDataInspectorate_ByApplication
                                    BOItem.Count = 1
                                    PermitList.Add(Key, BOItem)
                                Case SearchMode.Species
                                    BOItem = New Search.Data.CITESPermitSearchDataInspectorate_BySpecies
                                    BOItem.Count = 1
                                    PermitList.Add(Key, BOItem)
                            End Select
                        Case SearchGroup.Customer
                            Select Case mode
                                Case SearchMode.Permit_With_Multiples
                                    BOItem = New Search.Data.CITESPermitSearchDataCustomer_ByPermit
                                Case SearchMode.Permit
                                    BOItem = New Search.Data.CITESPermitSearchDataCustomer_ByPermit
                                    BOItem.Count = 1
                                    If Not Key Is Nothing Then PermitList.Add(Key, BOItem)
                                Case SearchMode.Application
                                    BOItem = New Search.Data.CITESPermitSearchDataCustomer_ByApplication
                                    BOItem.Count = 1
                                    PermitList.Add(Key, BOItem)
                                Case SearchMode.Species
                                    BOItem = New Search.Data.CITESPermitSearchDataCustomer_BySpecies
                                    BOItem.Count = 1
                                    PermitList.Add(Key, BOItem)
                            End Select
                    End Select
                Else
                    Select Case mode
                        Case SearchMode.Permit
                            If item.PermitStatusId = Application.BOPermitInfo.PermitStatusTypes.Issued OrElse _
                               item.PermitStatusId = Application.BOPermitInfo.PermitStatusTypes.UpdatedSemiComplete OrElse _
                               item.PermitStatusId = Application.BOPermitInfo.PermitStatusTypes.ReturnedUnused OrElse _
                               item.PermitStatusId = Application.BOPermitInfo.PermitStatusTypes.ReturnedUsed Then
                                BOItem = CType(PermitList.Item(Key), Search.Data.BaseCITESPermitSearchData)
                                BOItem.Count += 1
                            Else
                                BOItem = Nothing
                            End If
                        Case SearchMode.Application
                            BOItem = CType(PermitList.Item(Key), Search.Data.BaseCITESPermitSearchData)
                            BOItem.Count += 1
                        Case SearchMode.Species
                            BOItem = CType(PermitList.Item(Key), Search.Data.BaseCITESPermitSearchData)
                            BOItem.Count += 1
                    End Select
                End If

                If Not BOItem Is Nothing Then
                    With item
                        Select Case mode
                            Case SearchMode.Permit_With_Multiples
                                If Not .IsDisplayNumberSeqNull Then CType(BOItem, Data.IPermitId).PermitId = .DisplayNumberSeq
                            Case SearchMode.Permit
                                If Not .IsDisplayNumberNull Then
                                    If BOItem.Count = 1 Then
                                        CType(BOItem, Data.IPermitId).PermitId = .DisplayNumber
                                    Else
                                        CType(BOItem, Data.IPermitId).PermitId = String.Concat(.DisplayNumber, "x(", BOItem.Count, ")")
                                    End If
                                End If
                            Case SearchMode.Application
                                BOItem.ApplicationId = .ApplicationId
                            Case SearchMode.Species
                                'ctype(BOItem,Search.Data.CITESPermitSearchDataCaseOfficer_BySpecies).s
                        End Select

                        If Not .IsDisplayNameNull Then BOItem.PartyName = .DisplayName
                        If Not .IsPartyIdNull Then BOItem.PartyId = .PartyId.ToString
                        BOItem.PermitType = .PermitType
                        BOItem.PermitTypeId = CType(.PermitTypeId, Application.ApplicationTypes)

                        If Not BOItem.getType.GetInterface(GetType(Data.IDateReceived).ToString) Is Nothing Then
                            If Not .IsOrderDateNull Then CType(BOItem, Data.IDateReceived).DateReceived = .OrderDate.ToShortDateString
                        End If
                        If Not BOItem.getType.GetInterface(GetType(Data.IISOCode).ToString) Is Nothing Then
                            If Not .IsISOCodeNull Then CType(BOItem, Data.IISOCode).ISOCode = .ISOCode
                        End If
                        If Not BOItem.getType.GetInterface(GetType(Data.IISOCodeDescription).ToString) Is Nothing Then
                            If Not .IsISOCodeDescriptionNull Then CType(BOItem, Data.IISOCodeDescription).ISOCodeDescription = .ISOCodeDescription
                        End If
                        If Not BOItem.getType.GetInterface(GetType(Data.IAssignedTo).ToString) Is Nothing Then
                            If mode = SearchMode.Permit_With_Multiples OrElse BOItem.Count = 1 Then
                                If Not .IsAssignedToNull Then CType(BOItem, Data.IAssignedTo).AssignedTo = .AssignedTo
                            Else
                                If Not .IsAssignedToNull AndAlso Compare(CType(BOItem, Data.IAssignedTo).AssignedTo, .AssignedTo) Then CType(BOItem, Data.IAssignedTo).AssignedTo = "*"
                            End If
                        End If
                        If Not BOItem.getType.GetInterface(GetType(Data.IPD).ToString) Is Nothing Then
                            If Not .IsPDNull Then CType(BOItem, Data.IPD).PD = .PD
                        End If
                        If Not BOItem.getType.GetInterface(GetType(Data.IPaid).ToString) Is Nothing Then
                            If mode = SearchMode.Permit_With_Multiples OrElse BOItem.Count = 1 Then
                                If Not .IsPaidNull Then CType(BOItem, Data.IPaid).Paid = .Paid.ToString
                            Else
                                If Not .IsPaidNull AndAlso Compare(CType(BOItem, Data.IPaid).Paid, .Paid.ToString) Then CType(BOItem, Data.IPaid).Paid = "*"
                            End If
                        End If
                        If Not BOItem.getType.GetInterface(GetType(Data.ISAAdvice).ToString) Is Nothing Then
                            If mode = SearchMode.Permit_With_Multiples OrElse BOItem.Count = 1 Then
                                If Not .IsSAAdviceNull Then CType(BOItem, Data.ISAAdvice).SAAdvice = .SAAdvice
                            Else
                                If Not .IsSAAdviceNull AndAlso Compare(CType(BOItem, Data.ISAAdvice).SAAdvice, .SAAdvice) Then CType(BOItem, Data.ISAAdvice).SAAdvice = "*"
                            End If
                        End If
                        If Not BOItem.getType.GetInterface(GetType(Data.IInspectorateAdvice).ToString) Is Nothing Then
                            If mode = SearchMode.Permit_With_Multiples OrElse BOItem.Count = 1 Then
                                If Not .IsInspectionAdviceNull Then CType(BOItem, Data.IInspectorateAdvice).InspectorateAdvice = .InspectionAdvice
                            Else
                                If Not .IsInspectionAdviceNull AndAlso Compare(CType(BOItem, Data.IInspectorateAdvice).InspectorateAdvice, .InspectionAdvice) Then CType(BOItem, Data.IInspectorateAdvice).InspectorateAdvice = "*"
                            End If
                        End If
                        If Not BOItem.getType.GetInterface(GetType(Data.IReferred).ToString) Is Nothing Then
                            If mode = SearchMode.Permit_With_Multiples OrElse BOItem.Count = 1 Then
                                If Not .IsReferredNull Then CType(BOItem, Data.IReferred).Referred = .Referred
                            Else
                                If Not .IsReferredNull AndAlso Compare(CType(BOItem, Data.IReferred).Referred, .Referred) Then CType(BOItem, Data.IReferred).Referred = "*"
                            End If
                        End If
                        If Not BOItem.getType.GetInterface(GetType(Data.IReIssued).ToString) Is Nothing Then
                            If mode = SearchMode.Permit_With_Multiples OrElse BOItem.Count = 1 Then
                                If Not .IsReIssuedNull Then CType(BOItem, Data.IReIssued).ReIssued = .ReIssued
                            Else
                                If Not .IsReIssuedNull AndAlso Compare(CType(BOItem, Data.IReIssued).ReIssued, .ReIssued) Then CType(BOItem, Data.IReIssued).ReIssued = "*"
                            End If
                        End If
                        If Not BOItem.getType.GetInterface(GetType(Data.IScientificName).ToString) Is Nothing Then
                            If Not .IsScientificNameNull Then CType(BOItem, Data.IScientificName).ScientificName = .ScientificName
                        End If

                        If Not BOItem.getType.GetInterface(GetType(Data.INumberOfPermits).ToString) Is Nothing Then
                            CType(BOItem, Data.INumberOfPermits).NumberOfPermits = BOItem.Count.ToString
                        End If

                        If Not BOItem.getType.GetInterface(GetType(Data.IApplicationNumber).ToString) Is Nothing Then
                            If Not .IsApplicationIdNull Then CType(BOItem, Data.IApplicationNumber).ApplicationNumber = .ApplicationId.ToString
                        End If

                        If Not BOItem.getType.GetInterface(GetType(Data.IApplicationId).ToString) Is Nothing Then
                            If Not .IsApplicationIdNull Then CType(BOItem, Data.IApplicationId).ApplicationId = .ApplicationId
                        End If

                        If Not BOItem.getType.GetInterface(GetType(Data.IQuantity).ToString) Is Nothing Then
                            If Not .IsQuantityNull Then CType(BOItem, Data.IQuantity).Quantity = .Quantity.ToString
                        End If

                        If Not BOItem.getType.GetInterface(GetType(Data.ICountryOfOrigin).ToString) Is Nothing Then
                            If Not .IsCountryOfOriginNull Then CType(BOItem, Data.ICountryOfOrigin).CountryOfOrigin = .CountryOfOrigin
                        End If

                        If Not BOItem.getType.GetInterface(GetType(Data.IDateReferred).ToString) Is Nothing Then
                            If mode = SearchMode.Permit_With_Multiples OrElse BOItem.Count = 1 Then
                                If Not .IsDateReferredNull Then CType(BOItem, Data.IDateReferred).DateReferred = .DateReferred.ToShortDateString
                            Else
                                If Not .IsDateReferredNull AndAlso Date.Compare(Date.Parse(CType(BOItem, Data.IDateReferred).DateReferred), .DateReferred) = 0 Then CType(BOItem, Data.IDateReferred).DateReferred = "*"
                            End If
                        End If

                        If mode = SearchMode.Permit_With_Multiples OrElse BOItem.Count = 1 Then
                            If Not .IsPermitStatusNull Then BOItem.Status = .PermitStatus
                        Else
                            If Not .IsPermitStatusNull AndAlso Compare(BOItem.Status, .PermitStatus) Then BOItem.Status = "*"
                        End If

                        BOItem.LinkId = .PermitId

                        Dim NewItem As Int32
                        If BOItem.Count <= 1 Then
                            NewItem = DataItems.Add(BOItem)
                        Else
                            NewItem = DataItems.IndexOf(BOItem)
                        End If

                        If Not .IsReceivedDateNull Then
                            ReDim CType(DataItems.Item(NewItem), Search.Data.BaseSearchData).CellColour(0)
                            CType(DataItems.Item(NewItem), Search.Data.BaseSearchData).CellColour(0) = New Data.CellColour(System.Drawing.Color.Red, "DateReceived")
                        End If
                    End With
                End If
            Next item
            Return New SearchResults(CType(DataItems.ToArray(GetType(Search.Data.BaseSearchData)), Search.Data.BaseSearchData()), HeaderType)
        End Function

        Private Shared Function GetPermitData_Notifications(ByVal criteria As ApplicationSearchCriteriaBase, ByVal import As Boolean, ByVal group As SearchGroup) As SearchResults
            ' Load the data
            Dim HeaderType As SearchResults.ColumnHeaderType
            Dim SearchType As SearchType
            If import Then
                SearchType = SearchType.ImportNotification
                Select Case group
                    Case SearchGroup.CaseOfficer, SearchGroup.KewJNCC, SearchGroup.Other
                        HeaderType = SearchResults.ColumnHeaderType.Four
                    Case SearchGroup.Inspectorate
                        HeaderType = SearchResults.ColumnHeaderType.Twelve
                    Case SearchGroup.Customer
                        HeaderType = SearchResults.ColumnHeaderType.Seventeen
                End Select
            Else
                SearchType = SearchType.SeizureNotification
                Select Case group
                    Case SearchGroup.CaseOfficer, SearchGroup.KewJNCC, SearchGroup.Other
                        HeaderType = SearchResults.ColumnHeaderType.Five
                    Case SearchGroup.Inspectorate
                        HeaderType = SearchResults.ColumnHeaderType.Thirteen
                    Case SearchGroup.Customer
                        HeaderType = SearchResults.ColumnHeaderType.Eighteen
                End Select
            End If

            Dim Data As DataObjects.Views.EntitySet.SearchCITESNotificationSet = CType(GetCITESPermit(criteria, HeaderType, "vSearchCITESNotification", SearchType, DataObjects.Views.Entity.SearchCITESNotification.ServiceObject, GetType(DataObjects.Views.EntitySet.SearchCITESNotificationSet)), DataObjects.Views.EntitySet.SearchCITESNotificationSet)
            Dim DataItems As New ArrayList

            For Each item As DataObjects.Views.Entity.SearchCITESNotification In Data
                Dim BOItem As Search.Data.BaseCITESNotificationSearchData
                If import Then
                    Select Case group
                        Case SearchGroup.CaseOfficer, SearchGroup.Other
                            BOItem = New Search.Data.CITESImportNotificationSearchData_CaseOfficer
                        Case SearchGroup.KewJNCC
                            BOItem = New Search.Data.CITESImportNotificationSearchData_KewJNCC
                        Case SearchGroup.Inspectorate
                            BOItem = New Search.Data.CITESImportNotificationSearchData_Inspectorate
                        Case SearchGroup.Customer
                            BOItem = New Search.Data.CITESImportNotificationSearchData_Customer
                    End Select
                Else
                    Select Case group
                        Case SearchGroup.CaseOfficer, SearchGroup.Other
                            BOItem = New Search.Data.CITESSeizureNotificationSearchData_CaseOfficer
                        Case SearchGroup.KewJNCC
                            BOItem = New Search.Data.CITESSeizureNotificationSearchData_KewJNCC
                        Case SearchGroup.Inspectorate
                            BOItem = New Search.Data.CITESSeizureNotificationSearchData_Inspectorate
                        Case SearchGroup.Customer
                            BOItem = New Search.Data.CITESSeizureNotificationSearchData_Customer
                    End Select
                End If

                If Not BOItem Is Nothing Then
                    With item
                        If Not BOItem.getType.GetInterface(GetType(Data.ISearchDataParty).ToString) Is Nothing Then
                            If Not .IsDisplayNameNull Then CType(BOItem, Data.ISearchDataParty).PartyName = .DisplayName
                            If Not .IsPartyIdNull Then CType(BOItem, Data.ISearchDataParty).PartyId = .PartyId.ToString
                        End If

                        If Not BOItem.getType.GetInterface(GetType(Data.IISOCode).ToString) Is Nothing Then
                            If Not .IsISOCodeNull Then CType(BOItem, Data.IISOCode).ISOCode = .ISOCode
                        End If
                        If Not BOItem.getType.GetInterface(GetType(Data.IISOCodeDescription).ToString) Is Nothing Then
                            If Not .IsISOCodeDescriptionNull Then CType(BOItem, Data.IISOCodeDescription).ISOCodeDescription = .ISOCodeDescription
                        End If
                        If Not BOItem.getType.GetInterface(GetType(Data.IPD).ToString) Is Nothing Then
                            If Not .IsPDNull Then CType(BOItem, Data.IPD).PD = .PD
                        End If
                        If Not BOItem.getType.GetInterface(GetType(Data.IScientificName).ToString) Is Nothing Then
                            If Not .IsScientificNameNull Then CType(BOItem, Data.IScientificName).ScientificName = .ScientificName
                        End If
                        If Not BOItem.getType.GetInterface(GetType(Data.INotificationReference).ToString) Is Nothing Then
                            If Not .IsNotificationRefNull Then CType(BOItem, Data.INotificationReference).NotificationReference = .NotificationRef
                        End If
                        'If Not .IsDateOfImportNull Then
                        Dim DateStr As String = .DateOfImport.ToShortDateString
                        If Not BOItem.getType.GetInterface(GetType(Data.IDateOfImport).ToString) Is Nothing Then
                            CType(BOItem, Data.IDateOfImport).DateOfImport = DateStr
                        End If
                        If Not BOItem.getType.GetInterface(GetType(Data.IDateSeized).ToString) Is Nothing Then
                            CType(BOItem, Data.IDateSeized).DateSeized = DateStr
                        End If
                        'End If
                        If Not BOItem.getType.GetInterface(GetType(Data.IQuantity).ToString) Is Nothing Then
                            If Not .IsQtyNull Then CType(BOItem, Data.IQuantity).Quantity = .Qty.ToString
                        End If
                        If Not BOItem.getType.GetInterface(GetType(Data.ICustomsRef).ToString) Is Nothing Then
                            If Not .IsNotificationRefNull Then CType(BOItem, Data.ICustomsRef).CustomsRef = .NotificationRef
                        End If
                        If Not BOItem.getType.GetInterface(GetType(Data.IActive).ToString) Is Nothing Then
                            CType(BOItem, Data.IActive).Active = ConvertToEnglishBoolean(.Active)
                        End If
                        If Not BOItem.getType.GetInterface(GetType(Data.ILinked).ToString) Is Nothing Then
                            CType(BOItem, Data.ILinked).Linked = ConvertToEnglishBoolean(.Linked).Substring(0, 1)
                        End If
                        If Not .IsNotificationIdNull Then BOItem.LinkId = .NotificationId
                        If Not BOItem.getType.GetInterface(GetType(Data.IDateReceived).ToString) Is Nothing Then
                            If Not .IsReceivedDateNull Then CType(BOItem, Data.IDateReceived).DateReceived = .ReceivedDate.ToShortDateString
                        End If
                        DataItems.Add(BOItem)
                    End With
                End If
            Next item
            Return New SearchResults(CType(DataItems.ToArray(GetType(Search.Data.BaseSearchData)), Search.Data.BaseSearchData()), HeaderType)
        End Function

        Private Shared Function GetData_ChickDOR(ByVal criteria As ApplicationSearchCriteriaBase, ByVal group As SearchGroup) As SearchResults
            ' Load the data
            Dim HeaderType As SearchResults.ColumnHeaderType
            Select Case group
                Case SearchGroup.CaseOfficer, SearchGroup.KewJNCC, SearchGroup.Other
                    HeaderType = SearchResults.ColumnHeaderType.TwentyOne
                Case SearchGroup.Inspectorate
                    HeaderType = SearchResults.ColumnHeaderType.TwentyTwo
                Case SearchGroup.Customer
                    HeaderType = SearchResults.ColumnHeaderType.TwentyThree
            End Select

            Dim Data As DataObjects.Views.EntitySet.SearchBirdChickDORSet = CType(GetCITESPermit(criteria, HeaderType, "vSearchBirdChickDOR", SearchType.None, DataObjects.Views.Entity.SearchBirdChickDOR.ServiceObject, GetType(DataObjects.Views.EntitySet.SearchBirdChickDORSet)), DataObjects.Views.EntitySet.SearchBirdChickDORSet)
            Dim DataItems As New ArrayList

            For Each item As DataObjects.Views.Entity.SearchBirdChickDOR In Data
                Dim BOItem As Search.Data.BaseBirdSearchData
                If group = SearchGroup.Customer Then
                    BOItem = New Search.Data.BirdOtherSearchData_Customer
                Else
                    BOItem = New Search.Data.BirdOtherSearchData_NonCustomer
                End If

                If Not BOItem Is Nothing Then
                    With item
                        BOItem.ApplicationId = .ApplicationId
                        BOItem.LinkId = .ApplicationId

                        If Not .IsRequestSubmittedDateNull Then BOItem.DateRequestSubmitted = .RequestSubmittedDate.ToShortDateString Else BOItem.DateRequestSubmitted = String.Empty
                        If Not .IsScientificNameNull Then BOItem.ScientificName = .ScientificName Else BOItem.ScientificName = String.Empty

                        If TypeOf BOItem Is Data.BirdChickSearchData_Customer Then
                            If Not .IsHatchDateNull Then CType(BOItem, Data.BirdChickSearchData_Customer).HatchDate = .HatchDate.ToShortDateString Else CType(BOItem, Data.BirdChickSearchData_Customer).HatchDate = String.Empty
                            If Not .IsNumberOfEggsNull Then CType(BOItem, Data.BirdChickSearchData_Customer).NumberOfEggs = .HatchDate.ToShortDateString Else CType(BOItem, Data.BirdChickSearchData_Customer).NumberOfEggs = String.Empty
                        End If

                        If group = SearchGroup.Customer Then
                            If Not .IsCustomerStatusNull Then BOItem.Status = .CustomerStatus Else BOItem.Status = String.Empty
                        Else
                            If Not .IsStatusNull Then BOItem.Status = .Status Else BOItem.Status = String.Empty
                        End If

                        If Not BOItem.getType.GetInterface(GetType(Data.ISearchDataParty).ToString) Is Nothing Then
                            If Not .IsKeeperNameNull Then CType(BOItem, Data.ISearchDataParty).PartyName = .KeeperName
                            'If Not .IsPartyIdNull Then CType(BOItem, Data.ISearchDataParty).PartyId = .PartyId.ToString
                        End If

                        'If Not BOItem.GetType.GetInterface(GetType(Data.IISOCode).ToString) Is Nothing Then
                        '    'If Not .IsISOCodeNull Then CType(BOItem, Data.IISOCode).ISOCode = .ISOCode
                        'End If
                        'If Not BOItem.GetType.GetInterface(GetType(Data.IISOCodeDescription).ToString) Is Nothing Then
                        '    'If Not .IsISOCodeDescriptionNull Then CType(BOItem, Data.IISOCodeDescription).ISOCodeDescription = .ISOCodeDescription
                        'End If
                        'If Not BOItem.GetType.GetInterface(GetType(Data.IPD).ToString) Is Nothing Then
                        '    'If Not .IsPDNull Then CType(BOItem, Data.IPD).PD = .PD
                        'End If
                        'If Not BOItem.GetType.GetInterface(GetType(Data.IScientificName).ToString) Is Nothing Then
                        '    If Not .IsScientificNameNull Then CType(BOItem, Data.IScientificName).ScientificName = .ScientificName
                        'End If
                        'If Not BOItem.GetType.GetInterface(GetType(Data.INotificationReference).ToString) Is Nothing Then
                        '    'If Not .IsNotificationRefNull Then CType(BOItem, Data.INotificationReference).NotificationReference = .NotificationRef
                        'End If
                        'Dim DateStr As String = .DateOfImport.ToShortDateString
                        'If Not BOItem.GetType.GetInterface(GetType(Data.IDateOfImport).ToString) Is Nothing Then
                        '    CType(BOItem, Data.IDateOfImport).DateOfImport = DateStr
                        'End If
                        'If Not BOItem.GetType.GetInterface(GetType(Data.IDateSeized).ToString) Is Nothing Then
                        '    CType(BOItem, Data.IDateSeized).DateSeized = DateStr
                        'End If
                        'If Not BOItem.GetType.GetInterface(GetType(Data.IQuantity).ToString) Is Nothing Then
                        '    If Not .IsQtyNull Then CType(BOItem, Data.IQuantity).Quantity = .Qty.ToString
                        'End If
                        'If Not BOItem.GetType.GetInterface(GetType(Data.ICustomsRef).ToString) Is Nothing Then
                        '    If Not .IsNotificationRefNull Then CType(BOItem, Data.ICustomsRef).CustomsRef = .NotificationRef
                        'End If
                        'If Not BOItem.GetType.GetInterface(GetType(Data.IActive).ToString) Is Nothing Then
                        '    CType(BOItem, Data.IActive).Active = ConvertToEnglishBoolean(.Active)
                        'End If
                        'If Not BOItem.GetType.GetInterface(GetType(Data.ILinked).ToString) Is Nothing Then
                        '    CType(BOItem, Data.ILinked).Linked = ConvertToEnglishBoolean(.Linked).Substring(0, 1)
                        'End If
                        'If Not .IsNotificationIdNull Then BOItem.LinkId = .NotificationId
                        'If Not BOItem.GetType.GetInterface(GetType(Data.IDateReceived).ToString) Is Nothing Then
                        '    If Not .IsReceivedDateNull Then CType(BOItem, Data.IDateReceived).DateReceived = .ReceivedDate.ToShortDateString
                        'End If
                        DataItems.Add(BOItem)
                    End With
                End If
            Next item
            Return New SearchResults(CType(DataItems.ToArray(GetType(Search.Data.BaseSearchData)), Search.Data.BaseSearchData()), HeaderType)
        End Function

        'Private Shared Function GetData_BirdRegistrations(ByVal criteria As ApplicationSearchCriteriaBase, ByVal group As SearchGroup) As SearchResults
        '    ' Load the data
        '    Dim HeaderType As SearchResults.ColumnHeaderType
        '    Select Case group
        '        Case SearchGroup.CaseOfficer, SearchGroup.KewJNCC, SearchGroup.Other, SearchGroup.Inspectorate
        '            HeaderType = SearchResults.ColumnHeaderType.Nineteen
        '        Case SearchGroup.Customer
        '            HeaderType = SearchResults.ColumnHeaderType.Twenty
        '    End Select

        '    Dim Data As DataObjects.Views.EntitySet.SearchCITESNotificationSet = CType(GetCITESPermit(criteria, HeaderType, "vSearchCITESNotification", DataObjects.Views.Entity.SearchCITESNotification.ServiceObject, GetType(DataObjects.Views.EntitySet.SearchCITESNotificationSet)), DataObjects.Views.EntitySet.SearchCITESNotificationSet)
        '    Dim DataItems As New ArrayList

        '    For Each item As DataObjects.Views.Entity.SearchCITESNotification In Data
        '        Dim BOItem As Search.Data.BaseBirdSearchData
        '        If group = SearchGroup.Customer Then
        '            BOItem = New Search.Data.BirdOtherSearchData_Customer
        '        Else
        '            BOItem = New Search.Data.BirdOtherSearchData_NonCustomer
        '        End If

        '        If Not BOItem Is Nothing Then
        '            With item
        '                If Not BOItem.GetType.GetInterface(GetType(Data.ISearchDataParty).ToString) Is Nothing Then
        '                    If Not .IsDisplayNameNull Then CType(BOItem, Data.ISearchDataParty).PartyName = .DisplayName
        '                    If Not .IsPartyIdNull Then CType(BOItem, Data.ISearchDataParty).PartyId = .PartyId.ToString
        '                End If

        '                If Not BOItem.GetType.GetInterface(GetType(Data.IISOCode).ToString) Is Nothing Then
        '                    If Not .IsISOCodeNull Then CType(BOItem, Data.IISOCode).ISOCode = .ISOCode
        '                End If
        '                If Not BOItem.GetType.GetInterface(GetType(Data.IISOCodeDescription).ToString) Is Nothing Then
        '                    If Not .IsISOCodeDescriptionNull Then CType(BOItem, Data.IISOCodeDescription).ISOCodeDescription = .ISOCodeDescription
        '                End If
        '                If Not BOItem.GetType.GetInterface(GetType(Data.IPD).ToString) Is Nothing Then
        '                    If Not .IsPDNull Then CType(BOItem, Data.IPD).PD = .PD
        '                End If
        '                If Not BOItem.GetType.GetInterface(GetType(Data.IScientificName).ToString) Is Nothing Then
        '                    If Not .IsScientificNameNull Then CType(BOItem, Data.IScientificName).ScientificName = .ScientificName
        '                End If
        '                If Not BOItem.GetType.GetInterface(GetType(Data.INotificationReference).ToString) Is Nothing Then
        '                    If Not .IsNotificationRefNull Then CType(BOItem, Data.INotificationReference).NotificationReference = .NotificationRef
        '                End If
        '                'If Not .IsDateOfImportNull Then
        '                Dim DateStr As String = .DateOfImport.ToShortDateString
        '                If Not BOItem.GetType.GetInterface(GetType(Data.IDateOfImport).ToString) Is Nothing Then
        '                    CType(BOItem, Data.IDateOfImport).DateOfImport = DateStr
        '                End If
        '                If Not BOItem.GetType.GetInterface(GetType(Data.IDateSeized).ToString) Is Nothing Then
        '                    CType(BOItem, Data.IDateSeized).DateSeized = DateStr
        '                End If
        '                'End If
        '                If Not BOItem.GetType.GetInterface(GetType(Data.IQuantity).ToString) Is Nothing Then
        '                    If Not .IsQtyNull Then CType(BOItem, Data.IQuantity).Quantity = .Qty.ToString
        '                End If
        '                If Not BOItem.GetType.GetInterface(GetType(Data.ICustomsRef).ToString) Is Nothing Then
        '                    If Not .IsNotificationRefNull Then CType(BOItem, Data.ICustomsRef).CustomsRef = .NotificationRef
        '                End If
        '                If Not BOItem.GetType.GetInterface(GetType(Data.IActive).ToString) Is Nothing Then
        '                    CType(BOItem, Data.IActive).Active = ConvertToEnglishBoolean(.Active)
        '                End If
        '                If Not BOItem.GetType.GetInterface(GetType(Data.ILinked).ToString) Is Nothing Then
        '                    CType(BOItem, Data.ILinked).Linked = ConvertToEnglishBoolean(.Linked).Substring(0, 1)
        '                End If
        '                If Not .IsNotificationIdNull Then BOItem.LinkId = .NotificationId
        '                If Not BOItem.GetType.GetInterface(GetType(Data.IDateReceived).ToString) Is Nothing Then
        '                    If Not .IsReceivedDateNull Then CType(BOItem, Data.IDateReceived).DateReceived = .ReceivedDate.ToShortDateString
        '                End If
        '                DataItems.Add(BOItem)
        '            End With
        '        End If
        '    Next item
        '    Return New SearchResults(CType(DataItems.ToArray(GetType(Search.Data.BaseSearchData)), Search.Data.BaseSearchData()), HeaderType)
        'End Function

        Friend Shared Function ConvertToEnglishBoolean(ByVal flag As Int32) As String
            Dim bool As Boolean = Not (flag = 0)
            Return ConvertToEnglishBoolean(bool)
        End Function

        Friend Shared Function ConvertToEnglishBoolean(ByVal flag As Boolean) As String
            If flag Then
                Return "Yes"
            Else
                Return "No"
            End If
        End Function

        Friend Shared Function ConvertEnglishToBoolean(ByVal flag As String) As Boolean
            If flag Is Nothing OrElse flag.Length = 0 Then Return False

            Return flag.ToLower.StartsWith("y")
        End Function

        Friend Shared Function ConvertEnglishToInt32(ByVal flag As String) As Int32
            If ConvertEnglishToBoolean(flag) Then
                Return 1
            Else
                Return 0
            End If
        End Function

        Private Shared Function Compare(ByVal value1 As String, ByVal value2 As String) As Boolean
            Return (value1 Is Nothing AndAlso Not value2 Is Nothing) OrElse _
               (Not value1 Is Nothing AndAlso value2 Is Nothing) OrElse _
               (String.Compare(value1, value2) <> 0)
        End Function

        Public Shared Function ParseParams(ByVal data As String) As String
            Return DataObjects.Views.Service.SearchPartyService.ParseParams(data)
        End Function
    End Class


End Namespace
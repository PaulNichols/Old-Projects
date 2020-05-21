Namespace Application.CITES.Applications
    Public Class BOImportApplication
        Inherits Application.CITES.Applications.BOImportExportApplication
        Implements IBOImportApplication

#Region " Prelim code "
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal importApplicationId As Int32)
            MyClass.New()
            LoadImportApplication(importApplicationId)
        End Sub

        Public Sub New(ByVal importApplicationId As Int32, ByVal tran As SqlClient.SqlTransaction) 'MLD 2/12/4
            MyClass.New()
            LoadImportApplication(importApplicationId, tran)
        End Sub

        Private Function LoadImportApplication(ByVal id As Int32) As DataObjects.Entity.ImportApplication
            Return LoadImportApplication(id, Nothing)
        End Function

        Private Function LoadImportApplication(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.ImportApplication
            Dim NewImportApplication As DataObjects.Entity.ImportApplication = DataObjects.Entity.ImportApplication.GetById(id)
            If NewImportApplication Is Nothing Then
                Throw New RecordDoesNotExist("ImportApplication", id)
            Else
                InitialiseImportApplication(NewImportApplication, tran)
                Return NewImportApplication
            End If
        End Function

        Public Overridable Sub InitialiseImportApplication(ByVal applicationId As Int32)
            InitialiseImportApplication(applicationId, Nothing)
        End Sub

        Public Overridable Sub InitialiseImportApplication(ByVal applicationId As Int32, ByVal tran As SqlClient.SqlTransaction)
            Dim ImportApp As DataObjects.Entity.ImportApplication = Application.CITES.Applications.BOCITESImportExportPermit.GetImportApplicationDO(applicationId)
            If Not ImportApp Is Nothing Then
                InitialiseImportApplication(ImportApp, tran)
            End If
        End Sub

        Protected Overloads Overrides Sub InitialiseImportExportApplication(ByVal citesImportExportApplication As EnterpriseObjects.Entity, ByVal tran As System.Data.SqlClient.SqlTransaction)
            InitialiseImportApplication(CType(citesImportExportApplication, DataObjects.Entity.ImportApplication), tran)
        End Sub

        Friend Overridable Sub InitialiseImportApplication(ByVal citesImportApplication As DataObjects.Entity.ImportApplication, ByVal tran As SqlClient.SqlTransaction)
            With citesImportApplication
                Dim ExportCountry As Object = Nothing
                Dim ImportCountry As Object = Nothing
                Dim ExportRegion As Object = Nothing
                Dim ImportRegion As Object = Nothing
                If Not .IsCountryOfExportIdNull Then ExportCountry = .CountryOfExportId
                If Not .IsRegionOfExportIdNull Then ExportRegion = .RegionOfExportId
                If Not .IsRegionOfImportIdNull Then ImportRegion = .RegionOfImportId

                InitialiseImportExportApplication(.CitesApplicationId, .CheckSum, .Id, ExportCountry, ImportCountry, ExportRegion, ImportRegion, tran)
            End With
        End Sub
#End Region

#Region " Properties "


        Public Overrides Property ApplicationTypeId() As Int32  'MLD added 16/12/4
            Get
                Return Application.ApplicationTypes.Import
            End Get
            Set(ByVal Value As Int32)
            End Set
        End Property

        Public Overrides Property CountryOfImport() As ReferenceData.BOCountry
            Get
                If mCountryOfImport Is Nothing OrElse mCountryOfImport.ID = 0 Then
                    Dim c As New BO.BOConfiguration
                    Dim Result As Object = c.GetValue("DefaultCountry")
                    If Not Result Is Nothing AndAlso _
                        c.IsInt32(Result) Then
                        mCountryOfImport = New ReferenceData.BOCountry(CType(Result, Int32))
                    Else
                        mCountryOfImport = Nothing
                    End If
                    c = Nothing
                End If
                Return mCountryOfImport
            End Get
            Set(ByVal Value As ReferenceData.BOCountry)
                mCountryOfImport = Value
            End Set
        End Property

        Public Overrides Property CountryOfExport() As ReferenceData.BOCountry
            Get
                If mCountryOfExport Is Nothing OrElse mCountryOfExport.ID = 0 Then
                    'Dim Config As New BO.BOConfiguration
                    'Dim Result As Object = Config.GetValue("DefaultCountry")
                    'If Not Result Is Nothing AndAlso _
                    '    Config.IsInt32(Result) Then
                    If Not SecondParty Is Nothing AndAlso Not SecondParty.Address Is Nothing AndAlso SecondParty.Address.CountryId > 0 Then
                        mCountryOfExport = New ReferenceData.BOCountry(SecondParty.Address.CountryId)
                    Else
                        mCountryOfExport = Nothing
                    End If
                    'Config = Nothing
                End If
                Return mCountryOfExport
            End Get
            Set(ByVal Value As ReferenceData.BOCountry)
                mCountryOfExport = Value
            End Set
        End Property


        Public Overrides Property SecondParty() As BOApplicationPartyDetails
            Get
                Return MyBase.SecondParty
            End Get
            Set(ByVal Value As BOApplicationPartyDetails)
                MyBase.SecondParty = Value
            End Set
        End Property

        Public Property ImportApplicationCheckSum() As Integer Implements IBOImportApplication.ImportApplicationCheckSum
            Get
                Return MyBase.ImportExportApplicationCheckSum
            End Get
            Set(ByVal Value As Integer)
                MyBase.ImportExportApplicationCheckSum = Value
            End Set
        End Property

        Public Property ImportApplicationId() As Integer Implements IBOImportApplication.ImportApplicationId
            Get
                Return MyBase.ImportExportApplicationId
            End Get
            Set(ByVal Value As Integer)
                MyBase.ImportExportApplicationId = Value
            End Set
        End Property

        Public Overrides Property Exporter() As BOApplicationPartyDetails
            Get
                Return MyBase.SecondParty
            End Get
            Set(ByVal Value As BOApplicationPartyDetails)
                'commented out because there was a problem, the party was always nothing
                'when serialised
                ' MyBase.SecondParty = Value
            End Set
        End Property

        Public Overrides Property Importer() As BOApplicationPartyDetails
            Get
                Return MyBase.Party
            End Get
            Set(ByVal Value As BOApplicationPartyDetails)
                'commented out because there was a problem, the party was always nothing
                'when serialised

                ' MyBase.Party = Value
            End Set
        End Property
#End Region

#Region " Helper Functions "
        Protected Overrides Function GetChecksum(ByVal entity As EnterpriseObjects.Entity) As Integer
            Return CType(entity, DataObjects.Entity.ImportApplication).CheckSum
        End Function

        Protected Overrides Function GetID(ByVal entity As EnterpriseObjects.Entity) As Integer
            Return CType(entity, DataObjects.Entity.ImportApplication).Id
        End Function

        Friend Overrides ReadOnly Property CheckAuthorisedLocation(ByVal permit As Application.CITES.Applications.BOCITESImportExportPermit) As Boolean
            Get
                Dim Config As New BOConfiguration
                Return (Config.IsInAuthorisedLocation(permit.Source1.Code) OrElse _
                        Config.IsInAuthorisedLocation(permit.Source2.Code)) AndAlso _
                        permit.Specie.IsAnnexA AndAlso _
                        Config.IsLiveDerivative(permit.Derivative)
            End Get
        End Property
#End Region

#Region " Save "
        Protected Overrides Function GetSaveService() As EnterpriseObjects.Service
            Dim NewImportApplication As New DataObjects.Entity.ImportApplication
            Return NewImportApplication.ServiceObject
        End Function

        Protected Overrides Function InsertApp(ByVal citesApplicationId As Integer, ByVal countryofExportId As Object, ByVal regionOfExportId As Object, ByVal regionOfImportId As Object, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity
            If (CountryOfExport Is Nothing OrElse CountryOfExport.ID = 0) AndAlso Not Me.SecondParty Is Nothing Then
                CountryOfExport = New BO.ReferenceData.BOCountry(Me.SecondParty.Address.CountryId)
                countryofExportId = CountryOfExport.ID
            End If
            Return CType(GetSaveService(), DataObjects.service.ImportApplicationService).Insert(citesApplicationId, countryofExportId, regionOfExportId, regionOfImportId, tran)
        End Function

        Protected Overrides Function UpdateApp(ByVal importExportApplicationId As Integer, ByVal citesApplicationId As Integer, ByVal countryofExportId As Object, ByVal regionOfExportId As Object, ByVal regionOfImportId As Object, ByVal checkSum As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity
            If (CountryOfExport Is Nothing OrElse CountryOfExport.ID = 0) AndAlso Not Me.SecondParty Is Nothing Then
                CountryOfExport = New BO.ReferenceData.BOCountry(Me.SecondParty.Address.CountryId)
                countryofExportId = CountryOfExport.ID
            End If
            Return CType(GetSaveService(), DataObjects.service.ImportApplicationService).Update(importExportApplicationId, citesApplicationId, countryofExportId, regionOfExportId, regionOfImportId, checkSum, tran)
        End Function

        'Public Overloads Overrides Function Save(ByVal tran As System.Data.SqlClient.SqlTransaction) As BaseBO
        '    Return MyBase.Save(tran)
        'End Function

        'Public Overloads Overrides Function Save() As BaseBO
        '    Dim NewImportApplication As New DataObjects.Entity.ImportApplication
        '    Dim service As DataObjects.Service.ImportApplicationService = NewImportApplication.ServiceObject
        '    Dim tran As SqlClient.SqlTransaction = service.BeginTransaction

        '    Dim SaveResult As BaseBO = MyClass.Save(tran)
        '    If SaveResult Is Nothing Then
        '        service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
        '    Else
        '        service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Commit)
        '    End If
        '    Return SaveResult
        'End Function
#End Region

#Region " Validate "
        'Public Overloads Overrides Function Validate(ByVal writeFlag As Boolean, ByVal ignoreWarnings As Boolean) As Object Implements IBOImportApplication.Validate
        '    Return Validate(writeFlag, ignoreWarnings, True)
        'End Function

        Protected Overloads Overrides Function Validate(ByVal writeFlag As Boolean, ByVal ignoreWarnings As Boolean, ByVal saveApplication As Boolean) As BaseBO
            MyBase.Validate(writeFlag, ignoreWarnings, saveApplication)
            Dim ReturnObj As BaseBO = Me
            ReturnObj.ValidationErrors = New ValidationManager(ValidationManager.ValidationTitles.CannotSaveImportApplication, ignoreWarnings)

            'check the species to see if we have any EC Annex D
            Dim FoundAnnexAB As Boolean = False
            Dim FoundAppendixA As Boolean = ignoreWarnings
            Dim FoundAnnexABCD As Boolean = False

            Dim FoundAnnexB As Boolean = True 'ignoreWarnings
            Dim FoundAnnexBTrade As Boolean = True 'ignoreWarnings

            Dim FoundUOMError As Boolean = False
            Dim FoundBirthdateError As Boolean = False
            Dim FoundSex As Boolean = False
            Dim FoundAuthorisedLocation As Boolean = False

            Dim Config As New BOConfiguration
            CheckMandatory(ReturnObj)

            If Not Me.Permit Is Nothing AndAlso _
               Permit.Length > 0 Then
                For Each _permit As Application.CITES.Applications.BOCITESImportExportPermit In Permit
                    ' check mandatory
                    CheckMandatory(ReturnObj, _permit)

                    ' ensure UOM's are of the same type
                    Dim UOMDetails As BOMeasurement = Nothing
                    If Not FoundUOMError AndAlso _
                       Not FoundBirthdateError AndAlso _
                       Not FoundSex AndAlso _
                       Not _permit.Specimens Is Nothing AndAlso _
                       _permit.Specimens.Length > 0 Then
                        For Each _Specimen As Application.BOSpecimen In _permit.Specimens
                            If Not FoundUOMError Then
                                If UOMDetails Is Nothing Then
                                    ' first time through
                                    UOMDetails = _Specimen.UOM
                                ElseIf Not BOMeasurement.IsEqual(UOMDetails, _Specimen.UOM) Then
                                    ' ensure that they are the same
                                    ReturnObj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.UOMDifferBetweenSpecimens))
                                    FoundUOMError = True
                                End If
                            End If
                            If CheckBirthdateEntryRequired(_Permit) Then
                                If Not FoundBirthdateError Then
                                    ' only mandatory is "LIVE"
                                    If _Specimen.DOB = Nothing Then     'MLD 31/1/5
                                        ReturnObj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.IfLiveBirthDateCannotBeBlank))
                                        FoundBirthdateError = True
                                    End If
                                End If
                                If Not FoundSex Then
                                    ' only mandatory is "LIVE"
                                    If _Specimen.Gender = GenderType.Unknown Then
                                        ReturnObj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.IfLiveGenderCannotBeBlank))
                                        FoundSex = True
                                    End If
                                End If
                            End If
                            If FoundBirthdateError AndAlso FoundUOMError AndAlso FoundSex Then Exit For
                        Next _Specimen
                    End If

                    'more to check, so check 'em
                    If Not _Permit.Specie Is Nothing Then
                        If Not FoundAnnexAB AndAlso _
                           (_Permit.Specie.IsAnnexA OrElse _
                           _permit.Specie.IsAnnexB) AndAlso _
                           (Config.IsInImportSpecieSourceCountry(_Permit.Source1.Code) OrElse _
                           Config.IsInImportSpecieSourceCountry(_Permit.Source2.Code)) AndAlso _
                           _Permit.CountryOfOrigin.ID = 1 Then
                            ReturnObj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.SpecimensCannotBeAnnexD))
                            FoundAnnexAB = True
                        End If
                        If Not FoundAppendixA AndAlso _
                           _Permit.Specie.IsAnnexA AndAlso _
                           (Config.IsInExportAppendixISpecimenSource(_Permit.Source1.Code) OrElse _
                           Config.IsInExportAppendixISpecimenSource(_Permit.Source2.Code)) AndAlso _
                           Config.IsInExportAppendixISpecimenPurpose(_Permit.Purpose.Code) Then
                            ReturnObj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.SpecimensCannotBeAnnexA))
                            FoundAppendixA = True
                        End If
                        If Not FoundAnnexABCD AndAlso _
                           _Permit.Specie.IsAnnexC OrElse _
                           _permit.Specie.IsAnnexD Then
                            ReturnObj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.NotRequiredForSpecimenOnAnnexeCOrD))
                            FoundAnnexABCD = True
                        End If

                        If Not FoundAuthorisedLocation AndAlso _
                           CheckAuthorisedLocation(_Permit) Then
                            If LocationAddress Is Nothing OrElse _
                               LocationAddress.AddressId = 0 Then
                                ReturnObj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.AuthorisedLocationCannotBeBlank))
                                FoundAuthorisedLocation = True
                            End If
                        End If

                        ' Business rules 13 & 14
                        'If Not FoundAnnexBTrade AndAlso _
                        '   _Permit.Specie.IsAnnexB AndAlso _
                        '   Config.IsInExportAppendixISpecimenPurpose(_Permit.Purpose.Code) Then
                        '    MyBase.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.PermitLikelyToBeRefused))
                        '    FoundAnnexBTrade = True
                        'End If
                        'If Not FoundAnnexB AndAlso _
                        '   _Permit.Specie.IsAnnexB AndAlso _
                        '   Config.IsInExportAppendixISpecimenPurpose(_Permit.Purpose.Code) Then
                        '    MyBase.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.PermitLikelyToBeRefused))
                        '    FoundAnnexB = True
                        'End If
                    End If
                Next _Permit
            Else
                'no permits!
                ReturnObj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.AnApplicationMustHaveAtLeastOnePermit))
            End If

            'the country of export must NOT be an EC member state      MLD altered 12/10/04
            If Not CountryOfExport Is Nothing Then
                Dim GWDCountry As ReferenceData.BOGWDCountry = Me.CountryOfExport.GetGWDCountry()
                If GWDCountry Is Nothing OrElse GWDCountry.ECMemberState Then
                    ReturnObj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.CountryOfExportMustNotBeAnECState))
                End If
            End If

            If Importer Is Nothing Then
                ReturnObj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.ImporterRequired))
            Else
                'check semi-completes flag
                If Not Importer.Party.AllowIncompleteImportApplications AndAlso _
                   IsSemiComplete Then
                    ReturnObj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.PartyIsNotAllowedToProcessSemiCompletes))
                ElseIf Importer.Party.AllowIncompleteImportApplications AndAlso _
                   IsSemiComplete Then
                    'allowed to semi complete the data
                Else
                    'not allowed to semi-complete or is a non-semi complete app, 
                    'so check that the non semi complete requirements have been met.
                    If CountryOfImport Is Nothing OrElse CountryOfImport.ID = 0 Then
                        ReturnObj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.NonSemiCompleteCountryOfExportNotDefined))
                    End If
                    If Exporter Is Nothing OrElse Exporter.Party Is Nothing OrElse Exporter.Party.PartyId = 0 Then
                        ReturnObj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.NonSemiCompleteExporterNotDefined))
                    End If
                    If Not Me.Permit Is Nothing AndAlso _
                       Permit.Length > 0 Then
                        Dim FoundError As Boolean = False
                        For Each _Permit As Application.CITES.BOCITESPermit In Permit
                            If Not _permit.Specimens Is Nothing AndAlso _
                               _permit.Specimens.Length > 0 Then
                                For Each _Specimen As Application.BOSpecimen In _permit.Specimens
                                    'ensure that the qty and mass fields aren't blank
                                    If Not _Specimen.UOM Is Nothing Then
                                        If (_Specimen.UOM.Qty Is Nothing OrElse CType(_Specimen.UOM.Qty, Int32) = 0) AndAlso _
                                             (_Specimen.UOM.Mass Is Nothing OrElse CType(_Specimen.UOM.Mass, Decimal) = 0) Then
                                            'both are blank, which isn't allowed for non- semi-completes
                                            ReturnObj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.NonSemiCompleteMassOrQuantityNotDefined))
                                            FoundError = True
                                            Exit For
                                        End If
                                    End If
                                Next _Specimen
                                If FoundError Then Exit For
                            End If
                        Next _Permit
                    End If
                End If
            End If

            If MyBase.ValidationErrors.HasErrors Then
                If writeFlag Then CType(ReturnObj, IApplication).Validated = False
            Else
                If writeFlag Then CType(ReturnObj, IApplication).Validated = True
                MyBase.ValidationErrors = Nothing
            End If

            If saveApplication Then
                ReturnObj = MyBase.Save(True)
            End If

            Return ReturnObj
        End Function
#End Region

#Region " Operations "
        Public Overrides Function Clone() As Object
            Dim BaseApplication As Application.CITES.Applications.BOImportApplication = CType(MyBase.Clone, Application.CITES.Applications.BOImportApplication)
            With BaseApplication
                .ImportApplicationId = 0
            End With
            Return BaseApplication
        End Function


        Public Shared Function UnMatchedSeizureNotifications(ByVal partyLinkId As Int32, ByVal applicationId As Int32) As Application.CITES.SeizureNotification.BOSeizureNotification()
            Dim Config As New BO.BOConfiguration
            Dim Result As Object = Config.GetValue("UnMatchedSeizureNotificationDays")
            If Not Result Is Nothing AndAlso _
                Config.IsInt32(Result) Then
                Dim SNs As [DO].DataObjects.EntitySet.SeizureNotificationSet = CType([DO].DataObjects.Sprocs.dbo_usp_GetUnMatchedSeizureNotifications(partyLinkId, CType(Result, Int32), applicationId, Nothing, GetType([DO].DataObjects.EntitySet.SeizureNotificationSet)), [DO].DataObjects.EntitySet.SeizureNotificationSet)
                If Not SNs Is Nothing AndAlso SNs.Count > 0 Then
                    Dim ReturnArray(SNs.Count - 1) As Application.CITES.SeizureNotification.BOSeizureNotification
                    Dim i As Int32 = 0
                    For Each sn As [DO].DataObjects.Entity.SeizureNotification In SNs.Entities
                        ReturnArray(i) = New Application.CITES.SeizureNotification.BOSeizureNotification(sn.Id)
                        i += 1
                    Next

                    Return ReturnArray
                End If
            End If

            'Dim ReturnNotifications() As BO.Application.CITES.SeizureNotification.BOSeizureNotification
            'Dim PartyLinkDO As New [DO].DataObjects.Entity.PartyLink
            'Dim PartyLinkService As [DO].DataObjects.Service.PartyLinkService = PartyLinkDO.ServiceObject
            'Dim PartyLinks As [DO].DataObjects.EntitySet.PartyLinkSet = PartyLinkService.GetByIndex_IX_PartyLink(Me.Party.Party.PartyId)

            'Dim CollectionCount As Int32 = 0
            'If Not PartyLinks Is Nothing Then
            '    Dim Config As New BO.BOConfiguration
            '    Dim Result As Object = Config.GetValue("UnMatchedSeizureNotificationDays")
            '    If Not Result Is Nothing AndAlso _
            '        Config.IsInt32(Result) Then
            '        For Each PartyLink As DataObjects.Entity.PartyLink In PartyLinks
            '            Try
            '                Dim CNSet As [DO].DataObjects.EntitySet.CITESNotificationSet = PartyLink.GetRelatedPartyLinkIdCITESNotification
            '                If Not CNSet Is Nothing OrElse CNSet.Entities.Count > 0 Then
            '                    For Each NotificationDO As [DO].DataObjects.Entity.CITESNotification In CNSet
            '                        Dim SN As [DO].DataObjects.Entity.SeizureNotification = NotificationDO.GetRelatedSeizureNotification.Entities(0)
            '                        If SN.GetRelatedSeizureToPermitLink Is Nothing OrElse SN.GetRelatedSeizureToPermitLink.Entities.Count = 0 Then
            '                            Dim NotifySpecies As DataObjects.EntitySet.NotificationSpecieLinkSet = NotificationDO.GetRelatedNotificationSpecieLink()
            '                            If Not NotifySpecies Is Nothing AndAlso _
            '                               NotifySpecies.Count > 0 Then
            '                                If CollectionCount = 0 Then Me.SetPermits(Nothing)
            '                                For Each permit As BO.Application.BOPermit In Me.Permit
            '                                    Dim SpecieId As Int32 = CType(NotifySpecies.GetEntity(0), DataObjects.Entity.NotificationSpecieLink).SpecieId
            '                                    Dim SpecieDO As New DataObjects.Entity.Specie(SpecieId)

            '                                    If (Not SpecieDO.IsScientificNameNull AndAlso _
            '                                       String.Compare(SpecieDO.ScientificName, permit.Specie.ScientificName) = 0) AndAlso _
            '                                       Date.Compare(NotificationDO.DateOfImport, Date.Today) < CType(Result, Int32) Then

            '                                        ReDim Preserve ReturnNotifications(CollectionCount)
            '                                        ReturnNotifications(CollectionCount) = New BO.Application.CITES.SeizureNotification.BOSeizureNotification(SN.Id)
            '                                        CollectionCount += 1
            '                                    End If
            '                                Next permit
            '                            End If
            '                        End If
            '                    Next NotificationDO
            '                End If
            '            Catch ex As Exception

            '            End Try
            '        Next
            '    End If
            '    Config = Nothing
            'End If

            'Return ReturnNotifications
        End Function

#End Region





      
    End Class
End Namespace
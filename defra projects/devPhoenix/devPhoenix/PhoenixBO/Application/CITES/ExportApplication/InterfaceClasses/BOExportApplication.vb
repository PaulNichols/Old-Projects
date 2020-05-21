Namespace Application.CITES.Applications
    Public Class BOExportApplication
        Inherits Application.CITES.Applications.BOImportExportApplication
        Implements IBOExportApplication

#Region " Prelim code "
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal exportApplicationId As Int32)
            MyClass.New()
            LoadExportApplication(exportApplicationId)
        End Sub

        Public Sub New(ByVal exportApplicationId As Int32, ByVal tran As SqlClient.SqlTransaction) 'MLD 2/12/4
            MyClass.New()
            LoadExportApplication(exportApplicationId, tran)
        End Sub

        Private Function LoadExportApplication(ByVal id As Int32) As DataObjects.Entity.ExportApplication
            Return LoadExportApplication(id, Nothing)
        End Function

        Private Function LoadExportApplication(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.ExportApplication
            Dim NewExportApplication As DataObjects.Entity.ExportApplication = DataObjects.Entity.ExportApplication.GetById(id)
            If NewExportApplication Is Nothing Then
                Throw New RecordDoesNotExist("ExportApplication", id)
            Else
                InitialiseExportApplication(NewExportApplication, tran)
                Return NewExportApplication
            End If
        End Function

        Public Overridable Sub InitialiseExportApplication(ByVal applicationId As Int32)
            InitialiseExportApplication(applicationId, Nothing)
        End Sub

        Public Overridable Sub InitialiseExportApplication(ByVal applicationId As Int32, ByVal tran As SqlClient.SqlTransaction)
            Dim ExportApp As DataObjects.Entity.ExportApplication = Application.CITES.Applications.BOCITESImportExportPermit.GetExportApplicationDO(applicationId)
            If Not ExportApp Is Nothing Then
                InitialiseExportApplication(ExportApp, tran)
            End If
        End Sub

        Protected Overloads Overrides Sub InitialiseImportExportApplication(ByVal citesImportExportApplication As EnterpriseObjects.Entity, ByVal tran As System.Data.SqlClient.SqlTransaction)
            InitialiseExportApplication(CType(citesImportExportApplication, DataObjects.Entity.ExportApplication), tran)
        End Sub

        Friend Overridable Sub InitialiseExportApplication(ByVal citesExportApplication As DataObjects.Entity.ExportApplication, ByVal tran As SqlClient.SqlTransaction)
            With citesExportApplication
                Dim ExportCountry As Object = Nothing
                Dim ImportCountry As Object = Nothing
                Dim ExportRegion As Object = Nothing
                Dim ImportRegion As Object = Nothing
                If Not .IsCountryOfExportIdNull Then ExportCountry = .CountryOfExportId
                If Not .IsRegionOfExportIdNull Then ExportRegion = .RegionOfExportId
                If Not .IsRegionOfImportIdNull Then ImportRegion = .RegionOfImportId

                InitialiseImportExportApplication(.CitesApplicationId, .CheckSum, .Id, ExportCountry, ImportCountry, ExportRegion, ImportRegion, tran)

                mReExport = .ReExport
            End With
        End Sub
#End Region

#Region " Properties "

        Public Overrides Property ApplicationTypeId() As Int32  'MLD added 16/12/4
            Get
                Return Application.ApplicationTypes.Export
            End Get
            Set(ByVal Value As Int32)
            End Set
        End Property

        Public Overrides Property CountryOfExport() As ReferenceData.BOCountry
            Get
                If mCountryOfExport Is Nothing OrElse mCountryOfExport.ID = 0 Then
                    Dim c As New BO.BOConfiguration
                    Dim Result As Object = c.GetValue("DefaultCountry")
                    If Not Result Is Nothing AndAlso _
                        c.IsInt32(Result) Then
                        mCountryOfExport = New ReferenceData.BOCountry(CType(Result, Int32))
                    Else
                        mCountryOfExport = Nothing
                    End If
                    c = Nothing
                End If
                Return mCountryOfExport
            End Get
            Set(ByVal Value As ReferenceData.BOCountry)
                mCountryOfExport = Value
            End Set
        End Property

        Public Overrides Property CountryOfImport() As ReferenceData.BOCountry
            Get
                If mCountryOfImport Is Nothing OrElse mCountryOfImport.ID = 0 Then
                    'Dim Config As New BO.BOConfiguration
                    'Dim Result As Object = Config.GetValue("DefaultCountry")
                    'If Not Result Is Nothing AndAlso _
                    '    Config.IsInt32(Result) Then
                    If Not SecondParty Is Nothing AndAlso Not SecondParty.Address Is Nothing AndAlso SecondParty.Address.CountryId > 0 Then
                        mCountryOfImport = New ReferenceData.BOCountry(SecondParty.Address.CountryId)
                    Else
                        mCountryOfImport = Nothing
                    End If
                    'Config = Nothing
                End If
                Return mCountryOfImport
            End Get
            Set(ByVal Value As ReferenceData.BOCountry)
                mCountryOfImport = Value
            End Set
        End Property

        Public Property ExportApplicationCheckSum() As Integer Implements IBOExportApplication.ExportApplicationCheckSum
            Get
                Return MyBase.ImportExportApplicationCheckSum
            End Get
            Set(ByVal Value As Integer)
                MyBase.ImportExportApplicationCheckSum = Value
            End Set
        End Property

        Public Property ExportApplicationId() As Integer Implements IBOExportApplication.ExportApplicationId
            Get
                Return MyBase.ImportExportApplicationId
            End Get
            Set(ByVal Value As Integer)
                MyBase.ImportExportApplicationId = Value
            End Set
        End Property

        Public Overrides Property Exporter() As BOApplicationPartyDetails
            Get
                Return MyBase.Party
            End Get
            Set(ByVal Value As BOApplicationPartyDetails)
                'commented out because there was a problem, the party was always nothing
                'when serialised
                ' MyBase.Party = Value
            End Set
        End Property

        Public Overrides Property Importer() As BOApplicationPartyDetails
            Get
                Return MyBase.SecondParty
            End Get
            Set(ByVal Value As BOApplicationPartyDetails)
                'commented out because there was a problem, the party was always nothing
                'when serialised

                ' MyBase.SecondParty = Value
            End Set
        End Property

        Public Property ReExport() As Boolean Implements IBOExportApplication.ReExport
            Get
                Return mReExport
            End Get
            Set(ByVal Value As Boolean)
                mReExport = Value
            End Set
        End Property
        Private mReExport As Boolean
#End Region

#Region " Helper Functions "

    

        Protected Overrides Function GetChecksum(ByVal entity As EnterpriseObjects.Entity) As Integer
            Return CType(entity, DataObjects.Entity.ExportApplication).CheckSum
        End Function

        Protected Overrides Function GetID(ByVal entity As EnterpriseObjects.Entity) As Integer
            Return CType(entity, DataObjects.Entity.ExportApplication).Id
        End Function

        Friend Overrides ReadOnly Property CheckAuthorisedLocation(ByVal permit As Application.CITES.Applications.BOCITESImportExportPermit) As Boolean
            Get
                Return False
            End Get
        End Property
#End Region

#Region " Save "
        Protected Overrides Function GetSaveService() As EnterpriseObjects.Service
            Dim NewExportApplication As New DataObjects.Entity.ExportApplication
            Return NewExportApplication.ServiceObject
        End Function

        Protected Overrides Function InsertApp(ByVal citesApplicationId As Integer, ByVal countryofExportId As Object, ByVal regionOfExportId As Object, ByVal regionOfImportId As Object, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity
            If (CountryOfExport Is Nothing OrElse CountryOfExport.ID = 0) AndAlso Not Me.SecondParty Is Nothing Then
                CountryOfExport = New BO.ReferenceData.BOCountry(Me.SecondParty.Address.CountryId)
                countryofExportId = CountryOfExport.ID
            End If
            Return CType(GetSaveService(), DataObjects.service.ExportApplicationService).Insert(citesApplicationId, countryofExportId, mReExport, regionOfExportId, regionOfImportId, tran)
        End Function

        Protected Overrides Function UpdateApp(ByVal importExportApplicationId As Integer, ByVal citesApplicationId As Integer, ByVal countryofExportId As Object, ByVal regionOfExportId As Object, ByVal regionOfImportId As Object, ByVal checkSum As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity
            If (CountryOfExport Is Nothing OrElse CountryOfExport.ID = 0) AndAlso Not Me.SecondParty Is Nothing Then
                CountryOfExport = New BO.ReferenceData.BOCountry(Me.SecondParty.Address.CountryId)
                countryofExportId = CountryOfExport.ID
            End If
            Return CType(GetSaveService(), DataObjects.service.ExportApplicationService).Update(importExportApplicationId, citesApplicationId, countryofExportId, mReExport, regionOfExportId, regionOfImportId, checkSum, tran)
        End Function

        'Public Overridable Overloads Function Save(ByVal tran As System.Data.SqlClient.SqlTransaction) As BaseBO
        '    'as this record has been considered valid - we must ensure this continues by prevalidating
        '    Return MyBase.Save(tran)
        'End Function
#End Region

#Region " Validate "
        Protected Overloads Overrides Function Validate(ByVal writeFlag As Boolean, ByVal ignoreWarnings As Boolean, ByVal saveApplication As Boolean) As BaseBO
            MyBase.Validate(writeFlag, ignoreWarnings, saveApplication)
            Dim ReturnObj As BaseBO = Me
            ReturnObj.ValidationErrors = New ValidationManager(ValidationManager.ValidationTitles.CannotSaveExportApplication, ignoreWarnings)

            'check the species to see if we have any EC Annex D
            Dim FoundAnnexD As Boolean = False
            Dim FoundAnnexC As Boolean = False
            Dim FoundAppendixI As Boolean = ignoreWarnings
            Dim FoundSpecimen As Boolean = False
            Dim FoundUOMError As Boolean = False

            Dim Config As New BOConfiguration
            CheckMandatory(ReturnObj)

            If Not Me.Permit Is Nothing AndAlso _
               Permit.Length > 0 Then
                For Each _Permit As Application.CITES.Applications.BOCITESImportExportPermit In Permit
                    ' check mandatory
                    CheckMandatory(ReturnObj, _permit)

                    ' ensure UOM's are of the same type
                    Dim UOMDetails As BOMeasurement = Nothing
                    If Not FoundUOMError AndAlso _
                       Not _permit.Specimens Is Nothing AndAlso _
                       _permit.Specimens.Length > 0 Then
                        For Each _Specimen As Application.BOSpecimen In _permit.Specimens
                            If UOMDetails Is Nothing Then
                                ' first time through
                                UOMDetails = _Specimen.UOM
                            ElseIf Not BOMeasurement.IsEqual(UOMDetails, _Specimen.UOM) Then
                                ' ensure that they are the same
                                ReturnObj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.UOMDifferBetweenSpecimens))
                                FoundUOMError = True
                                Exit For
                            End If
                        Next _Specimen
                    End If

                    'check to see if we've found them all, if so may as well bail
                    If FoundAnnexD AndAlso FoundAnnexC AndAlso FoundAppendixI AndAlso FoundSpecimen Then Exit For

                    'more to check, so check 'em
                    If Not _Permit.Specie Is Nothing Then
                        If Not FoundAnnexD AndAlso _
                           _Permit.Specie.IsAnnexD Then
                            ReturnObj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.SpecimensCannotBeAnnexD))
                            FoundAnnexD = True
                        End If
                        If Not FoundAnnexC AndAlso _
                           _Permit.Specie.IsAnnexC AndAlso _
                           Not _Permit.Specie.IsListedOnAppendix Then
                            ReturnObj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.SpecimensCannotBeAnnexC))
                            FoundAnnexC = True
                        End If
                        If Not FoundAppendixI AndAlso _
                           _Permit.Specie.IsAppendixI AndAlso _
                           (Config.IsInExportAppendixISpecimenSource(_Permit.Source1.Code) OrElse _
                           Config.IsInExportAppendixISpecimenSource(_Permit.Source2.Code)) AndAlso _
                           Config.IsInExportAppendixISpecimenPurpose(_Permit.Purpose.Code) Then
                            ReturnObj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.SpecimensCannotBeAnnexI))
                            FoundAppendixI = True
                        End If
                        If Not FoundSpecimen AndAlso _
                           _Permit.Specimens Is Nothing OrElse _
                           _permit.Specimens.Length = 0 Then
                            ReturnObj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.APermitMustHaveAtLeastOneSpecimen))
                            FoundSpecimen = True
                        End If
                    End If
                Next _Permit
            Else
                'no permits!
                ReturnObj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.AnApplicationMustHaveAtLeastOnePermit))
            End If

            'the country of import must NOT be an EC member state   MLD 12/10/4
            Dim GWDCountry As ReferenceData.BOGWDCountry = Me.CountryOfImport.GetGWDCountry()
            If GWDCountry Is Nothing OrElse GWDCountry.ECMemberState Then
                ReturnObj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.CountryOfImportMustNotBeAnECState))
            End If

            If Exporter Is Nothing Then
                ReturnObj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.ExporterRequired))
            Else
                'check semi-completes flag
                If Not Exporter.Party.AllowSemicompleteCitesExport AndAlso _
                   IsSemiComplete Then
                    ReturnObj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.PartyIsNotAllowedToProcessSemiCompletes))
                ElseIf Exporter.Party.AllowSemicompleteCitesExport AndAlso _
                   IsSemiComplete Then
                    'allowed to semi complete the data
                Else
                    'not allowed to semi-complete or is a non-semi complete app, 
                    'so check that the non semi complete requirements have been met.
                    If CountryOfImport Is Nothing OrElse CountryOfImport.ID = 0 Then
                        ReturnObj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.NonSemiCompleteCountryOfExportNotDefined))
                    End If
                    If Importer Is Nothing OrElse Importer.Party Is Nothing OrElse Importer.Party.PartyId = 0 Then
                        ReturnObj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.NonSemiCompleteImporterNotDefined))
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

            'TODO: UC 205, business rule 10 needs working on
            If MyBase.ValidationErrors.HasErrors Then
                If writeFlag Then CType(ReturnObj, IApplication).Validated = False
            Else
                If writeFlag Then CType(ReturnObj, IApplication).Validated = True
                ReturnObj.ValidationErrors = Nothing
            End If

            If saveApplication Then
                ReturnObj = MyBase.Save(True)
            End If

            Return ReturnObj
        End Function
#End Region

#Region " Operations "

        Public Overrides Function Clone() As Object
            Dim BaseApplication As Application.CITES.Applications.BOExportApplication = CType(MyBase.Clone, Application.CITES.Applications.BOExportApplication)
            With BaseApplication
                .ExportApplicationId = 0
            End With
            Return BaseApplication
        End Function

        Protected Overloads Overrides Sub CheckMandatory(ByVal validationOwner As BaseBO, ByVal permit As Application.CITES.Applications.BOCITESImportExportPermit)
            MyBase.CheckMandatory(validationOwner, permit)

            'Dim ErrorType As ValidationError.ValidationCodes

            'ErrorType = ValidationError.ValidationCodes.ScientificNameCannotBeBlank
            'If Not CheckErrorExists(validationOwner, ErrorType) AndAlso _
            '   (permit..Specie Is Nothing OrElse _
            '   permit.Specie.ScientificName Is Nothing OrElse _
            '   permit.Specie.ScientificName.Length > 0) Then
            '    validationOwner.ValidationErrors.AddError(New ValidationError(ErrorType))
            'End If
        End Sub
#End Region


    End Class
End Namespace
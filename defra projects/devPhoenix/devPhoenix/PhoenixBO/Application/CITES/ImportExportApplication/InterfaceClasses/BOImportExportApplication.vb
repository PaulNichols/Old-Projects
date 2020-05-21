Namespace Application.CITES.Applications
    Public MustInherit Class BOImportExportApplication
        Inherits Application.CITES.Applications.BOCITESApplication
        Implements BO.Application.CITES.Applications.IBOImportExportApplication

        Protected MustOverride Function GetSaveService() As EnterpriseObjects.Service
        Protected MustOverride Function InsertApp(ByVal citesApplicationId As Int32, ByVal countryofExportId As Object, ByVal regionOfExportId As Object, ByVal regionOfImportId As Object, ByVal tran As SqlClient.SqlTransaction) As EnterpriseObjects.Entity
        Protected MustOverride Function UpdateApp(ByVal importExportApplicationId As Int32, ByVal citesApplicationId As Int32, ByVal countryofExportId As Object, ByVal regionOfExportId As Object, ByVal regionOfImportId As Object, ByVal checkSum As Int32, ByVal tran As SqlClient.SqlTransaction) As EnterpriseObjects.Entity
        Protected MustOverride Function GetChecksum(ByVal entity As EnterpriseObjects.Entity) As Int32
        Protected MustOverride Function GetID(ByVal entity As EnterpriseObjects.Entity) As Int32
        Protected MustOverride Sub InitialiseImportExportApplication(ByVal citesImportExportApplication As EnterpriseObjects.Entity, ByVal tran As SqlClient.SqlTransaction)
        Friend MustOverride ReadOnly Property CheckAuthorisedLocation(ByVal permit As Application.CITES.Applications.BOCITESImportExportPermit) As Boolean

#Region " Prelim code "
        'Public Sub New()
        '    MyBase.New()
        'End Sub

        'Public Sub New(ByVal importApplicationId As Int32)
        '    MyClass.New()
        '    LoadImportExportApplication(importApplicationId)
        'End Sub

        Protected Sub InitialiseImportExportApplication(ByVal citesApplicationId As Int32, ByVal checkSum As Int32, _
                                                        ByVal id As Int32, ByVal countryOfExportId As Object, _
                                                        ByVal countryOfImportId As Object, _
                                                        ByVal regionOfExportId As Object, ByVal regionOfImportId As Object, _
                                                        ByVal tran As SqlClient.SqlTransaction)
            MyBase.InitialiseCITESApplication(New DataObjects.Entity.CITESApplication(citesApplicationId, tran), tran)
            mImportExportApplicationCheckSum = checkSum
            mImportExportApplicationId = id

            If Not countryOfExportId Is Nothing Then mCountryOfExport = New ReferenceData.BOCountry(CType(countryOfExportId, Int32))
            If Not regionOfExportId Is Nothing Then mRegionOfExport = New ReferenceData.BOUKCountry(CType(regionOfExportId, Int32))
            If Not regionOfImportId Is Nothing Then mRegionOfImport = New ReferenceData.BOUKCountry(CType(regionOfImportId, Int32))
        End Sub

        'Public Overrides Sub SetPermits(ByVal tran As SqlClient.SqlTransaction)
        '    MyBase.SetPermits(tran)
        '    Dim Permits As BOPermit()
        '    If Not MyBase.Permit Is Nothing Then
        '        ReDim Permits(MyBase.Permit.Length - 1)
        '        Dim Index As Int32 = 0
        '        For Each citesPermit As BOCITESPermit In MyBase.Permit
        '            Dim ImportExportPermit As New DataObjects.Entity.CITESImportExportPermit
        '            Dim ImportExportPermits As DataObjects.EntitySet.CITESImportExportPermitSet = ImportExportPermit.GetForCITESPermit(citesPermit.CITESPermitId, tran)
        '            If Not ImportExportPermits Is Nothing AndAlso _
        '               ImportExportPermits.Count > 0 Then
        '                For Each ImportExportPermit In ImportExportPermits
        '                    Dim BOImportExportPerm As New BOCITESImportExportPermit
        '                    BOImportExportPerm.InitialiseCITESImportExportPermit(ImportExportPermit, tran)
        '                    Permits(Index) = BOImportExportPerm
        '                    Index += 1
        '                Next ImportExportPermit
        '            Else
        '                MyBase.Permit = Nothing
        '            End If
        '            ImportExportPermit = Nothing
        '            ImportExportPermits = Nothing
        '        Next citesPermit
        '        MyBase.Permit = Permits
        '    End If
        'End Sub
#End Region

#Region " Properties "
        Public MustOverride Property Exporter() As BOApplicationPartyDetails Implements IBOImportExportApplication.Exporter
        Public MustOverride Property Importer() As BOApplicationPartyDetails Implements IBOImportExportApplication.Importer

        Public Overridable Property CountryOfExport() As ReferenceData.BOCountry Implements IBOImportExportApplication.CountryOfExport
            Get
                Return mCountryOfExport
            End Get
            Set(ByVal Value As ReferenceData.BOCountry)
                mCountryOfExport = Value
            End Set
        End Property
        Protected mCountryOfExport As ReferenceData.BOCountry

        Public Property RegionOfImport() As ReferenceData.BOUKCountry Implements IBOImportExportApplication.RegionOfImport
            Get
                Return mRegionOfImport
            End Get
            Set(ByVal Value As ReferenceData.BOUKCountry)
                mRegionOfImport = Value
            End Set
        End Property
        Private mRegionOfImport As ReferenceData.BOUKCountry

        Public Property RegionOfExport() As ReferenceData.BOUKCountry Implements IBOImportExportApplication.RegionOfExport
            Get
                Return mRegionOfExport
            End Get
            Set(ByVal Value As ReferenceData.BOUKCountry)
                mRegionOfExport = Value
            End Set
        End Property
        Private mRegionOfExport As ReferenceData.BOUKCountry

        Public Property ImportExportApplicationCheckSum() As Integer Implements IBOImportExportApplication.ImportExportApplicationCheckSum
            Get
                Return mImportExportApplicationCheckSum
            End Get
            Set(ByVal Value As Integer)
                mImportExportApplicationCheckSum = Value
            End Set
        End Property
        Private mImportExportApplicationCheckSum As Int32

        Public Property ImportExportApplicationId() As Integer Implements IBOImportExportApplication.ImportExportApplicationId
            Get
                Return mImportExportApplicationId
            End Get
            Set(ByVal Value As Integer)
                mImportExportApplicationId = Value
            End Set
        End Property
        Private mImportExportApplicationId As Int32
#End Region

#Region " Helper Functions "

        Public Overrides Function GetNewPermit() As BOCITESPermit
            Dim Permit As New BO.Application.CITES.Applications.BOCITESImportExportPermit
            Select Case CType(ApplicationTypeId, Application.ApplicationTypes)
                Case Application.ApplicationTypes.Import
                    Permit.CofLExport = Me.CountryOfExport
                Case Application.ApplicationTypes.Export

            End Select

            Permit.IsRetrospective = Retrospective
            Permit.ApplicationId = ApplicationId

            Return Permit
        End Function

        Private ReadOnly Property CountryOfExportId() As Object
            Get
                If CountryOfExport Is Nothing OrElse CountryOfExport.ID = 0 Then
                    Return Nothing
                Else
                    Return CountryOfExport.ID
                End If
            End Get
        End Property

        Private ReadOnly Property RegionOfExportId() As Object
            Get
                If RegionOfExport Is Nothing OrElse RegionOfExport.ID = 0 Then
                    Return Nothing
                Else
                    Return RegionOfExport.ID
                End If
            End Get
        End Property

        Private ReadOnly Property RegionOfImportId() As Object
            Get
                If RegionOfImport Is Nothing OrElse RegionOfImport.ID = 0 Then
                    Return Nothing
                Else
                    Return RegionOfImport.ID
                End If
            End Get
        End Property

        Public Shared ReadOnly Property CheckBirthdateEntryRequired(ByVal permit As Application.CITES.Applications.BOCITESImportExportPermit) As Boolean
            Get
                Dim Config As New BOConfiguration
                Return Config.IsLiveDerivative(permit.Derivative)
            End Get
        End Property

        Public Shared ReadOnly Property CheckAuthorisedLocationRequired(ByVal permit As Application.CITES.Applications.BOCITESImportExportPermit) As Boolean
            Get
                Dim x As New Application.CITES.Applications.BOImportApplication
                Return x.CheckAuthorisedLocation(permit)
            End Get
        End Property

        Public Shared ReadOnly Property CheckAuthorisedLocationRequired(ByVal permits() As Application.CITES.Applications.BOCITESImportExportPermit) As Boolean
            Get
                Return CheckAuthorisedLocationRequired_Main(permits)
            End Get
        End Property

        Public Shared ReadOnly Property CheckAuthorisedLocationRequired(ByVal permits() As Object) As Boolean
            Get
                If permits Is Nothing Then Return False
                Return CheckAuthorisedLocationRequired_Main(permits)
            End Get
        End Property

        Private Shared ReadOnly Property CheckAuthorisedLocationRequired_Main(ByVal permits() As Object) As Boolean
            Get
                For Each permit As Application.CITES.Applications.BOCITESImportExportPermit In permits
                    ' if any permit is allowed then proceed
                    If CheckAuthorisedLocationRequired(permit) Then
                        Return True
                    End If
                Next permit
                Return False
            End Get
        End Property

#End Region

#Region " Save "
        Public Overloads Overrides Function Save(ByVal ignoreValidation As Boolean) As BaseBO
            Dim service As DataObjects.Service.ImportApplicationService = DataObjects.Entity.ImportApplication.ServiceObject
            Dim tran As SqlClient.SqlTransaction = service.BeginTransaction

            Dim SaveResult As BaseBO = MyClass.Save(tran, ignoreValidation)
            If SaveResult Is Nothing Then
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
            Else
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Commit)
            End If
            Return SaveResult
        End Function

        Public Overridable Overloads Function Save(ByVal tran As SqlClient.SqlTransaction, ByVal ignoreValidation As Boolean) As BaseBO
            Dim NewImportExportApplication As EnterpriseObjects.Entity
            Dim service As EnterpriseObjects.Service = GetSaveService()

            Created = (mImportExportApplicationId = 0)

            Dim CITESApplication As Object = MyBase.Save(tran, ignoreValidation)

            If Not CITESApplication Is Nothing AndAlso _
               Not CType(CITESApplication, BaseBO).ValidationErrors Is Nothing Then
                'rollback the transaction
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                'get the problems and assign them locally
                ValidationErrors = CType(CITESApplication, BaseBO).ValidationErrors
                'bail
                Return Me
            End If

            If Created Then
                NewImportExportApplication = InsertApp(CITESApplicationId, _
                                                       CountryOfExportId, _
                                                       RegionOfImportId, _
                                                       RegionOfExportId, _
                                                       tran)
            Else
                NewImportExportApplication = UpdateApp(mImportExportApplicationId, _
                                                       CITESApplicationId, _
                                                       CountryOfExportId, _
                                                       RegionOfImportId, _
                                                       RegionOfExportId, _
                                                       mImportExportApplicationCheckSum, _
                                                       tran)
            End If


            ''check to see if any SQL errors have occured
            If (NewImportExportApplication Is Nothing AndAlso Not DataObjects.Sprocs.LastError Is Nothing) Then
                'TODO: Use errors collection to check to see if the problem was concurrency
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveApplication)
                Return Me
            ElseIf Created And Not NewImportExportApplication Is Nothing Then
                mImportExportApplicationId = GetID(NewImportExportApplication)
            End If

            Try
                If GetChecksum(NewImportExportApplication) <> mImportExportApplicationCheckSum Then
                    InitialiseImportExportApplication(NewImportExportApplication, tran)
                End If
            Catch ex As Exception
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSavePermit)
            End Try
            Return Me
        End Function
#End Region

#Region " Operations "
        Protected Overridable Overloads Sub CheckMandatory(ByVal validationOwner As BaseBO, ByVal permit As Application.CITES.Applications.BOCITESImportExportPermit)

            Dim ErrorType As ValidationError.ValidationCodes

            ErrorType = ValidationError.ValidationCodes.ScientificNameCannotBeBlank
            If Not CheckErrorExists(validationOwner, ErrorType) AndAlso _
               (permit.Specie Is Nothing OrElse _
               permit.Specie.ScientificName Is Nothing OrElse _
               permit.Specie.ScientificName.Length = 0) Then
                validationOwner.ValidationErrors.AddError(New ValidationError(ErrorType))
            End If

            ErrorType = ValidationError.ValidationCodes.CountryOfOriginCannotBeBlank
            If Not CheckErrorExists(validationOwner, ErrorType) AndAlso _
               permit.CountryOfOrigin Is Nothing Then
                validationOwner.ValidationErrors.AddError(New ValidationError(ErrorType))
            End If

            ErrorType = ValidationError.ValidationCodes.CountryOfLastExportCannotBeBlank
            If Not CheckErrorExists(validationOwner, ErrorType) AndAlso _
               permit.CofLExport Is Nothing Then
                validationOwner.ValidationErrors.AddError(New ValidationError(ErrorType))
            End If

            ErrorType = ValidationError.ValidationCodes.SourceCodeCannotBeBlank
            If Not CheckErrorExists(validationOwner, ErrorType) AndAlso _
               (permit.Source1 Is Nothing OrElse _
               permit.Source2 Is Nothing) Then
                validationOwner.ValidationErrors.AddError(New ValidationError(ErrorType))
            End If

            ErrorType = ValidationError.ValidationCodes.PurposeCodeCannotBeBlank
            If Not CheckErrorExists(validationOwner, ErrorType) AndAlso _
               permit.Purpose Is Nothing Then
                validationOwner.ValidationErrors.AddError(New ValidationError(ErrorType))
            End If

            ErrorType = ValidationError.ValidationCodes.PartDerivativeCannotBeBlank
            If Not CheckErrorExists(validationOwner, ErrorType) AndAlso _
               permit.Derivative Is Nothing Then
                validationOwner.ValidationErrors.AddError(New ValidationError(ErrorType))
            End If
        End Sub
#End Region

        '#Region " Validate "
        '        Public Overridable Overloads Function Validate(ByVal writeFlag As Boolean, ByVal ignoreWarnings As Boolean) As Object Implements IBOImportExportApplication.Validate
        '            Return Validate(writeFlag, ignoreWarnings)
        '        End Function


        '#End Region

     
    End Class
End Namespace
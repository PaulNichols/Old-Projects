Imports uk.gov.defra.Phoenix.BO.ValidationError
Imports uk.gov.defra.Phoenix.BO.ValidationError.ValidationCodes
Imports uk.gov.defra.Phoenix.BO.ReportCriteria

Namespace Application.CITES.Applications
    Public Class BOCITESArticle10
        Inherits BOCITESApplication
        Implements IBOCITESArticle10



#Region " Prelim code "
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal citesArticle10Id As Int32)
            MyClass.New()
            LoadArticle10(citesArticle10Id)
        End Sub

        Public Sub New(ByVal citesArticle10Id As Int32, ByVal tran As SqlClient.SqlTransaction)  'MLD 2/12/4
            MyClass.New()
            LoadArticle10(citesArticle10Id, tran)
        End Sub

        Public Function LoadArticle10(ByVal id As Int32) As DataObjects.Entity.Article10
            Return LoadArticle10(id, Nothing)
        End Function

        Private Function LoadArticle10(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.Article10
            Dim NewArticle10 As DataObjects.Entity.Article10 = DataObjects.Entity.Article10.GetById(id, tran)
            If NewArticle10 Is Nothing Then
                Throw New RecordDoesNotExist("Article10", id)
            Else
                InitialiseArticle10(NewArticle10, tran)
                Return NewArticle10
            End If
        End Function

        Public Overridable Sub InitialiseArticle10(ByVal applicationId As Int32)
            InitialiseArticle10(applicationId, Nothing)
        End Sub

        Public Overridable Sub InitialiseArticle10(ByVal applicationId As Int32, ByVal tran As SqlClient.SqlTransaction)
            Dim App As DataObjects.Entity.Article10 = Application.CITES.Applications.BOCITESApplication.GetArticle10ApplicationDO(applicationId)
            If Not App Is Nothing Then
                InitialiseArticle10(App, tran)
            End If
        End Sub



        Public Overridable Sub InitialiseArticle10(ByVal article10 As DataObjects.Entity.Article10, ByVal tran As SqlClient.SqlTransaction)
            With article10
                'get the base class data
                MyBase.InitialiseCITESApplication(New DataObjects.Entity.CITESApplication(.CitesApplicationId, tran), tran)

                'set myself up
                mArticle10Id = .Id
                mArticle10CheckSum = .CheckSum

                If Not .IsArticle10TypeIdNull Then mArticle10Type = New BO.ReferenceData.BOArticle10CertificateType(.Article10TypeId)
                mBox18(0) = .Box18_1
                mBox18(1) = .Box18_2
                mBox18(2) = .Box18_3
                mBox18(3) = .Box18_4
                mBox18(4) = .Box18_5
                mBox18(5) = .Box18_6
                mBox18(6) = .Box18_7
                mBox18(7) = .Box18_8

            End With
        End Sub
#End Region

#Region " Properties "

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

        Public Overrides Property ApplicationTypeId() As Int32  'MLD added 16/12/4
            Get
                Return Application.ApplicationTypes.Article10
            End Get
            Set(ByVal Value As Int32)
            End Set
        End Property

        Public Property Holder() As BO.Application.BOApplicationPartyDetails Implements IBOCITESArticle10.Holder
            Get
                Return MyBase.Party
            End Get
            Set(ByVal Value As BO.Application.BOApplicationPartyDetails)
                MyBase.Party = Value
            End Set
        End Property

        Public Property Article10Id() As Int32 Implements IBOCITESArticle10.Article10Id
            Get
                Return mArticle10Id
            End Get
            Set(ByVal Value As Int32)
                mArticle10Id = Value
            End Set
        End Property
        Private mArticle10Id As Int32

        Public Property AquisitionDetails() As String Implements IBOCITESArticle10.AquisitionDetails
            Get

            End Get
            Set(ByVal Value As String)

            End Set
        End Property

        Public Property Article10TypeString() As String Implements IBOCITESArticle10.Article10TypeString
            Get
                If Not Article10TypeId Is Nothing Then
                    Dim BOArticle10CertificateType As New BO.ReferenceData.BOArticle10CertificateType(CType(Article10TypeId, Int32))
                    Return BOArticle10CertificateType.Description
                End If
            End Get
            Set(ByVal Value As String)

            End Set
        End Property

        Public Property Article10Type() As BO.ReferenceData.BOArticle10CertificateType Implements IBOCITESArticle10.Article10Type
            Get
                Return mArticle10Type
            End Get
            Set(ByVal Value As BO.ReferenceData.BOArticle10CertificateType)
                mArticle10Type = Value
            End Set
        End Property
        Private mArticle10Type As BO.ReferenceData.BOArticle10CertificateType

        Private mBox18(7) As Boolean
        Public Property Box18_1() As Boolean Implements IBOCITESArticle10.Box18_1
            Get
                Return mBox18(0)
            End Get
            Set(ByVal Value As Boolean)
                mBox18(0) = Value
            End Set
        End Property

        Public Property Box18_2() As Boolean Implements IBOCITESArticle10.Box18_2
            Get
                Return mBox18(1)
            End Get
            Set(ByVal Value As Boolean)
                mBox18(1) = Value
            End Set
        End Property

        Public Property Box18_3() As Boolean Implements IBOCITESArticle10.Box18_3
            Get
                Return mBox18(2)
            End Get
            Set(ByVal Value As Boolean)
                mBox18(2) = Value
            End Set
        End Property

        Public Property Box18_4() As Boolean Implements IBOCITESArticle10.Box18_4
            Get
                Return mBox18(3)
            End Get
            Set(ByVal Value As Boolean)
                mBox18(3) = Value
            End Set
        End Property

        Public Property Box18_5() As Boolean Implements IBOCITESArticle10.Box18_5
            Get
                Return mBox18(4)
            End Get
            Set(ByVal Value As Boolean)
                mBox18(4) = Value
            End Set
        End Property

        Public Property Box18_6() As Boolean Implements IBOCITESArticle10.Box18_6
            Get
                Return mBox18(5)
            End Get
            Set(ByVal Value As Boolean)
                mBox18(5) = Value
            End Set
        End Property

        Public Property Box18_7() As Boolean Implements IBOCITESArticle10.Box18_7
            Get
                Return mBox18(6)
            End Get
            Set(ByVal Value As Boolean)
                mBox18(6) = Value
            End Set
        End Property

        Public Property Box18_8() As Boolean Implements IBOCITESArticle10.Box18_8
            Get
                Return mBox18(7)
            End Get
            Set(ByVal Value As Boolean)
                mBox18(7) = Value
            End Set
        End Property

        Public Property Article10CheckSum() As Int32 Implements IBOCITESArticle10.Article10CheckSum
            Get
                Return mArticle10CheckSum
            End Get
            Set(ByVal Value As Int32)
                mArticle10CheckSum = Value
            End Set
        End Property
        Private mArticle10CheckSum As Int32

        Public Shared ReadOnly Property CheckBirthdateEntryRequired(ByVal permit As Application.CITES.Applications.BOCITESImportExportPermit) As Boolean
            Get
                Dim Config As New BOConfiguration
                Return Config.IsLiveDerivative(permit.Derivative)
            End Get
        End Property
#End Region

#Region " Helper Functions "
        Private ReadOnly Property Article10TypeId() As Object
            Get
                If mArticle10Type Is Nothing Then
                    Return Nothing
                Else
                    Return mArticle10Type.ID
                End If
            End Get
        End Property

        Friend ReadOnly Property CheckAuthorisedLocation(ByVal permit As Application.CITES.Applications.BOCITESImportExportPermit) As Boolean
            Get
                Dim Config As New BOConfiguration
                Return (Config.IsInAuthorisedLocation(permit.Source1.Code) OrElse _
                        Config.IsInAuthorisedLocation(permit.Source2.Code)) AndAlso _
                        Config.IsLiveDerivative(permit.Derivative)
            End Get
        End Property

        Public Shared ReadOnly Property CheckAuthorisedLocationRequired(ByVal permit As Application.CITES.Applications.BOCITESImportExportPermit) As Boolean
            Get
                Dim x As New Application.CITES.Applications.BOCITESArticle10
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
            Dim NewArticle10Application As New DataObjects.Entity.Article10
            Dim service As DataObjects.Service.Article10Service = NewArticle10Application.ServiceObject
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
            Dim NewArticle10Application As New DataObjects.Entity.Article10
            Dim service As DataObjects.Service.Article10Service = NewArticle10Application.ServiceObject

            Created = (mArticle10Id = 0)

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
                NewArticle10Application = service.Insert(CITESApplicationId, _
                                                         Article10TypeId, _
                                                         mBox18(0), _
                                                         mBox18(1), _
                                                         mBox18(2), _
                                                         mBox18(3), _
                                                         mBox18(4), _
                                                         mBox18(5), _
                                                         mBox18(6), _
                                                         mBox18(7), _
                                                          tran)
            Else
                NewArticle10Application = service.Update(mArticle10Id, _
                                                         CITESApplicationId, _
                                                         Article10TypeId, _
                                                         mBox18(0), _
                                                         mBox18(1), _
                                                         mBox18(2), _
                                                         mBox18(3), _
                                                         mBox18(4), _
                                                         mBox18(5), _
                                                         mBox18(6), _
                                                         mBox18(7), _
                                                         mArticle10CheckSum, _
                                                         tran)
            End If


            ''check to see if any SQL errors have occured
            If (NewArticle10Application Is Nothing AndAlso Not DataObjects.Sprocs.LastError Is Nothing) Then
                'TODO: Use errors collection to check to see if the problem was concurrency
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveArticle10Application)
                Return Me
            ElseIf Created And Not NewArticle10Application Is Nothing Then
                mArticle10Id = NewArticle10Application.Id
            End If

            Try
                If NewArticle10Application.CheckSum <> mArticle10CheckSum Then
                    InitialiseArticle10(NewArticle10Application, tran)
                End If
            Catch ex As Exception
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveArticle10Application)
            End Try
            Return Me
        End Function
#End Region

#Region " Validate "
        Protected Overloads Overrides Function Validate(ByVal writeFlag As Boolean, ByVal ignoreWarnings As Boolean, ByVal saveApplication As Boolean) As BaseBO
            MyBase.Validate(writeFlag, ignoreWarnings, saveApplication)
            Dim ReturnObj As BaseBO = Me
            ReturnObj.ValidationErrors = New ValidationManager(ValidationManager.ValidationTitles.CannotSaveArticle10Application, ignoreWarnings)

            Dim FoundBirthdateError As Boolean = False
            Dim FoundSex As Boolean = False
            Dim FoundMark As Boolean = False
            Dim FoundAcquiredDate As Boolean = False

            Dim Config As New BOConfiguration

            ' check mandatory
            CheckMandatory(ReturnObj)

            If Not Me.Permit Is Nothing AndAlso _
               Permit.Length > 0 Then
                For Each _permit As Application.CITES.Applications.BOCITESImportExportPermit In Permit
                    ' check mandatory
                    CheckMandatory(ReturnObj, _Permit)

                    If Not FoundBirthdateError AndAlso _
                       Not FoundSex AndAlso _
                       Not FoundMark AndAlso _
                       Not FoundAcquiredDate AndAlso _
                       Not _Permit.Specimens Is Nothing AndAlso _
                       _Permit.Specimens.Length > 0 Then
                        For Each _Specimen As Application.BOSpecimen In _Permit.Specimens
                            If Not FoundAcquiredDate AndAlso _
                               _specimen.AcquisitionDate = Nothing Then     'MLD 31/1/5
                                ReturnObj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.AcquisitionDateCannotBeBlank))
                                FoundAcquiredDate = True
                            End If
                            If Not FoundMark AndAlso _
                               _specimen.SpecimenMarks Is Nothing Then
                                ReturnObj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.MarkIdAndTypeCannotBeBlank))
                                FoundMark = True
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
                                    If Not Me.IsSemiComplete AndAlso _Specimen.Gender = GenderType.Unknown Then
                                        ReturnObj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.IfLiveGenderCannotBeBlank))
                                        FoundSex = True
                                    End If
                                End If
                            End If
                            If FoundBirthdateError AndAlso FoundSex AndAlso _
                               FoundMark AndAlso FoundAcquiredDate Then Exit For
                        Next _Specimen
                    End If

                    'more to check, so check 'em
                    'If Not _Permit.Specie Is Nothing Then
                    '    If Not FoundAnnexAB AndAlso _
                    '       (_Permit.Specie.IsAnnexA OrElse _
                    '       _Permit.Specie.IsAnnexB) AndAlso _
                    '       (Config.IsInImportSpecieSourceCountry(_Permit.Source1.Code) OrElse _
                    '       Config.IsInImportSpecieSourceCountry(_Permit.Source2.Code)) AndAlso _
                    '       _Permit.CountryOfOrigin.ID = 1 Then
                    '        ReturnObj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.SpecimensCannotBeAnnexD))
                    '        FoundAnnexAB = True
                    '    End If
                    '    If Not FoundAppendixA AndAlso _
                    '       _Permit.Specie.IsAnnexA AndAlso _
                    '       (Config.IsInExportAppendixISpecimenSource(_Permit.Source1.Code) OrElse _
                    '       Config.IsInExportAppendixISpecimenSource(_Permit.Source2.Code)) AndAlso _
                    '       Config.IsInExportAppendixISpecimenPurpose(_Permit.Purpose.Code) Then
                    '        ReturnObj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.SpecimensCannotBeAnnexA))
                    '        FoundAppendixA = True
                    '    End If
                    '    If Not FoundAnnexABCD AndAlso _
                    '       _Permit.Specie.IsAnnexC OrElse _
                    '       _Permit.Specie.IsAnnexD Then
                    '        ReturnObj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.NotRequiredForSpecimenOnAnnexeCOrD))
                    '        FoundAnnexABCD = True
                    '    End If

                    '    If Not FoundAuthorisedLocation AndAlso _
                    '       CheckAuthorisedLocation(_Permit) Then
                    '        If LocationAddress Is Nothing OrElse _
                    '           LocationAddress.AddressId = 0 Then
                    '            ReturnObj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.AuthorisedLocationCannotBeBlank))
                    '            FoundAuthorisedLocation = True
                    '        End If
                    '    End If

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
                    'End If
                Next _Permit
            Else
                'no permits!
                ReturnObj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.AnApplicationMustHaveAtLeastOnePermit))
            End If

            If Holder Is Nothing Then
                ReturnObj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.HolderRequired))
            Else
                Dim MainParty As BO.Party.BOParty
                If Not Agent Is Nothing Then
                    MainParty = Agent.Party
                Else
                    MainParty = Party.Party
                End If
                'check semi-completes flag
                If Not MainParty.AllowSemicompleteCitesArticle10 AndAlso _
                   IsSemiComplete Then
                    ReturnObj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.PartyIsNotAllowedToProcessSemiCompletes))
                ElseIf MainParty.AllowSemicompleteCitesArticle10 AndAlso _
                   IsSemiComplete Then
                    'allowed to semi complete the data
                Else
                    'not allowed to semi-complete or is a non-semi complete app, 
                    'so check that the non semi complete requirements have been met.
                    If Not Me.Permit Is Nothing AndAlso _
                       Permit.Length > 0 Then
                        Dim FoundError As Boolean = False
                        For Each _Permit As Application.CITES.BOCITESPermit In Permit
                            _Permit.LoadSpecimens(Nothing)
                            If Not _Permit.Specimens Is Nothing AndAlso _
                               _Permit.Specimens.Length > 0 Then
                                For Each _Specimen As Application.BOSpecimen In _Permit.Specimens
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
            Dim BaseApplication As Application.CITES.Applications.BOCITESArticle10 = CType(MyBase.Clone, Application.CITES.Applications.BOCITESArticle10)
            With BaseApplication
                .Article10Id = 0
            End With
            Return BaseApplication
        End Function

        Friend Shared Function SharedGetReportCriteria(ByVal permitinfoIds() As Integer, ByVal newStatus As BOPermitInfo.PermitStatusTypes, ByVal isSemiComplete As Boolean) As ReportCriteria.ReportCriteria()
            Dim reportCriterias As New ArrayList
            Dim criterion As ReportCriteria.ReportCriteria
            Dim duplicate As Boolean = newStatus = BO.Application.BOPermitInfo.PermitStatusTypes.Duplicate

            For Each id As Int32 In permitinfoIds
                If isSemiComplete Then
                    criterion = New Article10SemiReportCriteria
                    CType(criterion, Article10SemiReportCriteria).PermitInfoId = id         'MLD 31/1/5 roughly reinstated to previous version as cast did not work
                    CType(criterion, Article10SemiReportCriteria).Duplicate = duplicate
                Else
                    criterion = New Article10ReportCriteria
                    CType(criterion, Article10ReportCriteria).PermitInfoId = id
                    CType(criterion, Article10ReportCriteria).Duplicate = duplicate
                End If
                SetReportDescription(criterion, newStatus, id)
                reportCriterias.Add(criterion)
            Next

            Return CType(reportCriterias.ToArray(GetType(ReportCriteria.ReportCriteria)), ReportCriteria.ReportCriteria())
        End Function

        Public Overloads Shared Function GetReportCriteria(ByVal permitinfoIds() As Integer, ByVal newStatus As BOPermitInfo.PermitStatusTypes, ByVal isSemiComplete As Boolean) As uk.gov.defra.Phoenix.BO.ReportCriteria.ReportCriteria()
            Return SharedGetReportCriteria(permitinfoIds, newStatus, isSemiComplete)
        End Function

        'Refactored MLD 26/10/4
        Protected Overridable Overloads Sub CheckMandatory(ByVal owner As BaseBO, ByVal permit As Application.CITES.Applications.BOCITESImportExportPermit)

            AddErrorOnCondition(owner, ScientificNameCannotBeBlank, Not Me.IsSemiComplete AndAlso (permit.Specie Is Nothing OrElse _
               permit.Specie.ScientificName Is Nothing OrElse _
               permit.Specie.ScientificName.Length = 0))
            AddErrorOnCondition(owner, SourceCodeCannotBeBlank, permit.Source1 Is Nothing OrElse permit.Source2 Is Nothing)
            AddErrorOnCondition(owner, PartDerivativeCannotBeBlank, permit.Derivative Is Nothing)

            'MLD 26/10/4 these conditions commented out, as not necessarily true
            'AddErrorOnCondition(owner, QuantityMustBeGreaterThanZero, permit.Quantity = 0)
            'AddErrorOnCondition(owner, PreviousArticle10CertificateNumberCannotBeBlank, permit.CountryOfOriginPermitNumber Is Nothing OrElse _
            '   CType(permit.CofLExportNumber, Int32) = 0)
            AddErrorOnCondition(owner, PreviousArticle10CertificateIssueDateCannotBeBlank, permit.CountryOfOriginPermitDate Is Nothing)
            AddErrorOnCondition(owner, NumberOfCopiesMustBeOneUnlessCommercialUse, Not mArticle10Type Is Nothing AndAlso CType(mArticle10Type.ID, Int32) <> 1 AndAlso _
                (Not permit.NumberOfCopies Is Nothing AndAlso CType(permit.NumberOfCopies, Int32) <> 1))
        End Sub

        Public Shared Function GetAcquisitionDetails() As String()
            Dim Details(7) As String

            Details(0) = "were taken from the wild with the legislation in force in the issuing Member State"
            Details(1) = "are abandoned or escaped specimens that were recovered in accordance with the legislation in force in the issuing Member State"
            Details(2) = "are captive born-and-bred or artificially propagated specimens"
            Details(3) = "were acquired in or introduced into the Community in compliance with the provisions of Council Regulation (EC) No 338/97"
            Details(4) = "were acquired in or introduced into the Community before 1 June 1997 in accordance with Council Regulation (EEC) No 3626/82"
            Details(5) = "were acquired in or introduced into the Community before 1 January 1984 in compliance with the provisions of CITES"
            Details(6) = "were acquired in or introduced into the issuing Member State before the provisions of the Regulations under 3 and 4 or of CITES became applicable in the territory"
            Details(7) = "are to be used for the advancement of science/breeding or propagation/research or education or other non-detrimental purposes"
            Return Details
        End Function
#End Region

        '- Member State of Import (if C of O not in EC and specimen not Pre convention or source Unknown(U))
        '- Quantity (Number of Specimens) - mandatory (this will default to 1 if the parts and derivative codes LIV, SKI, BOD and SKE are used). 
        '- Unit of Measure (if Mass/Volume to be specified - applies to all Specimens)
        '- Net Volume/Mass (Units Based on pre-defined units linked to the parts and derivatives code selected) 


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
        '                    Dim BOImportExportPerm As New BOCITESArticle10Permit
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



        Public Overrides Function GetNewPermit() As BOCITESPermit
            Dim Permit As New BO.Application.CITES.Applications.BOCITESArticle10Permit
            Return Permit
        End Function


    End Class
End Namespace

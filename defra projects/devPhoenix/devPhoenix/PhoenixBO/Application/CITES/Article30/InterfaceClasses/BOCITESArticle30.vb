Namespace Application.CITES.Applications
    Public Class BOCITESArticle30
        Inherits BOCITESApplication
        Implements IBOCITESArticle30



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
                mArticle30Id = .Id
                mArticle30CheckSum = .CheckSum
                Dim Config As New BO.BOConfiguration
                Dim DefaultArticle30CertificateTypeId As Object = Config.GetValue("DefaultArticle30CertificateTypeId")
                If Config.IsInt32(DefaultArticle30CertificateTypeId) AndAlso Config.IsDefaultArticle30CertificateTypeId(CType(DefaultArticle30CertificateTypeId, Int32)) Then
                    mArticle30Type = New BO.ReferenceData.BOArticle10CertificateType(CType(DefaultArticle30CertificateTypeId, Int32))
                End If

                Me.Box18_8 = True
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
                Return Application.ApplicationTypes.Article30
            End Get
            Set(ByVal Value As Int32)
            End Set
        End Property

        Public Property Holder() As BO.Application.BOApplicationPartyDetails Implements IBOCITESArticle30.Holder
            Get
                Return MyBase.Party
            End Get
            Set(ByVal Value As BO.Application.BOApplicationPartyDetails)
                MyBase.Party = Value
            End Set
        End Property

        Public Property Article30Id() As Int32 Implements IBOCITESArticle30.Article30Id
            Get
                Return mArticle30Id
            End Get
            Set(ByVal Value As Int32)
                mArticle30Id = Value
            End Set
        End Property
        Private mArticle30Id As Int32

        Public Property Article30Type() As BO.ReferenceData.BOArticle10CertificateType Implements IBOCITESArticle30.Article30Type
            Get
                Return mArticle30Type
            End Get
            Set(ByVal Value As BO.ReferenceData.BOArticle10CertificateType)
                mArticle30Type = Value
            End Set
        End Property
        Private mArticle30Type As BO.ReferenceData.BOArticle10CertificateType


        Public Property Article30CheckSum() As Int32 Implements IBOCITESArticle30.Article30CheckSum
            Get
                Return mArticle30CheckSum
            End Get
            Set(ByVal Value As Int32)
                mArticle30CheckSum = Value
            End Set
        End Property
        Private mArticle30CheckSum As Int32

        Public Property IsTransactionSpecific() As Object Implements IBOCITESArticle30.IsTransactionSpecific
            Get
                Return mIsTransactionSpecific
            End Get
            Set(ByVal Value As Object)
                mIsTransactionSpecific = Value
            End Set
        End Property
        Private mIsTransactionSpecific As Object

        Private mBox18(7) As Boolean
        Public Property Box18_1() As Boolean Implements IBox18.Box18_1
            Get
                Return mBox18(0)
            End Get
            Set(ByVal Value As Boolean)
                mBox18(0) = Value
            End Set
        End Property

        Public Property Box18_2() As Boolean Implements IBox18.Box18_2
            Get
                Return mBox18(1)
            End Get
            Set(ByVal Value As Boolean)
                mBox18(1) = Value
            End Set
        End Property

        Public Property Box18_3() As Boolean Implements IBox18.Box18_3
            Get
                Return mBox18(2)
            End Get
            Set(ByVal Value As Boolean)
                mBox18(2) = Value
            End Set
        End Property

        Public Property Box18_4() As Boolean Implements IBox18.Box18_4
            Get
                Return mBox18(3)
            End Get
            Set(ByVal Value As Boolean)
                mBox18(3) = Value
            End Set
        End Property

        Public Property Box18_5() As Boolean Implements IBox18.Box18_5
            Get
                Return mBox18(4)
            End Get
            Set(ByVal Value As Boolean)
                mBox18(4) = Value
            End Set
        End Property

        Public Property Box18_6() As Boolean Implements IBox18.Box18_6
            Get
                Return mBox18(5)
            End Get
            Set(ByVal Value As Boolean)
                mBox18(5) = Value
            End Set
        End Property

        Public Property Box18_7() As Boolean Implements IBox18.Box18_7
            Get
                Return mBox18(6)
            End Get
            Set(ByVal Value As Boolean)
                mBox18(6) = Value
            End Set
        End Property

        Public Property Box18_8() As Boolean Implements IBox18.Box18_8
            Get
                Return mBox18(7)
            End Get
            Set(ByVal Value As Boolean)
                mBox18(7) = Value
            End Set
        End Property
#End Region

#Region " Helper Functions "

        Public Overrides Function GetNewPermit() As BOCITESPermit
            Dim Permit As New BO.Application.CITES.Applications.BOCITESArticle30Permit
            Permit.Specie = New BOSpecie
            Permit.Specie.ECAnnex = "A"
            Permit.Specie.ScientificName = "Article 30 Species Allowed"
            Return Permit
        End Function

        Private ReadOnly Property Article30TypeId() As Object
            Get
                If mArticle30Type Is Nothing Then
                    Return Nothing
                Else
                    Return mArticle30Type.ID
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
                Dim BOCITESArticle10 As New Application.CITES.Applications.BOCITESArticle10
                Return BOCITESArticle10.CheckAuthorisedLocation(permit)
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



            Dim CITESApplication As Object = MyBase.Save(tran, ignoreValidation)
            Created = (mArticle30Id = 0)
            If Not CITESApplication Is Nothing AndAlso _
               Not CType(CITESApplication, BaseBO).ValidationErrors Is Nothing Then
                'rollback the transaction
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                'get the problems and assign them locally
                ValidationErrors = CType(CITESApplication, BaseBO).ValidationErrors
                'bail
                Return Me
            End If
            mIsTransactionSpecific = True

            If Created Then
                NewArticle10Application = service.Insert(CITESApplicationId, _
                                                         Article30TypeId, _
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
                NewArticle10Application = service.Update(mArticle30Id, _
                                                         CITESApplicationId, _
                                                         Article30TypeId, _
                                                        mBox18(0), _
                                                         mBox18(1), _
                                                         mBox18(2), _
                                                         mBox18(3), _
                                                         mBox18(4), _
                                                         mBox18(5), _
                                                         mBox18(6), _
                                                         mBox18(7), _
                                                         mArticle30CheckSum, _
                                                         tran)
            End If


            ''check to see if any SQL errors have occured
            If (NewArticle10Application Is Nothing AndAlso Not DataObjects.Sprocs.LastError Is Nothing) Then
                'TODO: Use errors collection to check to see if the problem was concurrency
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveArticle10Application)
                Return Me
            ElseIf Created And Not NewArticle10Application Is Nothing Then
                mArticle30Id = NewArticle10Application.Id
            End If

            Try
                If NewArticle10Application.CheckSum <> mArticle30CheckSum Then
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

        End Function
#End Region

#Region " Operations "

        Public Overloads Shared Function GetReportCriteria(ByVal permitinfoIds() As Integer, ByVal newStatus As BOPermitInfo.PermitStatusTypes, ByVal isSemiComplete As Boolean) As ReportCriteria.ReportCriteria()
            Return BO.Application.CITES.Applications.BOCITESArticle10.SharedGetReportCriteria(permitinfoIds, newStatus, isSemiComplete)
        End Function

        Protected Overridable Overloads Sub CheckMandatory(ByVal validationOwner As BaseBO, ByVal permit As Application.CITES.Applications.BOCITESImportExportPermit)

            Dim ErrorType As ValidationError.ValidationCodes

            ErrorType = ValidationError.ValidationCodes.ScientificNameCannotBeBlank
            If Not CheckErrorExists(validationOwner, ErrorType) AndAlso _
               (permit.Specie Is Nothing OrElse _
               permit.Specie.ScientificName Is Nothing OrElse _
               permit.Specie.ScientificName.Length = 0) Then
                validationOwner.ValidationErrors.AddError(New ValidationError(ErrorType))
            End If

            ErrorType = ValidationError.ValidationCodes.SourceCodeCannotBeBlank
            If Not CheckErrorExists(validationOwner, ErrorType) AndAlso _
               (permit.Source1 Is Nothing OrElse _
               permit.Source2 Is Nothing) Then
                validationOwner.ValidationErrors.AddError(New ValidationError(ErrorType))
            End If

            ErrorType = ValidationError.ValidationCodes.PartDerivativeCannotBeBlank
            If Not CheckErrorExists(validationOwner, ErrorType) AndAlso _
               permit.Derivative Is Nothing Then
                validationOwner.ValidationErrors.AddError(New ValidationError(ErrorType))
            End If

            ErrorType = ValidationError.ValidationCodes.QuantityMustBeGreaterThanZero
            If Not CheckErrorExists(validationOwner, ErrorType) AndAlso _
               permit.Quantity = 0 Then
                validationOwner.ValidationErrors.AddError(New ValidationError(ErrorType))
            End If

            ErrorType = ValidationError.ValidationCodes.PreviousArticle10CertificateNumberCannotBeBlank
            If Not CheckErrorExists(validationOwner, ErrorType) AndAlso _
               (permit.CountryOfOriginPermitNumber Is Nothing OrElse _
               CType(permit.CofLExportNumber, Int32) = 0) Then
                validationOwner.ValidationErrors.AddError(New ValidationError(ErrorType))
            End If

            ErrorType = ValidationError.ValidationCodes.PreviousArticle10CertificateIssueDateCannotBeBlank
            If Not CheckErrorExists(validationOwner, ErrorType) AndAlso _
               permit.CountryOfOriginPermitDate Is Nothing Then
                validationOwner.ValidationErrors.AddError(New ValidationError(ErrorType))
            End If

            ErrorType = ValidationError.ValidationCodes.NumberOfCopiesMustBeOneUnlessCommercialUse
            If (Not mArticle30Type Is Nothing AndAlso CType(mArticle30Type.ID, Int32) <> 1 AndAlso _
                (Not permit.NumberOfCopies Is Nothing AndAlso CType(permit.NumberOfCopies, Int32) <> 1)) Then
                validationOwner.ValidationErrors.AddError(New ValidationError(ErrorType))
            End If

        End Sub
#End Region



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
        '                    Dim BOImportExportPerm As New BOCITESArticle30Permit
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


       
    End Class
End Namespace

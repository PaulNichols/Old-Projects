Imports System.Text
Imports uk.gov.defra.Phoenix.DO.DataObjects.Entity
Imports uk.gov.defra.Phoenix.DO.DataObjects.EntitySet

Namespace Application.CITES.Applications
    Public Class BOCITESImportExportPermit
        Inherits BOCITESPermit
        Implements IBOCITESImportExportPermit

#Region " Prelim code "
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal citesImportExportPermitId As Int32)
            MyClass.New(citesImportExportPermitId, Nothing)
        End Sub

        Public Sub New(ByVal citesImportExportPermitId As Int32, ByVal tran As SqlClient.SqlTransaction)
            MyClass.New()
            LoadCITESImportExportPermit(citesImportExportPermitId, tran)
        End Sub

        Friend Function LoadCITESImportExportPermit(ByVal id As Int32) As DataObjects.Entity.CITESImportExportPermit
            Return LoadCITESImportExportPermit(id, Nothing)
        End Function

        Friend Function LoadCITESImportExportPermit(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.CITESImportExportPermit
            Dim NewCITESImportExportPermit As DataObjects.Entity.CITESImportExportPermit = DataObjects.Entity.CITESImportExportPermit.GetById(id)
            If NewCITESImportExportPermit Is Nothing Then
                Throw New RecordDoesNotExist("CITESImportExportPermit", id)
            Else
                InitialiseCITESImportExportPermit(NewCITESImportExportPermit, tran)
                Return NewCITESImportExportPermit
            End If
        End Function

        Friend Overridable Sub InitialiseCITESImportExportPermit(ByVal citesImportExportPermit As DataObjects.Entity.CITESImportExportPermit, ByVal tran As SqlClient.SqlTransaction)
            'Try
            With citesImportExportPermit
                MyBase.InitialiseCITESPermit(New DataObjects.Entity.CITESPermit(.CITESPermitId, tran), tran)
                If Not .IsCofLExportIdNull Then Me.mCofLExport = New ReferenceData.BOCountry(.CofLExportId)
                If Not .IsCofLExportIssueDateNull() Then mCofLExportIssueDate = .CofLExportIssueDate
                If Not .IsCofLExportNumberNull() Then mCofLExportNumber = .CofLExportNumber
                If Not .IsCofLExportPermitExpiryDateNull() Then mCofLExportPermitExpiryDate = .CofLExportPermitExpiryDate
                If Not .IsEULicenseNumberNull() Then mEULicenseNumber = .EULicenseNumber

                mCITESImportExportPermitId = .Id
                mCITESImportExportPermitChecksum = .CheckSum
                Dim Advice As BO.Application.CITES.Applications.BOPermitScientificAdvice() = GetScientificAdvice(False)
                If Not Advice Is Nothing AndAlso Advice.Length > 0 Then mLatestSAAdvice = Advice(0)
            End With
            'Catch ex As Exception
            'End Try
        End Sub
#End Region

#Region " Properties "

        Public Property PreviousCertificateIssueDate() As Object Implements IBOCITESImportExportPermit.PreviousCertificateIssueDate
            Get
                Return mPreviousCertificateIssueDate
            End Get
            Set(ByVal Value As Object)
                mPreviousCertificateIssueDate = Value
            End Set
        End Property
        Protected mPreviousCertificateIssueDate As Object

        Public Property PreviousCertificateNumber() As Object Implements IBOCITESImportExportPermit.PreviousCertificateNumber
            Get
                Return mPreviousCertificateNumber
            End Get
            Set(ByVal Value As Object)
                mPreviousCertificateNumber = Value
            End Set
        End Property
        Protected mPreviousCertificateNumber As Object

        Public Property LatestSAAdvice() As BO.Application.CITES.Applications.BOPermitScientificAdvice Implements IBOCITESImportExportPermit.LatestSAAdvice
            Get
                Return mLatestSAAdvice
            End Get
            Set(ByVal Value As BOPermitScientificAdvice)

            End Set
        End Property
        Private mLatestSAAdvice As BO.Application.CITES.Applications.BOPermitScientificAdvice

        Protected mIsTransactionSpecific As Object

        Public Property CofLExport() As ReferenceData.BOCountry Implements IBOCITESImportExportPermit.CofLExport
            Get
                Return mCofLExport
            End Get
            Set(ByVal Value As ReferenceData.BOCountry)
                mCofLExport = Value
            End Set
        End Property
        Private mCofLExport As ReferenceData.BOCountry

        Public Property CITESImportExportPermitId() As Integer Implements IBOCITESImportExportPermit.CITESImportExportPermitId
            Get
                Return mCITESImportExportPermitId
            End Get
            Set(ByVal Value As Integer)
                mCITESImportExportPermitId = Value
            End Set
        End Property
        Private mCITESImportExportPermitId As Int32

        Public Property CofLExportIssueDate() As Object Implements IBOCITESImportExportPermit.CofLExportIssueDate
            Get
                Return mCofLExportIssueDate
            End Get
            Set(ByVal Value As Object)
                mCofLExportIssueDate = Value
            End Set
        End Property
        Private mCofLExportIssueDate As Object

        Public Property CofLExportNumber() As String Implements IBOCITESImportExportPermit.CofLExportNumber
            Get
                If mCofLExportNumber Is Nothing Or mCofLExportNumber = "0" Then
                    mCofLExportNumber = String.Empty
                End If
                Return mCofLExportNumber
            End Get
            Set(ByVal Value As String)
                mCofLExportNumber = Value
            End Set
        End Property
        Private mCofLExportNumber As String

        Public Property CofLExportPermitExpiryDate() As Object Implements IBOCITESImportExportPermit.CofLExportPermitExpiryDate
            Get
                Return mCofLExportPermitExpiryDate
            End Get
            Set(ByVal Value As Object)
                mCofLExportPermitExpiryDate = Value
            End Set
        End Property
        Private mCofLExportPermitExpiryDate As Object

        Public Property EULicenseNumber() As Object Implements IBOCITESImportExportPermit.EULicenseNumber
            Get
                Return mEULicenseNumber
            End Get
            Set(ByVal Value As Object)
                mEULicenseNumber = Value
            End Set
        End Property
        Private mEULicenseNumber As Object

        Public Property CITESImportExportPermitChecksum() As Integer Implements IBOCITESImportExportPermit.CITESImportExportPermitChecksum
            Get
                Return mCITESImportExportPermitChecksum
            End Get
            Set(ByVal Value As Integer)
                mCITESImportExportPermitChecksum = Value
            End Set
        End Property
        Private mCITESImportExportPermitChecksum As Int32
#End Region

#Region " Helper Functions "

        Private ReadOnly Property CofLExportId() As Object
            Get
                If Not mCofLExport Is Nothing AndAlso mCofLExport.ID > 0 Then
                    Return mCofLExport.ID
                Else
                    Return Nothing
                End If
            End Get
        End Property

        Private ReadOnly Property CofLExportNumberInteger() As Object
            Get

                Try
                    Return CType(CofLExportNumber, Int32)
                Catch ex As Exception
                    Return Nothing
                End Try

            End Get
        End Property

        Private ReadOnly Property EULicenseNumberInteger() As Object
            Get

                Try
                    Return CType(EULicenseNumber, Int32)
                Catch ex As Exception
                    Return Nothing
                End Try

            End Get
        End Property

        Private ReadOnly Property CountryOfOriginPermitNumberInteger() As Object
            Get

                Try
                    Return CType(CountryOfOriginPermitNumber, Int32)
                Catch ex As Exception
                    Return Nothing
                End Try

            End Get
        End Property

#End Region

#Region " Save "
        Public Overloads Overrides Function Save() As BaseBO
            Dim NewPermit As New DataObjects.Entity.CITESImportExportPermit
            Dim service As DataObjects.Service.CITESImportExportPermitService = NewPermit.ServiceObject
            Dim tran As SqlClient.SqlTransaction = service.BeginTransaction

            Dim SaveResult As BaseBO = MyClass.Save(tran)
            If SaveResult Is Nothing Then
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
            Else
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Commit)
            End If
            Return SaveResult
        End Function

        Public Overloads Overrides Function Save(ByVal tran As SqlClient.SqlTransaction) As BaseBO
            Dim NewCITESImportExportPermit As New DataObjects.Entity.CITESImportExportPermit
            Dim service As DataObjects.Service.CITESImportExportPermitService = NewCITESImportExportPermit.ServiceObject

            Dim CITESPermit As BOCITESPermit = CType(MyBase.Save(tran), BOCITESPermit)
            If Not CITESPermit.ValidationErrors Is Nothing Then
                'rollback the transaction
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                'get the problems and assign them locally
                ValidationErrors = CITESPermit.ValidationErrors
                'bail
                Return Me
            End If

            Created = (mCITESImportExportPermitId = 0)

            If Created Then
                NewCITESImportExportPermit = service.Insert(CofLExportNumber, _
                                                            mCofLExportIssueDate, _
                                                            mCofLExportPermitExpiryDate, _
                                                            EULicenseNumberInteger, _
                                                            CITESPermitId, _
                                                            CofLExportId, _
                                                             PreviousCertificateNumber, _
                                                            mPreviousCertificateIssueDate, _
                                                            mIsTransactionSpecific, _
                                                            tran)
            Else
                NewCITESImportExportPermit = service.Update(mCITESImportExportPermitId, _
                                                            CofLExportNumberInteger, _
                                                            mCofLExportIssueDate, _
                                                            mCofLExportPermitExpiryDate, _
                                                            EULicenseNumberInteger, _
                                                            CITESPermitId, _
                                                            CofLExportId, _
                                                            PreviousCertificateNumber, _
                                                            mPreviousCertificateIssueDate, _
                                                             mIsTransactionSpecific, _
                                                            mCITESImportExportPermitChecksum, _
                                                            tran)
            End If
            'check to see if any SQL errors have occured
            If NewCITESImportExportPermit Is Nothing AndAlso Not DataObjects.Sprocs.LastError Is Nothing Then
                'TODO: Use errors collection to check to see if the problem was concurrency
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSavePermit)
            ElseIf NewCITESImportExportPermit Is Nothing Then
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSavePermit)
            ElseIf Created And Not NewCITESImportExportPermit Is Nothing Then
                mCITESImportExportPermitId = NewCITESImportExportPermit.Id
            End If

            If NewCITESImportExportPermit.CheckSum <> mCITESImportExportPermitChecksum Then
                'no point in initialising unless things have changed
                InitialiseCITESImportExportPermit(NewCITESImportExportPermit, tran)
            End If
            Return Me
        End Function
#End Region

#Region " Operations "
        Private Sub PopulateImportPermitFromImportPermit(ByRef returnPermit As BO.Application.CITES.Applications.BOCITESImportExportPermit)
            With returnPermit
                .UnderDerogation = False
                .CofLExportNumber = ""
                .CofLExportIssueDate = Nothing
                .CofLExportPermitExpiryDate = Nothing
            End With
        End Sub

        Private Sub PopulateExportPermitFromImportPermit(ByRef returnPermit As BO.Application.CITES.Applications.BOCITESImportExportPermit)
            With returnPermit
                .UnderDerogation = False
                .CofLExportNumber = ""
                .CofLExport = Nothing
                .CofLExportIssueDate = Nothing
                .CofLExportPermitExpiryDate = Nothing
            End With
        End Sub

        Private Sub PopulateArt10PermitFromImportPermit(ByRef returnPermit As BO.Application.CITES.Applications.BOCITESImportExportPermit)
            With CType(returnPermit, BO.Application.CITES.Applications.BOCITESArticle10Permit)
                .IsTransactionSpecific = Nothing
                .UnderDerogation = False
                .CofLExportNumber = ""
                .CofLExport = Nothing
                .CofLExportIssueDate = Nothing
                .CofLExportPermitExpiryDate = Nothing
                .Description = Nothing
                .Specie = Me.Specie
                .Specie.SpecieId = 0
                .Derivative = Me.Derivative
                .Source1 = Me.Source1
                .Source2 = Me.Source1
                .CountryOfOrigin = Me.CountryOfOrigin
                .CountryOfOriginPermitDate = Me.CountryOfOriginPermitDate
                .CountryOfOriginPermitNumber = Me.CountryOfOriginPermitNumber
                .Specimens = Me.Specimens
                If Not .Specimens Is Nothing AndAlso .Specimens.Length > 0 Then
                    .Specimens(0).UOM.UOMId = 0
                    .Specimens(0).SpecimenId = 0
                End If
                .PreviousCertificateIssueDate = Me.PreviousCertificateIssueDate
                .PreviousCertificateNumber = Me.PreviousCertificateNumber
            End With
        End Sub

        Private Sub PopulateArt10PermitFromExportPermit(ByRef returnPermit As BO.Application.CITES.Applications.BOCITESImportExportPermit)
            With CType(returnPermit, BO.Application.CITES.Applications.BOCITESArticle10Permit)
                .IsTransactionSpecific = Nothing
                .UnderDerogation = False
                .CofLExportNumber = ""
                .CofLExport = Nothing
                .CofLExportIssueDate = Nothing
                .CofLExportPermitExpiryDate = Nothing
                .Description = Nothing

                .Specie = Me.Specie
                .Specie.SpecieId = 0
                .Derivative = Me.Derivative
                .Source1 = Me.Source1
                .Source2 = Me.Source1
                .CountryOfOrigin = Me.CountryOfOrigin
                .CountryOfOriginPermitDate = Me.CountryOfOriginPermitDate
                .CountryOfOriginPermitNumber = Me.CountryOfOriginPermitNumber
                .Specimens = Me.Specimens
                If Not .Specimens Is Nothing AndAlso .Specimens.Length > 0 Then
                    .Specimens(0).UOM.UOMId = 0
                    .Specimens(0).SpecimenId = 0
                End If
                .PreviousCertificateIssueDate = Me.PreviousCertificateIssueDate
                .PreviousCertificateNumber = Me.PreviousCertificateNumber
            End With
        End Sub

        Private Sub PopulateExportPermitFromExportPermit(ByRef returnPermit As BO.Application.CITES.Applications.BOCITESImportExportPermit)
            With returnPermit
                .Derivative = Nothing
                .Purpose = Nothing
                .Source1 = Nothing
                .Source2 = Nothing
                '.PreviousCertificateNumber = Nothing
                '.pr()
                .UnderDerogation = False
                .CofLExportNumber = ""
                .CofLExport = Nothing
                .CofLExportIssueDate = Nothing
                .CofLExportPermitExpiryDate = Nothing
            End With
        End Sub

        Private Sub PopulateImportPermitFromExportPermit(ByRef returnPermit As BO.Application.CITES.Applications.BOCITESImportExportPermit)
            With returnPermit
                .UnderDerogation = False
            End With
        End Sub

        Public Overridable Function CreatePermitFromPermit(ByVal specimenId As Int32, ByVal typeToCreate As uk.gov.defra.Phoenix.BO.Application.ApplicationTypes, ByVal fromType As uk.gov.defra.Phoenix.BO.Application.ApplicationTypes) As BO.Application.CITES.Applications.BOCITESImportExportPermit
            Dim returnPermit As BO.Application.CITES.Applications.BOCITESImportExportPermit

            If specimenId = 0 Then

                Select Case typeToCreate
                    Case ApplicationTypes.Article10
                        returnPermit = New BO.Application.CITES.Applications.BOCITESArticle10Permit
                        Select Case fromType
                            Case ApplicationTypes.Export
                                PopulateArt10PermitFromExportPermit(returnPermit)
                            Case ApplicationTypes.Import
                                PopulateArt10PermitFromImportPermit(returnPermit)
                        End Select
                    Case ApplicationTypes.Export
                        returnPermit = CType(Me.Clone, BO.Application.CITES.Applications.BOCITESImportExportPermit)
                        Select Case fromType
                            Case ApplicationTypes.Export
                                PopulateExportPermitFromExportPermit(returnPermit)
                            Case ApplicationTypes.Import
                                PopulateExportPermitFromImportPermit(returnPermit)
                        End Select
                    Case ApplicationTypes.Import
                        returnPermit = CType(Me.Clone, BO.Application.CITES.Applications.BOCITESImportExportPermit)
                        Select Case fromType
                            Case ApplicationTypes.Export
                                PopulateImportPermitFromExportPermit(returnPermit)
                            Case ApplicationTypes.Import
                                PopulateImportPermitFromImportPermit(returnPermit)
                        End Select

                End Select

            Else
                returnPermit = New BO.Application.CITES.Applications.BOCITESImportExportPermit
                PopulateSpecimenDetails(specimenId, returnPermit)
            End If
            Return returnPermit

        End Function

        Protected Sub PopulateSpecimenDetails(ByVal specimenId As Int32, ByRef returnPermit As BO.Application.CITES.Applications.BOCITESImportExportPermit)
            returnPermit.Specie = New BO.Application.BOSpecie
            With returnPermit.Specie
                .ScientificName = Specie.ScientificName
                .CommonName = Me.Specie.CommonName
                .ECAnnex = Me.Specie.ECAnnex
                .CITESAppendix = Me.Specie.CITESAppendix
                .Lineage = Me.Specie.Lineage
                .AppliedForName = Me.Specie.AppliedForName
            End With

            For Each spec As BO.Application.BOSpecimen In Me.Specimens
                If spec.SpecimenId = specimenId Then
                    ReDim returnPermit.Specimens(0)
                    returnPermit.Specimens(0) = New BO.Application.BOSpecimen
                    With returnPermit.Specimens(0)
                        .Gender = spec.Gender
                        .DOB = spec.DOB
                        .ExactDOB = spec.ExactDOB
                        .AcquisitionDate = spec.AcquisitionDate
                        .Description = spec.Description
                        .SpecimenMarks = spec.SpecimenMarks
                    End With
                End If
            Next
        End Sub

        Public Function HasCondition(ByVal conditionId As Int32) As Boolean
            'Could be done more efficently with an index on the PermitSpecialCondition table, but it's quicker for me to do it this way
            Dim SCs As BO.Application.CITES.Applications.BOPermitSpecialCondition() = BO.Application.CITES.Applications.BOCITESImportExportPermit.GetSpecialConditions(PermitId, False)
            For Each sc As BO.Application.CITES.Applications.BOPermitSpecialCondition In SCs
                If sc.SpecialConditionId = conditionId Then Return True
            Next
            Return False
        End Function

        Public Shared Function RemoveAllSpecialConditions(ByVal gwdApplying As Boolean, ByVal permitId As Int32) As Boolean
            Dim SCs As BO.Application.CITES.Applications.BOPermitSpecialCondition() = BO.Application.CITES.Applications.BOCITESImportExportPermit.GetSpecialConditions(permitId, False)
            For Each sc As BO.Application.CITES.Applications.BOPermitSpecialCondition In SCs
                'Only remove if all if a GWD user has triggered this method
                'If not only delete the Condition if it was added by an SA and not GWD
                If gwdApplying OrElse (Not gwdApplying AndAlso sc.AddedBySA) Then
                    If Not sc.Delete Then Return False
                End If
            Next
        End Function

        Public Shared Function GetAllSpecialConditionsNotAssigned(ByVal permitId As Int32) As DataObjects.Collection.SpecialConditionBoundCollection
            Dim ConditionSet As [DO].DataObjects.EntitySet.SpecialConditionSet = [DO].DataObjects.Entity.Permit.GetSpecialConditionsNotAssigned(permitId)
            Return CType(ConditionSet.GetBoundCollection(ConditionSet, 0, GetType(DataObjects.Collection.SpecialConditionBoundCollection)), DataObjects.Collection.SpecialConditionBoundCollection)
        End Function

        Public Overrides Function DeletePermitById(ByVal permit As BOPermit, ByVal tran As SqlClient.SqlTransaction) As Boolean
            Dim PermitService As New DataObjects.Service.CITESImportExportPermitService
            If tran Is Nothing Then
                tran = PermitService.BeginTransaction
            End If
            If PermitService.DeleteById(CType(permit, BOCITESImportExportPermit).CITESImportExportPermitId, 0, tran) Then
                If MyBase.DeletePermitById(permit, tran) Then
                    PermitService.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Commit)
                    Return True
                End If
            Else
                PermitService.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
            End If
        End Function

        Public Shared Function GetPermitByNumber(ByVal permitNumber As String, ByVal tran As SqlClient.SqlTransaction) As BO.Application.CITES.Applications.BOCITESImportExportPermit
            If permitNumber.IndexOf("/"c) > 0 Then
                Dim ApplicationPart As Object = permitNumber.Split("/"c)(0)
                Dim PermitPart As Object = permitNumber.Split("/"c)(1)
                Try
                    Dim PermitService As New DataObjects.Service.PermitService
                    Dim PermitSet As DataObjects.EntitySet.PermitSet = PermitService.GetByIndex_IX_Permit(Int32.Parse(PermitPart.ToString), Int32.Parse(ApplicationPart.ToString), tran)
                    If Not PermitSet Is Nothing Then
                        Dim DOPermit As DataObjects.Entity.Permit = PermitSet.Entities(0)
                        Return CType(BO.Application.CITES.Applications.BOCITESImportExportPermit.PolymorphicCreate(DOPermit.Id, tran), BO.Application.CITES.Applications.BOCITESImportExportPermit)
                    End If
                Catch ex As FormatException
                    'numbers not formated correctly, ie not valid integers
                    Return Nothing
                End Try
            Else
                'invalid permit number
                Return Nothing
            End If
        End Function

        Protected Overrides Function GetPermit(ByVal permitId As Int32) As BOPermit
            Return LoadByPermitId(permitId)
        End Function

        Friend Shared Function GetImportApplicationDO(ByVal applicationId As Int32) As DataObjects.Entity.ImportApplication
            Return GetImportApplicationDO(applicationId, Nothing)
        End Function

        Friend Shared Function GetImportApplicationDO(ByVal applicationId As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.ImportApplication
            If applicationId = 0 Then
                Throw New ArgumentException("Application Id is 0")
            Else
                Dim CITESApps As DataObjects.EntitySet.CITESApplicationSet = DataObjects.Entity.CITESApplication.GetForApplication(applicationId)
                If Not CITESApps Is Nothing AndAlso _
                   CITESApps.Count = 1 Then
                    Dim ImportApps As DataObjects.EntitySet.ImportApplicationSet = DataObjects.Entity.ImportApplication.GetForCITESApplication(CType(CITESApps.GetEntity(0), DataObjects.Entity.CITESApplication).Id)
                    If Not ImportApps Is Nothing AndAlso _
                       ImportApps.Count = 1 Then
                        Return CType(ImportApps.GetEntity(0), DataObjects.Entity.ImportApplication)
                    End If
                End If
            End If
            Return Nothing
        End Function

        Friend Shared Function GetExportApplicationDO(ByVal applicationId As Int32) As DataObjects.Entity.ExportApplication
            Return GetExportApplicationDO(applicationId, Nothing)
        End Function

        Friend Shared Function GetExportApplicationDO(ByVal applicationId As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.ExportApplication
            If applicationId = 0 Then
                Throw New ArgumentException("Application Id is 0")
            Else
                Dim CITESApps As DataObjects.EntitySet.CITESApplicationSet = DataObjects.Entity.CITESApplication.GetForApplication(applicationId)
                If Not CITESApps Is Nothing AndAlso _
                   CITESApps.Count = 1 Then
                    Dim ImportApps As DataObjects.EntitySet.ExportApplicationSet = DataObjects.Entity.ExportApplication.GetForCITESApplication(CType(CITESApps.GetEntity(0), DataObjects.Entity.CITESApplication).Id)
                    If Not ImportApps Is Nothing AndAlso _
                       ImportApps.Count = 1 Then
                        Return CType(ImportApps.GetEntity(0), DataObjects.Entity.ExportApplication)
                    End If
                End If
            End If
            Return Nothing
        End Function

        Private Function GetImportExportApplication() As BOImportExportApplication
            Dim application As BOImportExportApplication = GetImportApplication()
            If application Is Nothing Then
                application = GetExportApplication()
            End If
            Return application
        End Function

        Friend Shadows Function GetImportApplication() As Application.CITES.Applications.BOImportApplication
            Return GetImportApplication(Nothing)
        End Function

        Friend Shadows Function GetImportApplication(ByVal tran As SqlClient.SqlTransaction) As Application.CITES.Applications.BOImportApplication
            Dim AppDO As DataObjects.Entity.ImportApplication = GetImportApplicationDO(ApplicationId, tran)
            If Not AppDO Is Nothing Then
                Dim App As New Application.CITES.Applications.BOImportApplication
                App.InitialiseImportApplication(AppDO, tran)
                Return App
            End If
        End Function

        Friend Shadows Function GetExportApplication() As Application.CITES.Applications.BOExportApplication
            Return GetExportApplication(Nothing)
        End Function

        Friend Shadows Function GetExportApplication(ByVal tran As SqlClient.SqlTransaction) As Application.CITES.Applications.BOExportApplication
            Dim AppDO As DataObjects.Entity.ExportApplication = GetExportApplicationDO(ApplicationId, tran)
            If Not AppDO Is Nothing Then
                Dim App As New Application.CITES.Applications.BOExportApplication
                App.InitialiseExportApplication(AppDO, tran)
                Return App
            End If
        End Function


        Friend Shadows Function GetArticle10Application() As Application.CITES.Applications.BOCITESArticle10
            Return GetArticle10Application(Nothing)
        End Function

        Friend Shadows Function GetArticle10Application(ByVal tran As SqlClient.SqlTransaction) As Application.CITES.Applications.BOCITESArticle10
            Dim AppDO As DataObjects.Entity.Article10 = GetArticle10ApplicationDO(ApplicationId, tran)
            If Not AppDO Is Nothing Then
                Dim App As New Application.CITES.Applications.BOCITESArticle10
                App.InitialiseArticle10(AppDO, tran)
                Return App
            End If
        End Function

        Friend Shared Function GetArticle10ApplicationDO(ByVal applicationId As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.Article10
            If applicationId = 0 Then
                Throw New ArgumentException("Application Id is 0")
            Else
                Dim CITESApps As DataObjects.EntitySet.CITESApplicationSet = DataObjects.Entity.CITESApplication.GetForApplication(applicationId)
                If Not CITESApps Is Nothing AndAlso _
                   CITESApps.Count = 1 Then
                    Dim Article10Apps As DataObjects.EntitySet.Article10Set = DataObjects.Entity.Article10.GetForCITESApplication(CType(CITESApps.GetEntity(0), DataObjects.Entity.CITESApplication).Id)
                    If Not Article10Apps Is Nothing AndAlso _
                       Article10Apps.Count = 1 Then
                        Return CType(Article10Apps.GetEntity(0), DataObjects.Entity.Article10)
                    End If
                End If
            End If
            Return Nothing
        End Function

        Private Shared Function LoadByPermitId(ByVal permitId As Int32) As BOCITESImportExportPermit
            Dim CITESPermits As DataObjects.EntitySet.CITESPermitSet = DataObjects.Entity.CITESPermit.GetForPermit(permitId)
            If Not CITESPermits Is Nothing AndAlso _
               CITESPermits.Count = 1 Then
                Dim ImportPermits As DataObjects.EntitySet.CITESImportExportPermitSet = DataObjects.Entity.CITESImportExportPermit.GetForCITESPermit(CType(CITESPermits.GetEntity(0), DataObjects.Entity.CITESPermit).Id)
                If Not ImportPermits Is Nothing AndAlso _
                   ImportPermits.Count = 1 Then
                    Dim NewImportPermit As New BOCITESImportExportPermit
                    NewImportPermit.InitialiseCITESImportExportPermit(CType(ImportPermits.GetEntity(0), DataObjects.Entity.CITESImportExportPermit), Nothing)
                    Return NewImportPermit
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
        End Function

        Protected Shared Function GetDuplicateRequest(ByVal permitInfoId As Int32) As PermitDuplicateRequest
            Dim duplicates As PermitDuplicateRequestSet = PermitDuplicateRequest.GetForPermitInfo(permitInfoId)
            If Not duplicates Is Nothing AndAlso duplicates.Count > 0 Then
                Return CType(duplicates.GetEntity(0), PermitDuplicateRequest)
            End If
            Return Nothing
        End Function

        Protected Function GetSpecimenDesc() As String
            Dim desc As String = ""
            If Not Specimens Is Nothing AndAlso Specimens.Length > 0 Then
                desc = GetSpecimenDesc(Specimens(0))
            End If
            Return desc
        End Function

        Public Function GetAllSpecimenDescs() As String     'MLD 1/2/5 now Public
            Dim desc As New StringBuilder
            If Not Specimens Is Nothing Then
                For Each specimen As BOSpecimen In Specimens
                    desc.Append(GetSpecimenDesc(specimen))
                Next
            End If
            Return desc.ToString()
        End Function

        Private Function GetSpecimenDesc(ByVal specimen As BOSpecimen) As String
            Dim builder As New StringBuilder
            Dim config As New BOConfiguration
            If config.IsLiveDerivative(Derivative) Then 'live
                builder.Append(specimen.ReportDescription)
            Else 'not live  
                If Not Derivative Is Nothing Then
                    builder.Append(Derivative.Code)
                    builder.Append(" ")
                End If
                builder.Append(specimen.ReportDescription)
            End If
            Return builder.ToString()
        End Function

        Protected Function GetOriginCountry() As String
            If CountryOfOrigin Is Nothing Then Return ""
            Return CountryOfOrigin.LongName
        End Function

        Protected Function GetExportCountry() As String
            If CofLExport Is Nothing Then Return ""
            Return CofLExport.LongName
        End Function

        Public Shared Function GetSpecialConditions(ByVal permitId As Int32, ByVal hideFromGWD As Boolean) As BO.Application.CITES.Applications.BOPermitSpecialCondition()
            Dim ReturnSpecialConditions(-1) As BO.Application.CITES.Applications.BOPermitSpecialCondition   'MLD 2/3/5 (-1) added
            Dim DOSpecialConditions As [DO].DataObjects.Entity.PermitSpecialCondition() = Permit.GetSpecialConditions(permitId, hideFromGWD)
            If Not DOSpecialConditions Is Nothing Then                  'MLD 2/3/5 modified
                ReDim ReturnSpecialConditions(DOSpecialConditions.Length - 1)
                Dim Index As Int32
                For Each DOSpecialCondition As [DO].DataObjects.Entity.PermitSpecialCondition In DOSpecialConditions
                    ReturnSpecialConditions(Index) = New BO.Application.CITES.Applications.BOPermitSpecialCondition(DOSpecialCondition.Id)
                    Index += 1
                Next
            End If

            Return ReturnSpecialConditions
        End Function

        Public Function GetSpecialConditions(ByVal hideFromGWD As Boolean) As BO.Application.CITES.Applications.BOPermitSpecialCondition()
            Return Me.GetSpecialConditions(Me.PermitId, hideFromGWD)
        End Function

        Public Function GetReportSpecialConditions(ByVal specialConditions As BO.Application.CITES.Applications.BOPermitSpecialCondition()) As String
            Dim returnSpecialConditions As String = ""
            Dim nl As String = ""

            If Not specialConditions Is Nothing Then
                For Each specialCondition As BO.Application.CITES.Applications.BOPermitSpecialCondition In specialConditions
                    returnSpecialConditions = returnSpecialConditions & nl & specialCondition.SpecialCondition.Description()
                    nl = Environment.NewLine()

                Next
            End If

            Return returnSpecialConditions
        End Function

        Public Function GetScientificAdvice(ByVal hideFromGWD As Boolean) As BO.Application.CITES.Applications.BOPermitScientificAdvice()
            Return Me.GetScientificAdvice(Me.PermitId, hideFromGWD)
        End Function

        Public Shared Function GetScientificAdvice(ByVal permitId As Int32, ByVal hideFromGWD As Boolean) As BO.Application.CITES.Applications.BOPermitScientificAdvice()
            Dim ReturnScientificAdvice(-1) As BO.Application.CITES.Applications.BOPermitScientificAdvice    'MLD 2/3/5 (-1) added
            Dim DOScientificAdvices As [DO].DataObjects.Entity.PermitScientificAdvice() = Permit.GetScientificAdvice(permitId, hideFromGWD)
            If Not DOScientificAdvices Is Nothing Then      'MLD 2/3/5 redundant length check removed
                ReDim ReturnScientificAdvice(DOScientificAdvices.Length - 1)
                Dim Index As Int32
                For Each DOScientificAdvice As [DO].DataObjects.Entity.PermitScientificAdvice In DOScientificAdvices
                    ReturnScientificAdvice(Index) = New BO.Application.CITES.Applications.BOPermitScientificAdvice(DOScientificAdvice.Id)
                    Index += 1
                Next
            End If

            Return ReturnScientificAdvice
        End Function

        Public Function GetSpecialConditionsString() As String   'MLD note that this will change when the link table contains the condition text
            'Dim builder As New StringBuilder
            'Dim links As PermitSpecialConditionLinkSet = Permit.GetSpecialConditions
            'If Not links Is Nothing Then
            '    For Each link As PermitSpecialConditionLink In links
            '        Dim condition As SpecialCondition = SpecialCondition.GetById(link.SpecialConditionId)
            '        If Not condition Is Nothing Then
            '            builder.Append(condition.Description + " ")
            '        End If
            '    Next link
            'End If
            'Return builder.ToString().Trim()
        End Function

        Protected Shared Sub FillBasicDetails(ByVal application As BOCITESApplication, ByVal permit As BOCITESImportExportPermit, ByVal info As BOPermitInfo, ByRef newRow As DataRow, ByVal addImportation As Boolean, ByVal addIssueDate As Boolean, ByVal addIssueLetter As Boolean, ByVal duplicate As Boolean)
            Dim importX As String = Resolve(application.IsImport)
            Dim exportX As String = Resolve(application.IsExport)
            Dim reexpoX As String = Resolve(application.IsReExport)
            Dim issueDate As DateTime = Date.Today
            Dim issueLetter As String = info.GetNextIssue()
            Dim printStatus As String = ""

            If duplicate Then
                printStatus = "DUPLICATE"
            End If
            With newRow
                .Item("Barcode") = permit.GetBarCode(issueLetter, issueDate)
                .Item("ExpiryDate") = permit.ExpiryDate_String
                .Item("SheetDescription") = "Original"
                .Item("SheetNumber") = "1"
                .Item("ExhibitionX") = ""
                .Item("ImportX") = importX
                .Item("ExportX") = exportX
                .Item("ReExportX") = reexpoX
                .Item("PetX") = ""
                If addImportation Then
                    .Item("ExportationX") = exportX
                    .Item("ImportationX") = importX
                    .Item("ReExportationX") = reexpoX
                End If
                If addIssueDate Then
                    .Item("IssueDate") = issueDate.ToString("dd/MM/yyyy")
                End If
                If addIssueLetter Then
                    .Item("IssueLetter") = issueLetter
                    .Item("PrintStatus") = printStatus
                End If
            End With
        End Sub

        Protected Shared Sub FillExtraDetails(ByVal application As BOImportExportApplication, ByVal permit As BOCITESImportExportPermit, ByVal info As BOPermitInfo, ByRef newRow As DataRow)
            With newRow
                .Item("ImportCountry") = Resolve(application.CountryOfImport, "ShortName")
                .Item("ReExportCountry") = Resolve(application.CountryOfExport, "ShortName")
                .Item("IssuePlace") = Resolve(info.PlaceOfIssue)
                .Item("SpecialConditions") = permit.GetSpecialConditionsString()
                .Item("SurrenderBorderX") = "X"
                .Item("SurrenderIssuingX") = ""
                .Item("UserName") = Resolve(info.IssuedBy, "FullName")
            End With
        End Sub

        Protected Shared Sub FillAgentDetails(ByVal application As BOImportExportApplication, ByRef newRow As DataRow)
            FillSubDetails("Agent", application.Agent, newRow)
        End Sub

        Private Shared Sub FillSubDetails(ByVal prefix As String, ByVal details As BOApplicationPartyDetails, ByRef newRow As DataRow)
            With newRow
                If details Is Nothing Then
                    .Item(prefix + "_AuthorisedPartyId") = ""
                    .Item(prefix + "_PartyName") = ""
                    .Item(prefix + "_PartyAddress") = ""
                Else
                    .Item(prefix + "_AuthorisedPartyId") = details.Party.PartyId
                    .Item(prefix + "_PartyName") = details.Party.DisplayName
                    .Item(prefix + "_PartyAddress") = CompressLines(details.Address.ReportAddress, 5)
                End If
            End With
        End Sub

        Protected Shared Function GetIssuingAuthority(ByVal application As BOCITESApplication) As String
            If Not application.ManagementAuthority Is Nothing Then
                'Return application.ManagementAuthority.Party.DisplayName + Environment.NewLine + application.ManagementAuthority.Address.ReportAddress 
                Return CompressLines(application.ManagementAuthority.Party.DisplayName + Environment.NewLine + application.ManagementAuthority.Address.ReportAddress, 5)
            End If
            Return ""
        End Function

        Protected Shared Function GetForeignAuthority(ByVal application As BOCITESApplication) As String
            If Not application.ForeignManagementAuthority Is Nothing Then
                Return application.ForeignManagementAuthority.Party.DisplayName + Environment.NewLine + application.ForeignManagementAuthority.Address.ReportAddress
            End If
            Return ""
        End Function

        Protected Shared Sub FillPartyDetails(ByVal application As BOImportExportApplication, ByRef newRow As DataRow, ByVal foreign As Boolean)
            With newRow
                .Item("Location_Address") = Resolve(application.LocationAddress, "DisplayAddress")
                .Item("IssuingManagementAuthority_NameAddress") = GetIssuingAuthority(application)
                If foreign Then
                    .Item("ForeignManagementAuthority_NameAddress") = CompressLines(GetForeignAuthority(application), 3)
                End If
                FillSubDetails("Exporter", application.Exporter, newRow)
                FillSubDetails("Importer", application.Importer, newRow)
            End With
        End Sub

        'MLD 26/1/5 no longer needed
        'Public Shared Function GetConsignmentPermitReportData(ByVal applicationId As Int32, ByVal duplicate As Boolean, ByVal draft As Boolean, ByVal schema As String) As ReportDataResults
        '    Dim returnDS As DataSet = GetSchemaDataSet(schema)
        '    Dim application As BOCITESApplication = CType(BOApplication.PolymorphicCreate(applicationId), BOCITESApplication)
        '    Dim permits() As BOPermit = application.Permit
        '    Dim firstPermit As BOCITESImportExportPermit = CType(permits(0), BOCITESImportExportPermit)
        '    Dim firstInfo As BOPermitInfo = firstPermit.GetPermitInfos(Nothing)(0)
        '    Dim maxPermits As Int32 = permits.Length
        '    Dim pageNN As Int32 = CType(((maxPermits - 1) / 3), Int32) + 2 ' Calculate Page MM of NN
        '    Dim reference As String = firstPermit.ReportISOCode + firstPermit.ReportApplicationRef
        '    Dim index As Int32 = -1

        '    For pageMM As Int32 = 2 To pageNN
        '        Dim newRow As DataRow = returnDS.Tables("BOSpecimens").NewRow()
        '        FillBasicDetails(application, firstPermit, firstInfo, newRow, False, False, True, duplicate) 'flags, barcode, fixed fields, etc
        '        With newRow
        '            .Item("PermitReference") = reference
        '            .Item("PageMofN") = "Page " & pageMM.ToString & " of " & pageNN.ToString ' Leave this
        '            ' Create Permit Details for this page
        '            index += 1
        '            If index < maxPermits Then
        '                FillPermitData(newRow, permits(index), reference, "Specimen1", index + 1)
        '            End If
        '            index += 1
        '            If index < maxPermits Then
        '                FillPermitData(newRow, permits(index), reference, "Specimen2", index + 1)
        '            End If
        '            index += 1
        '            If index < maxPermits Then
        '                FillPermitData(newRow, permits(index), reference, "Specimen3", index + 1)
        '            End If
        '        End With
        '        CloneDataSets(returnDS, newRow, "BOSpecimens", draft)
        '    Next pageMM

        '    Return New ReportDataResults(returnDS, "")
        'End Function

        Private Shared Sub FillPermitData(ByRef newRow As DataRow, ByVal permit As BOPermit, ByVal reference As String, ByVal prefix As String, ByVal suffix As Int32)
            FillPermitData(newRow, CType(permit, BOCITESImportExportPermit), reference, prefix, suffix)
        End Sub

        Private Shared Sub FillPermitData(ByRef newRow As DataRow, ByVal permit As BOCITESImportExportPermit, ByVal reference As String, ByVal prefix As String, ByVal suffix As Int32)
            newRow.Item(prefix + "_Reference") = reference + "/" & suffix.ToString.PadLeft(2, CType("0", Char)) ' Leave this
            FillPermitData(newRow, permit, prefix)
        End Sub

        Private Shared Sub FillPermitData(ByRef newRow As DataRow, ByVal permit As BOCITESImportExportPermit, ByVal prefix As String)
            Dim species As BOSpecie = permit.Specie
            Dim exportDate As String = ""
            If Not permit.CofLExportIssueDate Is Nothing Then
                exportDate = CType(permit.CofLExportIssueDate, Date).ToString().Substring(0, 10)
            End If
            With newRow
                .Item(prefix + "_Descriptions") = permit.GetAllSpecimenDescs()
                .Item(prefix + "_Appendix") = species.CITESAppendix
                .Item(prefix + "_CommonName") = species.CommonName
                .Item(prefix + "_LastReExportCountry") = permit.GetExportCountry
                .Item(prefix + "_Mass") = permit.NetMassAbbrevString
                .Item(prefix + "_OriginCertificateDate") = exportDate
                .Item(prefix + "_OriginCertificateRef") = permit.CofLExportNumber
                .Item(prefix + "_OriginCountry") = permit.GetOriginCountry()
                .Item(prefix + "_OriginPermitDate") = permit.CountryOfOriginPermitDate_String
                .Item(prefix + "_OriginPermitRef") = permit.CountryOfOriginPermitNo_String
                .Item(prefix + "_Purpose") = permit.Purpose.Code
                .Item(prefix + "_Quantity") = ""
                If permit.Quantity > 0 Then .Item(prefix + "_Quantity") = permit.Quantity.ToString()
                Dim sourceCode1, sourceCode2 As String
                If Not permit.Source1 Is Nothing Then
                    If Not permit.Source1.Code Is Nothing Then sourceCode1 = permit.Source1.Code
                End If
                If Not permit.Source2 Is Nothing Then
                    If Not permit.Source2.Code Is Nothing Then sourceCode2 = permit.Source2.Code
                End If
                .Item(prefix + "_ScientificName") = species.ScientificName
                .Item(prefix + "_Source") = (sourceCode1 & " " & sourceCode2).Trim  'species.Source()
                Try                                                     'cope with inconsistent naming
                    .Item(prefix + "_ECAnnex") = species.ECAnnex
                Catch
                    .Item(prefix + "_EUAnnex") = species.ECAnnex
                End Try
            End With
        End Sub

        'MLD 25/1/5 renamed from GetCitesApplicationReportData
        Public Shared Function GetPermitApplicationReportTestData(ByVal permitInfoId As Int32, ByVal schema As String) As ReportDataResults

            'get the DS ready
            Dim ReturnDS As New DataSet
            'create a stream to put the info into
            Dim io As New io.StringReader(schema)
            'set-up the DS schema
            ReturnDS.ReadXmlSchema(io)
            'tidy ...
            io.Close()
            io = Nothing

            Dim copyDataset As DataSet
            Dim copyDataRow As DataRow

            'create a new row using the ds schema
            Dim newRow As DataRow

            ' Create a new BOPermit row - Populate with test data
            newRow = ReturnDS.Tables("BOPermit").NewRow()

            Dim searchReference As String = "GB12345/01/001" ' Get Permit Reference for this PerimtInfoId

            With newRow
                ' Create new Permit Application Details
                .Item("ApplicationId") = 1

                .Item("PermitReference") = searchReference
                .Item("BarCode") = "00GB1234501000ADDMMYYYY"

                .Item("ExhibitionX") = ""
                .Item("ExportX") = ""
                .Item("ImportX") = "X"
                .Item("ImportCountry") = "Import Country"
                .Item("PetX") = ""
                .Item("ReExportX") = ""
                .Item("ReExportCountry") = "ReExport Country"
                .Item("SheetDescription") = "APPLICATION"
                .Item("SheetNumber") = "5" ' Hard Coded
                .Item("PageMofN") = "Page 1 of 3" ' Hard Coded

                ' Create new IssuingManagementAuthority Details
                .Item("IssuingManagementAuthority_NameAddress") = "Wildlife Licensing and Registration Service" & Environment.NewLine & _
                "Floor 1, Zone 17, Temple Quary House" & Environment.NewLine & _
                "2 The Square, Temple Quary" & Environment.NewLine & _
                "Bristol BS1 6EB  Tel 0044 (0)117 372 8749" & Environment.NewLine & _
                "Department for Enviroment, Food and Rural Affairs"

                ' Create new Specimen Details
                .Item("Specimen_Descriptions") = "One live female orangutan" & Environment.NewLine & "microchip number 0005FC1EFS"
                .Item("Specimen_Appendix") = "I"
                .Item("Specimen_CommonName") = "Orang-utan"
                .Item("Specimen_EUAnnex") = "A"
                .Item("Specimen_LastReExportCountry") = "TAIWAN"
                .Item("Specimen_Mass") = "160"
                .Item("Specimen_OriginCertificateDate") = "01/01/2001"
                .Item("Specimen_OriginCertificateRef") = "ZB92S 006687"
                .Item("Specimen_OriginCountry") = "INDONESIA"
                .Item("Specimen_OriginPermitDate") = "02/12/2003"
                .Item("Specimen_OriginPermitRef") = "ABC92S 006655"
                .Item("Specimen_Purpose") = "Z"
                .Item("Specimen_Quantity") = "1"
                .Item("Specimen_ScientificName") = "Pongo pymaeus"
                .Item("Specimen_Source") = "C"

                ' Create new Exporter Details
                .Item("Exporter_AuthorisedPartyId") = "1"
                .Item("Exporter_PartyName") = "PINGTANG RESCUE CENTRE FOR ENDANGERED WILD ANIMALS"
                .Item("Exporter_PartyAddress") = "UNIV. OF SCIENCE and TECHNOLOGY" & Environment.NewLine & _
                "1 HSUEH-HE ROAD, CHIUNG" & Environment.NewLine & _
                "PINGTANG 94982" & Environment.NewLine & _
                "TAIWAN"

                ' Create new Importer Details
                .Item("Importer_AuthorisedPartyId") = "2"
                .Item("Importer_PartyName") = "MONKEY CENTRE"
                .Item("Importer_PartyAddress") = "STREET" & Environment.NewLine & _
                "TOWN" & Environment.NewLine & _
                "COUNTY L22 GBB"

                ' Create new Location Details
                .Item("Location_Address") = ""

                .Item("AdditionalDetails") = "Additional Details"
            End With

            'add the row to the dataset - Sheet 1
            ReturnDS.Tables("BOPermit").Rows.Add(newRow)
            ReturnDS.AcceptChanges()





            ' Create a new BOMoreSpecimens row - Populate with test data
            newRow = ReturnDS.Tables("BOMoreSpecimens").NewRow()

            With newRow

                ' Create new Permit Application Details
                .Item("ApplicationId") = 1
                .Item("PageMofN") = "Page 2 of 3" ' Hard Coded

                .Item("Specimen_Descriptions1") = "One live female orangutan" & Environment.NewLine & "microchip number 0005FC1EFS"
                .Item("Specimen_Appendix1") = "I"
                .Item("Specimen_CommonName1") = "Orang-utan"
                .Item("Specimen_EUAnnex1") = "A"
                .Item("Specimen_LastReExportCountry1") = "TAIWAN"
                .Item("Specimen_Mass1") = "160"
                .Item("Specimen_OriginCertificateDate1") = "01/01/2001"
                .Item("Specimen_OriginCertificateRef1") = "ZB92S 006687"
                .Item("Specimen_OriginCountry1") = "INDONESIA"
                .Item("Specimen_OriginPermitDate1") = "02/12/2003"
                .Item("Specimen_OriginPermitRef1") = "ABC92S 006655"
                .Item("Specimen_Purpose1") = "Z"
                .Item("Specimen_Quantity1") = "1"
                .Item("Specimen_ScientificName1") = "Pongo pymaeus"
                .Item("Specimen_Source1") = "C"

                .Item("Specimen_Descriptions2") = "One live female orangutan" & Environment.NewLine & "microchip number 0005FC1EFS"
                .Item("Specimen_Appendix2") = "I"
                .Item("Specimen_CommonName2") = "Orang-utan"
                .Item("Specimen_EUAnnex2") = "A"
                .Item("Specimen_LastReExportCountry2") = "TAIWAN"
                .Item("Specimen_Mass2") = "160"
                .Item("Specimen_OriginCertificateDate2") = "01/01/2001"
                .Item("Specimen_OriginCertificateRef2") = "ZB92S 006687"
                .Item("Specimen_OriginCountry2") = "INDONESIA"
                .Item("Specimen_OriginPermitDate2") = "02/12/2003"
                .Item("Specimen_OriginPermitRef2") = "ABC92S 006655"
                .Item("Specimen_Purpose2") = "Z"
                .Item("Specimen_Quantity2") = "1"
                .Item("Specimen_ScientificName2") = "Pongo pymaeus"
                .Item("Specimen_Source2") = "C"

                .Item("Specimen_Descriptions3") = "One live female orangutan" & Environment.NewLine & "microchip number 0005FC1EFS"
                .Item("Specimen_Appendix3") = "I"
                .Item("Specimen_CommonName3") = "Orang-utan"
                .Item("Specimen_EUAnnex3") = "A"
                .Item("Specimen_LastReExportCountry3") = "TAIWAN"
                .Item("Specimen_Mass3") = "160"
                .Item("Specimen_OriginCertificateDate3") = "01/01/2001"
                .Item("Specimen_OriginCertificateRef3") = "ZB92S 006687"
                .Item("Specimen_OriginCountry3") = "INDONESIA"
                .Item("Specimen_OriginPermitDate3") = "02/12/2003"
                .Item("Specimen_OriginPermitRef3") = "ABC92S 006655"
                .Item("Specimen_Purpose3") = "Z"
                .Item("Specimen_Quantity3") = "1"
                .Item("Specimen_ScientificName3") = "Pongo pymaeus"
                .Item("Specimen_Source3") = "C"

            End With

            'add the row to the dataset - Sheet 1
            ReturnDS.Tables("BOMoreSpecimens").Rows.Add(newRow)
            ReturnDS.AcceptChanges()




            ' Create a new BOMoreSpecimens row - Populate with test data
            newRow = ReturnDS.Tables("BOMoreSpecimens").NewRow()

            With newRow

                ' Create new Permit Application Details
                .Item("ApplicationId") = 1
                .Item("PageMofN") = "Page 3 of 3" ' Hard Coded

                .Item("Specimen_Descriptions1") = "One live female orangutan" & Environment.NewLine & "microchip number 0005FC1EFS"
                .Item("Specimen_Appendix1") = "I"
                .Item("Specimen_CommonName1") = "Orang-utan"
                .Item("Specimen_EUAnnex1") = "A"
                .Item("Specimen_LastReExportCountry1") = "TAIWAN"
                .Item("Specimen_Mass1") = "160"
                .Item("Specimen_OriginCertificateDate1") = "01/01/2001"
                .Item("Specimen_OriginCertificateRef1") = "ZB92S 006687"
                .Item("Specimen_OriginCountry1") = "INDONESIA"
                .Item("Specimen_OriginPermitDate1") = "02/12/2003"
                .Item("Specimen_OriginPermitRef1") = "ABC92S 006655"
                .Item("Specimen_Purpose1") = "Z"
                .Item("Specimen_Quantity1") = "1"
                .Item("Specimen_ScientificName1") = "Pongo pymaeus"
                .Item("Specimen_Source1") = "C"

                .Item("Specimen_Descriptions2") = "One live female orangutan" & Environment.NewLine & "microchip number 0005FC1EFS"
                .Item("Specimen_Appendix2") = "I"
                .Item("Specimen_CommonName2") = "Orang-utan"
                .Item("Specimen_EUAnnex2") = "A"
                .Item("Specimen_LastReExportCountry2") = "TAIWAN"
                .Item("Specimen_Mass2") = "160"
                .Item("Specimen_OriginCertificateDate2") = "01/01/2001"
                .Item("Specimen_OriginCertificateRef2") = "ZB92S 006687"
                .Item("Specimen_OriginCountry2") = "INDONESIA"
                .Item("Specimen_OriginPermitDate2") = "02/12/2003"
                .Item("Specimen_OriginPermitRef2") = "ABC92S 006655"
                .Item("Specimen_Purpose2") = "Z"
                .Item("Specimen_Quantity2") = "1"
                .Item("Specimen_ScientificName2") = "Pongo pymaeus"
                .Item("Specimen_Source2") = "C"

            End With

            'add the row to the dataset - Sheet 2
            ReturnDS.Tables("BOMoreSpecimens").Rows.Add(newRow)
            ReturnDS.AcceptChanges()


            Return New ReportDataResults(ReturnDS, searchReference)

        End Function


        Public Shared Function GetPermitApplicationReportData(ByVal applicationId As Int32, ByVal schema As String) As ReportDataResults
            Dim returnDS As DataSet = GetSchemaDataSet(schema)
            Dim newRow As DataRow = returnDS.Tables("BOPermit").NewRow()
            Dim application As BOImportExportApplication = CType(BOApplication.PolymorphicCreate(applicationId), BOImportExportApplication)
            Dim permits() As BOPermit = application.Permit
            Dim firstPermit As BOCITESImportExportPermit = CType(permits(0), BOCITESImportExportPermit)
            Dim firstInfo As BOPermitInfo = firstPermit.GetPermitInfos(Nothing)(0)
            Dim maxPermits As Int32 = permits.Length
            Dim pageNN As Int32 = CType(((maxPermits + 1) / 3), Int32) + 1 ' Calculate Page MM of NN
            Dim reference As String = firstPermit.ReportISOCode + firstPermit.ReportApplicationRef
            Dim issueDate As DateTime = DateTime.Now
            Dim issueLetter As String = firstInfo.GetNextIssue()
            Dim index As Int32 = 0

            FillPartyDetails(application, newRow, False)                                              'authorities, importer, exporter, etc
            FillApplicationPermitData(newRow, firstPermit, "")                                        'permit details
            With newRow
                .Item("ApplicationId") = 1
                .Item("PermitReference") = reference
                .Item("BarCode") = firstPermit.GetBarCode(issueLetter, issueDate)
                .Item("ExhibitionX") = ""
                .Item("ExportX") = Resolve(application.IsExport)
                .Item("ImportX") = Resolve(application.IsImport)
                .Item("ReExportX") = Resolve(application.IsReExport)
                .Item("PetX") = ""
                .Item("ImportCountry") = Resolve(application.CountryOfImport, "LongName")
                .Item("ReExportCountry") = Resolve(application.CountryOfExport, "LongName")
                .Item("SheetDescription") = "APPLICATION"
                .Item("SheetNumber") = "5" ' Hard Coded
                .Item("PageMofN") = "Page 1 of " & pageNN.ToString
                .Item("AdditionalDetails") = Resolve(application.AdditionalDeclaration, "OtherInformation")
            End With
            'add the row to the dataset - Sheet 1
            returnDS.Tables("BOPermit").Rows.Add(newRow)
            returnDS.AcceptChanges()

            For pageMM As Int32 = 2 To pageNN
                newRow = returnDS.Tables("BOMoreSpecimens").NewRow()
                With newRow
                    .Item("ApplicationId") = 1
                    .Item("PageMofN") = "Page " & pageMM.ToString & " of " & pageNN.ToString ' Leave this
                    ' Create Permit Details for this page
                    index += 1
                    If index < maxPermits Then
                        FillApplicationPermitData(newRow, permits(index), "1")
                    End If
                    index += 1
                    If index < maxPermits Then
                        FillApplicationPermitData(newRow, permits(index), "2")
                    End If
                    index += 1
                    If index < maxPermits Then
                        FillApplicationPermitData(newRow, permits(index), "3")
                    End If
                End With
                returnDS.Tables("BOMoreSpecimens").Rows.Add(newRow)
                returnDS.AcceptChanges()
            Next pageMM

            Return New ReportDataResults(returnDS, reference)
        End Function


        Private Shared Sub FillApplicationPermitData(ByRef newRow As DataRow, ByVal basePermit As BOPermit, ByVal suffix As String)
            Dim permit As BOCITESImportExportPermit = CType(basePermit, BOCITESImportExportPermit)
            Dim species As BOSpecie = permit.Specie
            Dim exportDate As String = ""
            If Not permit.CofLExportIssueDate Is Nothing Then
                exportDate = CType(permit.CofLExportIssueDate, Date).ToString().Substring(0, 10)
            End If
            With newRow
                .Item("Specimen_Descriptions" + suffix) = permit.GetAllSpecimenDescs()
                .Item("Specimen_Appendix" + suffix) = species.CITESAppendix
                .Item("Specimen_CommonName" + suffix) = species.CommonName
                .Item("Specimen_EUAnnex" + suffix) = ""
                .Item("Specimen_LastReExportCountry" + suffix) = permit.GetExportCountry
                .Item("Specimen_Mass" + suffix) = permit.NetMassString
                .Item("Specimen_OriginCertificateDate" + suffix) = exportDate
                .Item("Specimen_OriginCertificateRef" + suffix) = permit.CofLExportNumber
                .Item("Specimen_OriginCountry" + suffix) = permit.GetOriginCountry()
                .Item("Specimen_OriginPermitDate" + suffix) = permit.CountryOfOriginPermitDate_String
                .Item("Specimen_OriginPermitRef" + suffix) = permit.CountryOfOriginPermitNo_String
                .Item("Specimen_Purpose" + suffix) = permit.Purpose.Code
                .Item("Specimen_Quantity" + suffix) = permit.Quantity.ToString()
                .Item("Specimen_ScientificName" + suffix) = species.ScientificName
                .Item("Specimen_Source" + suffix) = species.Source
            End With
        End Sub

        Public Shared Function GetConsignmentReportTestData(ByVal applicationId As Int32, ByVal duplicate As Boolean, ByVal draft As Boolean, ByVal schema As String) As ReportDataResults
            'get the DS ready
            Dim ReturnDS As New DataSet
            'create a stream to put the info into
            Dim io As New io.StringReader(schema)
            'set-up the DS schema
            ReturnDS.ReadXmlSchema(io)
            'tidy ...
            io.Close()
            io = Nothing

            Dim copyDataset As DataSet
            Dim copyDataRow As DataRow

            'create a new row using the ds schema
            Dim newRow As DataRow

            ' Get number of specimens for this permitInfoId - Hard coded for test purposes only !!!! - Change this line !!!
            Dim maxSpecimens As Int32 = 5

            ' Calculate Page MM of NN
            Dim pageNN As Int32 = CType(((maxSpecimens - 1) / 3), Int32) + 2

            Dim specimenIdx As Int32 = 0
            Dim rowIdx As Int32 = -1

            ' Create a new row - Populate with test data
            newRow = ReturnDS.Tables("BOConsignment").NewRow()

            With newRow
                ' Create new Permit Details
                .Item("LinkId") = 4
                .Item("PermitReference") = "GB123456/01"
                .Item("Barcode") = "00GB12345601000A02122003"
                .Item("ExhibitionX") = ""
                .Item("ExpiryDate") = "01/06/2004"
                .Item("ExportX") = ""
                .Item("ImportX") = "X"
                .Item("ImportCountry") = "Import Country"
                .Item("PetX") = ""
                .Item("ReExportX") = ""
                .Item("ReExportCountry") = "ReExport Country"
                .Item("ExportationX") = ""
                .Item("ImportationX") = "X"
                .Item("IssueDate") = "02 December 2003"
                .Item("IssueLetter") = "A"
                .Item("IssuePlace") = "Bristol"
                .Item("ReExportationX") = ""
                .Item("SpecialConditions") = "Special Conditions"
                .Item("SurrenderBorderX") = "X"
                .Item("SurrenderIssuingX") = ""
                .Item("UserName") = "Julia Hassell"
                .Item("SheetDescription") = "ORIGINAL"
                .Item("SheetNumber") = "1"
                .Item("PrintStatus") = "DUPLICATE"
                .Item("PageMofN") = "Page 1 of " & pageNN.ToString

                ' Create new IssuingManagementAuthority Details
                .Item("IssuingManagementAuthority_NameAddress") = "Wildlife Licensing and Registration Service" & Environment.NewLine & _
                "Floor 1, Zone 17, Temple Quary House" & Environment.NewLine & _
                "2 The Square, Temple Quary" & Environment.NewLine & _
                "Bristol BS1 6EB  Tel 0044 (0)117 372 8749" & Environment.NewLine & _
                "Department for Enviroment, Food and Rural Affairs"

                ' Create new Exporter Details
                .Item("Exporter_AuthorisedPartyId") = "1"
                .Item("Exporter_PartyName") = "PINGTANG RESCUE CENTRE FOR ENDANGERED WILD ANIMALS"
                .Item("Exporter_PartyAddress") = "UNIV. OF SCIENCE and TECHNOLOGY" & Environment.NewLine & _
                "1 HSUEH-HE ROAD, CHIUNG" & Environment.NewLine & _
                "PINGTANG 94982" & Environment.NewLine & _
                "TAIWAN"

                ' Create new Importer Details
                .Item("Importer_AuthorisedPartyId") = "2"
                .Item("Importer_PartyName") = "MONKEY CENTRE"
                .Item("Importer_PartyAddress") = "STREET" & Environment.NewLine & _
                "TOWN" & Environment.NewLine & _
                "COUNTY L22 GBB"

                ' Create new Location Details
                .Item("Location_Address") = ""

                ' Create new ForeignManagementAuthority Details
                .Item("ForeignManagementAuthority_NameAddress") = "BOARD OF FOREIGN TRADE" & Environment.NewLine & _
                "MINISTRY OF ECONOMICS AFFAIRS" & Environment.NewLine & _
                "NO.1 HU KOU STREET" & Environment.NewLine & _
                "TAIPEI"

            End With

            'add the row to the dataset - Sheet 1
            rowIdx += 1
            ReturnDS.Tables("BOConsignment").Rows.Add(newRow)
            ReturnDS.AcceptChanges()

            'add the row to the dataset - Sheet 2
            copyDataset = ReturnDS.Clone
            copyDataset.Tables("BOConsignment").ImportRow(newRow)
            copyDataRow = copyDataset.Tables("BOConsignment").Rows(0)
            copyDataRow.Item("LinkId") = 3
            copyDataRow.Item("SheetNumber") = "2"
            copyDataRow.Item("SheetDescription") = "COPY for the holder"
            ReturnDS.Merge(copyDataset)
            ReturnDS.AcceptChanges()

            'add the row to the dataset - Sheet 3
            copyDataset = ReturnDS.Clone()
            copyDataset.Tables("BOConsignment").ImportRow(newRow)
            copyDataRow = copyDataset.Tables("BOConsignment").Rows(0)
            copyDataRow.Item("LinkId") = 2
            copyDataRow.Item("SheetNumber") = "3"
            copyDataRow.Item("SheetDescription") = "COPY for return by customs to issuing"
            ReturnDS.Merge(copyDataset)
            ReturnDS.AcceptChanges()

            'add the row to the dataset - Sheet 4
            copyDataset = ReturnDS.Clone()
            copyDataset.Tables("BOConsignment").ImportRow(newRow)
            copyDataRow = copyDataset.Tables("BOConsignment").Rows(0)
            copyDataRow.Item("LinkId") = 1
            copyDataRow.Item("SheetNumber") = "4"
            copyDataRow.Item("SheetDescription") = "COPY for the issuing authority"
            ReturnDS.Merge(copyDataset)
            ReturnDS.AcceptChanges()

            ReturnDS = GetConsignmentSpecimenReportTestData(ReturnDS)

            Return New ReportDataResults(ReturnDS, "")

        End Function

        Private Shared Function GetConsignmentSpecimenReportTestData(ByVal returnDs As DataSet) As DataSet

            Dim copyDataset As DataSet
            Dim copyDataRow As DataRow

            'create a new row using the ds schema
            Dim newRow As DataRow

            ' Get number of specimens for this permitInfoId - Hard coded for test purposes only !!!! - Change this line !!!
            Dim maxSpecimens As Int32 = 5

            ' Calculate Page MM of NN
            Dim pageNN As Int32 = CType(((maxSpecimens - 1) / 3), Int32) + 2

            Dim specimenIdx As Int32 = 0
            Dim rowIdx As Int32 = -1

            For pageMM As Int32 = 2 To pageNN

                newRow = returnDs.Tables("BOSpecimens").NewRow()

                With newRow
                    ' Create new Page Permit Details
                    .Item("LinkId") = 1
                    .Item("PermitReference") = "GB123456"
                    .Item("Barcode") = "00GB12345601000A02122003"
                    .Item("ExhibitionX") = ""
                    .Item("ExpiryDate") = "01/06/2004"
                    .Item("ExportX") = ""
                    .Item("ImportX") = "X"
                    .Item("PetX") = ""
                    .Item("ReExportX") = ""
                    .Item("IssueLetter") = "A"
                    .Item("SheetDescription") = "ORIGINAL"
                    .Item("SheetNumber") = "1"
                    .Item("PrintStatus") = "DUPLICATE"
                    .Item("PageMofN") = "Page " & pageMM.ToString & " of " & pageNN.ToString ' Leave this

                    ' Create Specimen Details for this page
                    specimenIdx += 1
                    If specimenIdx <= maxSpecimens Then
                        .Item("Specimen1_Descriptions") = "One live female orangutan" & Environment.NewLine & "microchip number 0005FC1EFS"
                        .Item("Specimen1_Appendix") = "I"
                        .Item("Specimen1_CommonName") = "Orang-utan"
                        .Item("Specimen1_EUAnnex") = "A"
                        .Item("Specimen1_LastReExportCountry") = "TAIWAN"
                        .Item("Specimen1_Mass") = "160"
                        .Item("Specimen1_OriginCertificateDate") = "01/01/2001"
                        .Item("Specimen1_OriginCertificateRef") = "ZB92S 006687"
                        .Item("Specimen1_OriginCountry") = "INDONESIA"
                        .Item("Specimen1_OriginPermitDate") = "02/12/2003"
                        .Item("Specimen1_OriginPermitRef") = "ABC92S 006655"
                        .Item("Specimen1_Purpose") = "Z"
                        .Item("Specimen1_Quantity") = "1"
                        .Item("Specimen1_ScientificName") = "Pongo pymaeus"
                        .Item("Specimen1_Source") = "C"
                        .Item("Specimen1_Reference") = "GB123456/" & (specimenIdx).ToString.PadLeft(2, CType("0", Char)) ' Leave this
                    End If

                    ' Create Specimen Details for this page
                    specimenIdx += 1
                    If specimenIdx <= maxSpecimens Then
                        .Item("Specimen2_Descriptions") = "One live female orangutan" & Environment.NewLine & "microchip number 0005FC1EFS"
                        .Item("Specimen2_Appendix") = "I"
                        .Item("Specimen2_CommonName") = "Orang-utan"
                        .Item("Specimen2_EUAnnex") = "A"
                        .Item("Specimen2_LastReExportCountry") = "TAIWAN"
                        .Item("Specimen2_Mass") = "160"
                        .Item("Specimen2_OriginCertificateDate") = "01/01/2001"
                        .Item("Specimen2_OriginCertificateRef") = "ZB92S 006687"
                        .Item("Specimen2_OriginCountry") = "INDONESIA"
                        .Item("Specimen2_OriginPermitDate") = "02/12/2003"
                        .Item("Specimen2_OriginPermitRef") = "ABC92S 006655"
                        .Item("Specimen2_Purpose") = "Z"
                        .Item("Specimen2_Quantity") = "1"
                        .Item("Specimen2_ScientificName") = "Pongo pymaeus"
                        .Item("Specimen2_Source") = "C"
                        .Item("Specimen2_Reference") = "GB123456/" & (specimenIdx).ToString.PadLeft(2, CType("0", Char)) ' Leave this
                    End If

                    ' Create Specimen Details for this page
                    specimenIdx += 1
                    If specimenIdx <= maxSpecimens Then
                        .Item("Specimen3_Descriptions") = "One live female orangutan" & Environment.NewLine & "microchip number 0005FC1EFS"
                        .Item("Specimen3_Appendix") = "I"
                        .Item("Specimen3_CommonName") = "Orang-utan"
                        .Item("Specimen3_EUAnnex") = "A"
                        .Item("Specimen3_LastReExportCountry") = "TAIWAN"
                        .Item("Specimen3_Mass") = "160"
                        .Item("Specimen3_OriginCertificateDate") = "01/01/2001"
                        .Item("Specimen3_OriginCertificateRef") = "ZB92S 006687"
                        .Item("Specimen3_OriginCountry") = "INDONESIA"
                        .Item("Specimen3_OriginPermitDate") = "02/12/2003"
                        .Item("Specimen3_OriginPermitRef") = "ABC92S 006655"
                        .Item("Specimen3_Purpose") = "Z"
                        .Item("Specimen3_Quantity") = "1"
                        .Item("Specimen3_ScientificName") = "Pongo pymaeus"
                        .Item("Specimen3_Source") = "C"
                        .Item("Specimen3_Reference") = "GB123456/" & (specimenIdx).ToString.PadLeft(2, CType("0", Char)) ' Leave this
                    End If

                End With

                'add the row to the dataset - Sheet 1
                rowIdx += 1
                returnDs.Tables("BOSpecimens").Rows.Add(newRow)
                returnDs.AcceptChanges()

                'add the row to the dataset - Sheet 2
                returnDs.Tables("BOSpecimens").ImportRow(newRow)
                newRow = returnDs.Tables("BOSpecimens").Rows(returnDs.Tables("BOSpecimens").Rows.Count - 1)
                newRow.Item("SheetNumber") = "2"
                newRow.Item("SheetDescription") = "COPY for the holder"
                returnDs.AcceptChanges()


                'add the row to the dataset - Sheet 3
                returnDs.Tables("BOSpecimens").ImportRow(newRow)
                newRow = returnDs.Tables("BOSpecimens").Rows(returnDs.Tables("BOSpecimens").Rows.Count - 1)
                newRow.Item("SheetNumber") = "3"
                newRow.Item("SheetDescription") = "COPY for return by customs to issuing"
                returnDs.AcceptChanges()


                'add the row to the dataset - Sheet 4
                returnDs.Tables("BOSpecimens").ImportRow(newRow)
                newRow = returnDs.Tables("BOSpecimens").Rows(returnDs.Tables("BOSpecimens").Rows.Count - 1)
                newRow.Item("SheetNumber") = "4"
                newRow.Item("SheetDescription") = "COPY for the issuing authority"
                returnDs.AcceptChanges()

            Next pageMM

            Return returnDs

        End Function


        Public Shared Function GetConsignmentReportData(ByVal applicationId As Int32, ByVal duplicate As Boolean, ByVal draft As Boolean, ByVal schema As String) As ReportDataResults
            Dim returnDS As DataSet = GetSchemaDataSet(schema)
            Dim newRow As DataRow = returnDS.Tables("BOConsignment").NewRow()
            Dim baseApp As BOApplication = BOApplication.PolymorphicCreate(applicationId)

            If TypeOf baseApp Is BOImportExportApplication Then
                Dim application As BOImportExportApplication = CType(baseApp, BOImportExportApplication)
                Dim permits() As BOPermit = application.Permit
                Dim firstPermit As BOCITESImportExportPermit = CType(permits(0), BOCITESImportExportPermit)
                Dim firstInfo As BOPermitInfo = firstPermit.GetPermitInfos(Nothing)(0)
                Dim maxPermits As Int32 = permits.Length
                Dim pageNN As Int32 = CType(((maxPermits - 1) / 3), Int32) + 2 ' Calculate Page MM of NN
                Dim reference As String = firstPermit.ReportISOCode + firstPermit.ReportApplicationRef
                Dim index As Int32 = -1

                FillBasicDetails(application, firstPermit, firstInfo, newRow, True, True, True, duplicate) 'flags, barcode, fixed fields, etc
                FillExtraDetails(application, firstPermit, firstInfo, newRow)                              'countries, user name, etc
                FillPartyDetails(application, newRow, True)                                                'authorities, importer, exporter, etc
                newRow.Item("LinkId") = 4
                newRow.Item("PermitReference") = reference
                newRow.Item("PageMofN") = "Page 1 of " & pageNN.ToString

                CloneDataSets(returnDS, newRow, "BOConsignment", draft, True)
                returnDS = GetConsignmentSpecimenReportData(returnDS, application, permits, reference, duplicate, draft)
            End If
            Return New ReportDataResults(returnDS, "")
        End Function

        Private Shared Function GetConsignmentSpecimenReportData(ByRef returnDS As DataSet, ByVal application As BOImportExportApplication, ByVal permits() As BOPermit, ByVal reference As String, ByVal duplicate As Boolean, ByVal draft As Boolean) As DataSet
            Dim firstPermit As BOCITESImportExportPermit = CType(permits(0), BOCITESImportExportPermit)
            Dim firstInfo As BOPermitInfo = firstPermit.GetPermitInfos(Nothing)(0)
            Dim maxPermits As Int32 = permits.Length
            Dim pageNN As Int32 = CType(((maxPermits - 1) / 3), Int32) + 2
            Dim index As Int32 = -1

            For pageMM As Int32 = 2 To pageNN
                Dim newRow As DataRow = returnDS.Tables("BOSpecimens").NewRow()
                FillBasicDetails(application, firstPermit, firstInfo, newRow, False, False, True, duplicate)
                With newRow
                    ' Create new Page Permit Details
                    .Item("LinkId") = 1
                    .Item("PermitReference") = reference
                    .Item("SheetDescription") = "ORIGINAL"
                    .Item("SheetNumber") = "1"
                    .Item("PageMofN") = "Page " & pageMM.ToString & " of " & pageNN.ToString
                    index += 1
                    If index < maxPermits Then
                        FillPermitData(newRow, permits(index), reference, "Specimen1", index + 1)
                    End If
                    index += 1
                    If index < maxPermits Then
                        FillPermitData(newRow, permits(index), reference, "Specimen2", index + 1)
                    End If
                    index += 1
                    If index < maxPermits Then
                        FillPermitData(newRow, permits(index), reference, "Specimen3", index + 1)
                    End If
                End With
                CopyDataSets(returnDS, newRow)
            Next pageMM
            Return returnDS
        End Function

        Private Shared Sub CopyDataSets(ByVal returnDS As DataSet, ByVal newRow As DataRow)
            Dim copyDataset As DataSet
            Dim copyDataRow As DataRow

            'add the row to the dataset - Sheet 1
            returnDS.Tables("BOSpecimens").Rows.Add(newRow)
            returnDS.AcceptChanges()

            'add the row to the dataset - Sheet 2
            returnDS.Tables("BOSpecimens").ImportRow(newRow)
            newRow = returnDS.Tables("BOSpecimens").Rows(returnDS.Tables("BOSpecimens").Rows.Count - 1)
            newRow.Item("SheetNumber") = "2"
            newRow.Item("SheetDescription") = "COPY for the holder"
            returnDS.AcceptChanges()


            'add the row to the dataset - Sheet 3
            returnDS.Tables("BOSpecimens").ImportRow(newRow)
            newRow = returnDS.Tables("BOSpecimens").Rows(returnDS.Tables("BOSpecimens").Rows.Count - 1)
            newRow.Item("SheetNumber") = "3"
            newRow.Item("SheetDescription") = "COPY for return by customs to issuing"
            returnDS.AcceptChanges()


            'add the row to the dataset - Sheet 4
            returnDS.Tables("BOSpecimens").ImportRow(newRow)
            newRow = returnDS.Tables("BOSpecimens").Rows(returnDS.Tables("BOSpecimens").Rows.Count - 1)
            newRow.Item("SheetNumber") = "4"
            newRow.Item("SheetDescription") = "COPY for the issuing authority"
            returnDS.AcceptChanges()
        End Sub

        Private Shared Sub CloneDataSets(ByVal returnDS As DataSet, ByVal newRow As DataRow, ByVal objectName As String, ByVal draft As Boolean)
            CloneDataSets(returnDS, newRow, objectName, draft, False)
        End Sub

        Private Shared Sub CloneDataSets(ByVal returnDS As DataSet, ByVal newRow As DataRow, ByVal objectName As String, ByVal draft As Boolean, ByVal useLinkId As Boolean)
            Dim copyDataset As DataSet
            Dim copyDataRow As DataRow

            'add the row to the dataset - Sheet 1
            returnDS.Tables(objectName).Rows.Add(newRow)
            returnDS.AcceptChanges()

            If Not draft Then
                'add the row to the dataset - Sheet 2
                copyDataset = returnDS.Clone
                copyDataset.Tables(objectName).ImportRow(newRow)
                copyDataRow = copyDataset.Tables(objectName).Rows(0)
                copyDataRow.Item("SheetNumber") = "2"
                copyDataRow.Item("SheetDescription") = "COPY for the holder"
                If useLinkId Then
                    copyDataRow.Item("LinkId") = 3
                End If
                returnDS.Merge(copyDataset)
                returnDS.AcceptChanges()

                'add the row to the dataset - Sheet 3
                copyDataset = returnDS.Clone()
                copyDataset.Tables(objectName).ImportRow(newRow)
                copyDataRow = copyDataset.Tables(objectName).Rows(0)
                copyDataRow.Item("SheetNumber") = "3"
                copyDataRow.Item("SheetDescription") = "COPY for return by customs to issuing"
                If useLinkId Then
                    copyDataRow.Item("LinkId") = 2
                End If
                returnDS.Merge(copyDataset)
                returnDS.AcceptChanges()

                'add the row to the dataset - Sheet 4
                copyDataset = returnDS.Clone()
                copyDataset.Tables(objectName).ImportRow(newRow)
                copyDataRow = copyDataset.Tables(objectName).Rows(0)
                copyDataRow.Item("SheetNumber") = "4"
                copyDataRow.Item("SheetDescription") = "COPY for the issuing authority"
                If useLinkId Then
                    copyDataRow.Item("LinkId") = 1
                End If
                returnDS.Merge(copyDataset)
                returnDS.AcceptChanges()
            End If

        End Sub


        Public Shared Function GetPermitReportData(ByVal permitInfoId As Int32, ByVal duplicate As Boolean, ByVal draft As Boolean, ByVal schema As String) As ReportDataResults
            Dim returnDS As DataSet = GetSchemaDataSet(schema)
            Dim newRow As DataRow = returnDS.Tables("BOPermit").NewRow()
            Dim info As New BOPermitInfo(permitInfoId)
            Dim permit As BOPermit = BOPermit.PolymorphicCreate(info.PermitId, Nothing)
            Dim reference As String = ""

            If TypeOf permit Is BOCITESImportExportPermit Then
                Dim iePermit As BOCITESImportExportPermit = CType(permit, BOCITESImportExportPermit)
                Dim application As BOImportExportApplication = iePermit.GetImportExportApplication()

                'application.ManagementAuthority.Party.GetMailingAddress(Nothing).ISO2CountryCode()
                reference = iePermit.PermitReference(iePermit.GetCopyNo(info))
                FillBasicDetails(application, iePermit, info, newRow, True, True, True, duplicate) 'flags, barcode, fixed fields, etc
                FillExtraDetails(application, iePermit, info, newRow)                              'countries, user name, etc
                FillPartyDetails(application, newRow, True)                                        'authorities, importer, exporter, etc
                FillAgentDetails(application, newRow)                                              'agent
                FillPermitData(newRow, iePermit, "Specimen")                                       'permit details
                newRow.Item("PermitReference") = reference
                newRow.Item("PageMofN") = "Page 1 of 1"                                            ' Do not set from BO's
            End If

            CloneDataSets(returnDS, newRow, "BOPermit", draft)
            Return New ReportDataResults(returnDS, reference)
        End Function


        Public Shared Function PermitReportData(ByVal permitInfoId As Int32, ByVal schema As String) As ReportDataResults
            Dim PermitInfo As New BOPermitInfo(permitInfoId)
            If Not PermitInfo Is Nothing Then
                Dim ImportPermit As BOCITESImportExportPermit = LoadByPermitId(PermitInfo.PermitId)
                If Not ImportPermit Is Nothing Then
                    Return ImportPermit.PermitReportData(schema, PermitInfo)
                End If
            End If
        End Function

        Protected Function GetBarCode(ByVal IssueLetter As String, ByVal IssueDate As Date) As String 'MLD 9/11/4 moved here so it can be reused for Article 10 permits
            Dim paddedPermitNo As String = ReportPermitNo.Replace("/", "").PadRight(13, "0"c)
            Return String.Concat("00", ReportISOCode, ReportApplicationRef, paddedPermitNo, NumberOfCopies, IssueLetter, IssueDate.ToString("ddMMyyyy"))
        End Function

        Protected Function GetCopyNo(ByVal info As BOPermitInfo) As String
            Dim copyNo As String = ""
            If CType(NumberOfCopies, Int32) > 1 Then
                copyNo = info.SequenceNumber.ToString.PadLeft(3, "0"c)
            End If
            Return copyNo
        End Function

        Public Function PermitReportData(ByVal schema As String, ByVal permitInfo As BOPermitInfo) As ReportDataResults
            Dim returnDS As DataSet = GetSchemaDataSet(schema)
            Dim Application As BOImportExportApplication = GetImportExportApplication() ' MLD code moved to method 18/11/4
            Dim NewRow As DataRow = returnDS.Tables("BOPermit").NewRow()
            Dim copyNo As String = GetCopyNo(permitInfo)
            Dim PermitRef As String = PermitReference(copyNo)

            FillBasicDetails(Application, Me, permitInfo, NewRow, True, True, True, False) 'MLD refactored 17/11/4
            FillPartyDetails(Application, NewRow, True)                                    'authorities, importer, exporter, etc
            FillAgentDetails(Application, NewRow)                                          'agent
            With NewRow
                'populate the new row with data
                .Item("PermitNo") = ReportPermitNo
                .Item("CopyNo") = copyNo
                .Item("IsoCountryCode") = ReportISOCode
                .Item("ApplicationRef") = ReportApplicationRef
                .Item("PermitReference") = PermitRef
                .Item("ImportCountry") = Resolve(Application.CountryOfImport, "LongName")   'MLD refactored 17/11/4
                .Item("ReExportCountry") = Resolve(Application.CountryOfExport, "LongName") 'MLD refactored 17/11/4
                .Item("Specimen_Heading") = String.Empty
                .Item("Specimen_Descriptions") = String.Empty
                If Not Specimens Is Nothing AndAlso _
                   Specimens.Length > 0 Then
                    For Each spec As BOSpecimen In Specimens
                        If .Item("Specimen_Heading").ToString.Length = 0 Then
                            'should only do this the once
                            If Not Specie Is Nothing Then
                                Dim PartAndDerivitiveCode As String = ""
                                If Not Derivative Is Nothing Then
                                    PartAndDerivitiveCode = Derivative.Code.TrimEnd & " "
                                End If
                                .Item("Specimen_Heading") = String.Concat(PartAndDerivitiveCode, _
                                                                          Specie.ReportDescription, " ", _
                                                                          Specie.ReportAppliedForName).Trim
                            End If

                            'now do the other specimens one-by-one
                            .Item("Specimen_Appendix") = Specie.CITESAppendix
                            .Item("Specimen_ECAnnex") = Specie.ECAnnex
                            Dim Source As String = ""
                            If Not Source1 Is Nothing Then
                                Source = Source1.Code
                            End If
                            If Not Source2 Is Nothing Then
                                If Not Source1 Is Nothing Then Source &= " "
                                Source &= Source2.Code
                            End If
                            .Item("Specimen_Source") = Source
                            If Not Purpose Is Nothing Then .Item("Specimen_Purpose") = Purpose.Code
                            '.Item("Specimen_Purpose") = Purpose.Code
                            If Not CountryOfOrigin Is Nothing Then .Item("Specimen_OriginCountry") = CountryOfOrigin.LongName
                            .Item("Specimen_OriginPermitRef") = CountryOfOriginPermitNo_String
                            .Item("Specimen_OriginPermitDate") = CountryOfOriginPermitDate_String

                            If Not Application.CountryOfExport Is Nothing Then .Item("Specimen_LastReExportCountry") = Application.CountryOfExport.LongName
                            If Not CountryOfOriginPermitNumberInteger Is Nothing Then .Item("Specimen_OriginCertificateRef") = CountryOfOriginPermitNumberInteger.ToString
                            .Item("Specimen_OriginCertificateDate") = CountryOfOriginPermitDate_String

                            If Not Specie Is Nothing Then
                                .Item("Specimen_CommonName") = Specie.CommonName
                                .Item("Specimen_ScientificName") = Specie.ScientificName
                            End If
                            .Item("Specimen_Descriptions") = spec.ReportDescription 'ignore lineage for now!
                            '  Exit For
                        End If

                        '.Item("Specimen_Heading") = String.Empty
                        '.Item("Specimen_Descriptions") = String.Empty
                        If Not spec.UOM Is Nothing Then
                            '   If spec.UOM.Qty > 0 Then
                            .Item("Specimen_Quantity") = spec.UOM.Qty.ToString
                            'Else
                            .Item("Specimen_Mass") = spec.UOM.NetMass
                            ' End If
                        End If
                    Next spec
                End If

                'find the authorised user
                'Dim PermitHistoryItems As DataObjects.Collection.PermitHistoryBoundCollection = BOPermitHistory.GetHistory(permitInfo.PermitId)
                'If Not PermitHistoryItems Is Nothing AndAlso PermitHistoryItems.Count > 0 Then
                '    '    'these are returned with the most recent items first.
                '    '    'loop back until we find an authroised on
                '    '    For Each PH As DataObjects.Entity.PermitHistory In PermitHistoryItems
                '    '        If Not PH.IsPermitStatusIdNull AndAlso _
                '    '           PH.PermitStatusId = BOPermitInfo.PermitStatusTypes.Authorised Then
                '    '            ' now we've
                '    '.Item("UserName") = me.au permitinfo.ActionResult "Julia Hassell"
                '    '        End If
                '    '    Next PH
                'Else
                '    'unknown
                '    .Item("UserName") = ""
                'End If
                .Item("UserName") = Resolve(permitInfo.IssuedBy, "FullName")    'MLD 17/11/4 refactored
                .Item("IssuePlace") = Resolve(permitInfo.PlaceOfIssue)          'MLD 17/11/4 refactored

                'hard coded
                .Item("PageMofN") = "Page 1 of 1"

                .Item("SurrenderBorderX") = "X"
                .Item("SurrenderIssuingX") = ""
            End With

            'add the row to the dataset
            returnDS.Tables("BOPermit").Rows.Add(NewRow)

            'return the datset containing the single row
            Return New ReportDataResults(returnDS, PermitRef)
        End Function
#End Region



    End Class
End Namespace

Namespace Application
    Public Class BOSpecimenReport
        Inherits BaseBO
        Implements Application.IBOSpecimenReport


#Region "Properties"
        Public Property AimsObjectives() As String Implements IBOSpecimenReport.AimsObjectives
            Get
                Return mAimsObjectives
            End Get
            Set(ByVal Value As String)
                mAimsObjectives = Value
            End Set
        End Property
        Private mAimsObjectives As String


        Public Property BreedingSuccess() As String Implements IBOSpecimenReport.BreedingSuccess
            Get
                Return mBreedingSuccess
            End Get
            Set(ByVal Value As String)
                mBreedingSuccess = Value
            End Set
        End Property
        Private mBreedingSuccess As String

        Public Property HowObjectivesAchieved() As String Implements IBOSpecimenReport.HowObjectivesAchieved
            Get
                Return mHowObjectivesAchieved
            End Get
            Set(ByVal Value As String)
                mHowObjectivesAchieved = Value
            End Set
        End Property
        Private mHowObjectivesAchieved As String

        Public Property OtherSpecimensInvolved() As String Implements IBOSpecimenReport.OtherSpecimensInvolved
            Get
                Return mOtherSpecimensInvolved
            End Get
            Set(ByVal Value As String)
                mOtherSpecimensInvolved = Value
            End Set
        End Property
        Private mOtherSpecimensInvolved As String

        Public Property Markings() As String Implements IBOSpecimenReport.Markings
            Get
                Return mMarkings
            End Get
            Set(ByVal Value As String)
            End Set
        End Property
        Private mMarkings As String

        Public Property HatchDate() As String Implements IBOSpecimenReport.HatchDate
            Get
                Return mHatchDate
            End Get
            Set(ByVal Value As String)
            End Set
        End Property
        Private mHatchDate As String

        Public Property HatchDateExact() As String Implements IBOSpecimenReport.HatchDateExact
            Get
                Return mHatchDateExact
            End Get
            Set(ByVal Value As String)
            End Set
        End Property
        Private mHatchDateExact As String

        Public Property Sex() As String Implements IBOSpecimenReport.Sex
            Get
                Return mSex
            End Get
            Set(ByVal Value As String)
            End Set
        End Property
        Private mSex As String

        Public Property CommonName() As String Implements IBOSpecimenReport.CommonName
            Get
                Return mCommonName
            End Get
            Set(ByVal Value As String)
            End Set
        End Property
        Private mCommonName As String

        Public Property ScientificName() As String Implements IBOSpecimenReport.ScientificName
            Get
                Return mScientificName
            End Get
            Set(ByVal Value As String)
            End Set
        End Property
        Private mScientificName As String

        Public Property StudBookNumber() As String Implements IBOSpecimenReport.StudBookNumber
            Get
                Return mStudBookNumber
            End Get
            Set(ByVal Value As String)
                mStudBookNumber = Value
            End Set
        End Property
        Private mStudBookNumber As String

        Public Property PresentSpecAddress() As String Implements IBOSpecimenReport.PresentSpecAddress
            Get
                Return mPresentSpecAddress
            End Get
            Set(ByVal Value As String)
                mPresentSpecAddress = Value
            End Set
        End Property
        Private mPresentSpecAddress As String


        Public Property BreederNameAddress() As String Implements IBOSpecimenReport.BreederNameAddress
            Get
                Return mBreederNameAddress
            End Get
            Set(ByVal Value As String)
                mBreederNameAddress = Value
            End Set
        End Property
        Private mBreederNameAddress As String

        Public Property BreedingFactilities() As String Implements IBOSpecimenReport.BreedingFactilities
            Get
                Return mBreedingFactilities
            End Get
            Set(ByVal Value As String)
                mBreedingFactilities = Value
            End Set
        End Property
        Private mBreedingFactilities As String

        Public Property BreedingStockSizeDetails() As String Implements IBOSpecimenReport.BreedingStockSizeDetails
            Get
                Return mBreedingStockSizeDetails
            End Get
            Set(ByVal Value As String)
                mBreedingStockSizeDetails = Value
            End Set
        End Property
        Private mBreedingStockSizeDetails As String

        Public Property BreedingTechniqueDetails() As String Implements IBOSpecimenReport.BreedingTechniqueDetails
            Get
                Return mBreedingTechniqueDetails
            End Get
            Set(ByVal Value As String)
                mBreedingTechniqueDetails = Value
            End Set
        End Property
        Private mBreedingTechniqueDetails As String

        Public Property BreedingwithoutWildAugmentationDetails() As String Implements IBOSpecimenReport.BreedingwithoutWildAugmentationDetails
            Get
                Return mBreedingwithoutWildAugmentationDetails
            End Get
            Set(ByVal Value As String)
                mBreedingwithoutWildAugmentationDetails = Value
            End Set
        End Property
        Private mBreedingwithoutWildAugmentationDetails As String

        Public Property CaptivebredGeneration() As String Implements IBOSpecimenReport.CaptivebredGeneration
            Get
                Return mCaptivebredGeneration
            End Get
            Set(ByVal Value As String)
                mCaptivebredGeneration = Value
            End Set
        End Property
        Private mCaptivebredGeneration As String

        Public Property EntryDateTime() As Date
            Get
                Return mEntryDateTime
            End Get
            Set(ByVal Value As Date)

            End Set
        End Property
        Private mEntryDateTime As Date

        Public Property CountryOfOriginId() As Object Implements IBOSpecimenReport.CountryOfOriginId
            Get
                Return mCountryOfOriginId
            End Get
            Set(ByVal Value As Object)
                mCountryOfOriginId = Value
            End Set
        End Property
        Private mCountryOfOriginId As Object

        Public Property DeclarationAcknowledged() As Boolean Implements IBOSpecimenReport.DeclarationAcknowledged
            Get
                Return mDeclarationAcknowledged
            End Get
            Set(ByVal Value As Boolean)
                mDeclarationAcknowledged = Value
            End Set
        End Property
        Private mDeclarationAcknowledged As Boolean

        Public Property DOENumber() As Object Implements IBOSpecimenReport.DOENumber
            Get
                Return mDOENumber
            End Get
            Set(ByVal Value As Object)
                mDOENumber = Value
            End Set
        End Property
        Private mDOENumber As Object


        Public Property EstablishmentBredtoF2Details() As String Implements IBOSpecimenReport.EstablishmentBredtoF2Details
            Get
                Return mEstablishmentBredtoF2Details
            End Get
            Set(ByVal Value As String)
                mEstablishmentBredtoF2Details = Value
            End Set
        End Property
        Private mEstablishmentBredtoF2Details As String

        Public Property ExportNumber() As Object Implements IBOSpecimenReport.ExportNumber
            Get
                Return mExportNumber
            End Get
            Set(ByVal Value As Object)
                mExportNumber = Value
            End Set
        End Property
        Private mExportNumber As Object

        Public Property FounderBreedingStockDetails() As String Implements IBOSpecimenReport.FounderBreedingStockDetails
            Get
                Return mFounderBreedingStockDetails
            End Get
            Set(ByVal Value As String)
                mFounderBreedingStockDetails = Value
            End Set
        End Property
        Private mFounderBreedingStockDetails As String

        Public Property Importer() As Object Implements IBOSpecimenReport.Importer
            Get
                Return mImporter
            End Get
            Set(ByVal Value As Object)
                mImporter = Value
            End Set
        End Property
        Private mImporter As Object

        Public Property ImportExportPurposeDetails() As String Implements IBOSpecimenReport.ImportExportPurposeDetails
            Get
                Return mImportExportPurposeDetails
            End Get
            Set(ByVal Value As String)
                mImportExportPurposeDetails = Value
            End Set
        End Property
        Private mImportExportPurposeDetails As String

        Public Property ImportNumber() As Object Implements IBOSpecimenReport.ImportNumber
            Get
                Return mImportNumber
            End Get
            Set(ByVal Value As Object)
                mImportNumber = Value
            End Set
        End Property
        Private mImportNumber As Object

        Public Property OriginId() As Integer Implements IBOSpecimenReport.OriginId
            Get
                Return mOriginId
            End Get
            Set(ByVal Value As Integer)
                mOriginId = Value
            End Set
        End Property
        Private mOriginId As Int32

        Public Property OtherCITESSourceExplanation() As String Implements IBOSpecimenReport.OtherCITESSourceExplanation
            Get
                Return mOtherCITESSourceExplanation
            End Get
            Set(ByVal Value As String)
                mOtherCITESSourceExplanation = Value
            End Set
        End Property
        Private mOtherCITESSourceExplanation As String

        Public Property OtherInformation() As String Implements IBOSpecimenReport.OtherInformation
            Get
                Return mOtherInformation
            End Get
            Set(ByVal Value As String)
                mOtherInformation = Value
            End Set
        End Property
        Private mOtherInformation As String

        Public Property PermitId() As Integer Implements IBOSpecimenReport.PermitId
            Get
                Return mPermitId
            End Get
            Set(ByVal Value As Integer)
                mPermitId = Value
            End Set
        End Property
        Private mPermitId As Int32

        Public Property SeizedByCustoms() As Boolean Implements IBOSpecimenReport.SeizedByCustoms
            Get
                Return mSeizedByCustoms
            End Get
            Set(ByVal Value As Boolean)
                mSeizedByCustoms = Value
            End Set
        End Property
        Private mSeizedByCustoms As Boolean

        Public Property SpeciesConservationDetails() As String Implements IBOSpecimenReport.SpeciesConservationDetails
            Get
                Return mSpeciesConservationDetails
            End Get
            Set(ByVal Value As String)
                mSpeciesConservationDetails = Value
            End Set
        End Property
        Private mSpeciesConservationDetails As String

        Public Property SpecimenId() As Integer Implements IBOSpecimenReport.SpecimenId
            Get
                Return mSpecimenId
            End Get
            Set(ByVal Value As Integer)
                mSpecimenId = Value
            End Set
        End Property
        Private mSpecimenId As Int32

        Public Property SpecimenReportDate() As Date Implements IBOSpecimenReport.SpecimenReportDate
            Get
                Return mSpecimenReportDate
            End Get
            Set(ByVal Value As Date)
                mSpecimenReportDate = Value
            End Set
        End Property
        Private mSpecimenReportDate As Date

        Public Property SpecimenReportId() As Integer Implements IBOSpecimenReport.SpecimenReportId
            Get
                Return mSpecimenReportId
            End Get
            Set(ByVal Value As Integer)
                mSpecimenReportId = Value
            End Set
        End Property
        Private mSpecimenReportId As Int32

        Public Property WildTakenDate() As Object Implements IBOSpecimenReport.WildTakenDate
            Get
                Return mWildTakenDate
            End Get
            Set(ByVal Value As Object)
                mWildTakenDate = Value
            End Set
        End Property
        Private mWildTakenDate As Object

        Public Property WildTakenDisabilityDetails() As String Implements IBOSpecimenReport.WildTakenDisabilityDetails
            Get
                Return mWildTakenDisabilityDetails
            End Get
            Set(ByVal Value As String)
                mWildTakenDisabilityDetails = Value
            End Set
        End Property
        Private mWildTakenDisabilityDetails As String

        Public Property Role() As String Implements IBOSpecimenReport.role
            Get
                Return mRole
            End Get
            Set(ByVal Value As String)
                mRole = Value
            End Set
        End Property
        Private mRole As String

        Public Property SourceUnknownExplanation() As String Implements IBOSpecimenReport.SourceUnknownExplanantion
            Get
                Return mSourceUnknownExplanation
            End Get
            Set(ByVal Value As String)
                mSourceUnknownExplanation = Value
            End Set
        End Property
        Private mSourceUnknownExplanation As String
#End Region

#Region "Save"
        Public Shadows Function Save(ByVal permissions As String, ByVal tran As SqlClient.SqlTransaction, ByVal authorisedUserId As Int64, ByVal changeStatus As Boolean) As BO.Application.BOSpecimenReport
            MyBase.Save()
            Dim NewSR As New DataObjects.Entity.SpecimenReport
            Dim service As DataObjects.Service.SpecimenReportService = NewSR.ServiceObject

            Created = (mSpecimenReportId = 0)

            If Created Then
                NewSR = service.Insert(SpecimenId, _
                                        PermitId, _
                                        OriginId, _
                                        SeizedByCustoms, _
                                        ImportNumber, _
                                        Me.Importer, _
                                        ExportNumber, _
                                        Me.CountryOfOriginId, _
                                        Me.BreedingFactilities, _
                                        SpecimenReportDate, _
                                        Me.OtherCITESSourceExplanation, _
                                        BreederNameAddress, _
                                        Me.CaptivebredGeneration, _
                                        Me.OtherInformation, _
                                         Me.WildTakenDate, _
                                        Me.DOENumber, _
                                        Me.WildTakenDisabilityDetails, _
                                        Me.BreedingTechniqueDetails, _
                                        Me.FounderBreedingStockDetails, _
                                        Me.EstablishmentBredtoF2Details, _
                                        Me.BreedingwithoutWildAugmentationDetails, _
                                        Me.BreedingStockSizeDetails, _
                                        Me.ImportExportPurposeDetails, _
                                         Me.SpeciesConservationDetails, _
                                        Me.DeclarationAcknowledged, _
                                         Nothing, _
                                         Me.StudBookNumber, _
                                         Me.Role, _
                                         Me.PresentSpecAddress, _
                                         Me.SourceUnknownExplanation, _
                                           AimsObjectives, _
                                          HowObjectivesAchieved, _
                                          OtherSpecimensInvolved, _
                                         BreedingSuccess, _
                                        tran)
            Else
                If changeStatus Then mEntryDateTime = Date.Now
                NewSR = service.Update(SpecimenReportId, SpecimenId, _
                                        PermitId, _
                                        OriginId, _
                                        SeizedByCustoms, _
                                        ImportNumber, _
                                        Me.Importer, _
                                        ExportNumber, _
                                        Me.CountryOfOriginId, _
                                        Me.BreedingFactilities, _
                                        SpecimenReportDate, _
                                        Me.OtherCITESSourceExplanation, _
                                        BreederNameAddress, _
                                        Me.CaptivebredGeneration, _
                                        Me.OtherInformation, _
                                        Me.WildTakenDate, _
                                        Me.DOENumber, _
                                        Me.WildTakenDisabilityDetails, _
                                        Me.BreedingTechniqueDetails, _
                                        Me.FounderBreedingStockDetails, _
                                        Me.EstablishmentBredtoF2Details, _
                                        Me.BreedingwithoutWildAugmentationDetails, _
                                        Me.BreedingStockSizeDetails, _
                                        Me.ImportExportPurposeDetails, _
                                        Me.SpeciesConservationDetails, _
                                        Me.DeclarationAcknowledged, _
                                        mEntryDateTime, _
                                        Me.StudBookNumber, _
                                        Me.Role, _
                                          Me.PresentSpecAddress, _
                                         Me.SourceUnknownExplanation, _
                                          AimsObjectives, _
                                         HowObjectivesAchieved, _
                                         OtherSpecimensInvolved, _
                                        BreedingSuccess, _
                                        tran)
            End If
            'check to see if any SQL errors have occured
            If NewSR Is Nothing AndAlso Not DataObjects.Sprocs.LastError Is Nothing Then
                'TODO: Use errors collection to check to see if the problem was concurrency
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveCITESNotification)
            ElseIf NewSR Is Nothing Then
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveCITESNotification)
            Else
                If Created And Not NewSR Is Nothing Then
                    Me.mSpecimenReportId = NewSR.Id


                End If
                If changeStatus Then
                    If Me.ChangeStatus(authorisedUserId, tran) Then
                        '  service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Commit)
                    Else
                        service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    End If
                Else
                    service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Commit)
                End If

                'no point in initialising unless things have changed
                If NewSR.CheckSum <> CheckSum Then
                    Me.InitialiseSR(NewSR, tran)
                End If
            End If
            Return Me
        End Function

        Private Function ChangeStatus(ByVal authorisedUserId As Int64, ByVal tran As SqlClient.SqlTransaction) As Boolean
            Return True
            'if refered to customer for spec report then move back to previous owner and status of progress allowed
            Dim BoPermit As New BO.Application.BOPermit(Me.PermitId)
            Dim PIs As BO.Application.BOPermitInfo() = BoPermit.GetPermitInfos(tran)
            Dim Ids(PIs.Length - 1) As Int32
            Dim i As Int32

            For Each pi As BO.Application.BOPermitInfo In BoPermit.GetPermitInfos(tran)
                Ids(i) = pi.PermitInfoId
                i += 1
            Next


            Return BO.Application.BOPermitInfo.ChangeStatus(Ids, Common.AssignedToList.CaseOfficer, authorisedUserId, _
                New BO.Application.AdditionalInformation(BO.Application.BOPermitInfo.PermitStatusTypes.ProgressAllowed), tran)
        End Function
#End Region

#Region " Prelim code "
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal permitId As Int32, ByVal specimenId As Int32, ByVal tran As SqlClient.SqlTransaction)
            MyClass.New()
            Try
                LoadSR(permitId, specimenId, tran)
            Catch ex As RecordDoesNotExist

            End Try
            Dim DOPermit As New [DO].DataObjects.Entity.Permit(permitId, tran)
            Dim DOSpecie As New [DO].DataObjects.Entity.Specie(DOPermit.SpecieId)
            Me.mCommonName = DOSpecie.CommonName
            Me.mScientificName = DOSpecie.ScientificName
            Dim Spec As New BO.Application.BOSpecimen(specimenId, tran)
            Me.mSex = Spec.Gender.ToString()
            Me.mMarkings = Spec.ReportMark
            If Spec.DOB <> Nothing Then     'MLD 31/1/5
                mHatchDate = CType(Spec.DOB, String)
            Else
                mHatchDate = "Unknown"
            End If
            mHatchDateExact = Application.Search.ApplicationSearch.ConvertToEnglishBoolean(Spec.ExactDOB)
        End Sub

        Private Function LoadSR(ByVal permitId As Int32, ByVal specimenId As Int32) As DataObjects.Entity.SpecimenReport
            Return LoadSR(permitId, specimenId, Nothing)
        End Function

        Private Function LoadSR(ByVal permitId As Int32, ByVal specimenId As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.SpecimenReport
            Me.SpecimenId = specimenId
            Me.PermitId = permitId
            Dim Service As New uk.gov.defra.Phoenix.DO.DataObjects.Service.SpecimenReportService
            Dim NewSRSet As DataObjects.EntitySet.SpecimenReportSet = Service.GetByIndex_IX_SpecimenReport(specimenId, permitId, tran) 'MLD 23/11/4 params reversed
            If NewSRSet Is Nothing OrElse NewSRSet.Count = 0 Then
                Throw New RecordDoesNotExist("Specimen Report", 0)
            Else
                InitialiseSR(NewSRSet.Entities(0), tran)
                Return NewSRSet.Entities(0)
            End If
        End Function

        Protected Overridable Sub InitialiseSR(ByVal sr As DataObjects.Entity.SpecimenReport, ByVal tran As SqlClient.SqlTransaction)
            With sr
                If Not .IsSourceUnknownExplanationNull Then SourceUnknownExplanation = .SourceUnknownExplanation
                If Not .IsPresentSpecAddressNull Then PresentSpecAddress = .PresentSpecAddress
                If Not .IsBreederNameAddressNull Then BreederNameAddress = .BreederNameAddress
                If Not .IsBreedingFactilitiesNull Then Me.BreedingFactilities = .BreedingFactilities
                If Not .IsBreedingStockSizeDetailsNull Then Me.BreedingStockSizeDetails = .BreedingStockSizeDetails
                If Not .IsBreedingTechniqueDetailsNull Then Me.BreedingTechniqueDetails = .BreedingTechniqueDetails
                If Not .IsBreedingwithoutWildAugmentationDetailsNull Then Me.BreedingwithoutWildAugmentationDetails = .BreedingwithoutWildAugmentationDetails
                If Not .IsCaptivebredGenerationNull Then Me.CaptivebredGeneration = .CaptivebredGeneration
                If Not .IsCountryOfOriginIdNull Then Me.CountryOfOriginId = .CountryOfOriginId
                Me.DeclarationAcknowledged = .DeclarationAcknowledged
                If Not .IsDOENumberNull Then Me.DOENumber = .DOENumber
                If Not .IsEstablishmentBredtoF2DetailsNull Then Me.EstablishmentBredtoF2Details = .EstablishmentBredtoF2Details
                If Not .IsExportNumberNull Then Me.ExportNumber = .ExportNumber
                If Not .IsFounderBreedingStockDetailsNull Then Me.FounderBreedingStockDetails = .FounderBreedingStockDetails
                If Not .IsImporterNull Then Me.Importer = .Importer
                If Not .IsImportExportPurposeDetailsNull Then Me.ImportExportPurposeDetails = .ImportExportPurposeDetails
                If Not .IsImportNumberNull Then Me.ImportNumber = .ImportNumber
                Me.OriginId = .OriginId
                If Not .IsOtherCITESSourceExplanationNull Then Me.OtherCITESSourceExplanation = .OtherCITESSourceExplanation
                If Not .IsOtherInformationNull Then Me.OtherInformation = .OtherInformation
                Me.PermitId = .PermitId
                Me.SeizedByCustoms = .SeizedByCustoms
                If Not .IsSpeciesConservationDetailsNull Then Me.SpeciesConservationDetails = .SpeciesConservationDetails
                Me.SpecimenId = .SpecimenId
                Me.SpecimenReportDate = .SpecimenReportDate
                Me.SpecimenReportId = .SpecimenReportId
                If Not .IsWildTakenDateNull Then Me.WildTakenDate = .WildTakenDate
                If Not .IsWildTakenDisabilityDetailsNull Then Me.WildTakenDisabilityDetails = .WildTakenDisabilityDetails
                Me.StudBookNumber = .StudBookNumber
                If Not .IsRoleNull Then Me.Role = .Role
                If Not .IsHowObjectivesAchievedNull Then mHowObjectivesAchieved = .HowObjectivesAchieved
                If Not .IsBreedingSuccessNull Then Me.mBreedingSuccess = .BreedingSuccess
                If Not .IsOtherSpecimensInvolvedNull Then Me.mOtherSpecimensInvolved = .OtherSpecimensInvolved
                If Not .IsAimsObjectivesNull Then mAimsObjectives = .AimsObjectives

            End With



        End Sub

#End Region




    End Class

End Namespace
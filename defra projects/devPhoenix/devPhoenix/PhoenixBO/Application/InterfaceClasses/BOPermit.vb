Imports uk.gov.defra.Phoenix.DO.DataObjects

Namespace Application
    Public Class BOPermit
        Inherits BaseBO
        Implements IBOPermit


#Region " Prelim code "
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal permitId As Int32, ByVal tran As SqlClient.SqlTransaction)
            MyClass.New()
            LoadPermit(permitId, tran)
        End Sub

        Public Sub New(ByVal permitId As Int32)
            MyClass.New(permitId, Nothing)
        End Sub

        Public Sub New(ByVal applicationId As Int32, ByVal permitNumber As Int32, ByVal tran As SqlClient.SqlTransaction)
            MyClass.New()
            InitialisePermit(GetByApplicationandPermitNumber(applicationId, permitNumber, tran), tran)
        End Sub

        Public Function GetByApplicationandPermitNumber(ByVal applicationId As Int32, ByVal permitNumber As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.Permit
            Dim PermitService As New DataObjects.Service.PermitService
            Dim PermitSet As DataObjects.EntitySet.PermitSet = PermitService.GetByIndex_IX_Permit(permitNumber, applicationId)
            If Not PermitSet Is Nothing Then
                Dim NewPermit As DataObjects.Entity.Permit = PermitSet.Entities(0)
                If NewPermit Is Nothing Then
                    Throw New RecordDoesNotExist("Permit", 0)
                Else
                    Return NewPermit
                End If
            Else
                Throw New RecordDoesNotExist("Permit", 0)
            End If
        End Function
        Private Function LoadPermit(ByVal id As Int32) As DataObjects.Entity.Permit
            Return LoadPermit(id, Nothing)
        End Function

        Private Function LoadPermit(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.Permit
            Dim NewPermit As DataObjects.Entity.Permit = DataObjects.Entity.Permit.GetById(id)
            If NewPermit Is Nothing Then
                Throw New RecordDoesNotExist("Permit", id)
            Else
                InitialisePermit(NewPermit, tran)
                Return NewPermit
            End If
        End Function

        Friend Overridable Sub InitialisePermit(ByVal permit As DataObjects.Entity.Permit, ByVal tran As SqlClient.SqlTransaction)
            Try
                With permit
                    CheckSum = .CheckSum
                    mPermitId = .Id

                    If Not .IsCountryOfOriginIdNull Then mCountryOfOrigin = New ReferenceData.BOCountry(.CountryOfOriginId, tran)
                    If Not .IsCountryOfOriginPermitDateNull Then mCountryOfOriginPermitDate = .CountryOfOriginPermitDate
                    If Not .IsCountryOfOriginPermitNumberNull Then mCountryOfOriginPermitNumber = .CountryOfOriginPermitNumber
                    If Not .IsDescriptionNull Then mDescription = .Description
                    If Not .IsNumberOfCopiesNull Then mNumberOfCopies = .NumberOfCopies
                    If Not .IsSpecieIdNull Then mSpecie = New BOSpecie(.SpecieId, tran)
                    If Not .IsExpiryDateNull Then mExpiryDate = .ExpiryDate
                    mPermitNumber = .PermitNumber

                    mPermitDate = .PermitDate
                    mApplicationId = .ApplicationId
                    If .IsApplicationReferenceNull Then mApplicationPermitNumber = String.Empty Else mApplicationPermitNumber = .ApplicationReference

                    'load the specimens
                    LoadSpecimens(tran)

                    Dim PermitInfos As BOPermitInfo() = GetPermitInfos(tran)
                    If PermitInfos.Length > 0 Then          'MLD 27/1/5 test for Nothing no longer necessary
                        PermitStatus = PermitInfos(0).PermitStatus
                    End If
                End With

            Catch ex As Exception
            End Try
        End Sub

        Friend Sub LoadSpecimens(ByVal tran As SqlClient.SqlTransaction)
            Dim SpecSet As DataObjects.EntitySet.SpecimenSet = DataObjects.Entity.Permit.GetRelatedSpecimens(mPermitId, tran)
            If Not SpecSet Is Nothing Then
                ReDim Specimens(SpecSet.Count - 1)
                Dim Index As Int32 = 0
                For Each spec As DataObjects.Entity.Specimen In SpecSet.Entities
                    Dim NewSpec As New BOSpecimen
                    NewSpec.InitialiseSpecimen(spec, tran)

                    'load the UOM details as this spec is being loaded in the contents of a permit
                    Dim PermitSpec As New DataObjects.Entity.PermitSpecimen(mPermitId, NewSpec.SpecimenId, tran)
                    If Not PermitSpec Is Nothing Then
                        NewSpec.UOM = New BOMeasurement(PermitSpec.UOMId, tran)
                        PermitSpec = Nothing
                    End If

                    Specimens(Index) = NewSpec
                    Index += 1
                Next spec
            End If
        End Sub

#End Region

#Region " Properties "

        'Public Property ReferredForCA() As Boolean
        '    Get
        '        If Not Me.GetPermitInfos(Nothing) Is Nothing Then
        '            For Each pi As BO.Application.BOPermitInfo In GetPermitInfos(Nothing)
        '                If pi.PermitStatus.ID = BOPermitInfo.PermitStatusTypes.ReferredForCAndA Then
        '                    Return True
        '                End If
        '            Next
        '        End If
        '        Return False
        '    End Get
        '    Set(ByVal Value As Boolean)

        '    End Set
        'End Property

        'Public Property CAndASubmitted() As Boolean
        '    Get
        '        Dim DOCandAService As New [DO].DataObjects.Service.AccommodationAndCareService
        '        If Not SpecieId Is Nothing Then
        '            Dim DOCandA As [DO].DataObjects.EntitySet.AccommodationAndCareSet = DOCandAService.GetByIndex_IX_AccommodationAndCare(CType(SpecieId, Int32), PermitId)
        '            Return Not DOCandA Is Nothing AndAlso DOCandA.Entities.Count > 0
        '        End If
        '    End Get
        '    Set(ByVal Value As Boolean)

        '    End Set
        'End Property

        Public Property CreatedById() As Int64 Implements IBOPermit.CreatedById
            Get
                Return mCreatedById
            End Get
            Set(ByVal Value As Int64)
                mCreatedById = Value
            End Set
        End Property
        Private mCreatedById As Int64

        Public Property InputtedByCustomer() As Boolean Implements IBOPermit.InputtedByCustomer
            Get
                Return mInputtedByCustomer
            End Get
            Set(ByVal Value As Boolean)
                mInputtedByCustomer = Value
            End Set
        End Property
        Protected mInputtedByCustomer As Boolean


        Public Property ApplicationPermitNumber() As String Implements IBOPermit.ApplicationPermitNumber
            Get
                Return mApplicationPermitNumber 'ApplicationPermitNumber(PermitNumber, ApplicationId)
            End Get
            Set(ByVal Value As String)
            End Set
        End Property
        Private mApplicationPermitNumber As String

        Public Shared ReadOnly Property ApplicationPermitNumber(ByVal permitNumber As Int32, ByVal applicationid As Int32) As String
            Get
                If permitNumber <= 0 Then permitNumber = 1
                Return String.Concat(applicationid.ToString, "/", permitNumber.ToString)
            End Get
        End Property

        Public Property Quantity() As Int32
            Get
                Dim Qty As Int32
                If Not Specimens Is Nothing Then
                    For Each specimen As BOSpecimen In Specimens
                        If Not specimen.UOM Is Nothing AndAlso Not specimen.UOM.Qty Is Nothing Then
                            Qty += CType(specimen.UOM.Qty, Int32)
                        End If
                    Next
                End If
                Return Qty
            End Get
            Set(ByVal Value As Int32)

            End Set
        End Property

        Public Property NetMass() As Decimal
            Get
                Dim Mass As Decimal
                If Not Specimens Is Nothing Then
                    For Each specimen As BOSpecimen In Specimens
                        If Not specimen.UOM Is Nothing AndAlso Not specimen.UOM.Mass Is Nothing Then
                            Mass += CType(specimen.UOM.Mass, Decimal)
                        End If
                    Next
                End If
                Return Mass
            End Get
            Set(ByVal Value As Decimal)

            End Set
        End Property

        Public Property NetMassString() As String
            Get
                Return GetMassString(False)
            End Get
            Set(ByVal Value As String)

            End Set
        End Property

        Public Property NetMassAbbrevString() As String
            Get
                Return GetMassString(True)
            End Get
            Set(ByVal Value As String)

            End Set
        End Property

        Private Function GetMassString(ByVal getCode As Boolean) As String
            Dim total As Decimal = NetMass  'MLD 30/11/4 added, to return "" if total is zero

            'set a default in case we either don't have any specimens or the mass hasn't been set
            Dim Result As String = String.Empty

            If total > 0 Then
                If Not Specimens Is Nothing Then
                    For Each specimen As BOSpecimen In Specimens
                        If Not specimen.UOM.Mass Is Nothing Then
                            'Note: unit of measurement has to be the same for each specimen#
                            Dim UOM As BO.BOUnitOfMeasurement = specimen.UOM.UOM
                            Result = String.Concat(total, " ")
                            If getCode Then
                                Result &= UOM.Code
                            Else
                                Result &= UOM.Description
                            End If
                            'set the values so may as well bail from the loop
                            Exit For
                        End If
                    Next specimen
                End If
            End If
            Return Result
        End Function

        Public Property PermitNumber() As Int32 Implements IBOPermit.PermitNumber
            Get
                Return mPermitNumber
            End Get
            Set(ByVal Value As Int32)
                mPermitNumber = Value
            End Set
        End Property
        Private mPermitNumber As Int32

        Public Property NumberOfCopies() As Object Implements IBOPermit.NumberOfCopies
            Get
                If mNumberOfCopies Is Nothing Then
                    mNumberOfCopies = 1
                End If
                Return mNumberOfCopies
            End Get
            Set(ByVal Value As Object)
                mNumberOfCopies = Value
            End Set
        End Property
        Private mNumberOfCopies As Object

        Public Property CountryOfOrigin() As ReferenceData.BOCountry Implements IBOPermit.CountryOfOrigin
            Get
                Return mCountryOfOrigin
            End Get
            Set(ByVal Value As ReferenceData.BOCountry)
                mCountryOfOrigin = Value
            End Set
        End Property
        Private mCountryOfOrigin As ReferenceData.BOCountry

        Public Property CountryOfOriginPermitDate() As Object Implements IBOPermit.CountryOfOriginPermitDate
            Get
                Return mCountryOfOriginPermitDate
            End Get
            Set(ByVal Value As Object)
                mCountryOfOriginPermitDate = Value
            End Set
        End Property
        Private mCountryOfOriginPermitDate As Object

        Public Property CountryOfOriginPermitNumber() As String Implements IBOPermit.CountryOfOriginPermitNumber
            Get
                Return mCountryOfOriginPermitNumber
            End Get
            Set(ByVal Value As String)
                mCountryOfOriginPermitNumber = Value
            End Set
        End Property
        Private mCountryOfOriginPermitNumber As String

        Public Property Description() As Object Implements IBOPermit.Description
            Get
                Return mDescription
            End Get
            Set(ByVal Value As Object)
                mDescription = Value
            End Set
        End Property
        Private mDescription As Object

        Public Property PermitDate() As Date Implements IBOPermit.PermitDate
            Get
                Return mPermitDate
            End Get
            Set(ByVal Value As Date)
                mPermitDate = Value
            End Set
        End Property
        Private mPermitDate As Date

        Public Property PermitId() As Integer Implements IBOPermit.PermitId
            Get
                Return mPermitId
            End Get
            Set(ByVal Value As Integer)
                mPermitId = Value
            End Set
        End Property
        Private mPermitId As Int32

        Public Property Specie() As BOSpecie Implements IBOPermit.Specie
            Get
                Return mSpecie
            End Get
            Set(ByVal Value As BOSpecie)
                mSpecie = Value
            End Set
        End Property
        Private mSpecie As BOSpecie

        Public Property ExpiryDate() As Object Implements IBOPermit.ExpiryDate
            Get
                Return mExpiryDate
            End Get
            Set(ByVal Value As Object)
                mExpiryDate = Value
            End Set
        End Property
        Protected mExpiryDate As Object

        Public Property ApplicationId() As Integer Implements IBOPermit.ApplicationId
            Get
                Return mApplicationId
            End Get
            Set(ByVal Value As Integer)
                mApplicationId = Value
            End Set
        End Property
        Private mApplicationId As Int32

        Public Property Specimens() As BOSpecimen() Implements IBOPermit.Specimens
            Get
                Return mSpecimens
            End Get
            Set(ByVal Value() As BOSpecimen)
                mSpecimens = Value
            End Set
        End Property
        Private mSpecimens As BOSpecimen()

        Public Property PermitStatus() As ReferenceData.BOPermitStatus Implements IBOPermit.PermitStatus
            Get
                Return mPermitStatus
            End Get
            Set(ByVal Value As ReferenceData.BOPermitStatus)
                mPermitStatus = Value
            End Set
        End Property
        Private mPermitStatus As ReferenceData.BOPermitStatus

        Public Property ProgressStatusInspection() As ProgressStatus.BOProgressStatusInspection Implements IBOPermit.ProgressStatusInspection
            Get
                Return mProgressStatusInspection
            End Get
            Set(ByVal Value As ProgressStatus.BOProgressStatusInspection)
                mProgressStatusInspection = Value
            End Set
        End Property
        Private mProgressStatusInspection As ProgressStatus.BOProgressStatusInspection

        Public Property ProgressStatusPayment() As ProgressStatus.BOProgressStatusPayment Implements IBOPermit.ProgressStatusPayment
            Get
                Return mProgressStatusPayment
            End Get
            Set(ByVal Value As ProgressStatus.BOProgressStatusPayment)
                mProgressStatusPayment = Value
            End Set
        End Property
        Private mProgressStatusPayment As ProgressStatus.BOProgressStatusPayment

        Public Property ProgressStatusReferralHistory() As ProgressStatus.BOProgressStatusReferralHistory Implements IBOPermit.ProgressStatusReferralHistory
            Get
                Return mProgressStatusReferralHistory
            End Get
            Set(ByVal Value As ProgressStatus.BOProgressStatusReferralHistory)
                mProgressStatusReferralHistory = Value
            End Set
        End Property
        Private mProgressStatusReferralHistory As ProgressStatus.BOProgressStatusReferralHistory

        Public Property ProgressStatusReIssued() As ProgressStatus.BOProgressStatusReIssued Implements IBOPermit.ProgressStatusReIssued
            Get
                Return mProgressStatusReIssued
            End Get
            Set(ByVal Value As ProgressStatus.BOProgressStatusReIssued)
                mProgressStatusReIssued = Value
            End Set
        End Property
        Private mProgressStatusReIssued As ProgressStatus.BOProgressStatusReIssued

        Public Property ProgressStatusSAAdvice() As ProgressStatus.BOProgressStatusSAAdvice Implements IBOPermit.ProgressStatusSAAdvice
            Get
                Return mProgressStatusSAAdvice
            End Get
            Set(ByVal Value As ProgressStatus.BOProgressStatusSAAdvice)
                mProgressStatusSAAdvice = Value
            End Set
        End Property
        Private mProgressStatusSAAdvice As ProgressStatus.BOProgressStatusSAAdvice

        Public Property ReReferredCount() As Integer Implements IBOPermit.ReReferredCount
            Get
                Return mReReferredCount
            End Get
            Set(ByVal Value As Integer)
                mReReferredCount = Value
            End Set
        End Property
        Private mReReferredCount As Int32

        Public Property JNCCAdvice() As String Implements IBOPermit.JNCCAdvice
            Get
                Return mJNCCAdvice
            End Get
            Set(ByVal Value As String)
                mJNCCAdvice = Value
            End Set
        End Property
        Private mJNCCAdvice As String

        Public Property KewAdvice() As String Implements IBOPermit.KewAdvice
            Get
                Return mKewAdvice
            End Get
            Set(ByVal Value As String)
                mKewAdvice = Value
            End Set
        End Property
        Private mKewAdvice As String
#End Region

#Region " Helper Functions "
        Public Property NumberOfCopiesReport() As Int32
            Get
                Try
                    Return CType(NumberOfCopies, Int32)
                Catch ex As Exception
                    Return 1
                End Try
            End Get
            Set(ByVal Value As Int32)
            End Set
        End Property

        Protected ReadOnly Property SpecieId() As Object
            Get
                If mSpecie Is Nothing OrElse mSpecie.SpecieId = 0 Then
                    Return Nothing
                Else
                    Return mSpecie.SpecieId
                End If
            End Get
        End Property

        Private ReadOnly Property NumberOfCopiesInteger() As Object
            Get

                Try
                    Return CType(NumberOfCopies, Int32)
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

        Public Overridable Function GetApplication() As BOApplication
            If ApplicationId = 0 Then
                Throw New ArgumentException("Application Id as 0")
            Else
                Dim App As New BOApplication(ApplicationId, 0)
                Return App
            End If
        End Function

        Private ReadOnly Property CountryOfOriginId() As Object
            Get
                If CountryOfOrigin Is Nothing OrElse CountryOfOrigin.ID = 0 Then
                    Return Nothing
                Else
                    Return CountryOfOrigin.ID
                End If
            End Get
        End Property

        Protected ReadOnly Property ExpiryDate_String() As String
            Get
                If mExpiryDate Is Nothing Then
                    Return String.Empty
                Else
                    Return CType(mExpiryDate, Date).ToString("dd/MM/yyyy")
                End If
            End Get
        End Property

        Protected ReadOnly Property PermitDate_String() As String
            Get
                Return CType(mPermitDate, Date).ToString("dd/MM/yyyy")
            End Get
        End Property

        Protected ReadOnly Property CountryOfOriginPermitDate_String() As String
            Get
                If mCountryOfOriginPermitDate Is Nothing Then
                    Return String.Empty
                Else
                    Return CType(mCountryOfOriginPermitDate, Date).ToString("dd/MM/yyyy")
                End If
            End Get
        End Property

        Protected ReadOnly Property CountryOfOriginPermitNo_String() As String
            Get
                If mCountryOfOriginPermitNumber Is Nothing Or mCountryOfOriginPermitNumber = "0" Then
                    Return String.Empty
                Else
                    Return mCountryOfOriginPermitNumber.ToString
                End If
            End Get
        End Property

        'Public ReadOnly Property PermitInfo() As BOPermitInfo
        '    Get
        '        If mPermitInfo Is Nothing Then
        '            mPermitInfo = New BOPermitInfo(mPermitId)
        '        End If
        '        Return mPermitInfo
        '    End Get
        'End Property
        'Private mPermitInfo As BOPermitInfo = Nothing
#End Region

#Region " Save "
        Public Overloads Overrides Function Save() As BaseBO
            Dim NewPermit As New DataObjects.Entity.Permit
            Dim service As DataObjects.Service.PermitService = NewPermit.ServiceObject
            Dim tran As SqlClient.SqlTransaction = service.BeginTransaction

            Dim SaveResult As BaseBO = MyClass.Save(tran)
            If SaveResult Is Nothing Then
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
            Else
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Commit)
            End If
            Return SaveResult
        End Function

        Public Overridable Overloads Function Save(ByVal tran As SqlClient.SqlTransaction) As BaseBO
            MyBase.Save()

            'save specimens
            Dim SpecimensToSave As BOSpecimen() = Specimens



            Dim NewPermit As New DataObjects.Entity.Permit
            Dim service As DataObjects.Service.PermitService = NewPermit.ServiceObject

            Created = (mPermitId = 0)

            If PermitNumber > 0 Then
                'it's fine, so leave
            Else
                'get a new permit number for this application
                Dim Permit As New DataObjects.Entity.Permit
                Dim Permits As DataObjects.EntitySet.PermitSet = Permit.GetForApplication(mApplicationId, tran)
                If Not Permits Is Nothing AndAlso _
                   Permits.Count > 0 Then
                    Dim HighestPermitNumber As Int32 = 0
                    For Each Permit In Permits

                        If Permit.PermitNumber > 0 AndAlso _
                           Permit.PermitNumber > HighestPermitNumber AndAlso _
                           ((Not Created And Permit.Id <> mPermitId) OrElse Created) Then
                            HighestPermitNumber = Permit.PermitNumber
                        End If
                    Next Permit
                    If HighestPermitNumber > 0 Then
                        PermitNumber = HighestPermitNumber + 1
                    End If
                End If
            End If
            If mPermitNumber <= 0 Then
                PermitNumber = 1
            End If

            If Not mSpecie Is Nothing Then
                'save the specie object
                mSpecie = mSpecie.Save(tran)
                'check to see if there has been any problems
                If Not mSpecie.ValidationErrors Is Nothing Then
                    'rollback the transaction
                    If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    'get the problems and assign them locally
                    ValidationErrors = mSpecie.ValidationErrors
                    'bail
                    Return Me
                End If
            End If

            If Created Then
                NewPermit = service.Insert(CountryOfOriginId, _
                                           mCountryOfOriginPermitDate, _
                                           CountryOfOriginPermitNumberInteger, _
                                           mPermitDate, _
                                           mDescription, _
                                           NumberOfCopiesInteger, _
                                           SpecieId, _
                                           mExpiryDate, _
                                           ApplicationId, _
                                           mPermitNumber, _
                                           mKewAdvice, _
                                           mJNCCAdvice, _
                                           tran)


            Else
                NewPermit = service.Update(mPermitId, _
                                           CountryOfOriginId, _
                                           mCountryOfOriginPermitDate, _
                                           CountryOfOriginPermitNumberInteger, _
                                           mPermitDate, _
                                           mDescription, _
                                           NumberOfCopiesInteger, _
                                           SpecieId, _
                                           mExpiryDate, _
                                           ApplicationId, _
                                           mPermitNumber, _
                                           mKewAdvice, _
                                           mJNCCAdvice, _
                                           CheckSum, _
                                           tran)
            End If
            'check to see if any SQL errors have occured
            If NewPermit Is Nothing AndAlso Not DataObjects.Sprocs.LastError Is Nothing Then
                'TODO: Use errors collection to check to see if the problem was concurrency
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSavePermit)

            ElseIf NewPermit Is Nothing Then
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSavePermit)
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
            Else
                If Created And Not NewPermit Is Nothing Then
                    mPermitId = NewPermit.Id
                End If
                If Created OrElse (NewPermit.CheckSum <> CheckSum AndAlso ShouldCreateNewPermit(NewPermit)) Then
                    'if it's a new permit o
                    If Not BOPermitInfo.CreateFirstPermitInfo(NewPermit, InputtedByCustomer, CreatedById, tran) Then
                        ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSavePermit)
                        Return Me
                    End If
                End If
                'no point in initialising unless things have changed
                If NewPermit.CheckSum <> CheckSum Then InitialisePermit(NewPermit, tran)

                If Not SpecimensToSave Is Nothing AndAlso _
                    SpecimensToSave.Length > 0 Then
                    For Each spec As BOSpecimen In SpecimensToSave
                        'saves and creates the links
                        spec.Save(mPermitId, BOSpecimen.TypeOfSave.PermitId, tran)
                        'If ThisSpec.Created Then
                        '    'add our link in
                        Dim PermitSpecimenService As New DataObjects.Service.PermitSpecimenService
                        Try
                            PermitSpecimenService.Insert(mPermitId, spec.SpecimenId, spec.UOM.UOMId, tran)
                        Catch ex As Exception
                            PermitSpecimenService.Update(mPermitId, spec.SpecimenId, spec.UOM.UOMId, tran)
                        End Try

                        'End If
                    Next spec
                    Specimens = SpecimensToSave
                End If
            End If

            Return Me
        End Function

        Private Function ShouldCreateNewPermit(ByVal newPermit As DataObjects.Entity.Permit) As Boolean
            Return ShouldCreateNewPermit(newPermit, Me)
        End Function

        Private Function ShouldCreateNewPermit(ByVal newPermit As DataObjects.Entity.Permit, ByVal oldPermit As BOPermit) As Boolean
            Return (newPermit.IsNumberOfCopiesNull AndAlso Not oldPermit.NumberOfCopies Is Nothing) OrElse _
                   (Not newPermit.IsNumberOfCopiesNull AndAlso oldPermit.NumberOfCopies Is Nothing) OrElse _
                   (Not newPermit.IsNumberOfCopiesNull AndAlso Not oldPermit.NumberOfCopies Is Nothing AndAlso _
                   newPermit.NumberOfCopies <> CType(oldPermit.NumberOfCopies, Int32))
        End Function
#End Region

#Region " Validate "
        Public Overloads Function Validate(ByVal userID As Int64, ByVal writeFlag As Boolean) As ValidationManager
            ' init the errors list
            MyBase.ValidationErrors = New ValidationManager(ValidationManager.ValidationTitles.CannotSavePermit)


            If MyBase.ValidationErrors.HasErrors Then
                'If writeFlag Then Validated = False
            Else
                'If writeFlag Then Validated = True
                MyBase.ValidationErrors = Nothing
            End If

            Return MyBase.ValidationErrors
        End Function
#End Region

#Region " Operations "

        Public Shared Function UpdateDelogationGuidline(ByVal guidelineId As Int32, ByVal permitInfoIds As Int32()) As Boolean
            Dim service As [DO].DataObjects.Service.PermitService = uk.gov.defra.Phoenix.DO.DataObjects.Base.PermitBase.ServiceObject
                Dim tran As SqlClient.SqlTransaction = service.BeginTransaction
                Dim Permit As BO.Application.BOPermit
                For Each pID As Int32 In permitInfoIds
                    Dim Pi As New BO.Application.BOPermitInfo(pID, tran)
                If BO.Application.BOPermitInfo.BeforeIssued(CType(Pi.PermitStatus.ID, BO.Application.BOPermitInfo.PermitStatusTypes)) Then
                    Permit = BO.Application.BOPermit.PolymorphicCreate(Pi.PermitId, tran)
                    If TypeOf Permit Is BO.Application.CITES.Applications.BOCITESImportExportPermit Then
                        CType(Permit, BO.Application.CITES.Applications.BOCITESImportExportPermit).DelegationOfAuthorityGuidline = New BO.ReferenceData.BODelegationGuideline(guidelineId, tran)
                        Permit = CType(Permit.Save(tran), BO.Application.BOPermit)
                        If Not Permit.ValidationErrors Is Nothing Then
                            service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                            Return False
                        End If
                    End If
                End If
            Next
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Commit)
                Return True
        End Function

        Public Shared Function CanUpdatePermit(ByVal SSOUserId As Int64, ByVal permitId As Int32) As Boolean
            Dim permit As New BO.Application.BOPermit(permitId)
            Dim PIs As BO.Application.BOPermitInfo() = permit.GetPermitInfos(Nothing)
            If PIs Is Nothing Then Return True
            For Each Pi As BO.Application.BOPermitInfo In PIs
                If Pi.CanUpdateApplication(SSOUserId) Then
                    Return True
                End If
            Next
        End Function

        'Public Shared Function CanUpdatePermit(ByVal SSOUserId As Int64, ByVal permit As BO.Application.BOPermit) As Boolean
        '    Dim PIs As BO.Application.BOPermitInfo() = permit.GetPermitInfos(Nothing)
        '    If PIs Is Nothing Then Return True
        '    For Each Pi As BO.Application.BOPermitInfo In PIs
        '        If Pi.CanUpdateApplication(SSOUserId) Then
        '            Return True
        '        End If
        '    Next
        'End Function

        Public Function GetGridNotes(ByVal permitId As Int32, ByVal tran As SqlClient.SqlTransaction) As Party.Note.GridNote()
            Dim Notes As Application.CITES.Applications.ApplicationNote() = GetNotes(permitId, tran)
            Dim GridNotes(Notes.Length - 1) As Party.Note.GridNote

            Dim index As Int32
            For Each Note As BOCommon.BaseNote In Notes
                GridNotes(index) = New Party.Note.GridNote(Note)
                index += 1
            Next

            Return GridNotes
        End Function

        Public Function GetNotes(ByVal permitId As Int32, ByVal tran As SqlClient.SqlTransaction) As Application.CITES.Applications.ApplicationNote()
            Dim Permit As New DataObjects.Entity.Permit(permitId, tran)
            Return GetNotes(Permit, ApplicationId, tran)
        End Function

        Public Function GetNotes(ByVal permit As DataObjects.Entity.Permit, ByVal applicationid As Int32, ByVal tran As SqlClient.SqlTransaction) As Application.CITES.Applications.ApplicationNote()
            Dim Notes As DataObjects.EntitySet.PermitNoteSet = permit.GetRelatedNotes(permit.Id, tran)

            If Not Notes Is Nothing AndAlso _
               Notes.Count > 0 Then
                Dim NoteList(Notes.Count - 1) As Application.CITES.Applications.ApplicationNote
                Dim Index As Int32 = 0
                For Each note As DataObjects.Entity.PermitNote In Notes
                    NoteList(Index) = New Application.CITES.Applications.ApplicationNote(New DataObjects.Entity.Note(note.NoteId), note.ApplicationId)
                    Index += 1
                Next note
                Return NoteList
            Else
                Return Nothing
            End If
        End Function

        Public Shared Function GetRelatedPermitInfo(ByVal permitid As Int32, ByVal tran As SqlClient.SqlTransaction) As BO.Application.BOPermitInfo()

            Dim DOPermit As New [DO].DataObjects.Entity.Permit(permitid, tran)
            Dim DOPermitInfoSet As [DO].DataObjects.EntitySet.PermitInfoSet = DOPermit.GetRelatedPermitInfo(tran)
            Dim ReturnArray As New ArrayList
            If Not DOPermitInfoSet Is Nothing Then
                For Each DOPermitInfo As [DO].DataObjects.Entity.PermitInfo In DOPermitInfoSet.Entities
                    ReturnArray.Add(New BO.Application.BOPermitInfo(DOPermitInfo.PermitInfoId))
                Next
            End If
            Return CType(ReturnArray.ToArray(GetType(BO.Application.BOPermitInfo)), BO.Application.BOPermitInfo())
        End Function

        Public Function GetRelatedPermitInfo(ByVal tran As SqlClient.SqlTransaction) As BO.Application.BOPermitInfo()
            Return GetRelatedPermitInfo(PermitId, tran)
        End Function

        Public Shared Function DeleteSpecimenFromPermit(ByVal SpecimenId As Int32, ByVal permitId As Int32) As Boolean
            Dim SpecimenService As New DataObjects.Service.SpecimenService
            Dim SpecimenMarkService As New DataObjects.Service.SpecimenIDMarkService
            Dim Success As Boolean = True
            Dim tran As SqlClient.SqlTransaction = SpecimenMarkService.BeginTransaction
            Dim DOSpecimen As New Entity.Specimen(SpecimenId, tran)
            Dim IdMarks As [DO].DataObjects.EntitySet.SpecimenIDMarkSet = DOSpecimen.GetRelatedSpecimenIDMark(tran)

            If Not IdMarks Is Nothing Then
                For Each Mark As Entity.SpecimenIDMark In IdMarks
                    If Not Mark.DeleteById(Mark.SpecimenMarkId, Mark.CheckSum, tran) Then
                        SpecimenMarkService.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                        Success = False
                    End If
                Next
            End If

            If Success Then
                Dim PermitSpecimenService As New DataObjects.Service.PermitSpecimenService
                If Not PermitSpecimenService.DeleteById(New Int32() {permitId, SpecimenId}, 0, tran) Then
                    PermitSpecimenService.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    Success = False

                End If
            End If

            If Not DOSpecimen.DeleteById(DOSpecimen.SpecimenId, DOSpecimen.CheckSum, tran) Then
                SpecimenMarkService.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                Return False
            Else
                tran.Commit()
                tran.Dispose()
                tran = Nothing
                Return True
            End If
        End Function

        Public Overridable Function DeletePermitById(ByVal permit As BOPermit, ByVal tran As SqlClient.SqlTransaction) As Boolean
            Dim PermitService As New DataObjects.Service.PermitService
            If tran Is Nothing Then
                tran = PermitService.BeginTransaction
            End If
            Dim PIs As BO.Application.BOPermitInfo() = Me.GetPermitInfos(tran)
            If Not PIs Is Nothing Then
                Dim PIService As New [DO].DataObjects.Service.PermitInfoService
                For Each pi As BO.Application.BOPermitInfo In PIs
                    If Not PIService.DeleteById(pi.PermitInfoId, Nothing, tran) Then
                        PermitService.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                        Return False
                    End If
                Next
            End If
            If Not Me.Specimens Is Nothing Then
                Dim PermitSpecimenService As New [DO].DataObjects.Service.PermitSpecimenService
                For Each spec As BO.Application.BOSpecimen In Me.Specimens
                    If Not PermitSpecimenService.DeleteById(New Int32() {PermitId, spec.SpecimenId}, Nothing, tran) Then
                        PermitSpecimenService.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                        Return False
                    End If
                Next
            End If

            Return PermitService.DeleteById(permit.PermitId, 0, tran)
        End Function


        Private Shared Function LoadByPermitId(ByVal permitId As Int32) As BOPermit
            Return New BOPermit(permitId)
        End Function

        Protected Overridable Function GetPermit(ByVal permitId As Int32) As BOPermit
            Return LoadByPermitId(permitId)
        End Function

#Region " Get Related Permits "
        Public Function GetRelatedPermits() As BOPermit()
            Return GetRelatedPermits(Nothing)
        End Function

        Public Function GetRelatedPermits(ByVal tran As SqlClient.SqlTransaction) As BOPermit()
            Return GetRelatedPermits(Me.PermitId, tran)
        End Function

        Public Function GetRelatedPermits(ByVal permitId As Int32, ByVal tran As SqlClient.SqlTransaction) As BOPermit()
            Dim DORelatedPermits As New DataObjects.Entity.RelatedPermits
            Dim RelatedSet As DataObjects.EntitySet.RelatedPermitsSet = DORelatedPermits.GetForPermitIdPermit(permitId, tran)
            Dim Permits As BOPermit()
            If Not RelatedSet Is Nothing AndAlso _
               RelatedSet.Count > 0 Then
                Dim Index As Int32 = 0
                For Each RP As DataObjects.Entity.RelatedPermits In RelatedSet
                    ReDim Preserve Permits(Index)
                    Permits(Index) = GetPermit(RP.RelatedPermitId)
                    Index += 1
                Next RP
            End If
            Return Permits
        End Function
#End Region

#Region " Save Related Permits "
        Public Sub SaveRelatedPermits(ByVal relatedPermitId As Int32)
            SaveRelatedPermits(relatedPermitId, Nothing)
        End Sub

        Public Sub SaveRelatedPermits(ByVal relatedPermitId() As Int32)
            SaveRelatedPermits(relatedPermitId, Nothing)
        End Sub

        Public Sub SaveRelatedPermits(ByVal relatedPermitId As Int32, ByVal tran As SqlClient.SqlTransaction)
            SaveRelatedPermits(New Int32() {relatedPermitId}, tran)
        End Sub

        Public Sub SaveRelatedPermits(ByVal relatedPermitId() As Int32, ByVal tran As SqlClient.SqlTransaction)
            SaveRelatedPermits(Me.PermitId, relatedPermitId, tran)
        End Sub

        Public Sub SaveRelatedPermits(ByVal permitId As Int32, ByVal relatedPermitId() As Int32, ByVal tran As SqlClient.SqlTransaction)
            Dim DORelatedPermits As New DataObjects.Entity.RelatedPermits
            For Each relatedId As Int32 In relatedPermitId
                If relatedId <> permitId Then
                    DORelatedPermits = DORelatedPermits.ServiceObject.Update(permitId, relatedId, 0, tran)
                    If DORelatedPermits Is Nothing Then
                        DORelatedPermits.ServiceObject.Insert(permitId, relatedId, tran)
                    End If
                End If
            Next relatedId
        End Sub
#End Region

#Region " Delete Related Permits "
        Public Sub DeleteRelatedPermits(ByVal relatedPermitId As Int32)
            DeleteRelatedPermits(relatedPermitId, Nothing)
        End Sub

        Public Sub DeleteRelatedPermits(ByVal relatedPermitId() As Int32)
            DeleteRelatedPermits(relatedPermitId, Nothing)
        End Sub

        Public Sub DeleteRelatedPermits(ByVal relatedPermitId As Int32, ByVal tran As SqlClient.SqlTransaction)
            DeleteRelatedPermits(New Int32() {relatedPermitId}, tran)
        End Sub

        Public Sub DeleteRelatedPermits(ByVal relatedPermitId() As Int32, ByVal tran As SqlClient.SqlTransaction)
            DeleteRelatedPermits(Me.PermitId, relatedPermitId, tran)
        End Sub

        Public Sub DeleteRelatedPermits(ByVal permitId As Int32, ByVal relatedPermitId() As Int32, ByVal tran As SqlClient.SqlTransaction)
            Dim DORelatedPermits As New DataObjects.Entity.RelatedPermits
            For Each relatedId As Int32 In relatedPermitId
                If relatedId <> permitId Then
                    DORelatedPermits.DeleteById(permitId, relatedId, 0, tran)
                End If
            Next relatedId
        End Sub
#End Region

        '        Public Shadows Function Clone() As BOPermit Implements IBOPermit.Clone
        '            Dim ClonePermit As New BOPermit

        '            With ClonePermit
        '                .mApplicationId = .mApplicationId
        '                .mCountryOfOrigin = .mCountryOfOrigin
        '                .mCountryOfOriginPermitDate = .mCountryOfOriginPermitDate
        '                .mCountryOfOriginPermitNumber = .mCountryOfOriginPermitNumber
        '                .mDescription = .mDescription
        '                .mExpiryDate = .mExpiryDate
        '                .mIssueDate = .mIssueDate
        '                .mNumberOfCopies = .mNumberOfCopies
        '                .mPermitDate = .mPermitDate
        '                '.mPermitId
        '                .mSpecie = .mSpecie
        '                .mSpecimens = .mSpecimens
        '            End With
        '            Return ClonePermit
        '        End Function

        'Friend Function GetScientificAdvice() As Object

        'End Function

        'Public Class SciAdvice
        '    Private mPermitId As Int32
        '    Public Sub New()
        '    End Sub

        '    Public Sub New(ByVal permitId As Int32)
        '        mPermitId = permitId
        '    End Sub

        '    Public Property LeftList() As LeftSciAdvice()
        '        Get
        '            Return mLeftList
        '        End Get
        '        Set(ByVal Value As LeftSciAdvice())
        '            mLeftList = Value
        '        End Set
        '    End Property
        '    Private mLeftList As LeftSciAdvice()

        'Public Sub LoadAdvice()
        '    Dim AdviceList As DataObjects.Collection.ScientificAdviceBoundCollection = ReferenceData.BOScientificAdvice.GetAll(False)
        '    If Not AdviceList Is Nothing AndAlso AdviceList.Count > 0 Then
        '        Dim AdviceSet As DataObjects.EntitySet.ScientificAdviceIdLinkSet = DataObjects.Entity.ScientificAdviceIdLink.GetForPermit(mPermitId)
        '        ' how many ad-hoc are there?
        '        Dim Count As Int32 = AdviceList.Count
        '        For Each SciAd As DataObjects.Entity.ScientificAdviceIdLink In AdviceSet
        '            If SciAd.IsScientificAdviceIdNull OrElse SciAd.ScientificAdviceId = 0 Then
        '                Count += 1
        '            End If
        '        Next sciad
        '        ReDim mLeftList(AdviceList.Count - 1)

        '        Dim Index As Int32 = 0
        '        'add the standard set
        '        For Each SciAd As DataObjects.Entity.ScientificAdviceIdLink In AdviceList
        '            Index += 1
        '            'check to see if it's adhoc
        '            Dim Description As String
        '            Dim AdviceId As Int32
        '            If Not SciAd.IsScientificAdviceIdNull AndAlso SciAd.ScientificAdviceId > 0 Then
        '                AdviceId = SciAd.ScientificAdviceId
        '                Dim NonAdHoc As New ReferenceData.BOScientificAdvice(AdviceId)
        '                Description = NonAdHoc.Description
        '            Else
        '                Description = SciAd.Adhoc
        '                AdviceId = 0
        '            End If
        '            mLeftList(Index - 1) = New LeftSciAdvice(Index, Description, AdviceId)
        '        Next SciAd

        '        'add the ad hoc list
        '        For Each SciAd As DataObjects.Entity.ScientificAdviceIdLink In AdviceSet
        '            If SciAd.IsScientificAdviceIdNull OrElse SciAd.ScientificAdviceId = 0 Then
        '                Count += 1
        '            End If
        '        Next sciad
        '    End If
        'End Sub

        '    Public Class LeftSciAdvice
        '        Public Sub New()
        '        End Sub

        '        Public Sub New(ByVal key As Int32, ByVal adviceDescription As String, ByVal adviceId As Int32)
        '            mKey = key
        '            mAdviceDescription = adviceDescription
        '            mAdviceId = adviceId
        '        End Sub

        '        Public Property AdviceDescription() As String
        '            Get
        '                Return mAdviceDescription
        '            End Get
        '            Set(ByVal Value As String)
        '                mAdviceDescription = Value
        '            End Set
        '        End Property
        '        Private mAdviceDescription As String

        '        Public Property AdviceId() As Int32
        '            Get
        '                Return mAdviceId
        '            End Get
        '            Set(ByVal Value As Int32)
        '                mAdviceId = Value
        '            End Set
        '        End Property
        '        Private mAdviceId As Int32

        '        Public Property Key() As Int32
        '            Get
        '                Return mKey
        '            End Get
        '            Set(ByVal Value As Int32)
        '                mKey = Value
        '            End Set
        '        End Property
        '        Private mKey As Int32
        '    End Class

        '    Public Class RightSciAdvice

        '    End Class

        '    'propv Permits
        'End Class
#End Region

        'Create an instance of the correct subclass given a "base" permit id.
        'Note that at the moment only BOCITESImportExportPermit, BOCITESArticle10Permit and
        'BOCITESArticle30Permit are valid and there is no way to distinguish 10s and 30s from
        'Import/Exports. Consequently, this method only returns BOCITESImportExportPermit
        'instances at present
        Public Shared Function PolymorphicCreate(ByVal permitId As Int32) As BOPermit 'PJN 1/12/04
            Return PolymorphicCreate(permitId, Nothing)
        End Function

        Public Shared Function PolymorphicCreate(ByVal permitId As Int32, ByVal tran As SqlClient.SqlTransaction) As BOPermit 'MLD 29/11/4
            Dim basePermit As New Entity.Permit(permitId, tran)
            Dim citesPermits As EntitySet.CITESPermitSet = basePermit.GetRelatedCITESPermit(tran)
            Dim importExportPermits As EntitySet.CITESImportExportPermitSet
            Dim importExportPermitId As Int32

            If citesPermits Is Nothing OrElse citesPermits.Count <> 1 Then  'should never happen
                Throw New Exception("Cannot find Permit, id=" + permitId.ToString())
            End If
            importExportPermits = citesPermits.Entities(0).GetRelatedCITESImportExportPermit(tran)

            If importExportPermits Is Nothing OrElse importExportPermits.Count <> 1 Then  'should never happen
                Throw New Exception("Cannot find Import/Export Permit, permit id=" + permitId.ToString())
            End If

            importExportPermitId = importExportPermits.Entities(0).CITESImportExportPermitId

            Dim DOApplication As New Entity.Application(basePermit.ApplicationId, tran)
            If CType(DOApplication.ApplicationTypeId, Application.ApplicationTypes) = Application.ApplicationTypes.Article10 Then
                Return New CITES.Applications.BOCITESArticle10Permit(importExportPermitId, tran)
            Else
                Return New CITES.Applications.BOCITESImportExportPermit(importExportPermitId, tran)
            End If
        End Function

        Public Overloads Shared Function GetPermitInfos(ByVal tran As SqlClient.SqlTransaction, ByVal PermitId As Int32) As BO.Application.BOPermitInfo()
            Dim DOPermit As New [DO].DataObjects.Entity.Permit(PermitId, tran)
            Dim permitInfos As [DO].DataObjects.EntitySet.PermitInfoSet = DOPermit.GetRelatedPermitInfo(tran)
            Dim results(-1) As BO.Application.BOPermitInfo
            If Not permitInfos Is Nothing Then
                Dim i As Int32 = 0
                ReDim results(permitInfos.Entities.Count - 1)
                For Each info As [DO].DataObjects.Entity.PermitInfo In permitInfos
                    results(i) = New BO.Application.BOPermitInfo(info, tran)
                    i += 1
                Next
            End If
            Return results
        End Function

        Public Overloads Function GetPermitInfos(ByVal tran As SqlClient.SqlTransaction) As BO.Application.BOPermitInfo()   'MLD 27/1/5 modified to never return Nothing, as nearly all the calling code assumes this
            Return GetPermitInfos(tran, PermitId)
        End Function

        Public Overloads Overrides Function Clone() As Object
            Dim ClonedObject As Object = MyBase.Clone
            With CType(ClonedObject, BOPermit)
                .NumberOfCopies = 1
                .PermitId = 0
            End With
            Return ClonedObject
        End Function

        Public Shared Function GetCurrentStatusInfo(ByVal permitId As Int32) As StatusInfoBase
            Dim HistorySet As DataObjects.EntitySet.PermitHistorySet = DataObjects.Sprocs.dbo_usp_GetLatestStatus__PermitHistory__(permitId, Nothing, Nothing)
            If HistorySet Is Nothing OrElse HistorySet.Count = 0 Then
                Return Nothing
            Else
                Dim info As New StatusInfoBase
                Dim RefCount As Int32 = 0
                For Each HistoryItem As DataObjects.Entity.PermitHistory In HistorySet
                    With info
                        If Not HistoryItem.IsPermitStatusIdNull Then
                            If .StatusId = 0 Then
                                Dim PermitStatus As New ReferenceData.BOPermitStatus(HistoryItem.PermitStatusId)
                                If Not PermitStatus Is Nothing Then
                                    .Status = PermitStatus.Description
                                    .StatusId = PermitStatus.ID
                                End If
                            ElseIf .StatusId <> -1 AndAlso .StatusId <> HistoryItem.PermitStatusId Then
                                'multiple permit info's that are different
                                .Status = "*"
                                .StatusId = -1
                            End If
                        End If
                        If Not HistoryItem.IsAssignedToNull Then
                            If .AssignedToId = 0 Then
                                Dim AssignedTo As New ReferenceData.BOStatusAssignedToGroup(HistoryItem.AssignedTo)
                                If Not AssignedTo Is Nothing Then
                                    .AssignedTo = AssignedTo.Description
                                    .AssignedToId = AssignedTo.ID
                                End If
                            ElseIf .AssignedToId <> -1 AndAlso .AssignedToId <> HistoryItem.AssignedTo Then
                                'multiple permit info's that are different
                                .AssignedTo = "*"
                                .AssignedToId = -1
                            End If
                        End If
                        Dim AssignedDate As String = HistoryItem.ChangeDate.ToShortDateString
                        If .AssignedDate Is Nothing Then
                            .AssignedDate = AssignedDate
                        ElseIf .AssignedDate <> "*" AndAlso String.Compare(AssignedDate, .AssignedDate) <> 0 Then
                            'multiple permit info's that are different
                            .AssignedDate = "*"
                        End If
                    End With
                    If info.AssignedToId > 0 AndAlso _
                       (info.AssignedToId = Application.BOPermitInfo.PermitStatusTypes.Referred OrElse _
                        info.AssignedToId = Application.BOPermitInfo.PermitStatusTypes.ReferredForCAndA OrElse _
                        info.AssignedToId = Application.BOPermitInfo.PermitStatusTypes.ReferredForSpecimenReport) Then
                        RefCount += 1
                    End If
                Next HistoryItem
                If RefCount > 0 Then
                    info = New StatusInfoRefer(info, RefCount)
                End If
                Return info
            End If
        End Function

        <Serializable()> _
        Public Class StatusInfoBase
            Public Sub New()
                MyBase.new()
            End Sub

            Public Sub New(ByVal status As String, ByVal assignedTo As String, ByVal assignedDate As String)
                mStatus = status
                mAssignedTo = assignedTo
                mAssignedDate = mAssignedDate
            End Sub

            Public Property Status() As String
                Get
                    Return mStatus
                End Get
                Set(ByVal Value As String)
                    mStatus = Value
                End Set
            End Property
            Private mStatus As String

            Friend Property StatusId() As Int32
                Get
                    Return mStatusId
                End Get
                Set(ByVal Value As Int32)
                    mStatusId = Value
                End Set
            End Property
            Private mStatusId As Int32

            Public Property AssignedTo() As String
                Get
                    Return mAssignedTo
                End Get
                Set(ByVal Value As String)
                    mAssignedTo = Value
                End Set
            End Property
            Private mAssignedTo As String

            Friend Property AssignedToId() As Int32
                Get
                    Return mAssignedToId
                End Get

                Set(ByVal Value As Int32)
                    mAssignedToId = Value
                End Set
            End Property

            Private mAssignedToId As Int32

            Public Property AssignedDate() As String
                Get
                    Return mAssignedDate
                End Get
                Set(ByVal Value As String)
                    mAssignedDate = Value
                End Set
            End Property
            Private mAssignedDate As String
        End Class

        <Serializable()> _
        Public Class StatusInfoRefer
            Inherits StatusInfoBase

            Public Sub New()
                MyBase.new()
            End Sub

            Public Sub New(ByVal statusInfo As StatusInfoBase)
                MyBase.New(statusInfo.Status, statusInfo.AssignedTo, statusInfo.AssignedDate)
            End Sub

            Public Sub New(ByVal statusInfo As StatusInfoBase, ByVal referralCount As Int32)
                MyClass.New(statusInfo)
                mReferralCount = referralCount
            End Sub

            Public Property ReferralCount() As Int32
                Get
                    Return mReferralCount
                End Get
                Set(ByVal Value As Int32)
                    mReferralCount = Value
                End Set
            End Property
            Private mReferralCount As Int32
        End Class

        <Serializable()> _
          Public Class ProgressPermitGrid

            Public Property SAAdvice() As String
                Get
                    Return mSAAdvice
                End Get
                Set(ByVal Value As String)
                    mSAAdvice = Value
                End Set
            End Property
            Private mSAAdvice As String

            Public Property PermitInfoId() As Int32
                Get
                    Return mPermitInfoId
                End Get
                Set(ByVal Value As Int32)
                    mPermitInfoId = Value
                End Set
            End Property
            Private mPermitInfoId As Int32

            Public Property PermitId() As Int32
                Get
                    Return mPermitId
                End Get
                Set(ByVal Value As Int32)
                    mPermitId = Value
                End Set
            End Property
            Private mPermitId As Int32

            Public Property AppIsSemiComplete() As Boolean
                Get
                    Return mAppIsSemiComplete
                End Get
                Set(ByVal Value As Boolean)
                    mAppIsSemiComplete = Value
                End Set
            End Property
            Private mAppIsSemiComplete As Boolean

            Public Property CustomerStatus() As String
                Get
                    Return mCustomerStatus
                End Get
                Set(ByVal Value As String)
                    mCustomerStatus = Value
                End Set
            End Property
            Private mCustomerStatus As String

            Public Property Status() As String
                Get
                    Return mStatus
                End Get
                Set(ByVal Value As String)
                    mStatus = Value
                End Set
            End Property
            Private mStatus As String

            Public Property TranOrSpecType() As String
                Get
                    Return mTranOrSpecType
                End Get
                Set(ByVal Value As String)
                    mTranOrSpecType = Value
                End Set
            End Property
            Private mTranOrSpecType As String

            Public Property ApplicationId() As Int32
                Get
                    Return mApplicationId
                End Get
                Set(ByVal Value As Int32)
                    mApplicationId = Value
                End Set
            End Property
            Private mApplicationId As Int32

            Public Property PermitType() As Application.ApplicationTypes
                Get
                    Return mPermitType
                End Get
                Set(ByVal Value As Application.ApplicationTypes)
                    mPermitType = Value
                End Set
            End Property
            Private mPermitType As Application.ApplicationTypes

            Public Property StatusId() As Int32
                Get
                    Return mStatusId
                End Get
                Set(ByVal Value As Int32)
                    mStatusId = Value
                End Set
            End Property
            Private mStatusId As Int32

            Public Property ApplicationNumber() As String
                Get
                    Return mApplicationNumber
                End Get
                Set(ByVal Value As String)
                    mApplicationNumber = Value
                End Set
            End Property
            Private mApplicationNumber As String

            Public Property Description() As String
                Get
                    Return mDescription
                End Get
                Set(ByVal Value As String)
                    mDescription = Value
                End Set
            End Property
            Private mDescription As String

            Public Property PurposeCode() As String
                Get
                    Return mPurposeCode
                End Get
                Set(ByVal Value As String)
                    mPurposeCode = Value
                End Set
            End Property
            Private mPurposeCode As String

            Public Property CountryOfOrigin() As String
                Get
                    Return mCountryOfOrigin
                End Get
                Set(ByVal Value As String)
                    mCountryOfOrigin = Value
                End Set
            End Property
            Private mCountryOfOrigin As String

            Public Property Derivative() As String
                Get
                    Return mDerivative
                End Get
                Set(ByVal Value As String)
                    mDerivative = Value
                End Set
            End Property
            Private mDerivative As String

            Public Property SourceCode() As String
                Get
                    Return mSourceCode
                End Get
                Set(ByVal Value As String)
                    mSourceCode = Value
                End Set
            End Property
            Private mSourceCode As String

            Public Property ScientificName() As String
                Get
                    Return mScientificName
                End Get
                Set(ByVal Value As String)
                    mScientificName = Value
                End Set
            End Property
            Private mScientificName As String

            Public Property ExpiryDate() As String
                Get
                    Return mExpiryDate
                End Get
                Set(ByVal Value As String)
                    mExpiryDate = Value
                End Set
            End Property
            Private mExpiryDate As String

            Public Property Quantity() As Int32
                Get
                    Return mQuantity
                End Get
                Set(ByVal Value As Int32)
                    mQuantity = Value
                End Set
            End Property
            Private mQuantity As Int32

            Public Property NetMass() As Decimal
                Get
                    Return mNetMass
                End Get
                Set(ByVal Value As Decimal)
                    mNetMass = Value
                End Set
            End Property
            Private mNetMass As Decimal



            Public Property CITESAppendix() As String
                Get
                    Return mCITESAppendix
                End Get
                Set(ByVal Value As String)
                    mCITESAppendix = Value
                End Set
            End Property
            Private mCITESAppendix As String

            Public Overridable Property ECAnnex() As String
                Get
                    Return mECAnnex
                End Get
                Set(ByVal Value As String)
                    mECAnnex = Value
                End Set
            End Property
            Protected mECAnnex As String

            Public Property ReferredForCAndA() As Boolean
                Get
                    Return mReferredForCAndA
                End Get
                Set(ByVal Value As Boolean)
                    mReferredForCAndA = Value
                End Set
            End Property
            Private mReferredForCAndA As Boolean

            Public Property AssignedTo() As ReferenceData.BOStatusAssignedToGroup
                Get
                    Return mAssignedTo
                End Get
                Set(ByVal Value As ReferenceData.BOStatusAssignedToGroup)
                    mAssignedTo = Value
                End Set
            End Property
            Private mAssignedTo As ReferenceData.BOStatusAssignedToGroup

            Public Property CAndASubmitted() As Boolean
                Get
                    Dim DOCandAService As New [DO].DataObjects.Service.AccommodationAndCareService
                    If Not mSpecieId Is Nothing Then
                        Dim DOCandA As [DO].DataObjects.EntitySet.AccommodationAndCareSet = DOCandAService.GetByIndex_IX_AccommodationAndCare(CType(mSpecieId, Int32), PermitId)
                        Return (Not DOCandA Is Nothing AndAlso DOCandA.Entities.Count > 0)
                    End If
                End Get
                Set(ByVal Value As Boolean)

                End Set
            End Property
            Private mSpecieId As Object

            Public Sub New()

            End Sub

            Public Sub New(ByVal app As BO.Application.cites.Applications.BOCITESApplication, ByVal showMultipleNumber As Boolean, ByVal permitinfo As BO.Application.BOPermitInfo, ByVal permit As BO.Application.cites.BOCITESPermit)
                CustomerStatus = permitinfo.GridColumn_Status_Customer
                Status = permitinfo.GridColumn_Status
                Me.StatusId = permitinfo.PermitStatus.ID
                Me.AssignedTo = permitinfo.AssignedTo
                Me.PermitInfoId = permitinfo.PermitInfoId
                If Not app Is Nothing Then mAppIsSemiComplete = app.IsSemiComplete
                With permit
                    If Not .SpecieId Is Nothing Then mSpecieId = .SpecieId
                    Me.ApplicationNumber = .ApplicationPermitNumber
                    If showMultipleNumber AndAlso CType(.NumberOfCopies, Int32) > 1 Then
                        Me.ApplicationNumber = Me.ApplicationNumber & "/" & permitinfo.SequenceNumber.ToString.PadLeft(3, CType("0", Char))

                    Else
                        mReferredForCAndA = (StatusId = BOPermitInfo.PermitStatusTypes.ReferredForCAndA)

                        Try
                            If .NumberOfCopies Is Nothing OrElse CType(.NumberOfCopies, Int32) <= 1 Then
                                Me.ApplicationNumber = Me.ApplicationNumber
                            Else
                                Me.ApplicationNumber = Me.ApplicationNumber & "(" & CType(.NumberOfCopies, Int32) & ")"
                            End If
                        Catch ex As Exception
                            Me.ApplicationNumber = Me.ApplicationNumber
                        End Try
                    End If
                    Me.CITESAppendix = .Specie.CITESAppendix
                    If Not .Description Is Nothing Then Me.Description = .Description.toString
                    If Not .Specie Is Nothing Then Me.ECAnnex = .Specie.ECAnnex
                    Me.PermitId = permit.PermitId
                    If Not .Purpose Is Nothing Then Me.PurposeCode = .Purpose.Code
                    Me.Quantity = .Quantity
                    Me.NetMass = .NetMass
                    If Not .Derivative Is Nothing Then Me.Derivative = .Derivative.Code
                    If Not .CountryOfOrigin Is Nothing Then Me.CountryOfOrigin = .CountryOfOrigin.CodeDescription
                    Me.ScientificName = .Specie.ScientificName
                    If Not .Source1 Is Nothing Then Me.SourceCode = .Source1.Code
                    Me.ApplicationId = .ApplicationId
                    If Not .ExpiryDate Is Nothing Then ExpiryDate = .ExpiryDate_String
                    If Not app Is Nothing Then
                        If app.IsArticle10 Then
                            PermitType = Application.ApplicationTypes.Article10
                        ElseIf app.IsExport Then
                            PermitType = Application.ApplicationTypes.Export
                        ElseIf app.IsImport Then
                            PermitType = Application.ApplicationTypes.Import
                        End If
                    End If

                    If TypeOf permit Is CITES.Applications.BOCITESArticle10Permit Then
                        TranOrSpecType = CType(permit, CITES.Applications.BOCITESArticle10Permit).TranOrSpecType
                    End If
                End With
            End Sub
        End Class
    End Class

End Namespace
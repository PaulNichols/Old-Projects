Imports uk.gov.defra.Phoenix.DO.DataObjects

Namespace Application
    Public Class BOSpecimen
        Inherits BaseBO
        Implements IBOSpecimen

#Region " Prelim code "
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal specimenId As Int32, ByVal tran As SqlClient.SqlTransaction)
            MyClass.New()
            LoadSpecimen(specimenId, tran)
        End Sub

        Protected Overridable Function LoadSpecimen(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.Specimen
            Dim NewSpecimen As DataObjects.Entity.Specimen = DataObjects.Entity.Specimen.GetById(id, tran)
            If NewSpecimen Is Nothing Then
                Throw New RecordDoesNotExist("Specimen", id)
            Else
                InitialiseSpecimen(NewSpecimen, tran)
                Return NewSpecimen
            End If
        End Function

        Public Overridable Sub InitialiseSpecimen(ByVal specimen As DataObjects.Entity.Specimen, ByVal tran As SqlClient.SqlTransaction)
            With specimen
                mSpecimenId = .Id
                mSpecimenCheckSum = .CheckSum
                mGender = GenderType.Unknown
                mFateDate = .FateDate               'MLD 27/1/5 added
                mExactDOB = .ExactDOB               'MLD 27/1/5 added
                mDOB = .DOB                         'MLD 31/1/5 refactored
                mAcquisitionDate = .AcquisitionDate 'MLD 31/1/5 refactored
                mDescription = .Description         'MLD 31/1/5 refactored
                If Not .IsFateIdNull Then mFate = New ReferenceData.BOSpecimenFate(.FateId, tran)
                If Not .IsFatherSpecimenNull Then mFatherSpecimen = New BOSpecimen(.FatherSpecimen, tran)
                If Not .IsMotherSpecimenNull Then mMotherSpecimen = New BOSpecimen(.MotherSpecimen, tran)
                If Not .IsGenderIdNull Then mGender = CType(.GenderId, GenderType)
                SetSpecimenMarks(Me.mSpecimenId, tran)
            End With
        End Sub

        Private Sub SetSpecimenMarks(ByVal specId As Int32, ByVal tran As SqlClient.SqlTransaction) 'MLD 31/1/5 now never Nothing
            'populate the specimen marks
            Dim marks As DataObjects.EntitySet.SpecimenIDMarkSet = DataObjects.Entity.SpecimenIDMark.GetForSpecimen(specId, tran)
            If Not marks Is Nothing Then
                ReDim mSpecimenMarks(marks.Count - 1)
                Dim index As Int32 = 0
                For Each mark As DataObjects.Entity.SpecimenIDMark In marks
                    Dim BOMark As New BOSpecimenMark
                    BOMark.InitialiseSpecimenMark(mark, tran)
                    mSpecimenMarks(index) = BOMark
                    index += 1
                Next mark
            End If
        End Sub
#End Region

#Region " Properties "
        Public Property DOB As Date Implements IBOSpecimen.DOB    'MLD 31/1/5 refactored
            Get
                Return mDOB
            End Get
            Set
                mDOB = Value
            End Set
        End Property
        Private mDOB As Date

        Public Property Fate As ReferenceData.BOSpecimenFate Implements IBOSpecimen.Fate
            Get
                Return mFate
            End Get
            Set
                mFate = Value
            End Set
        End Property
        Private mFate As ReferenceData.BOSpecimenFate

        Public Property FatherSpecimen As BOSpecimen Implements IBOSpecimen.FatherSpecimen
            Get
                Return mFatherSpecimen
            End Get
            Set
                mFatherSpecimen = Value
            End Set
        End Property
        Private mFatherSpecimen As BOSpecimen

        Public Property UOM As BOMeasurement Implements IBOSpecimen.Measurement
            Get
                Return mUOM
            End Get
            Set
                mUOM = Value
            End Set
        End Property
        Private mUOM As BOMeasurement

        Public Property Gender As GenderType Implements IBOSpecimen.Gender
            Get
                Return mGender
            End Get
            Set
                mGender = Value
            End Set
        End Property
        Private mGender As GenderType

        Public Property MotherSpecimen As BOSpecimen Implements IBOSpecimen.MotherSpecimen
            Get
                Return mMotherSpecimen
            End Get
            Set
                mMotherSpecimen = Value
            End Set
        End Property
        Private mMotherSpecimen As BOSpecimen

        Public Property SpecimenId As Integer Implements IBOSpecimen.SpecimenId
            Get
                Return mSpecimenId
            End Get
            Set
                mSpecimenId = Value
            End Set
        End Property
        Private mSpecimenId As Int32

        Public Property SpecimenCheckSum As Integer Implements IBOSpecimen.SpecimenCheckSum
            Get
                Return mSpecimenCheckSum
            End Get
            Set
                mSpecimenCheckSum = Value
            End Set
        End Property
        Private mSpecimenCheckSum As Int32

        Public Property AcquisitionDate As Date Implements IBOSpecimen.AcquisitionDate  'MLD 31/1/5 refactored
            Get
                Return mAcquisitionDate
            End Get
            Set
                mAcquisitionDate = Value
            End Set
        End Property
        Private mAcquisitionDate As Date

        Public Property Description As String Implements IBOSpecimen.Description
            Get
                Return mDescription
            End Get
            Set
                mDescription = Value
            End Set
        End Property
        Private mDescription As String

        Public Property ExactDOB As Boolean Implements IBOSpecimen.ExactDOB
            Get
                Return mExactDOB
            End Get
            Set
                mExactDOB = Value
            End Set
        End Property
        Private mExactDOB As Boolean

        Public Property SpecimenMarks As BOSpecimenMark() Implements IBOSpecimen.SpecimenMarks
            Get
                Return mSpecimenMarks
            End Get
            Set
                mSpecimenMarks = Value
            End Set
        End Property
        Private mSpecimenMarks(-1) As BOSpecimenMark        'MLD 31/1/5 now never Nothing

        Public Property FateDate As Date Implements IBOSpecimen.FateDate   'MLD 27/1/5 added
            Get
                Return mFateDate
            End Get
            Set
                mFateDate = Value
            End Set
        End Property
        Private mFateDate As Date
#End Region

#Region " Helper Functions "

        Private ReadOnly Property FateId() As Object
            Get
                If mFate Is Nothing OrElse mFate.ID = 0 Then
                    Return Nothing
                Else
                    Return mFate.ID
                End If
            End Get
        End Property


        Private ReadOnly Property FatherSpecimenId() As Object
            Get
                If mFatherSpecimen Is Nothing OrElse mFatherSpecimen.SpecimenId = 0 Then
                    Return Nothing
                Else
                    Return mFatherSpecimen.SpecimenId
                End If
            End Get
        End Property

        Private ReadOnly Property MotherSpecimenId() As Object
            Get
                If mMotherSpecimen Is Nothing OrElse mMotherSpecimen.SpecimenId = 0 Then
                    Return Nothing
                Else
                    Return mMotherSpecimen.SpecimenId
                End If
            End Get
        End Property

        Public Property ReportDOB() As String
            Get
                If DOB = Nothing Then
                    Return String.Empty
                Else
                    Return CType(DOB, Date).ToString("dd/MM/yyyy")
                End If
            End Get
            Set(ByVal Value As String)

            End Set
        End Property

        Friend ReadOnly Property ReportDescription() As String
            Get
                Return String.Concat(ReportMark, " ", Gender.ToString(), " ", ReportDOB).Trim
            End Get
        End Property

        Public Property FirstIDMark() As String
            Get
                If Not SpecimenMarks Is Nothing AndAlso _
                 SpecimenMarks.Length > 0 Then
                    With SpecimenMarks(0)
                        Return String.Concat(.IdMarkType.Description.TrimEnd, " ", .IdMark.ToString)
                    End With
                End If
            End Get
            Set(ByVal Value As String)

            End Set
        End Property

        Public ReadOnly Property HasSpecimenReport(ByVal permitId As Int32) As Boolean
            Get
                Dim SpecReportService As DataObjects.Service.SpecimenReportService
                Dim SpecReportSet As [DO].DataObjects.EntitySet.SpecimenReportSet = SpecReportService.GetByIndex_IX_SpecimenReport(Me.SpecimenId, permitId) 'MLD 23/11/4 params reversed
                Return (Not SpecReportSet Is Nothing AndAlso SpecReportSet.Entities.Count > 0)
            End Get
        End Property

        Public Property NeedSpecimenReport() As Boolean
            Get
                Return mNeedSpecimenReport
            End Get
            Set(ByVal Value As Boolean)
                mNeedSpecimenReport = Value
            End Set
        End Property
        Private mNeedSpecimenReport As Boolean

        Public Property ReportMark() As String
            Get
                Try
                    If Not SpecimenMarks Is Nothing AndAlso _
                       SpecimenMarks.Length > 0 Then
                        Dim Mark As String = String.Empty
                        For Each specMark As BOSpecimenMark In SpecimenMarks
                            If Mark.Length > 0 Then
                                Mark &= ";"
                            End If
                            Mark &= String.Concat(specMark.IdMarkType.Description.TrimEnd, " ", specMark.IdMark.ToString)
                        Next specMark
                        Return Mark
                    Else
                        Return String.Empty
                    End If
                Catch
                End Try
            End Get
            Set(ByVal Value As String)

            End Set
        End Property
#End Region

#Region " Save "
        Public Overloads Overrides Function Save() As BaseBO
            'PN Commented out for now due to the change to Save
            Throw New NotImplementedException
            '   Return Save(tran:=Nothing)

        End Function

        'Public Overridable Overloads Function Save(ByVal permitId As Int32) As BaseBO
        '    Return MyClass.Save(permitId, TypeOfSave.PermitId, Nothing)
        'End Function

        'Public Overridable Overloads Function Save(ByVal permitId As Int32, ByVal tran As SqlClient.SqlTransaction) As BaseBO
        '    Dim SaveResults As BaseBO = MyClass.Save(tran)
        '    'sort out link

        '    'make sure there were no errors
        '    If Not SaveResults Is Nothing Then
        '        If CType(SaveResults, BO.BaseBO).ValidationErrors Is Nothing OrElse _
        '            Not CType(SaveResults, BO.BaseBO).ValidationErrors.HasErrors Then

        '            'check if link exists
        '            Dim Link As New DataObjects.Entity.PermitSpecimen
        '            Try 'couldn't add the required constraint on permit and specimenid to do the above check with so just put a try for now
        '                Link.ServiceObject.Insert(permitId, SpecimenId, UOM.UOMId, tran)
        '            Catch ex As Exception

        '            End Try
        '            'End If
        '        End If
        '    End If
        '    Return SaveResults
        'End Function

        Private Function InsertPermitSpecimenLink(ByVal partyId As Int32, ByVal permitId As Int32, ByVal addressId As Int32, ByVal tran As SqlClient.SqlTransaction) As Boolean
            'sort out link

            'check if link exists
            Dim Link As New DataObjects.Entity.PermitSpecimen
            Try 'couldn't add the required constraint on permit and specimenid to do the above check with so just put a try for now
                Link.ServiceObject.Insert(permitId, SpecimenId, UOM.UOMId, tran)
            Catch ex As Exception
            End Try

            'Dim PartySpecimen As New Party.BOPartySpecimen(partyId, SpecimenId, addressId, tran)
            ''need to do delete too
            'With PartySpecimen
            '    Try
            '        Dim ExistingPartySpecimen As New Party.BOPartySpecimen(SpecimenId, tran)
            '        ExistingPartySpecimen.EndDate = Date.Now
            '        ExistingPartySpecimen.Save()
            '        .StartDate = ExistingPartySpecimen.EndDate
            '    Catch ex As RecordDoesNotExist
            '        .StartDate = Date.Now
            '    End Try
            '    .RoleType = Party.BOPartySpecimen.Role.Owner
            '    .PartySpecimenStatus = Nothing
            '    PartySpecimen = CType(PartySpecimen.Save(tran), Party.BOPartySpecimen)
            '    Return (PartySpecimen.ValidationErrors Is Nothing)
            'End With

            Return True
        End Function

        Public Enum TypeOfSave
            PermitId
            PartyId
        End Enum

        Public Overridable Overloads Function Save(ByVal partyId As Int32, ByVal permitId As Int32, ByVal tran As SqlClient.SqlTransaction) As BaseBO

            MyBase.Save()
            Dim NewSpecimen As New DataObjects.Entity.Specimen
            Dim service As DataObjects.Service.SpecimenService = NewSpecimen.ServiceObject
            Dim acquisitionObj As Object

            Created = (mSpecimenId = 0)

            If mAcquisitionDate <> Nothing Then
                acquisitionObj = mAcquisitionDate
            End If

            If Not FatherSpecimenId Is Nothing Then
                'save the specie object
                mFatherSpecimen = CType(mFatherSpecimen.Save(partyId, permitId, tran), BOSpecimen)
                'check to see if there has been any problems
                If Not mFatherSpecimen.ValidationErrors Is Nothing Then
                    'rollback the transaction
                    If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    'get the problems and assign them locally
                    ValidationErrors = mFatherSpecimen.ValidationErrors
                    'bail
                    Return Me
                End If
            End If

            If Not mUOM Is Nothing Then
                'save the specie object
                mUOM = CType(mUOM.Save(tran), BOMeasurement)
                'check to see if there has been any problems
                If Not mUOM.ValidationErrors Is Nothing Then
                    'rollback the transaction
                    If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    'get the problems and assign them locally
                    ValidationErrors = mUOM.ValidationErrors
                    'bail
                    Return Me
                End If
            End If

            If Not MotherSpecimenId Is Nothing Then
                'save the specie object
                mMotherSpecimen = CType(mMotherSpecimen.Save(partyId, permitId, tran), BOSpecimen)
                'check to see if there has been any problems
                If Not mMotherSpecimen.ValidationErrors Is Nothing Then
                    'rollback the transaction
                    If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    'get the problems and assign them locally
                    ValidationErrors = mMotherSpecimen.ValidationErrors
                    'bail
                    Return Me
                End If
            End If

            If Created Then
                NewSpecimen = service.Insert(mGender, _
                                             mDOB, _
                                             FateId, _
                                             FatherSpecimenId, _
                                             MotherSpecimenId, _
                                             acquisitionObj, _
                                             mDescription, _
                                             mExactDOB, _
                                             mFateDate, _
                                             tran)
            Else
                NewSpecimen = service.Update(mSpecimenId, _
                                             mGender, _
                                             mDOB, _
                                             FateId, _
                                             FatherSpecimenId, _
                                             MotherSpecimenId, _
                                             acquisitionObj, _
                                             mDescription, _
                                             mExactDOB, _
                                             mFateDate, _
                                             mSpecimenCheckSum, _
                                                tran)
            End If
            'check to see if any SQL errors have occured
            If NewSpecimen Is Nothing AndAlso Not DataObjects.Sprocs.LastError Is Nothing Then
                'TODO: Use errors collection to check to see if the problem was concurrency
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveSpecimen)
            ElseIf NewSpecimen Is Nothing Then
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveSpecimen)
            Else
                If Created And Not NewSpecimen Is Nothing Then
                    mSpecimenId = NewSpecimen.Id
                End If
                If NewSpecimen.CheckSum <> mSpecimenCheckSum Then
                    'no point in initialising unless things have changed
                    InitialiseSpecimen(NewSpecimen, tran)
                End If
            End If

            'Now the specimen is saved we need to link it to a Permit and Save the Party-Specimen link
            Dim addressid As Int32 = 0
            InsertPermitSpecimenLink(partyId, permitId, addressid, tran)

            Return Me

        End Function
#End Region

#Region " Operations "

        Public Function DeleteSpecimenMarks(ByVal marks As BO.Application.BOSpecimenMark(), ByVal tran As SqlClient.SqlTransaction) As Boolean
            Dim SpecimenMarkService As New DataObjects.Service.SpecimenIDMarkService
            Dim Success As Boolean = True


            If Not marks Is Nothing Then
                For Each Mark As BOSpecimenMark In marks
                    If Not SpecimenMarkService.DeleteById(Mark.SpecimenMarkId, 0, tran) Then
                        If Not DataObjects.Sprocs.LastError Is Nothing Then
                            SpecimenMarkService.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                        Else
                            SpecimenMarkService.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                        End If
                        Success = False
                    End If
                Next
            End If
            Return Success
        End Function

        Public Overridable Function DeleteSpecimenAndMarks(ByVal specimen As BOSpecimen, ByVal specimenMarks As BO.Application.BOSpecimenMark()) As Boolean

            Dim SpecimenService As New DataObjects.Service.SpecimenService
            Dim tran As SqlClient.SqlTransaction = SpecimenService.BeginTransaction
            Dim Success As Boolean = True

            If DeleteSpecimenMarks(specimenMarks, tran) Then
                If Not SpecimenService.DeleteById(specimen.SpecimenId, 0, tran) Then
                    If Not DataObjects.Sprocs.LastError Is Nothing Then
                        SpecimenService.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    Else
                        SpecimenService.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    End If
                    Success = False
                Else
                    tran.Commit()
                    tran.Dispose()
                    tran = Nothing
                End If
            End If
            Return Success
        End Function


        Public Overridable Function DeleteSpecimenById(ByVal specimen As BOSpecimen) As Boolean
            Dim SpecimenService As New DataObjects.Service.SpecimenService
            Dim SpecimenMarkService As New DataObjects.Service.SpecimenIDMarkService
            Dim Success As Boolean = True
            Dim tran As SqlClient.SqlTransaction = SpecimenMarkService.BeginTransaction

            If specimen.SpecimenMarks Is Nothing Then
                SetSpecimenMarks(specimen.SpecimenId, tran)
            End If


            If DeleteSpecimenMarks(specimen.SpecimenMarks, tran) Then
                If Not SpecimenService.DeleteById(specimen.SpecimenId, 0, tran) Then
                    If Not DataObjects.Sprocs.LastError Is Nothing Then
                        SpecimenService.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    Else
                        SpecimenService.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    End If
                    Success = False
                Else
                    tran.Commit()
                    tran.Dispose()
                    tran = Nothing
                End If
            End If
            Return Success
        End Function

        Public Overrides Function Clone() As Object
            Dim NewSpecimen As BOSpecimen = CType(MyBase.Clone(), BOSpecimen)
            NewSpecimen.SpecimenId = 0
            NewSpecimen.SpecimenMarks = Nothing
            Return NewSpecimen
        End Function

        Public Function GetSpeciesReportName() As String
            Dim links As EntitySet.SpecimenSpecieSet = Entity.SpecimenSpecie.GetForSpecimen(mSpecimenId)
            Dim primary As BOSpecie = GetPrimarySpecie(links)
            If Not links Is Nothing AndAlso Not primary Is Nothing Then
                If links.Count = 1 Then
                    Return primary.CommonName
                End If
                Return primary.CommonName + " Hybrid"
            End If
            Return "Unknown"
        End Function

        Private Function GetPrimarySpecie(ByVal links As EntitySet.SpecimenSpecieSet) As BOSpecie
            For Each link As Entity.SpecimenSpecie in links
                If link.HybridSequence = 1 Then
                    Return New BOSpecie(link.SpecieId, Nothing)
                End If
            Next
            Return Nothing
        End Function
#End Region

    End Class
End Namespace


Namespace Application.CITES
    Public Class BOCITESPermit
        Inherits BOPermit
        Implements IBOCITESPermit

#Region " Prelim code "
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal citesPermitId As Int32)
            MyClass.New()
            LoadCITESPermit(citesPermitId)
        End Sub

        Private Function LoadCITESPermit(ByVal id As Int32) As DataObjects.Entity.CITESPermit
            Return LoadCITESPermit(id, Nothing)
        End Function

        Private Function LoadCITESPermit(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.CITESPermit
            Dim NewCITESPermit As DataObjects.Entity.CITESPermit = DataObjects.Entity.CITESPermit.GetById(id)
            If NewCITESPermit Is Nothing Then
                Throw New RecordDoesNotExist("CITESPermit", id)
            Else
                InitialiseCITESPermit(NewCITESPermit, tran)
                Return NewCITESPermit
            End If
        End Function

        Friend Overridable Sub InitialiseCITESPermit(ByVal citesPermit As DataObjects.Entity.CITESPermit, ByVal tran As SqlClient.SqlTransaction)
            Try
                With citesPermit
                    MyBase.InitialisePermit(New DataObjects.Entity.Permit(.PermitId, tran), tran)

                    If Not .IsAuthorityLocationNull Then mAuthorityLocation = New ReferenceData.BOCountry(.AuthorityLocation, tran)
                    If Not .IsDerivativeNull() Then mDerivative = New ReferenceData.BOCITESDerivative(.Derivative, tran)
                    If Not .IsPurposeIdNull() Then mPurpose = New ReferenceData.BOCITESPurpose(.PurposeId, tran)
                    If Not .IsSource1IdNull() Then mSource1 = New ReferenceData.BOCITESSource(.Source1Id, tran)
                    If Not .IsSource2IdNull() Then mSource2 = New ReferenceData.BOCITESSource(.Source2Id, tran)
                    If Not .IsDelegationAuthorityIdNull() Then mDelegationOfAuthorityGuidline = New ReferenceData.BODelegationGuideLine(.DelegationAuthorityId, tran)
                    mCITESPermitID = .Id
                    mIsRetrospective = .IsRetrospective
                    mCITESPermitChecksum = .CheckSum
                    mUnderDerogation = .UnderDerogation
                End With
            Catch ex As Exception
            End Try
        End Sub
#End Region

#Region " Properties "
        Public Property DelegationOfAuthorityGuidline() As ReferenceData.BODelegationGuideLine Implements IBOCITESPermit.DelegationOfAuthorityGuidline
            Get
                Return mDelegationOfAuthorityGuidline
            End Get
            Set(ByVal Value As ReferenceData.BODelegationGuideLine)
                mDelegationOfAuthorityGuidline = Value
            End Set
        End Property
        Private mDelegationOfAuthorityGuidline As ReferenceData.BODelegationGuideLine

        Public Property CITESPermitChecksum() As Integer Implements IBOCITESPermit.CITESPermitChecksum
            Get
                Return mCITESPermitChecksum
            End Get
            Set(ByVal Value As Integer)
                mCITESPermitChecksum = Value
            End Set
        End Property
        Private mCITESPermitChecksum As Int32

        Public Property AuthorityLocation() As ReferenceData.BOCountry Implements IBOCITESPermit.AuthorityLocation
            Get
                Return mAuthorityLocation
            End Get
            Set(ByVal Value As ReferenceData.BOCountry)
                mAuthorityLocation = Value
            End Set
        End Property
        Private mAuthorityLocation As ReferenceData.BOCountry

        Public Property Derivative() As ReferenceData.BOCITESDerivative Implements IBOCITESPermit.Derivative
            Get
                Return mDerivative
            End Get
            Set(ByVal Value As ReferenceData.BOCITESDerivative)
                mDerivative = Value
            End Set
        End Property
        Private mDerivative As ReferenceData.BOCITESDerivative

        Public Property IsRetrospective() As Boolean Implements IBOCITESPermit.IsRetrospective
            Get
                Return mIsRetrospective
            End Get
            Set(ByVal Value As Boolean)
                mIsRetrospective = Value
            End Set
        End Property
        Private mIsRetrospective As Boolean


        Public Property UnderDerogation() As Boolean Implements IBOCITESPermit.UnderDerogation
            Get
                Return mUnderDerogation
            End Get
            Set(ByVal Value As Boolean)
                mUnderDerogation = Value
            End Set
        End Property
        Private mUnderDerogation As Boolean

        Public Property Purpose() As ReferenceData.BOCITESPurpose Implements IBOCITESPermit.Purpose
            Get
                Return mPurpose
            End Get
            Set(ByVal Value As ReferenceData.BOCITESPurpose)
                mPurpose = Value
            End Set
        End Property
        Private mPurpose As ReferenceData.BOCITESPurpose

        Public Property CITESPermitId() As Integer Implements IBOCITESPermit.CITESPermitId
            Get
                Return mCITESPermitID
            End Get
            Set(ByVal Value As Integer)
                mCITESPermitID = Value
            End Set
        End Property
        Private mCITESPermitID As Int32

        Public Property Source1() As ReferenceData.BOCITESSource Implements IBOCITESPermit.Source1
            Get
                Return mSource1
            End Get
            Set(ByVal Value As ReferenceData.BOCITESSource)
                mSource1 = Value
            End Set
        End Property
        Private mSource1 As ReferenceData.BOCITESSource

        Public Property Source2() As ReferenceData.BOCITESSource Implements IBOCITESPermit.Source2
            Get
                Return mSource2
            End Get
            Set(ByVal Value As ReferenceData.BOCITESSource)
                mSource2 = Value
            End Set
        End Property
        Private mSource2 As ReferenceData.BOCITESSource

        'Protected ReadOnly Property ReportPermitReference() As String
        '    Get
        '        Return String.Concat(ReportISOCode, ReportApplicationRef, "/", ReportPermitNo, "/", ReportCopyNo)
        '    End Get
        'End Property

        'Protected ReadOnly Property ReportCopyNo() As String
        '    Get
        '        If NumberOfCopies Is Nothing Then
        '            Return String.Empty
        '        Else
        '            Return NumberOfCopies.ToString.PadLeft(3, CType("0", Char))
        '        End If
        '    End Get
        'End Property

        Protected ReadOnly Property ReportPermitNo() As String
            Get
                If PermitNumber <= 0 Then
                    Return String.Empty
                Else
                    Return PermitNumber.ToString.PadLeft(2, CType("0", Char))
                End If
            End Get
        End Property

        Protected ReadOnly Property ReportApplicationRef() As String
            Get
                Return ApplicationId.ToString
            End Get
        End Property

        Protected ReadOnly Property ReportISOCode() As String
            Get
                ' PLW - Modified 10/5/2005 so that ManagementAuthority is checked for Nothing.
                ' This is a bit of a fudge. We should be asking if ManagementAuthority is allowed to be Nothing!
                Dim app As Application.CITES.Applications.BOCITESApplication = GetApplication()
                If Not app.ManagementAuthority Is Nothing Then
                    Return app.ManagementAuthority.Party.GetMailingAddress(Nothing).ISO2CountryCode()
                Else
                    Return String.Empty
                End If
            End Get
        End Property

        Public ReadOnly Property PermitReference(ByVal numberOfCopies As String) As String 'MLD 15/11/5 changed to Public
            Get
                Dim Suffix As String = ""
                If Not numberOfCopies Is Nothing AndAlso numberOfCopies <> "" Then
                    Suffix = "/" & numberOfCopies
                End If
                Return String.Concat(ReportISOCode, ReportApplicationRef, "/", ReportPermitNo, Suffix)
            End Get
        End Property

#End Region

#Region " Helper Functions "

        Private ReadOnly Property DelegationOfAuthorityGuidlineId() As Object
            Get
                If mDelegationOfAuthorityGuidline Is Nothing OrElse mDelegationOfAuthorityGuidline.ID = 0 Then
                    Return Nothing
                Else
                    Return mDelegationOfAuthorityGuidline.ID
                End If
            End Get
        End Property

        Private ReadOnly Property DerivativeId() As Object
            Get
                If mDerivative Is Nothing OrElse mDerivative.ID = 0 Then
                    Return Nothing
                Else
                    Return mDerivative.ID
                End If
            End Get
        End Property

        Private ReadOnly Property PurposeId() As Object
            Get
                If mPurpose Is Nothing OrElse mPurpose.ID = 0 Then
                    Return Nothing
                Else
                    Return mPurpose.ID
                End If
            End Get
        End Property

        Private ReadOnly Property Source1Id() As Object
            Get
                If mSource1 Is Nothing OrElse mSource1.ID = 0 Then
                    Return Nothing
                Else
                    Return mSource1.ID
                End If
            End Get
        End Property

        Private ReadOnly Property Source2Id() As Object
            Get
                If mSource2 Is Nothing OrElse mSource2.ID = 0 Then
                    Return Nothing
                Else
                    Return mSource2.ID
                End If
            End Get
        End Property

        Private ReadOnly Property AuthorityLocationId() As Object
            Get
                If mAuthorityLocation Is Nothing OrElse mAuthorityLocation.ID = 0 Then
                    Return Nothing
                Else
                    Return mAuthorityLocation.ID
                End If
            End Get
        End Property


#End Region

#Region " Save "
        Public Overloads Overrides Function Save() As BaseBO
            Dim NewPermit As New DataObjects.Entity.CITESPermit
            Dim service As DataObjects.Service.CITESPermitService = NewPermit.ServiceObject
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
            Dim NewCITESPermit As New DataObjects.Entity.CITESPermit
            Dim service As DataObjects.Service.CITESPermitService = NewCITESPermit.ServiceObject

            Dim BasePermit As BOPermit = CType(MyBase.Save(tran), BOPermit)
            If Not BasePermit.ValidationErrors Is Nothing Then
                'rollback the transaction
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                'get the problems and assign them locally
                ValidationErrors = BasePermit.ValidationErrors
                'bail
                Return Me
            End If

            Created = (mCITESPermitID = 0)

            If Created Then
                NewCITESPermit = service.Insert(PermitId, _
                                                DerivativeId, _
                                                AuthorityLocationId, _
                                                mUnderDerogation, _
                                                mIsRetrospective, _
                                                PurposeId, _
                                                Source1Id, _
                                                Source2Id, _
                                                 DelegationOfAuthorityGuidlineId, _
                                                 mExpiryDate, _
                                                 tran)
            Else
                NewCITESPermit = service.Update(mCITESPermitID, _
                                                PermitId, _
                                                DerivativeId, _
                                                AuthorityLocationId, _
                                                mUnderDerogation, _
                                                mIsRetrospective, _
                                                PurposeId, _
                                                Source1Id, _
                                                Source2Id, _
                                                DelegationOfAuthorityGuidlineId, _
                                                mExpiryDate, _
                                                mCITESPermitChecksum, _
                                                tran)
            End If
            'check to see if any SQL errors have occured
            If NewCITESPermit Is Nothing AndAlso Not DataObjects.Sprocs.LastError Is Nothing Then
                'TODO: Use errors collection to check to see if the problem was concurrency
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSavePermit)
            ElseIf NewCITESPermit Is Nothing Then
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSavePermit)
            ElseIf Created And Not NewCITESPermit Is Nothing Then
                mCITESPermitID = NewCITESPermit.Id
            End If

            If NewCITESPermit.CheckSum <> mCITESPermitChecksum Then
                'no point in initialising unless things have changed
                InitialiseCITESPermit(NewCITESPermit, tran)
            End If
            Return Me
        End Function
#End Region

#Region " Operations "

        Public Function GetCareAndAccomodation(ByVal permitId As Int32, ByVal tran As SqlClient.SqlTransaction) As BO.Application.CITES.BOCareAccommodation
            If Not specieid Is Nothing Then
                Return New BO.Application.CITES.BOCareAccommodation(permitId, CType(specieid, Int32), tran)
            End If
        End Function

        Public Shared Function GetByPermitId(ByVal permitId As Int32) As BOCITESPermit
            Dim BOCITESPermit As New BOCITESPermit
            Return BOCITESPermit.LoadByPermitId(permitId)
        End Function

        Public Overrides Function DeletePermitbyId(ByVal permit As BOPermit, ByVal tran As SqlClient.SqlTransaction) As Boolean
            Dim PermitService As New DataObjects.Service.CITESPermitService
            If tran Is Nothing Then
                tran = PermitService.BeginTransaction
            End If
            If PermitService.DeleteById(CType(permit, BOCITESPermit).CITESPermitId, 0, tran) Then
                If MyBase.DeletePermitById(permit, tran) Then
                    PermitService.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Commit)
                    Return True
                End If
            Else
                PermitService.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
            End If
        End Function

        Protected Overrides Function GetPermit(ByVal permitId As Int32) As BOPermit
            Return LoadByPermitId(permitId)
        End Function

        Public Overrides Function clone() As Object
            Dim ClonedObject As BO.Application.CITES.Applications.BOCITESImportExportPermit = CType(MyBase.Clone, BO.Application.CITES.Applications.BOCITESImportExportPermit)

            With ClonedObject
                .PermitNumber = 0
                .CITESPermitId = 0
                .CITESImportExportPermitId = 0
                If Not .Specimens Is Nothing Then
                    For Each spec As BOSpecimen In .Specimens
                        spec.SpecimenId = 0
                    Next
                End If
            End With
            Return ClonedObject
        End Function

        Friend Shadows Function GetApplication() As Application.CITES.Applications.BOCITESApplication
            If ApplicationId = 0 Then
                Throw New ArgumentException("Application Id is 0")
            Else
                Dim CITESApps As DataObjects.EntitySet.CITESApplicationSet = DataObjects.Entity.CITESApplication.GetForApplication(ApplicationId)
                If Not CITESApps Is Nothing AndAlso _
                   CITESApps.Count = 1 Then
                    Dim App As New Application.CITES.Applications.BOCITESApplication
                    App.InitialiseCITESApplication(CType(CITESApps.GetEntity(0), DataObjects.Entity.CITESApplication), Nothing)
                    Return App
                End If

            End If
        End Function

        Private Shared Function LoadByPermitId(ByVal permitId As Int32) As BOCITESPermit
            Dim CITESPermits As DataObjects.EntitySet.CITESPermitSet = DataObjects.Entity.CITESPermit.GetForPermit(permitId)
            If Not CITESPermits Is Nothing AndAlso _
               CITESPermits.Count = 1 Then
                Dim NewCITESPermit As New BOCITESPermit
                NewCITESPermit.InitialiseCITESPermit(CType(CITESPermits.GetEntity(0), DataObjects.Entity.CITESPermit), Nothing)
                Return NewCITESPermit
            Else
                Return Nothing
            End If
        End Function


#End Region

    End Class
End Namespace
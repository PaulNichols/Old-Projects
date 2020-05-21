Namespace Application.CITES.ImportNotification
    Public Class BOImportSpecie
        Inherits BONotificationSpecie
        Implements IBOImportSpecie


#Region " Prelim code "

        Public Sub New()

        End Sub

        Public Sub New(ByVal specieId As Int32, ByVal tran As SqlClient.SqlTransaction)
            LoadImportSpecie(specieId, tran)
        End Sub

        Protected Overrides Function LoadSpecie(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.Specie
            Dim Specie As DataObjects.Entity.Specie = MyBase.LoadSpecie(id, tran)
            LoadImportSpecie(Specie.Id, tran)
        End Function

        Public Function LoadImportSpecie(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.ImportSpecie
            Dim NewSpecieSet As DataObjects.EntitySet.ImportSpecieSet = DataObjects.Entity.ImportSpecie.GetForSpecie(id, tran)
            If Not NewSpecieSet Is Nothing AndAlso NewSpecieSet.Count = 1 Then
                ' I think it's zero based?
                Dim NewSpecie As DataObjects.Entity.ImportSpecie = CType(NewSpecieSet.GetEntity(0), DataObjects.Entity.ImportSpecie)
                InitialiseImportSpecie(NewSpecie, tran)
                Return NewSpecie
            Else
                Throw New RecordDoesNotExist("ImportSpecie", id)
            End If
        End Function

        Protected Overridable Sub InitialiseImportSpecie(ByVal importSpecie As DataObjects.Entity.ImportSpecie, ByVal tran As SqlClient.SqlTransaction)
            With importSpecie
                SpecieId = .SpecieId
                ImportSpecieCheckSum = .CheckSum
                If Not .IsCertificateOfOriginNumberNull Then mCertificateOfOriginNumber = .CertificateOfOriginNumber
                If Not .IsExportNumberDateOfIssueNull Then mExportNumberDateOfIssue = .ExportNumberDateOfIssue
                If Not .IsExportPermitNumberNull Then mExportPermitNumber = .ExportPermitNumber
                Id = .Id
            End With
        End Sub
#End Region

#Region " Properties "
        Public Property Section() As String Implements IBOImportSpecie.Section
            Get
                Return mSection
            End Get
            Set(ByVal Value As String)
                mSection = Value
            End Set
        End Property
        Private mSection As String

        Public Property CertificateOfOriginNumber() As Object Implements IBOImportSpecie.CertificateOfOriginNumber
            Get
                Return mCertificateOfOriginNumber
            End Get
            Set(ByVal Value As Object)
                mCertificateOfOriginNumber = Value
            End Set
        End Property
        Private mCertificateOfOriginNumber As Object

        Public Property ExportNumberDateOfIssue() As Date Implements IBOImportSpecie.ExportNumberDateOfIssue
            Get
                Return mExportNumberDateOfIssue
            End Get
            Set(ByVal Value As Date)
                mExportNumberDateOfIssue = Value
            End Set
        End Property
        Private mExportNumberDateOfIssue As Date

        Public Property ExportPermitNumber() As Object Implements IBOImportSpecie.ExportPermitNumber
            Get
                Return mExportPermitNumber
            End Get
            Set(ByVal Value As Object)
                mExportPermitNumber = Value
            End Set
        End Property
        Private mExportPermitNumber As Object

        Public Property Id() As Integer Implements IBOImportSpecie.Id
            Get
                Return mId
            End Get
            Set(ByVal Value As Integer)
                mId = Value
            End Set
        End Property
        Private mId As Int32


        Public Property ImportSpecieCheckSum() As Int32 Implements IBOImportSpecie.CheckSum
            Get
                Return mImportSpecieCheckSum
            End Get
            Set(ByVal Value As Int32)
                mImportSpecieCheckSum = Value
            End Set
        End Property
        Private mImportSpecieCheckSum As Int32
#End Region

#Region " Save "
        Public Overridable Shadows Function Save(ByVal citesNotificationId As Int32) As BOImportSpecie
            Dim NewBOImportSpecie As New DataObjects.Entity.ImportSpecie
            Dim service As DataObjects.Service.ImportSpecieService = NewBOImportSpecie.ServiceObject
            Dim tran As SqlClient.SqlTransaction = service.BeginTransaction

            Dim ThisSpecie As BOImportSpecie = MyClass.Save(citesNotificationId, tran)
            service.EndTransaction(tran)
            Return ThisSpecie
        End Function

        Public Overridable Shadows Function Save() As BOImportSpecie
            Return Save(Nothing, tran:=Nothing)
        End Function

        Public Overridable Shadows Function Save(ByVal citesNotificationId As Int32, ByVal tran As SqlClient.SqlTransaction) As BOImportSpecie
            DataObjects.Sprocs.LastError = Nothing
            Dim specie As BOSpecie
            If citesNotificationId > 0 Then
                specie = MyBase.Save(citesNotificationId, tran)
            Else
                specie = MyBase.Save(tran)
            End If

            If Not MyBase.ValidationErrors Is Nothing Then
                Return Me
            Else
                If specie Is Nothing Then
                    ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveImportSpecie)
                    Return Me
                Else
                    Dim NewImportSpecie As New DataObjects.Entity.ImportSpecie
                    Dim service As DataObjects.Service.ImportSpecieService = NewImportSpecie.ServiceObject

                    Created = (ImportSpecieCheckSum = 0)

                    If Created Then
                        service.Insert(specie.SpecieId, _
                                       mExportPermitNumber, _
                                       mExportNumberDateOfIssue, _
                                       mCertificateOfOriginNumber, _
                                       tran)
                    Else
                        service.Update(Id, specie.SpecieId, _
                                        mExportPermitNumber, _
                                       mExportNumberDateOfIssue, _
                                        mCertificateOfOriginNumber , _
                                        ImportSpecieCheckSum, _
                                       tran)
                    End If
                    Dim ImportSet As DataObjects.EntitySet.ImportSpecieSet = service.GetByIndex_Specie(specie.SpecieId, tran)
                    If ImportSet Is Nothing Then
                        service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                        ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveImportSpecie)
                        Return Me
                    Else
                        NewImportSpecie = CType(ImportSet.GetEntity(0), DataObjects.Entity.ImportSpecie)
                        mId = NewImportSpecie.Id
                        'check to see if any SQL errors have occured
                        If NewImportSpecie Is Nothing AndAlso Not DataObjects.Sprocs.LastError Is Nothing Then
                            'TODO: Use errors collection to check to see if the problem was concurrency
                            service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                            ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveImportSpecie)
                            Return Me
                        ElseIf NewImportSpecie Is Nothing Then
                            service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                            ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveImportSpecie)
                            Return Me
                        End If

                        If NewImportSpecie.CheckSum <> ImportSpecieCheckSum Then
                            'no point in initialising unless things have changed
                            InitialiseImportSpecie(NewImportSpecie, tran)
                        End If
                        Return Me
                    End If
                End If
            End If
        End Function
#End Region

#Region " Validate "
        Protected Overrides Sub GetValidationErrors()
            'check to see if we have too many species
            If MyBase.IsAppendixIII AndAlso (mExportPermitNumber Is Nothing OrElse mExportPermitNumber.ToString = String.Empty) Then
                MyBase.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.ExportPermitNumberMandatoryIfAppendixIII))
            End If
            If MyBase.IsAppendixIII AndAlso (mCertificateOfOriginNumber Is Nothing OrElse mCertificateOfOriginNumber.ToString = String.Empty) Then
                MyBase.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.CertificateOfOriginMandatoryIfAppendixIII))
            End If
        End Sub
#End Region



    End Class
End Namespace
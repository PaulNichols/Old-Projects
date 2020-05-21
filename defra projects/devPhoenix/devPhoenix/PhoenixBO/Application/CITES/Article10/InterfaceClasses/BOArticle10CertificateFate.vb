Namespace Application.CITES.Applications
    Public Class BOArticle10CertificateFate
        Inherits BaseBO
        Implements Application.CITES.Applications.IBOArticle10CertificateFate



#Region " Validate "
        Protected Function Validate() As BaseBO
            Dim returnobj As BOArticle10CertificateFate = Me

            If Not mQtyUsed Is Nothing Then

                Dim DOPermit As New [DO].DataObjects.Entity.Permit(mPermitId, Nothing)
                Dim PermitSpecSet As [DO].DataObjects.EntitySet.PermitSpecimenSet = DOPermit.GetRelatedPermitSpecimen
                Dim Specimen As New [DO].DataObjects.Entity.Specimen(PermitSpecSet.Entities(0).SpecimenId, Nothing)

                Dim DOUom As New [DO].DataObjects.Entity.UOM(PermitSpecSet.Entities(0).UOMId)

                If mQtyUsed.ToString = "" AndAlso DOUom.Qty > 1 Then
                    If returnobj.ValidationErrors Is Nothing Then returnobj.ValidationErrors = New ValidationManager(ValidationManager.ValidationTitles.CannotSaveExportApplication)
                    returnobj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.YouMustEnterANumberForFateQty))
                End If

                If Me.mFate.ID = 0 Then
                    If returnobj.ValidationErrors Is Nothing Then returnobj.ValidationErrors = New ValidationManager(ValidationManager.ValidationTitles.CannotSaveExportApplication)
                    returnobj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.YouMustChooseAFate))
                End If

                If DOUom.Qty > 1 AndAlso (mQtyUsed.ToString = "" OrElse CType(mQtyUsed, Int32) > DOUom.Qty) Then
                    If returnobj.ValidationErrors Is Nothing Then returnobj.ValidationErrors = New ValidationManager(ValidationManager.ValidationTitles.CannotSaveExportApplication)
                    returnobj.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.YouCannotFateMoreThanPermitQty))
                End If
            End If

            Return returnobj
        End Function
#End Region

#Region " Save "


        Public Overridable Overloads Function Save(ByVal permissions As String, ByVal authorisedUserId As Int64) As BaseBO
            Me.ValidationErrors = Validate.ValidationErrors

            If Me.ValidationErrors Is Nothing Then
                Dim NewFate As New DataObjects.Entity.Article10CertificateFate
                Dim service As DataObjects.Service.Article10CertificateFateService = NewFate.ServiceObject
                Dim tran As SqlClient.SqlTransaction = service.BeginTransaction

                Created = (mArticle10CertificateFateId = 0)

                Dim BOFate As Object = MyBase.Save

                If Not BOFate Is Nothing AndAlso _
                   Not CType(BOFate, BaseBO).ValidationErrors Is Nothing Then
                    'rollback the transaction
                    If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    'get the problems and assign them locally
                    ValidationErrors = CType(BOFate, BaseBO).ValidationErrors
                    'bail
                    Return Me
                End If

                If Created Then
                    NewFate = service.Insert( _
                                            Me.mFate.ID, _
                                            mQtyUsed, _
                                            mReturnedToDefra, _
                                            mPermitId, mspecimensoldto, tran)
                Else
                    'NewFate = service.Update(, _
                    '                                         tran)
                End If


                ''check to see if any SQL errors have occured
                If (NewFate Is Nothing AndAlso Not DataObjects.Sprocs.LastError Is Nothing) Then
                    'TODO: Use errors collection to check to see if the problem was concurrency
                    If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveArticle10Application)
                    Return Me
                ElseIf Created And Not NewFate Is Nothing Then
                    mArticle10CertificateFateId = NewFate.Id
                End If

                Try
                    ' If NewFate.CheckSum <> mArticle10CertificateFateCheckSum Then
                    If ChangeStatus(authorisedUserId, tran) Then
                        '  service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Commit)
                        InitialiseFate(NewFate, tran)
                    Else
                        service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    End If
                    'End If
                Catch ex As Exception
                    If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveArticle10Application)
                End Try
            End If
            Return Me
        End Function
#End Region

        Private Function ChangeStatus(ByVal authorisedUserId As Int64, ByVal tran As SqlClient.SqlTransaction) As Boolean
            Dim BoPermit As New BO.Application.BOPermit(Me.PermitId)
            Dim PIs As BO.Application.BOPermitInfo() = BoPermit.GetPermitInfos(tran)
            Dim Ids(PIs.Length - 1) As Int32
            Dim i As Int32

            For Each pi As BO.Application.BOPermitInfo In BoPermit.GetPermitInfos(tran)
                Ids(i) = pi.PermitInfoId
                i += 1
            Next


            Return BO.Application.BOPermitInfo.ChangeStatus(Ids, Common.AssignedToList.CaseOfficer, authorisedUserId, _
                New BO.Application.AdditionalInformation(BO.Application.BOPermitInfo.PermitStatusTypes.ReturnedUsed), tran)
        End Function

#Region " Prelim code "
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal fateId As Int32)
            MyClass.New()
            LoadFate(fateId)
        End Sub

        Public Function LoadFateByCertificateId(ByVal certificateId As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.Article10CertificateFate
            Dim NewFate As DataObjects.EntitySet.Article10CertificateFateSet = DataObjects.Entity.Article10CertificateFate.GetForPermit(certificateId, tran)
            If NewFate Is Nothing OrElse NewFate.Entities.Count = 0 Then
                Dim ReturnFate As New BO.Application.CITES.Applications.BOArticle10CertificateFate
                ReturnFate.Fate = New BO.ReferenceData.BOArticle10Fate
            Else
                InitialiseFate(NewFate.Entities(0), tran)
                Return NewFate.Entities(0)
            End If
        End Function

        Public Function LoadFate(ByVal id As Int32) As DataObjects.Entity.Article10CertificateFate
            Return LoadFate(id, Nothing)
        End Function

        Private Function LoadFate(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.Article10CertificateFate
            Dim NewFate As DataObjects.Entity.Article10CertificateFate = DataObjects.Entity.Article10CertificateFate.GetById(id, tran)
            If NewFate Is Nothing Then
                Throw New RecordDoesNotExist("Article10CertificateFate", id)
            Else
                InitialiseFate(NewFate, tran)
                Return NewFate
            End If
        End Function

        Public Overridable Sub InitialiseFate(ByVal fate As DataObjects.Entity.Article10CertificateFate, ByVal tran As SqlClient.SqlTransaction)
            With fate
                mArticle10CertificateFateId = .Article10CertificateFateId
                mFate = New BO.ReferenceData.BOArticle10Fate(.FateId)
                If Not .IsQtyUsedNull Then mQtyUsed = .QtyUsed
                mReturnedToDefra = .ReturnedToDefra
                .PermitId = mPermitId
            End With
        End Sub
#End Region

#Region "Properties"
        Public Property Article10CertificateFateId() As Integer Implements IBOArticle10CertificateFate.Article10CertificateFateId
            Get
                Return mArticle10CertificateFateId
            End Get
            Set(ByVal Value As Integer)
                mArticle10CertificateFateId = Value
            End Set
        End Property
        Private mArticle10CertificateFateId As Int32

        Public Property Fate() As ReferenceData.BOArticle10Fate Implements IBOArticle10CertificateFate.Fate
            Get
                Return mFate
            End Get
            Set(ByVal Value As ReferenceData.BOArticle10Fate)
                mFate = Value
            End Set
        End Property
        Private mFate As ReferenceData.BOArticle10Fate

        Public Property QtyUsed() As Object Implements IBOArticle10CertificateFate.QtyUsed
            Get
                Return mQtyUsed
            End Get
            Set(ByVal Value As Object)
                mQtyUsed = Value
            End Set
        End Property
        Private mQtyUsed As Object

        Public Property ReturnedToDefra() As Boolean Implements IBOArticle10CertificateFate.ReturnedToDefra
            Get
                Return mReturnedToDefra
            End Get
            Set(ByVal Value As Boolean)
                mReturnedToDefra = Value
            End Set
        End Property
        Private mReturnedToDefra As Boolean



        Public Property PermitId() As Integer Implements IBOArticle10CertificateFate.PermitId
            Get
                Return mPermitId
            End Get
            Set(ByVal Value As Integer)
                mPermitId = Value
            End Set
        End Property
        Private mPermitId As Int32

        Public Property SpecimenSoldTo() As Object Implements IBOArticle10CertificateFate.SpecimenSoldTo
            Get
                Return mspecimensoldto
            End Get
            Set(ByVal Value As Object)
                mspecimensoldto = Value
            End Set
        End Property
        Private mspecimensoldto As Object
#End Region


    End Class
End Namespace

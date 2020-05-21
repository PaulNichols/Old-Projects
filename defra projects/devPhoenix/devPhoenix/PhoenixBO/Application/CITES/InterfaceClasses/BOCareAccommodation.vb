Namespace Application.CITES
    Public Class BOCareAccommodation
        Inherits BaseBO

        Implements ICareAccommodation


#Region " Prelim code "
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal permitid As Int32, ByVal specieId As Int32, ByVal tran As SqlClient.SqlTransaction)
            MyClass.New()
            Try
                LoadCA(permitid, specieId)
            Catch ex As RecordDoesNotExist
                Dim BoPermit As New BO.Application.BOPermit(permitid, tran)
                Dim BOApp As New BO.Application.BOApplication(BoPermit.ApplicationId, 0, tran)
                If mApplicantName Is Nothing Then Me.mApplicantName = BOApp.Party.Party.DisplayName
                '  If Me.PremisesDetails Is Nothing Then Me.mPremisesDetails = BOApp.Party.Address.ReportAddress
                If mApplicantId Is Nothing Then Me.mApplicantId = BOApp.Party.Party.PartyId
                If Not BoPermit.Specie Is Nothing Then
                    If mScientificName Is Nothing Then Me.mScientificName = BoPermit.Specie.ScientificName
                    If mApplicationid Is Nothing Then Me.mApplicationid = BoPermit.ApplicationPermitNumber
                    If mCommonName Is Nothing Then Me.mCommonName = BoPermit.Specie.CommonName
                End If
                If Not BoPermit.Specimens Is Nothing Then
                    If mTotalNumberOfSpecimens Is Nothing Then Me.mTotalNumberOfSpecimens = BoPermit.Specimens.Length
                End If
            End Try

        End Sub

        Private Function LoadCA(ByVal permitid As Int32, ByVal specieId As Int32) As DataObjects.Entity.AccommodationAndCare
            Return LoadCA(permitid, specieId, Nothing)
        End Function

        Private Function LoadCA(ByVal permitid As Int32, ByVal specieId As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.AccommodationAndCare
            Me.SpecieId = specieId
            Me.PermitId = permitid
            Dim Service As New uk.gov.defra.Phoenix.DO.DataObjects.Service.AccommodationAndCareService
            Dim NewCandASet As DataObjects.EntitySet.AccommodationAndCareSet = Service.GetByIndex_IX_AccommodationAndCare(specieId, permitid, tran)

            If NewCandASet Is Nothing OrElse NewCandASet.Count = 0 Then
                Throw New RecordDoesNotExist("Care and Accomidation", 0)
            Else
                InitialiseCA(NewCandASet.Entities(0), tran)
                Return NewCandASet.Entities(0)
            End If
        End Function

        Protected Overridable Sub InitialiseCA(ByVal ca As DataObjects.Entity.AccommodationAndCare, ByVal tran As SqlClient.SqlTransaction)
            With ca
                mAccommodationAndCareId = .AccommodationAndCareId
                mBALAIDeirectiveLicenceHeld = .BALAIDeirectiveLicenceHeld
                mBALAIDeirectiveLicenceNumber = .BALAIDeirectiveLicenceNumber
                mDangerousWildAnimalLicenceHeld = .DangerousWildAnimalLicenceHeld
                mDangerousWildAnimalLicenceNumber = .DangerousWildAnimalLicenceNumber
                mDeclarationAcknowledgement = .DeclarationAcknowledgement
                mEnclosureFurnishing = .EnclosureFurnishing
                mEnclosures = .Enclosures
                mEntryDate = .EntryDate
                mEstablishmentDescription = .EstablishmentDescription
                mFoodProvisions = .FoodProvisions
                mLicensedPetShop = .LicensedPetShop
                mLicensedZoo = .LicensedZoo
                mOtherInformation = .OtherInformation
                mPermitId = .PermitId
                mQuarantineApproved = .QuarantineApproved
                mReceiptDate = .ReciptDate
                mSpecimensPerEnclosure = .SpecimensPerEnclosure
                mVeterinaryProvisions = .VeterinaryProvisions
                mApplicantName = .ApplicantName
                mApplicantId = .ApplicantId
                mScientificName = .ScientificName
                mCommonName = .CommonName
                mTotalNumberOfSpecimens = .TotalNumberOfSpecimens
                mDeliveryAddress = .DeliveryAddress
                mPremisesDetails = .PremisesDetails
            End With
        End Sub

#End Region

#Region "Properties"
        Public Property DeliveryAddress() As String Implements ICareAccommodation.DeliveryAddress
            Get
                Return mDeliveryAddress
            End Get
            Set(ByVal Value As String)
                mDeliveryAddress = Value
            End Set
        End Property
        Private mDeliveryAddress As String

        Public Property ApplicantId() As Object Implements ICareAccommodation.ApplicantId
            Get
                Return mApplicantId
            End Get
            Set(ByVal Value As Object)
                mApplicantId = Value
            End Set
        End Property
        Private mApplicantId As Object

        Public Property ApplicantName() As String Implements ICareAccommodation.ApplicantName
            Get
                Return mApplicantName
            End Get
            Set(ByVal Value As String)
                mApplicantName = Value
            End Set
        End Property
        Private mApplicantName As String

        Public Property CommonName() As String Implements ICareAccommodation.CommonName
            Get
                Return mCommonName
            End Get
            Set(ByVal Value As String)
                mCommonName = Value
            End Set
        End Property
        Private mCommonName As String

        Public Property PremisesDetails() As String Implements ICareAccommodation.PremisesDetails
            Get
                Return mPremisesDetails
            End Get
            Set(ByVal Value As String)
                mPremisesDetails = Value
            End Set
        End Property
        Private mPremisesDetails As String

        Public Property ScientificName() As String Implements ICareAccommodation.ScientificName
            Get
                Return mScientificName
            End Get
            Set(ByVal Value As String)
                mScientificName = Value
            End Set
        End Property
        Private mScientificName As String

        Public Property SpecieId() As Integer Implements ICareAccommodation.SpecieId
            Get
                Return mSpecieId
            End Get
            Set(ByVal Value As Integer)
                mSpecieId = Value
            End Set
        End Property
        Private mSpecieId As Int32

        Public Property TotalNumberOfSpecimens() As Object Implements ICareAccommodation.TotalNumberOfSpecimens
            Get
                Return mTotalNumberOfSpecimens
            End Get
            Set(ByVal Value As Object)
                mTotalNumberOfSpecimens = Value
            End Set
        End Property
        Private mTotalNumberOfSpecimens As Object

        Public Property AccommodationAndCareId() As Int32 Implements ICareAccommodation.AccommodationAndCareId
            Get
                Return mAccommodationAndCareId
            End Get
            Set(ByVal Value As Integer)
                mAccommodationAndCareId = Value
            End Set
        End Property
        Private mAccommodationAndCareId As Int32

        Public Property BALAIDeirectiveLicenceHeld() As Boolean Implements ICareAccommodation.BALAIDeirectiveLicenceHeld
            Get
                Return mBALAIDeirectiveLicenceHeld
            End Get
            Set(ByVal Value As Boolean)
                mBALAIDeirectiveLicenceHeld = Value
            End Set
        End Property
        Private mBALAIDeirectiveLicenceHeld As Boolean

        Public Property BALAIDeirectiveLicenceNumber() As Object Implements ICareAccommodation.BALAIDeirectiveLicenceNumber
            Get
                Return mBALAIDeirectiveLicenceNumber
            End Get
            Set(ByVal Value As Object)
                mBALAIDeirectiveLicenceNumber = Value
            End Set
        End Property
        Private mBALAIDeirectiveLicenceNumber As Object

        Public Property DangerousWildAnimalLicenceHeld() As Boolean Implements ICareAccommodation.DangerousWildAnimalLicenceHeld
            Get
                Return mDangerousWildAnimalLicenceHeld
            End Get
            Set(ByVal Value As Boolean)
                mDangerousWildAnimalLicenceHeld = Value
            End Set
        End Property
        Private mDangerousWildAnimalLicenceHeld As Boolean

        Public Property DangerousWildAnimalLicenceNumber() As Object Implements ICareAccommodation.DangerousWildAnimalLicenceNumber
            Get
                Return mDangerousWildAnimalLicenceNumber
            End Get
            Set(ByVal Value As Object)
                mDangerousWildAnimalLicenceNumber = Value
            End Set
        End Property
        Private mDangerousWildAnimalLicenceNumber As Object

        Public Property DeclarationAcknowledgement() As Boolean Implements ICareAccommodation.DeclarationAcknowledgement
            Get
                Return mDeclarationAcknowledgement
            End Get
            Set(ByVal Value As Boolean)
                mDeclarationAcknowledgement = Value
            End Set
        End Property
        Private mDeclarationAcknowledgement As Boolean

        Public Property EnclosureFurnishing() As Object Implements ICareAccommodation.EnclosureFurnishing
            Get
                Return mEnclosureFurnishing
            End Get
            Set(ByVal Value As Object)
                mEnclosureFurnishing = Value
            End Set
        End Property
        Private mEnclosureFurnishing As Object

        Public Property Enclosures() As Object Implements ICareAccommodation.Enclosures
            Get
                Return mEnclosures
            End Get
            Set(ByVal Value As Object)
                mEnclosures = Value
            End Set
        End Property
        Private mEnclosures As Object

        Public Property EntryDate() As Object Implements ICareAccommodation.EntryDate
            Get
                Return mEntryDate
            End Get
            Set(ByVal Value As Object)
                mEntryDate = Value
            End Set
        End Property
        Private mEntryDate As Object

        Public Property EstablishmentDescription() As Object Implements ICareAccommodation.EstablishmentDescription
            Get
                Return mEstablishmentDescription
            End Get
            Set(ByVal Value As Object)
                mEstablishmentDescription = Value
            End Set
        End Property
        Private mEstablishmentDescription As Object

        Public Property FoodProvisions() As Object Implements ICareAccommodation.FoodProvisions
            Get
                Return mFoodProvisions
            End Get
            Set(ByVal Value As Object)
                mFoodProvisions = Value
            End Set
        End Property
        Private mFoodProvisions As Object

        Public Property LicensedPetShop() As Boolean Implements ICareAccommodation.LicensedPetShop
            Get
                Return mLicensedPetShop
            End Get
            Set(ByVal Value As Boolean)
                mLicensedPetShop = Value
            End Set
        End Property
        Private mLicensedPetShop As Boolean

        Public Property LicensedZoo() As Boolean Implements ICareAccommodation.LicensedZoo
            Get
                Return mLicensedZoo
            End Get
            Set(ByVal Value As Boolean)
                mLicensedZoo = Value
            End Set
        End Property
        Private mLicensedZoo As Boolean

        Public Property OtherInformation() As Object Implements ICareAccommodation.OtherInformation
            Get
                Return mOtherInformation
            End Get
            Set(ByVal Value As Object)
                mOtherInformation = Value
            End Set
        End Property
        Private mOtherInformation As Object

        Public Property PermitId() As Int32 Implements ICareAccommodation.PermitId
            Get
                Return mPermitId
            End Get
            Set(ByVal Value As Integer)
                mPermitId = Value
            End Set
        End Property
        Private mPermitId As Int32

        Public Property QuarantineApproved() As Boolean Implements ICareAccommodation.QuarantineApproved
            Get
                Return mQuarantineApproved
            End Get
            Set(ByVal Value As Boolean)
                mQuarantineApproved = Value
            End Set
        End Property
        Private mQuarantineApproved As Boolean

        Public Property ReceiptDate() As Object Implements ICareAccommodation.ReceiptDate
            Get
                Try
                    If mReceiptDate.ToString = "" Then
                        mReceiptDate = Nothing
                    End If
                Catch ex As Exception

                End Try

                Return mReceiptDate
            End Get
            Set(ByVal Value As Object)
                mReceiptDate = Value
            End Set
        End Property
        Private mReceiptDate As Object

        Public Property SpecimensPerEnclosure() As String Implements ICareAccommodation.SpecimensPerEnclosure
            Get
                Return mSpecimensPerEnclosure
            End Get
            Set(ByVal Value As String)
                mSpecimensPerEnclosure = Value
            End Set
        End Property
        Private mSpecimensPerEnclosure As String

        Public Property VeterinaryProvisions() As Object Implements ICareAccommodation.VeterinaryProvisions
            Get
                Return mVeterinaryProvisions
            End Get
            Set(ByVal Value As Object)
                mVeterinaryProvisions = Value
            End Set
        End Property
        Private mVeterinaryProvisions As Object

        Public Property Applicationid() As String Implements ICareAccommodation.Applicationid
            Get
                Return mApplicationid
            End Get
            Set(ByVal Value As String)
                mApplicationid = Value
            End Set
        End Property
        Private mApplicationid As String
#End Region

#Region "Save"
        Public Shadows Function Save(ByVal permissions As String, ByVal tran As SqlClient.SqlTransaction, ByVal authorisedUserId As Int64, ByVal changeStatus As Boolean) As BO.Application.CITES.BOCareAccommodation
            MyBase.Save()
            Dim NewCA As New DataObjects.Entity.AccommodationAndCare
            Dim service As DataObjects.Service.AccommodationAndCareService = NewCA.ServiceObject

            Created = (mAccommodationAndCareId = 0)

            If Created Then
                If Me.ReceiptDate Is Nothing Then ReceiptDate = Date.Now
                NewCA = service.Insert(mPermitId, _
                                                    mEstablishmentDescription, _
                                                    mEnclosures, _
                                                    mSpecimensPerEnclosure, _
                                                    mEnclosureFurnishing, _
                                                    mFoodProvisions, _
                                                    mVeterinaryProvisions, _
                                                    mQuarantineApproved, _
                                                    mLicensedZoo, _
                                                    mLicensedPetShop, _
                                                    mDangerousWildAnimalLicenceHeld, _
                                                    mDangerousWildAnimalLicenceNumber, _
                                                    mBALAIDeirectiveLicenceHeld, _
                                                    mBALAIDeirectiveLicenceNumber, _
                                                    mOtherInformation, _
                                                    mDeclarationAcknowledgement, _
                                                    ReceiptDate, _
                                                    Nothing, _
                                                    Me.mPremisesDetails, _
                                                    Me.mSpecieId, _
                                                    Me.mApplicantName, _
                                                    Me.mScientificName, _
                                                    Me.mCommonName, _
                                                    Me.mTotalNumberOfSpecimens, _
                                                    Me.mApplicantId, _
                                                    tran)
            Else
                If changeStatus Then mEntryDate = Date.Now
                NewCA = service.Update(mAccommodationAndCareId, _
                                                    mPermitId, _
                                                    mEstablishmentDescription, _
                                                    mEnclosures, _
                                                    mSpecimensPerEnclosure, _
                                                    mEnclosureFurnishing, _
                                                    mFoodProvisions, _
                                                    mVeterinaryProvisions, _
                                                    mQuarantineApproved, _
                                                    mLicensedZoo, _
                                                    mLicensedPetShop, _
                                                    mDangerousWildAnimalLicenceHeld, _
                                                    mDangerousWildAnimalLicenceNumber, _
                                                    mBALAIDeirectiveLicenceHeld, _
                                                    mBALAIDeirectiveLicenceNumber, _
                                                    mOtherInformation, _
                                                    mDeclarationAcknowledgement, _
                                                    ReceiptDate, _
                                                    mEntryDate, _
                                                       Me.mPremisesDetails, _
                                                    Me.mSpecieId, _
                                                    Me.mApplicantName, _
                                                    Me.mScientificName, _
                                                    Me.mCommonName, _
                                                    Me.mTotalNumberOfSpecimens, _
                                                    Me.mApplicantId, _
                                                    CheckSum, _
                                                    tran)
            End If
            'check to see if any SQL errors have occured
            If NewCA Is Nothing AndAlso Not DataObjects.Sprocs.LastError Is Nothing Then
                'TODO: Use errors collection to check to see if the problem was concurrency
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveCITESNotification)
            ElseIf NewCA Is Nothing Then
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveCITESNotification)
            Else
                If Created And Not NewCA Is Nothing Then
                    Me.mAccommodationAndCareId = NewCA.Id
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
                If NewCA.CheckSum <> CheckSum Then
                    Me.InitialiseCA(NewCA, tran)
                End If
            End If
            Return Me
        End Function

        Private Function ChangeStatus(ByVal authorisedUserId As Int64, ByVal tran As SqlClient.SqlTransaction) As Boolean
            ' Return True
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



    End Class

End Namespace
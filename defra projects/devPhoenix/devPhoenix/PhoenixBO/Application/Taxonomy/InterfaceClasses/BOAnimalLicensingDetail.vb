Imports uk.gov.defra.Phoenix.DO.DataObjects
Imports uk.gov.defra.Phoenix.BO.Payments

Namespace Taxonomy
    <Serializable()> _
    Public Class BOAnimalLicensingDetail
        Inherits PaymentsBaseBO
        Implements IAnimalLicensingDetail

#Region " Prelim code "
        Public Sub New()
            'This constructor is intentionally empty.
        End Sub

        Public Sub New(ByVal id As Int32)
            Dim service As New Service.AnimalLicensingDetailService
            Dim result As Entity.AnimalLicensingDetail = service.GetById(id)
            If result Is Nothing Then
                Throw New RecordDoesNotExist("AnimalLicensingDetail", id)
            Else
                InitialiseDetail(result)
            End If
        End Sub

        Private Sub InitialiseDetail(ByVal detail As Entity.AnimalLicensingDetail)
            With detail
                Me.mAnimalLicensingId         = .AnimalLicensingID
                Me.mAverageLifespan           = .AverageLifespan
                Me.mAverageNumberOfOffspring  = .AverageNumberOfOffspring
                Me.mBirdFeeLevel              = .BirdFeeLevel
                Me.mCheckSum                  = .CheckSum
                Me.mIncubationOrGestationDays = .IncubationOrGestationDays
                Me.mKingdomId                 = .KingdomID
                Me.mMinimumMicrochipAge       = .MinimumMicrochipAge
                Me.mMinimumMicrochipSize      = .MinimumMicrochipSize
                Me.mOldestAcceptedAge         = .OldestAcceptedAge
                Me.mSexualMaturityAge         = .SexualMaturityAge
                Me.mTaxonId                   = .TaxonID
                Me.mTaxonTypeId               = .TaxonTypeID
            End With
        End Sub

#End Region

#Region " Properties "
        Public Property AnimalLicensingId As Int32 Implements IAnimalLicensingDetail.AnimalLicensingId
            Get
                Return mAnimalLicensingId
            End Get
            Set
                mAnimalLicensingId = Value
            End Set
        End Property
        Private mAnimalLicensingId As Int32

        Public Property KingdomId As Int32 Implements IAnimalLicensingDetail.KingdomId
            Get
                Return mKingdomId
            End Get
            Set
                mKingdomId = Value
            End Set
        End Property
        Private mKingdomId As Int32

        Public Property TaxonId As Int32 Implements IAnimalLicensingDetail.TaxonId
            Get
                Return mTaxonId
            End Get
            Set
                mTaxonId = Value
            End Set
        End Property
        Private mTaxonId As Int32

        Public Property TaxonTypeId As Int32 Implements IAnimalLicensingDetail.TaxonTypeId
            Get
                Return mTaxonTypeId
            End Get
            Set
                mTaxonTypeId = Value
            End Set
        End Property
        Private mTaxonTypeId As Int32

        Public Property BirdFeeLevel As Int32 Implements IAnimalLicensingDetail.BirdFeeLevel
            Get
                Return mBirdFeeLevel
            End Get
            Set
                mBirdFeeLevel = Value
            End Set
        End Property
        Private mBirdFeeLevel As Int32

        Public Property IncubationOrGestationDays As Int32 Implements IAnimalLicensingDetail.IncubationOrGestationDays
            Get
                Return mIncubationOrGestationDays
            End Get
            Set
                mIncubationOrGestationDays = Value
            End Set
        End Property
        Private mIncubationOrGestationDays As Int32

        Public Property AverageNumberOfOffspring As Int32 Implements IAnimalLicensingDetail.AverageNumberOfOffspring
            Get
                Return mAverageNumberOfOffspring
            End Get
            Set
                mAverageNumberOfOffspring = Value
            End Set
        End Property
        Private mAverageNumberOfOffspring As Int32

        Public Property MinimumMicrochipSize As Int32 Implements IAnimalLicensingDetail.MinimumMicrochipSize
            Get
                Return mMinimumMicrochipSize
            End Get
            Set
                mMinimumMicrochipSize = Value
            End Set
        End Property
        Private mMinimumMicrochipSize As Int32

        Public Property MinimumMicrochipAge As Int32 Implements IAnimalLicensingDetail.MinimumMicrochipAge
            Get
                Return mMinimumMicrochipAge
            End Get
            Set
                mMinimumMicrochipAge = Value
            End Set
        End Property
        Private mMinimumMicrochipAge As Int32

        Public Property AverageLifespan As Int32 Implements IAnimalLicensingDetail.AverageLifespan
            Get
                Return mAverageLifespan
            End Get
            Set
                mAverageLifespan = Value
            End Set
        End Property
        Private mAverageLifespan As Int32

        Public Property OldestAcceptedAge As Int32 Implements IAnimalLicensingDetail.OldestAcceptedAge
            Get
                Return mOldestAcceptedAge
            End Get
            Set
                mOldestAcceptedAge = Value
            End Set
        End Property
        Private mOldestAcceptedAge As Int32

        Public Property SexualMaturityAge As Int32 Implements IAnimalLicensingDetail.SexualMaturityAge
            Get
                Return mSexualMaturityAge
            End Get
            Set
                mSexualMaturityAge = Value
            End Set
        End Property
        Private mSexualMaturityAge As Int32

#End Region

#Region " Save "
        Public Overrides Overloads Function Save() As BaseBO
            Dim service As New Service.AnimalLicensingDetailService
            Dim tran As SqlClient.SqlTransaction = service.BeginTransaction
            Dim result As BaseBO = MyClass.Save(tran)

            If result Is Nothing Then
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
            Else
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Commit)
            End If
            Return result
        End Function

        Public Shadows Function Delete() As Boolean
            Dim service As New Service.AnimalLicensingDetailService
            Return service.DeleteById(mAnimalLicensingId, Checksum)
        End Function

        Public Overloads Function Save(ByVal tran As SqlClient.SqlTransaction) As BaseBO
            Dim detail As New Entity.AnimalLicensingDetail
            Dim service As service.AnimalLicensingDetailService = detail.ServiceObject
            Dim created As Boolean = mAnimalLicensingId = 0

            If created Then
                detail = service.Insert(mKingdomId, _
                                      mTaxonId, _
                                      mTaxonTypeId, _
                                      mBirdFeeLevel, _
                                      mIncubationOrGestationDays, _
                                      mAverageNumberOfOffspring, _
                                      mMinimumMicrochipSize, _
                                      mMinimumMicrochipAge, _
                                      mAverageLifespan, _
                                      mOldestAcceptedAge, _
                                      mSexualMaturityAge, _
                                      tran)
            Else
                detail = service.Update(mAnimalLicensingId, _
                                      mKingdomId, _
                                      mTaxonId, _
                                      mTaxonTypeId, _
                                      mBirdFeeLevel, _
                                      mIncubationOrGestationDays, _
                                      mAverageNumberOfOffspring, _
                                      mMinimumMicrochipSize, _
                                      mMinimumMicrochipAge, _
                                      mAverageLifespan, _
                                      mOldestAcceptedAge, _
                                      mSexualMaturityAge, _
                                      mChecksum, _
                                      tran)
            End If
            If detail Is Nothing Then
                CheckSqlErrors("Animal Licensing Detail", tran, service)
            Else
                If created And Not detail Is Nothing Then
                    mAnimalLicensingId = detail.Id
                End If
                If detail.CheckSum <> CheckSum Then
                     InitialiseDetail(detail)
                End If
            End If
            Return Me
        End Function

#End Region


    End Class
End Namespace
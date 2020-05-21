Namespace Taxonomy
    Public Class BOAnimalLicensingDisplay
        Implements IAnimalLicensingDisplay

#Region " Prelim code "
        Public Sub New()
            'This constructor is intentionally empty.
        End Sub

        Public Sub New(ByVal AnimalLicenseDetailID As Int32, ByVal tran As SqlClient.SqlTransaction)
            LoadObject(AnimalLicenseDetailID, tran)
        End Sub

        Public Sub New(ByVal AnimalLicenseDetailID As Int32)
            LoadObject(AnimalLicenseDetailID, Nothing)
        End Sub

        Public Sub New(ByVal ALD As [DO].DataObjects.Entity.AnimalLicensingDetail)
            InitialiseMe(ALD, Nothing)
        End Sub

        Private Overloads Function LoadObject(ByVal id As Int32) As DataObjects.Entity.AnimalLicensingDetail
            Return Me.LoadObject(id, Nothing)
        End Function

        Private Overloads Function LoadObject(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.AnimalLicensingDetail
            Dim NewDO As DataObjects.Entity.AnimalLicensingDetail = _
                DataObjects.Entity.AnimalLicensingDetail.GetById(id, tran)
            If NewDO Is Nothing Then
                Throw New RecordDoesNotExist("AnimalLicensingDetail", id)
            Else
                InitialiseMe(NewDO, tran)
                Return NewDO
            End If
        End Function

        Protected Overridable Sub InitialiseMe(ByVal NewDO As DataObjects.Entity.AnimalLicensingDetail, ByVal tran As SqlClient.SqlTransaction)
            With NewDO
                Me.ID = .Id
                Me.AverageLifespan = .AverageLifespan
                Me.AverageNumberOfOffspring = .AverageNumberOfOffspring
                Me.BirdFeeLevel = .BirdFeeLevel
                Me.IncubationOrGestationDays = .IncubationOrGestationDays
                Me.MinimumMicrochipAge = .MinimumMicrochipAge
                Me.MinimumMicrochipSize = .MinimumMicrochipSize
                Me.OldestAcceptedAge = .OldestAcceptedAge
                Me.SexualMaturityAge = .SexualMaturityAge
            End With
        End Sub

#End Region
        Public Property ID() As Int32 Implements IAnimalLicensingDisplay.ID
            Get
                Return mID
            End Get
            Set(ByVal Value As Int32)
                mID = Value
            End Set
        End Property
        Private mID As Int32

        Public Property AverageLifespan() As Integer Implements IAnimalLicensingDisplay.AverageLifespan
            Get
                Return mAverageLifespan
            End Get
            Set(ByVal Value As Integer)
                mAverageLifespan = Value
            End Set
        End Property
        Private mAverageLifespan As Int32

        Public Property AverageNumberOfOffspring() As Integer Implements IAnimalLicensingDisplay.AverageNumberOfOffspring
            Get
                Return mAverageNumberOfOffspring
            End Get
            Set(ByVal Value As Integer)
                mAverageNumberOfOffspring = Value
            End Set
        End Property
        Private mAverageNumberOfOffspring As Int32

        Public Property BirdFeeLevel() As Integer Implements IAnimalLicensingDisplay.BirdFeeLevel
            Get
                Return mBirdFeeLevel
            End Get
            Set(ByVal Value As Integer)
                mBirdFeeLevel = Value
            End Set
        End Property
        Private mBirdFeeLevel As Int32

        Public Property IncubationOrGestationDays() As Integer Implements IAnimalLicensingDisplay.IncubationOrGestationDays
            Get
                Return mIncubationOrGestationDays
            End Get
            Set(ByVal Value As Integer)
                mIncubationOrGestationDays = Value
            End Set
        End Property
        Private mIncubationOrGestationDays As Int32

        Public Property MinimumMicrochipAge() As Integer Implements IAnimalLicensingDisplay.MinimumMicrochipAge
            Get
                Return mMinimumMicrochipAge
            End Get
            Set(ByVal Value As Integer)
                mMinimumMicrochipAge = Value
            End Set
        End Property
        Private mMinimumMicrochipAge As Int32

        Public Property MinimumMicrochipSize() As Integer Implements IAnimalLicensingDisplay.MinimumMicrochipSize
            Get
                Return mMinimumMicrochipSize
            End Get
            Set(ByVal Value As Integer)
                mMinimumMicrochipSize = Value
            End Set
        End Property
        Private mMinimumMicrochipSize As Int32

        Public Property OldestAcceptedAge() As Integer Implements IAnimalLicensingDisplay.OldestAcceptedAge
            Get
                Return mOldestAcceptedAge
            End Get
            Set(ByVal Value As Integer)
                mOldestAcceptedAge = Value
            End Set
        End Property
        Private mOldestAcceptedAge As Int32

        Public Property SexualMaturityAge() As Integer Implements IAnimalLicensingDisplay.SexualMaturityAge
            Get
                Return mSexualMaturityAge
            End Get
            Set(ByVal Value As Integer)
                mSexualMaturityAge = Value
            End Set
        End Property
        Private mSexualMaturityAge As Int32
    End Class
End Namespace
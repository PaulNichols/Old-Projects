Namespace Application.Bird.Notification
    <Serializable()> _
    Public Class Keeper
        Public Sub New()
        End Sub

        Friend Sub New(ByVal newKeeper As BirdNotificationDataset.NewKeeperRow)
            If Not newKeeper Is Nothing Then
                mKeeperId = newKeeper.KeeperId
                mTransferDate = newKeeper.TransferDate
                If newKeeper.IsTransferMethodNull Then mTransferMethod = Registration.BaseBird.AcquisitionMethodTypes.Unknown Else mTransferMethod = CType(System.Enum.Parse(GetType(Registration.BaseBird.AcquisitionMethodTypes), newKeeper.TransferMethod), Registration.BaseBird.AcquisitionMethodTypes)
                Dim kr As BirdNotificationDataset.KeeperRow() = newKeeper.GetKeeperRows()
                If kr.Length > 0 Then
                    With kr(0)
                        mTitle = .Title
                        mFirstName = .FirstName
                        mSurname = .Surname
                        If .IsBusinessNameNull Then mBusinessName = Nothing Else mBusinessName = .BusinessName
                        mAddressLine1 = .AddressLine1
                        If .IsAddressLine2Null Then mAddressLine2 = Nothing Else mAddressLine2 = .AddressLine2
                        If .IsAddressLine3Null Then mAddressLine3 = Nothing Else mAddressLine3 = .AddressLine3
                        If .IsAddressLine4Null Then mAddressLine4 = Nothing Else mAddressLine4 = .AddressLine4
                        mTown = .Town
                        mPostCode = .PostCode
                    End With
                End If
            End If
        End Sub

        Friend Sub GetData(ByRef birdNotification As BirdNotificationDataset.BirdNotificationRow)
            If Not birdNotification Is Nothing Then
                Dim NotificationDS As BirdNotificationDataset = CType(birdNotification.Table.DataSet, BirdNotificationDataset)

                Dim NewKeeperRow As BirdNotificationDataset.NewKeeperRow = NotificationDS.NewKeeper.NewNewKeeperRow
                NewKeeperRow.BirdNotificationRow = birdNotification
                NewKeeperRow.KeeperId = mKeeperId
                NewKeeperRow.TransferDate = mTransferDate
                If TransferMethod_Helper.Length > 0 Then NewKeeperRow.TransferMethod = TransferMethod_Helper Else NewKeeperRow.SetTransferMethodNull()
                NotificationDS.NewKeeper.AddNewKeeperRow(NewKeeperRow)

                Dim KeeperRow As BirdNotificationDataset.KeeperRow = NotificationDS.Keeper.NewKeeperRow
                With KeeperRow
                    .NewKeeperRow = NewKeeperRow
                    .Title = mTitle
                    .FirstName = mFirstName
                    .Surname = mSurname
                    If Not mBusinessName Is Nothing AndAlso TypeOf mBusinessName Is String Then .BusinessName = mBusinessName Else .SetBusinessNameNull()
                    .AddressLine1 = mAddressLine1
                    If Not mAddressLine2 Is Nothing AndAlso TypeOf mAddressLine2 Is String Then .AddressLine2 = mAddressLine2 Else .SetAddressLine2Null()
                    If Not mAddressLine3 Is Nothing AndAlso TypeOf mAddressLine3 Is String Then .AddressLine3 = mAddressLine3 Else .SetAddressLine3Null()
                    If Not mAddressLine4 Is Nothing AndAlso TypeOf mAddressLine4 Is String Then .AddressLine4 = mAddressLine4 Else .SetAddressLine4Null()
                    .Town = mTown
                    .PostCode = mPostCode
                End With

                NotificationDS.Keeper.AddKeeperRow(KeeperRow)
            End If
        End Sub

        Public Property KeeperId() As Int32
            Get
                Return mKeeperId
            End Get
            Set(ByVal Value As Int32)
                mKeeperId = Value
            End Set
        End Property
        Private mKeeperId As Int32

        Public Property Title() As String
            Get
                Return mTitle
            End Get
            Set(ByVal Value As String)
                mTitle = Value
            End Set
        End Property
        Private mTitle As String = String.Empty

        Public Property FirstName() As String
            Get
                Return mFirstName
            End Get
            Set(ByVal Value As String)
                mFirstName = Value
            End Set
        End Property
        Private mFirstName As String = String.Empty

        Public Property Surname() As String
            Get
                Return mSurname
            End Get
            Set(ByVal Value As String)
                mSurname = Value
            End Set
        End Property
        Private mSurname As String = String.Empty

        'optional
        Public Property BusinessName() As String
            Get
                Return mBusinessName
            End Get
            Set(ByVal Value As String)
                mBusinessName = Value
            End Set
        End Property
        Private mBusinessName As String = String.Empty

        Public Property AddressLine1() As String
            Get
                Return mAddressLine1
            End Get
            Set(ByVal Value As String)
                mAddressLine1 = Value
            End Set
        End Property
        Private mAddressLine1 As String = String.Empty

        'optional
        Public Property AddressLine2() As String
            Get
                Return mAddressLine2
            End Get
            Set(ByVal Value As String)
                mAddressLine2 = Value
            End Set
        End Property
        Private mAddressLine2 As String = String.Empty

        'optional
        Public Property AddressLine3() As String
            Get
                Return mAddressLine3
            End Get
            Set(ByVal Value As String)
                mAddressLine3 = Value
            End Set
        End Property
        Private mAddressLine3 As String = String.Empty

        'optional
        Public Property AddressLine4() As String
            Get
                Return mAddressLine4
            End Get
            Set(ByVal Value As String)
                mAddressLine4 = Value
            End Set
        End Property
        Private mAddressLine4 As String = String.Empty

        Public Property Town() As String
            Get
                Return mTown
            End Get
            Set(ByVal Value As String)
                mTown = Value
            End Set
        End Property
        Private mTown As String = String.Empty

        Public Property PostCode() As String
            Get
                Return mPostCode
            End Get
            Set(ByVal Value As String)
                mPostCode = Value
            End Set
        End Property
        Private mPostCode As String = String.Empty

        Public Property TransferMethod() As Registration.BaseBird.AcquisitionMethodTypes
            Get
                Return mTransferMethod
            End Get
            Set(ByVal Value As Registration.BaseBird.AcquisitionMethodTypes)
                mTransferMethod = Value
            End Set
        End Property
        Private mTransferMethod As Registration.BaseBird.AcquisitionMethodTypes

        Public Property TransferMethod_Helper() As String
            Get
                If mTransferMethod = Registration.BaseBird.AcquisitionMethodTypes.Unknown Then
                    Return String.Empty
                Else
                    Return System.Enum.GetName(GetType(Registration.BaseBird.AcquisitionMethodTypes), mTransferMethod).Replace("[", "").Replace("]", "")
                End If
            End Get
            Set(ByVal Value As String)
                'proxy
            End Set
        End Property

        Public Property TransferDate() As Date
            Get
                Return mTransferDate
            End Get
            Set(Byval Value As Date)
                mTransferDate = Value
            End Set
        End Property
        Private mTransferDate As Date
    End Class
End Namespace

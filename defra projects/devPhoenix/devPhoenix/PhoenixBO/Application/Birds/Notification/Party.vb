Namespace Application.Bird.Notification
    <Serializable()> _
        Public Class Party
        Public Sub New()
        End Sub

        Friend Sub New(ByVal partyId As Int32)
            mPartyId = partyId
        End Sub

        Friend Sub New(ByVal newParty As BirdNotificationDataset.PartyRow)
            If Not newParty Is Nothing Then
                mPartyId = newParty.PartyID
                mTransferDate = newParty.TransferDate
                If newParty.IsTransferMethodNull Then mTransferMethod = Registration.BaseBird.AcquisitionMethodTypes.Unknown Else mTransferMethod = CType(System.Enum.Parse(GetType(Registration.BaseBird.AcquisitionMethodTypes), newParty.TransferMethod), Registration.BaseBird.AcquisitionMethodTypes)
            End If
        End Sub

        Friend Sub GetData(ByRef birdNotification As BirdNotificationDataset.BirdNotificationRow)
            If Not birdNotification Is Nothing Then
                Dim NotificationDS As BirdNotificationDataset = CType(birdNotification.Table.DataSet, BirdNotificationDataset)

                Dim PartyRow As BirdNotificationDataset.PartyRow = NotificationDS.Party.NewPartyRow
                PartyRow.BirdNotificationRow = birdNotification
                PartyRow.PartyID = mPartyId
                PartyRow.TransferDate = mTransferDate
                If TransferMethod_Helper.Length > 0 Then PartyRow.TransferMethod = TransferMethod_Helper Else PartyRow.SetTransferMethodNull()

                NotificationDS.Party.AddPartyRow(PartyRow)
            End If
        End Sub

        Public Property PartyId() As Int32
            Get
                Return mPartyId
            End Get
            Set(ByVal Value As Int32)
                mPartyId = Value
            End Set
        End Property
        Private mPartyId As Int32

        Public Property TransferDate() As Date
            Get
                Return mTransferDate
            End Get
            Set(ByVal Value As Date)
                mTransferDate = Value
            End Set
        End Property
        Private mTransferDate As Date

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

        Public Property TransferMethod() As Registration.BaseBird.AcquisitionMethodTypes
            Get
                Return mTransferMethod
            End Get
            Set(ByVal Value As Registration.BaseBird.AcquisitionMethodTypes)
                mTransferMethod = Value
            End Set
        End Property
        Private mTransferMethod As Registration.BaseBird.AcquisitionMethodTypes
    End Class
End Namespace
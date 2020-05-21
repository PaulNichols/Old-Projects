Namespace Application.Bird.Notification
    <Serializable()> _
    Public Class Fate
        Public Sub New()
        End Sub

        Public Sub New(ByVal fateCode As Int32, ByVal fateDate As Date)
            mFateCode = fateCode
            mFateDate = fateDate
            ''get the fate description
            'Dim SpecFate As DataObjects.Entity.SpecimenFate = DataObjects.Entity.SpecimenFate.GetById(id)
            'If Not SpecFate Is Nothing Then

            'End If
        End Sub

        Friend Sub New(ByVal birdNotificationFate As BirdNotificationDataset.FateRow)
            If Not birdNotificationFate Is Nothing Then
                mFateDate = birdNotificationFate.FateDate
                mFateCode = birdNotificationFate.FateCode
            End If
        End Sub

        Friend Sub GetData(ByRef birdNotification As BirdNotificationDataset.BirdNotificationRow)
            If Not birdNotification Is Nothing Then
                Dim NotificationDS As BirdNotificationDataset = CType(birdNotification.Table.DataSet, BirdNotificationDataset)
                Dim FateRow As BirdNotificationDataset.FateRow = NotificationDS.Fate.NewFateRow
                With FateRow
                    .FateCode = mFateCode
                    If Not mFateDate Is Nothing AndAlso TypeOf mFateDate Is Date Then
                        .FateDate = CType(mFateDate, Date)
                    Else
                        Throw New ArgumentException("Fate Date needs to be set to a valid date")
                    End If
                    .BirdNotificationRow = birdNotification
                End With
                NotificationDS.Fate.AddFateRow(FateRow)
            End If
        End Sub

        Public Property FateDate() As Object
            Get
                Return mFateDate
            End Get
            Set(ByVal Value As Object)
                mFateDate = Value
            End Set
        End Property
        Private mFateDate As Object

        Public Property FateCode() As Int32
            Get
                Return mFateCode
            End Get
            Set(ByVal Value As Int32)
                mFateCode = Value
            End Set
        End Property
        Private mFateCode As Int32

    End Class
End Namespace

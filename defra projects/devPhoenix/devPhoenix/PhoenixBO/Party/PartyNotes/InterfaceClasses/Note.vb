Namespace Party
    Public Class Note
        Inherits BOCommon.BaseNote

        Public Sub New(ByVal data As DataObjects.Entity.Note, ByVal otherId As Int32)
            MyBase.New(data, otherId)
        End Sub

        Public Sub New(ByVal data As DataObjects.Entity.Note)
            MyBase.New(data)
        End Sub

        Public Sub New()
            MyBase.New()
        End Sub

        Public Overrides Function PostSave(ByVal tran As SqlClient.SqlTransaction, ByVal created As Boolean) As Boolean
            If created Then
                Dim NoteJoin As New DataObjects.Entity.PartyNote
                Dim JoinService As DataObjects.Service.PartyNoteService = NoteJoin.ServiceObject
                JoinService.Insert(OtherId, NoteId, tran)
            End If
        End Function

        <Serializable()> _
  Public Class GridNote

            Public Property Subject() As String
                Get
                    Return mSubject
                End Get
                Set(ByVal Value As String)
                    mSubject = Value
                End Set
            End Property
            Private mSubject As String

            Public Property ShortContent() As String
                Get
                    Return mShortContent
                End Get
                Set(ByVal Value As String)
                    mShortContent = Value
                End Set
            End Property
            Private mShortContent As String

            Public Property Important() As Boolean
                Get
                    Return mImportant
                End Get
                Set(ByVal Value As Boolean)
                    mImportant = Value
                End Set
            End Property
            Private mImportant As Boolean

            Public Property CreatedBy() As String
                Get
                    Return mCreatedBy
                End Get
                Set(ByVal Value As String)
                    mCreatedBy = Value
                End Set
            End Property
            Private mCreatedBy As String

            Public Property CreatedDate() As Date
                Get
                    Return mCreatedDate
                End Get
                Set(ByVal Value As Date)
                    mCreatedDate = Value
                End Set
            End Property
            Private mCreatedDate As Date

            Public Property NoteId() As Int32
                Get
                    Return mnoteid
                End Get
                Set(ByVal Value As Int32)
                    mnoteid = Value
                End Set
            End Property
            Private mNoteId As Int32

            Public Sub New()
            End Sub

            Public Sub New(ByVal note As BOCommon.BaseNote)
                With note
                    Me.NoteId = .NoteId
                    Me.ShortContent = .ShortContent
                    Me.Subject = .Subject
                    Me.CreatedDate = .CreatedDate
                    Me.CreatedBy = .CreatedBy
                    Me.Important = .Important
                End With
            End Sub
        End Class
    End Class
End Namespace
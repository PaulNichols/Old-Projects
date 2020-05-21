Namespace BOCommon
    Public MustInherit Class BaseNote
        Inherits BaseBO
        Implements IBaseNote


        Public MustOverride Function PostSave(ByVal tran As SqlClient.SqlTransaction, ByVal created As Boolean) As Boolean

        Public Sub New(ByVal data As DataObjects.Entity.Note, ByVal otherId As Int32)
            MyClass.New(data)
            mOtherId = otherId
        End Sub

        Public Sub New(ByVal data As DataObjects.Entity.Note)
            MyBase.New()
            InitialiseNote(data)
        End Sub

        Public Sub New()
            MyBase.New()
        End Sub

        Public Property Active() As Boolean Implements IBaseNote.Active
            Get
                Return mActive
            End Get
            Set(ByVal Value As Boolean)
                mActive = value
            End Set
        End Property
        Private mActive As Boolean

        Public Property Content() As String Implements IBaseNote.Content
            Get
                Return mContent
            End Get
            Set(ByVal Value As String)
                mContent = Value
            End Set
        End Property
        Private mContent As String

        Public Property CreatedById() As Object Implements IBaseNote.CreatedById
            Get
                Return mCreatedById
            End Get
            Set(ByVal Value As Object)
                mCreatedById = Value
            End Set
        End Property
        Private mCreatedById As Object

        Public Property CreatedDate() As Date Implements IBaseNote.CreatedDate
            Get
                Return mCreatedDate
            End Get
            Set(ByVal Value As Date)
                mCreatedDate = Value
            End Set
        End Property
        Private mCreatedDate As Date

        Public Property Important() As Boolean Implements IBaseNote.Important
            Get
                Return mImportant
            End Get
            Set(ByVal Value As Boolean)
                mImportant = Value
            End Set
        End Property
        Private mImportant As Boolean

        Public Property ShortContent() As String Implements IBaseNote.ShortContent
            Get
                Return mShortContent
            End Get
            Set(ByVal Value As String)
                mShortContent = value
            End Set
        End Property
        Private mShortContent As String

        Public Property IsReadOnly() As Boolean Implements IBaseNote.IsReadOnly
            Get
                Return mIsReadOnly
            End Get
            Set(ByVal Value As Boolean)
                mIsReadOnly = Value
            End Set
        End Property
        Private mIsReadOnly As Boolean

        Public Property ModifiedById() As Object Implements IBaseNote.ModifiedById
            Get
                Return mModifiedById
            End Get
            Set(ByVal Value As Object)
                mModifiedById = Value
            End Set
        End Property
        Private mModifiedById As Object

        Public Property ModifiedDate() As Object Implements IBaseNote.ModifiedDate
            Get
                Return mModifiedDate
            End Get
            Set(ByVal Value As Object)
                mModifiedDate = Value
            End Set
        End Property
        Private mModifiedDate As Object

        Public Property NoteDate() As Date Implements IBaseNote.NoteDate
            Get
                Return mNoteDate
            End Get
            Set(ByVal Value As Date)
                mNoteDate = Value
            End Set
        End Property
        Private mNoteDate As Date

        Public Property NoteId() As Integer Implements IBaseNote.NoteId
            Get
                Return mNoteId
            End Get
            Set(ByVal Value As Integer)
                mNoteId = Value
            End Set
        End Property
        Private mNoteId As Int32

        Public Property Subject() As String Implements IBaseNote.Subject
            Get
                Return mSubject
            End Get
            Set(ByVal Value As String)
                mSubject = Value
            End Set
        End Property
        Private mSubject As String

        Public Property OtherId() As Int32 Implements IBaseNote.OtherId
            Get
                Return mOtherId
            End Get
            Set(ByVal Value As Int32)
                mOtherId = Value
            End Set
        End Property
        Private mOtherId As Int32

#Region " Helper Functions "
        Public Property CreatedBy() As String Implements IBaseNote.CreatedBy
            Get
                If mCreatedBy Is Nothing Then
                    If mCreatedById Is Nothing OrElse _
                       CType(mCreatedById, Int32) = 0 OrElse _
                       mCreatedById.GetType.Equals(Convert.DBNull) Then
                        mCreatedBy = String.Empty
                    Else
                        mCreatedBy = New BOAuthorisedUser(CType(mCreatedById, Int32)).FullName
                    End If
                End If
                Return mCreatedBy
            End Get
            Set(ByVal Value As String)
                mCreatedBy = Value
            End Set
        End Property
        Private mCreatedBy As String

        Public Property ModifiedBy() As String Implements IBaseNote.ModifiedBy
            Get
                If mModifiedBy Is Nothing Then
                    If mModifiedById Is Nothing OrElse _
                       CType(mModifiedById, Int32) = 0 OrElse _
                       mModifiedById.GetType.Equals(Convert.DBNull) Then
                        mModifiedBy = String.Empty
                    Else
                        mModifiedBy = New BOAuthorisedUser(CType(mModifiedById, Int32)).FullName
                    End If
                End If
                Return mModifiedBy
            End Get
            Set(ByVal Value As String)
                mModifiedBy = Value
            End Set
        End Property
        Private mModifiedBy As String

        '        Private ReadOnly Property CreatedDate() As Date
        '            Get
        '                If mCreatedInfo Is Nothing Then
        '                    Return Date.Today
        '                Else
        '                    Return mCreatedInfo.Date
        '                End If
        '            End Get
        '        End Property

        '        Private ReadOnly Property CreatedById() As Object
        '            Get
        '                If mCreatedInfo Is Nothing Then
        '                    Return Nothing
        '                Else
        '                    Return mCreatedInfo.User.AuthorisedUserId
        '                End If
        '            End Get
        '        End Property

        '        Private ReadOnly Property ModifiedDate() As Object
        '            Get
        '                If mModifiedInfo Is Nothing Then
        '                    Return Nothing
        '                Else
        '                    Return mModifiedInfo.Date
        '                End If
        '            End Get
        '        End Property

        '        Private ReadOnly Property ModifiedById() As Object
        '            Get
        '                If mModifiedInfo Is Nothing Then
        '                    Return Nothing
        '                Else
        '                    Return mModifiedInfo.User.AuthorisedUserId
        '                End If
        '            End Get
        '        End Property
#End Region

#Region " Save "
        Public Shadows Function Save() As BOCommon.BaseNote
            Dim service As DataObjects.Service.NoteService = DataObjects.Entity.Note.ServiceObject
            Dim tran As SqlClient.SqlTransaction = service.BeginTransaction
            Return MyClass.Save(True, tran)
        End Function

        Public Shadows Function Save(ByVal tran As SqlClient.SqlTransaction) As BOCommon.BaseNote
            Return MyClass.Save(False, tran)
        End Function

        Private Shadows Function Save(ByVal commitTran As Boolean, ByVal tran As SqlClient.SqlTransaction) As BOCommon.BaseNote
            If mOtherId > 0 Then
                Created = (NoteId = 0)
                Dim NewNote As New DataObjects.Entity.Note
                Dim service As DataObjects.Service.NoteService = NewNote.ServiceObject

                If Created Then
                    NewNote = service.Insert(mNoteDate, _
                                             mContent, _
                                             mIsReadOnly, _
                                             mImportant, _
                                             mSubject, _
                                             ModifiedById, _
                                             Date.Now, _
                                             CreatedById, _
                                              CreatedDate, _
                                              Active, _
                                            tran)
                Else
                    NewNote = service.Update(mNoteId, _
                                             mNoteDate, _
                                             mContent, _
                                             mIsReadOnly, _
                                             mImportant, _
                                             mSubject, _
                                             ModifiedById, _
                                             Date.Now, _
                                             CreatedById, _
                                             CreatedDate, _
                                             Active, _
                                             CheckSum, _
                                             tran)
                End If
                If NewNote Is Nothing Then
                    If commitTran Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveNote)
                Else
                    InitialiseNote(NewNote)
                    PostSave(tran, Created)
                    If commitTran Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Commit)
                End If
            Else
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveNote)
                ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.OtherIdNotInitialised))
            End If
            Return Me
        End Function
#End Region

        Public Shadows Function Delete() As Boolean
            Dim NewNote As New DataObjects.Entity.Note
            Dim service As DataObjects.Service.NoteService = NewNote.ServiceObject
            Dim Deleted As Boolean
            Deleted = service.DeleteById(mNoteId, CheckSum)
            Return Deleted
        End Function

        Protected Sub InitialiseNote(ByVal note As DataObjects.Entity.Note)
            With note
                mNoteId = .Id

                mContent = .Content

                If Not .IsCreatedByNull Then
                    mCreatedById = .CreatedBy
                End If
                mCreatedDate = .CreatedDate
                If Not .IsModifiedByNull Then
                    mModifiedById = .ModifiedBy
                End If
                If Not .IsModifiedDateNull Then
                    mModifiedDate = .ModifiedDate
                End If
                'Try
                '    mCreatedInfo = New Stamp(.CreatedBy, .CreatedDate)
                'Catch ex As Exception
                '    ' no created info - so set an empty object
                '    mCreatedInfo = Nothing
                'End Try
                'Try
                '    mModifiedInfo = New Stamp(.ModifiedBy, .ModifiedDate)
                'Catch ex As Exception
                '    ' no modified info - so set an empty object
                '    mModifiedInfo = Nothing
                'End Try
                mActive = .Active
                mNoteDate = .Date
                mImportant = .Important
                mIsReadOnly = .IsReadOnly
                mSubject = .Subject
                mShortContent = .ShortContent
                CheckSum = .CheckSum
            End With
        End Sub



    End Class
End Namespace


<Serializable()> _
Public Class ErrorManager
    Public Sub New()
    End Sub

    Friend Structure Info
        Public Sub New(ByVal message As String, ByVal stage As Int32, ByVal url As Object, ByVal isWarning As Boolean)
            Me.Message = message
            Me.Stage = stage
            If Not url Is Nothing AndAlso url.ToString.Length > 0 Then Me.URL = url.ToString
            Me.IsWarning = isWarning
        End Sub
        Public Message As String
        Public Stage As Int32
        Public URL As String
        Public IsWarning As Boolean
    End Structure
    'Public Sub New(ByVal errors As BOError(), ByVal titleId As Titles)
    '    Init(errors, titleId)
    'End Sub

    'Public Sub New(ByVal errors As Array, ByVal titleId As Titles)
    '    'Dim errorList As Array
    '    'errors.CopyTo(errorList)
    '    Init(errors, titleId)
    'End Sub

    Friend ReadOnly Property HasErrors() As Boolean
        Get
            If mErrors Is Nothing OrElse mErrors.Length = 0 Then
                Return False
            Else
                Return True
            End If
        End Get
    End Property

    Protected Sub Init(ByVal errors As Array, ByVal titleId As Int32)
        If errors Is Nothing Then
            Erase mErrors
        Else
            For Each ErrorObject As ValidationError In errors
                If mErrors Is Nothing Then
                    ReDim mErrors(0)
                Else
                    ReDim Preserve mErrors(mErrors.GetUpperBound(0) + 1)
                End If
                mErrors(mErrors.GetUpperBound(0)) = ErrorObject
            Next
        End If
        mTitle = GetTitle(titleId)
    End Sub

    Public Property Title() As String
        Get
            Return mTitle
        End Get
        Set(ByVal Value As String)
            mTitle = Value
        End Set
    End Property
    Private mTitle As String

    Public Property Errors() As BOError()
        Get
            Return mErrors
        End Get
        Set(ByVal Value As BOError())
            mErrors = Value
        End Set
    End Property
    Private mErrors As BOError()

    Shared Function GetTitle(ByVal titleId As Int32) As String
        Dim Message As New DataObjects.Service.MessageService
        Dim MessageSet As DataObjects.EntitySet.MessageSet = Message.GetByIndex_TitleId(titleId, True)
        If Not MessageSet Is Nothing AndAlso _
           MessageSet.Count = 1 Then
            Return CType(MessageSet.GetEntity(0), DataObjects.Entity.Message).Description
        Else
            Return Nothing
        End If
    End Function

    Shared Function GetMessage(ByVal messageId As Int32) As String
        Dim Message As New DataObjects.Service.MessageService
        Dim MessageSet As DataObjects.EntitySet.MessageSet = Message.GetByIndex_IndividualMessageId(messageId, True)
        If Not MessageSet Is Nothing AndAlso _
           MessageSet.Count = 1 Then
            Return CType(MessageSet.GetEntity(0), DataObjects.Entity.Message).Description
        Else
            Return Nothing
        End If
    End Function

    'Shared Function GetStage(ByVal id As Int32) As Int32
    '    Dim Message As New DataObjects.Entity.Message(id)
    '    If Message Is Nothing Then
    '        Return Nothing
    '    Else
    '        Return Message.Stage
    '    End If
    'End Function

    Friend Shared Function GetInfo(ByVal id As Int32) As Info
        Dim Message As New DataObjects.Service.MessageService
        Dim MessageSet As DataObjects.EntitySet.MessageSet = Message.GetByIndex_IndividualMessageId(id, True)
        If Not MessageSet Is Nothing AndAlso _
           MessageSet.Count = 1 Then
            Dim MessageEntity As DataObjects.Entity.Message = CType(MessageSet.GetEntity(0), DataObjects.Entity.Message)
            Dim URL As Object
            If Not MessageEntity.IsURLNull Then URL = MessageEntity.URL
            Return New Info(MessageEntity.Description, MessageEntity.Stage, URL, MessageEntity.IsWarning)
        Else
            Return Nothing
        End If
    End Function
End Class

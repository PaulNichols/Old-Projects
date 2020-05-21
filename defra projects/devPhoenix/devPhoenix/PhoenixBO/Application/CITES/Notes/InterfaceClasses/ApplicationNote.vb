Namespace Application.CITES.Applications
    Public Class ApplicationNote
        Inherits BOCommon.BaseNote
        Implements IApplicationNote

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
                Dim NoteJoin As New DataObjects.Entity.ApplicationNote
                Dim JoinService As DataObjects.Service.ApplicationNoteService = NoteJoin.ServiceObject
                JoinService.Insert(OtherId, NoteId, tran)
            End If
        End Function

        Public Shared Function GetLinkedPermitNotes(ByVal noteId As Int32, ByVal appId As Int32) As Int32()
            Dim PermitNotesService As New [DO].DataObjects.Service.PermitNoteService
            Dim PermitNoteSet As [DO].DataObjects.EntitySet.PermitNoteSet = PermitNotesService.GetByIndex_IX_PermitNote_1(noteId)

            Dim ReturnIds As Int32()

            If Not PermitNoteSet Is Nothing AndAlso PermitNoteSet.Entities.Count > 0 Then
                ReDim ReturnIds(PermitNoteSet.Entities.Count - 1)
                Dim i As Int32
                For Each permitnote As DataObjects.Entity.PermitNote In PermitNoteSet.Entities
                    ReturnIds(i) = permitnote.PermitId
                    i += 1
                Next
            End If
            Return ReturnIds
        End Function

    End Class
End Namespace
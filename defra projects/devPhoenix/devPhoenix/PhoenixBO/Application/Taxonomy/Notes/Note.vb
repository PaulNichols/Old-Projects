Namespace Taxonomy
    Public Class TaxonomyNote
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
                Dim NoteJoin As New DataObjects.Entity.TaxonomyNote
                Dim JoinService As DataObjects.Service.TaxonomyNoteService = NoteJoin.ServiceObject
                JoinService.Insert(OtherId, NoteId, tran)
            End If
        End Function


    End Class
End Namespace
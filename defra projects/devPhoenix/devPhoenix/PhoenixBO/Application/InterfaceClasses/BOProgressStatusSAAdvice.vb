Namespace Application.ProgressStatus
    Public Class BOProgressStatusSAAdvice
        Inherits BOProgressStatus

        Public Sub New()
        End Sub

        Public Sub New(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction)
            Dim entity As DataObjects.Entity.ProgressStatusSAAdvice = DataObjects.Entity.ProgressStatusSAAdvice.GetById(id, tran)
            If Not entity Is Nothing Then
                Load(entity.Id, entity.Code, entity.Description)
            End If
        End Sub
    End Class
End Namespace


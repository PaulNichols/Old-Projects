Imports uk.gov.defra.Phoenix.DO.DataObjects

Namespace Payments
    Public Class PaymentsBaseBO
        Inherits BaseBO

#Region " Helper Functions "
        Protected Shared Function InsertIntoLog(ByVal userId As Int64, ByVal info As String) As Boolean
            If userId > 0 Then
                'log the update
                Dim Log As New Entity.SearchLog
                Return Not Log.Insert(userId, _
                                      Nothing, _
                                      info) Is Nothing
            End If
            Return False
        End Function

        Protected Sub CheckSqlErrors(ByVal table As String, ByVal tran As SqlClient.SqlTransaction, ByVal Service As EnterpriseObjects.Service)
            If Not tran Is Nothing Then
                Service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
            End If
            If Sprocs.LastError Is Nothing Then
                Throw New Exception("Cannot save " + table + ": reason unknown")
            Else
                Throw New Exception("Cannot save " + table + ": " + Sprocs.LastError.ErrorMessage)
            End If
        End Sub
#End Region

    End Class
End Namespace
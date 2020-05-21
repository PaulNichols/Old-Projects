Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do
Imports uk.gov.defra.Phoenix.DO.DataObjects

Namespace ReportData
    <Serializable()> Public Class BoReportPrintSequence

        Public Shared Function GetNextReportSequence(ByVal IncrementValue As Int32) As Int32

            ' Clear any errors from stack before we start
            DataObjects.Sprocs.LastError = Nothing

            Dim reportPrintSequence As DataObjects.Entity.ReportPrintSequence
            Dim reportPrintSequenceService As DataObjects.Service.ReportPrintSequenceService = reportPrintSequence.ServiceObject
            Dim tran As SqlClient.SqlTransaction = reportPrintSequenceService.BeginTransaction

            reportPrintSequence = reportPrintSequenceService.GetById(1, tran)
            Dim nextPrintSequence As Int32 = reportPrintSequence.NextPrintSequence
            reportPrintSequenceService.Update(1, nextPrintSequence + IncrementValue)

            ' Do we have any errors?
            If DataObjects.Sprocs.LastError Is Nothing Then

                ' No Errors - So Commit Transaction 
                reportPrintSequenceService.EndTransaction(tran)

                Return nextPrintSequence

            Else
                ' We have Errors - So Rollback Transaction

                'TODO: Use errors collection to check to see if the problem was concurrency
                reportPrintSequenceService.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)

                Return -1
            End If

        End Function

    End Class
End Namespace
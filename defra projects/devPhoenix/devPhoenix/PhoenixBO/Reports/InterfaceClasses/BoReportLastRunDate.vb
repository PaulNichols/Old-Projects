'''<copyright>Defra 2004</copyright>
'''<author>Mark Lines-Davies</author>
'''<summary>
''' Class for storing and retrieving "last run dates" for reports
'''</summary>
'Imports uk.gov.defra.Phoenix.BO
'Imports uk.gov.defra.Phoenix.do
Imports uk.gov.defra.Phoenix.DO.DataObjects

Namespace ReportData
    <Serializable()> Public Class BoReportLastRunDate

        Public Shared Function GetLastRunDate(ByVal screenCode As String) As DateTime
            Dim record As Entity.LastRunDate = GetRecord(screenCode)
            If record Is Nothing Then Return New DateTime(2000, 1, 1)
            Return record.LastRunDate
        End Function

        Public Shared Sub SetLastRunDate(ByVal screenCode As String, ByVal lastRunDate As DateTime)
            Dim record As Entity.LastRunDate = GetRecord(screenCode)
            If record Is Nothing Then
                Entity.LastRunDate.Insert(screenCode, lastRunDate)
            Else
                record.LastRunDate = lastRunDate
                record.SaveChanges()
            End If
        End Sub

        Private Shared Function GetRecord(ByVal screenCode As String) As Entity.LastRunDate
            Dim records As EntitySet.LastRunDateSet = Entity.LastRunDate.GetAll()
            For Each record As Entity.LastRunDate In records
                If record.ScreenCode = screenCode Then
                    Return record
                End If
            Next
            Return Nothing
        End Function
    End Class
End Namespace

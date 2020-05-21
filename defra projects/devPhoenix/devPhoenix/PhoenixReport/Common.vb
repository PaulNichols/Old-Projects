Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared

Public Class Common
    Public Shared Sub SetParameterValue(ByVal report As ReportClass, ByVal name As String, ByVal value As Object)
        report.SetParameterValue(name, value)
    End Sub

    Public Shared Sub SetParameterValue(ByVal report As ReportDocument, ByVal name As String, ByVal value As Object)
        report.SetParameterValue(name, value)
    End Sub
End Class

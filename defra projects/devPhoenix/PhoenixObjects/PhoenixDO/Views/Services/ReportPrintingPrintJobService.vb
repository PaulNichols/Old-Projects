'------------------------------------------------------------------------------
' <autogenerated>
'     This code was generated by a tool.
'     Runtime Version: 1.1.4322.573
'
'     Changes to this file may cause incorrect behavior and will be lost if 
'     the code is regenerated.
' </autogenerated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On


Namespace DataObjects.Views.Service
    
    'Service implementation for table 'vReportPrintingPrintJob'
    '*DO* add your modifications to this file
    Public Class ReportPrintingPrintJobService
        Inherits Base.ReportPrintingPrintJobServiceBase
        Public Shared Function ParseParams(ByVal data As String) As String
            'remove SQL stuff
            Return EnterpriseObjects.Common.ParseSQLText(data, True, True)
        End Function

        Public Shared Function ReportPrintingPrintJob(ByVal reportPrinterId As Int32) As Views.Collection.ReportPrintingPrintJobBoundCollection
            'create a command...
            Dim command As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("dbo.usp_ReportPrintingPrintJob")
            command.CommandType = System.Data.CommandType.StoredProcedure

            'parameters...
            command.Parameters.Add("@ReportPrinterId", System.Data.SqlDbType.Int).Value = ParseParams(reportPrinterId)

            Dim service As Views.service.ReportPrintingPrintJobService
            service = Views.Entity.ReportPrintingPrintJob.ServiceObject
            Dim ReportSet As Views.EntitySet.ReportPrintingPrintJobSet = CType(service.GetEntitySet(command, GetType(Views.EntitySet.ReportPrintingPrintJobSet)), Views.EntitySet.ReportPrintingPrintJobSet)

            command.Dispose()

            Return ReportSet.Entities()

        End Function
    End Class
End Namespace

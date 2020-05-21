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

'ALTER PROCEDURE dbo.usp_SearchCash
'	(
'		@FromDate Datetime,
'		@ToDate Datetime
'	)
'AS
'	SELECT     *
'	FROM         dbo.vSearchCash
'	WHERE     (PaymentDateTime >= @FromDate AND PaymentDateTime <= @ToDate)
'	ORDER BY PaymentDateTime, PaymentReference	 

Namespace DataObjects.Views.Service
    
    'Service implementation for table 'vSearchCash'
    '*DO* add your modifications to this file
    Public Class SearchCashService
        Inherits Base.SearchCashServiceBase

        Public Shared Function GetCashByDateRange(ByVal fromDate As DateTime, ByVal todate As DateTime) As Views.Collection.SearchCashBoundCollection
            'create a command...
            Dim command As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("dbo.usp_SearchCash")
            command.CommandType = System.Data.CommandType.StoredProcedure

            'parameters...
            command.Parameters.Add("@FromDate", System.Data.SqlDbType.DateTime).Value = fromDate
            command.Parameters.Add("@ToDate", System.Data.SqlDbType.DateTime).Value = todate

            Dim service As Views.Service.SearchCashService = Views.Entity.SearchCash.ServiceObject
            Dim cash As Views.EntitySet.SearchCashSet = CType(service.GetEntitySet(command, GetType(Views.EntitySet.SearchCashSet)), Views.EntitySet.SearchCashSet)

            command.Dispose()
            Return cash.Entities()
        End Function
    End Class
End Namespace

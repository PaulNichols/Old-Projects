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

'ALTER PROCEDURE dbo.usp_SearchCheque
'	(
'		@FromDate Datetime,
'		@ToDate Datetime
'	)
'AS
'	SELECT     *
'	FROM         dbo.vSearchCheque
'	WHERE     (PaymentDateTime >= @FromDate AND PaymentDateTime <= @ToDate)
'	ORDER BY PaymentDateTime, PaymentReference	 

Namespace DataObjects.Views.Service
    
    'Service implementation for table 'vSearchCheque'
    '*DO* add your modifications to this file
    Public Class SearchChequeService
        Inherits Base.SearchChequeServiceBase


        Public Shared Function GetChequesByDateRange(ByVal fromDate As DateTime, ByVal todate As DateTime) As Views.Collection.SearchChequeBoundCollection
            'create a command...
            Dim command As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("dbo.usp_SearchCheque")
            command.CommandType = System.Data.CommandType.StoredProcedure

            'parameters...
            command.Parameters.Add("@FromDate", System.Data.SqlDbType.DateTime).Value = fromDate
            command.Parameters.Add("@ToDate", System.Data.SqlDbType.DateTime).Value = todate

            Dim service As Views.Service.SearchChequeService = Views.Entity.SearchCheque.ServiceObject
            Dim cheques As Views.EntitySet.SearchChequeSet = CType(service.GetEntitySet(command, GetType(Views.EntitySet.SearchChequeSet)), Views.EntitySet.SearchChequeSet)

            command.Dispose()
            Return cheques.Entities()
        End Function

    End Class
End Namespace

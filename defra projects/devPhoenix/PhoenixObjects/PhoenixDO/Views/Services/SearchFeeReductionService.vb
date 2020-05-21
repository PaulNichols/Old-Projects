'------------------------------------------------------------------------------
' <autogenerated>
'     This code was generated by a tool.
'     Runtime Version: 1.1.4322.2032
'
'     Changes to this file may cause incorrect behavior and will be lost if 
'     the code is regenerated.
' </autogenerated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

'ALTER PROCEDURE dbo.usp_SearchFeeReduction
'	(
'		@FromDate Datetime,
'		@ToDate Datetime
'	)

'AS

'	SELECT     *
'	FROM         dbo.vSearchFeeReduction
'	WHERE     (ReductionDate >= @FromDate AND ReductionDate <= @ToDate)
'	ORDER BY ReductionDate	

Namespace DataObjects.Views.Service
    
    'Service implementation for table 'vSearchFeeReduction'
    '*DO* add your modifications to this file
    Public Class SearchFeeReductionService
        Inherits Base.SearchFeeReductionServiceBase

        Public Shared Function GetFeeReductionsByDateRange(ByVal fromDate As DateTime, ByVal todate As DateTime) As Views.Collection.SearchFeeReductionBoundCollection
            'create a command...
            Dim command As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("dbo.usp_SearchFeeReduction")
            command.CommandType = System.Data.CommandType.StoredProcedure

            'parameters...
            command.Parameters.Add("@FromDate", System.Data.SqlDbType.DateTime).Value = fromDate
            command.Parameters.Add("@ToDate", System.Data.SqlDbType.DateTime).Value = todate

            Dim service As Views.Service.SearchFeeReductionService = Views.Entity.SearchFeeReduction.ServiceObject
            Dim reductions As Views.EntitySet.SearchFeeReductionSet = CType(service.GetEntitySet(command, GetType(Views.EntitySet.SearchFeeReductionSet)), Views.EntitySet.SearchFeeReductionSet)

            command.Dispose()
            Return reductions.Entities()
        End Function
    End Class
End Namespace

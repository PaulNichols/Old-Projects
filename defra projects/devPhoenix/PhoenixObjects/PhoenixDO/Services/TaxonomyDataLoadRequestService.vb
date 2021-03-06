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


Namespace DataObjects.Service
    
    'Service implementation for table 'TaxonomyDataLoadRequest'
    '*DO* add your modifications to this file
    Public Class TaxonomyDataLoadRequestService
        Inherits Base.TaxonomyDataLoadRequestServiceBase

        Public Shared Function ParseParams(ByVal data As String) As String
            'remove SQL stuff
            Return EnterpriseObjects.Common.ParseSQLText(data, True, True)
        End Function

        Public Overloads Shared Function GetNextToFulfill(ByVal MessageDate As Date) As Entity.TaxonomyDataLoadRequest
            'create a command...
            Dim command As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("usp_SelectTaxonomyDataLoadRequestNextToFulfill")
            command.CommandType = System.Data.CommandType.StoredProcedure

            'parameters...
            command.Parameters.Add("@MessageDate", System.Data.SqlDbType.DateTime).Value = ParseParams(MessageDate)

            Dim service As service.TaxonomyDataLoadRequestService
            service = Entity.TaxonomyDataLoadRequest.ServiceObject
            Dim DataLoadSet As EntitySet.TaxonomyDataLoadRequestSet = CType(service.GetEntitySet(command, GetType(EntitySet.TaxonomyDataLoadRequestSet)), EntitySet.TaxonomyDataLoadRequestSet)

            command.Dispose()

            If DataLoadSet.Entities Is Nothing = False AndAlso DataLoadSet.Entities.Count > 0 Then
                Return DataLoadSet.Entities(0)
            Else
                Return Nothing
            End If
        End Function

        
    End Class
End Namespace

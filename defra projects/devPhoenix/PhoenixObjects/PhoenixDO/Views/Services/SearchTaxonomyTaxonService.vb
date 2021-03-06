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
    
    'Service implementation for table 'vSearchTaxonomyTaxon'
    '*DO* add your modifications to this file
    Public Class SearchTaxonomyTaxonService
        Inherits Base.SearchTaxonomyTaxonServiceBase

        Protected Shared Sub RaiseSqlErrors(ByVal sqlcommand As String, ByVal Service As EnterpriseObjects.Service)
            RaiseSqlErrors(sqlcommand, Service, Nothing)
        End Sub

        Protected Shared Sub RaiseSqlErrors(ByVal sqlcommand As String, ByVal Service As EnterpriseObjects.Service, ByVal tran As SqlClient.SqlTransaction)
            If Not Service.GetLastDBError Is Nothing Then
                If Not tran Is Nothing AndAlso Not Service Is Nothing Then
                    Service.EndTransaction(tran, Service.TransactionEndEnum.Rollback)
                End If
                Throw New Exception("Error with " + sqlcommand + ": " + Service.GetLastDBError.ErrorMessage)
            End If
        End Sub

        Public Shared Function ParseParams(ByVal data As Int32) As String
            Return EnterpriseObjects.Common.ParseSQLText(data, True, True)
        End Function

        Public Shared Function ParseParams(ByVal data As String) As String
            'remove SQL stuff
            Return EnterpriseObjects.Common.ParseSQLText(data, True, True)
        End Function

        Public Shared Function ParseBitParams(ByVal data As Boolean) As SqlTypes.SqlBoolean
            If data = True Then
                Return SqlTypes.SqlBoolean.One
            Else
                Return SqlTypes.SqlBoolean.Zero
            End If
        End Function

#Region " GetTaxaByInfo "

        Public Delegate Function GetTaxaByInfoDelegate(ByVal KingdomID As Int32, ByVal SearchString1 As String, ByVal SearchString2 As String, ByVal Soundex As Boolean) As Views.Collection.SearchTaxonomyTaxonBoundCollection

        Public Shared Function GetTaxaByInfo(ByVal TaxonType As GetTaxaByInfoDelegate, ByVal KingdomID As Int32, ByVal SearchString1 As String, ByVal SearchString2 As String, ByVal Soundex As Boolean) As Views.Collection.SearchTaxonomyTaxonBoundCollection
            Return TaxonType.Invoke(KingdomID, SearchString1, SearchString2, Soundex)
        End Function

        Public Shared Function GetShed4SpeciesTaxaByInfo(ByVal KingdomID As Int32, ByVal SearchString1 As String, ByVal SearchString2 As String, ByVal Soundex As Boolean) As Views.Collection.SearchTaxonomyTaxonBoundCollection
            Return mGetTaxaByInfo("dbo.usp_SearchSpeciesTaxonSched4ByInfo", KingdomID, SearchString1, SearchString2, Soundex)
        End Function

        Public Shared Function GetShed4GenusTaxaByInfo(ByVal KingdomID As Int32, ByVal SearchString1 As String, ByVal SearchString2 As String, ByVal Soundex As Boolean) As Views.Collection.SearchTaxonomyTaxonBoundCollection
            Return mGetTaxaByInfo("dbo.usp_SearchGenusTaxonSched4ByInfo", KingdomID, SearchString1, SearchString2, Soundex)
        End Function

        Public Shared Function GetShed4FamilyTaxaByInfo(ByVal KingdomID As Int32, ByVal SearchString1 As String, ByVal SearchString2 As String, ByVal Soundex As Boolean) As Views.Collection.SearchTaxonomyTaxonBoundCollection
            Return mGetTaxaByInfo("dbo.usp_SearchFamilyTaxonSched4ByInfo", KingdomID, SearchString1, SearchString2, Soundex)
        End Function

        Public Shared Function GetShed4OrderTaxaByInfo(ByVal KingdomID As Int32, ByVal SearchString1 As String, ByVal SearchString2 As String, ByVal Soundex As Boolean) As Views.Collection.SearchTaxonomyTaxonBoundCollection
            Return mGetTaxaByInfo("dbo.usp_SearchOrderTaxonSched4ByInfo", KingdomID, SearchString1, SearchString2, Soundex)
        End Function

        Public Shared Function GetAvesClassSpeciesTaxaByInfo(ByVal KingdomID As Int32, ByVal SearchString1 As String, ByVal SearchString2 As String, ByVal Soundex As Boolean) As Views.Collection.SearchTaxonomyTaxonBoundCollection
            Return mGetTaxaByInfo("dbo.usp_SearchSpeciesTaxonAvesClassByInfo", KingdomID, SearchString1, SearchString2, Soundex)
        End Function

        Public Shared Function GetAvesClassGenusTaxaByInfo(ByVal KingdomID As Int32, ByVal SearchString1 As String, ByVal SearchString2 As String, ByVal Soundex As Boolean) As Views.Collection.SearchTaxonomyTaxonBoundCollection
            Return mGetTaxaByInfo("dbo.usp_SearchGenusTaxonAvesClassByInfo", KingdomID, SearchString1, SearchString2, Soundex)
        End Function

        Public Shared Function GetAvesClassFamilyTaxaByInfo(ByVal KingdomID As Int32, ByVal SearchString1 As String, ByVal SearchString2 As String, ByVal Soundex As Boolean) As Views.Collection.SearchTaxonomyTaxonBoundCollection
            Return mGetTaxaByInfo("dbo.usp_SearchFamilyTaxonAvesClassByInfo", KingdomID, SearchString1, SearchString2, Soundex)
        End Function

        Public Shared Function GetAvesClassOrderTaxaByInfo(ByVal KingdomID As Int32, ByVal SearchString1 As String, ByVal SearchString2 As String, ByVal Soundex As Boolean) As Views.Collection.SearchTaxonomyTaxonBoundCollection
            Return mGetTaxaByInfo("dbo.usp_SearchOrderTaxonAvesClassByInfo", KingdomID, SearchString1, SearchString2, Soundex)
        End Function

        Public Shared Function GetSpeciesTaxaByInfo(ByVal KingdomID As Int32, ByVal SearchString1 As String, ByVal SearchString2 As String, ByVal Soundex As Boolean) As Views.Collection.SearchTaxonomyTaxonBoundCollection
            Return mGetTaxaByInfo("dbo.usp_SearchSpeciesTaxonByInfo", KingdomID, SearchString1, SearchString2, Soundex)
        End Function

        Public Shared Function GetGenusTaxaByInfo(ByVal KingdomID As Int32, ByVal SearchString1 As String, ByVal SearchString2 As String, ByVal Soundex As Boolean) As Views.Collection.SearchTaxonomyTaxonBoundCollection
            Return mGetTaxaByInfo("dbo.usp_SearchGenusTaxonByInfo", KingdomID, SearchString1, SearchString2, Soundex)
        End Function

        Public Shared Function GetFamilyTaxaByInfo(ByVal KingdomID As Int32, ByVal SearchString1 As String, ByVal SearchString2 As String, ByVal Soundex As Boolean) As Views.Collection.SearchTaxonomyTaxonBoundCollection
            Return mGetTaxaByInfo("dbo.usp_SearchFamilyTaxonByInfo", KingdomID, SearchString1, SearchString2, Soundex)
        End Function

        Public Shared Function GetOrderTaxaByInfo(ByVal KingdomID As Int32, ByVal SearchString1 As String, ByVal SearchString2 As String, ByVal Soundex As Boolean) As Views.Collection.SearchTaxonomyTaxonBoundCollection
            Return mGetTaxaByInfo("dbo.usp_SearchOrderTaxonByInfo", KingdomID, SearchString1, SearchString2, Soundex)
        End Function

        Public Shared Function GetClassTaxaByInfo(ByVal KingdomID As Int32, ByVal SearchString1 As String, ByVal SearchString2 As String, ByVal Soundex As Boolean) As Views.Collection.SearchTaxonomyTaxonBoundCollection
            Return mGetTaxaByInfo("dbo.usp_SearchClassTaxonByInfo", KingdomID, SearchString1, SearchString2, Soundex)
        End Function

        Public Shared Function GetPhylumTaxaByInfo(ByVal KingdomID As Int32, ByVal SearchString1 As String, ByVal SearchString2 As String, ByVal Soundex As Boolean) As Views.Collection.SearchTaxonomyTaxonBoundCollection
            Return mGetTaxaByInfo("dbo.usp_SearchPhylumTaxonByInfo", KingdomID, SearchString1, SearchString2, Soundex)
        End Function

        Private Shared Function mGetTaxaByInfo(ByVal SQLCommand As String, ByVal KingdomID As Int32, ByVal SearchString1 As String, ByVal SearchString2 As String, ByVal Soundex As Boolean) As Views.Collection.SearchTaxonomyTaxonBoundCollection
            'create a command...
            Dim command As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand(SQLCommand)
            command.CommandType = System.Data.CommandType.StoredProcedure
            Dim FormatedSearchString1 As String
            'parameters...
            command.Parameters.Add("@KingdomID", System.Data.SqlDbType.Int).Value = ParseParams(KingdomID)
            If Not SearchString1 Is Nothing AndAlso SearchString1.Length > 0 Then
                If Soundex = True Then
                    FormatedSearchString1 = ParseParams(SearchString1)
                Else
                    FormatedSearchString1 = ParseParams(PendWildcards(SearchString1))
                End If
                command.Parameters.Add("@SearchString1", System.Data.SqlDbType.VarChar, 100).Value = FormatedSearchString1
            End If
            If Not SearchString2 Is Nothing AndAlso SearchString2.Length > 0 Then
                If Soundex = True Then
                    command.Parameters.Add("@SearchString2", System.Data.SqlDbType.VarChar, 100).Value = ParseParams(SearchString2)
                Else
                    command.Parameters.Add("@SearchString2", System.Data.SqlDbType.VarChar, 100).Value = ParseParams(PendWildcards(SearchString2))
                End If
            Else
                command.Parameters.Add("@SearchString2", System.Data.SqlDbType.VarChar, 100).Value = FormatedSearchString1
            End If
            command.Parameters.Add("@Soundex", System.Data.SqlDbType.Bit).Value = ParseBitParams(Soundex)

            Dim service As Views.service.SearchTaxonomyTaxonService
            service = Views.Entity.SearchTaxonomyTaxon.ServiceObject
            Dim TaxonSet As Views.EntitySet.SearchTaxonomyTaxonSet = CType(service.GetEntitySet(command, GetType(Views.EntitySet.SearchTaxonomyTaxonSet)), Views.EntitySet.SearchTaxonomyTaxonSet)

            command.Dispose()
            RaiseSqlErrors("mGetTaxaByInfo", service)
            Return TaxonSet.Entities()
        End Function
#End Region

        Public Shared Function GetUsagesByInfo(ByVal KingdomID As Int32, ByVal SearchString1 As String, ByVal SearchString2 As String, ByVal Soundex As Boolean) As Views.Collection.SearchTaxonomyTaxonBoundCollection
            Return mGetTaxaByInfo("dbo.usp_SearchUsageTaxonByInfo", KingdomID, SearchString1, SearchString2, Soundex)
        End Function

        Private Shared Function PendWildcards(ByVal searchstring As String) As String
            Dim workersearchstring As New Text.StringBuilder(searchstring)
            'Check if the beginning of the search string contains a wildcard.
            If searchstring.StartsWith("*") = False _
            And searchstring.StartsWith("?") = False Then
                'The beginning of the search string does not contain any wildcards so add a wildcard.
                workersearchstring.Insert(0, "*")
            End If
            'Check if the end of the search string contains a wildcard.
            If searchstring.EndsWith("*") = False _
            And searchstring.EndsWith("?") = False Then
                'The end of the search string does not contain any wildcards so add a wildcard.
                workersearchstring.Append("*")
            End If
            Return workersearchstring.ToString
        End Function

        Public Shared Function GetHigherTaxaByID(ByVal ID As Int32) As Views.Collection.SearchTaxonomyTaxonBoundCollection
            'create a command...
            Dim command As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("dbo.usp_GetHigherTaxaByID")
            command.CommandType = System.Data.CommandType.StoredProcedure

            'parameters...
            command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = ParseParams(ID)

            Dim service As Views.service.SearchTaxonomyTaxonService
            service = Views.Entity.SearchTaxonomyTaxon.ServiceObject
            Dim TaxonSet As Views.EntitySet.SearchTaxonomyTaxonSet = CType(service.GetEntitySet(command, GetType(Views.EntitySet.SearchTaxonomyTaxonSet)), Views.EntitySet.SearchTaxonomyTaxonSet)

            command.Dispose()
            RaiseSqlErrors("GetHigherTaxaByID", service)
            Return TaxonSet.Entities()

        End Function

        Public Shared Function GetLowerTaxaByID(ByVal ID As Int32, ByVal Deepness As Int32) As Views.Collection.SearchTaxonomyTaxonBoundCollection
            'create a command...
            Dim command As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("dbo.usp_GetLowerTaxaByID")
            command.CommandType = System.Data.CommandType.StoredProcedure

            'parameters...
            command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = ParseParams(ID)
            command.Parameters.Add("@Deepness", System.Data.SqlDbType.Int).Value = ParseParams(Deepness)

            Dim service As Views.service.SearchTaxonomyTaxonService
            service = Views.Entity.SearchTaxonomyTaxon.ServiceObject
            Dim TaxonSet As Views.EntitySet.SearchTaxonomyTaxonSet = CType(service.GetEntitySet(command, GetType(Views.EntitySet.SearchTaxonomyTaxonSet)), Views.EntitySet.SearchTaxonomyTaxonSet)

            command.Dispose()

            RaiseSqlErrors("GetLowerTaxaByID", service)

            Return TaxonSet.Entities()

        End Function

        Public Shared Function GetLowerTaxaByID(ByVal ID As Int32) As Views.Collection.SearchTaxonomyTaxonBoundCollection
            Return GetLowerTaxaByID(ID, 1)
        End Function

        Public Shared Function GetAcceptedTaxaByID(ByVal ID As Int32) As Views.Collection.SearchTaxonomyTaxonBoundCollection
            'create a command...
            Dim command As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("dbo.usp_GetAcceptedTaxaByID")
            command.CommandType = System.Data.CommandType.StoredProcedure

            'parameters...
            command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = ParseParams(ID)

            Dim service As Views.service.SearchTaxonomyTaxonService
            service = Views.Entity.SearchTaxonomyTaxon.ServiceObject
            Dim TaxonSet As Views.EntitySet.SearchTaxonomyTaxonSet = CType(service.GetEntitySet(command, GetType(Views.EntitySet.SearchTaxonomyTaxonSet)), Views.EntitySet.SearchTaxonomyTaxonSet)

            command.Dispose()
            RaiseSqlErrors("GetAcceptedTaxaByID", service)
            Return TaxonSet.Entities()

        End Function


        Public Shared Function GetSynonymsByID(ByVal ID As Int32) As Views.Collection.SearchTaxonomyTaxonBoundCollection
            'create a command...
            Dim command As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("dbo.usp_GetSynonymTaxaByID")
            command.CommandType = System.Data.CommandType.StoredProcedure

            'parameters...
            command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = ParseParams(ID)

            Dim service As Views.service.SearchTaxonomyTaxonService
            service = Views.Entity.SearchTaxonomyTaxon.ServiceObject
            Dim TaxonSet As Views.EntitySet.SearchTaxonomyTaxonSet = CType(service.GetEntitySet(command, GetType(Views.EntitySet.SearchTaxonomyTaxonSet)), Views.EntitySet.SearchTaxonomyTaxonSet)

            command.Dispose()
            RaiseSqlErrors("GetSynonymsByID", service)
            Return TaxonSet.Entities()

        End Function

        Public Shared Function GetStockNamesByTaxon(ByVal KingdomID As Int32, ByVal TaxonID As Int32, ByVal TaxonTypeID As Int32) As Views.Collection.SearchTaxonomyTaxonBoundCollection
            'create a command...
            Dim command As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("dbo.usp_GetStockNameTaxaByTaxon")
            command.CommandType = System.Data.CommandType.StoredProcedure

            'parameters...
            command.Parameters.Add("@OriginalSpeciesKingdomID", System.Data.SqlDbType.Int).Value = ParseParams(KingdomID)
            command.Parameters.Add("@OriginalSpeciesTaxonID", System.Data.SqlDbType.Int).Value = ParseParams(TaxonID)
            command.Parameters.Add("@OriginalSpeciesTaxonTypeID", System.Data.SqlDbType.Int).Value = ParseParams(TaxonTypeID)

            Dim service As Views.service.SearchTaxonomyTaxonService
            service = Views.Entity.SearchTaxonomyTaxon.ServiceObject
            Dim TaxonSet As Views.EntitySet.SearchTaxonomyTaxonSet = CType(service.GetEntitySet(command, GetType(Views.EntitySet.SearchTaxonomyTaxonSet)), Views.EntitySet.SearchTaxonomyTaxonSet)

            command.Dispose()
            RaiseSqlErrors("GetStockNamesByTaxon", service)
            Return TaxonSet.Entities()

        End Function

        Public Shared Function GetTaxaByUsageSched4(ByVal KingdomID As Int32, ByVal LevelOfUseID As Int32, ByVal PartID As Int32, ByVal UsageTypeID As Int32) As Views.Collection.SearchTaxonomyTaxonBoundCollection
            'create a command...
            Dim command As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("dbo.usp_SearchTaxaByUsageSched4")
            command.CommandType = System.Data.CommandType.StoredProcedure

            'parameters...
            command.Parameters.Add("@KingdomID", System.Data.SqlDbType.Int).Value = ParseParams(KingdomID)
            command.Parameters.Add("@LevelOfUseID", System.Data.SqlDbType.Int).Value = ParseParams(LevelOfUseID)
            command.Parameters.Add("@PartID", System.Data.SqlDbType.Int).Value = ParseParams(PartID)
            command.Parameters.Add("@UsageTypeID", System.Data.SqlDbType.Int).Value = ParseParams(UsageTypeID)

            Dim service As Views.service.SearchTaxonomyTaxonService
            service = Views.Entity.SearchTaxonomyTaxon.ServiceObject
            Dim TaxonSet As Views.EntitySet.SearchTaxonomyTaxonSet = CType(service.GetEntitySet(command, GetType(Views.EntitySet.SearchTaxonomyTaxonSet)), Views.EntitySet.SearchTaxonomyTaxonSet)

            command.Dispose()
            RaiseSqlErrors("GetTaxaByUsageSched4", service)
            Return TaxonSet.Entities()
        End Function

        Public Shared Function GetTaxaByUsageAvesClass(ByVal KingdomID As Int32, ByVal LevelOfUseID As Int32, ByVal PartID As Int32, ByVal UsageTypeID As Int32) As Views.Collection.SearchTaxonomyTaxonBoundCollection
            'create a command...
            Dim command As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("dbo.usp_SearchTaxaByUsageAvesClass")
            command.CommandType = System.Data.CommandType.StoredProcedure

            'parameters...
            command.Parameters.Add("@KingdomID", System.Data.SqlDbType.Int).Value = ParseParams(KingdomID)
            command.Parameters.Add("@LevelOfUseID", System.Data.SqlDbType.Int).Value = ParseParams(LevelOfUseID)
            command.Parameters.Add("@PartID", System.Data.SqlDbType.Int).Value = ParseParams(PartID)
            command.Parameters.Add("@UsageTypeID", System.Data.SqlDbType.Int).Value = ParseParams(UsageTypeID)

            Dim service As Views.service.SearchTaxonomyTaxonService
            service = Views.Entity.SearchTaxonomyTaxon.ServiceObject
            Dim TaxonSet As Views.EntitySet.SearchTaxonomyTaxonSet = CType(service.GetEntitySet(command, GetType(Views.EntitySet.SearchTaxonomyTaxonSet)), Views.EntitySet.SearchTaxonomyTaxonSet)

            command.Dispose()
            RaiseSqlErrors("GetTaxaByUsageAvesClass", service)
            Return TaxonSet.Entities()
        End Function

        Public Shared Function GetTaxaByUsage(ByVal KingdomID As Int32, ByVal LevelOfUseID As Int32, ByVal PartID As Int32, ByVal UsageTypeID As Int32) As Views.Collection.SearchTaxonomyTaxonBoundCollection
            'create a command...
            Dim command As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("dbo.usp_SearchTaxaByUsage")
            command.CommandType = System.Data.CommandType.StoredProcedure

            'parameters...
            command.Parameters.Add("@KingdomID", System.Data.SqlDbType.Int).Value = ParseParams(KingdomID)
            command.Parameters.Add("@LevelOfUseID", System.Data.SqlDbType.Int).Value = ParseParams(LevelOfUseID)
            command.Parameters.Add("@PartID", System.Data.SqlDbType.Int).Value = ParseParams(PartID)
            command.Parameters.Add("@UsageTypeID", System.Data.SqlDbType.Int).Value = ParseParams(UsageTypeID)

            Dim service As Views.service.SearchTaxonomyTaxonService
            service = Views.Entity.SearchTaxonomyTaxon.ServiceObject
            Dim TaxonSet As Views.EntitySet.SearchTaxonomyTaxonSet = CType(service.GetEntitySet(command, GetType(Views.EntitySet.SearchTaxonomyTaxonSet)), Views.EntitySet.SearchTaxonomyTaxonSet)

            command.Dispose()
            RaiseSqlErrors("GetTaxaByUsage", service)
            Return TaxonSet.Entities()
        End Function
    End Class
End Namespace

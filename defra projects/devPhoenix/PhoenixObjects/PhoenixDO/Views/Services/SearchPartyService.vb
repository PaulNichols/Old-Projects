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
    
    'Service implementation for table 'vSearchParty'
    '*DO* add your modifications to this file
    Public Class SearchPartyService
        Inherits Base.SearchPartyServiceBase

        Public Enum LoadType
            LoadPerson
            LoadBusiness
            LoadBoth
        End Enum

        Public Enum AuthorisedLoadType
            LoadAll = 0
            LoadAuthorised = 1
            LoadUnauthorised = 2
        End Enum

        Public Shared Function ParseParams(ByVal data As String) As String
            'remove SQL stuff
            Return EnterpriseObjects.Common.ParseSQLText(data, True, True)
        End Function


        Public Shared Function GetPartyByInfo(ByVal housenumber As String, ByVal ISO2 As String, ByVal businessName As String, ByVal surname As String, ByVal forename As String, ByVal address1 As String, ByVal address2 As String, ByVal address3 As String, ByVal address4 As String, ByVal postcode As String, ByVal contactDetail As String, ByVal businessTypeId As Int32, ByVal soundexSearch As Boolean, ByVal loadType As LoadType, ByVal authorisedType As AuthorisedLoadType) As Views.Collection.SearchPartyBoundCollection
            'create a command...
            Dim command As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("dbo.usp_SearchParty")
            command.CommandType = System.Data.CommandType.StoredProcedure

            'parameters...
            If Not surname Is Nothing AndAlso surname.Length > 0 Then
                command.Parameters.Add("@Surname", System.Data.SqlDbType.VarChar, 100).Value = ParseParams(surname)
            End If

            If Not ISO2 Is Nothing AndAlso ISO2.Length > 0 Then
                command.Parameters.Add("@ISO2", System.Data.SqlDbType.VarChar, 100).Value = ParseParams(ISO2)
            End If

            If Not housenumber Is Nothing AndAlso housenumber.Length > 0 Then
                command.Parameters.Add("@HouseNumber", System.Data.SqlDbType.VarChar, 20).Value = ParseParams(housenumber)
            End If

            If Not forename Is Nothing AndAlso forename.Length > 0 Then
                command.Parameters.Add("@Forename", System.Data.SqlDbType.VarChar, 100).Value = ParseParams(forename)
            End If

            If Not address1 Is Nothing AndAlso address1.Length > 0 Then
                command.Parameters.Add("@Address1", System.Data.SqlDbType.VarChar, 200).Value = ParseParams(address1)
            End If

            If Not address2 Is Nothing AndAlso address2.Length > 0 Then
                command.Parameters.Add("@Address2", System.Data.SqlDbType.VarChar, 100).Value = ParseParams(address2)
            End If

            If Not address3 Is Nothing AndAlso address3.Length > 0 Then
                command.Parameters.Add("@Address3", System.Data.SqlDbType.VarChar, 100).Value = ParseParams(address3)
            End If

            If Not address4 Is Nothing AndAlso address4.Length > 0 Then
                command.Parameters.Add("@Address4", System.Data.SqlDbType.VarChar, 100).Value = ParseParams(address4)
            End If

            If Not postcode Is Nothing AndAlso postcode.Length > 0 Then
                command.Parameters.Add("@Postcode", System.Data.SqlDbType.VarChar, 20).Value = ParseParams(postcode)
            End If

            If Not contactDetail Is Nothing AndAlso contactDetail.Length > 0 Then
                command.Parameters.Add("@ContactDetail", System.Data.SqlDbType.VarChar, 100).Value = ParseParams(contactDetail)
            End If

            If Not businessName Is Nothing AndAlso businessName.Length > 0 Then
                command.Parameters.Add("@BusinessName", System.Data.SqlDbType.VarChar, 100).Value = ParseParams(businessName)
            End If

            If businessTypeId > 0 Then
                command.Parameters.Add("@BusinessTypeId", System.Data.SqlDbType.VarChar, 1).Value = ParseParams(businessTypeId.ToString)
            End If

            command.Parameters.Add("@SoundsLike", System.Data.SqlDbType.Bit).Value = soundexSearch

            Select Case authorisedType
                Case AuthorisedLoadType.LoadAll
                    command.Parameters.Add("@Authorised", System.Data.SqlDbType.Int).Value = 0
                Case AuthorisedLoadType.LoadAuthorised
                    command.Parameters.Add("@Authorised", System.Data.SqlDbType.Int).Value = 1
                Case AuthorisedLoadType.LoadUnauthorised
                    command.Parameters.Add("@Authorised", System.Data.SqlDbType.Int).Value = 2
            End Select

            Select Case loadType
                Case loadType.LoadPerson
                    command.Parameters.Add("@IsBusiness", System.Data.SqlDbType.Int).Value = 0
                Case loadType.LoadBusiness
                    command.Parameters.Add("@IsBusiness", System.Data.SqlDbType.Int).Value = 1
                Case loadType.LoadBoth
                    command.Parameters.Add("@IsBusiness", System.Data.SqlDbType.Int).Value = 2
            End Select

            Dim service As Views.service.SearchPartyService
            service = Views.Entity.SearchParty.ServiceObject
            Dim PartySet As Views.EntitySet.SearchPartySet = CType(service.GetEntitySet(command, GetType(Views.EntitySet.SearchPartySet)), Views.EntitySet.SearchPartySet)

            command.Dispose()

            Return PartySet.Entities()
        End Function
    End Class
End Namespace

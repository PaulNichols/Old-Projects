Imports uk.gov.defra.Phoenix.DO.DataObjects

Namespace Party.Search
    Public Class PartySearch

        Shared Function GetPartyByAuthorisedPartyId(ByVal authorisedPartyId As Int32, ByVal searchingUserId As Int64) As BOParty
            Try
                Dim AuthParty As BOParty = BOParty.CreateByAuthorisedPartyId(authorisedPartyId)
                If Not AuthParty Is Nothing Then
                    Return GetPartyByPartyId(AuthParty, searchingUserId)
                Else
                    Return Nothing
                End If
            Catch noRecord As RecordDoesNotExist
                Return Nothing
            Catch ex As Exception
                Throw
            End Try
        End Function

        Shared Function GetPartyByPartyId(ByVal partyId As Int32, ByVal searchingUserId As Int64) As BOParty
            Try
                Return GetPartyByPartyId(New BOParty(partyId), searchingUserId)
            Catch noRecord As RecordDoesNotExist
                Return Nothing
            Catch ex As Exception
                Throw
            End Try
        End Function

        Private Shared Function GetPartyByPartyId(ByVal party As BOParty, ByVal searchingUserId As Int64) As BOParty
            Try
                If Not party Is Nothing Then
                    Dim PartyId As Int32 = party.PartyId
                    If searchingUserId > 0 Then InsertIntoLog(searchingUserId, "Party Id: " & PartyId)
                    If party.IsBusiness Then
                        Dim FoundParty_Business As New BOPartyBusiness(PartyId)
                        Return FoundParty_Business
                    Else
                        Dim FoundParty_Individual As New BOPartyIndividual(PartyId)
                        Return FoundParty_Individual
                    End If
                End If
                Return party
            Catch noRecord As RecordDoesNotExist
                Return Nothing
            Catch ex As Exception
                Throw
            End Try
        End Function

        Shared Function GetPartyByApplicationId(ByVal applicationId As Int32, ByVal searchingUserId As Int64) As Party.BOParty
            Try
                Dim Application As New BO.Application.BOApplication(applicationId, 0)
                If Application.Agent Is Nothing OrElse Application.Agent.Party Is Nothing OrElse Application.Agent.Party.PartyId = 0 Then
                    Return Application.Party.Party
                Else
                    Return Application.Agent.Party
                End If
            Catch ex As RecordDoesNotExist
                Throw ex
            End Try
        End Function

        Shared Function GetPartyByInfo(ByVal searchInfo As SearchParameters, ByVal soundexSearch As Boolean, ByVal searchingUserId As Int64) As Views.Collection.SearchPartyBoundCollection
            If TypeOf searchInfo Is SearchParameters_Both Then
                Return GetPartyByInfo(CType(searchInfo, SearchParameters_Both), soundexSearch, searchingUserId)
            ElseIf TypeOf searchInfo Is SearchParameters_Business Then
                Return GetPartyByInfo(CType(searchInfo, SearchParameters_Business), soundexSearch, searchingUserId)
            Else
                Return GetPartyByInfo(CType(searchInfo, SearchParameters_Individual), soundexSearch, searchingUserId)
            End If
        End Function

        Shared Function GetPartyByInfo(ByVal searchInfo As SearchParameters_Individual, ByVal soundexSearch As Boolean, ByVal searchingUserId As Int64) As Views.Collection.SearchPartyBoundCollection
            With searchInfo
                Dim results As Views.Collection.SearchPartyBoundCollection = uk.gov.defra.Phoenix.DO.DataObjects.Views.Service.SearchPartyService.GetPartyByInfo(.HouseNumber, .ISO2, Nothing, .Surname, .Forename, .Address1, .Address2, .Address3, .Address4, .Postcode, .ContactDetail, 0, soundexSearch, Views.Service.SearchPartyService.LoadType.LoadPerson, .Authorised)
                If Not results Is Nothing AndAlso _
                   results.Count > 0 Then
                    InsertIntoLog(searchingUserId, searchInfo.ToString)
                End If
                Return results
            End With
        End Function

        Shared Function GetPartyByInfo(ByVal searchInfo As SearchParameters_Both, ByVal soundexSearch As Boolean, ByVal searchingUserId As Int64) As Views.Collection.SearchPartyBoundCollection
            With searchInfo
                Dim results As Views.Collection.SearchPartyBoundCollection = uk.gov.defra.Phoenix.DO.DataObjects.Views.Service.SearchPartyService.GetPartyByInfo(.HouseNumber, .ISO2, .Name, Nothing, Nothing, .Address1, .Address2, .Address3, .Address4, .Postcode, .ContactDetail, 0, soundexSearch, Views.Service.SearchPartyService.LoadType.LoadBoth, .Authorised)
                If Not results Is Nothing AndAlso _
                   results.Count > 0 Then
                    InsertIntoLog(searchingUserId, searchInfo.ToString)
                End If
                Return results
            End With
        End Function

        Private Shared Function InsertIntoLog(ByVal userId As Int64, ByVal info As String) As Boolean
            If userId > 0 Then
                'log the update
                Dim Log As New Entity.SearchLog
                Return Not Log.Insert(userId, _
                                      Nothing, _
                                      info) Is Nothing
            Else
                Return False
            End If
        End Function

        Shared Function GetPartyByInfo(ByVal searchInfo As SearchParameters_Business, ByVal soundexSearch As Boolean, ByVal searchingUserId As Int64) As Views.Collection.SearchPartyBoundCollection
            With searchInfo
                Dim results As Views.Collection.SearchPartyBoundCollection = uk.gov.defra.Phoenix.DO.DataObjects.Views.Service.SearchPartyService.GetPartyByInfo(.HouseNumber, .ISO2, .BusinessName, Nothing, Nothing, .Address1, .Address2, .Address3, .Address4, .Postcode, .ContactDetail, .BusinessTypeID, soundexSearch, Views.Service.SearchPartyService.LoadType.LoadBusiness, .Authorised)
                If Not results Is Nothing AndAlso _
                   results.Count > 0 Then
                    InsertIntoLog(searchingUserId, searchInfo.ToString)
                End If
                Return results
            End With
        End Function
    End Class
End Namespace
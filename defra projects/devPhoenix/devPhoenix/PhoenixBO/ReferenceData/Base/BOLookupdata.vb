
Option Strict On

Imports System.reflection

Namespace ReferenceData

    Public Class LookUpData
        Inherits BaseBO

        <Serializable()> _
        Public Enum LookupFilter
            All = 0
            Administer = 1
            JnccOrKew = 2
            None = -1
        End Enum

        Public Enum LookUpDataList
            None
            CITESDerivative = 3
            CITESUnitOfMesurement = 7
            Message = 10
            StandardFee = 17
            DelegationGuideline = 47
            DerogationGuideLine = 48
        End Enum

        Public Shared Function GetFilteredLookups(ByVal includeHyphen As Boolean, ByVal filter As uk.gov.defra.Phoenix.BO.ReferenceData.LookUpData.LookupFilter) As NewLookupTable()
            Dim all() As NewLookupTable = NewLookupTable.GetAll()
            Dim results(all.Length) As NewLookupTable   'one extra element, in case needed for hyphen
            Dim index As Int32 = 0

            If includeHyphen Then
                results(0) = New NewLookupTable         'parameterless constructor creates "hyphen" 
                index = 1
            End If
            For Each lookup As NewLookupTable In all
                If CheckObjectSecurity(CType(lookup.SecurityLevel, LookupFilter), filter) Then
                    results(index) = lookup
                    index += 1
                End If
            Next
            ReDim Preserve results(index - 1)
            Array.Sort(results, New LookupComparer)
            Return results
        End Function

        Private Shared Function CheckObjectSecurity(ByVal intObjectSecurity As LookupFilter, ByVal intSecurity As LookupFilter) As Boolean
            Select Case intSecurity
                Case LookupFilter.None
                    Return False
                Case LookupFilter.All
                    Return True
                Case LookupFilter.Administer
                    Return intObjectSecurity = LookupFilter.Administer Or intObjectSecurity = LookupFilter.JnccOrKew
                Case LookupFilter.JnccOrKew
                    Return intObjectSecurity = LookupFilter.JnccOrKew
            End Select
        End Function

        Public Shared Function GetContactGroupText() As String
            Dim service As New DataObjects.Service.ContactTypeGroupService
            Dim ContactGroups As [DO].DataObjects.Collection.ContactTypeGroupBoundCollection
            Dim sResult As String
            ContactGroups = service.GetAll(False, True).Entities
            Dim Group As [DO].DataObjects.Entity.ContactTypeGroup
            For Each Group In ContactGroups
                sResult = sResult & Group.Id & "-" & Group.GroupName & ", "
            Next
            sResult = sResult.Remove(sResult.Length - 2, 2)
            Return sResult
        End Function

        Public Shared Function DeleteInActiveItems(ByVal id As Int32) As Integer
            Try
                Dim lookup As New NewLookupTable(id)
                Dim tableName As String = lookup.TableName
                Return Integer.Parse(DataObjects.Sprocs.USP_DeleteInActiveRefData(tableName, Nothing, GetType(System.Data.DataSet)).Tables(0).Rows(0)("retVal").ToString)
            Catch ex As NullReferenceException
                'occurs when lookup isn't instantiated
                Return 0
            Catch ex As FormatException
                'occurs if the sproc doesn't return an integer
                Return 0
            End Try
        End Function

        Public Shared Function GetContactGroupTypeList(ByVal blnIncludeHyphen As Boolean) As DataObjects.Collection.ContactTypeGroupBoundCollection
            Dim objService As New DataObjects.Service.ContactTypeGroupService
            Return objService.GetAll(blnIncludeHyphen, True).Entities
        End Function

        Public Shared Function GetCountryList(ByVal blnIncludeHyphen As Boolean) As DataObjects.Collection.CountryBoundCollection
            Dim objService As New DataObjects.Service.CountryService
            Return objService.GetAll(blnIncludeHyphen, True).Entities
        End Function

        Public Shared Function GetApplicationTypeCodes() As DataObjects.Collection.ApplicationTypeBoundCollection
            Return GetApplicationTypeCodes(False)
        End Function

        Public Shared Function GetApplicationTypeCodes(ByVal blnIncludeHyphen As Boolean) As DataObjects.Collection.ApplicationTypeBoundCollection
            Dim objservice As New DataObjects.Service.ApplicationTypeService
            Return objservice.GetAll(blnIncludeHyphen, True).Entities
        End Function

        Public Shared Function GetApplicationTypeCodeFromID(ByVal id As Int32) As String
            Return DataObjects.Entity.ApplicationType.GetById(id).Code
        End Function


        Private Class LookupComparer
            Implements IComparer

            Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
                Dim xlook As NewLookupTable = CType(x, NewLookupTable)
                Dim ylook As NewLookupTable = CType(y, NewLookupTable)
                Return String.Compare(xlook.TableDescription, ylook.TableDescription)
            End Function
        End Class
    End Class
End Namespace

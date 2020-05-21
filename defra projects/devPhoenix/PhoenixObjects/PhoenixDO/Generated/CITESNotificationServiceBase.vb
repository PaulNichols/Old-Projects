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


Namespace DataObjects.Base
    
    'Service base implementation for table 'CITESNotification'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class CITESNotificationServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return true
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.CITESNotificationSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.CITESNotificationSet
            Return CType(MyBase.GetAll("eosp_SelectCITESNotification", GetType(EntitySet.CITESNotificationSet), includeHyphen, includeInactive, orderBy),EntitySet.CITESNotificationSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.CITESNotificationSet
            Return Me.GetAll(includeHyphen, includeInactive, CITESNotificationServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, CITESNotificationServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal orderBy As OrderBy) As EntitySet.CITESNotificationSet
            Return Me.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal cITESNotificationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectCITESNotification", "CITESNotificationId", cITESNotificationId, GetType(EntitySet.CITESNotificationSet), tran),Entity.CITESNotification)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal cITESNotificationId As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(cITESNotificationId, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal cITESNotificationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.CITESNotification
            Return CType(MyBase.GetById("eosp_SelectCITESNotification", "CITESNotificationId", cITESNotificationId, GetType(EntitySet.CITESNotificationSet), tran),Entity.CITESNotification)
        End Function
        
        Public Overloads Function GetById(ByVal cITESNotificationId As Integer) As Entity.CITESNotification
            Return Me.GetById(cITESNotificationId, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal cITESNotificationId As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(cITESNotificationId, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal cITESNotificationId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeleteCITESNotification", "CITESNotificationId", cITESNotificationId, checkSum, transaction)
        End Function
        
        'GetForMemberStateOfImportIdCountry - links to the Country table...
        Public Overloads Function GetForMemberStateOfImportIdCountry(ByVal MemberStateOfImportId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESNotificationSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from CITESNotification where MemberState"& _ 
"OfImportId=" + MemberStateOfImportId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.CITESNotificationSet), tran),EntitySet.CITESNotificationSet)
        End Function
        
        'GetForMemberStateOfImportIdCountry - links to the Country table...
        Public Overloads Function GetForMemberStateOfImportIdCountry(ByVal MemberStateOfImportId As Integer) As EntitySet.CITESNotificationSet
            Return Me.GetForMemberStateOfImportIdCountry(MemberStateOfImportId, Nothing)
        End Function
        
        'GetForCountryOfOriginIdCountry - links to the Country table...
        Public Overloads Function GetForCountryOfOriginIdCountry(ByVal CountryOfOriginId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESNotificationSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from CITESNotification where CountryOfOr"& _ 
"iginId=" + CountryOfOriginId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.CITESNotificationSet), tran),EntitySet.CITESNotificationSet)
        End Function
        
        'GetForCountryOfOriginIdCountry - links to the Country table...
        Public Overloads Function GetForCountryOfOriginIdCountry(ByVal CountryOfOriginId As Integer) As EntitySet.CITESNotificationSet
            Return Me.GetForCountryOfOriginIdCountry(CountryOfOriginId, Nothing)
        End Function
        
        'GetForExportedCountryIdCountry - links to the Country table...
        Public Overloads Function GetForExportedCountryIdCountry(ByVal ExportedCountryId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESNotificationSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from CITESNotification where ExportedCou"& _ 
"ntryId=" + ExportedCountryId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.CITESNotificationSet), tran),EntitySet.CITESNotificationSet)
        End Function
        
        'GetForExportedCountryIdCountry - links to the Country table...
        Public Overloads Function GetForExportedCountryIdCountry(ByVal ExportedCountryId As Integer) As EntitySet.CITESNotificationSet
            Return Me.GetForExportedCountryIdCountry(ExportedCountryId, Nothing)
        End Function
        
        'GetForAgentPartyLinkIdPartyLink - links to the PartyLink table...
        Public Overloads Function GetForAgentPartyLinkIdPartyLink(ByVal AgentPartyLinkId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESNotificationSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from CITESNotification where AgentPartyL"& _ 
"inkId=" + AgentPartyLinkId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.CITESNotificationSet), tran),EntitySet.CITESNotificationSet)
        End Function
        
        'GetForAgentPartyLinkIdPartyLink - links to the PartyLink table...
        Public Overloads Function GetForAgentPartyLinkIdPartyLink(ByVal AgentPartyLinkId As Integer) As EntitySet.CITESNotificationSet
            Return Me.GetForAgentPartyLinkIdPartyLink(AgentPartyLinkId, Nothing)
        End Function
        
        'GetForPartyLinkIdPartyLink - links to the PartyLink table...
        Public Overloads Function GetForPartyLinkIdPartyLink(ByVal PartyLinkId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESNotificationSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from CITESNotification where PartyLinkId"& _ 
"=" + PartyLinkId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.CITESNotificationSet), tran),EntitySet.CITESNotificationSet)
        End Function
        
        'GetForPartyLinkIdPartyLink - links to the PartyLink table...
        Public Overloads Function GetForPartyLinkIdPartyLink(ByVal PartyLinkId As Integer) As EntitySet.CITESNotificationSet
            Return Me.GetForPartyLinkIdPartyLink(PartyLinkId, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal memberStateOfImportId As Object, ByVal dateOfImport As Date, ByVal countryOfOriginId As Object, ByVal exportedCountryId As Object, ByVal agentPartyLinkId As Object, ByVal active As Boolean, ByVal validated As Boolean, ByVal partyLinkId As Object, ByVal referenceNumber As Object, ByVal dateUnknown As Object, ByVal receivedDate As Object, ByVal unknownCountryOfExport As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.CITESNotification
            Return Me.GetById(Sprocs.eosp_CreateCITESNotification(memberStateOfImportId, dateOfImport, countryOfOriginId, exportedCountryId, agentPartyLinkId, active, validated, partyLinkId, referenceNumber, dateUnknown, receivedDate, unknownCountryOfExport, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal memberStateOfImportId As Object, ByVal dateOfImport As Date, ByVal countryOfOriginId As Object, ByVal exportedCountryId As Object, ByVal agentPartyLinkId As Object, ByVal active As Boolean, ByVal validated As Boolean, ByVal partyLinkId As Object, ByVal referenceNumber As Object, ByVal dateUnknown As Object, ByVal receivedDate As Object, ByVal unknownCountryOfExport As Object) As Entity.CITESNotification
            Return Me.Insert(memberStateOfImportId, dateOfImport, countryOfOriginId, exportedCountryId, agentPartyLinkId, active, validated, partyLinkId, referenceNumber, dateUnknown, receivedDate, unknownCountryOfExport, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal cITESNotification As Entity.CITESNotification) As Entity.CITESNotification
            Return Me.Insert(cITESNotification(1), cITESNotification(2), cITESNotification(3), cITESNotification(4), cITESNotification(5), cITESNotification(6), cITESNotification(7), cITESNotification(8), cITESNotification(9), cITESNotification(10), cITESNotification(11), cITESNotification(12))
        End Function
        
        Public Overloads Function Insert(ByVal cITESNotification As Entity.CITESNotification, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.CITESNotification
            Return Me.Insert(cITESNotification(1), cITESNotification(2), cITESNotification(3), cITESNotification(4), cITESNotification(5), cITESNotification(6), cITESNotification(7), cITESNotification(8), cITESNotification(9), cITESNotification(10), cITESNotification(11), cITESNotification(12), transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal memberStateOfImportId As Object, ByVal dateOfImport As Date, ByVal countryOfOriginId As Object, ByVal exportedCountryId As Object, ByVal agentPartyLinkId As Object, ByVal active As Boolean, ByVal validated As Boolean, ByVal partyLinkId As Object, ByVal referenceNumber As Object, ByVal dateUnknown As Object, ByVal receivedDate As Object, ByVal unknownCountryOfExport As Object, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.CITESNotification
            Return Sprocs.eosp_UpdateCITESNotification(id, memberStateOfImportId, dateOfImport, countryOfOriginId, exportedCountryId, agentPartyLinkId, active, validated, partyLinkId, referenceNumber, dateUnknown, receivedDate, unknownCountryOfExport, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal memberStateOfImportId As Object, ByVal dateOfImport As Date, ByVal countryOfOriginId As Object, ByVal exportedCountryId As Object, ByVal agentPartyLinkId As Object, ByVal active As Boolean, ByVal validated As Boolean, ByVal partyLinkId As Object, ByVal referenceNumber As Object, ByVal dateUnknown As Object, ByVal receivedDate As Object, ByVal unknownCountryOfExport As Object) As Entity.CITESNotification
            Return Me.Update(id, memberStateOfImportId, dateOfImport, countryOfOriginId, exportedCountryId, agentPartyLinkId, active, validated, partyLinkId, referenceNumber, dateUnknown, receivedDate, unknownCountryOfExport, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal memberStateOfImportId As Object, ByVal dateOfImport As Date, ByVal countryOfOriginId As Object, ByVal exportedCountryId As Object, ByVal agentPartyLinkId As Object, ByVal active As Boolean, ByVal validated As Boolean, ByVal partyLinkId As Object, ByVal referenceNumber As Object, ByVal dateUnknown As Object, ByVal receivedDate As Object, ByVal unknownCountryOfExport As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.CITESNotification
            Return Me.Update(id, memberStateOfImportId, dateOfImport, countryOfOriginId, exportedCountryId, agentPartyLinkId, active, validated, partyLinkId, referenceNumber, dateUnknown, receivedDate, unknownCountryOfExport, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal memberStateOfImportId As Object, ByVal dateOfImport As Date, ByVal countryOfOriginId As Object, ByVal exportedCountryId As Object, ByVal agentPartyLinkId As Object, ByVal active As Boolean, ByVal validated As Boolean, ByVal partyLinkId As Object, ByVal referenceNumber As Object, ByVal dateUnknown As Object, ByVal receivedDate As Object, ByVal unknownCountryOfExport As Object, ByVal checkSum As Integer) As Entity.CITESNotification
            Return Me.Update(id, memberStateOfImportId, dateOfImport, countryOfOriginId, exportedCountryId, agentPartyLinkId, active, validated, partyLinkId, referenceNumber, dateUnknown, receivedDate, unknownCountryOfExport, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal cITESNotification As Entity.CITESNotification) As Entity.CITESNotification
            Return Me.Update(cITESNotification.id, cITESNotification(1), cITESNotification(2), cITESNotification(3), cITESNotification(4), cITESNotification(5), cITESNotification(6), cITESNotification(7), cITESNotification(8), cITESNotification(9), cITESNotification(10), cITESNotification(11), cITESNotification(12))
        End Function
        
        Public Overloads Function Update(ByVal cITESNotification As Entity.CITESNotification, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.CITESNotification
            Return Me.Update(cITESNotification.id, cITESNotification(1), cITESNotification(2), cITESNotification(3), cITESNotification(4), cITESNotification(5), cITESNotification(6), cITESNotification(7), cITESNotification(8), cITESNotification(9), cITESNotification(10), cITESNotification(11), cITESNotification(12), transaction)
        End Function
        
        Public Overloads Function Update(ByVal cITESNotification As Entity.CITESNotification, ByVal checkSum As Integer) As Entity.CITESNotification
            Return Me.Update(cITESNotification.id, cITESNotification(1), cITESNotification(2), cITESNotification(3), cITESNotification(4), cITESNotification(5), cITESNotification(6), cITESNotification(7), cITESNotification(8), cITESNotification(9), cITESNotification(10), cITESNotification(11), cITESNotification(12), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal cITESNotification As Entity.CITESNotification, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.CITESNotification
            Return Me.Update(cITESNotification.id, cITESNotification(1), cITESNotification(2), cITESNotification(3), cITESNotification(4), cITESNotification(5), cITESNotification(6), cITESNotification(7), cITESNotification(8), cITESNotification(9), cITESNotification(10), cITESNotification(11), cITESNotification(12), checkSum, transaction)
        End Function
        
        Public Overloads Function GetByIndex_UQ__CITESNotificatio__43D61337(ByVal cITESNotificationId As Integer, ByVal includeInactive As Boolean) As EntitySet.CITESNotificationSet
            Return Sprocs.eosp_SelectCITESNotification(cITESNotificationId:=Nothing, Index_CITESNotificationId:=[cITESNotificationId], Index_ReferenceNumber:=Nothing, Index_PartyLinkId:=Nothing, includeInactive:=includeInactive, transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_UQ__CITESNotificatio__43D61337(ByVal cITESNotificationId As Integer, ByVal includeInactive As Boolean, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESNotificationSet
            Return Sprocs.eosp_SelectCITESNotification(cITESNotificationId:=Nothing, Index_CITESNotificationId:=[cITESNotificationId], Index_ReferenceNumber:=Nothing, Index_PartyLinkId:=Nothing, includeInactive:=includeInactive, sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Overloads Function GetByIndex_IX_CITESNotification_1(ByVal referenceNumber As String, ByVal includeInactive As Boolean) As EntitySet.CITESNotificationSet
            Return Sprocs.eosp_SelectCITESNotification(cITESNotificationId:=Nothing, Index_ReferenceNumber:=[referenceNumber], Index_CITESNotificationId:=Nothing, Index_PartyLinkId:=Nothing, includeInactive:=includeInactive, transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_IX_CITESNotification_1(ByVal referenceNumber As String, ByVal includeInactive As Boolean, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESNotificationSet
            Return Sprocs.eosp_SelectCITESNotification(cITESNotificationId:=Nothing, Index_ReferenceNumber:=[referenceNumber], Index_CITESNotificationId:=Nothing, Index_PartyLinkId:=Nothing, includeInactive:=includeInactive, sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Overloads Function GetByIndex_IX_CITESNotification(ByVal partyLinkId As Integer, ByVal includeInactive As Boolean) As EntitySet.CITESNotificationSet
            Return Sprocs.eosp_SelectCITESNotification(cITESNotificationId:=Nothing, Index_PartyLinkId:=[partyLinkId], Index_CITESNotificationId:=Nothing, Index_ReferenceNumber:=Nothing, includeInactive:=includeInactive, transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_IX_CITESNotification(ByVal partyLinkId As Integer, ByVal includeInactive As Boolean, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESNotificationSet
            Return Sprocs.eosp_SelectCITESNotification(cITESNotificationId:=Nothing, Index_PartyLinkId:=[partyLinkId], Index_CITESNotificationId:=Nothing, Index_ReferenceNumber:=Nothing, includeInactive:=includeInactive, sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
            
            UQ__CITESNotificatio__43D61337
            
            IX_CITESNotification_1
            
            IX_CITESNotification
            
            
        End Enum
    End Class
End Namespace

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
    
    'Service base implementation for table 'PartySpecimen'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class PartySpecimenServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.PartySpecimenSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.PartySpecimenSet
            Return CType(MyBase.GetAll("eosp_SelectPartySpecimen", GetType(EntitySet.PartySpecimenSet), includeHyphen, includeInactive, orderBy),EntitySet.PartySpecimenSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.PartySpecimenSet
            Return Me.GetAll(includeHyphen, includeInactive, PartySpecimenServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, PartySpecimenServiceBase.OrderBy.DefaultOrder)
        End Function
        
        'GetForSpecimen - links to the Specimen table...
        Public Overloads Function GetForSpecimen(ByVal SpecimenId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PartySpecimenSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from PartySpecimen where SpecimenID=" + SpecimenId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.PartySpecimenSet), tran),EntitySet.PartySpecimenSet)
        End Function
        
        'GetForSpecimen - links to the Specimen table...
        Public Overloads Function GetForSpecimen(ByVal SpecimenId As Integer) As EntitySet.PartySpecimenSet
            Return Me.GetForSpecimen(SpecimenId, Nothing)
        End Function
        
        'GetForParty - links to the Party table...
        Public Overloads Function GetForParty(ByVal PartyId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PartySpecimenSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from PartySpecimen where PartyID=" + PartyId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.PartySpecimenSet), tran),EntitySet.PartySpecimenSet)
        End Function
        
        'GetForParty - links to the Party table...
        Public Overloads Function GetForParty(ByVal PartyId As Integer) As EntitySet.PartySpecimenSet
            Return Me.GetForParty(PartyId, Nothing)
        End Function
        
        'GetForAddress - links to the Address table...
        Public Overloads Function GetForAddress(ByVal AddressId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PartySpecimenSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from PartySpecimen where AddressId=" + AddressId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.PartySpecimenSet), tran),EntitySet.PartySpecimenSet)
        End Function
        
        'GetForAddress - links to the Address table...
        Public Overloads Function GetForAddress(ByVal AddressId As Integer) As EntitySet.PartySpecimenSet
            Return Me.GetForAddress(AddressId, Nothing)
        End Function
        
        Public Overloads Sub Insert(ByVal specimenID As Integer, ByVal partyID As Integer, ByVal addressId As Integer, ByVal startDate As Date, ByVal endDate As Object, ByVal roleType As Integer, ByVal partySpecimenStatus As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction)
            Sprocs.eosp_CreatePartySpecimen(specimenID, partyID, addressId, startDate, endDate, roleType, partySpecimenStatus, transaction)
        End Sub
        
        Public Overloads Sub Insert(ByVal specimenID As Integer, ByVal partyID As Integer, ByVal addressId As Integer, ByVal startDate As Date, ByVal endDate As Object, ByVal roleType As Integer, ByVal partySpecimenStatus As Object)
            Me.Insert(specimenID, partyID, addressId, startDate, endDate, roleType, partySpecimenStatus, Nothing)
        End Sub
        
        Public Overloads Sub Insert(ByVal partySpecimen As Entity.PartySpecimen)
            Me.Insert(partySpecimen(0), partySpecimen(1), partySpecimen(2), partySpecimen(3), partySpecimen(4), partySpecimen(5), partySpecimen(6))
        End Sub
        
        Public Overloads Sub Insert(ByVal partySpecimen As Entity.PartySpecimen, ByVal transaction As System.Data.SqlClient.SqlTransaction)
            Me.Insert(partySpecimen(0), partySpecimen(1), partySpecimen(2), partySpecimen(3), partySpecimen(4), partySpecimen(5), partySpecimen(6), transaction)
        End Sub
        
        Public Overloads Sub Update(ByVal specimenID As Integer, ByVal partyID As Integer, ByVal addressId As Integer, ByVal startDate As Date, ByVal endDate As Object, ByVal roleType As Integer, ByVal partySpecimenStatus As Object, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction)
            Sprocs.eosp_UpdatePartySpecimen(specimenID, partyID, addressId, startDate, endDate, roleType, partySpecimenStatus, checkSum, transaction)
        End Sub
        
        Public Overloads Sub Update(ByVal specimenID As Integer, ByVal partyID As Integer, ByVal addressId As Integer, ByVal startDate As Date, ByVal endDate As Object, ByVal roleType As Integer, ByVal partySpecimenStatus As Object)
            Me.Update(specimenID, partyID, addressId, startDate, endDate, roleType, partySpecimenStatus, 0, Nothing)
        End Sub
        
        Public Overloads Sub Update(ByVal specimenID As Integer, ByVal partyID As Integer, ByVal addressId As Integer, ByVal startDate As Date, ByVal endDate As Object, ByVal roleType As Integer, ByVal partySpecimenStatus As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction)
            Me.Update(specimenID, partyID, addressId, startDate, endDate, roleType, partySpecimenStatus, 0, transaction)
        End Sub
        
        Public Overloads Sub Update(ByVal specimenID As Integer, ByVal partyID As Integer, ByVal addressId As Integer, ByVal startDate As Date, ByVal endDate As Object, ByVal roleType As Integer, ByVal partySpecimenStatus As Object, ByVal checkSum As Integer)
            Me.Update(specimenID, partyID, addressId, startDate, endDate, roleType, partySpecimenStatus, checkSum, Nothing)
        End Sub
        
        Public Overloads Sub Update(ByVal partySpecimen As Entity.PartySpecimen)
            Me.Update(partySpecimen(0), partySpecimen(1), partySpecimen(2), partySpecimen(3), partySpecimen(4), partySpecimen(5), partySpecimen(6))
        End Sub
        
        Public Overloads Sub Update(ByVal partySpecimen As Entity.PartySpecimen, ByVal transaction As System.Data.SqlClient.SqlTransaction)
            Me.Update(partySpecimen(0), partySpecimen(1), partySpecimen(2), partySpecimen(3), partySpecimen(4), partySpecimen(5), partySpecimen(6), transaction)
        End Sub
        
        Public Overloads Sub Update(ByVal partySpecimen As Entity.PartySpecimen, ByVal checkSum As Integer)
            Me.Update(partySpecimen(0), partySpecimen(1), partySpecimen(2), partySpecimen(3), partySpecimen(4), partySpecimen(5), partySpecimen(6), checkSum)
        End Sub
        
        Public Overloads Sub Update(ByVal partySpecimen As Entity.PartySpecimen, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction)
            Me.Update(partySpecimen(0), partySpecimen(1), partySpecimen(2), partySpecimen(3), partySpecimen(4), partySpecimen(5), partySpecimen(6), checkSum, transaction)
        End Sub
        
        Public Overloads Function GetByIndex_UQ_PartySpecimen(ByVal partyID As Integer, ByVal specimenID As Integer, ByVal roleType As Integer, ByVal startDate As Date) As EntitySet.PartySpecimenSet
            Return Sprocs.eosp_SelectPartySpecimen(Index_PartyID:=[partyID], Index_SpecimenID:=[specimenID], Index_RoleType:=[roleType], Index_StartDate:=[startDate], transaction:=Nothing)
        End Function
        
        Public Overloads Function GetByIndex_UQ_PartySpecimen(ByVal partyID As Integer, ByVal specimenID As Integer, ByVal roleType As Integer, ByVal startDate As Date, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.PartySpecimenSet
            Return Sprocs.eosp_SelectPartySpecimen(Index_PartyID:=[partyID], Index_SpecimenID:=[specimenID], Index_RoleType:=[roleType], Index_StartDate:=[startDate], transaction:=transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
        End Enum
    End Class
End Namespace
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
    
    'Service base implementation for table 'SeizureNotification'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class SeizureNotificationServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.SeizureNotificationSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.SeizureNotificationSet
            Return CType(MyBase.GetAll("eosp_SelectSeizureNotification", GetType(EntitySet.SeizureNotificationSet), includeHyphen, includeInactive, orderBy),EntitySet.SeizureNotificationSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.SeizureNotificationSet
            Return Me.GetAll(includeHyphen, includeInactive, SeizureNotificationServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, SeizureNotificationServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal orderBy As OrderBy) As EntitySet.SeizureNotificationSet
            Return Me.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal seizureNotificationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectSeizureNotification", "SeizureNotificationId", seizureNotificationId, GetType(EntitySet.SeizureNotificationSet), tran),Entity.SeizureNotification)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal seizureNotificationId As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(seizureNotificationId, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal seizureNotificationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.SeizureNotification
            Return CType(MyBase.GetById("eosp_SelectSeizureNotification", "SeizureNotificationId", seizureNotificationId, GetType(EntitySet.SeizureNotificationSet), tran),Entity.SeizureNotification)
        End Function
        
        Public Overloads Function GetById(ByVal seizureNotificationId As Integer) As Entity.SeizureNotification
            Return Me.GetById(seizureNotificationId, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal seizureNotificationId As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(seizureNotificationId, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal seizureNotificationId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeleteSeizureNotification", "SeizureNotificationId", seizureNotificationId, checkSum, transaction)
        End Function
        
        'GetForPortOfEntry - links to the PortOfEntry table...
        Public Overloads Function GetForPortOfEntry(ByVal PortOfEntryID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.SeizureNotificationSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from SeizureNotification where PortOfEnt"& _ 
"ryID=" + PortOfEntryID.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.SeizureNotificationSet), tran),EntitySet.SeizureNotificationSet)
        End Function
        
        'GetForPortOfEntry - links to the PortOfEntry table...
        Public Overloads Function GetForPortOfEntry(ByVal PortOfEntryID As Integer) As EntitySet.SeizureNotificationSet
            Return Me.GetForPortOfEntry(PortOfEntryID, Nothing)
        End Function
        
        'GetForCITESNotification - links to the CITESNotification table...
        Public Overloads Function GetForCITESNotification(ByVal CITESNotificationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.SeizureNotificationSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from SeizureNotification where CITESNoti"& _ 
"ficationId=" + CITESNotificationId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.SeizureNotificationSet), tran),EntitySet.SeizureNotificationSet)
        End Function
        
        'GetForCITESNotification - links to the CITESNotification table...
        Public Overloads Function GetForCITESNotification(ByVal CITESNotificationId As Integer) As EntitySet.SeizureNotificationSet
            Return Me.GetForCITESNotification(CITESNotificationId, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal customsReference As Object, ByVal seizureReason As Object, ByVal portOfEntryID As Object, ByVal cITESNotificationId As Integer, ByVal type As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.SeizureNotification
            Return Me.GetById(Sprocs.eosp_CreateSeizureNotification(customsReference, seizureReason, portOfEntryID, cITESNotificationId, type, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal customsReference As Object, ByVal seizureReason As Object, ByVal portOfEntryID As Object, ByVal cITESNotificationId As Integer, ByVal type As Object) As Entity.SeizureNotification
            Return Me.Insert(customsReference, seizureReason, portOfEntryID, cITESNotificationId, type, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal seizureNotification As Entity.SeizureNotification) As Entity.SeizureNotification
            Return Me.Insert(seizureNotification(1), seizureNotification(2), seizureNotification(3), seizureNotification(4), seizureNotification(5))
        End Function
        
        Public Overloads Function Insert(ByVal seizureNotification As Entity.SeizureNotification, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.SeizureNotification
            Return Me.Insert(seizureNotification(1), seizureNotification(2), seizureNotification(3), seizureNotification(4), seizureNotification(5), transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal customsReference As Object, ByVal seizureReason As Object, ByVal portOfEntryID As Object, ByVal cITESNotificationId As Integer, ByVal type As Object, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.SeizureNotification
            Return Sprocs.eosp_UpdateSeizureNotification(id, customsReference, seizureReason, portOfEntryID, cITESNotificationId, type, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal customsReference As Object, ByVal seizureReason As Object, ByVal portOfEntryID As Object, ByVal cITESNotificationId As Integer, ByVal type As Object) As Entity.SeizureNotification
            Return Me.Update(id, customsReference, seizureReason, portOfEntryID, cITESNotificationId, type, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal customsReference As Object, ByVal seizureReason As Object, ByVal portOfEntryID As Object, ByVal cITESNotificationId As Integer, ByVal type As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.SeizureNotification
            Return Me.Update(id, customsReference, seizureReason, portOfEntryID, cITESNotificationId, type, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal customsReference As Object, ByVal seizureReason As Object, ByVal portOfEntryID As Object, ByVal cITESNotificationId As Integer, ByVal type As Object, ByVal checkSum As Integer) As Entity.SeizureNotification
            Return Me.Update(id, customsReference, seizureReason, portOfEntryID, cITESNotificationId, type, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal seizureNotification As Entity.SeizureNotification) As Entity.SeizureNotification
            Return Me.Update(seizureNotification.id, seizureNotification(1), seizureNotification(2), seizureNotification(3), seizureNotification(4), seizureNotification(5))
        End Function
        
        Public Overloads Function Update(ByVal seizureNotification As Entity.SeizureNotification, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.SeizureNotification
            Return Me.Update(seizureNotification.id, seizureNotification(1), seizureNotification(2), seizureNotification(3), seizureNotification(4), seizureNotification(5), transaction)
        End Function
        
        Public Overloads Function Update(ByVal seizureNotification As Entity.SeizureNotification, ByVal checkSum As Integer) As Entity.SeizureNotification
            Return Me.Update(seizureNotification.id, seizureNotification(1), seizureNotification(2), seizureNotification(3), seizureNotification(4), seizureNotification(5), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal seizureNotification As Entity.SeizureNotification, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.SeizureNotification
            Return Me.Update(seizureNotification.id, seizureNotification(1), seizureNotification(2), seizureNotification(3), seizureNotification(4), seizureNotification(5), checkSum, transaction)
        End Function
        
        Public Overloads Function GetByIndex_UQ__SeizureNotificat__51300E55(ByVal seizureNotificationId As Integer, ByVal seizureReason As String, ByVal cITESNotificationId As Integer) As EntitySet.SeizureNotificationSet
            Return Sprocs.eosp_SelectSeizureNotification(seizureNotificationId:=Nothing, Index_SeizureNotificationId:=[seizureNotificationId], Index_SeizureReason:=[seizureReason], Index_CITESNotificationId:=[cITESNotificationId], transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_UQ__SeizureNotificat__51300E55(ByVal seizureNotificationId As Integer, ByVal seizureReason As String, ByVal cITESNotificationId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.SeizureNotificationSet
            Return Sprocs.eosp_SelectSeizureNotification(seizureNotificationId:=Nothing, Index_SeizureNotificationId:=[seizureNotificationId], Index_SeizureReason:=[seizureReason], Index_CITESNotificationId:=[cITESNotificationId], sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
            
            UQ__SeizureNotificat__51300E55
            
            
        End Enum
    End Class
End Namespace

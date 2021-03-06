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
    
    'Service base implementation for table 'PermitDerogationGuildline'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class PermitDerogationGuildlineServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.PermitDerogationGuildlineSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.PermitDerogationGuildlineSet
            Return CType(MyBase.GetAll("eosp_SelectPermitDerogationGuildline", GetType(EntitySet.PermitDerogationGuildlineSet), includeHyphen, includeInactive, orderBy),EntitySet.PermitDerogationGuildlineSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.PermitDerogationGuildlineSet
            Return Me.GetAll(includeHyphen, includeInactive, PermitDerogationGuildlineServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, PermitDerogationGuildlineServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal orderBy As OrderBy) As EntitySet.PermitDerogationGuildlineSet
            Return Me.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal permitDerogationGuildlineId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectPermitDerogationGuildline", "PermitDerogationGuildlineId", permitDerogationGuildlineId, GetType(EntitySet.PermitDerogationGuildlineSet), tran),Entity.PermitDerogationGuildline)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal permitDerogationGuildlineId As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(permitDerogationGuildlineId, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal permitDerogationGuildlineId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.PermitDerogationGuildline
            Return CType(MyBase.GetById("eosp_SelectPermitDerogationGuildline", "PermitDerogationGuildlineId", permitDerogationGuildlineId, GetType(EntitySet.PermitDerogationGuildlineSet), tran),Entity.PermitDerogationGuildline)
        End Function
        
        Public Overloads Function GetById(ByVal permitDerogationGuildlineId As Integer) As Entity.PermitDerogationGuildline
            Return Me.GetById(permitDerogationGuildlineId, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal permitDerogationGuildlineId As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(permitDerogationGuildlineId, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal permitDerogationGuildlineId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeletePermitDerogationGuildline", "PermitDerogationGuildlineId", permitDerogationGuildlineId, checkSum, transaction)
        End Function
        
        'GetForPermit - links to the Permit table...
        Public Overloads Function GetForPermit(ByVal PermitId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitDerogationGuildlineSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from PermitDerogationGuildline where Per"& _ 
"mitId=" + PermitId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.PermitDerogationGuildlineSet), tran),EntitySet.PermitDerogationGuildlineSet)
        End Function
        
        'GetForPermit - links to the Permit table...
        Public Overloads Function GetForPermit(ByVal PermitId As Integer) As EntitySet.PermitDerogationGuildlineSet
            Return Me.GetForPermit(PermitId, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal permitId As Object, ByVal derogationGuideLineId As Object, ByVal description As String, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PermitDerogationGuildline
            Return Me.GetById(Sprocs.eosp_CreatePermitDerogationGuildline(permitId, derogationGuideLineId, description, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal permitId As Object, ByVal derogationGuideLineId As Object, ByVal description As String) As Entity.PermitDerogationGuildline
            Return Me.Insert(permitId, derogationGuideLineId, description, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal permitDerogationGuildline As Entity.PermitDerogationGuildline) As Entity.PermitDerogationGuildline
            Return Me.Insert(permitDerogationGuildline(1), permitDerogationGuildline(2), permitDerogationGuildline(3))
        End Function
        
        Public Overloads Function Insert(ByVal permitDerogationGuildline As Entity.PermitDerogationGuildline, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PermitDerogationGuildline
            Return Me.Insert(permitDerogationGuildline(1), permitDerogationGuildline(2), permitDerogationGuildline(3), transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal permitId As Object, ByVal derogationGuideLineId As Object, ByVal description As String, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PermitDerogationGuildline
            Return Sprocs.eosp_UpdatePermitDerogationGuildline(id, permitId, derogationGuideLineId, description, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal permitId As Object, ByVal derogationGuideLineId As Object, ByVal description As String) As Entity.PermitDerogationGuildline
            Return Me.Update(id, permitId, derogationGuideLineId, description, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal permitId As Object, ByVal derogationGuideLineId As Object, ByVal description As String, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PermitDerogationGuildline
            Return Me.Update(id, permitId, derogationGuideLineId, description, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal permitId As Object, ByVal derogationGuideLineId As Object, ByVal description As String, ByVal checkSum As Integer) As Entity.PermitDerogationGuildline
            Return Me.Update(id, permitId, derogationGuideLineId, description, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal permitDerogationGuildline As Entity.PermitDerogationGuildline) As Entity.PermitDerogationGuildline
            Return Me.Update(permitDerogationGuildline.id, permitDerogationGuildline(1), permitDerogationGuildline(2), permitDerogationGuildline(3))
        End Function
        
        Public Overloads Function Update(ByVal permitDerogationGuildline As Entity.PermitDerogationGuildline, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PermitDerogationGuildline
            Return Me.Update(permitDerogationGuildline.id, permitDerogationGuildline(1), permitDerogationGuildline(2), permitDerogationGuildline(3), transaction)
        End Function
        
        Public Overloads Function Update(ByVal permitDerogationGuildline As Entity.PermitDerogationGuildline, ByVal checkSum As Integer) As Entity.PermitDerogationGuildline
            Return Me.Update(permitDerogationGuildline.id, permitDerogationGuildline(1), permitDerogationGuildline(2), permitDerogationGuildline(3), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal permitDerogationGuildline As Entity.PermitDerogationGuildline, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PermitDerogationGuildline
            Return Me.Update(permitDerogationGuildline.id, permitDerogationGuildline(1), permitDerogationGuildline(2), permitDerogationGuildline(3), checkSum, transaction)
        End Function
        
        Public Overloads Function GetByIndex_UQ__PermitDerogation__5CA1C101(ByVal permitDerogationGuildlineId As Integer, ByVal permitId As Integer, ByVal derogationGuideLineId As Integer, ByVal description As String) As EntitySet.PermitDerogationGuildlineSet
            Return Sprocs.eosp_SelectPermitDerogationGuildline(permitDerogationGuildlineId:=Nothing, Index_PermitDerogationGuildlineId:=[permitDerogationGuildlineId], Index_PermitId:=[permitId], Index_DerogationGuideLineId:=[derogationGuideLineId], Index_Description:=[description], transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_UQ__PermitDerogation__5CA1C101(ByVal permitDerogationGuildlineId As Integer, ByVal permitId As Integer, ByVal derogationGuideLineId As Integer, ByVal description As String, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitDerogationGuildlineSet
            Return Sprocs.eosp_SelectPermitDerogationGuildline(permitDerogationGuildlineId:=Nothing, Index_PermitDerogationGuildlineId:=[permitDerogationGuildlineId], Index_PermitId:=[permitId], Index_DerogationGuideLineId:=[derogationGuideLineId], Index_Description:=[description], sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
            
            UQ__PermitDerogation__5CA1C101
            
            
        End Enum
    End Class
End Namespace

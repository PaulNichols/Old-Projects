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
    
    'Service base implementation for table 'PermitSpecialCondition'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class PermitSpecialConditionServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.PermitSpecialConditionSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.PermitSpecialConditionSet
            Return CType(MyBase.GetAll("eosp_SelectPermitSpecialCondition", GetType(EntitySet.PermitSpecialConditionSet), includeHyphen, includeInactive, orderBy),EntitySet.PermitSpecialConditionSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.PermitSpecialConditionSet
            Return Me.GetAll(includeHyphen, includeInactive, PermitSpecialConditionServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, PermitSpecialConditionServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal permitSpecialConditionId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectPermitSpecialCondition", "PermitSpecialConditionId", permitSpecialConditionId, GetType(EntitySet.PermitSpecialConditionSet), tran),Entity.PermitSpecialCondition)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal permitSpecialConditionId As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(permitSpecialConditionId, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal permitSpecialConditionId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.PermitSpecialCondition
            Return CType(MyBase.GetById("eosp_SelectPermitSpecialCondition", "PermitSpecialConditionId", permitSpecialConditionId, GetType(EntitySet.PermitSpecialConditionSet), tran),Entity.PermitSpecialCondition)
        End Function
        
        Public Overloads Function GetById(ByVal permitSpecialConditionId As Integer) As Entity.PermitSpecialCondition
            Return Me.GetById(permitSpecialConditionId, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal permitSpecialConditionId As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(permitSpecialConditionId, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal permitSpecialConditionId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeletePermitSpecialCondition", "PermitSpecialConditionId", permitSpecialConditionId, checkSum, transaction)
        End Function
        
        'GetForPermit - links to the Permit table...
        Public Overloads Function GetForPermit(ByVal PermitId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitSpecialConditionSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from PermitSpecialCondition where Permit"& _ 
"Id=" + PermitId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.PermitSpecialConditionSet), tran),EntitySet.PermitSpecialConditionSet)
        End Function
        
        'GetForPermit - links to the Permit table...
        Public Overloads Function GetForPermit(ByVal PermitId As Integer) As EntitySet.PermitSpecialConditionSet
            Return Me.GetForPermit(PermitId, Nothing)
        End Function
        
        'GetForSpecialCondition - links to the SpecialCondition table...
        Public Overloads Function GetForSpecialCondition(ByVal SpecialConditionId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitSpecialConditionSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from PermitSpecialCondition where Specia"& _ 
"lConditionId=" + SpecialConditionId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.PermitSpecialConditionSet), tran),EntitySet.PermitSpecialConditionSet)
        End Function
        
        'GetForSpecialCondition - links to the SpecialCondition table...
        Public Overloads Function GetForSpecialCondition(ByVal SpecialConditionId As Integer) As EntitySet.PermitSpecialConditionSet
            Return Me.GetForSpecialCondition(SpecialConditionId, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal permitId As Integer, ByVal bFDate As Object, ByVal statusId As Object, ByVal dateApplied As Date, ByVal specialConditionId As Integer, ByVal sSOUserId As Decimal, ByVal condition As Object, ByVal current As Boolean, ByVal addedBySA As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PermitSpecialCondition
            Return Me.GetById(Sprocs.eosp_CreatePermitSpecialCondition(permitId, bFDate, statusId, dateApplied, specialConditionId, sSOUserId, condition, current, addedBySA, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal permitId As Integer, ByVal bFDate As Object, ByVal statusId As Object, ByVal dateApplied As Date, ByVal specialConditionId As Integer, ByVal sSOUserId As Decimal, ByVal condition As Object, ByVal current As Boolean, ByVal addedBySA As Object) As Entity.PermitSpecialCondition
            Return Me.Insert(permitId, bFDate, statusId, dateApplied, specialConditionId, sSOUserId, condition, current, addedBySA, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal permitSpecialCondition As Entity.PermitSpecialCondition) As Entity.PermitSpecialCondition
            Return Me.Insert(permitSpecialCondition(1), permitSpecialCondition(2), permitSpecialCondition(3), permitSpecialCondition(4), permitSpecialCondition(5), permitSpecialCondition(6), permitSpecialCondition(7), permitSpecialCondition(8), permitSpecialCondition(9))
        End Function
        
        Public Overloads Function Insert(ByVal permitSpecialCondition As Entity.PermitSpecialCondition, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PermitSpecialCondition
            Return Me.Insert(permitSpecialCondition(1), permitSpecialCondition(2), permitSpecialCondition(3), permitSpecialCondition(4), permitSpecialCondition(5), permitSpecialCondition(6), permitSpecialCondition(7), permitSpecialCondition(8), permitSpecialCondition(9), transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal permitId As Integer, ByVal bFDate As Object, ByVal statusId As Object, ByVal dateApplied As Date, ByVal specialConditionId As Integer, ByVal sSOUserId As Decimal, ByVal condition As Object, ByVal current As Boolean, ByVal addedBySA As Object, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PermitSpecialCondition
            Return Sprocs.eosp_UpdatePermitSpecialCondition(id, permitId, bFDate, statusId, dateApplied, specialConditionId, sSOUserId, condition, current, addedBySA, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal permitId As Integer, ByVal bFDate As Object, ByVal statusId As Object, ByVal dateApplied As Date, ByVal specialConditionId As Integer, ByVal sSOUserId As Decimal, ByVal condition As Object, ByVal current As Boolean, ByVal addedBySA As Object) As Entity.PermitSpecialCondition
            Return Me.Update(id, permitId, bFDate, statusId, dateApplied, specialConditionId, sSOUserId, condition, current, addedBySA, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal permitId As Integer, ByVal bFDate As Object, ByVal statusId As Object, ByVal dateApplied As Date, ByVal specialConditionId As Integer, ByVal sSOUserId As Decimal, ByVal condition As Object, ByVal current As Boolean, ByVal addedBySA As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PermitSpecialCondition
            Return Me.Update(id, permitId, bFDate, statusId, dateApplied, specialConditionId, sSOUserId, condition, current, addedBySA, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal permitId As Integer, ByVal bFDate As Object, ByVal statusId As Object, ByVal dateApplied As Date, ByVal specialConditionId As Integer, ByVal sSOUserId As Decimal, ByVal condition As Object, ByVal current As Boolean, ByVal addedBySA As Object, ByVal checkSum As Integer) As Entity.PermitSpecialCondition
            Return Me.Update(id, permitId, bFDate, statusId, dateApplied, specialConditionId, sSOUserId, condition, current, addedBySA, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal permitSpecialCondition As Entity.PermitSpecialCondition) As Entity.PermitSpecialCondition
            Return Me.Update(permitSpecialCondition.id, permitSpecialCondition(1), permitSpecialCondition(2), permitSpecialCondition(3), permitSpecialCondition(4), permitSpecialCondition(5), permitSpecialCondition(6), permitSpecialCondition(7), permitSpecialCondition(8), permitSpecialCondition(9))
        End Function
        
        Public Overloads Function Update(ByVal permitSpecialCondition As Entity.PermitSpecialCondition, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PermitSpecialCondition
            Return Me.Update(permitSpecialCondition.id, permitSpecialCondition(1), permitSpecialCondition(2), permitSpecialCondition(3), permitSpecialCondition(4), permitSpecialCondition(5), permitSpecialCondition(6), permitSpecialCondition(7), permitSpecialCondition(8), permitSpecialCondition(9), transaction)
        End Function
        
        Public Overloads Function Update(ByVal permitSpecialCondition As Entity.PermitSpecialCondition, ByVal checkSum As Integer) As Entity.PermitSpecialCondition
            Return Me.Update(permitSpecialCondition.id, permitSpecialCondition(1), permitSpecialCondition(2), permitSpecialCondition(3), permitSpecialCondition(4), permitSpecialCondition(5), permitSpecialCondition(6), permitSpecialCondition(7), permitSpecialCondition(8), permitSpecialCondition(9), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal permitSpecialCondition As Entity.PermitSpecialCondition, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PermitSpecialCondition
            Return Me.Update(permitSpecialCondition.id, permitSpecialCondition(1), permitSpecialCondition(2), permitSpecialCondition(3), permitSpecialCondition(4), permitSpecialCondition(5), permitSpecialCondition(6), permitSpecialCondition(7), permitSpecialCondition(8), permitSpecialCondition(9), checkSum, transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
        End Enum
    End Class
End Namespace

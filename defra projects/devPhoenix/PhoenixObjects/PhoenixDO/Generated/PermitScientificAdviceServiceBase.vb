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
    
    'Service base implementation for table 'PermitScientificAdvice'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class PermitScientificAdviceServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.PermitScientificAdviceSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.PermitScientificAdviceSet
            Return CType(MyBase.GetAll("eosp_SelectPermitScientificAdvice", GetType(EntitySet.PermitScientificAdviceSet), includeHyphen, includeInactive, orderBy),EntitySet.PermitScientificAdviceSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.PermitScientificAdviceSet
            Return Me.GetAll(includeHyphen, includeInactive, PermitScientificAdviceServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, PermitScientificAdviceServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal permitScientificAdvice As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectPermitScientificAdvice", "PermitScientificAdvice", permitScientificAdvice, GetType(EntitySet.PermitScientificAdviceSet), tran),Entity.PermitScientificAdvice)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal permitScientificAdvice As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(permitScientificAdvice, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal permitScientificAdvice As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.PermitScientificAdvice
            Return CType(MyBase.GetById("eosp_SelectPermitScientificAdvice", "PermitScientificAdvice", permitScientificAdvice, GetType(EntitySet.PermitScientificAdviceSet), tran),Entity.PermitScientificAdvice)
        End Function
        
        Public Overloads Function GetById(ByVal permitScientificAdvice As Integer) As Entity.PermitScientificAdvice
            Return Me.GetById(permitScientificAdvice, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal permitScientificAdvice As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(permitScientificAdvice, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal permitScientificAdvice As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeletePermitScientificAdvice", "PermitScientificAdvice", permitScientificAdvice, checkSum, transaction)
        End Function
        
        'GetForScientificAdvice - links to the ScientificAdvice table...
        Public Overloads Function GetForScientificAdvice(ByVal ScientificAdviceId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitScientificAdviceSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from PermitScientificAdvice where Scient"& _ 
"ificAdviceId=" + ScientificAdviceId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.PermitScientificAdviceSet), tran),EntitySet.PermitScientificAdviceSet)
        End Function
        
        'GetForScientificAdvice - links to the ScientificAdvice table...
        Public Overloads Function GetForScientificAdvice(ByVal ScientificAdviceId As Integer) As EntitySet.PermitScientificAdviceSet
            Return Me.GetForScientificAdvice(ScientificAdviceId, Nothing)
        End Function
        
        'GetForPermit - links to the Permit table...
        Public Overloads Function GetForPermit(ByVal PermitId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitScientificAdviceSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from PermitScientificAdvice where Permit"& _ 
"Id=" + PermitId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.PermitScientificAdviceSet), tran),EntitySet.PermitScientificAdviceSet)
        End Function
        
        'GetForPermit - links to the Permit table...
        Public Overloads Function GetForPermit(ByVal PermitId As Integer) As EntitySet.PermitScientificAdviceSet
            Return Me.GetForPermit(PermitId, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal dateOfAdvice As Date, ByVal scientificAdviceId As Integer, ByVal permitId As Integer, ByVal sSOUserId As Decimal, ByVal specificAdvice As Object, ByVal current As Boolean, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PermitScientificAdvice
            Return Me.GetById(Sprocs.eosp_CreatePermitScientificAdvice(dateOfAdvice, scientificAdviceId, permitId, sSOUserId, specificAdvice, current, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal dateOfAdvice As Date, ByVal scientificAdviceId As Integer, ByVal permitId As Integer, ByVal sSOUserId As Decimal, ByVal specificAdvice As Object, ByVal current As Boolean) As Entity.PermitScientificAdvice
            Return Me.Insert(dateOfAdvice, scientificAdviceId, permitId, sSOUserId, specificAdvice, current, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal permitScientificAdvice As Entity.PermitScientificAdvice) As Entity.PermitScientificAdvice
            Return Me.Insert(permitScientificAdvice(1), permitScientificAdvice(2), permitScientificAdvice(3), permitScientificAdvice(4), permitScientificAdvice(5), permitScientificAdvice(6))
        End Function
        
        Public Overloads Function Insert(ByVal permitScientificAdvice As Entity.PermitScientificAdvice, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PermitScientificAdvice
            Return Me.Insert(permitScientificAdvice(1), permitScientificAdvice(2), permitScientificAdvice(3), permitScientificAdvice(4), permitScientificAdvice(5), permitScientificAdvice(6), transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal dateOfAdvice As Date, ByVal scientificAdviceId As Integer, ByVal permitId As Integer, ByVal sSOUserId As Decimal, ByVal specificAdvice As Object, ByVal current As Boolean, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PermitScientificAdvice
            Return Sprocs.eosp_UpdatePermitScientificAdvice(id, dateOfAdvice, scientificAdviceId, permitId, sSOUserId, specificAdvice, current, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal dateOfAdvice As Date, ByVal scientificAdviceId As Integer, ByVal permitId As Integer, ByVal sSOUserId As Decimal, ByVal specificAdvice As Object, ByVal current As Boolean) As Entity.PermitScientificAdvice
            Return Me.Update(id, dateOfAdvice, scientificAdviceId, permitId, sSOUserId, specificAdvice, current, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal dateOfAdvice As Date, ByVal scientificAdviceId As Integer, ByVal permitId As Integer, ByVal sSOUserId As Decimal, ByVal specificAdvice As Object, ByVal current As Boolean, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PermitScientificAdvice
            Return Me.Update(id, dateOfAdvice, scientificAdviceId, permitId, sSOUserId, specificAdvice, current, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal dateOfAdvice As Date, ByVal scientificAdviceId As Integer, ByVal permitId As Integer, ByVal sSOUserId As Decimal, ByVal specificAdvice As Object, ByVal current As Boolean, ByVal checkSum As Integer) As Entity.PermitScientificAdvice
            Return Me.Update(id, dateOfAdvice, scientificAdviceId, permitId, sSOUserId, specificAdvice, current, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal permitScientificAdvice As Entity.PermitScientificAdvice) As Entity.PermitScientificAdvice
            Return Me.Update(permitScientificAdvice.id, permitScientificAdvice(1), permitScientificAdvice(2), permitScientificAdvice(3), permitScientificAdvice(4), permitScientificAdvice(5), permitScientificAdvice(6))
        End Function
        
        Public Overloads Function Update(ByVal permitScientificAdvice As Entity.PermitScientificAdvice, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PermitScientificAdvice
            Return Me.Update(permitScientificAdvice.id, permitScientificAdvice(1), permitScientificAdvice(2), permitScientificAdvice(3), permitScientificAdvice(4), permitScientificAdvice(5), permitScientificAdvice(6), transaction)
        End Function
        
        Public Overloads Function Update(ByVal permitScientificAdvice As Entity.PermitScientificAdvice, ByVal checkSum As Integer) As Entity.PermitScientificAdvice
            Return Me.Update(permitScientificAdvice.id, permitScientificAdvice(1), permitScientificAdvice(2), permitScientificAdvice(3), permitScientificAdvice(4), permitScientificAdvice(5), permitScientificAdvice(6), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal permitScientificAdvice As Entity.PermitScientificAdvice, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PermitScientificAdvice
            Return Me.Update(permitScientificAdvice.id, permitScientificAdvice(1), permitScientificAdvice(2), permitScientificAdvice(3), permitScientificAdvice(4), permitScientificAdvice(5), permitScientificAdvice(6), checkSum, transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
        End Enum
    End Class
End Namespace
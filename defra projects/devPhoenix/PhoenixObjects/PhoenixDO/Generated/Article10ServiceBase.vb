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
    
    'Service base implementation for table 'Article10'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class Article10ServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.Article10Set
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.Article10Set
            Return CType(MyBase.GetAll("eosp_SelectArticle10", GetType(EntitySet.Article10Set), includeHyphen, includeInactive, orderBy),EntitySet.Article10Set)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.Article10Set
            Return Me.GetAll(includeHyphen, includeInactive, Article10ServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, Article10ServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal article10Id As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectArticle10", "Article10Id", article10Id, GetType(EntitySet.Article10Set), tran),Entity.Article10)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal article10Id As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(article10Id, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal article10Id As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.Article10
            Return CType(MyBase.GetById("eosp_SelectArticle10", "Article10Id", article10Id, GetType(EntitySet.Article10Set), tran),Entity.Article10)
        End Function
        
        Public Overloads Function GetById(ByVal article10Id As Integer) As Entity.Article10
            Return Me.GetById(article10Id, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal article10Id As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(article10Id, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal article10Id As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeleteArticle10", "Article10Id", article10Id, checkSum, transaction)
        End Function
        
        'GetForCITESApplication - links to the CITESApplication table...
        Public Overloads Function GetForCITESApplication(ByVal CitesApplicationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.Article10Set
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from Article10 where CitesApplicationId="& _ 
"" + CitesApplicationId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.Article10Set), tran),EntitySet.Article10Set)
        End Function
        
        'GetForCITESApplication - links to the CITESApplication table...
        Public Overloads Function GetForCITESApplication(ByVal CitesApplicationId As Integer) As EntitySet.Article10Set
            Return Me.GetForCITESApplication(CitesApplicationId, Nothing)
        End Function
        
        'GetForArticle10CertificateType - links to the Article10CertificateType table...
        Public Overloads Function GetForArticle10CertificateType(ByVal Article10CertificationType As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.Article10Set
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from Article10 where Article10TypeId=" + Article10CertificationType.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.Article10Set), tran),EntitySet.Article10Set)
        End Function
        
        'GetForArticle10CertificateType - links to the Article10CertificateType table...
        Public Overloads Function GetForArticle10CertificateType(ByVal Article10CertificationType As Integer) As EntitySet.Article10Set
            Return Me.GetForArticle10CertificateType(Article10CertificationType, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal citesApplicationId As Integer, ByVal article10TypeId As Object, ByVal box18_1 As Boolean, ByVal box18_2 As Boolean, ByVal box18_3 As Boolean, ByVal box18_4 As Boolean, ByVal box18_5 As Boolean, ByVal box18_6 As Boolean, ByVal box18_7 As Boolean, ByVal box18_8 As Boolean, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Article10
            Return Me.GetById(Sprocs.eosp_CreateArticle10(citesApplicationId, article10TypeId, box18_1, box18_2, box18_3, box18_4, box18_5, box18_6, box18_7, box18_8, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal citesApplicationId As Integer, ByVal article10TypeId As Object, ByVal box18_1 As Boolean, ByVal box18_2 As Boolean, ByVal box18_3 As Boolean, ByVal box18_4 As Boolean, ByVal box18_5 As Boolean, ByVal box18_6 As Boolean, ByVal box18_7 As Boolean, ByVal box18_8 As Boolean) As Entity.Article10
            Return Me.Insert(citesApplicationId, article10TypeId, box18_1, box18_2, box18_3, box18_4, box18_5, box18_6, box18_7, box18_8, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal article10 As Entity.Article10) As Entity.Article10
            Return Me.Insert(article10(1), article10(2), article10(3), article10(4), article10(5), article10(6), article10(7), article10(8), article10(9), article10(10))
        End Function
        
        Public Overloads Function Insert(ByVal article10 As Entity.Article10, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Article10
            Return Me.Insert(article10(1), article10(2), article10(3), article10(4), article10(5), article10(6), article10(7), article10(8), article10(9), article10(10), transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal citesApplicationId As Integer, ByVal article10TypeId As Object, ByVal box18_1 As Boolean, ByVal box18_2 As Boolean, ByVal box18_3 As Boolean, ByVal box18_4 As Boolean, ByVal box18_5 As Boolean, ByVal box18_6 As Boolean, ByVal box18_7 As Boolean, ByVal box18_8 As Boolean, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Article10
            Return Sprocs.eosp_UpdateArticle10(id, citesApplicationId, article10TypeId, box18_1, box18_2, box18_3, box18_4, box18_5, box18_6, box18_7, box18_8, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal citesApplicationId As Integer, ByVal article10TypeId As Object, ByVal box18_1 As Boolean, ByVal box18_2 As Boolean, ByVal box18_3 As Boolean, ByVal box18_4 As Boolean, ByVal box18_5 As Boolean, ByVal box18_6 As Boolean, ByVal box18_7 As Boolean, ByVal box18_8 As Boolean) As Entity.Article10
            Return Me.Update(id, citesApplicationId, article10TypeId, box18_1, box18_2, box18_3, box18_4, box18_5, box18_6, box18_7, box18_8, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal citesApplicationId As Integer, ByVal article10TypeId As Object, ByVal box18_1 As Boolean, ByVal box18_2 As Boolean, ByVal box18_3 As Boolean, ByVal box18_4 As Boolean, ByVal box18_5 As Boolean, ByVal box18_6 As Boolean, ByVal box18_7 As Boolean, ByVal box18_8 As Boolean, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Article10
            Return Me.Update(id, citesApplicationId, article10TypeId, box18_1, box18_2, box18_3, box18_4, box18_5, box18_6, box18_7, box18_8, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal citesApplicationId As Integer, ByVal article10TypeId As Object, ByVal box18_1 As Boolean, ByVal box18_2 As Boolean, ByVal box18_3 As Boolean, ByVal box18_4 As Boolean, ByVal box18_5 As Boolean, ByVal box18_6 As Boolean, ByVal box18_7 As Boolean, ByVal box18_8 As Boolean, ByVal checkSum As Integer) As Entity.Article10
            Return Me.Update(id, citesApplicationId, article10TypeId, box18_1, box18_2, box18_3, box18_4, box18_5, box18_6, box18_7, box18_8, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal article10 As Entity.Article10) As Entity.Article10
            Return Me.Update(article10.id, article10(1), article10(2), article10(3), article10(4), article10(5), article10(6), article10(7), article10(8), article10(9), article10(10))
        End Function
        
        Public Overloads Function Update(ByVal article10 As Entity.Article10, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Article10
            Return Me.Update(article10.id, article10(1), article10(2), article10(3), article10(4), article10(5), article10(6), article10(7), article10(8), article10(9), article10(10), transaction)
        End Function
        
        Public Overloads Function Update(ByVal article10 As Entity.Article10, ByVal checkSum As Integer) As Entity.Article10
            Return Me.Update(article10.id, article10(1), article10(2), article10(3), article10(4), article10(5), article10(6), article10(7), article10(8), article10(9), article10(10), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal article10 As Entity.Article10, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Article10
            Return Me.Update(article10.id, article10(1), article10(2), article10(3), article10(4), article10(5), article10(6), article10(7), article10(8), article10(9), article10(10), checkSum, transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
        End Enum
    End Class
End Namespace

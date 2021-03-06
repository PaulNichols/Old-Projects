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
    
    'Service base implementation for table 'SSOUser'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class SSOUserServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.SSOUserSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.SSOUserSet
            Return CType(MyBase.GetAll("eosp_SelectSSOUser", GetType(EntitySet.SSOUserSet), includeHyphen, includeInactive, orderBy),EntitySet.SSOUserSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.SSOUserSet
            Return Me.GetAll(includeHyphen, includeInactive, SSOUserServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, SSOUserServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal orderBy As OrderBy) As EntitySet.SSOUserSet
            Return Me.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal userId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectSSOUser", "UserId", userId, GetType(EntitySet.SSOUserSet), tran),Entity.SSOUser)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal userId As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(userId, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal userId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.SSOUser
            Return CType(MyBase.GetById("eosp_SelectSSOUser", "UserId", userId, GetType(EntitySet.SSOUserSet), tran),Entity.SSOUser)
        End Function
        
        Public Overloads Function GetById(ByVal userId As Integer) As Entity.SSOUser
            Return Me.GetById(userId, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal userId As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(userId, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal userId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeleteSSOUser", "UserId", userId, checkSum, transaction)
        End Function
        
        Public Overloads Function Insert( _
                    ByVal sSOUserId As Integer,  _
                    ByVal sPNumber As String,  _
                    ByVal fullname As String,  _
                    ByVal email As String,  _
                    ByVal location As String,  _
                    ByVal title As String,  _
                    ByVal initials As String,  _
                    ByVal firstName As String,  _
                    ByVal surname As String,  _
                    ByVal grade As String,  _
                    ByVal unit As String,  _
                    ByVal room As String,  _
                    ByVal ext As String,  _
                    ByVal sTD As String,  _
                    ByVal jobTitle As String,  _
                    ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.SSOUser
            Return Me.GetById(Sprocs.eosp_CreateSSOUser(sSOUserId, sPNumber, fullname, email, location, title, initials, firstName, surname, grade, unit, room, ext, sTD, jobTitle, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal sSOUserId As Integer, ByVal sPNumber As String, ByVal fullname As String, ByVal email As String, ByVal location As String, ByVal title As String, ByVal initials As String, ByVal firstName As String, ByVal surname As String, ByVal grade As String, ByVal unit As String, ByVal room As String, ByVal ext As String, ByVal sTD As String, ByVal jobTitle As String) As Entity.SSOUser
            Return Me.Insert(sSOUserId, sPNumber, fullname, email, location, title, initials, firstName, surname, grade, unit, room, ext, sTD, jobTitle, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal sSOUser As Entity.SSOUser) As Entity.SSOUser
            Return Me.Insert(sSOUser(1), sSOUser(2), sSOUser(3), sSOUser(4), sSOUser(5), sSOUser(6), sSOUser(7), sSOUser(8), sSOUser(9), sSOUser(10), sSOUser(11), sSOUser(12), sSOUser(13), sSOUser(14), sSOUser(15))
        End Function
        
        Public Overloads Function Insert(ByVal sSOUser As Entity.SSOUser, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.SSOUser
            Return Me.Insert(sSOUser(1), sSOUser(2), sSOUser(3), sSOUser(4), sSOUser(5), sSOUser(6), sSOUser(7), sSOUser(8), sSOUser(9), sSOUser(10), sSOUser(11), sSOUser(12), sSOUser(13), sSOUser(14), sSOUser(15), transaction)
        End Function
        
        Public Overloads Function Update( _
                    ByVal id As Integer,  _
                    ByVal sSOUserId As Integer,  _
                    ByVal sPNumber As String,  _
                    ByVal fullname As String,  _
                    ByVal email As String,  _
                    ByVal location As String,  _
                    ByVal title As String,  _
                    ByVal initials As String,  _
                    ByVal firstName As String,  _
                    ByVal surname As String,  _
                    ByVal grade As String,  _
                    ByVal unit As String,  _
                    ByVal room As String,  _
                    ByVal ext As String,  _
                    ByVal sTD As String,  _
                    ByVal jobTitle As String,  _
                    ByVal checkSum As Integer,  _
                    ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.SSOUser
            Return Sprocs.eosp_UpdateSSOUser(id, sSOUserId, sPNumber, fullname, email, location, title, initials, firstName, surname, grade, unit, room, ext, sTD, jobTitle, checkSum, transaction)
        End Function
        
        Public Overloads Function Update( _
                    ByVal id As Integer,  _
                    ByVal sSOUserId As Integer,  _
                    ByVal sPNumber As String,  _
                    ByVal fullname As String,  _
                    ByVal email As String,  _
                    ByVal location As String,  _
                    ByVal title As String,  _
                    ByVal initials As String,  _
                    ByVal firstName As String,  _
                    ByVal surname As String,  _
                    ByVal grade As String,  _
                    ByVal unit As String,  _
                    ByVal room As String,  _
                    ByVal ext As String,  _
                    ByVal sTD As String,  _
                    ByVal jobTitle As String) As Entity.SSOUser
            Return Me.Update(id, sSOUserId, sPNumber, fullname, email, location, title, initials, firstName, surname, grade, unit, room, ext, sTD, jobTitle, 0, Nothing)
        End Function
        
        Public Overloads Function Update( _
                    ByVal id As Integer,  _
                    ByVal sSOUserId As Integer,  _
                    ByVal sPNumber As String,  _
                    ByVal fullname As String,  _
                    ByVal email As String,  _
                    ByVal location As String,  _
                    ByVal title As String,  _
                    ByVal initials As String,  _
                    ByVal firstName As String,  _
                    ByVal surname As String,  _
                    ByVal grade As String,  _
                    ByVal unit As String,  _
                    ByVal room As String,  _
                    ByVal ext As String,  _
                    ByVal sTD As String,  _
                    ByVal jobTitle As String,  _
                    ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.SSOUser
            Return Me.Update(id, sSOUserId, sPNumber, fullname, email, location, title, initials, firstName, surname, grade, unit, room, ext, sTD, jobTitle, 0, transaction)
        End Function
        
        Public Overloads Function Update( _
                    ByVal id As Integer,  _
                    ByVal sSOUserId As Integer,  _
                    ByVal sPNumber As String,  _
                    ByVal fullname As String,  _
                    ByVal email As String,  _
                    ByVal location As String,  _
                    ByVal title As String,  _
                    ByVal initials As String,  _
                    ByVal firstName As String,  _
                    ByVal surname As String,  _
                    ByVal grade As String,  _
                    ByVal unit As String,  _
                    ByVal room As String,  _
                    ByVal ext As String,  _
                    ByVal sTD As String,  _
                    ByVal jobTitle As String,  _
                    ByVal checkSum As Integer) As Entity.SSOUser
            Return Me.Update(id, sSOUserId, sPNumber, fullname, email, location, title, initials, firstName, surname, grade, unit, room, ext, sTD, jobTitle, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal sSOUser As Entity.SSOUser) As Entity.SSOUser
            Return Me.Update(sSOUser.id, sSOUser(1), sSOUser(2), sSOUser(3), sSOUser(4), sSOUser(5), sSOUser(6), sSOUser(7), sSOUser(8), sSOUser(9), sSOUser(10), sSOUser(11), sSOUser(12), sSOUser(13), sSOUser(14), sSOUser(15))
        End Function
        
        Public Overloads Function Update(ByVal sSOUser As Entity.SSOUser, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.SSOUser
            Return Me.Update(sSOUser.id, sSOUser(1), sSOUser(2), sSOUser(3), sSOUser(4), sSOUser(5), sSOUser(6), sSOUser(7), sSOUser(8), sSOUser(9), sSOUser(10), sSOUser(11), sSOUser(12), sSOUser(13), sSOUser(14), sSOUser(15), transaction)
        End Function
        
        Public Overloads Function Update(ByVal sSOUser As Entity.SSOUser, ByVal checkSum As Integer) As Entity.SSOUser
            Return Me.Update(sSOUser.id, sSOUser(1), sSOUser(2), sSOUser(3), sSOUser(4), sSOUser(5), sSOUser(6), sSOUser(7), sSOUser(8), sSOUser(9), sSOUser(10), sSOUser(11), sSOUser(12), sSOUser(13), sSOUser(14), sSOUser(15), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal sSOUser As Entity.SSOUser, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.SSOUser
            Return Me.Update(sSOUser.id, sSOUser(1), sSOUser(2), sSOUser(3), sSOUser(4), sSOUser(5), sSOUser(6), sSOUser(7), sSOUser(8), sSOUser(9), sSOUser(10), sSOUser(11), sSOUser(12), sSOUser(13), sSOUser(14), sSOUser(15), checkSum, transaction)
        End Function
        
        Public Overloads Function GetByIndex_Fullname(ByVal fullname As String) As EntitySet.SSOUserSet
            Return Sprocs.eosp_SelectSSOUser(userId:=Nothing, Index_Fullname:=[fullname], Index_SPNumber:=Nothing, Index_SSOUserId:=Nothing, transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_Fullname(ByVal fullname As String, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.SSOUserSet
            Return Sprocs.eosp_SelectSSOUser(userId:=Nothing, Index_Fullname:=[fullname], Index_SPNumber:=Nothing, Index_SSOUserId:=Nothing, sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Overloads Function GetByIndex_SPNumber(ByVal sPNumber As String) As EntitySet.SSOUserSet
            Return Sprocs.eosp_SelectSSOUser(userId:=Nothing, Index_SPNumber:=[sPNumber], Index_Fullname:=Nothing, Index_SSOUserId:=Nothing, transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_SPNumber(ByVal sPNumber As String, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.SSOUserSet
            Return Sprocs.eosp_SelectSSOUser(userId:=Nothing, Index_SPNumber:=[sPNumber], Index_Fullname:=Nothing, Index_SSOUserId:=Nothing, sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Overloads Function GetByIndex_Name(ByVal fullname As String) As EntitySet.SSOUserSet
            Return Sprocs.eosp_SelectSSOUser(userId:=Nothing, Index_Fullname:=[fullname], Index_SPNumber:=Nothing, Index_SSOUserId:=Nothing, transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_Name(ByVal fullname As String, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.SSOUserSet
            Return Sprocs.eosp_SelectSSOUser(userId:=Nothing, Index_Fullname:=[fullname], Index_SPNumber:=Nothing, Index_SSOUserId:=Nothing, sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Overloads Function GetByIndex_SSOUserId(ByVal sSOUserId As Integer) As EntitySet.SSOUserSet
            Return Sprocs.eosp_SelectSSOUser(userId:=Nothing, Index_SSOUserId:=[sSOUserId], Index_Fullname:=Nothing, Index_SPNumber:=Nothing, transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_SSOUserId(ByVal sSOUserId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.SSOUserSet
            Return Sprocs.eosp_SelectSSOUser(userId:=Nothing, Index_SSOUserId:=[sSOUserId], Index_Fullname:=Nothing, Index_SPNumber:=Nothing, sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
            
            Fullname
            
            SPNumber
            
            Name
            
            SSOUserId
            
            
        End Enum
    End Class
End Namespace

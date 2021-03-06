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
    
    'Service base implementation for table 'AcquisitionTransferMethod'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class AcquisitionTransferMethodServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return true
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.AcquisitionTransferMethodSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.AcquisitionTransferMethodSet
            Return CType(MyBase.GetAll("eosp_SelectAcquisitionTransferMethod", GetType(EntitySet.AcquisitionTransferMethodSet), includeHyphen, includeInactive, orderBy),EntitySet.AcquisitionTransferMethodSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.AcquisitionTransferMethodSet
            Return Me.GetAll(includeHyphen, includeInactive, AcquisitionTransferMethodServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, AcquisitionTransferMethodServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal orderBy As OrderBy) As EntitySet.AcquisitionTransferMethodSet
            Return Me.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal acquisitionTransferMethodID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectAcquisitionTransferMethod", "AcquisitionTransferMethodID", acquisitionTransferMethodID, GetType(EntitySet.AcquisitionTransferMethodSet), tran),Entity.AcquisitionTransferMethod)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal acquisitionTransferMethodID As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(acquisitionTransferMethodID, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal acquisitionTransferMethodID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.AcquisitionTransferMethod
            Return CType(MyBase.GetById("eosp_SelectAcquisitionTransferMethod", "AcquisitionTransferMethodID", acquisitionTransferMethodID, GetType(EntitySet.AcquisitionTransferMethodSet), tran),Entity.AcquisitionTransferMethod)
        End Function
        
        Public Overloads Function GetById(ByVal acquisitionTransferMethodID As Integer) As Entity.AcquisitionTransferMethod
            Return Me.GetById(acquisitionTransferMethodID, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal acquisitionTransferMethodID As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(acquisitionTransferMethodID, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal acquisitionTransferMethodID As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeleteAcquisitionTransferMethod", "AcquisitionTransferMethodID", acquisitionTransferMethodID, checkSum, transaction)
        End Function
        
        Public Overloads Function Insert(ByVal description As String, ByVal active As Boolean, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.AcquisitionTransferMethod
            Return Me.GetById(Sprocs.eosp_CreateAcquisitionTransferMethod(description, active, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal description As String, ByVal active As Boolean) As Entity.AcquisitionTransferMethod
            Return Me.Insert(description, active, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal acquisitionTransferMethod As Entity.AcquisitionTransferMethod) As Entity.AcquisitionTransferMethod
            Return Me.Insert(acquisitionTransferMethod(1), acquisitionTransferMethod(2))
        End Function
        
        Public Overloads Function Insert(ByVal acquisitionTransferMethod As Entity.AcquisitionTransferMethod, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.AcquisitionTransferMethod
            Return Me.Insert(acquisitionTransferMethod(1), acquisitionTransferMethod(2), transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal description As String, ByVal active As Boolean, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.AcquisitionTransferMethod
            Return Sprocs.eosp_UpdateAcquisitionTransferMethod(id, description, active, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal description As String, ByVal active As Boolean) As Entity.AcquisitionTransferMethod
            Return Me.Update(id, description, active, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal description As String, ByVal active As Boolean, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.AcquisitionTransferMethod
            Return Me.Update(id, description, active, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal description As String, ByVal active As Boolean, ByVal checkSum As Integer) As Entity.AcquisitionTransferMethod
            Return Me.Update(id, description, active, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal acquisitionTransferMethod As Entity.AcquisitionTransferMethod) As Entity.AcquisitionTransferMethod
            Return Me.Update(acquisitionTransferMethod.id, acquisitionTransferMethod(1), acquisitionTransferMethod(2))
        End Function
        
        Public Overloads Function Update(ByVal acquisitionTransferMethod As Entity.AcquisitionTransferMethod, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.AcquisitionTransferMethod
            Return Me.Update(acquisitionTransferMethod.id, acquisitionTransferMethod(1), acquisitionTransferMethod(2), transaction)
        End Function
        
        Public Overloads Function Update(ByVal acquisitionTransferMethod As Entity.AcquisitionTransferMethod, ByVal checkSum As Integer) As Entity.AcquisitionTransferMethod
            Return Me.Update(acquisitionTransferMethod.id, acquisitionTransferMethod(1), acquisitionTransferMethod(2), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal acquisitionTransferMethod As Entity.AcquisitionTransferMethod, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.AcquisitionTransferMethod
            Return Me.Update(acquisitionTransferMethod.id, acquisitionTransferMethod(1), acquisitionTransferMethod(2), checkSum, transaction)
        End Function
        
        Public Overloads Function GetByIndex_IX_AcquisitionTransferMethod(ByVal description As String, ByVal includeInactive As Boolean) As EntitySet.AcquisitionTransferMethodSet
            Return Sprocs.eosp_SelectAcquisitionTransferMethod(acquisitionTransferMethodID:=Nothing, Index_Description:=[description], includeInactive:=includeInactive, transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_IX_AcquisitionTransferMethod(ByVal description As String, ByVal includeInactive As Boolean, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.AcquisitionTransferMethodSet
            Return Sprocs.eosp_SelectAcquisitionTransferMethod(acquisitionTransferMethodID:=Nothing, Index_Description:=[description], includeInactive:=includeInactive, sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
            
            IX_AcquisitionTransferMethod
            
            
        End Enum
    End Class
End Namespace

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


Namespace DataObjects.Views.Base
    
    'Service base implementation for table 'vReportJobs'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class ReportJobsServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.ReportJobsSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.ReportJobsSet
            Return CType(MyBase.GetAll("eosp_SelectReportJobs", GetType(EntitySet.ReportJobsSet), includeHyphen, includeInactive, orderBy),EntitySet.ReportJobsSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.ReportJobsSet
            Return Me.GetAll(includeHyphen, includeInactive, ReportJobsServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, ReportJobsServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
        End Enum
    End Class
End Namespace

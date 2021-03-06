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
    
    'Base entity implementation for table 'vReportPrintedJobsCount'
    '*DO NOT* modify this file.
    'Add new properties and methods to ReportPrintedJobsCount instead.
    Public MustInherit Class ReportPrintedJobsCountBase
        Inherits EnterpriseObjects.Entity
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Property PrintedCount As Integer
            Get
                If (Me.IsPrintedCountNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(0),Integer)
                End If
            End Get
            Set
                Me(0) = value
            End Set
        End Property
        
        Public Property ReportPrinterId As Integer
            Get
                Return CType(Me(1),Integer)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.ReportPrintedJobsCountService
            Get
                Return CType(GetServiceObject(GetType(Service.ReportPrintedJobsCountService)),Service.ReportPrintedJobsCountService)
            End Get
        End Property
        
        Public Overrides Sub GetObjectData(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.GetObjectData(info, context)
        End Sub
        
        Public Function IsPrintedCountNull() As Boolean
            Return Me.IsNull(0)
        End Function
        
        Public Sub SetPrintedCountToNull()
            Me(0) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(2)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.ReportPrintedJobsCountSet
            Return ReportPrintedJobsCountBase.GetAll(false, false, ReportPrintedJobsCountServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.ReportPrintedJobsCountSet
            Return ReportPrintedJobsCountBase.GetAll(includeHyphen, false, ReportPrintedJobsCountServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As ReportPrintedJobsCountServiceBase.OrderBy) As EntitySet.ReportPrintedJobsCountSet
            Dim service As Service.ReportPrintedJobsCountService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As ReportPrintedJobsCountServiceBase.OrderBy) As EntitySet.ReportPrintedJobsCountSet
            Return ReportPrintedJobsCountBase.GetAll(false, false, orderBy)
        End Function
    End Class
End Namespace

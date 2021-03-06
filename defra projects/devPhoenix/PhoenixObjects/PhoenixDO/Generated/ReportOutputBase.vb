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
    
    'Base entity implementation for table 'ReportOutput'
    '*DO NOT* modify this file.
    'Add new properties and methods to ReportOutput instead.
    Public MustInherit Class ReportOutputBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Property ReportId As Integer
            Get
                Return CType(Me(0),Integer)
            End Get
            Set
                Me(0) = value
            End Set
        End Property
        
        Public Property Image As String
            Get
                Return CType(Me(1),String)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        Public Overrides Property CheckSum As Integer Implements EnterpriseObjects.IUpdatable.CheckSum
            Get
                If (Me.IsCheckSumNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(2),Integer)
                End If
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.ReportOutputService
            Get
                Return CType(GetServiceObject(GetType(Service.ReportOutputService)),Service.ReportOutputService)
            End Get
        End Property
        
        Public Overridable Property RawDataset As System.Data.DataSet Implements EnterpriseObjects.IUpdatable.RawDataset
            Get
                Return mRawDataset
            End Get
            Set
                mRawDataset = value
            End Set
        End Property
        
        Public Overrides Sub GetObjectData(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.GetObjectData(info, context)
        End Sub
        
        Public Function IsCheckSumNull() As Boolean
            Return Me.IsNull(2)
        End Function
        
        Public Sub SetCheckSumToNull()
            Me(2) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(3)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.ReportOutputSet
            Return ReportOutputBase.GetAll(false, false, ReportOutputServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.ReportOutputSet
            Return ReportOutputBase.GetAll(includeHyphen, false, ReportOutputServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As ReportOutputServiceBase.OrderBy) As EntitySet.ReportOutputSet
            Dim service As Service.ReportOutputService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As ReportOutputServiceBase.OrderBy) As EntitySet.ReportOutputSet
            Return ReportOutputBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetForReport(ByVal reportId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ReportOutputSet
            Dim service As Service.ReportOutputService
            service = ServiceObject
            Return service.GetForReport(reportId, tran)
        End Function
        
        Public Overloads Shared Function GetForReport(ByVal reportId As Integer) As EntitySet.ReportOutputSet
            Return ReportOutputBase.GetForReport(reportId, Nothing)
        End Function
        
        Public Shared Sub Insert(ByVal reportId As Integer, ByVal image As String)
            Entity.ReportOutput.ServiceObject.Insert(reportId, image)
        End Sub
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim reportIdParam As Integer = Me.ReportId
            Dim imageParam As String = Me.Image
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Return true
        End Function
    End Class
End Namespace

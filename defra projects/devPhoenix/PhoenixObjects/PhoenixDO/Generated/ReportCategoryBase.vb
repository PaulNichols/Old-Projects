'------------------------------------------------------------------------------
' <autogenerated>
'     This code was generated by a tool.
'     Runtime Version: 1.1.4322.573
'
'     Changes to this file may cause incorrect behavior and will be lost if 
'     the code is regenerated.
' </autogenerated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On


Namespace DataObjects.Base
    
    'Base entity implementation for table 'ReportCategory'
    '*DO NOT* modify this file.
    'Add new properties and methods to ReportCategory instead.
    Public MustInherit Class ReportCategoryBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal reportCategoryId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(reportCategoryId, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal reportCategoryId As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(reportCategoryId).RawDataset.Tables(0).Rows(0))
        End Sub
        
        <EnterpriseObjects.Attributes.FieldDescription("Primary key id")>  _
        Public Property ReportCategoryId As Integer
            Get
                Return CType(Me(0),Integer)
            End Get
            Set
                Me(0) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldDescription("Primary key id")>  _
        Public Overrides Property Id As Integer
            Get
                Return CType(Me(0),Integer)
            End Get
            Set
                Me(0) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50),  _
         EnterpriseObjects.Attributes.FieldDescription("Report category description")>  _
        Public Property Description As String
            Get
                Return CType(Me(1),String)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldDescription("FK to ReportSearchReferenceType")>  _
        Public Property ReportSearchReferenceTypeId As Integer
            Get
                If (Me.IsReportSearchReferenceTypeIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(2),Integer)
                End If
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldDescription("Is this row active?")>  _
        Public Property Active As Boolean
            Get
                Return CType(Me(3),Boolean)
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        Public Overrides Property CheckSum As Integer Implements EnterpriseObjects.IUpdatable.CheckSum
            Get
                If (Me.IsCheckSumNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(4),Integer)
                End If
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.ReportCategoryService
            Get
                Return CType(GetServiceObject(GetType(Service.ReportCategoryService)),Service.ReportCategoryService)
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
        
        Public Function IsReportSearchReferenceTypeIdNull() As Boolean
            Return Me.IsNull(2)
        End Function
        
        Public Sub SetReportSearchReferenceTypeIdToNull()
            Me(2) = System.DBNull.Value
        End Sub
        
        Public Function IsCheckSumNull() As Boolean
            Return Me.IsNull(4)
        End Function
        
        Public Sub SetCheckSumToNull()
            Me(4) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(5)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.ReportCategorySet
            Return ReportCategoryBase.GetAll(false, false, ReportCategoryServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.ReportCategorySet
            Return ReportCategoryBase.GetAll(includeHyphen, false, ReportCategoryServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As ReportCategoryServiceBase.OrderBy) As EntitySet.ReportCategorySet
            Dim service As Service.ReportCategoryService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As ReportCategoryServiceBase.OrderBy) As EntitySet.ReportCategorySet
            Return ReportCategoryBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal reportCategoryId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.ReportCategory
            Dim service As Service.ReportCategoryService
            service = ServiceObject
            Return service.GetById(ReportCategoryId, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal reportCategoryId As Integer) As Entity.ReportCategory
            Dim service As Service.ReportCategoryService
            service = ServiceObject
            Return service.GetById(ReportCategoryId)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal reportCategoryId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.ReportCategoryService
            service = ServiceObject
            Return service.DeleteById(reportCategoryId, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal reportCategoryId As Integer) As Boolean
            Return ReportCategoryBase.DeleteById(reportCategoryId, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal reportCategoryId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return ReportCategoryBase.DeleteById(reportCategoryId, 0, transaction)
        End Function
        
        Public Overloads Shared Function GetForReportSearchReferenceType(ByVal reportSearchReferenceTypeId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ReportCategorySet
            Dim service As Service.ReportCategoryService
            service = ServiceObject
            Return service.GetForReportSearchReferenceType(reportSearchReferenceTypeId, tran)
        End Function
        
        Public Overloads Shared Function GetForReportSearchReferenceType(ByVal reportSearchReferenceTypeId As Integer) As EntitySet.ReportCategorySet
            Return ReportCategoryBase.GetForReportSearchReferenceType(reportSearchReferenceTypeId, Nothing)
        End Function
        
        Public Overloads Function GetRelatedReportType(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ReportTypeSet
            Return Entity.ReportType.GetForReportCategory(Me.ReportCategoryId, tran)
        End Function
        
        Public Overloads Function GetRelatedReportType() As EntitySet.ReportTypeSet
            Return Me.GetRelatedReportType(Nothing)
        End Function
        
        Public Shared Function Insert(ByVal description As String, ByVal reportSearchReferenceTypeId As Object, ByVal active As Boolean) As Entity.ReportCategory
            Return Entity.ReportCategory.ServiceObject.Insert(description, reportSearchReferenceTypeId, active)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim descriptionParam As String = Me.Description
            Dim reportSearchReferenceTypeIdParam As Object
            If (Me.IsReportSearchReferenceTypeIdNull = false) Then
                reportSearchReferenceTypeIdParam = Me.ReportSearchReferenceTypeId
            Else
                reportSearchReferenceTypeIdParam = System.DBNull.Value
            End If
            Dim activeParam As Boolean = Me.Active
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.ReportCategory.ServiceObject.Update(Me.Id, descriptionParam, reportSearchReferenceTypeIdParam, activeParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace

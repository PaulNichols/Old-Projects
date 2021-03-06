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
    
    'Base entity implementation for table 'SavedApplicationSearch'
    '*DO NOT* modify this file.
    'Add new properties and methods to SavedApplicationSearch instead.
    Public MustInherit Class SavedApplicationSearchBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal savedSearchId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(savedSearchId, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal savedSearchId As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(savedSearchId).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property SavedSearchId As Integer
            Get
                Return CType(Me(0),Integer)
            End Get
            Set
                Me(0) = value
            End Set
        End Property
        
        Public Overrides Property Id As Integer
            Get
                Return CType(Me(0),Integer)
            End Get
            Set
                Me(0) = value
            End Set
        End Property
        
        Public Property Criteria As String
            Get
                Return CType(Me(1),String)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        Public Property Modified As Date
            Get
                Return CType(Me(2),Date)
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        Public Property UserId As Decimal
            Get
                Return CType(Me(3),Decimal)
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(100)>  _
        Public Property Description As String
            Get
                Return CType(Me(4),String)
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        Public Property WorkList As Boolean
            Get
                Return CType(Me(5),Boolean)
            End Get
            Set
                Me(5) = value
            End Set
        End Property
        
        Public Overrides Property CheckSum As Integer Implements EnterpriseObjects.IUpdatable.CheckSum
            Get
                If (Me.IsCheckSumNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(6),Integer)
                End If
            End Get
            Set
                Me(6) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.SavedApplicationSearchService
            Get
                Return CType(GetServiceObject(GetType(Service.SavedApplicationSearchService)),Service.SavedApplicationSearchService)
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
            Return Me.IsNull(6)
        End Function
        
        Public Sub SetCheckSumToNull()
            Me(6) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(7)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.SavedApplicationSearchSet
            Return SavedApplicationSearchBase.GetAll(false, false, SavedApplicationSearchServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.SavedApplicationSearchSet
            Return SavedApplicationSearchBase.GetAll(includeHyphen, false, SavedApplicationSearchServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As SavedApplicationSearchServiceBase.OrderBy) As EntitySet.SavedApplicationSearchSet
            Dim service As Service.SavedApplicationSearchService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As SavedApplicationSearchServiceBase.OrderBy) As EntitySet.SavedApplicationSearchSet
            Return SavedApplicationSearchBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal savedSearchId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.SavedApplicationSearch
            Dim service As Service.SavedApplicationSearchService
            service = ServiceObject
            Return service.GetById(SavedSearchId, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal savedSearchId As Integer) As Entity.SavedApplicationSearch
            Dim service As Service.SavedApplicationSearchService
            service = ServiceObject
            Return service.GetById(SavedSearchId)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal savedSearchId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.SavedApplicationSearchService
            service = ServiceObject
            Return service.DeleteById(savedSearchId, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal savedSearchId As Integer) As Boolean
            Return SavedApplicationSearchBase.DeleteById(savedSearchId, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal savedSearchId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return SavedApplicationSearchBase.DeleteById(savedSearchId, 0, transaction)
        End Function
        
        Public Shared Function Insert(ByVal criteria As String, ByVal modified As Date, ByVal userId As Decimal, ByVal description As String, ByVal workList As Boolean) As Entity.SavedApplicationSearch
            Return Entity.SavedApplicationSearch.ServiceObject.Insert(criteria, modified, userId, description, workList)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim criteriaParam As String = Me.Criteria
            Dim modifiedParam As Date = Me.Modified
            Dim userIdParam As Decimal = Me.UserId
            Dim descriptionParam As String = Me.Description
            Dim workListParam As Boolean = Me.WorkList
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.SavedApplicationSearch.ServiceObject.Update(Me.Id, criteriaParam, modifiedParam, userIdParam, descriptionParam, workListParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace

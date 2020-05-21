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
    
    'Base entity implementation for table 'vSearchTaxonomyUsage'
    '*DO NOT* modify this file.
    'Add new properties and methods to SearchTaxonomyUsage instead.
    Public MustInherit Class SearchTaxonomyUsageBase
        Inherits EnterpriseObjects.Entity
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        <EnterpriseObjects.Attributes.FieldSize(30)>  _
        Public Property LevelOfUseDescription As String
            Get
                Return CType(Me(0),String)
            End Get
            Set
                Me(0) = value
            End Set
        End Property
        
        Public Property UsageID As Integer
            Get
                Return CType(Me(1),Integer)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        Public Property KingdomID As Integer
            Get
                Return CType(Me(2),Integer)
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        Public Property TaxonID As Integer
            Get
                Return CType(Me(3),Integer)
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        Public Property TaxonTypeID As Integer
            Get
                Return CType(Me(4),Integer)
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        Public Property UsageTypeID As Integer
            Get
                If (Me.IsUsageTypeIDNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(5),Integer)
                End If
            End Get
            Set
                Me(5) = value
            End Set
        End Property
        
        Public Property PartID As Integer
            Get
                If (Me.IsPartIDNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(6),Integer)
                End If
            End Get
            Set
                Me(6) = value
            End Set
        End Property
        
        Public Property LevelOfUseID As Integer
            Get
                If (Me.IsLevelOfUseIDNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(7),Integer)
                End If
            End Get
            Set
                Me(7) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(20)>  _
        Public Property PartDescription As String
            Get
                Return CType(Me(8),String)
            End Get
            Set
                Me(8) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(1)>  _
        Public Property UsageTypeStatus As String
            Get
                If (Me.IsUsageTypeStatusNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(9),String)
                End If
            End Get
            Set
                Me(9) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(30)>  _
        Public Property UsageTypeDescription As String
            Get
                Return CType(Me(10),String)
            End Get
            Set
                Me(10) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.SearchTaxonomyUsageService
            Get
                Return CType(GetServiceObject(GetType(Service.SearchTaxonomyUsageService)),Service.SearchTaxonomyUsageService)
            End Get
        End Property
        
        Public Overrides Sub GetObjectData(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.GetObjectData(info, context)
        End Sub
        
        Public Function IsUsageTypeIDNull() As Boolean
            Return Me.IsNull(5)
        End Function
        
        Public Sub SetUsageTypeIDToNull()
            Me(5) = System.DBNull.Value
        End Sub
        
        Public Function IsPartIDNull() As Boolean
            Return Me.IsNull(6)
        End Function
        
        Public Sub SetPartIDToNull()
            Me(6) = System.DBNull.Value
        End Sub
        
        Public Function IsLevelOfUseIDNull() As Boolean
            Return Me.IsNull(7)
        End Function
        
        Public Sub SetLevelOfUseIDToNull()
            Me(7) = System.DBNull.Value
        End Sub
        
        Public Function IsUsageTypeStatusNull() As Boolean
            Return Me.IsNull(9)
        End Function
        
        Public Sub SetUsageTypeStatusToNull()
            Me(9) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(11)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.SearchTaxonomyUsageSet
            Return SearchTaxonomyUsageBase.GetAll(false, false, SearchTaxonomyUsageServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.SearchTaxonomyUsageSet
            Return SearchTaxonomyUsageBase.GetAll(includeHyphen, false, SearchTaxonomyUsageServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As SearchTaxonomyUsageServiceBase.OrderBy) As EntitySet.SearchTaxonomyUsageSet
            Dim service As Service.SearchTaxonomyUsageService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As SearchTaxonomyUsageServiceBase.OrderBy) As EntitySet.SearchTaxonomyUsageSet
            Return SearchTaxonomyUsageBase.GetAll(false, false, orderBy)
        End Function
    End Class
End Namespace
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
    
    'Base entity implementation for table 'vTaxonomyClass'
    '*DO NOT* modify this file.
    'Add new properties and methods to TaxonomyClass instead.
    Public MustInherit Class TaxonomyClassBase
        Inherits EnterpriseObjects.Entity
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Property KingdomID As Integer
            Get
                Return CType(Me(0),Integer)
            End Get
            Set
                Me(0) = value
            End Set
        End Property
        
        Public Property TaxonID As Integer
            Get
                Return CType(Me(1),Integer)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        Public Property TaxonTypeID As Integer
            Get
                Return CType(Me(2),Integer)
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property EpithetType As String
            Get
                If (Me.IsEpithetTypeNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(3),String)
                End If
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property TaxonName As String
            Get
                Return CType(Me(4),String)
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property TaxonAuthor As String
            Get
                If (Me.IsTaxonAuthorNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(5),String)
                End If
            End Get
            Set
                Me(5) = value
            End Set
        End Property
        
        Public Property TaxonStatusID As Integer
            Get
                Return CType(Me(6),Integer)
            End Get
            Set
                Me(6) = value
            End Set
        End Property
        
        Public Property ParentKingdomID As Integer
            Get
                If (Me.IsParentKingdomIDNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(7),Integer)
                End If
            End Get
            Set
                Me(7) = value
            End Set
        End Property
        
        Public Property ParentTaxonID As Integer
            Get
                If (Me.IsParentTaxonIDNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(8),Integer)
                End If
            End Get
            Set
                Me(8) = value
            End Set
        End Property
        
        Public Property ParentTaxonTypeID As Integer
            Get
                If (Me.IsParentTaxonTypeIDNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(9),Integer)
                End If
            End Get
            Set
                Me(9) = value
            End Set
        End Property
        
        Public Overrides Property ID As Integer
            Get
                Return CType(Me(10),Integer)
            End Get
            Set
                Me(10) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(100)>  _
        Public Property Lineage As String
            Get
                If (Me.IsLineageNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(11),String)
                End If
            End Get
            Set
                Me(11) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(1)>  _
        Public Property DistributionComplete As String
            Get
                If (Me.IsDistributionCompleteNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(12),String)
                End If
            End Get
            Set
                Me(12) = value
            End Set
        End Property
        
        Public Property IsCoral As Boolean
            Get
                If (Me.IsIsCoralNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(13),Boolean)
                End If
            End Get
            Set
                Me(13) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(30)>  _
        Public Property CITESReference As String
            Get
                If (Me.IsCITESReferenceNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(14),String)
                End If
            End Get
            Set
                Me(14) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.TaxonomyClassService
            Get
                Return CType(GetServiceObject(GetType(Service.TaxonomyClassService)),Service.TaxonomyClassService)
            End Get
        End Property
        
        Public Overrides Sub GetObjectData(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.GetObjectData(info, context)
        End Sub
        
        Public Function IsEpithetTypeNull() As Boolean
            Return Me.IsNull(3)
        End Function
        
        Public Sub SetEpithetTypeToNull()
            Me(3) = System.DBNull.Value
        End Sub
        
        Public Function IsTaxonAuthorNull() As Boolean
            Return Me.IsNull(5)
        End Function
        
        Public Sub SetTaxonAuthorToNull()
            Me(5) = System.DBNull.Value
        End Sub
        
        Public Function IsParentKingdomIDNull() As Boolean
            Return Me.IsNull(7)
        End Function
        
        Public Sub SetParentKingdomIDToNull()
            Me(7) = System.DBNull.Value
        End Sub
        
        Public Function IsParentTaxonIDNull() As Boolean
            Return Me.IsNull(8)
        End Function
        
        Public Sub SetParentTaxonIDToNull()
            Me(8) = System.DBNull.Value
        End Sub
        
        Public Function IsParentTaxonTypeIDNull() As Boolean
            Return Me.IsNull(9)
        End Function
        
        Public Sub SetParentTaxonTypeIDToNull()
            Me(9) = System.DBNull.Value
        End Sub
        
        Public Function IsLineageNull() As Boolean
            Return Me.IsNull(11)
        End Function
        
        Public Sub SetLineageToNull()
            Me(11) = System.DBNull.Value
        End Sub
        
        Public Function IsDistributionCompleteNull() As Boolean
            Return Me.IsNull(12)
        End Function
        
        Public Sub SetDistributionCompleteToNull()
            Me(12) = System.DBNull.Value
        End Sub
        
        Public Function IsIsCoralNull() As Boolean
            Return Me.IsNull(13)
        End Function
        
        Public Sub SetIsCoralToNull()
            Me(13) = System.DBNull.Value
        End Sub
        
        Public Function IsCITESReferenceNull() As Boolean
            Return Me.IsNull(14)
        End Function
        
        Public Sub SetCITESReferenceToNull()
            Me(14) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(15)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.TaxonomyClassSet
            Return TaxonomyClassBase.GetAll(false, false, TaxonomyClassServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.TaxonomyClassSet
            Return TaxonomyClassBase.GetAll(includeHyphen, false, TaxonomyClassServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As TaxonomyClassServiceBase.OrderBy) As EntitySet.TaxonomyClassSet
            Dim service As Service.TaxonomyClassService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As TaxonomyClassServiceBase.OrderBy) As EntitySet.TaxonomyClassSet
            Return TaxonomyClassBase.GetAll(false, false, orderBy)
        End Function
    End Class
End Namespace

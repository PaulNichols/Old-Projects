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
    
    'Base entity implementation for table 'vDisplayAquaticDistribution'
    '*DO NOT* modify this file.
    'Add new properties and methods to DisplayAquaticDistribution instead.
    Public MustInherit Class DisplayAquaticDistributionBase
        Inherits EnterpriseObjects.Entity
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Property SpeciesAquaticDistributionID As Integer
            Get
                Return CType(Me(0),Integer)
            End Get
            Set
                Me(0) = value
            End Set
        End Property
        
        Public Property KingdomId As Integer
            Get
                Return CType(Me(1),Integer)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        Public Property TaxonID As Integer
            Get
                Return CType(Me(2),Integer)
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        Public Property TaxonTypeID As Integer
            Get
                Return CType(Me(3),Integer)
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        Public Property AquaticRegionID As Integer
            Get
                Return CType(Me(4),Integer)
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(1)>  _
        Public Property Certain As String
            Get
                If (Me.IsCertainNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(5),String)
                End If
            End Get
            Set
                Me(5) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(1)>  _
        Public Property Extinct As String
            Get
                If (Me.IsExtinctNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(6),String)
                End If
            End Get
            Set
                Me(6) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(1)>  _
        Public Property Introduced As String
            Get
                If (Me.IsIntroducedNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(7),String)
                End If
            End Get
            Set
                Me(7) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(1)>  _
        Public Property ReIntroduced As String
            Get
                If (Me.IsReIntroducedNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(8),String)
                End If
            End Get
            Set
                Me(8) = value
            End Set
        End Property
        
        Public Property SpeciesAquaticDistributionNoteID As Integer
            Get
                If (Me.IsSpeciesAquaticDistributionNoteIDNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(9),Integer)
                End If
            End Get
            Set
                Me(9) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property RegionName As String
            Get
                If (Me.IsRegionNameNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(10),String)
                End If
            End Get
            Set
                Me(10) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(30)>  _
        Public Property RegionSubName As String
            Get
                If (Me.IsRegionSubNameNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(11),String)
                End If
            End Get
            Set
                Me(11) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(15)>  _
        Public Property RegionType As String
            Get
                If (Me.IsRegionTypeNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(12),String)
                End If
            End Get
            Set
                Me(12) = value
            End Set
        End Property
        
        Public Property AquaticRegionNoteID As Integer
            Get
                If (Me.IsAquaticRegionNoteIDNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(13),Integer)
                End If
            End Get
            Set
                Me(13) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.DisplayAquaticDistributionService
            Get
                Return CType(GetServiceObject(GetType(Service.DisplayAquaticDistributionService)),Service.DisplayAquaticDistributionService)
            End Get
        End Property
        
        Public Overrides Sub GetObjectData(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.GetObjectData(info, context)
        End Sub
        
        Public Function IsCertainNull() As Boolean
            Return Me.IsNull(5)
        End Function
        
        Public Sub SetCertainToNull()
            Me(5) = System.DBNull.Value
        End Sub
        
        Public Function IsExtinctNull() As Boolean
            Return Me.IsNull(6)
        End Function
        
        Public Sub SetExtinctToNull()
            Me(6) = System.DBNull.Value
        End Sub
        
        Public Function IsIntroducedNull() As Boolean
            Return Me.IsNull(7)
        End Function
        
        Public Sub SetIntroducedToNull()
            Me(7) = System.DBNull.Value
        End Sub
        
        Public Function IsReIntroducedNull() As Boolean
            Return Me.IsNull(8)
        End Function
        
        Public Sub SetReIntroducedToNull()
            Me(8) = System.DBNull.Value
        End Sub
        
        Public Function IsSpeciesAquaticDistributionNoteIDNull() As Boolean
            Return Me.IsNull(9)
        End Function
        
        Public Sub SetSpeciesAquaticDistributionNoteIDToNull()
            Me(9) = System.DBNull.Value
        End Sub
        
        Public Function IsRegionNameNull() As Boolean
            Return Me.IsNull(10)
        End Function
        
        Public Sub SetRegionNameToNull()
            Me(10) = System.DBNull.Value
        End Sub
        
        Public Function IsRegionSubNameNull() As Boolean
            Return Me.IsNull(11)
        End Function
        
        Public Sub SetRegionSubNameToNull()
            Me(11) = System.DBNull.Value
        End Sub
        
        Public Function IsRegionTypeNull() As Boolean
            Return Me.IsNull(12)
        End Function
        
        Public Sub SetRegionTypeToNull()
            Me(12) = System.DBNull.Value
        End Sub
        
        Public Function IsAquaticRegionNoteIDNull() As Boolean
            Return Me.IsNull(13)
        End Function
        
        Public Sub SetAquaticRegionNoteIDToNull()
            Me(13) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(14)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.DisplayAquaticDistributionSet
            Return DisplayAquaticDistributionBase.GetAll(false, false, DisplayAquaticDistributionServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.DisplayAquaticDistributionSet
            Return DisplayAquaticDistributionBase.GetAll(includeHyphen, false, DisplayAquaticDistributionServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As DisplayAquaticDistributionServiceBase.OrderBy) As EntitySet.DisplayAquaticDistributionSet
            Dim service As Service.DisplayAquaticDistributionService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As DisplayAquaticDistributionServiceBase.OrderBy) As EntitySet.DisplayAquaticDistributionSet
            Return DisplayAquaticDistributionBase.GetAll(false, false, orderBy)
        End Function
    End Class
End Namespace
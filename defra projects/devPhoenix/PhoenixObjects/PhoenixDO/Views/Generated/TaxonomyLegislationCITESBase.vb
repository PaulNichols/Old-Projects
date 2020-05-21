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


Namespace DataObjects.Views.Base
    
    'Base entity implementation for table 'vTaxonomyLegislationCITES'
    '*DO NOT* modify this file.
    'Add new properties and methods to TaxonomyLegislationCITES instead.
    Public MustInherit Class TaxonomyLegislationCITESBase
        Inherits EnterpriseObjects.Entity
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Property Source As Integer
            Get
                Return CType(Me(0),Integer)
            End Get
            Set
                Me(0) = value
            End Set
        End Property
        
        Public Property LegislationID As Integer
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
        
        Public Property LegislationNameID As Integer
            Get
                Return CType(Me(5),Integer)
            End Get
            Set
                Me(5) = value
            End Set
        End Property
        
        Public Property DateListed As Date
            Get
                Return CType(Me(6),Date)
            End Get
            Set
                Me(6) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(10)>  _
        Public Property Listing As String
            Get
                Return CType(Me(7),String)
            End Get
            Set
                Me(7) = value
            End Set
        End Property
        
        Public Property IsSplitListed As Boolean
            Get
                Return CType(Me(8),Boolean)
            End Get
            Set
                Me(8) = value
            End Set
        End Property
        
        Public Property HasHigherTaxonomyProtection As Boolean
            Get
                Return CType(Me(9),Boolean)
            End Get
            Set
                Me(9) = value
            End Set
        End Property
        
        Public Property ISO2CountryID As Integer
            Get
                If (Me.IsISO2CountryIDNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(10),Integer)
                End If
            End Get
            Set
                Me(10) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(20)>  _
        Public Property Miscellaneous As String
            Get
                If (Me.IsMiscellaneousNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(11),String)
                End If
            End Get
            Set
                Me(11) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(4000)>  _
        Public Property Note As String
            Get
                If (Me.IsNoteNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(12),String)
                End If
            End Get
            Set
                Me(12) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property LegislationShortName As String
            Get
                If (Me.IsLegislationShortNameNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(13),String)
                End If
            End Get
            Set
                Me(13) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(250)>  _
        Public Property LegislationLongName As String
            Get
                If (Me.IsLegislationLongNameNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(14),String)
                End If
            End Get
            Set
                Me(14) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(1)>  _
        Public Property LegislationLevel As String
            Get
                If (Me.IsLegislationLevelNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(15),String)
                End If
            End Get
            Set
                Me(15) = value
            End Set
        End Property
        
        Public Property LegislationDateAdopted As Date
            Get
                If (Me.IsLegislationDateAdoptedNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(16),Date)
                End If
            End Get
            Set
                Me(16) = value
            End Set
        End Property
        
        Public Property LegislationDateEnforced As Date
            Get
                If (Me.IsLegislationDateEnforcedNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(17),Date)
                End If
            End Get
            Set
                Me(17) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(100)>  _
        Public Property LegislationURL As String
            Get
                If (Me.IsLegislationURLNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(18),String)
                End If
            End Get
            Set
                Me(18) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(1)>  _
        Public Property LegislationNameStatus As String
            Get
                If (Me.IsLegislationNameStatusNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(19),String)
                End If
            End Get
            Set
                Me(19) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(2)>  _
        Public Property ISO2CountryCode As String
            Get
                If (Me.IsISO2CountryCodeNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(20),String)
                End If
            End Get
            Set
                Me(20) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.TaxonomyLegislationCITESService
            Get
                Return CType(GetServiceObject(GetType(Service.TaxonomyLegislationCITESService)),Service.TaxonomyLegislationCITESService)
            End Get
        End Property
        
        Public Overrides Sub GetObjectData(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.GetObjectData(info, context)
        End Sub
        
        Public Function IsISO2CountryIDNull() As Boolean
            Return Me.IsNull(10)
        End Function
        
        Public Sub SetISO2CountryIDToNull()
            Me(10) = System.DBNull.Value
        End Sub
        
        Public Function IsMiscellaneousNull() As Boolean
            Return Me.IsNull(11)
        End Function
        
        Public Sub SetMiscellaneousToNull()
            Me(11) = System.DBNull.Value
        End Sub
        
        Public Function IsNoteNull() As Boolean
            Return Me.IsNull(12)
        End Function
        
        Public Sub SetNoteToNull()
            Me(12) = System.DBNull.Value
        End Sub
        
        Public Function IsLegislationShortNameNull() As Boolean
            Return Me.IsNull(13)
        End Function
        
        Public Sub SetLegislationShortNameToNull()
            Me(13) = System.DBNull.Value
        End Sub
        
        Public Function IsLegislationLongNameNull() As Boolean
            Return Me.IsNull(14)
        End Function
        
        Public Sub SetLegislationLongNameToNull()
            Me(14) = System.DBNull.Value
        End Sub
        
        Public Function IsLegislationLevelNull() As Boolean
            Return Me.IsNull(15)
        End Function
        
        Public Sub SetLegislationLevelToNull()
            Me(15) = System.DBNull.Value
        End Sub
        
        Public Function IsLegislationDateAdoptedNull() As Boolean
            Return Me.IsNull(16)
        End Function
        
        Public Sub SetLegislationDateAdoptedToNull()
            Me(16) = System.DBNull.Value
        End Sub
        
        Public Function IsLegislationDateEnforcedNull() As Boolean
            Return Me.IsNull(17)
        End Function
        
        Public Sub SetLegislationDateEnforcedToNull()
            Me(17) = System.DBNull.Value
        End Sub
        
        Public Function IsLegislationURLNull() As Boolean
            Return Me.IsNull(18)
        End Function
        
        Public Sub SetLegislationURLToNull()
            Me(18) = System.DBNull.Value
        End Sub
        
        Public Function IsLegislationNameStatusNull() As Boolean
            Return Me.IsNull(19)
        End Function
        
        Public Sub SetLegislationNameStatusToNull()
            Me(19) = System.DBNull.Value
        End Sub
        
        Public Function IsISO2CountryCodeNull() As Boolean
            Return Me.IsNull(20)
        End Function
        
        Public Sub SetISO2CountryCodeToNull()
            Me(20) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(21)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.TaxonomyLegislationCITESSet
            Return TaxonomyLegislationCITESBase.GetAll(false, false, TaxonomyLegislationCITESServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.TaxonomyLegislationCITESSet
            Return TaxonomyLegislationCITESBase.GetAll(includeHyphen, false, TaxonomyLegislationCITESServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As TaxonomyLegislationCITESServiceBase.OrderBy) As EntitySet.TaxonomyLegislationCITESSet
            Dim service As Service.TaxonomyLegislationCITESService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As TaxonomyLegislationCITESServiceBase.OrderBy) As EntitySet.TaxonomyLegislationCITESSet
            Return TaxonomyLegislationCITESBase.GetAll(false, false, orderBy)
        End Function
    End Class
End Namespace
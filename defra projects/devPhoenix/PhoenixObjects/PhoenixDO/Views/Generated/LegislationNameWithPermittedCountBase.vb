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
    
    'Base entity implementation for table 'vLegislationNameWithPermittedCount'
    '*DO NOT* modify this file.
    'Add new properties and methods to LegislationNameWithPermittedCount instead.
    Public MustInherit Class LegislationNameWithPermittedCountBase
        Inherits EnterpriseObjects.Entity
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Property LegislationNameID As Integer
            Get
                Return CType(Me(0),Integer)
            End Get
            Set
                Me(0) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property LegislationShortName As String
            Get
                If (Me.IsLegislationShortNameNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(1),String)
                End If
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        Public Property PermittedListingCount As Integer
            Get
                If (Me.IsPermittedListingCountNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(2),Integer)
                End If
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.LegislationNameWithPermittedCountService
            Get
                Return CType(GetServiceObject(GetType(Service.LegislationNameWithPermittedCountService)),Service.LegislationNameWithPermittedCountService)
            End Get
        End Property
        
        Public Overrides Sub GetObjectData(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.GetObjectData(info, context)
        End Sub
        
        Public Function IsLegislationShortNameNull() As Boolean
            Return Me.IsNull(1)
        End Function
        
        Public Sub SetLegislationShortNameToNull()
            Me(1) = System.DBNull.Value
        End Sub
        
        Public Function IsPermittedListingCountNull() As Boolean
            Return Me.IsNull(2)
        End Function
        
        Public Sub SetPermittedListingCountToNull()
            Me(2) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(3)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.LegislationNameWithPermittedCountSet
            Return LegislationNameWithPermittedCountBase.GetAll(false, false, LegislationNameWithPermittedCountServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.LegislationNameWithPermittedCountSet
            Return LegislationNameWithPermittedCountBase.GetAll(includeHyphen, false, LegislationNameWithPermittedCountServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As LegislationNameWithPermittedCountServiceBase.OrderBy) As EntitySet.LegislationNameWithPermittedCountSet
            Dim service As Service.LegislationNameWithPermittedCountService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As LegislationNameWithPermittedCountServiceBase.OrderBy) As EntitySet.LegislationNameWithPermittedCountSet
            Return LegislationNameWithPermittedCountBase.GetAll(false, false, orderBy)
        End Function
    End Class
End Namespace

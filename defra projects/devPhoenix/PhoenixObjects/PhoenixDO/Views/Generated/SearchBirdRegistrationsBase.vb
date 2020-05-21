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
    
    'Base entity implementation for table 'vSearchBirdRegistrations'
    '*DO NOT* modify this file.
    'Add new properties and methods to SearchBirdRegistrations instead.
    Public MustInherit Class SearchBirdRegistrationsBase
        Inherits EnterpriseObjects.Entity
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Property ApplicationId As Integer
            Get
                Return CType(Me(0),Integer)
            End Get
            Set
                Me(0) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property RequestSubmittedDate As String
            Get
                If (Me.IsRequestSubmittedDateNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(1),String)
                End If
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property KeeperId As String
            Get
                If (Me.IsKeeperIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(2),String)
                End If
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(8000)>  _
        Public Property KeeperName As String
            Get
                If (Me.IsKeeperNameNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(3),String)
                End If
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.SearchBirdRegistrationsService
            Get
                Return CType(GetServiceObject(GetType(Service.SearchBirdRegistrationsService)),Service.SearchBirdRegistrationsService)
            End Get
        End Property
        
        Public Overrides Sub GetObjectData(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.GetObjectData(info, context)
        End Sub
        
        Public Function IsRequestSubmittedDateNull() As Boolean
            Return Me.IsNull(1)
        End Function
        
        Public Sub SetRequestSubmittedDateToNull()
            Me(1) = System.DBNull.Value
        End Sub
        
        Public Function IsKeeperIdNull() As Boolean
            Return Me.IsNull(2)
        End Function
        
        Public Sub SetKeeperIdToNull()
            Me(2) = System.DBNull.Value
        End Sub
        
        Public Function IsKeeperNameNull() As Boolean
            Return Me.IsNull(3)
        End Function
        
        Public Sub SetKeeperNameToNull()
            Me(3) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(4)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.SearchBirdRegistrationsSet
            Return SearchBirdRegistrationsBase.GetAll(false, false, SearchBirdRegistrationsServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.SearchBirdRegistrationsSet
            Return SearchBirdRegistrationsBase.GetAll(includeHyphen, false, SearchBirdRegistrationsServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As SearchBirdRegistrationsServiceBase.OrderBy) As EntitySet.SearchBirdRegistrationsSet
            Dim service As Service.SearchBirdRegistrationsService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As SearchBirdRegistrationsServiceBase.OrderBy) As EntitySet.SearchBirdRegistrationsSet
            Return SearchBirdRegistrationsBase.GetAll(false, false, orderBy)
        End Function
    End Class
End Namespace
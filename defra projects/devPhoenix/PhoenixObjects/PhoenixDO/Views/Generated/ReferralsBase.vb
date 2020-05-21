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
    
    'Base entity implementation for table 'vReferrals'
    '*DO NOT* modify this file.
    'Add new properties and methods to Referrals instead.
    Public MustInherit Class ReferralsBase
        Inherits EnterpriseObjects.Entity
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Property PermitInfoId As Integer
            Get
                Return CType(Me(0),Integer)
            End Get
            Set
                Me(0) = value
            End Set
        End Property
        
        Public Property ApplicationTypeId As Integer
            Get
                Return CType(Me(1),Integer)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        Public Property ChangeDate As Date
            Get
                Return CType(Me(2),Date)
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        Public Property AssignedTo As Integer
            Get
                If (Me.IsAssignedToNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(3),Integer)
                End If
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.ReferralsService
            Get
                Return CType(GetServiceObject(GetType(Service.ReferralsService)),Service.ReferralsService)
            End Get
        End Property
        
        Public Overrides Sub GetObjectData(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.GetObjectData(info, context)
        End Sub
        
        Public Function IsAssignedToNull() As Boolean
            Return Me.IsNull(3)
        End Function
        
        Public Sub SetAssignedToToNull()
            Me(3) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(4)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.ReferralsSet
            Return ReferralsBase.GetAll(false, false, ReferralsServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.ReferralsSet
            Return ReferralsBase.GetAll(includeHyphen, false, ReferralsServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As ReferralsServiceBase.OrderBy) As EntitySet.ReferralsSet
            Dim service As Service.ReferralsService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As ReferralsServiceBase.OrderBy) As EntitySet.ReferralsSet
            Return ReferralsBase.GetAll(false, false, orderBy)
        End Function
    End Class
End Namespace
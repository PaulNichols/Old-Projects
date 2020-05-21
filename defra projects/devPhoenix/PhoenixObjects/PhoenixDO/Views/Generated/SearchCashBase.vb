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
    
    'Base entity implementation for table 'vSearchCash'
    '*DO NOT* modify this file.
    'Add new properties and methods to SearchCash instead.
    Public MustInherit Class SearchCashBase
        Inherits EnterpriseObjects.Entity
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        <EnterpriseObjects.Attributes.FieldSize(10)>  _
        Public Property PaymentReference As String
            Get
                If (Me.IsPaymentReferenceNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(0),String)
                End If
            End Get
            Set
                Me(0) = value
            End Set
        End Property
        
        Public Property PaymentAmount As Decimal
            Get
                If (Me.IsPaymentAmountNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(1),Decimal)
                End If
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        Public Property Amount As Decimal
            Get
                Return CType(Me(2),Decimal)
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(10)>  _
        Public Property SerialNumber As String
            Get
                Return CType(Me(3),String)
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        Public Property ApplicationCount As Integer
            Get
                If (Me.IsApplicationCountNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(4),Integer)
                End If
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        Public Property PaymentDateTime As Date
            Get
                If (Me.IsPaymentDateTimeNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(5),Date)
                End If
            End Get
            Set
                Me(5) = value
            End Set
        End Property
        
        Public Property PartyId As Integer
            Get
                Return CType(Me(6),Integer)
            End Get
            Set
                Me(6) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(8000)>  _
        Public Property DisplayName As String
            Get
                If (Me.IsDisplayNameNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(7),String)
                End If
            End Get
            Set
                Me(7) = value
            End Set
        End Property
        
        Public Property RemittanceCount As Integer
            Get
                If (Me.IsRemittanceCountNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(8),Integer)
                End If
            End Get
            Set
                Me(8) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.SearchCashService
            Get
                Return CType(GetServiceObject(GetType(Service.SearchCashService)),Service.SearchCashService)
            End Get
        End Property
        
        Public Overrides Sub GetObjectData(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.GetObjectData(info, context)
        End Sub
        
        Public Function IsPaymentReferenceNull() As Boolean
            Return Me.IsNull(0)
        End Function
        
        Public Sub SetPaymentReferenceToNull()
            Me(0) = System.DBNull.Value
        End Sub
        
        Public Function IsPaymentAmountNull() As Boolean
            Return Me.IsNull(1)
        End Function
        
        Public Sub SetPaymentAmountToNull()
            Me(1) = System.DBNull.Value
        End Sub
        
        Public Function IsApplicationCountNull() As Boolean
            Return Me.IsNull(4)
        End Function
        
        Public Sub SetApplicationCountToNull()
            Me(4) = System.DBNull.Value
        End Sub
        
        Public Function IsPaymentDateTimeNull() As Boolean
            Return Me.IsNull(5)
        End Function
        
        Public Sub SetPaymentDateTimeToNull()
            Me(5) = System.DBNull.Value
        End Sub
        
        Public Function IsDisplayNameNull() As Boolean
            Return Me.IsNull(7)
        End Function
        
        Public Sub SetDisplayNameToNull()
            Me(7) = System.DBNull.Value
        End Sub
        
        Public Function IsRemittanceCountNull() As Boolean
            Return Me.IsNull(8)
        End Function
        
        Public Sub SetRemittanceCountToNull()
            Me(8) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(9)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.SearchCashSet
            Return SearchCashBase.GetAll(false, false, SearchCashServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.SearchCashSet
            Return SearchCashBase.GetAll(includeHyphen, false, SearchCashServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As SearchCashServiceBase.OrderBy) As EntitySet.SearchCashSet
            Dim service As Service.SearchCashService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As SearchCashServiceBase.OrderBy) As EntitySet.SearchCashSet
            Return SearchCashBase.GetAll(false, false, orderBy)
        End Function
    End Class
End Namespace

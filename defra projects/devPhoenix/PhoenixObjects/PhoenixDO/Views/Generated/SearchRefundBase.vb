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
    
    'Base entity implementation for table 'vSearchRefund'
    '*DO NOT* modify this file.
    'Add new properties and methods to SearchRefund instead.
    Public MustInherit Class SearchRefundBase
        Inherits EnterpriseObjects.Entity
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Property RefundType As Byte
            Get
                If (Me.IsRefundTypeNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(0),Byte)
                End If
            End Get
            Set
                Me(0) = value
            End Set
        End Property
        
        Public Property RefundDateTime As Date
            Get
                If (Me.IsRefundDateTimeNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(1),Date)
                End If
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        Public Property Amount As Decimal
            Get
                If (Me.IsAmountNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(2),Decimal)
                End If
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        Public Property PaymentDateTime As Date
            Get
                If (Me.IsPaymentDateTimeNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(3),Date)
                End If
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        Public Property PartyId As Integer
            Get
                If (Me.IsPartyIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(4),Integer)
                End If
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(8000)>  _
        Public Property DisplayName As String
            Get
                If (Me.IsDisplayNameNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(5),String)
                End If
            End Get
            Set
                Me(5) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(10)>  _
        Public Property PaymentReference As String
            Get
                If (Me.IsPaymentReferenceNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(6),String)
                End If
            End Get
            Set
                Me(6) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property NotesComments As String
            Get
                If (Me.IsNotesCommentsNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(7),String)
                End If
            End Get
            Set
                Me(7) = value
            End Set
        End Property
        
        Public Property PaymentMethodId As Integer
            Get
                If (Me.IsPaymentMethodIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(8),Integer)
                End If
            End Get
            Set
                Me(8) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.SearchRefundService
            Get
                Return CType(GetServiceObject(GetType(Service.SearchRefundService)),Service.SearchRefundService)
            End Get
        End Property
        
        Public Overrides Sub GetObjectData(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.GetObjectData(info, context)
        End Sub
        
        Public Function IsRefundTypeNull() As Boolean
            Return Me.IsNull(0)
        End Function
        
        Public Sub SetRefundTypeToNull()
            Me(0) = System.DBNull.Value
        End Sub
        
        Public Function IsRefundDateTimeNull() As Boolean
            Return Me.IsNull(1)
        End Function
        
        Public Sub SetRefundDateTimeToNull()
            Me(1) = System.DBNull.Value
        End Sub
        
        Public Function IsAmountNull() As Boolean
            Return Me.IsNull(2)
        End Function
        
        Public Sub SetAmountToNull()
            Me(2) = System.DBNull.Value
        End Sub
        
        Public Function IsPaymentDateTimeNull() As Boolean
            Return Me.IsNull(3)
        End Function
        
        Public Sub SetPaymentDateTimeToNull()
            Me(3) = System.DBNull.Value
        End Sub
        
        Public Function IsPartyIdNull() As Boolean
            Return Me.IsNull(4)
        End Function
        
        Public Sub SetPartyIdToNull()
            Me(4) = System.DBNull.Value
        End Sub
        
        Public Function IsDisplayNameNull() As Boolean
            Return Me.IsNull(5)
        End Function
        
        Public Sub SetDisplayNameToNull()
            Me(5) = System.DBNull.Value
        End Sub
        
        Public Function IsPaymentReferenceNull() As Boolean
            Return Me.IsNull(6)
        End Function
        
        Public Sub SetPaymentReferenceToNull()
            Me(6) = System.DBNull.Value
        End Sub
        
        Public Function IsNotesCommentsNull() As Boolean
            Return Me.IsNull(7)
        End Function
        
        Public Sub SetNotesCommentsToNull()
            Me(7) = System.DBNull.Value
        End Sub
        
        Public Function IsPaymentMethodIdNull() As Boolean
            Return Me.IsNull(8)
        End Function
        
        Public Sub SetPaymentMethodIdToNull()
            Me(8) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(9)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.SearchRefundSet
            Return SearchRefundBase.GetAll(false, false, SearchRefundServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.SearchRefundSet
            Return SearchRefundBase.GetAll(includeHyphen, false, SearchRefundServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As SearchRefundServiceBase.OrderBy) As EntitySet.SearchRefundSet
            Dim service As Service.SearchRefundService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As SearchRefundServiceBase.OrderBy) As EntitySet.SearchRefundSet
            Return SearchRefundBase.GetAll(false, false, orderBy)
        End Function
    End Class
End Namespace

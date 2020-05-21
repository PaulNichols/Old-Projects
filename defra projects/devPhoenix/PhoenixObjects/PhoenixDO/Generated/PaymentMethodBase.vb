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
    
    'Base entity implementation for table 'PaymentMethod'
    '*DO NOT* modify this file.
    'Add new properties and methods to PaymentMethod instead.
    <EnterpriseObjects.Attributes.TableDescription("Payment Method")>  _
    Public MustInherit Class PaymentMethodBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal paymentMethodID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(paymentMethodID, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal paymentMethodID As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(paymentMethodID).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property PaymentMethodID As Integer
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
        
        <EnterpriseObjects.Attributes.FieldSize(255),  _
         EnterpriseObjects.Attributes.FieldDescription("Description")>  _
        Public Property Description As String
            Get
                Return CType(Me(1),String)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        Public Property Active As Boolean
            Get
                Return CType(Me(2),Boolean)
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property Warning As String
            Get
                If (Me.IsWarningNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(3),String)
                End If
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        Public Overrides Property CheckSum As Integer Implements EnterpriseObjects.IUpdatable.CheckSum
            Get
                If (Me.IsCheckSumNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(4),Integer)
                End If
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.PaymentMethodService
            Get
                Return CType(GetServiceObject(GetType(Service.PaymentMethodService)),Service.PaymentMethodService)
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
        
        Public Function IsWarningNull() As Boolean
            Return Me.IsNull(3)
        End Function
        
        Public Sub SetWarningToNull()
            Me(3) = System.DBNull.Value
        End Sub
        
        Public Function IsCheckSumNull() As Boolean
            Return Me.IsNull(4)
        End Function
        
        Public Sub SetCheckSumToNull()
            Me(4) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(5)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.PaymentMethodSet
            Return PaymentMethodBase.GetAll(false, false, PaymentMethodServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.PaymentMethodSet
            Return PaymentMethodBase.GetAll(includeHyphen, false, PaymentMethodServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As PaymentMethodServiceBase.OrderBy) As EntitySet.PaymentMethodSet
            Dim service As Service.PaymentMethodService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As PaymentMethodServiceBase.OrderBy) As EntitySet.PaymentMethodSet
            Return PaymentMethodBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal paymentMethodID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.PaymentMethod
            Dim service As Service.PaymentMethodService
            service = ServiceObject
            Return service.GetById(PaymentMethodID, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal paymentMethodID As Integer) As Entity.PaymentMethod
            Dim service As Service.PaymentMethodService
            service = ServiceObject
            Return service.GetById(PaymentMethodID)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal paymentMethodID As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.PaymentMethodService
            service = ServiceObject
            Return service.DeleteById(paymentMethodID, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal paymentMethodID As Integer) As Boolean
            Return PaymentMethodBase.DeleteById(paymentMethodID, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal paymentMethodID As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return PaymentMethodBase.DeleteById(paymentMethodID, 0, transaction)
        End Function
        
        Public Overloads Function GetRelatedPayment(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PaymentSet
            Return Entity.Payment.GetForPaymentMethod(Me.PaymentMethodID, tran)
        End Function
        
        Public Overloads Function GetRelatedPayment() As EntitySet.PaymentSet
            Return Me.GetRelatedPayment(Nothing)
        End Function
        
        Public Shared Function Insert(ByVal description As String, ByVal active As Boolean, ByVal warning As Object) As Entity.PaymentMethod
            Return Entity.PaymentMethod.ServiceObject.Insert(description, active, warning)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim descriptionParam As String = Me.Description
            Dim activeParam As Boolean = Me.Active
            Dim warningParam As Object
            If (Me.IsWarningNull = false) Then
                warningParam = EnterpriseObjects.Common.ParseSQLText(Me.Warning)
            Else
                warningParam = System.DBNull.Value
            End If
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.PaymentMethod.ServiceObject.Update(Me.Id, descriptionParam, activeParam, warningParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace
Imports uk.gov.defra.Phoenix.DO.DataObjects


Namespace Payments
    Public Class BOPostalOrder
        Inherits PaymentsBaseBO
        Implements IBOPostalOrder

#Region " Prelim code "
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal postalOrderId As Int32, ByVal tran As SqlClient.SqlTransaction)
            MyClass.New()
            LoadPostalOrder(postalOrderId, tran)
        End Sub

        Public Sub New(ByVal postalOrderId As Int32)
            MyClass.New(postalOrderId, Nothing)
        End Sub

        Private Function LoadPostalOrder(ByVal id As Int32) As Entity.PostalOrder
            Return LoadPostalOrder(id, Nothing)
        End Function

        Private Function LoadPostalOrder(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As Entity.PostalOrder
            Dim postalOrder As Entity.PostalOrder = Entity.PostalOrder.GetById(id)
            If postalOrder Is Nothing Then
                Throw New RecordDoesNotExist("PostalOrder", id)
            Else
                InitialisePostalOrder(postalOrder, tran)
                Return postalOrder
            End If
        End Function

        Friend Overridable Sub InitialisePostalOrder(ByVal postalOrder As Entity.PostalOrder, ByVal tran As SqlClient.SqlTransaction)
            Try
                With postalOrder
                    CheckSum = .CheckSum
                    mPostalOrderId = .Id
                    mSerialNumber = .SerialNumber
                    mAmount = .Amount
                End With
            Catch ex As Exception
            End Try
        End Sub
#End Region

#Region " Properties "


        Private mPostalOrderId As Integer
        Private mPaymentId As Integer
        Private mSerialNumber As String
        Private mAmount As Decimal

        Public Property PostalOrderId() As Integer Implements IBOPostalOrder.PostalOrderId
            Get
                Return mPostalOrderId
            End Get
            Set(ByVal Value As Integer)
                mPostalOrderId = Value
            End Set
        End Property


        Public Property PaymentId() As Integer Implements IBOPostalOrder.PaymentId
            Get
                Return mPaymentId
            End Get
            Set(ByVal Value As Integer)
                mPaymentId = Value
            End Set
        End Property

        Public Property SerialNumber() As String Implements IBOPostalOrder.SerialNumber
            Get
                Return mSerialNumber
            End Get
            Set(ByVal Value As String)
                mSerialNumber = Value
            End Set
        End Property

        Public Property Amount() As Decimal Implements IBOPostalOrder.Amount
            Get
                Return mAmount
            End Get
            Set(ByVal Value As Decimal)
                mAmount = Value
            End Set
        End Property

        Public Property AmountDisplay() As String Implements IBOPostalOrder.AmountDisplay
            Get
                Return mAmount.ToString("C")
            End Get
            Set(ByVal Value As String)
            End Set
        End Property
        Public Property RemoveX() As String
            Get
                Return "X"
            End Get
            Set(ByVal Value As String)
            End Set
        End Property
#End Region

#Region " Helper Functions "
        Public Function GetPayment() As BOPayment
            If mPaymentId = 0 Then
                Throw New ArgumentException("Payment Id is 0")
            Else
                Return New BOPayment(mPaymentId)
            End If
        End Function

#End Region

#Region " Save "
        Public Overloads Overrides Function Save() As BaseBO
            Dim postalOrder As New Entity.PostalOrder
            Dim service As service.PostalOrderService = postalOrder.ServiceObject
            Dim tran As SqlClient.SqlTransaction = service.BeginTransaction

            Dim result As BaseBO = MyClass.Save(tran)
            If result Is Nothing Then
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
            Else
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Commit)
            End If
            Return result
        End Function

        Public Overridable Overloads Function Save(ByVal tran As SqlClient.SqlTransaction) As BaseBO
            MyBase.Save()

            Dim postalOrder As New Entity.PostalOrder
            Dim service As service.PostalOrderService = postalOrder.ServiceObject
            Created = (mPostalOrderId = 0)

            If Created Then
                postalOrder = service.Insert(mPaymentId, _
                                      mAmount, _
                                      mSerialNumber, _
                                      tran)
            Else
                postalOrder = service.Update(mPostalOrderId, _
                                      mPaymentId, _
                                      mAmount, _
                                      mSerialNumber, _
                                      tran)
            End If
            'check to see if any SQL errors have occured
            If postalOrder Is Nothing Then
                CheckSqlErrors("Postal Order", tran, service)
            Else
                If Created And Not postalOrder Is Nothing Then
                    mPostalOrderId = postalOrder.Id
                End If
                'no point in initialising unless things have changed
                If postalOrder.CheckSum <> CheckSum Then InitialisePostalOrder(postalOrder, tran)

            End If

            Return Me
        End Function

#End Region

#Region " Validate "
        Public Overloads Function Validate(ByVal userID As Int64, ByVal writeFlag As Boolean) As ValidationManager
            ' init the errors list
            MyBase.ValidationErrors = New ValidationManager(ValidationManager.ValidationTitles.CannotSavePostalOrder)

            If MyBase.ValidationErrors.HasErrors Then
                'If writeFlag Then Validated = False
            Else
                'If writeFlag Then Validated = True
                MyBase.ValidationErrors = Nothing
            End If

            Return MyBase.ValidationErrors
        End Function
#End Region

#Region " Operations "

        Public Shared Sub UpdatePostalOrderByPaymentId(ByVal paymentId As Int32, ByRef items As BOPostalOrder(), ByVal tran As SqlClient.SqlTransaction)
            Dim service As New service.PostalOrderService
            Dim hashtable As hashtable = BuildHashtable(paymentId, tran)  'get all old items
            If Not items Is Nothing Then                                  'save all new/existing items
                For Each item As BOPostalOrder In items
                    item.PaymentId = paymentId
                    item.Save(tran)
                    If item.mPostalOrderId <> 0 AndAlso hashtable.ContainsKey(item.mPostalOrderId) Then
                        hashtable.Item(item.mPostalOrderId) = False 'mark old item to show updated
                    End If
                Next
            End If
            For Each item As DictionaryEntry In hashtable                 'delete any old items not updated
                If CBool(item.Value) Then
                    service.DeleteById(CInt(item.Key), 0, tran)
                End If
            Next
        End Sub


        Public Shared Function GetPostalOrderByPaymentId(ByVal paymentId As Int32, ByVal tran As SqlClient.SqlTransaction) As BOPostalOrder()
            Dim items As EntitySet.PostalOrderSet = Entity.PostalOrder.GetForPayment(paymentId, tran)
            Dim results(-1) As BOPostalOrder
            Dim index As Int32 = 0
            If Not items Is Nothing Then
                ReDim results(items.Count - 1)
                For Each item As Entity.PostalOrder In items
                    results(index) = New BOPostalOrder
                    results(index).InitialisePostalOrder(item, tran)
                    index += 1
                Next
            End If
            Return results
        End Function

        Private Shared Function BuildHashtable(ByVal paymentId As Int32, ByVal tran As SqlClient.SqlTransaction) As Hashtable
            Dim item As New entity.PostalOrder
            Dim entitySet As entitySet.PostalOrderSet = item.GetForPayment(paymentId, tran)
            Dim hashtable As New hashtable
            For Each entity As entity.PostalOrder In entitySet
                If entity.PostalOrderId <> 0 Then
                    hashtable.Add(entity.PostalOrderId, True)
                End If
            Next
            Return hashtable
        End Function

        Public Overridable Function DeletePostalOrderById(ByVal postalOrder As BOPostalOrder, ByVal tran As SqlClient.SqlTransaction) As Boolean
            Dim service As New service.PostalOrderService
            Return service.DeleteById(postalOrder.PostalOrderId, 0, tran)
        End Function


        Private Shared Function LoadByPostalOrderId(ByVal postalOrderId As Int32) As BOPostalOrder
            Return New BOPostalOrder(PostalOrderId)
        End Function

        Protected Overridable Function GetPostalOrder(ByVal postalOrderId As Int32) As BOPostalOrder
            Return LoadByPostalOrderId(PostalOrderId)
        End Function

#End Region

    End Class
End Namespace

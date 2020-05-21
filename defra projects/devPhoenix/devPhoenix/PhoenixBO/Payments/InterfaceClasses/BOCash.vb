Imports uk.gov.defra.Phoenix.DO.DataObjects


Namespace Payments
    Public Class BOCash
        Inherits PaymentsBaseBO
        Implements IBOCash

#Region " Prelim code "
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal cashId As Int32, ByVal tran As SqlClient.SqlTransaction)
            MyClass.New()
            LoadCash(cashId, tran)
        End Sub

        Public Sub New(ByVal cashId As Int32)
            MyClass.New(cashId, Nothing)
        End Sub

        Private Function LoadCash(ByVal id As Int32) As Entity.Cash
            Return LoadCash(id, Nothing)
        End Function

        Private Function LoadCash(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As Entity.Cash
            Dim cash As Entity.Cash = Entity.Cash.GetById(id)
            If cash Is Nothing Then
                Throw New RecordDoesNotExist("Cash", id)
            Else
                InitialiseCash(cash, tran)
                Return cash
            End If
        End Function

        Friend Overridable Sub InitialiseCash(ByVal cash As Entity.Cash, ByVal tran As SqlClient.SqlTransaction)
            Try
                With cash
                    CheckSum = .CheckSum
                    mCashId = .Id
                    mSerialNumber = .SerialNumber
                    mAmount = .Amount
                End With
            Catch ex As Exception
            End Try
        End Sub
#End Region

#Region " Properties "


        Private mCashId As Integer
        Private mPaymentId As Integer
        Private mSerialNumber As String
        Private mAmount As Decimal

        Public Property CashId() As Integer Implements IBOCash.CashId
            Get
                Return mCashId
            End Get
            Set(ByVal Value As Integer)
                mCashId = Value
            End Set
        End Property


        Public Property PaymentId() As Integer Implements IBOCash.PaymentId
            Get
                Return mPaymentId
            End Get
            Set(ByVal Value As Integer)
                mPaymentId = Value
            End Set
        End Property

        Public Property SerialNumber() As String Implements IBOCash.SerialNumber
            Get
                Return mSerialNumber
            End Get
            Set(ByVal Value As String)
                mSerialNumber = Value
            End Set
        End Property

        Public Property Amount() As Decimal Implements IBOCash.Amount
            Get
                Return mAmount
            End Get
            Set(ByVal Value As Decimal)
                mAmount = Value
            End Set
        End Property

        Public Property AmountDisplay() As String Implements IBOCash.AmountDisplay
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
            Dim cash As New Entity.Cash
            Dim service As service.CashService = cash.ServiceObject
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

            Dim cash As New Entity.Cash
            Dim service As service.CashService = cash.ServiceObject
            Created = (mCashId = 0)

            If Created Then
                cash = service.Insert(mPaymentId, _
                                      mAmount, _
                                      mSerialNumber, _
                                      tran)
            Else
                cash = service.Update(mCashId, _
                                      mPaymentId, _
                                      mAmount, _
                                      mSerialNumber, _
                                      tran)
            End If
            'check to see if any SQL errors have occured
            If cash Is Nothing Then
                CheckSqlErrors("Cash", tran, service)
            Else
                If Created And Not cash Is Nothing Then
                    mCashId = cash.Id
                End If
                'no point in initialising unless things have changed
                If cash.CheckSum <> CheckSum Then InitialiseCash(cash, tran)

            End If

            Return Me
        End Function

#End Region

#Region " Validate "
        Public Overloads Function Validate(ByVal userID As Int64, ByVal writeFlag As Boolean) As ValidationManager
            ' init the errors list
            MyBase.ValidationErrors = New ValidationManager(ValidationManager.ValidationTitles.CannotSaveCash)


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

        Public Shared Sub UpdateCashByPaymentId(ByVal paymentId As Int32, ByRef items As BOCash(), ByVal tran As SqlClient.SqlTransaction)
            Dim service As New service.CashService
            Dim hashtable As hashtable = BuildHashtable(paymentId, tran)  'get all old items
            If Not items Is Nothing Then                                  'save all new/existing items
                For Each item As BOCash In items
                    item.PaymentId = paymentId
                    item.Save(tran)
                    If item.mCashId <> 0 AndAlso hashtable.ContainsKey(item.mCashId) Then
                        hashtable.Item(item.mCashId) = False 'mark old item to show updated
                    End If
                Next
            End If
            For Each item As DictionaryEntry In hashtable                 'delete any old items not updated
                If CBool(item.Value) Then
                    service.DeleteById(CInt(item.Key), 0, tran)
                End If
            Next
        End Sub


        Public Shared Function GetCashByPaymentId(ByVal paymentId As Int32, ByVal tran As SqlClient.SqlTransaction) As BOCash()
            Dim items As EntitySet.CashSet = Entity.Cash.GetForPayment(paymentId, tran)
            Dim results(-1) As BOCash
            Dim index As Int32 = 0
            If Not items Is Nothing Then
                ReDim results(items.Count - 1)
                For Each item As Entity.Cash In items
                    results(index) = New BOCash
                    results(index).InitialiseCash(item, tran)
                    index += 1
                Next
            End If
            Return results
        End Function

        Private Shared Function BuildHashtable(ByVal paymentId As Int32, ByVal tran As SqlClient.SqlTransaction) As Hashtable
            Dim item As New entity.Cash
            Dim entitySet As entitySet.CashSet = item.GetForPayment(paymentId, tran)
            Dim hashtable As New hashtable
            For Each entity As entity.Cash In entitySet
                If entity.CashId <> 0 Then
                    hashtable.Add(entity.CashId, True)
                End If
            Next
            Return hashtable
        End Function

        Public Overridable Function DeleteCashById(ByVal cash As BOCash, ByVal tran As SqlClient.SqlTransaction) As Boolean
            Dim service As New service.CashService
            Return service.DeleteById(cash.CashId, 0, tran)
        End Function


        Private Shared Function LoadByCashId(ByVal cashId As Int32) As BOCash
            Return New BOCash(CashId)
        End Function

        Protected Overridable Function GetCash(ByVal cashId As Int32) As BOCash
            Return LoadByCashId(CashId)
        End Function

#End Region

    End Class
End Namespace
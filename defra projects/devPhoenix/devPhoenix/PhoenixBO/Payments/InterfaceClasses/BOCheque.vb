Imports uk.gov.defra.Phoenix.DO.DataObjects

Namespace Payments
    Public Class BOCheque
        Inherits PaymentsBaseBO
        Implements IBOCheque
#Region " Prelim code "
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal chequeId As Int32, ByVal tran As SqlClient.SqlTransaction)
            MyClass.New()
            LoadCheque(chequeId, tran)
        End Sub

        Public Sub New(ByVal chequeId As Int32)
            MyClass.New(chequeId, Nothing)
        End Sub

        Private Function LoadCheque(ByVal id As Int32) As Entity.Cheque
            Return LoadCheque(id, Nothing)
        End Function

        Private Function LoadCheque(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As Entity.Cheque
            Dim cheque As Entity.Cheque = Entity.Cheque.GetById(id)
            If cheque Is Nothing Then
                Throw New RecordDoesNotExist("Cheque", id)
            Else
                InitialiseCheque(cheque, tran)
                Return cheque
            End If
        End Function

        Friend Overridable Sub InitialiseCheque(ByVal cheque As Entity.Cheque, ByVal tran As SqlClient.SqlTransaction)
            Try
                With cheque
                    CheckSum = .CheckSum
                    mChequeId = .Id

                    If Not .IsBankAccountNumberNull Then mBankAccountNumber = .BankAccountNumber
                    If Not .IsBankSortCodeNull Then mBankSortCode = .BankSortCode
                    If Not .IsSerialNumberNull Then mSerialNumber = .SerialNumber
                    If Not .IsAccountNameNull Then mAccountName = .AccountName
                    If Not .IsAmountNull Then mAmount = .Amount
                End With
            Catch ex As Exception
            End Try
        End Sub
#End Region

#Region " Properties "

        Private mChequeId As Integer
        Private mPaymentId As Integer
        Private mBankSortCode As String
        Private mBankAccountNumber As String
        Private mSerialNumber As String
        Private mAccountName As String
        Private mAmount As Decimal

        Public Property ChequeId() As Integer Implements IBOCheque.ChequeId
            Get
                Return mChequeId
            End Get
            Set(ByVal Value As Integer)
                mChequeId = Value
            End Set
        End Property


        Public Property BankAccountNumber() As String Implements IBOCheque.BankAccountNumber
            Get
                Return mBankAccountNumber
            End Get
            Set(ByVal Value As String)
                mBankAccountNumber = Value
            End Set
        End Property

        Public Property PaymentId() As Integer Implements IBOCheque.PaymentId
            Get
                Return mPaymentId
            End Get
            Set(ByVal Value As Integer)
                mPaymentId = Value
            End Set
        End Property

        Public Property BankSortCode() As String Implements IBOCheque.BankSortCode
            Get
                Return mBankSortCode
            End Get
            Set(ByVal Value As String)
                mBankSortCode = Value
            End Set
        End Property

        Public Property SerialNumber() As String Implements IBOCheque.SerialNumber
            Get
                Return mSerialNumber
            End Get
            Set(ByVal Value As String)
                mSerialNumber = Value
            End Set
        End Property

        Public Property AccountName() As String Implements IBOCheque.AccountName
            Get
                Return mAccountName
            End Get
            Set(ByVal Value As String)
                mAccountName = Value
            End Set
        End Property

        Public Property Amount() As Decimal Implements IBOCheque.Amount
            Get
                Return mAmount
            End Get
            Set(ByVal Value As Decimal)
                mAmount = Value
            End Set
        End Property

        Public Property AmountDisplay() As String Implements IBOCheque.AmountDisplay
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
        Public Function GetCheque() As BOCheque
            If mChequeId = 0 Then
                Throw New ArgumentException("Cheque Id is 0")
            Else
                Return New BOCheque(mChequeId)
            End If
        End Function

#End Region

#Region " Save "
        Public Overloads Overrides Function Save() As BaseBO
            Dim cheque As New Entity.Cheque
            Dim service As service.ChequeService = cheque.ServiceObject
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

            Dim cheque As New Entity.Cheque
            Dim service As service.ChequeService = cheque.ServiceObject
            Created = (mChequeId = 0)

            If Created Then
                cheque = service.Insert(mPaymentId, _
                    mBankSortCode, _
                    mBankAccountNumber, _
                    mSerialNumber, _
                    mAccountName, _
                    mAmount, _
                    tran)
            Else
                cheque = service.Update(mChequeId, _
                    mPaymentId, _
                    mBankSortCode, _
                    mBankAccountNumber, _
                    mSerialNumber, _
                    mAccountName, _
                    mAmount, _
                    tran)
            End If
            'check to see if any SQL errors have occured
            If cheque Is Nothing Then
                CheckSqlErrors("Cheque", tran, service)
            Else
                If Created And Not cheque Is Nothing Then
                    mChequeId = cheque.Id
                End If
                'no point in initialising unless things have changed
                If cheque.CheckSum <> CheckSum Then InitialiseCheque(cheque, tran)

            End If

            Return Me
        End Function

#End Region

#Region " Validate "
        Public Overloads Function Validate(ByVal userID As Int64, ByVal writeFlag As Boolean) As ValidationManager
            ' init the errors list
            MyBase.ValidationErrors = New ValidationManager(ValidationManager.ValidationTitles.CannotSaveCheque)

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

        Public Shared Sub UpdateChequesByPaymentId(ByVal paymentId As Int32, ByRef cheques As BOCheque(), ByVal tran As SqlClient.SqlTransaction)
            Dim service As New service.ChequeService
            Dim hashtable As hashtable = BuildHashtable(paymentId, tran)    'get all old items
            If Not cheques Is Nothing Then                                  'save all new/existing items
                For Each cheque As BOCheque In cheques
                    cheque.PaymentId = paymentId
                    cheque.Save(tran)
                    If cheque.mChequeId <> 0 AndAlso hashtable.ContainsKey(cheque.mChequeId) Then
                        hashtable.Item(cheque.mChequeId) = False            'mark old item to show updated
                    End If
                Next
            End If
            For Each item As DictionaryEntry In hashtable                   'delete any old items not updated
                If CBool(item.Value) Then
                    service.DeleteById(CInt(item.Key), 0, tran)
                End If
            Next
        End Sub

        Public Shared Function GetChequesByPaymentId(ByVal paymentId As Int32, ByVal tran As SqlClient.SqlTransaction) As BOCheque()
            Dim items As EntitySet.ChequeSet = Entity.Cheque.GetForPayment(paymentId, tran)
            Dim results(-1) As BOCheque
            Dim index As Int32 = 0
            If Not items Is Nothing Then
                ReDim results(items.Count - 1)
                For Each item As Entity.Cheque In items
                    results(index) = New BOCheque
                    results(index).InitialiseCheque(item, tran)
                    index += 1
                Next
            End If
            Return results
        End Function

        Private Shared Function BuildHashtable(ByVal paymentId As Int32, ByVal tran As SqlClient.SqlTransaction) As Hashtable
            Dim cheque As New entity.Cheque
            Dim entitySet As entitySet.ChequeSet = cheque.GetForPayment(paymentId, tran)
            Dim hashtable As New hashtable
            For Each entity As entity.Cheque In entitySet
                If entity.ChequeId <> 0 Then
                    hashtable.Add(entity.ChequeId, True)
                End If
            Next
            Return hashtable
        End Function

        Public Overridable Function DeleteChequeById(ByVal cheque As BOCheque, ByVal tran As SqlClient.SqlTransaction) As Boolean
            Dim service As New service.ChequeService
            Return service.DeleteById(cheque.ChequeId, 0, tran)
        End Function


        Private Shared Function LoadByChequeId(ByVal chequeId As Int32) As BOCheque
            Return New BOCheque(chequeId)
        End Function

        Protected Overridable Function GetCheque(ByVal chequeId As Int32) As BOCheque
            Return LoadByChequeId(PaymentId)
        End Function

#End Region
    End Class
End Namespace



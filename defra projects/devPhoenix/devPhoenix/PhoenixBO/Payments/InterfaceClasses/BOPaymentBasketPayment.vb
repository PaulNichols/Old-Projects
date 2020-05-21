Imports uk.gov.defra.Phoenix.DO.DataObjects

Namespace Payments
    Public Class BOPaymentBasketPayment
        Inherits PaymentsBaseBO
        Implements IBOPaymentBasketPayment
#Region " Prelim code "
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal paymentId As Int32, ByVal paymentBasketId As Int32, ByVal tran As SqlClient.SqlTransaction)
            MyClass.New()
            LoadPBP(paymentId, paymentBasketId, tran)
        End Sub

        Public Sub New(ByVal paymentId As Int32, ByVal paymentBasketId As Int32)
            MyClass.New(paymentId, paymentBasketId, Nothing)
        End Sub

        Private Function LoadPBP(ByVal paymentId As Int32, ByVal paymentBasketId As Int32) As DataObjects.Entity.PaymentBasketPayment
            Return LoadPBP(paymentId, paymentBasketId, Nothing)
        End Function

        Private Function LoadPBP(ByVal paymentId As Int32, ByVal paymentBasketId As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.PaymentBasketPayment
            Dim pbp As DataObjects.Entity.PaymentBasketPayment = DataObjects.Entity.PaymentBasketPayment.GetById(paymentId, paymentBasketId)
            If pbp Is Nothing Then
                Throw New RecordDoesNotExist("PaymentBasketPayment, payment id ", paymentId)
            Else
                InitialisePBP(pbp, tran)
                Return pbp
            End If
        End Function

        Friend Overridable Sub InitialisePBP(ByVal pbp As DataObjects.Entity.PaymentBasketPayment, ByVal tran As SqlClient.SqlTransaction)
            Try
                With pbp
                    Dim payment As New Entity.Payment(.PaymentId)
                    CheckSum = .CheckSum
                    mPaymentId = .PaymentId
                    mPaymentBasketId = .PaymentBasketId
                    mPaymentRef = payment.PaymentReference
                    mAmount = 0
                    If Not .IsAmountNull Then mAmount = .Amount
                    If Not .IsLinkDateTimeNull Then mLinkDateTime = .LinkDateTime
                End With
            Catch ex As Exception
            End Try
        End Sub
#End Region

#Region " Properties "
        Private mPaymentBasketId As Integer
        Private mPaymentId As Integer
        Private mPaymentRef As String
        Private mLinkDateTime As Date
        Private mAmount As Decimal

        Public Property PaymentBasketId() As Integer Implements IBOPaymentBasketPayment.PaymentBasketId
            Get
                Return mPaymentBasketId
            End Get
            Set(ByVal Value As Integer)
                mPaymentBasketId = Value
            End Set
        End Property


        Public Property PaymentId() As Int32 Implements IBOPaymentBasketPayment.PaymentId
            Get
                Return mPaymentId
            End Get
            Set(ByVal Value As Int32)
                mPaymentId = Value
            End Set
        End Property

        Public Property PaymentReference() As String Implements IBOPaymentBasketPayment.PaymentReference
            Get
                Return mPaymentRef
            End Get
            Set(ByVal Value As String)
                mPaymentRef = Value
            End Set
        End Property

        Public Property LinkDateTime() As Date Implements IBOPaymentBasketPayment.LinkDateTime
            Get
                Return mLinkDateTime
            End Get
            Set(ByVal Value As Date)
                mLinkDateTime = Value
            End Set
        End Property

        Public Property Amount() As Decimal Implements IBOPaymentBasketPayment.Amount
            Get
                Return mAmount
            End Get
            Set(ByVal Value As Decimal)
                mAmount = Value
            End Set
        End Property

#End Region

#Region " Helper Functions "
        Public Function GetPaymentBasketPayment() As BOPaymentBasketPayment
            If mPaymentBasketId = 0 Then
                Throw New ArgumentException("PaymentBasket Id is 0")
            ElseIf mPaymentId = 0 Then
                Throw New ArgumentException("Payment Id is 0")
            Else
                Return New BOPaymentBasketPayment(mPaymentId, mPaymentBasketId)
            End If
        End Function

        Public Shared Function GetLinkedPaymentsByBasketId(ByVal basketId As Int32, ByVal userId As Int64) As BOPaymentBasketPayment()
            Try
                Dim pbp As New Entity.PaymentBasketPayment
                Dim service As service.PaymentBasketPaymentService = pbp.ServiceObject
                Dim entitySet As entitySet.PaymentBasketPaymentSet = service.GetForPaymentBasket(basketId)
                Dim results(-1) As BOPaymentBasketPayment

                InsertIntoLog(userId, "Payment Basket Id: " & basketId.ToString())
                If Not entitySet Is Nothing Then
                    ReDim results(entitySet.Count - 1)
                    Dim index As Int32 = 0
                    For Each item As Entity.PaymentBasketPayment In entitySet
                        Dim result As New BOPaymentBasketPayment
                        result.InitialisePBP(item, Nothing)
                        results(index) = result
                        index += 1
                    Next
                End If
                Return results
            Catch noRecord As RecordDoesNotExist
                Return Nothing
            Catch ex As Exception
                Throw
            End Try
        End Function

#End Region

#Region " Save "
        Public Overloads Overrides Function Save() As BaseBO
            Dim basket As New Entity.PaymentBasketPayment
            Dim service As service.PaymentBasketPaymentService = basket.ServiceObject
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

            Dim pbp As New Entity.PaymentBasketPayment
            Dim service As service.PaymentBasketPaymentService = pbp.ServiceObject
            Dim create As Boolean = pbp.GetById(mPaymentId, mPaymentBasketId) Is Nothing

            If create Then
                service.Insert(mPaymentId, _
                    mPaymentBasketId, _
                    mLinkDateTime, _
                    mAmount, _
                    tran)
            Else
                service.Update(mPaymentId, _
                    mPaymentBasketId, _
                    mLinkDateTime, _
                    mAmount, _
                    tran)
            End If
            'check to see if any SQL errors have occured
            If Not DataObjects.Sprocs.LastError Is Nothing Then
                'TODO: Use errors collection to check to see if the problem was concurrency
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSavePaymentBasketPayment)
            End If
            Return Me
        End Function

#End Region

#Region " Validate "
        Public Overloads Function Validate(ByVal userID As Int64, ByVal writeFlag As Boolean) As ValidationManager
            ' init the errors list
            MyBase.ValidationErrors = New ValidationManager(ValidationManager.ValidationTitles.CannotSavePaymentBasketPayment)
            If Not MyBase.ValidationErrors.HasErrors Then
                MyBase.ValidationErrors = Nothing
            End If
            Return MyBase.ValidationErrors
        End Function
#End Region

#Region " Operations "

        Public Shadows Function Delete() As Boolean
            Return Entity.PaymentBasketPayment.DeleteById(mPaymentId, mPaymentBasketId)
        End Function

#End Region
    End Class
End Namespace




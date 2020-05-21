Imports uk.gov.defra.Phoenix.DO.DataObjects

Namespace Payments
    Public Class BOPaymentBasket
        Inherits PaymentsBaseBO
        Implements IBOPaymentBasket

        Private Class AmountComparer
            Implements IComparer

            Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
                Dim xlink As BOPaymentBasketPayment = CType(x, BOPaymentBasketPayment)
                Dim ylink As BOPaymentBasketPayment = CType(y, BOPaymentBasketPayment)
                Return Decimal.Compare(xlink.Amount, ylink.Amount)
            End Function
        End Class


#Region " Prelim code "
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal basketId As Int32, ByVal tran As SqlClient.SqlTransaction)
            MyClass.New()
            LoadPaymentBasket(basketId, tran)
        End Sub

        Public Sub New(ByVal basketId As Int32)
            MyClass.New(basketId, Nothing)
        End Sub

        Private Function LoadPaymentBasket(ByVal id As Int32) As DataObjects.Entity.PaymentBasket
            Return LoadPaymentBasket(id, Nothing)
        End Function

        Private Function LoadPaymentBasket(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.PaymentBasket
            Dim basket As DataObjects.Entity.PaymentBasket = DataObjects.Entity.PaymentBasket.GetById(id)
            If basket Is Nothing Then
                Throw New RecordDoesNotExist("PaymentBasket", id)
            Else
                InitialisePaymentBasket(basket, tran)
                Return basket
            End If
        End Function

        Friend Overridable Sub InitialisePaymentBasket(ByVal basket As DataObjects.Entity.PaymentBasket, ByVal tran As SqlClient.SqlTransaction)
            Try
                With basket
                    CheckSum = .CheckSum
                    mPaymentBasketId = .Id
                    mPartyId = .PartyId
                    mBasketClosed = .BasketClosed
                    If Not .IsTotalFeeChargedNull Then mTotalFeeCharged = .TotalFeeCharged
                    If Not .IsRemittanceAdvicePrintedNull Then mRemittanceAdvicePrinted = .RemittanceAdvicePrinted
                    If Not .IsRemittanceAdviceDateTimeNull Then mRemittanceAdviceDateTime = .RemittanceAdviceDateTime
                    If Not .IsRemittanceAdvicePaymentIdNull Then mRemittanceAdvicePaymentId = .RemittanceAdvicePaymentId
                End With
            Catch ex As Exception
            End Try
        End Sub
#End Region

#Region " Properties "
        Private mPaymentBasketId As Integer
        Private mPartyId As Integer
        Private mTotalFeeCharged As Decimal
        Private mBasketClosed As Boolean = False
        Private mBasketIsTopup As Boolean = False
        Private mTopupReference As String
        Private mTopupAmount As Decimal
        Private mRemittanceAdvicePrinted As Boolean
        Private mRemittanceAdviceDateTime As Date
        Private mRemittanceAdvicePaymentId As Integer = 0
        Private mRemittanceAdvicePayment As BOPayment

        Public Property PaymentBasketId() As Integer Implements IBOPaymentBasket.PaymentBasketId
            Get
                Return mPaymentBasketId
            End Get
            Set(ByVal Value As Integer)
                mPaymentBasketId = Value
            End Set
        End Property


        Public Property BasketClosed() As Boolean Implements IBOPaymentBasket.BasketClosed
            Get
                Return mBasketClosed
            End Get
            Set(ByVal Value As Boolean)
                mBasketClosed = Value
            End Set
        End Property

        Public Property BasketIsTopup() As Boolean Implements IBOPaymentBasket.BasketIsTopup
            Get
                Return mBasketIsTopup
            End Get
            Set(ByVal Value As Boolean)
                mBasketIsTopup = Value
            End Set
        End Property

        Public Property TopupReference() As String Implements IBOPaymentBasket.TopupReference
            Get
                Return mTopupReference
            End Get
            Set(ByVal Value As String)
                mTopupReference = Value
            End Set
        End Property

        Public Property TopupAmount() As Decimal Implements IBOPaymentBasket.TopupAmount
            Get
                Return mTopupAmount
            End Get
            Set(ByVal Value As Decimal)
                mTopupAmount = Value
            End Set
        End Property

        Public Property PartyId() As Integer Implements IBOPaymentBasket.PartyId
            Get
                Return mPartyId
            End Get
            Set(ByVal Value As Integer)
                mPartyId = Value
            End Set
        End Property

        Public Property TotalFeeCharged() As Decimal Implements IBOPaymentBasket.TotalFeeCharged
            Get
                Return mTotalFeeCharged
            End Get
            Set(ByVal Value As Decimal)
                mTotalFeeCharged = Value
            End Set
        End Property

        Public Property RemittanceAdvicePrinted() As Boolean Implements IBOPaymentBasket.RemittanceAdvicePrinted
            Get
                Return mRemittanceAdvicePrinted
            End Get
            Set(ByVal Value As Boolean)
                mRemittanceAdvicePrinted = Value
            End Set
        End Property

        Public Property RemittanceAdviceDateTime() As Date Implements IBOPaymentBasket.RemittanceAdviceDateTime
            Get
                Return mRemittanceAdviceDateTime
            End Get
            Set(ByVal Value As Date)
                mRemittanceAdviceDateTime = Value
            End Set
        End Property

        Public Property RemittanceAdvicePaymentId() As Integer Implements IBOPaymentBasket.RemittanceAdvicePaymentId
            Get
                Return mRemittanceAdvicePaymentId
            End Get
            Set(ByVal Value As Integer)
                mRemittanceAdvicePaymentId = Value
            End Set
        End Property

        Public Property RemittanceAdvicePaymentRef() As String Implements IBOPaymentBasket.RemittanceAdvicePaymentRef
            Get
                Dim payment As BOPayment = RemittanceAdvicePayment
                If payment Is Nothing
                    Return ""
                End If
                Return payment.PaymentReference
            End Get
            Set(ByVal Value As String)
            End Set
        End Property


#End Region

#Region " Helper Functions "
        Public Function GetPaymentBasket() As BOPaymentBasket
            If mPaymentBasketId = 0 Then
                Throw New ArgumentException("PaymentBasket Id is 0")
            Else
                Return New BOPaymentBasket(mPaymentBasketId)
            End If
        End Function

        Public ReadOnly Property RemittanceAdvicePayment() As BOPayment
            Get
                If mRemittanceAdvicePayment Is Nothing AndAlso mRemittanceAdvicePaymentId <> 0 Then
                    mRemittanceAdvicePayment = New BOPayment(mRemittanceAdvicePaymentId)
                End If
                Return mRemittanceAdvicePayment
            End Get
        End Property

        Public Shared Function GetBasketsByPaymentId(ByVal paymentId As Int32, ByVal searchingUserId As Int64) As BOPaymentBasket()
            Try
                Dim basket As New Entity.PaymentBasket
                Dim pbp As New Entity.PaymentBasketPayment
                Dim service1 As Service.PaymentBasketService = basket.ServiceObject
                Dim service2 As Service.PaymentBasketPaymentService = pbp.ServiceObject
                Dim entitySet1 As EntitySet.PaymentBasketSet = service1.GetForPayment(paymentId)
                Dim entitySet2 As EntitySet.PaymentBasketPaymentSet = service2.GetForPayment(paymentId)
                Dim recsFound As Int32 = 0
                Dim index As Int32 = 0
                Dim results(-1) As BOPaymentBasket

                InsertIntoLog(searchingUserId, "Payment Id: " & paymentId.ToString())
                If Not entitySet1 Is Nothing Then
                    recsFound += entitySet1.Count
                End If
                If Not entitySet2 Is Nothing Then
                    recsFound += entitySet2.Count
                End If
                ReDim results(recsFound - 1)
                If Not entitySet1 Is Nothing Then
                    FillBaskets(entitySet1, results, index, True)    ' all closed
                    FillBaskets(entitySet1, results, index, False)   ' all open (should be zero or one)
                End If
                If Not entitySet2 Is Nothing Then
                    FillBaskets(entitySet2, results, index) ' all linked
                End If
                Return results
            Catch noRecord As RecordDoesNotExist
                Dim empty(-1) As BOPaymentBasket
                Return empty
            Catch ex As Exception
                Throw
            End Try
        End Function

        Private Overloads Shared Sub FillBaskets(ByRef entitySet As EntitySet.PaymentBasketSet, ByRef results() As BOPaymentBasket, ByRef index As Int32, ByVal closed As Boolean)
            For Each item As Entity.PaymentBasket In entitySet
                If item.BasketClosed = closed Then
                    Dim result As New BOPaymentBasket
                    result.InitialisePaymentBasket(item, Nothing)
                    results(index) = result
                    index += 1
                End If
            Next
        End Sub

        Private Overloads Shared Sub FillBaskets(ByRef entitySet As EntitySet.PaymentBasketPaymentSet, ByRef results() As BOPaymentBasket, ByRef index As Int32)
            For Each item As Entity.PaymentBasketPayment In entitySet
                Dim result As New BOPaymentBasket(item.PaymentBasketId)
                Dim payment As BOPayment = result.RemittanceAdvicePayment

                result.mBasketIsTopup = True
                result.mTopupAmount = 0
                If Not item.IsAmountNull Then
                    result.mTopupAmount = item.Amount
                End If
                If Not payment Is Nothing Then
                    result.mTopupReference = payment.PaymentReference
                End If
                results(index) = result
                index += 1
            Next
        End Sub

        Public Function GetLinkedApplications(ByVal searchingUserId As Int64) As BOApplicationSummary()
            Return GetApplicationsByBasket(mPaymentBasketId, searchingUserId)
        End Function

        Public Function GetUnlinkedApplications(ByVal searchingUserId As Int64) As BOApplicationSummary()
            Return GetApplicationsByBasket(0, searchingUserId)
        End Function

        Private Function GetApplicationsForParty(ByVal searchingUserId As Int64) As Entity.Application()
            Dim party As New Entity.Party(mPartyId)
            Dim linkset As EntitySet.PartyLinkSet = party.GetRelatedPartyLink

            InsertIntoLog(searchingUserId, "Apps for Party Id: " & PartyId.ToString())
            If Not linkset Is Nothing Then
                Dim app As New Entity.Application
                Dim linkapps(linkset.Count - 1) As EntitySet.ApplicationSet
                Dim index As Int32 = 0
                Dim count As Int32 = 0
                For Each item As Entity.PartyLink In linkset
                    linkapps(index) = app.GetForPartyLinkIdPartyLink(item.PartyLinkId)
                    count += linkapps(index).Count
                    index += 1
                Next
                Dim results(count - 1) As Entity.Application
                index = 0
                For Each item As EntitySet.ApplicationSet In linkapps
                    For Each subitem As Entity.Application In item
                        results(index) = subitem
                        index += 1
                    Next
                Next
                Return results
            End If
            Return Nothing
        End Function

        Private Function GetApplicationsByBasket(ByVal basketId As Int32, ByVal searchingUserId As Int64) As BOApplicationSummary()
            Try
                Dim allApps As Entity.Application() = GetApplicationsForParty(searchingUserId)
                Dim count As Int32 = 0
                For Each app As Entity.Application In allApps
                    If app.PaymentBasketId = basketId Then
                        count += 1
                    End If
                Next
                Dim results(count - 1) As BOApplicationSummary
                count = 0
                For Each app As Entity.Application In allApps
                    If app.PaymentBasketId = basketId Then
                        results(count) = New BOApplicationSummary(app.ApplicationId)
                        count += 1
                    End If
                Next
                Return results
            Catch noRecord As RecordDoesNotExist
                Return Nothing
            End Try
        End Function

        Public Sub AddApplications(ByVal applicationIds As Int32(), ByVal userId As Int64)
            For Each applicationId As Int32 In applicationIds
                Dim application As New Entity.Application(applicationId)
                application.PaymentBasketId = mPaymentBasketId
                application.SaveChanges()
                InsertIntoLog(userId, "Add Application to Basket, app Id: " & applicationId.ToString())
            Next
            UpdatePayment(userId, False)
        End Sub

        Public Sub RemoveApplication(ByVal applicationId As Int32, ByVal userId As Int64)
            Dim app As New Entity.Application(applicationId)
            Dim feeCharged As Decimal = app.FeeCharged
            Dim links As BOPaymentBasketPayment() = BOPaymentBasketPayment.GetLinkedPaymentsByBasketId(mPaymentBasketId, userId)

            app.SetPaymentBasketIdToNull()
            app.PaymentStatusId = Application.PaymentStatusTypes.Unpaid
            app.SaveChanges()
            If Not links Is Nothing AndAlso links.Length > 0 Then 'use the money to repay top-up payments (smallest first)
                Dim index As Int32 = 0
                Array.Sort(links, New AmountComparer)
                While index < links.Length AndAlso feeCharged >= links(index).Amount 'repay a top-up in full
                    feeCharged -= links(index).Amount
                    links(index).Delete()
                    index += 1
                End While
                If feeCharged > 0 AndAlso index < links.Length Then 'partially repay a top-up
                    links(index).Amount -= feeCharged
                    links(index).Save()
                    feeCharged = 0
                End If
            End If
            If feeCharged > 0 Then                 'repay part/all of this basket
                mTotalFeeCharged -= feeCharged
                If mTotalFeeCharged > 0 Then
                    Save()
                ElseIf PaymentHasOpenBasket() Then 'repaid completely: delete
                    Delete()
                Else                               'repaid completely: convert to open basket
                    mBasketClosed = False
                    mTotalFeeCharged = 0
                    Save()
                End If
            End If
            UpdatePayment(userId, False)
            InsertIntoLog(userId, "Remove Application from Basket, app Id: " & applicationId.ToString())
        End Sub

        Private Function PaymentHasOpenBasket() As Boolean
            If mRemittanceAdvicePaymentId > 0 Then
                Dim baskets As EntitySet.PaymentBasketSet = Entity.PaymentBasket.GetForPayment(mRemittanceAdvicePaymentId)
                For Each basket As Entity.PaymentBasket In baskets
                    If Not basket.BasketClosed Then
                        Return True
                    End If
                Next
            End If
            Return False
        End Function

        Public Function Close(ByVal userId As Int64) As BOPayment
            Return Close(userId, 0)
        End Function

        Private Function Close(ByVal userId As Int64, ByVal total As Decimal) As BOPayment
            For Each app As BOApplicationSummary In GetLinkedApplications(userId)
                total += app.Cost
                app.MarkAsPaid()
            Next
            mTotalFeeCharged = total
            mBasketClosed = True
            Save()
            InsertIntoLog(userId, "Close Basket, Id: " & mPaymentBasketId.ToString())
            Return UpdatePayment(userId, True)
        End Function

        Public Function CloseUsingTopupPayments(ByVal paymentIds() As Int32, ByVal amounts() As Decimal, ByVal userId As Int64) As BOPayment
            Dim linkDate As Date = DateTime.Now
            Dim count As Int32 = paymentIds.Length
            Dim total As Decimal = 0
            For i As Integer = 0 To count - 1
                Dim pbp As New BOPaymentBasketPayment
                Dim payment As New BOPayment(paymentIds(i))
                pbp.PaymentId = paymentIds(i)
                pbp.PaymentBasketId = mPaymentBasketId
                pbp.LinkDateTime = linkDate
                pbp.Amount = amounts(i)
                pbp.Save()
                payment.UpdateCounts(userId)
                total -= amounts(i)
            Next
            InsertIntoLog(userId, "Close Basket Using Topups, Id: " & mPaymentBasketId.ToString())
            Return Close(userId, total)
        End Function

        Private Function UpdatePayment(ByVal userId As Int64, ByVal addNewIfFunds As Boolean) As BOPayment
            If mRemittanceAdvicePaymentId > 0 Then
                Dim payment As New BOPayment(mRemittanceAdvicePaymentId)
                Dim availableBalance As Decimal = payment.UpdateCounts(userId)
                If availableBalance > 0 AndAlso addNewIfFunds Then
                    Dim basket As New BOPaymentBasket
                    basket.mPartyId = mPartyId
                    basket.mRemittanceAdvicePaymentId = mRemittanceAdvicePaymentId
                    basket.Save()
                End If
                Return payment
            End If
            Return Nothing
        End Function

#End Region

#Region " Save "
        Public Overloads Overrides Function Save() As BaseBO
            Dim basket As New DataObjects.Entity.PaymentBasket
            Dim service As DataObjects.Service.PaymentBasketService = basket.ServiceObject
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

            Dim basket As New DataObjects.Entity.PaymentBasket
            Dim service As DataObjects.Service.PaymentBasketService = basket.ServiceObject
            Created = (mPaymentBasketId = 0)

            If Created Then
                basket = service.Insert(mPartyId, _
                    mTotalFeeCharged, _
                    mBasketClosed, _
                    mRemittanceAdvicePrinted, _
                    mRemittanceAdviceDateTime, _
                    mRemittanceAdvicePaymentId, _
                    tran)
            Else
                basket = service.Update(mPaymentBasketId, _
                    mPartyId, _
                    mTotalFeeCharged, _
                    mBasketClosed, _
                    mRemittanceAdvicePrinted, _
                    mRemittanceAdviceDateTime, _
                    mRemittanceAdvicePaymentId, _
                    tran)
            End If
            'check to see if any SQL errors have occured
            If basket Is Nothing Then
                CheckSqlErrors("Payment Basket", tran, service)
            Else
                If Created And Not basket Is Nothing Then
                    mPaymentBasketId = basket.Id
                End If
                'no point in initialising unless things have changed
                If basket.CheckSum <> CheckSum Then InitialisePaymentBasket(basket, tran)

            End If

            Return Me
        End Function

#End Region

#Region " Validate "
        Public Overloads Function Validate(ByVal userID As Int64, ByVal writeFlag As Boolean) As ValidationManager
            ' init the errors list
            MyBase.ValidationErrors = New ValidationManager(ValidationManager.ValidationTitles.CannotSavePaymentBasket)

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

        Public Overrides Function Delete() As Object
            Return Entity.PaymentBasket.DeleteById(mPaymentBasketId)
        End Function

        Public Overridable Function DeletePaymentBasketById(ByVal basket As BOPaymentBasket, ByVal tran As SqlClient.SqlTransaction) As Boolean
            Dim service As New DataObjects.Service.PaymentBasketService
            Return service.DeleteById(basket.PaymentBasketId, 0, tran)
        End Function

        Private Shared Function LoadByPaymentBasketId(ByVal basketId As Int32) As BOPaymentBasket
            Return New BOPaymentBasket(basketId)
        End Function

        Protected Overridable Function GetPaymentBasket(ByVal basketId As Int32) As BOPaymentBasket
            Return LoadByPaymentBasketId(basketId)
        End Function

#End Region
    End Class
End Namespace




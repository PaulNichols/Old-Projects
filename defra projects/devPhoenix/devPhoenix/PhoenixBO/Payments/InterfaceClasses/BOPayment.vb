Imports uk.gov.defra.Phoenix.DO.DataObjects
Imports uk.gov.defra.Phoenix.BO.Party
Imports uk.gov.defra.Phoenix.BO.Application
Imports uk.gov.defra.Phoenix.BO.Application.CITES.Applications

Namespace Payments
    Public Class BOPayment
        Inherits PaymentsBaseBO
        Implements IBOPayment
#Region " Prelim code "
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal paymentId As Int32, ByVal tran As SqlClient.SqlTransaction)
            MyClass.New()
            LoadPayment(paymentId, tran)
        End Sub

        Public Sub New(ByVal paymentId As Int32)
            MyClass.New(paymentId, Nothing)
        End Sub

        Private Function LoadPayment(ByVal id As Int32) As Entity.Payment
            Return LoadPayment(id, Nothing)
        End Function

        Private Function LoadPayment(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As Entity.Payment
            Dim Payment As Entity.Payment = Entity.Payment.GetById(id)
            If Payment Is Nothing Then
                Throw New RecordDoesNotExist("Payment", id)
            Else
                InitialisePayment(Payment, tran)
                Return Payment
            End If
        End Function

        Friend Overridable Sub InitialisePayment(ByVal payment As Entity.Payment, ByVal tran As SqlClient.SqlTransaction)
            With payment
                CheckSum = .CheckSum
                mPaymentId = .Id
                mPartyId = .PartyId
                mPaymentMethodId = CType(.PaymentMethodId, PaymentMethod)
                mRefundTotal = GetRefundTotal(tran)
                If Not .IsPaymentReferenceNull Then mPaymentReference = .PaymentReference
                If Not .IsInitiatedByCustomerNull Then mInitiatedByCustomer = .InitiatedByCustomer
                If Not .IsPaymentDateTimeNull Then mPaymentDateTime = .PaymentDateTime
                If Not .IsPaymentAmountNull Then mPaymentAmount = .PaymentAmount
                If Not .IsAvailableBalanceNull Then mAvailableBalance = .AvailableBalance
                If Not .IsGatewayAuthorisationCodeNull Then mGatewayAuthorisationCode = .GatewayAuthorisationCode
                If Not .IsGatewayBankReferenceNull Then mGatewayBankReference = .GatewayBankReference
                If Not .IsEnteredApplicationsCoveredNull Then mEnteredApplicationsCovered = .EnteredApplicationsCovered
                If Not .IsActualApplicationsCoveredNull Then mActualApplicationsCovered = .ActualApplicationsCovered
                If Not .IsReceiptPrintedNull Then mReceiptPrinted = .ReceiptPrinted
                If Not .IsReportDateNull Then mReportDateTime = .ReportDate
                If mWithDetails Then
                    mCheques = BOCheque.GetChequesByPaymentId(mPaymentId, tran)
                    mCash = BOCash.GetCashByPaymentId(mPaymentId, tran)
                    mPostalOrders = BOPostalOrder.GetPostalOrderByPaymentId(mPaymentId, tran)
                End If
            End With
        End Sub

        Private Function GetRefundTotal(ByVal tran As SqlClient.SqlTransaction) As Decimal
            Dim refunds As BORefund() = BORefund.GetRefundsByPaymentId(mPaymentId, tran)
            Dim result As Decimal = 0
            For Each refund As BORefund In refunds
                result += refund.Amount
            Next
            Return result
        End Function
#End Region

#Region " Properties "

        <Serializable()> Public Enum PaymentMethod
            DoNotUse            'placeholder so that Cheque=1, etc on both sides of Web Service divide
            Cheque
            Cash
            Card
            PostalOrder
            FeeWaiver
            AwaitingPayment     
        End Enum

        Private mPaymentId As Integer
        Private mPartyId As Integer
        Private mPaymentReference As String
        Private mPaymentMethodId As PaymentMethod
        Private mInitiatedByCustomer As Boolean
        Private mPaymentDateTime As Date
        Private mPaymentAmount As Decimal
        Private mAvailableBalance As Decimal
        Private mRefundTotal As Decimal
        Private mGatewayAuthorisationCode As String
        Private mGatewayBankReference As String
        Private mEnteredApplicationsCovered As Integer
        Private mActualApplicationsCovered As Integer
        Private mReceiptPrinted As Boolean
        Private mReportDateTime As Date
        Private mCheques() As BOCheque = New BOCheque() {}
        Private mCash() As BOCash = New BOCash() {}
        Private mPostalOrders() As BOPostalOrder = New BOPostalOrder() {}
        Private mWithDetails As Boolean = True

        Public Property PaymentId() As Integer Implements IBOPayment.PaymentId
            Get
                Return mPaymentId
            End Get
            Set(ByVal Value As Integer)
                mPaymentId = Value
            End Set
        End Property

        Public Property PaymentMethodName() As String Implements IBOPayment.PaymentMethodName
            Get
                Select Case mPaymentMethodId
                    Case PaymentMethod.Cheque
                        Return "Cheque"
                    Case PaymentMethod.Cash
                        Return "Cash"
                    Case PaymentMethod.Card
                        Return "Credit/Debit Card"
                    Case PaymentMethod.PostalOrder
                        Return "Postal Order"
                    Case PaymentMethod.FeeWaiver
                        Return "Fee Waiver"
                    Case PaymentMethod.AwaitingPayment  
                        Return "Awaiting Payment"
                End Select   
                Return "Unknown"
            End Get
            Set(ByVal Value As String)
            End Set
        End Property


        Public Property PaymentMethodId() As PaymentMethod Implements IBOPayment.PaymentMethodId
            Get
                Return mPaymentMethodId
            End Get
            Set(ByVal Value As PaymentMethod)
                mPaymentMethodId = Value
            End Set
        End Property

        Public Property PartyId() As Integer Implements IBOPayment.PartyId
            Get
                Return mPartyId
            End Get
            Set(ByVal Value As Integer)
                mPartyId = Value
            End Set
        End Property

        Public Property PaymentReference() As String Implements IBOPayment.PaymentReference
            Get
                Return mPaymentReference
            End Get
            Set(ByVal Value As String)
                mPaymentReference = Value
            End Set
        End Property

        Public Property InitiatedByCustomer() As Boolean Implements IBOPayment.InitiatedByCustomer
            Get
                Return mInitiatedByCustomer
            End Get
            Set(ByVal Value As Boolean)
                mInitiatedByCustomer = Value
            End Set
        End Property

        Public Property PaymentDateTime() As Date Implements IBOPayment.PaymentDateTime
            Get
                Return mPaymentDateTime
            End Get
            Set(ByVal Value As Date)
                mPaymentDateTime = Value
            End Set
        End Property


        Public Property PaymentDate() As String Implements IBOPayment.PaymentDate
            Get
                Return mPaymentDateTime.ToShortDateString()
            End Get
            Set(ByVal Value As String)
            End Set
        End Property
        Public Property PaymentAmount() As Decimal Implements IBOPayment.PaymentAmount
            Get
                Return mPaymentAmount
            End Get
            Set(ByVal Value As Decimal)
                mPaymentAmount = Value
            End Set
        End Property

        Public Property PaymentAmountDisplay() As String Implements IBOPayment.PaymentAmountDisplay
            Get
                Return mPaymentAmount.ToString("C")
            End Get
            Set(ByVal Value As String)
            End Set
        End Property

        Public Property AvailableBalance() As Decimal Implements IBOPayment.AvailableBalance
            Get
                Return mAvailableBalance
            End Get
            Set(ByVal Value As Decimal)
                mAvailableBalance = Value
            End Set
        End Property

        Public Property AvailableBalanceDisplay() As String Implements IBOPayment.AvailableBalanceDisplay
            Get
                If IsComplete Then
                    Return "Completed"
                End If
                Return mAvailableBalance.ToString("C")
            End Get
            Set(ByVal Value As String)
            End Set
        End Property

        Public Property IsComplete() As Boolean
             Get
                Return mAvailableBalance = 0 AndAlso Me.mPaymentMethodId <> PaymentMethod.AwaitingPayment
            End Get
            Set(ByVal Value As Boolean)
            End Set
        End Property

        Public Property RefundTotal() As Decimal Implements IBOPayment.RefundTotal
            Get
                Return mRefundTotal
            End Get
            Set(ByVal Value As Decimal)
                mRefundTotal = Value
            End Set
        End Property

        Public Property RefundTotalDisplay() As String Implements IBOPayment.RefundTotalDisplay
            Get
                If mRefundTotal = 0 Then
                    Return "None"
                End If
                Return mRefundTotal.ToString("C")
            End Get
            Set(ByVal Value As String)
            End Set
        End Property

        Public Property GatewayAuthorisationCode() As String Implements IBOPayment.GatewayAuthorisationCode
            Get
                Return mGatewayAuthorisationCode
            End Get
            Set(ByVal Value As String)
                mGatewayAuthorisationCode = Value
            End Set
        End Property

        Public Property GatewayBankReference() As String Implements IBOPayment.GatewayBankReference
            Get
                Return mGatewayBankReference
            End Get
            Set(ByVal Value As String)
                mGatewayBankReference = Value
            End Set
        End Property

        Public Property EnteredApplicationsCovered() As Integer Implements IBOPayment.EnteredApplicationsCovered
            Get
                Return mEnteredApplicationsCovered
            End Get
            Set(ByVal Value As Integer)
                mEnteredApplicationsCovered = Value
            End Set
        End Property

        Public Property ActualApplicationsCovered() As Integer Implements IBOPayment.ActualApplicationsCovered
            Get
                Return mActualApplicationsCovered
            End Get
            Set(ByVal Value As Integer)
                mActualApplicationsCovered = Value
            End Set
        End Property

        Public Property ReceiptPrinted() As Boolean Implements IBOPayment.ReceiptPrinted
            Get
                Return mReceiptPrinted
            End Get
            Set(ByVal Value As Boolean)
                mReceiptPrinted = Value
            End Set
        End Property

        Public Property ReportDate() As String Implements IBOPayment.ReportDate
            Get
                Return mReportDateTime.ToShortDateString()
            End Get
            Set(ByVal Value As String)
            End Set
        End Property

        Public Property ReportDateTime() As Date Implements IBOPayment.ReportDateTime
            Get
                Return mReportDateTime
            End Get
            Set(ByVal Value As Date)
                mReportDateTime = Value
            End Set
        End Property

        Public Property Cheques() As BOCheque() Implements IBOPayment.Cheques
            Get
                Return mCheques
            End Get
            Set(ByVal Value As BOCheque())
                mCheques = Value
            End Set
        End Property

        Public Property Cash() As BOCash() Implements IBOPayment.Cash
            Get
                Return mCash
            End Get
            Set(ByVal Value As BOCash())
                mCash = Value
            End Set
        End Property

        Public Property PostalOrders() As BOPostalOrder() Implements IBOPayment.PostalOrders
            Get
                Return mPostalOrders
            End Get
            Set(ByVal Value As BOPostalOrder())
                mPostalOrders = Value
            End Set
        End Property

        Public Property NewAvailableBalanceDisplay() As String
            Get
                Return "-"
            End Get
            Set(ByVal Value As String)
            End Set
        End Property

        Public Property SelectDisplay() As String
            Get
                Return ""
            End Get
            Set(ByVal Value As String)

            End Set
        End Property
#End Region

#Region " Helper Functions "
        Public Function GetPayment() As BOPayment
            If mPaymentId = 0 Then
                Throw New ArgumentException("Payment Id is 0")
            End If
            Return New BOPayment(mPaymentId)
        End Function

        Public Function GetTopupPaymentsTotal() As Decimal
            If mPaymentId = 0 Then
                Throw New ArgumentException("Payment Id is 0")
            End If
            Dim payment As Entity.Payment = Entity.Payment.GetById(mPaymentId)
            Dim links As EntitySet.PaymentBasketPaymentSet = payment.GetRelatedPaymentBasketPayment()
            Dim result As Decimal = 0
            If Not links Is Nothing Then
                For Each link As Entity.PaymentBasketPayment In links
                    If Not link.IsAmountNull Then
                        result += link.Amount
                    End If
                Next link
            End If
            Return result
        End Function

        Public Function GetPaymentBaskets() As BOPaymentBasket()
            If mPaymentId = 0 Then
                Throw New ArgumentException("Payment Id is 0")
            End If
            Dim payment As Entity.Payment = Entity.Payment.GetById(mPaymentId)
            Dim links As EntitySet.PaymentBasketSet = payment.GetRelatedPaymentBasket()
            If links Is Nothing Then
                Dim empty(-1) As BOPaymentBasket
                Return empty
            End If
            Dim baskets(links.Count - 1) As BOPaymentBasket
            Dim index As Int32 = 0
            For Each link As Entity.PaymentBasket In links
                baskets(index) = New BOPaymentBasket(link.PaymentBasketId)
                index += 1
            Next link
            Return baskets
        End Function

        Public Shared Function GetCountsByPaymentId(ByVal paymentId As Int32) As BOCounts
            Dim result As New BOCounts
            Dim count1 As Int32 = 0
            Dim count2 As Int32 = 0
            Dim payment As Entity.Payment = Entity.Payment.GetById(paymentId)
            Dim links As EntitySet.PaymentBasketSet = payment.GetRelatedPaymentBasket()
            If Not links Is Nothing Then
                For Each link As Entity.PaymentBasket In links
                    Dim app As New Entity.Application
                    Dim appCount As Int32 = app.GetForPaymentBasket(link.PaymentBasketId).Count
                    count1 += appCount
                    If Not link.BasketClosed Then
                        count2 += appCount
                    End If
                Next link
            End If
            result.ApplicationCount = count1
            result.OpenBasketApplicationCount = count2
            Return result
        End Function

        Public Shared Function GetPaymentByPaymentId(ByVal paymentId As Int32, ByVal searchingUserId As Int64) As BOPayment
            Try
                InsertIntoLog(searchingUserId, "Payment Id: " & paymentId)
                Return New BOPayment(paymentId)
            Catch noRecord As RecordDoesNotExist
                Return Nothing
            Catch ex As Exception
                Throw
            End Try
        End Function

        Public Shared Function GetPaymentByPaymentReference(ByVal paymentReference As String, ByVal searchingUserId As Int64) As BOPayment
            Try
                Dim payment As New Entity.Payment
                Dim service As service.PaymentService = payment.ServiceObject
                Dim results As EntitySet.PaymentSet = service.GetByIndex_IX_Payment_1(paymentReference)

                InsertIntoLog(searchingUserId, "Payment Reference: " & paymentReference)
                If Not results Is Nothing AndAlso results.Count > 0 Then
                    Dim result As New BOPayment
                    payment = CType(results.GetEntity(0), Entity.Payment)
                    result.InitialisePayment(payment, Nothing)
                    Return result
                End If
                Return Nothing
            Catch noRecord As RecordDoesNotExist
                Return Nothing
            Catch ex As Exception
                Throw
            End Try
        End Function

        Public Shared Function GetPaymentsByPartyId(ByVal partyId As Int32, ByVal withDetails As Boolean, ByVal searchingUserId As Int64) As BOPayment()
            Try
                Dim payment As New Entity.Payment
                Dim entityset As entityset.PaymentSet = payment.GetForParty(partyId)

                InsertIntoLog(searchingUserId, "Party Id: " & partyId)
                If Not entityset Is Nothing Then
                    Dim results(entityset.Count - 1) As BOPayment
                    Dim index As Int32 = 0
                    For Each item As Entity.Payment In entityset
                        Dim result As New BOPayment
                        result.mWithDetails = withDetails
                        result.InitialisePayment(item, Nothing)
                        results(index) = result
                        index += 1
                    Next
                    Return results
                End If
                Return Nothing
            Catch noRecord As RecordDoesNotExist
                Return Nothing
            Catch ex As Exception
                Throw
            End Try
        End Function

#End Region

#Region " Save "
        Public Overloads Overrides Function Save() As BaseBO
            Dim payment As New Entity.Payment
            Dim service As service.PaymentService = payment.ServiceObject
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

            Dim payment As New Entity.Payment
            Dim service As service.PaymentService = payment.ServiceObject
            Created = (mPaymentId = 0)
            If Created Then
                Dim basket As BOPaymentBasket = New BOPaymentBasket
                payment = service.Insert(mPartyId, _
                    GenerateUniquePaymentReference(), _
                    mPaymentMethodId, _
                    mInitiatedByCustomer, _
                    mPaymentDateTime, _
                    mPaymentAmount, _
                    mAvailableBalance, _
                    mGatewayAuthorisationCode, _
                    mGatewayBankReference, _
                    mEnteredApplicationsCovered, _
                    mActualApplicationsCovered, _
                    mReceiptPrinted, _
                    mReportDateTime, _
                    tran)
                basket.PartyId = mPartyId
                basket.RemittanceAdvicePaymentId = payment.PaymentId
                basket.Save(tran)
            Else
                UpdateBaskets(tran)
                payment = service.Update(mPaymentId, _
                    mPartyId, _
                    mPaymentReference, _
                    mPaymentMethodId, _
                    mInitiatedByCustomer, _
                    mPaymentDateTime, _
                    mPaymentAmount, _
                    mAvailableBalance, _
                    mGatewayAuthorisationCode, _
                    mGatewayBankReference, _
                    mEnteredApplicationsCovered, _
                    mActualApplicationsCovered, _
                    mReceiptPrinted, _
                    mReportDateTime, _
                    tran)
            End If
            If Not payment Is Nothing AndAlso Sprocs.LastError Is Nothing Then
                BOCheque.UpdateChequesByPaymentId(payment.PaymentId, mCheques, tran)
                BOCash.UpdateCashByPaymentId(payment.PaymentId, mCash, tran)
                BOPostalOrder.UpdatePostalOrderByPaymentId(payment.PaymentId, mPostalOrders, tran)
            End If
            'check to see if any SQL errors have occured
            If payment Is Nothing Then
                CheckSqlErrors("Payment", tran, service)
            Else
                If Created And Not payment Is Nothing Then
                    mPaymentId = payment.Id
                End If
                If payment.CheckSum <> CheckSum Then
                     InitialisePayment(payment, tran)
                End If
            End If
            Return Me
        End Function

        Private Sub UpdateBaskets(ByVal tran As SqlClient.SqlTransaction)
            Dim baskets As EntitySet.PaymentBasketSet = Entity.PaymentBasket.GetForPayment(mPaymentId, tran)
            Dim links As EntitySet.PaymentBasketPaymentSet = Entity.PaymentBasketPayment.GetForPayment(mPaymentId, tran)
            For Each item As Entity.PaymentBasket In baskets
                If item.PartyId <> mPartyId Then
                    Dim basket As New BOPaymentBasket(item.PaymentBasketId, tran)
                    basket.PartyId = mPartyId
                    basket.Save(tran)
                End If
            Next
            For Each item As Entity.PaymentBasketPayment In links
                Dim basket As New BOPaymentBasket(item.PaymentBasketId, tran)
                If basket.PartyId <> mPartyId Then
                    basket.PartyId = mPartyId
                    basket.Save(tran)
                End If
            Next
        End Sub

        Private Function GenerateUniquePaymentReference() As String
            Dim ok As Boolean = False
            Dim result As String
            Dim payment As New Entity.Payment
            Dim service As service.PaymentService = payment.ServiceObject
            Dim entitySet As entitySet.PaymentSet
            While Not ok
                result = GeneratePaymentReference()
                entitySet = service.GetByIndex_IX_Payment_1(result)
                ok = entitySet Is Nothing
            End While
            Return result
        End Function

        Private Function GeneratePaymentReference() As String
            Dim rand As New Random(Convert.ToInt32(DateTime.Now.Ticks Mod Int32.MaxValue))
            Return RandomPair(RandomDigit(rand), RandomDigit(rand), rand) + _
                   RandomPair(RandomLetter(rand), RandomLetter(rand), rand) + _
                   RandomPair(RandomDigit(rand), RandomDigit(rand), rand) + _
                   RandomPair(RandomLetter(rand), RandomLetter(rand), rand) + _
                   RandomPair(RandomDigit(rand), RandomDigit(rand), rand)
        End Function

        Private Function RandomPair(ByVal s1 As String, ByVal s2 As String, ByRef rand As Random) As String
            If RandomBool(rand) Then
                Return s1 + s2
            End If
            Return s1 + s1
        End Function

        Private Function RandomBool(ByRef rand As Random) As Boolean
            Return CInt(rand.Next(2)) = 0
        End Function

        Private Function RandomDigit(ByRef rand As Random) As String
            Return CInt(rand.Next(10)).ToString
        End Function

        Private Function RandomLetter(ByRef rand As Random) As String
            Return "ABCDEFGHJKLMNPQRSYUVWXYZ".Substring(CInt(rand.Next(24)), 1)  ' I and O missing
        End Function

#End Region

#Region " Validate "
        Public Overloads Function Validate(ByVal userID As Int64, ByVal writeFlag As Boolean) As ValidationManager
            ' init the errors list
            MyBase.ValidationErrors = New ValidationManager(ValidationManager.ValidationTitles.CannotSavePayment)


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

        Public Shared Function GetRemittanceAdviceData(ByVal paymentId As Int32, ByVal schema As String) As ReportDataResults
            Dim returnDS As DataSet = GetSchemaDataSet(schema)
            Dim newRow As DataRow = returnDS.Tables("BORemittanceHeader").NewRow()
            Dim payment As New BOPayment(paymentId)
            Dim reference As String = payment.PaymentReference
            Dim basket As BOPaymentBasket = BOPaymentBasket.GetBasketsByPaymentId(paymentId, Nothing)(0)
            Dim apps() As BOApplicationSummary = basket.GetLinkedApplications(Nothing)
            Dim party As BOParty = BOParty.PolymorphicCreate(payment.PartyId)
            Dim index As Int32 = 1
            Dim total As Decimal = 0

            With newRow
                ' Create new Payment Receipt Details
                .Item("PaymentReference") = reference
                .Item("PrintDate") = DateTime.Now.ToString("dd/MM/yyyy")
                .Item("IssueAuthority") = GetIssuingAuthority(apps)
                .Item("ApplicantId") = party.PartyId.ToString()
                .Item("ApplicantName") = party.DisplayName
                For Each app As BOApplicationSummary In apps
                    Dim suffix As Char = Microsoft.VisualBasic.Chr(48 + index)
                    If index <= 6 Then
                        .Item("ApplicationRef" + suffix) = app.ApplicationId
                        .Item("ApplicationType" + suffix) = app.ApplicationType
                        .Item("StandardFee" + suffix) = app.TrueCostDisplay.Substring(1)
                        .Item("FeeCharged" + suffix) = app.CostDisplay.Substring(1)
                        total += app.Cost
                    End if
                    index += 1
                Next
                .Item("AmountPayable") = total.ToString("c").Substring(1)
            End With

            'add the row to the dataset - Sheet 1
            returnDS.Tables("BORemittanceHeader").Rows.Add(newRow)
            returnDS.AcceptChanges()

            Return New ReportDataResults(returnDS, reference)
        End Function

        Private Shared Function GetIssuingAuthority(ByVal apps() As BOApplicationSummary) As String
            If apps.Length > 0 Then
                Dim app As BOApplication = BOApplication.PolymorphicCreate(apps(0).ApplicationId)
                Return CType(app, BOCITESApplication).IssuingAuthorityAddress
            End If
            Return ""
        End Function

        Public Shared Function GetPaymentReceiptReportData(ByVal paymentId As Int32, ByVal schema As String) As ReportDataResults
            Dim returnDS As DataSet = GetSchemaDataSet(schema)
            Dim newRow As DataRow = returnDS.Tables("BOPaymentReceipt").NewRow()
            Dim payment As New BOPayment(paymentId)
            Dim party As BOParty = BOParty.PolymorphicCreate(payment.PartyId)
            Dim address As Party.BOAddress = party.GetMailingAddress(Nothing)

            With newRow
                ' Create new Payment Receipt Details
                .Item("ApplicantNameAddress") = party.DisplayName & Environment.NewLine & _
                address.ReportAddress
                '.Item("ApplicantNameAddress") = party.DisplayName & Environment.NewLine & _
                'address.Address1 & Environment.NewLine & _
                'address.Address2 & Environment.NewLine & _
                'address.Address3 & Environment.NewLine & _
                'address.Address4 & Environment.NewLine & _
                'address.Postcode

                .Item("ReceivedFrom") = party.DisplayName
                .Item("IdNumber") = payment.PartyId.ToString()
                .Item("ReceivedDay") = payment.PaymentDateTime.Day.ToString()
                .Item("ReceivedMonth") = payment.PaymentDateTime.Month.ToString()
                .Item("ReceivedYear") = payment.PaymentDateTime.Year.ToString()
                .Item("PaymentReference") = payment.PaymentReference
                .Item("PaymentMethod") = payment.PaymentMethodName
                .Item("AmountReceived") = payment.PaymentAmountDisplay.Replace("£", "")
            End With

            'add the row to the dataset - Sheet 1
            returnDS.Tables("BOPaymentReceipt").Rows.Add(newRow)
            returnDS.AcceptChanges()

            Return New ReportDataResults(returnDS, payment.PaymentReference)
        End Function


        Public Function UpdateCounts(ByVal userId As Int64) As Decimal
            Dim appCount As Int32 = 0
            Dim appValue As Decimal = 0

            For Each basket As BOPaymentBasket In GetPaymentBaskets()
                If basket.BasketClosed Then
                    appValue += basket.TotalFeeCharged
                End If
                appCount += basket.GetLinkedApplications(userId).Length
            Next
            mAvailableBalance = mPaymentAmount - mRefundTotal - appValue - GetTopupPaymentsTotal()
            mActualApplicationsCovered = appCount
            Save()
            Return mAvailableBalance
        End Function

        Public Overridable Function DeletePaymentById(ByVal payment As BOPayment, ByVal tran As SqlClient.SqlTransaction) As Boolean
            Dim service As New service.PaymentService
            Return service.DeleteById(payment.PaymentId, 0, tran)
        End Function


        Private Shared Function LoadByPaymentId(ByVal paymentId As Int32) As BOPayment
            Return New BOPayment(paymentId)
        End Function

        Protected Overridable Function GetPayment(ByVal paymentId As Int32) As BOPayment
            Return LoadByPaymentId(paymentId)
        End Function

#End Region
    End Class
End Namespace

Imports uk.gov.defra.Phoenix.DO.DataObjects

Namespace Payments
    Public Class BORefund
        Inherits PaymentsBaseBO
        Implements IBORefund

        <Serializable()> _
        Public Enum RefundCode
            Unknown
            Online
            Manual
            Amendment
        End Enum

#Region " Prelim code "
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal refundId As Int32, ByVal tran As SqlClient.SqlTransaction)
            MyClass.New()
            LoadRefund(RefundId, tran)
        End Sub

        Public Sub New(ByVal refundId As Int32)
            MyClass.New(refundId, Nothing)
        End Sub

        Private Function LoadRefund(ByVal id As Int32) As Entity.Refund
            Return LoadRefund(id, Nothing)
        End Function

        Private Function LoadRefund(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As Entity.Refund
            Dim Refund As Entity.Refund = Entity.Refund.GetById(id)
            If Refund Is Nothing Then
                Throw New RecordDoesNotExist("Refund", id)
            Else
                InitialiseRefund(Refund, tran)
                Return Refund
            End If
        End Function

        Friend Overridable Sub InitialiseRefund(ByVal refund As Entity.Refund, ByVal tran As SqlClient.SqlTransaction)
            With refund
                CheckSum = .CheckSum
                mRefundId = .Id
                mPaymentId = .PaymentId
                mOriginalPartyId = .OriginalPartyId
                mAmount = 0
                mReportDate = .ReportDate
                mAmount = .Amount
                mBankReference = .BankReference
                mRefundDateTime = .DateTime
                mGatewayAuthorisation = .GatewayAuthorisation
                mGatewayReference = .GatewayReference
                mNotesComments = .NotesComments
                mOriginalPaymentAmount = .OriginalPaymentAmount
                mSSOUserId = .SSOUserId
            End With
        End Sub
#End Region

#Region " Properties "

        Private mRefundId As Integer
        Private mPaymentId As Integer
        Private mSSOUserId As Int64
        Private mRefundType As RefundCode
        Private mRefundDateTime As DateTime
        Private mAmount As Decimal
        Private mOriginalPaymentAmount As Decimal
        Private mOriginalPartyId As Integer
        Private mNotesComments As String
        Private mReportDate As DateTime
        Private mGatewayAuthorisation As String
        Private mGatewayReference As String
        Private mBankReference As String

        Property RefundId() As Integer Implements IBORefund.RefundId
            Get
                Return mRefundId
            End Get
            Set(ByVal Value As Integer)
                mRefundId = Value
            End Set
        End Property
        Property PaymentId() As Integer Implements IBORefund.PaymentId
            Get
                Return mPaymentId
            End Get
            Set(ByVal Value As Integer)
                mPaymentId = Value
            End Set
        End Property
        Property SSOUserId() As Int64 Implements IBORefund.SSOUserId
            Get
                Return mSSOUserId
            End Get
            Set(ByVal Value As Int64)
                mSSOUserId = Value
            End Set
        End Property
        Property RefundType() As RefundCode Implements IBORefund.RefundType
            Get
                Return mRefundType
            End Get
            Set(ByVal Value As RefundCode)
                mRefundType = Value
            End Set
        End Property
        Property RefundDateTime() As DateTime Implements IBORefund.RefundDateTime
            Get
                Return mRefundDateTime
            End Get
            Set(ByVal Value As DateTime)
                mRefundDateTime = Value
            End Set
        End Property
        Property Amount() As Decimal Implements IBORefund.Amount
            Get
                Return mAmount
            End Get
            Set(ByVal Value As Decimal)
                mAmount = Value
            End Set
        End Property
        Property OriginalPaymentAmount() As Decimal Implements IBORefund.OriginalPaymentAmount
            Get
                Return mOriginalPaymentAmount
            End Get
            Set(ByVal Value As Decimal)
                mOriginalPaymentAmount = Value
            End Set
        End Property
        Property OriginalPartyId() As Integer Implements IBORefund.OriginalPartyId
            Get
                Return mOriginalPartyId
            End Get
            Set(ByVal Value As Integer)
                mOriginalPartyId = Value
            End Set
        End Property
        Property NotesComments() As String Implements IBORefund.NotesComments
            Get
                Return mNotesComments
            End Get
            Set(ByVal Value As String)
                mNotesComments = Value
            End Set
        End Property
        Property ReportDate() As DateTime Implements IBORefund.ReportDate
            Get
                Return mReportDate
            End Get
            Set(ByVal Value As DateTime)
                mReportDate = Value
            End Set
        End Property
        Property GatewayAuthorisation() As String Implements IBORefund.GatewayAuthorisation
            Get
                Return mGatewayAuthorisation
            End Get
            Set(ByVal Value As String)
                mGatewayAuthorisation = Value
            End Set
        End Property
        Property GatewayReference() As String Implements IBORefund.GatewayReference
            Get
                Return mGatewayReference
            End Get
            Set(ByVal Value As String)
                mGatewayReference = Value
            End Set
        End Property
        Property BankReference() As String Implements IBORefund.BankReference
            Get
                Return mBankReference
            End Get
            Set(ByVal Value As String)
                mBankReference = Value
            End Set
        End Property
#End Region

#Region " Helper Functions "
        Public Function GetRefund() As BORefund
            If mRefundId = 0 Then
                Throw New ArgumentException("Refund Id is 0")
            End If
            Return New BORefund(mRefundId)
        End Function

        Public Shared Function GetRefundsByPaymentId(ByVal paymentId As Int32, ByVal tran As SqlClient.SqlTransaction) As BORefund()
            Try
                Dim refund As New Entity.Refund
                Dim entityset As entityset.RefundSet = refund.GetForPayment(paymentId, tran)

                If Not entityset Is Nothing Then
                    Dim results(entityset.Count - 1) As BORefund
                    Dim index As Int32 = 0
                    For Each item As Entity.Refund In entityset
                        Dim result As New BORefund
                        result.InitialiseRefund(item, Nothing)
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
            Dim refund As New Entity.Refund
            Dim service As service.RefundService = refund.ServiceObject
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

            Dim refund As New Entity.Refund
            Dim service As service.RefundService = refund.ServiceObject
            Dim payment As New BOPayment(PaymentId, tran)
            Dim reduction As Decimal = 0
            Created = (mRefundId = 0)
            If Created Then
                refund = service.Insert(mPaymentId, _
                    mSSOUserId, _
                    mRefundType, _
                    mRefundDateTime, _
                    mAmount, _
                    mOriginalPaymentAmount, _
                    mOriginalPartyId, _
                    mNotesComments, _
                    mReportDate, _
                    mGatewayAuthorisation, _
                    mGatewayReference, _
                    mBankReference, _
                    tran)
                reduction = mAmount
            Else
                refund = service.Update(mRefundId, _
                    mPaymentId, _
                    mSSOUserId, _
                    mRefundType, _
                    mRefundDateTime, _
                    mAmount, _
                    mOriginalPaymentAmount, _
                    mOriginalPartyId, _
                    mNotesComments, _
                    mReportDate, _
                    mGatewayAuthorisation, _
                    mGatewayReference, _
                    mBankReference, _
                    tran)
                ' To do: fix up reduction
            End If
            payment.AvailableBalance -= reduction
            payment.Save(tran)
            'check to see if any SQL errors have occured
            If refund Is Nothing Then
                CheckSqlErrors("Refund", tran, service)
            Else
                If Created And Not refund Is Nothing Then
                    mRefundId = refund.Id
                End If
                CheckSum = refund.CheckSum
            End If
            Return Me
        End Function
#End Region

#Region " Validate "
        Public Overloads Function Validate(ByVal userID As Int64, ByVal writeFlag As Boolean) As ValidationManager
            ' init the errors list
            MyBase.ValidationErrors = New ValidationManager(ValidationManager.ValidationTitles.CannotSaveRefund)

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

        Public Overridable Function DeleteRefundById(ByVal refund As BORefund, ByVal tran As SqlClient.SqlTransaction) As Boolean
            Dim service As New service.RefundService
            Return service.DeleteById(refund.RefundId, 0, tran)
        End Function


        Private Shared Function LoadByRefundId(ByVal refundId As Int32) As BORefund
            Return New BORefund(refundId)
        End Function

        Protected Overridable Function GetRefund(ByVal refundId As Int32) As BORefund
            Return LoadByRefundId(refundId)
        End Function

#End Region
    End Class
End Namespace


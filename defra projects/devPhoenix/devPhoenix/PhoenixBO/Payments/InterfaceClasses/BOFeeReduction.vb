Imports uk.gov.defra.Phoenix.DO.DataObjects

Namespace Payments
    Public Class BOFeeReduction
        Inherits PaymentsBaseBO
        Implements IBOFeeReduction
#Region " Prelim code "
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal feeReductionId As Int32, ByVal tran As SqlClient.SqlTransaction)
            MyClass.New()
            LoadFeeReduction(FeeReductionId, tran)
        End Sub

        Public Sub New(ByVal feeReductionId As Int32)
            MyClass.New(FeeReductionId, Nothing)
        End Sub

        Private Function LoadFeeReduction(ByVal id As Int32) As Entity.FeeReduction
            Return LoadFeeReduction(id, Nothing)
        End Function

        Private Function LoadFeeReduction(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As Entity.FeeReduction
            Dim feeReduction As Entity.FeeReduction = Entity.FeeReduction.GetById(id)
            If feeReduction Is Nothing Then
                Throw New RecordDoesNotExist("FeeReduction", id)
            Else
                InitialiseFeeReduction(feeReduction, tran)
                Return feeReduction
            End If
        End Function

        Friend Overridable Sub InitialiseFeeReduction(ByVal feeReduction As Entity.FeeReduction, ByVal tran As SqlClient.SqlTransaction)
            Try
                With feeReduction
                    CheckSum = .CheckSum
                    mFeeReductionId = .Id

                    If Not .IsReasonNull Then mReason = .Reason
                    If Not .IsSSOUserNull Then mSSOUserId = .SSOUser
                    If Not .IsDateTimeNull Then mDateTime = .DateTime
                    If Not .IsOriginalFeeNull Then mOriginalFee = .OriginalFee
                    If Not .IsAmountNull Then mAmount = .Amount
                End With
            Catch ex As Exception
            End Try
        End Sub
#End Region

#Region " Properties "

        Private mFeeReductionId As Integer
        Private mApplicationId As Integer
        Private mPaymentId As Integer
        Private mReason As String
        Private mSSOUserId As Int64
        Private mDateTime As DateTime
        Private mAmount As Decimal
        Private mOriginalFee As Decimal

        Public Property FeeReductionId() As Integer Implements IBOFeeReduction.FeeReductionId
            Get
                Return mFeeReductionId
            End Get
            Set(ByVal Value As Integer)
                mFeeReductionId = Value
            End Set
        End Property

        Public Property ApplicationId() As Integer Implements IBOFeeReduction.ApplicationId
            Get
                Return mApplicationId
            End Get
            Set(ByVal Value As Integer)
                mApplicationId = Value
            End Set
        End Property


        Public Property Reason() As String Implements IBOFeeReduction.Reason
            Get
                Return mReason
            End Get
            Set(ByVal Value As String)
                mReason = Value
            End Set
        End Property

        Public Property PaymentId() As Integer Implements IBOFeeReduction.PaymentId
            Get
                Return mPaymentId
            End Get
            Set(ByVal Value As Integer)
                mPaymentId = Value
            End Set
        End Property

        Public Property SSOUserId() As Int64 Implements IBOFeeReduction.SSOUserId
            Get
                Return mSSOUserId
            End Get
            Set(ByVal Value As Int64)
                mSSOUserId = Value
            End Set
        End Property

        Public Property DateTime() As DateTime Implements IBOFeeReduction.DateTime
            Get
                Return mDateTime
            End Get
            Set(ByVal Value As DateTime)
                mDateTime = Value
            End Set
        End Property

        Public Property OriginalFee() As Decimal Implements IBOFeeReduction.OriginalFee
            Get
                Return mOriginalFee
            End Get
            Set(ByVal Value As Decimal)
                mOriginalFee = Value
            End Set
        End Property

        Public Property Amount() As Decimal Implements IBOFeeReduction.Amount
            Get
                Return mAmount
            End Get
            Set(ByVal Value As Decimal)
                mAmount = Value
            End Set
        End Property
#End Region

#Region " Helper Functions "
        Public Function GetFeeReduction() As BOFeeReduction
            If mFeeReductionId = 0 Then
                Throw New ArgumentException("FeeReduction Id is 0")
            Else
                Return New BOFeeReduction(mFeeReductionId)
            End If
        End Function

#End Region

#Region " Save "
        Public Overloads Overrides Function Save() As BaseBO
            Dim feeReduction As New Entity.FeeReduction
            Dim service As service.FeeReductionService = feeReduction.ServiceObject
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

            Dim feeReduction As New Entity.FeeReduction
            Dim service As service.FeeReductionService = feeReduction.ServiceObject
            Created = (mFeeReductionId = 0)

            If Created Then
                feeReduction = service.Insert(mApplicationId, _
                    mPaymentId, _
                    mReason, _
                     mSSOUserId, _
                    mDateTime, _
                    mAmount, _
                    mOriginalFee, _
                    tran)
            Else
                feeReduction = service.Update(mFeeReductionId, _
                    mApplicationId, _
                    mPaymentId, _
                    mReason, _
                    mSSOUserId, _
                    mDateTime, _
                    mAmount, _
                    mOriginalFee, _
                    tran)
            End If
            'check to see if any SQL errors have occured
            If feeReduction Is Nothing Then
                CheckSqlErrors("Fee Reduction", tran, service)
            Else
                If Created And Not feeReduction Is Nothing Then
                    mFeeReductionId = feeReduction.Id
                End If
                'no point in initialising unless things have changed
                If feeReduction.CheckSum <> CheckSum Then InitialiseFeeReduction(feeReduction, tran)

            End If

            Return Me
        End Function

#End Region

#Region " Validate "
        Public Overloads Function Validate(ByVal userID As Int64, ByVal writeFlag As Boolean) As ValidationManager
            ' init the errors list
            MyBase.ValidationErrors = New ValidationManager(ValidationManager.ValidationTitles.CannotSaveFeeReduction)

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

 #End Region
    End Class
End Namespace

Imports uk.gov.defra.Phoenix.DO.DataObjects

Namespace Payments
    Public Class BOApplicationSummary
        Inherits PaymentsBaseBO
        Implements IBOApplicationSummary
#Region " Prelim code "
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal summaryId As Int32, ByVal tran As SqlClient.SqlTransaction)
            MyClass.New()
            LoadApplicationSummary(summaryId, tran)
        End Sub

        Public Sub New(ByVal summaryId As Int32)
            MyClass.New(summaryId, Nothing)
        End Sub

        Private Function LoadApplicationSummary(ByVal id As Int32) As DataObjects.Entity.Application
            Return LoadApplicationSummary(id, Nothing)
        End Function

        Private Function LoadApplicationSummary(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.Application
            Dim app As Entity.Application = Entity.Application.GetById(id)
            If app Is Nothing Then
                Throw New RecordDoesNotExist("ApplicationSummary", id)
            Else
                InitialiseApplicationSummary(app, tran)
                Return app
            End If
        End Function

        Friend Overridable Sub InitialiseApplicationSummary(ByVal summary As DataObjects.Entity.Application, ByVal tran As SqlClient.SqlTransaction)
            With summary
'               Dim boapp As Application.BOApplication = New Application.BOApplication(.Id)
                Dim boapp As Application.BOApplication = Application.BOApplication.PolymorphicCreate(.Id)
                CheckSum = .CheckSum
                mApplicationId = .Id
                mDateReceived = .ApplicationDate
                mApplicationType = boapp.ApplicationType
                mPayStatus = "N/A"
                mOwner = boapp.OwnerString
                mTrueCost = 0
                mCost = 0
                If Not .IsStandardFeeNull Then mTrueCost = .StandardFee
                If Not .IsFeeChargedNull Then mCost = .FeeCharged
                If Not .IsPaymentStatusIdNull Then
                    Select Case .PaymentStatusId
                        Case Application.PaymentStatusTypes.Paid
                            mPayStatus = "Paid"
                        Case Application.PaymentStatusTypes.Unpaid
                            mPayStatus = "Unpaid"
                        Case Application.PaymentStatusTypes.Payment_Pending
                            mPayStatus = "Pending"
                    End Select
                End If
            End With
        End Sub
#End Region

#Region " Properties "
        Private mApplicationId As Integer
        Private mDateReceived As Date
        Private mApplicationType As String
        Private mPayStatus As String
        Private mOwner As String
        Private mTrueCost As Decimal
        Private mCost As Decimal

        Public Property ApplicationId() As Integer Implements IBOApplicationSummary.ApplicationId
            Get
                Return mApplicationId
            End Get
            Set(ByVal Value As Integer)
            End Set
        End Property


        Public Property DateReceived() As String Implements IBOApplicationSummary.DateReceived
            Get
                Return mDateReceived.ToString("dd MMMM yyyy")
            End Get
            Set(ByVal Value As String)
            End Set
        End Property

        Public Property ApplicationType() As String Implements IBOApplicationSummary.ApplicationType
            Get
                Return mApplicationType
            End Get
            Set(ByVal Value As String)
            End Set
        End Property

        Public Property PayStatus() As String Implements IBOApplicationSummary.PayStatus
            Get
                Return mPayStatus
            End Get
            Set(ByVal Value As String)
            End Set
        End Property

        Public Property Owner() As String Implements IBOApplicationSummary.Owner
            Get
                Return mOwner
            End Get
            Set(ByVal Value As String)
            End Set
        End Property


        Public Property TrueCost() As Decimal Implements IBOApplicationSummary.TrueCost
            Get
                Return mTrueCost
            End Get
            Set(ByVal Value As Decimal)
            End Set
        End Property

        Public Property TrueCostDisplay() As String Implements IBOApplicationSummary.TrueCostDisplay
            Get
                Return mTrueCost.ToString("C")
            End Get
            Set(ByVal Value As String)
            End Set
        End Property

        Public Property Cost() As Decimal Implements IBOApplicationSummary.Cost
            Get
                Return mCost
            End Get
            Set(ByVal Value As Decimal)
            End Set
        End Property

        Public Property CostDisplay() As String Implements IBOApplicationSummary.CostDisplay
            Get
                Return mCost.ToString("C")
            End Get
            Set(ByVal Value As String)
            End Set
        End Property

        Public Property RemoveX() As String Implements IBOApplicationSummary.RemoveX
            Get
                Return "X"
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
        Public Function GetApplicationSummary() As BOApplicationSummary
            If mApplicationId = 0 Then
                Throw New ArgumentException("ApplicationSummary Id is 0")
            Else
                Return New BOApplicationSummary(mApplicationId)
            End If
        End Function

        Public Sub MarkAsPaid()
            Dim app As Entity.Application = New Entity.Application(mApplicationId)
            app.PaymentStatusId = Application.PaymentStatusTypes.Paid
            app.PaidDate = Date.Now 'MLD added 15/11/4
            app.SaveChanges()
        End Sub

#End Region

#Region " Save "

#End Region

#Region " Validate "
#End Region

#Region " Operations "



        Private Shared Function LoadByApplicationId(ByVal summaryId As Int32) As BOApplicationSummary
            Return New BOApplicationSummary(summaryId)
        End Function

        Protected Function GetApplicationSummary(ByVal summaryId As Int32) As BOApplicationSummary
            Return LoadByApplicationId(summaryId)
        End Function

        Public Sub ReduceCost(ByVal paymentId As Int32, ByVal newCost As Decimal, ByVal reason As String)
            Dim reduction As New BOFeeReduction
            Dim app As New Entity.Application(mApplicationId)
            reduction.ApplicationId = mApplicationId
            reduction.PaymentId = paymentId
            reduction.Reason = reason
            'reduction.SSOUserId =
            reduction.DateTime = DateTime.Now
            reduction.Amount = mCost - newCost
            reduction.OriginalFee = mCost
            reduction.Save()
            If newCost = 0 Then
                app.PaymentBasketId = 0
                app.PaymentStatusId = Application.PaymentStatusTypes.Paid
            End If
            app.FeeCharged = newCost
            app.SaveChanges()
        End Sub

#End Region
    End Class
End Namespace



Namespace TaxonomyData
    Public Class BORequest
        Inherits BaseBO

#Region "Prelim Code"

        Public Sub New()
            InitialiseNew()
        End Sub

        Public Sub New(ByVal Request As DataObjects.Entity.TaxonomyDataLoadRequest)
            InitialiseDO(Request)
        End Sub

        Public Sub New(ByVal Dataset As DataSet)
            InitialiseDS(Dataset)
        End Sub

        Private Sub InitialiseNew()
            Me.Status = RequestResponseEnum.Initiated
        End Sub

        Private Sub InitialiseDO(ByVal Request As DataObjects.Entity.TaxonomyDataLoadRequest)
            If Request Is Nothing Then
                Throw New Exception("Request cannot be nothing")
            Else
                With Request
                    Me.TaxonomyDataLoadRequestID = .Id
                    Me.CheckSum = .CheckSum
                    Me.Date = .Date
                    Me.Status = CType(System.Enum.Parse(GetType(RequestResponseEnum), .Status.ToString), RequestResponseEnum)
                    Me.Message = .Message
                End With
            End If
        End Sub

        Private Sub InitialiseDS(ByVal Dataset As DataSet)

            If Dataset.Tables(0).Rows.Count = 1 Then
                With Dataset.Tables(0).Rows(0)
                    Me.TaxonomyDataLoadRequestID = Int32.Parse(.Item("TaxonomyDataLoadRequestID").ToString)
                    Me.CheckSum = Int32.Parse(.Item("CheckSum").ToString)
                    Me.Date = CType(.Item("Date"), System.DateTime)
                    Me.Status = CType(System.Enum.Parse(GetType(RequestResponseEnum), CType(.Item("Status"), String)), RequestResponseEnum)
                    Me.Message = .Item("Message").ToString
                End With
            Else
                Throw New Exception("Cannot initialise BORequest - invalid dataset")
            End If
        End Sub
#End Region

        Public Enum RequestResponseEnum
            Initiated = 0
            Fulfilled = 1
            StartTimesInvalid = 2
            RequestOutsideWindow = 3
            GeneralError = 4
            RequestPermissionFailed = 5
            OverridePermissionFailed = 6
            CannotAccessRequestPage = 7
        End Enum

#Region "Properties"

        Public Property TaxonomyDataLoadRequestID() As Int32
            Get
                Return mTaxonomyDataLoadRequestID
            End Get
            Set(ByVal Value As Int32)
                mTaxonomyDataLoadRequestID = Value
            End Set
        End Property
        Private mTaxonomyDataLoadRequestID As Int32

        Public Property Message() As String
            Get
                Return mMessage
            End Get
            Set(ByVal Value As String)
                mMessage = Value
            End Set
        End Property
        Private mMessage As String

        Public Property Status() As RequestResponseEnum
            Get
                Return mStatus
            End Get
            Set(ByVal Value As RequestResponseEnum)
                mStatus = Value
            End Set
        End Property
        Private mStatus As RequestResponseEnum

        Public Property [Date]() As Date
            Get
                Return mDate
            End Get
            Set(ByVal Value As Date)
                mDate = Value
            End Set
        End Property
        Private mDate As Date
#End Region

#Region "Methods"
        Friend Sub Deliver(ByVal Uri As String)
            Try
                Const TotalAttempts As Int32 = 5
                Dim Attempt As Int32 = 0
                Dim dr As New DataObjects.TaxonomyDataRequester
                Dim DeliverSuccessful As Boolean
                Do
                    DeliverSuccessful = dr.DeliverRequestToSupplier(Uri)
                    Attempt += 1
                Loop While DeliverSuccessful = False And Attempt < TotalAttempts
                If DeliverSuccessful = True Then
                    Me.Status = BORequest.RequestResponseEnum.Initiated
                Else
                    Me.Status = BORequest.RequestResponseEnum.CannotAccessRequestPage
                    Me.Message = dr.LastError.Message
                End If
            Catch ex As Exception
                Me.Status = BORequest.RequestResponseEnum.CannotAccessRequestPage
            End Try
        End Sub

        Friend Shared Function GetNextToFulfill(ByVal MessageDate As Date) As BORequest
            Try
                Dim NewDORequest As New DataObjects.Entity.TaxonomyDataLoadRequest


                Dim service As DataObjects.Service.TaxonomyDataLoadRequestService = NewDORequest.ServiceObject
                Dim DORequest As DataObjects.Entity.TaxonomyDataLoadRequest = service.GetNextToFulfill(MessageDate)
                If DORequest Is Nothing = False Then
                    Return New BORequest(DORequest)
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Throw New Exception("Cannot load the next Taxonomy Data Load Request to be fulfilled", ex)
            End Try
        End Function

#End Region

#Region " Save "

        Friend Overridable Shadows Function Save(ByVal Transaction As System.Data.SqlClient.SqlTransaction) As BORequest
            Try
                MyBase.Save()
                Dim DORequest As New DataObjects.Entity.TaxonomyDataLoadRequest
                Dim service As DataObjects.Service.TaxonomyDataLoadRequestService = DORequest.ServiceObject
                Me.Date = System.DateTime.Now
                If TaxonomyDataLoadRequestID = 0 Then
                    DORequest = service.Insert(Me.Date, Me.Status, Me.Message, Transaction)
                Else
                    DORequest = service.Update(Me.TaxonomyDataLoadRequestID, Me.Date, Me.Status, Me.Message, CheckSum, Transaction)
                End If
                InitialiseDO(DORequest)
                Return Me
            Catch ex As Exception
                Throw New Exception("Cannot save BORequest object", ex)
            End Try
        End Function

        Friend Overridable Shadows Function Save() As BORequest
            Return Me.Save(Nothing)
        End Function

#End Region
    End Class


End Namespace
Namespace TaxonomyData
    Public Class BOMessageStatus
        Public Enum MessageStatusEnum
            Pending = 0
            Loaded = 1
            Rejected = 2
        End Enum

        Public Sub New()

        End Sub

        Friend Sub New(ByVal NewBOMessageStatus As DataSet)
            With NewBOMessageStatus.Tables(0).Rows(0)
                Me.Status = CType(System.Enum.Parse(GetType(BOMessageStatus.MessageStatusEnum), CType(.Item("Status"), String)), BOMessageStatus.MessageStatusEnum)
                Me.Date = CType(.Item("StatusDateTime"), Date)
                Me.Diagnostics = CType(.Item("Diagnostics"), String)
            End With
        End Sub

        Public Property ID() As Int32
            Get
                Return mID
            End Get
            Set(ByVal Value As Int32)
                mID = Value
            End Set
        End Property
        Private mID As Int32

        Public Property Status() As MessageStatusEnum
            Get
                Return mStatus
            End Get
            Set(ByVal Value As MessageStatusEnum)
                mStatus = Value
                Me.Date = DateTime.Now
            End Set
        End Property
        Private mStatus As MessageStatusEnum

        Public Property [Date]() As Date
            Get
                Return mDate
            End Get
            Set(ByVal Value As Date)
                mDate = Value
            End Set
        End Property
        Private mDate As Date

        Public Property Diagnostics() As String
            Get
                Return mDiagnostics
            End Get
            Set(ByVal Value As String)
                mDiagnostics = Value
                Me.Date = DateTime.Now
            End Set
        End Property
        Private mDiagnostics As String

    End Class

    Friend Class BOMessage
        Inherits BaseBO
        Implements IComparable


#Region "Prelim code"
        Public Sub New(ByVal NewBase64Message As String, ByVal NewNumberOfTimesEncoded As Int32)
            Try
                Me.Base64Message = NewBase64Message
                Me.NumberOfTimesBase64Encoded = NewNumberOfTimesEncoded
                Me.Status = New BOMessageStatus
                Me.Status.Date = System.DateTime.Now
                Me.Status.Status = BOMessageStatus.MessageStatusEnum.Pending
            Catch Ex As Exception
                Throw New Exception("Cannot create new BOMessage object", Ex)
            End Try
        End Sub

        Private Sub New(ByVal Message As DataSet)
            InitialiseDO(Message)
        End Sub

        Private Sub InitialiseDO(ByVal Message As DataSet)
            If Message Is Nothing Then
                Throw New Exception("Message cannot be nothing")
            Else
                With Message.Tables(0).Rows(0)
                    Me.TaxonomyDataLoadMessageID = CType(.Item("TaxonomyDataLoadMessageID"), Int32)
                    Me.CheckSum = CType(.Item("CheckSum"), Int32)
                    Me.NumberOfTimesBase64Encoded = CType(.Item("NumberOfTimesEncoded"), Int32)
                    Me.Base64Message = CType(.Item("Base64Message"), String)
                    Me.Status = New BOMessageStatus
                    Me.Status.Status = CType(System.Enum.Parse(GetType(BOMessageStatus.MessageStatusEnum), CType(.Item("Status"), String)), BOMessageStatus.MessageStatusEnum)
                    Me.Status.Date = CType(.Item("StatusDateTime"), Date)
                    Me.Status.Diagnostics = CType(.Item("Diagnostics"), String)
                End With
            End If
        End Sub

#End Region

#Region "Properties"


        Public Property Status() As BOMessageStatus
            Get
                Return mStatus
            End Get
            Set(ByVal Value As BOMessageStatus)
                mStatus = Value
            End Set
        End Property
        Private mStatus As BOMessageStatus

        Public Property Files() As BOFiles
            Get
                Return mFiles
            End Get
            Set(ByVal Value As BOFiles)
                mFiles = Value
            End Set
        End Property
        Private mFiles As BOFiles



        Public Property TaxonomyDataLoadMessageID() As Int32
            Get
                Return mTaxonomyDataLoadMessageID
            End Get
            Set(ByVal Value As Int32)
                mTaxonomyDataLoadMessageID = Value
            End Set
        End Property
        Private mTaxonomyDataLoadMessageID As Int32

        Public Property Base64Message() As String
            Get
                Return mBase64Message
            End Get
            Set(ByVal Value As String)
                mBase64Message = Value
            End Set
        End Property
        Private mBase64Message As String

        Public Property NumberOfTimesBase64Encoded() As Int32
            Get
                Return mNumberOfTimesBase64Encoded
            End Get
            Set(ByVal Value As Int32)
                mNumberOfTimesBase64Encoded = Value
            End Set
        End Property
        Private mNumberOfTimesBase64Encoded As Int32

#End Region

#Region "Methods"

        Friend Shared Function LoadPreviouslyLoaded(ByVal TaxonomyDataLoadMessageID As Int32) As BOMessage
            Try
                Dim Results As DataSet = [DO].DataObjects.Sprocs.dbo_usp_SelectPreviousLoadedTaxonomyDataLoadMessage(TaxonomyDataLoadMessageID, Nothing, GetType(DataSet))
                If Results Is Nothing = False AndAlso Results.Tables.Count > 0 AndAlso Results.Tables(0).Rows.Count > 0 Then
                    Return New BOMessage(Results)
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Throw New Exception("Cannot load previously loaded Taxonomy Data Load Message", ex)
            End Try
        End Function

        Friend Shared Function LoadPrevious(ByVal TaxonomyDataLoadMessageID As Int32) As BOMessage
            Try
                Dim Results As DataSet
                Results = [DO].DataObjects.Sprocs.dbo_usp_SelectPreviousTaxonomyDataLoadMessage(TaxonomyDataLoadMessageID, Nothing, GetType(DataSet))
                If Results Is Nothing = False AndAlso Results.Tables.Count > 0 AndAlso Results.Tables(0).Rows.Count > 0 Then
                    Return New BOMessage(Results)
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Throw New Exception("Cannot load previous Taxonomy Data Load Message", ex)
            End Try
        End Function

        Public Sub DecodeMessage()
            Try
                Me.Files = New BOFiles(Me.Base64Message, Me.NumberOfTimesBase64Encoded)
            Catch ex As Exception
                Throw New Exception("Cannot decode message", ex)
            End Try
        End Sub

        Public Function GetScripter(ByVal PreviousMessage As BOMessage) As BOScripter
            If PreviousMessage Is Nothing = False Then
                Return New BOScripter(Me.Files.Transfer, PreviousMessage.Files.Transfer)
            Else
                Return New BOScripter(Me.Files.Transfer, Nothing)
            End If
        End Function

        Public Function FulfillLatestRequest() As BORequest
            Try
                Dim EO As New EnterpriseObjects.Service
                Dim Transaction As System.Data.SqlClient.SqlTransaction = EO.BeginTransaction()
                Try
                    'Get the request.
                    Dim ds As DataSet = [DO].DataObjects.Sprocs.dbo_usp_SelectTaxonomyDataLoadRequestNextToFulfill(Me.Status.Date, Transaction, GetType(DataSet))
                    'Test the request.
                    If ds Is Nothing = False _
                    AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Me.Status.Status = BOMessageStatus.MessageStatusEnum.Pending
                        Me.SaveStatus(Transaction)
                        Dim Request As New BORequest(ds)
                        Request.Status = BORequest.RequestResponseEnum.Fulfilled
                        Request.Message = "Request fullfilled by message:" & Me.TaxonomyDataLoadMessageID.ToString
                        Request.Save(Transaction)
                        EO.EndTransaction(Transaction, EnterpriseObjects.Service.TransactionEndEnum.Commit)
                        Return Request
                    Else
                        Me.Status.Status = BOMessageStatus.MessageStatusEnum.Rejected
                        Me.Status.Diagnostics = "Cannot find a request for this message."
                        Me.SaveStatus()
                        Return Nothing
                    End If
                Catch ex As Exception
                    EO.EndTransaction(Transaction, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    Throw ex
                End Try
            Catch ex As Exception
                Throw New Exception("Cannot fulfill latest request", ex)
            End Try
        End Function

        Public Function CompareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
            Try
                If TypeOf obj Is BOMessage Then
                    Dim Message As BOMessage = CType(obj, BOMessage)
                    Return Me.Files.CompareManifest(Message.Files)
                ElseIf obj Is Nothing Then
                    Return Me.Files.CompareManifest(Nothing)
                Else
                    Throw New ArgumentException("obj is not a BOMessage object")
                End If
            Catch ex As Exception
                Throw New Exception("cannot compare", ex)
            End Try
        End Function
#End Region

#Region " Save "

        Public Overridable Shadows Sub Save(ByVal Transaction As System.Data.SqlClient.SqlTransaction) 'As BOMessage
            Try
                MyBase.Save()
                Validate()
                Dim NewDOMessage As New DataObjects.Entity.TaxonomyDataLoadMessage
                Dim service As DataObjects.Service.TaxonomyDataLoadMessageService = NewDOMessage.ServiceObject
                '#If Transaction Is Nothing Then
                '#    Transaction = service.BeginTransaction
                '#End If
                If Me.TaxonomyDataLoadMessageID = 0 Then
                    NewDOMessage = service.Insert(Base64Message:=Me.Base64Message, numberoftimesencoded:=Me.NumberOfTimesBase64Encoded, Transaction:=Transaction)
                    Dim NewDOMessageStatus As New DataObjects.Entity.TaxonomyDataLoadMessageStatus
                    Dim StatuService As DataObjects.Service.TaxonomyDataLoadMessageStatusService = NewDOMessageStatus.ServiceObject
                    NewDOMessageStatus.Insert(TaxonomyDataLoadMessageID:=NewDOMessage.Id, statusdatetime:=Me.Status.Date, Status:=Me.Status.Status, Diagnostics:=Me.Status.Diagnostics) 'Add a transaction parameter.
                Else
                    NewDOMessage = service.Update(id:=Me.TaxonomyDataLoadMessageID, Base64Message:=Me.Base64Message, numberoftimesencoded:=Me.NumberOfTimesBase64Encoded, CheckSum:=Me.CheckSum, Transaction:=Transaction)
                End If
                '#Transaction.Commit()
                Me.TaxonomyDataLoadMessageID = NewDOMessage.Id
                'InitialiseDO(NewDOMessage)
                'Return Me
            Catch ex As Exception
                Throw New Exception("Cannot save Message", ex)
            End Try
        End Sub

        Public Overridable Shadows Sub SaveStatus()
            Me.SaveStatus(Nothing)
        End Sub

        Public Overridable Shadows Sub SaveStatus(ByVal Transaction As System.Data.SqlClient.SqlTransaction)
            Try
                Dim NewDOMessageStatus As New DataObjects.Entity.TaxonomyDataLoadMessageStatus
                Dim StatuService As DataObjects.Service.TaxonomyDataLoadMessageStatusService = NewDOMessageStatus.ServiceObject
                NewDOMessageStatus.Insert(TaxonomyDataLoadMessageID:=Me.TaxonomyDataLoadMessageID, statusdatetime:=Me.Status.Date, Status:=Me.Status.Status, Diagnostics:=Me.Status.Diagnostics)  'Add a transaction parameter.
            Catch ex As Exception
                Throw New Exception("Cannot save Message Status", ex)
            End Try
        End Sub

        Public Overridable Shadows Sub Save() 'As BOMessage
            Me.Save(Nothing)
        End Sub

#End Region

#Region " Validate "

        Protected Overridable Overloads Function Validate() As ValidationManager
            Return New ValidationManager
        End Function

#End Region


    End Class
End Namespace
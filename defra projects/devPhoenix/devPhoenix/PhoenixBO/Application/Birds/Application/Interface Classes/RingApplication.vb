Imports uk.gov.defra.Phoenix.BO.ValidationError
Imports uk.gov.defra.Phoenix.BO.ValidationError.ValidationCodes

'Namespace Application.Bird.Applications
'    Public Class RingApplication
'        Inherits BaseBO ' BOApplication

'#Region " Prelim code "

'        Public Sub New()
'            MyBase.New()
'        End Sub

'        Public Sub New(ByVal applicationId As Int32)
'            MyClass.New()
'            LoadRingApplication(applicationId)
'        End Sub

'        Private Function LoadRingApplication(ByVal id As Int32) As DataObjects.Entity.RingApplication
'            Return LoadRingApplication(id, Nothing)
'        End Function

'        Private Function LoadRingApplication(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.RingApplication
'            Dim NewRingApplication As DataObjects.Entity.RingApplication = DataObjects.Entity.RingApplication.GetById(id)
'            If NewRingApplication Is Nothing Then
'                Throw New RecordDoesNotExist("RingApplication", id)
'            Else
'                InitialiseRingApplication(NewRingApplication, tran)
'                Return NewRingApplication
'            End If
'        End Function

'        Friend Overridable Sub InitialiseRingApplicationByApplicationId(ByVal applicationId As Int32)
'            InitialiseRingApplicationByApplicationId(applicationId, Nothing)
'        End Sub

'        Friend Overridable Sub InitialiseRingApplicationByApplicationId(ByVal applicationId As Int32, ByVal tran As SqlClient.SqlTransaction)
'            If applicationId = 0 Then
'                Throw New ArgumentException("Application Id is 0")
'            Else
'                Dim Apps As DataObjects.EntitySet.RingApplicationSet = DataObjects.Entity.RingApplication.GetForApplication(applicationId, tran)
'                Dim RingApps As DataObjects.EntitySet.RingApplicationSet = DataObjects.Entity.RingApplication.GetForApplication(applicationId)
'                If Not RingApps Is Nothing AndAlso _
'                   RingApps.Count = 1 Then
'                    InitialiseRingApplication(CType(RingApps.GetEntity(0), DataObjects.Entity.RingApplication), tran)
'                Else
'                    Throw New ArgumentException("Cannot find ring application")
'                End If
'            End If
'        End Sub

'        Friend Overridable Sub InitialiseRingApplication(ByVal ringApplication As DataObjects.Entity.RingApplication, ByVal tran As SqlClient.SqlTransaction)
'            With ringApplication
'                mRingChecksum = .CheckSum
'                mRingApplicationId = .Id
'                mPartyAcknowledgedAsKeeper = .PartyAcknowledgedAsKeeper
'                mProvisionalXML = .ProvisionalXML
'            End With
'        End Sub
'#End Region

'#Region " Properties "
'        Public Property RingChecksum() As Integer
'            Get
'                Return mRingChecksum
'            End Get
'            Set(ByVal Value As Integer)
'                mRingChecksum = Value
'            End Set
'        End Property
'        Private mRingChecksum As Int32

'        Public Property PartyAcknowledgedAsKeeper() As Boolean
'            Get
'                Return mPartyAcknowledgedAsKeeper
'            End Get
'            Set(ByVal Value As Boolean)
'                mPartyAcknowledgedAsKeeper = Value
'            End Set
'        End Property
'        Private mPartyAcknowledgedAsKeeper As Boolean

'        Public Property ProvisionalXML() As String
'            Get
'                Return mProvisionalXML
'            End Get
'            Set(ByVal Value As String)
'                mProvisionalXML = Value
'            End Set
'        End Property
'        Private mProvisionalXML As String

'        Public Property RingApplicationId() As Int32
'            Get
'                Return mRingApplicationId
'            End Get
'            Set(ByVal Value As Int32)
'                mRingApplicationId = Value
'            End Set
'        End Property
'        Private mRingApplicationId As Int32
'#End Region

'#Region " Save "
'        Public Overloads Overrides Function Save() As BaseBO
'            Dim NewApplication As New DataObjects.Entity.RingApplication
'            Dim service As DataObjects.Service.RingApplicationService = NewApplication.ServiceObject
'            Dim tran As SqlClient.SqlTransaction = service.BeginTransaction

'            Dim SaveResult As BaseBO = MyClass.Save(tran)
'            If SaveResult Is Nothing Then
'                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
'            Else
'                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Commit)
'            End If
'            Return SaveResult
'        End Function

'        Public Overridable Overloads Function Save(ByVal tran As SqlClient.SqlTransaction) As BaseBO
'            Dim NewRingApplication As New DataObjects.Entity.RingApplication
'            Dim service As DataObjects.Service.RingApplicationService = NewRingApplication.ServiceObject

'            Dim Application As Object = MyBase.Save(tran)
'            Created = (mRingApplicationId = 0)
'            If Not Application Is Nothing AndAlso _
'               Not CType(Application, BaseBO).ValidationErrors Is Nothing Then
'                'rollback the transaction
'                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
'                'get the problems and assign them locally
'                ValidationErrors = CType(Application, BaseBO).ValidationErrors
'                'bail
'                Return Me
'            End If

'            If Created Then
'                NewRingApplication = service.Insert(ApplicationId, _
'                                                    mPartyAcknowledgedAsKeeper, _
'                                                    mProvisionalXML, _
'                                                    tran)

'            Else
'                NewRingApplication = service.Update(ApplicationId, _
'                                                    mPartyAcknowledgedAsKeeper, _
'                                                    mProvisionalXML, _
'                                                    RingChecksum, _
'                                                    tran)
'            End If

'            ''check to see if any SQL errors have occured
'            If (NewRingApplication Is Nothing AndAlso Not DataObjects.Sprocs.LastError Is Nothing) Then
'                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
'                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSavePermit)
'            Else
'                'If Created And Not NewRingApplication Is Nothing Then
'                '    mCITESApplicationId = NewCITESApplication.Id
'                'End If

'                Try
'                    If NewRingApplication.CheckSum <> RingChecksum Then
'                        InitialiseRingApplication(NewRingApplication, tran)
'                    End If
'                Catch ex As Exception
'                    If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
'                    ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSavePermit)
'                End Try
'            End If

'            Return Me
'        End Function
'#End Region

'    End Class
'End Namespace
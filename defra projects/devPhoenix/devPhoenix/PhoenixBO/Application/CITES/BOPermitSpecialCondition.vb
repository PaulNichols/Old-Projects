Namespace Application.CITES.Applications
    Public Class BOPermitSpecialCondition
        Inherits BaseBO
        Implements IBOPermitSpecialCondition


#Region " Prelim code "
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal permitSpecialConditionId As Int32)
            MyClass.New()
            LoadPermitSpecialCondition(permitSpecialConditionId)
        End Sub

        Private Function LoadPermitSpecialCondition(ByVal permitSpecialConditionId As Int32) As DataObjects.Entity.PermitSpecialCondition
            Return LoadPermitSpecialCondition(permitSpecialConditionId, Nothing)
        End Function

        Private Function LoadPermitSpecialCondition(ByVal permitSpecialConditionId As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.PermitSpecialCondition
            Dim NewPermitCondition As DataObjects.Entity.PermitSpecialCondition = DataObjects.Entity.PermitSpecialCondition.GetById(permitSpecialConditionId)
            If NewPermitCondition Is Nothing Then
                Throw New RecordDoesNotExist("Permit Scientific Advice", permitSpecialConditionId)
            Else
                InitialisePermitCondition(NewPermitCondition, tran)
                Return NewPermitCondition
            End If
        End Function

        Protected Overridable Sub InitialisePermitCondition(ByVal permitCondition As DataObjects.Entity.PermitSpecialCondition, ByVal tran As SqlClient.SqlTransaction)
            With permitCondition
                mPermitSpecialConditionId = .PermitSpecialConditionId
                mPermitId = .PermitId
                If Not .IsBFDateNull Then mBFDate = .BFDate
                If Not .IsStatusIdNull Then mStatusId = CType(.StatusId, SpecialConditionStatus)
                mDateApplied = .DateApplied
                mSpecialConditionId = .SpecialConditionId
                mSSOUserId = .SSOUserId
                If Not .IsConditionNull Then mCondition = .Condition
                mCurrent = .Current
                If Not .IsCheckSumNull Then Me.mPermitSpecialConditionCheckSum = .CheckSum
                Me.mSpecialCondition = New ReferenceData.BOSpecialCondition(.SpecialConditionId, tran)
                If Not .IsAddedBySANull Then mAddedBySA = .AddedBySA
            End With
        End Sub
#End Region

#Region " Properties"
        Public Property BFDateGrid() As String Implements IBOPermitSpecialCondition.BFDateGrid
            Get
                If Not BFDate Is Nothing Then
                    Return CType(BFDate, Date).ToShortDateString
                Else
                    Return ""
                End If
            End Get
            Set(ByVal Value As String)

            End Set
        End Property

        Public Property DateAppliedGrid() As String Implements IBOPermitSpecialCondition.DateAppliedGrid
            Get
                Return DateApplied.ToShortDateString()
            End Get
            Set(ByVal Value As String)

            End Set
        End Property

        Public Property AddedBySA() As Boolean Implements IBOPermitSpecialCondition.AddedBySA
            Get
                Return mAddedBySA
            End Get
            Set(ByVal Value As Boolean)
                mAddedBySA = Value
            End Set
        End Property
        Private mAddedBySA As Boolean

        Public Property SpecialCondition() As ReferenceData.BOSpecialCondition Implements IBOPermitSpecialCondition.SpecialCondition
            Get
                Return mSpecialCondition
            End Get
            Set(ByVal Value As ReferenceData.BOSpecialCondition)
                mSpecialCondition = Value
            End Set
        End Property
        Private mSpecialCondition As ReferenceData.BOSpecialCondition

        Public Property User() As String Implements IBOPermitSpecialCondition.User
            Get
                Dim BOUser As New BOAuthorisedUser(CType(SSOUserId, Long))
                Return BOUser.FullName
            End Get
            Set(ByVal Value As String)

            End Set
        End Property

        Public Property Standard() As Boolean Implements IBOPermitSpecialCondition.Standard
            Get
                Return (Me.Condition Is Nothing OrElse Condition.Length = 0)
            End Get
            Set(ByVal Value As Boolean)

            End Set
        End Property

        Public Property StandardString() As String Implements IBOPermitSpecialCondition.StandardString
            Get
                If Standard Then
                    Return "Y"
                Else
                    Return "N"
                End If
            End Get
            Set(ByVal Value As String)

            End Set
        End Property

        Public Property Status() As String Implements IBOPermitSpecialCondition.Status
            Get
                Return Me.StatusId.ToString.Replace("_", "")
            End Get
            Set(ByVal Value As String)

            End Set
        End Property

        Public Property BFDate() As Object Implements IBOPermitSpecialCondition.BFDate
            Get
                Return mBFDate
            End Get
            Set(ByVal Value As Object)
                mBFDate = Value
            End Set
        End Property
        Private mBFDate As Object

        Public Property Condition() As String Implements IBOPermitSpecialCondition.Condition
            Get
                Return mCondition
            End Get
            Set(ByVal Value As String)
                mCondition = Value
            End Set
        End Property
        Private mCondition As String

        Public Property Current() As Boolean Implements IBOPermitSpecialCondition.Current
            Get
                Return mCurrent
            End Get
            Set(ByVal Value As Boolean)
                mCurrent = Value
            End Set
        End Property
        Private mCurrent As Boolean

        Public Property DateApplied() As Date Implements IBOPermitSpecialCondition.DateApplied
            Get
                Return mDateApplied
            End Get
            Set(ByVal Value As Date)
                mDateApplied = Value
            End Set
        End Property
        Private mDateApplied As Date

        Public Property PermitId() As Integer Implements IBOPermitSpecialCondition.PermitId
            Get
                Return mPermitId
            End Get
            Set(ByVal Value As Integer)
                mPermitId = Value
            End Set
        End Property
        Private mPermitId As Int32

        Public Property PermitSpecialConditionId() As Integer Implements IBOPermitSpecialCondition.PermitSpecialConditionId
            Get
                Return mPermitSpecialConditionId
            End Get
            Set(ByVal Value As Integer)
                mPermitSpecialConditionId = Value
            End Set
        End Property
        Private mPermitSpecialConditionId As Int32

        Public Property SpecialConditionId() As Integer Implements IBOPermitSpecialCondition.SpecialConditionId
            Get
                Return mSpecialConditionId
            End Get
            Set(ByVal Value As Integer)
                mSpecialConditionId = Value
            End Set
        End Property
        Private mSpecialConditionId As Int32

        Public Property SSOUserId() As Decimal Implements IBOPermitSpecialCondition.SSOUserId
            Get
                Return mSSOUserId
            End Get
            Set(ByVal Value As Decimal)
                mSSOUserId = Value
            End Set
        End Property
        Private mSSOUserId As Decimal

        Public Property StatusId() As SpecialConditionStatus Implements IBOPermitSpecialCondition.StatusId
            Get
                Return mStatusId
            End Get
            Set(ByVal Value As SpecialConditionStatus)
                mStatusId = Value
            End Set
        End Property
        Private mStatusId As SpecialConditionStatus

        Public Property PermitSpecialConditionCheckSum() As Integer Implements IBOPermitSpecialCondition.PermitSpecialConditionCheckSum
            Get
                Return mPermitSpecialConditionCheckSum
            End Get
            Set(ByVal Value As Integer)
                mPermitSpecialConditionCheckSum = Value
            End Set
        End Property
        Private mPermitSpecialConditionCheckSum As Int32
#End Region

#Region " Delete "
        Public Shadows Function Delete() As Boolean
            Dim PermitSpecialCondition As New DataObjects.Entity.PermitSpecialCondition
            If PermitSpecialCondition.DeleteById(Me.PermitSpecialConditionId) Then
                Return True
            Else
                Return False
            End If
        End Function
#End Region

#Region " Save "
        Public Overridable Shadows Function Save() As BOPermitSpecialCondition

            Dim NewPermitSpecialCondition As New DataObjects.Entity.PermitSpecialCondition
            Dim service As DataObjects.Service.PermitSpecialConditionService = NewPermitSpecialCondition.ServiceObject

            Dim tran As SqlClient.SqlTransaction = service.BeginTransaction
            MyBase.Save()

            If DataObjects.Sprocs.LastError Is Nothing Then

                Created = (mPermitSpecialConditionId = 0)

                If Created Then
                    NewPermitSpecialCondition = service.Insert(mPermitId, _
                                                                mBFDate, _
                                                                mStatusId, _
                                                                 mDateApplied, _
                                                                mSpecialConditionId, _
                                                                mSSOUserId, _
                                                                mCondition, _
                                                                 mCurrent, _
                                                                 mAddedBySA, _
                                                                tran)
                Else
                    NewPermitSpecialCondition = service.Update(mPermitSpecialConditionId, _
                                                           mPermitId, _
                                                                mBFDate, _
                                                                mStatusId, _
                                                                 mDateApplied, _
                                                                mSpecialConditionId, _
                                                                mSSOUserId, _
                                                                mCondition, _
                                                                 mCurrent, _
                                                                 mAddedBySA, _
                                                                tran)
                End If


                'check to see if any SQL errors have occured
                If (NewPermitSpecialCondition Is Nothing AndAlso Not DataObjects.Sprocs.LastError Is Nothing) Then
                    'TODO: Use errors collection to check to see if the problem was concurrency
                    service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveSeizureNotification)
                    Return Me
                ElseIf Created And Not NewPermitSpecialCondition Is Nothing Then
                    mPermitSpecialConditionId = NewPermitSpecialCondition.Id
                End If

                If NewPermitSpecialCondition.CheckSum <> PermitSpecialConditionCheckSum Then
                    'no point in initialising unless things have changed
                    service.EndTransaction(tran)
                    Me.InitialisePermitCondition(NewPermitSpecialCondition, Nothing)
                Else
                    service.EndTransaction(tran)
                End If
                Return Me
            Else
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.DoNothing)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveSeizureNotification)
                Return Me
            End If
        End Function
#End Region


    End Class
End Namespace
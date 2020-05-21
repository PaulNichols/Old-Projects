Namespace Application.CITES.Applications
    Public Class BOPermitScientificAdvice
        Inherits BaseBO
        Implements IBOPermitScientificAdvice



#Region " Prelim code "
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal permitAdviceid As Int32)
            MyClass.New()
            LoadPermitAdvice(permitAdviceid)
        End Sub

        Private Function LoadPermitAdvice(ByVal permitAdviceid As Int32) As DataObjects.Entity.PermitScientificAdvice
            Return LoadPermitAdvice(permitAdviceid, Nothing)
        End Function

        Private Function LoadPermitAdvice(ByVal permitAdviceid As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.PermitScientificAdvice
            Dim NewPermitAdvice As DataObjects.Entity.PermitScientificAdvice = DataObjects.Entity.PermitScientificAdvice.GetById(permitAdviceid)
            If NewPermitAdvice Is Nothing Then
                Throw New RecordDoesNotExist("Permit Scientific Advice", permitAdviceid)
            Else
                InitialisePermitAdvice(NewPermitAdvice, tran)
                Return NewPermitAdvice
            End If
        End Function

        Protected Overridable Sub InitialisePermitAdvice(ByVal permitAdvice As DataObjects.Entity.PermitScientificAdvice, ByVal tran As SqlClient.SqlTransaction)
            With permitAdvice
                mPermitScientificAdviceId = .PermitScientificAdvice
                mDateOfAdvice = .DateOfAdvice
                mScientificAdviceId = .ScientificAdviceId
                mPermitId = .PermitId
                mSSOUserId = .SSOUserId
                If Not .IsSpecificAdviceNull Then mSpecificAdvice = .SpecificAdvice
                mCurrent = .Current
                If Not .IsCheckSumNull Then Me.mPermitScientificAdviceCheckSum = .CheckSum
                mScientificAdvice = New ReferenceData.BOScientificAdvice(.ScientificAdviceId, tran)
            End With
        End Sub
#End Region

#Region " Properties "

        Public Property ScientificAdvice() As ReferenceData.BOScientificAdvice Implements IBOPermitScientificAdvice.ScientificAdvice
            Get
                Return mScientificAdvice
            End Get
            Set(ByVal Value As ReferenceData.BOScientificAdvice)
                mScientificAdvice = Value
            End Set
        End Property
        Private mScientificAdvice As ReferenceData.BOScientificAdvice

        Public Property DateOfAdviceGrid() As String Implements IBOPermitScientificAdvice.DateOfAdviceGrid
            Get
                Return mDateOfAdvice.ToShortDateString
            End Get
            Set(ByVal Value As String)
            End Set
        End Property

        Public Property User() As String Implements IBOPermitScientificAdvice.User
            Get
                Dim BOUser As New BOAuthorisedUser(CType(SSOUserId, Long))
                Return BOUser.FullName
            End Get
            Set(ByVal Value As String)

            End Set
        End Property

        Public Property ShortAdviceText() As String Implements IBOPermitScientificAdvice.ShortAdviceText
            Get
                Dim BOSA As New ReferenceData.BOScientificAdvice(Me.ScientificAdviceId)
                If Not BOSA.AdviceText.Length = 0 Then
                    If BOSA.AdviceText.Length < 50 Then
                        Return BOSA.AdviceText
                    Else
                        Return String.Concat(BOSA.AdviceText.Substring(0, 50), "...")
                    End If

                End If
            End Get
            Set(ByVal Value As String)

            End Set
        End Property

        Public Property Current() As Boolean Implements IBOPermitScientificAdvice.Current
            Get
                Return mCurrent
            End Get
            Set(ByVal Value As Boolean)
                mCurrent = Value
            End Set
        End Property
        Private mCurrent As Boolean

        Public Property DateOfAdvice() As Date Implements IBOPermitScientificAdvice.DateOfAdvice
            Get
                Return mDateOfAdvice
            End Get
            Set(ByVal Value As Date)
                mDateOfAdvice = Value
            End Set
        End Property
        Private mDateOfAdvice As Date

        Public Property PermitId() As Integer Implements IBOPermitScientificAdvice.PermitId
            Get
                Return mPermitId
            End Get
            Set(ByVal Value As Integer)
                mPermitId = Value
            End Set
        End Property
        Private mPermitId As Int32

        Public Property PermitScientificAdviceId() As Int32 Implements IBOPermitScientificAdvice.PermitScientificAdviceId
            Get
                Return mPermitScientificAdviceId
            End Get
            Set(ByVal Value As Int32)
                mPermitScientificAdviceId = Value
            End Set
        End Property
        Private mPermitScientificAdviceId As Int32

        Public Property ScientificAdviceId() As Integer Implements IBOPermitScientificAdvice.ScientificAdviceId
            Get
                Return mScientificAdviceId
            End Get
            Set(ByVal Value As Integer)
                mScientificAdviceId = Value
            End Set
        End Property
        Private mScientificAdviceId As Int32

        Public Property SpecificAdvice() As String Implements IBOPermitScientificAdvice.SpecificAdvice
            Get
                Return mSpecificAdvice
            End Get
            Set(ByVal Value As String)
                mSpecificAdvice = Value
            End Set
        End Property
        Private mSpecificAdvice As String

        Public Property SSOUserId() As Decimal Implements IBOPermitScientificAdvice.SSOUserId
            Get
                Return mSSOUserId
            End Get
            Set(ByVal Value As Decimal)
                mSSOUserId = Value
            End Set
        End Property
        Private mSSOUserId As Decimal

        Public Property PermitScientificAdviceCheckSum() As Integer Implements IBOPermitScientificAdvice.PermitScientificAdviceCheckSum
            Get
                Return mPermitScientificAdviceCheckSum
            End Get
            Set(ByVal Value As Integer)
                mPermitScientificAdviceCheckSum = Value
            End Set
        End Property
        Private mPermitScientificAdviceCheckSum As Int32

#End Region

#Region " Save "
        Public Overridable Shadows Function Save() As BOPermitScientificAdvice

            Dim NewPermitScientificAdvice As New DataObjects.Entity.PermitScientificAdvice
            Dim service As DataObjects.Service.PermitScientificAdviceService = NewPermitScientificAdvice.ServiceObject

            Dim tran As SqlClient.SqlTransaction = service.BeginTransaction
            MyBase.Save()

            If DataObjects.Sprocs.LastError Is Nothing Then

                Created = (mPermitScientificAdviceId = 0)

                If Created Then
                    NewPermitScientificAdvice = service.Insert(mDateOfAdvice, _
                                                            mScientificAdviceId, _
                                                            mPermitId, _
                                                            mSSOUserId, _
                                                            mSpecificAdvice, _
                                                            mCurrent, _
                                                            tran)
                Else
                    NewPermitScientificAdvice = service.Update(mPermitScientificAdviceId, _
                                                           mDateOfAdvice, _
                                                            mScientificAdviceId, _
                                                            mPermitId, _
                                                            mSSOUserId, _
                                                            mSpecificAdvice, _
                                                            mCurrent, _
                                                            tran)
                End If


                'check to see if any SQL errors have occured
                If (NewPermitScientificAdvice Is Nothing AndAlso Not DataObjects.Sprocs.LastError Is Nothing) Then
                    'TODO: Use errors collection to check to see if the problem was concurrency
                    service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveSeizureNotification)
                    Return Me
                ElseIf Created And Not NewPermitScientificAdvice Is Nothing Then
                    mPermitScientificAdviceId = NewPermitScientificAdvice.Id
                End If

                If NewPermitScientificAdvice.CheckSum <> PermitScientificAdviceCheckSum Then
                    'no point in initialising unless things have changed
                    service.EndTransaction(tran)
                    Me.InitialisePermitAdvice(NewPermitScientificAdvice, Nothing)
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

#Region " Delete "
        Public Shadows Function Delete() As Boolean
            Dim PermitScientificAdvice As New DataObjects.Entity.PermitScientificAdvice
            If PermitScientificAdvice.DeleteById(Me.PermitScientificAdviceId) Then
                Return True
            Else
                Return False
            End If
        End Function
#End Region

    End Class
End Namespace

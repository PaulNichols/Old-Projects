Namespace Application
    Public Class BOAdditionalDeclaration
        Inherits BaseBO
        Implements IBOAdditionalDeclaration

#Region " Prelim code "
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal declarationId As Int32)
            MyClass.New()
            LoadAdditionalDeclaration(declarationId)
        End Sub

        Private Function LoadAdditionalDeclaration(ByVal id As Int32) As DataObjects.Entity.AdditionalDeclaration
            Return LoadAdditionalDeclaration(id, Nothing)
        End Function

        Protected Function LoadAdditionalDeclaration(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.AdditionalDeclaration
            Dim NewAdditionalDeclaration As DataObjects.Entity.AdditionalDeclaration = DataObjects.Entity.AdditionalDeclaration.GetById(id, tran)
            If NewAdditionalDeclaration Is Nothing Then
                Throw New RecordDoesNotExist("AdditionalDeclaration", id)
            Else
                InitialiseAdditionalDeclaration(NewAdditionalDeclaration, tran)
                Return NewAdditionalDeclaration
            End If
        End Function

        Friend Overridable Sub InitialiseAdditionalDeclaration(ByVal additionalDeclaration As DataObjects.Entity.AdditionalDeclaration, ByVal tran As SqlClient.SqlTransaction)
            With additionalDeclaration
                mAdditionalDeclarationId = .Id
                CheckSum = .CheckSum

                If Not .IsFurtherPossesionDetailsNull() Then mFurtherPossesionDetails = .FurtherPossesionDetails
                If Not .IsHadRejectedCITESApplicationNull() Then mHadRejectedCITESApplication = .HadRejectedCITESApplication
                If Not .IsOtherInfoNull() Then mOtherInformation = .OtherInfo
                If Not .IsApplicationDateNull() Then mApplicationDate = .ApplicationDate
                mFalseStatement = .FalseStatement
                mConfirmAddress = .ConfirmAddress

                mCITESApplicationId = .CITESApplicationId
                mDeclarationAcknowledged = .DeclarationAcknowledged
            End With
        End Sub
#End Region

#Region " Properties "
        Public Property ApplicationDate() As Object Implements IBOAdditionalDeclaration.ApplicationDate
            Get
                Return mApplicationDate
            End Get
            Set(ByVal Value As Object)
                mApplicationDate = Value
            End Set
        End Property
        Private mApplicationDate As Object

        Public Property FurtherPossesionDetails() As String Implements IBOAdditionalDeclaration.FurtherPossesionDetails
            Get
                Return mFurtherPossesionDetails
            End Get
            Set(ByVal Value As String)
                mFurtherPossesionDetails = Value
            End Set
        End Property
        Private mFurtherPossesionDetails As String

        Public Property HadRejectedCITESApplication() As String Implements IBOAdditionalDeclaration.HadRejectedCITESApplication
            Get
                Return mHadRejectedCITESApplication
            End Get
            Set(ByVal Value As String)
                mHadRejectedCITESApplication = Value
            End Set
        End Property
        Private mHadRejectedCITESApplication As String

        Public Property OtherInformation() As String Implements IBOAdditionalDeclaration.OtherInformation
            Get
                Return mOtherInformation
            End Get
            Set(ByVal Value As String)
                mOtherInformation = Value
            End Set
        End Property
        Private mOtherInformation As String

        Public Property AdditionalDeclarationId() As Integer Implements IBOAdditionalDeclaration.AdditionalDeclarationId
            Get
                Return mAdditionalDeclarationId
            End Get
            Set(ByVal Value As Integer)
                mAdditionalDeclarationId = Value
            End Set
        End Property
        Private mAdditionalDeclarationId As Int32

        Public Property CITESApplicationId() As Integer Implements IBOAdditionalDeclaration.CITESApplicationId
            Get
                Return mCITESApplicationId
            End Get
            Set(ByVal Value As Integer)
                mCITESApplicationId = Value
            End Set
        End Property
        Private mCITESApplicationId As Int32

        Public Property DeclarationAcknowledged() As Boolean Implements IBOAdditionalDeclaration.DeclarationAcknowledged
            Get
                Return mDeclarationAcknowledged
            End Get
            Set(ByVal Value As Boolean)
                mDeclarationAcknowledged = Value
            End Set
        End Property
        Private mDeclarationAcknowledged As Boolean

        Public Property ConfirmAddress() As Boolean Implements IBOAdditionalDeclaration.ConfirmAddress
            Get
                Return mConfirmAddress
            End Get
            Set(ByVal Value As Boolean)
                mConfirmAddress = Value
            End Set
        End Property
        Private mConfirmAddress As Boolean

        Public Property FalseStatement() As Boolean Implements IBOAdditionalDeclaration.FalseStatement
            Get
                Return mFalseStatement
            End Get
            Set(ByVal Value As Boolean)
                mFalseStatement = Value
            End Set
        End Property
        Private mFalseStatement As Boolean

        Public Property HadRejectedCITESApplication_Boolean() As Boolean Implements IBOAdditionalDeclaration.HadRejectedCITESApplication_Boolean
            Get
                If Not mHadRejectedCITESApplication Is Nothing AndAlso _
                   mHadRejectedCITESApplication.Length > 0 Then
                    Return True
                Else
                    Return False
                End If
            End Get
            Set(ByVal Value As Boolean)
            End Set
        End Property

        Friend ReadOnly Property Confirmed() As Boolean Implements IBOAdditionalDeclaration.Confirmed
            Get
                Return mDeclarationAcknowledged And mConfirmAddress And mFalseStatement
            End Get
        End Property
#End Region

#Region " Save "
        Public Overloads Function Save(ByVal tran As SqlClient.SqlTransaction) As Object
            MyBase.Save()

            Dim NewAdditionalDeclaration As New DataObjects.Entity.AdditionalDeclaration
            Dim service As DataObjects.Service.AdditionalDeclarationService = NewAdditionalDeclaration.ServiceObject

            Created = (mAdditionalDeclarationId = 0)

            If Created Then
                NewAdditionalDeclaration = service.Insert(mOtherInformation, _
                                                          mHadRejectedCITESApplication, _
                                                          mFurtherPossesionDetails, _
                                                          mDeclarationAcknowledged, _
                                                          mCITESApplicationId, _
                                                          date.Now, _
                                                          mConfirmAddress, _
                                                          mFalseStatement, _
                                                          tran)
            Else
                NewAdditionalDeclaration = service.Update(mAdditionalDeclarationId, _
                                                          mOtherInformation, _
                                                          mHadRejectedCITESApplication, _
                                                          mFurtherPossesionDetails, _
                                                          mDeclarationAcknowledged, _
                                                          mCITESApplicationId, _
                                                          date.Now, _
                                                          mConfirmAddress, _
                                                          mFalseStatement, _
                                                          CheckSum, _
                                                          tran)
            End If


            'check to see if any SQL errors have occured
            If (NewAdditionalDeclaration Is Nothing AndAlso Not DataObjects.Sprocs.LastError Is Nothing) Then
                'TODO: Use errors collection to check to see if the problem was concurrency
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveAdditionalDeclaration)
            Else
                If Created And Not NewAdditionalDeclaration Is Nothing Then
                    mAdditionalDeclarationId = NewAdditionalDeclaration.Id
                End If
                Try
                    If NewAdditionalDeclaration.CheckSum <> CheckSum Then
                        InitialiseAdditionalDeclaration(NewAdditionalDeclaration, tran)
                    End If
                Catch ex As Exception
                    If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveAdditionalDeclaration)
                End Try
            End If

            Return Me
        End Function
#End Region

    End Class
End Namespace
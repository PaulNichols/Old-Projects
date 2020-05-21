Namespace Application
    Public Class BOTaxonIdentifier
        Inherits BaseBO
        Implements Application.IBOTaxon

#Region " Prelim code "
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal Id As Int32, ByVal tran As SqlClient.SqlTransaction)
            MyClass.New()
            LoadTaxon(Id, tran)
        End Sub

        Protected Overridable Function LoadTaxon(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.Taxon
            Dim NewTaxon As DataObjects.Entity.Taxon = DataObjects.Entity.Taxon.GetById(id, tran)
            If NewTaxon Is Nothing Then
                Throw New RecordDoesNotExist("Taxon", id)
            Else
                InitialiseTaxon(NewTaxon, tran)
                Return NewTaxon
            End If
        End Function

        Protected Overridable Sub InitialiseTaxon(ByVal taxon As DataObjects.Entity.Taxon, ByVal tran As SqlClient.SqlTransaction)
            With taxon
                mId = .Id
                CheckSum = .CheckSum
                mKingdomID = .TaxonomyKingdomID
                mTaxonID = .TaxonomyTaxonID
                mTaxonTypeID = .TaxonomyTaxonTypeID
                mIsCoral = .IsCoral
                mPaymentKingdom = CType(.PaymentKingdom, PaymentKingdomEnum)
                mPaymentTaxonType = CType(.PaymentTaxonType, PaymentTaxonTypeEnum)
            End With
        End Sub

        Public Overridable Sub InitialiseTaxon(ByVal taxon As BOTaxonIdentifier, ByVal tran As SqlClient.SqlTransaction)
            With taxon
                mId = .Id
                CheckSum = .CheckSum
                mKingdomID = .KingdomID
                mTaxonID = .TaxonID
                mTaxonTypeID = .TaxonTypeID
                mIsCoral = .IsCoral
                mPaymentKingdom = CType(.PaymentKingdom, PaymentKingdomEnum)
                mPaymentTaxonType = CType(.PaymentTaxonType, PaymentTaxonTypeEnum)
            End With
        End Sub
#End Region


#Region " Properties "
        Public Property Id() As Int32 Implements IBOTaxon.Id
            Get
                Return mId
            End Get
            Set(ByVal Value As Int32)
                mId = Value
            End Set
        End Property
        Private mId As Int32

        Public Property KingdomID() As Integer Implements IBOTaxon.KingdomID
            Get
                Return mKingdomID
            End Get
            Set(ByVal Value As Integer)
                mKingdomID = Value
            End Set
        End Property
        Private mKingdomID As Int32

        Public Property TaxonID() As Integer Implements IBOTaxon.TaxonID
            Get
                Return mTaxonID
            End Get
            Set(ByVal Value As Integer)
                mTaxonID = Value
            End Set
        End Property
        Private mTaxonID As Int32

        Public Property TaxonTypeID() As Integer Implements IBOTaxon.TaxonTypeID
            Get
                Return mTaxonTypeID
            End Get
            Set(ByVal Value As Integer)
                mTaxonTypeID = Value
            End Set
        End Property
        Private mTaxonTypeID As Int32

        Public Property SpecieID() As Integer Implements IBOTaxon.SpecieID
            Get
                Return mSpecieID
            End Get
            Set(ByVal Value As Integer)
                mSpecieID = Value
            End Set
        End Property
        Private mSpecieID As Int32

        Public Property IsCoral() As Boolean Implements IBOTaxon.IsCoral
            Get
                Return mIsCoral
            End Get
            Set(ByVal Value As Boolean)
                mIsCoral = Value
            End Set
        End Property
        Private mIsCoral As Boolean

        Public Property PaymentKingdom() As PaymentKingdomEnum Implements IBOTaxon.PaymentKingdom
            Get
                Return mPaymentKingdom
            End Get
            Set(ByVal Value As PaymentKingdomEnum)
                mPaymentKingdom = Value
            End Set
        End Property
        Private mPaymentKingdom As PaymentKingdomEnum

        Public Property PaymentTaxonType() As PaymentTaxonTypeEnum Implements IBOTaxon.PaymentTaxonType
            Get
                Return mPaymentTaxonType
            End Get
            Set(ByVal Value As PaymentTaxonTypeEnum)
                mPaymentTaxonType = Value
            End Set
        End Property
        Private mPaymentTaxonType As PaymentTaxonTypeEnum

#End Region

#Region " Helper Functions "

#End Region

#Region " Save "

        Public Overridable Shadows Function Save(ByVal Id As Int32, ByVal tran As SqlClient.SqlTransaction) As BOTaxonIdentifier
            Dim NewBOTaxon As New DataObjects.Entity.Taxon
            Dim service As DataObjects.Service.TaxonService = NewBOTaxon.ServiceObject

            Dim ThisTaxon As BOTaxonIdentifier = MyClass.Save(tran:=tran)

            If ThisTaxon Is Nothing Then
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveTaxon)
                Return Me

            End If
            Return ThisTaxon
        End Function

        Public Overridable Shadows Function Save(ByVal tran As SqlClient.SqlTransaction) As BOTaxonIdentifier
            MyBase.Save()
            Validate()

            Dim NewTaxon As New DataObjects.Entity.Taxon
            Dim service As DataObjects.Service.TaxonService = NewTaxon.ServiceObject

            Created = (mId = 0)

            If Created Then
                NewTaxon = service.Insert(SpecieID, _
                                        mKingdomID, _
                                        mTaxonID, _
                                        mTaxonTypeID, _
                                        mIsCoral, _
                                        mPaymentKingdom, _
                                        mPaymentTaxonType, _
                                        tran)
            Else
                NewTaxon = service.Update(mId, _
                                        SpecieID, _
                                        mKingdomID, _
                                        mTaxonID, _
                                        mTaxonTypeID, _
                                        mIsCoral, _
                                        mPaymentKingdom, _
                                        mPaymentTaxonType, _
                                        CheckSum, _
                                        tran)
            End If
            'check to see if any SQL errors have occured
            If NewTaxon Is Nothing AndAlso Not DataObjects.Sprocs.LastError Is Nothing Then
                'TODO: Use errors collection to check to see if the problem was concurrency
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveTaxon)
            ElseIf NewTaxon Is Nothing Then
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveTaxon)
            Else
                If Created And Not NewTaxon Is Nothing Then
                    mTaxonID = NewTaxon.Id
                End If
                If NewTaxon.CheckSum <> CheckSum Then
                    'no point in initialising unless things have changed
                    InitialiseTaxon(NewTaxon, tran)
                End If
            End If

            Return Me
        End Function
#End Region

#Region " Validate "
        Public Function Validate() As ValidationManager Implements IBOTaxon.Validate
            ' init the errors list
            MyBase.ValidationErrors = New ValidationManager(ValidationManager.ValidationTitles.CannotSaveTaxon)

            GetValidationErrors()

            If Not MyBase.ValidationErrors.HasErrors Then
                MyBase.ValidationErrors = Nothing
            End If

            Return MyBase.ValidationErrors
        End Function

        Protected Overridable Sub GetValidationErrors()
        End Sub
#End Region


    End Class
End Namespace
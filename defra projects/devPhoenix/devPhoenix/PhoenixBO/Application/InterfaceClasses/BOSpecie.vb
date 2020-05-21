Namespace Application
    Public Class BOSpecie
        Inherits BaseBO
        Implements Application.IBOSpecie

#Region " Prelim code "
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal specieId As Int32, ByVal tran As SqlClient.SqlTransaction)
            MyClass.New()
            LoadSpecie(specieId, tran)
        End Sub

        'Private Function LoadSpecie(ByVal id As Int32) As DataObjects.Entity.Specie
        '    Return LoadSpecie(id, Nothing)
        'End Function

        Protected Overridable Function LoadSpecie(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.Specie
            Dim NewSpecie As DataObjects.Entity.Specie = DataObjects.Entity.Specie.GetById(id, tran)
            If NewSpecie Is Nothing Then
                Throw New RecordDoesNotExist("Specie", id)
            Else
                InitialiseSpecie(NewSpecie, tran)
                Return NewSpecie
            End If
        End Function

        Protected Overridable Sub InitialiseSpecie(ByVal specie As DataObjects.Entity.Specie, ByVal tran As SqlClient.SqlTransaction)
            With specie
                mSpecieId = .Id
                CheckSum = .CheckSum

                mCommonName = .CommonName
                mECAnnex = .ECAnnex
                mCITESAppendix = .CITESAppendix
                mScientificName = .ScientificName
                mDescription = .Description
                mAppliedForName = .AppliedForName
                mLineage = .Lineage
                mKeyedByApplicant = .KeyedByApplicant
                mPaymentKingdom = CType(.PaymentKingdom, PaymentKingdomEnum)
                mPaymentTaxonType = CType(.PaymentTaxonType, PaymentTaxonTypeEnum)
                mIsCoral = .IsCoral
            End With
        End Sub

        Public Overridable Sub InitialiseSpecie(ByVal specie As BOSpecie, ByVal tran As SqlClient.SqlTransaction)
            With specie
                mSpecieId = .SpecieId
                CheckSum = .CheckSum

                mCommonName = .CommonName
                mECAnnex = .ECAnnex
                mCITESAppendix = .CITESAppendix
                mScientificName = .ScientificName
                mDescription = .Description
                mAppliedForName = .AppliedForName
                mLineage = .Lineage
                mPaymentKingdom = CType(.PaymentKingdom, PaymentKingdomEnum)
                mPaymentTaxonType = CType(.PaymentTaxonType, PaymentTaxonTypeEnum)
                mIsCoral = .IsCoral
            End With
        End Sub
#End Region


#Region " Properties "
        Public Property SpecieId() As Int32 Implements IBOSpecie.SpecieId
            Get
                Return mSpecieId
            End Get
            Set(ByVal Value As Int32)
                mSpecieId = Value
            End Set
        End Property
        Private mSpecieId As Int32

        Public Property CommonName() As String Implements IBOSpecie.CommonName
            Get
                Return mCommonName
            End Get
            Set(ByVal Value As String)
                mCommonName = Value
            End Set
        End Property
        Private mCommonName As String

        Public Property ECAnnex() As String Implements IBOSpecie.ECAnnex
            Get
                Return mECAnnex
            End Get
            Set(ByVal Value As String)
                mECAnnex = Value
            End Set
        End Property
        <Microsoft.VisualBasic.VBFixedStringAttribute(1)> _
        Private mECAnnex As String

        Public Property CITESAppendix() As String Implements IBOSpecie.CITESAppendix
            Get
                Return mCITESAppendix
            End Get
            Set(ByVal Value As String)
                mCITESAppendix = Value
            End Set
        End Property
        Private mCITESAppendix As String

        Public Property ScientificName() As String Implements IBOSpecie.ScientificName
            Get
                Return mScientificName
            End Get
            Set(ByVal Value As String)
                mScientificName = Value
            End Set
        End Property
        Private mScientificName As String

        Public Property Description() As String Implements IBOSpecie.Description
            Get
                Return mDescription
            End Get
            Set(ByVal Value As String)
                mDescription = Value
            End Set
        End Property
        Private mDescription As String

        Public Property AppliedForName() As String Implements IBOSpecie.AppliedForName
            Get
                Return mAppliedForName
            End Get
            Set(ByVal Value As String)
                mAppliedForName = Value
            End Set
        End Property
        Private mAppliedForName As String

        Public Property Lineage() As String Implements IBOSpecie.Lineage
            Get
                Return mLineage
            End Get
            Set(ByVal Value As String)
                mLineage = Value
            End Set
        End Property
        Private mLineage As String

        Public Property CommonNameID() As Int32 Implements IBOSpecie.CommonNameID
            Get
                Return mCommonNameID
            End Get
            Set(ByVal Value As Int32)
                mCommonNameID = Value
            End Set
        End Property
        Private mCommonNameID As Int32

        Public Property KeyedByApplicant() As Boolean Implements IBOSpecie.KeyedByApplicant
            Get
                Return mKeyedByApplicant
            End Get
            Set(ByVal Value As Boolean)
                mKeyedByApplicant = Value
            End Set
        End Property
        Private mKeyedByApplicant As Boolean

        Public Property Hybrid() As Boolean Implements IBOSpecie.Hybrid
            Get
                Return mHybrid
            End Get
            Set(ByVal Value As Boolean)
                mHybrid = Value
            End Set
        End Property
        Private mHybrid As Boolean

        Public Property Source() As Int32 Implements IBOSpecie.Source
            Get
                Return mSource
            End Get
            Set(ByVal Value As Int32)
                mSource = Value
            End Set
        End Property
        Private mSource As Int32

        Public Property Taxa() As BOTaxonIdentifier() Implements IBOSpecie.Taxa
            Get
                Return mTaxa
            End Get
            Set(ByVal Value() As BOTaxonIdentifier)
                mTaxa = Value
            End Set
        End Property
        Private mTaxa() As BOTaxonIdentifier

        Public Property IsCoral() As Boolean Implements IBOSpecie.IsCoral
            Get
                Return mIsCoral
            End Get
            Set(ByVal Value As Boolean)
                mIsCoral = Value
            End Set
        End Property
        Private mIsCoral As Boolean

        Public Property PaymentKingdom() As PaymentKingdomEnum Implements IBOSpecie.PaymentKingdom
            Get
                Return mPaymentKingdom
            End Get
            Set(ByVal Value As PaymentKingdomEnum)
                mPaymentKingdom = Value
            End Set
        End Property
        Private mPaymentKingdom As PaymentKingdomEnum

        Public Property PaymentTaxonType() As PaymentTaxonTypeEnum Implements IBOSpecie.PaymentTaxonType
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

        Public ReadOnly Property SourceHelper() As Object
            Get
                If Me.mSource = 0 Then
                    Return Nothing
                Else
                    Return Me.mSource
                End If
            End Get
        End Property

        Public ReadOnly Property CommonNameIdHelper() As Object
            Get
                If Me.mCommonNameID = 0 Then
                    Return Nothing
                Else
                    Return Me.mCommonNameID
                End If
            End Get
        End Property

        Public ReadOnly Property IsAnnexA() As Boolean
            Get
                Return (String.Compare(ECAnnex, "A", True) = 0)
            End Get
        End Property

        Public ReadOnly Property IsAnnexB() As Boolean
            Get
                Return (String.Compare(ECAnnex, "B", True) = 0)
            End Get
        End Property

        Public ReadOnly Property IsAnnexC() As Boolean
            Get
                Return (String.Compare(ECAnnex, "C", True) = 0)
            End Get
        End Property

        Public ReadOnly Property IsAnnexD() As Boolean
            Get
                Return (String.Compare(ECAnnex, "D", True) = 0)
            End Get
        End Property

        Public ReadOnly Property IsAppendixI() As Boolean
            Get
                Return (String.Compare(CITESAppendix, "I", True) = 0)
            End Get
        End Property

        Public ReadOnly Property IsAppendixIII() As Boolean
            Get
                Return (String.Compare(CITESAppendix, "III", True) = 0)
            End Get
        End Property

        Public ReadOnly Property IsListedOnAppendix() As Boolean
            Get
                Return (Not CITESAppendix Is Nothing AndAlso _
                        CITESAppendix.Length > 0)
            End Get
        End Property

        Public ReadOnly Property ReportAppliedForName() As String
            Get
                If mAppliedForName Is Nothing Then
                    Return String.Empty
                Else
                    Return mAppliedForName.TrimEnd
                End If
            End Get
        End Property

        Public Property ReportDescription() As String
            Get
                If mDescription Is Nothing Then
                    Return String.Empty
                Else
                    Return mDescription.TrimEnd
                End If
            End Get
            Set(ByVal Value As String)

            End Set
        End Property
#End Region

#Region " Save "

      

        Public Overridable Shadows Function Save(ByVal tran As SqlClient.SqlTransaction) As BOSpecie
            MyBase.Save()
            Validate()

            Dim NewSpecie As New DataObjects.Entity.Specie
            Dim service As DataObjects.Service.SpecieService = NewSpecie.ServiceObject

            If Not TypeOf Me Is BO.Application.CITES.BONotificationSpecie Then Created = (mSpecieId = 0)

            '  Me.KeyedByApplicant = (mTaxa Is Nothing OrElse mTaxa.Length = 0)

            If mSpecieId = 0 Then
                NewSpecie = service.Insert(mCommonName, _
                                           mECAnnex, _
                                           mCITESAppendix, _
                                           mScientificName, _
                                           mDescription, _
                                           mAppliedForName, _
                                           mLineage, _
                                           mKeyedByApplicant, _
                                           mHybrid, _
                                           SourceHelper, _
                                           CommonNameIdHelper, _
                                           mIsCoral, _
                                           mPaymentKingdom, _
                                           mPaymentTaxonType, _
                                           tran)
            Else
                NewSpecie = service.Update(mSpecieId, _
                                           mCommonName, _
                                           mECAnnex, _
                                           mCITESAppendix, _
                                           mScientificName, _
                                           mDescription, _
                                           mAppliedForName, _
                                           mLineage, _
                                           mKeyedByApplicant, _
                                           mHybrid, _
                                           SourceHelper, _
                                           Me.CommonNameIdHelper, _
                                           mIsCoral, _
                                           mPaymentKingdom, _
                                           mPaymentTaxonType, _
                                           CheckSum, _
                                           tran)
            End If
            'Save all of the associated taxon objects.
            If Not NewSpecie Is Nothing AndAlso Not mTaxa Is Nothing Then
                For Each Taxon As BOTaxonIdentifier In mTaxa
                    Taxon.SpecieID = NewSpecie.SpecieId
                    Taxon.Save(tran)
                Next
            End If

            'check to see if any SQL errors have occured
            If NewSpecie Is Nothing AndAlso Not DataObjects.Sprocs.LastError Is Nothing Then
                'TODO: Use errors collection to check to see if the problem was concurrency
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveSpecie)
            ElseIf NewSpecie Is Nothing Then
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveSpecie)
            Else
                If Created And Not NewSpecie Is Nothing Then
                    mSpecieId = NewSpecie.Id
                End If
                If NewSpecie.CheckSum <> CheckSum Then
                    'no point in initialising unless things have changed
                    InitialiseSpecie(NewSpecie, tran)
                End If
            End If

            Return Me
        End Function
#End Region

#Region " Validate "
        Public Function Validate() As ValidationManager Implements IBOSpecie.Validate
            ' init the errors list
            MyBase.ValidationErrors = New ValidationManager(ValidationManager.ValidationTitles.CannotSaveImportSpecie)

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
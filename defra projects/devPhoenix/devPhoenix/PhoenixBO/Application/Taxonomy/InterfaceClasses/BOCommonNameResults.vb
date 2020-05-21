Namespace Taxonomy
    Public Class BOCommonNameResults
        Inherits BOCommonName
        Implements ICommonNameResults

#Region " Prelim code "
        Public Sub New()
            'This constructor is intentionally empty.
        End Sub

        Public Sub New(ByVal Source As Int32, ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction)
            LoadCommonName(Source, id, tran)
        End Sub

        Public Sub New(ByVal Source As Int32, ByVal id As Int32)
            LoadCommonName(Source, id, Nothing)
        End Sub

        Private Overloads Function LoadCommonName(ByVal Source As Int32, ByVal id As Int32) As DataObjects.Entity.TaxonomyCommonName
            Return Me.LoadCommonName(Source, id, Nothing)
        End Function

        Private Overloads Function LoadCommonName(ByVal Source As Int32, ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.TaxonomyCommonName
            Dim NewCommonName As DataObjects.Entity.TaxonomyCommonName = DataObjects.Entity.TaxonomyCommonName.GetById(Source, id, tran)
            If NewCommonName Is Nothing Then
                Throw New RecordDoesNotExist("Common Name", id)
            Else
                InitialiseMe(NewCommonName, tran)
                Return NewCommonName
            End If
        End Function

        Protected Overrides Sub InitialiseMe(ByVal CommonName As DataObjects.Entity.TaxonomyCommonName, ByVal tran As SqlClient.SqlTransaction)
            MyBase.InitialiseMe(CommonName, tran)
            With CommonName
                Dim Taxon As New BOTaxon(Me.KingdomID, Me.TaxonId, Me.TaxonTypeID, tran)
                Me.TaxonScientificName = Taxon.LongScientificNameHTMLFormatted
                Me.TaxonTypeDescription = Taxon.TaxonTypeDescription
                Me.TaxonTypeDisplayDescription = Taxon.DisplayTaxonTypeDescription
                Me.Taxon = Taxon
                If Not Validated Is Nothing AndAlso Not CType(Validated, Boolean) Then Validated = False
            End With
        End Sub

#End Region

#Region " Properties "

        Public Property TaxonTypeDisplayDescription() As String Implements ICommonNameResults.TaxonTypeDisplayDescription
            Get
                Return mTaxonTypeDisplayDescription
            End Get
            Set(ByVal Value As String)
                mTaxonTypeDisplayDescription = Value
            End Set
        End Property
        Private mTaxonTypeDisplayDescription As String

        Public Property TaxonTypeDescription() As String Implements ICommonNameResults.TaxonTypeDescription
            Get
                Return mTaxonTypeDescription
            End Get
            Set(ByVal Value As String)
                mTaxonTypeDescription = Value
            End Set
        End Property
        Private mTaxonTypeDescription As String

        Public Property TaxonScientificName() As String Implements ICommonNameResults.TaxonScientificName
            Get
                Return mTaxonScientificName
            End Get
            Set(ByVal Value As String)
                mTaxonScientificName = Value
            End Set
        End Property
        Private mTaxonScientificName As String

#End Region

    End Class
End Namespace
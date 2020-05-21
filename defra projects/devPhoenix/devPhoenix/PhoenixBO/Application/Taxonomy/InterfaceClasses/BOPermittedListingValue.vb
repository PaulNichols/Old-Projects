Imports uk.gov.defra.Phoenix.DO.DataObjects
Imports uk.gov.defra.Phoenix.BO.Payments

Namespace Taxonomy
    <Serializable()> _
    Public Class BOPermittedListingValue
        Inherits PaymentsBaseBO
        Implements IPermittedListingValue

#Region " Prelim code "
        Public Sub New()
            'This constructor is intentionally empty.
        End Sub

        Public Sub New(ByVal id As Int32)
            Dim service As New Service.TaxonomyPermittedListingValueService
            Dim result As Entity.TaxonomyPermittedListingValue = service.GetById(id)
            If result Is Nothing Then
                Throw New RecordDoesNotExist("PermittedListingValue", id)
            Else
                InitialiseValue(result)
            End If
        End Sub

        Private Sub InitialiseValue(ByVal val As Entity.TaxonomyPermittedListingValue)
            With val
                mPermittedListingId = .PermittedListingId
                mLegislationNameId  = .LegislationNameId
                mListingValue       = .ListingValue
                mListingOrder       = .ListingOrder
                mCheckSum           = .CheckSum
            End With
        End Sub

        Public Shared Function GetByLegislationId(ByVal legislationId As Int32, ByVal tran As SqlClient.SqlTransaction) As BOPermittedListingValue()
            Dim items As EntitySet.TaxonomyPermittedListingValueSet = Entity.TaxonomyPermittedListingValue.GetForTaxonomyLegislationName(legislationId, tran)
            Dim results(-1) As BOPermittedListingValue
            Dim index As Int32 = 0
            If Not items Is Nothing Then
                ReDim results(items.Count - 1)
                For Each item As Entity.TaxonomyPermittedListingValue In items
                    results(index) = New BOPermittedListingValue
                    results(index).InitialiseValue(item)
                    index += 1
                Next
            End If
            Return results
        End Function

#End Region

#Region " Properties "
        Public Property PermittedListingId As Int32 Implements IPermittedListingValue.PermittedListingId
            Get
                Return mPermittedListingId
            End Get
            Set
                mPermittedListingId = Value
            End Set
        End Property
        Private mPermittedListingId As Int32

        Public Property LegislationNameId As Int32 Implements IPermittedListingValue.LegislationNameId
            Get
                Return mLegislationNameId
            End Get
            Set
                mLegislationNameId = Value
            End Set
        End Property
        Private mLegislationNameId As Int32

        Public Property ListingValue As String Implements IPermittedListingValue.ListingValue
            Get
                Return mListingValue
            End Get
            Set
                mListingValue = Value
            End Set
        End Property
        Private mListingValue As String

        Public Property ListingOrder As String Implements IPermittedListingValue.ListingOrder
            Get
                Return mListingOrder
            End Get
            Set
                mListingOrder = Value
            End Set
        End Property
        Private mListingOrder As String

#End Region

#Region " Save "
        Public Overrides Overloads Function Save() As BaseBO
            Dim service As New Service.TaxonomyPermittedListingValueService
            Dim tran As SqlClient.SqlTransaction = service.BeginTransaction
            Dim result As BaseBO = MyClass.Save(tran)

            If result Is Nothing Then
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
            Else
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Commit)
            End If
            Return result
        End Function

        Public Shadows Function Delete() As Boolean
            Dim service As New Service.TaxonomyPermittedListingValueService
            Return service.DeleteById(mPermittedListingId, Checksum)
        End Function

        Public Overloads Function Save(ByVal tran As SqlClient.SqlTransaction) As BaseBO
            Dim detail As New Entity.TaxonomyPermittedListingValue
            Dim service As service.TaxonomyPermittedListingValueService = detail.ServiceObject
            Dim created As Boolean = mPermittedListingId = 0

            If created Then
                detail = service.Insert(mLegislationNameId, _
                                      mListingValue, _
                                      tran)
            Else
                detail = service.Update(mPermittedListingId, _
                                      mLegislationNameId, _
                                      mListingValue, _
                                      mChecksum, _
                                      tran)
            End If
            If detail Is Nothing Then
                CheckSqlErrors("Animal Licensing Detail", tran, service)
            Else
                If created And Not detail Is Nothing Then
                    mPermittedListingId = detail.Id
                End If
                If detail.CheckSum <> CheckSum Then
                     InitialiseValue(detail)
                End If
            End If
            Return Me
        End Function

#End Region


    End Class
End Namespace

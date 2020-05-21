Imports uk.gov.defra.Phoenix.DO.DataObjects
Imports uk.gov.defra.Phoenix.BO.Payments

Namespace Taxonomy
    <Serializable()> _
    Public Class BOAnimalDelegationAuthority
        Inherits PaymentsBaseBO
        Implements IAnimalDelegationAuthority

#Region " Prelim code "
        Public Sub New()
            'This constructor is intentionally empty.
        End Sub

        Public Sub New(ByVal id As Int32)
            Dim service As New Service.AnimalDelegationAuthorityService
            Dim result As Entity.AnimalDelegationAuthority = service.GetById(id)
            If result Is Nothing Then
                Throw New RecordDoesNotExist("AnimalDelegationAuthority", id)
            Else
                InitialiseAuthority(result)
            End If
        End Sub

        Private Sub InitialiseAuthority(ByVal authority As Entity.AnimalDelegationAuthority)
            With authority
                CheckSum                     = .CheckSum
                mAnimalDelegationAuthorityID = .AnimalDelegationAuthorityID
                mDelegationCode              = .DelegationCode
                mApplicationTypeID           = .ApplicationTypeID
                mSpeciesKingdomID            = .SpeciesKingdomID
                mSpeciesTaxonomyID           = .SpeciesTaxonomyID
                mSpeciesTaxonTypeID          = .SpeciesTaxonTypeID
                mHyperlinkRTARoadmap         = .HyperlinkRTARoadMap
            End With
        End Sub

#End Region

#Region " Properties "
        Public Property AnimalDelegationAuthorityID As Int32 Implements IAnimalDelegationAuthority.AnimalDelegationAuthorityID
            Get
                Return mAnimalDelegationAuthorityID
            End Get
            Set
                mAnimalDelegationAuthorityID = Value
            End Set
        End Property
        Private mAnimalDelegationAuthorityID As Int32

        Public Property DelegationCode As Int32 Implements IAnimalDelegationAuthority.DelegationCode
            Get
                Return mDelegationCode
            End Get
            Set
                mDelegationCode = Value
            End Set
        End Property
        Private mDelegationCode As Int32

        Public Property ApplicationTypeID As Int32 Implements IAnimalDelegationAuthority.ApplicationTypeID
            Get
                Return mApplicationTypeID
            End Get
            Set
                mApplicationTypeID = Value
            End Set
        End Property
        Private mApplicationTypeID As Int32

        Public Property SpeciesKingdomID As Int32 Implements IAnimalDelegationAuthority.SpeciesKingdomID
            Get
                Return mSpeciesKingdomID
            End Get
            Set
                mSpeciesKingdomID = Value
            End Set
        End Property
        Private mSpeciesKingdomID As Int32

        Public Property SpeciesTaxonomyID As Int32 Implements IAnimalDelegationAuthority.SpeciesTaxonomyID
            Get
                Return mSpeciesTaxonomyID
            End Get
            Set
                mSpeciesTaxonomyID = Value
            End Set
        End Property
        Private mSpeciesTaxonomyID As Int32

        Public Property SpeciesTaxonTypeID As Int32 Implements IAnimalDelegationAuthority.SpeciesTaxonTypeID
            Get
                Return mSpeciesTaxonTypeID
            End Get
            Set
                mSpeciesTaxonTypeID = Value
            End Set
        End Property
        Private mSpeciesTaxonTypeID As Int32

        Public Property HyperlinkRTARoadmap As String Implements IAnimalDelegationAuthority.HyperlinkRTARoadmap
            Get
                Return mHyperlinkRTARoadmap
            End Get
            Set
                mHyperlinkRTARoadmap = Value
            End Set
        End Property
        Private mHyperlinkRTARoadmap As String
#End Region

#Region " Save "
        Public Overrides Overloads Function Save() As BaseBO
            Dim service As New Service.AnimalDelegationAuthorityService
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
            Dim service As New Service.AnimalDelegationAuthorityService
            Return service.DeleteById(mAnimalDelegationAuthorityID, Checksum)
        End Function

        Public Overloads Function Save(ByVal tran As SqlClient.SqlTransaction) As BaseBO
            Dim authority As New Entity.AnimalDelegationAuthority
            Dim service As service.AnimalDelegationAuthorityService = authority.ServiceObject
            Dim created As Boolean = mAnimalDelegationAuthorityID = 0

            If created Then
                authority = service.Insert(mDelegationCode, _
                                      mApplicationTypeID, _
                                      mSpeciesKingdomID, _
                                      mSpeciesTaxonomyID, _
                                      mSpeciesTaxonTypeID, _
                                      mHyperlinkRTARoadmap, _
                                      tran)
            Else
                authority = service.Update(mAnimalDelegationAuthorityID, _
                                      mDelegationCode, _
                                      mApplicationTypeID, _
                                      mSpeciesKingdomID, _
                                      mSpeciesTaxonomyID, _
                                      mSpeciesTaxonTypeID, _
                                      mHyperlinkRTARoadmap, _
                                      tran)
            End If
            If authority Is Nothing Then
                CheckSqlErrors("Animal Delegation Authority", tran, service)
            Else
                If created And Not authority Is Nothing Then
                    mAnimalDelegationAuthorityID = authority.Id
                End If
                If authority.CheckSum <> CheckSum Then
                     InitialiseAuthority(authority)
                End If
            End If
            Return Me
        End Function

#End Region


    End Class
End Namespace
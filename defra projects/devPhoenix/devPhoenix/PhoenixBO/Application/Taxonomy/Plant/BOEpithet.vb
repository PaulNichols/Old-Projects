Imports uk.gov.defra.Phoenix.DO.DataObjects

Namespace Taxonomy.Plant
    <Serializable()> _
    Public Class BOEpithet
        Inherits BOSpecies

#Region " Prelim code "
        Public Sub New()
            'This constructor is intentionally empty.
        End Sub

        Sub New(ByVal taxon As Taxonomy.BOTaxon)
            MyBase.New(taxon)
        End Sub

#End Region

#Region " Properties "
#End Region

#Region " Save "
        Public Overrides Overloads Sub Save(ByVal tran As SqlClient.SqlTransaction)
            MyBase.Save(tran, TaxonTypeEnum.Epithet)
        End Sub

        Public Overrides Function CreateChild() As BOTaxonBase
            Throw New Exception("Cannot create child of Epithet")
        End Function

#End Region


    End Class
End Namespace
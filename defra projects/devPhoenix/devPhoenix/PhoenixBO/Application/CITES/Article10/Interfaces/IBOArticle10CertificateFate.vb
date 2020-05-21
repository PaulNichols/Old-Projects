Namespace Application.CITES.Applications
    Public Interface IBOArticle10CertificateFate

        Property Article10CertificateFateId() As Int32
        Property QtyUsed() As Object
        Property Fate() As ReferenceData.BOArticle10Fate
        Property ReturnedToDefra() As Boolean
        Property PermitId() As Int32
        Property SpecimenSoldTo() As Object
    End Interface
End Namespace
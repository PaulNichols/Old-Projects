Namespace Application.CITES.Applications
    Public Interface IBOCITESArticle30
        Inherits IBox18

        Property Article30Id() As Int32
        Property Article30Type() As BO.ReferenceData.BOArticle10CertificateType
        Property Article30CheckSum() As Int32
        Property IsTransactionSpecific() As Object
        Property Holder() As BO.Application.BOApplicationPartyDetails
    End Interface
End Namespace

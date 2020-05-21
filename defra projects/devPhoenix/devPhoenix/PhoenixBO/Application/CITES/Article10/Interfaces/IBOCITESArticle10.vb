Namespace Application.CITES.Applications
    Public Interface IBOCITESArticle10
        Inherits IBox18

        Property Article10Id() As Int32
        Property Article10Type() As BO.ReferenceData.BOArticle10CertificateType
        Property Article10TypeString() As String
        Property Article10CheckSum() As Int32
        Property AquisitionDetails() As String
        Property Holder() As BO.Application.BOApplicationPartyDetails
    End Interface

    Public Interface IBox18
        Property Box18_1() As Boolean
        Property Box18_2() As Boolean
        Property Box18_3() As Boolean
        Property Box18_4() As Boolean
        Property Box18_5() As Boolean
        Property Box18_6() As Boolean
        Property Box18_7() As Boolean
        Property Box18_8() As Boolean
    End Interface
End Namespace

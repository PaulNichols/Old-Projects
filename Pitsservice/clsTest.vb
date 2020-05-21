Public Class authHeader
    Inherits System.Web.Services.Protocols.SoapHeader
    Public authToken As String
End Class

<System.Web.Services.WebService(Namespace:="http://www.Accuride.com/")> _
Public Class ISDWebService
    Inherits System.Web.Services.WebService

    Public authHdr As authHeader

    Public Function IsRequestOK() As Boolean
        Return authHdr.authToken = "Steven"
    End Function
End Class


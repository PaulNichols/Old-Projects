Option Strict On
Option Explicit On 

Namespace DataObjects
    Public Class TaxonomyDataRequester

        Public ReadOnly Property LastError() As Exception
            Get
                Return mLastError
            End Get
        End Property
        Private mLastError As Exception

        Public Function DeliverRequestToSupplier(ByVal Uri As String) As Boolean
            Try
                mLastError = Nothing
                'Attempt to create a Uri from the supplied string.
                Dim siteUri As New System.Uri(Uri)
                'Attempt to use the Uri.
                Dim webRequest As System.Net.HttpWebRequest = CType(System.Net.HttpWebRequest.Create(siteUri), System.Net.HttpWebRequest)
                'webRequest.Proxy = New System.Net.WebProxy("http://proxyserver:port/")
                Dim webResponse As System.Net.HttpWebResponse = CType(webRequest.GetResponse(), System.Net.HttpWebResponse)
                webResponse.Close()
                Return True
            Catch ex As Exception
                mLastError = ex
                Return False
            End Try
        End Function
    End Class
End Namespace
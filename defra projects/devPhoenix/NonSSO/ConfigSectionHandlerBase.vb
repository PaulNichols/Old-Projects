Namespace Configuration
    Public Class ConfigSectionHandlerBase

        Protected Function ExtractValue(ByVal node As Xml.XmlNode, ByVal attr As String, Optional ByVal def As Object = "") As String
            Try
                Return node.Attributes(attr).Value
            Catch ex As Exception
                Return Convert.ToString(def)
            End Try
        End Function

    End Class
End Namespace


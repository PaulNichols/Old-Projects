Public Class DataTypeConversion
    Shared Function GetDotNetTypeFromXML(ByVal xmlType As String) As Type
        If xmlType.StartsWith("s:") Then
            xmlType = xmlType.Substring(2)
        End If
        Select Case xmlType.ToLower
            Case "anyuri", "entity", "entities", "gday", _
                 "gmonth", "gmonthday", "gyear", _
                 "gyearmonth", "id", "idref", _
                 "idrefs", "integer", "language", _
                 "name", "ncname", "negativeinteger", _
                 "nmtoken", "nmtokens", "normalizedstring", _
                 "nonnegativeinteger", "nonpositiveinteger", _
                 "notation", "positiveinteger", "duration", _
                 "string", "token"
                Return GetType(String)
            Case "base64binary", "hexbinary"
                Return GetType(Byte())
            Case "boolean"
                Return GetType(Boolean)
            Case "byte"
                Return GetType(SByte)
            Case "date", "datetime", "time"
                Return GetType(DateTime)
            Case "decimal"
                Return GetType(Decimal)
            Case "double"
                Return GetType(Double)
            Case "float"
                Return GetType(Single)
            Case "float"
                Return GetType(Single)
            Case "short"
                Return GetType(Int16)
            Case "int"
                Return GetType(Int32)
            Case "long"
                Return GetType(Int64)
            Case "qname"
                Return GetType(Xml.XmlQualifiedName)
            Case "unsignedbyte"
                Return GetType(Byte)
            Case "unsignedint"
                Return GetType(UInt32)
            Case "unsignedlong"
                Return GetType(UInt64)
            Case "unsignedshort"
                Return GetType(UInt16)
            Case "color"
                Return GetType(System.Drawing.Color)
            Case Else
                Throw New TypeLoadException
        End Select
    End Function
End Class

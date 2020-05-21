Public Class Common
    Public Shared Function ParseSQLText(ByVal sql As String) As String
        Return ParseSQLText(sql, False, False)
    End Function

    Public Shared Function ParseSQLText(ByVal sql As String, ByVal rectifyWildcards As Boolean, ByVal rectifyQuotes As Boolean) As String
        If rectifyQuotes Then
            If sql.IndexOf("'") <> -1 Then sql = sql.Replace("'", "''")
            If sql.IndexOf("""") <> -1 Then sql = sql.Replace("""", """""")
        End If
        If rectifyWildcards Then
            If sql.IndexOf("%") <> -1 Then sql = sql.Replace("%", "[%]")
            If sql.IndexOf("*") <> -1 Then sql = sql.Replace("*", "%")
            If sql.IndexOf("_") <> -1 Then sql = sql.Replace("_", "[_]")
            If sql.IndexOf("?") <> -1 Then sql = sql.Replace("?", "_")
        End If
        Return sql
    End Function

    Public Shared Function ParseSQLText(ByVal sql As String, ByVal rectifyWildcards As Boolean) As String
        Return ParseSQLText(sql, rectifyWildcards, False)
    End Function
End Class

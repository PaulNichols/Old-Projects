Attribute VB_Name = "modmain"
Option Explicit
Public con As New Connection
Public DBPath As String
Public bDontPrintLabels As Boolean
Public Sub Main()


End Sub

Public Sub OpenConnection(oCnn As Connection, Optional sDBPath)
Dim sDBType As String

    If IsMissing(sDBPath) Then sDBPath = ""
'    If sDBPath = "" Then sDBPath = ReadRegEntry("dbPath", , "\\green\c\greener world\new isd version\greenerworld_be.mdb")
'    If Dir(sDBPath) = "" Then
'        sDBPath = Trim(APIGetOpenFileName("Choose Greenerworld DB...", CurDir & "\", "Microsoft Access Database {*.mdb}" & Chr(0) & "*.mdb" & Chr(0), 0, "", 200, 0, 0))
'    End If
'    CreateRegEntry "dbPath", sDBPath
    Set oCnn = New Connection
    oCnn.Provider = "Microsoft.Jet.OLEDB.4.0"
    oCnn.Open sDBPath
    
    If Left(oCnn.Provider, 19) = "Microsoft.Jet.OLEDB" Then
        sDBType = "Access"
    End If
End Sub



Attribute VB_Name = "modmain"
Option Explicit
Public objFolder As MAPIFolder
Public gOLNameSpace As NameSpace
Public gOLApp As Outlook.Application
Public Enum EncryptDecrypt
    ENCODE = 1
    DECODE = 2
End Enum
Const msScramble = "soDSMDHnsQ" 'This is used for encryption
Public sDb As String, sDbname As String
Global Const DEFAULT_DATE = "01/01/1800"
Public oCon As Connection, ssql As String

Public Function ConvertString(aString As String, Optional AddPercent) As String

Dim sadd As String

    aString = Stuff(aString, Chr$(34), Chr$(34) & Chr$(34))
    aString = Stuff(aString, Chr$(39), Chr$(39) & Chr$(39))
    sadd = IIf(IsMissing(AddPercent), "", "%")
    ConvertString = "'" & aString & sadd & "'"
End Function

Public Function Stuff(sPwdString As String, asSearch As String, asReplace) As String
Dim i As Integer

    i = InStr(sPwdString, asSearch)
    Do Until i = 0
        sPwdString = Left$(sPwdString, i - 1) + asReplace + Mid$(sPwdString, i + Len(asSearch))
        i = InStr(i + Len(asReplace), sPwdString, asSearch)
    Loop
    Stuff = sPwdString
End Function

Public Function Encrypt(sPwdString As String, abEncryptType As EncryptDecrypt) As String
Dim nCount As Integer, nCode1 As Integer, nCode2 As Integer
Dim sScrambled As String, nStringLength As Integer

    If sPwdString <> "" Then
        nStringLength = Len(msScramble)

        Select Case abEncryptType
            Case ENCODE
                For nCount = 1 To Len(sPwdString)
                    nCode1 = Asc(Mid$(sPwdString, nCount, 1))
                    nCode2 = Asc(Mid$(msScramble, (nCount Mod nStringLength) + 1, 1))
                    nCode1 = nCode1 Xor nCode2
                    sScrambled = sScrambled + Chr$(Asc("z") - (nCode1 \ 16)) + Chr$(Asc("a") + (nCode1 Mod 16))
                Next nCount
            Case DECODE
                For nCount = 1 To Len(sPwdString) Step 2
                    nCode1 = (Asc("z") - Asc(Mid$(sPwdString, nCount, 1))) * 16 + (Asc(Mid$(sPwdString, nCount + 1, 1)) - Asc("a"))
                    nCode2 = Asc(Mid$(msScramble, (((nCount + 1) / 2) Mod nStringLength) + 1, 1))
                    nCode1 = nCode1 Xor nCode2
                    sScrambled = sScrambled + Chr$(nCode1)
                Next nCount
        End Select

        Encrypt = sScrambled
    End If
End Function
Private Sub Connectme(objConnection As ADODB.Connection, sDBLocation As String)
    objConnection.Provider = "Microsoft.Jet.OLEDB.4.0"
    objConnection.Open sDBLocation
End Sub

Public Function executesql(ssql As String, Optional rs) As Boolean
Dim RowsAffected As Long

  If oCon.State = adStateClosed Then
        Connectme oCon, sDb & "\" & sDbname
    End If
     If IsMissing(rs) Then
        oCon.Execute ssql, RowsAffected
        executesql = (RowsAffected > 0)
    Else
        Set rs = oCon.Execute(ssql, RowsAffected)
        If (rs.EOF And rs.BOF) Then
            rs.Close
            executesql = False
        Else
            executesql = True
        End If
    End If
    'oCon.Close
End Function

Public Function NullToEmpty(avValue, Optional avDefault) As Variant

    If IsMissing(avDefault) Then
        Select Case VarType(avValue)
            Case vbString: avDefault = ""
            Case vbBoolean: avDefault = False
            Case vbDate: avDefault = DEFAULT_DATE
            Case Else: avDefault = 0
        End Select
    End If

    If IsNull(avValue) Then
        NullToEmpty = avDefault
    Else
        If VarType(avValue) = vbDate Then
            NullToEmpty = IIf(CDate("00:00:00") = avValue, avDefault, avValue)
        Else
            NullToEmpty = avValue
        End If
    End If
End Function

Public Function AddContacts(sDBLocation As String, ContactsCol) As Boolean
Dim sNTlogon As String
Dim obj As Object, nRowsAffected As Long, con As Connection, nNewid As Long

    Set oCon = New Connection
 Connectme oCon, sDBLocation
    executesql "delete * from tcontacts"
    
    oCon.BeginTrans
 For Each obj In ContactsCol.Items
        With obj
            On Error Resume Next
            sNTlogon = ""
            sNTlogon = .User1
            If Err.Number <> 91 And Err.Number <> 0 Then GoTo errcode
            On Error GoTo 0
                nNewid = NextId("tcontacts")
                On Error Resume Next
                Dim s As String
                s = .UserProperties("Salutation")
                

                 ssql = "update tcontacts set otherfaxnumber=" & IIf(Len(Trim(.OtherFaxNumber)) = 0, 0, .OtherFaxNumber) & ",fullname=" & ConvertString(.FullName) & ",birthday=" & ConvertString(.Birthday) & _
                    ",Business2TelephoneNumber=" & ConvertString(.Business2TelephoneNumber) & ",BusinessAddress=" & ConvertString(.BusinessAddress) & _
                    ",BusinessFaxNumber=" & ConvertString(.BusinessFaxNumber) & ",BusinessHomePage=" & ConvertString(.BusinessHomePage) & _
                    ",Homeaddress=" & ConvertString(.HomeAddress) & ",suffix=" & ConvertString(.Suffix) & _
                    ",title=" & ConvertString(.Title) & ",HomeTelephoneNumber=" & ConvertString(.HomeTelephoneNumber) & _
                    ",HomeFaxNumber=" & ConvertString(.HomeFaxNumber) & _
                    ",fileas=" & ConvertString(.FileAs) & ",companyname=" & ConvertString(.CompanyName) & ",email1address=" & ConvertString(.Email1Address) & _
                    ",Firstname=" & ConvertString(.FirstName) & ",LastName=" & ConvertString(.LastName) & _
                    ",mobiletelephonenumber=" & ConvertString(.MobileTelephoneNumber) & _
                    ",BusinessTelephoneNumber=" & ConvertString(.BusinessTelephoneNumber) & ",ntlogon=" & ConvertString(sNTlogon) & ",Salutation=" & ConvertString(s) & _
                    ",OtherTelephoneNumber=" & ConvertString(.OtherTelephoneNumber) & ",entry_id=" & ConvertString(.EntryID) & " where id=" & nNewid
                Err.Clear
                On Error GoTo 0
                oCon.Execute ssql, nRowsAffected
                If nRowsAffected = 0 Then Exit Function
'                If Not executesql(ssql) Then Exit Function
        End With
    Next obj
    oCon.CommitTrans
    oCon.Close
    Set objFolder = Nothing
    AddContacts = True
    Exit Function
errcode:
    AddContacts = False
End Function

Public Function InitializeOutlook() As Boolean
On Error Resume Next
    Set gOLApp = New Outlook.Application
    gOLApp.Session.Logon , , False, True
    Set gOLNameSpace = gOLApp.GetNamespace("MAPI")
End Function



Public Function CreateUserDB(DbLocation As String, Optional ContactsCol) As Boolean

   Dim cat As New ADOX.Catalog
    Dim tbl As New ADOX.Table
    Dim ssql As String, i As Long, nNewid As Long
    Dim objFolder As Object
   
    On Error GoTo errcode
     Set oCon = New Connection

        ' Open the catalog
       cat.ActiveConnection = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
          "Data Source=" & DbLocation & ";"
    
       ' Create a new Table object.
       With tbl
          .Name = "tUsers"
          ' Create fields and append them to the new Table
          ' object. This must be done before appending the
          ' Table object to the Tables collection of the
          ' Catalog.
                    
        .Columns.Append "FullName", adVarWChar
        .Columns.Append "FileAs", adVarWChar
        .Columns.Append "id", adInteger
        .Columns.Append "username", adVarWChar
        .Columns.Append "pwd", adVarWChar
        .Columns.Append "rights", adVarWChar
        .Columns.Append "userref", adVarWChar
        .Columns.Append "ClientName", adVarWChar
        .Columns.Append "LetrUserName", adVarWChar
        .Columns.Append "AuthorName", adVarWChar
        .Columns.Append "AuthorTitle", adVarWChar
        .Columns.Append "WhichAuthor", adVarWChar
        .Columns.Append "AuthorInitials", adVarWChar
        .Columns.Append "CreateOfflineProfile", adVarWChar
        .Columns.Append "FullPSTCopy", adVarWChar
        .Columns.Append "seeall", adVarWChar
        .Columns.Append "entry_id", adVarWChar
        
          .Columns("FullName").Attributes = adColNullable
          .Columns("FileAs").Attributes = adColNullable
          .Columns("id").Attributes = adColNullable
          .Columns("username").Attributes = adColNullable
          .Columns("pwd").Attributes = adColNullable
          .Columns("rights").Attributes = adColNullable
          .Columns("ClientName").Attributes = adColNullable
          .Columns("userref").Attributes = adColNullable
          .Columns("LetrUserName").Attributes = adColNullable
          .Columns("AuthorName").Attributes = adColNullable
          .Columns("AuthorTitle").Attributes = adColNullable
          .Columns("WhichAuthor").Attributes = adColNullable
          .Columns("AuthorInitials").Attributes = adColNullable
          .Columns("CreateOfflineProfile").Attributes = adColNullable
          .Columns("FullPSTCopy").Attributes = adColNullable
          .Columns("seeall").Attributes = adColNullable
          .Columns("entry_id").Attributes = adColNullable
        End With
    
       ' Add the new table to the database.
       cat.Tables.Append tbl
        Set cat = Nothing

          CreateUserDB = True
    Exit Function
    
errcode:
    CreateUserDB = False
End Function


Public Function CreateDB(DbLocation As String, Optional ContactsCol) As Boolean

   Dim cat As New ADOX.Catalog
    Dim tbl As New ADOX.Table
    Dim ssql As String, i As Long, nNewid As Long
    Dim objFolder As Object
   
    On Error GoTo errcode
     Set oCon = New Connection

    cat.Create "Provider=Microsoft.Jet.OLEDB.4.0;" & _
        "Data Source=" & DbLocation & ";"

        ' Open the catalog
       cat.ActiveConnection = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
          "Data Source=" & DbLocation & ";"
    
       ' Create a new Table object.
       With tbl
          .Name = "tContacts"
          ' Create fields and append them to the new Table
          ' object. This must be done before appending the
          ' Table object to the Tables collection of the
          ' Catalog.
          .Columns.Append "id", adInteger
          .Columns.Append "fullname", adVarWChar
          .Columns.Append "fileas", adVarWChar
          .Columns.Append "companyname", adVarWChar
          .Columns.Append "email1address", adVarWChar
          .Columns.Append "mobiletelephonenumber", adVarWChar
          .Columns.Append "Birthday", adVarWChar
          .Columns.Append "Business2TelephoneNumber", adVarWChar
          .Columns.Append "BusinessAddress", adLongVarWChar
          .Columns.Append "Homeaddress", adVarWChar
          .Columns.Append "suffix", adVarWChar
          .Columns.Append "title", adVarWChar
          .Columns.Append "HomeTelephoneNumber", adVarWChar
          .Columns.Append "HomeFaxNumber", adVarWChar
          .Columns.Append "BusinessFaxNumber", adVarWChar
          .Columns.Append "BusinessHomePage", adVarWChar
          .Columns.Append "BusinessTelephoneNumber", adVarWChar
          .Columns.Append "ntlogon", adVarWChar
          .Columns.Append "Salutation", adVarWChar
          .Columns.Append "entry_id", adVarWChar
          .Columns.Append "FirstName", adVarWChar
          .Columns.Append "LastName", adVarWChar
          .Columns.Append "OtherTelephoneNumber", adVarWChar
          .Columns.Append "otherfaxnumber", adInteger
          
            .Columns("FirstName").Attributes = adColNullable
          .Columns("LastName").Attributes = adColNullable
            .Columns("OtherTelephoneNumber").Attributes = adColNullable
          .Columns("fullname").Attributes = adColNullable
          .Columns("companyname").Attributes = adColNullable
          .Columns("fileas").Attributes = adColNullable
          .Columns("email1address").Attributes = adColNullable
          .Columns("mobiletelephonenumber").Attributes = adColNullable
          .Columns("Birthday").Attributes = adColNullable
          .Columns("Business2TelephoneNumber").Attributes = adColNullable
          .Columns("BusinessAddress").Attributes = adColNullable
          .Columns("Homeaddress").Attributes = adColNullable
          .Columns("suffix").Attributes = adColNullable
          .Columns("title").Attributes = adColNullable
          .Columns("HomeTelephoneNumber").Attributes = adColNullable
          .Columns("HomeFaxNumber").Attributes = adColNullable
          .Columns("BusinessFaxNumber").Attributes = adColNullable
          .Columns("BusinessHomePage").Attributes = adColNullable
          .Columns("BusinessTelephoneNumber").Attributes = adColNullable
          .Columns("ntlogon").Attributes = adColNullable
          .Columns("Salutation").Attributes = adColNullable
          .Columns("OtherTelephoneNumber").Attributes = adColNullable
          .Columns("entry_id").Attributes = adColNullable
          .Columns("otherfaxnumber").Attributes = adColNullable
        End With
    
       ' Add the new table to the database.
       cat.Tables.Append tbl
        Set cat = Nothing

          CreateDB = True
    Exit Function
    
errcode:
    CreateDB = False
End Function



Public Function NextId(aTableName As String, Optional nKey, Optional idField As String) As Long
Dim SqlString As String, ErrNumber As Long, bTest As Boolean, rs As Recordset
Dim Id As Long


    If IsMissing(nKey) Then
       SqlString = "select Max(id) + 1 as NewId from " & aTableName
    Else
       SqlString = "select Max(id), Max(" & idField & ") + 1 as NewId " & _
                   "From " & aTableName & " Where id = " & nKey
    End If
    
    If executesql(SqlString, rs) Then
        Id = NullToEmpty(rs!NewId, 1)
        rs.Close
        Do
            If IsMissing(nKey) Then
               SqlString = "Insert Into " & aTableName & " (id) values (" & Id & ")"
            Else
               SqlString = "Insert Into " & aTableName & " (id," & idField & ") " & _
                           "Values (" & nKey & "," & Id & ")"
            End If
            
            ErrNumber = 0
            executesql SqlString
            If ErrNumber = 20 Then
                Id = Id + 1
            Else
                NextId = Id
                Exit Do
            End If
        Loop
    End If

End Function



Public Function AddUsers(sDBLocation As String, ContactsCol) As Boolean
Dim sNTlogon As String
Dim obj As Object, nRowsAffected As Long, con As Connection, nNewid As Long

On Error GoTo errcode
    Set oCon = New Connection
 Connectme oCon, sDBLocation
    executesql "delete * from tUsers"
    
    oCon.BeginTrans
 For Each obj In ContactsCol.Items
    With obj
            nNewid = NextId("tusers")
      
    ssql = "update tusers set fullname=" & ConvertString(.FullName) & _
       ",fileas=" & ConvertString(.FileAs) & _
       ",username=" & ConvertString(.UserProperties("username")) & _
       ",pwd=" & ConvertString(.UserProperties("pwd")) & _
       ",seeall=" & ConvertString(.UserProperties("seeall")) & _
       ",rights=" & ConvertString(.UserProperties("rights")) & _
       ",userref=" & ConvertString(.UserProperties("userref")) & ",ClientName=" & ConvertString(.UserProperties("ClientName")) & _
       ",LetrUserName=" & ConvertString(.UserProperties("LetrUserName")) & ",AuthorName=" & ConvertString(.UserProperties("AuthorName")) & _
       ",CreateOfflineProfile=" & ConvertString(.UserProperties("CreateOfflineProfile")) & ",FullPSTCopy=" & ConvertString(.UserProperties("FullPSTCopy")) & _
       ",AuthorTitle=" & ConvertString(.UserProperties("AuthorTitle")) & ",WhichAuthor=" & ConvertString(.UserProperties("WhichAuthor")) & _
        ",entry_id=" & ConvertString(.EntryID) & " where id=" & nNewid
            Err.Clear
            On Error GoTo 0
            oCon.Execute ssql, nRowsAffected
            If nRowsAffected = 0 Then Exit Function
'                If Not executesql(ssql) Then Exit Function
    End With
Next obj
    oCon.CommitTrans
    oCon.Close
    Set objFolder = Nothing
    AddUsers = True
    Exit Function
errcode:
    AddUsers = False
End Function

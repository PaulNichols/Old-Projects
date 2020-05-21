VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsContact"
Attribute VB_GlobalNameSpace = True
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit


Public Enum ContactType
    OutlookPrivate = 0
    Exchangeprivate = 1
    Exchangepublic = 2
    AllExchange = 3
    Users = 4
    Offline = 5
End Enum
Public colContacts As Collection
Public objFolder As MAPIFolder
Public currenttype As ContactType
Private gOLNameSpace As NameSpace
Private boffline As Boolean
Private gOLApp As Outlook.Application
Private colData As Collection
'Private oData As clsusers
Private rs As Recordset
Private ssql As String
Public empid As Long
Public Settings As New clsSettings

Public Id As Long
Public User1 As String
Public username As String
Public pwd As String
Public rights As String
Public userref As String
Public ClientName As String
Public LetrUserName As String
Public AuthorName As String
Public AuthorTitle As String
Public WhichAuthor As String
Public AuthorInitials As String
Public userlevel As String
Public Seeall As String

Public Enum LoginStatus
    success = 0
    IncorrectUserName = 1
    IncorrectPassword = 2
End Enum
Public fullname As String
Public Firstname As String
Public LastName As String
Public Birthday As String
Public Business2TelephoneNumber As String
Public OtherTelephoneNumber As String
Public otherfaxnumber As Long
Public BusinessAddress As String
Public BusinessAddressCity As String
Public BusinessAddressCountry As String
Public BusinessAddressPostalCode As String
Public BusinessAddressStreet As String
Public BusinessFaxNumber As String
Public BusinessHomePage As String
Public BusinessTelephoneNumber As String
Public CompanyName As String
Public ntuserlogon As String
Public homeaddress As String
Public HomeFaxNumber As String
Public HomeTelephoneNumber As String
Public MobileTelephoneNumber As String
Public Title As String
Public Suffix As String
Public FileAs As String
Public Email1Address As String
Public Salutation As String
Public CreateOfflineProfile  As String
Private sCurruser As String
Public FullPSTCopy As String
Public EntryId As String
Private Declare Function GetUserName Lib "advapi32.dll" Alias "GetUserNameA" (ByVal lpBuffer As String, nSize As Long) As Long


Public Property Get ContactFolder() As Object
        Set ContactFolder = gOLNameSpace.Folders("Public Folders").Folders("All Public Folders").Folders("Contacts")
End Property

Public Property Get ConType() As ContactType
        ConType = currenttype
End Property

Public Property Let SetDBPath(sPath As String)
        DBPath = sPath
End Property
Public Property Let ConType(ConType As ContactType)
    On Error GoTo errcode
    
    'Logon
   ' gOLNameSpace.Logon
  ' Stop
   'boffline = True
   If ConType = Offline Then
        Set objFolder = gOLNameSpace.Folders("Offline").Folders("Contacts")
        'Set objFolder = gOLNameSpace.Folders("personal folders").Folders("Contacts")
        boffline = True
   Else
    If ConType <> OutlookPrivate And ConType <> Users Then
        If Not boffline Then
            Set objFolder = gOLNameSpace.Folders("Public Folders").Folders("All Public Folders").Folders("Contacts")
        Else
         Set objFolder = gOLNameSpace.Folders("Offline").Folders("Contacts")
        ' Set objFolder = gOLNameSpace.Folders("personal folders").Folders("Contacts")
       End If
    ElseIf ConType <> Users Then
        Set objFolder = gOLNameSpace.GetDefaultFolder(olFolderContacts)
    ElseIf ConType = Users Then
       If Not boffline Then
            Set objFolder = gOLNameSpace.Folders("Public Folders").Folders("All Public Folders").Folders("Users")
        Else
            Set objFolder = gOLNameSpace.Folders("Offline").Folders("Users")
        End If
    End If
    End If
    currenttype = ConType
    Exit Property

errcode:

End Property

'Public Property Let CurrUser(user As String)
'    sCurrUser = user
'End Property

Public Property Get Curruser() As String
       Dim nLen&
       
 nLen = 100
 On Error Resume Next
 Curruser = String$(nLen, 0)
 GetUserName Curruser, nLen
 Curruser = Left(Curruser, nLen - 1)
End Property
'
'Public Function CreateOffLineDB(Optional Currentuser, Optional ContactsCol, Optional bKillDb = True) As Boolean
'
'   Dim cat As New ADOX.Catalog
'    Dim tbl As New ADOX.Table
'    Dim ssql As String, i As Long, nnewid As Long
'    Dim objFolder As Object
'     Dim obj As Object, sNTlogon As String, bCreateStructure As Boolean, nRowsAffected As Long
'    'create db
'
'    On Error Resume Next
'       DBPath = DBPath
'
'    If bKillDb Then
'       Set con = Nothing
'        Kill DBPath
'        bCreateStructure = True
'    Else
'        bCreateStructure = Dir(DBPath) = ""
'    End If
'    If Err.Number = 53 Or Err.Number = 0 Then
'        Err.Clear
'    Else
'        Exit Function
'    End If
'    On Error GoTo 0
'     Set con = New Connection
'
'    If bCreateStructure Then
'        cat.Create "Provider=Microsoft.Jet.OLEDB.4.0;" & _
'            "Data Source=" & DBPath & ";"
'
'            'connect to exchange contacts list
'
'            'Set ContactsCol = gOLNameSpace.Folders("Public Folders").Folders("All Public Folders").Folders("Contacts")
'
'            ' Open the catalog
'           cat.ActiveConnection = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
'              "Data Source=" & DBPath & ";"
'
'           ' Create a new Table object.
'           With tbl
'              .Name = "tContacts"
'              ' Create fields and append them to the new Table
'              ' object. This must be done before appending the
'              ' Table object to the Tables collection of the
'              ' Catalog.
'              .Columns.Append "id", adInteger
'              .Columns.Append "fullname", adVarWChar
'              .Columns.Append "fileas", adVarWChar
'              .Columns.Append "companyname", adVarWChar
'              .Columns.Append "email1address", adVarWChar
'              .Columns.Append "mobiletelephonenumber", adVarWChar
'              .Columns.Append "Birthday", adVarWChar
'              .Columns.Append "Business2TelephoneNumber", adVarWChar
'              .Columns.Append "BusinessAddress", adLongVarWChar
'              .Columns.Append "Homeaddress", adVarWChar
'              .Columns.Append "suffix", adVarWChar
'              .Columns.Append "title", adVarWChar
'              .Columns.Append "HomeTelephoneNumber", adVarWChar
'              .Columns.Append "HomeFaxNumber", adVarWChar
'              .Columns.Append "BusinessFaxNumber", adVarWChar
'              .Columns.Append "BusinessHomePage", adVarWChar
'              .Columns.Append "BusinessTelephoneNumber", adVarWChar
'              .Columns.Append "ntlogon", adVarWChar
'              .Columns.Append "Salutation", adVarWChar
'              .Columns.Append "OtherTelephoneNumber", adVarWChar
'
'              .Columns("fullname").Attributes = adColNullable
'              .Columns("companyname").Attributes = adColNullable
'              .Columns("fileas").Attributes = adColNullable
'              .Columns("email1address").Attributes = adColNullable
'              .Columns("mobiletelephonenumber").Attributes = adColNullable
'              .Columns("Birthday").Attributes = adColNullable
'              .Columns("Business2TelephoneNumber").Attributes = adColNullable
'              .Columns("BusinessAddress").Attributes = adColNullable
'              .Columns("Homeaddress").Attributes = adColNullable
'              .Columns("suffix").Attributes = adColNullable
'              .Columns("title").Attributes = adColNullable
'              .Columns("HomeTelephoneNumber").Attributes = adColNullable
'              .Columns("HomeFaxNumber").Attributes = adColNullable
'              .Columns("BusinessFaxNumber").Attributes = adColNullable
'              .Columns("BusinessHomePage").Attributes = adColNullable
'              .Columns("BusinessTelephoneNumber").Attributes = adColNullable
'              .Columns("ntlogon").Attributes = adColNullable
'              .Columns("Salutation").Attributes = adColNullable
'              .Columns("OtherTelephoneNumber").Attributes = adColNullable
'            End With
'
'           ' Add the new table to the database.
'           cat.Tables.Append tbl
'
'
'           Set cat = Nothing
'
'        If IsMissing(ContactsCol) Then Set ContactsCol = gOLNameSpace.Folders("Offline").Folders("Contacts").Items
'    End If
'
'    OpenConnection con, DBPath
'    If Not bCreateStructure Then executesql "delete * from tcontacts"
'
'    con.BeginTrans
' For Each obj In ContactsCol
'        With obj
'            On Error Resume Next
'            sNTlogon = ""
'            sNTlogon = .User1
'            If Err.Number <> 91 And Err.Number <> 0 Then GoTo errcode
'            On Error GoTo 0
'                nnewid = NextId("tcontacts")
'                On Error Resume Next
'                Dim s As String
'                s = .UserProperties("Salutation")
'
'
'                 ssql = "update tcontacts set fullname=" & ConvertString(.fullname) & ",birthday=" & ConvertString(.Birthday) & _
'                    ",Business2TelephoneNumber=" & ConvertString(.Business2TelephoneNumber) & ",BusinessAddress=" & ConvertString(.BusinessAddress) & _
'                    ",BusinessFaxNumber=" & ConvertString(.BusinessFaxNumber) & ",BusinessHomePage=" & ConvertString(.BusinessHomePage) & _
'                    ",Homeaddress=" & ConvertString(.homeaddress) & ",suffix=" & ConvertString(.Suffix) & _
'                    ",title=" & ConvertString(.Title) & ",HomeTelephoneNumber=" & ConvertString(.HomeTelephoneNumber) & _
'                    ",HomeFaxNumber=" & ConvertString(.HomeFaxNumber) & _
'                    ",fileas=" & ConvertString(.FileAs) & ",companyname=" & ConvertString(.CompanyName) & ",email1address=" & ConvertString(.Email1Address) & _
'                    ",mobiletelephonenumber=" & ConvertString(.MobileTelephoneNumber) & _
'                    ",BusinessTelephoneNumber=" & ConvertString(.BusinessTelephoneNumber) & ",ntlogon=" & ConvertString(sNTlogon) & ",Salutation=" & ConvertString(s) & _
'                    ",OtherTelephoneNumber=" & ConvertString(.OtherTelephoneNumber) & " where id=" & nnewid
'                Err.Clear
'                On Error GoTo 0
'                con.Execute ssql, nRowsAffected
'                If nRowsAffected = 0 Then Exit Function
''                If Not executesql(ssql) Then Exit Function
'        End With
'    Next obj
'    con.CommitTrans
'    con.Close
'    Set objFolder = Nothing
'    CreateOffLineDB = True
'    Exit Function
'
'errcode:
'    CreateOffLineDB = False
'End Function
'
Private Sub Class_Initialize()
    If gOLApp Is Nothing Then InitializeOutlook
End Sub

Private Sub Class_Terminate()
On Error Resume Next

'gOLApp.Quit
'gOLNameSpace.Logoff
   ' Set gOLApp = Nothing
    'Set gOLNameSpace = Nothing

con.Close
End Sub

Public Function AddContact(oContactToAdd As Object, Currentuser As String, Optional bUser) As Boolean

Dim objContact As ContactItem, objContacts As Object, oItem As ContactItem
Dim i As Long
Dim newprop As Object, suser1 As String, currenttype As Integer, sEntryid As String

sEntryid = " "
On Error GoTo errcode
'If gOLApp Is Nothing Then InitializeOutlook

If oContactToAdd.Id = 0 Then
    Set objContact = objFolder.Items.Add
Else
    Set objContact = gOLNameSpace.GetItemFromID(oContactToAdd.EntryId, objFolder.StoreID)
    ' GetOutlookObjectFromEntryId (oContactToAdd.EntryId)
End If


Select Case ConType

Case 1
        objContact.User1 = Currentuser
Case Else

End Select
currenttype = ConType

'On Error Resume Next
With objContact
    If IsMissing(bUser) Then
        suser1 = ""
        On Error Resume Next
        suser1 = oContactToAdd.User1
        Err.Clear
        On Error GoTo errcode
        If (ConType = OutlookPrivate And (suser1 = Currentuser Or suser1 = "")) Or ConType <> 0 Then
            If ConType = OutlookPrivate Then .User1 = oContactToAdd.User1
            
            If oContactToAdd.Birthday <> "" Then .Birthday = oContactToAdd.Birthday
            .Email1Address = oContactToAdd.Email1Address
            .Business2TelephoneNumber = oContactToAdd.Business2TelephoneNumber
            .BusinessAddress = oContactToAdd.BusinessAddress
            .homeaddress = oContactToAdd.homeaddress
            .Suffix = oContactToAdd.Suffix
            .Title = .Title = IIf(oContactToAdd.Title = "", "Mr.", oContactToAdd.Title)
            .HomeTelephoneNumber = oContactToAdd.HomeTelephoneNumber
            .HomeFaxNumber = oContactToAdd.HomeFaxNumber
            .BusinessFaxNumber = oContactToAdd.BusinessFaxNumber
            .BusinessHomePage = oContactToAdd.BusinessHomePage
            .BusinessTelephoneNumber = oContactToAdd.BusinessTelephoneNumber
            .CompanyName = oContactToAdd.CompanyName
            .Firstname = oContactToAdd.Firstname
            .LastName = oContactToAdd.LastName
            .MobileTelephoneNumber = oContactToAdd.MobileTelephoneNumber
            On Error Resume Next
            Set objContacts = objFolder.Items.Restrict("[Company]='" & oContactToAdd.CompanyName & "'")
            If objContacts.Count = 0 Then
                .OtherTelephoneNumber = oContactToAdd.OtherTelephoneNumber
                .otherfaxnumber = oContactToAdd.otherfaxnumber
            Else
                For Each oItem In objContacts
                    .OtherTelephoneNumber = oItem.OtherTelephoneNumber
                    oContactToAdd.OtherTelephoneNumber = oItem.OtherTelephoneNumber
                    .otherfaxnumber = oItem.otherfaxnumber
                    oContactToAdd.otherfaxnumber = oItem.otherfaxnumber
                    Exit For
                Next
            End If
            On Error GoTo 0
            '.Title = oContactToAdd.Title
            .Suffix = oContactToAdd.Suffix
            '.UserProperties.
            .fullname = oContactToAdd.fullname
             .FileAs = oContactToAdd.FileAs
            .UserProperties.Add("Salutation", olText) = oContactToAdd.Salutation
             
        End If
        On Error Resume Next
        .Save
        On Error GoTo 0
        sEntryid = .EntryId
here:
    With oContactToAdd
          ssql = "update tcontacts set fullname=" & ConvertString(.fullname) & ",birthday=" & ConvertString(.Birthday) & _
                    ",Business2TelephoneNumber=" & ConvertString(.Business2TelephoneNumber) & ",BusinessAddress=" & ConvertString(.BusinessAddress) & _
                    ",BusinessFaxNumber=" & ConvertString(.BusinessFaxNumber) & ",BusinessHomePage=" & ConvertString(.BusinessHomePage) & _
                    ",Homeaddress=" & ConvertString(.homeaddress) & ",suffix=" & ConvertString(.Suffix) & _
                    ",firstname=" & ConvertString(.Firstname) & ",lastname=" & ConvertString(.LastName) & _
                    ",title=" & ConvertString(.Title) & ",HomeTelephoneNumber=" & ConvertString(.HomeTelephoneNumber) & _
                    ",HomeFaxNumber=" & ConvertString(.HomeFaxNumber) & _
                    ",fileas=" & ConvertString(.FileAs) & ",companyname=" & ConvertString(.CompanyName) & ",email1address=" & ConvertString(.Email1Address) & _
                    ",mobiletelephonenumber=" & ConvertString(.MobileTelephoneNumber) & _
                    ",entry_id=" & ConvertString(sEntryid) & _
                    ",otherfaxnumber=" & .otherfaxnumber & _
                    ",BusinessTelephoneNumber=" & ConvertString(.BusinessTelephoneNumber) & ",ntlogon=" & ConvertString(Currentuser) & ",Salutation=" & ConvertString(.Salutation) & _
                    ",OtherTelephoneNumber=" & CLng(IIf(Len(Trim(.OtherTelephoneNumber)) = 0, 0, .OtherTelephoneNumber)) & " where id=" & NextId("tcontacts", DBPath)
        End With
               ' .otherfaxnumber = CLng(IIf(Len(Trim(txtdtls1(10))) = 0, 0, txtdtls1(10)))
    Else
        .fullname = oContactToAdd.username
        .FileAs = oContactToAdd.username
        .UserProperties.Add("username", olText) = oContactToAdd.username
        .UserProperties.Add("pwd", olText) = oContactToAdd.pwd
        .UserProperties.Add("rights", olText) = oContactToAdd.rights
        .UserProperties.Add("userref", olText) = oContactToAdd.userref
        .UserProperties.Add("ClientName", olText) = oContactToAdd.ClientName
        .UserProperties.Add("LetrUserName", olText) = oContactToAdd.LetrUserName
        .UserProperties.Add("AuthorName", olText) = oContactToAdd.AuthorName
        .UserProperties.Add("AuthorTitle", olText) = oContactToAdd.AuthorTitle
        .UserProperties.Add("WhichAuthor", olText) = oContactToAdd.WhichAuthor
        .UserProperties.Add("AuthorInitials", olText) = oContactToAdd.AuthorInitials
        .UserProperties.Add("CreateOfflineProfile", olText) = oContactToAdd.CreateOfflineProfile
        .UserProperties.Add("FullPSTCopy", olText) = oContactToAdd.FullPSTCopy
        .UserProperties.Add("seeall", olText) = oContactToAdd.Seeall
        
        .Save
        If oContactToAdd.Id = 0 Then
            oContactToAdd.Id = NextId("tusers", DBPath)
        End If
        ssql = "update tusers set fullname =" & ConvertString(oContactToAdd.username) & ",FileAs=" & ConvertString(oContactToAdd.username) & _
        ",username=" & ConvertString(oContactToAdd.username) & ",pwd=" & ConvertString(oContactToAdd.pwd) & ",rights=" & ConvertString(oContactToAdd.rights) & _
        ",userref=" & ConvertString(oContactToAdd.userref) & ",ClientName=" & ConvertString(oContactToAdd.ClientName) & ",LetrUserName=" & ConvertString(oContactToAdd.LetrUserName) & _
        ",AuthorName=" & ConvertString(oContactToAdd.AuthorName) & ",AuthorTitle=" & ConvertString(oContactToAdd.AuthorTitle) & ",WhichAuthor=" & ConvertString(oContactToAdd.WhichAuthor) & _
        ",AuthorInitials=" & ConvertString(oContactToAdd.AuthorInitials) & ",CreateOfflineProfile=" & ConvertString(oContactToAdd.CreateOfflineProfile) & ",FullPSTCopy=" & ConvertString(oContactToAdd.FullPSTCopy) & _
        ",seeall=" & ConvertString(oContactToAdd.Seeall) & ",entry_id=" & ConvertString(.EntryId) & _
        " where id=" & oContactToAdd.Id
    End If
         AddContact = ExecuteSql(ssql, DBPath)
End With

Exit Function

errcode:
If Err.Number = 91 Then
    Err.Clear
    GoTo here
    MsgBox "It apperas that you are working Offline. The changes you have just made will only last until the Database is next updated.", vbInformation, "Offline"
End If
AddContact = False
'Resume Next
End Function


Public Function DelContacts(Currentuser As String, ByRef nDeleted As Long, Optional bAll = False, Optional DBID = 0, Optional bUser) As Boolean

Dim i As Long, x As Object, oCon As New Connection, ssql As String, nRowsAffected As Long, oContacttoremove As ContactItem
Dim oContacts As Object, rs As Recordset

On Error Resume Next
    If bAll Then
        
        Do While objFolder.Items.Count > 0
            Set x = objFolder.Items(1)
             x.Delete
        Loop
        ssql = "delete * from tcontacts"
    Else

        If DBID = "" Then
            If ConType <> AllExchange Then
               If ConType = Exchangeprivate Then
                   Set oContacts = objFolder.Items.Restrict("[user1]='" & Currentuser & "'")
                   ssql = "delete * from tcontacts where ntlogon=" & ConvertString(Currentuser)
               Else
                   Set oContacts = objFolder.Items.Restrict("[user1] =null")
                   If ConType <> 0 Then ssql = "delete * from tcontacts where ntlogon=' ' or ntlogon is null"
               End If
            Else
               Set oContacts = objFolder.Items.Restrict("[user1]=null or [user1]='" & Currentuser & "'")
               ssql = "delete * from tcontacts where ntlogon=' ' or ntlogon is null or ntlogon=" & ConvertString(Currentuser)
            End If
             nDeleted = oContacts.Count
             For i = nDeleted To 1 Step -1
                Set x = oContacts(i)
                x.Delete
            Next i
        Else
            If Not IsMissing(bUser) Then
                 If ExecuteSql("select entry_id from tusers where id=" & DBID, DBPath, rs) Then
                    If rs!entry_id <> "" Then
                        Set oContacttoremove = gOLNameSpace.GetItemFromID(rs!entry_id, objFolder.StoreID)
                        oContacttoremove.Delete
                    End If
                End If
                ssql = "delete * from tusers where id=" & DBID
            Else
                Err.Clear
                If ExecuteSql("select entry_id from tcontacts where id=" & DBID, DBPath, rs) Then
                    If rs!entry_id <> "" Then
                        Set oContacttoremove = gOLNameSpace.GetItemFromID(rs!entry_id, objFolder.StoreID)
                        oContacttoremove.Delete
                    End If
                End If
                If Err.Number = 91 Then
                    Err.Clear
                    MsgBox "It apperas that you are working Offline. The changes you have just made will only last until the Database is next updated.", vbInformation, "Offline"
                End If
                ssql = "delete * from tcontacts where id=" & DBID
            End If
            nDeleted = 1
        End If
    End If
     DelContacts = ExecuteSql(ssql, DBPath)


End Function

Public Function GetContacts(Optional sSortby, Optional bDesc, Optional sFilter, Optional sUser) As Boolean

'On Error Resume Next
'Dim objContact As ContactItem
'Dim i As Long, o As Items
    
    On Error Resume Next
    If IsMissing(sSortby) Or sSortby = "" Then sSortby = "Fileas"
    
        Select Case sSortby
        Case "File As": sSortby = "FileAs"
        Case "Full Name": sSortby = "fullname"
        Case "Company Name": sSortby = "CompanyName"
        Case "Business Address": sSortby = "BusinessAddress"
        Case "Business Fax No.": sSortby = "BusinessFaxNumber"
        Case "Business Home Page": sSortby = "BusinessHomePage"
        Case "Business Telephone No.": sSortby = "BusinessTelephoneNumber"
        Case "Email Address": sSortby = "Email1Address"
        Case "Mobile Telephone No.": sSortby = "MobileTelephoneNumber"
        Case "Home Address": sSortby = "homeaddress"
        Case "Home Fax No.": sSortby = "HomeFaxNumber"
        Case "Home Telephone No.": sSortby = "HomeTelephoneNumber"
        End Select


    If IsMissing(bDesc) Then bDesc = True
    If IsMissing(sFilter) Then
        Set colContacts = objFolder.Items
    Else
        If sFilter = "#" Then
            Set colContacts = objFolder.Items.Restrict("[fileas] < 'a' or [fileas] > 'z'")
        ElseIf sFilter = "*" Then
            Set colContacts = objFolder.Items
            'Set colContacts = objFolder.Items.Restrict("[user1] < 'a' or [user1] > 'z' or [user1] = ''" & IIf(sUser <> "", "'or [user1]='" & sUser & "'", ""))
        Else
            Set colContacts = objFolder.Items.Restrict("[fileas] >= '" & sFilter & "' and [fileas] <= '" & sFilter & "z'")
        End If
    End If
'
'    If Not IsMissing(sUser) Then
'        If sUser <> "" Then
'         Set colContacts = colContacts.Items.Restrict("[user1]='" & sUser & "'")
'        End If
'    End If
    colContacts.Sort "[" & sSortby & "]", bDesc
  

    GetContacts = (colContacts.Count > 0)

Exit Function

errcode:
    GetContacts = False
End Function

Public Function GetUsers() As Boolean
On Error GoTo errcode
Dim oContactToAdd As New clsContact, rs As New Recordset

Set rs = New Recordset
ssql = "Select * from tusers order by fileas"
If ExecuteSql(ssql, DBPath, rs) Then
    Set colContacts = New Collection
    Do While Not rs.EOF
        Set oContactToAdd = New clsContact
        With rs
            oContactToAdd.fullname = NullToEmpty(!username, "")
            oContactToAdd.FileAs = NullToEmpty(!username, "")
            oContactToAdd.username = NullToEmpty(!username, "")
            oContactToAdd.pwd = NullToEmpty(!pwd, "")
            oContactToAdd.rights = NullToEmpty(!rights, "")
            oContactToAdd.userref = NullToEmpty(!userref, "")
            oContactToAdd.ClientName = NullToEmpty(!ClientName, "")
            oContactToAdd.LetrUserName = NullToEmpty(!LetrUserName, "")
            oContactToAdd.AuthorName = NullToEmpty(!AuthorName, "")
            oContactToAdd.AuthorTitle = NullToEmpty(!AuthorTitle, "")
            oContactToAdd.WhichAuthor = NullToEmpty(!WhichAuthor, "")
            oContactToAdd.AuthorInitials = NullToEmpty(!AuthorInitials, "")
            oContactToAdd.CreateOfflineProfile = NullToEmpty(!CreateOfflineProfile, "")
            oContactToAdd.FullPSTCopy = NullToEmpty(!FullPSTCopy, "")
            oContactToAdd.Seeall = NullToEmpty(!Seeall, "")
            oContactToAdd.Id = !Id
            .MoveNext
        End With
        colContacts.Add oContactToAdd, CStr(oContactToAdd.Id)
    Loop
End If
    
    Set oContactToAdd = Nothing
    GetUsers = True
Exit Function

errcode:
    GetUsers = False
    If Err.Number = 3704 Then
        If ExecuteSql(ssql, DBPath, rs) Then Resume Next
    End If
End Function

Public Function FindContact(cuser As String, sField As String, sCriteria As String, Optional ByRef ReturnCollection) As Boolean


Dim oContact As ContactItem
Dim slogon  As String, i As Long, cType As Long, nLen As Long
Dim x As New Collection

On Error Resume Next
    cType = ConType
    nLen = Len(sCriteria)
    sCriteria = LCase$(sCriteria)

'    Set x = New Collection
'    Set oContact = objFolder.Items.Find("[" & sField & "] = '" & sCriteria & "'")
    Set x = objFolder.Items.Find("[" & sField & "] = '" & sCriteria & "'")
    Set oContact = objFolder.Items.Find("[FileAs] = '" & sCriteria & "'")
    Do While Not oContact Is Nothing
        x.Add oContact, CStr(oContact.FileAs)
        Set oContact = objFolder.Items.FindNext
        
    Loop
 FindContact = Not (x.Count = 0)
 If Not IsMissing(ReturnCollection) Then
    Set ReturnCollection = x
End If
Exit Function

errcode:

End Function

'Public Sub AddStore(sStore As String)
'    gOLNameSpace.AddStore sStore
'End Sub
Public Function InitializeOutlook() As Boolean
On Error Resume Next
'gOLApp.Quit
'gOLNameSpace.Logoff
    Set gOLApp = New Outlook.Application
    gOLApp.Session.Logon , , False, True
    Set gOLNameSpace = gOLApp.GetNamespace("MAPI")
End Function
Public Function movetoofflinepst(sPst As String) As Boolean
'Dim i As Long, j As Long
'
''For i = 1 To gOLNameSpace.Folders.Count
'' If gOLNameSpace.Folders(i).Name = "Offline" Then
''
''        gOLNameSpace.Folders("Offline").Folders.Remove ("Contacts")
''        gOLNameSpace.Folders("Offline").Folders("Users").Remove
''    End If
''Next i
'On Error GoTo here2
'here2:
'For i = 1 To gOLNameSpace.Folders.Count
' If gOLNameSpace.Folders(i).Name = "Offline" Then
'    For j = 1 To gOLNameSpace.Folders(i).Folders.Count
'        If gOLNameSpace.Folders(i).Folders(j).Name = "Contacts" Then
'            gOLNameSpace.Folders(i).Folders.Remove j
'        ElseIf gOLNameSpace.Folders(i).Folders(j).Name = "Users" Then
'            gOLNameSpace.Folders(i).Folders.Remove j
'        End If
'    Next j
'        GoTo here
'
'    End If
'Next i
'AddStore sPst
'On Error GoTo 0
''For i = 1 To gOLNameSpace.Folders.Count
'
'  '  If gOLNameSpace.Folders(i).Name = "Personal Folders" Then
'       ' If gOLNameSpace.Folders(i).Folders.Count = 1 Then
'If gOLNameSpace.Folders(gOLNameSpace.Folders.Count).Name = "Personal Folders" Then gOLNameSpace.Folders(gOLNameSpace.Folders.Count).Name = "Offline"
'        '  Exit For
'       ' Else
'            'gOLNameSpace.Folders("Offline").Folders("Contacts").Name = "Contacts2"
'           ' Exit For
'       ' End If
'    'End If
'
''Next i
'here:
'
'ConType = Exchangeprivate
'objFolder.CopyTo gOLNameSpace.Folders("Offline")
'ConType = Users
'objFolder.CopyTo gOLNameSpace.Folders("Offline")
'ConType = AllExchange
End Function
Public Function Logon(Optional sprofile As String, Optional spassword = "") As Boolean
On Error GoTo errcode

    
        gOLNameSpace.Logon sprofile, spassword, False, True

    Logon = True
    Exit Function
errcode:
    Logon = False
End Function

Public Sub Logoff()
    gOLNameSpace.Logoff
End Sub
Sub DisplayEmailObject(sTo As String)
    Dim objMail As MailItem
    If gOLApp Is Nothing Then InitializeOutlook
    Set objMail = gOLApp.CreateItem(olMailItem)
    objMail.To = sTo
    objMail.Display
    Set objMail = Nothing
End Sub

Public Property Get Count() As Integer
    'this error trap allows us to fill an empty grid
    On Error Resume Next
    Count = colContacts.Count
    If Err.Number > 0 Then
        Count = 0
    End If
    On Error GoTo 0
End Property

Public Function Item(vKey As Variant) As Object
    If VarType(vKey) = vbInteger Or VarType(vKey) = vbLong Then
        If vKey > 0 And vKey <= Count Then
            Set Item = colContacts.Item(CInt(vKey))
        End If
    Else
        Set Item = colContacts.Item(CStr(vKey))
    End If
End Function

Public Function UpdateContact(oContact As clsContact, Optional DBID = 0) As Boolean
Dim ssql As String, s As String
Dim oExchContact As ContactItem, objContacts As Object, oItem As ContactItem, rs As Recordset
On Error GoTo errcode
    
   
'        If oContact.User1 = "*deleted*" Then MsgBox "This Contacts has actually been deleted by another user." & _
'        "To refresh your view log back into the program.", vbInformation, "Unable to save changes."
        
        If oContact.EntryId <> "" Then
            Set oExchContact = gOLNameSpace.GetItemFromID(oContact.EntryId, objFolder.StoreID)
        Else
            Set oExchContact = objFolder.Items.Add
        End If
            With oExchContact
                .fullname = oContact.fullname
                .Birthday = oContact.Birthday
                .Business2TelephoneNumber = oContact.Business2TelephoneNumber
                .BusinessAddress = oContact.BusinessAddress
                .BusinessFaxNumber = oContact.BusinessFaxNumber
                .BusinessHomePage = oContact.BusinessHomePage
                .homeaddress = oContact.homeaddress
                .Suffix = oContact.Suffix
                .Title = IIf(oContact.Title = "", "Mr.", oContact.Title)
                .HomeTelephoneNumber = oContact.HomeTelephoneNumber
                .HomeFaxNumber = oContact.HomeFaxNumber
                .Firstname = oContact.Firstname
                .LastName = oContact.LastName
                .FileAs = oContact.FileAs
                .CompanyName = oContact.CompanyName
                .Email1Address = oContact.Email1Address
                .MobileTelephoneNumber = oContact.MobileTelephoneNumber
                .BusinessTelephoneNumber = oContact.BusinessTelephoneNumber
                .User1 = oContact.User1
                .UserProperties.Add("Salutation", olText) = oContact.Salutation
                .OtherTelephoneNumber = oContact.OtherTelephoneNumber
                .otherfaxnumber = oContact.otherfaxnumber
                Set objContacts = objFolder.Items.Restrict("[Company]='" & oContact.CompanyName & "'")
                For Each oItem In objContacts
                    oItem.otherfaxnumber = oContact.otherfaxnumber
                Next oItem
                
                On Error Resume Next
                .Save
                On Error GoTo 0
            End With
here:
    With oContact
        
        ssql = "update tcontacts set fullname=" & ConvertString(.fullname) & ",birthday=" & ConvertString(.Birthday) & _
            ",Business2TelephoneNumber=" & ConvertString(.Business2TelephoneNumber) & ",BusinessAddress=" & ConvertString(.BusinessAddress) & _
            ",BusinessFaxNumber=" & ConvertString(.BusinessFaxNumber) & ",BusinessHomePage=" & ConvertString(.BusinessHomePage) & _
            ",Homeaddress=" & ConvertString(.homeaddress) & ",suffix=" & ConvertString(.Suffix) & _
            ",title=" & ConvertString(.Title) & ",HomeTelephoneNumber=" & ConvertString(.HomeTelephoneNumber) & _
            ",HomeFaxNumber=" & ConvertString(.HomeFaxNumber) & _
            ",otherfaxnumber=" & .otherfaxnumber & _
            ",Firstname=" & ConvertString(.Firstname) & _
            ",Lastname=" & ConvertString(.LastName) & _
            IIf(.EntryId = "", "", ",entry_id=" & ConvertString(.EntryId)) & _
            ",fileas=" & ConvertString(.FileAs) & ",companyname=" & ConvertString(.CompanyName) & ",email1address=" & ConvertString(.Email1Address) & _
            ",mobiletelephonenumber=" & ConvertString(.MobileTelephoneNumber) & _
            ",BusinessTelephoneNumber=" & ConvertString(.BusinessTelephoneNumber) & ",ntlogon=" & ConvertString(.User1) & ",Salutation=" & ConvertString(.Salutation) & _
            ",OtherTelephoneNumber=" & ConvertString(.OtherTelephoneNumber) & " where id=" & DBID
        
        UpdateContact = ExecuteSql(ssql, DBPath)

        ExecuteSql "update tcontacts set othertelephonenumber=" & ConvertString(.OtherTelephoneNumber) & _
                " , otherfaxnumber=" & .otherfaxnumber & " where companyname=" & ConvertString(.CompanyName), DBPath
        
        'Set rs = Nothing
    End With
    'UpdateContact = (nRowsAffected > 0)
    Exit Function
errcode:
    If Err.Number = 91 Then
        Err.Clear
        MsgBox "It apperas that you are working Offline. The changes you have just made will only last until the Database is next updated.", vbInformation, "Offline"
       'On Error Resume Next
       GoTo here
    End If
    UpdateContact = False
End Function

Public Function ContactPropertyValue(oContact As Object, sProperty As String) As String

    With oContact
        Select Case sProperty
        Case "File As": ContactPropertyValue = .FileAs
        Case "Full Name": ContactPropertyValue = .fullname
        Case "Company Name": ContactPropertyValue = .CompanyName
        Case "Business Address": ContactPropertyValue = .BusinessAddress
        Case "Business Fax No.": ContactPropertyValue = .BusinessFaxNumber
        Case "Business Home Page": ContactPropertyValue = .BusinessHomePage
        Case "Business Telephone No.": ContactPropertyValue = .BusinessTelephoneNumber
        Case "Email Address": ContactPropertyValue = .Email1Address
        Case "Mobile Telephone No.": ContactPropertyValue = .MobileTelephoneNumber
        Case "Home Address": ContactPropertyValue = .homeaddress
        Case "Home Fax No.": ContactPropertyValue = .HomeFaxNumber
        Case "Home Telephone No.": ContactPropertyValue = .HomeTelephoneNumber
        Case "Salutation": ContactPropertyValue = .Salutation
        Case "CreateOfflineProfile": ContactPropertyValue = .CreateOfflineProfile
        Case "FullPSTCopy": ContactPropertyValue = .FullPSTCopy
        Case "Phone Abbr": ContactPropertyValue = .OtherTelephoneNumber
        Case "Fax Abbr": ContactPropertyValue = .otherfaxnumber
        End Select
    End With
End Function

Public Function ImportAddresses(sPath As String) As Boolean
Dim ssql As String, rs As New Recordset, oContact As ContactItem

    DBPath = sPath
    ssql = "SELECT tUsers.UserName, tAddresses.*" & _
    " FROM tAddresses LEFT JOIN tUsers ON tAddresses.UserID = tUsers.ID" & _
    " WHERE (((tAddresses.Deleted)=False))"

    If ExecuteSql(ssql, DBPath, rs) Then
        ConType = Exchangeprivate
        Do While Not rs.EOF Or rs.BOF
            Set oContact = objFolder.Items.Add(olContactItem)
            With rs
                oContact.FileAs = NullToEmpty(!sName, "") & "," & NullToEmpty(!fname, "")
                oContact.CompanyName = NullToEmpty(!company, "")
                oContact.Firstname = NullToEmpty(!fname, "")
                oContact.LastName = NullToEmpty(!sName, "")
                oContact.Title = NullToEmpty(!Title, "")
                'If !add3 = "EC1Y 2DY" Then Stop
                oContact.BusinessAddress = NullToEmpty(!add1, "") & vbNewLine & NullToEmpty(!add2, "") & vbNewLine & NullToEmpty(!add3, "") & vbNewLine & NullToEmpty(!add4, "") & vbNewLine & NullToEmpty(!postcode, "")
                'oContact.BusinessAddressPostalCode = NullToEmpty(!postcode, "")
                oContact.BusinessTelephoneNumber = NullToEmpty(!tel, "")
                oContact.BusinessFaxNumber = NullToEmpty(!fax, "")
                oContact.User1 = NullToEmpty(!username, "")
                oContact.Email1Address = NullToEmpty(!Email, "")
                oContact.UserProperties.Add("Salutation", olText) = NullToEmpty(!Salutation, "")
                oContact.Save
            End With
            rs.MoveNext
        Loop
        ImportAddresses = True
        Set rs = Nothing
        Set oContact = Nothing
    End If
End Function

Public Function Login(username As String, password As String, ByRef OCurrentUser As clsContact) As LoginStatus
  '  Dim ocontacts As New clsContact
   ' Dim x As Collection
       ' ConType = Users
        'If FindContact(username, "fileas", username, x) Then
        Dim rs As Recordset
        OpenConnection con, DBPath
        Set rs = con.Execute("select * from tusers where fileas=" & ConvertString(username))
        If Not rs.EOF Then
            With rs
                If Not LCase$(!pwd) = LCase$(password) Then Login = IncorrectPassword: Exit Function
                OCurrentUser.AuthorInitials = NullToEmpty(!AuthorInitials, "")
                OCurrentUser.AuthorName = NullToEmpty(!AuthorName, "")
                OCurrentUser.AuthorTitle = NullToEmpty(!AuthorTitle, "")
                OCurrentUser.ClientName = NullToEmpty(!ClientName, "")
                OCurrentUser.LetrUserName = NullToEmpty(!LetrUserName, "")
                OCurrentUser.pwd = NullToEmpty(!pwd, "")
                OCurrentUser.rights = NullToEmpty(!rights, "")
                'OCurrentUser.userlevel = NullToEmpty(!userlevel, "")
                OCurrentUser.username = NullToEmpty(!username, "")
                OCurrentUser.userref = NullToEmpty(!userref, "")
                OCurrentUser.WhichAuthor = NullToEmpty(!WhichAuthor, "")
                OCurrentUser.Id = NullToEmpty(!Id, "")
                OCurrentUser.EntryId = NullToEmpty(!entry_id, "")
                On Error Resume Next
                OCurrentUser.Salutation = NullToEmpty(!Salutation, "")
                OCurrentUser.CreateOfflineProfile = NullToEmpty(!CreateOfflineProfile, "")
                OCurrentUser.FullPSTCopy = NullToEmpty(!FullPSTCopy, "")
                OCurrentUser.Seeall = NullToEmpty(!Seeall, "")
            End With
            Login = success
        Else
            Login = IncorrectUserName: Exit Function
        End If
        Set rs = Nothing
   

End Function

Public Function GetPassword(Uname As String) As String
Dim rs As Recordset

    'Dim x As Object
    'ConType = Users
    If ExecuteSql("select pwd from tusers where  [FullName]=" & ConvertString(Uname), DBPath, rs) Then
        GetPassword = rs!pwd
    End If
    'If FindContact(Uname, "Full Name", LCase$(Uname), x) Then GetPassword = x.Item(1).UserProperties("pwd")
    Set rs = Nothing
End Function


Public Function GetUsersFromMDB(sDBnPAth As String)
Dim cn As Connection, rs As Recordset, oUser As New clsContact, oContacts As New clsContact

'DBPath = sDBnPAth
'On Error Resume Next
OpenConnection cn, sDBnPAth
Set rs = cn.Execute("select * from tusers")

    Do While Not rs.EOF
        With rs
            If Not !Deleted Then
                oContacts.ConType = Users
                oUser.Id = 0
                oUser.AuthorInitials = NullToEmpty(!AuthorInitials, "")
                oUser.AuthorName = NullToEmpty(!AuthorName, "")
                oUser.AuthorTitle = NullToEmpty(!AuthorTitle, "")
                oUser.ClientName = NullToEmpty(!ClientName, "")
                oUser.LetrUserName = NullToEmpty(!LetrUserName, "")
                oUser.pwd = NullToEmpty(!password, "")
                oUser.rights = NullToEmpty(!rights, "")
                oUser.username = NullToEmpty(!username, "")
                oUser.userref = NullToEmpty(!userref, "")
                oUser.WhichAuthor = NullToEmpty(!WhichAuthor, "")
                oUser.CreateOfflineProfile = "1"
                oUser.Seeall = "0"
                oUser.FileAs = NullToEmpty(!AuthorName, "")
                If oUser.FileAs = "" Then oUser.FileAs = NullToEmpty(!username, "")
                oUser.fullname = NullToEmpty(!username, "")
                oContacts.AddContact oUser, "", True
            End If
            .MoveNext
        End With
    Loop
End Function


Public Function FindContact2(sCompany As String, sFileAs As String, username As String) As Boolean
Dim sextra As String
On Error GoTo errcode
sextra = IIf(ConType = Exchangeprivate, " and [User1]='" & username & "'", " and not ([User1] >= 'aa' and [User1] <= 'zz')")
If ConType = AllExchange Then sextra = " and ([User1]='" & username & "' or  not ([User1] >= 'aa' and [User1] <= 'zz'))"
If sCompany = "" And sFileAs <> "" Then
    Set colContacts = objFolder.Items.Restrict("([FileAs]>='" & sFileAs & "' and [FileAs]<'" & sFileAs & "z" & "')" & sextra)
ElseIf sCompany <> "" And sFileAs = "" Then
    Set colContacts = objFolder.Items.Restrict("([CompanyName]>='" & sCompany & "' and [CompanyName]<'" & sCompany & "z" & "')" & sextra)
ElseIf sCompany <> "" And sFileAs <> "" Then
    Set colContacts = objFolder.Items.Restrict("([CompanyName]>='" & sCompany & "' and [CompanyName]<'" & sCompany & "z" & "') and [FileAs]>='" & sFileAs & "' and [FileAs]<'" & sFileAs & "z" & "'" & sextra)
ElseIf sCompany = "" And sFileAs = "" And username = "" Then
    Set colContacts = objFolder.Items.Restrict("([FileAs] >= 'aa' and [FileAs] <= 'zz') " & sextra)
ElseIf sCompany = "" And sFileAs = "" Then
    Set colContacts = objFolder.Items.Restrict("([FileAs] >= 'aa' and [FileAs] <= 'zz') " & sextra)
End If
colContacts.Sort "[FileAs]", False
Exit Function
errcode:
 
End Function

Public Function FindContact3(rs As Recordset, username As String, ParamArray Criteria() As Variant) As Boolean
Dim oCon As Connection, ssql As String, ssqlextra As String
On Error GoTo errcode

    ssql = "select * from tcontacts "
    ssqlextra = ""
    If Len(Criteria(0)) > 0 Then
        ssqlextra = ssqlextra & "where left(lcase$(companyname)," & Len(Criteria(0)) & ")=" & ConvertString(CStr(Criteria(0)))
        If Len(Criteria(1)) > 0 Then
            ssqlextra = ssqlextra & " and left$(lcase$(fileas)," & Len(Criteria(1)) & ") = " & ConvertString(CStr(Criteria(1)))
        End If
    Else
         If Len(Criteria(1)) > 0 Then
            ssqlextra = ssqlextra & " where left$(lcase$(fileas)," & Len(Criteria(1)) & ") = " & ConvertString(CStr(Criteria(1)))
        End If
    End If
   ' If Criteria(2) <> 2 And (Len(CStr(Criteria(3))) = 0 Or CStr(Criteria(3)) = "0") Then
        If Len(ssqlextra) = 0 Then
            If (Len(CStr(Criteria(3))) = 0 Or CStr(Criteria(3)) = "0") Or Criteria(2) <> "2" Then
                ssqlextra = ssqlextra & " where "
            End If
        Else
            If (Len(CStr(Criteria(3))) = 0 Or CStr(Criteria(3)) = "0") Or Criteria(2) <> "2" Then
                ssqlextra = ssqlextra & " and "
            End If
        End If
    'End If
    ssql = ssql & ssqlextra
    Select Case Criteria(2)
        Case 0: ssqlextra = " (ntlogon= ' ' or ntlogon is null)"
        Case 1: ssqlextra = IIf(Len(CStr(Criteria(3))) = 0 Or CStr(Criteria(3)) = "0", " (ntlogon= " & ConvertString(username), " (ntlogon<> ' ' and not ntlogon is null") & ")"
        Case 2: ssqlextra = IIf(Len(CStr(Criteria(3))) = 0 Or CStr(Criteria(3)) = "0", " (ntlogon= ' ' or ntlogon is null or ntlogon= " & ConvertString(username), "") & ")"
    End Select
    If ssqlextra = ")" Then ssqlextra = ""
    Set rs = New Recordset
    FindContact3 = ExecuteSql(ssql & ssqlextra, DBPath, rs)
'    OpenConnection oCon, dbpath
'    Set FindContact3 = oCon.Execute(ssql & ssqlextra)
Exit Function

errcode:

End Function


Public Function MoveContacts(bAll As Boolean, sCurrentuser As String, FromType As ContactType, ToType As ContactType, nCount As Long, Optional DBID = 0) As Boolean
Dim i  As Long, x As ContactItem, ssql As String, nRowsAffected As Long, oCon As Connection
Dim ThisContact As Object, rs As Recordset

On Error GoTo errcode
    If Not bAll Then
        If FromType = Exchangeprivate And ToType = Exchangepublic Then
            ssql = "select entry_id from tcontacts where id=" & DBID
            Err.Clear
            On Error Resume Next
            If ExecuteSql(ssql, DBPath, rs) Then
                Set ThisContact = gOLNameSpace.GetItemFromID(rs!entry_id, objFolder.StoreID)
                If Err.Number = 91 Then
                    Err.Clear
                    MsgBox "It apperas that you are working Offline. The changes you have just made will only last until the Database is next updated.", vbInformation, "Offline"
                    On Error GoTo errcode
                Else
                
                    If Not ThisContact Is Nothing Then
                        ThisContact.User1 = ""
                        ThisContact.Save
                    End If
                End If
            End If
            ssql = "update tcontacts set ntlogon=null where id=" & DBID
        ElseIf FromType = Exchangepublic And ToType = Exchangeprivate Then
            ssql = "select entry_id from tcontacts where id=" & DBID
            Err.Clear
            On Error Resume Next
            If ExecuteSql(ssql, DBPath, rs) Then
                Set ThisContact = gOLNameSpace.GetItemFromID(rs!entry_id, objFolder.StoreID)
                If Err.Number = 91 Then
                    Err.Clear
                    MsgBox "It apperas that you are working Offline. The changes you have just made will only last until the Database is next updated.", vbInformation, "Offline"
                    On Error GoTo errcode
                Else
                    If Not ThisContact Is Nothing Then
                        ThisContact.User1 = sCurrentuser
                        ThisContact.Save
                    End If
                End If
            End If
            ssql = "update tcontacts set ntlogon=" & ConvertString(sCurrentuser) & " where id=" & DBID
        End If

        nCount = 1
    Else
        If FromType = Exchangeprivate And ToType = Exchangepublic Then
            ConType = Exchangeprivate
            Set colContacts = objFolder.Items.Restrict("[user1]='" & sCurrentuser & "'")
            nCount = colContacts.Count
            For i = nCount To 1 Step -1
                Set x = colContacts(i)
                x.User1 = Empty
                x.Save
            Next i
            ssql = "update tcontacts set ntlogon=null where ntlogon=" & ConvertString(sCurrentuser)
        ElseIf FromType = Exchangepublic And ToType = Exchangeprivate Then
            ConType = Exchangepublic
            Set colContacts = objFolder.Items.Restrict("[user1]=empty or [user1]=''")
            nCount = colContacts.Count
            For i = nCount To 1 Step -1
               Set x = colContacts(i)
                x.User1 = sCurrentuser
                x.Save
            Next i
            ssql = "update tcontacts set ntlogon=" & ConvertString(sCurrentuser) & "where ntlogon=' ' or ntlogon is null"
        ElseIf FromType = OutlookPrivate And ToType = Exchangeprivate Then
            ConType = OutlookPrivate
            Set colContacts = objFolder.Items
            ConType = ToType
            For i = 1 To colContacts.Count
                If Not AddContact(colContacts(i), sCurrentuser, False) Then
                    GoTo errcode
                End If
            Next i
            'frmaddressbook.mnuRefresh_Click
        End If
    End If
    If Len(ssql) > 0 Then
         MoveContacts = ExecuteSql(ssql, DBPath)
    Else
        MoveContacts = True
    End If
    Exit Function
errcode:
    MoveContacts = False
End Function

Public Function GetPhoneAbbr(bReOrder As Boolean) As Recordset
On Error GoTo errorcode
Dim rs As Recordset, i As Long, objContacts As Object, oItem As ContactItem

If bReOrder Then
    ExecuteSql " SELECT tContacts.companyname" & _
            " From tContacts" & _
            " Where (((tContacts.BusinessTelephoneNumber) Is Not Null) And ((Len(Trim([BusinessTelephoneNumber]))) > 0))" & _
            " GROUP BY tContacts.companyname, tContacts.OtherTelephoneNumber, Len(Trim([othertelephonenumber]))" & _
            " HAVING ((Not (tContacts.companyname) Is Null) AND ((Len(Trim([CompanyName])))>0) AND ((tContacts.OtherTelephoneNumber) Is Not Null) AND ((Len(Trim([othertelephonenumber])))>0))", DBPath, rs
        i = 1
    Do While Not rs.EOF
        ExecuteSql "update tcontacts set OtherTelephoneNumber='" & i & "' where companyname=" & ConvertString(rs!CompanyName), DBPath
         Set objContacts = objFolder.Items.Restrict("[Company]='" & rs!CompanyName & "'")
        For Each oItem In objContacts
            oItem.OtherTelephoneNumber = i
        Next oItem
        i = i + 1
        rs.MoveNext
    Loop
End If

ExecuteSql "SELECT tContacts.BusinessTelephoneNumber" & _
    " From tContacts" & _
    " GROUP BY tContacts.BusinessTelephoneNumber" & _
    " Having (((tContacts.BusinessTelephoneNumber) Is Not Null And Len(Trim([BusinessTelephoneNumber])) > 0))" & _
    "ORDER BY tContacts.BusinessTelephoneNumber", DBPath, rs
    Do While Not rs.EOF
        ExecuteSql "Update tcontacts set BusinessTelephoneNumber=" & ConvertString(Replace(rs!BusinessTelephoneNumber, " ", "")) & " where BusinessTelephoneNumber=" & ConvertString(rs!BusinessTelephoneNumber), DBPath
        rs.MoveNext
    Loop

ExecuteSql "SELECT Format(CLng([othertelephonenumber]),'00') AS field1, tContacts.companyname AS field2, First(tContacts.BusinessTelephoneNumber) AS field3" & _
        " From tContacts" & _
    " Where (((Len(Trim([CompanyName]))) > 0) And ((Len(Trim([BusinessTelephoneNumber]))) > 0) And ((Len(Trim([OtherTelephoneNumber]))) > 0))" & _
    " GROUP BY Format(CLng([othertelephonenumber]),'00'), tContacts.companyname, tContacts.OtherTelephoneNumber" & _
    " Having (((tContacts.CompanyName) Is Not Null) And (Not (tContacts.OtherTelephoneNumber) Is Null))" & _
    " ORDER BY Format(CLng([othertelephonenumber]),'00'), First(tContacts.BusinessTelephoneNumber), tContacts.OtherTelephoneNumber, tContacts.CompanyName", DBPath, GetPhoneAbbr
    Exit Function
errorcode:
    
End Function
Public Function GetFaxAbbr(bReOrder As Boolean) As Recordset
On Error GoTo errorcode
Dim rs As Recordset, i As Long, objContacts As Object, oItem As ContactItem

If bReOrder Then
    ExecuteSql " SELECT tContacts.companyname" & _
        " From tContacts" & _
        " Where (((tContacts.BusinessFaxNumber) Is Not Null) And ((Len(Trim([BusinessFaxNumber]))) > 0))" & _
        " GROUP BY tContacts.companyname, tContacts.otherfaxnumber" & _
        " HAVING ((Not (tContacts.companyname) Is Null) AND (Not (tContacts.otherfaxnumber) Is Null And (tContacts.otherfaxnumber)>0) AND ((Len(Trim([CompanyName])))>0))", DBPath, rs
        i = 1
    Do While Not rs.EOF
        ExecuteSql "update tcontacts set OtherfaxNumber='" & i & "' where companyname=" & ConvertString(rs!CompanyName), DBPath
        i = i + 1
        Set objContacts = objFolder.Items.Restrict("[Company]='" & rs!CompanyName & "'")
        For Each oItem In objContacts
            oItem.otherfaxnumber = i
        Next oItem
        rs.MoveNext
    Loop
End If


ExecuteSql "SELECT tContacts.BusinessFaxNumber" & _
    " From tContacts" & _
    " GROUP BY tContacts.BusinessFaxNumber" & _
    " Having (((tContacts.BusinessFaxNumber) Is Not Null And Len(Trim([BusinessFaxNumber])) > 0))" & _
    "ORDER BY tContacts.BusinessFaxNumber", DBPath, rs
    Do While Not rs.EOF
        ExecuteSql "Update tcontacts set BusinessFaxNumber=" & ConvertString(Replace(rs!BusinessFaxNumber, " ", "")) & " where BusinessFaxNumber=" & ConvertString(rs!BusinessFaxNumber), DBPath
        rs.MoveNext
    Loop

ExecuteSql "SELECT Format([otherfaxnumber],'00') AS field1, tContacts.companyname AS field2, First(tContacts.BusinessFaxNumber) AS field3" & _
        " From tContacts" & _
        " Where (((Len(Trim([CompanyName]))) > 0) And ((tContacts.otherfaxnumber) > 0) And ((Len(Trim([BusinessFaxNumber]))) > 0) And ((Len(Trim([otherfaxnumber]))) > 0))" & _
        " GROUP BY Format([otherfaxnumber],'00'), tContacts.companyname" & _
        " Having ((Not (Format([otherfaxnumber], '00')) Is Null) And ((tContacts.CompanyName) Is Not Null))" & _
        " ORDER BY Format([otherfaxnumber],'00'), tContacts.companyname, First(tContacts.BusinessFaxNumber)", DBPath, GetFaxAbbr
    Exit Function
errorcode:


End Function


Public Function GetNewAbbr() As Long
    Dim rs As Recordset
    On Error Resume Next
    ExecuteSql "SELECT Max(OtherTelephoneNumber)+1 AS maxno" & _
        " FROM [SELECT tContacts.OtherTelephoneNumber" & _
        " From tContacts" & _
        " GROUP BY tContacts.OtherTelephoneNumber" & _
        " HAVING ((Not (tContacts.OtherTelephoneNumber) Is Null))]. AS [%$##@_Alias]" & _
        " HAVING (((Max(OtherTelephoneNumber)+1)>0))", DBPath, rs
        GetNewAbbr = 1
    If Not rs.EOF Then
        GetNewAbbr = NullToEmpty(rs!maxno, 0)
    End If
End Function

Public Function GetNewAbbr2() As Long
    Dim rs As Recordset
    On Error Resume Next
       ExecuteSql "SELECT Max(otherfaxnumber)+1 AS maxno" & _
        " FROM [SELECT tContacts.otherfaxnumber" & _
        " From tContacts" & _
        " GROUP BY tContacts.otherfaxnumber" & _
        " HAVING ((Not (tContacts.otherfaxnumber) Is Null))]. AS [%$##@_Alias]" & _
        " HAVING (((Max(otherfaxnumber)+1)>0))", DBPath, rs
        GetNewAbbr2 = 1
    If Not rs.EOF Then
        GetNewAbbr2 = NullToEmpty(rs!maxno, 0)
    End If
End Function
Public Function PrintContacts(bPreview As Boolean, Index As Integer)
    'PrintContactsClass bPreview, Index, objFolder
End Function

Public Function RetriveFromDB(sCurrentuser As String, rs As Recordset, Optional sLetter = "", Optional bDesc = False, Optional sSortby, Optional bKillDb, Optional aContactType, Optional bSeeAll) As Boolean
Dim oCon As New Connection, ssqlextra As String

Screen.MousePointer = vbHourglass
DoEvents
If IsMissing(bSeeAll) Then bSeeAll = "1"
'If IsMissing(bKillDb) Then bKillDb = True
If IsMissing(aContactType) Then aContactType = 3
'If colContacts Is Nothing Then
'If Not boffline Then
'    Set colContacts = objFolder.Items
'    If bKillDb Then
'        If CreateOffLineDB(sCurrentuser, colContacts, bKillDb) Then
'    End If
'        End If
'    End If
 'Else
   ' If Dir(DBPath) = "" Then MsgBox "There is no Offline DB to work with. The program will now terminate.": End
       
 'End If
 


    If IsMissing(sSortby) Or sSortby = "" Then sSortby = "Fileas"
        Select Case sSortby
        Case "File As": sSortby = "FileAs"
        Case "Full Name": sSortby = "fullname"
        Case "Company Name": sSortby = "CompanyName"
        Case "Business Address": sSortby = "BusinessAddress"
        Case "Business Fax No.": sSortby = "BusinessFaxNumber"
        Case "Business Home Page": sSortby = "BusinessHomePage"
        Case "Business Telephone No.": sSortby = "BusinessTelephoneNumber"
        Case "Email Address": sSortby = "Email1Address"
        Case "Mobile Telephone No.": sSortby = "MobileTelephoneNumber"
        Case "Home Address": sSortby = "homeaddress"
        Case "Home Fax No.": sSortby = "HomeFaxNumber"
        Case "Home Telephone No.": sSortby = "HomeTelephoneNumber"
        Case "Phone Abbr": sSortby = "OtherTelephoneNumber"
        End Select

    If IsMissing(bDesc) Then bDesc = True


ssql = "select * from tcontacts "
Select Case aContactType
    Case Exchangeprivate: ssqlextra = IIf(Len(sCurrentuser) > 0, IIf(bSeeAll = "1", " where (ntlogon<>' '  and not ntlogon is null) ", " where ntlogon=" & ConvertString(sCurrentuser)), "")
    Case Exchangepublic: ssqlextra = " where (ntlogon=  ' ' or  ntlogon is null) "
    Case AllExchange: ssqlextra = IIf(bSeeAll = "1", "", " where ((ntlogon=' ' or ntlogon is null) " & IIf(Len(sCurrentuser) = 0, "", " or ntlogon=" & ConvertString(sCurrentuser)) & ")")
End Select

 
 ssql = ssql & ssqlextra
 
 If Len(sLetter) > 0 Then
     If Trim(sLetter) = "*" Then
        
    ElseIf Trim(sLetter) = "#" Then
        ssql = ssql & IIf(Len(ssqlextra) > 0, " and ", " where") & _
        "  (left(fileas,1)<'a' or left(fileas,1)>'z')  "
    Else
        ssql = ssql & IIf(Len(ssqlextra) > 0, " and ", " where") & _
        " left(lcase(fileas),1)=" & ConvertString(CStr(sLetter))
    End If
 End If
 
  'ssql = ssql & IIf(Len(sLetter) > 0 And Trim(sLetter) <> "*", IIf(Trim(sLetter) <> "#", IIf(Len(ssqlextra) > 0, " and ", " where") & " left(lcase$(fileas),1)=" & ConvertString(CStr(sLetter)), ""), IIf(Len(ssqlextra) > 0, " and ", " where") & "  (left(fileas,1)<'a' and left(fileas,1)>'z')  ")

    ssql = ssql & " order by [" & sSortby & "]" & IIf(bDesc, " DESC", "")

Dim oContact As clsContact
Set colContacts = New Collection
If Not ExecuteSql(ssql, DBPath, rs) Then
    RetriveFromDB = False
    Screen.MousePointer = vbDefault
    Exit Function
End If

rs.MoveFirst
RetriveFromDB = True
Screen.MousePointer = vbDefault
End Function


Public Sub ResetColection()
'Set colContacts = objFolder.Items
End Sub

Public Function RemoveDeleted() As Boolean
Dim obj As Object
On Error GoTo errcode
MsgBox "It is recommended that all users leave the system before this is run." & vbNewLine & _
    "Other users systems will behave erratically until this is done.", vbExclamation, "Warning"
For Each obj In colContacts
    If obj.User1 = "*deleted*" Then obj.Delete
Next obj
RemoveDeleted = True
Exit Function
errcode:
RemoveDeleted = False
End Function
'
'Public Function CheckCount() As Boolean
'Dim rs As Recordset, oCon As New ADODB.Connection, nCount As Long
'
'     OpenConnection oCon, dbpath
'    Set rs = oCon.Execute("select count(id) as RECCOUNT from tcontacts")
'    If Not rs.EOF Then nCount = rs!RECCOUNT
'    CheckCount = (colContacts.Count <> nCount)
'    rs.Close
'    Set rs = Nothing
'    oCon.Close
'    Set oCon = Nothing
'End Function



Public Function GetSingleContact(nId As Long, oContact As clsContact) As Boolean
Dim rs As Recordset
        
        On Error GoTo errcode
    If ExecuteSql("select * from tcontacts where id=" & nId, DBPath, rs) Then
        With rs
            oContact.otherfaxnumber = NullToEmpty(!otherfaxnumber)
            oContact.Id = !Id
            oContact.fullname = !fullname
            oContact.Firstname = !Firstname
            oContact.LastName = !LastName
            oContact.Birthday = !Birthday
            oContact.Business2TelephoneNumber = !Business2TelephoneNumber
            oContact.OtherTelephoneNumber = !OtherTelephoneNumber
            oContact.BusinessAddress = !BusinessAddress
            oContact.BusinessFaxNumber = !BusinessFaxNumber
            oContact.BusinessHomePage = !BusinessHomePage
            oContact.BusinessTelephoneNumber = !BusinessTelephoneNumber
            oContact.CompanyName = !CompanyName
            oContact.User1 = NullToEmpty(!ntlogon, "")
            oContact.homeaddress = !homeaddress
            oContact.HomeFaxNumber = !HomeFaxNumber
            oContact.HomeTelephoneNumber = !HomeTelephoneNumber
            oContact.MobileTelephoneNumber = !MobileTelephoneNumber
            oContact.Title = !Title
            oContact.Suffix = !Suffix
            oContact.FileAs = !FileAs
            oContact.Email1Address = !Email1Address
            oContact.Salutation = !Salutation
            oContact.EntryId = !entry_id
        End With
    Else
        GetSingleContact = False
    End If
    GetSingleContact = True
    Set rs = Nothing
    Exit Function
errcode:
    GetSingleContact = False
End Function


Public Function GetSingleUser(nId As Long, oContact As clsContact) As Boolean
Dim rs As Recordset
        
        On Error GoTo errcode
    If ExecuteSql("select * from tusers where id=" & nId, DBPath, rs) Then
        With rs
            oContact.Id = !Id
            oContact.fullname = !username
            oContact.Firstname = !username
            oContact.username = !username
            oContact.pwd = !pwd
            oContact.rights = !rights
            oContact.userref = !userref
            oContact.ClientName = !ClientName
            oContact.LetrUserName = !LetrUserName
            oContact.AuthorName = !AuthorName
            oContact.AuthorTitle = !AuthorTitle
            oContact.WhichAuthor = !WhichAuthor
            oContact.userlevel = ""
            oContact.Seeall = !Seeall
            oContact.EntryId = !entry_id
        End With
    Else
        GetSingleUser = False
    End If
    GetSingleUser = True
    Set rs = Nothing
    Exit Function
errcode:
    GetSingleUser = False
End Function
Public Function GetOutlookObjectFromEntryId(EntryId As String) As Object
Set GetOutlookObjectFromEntryId = gOLNameSpace.GetItemFromID("EntryId", objFolder.StoreID)
'if GetOutlookObjectFromEntryId is nothing then set GetOutlookObjectFromEntryId=
End Function

Public Sub SetConType(aConType As Integer)
ConType = aConType
End Sub



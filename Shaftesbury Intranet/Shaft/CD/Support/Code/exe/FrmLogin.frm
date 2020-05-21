VERSION 5.00
Begin VB.Form FrmLogin 
   BackColor       =   &H00C0FFFF&
   BorderStyle     =   3  'Fixed Dialog
   ClientHeight    =   3780
   ClientLeft      =   30
   ClientTop       =   30
   ClientWidth     =   4770
   ClipControls    =   0   'False
   ControlBox      =   0   'False
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   3780
   ScaleWidth      =   4770
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton cmdadv 
      Caption         =   "Advanced"
      Height          =   375
      Left            =   3360
      TabIndex        =   13
      Top             =   1440
      Width           =   1215
   End
   Begin VB.CommandButton cmdcancel 
      Caption         =   "Cancel"
      Height          =   375
      Left            =   3360
      TabIndex        =   12
      Top             =   1080
      Width           =   1215
   End
   Begin VB.CommandButton cmdOK 
      Caption         =   "OK"
      Height          =   375
      Left            =   3360
      TabIndex        =   11
      Top             =   720
      Width           =   1215
   End
   Begin VB.TextBox txtprofile 
      Appearance      =   0  'Flat
      Height          =   285
      Left            =   2640
      TabIndex        =   3
      Top             =   3000
      Width           =   1935
   End
   Begin VB.CommandButton cmdnetwork 
      Caption         =   "Exchange Server"
      Height          =   495
      Index           =   0
      Left            =   1320
      TabIndex        =   9
      TabStop         =   0   'False
      Top             =   2280
      Width           =   1215
   End
   Begin VB.Frame Frame1 
      BackColor       =   &H00C0FFFF&
      Height          =   1695
      Left            =   1200
      TabIndex        =   8
      Top             =   2040
      Width           =   3495
      Begin VB.CheckBox chkoffline 
         BackColor       =   &H00C0FFFF&
         Caption         =   "Work Offline"
         Height          =   255
         Left            =   120
         TabIndex        =   4
         Top             =   1320
         Visible         =   0   'False
         Width           =   3255
      End
      Begin VB.TextBox txtnetwork 
         Appearance      =   0  'Flat
         Height          =   285
         Index           =   0
         Left            =   1440
         Locked          =   -1  'True
         TabIndex        =   2
         TabStop         =   0   'False
         Top             =   240
         Width           =   1935
      End
      Begin VB.Label lblprofile 
         BackStyle       =   0  'Transparent
         Caption         =   "Profile:"
         Height          =   255
         Left            =   720
         TabIndex        =   10
         Top             =   960
         Width           =   615
      End
   End
   Begin VB.TextBox txtusername 
      Appearance      =   0  'Flat
      Height          =   285
      Left            =   1200
      TabIndex        =   0
      Top             =   960
      Width           =   2055
   End
   Begin VB.TextBox txtPassword 
      Appearance      =   0  'Flat
      Height          =   288
      IMEMode         =   3  'DISABLE
      Left            =   1215
      PasswordChar    =   "*"
      TabIndex        =   1
      Top             =   1470
      Width           =   2055
   End
   Begin VB.Line Line1 
      X1              =   1200
      X2              =   4360
      Y1              =   525
      Y2              =   525
   End
   Begin VB.Label Label1 
      BackColor       =   &H00C0FFFF&
      Caption         =   "Contacts Login"
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   17.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   540
      Left            =   1200
      TabIndex        =   7
      Top             =   75
      Width           =   3135
   End
   Begin VB.Label lblPassword 
      BackStyle       =   0  'Transparent
      Caption         =   "Password"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   1215
      TabIndex        =   6
      Top             =   1275
      Width           =   975
   End
   Begin VB.Label lblUserName 
      BackStyle       =   0  'Transparent
      Caption         =   "User Name"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   1215
      TabIndex        =   5
      Top             =   780
      Width           =   975
   End
   Begin VB.Shape Shape1 
      BackColor       =   &H00FFC0C0&
      BackStyle       =   1  'Opaque
      BorderStyle     =   0  'Transparent
      Height          =   4935
      Index           =   0
      Left            =   -120
      Top             =   0
      Width           =   1215
   End
End
Attribute VB_Name = "FrmLogin"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Dim sPassword As String
Dim sUserName As String
Dim bPassfound As Boolean
Dim badvanced As Boolean
Dim oldtxtusername As String
Private Declare Function GetWindowsDirectory Lib "kernel32" Alias "GetWindowsDirectoryA" (ByVal lpBuffer As String, ByVal nSize As Long) As Long

Private Sub chkoffline_Click()
    bIsOnline = Not CBool(Abs(chkoffline.Value))
    txtprofile = IIf(bIsOnline, IIf(IsEmpty(ReadRegEntry("profile", txtusername)), "", ReadRegEntry("profile", txtusername)), "Shaftoffline")
   ' txtpst = IIf(bIsOnline, IIf(IsEmpty(ReadRegEntry("pst", txtusername)), "", ReadRegEntry("pst", txtusername)), IIf(IsEmpty(ReadRegEntry("offlinepst", txtusername)), "c:\shaftoffline_" & txtusername & ".pst", ReadRegEntry("offlinepst", txtusername)))
    'If txtpst = "  " And Not bIsOnline Then txtpst = "c:\shaftoffline_" & txtusername & ".pst"
    If bIsOnline And txtprofile = "" Then txtprofile = "devpn"
    If Not bIsOnline Then chkoffline.Value = Abs(Not (IsEmpty(ReadRegEntry("offlinepst", txtusername)) Or Trim(ReadRegEntry("offlinepst", txtusername)) = ""))
    If Not bIsOnline And txtprofile = "" Then txtprofile = "Shaftoffline"
End Sub

Private Sub cmdadv_Click()
badvanced = Not badvanced
Height = IIf(badvanced, 3870, 2145)
End Sub

Private Sub cmdCancel_Click()
  End
End Sub

Private Sub cmdnetwork_Click(Index As Integer)
Dim sRet As String, oldvalue As String

start:
sRet = frmNWCheck.LoadForm(Me)
If sRet = "" Then sRet = oldvalue
txtnetwork(Index) = sRet
End Sub

Private Sub CmdOk_Click()
On Error GoTo errcode
    Dim oUsers As New clsContact, sPath As String
 '   bIsOnline = Not CBool(Abs(chkoffline))
   'If Not bIsOnline Then oUsers.contype = Offline
    If txtprofile = "" Then
        MsgBox "You must enter the profile you wish to use."
        Exit Sub
    End If
here:
'    If Dir(txtpst) = "" Then
'        If Not bIsOnline Then
'                MsgBox "Your Offline file cannot be found."
'                Exit Sub
'        Else
'            MsgBox "You must select a valid pst to work with."
'        End If
'        txtpst = Trim(APIGetOpenFileName("Choose a valid PST...", "c:\", "Personal Folder {*.pst}" & Chr(0) & "*.pst" & Chr(0), 0, "", 200, 0, hwnd))
'
'
'        If txtpst = "" Then
'            If MsgBox("You failed to select a valid PST file to work with. " & vbNewLine & _
'            "Do you want to search again.", vbQuestion + vbYesNo) = vbYes Then
'                GoTo here
'            Else
'                Exit Sub
'            End If
'        End If
'
'    End If
'If bIsOnline Then
'     If CheckForMachine(IIf(txtnetwork(0) <> "", txtnetwork(0), ReadRegEntry("exchserver"))) <> "0   [ ip success ]" Then
'        badvanced = False
'        cmdadv_Click
'        MsgBox "Your Exchange Server cannot be reached. Please make sure you have selected the correct server. This program cannot continue without the correct server being selected.", vbCritical
'        Exit Sub
'    End If
 '   End If

    Set oCurUser = New clsContact
     Set oContacts = New clsContact
    ' oContacts.InitializeOutlook
     
           Set oCurUser = New clsContact
       GetASetting "GlobalTemplatePath", txtusername, sOldTemplatePath, sPath, vbString
       If sPath = "" Then sPath = sOldTemplatePath
          If Len(sPath) = 0 Or Len(Dir(sPath & "\*.dot")) = 0 Or Len(Dir(sPath & "\*.mdb")) = 0 Then
            'never set, or logging on as a different user
            MsgBox "You must set the directory location of the global templates by" & vbNewLine & "locating the normal.dot file"
            Dim oCD As New clsCommonDialog
            Do While Len(sPath) = 0
                sPath = oCD.APIGetOpenFileName("Select Normal.dot Template directory", "F:\DATA\OFFICE 2000\TEMPLATES\", "Microsoft Office Templates {*.dot}" & Chr(0) & "*.dot" & Chr(0), 0, "*.dot", 255, 1, hwnd)
                If InStr(1, sPath, "normal.dot", vbTextCompare) = 0 Then
                    MsgBox "You MUST select the normal.dot file"
                    sPath = ""
                End If
            Loop
            Set oCD = Nothing
            sPath = Left$(sPath, InStrRev(sPath, "\") - 1)
       End If
        UpdateRegEntry "GlobalTemplatePath", sPath, txtusername
        sOldTemplatePath = sPath
       oCurUser.Settings.GlobalTemplatePath = sPath
       oCurUser.SetDBPath = sPath & "\offlinecontacts2.mdb"
       ' oCurUser.Logon ReadRegEntry("profile", UserName)
    ' If Not bIsOnline Then oContacts.contype = Offline
    If Not oCurUser.Logon(txtprofile) Then MsgBox "Unable to login to Outlook, Check profile name.": Exit Sub

    Select Case oCurUser.Login(txtusername, txtPassword, oCurUser)
        Case IncorrectPassword
            MsgBox "The Password entered is incorrect."
             txtPassword = ""
            txtPassword.SetFocus
        Case IncorrectUserName
            MsgBox "The User Name entred is incorrect."
           ' txtusername = ""
            txtPassword = ""
            txtusername.SetFocus
        Case success
            GetUserSettings
            oCurUser.Settings.exchserver = txtnetwork(0)
           ' oCurUser.Settings.pdc = txtnetwork(1)
          '  oCurUser.Settings.PST = txtpst
            If bIsOnline Then
              '  oCurUser.Settings.OnlinePST = txtpst
                oCurUser.Settings.onlineProfile = txtprofile
            Else
               ' oCurUser.Settings.OfflinePST = txtpst
                oCurUser.Settings.offlineprofile = txtprofile
            End If
            oCurUser.Settings.Profile = txtprofile
            ' UpdateRegEntry "pst", CStr(PST), oCurUser.UserName
             UpdateRegEntry "profile", CStr(Profile), oCurUser.UserName
             
            'If oCurUser.CreateOfflineProfile = "1" Then SetupOffline
           ' oContacts.InitializeOutlook
            oContacts.contype = AllExchange
            
          '  oContacts.getcontacts
            Unload Me
            bDontLoad = True
            frmaddressbook.Show
          '  Load frmaddressbook
            
    End Select
    Set oUsers = Nothing
    Exit Sub
errcode:
 
End Sub

Private Sub GetUserSettings()
 Dim x As Variant, oCol As clsCols, Colnames() As String, i As Long, sPath As String
 
 On Error GoTo errcode
    If IsEmpty(ReadRegEntry("", oCurUser.UserName)) Then CreateRegEntry "", "", , oCurUser.UserName
    CreateRegEntry "LastUser", txtusername
    With oCurUser.Settings
        GetASetting "Templatepath", oCurUser.UserName, Space(1), sPath, vbString: .TemplatePath = sPath
        sPath = Trim$(sPath)
        If Len(Dir(sPath, vbDirectory)) = 0 Or Dir(sPath, vbDirectory) = "." Then
        Do While Len(Dir(sPath, vbDirectory)) = 0 Or Dir(sPath, vbDirectory) = "."
'            sPath = Trim$(Replace(LoadResString(105), "#####", oCurUser.ntuserlogon))
            sPath = String(255, Space(1))
            GetWindowsDirectory sPath, 255
            sPath = Left$(sPath, InStr(sPath, Chr(0)) - 1)
            sPath = Browse(sPath, hwnd, "Please select a directory for your templates")
            If Len(sPath) > 0 Then
                 If InStr(sPath, "SubTemplates") = 0 Then
                    sPath = sPath & "\SubTemplates"
                    MkDir sPath
                End If
                UpdateRegEntry "Templatepath", sPath, oCurUser.UserName
                .TemplatePath = sPath
                CheckTemplates oCurUser.Settings.GlobalTemplatePath
                CheckTemplates oCurUser.Settings.GlobalTemplatePath & "\SubTemplates"
            End If
        Loop
        Else
            CheckTemplates oCurUser.Settings.GlobalTemplatePath
            CheckTemplates oCurUser.Settings.GlobalTemplatePath & "\SubTemplates"
        End If
        GetASetting "PDC", "", LoadResString(104), x, vbString: .PDC = x
        GetASetting "exchserver", "", LoadResString(103), x, vbString: .exchserver = x
        GetASetting "ContactsView", oCurUser.UserName, "2", x, vbString: .ContactsView = x
        GetASetting "ContactsGridViewSortedBy", oCurUser.UserName, "0", x, vbLong: .ContactsGridViewSortedBy = x
        GetASetting "ContactsScreenWidth", oCurUser.UserName, "0", x, vbLong: .ContactsScreenWidth = x
        GetASetting "ContactsScreenHeight", oCurUser.UserName, "0", x, vbLong: .ContactsScreenHeight = x
        GetASetting "ContactsScreenTop", oCurUser.UserName, "0", x, vbLong: .ContactsScreenTop = x
        GetASetting "ContactsScreenLeft", oCurUser.UserName, "0", x, vbLong: .ContactsScreenLeft = x
        GetASetting "avaliblefields", oCurUser.UserName, LoadResString(101), x, vbString: .AvalibleFields = x
        GetASetting "column1", oCurUser.UserName, "", x, vbString
        GetASetting "Highlighttext", oCurUser.UserName, "0", x, vbString: .Highlighttext = x
        
       '  GetASetting "pst", oCurUser.UserName, "", x, vbString: .PST = x
          GetASetting "profile", oCurUser.UserName, oCurUser.UserName, x, vbString: .Profile = x
       ' GetASetting "onlinepst", oCurUser.UserName, "c:\" & oCurUser.UserName & ".pst", x, vbString: .OnlinePST = x
        GetASetting "seeall", oCurUser.UserName, "0", x, vbString: .Seeall = x
        '  GetASetting "offlinepst", oCurUser.UserName, "c:\" & oCurUser.UserName & ".pst", x, vbString: .OfflinePST = x
          GetASetting "viewtype", oCurUser.UserName, "3", x, vbString: .ViewType = x
          GetASetting "currentletter", oCurUser.UserName, "*", x, vbString: .CurrentLetter = x
          GetASetting "onlineprofile", oCurUser.UserName, oCurUser.UserName, x, vbString: .onlineProfile = x
        '  GetASetting "offlineprofile", oCurUser.UserName, "ShaftOffLine", x, vbString: .offlineprofile = x
          GetASetting "webpage", oCurUser.UserName, "http:\\www.yahoo.co.uk", x, vbString: .WebPage = x
          GetASetting "Background", oCurUser.UserName, &HC0FFFF, x, vbLong: .Background = x
          GetASetting "CompanyCol", oCurUser.UserName, &HFF&, x, vbLong: .CompanyCol = x
          GetASetting "GridBackCol", oCurUser.UserName, &H80000005, x, vbLong: .GridBackCol = x
          GetASetting "GridBorders", oCurUser.UserName, &HFFC0C0, x, vbLong: .GridBorders = x
          GetASetting "PrivateCol", oCurUser.UserName, &H0&, x, vbLong: .PrivateCol = x
         ' GetASetting "GlobalTemplatePath", oCurUser.UserName, "", spath, vbString: .GlobalTemplatePath = spath
      
        'check template dates for current

        
        GetCols .AvalibleFields, Colnames()
        For i = 1 To UBound(Colnames)
            Set oCol = New clsCols
            GetASetting "column" & i, oCurUser.UserName, "2000," & Colnames(i) & "," & i, x, vbString
             With oCol
                .Width = GetColWidth(x)
                .DefaultColWidth = 2000
                .Name = GetColName(x)
                .position = i
                '.PropertyName =
             End With
            .Columns.Add oCol, CStr(oCol.position)
        Next i
    End With
    Exit Sub
errcode:
    If Err.Number = 75 Then Resume Next
End Sub

Private Function GetColWidth(x)
GetColWidth = Mid(x, 1, InStr(1, x, ",") - 1)
End Function

Private Function GetColName(x)
Dim nPos As Long
nPos = InStr(1, x, ",")
GetColName = Mid(x, nPos + 1, InStr(nPos + 1, x, ",") - (nPos + 1))
End Function
Private Function GetColPos(x)
Dim nPos As Long
nPos = InStr(1, x, ",")
nPos = InStr(nPos + 1, x, ",")
GetColPos = Mid(x, nPos + 1)
End Function

Private Sub cmdpst_Click()
   'txtpst = Trim(APIGetOpenFileName("Choose a valid PST...", "c:\", "Personal Folder {*.pst}" & Chr(0) & "*.pst" & Chr(0), 0, "", 200, 0, hwnd))
End Sub

Private Sub Form_Activate()
    Show
   ' txtnetwork(0) = IIf(IsEmpty(ReadRegEntry("exchserver")), LoadResString(103), ReadRegEntry("exchserver"))
'    txtnetwork(1) = IIf(IsEmpty(ReadRegEntry("pdc")), LoadResString(104), ReadRegEntry("exchserver"))
'    If CheckForMachine(txtnetwork(0)) <> "0   [ ip success ]" Then
'        cmdadv_Click
'    Else
'        If CheckForMachine(txtnetwork(1)) <> "0   [ ip success ]" Then
'            cmdadv_Click
'        End If
'    End If
    txtPassword.SetFocus
End Sub

Public Sub Form_Load()
    Dim x$, oUsers As clsContact, bfailed As Boolean, sPath As String
    
    txtnetwork(0) = IIf(IsEmpty(ReadRegEntry("exchserver")), LoadResString(103), ReadRegEntry("exchserver"))
  '  txtnetwork(1) = IIf(IsEmpty(ReadRegEntry("pdc")), LoadResString(104), ReadRegEntry("pdc"))
'    txtpst = IIf(IsEmpty(ReadRegEntry("pst", UserName)), "c:\" & UserName, ReadRegEntry("pst", UserName))
    chkoffline.Value = Abs((IIf(IsEmpty(ReadRegEntry("pst", UserName)), "", ReadRegEntry("pst", UserName)) = IIf(IsEmpty(ReadRegEntry("offlinepst", UserName)), "", ReadRegEntry("offlinepst"))))
    If IIf(IsEmpty(ReadRegEntry("offlinepst", UserName)), "", ReadRegEntry("offlinepst")) = "" Then
     chkoffline.Value = 0
     Else
       ' chkoffline.Value = Abs((Dir(IIf(IsEmpty(ReadRegEntry("offlinepst", UserName)), "", ReadRegEntry("offlinepst")))) <> "" And chkoffline.Value = 1)
        End If
    txtprofile = IIf(IsEmpty(ReadRegEntry("profile", UserName)), UserName, ReadRegEntry("profile", UserName))
    bIsOnline = Not CBool(chkoffline.Value)
'    If CheckForMachine(txtnetwork(0)) <> "0   [ ip success ]" Then
'       bfailed = True
'       txtnetwork(0) = "Not Valid"
'    Else
'        If CheckForMachine(txtnetwork(1)) <> "0   [ ip success ]" Then
'            bfailed = True
'            txtnetwork(1) = "Not Valid"
'        End If
'    End If
    badvanced = Not bfailed
    cmdadv_Click
    GetASetting "lastuser", "", "", x, vbString
    If Not IsEmpty(ReadRegEntry("avaliblefields", UserName)) And LCase$(x) = LCase$(UserName) And Not badvanced Then
        'Set oUsers = New clsContact
       ' oUsers.InitializeOutlook
       Set oCurUser = New clsContact
       GetASetting "GlobalTemplatePath", UserName, sOldTemplatePath, sPath, vbString
              If sPath = "" Then sPath = sOldTemplatePath

          If Len(sPath) = 0 Or Len(Dir(sPath & "\*.dot")) = 0 Or Len(Dir(sPath & "\*.mdb")) = 0 Then
            'never set, or logging on as a different user
            MsgBox "You must set the directory location of the global templates by" & vbNewLine & "locating the normal.dot file"
            Dim oCD As New clsCommonDialog
            Do While Len(sPath) = 0
                sPath = oCD.APIGetOpenFileName("Select Normal.dot Template directory", "F:\DATA\OFFICE 2000\TEMPLATES\", "Microsoft Office Templates {*.dot}" & Chr(0) & "*.dot" & Chr(0), 0, "*.dot", 255, 1, hwnd)
                If InStr(1, sPath, "normal.dot", vbTextCompare) = 0 Then
                    MsgBox "You MUST select the normal.dot file"
                    sPath = ""
                End If
            Loop
            Set oCD = Nothing
            sPath = Left$(sPath, InStrRev(sPath, "\") - 1)
            
       End If
        UpdateRegEntry "GlobalTemplatePath", sPath, UserName
       sOldTemplatePath = sPath
       oCurUser.Settings.GlobalTemplatePath = sPath
       oCurUser.SetDBPath = sPath & "\offlinecontacts2.mdb"
        oCurUser.Logon ReadRegEntry("profile", UserName)
        
        If oCurUser.Login(UserName, oCurUser.GetPassword(UserName), oCurUser) = success Then
        
            GetUserSettings
             CreateRegEntry "LastUser", UserName
             Set oContacts = New clsContact
            oContacts.contype = AllExchange
            
         '   oContacts.getcontacts
             Load frmaddressbook
        Else
            Show
        End If
        Set oUsers = Nothing
    Else
        Show
        'Image1.Picture = LoadResPicture(107, 0)
         GetASetting "LastUser", "", UserName, x, vbString
         txtusername = IIf(x = "", UserName, x)
        If txtusername <> "" Then
            txtPassword.SetFocus
        Else
            txtusername.SetFocus
        End If
    End If
End Sub

Private Sub Form_Unload(Cancel As Integer)
On Error Resume Next
Set oContacts.colContacts = Nothing
End Sub

Private Sub txtPassword_GotFocus()
txtPassword.SelStart = 0
txtPassword.SelLength = Len(txtPassword)
End Sub

Private Sub txtPassword_KeyPress(KeyAscii As Integer)
    If KeyAscii = vbKeyReturn Then CmdOk_Click
End Sub

Private Sub txtusername_GotFocus()
oldtxtusername = txtusername
txtusername.SelStart = 0
txtusername.SelLength = Len(txtusername)
End Sub

Private Sub txtusername_LostFocus()
If txtusername <> oldtxtusername Then
txtprofile = IIf(IsEmpty(ReadRegEntry("profile", txtusername)), txtusername, ReadRegEntry("profile", txtusername))
'txtpst = IIf(IsEmpty(ReadRegEntry("pst", txtusername)), "c:\", ReadRegEntry("pst", txtusername))
End If
End Sub

Private Sub CheckTemplates(sPath As String)
Dim oFile As Scripting.FileSystemObject
Dim sFile As String, sFile2 As String
Dim Time1 As Date, Time2 As Date


    Set oFile = New Scripting.FileSystemObject
    sFile = Dir(sPath & "\*.dot")
    Do While Len(sFile) > 0
        If InStr(sPath, "SubTemplates") > 0 Then
            sFile2 = oCurUser.Settings.TemplatePath & "\" & sFile
        Else
            'sFile2 = oCurUser.Settings.TemplatePath & "\SubTemplates\" & sFile
            sFile2 = Left$(oCurUser.Settings.TemplatePath, InStrRev(oCurUser.Settings.TemplatePath, "\") - 1) & "\" & sFile
        End If
        'make sure that the dir exists
        If Not oFile.FolderExists(Left$(sFile2, InStrRev(sFile2, "\") - 1)) Then
            oFile.CreateFolder Left$(sFile2, InStrRev(sFile2, "\") - 1)
        End If
        
        If Not oFile.FileExists(sFile2) Then
            'file doesn't exist so copy
            oFile.CopyFile sPath & "\" & sFile, sFile2
        Else
            On Error Resume Next
            Time1 = "": Time2 = "1/1/1899"
            Time1 = oFile.GetFile(sPath & "\" & sFile).DateLastModified
            Time2 = oFile.GetFile(sFile2).DateLastModified
            On Error GoTo 0
            If DateDiff("n", Time1, Time2) <> 0 Then
                On Error Resume Next
                oFile.DeleteFile sFile2, True
                On Error GoTo 0
                oFile.CopyFile sPath & "\" & sFile, sFile2, True
            End If
        End If
        sFile = Dir
    Loop
End Sub

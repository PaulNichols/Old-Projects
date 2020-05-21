VERSION 5.00
Object = "{5E9E78A0-531B-11CF-91F6-C2863C385E30}#1.0#0"; "MSFLXGRD.OCX"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "mscomctl.ocx"
Begin VB.Form frmaddressbook 
   BackColor       =   &H00C0FFFF&
   Caption         =   " "
   ClientHeight    =   7395
   ClientLeft      =   165
   ClientTop       =   450
   ClientWidth     =   11880
   ClipControls    =   0   'False
   ControlBox      =   0   'False
   Icon            =   "frmaddressbookX.frx":0000
   LinkTopic       =   "Form1"
   ScaleHeight     =   7395
   ScaleWidth      =   11880
   StartUpPosition =   3  'Windows Default
   Begin VB.Timer tmrStartup 
      Enabled         =   0   'False
      Interval        =   65000
      Left            =   960
      Top             =   2280
   End
   Begin VB.Frame Fratoolbar 
      BackColor       =   &H00000000&
      BorderStyle     =   0  'None
      Height          =   615
      Left            =   135
      TabIndex        =   1
      Top             =   1320
      Width           =   12135
      Begin VB.CommandButton cmdTemplate 
         BackColor       =   &H00FFC0C0&
         Caption         =   "CHAPs"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Index           =   13
         Left            =   8640
         Style           =   1  'Graphical
         TabIndex        =   6
         Top             =   120
         Width           =   1335
      End
      Begin VB.CommandButton cmdTemplate 
         BackColor       =   &H00FFC0C0&
         Caption         =   "Letter"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Index           =   8
         Left            =   2040
         Style           =   1  'Graphical
         TabIndex        =   5
         Top             =   120
         Width           =   1335
      End
      Begin VB.CommandButton cmdTemplate 
         BackColor       =   &H00FFC0C0&
         Caption         =   "Memo"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Index           =   6
         Left            =   4680
         Style           =   1  'Graphical
         TabIndex        =   4
         Top             =   120
         Width           =   1335
      End
      Begin VB.CommandButton cmdTemplate 
         BackColor       =   &H00FFC0C0&
         Caption         =   "Rem Advice"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Index           =   5
         Left            =   7320
         Style           =   1  'Graphical
         TabIndex        =   3
         Top             =   120
         Width           =   1335
      End
      Begin VB.CommandButton cmdTemplate 
         BackColor       =   &H00FFC0C0&
         Caption         =   "Minutes"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Index           =   4
         Left            =   6000
         Style           =   1  'Graphical
         TabIndex        =   2
         Top             =   120
         Width           =   1335
      End
   End
   Begin VB.PictureBox picpopup 
      BackColor       =   &H00FFC0C0&
      BorderStyle     =   0  'None
      FillColor       =   &H00FFC0C0&
      FillStyle       =   0  'Solid
      Height          =   1695
      Left            =   3495
      Picture         =   "frmaddressbookX.frx":0442
      ScaleHeight     =   1695
      ScaleWidth      =   255
      TabIndex        =   0
      Top             =   840
      Visible         =   0   'False
      Width           =   255
   End
   Begin MSComctlLib.StatusBar StatusBar1 
      Align           =   2  'Align Bottom
      Height          =   375
      Left            =   0
      TabIndex        =   8
      Top             =   7020
      Width           =   11880
      _ExtentX        =   20955
      _ExtentY        =   661
      _Version        =   393216
      BeginProperty Panels {8E3867A5-8586-11D1-B16A-00C0F0283628} 
         NumPanels       =   2
         BeginProperty Panel1 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
         EndProperty
         BeginProperty Panel2 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
         EndProperty
      EndProperty
   End
   Begin MSFlexGridLib.MSFlexGrid grdcontacts 
      Height          =   5175
      Left            =   1935
      TabIndex        =   9
      Top             =   1200
      Width           =   6495
      _ExtentX        =   11456
      _ExtentY        =   9128
      _Version        =   393216
      Rows            =   0
      FixedRows       =   0
      BackColor       =   16777215
      BackColorFixed  =   12632256
      BackColorBkg    =   8454143
      GridColor       =   14737632
      AllowBigSelection=   0   'False
      SelectionMode   =   1
      AllowUserResizing=   1
      BorderStyle     =   0
      Appearance      =   0
   End
   Begin VB.Label lblopoup 
      BackColor       =   &H00FFC0C0&
      Height          =   4455
      Left            =   3015
      TabIndex        =   7
      Top             =   600
      Visible         =   0   'False
      Width           =   375
   End
   Begin VB.Menu mnugridoptions 
      Caption         =   "grid options"
      Visible         =   0   'False
      Begin VB.Menu mnuview 
         Caption         =   "Views"
         Begin VB.Menu mnuviewtype 
            Caption         =   "Private Contacts"
            Index           =   1
         End
         Begin VB.Menu mnuviewtype 
            Caption         =   "Company Contacts"
            Index           =   2
         End
         Begin VB.Menu mnuviewtype 
            Caption         =   "Private/Company Contacts"
            Index           =   3
         End
         Begin VB.Menu mnuviews 
            Caption         =   "Card View"
            Checked         =   -1  'True
            Index           =   1
            Visible         =   0   'False
         End
         Begin VB.Menu mnuviews 
            Caption         =   "Grid View"
            Index           =   2
            Visible         =   0   'False
         End
         Begin VB.Menu mnuviews 
            Caption         =   "Full Screen"
            Index           =   3
         End
         Begin VB.Menu mnuRefresh 
            Caption         =   "Refresh"
         End
      End
      Begin VB.Menu mnucolumns 
         Caption         =   "Visible Columns"
         Begin VB.Menu mnufields 
            Caption         =   "field1"
            Checked         =   -1  'True
            Index           =   0
         End
      End
      Begin VB.Menu mnusortby 
         Caption         =   "Sort By"
         Begin VB.Menu mnufeilds 
            Caption         =   "feild1"
            Checked         =   -1  'True
            Index           =   0
         End
      End
      Begin VB.Menu mnusplit5 
         Caption         =   "-"
      End
      Begin VB.Menu mnuEdit 
         Caption         =   "&Edit"
      End
      Begin VB.Menu mnuDelete 
         Caption         =   "&Delete"
      End
      Begin VB.Menu mnusplit1 
         Caption         =   "-"
      End
      Begin VB.Menu mnulogon 
         Caption         =   "Logon"
      End
      Begin VB.Menu mnuOnOffline 
         Caption         =   "Offline"
      End
      Begin VB.Menu mnuDock 
         Caption         =   "Dock"
      End
      Begin VB.Menu mnusplit2 
         Caption         =   "-"
      End
      Begin VB.Menu mnuemail 
         Caption         =   "Email"
      End
      Begin VB.Menu mnuRepots 
         Caption         =   "Reports"
      End
      Begin VB.Menu mnuEnv 
         Caption         =   "Print Envelopes"
      End
      Begin VB.Menu mnuLabels 
         Caption         =   "Print Labels"
      End
      Begin VB.Menu mnuTemplates 
         Caption         =   "Templates"
         Begin VB.Menu mnuSubTemplate 
            Caption         =   "?"
            Index           =   0
         End
      End
      Begin VB.Menu mnurunWord 
         Caption         =   "Run Word"
      End
      Begin VB.Menu mnuIE 
         Caption         =   "The Internet"
      End
      Begin VB.Menu mnusplit3 
         Caption         =   "-"
      End
      Begin VB.Menu mnuAdmin 
         Caption         =   "Administration"
      End
      Begin VB.Menu mnusplit4 
         Caption         =   "-"
      End
      Begin VB.Menu mnuExit 
         Caption         =   "Exit Application"
      End
   End
End
Attribute VB_Name = "frmaddressbook"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
'Dim bsmall As Boolean
Private bSeeDeleted As Boolean
Dim nHeight As Double, nWidth As Double, nheightper As Double, nwidthper As Double
Dim iLastCardRow As Integer, iLastCardCol As Integer
Dim npage As Long, bAllShown As Boolean, batbeginning As Boolean
'Dim nCurrentletter As Long
Dim CurrentGridView As ContactViews
Dim bmoveback As Boolean
Dim bDontRun As Boolean
Public bDontResize As Boolean
Dim bpopup As Boolean
Private Declare Function SetWindowLong Lib "user32" _
Alias "SetWindowLongA" _
(ByVal hwnd As Long, ByVal nIndex As Long, _
ByVal dwNewLong As Long) As Long
Private m_bdr As cFormBorder
Private bFirstTime As Boolean
Public SearchCompany As String
Public SearchFileas As String
Private Enum eMovement
    efirst = 1
    eprev = 2
    enext = 3
    elast = 4
End Enum
Dim bWasMax As Boolean
Public bDesc As Boolean
Private Enum eFunctions
    eadd = 0
    eedit = 1
    edelete = 2
    eSearch = 3
    eClose = 5
    eMove = 4
End Enum
'Dim region As Long

Private sTemplates() As String
 Private Declare Function ShellExecute Lib "shell32.dll" Alias _
        "ShellExecuteA" (ByVal hwnd As Long, ByVal lpOperation As _
        String, ByVal lpFile As String, ByVal lpParameters As String, _
        ByVal lpDirectory As String, ByVal nShowCmd As Long) As Long
      Public rs As Recordset

Private Sub Form_Activate()
If Not bDontactivate Then Form_Load
End Sub

Private Sub Form_Load()
    
    bDontactivate = True
    Visible = False
   ' bDontLoad = False
    If Not bDontLoad Then
    TemplateRegistry
    mnuviewtype(3).Checked = True

    'CurrentGridView = GridView
    Caption = LoadResString(102)
   ' StatusBar1.Panels.Add 1
    bDontResize = True
    On Error Resume Next
    With oCurUser.Settings
         Height = IIf(.ContactsScreenHeight = "0", GetSystemMetrics(SM_CYFULLSCREEN) * Screen.TwipsPerPixelY, .ContactsScreenHeight)
        Width = IIf(.ContactsScreenWidth = "0", GetSystemMetrics(SM_CXFULLSCREEN) * Screen.TwipsPerPixelX, .ContactsScreenWidth)
        Left = IIf(.ContactsScreenLeft = "0", 0, .ContactsScreenLeft)
        Top = IIf(.ContactsScreenTop = "0", 0, .ContactsScreenTop)
    End With
       With oCurUser.Settings
        .ContactsScreenLeft = Left
        .ContactsScreenTop = Top
        .ContactsScreenWidth = Width
        .ContactsScreenHeight = Height
    End With
    bDontResize = False
    bFirstTime = True
   ' oAZ.CurrentLetter = oCurUser.Settings.CurrentLetter
    position
    'Form_Resize
    batbeginning = True
    tX = Screen.TwipsPerPixelX
    TY = Screen.TwipsPerPixelY

'    nCurrentletter = 0
    'position
   ' mnuAdmin.Visible = (oCurUser.rights = "1")
    
    'lblcurrentletter_Click
    oAZ.Online = bIsOnline

    bDontactivate = True
   ' SetupGrid
    'mnuviewtype_Click 3
    End If
    If bDontLoad Then mnuDock_Click
    
    BackColor = oCurUser.Settings.Background
    Visible = True
End Sub

Private Sub Form_MouseDown(Button As Integer, Shift As Integer, x As Single, y As Single)
    If Button = 2 Then RightClicked
End Sub

Private Sub Form_QueryUnload(Cancel As Integer, UnloadMode As Integer)
    If MsgBox("Are you sure that you want to close this application.", vbQuestion + vbYesNo) = vbNo Then
        Cancel = True
    Else
        Unhook frmaddressbook
    End If
End Sub

Private Sub Form_Resize()
    On Error Resume Next
    If Not bDontResize And Not bShrunk Then
        'Visible = False
        mnuviews(3).Checked = False
      
        With oCurUser.Settings
            .ContactsScreenHeight = Height
            .ContactsScreenWidth = Width
            .ContactsScreenLeft = Left
            .ContactsScreenTop = Top
        End With
        bDontResize = False
        If mnuAdmin.Checked Then
            mnuAdmin_Click
        Else
            position
        End If
    End If
End Sub

Private Sub Form_Unload(Cancel As Integer)
Dim i As Integer

    
    If oCurUser.Settings.ContactsView = GridView Then
        For i = 0 To oCurUser.Settings.Columns.Count - 1
            oCurUser.Settings.Columns(i + 1).Width = grdcontacts.ColWidth(i)
        Next i
    End If
    With oCurUser.Settings
        .ContactsScreenLeft = Left
        .ContactsScreenTop = Top
        .ContactsScreenHeight = Height
        .ContactsScreenWidth = Width
    End With
    If oCurUser.Settings.SaveSettings(oCurUser.UserName) <> 0 Then
        MsgBox "There was an error saving your personalised settings."
    End If
    'If oCurUser.CreateOfflineProfile = "1" Then SetupOffline CBool(oCurUser.FullPSTCopy = "1")
    Set oContacts = Nothing
    CreateRegEntry "CurUser", "", REG_SZ, ""
    CreateRegEntry "CurUserPWD", "", REG_SZ, ""
  '  Set Me.oAZ = Nothing
 '    Unload Me
'   End
    'Set Me = Nothing
'PostMessage hwnd, WM_CLOSE, 0&, 0&

Dim nExitCode As Long
GetExitCodeProcess GetCurrentProcess(), nExitCode
'
ExitProcess nExitCode
End Sub

Private Sub irCommandButton1_Click()

End Sub

Private Sub irCommandButton1_GotFocus()

End Sub

Private Sub grdcontacts_Compare(ByVal Row1 As Long, ByVal Row2 As Long, Cmp As Integer)
'grdcontacts.ro
End Sub

Private Sub grdcontacts_MouseUp(Button As Integer, Shift As Integer, x As Single, y As Single)
'grdcontacts.cellfrom

mouse_event MOUSEEVENTF_ABSOLUTE, x, y, 0, 0
    Form_MouseDown Button, Shift, x, y
End Sub

Private Sub lblopoup_Click()
EnlargeForm
End Sub

Private Sub mnuDelete_Click()
    oAZ_FunctionButtonsClick edelete
End Sub

Private Sub mnuDock_Click()
    DockForm
End Sub

Private Sub mnuEdit_Click()
oAZ_FunctionButtonsClick eedit
End Sub

Private Sub mnuEnv_Click()
    Dim oTemplate As New clsTemplates
    If grdcontacts.Row > 1 Then
        If Not oTemplate.PrintEnv(MailAddress()) Then MsgBox "There was an error printing the envolopes."
    End If
    Set oTemplate = Nothing
End Sub

Private Sub mnuIE_Click()
On Error Resume Next
Dim ret&
ret& = ShellExecute(hwnd, "Open", oCurUser.Settings.WebPage, 0, App.Path, 1)

End Sub

Private Sub mnuLabels_Click()
    Dim oTemplate As New clsTemplates
    If grdcontacts.Row > 1 Then
        If Not oTemplate.PrintLabel(MailAddress()) Then MsgBox "There was an error printing the labels."
    End If
    Set oTemplate = Nothing
End Sub
Private Function MailAddress() As String
Dim oContact As Object

Set oContact = New clsContact
If oContacts.GetSingleContact(grdcontacts.RowData(grdcontacts.Row), oContact) Then
    With oContact
        MailAddress = Trim$(.Title & " " & Left$(.Firstname, 1))
        If InStr(1, .Firstname, " ") > 0 Then MailAddress = MailAddress & Trim$(Mid(.Firstname, InStr(1, .Firstname, " ") + 1, 1))
        MailAddress = MailAddress & " " & Trim$(.LastName) & IIf(Len(.Title) = 0, " Esq.", "") & vbCr
        If Len(Trim(.CompanyName)) > 0 Then MailAddress = MailAddress & Trim$(.CompanyName) & vbCr
        MailAddress = MailAddress & Trim$(.BusinessAddress)
    End With
    Set oContact = Nothing
End If
End Function

Public Sub mnuRefresh_Click()
Set oContacts.colContacts = Nothing
ShowContacts oAZ.CurrentLetter
End Sub

Private Sub mnurunWord_Click()
Dim oTemplate As New clsTemplates
oTemplate.RunTemplate ""
End Sub

Private Sub mnuSubTemplate_Click(Index As Integer)
Dim nIndex As Integer
   Dim oT As New clsTemplates
Dim names(5) As String
Dim oThisContact As New clsContact

    nIndex = GiveRealTemplateIndex(mnuSubTemplate(Index).Caption)

With oT
    If oContacts.GetSingleContact(grdcontacts.RowData(grdcontacts.Row), oThisContact) Then
    
    'Set oThisContact = oContacts.Item(grdcontacts.RowData(grdcontacts.Row))
    
    
    .TemplatePath = oCurUser.Settings.TemplatePath
    CreateRegEntry "CurUser", oCurUser.UserName, REG_SZ, ""
    CreateRegEntry "CurUserPWD", oCurUser.pwd, REG_SZ, ""
    Select Case mnuSubTemplate(Index).Caption
    Case "Letter"
    If grdcontacts.Row > 1 Then
      '  CreateRegEntry "let_theirref", oThisContact.ref, REG_SZ, oCurUser.UserName & "\TemplateDtls\"
        CreateRegEntry "let_name", oThisContact.fullname, REG_SZ, oCurUser.UserName & "\TemplateDtls\"
        CreateRegEntry "company", oThisContact.CompanyName, REG_SZ, oCurUser.UserName & "\TemplateDtls\"
        CreateRegEntry "address", oThisContact.BusinessAddress, REG_SZ, oCurUser.UserName & "\TemplateDtls\"
        CreateRegEntry "dear", oThisContact.Salutation, REG_SZ, oCurUser.UserName & "\TemplateDtls\"
        .RunTemplate "plc_let.dot"
        CreateRegEntry "let_name", "", REG_SZ, oCurUser.UserName & "\TemplateDtls\"
        CreateRegEntry "company", "", REG_SZ, oCurUser.UserName & "\TemplateDtls\"
        CreateRegEntry "address", "", REG_SZ, oCurUser.UserName & "\TemplateDtls\"
        CreateRegEntry "dear", "", REG_SZ, oCurUser.UserName & "\TemplateDtls\"

        End If
    Case "Fax"
        If grdcontacts.Row > 1 Then
            On Error Resume Next
             CreateRegEntry "fax_name", oThisContact.fullname, REG_SZ, oCurUser.UserName & "\TemplateDtls\"
            On Error GoTo 0
            CreateRegEntry "fax_company", oThisContact.CompanyName, REG_SZ, oCurUser.UserName & "\TemplateDtls\"
            CreateRegEntry "fax_abbr", oThisContact.otherfaxnumber, REG_SZ, oCurUser.UserName & "\TemplateDtls\"
            CreateRegEntry "their_fax", oThisContact.BusinessFaxNumber, REG_SZ, oCurUser.UserName & "\TemplateDtls\"
            .RunTemplate "Plc_fax.dot"
            CreateRegEntry "fax_name", "", REG_SZ, oCurUser.UserName & "\TemplateDtls\"
            CreateRegEntry "fax_company", "", REG_SZ, oCurUser.UserName & "\TemplateDtls\"
            CreateRegEntry "their_fax", "", REG_SZ, oCurUser.UserName & "\TemplateDtls\"
            CreateRegEntry "fax_abbr", "", REG_SZ, oCurUser.UserName & "\TemplateDtls\"
        End If
    Case "Memo"
        If grdcontacts.Row > 1 Then
            CreateRegEntry "To", oThisContact.fullname, REG_SZ, oCurUser.UserName & "\TemplateDtls\"
        Else
            CreateRegEntry "To", "", REG_SZ, oCurUser.UserName & "\TemplateDtls\"
        End If
        .RunTemplate "Plc_memo.dot"
        CreateRegEntry "To", "", REG_SZ, oCurUser.UserName & "\TemplateDtls\"

        
    Case "Mins"
        .RunTemplate "Plc_mins.dot"
    Case "Chaps"
        .RunTemplate "Plc_chap.dot"
       
        
    Case "Invoices"
        .RunTemplate "Plc_inv.dot"
    Case "Remitence"
     CreateRegEntry "rem_name", oThisContact.fullname, REG_SZ, oCurUser.UserName & "\TemplateDtls\"
        CreateRegEntry "company", oThisContact.CompanyName, REG_SZ, oCurUser.UserName & "\TemplateDtls\"
        CreateRegEntry "address", oThisContact.BusinessAddress, REG_SZ, oCurUser.UserName & "\TemplateDtls\"

        .RunTemplate "Plc_rem.dot"
     CreateRegEntry "rem_name", "", REG_SZ, oCurUser.UserName & "\TemplateDtls\"
        CreateRegEntry "company", "", REG_SZ, oCurUser.UserName & "\TemplateDtls\"
        CreateRegEntry "address", "", REG_SZ, oCurUser.UserName & "\TemplateDtls\"
        
    End Select
      End If
End With

  CreateRegEntry "CurUser", oCurUser.UserName, REG_SZ, ""

    ReOrderTemplateMenu Index
  
End Sub

Public Sub oAZ_AZClick(LetterChar As String)
    If LetterChar = "?" Then
        'frmaddressbook.Timer1.Enabled = False
        frmSearch.LoadForm Me
    Else
        oCurUser.Settings.CurrentLetter = LetterChar
        ShowContacts
    End If
End Sub

Private Sub grdcontacts_DblClick()
Dim oContact As New clsContact
With grdcontacts
    If .Row > 1 Then
        If oContacts.GetSingleContact(.RowData(.Row), oContact) Then
            frmAdd.LoadForm True, oContact
        End If
    End If
End With

End Sub

Private Sub mnuAdmin_Click()
Dim i As Long

'If oCurUser.rights <> "1" Then Exit Sub
For i = 1 To 2
    mnuviews(i).Checked = False
Next i
'mnuAdmin.Checked = Not mnuAdmin.Checked

Enabled = False
'frmaddressbook.Timer1.Enabled = False
frmadm.Show , Me
End Sub

Private Sub mnuemail_Click()
Dim oContact As New clsContact

If oCurUser.Settings.ContactsView = GridView Then
    If grdcontacts.Row > 1 Then
        If oContacts.GetSingleContact(grdcontacts.RowData(grdcontacts.Row), oContact) Then
            If oContact.Email1Address <> "" Then
                oContacts.DisplayEmailObject oContact.Email1Address
            Else
                MsgBox "This Contact does not have an Email address."
            End If
        End If
    End If
End If
End Sub

Private Sub mnuExit_Click()
    Unload Me
End Sub

Private Sub mnufeilds_Click(Index As Integer)
Dim i As Integer

    Screen.MousePointer = vbHourglass
    If mnufeilds(Index).Checked And bDesc Then
        bDesc = False
    ElseIf mnufeilds(Index).Checked And Not bDesc Then
         bDesc = True
    ElseIf Not mnufeilds(Index).Checked Then
         bDesc = False
    End If

    For i = 0 To mnufeilds.UBound - 1
        mnufeilds(i).Checked = (i = Index)
    Next i
    oCurUser.Settings.ContactsGridViewSortedBy = Index
    ShowContacts oCurUser.Settings.CurrentLetter
    Screen.MousePointer = vbNormal
End Sub

Private Sub mnufields_Click(Index As Integer)
mnufields(Index).Checked = Not mnufields(Index).Checked
oCurUser.Settings.Columns(Index + 1).Width = IIf(mnufields(Index).Checked, oCurUser.Settings.Columns(Index + 1).DefaultColWidth, 0)
grdcontacts.ColWidth(oCurUser.Settings.Columns(Index + 1).position - 1) = oCurUser.Settings.Columns(Index + 1).Width
End Sub

Private Sub mnulogon_Click()
  '  Set oContacts = New clsContact
  'oCurUser = Nothing
'Unload Me
bDontactivate = False
Hide
If oCurUser.Settings.SaveSettings(oCurUser.UserName) Then MsgBox "Failed to save setting.", vbInformation

UpdateRegEntry "lastuser", ""
Set oContacts.colContacts = Nothing
    FrmLogin.Show
End Sub

Private Sub mnuOnOffline_Click()
    OnLineOffline
End Sub

Private Sub mnuRepots_Click()
Dim i As Integer, oContact As New clsContact
With grdcontacts
    For i = 1 To .cols - 1
        'If Not bFirstTime Then oCurUser.Settings.Columns(i).Width = .ColWidth(i - 1)
         oCurUser.Settings.Columns(i).Width = .ColWidth(i - 1)
    Next i
    'frmaddressbook.Timer1.Enabled = False
   If .Row > 1 Then
        If oContacts.GetSingleContact(.RowData(.Row), oContact) Then
            frmreports.LoadForm Me, oContact
        End If
    Else
        frmreports.LoadForm Me
    End If
End With
End Sub

Public Sub mnuviews_Click(Index As Integer)
On Error Resume Next
    If Index <> 3 Then
        oCurUser.Settings.ContactsView = Index
        SwitchViews
    Else
        If WindowState = vbMaximized Then WindowState = vbNormal
        Visible = False
        Top = 0
        Left = 0
        Height = GetSystemMetrics(SM_CYFULLSCREEN) * Screen.TwipsPerPixelY
        Width = GetSystemMetrics(SM_CXFULLSCREEN) * Screen.TwipsPerPixelX
        Visible = True
        If Not mnuviews(3).Checked Then mnuviews(3).Checked = True
    End If
End Sub

Private Sub mnuviewtype_Click(Index As Integer)
Dim i As Integer, sExtra As String

For i = 1 To 3
    mnuviewtype(i).Checked = (i = Index)
Next i
'oContacts.InitializeOutlook
If i - 1 < 4 Then oContacts.contype = i - 1
ShowContacts oAZ.CurrentLetter
Select Case Index
Case 3: sExtra = "All "
Case 1: sExtra = "Private "
Case 2: sExtra = "Company "
End Select
StatusBar1.Panels(1).Text = sExtra & "Contacts for " & UCase$(oCurUser.UserName) & "   "
End Sub



Private Sub oAZ_FunctionButtonsClick(Index As Integer)
Dim oContact As New clsContact

    Enabled = False
    With grdcontacts
        Select Case Index
            Case eadd
               ' frmaddressbook.Timer1.Enabled = False
                frmAdd.LoadForm False, , Me
            Case eedit
                If .Row > 1 Then
                   ' frmaddressbook.Timer1.Enabled = False
                    If oContacts.GetSingleContact(.RowData(.Row), oContact) Then
                        frmAdd.LoadForm False, oContact, Me, oContact.ID
                    End If
                Else
                    MsgBox "Please choose a Contacts to Edit.", vbInformation
                End If
            Case edelete
               ' frmaddressbook.Timer1.Enabled = False
                If .Row > 1 Then
                    If oContacts.GetSingleContact(.RowData(.Row), oContact) Then
                        frmdel.LoadForm oContact, Me, oContact.ID
                    End If
                Else
                    frmdel.LoadForm , Me
                End If
            Case eSearch
                oAZ.CurrentLetter = "?"
            Case eClose
                Unload Me
            Case eMove
                'frmaddressbook.Timer1.Enabled = False
               If .Row > 1 Then
                    If oContacts.GetSingleContact(.RowData(.Row), oContact) Then
                        frmMove.LoadForm oContact, Me, oContact.ID
                    End If
                Else
                    frmMove.LoadForm , Me
                End If
        End Select
        Enabled = True
    End With
End Sub

Private Sub oAZ_RightClick(x As Single, y As Single)
    RightClicked
End Sub

Private Sub picpopup_Click()
    EnlargeForm
End Sub

Private Sub picpopup_MouseMove(Button As Integer, Shift As Integer, x As Single, y As Single)
    bDontResize = True
'    Move 0, 0, cmdletters(1).Width * 1.5, Height
    bDontResize = False
    bpopup = True
End Sub

Private Sub DisplayCount(nPrivate As Long, nPublic As Long)
 StatusBar1.Panels.Item(2).Text = nPrivate & " Private Contact(s) and " & nPublic & " Company Contact(s).   "
'    Select Case oContacts.contype
'        Case ExchangePublic: StatusBar1.Panels.Item(2).Text = nPublic & " Company Contact(s)."
'        Case ExchangePrivate: StatusBar1.Panels.Item(2).Text = nPrivate & " Private Contact(s)."
'        Case AllExchange: StatusBar1.Panels.Item(2).Text = nPrivate & " Private Contact(s) and " & nPublic & " Company Contact(s)."
'        Case OutlookPrivate: StatusBar1.Panels.Item(2).Text = nPrivate & " Outlook Contact(s)."
'    End Select
End Sub

Public Sub EnlargeForm()
Dim i As Long
    tmrStartup.Enabled = False
    Screen.MousePointer = vbHourglass
    
    If bDontLoad Then
        bDontResize = False
        bDontLoad = False
        Form_Load
    End If
    bShrunk = False
    KillTimer hwnd, 5001
    Visible = False
    bDontResize = True
    BorderStyle = 2
    Caption = LoadResString(102)
   
    With oCurUser.Settings
        Height = .ContactsScreenHeight
        Width = .ContactsScreenWidth
        Left = .ContactsScreenLeft
        Top = .ContactsScreenTop
    End With
    bDontResize = False
    lblopoup.Visible = False
    picpopup.Visible = False
   ' Fratoolbar.Visible = True
    oAZ.Visible = True
    grdcontacts.Visible = True
    'oAZ.Visible = True
    StatusBar1.Visible = True
    position
    bpopup = False
    If bWasMax Then WindowState = vbMaximized
     m_bdr.ShowInTaskBar = True
     ShowContacts
    Visible = True
    Notontop Me
'    Hook Me
   ' Timer1.Enabled = True
    Screen.MousePointer = vbNormal
End Sub
Public Sub SetupGrid()
Dim i As Long, sColNames As String
    With grdcontacts
        MousePointer = vbHourglass
         .Redraw = False
            .BackColor = oCurUser.Settings.Background
            BackColor = oCurUser.Settings.Background
            .BackColorFixed = oCurUser.Settings.GridBorders
            
          '   oAZ.ButtonBackgroundColor = oCurUser.settings.GridBorders
            .BackColorBkg = BackColor
           
            .cols = oCurUser.Settings.Columns.Count + 1
            .Rows = 0
            .FixedCols = 1
           ' .AddItem Replace(oCurUser.Settings.AvalibleFields, "|", vbTab)
           For i = 1 To oCurUser.Settings.Columns.Count
                sColNames = sColNames & oCurUser.Settings.Columns(CStr(i)).Name & vbTab
           Next i
           sColNames = sColNames & "  " & vbTab
           .AddItem sColNames
            .AddItem ""
            For i = 1 To .cols - 1
               ' If Not bFirstTime Then oCurUser.Settings.Columns(i).Width = .ColWidth(i - 1)
                .ColWidth(i - 1) = oCurUser.Settings.Columns(i).Width
                .TextMatrix(1, i - 1) = oCurUser.Settings.Columns(i).PropertyName
            Next i
            .ColWidth(i - 1) = 0
              .Row = 1
              .RowHeight(1) = 0
              For i = 0 To .cols - 1
                .Col = i
                If i > 0 Then .CellBackColor = oCurUser.Settings.GridBackCol
                If .Row = 1 Then .ForeColorSel = oCurUser.Settings.GridBackCol
                If i = 0 Then
                    .CellForeColor = .BackColorFixed
                Else
                    .CellForeColor = .CellBackColor
                End If
              Next i
         
'
'            .Refresh
'            oAZ.Refresh
    End With
     Me.MousePointer = vbDefault
End Sub
Public Sub fillgrid()
Dim oContact As Object, sNTlogon As String, ncount1 As Long, ncount2 As Long
Dim i As Long, contype As ContactType, nConCount As Long, cols() As String, j As Long
Dim sRow As String, bfoundsome As Boolean, k As Long, nCols As Long, oSettings As clsSettings
Dim bDeleted As Boolean, bMnu3 As Boolean, bMnu2 As Boolean, bMnu1 As Boolean, bSeeAll As Boolean, bPublic As Boolean

MousePointer = vbHourglass
DoEvents
bFirstTime = False

contype = oContacts.contype
Debug.Print Time & " Filling Grid"
With grdcontacts
    .Redraw = False
    .Rows = 2
    Set oSettings = oCurUser.Settings
    nCols = oSettings.Columns.Count

If Not rs Is Nothing Then
            Do While Not rs.EOF
               bDeleted = False
               bPublic = False
               If Len(Trim$(NullToEmpty(rs!ntlogon, ""))) = 0 Then
                    bPublic = True
                ElseIf Trim$(NullToEmpty(rs!ntlogon, "")) = "*deleted*" Then
                    bDeleted = True
                End If
                sRow = ""
                 For k = 0 To nCols - 1
                     sRow = sRow & rs.Fields(.TextMatrix(1, k)) & vbTab
                 Next k
                 'sRow = sRow & vbTab & rs!entry_ID
                 .AddItem sRow
                 .Row = .Rows - 1
                 .RowData(.Row) = rs!ID
                 For k = 0 To nCols
                     .Col = k
                     .CellForeColor = IIf(bPublic, oSettings.CompanyCol, oSettings.PrivateCol)
                     If bDeleted Then .CellForeColor = QBColor(9)
                     If k > 0 Then .CellBackColor = oSettings.GridBackCol
                     If k = 9 Then .CellForeColor = oSettings.GridBackCol
                 Next k
                 If bPublic Then
                     ncount2 = ncount2 + 1
                 Else
                     ncount1 = ncount1 + 1
                 End If

                 rs.MoveNext
           Loop
    If Not rs.BOF Then rs.MoveFirst
    End If
        .FixedRows = 1
         DisplayCount ncount1, ncount2
errcode:
    On Error GoTo 0
        .Redraw = True
        Me.MousePointer = vbDefault
    End With
    
     Debug.Print Time & " Finished"
End Sub



Private Sub position()
Dim ntop As Long, i As Long, nleft As Long
Const nNoofFUnctions = 5
On Error Resume Next
    
   ' Visible = False
   LockWindowUpdate hwnd
LockWindowUpdate grdcontacts.hwnd
For i = 0 To 2
    StatusBar1.Panels(i).AutoSize = sbrContents
Next i
    cmdWinState(2).Move (Width) - cmdWinState(2).Width - 50, ScaleTop
    cmdWinState(1).Move cmdWinState(2).Left - cmdWinState(1).Width, cmdWinState(2).Top
    cmdWinState(0).Move cmdWinState(1).Left - cmdWinState(0).Width, cmdWinState(2).Top
   ' Fratoolbar.Move (Width - oAZ.Width) / 2, (ScaleTop + ScaleHeight) - (StatusBar1.Height + (oAZ.Height))
   grdcontacts.Move ScaleLeft + 50, cmdWinState(0).Top + cmdWinState(0).Height + 10, ScaleWidth - 200, ScaleTop + (Height - ((Height - ScaleHeight) * 2)) - (oAZ.Height + StatusBar1.Height)
    oAZ.Move (Width - oAZ.Width) / 2, grdcontacts.Top + grdcontacts.Height ' (ScaleTop + ScaleHeight) - (StatusBar1.Height + (oAZ.Height))
    oAZ.ZOrder
    
    LockWindowUpdate 0
    LockWindowUpdate 0
  ' Visible = True 'Not bFirstTime
End Sub

Public Function ShowContacts(Optional sLetter As String, Optional bRefresh) As Boolean
Dim i As Long, j As Long

    If IsMissing(bRefresh) Then bRefresh = True
    If Len(sLetter) = 0 Then
        'not passed
        sLetter = oAZ.CurrentLetter
    End If
    
    If sLetter <> "?" Then
        If oContacts Is Nothing Then Set oContacts = New clsContact ': oContacts.InitializeOutlook , oContacts.contype
        For i = 1 To 3
            If mnuviewtype(i).Checked Then oContacts.contype = i
        Next i
         For i = 0 To oCurUser.Settings.Columns.Count - 1
            On Error Resume Next
                Load mnufields(i)
                mnufields(i).Caption = oCurUser.Settings.Columns(i + 1).Name
                mnufields(i).Checked = (oCurUser.Settings.Columns(i + 1).Width > 0)
                Load mnufeilds(i)
                mnufeilds(i).Caption = oCurUser.Settings.Columns(i + 1).Name
                mnufeilds(i).Checked = IIf(i = oCurUser.Settings.ContactsGridViewSortedBy, True, False)
            Next i
            'oCurUser.UserName ,True, rs, ,
       ' oContacts.getcontacts mnufeilds(oCurUser.Settings.ContactsGridViewSortedBy).Caption, bDesc, sLetter, _
        IIf(oContacts.contype = ExchangePrivate And oCurUser.Seeall <> "1", oCurUser.UserName, ""), bRefresh, rs
        If oContacts.RetriveFromDB(oCurUser.UserName, rs, sLetter, bDesc, mnufeilds(oCurUser.Settings.ContactsGridViewSortedBy).Caption, bRefresh, oContacts.contype, oCurUser.Seeall) Then
            fillgrid
        Else
            grdcontacts.Rows = 2
        End If
        ShowContacts = True
    End If
End Function

Private Sub SwitchViews()
'Dim i As Long
'
''fraadmin(0).Visible = False
''Fratoolbar.Visible = True
'mnuAdmin.Checked = False
'
'
'Select Case oCurUser.Settings.ContactsView
'    Case CardView
''        setupcards
''       ' fillgrid
''        mnucolumns.Visible = False
''        mnusortby.Visible = False
''        mnuviews(CardView).Checked = True
''        mnuviews(GridView).Checked = False
'''        With grdData(0)
'''            .Width = 6480
'''            .Left = shppage(0).Left
'''            .GridLines = flexGridFlat
'''            .GridLinesFixed = flexGridFlat
'''            .GridColor = vbWhite
'''            .GridColorFixed = vbWhite
'''            .BackColorFixed = vbWhite
'''            .ScrollBars = flexScrollBarNone
'''        End With
''        For i = 1 To cmdletters.UBound
''            If i <> 12 Then cmdletters(i).Visible = True
''        Next i
''        position
''        grdcontacts.Visible = False
'    Case Else
'        position
'        'FillGrid2
'        For i = 0 To oCurUser.Settings.Columns.Count - 1
'        On Error Resume Next
'            Load mnufields(i)
'            mnufields(i).Caption = oCurUser.Settings.Columns(i + 1).Name
'            mnufields(i).Checked = (oCurUser.Settings.Columns(i + 1).Width > 0)
'            Load mnufeilds(i)
'            mnufeilds(i).Caption = oCurUser.Settings.Columns(i + 1).Name
'            mnufeilds(i).Checked = IIf(i = oCurUser.Settings.ContactsGridViewSortedBy, True, False)
'        Next i
'        On Error GoTo 0
'        mnucolumns.Visible = True
'        mnusortby.Visible = True
'        mnuviews(CardView).Checked = False
'        mnuviews(GridView).Checked = True
'       ' grdData(0).Move shppage(0).Left, shppage(0).Top, shppage(0).Width * 2, shppage(0).Height - 25
'        'frarecord.Visible = False
''        With grdData(0)
''            '.Width = 6480
''            .GridLines = flexGridFlat
''            .GridLinesFixed = flexGridFlat
''            .GridColor = vbBlack
''            .GridColorFixed = vbBlack
''            .BackColorFixed = &HE0E0E0
''        End With
'    grdcontacts.Visible = True
'
'End Select


End Sub

Private Sub OnLineOffline()
oContacts.contype = Offline
End Sub

Private Sub RightClicked()
Dim sText As String, i As Long, oContact As New clsContact

With grdcontacts
If oContacts.GetSingleContact(.RowData(.Row), oContact) Then
    mnuemail.Enabled = (.Row > 1) And Len(Trim$(oContact.Email1Address)) <> 0
Else
     mnuemail.Enabled = (.Row > 1)
End If
    mnusortby.Enabled = (.Rows > 3)
    mnuEnv.Enabled = (.Row > 1)
    mnuLabels.Enabled = (.Row > 1)
    Err.Clear
    On Error Resume Next

        sText = sTemplates(0)
        mnuTemplates.Enabled = (Err.Number = 0)
        On Error GoTo 0

        If mnuSubTemplate.Count > 1 Then
            For i = 0 To mnuSubTemplate.Count - 1
                Select Case mnuSubTemplate(i).Caption
                Case "Fax": mnuSubTemplate(i).Enabled = (.Row > 1)
               ' Case "Memo": mnuSubTemplate(i).Enabled = (grdcontacts.Row > 1)
                Case "Letter": mnuSubTemplate(i).Enabled = (.Row > 1)
                Case Else: mnuSubTemplate(i).Enabled = True
                End Select
            Next
        End If
    
  End With
  Set oContact = Nothing
    PopupMenu mnugridoptions
End Sub

Public Sub DockForm()
    'Timer1.Enabled = False
    Unhook Me
If WindowState = vbMaximized Then WindowState = vbNormal: bWasMax = True ' Exit Sub
   ' ControlBox = False
   If Not bDontLoad Then
    With oCurUser.Settings
        .ContactsScreenLeft = Left
        .ContactsScreenTop = Top
        .ContactsScreenWidth = Width
        .ContactsScreenHeight = Height
    End With
    End If
     Visible = False
    bDontResize = True
    WindowState = vbNormal
    BorderStyle = 0
    Caption = ""
    'On Error Resume Next
    StatusBar1.Visible = False
    oAZ.Visible = False
    Fratoolbar.Visible = False
    bShrunk = True
    grdcontacts.Visible = False
    lblopoup.Visible = True
    picpopup.Visible = True
    lblopoup.Move 0, 0, 375, Screen.Height
    picpopup.Move 0, (Screen.Height - picpopup.Height) / 2, picpopup.Width, picpopup.Height
    bDontResize = True
    WindowState = vbNormal
    Visible = True
    'On Error Resume Next
    Move 0, 0, 60, Screen.Height
   ' lblopoup.BackColor = BackColor
    bDontResize = False
    picpopup.ZOrder
    lblopoup.ZOrder
    
    Ontop Me
    
    SetTimer hwnd, 5001, 1, AddressOf FormTimerProc
    
         Set m_bdr = New cFormBorder
    Set m_bdr.Client = Me
    m_bdr.ShowInTaskBar = False
    tmrStartup_Timer
    tmrStartup.Enabled = True
End Sub

Public Sub TemplateRegistry(Optional bClear As Boolean)
Dim i As Integer, sTemplate As String, sFile As String
Dim sReturn As String

On Error Resume Next
Screen.MousePointer = vbHourglass
DoEvents
If bClear Then
For i = 0 To UBound(sTemplates)
    UpdateRegEntry "Template" & i, "", oCurUser.UserName
Next i
End If
On Error GoTo 0
'    If IsEmpty(ReadRegEntry("", oCurUser.UserName)) Then CreateRegEntry "", "", , oCurUser.UserName
    sTemplate = oCurUser.Settings.TemplatePath
    If Len(sTemplate) > 0 Then
        If IsEmpty(ReadRegEntry("Template1", oCurUser.UserName)) Or ReadRegEntry("Template1", oCurUser.UserName) = "" Then
            On Error GoTo errcode
            sFile = Dir$(sTemplate & "\*.dot", vbNormal)
            i = 0
            Do While Len(sFile) > 0
                'CreateRegEntry "LastUser", txtusername
                sTemplate = GiveRealTemplateName(Replace(LCase$(sFile), ".dot", ""))
                If Len(sTemplate) > 0 Then
                 
                    GetASetting "Template" & i, oCurUser.UserName, sTemplate, sReturn, vbString
                    'fill array
                    ReDim Preserve sTemplates(i) As String
                    sTemplates(i) = sTemplate
                    i = i + 1
                End If
                sFile = Dir
            Loop
        Else
            i = 0
            Do While Not IsEmpty(ReadRegEntry("Template" & i, oCurUser.UserName))
                GetASetting "Template" & i, oCurUser.UserName, "", sReturn, vbString
                ReDim Preserve sTemplates(i) As String
                sTemplates(i) = sReturn
                i = i + 1
            Loop
        End If
    End If
    
    On Error Resume Next
    If Len(sTemplates(0)) > 0 Then
        If Err.Number = 0 Then
            For i = 0 To UBound(sTemplates)
                If i > 0 Then Load mnuSubTemplate(i)
                mnuSubTemplate(i).Caption = sTemplates(i)
            Next i
        End If
    End If
    On Error GoTo 0
    Screen.MousePointer = vbNormal
    Exit Sub
errcode:
    Screen.MousePointer = vbNormal
    'Resume Next
End Sub

Private Function GiveRealTemplateName(sFileName As String) As String

    Select Case LCase$(sFileName)
        Case "plc_chap": GiveRealTemplateName = "Chaps"
        Case "plc_fax": GiveRealTemplateName = "Fax"
        Case "plc_inv": GiveRealTemplateName = "Invoices"
        Case "plc_let": GiveRealTemplateName = "Letter"
        Case "plc_memo": GiveRealTemplateName = "Memo"
        Case "plc_mins": GiveRealTemplateName = "Mins"
        Case "plc_rem": GiveRealTemplateName = "Remitence"
        Case Else: GiveRealTemplateName = ""
    End Select
End Function

Private Function GiveRealTemplateIndex(sName As String) As Integer

    Select Case LCase$(sName)
        Case "Chaps": GiveRealTemplateIndex = 13
        Case "Fax": GiveRealTemplateIndex = 6
        Case "Invoices": GiveRealTemplateIndex = 0
        Case "Letter": GiveRealTemplateIndex = 8
        Case "Memo": GiveRealTemplateIndex = 7
        Case "Mins": GiveRealTemplateIndex = 4
        Case "Remitence": GiveRealTemplateIndex = 5
        Case Else: GiveRealTemplateIndex = 0
    End Select
End Function

Private Sub ReOrderTemplateMenu(nLastSelected As Integer)
Dim i As Integer, sTempArray() As String, sMyString As String, bFound As Boolean
    
    'no need to change if the top one was selected
    If nLastSelected = 0 Then Exit Sub
    
    ReDim sTempArray(UBound(sTemplates)) As String
    sMyString = sTemplates(nLastSelected)
    bFound = False
    For i = 0 To UBound(sTemplates)
        If i = 0 Then
            sTempArray(i) = sTemplates(nLastSelected)
        Else
            If sMyString = sTemplates(i - 1) Then bFound = True
            sTempArray(i) = sTemplates(i + IIf(bFound, 0, -1))
        End If
    Next i
    'reasign the array
    For i = 0 To UBound(sTemplates)
        sTemplates(i) = sTempArray(i)
        mnuSubTemplate(i).Caption = sTemplates(i)
        UpdateRegEntry "Template" & i, sTemplates(i), oCurUser.UserName
    Next i
   
End Sub

'
'Private Sub Timer1_Timer()
'If oContacts.CheckCount Then
'    Screen.MousePointer = vbHourglass
'    MsgBox "There has been a Contact Added by another User." & _
'    "The system will now refresh this will take a moment.", vbInformation, "Contact Added"
'    Timer1.Enabled = False
'    'oContacts.CreateOffLineDB oCurUser.UserName, oContacts.colContacts, False
'    Set oContacts.colContacts = Nothing
'    ShowContacts , False
'    Timer1.Enabled = True
'    Screen.MousePointer = vbDefault
'End If
'End Sub

Private Sub tmrStartup_Timer()
Static nCounter As Integer
    If nCounter = 0 Then
        DoEvents
        SetupGrid
        DoEvents
        ShowContacts
        DoEvents
    End If
    If nCounter = 10000 Then
        nCounter = 0
    Else
        nCounter = nCounter + 1
    End If
End Sub
Private Sub grdcontacts_KeyDown(KeyCode As Integer, Shift As Integer)
If KeyCode = 46 Then oAZ_FunctionButtonsClick 2
End Sub


'Private Sub mnuseedeleted_Click()
'mnuseedeleted.Checked = Not mnuseedeleted.Checked
'bSeeDeleted = mnuseedeleted.Checked
'fillgrid
'End Sub

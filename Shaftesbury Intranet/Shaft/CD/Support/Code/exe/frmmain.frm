VERSION 5.00
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Object = "{5E9E78A0-531B-11CF-91F6-C2863C385E30}#1.0#0"; "MSFLXGRD.OCX"
Object = "*\A..\ocx\ocx.vbp"
Begin VB.Form frmaddressbook 
   BackColor       =   &H00C0FFFF&
   Caption         =   " "
   ClientHeight    =   7590
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   12405
   ControlBox      =   0   'False
   LinkTopic       =   "Form1"
   ScaleHeight     =   7590
   ScaleWidth      =   12405
   StartUpPosition =   3  'Windows Default
   Begin MSComctlLib.StatusBar StatusBar1 
      Align           =   2  'Align Bottom
      Height          =   375
      Left            =   0
      TabIndex        =   23
      Top             =   7215
      Width           =   12405
      _ExtentX        =   21881
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
   Begin VB.PictureBox picpopup 
      BackColor       =   &H00FFC0C0&
      BorderStyle     =   0  'None
      FillColor       =   &H00FFC0C0&
      FillStyle       =   0  'Solid
      Height          =   1695
      Left            =   3495
      Picture         =   "frmmain.frx":0000
      ScaleHeight     =   1695
      ScaleWidth      =   255
      TabIndex        =   19
      Top             =   840
      Visible         =   0   'False
      Width           =   255
   End
   Begin VB.Timer tmrpopup 
      Enabled         =   0   'False
      Interval        =   200
      Left            =   1215
      Top             =   4440
   End
   Begin VB.Frame Fratoolbar 
      BackColor       =   &H00000000&
      BorderStyle     =   0  'None
      Height          =   615
      Index           =   0
      Left            =   0
      TabIndex        =   8
      Top             =   2130
      Width           =   12255
      Begin VB.CommandButton cmdfunctions 
         BackColor       =   &H00FFC0C0&
         Caption         =   "&Add"
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
         Index           =   0
         Left            =   3480
         Style           =   1  'Graphical
         TabIndex        =   16
         Top             =   120
         Width           =   735
      End
      Begin VB.CommandButton cmdfunctions 
         BackColor       =   &H00FFC0C0&
         Caption         =   "&Edit"
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
         Index           =   1
         Left            =   4200
         Style           =   1  'Graphical
         TabIndex        =   15
         Top             =   120
         Width           =   735
      End
      Begin VB.CommandButton cmdfunctions 
         BackColor       =   &H00FFC0C0&
         Caption         =   "&Search"
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
         Index           =   3
         Left            =   6360
         Style           =   1  'Graphical
         TabIndex        =   14
         Top             =   120
         Width           =   735
      End
      Begin VB.CommandButton cmdmovement 
         BackColor       =   &H00FFC0C0&
         Caption         =   "<"
         Height          =   375
         Index           =   0
         Left            =   120
         Style           =   1  'Graphical
         TabIndex        =   13
         Top             =   120
         Width           =   495
      End
      Begin VB.CommandButton cmdmovement 
         BackColor       =   &H00FFC0C0&
         Caption         =   ">"
         Height          =   375
         Index           =   1
         Left            =   11640
         Style           =   1  'Graphical
         TabIndex        =   12
         Top             =   120
         Visible         =   0   'False
         Width           =   495
      End
      Begin VB.CommandButton cmdfunctions 
         BackColor       =   &H00FFC0C0&
         Caption         =   "&Delete"
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
         Index           =   2
         Left            =   4920
         Style           =   1  'Graphical
         TabIndex        =   11
         Top             =   120
         Width           =   735
      End
      Begin VB.CommandButton cmdfunctions 
         BackColor       =   &H00FFC0C0&
         Caption         =   "&Exit"
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
         Left            =   7800
         Style           =   1  'Graphical
         TabIndex        =   10
         Top             =   120
         Width           =   735
      End
      Begin VB.CommandButton cmdfunctions 
         BackColor       =   &H00FFC0C0&
         Caption         =   "&Move"
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
         Left            =   7080
         Style           =   1  'Graphical
         TabIndex        =   9
         Top             =   120
         Width           =   735
      End
      Begin ocx.irCommandButton cmdletters 
         Height          =   375
         Index           =   25
         Left            =   11400
         TabIndex        =   17
         Top             =   120
         Width           =   255
         _ExtentX        =   450
         _ExtentY        =   661
         Caption         =   "A"
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         BackColor       =   12648447
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ButtonIcon      =   "frmmain.frx":0572
         AccessKey       =   " "
      End
      Begin VB.Label lblcurrentletter 
         Alignment       =   2  'Center
         BackColor       =   &H00C0FFFF&
         Caption         =   "A"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   15.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Left            =   5760
         TabIndex        =   18
         Top             =   120
         Width           =   495
      End
   End
   Begin VB.Timer tmrletters 
      Interval        =   500
      Left            =   1455
      Top             =   5280
   End
   Begin VB.Frame Fratoolbar 
      BackColor       =   &H00000000&
      BorderStyle     =   0  'None
      Height          =   615
      Index           =   1
      Left            =   135
      TabIndex        =   1
      Top             =   1320
      Width           =   12135
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
         TabIndex        =   7
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
         TabIndex        =   6
         Top             =   120
         Width           =   1335
      End
      Begin VB.CommandButton cmdTemplate 
         BackColor       =   &H00FFC0C0&
         Caption         =   "Fax"
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
         Index           =   7
         Left            =   3360
         Style           =   1  'Graphical
         TabIndex        =   4
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
         TabIndex        =   3
         Top             =   120
         Width           =   1335
      End
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
         TabIndex        =   2
         Top             =   120
         Width           =   1335
      End
   End
   Begin ocx.AZ oAZ 
      Height          =   615
      Left            =   15
      TabIndex        =   0
      Top             =   3000
      Width           =   12255
      _ExtentX        =   21616
      _ExtentY        =   1085
      BackColor       =   0
      ForeColor       =   -2147483630
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ButtonBackgroundColor=   16761024
   End
   Begin ocx.irCommandButton cmdtoolbar 
      Height          =   375
      Left            =   15
      TabIndex        =   20
      Top             =   0
      Width           =   375
      _ExtentX        =   661
      _ExtentY        =   661
      Caption         =   "<<"
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      BackColor       =   16761024
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      State           =   1
      ButtonIcon      =   "frmmain.frx":058E
      AccessKey       =   " "
   End
   Begin MSFlexGridLib.MSFlexGrid grdcontacts 
      Height          =   5175
      Left            =   1935
      TabIndex        =   21
      Top             =   1200
      Width           =   6495
      _ExtentX        =   11456
      _ExtentY        =   9128
      _Version        =   393216
      Rows            =   0
      FixedRows       =   0
      BackColorFixed  =   12632256
      BackColorBkg    =   16777215
      SelectionMode   =   1
      AllowUserResizing=   1
      BorderStyle     =   0
      Appearance      =   0
   End
   Begin VB.Label lblopoup 
      BackColor       =   &H00FFC0C0&
      Height          =   4455
      Left            =   3015
      TabIndex        =   22
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
      Begin VB.Menu mnuRepots 
         Caption         =   "Reports"
      End
      Begin VB.Menu mnuemail 
         Caption         =   "Email"
      End
      Begin VB.Menu mnuword 
         Caption         =   "Word Templates"
      End
      Begin VB.Menu mnulogon 
         Caption         =   "Logon"
      End
      Begin VB.Menu mnuAdmin 
         Caption         =   "Administartion"
      End
      Begin VB.Menu mnuOnOffline 
         Caption         =   "Online"
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
Dim nHeight As Double, nWidth As Double, nheightper As Double, nwidthper As Double
Dim iLastCardRow As Integer, iLastCardCol As Integer
Dim npage As Long, bAllShown As Boolean, batbeginning As Boolean
Dim nCurrentletter As Long
Dim CurrentGridView As ContactViews
Dim bmoveback As Boolean
Dim bdontrun As Boolean
Public bDontResize As Boolean
Dim bpopup As Boolean
Private Declare Function SetWindowLong Lib "user32" _
Alias "SetWindowLongA" _
(ByVal hwnd As Long, ByVal nIndex As Long, _
ByVal dwNewLong As Long) As Long
Dim bshrunk As Boolean
Private Declare Function SetWindowPos Lib "user32" _
(ByVal hwnd As Long, ByVal hWndInsertAfter _
As Long, ByVal X As Long, ByVal Y As Long, _
ByVal cx As Long, ByVal cy As Long, _
ByVal wFlags As Long) As Long
Private Enum eMovement
    efirst = 1
    eprev = 2
    enext = 3
    elast = 4
End Enum
Dim bDesc As Boolean
Private Enum eFunctions
    eAdd = 0
    eEdit = 1
    eDelete = 2
    eSearch = 3
    eClose = 5
    eMove = 4
End Enum
'Dim region As Long
Dim bshrink As Boolean


Private Sub cmdfunctions_Click(Index As Integer)
    If bpopup Then cmdtoolbar_Click
    Enabled = False
    With grdcontacts
        Select Case Index
            Case eAdd
                frmAdd.loadform False, , Me
            Case eEdit
                If .Row > 1 Then
                    frmAdd.loadform False, oContacts.Item(.RowData(.Row)), Me
                Else
                    MsgBox "Please choose a Contacts to Edit.", vbInformation
                End If
            Case eDelete
                If .Row > 1 Then
                    frmdel.loadform oContacts.Item(.RowData(.Row)), Me
                Else
                    frmdel.loadform , Me
                End If
            Case eSearch
                frmSearch.loadform Me
            Case eClose
                If MsgBox("Are you sure that you want to Close this application.", vbQuestion + vbYesNo) = vbYes Then Unload Me
            Case eMove
               If .Row > 1 Then
                frmMove.loadform oContacts.Item(.RowData(.Row)), Me
            Else
                frmMove.loadform , Me
            End If
        End Select
         Enabled = True
    End With
End Sub

Private Sub cmdletters_MouseMove(Index As Integer, Button As Integer, Shift As Integer, X As Single, Y As Single)
Static oldindex As Long

cmdletters(Index).ForeColor = vbRed
cmdletters(Index).Font.Bold = True
If oldindex <> Index Then
    cmdletters(oldindex).ForeColor = vbBlack
    cmdletters(oldindex).Font.Bold = False
End If
oldindex = Index
End Sub

Private Sub printletters()
Dim i As Long, nCurrentletter As Long, j As Long

bdontrun = True
nCurrentletter = Asc(oCurUser.Settings.CurrentLetter)
For i = 13 To 25
j = j + 1
    If nCurrentletter + j = 91 Then
        j = j - 26
    End If
    cmdletters(i).Caption = Chr$(nCurrentletter + j)
Next i
For i = 0 To 11
j = j + 1
    If nCurrentletter + j = 91 Then
        j = j - 26
    End If
     cmdletters(i).Caption = Chr$(nCurrentletter + j)
     
Next i
lblcurrentletter.Caption = oCurUser.Settings.CurrentLetter
bdontrun = False
End Sub

Private Sub cmdTemplate_Click(Index As Integer)
Dim oT As New clsTemplates
Dim names(5) As String
Dim oThisContact As Object

If grdcontacts.Row > 1 Then
With oT
    Set oThisContact = oContacts.Item(grdcontacts.RowData(grdcontacts.Row))
    .TemplatePath = oCurUser.Settings.TemplatePath
    CreateRegEntry "CurUser", oCurUser.UserName, REG_SZ, ""
    CreateRegEntry "CurUserPWD", oCurUser.pwd, REG_SZ, ""
    Select Case cmdTemplate(Index).Caption
    Case "Letter"
      '  CreateRegEntry "let_theirref", oThisContact.ref, REG_SZ, oCurUser.UserName & "\TemplateDtls\"
        CreateRegEntry "let_name", oThisContact.fullname, REG_SZ, oCurUser.UserName & "\TemplateDtls\"
        CreateRegEntry "company", oThisContact.CompanyName, REG_SZ, oCurUser.UserName & "\TemplateDtls\"
        CreateRegEntry "address", oThisContact.BusinessAddress, REG_SZ, oCurUser.UserName & "\TemplateDtls\"
        CreateRegEntry "dear", oThisContact.userproperties("Salutation"), REG_SZ, oCurUser.UserName & "\TemplateDtls\"
        .RunTemplate "plc_let.dot"
    Case "Fax"
        CreateRegEntry "fax_name", oThisContact.userproperties("Salutation"), REG_SZ, oCurUser.UserName & "\TemplateDtls\"
        CreateRegEntry "fax_company", oThisContact.userproperties("Salutation"), REG_SZ, oCurUser.UserName & "\TemplateDtls\"
        'CreateRegEntry "fax_theirref", oThisContact.userproperties("Salutation"), REG_SZ, oCurUser.UserName & "\TemplateDtls\"
        CreateRegEntry "their_fax", oThisContact.userproperties("Salutation"), REG_SZ, oCurUser.UserName & "\TemplateDtls\"
        .RunTemplate "Plc_fax.dot"
    Case "Memo"
        CreateRegEntry "To", oThisContact.fullname, REG_SZ, oCurUser.UserName & "\TemplateDtls\"
        .RunTemplate "Plc_memo.dot"
    End Select
    
End With
End If
End Sub

Private Sub cmdtoolbar_Click()
Dim i As Long, ctrl As Control

Select Case cmdtoolbar.Caption
Case ">>"

Case Else
    Visible = False
    bDontResize = True
    With oCurUser.Settings
        .ContactsScreenLeft = Left
        .ContactsScreenTop = Top
    End With
    WindowState = vbNormal
    BorderStyle = 0
    Caption = ""
    On Error Resume Next
   ' grdData(0).Visible = False
    StatusBar1.Visible = False
    cmdtoolbar.Visible = False
    For i = 1 To cmdletters.UBound
        cmdletters(i).Visible = False
    Next i
    bshrunk = True
    lblopoup.Visible = True
    picpopup.Visible = True
    lblopoup.Move 0, 0, 375, Screen.Height
    picpopup.Move 0, (Screen.Height - picpopup.Height) / 2, picpopup.Width, picpopup.Height
    bDontResize = True
    Move 0, 0, 60, Screen.Height
    bDontResize = False
    picpopup.ZOrder
    lblopoup.ZOrder
     Visible = True
    'SetWindowPos hwnd, HWND_TOPMOST, 0, 0, 0, 0, 0
End Select


bDontResize = False
End Sub


Private Sub Form_Load()

    Hide
    mnuviewtype(3).Checked = True
    'CurrentGridView = GridView
    Caption = LoadResString(102)
    StatusBar1.Panels.Add 1
    bDontResize = True
     Height = IIf(oCurUser.Settings.ContactsScreenHeight = 0, Screen.Height, oCurUser.Settings.ContactsScreenHeight)
    Width = IIf(oCurUser.Settings.ContactsScreenWidth = 0, Screen.Width, oCurUser.Settings.ContactsScreenWidth)
    Left = oCurUser.Settings.ContactsScreenLeft
    Top = oCurUser.Settings.ContactsScreenTop
    bDontResize = False
     oCurUser.Settings.CurrentLetter = "A"
    printletters
    ShowContacts oCurUser.Settings.CurrentLetter
    SwitchViews
    optMode_Click (1)
    batbeginning = True
    TX = Screen.TwipsPerPixelX
    TY = Screen.TwipsPerPixelY
    
    nCurrentletter = 0
    position
    mnuAdmin.Visible = (oCurUser.rights = "1")
    'lblcurrentletter_Click
    Show
End Sub

Private Sub SwitchViews()
Dim i As Long

'fraadmin(0).Visible = False
For i = 0 To Fratoolbar.UBound
    Fratoolbar(i).Visible = True
Next i
mnuAdmin.Checked = False

    
Select Case oCurUser.Settings.ContactsView
    Case CardView
'        setupcards
'       ' fillgrid
'        mnucolumns.Visible = False
'        mnusortby.Visible = False
'        mnuviews(CardView).Checked = True
'        mnuviews(GridView).Checked = False
''        With grdData(0)
''            .Width = 6480
''            .Left = shppage(0).Left
''            .GridLines = flexGridFlat
''            .GridLinesFixed = flexGridFlat
''            .GridColor = vbWhite
''            .GridColorFixed = vbWhite
''            .BackColorFixed = vbWhite
''            .ScrollBars = flexScrollBarNone
''        End With
'        For i = 1 To cmdletters.UBound
'            If i <> 12 Then cmdletters(i).Visible = True
'        Next i
'        position
'        grdcontacts.Visible = False
    Case Else
        position
        'FillGrid2
        For i = 0 To oCurUser.Settings.Columns.Count - 1
        On Error Resume Next
            Load mnufields(i)
            mnufields(i).Caption = oCurUser.Settings.Columns(i + 1).Name
            mnufields(i).Checked = (oCurUser.Settings.Columns(i + 1).Width > 0)
            Load mnufeilds(i)
            mnufeilds(i).Caption = oCurUser.Settings.Columns(i + 1).Name
            mnufeilds(i).Checked = IIf(i = oCurUser.Settings.ContactsGridViewSortedBy, True, False)
        Next i
        On Error GoTo 0
        mnucolumns.Visible = True
        mnusortby.Visible = True
        mnuviews(CardView).Checked = False
        mnuviews(GridView).Checked = True
       ' grdData(0).Move shppage(0).Left, shppage(0).Top, shppage(0).Width * 2, shppage(0).Height - 25
        'frarecord.Visible = False
'        With grdData(0)
'            '.Width = 6480
'            .GridLines = flexGridFlat
'            .GridLinesFixed = flexGridFlat
'            .GridColor = vbBlack
'            .GridColorFixed = vbBlack
'            .BackColorFixed = &HE0E0E0
'        End With
    grdcontacts.Visible = True

End Select


End Sub

Private Sub Form_MouseDown(Button As Integer, Shift As Integer, X As Single, Y As Single)
If Button = vbRightButton Then PopupMenu mnugridoptions
End Sub

Private Sub Form_Resize()
    On Error Resume Next
    If Not bDontResize Then
        Visible = False
        mnuviews(3).Checked = False
        If Width < Fratoolbar(0).Width + (cmdtoolbar.Width * 2) Then bDontResize = True: Width = Fratoolbar(0).Width + (cmdtoolbar.Width * 2)
        If Height < 6500 Then bDontResize = True: Height = 6500
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
    End With
    If oCurUser.Settings.SaveSettings(oCurUser.UserName) <> 0 Then
        MsgBox "There was an error saving your personalised settings."
    End If
    Set oContacts = Nothing
    CreateRegEntry "CurUser", "", REG_SZ, ""
    CreateRegEntry "CurUserPWD", "", REG_SZ, ""
    Unload Me
End Sub

Private Sub fraadmin_MouseDown(Index As Integer, Button As Integer, Shift As Integer, X As Single, Y As Single)
If Button = vbRightButton Then PopupMenu mnugridoptions
End Sub

Private Sub grdcontacts_MouseDown(Button As Integer, Shift As Integer, X As Single, Y As Single)
'If Button = 2 Then grdcontacts_MouseDown 1, Shift, x, y
Form_MouseDown Button, Shift, X, Y
End Sub

Private Sub lblcurrentletter_Change()
    If Not bdontrun Then
     oCurUser.Settings.CurrentLetter = lblcurrentletter.Caption
    ShowContacts oCurUser.Settings.CurrentLetter
End If
End Sub

Public Function ShowContacts(Optional sLetter As String) As Boolean
Dim i As Long, j As Long

    If oContacts Is Nothing Then Set oContacts = New clsContact
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
    oContacts.GetContacts mnufeilds(oCurUser.Settings.ContactsGridViewSortedBy).Caption, bDesc, sLetter
    fillgrid
    ShowContacts = True
End Function



Private Sub setupcards()

End Sub

Public Sub lblcurrentletter_Click()
Screen.MousePointer = vbHourglass
lblcurrentletter_Change
Screen.MousePointer = vbDefault
End Sub

Private Sub lblopoup_Click()
enlargeform
End Sub

Private Sub enlargeform()
Dim i As Long
    Visible = False
   ' SetWindowPos hwnd, HWND_NOTOPMOST, 0, 0, 0, 0, 0
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
    Fratoolbar(0).Visible = True
    For i = 1 To cmdletters.UBound
       If i <> 12 Then cmdletters(i).Visible = True
    Next i
    'Shape1(2).Visible = True
   ' grdData(0).Visible = True
    StatusBar1.Visible = True
    cmdtoolbar.Visible = True
    position
    bpopup = False
    Visible = True
End Sub
Private Sub lblopoup_MouseMove(Button As Integer, Shift As Integer, X As Single, Y As Single)

    bDontResize = True
    Move 0, 0, cmdletters(1).Width * 1.5, Height
    bDontResize = False
    bpopup = True
    tmrpopup.Enabled = True
End Sub

Private Sub cmdLetters_Click(Index As Integer)
On Error Resume Next
    Static nOldbutton As Long
    Screen.MousePointer = vbHourglass
    If bpopup Then cmdtoolbar_Click
    nOldbutton = Index
  
  '  grdData(0).Clear
    'If oContacts.FindContact("fileas", cmdletters(Index).Caption) Then
     If oContacts.GetContacts("fileas", False, cmdletters(Index).Caption) Then


    End If
    nCurrentletter = Asc(cmdletters(Index).Caption)
    fillgrid
     oCurUser.Settings.CurrentLetter = cmdletters(Index).Caption
    oCurUser.Settings.CurrentLetter = cmdletters(Index).Caption
    printletters
    Screen.MousePointer = vbNormal
End Sub

Private Sub LoadControls(nooffunctions As Long)
Dim i As Long, ntop As Long

On Error Resume Next
'    Load grdData(1)
'    grdData(1).Visible = True
'    For i = 1 To 26
'        cmdletters(i).Visible = False
'        Load cmdletters(i)
'    Next i
'        shppage.Visible = True
'        shppage.BackColor = 0
'        shppage.BorderStyle = 4
'    For i = 1 To 4
'        If i < 3 Then Load cmdmovement(i)
'       ' Load picring(i)
'    Next i
'    For i = 1 To nooffunctions
'        Load cmdfunctions(i)
'    Next i
    
'    ntop = (shppage(0).Top + (shppage(0).Height / 2)) - (optmode(0).Height / 1.5)
'    For i = 1 To 6
'        Load optmode(i)
'        With optmode(i)
'            .Visible = False
'            .Move shppage(0).Left - (optmode(0).Width / 2), ntop, .Width, .Height
'            .ZOrder
'            ntop = ntop + .Height
'            Select Case i
'                Case 1: .Picture = LoadResPicture(101, 0)
'                Case 2: .Picture = LoadResPicture(102, 0)
'                Case 3: .Picture = LoadResPicture(106, 0)
'                Case 4: .Picture = LoadResPicture(103, 0)
'                Case 5: .Picture = LoadResPicture(104, 0)
'                Case 6: .Picture = LoadResPicture(105, 0)
'            End Select
'        End With
'    Next i
End Sub
Private Sub position()
Dim ntop As Long, i As Long, nleft As Long
Const nNoofFUnctions = 5
On Error Resume Next
    
    Visible = False
    LoadControls (nNoofFUnctions)
    StatusBar1.Panels(1).Width = StatusBar1.Width / 3
    StatusBar1.Panels(2).Width = (StatusBar1.Width / 3) * 3
    With grdcontacts
        .Width = Width - (cmdmovement(1).Width / 1.9)
        .Height = Height - (StatusBar1.Height + (Fratoolbar(0).Height * 1.75))
        .Left = cmdmovement(1).Width / 4
        .Top = 0
    End With
    For i = 0 To Fratoolbar.UBound
        Fratoolbar(i).Top = (ScaleTop + ScaleHeight) - (StatusBar1.Height + (Fratoolbar(i).Height))
        Fratoolbar(i).Left = (Width - Fratoolbar(i).Width) / 2
    Next i
    Fratoolbar(0).ZOrder
   cmdtoolbar.Move cmdmovement(1).Width / 3, Fratoolbar(0).Top, cmdtoolbar.Width, cmdtoolbar.Height
   cmdtoolbar.ZOrder
   Visible = True
End Sub

Private Sub grdcontacts_DblClick()
If grdcontacts.Row > 1 Then
    frmAdd.loadform True, oContacts.Item(grdcontacts.RowData(grdcontacts.Row))
End If

End Sub

Private Sub mnuAdmin_Click()
Dim i As Long

If oCurUser.rights <> "1" Then Exit Sub
For i = 1 To 2
    mnuviews(i).Checked = False
Next i
'mnuAdmin.Checked = Not mnuAdmin.Checked

Enabled = False
frmadm.Show , Me
'For i = 0 To Fratoolbar.UBound
'    Fratoolbar(i).Visible = False
'Next i

End Sub

Private Sub mnuemail_Click()
If oCurUser.Settings.ContactsView = GridView Then
    If grdcontacts.Row > 1 Then
        If oContacts.Item(grdcontacts.RowData(grdcontacts.Row)).Email1Address <> "" Then
            oContacts.DisplayEmailObject oContacts.Item(grdcontacts.RowData(grdcontacts.Row)).Email1Address
        Else
            MsgBox "This Contact does not have an Email address."
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
    'FillGrid2
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
Unload Me
UpdateRegEntry "lastuser", ""
Set oCurUser = New clsContact
    FrmLogin.Show
End Sub

Private Sub mnuOnOffline_Click()
    OnLineOffline
End Sub

Private Sub mnuRepots_Click()
frmreports.loadform Me
End Sub

Private Sub mnuviews_Click(Index As Integer)
    If Index <> 3 Then
        oCurUser.Settings.ContactsView = Index
        SwitchViews
    Else
        Visible = False
        Top = 0
        Left = 0
        Height = Screen.Height
        Width = Screen.Width
        Visible = True
        If Not mnuviews(3).Checked Then mnuviews(3).Checked = True
    End If
End Sub

Private Sub optMode_Click(Index As Integer)
On Error Resume Next
Dim i As Long

'frarecord.Visible = Not (Index = 3)
Select Case Index
Case 1
    StatusBar1.Panels(1).Text = "Address Book - Global Address Book for " & oCurUser.UserName & "."
    Fratoolbar(0).ZOrder
Case 2
    StatusBar1.Panels(1).Text = "Address Book - Private Address Book for " & oCurUser.UserName & "."
    Fratoolbar(0).ZOrder
Case 3
    StatusBar1.Panels(1).Text = "Address Book - Options for " & oCurUser.UserName & "."
    Fratoolbar(0).ZOrder
    'grdData(0).Clear
Case 6:    Fratoolbar(1).ZOrder
End Select
 
    For i = 1 To 26
        cmdletters(i).Visible = Not (Index = 3)
        cmdmovement(i).Visible = Not (Index = 3)
        cmdfunctions(i).Enabled = Not (Index = 3)
    Next i
End Sub

Private Sub DisplayCount(nPrivate As Long, nPublic As Long)

    Select Case oContacts.contype
        Case AllExchange: StatusBar1.Panels.Item(2).Text = nPrivate & " Private Contact(s) and " & nPublic & " Company Contact(s)."
        Case OutlookPrivate: StatusBar1.Panels.Item(2).Text = nPrivate & " Outlook Contact(s)."
    End Select
End Sub
Public Sub fillgrid()
 Dim oContact As Object, sNTlogon As String, ncount1 As Long, ncount2 As Long
  Dim i As Long, contype As ContactType, nConCount As Long, cols() As String, j As Long
  Dim sRow As String, bfoundsome As Boolean, k As Long
  
  contype = oContacts.contype
  Debug.Print Time & " Filling Grid"
    With grdcontacts
        Screen.MousePointer = vbHourglass
            .BackColorFixed = &HFFC0C0
            .BackColorBkg = &HC0FFFF
            .cols = GetCols(oCurUser.Settings.AvalibleFields, cols()) + 1
            .Rows = 0
            .FixedCols = 1
            .AddItem Replace(oCurUser.Settings.AvalibleFields, "|", vbTab)
            For i = 1 To .cols - 1
                .ColWidth(i - 1) = oCurUser.Settings.Columns(i).Width
            Next i
            .ColWidth(i - 1) = 50000
            .AddItem ""
         nConCount = oContacts.Count
         If nConCount > 0 Then
            .Redraw = False
            For i = 1 To nConCount
                Set oContact = oContacts.Item(i)
                 On Error Resume Next
                sNTlogon = ""
                sNTlogon = oContact.user1
                If (sNTlogon = oCurUser.UserName And mnuviewtype(1).Checked Or (sNTlogon = oCurUser.UserName And mnuviewtype(3).Checked)) Or (sNTlogon = "" And mnuviewtype(2).Checked) Or (sNTlogon = "" And mnuviewtype(3).Checked) Then
                        sRow = ""
                         For j = 0 To oCurUser.Settings.Columns.Count
                            sRow = sRow & oContacts.ContactPropertyValue(oContact, oCurUser.Settings.Columns(j + 1).Name) & vbTab
                        Next j
                        .AddItem sRow
                        .RowData(.Rows - 1) = i
                        .Row = .Rows - 1
                        For k = 0 To .cols - 1
                            .Col = k
                            .CellForeColor = IIf(sNTlogon = "", vbRed, vbBlack)
                        Next k
                    If contype = OutlookPrivate Then
                        ncount1 = ncount1 + 1
                    ElseIf contype = AllExchange Then
                        If sNTlogon = "" Then
                            ncount2 = ncount2 + 1
                        Else
                            ncount1 = ncount1 + 1
                        End If
                    End If
                 End If
                Err.Clear
            Next i
            DisplayCount ncount1, ncount2
        
        End If
        .FixedRows = 1
errcode:
    On Error GoTo 0
        .Redraw = True
        Screen.MousePointer = vbDefault
    End With
    
     Debug.Print Time & " Finished"
End Sub



Private Sub grddata_Click(Index As Integer)

' If grdData(0).CellForeColor = &HFFFFFF Then Exit Sub
' Do While grdData(0).CellForeColor <> &HFFFFFF
'     If Not (grdData(0).Row + 1 > (grdData(0).Rows - 1)) Then
'        grdData(0).Row = grdData(0).Row + 1
'    End If
' Loop
'    If grdData(0).Clip <> "" Then
'        populaterecord grdData(0).Clip
'        Shadow
'    End If
End Sub

Private Sub mnuviewtype_Click(Index As Integer)
Dim i As Integer
For i = 1 To 3
    mnuviewtype(i).Checked = (i = Index)
Next i
ShowContacts lblcurrentletter.Caption
End Sub

Private Sub mnuword_Click()
Fratoolbar(1).Visible = True
Fratoolbar(1).ZOrder
End Sub

Private Sub picpopup_Click()
enlargeform
End Sub

Private Sub picpopup_MouseMove(Button As Integer, Shift As Integer, X As Single, Y As Single)
    bDontResize = True
    Move 0, 0, cmdletters(1).Width * 1.5, Height
    bDontResize = False
    bpopup = True
    tmrpopup.Enabled = True
End Sub

'Private Sub tabAdmin_Click()
'Dim oUsers As New clsContact
'Dim i As Long
'
'fraadmin(tabAdmin.SelectedItem.Index).ZOrder
'Select Case tabAdmin.SelectedItem.Index
'Case 1
'    lstusers.Clear
'    If oUsers.Load Then
'        For i = 1 To oUsers.Count
'            lstusers.AddItem oUsers.Item(i).Uname
'            lstusers.ItemData(lstusers.NewIndex) = oUsers.Item(i).ID
'        Next i
'    End If
'Case 2
'
'End Select
'End Sub

Private Sub tabAdmin_MouseDown(Button As Integer, Shift As Integer, X As Single, Y As Single)
If Button = vbRightButton Then PopupMenu mnugridoptions
End Sub

Private Sub tmrletters_Timer()
Dim i As Long
'    For i = 0 To 25
'        cmdletters(i).Font.Bold = False
'        cmdletters(i).ForeColor = vbBlack
'    Next i
End Sub

Private Sub tmrpopup_Timer()
Dim pt As POINTAPI

    If bpopup Then
        GetCursorPos pt
        If pt.X > (Left + Width) / TX Then
            tmrpopup.Enabled = True
            bDontResize = True
            cmdtoolbar.Caption = "<<"
            cmdtoolbar_Click
            bDontResize = False
            cmdtoolbar.Height = cmdtoolbar.Height / 3
            tmrpopup.Enabled = False
        End If
    End If

End Sub



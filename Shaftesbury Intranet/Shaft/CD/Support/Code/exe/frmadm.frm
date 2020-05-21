VERSION 5.00
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "Mscomctl.ocx"
Begin VB.Form frmadm 
   BorderStyle     =   4  'Fixed ToolWindow
   Caption         =   "Adminisitration"
   ClientHeight    =   5145
   ClientLeft      =   45
   ClientTop       =   285
   ClientWidth     =   10080
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   5145
   ScaleWidth      =   10080
   ShowInTaskbar   =   0   'False
   StartUpPosition =   1  'CenterOwner
   Begin VB.CommandButton cmdapplyconfig 
      Caption         =   "&Apply"
      Enabled         =   0   'False
      Height          =   375
      Left            =   7200
      TabIndex        =   43
      Top             =   4200
      Width           =   1095
   End
   Begin VB.CommandButton cmdaddresses 
      Caption         =   "Import Addresses"
      Height          =   615
      Left            =   8520
      TabIndex        =   17
      Top             =   2220
      Width           =   1455
   End
   Begin VB.CommandButton cmdusers 
      Caption         =   "Import Users"
      Height          =   615
      Left            =   8520
      TabIndex        =   16
      Top             =   1350
      Width           =   1455
   End
   Begin VB.CommandButton cmdoffline 
      Caption         =   "Create an Offline MDB"
      Height          =   615
      Left            =   8520
      TabIndex        =   15
      Top             =   480
      Visible         =   0   'False
      Width           =   1455
   End
   Begin VB.Frame fraadmin 
      BorderStyle     =   0  'None
      Caption         =   "Frame1"
      Height          =   4095
      Index           =   3
      Left            =   240
      TabIndex        =   38
      Top             =   600
      Width           =   8055
      Begin VB.CheckBox Check2 
         Caption         =   "Auto Highlight Text Boxes"
         Height          =   495
         Left            =   3840
         TabIndex        =   55
         Top             =   3240
         Width           =   2055
      End
      Begin VB.TextBox txtweb 
         BorderStyle     =   0  'None
         Height          =   255
         Left            =   3960
         TabIndex        =   42
         Top             =   480
         Width           =   3855
      End
      Begin VB.ListBox lstcolumns 
         Appearance      =   0  'Flat
         Height          =   3180
         Left            =   120
         Style           =   1  'Checkbox
         TabIndex        =   39
         Top             =   480
         Width           =   3255
      End
      Begin Contacts.ColorPicker colCompany 
         Height          =   375
         Left            =   5880
         TabIndex        =   44
         Top             =   1920
         Width           =   2055
         _ExtentX        =   3625
         _ExtentY        =   661
      End
      Begin Contacts.ColorPicker ColBackGround 
         Height          =   375
         Left            =   5880
         TabIndex        =   45
         Top             =   1440
         Width           =   2055
         _ExtentX        =   3625
         _ExtentY        =   661
      End
      Begin Contacts.ColorPicker colPrivate 
         Height          =   375
         Left            =   5880
         TabIndex        =   46
         Top             =   2400
         Width           =   2055
         _ExtentX        =   3625
         _ExtentY        =   661
      End
      Begin Contacts.ColorPicker colGridBorders 
         Height          =   375
         Left            =   5880
         TabIndex        =   47
         Top             =   2880
         Width           =   2055
         _ExtentX        =   3625
         _ExtentY        =   661
      End
      Begin Contacts.ColorPicker colGridBack 
         Height          =   375
         Left            =   5880
         TabIndex        =   48
         Top             =   960
         Width           =   2055
         _ExtentX        =   3625
         _ExtentY        =   661
      End
      Begin VB.Label lblColours 
         Caption         =   "Grid Borders:"
         Height          =   255
         Index           =   4
         Left            =   3960
         TabIndex        =   53
         Top             =   2880
         Width           =   1815
      End
      Begin VB.Label lblColours 
         Caption         =   "Private Contacts:"
         Height          =   255
         Index           =   3
         Left            =   3960
         TabIndex        =   52
         Top             =   2400
         Width           =   1815
      End
      Begin VB.Label lblColours 
         Caption         =   "Company Contacts:"
         Height          =   255
         Index           =   2
         Left            =   3960
         TabIndex        =   51
         Top             =   1920
         Width           =   1815
      End
      Begin VB.Label lblColours 
         Caption         =   "Back Colour:"
         Height          =   255
         Index           =   1
         Left            =   3960
         TabIndex        =   50
         Top             =   1440
         Width           =   1815
      End
      Begin VB.Label lblColours 
         Caption         =   "Grid Back Colour:"
         Height          =   255
         Index           =   0
         Left            =   3960
         TabIndex        =   49
         Top             =   960
         Width           =   1815
      End
      Begin VB.Label Label2 
         Caption         =   "Default Web Page:"
         Height          =   495
         Left            =   3960
         TabIndex        =   41
         Top             =   240
         Width           =   2895
      End
      Begin VB.Label Label1 
         Caption         =   "Columns Order and Visability:"
         Height          =   255
         Left            =   120
         TabIndex        =   40
         Top             =   240
         Width           =   2535
      End
   End
   Begin VB.Frame fraadmin 
      BorderStyle     =   0  'None
      Caption         =   "Currently Available Templates:"
      Height          =   4095
      Index           =   2
      Left            =   360
      TabIndex        =   0
      Top             =   600
      Width           =   7935
      Begin VB.CommandButton Command1 
         Caption         =   "Clear Template History"
         Height          =   255
         Left            =   5760
         TabIndex        =   37
         Top             =   960
         Width           =   1935
      End
      Begin VB.CommandButton cmdnetwork 
         Caption         =   "..."
         Height          =   255
         Index           =   1
         Left            =   5160
         TabIndex        =   35
         Top             =   600
         Width           =   495
      End
      Begin VB.CommandButton cmdnetwork 
         Caption         =   "..."
         Height          =   255
         Index           =   0
         Left            =   5160
         TabIndex        =   34
         Top             =   240
         Width           =   495
      End
      Begin VB.CommandButton cmdtemplate 
         Caption         =   "..."
         Height          =   255
         Left            =   5160
         TabIndex        =   33
         Top             =   960
         Width           =   495
      End
      Begin VB.TextBox txtconfigdtls 
         Appearance      =   0  'Flat
         Height          =   285
         Index           =   2
         Left            =   1680
         Locked          =   -1  'True
         TabIndex        =   31
         Top             =   960
         Width           =   3375
      End
      Begin VB.TextBox txtconfigdtls 
         Appearance      =   0  'Flat
         Height          =   285
         Index           =   1
         Left            =   1680
         Locked          =   -1  'True
         TabIndex        =   29
         Top             =   600
         Width           =   2655
      End
      Begin VB.TextBox txtconfigdtls 
         Appearance      =   0  'Flat
         Height          =   285
         Index           =   0
         Left            =   1680
         Locked          =   -1  'True
         TabIndex        =   27
         Top             =   240
         Width           =   2655
      End
      Begin VB.Label lblconfigdtls 
         Caption         =   "Templates Path:"
         Height          =   255
         Index           =   2
         Left            =   120
         TabIndex        =   32
         Top             =   960
         Width           =   1575
      End
      Begin VB.Label lblconfigdtls 
         Caption         =   "PDC Server:"
         Height          =   255
         Index           =   1
         Left            =   120
         TabIndex        =   30
         Top             =   600
         Width           =   1575
      End
      Begin VB.Label lblconfigdtls 
         Caption         =   "Exchange Server:"
         Height          =   255
         Index           =   0
         Left            =   120
         TabIndex        =   28
         Top             =   240
         Width           =   1575
      End
   End
   Begin VB.Frame fraadmin 
      BorderStyle     =   0  'None
      Caption         =   "Author Name:Author Name:"
      Height          =   4095
      Index           =   1
      Left            =   360
      TabIndex        =   13
      Top             =   600
      Width           =   7935
      Begin VB.CheckBox chkseeall 
         Caption         =   "Allow See All"
         Enabled         =   0   'False
         Height          =   375
         Left            =   4800
         TabIndex        =   54
         Top             =   3240
         Width           =   1215
      End
      Begin VB.CheckBox Check1 
         Caption         =   "Administrator"
         Enabled         =   0   'False
         Height          =   195
         Left            =   4800
         TabIndex        =   36
         Top             =   3000
         Width           =   1215
      End
      Begin VB.CommandButton cmduseroptions 
         Caption         =   "&Delete User"
         Height          =   315
         Index           =   2
         Left            =   6600
         TabIndex        =   11
         Top             =   3720
         Width           =   1215
      End
      Begin VB.CommandButton cmduseroptions 
         Caption         =   "&Edit User"
         Height          =   315
         Index           =   1
         Left            =   5280
         TabIndex        =   10
         Top             =   3720
         Width           =   1215
      End
      Begin VB.CommandButton cmduseroptions 
         Caption         =   "&Add User"
         Height          =   315
         Index           =   0
         Left            =   3960
         TabIndex        =   9
         Top             =   3720
         Width           =   1215
      End
      Begin VB.TextBox txtuserdtls 
         Appearance      =   0  'Flat
         Height          =   285
         Index           =   7
         Left            =   4800
         Locked          =   -1  'True
         TabIndex        =   8
         Top             =   2640
         Width           =   3015
      End
      Begin VB.TextBox txtuserdtls 
         Appearance      =   0  'Flat
         Height          =   285
         Index           =   6
         Left            =   4800
         Locked          =   -1  'True
         TabIndex        =   7
         Top             =   2280
         Width           =   3015
      End
      Begin VB.TextBox txtuserdtls 
         Appearance      =   0  'Flat
         Height          =   285
         Index           =   5
         Left            =   4800
         Locked          =   -1  'True
         TabIndex        =   6
         Top             =   1920
         Width           =   3015
      End
      Begin VB.TextBox txtuserdtls 
         Appearance      =   0  'Flat
         Height          =   285
         Index           =   4
         Left            =   4800
         Locked          =   -1  'True
         TabIndex        =   5
         Top             =   1560
         Width           =   3015
      End
      Begin VB.TextBox txtuserdtls 
         Appearance      =   0  'Flat
         Height          =   285
         Index           =   3
         Left            =   4800
         Locked          =   -1  'True
         TabIndex        =   4
         Top             =   1200
         Width           =   3015
      End
      Begin VB.TextBox txtuserdtls 
         Appearance      =   0  'Flat
         Height          =   285
         Index           =   2
         Left            =   4800
         Locked          =   -1  'True
         TabIndex        =   3
         Top             =   840
         Width           =   3015
      End
      Begin VB.TextBox txtuserdtls 
         Appearance      =   0  'Flat
         Height          =   285
         Index           =   1
         Left            =   4800
         Locked          =   -1  'True
         TabIndex        =   2
         Top             =   480
         Width           =   3015
      End
      Begin VB.TextBox txtuserdtls 
         Appearance      =   0  'Flat
         Height          =   285
         Index           =   0
         Left            =   4800
         Locked          =   -1  'True
         TabIndex        =   1
         Top             =   120
         Width           =   3015
      End
      Begin VB.ListBox lstusers 
         Appearance      =   0  'Flat
         Height          =   3345
         Left            =   0
         Sorted          =   -1  'True
         TabIndex        =   14
         Top             =   120
         Width           =   3135
      End
      Begin VB.Label lblUsersCount 
         Caption         =   "Label1"
         Height          =   255
         Left            =   0
         TabIndex        =   26
         Top             =   3600
         Width           =   3135
      End
      Begin VB.Label lblUserdtls 
         Caption         =   "Author Initials:"
         Height          =   375
         Index           =   7
         Left            =   3360
         TabIndex        =   25
         Top             =   2640
         Width           =   1335
      End
      Begin VB.Label lblUserdtls 
         Caption         =   "Author Title:"
         Height          =   375
         Index           =   6
         Left            =   3360
         TabIndex        =   24
         Top             =   2280
         Width           =   1335
      End
      Begin VB.Label lblUserdtls 
         Caption         =   "Author Name:"
         Height          =   375
         Index           =   5
         Left            =   3360
         TabIndex        =   23
         Top             =   1920
         Width           =   1335
      End
      Begin VB.Label lblUserdtls 
         Caption         =   "Letter User Name:"
         Height          =   375
         Index           =   4
         Left            =   3360
         TabIndex        =   22
         Top             =   1560
         Width           =   1335
      End
      Begin VB.Label lblUserdtls 
         Caption         =   "Client Name:"
         Height          =   255
         Index           =   3
         Left            =   3360
         TabIndex        =   21
         Top             =   1200
         Width           =   975
      End
      Begin VB.Label lblUserdtls 
         Caption         =   "User Ref:"
         Height          =   255
         Index           =   2
         Left            =   3360
         TabIndex        =   20
         Top             =   840
         Width           =   975
      End
      Begin VB.Label lblUserdtls 
         Caption         =   "Password:"
         Height          =   255
         Index           =   1
         Left            =   3360
         TabIndex        =   19
         Top             =   480
         Width           =   975
      End
      Begin VB.Label lblUserdtls 
         Caption         =   "User Name:"
         Height          =   255
         Index           =   0
         Left            =   3360
         TabIndex        =   18
         Top             =   120
         Width           =   975
      End
   End
   Begin MSComctlLib.TabStrip tabAdmin 
      Height          =   4575
      Left            =   120
      TabIndex        =   12
      Top             =   240
      Width           =   8295
      _ExtentX        =   14631
      _ExtentY        =   8070
      MultiRow        =   -1  'True
      HotTracking     =   -1  'True
      _Version        =   393216
      BeginProperty Tabs {1EFB6598-857C-11D1-B16A-00C0F0283628} 
         NumTabs         =   3
         BeginProperty Tab1 {1EFB659A-857C-11D1-B16A-00C0F0283628} 
            Caption         =   "Users"
            Key             =   "Users"
            Object.ToolTipText     =   "Add/Edit & Delete Users"
            ImageVarType    =   2
         EndProperty
         BeginProperty Tab2 {1EFB659A-857C-11D1-B16A-00C0F0283628} 
            Caption         =   "Configuration"
            Key             =   "Configuration"
            ImageVarType    =   2
         EndProperty
         BeginProperty Tab3 {1EFB659A-857C-11D1-B16A-00C0F0283628} 
            Caption         =   "Appearance"
            Key             =   "Appearance"
            Object.ToolTipText     =   "Appearance"
            ImageVarType    =   2
         EndProperty
      EndProperty
   End
End
Attribute VB_Name = "frmadm"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Dim oUsers As New clsContact
Dim UserbEdit As Boolean
Dim pcSelectedItem As String, pnSelectedIndex As Integer, pnDestinationIndex As Integer, plSelected As Boolean
Dim nitemdata As Long, bcolchanged As Boolean

Private Sub cmdIE_Click()
'FrmStart.Show
End Sub

Private Sub Check2_Click()
cmdapplyconfig.Enabled = True
End Sub

Private Sub ColBackGround_ColorChange(SelectedColor As stdole.OLE_COLOR)
cmdapplyconfig.Enabled = True
End Sub

Private Sub colCompany_ColorChange(SelectedColor As stdole.OLE_COLOR)
cmdapplyconfig.Enabled = True
End Sub

Private Sub colGridBack_ColorChange(SelectedColor As stdole.OLE_COLOR)
cmdapplyconfig.Enabled = True
End Sub

Private Sub colGridBorders_ColorChange(SelectedColor As stdole.OLE_COLOR)
cmdapplyconfig.Enabled = True
End Sub

Private Sub colPrivate_ColorChange(SelectedColor As stdole.OLE_COLOR)
cmdapplyconfig.Enabled = True
End Sub

Private Sub Command1_Click()
frmaddressbook.TemplateRegistry True
Command1.Enabled = False
End Sub





Private Sub cmdaddresses_Click()
Dim sPath As String, oCdlg As New clsCommonDialog

     Screen.MousePointer = vbHourglass
     sPath = oCdlg.APIGetOpenFileName("Choose MDB", oCurUser.Settings.TemplatePath, "Microsoft Access Database {*.mdb}" & Chr(0) & "*.mdb" & Chr(0), 0, "*.mdb", 255, 1, hwnd)
    Screen.MousePointer = vbHourglass
    If sPath <> "" Then
        If Not oContacts.ImportAddresses(Trim$(sPath)) Then
            MsgBox "Failed to imported Addresses from MDB"
        Else
            MsgBox "Successfully Imported addresses from MDB."
        End If
    End If
    frmaddressbook.ShowContacts
    Screen.MousePointer = vbDefault
End Sub

Private Sub cmdapplyconfig_Click()
Dim i As Long, oCol As clsCols, bSetupGrid As Boolean, j As Long, nCols As Long, nRows As Long

With oCurUser.Settings
    .Highlighttext = IIf(CBool(Abs(Check2.Value)), "1", "0")
    .PDC = txtconfigdtls(1)
    .exchserver = txtconfigdtls(0)
    If .TemplatePath <> txtconfigdtls(2) Then
      .TemplatePath = txtconfigdtls(2)
        frmaddressbook.TemplateRegistry True
    End If
    If bcolchanged Then
        For i = 1 To .Columns.Count
            .Columns.Remove 1
        Next i
        For i = 0 To lstcolumns.ListCount - 1
            Set oCol = New clsCols
            oCol.Width = IIf(lstcolumns.Selected(i), 2000, 0)
            oCol.position = i + 1
            oCol.Name = lstcolumns.list(i)
            .Columns.Add oCol, CStr(oCol.position)
        Next i
        Set oCol = Nothing
        frmaddressbook.SetupGrid
        frmaddressbook.fillgrid
        bcolchanged = False
    End If

   ' If .Background <> ColBackGround.Color Then .Background = ColBackGround.Color: frmaddressbook.grdcontacts.BackColor = ColBackGround.Color
    If .CompanyCol <> colCompany.Color Then
        frmaddressbook.grdcontacts.Redraw = False
        nRows = frmaddressbook.grdcontacts.Rows - 1
        nCols = frmaddressbook.grdcontacts.cols - 1
        For j = 1 To nRows
            frmaddressbook.grdcontacts.Row = j
            For i = 0 To nCols
                frmaddressbook.grdcontacts.Col = i
                 If frmaddressbook.grdcontacts.CellForeColor = .CompanyCol Then
                    frmaddressbook.grdcontacts.CellForeColor = colCompany.Color
                 End If
            Next i
        Next j
         .CompanyCol = colCompany.Color
        frmaddressbook.grdcontacts.Redraw = True
    End If
    If .GridBackCol <> colGridBack.Color Then
        frmaddressbook.grdcontacts.Redraw = False
        .GridBackCol = colGridBack.Color
        nRows = frmaddressbook.grdcontacts.Rows - 1
        nCols = frmaddressbook.grdcontacts.cols - 1
        For j = 1 To nRows
            frmaddressbook.grdcontacts.Row = j
            For i = 1 To nCols
                frmaddressbook.grdcontacts.Col = i
                 frmaddressbook.grdcontacts.CellBackColor = .GridBackCol
            Next i
        Next j
        frmaddressbook.grdcontacts.Redraw = True
    End If
    If .GridBorders <> colGridBorders.Color Then .GridBorders = colGridBorders.Color: frmaddressbook.grdcontacts.BackColorFixed = colGridBorders.Color
    If .PrivateCol <> colPrivate.Color Then
        frmaddressbook.grdcontacts.Redraw = False
        nRows = frmaddressbook.grdcontacts.Rows - 1
        nCols = frmaddressbook.grdcontacts.cols - 1
        For j = 1 To nRows
            frmaddressbook.grdcontacts.Row = j
            For i = 0 To nCols
                frmaddressbook.grdcontacts.Col = i
                 If frmaddressbook.grdcontacts.CellForeColor = .PrivateCol Then
                    frmaddressbook.grdcontacts.CellForeColor = colPrivate.Color
                 End If
            Next i
        Next j
        .PrivateCol = colPrivate.Color
        frmaddressbook.grdcontacts.Redraw = True
        
    End If
    If .Background <> ColBackGround.Color Then
        .Background = ColBackGround.Color: frmaddressbook.BackColor = .Background
        .GridBackCol = .Background
        frmaddressbook.grdcontacts.BackColorBkg = .Background
        frmaddressbook.grdcontacts.Refresh
    End If
    
End With
Command1.Enabled = True
'If bSetupGrid Then frmaddressbook.SetupGrid
cmdapplyconfig.Enabled = False
End Sub

Private Sub cmdnetwork_Click(Index As Integer)
Dim sRet As String, oldvalue As String

start:
sRet = frmNWCheck.LoadForm(Me)
If sRet = "" Then sRet = oldvalue
txtconfigdtls(Index) = sRet
End Sub

'Private Sub cmdoffline_Click()
'    Screen.MousePointer = vbHourglass
'   ' oCurUser.Settings.exchserver = frmNWCheck.loadform(Me)
'    If Not oContacts.CreateOffLineDB(oCurUser.UserName) Then
'        MsgBox "Failed to create Off-Line DB"
'    Else
'        MsgBox "Created Off-Line DB successfully and can be found in the root of the C drive." '& "Time elapsed: " & DateDiff("s", dstart, Now)
'    End If
'    'Set oContacts = Nothing
'    Screen.MousePointer = vbDefault
'End Sub

Private Sub cmdprofile_Click()
 Screen.MousePointer = vbHourglass
   ' MsgBox IIf(SetupOffline(oCurUser.UserName, CBool(oCurUser.FullPSTCopy = "1")), "Succesfull", "Failed")
    Screen.MousePointer = vbDefault
End Sub

Private Sub cmdtemplate_Click()
Dim soldvalue As String
soldvalue = txtconfigdtls(2)
txtconfigdtls(2) = Browse(oCurUser.Settings.TemplatePath, hwnd, "Choose the template Directory")
If txtconfigdtls(2) = "" Then txtconfigdtls(2) = soldvalue
End Sub

Private Sub cmdtemplateoptions_Click()
'If lsttemplates.ListIndex > -1 Then
'    If MsgBox("Are you totally sure you want to delete this template. Doing so will result in other Users not being able to use the template either.", vbQuestion + vbYesNo) = vbYes Then
'        Kill (oCurUser.Settings.TemplatePath & "\" & lsttemplates.List(lsttemplates.ListIndex))
'    End If
'    lsttemplates.Refresh
'End If
End Sub

Private Sub cmduseroptions_Click(Index As Integer)
Dim i As Long, oUser As New clsContact, ndeleted As Long
Select Case Index
Case 0 'add
    Select Case cmduseroptions(0).Caption
    Case "&Add User"
        lstusers.ListIndex = -1
        lstusers.Enabled = False
        For i = 0 To txtuserdtls.Count - 1
            txtuserdtls(i).Locked = False
            txtuserdtls(i) = ""
        Next i
        Check1.Value = 0
        chkseeall.Enabled = True
        Check1.Enabled = True
        cmduseroptions(0).Caption = "&Save User"
        cmduseroptions(1).Enabled = False
        cmduseroptions(2).Caption = "&Cancel"
        cmdaddresses.Enabled = False
        'cmdoffline.Enabled = False
       ' cmdprofile.Enabled = False
        cmdusers.Enabled = False
    Case "&Save User"
        If MsgBox("Are you sure you want to Save your Changes", vbYesNo + vbQuestion) = vbYes Then
            If UserbEdit Then
                oContacts.GetSingleUser lstusers.ItemData(lstusers.ListIndex), oUser
            End If
                With oUser
                    .UserName = txtuserdtls(0)
                    .pwd = txtuserdtls(1)
                    .userref = txtuserdtls(2)
                    .ClientName = txtuserdtls(3)
                    .LetrUserName = txtuserdtls(4)
                    .AuthorName = txtuserdtls(5)
                    .AuthorTitle = txtuserdtls(6)
                    .AuthorInitials = txtuserdtls(7)
                    .rights = IIf(Check1.Value = 0, 0, 1)
                    .Seeall = CStr(chkseeall.Value)
                End With
                oContacts.contype = Users

            If Not oContacts.AddContact(oUser, oCurUser.UserName, True) Then MsgBox "Save Failed."
            'If UserName = oCurUser.UserName Then oContacts.Login oCurUser.UserName, oCurUser.pwd, oCurUser
            'Set oUser = Nothing
            FillUsersList
            lstusers.ListIndex = -1
            For i = 0 To txtuserdtls.Count - 1
                 txtuserdtls(i).Locked = True
                 txtuserdtls(i) = ""
             Next i
            chkseeall.Enabled = False
            Check1.Enabled = False
            lstusers.Enabled = True
            cmduseroptions(0).Caption = "&Add User"
            cmduseroptions(1).Enabled = True
            cmduseroptions(2).Caption = "&Delete User"
            UserbEdit = False
            cmdaddresses.Enabled = True
          '  cmdoffline.Enabled = True
           ' cmdprofile.Enabled = True
            cmdusers.Enabled = True
        End If
    End Select
Case 1 'edit
    If lstusers.ListIndex > -1 Then
        UserbEdit = True
        lstusers.Enabled = False
        For i = 0 To txtuserdtls.Count - 1
            txtuserdtls(i).Locked = False
        Next i
        Check1.Enabled = True
        chkseeall.Enabled = True
        cmduseroptions(0).Caption = "&Save User"
        cmduseroptions(1).Enabled = False
        cmduseroptions(2).Caption = "&Cancel"
        cmdaddresses.Enabled = False
       ' cmdoffline.Enabled = False
       ' cmdprofile.Enabled = False
        cmdusers.Enabled = False
    Else
        MsgBox "Please select a User to Edit"
    End If
    
Case 2 'delete
    Select Case cmduseroptions(2).Caption
    Case "&Delete User"
        If lstusers.ListIndex = -1 Then Exit Sub
        If MsgBox("Are you sure you want to Delete this User.", vbQuestion + vbYesNo) = vbYes Then
            oContacts.contype = Users
            If Not oContacts.DelContacts(oCurUser.UserName, ndeleted, False, oUsers.Item(CStr(lstusers.ItemData(lstusers.ListIndex))).ID, True) Then
                MsgBox "Failed to Delete User"
                Exit Sub
            End If
            FillUsersList
             For i = 0 To txtuserdtls.Count - 1
                txtuserdtls(i).Locked = True
                txtuserdtls(i) = ""
            Next i
        End If
    Case "&Cancel"
        cmduseroptions(0).Caption = "&Add User"
        cmduseroptions(1).Enabled = True
        cmduseroptions(2).Caption = "&Delete User"
        For i = 0 To txtuserdtls.Count - 1
            txtuserdtls(i).Locked = True
            If Not UserbEdit Then txtuserdtls(i) = ""
        Next i
        Check1.Enabled = False
        If UserbEdit Then lstusers_Click
        cmdaddresses.Enabled = True
      '  cmdoffline.Enabled = True
      '  cmdprofile.Enabled = True
        cmdusers.Enabled = True
        chkseeall.Enabled = False
        lstusers.Enabled = True
    End Select
End Select
End Sub

Private Sub cmdusers_Click()
Dim o As New clsContact, oCdlg As New clsCommonDialog, sPath As String

sPath = Space(200)
sPath = oCdlg.APIGetOpenFileName("Choose MDB", "c:", "Microsoft Access Database {*.mdb}" & Chr(0) & "*.mdb" & Chr(0), 0, "*.mdb", 255, 1, hwnd)
Screen.MousePointer = vbHourglass
If sPath <> "" Then
    o.GetUsersFromMDB sPath
'    If o.Count > 0 Then
'        Stop
'    End If
    FillUsersList
End If
Screen.MousePointer = vbDefault
End Sub

Private Sub Command2_Click()

End Sub

Private Sub Form_Load()
Dim i As Long, sColumnArray() As String

With oCurUser.Settings
    cmdoffline.Enabled = False
   ' cmdprofile.Enabled = False
    For i = 1 To .Columns.Count
        lstcolumns.AddItem oCurUser.Settings.Columns(i).Name
        lstcolumns.ItemData(lstcolumns.NewIndex) = i
        lstcolumns.Selected(i - 1) = (.Columns(i).Width > 0)
    Next i
    ColBackGround.Color = .Background
    colCompany.Color = .CompanyCol
    colGridBack.Color = .GridBackCol
    colGridBorders.Color = .GridBorders
    colPrivate.Color = .PrivateCol
       txtweb = .WebPage
If oCurUser.rights <> "1" Then

      tabAdmin.Tabs(3).Selected = True
    'cmdDeletions.Visible = False
    cmdaddresses.Enabled = False
    cmdusers.Enabled = False
   ' cmdoffline.Enabled = False
    'cmdprofile.Enabled = False
Else
    'cmdDeletions.Visible = True
    tabAdmin.Tabs(1).Selected = True
    FillUsersList
    On Error Resume Next
    'lsttemplates.Path = oCurUser.Settings.TemplatePath
    
    lstusers.ListIndex = 0

End If
    txtconfigdtls(0) = .exchserver
    txtconfigdtls(1) = .PDC
    txtconfigdtls(2) = .TemplatePath
    cmdapplyconfig.Enabled = False
    Check2.Value = IIf(.Highlighttext = "1", 1, 0)
End With
End Sub

Private Sub Form_QueryUnload(Cancel As Integer, UnloadMode As Integer)
If cmdapplyconfig.Enabled Then
    If MsgBox("There are changes that have not been applied, would you like to apply these changes now.", vbQuestion + vbYesNo) = vbYes Then
        cmdapplyconfig_Click
    End If
End If
End Sub

Private Sub Form_Unload(Cancel As Integer)
bDontactivate = True
oContacts.InitializeOutlook
If Not bIsOnline Then oContacts.contype = Offline
oContacts.contype = AllExchange
With frmaddressbook
    oContacts.ResetColection
'frmaddressbook.oAZ_AZClick (frmaddressbook.oAZ.CurrentLetter)
    .Enabled = True
    '.Timer1.Enabled = False
End With
End Sub

Private Sub lstcolumns_Click()
cmdapplyconfig.Enabled = True
bcolchanged = True
End Sub

Private Sub lstColumns_MouseUp(Button As Integer, Shift As Integer, x As Single, y As Single)

pnDestinationIndex = lstcolumns.ListIndex

If pnSelectedIndex < 0 Or pnDestinationIndex < 0 Or pnSelectedIndex = pnDestinationIndex Then
    lstcolumns.Selected(pnDestinationIndex) = plSelected
    Exit Sub
End If

pcSelectedItem = lstcolumns.list(pnSelectedIndex)

lstcolumns.RemoveItem pnSelectedIndex
lstcolumns.AddItem pcSelectedItem, pnDestinationIndex
lstcolumns.ItemData(pnDestinationIndex) = nitemdata
lstcolumns.Selected(pnDestinationIndex) = Not plSelected
bcolchanged = True
End Sub

Private Sub lstColumns_MouseDown(Button As Integer, Shift As Integer, x As Single, y As Single)

pnSelectedIndex = lstcolumns.ListIndex
plSelected = lstcolumns.Selected(pnSelectedIndex)
nitemdata = lstcolumns.ItemData(pnSelectedIndex)
lstcolumns.Selected(pnSelectedIndex) = Not plSelected

End Sub

Private Sub lstusers_Click()
If lstusers.ListIndex > -1 Then
    With oUsers.Item(CStr(lstusers.ItemData(lstusers.ListIndex)))
    On Error Resume Next
        txtuserdtls(0) = .UserName
        txtuserdtls(1) = .pwd
        txtuserdtls(2) = .userref
        txtuserdtls(3) = .ClientName
        txtuserdtls(4) = .LetrUserName
        txtuserdtls(5) = .AuthorName
        txtuserdtls(6) = .AuthorTitle
        txtuserdtls(7) = .AuthorInitials
        On Error Resume Next
        Check1.Value = .rights
        chkseeall.Value = 0
        chkseeall.Value = .Seeall
        End With
End If
End Sub

Private Sub tabAdmin_Click()
With tabAdmin
    ColBackGround.Visible = (.SelectedItem.Index = 3)
    colCompany.Visible = (.SelectedItem.Index = 3)
    colGridBack.Visible = (.SelectedItem.Index = 3)
    colGridBorders.Visible = (.SelectedItem.Index = 3)
    colPrivate.Visible = (.SelectedItem.Index = 3)
    
    If oCurUser.rights <> "1" And (.SelectedItem.Index = 1 Or .SelectedItem.Index = 2) Then
            fraadmin(1).Enabled = False
          'fraadmin(2).Enabled = False
    End If
End With
fraadmin(tabAdmin.SelectedItem.Index).ZOrder
If tabAdmin.SelectedItem.Index <> 1 Then cmdapplyconfig.ZOrder
End Sub

Private Sub FillUsersList()
Dim i As Long

oUsers.contype = Users
If oUsers.GetUsers() Then
    lstusers.Clear
    For i = 1 To oUsers.Count
        lstusers.AddItem oUsers.Item(i).FileAs
        lstusers.ItemData(lstusers.NewIndex) = oUsers.Item(i).ID
    Next i
    lblUsersCount.Caption = lstusers.ListCount & " Active User(s)"
End If
End Sub

Private Sub txtconfigdtls_Change(Index As Integer)
cmdapplyconfig.Enabled = True
End Sub

Private Sub txtweb_Change()
cmdapplyconfig.Enabled = True
End Sub

Private Sub cmdDeletions_Click()
    MousePointer = vbHourglass
    If Not oContacts.RemoveDeleted Then MsgBox "Failed."
    MousePointer = vbDefault
End Sub

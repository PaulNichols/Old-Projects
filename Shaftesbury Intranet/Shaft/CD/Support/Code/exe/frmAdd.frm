VERSION 5.00
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Object = "{86CF1D34-0C5F-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCT2.OCX"
Begin VB.Form frmAdd 
   BorderStyle     =   4  'Fixed ToolWindow
   Caption         =   "Add Contact..."
   ClientHeight    =   7050
   ClientLeft      =   45
   ClientTop       =   285
   ClientWidth     =   6660
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   7050
   ScaleWidth      =   6660
   ShowInTaskbar   =   0   'False
   StartUpPosition =   1  'CenterOwner
   Begin VB.Frame fraContactDtls 
      Caption         =   "Details:"
      Height          =   6855
      Left            =   120
      TabIndex        =   17
      Top             =   120
      Width           =   6495
      Begin VB.Frame frabuss 
         BorderStyle     =   0  'None
         Caption         =   "Frame1"
         Height          =   2775
         Left            =   240
         TabIndex        =   21
         Top             =   3360
         Width           =   5775
         Begin VB.TextBox txtdtls1 
            Appearance      =   0  'Flat
            Height          =   285
            Index           =   10
            Left            =   3960
            TabIndex        =   42
            Top             =   2400
            Width           =   1815
         End
         Begin VB.TextBox txtdtls1 
            Appearance      =   0  'Flat
            Height          =   285
            Index           =   5
            Left            =   1680
            TabIndex        =   40
            Top             =   2040
            Width           =   2175
         End
         Begin VB.TextBox txtdtls1 
            Appearance      =   0  'Flat
            Height          =   285
            Index           =   17
            Left            =   3960
            TabIndex        =   12
            Top             =   1680
            Width           =   1815
         End
         Begin VB.TextBox txtdtls1 
            Appearance      =   0  'Flat
            Height          =   285
            Index           =   6
            Left            =   1680
            TabIndex        =   9
            Top             =   1335
            Width           =   2175
         End
         Begin VB.TextBox txtdtls1 
            Appearance      =   0  'Flat
            Height          =   285
            Index           =   7
            Left            =   1680
            TabIndex        =   10
            Top             =   1680
            Width           =   2175
         End
         Begin VB.TextBox txtdtls1 
            Appearance      =   0  'Flat
            Height          =   285
            Index           =   8
            Left            =   1680
            TabIndex        =   11
            Top             =   2400
            Width           =   2175
         End
         Begin VB.TextBox txtdtls1 
            Appearance      =   0  'Flat
            Height          =   1245
            Index           =   2
            Left            =   1680
            MultiLine       =   -1  'True
            TabIndex        =   8
            Top             =   0
            Width           =   3975
         End
         Begin VB.Label lbl 
            Caption         =   "Fax Abbr:"
            Height          =   255
            Index           =   5
            Left            =   3960
            TabIndex        =   43
            Top             =   2160
            Width           =   1335
         End
         Begin VB.Label lbl 
            Caption         =   "Phone Abbr:"
            Height          =   255
            Index           =   4
            Left            =   0
            TabIndex        =   41
            Top             =   2040
            Width           =   1335
         End
         Begin VB.Label lbl 
            Caption         =   "Mobile:"
            Height          =   255
            Index           =   18
            Left            =   3960
            TabIndex        =   27
            Top             =   1440
            Width           =   1335
         End
         Begin VB.Label lbl 
            Caption         =   "Fax:"
            Height          =   255
            Index           =   6
            Left            =   0
            TabIndex        =   25
            Top             =   1320
            Width           =   1335
         End
         Begin VB.Label lbl 
            Caption         =   "Tel:"
            Height          =   255
            Index           =   7
            Left            =   0
            TabIndex        =   24
            Top             =   1680
            Width           =   1335
         End
         Begin VB.Label lbl 
            Caption         =   "Home Page - http://:"
            Height          =   255
            Index           =   8
            Left            =   0
            TabIndex        =   23
            Top             =   2400
            Width           =   1695
         End
         Begin VB.Label lbl 
            Caption         =   "Business Address:"
            Height          =   255
            Index           =   2
            Left            =   0
            TabIndex        =   22
            Top             =   0
            Width           =   1335
         End
      End
      Begin VB.TextBox txtdtls1 
         Appearance      =   0  'Flat
         Height          =   285
         Index           =   4
         Left            =   1920
         TabIndex        =   4
         Top             =   1455
         Width           =   2895
      End
      Begin VB.TextBox txtdtls1 
         Appearance      =   0  'Flat
         Height          =   285
         Index           =   3
         Left            =   3960
         TabIndex        =   3
         Top             =   1080
         Width           =   2295
      End
      Begin VB.ComboBox cbosuffix 
         Height          =   315
         Left            =   3840
         TabIndex        =   1
         Top             =   720
         Width           =   975
      End
      Begin VB.ComboBox cbotitle 
         Height          =   315
         Left            =   1920
         TabIndex        =   0
         Top             =   720
         Width           =   975
      End
      Begin VB.CommandButton cmdadd 
         Caption         =   "Save as &Private Contact"
         Height          =   375
         Index           =   0
         Left            =   120
         TabIndex        =   30
         Top             =   6360
         Width           =   2175
      End
      Begin VB.CommandButton cmdadd 
         Caption         =   "&Save as Company Contact"
         Height          =   375
         Index           =   1
         Left            =   2400
         TabIndex        =   29
         Top             =   6360
         Width           =   2175
      End
      Begin VB.CommandButton cmdclose 
         Cancel          =   -1  'True
         Caption         =   "&Close"
         Height          =   375
         Left            =   5040
         TabIndex        =   28
         Top             =   6360
         Width           =   1215
      End
      Begin VB.TextBox txtdtls1 
         Appearance      =   0  'Flat
         Height          =   285
         Index           =   15
         Left            =   1920
         TabIndex        =   7
         Top             =   375
         Width           =   2895
      End
      Begin VB.TextBox txtdtls1 
         Appearance      =   0  'Flat
         Height          =   285
         Index           =   9
         Left            =   1920
         TabIndex        =   6
         Top             =   2175
         Width           =   2895
      End
      Begin VB.TextBox txtdtls1 
         Appearance      =   0  'Flat
         Height          =   285
         Index           =   1
         Left            =   1920
         TabIndex        =   5
         Top             =   1815
         Width           =   2895
      End
      Begin VB.TextBox txtdtls1 
         Appearance      =   0  'Flat
         Height          =   285
         Index           =   0
         Left            =   1920
         TabIndex        =   2
         Top             =   1080
         Width           =   1935
      End
      Begin MSComctlLib.TabStrip TabStrip1 
         Height          =   3495
         Left            =   120
         TabIndex        =   31
         Top             =   2760
         Width           =   6135
         _ExtentX        =   10821
         _ExtentY        =   6165
         _Version        =   393216
         BeginProperty Tabs {1EFB6598-857C-11D1-B16A-00C0F0283628} 
            NumTabs         =   2
            BeginProperty Tab1 {1EFB659A-857C-11D1-B16A-00C0F0283628} 
               Caption         =   "Business Details"
               Key             =   "Buss"
               ImageVarType    =   2
            EndProperty
            BeginProperty Tab2 {1EFB659A-857C-11D1-B16A-00C0F0283628} 
               Caption         =   "Personal Details"
               Key             =   "Personal"
               ImageVarType    =   2
            EndProperty
         EndProperty
      End
      Begin VB.Frame frapersonal 
         BorderStyle     =   0  'None
         Caption         =   "Frame1"
         Height          =   2415
         Left            =   240
         TabIndex        =   32
         Top             =   3360
         Width           =   5775
         Begin VB.TextBox txtdtls1 
            Appearance      =   0  'Flat
            Height          =   285
            Index           =   13
            Left            =   1680
            TabIndex        =   14
            Top             =   1335
            Width           =   2175
         End
         Begin VB.TextBox txtdtls1 
            Appearance      =   0  'Flat
            Height          =   285
            Index           =   14
            Left            =   1680
            TabIndex        =   15
            Top             =   1680
            Width           =   2175
         End
         Begin VB.TextBox txtdtls1 
            Appearance      =   0  'Flat
            Height          =   1245
            Index           =   16
            Left            =   1680
            MultiLine       =   -1  'True
            TabIndex        =   13
            Top             =   0
            Width           =   3975
         End
         Begin MSComCtl2.DTPicker DTPicker1 
            Height          =   300
            Left            =   1680
            TabIndex        =   16
            Top             =   2040
            Width           =   2175
            _ExtentX        =   3836
            _ExtentY        =   529
            _Version        =   393216
            Format          =   22740992
            CurrentDate     =   36725
         End
         Begin VB.Label lbl 
            Caption         =   "Fax:"
            Height          =   255
            Index           =   14
            Left            =   0
            TabIndex        =   36
            Top             =   1320
            Width           =   1335
         End
         Begin VB.Label lbl 
            Caption         =   "Tel:"
            Height          =   255
            Index           =   15
            Left            =   0
            TabIndex        =   35
            Top             =   1680
            Width           =   1335
         End
         Begin VB.Label lbl 
            Caption         =   "Home Address:"
            Height          =   255
            Index           =   17
            Left            =   0
            TabIndex        =   34
            Top             =   0
            Width           =   1335
         End
         Begin VB.Label lbl 
            Caption         =   "Birthday:"
            Height          =   255
            Index           =   1
            Left            =   0
            TabIndex        =   33
            Top             =   2040
            Width           =   1335
         End
      End
      Begin VB.Label lbl 
         Caption         =   "Salutation:"
         Height          =   375
         Index           =   3
         Left            =   240
         TabIndex        =   39
         Top             =   1440
         Width           =   1335
      End
      Begin VB.Label Label2 
         Caption         =   "Suffix:"
         Height          =   255
         Left            =   3240
         TabIndex        =   38
         Top             =   720
         Width           =   495
      End
      Begin VB.Label Label1 
         Caption         =   "Title:"
         Height          =   255
         Left            =   240
         TabIndex        =   37
         Top             =   720
         Width           =   855
      End
      Begin VB.Label lbl 
         Caption         =   "File As:"
         Height          =   375
         Index           =   16
         Left            =   240
         TabIndex        =   26
         Top             =   360
         Width           =   1335
      End
      Begin VB.Label lbl 
         Caption         =   "Email:"
         Height          =   375
         Index           =   10
         Left            =   240
         TabIndex        =   20
         Top             =   2160
         Width           =   1335
      End
      Begin VB.Label lbl 
         Caption         =   "Company Name:"
         Height          =   375
         Index           =   9
         Left            =   240
         TabIndex        =   19
         Top             =   1800
         Width           =   1335
      End
      Begin VB.Label lbl 
         Caption         =   "First Name/Surname:"
         Height          =   375
         Index           =   0
         Left            =   240
         TabIndex        =   18
         Top             =   1080
         Width           =   1575
      End
   End
End
Attribute VB_Name = "frmAdd"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Dim oThisContact As Object
Dim bAllowClose As Boolean
Dim bEdit As Boolean, bUnSavedData As Boolean
Dim bViewOnlymode As Boolean
Dim bcloseclicked  As Boolean
Dim bFinishLoad As Boolean
Dim DBID As Long, sOldAbbr As String, sOldFaxAbbr As String

Public Function LoadForm(bViewOnly As Boolean, Optional oContact, Optional frmOwner As Form, Optional aDBID) As Boolean
Dim sNTlogon As String, i As Long

If IsMissing(aDBID) Then
    DBID = 0
Else
    DBID = aDBID
End If

    bFinishLoad = False
    If Not IsMissing(oContact) Then
        If oContact Is Nothing Then Unload Me: LoadForm = False: Exit Function
 bViewOnlymode = bViewOnly
        If bViewOnly Then
            bEdit = False
            Caption = "Contact: " & oContact.FileAs
            On Error Resume Next
            For i = 0 To 18
                txtdtls1(i).Locked = True
            Next i
            cbotitle.Enabled = False
            cbosuffix.Enabled = False
            On Error GoTo 0
            DTPicker1.Enabled = False
            cmdadd(0).Enabled = False
            cmdadd(1).Enabled = False
            Set oThisContact = oContact
            FillDtls
        Else
            Set oThisContact = oContact
            FillDtls
            Caption = "Edit Contact..."
            bEdit = True
                On Error Resume Next
                 
                sNTlogon = oContact.User1
               'On Error GoTo 0
            If sNTlogon = "" Then
                cmdadd(0).Enabled = False
                cmdadd(1).Enabled = True
                cmdadd(1).Caption = "Update Company Contact"
            Else
                cmdadd(0).Enabled = True
                cmdadd(1).Enabled = False
                cmdadd(0).Caption = "Update Private Contact"
            End If
        End If
    Else
        bEdit = False
        Caption = "Add Contact..."
    End If
    TabStrip1.Tabs("Buss").Selected = True
    With cbotitle
        .AddItem "Dr."
        .AddItem "Miss"
        .AddItem "Mr."
        .AddItem "Mrs."
        .AddItem "Ms."
        .AddItem "Prof."
        .AddItem "Rev"
        .AddItem "Lady"
        .AddItem "Lord"
     End With
     With cbosuffix
        .AddItem "I"
        .AddItem "II"
        .AddItem "III"
        .AddItem "Jr."
        .AddItem "OBE."
        .AddItem "MBE."
        .AddItem "Rt.Hon"
    End With
    Show vbModeless, frmOwner
    HighlightTxT Me
    bFinishLoad = True
End Function

Private Sub FillDtls()

     
    With oThisContact
        txtdtls1(0) = Trim(.Firstname)
        txtdtls1(3) = Trim(.LastName)
        DTPicker1 = Trim(.Birthday)
        txtdtls1(2) = Trim(.BusinessAddress)
       ' txtdtls1(3) = .BusinessAddressCity
       On Error Resume Next
        txtdtls1(4) = Trim(.Salutation)
        On Error GoTo 0
        txtdtls1(5) = Trim(.OtherTelephoneNumber)
        txtdtls1(6) = Trim(.BusinessFaxNumber)
        On Error Resume Next
        txtdtls1(8) = Trim(Mid$(.BusinessHomePage, 8))
        On Error GoTo 0
        txtdtls1(7) = Trim(.BusinessTelephoneNumber)
        txtdtls1(1) = Trim(.CompanyName)
        txtdtls1(16) = Trim(.homeaddress)
        txtdtls1(13) = Trim(.HomeFaxNumber)
        txtdtls1(17) = Trim(.MobileTelephoneNumber)
        txtdtls1(15) = Trim(.FileAs)
        txtdtls1(9) = Trim(.Email1Address)
        txtdtls1(10) = Trim(.otherfaxnumber)
        cbosuffix.Text = Trim(.Suffix)
        cbotitle.Text = Trim(.Title)
        txtdtls1(14) = Trim(.HomeTelephoneNumber)
    End With
     sOldAbbr = txtdtls1(5)
     sOldFaxAbbr = txtdtls1(10)
End Sub
Private Function Add(atype As ContactType) As Boolean
    Dim oFindContact As New clsContact
    Screen.MousePointer = vbHourglass
  
              
    If bEdit Then
        oContacts.contype = atype
        If oContacts.UpdateContact(GatherContactDtls, DBID) Then
            bAllowClose = True
            Add = True
        Else
        
        End If
    Else
        'add move to find here
        'if adding a company or private contact there adding  then ask if they want to overwrite.if adding a private and someone else has it as private ask if
        'contact should be promoted and whether these are the details or the existing are the details?
        oFindContact.InitializeOutlook
        oFindContact.contype = AllExchange
        If oFindContact.FindContact(oCurUser.UserName, "fileas", txtdtls1(15)) Then
            If oFindContact.Count > 0 Then
                If atype = Exchangeprivate Or atype = Exchangepublic Then
                    MsgBox "This contact already exists"
                    Add = False
                    bDontactivate = True
                    Screen.MousePointer = vbDefault
                    Exit Function
                Else

                End If
            End If
        End If
        Set oFindContact = Nothing
        If checkdtls Then
            oContacts.InitializeOutlook
             oContacts.contype = atype
            If Not oContacts.AddContact(GatherContactDtls(), IIf(atype = Exchangepublic, "", oCurUser.UserName)) Then
                MsgBox "Failed to save Contact": Add = False
            Else
              '  oContacts.GetContacts
                 'MsgBox "Contact successfully added"
                 'Set oContacts = Nothing
                 frmaddressbook.oAZ.CurrentLetter = Left$(txtdtls1(15), 1)
                 'frmaddressbook.oAZ_AZClick
'                 frmaddressbook.oAZ.CurrentLetter = "G"
'                If frmaddressbook.ShowContacts() Then
                    Add = True
                    bAllowClose = True
'                End If
            End If
        Else
            MsgBox "Failed to save Contact due to missing data or incorrect data": Add = False
            bAllowClose = True
        End If
    End If
    Screen.MousePointer = vbDefault
End Function
       
Private Function checkdtls() As Boolean
    checkdtls = True
    If txtdtls1(0) = "" Then checkdtls = False: Exit Function
End Function
Private Sub cmdadd_Click(Index As Integer)
    If Add(IIf(Index = 0, Exchangeprivate, Exchangepublic)) Then: bUnSavedData = False: Unload Me
End Sub

Private Function GatherContactDtls() As Object

If bEdit Then
    Set GatherContactDtls = oThisContact
Else
    Set GatherContactDtls = New clsContact
 End If
    With GatherContactDtls
        .Firstname = IIf(Len(Trim(txtdtls1(0))) = 0, " ", txtdtls1(0))
        .fullname = cbotitle.Text & " " & txtdtls1(0) & " " & txtdtls1(3) & " " & cbosuffix.Text
        .fullname = Trim(.fullname)
        .Birthday = IIf(Len(Trim(DTPicker1)) = 0, " ", DTPicker1)
        .BusinessAddress = IIf(Len(Trim(txtdtls1(2))) = 0, " ", txtdtls1(2))
        .LastName = IIf(Len(Trim(txtdtls1(3))) = 0, " ", txtdtls1(3))
        .OtherTelephoneNumber = IIf(Len(Trim(txtdtls1(5))) = 0, " ", txtdtls1(5))
        .otherfaxnumber = CLng(IIf(Len(Trim(txtdtls1(10))) = 0, 0, txtdtls1(10)))
        .BusinessFaxNumber = IIf(Len(Trim(txtdtls1(6))) = 0, " ", txtdtls1(6))
        .BusinessHomePage = IIf(Len(Trim(txtdtls1(8))) = 0, " ", txtdtls1(8))
        .BusinessTelephoneNumber = IIf(Len(Trim(txtdtls1(7))) = 0, " ", txtdtls1(7))
        .CompanyName = IIf(Len(Trim(txtdtls1(1))) = 0, " ", txtdtls1(1))
        .homeaddress = IIf(Len(Trim(txtdtls1(16))) = 0, " ", txtdtls1(16))
        .HomeFaxNumber = IIf(Len(Trim(txtdtls1(13))) = 0, " ", txtdtls1(13))
        .MobileTelephoneNumber = IIf(Len(Trim(txtdtls1(17))) = 0, " ", txtdtls1(17))
        .Title = IIf(Len(Trim(cbotitle.Text)) = 0, " ", cbotitle.Text)
        .Suffix = IIf(Len(Trim(cbosuffix.Text)) = 0, " ", cbosuffix.Text)
        .FileAs = IIf(Len(Trim(txtdtls1(15))) = 0, " ", txtdtls1(15))
        .Email1Address = IIf(Len(Trim(txtdtls1(9))) = 0, " ", txtdtls1(9))
        .HomeTelephoneNumber = IIf(Len(Trim(txtdtls1(14))) = 0, " ", txtdtls1(14))
'        If bEdit Then
'            .userproperties.Add("Salutation", 1) = txtdtls1(4)
'        Else
            .Salutation = IIf(Len(Trim(txtdtls1(4))) = 0, " ", txtdtls1(4))
       ' End If
       .Business2TelephoneNumber = " "
       
    End With
End Function

Private Sub cmdclose_Click()
    bcloseclicked = True
    Unload Me
    bAllowClose = False
    bcloseclicked = False
End Sub

Private Function closeform() As Boolean
Dim i As Long, j As Long, sRow As String

    If Not bAllowClose Or Not bEdit Then
        If bUnSavedData Then
            If MsgBox("You will loose unsaved data are you sure you wish to close.", vbQuestion + vbYesNo) = vbYes Then
                closeform = False
            Else
                closeform = True
            End If
        End If
    Else
        closeform = False
    End If
    If bEdit Then
        With frmaddressbook.grdcontacts
           
             For j = 0 To oCurUser.Settings.Columns.Count - 1
                .Col = j
                .Text = oContacts.ContactPropertyValue(oThisContact, oCurUser.Settings.Columns(j + 1).Name)
            Next j
        End With
    Else
'     With frmaddressbook.grdcontacts
'            .Col = 0
'             For j = 0 To oCurUser.Settings.Columns.Count - 1
'                sRow = oContacts.ContactPropertyValue(oContacts.Item(.RowData(.Row)), oCurUser.Settings.Columns(j + 1).Name)
'            Next j
'            .AddItem sRow
'        End With
    End If
    'frmaddressbook.ShowContacts
   ' If Not bViewOnlymode And Not bcloseclicked Then frmaddressbook.ShowContacts
End Function

Private Sub DTPicker1_Change()
    SetDirty
End Sub

Private Sub Form_QueryUnload(Cancel As Integer, UnloadMode As Integer)
    Cancel = closeform()
    frmaddressbook.Enabled = True
End Sub

Private Sub Form_Unload(Cancel As Integer)
    bDontactivate = True
    oContacts.contype = oCurUser.Settings.ViewType
    'frmaddressbook.Timer1.Enabled = True
    Set frmAdd = Nothing
    
End Sub

Private Sub TabStrip1_Click()

    frabuss.Visible = IIf(TabStrip1.Tabs("Personal").Selected, False, True): frabuss.ZOrder
    frapersonal.Visible = IIf(TabStrip1.Tabs("Personal").Selected, True, False): frapersonal.ZOrder
End Sub

Private Function SetDirty() As Boolean
    If bFinishLoad And Not bViewOnlymode Then
        bUnSavedData = True
        SetDirty = True
    End If
End Function

Private Sub txtdtls1_KeyPress(Index As Integer, KeyAscii As Integer)
If KeyAscii <> 8 Then
If (Index = 5 Or Index = 10) And (KeyAscii < 48 Or KeyAscii > 57) Then KeyAscii = 0
End If
End Sub

Private Sub txtdtls1_LostFocus(Index As Integer)
Dim rs As Recordset, nNew As Long

If Index = 5 And txtdtls1(5) <> sOldAbbr Then
    nNew = oContacts.GetNewAbbr
    If Len(Trim(txtdtls1(5))) = 0 Then txtdtls1(5) = "0"
    If CLng(txtdtls1(5)) < (nNew - 1) Then
       ' MsgBox "If you wish to Enter an Abbreviation try  - " & nNew & IIf(sOldAbbr <> "", " or " & sOldAbbr, "")
        'txtdtls1(5) = ""
    End If
End If

If Index = 10 And txtdtls1(10) <> sOldFaxAbbr Then
    nNew = oContacts.GetNewAbbr2
    If Len(Trim(txtdtls1(10))) = 0 Then txtdtls1(10) = "0"
    If CLng(txtdtls1(10)) < (nNew - 1) Then
       ' MsgBox "If you wish to Enter an Abbreviation try  - " & nNew & IIf(sOldFaxAbbr <> "" And txtdtls1(10) <> "0" And sOldFaxAbbr <> "0", " or " & sOldFaxAbbr, "")
        'txtdtls1(10) = ""
    End If
End If

 If SetDirty Then
        If Index = 0 Or Index = 3 Then
            txtdtls1(3) = Proper(txtdtls1(3))
            txtdtls1(0) = Proper(txtdtls1(0))
            txtdtls1(4) = Proper(txtdtls1(0)) & " " & Proper(txtdtls1(3))
            txtdtls1(15) = txtdtls1(3) & "," & txtdtls1(0)
        End If
    End If
End Sub

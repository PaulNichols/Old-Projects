VERSION 5.00
Begin VB.Form frmSearch 
   BorderStyle     =   4  'Fixed ToolWindow
   Caption         =   "Find Contact..."
   ClientHeight    =   3165
   ClientLeft      =   45
   ClientTop       =   285
   ClientWidth     =   4665
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   3165
   ScaleWidth      =   4665
   ShowInTaskbar   =   0   'False
   StartUpPosition =   1  'CenterOwner
   Begin VB.TextBox Text2 
      BorderStyle     =   0  'None
      Height          =   405
      Left            =   240
      TabIndex        =   6
      Top             =   1080
      Width           =   4215
   End
   Begin VB.TextBox Text1 
      BorderStyle     =   0  'None
      Height          =   405
      Left            =   240
      TabIndex        =   4
      Top             =   360
      Width           =   4215
   End
   Begin VB.OptionButton Option1 
      Caption         =   "Public"
      Height          =   375
      Index           =   0
      Left            =   840
      TabIndex        =   3
      Top             =   1920
      Value           =   -1  'True
      Width           =   975
   End
   Begin VB.OptionButton Option1 
      Caption         =   "Private"
      Height          =   375
      Index           =   1
      Left            =   1800
      TabIndex        =   2
      Top             =   1920
      Width           =   975
   End
   Begin VB.OptionButton Option1 
      Caption         =   "All"
      Height          =   375
      Index           =   2
      Left            =   2880
      TabIndex        =   1
      Top             =   1920
      Width           =   975
   End
   Begin VB.CommandButton cmdsearch 
      Caption         =   "Search"
      Height          =   495
      Left            =   1080
      TabIndex        =   0
      Top             =   2520
      Width           =   2535
   End
   Begin VB.Label Label2 
      Caption         =   "Company Name:"
      Height          =   495
      Left            =   240
      TabIndex        =   7
      Top             =   840
      Width           =   1215
   End
   Begin VB.Label Label1 
      Caption         =   "Contact Name:"
      Height          =   495
      Left            =   240
      TabIndex        =   5
      Top             =   120
      Width           =   1215
   End
End
Attribute VB_Name = "frmSearch"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Dim frm As Form, bSearchAgain As Boolean

Public Function LoadForm(Optional frmOwner As Form) As Boolean
    Set frm = frmOwner
     Show vbModal, frmOwner
End Function

Private Sub cmdsearch_Click()
Dim i As Integer, rs As Recordset

    cmdsearch.Enabled = False
    For i = 0 To 2
        If Option1(i).Value Then Exit For
    Next i
    If oContacts.FindContact3(rs, oCurUser.UserName, Text2, Text1, i, oCurUser.Seeall) Then
        Set frmaddressbook.rs = rs
        frmaddressbook.fillgrid
    Else
        frmaddressbook.grdcontacts.Rows = 2
    End If
    'If frmaddressbook.grdcontacts.Rows <= 2 Then MsgBox "No Matches"
    bSearchAgain = True
    cmdsearch.Enabled = True
        
        
'    If bSearchAgain Then
'        'oContacts.GetContacts
'    End If
'    With frmaddressbook
'        If Option1(0).Value = True Then
'            oContacts.contype = ExchangePublic
'            oContacts.FindContact2 Text2.Text, Text1.Text, oCurUser.UserName
'            .mnuviewtype(1).Checked = False
'            .mnuviewtype(2).Checked = True
'            .mnuviewtype(3).Checked = False
'        ElseIf Option1(1).Value = True Then
'            oContacts.contype = ExchangePrivate
'            oContacts.FindContact2 Text2.Text, Text1.Text, oCurUser.UserName
'            .mnuviewtype(1).Checked = True
'            .mnuviewtype(2).Checked = False
'            .mnuviewtype(3).Checked = False
'        Else
'            oContacts.contype = AllExchange
'            oContacts.FindContact2 Text2.Text, Text1.Text, oCurUser.UserName
'            .mnuviewtype(1).Checked = False
'            .mnuviewtype(2).Checked = False
'            .mnuviewtype(3).Checked = True
'        End If
        frmaddressbook.SearchCompany = Text2.Text
        frmaddressbook.SearchFileas = Text1.Text
End Sub

Private Sub Form_Load()
Hide
End Sub

Private Sub Form_Unload(Cancel As Integer)
bDontactivate = True
frmaddressbook.Enabled = True
'frmaddressbook.Timer1.Enabled = True
End Sub

Private Sub Text1_Change()
'cmdsearch.Enabled = Not (Len(Text1) = 0 And Len(Text2) = 0)
End Sub

Private Sub Text1_KeyPress(KeyAscii As Integer)
If KeyAscii = 13 Then cmdsearch_Click
End Sub

Private Sub Text2_KeyPress(KeyAscii As Integer)
If KeyAscii = 13 Then cmdsearch_Click
End Sub

VERSION 5.00
Begin VB.Form frmdel 
   BorderStyle     =   4  'Fixed ToolWindow
   Caption         =   "Delete Contact(s)..."
   ClientHeight    =   3000
   ClientLeft      =   45
   ClientTop       =   285
   ClientWidth     =   4290
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   3000
   ScaleWidth      =   4290
   ShowInTaskbar   =   0   'False
   StartUpPosition =   1  'CenterOwner
   Begin VB.CommandButton cmddel 
      Caption         =   "Delete All Outlook Contacts"
      Height          =   375
      Index           =   3
      Left            =   600
      TabIndex        =   4
      Top             =   1680
      Width           =   3135
   End
   Begin VB.CommandButton cmddel 
      Caption         =   "Delete All Company and Private Contacts"
      Height          =   375
      Index           =   4
      Left            =   600
      TabIndex        =   3
      Top             =   2400
      Width           =   3135
   End
   Begin VB.CommandButton cmddel 
      Caption         =   "Delete Chosen Contact"
      Height          =   375
      Index           =   0
      Left            =   600
      TabIndex        =   2
      Top             =   240
      Width           =   3135
   End
   Begin VB.CommandButton cmddel 
      Caption         =   "Delete All Private Contacts"
      Height          =   375
      Index           =   1
      Left            =   600
      TabIndex        =   1
      Top             =   720
      Width           =   3135
   End
   Begin VB.CommandButton cmddel 
      Caption         =   "Delete All Company Contacts"
      Height          =   375
      Index           =   2
      Left            =   600
      TabIndex        =   0
      Top             =   1200
      Width           =   3135
   End
End
Attribute VB_Name = "frmdel"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Dim oThisContact As Object, DBID As String

Public Function LoadForm(Optional oContact, Optional frmOwner As Form, Optional aDBID = "") As Boolean
    DBID = aDBID
    frmaddressbook.Enabled = False
    cmddel(2).Enabled = (oCurUser.rights = 1)
    cmddel(4).Enabled = (oCurUser.rights = 1)
    cmddel(0).Enabled = Not IsMissing(oContact)
    If Not IsMissing(oContact) Then Set oThisContact = oContact
 
    Show vbModeless, frmOwner
End Function

Private Sub cmddel_Click(Index As Integer)
Dim sFileas As String, nCount As Long, i As Long, sNTlogon As String, ncount2 As Long

    If MsgBox("Are you sure you want to Delete chosen Contact(s). You will not be able to undelete.", vbQuestion + vbYesNo) = vbNo Then Exit Sub
 Screen.MousePointer = vbHourglass
 
    Select Case Index
        Case 0:
        Case 1: oContacts.contype = ExchangePrivate
        Case 2: oContacts.contype = ExchangePublic
        Case 3: oContacts.contype = OutlookPrivate
        Case 4: oContacts.contype = AllExchange
    End Select
    If Index <> 0 Then If MsgBox("Are you positive that you want to do this.", vbQuestion + vbYesNo) = vbNo Then Screen.MousePointer = vbDefault: Exit Sub
    If Not oContacts.DelContacts(oCurUser.UserName, nCount, , IIf(Index = 0, DBID, "")) Then
        MsgBox "Deletion of Contact(s) failed."
    End If
    Unload Me
    Screen.MousePointer = vbDefault
 
    If nCount = 1 Then
        MsgBox "Contact Deleted sucessfully."
    ElseIf nCount > 1 Then
        'MsgBox nCount & " Contacts Deleted sucessfully."
    Else
        MsgBox "There were no Contacts to Delete."
        Exit Sub
    End If
    frmaddressbook.ShowContacts
End Sub

Private Sub Form_Activate()
   If cmddel(0).Enabled Then
    cmddel(0).SetFocus
    Else
    cmddel(1).SetFocus
    End If
End Sub

Private Sub Form_Load()
    Hide
End Sub

Private Sub Form_Unload(Cancel As Integer)
bDontactivate = True
frmaddressbook.Enabled = True
    frmaddressbook.grdcontacts.Row = 0
    'frmaddressbook.Timer1.Enabled = True
End Sub

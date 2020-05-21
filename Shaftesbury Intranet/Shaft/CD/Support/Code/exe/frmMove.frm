VERSION 5.00
Begin VB.Form frmMove 
   BorderStyle     =   4  'Fixed ToolWindow
   Caption         =   "Move Contacts..."
   ClientHeight    =   3450
   ClientLeft      =   45
   ClientTop       =   285
   ClientWidth     =   4815
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   3450
   ScaleWidth      =   4815
   ShowInTaskbar   =   0   'False
   StartUpPosition =   1  'CenterOwner
   Begin VB.Frame Frame2 
      BorderStyle     =   0  'None
      Height          =   1575
      Left            =   2520
      TabIndex        =   5
      Top             =   840
      Width           =   2055
      Begin VB.OptionButton optfrom 
         Caption         =   "Company Contacts"
         Height          =   375
         Index           =   5
         Left            =   0
         TabIndex        =   12
         Top             =   960
         Width           =   1815
      End
      Begin VB.OptionButton optfrom 
         Caption         =   "Private Contacts"
         Height          =   375
         Index           =   4
         Left            =   0
         TabIndex        =   11
         Top             =   600
         Width           =   1575
      End
      Begin VB.OptionButton optfrom 
         Caption         =   "Outlook"
         Enabled         =   0   'False
         Height          =   375
         Index           =   3
         Left            =   0
         TabIndex        =   10
         Top             =   240
         Width           =   1575
      End
      Begin VB.Label Label1 
         Caption         =   "To:"
         Height          =   255
         Index           =   1
         Left            =   0
         TabIndex        =   6
         Top             =   0
         Width           =   975
      End
   End
   Begin VB.Frame Frame1 
      BorderStyle     =   0  'None
      Height          =   1455
      Left            =   240
      TabIndex        =   3
      Top             =   840
      Width           =   2175
      Begin VB.OptionButton optfrom 
         Caption         =   "Company Contacts"
         Height          =   375
         Index           =   2
         Left            =   120
         TabIndex        =   9
         Top             =   960
         Width           =   1935
      End
      Begin VB.OptionButton optfrom 
         Caption         =   "Private Contacts"
         Height          =   375
         Index           =   1
         Left            =   120
         TabIndex        =   8
         Top             =   600
         Width           =   1575
      End
      Begin VB.OptionButton optfrom 
         Caption         =   "Outlook"
         Height          =   375
         Index           =   0
         Left            =   120
         TabIndex        =   7
         Top             =   240
         Width           =   1575
      End
      Begin VB.Label Label1 
         Caption         =   "From:"
         Height          =   255
         Index           =   0
         Left            =   0
         TabIndex        =   4
         Top             =   0
         Width           =   975
      End
   End
   Begin VB.OptionButton Option1 
      Caption         =   "Move Selected"
      Height          =   375
      Index           =   1
      Left            =   2160
      TabIndex        =   2
      Top             =   120
      Width           =   1455
   End
   Begin VB.OptionButton Option1 
      Caption         =   "Move All"
      Height          =   375
      Index           =   0
      Left            =   960
      TabIndex        =   1
      Top             =   120
      Width           =   1455
   End
   Begin VB.CommandButton cmdmove 
      Caption         =   "Move Contacts"
      Height          =   615
      Left            =   600
      TabIndex        =   0
      Top             =   2520
      Width           =   3495
   End
   Begin VB.Shape Shape2 
      BorderWidth     =   2
      Height          =   2535
      Left            =   120
      Top             =   720
      Width           =   4575
   End
End
Attribute VB_Name = "frmMove"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Dim bDontRun As Boolean, frm As Form
Dim ThisContact As Object, nFrom As Long, nTo As Long, DBID As String
Public Function LoadForm(Optional oContact, Optional frmOwner As Form, Optional aDBID = 0) As Boolean
Dim sNTlogon As String

DBID = aDBID
    If IsMissing(oContact) Then
        Option1(1).Enabled = False
        Option1(0).Value = 1
        optfrom(1).Value = True
        optfrom(5).Value = True
    Else
        Option1(1).Value = 1
        On Error Resume Next
        If Not oContact.User1 = "" Then
         optfrom(0).Enabled = False
            optfrom(2).Enabled = False
            optfrom(1).Value = True
            optfrom(5).Value = True
        Else
                optfrom(0).Enabled = False
            optfrom(1).Enabled = False
            optfrom(2).Value = True
            optfrom(4).Value = True
        End If
    End If
    If Not IsMissing(oContact) Then Set ThisContact = oContact
    Set frm = frmOwner
    Show vbModeless, frmOwner
End Function

Private Function BuildString() As String
Dim i As Long, sExtra1 As String, sExtra2 As String, sExtra3 As String, bdontdoagain As Boolean

  Select Case Option1(0).Value
        Case True: sExtra1 = "ALL the Contacts from the "
        Case False: sExtra1 = "the selected Contact from the "
    End Select
    
    For i = 0 To optfrom.Count - 1
        If optfrom(i).Value = True Then
            Select Case i
            Case 0, 3: sExtra2 = "Outlook Contacts list"
            Case 1, 4: sExtra2 = "Private Contacts list"
            Case 2, 5: sExtra2 = "Company Contacts list"
            End Select
        End If
        If sExtra2 <> "" And Not bdontdoagain Then sExtra1 = sExtra1 & sExtra2 & " to the ": sExtra2 = "": bdontdoagain = True
    Next i
    BuildString = sExtra1 & sExtra2
End Function
Private Sub cmdmove_Click()
Dim dstart As Date, i As Long
Dim sNTlogon As String, fromtype As ContactType, nNoMoved As Long, bok1 As Boolean
    
    
If MsgBox("Are you sure that you want to move " & BuildString & ".", vbQuestion + vbYesNo) = vbYes Then
    Screen.MousePointer = vbHourglass
    For i = 0 To 2
        If optfrom(i).Value Then nFrom = i: Exit For
    Next i
    bok1 = (i = 1 Or i = 2)
        For i = 3 To 5
        If optfrom(i).Value Then nTo = i - 3: Exit For
    Next i
'    If bok1 Then
'        If i = 4 Then
'            With frmaddressbook.grdcontacts
'                For i = 0 To .cols - 1
'                    .Col = i
'                    .CellForeColor = oCurUser.Settings.PrivateCol
'                Next i
'            End With
'        ElseIf i = 5 Then
'            With frmaddressbook.grdcontacts
'                For i = 0 To .cols - 1
'                    .Col = i
'                    .CellForeColor = oCurUser.Settings.CompanyCol
'                Next i
'            End With
'        End If
'    End If
    If Not oContacts.MoveContacts(Option1(0).Value, oCurUser.UserName, nFrom, nTo, nNoMoved, DBID) Then
        MsgBox "Move Failed"
    End If
End If

        Screen.MousePointer = vbDefault
        frmaddressbook.ShowContacts frmaddressbook.oAZ.CurrentLetter
        Unload Me
        If nNoMoved = 0 Then
            MsgBox "There were no Contacts to move."
        Else
            MsgBox "Moved " & nNoMoved & " Contact(s) successfully." '& "Time elapsed: " & DateDiff("s", dstart, Now)
        End If

End Sub

Private Sub Form_Load()
Hide
End Sub

Private Sub Form_Unload(Cancel As Integer)

frm.grdcontacts.Row = 0
frm.Enabled = True
'frmaddressbook.Timer1.Enabled = True
End Sub

Private Sub optfrom_Click(Index As Integer)
Dim i As Long
Static bDontRun As Boolean
If Not bDontRun And Index <> 5 Then
    If Index = 0 Then Option1(1).Enabled = Not optfrom(0).Value
    bDontRun = True
    optfrom(5).Value = Abs(Not (Index = 2))
    optfrom(4).Value = Abs(Not (Index = 1))
    bDontRun = False
    optfrom(4).Enabled = Not (Index = 1)
    optfrom(5).Enabled = Not (Index = 2)
    If Index < 3 Then
        nFrom = Index
    Else
        nTo = Index - 3
    End If
End If
End Sub

Private Sub Option1_Click(Index As Integer)
   optfrom(0).Enabled = True
    optfrom(1).Enabled = True
    optfrom(2).Enabled = True
    optfrom(3).Enabled = False
    optfrom(4).Enabled = False
    optfrom(5).Enabled = True
     optfrom_Click (1)
End Sub

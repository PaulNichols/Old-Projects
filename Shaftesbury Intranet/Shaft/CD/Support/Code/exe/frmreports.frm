VERSION 5.00
Begin VB.Form frmreports 
   BorderStyle     =   4  'Fixed ToolWindow
   Caption         =   "Reports..."
   ClientHeight    =   3600
   ClientLeft      =   45
   ClientTop       =   285
   ClientWidth     =   6510
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   3600
   ScaleWidth      =   6510
   ShowInTaskbar   =   0   'False
   StartUpPosition =   1  'CenterOwner
   WindowState     =   2  'Maximized
   Begin VB.CommandButton cmdoptions 
      Caption         =   "Abbreviated Fax List"
      Enabled         =   0   'False
      Height          =   495
      Index           =   7
      Left            =   3480
      TabIndex        =   9
      Top             =   720
      Width           =   2895
   End
   Begin VB.CommandButton cmdoptions 
      Caption         =   "Abbreviated Phone List"
      Enabled         =   0   'False
      Height          =   495
      Index           =   6
      Left            =   3480
      TabIndex        =   8
      Top             =   120
      Width           =   2895
   End
   Begin VB.OptionButton optprint 
      Caption         =   "Preview"
      Height          =   375
      Index           =   1
      Left            =   2040
      Style           =   1  'Graphical
      TabIndex        =   6
      Top             =   3120
      Value           =   -1  'True
      Width           =   1215
   End
   Begin VB.OptionButton optprint 
      Caption         =   "Print"
      Height          =   375
      Index           =   0
      Left            =   360
      Style           =   1  'Graphical
      TabIndex        =   7
      Top             =   3120
      Width           =   1215
   End
   Begin VB.CommandButton cmdoptions 
      Caption         =   "Members of a Distribution list"
      Enabled         =   0   'False
      Height          =   495
      Index           =   5
      Left            =   2760
      TabIndex        =   5
      Top             =   2520
      Visible         =   0   'False
      Width           =   2895
   End
   Begin VB.CommandButton cmdoptions 
      Caption         =   "Results of Search"
      Height          =   495
      Index           =   4
      Left            =   360
      TabIndex        =   4
      Top             =   2520
      Width           =   2895
   End
   Begin VB.CommandButton cmdoptions 
      Caption         =   "All Company && Private Contacts"
      Height          =   495
      Index           =   3
      Left            =   360
      TabIndex        =   3
      Top             =   1920
      Width           =   2895
   End
   Begin VB.CommandButton cmdoptions 
      Caption         =   "Selected Contacts Details"
      Enabled         =   0   'False
      Height          =   495
      Index           =   2
      Left            =   360
      TabIndex        =   2
      Top             =   120
      Width           =   2895
   End
   Begin VB.CommandButton cmdoptions 
      Caption         =   "List of all Private Contacts"
      Height          =   495
      Index           =   1
      Left            =   360
      TabIndex        =   1
      Top             =   1320
      Width           =   2895
   End
   Begin VB.CommandButton cmdoptions 
      Caption         =   "List of all Company Contacts"
      Height          =   495
      Index           =   0
      Left            =   360
      TabIndex        =   0
      Top             =   720
      Width           =   2895
   End
End
Attribute VB_Name = "frmreports"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private frm As Form
Private oContact As clsContact, ocontacts2 As clsContact

Private Sub cmdoptions_Click(Index As Integer)
Dim rs As Recordset
    'FILTER
    Set ocontacts2 = New clsContact
    ocontacts2.InitializeOutlook
    With rptshaft1
        Select Case Index
        Case 0 ' all company contacts
            ocontacts2.contype = ExchangePublic
            ocontacts2.RetriveFromDB "", rs, "*", frmaddressbook.bDesc, frmaddressbook.mnufeilds(oCurUser.Settings.ContactsGridViewSortedBy).Caption, False, 2
            Set .rptRS.Recordset = rs
        Case 1 'all private
            ocontacts2.contype = ExchangePrivate
            ocontacts2.RetriveFromDB oCurUser.UserName, rs, "*", frmaddressbook.bDesc, frmaddressbook.mnufeilds(oCurUser.Settings.ContactsGridViewSortedBy).Caption, False, 1
            Set .rptRS.Recordset = rs
        Case 2
            'on error  resume next
            Dim oObj As Object
            Set oObj = oContacts.GetOutlookObjectFromEntryId(oContact.EntryId)
            oObj.printout:    MsgBox "Contact sent to printer": Exit Sub  'single contact
            Set oObj = Nothing
           ' oContact.EntryId
           ' oContact.printout
        Case 3 'private & company
            ocontacts2.contype = AllExchange
             ocontacts2.RetriveFromDB oCurUser.UserName, rs, "*", frmaddressbook.bDesc, frmaddressbook.mnufeilds(oCurUser.Settings.ContactsGridViewSortedBy).Caption
             Set .rptRS.Recordset = rs
        Case 4: Set .rptRS = frmaddressbook.rs
        Case 6, 7
            If Index = 6 Then
                If MsgBox("Would you like to Alphabetically Order the list.", vbYesNo + vbQuestion) = vbYes Then
                    Screen.MousePointer = 11
                    Set rs = oContacts.GetPhoneAbbr(True)
                    Screen.MousePointer = 0
                Else
                    Set rs = oContacts.GetPhoneAbbr(False)
                End If
            Else
                 If MsgBox("Would you like to Alphabetically Order the list.", vbYesNo + vbQuestion) = vbYes Then
                    Screen.MousePointer = 11
                    Set rs = oContacts.GetFaxAbbr(True)
                    Screen.MousePointer = 0
                Else
                    Set rs = oContacts.GetFaxAbbr(False)
                End If
            End If
            Set rptAbbr.rptRS.Recordset = rs
            If rptAbbr.rptRS.Recordset.EOF Then
                MsgBox "There are no records to print."
            Else
                If optprint(0) Then
                    rptAbbr.PrintReport True
                Else
                    rptAbbr.TOCEnabled = False
                     Select Case Index
                       Case 6: rptAbbr.lblheader.Caption = "Abbreviated Phone List"
                       Case 7: rptAbbr.lblheader.Caption = "Abbreviated Fax List"
                    End Select
                    rptAbbr.lblheader.Caption = rptAbbr.lblheader.Caption & " - " & Format(Date, "mmmm yyyy")
                    rptAbbr.Show
                End If
            End If
            Exit Sub
        End Select
                
        
        If .rptRS.Recordset.EOF Then
            MsgBox "There are no Contacts to print."
        Else
      
            If optprint(0) Then
                .PrintReport True
            Else
                .TOCEnabled = False
                 Select Case Index
                   Case 0: .lblheader.Caption = "List of All Company Contacts"
                   Case 1: .lblheader.Caption = "List of All Private Contacts"
                   Case 3: .lblheader.Caption = "List of Company And Private Contacts"
                End Select
                .Show
            End If
           
        End If
    End With
    Exit Sub
errcode:
MsgBox "There was an error with the printing."
End Sub

Private Sub Form_Load()
Hide
End Sub

Public Function LoadForm(frmOwner As Form, Optional aContact) As Boolean
Set frm = frmOwner
WindowState = vbNormal
frmaddressbook.Enabled = False
    
cmdoptions(2).Enabled = (Not IsMissing(aContact))
cmdoptions(4).Enabled = (frmaddressbook.oAZ.CurrentLetter = "?")
cmdoptions(6).Enabled = True
cmdoptions(7).Enabled = True
If Not IsMissing(aContact) Then
    Set oContact = aContact
End If

Show , frm
End Function

Private Sub Form_Unload(Cancel As Integer)
'frmaddressbook.ShowContacts frmaddressbook.oAZ.CurrentLetter
'ocontacts2.ResetAfterPrint
Set ocontacts2 = Nothing
bDontactivate = True
frmaddressbook.Enabled = True
'frmaddressbook.Timer1.Enabled = True
End Sub


VERSION 5.00
Begin VB.Form frmLabels 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Labels"
   ClientHeight    =   1440
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   4140
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   1440
   ScaleWidth      =   4140
   ShowInTaskbar   =   0   'False
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton cmdOK 
      Caption         =   "OK"
      Height          =   315
      Left            =   2760
      TabIndex        =   5
      Top             =   960
      Width           =   1215
   End
   Begin VB.ComboBox cboColumn 
      Enabled         =   0   'False
      Height          =   315
      Left            =   1080
      Style           =   2  'Dropdown List
      TabIndex        =   2
      Top             =   960
      Width           =   1335
   End
   Begin VB.ComboBox cboRow 
      Enabled         =   0   'False
      Height          =   315
      Left            =   1080
      Style           =   2  'Dropdown List
      TabIndex        =   1
      Top             =   480
      Width           =   1335
   End
   Begin VB.CheckBox chkAll 
      Caption         =   "Print To All Labels"
      Height          =   255
      Left            =   240
      TabIndex        =   0
      Top             =   120
      Value           =   1  'Checked
      Width           =   1575
   End
   Begin VB.Label Label2 
      Caption         =   "Column :"
      Enabled         =   0   'False
      Height          =   255
      Left            =   240
      TabIndex        =   4
      Top             =   960
      Width           =   735
   End
   Begin VB.Label Label1 
      Caption         =   "Row :"
      Enabled         =   0   'False
      Height          =   255
      Left            =   240
      TabIndex        =   3
      Top             =   480
      Width           =   735
   End
End
Attribute VB_Name = "frmLabels"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub chkAll_Click()
Dim bEnabled As Boolean

    bEnabled = Not CBool(chkAll.value)
    Label1.Enabled = bEnabled
    Label2.Enabled = bEnabled
    cboRow.Enabled = bEnabled
    cboColumn.Enabled = bEnabled
End Sub

Private Sub cmdOK_Click()
    bDontPrintLabels = False
    Hide
End Sub

Private Sub Form_Load()
    bDontPrintLabels = False
    With cboColumn
        .AddItem "1"
        .AddItem "2"
        .ListIndex = 0
    End With
    
    With cboRow
        .AddItem "1"
        .AddItem "2"
        .AddItem "3"
        .AddItem "4"
        .AddItem "5"
        .AddItem "6"
        .AddItem "7"
        .ListIndex = 0
    End With
End Sub

Private Sub Form_Unload(Cancel As Integer)
bDontPrintLabels = True
End Sub

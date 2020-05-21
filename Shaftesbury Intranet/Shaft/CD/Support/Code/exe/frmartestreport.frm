VERSION 5.00
Object = "{698E14D0-8B82-11D1-8B57-00A0C98CD92B}#1.0#0"; "ARVIEWER.OCX"
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   3195
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   4680
   LinkTopic       =   "Form1"
   ScaleHeight     =   3195
   ScaleWidth      =   4680
   StartUpPosition =   3  'Windows Default
   Begin DDActiveReportsViewerCtl.ARViewer ARViewer1 
      Height          =   1215
      Left            =   360
      TabIndex        =   0
      Top             =   600
      Width           =   2295
      _ExtentX        =   4048
      _ExtentY        =   2143
      SectionData     =   "frmartestreport.frx":0000
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Sub ARViewer1_MouseDown(Button As Integer, Shift As Integer, x As Single, y As Single)

End Sub

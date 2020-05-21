VERSION 5.00
Object = "{E7BC34A0-BA86-11CF-84B1-CBC2DA68BF6C}#1.0#0"; "NTSVC.ocx"
Begin VB.Form frmService 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Contacts Service Manager"
   ClientHeight    =   2205
   ClientLeft      =   1380
   ClientTop       =   1605
   ClientWidth     =   4545
   Icon            =   "frmService.frx":0000
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   PaletteMode     =   1  'UseZOrder
   ScaleHeight     =   2205
   ScaleWidth      =   4545
   Begin VB.CommandButton Command2 
      Caption         =   "Create DB"
      Height          =   495
      Left            =   2520
      TabIndex        =   7
      Top             =   1080
      Width           =   1215
   End
   Begin VB.CommandButton Command1 
      Caption         =   "DB Location"
      Height          =   495
      Left            =   2520
      TabIndex        =   6
      Top             =   480
      Width           =   1215
   End
   Begin VB.CheckBox Check1 
      Caption         =   "Auto-Start service when OS starts"
      Height          =   495
      Left            =   2520
      TabIndex        =   5
      Top             =   1680
      Width           =   1935
   End
   Begin VB.CommandButton cmdOptions 
      Height          =   615
      Index           =   2
      Left            =   120
      Picture         =   "frmService.frx":000C
      Style           =   1  'Graphical
      TabIndex        =   2
      Top             =   960
      Width           =   735
   End
   Begin VB.CommandButton cmdOptions 
      Height          =   615
      Index           =   0
      Left            =   120
      Picture         =   "frmService.frx":044E
      Style           =   1  'Graphical
      TabIndex        =   1
      Top             =   240
      Width           =   735
   End
   Begin VB.Timer Timer 
      Interval        =   1000
      Left            =   3120
      Top             =   0
   End
   Begin NTService.NTService NTService 
      Left            =   3120
      Top             =   120
      _Version        =   65536
      _ExtentX        =   741
      _ExtentY        =   741
      _StockProps     =   0
      DisplayName     =   "Sample NT Service"
      ServiceName     =   "SampleService"
      StartMode       =   3
   End
   Begin VB.Label Label2 
      Caption         =   "&St&op"
      Height          =   255
      Index           =   2
      Left            =   960
      TabIndex        =   4
      Top             =   1080
      Width           =   1215
   End
   Begin VB.Label Label2 
      Caption         =   "&Start"
      Height          =   255
      Index           =   0
      Left            =   960
      TabIndex        =   3
      Top             =   360
      Width           =   1215
   End
   Begin VB.Label Label1 
      Alignment       =   2  'Center
      Caption         =   "Label1"
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   18
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   615
      Left            =   0
      TabIndex        =   0
      Top             =   0
      Visible         =   0   'False
      Width           =   3555
   End
End
Attribute VB_Name = "frmService"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Dim StopService As Boolean

Private Sub Check1_Click()
If Check1 Then
    NTService.StartMode = svcStartAutomatic
End If
End Sub

Private Sub cmdOptions_Click(Index As Integer)
Select Case Index
Case 0
    cmdOptions(0).Enabled = False
    cmdOptions(2).Enabled = True
    NTService.StartService
Case 2
    cmdOptions(0).Enabled = True
    cmdOptions(2).Enabled = False
    NTService.StopService
End Select
End Sub

Private Sub Command1_Click()
 sDb = Browse("c:\", hwnd, "Choose the Global Templates Directory where subtemplates lives.")
    If Len(sDb) = 0 Then
        MsgBox "You must select a valid directory.", vbInformation
    Else
        NTService.SaveSetting "Parameters", "db", sDb
    End If
End Sub

Private Sub Command2_Click()
DoStuff
End Sub

Private Sub Form_Load()
    On Error GoTo ServiceError
    StopService = False
    
    
    Label1.Caption = "Loading"
    NTService.StartMode = svcStartAutomatic
   Check1.Value = Abs(CBool(NTService.StartMode = svcStartAutomatic))
    NTService.DisplayName = "Contacts Service Manager"
    'Install the service
    
    If Command = "/i" Then
        ' enable interaction with desktop
        NTService.Interactive = True
        'Install the program as an NT service
        If NTService.Install Then
            
            MsgBox NTService.DisplayName & ": installed successfully"
        Else
            MsgBox NTService.DisplayName & ": failed to install"
        End If
        'Unload Me
    'Remove the Service Registry Keys and uninstall the service
    ElseIf Command = "/u" Then
        If NTService.Uninstall Then
            MsgBox NTService.DisplayName & ": uninstalled successfully"
        Else
            MsgBox NTService.DisplayName & ": failed to uninstall"
        End If
        Unload Me
    'Invalid parameter
    ElseIf Command <> "" Then
        MsgBox "Invalid Parameter"
        Unload Me
    End If
    'Retrive the stored value for the timer interval
    
    ' enable Pause/Continue. Must be set before StartService
    ' is called or in design mode
    NTService.ControlsAccepted = svcCtrlPauseContinue
    ' connect service to Windows NT services controller
    NTService.StartService
        cmdOptions(0).Enabled = False
    cmdOptions(2).Enabled = True
    Exit Sub
ServiceError:
    Call NTService.LogEvent(svcMessageError, svcEventError, "[" & Err.Number & "] " & Err.Description)
End Sub

'Unload the Service
Private Sub Form_Unload(Cancel As Integer)
    If Not StopService Then
        If MsgBox("Are you sure you want to unload the service?..." & vbCrLf & "the service will be stopped", vbQuestion + vbYesNo, "Stop Service") = vbYes Then
            NTService.StopService
            Label1.Caption = "Stopping"
            Cancel = True
        Else
            Cancel = True
        End If
    End If
End Sub

Private Sub NTService_Continue(Success As Boolean)
    'Handle the continue service event
    On Error GoTo ServiceError
    Timer.Enabled = True
    Label1.Caption = "Running"
    Success = True
    NTService.LogEvent svcEventInformation, svcMessageInfo, "Service continued"
    Exit Sub
ServiceError:
    NTService.LogEvent svcMessageError, svcEventError, "Contacts:[" & Err.Number & "] " & Err.Description
End Sub

Private Sub NTService_Control(ByVal mEvent As Long)
    'Take control of the service events
    On Error GoTo ServiceError
    Label1.Caption = NTService.DisplayName & " Control signal " & CStr([mEvent])
    Exit Sub
ServiceError:
    NTService.LogEvent svcMessageError, svcEventError, "Contacts:[" & Err.Number & "] " & Err.Description
End Sub

Private Sub NTService_Pause(Success As Boolean)
    'Pause Event Request
    On Error GoTo ServiceError
    Timer.Enabled = False
    Label1.Caption = "Paused"
    NTService.LogEvent svcEventError, svcMessageError, "Service paused"
    Success = True
    Exit Sub
ServiceError:
    NTService.LogEvent svcMessageError, svcEventError, "Contacts:[" & Err.Number & "] " & Err.Description
End Sub

Private Sub NTService_Start(Success As Boolean)
    'Start Event Request
    On Error GoTo ServiceError
     Label1.Caption = "Started"
   Timer.Enabled = True
    Success = True
    Exit Sub
ServiceError:
    NTService.LogEvent svcMessageError, svcEventError, "Contacts:[" & Err.Number & "] " & Err.Description
End Sub

Private Sub NTService_Stop()
    'Stop and terminate the Service
    On Error GoTo ServiceError
    Label1.Caption = "Stopped"
    StopService = True
ServiceError:
    NTService.LogEvent svcMessageError, svcEventError, "Contacts:[" & Err.Number & "] " & Err.Description
End Sub

Private Sub Timer_Timer()
DoEvents
Label1.Caption = IIf(NTService.Running, "Running", "Stopped")
If Time = "00:00:00" Then
  DoStuff
End If
    Exit Sub
ServiceError:
    NTService.LogEvent svcMessageError, svcEventError, "Contacts:[" & Err.Number & "] " & Err.Description
End Sub

Private Sub DoStuff()
  Timer.Enabled = False
    cmdOptions(2).Enabled = False
    Command1.Enabled = False
    DoEvents
    UpdateDB
    cmdOptions(2).Enabled = True
    Command1.Enabled = True
    Timer.Enabled = True
End Sub
Private Function UpdateDB() As Boolean

On Error GoTo errcode

sDb = NTService.GetSetting("Parameters", "db", "")
If Len(sDb) = 0 Then
    sDb = Browse("c:\", hwnd, "Choose the Global Templates Directory where subtemplates lives.")
    If Len(sDb) = 0 Then
        MsgBox "You must select a valid directory.", vbInformation
        GoTo errcode
    Else
        NTService.SaveSetting "Parameters", "db", sDb
    End If
End If

sDbname = "offlinecontacts2.mdb"

If Len(Dir(sDb & "\" & sDbname)) > 0 Then
    Kill sDb & "\" & sDbname
End If
InitializeOutlook

'Set objFolder = gOLNameSpace.Folders("Offline").Folders("Contacts")
Set objFolder = gOLNameSpace.Folders("Public Folders").Folders("All Public Folders").Folders("Contacts")
If objFolder Is Nothing Then GoTo errcode
If Not CreateDB(sDb & "\" & sDbname, objFolder) Then GoTo errcode
If Not AddContacts(sDb & "\" & sDbname, objFolder) Then GoTo errcode

'Set objFolder = gOLNameSpace.Folders("Offline").Folders("Users")
Set objFolder = gOLNameSpace.Folders("Public Folders").Folders("All Public Folders").Folders("Users")
If objFolder Is Nothing Then GoTo errcode
If Not CreateUserDB(sDb & "\" & sDbname, objFolder) Then GoTo errcode
If Not AddUsers(sDb & "\" & sDbname, objFolder) Then GoTo errcode
Exit Function

errcode:
UpdateDB = False
NTService.LogEvent svcMessageError, svcEventError, "Contacts:[" & Err.Number & "] " & Err.Description
End Function

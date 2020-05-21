Attribute VB_Name = "modmain"
Option Explicit
Public Declare Function GetCurrentProcess Lib "KERNEL32" () As Long
Public Declare Function GetExitCodeProcess Lib "KERNEL32" (ByVal hProcess As Long, lpExitCode As Long) As Long
Public Declare Sub ExitProcess Lib "KERNEL32" (ByVal uExitCode As Long)
Public sOldTemplatePath As String
Public Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Long, ByVal wMsg As Long, ByVal wParam As Long, lParam As Any) As Long
Public Const WM_SETREDRAW = &HB
Public Declare Function SetTimer Lib "user32" (ByVal hwnd As Long, ByVal nIDEvent As Long, ByVal uElapse As Long, ByVal lpTimerFunc As Long) As Long
Public Declare Function KillTimer Lib "user32" (ByVal hwnd As Long, ByVal nIDEvent As Long) As Long
Public bDontLoad As Boolean
Private Declare Function WindowFromPoint Lib "user32" (ByVal xPoint As Long, ByVal yPoint As Long) As Long
Public bShrunk As Boolean
Public Enum ContactType
    OutlookPrivate = 0
    Exchangeprivate = 1
    Exchangepublic = 2
    AllExchange = 3
End Enum
Public Const MOUSEEVENTF_ABSOLUTE = &H8000

Public Declare Sub mouse_event Lib "user32" (ByVal dwFlags As Long, ByVal dx As Long, ByVal dy As Long, ByVal cButtons As Long, ByVal dwExtraInfo As Long)
Public oContacts As clsContact
Public NoofContactsLimit As Long
Public bIsOnline As Boolean
Public oCurUser As New clsContact
Public bDontactivate As Boolean
Public Declare Function GetSystemMetrics Lib "user32" (ByVal nIndex As Long) As Long
Public Const SM_CYFULLSCREEN = 17
Public Const SM_CXFULLSCREEN = 16

Public Declare Function LockWindowUpdate Lib "user32" (ByVal hwndLock As Long) As Long

Declare Function CallWindowProc Lib "user32" Alias "CallWindowProcA" (ByVal lpPrevWndFunc As Long, ByVal hwnd As Long, ByVal msg As Long, ByVal wParam As Long, ByVal lParam As Long) As Long


Declare Function SetWindowLong Lib "user32" Alias "SetWindowLongA" (ByVal hwnd As Long, ByVal nIndex As Long, ByVal dwNewLong As Long) As Long


Declare Sub CopyMemory Lib "KERNEL32" Alias "RtlMoveMemory" (pDest As Any, pSrc As Any, ByVal ByteLen As Long)

Type MINMAXINFO
    ptReserved As POINTAPI
    ptMaxSize As POINTAPI
    ptMaxPosition As POINTAPI
    ptMinTrackSize As POINTAPI
    ptMaxTrackSize As POINTAPI
    End Type
    Public Const GWL_WNDPROC = (-4)
    Public Const WM_GETMINMAXINFO = &H24
    Dim lpPrevWndProc As Long
    Public MinWidth As Integer
    Public Minheight As Integer
Public Const WM_SIZE = &H5
Public Const WM_WINDOWPOSCHANGED = &H47


Sub Main()
    If App.PrevInstance Then MsgBox "There is already an Instance of the application running.", vbInformation: End
   'MsgBox "Enable hooks and killprocess"
    bDontLoad = True
    Load FrmLogin
    ReadRegEntry ("")
End Sub



Public Function CheckForMachine(DNS As String) As String

   Dim ECHO As ICMP_ECHO_REPLY
   Dim pos As Long
   Dim success As Long
   Dim sIPAddress As String
   
   Screen.MousePointer = vbHourglass
   If SocketsInitialize() Then
   
     'convert the host name into an IP address
      sIPAddress = GetIPFromHostName(DNS)
      'Text2.Text = sIPAddress
      
     'ping the ip passing the address, text
     'to use, and the ECHO structure
      success = Ping(sIPAddress, "test", ECHO)
      
     'display the results
      CheckForMachine = GetStatusCode(success)
'      text4(1) = ECHO.Address
'      text4(2) = ECHO.RoundTripTime & " ms"
'      text4(3) = ECHO.DataSize & " bytes"
      
'      If Left$(ECHO.Data, 1) <> Chr$(0) Then
'         pos = InStr(ECHO.Data, Chr$(0))
'         text4(4) = Left$(ECHO.Data, pos - 1)
'      End If
'
'      text4(5) = ECHO.DataPointer
'
      SocketsCleanup
      
   Else
   
        CheckForMachine = "Windows Sockets for 32 bit Windows " & _
               "environments is not successfully responding."
   
   End If
   Screen.MousePointer = vbDefault
End Function
'
'
'Public Function SetupOffline(Optional sUser, Optional bFull = False) As Boolean
'
'Dim i As Long, sCurUser As String, ndeleted As Long, oNewContacts As clsContact
'
'    On Error GoTo errcode
'    sCurUser = oCurUser.UserName
'    Set oContacts = Nothing
'
'    Kill "c:\shaftoffline_" & IIf(IsMissing(sUser), sCurUser, sUser) & ".pst"
'    If bFull Then FileCopy oCurUser.Settings.PST, "c:\shaftoffline_" & IIf(IsMissing(sUser), sCurUser, sUser) & ".pst"
'    CreatePrf IIf(IsMissing(sUser), sCurUser, sUser)
'    Shell (App.Path & "\newprof -p " & Chr$(34) & App.Path & "\" & IIf(IsMissing(sUser), sCurUser, sUser) & ".prf" & Chr$(34))
'    Set oNewContacts = New clsContact
'    oNewContacts.InitializeOutlook
'    oNewContacts.movetoofflinepst "c:\shaftoffline_" & IIf(IsMissing(sUser), sCurUser, sUser) & ".pst"
'    oCurUser.Settings.OfflinePST = "c:\shaftoffline_" & IIf(IsMissing(sUser), sCurUser, sUser)
'    Set oNewContacts = Nothing
'    Set oContacts = New clsContact
'    oContacts.InitializeOutlook
'    If Not oContacts.Logon(oCurUser.Settings.Profile) Then GoTo errcode
'    frmaddressbook.ShowContacts IIf(frmaddressbook.oAZ.CurrentLetter = "?", "*", frmaddressbook.oAZ.CurrentLetter)
'    SetupOffline = True
'    Exit Function
'errcode:
'    If Err.Number = 53 Or Err.Number = 75 Then Resume Next
'    SetupOffline = False
'End Function
'

Public Function OnLineOffline() As Boolean
On Error GoTo errcode
    If bIsOnline Then 'go off line
       oCurUser.Settings.OnlinePST = oCurUser.Settings.PST
        oCurUser.Settings.PST = oCurUser.Settings.OfflinePST
         oCurUser.Settings.onlineProfile = oCurUser.Settings.Profile
        oCurUser.Settings.Profile = oCurUser.Settings.offlineprofile
        oContacts.contype = Offline
    Else  'online
       oCurUser.Settings.OfflinePST = oCurUser.Settings.PST
       oCurUser.Settings.PST = oCurUser.Settings.OnlinePST
        oCurUser.Settings.offlineprofile = oCurUser.Settings.Profile
        oCurUser.Settings.Profile = oCurUser.Settings.onlineProfile
        oContacts.contype = oCurUser.Settings.ContactsView
    End If
    
    Set oContacts = Nothing
    Set oContacts = New clsContact
    If oContacts.Logon(oCurUser.Settings.Profile, "") Then
        frmaddressbook.ShowContacts oCurUser.Settings.CurrentLetter
    Else
      GoTo errcode
    End If
    
 OnLineOffline = True
 Exit Function
errcode:
 OnLineOffline = False
End Function
Private Sub CreatePrf(sUser As String)
Dim sPSTPath As String

On Error GoTo errcode
    Kill App.Path & "\" & sUser & ".prf"

    Open App.Path & "\" & sUser & ".prf" For Output As #1
    Print #1, "[General]"
    Print #1, "ProfileName = ShaftOffLine"
    Print #1, "DefaultProfile = No"
    Print #1, "OverwriteProfile = yes"
    Print #1, "DefaultStore = Service1"
    Print #1, "[Service List]"
    Print #1, "Service1=Personal Folders"
    Print #1, "[Service1]"
    sPSTPath = "c:\shaftoffline_" & sUser & ".pst"
    Print #1, "PathToPersonalFolders = " & sPSTPath
    Print #1, "[Personal Folders]"
    Print #1, "ServiceName=MSPST MS"
    Print #1, "PathToPersonalFolders=PT_STRING8,0x6700"
    Close #1
Exit Sub

errcode:
If Err.Number = 53 Then
    Resume Next
End If
End Sub

Public Function GetCols(sCols As String, Colnames() As String) As Long
Dim i As Long, start As Long, newstart As Long
    
    start = 1
    newstart = 1
    Do While newstart <> 0
        newstart = InStr(start, sCols, "|")
        If newstart > 0 Then
            i = i + 1
            ReDim Preserve Colnames(i)
            Colnames(i) = Mid$(sCols, start, (newstart - start))
            start = newstart + 1
        End If
    Loop
    GetCols = i
End Function

Public Function Proper(AnyValue As Variant) As Variant
Dim ptr As Integer
Dim TheString As String
Dim currChar As String, prevChar As String

        If IsNull(AnyValue) Then
            Exit Function
        End If
        TheString = CStr(AnyValue)
        For ptr = 1 To Len(TheString)
            currChar = Mid$(TheString, ptr, 1)
            Select Case prevChar
                Case "A" To "Z", "a" To "z"
                    Mid(TheString, ptr, 1) = LCase(currChar)
                Case Else
                    Mid(TheString, ptr, 1) = UCase(currChar)
            End Select
            prevChar = currChar
        Next ptr
        Proper = TheString

End Function

Public Sub FormTimerProc(ByVal hwnd As Long, ByVal uMsg As Long, ByVal idEvent As Long, ByVal dwTime As Long)
Dim lHwnd As Long, lPoint As POINTAPI
    If idEvent = 5001 Then
        GetCursorPos lPoint
        If lPoint.x < (frmaddressbook.Width / Screen.TwipsPerPixelX) And frmaddressbook.Width = 60 Then
            frmaddressbook.bDontResize = True
            frmaddressbook.Width = frmaddressbook.lblopoup.Width
            frmaddressbook.bDontResize = False
        ElseIf lPoint.x > (frmaddressbook.Width / Screen.TwipsPerPixelX) And frmaddressbook.Width > 60 Then
            frmaddressbook.bDontResize = True
            frmaddressbook.Width = 60
            frmaddressbook.bDontResize = False
        End If
    End If
End Sub




Public Sub Hook(frm As Form)
    ' HOOK! Place the Call Hook(Me) code in
    '     your desired form
    lpPrevWndProc = SetWindowLong(frm.hwnd, GWL_WNDPROC, AddressOf WindowProc)
End Sub


Function WindowProc(ByVal hw As Long, ByVal uMsg As Long, ByVal wParam As Long, ByVal lParam As Long) As Long
    ' WINDOWPROC! Does the actual subclassin
    '     g

If uMsg = WM_GETMINMAXINFO Then
        Dim MinMax As MINMAXINFO
        CopyMemory MinMax, ByVal lParam, Len(MinMax)
        If Not bShrunk Then
            MinMax.ptMinTrackSize.x = frmaddressbook.oAZ.Width / Screen.TwipsPerPixelX ' Set this to the min width in PIXELS (not twip!)
            MinMax.ptMinTrackSize.y = 6500 / Screen.TwipsPerPixelX ' Set this to the min height in PIXELS (not twip!)
        End If
        CopyMemory ByVal lParam, MinMax, Len(MinMax)
ElseIf WM_SIZE Then
           ' LockWindowUpdate hw
         '    Debug.Print "1"
           WindowProc = CallWindowProc(lpPrevWndProc, hw, uMsg, wParam, lParam)
    ElseIf WM_WINDOWPOSCHANGED Then
        'Debug.Print "2"
           ' LockWindowUpdate 0
           WindowProc = CallWindowProc(lpPrevWndProc, hw, uMsg, wParam, lParam)
     Else
        WindowProc = CallWindowProc(lpPrevWndProc, hw, uMsg, wParam, lParam)
    End If
End Function


Public Sub Unhook(frm As Form)
    ' UNHOOK! Place the code Call Unhook(Me)
    '     in your form's Unload() event
    SetWindowLong frm.hwnd, GWL_WNDPROC, lpPrevWndProc
End Sub

Public Function NullToEmpty(avValue, Optional avDefault) As Variant

    If IsMissing(avDefault) Then
        Select Case VarType(avValue)
            Case vbString: avDefault = ""
            Case vbBoolean: avDefault = False
            Case vbDate: avDefault = DEFAULT_DATE
            Case Else: avDefault = 0
        End Select
    End If

    If IsNull(avValue) Then
        NullToEmpty = avDefault
    Else
        If VarType(avValue) = vbDate Then
            NullToEmpty = IIf(CDate("00:00:00") = avValue, avDefault, avValue)
        Else
            NullToEmpty = avValue
        End If
    End If
End Function

Public Sub HighlightTxT(frm As Form)
Dim oControl As Object
For Each oControl In frm.Controls
    If TypeOf oControl Is TextBox Then
        If oCurUser.Settings.Highlighttext = "1" Then
            oControl.SelStart = 0
            oControl.SelLength = Len(oControl.Text)
        Else
            oControl.SelStart = Len(oControl.Text)
        End If
    End If
Next oControl
End Sub


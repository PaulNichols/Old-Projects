Attribute VB_Name = "modMain"
Option Explicit
Public OldWindowProc As Long
Public Const ISD_TIMER = 5001
Public Type RECT
    Left As Long
    Top As Long
    Right As Long
    Bottom As Long
End Type
Public Type POINTAPI
    X As Long
    Y As Long
End Type
Public Enum EncryptDecrypt
    ENCODE = 1
    DECODE = 2
End Enum
Public Declare Sub mouse_event Lib "user32" (ByVal dwFlags As Long, ByVal dx As Long, ByVal dy As Long, ByVal cButtons As Long, ByVal dwExtraInfo As Long)

Public Declare Sub CopyMemory Lib "kernel32" Alias "RtlMoveMemory" (lpvDest As Any, lpvSource As Any, ByVal cbCopy As Long)
Public Declare Function DrawStateString Lib "user32" Alias "DrawStateA" (ByVal hdc As Long, ByVal hBrush As Long, ByVal lpDrawStateProc As Long, ByVal lpString As String, ByVal cbStringLen As Long, ByVal X As Long, ByVal Y As Long, ByVal cx As Long, ByVal cy As Long, ByVal fuFlags As Long) As Long
Public Declare Function CallWindowProc Lib "user32" Alias "CallWindowProcA" (ByVal lpPrevWndFunc As Long, ByVal hWnd As Long, ByVal Msg As Long, ByVal wParam As Long, ByVal lParam As Long) As Long
Public Declare Function SetWindowLong Lib "user32" Alias "SetWindowLongA" (ByVal hWnd As Long, ByVal nIndex As Long, ByVal dwNewLong As Long) As Long
Public Declare Function GetWindowLong Lib "user32" Alias "GetWindowLongA" (ByVal hWnd As Long, ByVal nIndex As Long) As Long
Public Declare Function GetCurrentObject Lib "gdi32" (ByVal hdc As Long, ByVal uObjectType As Long) As Long
Public Declare Function CreateSolidBrush Lib "gdi32" (ByVal crColor As Long) As Long
Public Declare Function DeleteObject Lib "gdi32" (ByVal hObject As Long) As Long
Public Declare Function SelectObject Lib "gdi32" (ByVal hdc As Long, ByVal hObject As Long) As Long
Public Declare Function MoveToEx Lib "gdi32" (ByVal hdc As Long, ByVal X As Long, ByVal Y As Long, lpPoint As POINTAPI) As Long
Public Declare Function LineTo Lib "gdi32" (ByVal hdc As Long, ByVal X As Long, ByVal Y As Long) As Long
Public Declare Function Rectangle Lib "gdi32" (ByVal hdc As Long, ByVal X1 As Long, ByVal Y1 As Long, ByVal X2 As Long, ByVal Y2 As Long) As Long
Public Declare Function DrawEdge& Lib "user32" (ByVal hdc As Long, qrc As RECT, ByVal edge As Long, ByVal grfFlags As Long)
Public Declare Function GetClientRect Lib "user32" (ByVal hWnd As Long, lpRect As RECT) As Long
Public Declare Function GetCursorPos Lib "user32" (lpPoint As POINTAPI) As Long
Public Declare Function GetWindowRect Lib "user32" (ByVal hWnd As Long, lpRect As RECT) As Long
Public Declare Function DrawFrameControl Lib "user32" (ByVal hdc As Long, lpRect As RECT, ByVal un1 As Long, ByVal un2 As Long) As Long
Declare Function GetVolumeInformation Lib "kernel32" Alias "GetVolumeInformationA" (ByVal lpRootPathName As String, ByVal lpVolumeNameBuffer As String, ByVal nVolumeNameSize As Long, lpVolumeSerialNumber As Long, lpMaximumComponentLength As Long, lpFileSystemFlags As Long, ByVal lpFileSystemNameBuffer As String, ByVal nFileSystemNameSize As Long) As Long
Declare Function LockWindowUpdate Lib "user32" (ByVal hwndLock As Long) As Long
        
Public Declare Function GetDesktopWindow Lib "user32" () As Long
Public Declare Function GetWindowDC Lib "user32" (ByVal hWnd As Long) As Long
Public Declare Function GetTextMetrics Lib "gdi32" Alias "GetTextMetricsA" (ByVal hdc As Long, lpMetrics As TEXTMETRIC) As Long
Public Declare Function SetMapMode Lib "gdi32" (ByVal hdc As Long, ByVal nMapMode As Long) As Long
Public Declare Function ReleaseDC Lib "user32" (ByVal hWnd As Long, ByVal hdc As Long) As Long

'Public Declare Function KillTimer Lib "user32" (ByVal hWnd As Long, ByVal nIDEvent As Long) As Long
'Public Declare Function SetTimer Lib "user32" (ByVal hWnd As Long, ByVal nIDEvent As Long, ByVal uElapse As Long, ByVal lpTimerFunc As Long) As Long

Public Type TEXTMETRIC
   tmHeight As Integer
   tmAscent As Integer
   tmDescent As Integer
   tmInternalLeading As Integer
   tmExternalLeading As Integer
   tmAveCharWidth As Integer
   tmMaxCharWidth As Integer
   tmWeight As Integer
   tmItalic As String * 1
   tmUnderlined As String * 1
   tmStruckOut As String * 1
   tmFirstChar As String * 1
   tmLastChar As String * 1
   tmDefaultChar As String * 1
   tmBreakChar As String * 1
   tmPitchAndFamily As String * 1
   tmCharSet As String * 1
   tmOverhang As Integer
   tmDigitizedAspectX As Integer
   tmDigitizedAspectY As Integer
End Type

Public Const MM_TEXT = 1

Public Const EdgeRaised = &H5
Public Const EdgeSunken = &HA
Public Const EdgeEtched = &H6
Public Const EdgeBump = &H9
Public Const EdgeNone = &H0

Public Const BF_LEFT = &H1
Public Const BF_TOP = &H2
Public Const BF_RIGHT = &H4
Public Const BF_BOTTOM = &H8

Public Const BF_TOPLEFT = &H3
Public Const BF_TOPRIGHT = &H6
Public Const BF_BOTTOMLEFT = &H9
Public Const BF_BOTTOMRIGHT = &HC

Public Const BF_TOPRECT = &H7
Public Const BF_BOTTOMRECT = &HD
Public Const BF_RECT = &HF
Public Const BDR_RAISED = &H5
Public Const BDR_RAISEDOUTER = &H1
Public Const BDR_SUNKENOUTER = &H2
Public Const BDR_SUNKENINNER = &H8

Public CurrentX As Single, CurrentY As Single

Public Function VolumeSerial() As Long
Dim lSerialNo As Long

    GetVolumeInformation "c:\" & Chr$(0), "", 0, lSerialNo, 0, 0, "", 0
    VolumeSerial = lSerialNo

End Function

Public Function Encrypt(sPwdString As String, abEncryptType As Byte, Optional msScramble As String = "soDSMDHnsQ") As String
Dim nCount As Integer, nCode1 As Integer, nCode2 As Integer
Dim sScrambled As String, nStringLength As Integer
    If sPwdString <> "" Then
        nStringLength = Len(msScramble)

        Select Case abEncryptType
            Case ENCODE
                For nCount = 1 To Len(sPwdString)
                    nCode1 = Asc(Mid$(sPwdString, nCount, 1))
                    nCode2 = Asc(Mid$(msScramble, (nCount Mod nStringLength) + 1, 1))
                    nCode1 = nCode1 Xor nCode2
                    sScrambled = sScrambled + Chr$(Asc("z") - (nCode1 \ 16)) + Chr$(Asc("a") + (nCode1 Mod 16))
                Next nCount
            Case DECODE
                For nCount = 1 To Len(sPwdString) Step 2
                    nCode1 = (Asc("z") - Asc(Mid$(sPwdString, nCount, 1))) * 16 + (Asc(Mid$(sPwdString, nCount + 1, 1)) - Asc("a"))
                    nCode2 = Asc(Mid$(msScramble, (((nCount + 1) / 2) Mod nStringLength) + 1, 1))
                    nCode1 = nCode1 Xor nCode2
                    sScrambled = sScrambled + Chr$(nCode1)
                Next nCount
        End Select

        Encrypt = sScrambled
    End If
End Function

Public Function Stuff(ByVal sPwdString As String, asSearch As String, asReplace) As String
Dim i As Integer

    i = InStr(sPwdString, asSearch)
    Do Until i = 0
        sPwdString = Left$(sPwdString, i - 1) + asReplace + Mid$(sPwdString, i + Len(asSearch))
        i = InStr(i + Len(asReplace), sPwdString, asSearch)
    Loop
    Stuff = sPwdString
End Function

Public Sub LockOn(Optional hWnd As Long = 0)
    LockWindowUpdate hWnd
End Sub

Public Sub DrawShadowBox(ctrl As Object, lEdge As Long, r As RECT, BevelWidth As Byte)
Dim Color1 As Long, Color2 As Long, i As Integer

Dim X As Integer, Y As Integer, cx As Integer, cy As Integer
    
    X = r.Left
    Y = r.Top
    cx = r.Right
    cy = r.Bottom
    Select Case lEdge
        Case EdgeSunken
            Color1 = vbButtonShadow
            Color2 = vb3DHighlight
        Case EdgeRaised
            Color1 = vb3DHighlight
            Color2 = vbButtonShadow
        Case Else: Exit Sub
    End Select
    For i = 0 To BevelWidth - 1
        ctrl.Line (X + i, Y + i)-(X + cx - 1 - i, Y + i), Color1
        ctrl.Line (X + i, Y + i)-(X + i, Y + cy - 1 - i), Color1
        ctrl.Line (X + cx - 1 - i, Y + i)-(X + cx - 1 - i, Y + cy - i), Color2
        ctrl.Line (X + i, Y + cy - 1 - i)-(X + cx - i, Y + cy - 1 - i), Color2
    Next i
End Sub

Public Function SmallFonts() As Boolean
Dim hdc As Long, hWnd As Long
Dim PrevMapMode As Long, tm As TEXTMETRIC

    ' Set the default return value to small fonts
    SmallFonts = True

    ' Get the handle of the desktop window
    hWnd = GetDesktopWindow()

    ' Get the device context for the desktop
    hdc = GetWindowDC(hWnd)
    If hdc Then
        ' Set the mapping mode to pixels
        PrevMapMode = SetMapMode(hdc, MM_TEXT)

        ' Get the size of the system font
        GetTextMetrics hdc, tm

        ' Set the mapping mode back to what it was
        PrevMapMode = SetMapMode(hdc, PrevMapMode)

        ' Release the device context
        ReleaseDC hWnd, hdc

        ' If the system font is more than 16 pixels high,
        ' then large fonts are being used
        If tm.tmHeight > 16 Then SmallFonts = False
    End If
End Function


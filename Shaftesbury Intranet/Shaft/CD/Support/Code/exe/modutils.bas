Attribute VB_Name = "modutils"
Option Explicit
Global Const DEFAULT_DATE = "01/01/1800"
Public Type POINTAPI
        x As Long
        Y As Long
End Type
Dim sCurUser$
Public Enum EncryptDecrypt
    ENCODE = 1
    DECODE = 2
End Enum
Public Declare Function GetCursorPos Lib "user32" (lpPoint As POINTAPI) As Long
'Public Enum ContactViews
'    CardView = 1
'    GridView = 2
'End Enum
'Public nWindowProc As Long
'Public isSubclassed As Boolean
'Public Const GWL_WNDPROC As Long = (-4)
'Public Const GWL_HWNDPARENT As Long = (-8)
'Public Const GWL_ID As Long = (-12)
'Public Const GWL_STYLE As Long = (-16)
'Public Const GWL_EXSTYLE As Long = (-20)
'Public Const GWL_USERDATA As Long = (-21)
'Public Const WM_USER As Long = &H400
'Public Const WM_MYHOOK As Long = WM_USER + 1
'Public Const WM_NOTIFY As Long = &H4E
'Public Const WM_COMMAND As Long = &H111

'Public Declare Function FrameRgn& Lib "gdi32" (ByVal hDC As Long, ByVal hRgn As Long, ByVal hBrush As Long, ByVal nWidth As Long, ByVal nHeight As Long)
'Public Declare Function SetForegroundWindow Lib "user32" _
'   (ByVal hwnd As Long) As Long
'Public Declare Function PostMessageLong Lib "user32" Alias "PostMessageA" _
'   (ByVal hwnd As Long, _
'   ByVal wMsg As Long, _
'   ByVal wParam As Long, _
'   ByVal lParam As Long) As Long
'Public Declare Function SetWindowLong Lib "user32" Alias "SetWindowLongA" _
'   (ByVal hwnd As Long, ByVal nIndex As Long, ByVal dwNewLong As Any) As Long
'Public Declare Function CallWindowProc Lib "user32" Alias "CallWindowProcA" _
'   (ByVal lpPrevWndFunc As Long, ByVal hwnd As Long, ByVal uMsg As Long, _
'    ByVal wParam As Long, ByVal lParam As Long) As Long
Const msScramble = "soDSMDHnsQ" 'This is used for encryption
'Public Const constClosedOut = 10
'Public Const constOnHold = 11
Public Declare Function GetComputerName Lib "KERNEL32" Alias "GetComputerNameA" (ByVal lpBuffer As String, nSize As Long) As Long

Public nCurRecID&
'Public Const RGN_OR = 2
'Public Declare Function CreateRoundRectRgn Lib "gdi32" _
'(ByVal X1 As Long, ByVal Y1 As Long, ByVal X2 As Long, _
'ByVal Y2 As Long, ByVal X3 As Long, ByVal Y3 As Long) As Long
Public tX As Double, TY As Double
'Public pt() As POINTAPI
'Public Declare Function SetWindowRgn Lib "user32" _
'(ByVal hwnd As Long, ByVal hRgn As Long, ByVal bredraw As Boolean) As Long
'Public Declare Function DeleteObject Lib "gdi32" _
'(ByVal hObject As Long) As Long
'Public Declare Function CombineRgn Lib "gdi32" _
'(ByVal hDestRgn As Long, ByVal hSrcRgn1 As Long, _
'ByVal hSrcRgn2 As Long, ByVal nCombineMode As Long) As Long

'Public Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Long)

'Public Const PS_SOLID = &H0
'Public Const WM_PAINT = &HF
'Public Declare Function SelectObject Lib "gdi32" (ByVal hDC As Integer, ByVal hObject As Integer) As Integer
'Public Declare Function RoundRect Lib "gdi32" (ByVal hDC As Long, ByVal X1 As Long, ByVal Y1 As Long, ByVal X2 As Long, ByVal Y2 As Long, ByVal X3 As Long, ByVal Y3 As Long) As Long
'Public Declare Function CreateSolidBrush Lib "gdi32" (ByVal crColor As Long) As Integer
'Public Declare Function GetDC Lib "user32" (ByVal hwnd As Long) As Long
'Public Declare Function CreatePen Lib "gdi32" (ByVal nPenStyle As Integer, ByVal nWidth As Integer, ByVal crColor As Long) As Integer
'Public DC_FRM As Long
'Public Declare Function CreateRectRgn Lib "gdi32" (ByVal X1 As Long, ByVal Y1 As Long, ByVal X2 As Long, ByVal Y2 As Long) As Long
'Public Const WM_MOUSEHOVER = &H2A1&
'Public Const WM_MOUSELEAVE = &H2A3&
Public Declare Function GetUserName Lib "advapi32.dll" Alias "GetUserNameA" (ByVal lpBuffer As String, nSize As Long) As Long

'Public Const HWND_TOPMOST = -1
'Public Const HWND_NOTOPMOST = -2

'Public Declare Function SetWindowPos Lib "user32" (ByVal hwnd As Long, ByVal hWndInsertAfter As Long, ByVal X As Long, ByVal Y As Long, ByVal cx As Long, ByVal cy As Long, ByVal wFlags As Long) As Long

Public DBPath As String

Public Function UserName() As String
     Dim nLen&
 nLen = 100
 On Error Resume Next
 UserName = String$(nLen, 0)
 GetUserName UserName, nLen
 UserName = Left(UserName, nLen - 1)
End Function
'Public Sub Get_GridRect(oObject As MSFlexGrid, points() As POINTAPI)
'    ReDim points(2) As POINTAPI
'
'
'    With oObject
'        If .CellForeColor <> &HFFFFFF Then
'            Do While .CellForeColor <> &HFFFFFF
'                If Not (.Row + 1 > (.Rows - 1)) And .Row <> 0 Then
'                    .Row = .Row + 1
'                Else
'                    Exit Do
'                End If
'            Loop
'        End If
'        .Row = .Row - 1
'        If .ColData(.Col) = 1 Then .Col = .Col + 2
'        points(1).X = (.CellLeft + .CellWidth) / TX
'        points(1).Y = (.CellTop + .CellHeight) / TY
'        .Row = .Row - 1
'        If .CellForeColor <> &HFFFFFF Then
'            Do While .CellForeColor <> &HFFFFFF
'                If Not (.Row + 1 > (.Rows - 1)) And .Row <> 0 Then
'                    .Row = .Row - 1
'                Else
'                    Exit Do
'                End If
'            Loop
'        End If
'         If .ColData(.Col) = 2 Then .Col = .Col - 1
'        points(0).X = (.CellLeft / TX)
'        points(0).Y = (.CellTop / TY)
'    End With
'End Sub
'
'Public Sub Get_Rect(oObject As Object, points() As POINTAPI)
'    ReDim points(2) As POINTAPI
'
'    With oObject
'        points(0).X = (.Left / TX)
'        points(0).Y = (.Top / TY)
'        points(1).X = (.Left + .Width) / TX
'        points(1).Y = (.Top + .Height) / TY
'    End With
'End Sub


Public Function Currentuser() As String
 Currentuser = sCurUser$
End Function

Public Function ComputerName() As String
 Dim nLen&
 nLen = 100
 On Error Resume Next
 ComputerName = String$(nLen, 0)
 GetComputerName ComputerName, nLen
 ComputerName = Left(ComputerName, nLen)
End Function

Public Function Encrypt(sPwdString As String, abEncryptType As EncryptDecrypt) As String
Dim ncount As Integer, nCode1 As Integer, nCode2 As Integer
Dim sScrambled As String, nStringLength As Integer

    If sPwdString <> "" Then
        nStringLength = Len(msScramble)

        Select Case abEncryptType
            Case ENCODE
                For ncount = 1 To Len(sPwdString)
                    nCode1 = Asc(Mid$(sPwdString, ncount, 1))
                    nCode2 = Asc(Mid$(msScramble, (ncount Mod nStringLength) + 1, 1))
                    nCode1 = nCode1 Xor nCode2
                    sScrambled = sScrambled + Chr$(Asc("z") - (nCode1 \ 16)) + Chr$(Asc("a") + (nCode1 Mod 16))
                Next ncount
            Case DECODE
                For ncount = 1 To Len(sPwdString) Step 2
                    nCode1 = (Asc("z") - Asc(Mid$(sPwdString, ncount, 1))) * 16 + (Asc(Mid$(sPwdString, ncount + 1, 1)) - Asc("a"))
                    nCode2 = Asc(Mid$(msScramble, (((ncount + 1) / 2) Mod nStringLength) + 1, 1))
                    nCode1 = nCode1 Xor nCode2
                    sScrambled = sScrambled + Chr$(nCode1)
                Next ncount
        End Select

        Encrypt = sScrambled
    End If
End Function


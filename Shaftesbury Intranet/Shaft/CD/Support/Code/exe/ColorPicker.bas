Attribute VB_Name = "modColorPicker"
Option Base 1

' #######################################
' # Constants                           #
' #######################################
Public Const BDR_INNER = &HC
Public Const BDR_OUTER = &H3
Public Const BDR_RAISED = &H5
Public Const BDR_SUNKEN = &HA
Public Const BDR_RAISEDINNER = &H4
Public Const BDR_SUNKENINNER = &H8
Public Const BDR_RAISEDOUTER = &H1
Public Const BDR_SUNKENOUTER = &H2

Public Const EDGE_BUMP = &H9&
Public Const EDGE_ETCHED = &H6&
Public Const EDGE_RAISED = &H5&
Public Const EDGE_SUNKEN = &HA&

Public Const BF_ADJUST = &H2000
Public Const BF_BOTTOMLEFT = &H9
Public Const BF_BOTTOMRIGHT = &HC
Public Const BF_DIAGONAL = &H10
Public Const BF_MIDDLE = &H800
Public Const BF_SOFT = &H1000
Public Const BF_TOPLEFT = &H3
Public Const BF_TOPRIGHT = &H6
Public Const BF_TOPRECT = &H7
Public Const BF_BOTTOMRECT = &HD
Public Const BF_RECT = &HF
Public Const BF_BOTTOM = &H8
Public Const BF_FLAT = &H4000      ' For flat rather than 3D borders
Public Const BF_LEFT = &H1
Public Const BF_MONO = &H8000      ' For monochrome borders.
Public Const BF_RIGHT = &H4
Public Const BF_TOP = &H2

Public Const SW_HIDE = 0
Public Const SW_SHOW = 5

Public Const CLR_NONE = -1

Public Const GWL_EXSTYLE = (-20)
Public Const GWL_STYLE = (-16)

Public Const WS_HSCROLL = &H100000

Public Const ILD_TRANSPARENT = 1&
Public Const ILD_SELECTED = 4&

Public Const HWND_NOTOPMOST = -2

' SysMetrics
Public Const SM_CXBORDER = 5
Public Const SM_CXDLGFRAME = 7, SM_CXFIXEDFRAME = SM_CXDLGFRAME
Public Const SM_CXFRAME = 32
Public Const SM_CXHSCROLL = 21
Public Const SM_CXVSCROLL = 2
Public Const SM_CYCAPTION = 4
Public Const SM_CYDLGFRAME = 8, SM_CYFIXEDFRAME = SM_CYDLGFRAME
Public Const SM_CYFRAME = 33
Public Const SM_CYHSCROLL = 3
Public Const SM_CYMENU = 15
Public Const SM_CYSMSIZE = 31
Public Const SM_CXSMSIZE = 30
Public Const SM_CXICON = 11
Public Const SM_CYICON = 12
Public Const SM_CYBORDER = 6

Public Const WM_ENTERMENULOOP = &H211
Public Const WM_MENUSELECT = &H11F
Public Const WM_EXITMENULOOP = &H212
Public Const WM_KEYDOWN = &H100
Public Const WM_MOUSEMOVE = &H200
Public Const WM_LBUTTONDOWN = &H201
Public Const WM_LBUTTONUP = &H202
Public Const WM_RBUTTONUP = &H205
Public Const WM_CANCELMODE = &H1F
Public Const WM_DRAWITEM = &H2B
Public Const WM_MEASUREITEM = &H2C
Public Const WM_MENUCHAR = &H120
Public Const WM_NCPAINT = &H85
Public Const WM_SIZE = &H5
Public Const WM_NCACTIVATE = &H86
Public Const WM_PAINT = &HF
Public Const WM_ERASEBKGND = &H14
Public Const WM_COMMAND = &H111
Public Const WM_ENABLE = &HA

Public Const TPM_CENTERALIGN = &H4&
Public Const TPM_LEFTALIGN = &H0&
Public Const TPM_LEFTBUTTON = &H0&
Public Const TPM_RIGHTALIGN = &H8&
Public Const TPM_RIGHTBUTTON = &H2&
Public Const TPM_NONOTIFY = &H80&           '/* Don't send any notification msgs */
Public Const TPM_RETURNCMD = &H100
Public Const TPM_HORIZONTAL = &H0          '/* Horz alignment matters more */
Public Const TPM_VERTICAL = &H40           '/* Vert alignment matters more */

Public Const GW_CHILD = 5

Public Const PS_SOLID& = 0

'Public Const DT_CENTER& = &H1
'Public Const DT_VCENTER& = &H4
'Public Const DT_SINGLELINE& = &H20
'Public Const DT_CALCRECT& = &H400

Public Const SWP_NOACTIVATE = &H10
Public Const SWP_SHOWWINDOW = &H40
Public Const SWP_HIDEWINDOW = &H80
Public Const SWP_FRAMECHANGED = &H20        '  The frame changed: send WM_NCCALCSIZE
Public Const SWP_NOCOPYBITS = &H100
Public Const SWP_NOMOVE = &H2
Public Const SWP_NOOWNERZORDER = &H200      '  Don't do owner Z ordering
Public Const SWP_NOREDRAW = &H8
Public Const SWP_NOREPOSITION = SWP_NOOWNERZORDER
Public Const SWP_NOSIZE = &H1
Public Const SWP_NOZORDER = &H4
Public Const SWP_DRAWFRAME = SWP_FRAMECHANGED

Public Const LF_FACESIZE = 32
Public Const LF_FULLFACESIZE = 64

'Draw State constants
'Image type
Public Const DST_ICON = &H3&
Public Const DST_BITMAP = &H4&

Public Const DSS_DISABLED = &H20

Public Const HWND_TOP& = 0

Public Const LOGPIXELSX = 88    '  Logical pixels/inch in X
Public Const LOGPIXELSY = 90    '  Logical pixels/inch in Y

Public Const ODS_DISABLED = &H4
Public Const ODS_SELECTED = &H1
Public Const ODS_CHECKED = &H8
Public Const ODS_FOCUS = &H10
Public Const ODS_GRAYED = &H2

Public Const ODT_STATIC = 5
Public Const ODT_BUTTON = 4
Public Const ODT_COMBOBOX = 3
Public Const ODT_LISTBOX = 2
Public Const ODT_LISTVIEW = 102
Public Const ODT_MENU = 1
Public Const ODT_TAB = 101
Public Const ODT_HEADER = 100

Public Const SW_SHOWNORMAL = 1

Public Const FW_NORMAL = 400
Public Const FW_BOLD = 700
Public Const FF_DONTCARE = 0
Public Const DEFAULT_QUALITY = 0
Public Const DEFAULT_PITCH = 0
Public Const DEFAULT_CHARSET = 1

Public Const NV_CLOSEMSGBOX = &H5000&
Public Const NV_MOVEMSGBOX = &H5001&
Public Const NV_ALTERNATEMSGBOX = &H5002&
Public Const NV_ALTERNATECHECK = &H5003&
Public Const NV_WWW = &H5004&
Public Const NV_COMBOBOX = &H5005&
Public Const NV_MDI = &H5006&
Public Const NV_DATEBUTTON = &H5007&

' #######################################
' # Types                               #
' #######################################
Public Type RECT
    Left As Long
    Top As Long
    Right As Long
    Bottom As Long
End Type
Public Type TPMPARAMS
    cbSize As Long
    rcExclude As RECT
End Type
Public Type LogFont
    lfHeight As Long
    lfWidth As Long
    lfEscapement As Long
    lfOrientation As Long
    lfWeight As Long
    lfItalic As Byte
    lfUnderline As Byte
    lfStrikeOut As Byte
    lfCharSet As Byte
    lfOutPrecision As Byte
    lfClipPrecision As Byte
    lfQuality As Byte
    lfPitchAndFamily As Byte
    lfFaceName(LF_FACESIZE) As Byte
End Type
Public Type LogFontString
    lfHeight As Long
    lfWidth As Long
    lfEscapement As Long
    lfOrientation As Long
    lfWeight As Long
    lfItalic As Byte
    lfUnderline As Byte
    lfStrikeOut As Byte
    lfCharSet As Byte
    lfOutPrecision As Byte
    lfClipPrecision As Byte
    lfQuality As Byte
    lfPitchAndFamily As Byte
    lfFaceName As String * LF_FACESIZE
End Type
Public Type PAINTSTRUCT
   hdc As Long
   fErase As Long
   rcPaint As RECT
   fRestore As Long
   fIncUpdate As Long
   rgbReserved(0 To 31) As Byte
End Type


' #######################################
' # API - Functions                     #
' #######################################
Public Declare Function SetParent Lib "user32" (ByVal hWndChild As Long, ByVal hWndNewParent As Long) As Long
Public Declare Function SendMessageLong Lib "user32" Alias "SendMessageA" (ByVal hwnd As Long, ByVal wMsg As Long, ByVal wParam As Long, ByVal lParam As Long) As Long
Public Declare Function GetSubMenu Lib "user32" (ByVal hMenu As Long, ByVal nPos As Long) As Long
Public Declare Function PostMessage Lib "user32" Alias "PostMessageA" (ByVal hwnd As Long, ByVal wMsg As Long, ByVal wParam As Long, ByVal lParam As Long) As Long
Public Declare Function TrackPopupMenuEx Lib "user32" (ByVal hMenu As Long, ByVal un As Long, ByVal n1 As Long, ByVal n2 As Long, ByVal hwnd As Long, lpTPMParams As TPMPARAMS) As Long
Public Declare Function GetCurrentThreadId Lib "kernel32" () As Long
Public Declare Function SetWindowsHookEx Lib "user32" Alias "SetWindowsHookExA" (ByVal idHook As Long, ByVal lpFn As Long, ByVal hMod As Long, ByVal dwThreadId As Long) As Long
Public Declare Function UnhookWindowsHookEx Lib "user32" (ByVal hHook As Long) As Long
Public Declare Function CallNextHookEx Lib "user32" (ByVal hHook As Long, ByVal nCode As Long, ByVal wParam As Long, ByVal lParam As Long) As Long
Public Declare Function SetProp Lib "user32" Alias "SetPropA" (ByVal hwnd As Long, ByVal lpString As String, ByVal hData As Long) As Long
Public Declare Function GetAsyncKeyState Lib "user32" (ByVal vKey As Long) As Integer
Public Declare Function RemoveProp Lib "user32" Alias "RemovePropA" (ByVal hwnd As Long, ByVal lpString As String) As Long
Public Declare Function GetProp Lib "user32" Alias "GetPropA" (ByVal hwnd As Long, ByVal lpString As String) As Long
Public Declare Function BeginPaint Lib "user32" (ByVal hwnd As Long, lpPaint As PAINTSTRUCT) As Long
Public Declare Function EndPaint Lib "user32" (ByVal hwnd As Long, lpPaint As PAINTSTRUCT) As Long
Public Declare Function GetClientRect Lib "user32" (ByVal hwnd As Long, lpRect As RECT) As Long
Public Declare Function GetWindow Lib "user32" (ByVal hwnd As Long, ByVal wCmd As Long) As Long
Public Declare Function GetWindowRect Lib "user32" (ByVal hwnd As Long, lpRect As RECT) As Long
Public Declare Function DrawText Lib "user32" Alias "DrawTextA" (ByVal hdc As Long, ByVal lpStr As String, ByVal nCount As Long, lpRect As RECT, ByVal wFormat As Long) As Long
Public Declare Function FillRect Lib "user32" (ByVal hdc As Long, lpRect As RECT, ByVal hBrush As Long) As Long
Public Declare Function ScreenToClient Lib "user32" (ByVal hwnd As Long, lpPoint As POINTAPI) As Long
Public Declare Function SetCapture Lib "user32" (ByVal hwnd As Long) As Long
Public Declare Function ReleaseCapture Lib "user32" () As Long
Public Declare Function SelectPalette Lib "gdi32" (ByVal hdc As Long, ByVal hPalette As Long, ByVal bForceBackground As Long) As Long
Public Declare Function RealizePalette Lib "gdi32" (ByVal hdc As Long) As Long
Public Declare Function GetBkColor Lib "gdi32" (ByVal hdc As Long) As Long
Public Declare Function CreateBitmap Lib "gdi32" (ByVal nWidth As Long, ByVal nHeight As Long, ByVal nPlanes As Long, ByVal nBitCount As Long, lpBits As Any) As Long
Public Declare Function GetTextColor Lib "gdi32" (ByVal hdc As Long) As Long
Public Declare Function CreateHalftonePalette Lib "gdi32" (ByVal hdc As Long) As Long
Public Declare Function DrawFrameControl& Lib "user32" (ByVal hdc As Long, lpRect As RECT, ByVal un1 As Long, ByVal un2 As Long)
Public Declare Function OleTranslateColor Lib "oleaut32.dll" (ByVal lOleColor As Long, ByVal lHPalette As Long, lColorRef As Long) As Long
Public Declare Function DrawFocusRect Lib "user32" (ByVal hdc As Long, lpRect As RECT) As Long
Public Declare Function CreateDCAsNull Lib "gdi32" Alias "CreateDCA" (ByVal lpDriverName As String, lpDeviceName As Any, lpOutput As Any, lpInitData As Any) As Long
Public Declare Function SetWindowPos Lib "user32" (ByVal hwnd As Long, ByVal hWndInsertAfter As Long, ByVal x As Long, ByVal y As Long, ByVal cx As Long, ByVal cy As Long, ByVal wFlags As Long) As Long
Public Declare Function GetActiveWindow Lib "user32" () As Long
Public Declare Function CreateFontIndirect Lib "gdi32" Alias "CreateFontIndirectA" (lpLogFont As LogFont) As Long
Public Declare Function CreateFontIndirectString Lib "gdi32" Alias "CreateFontIndirectA" (lpLogFont As LogFontString) As Long
Public Declare Function GetWindowText Lib "user32" Alias "GetWindowTextA" (ByVal hwnd As Long, ByVal lpString As String, ByVal cch As Long) As Long
Public Declare Function CallWindowProc Lib "user32" Alias "CallWindowProcA" (ByVal lpPrevWndFunc As Long, ByVal hwnd As Long, ByVal Msg As Long, ByVal wParam As Long, ByVal lParam As Long) As Long
Public Declare Function EnableWindow Lib "user32" (ByVal hwnd As Long, ByVal fEnable As Long) As Long
Public Declare Function MulDiv Lib "kernel32" (ByVal nNumber As Long, ByVal nNumerator As Long, ByVal nDenominator As Long) As Long
Public Declare Function SetWindowText Lib "user32" Alias "SetWindowTextA" (ByVal hwnd As Long, ByVal lpString As String) As Long
Public Declare Function MoveWindow& Lib "user32" (ByVal hwnd&, ByVal x&, ByVal y&, ByVal nWidth&, ByVal nHeight&, ByVal bRepaint&)
Public Declare Function SetTimer Lib "user32" (ByVal hwnd As Long, ByVal nIDEvent As Long, ByVal uElapse As Long, ByVal lpTimerFunc As Long) As Long
Public Declare Function KillTimer Lib "user32" (ByVal hwnd As Long, ByVal nIDEvent As Long) As Long
Public Declare Function GetSystemMetrics Lib "user32" (ByVal nIndex As Long) As Long
Public Declare Function ImageList_GetIconSize Lib "COMCTL32" (ByVal hImageList As Long, cx As Long, cy As Long) As Long
Public Declare Function ImageList_GetImageCount Lib "COMCTL32" (ByVal hImageList As Long) As Long
Public Declare Function GetSysColorBrush Lib "user32" (ByVal nIndex As Long) As Long
Public Declare Function InflateRect Lib "user32" (lpRect As RECT, ByVal x As Long, ByVal y As Long) As Long
Public Declare Function ClientToScreen Lib "user32" (ByVal hwnd As Long, lpPoint As POINTAPI) As Long
Public Declare Function UpdateWindow& Lib "user32" (ByVal hwnd As Long)
Public Declare Function SendMessageByLong Lib "user32" Alias "SendMessageA" (ByVal hwnd As Long, ByVal wMsg As Long, ByVal wParam As Long, ByVal lParam As Long) As Long
Public Declare Function GetFocus Lib "user32" () As Long

#If UNICODE Then
    Public Declare Function DrawState Lib "user32" Alias "DrawStateW" (ByVal hdc As Long, ByVal hBrush As Long, ByVal lpDrawStateProc As Long, ByVal lParam As Long, ByVal wParam As Long, ByVal n1 As Long, ByVal n2 As Long, ByVal n3 As Long, ByVal n4 As Long, ByVal un As Long) As Long
#Else
    Public Declare Function DrawState Lib "user32" Alias "DrawStateA" (ByVal hdc As Long, ByVal hBrush As Long, ByVal lpDrawStateProc As Long, ByVal lParam As Long, ByVal wParam As Long, ByVal n1 As Long, ByVal n2 As Long, ByVal n3 As Long, ByVal n4 As Long, ByVal un As Long) As Long
#End If
Public Declare Function DrawEdge& Lib "user32" (ByVal hdc As Long, qrc As RECT, ByVal edge As Long, ByVal grfFlags As Long)

Public Enum ECGTextAlignFlags
   DT_TOP = &H0&
   DT_LEFT = &H0&
   DT_CENTER = &H1&
   DT_RIGHT = &H2&
   DT_VCENTER = &H4&
   DT_BOTTOM = &H8&
   DT_WORDBREAK = &H10&
   DT_SINGLELINE = &H20&
   DT_EXPANDTABS = &H40&
   DT_TABSTOP = &H80&
   DT_NOCLIP = &H100&
   DT_EXTERNALLEADING = &H200&
   DT_CALCRECT = &H400&
   DT_NOPREFIX = &H800&
   DT_INTERNAL = &H1000&
'#if(WINVER >= =&H0400)
   DT_EDITCONTROL = &H2000&
   DT_PATH_ELLIPSIS = &H4000&
   DT_END_ELLIPSIS = &H8000&
   DT_MODIFYSTRING = &H10000
   DT_RTLREADING = &H20000
   DT_WORD_ELLIPSIS = &H40000
End Enum
' #######################################
' # API - Subs                          #
' #######################################
Public Declare Sub CopyMemory Lib "kernel32" Alias "RtlMoveMemory" (pDest As Any, pSrc As Any, ByVal ByteLen As Long)

' #######################################
' # Enums                              #
' #######################################

Public Declare Function SetRect Lib "user32" (lpRect As RECT, ByVal X1 As Long, ByVal Y1 As Long, ByVal X2 As Long, ByVal Y2 As Long) As Long
Public Declare Function SetRectEmpty Lib "user32" (lpRect As RECT) As Long


Public Declare Function CreateSolidBrush Lib "gdi32" (ByVal crColor As Long) As Long
Public Declare Function FrameRect Lib "user32" (ByVal hdc As Long, lpRect As RECT, ByVal hBrush As Long) As Long
Public Declare Function DeleteObject Lib "gdi32" (ByVal hObject As Long) As Long

Public CustClrs() As OLE_COLOR
Public LastSavedCustClr As Integer

Public DefClr As OLE_COLOR
Public CurClr As OLE_COLOR

Public DefCap As String
Public MorCap As String

Public ShwDef As Boolean
Public ShwCus As Boolean
Public ShwMor As Boolean
Public ShwSys As Boolean
Public ShwTip As Boolean

Public Sub CPTimer(ByVal hwnd As Long, ByVal uMsg As Long, ByVal idEvent As Long, ByVal dwTime As Long)
    Call frmColorPalette.TipTimer(hwnd, uMsg, idEvent, dwTime)
End Sub


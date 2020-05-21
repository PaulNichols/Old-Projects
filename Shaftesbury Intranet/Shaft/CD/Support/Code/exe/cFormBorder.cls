VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cFormBorder"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Private Declare Function SetWindowContextHelpId Lib "user32" (ByVal hwnd As Long, ByVal dw As Long) As Long
Private Declare Function SetWindowLong Lib "user32" Alias "SetWindowLongA" (ByVal hwnd As Long, ByVal nIndex As Long, ByVal dwNewLong As Long) As Long
Private Declare Function ShowWindow Lib "user32" (ByVal hwnd As Long, ByVal nCmdShow As Long) As Long
Private Declare Function LockWindowUpdate Lib "user32" (ByVal hwndLock As Long) As Long
Private Declare Function GetWindowLong Lib "user32" Alias "GetWindowLongA" (ByVal hwnd As Long, ByVal nIndex As Long) As Long

Private Const GWL_EXSTYLE = (-20)
Private Const WS_EX_APPWINDOW = &H40000

Private WithEvents m_client As Form
Attribute m_client.VB_VarHelpID = -1
Private m_Hwnd As Long

Public Property Set Client(ByVal obj As Form)
Set m_client = obj
If m_client Is Nothing Then
m_Hwnd = 0
Else
m_Hwnd = m_client.hwnd
End If
End Property


Public Property Get Client() As Form
Set Client = m_client
End Property

Public Property Let ShowInTaskBar(ByVal Value As Boolean)
LockWindowUpdate m_Hwnd
ShowWindow m_Hwnd, vbHide
FlipBitEx WS_EX_APPWINDOW, Value
ShowWindow m_Hwnd, vbNormalFocus
LockWindowUpdate 0&
End Property

Public Property Get ShowInTaskBar() As Boolean
ShowInTaskBar = CBool(StyleEx And WS_EX_APPWINDOW)
End Property

Private Function StyleEx(Optional ByVal newbits As Long = 0) As Long
If newbits Then
SetWindowLong m_Hwnd, GWL_EXSTYLE, newbits
End If
StyleEx = GetWindowLong(m_Hwnd, GWL_EXSTYLE)
End Function

Private Function FlipBitEx(ByVal Bit As Long, ByVal Value As Boolean) As Boolean
Dim nStyleEx As Long
    nStyleEx = GetWindowLong(m_Hwnd, GWL_EXSTYLE)
If Value Then
    nStyleEx = nStyleEx Or Bit
Else
    nStyleEx = nStyleEx And Not Bit
End If
SetWindowLong m_Hwnd, GWL_EXSTYLE, nStyleEx
'redraw
FlipBitEx = (nStyleEx = GetWindowLong(m_Hwnd, GWL_EXSTYLE))
End Function


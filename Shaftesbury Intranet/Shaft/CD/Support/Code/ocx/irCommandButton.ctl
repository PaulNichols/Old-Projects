VERSION 5.00
Begin VB.UserControl irCommandButton 
   ClientHeight    =   825
   ClientLeft      =   0
   ClientTop       =   0
   ClientWidth     =   1635
   ClipControls    =   0   'False
   KeyPreview      =   -1  'True
   ScaleHeight     =   55
   ScaleMode       =   3  'Pixel
   ScaleWidth      =   109
   ToolboxBitmap   =   "irCommandButton.ctx":0000
   Begin VB.Timer tmrTimer 
      Left            =   600
      Top             =   0
   End
End
Attribute VB_Name = "irCommandButton"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Dim lPic As New StdPicture
Dim MouseIsDown As Boolean
Dim WasButtonDown As Boolean
Dim EnterState As Integer
Dim r As RECT
Dim HasFocus As Boolean
Dim VirtualInControl As Boolean
Private Declare Function Rectangle Lib "gdi32" (ByVal hdc As Long, ByVal X1 As Long, ByVal Y1 As Long, ByVal X2 As Long, ByVal Y2 As Long) As Long
Private Declare Function CreatePen Lib "gdi32" (ByVal nPenStyle As Long, ByVal nWidth As Long, ByVal crColor As Long) As Long
Private Declare Function DrawState Lib "user32" Alias "DrawStateA" _
   (ByVal hdc As Long, _
   ByVal hBrush As Long, _
   ByVal lpDrawStateProc As Long, _
   ByVal lParam As Long, _
   ByVal wParam As Long, _
   ByVal X As Long, _
   ByVal Y As Long, _
   ByVal cx As Long, _
   ByVal cy As Long, _
   ByVal fuFlags As Long) As Long
'Private WithEvents ExitTimer As objTimer
'
Public Enum ButtonValues
    ButtonRaised = EdgeRaised
    ButtonSunken = EdgeSunken
    ButtonEtched = EdgeEtched
    ButtonBump = EdgeBump
    ButtonNone = EdgeNone
End Enum
Public Enum ButtonState
    [Coolbar Button] = 0
    [Standard Button] = 1
    [Panel] = 2
End Enum
Public Enum SpecialButtonStyles
    [Plain Button] = 0
    [Close Button] = 1
    [Minimize Button] = 2
    [Maximize Button] = 3
    [Normal Button] = 4
End Enum
'Default Property Values:
Const m_def_Bevel = 1
Const m_def_State = 0
Const m_def_ButtonStyle = 0
'Const m_def_Enabled = True
Const m_def_ForeColor = 0
Const m_def_BackColor = vbButtonFace
Const m_def_Caption = ""
Const m_def_ShowFocusRect = True
'Property Variables:
Dim m_CalDate As Date
Dim m_ButtonStyle As SpecialButtonStyles
Dim m_Bevel As Byte
Dim m_ShowFocusRect As Boolean
Dim m_State As ButtonState
Dim m_ForeColor As OLE_COLOR
Dim m_LetItRise As Boolean
Dim m_Value As ButtonValues
Dim m_Caption As String
Dim DrawNoMore As Boolean
Dim m_AccessKey As String * 1
'Event Declarations:
Event Click()
Event ClickUp()
Event MouseMove(Button As Integer, Shift As Integer, X As Single, Y As Single)
Event MouseUp(Button As Integer)

'Private Sub lPic_MouseDown(Button As Integer, Shift As Integer, x As Single, Y As Single)
'    UserControl_MouseDown Button, Shift, x, Y
'End Sub
'
'Private Sub lPic_MouseMove(Button As Integer, Shift As Integer, x As Single, Y As Single)
'    UserControl_MouseMove Button, Shift, x, Y
'End Sub
'
'Private Sub lPic_MouseUp(Button As Integer, Shift As Integer, x As Single, Y As Single)
'    UserControl_MouseUp Button, Shift, x, Y
'End Sub
Private Sub tmrTimer_Timer()
    
    If InControl Then
        tmrTimer.Enabled = True
    Else
        tmrTimer.Enabled = False
        EnterState = 0
        'tmrTimer.Enabled = False
        If Not m_LetItRise And m_Value = EdgeSunken Then
'            Stop
            Refresh
        Else
            If Not MouseIsDown Then
                m_Value = EdgeNone
                Refresh
            End If
        End If
    End If
End Sub
'
'Private Sub tmrTimer_Timer()
'Dim p As PointApi, r As RECT
'
'    If m_LetItRise = False Then Exit Sub
'    GetWindowRect UserControl.hWnd, r
'    GetCursorPos p
'    With p
'        If .x > r.Left And .x < r.Right And _
'           .y > r.Top And .y < r.Bottom Then
'            tmrTimer.Enabled = True
'        Else
'            tmrTimer.Enabled = False
'            m_Value = EdgeNone
'            Refresh
'        End If
'    End With
'End Sub

Private Sub UserControl_AccessKeyPress(KeyAscii As Integer)
    UserControl_MouseDown 0, 0, 0, 0
    RaiseEvent Click
    UserControl_MouseUp 0, 0, 0, 0
End Sub

Private Sub UserControl_GotFocus()
    If Not MouseIsDown Then
        HasFocus = True
        Refresh
    End If
End Sub

Private Sub UserControl_Initialize()
    m_Value = EdgeNone
End Sub

Private Sub UserControl_KeyDown(KeyCode As Integer, Shift As Integer)
    If KeyCode = vbKeySpace Then
        RaiseEvent Click
        KeyCode = 0
    End If
End Sub

Private Sub UserControl_LostFocus()
    HasFocus = False
    Refresh
End Sub

Private Sub UserControl_Paint()
    DrawNoMore = False
    DrawControl m_Caption ' , IIf(m_Value = 0, m_Value)
End Sub

Private Sub DrawControl(sCaption As String, Optional m_Var)
Dim nOffset As Integer, nRet As Boolean, hBrush As Long, hPen As Long
Dim nWidth As Integer, K As Integer, i As Integer, j As Integer
Dim nCount As Integer, cy As Single, cx As Single, nLen As Integer
Static UpColor As Long
    If IsMissing(m_Var) Then m_Var = m_Value
    i = InStr(1, sCaption, "&")
    If i > 0 Then
        AccessKey = Mid$(sCaption, i + 1, 1)
    Else
        AccessKey = ""
    End If
    With UserControl
        .Cls
        GetClientRect .hWnd, r
        
        If m_Var = EdgeSunken Then
            nOffset = IIf(m_Bevel = 0, 2, m_Bevel)
        Else
            nOffset = 0
        End If
        
        If Not .Enabled Then m_Var = EdgeNone
        If Len(sCaption) > 0 Then
            hBrush = CreateSolidBrush(m_ForeColor)
            cx = (r.Right - .TextWidth(Stuff(sCaption, "&", ""))) \ 2
            If lPic = LoadPicture("") Then
                cy = (r.Bottom - TextHeight(sCaption)) \ 2
            Else
                cy = (2 * ((r.Bottom - PicHeight - (.TextHeight(sCaption) * (nCount + 1)))) / 3) + PicHeight
            End If
            DrawStateString hdc, hBrush, 0, sCaption, Len(sCaption), cx, cy, 0, 0, &H2 Or IIf(.Enabled, &H0, &H20) Or &H80
            DeleteObject hBrush
        End If
        If m_ButtonStyle > 0 Then
            If Enabled Then
                DrawFrameControl .hdc, r, 1, ((m_ButtonStyle - 1) Or &H4000)
            Else
                DrawFrameControl .hdc, r, 1, ((m_ButtonStyle - 1) Or &H4000 Or &H100)
            End If
        ElseIf lPic <> LoadPicture("") Then
            If Len(sCaption) = 0 Then
                cy = (r.Bottom - PicHeight + nOffset) / 2
            Else
                cy = (r.Bottom - PicHeight - (.TextHeight(sCaption) * (nCount + 1)) + nOffset) / 3
            End If
            cx = ((r.Right - r.Left - PicWidth) / 2) + nOffset
            
            DrawState hdc, 0, 0, lPic.Handle, 0, cx, cy, PicHeight, PicWidth, &H3 Or IIf(.Enabled, &H0, &H20)
        End If
        GetClientRect .hWnd, r
        If Not Ambient.UserMode Then
            If m_Var <> EdgeSunken Then m_Var = EdgeRaised            '= False Then Err.Raise 382
        End If
        Select Case m_State
            Case [Panel]: m_Var = EdgeRaised
            Case [Standard Button]: If m_Var <> EdgeSunken Then m_Var = EdgeRaised
        End Select
        If m_Bevel = 0 Then
            DrawEdge .hdc, r, CLng(m_Var), BF_RECT
        Else
            DrawShadowBox CLng(m_Var), r
        End If
        If HasFocus And m_ShowFocusRect And Not m_State = Panel Then
            hPen = CreatePen(2, 1, QBColor(8))
            SelectObject hdc, hPen
            Rectangle hdc, r.Left + 4, r.Top + 4, r.Right - 4, r.Bottom - 4
            DeleteObject hPen
        End If
    End With
End Sub

Private Sub UserControl_Show()
    Refresh
End Sub

Public Property Get Caption() As String
    Caption = Stuff(m_Caption, vbLf, Space(1))
End Property

Public Property Let Caption(ByVal New_Caption As String)
    m_Caption = New_Caption
    PropertyChanged "Caption"
    Refresh
End Property

'Initialize Properties for User Control
Private Sub UserControl_InitProperties()
    m_Caption = m_def_Caption
    m_ForeColor = m_def_ForeColor
    UserControl.BackColor = m_def_BackColor
    m_Bevel = m_def_Bevel
'    m_Enabled = m_def_Enabled
    Set Font = Ambient.Font
    Set lPic = LoadPicture("")
    m_LetItRise = True
    EnterState = 0
    m_ButtonStyle = m_def_ButtonStyle
    m_AccessKey = ""
    m_ShowFocusRect = m_def_ShowFocusRect
End Sub

'Load property values from storage
Private Sub UserControl_ReadProperties(PropBag As PropertyBag)

    m_AccessKey = PropBag.ReadProperty("AccessKey", "")
    m_ShowFocusRect = PropBag.ReadProperty("ShowFocusRect", m_def_ShowFocusRect)
    m_Caption = PropBag.ReadProperty("Caption", m_def_Caption)
    m_LetItRise = PropBag.ReadProperty("LetItRise", True)
    m_ForeColor = PropBag.ReadProperty("ForeColor", m_def_ForeColor)
    Set Font = PropBag.ReadProperty("Font", Ambient.Font)
    UserControl.Enabled = PropBag.ReadProperty("Enabled", True)
    UserControl.BackColor = PropBag.ReadProperty("BackColor", m_def_BackColor)
    m_State = PropBag.ReadProperty("State", m_def_State)
    Set lPic = PropBag.ReadProperty("ButtonIcon", Nothing)
    m_Bevel = PropBag.ReadProperty("Bevel", m_def_Bevel)
    m_ButtonStyle = PropBag.ReadProperty("ButtonStyle", m_def_ButtonStyle)
End Sub

'Write property values to storage
Private Sub UserControl_WriteProperties(PropBag As PropertyBag)

    PropBag.WriteProperty "ShowFocusRect", m_ShowFocusRect, m_def_ShowFocusRect
    PropBag.WriteProperty "Bevel", m_Bevel, m_def_Bevel
    PropBag.WriteProperty "Caption", m_Caption, m_def_Caption
    PropBag.WriteProperty "LetItRise", m_LetItRise, True
    PropBag.WriteProperty "Font", Font, Ambient.Font
    PropBag.WriteProperty "ForeColor", m_ForeColor, m_def_ForeColor
    PropBag.WriteProperty "BackColor", UserControl.BackColor, m_def_BackColor
    PropBag.WriteProperty "Font", Font, Ambient.Font
    PropBag.WriteProperty "Enabled", UserControl.Enabled, True
    PropBag.WriteProperty "State", m_State, m_def_State
    PropBag.WriteProperty "ButtonIcon", lPic, Nothing
    PropBag.WriteProperty "ButtonStyle", m_ButtonStyle, m_def_ButtonStyle
    PropBag.WriteProperty "AccessKey", m_AccessKey, ""
End Sub

Private Sub UserControl_MouseDown(Button As Integer, Shift As Integer, X As Single, Y As Single)
    MouseIsDown = True
    WasButtonDown = (m_Value = EdgeSunken)
    If m_Value <> EdgeSunken And m_State <> [Panel] Then
        m_Value = EdgeSunken
        Refresh
    End If
End Sub

Private Sub UserControl_MouseMove(Button As Integer, Shift As Integer, X As Single, Y As Single)
        
    If Not m_LetItRise And EnterState > 0 Then Exit Sub
    RaiseEvent MouseMove(Button, Shift, X, Y)
    If m_State = Panel Then Exit Sub
    If EnterState = 0 Then
        EnterState = m_Value + 1
        If m_LetItRise Or m_Value = EdgeNone Then
            If Button = 0 And m_Value <> EdgeRaised Then
                m_Value = EdgeRaised
                Refresh
            End If
        End If
    End If
    tmrTimer.Interval = 50
    tmrTimer.Enabled = True
End Sub


Private Sub UserControl_MouseUp(Button As Integer, Shift As Integer, X As Single, Y As Single)
Dim OldRiseState As Boolean
    If m_Value <> EdgeRaised Then
        If InControl Or VirtualInControl Then
            If WasButtonDown Or EnterState = 0 Then
                m_Value = EdgeRaised
                EnterState = EdgeRaised + 1
                RaiseEvent ClickUp
                Refresh
            Else
                m_Value = IIf(m_LetItRise, EdgeRaised, EdgeSunken)
                With UserControl
                        Select Case ButtonStyle
                        Case SpecialButtonStyles.[Close Button]
                            Unload UserControl.Parent
                        Case SpecialButtonStyles.[Maximize Button]
                          '  UserControl.Parent.WindowState = vbMaximized
                           UserControl.Parent.mnuviews_Click 3
                            ButtonStyle = [Normal Button]
                        Case SpecialButtonStyles.[Minimize Button]
                            UserControl.Parent.DockForm
                        Case SpecialButtonStyles.[Normal Button]
                            UserControl.Parent.WindowState = vbNormal
                            ButtonStyle = [Maximize Button]
                        Case Else: RaiseEvent Click
                    End Select
                End With
              Refresh
                tmrTimer.Enabled = False
                tmrTimer.Enabled = True
            End If
        Else
            m_Value = IIf(m_LetItRise, EdgeNone, EdgeSunken)
            Refresh
        End If
    End If
    MouseIsDown = False
    RaiseEvent MouseUp(Button)
End Sub

Public Property Get ForeColor() As OLE_COLOR
Attribute ForeColor.VB_Description = "Returns/sets the foreground color used to display text and graphics in an object."
    ForeColor = m_ForeColor
End Property

Public Property Let ForeColor(ByVal New_ForeColor As OLE_COLOR)
    m_ForeColor = New_ForeColor
    PropertyChanged "ForeColor"
    Refresh
End Property

Public Property Get BackColor() As OLE_COLOR
    BackColor = UserControl.BackColor
End Property

Public Property Let BackColor(ByVal New_BackColor As OLE_COLOR)
    UserControl.BackColor = New_BackColor
    PropertyChanged "BackColor"
    Refresh
End Property

Public Property Get LetItRise() As Boolean
 LetItRise = m_LetItRise
End Property

Public Property Let LetItRise(ByVal vData As Boolean)
 If vData = True And Not m_LetItRise Then m_Value = EdgeNone
 m_LetItRise = vData
 PropertyChanged "LetItRise"
 Refresh
End Property

Private Function Stuff(ByVal sPwdString As String, asSearch As String, asReplace) As String
Dim i As Integer
    
    i = InStr(sPwdString, asSearch)
    Do Until i = 0
        sPwdString = Left$(sPwdString, i - 1) + asReplace + Mid$(sPwdString, i + Len(asSearch))
        i = InStr(i + Len(asReplace), sPwdString, asSearch)
    Loop
    Stuff = sPwdString
End Function

Public Property Get Font() As Font
Attribute Font.VB_Description = "Returns a Font object."
Attribute Font.VB_UserMemId = -512
    Set Font = UserControl.Font
End Property

Public Property Set Font(ByVal New_Font As Font)
    Set UserControl.Font = New_Font
    PropertyChanged "Font"
    Refresh
End Property
'WARNING! DO NOT REMOVE OR MODIFY THE FOLLOWING COMMENTED LINES!
'MappingInfo=UserControl,UserControl,-1,Enabled
Public Property Get Enabled() As Boolean
Attribute Enabled.VB_Description = "Returns/sets a value that determines whether an object can respond to user-generated events."
    Enabled = UserControl.Enabled
End Property

Public Property Let Enabled(ByVal New_Enabled As Boolean)
    UserControl.Enabled() = New_Enabled
    PropertyChanged "Enabled"
    m_Value = EdgeNone
    Refresh
End Property

'WARNING! DO NOT REMOVE OR MODIFY THE FOLLOWING COMMENTED LINES!
'MappingInfo=UserControl,UserControl,-1,hWnd
Public Property Get hWnd() As Long
Attribute hWnd.VB_Description = "Returns a handle (from Microsoft Windows) to an object's window."
    hWnd = UserControl.hWnd
End Property

Public Property Get State() As ButtonState
    State = m_State
End Property

Public Property Let State(ByVal New_Value As ButtonState)
    m_State = New_Value
    PropertyChanged "State"
    Refresh
End Property

Private Function PicWidth() As Single
    PicWidth = lPic.Width * 0.5669 / Screen.TwipsPerPixelX
End Function

Private Function PicHeight() As Single
    PicHeight = lPic.Height * 0.5669 / Screen.TwipsPerPixelY
End Function

Public Property Get ButtonIcon() As StdPicture
Attribute ButtonIcon.VB_Description = "Returns/sets a graphic to be displayed in a control."
    Set ButtonIcon = lPic
End Property

Public Property Set ButtonIcon(ByVal NewData As StdPicture)
    Set lPic = NewData
    PropertyChanged "ButtonIcon"
    Refresh
End Property

Public Property Get Bevel() As Byte
    Bevel = m_Bevel
End Property

Public Property Let Bevel(ByVal vData As Byte)
    m_Bevel = vData
    PropertyChanged "Bevel"
    Refresh
End Property

Private Function InControl() As Boolean
Dim p As POINTAPI
Dim r As RECT
    GetWindowRect UserControl.hWnd, r
    GetCursorPos p
    With p
        InControl = (.X > r.Left And .X < r.Right And .Y > r.Top And .Y < r.Bottom)
    End With
End Function

Public Sub SetNormal()
    m_Value = EdgeNone
    Refresh
End Sub

Private Sub DrawShadowBox(lEdge As Long, r As RECT)
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
        Case EdgeNone
            If m_ButtonStyle = 0 Then Exit Sub
            Color1 = BackColor
            Color2 = BackColor
        Case Else: Exit Sub
    End Select
    For i = 0 To m_Bevel - 1
        Line (X + i, Y + i)-(X + cx - 1 - i, Y + i), Color1
        Line (X + i, Y + i)-(X + i, Y + cy - 1 - i), Color1
        Line (X + cx - 1 - i, Y + i)-(X + cx - 1 - i, Y + cy - i), Color2
        Line (X + i, Y + cy - 1 - i)-(X + cx - i, Y + cy - 1 - i), Color2
    Next i
End Sub

Public Property Get ButtonStyle() As SpecialButtonStyles
    ButtonStyle = m_ButtonStyle
End Property

Public Property Let ButtonStyle(ByVal NewData As SpecialButtonStyles)
    m_ButtonStyle = NewData
    PropertyChanged "ButtonStyle"
    Refresh
End Property

Public Property Get AccessKey() As String
    AccessKey = m_AccessKey
End Property

Public Property Let AccessKey(ByVal NewData As String)
    If Len(NewData) = 1 Then
        m_AccessKey = NewData
        AccessKeys = NewData
    End If
End Property

Public Property Get ButtonState() As ButtonValues
    ButtonState = m_Value
End Property

Public Property Let ButtonState(ByVal NewData As ButtonValues)
    m_Value = NewData
    'PropertyChanged "Value"
    Refresh
End Property


Public Property Get ShowFocusRect() As Boolean
    ShowFocusRect = m_ShowFocusRect
End Property

Public Property Let ShowFocusRect(ByVal NewData As Boolean)
    m_ShowFocusRect = NewData
    PropertyChanged "ShowFocusRect"
    Refresh
End Property

Public Sub InvokeClick()
    VirtualInControl = True
    EnterState = 1
    m_Value = 0
    UserControl_MouseUp 0, 0, 0, 0
    VirtualInControl = False
End Sub

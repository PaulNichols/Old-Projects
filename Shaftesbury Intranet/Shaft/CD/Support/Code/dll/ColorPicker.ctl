VERSION 5.00
Begin VB.UserControl ColorPicker 
   AutoRedraw      =   -1  'True
   ClientHeight    =   525
   ClientLeft      =   0
   ClientTop       =   0
   ClientWidth     =   2055
   ScaleHeight     =   35
   ScaleMode       =   3  'Pixel
   ScaleWidth      =   137
   ToolboxBitmap   =   "ColorPicker.ctx":0000
End
Attribute VB_Name = "ColorPicker"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit
'Module specific variable declarations
Private RClr As RECT
Private RBut As RECT

Private IsInFocus As Boolean
Private IsButDown As Boolean

'Enums
Public Enum cpAppearanceConstants
    FlatPicker
    [3D]
End Enum

'Default Property Values:
Private Const m_def_ShowToolTips = True
Private Const m_def_ShowSysColorButton = True
Private Const m_def_ShowDefault = True
Private Const m_def_ShowCustomColors = True
Private Const m_def_ShowMoreColors = True
Private Const m_def_DefaultCaption = "Default"
Private Const m_def_MoreColorsCaption = "More Colors..."
Private Const m_def_BackColor = &H8000000C
Private Const m_def_Appearance = cpAppearanceConstants.[3D]
Private Const m_def_Color = &HFFFFFF
Private Const m_def_DefaultColor = &HFFFFFF

'Property Variables:
Private m_ShowToolTips As Boolean
Private m_ShowSysColorButton    As Boolean
Private m_ShowDefault           As Boolean
Private m_ShowCustomColors      As Boolean
Private m_ShowMoreColors        As Boolean
Private m_DefaultCaption        As String
Private m_MoreColorsCaption     As String
Private m_BackColor             As OLE_COLOR
Private m_Appearance            As cpAppearanceConstants
Private m_Color                 As OLE_COLOR
Private m_DefaultColor          As OLE_COLOR

'Event Declarations:
Event Click()
Event DblClick()
Event KeyDown(keycode As Integer, Shift As Integer)
Event KeyPress(KeyAscii As Integer)
Event KeyUp(keycode As Integer, Shift As Integer)
Event MouseDown(Button As Integer, Shift As Integer, x As Single, y As Single)
Event MouseMove(Button As Integer, Shift As Integer, x As Single, y As Single)
Event MouseUp(Button As Integer, Shift As Integer, x As Single, y As Single)
Event Resize()
Event ColorChange(SelectedColor As OLE_COLOR)

Private Sub UserControl_Click()
    RaiseEvent Click
End Sub

Private Sub UserControl_GotFocus()
    IsInFocus = True
    Call RedrawControl
End Sub

Private Sub UserControl_Initialize()
    ScaleMode = vbPixels
End Sub

Private Sub UserControl_LostFocus()
    IsInFocus = False
    Call RedrawControl
End Sub

Private Sub UserControl_MouseDown(Button As Integer, Shift As Integer, x As Single, y As Single)
    RaiseEvent MouseDown(Button, Shift, x * Screen.TwipsPerPixelX, y * Screen.TwipsPerPixelY)
    
    If Button = 1 Then
        If (x >= RBut.Left And x <= RBut.Right) And (y >= RBut.Top And y <= RBut.Bottom) Then
            IsButDown = True
            RedrawControl
        End If
    End If
End Sub

Private Sub UserControl_MouseMove(Button As Integer, Shift As Integer, x As Single, y As Single)
    RaiseEvent MouseMove(Button, Shift, x * Screen.TwipsPerPixelX, y * Screen.TwipsPerPixelY)
    
    If IsButDown Then
        If Not ((x >= RBut.Left And x <= RBut.Right) And (y >= RBut.Top And y <= RBut.Bottom)) Then
            IsButDown = False
            RedrawControl
        End If
    End If
End Sub

Private Sub UserControl_MouseUp(Button As Integer, Shift As Integer, x As Single, y As Single)
    RaiseEvent MouseUp(Button, Shift, x * Screen.TwipsPerPixelX, y * Screen.TwipsPerPixelY)
    
    If Button = 1 Then
        If IsButDown Then
            IsButDown = False
            RedrawControl
        End If
        
        If ((x >= ScaleLeft And x <= ScaleWidth) And (y >= ScaleTop And y <= ScaleHeight)) Then
            ShowPalette
        End If
    End If
End Sub

Private Sub UserControl_Resize()
    RaiseEvent Resize
    If Height < 285 Then Height = 285
    
    RedrawControl
End Sub

Private Sub UserControl_DblClick()
    RaiseEvent DblClick
End Sub

Private Sub UserControl_KeyDown(keycode As Integer, Shift As Integer)
    RaiseEvent KeyDown(keycode, Shift)
End Sub

Private Sub UserControl_KeyPress(KeyAscii As Integer)
    RaiseEvent KeyPress(KeyAscii)
End Sub

Private Sub UserControl_KeyUp(keycode As Integer, Shift As Integer)
    RaiseEvent KeyUp(keycode, Shift)
End Sub

Private Sub RedrawControl()
Dim Rct As RECT
Dim Brsh As Long, clr As Long
    
Dim lX As Long, ty As Long
Dim rx As Long, by As Long
    
    lX = ScaleLeft: ty = ScaleTop
    rx = ScaleWidth: by = ScaleHeight
    
    Cls
    
    'Draw background
    SetRect Rct, 0, 0, rx, by
    OleTranslateColor m_BackColor, ByVal 0&, clr
    Brsh = CreateSolidBrush(clr)
    FillRect hdc, Rct, Brsh
    If m_Appearance = [3D] Then
        DrawEdge hdc, Rct, EDGE_SUNKEN, BF_RECT
    Else
        DrawEdge hdc, Rct, BDR_SUNKENOUTER, BF_RECT Or BF_FLAT Or BF_MONO
    End If
    DeleteObject Brsh
    DeleteObject clr
    
    'Draw button
    Dim CurFontName As String
    CurFontName = Font.Name
    Font.Name = "Marlett"
    OleTranslateColor vbButtonFace, ByVal 0&, clr
    Brsh = CreateSolidBrush(clr)
    If m_Appearance = [3D] Then
        If IsButDown Then
            SetRect RBut, rx - 15, 2, rx - 2, by - 2
            FillRect hdc, RBut, Brsh
            DrawEdge hdc, RBut, EDGE_RAISED, BF_RECT Or BF_FLAT
            SetRect Rct, RBut.Left + 2, RBut.Top, RBut.Right, RBut.Bottom
            DrawText hdc, "6", 1&, Rct, DT_CENTER Or DT_VCENTER Or DT_SINGLELINE
        Else
            SetRect RBut, rx - 15, 2, rx - 2, by - 2
            FillRect hdc, RBut, Brsh
            DrawEdge hdc, RBut, EDGE_RAISED, BF_RECT
            SetRect Rct, RBut.Left, RBut.Top, RBut.Right, RBut.Bottom - 1
            DrawText hdc, "6", 1&, Rct, DT_CENTER Or DT_VCENTER Or DT_SINGLELINE
        End If
    Else
        SetRect RBut, rx - 15, ty, rx, by
        FillRect hdc, RBut, Brsh
        DrawEdge hdc, RBut, BDR_SUNKENOUTER, BF_RECT Or BF_FLAT
        SetRect Rct, RBut.Left + 1, RBut.Top, RBut.Right, RBut.Bottom - 1
        DrawText hdc, "6", 1&, Rct, DT_CENTER Or DT_VCENTER Or DT_SINGLELINE
    End If
    Font.Name = CurFontName
    DeleteObject Brsh
    DeleteObject clr
    
    'Draw Color
    If m_Appearance = [3D] Then
        SetRect RClr, 4, 4, rx - 17, by - 4
    Else
        SetRect RClr, 3, 3, rx - 17, by - 3
    End If
    OleTranslateColor m_Color, ByVal 0&, clr
    Brsh = CreateSolidBrush(clr)
    FillRect hdc, RClr, Brsh
    DeleteObject Brsh
    DeleteObject clr
    
    'Draw border to the color
    OleTranslateColor vbGrayText, ByVal 0&, clr
    Brsh = CreateSolidBrush(clr)
    FrameRect hdc, RClr, Brsh
    DeleteObject Brsh
    DeleteObject clr
    
    'Draw focus
    If m_Appearance = [3D] Then
        SetRect Rct, 6, 6, rx - 19, by - 6
    Else
        SetRect Rct, 5, 5, rx - 19, by - 5
    End If
    If IsInFocus Then DrawFocusRect hdc, Rct
    
    Refresh
End Sub

Private Sub ShowPalette()
Dim ClrCtrlPos As RECT
    
    GetWindowRect hwnd, ClrCtrlPos
    
    DefClr = m_DefaultColor
    CurClr = m_Color
    
    DefCap = m_DefaultCaption
    MorCap = m_MoreColorsCaption
    
    ShwDef = m_ShowDefault
    ShwMor = m_ShowMoreColors
    ShwCus = m_ShowCustomColors
    ShwSys = m_ShowSysColorButton
    ShwTip = m_ShowToolTips

    Load frmColorPalette
    With frmColorPalette
        .Left = ClrCtrlPos.Left * Screen.TwipsPerPixelX
        .Top = ClrCtrlPos.Bottom * Screen.TwipsPerPixelY
        If (.Top + .Height) > Screen.Height Then
            .Top = ClrCtrlPos.Top * Screen.TwipsPerPixelY - .Height
        End If
        
        .Show vbModal
        
        If Not .IsCanceled Then
            m_Color = .SelectedColor
            RaiseEvent ColorChange(.SelectedColor)
        End If
        RedrawControl
    End With
    Unload frmColorPalette
End Sub

'Initialize Properties for User Control
Private Sub UserControl_InitProperties()
    m_DefaultColor = m_def_DefaultColor
    m_Color = m_def_Color
    m_Appearance = m_def_Appearance
    m_BackColor = m_def_BackColor
    m_ShowDefault = m_def_ShowDefault
    m_ShowCustomColors = m_def_ShowCustomColors
    m_ShowMoreColors = m_def_ShowMoreColors
    m_DefaultCaption = m_def_DefaultCaption
    m_MoreColorsCaption = m_def_MoreColorsCaption
    m_ShowSysColorButton = m_def_ShowSysColorButton
    m_ShowToolTips = m_def_ShowToolTips
    
    Height = 315
End Sub

'Load property values from storage
Private Sub UserControl_ReadProperties(PropBag As PropertyBag)
    m_DefaultColor = PropBag.ReadProperty("DefaultColor", m_def_DefaultColor)
    m_Color = PropBag.ReadProperty("Value", m_def_Color)
    m_Appearance = PropBag.ReadProperty("Appearance", m_def_Appearance)
    m_BackColor = PropBag.ReadProperty("BackColor", m_def_BackColor)
    m_ShowDefault = PropBag.ReadProperty("ShowDefault", m_def_ShowDefault)
    m_ShowCustomColors = PropBag.ReadProperty("ShowCustomColors", m_def_ShowCustomColors)
    m_ShowMoreColors = PropBag.ReadProperty("ShowMoreColors", m_def_ShowMoreColors)
    m_DefaultCaption = PropBag.ReadProperty("DefaultCaption", m_def_DefaultCaption)
    m_MoreColorsCaption = PropBag.ReadProperty("MoreColorsCaption", m_def_MoreColorsCaption)
    m_ShowSysColorButton = PropBag.ReadProperty("ShowSysColorButton", m_def_ShowSysColorButton)
    m_ShowToolTips = PropBag.ReadProperty("ShowToolTips", m_def_ShowToolTips)
    
    RedrawControl
End Sub

'Write property values to storage
Private Sub UserControl_WriteProperties(PropBag As PropertyBag)
    With PropBag
        .WriteProperty "DefaultColor", m_DefaultColor, m_def_DefaultColor
        .WriteProperty "Value", m_Color, m_def_Color
        .WriteProperty "Appearance", m_Appearance, m_def_Appearance
        .WriteProperty "BackColor", m_BackColor, m_def_BackColor
        .WriteProperty "ShowDefault", m_ShowDefault, m_def_ShowDefault
        .WriteProperty "ShowCustomColors", m_ShowCustomColors, m_def_ShowCustomColors
        .WriteProperty "ShowMoreColors", m_ShowMoreColors, m_def_ShowMoreColors
        .WriteProperty "DefaultCaption", m_DefaultCaption, m_def_DefaultCaption
        .WriteProperty "MoreColorsCaption", m_MoreColorsCaption, m_def_MoreColorsCaption
        .WriteProperty "ShowSysColorButton", m_ShowSysColorButton, m_def_ShowSysColorButton
        .WriteProperty "ShowToolTips", m_ShowToolTips, m_def_ShowToolTips
    End With
End Sub

Public Property Get DefaultColor() As OLE_COLOR
Attribute DefaultColor.VB_Description = "Returns/Sets  the default color"
Attribute DefaultColor.VB_ProcData.VB_Invoke_Property = ";Appearance"
    DefaultColor = m_DefaultColor
End Property

Public Property Let DefaultColor(ByVal New_DefaultColor As OLE_COLOR)
    m_DefaultColor = New_DefaultColor
    PropertyChanged "DefaultColor"
End Property

Public Property Get Color() As OLE_COLOR
Attribute Color.VB_Description = "Returns/Sets the selected color"
Attribute Color.VB_ProcData.VB_Invoke_Property = ";Appearance"
Attribute Color.VB_UserMemId = 0
    Color = m_Color
End Property

Public Property Let Color(ByVal New_Color As OLE_COLOR)
    m_Color = New_Color
    PropertyChanged "Value"
    
    RedrawControl
End Property

Public Property Get Appearance() As cpAppearanceConstants
Attribute Appearance.VB_Description = "Returns/sets whether or not an object is painted at run time with 3-D effects."
Attribute Appearance.VB_ProcData.VB_Invoke_Property = ";Appearance"
    Appearance = m_Appearance
End Property

Public Property Let Appearance(ByVal New_Appearance As cpAppearanceConstants)
    m_Appearance = New_Appearance
    PropertyChanged "Appearance"
    
    RedrawControl
End Property

Public Property Get BackColor() As OLE_COLOR
Attribute BackColor.VB_Description = "Returns/sets the background color used to display text and graphics in an object."
Attribute BackColor.VB_ProcData.VB_Invoke_Property = ";Appearance"
    BackColor = m_BackColor
End Property

Public Property Let BackColor(ByVal New_BackColor As OLE_COLOR)
    m_BackColor = New_BackColor
    PropertyChanged "BackColor"
    
    RedrawControl
End Property

Public Property Get ShowDefault() As Boolean
Attribute ShowDefault.VB_Description = "Returns/Sets whether default button will be shown or not"
Attribute ShowDefault.VB_ProcData.VB_Invoke_Property = ";Behavior"
    ShowDefault = m_ShowDefault
End Property

Public Property Let ShowDefault(ByVal New_ShowDefault As Boolean)
    m_ShowDefault = New_ShowDefault
    PropertyChanged "ShowDefault"
End Property

Public Property Get ShowCustomColors() As Boolean
Attribute ShowCustomColors.VB_Description = "Returns/Sets whether custom colors will be shown or not"
Attribute ShowCustomColors.VB_ProcData.VB_Invoke_Property = ";Behavior"
    ShowCustomColors = m_ShowCustomColors
End Property

Public Property Let ShowCustomColors(ByVal New_ShowCustomColors As Boolean)
    m_ShowCustomColors = New_ShowCustomColors
    PropertyChanged "ShowCustomColors"
End Property

Public Property Get ShowMoreColors() As Boolean
Attribute ShowMoreColors.VB_Description = "Returns/Sets whether More Colors button will be shown or not"
Attribute ShowMoreColors.VB_ProcData.VB_Invoke_Property = ";Behavior"
    ShowMoreColors = m_ShowMoreColors
End Property

Public Property Let ShowMoreColors(ByVal New_ShowMoreColors As Boolean)
    m_ShowMoreColors = New_ShowMoreColors
    PropertyChanged "ShowMoreColors"
End Property

Public Property Get DefaultCaption() As String
Attribute DefaultCaption.VB_Description = "Returns/Sets the caption in default button"
Attribute DefaultCaption.VB_ProcData.VB_Invoke_Property = ";Appearance"
    DefaultCaption = m_DefaultCaption
End Property

Public Property Let DefaultCaption(ByVal New_DefaultCaption As String)
    m_DefaultCaption = New_DefaultCaption
    PropertyChanged "DefaultCaption"
End Property

Public Property Get MoreColorsCaption() As String
Attribute MoreColorsCaption.VB_Description = "Returns/Sets the caption in the More button"
Attribute MoreColorsCaption.VB_ProcData.VB_Invoke_Property = ";Appearance"
    MoreColorsCaption = m_MoreColorsCaption
End Property

Public Property Let MoreColorsCaption(ByVal New_MoreColorsCaption As String)
    m_MoreColorsCaption = New_MoreColorsCaption
    PropertyChanged "MoreColorsCaption"
End Property

Public Property Get ShowSysColorButton() As Boolean
    ShowSysColorButton = m_ShowSysColorButton
End Property

Public Property Let ShowSysColorButton(ByVal New_ShowSysColorButton As Boolean)
Attribute ShowSysColorButton.VB_ProcData.VB_Invoke_PropertyPut = ";Behavior"
    m_ShowSysColorButton = New_ShowSysColorButton
    PropertyChanged "ShowSysColorButton"
End Property

Public Property Get ShowToolTips() As Boolean
Attribute ShowToolTips.VB_ProcData.VB_Invoke_Property = ";Behavior"
    ShowToolTips = m_ShowToolTips
End Property

Public Property Let ShowToolTips(ByVal New_ShowToolTips As Boolean)
    m_ShowToolTips = New_ShowToolTips
    PropertyChanged "ShowToolTips"
End Property


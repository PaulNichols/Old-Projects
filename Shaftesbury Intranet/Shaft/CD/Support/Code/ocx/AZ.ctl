VERSION 5.00
Begin VB.UserControl AZ 
   BackColor       =   &H00000000&
   ClientHeight    =   675
   ClientLeft      =   0
   ClientTop       =   0
   ClientWidth     =   10020
   ScaleHeight     =   675
   ScaleWidth      =   10020
   Begin VB.CommandButton cmdMenu 
      BackColor       =   &H00FFC0C0&
      Caption         =   "v"
      BeginProperty Font 
         Name            =   "Marlett"
         Size            =   11.25
         Charset         =   2
         Weight          =   500
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   1080
      Style           =   1  'Graphical
      TabIndex        =   36
      TabStop         =   0   'False
      Top             =   120
      Width           =   375
   End
   Begin VB.CommandButton cmdFocus 
      Caption         =   "FOCUS"
      Height          =   255
      Left            =   3240
      TabIndex        =   0
      TabStop         =   0   'False
      Top             =   480
      Width           =   975
   End
   Begin VB.Timer tmrTimer 
      Enabled         =   0   'False
      Interval        =   1
      Left            =   9600
      Top             =   360
   End
   Begin VB.CommandButton cmdFunctions 
      BackColor       =   &H00FFC0C0&
      Caption         =   "&Add"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Index           =   0
      Left            =   5520
      Style           =   1  'Graphical
      TabIndex        =   35
      TabStop         =   0   'False
      Top             =   120
      Width           =   735
   End
   Begin VB.CommandButton cmdFunctions 
      BackColor       =   &H00FFC0C0&
      Caption         =   "&Edit"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Index           =   1
      Left            =   6240
      Style           =   1  'Graphical
      TabIndex        =   34
      TabStop         =   0   'False
      Top             =   120
      Width           =   735
   End
   Begin VB.CommandButton cmdFunctions 
      BackColor       =   &H00FFC0C0&
      Caption         =   "&Search"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Index           =   3
      Left            =   7680
      Style           =   1  'Graphical
      TabIndex        =   33
      TabStop         =   0   'False
      Top             =   120
      Width           =   735
   End
   Begin VB.CommandButton cmdMovement 
      BackColor       =   &H00FFC0C0&
      Caption         =   "3"
      BeginProperty Font 
         Name            =   "Marlett"
         Size            =   11.25
         Charset         =   2
         Weight          =   500
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Index           =   0
      Left            =   120
      Style           =   1  'Graphical
      TabIndex        =   32
      TabStop         =   0   'False
      Top             =   120
      Width           =   375
   End
   Begin VB.CommandButton cmdMovement 
      BackColor       =   &H00FFC0C0&
      Caption         =   "4"
      BeginProperty Font 
         Name            =   "Marlett"
         Size            =   11.25
         Charset         =   2
         Weight          =   500
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Index           =   1
      Left            =   600
      Style           =   1  'Graphical
      TabIndex        =   31
      TabStop         =   0   'False
      Top             =   120
      Width           =   375
   End
   Begin VB.CommandButton cmdFunctions 
      BackColor       =   &H00FFC0C0&
      Caption         =   "&Delete"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Index           =   2
      Left            =   6960
      Style           =   1  'Graphical
      TabIndex        =   30
      TabStop         =   0   'False
      Top             =   120
      Width           =   735
   End
   Begin VB.CommandButton cmdFunctions 
      BackColor       =   &H00FFC0C0&
      Caption         =   "&Exit"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Index           =   5
      Left            =   9120
      Style           =   1  'Graphical
      TabIndex        =   29
      TabStop         =   0   'False
      Top             =   120
      Width           =   735
   End
   Begin VB.CommandButton cmdFunctions 
      BackColor       =   &H00FFC0C0&
      Caption         =   "&Move"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Index           =   4
      Left            =   8400
      Style           =   1  'Graphical
      TabIndex        =   28
      TabStop         =   0   'False
      Top             =   120
      Width           =   735
   End
   Begin VB.Label lblLetters 
      Alignment       =   2  'Center
      Caption         =   "*"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   210
      Index           =   27
      Left            =   2160
      TabIndex        =   38
      Top             =   0
      Width           =   225
   End
   Begin VB.Label lblLetters 
      Alignment       =   2  'Center
      Caption         =   "#"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   210
      Index           =   0
      Left            =   1560
      TabIndex        =   37
      Top             =   120
      Width           =   225
   End
   Begin VB.Label lblSelectedLetter 
      Alignment       =   2  'Center
      Caption         =   "W"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   15.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   360
      Left            =   4920
      TabIndex        =   27
      Top             =   120
      Width           =   495
   End
   Begin VB.Label lblLetters 
      Alignment       =   2  'Center
      Caption         =   "Z"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   210
      Index           =   26
      Left            =   4680
      TabIndex        =   26
      Top             =   240
      Width           =   220
   End
   Begin VB.Label lblLetters 
      Alignment       =   2  'Center
      Caption         =   "Y"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   210
      Index           =   25
      Left            =   4560
      TabIndex        =   25
      Top             =   240
      Width           =   220
   End
   Begin VB.Label lblLetters 
      Alignment       =   2  'Center
      Caption         =   "X"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   210
      Index           =   24
      Left            =   4440
      TabIndex        =   24
      Top             =   240
      Width           =   220
   End
   Begin VB.Label lblLetters 
      Alignment       =   2  'Center
      Caption         =   "W"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   210
      Index           =   23
      Left            =   4320
      TabIndex        =   23
      Top             =   240
      Width           =   220
   End
   Begin VB.Label lblLetters 
      Alignment       =   2  'Center
      Caption         =   "V"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   210
      Index           =   22
      Left            =   4200
      TabIndex        =   22
      Top             =   240
      Width           =   220
   End
   Begin VB.Label lblLetters 
      Alignment       =   2  'Center
      Caption         =   "U"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   210
      Index           =   21
      Left            =   4080
      TabIndex        =   21
      Top             =   240
      Width           =   220
   End
   Begin VB.Label lblLetters 
      Alignment       =   2  'Center
      Caption         =   "T"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   210
      Index           =   20
      Left            =   3960
      TabIndex        =   20
      Top             =   240
      Width           =   220
   End
   Begin VB.Label lblLetters 
      Alignment       =   2  'Center
      Caption         =   "S"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   210
      Index           =   19
      Left            =   3840
      TabIndex        =   19
      Top             =   240
      Width           =   220
   End
   Begin VB.Label lblLetters 
      Alignment       =   2  'Center
      Caption         =   "R"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   210
      Index           =   18
      Left            =   3720
      TabIndex        =   18
      Top             =   240
      Width           =   220
   End
   Begin VB.Label lblLetters 
      Alignment       =   2  'Center
      Caption         =   "Q"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   210
      Index           =   17
      Left            =   3600
      TabIndex        =   17
      Top             =   240
      Width           =   220
   End
   Begin VB.Label lblLetters 
      Alignment       =   2  'Center
      Caption         =   "P"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   210
      Index           =   16
      Left            =   3480
      TabIndex        =   16
      Top             =   240
      Width           =   220
   End
   Begin VB.Label lblLetters 
      Alignment       =   2  'Center
      Caption         =   "O"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   210
      Index           =   15
      Left            =   3360
      TabIndex        =   15
      Top             =   240
      Width           =   220
   End
   Begin VB.Label lblLetters 
      Alignment       =   2  'Center
      Caption         =   "N"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   210
      Index           =   14
      Left            =   3240
      TabIndex        =   14
      Top             =   240
      Width           =   220
   End
   Begin VB.Label lblLetters 
      Alignment       =   2  'Center
      Caption         =   "M"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   210
      Index           =   13
      Left            =   3120
      TabIndex        =   13
      Top             =   240
      Width           =   220
   End
   Begin VB.Label lblLetters 
      Alignment       =   2  'Center
      Caption         =   "L"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   210
      Index           =   12
      Left            =   3000
      TabIndex        =   12
      Top             =   240
      Width           =   220
   End
   Begin VB.Label lblLetters 
      Alignment       =   2  'Center
      Caption         =   "K"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   210
      Index           =   11
      Left            =   2880
      TabIndex        =   11
      Top             =   240
      Width           =   220
   End
   Begin VB.Label lblLetters 
      Alignment       =   2  'Center
      Caption         =   "J"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   210
      Index           =   10
      Left            =   2760
      TabIndex        =   10
      Top             =   240
      Width           =   220
   End
   Begin VB.Label lblLetters 
      Alignment       =   2  'Center
      Caption         =   "I"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   210
      Index           =   9
      Left            =   2640
      TabIndex        =   9
      Top             =   240
      Width           =   220
   End
   Begin VB.Label lblLetters 
      Alignment       =   2  'Center
      Caption         =   "H"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   210
      Index           =   8
      Left            =   2520
      TabIndex        =   8
      Top             =   240
      Width           =   220
   End
   Begin VB.Label lblLetters 
      Alignment       =   2  'Center
      Caption         =   "G"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   210
      Index           =   7
      Left            =   2400
      TabIndex        =   7
      Top             =   240
      Width           =   220
   End
   Begin VB.Label lblLetters 
      Alignment       =   2  'Center
      Caption         =   "F"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   210
      Index           =   6
      Left            =   2280
      TabIndex        =   6
      Top             =   240
      Width           =   220
   End
   Begin VB.Label lblLetters 
      Alignment       =   2  'Center
      Caption         =   "E"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   210
      Index           =   5
      Left            =   2160
      TabIndex        =   5
      Top             =   240
      Width           =   220
   End
   Begin VB.Label lblLetters 
      Alignment       =   2  'Center
      Caption         =   "D"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   210
      Index           =   4
      Left            =   2040
      TabIndex        =   4
      Top             =   240
      Width           =   220
   End
   Begin VB.Label lblLetters 
      Alignment       =   2  'Center
      Caption         =   "C"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   210
      Index           =   3
      Left            =   1920
      TabIndex        =   3
      Top             =   240
      Width           =   220
   End
   Begin VB.Label lblLetters 
      Alignment       =   2  'Center
      Caption         =   "B"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   210
      Index           =   2
      Left            =   1800
      TabIndex        =   2
      Top             =   240
      Width           =   220
   End
   Begin VB.Label lblLetters 
      Alignment       =   2  'Center
      Caption         =   "A"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   210
      Index           =   1
      Left            =   1680
      TabIndex        =   1
      Top             =   240
      Width           =   220
   End
End
Attribute VB_Name = "AZ"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Declare Function GetCursorPos Lib "user32" (lpPoint As POINTAPI) As Long
Private Declare Function WindowFromPoint Lib "user32" (ByVal xPoint As Long, ByVal yPoint As Long) As Long
Private Declare Function GetFocus Lib "user32" () As Long
Private Declare Function SetFocus Lib "user32" (ByVal hWnd As Long) As Long

'Default Property Values:
Const m_def_Online = True
Const m_def_HighlightColor = vbRed
Const m_def_ButtonBackgroundColor = vbButtonFace
Const m_def_HighlightBold = True
Const m_def_LetterSpaceWidth = 53
Const m_def_ExtraButtonsCaptionCSV = "&Add,&Edit,&Delete,&Search,&Move,E&xit"
Const m_def_NavigateLeftCaption = "3"
Const m_def_NavigateRightCaption = "4"
Const m_def_MenuCaption = "v"
Const m_def_CurrentLetter = "*"
Const m_def_ShowNumeric = True

Const StartLeft = 100
Const LetterWidth = 150
'Property Variables:
Dim m_Online As Boolean
Dim m_HighlightColor As OLE_COLOR
Dim m_ButtonBackgroundColor As OLE_COLOR
Dim m_HighlightBold As Boolean
Dim m_LetterSpaceWidth As Integer
Dim m_ExtraButtonsCaptionCSV As String
Dim m_NavigateLeftCaption As String
Dim m_NavigateRightCaption As String
Dim m_MenuCaption As String
Dim m_CurrentLetter As String
Dim m_ShowNumeric As Boolean

Dim m_MinSize As Integer
'Event Declarations:
Event AZClick(LetterChar As String)
Event NavigateLeftClick()
Event NavigateRightCick()
Event RightClick(X As Single, Y As Single)
Event FunctionButtonsClick(Index As Integer)
Private CurrentIndex As Integer

Public Property Get BackColor() As OLE_COLOR
Attribute BackColor.VB_Description = "Returns/sets the background color used to display text and graphics in an object."
    BackColor = UserControl.BackColor
End Property

Public Property Let BackColor(ByVal New_BackColor As OLE_COLOR)
    UserControl.BackColor() = New_BackColor
    PropertyChanged "BackColor"
End Property

Public Property Get ForeColor() As OLE_COLOR
Attribute ForeColor.VB_Description = "Returns/sets the foreground color used to display text and graphics in an object."
    ForeColor = UserControl.ForeColor
End Property

Public Property Let ForeColor(ByVal New_ForeColor As OLE_COLOR)
    UserControl.ForeColor() = New_ForeColor
    PropertyChanged "ForeColor"
    SetLetterBackgrounds
End Property

Public Property Get Enabled() As Boolean
Attribute Enabled.VB_Description = "Returns/sets a value that determines whether an object can respond to user-generated events."
    Enabled = UserControl.Enabled
End Property

Public Property Let Enabled(ByVal New_Enabled As Boolean)
    UserControl.Enabled() = New_Enabled
    PropertyChanged "Enabled"
End Property

Public Property Get Font() As Font
Attribute Font.VB_Description = "Returns a Font object."
Attribute Font.VB_UserMemId = -512
    Set Font = UserControl.Font
End Property

Public Property Set Font(ByVal New_Font As Font)
    Set UserControl.Font = New_Font
    PropertyChanged "Font"
End Property

Public Sub Refresh()
Attribute Refresh.VB_Description = "Forces a complete repaint of a object."
    UserControl.Refresh
End Sub

Public Property Get hWnd() As Long
Attribute hWnd.VB_Description = "Returns a handle (from Microsoft Windows) to an object's window."
    hWnd = UserControl.hWnd
End Property

Public Property Get Online() As Boolean
    Online = m_Online
End Property

Public Property Let Online(ByVal New_Online As Boolean)
    m_Online = New_Online
    cmdFunctions(4).Enabled = New_Online
    PropertyChanged "Online"
End Property

Public Property Get ButtonBackgroundColor() As OLE_COLOR
    ButtonBackgroundColor = m_ButtonBackgroundColor
End Property

Public Property Let ButtonBackgroundColor(ByVal New_ButtonBackgroundColor As OLE_COLOR)
    m_ButtonBackgroundColor = New_ButtonBackgroundColor
    PropertyChanged "ButtonBackgroundColor"
    SetButtonBackgrounds
End Property

Public Property Get HighlightColor() As OLE_COLOR
    HighlightColor = m_HighlightColor
End Property

Public Property Let HighlightColor(ByVal New_HighlightColor As OLE_COLOR)
    m_HighlightColor = New_HighlightColor
    PropertyChanged "HighlightColor"
End Property

Public Property Get HighlightBold() As Boolean
    HighlightBold = m_HighlightBold
End Property

Public Property Let HighlightBold(ByVal New_HighlightBold As Boolean)
    m_HighlightBold = New_HighlightBold
    PropertyChanged "HighlightBold"
End Property

Public Property Get LetterSpaceWidth() As Integer
    LetterSpaceWidth = m_LetterSpaceWidth
End Property

Public Property Let LetterSpaceWidth(ByVal New_LetterSpaceWidth As Integer)
    m_LetterSpaceWidth = New_LetterSpaceWidth
    PropertyChanged "LetterSpaceWidth"
End Property

Public Property Get ExtraButtonsCaptionCSV() As String
    ExtraButtonsCaptionCSV = m_ExtraButtonsCaptionCSV
End Property

Public Property Let ExtraButtonsCaptionCSV(ByVal New_ExtraButtonsCaptionCSV As String)
    m_ExtraButtonsCaptionCSV = New_ExtraButtonsCaptionCSV
    PropertyChanged "ExtraButtonsCaptionCSV"
    SetupCaptions
End Property

Public Property Get NavigateLeftCaption() As String
    NavigateLeftCaption = m_NavigateLeftCaption
End Property

Public Property Let NavigateLeftCaption(ByVal New_NavigateLeftCaption As String)
    m_NavigateLeftCaption = New_NavigateLeftCaption
    PropertyChanged "NavigateLeftCaption"
    SetupCaptions
End Property

Public Property Get NavigateRightCaption() As String
    NavigateRightCaption = m_NavigateRightCaption
End Property

Public Property Let NavigateRightCaption(ByVal New_NavigateRightCaption As String)
    m_NavigateRightCaption = New_NavigateRightCaption
    PropertyChanged "NavigateRightCaption"
    SetupCaptions
End Property

Public Property Get MenuCaption() As String
    MenuCaption = m_MenuCaption
End Property

Public Property Let MenuCaption(ByVal New_MenuCaption As String)
    m_MenuCaption = New_MenuCaption
    PropertyChanged "MenuCaption"
    SetupCaptions
End Property

Public Property Get CurrentLetter() As String
    CurrentLetter = m_CurrentLetter
End Property

Public Property Let CurrentLetter(ByVal New_CurrentLetter As String)
    m_CurrentLetter = New_CurrentLetter
    lblSelectedLetter.Caption = New_CurrentLetter
    SetupLetters
    RaiseEvent AZClick(New_CurrentLetter)
    PropertyChanged "CurrentLetter"
End Property

Private Sub cmdFunctions_MouseUp(Index As Integer, Button As Integer, Shift As Integer, X As Single, Y As Single)
    Select Case Button
        Case 1
            RaiseEvent FunctionButtonsClick(Index)
        Case 2
            cmdFocus.SetFocus
            RaiseEvent RightClick(X, Y)
    End Select
End Sub

Private Sub cmdMenu_Click()
    cmdFocus.SetFocus
    RaiseEvent RightClick(0, 0)
End Sub

Private Sub cmdMovement_Click(Index As Integer)
    cmdFocus.SetFocus
    If Index = 1 Then
        CurrentLetter = GiveNextLetter(CurrentLetter)
    Else
        CurrentLetter = GiveNextLetter(CurrentLetter, False)
    End If
End Sub

Private Sub cmdMovement_MouseUp(Index As Integer, Button As Integer, Shift As Integer, X As Single, Y As Single)
    If Button = 2 Then
        cmdFocus.SetFocus
        RaiseEvent RightClick(X, Y)
    End If
End Sub

Private Sub lblLetters_MouseMove(Index As Integer, Button As Integer, Shift As Integer, X As Single, Y As Single)
    SetHighlight Index
End Sub

Private Sub lblLetters_MouseUp(Index As Integer, Button As Integer, Shift As Integer, X As Single, Y As Single)
    Select Case Button
        Case 1
            CurrentLetter = lblLetters(Index).Caption
        Case 2
            cmdFocus.SetFocus
            RaiseEvent RightClick(X, Y)
    End Select
End Sub

Private Sub lblSelectedLetter_MouseUp(Button As Integer, Shift As Integer, X As Single, Y As Single)
    If Button = 2 Then
        cmdFocus.SetFocus
        RaiseEvent RightClick(X, Y)
    End If
End Sub

Private Sub tmrTimer_Timer()
Dim xy As POINTAPI, lHwnd As Long, lFocusHwnd As Long
    
    GetCursorPos xy
    lHwnd = WindowFromPoint(xy.X, xy.Y)
    If lHwnd <> UserControl.hWnd Then
        SetHighlight -1
        lFocusHwnd = GetFocus()
        If lFocusHwnd > 0 Then
            cmdFocus.SetFocus
            SetFocus lFocusHwnd
        End If
    End If
End Sub

'Initialize Properties for User Control
Private Sub UserControl_InitProperties()
    Set UserControl.Font = Ambient.Font
    m_Online = m_def_Online
    m_ButtonBackgroundColor = m_def_ButtonBackgroundColor
    m_HighlightColor = m_def_HighlightColor
    m_HighlightBold = m_def_HighlightBold
    m_LetterSpaceWidth = m_def_LetterSpaceWidth
    m_ExtraButtonsCaptionCSV = m_def_ExtraButtonsCaptionCSV
    m_NavigateLeftCaption = m_def_NavigateLeftCaption
    m_NavigateRightCaption = m_def_NavigateRightCaption
    m_MenuCaption = m_def_MenuCaption
    m_CurrentLetter = m_def_CurrentLetter
    m_ShowNumeric = m_def_ShowNumeric
    CurrentIndex = -1
End Sub

Private Sub UserControl_MouseMove(Button As Integer, Shift As Integer, X As Single, Y As Single)
    SetHighlight -1
End Sub

Private Sub UserControl_MouseUp(Button As Integer, Shift As Integer, X As Single, Y As Single)
    If Button = 2 Then
        cmdFocus.SetFocus
        RaiseEvent RightClick(X, Y)
    End If
End Sub

'Load property values from storage
Private Sub UserControl_ReadProperties(PropBag As PropertyBag)

    UserControl.BackColor = PropBag.ReadProperty("BackColor", &H8000000F)
    UserControl.ForeColor = PropBag.ReadProperty("ForeColor", &H80000012)
    UserControl.Enabled = PropBag.ReadProperty("Enabled", True)
    Set UserControl.Font = PropBag.ReadProperty("Font", Ambient.Font)
    m_Online = PropBag.ReadProperty("Online", m_def_Online)
    m_ButtonBackgroundColor = PropBag.ReadProperty("ButtonBackgroundColor", m_def_ButtonBackgroundColor)
    m_HighlightColor = PropBag.ReadProperty("HighlightColor", m_def_HighlightColor)
    m_HighlightBold = PropBag.ReadProperty("HighlightBold", m_def_HighlightBold)
    m_LetterSpaceWidth = PropBag.ReadProperty("LetterSpaceWidth", m_def_LetterSpaceWidth)
    m_ExtraButtonsCaptionCSV = PropBag.ReadProperty("ExtraButtonsCaptionCSV", m_def_ExtraButtonsCaptionCSV)
    m_NavigateLeftCaption = PropBag.ReadProperty("NavigateLeftCaption", m_def_NavigateLeftCaption)
    m_NavigateRightCaption = PropBag.ReadProperty("NavigateRightCaption", m_def_NavigateRightCaption)
    m_MenuCaption = PropBag.ReadProperty("MenuCaption", m_def_MenuCaption)
    m_CurrentLetter = PropBag.ReadProperty("CurrentLetter", m_def_CurrentLetter)
    m_ShowNumeric = PropBag.ReadProperty("ShowNumeric", m_def_ShowNumeric)
End Sub

Private Sub UserControl_Resize()
    LockWindowUpdate UserControl.hWnd

    UserControl.lblSelectedLetter = m_CurrentLetter
    SetupLetters
    AlignLetters
    SetLetterBackgrounds
    SetupCaptions
    SetButtonBackgrounds
    Online = Online
    
    'This errors whilst the form is loading
    On Error Resume Next
    cmdFocus.SetFocus
    On Error GoTo 0
    
    LockWindowUpdate 0

End Sub

'Write property values to storage
Private Sub UserControl_WriteProperties(PropBag As PropertyBag)

    Call PropBag.WriteProperty("BackColor", UserControl.BackColor, &H8000000F)
    Call PropBag.WriteProperty("ForeColor", UserControl.ForeColor, vbBlack)
    Call PropBag.WriteProperty("Enabled", UserControl.Enabled, True)
    Call PropBag.WriteProperty("Font", UserControl.Font, Ambient.Font)
    Call PropBag.WriteProperty("Online", m_Online, m_def_Online)
    Call PropBag.WriteProperty("ButtonBackgroundColor", m_ButtonBackgroundColor, m_def_ButtonBackgroundColor)
    Call PropBag.WriteProperty("HighlightColor", m_HighlightColor, m_def_HighlightColor)
    Call PropBag.WriteProperty("HighlightBold", m_HighlightBold, m_def_HighlightBold)
    Call PropBag.WriteProperty("LetterSpaceWidth", m_LetterSpaceWidth, m_def_LetterSpaceWidth)
    Call PropBag.WriteProperty("ExtraButtonsCaptionCSV", m_ExtraButtonsCaptionCSV, m_def_ExtraButtonsCaptionCSV)
    Call PropBag.WriteProperty("NavigateLeftCaption", m_NavigateLeftCaption, m_def_NavigateLeftCaption)
    Call PropBag.WriteProperty("NavigateRightCaption", m_NavigateRightCaption, m_def_NavigateRightCaption)
    Call PropBag.WriteProperty("MenuCaption", m_MenuCaption, m_def_MenuCaption)
    Call PropBag.WriteProperty("CurrentLetter", m_CurrentLetter, m_def_CurrentLetter)
    Call PropBag.WriteProperty("ShowNumeric", m_ShowNumeric, m_def_ShowNumeric)
End Sub

Private Function GiveNextLetter(ByRef sCurrentLetter As String, Optional bForwardDirection As Boolean = True) As String
    
StartLoop:
    Select Case sCurrentLetter
        Case "Z": sCurrentLetter = IIf(bForwardDirection, "*", "Y")
        Case "*": sCurrentLetter = IIf(bForwardDirection, "?", "Z")
        Case "?": sCurrentLetter = IIf(bForwardDirection, IIf(m_ShowNumeric, "#", "A"), "*")
        Case "#": sCurrentLetter = IIf(bForwardDirection, "A", "?")
        Case "A": sCurrentLetter = IIf(bForwardDirection, "B", IIf(m_ShowNumeric, "#", "?"))
        Case Else: sCurrentLetter = Chr$(Asc(sCurrentLetter) + IIf(bForwardDirection, 1, -1))
    End Select
    If sCurrentLetter = m_CurrentLetter Then GoTo StartLoop
    
    GiveNextLetter = sCurrentLetter
End Function

Private Sub SetupLetters()
Dim sCurrentLetter As String * 1
Dim nStart As Integer, nEnd As Integer, i As Integer

    '13 either side with one in the middle
    sCurrentLetter = m_CurrentLetter
    nStart = 14: nEnd = 27
    Do
        For i = nStart To nEnd
            lblLetters(i).Caption = GiveNextLetter(sCurrentLetter)
        Next i
        If nStart = IIf(m_ShowNumeric, 0, 1) Then Exit Do
        nStart = IIf(m_ShowNumeric, 0, 1): nEnd = 13
    Loop

End Sub

Private Sub AlignLetters()
Dim i As Integer, nLeft As Long, nWidth As Integer, nTop As Integer, nButtonTop As Integer

    nTop = GetSmallLetterTop
    nButtonTop = GetButtonTop
    nLeft = StartLeft
    nWidth = LetterWidth + m_LetterSpaceWidth
    
    cmdMovement(0).Move nLeft, nButtonTop
    nLeft = nLeft + cmdMenu.Width + 20
    cmdMenu.Move nLeft, nButtonTop
    nLeft = nLeft + cmdMovement(0).Width + m_LetterSpaceWidth - nWidth
    For i = IIf(m_ShowNumeric, 0, 1) To 27
        nLeft = nLeft + nWidth
        If i = 14 Then
            'insert 3 command buttons
            cmdFunctions(0).Move nLeft, nButtonTop: nLeft = nLeft + cmdFunctions(0).Width
            cmdFunctions(1).Move nLeft, nButtonTop: nLeft = nLeft + cmdFunctions(1).Width
            cmdFunctions(2).Move nLeft, nButtonTop
            nLeft = nLeft + cmdFunctions(2).Width + 100
            
            'insert BIG letter
            lblSelectedLetter.Move nLeft, GetBigLetterTop
            nLeft = nLeft + lblSelectedLetter.Width + 100
            
            'insert 3 command buttons
            cmdFunctions(3).Move nLeft, nButtonTop: nLeft = nLeft + cmdFunctions(3).Width
            cmdFunctions(4).Move nLeft, nButtonTop: nLeft = nLeft + cmdFunctions(4).Width
            cmdFunctions(5).Move nLeft, nButtonTop
            nLeft = nLeft + cmdFunctions(5).Width + m_LetterSpaceWidth
            
        End If
        lblLetters(i).Move nLeft, nTop
    Next i
    nLeft = nLeft + nWidth + m_LetterSpaceWidth
    cmdMovement(1).Move nLeft, nButtonTop
    
    GiveMinSize = nLeft + cmdMovement(1).Width + StartLeft
    
    cmdFocus.Top = -1000
    On Error Resume Next
    cmdFocus.SetFocus
    On Error GoTo 0
End Sub

Private Function GetSmallLetterTop() As Integer
    GetSmallLetterTop = (UserControl.Height - lblLetters(1).Height) \ 2
End Function

Private Function GetBigLetterTop() As Integer
    GetBigLetterTop = (UserControl.Height - lblSelectedLetter.Height) \ 2
End Function

Private Function GetButtonTop() As Integer
    GetButtonTop = (UserControl.Height - cmdFunctions(0).Height) \ 2
End Function

Private Sub SetLetterBackgrounds(Optional SetForeColor As Boolean = True)
Dim i As Integer
    For i = 0 To 27
        lblLetters(i).BackColor = UserControl.Parent.BackColor
        If SetForeColor Then lblLetters(i).ForeColor = UserControl.ForeColor
    Next i
    lblSelectedLetter.BackColor = UserControl.Parent.BackColor
    If SetForeColor Then lblSelectedLetter.ForeColor = UserControl.ForeColor
End Sub

Public Property Get GiveMinSize() As Integer
    GiveMinSize = m_MinSize
End Property

Public Property Let GiveMinSize(ByVal vNewValue As Integer)
    m_MinSize = vNewValue
End Property

Private Sub SetupCaptions()
Dim sCaptions() As String, i As Integer

    sCaptions = Split(m_ExtraButtonsCaptionCSV, ",")
    For i = 0 To 5
        cmdFunctions(i).Caption = sCaptions(i)
    Next i
    Erase sCaptions
    
    cmdMovement(0).Caption = m_NavigateLeftCaption
    cmdMovement(1).Caption = m_NavigateRightCaption
End Sub

Private Sub SetHighlight(Index As Integer)
Dim i As Integer
    If Index = -1 Then
        For i = 0 To 27
            lblLetters(i).ForeColor = UserControl.ForeColor
            lblLetters(i).FontBold = False
        Next i
        CurrentIndex = -1
        tmrTimer.Enabled = False
    ElseIf CurrentIndex = -1 Or Index <> CurrentIndex Then
        If CurrentIndex <> -1 Then
            lblLetters(CurrentIndex).ForeColor = UserControl.ForeColor
            lblLetters(CurrentIndex).FontBold = False
        End If
        CurrentIndex = Index
        lblLetters(Index).ForeColor = m_HighlightColor
        lblLetters(Index).FontBold = m_HighlightBold
        tmrTimer.Enabled = True
    End If
End Sub

Private Sub SetButtonBackgrounds()
Dim i As Integer

    For i = 0 To 5
        cmdFunctions(i).BackColor = m_ButtonBackgroundColor
    Next i
    For i = 0 To 1
        cmdMovement(i).BackColor = m_ButtonBackgroundColor
    Next i
    cmdMenu.BackColor = m_ButtonBackgroundColor
End Sub

Public Property Get ShowNumeric() As Boolean
    ShowNumeric = m_ShowNumeric
End Property

Public Property Let ShowNumeric(ByVal New_ShowNumeric As Boolean)
    m_ShowNumeric = New_ShowNumeric
    lblLetters(0).Visible = m_ShowNumeric
    PropertyChanged "ShowNumeric"
End Property


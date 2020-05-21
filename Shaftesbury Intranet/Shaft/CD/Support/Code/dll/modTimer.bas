Attribute VB_Name = "modTimer"
Option Explicit
Private Declare Function PostMessage Lib "user32" Alias "PostMessageA" (ByVal hwnd As Long, ByVal wMsg As Long, ByVal wparam As Long, ByVal lparam As Long) As Long
Private Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Long, ByVal wMsg As Long, ByVal wparam As Long, lparam As Any) As Long
Private Declare Function SetTimer Lib "user32" (ByVal hwnd As Long, ByVal nIDEvent As Long, ByVal uElapse As Long, ByVal lpTimerFunc As Long) As Long
Private Declare Function KillTimer Lib "user32" (ByVal hwnd As Long, ByVal nIDEvent As Long) As Long
Private Declare Sub keybd_event Lib "user32" (ByVal bVk As Byte, ByVal bScan As Byte, ByVal dwFlags As Long, ByVal dwExtraInfo As Long)
Private Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Long
Private Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal hWnd1 As Long, ByVal hWnd2 As Long, ByVal lpsz1 As String, ByVal lpsz2 As String) As Long
Public Const NV_OUTLOOKFORM1 = &H5001&
Private Const WM_KEYUP = &H101, WM_KEYDOWN = &H100
Private OutlookHwnd As Long, lTimerReturn As Long

Public Function PrintContactsClass(bPreview As Boolean, Index As Integer, objFolder As MAPIFolder)
Dim e As Explorer, oCV As Object, oCustomView As Object
Dim sCaption As String, bFound As Boolean

    Select Case Index
        Case 0: sCaption = "isd All Contacts"
        Case 1: sCaption = "isd My Contacts"
        Case 3: sCaption = "isd Mixed Contacts"
        Case Else: Exit Function
    End Select
    
    Set e = objFolder.GetExplorer
    Dim x As New Outlook.Application
    
    'look for the old menu item first
    For Each oCV In e.CommandBars("Current View").Controls
        If LCase$(Trim$(oCV.Caption)) = LCase$(sCaption) Then
            'the item already exists so use it
            bFound = True
            oCV.Execute
            Exit For
        End If
    Next oCV
    If Not bFound Then
        e.Activate
        e.WindowState = olMinimized
        OutlookHwnd = FindWindow("rctrl_renwnd32", e.Caption)
        If OutlookHwnd > 0 Then
            'we ain't found it so look for the define views
            For Each oCV In e.CommandBars("Current View").Controls
                Debug.Print oCV.Caption
                If InStr(LCase$(oCV.Caption), "define views") > 0 Then
                    SetTimer OutlookHwnd, NV_OUTLOOKFORM1, 1, AddressOf NewTimerProc
                    'lTimerReturn = SetTimer(0, 0, 0, AddressOf NewTimerProc)
                    'e.Application.ActiveWindow.Name
                    If lTimerReturn > 0 Then
                        'oCV.Execute
                        KillTimer 0, lTimerReturn
                        
                        PostMessage 1770244, WM_KEYDOWN, &H11, &H1D0001   'control
                        PostMessage 1770244, WM_KEYDOWN, &H4E, &H310001 'N
                    End If
    '                SendMessage
    '                TAB SPACE
                End If
            Next oCV
        End If
    End If

'    e.CurrentView = "By Company"
'    For Each oCV In e.CommandBars("File").Controls
'        If InStr(1, oCV.Caption, "Print..") > 0 Then
'            oCV.Execute
'            Exit For
'        End If
'    Next oCV
    
    Set oCV = Nothing
    Set oCustomView = Nothing
    Set e = Nothing

End Function

Public Function NewTimerProc(ByVal hwnd As Long, ByVal Msg As Long, ByVal wparam As Long, ByVal lparam As Long) As Long
Dim mHandle As Long

    mHandle = FindWindow(vbNullString, "Define Views for " & Chr(34) & "Contacts" & Chr(34))
    If mHandle > 0 Then
        'only kill the timer if I've found my window
        'KillTimer 0, lTimerReturn
        KillTimer OutlookHwnd, NV_OUTLOOKFORM1
        PostMessage mHandle, WM_KEYDOWN, &H11, &H1D0001   'control
        PostMessage mHandle, WM_KEYDOWN, &H4E, &H310001 'N
'        PostMessage mHandle, WM_KEYDOWN, &H9, &HC00F0001
'        PostMessage mHandle, WM_KEYDOWN, &H20, &HC0390001
        
    End If
End Function

'    SetTimer oMDI.hwnd, NV_MOVEMSGBOX, 0&, AddressOf NewTimerProc
'    RepositionedMsgbox = MessageBox(oMDI.hwnd, Prompt, Title, Buttons)


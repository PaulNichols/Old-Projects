Attribute VB_Name = "modBrowse"
Option Explicit

'common to both methods
Private Type BROWSEINFO
  hOwner As Long
  pidlRoot As Long
  pszDisplayName As String
  lpszTitle As String
  ulFlags As Long
  lpfn As Long
  lParam As Long
  iImage As Long
End Type
Public Const BIF_RETURNONLYFSDIRS = &H1
Private Declare Function SHBrowseForFolder Lib _
   "shell32.dll" Alias "SHBrowseForFolderA" _
   (lpBrowseInfo As BROWSEINFO) As Long

Private Declare Function SHGetPathFromIDList Lib _
   "shell32.dll" Alias "SHGetPathFromIDListA" _
   (ByVal pidl As Long, _
   ByVal pszPath As String) As Long

Private Declare Sub CoTaskMemFree Lib "ole32.dll" (ByVal pv As Long)

Private Declare Function SendMessage Lib "user32" _
   Alias "SendMessageA" _
   (ByVal hwnd As Long, _
   ByVal wMsg As Long, _
   ByVal wParam As Long, _
   lParam As Any) As Long
   
Private Declare Sub MoveMemory Lib "kernel32" _
   Alias "RtlMoveMemory" _
   (pDest As Any, _
    pSource As Any, _
    ByVal dwLength As Long)
    
Private Const MAX_PATH = 260
Private Const WM_USER = &H400
Private Const BFFM_INITIALIZED = 1

'Constants ending in 'A' are for Win95 ANSI
'calls; those ending in 'W' are the wide Unicode
'calls for NT.

'Sets the status text to the null-terminated
'string specified by the lParam parameter.
'wParam is ignored and should be set to 0.
Private Const BFFM_SETSTATUSTEXTA As Long = (WM_USER + 100)
Private Const BFFM_SETSTATUSTEXTW As Long = (WM_USER + 104)

'If the lParam  parameter is non-zero, enables the
'OK button, or disables it if lParam is zero.
'(docs erroneously said wParam!)
'wParam is ignored and should be set to 0.
Private Const BFFM_ENABLEOK As Long = (WM_USER + 101)

'Selects the specified folder. If the wParam
'parameter is FALSE, the lParam parameter is the
'PIDL of the folder to select , or it is the path
'of the folder if wParam is the C value TRUE (or 1).
'Note that after this message is sent, the browse
'dialog receives a subsequent BFFM_SELECTIONCHANGED
'message.
Private Const BFFM_SETSELECTIONA As Long = (WM_USER + 102)
Private Const BFFM_SETSELECTIONW As Long = (WM_USER + 103)
   

'specific to the PIDL method
'Undocumented call for the example. IShellFolder's
'ParseDisplayName member function should be used instead.
Private Declare Function SHSimpleIDListFromPath Lib _
   "shell32" Alias "#162" _
   (ByVal szPath As String) As Long


'specific to the STRING method
Private Declare Function LocalAlloc Lib "kernel32" _
   (ByVal uFlags As Long, _
    ByVal uBytes As Long) As Long
    
Private Declare Function LocalFree Lib "kernel32" _
   (ByVal hMem As Long) As Long

Private Declare Function lstrcpyA Lib "kernel32" _
   (lpString1 As Any, lpString2 As Any) As Long

Private Declare Function lstrlenA Lib "kernel32" _
   (lpString As Any) As Long

Private Const LMEM_FIXED = &H0
Private Const LMEM_ZEROINIT = &H40
Private Const LPTR = (LMEM_FIXED Or LMEM_ZEROINIT)

'windows-defined type OSVERSIONINFO
Private Type OSVERSIONINFO
  OSVSize         As Long
  dwVerMajor      As Long
  dwVerMinor      As Long
  dwBuildNumber   As Long
  PlatformID      As Long
  szCSDVersion    As String * 128
End Type
Private Const VER_PLATFORM_WIN32_NT = 2
Private Declare Function GetVersionEx Lib "kernel32" Alias "GetVersionExA" _
  (lpVersionInformation As OSVERSIONINFO) As Long


Private Function BrowseCallbackProcStr(ByVal hwnd As Long, _
                                      ByVal uMsg As Long, _
                                      ByVal lParam As Long, _
                                      ByVal lpData As Long) As Long
                                       
  'Callback for the Browse STRING method.
 
  'On initialization, set the dialog's
  'pre-selected folder from the pointer
  'to the path allocated as bi.lParam,
  'passed back to the callback as lpData param.
 
   Select Case uMsg
      Case BFFM_INITIALIZED
      
         Call SendMessage(hwnd, BFFM_SETSELECTIONA, _
                          True, ByVal lpData)
                          
         Case Else:
         
   End Select
          
End Function
          

Private Function BrowseCallbackProc(ByVal hwnd As Long, _
                                   ByVal uMsg As Long, _
                                   ByVal lParam As Long, _
                                   ByVal lpData As Long) As Long

  'Callback for the Browse PIDL method.

  'On initialization, set the dialog's
  'pre-selected folder using the pidl
  'set as the bi.lParam, and passed back
  'to the callback as lpData param.

   Select Case uMsg
      Case BFFM_INITIALIZED

         Call SendMessage(hwnd, BFFM_SETSELECTIONA, _
                          False, ByVal lpData)

         Case Else:

   End Select

End Function


Private Function FARPROC(pfn As Long) As Long
  
  'A dummy procedure that receives and returns
  'the value of the AddressOf operator.
 
  'Obtain and set the address of the callback
  'This workaround is needed as you can't assign
  'AddressOf directly to a member of a user-
  'defined type, but you can assign it to another
  'long and use that (as returned here)
 
  FARPROC = pfn

End Function


Private Function BrowseForFolderByPIDL(sSelPath As String, nHandle As Long, sMessage As String) As String

   Dim BI As BROWSEINFO
   Dim pidl As Long
   Dim spath As String * MAX_PATH

   With BI
      .hOwner = nHandle
      .pidlRoot = 0
      .lpszTitle = sMessage
      .lpfn = FARPROC(AddressOf BrowseCallbackProc)
      .lParam = GetPIDLFromPath(sSelPath) 'replaces '= SHSimpleIDListFromPath(sSelPath)'
      .ulFlags = BIF_RETURNONLYFSDIRS
      .pszDisplayName = String$(MAX_PATH, 0)
   End With


   pidl = SHBrowseForFolder(BI)

   If pidl Then
      If SHGetPathFromIDList(pidl, spath) Then
         BrowseForFolderByPIDL = Left$(spath, InStr(spath, vbNullChar) - 1)
      End If

     'free the pidl returned by call to SHBrowseForFolder
      Call CoTaskMemFree(pidl)
  End If

 'free the pidl set in call to GetPIDLFromPath
  Call CoTaskMemFree(BI.lParam)

End Function


Private Function GetPIDLFromPath(spath As String) As Long

  'return the pidl to the path supplied by calling the
  'undocumented API #162 (our name SHSimpleIDListFromPath).
  'This function is necessary as, unlike documented APIs,
  'the API is not implemented in 'A' or 'W' versions.

  If IsWinNT Then
    GetPIDLFromPath = SHSimpleIDListFromPath(StrConv(spath, vbUnicode))
  Else
    GetPIDLFromPath = SHSimpleIDListFromPath(spath)
  End If

End Function


Private Function IsWinNT() As Boolean
   #If Win32 Then
   Dim OSV As OSVERSIONINFO
      OSV.OSVSize = Len(OSV)
     'API returns 1 if a successful call
      If GetVersionEx(OSV) = 1 Then
        'PlatformId contains a value representing
        
        'the OS, so if its VER_PLATFORM_WIN32_NT,
        'return true
         IsWinNT = OSV.PlatformID = VER_PLATFORM_WIN32_NT
      End If
   #End If
End Function

Private Function UnqualifyPath(spath As String) As String
   If Len(spath) > 0 Then
      If Right$(spath, 1) = "\" Then
         UnqualifyPath = Left$(spath, Len(spath) - 1)
         Exit Function
      End If
   End If
   UnqualifyPath = spath
End Function

Public Function Browse(Path As String, nHandle As Long, sMessage As String) As String
    Browse = BrowseForFolderByPIDL(UnqualifyPath((Path)), nHandle, sMessage)
End Function

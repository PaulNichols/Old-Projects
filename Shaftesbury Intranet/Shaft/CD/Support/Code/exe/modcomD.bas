Attribute VB_Name = "modcomD"
Option Explicit
'Declare API calls to obtain Common Dialogs
Public Declare Function GetOpenFileName Lib "comdlg32.dll" Alias "GetOpenFileNameA" (pOpenfilename As OPENFILENAME) As Long

'Declare Type Structures for API Calls
Public Type OPENFILENAME
        lStructSize As Long
        hwndOwner As Long
        hInstance As Long
        lpstrFilter As String
        lpstrCustomFilter As String
        nMaxCustFilter As Long
        nFilterIndex As Long
        lpstrFile As String
        nMaxFile As Long
        lpstrFileTitle As String
        nMaxFileTitle As Long
        lpstrInitialDir As String
        lpstrTitle As String
        flags As Long
        nFileOffset As Integer
        nFileExtension As Integer
        lpstrDefExt As String
        lCustData As Long
        lpfnHook As Long
        lpTemplateName As String
End Type



Public Function APIGetOpenFileName(apiDialogtitle As String, apiInitDir As String, apifilter As String, apiflags As Long, apiDefaultExt As String, apimaxfilesize As Long, apifilterindex As Long, apiHwndOwner As Long) As String
'Obtain the OPEN dialog.

Dim apitypesetting As OPENFILENAME
Dim retval As Long

With apitypesetting
    .lStructSize = Len(apitypesetting)
    .hwndOwner = apiHwndOwner
    .lpstrFilter = apifilter & Chr(0)
    .nFilterIndex = apifilterindex
    .lpstrFile = String(256, 0)
    .nMaxFile = Len(.lpstrFile) - 1
    .lpstrFileTitle = .lpstrFile
    .nMaxFileTitle = .nMaxFile
    .lpstrTitle = apiDialogtitle & vbNullString
    .flags = apiflags
    .lpstrDefExt = apiDefaultExt & vbNullString
    .hInstance = 0
    .lpstrCustomFilter = vbNullString
    .nMaxCustFilter = 0
    .lpstrInitialDir = apiInitDir & vbNullString
    .nFileOffset = 0
    .nFileExtension = 0
    .lCustData = 0
    .lpfnHook = 0
    .lpTemplateName = " " & vbNullString
End With

retval = GetOpenFileName(apitypesetting)

If retval <> 0 Then
    APIGetOpenFileName = Left(apitypesetting.lpstrFile, InStr(1, apitypesetting.lpstrFile, Chr(0)) - 1)
Else
    'If vbObjectError + 23371 <> -2147220504 Then Err.Raise vbObjectError + 23371, "IrCommonDialog.APIGetOpenFilename", "Common Dialog : OPEN, failed to initialise"
End If

End Function

Public Function APIGetSaveFileName() As String

End Function

Public Function APIColour() As String

End Function

Public Function APIFont() As String

End Function

Public Function APIPrint() As String

End Function



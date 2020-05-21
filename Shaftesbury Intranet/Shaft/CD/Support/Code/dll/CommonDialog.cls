VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCommonDialog"
Attribute VB_GlobalNameSpace = True
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

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

If retval <> 0 And Len(apitypesetting.lpstrFile) > 0 Then
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



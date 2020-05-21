Attribute VB_Name = "modReg"
Option Explicit

'********************************************************
' Functions

Declare Function RegCreateKeyEx& Lib "advapi32.dll" Alias "RegCreateKeyExA" (ByVal hKey As Long, ByVal lpSubKey As String, ByVal Reserved As Long, ByVal lpClass As String, ByVal dwOptions As Long, ByVal samDesired As Long, lpSecurityAttributes As SECURITY_ATTRIBUTES, phkResult As Long, lpdwDisposition As Long)
Declare Function RegOpenKeyEx& Lib "advapi32.dll" Alias "RegOpenKeyExA" (ByVal hKey As Long, ByVal lpSubKey As String, ByVal ulOptions As Long, ByVal samDesired As Long, phkResult As Long)
Declare Function RegCloseKey& Lib "advapi32.dll" (ByVal hKey As Long)
Declare Function RegDeleteKey Lib "advapi32.dll" Alias "RegDeleteKeyA" (ByVal hKey As Long, ByVal lpSubKey As String) As Long


Declare Function RegSetValueExString Lib "advapi32.dll" Alias "RegSetValueExA" (ByVal hKey As Long, ByVal lpValueName As String, ByVal Reserved As Long, ByVal dwType As Long, ByVal lpValue As String, ByVal cbData As Long) As Long
Declare Function RegSetValueExLong Lib "advapi32.dll" Alias "RegSetValueExA" (ByVal hKey As Long, ByVal lpValueName As String, ByVal Reserved As Long, ByVal dwType As Long, lpValue As Long, ByVal cbData As Long) As Long
Declare Function RegQueryValueExString Lib "advapi32.dll" Alias "RegQueryValueExA" (ByVal hKey As Long, ByVal lpValueName As String, ByVal lpReserved As Long, lpType As Long, ByVal lpData As String, lpcbData As Long) As Long
Declare Function RegQueryValueExLong Lib "advapi32.dll" Alias "RegQueryValueExA" (ByVal hKey As Long, ByVal lpValueName As String, ByVal lpReserved As Long, lpType As Long, lpData As Long, lpcbData As Long) As Long

Public Declare Function RegEnumKeyEx Lib "advapi32.dll" Alias "RegEnumKeyExA" _
(ByVal hKey As Long, ByVal dwIndex As Long, ByVal lpName As String, _
lpcbName As Long, ByVal lpReserved As Long, ByVal lpClass As String, _
lpcbClass As Long, lpftLastWriteTime As FILETIME) As Long

'********************************************************
' Types

Public Type FILETIME
    dwLowDateTime As Long
    dwHighDateTime As Long
End Type



'********************************************************
' Constants

Private Type SECURITY_ATTRIBUTES
    nLength As Long
    lpSecurityDescriptor As Long
    bInheritHandle As Long
End Type
Private Const STANDARD_RIGHTS_ALL = &H1F0000
Private Const KEY_QUERY_VALUE = &H1
Private Const KEY_SET_VALUE = &H2
Private Const KEY_CREATE_SUB_KEY = &H4
Private Const KEY_ENUMERATE_SUB_KEYS = &H8
Private Const KEY_NOTIFY = &H10
Private Const KEY_CREATE_LINK = &H20
Private Const SYNCHRONIZE = &H100000

Public Const HKEY_LOCAL_MACHINE = &H80000002
Public Const ERROR_SUCCESS& = 0&
Public Const KEY_ALL_ACCESS = ((STANDARD_RIGHTS_ALL Or KEY_QUERY_VALUE Or KEY_SET_VALUE Or KEY_CREATE_SUB_KEY Or KEY_ENUMERATE_SUB_KEYS Or KEY_NOTIFY Or KEY_CREATE_LINK) And (Not SYNCHRONIZE))

Const REG_OPTION_NON_VOLATILE& = 0
Public Const REG_SZ = 1

Const REG_CREATED_NEW_KEY& = &H1
Private objSECURITY_ATTRIBUTES As SECURITY_ATTRIBUTES
Public Const REG_DWORD = 4

Public Const AppPath$ = "Software\IR Software\Shaftsbury Contacts"
Public Function ReadRegEntry(ValueName As String, Optional SubKeyName As String, Optional Default) As Variant

Dim nSuccess&, nKeyhandle&
Dim nLen&, nReserved&, nVarType&, chSubKey$
Dim chRetVal$
Dim nRetVal&

On Error GoTo ReadFail

chSubKey = AppPath
If SubKeyName <> "" Then chSubKey = chSubKey & "\" & SubKeyName

chRetVal = String$(1000, 0)

nSuccess = RegOpenKeyEx(HKEY_LOCAL_MACHINE, chSubKey, 0, KEY_ALL_ACCESS, nKeyhandle)
If nSuccess = ERROR_SUCCESS Then nSuccess = RegQueryValueExString(nKeyhandle, ValueName, 0, nVarType, ByVal chRetVal, nLen)

Select Case nVarType
 Case REG_SZ:
  nSuccess = RegQueryValueExString(nKeyhandle, ValueName, 0, nVarType, chRetVal, nLen)
  If nSuccess = ERROR_SUCCESS Then
  If nLen = 0 Then
        'ReadRegEntry = ""
    Else
    ReadRegEntry = Left(chRetVal, nLen - 1)
    End If
End If
 Case REG_DWORD:
  nSuccess = RegQueryValueExLong(nKeyhandle, ValueName, 0, nVarType, nRetVal, nLen)
  If nSuccess = ERROR_SUCCESS Then ReadRegEntry = nRetVal
End Select

If Not IsMissing(Default) Then
 If IsEmpty(ReadRegEntry) Then ReadRegEntry = Default
End If

Exit Function
ReadFail:
ReadRegEntry = -1

End Function

Public Function UpdateRegEntry(ValueName As String, aValue As String, Optional SubKeyName As String) As Long

Dim nSuccess&, nKeyhandle&
Dim nLenValue&, nVarType&, chSubKey$

On Error GoTo RegFail

nVarType = IIf(VarType(aValue) = vbString, REG_SZ, REG_DWORD)
chSubKey = "Software\IR Software\Shaftsbury Contacts"
If SubKeyName <> "" Then chSubKey = chSubKey & "\" & SubKeyName

nSuccess = RegOpenKeyEx(HKEY_LOCAL_MACHINE, chSubKey, 0, KEY_ALL_ACCESS, nKeyhandle)
If nSuccess = ERROR_SUCCESS Then nSuccess = RegSetValueExString(nKeyhandle, ValueName, 0, nVarType, ByVal aValue, Len(aValue))

Call RegCloseKey(nKeyhandle)
UpdateRegEntry = nSuccess

Exit Function

RegFail:
UpdateRegEntry = Err

End Function

Public Function CreateRegEntry(ValueName As String, Value As Variant, Optional nVarType As Long, Optional SubKeyName As String) As Long

Dim nSuccess&, nKeyhandle&
Dim nLenValue&, chSubKey$

On Error GoTo RegFail

'nVarType = IIf(VarType(aValue) = vbString, REG_SZ, REG_DWORD)
chSubKey = AppPath$
If SubKeyName <> "" Then chSubKey = chSubKey & "\" & SubKeyName
If nVarType = 0 Then nVarType = 1


nSuccess = RegOpenKeyEx(HKEY_LOCAL_MACHINE, chSubKey, 0, KEY_ALL_ACCESS, nKeyhandle)
If nSuccess <> ERROR_SUCCESS Then nSuccess = RegCreateKeyEx(HKEY_LOCAL_MACHINE, chSubKey, 0, 1, REG_OPTION_NON_VOLATILE, KEY_ALL_ACCESS, objSECURITY_ATTRIBUTES, 0, 0)
    
Select Case nVarType
 Case REG_SZ:
  nSuccess = RegSetValueExString(nKeyhandle, ValueName, 0, nVarType, Value, Len(Value))
 ' If nSuccess = ERROR_SUCCESS Then ReadRegEntry = Left(chRetVal, nLen - 1)
 Case REG_DWORD:
  nSuccess = RegSetValueExLong(nKeyhandle, ValueName, 0, nVarType, Value, Len(Value))
  'If nSuccess = ERROR_SUCCESS Then ReadRegEntry = nRetVal
End Select

Call RegCloseKey(nKeyhandle)
CreateRegEntry = nSuccess

Exit Function

RegFail:
CreateRegEntry = Err

End Function

Public Function DelRegEntry(SubKeyName As String) As Long
Dim nSuccess&, nKeyhandle&, chSubKey$, chRetVal$, nRetVal&
    
    chRetVal = String$(1000, 0)
    
    nSuccess = RegOpenKeyEx(HKEY_LOCAL_MACHINE, "", 0, KEY_ALL_ACCESS, nKeyhandle)
    If nSuccess = ERROR_SUCCESS Then nSuccess = RegDeleteKey(nKeyhandle, SubKeyName)
    DelRegEntry = nSuccess
End Function

Public Function GetASetting(ValueName As String, ThisUser As String, DefaultValue As Variant, ReturnValue, Optional ntype) As Long
    
    ReturnValue = ReadRegEntry(ValueName, ThisUser)
    If IsEmpty(ReturnValue) Or ReturnValue = "" Then
        GetASetting = CreateRegEntry(ValueName, DefaultValue, , ThisUser)
        If GetASetting = 0 Then
            If VarType(ReturnValue) = vbString Then
                ReturnValue = CStr(DefaultValue)
            Else
                ReturnValue = DefaultValue
            End If
            Exit Function
        Else
           
        End If
    End If
    If Not IsMissing(ntype) Then
        If ntype = vbString Then
            ReturnValue = CStr(ReturnValue)
            If ReturnValue = -1 Then ReturnValue = ""
        Else
            ReturnValue = CLng(ReturnValue)
        End If
    End If
End Function




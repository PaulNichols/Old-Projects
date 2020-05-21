Attribute VB_Name = "modutils"
Option Explicit
Global Const DEFAULT_DATE = "01/01/1800"
Public Type POINTAPI
        x As Long
        y As Long
End Type
Dim sCurUser$
Public Enum EncryptDecrypt
    ENCODE = 1
    DECODE = 2
End Enum



Const msScramble = "soDSMDHnsQ" 'This is used for encryption
Public Const constClosedOut = 10
Public Const constOnHold = 11

Public Declare Function GetComputerName Lib "kernel32" Alias "GetComputerNameA" (ByVal lpBuffer As String, nSize As Long) As Long
Public Declare Function GetCursorPos Lib "user32" (lpPoint As POINTAPI) As Long
Public nCurRecID&


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


Public Function Currentuser() As String
 Currentuser = sCurUser$
End Function


Public Function ComputerName() As String
 Dim nLen&
 nLen = 100
 On Error Resume Next
 ComputerName = String$(nLen, 0)
 GetComputerName ComputerName, nLen
 ComputerName = Left(ComputerName, nLen)
End Function

Public Function ConvertString(aString As String, Optional AddPercent) As String

Dim sadd As String

    aString = Stuff(aString, Chr$(34), Chr$(34) & Chr$(34))
    aString = Stuff(aString, Chr$(39), Chr$(39) & Chr$(39))
    sadd = IIf(IsMissing(AddPercent), "", "%")
    ConvertString = "'" & aString & sadd & "'"
End Function

Public Function Stuff(sPwdString As String, asSearch As String, asReplace) As String
Dim i As Integer

    i = InStr(sPwdString, asSearch)
    Do Until i = 0
        sPwdString = Left$(sPwdString, i - 1) + asReplace + Mid$(sPwdString, i + Len(asSearch))
        i = InStr(i + Len(asReplace), sPwdString, asSearch)
    Loop
    Stuff = sPwdString
End Function

Public Function Encrypt(sPwdString As String, abEncryptType As EncryptDecrypt) As String
Dim nCount As Integer, nCode1 As Integer, nCode2 As Integer
Dim sScrambled As String, nStringLength As Integer

    If sPwdString <> "" Then
        nStringLength = Len(msScramble)

        Select Case abEncryptType
            Case ENCODE
                For nCount = 1 To Len(sPwdString)
                    nCode1 = Asc(Mid$(sPwdString, nCount, 1))
                    nCode2 = Asc(Mid$(msScramble, (nCount Mod nStringLength) + 1, 1))
                    nCode1 = nCode1 Xor nCode2
                    sScrambled = sScrambled + Chr$(Asc("z") - (nCode1 \ 16)) + Chr$(Asc("a") + (nCode1 Mod 16))
                Next nCount
            Case DECODE
                For nCount = 1 To Len(sPwdString) Step 2
                    nCode1 = (Asc("z") - Asc(Mid$(sPwdString, nCount, 1))) * 16 + (Asc(Mid$(sPwdString, nCount + 1, 1)) - Asc("a"))
                    nCode2 = Asc(Mid$(msScramble, (((nCount + 1) / 2) Mod nStringLength) + 1, 1))
                    nCode1 = nCode1 Xor nCode2
                    sScrambled = sScrambled + Chr$(nCode1)
                Next nCount
        End Select

        Encrypt = sScrambled
    End If
End Function
Private Sub connectme(objConnection As ADODB.Connection, DBPath As String)
'objConnection.Open "Provider=SQLOLEDB.1;Persist Security Info=False;User ID=sa;Initial Catalog=isd;Data Source=pdc_server;Connect Timeout=15"
  objConnection.Provider = "Microsoft.Jet.OLEDB.4.0"
    objConnection.Open DBPath
End Sub
Public Function ExecuteSql(ssql As String, DBPath As String, Optional rs) As Boolean
Dim RowsAffected As Long

  If con.State = adStateClosed Then
        connectme con, DBPath
    End If
     If IsMissing(rs) Then
        con.Execute ssql, RowsAffected
        ExecuteSql = (RowsAffected > 0)
    Else

        Set rs = con.Execute(ssql, RowsAffected)
        
        If (rs.EOF And rs.BOF) Then
            rs.Close
            ExecuteSql = False
        Else
            ExecuteSql = True
        End If
    End If
    'con.Close
End Function

Public Function NullToEmpty(avValue, Optional avDefault) As Variant

    If IsMissing(avDefault) Then
        Select Case VarType(avValue)
            Case vbString: avDefault = ""
            Case vbBoolean: avDefault = False
            Case vbDate: avDefault = DEFAULT_DATE
            Case Else: avDefault = 0
        End Select
    End If

    If IsNull(avValue) Then
        NullToEmpty = avDefault
    Else
        If VarType(avValue) = vbDate Then
            NullToEmpty = IIf(CDate("00:00:00") = avValue, avDefault, avValue)
        Else
            NullToEmpty = avValue
        End If
    End If
End Function

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
  If nSuccess = ERROR_SUCCESS Then ReadRegEntry = Left(chRetVal, nLen - 1)
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
chSubKey = AppPath$
If SubKeyName <> "" Then chSubKey = chSubKey & "\" & SubKeyName

nSuccess = RegOpenKeyEx(HKEY_LOCAL_MACHINE, chSubKey, 0, KEY_ALL_ACCESS, nKeyhandle)
If nSuccess = ERROR_SUCCESS Then nSuccess = RegSetValueExString(nKeyhandle, ValueName, 0, nVarType, ByVal aValue, Len(aValue))

Call RegCloseKey(nKeyhandle)
UpdateRegEntry = nSuccess

Exit Function

RegFail:
UpdateRegEntry = Err

End Function

Public Function CreateRegEntry(ValueName As String, value As Variant, Optional nVarType As Long, Optional SubKeyName As String) As Long

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
  nSuccess = RegSetValueExString(nKeyhandle, ValueName, 0, nVarType, value, 1000)
 ' If nSuccess = ERROR_SUCCESS Then ReadRegEntry = Left(chRetVal, nLen - 1)
 Case REG_DWORD:
  nSuccess = RegSetValueExLong(nKeyhandle, ValueName, 0, nVarType, value, 1000)
  'If nSuccess = ERROR_SUCCESS Then ReadRegEntry = nRetVal
End Select

Call RegCloseKey(nKeyhandle)
CreateRegEntry = nSuccess

Exit Function

RegFail:
CreateRegEntry = Err

End Function




Public Function DelRegEntry(SubKeyName As String) As Long
Dim nSuccess&, nKeyhandle&
Dim chSubKey$
Dim chRetVal$
Dim nRetVal&



    'chSubKey = AppPath
'    If SubKeyName <> "" Then chSubKey = chSubKey & "\" & SubKeyName
    
    chRetVal = String$(1000, 0)
    
    nSuccess = RegOpenKeyEx(HKEY_LOCAL_MACHINE, "", 0, KEY_ALL_ACCESS, nKeyhandle)
    'chSubKey = ""
    If nSuccess = ERROR_SUCCESS Then nSuccess = RegDeleteKey(nKeyhandle, SubKeyName)
    DelRegEntry = nSuccess
End Function


Public Function NextId(aTableName As String, DBPath As String, Optional nKey, Optional idField As String) As Long
Dim SqlString As String, ErrNumber As Long, bTest As Boolean, rs As Recordset
Dim Id As Long


    If IsMissing(nKey) Then
       SqlString = "select Max(id) + 1 as NewId from " & aTableName
    Else
       SqlString = "select Max(id), Max(" & idField & ") + 1 as NewId " & _
                   "From " & aTableName & " Where id = " & nKey
    End If
    
    If ExecuteSql(SqlString, DBPath, rs) Then
        Id = NullToEmpty(rs!NewId, 1)
        rs.Close
        Do
            If IsMissing(nKey) Then
               SqlString = "Insert Into " & aTableName & " (id) values (" & Id & ")"
            Else
               SqlString = "Insert Into " & aTableName & " (id," & idField & ") " & _
                           "Values (" & nKey & "," & Id & ")"
            End If
            
            ErrNumber = 0
            ExecuteSql SqlString, DBPath
            If ErrNumber = 20 Then
                Id = Id + 1
            Else
                NextId = Id
                Exit Do
            End If
        Loop
    End If

End Function





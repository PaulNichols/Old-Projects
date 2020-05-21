VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsSettings"
Attribute VB_GlobalNameSpace = True
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Public Enum ContactViews
    CardView = 1
    GridView = 2
End Enum
Public ContactsView As ContactViews
Public Columns As New Collection
Public ContactsGridViewSortedBy As Integer
Public ContactsScreenWidth As Double
Public ContactsScreenTop As Double
Public ContactsScreenLeft As Double
Public ContactsScreenHeight As Double
Private sFields As String
Public Highlighttext As String
Public TemplatePath As String
Public exchserver As String
Public PDC As String
Public PST As String
Public Profile As String
Public OfflinePST As String
Public OnlinePST As String
Public ViewType As Integer
Public CurrentLetter As String
Public onlineProfile As String
Public offlineprofile As String
Public WebPage As String
Public Background As Long
Public CompanyCol As Long
Public GridBackCol As Long
Public GridBorders As Long
Public PrivateCol As Long
Public Seeall As String
Public GlobalTemplatePath As String

Public Property Get AvalibleFields() As String
    AvalibleFields = sFields
End Property
Public Property Let AvalibleFields(Thefields As String)
    sFields = Thefields
End Property

'Public Property Get getDBPath() As String
'    getDBPath = dbpath
'End Property
'
'Public Property Let setDBPath(sPath)
'    dbpath = sPath
'End Property

Public Function SaveSettings(ThisUser As String) As Boolean
Dim i As Long

    If UpdateRegEntry("ContactsView", CStr(ContactsView), ThisUser) <> 0 Then SaveSettings = True: Exit Function
    If UpdateRegEntry("ContactsGridViewSortedBy", CStr(ContactsGridViewSortedBy), ThisUser) <> 0 Then SaveSettings = True: Exit Function
    For i = 1 To Columns.Count
        If UpdateRegEntry("column" & i, CStr(Columns(i).width & "," & Columns(i).Name & "," & Columns(i).Position), ThisUser) <> 0 Then SaveSettings = True: Exit Function
    Next i
    If UpdateRegEntry("ContactsScreenWidth", CStr(ContactsScreenWidth), ThisUser) <> 0 Then SaveSettings = True: Exit Function
    If UpdateRegEntry("ContactsScreenHeight", CStr(ContactsScreenHeight), ThisUser) <> 0 Then SaveSettings = True: Exit Function
    If UpdateRegEntry("ContactsScreenleft", CStr(ContactsScreenLeft), ThisUser) <> 0 Then SaveSettings = True: Exit Function
    If UpdateRegEntry("ContactsScreentop", CStr(ContactsScreenTop), ThisUser) <> 0 Then SaveSettings = True: Exit Function
    If UpdateRegEntry("PDC", CStr(PDC), "") <> 0 Then SaveSettings = True: Exit Function
    If UpdateRegEntry("exchserver", CStr(exchserver), "") <> 0 Then SaveSettings = True: Exit Function
    If UpdateRegEntry("TemplatePath", CStr(TemplatePath), ThisUser) <> 0 Then SaveSettings = True: Exit Function
    If UpdateRegEntry("Highlighttext", CStr(Highlighttext), ThisUser) <> 0 Then SaveSettings = True: Exit Function
    
   ' If UpdateRegEntry("pst", CStr(PST), ThisUser) <> 0 Then SaveSettings = True: Exit Function
    If UpdateRegEntry("profile", CStr(Profile), ThisUser) <> 0 Then SaveSettings = True: Exit Function
   ' If UpdateRegEntry("onlinepst", CStr(OnlinePST), ThisUser) <> 0 Then SaveSettings = True: Exit Function
    'If UpdateRegEntry("offlinepst", IIf(CStr(OfflinePST) = "", "  ", CStr(OfflinePST)), ThisUser) <> 0 Then SaveSettings = True: Exit Function
    If UpdateRegEntry("ViewType", CStr(ViewType), ThisUser) <> 0 Then SaveSettings = True: Exit Function
    'If UpdateRegEntry("CurrentLetter", IIf(CurrentLetter = "", "A", CStr(CurrentLetter)), ThisUser) <> 0 Then SaveSettings = True: Exit Function
    If UpdateRegEntry("CurrentLetter", IIf(CurrentLetter = "", "*", "*"), ThisUser) <> 0 Then SaveSettings = True: Exit Function
    If UpdateRegEntry("onlineprofile", CStr(onlineProfile), ThisUser) <> 0 Then SaveSettings = True: Exit Function
    'If UpdateRegEntry("offlineprofile", IIf(CStr(offlineprofile) = "", "  ", CStr(offlineprofile)), ThisUser) <> 0 Then SaveSettings = True: Exit Function
    If UpdateRegEntry("webpage", IIf(CStr(WebPage) = "", "http:\\www.yahoo.co.uk", CStr(WebPage)), ThisUser) <> 0 Then SaveSettings = True: Exit Function

    If UpdateRegEntry("Background", CStr(Background), ThisUser) <> 0 Then SaveSettings = True: Exit Function
    If UpdateRegEntry("CompanyCol", CStr(CompanyCol), ThisUser) <> 0 Then SaveSettings = True: Exit Function
    If UpdateRegEntry("GridBackCol", CStr(GridBackCol), ThisUser) <> 0 Then SaveSettings = True: Exit Function
    If UpdateRegEntry("GridBorders", CStr(GridBorders), ThisUser) <> 0 Then SaveSettings = True: Exit Function
    If UpdateRegEntry("PrivateCol", CStr(PrivateCol), ThisUser) <> 0 Then SaveSettings = True: Exit Function

    If UpdateRegEntry("GlobalTemplatePath", GlobalTemplatePath, ThisUser) <> 0 Then SaveSettings = True: Exit Function
End Function


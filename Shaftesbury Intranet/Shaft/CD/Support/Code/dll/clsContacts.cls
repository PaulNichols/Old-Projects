VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsContacts"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private colData As Collection
Private oData As clsContacts
Private rs As Recordset
Private ssql As String

Public id As Long
Public CompanyName As String
Public FullName As String
Public title As String
Public firstname As String
Public middlename As String
Public surname As String
Public Initials As String
Public Address1 As String
Public Address2 As String
Public Address3 As String
Public Address4 As String
Public PostCode As String
Public JobTitle As String
Public TelNo As String
Public BussFax As String
Public FaxNo As String
Public Mobile1 As String
Public Cat As String
Public Deleted As Boolean
Public email2 As String
Public email1 As String
Public suffix As String

Public Function load(Optional nId, Optional sLetter, Optional sOrderby, Optional owner) As Boolean
Dim sOrder As String

Set colData = New Collection
If IsMissing(nId) Then
    ssql = "select * from tcontacts "
    If Not IsMissing(owner) Then ssql = "select * from tcontacts where owner=" & owner
Else
    ssql = "select * from tcontacts where id=" & nId
    If Not IsMissing(owner) Then ssql = "select * from tcontacts where id=" & nId & " and  owner=" & owner
End If
If Not IsMissing(sLetter) Then
    If IsMissing(nId) Then
        If Not IsMissing(owner) Then
            ssql = ssql & " and surname like " & ConvertString(CStr(sLetter), True)
        Else
            ssql = ssql & " where surname like " & ConvertString(CStr(sLetter), True)
        End If
    Else
        ssql = ssql & " and surname like " & ConvertString(CStr(sLetter), True)
    End If

End If

Select Case sOrderby + 1
    Case 0: sOrder = "CompanyName"
    Case 1: sOrder = "FullName"
    Case 2: sOrder = "title"
    Case 3: sOrder = "firstname"
    Case 4: sOrder = "middlename"
    Case 5: sOrder = "surname"
    Case 6: sOrder = "Initials"
    Case 7: sOrder = "Address1"
    Case 8: sOrder = "Address2"
    Case 9: sOrder = "Address3"
    Case 10: sOrder = "Address4"
    Case 11: sOrder = "PostCode"
    Case 12: sOrder = "JobTitle"
    Case 13: sOrder = "TelNo"
    Case 14: sOrder = "BussFax"
    Case 15: sOrder = "FaxNo"
    Case 16: sOrder = "Mobile1"
    Case 17: sOrder = "Cat"
    Case 18: sOrder = "Deleted"
    Case 19: sOrder = "email2"
    Case 20: sOrder = "email1"
    Case 21: sOrder = "suffix"
    Case 22: sOrder = "FileAs"
End Select

ssql = ssql & " order by " & sOrder

If executesql(ssql, rs) Then
    With rs
        Do While Not (.EOF Or .BOF)
            Set oData = New clsContacts
            oData.id = !id
            oData.CompanyName = NullToEmpty(!CompanyName, "")
            oData.FullName = NullToEmpty(!FullName, "")
            oData.title = NullToEmpty(!title, "")
            oData.firstname = NullToEmpty(!firstname, "")
            oData.middlename = NullToEmpty(!middlename, "")
            oData.surname = NullToEmpty(!surname, "")
            oData.Initials = NullToEmpty(!Initials, "")
            oData.Address1 = NullToEmpty(!Address1, "")
            oData.Address2 = NullToEmpty(!Address2, "")
            oData.Address3 = NullToEmpty(!Address3, "")
            oData.Address4 = NullToEmpty(!Address4, "")
            oData.PostCode = NullToEmpty(!PostCode, "")
            oData.JobTitle = NullToEmpty(!JobTitle, "")
            oData.TelNo = NullToEmpty(!TelNo, "")
            oData.BussFax = NullToEmpty(!BussFax, "")
            oData.FaxNo = NullToEmpty(!FaxNo, "")
            oData.Mobile1 = NullToEmpty(!Mobile1, "")
            oData.Cat = NullToEmpty(!Cat, "")
            oData.Deleted = NullToEmpty(!Deleted, False)
            oData.email2 = NullToEmpty(!email2, "")
            oData.email1 = NullToEmpty(!email2, "")
            oData.suffix = NullToEmpty(!suffix, "")
            colData.Add oData, CStr(oData.id)
            .MoveNext
        Loop
        End With
        Set rs = Nothing
        load = True
End If
    
    Set oData = Nothing

End Function

Public Function item(vKey As Variant) As clsContacts
    If VarType(vKey) = vbLong Or VarType(vKey) = vbInteger Then
        If vKey > 0 And vKey <= colData.count Then
            Set item = colData.item(CLng(vKey))
        End If
    Else
        Set item = colData.item(CStr(vKey))
    End If
End Function


Public Function count() As Long
count = colData.count
End Function

Public Function Save(Optional oData As clsContacts, Optional colData As Collection) As Boolean

End Function


Public Function Property(vKey As Variant, nItem As Long) As Variant

    If VarType(vKey) = vbInteger Or VarType(vKey) = vbLong Then
            Select Case vKey
                Case 1: Property = "id"
                Case 2: Property = "Company Name"
                Case 3: Property = "Full Name"
                Case 4: Property = "title"
                Case 5: Property = "firstname"
                Case 6: Property = "middlename"
                Case 7: Property = "surname"
                Case 8: Property = "Initials"
                Case 9: Property = "Address1"
                Case 10: Property = "Address2"
                Case 11: Property = "Address3"
                Case 12: Property = "Address4"
                Case 13: Property = "PostCode"
                Case 14: Property = "JobTitle"
                Case 15: Property = "TelNo"
                Case 16: Property = "BussFax"
                Case 17: Property = "Mobile1"
                Case 18: Property = "Cat"
                Case 19: Property = "Deleted"
                Case 20: Property = "email2"
                Case 21: Property = "suffix"
                Case 22: Property = "email1"
            End Select
    Else
        Select Case vKey
            Case "id": Property = colData.item(nItem).id
            Case "Company Name": Property = colData.item(nItem).CompanyName
            Case "First Name": Property = colData.item(nItem).FullName
            Case "Title": Property = colData.item(nItem).title
            Case "First Name": Property = colData.item(nItem).firstname
            Case "middle name": Property = colData.item(nItem).middlename
            Case "surname": Property = colData.item(nItem).surname
            Case "Initials": Property = colData.item(nItem).Initials
            Case "Address1": Property = colData.item(nItem).Address1
            Case "Address2": Property = colData.item(nItem).Address2
            Case "Address3": Property = colData.item(nItem).Address3
            Case "Address4": Property = colData.item(nItem).Address4
            Case "Post Code": Property = colData.item(nItem).PostCode
            Case "Job Title": Property = colData.item(nItem).JobTitle
            Case "Tel No": Property = colData.item(nItem).TelNo
            Case "BussFax": Property = colData.item(nItem).BussFax
            Case "Mobile1": Property = colData.item(nItem).Mobile1
            Case "Cat": Property = colData.item(nItem).Cat
            Case "Deleted": Property = colData.item(nItem).Deleted
            Case "email2": Property = colData.item(nItem).email2
            Case "suffix": Property = colData.item(nItem).suffix
            Case "email1": Property = colData.item(nItem).email1
        End Select
    End If


End Function

VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCols"
Attribute VB_GlobalNameSpace = True
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Public width As Double
Private sName As String
Public Position As Integer
Public DefaultColWidth As Double
Public PropertyName As String


Public Property Get Name() As String
Name = sName
End Property

Public Property Let Name(aName As String)
        sName = aName

        Select Case aName
        Case "File As": PropertyName = "FileAs"
        Case "Full Name": PropertyName = "fullname"
        Case "Company Name": PropertyName = "CompanyName"
        Case "Business Address": PropertyName = "BusinessAddress"
        Case "Business Fax No.": PropertyName = "BusinessFaxNumber"
        Case "Business Home Page": PropertyName = "BusinessHomePage"
        Case "Business Telephone No.": PropertyName = "BusinessTelephoneNumber"
        Case "Email Address": PropertyName = "Email1Address"
        Case "Mobile Telephone No.": PropertyName = "MobileTelephoneNumber"
        Case "Home Address": PropertyName = "homeaddress"
        Case "Home Fax No.": PropertyName = "HomeFaxNumber"
        Case "Home Telephone No.": PropertyName = "HomeTelephoneNumber"
        Case "Salutation": PropertyName = "Salutation"
        Case "CreateOfflineProfile": PropertyName = "CreateOfflineProfile"
        Case "FullPSTCopy": PropertyName = "FullPSTCopy"
        Case "Phone Abbr": PropertyName = "OtherTelephoneNumber"
        Case "Fax Abbr": PropertyName = "otherfaxnumber"
        End Select
'  Select Case aName
'        Case "File As": PropertyName = "File As"
'        Case "Full Name": PropertyName = "Display Name"
'        Case "Company Name": PropertyName = "Company"
'        Case "Business Address": PropertyName = "Business Address"
'        Case "Business Fax No.": PropertyName = "Fax Number"
'        Case "Business Home Page": PropertyName = "Web page"
'         Case "Business Telephone No.": PropertyName = "File As"
'        Case "Email Address": PropertyName = "File As"
'        Case "Mobile Telephone No.": PropertyName = "File As"
'        Case "Home Address": PropertyName = "File As"
'        Case "Home Fax No.": PropertyName = "File As"
'        Case "Home Telephone No.": PropertyName = "File As"
'        Case "Salutation": PropertyName = "File As"
'        Case "CreateOfflineProfile": PropertyName = "File As"
'        Case "FullPSTCopy": PropertyName = "File As"
'        Case "Phone Abbr": PropertyName = "File As"
'        End Select
End Property




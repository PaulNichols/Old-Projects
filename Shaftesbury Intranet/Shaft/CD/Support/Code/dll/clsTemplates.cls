VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsTemplates"
Attribute VB_GlobalNameSpace = True
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private sTPath As String

Public Address As String
Public Ref As String
Dim oWordApp As Word.Application
Dim oDoc As Word.Document

Public Function PrintEnv(sMailAddress As String) As Boolean
''Stop
'Dim oReport As Envelopes, ctl As Object
'
'    Set oReport = New Envelopes
'    With oReport
'        .MaxPages = 1
'        '566.929133858268
'        .Pages.Add
'        .Pages(0).width = 12472
'        .Pages(0).Height = 6292
'        .Pages(0).Font.Name = "Arial"
'        .Pages(0).Font.Size = 12
'        .Pages(0).DrawText Trim$(sMailAddress), 9000, 5750, 5200, 2400
'        Set ctl = oReport.PageHeader.Controls.Add("DDActiveReports.Field")
'        With ctl
'            .Left = 0
'            .Top = 0
'            .Height = 2500
'            .Text = sMailAddress
'            .Visible = True
'        End With
'        .Pages(0).Orientation = ddOLandscape
'        .Pages.Commit
'        .PrintReport False
'    End With
'    Set oReport = Nothing
'    PrintEnv = True
'    Exit Function
''Dim oWordApp As Object ', nAddressFromTop As Double
''Dim oWordApp As Object
''Dim oDoc As Object
'
''On Error Resume Next
''Set oWordApp = GetObject(, "Word.Application")
''If Err.Number > 0 Then Set oWordApp = CreateObject("Word.Application")
''On Error GoTo 0 'errcode
''Err.Clear
'Set oWordApp = New Word.Application
'With oWordApp
'    .Visible = True
'    '.Documents.Add
'    'nAddressFromTop =
'    'On Error Resume Next
'
'     Set oDoc = .Documents.Add(DocumentType:=wdNewBlankDocument)
'    oDoc.Envelope.PrintOut ExtractAddress:=False, OmitReturnAddress _
'        :=False, PrintBarCode:=False, PrintFIMA:=False, Height:=InchesToPoints( _
'        4.13), width:=InchesToPoints(9.5), Address:=sMailAddress, AutoText:= _
'        "", ReturnAddress:="", ReturnAutoText:= _
'        "", AddressFromLeft:=wdAutoPosition, AddressFromTop:= _
'        wdAutoPosition, ReturnAddressFromLeft:=wdAutoPosition, _
'        ReturnAddressFromTop:=wdAutoPosition, DefaultOrientation:=wdLeftLandscape _
'        , DefaultFaceUp:=True
'    Do While .BackgroundPrintingStatus <> 0
'        DoEvents
'    Loop
''    .ActiveDocument.Envelope.Address = sMailAddress
''    .ActiveDocument.Envelope.AddressFromTop = oWordApp.InchesToPoints(2)
''    .ActiveDocument.Envelope.PrintOut ' , , , , , , , , , , , , , nAddressFromTop
'    .Quit False
'End With
'
''Set oWordApp = Nothing
'PrintEnv = True
'Exit Function
'errcode:
'oWordApp.Quit False
''Set oWordApp = Nothing
'PrintEnv = False
End Function

Public Function PrintLabel(sMailAddress As String) As Boolean
'Dim oReport As Labels ', ctl As Object
'Dim nTop As Integer, nLeft As Integer, i As Integer, j As Integer
'Dim bAll As Boolean, nRow As Integer, nCol As Integer
'    Set oReport = New Labels
'    With frmLabels
'        .Show vbModal
'        If bDontPrintLabels Then GoTo here
'        bAll = CBool(.chkAll.value)
'        If Not bAll Then
'            nRow = .cboRow.ListIndex + 1
'            nCol = .cboColumn.ListIndex + 1
'        End If
'    End With
'    Unload frmLabels
'    Set frmLabels = Nothing
'    With oReport
'        .MaxPages = 1
'        '566.929133858268
'        .Pages.Add
'        .Pages(0).Font.Name = "Arial"
'        .Pages(0).Font.Size = 12
'        '.Pages(0).width = 12472
'        '.Pages(0).Height = 6292
'        '56.25
'        For j = IIf(bAll, 1, nCol) To IIf(bAll, 2, nCol)
'            For i = IIf(bAll, 1, nRow) To IIf(bAll, 7, nRow)
'                nLeft = IIf(j = 1, 1012, 6750)
'                nTop = 1181 + ((i - 1) * 2160)
'                .Pages(0).DrawText sMailAddress, nLeft, nTop, 5200, 2400
'            Next i
'        Next j
'        .Pages.Commit
'        .PrintReport False
'    End With
'here:
'    Set oReport = Nothing
'    PrintLabel = True
'    Exit Function
'
''On Error GoTo errcode
''Dim oWordApp As New Word.Application
''
''With oWordApp
''    .Visible = True
''    .Documents.Add
''    .Application.MailingLabel.DefaultPrintBarCode = False
''    .Application.MailingLabel.PrintOut Name:="", Address:=sMailAddress, _
''        LaserTray:=wdPrinterManualFeed, SingleLabel:=True, Row:=4, Column:=4
''    .Quit False
''End With
''
''Set oWordApp = Nothing
''PrintLabel = True
''Exit Function
''errcode:
''PrintLabel = False
End Function

Public Property Let TemplatePath(sPath As String)
    sTPath = sPath
End Property

Public Property Get TemplatePath() As String
TemplatePath = sTPath
End Property

Public Function RunTemplate(sTemplateName As String, Optional oContact As Object) As Boolean
Dim oWrdApp As New Word.Application
   ' If Len(Trim$(sTemplateName)) > 0 Then
        Screen.MousePointer = vbHourglass
        oWrdApp.Visible = True
        On Error Resume Next
        oWrdApp.Documents.Add sTemplateName
        If Err.Number > 0 Then
           ' MsgBox "There was a problem opening the template.", vbInformation, "Template"
        End If
        On Error GoTo 0
        Screen.MousePointer = vbNormal
   ' End If
    Set oWrdApp = Nothing
End Function

Public Function GetTemplates(names() As String) As Boolean
Dim i As Long

    ReDim snames(3, 1) As String
    
    sTPath = TemplatePath
    i = 0
    snames(i) = Dir(sTPath & "*.dot")
    Do While snames(i) <> ""
        i = i + 1
        snames(i) = Dir
    Loop
    If snames(i) = "" Then ReDim Preserve snames(i - 1, 1)
    
End Function

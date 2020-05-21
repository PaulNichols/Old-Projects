Option Explicit

Dim Response
Dim vbYesNoCancel
Dim vbQuestion
Dim vbCancel
Dim vbCrLf

vbYesNoCancel = 3
vbQuestion = 32
vbCancel = 2
vbCrLf = Chr(13) + Chr(10)

Response = MsgBox("Do you want to target the latest version of the .NET Framework and Visual Studio .NET?" & vbCrLf & vbCrLf & "Yes = .NET Framework 1.1 & VS .NET 2003" & vbCrLf & "No = .NET Framework 1.0 & VS .NET 2002" & vbCrLf & "Cancel = No change will be made", vbYesNoCancel + vbQuestion, "Which .NET version you want to use ?")

If Response = vbCancel Then WScript.Quit

DoTheWork Response
WScript.Echo "Visual Studio.NET toolbox was successfully uncustomized!"

Sub DoTheWork (ByVal Response)

	Dim myDTE
	Dim objToolbox
	Dim colTbxTabs
	Dim objTab
	Dim vsWindowKindToolbox
	Dim TabName
	Dim windows
	Dim visibleState
	Dim autoHidesState
	Dim vbYes
	Dim vbNo
	Dim ProgID

	vbYes = 6
	vbNo = 7

	If Response = vbYes Then

		ProgID = "VisualStudio.DTE.7.1"

	ElseIf Response = vbNo Then

		ProgID = "VisualStudio.DTE.7"

	End If
	
	vsWindowKindToolbox = "{B1E99781-AB81-11D0-B683-00AA00A3EE26}" 
	
	On Error Resume Next
	Set myDTE = GetObject(,ProgID)

	If myDTE Is Nothing Then
		Set myDTE = WScript.CreateObject(ProgID)
	End If

	If myDTE Is Nothing Then
		WScript.Echo "Visual Studio .NET is not installed on this computer."
		WScript.Quit	
	End If

	Err.Clear
	On Error Goto 0

	Set windows = myDTE.Windows.Item(vsWindowKindToolbox)
	visibleState = windows.Visible
	autoHidesState = windows.AutoHides
	windows.Visible = True
	windows.AutoHides = False
	
	Set objToolbox = windows.Object
	Set colTbxTabs = objToolbox.ToolBoxTabs

	'***************************************************************************
	TabName = "Bob Custom WinDataGrids"
	DeleteTab colTbxTabs, TabName

	'***************************************************************************
	TabName = "Bob Custom WinCheckedListBoxes"
	DeleteTab colTbxTabs, TabName

	'***************************************************************************
	TabName = "Bob Custom WinListBoxes"
	DeleteTab colTbxTabs, TabName

	'***************************************************************************
	TabName = "Bob Custom WinComboBoxes"
	DeleteTab colTbxTabs, TabName

	'***************************************************************************
	TabName = "Bob Standard WinDataGrids"
	DeleteTab colTbxTabs, TabName

	'***************************************************************************
	TabName = "Bob Standard WinCheckedListBoxes"
	DeleteTab colTbxTabs, TabName

	'***************************************************************************
	TabName = "Bob Standard WinListBoxes"
	DeleteTab colTbxTabs, TabName

	'***************************************************************************
	TabName = "Bob Standard WinComboBoxes"
	DeleteTab colTbxTabs, TabName
	
	windows.Visible = visibleState
	windows.AutoHides = autoHidesState

End Sub

Sub DeleteTab (colTbxTabs, TabName)

	On Error Resume Next
	colTbxTabs.Item(TabName).Delete()
	Err.Clear
	On Error Goto 0	

End Sub

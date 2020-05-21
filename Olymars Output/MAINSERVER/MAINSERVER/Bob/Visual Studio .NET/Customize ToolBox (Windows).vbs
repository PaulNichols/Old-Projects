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
WScript.Echo "Visual Studio.NET toolbox was successfully customized!"

Sub DoTheWork (ByVal Response)

	Dim myDTE
	Dim objToolbox
	Dim colTbxTabs
	Dim objTab
	Dim vsWindowKindToolbox
	Dim vsToolBoxItemFormatDotNETComponent
	Dim TabName
	Dim DllPath
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

	Wscript.Echo "Visual Studio.NET toolbox is going to be customized. Do not interact with Visual Studio.NET during this process. This operation can take a few minutes. Please be patient..."

	DllPath = Left(WScript.ScriptFullName, Len(WScript.ScriptFullName) - Len(WScript.ScriptName) ) + "..\Bin\Bob.dll"

	vsWindowKindToolbox = "{B1E99781-AB81-11D0-B683-00AA00A3EE26}" 
	vsToolBoxItemFormatDotNETComponent = 8
	
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
	Set objTab = CreateTab(colTbxTabs, TabName)
	objTab.ToolBoxItems.Add "WinDataGridCustom_spS_Customers", "Bob.Windows.DataGrids.WinDataGridCustom_spS_Customers," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGridCustom_spS_Customers_Full", "Bob.Windows.DataGrids.WinDataGridCustom_spS_Customers_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGridCustom_spS_Customers_SelectDisplay", "Bob.Windows.DataGrids.WinDataGridCustom_spS_Customers_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGridCustom_spS_Job", "Bob.Windows.DataGrids.WinDataGridCustom_spS_Job," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGridCustom_spS_Job_Full", "Bob.Windows.DataGrids.WinDataGridCustom_spS_Job_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGridCustom_spS_Job_SelectDisplay", "Bob.Windows.DataGrids.WinDataGridCustom_spS_Job_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGridCustom_spS_JobPart", "Bob.Windows.DataGrids.WinDataGridCustom_spS_JobPart," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGridCustom_spS_JobPart_Full", "Bob.Windows.DataGrids.WinDataGridCustom_spS_JobPart_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGridCustom_spS_JobPart_SelectDisplay", "Bob.Windows.DataGrids.WinDataGridCustom_spS_JobPart_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGridCustom_spS_JobPartType", "Bob.Windows.DataGrids.WinDataGridCustom_spS_JobPartType," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGridCustom_spS_Title", "Bob.Windows.DataGrids.WinDataGridCustom_spS_Title," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGridCustom_spS_xSearchCustomer", "Bob.Windows.DataGrids.WinDataGridCustom_spS_xSearchCustomer," + DllPath, vsToolBoxItemFormatDotNETComponent

	'***************************************************************************
	TabName = "Bob Custom WinCheckedListBoxes"
	Set objTab = CreateTab(colTbxTabs, TabName)
	objTab.ToolBoxItems.Add "WinCheckedListBoxCustom_spS_Customers", "Bob.Windows.CheckedListBoxes.WinCheckedListBoxCustom_spS_Customers," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBoxCustom_spS_Customers_Full", "Bob.Windows.CheckedListBoxes.WinCheckedListBoxCustom_spS_Customers_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBoxCustom_spS_Customers_SelectDisplay", "Bob.Windows.CheckedListBoxes.WinCheckedListBoxCustom_spS_Customers_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBoxCustom_spS_Job", "Bob.Windows.CheckedListBoxes.WinCheckedListBoxCustom_spS_Job," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBoxCustom_spS_Job_Full", "Bob.Windows.CheckedListBoxes.WinCheckedListBoxCustom_spS_Job_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBoxCustom_spS_Job_SelectDisplay", "Bob.Windows.CheckedListBoxes.WinCheckedListBoxCustom_spS_Job_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBoxCustom_spS_JobPart", "Bob.Windows.CheckedListBoxes.WinCheckedListBoxCustom_spS_JobPart," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBoxCustom_spS_JobPart_Full", "Bob.Windows.CheckedListBoxes.WinCheckedListBoxCustom_spS_JobPart_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBoxCustom_spS_JobPart_SelectDisplay", "Bob.Windows.CheckedListBoxes.WinCheckedListBoxCustom_spS_JobPart_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBoxCustom_spS_JobPartType", "Bob.Windows.CheckedListBoxes.WinCheckedListBoxCustom_spS_JobPartType," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBoxCustom_spS_Title", "Bob.Windows.CheckedListBoxes.WinCheckedListBoxCustom_spS_Title," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBoxCustom_spS_xSearchCustomer", "Bob.Windows.CheckedListBoxes.WinCheckedListBoxCustom_spS_xSearchCustomer," + DllPath, vsToolBoxItemFormatDotNETComponent

	'***************************************************************************
	TabName = "Bob Custom WinListBoxes"
	Set objTab = CreateTab(colTbxTabs, TabName)
	objTab.ToolBoxItems.Add "WinListBoxCustom_spS_Customers", "Bob.Windows.ListBoxes.WinListBoxCustom_spS_Customers," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBoxCustom_spS_Customers_Full", "Bob.Windows.ListBoxes.WinListBoxCustom_spS_Customers_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBoxCustom_spS_Customers_SelectDisplay", "Bob.Windows.ListBoxes.WinListBoxCustom_spS_Customers_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBoxCustom_spS_Job", "Bob.Windows.ListBoxes.WinListBoxCustom_spS_Job," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBoxCustom_spS_Job_Full", "Bob.Windows.ListBoxes.WinListBoxCustom_spS_Job_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBoxCustom_spS_Job_SelectDisplay", "Bob.Windows.ListBoxes.WinListBoxCustom_spS_Job_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBoxCustom_spS_JobPart", "Bob.Windows.ListBoxes.WinListBoxCustom_spS_JobPart," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBoxCustom_spS_JobPart_Full", "Bob.Windows.ListBoxes.WinListBoxCustom_spS_JobPart_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBoxCustom_spS_JobPart_SelectDisplay", "Bob.Windows.ListBoxes.WinListBoxCustom_spS_JobPart_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBoxCustom_spS_JobPartType", "Bob.Windows.ListBoxes.WinListBoxCustom_spS_JobPartType," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBoxCustom_spS_Title", "Bob.Windows.ListBoxes.WinListBoxCustom_spS_Title," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBoxCustom_spS_xSearchCustomer", "Bob.Windows.ListBoxes.WinListBoxCustom_spS_xSearchCustomer," + DllPath, vsToolBoxItemFormatDotNETComponent

	'***************************************************************************
	TabName = "Bob Custom WinComboBoxes"
	Set objTab = CreateTab(colTbxTabs, TabName)
	objTab.ToolBoxItems.Add "WinComboBoxCustom_spS_Customers", "Bob.Windows.ComboBoxes.WinComboBoxCustom_spS_Customers," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBoxCustom_spS_Customers_Full", "Bob.Windows.ComboBoxes.WinComboBoxCustom_spS_Customers_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBoxCustom_spS_Customers_SelectDisplay", "Bob.Windows.ComboBoxes.WinComboBoxCustom_spS_Customers_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBoxCustom_spS_Job", "Bob.Windows.ComboBoxes.WinComboBoxCustom_spS_Job," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBoxCustom_spS_Job_Full", "Bob.Windows.ComboBoxes.WinComboBoxCustom_spS_Job_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBoxCustom_spS_Job_SelectDisplay", "Bob.Windows.ComboBoxes.WinComboBoxCustom_spS_Job_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBoxCustom_spS_JobPart", "Bob.Windows.ComboBoxes.WinComboBoxCustom_spS_JobPart," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBoxCustom_spS_JobPart_Full", "Bob.Windows.ComboBoxes.WinComboBoxCustom_spS_JobPart_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBoxCustom_spS_JobPart_SelectDisplay", "Bob.Windows.ComboBoxes.WinComboBoxCustom_spS_JobPart_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBoxCustom_spS_JobPartType", "Bob.Windows.ComboBoxes.WinComboBoxCustom_spS_JobPartType," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBoxCustom_spS_Title", "Bob.Windows.ComboBoxes.WinComboBoxCustom_spS_Title," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBoxCustom_spS_xSearchCustomer", "Bob.Windows.ComboBoxes.WinComboBoxCustom_spS_xSearchCustomer," + DllPath, vsToolBoxItemFormatDotNETComponent

	'***************************************************************************
	TabName = "Bob Standard WinDataGrids"
	Set objTab = CreateTab(colTbxTabs, TabName)
	objTab.ToolBoxItems.Add "WinDataGrid_Customers", "Bob.Windows.DataGrids.WinDataGrid_Customers," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGrid_Job", "Bob.Windows.DataGrids.WinDataGrid_Job," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGrid_JobPart", "Bob.Windows.DataGrids.WinDataGrid_JobPart," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGrid_JobPartType", "Bob.Windows.DataGrids.WinDataGrid_JobPartType," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGrid_Title", "Bob.Windows.DataGrids.WinDataGrid_Title," + DllPath, vsToolBoxItemFormatDotNETComponent

	'***************************************************************************
	TabName = "Bob Standard WinCheckedListBoxes"
	Set objTab = CreateTab(colTbxTabs, TabName)
	objTab.ToolBoxItems.Add "WinCheckedListBox_Customers", "Bob.Windows.CheckedListBoxes.WinCheckedListBox_Customers," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBox_Job", "Bob.Windows.CheckedListBoxes.WinCheckedListBox_Job," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBox_JobPart", "Bob.Windows.CheckedListBoxes.WinCheckedListBox_JobPart," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBox_JobPartType", "Bob.Windows.CheckedListBoxes.WinCheckedListBox_JobPartType," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBox_Title", "Bob.Windows.CheckedListBoxes.WinCheckedListBox_Title," + DllPath, vsToolBoxItemFormatDotNETComponent

	'***************************************************************************
	TabName = "Bob Standard WinListBoxes"
	Set objTab = CreateTab(colTbxTabs, TabName)
	objTab.ToolBoxItems.Add "WinListBox_Customers", "Bob.Windows.ListBoxes.WinListBox_Customers," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBox_Job", "Bob.Windows.ListBoxes.WinListBox_Job," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBox_JobPart", "Bob.Windows.ListBoxes.WinListBox_JobPart," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBox_JobPartType", "Bob.Windows.ListBoxes.WinListBox_JobPartType," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBox_Title", "Bob.Windows.ListBoxes.WinListBox_Title," + DllPath, vsToolBoxItemFormatDotNETComponent

	'***************************************************************************
	TabName = "Bob Standard WinComboBoxes"
	Set objTab = CreateTab(colTbxTabs, TabName)
	objTab.ToolBoxItems.Add "WinComboBox_Customers", "Bob.Windows.ComboBoxes.WinComboBox_Customers," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBox_Job", "Bob.Windows.ComboBoxes.WinComboBox_Job," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBox_JobPart", "Bob.Windows.ComboBoxes.WinComboBox_JobPart," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBox_JobPartType", "Bob.Windows.ComboBoxes.WinComboBox_JobPartType," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBox_Title", "Bob.Windows.ComboBoxes.WinComboBox_Title," + DllPath, vsToolBoxItemFormatDotNETComponent

	windows.Visible = visibleState
	windows.AutoHides = autoHidesState

End Sub

Function CreateTab (colTbxTabs, ByVal TabName)

	Dim objTab
	
	On Error Resume Next
	colTbxTabs.Item(TabName).Delete()
	Err.Clear
	On Error Goto 0
	
	Set objTab = colTbxTabs.Add(TabName)
	objTab.Activate()
	
	Set CreateTab = objTab
	
End Function

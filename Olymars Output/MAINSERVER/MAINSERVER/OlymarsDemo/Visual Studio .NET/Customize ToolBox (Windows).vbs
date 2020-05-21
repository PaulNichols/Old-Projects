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

	DllPath = Left(WScript.ScriptFullName, Len(WScript.ScriptFullName) - Len(WScript.ScriptName) ) + "..\Bin\OlymarsDemo.dll"

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
	TabName = "OlymarsDemo Custom WinDataGrids"
	Set objTab = CreateTab(colTbxTabs, TabName)
	objTab.ToolBoxItems.Add "WinDataGridCustom_spS_tblCategory", "OlymarsDemo.Windows.DataGrids.WinDataGridCustom_spS_tblCategory," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGridCustom_spS_tblCustomer", "OlymarsDemo.Windows.DataGrids.WinDataGridCustom_spS_tblCustomer," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGridCustom_spS_tblOrder", "OlymarsDemo.Windows.DataGrids.WinDataGridCustom_spS_tblOrder," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGridCustom_spS_tblOrder_Full", "OlymarsDemo.Windows.DataGrids.WinDataGridCustom_spS_tblOrder_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGridCustom_spS_tblOrder_SelectDisplay", "OlymarsDemo.Windows.DataGrids.WinDataGridCustom_spS_tblOrder_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGridCustom_spS_tblOrderItem", "OlymarsDemo.Windows.DataGrids.WinDataGridCustom_spS_tblOrderItem," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGridCustom_spS_tblOrderItem_Full", "OlymarsDemo.Windows.DataGrids.WinDataGridCustom_spS_tblOrderItem_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGridCustom_spS_tblOrderItem_SelectDisplay", "OlymarsDemo.Windows.DataGrids.WinDataGridCustom_spS_tblOrderItem_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGridCustom_spS_tblProduct", "OlymarsDemo.Windows.DataGrids.WinDataGridCustom_spS_tblProduct," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGridCustom_spS_tblProduct_Full", "OlymarsDemo.Windows.DataGrids.WinDataGridCustom_spS_tblProduct_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGridCustom_spS_tblProduct_SelectDisplay", "OlymarsDemo.Windows.DataGrids.WinDataGridCustom_spS_tblProduct_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGridCustom_spS_tblSupplier", "OlymarsDemo.Windows.DataGrids.WinDataGridCustom_spS_tblSupplier," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGridCustom_spS_tblSupplierProduct", "OlymarsDemo.Windows.DataGrids.WinDataGridCustom_spS_tblSupplierProduct," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGridCustom_spS_tblSupplierProduct_Full", "OlymarsDemo.Windows.DataGrids.WinDataGridCustom_spS_tblSupplierProduct_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGridCustom_spS_tblSupplierProduct_SelectDisplay", "OlymarsDemo.Windows.DataGrids.WinDataGridCustom_spS_tblSupplierProduct_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGridCustom_spS_xProductQuantityPerOrder", "OlymarsDemo.Windows.DataGrids.WinDataGridCustom_spS_xProductQuantityPerOrder," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGridCustom_spS_xReadOrderDateAmount", "OlymarsDemo.Windows.DataGrids.WinDataGridCustom_spS_xReadOrderDateAmount," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGridCustom_spS_xReadOrderItems", "OlymarsDemo.Windows.DataGrids.WinDataGridCustom_spS_xReadOrderItems," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGridCustom_spS_xSearchCustomer", "OlymarsDemo.Windows.DataGrids.WinDataGridCustom_spS_xSearchCustomer," + DllPath, vsToolBoxItemFormatDotNETComponent

	'***************************************************************************
	TabName = "OlymarsDemo Custom WinCheckedListBoxes"
	Set objTab = CreateTab(colTbxTabs, TabName)
	objTab.ToolBoxItems.Add "WinCheckedListBoxCustom_spS_tblCategory", "OlymarsDemo.Windows.CheckedListBoxes.WinCheckedListBoxCustom_spS_tblCategory," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBoxCustom_spS_tblCustomer", "OlymarsDemo.Windows.CheckedListBoxes.WinCheckedListBoxCustom_spS_tblCustomer," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBoxCustom_spS_tblOrder", "OlymarsDemo.Windows.CheckedListBoxes.WinCheckedListBoxCustom_spS_tblOrder," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBoxCustom_spS_tblOrder_Full", "OlymarsDemo.Windows.CheckedListBoxes.WinCheckedListBoxCustom_spS_tblOrder_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBoxCustom_spS_tblOrder_SelectDisplay", "OlymarsDemo.Windows.CheckedListBoxes.WinCheckedListBoxCustom_spS_tblOrder_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBoxCustom_spS_tblOrderItem", "OlymarsDemo.Windows.CheckedListBoxes.WinCheckedListBoxCustom_spS_tblOrderItem," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBoxCustom_spS_tblOrderItem_Full", "OlymarsDemo.Windows.CheckedListBoxes.WinCheckedListBoxCustom_spS_tblOrderItem_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBoxCustom_spS_tblOrderItem_SelectDisplay", "OlymarsDemo.Windows.CheckedListBoxes.WinCheckedListBoxCustom_spS_tblOrderItem_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBoxCustom_spS_tblProduct", "OlymarsDemo.Windows.CheckedListBoxes.WinCheckedListBoxCustom_spS_tblProduct," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBoxCustom_spS_tblProduct_Full", "OlymarsDemo.Windows.CheckedListBoxes.WinCheckedListBoxCustom_spS_tblProduct_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBoxCustom_spS_tblProduct_SelectDisplay", "OlymarsDemo.Windows.CheckedListBoxes.WinCheckedListBoxCustom_spS_tblProduct_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBoxCustom_spS_tblSupplier", "OlymarsDemo.Windows.CheckedListBoxes.WinCheckedListBoxCustom_spS_tblSupplier," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBoxCustom_spS_tblSupplierProduct", "OlymarsDemo.Windows.CheckedListBoxes.WinCheckedListBoxCustom_spS_tblSupplierProduct," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBoxCustom_spS_tblSupplierProduct_Full", "OlymarsDemo.Windows.CheckedListBoxes.WinCheckedListBoxCustom_spS_tblSupplierProduct_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBoxCustom_spS_tblSupplierProduct_SelectDisplay", "OlymarsDemo.Windows.CheckedListBoxes.WinCheckedListBoxCustom_spS_tblSupplierProduct_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBoxCustom_spS_xProductQuantityPerOrder", "OlymarsDemo.Windows.CheckedListBoxes.WinCheckedListBoxCustom_spS_xProductQuantityPerOrder," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBoxCustom_spS_xReadOrderDateAmount", "OlymarsDemo.Windows.CheckedListBoxes.WinCheckedListBoxCustom_spS_xReadOrderDateAmount," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBoxCustom_spS_xReadOrderItems", "OlymarsDemo.Windows.CheckedListBoxes.WinCheckedListBoxCustom_spS_xReadOrderItems," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBoxCustom_spS_xSearchCustomer", "OlymarsDemo.Windows.CheckedListBoxes.WinCheckedListBoxCustom_spS_xSearchCustomer," + DllPath, vsToolBoxItemFormatDotNETComponent

	'***************************************************************************
	TabName = "OlymarsDemo Custom WinListBoxes"
	Set objTab = CreateTab(colTbxTabs, TabName)
	objTab.ToolBoxItems.Add "WinListBoxCustom_spS_tblCategory", "OlymarsDemo.Windows.ListBoxes.WinListBoxCustom_spS_tblCategory," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBoxCustom_spS_tblCustomer", "OlymarsDemo.Windows.ListBoxes.WinListBoxCustom_spS_tblCustomer," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBoxCustom_spS_tblOrder", "OlymarsDemo.Windows.ListBoxes.WinListBoxCustom_spS_tblOrder," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBoxCustom_spS_tblOrder_Full", "OlymarsDemo.Windows.ListBoxes.WinListBoxCustom_spS_tblOrder_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBoxCustom_spS_tblOrder_SelectDisplay", "OlymarsDemo.Windows.ListBoxes.WinListBoxCustom_spS_tblOrder_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBoxCustom_spS_tblOrderItem", "OlymarsDemo.Windows.ListBoxes.WinListBoxCustom_spS_tblOrderItem," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBoxCustom_spS_tblOrderItem_Full", "OlymarsDemo.Windows.ListBoxes.WinListBoxCustom_spS_tblOrderItem_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBoxCustom_spS_tblOrderItem_SelectDisplay", "OlymarsDemo.Windows.ListBoxes.WinListBoxCustom_spS_tblOrderItem_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBoxCustom_spS_tblProduct", "OlymarsDemo.Windows.ListBoxes.WinListBoxCustom_spS_tblProduct," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBoxCustom_spS_tblProduct_Full", "OlymarsDemo.Windows.ListBoxes.WinListBoxCustom_spS_tblProduct_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBoxCustom_spS_tblProduct_SelectDisplay", "OlymarsDemo.Windows.ListBoxes.WinListBoxCustom_spS_tblProduct_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBoxCustom_spS_tblSupplier", "OlymarsDemo.Windows.ListBoxes.WinListBoxCustom_spS_tblSupplier," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBoxCustom_spS_tblSupplierProduct", "OlymarsDemo.Windows.ListBoxes.WinListBoxCustom_spS_tblSupplierProduct," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBoxCustom_spS_tblSupplierProduct_Full", "OlymarsDemo.Windows.ListBoxes.WinListBoxCustom_spS_tblSupplierProduct_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBoxCustom_spS_tblSupplierProduct_SelectDisplay", "OlymarsDemo.Windows.ListBoxes.WinListBoxCustom_spS_tblSupplierProduct_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBoxCustom_spS_xProductQuantityPerOrder", "OlymarsDemo.Windows.ListBoxes.WinListBoxCustom_spS_xProductQuantityPerOrder," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBoxCustom_spS_xReadOrderDateAmount", "OlymarsDemo.Windows.ListBoxes.WinListBoxCustom_spS_xReadOrderDateAmount," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBoxCustom_spS_xReadOrderItems", "OlymarsDemo.Windows.ListBoxes.WinListBoxCustom_spS_xReadOrderItems," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBoxCustom_spS_xSearchCustomer", "OlymarsDemo.Windows.ListBoxes.WinListBoxCustom_spS_xSearchCustomer," + DllPath, vsToolBoxItemFormatDotNETComponent

	'***************************************************************************
	TabName = "OlymarsDemo Custom WinComboBoxes"
	Set objTab = CreateTab(colTbxTabs, TabName)
	objTab.ToolBoxItems.Add "WinComboBoxCustom_spS_tblCategory", "OlymarsDemo.Windows.ComboBoxes.WinComboBoxCustom_spS_tblCategory," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBoxCustom_spS_tblCustomer", "OlymarsDemo.Windows.ComboBoxes.WinComboBoxCustom_spS_tblCustomer," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBoxCustom_spS_tblOrder", "OlymarsDemo.Windows.ComboBoxes.WinComboBoxCustom_spS_tblOrder," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBoxCustom_spS_tblOrder_Full", "OlymarsDemo.Windows.ComboBoxes.WinComboBoxCustom_spS_tblOrder_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBoxCustom_spS_tblOrder_SelectDisplay", "OlymarsDemo.Windows.ComboBoxes.WinComboBoxCustom_spS_tblOrder_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBoxCustom_spS_tblOrderItem", "OlymarsDemo.Windows.ComboBoxes.WinComboBoxCustom_spS_tblOrderItem," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBoxCustom_spS_tblOrderItem_Full", "OlymarsDemo.Windows.ComboBoxes.WinComboBoxCustom_spS_tblOrderItem_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBoxCustom_spS_tblOrderItem_SelectDisplay", "OlymarsDemo.Windows.ComboBoxes.WinComboBoxCustom_spS_tblOrderItem_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBoxCustom_spS_tblProduct", "OlymarsDemo.Windows.ComboBoxes.WinComboBoxCustom_spS_tblProduct," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBoxCustom_spS_tblProduct_Full", "OlymarsDemo.Windows.ComboBoxes.WinComboBoxCustom_spS_tblProduct_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBoxCustom_spS_tblProduct_SelectDisplay", "OlymarsDemo.Windows.ComboBoxes.WinComboBoxCustom_spS_tblProduct_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBoxCustom_spS_tblSupplier", "OlymarsDemo.Windows.ComboBoxes.WinComboBoxCustom_spS_tblSupplier," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBoxCustom_spS_tblSupplierProduct", "OlymarsDemo.Windows.ComboBoxes.WinComboBoxCustom_spS_tblSupplierProduct," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBoxCustom_spS_tblSupplierProduct_Full", "OlymarsDemo.Windows.ComboBoxes.WinComboBoxCustom_spS_tblSupplierProduct_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBoxCustom_spS_tblSupplierProduct_SelectDisplay", "OlymarsDemo.Windows.ComboBoxes.WinComboBoxCustom_spS_tblSupplierProduct_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBoxCustom_spS_xProductQuantityPerOrder", "OlymarsDemo.Windows.ComboBoxes.WinComboBoxCustom_spS_xProductQuantityPerOrder," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBoxCustom_spS_xReadOrderDateAmount", "OlymarsDemo.Windows.ComboBoxes.WinComboBoxCustom_spS_xReadOrderDateAmount," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBoxCustom_spS_xReadOrderItems", "OlymarsDemo.Windows.ComboBoxes.WinComboBoxCustom_spS_xReadOrderItems," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBoxCustom_spS_xSearchCustomer", "OlymarsDemo.Windows.ComboBoxes.WinComboBoxCustom_spS_xSearchCustomer," + DllPath, vsToolBoxItemFormatDotNETComponent

	'***************************************************************************
	TabName = "OlymarsDemo Standard WinDataGrids"
	Set objTab = CreateTab(colTbxTabs, TabName)
	objTab.ToolBoxItems.Add "WinDataGrid_tblCategory", "OlymarsDemo.Windows.DataGrids.WinDataGrid_tblCategory," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGrid_tblCustomer", "OlymarsDemo.Windows.DataGrids.WinDataGrid_tblCustomer," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGrid_tblOrder", "OlymarsDemo.Windows.DataGrids.WinDataGrid_tblOrder," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGrid_tblOrderItem", "OlymarsDemo.Windows.DataGrids.WinDataGrid_tblOrderItem," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGrid_tblProduct", "OlymarsDemo.Windows.DataGrids.WinDataGrid_tblProduct," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGrid_tblSupplier", "OlymarsDemo.Windows.DataGrids.WinDataGrid_tblSupplier," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinDataGrid_tblSupplierProduct", "OlymarsDemo.Windows.DataGrids.WinDataGrid_tblSupplierProduct," + DllPath, vsToolBoxItemFormatDotNETComponent

	'***************************************************************************
	TabName = "OlymarsDemo Standard WinCheckedListBoxes"
	Set objTab = CreateTab(colTbxTabs, TabName)
	objTab.ToolBoxItems.Add "WinCheckedListBox_tblCategory", "OlymarsDemo.Windows.CheckedListBoxes.WinCheckedListBox_tblCategory," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBox_tblCustomer", "OlymarsDemo.Windows.CheckedListBoxes.WinCheckedListBox_tblCustomer," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBox_tblOrder", "OlymarsDemo.Windows.CheckedListBoxes.WinCheckedListBox_tblOrder," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBox_tblOrderItem", "OlymarsDemo.Windows.CheckedListBoxes.WinCheckedListBox_tblOrderItem," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBox_tblProduct", "OlymarsDemo.Windows.CheckedListBoxes.WinCheckedListBox_tblProduct," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinCheckedListBox_tblSupplier", "OlymarsDemo.Windows.CheckedListBoxes.WinCheckedListBox_tblSupplier," + DllPath, vsToolBoxItemFormatDotNETComponent

	'***************************************************************************
	TabName = "OlymarsDemo Standard WinListBoxes"
	Set objTab = CreateTab(colTbxTabs, TabName)
	objTab.ToolBoxItems.Add "WinListBox_tblCategory", "OlymarsDemo.Windows.ListBoxes.WinListBox_tblCategory," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBox_tblCustomer", "OlymarsDemo.Windows.ListBoxes.WinListBox_tblCustomer," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBox_tblOrder", "OlymarsDemo.Windows.ListBoxes.WinListBox_tblOrder," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBox_tblOrderItem", "OlymarsDemo.Windows.ListBoxes.WinListBox_tblOrderItem," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBox_tblProduct", "OlymarsDemo.Windows.ListBoxes.WinListBox_tblProduct," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinListBox_tblSupplier", "OlymarsDemo.Windows.ListBoxes.WinListBox_tblSupplier," + DllPath, vsToolBoxItemFormatDotNETComponent

	'***************************************************************************
	TabName = "OlymarsDemo Standard WinComboBoxes"
	Set objTab = CreateTab(colTbxTabs, TabName)
	objTab.ToolBoxItems.Add "WinComboBox_tblCategory", "OlymarsDemo.Windows.ComboBoxes.WinComboBox_tblCategory," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBox_tblCustomer", "OlymarsDemo.Windows.ComboBoxes.WinComboBox_tblCustomer," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBox_tblOrder", "OlymarsDemo.Windows.ComboBoxes.WinComboBox_tblOrder," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBox_tblOrderItem", "OlymarsDemo.Windows.ComboBoxes.WinComboBox_tblOrderItem," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBox_tblProduct", "OlymarsDemo.Windows.ComboBoxes.WinComboBox_tblProduct," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WinComboBox_tblSupplier", "OlymarsDemo.Windows.ComboBoxes.WinComboBox_tblSupplier," + DllPath, vsToolBoxItemFormatDotNETComponent

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

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
	TabName = "OlymarsDemo Custom WebRepeaters"
	Set objTab = CreateTab(colTbxTabs, TabName)
	objTab.ToolBoxItems.Add "WebRepeaterCustom_spS_tblCategory", "OlymarsDemo.Web.Repeaters.WebRepeaterCustom_spS_tblCategory," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebRepeaterCustom_spS_tblCustomer", "OlymarsDemo.Web.Repeaters.WebRepeaterCustom_spS_tblCustomer," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebRepeaterCustom_spS_tblOrder", "OlymarsDemo.Web.Repeaters.WebRepeaterCustom_spS_tblOrder," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebRepeaterCustom_spS_tblOrder_Full", "OlymarsDemo.Web.Repeaters.WebRepeaterCustom_spS_tblOrder_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebRepeaterCustom_spS_tblOrder_SelectDisplay", "OlymarsDemo.Web.Repeaters.WebRepeaterCustom_spS_tblOrder_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebRepeaterCustom_spS_tblOrderItem", "OlymarsDemo.Web.Repeaters.WebRepeaterCustom_spS_tblOrderItem," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebRepeaterCustom_spS_tblOrderItem_Full", "OlymarsDemo.Web.Repeaters.WebRepeaterCustom_spS_tblOrderItem_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebRepeaterCustom_spS_tblOrderItem_SelectDisplay", "OlymarsDemo.Web.Repeaters.WebRepeaterCustom_spS_tblOrderItem_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebRepeaterCustom_spS_tblProduct", "OlymarsDemo.Web.Repeaters.WebRepeaterCustom_spS_tblProduct," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebRepeaterCustom_spS_tblProduct_Full", "OlymarsDemo.Web.Repeaters.WebRepeaterCustom_spS_tblProduct_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebRepeaterCustom_spS_tblProduct_SelectDisplay", "OlymarsDemo.Web.Repeaters.WebRepeaterCustom_spS_tblProduct_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebRepeaterCustom_spS_tblSupplier", "OlymarsDemo.Web.Repeaters.WebRepeaterCustom_spS_tblSupplier," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebRepeaterCustom_spS_tblSupplierProduct", "OlymarsDemo.Web.Repeaters.WebRepeaterCustom_spS_tblSupplierProduct," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebRepeaterCustom_spS_tblSupplierProduct_Full", "OlymarsDemo.Web.Repeaters.WebRepeaterCustom_spS_tblSupplierProduct_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebRepeaterCustom_spS_tblSupplierProduct_SelectDisplay", "OlymarsDemo.Web.Repeaters.WebRepeaterCustom_spS_tblSupplierProduct_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebRepeaterCustom_spS_xProductQuantityPerOrder", "OlymarsDemo.Web.Repeaters.WebRepeaterCustom_spS_xProductQuantityPerOrder," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebRepeaterCustom_spS_xReadOrderDateAmount", "OlymarsDemo.Web.Repeaters.WebRepeaterCustom_spS_xReadOrderDateAmount," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebRepeaterCustom_spS_xReadOrderItems", "OlymarsDemo.Web.Repeaters.WebRepeaterCustom_spS_xReadOrderItems," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebRepeaterCustom_spS_xSearchCustomer", "OlymarsDemo.Web.Repeaters.WebRepeaterCustom_spS_xSearchCustomer," + DllPath, vsToolBoxItemFormatDotNETComponent

	'***************************************************************************
	TabName = "OlymarsDemo Custom WebDataLists"
	Set objTab = CreateTab(colTbxTabs, TabName)
	objTab.ToolBoxItems.Add "WebDataListCustom_spS_tblCategory", "OlymarsDemo.Web.DataLists.WebDataListCustom_spS_tblCategory," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataListCustom_spS_tblCustomer", "OlymarsDemo.Web.DataLists.WebDataListCustom_spS_tblCustomer," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataListCustom_spS_tblOrder", "OlymarsDemo.Web.DataLists.WebDataListCustom_spS_tblOrder," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataListCustom_spS_tblOrder_Full", "OlymarsDemo.Web.DataLists.WebDataListCustom_spS_tblOrder_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataListCustom_spS_tblOrder_SelectDisplay", "OlymarsDemo.Web.DataLists.WebDataListCustom_spS_tblOrder_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataListCustom_spS_tblOrderItem", "OlymarsDemo.Web.DataLists.WebDataListCustom_spS_tblOrderItem," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataListCustom_spS_tblOrderItem_Full", "OlymarsDemo.Web.DataLists.WebDataListCustom_spS_tblOrderItem_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataListCustom_spS_tblOrderItem_SelectDisplay", "OlymarsDemo.Web.DataLists.WebDataListCustom_spS_tblOrderItem_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataListCustom_spS_tblProduct", "OlymarsDemo.Web.DataLists.WebDataListCustom_spS_tblProduct," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataListCustom_spS_tblProduct_Full", "OlymarsDemo.Web.DataLists.WebDataListCustom_spS_tblProduct_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataListCustom_spS_tblProduct_SelectDisplay", "OlymarsDemo.Web.DataLists.WebDataListCustom_spS_tblProduct_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataListCustom_spS_tblSupplier", "OlymarsDemo.Web.DataLists.WebDataListCustom_spS_tblSupplier," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataListCustom_spS_tblSupplierProduct", "OlymarsDemo.Web.DataLists.WebDataListCustom_spS_tblSupplierProduct," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataListCustom_spS_tblSupplierProduct_Full", "OlymarsDemo.Web.DataLists.WebDataListCustom_spS_tblSupplierProduct_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataListCustom_spS_tblSupplierProduct_SelectDisplay", "OlymarsDemo.Web.DataLists.WebDataListCustom_spS_tblSupplierProduct_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataListCustom_spS_xProductQuantityPerOrder", "OlymarsDemo.Web.DataLists.WebDataListCustom_spS_xProductQuantityPerOrder," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataListCustom_spS_xReadOrderDateAmount", "OlymarsDemo.Web.DataLists.WebDataListCustom_spS_xReadOrderDateAmount," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataListCustom_spS_xReadOrderItems", "OlymarsDemo.Web.DataLists.WebDataListCustom_spS_xReadOrderItems," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataListCustom_spS_xSearchCustomer", "OlymarsDemo.Web.DataLists.WebDataListCustom_spS_xSearchCustomer," + DllPath, vsToolBoxItemFormatDotNETComponent

	'***************************************************************************
	TabName = "OlymarsDemo Custom WebDataGrids"
	Set objTab = CreateTab(colTbxTabs, TabName)
	objTab.ToolBoxItems.Add "WebDataGridCustom_spS_tblCategory", "OlymarsDemo.Web.DataGrids.WebDataGridCustom_spS_tblCategory," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataGridCustom_spS_tblCustomer", "OlymarsDemo.Web.DataGrids.WebDataGridCustom_spS_tblCustomer," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataGridCustom_spS_tblOrder", "OlymarsDemo.Web.DataGrids.WebDataGridCustom_spS_tblOrder," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataGridCustom_spS_tblOrder_Full", "OlymarsDemo.Web.DataGrids.WebDataGridCustom_spS_tblOrder_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataGridCustom_spS_tblOrder_SelectDisplay", "OlymarsDemo.Web.DataGrids.WebDataGridCustom_spS_tblOrder_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataGridCustom_spS_tblOrderItem", "OlymarsDemo.Web.DataGrids.WebDataGridCustom_spS_tblOrderItem," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataGridCustom_spS_tblOrderItem_Full", "OlymarsDemo.Web.DataGrids.WebDataGridCustom_spS_tblOrderItem_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataGridCustom_spS_tblOrderItem_SelectDisplay", "OlymarsDemo.Web.DataGrids.WebDataGridCustom_spS_tblOrderItem_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataGridCustom_spS_tblProduct", "OlymarsDemo.Web.DataGrids.WebDataGridCustom_spS_tblProduct," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataGridCustom_spS_tblProduct_Full", "OlymarsDemo.Web.DataGrids.WebDataGridCustom_spS_tblProduct_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataGridCustom_spS_tblProduct_SelectDisplay", "OlymarsDemo.Web.DataGrids.WebDataGridCustom_spS_tblProduct_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataGridCustom_spS_tblSupplier", "OlymarsDemo.Web.DataGrids.WebDataGridCustom_spS_tblSupplier," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataGridCustom_spS_tblSupplierProduct", "OlymarsDemo.Web.DataGrids.WebDataGridCustom_spS_tblSupplierProduct," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataGridCustom_spS_tblSupplierProduct_Full", "OlymarsDemo.Web.DataGrids.WebDataGridCustom_spS_tblSupplierProduct_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataGridCustom_spS_tblSupplierProduct_SelectDisplay", "OlymarsDemo.Web.DataGrids.WebDataGridCustom_spS_tblSupplierProduct_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataGridCustom_spS_xProductQuantityPerOrder", "OlymarsDemo.Web.DataGrids.WebDataGridCustom_spS_xProductQuantityPerOrder," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataGridCustom_spS_xReadOrderDateAmount", "OlymarsDemo.Web.DataGrids.WebDataGridCustom_spS_xReadOrderDateAmount," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataGridCustom_spS_xReadOrderItems", "OlymarsDemo.Web.DataGrids.WebDataGridCustom_spS_xReadOrderItems," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataGridCustom_spS_xSearchCustomer", "OlymarsDemo.Web.DataGrids.WebDataGridCustom_spS_xSearchCustomer," + DllPath, vsToolBoxItemFormatDotNETComponent

	'***************************************************************************
	TabName = "OlymarsDemo Custom WebCheckBoxLists"
	Set objTab = CreateTab(colTbxTabs, TabName)
	objTab.ToolBoxItems.Add "WebCheckBoxListCustom_spS_tblCategory", "OlymarsDemo.Web.CheckBoxLists.WebCheckBoxListCustom_spS_tblCategory," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebCheckBoxListCustom_spS_tblCustomer", "OlymarsDemo.Web.CheckBoxLists.WebCheckBoxListCustom_spS_tblCustomer," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebCheckBoxListCustom_spS_tblOrder", "OlymarsDemo.Web.CheckBoxLists.WebCheckBoxListCustom_spS_tblOrder," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebCheckBoxListCustom_spS_tblOrder_Full", "OlymarsDemo.Web.CheckBoxLists.WebCheckBoxListCustom_spS_tblOrder_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebCheckBoxListCustom_spS_tblOrder_SelectDisplay", "OlymarsDemo.Web.CheckBoxLists.WebCheckBoxListCustom_spS_tblOrder_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebCheckBoxListCustom_spS_tblOrderItem", "OlymarsDemo.Web.CheckBoxLists.WebCheckBoxListCustom_spS_tblOrderItem," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebCheckBoxListCustom_spS_tblOrderItem_Full", "OlymarsDemo.Web.CheckBoxLists.WebCheckBoxListCustom_spS_tblOrderItem_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebCheckBoxListCustom_spS_tblOrderItem_SelectDisplay", "OlymarsDemo.Web.CheckBoxLists.WebCheckBoxListCustom_spS_tblOrderItem_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebCheckBoxListCustom_spS_tblProduct", "OlymarsDemo.Web.CheckBoxLists.WebCheckBoxListCustom_spS_tblProduct," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebCheckBoxListCustom_spS_tblProduct_Full", "OlymarsDemo.Web.CheckBoxLists.WebCheckBoxListCustom_spS_tblProduct_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebCheckBoxListCustom_spS_tblProduct_SelectDisplay", "OlymarsDemo.Web.CheckBoxLists.WebCheckBoxListCustom_spS_tblProduct_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebCheckBoxListCustom_spS_tblSupplier", "OlymarsDemo.Web.CheckBoxLists.WebCheckBoxListCustom_spS_tblSupplier," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebCheckBoxListCustom_spS_tblSupplierProduct", "OlymarsDemo.Web.CheckBoxLists.WebCheckBoxListCustom_spS_tblSupplierProduct," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebCheckBoxListCustom_spS_tblSupplierProduct_Full", "OlymarsDemo.Web.CheckBoxLists.WebCheckBoxListCustom_spS_tblSupplierProduct_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebCheckBoxListCustom_spS_tblSupplierProduct_SelectDisplay", "OlymarsDemo.Web.CheckBoxLists.WebCheckBoxListCustom_spS_tblSupplierProduct_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebCheckBoxListCustom_spS_xProductQuantityPerOrder", "OlymarsDemo.Web.CheckBoxLists.WebCheckBoxListCustom_spS_xProductQuantityPerOrder," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebCheckBoxListCustom_spS_xReadOrderDateAmount", "OlymarsDemo.Web.CheckBoxLists.WebCheckBoxListCustom_spS_xReadOrderDateAmount," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebCheckBoxListCustom_spS_xReadOrderItems", "OlymarsDemo.Web.CheckBoxLists.WebCheckBoxListCustom_spS_xReadOrderItems," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebCheckBoxListCustom_spS_xSearchCustomer", "OlymarsDemo.Web.CheckBoxLists.WebCheckBoxListCustom_spS_xSearchCustomer," + DllPath, vsToolBoxItemFormatDotNETComponent

	'***************************************************************************
	TabName = "OlymarsDemo Custom WebListBoxes"
	Set objTab = CreateTab(colTbxTabs, TabName)
	objTab.ToolBoxItems.Add "WebListBoxCustom_spS_tblCategory", "OlymarsDemo.Web.ListBoxes.WebListBoxCustom_spS_tblCategory," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebListBoxCustom_spS_tblCustomer", "OlymarsDemo.Web.ListBoxes.WebListBoxCustom_spS_tblCustomer," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebListBoxCustom_spS_tblOrder", "OlymarsDemo.Web.ListBoxes.WebListBoxCustom_spS_tblOrder," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebListBoxCustom_spS_tblOrder_Full", "OlymarsDemo.Web.ListBoxes.WebListBoxCustom_spS_tblOrder_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebListBoxCustom_spS_tblOrder_SelectDisplay", "OlymarsDemo.Web.ListBoxes.WebListBoxCustom_spS_tblOrder_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebListBoxCustom_spS_tblOrderItem", "OlymarsDemo.Web.ListBoxes.WebListBoxCustom_spS_tblOrderItem," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebListBoxCustom_spS_tblOrderItem_Full", "OlymarsDemo.Web.ListBoxes.WebListBoxCustom_spS_tblOrderItem_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebListBoxCustom_spS_tblOrderItem_SelectDisplay", "OlymarsDemo.Web.ListBoxes.WebListBoxCustom_spS_tblOrderItem_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebListBoxCustom_spS_tblProduct", "OlymarsDemo.Web.ListBoxes.WebListBoxCustom_spS_tblProduct," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebListBoxCustom_spS_tblProduct_Full", "OlymarsDemo.Web.ListBoxes.WebListBoxCustom_spS_tblProduct_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebListBoxCustom_spS_tblProduct_SelectDisplay", "OlymarsDemo.Web.ListBoxes.WebListBoxCustom_spS_tblProduct_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebListBoxCustom_spS_tblSupplier", "OlymarsDemo.Web.ListBoxes.WebListBoxCustom_spS_tblSupplier," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebListBoxCustom_spS_tblSupplierProduct", "OlymarsDemo.Web.ListBoxes.WebListBoxCustom_spS_tblSupplierProduct," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebListBoxCustom_spS_tblSupplierProduct_Full", "OlymarsDemo.Web.ListBoxes.WebListBoxCustom_spS_tblSupplierProduct_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebListBoxCustom_spS_tblSupplierProduct_SelectDisplay", "OlymarsDemo.Web.ListBoxes.WebListBoxCustom_spS_tblSupplierProduct_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebListBoxCustom_spS_xProductQuantityPerOrder", "OlymarsDemo.Web.ListBoxes.WebListBoxCustom_spS_xProductQuantityPerOrder," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebListBoxCustom_spS_xReadOrderDateAmount", "OlymarsDemo.Web.ListBoxes.WebListBoxCustom_spS_xReadOrderDateAmount," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebListBoxCustom_spS_xReadOrderItems", "OlymarsDemo.Web.ListBoxes.WebListBoxCustom_spS_xReadOrderItems," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebListBoxCustom_spS_xSearchCustomer", "OlymarsDemo.Web.ListBoxes.WebListBoxCustom_spS_xSearchCustomer," + DllPath, vsToolBoxItemFormatDotNETComponent

	'***************************************************************************
	TabName = "OlymarsDemo Custom WebDropDownLists"
	Set objTab = CreateTab(colTbxTabs, TabName)
	objTab.ToolBoxItems.Add "WebDropDownListCustom_spS_tblCategory", "OlymarsDemo.Web.DropDownLists.WebDropDownListCustom_spS_tblCategory," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDropDownListCustom_spS_tblCustomer", "OlymarsDemo.Web.DropDownLists.WebDropDownListCustom_spS_tblCustomer," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDropDownListCustom_spS_tblOrder", "OlymarsDemo.Web.DropDownLists.WebDropDownListCustom_spS_tblOrder," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDropDownListCustom_spS_tblOrder_Full", "OlymarsDemo.Web.DropDownLists.WebDropDownListCustom_spS_tblOrder_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDropDownListCustom_spS_tblOrder_SelectDisplay", "OlymarsDemo.Web.DropDownLists.WebDropDownListCustom_spS_tblOrder_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDropDownListCustom_spS_tblOrderItem", "OlymarsDemo.Web.DropDownLists.WebDropDownListCustom_spS_tblOrderItem," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDropDownListCustom_spS_tblOrderItem_Full", "OlymarsDemo.Web.DropDownLists.WebDropDownListCustom_spS_tblOrderItem_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDropDownListCustom_spS_tblOrderItem_SelectDisplay", "OlymarsDemo.Web.DropDownLists.WebDropDownListCustom_spS_tblOrderItem_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDropDownListCustom_spS_tblProduct", "OlymarsDemo.Web.DropDownLists.WebDropDownListCustom_spS_tblProduct," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDropDownListCustom_spS_tblProduct_Full", "OlymarsDemo.Web.DropDownLists.WebDropDownListCustom_spS_tblProduct_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDropDownListCustom_spS_tblProduct_SelectDisplay", "OlymarsDemo.Web.DropDownLists.WebDropDownListCustom_spS_tblProduct_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDropDownListCustom_spS_tblSupplier", "OlymarsDemo.Web.DropDownLists.WebDropDownListCustom_spS_tblSupplier," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDropDownListCustom_spS_tblSupplierProduct", "OlymarsDemo.Web.DropDownLists.WebDropDownListCustom_spS_tblSupplierProduct," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDropDownListCustom_spS_tblSupplierProduct_Full", "OlymarsDemo.Web.DropDownLists.WebDropDownListCustom_spS_tblSupplierProduct_Full," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDropDownListCustom_spS_tblSupplierProduct_SelectDisplay", "OlymarsDemo.Web.DropDownLists.WebDropDownListCustom_spS_tblSupplierProduct_SelectDisplay," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDropDownListCustom_spS_xProductQuantityPerOrder", "OlymarsDemo.Web.DropDownLists.WebDropDownListCustom_spS_xProductQuantityPerOrder," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDropDownListCustom_spS_xReadOrderDateAmount", "OlymarsDemo.Web.DropDownLists.WebDropDownListCustom_spS_xReadOrderDateAmount," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDropDownListCustom_spS_xReadOrderItems", "OlymarsDemo.Web.DropDownLists.WebDropDownListCustom_spS_xReadOrderItems," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDropDownListCustom_spS_xSearchCustomer", "OlymarsDemo.Web.DropDownLists.WebDropDownListCustom_spS_xSearchCustomer," + DllPath, vsToolBoxItemFormatDotNETComponent

	'***************************************************************************
	TabName = "OlymarsDemo Standard WebRepeaters"
	Set objTab = CreateTab(colTbxTabs, TabName)
	objTab.ToolBoxItems.Add "WebRepeater_tblCategory", "OlymarsDemo.Web.Repeaters.WebRepeater_tblCategory," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebRepeater_tblCustomer", "OlymarsDemo.Web.Repeaters.WebRepeater_tblCustomer," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebRepeater_tblOrder", "OlymarsDemo.Web.Repeaters.WebRepeater_tblOrder," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebRepeater_tblOrderItem", "OlymarsDemo.Web.Repeaters.WebRepeater_tblOrderItem," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebRepeater_tblProduct", "OlymarsDemo.Web.Repeaters.WebRepeater_tblProduct," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebRepeater_tblSupplier", "OlymarsDemo.Web.Repeaters.WebRepeater_tblSupplier," + DllPath, vsToolBoxItemFormatDotNETComponent

	'***************************************************************************
	TabName = "OlymarsDemo Standard WebDataLists"
	Set objTab = CreateTab(colTbxTabs, TabName)
	objTab.ToolBoxItems.Add "WebDataList_tblCategory", "OlymarsDemo.Web.DataLists.WebDataList_tblCategory," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataList_tblCustomer", "OlymarsDemo.Web.DataLists.WebDataList_tblCustomer," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataList_tblOrder", "OlymarsDemo.Web.DataLists.WebDataList_tblOrder," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataList_tblOrderItem", "OlymarsDemo.Web.DataLists.WebDataList_tblOrderItem," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataList_tblProduct", "OlymarsDemo.Web.DataLists.WebDataList_tblProduct," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataList_tblSupplier", "OlymarsDemo.Web.DataLists.WebDataList_tblSupplier," + DllPath, vsToolBoxItemFormatDotNETComponent

	'***************************************************************************
	TabName = "OlymarsDemo Standard WebDataGrids"
	Set objTab = CreateTab(colTbxTabs, TabName)
	objTab.ToolBoxItems.Add "WebDataGrid_tblCategory", "OlymarsDemo.Web.DataGrids.WebDataGrid_tblCategory," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataGrid_tblCustomer", "OlymarsDemo.Web.DataGrids.WebDataGrid_tblCustomer," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataGrid_tblOrder", "OlymarsDemo.Web.DataGrids.WebDataGrid_tblOrder," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataGrid_tblOrderItem", "OlymarsDemo.Web.DataGrids.WebDataGrid_tblOrderItem," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataGrid_tblProduct", "OlymarsDemo.Web.DataGrids.WebDataGrid_tblProduct," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDataGrid_tblSupplier", "OlymarsDemo.Web.DataGrids.WebDataGrid_tblSupplier," + DllPath, vsToolBoxItemFormatDotNETComponent


	'***************************************************************************
	TabName = "OlymarsDemo Standard WebCheckBoxLists"
	Set objTab = CreateTab(colTbxTabs, TabName)
	objTab.ToolBoxItems.Add "WebCheckBoxList_tblCategory", "OlymarsDemo.Web.CheckBoxLists.WebCheckBoxList_tblCategory," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebCheckBoxList_tblCustomer", "OlymarsDemo.Web.CheckBoxLists.WebCheckBoxList_tblCustomer," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebCheckBoxList_tblOrder", "OlymarsDemo.Web.CheckBoxLists.WebCheckBoxList_tblOrder," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebCheckBoxList_tblOrderItem", "OlymarsDemo.Web.CheckBoxLists.WebCheckBoxList_tblOrderItem," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebCheckBoxList_tblProduct", "OlymarsDemo.Web.CheckBoxLists.WebCheckBoxList_tblProduct," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebCheckBoxList_tblSupplier", "OlymarsDemo.Web.CheckBoxLists.WebCheckBoxList_tblSupplier," + DllPath, vsToolBoxItemFormatDotNETComponent

	'***************************************************************************
	TabName = "OlymarsDemo Standard WebListBoxes"
	Set objTab = CreateTab(colTbxTabs, TabName)
	objTab.ToolBoxItems.Add "WebListBox_tblCategory", "OlymarsDemo.Web.ListBoxes.WebListBox_tblCategory," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebListBox_tblCustomer", "OlymarsDemo.Web.ListBoxes.WebListBox_tblCustomer," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebListBox_tblOrder", "OlymarsDemo.Web.ListBoxes.WebListBox_tblOrder," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebListBox_tblOrderItem", "OlymarsDemo.Web.ListBoxes.WebListBox_tblOrderItem," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebListBox_tblProduct", "OlymarsDemo.Web.ListBoxes.WebListBox_tblProduct," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebListBox_tblSupplier", "OlymarsDemo.Web.ListBoxes.WebListBox_tblSupplier," + DllPath, vsToolBoxItemFormatDotNETComponent


	'***************************************************************************
	TabName = "OlymarsDemo Standard WebDropDownLists"
	Set objTab = CreateTab(colTbxTabs, TabName)
	objTab.ToolBoxItems.Add "WebDropDownList_tblCategory", "OlymarsDemo.Web.DropDownLists.WebDropDownList_tblCategory," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDropDownList_tblCustomer", "OlymarsDemo.Web.DropDownLists.WebDropDownList_tblCustomer," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDropDownList_tblOrder", "OlymarsDemo.Web.DropDownLists.WebDropDownList_tblOrder," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDropDownList_tblOrderItem", "OlymarsDemo.Web.DropDownLists.WebDropDownList_tblOrderItem," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDropDownList_tblProduct", "OlymarsDemo.Web.DropDownLists.WebDropDownList_tblProduct," + DllPath, vsToolBoxItemFormatDotNETComponent
	objTab.ToolBoxItems.Add "WebDropDownList_tblSupplier", "OlymarsDemo.Web.DropDownLists.WebDropDownList_tblSupplier," + DllPath, vsToolBoxItemFormatDotNETComponent

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

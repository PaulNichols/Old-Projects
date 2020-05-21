<%@ Register TagPrefix="Repeaters" Namespace="OlymarsDemo.Web.Repeaters" Assembly="OlymarsDemo" %>
<%@ Register TagPrefix="DropDownLists" Namespace="OlymarsDemo.Web.DropDownLists" Assembly="OlymarsDemo" %>
<%@ Page language="c#" Codebehind="WebFormList_tblOrderItem.aspx.cs" AutoEventWireup="false" Inherits="OlymarsDemo.Web.Forms.WebFormList_tblOrderItem" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>tblOrderItem table content management</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript (ECMAScript)" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="StyleSheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="WebFormList_tblOrderItem" method="post" runat="server">
			<table rules="all" style="border-collapse:collapse;" cellSpacing="0" cellPadding="5" border="1">
				<tr class="TableFilter" align="middle">
					<td>
					</td>
					<td>
						<DropDownLists:WebDropDownList_tblOrder id="com_Oit_GuidOrderID" runat="server" AutoPostBack="True" CssClass="Filter">
						</DropDownLists:WebDropDownList_tblOrder>
					</td>
					<td>
						<DropDownLists:WebDropDownList_tblProduct id="com_Oit_GuidProductID" runat="server" AutoPostBack="True" CssClass="Filter">
						</DropDownLists:WebDropDownList_tblProduct>
					</td>
					<td>
					</td>
				</tr>
				<tr class="TableHeader" align="middle">
					<td>
						<asp:HyperLink id="Add" runat="server" CssClass="EditHyperLink" EnableViewState="false" NavigateUrl="WebForm_tblOrderItem.aspx?Action=Add&ReturnToUrl=WebFormList_tblOrderItem.aspx&ReturnToDisplay=Back to the list">
							Add
						</asp:HyperLink>
					</td>
					<td>
						<asp:linkbutton id="Label_Oit_GuidOrderID" runat="server" CssClass="TableHeader" EnableViewState="false">
							Oit_GuidOrderID (update this label in the 'Olymars/ColumnLabel' extended property of the 'Oit_GuidOrderID' column)
						</asp:linkbutton>
					</td>
					<td>
						<asp:linkbutton id="Label_Oit_GuidProductID" runat="server" CssClass="TableHeader" EnableViewState="false">
							Oit_GuidProductID (update this label in the 'Olymars/ColumnLabel' extended property of the 'Oit_GuidProductID' column)
						</asp:linkbutton>
					</td>
					<td>
						<asp:linkbutton id="Label_Oit_LngAmount" runat="server" CssClass="TableHeader" EnableViewState="false">
							Oit_LngAmount (update this label in the 'Olymars/ColumnLabel' extended property of the 'Oit_LngAmount' column)
						</asp:linkbutton>
					</td>
				</tr>
				<Repeaters:WebRepeaterCustom_spS_tblOrderItem_SelectDisplay id="repeater_tblOrderItem_SelectDisplay" runat="server">
					<ItemTemplate>
						<tr class="TableRow" align="center">
							<td>
								<asp:HyperLink id="Edit" runat="server" CssClass="EditHyperLink" EnableViewState="false" NavigateUrl='<%# String.Format("WebForm_tblOrderItem.aspx?Action=Edit&ID={0}&ReturnToUrl=WebFormList_tblOrderItem.aspx&ReturnToDisplay=Back to the list", DataBinder.Eval(Container.DataItem, "Oit_GuidID") ) %>'>
									Edit
								</asp:HyperLink>
								<br>
								<asp:HyperLink id="Delete" runat="server" CssClass="EditHyperLink" EnableViewState="false" NavigateUrl='<%# String.Format("WebForm_tblOrderItem.aspx?Action=Delete&ID={0}&ReturnToUrl=WebFormList_tblOrderItem.aspx&ReturnToDisplay=Back to the list", DataBinder.Eval(Container.DataItem, "Oit_GuidID") ) %>'>
									Delete
								</asp:HyperLink>
							</td>
							<td>
								<%# DataBinder.Eval(Container.DataItem, "Oit_GuidOrderID_Display") %>
							</td>
							<td>
								<%# DataBinder.Eval(Container.DataItem, "Oit_GuidProductID_Display") %>
							</td>
							<td>
								<%# DataBinder.Eval(Container.DataItem, "Oit_LngAmount") %>
							</td>
						</tr>
					</ItemTemplate>
				</Repeaters:WebRepeaterCustom_spS_tblOrderItem_SelectDisplay>
			</table>
		</form>
	</body>
</HTML>

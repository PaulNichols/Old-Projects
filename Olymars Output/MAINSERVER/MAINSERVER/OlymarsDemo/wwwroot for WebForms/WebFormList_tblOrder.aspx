<%@ Register TagPrefix="Repeaters" Namespace="OlymarsDemo.Web.Repeaters" Assembly="OlymarsDemo" %>
<%@ Register TagPrefix="DropDownLists" Namespace="OlymarsDemo.Web.DropDownLists" Assembly="OlymarsDemo" %>
<%@ Page language="c#" Codebehind="WebFormList_tblOrder.aspx.cs" AutoEventWireup="false" Inherits="OlymarsDemo.Web.Forms.WebFormList_tblOrder" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>tblOrder table content management</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript (ECMAScript)" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="StyleSheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="WebFormList_tblOrder" method="post" runat="server">
			<table rules="all" style="border-collapse:collapse;" cellSpacing="0" cellPadding="5" border="1">
				<tr class="TableFilter" align="middle">
					<td>
					</td>
					<td>
					</td>
					<td>
						<DropDownLists:WebDropDownList_tblCustomer id="com_Ord_LngCustomerID" runat="server" AutoPostBack="True" CssClass="Filter">
						</DropDownLists:WebDropDownList_tblCustomer>
					</td>
					<td>
					</td>
				</tr>
				<tr class="TableHeader" align="middle">
					<td>
						<asp:HyperLink id="Add" runat="server" CssClass="EditHyperLink" EnableViewState="false" NavigateUrl="WebForm_tblOrder.aspx?Action=Add&ReturnToUrl=WebFormList_tblOrder.aspx&ReturnToDisplay=Back to the list">
							Add
						</asp:HyperLink>
					</td>
					<td>
						<asp:linkbutton id="Label_Ord_DatOrderedOn" runat="server" CssClass="TableHeader" EnableViewState="false">
							Ord_DatOrderedOn (update this label in the 'Olymars/ColumnLabel' extended property of the 'Ord_DatOrderedOn' column)
						</asp:linkbutton>
					</td>
					<td>
						<asp:linkbutton id="Label_Ord_LngCustomerID" runat="server" CssClass="TableHeader" EnableViewState="false">
							Ord_LngCustomerID (update this label in the 'Olymars/ColumnLabel' extended property of the 'Ord_LngCustomerID' column)
						</asp:linkbutton>
					</td>
					<td>
						<asp:linkbutton id="Label_Ord_CurTotal" runat="server" CssClass="TableHeader" EnableViewState="false">
							Ord_CurTotal (update this label in the 'Olymars/ColumnLabel' extended property of the 'Ord_CurTotal' column)
						</asp:linkbutton>
					</td>
				</tr>
				<Repeaters:WebRepeaterCustom_spS_tblOrder_SelectDisplay id="repeater_tblOrder_SelectDisplay" runat="server">
					<ItemTemplate>
						<tr class="TableRow" align="center">
							<td>
								<asp:HyperLink id="Edit" runat="server" CssClass="EditHyperLink" EnableViewState="false" NavigateUrl='<%# String.Format("WebForm_tblOrder.aspx?Action=Edit&ID={0}&ReturnToUrl=WebFormList_tblOrder.aspx&ReturnToDisplay=Back to the list", DataBinder.Eval(Container.DataItem, "Ord_GuidID") ) %>'>
									Edit
								</asp:HyperLink>
								<br>
								<asp:HyperLink id="Delete" runat="server" CssClass="EditHyperLink" EnableViewState="false" NavigateUrl='<%# String.Format("WebForm_tblOrder.aspx?Action=Delete&ID={0}&ReturnToUrl=WebFormList_tblOrder.aspx&ReturnToDisplay=Back to the list", DataBinder.Eval(Container.DataItem, "Ord_GuidID") ) %>'>
									Delete
								</asp:HyperLink>
							</td>
							<td>
								<%# DataBinder.Eval(Container.DataItem, "Ord_DatOrderedOn") %>
							</td>
							<td>
								<%# DataBinder.Eval(Container.DataItem, "Ord_LngCustomerID_Display") %>
							</td>
							<td>
								<%# DataBinder.Eval(Container.DataItem, "Ord_CurTotal") %>
							</td>
						</tr>
					</ItemTemplate>
				</Repeaters:WebRepeaterCustom_spS_tblOrder_SelectDisplay>
			</table>
		</form>
	</body>
</HTML>

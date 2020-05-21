<%@ Register TagPrefix="Repeaters" Namespace="OlymarsDemo.Web.Repeaters" Assembly="OlymarsDemo" %>
<%@ Register TagPrefix="DropDownLists" Namespace="OlymarsDemo.Web.DropDownLists" Assembly="OlymarsDemo" %>
<%@ Page language="c#" Codebehind="WebFormList_tblProduct.aspx.cs" AutoEventWireup="false" Inherits="OlymarsDemo.Web.Forms.WebFormList_tblProduct" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>tblProduct table content management</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript (ECMAScript)" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="StyleSheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="WebFormList_tblProduct" method="post" runat="server">
			<table rules="all" style="border-collapse:collapse;" cellSpacing="0" cellPadding="5" border="1">
				<tr class="TableFilter" align="middle">
					<td>
					</td>
					<td>
					</td>
					<td>
					</td>
					<td>
						<DropDownLists:WebDropDownList_tblCategory id="com_Pro_LngCategoryID" runat="server" AutoPostBack="True" CssClass="Filter">
						</DropDownLists:WebDropDownList_tblCategory>
					</td>
				</tr>
				<tr class="TableHeader" align="middle">
					<td>
						<asp:HyperLink id="Add" runat="server" CssClass="EditHyperLink" EnableViewState="false" NavigateUrl="WebForm_tblProduct.aspx?Action=Add&ReturnToUrl=WebFormList_tblProduct.aspx&ReturnToDisplay=Back to the list">
							Add
						</asp:HyperLink>
					</td>
					<td>
						<asp:linkbutton id="Label_Pro_StrName" runat="server" CssClass="TableHeader" EnableViewState="false">
							Pro_StrName (update this label in the 'Olymars/ColumnLabel' extended property of the 'Pro_StrName' column)
						</asp:linkbutton>
					</td>
					<td>
						<asp:linkbutton id="Label_Pro_CurPrice" runat="server" CssClass="TableHeader" EnableViewState="false">
							Pro_CurPrice (update this label in the 'Olymars/ColumnLabel' extended property of the 'Pro_CurPrice' column)
						</asp:linkbutton>
					</td>
					<td>
						<asp:linkbutton id="Label_Pro_LngCategoryID" runat="server" CssClass="TableHeader" EnableViewState="false">
							Pro_LngCategoryID (update this label in the 'Olymars/ColumnLabel' extended property of the 'Pro_LngCategoryID' column)
						</asp:linkbutton>
					</td>
				</tr>
				<Repeaters:WebRepeaterCustom_spS_tblProduct_SelectDisplay id="repeater_tblProduct_SelectDisplay" runat="server">
					<ItemTemplate>
						<tr class="TableRow" align="center">
							<td>
								<asp:HyperLink id="Edit" runat="server" CssClass="EditHyperLink" EnableViewState="false" NavigateUrl='<%# String.Format("WebForm_tblProduct.aspx?Action=Edit&ID={0}&ReturnToUrl=WebFormList_tblProduct.aspx&ReturnToDisplay=Back to the list", DataBinder.Eval(Container.DataItem, "Pro_GuidID") ) %>'>
									Edit
								</asp:HyperLink>
								<br>
								<asp:HyperLink id="Delete" runat="server" CssClass="EditHyperLink" EnableViewState="false" NavigateUrl='<%# String.Format("WebForm_tblProduct.aspx?Action=Delete&ID={0}&ReturnToUrl=WebFormList_tblProduct.aspx&ReturnToDisplay=Back to the list", DataBinder.Eval(Container.DataItem, "Pro_GuidID") ) %>'>
									Delete
								</asp:HyperLink>
							</td>
							<td>
								<%# DataBinder.Eval(Container.DataItem, "Pro_StrName") %>
							</td>
							<td>
								<%# DataBinder.Eval(Container.DataItem, "Pro_CurPrice") %>
							</td>
							<td>
								<%# DataBinder.Eval(Container.DataItem, "Pro_LngCategoryID_Display") %>
							</td>
						</tr>
					</ItemTemplate>
				</Repeaters:WebRepeaterCustom_spS_tblProduct_SelectDisplay>
			</table>
		</form>
	</body>
</HTML>

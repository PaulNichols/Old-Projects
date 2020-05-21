<%@ Register TagPrefix="Repeaters" Namespace="OlymarsDemo.Web.Repeaters" Assembly="OlymarsDemo" %>
<%@ Page language="c#" Codebehind="WebFormList_tblCustomer.aspx.cs" AutoEventWireup="false" Inherits="OlymarsDemo.Web.Forms.WebFormList_tblCustomer" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>tblCustomer table content management</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript (ECMAScript)" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="StyleSheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="WebFormList_tblCustomer" method="post" runat="server">
			<table rules="all" style="border-collapse:collapse;" cellSpacing="0" cellPadding="5" border="1">
				</tr>
				<tr class="TableHeader" align="middle">
					<td>
						<asp:HyperLink id="Add" runat="server" CssClass="EditHyperLink" EnableViewState="false" NavigateUrl="WebForm_tblCustomer.aspx?Action=Add&ReturnToUrl=WebFormList_tblCustomer.aspx&ReturnToDisplay=Back to the list">
							Add
						</asp:HyperLink>
					</td>
					<td>
						<asp:linkbutton id="Label_Cus_StrLastName" runat="server" CssClass="TableHeader" EnableViewState="false">
							Surname
						</asp:linkbutton>
					</td>
					<td>
						<asp:linkbutton id="Label_Cus_StrFirstName" runat="server" CssClass="TableHeader" EnableViewState="false">
							Cus_StrFirstName (update this label in the 'Olymars/ColumnLabel' extended property of the 'Cus_StrFirstName' column)
						</asp:linkbutton>
					</td>
					<td>
						<asp:linkbutton id="Label_Cus_StrEmail" runat="server" CssClass="TableHeader" EnableViewState="false">
							Cus_StrEmail (update this label in the 'Olymars/ColumnLabel' extended property of the 'Cus_StrEmail' column)
						</asp:linkbutton>
					</td>
				</tr>
				<Repeaters:WebRepeater_tblCustomer id="repeater_tblCustomer_SelectDisplay" runat="server">
					<ItemTemplate>
						<tr class="TableRow" align="center">
							<td>
								<asp:HyperLink id="Edit" runat="server" CssClass="EditHyperLink" EnableViewState="false" NavigateUrl='<%# String.Format("WebForm_tblCustomer.aspx?Action=Edit&ID={0}&ReturnToUrl=WebFormList_tblCustomer.aspx&ReturnToDisplay=Back to the list", DataBinder.Eval(Container.DataItem, "Cus_LngID") ) %>'>
									Edit
								</asp:HyperLink>
								<br>
								<asp:HyperLink id="Delete" runat="server" CssClass="EditHyperLink" EnableViewState="false" NavigateUrl='<%# String.Format("WebForm_tblCustomer.aspx?Action=Delete&ID={0}&ReturnToUrl=WebFormList_tblCustomer.aspx&ReturnToDisplay=Back to the list", DataBinder.Eval(Container.DataItem, "Cus_LngID") ) %>'>
									Delete
								</asp:HyperLink>
							</td>
							<td>
								<%# DataBinder.Eval(Container.DataItem, "Cus_StrLastName") %>
							</td>
							<td>
								<%# DataBinder.Eval(Container.DataItem, "Cus_StrFirstName") %>
							</td>
							<td>
								<%# DataBinder.Eval(Container.DataItem, "Cus_StrEmail") %>
							</td>
						</tr>
					</ItemTemplate>
				</Repeaters:WebRepeater_tblCustomer>
			</table>
		</form>
	</body>
</HTML>

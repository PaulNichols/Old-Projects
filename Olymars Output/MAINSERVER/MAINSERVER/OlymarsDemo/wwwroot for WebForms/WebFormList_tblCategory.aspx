<%@ Register TagPrefix="Repeaters" Namespace="OlymarsDemo.Web.Repeaters" Assembly="OlymarsDemo" %>
<%@ Page language="c#" Codebehind="WebFormList_tblCategory.aspx.cs" AutoEventWireup="false" Inherits="OlymarsDemo.Web.Forms.WebFormList_tblCategory" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>tblCategory table content management</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript (ECMAScript)" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="StyleSheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="WebFormList_tblCategory" method="post" runat="server">
			<table rules="all" style="border-collapse:collapse;" cellSpacing="0" cellPadding="5" border="1">
				</tr>
				<tr class="TableHeader" align="middle">
					<td>
						<asp:HyperLink id="Add" runat="server" CssClass="EditHyperLink" EnableViewState="false" NavigateUrl="WebForm_tblCategory.aspx?Action=Add&ReturnToUrl=WebFormList_tblCategory.aspx&ReturnToDisplay=Back to the list">
							Add
						</asp:HyperLink>
					</td>
					<td>
						<asp:linkbutton id="Label_Cat_StrName" runat="server" CssClass="TableHeader" EnableViewState="false">
							Cat_StrName (update this label in the 'Olymars/ColumnLabel' extended property of the 'Cat_StrName' column)
						</asp:linkbutton>
					</td>
				</tr>
				<Repeaters:WebRepeater_tblCategory id="repeater_tblCategory_SelectDisplay" runat="server">
					<ItemTemplate>
						<tr class="TableRow" align="center">
							<td>
								<asp:HyperLink id="Edit" runat="server" CssClass="EditHyperLink" EnableViewState="false" NavigateUrl='<%# String.Format("WebForm_tblCategory.aspx?Action=Edit&ID={0}&ReturnToUrl=WebFormList_tblCategory.aspx&ReturnToDisplay=Back to the list", DataBinder.Eval(Container.DataItem, "Cat_LngID") ) %>'>
									Edit
								</asp:HyperLink>
								<br>
								<asp:HyperLink id="Delete" runat="server" CssClass="EditHyperLink" EnableViewState="false" NavigateUrl='<%# String.Format("WebForm_tblCategory.aspx?Action=Delete&ID={0}&ReturnToUrl=WebFormList_tblCategory.aspx&ReturnToDisplay=Back to the list", DataBinder.Eval(Container.DataItem, "Cat_LngID") ) %>'>
									Delete
								</asp:HyperLink>
							</td>
							<td>
								<%# DataBinder.Eval(Container.DataItem, "Cat_StrName") %>
							</td>
						</tr>
					</ItemTemplate>
				</Repeaters:WebRepeater_tblCategory>
			</table>
		</form>
	</body>
</HTML>

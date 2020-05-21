<%@ Page language="c#" Codebehind="WebForm_tblOrder.aspx.cs" AutoEventWireup="false" Inherits="OlymarsDemo.Web.Forms.WebForm_tblOrder" %>
<%@ Register TagPrefix="DropDownLists" Namespace="OlymarsDemo.Web.DropDownLists" Assembly="OlymarsDemo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>tblOrder table content management</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript (ECMAScript)">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="StyleSheet.css" type="text/css" rel="stylesheet">
	</head>
	<body class="FormStyle">
		<asp:panel id="ErrorDisplay" runat="server" visible="false">
			<asp:Label id="lab_Error" runat="server" CssClass="ErrorDisplayStyle"></asp:Label>
		</asp:panel>
		<asp:HyperLink id="ReturnURL" runat="server" CssClass="EditHyperLink" EnableViewState="true" Visible="false" NavigateUrl="" Text="Return">
		</asp:HyperLink>
		<asp:panel id="MainDisplay" runat="server">
			<form id="WebForm_tblOrder" method="post" runat="server">
				<table border="0" cellspacing="10" cellpadding="5">
					<tr>
						<td align="Right" class="MandatoryLabelStyle">
							Ord_DatOrderedOn (update this label in the 'Olymars/ColumnLabel' extended property of the 'Ord_DatOrderedOn' column)
						</td>
						<td>
							<asp:TextBox id="txt_Ord_DatOrderedOn" runat="server" CssClass="MandatoryTextBoxStyle" tabIndex="1" ></asp:TextBox>
						</td>
						<td>
							<asp:Label EnableViewState="False" Visible="False" runat="Server" id="labError_Ord_DatOrderedOn" CssClass="ErrorStyle"></asp:Label>
						</td>
					</tr>
					<tr>
						<td align="Right" class="MandatoryLabelStyle">
							Ord_LngCustomerID (update this label in the 'Olymars/ColumnLabel' extended property of the 'Ord_LngCustomerID' column)
						</td>
						<td>
							<DropDownLists:WebDropDownList_tblCustomer id="com_Ord_LngCustomerID" runat="server" CssClass="MandatoryComboBoxStyle" tabIndex="2">
							</DropDownLists:WebDropDownList_tblCustomer>
						</td>
					</tr>
					<tr>
						<td align="Right" class="OptionalLabelStyle">
							Ord_CurTotal (update this label in the 'Olymars/ColumnLabel' extended property of the 'Ord_CurTotal' column)
						</td>
						<td>
							<asp:TextBox id="txt_Ord_CurTotal" runat="server" CssClass="OptionalTextBoxStyle" tabIndex="3" ></asp:TextBox>
						</td>
						<td>
							<asp:Label EnableViewState="False" Visible="False" runat="Server" id="labError_Ord_CurTotal" CssClass="ErrorStyle"></asp:Label>
						</td>
					</tr>
					<tr>
					</tr>
					<tr>
						<td>
						</td>
						<td align="center">
							<table border="0" cellspacing="10" cellpadding="5">
								<tr align="center">
									<td>
										<asp:Button id="cmdRefresh" runat="server" Text="Refresh" CssClass="ButtonStyle" tabIndex="4"></asp:Button>
									</td>
									<td>
										<asp:Button id="cmdDelete" runat="server" Text="Delete" CssClass="ButtonStyle" tabIndex="5"></asp:Button>
									</td>
									<td>
										<asp:Button id="cmdUpdate" runat="server" Text="Update" CssClass="ButtonStyle" tabIndex="6"></asp:Button>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</form>
		</asp:panel>
	</body>
</html>

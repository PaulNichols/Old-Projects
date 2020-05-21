<%@ Page language="c#" Codebehind="WebForm_tblOrderItem.aspx.cs" AutoEventWireup="false" Inherits="OlymarsDemo.Web.Forms.WebForm_tblOrderItem" %>
<%@ Register TagPrefix="DropDownLists" Namespace="OlymarsDemo.Web.DropDownLists" Assembly="OlymarsDemo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>tblOrderItem table content management</title>
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
			<form id="WebForm_tblOrderItem" method="post" runat="server">
				<table border="0" cellspacing="10" cellpadding="5">
					<tr>
						<td align="Right" class="MandatoryLabelStyle">
							Oit_GuidOrderID (update this label in the 'Olymars/ColumnLabel' extended property of the 'Oit_GuidOrderID' column)
						</td>
						<td>
							<DropDownLists:WebDropDownList_tblOrder id="com_Oit_GuidOrderID" runat="server" CssClass="MandatoryComboBoxStyle" tabIndex="1">
							</DropDownLists:WebDropDownList_tblOrder>
						</td>
					</tr>
					<tr>
						<td align="Right" class="MandatoryLabelStyle">
							Oit_GuidProductID (update this label in the 'Olymars/ColumnLabel' extended property of the 'Oit_GuidProductID' column)
						</td>
						<td>
							<DropDownLists:WebDropDownList_tblProduct id="com_Oit_GuidProductID" runat="server" CssClass="MandatoryComboBoxStyle" tabIndex="2">
							</DropDownLists:WebDropDownList_tblProduct>
						</td>
					</tr>
					<tr>
						<td align="Right" class="MandatoryLabelStyle">
							Oit_LngAmount (update this label in the 'Olymars/ColumnLabel' extended property of the 'Oit_LngAmount' column)
						</td>
						<td>
							<asp:TextBox id="txt_Oit_LngAmount" runat="server" CssClass="MandatoryTextBoxStyle" tabIndex="3" ></asp:TextBox>
						</td>
						<td>
							<asp:Label EnableViewState="False" Visible="False" runat="Server" id="labError_Oit_LngAmount" CssClass="ErrorStyle"></asp:Label>
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

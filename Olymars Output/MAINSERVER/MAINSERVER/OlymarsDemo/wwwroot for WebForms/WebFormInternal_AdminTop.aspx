<%@ Page language="c#" Codebehind="AdminTop.aspx.cs" AutoEventWireup="false" Inherits="OlymarsDemo.Web.Forms.WebFormInternal_AdminTop" %>
<%@ Register TagPrefix="DropDownLists" Namespace="OlymarsDemo.Web.DropDownLists" Assembly="OlymarsDemo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript (ECMAScript)" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script id="clientEventHandlersJS" language="javascript">
<!--
			function window_onload()
			{
				parent.frames("main").location.replace("<%=FrameEditURL%>");
			}

			function cmdAddNewRecord_onclick()
			{
				AdminTop.<%=ComboBoxName%>.selectedIndex = 0;
				parent.frames("main").location.replace("<%=FrameAddURL%>");
			}
//-->
		</script>
		<link href="StyleSheet.css" type="text/css" rel="stylesheet">
	</head>
	<body ms_positioning="GridLayout" language="javascript" onload="return window_onload();" class="AdminTopFormStyle">
		<form id="AdminTop" method="post" runat="server">
			<table>
				<tr>
					<td class="TablesLabelStyle">
						Tables:
					</td>
					<td class="RecordsLabelStyle">
						Records:
					</td>
				</tr>
				<tr>
					<td>
						<asp:dropdownlist id="comDatabaseTablesList" style="Z-INDEX: 100" runat="server" CssClass="TablesDropDownStyle" AutoPostBack="True">
							<asp:ListItem Value="tblCategory" Selected="True">tblCategory</asp:ListItem>
							<asp:ListItem Value="tblCustomer">tblCustomer</asp:ListItem>
							<asp:ListItem Value="tblOrder">tblOrder</asp:ListItem>
							<asp:ListItem Value="tblOrderItem">tblOrderItem</asp:ListItem>
							<asp:ListItem Value="tblProduct">tblProduct</asp:ListItem>
							<asp:ListItem Value="tblSupplier">tblSupplier</asp:ListItem>
						</asp:dropdownlist>
					</td>
					<td>
						<DropDownLists:WebDropDownList_tblCategory id="com_tblCategory" style="Z-INDEX: 0" runat="server" CssClass="RecordsDropDownStyle" Visible="False" AutoPostBack="True">
						</DropDownLists:WebDropDownList_tblCategory>
						<DropDownLists:WebDropDownList_tblCustomer id="com_tblCustomer" style="Z-INDEX: 0" runat="server" CssClass="RecordsDropDownStyle" Visible="False" AutoPostBack="True">
						</DropDownLists:WebDropDownList_tblCustomer>
						<DropDownLists:WebDropDownList_tblOrder id="com_tblOrder" style="Z-INDEX: 0" runat="server" CssClass="RecordsDropDownStyle" Visible="False" AutoPostBack="True">
						</DropDownLists:WebDropDownList_tblOrder>
						<DropDownLists:WebDropDownList_tblOrderItem id="com_tblOrderItem" style="Z-INDEX: 0" runat="server" CssClass="RecordsDropDownStyle" Visible="False" AutoPostBack="True">
						</DropDownLists:WebDropDownList_tblOrderItem>
						<DropDownLists:WebDropDownList_tblProduct id="com_tblProduct" style="Z-INDEX: 0" runat="server" CssClass="RecordsDropDownStyle" Visible="False" AutoPostBack="True">
						</DropDownLists:WebDropDownList_tblProduct>
						<DropDownLists:WebDropDownList_tblSupplier id="com_tblSupplier" style="Z-INDEX: 0" runat="server" CssClass="RecordsDropDownStyle" Visible="False" AutoPostBack="True">
						</DropDownLists:WebDropDownList_tblSupplier>
					</td>
					<td>
						<input id="cmdAddNewRecord" type="button" value="Add a new record" language="javascript" onclick="return cmdAddNewRecord_onclick()" class="AddNewRecordButtonStyle">
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>

<%@ Control CodeBehind="DesktopModuleTitle.ascx.vb" language="vb" AutoEventWireup="false" Inherits="ASPNET.StarterKit.Portal.DesktopModuleTitle" %>
<LINK href="ASPNETPortal.css" type="text/css" rel="stylesheet">
	<%--

   The PortalModuleTitle User Control is responsible for displaying the title of each
   portal module within the portal -- as well as optionally the module's "Edit Page"
   (if such a page has been configured).

--%>
	<table width="98%" cellspacing="0" cellpadding="0" id="Table1">
		<tr>
			<td align="left">
				<asp:label id="ModuleTitle" cssclass="Head" EnableViewState="false" runat="server" />
			</td>
			<td align="right">
				<asp:hyperlink id="butPrint" runat="server" visible="False" cssclass="printbutton" NavigateUrl="javascript:Print()">Print</asp:hyperlink><br>
				<asp:linkbutton id="butPrintPreview" cssclass="printbutton" visible="False" runat="server">Close Preview</asp:linkbutton>
				<asp:hyperlink id="EditButton" cssclass="CommandButton" EnableViewState="false" runat="server" />
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<hr noshade size="1">
			</td>
		</tr>
	</table>
	<asp:Label id="lblPrintSpacer" visible="False" cssclass="printbutton" runat="server" Font-Bold="True"
		Font-Size="14pt" Font-Names="Arial" foreColor="#ffffff"> spacer</asp:Label>

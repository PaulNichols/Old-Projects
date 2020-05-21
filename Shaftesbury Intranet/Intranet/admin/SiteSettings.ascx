<%@ Control Inherits="ASPNET.StarterKit.Portal.SiteSettings" CodeBehind="SiteSettings.ascx.vb" language="vb" AutoEventWireup="false" targetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="~/DesktopModuleTitle.ascx"%>
<LINK href="../ASPNETPortal.css" type="text/css" rel="stylesheet">
<ASPNETPortal:title runat="server" id="Title1" />
<table cellpadding="2" cellspacing="0" border="0" id="Table1">
	<tr>
		<td width="100" class="Normal">
			Site Title:
		</td>
		<td colspan="2" class="NormalTextBox">
			<asp:Textbox id="siteName" width="240" runat="server" />
		</td>
	</tr>
	<tr>
		<td class="Normal">
			Always show edit button?:
		</td>
		<td colspan="2" class="Normal">
			<asp:CheckBox id="showEdit" runat="server" />
		</td>
	</tr>
	<tr>
		<td>
			&nbsp;
		</td>
		<td colspan="2">
			<asp:LinkButton id="applyBtn" class="CommandButton" Text="Apply Changes" runat="server" />
		</td>
	</tr>
</table>

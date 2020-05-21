<%@ Page validateRequest="false"   language="vb" CodeBehind="EditHtml.aspx.vb" AutoEventWireup="false" Inherits="ASPNET.StarterKit.Portal.EditHtml" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Banner" Src="~/DesktopPortalBanner.ascx" %>
<%@ Import Namespace="ASPNET.StarterKit.Portal" %>
<HTML>
	<HEAD>
		<LINK href="../ASPNETPortal.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body leftmargin="0" bottommargin="0" rightmargin="0" topmargin="0" marginheight="0" marginwidth="0">
		<form runat="server" id="Form1">
			<table width="100%" cellspacing="0" cellpadding="0" border="0" id="Table1">
				<tr valign="top">
					<td colspan="2">
						<aspnetportal:banner id="SiteHeader" runat="server" />
					</td>
				</tr>
				<tr>
					<td>
						<br>
						<table width="98%" cellspacing="0" cellpadding="4" border="0" id="Table2">
							<tr valign="top">
								<td width="100">
									&nbsp;
								</td>
								<td width="*">
									<table width="750" cellspacing="0" cellpadding="0" id="Table3">
										<tr>
											<td align="left" class="Head">
												Html Settings
											</td>
										</tr>
										<tr>
											<td colspan="2">
												<hr noshade size="1">
											</td>
										</tr>
									</table>
									<table width="720" cellspacing="0" cellpadding="0" id="Table4">
										<tr valign="top">
											<td class="SubHead">
												Desktop Html Content:
											</td>
											<td>
												&nbsp;&nbsp;
											</td>
											<td>
												<asp:textbox id="DesktopText" columns="75" width="650" rows="12" textmode="multiline" runat="server" />
											</td>
										</tr>
									</table>
									<p>
										<asp:linkbutton id="updateButton" text="Update" runat="server" class="CommandButton" borderstyle="none" />
										&nbsp;
										<asp:linkbutton id="cancelButton" text="Cancel" causesvalidation="False" runat="server" class="CommandButton"
											borderstyle="none" />
										&nbsp;
									</p>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

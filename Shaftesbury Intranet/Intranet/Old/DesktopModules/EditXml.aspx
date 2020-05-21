<%@ Page language="vb" CodeBehind="EditXml.aspx.vb" AutoEventWireup="false" Inherits="ASPNET.StarterKit.Portal.EditXml" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Banner" Src="~/DesktopPortalBanner.ascx" %>
<%@ Import Namespace="ASPNET.StarterKit.Portal" %>
<html>
	<head>
		<link rel="stylesheet" href='<%= Global.GetApplicationPath(Request) & "/ASPNETPortal.css" %>' type="text/css">
	</head>
	<body leftmargin="0" bottommargin="0" rightmargin="0" topmargin="0" marginheight="0" marginwidth="0">
		<form runat="server">
			<table width="100%" cellspacing="0" cellpadding="0" border="0">
				<tr valign="top">
					<td colspan="2">
						<aspnetportal:banner id="SiteHeader" runat="server" />
					</td>
				</tr>
				<tr>
					<td>
						<br>
						<table width="98%" cellspacing="0" cellpadding="4" border="0">
							<tr valign="top">
								<td width="150">
									&nbsp;
								</td>
								<td width="*">
									<table width="500" cellspacing="0" cellpadding="0">
										<tr>
											<td align="left" class="Head">
												XML Settings
											</td>
										</tr>
										<tr>
											<td colspan="2">
												<hr noshade size="1">
											</td>
										</tr>
									</table>
									<table width="500" cellspacing="0" cellpadding="0">
										<tr valign="top">
											<td width="100" class="SubHead">
												XML Data File:
											</td>
											<td>
												&nbsp;
											</td>
											<td align="right">
												<asp:textbox id="XmlDataSrc" cssclass="NormalTextBox" columns="26" width="340" runat="server" />
											</td>
										</tr>
										<tr valign="top">
											<td class="SubHead">
												XSL/T Transform File:
											</td>
											<td>
												&nbsp;
											</td>
											<td align="right">
												<asp:textbox id="XslTransformSrc" cssclass="NormalTextBox" columns="26" width="340" runat="server" />
											</td>
										</tr>
									</table>
									<p>
										<asp:linkbutton id="updateButton" text="Update" runat="server" class="CommandButton" borderstyle="none" />
										&nbsp;
										<asp:linkbutton id="cancelButton" text="Cancel" causesvalidation="False" runat="server" class="CommandButton" borderstyle="none" />
									</p>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>

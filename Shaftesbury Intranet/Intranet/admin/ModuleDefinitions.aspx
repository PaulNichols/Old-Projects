<%@ Import Namespace="ASPNET.StarterKit.Portal" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Banner" Src="~/DesktopPortalBanner.ascx" %>
<%@ Page language="vb" CodeBehind="ModuleDefinitions.aspx.vb" AutoEventWireup="false" Inherits="ASPNET.StarterKit.Portal.ModuleDefinitions" %>
<HTML>
	<HEAD>
		<%--
    The SecurityRoles.aspx page is used to create and edit security roles within
    the Portal application.
--%>
		<link rel="stylesheet" href='<%= Global.GetApplicationPath(Request) &amp; "/ASPNETPortal.css" %>' type="text/css">
			<LINK href="../ASPNETPortal.css" type="text/css" rel="stylesheet">
	</HEAD>
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
												Module Type Definition
											</td>
										</tr>
										<tr>
											<td colspan="2">
												<hr noshade size="1">
											</td>
										</tr>
									</table>
									<table width="750" cellspacing="0" cellpadding="0" border="0">
										<tr>
											<td width="100" class="SubHead">
												Friendly Name:
											</td>
											<td rowspan="5">
												&nbsp;
											</td>
											<td>
												<asp:textbox id="FriendlyName" cssclass="NormalTextBox" width="390" columns="30" maxlength="150"
													runat="server" />
											</td>
											<td width="25" rowspan="5">
												&nbsp;
											</td>
											<td class="Normal" width="250">
												<asp:requiredfieldvalidator id="Req1" display="Static" errormessage="Enter a Module Name" controltovalidate="FriendlyName"
													runat="server" />
											</td>
										</tr>
										<tr>
											<td class="SubHead" nowrap>
												Desktop Source:
											</td>
											<td>
												<asp:textbox id="DesktopSrc" cssclass="NormalTextBox" width="390" columns="30" maxlength="150"
													runat="server" />
											</td>
											<td class="Normal">
												<asp:requiredfieldvalidator id="Req2" display="Static" errormessage="You Must Enter Source Path for the Desktop Module"
													controltovalidate="DesktopSrc" runat="server" />
											</td>
										</tr>
										<tr>
											<td class="SubHead">
												Mobile Source:
											</td>
											<td>
												<asp:textbox id="MobileSrc" cssclass="NormalTextBox" width="390" columns="30" maxlength="150"
													runat="server" />
											</td>
											<td>
												&nbsp;
											</td>
										</tr>
									</table>
									<p>
										<asp:linkbutton id="updateButton" text="Update" runat="server" class="CommandButton" borderstyle="none" />
										&nbsp;
										<asp:linkbutton id="cancelButton" text="Cancel" causesvalidation="False" runat="server" class="CommandButton"
											borderstyle="none" />
										&nbsp;
										<asp:linkbutton id="deleteButton" text="Delete this module type" causesvalidation="False" runat="server"
											class="CommandButton" borderstyle="none" />
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

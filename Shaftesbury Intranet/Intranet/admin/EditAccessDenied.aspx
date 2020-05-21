<%@ Import Namespace="ASPNET.StarterKit.Portal" %>
<%@ OutputCache Duration="36000" VaryByParam="none" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Banner" Src="~/DesktopPortalBanner.ascx" %>
<%@ Page CodeBehind="EditAccessDenied.aspx.vb" language="vb" AutoEventWireup="false" Inherits="ASPNET.StarterKit.Portal.EditAccessDenied" %>
<HTML>
	<HEAD>
		<title>ASP.NET Portal Starter Kit</title>
		<LINK href="../ASPNETPortal.css" type="text/css" rel="stylesheet">
			<LINK href="../ASPNETPortal.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body leftmargin="0" bottommargin="0" rightmargin="0" topmargin="0" marginheight="0" marginwidth="0">
		<form runat="server" id="Form1">
			<table width="100%" cellspacing="0" cellpadding="0" border="0" id="Table1">
				<tr valign="top">
					<td colspan="2">
						<aspnetportal:banner runat="server" id="Banner1" />
					</td>
				</tr>
				<tr>
					<td valign="top">
						<center>
							<br>
							<table width="500" border="0" id="Table2">
								<tr>
									<td class="Normal">
										<br>
										<br>
										<br>
										<br>
										<span class="Head">Edit Access Denied</span>
										<br>
										<br>
										<hr noshade size="1">
										<br>
										Either you are not currently logged in, or you do not have access to modify the 
										current content. Please contact the administrator to obtain edit access for 
										this module.
										<br>
										<br>
										<a href="<%=Global.GetApplicationPath(Request)%>/DesktopDefault.aspx">Home</a>
									</td>
								</tr>
							</table>
						</center>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

<%@ Import Namespace="ASPNET.StarterKit.Portal" %>
<%@ OutputCache Duration="600" VaryByParam="title" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Banner" Src="~/DesktopPortalBanner.ascx" %>
<%@ Page CodeBehind="NotImplemented.aspx.vb" language="vb" AutoEventWireup="false" Inherits="ASPNET.StarterKit.Portal.NotImplemented" %>
<HTML>
	<HEAD>
		<title>ASP.NET Portal Starter Kit: Content Not Implemented</title>
		<%--

   This page is the target for the fictious links in the sample data.

--%>
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
										<P>
											<br>
											<br>
											<br>
											<br>
											<span id="title" class="Head" runat="server">Linked Content Not Provided</span>
											<br>
											<br>
										</P>
										<P>
											<hr noshade size="1">
										<P></P>
										<P>
											<br>
											The link you clicked was provided as a part of the sample data for site</P>
										<P>
											<br>
											<a href="<%=Global.GetApplicationPath(Request)%>/DesktopDefault.aspx">Home</a>
										</P>
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

<%@ Control language="vb" CodeBehind="DocumentUsers.ascx.vb" AutoEventWireup="false" Inherits="ASPNET.StarterKit.Portal.DocumentUsers" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Banner" Src="~/DesktopPortalBanner.ascx" %>
<%@ Import Namespace="ASPNET.StarterKit.Portal" %>
<HTML>
	<HEAD>
		<%--
    The SecurityRoles.aspx page is used to create and edit security roles within
    the Portal application.
--%>
		<LINK href="../ASPNETPortal.css" type="text/css" rel="stylesheet">
			<LINK href="../ASPNETPortal.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body leftmargin="0" bottommargin="0" rightmargin="0" topmargin="0" marginheight="0" marginwidth="0">
		<table width="100%" cellspacing="0" cellpadding="0" border="0" id="Table1">
			<tr valign="top">
				<td colspan="2">
				</td>
			</tr>
			<tr>
				<td>
					<br>
					<table width="98%" cellspacing="0" cellpadding="4" border="0" id="Table2">
						<tr height="*" valign="top">
							<td width="100">
								&nbsp;
							</td>
							<td width="*">
								<table width="450" cellpadding="2" cellspacing="4" border="0" id="Table3">
									<tr>
										<td colspan="2">
											<table width="100%" cellspacing="0" cellpadding="0" id="Table4">
												<tr>
													<td align="left">
														<span id="title" class="Head" runat="server">Document Users</span>&nbsp;
													</td>
												</tr>
												<tr>
													<td>
														<hr noshade size="1">
													</td>
												</tr>
											</table>
											<asp:label id="Message" cssclass="NormalRed" runat="server" />
										</td>
									</tr>
									<tr>
										<td>
											&nbsp;
										</td>
										<td>
											<table width="100%" cellspacing="0" cellpadding="0" id="Table5">
												<tr>
													<td>
														<asp:dropdownlist id="allUsers" datatextfield="FullName" datavaluefield="UserID" runat="server" />
													</td>
													<td>
														<asp:linkbutton id="addExisting" cssclass="CommandButton" text="Link user to Document" runat="server" /><br>
														<asp:linkbutton id="lnkAddAllUsers" cssclass="CommandButton" text="Give all Users permissions to view this Document"
															runat="server" />
													</td>
												</tr>
											</table>
										</td>
									</tr>
									<tr valign="top">
										<td>
											&nbsp;
										</td>
										<td><br>
											The following users have permissions to view this document.<p></p>
											<asp:datalist id="DocumentUsers" repeatcolumns="2" datakeyfield="UserId" runat="server">
												<itemstyle width="225" />
												<itemtemplate>
													&nbsp;&nbsp;
													<asp:imagebutton imageurl="~/images/delete.gif" commandname="delete" alternatetext="Remove this user association"
														runat="server" />
													<asp:Label Text='<%# DataBinder.Eval(Container.DataItem, "FullName") %>' cssclass="Normal" runat="server" />
												</itemtemplate>
											</asp:datalist>
										</td>
									</tr>
									<tr>
										<td colspan="2">
											<hr noshade size="1">
										</td>
									</tr>
									<tr>
										<td colspan="2">
										</td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</body>
</HTML>

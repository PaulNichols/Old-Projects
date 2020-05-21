<%@ Page language="vb" CodeBehind="TabLayout.aspx.vb" AutoEventWireup="false" Inherits="ASPNET.StarterKit.Portal.TabLayout" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Banner" Src="~/DesktopPortalBanner.ascx" %>
<%@ Import Namespace="ASPNET.StarterKit.Portal" %>
<html>
	<head>
		<%--
     The TabLayout.aspx page is used to control the layout settings of an
     individual tab within the portal.
--%>
		<link rel="stylesheet" href='<%= Global.GetApplicationPath(Request) & "/ASPNETPortal.css" %>' type="text/css">
	</head>
	<body leftmargin="0" bottommargin="0" rightmargin="0" topmargin="0" marginheight="0" marginwidth="0">
		<form runat="server">
			<table width="100%" cellspacing="0" cellpadding="0" border="0">
				<tr valign="top">
					<td colspan="2">
						<aspnetportal:banner showtabs="false" runat="server" id="Banner1" />
					</td>
				</tr>
				<tr>
					<td>
						<br>
						<table width="98%" cellspacing="0" cellpadding="4">
							<tr valign="top">
								<td width="150">
									&nbsp;
								</td>
								<td width="*">
									<table border="0" cellpadding="2" cellspacing="1">
										<tr>
											<td colspan="4">
												<table width="100%" cellspacing="0" cellpadding="0">
													<tr>
														<td align="left" class="Head">
															Tab Name and Layout
														</td>
													</tr>
													<tr>
														<td>
															<hr noshade size="1">
														</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td width="100" class="Normal">
												Tab Name:
											</td>
											<td colspan="3">
												<asp:textbox id="tabName" width="300" cssclass="NormalTextBox" runat="server" />
											</td>
										</tr>
										<tr>
											<td class="Normal" nowrap>
												Authorized Roles:
											</td>
											<td colspan="3">
												<asp:checkboxlist id="authRoles" repeatcolumns="2" font-names="Verdana,Arial" font-size="8pt" width="300" runat="server" />
											</td>
										</tr>
										<tr>
											<td>
												&nbsp;
											</td>
											<td colspan="3">
												<hr noshade size="1">
											</td>
										</tr>
										<tr>
											<td class="Normal" nowrap>
												Show to mobile users?:
											</td>
											<td colspan="3">
												<asp:checkbox id="showMobile" font-names="Verdana,Arial" font-size="8pt" runat="server" />
											</td>
										</tr>
										<tr>
											<td class="Normal" nowrap>
												Mobile Tab Name:
											</td>
											<td colspan="3">
												<asp:textbox id="mobileTabName" width="300" cssclass="NormalTextBox" runat="server" />
											</td>
										</tr>
										<tr>
											<td colspan="4">
												<hr noshade size="1">
											</td>
										</tr>
										<tr>
											<td class="Normal">
												Add Module:
											</td>
											<td class="Normal">
												Module Type
											</td>
											<td colspan="2">
												<asp:dropdownlist id="moduleType" datavaluefield="ModuleDefID" datatextfield="FriendlyName" runat="server" />
											</td>
										</tr>
										<tr>
											<td>
												&nbsp;
											</td>
											<td class="Normal">
												Module Name:
											</td>
											<td colspan="2">
												<asp:textbox id="moduleTitle" enableviewstate="false" text="New Module Name" cssclass="NormalTextBox" width="250" runat="server" />
											</td>
										</tr>
										<tr>
											<td>
												&nbsp;
											</td>
											<td colspan="3">
												<asp:linkbutton class="CommandButton" text='<img src="../images/dn.gif" border=0> Add to "Organize Modules" Below' runat="server" id="AddModuleBtn" />
											</td>
										</tr>
										<tr>
											<td>
												&nbsp;
											</td>
											<td colspan="3">
												<hr noshade size="1">
											</td>
										</tr>
										<tr valign="top">
											<td class="Normal">
												Organize Modules:
											</td>
											<td width="120">
												<table border="0" cellspacing="0" cellpadding="2" width="100%">
													<tr>
														<td class="NormalBold">
															&nbsp;Left Mini Pane
														</td>
													</tr>
													<tr valign="top">
														<td>
															<table border="0" cellspacing="2" cellpadding="0">
																<tr valign="top">
																	<td rowspan="2">
																		<asp:ListBox id="leftPane" DataSource="<%# leftList %>" DataTextField="ModuleTitle" DataValueField="ModuleId" width="110" rows="7" runat="server" />
																	</td>
																	<td valign="top" nowrap>
																		<asp:imagebutton imageurl="~/images/up.gif" commandname="up" commandargument="leftPane" alternatetext="Move selected module up in list" runat="server" id="LeftUpBtn" />
																		<br>
																		<asp:imagebutton imageurl="~/images/rt.gif" commandname="right" sourcepane="leftPane" targetpane="contentPane" alternatetext="Move selected module to the content pane" runat="server" id="LeftRightBtn" />
																		<br>
																		<asp:imagebutton imageurl="~/images/dn.gif" commandname="down" commandargument="leftPane" alternatetext="Move selected module down in list" runat="server" id="LeftDownBtn" />
																		&nbsp;&nbsp;
																	</td>
																</tr>
																<tr>
																	<td valign="bottom" nowrap>
																		<asp:imagebutton imageurl="~/images/edit.gif" commandname="edit" commandargument="leftPane" alternatetext="Edit this item" runat="server" id="LeftEditBtn" />
																		<br>
																		<asp:imagebutton imageurl="~/images/delete.gif" commandname="delete" commandargument="leftPane" alternatetext="Delete this item" runat="server" id="LeftDeleteBtn" />
																	</td>
																</tr>
															</table>
														</td>
													</tr>
												</table>
											</td>
											<td width="*">
												<table border="0" cellspacing="0" cellpadding="2" width="100%">
													<tr>
														<td class="NormalBold">
															&nbsp;Content Pane
														</td>
													</tr>
													<tr>
														<td align="middle">
															<table border="0" cellspacing="2" cellpadding="0">
																<tr valign="top">
																	<td rowspan="2">
																		<asp:ListBox id="contentPane" DataSource="<%# contentList %>" DataTextField="ModuleTitle" DataValueField="ModuleId" width="170" rows="7" runat="server" />
																	</td>
																	<td valign="top" nowrap>
																		<asp:imagebutton imageurl="~/images/up.gif" commandname="up" commandargument="contentPane" alternatetext="Move selected module up in list" runat="server" id="ContentUpBtn" />
																		<br>
																		<asp:imagebutton imageurl="~/images/lt.gif" sourcepane="contentPane" targetpane="leftPane" alternatetext="Move selected module to the left pane" runat="server" id="ContentLeftBtn" />
																		<br>
																		<asp:imagebutton imageurl="~/images/rt.gif" sourcepane="contentPane" targetpane="rightPane" alternatetext="Move selected module to the right pane" runat="server" id="ContentRightBtn" />
																		<br>
																		<asp:imagebutton imageurl="~/images/dn.gif" commandname="down" commandargument="contentPane" alternatetext="Move selected module down in list" runat="server" id="ContentDownBtn" />
																		&nbsp;&nbsp;
																	</td>
																</tr>
																<tr>
																	<td valign="bottom" nowrap>
																		<asp:imagebutton imageurl="~/images/edit.gif" commandname="edit" commandargument="contentPane" alternatetext="Edit this item" runat="server" id="ContentEditBtn" />
																		<br>
																		<asp:imagebutton imageurl="~/images/delete.gif" commandname="delete" commandargument="contentPane" alternatetext="Delete this item" runat="server" id="ContentDeleteBtn" />
																	</td>
																</tr>
															</table>
														</td>
													</tr>
												</table>
											</td>
											<td width="120">
												<table border="0" cellspacing="0" cellpadding="2" width="100%">
													<tr>
														<td class="NormalBold">
															&nbsp;Right Mini Pane
														</td>
													</tr>
													<tr>
														<td>
															<table border="0" cellspacing="2" cellpadding="0">
																<tr valign="top">
																	<td rowspan="2">
																		<asp:ListBox id="rightPane" DataSource="<%# rightList %>" DataTextField="ModuleTitle" DataValueField="ModuleId" width="110" rows="7" runat="server" />
																	</td>
																	<td valign="top" nowrap>
																		<asp:imagebutton imageurl="~/images/up.gif" commandname="up" commandargument="rightPane" alternatetext="Move selected module up in list" runat="server" id="RightUpBtn" />
																		<br>
																		<asp:imagebutton imageurl="~/images/lt.gif" sourcepane="rightPane" targetpane="contentPane" alternatetext="Move selected module to the left pane" runat="server" id="RightLeftBtn" />
																		<br>
																		<asp:imagebutton imageurl="~/images/dn.gif" commandname="down" commandargument="rightPane" alternatetext="Move selected module down in list" runat="server" id="RightDownBtn" />
																	</td>
																</tr>
																<tr>
																	<td valign="bottom" nowrap>
																		<asp:imagebutton imageurl="~/images/edit.gif" commandname="edit" commandargument="rightPane" alternatetext="Edit this item" runat="server" id="RightEditBtn" />
																		<br>
																		<asp:imagebutton imageurl="~/images/delete.gif" commandname="delete" commandargument="rightPane" alternatetext="Delete this item" runat="server" id="RightDeleteBtn" />
																	</td>
																</tr>
															</table>
														</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td colspan="4">
												<hr noshade size="1">
											</td>
										</tr>
										<tr>
											<td colspan="4">
												<asp:linkbutton id="applyBtn" class="CommandButton" text="Apply Changes" runat="server" />
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>

<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="~/DesktopModuleTitle.ascx"%>
<%@ Control Inherits="ASPNET.StarterKit.Portal.Tabs" CodeBehind="Tabs.ascx.vb" language="vb" AutoEventWireup="false" %>
<LINK href="../ASPNETPortal.css" type="text/css" rel="stylesheet">
	<P>
		<ASPNETPortal:title runat="server" id="Title1" /></P>
	<table cellpadding="2" cellspacing="0" border="0" id="Table1">
		<tr>
			<td colspan="2">
				<asp:LinkButton id="addBtn" cssclass="CommandButton" Text="Add New Tab" runat="server" />
			</td>
		</tr>
		<tr valign="top">
			<td width="100">
				&nbsp;
			</td>
			<td width="50" class="Normal">
				Tabs:
			</td>
			<td>
				<table cellpadding="0" cellspacing="0" border="0" id="Table2">
					<tr valign="top">
						<td>
							<asp:ListBox id="tabList" width=200 DataSource="<%# portalTabs %>" DataTextField="TabName" DataValueField="TabId" rows=5 runat="server" />
						</td>
						<td>
							&nbsp;
						</td>
						<td>
							<table id="Table3">
								<tr>
									<td>
										<asp:ImageButton id="upBtn" ImageUrl="~/images/up.gif" CommandName="up" AlternateText="Move selected tab up in list"
											runat="server" />
									</td>
								</tr>
								<tr>
									<td>
										<asp:ImageButton id="downBtn" ImageUrl="~/images/dn.gif" CommandName="down" AlternateText="Move selected tab down in list"
											runat="server" />
									</td>
								</tr>
								<tr>
									<td>
										<asp:ImageButton id="editBtn" ImageUrl="~/images/edit.gif" AlternateText="Edit selected tab's properties"
											runat="server" />
									</td>
								</tr>
								<tr>
									<td>
										<asp:ImageButton id="deleteBtn" ImageUrl="~/images/delete.gif" AlternateText="Delete selected tab"
											runat="server" />
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>

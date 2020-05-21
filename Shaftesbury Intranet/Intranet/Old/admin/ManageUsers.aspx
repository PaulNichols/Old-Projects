<%@ Page language="vb" CodeBehind="ManageUsers.aspx.vb" AutoEventWireup="false" Inherits="ASPNET.StarterKit.Portal.ManageUsers" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Banner" Src="~/DesktopPortalBanner.ascx" %>
<%@ Import Namespace="ASPNET.StarterKit.Portal" %>
<HTML>
	<HEAD>
		<%--
    The SecurityRoles.aspx page is used to create and edit security roles within
    the Portal application.
--%>
		<LINK href="../ASPNETPortal.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body leftmargin="0" bottommargin="0" rightmargin="0" topmargin="0" marginheight="0" marginwidth="0">
		<form runat="server" id="Form1">
			<table width="100%" cellspacing="0" cellpadding="0" border="0" id="Table1">
				<tr valign="top">
					<td colspan="2">
						<aspnetportal:banner showtabs="false" runat="server" id="Banner1" />
					</td>
				</tr>
				<tr>
					<td width="100">
						&nbsp;
					</td>
					<td>
						<br>
						<table width="450" cellspacing="0" cellpadding="4" border="0" id="Table2">
							<tr height="*" valign="top">
								<td colspan="2">
									<table width="100%" cellspacing="0" cellpadding="0" id="Table3">
										<tr>
											<td align="left">
												<span id="title" class="Head" runat="server">Manage User</span>
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
								<td class="Normal" width="108">
									User Name:
								</td>
								<td>
									<asp:textbox id="txtusername" width="200" cssclass="NormalTextBox" runat="server" />
								</td>
							</tr>
							<tr>
								<td class="Normal" width="108">
									Full Name:
								</td>
								<td>
									<asp:textbox id="txtFullname" width="200" cssclass="NormalTextBox" runat="server" />
								</td>
							</tr>
							<tr>
								<td class="Normal" width="108">
									Email:
								</td>
								<td>
									<asp:textbox id="Email" width="200" cssclass="NormalTextBox" runat="server" />
								</td>
							</tr>
							<tr>
								<td class="Normal" width="108">
									Password:
								</td>
								<td>
									<asp:Textbox id="Password" width="200" cssclass="NormalTextBox" runat="server" TextMode="Password" />
									<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="Password"
										CssClass="NormalRed" Display="Dynamic"></asp:RequiredFieldValidator>
								</td>
							</tr>
							<tr>
								<td class="Normal" width="108">
									Confirm Password:
								</td>
								<td>
									<asp:Textbox id="ConfirmPassword" width="200" cssclass="NormalTextBox" runat="server" TextMode="Password" />
									<asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="ConfirmPassword"
										CssClass="NormalRed" Display="Dynamic"></asp:RequiredFieldValidator>
									<asp:CompareValidator id="CompareValidator1" runat="server" ErrorMessage="*" ControlToValidate="ConfirmPassword"
										ControlToCompare="Password" CssClass="NormalRed" Display="Dynamic"></asp:CompareValidator>
								</td>
							</tr>
							<tr>
								<td colspan="3">
									<asp:linkbutton Visible="False" text="Apply Name and Password Changes" cssclass="CommandButton"
										runat="server" id="UpdateUserBtn" />
									<br>
									<br>
								</td>
							</tr>
							<tr>
								<td colspan="2">
									<asp:dropdownlist id="allRoles" datatextfield="RoleName" datavaluefield="RoleID" runat="server" />
									&nbsp;<asp:linkbutton id="addExisting" cssclass="CommandButton" text="Add user to this role" runat="server"
										CausesValidation="False">
								Add user to this role</asp:linkbutton>
								</td>
							</tr>
							<tr valign="top">
								<td width="108">
									&nbsp;
								</td>
								<td>
									<asp:datalist id="userRoles" repeatcolumns="2" datakeyfield="RoleId" runat="server">
										<itemstyle width="225" />
										<itemtemplate>
											&nbsp;&nbsp;
											<asp:imagebutton imageurl="~/images/delete.gif" commandname="delete" alternatetext="Remove user from this role"
												runat="server" id="Imagebutton1" />
											<asp:Label Text='<%# DataBinder.Eval(Container.DataItem, "RoleName") %>' cssclass="Normal" runat="server" ID="Label1" />
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
									<asp:linkbutton id="saveBtn" class="CommandButton" text="Save User Changes" runat="server" CausesValidation="False">
								Save User Changes</asp:linkbutton>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

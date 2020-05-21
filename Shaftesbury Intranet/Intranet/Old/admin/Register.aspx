<%@ Import Namespace="ASPNET.StarterKit.Portal" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Banner" Src="~/DesktopPortalBanner.ascx" %>
<%@ Page language="vb" CodeBehind="Register.aspx.vb" AutoEventWireup="false" Inherits="ASPNET.StarterKit.Portal.Register" %>
<HTML>
	<HEAD>
		<%--

   The Register.aspx page is used to enable clients to register a new unique username
   and password with the portal system.  The page contains a single server event
   handler -- RegisterBtn_Click -- that executes in response to the page's Register
   Button being clicked.

   The Register.aspx page uses the UsersDB class to manage the actual account creation.
   Note that the Usernames and passwords are stored within a table in a SQL database.

--%>
		<LINK href="../ASPNETPortal.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body leftmargin="0" bottommargin="0" rightmargin="0" topmargin="0" marginheight="0" marginwidth="0">
		<form runat="server" id="Form1">
			<table width="100%" cellspacing="0" cellpadding="0" id="Table1">
				<tr valign="top">
					<td colspan="2">
						<aspnetportal:banner showtabs="false" runat="server" id="Banner1" />
					</td>
				</tr>
				<tr>
					<td>
						<br>
						<table width="98%" cellspacing="0" cellpadding="4" border="0" id="Table2">
							<tr>
								<td width="150">&nbsp;
								</td>
								<td width="*">
									<table cellpadding="2" cellspacing="1" border="0" id="Table3">
										<tr>
											<td width="450">
												<table width="100%" cellspacing="0" cellpadding="0" id="Table4">
													<tr>
														<td>
															<span class="Head">Create a New Account </span>
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
										<tr valign="top">
											<td class="Normal">
												Name:
												<br>
												<asp:textbox size="25" id="Name" runat="server" />
												&nbsp;
												<asp:requiredfieldvalidator controltovalidate="Name" errormessage="'Name' must not be left blank." runat="server"
													id="RequiredFieldValidator1" />
												<p>
													Email:
													<br>
													<asp:textbox size="25" id="Email" runat="server" />&nbsp;
													<asp:requiredfieldvalidator controltovalidate="Email" errormessage="'User Name' must not be left blank." runat="server"
														id="RequiredFieldValidator2" />
												<p>
													Full Name:
													<br>
													<asp:textbox size="25" id="txtFullName" runat="server" />&nbsp;
													<asp:requiredfieldvalidator controltovalidate="txtfullname" errormessage="'Full Name' must not be left blank." runat="server"
														id="Requiredfieldvalidator5" />
												<p>
													Password:
													<br>
													<asp:textbox size="25" id="Password" textmode="Password" runat="server" />
													&nbsp;
													<asp:requiredfieldvalidator controltovalidate="Password" errormessage="'Password' must not be left blank." runat="server"
														id="RequiredFieldValidator3" />
												<p>
													Confirm Password:
													<br>
													<asp:textbox size="25" id="ConfirmPassword" textmode="Password" runat="server" />
													&nbsp;
													<asp:requiredfieldvalidator controltovalidate="ConfirmPassword" display="Dynamic" errormessage="'Confirm' must not be left blank."
														runat="server" id="RequiredFieldValidator4" />
													<asp:comparevalidator controltovalidate="ConfirmPassword" controltocompare="Password" errormessage="Password fields do not match."
														runat="server" id="CompareValidator1" />
												<p>
													<asp:linkbutton class="CommandButton" text="Register and Sign In Now" runat="server" id="RegisterBtn" />
													<br>
													<br>
												<p>
													<asp:label id="Message" cssclass="NormalRed" runat="server" />
												</p>
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
</HTML>

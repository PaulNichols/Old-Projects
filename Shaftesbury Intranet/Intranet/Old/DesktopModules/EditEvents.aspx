<%@ Page language="vb" CodeBehind="EditEvents.aspx.vb" AutoEventWireup="false" Inherits="ASPNET.StarterKit.Portal.EditEvents" %>
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
									<table width="500" cellspacing="0" cellpadding="0" id="Table3">
										<tr>
											<td align="left" class="Head">
												Event Details
											</td>
										</tr>
										<tr>
											<td colspan="2">
												<hr noshade size="1">
											</td>
										</tr>
									</table>
									<table width="750" cellspacing="0" cellpadding="0" id="Table4">
										<tr valign="top">
											<td width="100" class="SubHead">
												Title:
											</td>
											<td rowspan="4" height="172">
												&nbsp;
											</td>
											<td>
												<asp:textbox id="TitleField" cssclass="NormalTextBox" width="390" columns="30" maxlength="150"
													runat="server" />
											</td>
											<td width="25" rowspan="4" height="172">
												&nbsp;
											</td>
											<td class="Normal" width="250">
												<asp:requiredfieldvalidator display="Static" runat="server" errormessage="You Must Enter a Valid Title" controltovalidate="TitleField"
													id="Requiredfieldvalidator1" />
											</td>
										</tr>
										<tr valign="top">
											<td class="SubHead">
												Description:
											</td>
											<td>
												<asp:textbox id="DescriptionField" textmode="Multiline" width="390" columns="44" rows="6" runat="server" />
											</td>
											<td class="Normal">
												<asp:requiredfieldvalidator display="Static" runat="server" errormessage="You Must Enter a Valid Description"
													controltovalidate="DescriptionField" id="Requiredfieldvalidator2" />
											</td>
										</tr>
										<tr valign="top">
											<td class="SubHead">
												Where/When:
											</td>
											<td>
												<asp:textbox id="WhereWhenField" cssclass="NormalTextBox" width="390" columns="30" maxlength="150"
													runat="server" />
											</td>
											<td class="Normal">
												<asp:requiredfieldvalidator display="Static" runat="server" errormessage="You Must Enter a Valid Time/Location"
													controltovalidate="WhereWhenField" id="Requiredfieldvalidator3" />
											</td>
										</tr>
										<tr valign="top">
											<td class="SubHead" height="24">
												Expires:
											</td>
											<td height="24">
												<P>
													<asp:TextBox id="ExpireField" runat="server" Width="144px" CssClass="normaltextbox"></asp:TextBox></P>
											</td>
											<td class="Normal" height="24">
												<asp:requiredfieldvalidator display="Static" id="RequiredExpireDate" runat="server" errormessage="You Must Enter a Valid Expiration Date"
													controltovalidate="ExpireField" />
												<asp:comparevalidator display="Static" id="VerifyExpireDate" runat="server" operator="DataTypeCheck" controltovalidate="ExpireField"
													type="Date" errormessage="You Must Enter a Valid Expiration Date" />
											</td>
										</tr>
										<tr valign="top">
											<td class="SubHead">
												URL for more:
											</td>
											<td>
											</td>
											<td class="Normal">
												<asp:TextBox id="txtUrl" runat="server" Width="392px" CssClass="normaltextbox"></asp:TextBox>
											</td>
										</tr>
									</table>
									<p>
										<asp:linkbutton id="updateButton" text="Update" runat="server" class="CommandButton" borderstyle="none" />
										&nbsp;
										<asp:linkbutton id="cancelButton" text="Cancel" causesvalidation="False" runat="server" class="CommandButton"
											borderstyle="none" />
										&nbsp;
										<asp:linkbutton id="deleteButton" text="Delete this item" causesvalidation="False" runat="server"
											class="CommandButton" borderstyle="none" />
										<hr noshade size="1" width="500">
										<span class="Normal">Created by
                                            <asp:label id="CreatedBy" runat="server" />
                                            on
                                            <asp:label id="CreatedDate" runat="server" />
                                            <br>
                                        </span>
									<P></P>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

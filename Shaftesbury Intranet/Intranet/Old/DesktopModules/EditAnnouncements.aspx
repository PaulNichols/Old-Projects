<%@ Register TagPrefix="ASPNETPortal" TagName="Banner" Src="~/DesktopPortalBanner.ascx" %>
<%@ Page language="vb" CodeBehind="EditAnnouncements.aspx.vb" AutoEventWireup="false" Inherits="ASPNET.StarterKit.Portal.EditAnnouncements" %>
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
								<td width="150">
									&nbsp;
								</td>
								<td width="*">
									<table width="520" cellspacing="0" cellpadding="0" id="Table3">
										<tr>
											<td align="left" class="Head">
												Announcement Details
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
											<td rowspan="5">
												&nbsp;
											</td>
											<td>
												<asp:textbox id="TitleField" cssclass="NormalTextBox" width="390" columns="30" maxlength="100"
													runat="server" />
											</td>
											<td width="25" rowspan="5">
												&nbsp;
											</td>
											<td class="Normal" width="250">
												<asp:requiredfieldvalidator id="Req1" display="Static" errormessage="You Must Enter a Valid Title" controltovalidate="TitleField"
													runat="server" />
											</td>
										</tr>
										<tr valign="top">
											<td class="SubHead">
												Read More Link:
											</td>
											<td>
												<asp:textbox id="MoreLinkField" cssclass="NormalTextBox" width="390" columns="30" maxlength="100"
													runat="server" />
											</td>
										</tr>
										<tr valign="top">
											<td class="SubHead">
												Description:
											</td>
											<td>
												<asp:textbox id="DescriptionField" width="390" textmode="Multiline" columns="44" rows="6" runat="server" />
											</td>
											<td class="Normal">
												<asp:requiredfieldvalidator id="Req2" display="Static" errormessage="You Must Enter a Valid Description" controltovalidate="DescriptionField"
													runat="server" />
											</td>
										</tr>
										<tr valign="top">
											<td class="SubHead">
												Expires:
											</td>
											<td>
												<asp:TextBox id="ExpireField" Text="" cssclass="NormalTextBox" width="100" Columns="8"
													runat="server"></asp:TextBox>
											</td>
											<td class="Normal">
												<asp:RequiredFieldValidator Display="Static" id="RequiredExpireDate" runat="server" ErrorMessage="You Must Enter a Valid Expiration Date"
													ControlToValidate="ExpireField" />
												<asp:CompareValidator Display="Static" id="VerifyExpireDate" runat="server" Operator="DataTypeCheck" ControlToValidate="ExpireField"
													Type="Date" ErrorMessage="You Must Enter a Valid Expiration Date" />
											</td>
										</tr>
									</table>
									<p>
										<asp:linkbutton id="updateButton" text="Update" runat="server" cssclass="CommandButton" borderstyle="none" />
										&nbsp;
										<asp:linkbutton id="cancelButton" text="Cancel" causesvalidation="False" runat="server" cssclass="CommandButton"
											borderstyle="none" />
										&nbsp;
										<asp:linkbutton id="deleteButton" text="Delete this item" causesvalidation="False" runat="server"
											cssclass="CommandButton" borderstyle="none" />
										<hr noshade size="1" width="520">
										<span class="Normal">Created by
                                            <asp:label id="CreatedBy" runat="server" />
                                            on
                                            <asp:label id="CreatedDate" runat="server" />
                                            <br>
                                        </span>
									<p></p>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

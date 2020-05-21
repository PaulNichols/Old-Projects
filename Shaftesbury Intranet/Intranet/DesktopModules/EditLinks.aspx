<%@ Page language="vb" CodeBehind="EditLinks.aspx.vb" AutoEventWireup="false" Inherits="ASPNET.StarterKit.Portal.EditLinks" %>
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
								<td width="150">
									&nbsp;
								</td>
								<td width="*">
									<table width="500" cellspacing="0" cellpadding="0" id="Table3">
										<tr>
											<td align="left" class="Head">
												Link Details
											</td>
										</tr>
										<tr>
											<td colspan="2">
												<hr noshade size="1">
											</td>
										</tr>
									</table>
									<table width="750" cellspacing="0" cellpadding="0" border="0" id="Table4">
										<tr>
											<td width="100" class="SubHead">
												Title:
											</td>
											<td rowspan="5">
												&nbsp;
											</td>
											<td>
												<asp:textbox id="TitleField" cssclass="NormalTextBox" width="390" columns="30" maxlength="150"
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
										<tr>
											<td class="SubHead">
												Url:
											</td>
											<td>
												<asp:textbox id="UrlField" cssclass="NormalTextBox" width="390" columns="30" maxlength="150"
													runat="server" />
											</td>
											<td class="Normal">
												<asp:requiredfieldvalidator id="Req2" display="Static" runat="server" errormessage="You Must Enter a Valid URL"
													controltovalidate="UrlField" />
											</td>
										</tr>
										<tr>
											<td class="SubHead">
												Description:
											</td>
											<td>
												<asp:textbox id="DescriptionField" cssclass="NormalTextBox" width="390" columns="30" maxlength="150"
													runat="server" />
											</td>
											<td>
												&nbsp;
											</td>
										</tr>
										<tr>
											<td class="SubHead">
												View Order:
											</td>
											<td>
												<asp:textbox id="ViewOrderField" cssclass="NormalTextBox" width="390" columns="30" maxlength="3"
													runat="server" />
											</td>
											<td class="Normal">
												<asp:requiredfieldvalidator display="Static" id="RequiredViewOrder" runat="server" controltovalidate="ViewOrderField"
													errormessage="You Must Enter a Valid View Order" />
												<asp:comparevalidator display="Static" id="VerifyViewOrder" runat="server" operator="DataTypeCheck" controltovalidate="ViewOrderField"
													type="Integer" errormessage="You Must Enter a Valid View Order" />
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
										<hr noshade size="1" width="500">
										<span class="Normal">Created by
                                            <asp:label id="CreatedBy" runat="server" />
                                            on
                                            <asp:label id="CreatedDate" runat="server" />
                                            <br>
                                        </span>
									<p>
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

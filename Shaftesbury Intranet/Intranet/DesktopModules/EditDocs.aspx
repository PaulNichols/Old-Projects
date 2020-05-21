<%@ Register TagPrefix="ASPNETPortal" TagName="Banner" Src="~/DesktopPortalBanner.ascx" %>
<%@ Page language="vb" CodeBehind="EditDocs.aspx.vb" AutoEventWireup="false" Inherits="ASPNET.StarterKit.Portal.EditDocs" %>
<%@ Register TagPrefix="uc1" TagName="DocumentUsers" Src="../admin/DocumentUsers.ascx" %>
<%@ Import Namespace="ASPNET.StarterKit.Portal" %>

<HTML>
  <HEAD>
		<LINK href="../ASPNETPortal.css" type="text/css" rel="stylesheet">
  </HEAD>
	<body leftmargin="0" bottommargin="0" rightmargin="0" topmargin="0" marginheight="0" marginwidth="0">
		<form enctype="multipart/form-data" runat="server" id="Form1">
			<table width="100%" cellspacing="0" cellpadding="0" border="0" id="Table1">
				<tr valign="top">
					<TD></TD>
					<TD></TD>
					<td colspan="2">
						<aspnetportal:banner id="SiteHeader" runat="server" />
					</td>
				</tr>
				<tr>
					<TD></TD>
					<TD></TD>
					<td>
						<br>
						<table width="98%" cellspacing="0" cellpadding="4" border="0" id="Table2">
							<tr valign="top">
								<td width="150">
									&nbsp;
								</td>
								<td>
									<table width="500" cellspacing="0" cellpadding="0" id="Table3">
										<tr>
											<td align="left" class="Head">
												Document Details
											</td>
										</tr>
										<tr>
											<td colspan="2">
												<hr noshade size="1">
											</td>
										</tr>
									</table>
									<table width="726" cellspacing="0" cellpadding="0" border="0" id="Table4">
										<tr valign="top">
											<td width="136" class="SubHead">
												Name:
											</td>
											<td>
												&nbsp;
											</td>
											<td>
												<asp:textbox id="NameField" cssclass="NormalTextBox" width="353" columns="28" maxlength="150"
													runat="server" />
											</td>
											<td width="25" rowspan="6">
												&nbsp;
											</td>
											<td class="Normal" width="250">
												<asp:requiredfieldvalidator display="Static" runat="server" errormessage="You Must Enter a Valid Name" controltovalidate="NameField"
													id="RequiredFieldValidator1" />
											</td>
										</tr>
										<tr valign="top">
											<td class="SubHead" width="136">
												Category:
											</td>
											<td>
												&nbsp;
											</td>
											<td>
												<asp:textbox id="CategoryField" cssclass="NormalTextBox" width="353" columns="28" maxlength="50"
													runat="server" />
											</td>
										</tr>
										<tr valign="top">
											<td width="136" class="SubHead">
												Description:
											</td>
											<td>
												&nbsp;
											</td>
											<td>
												<asp:textbox id="txtDescription" cssclass="NormalTextBox" width="353" columns="28" maxlength="150"
													runat="server" />
											</td>
											<td width="25" rowspan="6">
												&nbsp;
											</td>
											<td class="Normal" width="250">
											</td>
										</tr>
										<tr valign="top">
											<td width="136" class="SubHead">
												URL to Browse:
											</td>
											<td>
												&nbsp;
											</td>
											<td>
												<asp:textbox id="PathField" cssclass="NormalTextBox" width="353" columns="28" maxlength="250"
													runat="server" />
											</td>
										</tr>
										<tr valign="top">
											<td width="136" class="SubHead">
												Template Page:
											</td>
											<td>
												&nbsp;
											</td>
											<td>
												<asp:textbox id="txtTemplatePage" cssclass="NormalTextBox" width="353" columns="28" maxlength="250"
													runat="server" />
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
									<P>&nbsp;</P>
								</td>
							</tr>
						</table>
						<P>
							<TABLE id="Table5" cellSpacing="1" cellPadding="1" width="100%" border="0">
								<TR>
									<TD width="34"></TD>
									<TD>
										<uc1:DocumentUsers id="DocumentUsers" runat="server"></uc1:DocumentUsers></TD>
								</TR>
							</TABLE>
						</P>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

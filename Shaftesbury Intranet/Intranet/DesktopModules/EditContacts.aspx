<%@ Register TagPrefix="uc1" TagName="Contacts" Src="Contacts.ascx" %>
<%@ Register TagPrefix="uc2" TagName="Templates" Src="TemplatesPages/LetterTemplates.ascx" %>
<%@ Page language="vb" CodeBehind="EditContacts.aspx.vb" AutoEventWireup="false" Inherits="ASPNET.StarterKit.Portal.EditContacts" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Banner" Src="~/DesktopPortalBanner.ascx" %>
<%@ Import Namespace="ASPNET.StarterKit.Portal" %>
<HTML>
	<HEAD>
		<LINK href="../ASPNETPortal.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" marginwidth="0" marginheight="0">
		<form id="Form1" runat="server">
			<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr vAlign="top">
					<td colSpan="2"><aspnetportal:banner id="SiteHeader" runat="server"></aspnetportal:banner></td>
				</tr>
				<tr>
					<td><br>
						<table id="Table2" cellSpacing="0" cellPadding="4" width="98%" border="0">
							<tr vAlign="top">
								<td width="150">&nbsp;
								</td>
								<td>
									<table id="Table3" cellSpacing="0" cellPadding="0" width="500" border="0">
										<tr>
											<td class="Head" align="left">
												<P>Contact Details
												</P>
											</td>
										</tr>
									</table>
									<P>
										<TABLE id="Table5" cellSpacing="1" cellPadding="1" width="100%" border="0">
											<TR>
												<TD width="61" bgColor="lightgrey"><asp:linkbutton id="butGenaral" runat="server">General</asp:linkbutton></TD>
												<TD width="70" bgColor="lightgrey"><asp:linkbutton id="butBuiness" runat="server">Business</asp:linkbutton></TD>
												<TD width="232" bgColor="lightgrey"><asp:linkbutton id="butHome" runat="server">Home</asp:linkbutton></TD>
												<TD width="142" bgColor="lightgrey">
													<DIV align="right"><asp:radiobuttonlist id="RadioButtonList1" runat="server" RepeatDirection="Horizontal" CssClass="Normal">
															<asp:ListItem></asp:ListItem>
															<asp:ListItem></asp:ListItem>
														</asp:radiobuttonlist></DIV>
												</TD>
												<TD></TD>
											</TR>
										</TABLE>
									</P>
									<TABLE id="Table7" cellSpacing="1" cellPadding="1" width="100%" border="0">
										<TR>
											<TD><asp:panel id="pnlBasicDetails" runat="server" DESIGNTIMEDRAGDROP="1113" Height="160px" Width="769px">
													<TABLE id="Table9" height="47" cellSpacing="0" cellPadding="0" width="750" border="0">
														<TBODY>
															<TR vAlign="top">
																<TD class="SubHead" width="126">File As:</TD>
																<TD class="SubHead" width="126">
																	<asp:textbox id="txtFileAs" runat="server" Enabled="False" cssclass="NormalTextBox" width="390"
																		columns="30" maxlength="100"></asp:textbox></TD>
																<TD></TD>
																<TD></TD>
															</TR>
															<TR vAlign="top">
																<TD class="SubHead" width="126">Full Name:</TD>
																<TD class="SubHead" width="126">
																	<asp:textbox id="txtFullName" runat="server" Enabled="False" cssclass="NormalTextBox" width="390"
																		columns="30" maxlength="100"></asp:textbox></TD>
																<TD></TD>
																<TD></TD>
															</TR>
															<TR>
																<TD class="SubHead" width="126">Title/Suffix:</TD>
																<TD class="SubHead">
																	<asp:DropDownList id="cboTitle" runat="server" AutoPostBack="True">
																		<asp:ListItem Value="-">-</asp:ListItem>
																		<asp:ListItem Value="Dr.">Dr.</asp:ListItem>
																		<asp:ListItem Value="Miss">Miss.</asp:ListItem>
																		<asp:ListItem Value="Mr.">Mr.</asp:ListItem>
																		<asp:ListItem Value="Mrs.">Mrs.</asp:ListItem>
																		<asp:ListItem Value="Ms.">Ms.</asp:ListItem>
																		<asp:ListItem Value="Prof.">Prof.</asp:ListItem>
																		<asp:ListItem Value="Rev">Rev.</asp:ListItem>
																		<asp:ListItem Value="Lady">Lady</asp:ListItem>
																		<asp:ListItem Value="Lord">Lord</asp:ListItem>
																	</asp:DropDownList>
																	<asp:DropDownList id="cboSuffix" runat="server" AutoPostBack="True">
																		<asp:ListItem Value="-">-</asp:ListItem>
																		<asp:ListItem Value="I">I</asp:ListItem>
																		<asp:ListItem Value="II">II</asp:ListItem>
																		<asp:ListItem Value="III">III</asp:ListItem>
																		<asp:ListItem Value="Jr.">Jr.</asp:ListItem>
																		<asp:ListItem Value="OBE.">OBE.</asp:ListItem>
																		<asp:ListItem Value="MBE.">MBE.</asp:ListItem>
																		<asp:ListItem Value="Rt.Hon">Rt.Hon</asp:ListItem>
																	</asp:DropDownList></TD>
																<TD height="26"></TD>
															<TR vAlign="top">
																<TD class="SubHead" width="126">First Name:</TD>
																<TD class="SubHead" width="126">
																	<asp:textbox id="txtFirstName" runat="server" cssclass="NormalTextBox" width="390" columns="30"
																		maxlength="100" AutoPostBack="True"></asp:textbox></TD>
																<TD>
																	<asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" DESIGNTIMEDRAGDROP="121" display="Static"
																		errormessage="You Must Enter a Valid Name" controltovalidate="txtFirstName"></asp:requiredfieldvalidator></TD>
																<TD></TD>
															</TR>
															<TR vAlign="top">
																<TD class="SubHead" width="126">Surname:</TD>
																<TD class="SubHead" width="126">
																	<asp:textbox id="txtSurname" runat="server" cssclass="NormalTextBox" width="390" columns="30"
																		maxlength="100" AutoPostBack="True"></asp:textbox></TD>
																<TD>
																	<asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" DESIGNTIMEDRAGDROP="121" display="Static"
																		errormessage="You Must Enter a Valid Name" controltovalidate="txtSurname"></asp:requiredfieldvalidator></TD>
																<TD></TD>
															</TR>
															<TR vAlign="top">
																<TD class="SubHead" width="126">Company Name:</TD>
																<TD class="SubHead" width="126">
																	<asp:textbox id="txtCompanyName" runat="server" cssclass="NormalTextBox" width="390" columns="30"
																		maxlength="100"></asp:textbox></TD>
																<TD>
																	<asp:requiredfieldvalidator id="Requiredfieldvalidator5" runat="server" DESIGNTIMEDRAGDROP="121" display="Static"
																		errormessage="You Must Enter a Valid Name" controltovalidate="txtCompanyName"></asp:requiredfieldvalidator></TD>
															</TR>
															<TR vAlign="top">
																<TD class="SubHead" width="126">Role:
																</TD>
																<TD>
																	<asp:textbox id="txtRole" runat="server" cssclass="NormalTextBox" width="390" columns="30" maxlength="100"></asp:textbox></TD>
																<TD></TD>
															</TR>
															<TR vAlign="top">
																<TD class="SubHead" width="126">
																	<asp:HyperLink id="linkBusinessEMail" runat="server">Email:</asp:HyperLink></TD>
																<TD class="SubHead" width="126">
																	<asp:textbox id="txtEmail" runat="server" cssclass="NormalTextBox" width="390" columns="30" maxlength="100"></asp:textbox></TD>
																<TD>
																	<asp:regularexpressionvalidator id="Regularexpressionvalidator2" runat="server" errormessage="Must use a valid email address."
																		controltovalidate="txtEmail" validationexpression="[\w\.-]+(\+[\w-]*)?@([\w-]+\.)+[\w-]+"></asp:regularexpressionvalidator></TD>
																<TD></TD>
															</TR>
															<TR vAlign="top">
																<TD class="SubHead" width="126">Salutation:</TD>
																<TD class="SubHead" width="126">
																	<asp:textbox id="txtSalutation" runat="server" cssclass="NormalTextBox" width="390" columns="30"
																		maxlength="100"></asp:textbox></TD>
																<TD></TD>
																<TD></TD>
															</TR>
												</asp:panel></TD>
										<TR>
											<TD><asp:panel id="pnlBusiness" runat="server" Height="160px" Width="769px">
													<P></P>
													<TABLE id="Table10" height="47" cellSpacing="0" cellPadding="0" width="750" border="0">
														<TBODY>
															<TR vAlign="top">
																<TD class="SubHead" width="126">Mobile:</TD>
																<TD class="SubHead" width="126">
																	<asp:textbox id="txtMobile" runat="server" cssclass="NormalTextBox" width="390" columns="30"
																		maxlength="100"></asp:textbox></TD>
																<TD>
																	<asp:RegularExpressionValidator id="RegularExpressionValidator4" runat="server" DESIGNTIMEDRAGDROP="439" ErrorMessage="Please enter a valid number"
																		ValidationExpression="[0]((\d{2}(?:-|\s*)\d{4}(?:-|\s*)\d{4})|(\d{4}(?:-|\s*)\d{6})|(\d{3}(?:-|\s*)\d{3}(?:-|\s*)\d{4})|\d{10})"
																		ControlToValidate="txtMobile"></asp:RegularExpressionValidator></TD>
																<TD></TD>
															</TR>
															<TR vAlign="top">
																<TD class="SubHead" width="126">Phone:</TD>
																<TD class="SubHead" width="126">
																	<asp:textbox id="txtBusinessPhone" runat="server" cssclass="NormalTextBox" width="390" columns="30"
																		maxlength="100"></asp:textbox></TD>
																<TD>
																	<asp:RegularExpressionValidator id="RegularExpressionValidator5" runat="server" ErrorMessage="Please enter a valid number"
																		ValidationExpression="[0]((\d{2}(?:-|\s*)\d{4}(?:-|\s*)\d{4})|(\d{4}(?:-|\s*)\d{6})|(\d{3}(?:-|\s*)\d{3}(?:-|\s*)\d{4})|\d{10})"
																		ControlToValidate="txtBusinessPhone"></asp:RegularExpressionValidator></TD>
																<TD></TD>
															</TR>
															<TR vAlign="top">
																<TD class="SubHead" width="126">Fax:</TD>
																<TD class="SubHead" width="126">
																	<asp:textbox id="txtBusinessFax" runat="server" cssclass="NormalTextBox" width="390" columns="30"
																		maxlength="100"></asp:textbox></TD>
																<TD>
																	<asp:RegularExpressionValidator id="RegularExpressionValidator6" runat="server" DESIGNTIMEDRAGDROP="443" ErrorMessage="Please enter a valid number"
																		ValidationExpression="[0]((\d{2}(?:-|\s*)\d{4}(?:-|\s*)\d{4})|(\d{4}(?:-|\s*)\d{6})|(\d{3}(?:-|\s*)\d{3}(?:-|\s*)\d{4})|\d{10})"
																		ControlToValidate="txtBusinessPhone"></asp:RegularExpressionValidator></TD>
																<TD></TD>
															</TR>
															<TR vAlign="top">
																<TD class="SubHead" width="126">
																	<asp:HyperLink id="linkHomeAddress" runat="server" target="_blank">Home Page: </asp:HyperLink></TD>
																<TD>
																	<asp:textbox id="txtHomePage" runat="server" cssclass="NormalTextBox" width="390" columns="30"
																		maxlength="100"></asp:textbox></TD>
																<TD>
																	<asp:RegularExpressionValidator id="RegularExpressionValidator3" runat="server" DESIGNTIMEDRAGDROP="287" ErrorMessage="Please enter a valid URL"
																		ValidationExpression="http://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?" ControlToValidate="txtHomePage"></asp:RegularExpressionValidator></TD>
															</TR>
															<TR vAlign="top">
																<TD class="SubHead" width="126">Phone Abbr:</TD>
																<TD class="SubHead" width="126">
																	<asp:textbox id="txtPhoneAbbr" runat="server" cssclass="NormalTextBox" width="390" columns="30"
																		maxlength="100"></asp:textbox></TD>
																<TD></TD>
																<TD></TD>
															</TR>
															<TR vAlign="top">
																<TD class="SubHead" width="126">Fax Abbr:</TD>
																<TD class="SubHead" width="126">
																	<asp:textbox id="txtFaxAbbr" runat="server" cssclass="NormalTextBox" width="390" columns="30"
																		maxlength="100"></asp:textbox></TD>
																<TD></TD>
																<TD></TD>
															</TR>
															<TR vAlign="top">
																<TD class="SubHead" width="126">Address:
																</TD>
																<TD>
																	<asp:textbox id="txtAddress" runat="server" Height="136px" cssclass="NormalTextBox" width="390"
																		columns="30" maxlength="250" TextMode="MultiLine"></asp:textbox></TD>
																<TD>
																	<asp:requiredfieldvalidator id="Requiredfieldvalidator3" runat="server" DESIGNTIMEDRAGDROP="121" display="Static"
																		errormessage="You Must Enter a Valid Name" controltovalidate="txtFirstName"></asp:requiredfieldvalidator></TD>
															</TR>
												</asp:panel></TD>
										<TR>
											<td><asp:panel id="pnlHome" runat="server" Height="160px" Width="769px">
													<P></P>
													<TABLE id="Table4" height="47" cellSpacing="0" cellPadding="0" width="750" border="0">
														<TBODY>
															<TR vAlign="top">
																<TD class="SubHead" width="126">Phone:
																</TD>
																<TD>
																	<asp:textbox id="txtHomePhone" runat="server" DESIGNTIMEDRAGDROP="269" cssclass="NormalTextBox"
																		width="390" columns="30" maxlength="100"></asp:textbox></TD>
																<TD>
																	<asp:RegularExpressionValidator id="RegularExpressionValidator7" runat="server" DESIGNTIMEDRAGDROP="444" ErrorMessage="Please enter a valid number"
																		ValidationExpression="[0]((\d{2}(?:-|\s*)\d{4}(?:-|\s*)\d{4})|(\d{4}(?:-|\s*)\d{6})|(\d{3}(?:-|\s*)\d{3}(?:-|\s*)\d{4})|\d{10})"
																		ControlToValidate="txtHomePhone"></asp:RegularExpressionValidator></TD>
															</TR>
															<TR vAlign="top">
																<TD class="SubHead" width="126">Fax:</TD>
																<TD class="SubHead" width="126">
																	<asp:textbox id="txtHomeFax" runat="server" cssclass="NormalTextBox" width="390" columns="30"
																		maxlength="100"></asp:textbox></TD>
																<TD>
																	<asp:RegularExpressionValidator id="RegularExpressionValidator8" runat="server" ErrorMessage="Please enter a valid number"
																		ValidationExpression="[0]((\d{2}(?:-|\s*)\d{4}(?:-|\s*)\d{4})|(\d{4}(?:-|\s*)\d{6})|(\d{3}(?:-|\s*)\d{3}(?:-|\s*)\d{4})|\d{10})"
																		ControlToValidate="txtHomeFax"></asp:RegularExpressionValidator></TD>
																<TD></TD>
															</TR>
															<TR vAlign="top">
																<TD class="SubHead" width="126">Home Address:
																</TD>
																<TD>
																	<asp:textbox id="txtHomeAddress" runat="server" Height="136px" cssclass="NormalTextBox" width="390"
																		columns="30" maxlength="250" TextMode="MultiLine"></asp:textbox></TD>
																<TD>
																	<asp:requiredfieldvalidator id="Requiredfieldvalidator4" runat="server" DESIGNTIMEDRAGDROP="121" display="Static"
																		errormessage="You Must Enter a Valid Name" controltovalidate="txtFirstName"></asp:requiredfieldvalidator></TD>
															</TR>
												</asp:panel></td>
										</TR>
									</TABLE>
								</td>
							</tr>
						</table>
						<HR width="587" noShade SIZE="1">
						<P></P>
						<P></P>
						<P></P>
						<TABLE id="Table6" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD width="100%">
									<P align="left"><SPAN class="Normal">
											<TABLE id="Table8" cellSpacing="1" cellPadding="1" width="100%" border="0">
												<TR>
													<TD width="308"><SPAN class="Normal"><asp:linkbutton class="CommandButton" id="updateButton" runat="server" text="Update" borderstyle="none"></asp:linkbutton>&nbsp; 
<asp:linkbutton class="CommandButton" id="cancelButton" runat="server" text="Cancel" borderstyle="none"
																causesvalidation="False"></asp:linkbutton>&nbsp; 
<asp:linkbutton class="CommandButton" id="deleteButton" runat="server" text="Delete this item" borderstyle="none"
																causesvalidation="False"></asp:linkbutton></SPAN></TD>
													<TD><SPAN class="Normal"><asp:label id="lblCreatedText" runat="server">Created by </asp:label>
															<asp:label id="CreatedBy" runat="server" DESIGNTIMEDRAGDROP="982"></asp:label>
															<asp:label id="lblCreatedText2" runat="server">on </asp:label>
															<asp:label id="CreatedDate" runat="server"></asp:label>
														</SPAN></TD>
												</TR>
											</TABLE></P>
									</SPAN></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</table>
			<P><asp:panel id="pnlTemapltes" runat="server" Height="4px"></P>
			<P><uc2:templates id="Templates1" runat="server"></uc2:templates></P>
			</asp:panel></TD></TR></TBODY></TABLE>
			<P>&nbsp;</P>
			</TD></TR></TBODY></TABLE></TD></TR></TBODY></TABLE></TD></TR></TABLE></form>
		<P></P>
	</body>
</HTML>

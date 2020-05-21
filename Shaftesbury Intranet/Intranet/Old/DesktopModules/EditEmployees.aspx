<%@ Register TagPrefix="uc1" TagName="Contacts" Src="Contacts.ascx" %>
<%@ Page language="vb" CodeBehind="EditEmployees.aspx.vb" AutoEventWireup="false" Inherits="ASPNET.StarterKit.Portal.EditEmployees" %>
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
												Employee&nbsp;Details
											</td>
										</tr>
										<tr>
											<td colSpan="2">
												<hr noShade SIZE="1">
											</td>
										</tr>
									</table>
									<table id="Table4" cellSpacing="0" cellPadding="0" width="750" border="0">
										<tr vAlign="top">
											<td class="SubHead" width="176">Name:
											</td>
											<td rowSpan="5" width="22">&nbsp;
											</td>
											<td align="left" width="383"><asp:textbox id="NameField" runat="server" maxlength="50" columns="30" width="390" cssclass="NormalTextBox"></asp:textbox></td>
											<td class="Normal" width="566"><asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" controltovalidate="NameField" errormessage="You Must Enter a Valid Name"
													display="Static"></asp:requiredfieldvalidator></td>
										</tr>
										<tr vAlign="top">
											<td class="SubHead" width="176">Role:
											</td>
											<td width="383"><asp:textbox id="RoleField" runat="server" maxlength="100" columns="30" width="390" cssclass="NormalTextBox"></asp:textbox></td>
										</tr>
										<tr vAlign="top">
											<td class="SubHead" width="176">Email:</td>
											<td align="left" width="383"><asp:textbox id="EmailField" runat="server" maxlength="50" columns="30" width="390" cssclass="NormalTextBox"></asp:textbox></td>
											<td width="566" rowSpan="5">
												<asp:regularexpressionvalidator id="Regularexpressionvalidator2" runat="server" display="Dynamic" errormessage="Must use a valid email address."
													controltovalidate="EmailField" validationexpression="[\w\.-]+(\+[\w-]*)?@([\w-]+\.)+[\w-]+" CssClass="normal"></asp:regularexpressionvalidator>
											</td>
										</tr>
										<tr vAlign="top">
											<td class="SubHead" width="176">Phone Number:
											</td>
											<td width="383"><asp:textbox id="txtphone" runat="server" maxlength="250" columns="30" width="390" cssclass="NormalTextBox"></asp:textbox></td>
										</tr>
										<tr vAlign="top">
											<td class="SubHead" width="176">Address:
											</td>
											<td width="383"><asp:textbox id="txtaddress" runat="server" maxlength="250" columns="30" width="390" cssclass="NormalTextBox"
													Height="112px" TextMode="MultiLine"></asp:textbox></td>
										</tr>
									</table>
									<p>
									<P><asp:linkbutton class="CommandButton" id="updateButton" runat="server" borderstyle="none" text="Update"></asp:linkbutton>&nbsp;
										<asp:linkbutton class="CommandButton" id="cancelButton" runat="server" borderstyle="none" text="Cancel"
											causesvalidation="False"></asp:linkbutton>&nbsp;
										<asp:linkbutton class="CommandButton" id="deleteButton" runat="server" borderstyle="none" text="Delete this item"
											causesvalidation="False"></asp:linkbutton>
										<hr width="500" noShade SIZE="1">
										<span class="Normal">Created by <asp:label id="CreatedBy" runat="server"></asp:label>&nbsp;on 
<asp:label id="CreatedDate" runat="server"></asp:label><br></span>
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

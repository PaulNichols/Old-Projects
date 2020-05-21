<%@ Import Namespace="ASPNET.StarterKit.Portal" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Banner" Src="~/DesktopPortalBanner.ascx" %>
<%@ Page language="vb" CodeBehind="DiscussDetails.aspx.vb" AutoEventWireup="false" Inherits="ASPNET.StarterKit.Portal.DiscussDetails" %>
<HTML>
	<HEAD>
		<link href='<%= Global.GetApplicationPath(Request) &amp; "/ASPNETPortal.css" %>' type="text/css" rel="stylesheet">
			<LINK href="../ASPNETPortal.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0" marginwidth="0" marginheight="0">
		<form runat="server" name="form1">
			<table width="100%" cellspacing="0" cellpadding="0" border="0">
				<tr valign="top">
					<td colspan="2">
						<aspnetportal:banner showtabs="false" runat="server" id="Banner1" />
					</td>
				</tr>
				<tr valign="top">
					<td width="10%">
						&nbsp;
					</td>
					<td>
						<br>
						<table width="600" cellspacing="0" cellpadding="0">
							<tr>
								<td align="left">
									<span class="Head">Message Detail</span>
								</td>
								<td align="right">
									<asp:panel id="ButtonPanel" runat="server"><A class="CommandButton" id="prevItem" title="Previous Message" runat="server">
											<IMG 
            src='<%=Global.GetApplicationPath(Request) &amp; "/images/rew.gif"  %>' 
            border=0></A>&nbsp; <A class="CommandButton" id="nextItem" title="Next Message" runat="server"><IMG 
            src='<%=Global.GetApplicationPath(Request) &amp; "/images/fwd.gif"  %>' 
            border=0></A>&nbsp; 
<asp:linkbutton id="ReplyBtn" runat="server" enableviewstate="false" cssclass="CommandButton" text="Reply to this Message"></asp:linkbutton>
                              </asp:panel>
								</td>
							</tr>
							<tr>
								<td colspan="2">
									<hr noshade size="1">
								</td>
							</tr>
						</table>
						<asp:panel id="EditPanel" visible="false" runat="server">
							<TABLE cellSpacing="0" cellPadding="4" width="600" border="0">
								<TR vAlign="top">
									<TD class="SubHead" width="150">Title:
									</TD>
									<TD rowSpan="4">&nbsp;
									</TD>
									<TD width="*">
										<asp:textbox id="TitleField" runat="server" cssclass="NormalTextBox" maxlength="100" columns="40"
											width="500"></asp:textbox></TD>
								</TR>
								<TR vAlign="top">
									<TD class="SubHead">Body:
									</TD>
									<TD width="*">
										<asp:textbox id="BodyField" runat="server" columns="59" width="500" rows="15" textmode="Multiline"></asp:textbox></TD>
								</TR>
								<TR vAlign="top">
									<TD>&nbsp;
									</TD>
									<TD>
										<asp:linkbutton class="CommandButton" id="updateButton" runat="server" text="Submit"></asp:linkbutton>&nbsp;
										<asp:linkbutton class="CommandButton" id="cancelButton" runat="server" text="Cancel" causesvalidation="False"></asp:linkbutton>&nbsp;
									</TD>
								</TR>
								<TR vAlign="top">
									<TD class="SubHead">Original Message:
									</TD>
									<TD>&nbsp;
									</TD>
								</TR>
							</TABLE>
						</asp:panel>
						<table width="600" cellspacing="0" cellpadding="4" border="0">
							<tr valign="top">
								<td align="left" class="Message">
									<b>Subject: </b>
									<asp:label id="Title" runat="server" />
									<br>
									<b>Author: </b>
									<asp:label id="CreatedByUser" runat="server" />
									<br>
									<b>Date: </b>
									<asp:label id="CreatedDate" runat="server" />
									<br>
									<br>
									<asp:label id="Body" runat="server" />
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

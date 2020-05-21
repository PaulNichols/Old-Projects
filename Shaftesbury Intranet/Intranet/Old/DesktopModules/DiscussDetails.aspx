<%@ Page language="vb" CodeBehind="DiscussDetails.aspx.vb" AutoEventWireup="false" Inherits="ASPNET.StarterKit.Portal.DiscussDetails" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Banner" Src="~/DesktopPortalBanner.ascx" %>
<%@ Import Namespace="ASPNET.StarterKit.Portal" %>
<html>
	<head>
		<link href='<%= Global.GetApplicationPath(Request) & "/ASPNETPortal.css" %>' type="text/css" rel="stylesheet">
	</head>
	<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0" marginwidth="0" marginheight="0">
		<form runat="server" name="form1">
			<table width="100%" cellspacing="0" cellpadding="0" border="0">
				<tr valign="top">
					<td colspan="2">
						<aspnetportal:banner showtabs="false" runat="server" />
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
									<asp:panel id="ButtonPanel" runat="server">
                                  <a id="prevItem" class="CommandButton" title="Previous Message" runat="server"><img src='<%=Global.GetApplicationPath(Request) & "/images/rew.gif"  %>' border="0"></a>&nbsp;
                                  <a id="nextItem" class="CommandButton" title="Next Message" runat="server"><img src='<%=Global.GetApplicationPath(Request) & "/images/fwd.gif"  %>' border="0"></a>&nbsp;
                                  <asp:linkbutton id="ReplyBtn" text="Reply to this Message" runat="server" cssclass="CommandButton" enableviewstate="false" />
                              </asp:panel>
								</td>
							</tr>
							<tr>
								<td colspan="2">
									<hr noshade size="1pt">
								</td>
							</tr>
						</table>
						<asp:panel id="EditPanel" visible="false" runat="server">
							<table width="600" cellspacing="0" cellpadding="4" border="0">
								<tr valign="top">
									<td width="150" class="SubHead">
										Title:
									</td>
									<td rowspan="4">
										&nbsp;
									</td>
									<td width="*">
										<asp:textbox id="TitleField" cssclass="NormalTextBox" width="500" columns="40" maxlength="100" runat="server" />
									</td>
								</tr>
								<tr valign="top">
									<td class="SubHead">
										Body:
									</td>
									<td width="*">
										<asp:textbox id="BodyField" textmode="Multiline" width="500" columns="59" rows="15" runat="server" />
									</td>
								</tr>
								<tr valign="top">
									<td>
										&nbsp;
									</td>
									<td>
										<asp:linkbutton id="updateButton" text="Submit" runat="server" class="CommandButton" />
										&nbsp;
										<asp:linkbutton id="cancelButton" text="Cancel" causesvalidation="False" runat="server" class="CommandButton" />
										&nbsp;
									</td>
								</tr>
								<tr valign="top">
									<td class="SubHead">
										Original Message:
									</td>
									<td>
										&nbsp;
									</td>
								</tr>
							</table>
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
</html>

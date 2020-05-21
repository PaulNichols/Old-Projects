<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Mins.aspx.vb" Inherits="Mins"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>New Minutes</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie3-2nav3-0" name="vs_targetSchema">
		<LINK href="../../ASPNETPortal.css" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript">
if (top.frames.length!=0)
top.location=self.document.location;
self.moveTo(0,0);
self.resizeTo(screen.availWidth,screen.availHeight);
</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<DIV ms_positioning="GridLayout">
				<TABLE height="288" cellSpacing="0" cellPadding="0" width="733" border="0" ms_2d_layout="TRUE">
					<TR>
						<TD width="11" height="0"></TD>
						<TD width="9" height="0"></TD>
						<TD width="1" height="0"></TD>
						<TD width="12" height="0"></TD>
						<TD width="14" height="0"></TD>
						<TD width="81" height="0"></TD>
						<TD width="264" height="0"></TD>
						<TD width="13" height="0"></TD>
						<TD width="328" height="0"></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="9" height="51">
							<P><asp:label id="lblHeader" runat="server" EnableViewState="False" Width="681px" CssClass="SiteTitle2"
									Height="32px">Please enter the following details:</asp:label></P>
						</TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="9" height="21">
							<P></P>
						</TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="5" height="32"></TD>
						<TD>
							<asp:label id="lblMeetingOf" runat="server" CssClass="Normal">Meeting of:</asp:label></TD>
						<TD colSpan="3">
							<asp:DropDownList id="cboType" runat="server" CssClass="normal" Width="256px">
								<asp:ListItem Value="Directors">Directors</asp:ListItem>
								<asp:ListItem Value="Audit Committee">Audit Committee</asp:ListItem>
								<asp:ListItem Value="Remuneration Committee">Remuneration Committee</asp:ListItem>
							</asp:DropDownList></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="3" height="8"></TD>
						<TD colSpan="3" rowSpan="2">
							<asp:label id="lblDateOfMeeting" runat="server" CssClass="Normal">Date of Meeting:</asp:label></TD>
						<TD rowSpan="2">
							<asp:textbox id="txtDateOfMeeting" runat="server" CssClass="Normaltextbox" Width="256px"></asp:textbox></TD>
						<TD colSpan="2"></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="3" height="24"></TD>
						<TD colSpan="2" rowSpan="2">
							<asp:CompareValidator id="CompareValidator2" runat="server" CssClass="normal" ErrorMessage="You must enter a valid meeting date"
								ControlToValidate="txtDateOfMeeting" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="2" height="8"></TD>
						<TD colSpan="4" rowSpan="2"><asp:label id="lblTimeOfMeeting" runat="server" CssClass="Normal">Time of Meeting:</asp:label></TD>
						<TD rowSpan="2"><asp:textbox id="txtTimeOfMeeting" runat="server" Width="256px" CssClass="Normaltextbox"></asp:textbox></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="2" height="24"></TD>
						<TD colSpan="2" rowSpan="2">
							<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" CssClass="normal" Display="Static" ErrorMessage="You must enter a meeting time"
								ControlToValidate="txtTimeOfMeeting"></asp:RequiredFieldValidator></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="4" height="64"></TD>
						<TD colSpan="2"><asp:label id="lblInAttendance" runat="server" CssClass="Normal">In Attendance:</asp:label></TD>
						<TD rowSpan="2"><asp:textbox id="txtInAttendance" runat="server" Width="256px" CssClass="Normaltextbox" Height="88px"
								TextMode="MultiLine"></asp:textbox></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="6" height="32"></TD>
						<TD></TD>
						<TD><asp:button id="cmdNext" runat="server" CssClass="Normal" Text="Next >>"></asp:button></TD>
					</TR>
					<TR vAlign="top">
						<TD height="24"></TD>
						<TD colSpan="8">
							<HR width="674">
						</TD>
					</TR>
				</TABLE>
				</TABLE></TABLE></TABLE></TABLE></DIV>
			<P>&nbsp;</P>
			<P>&nbsp;</P>
			<P>&nbsp;</P>
			<P>
			<P></P>
			&nbsp;
			<P></P>
			<P>&nbsp;</P>
			<P>&nbsp;</P>
			<P>&nbsp;</P>
			<P>&nbsp;</P>
		</form>
	</body>
</HTML>

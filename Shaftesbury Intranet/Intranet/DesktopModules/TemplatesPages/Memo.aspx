<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Memo.aspx.vb" Inherits="Memo"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>New Memo</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie3-2nav3-0" name="vs_targetSchema">
		<SCRIPT language="JavaScript">
if (top.frames.length!=0)
top.location=self.document.location;
self.moveTo(0,0);
self.resizeTo(screen.availWidth,screen.availHeight);
		</SCRIPT>
		<LINK href="../../ASPNETPortal.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<DIV ms_positioning="GridLayout">
				<TABLE height="336" cellSpacing="0" cellPadding="0" width="689" border="0" ms_2d_layout="TRUE">
					<TR>
						<TD width="8" height="0"></TD>
						<TD width="5" height="0"></TD>
						<TD width="5" height="0"></TD>
						<TD width="14" height="0"></TD>
						<TD width="15" height="0"></TD>
						<TD width="33" height="0"></TD>
						<TD width="264" height="0"></TD>
						<TD width="136" height="0"></TD>
						<TD width="209" height="0"></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="9" height="51">
							<P><asp:label id="lblHeader" runat="server" Height="32px" CssClass="SiteTitle2" Width="681px"
									EnableViewState="False">Please enter the following details:</asp:label></P>
						</TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="9" height="21">
							<P></P>
						</TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="5" height="24"></TD>
						<TD>
							<asp:label id="lblTo" runat="server" CssClass="Normal" Height="4px">To:</asp:label></TD>
						<TD>
							<asp:textbox id="txtTo" runat="server" Width="256px" CssClass="Normaltextbox"></asp:textbox></TD>
						<TD colSpan="2">
							<asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" display="Static" errormessage="You must enter at least one recipient"
								controltovalidate="txtTo" CssClass="normal"></asp:requiredfieldvalidator></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="6" height="40"></TD>
						<TD colSpan="3">
							<asp:textbox id="txtTo2" runat="server" Width="256px" CssClass="Normaltextbox"></asp:textbox></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="2" height="24"></TD>
						<TD colSpan="4">
							<asp:label id="lblCopy" runat="server" CssClass="Normal" Height="4px">Copy To:</asp:label></TD>
						<TD colSpan="3">
							<asp:textbox id="txtCopyTo" runat="server" Width="256px" CssClass="Normaltextbox"></asp:textbox></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="6" height="48"></TD>
						<TD colSpan="3">
							<asp:textbox id="txtCopyTo2" runat="server" Width="256px" CssClass="Normaltextbox"></asp:textbox></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="6" height="8"></TD>
						<TD rowSpan="2"><asp:textbox id="txtFrom" runat="server" CssClass="Normaltextbox" Width="256px"></asp:textbox></TD>
						<TD colSpan="2" rowSpan="2"><asp:requiredfieldvalidator id="Requiredfieldvalidator3" runat="server" controltovalidate="txtFrom" errormessage="You must enter a sender for this Memo"
								display="Static" CssClass="normal"></asp:requiredfieldvalidator></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="4" height="24"></TD>
						<TD colSpan="2"><asp:label id="lblFrom" runat="server" CssClass="Normal">From:</asp:label></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="3" height="32"></TD>
						<TD colSpan="3"><asp:label id="lblSubject" runat="server" CssClass="Normal">Subject:</asp:label></TD>
						<TD colSpan="2"><asp:textbox id="txtSubject" runat="server" CssClass="Normaltextbox" Width="392px"></asp:textbox></TD>
						<TD><asp:button id="cmdNext" runat="server" CssClass="Normal" Text="Next >>"></asp:button></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="6" height="32"></TD>
						<TD colSpan="3"><asp:requiredfieldvalidator id="Requiredfieldvalidator6" runat="server" controltovalidate="txtSubject" errormessage="You must enter a subject"
								display="Static" CssClass="normal"></asp:requiredfieldvalidator></TD>
					</TR>
					<TR vAlign="top">
						<TD height="32"></TD>
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

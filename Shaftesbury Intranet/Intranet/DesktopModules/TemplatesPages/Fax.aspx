<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Fax.aspx.vb" Inherits="Fax"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>New Fax</title>
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
				<TABLE height="666" cellSpacing="0" cellPadding="0" width="689" border="0" ms_2d_layout="TRUE">
					<TR>
						<TD width="8" height="0"></TD>
						<TD width="12" height="0"></TD>
						<TD width="12" height="0"></TD>
						<TD width="5" height="0"></TD>
						<TD width="1" height="0"></TD>
						<TD width="2" height="0"></TD>
						<TD width="4" height="0"></TD>
						<TD width="6" height="0"></TD>
						<TD width="9" height="0"></TD>
						<TD width="4" height="0"></TD>
						<TD width="49" height="0"></TD>
						<TD width="264" height="0"></TD>
						<TD width="136" height="0"></TD>
						<TD width="16" height="0"></TD>
						<TD width="161" height="0"></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="15" height="51">
							<P><asp:label id="lblHeader" runat="server" Height="32px" CssClass="SiteTitle2" Width="681px"
									EnableViewState="False">Please enter the following details:</asp:label></P>
						</TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="15" height="29">
							<P></P>
						</TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="6" height="32"></TD>
						<TD colSpan="5">
							<asp:label id="lblYourRef" runat="server" CssClass="Normal">Your Ref:</asp:label></TD>
						<TD colSpan="4"><asp:textbox id="txtYourRef" runat="server" CssClass="Normaltextbox" Width="256px"></asp:textbox></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="11" height="5"></TD>
						<TD rowSpan="2"><asp:textbox id="txtName" runat="server" CssClass="Normaltextbox" Width="256px"></asp:textbox></TD>
						<TD colSpan="3" rowSpan="2"><asp:requiredfieldvalidator id="Requiredfieldvalidator5" runat="server" controltovalidate="txtName" errormessage="You Must Enter a Valid Name"
								display="Static" CssClass="normal"></asp:requiredfieldvalidator></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="9" height="35"></TD>
						<TD colSpan="2"><asp:label id="lblName" runat="server" CssClass="Normal">Name:</asp:label></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="11" height="2"></TD>
						<TD rowSpan="2"><asp:textbox id="txtCompany" runat="server" CssClass="Normaltextbox" Width="256px"></asp:textbox></TD>
						<TD colSpan="3" rowSpan="2"><asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" controltovalidate="txtCompany" errormessage="You Must Enter a Valid Company Name"
								display="Static" CssClass="normal"></asp:requiredfieldvalidator></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="4" height="30"></TD>
						<TD colSpan="7"><asp:label id="lblCompany" runat="server" CssClass="Normal">Company:</asp:label></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="11" height="7"></TD>
						<TD rowSpan="2"><asp:textbox id="txtRef" runat="server" CssClass="Normaltextbox" Width="256px"></asp:textbox></TD>
						<TD colSpan="3" rowSpan="2"></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="6" height="33"></TD>
						<TD colSpan="5"><asp:label id="lblTheirRef" runat="server" CssClass="Normal">Their Ref:</asp:label></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="11" height="4"></TD>
						<TD rowSpan="2"><asp:textbox id="txtTheirFax" runat="server" CssClass="Normaltextbox" Width="256px"></asp:textbox></TD>
						<TD colSpan="3" rowSpan="2"><asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" controltovalidate="txtTheirFax" errormessage="You Must Enter a Valid Fax Number"
								display="Static" CssClass="normal"></asp:requiredfieldvalidator></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="5" height="28"></TD>
						<TD colSpan="6"><asp:label id="lblTheirFax" runat="server" CssClass="Normal">Their Fax:</asp:label></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="11" height="9"></TD>
						<TD rowSpan="2"><asp:textbox id="txtFrom" runat="server" CssClass="Normaltextbox" Width="256px"></asp:textbox></TD>
						<TD colSpan="3" rowSpan="2"><asp:requiredfieldvalidator id="Requiredfieldvalidator3" runat="server" controltovalidate="txtFrom" errormessage="You Must Enter a Valid Sender for this Fax"
								display="Static" CssClass="normal"></asp:requiredfieldvalidator></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="10" height="31"></TD>
						<TD><asp:label id="lblFrom" runat="server" CssClass="Normal">From:</asp:label></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="11" height="6"></TD>
						<TD rowSpan="2"><asp:textbox id="txtJobTitle" runat="server" CssClass="Normaltextbox" Width="256px"></asp:textbox></TD>
						<TD colSpan="3" rowSpan="2"></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="7" height="26"></TD>
						<TD colSpan="4"><asp:label id="lblJobTitle" runat="server" CssClass="Normal">Job Title:</asp:label></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="11" height="11"></TD>
						<TD rowSpan="2"><asp:textbox id="txtNoOfPages" runat="server" CssClass="Normaltextbox" Width="256px">1 (Including This)</asp:textbox></TD>
						<TD colSpan="3" rowSpan="2"><asp:requiredfieldvalidator id="Requiredfieldvalidator4" runat="server" controltovalidate="txtNoOfPages" errormessage="You Must Enter the Number of Pages to be sent"
								display="Static" CssClass="normal"></asp:requiredfieldvalidator></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="2" height="29"></TD>
						<TD colSpan="9"><asp:label id="lblNoOfPages" runat="server" CssClass="Normal">No of Pages:</asp:label></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="11" height="8"></TD>
						<TD rowSpan="2"><asp:textbox id="txtCopiesTo" runat="server" CssClass="Normaltextbox" Width="256px"></asp:textbox></TD>
						<TD colSpan="3" rowSpan="2"></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="3" height="32"></TD>
						<TD colSpan="8"><asp:label id="lblCopiesTo" runat="server" CssClass="Normal">Copies To:</asp:label></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="11" height="8"></TD>
						<TD colSpan="2" rowSpan="3"><asp:textbox id="txtSubject" runat="server" Height="136px" CssClass="Normaltextbox" Width="392px"
								TextMode="MultiLine"></asp:textbox></TD>
						<TD colSpan="2"></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="8" height="120"></TD>
						<TD colSpan="3"><asp:label id="lblSubject" runat="server" CssClass="Normal">Subject:</asp:label></TD>
						<TD colSpan="2"><asp:requiredfieldvalidator id="Requiredfieldvalidator6" runat="server" controltovalidate="txtSubject" errormessage="You Must Enter a Subject"
								display="Static" CssClass="normal"></asp:requiredfieldvalidator></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="11" height="48"></TD>
						<TD></TD>
						<TD><asp:button id="cmdNext" runat="server" CssClass="Normal" Text="Next >>"></asp:button></TD>
					</TR>
					<TR vAlign="top">
						<TD height="82"></TD>
						<TD colSpan="14">
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

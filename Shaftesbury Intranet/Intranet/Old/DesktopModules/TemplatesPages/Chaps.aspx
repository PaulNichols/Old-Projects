<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Chaps.aspx.vb" Inherits="Chaps"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>New CHAPS</title>
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
				<TABLE height="456" cellSpacing="0" cellPadding="0" width="686" border="0" ms_2d_layout="TRUE">
					<TR>
						<TD width="2" height="0"></TD>
						<TD width="12" height="0"></TD>
						<TD width="9" height="0"></TD>
						<TD width="10" height="0"></TD>
						<TD width="9" height="0"></TD>
						<TD width="3" height="0"></TD>
						<TD width="11" height="0"></TD>
						<TD width="65" height="0"></TD>
						<TD width="1" height="0"></TD>
						<TD width="278" height="0"></TD>
						<TD width="1" height="0"></TD>
						<TD width="1" height="0"></TD>
						<TD width="198" height="0"></TD>
						<TD width="86" height="0"></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="14" height="51">
							<P><asp:label id="lblHeader" runat="server" EnableViewState="False" Width="681px" CssClass="SiteTitle2"
									Height="32px">Please enter the following details:</asp:label></P>
						</TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="14" height="21">
							<P></P>
						</TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="3" height="34"></TD>
						<TD colSpan="5">
							<asp:label id="lblTransferDate" runat="server" CssClass="Normal">Transfer Date:</asp:label></TD>
						<TD rowSpan="6"></TD>
						<TD colSpan="3"><asp:textbox id="txtTransferDate" runat="server" Width="256px" CssClass="Normal"></asp:textbox></TD>
						<TD colSpan="2">
							<asp:requiredfieldvalidator id="Requiredfieldvalidator5" runat="server" controltovalidate="txtTransferDate"
								errormessage="You must enter a transfer date" display="Static" CssClass="normal"></asp:requiredfieldvalidator></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="4" height="34"></TD>
						<TD colSpan="4"><asp:label id="lblName" runat="server" CssClass="Normal">Bank Name:</asp:label></TD>
						<TD colSpan="3"><asp:textbox id="txtBankName" runat="server" Width="256px" CssClass="Normal"></asp:textbox></TD>
						<TD colSpan="2">
							<asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" controltovalidate="txtBankName" errormessage="You must enter a Bank Name"
								display="Static" CssClass="normal"></asp:requiredfieldvalidator></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="5" height="34"></TD>
						<TD colSpan="3"><asp:label id="lblSortCode" runat="server" CssClass="Normal">Sort Code:</asp:label></TD>
						<TD colSpan="3"><asp:textbox id="txtSortCode" runat="server" Width="256px" CssClass="Normal"></asp:textbox></TD>
						<TD colSpan="2">
							<asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" controltovalidate="txtSortCode" errormessage="You must enter a  Sort Code"
								display="Static" CssClass="normal"></asp:requiredfieldvalidator></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="2" height="34"></TD>
						<TD colSpan="6"><asp:label id="lblAccountName" runat="server" CssClass="Normal">Account Name:</asp:label></TD>
						<TD colSpan="3"><asp:textbox id="txtAccountName" runat="server" Width="256px" CssClass="Normal"></asp:textbox></TD>
						<TD colSpan="2">
							<asp:requiredfieldvalidator id="Requiredfieldvalidator3" runat="server" controltovalidate="txtAccountName" errormessage="You must enter an Account Name"
								display="Static" CssClass="normal"></asp:requiredfieldvalidator></TD>
					</TR>
					<TR vAlign="top">
						<TD height="34"></TD>
						<TD colSpan="7"><asp:label id="lblAccountNumber" runat="server" CssClass="Normal">Account Number:</asp:label></TD>
						<TD colSpan="3"><asp:textbox id="txtAccountNumber" runat="server" Width="256px" CssClass="Normal"></asp:textbox></TD>
						<TD colSpan="2">
							<asp:requiredfieldvalidator id="Requiredfieldvalidator4" runat="server" controltovalidate="txtAccountNumber"
								errormessage="You must enter an Account Number" display="Static" CssClass="normal"></asp:requiredfieldvalidator></TD>
					</TR>
					<TR vAlign="top">
						<TD height="34"></TD>
						<TD colSpan="7"><asp:label id="lblTransferAccount" runat="server" CssClass="Normal">Transfer Account:</asp:label></TD>
						<TD><asp:textbox id="txtTransferAmount" runat="server" Width="256px" CssClass="Normal"></asp:textbox></TD>
						<TD colSpan="3">
							<asp:requiredfieldvalidator id="Requiredfieldvalidator7" runat="server" controltovalidate="txtTransferAmount"
								errormessage="You must enter a Transfer Amount" display="Static" CssClass="normal"></asp:requiredfieldvalidator></TD>
						<TD rowSpan="3"></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="7" height="36"></TD>
						<TD><asp:label id="lblYourRef" runat="server" CssClass="Normal">Our Ref:</asp:label></TD>
						<TD colSpan="2"><asp:textbox id="txtYourRef" runat="server" Width="256px" CssClass="Normal"></asp:textbox></TD>
						<TD rowSpan="2"></TD>
						<TD colSpan="2">
							<asp:requiredfieldvalidator id="Requiredfieldvalidator8" runat="server" controltovalidate="txtYourRef" errormessage="You must enter your Reference"
								display="Static" CssClass="normal"></asp:requiredfieldvalidator></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="6" height="40"></TD>
						<TD colSpan="2"><asp:label id="lblTheirRef" runat="server" CssClass="Normal"> Reference:</asp:label></TD>
						<TD colSpan="2"><asp:textbox id="txtRef" runat="server" Width="256px" CssClass="Normal"></asp:textbox></TD>
						<TD colSpan="2">
							<asp:requiredfieldvalidator id="Requiredfieldvalidator9" runat="server" controltovalidate="txtRef" errormessage="You must enter their reference"
								display="Static" CssClass="normal"></asp:requiredfieldvalidator></TD>
					</TR>
					<TR vAlign="top">
						<TD colSpan="13" height="32"></TD>
						<TD><asp:button id="cmdNext" runat="server" CssClass="Normal" Text="Next >>"></asp:button></TD>
					</TR>
					<TR vAlign="top">
						<TD height="72"></TD>
						<TD colSpan="13">
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

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Peter.aspx.vb" Inherits="PetersLetter"%>
<html><HEAD>
	<title>Private Letter for Peter</title>
	<LINK href="../../ASPNETPortal.css" type="text/css" rel="stylesheet">
	<SCRIPT language="JavaScript">
if (top.frames.length!=0)
top.location=self.document.location;
self.moveTo(0,0);
self.resizeTo(screen.availWidth,screen.availHeight);
</script>
</HEAD>
<BODY>
	<form runat="server">
		<P></P>
		<DIV ms_positioning="GridLayout">
			<TABLE height="642" cellSpacing="0" cellPadding="0" width="765" border="0" ms_2d_layout="TRUE">
				<TR>
					<TD width="8" height="0"></TD>
					<TD width="2" height="0"></TD>
					<TD width="6" height="0"></TD>
					<TD width="7" height="0"></TD>
					<TD width="20" height="0"></TD>
					<TD width="11" height="0"></TD>
					<TD width="15" height="0"></TD>
					<TD width="3" height="0"></TD>
					<TD width="5" height="0"></TD>
					<TD width="47" height="0"></TD>
					<TD width="1" height="0"></TD>
					<TD width="80" height="0"></TD>
					<TD width="36" height="0"></TD>
					<TD width="57" height="0"></TD>
					<TD width="10" height="0"></TD>
					<TD width="49" height="0"></TD>
					<TD width="61" height="0"></TD>
					<TD width="1" height="0"></TD>
					<TD width="61" height="0"></TD>
					<TD width="54" height="0"></TD>
					<TD width="47" height="0"></TD>
					<TD width="44" height="0"></TD>
					<TD width="140" height="0"></TD>
				</TR>
				<TR vAlign="top">
					<TD colSpan="23" height="51">
						<P><asp:label id="lblHeader" Height="32px" CssClass="SiteTitle2" runat="server" Width="720px"
								EnableViewState="False">Please enter the following details:</asp:label></P>
					</TD>
				</TR>
				<TR vAlign="top">
					<TD colSpan="23" height="29">
						<p></p>
					</TD>
				</TR>
				<TR vAlign="top">
					<TD colSpan="5" height="32"></TD>
					<TD colSpan="5"><asp:label id="Label7" CssClass="NormalBold" runat="server">Their Ref:</asp:label></TD>
					<TD colSpan="7">
						<asp:textbox id="txttheirRef" Width="256px" runat="server" CssClass="Normaltextbox"></asp:textbox></TD>
					<TD colSpan="2"><asp:label id="Label3" CssClass="NormalBold" runat="server">Your Ref:</asp:label></TD>
					<TD colSpan="4">
						<asp:textbox id="txtyourref" Width="208px" runat="server" CssClass="Normaltextbox"></asp:textbox></TD>
				</TR>
				<TR vAlign="top">
					<TD colSpan="8" height="8"></TD>
					<TD colSpan="2" rowSpan="2"><asp:label id="Label2" CssClass="NormalBold" runat="server">Name:</asp:label></TD>
					<TD rowSpan="6"></TD>
					<TD colSpan="6" rowSpan="2"><asp:textbox id="txtName" CssClass="Normaltextbox" runat="server" Width="256px"></asp:textbox></TD>
					<TD colSpan="6"></TD>
				</TR>
				<TR vAlign="top">
					<TD colSpan="8" height="24"></TD>
					<TD rowSpan="5"></TD>
					<TD colSpan="2"><asp:label id="Label8" CssClass="NormalBold" runat="server">Delivered by:</asp:label></TD>
					<TD colSpan="3"></TD>
				</TR>
				<TR vAlign="top">
					<TD colSpan="5" height="32"></TD>
					<TD colSpan="5"><asp:label id="Label1" CssClass="NormalBold" runat="server">Company:</asp:label></TD>
					<TD colSpan="6"><asp:textbox id="txtcompany" CssClass="Normaltextbox" runat="server" Width="256px"></asp:textbox></TD>
					<TD colSpan="5" rowSpan="4"><asp:listbox id="lstDeliveredBy" Height="248px" CssClass="Normaltextbox" runat="server" Width="264px"></asp:listbox></TD>
				</TR>
				<TR vAlign="top">
					<TD colSpan="6" height="160"></TD>
					<TD colSpan="4"><asp:label id="Label5" CssClass="NormalBold" runat="server">Address:</asp:label></TD>
					<TD colSpan="6"><asp:textbox id="txtAddress" Height="136px" CssClass="Normaltextbox" runat="server" Width="256px"
							TextMode="MultiLine"></asp:textbox></TD>
				</TR>
				<TR vAlign="top">
					<TD colSpan="8" height="32"></TD>
					<TD colSpan="2"><asp:label id="Label4" CssClass="NormalBold" runat="server">Dear:</asp:label></TD>
					<TD colSpan="6"><asp:textbox id="txtSalutation" CssClass="Normaltextbox" runat="server" Width="256px"></asp:textbox></TD>
				</TR>
				<TR vAlign="top">
					<TD colSpan="6" height="32"></TD>
					<TD colSpan="4"><asp:label id="Label6" CssClass="NormalBold" runat="server">Heading:</asp:label></TD>
					<TD colSpan="6"><asp:textbox id="txtHeading" CssClass="Normaltextbox" runat="server" Width="256px"></asp:textbox></TD>
				</TR>
				<TR vAlign="top">
					<TD colSpan="11" height="8"></TD>
					<TD colSpan="5" rowSpan="2"><asp:radiobuttonlist id="radYours" CssClass="normal" runat="server" Width="184px" RepeatDirection="Horizontal">
							<asp:ListItem></asp:ListItem>
							<asp:ListItem></asp:ListItem>
						</asp:radiobuttonlist></TD>
					<TD colSpan="7" rowSpan="2"></TD>
				</TR>
				<TR vAlign="top">
					<TD colSpan="7" height="24"></TD>
					<TD colSpan="3"><asp:label id="Label9" CssClass="NormalBold" runat="server">Yours:</asp:label></TD>
					<TD></TD>
				</TR>
				<TR vAlign="top">
					<TD height="8"></TD>
					<TD colSpan="22">
						<HR width="720">
					</TD>
				</TR>
				<TR vAlign="top">
					<TD height="24"></TD>
					<TD colSpan="9"><asp:label id="Label11" CssClass="NormalBold" runat="server">To be signed by:</asp:label></TD>
					<TD colSpan="13"></TD>
				</TR>
				<TR vAlign="top">
					<TD colSpan="3" height="40"></TD>
					<TD colSpan="9"><asp:radiobuttonlist id="radSignedBy" CssClass="normal" runat="server" Width="184px" RepeatDirection="Horizontal"
							AutoPostBack="True">
							<asp:ListItem></asp:ListItem>
							<asp:ListItem></asp:ListItem>
						</asp:radiobuttonlist></TD>
					<TD colSpan="2"><asp:textbox id="txtInitials" CssClass="Normaltextbox" runat="server" Width="80px" Enabled="False"></asp:textbox></TD>
					<TD></TD>
					<TD><asp:label id="Label10" CssClass="NormalBold" runat="server">Name:</asp:label></TD>
					<TD colSpan="4"><asp:textbox id="txtSignatoryName" CssClass="Normaltextbox" runat="server" Width="160px" Enabled="False"></asp:textbox></TD>
					<TD><asp:label id="Label12" CssClass="NormalBold" runat="server">Title:</asp:label></TD>
					<TD colSpan="2"><asp:textbox id="txtTitle" CssClass="Normaltextbox" runat="server" Width="88px" Enabled="False"></asp:textbox></TD>
				</TR>
				<TR vAlign="top">
					<TD colSpan="2" height="16"></TD>
					<TD colSpan="21">
						<HR width="720">
					</TD>
				</TR>
				<TR vAlign="top">
					<TD colSpan="4" height="8"></TD>
					<TD colSpan="4" rowSpan="2"><asp:checkbox id="chkEnc" CssClass="normalbold" runat="server" Text="Enc:" AutoPostBack="True"></asp:checkbox></TD>
					<TD colSpan="5"></TD>
					<TD rowSpan="2"><asp:checkbox id="chkCc" CssClass="normalbold" runat="server" Text="CC:" AutoPostBack="True"></asp:checkbox></TD>
					<TD colSpan="9"></TD>
				</TR>
				<TR vAlign="top">
					<TD colSpan="4" height="56"></TD>
					<TD></TD>
					<TD colSpan="4" rowSpan="2"><asp:textbox id="txtEnc" Height="72px" CssClass="Normaltextbox" runat="server" Width="160px"
							TextMode="MultiLine" Enabled="False"></asp:textbox></TD>
					<TD colSpan="5" rowSpan="2"><asp:textbox id="txtCC" Height="72px" CssClass="Normaltextbox" runat="server" Width="160px" TextMode="MultiLine"
							Enabled="False"></asp:textbox></TD>
					<TD colSpan="4"></TD>
				</TR>
				<TR vAlign="top">
					<TD colSpan="9" height="48"></TD>
					<TD></TD>
					<TD colSpan="3"></TD>
					<TD><asp:button id="cmdNext" CssClass="Normal" runat="server" Text="Next >>"></asp:button></TD>
				</TR>
				<TR vAlign="top">
					<TD colSpan="2" height="10"></TD>
					<TD colSpan="21">
						<HR width="720">
					</TD>
				</TR>
			</TABLE>
			</TABLE></TABLE></TABLE></TABLE></DIV>
	</form>
</BODY>
</html>
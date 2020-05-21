<%@ Page Language="vb" AutoEventWireup="false" Codebehind="StandardLabel.aspx.vb" Inherits="StandardLabel"%>
<HTML>
	<HEAD>
		<title>New Labels</title>
		<LINK href="../../ASPNETPortal.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="50%" border="0">
				<TR>
					<TD>
						<P><asp:label id="lblHeader" runat="server" Height="32px" CssClass="SiteTitle2" Width="681px"
								EnableViewState="False">Please enter the following details:</asp:label></P>
					</TD>
				</TR>
				<TR>
					<TD></TD>
				</TR>
				<TR>
					<TD height="95">
						<P>&nbsp;</P>
						<P>
							<asp:RadioButtonList id="radOptions" CssClass="Normal" runat="server" Width="280px" AutoPostBack="True">
								<asp:ListItem Value="1">Print a Single label</asp:ListItem>
								<asp:ListItem Value="0">Create a whole page of the same label</asp:ListItem>
							</asp:RadioButtonList></P>
						<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD>
									<P>
										<asp:Label id="lblRow" runat="server" Visible="False">Row:</asp:Label></P>
								</TD>
								<TD>
									<asp:TextBox id="txtRow" runat="server" Visible="False" Width="64px">1</asp:TextBox></TD>
								<TD>
									<P>
										<asp:CompareValidator id="CompareValidator1" runat="server" DESIGNTIMEDRAGDROP="399" ErrorMessage="Please enter a valid Row number"
											Type="Integer" ControlToValidate="txtRow" Operator="DataTypeCheck"></asp:CompareValidator><BR>
										<asp:CompareValidator id="CompareValidator3" runat="server" ErrorMessage="Please enter a valid Row number less than 8"
											Type="Integer" ControlToValidate="txtRow" Operator="LessThan" ValueToCompare="8"></asp:CompareValidator><BR>
									</P>
								</TD>
							</TR>
							<TR>
								<TD>
									<asp:Label id="lblColumn" runat="server" Visible="False">Column:</asp:Label></TD>
								<TD>
									<asp:TextBox id="txtColumn" runat="server" Width="64px" Visible="False">1</asp:TextBox></TD>
								<TD>
									<asp:CompareValidator id="CompareValidator2" runat="server" DESIGNTIMEDRAGDROP="449" ErrorMessage="Please enter a valid Column  number"
										Type="Integer" ControlToValidate="txtColumn" Operator="DataTypeCheck"></asp:CompareValidator><BR>
									<asp:CompareValidator id="CompareValidator4" runat="server" ErrorMessage="Please enter a valid Column number less than 3"
										Type="Integer" ControlToValidate="txtColumn" Operator="LessThanEqual" ValueToCompare="2"></asp:CompareValidator><BR>
								</TD>
							</TR>
						</TABLE>
						<asp:Label id="lblPrintProblem" runat="server" Visible="False" ForeColor="Red">There was a Problem Printing</asp:Label>
					</TD>
				</TR>
				<TR>
					<TD>
						<HR width="674">
						&nbsp;</TD>
				</TR>
				<TR>
					<TD>
						<P align="right">
							<asp:button id="cmdNext" runat="server" CssClass="Normal" Text="Next >>" Visible="False"></asp:button></P>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

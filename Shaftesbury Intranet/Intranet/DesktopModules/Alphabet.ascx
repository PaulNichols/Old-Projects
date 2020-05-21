<%@ Control Language="vb" AutoEventWireup="false" Codebehind="Alphabet.ascx.vb" Inherits="Alphabet" TargetSchema="http://schemas.microsoft.com/intellisense/ie3-2nav3-0" %>
<LINK href="../ASPNETPortal.css" type="text/css" rel="stylesheet">
	<P align="center">
		<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
			<TR>
				<TD width="114">
					<P align="left"><asp:label class="Head" id="lblCurrentLetter" runat="server" ForeColor="#C04000">Label</asp:label></P>
				</TD>
				<TD>
					<P align="center">|
						<asp:hyperlink id="HyperLink1" runat="server" NavigateUrl="..\DesktopDefault.aspx?tabindex=2&amp;tabid=7&amp;Filter=A">A</asp:hyperlink> |
						<asp:hyperlink id="HyperLink2" runat="server" NavigateUrl="..\DesktopDefault.aspx?tabindex=2&amp;tabid=7&amp;Filter=B">B</asp:hyperlink> |
						<asp:hyperlink id="Hyperlink3" runat="server" NavigateUrl="..\DesktopDefault.aspx?tabindex=2&amp;tabid=7&amp;Filter=C">C</asp:hyperlink> |
						<asp:hyperlink id="Hyperlink4" runat="server" NavigateUrl="..\DesktopDefault.aspx?tabindex=2&amp;tabid=7&amp;Filter=D">D</asp:hyperlink> |
						<asp:hyperlink id="Hyperlink6" runat="server" NavigateUrl="..\DesktopDefault.aspx?tabindex=2&amp;tabid=7&amp;Filter=E">E</asp:hyperlink> |
						<asp:hyperlink id="Hyperlink9" runat="server" NavigateUrl="..\DesktopDefault.aspx?tabindex=2&amp;tabid=7&amp;Filter=F">F</asp:hyperlink> |
						<asp:hyperlink id="Hyperlink10" runat="server" NavigateUrl="..\DesktopDefault.aspx?tabindex=2&amp;tabid=7&amp;Filter=G">G</asp:hyperlink> |
						<asp:hyperlink id="Hyperlink11" runat="server" NavigateUrl="..\DesktopDefault.aspx?tabindex=2&amp;tabid=7&amp;Filter=H">H</asp:hyperlink> |
						<asp:hyperlink id="Hyperlink12" runat="server" NavigateUrl="..\DesktopDefault.aspx?tabindex=2&amp;tabid=7&amp;Filter=I">I</asp:hyperlink> |
						<asp:hyperlink id="Hyperlink8" runat="server" NavigateUrl="..\DesktopDefault.aspx?tabindex=2&amp;tabid=7&amp;Filter=J">J</asp:hyperlink> |
						<asp:hyperlink id="Hyperlink7" runat="server" NavigateUrl="..\DesktopDefault.aspx?tabindex=2&amp;tabid=7&amp;Filter=K">K</asp:hyperlink> |
						<asp:hyperlink id="Hyperlink5" runat="server" NavigateUrl="..\DesktopDefault.aspx?tabindex=2&amp;tabid=7&amp;Filter=L">L</asp:hyperlink> |
						<asp:hyperlink id="Hyperlink13" runat="server" NavigateUrl="..\DesktopDefault.aspx?tabindex=2&amp;tabid=7&amp;Filter=M">M</asp:hyperlink> |
						<asp:hyperlink id="Hyperlink14" runat="server" NavigateUrl="..\DesktopDefault.aspx?tabindex=2&amp;tabid=7&amp;Filter=N">N</asp:hyperlink> |
						<asp:hyperlink id="Hyperlink15" runat="server" NavigateUrl="..\DesktopDefault.aspx?tabindex=2&amp;tabid=7&amp;Filter=O">O</asp:hyperlink> |
						<asp:hyperlink id="Hyperlink16" runat="server" NavigateUrl="..\DesktopDefault.aspx?tabindex=2&amp;tabid=7&amp;Filter=P">P</asp:hyperlink> |
						<asp:hyperlink id="Hyperlink17" runat="server" NavigateUrl="..\DesktopDefault.aspx?tabindex=2&amp;tabid=7&amp;Filter=Q">Q</asp:hyperlink> |
						<asp:hyperlink id="Hyperlink18" runat="server" NavigateUrl="..\DesktopDefault.aspx?tabindex=2&amp;tabid=7&amp;Filter=R">R</asp:hyperlink> |
						<asp:hyperlink id="Hyperlink19" runat="server" NavigateUrl="..\DesktopDefault.aspx?tabindex=2&amp;tabid=7&amp;Filter=S">S</asp:hyperlink> |
						<asp:hyperlink id="Hyperlink20" runat="server" NavigateUrl="..\DesktopDefault.aspx?tabindex=2&amp;tabid=7&amp;Filter=T">T</asp:hyperlink> |
						<asp:hyperlink id="Hyperlink21" runat="server" NavigateUrl="..\DesktopDefault.aspx?tabindex=2&amp;tabid=7&amp;Filter=U">U</asp:hyperlink> |
						<asp:hyperlink id="Hyperlink22" runat="server" NavigateUrl="..\DesktopDefault.aspx?tabindex=2&amp;tabid=7&amp;Filter=V">V</asp:hyperlink> |
						<asp:hyperlink id="Hyperlink23" runat="server" NavigateUrl="..\DesktopDefault.aspx?tabindex=2&amp;tabid=7&amp;Filter=W">W</asp:hyperlink> |
						<asp:hyperlink id="Hyperlink24" runat="server" NavigateUrl="..\DesktopDefault.aspx?tabindex=2&amp;tabid=7&amp;Filter=X">X</asp:hyperlink> |
						<asp:hyperlink id="Hyperlink25" runat="server" NavigateUrl="..\DesktopDefault.aspx?tabindex=2&amp;tabid=7&amp;Filter=Y">Y</asp:hyperlink> |
						<asp:hyperlink id="Hyperlink26" runat="server" NavigateUrl="..\DesktopDefault.aspx?tabindex=2&amp;tabid=7&amp;Filter=Z">Z</asp:hyperlink> |
						<asp:hyperlink id="Hyperlink27" runat="server" NavigateUrl="..\DesktopDefault.aspx?tabindex=2&amp;tabid=7&amp;Filter=*">View All</asp:hyperlink> |
					</P>
				</TD>
				<TD>
					<P align="right"><asp:radiobuttonlist id="radPublic" CssClass="Normal" runat="server" Width="154px" Height="16px" RepeatDirection="Horizontal"
							AutoPostBack="True">
							<asp:ListItem></asp:ListItem>
							<asp:ListItem></asp:ListItem>
						</asp:radiobuttonlist></P>
				</TD>
			</TR>
		</TABLE>
	</P>

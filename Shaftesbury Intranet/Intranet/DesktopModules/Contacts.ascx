<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="~/DesktopModuleTitle.ascx"%>
<%@ Register TagPrefix="uc1" TagName="Alphabet" Src="Alphabet.ascx" %>
<%@ Control language="vb" Inherits="ASPNET.StarterKit.Portal.Contacts" CodeBehind="Contacts.ascx.vb" AutoEventWireup="false" targetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="GeneralTemplates" Src="TemplatesPages/GeneralTemplates.ascx" %>
<HTML>
	<HEAD>
		<link href="<%= _styleSheet %>" type=text/css rel=stylesheet>
		<script src="scripts.js"></script>
	</HEAD>
	<body>
		<P><ASPNETPORTAL:TITLE id="Title" EditText="Add New Contact" EditUrl="~/DesktopModules/EditContacts.aspx"
				runat="server"></ASPNETPORTAL:TITLE>
			<uc1:alphabet id="Alphabet" runat="server"></uc1:alphabet>
			<asp:datagrid id="grdContacts" border="0" runat="server" width="100%" AutoGenerateColumns="False"
				ShowFooter="True" AllowPaging="True" AllowSorting="True">
				<FooterStyle ForeColor="#000099" BackColor="#CCCCCC"></FooterStyle>
				<Columns>
					<asp:TemplateColumn SortExpression="FileAs" HeaderText="File As" FooterText="Pages:">
						<HeaderStyle CssClass="CategoryHeader"></HeaderStyle>
						<ItemStyle CssClass="Normal"></ItemStyle>
						<ItemTemplate>
							<asp:HyperLink id=HyperLink2 runat="server" Visible="<%# IsEditable %>" NavigateUrl='<%# "~/DesktopModules/EditContacts.aspx?ItemID=" &amp; DataBinder.Eval(Container.DataItem,"ItemID") &amp; "&amp;mid=" &amp; ModuleId %>' Text='<%# DataBinder.Eval(Container.DataItem,"FileAs") %>'>
							</asp:HyperLink>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="Fullname" HeaderText="Full Name">
						<HeaderStyle CssClass="CategoryHeader"></HeaderStyle>
						<ItemStyle CssClass="Normal"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Company Name" SortExpression="Company Name" HeaderText="Company Name">
						<HeaderStyle CssClass="CategoryHeader"></HeaderStyle>
						<ItemStyle CssClass="Normal"></ItemStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Email">
						<HeaderStyle CssClass="CategoryHeader"></HeaderStyle>
						<ItemStyle CssClass="Normal"></ItemStyle>
						<ItemTemplate>
							<asp:HyperLink id=HyperLink1 runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Email") %>' NavigateUrl='<%# "mailto:" &amp; DataBinder.Eval(Container.DataItem,"Email") %>'>
							</asp:HyperLink>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="BusinessPhone" HeaderText="Phone">
						<HeaderStyle CssClass="CategoryHeader"></HeaderStyle>
						<ItemStyle CssClass="Normal"></ItemStyle>
					</asp:BoundColumn>
				</Columns>
				<PagerStyle NextPageText="Next" PrevPageText="Previous" Mode="NumericPages"></PagerStyle>
			</asp:datagrid>
		</P>
		<P align="center"></P>
		<P align="center">
			<TABLE id="Table1" height="32" cellSpacing="0" cellPadding="0" width="416" border="0">
				<TR>
					<TD>
						<P align="center"><uc1:generaltemplates id="GeneralTemplates" runat="server"></uc1:generaltemplates></P>
					</TD>
				</TR>
			</TABLE>
		</P>
		<P>&nbsp;</P>
		<P><asp:label id="lblFileAsOrder" runat="server" Visible="False"></asp:label><asp:label id="lblCompanyNameOrder" runat="server" Visible="False"></asp:label>
			<asp:Label id="lblPublic" runat="server" Visible="False"></asp:Label>
		</P>
	</body>
</HTML>

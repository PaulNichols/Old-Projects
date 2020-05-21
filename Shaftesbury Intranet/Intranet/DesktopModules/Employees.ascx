<%@ Control language="vb" Inherits="ASPNET.StarterKit.Portal.Employees" CodeBehind="Employees.ascx.vb" AutoEventWireup="false" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="~/DesktopModuleTitle.ascx"%>
<LINK href="../ASPNETPortal.css" type="text/css" rel="stylesheet">
	<ASPNETPortal:title EditText="Add New Employee" EditUrl="~/DesktopModules/EditEmployees.aspx" runat="server"
		id="Title1" />
	<asp:datagrid id="grdEmployees" Border="0" width="100%" AutoGenerateColumns="False" EnableViewState="False"
		runat="server" AllowPaging="True" ShowFooter="True">
		<FooterStyle BackColor="#CCCCCC"></FooterStyle>
		<Columns>
			<asp:TemplateColumn FooterText="Pages:">
				<ItemTemplate>
					<asp:HyperLink ImageUrl="~/images/edit.gif" NavigateUrl='<%# "~/DesktopModules/EditEmployees.aspx?ItemID=" & DataBinder.Eval(Container.DataItem,"ItemID") & "&mid=" & ModuleId %>' Visible="<%# IsEditable %>" runat="server" />
				</ItemTemplate>
				<FooterStyle ForeColor="#000099"></FooterStyle>
			</asp:TemplateColumn>
			<asp:BoundColumn DataField="FullName" HeaderText="Name">
				<HeaderStyle CssClass="NormalBold"></HeaderStyle>
				<ItemStyle CssClass="Normal"></ItemStyle>
			</asp:BoundColumn>
			<asp:BoundColumn DataField="Role" HeaderText="Role">
				<HeaderStyle CssClass="NormalBold"></HeaderStyle>
				<ItemStyle CssClass="Normal"></ItemStyle>
			</asp:BoundColumn>
			<asp:TemplateColumn HeaderText="Email">
				<HeaderStyle CssClass="NormalBold"></HeaderStyle>
				<ItemTemplate>
					<asp:HyperLink CssClass="Normal" id=linkEmail runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Email") %>' NavigateUrl='<%# "mailto:" &amp; DataBinder.Eval(Container.DataItem,"Email") %>'>
					</asp:HyperLink>
				</ItemTemplate>
			</asp:TemplateColumn>
		</Columns>
		<PagerStyle NextPageText="Next" PrevPageText="Previous" Mode="NumericPages"></PagerStyle>
	</asp:datagrid>

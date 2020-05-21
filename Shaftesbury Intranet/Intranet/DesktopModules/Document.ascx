<%@ Control language="vb" Inherits="ASPNET.StarterKit.Portal.Document" CodeBehind="Document.ascx.vb" AutoEventWireup="false" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="~/DesktopModuleTitle.ascx"%>
<LINK href="../ASPNETPortal.css" type="text/css" rel="stylesheet">
	<ASPNETPORTAL:TITLE id="Title1" runat="server" EditUrl="~/DesktopModules/EditDocs.aspx" EditText="Add New Document"></ASPNETPORTAL:TITLE>
	<asp:datagrid id="myDataGrid" runat="server" AllowSorting="True" EnableViewState="False" AutoGenerateColumns="False"
		width="100%" Border="0">
		<Columns>
			<asp:TemplateColumn>
				<ItemTemplate>
					<asp:HyperLink id="editLink" ImageUrl="~/images/edit.gif" NavigateUrl='<%# "~/DesktopModules/EditDocs.aspx?ItemID=" & DataBinder.Eval(Container.DataItem,"ItemID")  & "&mid=" & ModuleId %>' Visible="<%# IsEditable %>" runat="server" />
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Title">
				<HeaderStyle CssClass="NormalBold"></HeaderStyle>
				<ItemTemplate>
					<asp:HyperLink id="docLink" Text='<%# DataBinder.Eval(Container.DataItem,"FileFriendlyName") %>' NavigateUrl='<%# GetBrowsePath(DataBinder.Eval(Container.DataItem,"FileNameUrl"), DataBinder.Eval(Container.DataItem,"ContentSize"), CInt(DataBinder.Eval(Container.DataItem,"ItemId"))) %>' CssClass="Normal" Target="_new" runat="server" />
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:BoundColumn DataField="CreatedByUser" HeaderText="Owner">
				<HeaderStyle CssClass="NormalBold"></HeaderStyle>
				<ItemStyle CssClass="Normal"></ItemStyle>
			</asp:BoundColumn>
			<asp:BoundColumn DataField="Category" HeaderText="Category">
				<HeaderStyle CssClass="NormalBold"></HeaderStyle>
				<ItemStyle Wrap="False" CssClass="Normal"></ItemStyle>
			</asp:BoundColumn>
			<asp:BoundColumn DataField="CreatedDate" HeaderText="Last Updated" DataFormatString="{0:d}">
				<HeaderStyle CssClass="NormalBold"></HeaderStyle>
				<ItemStyle CssClass="Normal"></ItemStyle>
			</asp:BoundColumn>
		</Columns>
	</asp:datagrid>
	<asp:label id="lblAreaOrder" runat="server" Visible="False"></asp:label>

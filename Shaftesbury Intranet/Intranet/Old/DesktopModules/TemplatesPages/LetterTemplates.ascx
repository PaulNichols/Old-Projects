<%@ Register TagPrefix="uc2" TagName="Alphabet" Src="../Alphabet.ascx" %>
<%@ Control language="vb" Inherits="ASPNET.StarterKit.Portal.LetterTemplates" CodeBehind="LetterTemplates.ascx.vb" AutoEventWireup="false" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="~/DesktopModuleTitle.ascx"%>
<meta content="True" name="vs_showGrid">
<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
	<TR>
		<TD width="9"></TD>
		<TD rowSpan="1">
			<ASPNETPortal:title id="Title1" EditText="Add New Document" EditUrl="~/DesktopModules/EditDocs.aspx"
				runat="server"></ASPNETPortal:title></TD>
		<TD></TD>
	</TR>
</TABLE>
<P>
	<asp:datagrid id="myDataGrid" runat="server" EnableViewState="False" AutoGenerateColumns="False"
		width="100%" Border="0" AllowSorting="True">
		<Columns>
			<asp:TemplateColumn>
				<ItemTemplate>
					<asp:HyperLink id=editLink runat="server" Visible="<%# false %>" ImageUrl="~/images/edit.gif">
					</asp:HyperLink>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Title">
				<HeaderStyle CssClass="NormalBold"></HeaderStyle>
				<ItemTemplate>
					<asp:HyperLink id=docLink runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FileFriendlyName") %>' NavigateUrl='<%# GetBrowsePath(DataBinder.Eval(Container.DataItem,"FileNameUrl"), DataBinder.Eval(Container.DataItem,"ContentSize"), CInt(DataBinder.Eval(Container.DataItem,"ItemId")),DataBinder.Eval(Container.DataItem,"TemplatePage")) %>' CssClass="Normal" Target="_blank">
					</asp:HyperLink>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:BoundColumn DataField="Description" HeaderText="Description">
				<HeaderStyle CssClass="NormalBold"></HeaderStyle>
				<ItemStyle CssClass="Normal"></ItemStyle>
			</asp:BoundColumn>
			<asp:BoundColumn DataField="Category" HeaderText="Category">
				<HeaderStyle CssClass="NormalBold"></HeaderStyle>
				<ItemStyle Wrap="False" CssClass="Normal"></ItemStyle>
			</asp:BoundColumn>
		</Columns>
	</asp:datagrid></P>
<P>&nbsp;</P>
<P>&nbsp;</P>

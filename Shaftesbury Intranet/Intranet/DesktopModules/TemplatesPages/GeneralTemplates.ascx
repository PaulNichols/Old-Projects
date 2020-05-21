<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="~/DesktopModuleTitle.ascx"%>
<%@ Control language="vb" Inherits="ASPNET.StarterKit.Portal.GeneralTemplates" CodeBehind="GeneralTemplates.ascx.vb" AutoEventWireup="false" %>
<%@ Register TagPrefix="uc2" TagName="Alphabet" Src="../Alphabet.ascx" %>
<LINK href="../../ASPNETPortal.css" type="text/css" rel="stylesheet">
	<meta content="True" name="vs_showGrid">
	<P>
		<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
			<TR>
				<TD>
					<P align="center"><ASPNETPORTAL:TITLE id="Title" EditText="Add New Document" EditUrl="~/DesktopModules/EditDocs.aspx"
							runat="server"></ASPNETPORTAL:TITLE></P>
				</TD>
			</TR>
		</TABLE>
	</P>
	<P><asp:datagrid id="myDataGrid" runat="server" EnableViewState="False" AutoGenerateColumns="False"
			width="100%" Border="0" AllowSorting="True">
			<Columns>
				<asp:TemplateColumn>
					<ItemTemplate>
						<asp:HyperLink id=editLink runat="server" ImageUrl="~/images/edit.gif" Visible="<%# false %>">
						</asp:HyperLink>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Title">
					<HeaderStyle CssClass="NormalBold"></HeaderStyle>
					<ItemTemplate>
						<asp:HyperLink id=docLink runat="server" Target="_blank" CssClass="Normal" NavigateUrl='<%# GetBrowsePath(DataBinder.Eval(Container.DataItem,"FileNameUrl"), DataBinder.Eval(Container.DataItem,"ContentSize"), CInt(DataBinder.Eval(Container.DataItem,"ItemId")),DataBinder.Eval(Container.DataItem,"TemplatePage")) %>' Text='<%# DataBinder.Eval(Container.DataItem,"FileFriendlyName") %>'>
						</asp:HyperLink>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:BoundColumn DataField="Description" HeaderText="Description">
					<HeaderStyle CssClass="NormalBold"></HeaderStyle>
					<ItemStyle CssClass="Normal"></ItemStyle>
				</asp:BoundColumn>
			</Columns>
		</asp:datagrid></P>
	<P align="center">&nbsp;</P>

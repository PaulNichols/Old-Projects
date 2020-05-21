<%@ Control language="vb" Inherits="ASPNET.StarterKit.Portal.Discussion" CodeBehind="Discussion.ascx.vb" AutoEventWireup="false" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="~/DesktopModuleTitle.ascx"%>
<%@ Import Namespace="ASPNET.StarterKit.Portal" %>

<ASPNETPortal:title id=Title1 runat="server" EditTarget="_new" EditUrl="~/DesktopModules/DiscussDetails.aspx" EditText="Add New Thread">
</ASPNETPortal:title>
<%-- discussion list --%>
<asp:DataList id="TopLevelList" width="98%" ItemStyle-Cssclass="Normal" DataKeyField="Parent" runat="server">
    <ItemTemplate>
        <asp:ImageButton id="btnSelect" ImageUrl='<%# NodeImage(Cint(DataBinder.Eval(Container.DataItem, "ChildCount"))) %>' CommandName="select" runat="server" />
        <asp:hyperlink Text='<%# DataBinder.Eval(Container.DataItem, "Title") %>' NavigateUrl='<%# FormatUrl(CInt(DataBinder.Eval(Container.DataItem, "ItemID"))) %>' Target="_new" runat="server" />, from
        <%# DataBinder.Eval(Container.DataItem,"CreatedByUser") %>, posted
        <%# DataBinder.Eval(Container.DataItem,"CreatedDate", "{0:g}") %>
    </ItemTemplate>
    <SelectedItemTemplate>
        <asp:ImageButton id="btnCollapse" ImageUrl="~/images/minus.gif" runat="server" CommandName="collapse" />
        <asp:hyperlink Text='<%# DataBinder.Eval(Container.DataItem, "Title") %>' NavigateUrl='<%# FormatUrl(CInt(DataBinder.Eval(Container.DataItem, "ItemID"))) %>' Target="_new" runat="server" />, from
        <%# DataBinder.Eval(Container.DataItem,"CreatedByUser") %>, posted
        <%# DataBinder.Eval(Container.DataItem,"CreatedDate", "{0:g}") %>
        <asp:DataList id="DetailList" ItemStyle-Cssclass="Normal" datasource="<%# GetThreadMessages() %>" runat="server">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "Indent") %>
                <img src="<%=Global.GetApplicationPath(Request)%>/images/1x1.gif" height="15">
                <asp:hyperlink Text='<%# DataBinder.Eval(Container.DataItem, "Title") %>' NavigateUrl='<%# FormatUrl(CInt(DataBinder.Eval(Container.DataItem, "ItemID"))) %>' Target="_new" runat="server" />, from
                <%# DataBinder.Eval(Container.DataItem,"CreatedByUser") %>, posted
                <%# DataBinder.Eval(Container.DataItem,"CreatedDate", "{0:g}") %>
            </ItemTemplate>
        </asp:DataList>
    </SelectedItemTemplate>
</asp:DataList>


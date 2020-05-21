<%@ Page Language="C#" MasterPageFile="~/Main.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="TransactionTypes.aspx.cs" Inherits="Discovery.UI.Web.ReferenceData.TransactionTypes"
    Title="Management Server - Transaction Types" %>

<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="Discovery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <div class="PageTitle">
        Transaction Types</div>
    <hr />
    <asp:ImageButton ID="NewButton" runat="server" SkinID="NewButton" />
    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
        <contenttemplate>
<Discovery:DiscoveryGrid id="GridView1" runat="server" DataSourceID="TransactionTypesDataSource" DataKeyNames="Id" DefaultSortExpression="Code" DetailURL="TransactionType.aspx" AllowPaging="True" DoHiLiteRow="True"><Columns>
<asp:BoundField DataField="Code" SortExpression="Code" HeaderText="Code"></asp:BoundField>
<asp:BoundField DataField="Description" SortExpression="Description" HeaderText="Description"></asp:BoundField>
<asp:TemplateField SortExpression="IsStock" HeaderText="Stock">
<ItemStyle HorizontalAlign="Center"></ItemStyle>
<ItemTemplate>
<asp:Image id="Image1" runat="server" __designer:wfdid="w24" ImageUrl='<%# Eval("IsStock", "~/Images/{0}.gif") %>'></asp:Image> 
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField SortExpression="IsNonStock" HeaderText="Non Stock">
<ItemStyle HorizontalAlign="Center"></ItemStyle>
<ItemTemplate>
<asp:Image id="Image2" runat="server" ImageUrl='<%# Eval("IsNonStock", "~/Images/{0}.gif") %>'></asp:Image>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField SortExpression="IsCollection" HeaderText="Collection">
<ItemStyle HorizontalAlign="Center"></ItemStyle>
<ItemTemplate>
<asp:Image id="Image3" runat="server" ImageUrl='<%# Eval("IsCollection", "~/Images/{0}.gif") %>'></asp:Image>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField SortExpression="IsSample" HeaderText="Sample">
<ItemStyle HorizontalAlign="Center"></ItemStyle>
<ItemTemplate>
<asp:Image id="Image4" runat="server" ImageUrl='<%# Eval("IsSample", "~/Images/{0}.gif") %>'></asp:Image>
</ItemTemplate>
</asp:TemplateField>
</Columns>
</Discovery:DiscoveryGrid> 
</contenttemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="TransactionTypesDataSource" runat="server" SelectMethod="GetTransactionTypes"
        TypeName="Discovery.BusinessObjects.Controllers.TransactionTypeController"></asp:ObjectDataSource>
</asp:Content>

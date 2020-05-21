<%@ Page Language="C#" MasterPageFile="~/Main.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="TransactionSubTypes.aspx.cs" Inherits="Discovery.UI.Web.ReferenceData.TransactionSubTypes"
    Title="Management Server - Transaction Sub Types" %>

<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="Discovery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <div class="PageTitle">
        Transaction Sub Types</div>
    <hr />
    <asp:ImageButton ID="NewButton" runat="server" SkinID="NewButton" />
    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
        <contenttemplate>
        <Discovery:DiscoveryGrid  AllowPaging="true" DetailURL="TransactionSubType.aspx" DefaultSortExpression="Code" ID="GridView1" runat="server" DataSourceID="TransactionSubTypesDataSource" DataKeyNames="Id">
            <Columns>
                <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" />
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />

                <asp:TemplateField SortExpression="IsNormal" HeaderText="Normal">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                <asp:Image id="Image1" runat="server" ImageUrl='<%# Eval("IsNormal", "~/Images/{0}.gif") %>'></asp:Image> 
                </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField SortExpression="IsTransfer" HeaderText="Transfer">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                <asp:Image id="Image2" runat="server" ImageUrl='<%# Eval("IsTransfer", "~/Images/{0}.gif") %>'></asp:Image> 
                </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField SortExpression="IsLocalConversion" HeaderText="Local Conv.">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                <asp:Image id="Image3" runat="server" ImageUrl='<%# Eval("IsLocalConversion", "~/Images/{0}.gif") %>'></asp:Image> 
                </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField SortExpression="Is3rdPartyConversion" HeaderText="3rd Party Conv.">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                <asp:Image id="Image4" runat="server" ImageUrl='<%# Eval("Is3rdPartyConversion", "~/Images/{0}.gif") %>'></asp:Image> 
                </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </Discovery:DiscoveryGrid>
           </contenttemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="TransactionSubTypesDataSource" runat="server" SelectMethod="GetTransactionSubTypes"
        TypeName="Discovery.BusinessObjects.Controllers.TransactionSubTypeController"></asp:ObjectDataSource>
</asp:Content>

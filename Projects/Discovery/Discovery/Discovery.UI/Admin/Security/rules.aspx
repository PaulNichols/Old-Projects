<%@ Page Language="C#" MasterPageFile="~/Main.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="rules.aspx.cs" Inherits="Discovery.UI.Web.Security.Rules"
    Title="Management Server - Rules Maintenance" %>

<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="Discovery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <div class="PageTitle">
        Authorisation Rules</div>
    <hr />
    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
        <contenttemplate>
    <Discovery:DiscoveryGrid AllowPaging="True"  DetailURL="rule.aspx" Width="800px" DefaultSortExpression="Name" ID="GridView1" runat="server" DataKeyNames="Name" DataSourceID="RulesDataSource">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="Expression" HeaderText="Expression"  />
        </Columns>
    </Discovery:DiscoveryGrid >
        </contenttemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="RulesDataSource" runat="server" SelectMethod="GetAllRules"
        TypeName="Discovery.ComponentServices.Security.SecurityController"></asp:ObjectDataSource>
    <asp:Button ID="ButtonExportMatrix" runat="server" OnClick="ButtonExportMatrix_Click"
        Text="Export Rules Matrix" />
</asp:Content>

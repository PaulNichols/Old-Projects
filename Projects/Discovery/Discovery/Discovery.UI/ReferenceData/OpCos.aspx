<%@ Page Language="C#" MasterPageFile="~/Main.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="OpCos.aspx.cs" Inherits="Discovery.UI.Web.ReferenceData.OpCos"
    Title="Management Server - Operating Companies" %>
<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="Discovery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <div class="PageTitle">
        Operating Companies</div>
    <hr />
    <asp:ImageButton ID="NewButton" runat="server" SkinID="NewButton" />
    <asp:UpdatePanel Id="Opco" runat="server">
        <contenttemplate>
        <Discovery:DiscoveryGrid  AllowPaging="true" DetailURL="OpCo.aspx" DefaultSortExpression="Code"  ID="GridView1" runat="server" DataSourceID="OpCosDataSource" DataKeyNames="Id">
            <Columns>
                <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" />
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
            </Columns>
        </Discovery:DiscoveryGrid>
        </contenttemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="OpCosDataSource" runat="server" SelectMethod="GetOpCos"
        TypeName="Discovery.BusinessObjects.Controllers.OpcoController">
        <SelectParameters>
            <asp:Parameter DefaultValue="True" Name="fullyPopulate" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

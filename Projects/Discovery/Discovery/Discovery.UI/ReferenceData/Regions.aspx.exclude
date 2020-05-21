<%@ Page Language="C#" MasterPageFile="~/Main.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="Regions.aspx.cs" Inherits="Discovery.UI.Web.ReferenceData.Regions" Title="Management Server - Regions"  %>
<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls" TagPrefix="Discovery" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <div class="PageTitle">
        Regions</div>
        <hr />
        <asp:ImageButton ID="NewButton" runat="server" SkinID="NewButton" />
              <asp:UpdatePanel runat="server" ID="UpdatePanel3">
           
                <ContentTemplate>
          
        <Discovery:DiscoveryGrid AllowPaging="true"  DetailURL="region.aspx" DefaultSortExpression="Code"  ID="GridView1" runat="server" DataSourceID="RegionsDataSource" DataKeyNames="Id">
            <Columns>
                <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" />
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
            </Columns>
          
        </Discovery:DiscoveryGrid>
          </ContentTemplate>
            </asp:UpdatePanel>
        <asp:ObjectDataSource ID="RegionsDataSource" runat="server" SelectMethod="GetRegions"
            TypeName="Discovery.BusinessObjects.Controllers.RegionController"></asp:ObjectDataSource>
    
</asp:Content>


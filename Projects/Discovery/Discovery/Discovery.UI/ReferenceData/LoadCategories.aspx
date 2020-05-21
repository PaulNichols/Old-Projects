<%@ Page Language="C#" MasterPageFile="~/Main.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="LoadCategories.aspx.cs" Inherits="Discovery.UI.Web.ReferenceData.LoadCategories" Title="Management Server - Load Categories"  %>
<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls" TagPrefix="Discovery" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <div class="PageTitle">
        Load Categories</div>
        <hr />
        <asp:ImageButton ID="NewButton" runat="server" SkinID="NewButton" />
              <asp:UpdatePanel runat="server" ID="UpdatePanel3">
           
                <ContentTemplate>
          
        <Discovery:DiscoveryGrid AllowPaging="true" DetailURL="loadcategory.aspx" DefaultSortExpression="Code"  ID="GridView1" runat="server" DataSourceID="LoadCategoriesDataSource" DataKeyNames="Id">
            <Columns>
                <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" />
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
            </Columns>
          
        </Discovery:DiscoveryGrid>
          </ContentTemplate>
            </asp:UpdatePanel>
        <asp:ObjectDataSource ID="LoadCategoriesDataSource" runat="server" SelectMethod="GetLoadCategories"
            TypeName="Discovery.BusinessObjects.Controllers.LoadCategoryController"></asp:ObjectDataSource>
    
</asp:Content>


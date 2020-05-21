<%@ Page Language="C#" MasterPageFile="~/Main.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="SalesLocations.aspx.cs" Inherits="Discovery.UI.Web.ReferenceData.SalesLocations" Title="Management Server - Sales Location" %>
<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls" TagPrefix="Discovery" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">

  <div class="PageTitle">
        Sales Locations</div>
        <hr />
        <br />

         <asp:ImageButton ID="NewButton" runat="server" SkinID="NewButton" />
            <asp:UpdatePanel runat="server" ID="UpdatePanel3">
           
                <ContentTemplate>
        <Discovery:DiscoveryGrid  AllowPaging="true" DetailURL="SalesLocation.aspx" DefaultSortExpression="Location" ID="GridView1" runat="server" DataSourceID="SalesLocationsDataSource" DataKeyNames="Id">
            <Columns>
                <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" />
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                 <asp:TemplateField HeaderText="OpCo" SortExpression="OpCoId" >
                  <ItemTemplate>
                        <asp:Label ID="LabelOpCoDescription" runat="server" Text='<%# Eval("OpCo.Description") %>'></asp:Label>
                    
</ItemTemplate>
                </asp:TemplateField>
            </Columns>
          
        </Discovery:DiscoveryGrid>
             </ContentTemplate>
            </asp:UpdatePanel>
        <asp:ObjectDataSource ID="SalesLocationsDataSource" runat="server" SelectMethod="GetLocations"
            TypeName="Discovery.BusinessObjects.Controllers.SalesLocationController">
            <SelectParameters>
                <asp:Parameter DefaultValue="True" Name="fullyPopulate" Type="Boolean" />
            </SelectParameters>
        </asp:ObjectDataSource>
  
</asp:Content>


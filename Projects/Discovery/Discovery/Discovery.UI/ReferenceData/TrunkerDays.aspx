<%@ Page Language="C#" MasterPageFile="~/Main.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="TrunkerDays.aspx.cs" Inherits="Discovery.UI.Web.ReferenceData.TrunkerDays" Title="Management Server - Trunker Days"  %>
 

<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="Discovery" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    
    <div class="PageTitle">
        Trunker Days</div>
        <hr />
        <asp:ImageButton ID="NewButton" runat="server" SkinID="NewButton" />
         <asp:UpdatePanel runat="server" ID="UpdatePanel3">
           
                <ContentTemplate>
        <Discovery:DiscoveryGrid  AllowPaging="true" DetailURL="trunkerDay.aspx" DefaultSortExpression="SourceWarehouse.Description"  ID="GridView1" runat="server" DataSourceID="TrunkerDaysDataSource" DataKeyNames="Id">
            <Columns>
                <asp:BoundField DataField="Days" HeaderText="Days" SortExpression="Days" />
                <asp:TemplateField HeaderText="Source Warehouse" SortExpression="SourceWarehouse.Description">
                  <ItemTemplate>
                        <asp:Label ID="Label" runat="server" Text='<%# Eval("SourceWarehouse.Description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Destination Warehouse" SortExpression="DestinationWarehouse.Description">
                  <ItemTemplate>
                        <asp:Label ID="LabelDestWarehouseDescription" runat="server" Text='<%# Eval("DestinationWarehouse.Description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
          
        </Discovery:DiscoveryGrid>
          </ContentTemplate>
            </asp:UpdatePanel>
        <asp:ObjectDataSource ID="TrunkerDaysDataSource" runat="server" SelectMethod="GetTrunkerDays"
            TypeName="Discovery.BusinessObjects.Controllers.TrunkerDaysController" >
            
        <SelectParameters>
            
            <asp:Parameter DefaultValue="true" Name="fullyPopulate" Type="Boolean" />
        </SelectParameters>
            </asp:ObjectDataSource>
    
</asp:Content>


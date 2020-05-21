<%@ Page Language="C#" MasterPageFile="~/Main.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="Warehouses.aspx.cs" Inherits="Discovery.UI.Web.ReferenceData.Warehouses" Title="Management Server - Warehouses"  %>
 

<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="Discovery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
  
     <div class="PageTitle">
        Warehouses</div>
        <hr />
         <asp:ImageButton ID="NewButton" runat="server" SkinID="NewButton" />
           <asp:UpdatePanel runat="server" ID="UpdatePanel3">
           
                <ContentTemplate>
        <Discovery:DiscoveryGrid  AllowPaging="True" DetailURL="warehouse.aspx" DefaultSortExpression="Code" ID="GridView1" runat="server" DataSourceID="WarehousesDataSource" DataKeyNames="Id" DoHiLiteRow="True">
            <Columns>
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" />
           
                <asp:TemplateField HeaderText="Commander?">
                    <itemstyle horizontalalign="Center" />
                    <headerstyle horizontalalign="Center" />
                    <itemtemplate>
                     <asp:Image ID="ImageHasCommander" runat="server" SkinID="Check" Visible='<%# Convert.ToBoolean(Eval("HasCommander")) %>' />
                        <asp:Image ID="ImageNotCommander" runat="server" SkinID="UnCheck" Visible='<%# !Convert.ToBoolean(Eval("HasCommander")) %>' />
                    
</itemtemplate>
                </asp:TemplateField>
                
              <asp:TemplateField HeaderText="OpTrak?">
                    <itemstyle horizontalalign="Center" />
                    <headerstyle horizontalalign="Center" />
                    <itemtemplate>
                     <asp:Image ID="ImageHasOptrak" runat="server" SkinID="Check" Visible='<%# Convert.ToBoolean(Eval("HasOptrak")) %>' />
                        <asp:Image ID="ImageNotOptrak" runat="server" SkinID="UnCheck" Visible='<%# !Convert.ToBoolean(Eval("HasOptrak")) %>' />
                    
</itemtemplate>
                    
                </asp:TemplateField>
  <asp:TemplateField HeaderText="TDC?">
                    <itemstyle horizontalalign="Center" />
                    <headerstyle horizontalalign="Center" />
                    <itemtemplate>
                     <asp:Image ID="ImageIsTDC" runat="server" SkinID="Check" Visible='<%# Convert.ToBoolean(Eval("IsTDC")) %>' />
                        <asp:Image ID="ImageNotTDC" runat="server" SkinID="UnCheck" Visible='<%# !Convert.ToBoolean(Eval("IsTDC")) %>' />
                    
</itemtemplate>
                </asp:TemplateField>
              
                <asp:TemplateField HeaderText="Optrak Region">
                  <ItemTemplate>
                        <asp:Label ID="LabelRegionDescription" runat="server" Text='<%# Eval("OptrakRegion.Description") %>'></asp:Label>
                    
</ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Contact Name">
                  <ItemTemplate>
                        <asp:Label ID="LabelContactName" runat="server" Text='<%# Eval("Contact.Name") %>'></asp:Label>
                    
</ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Contact Telephone Number">
                  <ItemTemplate>
                        <asp:Label ID="LabelContactTelephoneNumber" runat="server" Text='<%# Eval("Contact.TelephoneNumber") %>'></asp:Label>
                    
</ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sales Email">
                  <ItemTemplate>
                        <asp:Label ID="LabelContactEmail" runat="server" Text='<%# Eval("Contact.Email") %>'></asp:Label>
                    
</ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PrinterName" HeaderText="Printer" SortExpression="PrinterName" />
            </Columns>
          
        </Discovery:DiscoveryGrid>
                  </ContentTemplate>
            </asp:UpdatePanel>
        <asp:ObjectDataSource ID="WarehousesDataSource" runat="server" SelectMethod="GetWarehouses"
            TypeName="Discovery.BusinessObjects.Controllers.WarehouseController"></asp:ObjectDataSource>


    
</asp:Content>


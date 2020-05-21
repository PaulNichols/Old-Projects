<%@ Page Language="C#" MasterPageFile="~/Main.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="Routes.aspx.cs" Inherits="Discovery.UI.Web.ReferenceData.Routes"
    Title="Management Server - Routes" %>

<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="Discovery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <div class="PageTitle">
        Routes</div>
    <hr />
    <table style="width: 206px">
        <tr>
            <td style="width: 100px">
                Warehouse:
            </td>
            <td style="width: 100px">
                <asp:DropDownList ID="ddlWarehouse" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                    DataSourceID="ObjectDataSourceWarehouse" DataTextField="CodeAndDescription" DataValueField="Id"
                    Width="258px" OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged" OnDataBound="ddlWarehouse_DataBound">
                    <asp:ListItem Selected="True" Value="-1">All</asp:ListItem>
                </asp:DropDownList><asp:ObjectDataSource ID="ObjectDataSourceWarehouse" runat="server"
                    SelectMethod="GetWarehouses" TypeName="Discovery.BusinessObjects.Controllers.WarehouseController">
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
    <asp:ImageButton ID="NewButton" runat="server" SkinID="NewButton" />

    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
        <contenttemplate>
<Discovery:DiscoveryGrid id="GridView1" DataSourceID="RoutesDataSource" runat="server"  DataKeyNames="Id" DefaultSortExpression="Code" DetailURL="route.aspx" AllowPaging="True" DoHiLiteRow="True" AutoGenerateColumns="False"><Columns>
<asp:BoundField DataField="Code" SortExpression="Code" HeaderText="Code"></asp:BoundField>
<asp:BoundField DataField="Description" SortExpression="Description" HeaderText="Description"></asp:BoundField>
<asp:TemplateField HeaderText="Is Next Day?">
<ItemStyle HorizontalAlign="Center"></ItemStyle>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
<ItemTemplate>
                     <asp:Image ID="ImageIsNextDay" runat="server" SkinID="Check" Visible='<%# Convert.ToBoolean(Eval("IsNextDay")) %>' />
                        <asp:Image ID="ImageNotNextDay" runat="server" SkinID="UnCheck" Visible='<%# !Convert.ToBoolean(Eval("IsNextDay")) %>' />
                    
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Is Same Day?">
<ItemStyle HorizontalAlign="Center"></ItemStyle>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
<ItemTemplate>
                     <asp:Image ID="ImageIsSameDay" runat="server" SkinID="Check" Visible='<%# Convert.ToBoolean(Eval("IsSameDay")) %>' />
                        <asp:Image ID="ImageNotSameDay" runat="server" SkinID="UnCheck" Visible='<%# !Convert.ToBoolean(Eval("IsSameDay")) %>' />
                    
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Is Collection?">
<ItemStyle HorizontalAlign="Center"></ItemStyle>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
<ItemTemplate>
                     <asp:Image ID="ImageIsCollection" runat="server" SkinID="Check" Visible='<%# Convert.ToBoolean(Eval("IsCollection")) %>' />
                        <asp:Image ID="ImageNotCollection" runat="server" SkinID="UnCheck" Visible='<%# !Convert.ToBoolean(Eval("IsCollection")) %>' />
                    
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Is Special?">
<ItemStyle HorizontalAlign="Center"></ItemStyle>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
<ItemTemplate>
                     <asp:Image ID="ImageIsSpecial" runat="server" SkinID="Check" Visible='<%# Convert.ToBoolean(Eval("IsSpecial")) %>' />
                        <asp:Image ID="ImageNotSpecial" runat="server" SkinID="UnCheck" Visible='<%# !Convert.ToBoolean(Eval("IsSpecial")) %>' />
                    
</ItemTemplate>
</asp:TemplateField>
   <asp:TemplateField HeaderText="Warehouse">
                  <ItemTemplate>
                        <asp:Label ID="LabelWarehouseDescription" runat="server" Text='<%# Eval("Warehouse.CodeAndDescription") %>'></asp:Label>
                    
</ItemTemplate>
                </asp:TemplateField>
</Columns>
</Discovery:DiscoveryGrid> 
</contenttemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlWarehouse" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
                <asp:ObjectDataSource ID="RoutesDataSource" runat="server" SelectMethod="GetRoutes" SelectCountMethod="NumberOfRoutesCount"
        TypeName="Discovery.BusinessObjects.Controllers.RouteController" OldValuesParameterFormatString="original_{0}" EnablePaging="True">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlWarehouse"  Name="warehouseId"
                PropertyName="SelectedValue" Type="Int32" />
            <asp:Parameter Name="sortExpression" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
            <asp:Parameter DefaultValue="true" Name="fullyPopulate" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/Main.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="TrunkedStockSummary.aspx.cs" Inherits="Discovery.UI.Web.Shipments.TrunkedStockSummary"
    Title="Trunked Stock Summary" %>

<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="Discovery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <div class="PageTitle">
        Trunked Stock Summary</div>
    <hr />
     <asp:UpdatePanel runat="server" ID="UpdatePanel3">
        <contenttemplate>
    <asp:Panel ID="Panel1" runat="server" Width="800px" CssClass="collapsePanelContainer">
        <asp:Panel ID="CriteriaHeader" runat="server" CssClass="collapsePanelHeader" Width="100%">
            <span class="collapsePanelTitle">Criteria</span><span class="collapsePanelMinMax">
                <asp:Image ID="CriteriaMinMax" ImageUrl="~/Images/Container/Min.gif" runat="server" /></span>
        </asp:Panel>
        <asp:Panel ID="CriteriaContent" runat="server" CssClass="collapsePanelContent" Height="0"
            Width="100%">
            <table>
                <tr>
                    <td>
                        Delivery Location:</td>
                    <td style="width: 200px">
                        <asp:DropDownList ID="DropDownListDeliveryLocation" runat="server" Width="259px"
                            DataSourceID="WarehouseObjectDataSource" DataTextField="CodeAndDescription" DataValueField="Id"
                            OnDataBound="DropDownListDeliveryLocation_DataBound" AutoPostBack="True">
                        </asp:DropDownList><asp:ObjectDataSource ID="WarehouseObjectDataSource" runat="server"
                            SelectMethod="GetWarehouses" TypeName="Discovery.BusinessObjects.Controllers.WarehouseController"
                            OldValuesParameterFormatString="original_{0}">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="CodeAndDescription" Name="sortExpression" Type="String" />
                                <asp:Parameter DefaultValue="false" Name="fullyPopualte" Type="Boolean" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                    <td>
                        Required Date:</td>
                    <td>
                        <asp:TextBox ID="TextBoxRequiredDate" runat="server"></asp:TextBox><rjs:PopCalendar
                            ID="PopCalendarRequiredDate" Control="TextBoxRequiredDate" runat="server" RequiredDate="True"
                            RequiredDateMessage="Please enter a Required Delivery Date"></rjs:PopCalendar>
                    </td>
                </tr>
                <tr>
                    <td>
                        Route Code:</td>
                    <td>
                        <asp:DropDownList ID="DropDownListRouteCode" runat="server" Width="258px" DataSourceID="RouteCodeObjectDataSource"
                            DataTextField="Description" DataValueField="Id" OnDataBound="DropDownListRouteCode_DataBound">
                            <asp:ListItem Selected="True" Value="-1">All</asp:ListItem>
                        </asp:DropDownList><asp:ObjectDataSource ID="RouteCodeObjectDataSource" runat="server"
                            SelectMethod="GetRoutes" TypeName="Discovery.BusinessObjects.Controllers.RouteController" OldValuesParameterFormatString="original_{0}">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="DropDownListDeliveryLocation" Name="warehouseId"
                                    PropertyName="SelectedValue" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                    <td>
                    </td>
                    <td align="right">
                        <asp:ImageButton ID="ImageButtonSearch" runat="server"
                            SkinID="ButtonSearch" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>
    <!-- AJAX Extenders -->
    <ajaxToolkit:CollapsiblePanelExtender  ID="cpe" runat="Server" TargetControlID="CriteriaContent"
        ExpandControlID="CriteriaMinMax" CollapseControlID="CriteriaMinMax" Collapsed="False"
        ImageControlID="CriteriaMinMax" CollapsedSize="0" SuppressPostBack="true" ExpandedImage="~/Images/Container/Min.gif"
        CollapsedImage="~/Images/Container/Max.gif" />
    <br />
   
        <Discovery:DiscoveryGrid  DoHiLiteRow="False"  ShowFooter="True" DetailURL=""   ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" DataSourceID="ShipmentObjectDataSource" DefaultSortExpression="">
            <Columns>              
                 <asp:TemplateField HeaderText="Route Code" >
                     <itemtemplate>
                        <asp:HyperLink id="HyperLinkRouteCode" runat="server" Text='<%# Bind("RouteCodeDescription") %>' ></asp:HyperLink> 
                    
</itemtemplate>             
                 </asp:TemplateField>      
                <asp:BoundField DataField="StockWarehouseDescription" HeaderText="Stock Warehouse" />
                <asp:BoundField DataField="Weight" HeaderText="Total Net Weight (kg)"  />
                <asp:BoundField DataField="Volume" HeaderText="Volume (m&#179;)"  />       
            </Columns>
          
        </Discovery:DiscoveryGrid>
         </contenttemplate>
        <triggers>
                    <asp:AsyncPostbackTrigger ControlID="ImageButtonSearch" EventName="Click" />
                </triggers>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="ShipmentObjectDataSource" runat="server" SelectMethod="GetTrunkedStockSummary"
        TypeName="Discovery.BusinessObjects.Controllers.TDCShipmentController" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownListDeliveryLocation" DefaultValue="" Name="deliveryWarehouseId"
                PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="TextBoxRequiredDate" DefaultValue="" Name="requireDate"
                PropertyName="Text" Type="DateTime" />
            <asp:ControlParameter ControlID="DropDownListRouteCode" DefaultValue="" Name="routeCode"
                PropertyName="SelectedValue" Type="Int32" />
            <asp:Parameter DefaultValue="RouteCode,DeliveryLocation,RequiredDeliveryDate" Name="sortExpression"
                Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    &nbsp;
</asp:Content>

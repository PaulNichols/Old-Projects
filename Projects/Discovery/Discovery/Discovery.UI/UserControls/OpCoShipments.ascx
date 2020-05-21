<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OpCoShipments.ascx.cs"
    Inherits="Discovery.UI.Web.UserControls.OpCoShipmentsUserControl" %>
<%@ Register Src="../UserControls/DiscoveryStatusLegend.ascx" TagName="DiscoveryStatusLegend"
    TagPrefix="Discovery" %>
<%@ Register Src="DiscoveryPager.ascx" TagName="DiscoveryPager" TagPrefix="Discovery" %>
<%@ Register Src="~/UserControls/ShipmentCriteria.ascx" TagName="ShipmentCriteria"
    TagPrefix="Discovery" %>
<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="Discovery" %>
<!-- Script Manager Proxy Start -->
<asp:ScriptManagerProxy ID="scriptManagerProxy" runat="server" />
<!-- Script Manager Proxy End -->
<asp:Panel ID="panelContainer" runat="server">
    <Discovery:DiscoveryPagedRepeater ID="discoveryShipments" runat="server" DataSourceID="dataSourceShipments"
        SortDirection="Ascending" DefaultSortExpression="RequiredShipmentDate">
        <HeaderTemplate>
            <asp:Panel ID="panelHeader" runat="server" CssClass="pagerHeader">
                <asp:Image ID="imgHelp" runat="server" CssClass="pagerTitleIcon" ImageUrl="~/Images/Icons/24x24/Help.gif" />
                <!-- Status Legend Start -->
                <asp:Panel ID="ShipmentStatusLegend" runat="server">
                    <Discovery:DiscoveryStatusLegend ID="ctrlStatusLegend" runat="server" />
                </asp:Panel>
                <!-- Status Legend End -->
                <asp:LinkButton ID="lnkOpCo" runat="server" Width="40px" Text="OpCo" CssClass="pagerTitle"
                    CommandName="Sort" CommandArgument="OpCoCode"></asp:LinkButton>
                <asp:LinkButton ID="lnkShipmentNumber" runat="server" Text="Ship #" Width="65px"
                    CssClass="pagerTitle" CommandName="Sort" CommandArgument="ShipmentNumber"></asp:LinkButton>
                <asp:LinkButton ID="lnkDespatchNumber" runat="server" Text="Desp #" Width="65px"
                    CssClass="pagerTitle" CommandName="Sort" CommandArgument="DespatchNumber"></asp:LinkButton>
                <asp:LinkButton ID="lnkSequenceNumber" runat="server" Text="Seq #" Width="50px" CssClass="pagerTitle"
                    CommandName="Sort" CommandArgument="OpCoSequenceNumber"></asp:LinkButton>
                <asp:LinkButton ID="lnkSalesBranch" runat="server" Text="Branch" Width="50px" CssClass="pagerTitle"
                    CommandName="Sort" CommandArgument="SalesBranchCode"></asp:LinkButton>
                <asp:LinkButton ID="lnkRouteCode" runat="server" Text="Route" Width="50px" CssClass="pagerTitle"
                    CommandName="Sort" CommandArgument="RouteCode"></asp:LinkButton>
                <asp:LinkButton ID="lnkCustomerName" runat="server" Text="Customer" Width="200px"
                    CssClass="pagerTitle" CommandName="Sort" CommandArgument="CustomerName"></asp:LinkButton>
                <asp:LinkButton ID="lnkRequiredDelivery" runat="server" Text="Req. Date" Width="65px"
                    CssClass="pagerTitle" CommandName="Sort" CommandArgument="RequiredShipmentDate"></asp:LinkButton>
                <asp:LinkButton ID="lnkStockWarehouse" runat="server" Text="Stock loc" Width="65px"
                    CssClass="pagerTitle" CommandName="Sort" CommandArgument="StockWarehouseCode"></asp:LinkButton>
                <asp:LinkButton ID="lnkDeliveryWarehouse" runat="server" Text="Del. Loc" Width="65px"
                    CssClass="pagerTitle" CommandName="Sort" CommandArgument="DeliveryWarehouseCode"></asp:LinkButton>
                <asp:LinkButton ID="lnkGrossWeight" runat="server" Text="Gross" Width="50px" CssClass="pagerTitle" CommandName="Sort" CommandArgument="NetWeight"></asp:LinkButton>
                <asp:Label ID="lnkLines" runat="server" Text="Lines" Width="40px" CssClass="pagerTitle"></asp:Label>
                <!-- Status Extender Start -->
                <ajaxToolkit:HoverMenuExtender ID="ShipmentHelpExtender" runat="server" OffsetX="0"
                    OffsetY="0" TargetControlID="imgHelp" PopupControlID="ShipmentStatusLegend" PopupPosition="Right" />
                <!-- Status Extender End -->
            </asp:Panel>
        </HeaderTemplate>
        <ItemTemplate>
            <asp:Panel CssClass="shipmentHeaderNormal" ID="panelHeader" runat="server">
                <image border="0" src='../Images/Icons/24x24/<%# Eval("Status") %>.gif' class="accordionTitleIcon"></image>
                <asp:Label ID="lblOpCo" runat="server" CssClass="accordionPanelTitle" Width="40px"
                    Text='<%# Eval("OpCoCode") %>' />
                <asp:Label ID="lblShipmentNumber" runat="server" style="font-weight:bold;" CssClass="accordionPanelTitle" Width="65px" Text='<%# Eval("ShipmentNumber") %>' />
                <asp:Label ID="lblDespatchNumber" runat="server" CssClass="accordionPanelTitle" Width="65px" Text='<%# Eval("DespatchNumber") %>' />
                <asp:Label ID="lblSequenceNumber" runat="server" CssClass="accordionPanelTitle" Width="50px" Text='<%# Eval("OpCoSequenceNumber") %>' />
                <asp:Label ID="lblSalesBranch" runat="server" CssClass="accordionPanelTitle" Width="50px" Text='<%# Eval("SalesBranchCode") %>' />
                <asp:Label ID="lblRouteCodeHdr" runat="server" CssClass="accordionPanelTitle" Width="50px" Text='<%# Eval("RouteCode") %>' />
                <asp:Label ID="lblCustomerName" runat="server" CssClass="accordionPanelTitle" Width="200px" Text='<%# Eval("CustomerName") %>' />
                <asp:Label ID="lblRequiredDelivery" runat="server" CssClass="accordionPanelTitle" Width="65px" Text='<%# Eval("RequiredShipmentDate", "{0:d}")%>' />
                <asp:Label ID="lblStockWarehouse" runat="server" CssClass="accordionPanelTitle" Width="65px" Text='<%# Eval("StockWarehouseCode") %>' />
                <asp:Label ID="lblDeliveryWarehouse" runat="server" CssClass="accordionPanelTitle" Width="65px" Text='<%# Eval("DeliveryWarehouseCode") %>' />
                <asp:Label ID="lblGrossWeight" runat="server" CssClass="accordionPanelTitle" Width="50px" Text='<%# Eval("GrossWeight", "{0:####00.00}") %>' />
                <asp:Label ID="lblLineQty" runat="server" style="text-align:center" CssClass="accordionPanelTitle" Width="40px" Text='<%# Eval("ShipmentLines.Count") %>' />
            </asp:Panel>
            <asp:Panel ID="panelContent" runat="server" CssClass="shipmentContent">
                <fieldset><legend>Shipment Details</legend>
                <table border="0" cellpadding="2" width="100%">
                    <tr>
                        <td valign="top" style="width: 33%">
                            <table cellpadding="2" border="0" width="100%">
                                <tr>
                                    <td nowrap="true">
                                        Customer Number:</td>
                                    <td>
                                        <asp:Label ID="lblCustomerNumber" runat="server"><%# Eval("CustomerNumber") %></asp:Label></td>
                                </tr>
                                <tr>
                                    <td nowrap="true">
                                        Customer Reference:</td>
                                    <td>
                                        <asp:Label ID="lblCustomerReference" runat="server"><%# Eval("CustomerReference") %></asp:Label></td>
                                </tr>
                                <tr>
                                    <td nowrap="true">
                                        OpCo Contact:</td>
                                    <td width="100%">
                                        <asp:Label ID="lblOpCoContactName" runat="server"><%# Eval("OpCoContact.Name") %></asp:Label></td>
                                </tr>
                                <tr>
                                    <td nowrap="true">
                                        OpCo Email:</td>
                                    <td>
                                        <asp:Label ID="lblOpCoContactEmail" runat="server"><%# Discovery.Utility.UIHelper.GenerateEmailUrl(Eval("OpCoContact.Email").ToString()) %></asp:Label></td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top" style="width: 33%">
                            <table cellpadding="2" border="0" width="100%">
                                <tr>
                                    <td nowrap="true">
                                        Shipment Name:</td>
                                    <td width="100%">
                                        <asp:Label ID="lblShipmentName" runat="server"><%# Eval("ShipmentName")%></asp:Label></td>
                                </tr>
                                <tr>
                                    <td nowrap="true">
                                        Shipment Number:</td>
                                    <td>
                                        <asp:Label ID="LabelShipmentNumber" runat="server"><%# Eval("ShipmentNumber")%></asp:Label></td>
                                </tr>
                                <tr>
                                    <td nowrap="true">
                                        Shipment Contact:</td>
                                    <td>
                                        <asp:Label ID="lblShipmentContactName" runat="server"><%# Eval("ShipmentContact.Name")%></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="true">
                                        Shipment Email:</td>
                                    <td>
                                        <asp:Label ID="lblShipmentContactEmail" runat="server"><%# Discovery.Utility.UIHelper.GenerateEmailUrl(Eval("ShipmentContact.Email").ToString())%></asp:Label></td>
                                </tr>
                                <tr>
                                    <td nowrap="true">
                                        Shipment Telephone:</td>
                                    <td>
                                        <asp:Label ID="lblShipmentContactTelephone" runat="server"><%# Eval("ShipmentContact.TelephoneNumber")%></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top" style="width: 33%">
                            <table cellpadding="2" border="0" width="100%">
                                <tr>
                                    <td nowrap="true">
                                        Division:</td>
                                    <td width="100%">
                                        <asp:Label ID="lblDivisionCode" runat="server"><%# Eval("DivisionCode")%></asp:Label></td>
                                </tr>
                                <tr>
                                    <td nowrap="true">
                                        Transaction Type:</td>
                                    <td>
                                        <asp:Label ID="lblTransactionType" runat="server"><%# Eval("TransactionTypeCode")%></asp:Label></td>
                                </tr>
                                <tr>
                                    <td nowrap="true">
                                        Route Code:</td>
                                    <td>
                                        <asp:Label ID="lblRouteCode" runat="server"><%# Eval("RouteCode")%></asp:Label></td>
                                </tr>
                                <tr>
                                    <td nowrap="true">
                                        Despatch #:</td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server"><%# Eval("DespatchNumber")%></asp:Label></td>
                                </tr>
                                <tr>
                                    <td nowrap="true">
                                        Sequence #:
                                    </td>
                                    <td>
                                        <asp:Label ID="Label3" runat="server"><%# Eval("OpCoSequenceNumber")%></asp:Label></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                </fieldset>                
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td width="50%" valign="top">
                            <fieldset>
                                <legend>Instructions</legend>
                                    <asp:Label ID="lblInstructions" runat="server" CssClass="Instructions"><%# Eval("Instructions")%>&nbsp;</asp:Label>
                            </fieldset>
                        </td>
                        <td width="50%" valign="top">
                            <fieldset>
                                <legend>Options</legend>
                                <asp:ImageButton ID="btnOpCoShipmentDetail" SkinID="ButtonOpCoShipment" CommandName="OpCoDetail"
                                    CommandArgument='<%# Eval("Id") %>' runat="server" ToolTip="Displays OpCo shipment details" />
                                <asp:ImageButton ID="btnTDCShipmentDetails" SkinID="ButtonTDCShipment" CommandName="TDCDetail"
                                    CommandArgument='<%# string.Concat(Eval("OpCoCode"), "|", Eval("ShipmentNumber"), "|", Eval("DespatchNumber")) %>'
                                    runat="server" ToolTip="Displays TDC shipment details" Visible='<%# Eval("Status").ToString() != "NotMapped" %>' />
                                <asp:ImageButton ID="btnAuditDetail" SkinID="ButtonAuditDetails" CommandName="AuditDetail"
                                    CommandArgument='<%# Eval("AuditId") %>' runat="server" ToolTip="Displays the audit entry for this shipment"
                                    Visible='<%# ((int) Eval("AuditId")) != Discovery.Utility.Null.NullInteger %>' />
                                <asp:ImageButton ID="btnMapToTdc" SkinID="ButtonMapToTDC" CommandName="MapToTdc"
                                    CommandArgument='<%# Eval("Id") %>' runat="server" ToolTip="Map to TDC Shipment"
                                    Visible='<%# Eval("Status").ToString() == "NotMapped" %>' />
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <!-- Collapsible Panel Start -->
            <ajaxToolkit:CollapsiblePanelExtender ID="ShipmentCPE" runat="Server" TargetControlID="panelContent"
                CollapsedSize="0" ExpandedSize="210" Collapsed="True" ExpandControlID="panelHeader"
                CollapseControlID="panelHeader" AutoCollapse="False" AutoExpand="False" ScrollContents="False"
                ExpandDirection="Vertical" />
            <!-- Collapsible Panel End -->
        </ItemTemplate>
        <FooterTemplate>
        </FooterTemplate>
    </Discovery:DiscoveryPagedRepeater>
</asp:Panel>
<!-- Discovery Pager -->
<Discovery:DiscoveryPager ID="discoveryPager" runat="server" PagesToDisplay="10"
    PageSize="10" />

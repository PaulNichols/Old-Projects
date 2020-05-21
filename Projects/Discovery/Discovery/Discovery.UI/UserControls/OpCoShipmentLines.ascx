<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OpCoShipmentLines.ascx.cs"
    Inherits="Discovery.UI.Web.UserControls.OpCoShipmentLines" %>
<%@ Register Src="../UserControls/DiscoveryStatusLegend.ascx" TagName="DiscoveryStatusLegend"
    TagPrefix="Discovery" %>
<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="Discovery" %>
<asp:UpdatePanel ID="updatePanelShipmentLines" runat="server">
    <ContentTemplate>
        <div id="ShipmentLineAccordion" runat="server" style="width: 990px;">
            <Discovery:DiscoveryPagedRepeater ID="discoveryShipmentLines" runat="server" DataSourceID="dataSourceShipmentLines"
                DefaultSortExpression="LineNumber">
                <HeaderTemplate>
                    <asp:Panel ID="panelHeader" runat="server" CssClass="pagerHeader">
                        <asp:LinkButton ID="lnkLineNumber" runat="server" Width="20px" Text="#" CssClass="pagerTitle"
                            CommandName="Sort" CommandArgument="LineNumber"></asp:LinkButton>
                        <asp:LinkButton ID="lnkProductCode" runat="server" Text="Code" Width="90px" CssClass="pagerTitle"
                            CommandName="Sort" CommandArgument="ProductCode"></asp:LinkButton>
                        <asp:LinkButton ID="lnkDescription1" runat="server" Text="Decription" Width="200px"
                            CssClass="pagerTitle" CommandName="Sort" CommandArgument="Description1"></asp:LinkButton>
                        <asp:LinkButton ID="lnkQuantity" runat="server" Text="Qty" Width="40px" CssClass="pagerTitle"
                            CommandName="Sort" CommandArgument="Quantity"></asp:LinkButton>
                        <asp:LinkButton ID="lnkQuantityUnit" runat="server" Text="Unit" Width="200px" CssClass="pagerTitle"
                            CommandName="Sort" CommandArgument="QuantityUnit"></asp:LinkButton>
                        <asp:LinkButton ID="lnkWidth" runat="server" Text="Width" Width="50px" CssClass="pagerTitle"
                            CommandName="Sort" CommandArgument="Width"></asp:LinkButton>
                        <asp:LinkButton ID="lnkLength" runat="server" Text="Length" Width="50px" CssClass="pagerTitle"
                            CommandName="Sort" CommandArgument="Length"></asp:LinkButton>
                        <asp:LinkButton ID="lnkVolume" runat="server" Text="Volume" Width="50px" CssClass="pagerTitle"
                            CommandName="Sort" CommandArgument="Volume"></asp:LinkButton>
                        <asp:LinkButton ID="lnkNetWeight" runat="server" Text="Net" Width="50px" CssClass="pagerTitle"
                            CommandName="Sort" CommandArgument="NetWeight"></asp:LinkButton>
                        <asp:LinkButton ID="lnkGrossWeight" runat="server" Text="Gross" Width="50px" CssClass="pagerTitle"
                            CommandName="Sort" CommandArgument="NetWeight"></asp:LinkButton>
                    </asp:Panel>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Panel CssClass="shipmentLineHeaderNormal" ID="panelHeader" runat="server">
                        <asp:Label ID="lblHdrLineNumber" runat="server" CssClass="accordionPanelTitle" Width="20px"><%# Eval("LineNumber") %></asp:Label>
                        <asp:Label ID="lblHdrProductCode" runat="server" CssClass="accordionPanelTitle" Width="90px"><%# Eval("ProductCode") %></asp:Label>
                        <asp:Label ID="lblHdrDescription1" runat="server" CssClass="accordionPanelTitle"
                            Width="200px"><%# Eval("Description1") %></asp:Label>
                        <asp:Label ID="lblHdrQuantity" runat="server" CssClass="accordionPanelTitle" Width="40px"><%# Eval("Quantity") %></asp:Label>
                        <asp:Label ID="lblHdrQuantityUnit" runat="server" CssClass="accordionPanelTitle"
                            Width="200px"><%# Eval("QuantityUnit") %></asp:Label>
                        <asp:Label ID="lblHdrWidth" runat="server" CssClass="accordionPanelTitle" Width="50px"><%# Eval("Width", "{0:d}") %></asp:Label>
                        <asp:Label ID="lblHdrLength" runat="server" CssClass="accordionPanelTitle" Width="50px"><%# Eval("Length", "{0:d}")%></asp:Label>
                        <asp:Label ID="lblHdrVolume" runat="server" CssClass="accordionPanelTitle" Width="50px"><%# Eval("Volume") %></asp:Label>
                        <asp:Label ID="lblHdrNetWeight" runat="server" CssClass="accordionPanelTitle" Width="50px"><%# Eval("NetWeight", "{0:F2}") %></asp:Label>
                        <asp:Label ID="lblHdrGrossWeight" runat="server" CssClass="accordionPanelTitle" Width="50px"><%# Eval("GrossWeight", "{0:F2}") %></asp:Label>
                    </asp:Panel>
                    <asp:Panel ID="panelContent" runat="server" CssClass="shipmentLineContent">
                            <table id="test" border="0" cellpadding="2" width="100%">
                                <tr>
                                    <td valign="top" style="width: 40%">
                                        <table cellpadding="2" border="0" width="100%">
                                            <tr>
                                                <td valign="top">
                                                    <fieldset>
                                                        <legend>Description</legend>
                                                        <asp:Label ID="Label2" runat="server" CssClass="Instructions"><%# Eval("Description1") %></asp:Label>&nbsp;
                                                    </fieldset>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    <fieldset>
                                                        <legend>Additional Description</legend>
                                                        <asp:Label ID="LabelDescription2" runat="server" CssClass="Instructions"><%# Eval("Description2") %></asp:Label>&nbsp;
                                                    </fieldset>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    <fieldset>
                                                        <legend>Conversion Instructions</legend>
                                                        <asp:Label ID="lblConversionInstructions" runat="server" CssClass="Instructions"><%# Eval("ConversionInstructions") %></asp:Label>&nbsp;
                                                    </fieldset>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <fieldset>
                                                        <legend>Packing</legend>
                                                        <asp:Label ID="lblPacking" runat="server"><%# Eval("Packing") %></asp:Label>&nbsp;
                                                    </fieldset>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="top" style="width: 30%">
                                        <fieldset>
                                            <legend>Additional Info</legend>
                                            <table cellpadding="2" border="0" width="100%">
                                                <tr>
                                                    <td nowrap="true">
                                                        Product Code:</td>
                                                    <td width="100%">
                                                        <asp:Label ID="lblProductCode" runat="server"><%# Eval("ProductCode")%></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="true">
                                                        Quantity:</td>
                                                    <td width="100%">
                                                        <asp:Label ID="lblQuantity" runat="server"><%# Eval("Quantity")%></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="true">
                                                        Quantity Unit:</td>
                                                    <td width="100%">
                                                        <asp:Label ID="lblQuantityUnit" runat="server"><%# Eval("QuantityUnit")%></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="true">
                                                        Customer Reference:</td>
                                                    <td width="100%">
                                                        <asp:Label ID="lblCustomerReference" runat="server"><%# Eval("CustomerReference")%></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="true">
                                                        Product Group:</td>
                                                    <td width="100%">
                                                        <asp:Label ID="Label9" runat="server"><%# Eval("ProductGroup", "{0:0}")%></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="true">
                                                        Conversion Qty:</td>
                                                    <td width="100%">
                                                        <asp:Label ID="Label3" runat="server"><%# Eval("ConversionQuantity", "{0:0}")%></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="true">
                                                        Is Panel:</td>
                                                    <td>
                                                        <asp:Image ID="imgIsPanel" runat="server" ImageUrl='<%# "~/Images/" + Eval("IsPanel") + ".gif" %>' /></td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="true">
                                                        ISO 9000 Approved:</td>
                                                    <td>
                                                        <asp:Image ID="imgIsISO9000Approved" runat="server" ImageUrl='<%# "~/Images/" + Eval("IsISO9000Approved") + ".gif" %>' /></td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                    <td valign="top" style="width: 30%">
                                        <fieldset>
                                            <legend>Weight and Measurements</legend>
                                            <table cellpadding="2" border="0" width="100%">
                                                <tr>
                                                    <td nowrap="true">
                                                        Load Category:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label6" runat="server"><%# Eval("LoadCategoryCode")%></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="true">
                                                        Net Weight:</td>
                                                    <td width="100%">
                                                        <asp:Label ID="lblNetWeight" runat="server"><%# Eval("NetWeight", "{0:F}")%></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="true">
                                                        Width:</td>
                                                    <td width="100%">
                                                        <asp:Label ID="lblWidth" runat="server"><%# Eval("Width", "{0:0}")%></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="true">
                                                        Length:</td>
                                                    <td>
                                                        <asp:Label ID="lblLength" runat="server"><%# Eval("Length", "{0:0}")%></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="true">
                                                        Volume:</td>
                                                    <td>
                                                        <asp:Label ID="lblVolume" runat="server"><%# Eval("Volume", "{0:F}")%></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="true">
                                                        Microns:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblMicrons" runat="server"><%# Eval("Microns", "{0:0}") %></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="true">
                                                        Grammage:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label7" runat="server"><%# Eval("Grammage", "{0:0}") %></asp:Label></td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                    </asp:Panel>
                    <!-- AJAX EXTENDERS -->
                    <ajaxToolkit:CollapsiblePanelExtender ID="collapsiblePanelShipmentLines" runat="Server"
                        TargetControlID="panelContent" CollapsedSize="0" ExpandControlID="panelHeader"
                        CollapseControlID="panelHeader" AutoCollapse="False" AutoExpand="False" ScrollContents="False"
                        ExpandDirection="Vertical" Collapsed="true" />
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </Discovery:DiscoveryPagedRepeater>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:ObjectDataSource ID="dataSourceShipmentLines" runat="server" SelectMethod="GetLines"
    TypeName="Discovery.BusinessObjects.Controllers.OpCoShipmentLineController" MaximumRowsParameterName=""
    OldValuesParameterFormatString="original_{0}" SortParameterName="sortExpression"
    StartRowIndexParameterName="">
    <SelectParameters>
        <asp:QueryStringParameter DefaultValue="-1" Name="shipmentId" QueryStringField="Id"
            Type="Int32" />
        <asp:Parameter Name="sortExpression" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>

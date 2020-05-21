<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TDCShipmentLines.ascx.cs"
    Inherits="Discovery.UI.Web.UserControls.TDCShipmentLines" %>
<%@ Register Src="../UserControls/DiscoveryStatusLegend.ascx" TagName="DiscoveryStatusLegend"
    TagPrefix="Discovery" %>
<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="Discovery" %>
<!-- Ajax update panel start -->
        <Discovery:DiscoveryPagedRepeater ID="discoveryShipmentLines" runat="server" DataSourceID="dataSourceShipmentLines"
            DefaultSortExpression="LineNumber" OnItemDataBound="discoveryShipmentLines_ItemDataBound">
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
                <!-- Shipment Line Content Start -->
                <asp:Panel ID="panelContent" runat="server">
                    <asp:FormView ID="TDCShipmentLineFormView" runat="server" AllowPaging="false" CssClass="shipmentLineContent"
                        DataKeyNames="Id" OnDataBound="TDCShipmentLineFormView_OnDataBound" OnItemCommand="TDCShipmentLineFormView_OnItemCommand"
                        OnModeChanging="TDCShipmentLineFormView_OnModeChanging" SkinID="FormViewNone"
                        Width="100%">
                        <ItemTemplate>
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
                                                        <asp:Label ID="Label6" runat="server"><%# Eval("LoadCategory.Description") %></asp:Label></td>
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
                                        <fieldset>
                                            <legend>Options</legend>
                                            <table cellpadding="2" border="0" width="100%">
                                                <tr>
                                                    <td nowrap="true" colspan="2">
                                                        <asp:ImageButton ID="ButtonEdit" runat="server" SkinID="ButtonEdit" CommandName="Edit">
                                                        </asp:ImageButton>&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <table border="0" cellpadding="0" width="100%">
                                <tr>
                                    <td valign="top" style="width: 40%" rowspan="2">
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                            <tr>
                                                <td valign="top">
                                                    <fieldset>
                                                        <legend>Description</legend>
                                                        <asp:TextBox MaxLength="50" TextMode="SingleLine" Width="330px" ID="txtDescription1"
                                                            runat="server" CssClass="EditText" Text='<%# Eval("Description1") %>'></asp:TextBox></fieldset>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    <fieldset>
                                                        <legend>Description</legend>
                                                        <asp:TextBox MaxLength="50" TextMode="SingleLine" Width="330px" ID="txtDescription2"
                                                            runat="server" CssClass="EditText" Text='<%# Eval("Description2") %>'></asp:TextBox></fieldset>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    <fieldset>
                                                        <legend>Conversion Instructions</legend>
                                                        <asp:TextBox MaxLength="50" TextMode="MultiLine" Width="330px" Height="50" ID="txtConversionInstructions"
                                                            runat="server" CssClass="EditText" Text='<%# Eval("ConversionInstructions") %>'></asp:TextBox></fieldset>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <fieldset>
                                                        <legend>Packing</legend>
                                                        <asp:TextBox MaxLength="50" TextMode="MultiLine" Width="330px" Height="50" ID="txtPacking"
                                                            runat="server" CssClass="EditText" Text='<%# Eval("Packing") %>'></asp:TextBox></fieldset>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="top" style="width: 30%" rowspan="2">
                                        <fieldset>
                                            <legend>Additional Information</legend>
                                            <table cellpadding="2" border="0" width="100%">
                                                <tr>
                                                    <td nowrap="true">
                                                        Product Code:</td>
                                                    <td width="100%">
                                                        <asp:TextBox ID="txtProductCode" runat="server" CssClass="EditText" Text='<%# Eval("ProductCode")%>'></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="true">
                                                        Quantity:</td>
                                                    <td width="100%">
                                                        <Discovery:DiscoveryNumericText ID="txtQuantity" runat="server" CssClass="EditText"
                                                            Text='<%# Eval("Quantity")%>'></Discovery:DiscoveryNumericText></td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="true">
                                                        Quantity Unit:</td>
                                                    <td width="100%">
                                                        <asp:TextBox ID="txtQuantityUnit" runat="server" CssClass="EditText" Text='<%# Eval("QuantityUnit")%>'></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="true">
                                                        Customer Reference:</td>
                                                    <td width="100%">
                                                        <asp:TextBox ID="txtCustomerReference" runat="server" CssClass="EditText" Text='<%# Eval("CustomerReference")%>'></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="true">
                                                        Product Group:</td>
                                                    <td width="100%">
                                                        <asp:TextBox ID="txtProductGroup" runat="server" CssClass="EditText" Text='<%# Eval("ProductGroup", "{0:0}")%>'></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="true">
                                                        Conversion Qty:</td>
                                                    <td width="100%">
                                                        <Discovery:DiscoveryNumericText ID="txtConversionQuantity" runat="server" CssClass="EditText"
                                                            Text='<%# Eval("ConversionQuantity", "{0:0}")%>'></Discovery:DiscoveryNumericText></td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="true">
                                                        Is Panel:</td>
                                                    <td>
                                                        <asp:CheckBox ID="chkIsPanel" runat="server" Checked='<%# Eval("IsPanel") %>' />
                                                </tr>
                                                <tr>
                                                    <td nowrap="true">
                                                        ISO 9000 Approved:</td>
                                                    <td>
                                                        <asp:CheckBox ID="chkIsISO9000Approved" runat="server" Checked='<%# Eval("IsISO9000Approved") %>' />
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
                                                        <asp:DropDownList ID="ddlLoadCategory" runat="server" CssClass="EditText" DataTextField="Description"
                                                            DataValueField="Code" DataSourceID="dataSourceLoadCategory" SelectedValue='<%# Bind("LoadCategoryCode") %>'>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="true">
                                                        Net Weight:</td>
                                                    <td width="100%">
                                                        <Discovery:DiscoveryNumericText ID="txtNetWeight" runat="server" MaxLength="6" Width="50"
                                                            CssClass="EditText" Text='<%# Eval("NetWeight", "{0:F}")%>'></Discovery:DiscoveryNumericText></td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="true">
                                                        Width:</td>
                                                    <td width="100%">
                                                        <Discovery:DiscoveryNumericText ID="txtWidth" runat="server" MaxLength="6" Width="50"
                                                            CssClass="EditText" Text='<%# Eval("Width", "{0:0}")%>'></Discovery:DiscoveryNumericText></td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="true">
                                                        Length:</td>
                                                    <td>
                                                        <Discovery:DiscoveryNumericText ID="txtLength" runat="server" MaxLength="6" Width="50"
                                                            CssClass="EditText" Text='<%# Eval("Length", "{0:0}")%>'></Discovery:DiscoveryNumericText></td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="true">
                                                        Volume:</td>
                                                    <td>
                                                        <Discovery:DiscoveryNumericText ID="txtVolume" runat="server" MaxLength="6" Width="50"
                                                            CssClass="EditText" Text='<%# Eval("Volume", "{0:F}")%>'></Discovery:DiscoveryNumericText></td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="true">
                                                        Microns:
                                                    </td>
                                                    <td>
                                                        <Discovery:DiscoveryNumericText ID="txtMicrons" runat="server" MaxLength="6" Width="50"
                                                            CssClass="EditText" Text='<%# Eval("Microns", "{0:0}") %>'></Discovery:DiscoveryNumericText></td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="true">
                                                        Grammage:
                                                    </td>
                                                    <td>
                                                        <Discovery:DiscoveryNumericText ID="txtGrammage" runat="server" MaxLength="6" Width="50"
                                                            CssClass="EditText" Text='<%# Eval("Grammage", "{0:0}") %>'></Discovery:DiscoveryNumericText></td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <fieldset>
                                            <legend>Options</legend>
                                            <asp:ImageButton ID="ButtonSave" runat="server" CausesValidation="False" CommandName="Save"
                                                SkinID="ButtonSave" />&nbsp;&nbsp;<asp:ImageButton ID="ButtonUpdateCancel" runat="server"
                                                    CausesValidation="False" CommandName="Cancel" SkinID="ButtonCancel" /><asp:HiddenField
                                                        ID="txtChecksum" runat="server" Value='<%# Eval("Checksum") %>' />
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                        </EditItemTemplate>
                    </asp:FormView>
                </asp:Panel>
                <!-- Shipment Line Content End -->
                <!-- Load Category Code Data Source -->
                <asp:ObjectDataSource ID="dataSourceLoadCategory" runat="server" SelectMethod="GetLoadCategories"
                    TypeName="Discovery.BusinessObjects.Controllers.LoadCategoryController" OldValuesParameterFormatString="original_{0}" />
                <!-- AJAX EXTENDERS -->
                <ajaxToolkit:CollapsiblePanelExtender ID="collapsiblePanelShipmentLines" runat="Server"
                    TargetControlID="panelContent" CollapsedSize="0" Collapsed='<%# (SelectedItemIndex != Convert.ToInt32(Eval("Id"))) %>'
                    ExpandControlID="panelHeader" CollapseControlID="panelHeader" AutoCollapse="False"
                    AutoExpand="False" ScrollContents="False" ExpandDirection="Vertical" />
            </ItemTemplate>
            <FooterTemplate>
            </FooterTemplate>
        </Discovery:DiscoveryPagedRepeater>
<!-- Ajax update panel end -->
<!-- Object Data Source -->
<asp:ObjectDataSource ID="dataSourceShipmentLines" runat="server" SelectMethod="GetLines"
    TypeName="Discovery.BusinessObjects.Controllers.TDCShipmentLineController" MaximumRowsParameterName=""
    OldValuesParameterFormatString="original_{0}" SortParameterName="sortExpression"
    StartRowIndexParameterName="">
    <SelectParameters>
        <asp:QueryStringParameter DefaultValue="-1" Name="shipmentId" QueryStringField="Id"
            Type="Int32" />
        <asp:Parameter Name="sortExpression" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>

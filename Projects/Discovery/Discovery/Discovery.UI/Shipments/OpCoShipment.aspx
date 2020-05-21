<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="OpCoShipment.aspx.cs"
    Inherits="Discovery.UI.Web.Shipments.OpCoShipment" Title="OpCo Shipment" %>

<%@ Register Src="~/UserControls/OpCoShipmentLines.ascx" TagName="OpCoShipmentLines"
    TagPrefix="Discovery" %>
<%@ Import Namespace="System.Web.UI.WebControls" %>
<%@ Import Namespace="System.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <asp:HyperLink ID="HyperLinkBack" SkinID="HyperLinkBack" runat="server">Back</asp:HyperLink>
    <!-- AJAX Update Panel Start -->
    <asp:UpdatePanel ID="shipment" runat="server">
        <contenttemplate>
    <!-- Form view for Op Co Shipment Start -->
    <asp:FormView SkinID="FormViewNone" ID="OpCoShipmentFormView" OnDataBound="OpCoShipmentFormView_OnDataBound" runat="server" DataSourceID="OpCoShipmentDataSource">
        <ItemTemplate>
            <asp:Panel ID="panelShipmentDetail" runat="server" CssClass="collapsePanelContainer"
                Width="990px">
                <asp:Panel ID="panelShipmentHeader" runat="server" CssClass="collapsePanelHeader"
                    Width="100%">
                    <span class="collapsePanelTitle">OpCo Shipment Details (<%# string.Concat(Eval("OpCoCode"), "-", Eval("ShipmentNumber"), "-", Eval("DespatchNumber"), " ", Eval("CustomerName")) %>)</span><span
                        class="collapsePanelMinMax">
                        <asp:Image ID="imgShipmentMinMax" ImageUrl="~/Images/Container/Min.gif" runat="server" /></span>
                </asp:Panel>
                <asp:Panel ID="panelShipmentContent" runat="server" CssClass="collapsePanelContent" Width="100%">
                    <fieldset>
                       <legend>Options</legend>
                                <asp:ImageButton ID="btnTDCShipmentDetails" runat="server" SkinID="ButtonTDCShipment" CommandArgument='<%# string.Concat(Eval("OpCoCode"), "|", Eval("ShipmentNumber"), "|", Eval("DespatchNumber")) %>' Visible='<%# Eval("Status").ToString() != "NotMapped" %>' OnClick="btnTDCShipmentDetails_Click" ToolTip="Displays TDC shipment details" />
                                <asp:ImageButton ID="btnAuditDetail" SKinID="ButtonAuditDetails" runat="server" CommandArgument='<%# Eval("AuditId") %>' CommandName="AuditDetail" 
                                OnClick="btnAuditDetail_Click" ToolTip="Displays the audit entry for this shipment" />
                                <asp:ImageButton ID="btnMapToTdc" SkinID="ButtonMapToTDC" CommandArgument='<%# Eval("Id") %>' runat="server" ToolTip="Map to TDC Shipment" OnClick="btnMapToTdc_Click" Visible='<%# Eval("Status").ToString() == "NotMapped" %>' />                                
                    </fieldset>
                    
                    <fieldset>
                        <legend>Customer Details</legend>
                        <table border="0" cellspacing="0" cellpadding="2" width="100%">
                            <tr>
                                <td class="shipmentLabel">
                                    Number:</td>
                                <td class="shipmentValue">
                                    <asp:Label ID="lblCustomerNumber" runat="server" Text='<%# Eval("CustomerNumber") %>'></asp:Label></td>
                                <td class="shipmentLabel">
                                    Reference:</td>
                                <td class="shipmentValue">
                                    <asp:Label ID="lblCustomerReference" runat="server" Text='<%# Eval("CustomerReference") %>'></asp:Label></td>
                                <td class="shipmentLabel">
                                    Name:</td>
                                <td>
                                    <asp:Label ID="lblCustomerName" runat="server" Text='<%# Eval("CustomerName") %>'></asp:Label></td>
                            </tr>
                        </table>
                    </fieldset>
                    <fieldset>
                        <legend>Shipment Details</legend>
                        <table border="0" cellspacing="0" cellpadding="2" width="100%">
                            <tr>
                                <td class="shipmentLabel">
                                    Op Co:</td>
                                <td class="shipmentValue">
                                    <asp:Label ID="lblOpCoCode" runat="server" Text='<%# Eval("OpCoCode") %>'></asp:Label></td>
                                <td class="shipmentLabel">
                                    Number:</td>
                                <td class="shipmentValue">
                                    <asp:Label ID="lblShipmentNumber" runat="server" Text='<%# Eval("ShipmentNumber") %>'></asp:Label></td>
                                <td class="shipmentLabel">
                                    Despatch #:</td>
                                <td class="shipmentValue">
                                    <asp:Label ID="lblDespatchNumber" runat="server" Text='<%# Eval("DespatchNumber") %>'></asp:Label></td>
                                <td class="shipmentLabel">
                                    Status:</td>
                                <td>
                                    <asp:Image ID="imgStatus" runat="server" ImageUrl='<%# "~/Images/Icons/16x16/" + Eval("Status") + ".gif" %>'
                                        ToolTip='<%# Eval("Status") %>' /></td>
                            </tr>
                            <tr>
                                <td>
                                    Sequence #:</td>
                                <td>
                                    <asp:Label ID="lblOpCoSequenceNumber" runat="server" Text='<%# Eval("OpCoSequenceNumber") %>'></asp:Label></td>
                                <td>
                                    Division Code:</td>
                                <td>
                                    <asp:Label ID="lblDivision" runat="server" Text='<%# Eval("DivisionCode") %>'></asp:Label></td>
                                <td>
                                    Route Code:</td>
                                <td>
                                    <asp:Label ID="lblRouteCode" runat="server" Text='<%# Eval("RouteCode") %>'></asp:Label></td>
                                <td  class="shipmentLabel">Sales Branch:</td>
                                <td>
                                    <asp:Label ID="lblSalesBranchCode" runat="server" Text='<%# Eval("SalesBranchCode") %>'></asp:Label></td>
                            </tr>
                            <tr>
                                <td >Transaction Type:</td>
                                <td>
                                    <asp:Label ID="lblTransactionType" runat="server" Text='<%# Eval("TransactionTypeCode") %>'></asp:Label></td>
                                <td>Generated:</td>
                                <td>
                                    <asp:Label ID="lblGeneratedDateTime" runat="server" Text='<%# Eval("GeneratedDateTime") %>'></asp:Label></td>
                                 <td>&nbsp;</td>
                                 <td>&nbsp;</td>
                                 <td>&nbsp;</td>
                                 <td>&nbsp;</td>
                            </tr>
                        </table>
                     </fieldset>
                                
                        <fieldset><legend>Delivery Details</legend>
                        <table border="0" cellspacing="0" cellpadding="2" width="100%">
                                    <tr>
                                        <td class="shipmentLabel">Stock W/house:</td>
                                        <td class="shipmentValue">
                                            <asp:Label ID="lblStockWareHouseCode" runat="server" Text='<%# Eval("StockWarehouseCode") %>'></asp:Label></td>
                                        <td class="shipmentLabel">Delivery W/house:</td>
                                        <td class="shipmentValue">
                                            <asp:Label ID="lblDeliveryWarehouseCode" runat="server" Text='<%# Eval("DeliveryWarehouseCode") %>'></asp:Label></td>
                                        <td class="shipmentLabel">&nbsp;</td>
                                        <td class="shipmentValue">&nbsp;</td>
                                        <td class="shipmentLabel">&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                            <tr>
                                <td>
                                    Required Date:</td>
                                <td>
                                    <asp:Label ID="lblRequiredDeliveryDate" runat="server" Text='<%# Eval("RequiredShipmentDate", "{0:d}") %>'></asp:Label></td>
                                <td>
                                    After Time:</td>
                                <td>
                                    <asp:Label ID="lblAfterTime" runat="server" Text='<%# Eval("AfterTime") %>'></asp:Label></td>
                                <td>
                                    Before Time:</td>
                                <td>
                                    <asp:Label ID="lblBeforeTime" runat="server" Text='<%# Eval("BeforeTime") %>'></asp:Label></td>
                                <td class="shipmentLabel">
                                    Check In Time:</td>
                                <td>
                                    <asp:Label ID="lblCheckInTime" runat="server" Text='<%# Eval("CheckInTime") %>'></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    Estimated Date:</td>
                                <td>
                                    NA</td>
                                <td>
                                    Estimated Time:</td>
                                <td>
                                    NA</td>
                                <td>
                                    Veh Max Weight:</td>
                                <td>
                                    <asp:Label ID="lblVehicleMaxWeight" runat="server" Text='<%# Eval("VehicleMaxWeight") %>'></asp:Label></td>
                                <td>
                                    Volume:</td>
                                <td>
                                    NA</td>
                            </tr>
                            <tr>
                                <td>
                                    Actual Date:</td>
                                <td>
                                    NA</td>
                                <td>
                                    Actual Time:</td>
                                <td>
                                    NA</td>
                                <td>
                                    Tail Lift Req:</td>
                                <td>
                                    <asp:Image ID="imgTailLiftRequired" runat="server" ImageUrl='<%# Eval("TailLiftRequired", "~/Images/{0}") + ".gif" %>' /></td>
                                <td>
                                    Net Weight:</td>
                                <td>
                                    <asp:Label ID="lblNetWeight" runat="server" Text='<%# Eval("NetWeight", "{0:F}") %>'></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    Vehicle:</td>
                                <td>
                                    NA</td>
                                <td>
                                    Cost:</td>
                                <td>
                                    NA</td>
                                <td>
                                    Hours:</td>
                                <td>
                                    NA</td>
                                <td>
                                    Gross Weight:</td>
                                <td>
                                    <asp:Label ID="lblGrossWeight" runat="server" Text='<%# Eval("GrossWeight", "{0:F}") %>'></asp:Label></td>
                            </tr>
                      </table>
                    </fieldset>
                                
                    <table border="0" cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                            <td colspan="1" style="width: 33%" valign="top">
                                <!-- OpCo Contact -->
                                <fieldset><legend>Op Co Contact</legend>
                                <table width="100%">
                                    <tr>
                                        <td class="shipmentLabel">Contact:</td>
                                        <td class="shipmentValue">
                                            <asp:Label ID="lblOpCoContactName" runat="server" Text='<%# Eval("OpCoContact.Name") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="shipmentLabel">Email:</td>
                                        <td class="shipmentValue">
                                            <asp:Label ID="lblOpCoContactEmail" runat="server" Text='<%# Eval("OpCoContact.Email") %>'></asp:Label></td>
                                    </tr>
                                </table>
                                </fieldset>
                            </td>
                            <td colspan="1" style="width: 33%" valign="top">
                                <!-- Shipment Contact -->
                                <fieldset><legend>Shipment Contact</legend>
                                <table width="100%">
                                    <tr>
                                        <td class="shipmentLabel">Contact:</td>
                                        <td class="shipmentValue">
                                            <asp:Label ID="lblShipmentContactName" runat="server" Text='<%# Eval("ShipmentContact.Name") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="shipmentLabel">Email:</td>
                                        <td class="shipmentValue">
                                            <asp:Label ID="lblShipmentContactEmail" runat="server" Text='<%# Eval("ShipmentContact.Email") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td  class="shipmentLabel">Telephone:</td>
                                        <td class="shipmentValue">
                                            <asp:Label ID="lblShipmentContactTelephone" runat="server" Text='<%# Eval("ShipmentContact.TelephoneNumber") %>'></asp:Label></td>
                                    </tr>
                                </table>
                                </fieldset>
                            </td>
                            <td colspan="1" style="width: 33%" valign="top"></td>
                        </tr>
                        <tr>
                            <td colspan="1" style="width: 33%" valign="top">
                                <!-- Shipment Address -->
                                <fieldset><legend>Shipment Address</legend>
                                <table width="100%">
                                    <tr>
                                        <td class="shipmentLabel">Name:</td>
                                        <td class="shipmentValue">
                                            <asp:Label ID="lblShipmentName" runat="server" Text='<%# Eval("CustomerName") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="shipmentLabel">Address:</td>
                                        <td class="shipmentValue">
                                            <asp:Label ID="lblShipmentAddress1" runat="server" Text='<%# Eval("ShipmentAddress.Line1") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="shipmentLabel">&nbsp;</td>
                                        <td  class="shipmentValue">
                                            <asp:Label ID="lblShipmentAddress2" runat="server" Text='<%# Eval("ShipmentAddress.Line2") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="shipmentLabel">&nbsp;</td>
                                        <td class="shipmentValue">
                                            <asp:Label ID="lblShipmentAddress3" runat="server" Text='<%# Eval("ShipmentAddress.Line3") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="shipmentLabel">&nbsp;</td>
                                        <td class="shipmentValue">
                                            <asp:Label ID="lblShipmentAddress4" runat="server" Text='<%# Eval("ShipmentAddress.Line4") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="shipmentLabel">&nbsp;</td>
                                        <td class="shipmentValue">
                                            <asp:Label ID="lblShipmentAddress5" runat="server" Text='<%# Eval("ShipmentAddress.Line5") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="shipmentLabel">Post Code:</td>
                                        <td class="shipmentValue">
                                            <asp:Label ID="lblShipmentAddressPostCode" runat="server" Text='<%# Eval("ShipmentAddress.PostCode") %>'></asp:Label></td>
                                    </tr>
                                </table>
                                </fieldset>
                            </td>
                            <td colspan="1" style="width: 33%" valign="top">
                                <!-- Customer Address -->
                                <fieldset><legend>Customer Address</legend>
                                <table width="100%">
                                    <tr>
                                        <td class="shipmentLabel">Name:</td>
                                        <td class="shipmentValue">
                                            <asp:Label ID="lblCustomerAddressName" runat="server" Text='<%# Eval("CustomerName") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="shipmentLabel">Address:</td>
                                        <td class="shipmentValue">
                                            <asp:Label ID="lblCustomerAddress1" runat="server" Text='<%# Eval("CustomerAddress.Line1") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="shipmentLabel">&nbsp;</td>
                                        <td class="shipmentValue">
                                            <asp:Label ID="lblCustomerAddress2" runat="server" Text='<%# Eval("CustomerAddress.Line2") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="shipmentLabel">&nbsp;</td>
                                        <td class="shipmentValue">
                                            <asp:Label ID="lblCustomerAddress3" runat="server" Text='<%# Eval("CustomerAddress.Line3") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="shipmentLabel">&nbsp;</td>
                                        <td class="shipmentValue">
                                            <asp:Label ID="lblCustomerAddress4" runat="server" Text='<%# Eval("CustomerAddress.Line4") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="shipmentLabel">&nbsp;</td>
                                        <td class="shipmentValue">
                                            <asp:Label ID="lblCustomerAddress5" runat="server" Text='<%# Eval("CustomerAddress.Line5") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="shipmentLabel">Post Code:</td>
                                        <td class="shipmentValue">
                                            <asp:Label ID="lblCustomerAddressPostCode" runat="server" Text='<%# Eval("CustomerAddress.PostCode") %>'></asp:Label></td>
                                    </tr>
                                </table>
                                </fieldset>
                            </td>
                            <td style="width: 33%" valign="top"></td>
                        </tr>
                        </table>
                        <fieldset>
                           <legend>Instructions</legend>
                            <asp:Label ID="lblInstructions" runat="server" CssClass="Instructions" Text='<%# Eval("Instructions") %>'></asp:Label>&nbsp;
                        </fieldset>
                </asp:Panel>
            </asp:Panel>
            <asp:Panel ID="panelShipmentLineDetail" runat="server" CssClass="collapsePanelContainer" Width="990px">
                <asp:Panel ID="panelShipmentLineHeader" runat="server" CssClass="collapsePanelHeader" Width="100%">
                    <span class="collapsePanelTitle">OpCo Shipment Lines (<%# Eval("ShipmentLines.Count") %>)</span><span
                        class="collapsePanelMinMax">
                        <asp:Image ID="imgShipmentLinesMinMax" ImageUrl="~/Images/Container/Min.gif" runat="server" /></span>
                </asp:Panel>
                <asp:Panel ID="panelShipmentLineContent" runat="server" CssClass="collapsePanelContent"
                    Width="100%">
                    <Discovery:OpCoShipmentLines ID="ShipmentLines" runat="server" />
                </asp:Panel>
            </asp:Panel>
            <!-- AJAX Extender -->
            <ajaxToolkit:CollapsiblePanelExtender ID="collapsiblePanelShipment1" runat="Server"
                TargetControlID="panelShipmentContent" ExpandControlID="imgShipmentMinMax" CollapseControlID="imgShipmentMinMax"
                ImageControlID="imgShipmentMinMax" CollapsedSize="0" SuppressPostBack="True"
                ExpandedImage="~/Images/Container/Min.gif" CollapsedImage="~/Images/Container/Max.gif" />
            <ajaxToolkit:CollapsiblePanelExtender ID="collapsiblePanelShipment2" runat="Server"
                TargetControlID="panelShipmentLineContent" ExpandControlID="imgShipmentLinesMinMax"
                CollapseControlID="imgShipmentLinesMinMax" Collapsed="True" ImageControlID="imgShipmentLinesMinMax"
                CollapsedSize="0" SuppressPostBack="True" ExpandedImage="~/Images/Container/Min.gif"
                CollapsedImage="~/Images/Container/Max.gif" />
        </ItemTemplate>
    </asp:FormView>
    <!-- Form view for Op Co Shipment End -->
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
    </contenttemplate>
    </asp:UpdatePanel>
    <!-- AJAX Update Panel End -->
    <!-- Object Data Source -->
    <asp:ObjectDataSource ID="OpCoShipmentDataSource" runat="server" DataObjectTypeName="Discovery.BusinessObjects.OpCoShipment"
        DeleteMethod="DeleteShipment" InsertMethod="SaveShipment" SelectMethod="GetShipment"
        TypeName="Discovery.BusinessObjects.Controllers.OpCoShipmentController" UpdateMethod="SaveShipment"
        OldValuesParameterFormatString="original_{0}">
        <DeleteParameters>
            <asp:Parameter Name="shipmentId" Type="Int32" />
        </DeleteParameters>
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="-1" Name="ShipmentId" QueryStringField="Id"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

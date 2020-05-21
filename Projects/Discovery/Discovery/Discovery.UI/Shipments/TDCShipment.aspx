<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="TDCShipment.aspx.cs"
    Inherits="Discovery.UI.Web.Shipments.TDCShipment" Title="TDC Shipment" %>

<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="Discovery" %>
<%@ Register Src="~/UserControls/TDCShipmentLines.ascx" TagName="TDCShipmentLines"
    TagPrefix="Discovery" %>
<%@ Register Src="~/UserControls/TDCShipmentLinesSplit.ascx" TagName="TDCShipmentLinesSplit"
    TagPrefix="Discovery" %>
<%@ Register Src="~/UserControls/TDCShipmentLinesAdd.ascx" TagName="TDCShipmentLinesAdd"
    TagPrefix="Discovery" %>
<%@ Register Src="../Printing/DeliveryNote.ascx" TagName="DeliveryNote" TagPrefix="Discovery" %>
<%@ Import Namespace="System.Web.UI.WebControls" %>
<%@ Import Namespace="System.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
            <%---- Form view for TDC Shipments Start ----%>
            <asp:FormView SkinID="FormViewNone" ID="TDCShipmentFormView" runat="server" DataSourceID="TDCShipmentDataSource"
                OnItemUpdating="TDCShipmentFormView_ItemUpdating" OnItemInserting="TDCShipmentFormView_ItemInserting"
                OnDataBound="TDCShipmentFormView_DataBound">
                <EditItemTemplate>
                    <%-------------------------EditItemTemplate Object datasources definitions------------------------------- 
                    Notice that the object datasources are specific to this template and are declared withint its boundaries
                    --------------------------------------------------------------------------------------------------------%>
                    <%---- Shipment Status Data Source ----%>
                    <asp:ObjectDataSource ID="StatusDataSource" runat="server" SelectMethod="GetShipmentStatuses"
                        TypeName="Discovery.BusinessObjects.Controllers.TDCShipmentController" />
                    <%---- Shipment Type Data Source ----%>
                    <asp:ObjectDataSource ID="TypeDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetShipmentTypes" TypeName="Discovery.BusinessObjects.Controllers.TDCShipmentController">
                    </asp:ObjectDataSource>
                    <%---- Route Data Source ----%>
                    <asp:ObjectDataSource ID="RouteDataSource" runat="server" SelectMethod="GetRoutes"
                        TypeName="Discovery.BusinessObjects.Controllers.RouteController" />
                    <%---- Transaction Type Data Source ----%>
                    <asp:ObjectDataSource ID="TransactionTypeDataSource" runat="server" SelectMethod="GetTransactionTypes"
                        TypeName="Discovery.BusinessObjects.Controllers.TransactionTypeController">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="Description" Name="sortExpression" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <%---- Transaction Sub Type Data Source ----%>
                    <asp:ObjectDataSource ID="TransactionSubTypeDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetTransactionSubTypes" TypeName="Discovery.BusinessObjects.Controllers.TransactionSubTypeController">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="Description" Name="sortExpression" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <%---- Warehouse Data Source ----%>
                    <asp:ObjectDataSource ID="WarehouseDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetWarehouses" TypeName="Discovery.BusinessObjects.Controllers.WarehouseController">
                    </asp:ObjectDataSource>
                    <%---- Sales Branch Data Source ----%>
                    <asp:ObjectDataSource ID="SalesBranchDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetLocations" TypeName="Discovery.BusinessObjects.Controllers.SalesLocationController">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="false" Name="fullyPopulate" Type="Boolean" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <%------------------- End of the object datasource difinitions for the EditItemTempalte  ---------------%>
                    <asp:Panel ID="panelShipmentDetail" runat="server" CssClass="collapsePanelContainer"
                        Width="990px">
                        <asp:Panel ID="panelShipmentHeader" runat="server" CssClass="collapsePanelHeader"
                            Width="100%">
                            <span class="collapsePanelTitle">TDC Shipment Details (<%# string.Concat(Eval("OpCoCode"), "-", Eval("ShipmentNumber"), "-", Eval("DespatchNumber"), " ", Eval("CustomerName")) %>)</span><span
                                class="collapsePanelMinMax">
                                <asp:Image ID="imgShipmentMinMax" ImageUrl="~/Images/Container/Min.gif" runat="server" /></span>
                        </asp:Panel>
                        <asp:Panel ID="panelShipmentContent" runat="server" CssClass="collapsePanelContent"
                            Width="100%">
                            <asp:HiddenField ID="txtId" runat="server" Value='<%# Bind("Id") %>' />
                            <asp:HiddenField ID="txtCheckSum" runat="server" Value='<%# Bind("CheckSum") %>' />
                            <asp:HiddenField ID="txtAuditId" runat="server" Value='<%# Bind("AuditId") %>' />
                            <asp:HiddenField ID="txtOpCoShipmentId" runat="server" Value='<%# Bind("OpCoShipmentId") %>' />
                            <asp:HiddenField ID="txtIsPrinted" runat="server" Value='<%# Bind("IsPrinted") %>' />
                            <asp:HiddenField ID="txtPrintCount" runat="server" Value='<%# Bind("PrintCount") %>' />
                            <asp:HiddenField ID="txtLocationCode" runat="server" Value='<%# Bind("LocationCode") %>' />
                            <fieldset>
                                <legend>Options</legend>
                                <asp:ImageButton ID="ButtonUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                    SkinID="ButtonSave" />
                                <asp:ImageButton ID="ButtonUpdateCancel" runat="server" CausesValidation="False"
                                    CommandName="Cancel" SkinID="ButtonCancel" />
                                <%---- Address Lookup ----%>
                                <asp:ImageButton ID="btnValidateAddress" SkinID="ButtonLookupAddress" runat="server"
                                    Enabled="True" ToolTip="Validates the shipment address." OnClick="btnValidateAddress_Click" />
                                <%-- Hide ajax controls ued in read-only view --%>
                                <div style="visibility: hidden; display: none">
                                    <%---- Line Splits Start ----%>
                                    <Discovery:TDCShipmentLinesSplit ID="TDCShipmentLinesSplits" runat="server" Enabled="false" />
                                    <%---- Line Add Start ----%>
                                    <Discovery:TDCShipmentLinesAdd ID="TDCShipmentLinesAdditions" runat="server" Enabled="false" />
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>Customer Details</legend>
                                <table border="0" cellspacing="0" cellpadding="2" width="100%">
                                    <tr>
                                        <td class="shipmentLabel">
                                            Number:</td>
                                        <td class="shipmentValue">
                                            <asp:Label ID="lblCustomerNumber" runat="server" Text='<%# Bind("CustomerNumber") %>'></asp:Label></td>
                                        <td class="shipmentLabel">
                                            Reference:</td>
                                        <td class="shipmentValue">
                                            <asp:Label ID="lblCustomerReference" runat="server" Text='<%# Bind("CustomerReference") %>'></asp:Label></td>
                                        <td class="shipmentLabel">
                                            Name:</td>
                                        <td>
                                            <asp:Label ID="lblCustomerName" runat="server" Text='<%# Bind("CustomerName") %>'></asp:Label></td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset>
                                <legend>Shipment Details</legend>
                                <table border="0" cellspacing="0" cellpadding="2">
                                    <tr>
                                        <td class="shipmentLabel">
                                            Op Co:</td>
                                        <td class="shipmentValue">
                                            <b><asp:Label ID="lblOpCoCode" runat="server" Text='<%# Bind("OpCoCode") %>'></asp:Label></b></td>
                                        <td class="shipmentLabel">
                                            Number:</td>
                                        <td class="shipmentValue">
                                            <b><asp:Label ID="lblShipmentNumber" runat="server" Text='<%# Bind("ShipmentNumber") %>'></asp:Label></b></td>
                                        <td class="shipmentLabel">
                                            Despatch #:</td>
                                        <td class="shipmentValue">
                                            <asp:Label ID="lblDespatchNumber" runat="server" Text='<%# Bind("DespatchNumber") %>'></asp:Label></td>
                                        <td class="shipmentLabel">
                                            Status:</td>
                                        <td class="shipmentValue">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="EditText" DataSourceID="StatusDataSource"
                                                DataValueField="Code" DataTextField="Code" SelectedValue='<%# Bind("Status") %>'>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Route Code:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlRouteCode" runat="server" CssClass="EditText" DataSourceID="RouteDataSource"
                                                DataTextField="Description" DataValueField="Code" SelectedValue='<%# Bind("RouteCode") %>'>
                                            </asp:DropDownList></td>
                                        <td>
                                            Trans. Type:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlTransactionType" runat="server" CssClass="EditText" DataSourceID="TransactionTypeDataSource"
                                                DataTextField="Description" DataValueField="Code" SelectedValue='<%# Bind("TransactionTypeCode") %>'>
                                            </asp:DropDownList></td>
                                        <td>
                                            Trans. Sub Type:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlTransactionSubType" runat="server" CssClass="EditText" DataSourceID="TransactionSubTypeDataSource"
                                                DataTextField="Description" DataValueField="Code" SelectedValue='<%# Bind("TransactionSubTypeCode") %>'>
                                            </asp:DropDownList></td>
                                        <td>
                                            Generated:</td>
                                        <td>
                                            <asp:Label ID="lblGeneratedDateTime" runat="server" Text='<%# Bind("GeneratedDateTime") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            File Seq. #:</td>
                                        <td>
                                            <asp:Label ID="lblOpCoSequenceNumber" runat="server" Text='<%# Bind("OpCoSequenceNumber") %>'></asp:Label></td>
                                        <td>
                                            Division Code:</td>
                                        <td>
                                            <asp:Label ID="lblDivision" runat="server" Text='<%# Bind("DivisionCode") %>'></asp:Label></td>
                                        <td>
                                            Sales Branch:</td>
                                        <td colspan="3">
                                            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SalesBranchDataSource"
                                                DataTextField="Description" DataValueField="Location" SelectedValue='<%# Bind("SalesBranchCode") %>'>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Type:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlType" runat="server" CssClass="EditText" DataSourceID="TypeDataSource"
                                                DataTextField="Code" DataValueField="Code" SelectedValue='<%# Bind("Type") %>'>
                                            </asp:DropDownList></td>
                                        <td>
                                            OpCo Held:</td>
                                        <td>
                                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("OpCoHeld", "~/Images/{0}") + ".gif" %>' /></td>
                                        <td>
                                            Is Recurring:</td>
                                        <td>
                                            <asp:CheckBox ID="chkIsRecurring" runat="server" Checked='<%# Bind("IsRecurring") %>' /></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset>
                                <legend>Delivery Details</legend>
                                <table border="0" width="100%" cellspacing="0" cellpadding="2">
                                    <tr>
                                        <td class="shipmentLabel">
                                            Stock Loc.:</td>
                                        <td class="shipmentValue">
                                            <asp:DropDownList ID="ddlStockWarehouse" runat="server" CssClass="EditText" DataSourceID="WarehouseDataSource"
                                                DataTextField="Code" DataValueField="Code" SelectedValue='<%# Bind("StockWarehouseCode") %>'>
                                            </asp:DropDownList></td>
                                        <td class="shipmentLabel">
                                            Delivery Loc.:</td>
                                        <td class="shipmentValue">
                                            <asp:DropDownList ID="ddlDeliveryWarehouse" runat="server" CssClass="EditText" DataSourceID="WarehouseDataSource"
                                                DataTextField="Code" DataValueField="Code" SelectedValue='<%# Bind("DeliveryWarehouseCode") %>'>
                                            </asp:DropDownList></td>
                                        <td class="shipmentLabel">
                                        </td>
                                        <td class="shipmentValue">
                                        </td>
                                        <td class="shipmentLabel">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="shipmentLabel">
                                            Required Date:</td>
                                        <td class="shipmentValue">
                                            <asp:TextBox ID="txtRequiredDeliveryDate" runat="server" CssClass="EditText" Text='<%# Bind("RequiredShipmentDate", "{0:d}") %>'
                                                Width="60px"></asp:TextBox></td>
                                        <td class="shipmentLabel">
                                            After Time:</td>
                                        <td class="shipmentValue">
                                            <asp:TextBox ID="txtAfterTime" runat="server" CssClass="EditText" Text='<%# Bind("AfterTime") %>'
                                                Width="35px"></asp:TextBox></td>
                                        <td class="shipmentLabel">
                                            Before Time:</td>
                                        <td class="shipmentValue">
                                            <asp:TextBox ID="txtBeforeTime" runat="server" CssClass="EditText" Text='<%# Bind("BeforeTime") %>'
                                                Width="35px"></asp:TextBox></td>
                                        <td class="shipmentLabel">
                                            Check In Time:</td>
                                        <td>
                                            <asp:TextBox ID="txtCheckInTime" runat="server" CssClass="EditText" Text='<%# Bind("CheckInTime") %>'
                                                Width="50px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Estimated Date:</td>
                                        <td>
                                            <Discovery:DiscoveryNullText ID="txtEstimatedDeliveryDate" runat="server" CssClass="EditText"
                                                Text='<%# Bind("EstimatedDeliveryDate", "{0:d}") %>' Width="60px" DataType="System.DateTime"
                                                NullText=""></Discovery:DiscoveryNullText>
                                        </td>
                                        <td>
                                            Estimated Time:</td>
                                        <td>
                                            <asp:TextBox ID="txtEstimatedDeliveryTime" runat="server" CssClass="EditText" Width="35px"
                                                Text='<%# Eval("EstimatedDeliveryDate", "{0:t}") %>' /></td>
                                        <td>
                                            Veh Max Weight:</td>
                                        <td>
                                            <asp:TextBox ID="txtVehicleMaxWeight" runat="server" CssClass="EditText" Text='<%# Bind("VehicleMaxWeight") %>'
                                                Width="50px"></asp:TextBox></td>
                                        <td>
                                            Volume:</td>
                                        <td>
                                            <asp:Label ID="lblVolume" runat="server" Text='<%# Eval("Volume") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Actual Date:</td>
                                        <td>
                                            <Discovery:DiscoveryNullText ID="txtActualDeliveryDate" runat="server" CssClass="EditText"
                                                Text='<%# Bind("ActualDeliveryDate", "{0:d}") %>' Width="60px" DataType="System.DateTime"
                                                NullText=""></Discovery:DiscoveryNullText>
                                        </td>
                                        <td>
                                            Actual Time:</td>
                                        <td>
                                            <asp:TextBox ID="txtActualDeliveryTime" runat="server" CssClass="EditText" Width="35px"
                                                Text='<%# Eval("ActualDeliveryDate", "{0:t}") %>' /></td>
                                        <td>
                                            Tail Lift Req:</td>
                                        <td>
                                            <asp:CheckBox ID="chkTailLiftRequired" runat="server" Checked='<%# Bind("TailLiftRequired") %>' /></td>
                                        <td>
                                            Net Weight:</td>
                                        <td>
                                            <asp:Label ID="lblNetWeight" runat="server" Text='<%# Eval("NetWeight", "{0:F}") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Vehicle:</td>
                                        <td>
                                            *****</td>
                                        <td>
                                            Cost:</td>
                                        <td>
                                            *****</td>
                                        <td>
                                            Hours:</td>
                                        <td>
                                            *****</td>
                                        <td>
                                            Gross Weight:</td>
                                        <td>
                                            <asp:Label ID="lblGrossWeight" runat="server" Text='<%# Eval("GrossWeight", "{0:F}") %>'></asp:Label></td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset>
                                <legend>Route</legend>
                                <table border="0" cellspacing="0" cellpadding="2" width="100%">
                                    <tr>
                                        <td class="shipmentLabel">
                                            Next Day:</td>
                                        <td class="shipmentValue">
                                            <asp:Image ID="imgIsNextDay" runat="server" ImageUrl='<%# Eval("Route.IsNextDay", "~/Images/{0}") + ".gif" %>' /></td>
                                        <td class="shipmentLabel">
                                            Same Day:</td>
                                        <td class="shipmentValue">
                                            <asp:Image ID="imgIsSameDay" runat="server" ImageUrl='<%# Eval("Route.IsSameDay", "~/Images/{0}") + ".gif" %>' /></td>
                                        <td class="shipmentLabel">
                                            Customer Collect:</td>
                                        <td class="shipmentValue">
                                            <asp:Image ID="imgIsCustomerCollection" runat="server" ImageUrl='<%# Eval("Route.IsCollection", "~/Images/{0}") + ".gif" %>' /></td>
                                        <td class="shipmentLabel">
                                            Special:</td>
                                        <td class="shipmentValue">
                                            <asp:Image ID="imgIsSpecial" runat="server" ImageUrl='<%# Eval("Route.IsSpecial", "~/Images/{0}") + ".gif" %>' /></td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset>
                                <legend>Transaction Type</legend>
                                <table border="0" cellspacing="0" cellpadding="2" width="100%">
                                    <tr>
                                        <td class="shipmentLabel">
                                            Stock:</td>
                                        <td class="shipmentValue">
                                            <asp:Image ID="imgIsStock" runat="server" ImageUrl='<%# Eval("TransactionType.IsStock", "~/Images/{0}") + ".gif" %>' /></td>
                                        <td class="shipmentLabel">
                                            Non Stock:</td>
                                        <td class="shipmentValue">
                                            <asp:Image ID="imgIsNonStock" runat="server" ImageUrl='<%# Eval("TransactionType.IsNonStock", "~/Images/{0}") + ".gif" %>' /></td>
                                        <td class="shipmentLabel">
                                            TDC Collect:</td>
                                        <td class="shipmentValue">
                                            <asp:Image ID="imgIsCollection" runat="server" ImageUrl='<%# Eval("TransactionType.IsCollection", "~/Images/{0}") + ".gif" %>' /></td>
                                        <td class="shipmentLabel">
                                            Sample:</td>
                                        <td class="shipmentValue">
                                            <asp:Image ID="imgIsSample" runat="server" ImageUrl='<%# Eval("TransactionType.IsSample", "~/Images/{0}") + ".gif" %>' /></td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset>
                                <legend>Transaction Sub Type</legend>
                                <table border="0" cellspacing="0" cellpadding="2" width="100%">
                                    <tr>
                                        <td class="shipmentLabel">
                                            Normal:</td>
                                        <td class="shipmentValue">
                                            <asp:Image ID="imgIsNormal" runat="server" ImageUrl='<%# Eval("TransactionSubType.IsNormal", "~/Images/{0}") + ".gif" %>' /></td>
                                        <td class="shipmentLabel">
                                            Transfer:</td>
                                        <td class="shipmentValue">
                                            <asp:Image ID="imgIsTransfer" runat="server" ImageUrl='<%# Eval("TransactionSubType.IsTransfer", "~/Images/{0}") + ".gif" %>' /></td>
                                        <td class="shipmentLabel">
                                            Local Conv.:</td>
                                        <td class="shipmentValue">
                                            <asp:Image ID="imgIsLocalConv" runat="server" ImageUrl='<%# Eval("TransactionSubType.IsLocalConversion", "~/Images/{0}") + ".gif" %>' /></td>
                                        <td class="shipmentLabel">
                                            3rd Party Conv.:</td>
                                        <td class="shipmentValue">
                                            <asp:Image ID="imgIs3rdPartyConv" runat="server" ImageUrl='<%# Eval("TransactionSubType.Is3rdPartyConversion", "~/Images/{0}") + ".gif" %>' /></td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset>
                                <legend>Routing Details</legend>
                                <table border="0" cellspacing="0" cellpadding="2" width="100%">
                                    <tr>
                                        <td class="shipmentLabel">
                                            Route Trip:</td>
                                        <td class="shipmentValue">
                                            <asp:Label ID="lblRouteTrip" runat="server" Text='<%# Bind("RouteTrip") %>' Width="50px"></asp:Label></td>
                                        <td class="shipmentLabel">
                                            Split Sequence:</td>
                                        <td class="shipmentValue">
                                            <asp:Label ID="lblSplitSequence" runat="server" Text='<%# Bind("SplitSequence") %>'
                                                Width="50px"></asp:Label></td>
                                        <td class="shipmentLabel">
                                            Sent To WMS:</td>
                                        <td class="shipmentValue">
                                            <Discovery:DiscoveryNullLabel ID="lblSentToWMS" runat="server" Text='<%# Bind("SentToWMS") %>'
                                                Width="100px" DataType="System.DateTime" NullText=""></Discovery:DiscoveryNullLabel>
                                        </td>
                                        <td class="shipmentLabel">
                                            Route Date Time:</td>
                                        <td>
                                            <Discovery:DiscoveryNullLabel ID="lblRoutingDateTime" runat="server" Text='<%# Bind("RoutingDateTime") %>'
                                                Width="100px" DataType="System.DateTime" NullText=""></Discovery:DiscoveryNullLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Route Drop:</td>
                                        <td>
                                            <asp:Label ID="lblRouteDrop" runat="server" Text='<%# Bind("RouteDrop") %>' Width="50px"></asp:Label></td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                <tr>
                                    <td colspan="1" style="width: 33%" valign="top">
                                        <fieldset>
                                            <legend>Op Co Contact</legend>
                                            <table width="100%" cellspacing="0" cellpadding="2">
                                                <tr>
                                                    <td class="shipmentLabel">
                                                        Contact:</td>
                                                    <td class="shipmentValue">
                                                        <asp:TextBox ID="txtOpCoContactName" CssClass="EditText" runat="server" Text='<%# Eval("OpCoContact.Name") %>'
                                                            Width="180px"></asp:TextBox>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Email:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtOpCoContactEmail" CssClass="EditText" runat="server" Text='<%# Eval("OpCoContact.Email") %>'
                                                            Width="180px"></asp:TextBox></td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                    <td colspan="1" style="width: 33%" valign="top">
                                        <fieldset>
                                            <legend>Shipment Contact</legend>
                                            <table width="100%" border="0" cellspacing="0" cellpadding="2">
                                                <tr>
                                                    <td class="shipmentLabel">
                                                        Contact:</td>
                                                    <td class="shipmentValue">
                                                        <asp:TextBox ID="txtShipmentContactName" CssClass="EditText" runat="server" Text='<%# Eval("ShipmentContact.Name") %>'
                                                            Width="180px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Email:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtShipmentContactEmail" CssClass="EditText" runat="server" Text='<%# Eval("ShipmentContact.Email") %>'
                                                            Width="180px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Telephone:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtShipmentContactTelephone" CssClass="EditText" runat="server"
                                                            Text='<%# Eval("ShipmentContact.TelephoneNumber") %>' Width="180px"></asp:TextBox></td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                    <td colspan="1" style="width: 33%" valign="top">
                                        <fieldset>
                                            <legend>PAF Details</legend>
                                            <table cellspacing="0" cellpadding="2">
                                                <tr>
                                                    <td class="shipmentLabel">
                                                        Easting:</td>
                                                    <td class="shipmentValue">
                                                        <asp:TextBox ID="txtPAFEasting" runat="server" CssClass="EditText" Text='<%# Eval("PAFAddress.Easting") %>'></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Northing:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtPAFNorthing" runat="server" CssClass="EditText" Text='<%# Eval("PAFAddress.Northing") %>'></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        DPS:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtPAFDPS" runat="server" CssClass="EditText" Text='<%# Eval("PAFAddress.DPS") %>'></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Status:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtPAFStatus" runat="server" CssClass="EditText" Text='<%# Eval("PAFAddress.Status") %>'></asp:TextBox></td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="1" style="width: 33%" valign="top">
                                        <fieldset>
                                            <legend>Customer Address</legend>
                                            <table width="100%" cellspacing="0" cellpadding="2">
                                                <tr>
                                                    <td class="shipmentLabel">
                                                        Name:</td>
                                                    <td class="shipmentValue">
                                                        <asp:TextBox ID="txtCustomerAddressName" CssClass="EditText" runat="server" Text='<%# Bind("CustomerName") %>'
                                                            Width="185px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Address:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtCustomerAddress1" CssClass="EditText" runat="server" Text='<%# Eval("CustomerAddress.Line1") %>'
                                                            Width="185px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:TextBox ID="txtCustomerAddress2" CssClass="EditText" runat="server" Text='<%# Eval("CustomerAddress.Line2") %>'
                                                            Width="185px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:TextBox ID="txtCustomerAddress3" CssClass="EditText" runat="server" Text='<%# Eval("CustomerAddress.Line3") %>'
                                                            Width="185px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:TextBox ID="txtCustomerAddress4" CssClass="EditText" runat="server" Text='<%# Eval("CustomerAddress.Line4") %>'
                                                            Width="185px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:TextBox ID="txtCustomerAddress5" CssClass="EditText" runat="server" Text='<%# Eval("CustomerAddress.Line5") %>'
                                                            Width="185px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Post Code:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtCustomerAddressPostCode" CssClass="EditText" runat="server" Text='<%# Eval("CustomerAddress.PostCode") %>'
                                                            Width="80px"></asp:TextBox></td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                    <td colspan="1" style="width: 33%" valign="top">
                                        <fieldset>
                                            <legend>Shipment Address</legend>
                                            <table width="100%" border="0" cellspacing="0" cellpadding="2">
                                                <tr>
                                                    <td class="shipmentLabel">
                                                        Name:</td>
                                                    <td class="shipmentValue">
                                                        <asp:TextBox ID="txtShipmentAddressName" CssClass="EditText" runat="server" Text='<%# Bind("ShipmentName") %>'
                                                            Width="185px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Address:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtShipmentAddress1" CssClass="EditText" runat="server" Text='<%# Eval("ShipmentAddress.Line1") %>'
                                                            Width="185px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:TextBox ID="txtShipmentAddress2" CssClass="EditText" runat="server" Text='<%# Eval("ShipmentAddress.Line2") %>'
                                                            Width="185px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:TextBox ID="txtShipmentAddress3" CssClass="EditText" runat="server" Text='<%# Eval("ShipmentAddress.Line3") %>'
                                                            Width="185px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:TextBox ID="txtShipmentAddress4" CssClass="EditText" runat="server" Text='<%# Eval("ShipmentAddress.Line4") %>'
                                                            Width="185px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:TextBox ID="txtShipmentAddress5" CssClass="EditText" runat="server" Text='<%# Eval("ShipmentAddress.Line5") %>'
                                                            Width="185px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Post Code:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtShipmentAddressPostCode" CssClass="EditText" runat="server" Text='<%# Eval("ShipmentAddress.PostCode") %>'
                                                            Width="80px"></asp:TextBox></td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                    <td colspan="1" style="width: 33%" valign="top">
                                        <fieldset>
                                            <legend>PAF Address</legend>
                                            <table width="100%" cellspacing="0" cellpadding="2">
                                                <tr>
                                                    <td class="shipmentLabel">
                                                        Name:</td>
                                                    <td class="shipmentValue">
                                                        <asp:Label ID="lblPAFCustomerName" runat="server" Text='<%# Eval("ShipmentName") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Address:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtPAFAddress1" runat="server" CssClass="EditText" Width="185px"
                                                            Text='<%# Eval("PAFAddress.Line1") %>'></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:TextBox ID="txtPAFAddress2" runat="server" CssClass="EditText" Width="185px"
                                                            Text='<%# Eval("PAFAddress.Line2") %>'></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:TextBox ID="txtPAFAddress3" runat="server" CssClass="EditText" Width="185px"
                                                            Text='<%# Eval("PAFAddress.Line3") %>'></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:TextBox ID="txtPAFAddress4" runat="server" CssClass="EditText" Width="185px"
                                                            Text='<%# Eval("PAFAddress.Line4") %>'></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:TextBox ID="txtPAFAddress5" runat="server" CssClass="EditText" Width="185px"
                                                            Text='<%# Eval("PAFAddress.Line5") %>'></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Post Code:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtPAFAddressPostCode" runat="server" CssClass="EditText" Text='<%# Eval("PAFAddress.PostCode") %>'
                                                            Width="80px"></asp:TextBox></td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                            <fieldset>
                                <legend>Instructions</legend>
                                <asp:TextBox ID="txtInstructions" CssClass="EditText" runat="server" Text='<%# Bind("Instructions") %>'
                                    Rows="4" TextMode="MultiLine" Width="95%" ToolTip="Please enter the shipping instructions."></asp:TextBox>
                            </fieldset>
                        </asp:Panel>
                    </asp:Panel>
                    <%---- AJAX EXTENDERS ----%>
                    <ajaxToolkit:CollapsiblePanelExtender ID="collapsiblePanelShipment" runat="Server"
                        TargetControlID="panelShipmentContent" ExpandControlID="imgShipmentMinMax" CollapseControlID="imgShipmentMinMax"
                        ImageControlID="imgShipmentMinMax" CollapsedSize="0" SuppressPostBack="True"
                        ExpandedImage="~/Images/Container/Min.gif" CollapsedImage="~/Images/Container/Max.gif" />
                    <ajaxToolkit:ToggleButtonExtender ID="ToggleButtonExtender1" runat="server" TargetControlID="chkTailLiftRequired"
                        UncheckedImageUrl="~/images/uncheck.gif" CheckedImageUrl="~/images/check.gif"
                        ImageHeight="16" ImageWidth="16" />
                    <ajaxToolkit:ToggleButtonExtender ID="ToggleButtonExtender2" runat="server" TargetControlID="chkIsRecurring"
                        UncheckedImageUrl="~/images/uncheck.gif" CheckedImageUrl="~/images/check.gif"
                        ImageHeight="16" ImageWidth="16" />
                </EditItemTemplate>
                <InsertItemTemplate>
                    <%-------------------------EditItemTemplate Object datasources definitions------------------------------- 
            Notice that the object datasources are specific to this template and are declared withint its boundaries
            --------------------------------------------------------------------------------------------------------%>
                    <%---- OpCo Data Sources ----%>
                    <asp:ObjectDataSource ID="OpCoDataSource" runat="server" SelectMethod="GetOpCos"
                        TypeName="Discovery.BusinessObjects.Controllers.OpcoController" />
                    <%---- Route Data Source ----%>
                    <asp:ObjectDataSource ID="RouteDataSource" runat="server" SelectMethod="GetRoutes"
                        TypeName="Discovery.BusinessObjects.Controllers.RouteController" />
                    <%---- Transaction Type Data Source ----%>
                    <asp:ObjectDataSource ID="TransactionTypeDataSource" runat="server" SelectMethod="GetTransactionTypes"
                        TypeName="Discovery.BusinessObjects.Controllers.TransactionTypeController">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="Description" Name="sortExpression" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <%---- Transaction Sub Type Data Source ----%>
                    <asp:ObjectDataSource ID="TransactionSubTypeDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetTransactionSubTypes" TypeName="Discovery.BusinessObjects.Controllers.TransactionSubTypeController">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="Description" Name="sortExpression" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <%---- Shipment Status Data Source ----%>
                    <asp:ObjectDataSource ID="StatusDataSource" runat="server" SelectMethod="GetShipmentStatuses"
                        TypeName="Discovery.BusinessObjects.Controllers.TDCShipmentController" />
                    <%---- Sales Location Data Sources ----%>
                    <asp:ObjectDataSource ID="SalesLocationDataSource" runat="server" SelectMethod="GetTDCSalesLocations"
                        TypeName="Discovery.BusinessObjects.Controllers.SalesLocationController" OldValuesParameterFormatString="original_{0}">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlOpCoCode" Name="opCoCode" PropertyName="SelectedValue"
                                Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <%---- Op Co Divisions ----%>
                    <asp:ObjectDataSource ID="DivisionDataSource" runat="server" SelectMethod="GetOpCoDivisions"
                        TypeName="Discovery.BusinessObjects.Controllers.OpcoDivisionController" OldValuesParameterFormatString="original_{0}">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlOpCoCode" Name="opCoCode" PropertyName="SelectedValue"
                                Type="String" />
                            <asp:Parameter DefaultValue="false" Name="fullyPopulate" Type="Boolean" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <%---- Warehouse Data Source ----%>
                    <asp:ObjectDataSource ID="WarehouseDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetWarehouses" TypeName="Discovery.BusinessObjects.Controllers.WarehouseController">
                    </asp:ObjectDataSource>
                    <%------------------- End of the object datasource difinitions for the EditItemTempalte  ---------------%>
                    <asp:Panel ID="panelShipmentDetail" runat="server" CssClass="collapsePanelContainer"
                        Width="990px">
                        <asp:Panel ID="panelShipmentHeader" runat="server" CssClass="collapsePanelHeader"
                            Width="100%">
                            <span class="collapsePanelTitle">New TDC Shipment</span><span class="collapsePanelMinMax">
                                <asp:Image ID="imgShipmentMinMax" ImageUrl="~/Images/Container/Min.gif" runat="server" /></span>
                        </asp:Panel>
                        <asp:Panel ID="panelShipmentContent" runat="server" CssClass="collapsePanelContent"
                            Width="100%">
                            <fieldset>
                                <legend>Options</legend>
                                <%---- Save ----%>
                                <asp:ImageButton ID="ButtonInsert" runat="server" CausesValidation="True" CommandName="Insert"
                                    SkinID="ButtonSave" />&nbsp;
                                <%---- Cancel ----%>
                                <asp:ImageButton ID="ButtonUpdateCancel" runat="server" CausesValidation="False"
                                    CommandName="Cancel" SkinID="ButtonCancel" />&nbsp;
                                <%---- Address Lookup ----%>
                                <asp:ImageButton ID="btnValidateAddress" SkinID="ButtonLookupAddress" runat="server"
                                    Enabled="True" ToolTip="Validates the shipment address." OnClick="btnValidateAddress_Click" />
                            </fieldset>
                            <fieldset>
                                <legend>Shipment Details</legend>
                                <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                    <tr>
                                        <td class="shipmentLabel">
                                            Op Co:</td>
                                        <td class="shipmentValue">
                                            <Discovery:OpCoDropDownList ID="ddlOpCoCode" UsersOpCo='<%# Profile.OpCoCode %>'
                                                runat="server" DataSourceID="OpCoDataSource" DataTextField="Code" DataValueField="Code"
                                                CssClass="EditText" SelectedValue='<%# Bind("OpCoCode") %>' AutoPostBack="True" />
                                        </td>
                                        <td class="shipmentLabel">
                                            Type:</td>
                                        <td class="shipmentValue">
                                            <asp:DropDownList ID="ddlType" runat="server" CssClass="EditText" SelectedValue='<%# Bind("Type") %>'>
                                                <asp:ListItem Selected="True" Value="ADH">Ad-hoc</asp:ListItem>
                                                <asp:ListItem Value="WHS">Warehouse</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="shipmentLabel">
                                            Route Code:</td>
                                        <td class="shipmentValue">
                                            <asp:DropDownList ID="ddlRouteCode" runat="server" CssClass="EditText" DataSourceID="RouteDataSource"
                                                DataTextField="Description" DataValueField="Code" SelectedValue='<%# Bind("RouteCode") %>'>
                                            </asp:DropDownList></td>
                                        <td class="shipmentLabel">
                                            Status:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="EditText" DataSourceID="StatusDataSource"
                                                DataTextField="Code" DataValueField="Code" SelectedValue='<%# Bind("Status") %>'>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Sales Branch:</td>
                                        <td>
                                            <%---- We cannot databind here or we'll get an Eval() error, etc ----%>
                                            <asp:DropDownList ID="ddlSalesBranch" runat="server" OnDataBound="ddlSalesBranch_DataBound"
                                                CssClass="EditText" DataSourceID="SalesLocationDataSource" DataTextField="Description"
                                                DataValueField="Location">
                                            </asp:DropDownList></td>
                                        <td>
                                            Trans. Type:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlTransactionType" runat="server" CssClass="EditText" DataSourceID="TransactionTypeDataSource"
                                                DataTextField="Description" DataValueField="Code" SelectedValue='<%# Bind("TransactionTypeCode") %>'>
                                            </asp:DropDownList></td>
                                        <td>
                                            Trans. Sub Type:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlTransactionSubType" runat="server" CssClass="EditText" DataSourceID="TransactionSubTypeDataSource"
                                                DataTextField="Description" DataValueField="Code" SelectedValue='<%# Bind("TransactionSubTypeCode") %>'>
                                            </asp:DropDownList></td>
                                        <td>
                                            Division:</td>
                                        <td>
                                            <%---- We cannot databind here or we'll get an Eval() error, etc ----%>
                                            <asp:DropDownList ID="ddlDivision" runat="server" OnDataBound="ddlDivision_DataBound"
                                                CssClass="EditText" DataSourceID="DivisionDataSource" DataTextField="Code" DataValueField="Code">
                                            </asp:DropDownList></td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset>
                                <legend>Delivery Details</legend>
                                <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                    <tr>
                                        <td class="shipmentLabel">
                                            Required Date:</td>
                                        <td class="shipmentValue">
                                            <asp:TextBox ID="txtRequiredDeliveryDate" runat="server" CssClass="EditText" Text='<%# Bind("RequiredShipmentDate", "{0:d}") %>'
                                                Width="60px"></asp:TextBox></td>
                                        <td class="shipmentLabel">
                                            After Time:</td>
                                        <td class="shipmentValue">
                                            <asp:TextBox ID="txtAfterTime" runat="server" CssClass="EditText" Text='<%# Bind("AfterTime") %>'
                                                Width="35px"></asp:TextBox></td>
                                        <td class="shipmentLabel">
                                            Before Time:</td>
                                        <td class="shipmentValue">
                                            <asp:TextBox ID="txtBeforeTime" runat="server" CssClass="EditText" Text='<%# Bind("BeforeTime") %>'
                                                Width="35px"></asp:TextBox></td>
                                        <td class="shipmentLabel">
                                            Check In Time:</td>
                                        <td>
                                            <asp:TextBox ID="txtCheckInTime" runat="server" CssClass="EditText" Text='<%# Bind("CheckInTime") %>'
                                                Width="35px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Estimated Date:</td>
                                        <td>
                                            <Discovery:DiscoveryNullText ID="txtEstimatedDeliveryDate" runat="server" CssClass="EditText"
                                                Text='<%# Bind("EstimatedDeliveryDate", "{0:d}") %>' Width="60px" DataType="System.DateTime"
                                                NullText=""></Discovery:DiscoveryNullText>
                                            <asp:ImageButton ID="btnCalcDeliveryDate" runat="server" OnClick="btnCalcDeliveryDate_Click"
                                                SkinID="ButtonCalculateDeliveryDate" /></td>
                                        <td>
                                            Estimated Time:</td>
                                        <td>
                                            *****</td>
                                        <td>
                                            Veh Max Weight:</td>
                                        <td>
                                            <asp:TextBox ID="txtVehicleMaxWeight" runat="server" CssClass="EditText" Text='<%# Bind("VehicleMaxWeight") %>'
                                                Width="50px"></asp:TextBox></td>
                                        <td>
                                            Tail Lift Req:</td>
                                        <td>
                                            <asp:CheckBox ID="chkTailLiftRequired" runat="server" Checked='<%# Bind("TailLiftRequired") %>' /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Stock Loc.:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlStockWarehouse" runat="server" CssClass="EditText" DataSourceID="WarehouseDataSource"
                                                DataTextField="Description" DataValueField="Code" SelectedValue='<%# Bind("StockWarehouseCode") %>'>
                                            </asp:DropDownList></td>
                                        <td>
                                            Delivery Loc.:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlDeliveryWarehouse" runat="server" CssClass="EditText" DataSourceID="WarehouseDataSource"
                                                DataTextField="Description" DataValueField="Code" SelectedValue='<%# Bind("DeliveryWarehouseCode") %>'>
                                            </asp:DropDownList></td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                <tr>
                                    <td style="width: 33%" valign="top">
                                        <fieldset>
                                            <legend>Shipment Contact</legend>
                                            <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                                <tr>
                                                    <td class="shipmentLabel">
                                                        Contact:</td>
                                                    <td class="shipmentValue">
                                                        <asp:TextBox ID="txtShipmentContactName" CssClass="EditText" runat="server" Text='<%# Eval("ShipmentContact.Name") %>'
                                                            Width="180px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Email:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtShipmentContactEmail" CssClass="EditText" runat="server" Text='<%# Eval("ShipmentContact.Email") %>'
                                                            Width="180px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Telephone:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtShipmentContactTelephone" CssClass="EditText" runat="server"
                                                            Text='<%# Eval("ShipmentContact.TelephoneNumber") %>' Width="180px"></asp:TextBox></td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                    <td style="width: 33%" valign="top">
                                        <fieldset>
                                            <legend>Shipment Address</legend>
                                            <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                                <tr>
                                                    <td class="shipmentLabel">
                                                        Name:</td>
                                                    <td class="shipmentValue">
                                                        <asp:TextBox ID="txtShipmentAddressName" CssClass="EditText" runat="server" Text='<%# Eval("ShipmentName") %>'
                                                            Width="185px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Address:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtShipmentAddress1" CssClass="EditText" runat="server" Text='<%# Eval("ShipmentAddress.Line1") %>'
                                                            Width="185px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:TextBox ID="txtShipmentAddress2" CssClass="EditText" runat="server" Text='<%# Eval("ShipmentAddress.Line2") %>'
                                                            Width="185px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:TextBox ID="txtShipmentAddress3" CssClass="EditText" runat="server" Text='<%# Eval("ShipmentAddress.Line3") %>'
                                                            Width="185px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:TextBox ID="txtShipmentAddress4" CssClass="EditText" runat="server" Text='<%# Eval("ShipmentAddress.Line4") %>'
                                                            Width="185px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:TextBox ID="txtShipmentAddress5" CssClass="EditText" runat="server" Text='<%# Eval("ShipmentAddress.Line5") %>'
                                                            Width="185px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Post Code:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtShipmentAddressPostCode" CssClass="EditText" runat="server" Text='<%# Eval("ShipmentAddress.PostCode") %>'
                                                            Width="80px"></asp:TextBox></td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                    <td style="width: 33%" valign="top">
                                        <fieldset>
                                            <legend>PAF Address</legend>
                                            <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                                <tr>
                                                    <td class="shipmentLabel">
                                                        Name:</td>
                                                    <td class="shipmentValue">
                                                        <asp:Label ID="lblPAFCustomerName" runat="server" Text='<%# Eval("ShipmentName") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Address:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtPAFAddress1" runat="server" CssClass="EditText" Width="185px"
                                                            Text='<%# Eval("PAFAddress.Line1") %>'></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:TextBox ID="txtPAFAddress2" runat="server" CssClass="EditText" Width="185px"
                                                            Text='<%# Eval("PAFAddress.Line2") %>'></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:TextBox ID="txtPAFAddress3" runat="server" CssClass="EditText" Width="185px"
                                                            Text='<%# Eval("PAFAddress.Line3") %>'></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:TextBox ID="txtPAFAddress4" runat="server" CssClass="EditText" Width="185px"
                                                            Text='<%# Eval("PAFAddress.Line4") %>'></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:TextBox ID="txtPAFAddress5" runat="server" CssClass="EditText" Width="185px"
                                                            Text='<%# Eval("PAFAddress.Line5") %>'></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Post Code:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtPAFAddressPostCode" runat="server" CssClass="EditText" Text='<%# Eval("PAFAddress.PostCode") %>'
                                                            Width="80px"></asp:TextBox></td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                            <fieldset>
                                <legend>Instructions</legend>
                                <asp:TextBox ID="txtInstructions" CssClass="EditText" runat="server" Text='<%# Bind("Instructions") %>'
                                    Rows="4" TextMode="MultiLine" Width="98%" ToolTip="Please enter the shipping instructions."></asp:TextBox>
                            </fieldset>
                        </asp:Panel>
                    </asp:Panel>
                    <%---- AJAX EXTENDERS ----%>
                    <ajaxToolkit:CollapsiblePanelExtender Enabled="false" ID="collapsiblePanelShipment"
                        runat="Server" TargetControlID="panelShipmentContent" ExpandControlID="imgShipmentMinMax"
                        CollapseControlID="imgShipmentMinMax" ImageControlID="imgShipmentMinMax" CollapsedSize="0"
                        SuppressPostBack="True" ExpandedImage="~/Images/Container/Min.gif" CollapsedImage="~/Images/Container/Max.gif" />
                    <ajaxToolkit:ToggleButtonExtender Enabled="false" ID="ToggleButtonExtender1" runat="server"
                        TargetControlID="chkTailLiftRequired" UncheckedImageUrl="~/images/uncheck.gif"
                        CheckedImageUrl="~/images/check.gif" ImageHeight="16" ImageWidth="16" />
                </InsertItemTemplate>
                <ItemTemplate>
                    <%---- Shipment Details ----%>
                    <asp:Panel ID="panelShipmentDetail" runat="server" CssClass="collapsePanelContainer"
                        Width="990px">
                        <asp:Panel ID="panelShipmentHeader" runat="server" CssClass="collapsePanelHeader"
                            Width="100%">
                            <span class="collapsePanelTitle">TDC Shipment Details (<%# string.Concat(Eval("OpCoCode"), "-", Eval("ShipmentNumber"), "-", Eval("DespatchNumber"), " ", Eval("CustomerName")) %>)</span><span
                                class="collapsePanelMinMax">
                                <asp:Image ID="imgShipmentMinMax" ImageUrl="~/Images/Container/Min.gif" runat="server" /></span>
                        </asp:Panel>
                        <asp:Panel ID="panelShipmentContent" runat="server" CssClass="collapsePanelContent"
                            Width="100%">
                            <fieldset>
                                <legend>Options</legend>
                                <asp:HyperLink ID="HyperLinkBack" NavigateUrl='<%# BackUrl %>' SkinID="HyperLinkBack"
                                    runat="server">Back</asp:HyperLink>
                                <asp:ImageButton ID="ButtonEdit" runat="server" CommandName="Edit" SkinID="ButtonEdit" />
                                <asp:ImageButton ID="btnOpCoShipmentDetail" SkinID="ButtonOpCoShipment" runat="server"
                                    CommandArgument='<%# Eval("OpCoShipmentId") %>' OnClick="btnOpCoShipmentDetail_Click"
                                    ToolTip="Displays OpCo shipment details" />
                                <asp:ImageButton ID="btnAuditDetail" SkinID="ButtonAuditDetails" runat="server" CommandArgument='<%# Eval("AuditId") %>'
                                    OnClick="btnAuditDetail_Click" ToolTip="Displays the audit entry for this shipment" />
                                <%---- Line Splits Start ----%>
                                <Discovery:TDCShipmentLinesSplit ID="TDCShipmentLinesSplits" Enabled='<%# (Discovery.BusinessObjects.Shipment.StatusEnum)System.Enum.Parse(typeof(Discovery.BusinessObjects.Shipment.StatusEnum), Eval("Status").ToString())!= Discovery.BusinessObjects.Shipment.StatusEnum.Completed && (Discovery.BusinessObjects.Shipment.StatusEnum)System.Enum.Parse(typeof(Discovery.BusinessObjects.Shipment.StatusEnum), Eval("Status").ToString())!= Discovery.BusinessObjects.Shipment.StatusEnum.Cancelled %>'
                                    runat="server" OnSaveComplete="ShipmentLineUpdated" />
                                <%---- Line Add Start ----%>
                                <Discovery:TDCShipmentLinesAdd ID="TDCShipmentLinesAdditions" runat="server" OnSaveComplete="ShipmentLineAdded" />
                                <%---- Print options ----%>
                                <asp:ImageButton ID="btnPrintTransConv" SkinID="ButtonShipmentPrintTransConv" CommandName="PrintTransConv"
                                    OnClick="btnPrintTransConv_Click" CommandArgument='<%# Eval("Id") %>' runat="server"
                                    ToolTip="Prints the transafer/conversion note" />
                                <asp:ImageButton ID="btnPrintDelColl" SkinID="ButtonShipmentPrintDelColl" CommandName="PrintDelColl"
                                    OnClick="btnPrintDelColl_Click" CommandArgument='<%# Eval("Id") %>' runat="server"
                                    ToolTip="Prints the delivery/collection note" />
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
                                            <b>
                                                <asp:Label ID="lblOpCoCode" runat="server" Text='<%# Eval("OpCoCode") %>'></asp:Label></b></td>
                                        <td class="shipmentLabel">
                                            Number:</td>
                                        <td class="shipmentValue">
                                            <b>
                                                <asp:Label ID="lblShipmentNumber" runat="server" Text='<%# Eval("ShipmentNumber") %>'></asp:Label></b></td>
                                        <td class="shipmentLabel">
                                            Despatch #:</td>
                                        <td class="shipmentValue">
                                            <asp:Label ID="lblDespatchNumber" runat="server" Text='<%# Eval("DespatchNumber") %>'></asp:Label></td>
                                        <td class="shipmentLabel">
                                            Status:</td>
                                        <td class="shipmentValue">
                                            <asp:Image ID="imgStatus" runat="server" ImageUrl='<%# "~/Images/Icons/16x16/" + Eval("Status") + ".gif" %>'
                                                ToolTip='<%# Eval("Status") %>' /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Route Code:</td>
                                        <td>
                                            <asp:Label ID="lblRouteCode" runat="server" Text='<%# Eval("Route.Description") %>'></asp:Label></td>
                                        <td>
                                            Trans. Type:</td>
                                        <td>
                                            <asp:Label ID="lblTransactionType" runat="server" Text='<%# Eval("TransactionType.Description") %>'></asp:Label></td>
                                        <td>
                                            Trans. Sub Type:</td>
                                        <td>
                                            <asp:Label ID="lblTransactionSubType" runat="server" Text='<%# Eval("TransactionSubType.Description") %>'></asp:Label></td>
                                        <td>
                                            Generated:</td>
                                        <td>
                                            <asp:Label ID="lblGeneratedDateTime" runat="server" Text='<%# Eval("GeneratedDateTime") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            File Seq. #:</td>
                                        <td>
                                            <asp:Label ID="lblOpCoSequenceNumber" runat="server" Text='<%# Eval("OpCoSequenceNumber") %>'></asp:Label></td>
                                        <td>
                                            Division Code:</td>
                                        <td>
                                            <asp:Label ID="lblDivision" runat="server" Text='<%# Eval("DivisionCode") %>'></asp:Label></td>
                                        <td>
                                            Sales Branch:</td>
                                        <td colspan="3">
                                            <asp:Label ID="lblSalesBranchCode" runat="server" Text='<%# Eval("SalesBranchCode") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Type:</td>
                                        <td>
                                            <asp:Label ID="lblType" runat="server" Text='<%# Eval("Type") %>'></asp:Label></td>
                                        <td>
                                            OpCo Held:</td>
                                        <td>
                                            <asp:Image ID="imgOpCoHeld" runat="server" ImageUrl='<%# Eval("OpCoHeld", "~/Images/{0}") + ".gif" %>' /></td>
                                        <td>
                                            Is Recurring:</td>
                                        <td>
                                            <asp:Image ID="imgIsRecurring" runat="server" ImageUrl='<%# Eval("IsRecurring", "~/Images/{0}") + ".gif" %>' /></td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset>
                                <legend>Delivery Details</legend>
                                <table border="0" width="100%" cellspacing="0" cellpadding="2">
                                    <tr>
                                        <td class="shipmentLabel">
                                            Stock W/house:</td>
                                        <td class="shipmentValue">
                                            <b>
                                                <asp:Label ID="lblStockWareHouseCode" runat="server" Text='<%# Eval("StockWarehouse.Description") %>'></asp:Label></b></td>
                                        <td class="shipmentLabel">
                                            Delivery W/house:</td>
                                        <td class="shipmentValue">
                                            <b>
                                                <asp:Label ID="lblDeliveryWarehouseCode" runat="server" Text='<%# Eval("DeliveryWarehouse.Description") %>'></asp:Label></b></td>
                                        <td class="shipmentLabel">
                                        </td>
                                        <td class="shipmentValue">
                                            &nbsp;</td>
                                        <td class="shipmentLabel">
                                            &nbsp;</td>
                                        <td class="shipmentValue">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="shipmentLabel">
                                            Required Date:</td>
                                        <td class="shipmentValue">
                                            <asp:Label ID="lblRequiredDeliveryDate" runat="server" Text='<%# Eval("RequiredShipmentDate", "{0:d}") %>'></asp:Label></td>
                                        <td class="shipmentLabel">
                                            After Time:</td>
                                        <td class="shipmentValue">
                                            <asp:Label ID="lblAfterTime" runat="server" Text='<%# Eval("AfterTime") %>'></asp:Label></td>
                                        <td class="shipmentLabel">
                                            Before Time:</td>
                                        <td class="shipmentValue">
                                            <asp:Label ID="lblBeforeTime" runat="server" Text='<%# Eval("BeforeTime") %>'></asp:Label></td>
                                        <td class="shipmentLabel">
                                            Check In Time:</td>
                                        <td class="shipmentValue">
                                            <asp:Label ID="lblCheckInTime" runat="server" Text='<%# Eval("CheckInTime") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Estimated Date:</td>
                                        <td>
                                            <Discovery:DiscoveryNullLabel ID="lblEstimatedDeliveryDate" runat="server" Text='<%# Eval("EstimatedDeliveryDate", "{0:d}") %>'
                                                DataType="System.DateTime" NullText="NA"></Discovery:DiscoveryNullLabel>
                                        </td>
                                        <td>
                                            Estimated Time:</td>
                                        <td>
                                            <asp:Label ID="lblEstimatedDeliveryTime" runat="server" Text='<%# Eval("EstimatedDeliveryDate", "{0:t}") %>' /></td>
                                        <td>
                                            Veh Max Weight:</td>
                                        <td>
                                            <asp:Label ID="lblVehicleMaxWeight" runat="server" Text='<%# Eval("VehicleMaxWeight") %>'></asp:Label></td>
                                        <td>
                                            Volume:</td>
                                        <td>
                                            <asp:Label ID="lblVolume" runat="server" Text='<%# Eval("Volume") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Actual Date:</td>
                                        <td>
                                            <Discovery:DiscoveryNullLabel ID="lblActualDeliveryDate" runat="server" Text='<%# Eval("ActualDeliveryDate", "{0:d}") %>'
                                                DataType="System.DateTime" NullText="NA"></Discovery:DiscoveryNullLabel>
                                        </td>
                                        <td>
                                            Actual Time:</td>
                                        <td>
                                            <asp:Label ID="lblActualDeliveryTime" runat="server" Text='<%# Eval("ActualDeliveryDate", "{0:t}") %>' /></td>
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
                                            *****</td>
                                        <td>
                                            Cost:</td>
                                        <td>
                                            *****</td>
                                        <td>
                                            Hours:</td>
                                        <td>
                                            *****</td>
                                        <td>
                                            Gross Weight:</td>
                                        <td>
                                            <asp:Label ID="lblGrossWeight" runat="server" Text='<%# Eval("GrossWeight", "{0:F}") %>'></asp:Label></td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset>
                                <legend>Route</legend>
                                <table border="0" cellspacing="0" cellpadding="2" width="100%">
                                    <tr>
                                        <td class="shipmentLabel">
                                            Next Day:</td>
                                        <td class="shipmentValue">
                                            <asp:Image ID="imgIsNextDay" runat="server" ImageUrl='<%# Eval("Route.IsNextDay", "~/Images/{0}") + ".gif" %>' /></td>
                                        <td class="shipmentLabel">
                                            Same Day:</td>
                                        <td class="shipmentValue">
                                            <asp:Image ID="imgIsSameDay" runat="server" ImageUrl='<%# Eval("Route.IsSameDay", "~/Images/{0}") + ".gif" %>' /></td>
                                        <td class="shipmentLabel">
                                            Customer Collect:</td>
                                        <td class="shipmentValue">
                                            <asp:Image ID="imgIsCustomerCollection" runat="server" ImageUrl='<%# Eval("Route.IsCollection", "~/Images/{0}") + ".gif" %>' /></td>
                                        <td class="shipmentLabel">
                                            Special:</td>
                                        <td class="shipmentValue">
                                            <asp:Image ID="imgIsSpecial" runat="server" ImageUrl='<%# Eval("Route.IsSpecial", "~/Images/{0}") + ".gif" %>' /></td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset>
                                <legend>Transaction Type</legend>
                                <table border="0" cellspacing="0" cellpadding="2" width="100%">
                                    <tr>
                                        <td class="shipmentLabel">
                                            Stock:</td>
                                        <td class="shipmentValue">
                                            <asp:Image ID="imgIsStock" runat="server" ImageUrl='<%# Eval("TransactionType.IsStock", "~/Images/{0}") + ".gif" %>' /></td>
                                        <td class="shipmentLabel">
                                            Non Stock:</td>
                                        <td class="shipmentValue">
                                            <asp:Image ID="imgIsNonStock" runat="server" ImageUrl='<%# Eval("TransactionType.IsNonStock", "~/Images/{0}") + ".gif" %>' /></td>
                                        <td class="shipmentLabel">
                                            TDC Collect:</td>
                                        <td class="shipmentValue">
                                            <asp:Image ID="imgIsCollection" runat="server" ImageUrl='<%# Eval("TransactionType.IsCollection", "~/Images/{0}") + ".gif" %>' /></td>
                                        <td class="shipmentLabel">
                                            Sample:</td>
                                        <td class="shipmentValue">
                                            <asp:Image ID="imgIsSample" runat="server" ImageUrl='<%# Eval("TransactionType.IsSample", "~/Images/{0}") + ".gif" %>' /></td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset>
                                <legend>Transaction Sub Type</legend>
                                <table border="0" cellspacing="0" cellpadding="2" width="100%">
                                    <tr>
                                        <td class="shipmentLabel">
                                            Normal:</td>
                                        <td class="shipmentValue">
                                            <asp:Image ID="imgIsNormal" runat="server" ImageUrl='<%# Eval("TransactionSubType.IsNormal", "~/Images/{0}") + ".gif" %>' /></td>
                                        <td class="shipmentLabel">
                                            Transfer:</td>
                                        <td class="shipmentValue">
                                            <asp:Image ID="imgIsTransfer" runat="server" ImageUrl='<%# Eval("TransactionSubType.IsTransfer", "~/Images/{0}") + ".gif" %>' /></td>
                                        <td class="shipmentLabel">
                                            Local Conv.:</td>
                                        <td class="shipmentValue">
                                            <asp:Image ID="imgIsLocalConv" runat="server" ImageUrl='<%# Eval("TransactionSubType.IsLocalConversion", "~/Images/{0}") + ".gif" %>' /></td>
                                        <td class="shipmentLabel">
                                            3rd Party Conv.:</td>
                                        <td class="shipmentValue">
                                            <asp:Image ID="imgIs3rdPartyConv" runat="server" ImageUrl='<%# Eval("TransactionSubType.Is3rdPartyConversion", "~/Images/{0}") + ".gif" %>' /></td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset>
                                <legend>Routing Details</legend>
                                <table border="0" cellspacing="0" cellpadding="2" width="100%">
                                    <tr>
                                        <td class="shipmentLabel">
                                            Route Trip:</td>
                                        <td class="shipmentValue">
                                            <Discovery:DiscoveryNullLabel ID="lblRouteTrip" runat="server" DataType="System.String"
                                                NullText="NA" Text='<%# Eval("RouteTrip") %>'></Discovery:DiscoveryNullLabel></td>
                                        <td class="shipmentLabel">
                                            Split Sequence:</td>
                                        <td class="shipmentValue">
                                            <asp:Label ID="lblSplitSequence" runat="server" Text='<%# Eval("SplitSequence") %>'></asp:Label></td>
                                        <td class="shipmentLabel">
                                            Sent To WMS:</td>
                                        <td class="shipmentValue">
                                            <Discovery:DiscoveryNullLabel ID="lblSentToWMS" runat="server" DataType="System.DateTime"
                                                Text='<%# Eval("SentToWMS") %>' NullText="NA"></Discovery:DiscoveryNullLabel>
                                        </td>
                                        <td class="shipmentLabel">
                                            Route Date Time:</td>
                                        <td class="shipmentValue">
                                            <Discovery:DiscoveryNullLabel ID="lblRoutingDateTime" runat="server" DataType="System.DateTime"
                                                Text='<%# Eval("RoutingDateTime") %>' NullText="NA"></Discovery:DiscoveryNullLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Route Drop:</td>
                                        <td>
                                            <Discovery:DiscoveryNullLabel ID="lblRouteDrop" runat="server" DataType="System.Int32"
                                                NullText="NA" Text='<%# Eval("RouteDrop") %>'></Discovery:DiscoveryNullLabel></td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                <tr>
                                    <td colspan="1" style="width: 33%;" valign="top">
                                        <fieldset>
                                            <legend>Op Co Contact</legend>
                                            <table width="100%" cellspacing="0" cellpadding="2">
                                                <tr>
                                                    <td class="shipmentLabel">
                                                        Contact:</td>
                                                    <td>
                                                        <asp:Label ID="lblOpCoContactName" runat="server" Text='<%# Eval("OpCoContact.Name") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Email:</td>
                                                    <td>
                                                        <asp:Label ID="lblOpCoContactEmail" runat="server" Text='<%# Eval("OpCoContact.Email") %>'></asp:Label></td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                    <td colspan="1" style="width: 33%;" valign="top">
                                        <fieldset>
                                            <legend>Shipment Contact</legend>
                                            <table width="100%" cellspacing="0" cellpadding="2">
                                                <tr>
                                                    <td class="shipmentLabel">
                                                        Contact:</td>
                                                    <td>
                                                        <asp:Label ID="lblShipmentContactName" runat="server" Text='<%# Eval("ShipmentContact.Name") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Email:</td>
                                                    <td>
                                                        <asp:Label ID="lblShipmentContactEmail" runat="server" Text='<%# Eval("ShipmentContact.Email") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Telephone:</td>
                                                    <td>
                                                        <asp:Label ID="lblShipmentContactTelephone" runat="server" Text='<%# Eval("ShipmentContact.TelephoneNumber") %>'></asp:Label></td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                    <td colspan="1" style="width: 33%;" valign="top">
                                        <fieldset>
                                            <legend>PAF Details</legend>
                                            <table width="100%" cellspacing="0" cellpadding="2">
                                                <tr>
                                                    <td class="shipmentLabel">
                                                        Easting:</td>
                                                    <td>
                                                        <asp:Label ID="lblPAFEasting" runat="server" Text='<%# Eval("PAFAddress.Easting") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Northing:</td>
                                                    <td>
                                                        <asp:Label ID="lblPAFNorthing" runat="server" Text='<%# Eval("PAFAddress.Northing") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        DPS:</td>
                                                    <td>
                                                        <asp:Label ID="lblPAFDPS" runat="server" Text='<%# Eval("PAFAddress.DPS") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Status:</td>
                                                    <td>
                                                        <asp:Label ID="lblPAFStatus" runat="server" Text='<%# Eval("PAFAddress.Match") %>'></asp:Label></td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="1" style="width: 33%" valign="top">
                                        <fieldset>
                                            <legend>Customer Address</legend>
                                            <table width="100%" cellspacing="0" cellpadding="2">
                                                <tr>
                                                    <td class="shipmentLabel">
                                                        Name:</td>
                                                    <td>
                                                        <asp:Label ID="lblCustomerAddressName" runat="server" Text='<%# Eval("CustomerName") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Address:</td>
                                                    <td>
                                                        <asp:Label ID="lblCustomerAddress1" runat="server" Text='<%# Eval("CustomerAddress.Line1") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="lblCustomerAddress2" runat="server" Text='<%# Eval("CustomerAddress.Line2") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="lblCustomerAddress3" runat="server" Text='<%# Eval("CustomerAddress.Line3") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="lblCustomerAddress4" runat="server" Text='<%# Eval("CustomerAddress.Line4") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="lblCustomerAddress5" runat="server" Text='<%# Eval("CustomerAddress.Line5") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Post Code:</td>
                                                    <td>
                                                        <asp:Label ID="lblCustomerAddressPostCode" runat="server" Text='<%# Eval("CustomerAddress.PostCode") %>'></asp:Label></td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                    <td colspan="1" style="width: 33%" valign="top">
                                        <fieldset>
                                            <legend>Shipment Address</legend>
                                            <table width="100%" cellspacing="0" cellpadding="2">
                                                <tr>
                                                    <td class="shipmentLabel">
                                                        Name:</td>
                                                    <td>
                                                        <asp:Label ID="lblShipmentName" runat="server" Text='<%# Eval("ShipmentName") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Address:</td>
                                                    <td>
                                                        <asp:Label ID="lblShipmentAddress1" runat="server" Text='<%# Eval("ShipmentAddress.Line1") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="lblShipmentAddress2" runat="server" Text='<%# Eval("ShipmentAddress.Line2") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="lblShipmentAddress3" runat="server" Text='<%# Eval("ShipmentAddress.Line3") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="lblShipmentAddress4" runat="server" Text='<%# Eval("ShipmentAddress.Line4") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="lblShipmentAddress5" runat="server" Text='<%# Eval("ShipmentAddress.Line5") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Post Code:</td>
                                                    <td>
                                                        <asp:Label ID="lblShipmentAddressPostCode" runat="server" Text='<%# Eval("ShipmentAddress.PostCode") %>'></asp:Label></td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                    <td colspan="1" style="width: 33%" valign="top">
                                        <fieldset>
                                            <legend>PAF Address</legend>
                                            <table width="100%" cellspacing="0" cellpadding="2" border="0">
                                                <tr>
                                                    <td class="shipmentLabel">
                                                        Name:</td>
                                                    <td>
                                                        <asp:Label ID="lblPAFCustomerName" runat="server" Text='<%# Eval("ShipmentName") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Address:</td>
                                                    <td>
                                                        <asp:Label ID="lblPAFAddress1" runat="server" Text='<%# Eval("PAFAddress.Line1") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="lblPAFAddress2" runat="server" Text='<%# Eval("PAFAddress.Line2") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="lblPAFAddress3" runat="server" Text='<%# Eval("PAFAddress.Line3") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="lblPAFAddress4" runat="server" Text='<%# Eval("PAFAddress.Line4") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="lblPAFAddress5" runat="server" Text='<%# Eval("PAFAddress.Line5") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Post Code:</td>
                                                    <td>
                                                        <asp:Label ID="lblPAFAddressPostCode" runat="server" Text='<%# Eval("PAFAddress.PostCode") %>'></asp:Label></td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                            <fieldset>
                                <legend>Instructions</legend>
                                <asp:Label ID="lblInstructions" runat="server" CssClass="Instructions" Text='<%# Eval("Instructions") %>'></asp:Label>&nbsp;
                            </fieldset>
                        </asp:Panel>
                    </asp:Panel>
                    <%---- AJAX EXTENDERS ----%>
                    <ajaxToolkit:CollapsiblePanelExtender ID="collapsiblePanelShipment" runat="Server"
                        TargetControlID="panelShipmentContent" ExpandControlID="imgShipmentMinMax" CollapseControlID="imgShipmentMinMax"
                        ImageControlID="imgShipmentMinMax" CollapsedSize="0" SuppressPostBack="True"
                        ExpandedImage="~/Images/Container/Min.gif" CollapsedImage="~/Images/Container/Max.gif" />
                    <asp:Panel ID="panelShipmentLineDetail" runat="server" CssClass="collapsePanelContainer"
                        Width="990px">
                        <asp:Panel ID="panelShipmentLineHeader" runat="server" CssClass="collapsePanelHeader"
                            Width="100%">
                            <span class="collapsePanelTitle">TDC Shipment Lines (<%# Eval("ShipmentLines.Count") %>)</span><span
                                class="collapsePanelMinMax">
                                <asp:Image ID="imgShipmentLinesMinMax" ImageUrl="~/Images/Container/Min.gif" runat="server" /></span>
                        </asp:Panel>
                        <asp:Panel ID="panelShipmentLineContent" runat="server" CssClass="collapsePanelContent"
                            Width="100%">
                            <%---- Shipment Lines Start ----%>
                            <Discovery:TDCShipmentLines ID="ShipmentLines" runat="server" OnSaveComplete="ShipmentLineUpdated" />
                            <%---- Shipment Lines End ----%>
                            <%---- AJAX EXTENDERS ----%>
                            <ajaxToolkit:CollapsiblePanelExtender ID="collapsiblePanelShipmentLines" runat="Server"
                                TargetControlID="panelShipmentLineContent" ExpandControlID="imgShipmentLinesMinMax"
                                CollapseControlID="imgShipmentLinesMinMax" Collapsed="True" ImageControlID="imgShipmentLinesMinMax"
                                CollapsedSize="0" SuppressPostBack="True" ExpandedImage="~/Images/Container/Min.gif"
                                CollapsedImage="~/Images/Container/Max.gif" />
                        </asp:Panel>
                    </asp:Panel>
                </ItemTemplate>
            </asp:FormView>
            <%---- Form view for TDC Shipments End ----%>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ShowSummary="False" />
    <%---- Main Object Data Source ----%>
    <asp:ObjectDataSource ID="TDCShipmentDataSource" runat="server" DataObjectTypeName="Discovery.BusinessObjects.TDCShipment"
        DeleteMethod="DeleteShipment" InsertMethod="SaveShipment" UpdateMethod="SaveShipment"
        SelectMethod="GetShipment" TypeName="Discovery.BusinessObjects.Controllers.TDCShipmentController"
        OldValuesParameterFormatString="original_{0}" OnInserting="TDCShipmentDataSource_Inserting"
        OnUpdating="TDCShipmentDataSource_Updating" ConvertNullToDBNull="True">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="-1" Name="ShipmentId" QueryStringField="Id"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

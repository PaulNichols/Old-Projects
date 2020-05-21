<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TDCShipmentLinesAdd.ascx.cs"
    Inherits="Discovery.UI.Web.UserControls.TDCShipmentLinesAdd" %>
<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="Discovery" %>
<!-- Button To Display Or Hide Popup -->
<asp:ImageButton ID="btnAddLinePopup" runat="server" SkinID="ButtonAddLine" ToolTip="Adds a line to this shipment" />
<!-- Add Popup  Start -->
<asp:Panel ID="panelShipmentAdd" runat="server" CssClass="AddLinesPopup">
    <table width="100%">
        <tr>
            <td>
                <asp:Panel ID="panelAddHeader" runat="server" CssClass="collapsePanelHeader" Width="100%">
                    <span class="collapsePanelTitle">Add Shipment Line</span>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" width="100%">
                    <tr>
                        <td valign="top" style="width: 40%" rowspan="2">
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td valign="top">
                                        <fieldset>
                                            <legend>Description</legend>
                                            <asp:TextBox MaxLength="50" TextMode="SingleLine" Width="330px" ID="txtDescription1"
                                                runat="server" CssClass="EditText" /></fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <fieldset>
                                            <legend>Description</legend>
                                            <asp:TextBox MaxLength="50" TextMode="SingleLine" Width="330px" ID="txtDescription2"
                                                runat="server" CssClass="EditText" /></fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <fieldset>
                                            <legend>Conversion Instructions</legend>
                                            <asp:TextBox MaxLength="50" TextMode="MultiLine" Width="330px" Height="50" ID="txtConversionInstructions"
                                                runat="server" CssClass="EditText" /></fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <fieldset>
                                            <legend>Packing</legend>
                                            <asp:TextBox MaxLength="50" TextMode="MultiLine" Width="330px" Height="50" ID="txtPacking"
                                                runat="server" CssClass="EditText" /></fieldset>
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
                                            <asp:TextBox ID="txtProductCode" runat="server" CssClass="EditText" ReadOnly="True" /></td>
                                    </tr>
                                    <tr>
                                        <td nowrap="true">
                                            Quantity:</td>
                                        <td width="100%">
                                            <Discovery:DiscoveryNumericText ID="txtQuantity" runat="server" CssClass="EditText" /></td>
                                    </tr>
                                    <tr>
                                        <td nowrap="true">
                                            Quantity Unit:</td>
                                        <td width="100%">
                                            <asp:TextBox ID="txtQuantityUnit" runat="server" CssClass="EditText" /></td>
                                    </tr>
                                    <tr>
                                        <td nowrap="true">
                                            Customer Reference:</td>
                                        <td width="100%">
                                            <asp:TextBox ID="txtCustomerReference" runat="server" CssClass="EditText" /></td>
                                    </tr>
                                    <tr>
                                        <td nowrap="true">
                                            Product Group:</td>
                                        <td width="100%">
                                            <asp:TextBox ID="txtProductGroup" runat="server" CssClass="EditText" /></td>
                                    </tr>
                                    <tr>
                                        <td nowrap="true">
                                            Conversion Qty:</td>
                                        <td width="100%">
                                            <Discovery:DiscoveryNumericText ID="txtConversionQuantity" runat="server" CssClass="EditText" /></td>
                                    </tr>
                                    <tr>
                                        <td nowrap="true">
                                            Is Panel:</td>
                                        <td>
                                            <asp:CheckBox ID="chkIsPanel" runat="server" />
                                    </tr>
                                    <tr>
                                        <td nowrap="true">
                                            ISO 9000 Approved:</td>
                                        <td>
                                            <asp:CheckBox ID="chkIsISO9000Approved" runat="server" />
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
                                                DataValueField="Code" DataSourceID="dataSourceLoadCategory">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap="true">
                                            Net Weight:</td>
                                        <td width="100%">
                                            <Discovery:DiscoveryNumericText ID="txtNetWeight" runat="server" MaxLength="6" Width="50"
                                                CssClass="EditText" /></td>
                                    </tr>
                                    <tr>
                                        <td nowrap="true">
                                            Width:</td>
                                        <td width="100%">
                                            <Discovery:DiscoveryNumericText ID="txtWidth" runat="server" MaxLength="6" Width="50"
                                                CssClass="EditText" /></td>
                                    </tr>
                                    <tr>
                                        <td nowrap="true">
                                            Length:</td>
                                        <td>
                                            <Discovery:DiscoveryNumericText ID="txtLength" runat="server" MaxLength="6" Width="50"
                                                CssClass="EditText" /></td>
                                    </tr>
                                    <tr>
                                        <td nowrap="true">
                                            Volume:</td>
                                        <td>
                                            <Discovery:DiscoveryNumericText ID="txtVolume" runat="server" MaxLength="6" Width="50"
                                                CssClass="EditText" /></td>
                                    </tr>
                                    <tr>
                                        <td nowrap="true">
                                            Microns:
                                        </td>
                                        <td>
                                            <Discovery:DiscoveryNumericText ID="txtMicrons" runat="server" MaxLength="6" Width="50"
                                                CssClass="EditText" /></td>
                                    </tr>
                                    <tr>
                                        <td nowrap="true">
                                            Grammage:
                                        </td>
                                        <td>
                                            <Discovery:DiscoveryNumericText ID="txtGrammage" runat="server" MaxLength="6" Width="50"
                                                CssClass="EditText" /></td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <fieldset>
                                <legend>Options</legend>
                                <asp:ImageButton ID="btnAddLine" SkinID="ButtonSave" runat="server" OnClick="btnAddLine_Click" />
                                <asp:ImageButton ID="btnSplitCancel" SkinID="ButtonCancel" runat="server" CausesValidation="false" />
                                <asp:HiddenField ID="txtChecksum" runat="server" Value='<%# Eval("Checksum") %>' />
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Panel>
<!-- Add Popup  End -->
<!-- Load Category Code Data Source -->
<asp:ObjectDataSource ID="dataSourceLoadCategory" runat="server" SelectMethod="GetLoadCategories"
    TypeName="Discovery.BusinessObjects.Controllers.LoadCategoryController" OldValuesParameterFormatString="original_{0}" />
<!-- AJAX EXTENDERS -->
<ajaxToolkit:ModalPopupExtender runat="server" ID="popupShipmentAdd" TargetControlID="btnAddLinePopup"
    PopupControlID="panelShipmentAdd" BackgroundCssClass="ModalPopup" CancelControlID="btnSplitCancel" />

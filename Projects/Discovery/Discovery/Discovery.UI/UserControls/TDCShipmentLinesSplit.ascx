<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TDCShipmentLinesSplit.ascx.cs"
    Inherits="Discovery.UI.Web.UserControls.TDCShipmentLinesSplit" %>
<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="Discovery" %>
<!-- Button To Display Or Hide Popup -->
<asp:ImageButton ID="btnSplitShipment" runat="server" SkinID="ButtonSplitShipment"
    ToolTip="Split this shipment by altering the product quantities." Enabled='<%# Enabled %>' />
<!-- Split Popup  Start -->
<asp:Panel ID="panelSplitPopup" runat="server" CssClass="SplitPopup">
    <table align="center" width="100%">
        <tr>
            <td>
                <asp:Panel ID="panelAddHeader" runat="server" CssClass="collapsePanelHeader" Width="100%">
                    <span class="collapsePanelTitle">Split Shipment</span>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
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
                            <asp:LinkButton ID="lnkSplitQuantity" runat="server" Text="Split Qty" Width="70px"
                                CssClass="pagerTitle"></asp:LinkButton>
                            <asp:LinkButton ID="lnkWidth" runat="server" Text="Width" Width="50px" CssClass="pagerTitle"
                                CommandName="Sort" CommandArgument="Width"></asp:LinkButton>
                            <asp:LinkButton ID="lnkLength" runat="server" Text="Length" Width="50px" CssClass="pagerTitle"
                                CommandName="Sort" CommandArgument="Length"></asp:LinkButton>
                            <asp:LinkButton ID="lnkVolume" runat="server" Text="Volume" Width="52px" CssClass="pagerTitle"
                                CommandName="Sort" CommandArgument="Volume"></asp:LinkButton>
                            <asp:LinkButton ID="lnkNetWeight" runat="server" Text="Net" Width="50px" CssClass="pagerTitle"
                                CommandName="Sort" CommandArgument="NetWeight"></asp:LinkButton>
                        </asp:Panel>
                        <div class="SplitScrollContent">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Panel CssClass="shipmentLineHeaderNormalNoClick" ID="panelHeader" runat="server">
                            <asp:Label ID="lblLineNumber" runat="server" CssClass="accordionPanelTitle" Width="20px"
                                Text='<%# Eval("LineNumber") %>'></asp:Label>
                            <asp:Label ID="lblProductCode" runat="server" CssClass="accordionPanelTitle" Width="90px"
                                Text='<%# Eval("ProductCode") %>'></asp:Label>
                            <asp:Label ID="lblDescription1" runat="server" CssClass="accordionPanelTitle" Width="200px"
                                Text='<%# Eval("Description1") %>'></asp:Label>
                            <asp:Label ID="lblQuantity" runat="server" CssClass="accordionPanelTitle" Width="40px"
                                Text='<%# Eval("Quantity") %>'></asp:Label>
                            <Discovery:DiscoveryNumericText ID="txtSplitQuantity" runat="server" CssClass="EditText"
                                Width="50px" Enabled='<%# Convert.ToInt32(Eval("Quantity")) > 0 %>' Text="0"></Discovery:DiscoveryNumericText>
                            <asp:RangeValidator ID="valSplitQuantity" ControlToValidate="txtSplitQuantity" EnableClientScript="true"
                                Display="None" SetFocusOnError="true" Type="Integer" MinimumValue="0" MaximumValue='<%# Eval("Quantity") %>'
                                runat="server" ErrorMessage='<%# "Line " + Eval("LineNumber") + " quantity must be between 0 and " + Eval("Quantity") + "." %>'></asp:RangeValidator>
                            <img border="0" src="../Images/1x1.gif" width="20px" alt="" />
                            <asp:Label ID="lblWidth" runat="server" CssClass="accordionPanelTitle" Width="50px"
                                Text='<%# Eval("Width", "{0:d}") %>'></asp:Label>
                            <asp:Label ID="lblLength" runat="server" CssClass="accordionPanelTitle" Width="50px"
                                Text='<%# Eval("Length", "{0:d}")%>'></asp:Label>
                            <asp:Label ID="lblVolume" runat="server" CssClass="accordionPanelTitle" Width="50px"
                                Text='<%# Eval("Volume") %>'></asp:Label>
                            <asp:Label ID="lblNetWeight" runat="server" CssClass="accordionPanelTitle" Width="50px"
                                Text='<%# Eval("NetWeight", "{0:F2}") %>'></asp:Label>
                        </asp:Panel>
                    </ItemTemplate>
                    <FooterTemplate>
                        </div>
                    </FooterTemplate>
                </Discovery:DiscoveryPagedRepeater>
            </td>
        </tr>
    </table>
    <fieldset>
        <legend>Quantities, weights and Volumes</legend>
        <asp:CheckBox ID="chkAlterSource" runat="server" Checked="true" Text="Automatically adjust source shipment values?"
            ToolTip="Indicates whether the source shipment will have its quantities reduced by the split quantities" />
        &nbsp;<asp:CheckBox ID="chkAlterWeight" runat="server" Checked="true" Text="Automatically adjust weights and volumes?"
            ToolTip="Indicates whether weight and volume should be recalculated based on adjusted quantities." />
    </fieldset>
    <fieldset>
        <legend>Options</legend>
        <asp:ImageButton ID="btnSplitConfirm" SkinID="ButtonSave" runat="server" OnClick="btnSplitConfirm_Click" />
        <asp:ImageButton ID="btnSplitCancel" SkinID="ButtonCancel" runat="server" CausesValidation="false" />
        <asp:Button ID="btnHidddenOk" runat="server" Text="OK" Visible="True" Width="0" Height="0" />
    </fieldset>
</asp:Panel>
<!-- Split Popup End -->

<!-- AJAX EXTENDERS -->
<ajaxToolkit:ModalPopupExtender ID="popupShipmentSplit" runat="server" TargetControlID="btnSplitShipment"
    PopupControlID="panelSplitPopup" BackgroundCssClass="ModalPopup" CancelControlID="btnSplitCancel" />

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

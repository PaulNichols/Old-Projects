<%@ Page Language="C#" MasterPageFile="~/Main.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="SummaryByRoute.aspx.cs" Inherits="Discovery.UI.Web.Shipments.SummaryByRoute"
    Title="Summary By Route" %>

<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="Discovery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <div class="PageTitle">
        Summary By Route</div>
    <hr />
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
                        <asp:DropDownList ID="DropDownListDeliveryLocation" runat="server" Width="288px"
                            DataSourceID="WarehouseObjectDataSource" DataTextField="CodeAndDescription" DataValueField="Id" OnDataBound="DropDownListDeliveryLocation_DataBound">
                        </asp:DropDownList><asp:ObjectDataSource ID="WarehouseObjectDataSource" runat="server"
                            SelectMethod="GetWarehouses" TypeName="Discovery.BusinessObjects.Controllers.WarehouseController">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="CodeAndDescription" Name="sortExpression" Type="String" />
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
                    </td>
                    <td>
                        <asp:CheckBox ID="CheckBoxIncludeSpecials" runat="server" Text="Include Collections, Specials and Same Days" />
                    </td>
                    <td>
                    </td>
                    <td align="right">
                        <asp:ImageButton ID="ImageButtonSearch" runat="server" OnClick="ImageButtonSearch_Click"
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
    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
        <contenttemplate>
<Discovery:DiscoveryGrid id="GridView1" runat="server" DataSourceID="ShipmentObjectDataSource" DoHiLiteRow="False" ShowFooter="True" DetailURL DefaultSortExpression="RouteCode" OnRowDataBound="GridView1_RowDataBound" AutoGenerateColumns="False"><Columns>
<asp:TemplateField SortExpression="RouteCode" HeaderText="Route Code"><ItemTemplate>
<asp:HyperLink id="HyperLinkRouteCode" runat="server" Text='<%# Bind("RouteCode") %>' ></asp:HyperLink> 
</ItemTemplate>
<FooterTemplate>
<b>Totals:</b>
</FooterTemplate>
</asp:TemplateField>


<asp:TemplateField SortExpression="Trunked" HeaderText="Trunked  (kg)"><FooterTemplate>
<asp:Literal id="LiteralTrunked" runat="server" ></asp:Literal>
</FooterTemplate>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
<ItemTemplate>
<asp:HyperLink id="HyperLinkTrunked" runat="server" Text='<%# Bind("Trunked","{0:F2}") %>' ></asp:HyperLink> 
</ItemTemplate>

<FooterStyle HorizontalAlign="Right" Font-Bold="True"></FooterStyle>
</asp:TemplateField>
<asp:TemplateField SortExpression="ExBranch" HeaderText="Ex-Branch (kg)"><FooterTemplate>
<asp:Literal id="LiteralExBranchTotal" runat="server" ></asp:Literal>
</FooterTemplate>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
<ItemTemplate>
<asp:Literal id="LiteralExBranch" runat="server" Text='<%# Bind("ExBranch","{0:F2}") %>' ></asp:Literal> 
</ItemTemplate>

<FooterStyle HorizontalAlign="Right" Font-Bold="True"></FooterStyle>
</asp:TemplateField>
<asp:TemplateField SortExpression="Weight" HeaderText="Total Gross Weight (kg)"><FooterTemplate>
<asp:Literal id="LiteralWeightTotal" runat="server" ></asp:Literal>
</FooterTemplate>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
<ItemTemplate>
<asp:Literal id="Literal1" runat="server" Text='<%# Bind("Weight","{0:F2}") %>' ></asp:Literal>
</ItemTemplate>

<FooterStyle HorizontalAlign="Right" Font-Bold="True"></FooterStyle>
</asp:TemplateField>
<asp:TemplateField SortExpression="Volume" HeaderText="Volume (m&#179;)"><FooterTemplate>
<asp:Literal id="LiteralVolumeTotal" runat="server" ></asp:Literal>
</FooterTemplate>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
<ItemTemplate>
<asp:Literal id="LiteralVolume" runat="server" Text='<%# Bind("Volume","{0:F2}") %>' ></asp:Literal>
</ItemTemplate>

<FooterStyle HorizontalAlign="Right" Font-Bold="True"></FooterStyle>
</asp:TemplateField>
</Columns>
</Discovery:DiscoveryGrid> 
</contenttemplate>
       <triggers>
                    <asp:AsyncPostbackTrigger ControlID="ImageButtonSearch" EventName="Click" />
                </triggers>
     
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="ShipmentObjectDataSource" runat="server" SelectMethod="GetShipmentByRoute"
        TypeName="Discovery.BusinessObjects.Controllers.TDCShipmentController" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownListDeliveryLocation" DefaultValue="" Name="deliveryWarehouseId"
                PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="TextBoxRequiredDate" DefaultValue="" Name="requireDate"
                PropertyName="Text" Type="DateTime" />
            <asp:ControlParameter ControlID="CheckBoxIncludeSpecials" DefaultValue="false" Name="includeSpecials"
                PropertyName="Checked" Type="Boolean" />
            <asp:Parameter DefaultValue="RouteCode,DeliveryLocation,RequiredDeliveryDate" Name="sortExpression"
                Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>

</asp:Content>

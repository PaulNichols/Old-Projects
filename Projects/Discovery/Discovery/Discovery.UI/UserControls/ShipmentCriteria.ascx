<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShipmentCriteria.ascx.cs"
    Inherits="Discovery.UI.Web.UserControls.ShipmentCriteriaControl" %>
<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="cc1" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<asp:Panel ID="panelCriteria" runat="server" CssClass="collapsePanelContainer">
    <asp:Panel ID="CriteriaHeader" runat="server" CssClass="collapsePanelHeader" Width="100%">
        <span class="collapsePanelTitle">
            <%= ShipmentType.ToString()%>
            Shipment search criteria</span><span class="collapsePanelMinMax">
                <asp:Image ID="CriteriaMinMax" ImageUrl="~/Images/Container/Min.gif" runat="server" /></span>
    </asp:Panel>
    <asp:Panel ID="CriteriaContent" runat="server" CssClass="collapsePanelContent" Width="100%"
        DefaultButton="btnSearch">
        <table width="100%" cellpadding="3" cellspacing="0" border="0">
            <tr>
                <td>
                    OpCo:</td>
                <td>
                    <cc1:OpCoDropDownList ID="ddlOpCo" UsersOpCo='<%# Profile.OpCoCode %>' runat="server"
                        AppendDataBoundItems="True" DataSourceID="dataSourceOpCo" DataTextField="Code"
                        DataValueField="Code" CssClass="EditText" AutoPostBack="True" OnSelectedIndexChanged="ddlOpCo_SelectedIndexChanged">
                        <asp:ListItem Value="-1">All</asp:ListItem>
                    </cc1:OpCoDropDownList>
                </td>
                <td>
                    Required From:</td>
                <td>
                    <asp:TextBox ID="txtRequiredFrom" runat="server" Width="75px" CssClass="EditText"></asp:TextBox><rjs:PopCalendar
                        ID="PopCalendarRequiredFrom" runat="server" RequiredDate="false" RequiredDateMessage="Please enter a Required From Date"
                        Control="txtRequiredFrom"></rjs:PopCalendar>
                </td>
                <td>
                    Required To:</td>
                <td>
                    <asp:TextBox ID="txtRequiredTo" runat="server" Width="75px" CssClass="EditText"></asp:TextBox><rjs:PopCalendar
                        ID="PopCalendarRequiredTo" runat="server" RequiredDate="false" RequiredDateMessage="Please enter a Required To Date"
                        Control="txtRequiredTo"></rjs:PopCalendar>
                </td>
                <td>
                    Status:</td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server" AppendDataBoundItems="True" CssClass="EditText">
                        <asp:ListItem Value="-1">All</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td>
                    Cust.&nbsp; Name:</td>
                <td>
                    <asp:TextBox ID="txtCustomerName" runat="server" CssClass="EditText"></asp:TextBox></td>
                <td>
                    Cust. Number:</td>
                <td>
                    <asp:TextBox ID="txtCustomerNumber" runat="server" CssClass="EditText"></asp:TextBox></td>
                <td>
                    Shipment #:</td>
                <td>
                    <asp:TextBox ID="txtOrderNumber" runat="server" CssClass="EditText" MaxLength="10"
                        Width="80px"></asp:TextBox></td>
                <td>
                    Sales Branch:</td>
                <td>
                    <asp:DropDownList ID="ddlSalesBranchCode" runat="server" AppendDataBoundItems="True"
                        DataTextField="Description" DataValueField="Location" CssClass="EditText">
                        <asp:ListItem Value="-1">All</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td>
                    Stock W/h:</td>
                <td>
                    <asp:DropDownList ID="ddlStockWarehouse" runat="server" AppendDataBoundItems="True"
                        DataTextField="Description" DataValueField="Code" CssClass="EditText">
                        <asp:ListItem Value="-1">All</asp:ListItem>
                    </asp:DropDownList></td>
                <td>
                    Delivery W/h:</td>
                <td>
                    <asp:DropDownList ID="ddlDeliveryWarehouse" runat="server" AppendDataBoundItems="True"
                        DataTextField="Description" DataValueField="Code" CssClass="EditText"
                        OnSelectedIndexChanged="ddlDeliveryWarehouse_SelectedIndexChanged">
                        <asp:ListItem Value="-1">All</asp:ListItem>
                    </asp:DropDownList></td>
                <td>
                    Trans. Type:</td>
                <td>
                    <asp:DropDownList ID="ddlTransactionType" runat="server" AppendDataBoundItems="True"
                        DataTextField="Description" DataValueField="Code" CssClass="EditText">
                        <asp:ListItem Value="-1">All</asp:ListItem>
                    </asp:DropDownList></td>
                <td>
                    Route Code:</td>
                <td>
                    <asp:DropDownList ID="ddlRouteCode" runat="server" AppendDataBoundItems="True" DataTextField="Description"
                        DataValueField="Code" CssClass="EditText">
                        <asp:ListItem Value="-1">All</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr id="rowTDC1" runat="server">
                <td>
                    Trip:</td>
                <td>
                    <asp:DropDownList ID="ddlTrip" runat="server" CssClass="EditText" AutoPostBack="True"
                        DataTextField="TripNumber" DataValueField="TripNumber" OnSelectedIndexChanged="ddlTrip_SelectedIndexChanged"
                        AppendDataBoundItems="True">
                        <asp:ListItem Value="-1">All</asp:ListItem>
                    </asp:DropDownList></td>
                <td>
                    Drop:</td>
                <td>
                    <asp:DropDownList ID="ddlDrop" runat="server" CssClass="EditText" DataTextField="DropSequence"
                        DataValueField="DropSequence" AppendDataBoundItems="True">
                        <asp:ListItem Value="-1">All</asp:ListItem>
                    </asp:DropDownList></td>
                <td>
                    Trans. Sub Type:</td>
                <td>
                    <asp:DropDownList ID="ddlTransactionSubType" runat="server" AppendDataBoundItems="True"
                        DataTextField="Description" DataValueField="Code" CssClass="EditText">
                        <asp:ListItem Value="-1">All</asp:ListItem>
                    </asp:DropDownList></td>
                <td>
                    Shipment Type:</td>
                <td align="left">
                    <asp:DropDownList ID="ddlType" runat="server" CssClass="EditText" DataTextField="Code"
                        DataValueField="Value" AppendDataBoundItems="True">
                        <asp:ListItem Value="-1">All</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
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
                <td>
                </td>
                <td align="right">
                    <asp:ImageButton ID="btnSearch" SkinID="ButtonSearch" runat="server" OnClick="btnSearch_Click" /></td>
            </tr>
        </table>
    </asp:Panel>
</asp:Panel>
<%-- AJAX Extenders --%>
<ajaxToolkit:CollapsiblePanelExtender ID="collapsiblePanelExtenderCriteria" runat="Server"
    TargetControlID="CriteriaContent" ExpandControlID="CriteriaMinMax" CollapseControlID="CriteriaMinMax"
    Collapsed="False" ImageControlID="CriteriaMinMax" CollapsedSize="0" SuppressPostBack="true"
    ExpandedImage="~/Images/Container/Min.gif" CollapsedImage="~/Images/Container/Max.gif" />
<%-- Object Data Source --%>
<asp:ObjectDataSource ID="dataSourceOpCo" runat="server" SelectMethod="GetOpCos"
    TypeName="Discovery.BusinessObjects.Controllers.OpcoController">
    <SelectParameters>
        <asp:Parameter DefaultValue="true" Name="fullyPopulate" Type="Boolean" />
    </SelectParameters>
</asp:ObjectDataSource>

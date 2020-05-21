<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="TripDetails.aspx.cs" Inherits="Discovery.UI.Web.Shipments.TripDetails" Title="Mangement Server - Trip Details" %>

<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <div class="PageTitle">
        Trip Details</div>
    <hr />
    <br />
    <asp:HyperLink ID="HyperLinkBack" SkinID="HyperLinkBack"  runat="server">Back</asp:HyperLink><br />
    <asp:FormView ID="FormView1" runat="server" DataSourceID="TripObjectDataSource" Width="781px" >
        <ItemTemplate>
            <table>
                <tr>
                    <td style="width: 63px">
                        <strong>
                        Trip No.:</strong></td>
                    <td style="width: 95px">
                        <asp:Label ID="LabelTripNumber" runat="server" Text='<%# Eval("TripNumber") %>'></asp:Label></td>
                    <td style="width: 78px">
                        <strong>
                        Trip Date:</strong></td>
                    <td style="width: 106px">
                        <asp:Label ID="LabelTripDate" runat="server" Text='<%# Eval("StartDate","{0:d}") %>'></asp:Label></td>
                    <td style="width: 100px">
                        <strong>
                        TDC Location:</strong></td>
                    <td style="width: 127px">
                        <asp:Label ID="LabelWarehouse" runat="server" Text='<%# Eval("Warehouse.Description") %>'></asp:Label></td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:FormView>
    <asp:ObjectDataSource ID="TripObjectDataSource" runat="server" SelectMethod="GetTrip"
        TypeName="Discovery.BusinessObjects.Controllers.TripController">
        <SelectParameters>
            <asp:QueryStringParameter Name="tripId" QueryStringField="Id" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <cc1:discoverygrid id="DiscoveryGrid1" runat="server" defaultsortexpression="" detailurl=""
        dohiliterow="True" DataSourceID="ShipmentDropObjectDataSource">
        <Columns>
            <asp:BoundField DataField="ShipmentNumberAndDespatch" HeaderText="Shipment" SortExpression="ShipmentNumberAndDespatch" />
            <asp:BoundField DataField="DropSequence" HeaderText="Drop" SortExpression="DropSequence" />
            <asp:BoundField DataField="ArriveTime" HtmlEncode="False" DataFormatString="{0:t}" HeaderText="Arrive" SortExpression="ArriveTime" />
            <asp:BoundField DataField="DepartTime" HtmlEncode="False" DataFormatString="{0:t}" HeaderText="Depart" SortExpression="DepartTime" />
            <asp:BoundField DataField="LoadingTime" HtmlEncode="False" DataFormatString="{0:F2}" HeaderText="Loading" SortExpression="LoadingTime" />
            <asp:BoundField DataField="TravellingTime" HtmlEncode="False" DataFormatString="{0:F2}" HeaderText="Travelling" SortExpression="TravellingTime" />
            <asp:BoundField DataField="Distance" HtmlEncode="False" DataFormatString="{0:F2}" HeaderText="Distance" SortExpression="Distance" />       
            <asp:BoundField DataField="Weight" HtmlEncode="False" DataFormatString="{0:F2}" HeaderText="Weight" SortExpression="Weight" />       
            <asp:BoundField DataField="Volume" HtmlEncode="False" DataFormatString="{0:F2}" HeaderText="Volume" SortExpression="Volume" />       
        </Columns>
    </cc1:discoverygrid>
    <asp:ObjectDataSource ID="ShipmentDropObjectDataSource" runat="server" SelectMethod="GetShipmentDropsForTrip" TypeName="Discovery.BusinessObjects.Controllers.TripController">
        <SelectParameters>
            <asp:QueryStringParameter Name="tripId" QueryStringField="Id" Type="Int32" />
            <asp:Parameter Name="sortExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>



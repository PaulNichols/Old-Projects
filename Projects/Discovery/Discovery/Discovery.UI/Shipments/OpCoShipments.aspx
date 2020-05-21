<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="OpCoShipments.aspx.cs"
    Inherits="Discovery.UI.Web.Shipments.OpCoShipments" Title="Management Server - OpCo Shipments" %>

<%@ Register Src="~/UserControls/OpCoShipments.ascx" TagName="OpCoShipmentsUserControl"
    TagPrefix="Discovery" %>
<%@ Register Src="~/UserControls/ShipmentCriteria.ascx" TagName="ShipmentCriteria"
    TagPrefix="Discovery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <div class="PageTitle">
        OpCo Shipments</div>
    <hr />
    <!-- AJAX Update Panel -->
    <asp:UpdatePanel id="ShipmentPanel" runat="server">
        <contenttemplate>
            <!-- Search Criteria -->
            <Discovery:ShipmentCriteria ID="ShipmentCriteriaUserControl" runat="server" Width="990px" ShipmentType="OpCo" />
            <!-- Op Co Shipments -->
            <Discovery:OpCoShipmentsUserControl ID="OpCoShipmentsUserControl" runat="server" DataSourceID="dataSourceShipments" Width="992px" />
        </contenttemplate>
    </asp:UpdatePanel>
    <!-- AJAX Update Panel -->
    <!-- Data Source -->
    <asp:ObjectDataSource ID="dataSourceShipments" runat="server" SelectMethod="GetShipments"
        TypeName="Discovery.BusinessObjects.Controllers.OpCoShipmentController" OnSelecting="dataSourceShipments_Selecting"
        EnablePaging="True" MaximumRowsParameterName="numRows" StartRowIndexParameterName="pageIndex"
        SortParameterName="sortExpression" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter Name="criteria" Type="Object" DefaultValue="" />
            <asp:Parameter Name="sortExpression" Type="String" DefaultValue="OpCoCode" />
            <asp:Parameter DefaultValue="1" Name="pageIndex" Type="Int32" />
            <asp:Parameter DefaultValue="10" Name="numRows" Type="Int32" />
            <asp:Parameter DefaultValue="true" Name="fullyPopulate" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

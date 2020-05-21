<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="TDCShipments.aspx.cs"
    Inherits="Discovery.UI.Web.Shipments.TDCShipments" Title="Management Server - TDC Shipments" %>

<%@ Register Src="~/UserControls/TDCShipments.ascx" TagName="TDCShipmentsUserControl"
    TagPrefix="Discovery" %>
<%@ Register Src="~/UserControls/ShipmentCriteria.ascx" TagName="ShipmentCriteriaControl"
    TagPrefix="Discovery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <div class="PageTitle">
        TDC Shipments</div>
    <hr />
    <%-- Shipment Criteria --%>
    <Discovery:ShipmentCriteriaControl ID="ShipmentCriteriaUserControl" runat="server"
        Width="990px" ShipmentType="TDC" />
    <br />
    <%-- Update Panel Start --%>
    <asp:UpdatePanel ID="TDCShipmentsUpdatePanel" runat="server">
        <ContentTemplate>
            <%-- Shipments --%>
            <Discovery:TDCShipmentsUserControl ID="TDCShipmentsUserControl" runat="server" DataSourceID="dataSourceShipments"
                Width="992px" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <%-- Update Panel End --%>
    
    <%-- Data Source --%>
    <asp:ObjectDataSource ID="dataSourceShipments" runat="server" SelectMethod="GetShipments"
        TypeName="Discovery.BusinessObjects.Controllers.TDCShipmentController" OnSelecting="dataSourceShipments_Selecting"
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

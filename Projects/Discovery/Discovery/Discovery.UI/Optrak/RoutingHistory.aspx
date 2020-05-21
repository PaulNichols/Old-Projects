<%@ Page Language="C#" MasterPageFile="~/Main.master" EnableEventValidation="false"  AutoEventWireup="true" CodeFile="RoutingHistory.aspx.cs" Inherits=" Discovery.UI.Web.Routing.RoutingHistory" Title="Routing History" %>
<%@ Import namespace="Discovery.Utility"%>

<%@ Register Src="~/UserControls/TDCShipments.ascx" TagName="TDCShipments" TagPrefix="Discovery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
  <div class="PageTitle">
        Routing History</div>
        <hr />
                <asp:MultiView ID="MultiViewHistory" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
    Status:
    <asp:DropDownList ID="DropDownListStatus" runat="server" AutoPostBack="True" Width="250px">
    
        <asp:ListItem Value="0">In Process</asp:ListItem>
        <asp:ListItem Selected="True" Value="1">Sent</asp:ListItem>
        <asp:ListItem Value="2">Recieved</asp:ListItem>
        <asp:ListItem Value="3">Reset</asp:ListItem>
        <asp:ListItem Value="4">All</asp:ListItem>
    </asp:DropDownList><br />
        <br />
         
    <asp:GridView ID="GridViewHistory" runat="server" DataSourceID="LockObjectDataSource"
        OnRowCommand="GridViewHistory_RowCommand" OnRowDataBound="GridViewHistory_RowDataBound">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonReset" runat="server" CommandArgument='<%# Eval("Id") %>'
                        CommandName="Reset" OnClientClick="return confirm('Are you sure?');">Reset</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>      
                 
            <asp:BoundField DataField="RegionCode" HeaderText="Region" SortExpression="RegionCode" />
              <asp:TemplateField HeaderText="Status" >
                            <ItemTemplate>
                                <asp:Label ID="LabelStatus" Text='<%# Eval("Status")  %>' runat="server" />
                            </ItemTemplate>
            </asp:TemplateField>
            
            
            <asp:BoundField DataField="ProcessedBy" HeaderText="Processed By" SortExpression="ProcessedBy" />            
            <asp:BoundField DataField="ProcessStartedDate" HeaderText="Process Started Date" SortExpression="ProcessStartedDate" />
             
            <asp:TemplateField HeaderText="Sent for Routing on" SortExpression="SentDate">
                            <ItemTemplate>
                                <asp:Label ID="LabelSentDate"  Text='<%# Convert.ToDateTime(Eval("SentDate"))==Null.NullDate?"N/A":Eval("SentDate") %>' runat="server" />
                            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Recieved Routings" SortExpression="ReceivedDate">
                <ItemTemplate>
                    <asp:Label ID="LabelReceivedDate"  Text='<%# (Discovery.BusinessObjects.RoutingHistory.StatusEnum)(Convert.ToInt32(Eval("Status")))==Discovery.BusinessObjects.RoutingHistory.StatusEnum.Recieved?Convert.ToDateTime(Eval("DropFileReceivedDate"))>Convert.ToDateTime(Eval("TripPartFileReceivedDate"))?Convert.ToDateTime(Eval("DropFileReceivedDate")).ToString():Convert.ToDateTime(Eval("TripPartFileReceivedDate")).ToString():"N/A" %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Reset on" SortExpression="ResetDate">
                <ItemTemplate>
                    <asp:Label ID="LabelResetDate"  Text='<%# Convert.ToDateTime(Eval("ResetDate"))==Null.NullDate?"N/A":Eval("ResetDate") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Reset by" SortExpression="ResetBy">
                <ItemTemplate>
                    <asp:Label ID="LabelResetBy"  Text='<%# string.IsNullOrEmpty(Eval("ResetBy").ToString())?"N/A":Eval("ResetBy") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
                  <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonDetails" runat="server" CommandArgument='<%# Eval("Id") %>'
                        CommandName="Details" >View Shipments</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>  
            
        </Columns>
    </asp:GridView>
    
    <asp:ObjectDataSource ID="LockObjectDataSource" runat="server" 
        
        SortParameterName="sortExpression"
        SelectMethod="GetRoutingHistory" 
        TypeName="Discovery.BusinessObjects.Controllers.RoutingController" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter Name="sortExpression" Type="String" />
            <asp:ControlParameter ControlID="DropDownListStatus" Name="status" PropertyName="SelectedValue"
                Type="Object" />
        </SelectParameters>
      
    </asp:ObjectDataSource>
        </asp:View>
        <asp:View ID="View2" runat="server">
            &nbsp;<asp:LinkButton ID="LinkButtonBack" runat="server" OnClick="LinkButtonBack_Click">Back To History</asp:LinkButton>
            <br />
            <br />
                    <Discovery:TDCShipments ID="TDCShipmentsUserControl" runat="server" DataSourceID="dataSourceShipments" />
            <asp:ObjectDataSource ID="dataSourceShipments" runat="server" SelectMethod="GetShipmentsByRoutingHistoryId" OnSelecting="dataSourceShipments_Selecting"
        EnablePaging="True" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="pageIndex"
        SortParameterName="sortExpression"
                TypeName="Discovery.BusinessObjects.Controllers.RoutingController" >
                <SelectParameters>
                    <asp:Parameter DefaultValue="" Name="routingHistoryId" Type="Int32" />
                           <asp:Parameter Name="sortExpression" Type="String" DefaultValue="OpCoCode" />
            <asp:Parameter DefaultValue="1" Name="pageIndex" Type="Int32" />
            <asp:Parameter DefaultValue="10" Name="maximumRows" Type="Int32" />

                    <asp:Parameter Name="fullyPopulate" Type="Boolean" />
                </SelectParameters>
            </asp:ObjectDataSource>

        </asp:View>
    </asp:MultiView>
</asp:Content>


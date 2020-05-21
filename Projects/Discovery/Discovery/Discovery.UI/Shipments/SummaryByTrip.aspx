<%@ Page Language="C#" MasterPageFile="~/Main.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="SummaryByTrip.aspx.cs" Inherits="Discovery.UI.Web.Shipments.SummaryByTrip"
    Title="Summary By Trip" %>

<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="Discovery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <div class="PageTitle">
        Summary By Trip</div>
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
                        Required Date From:</td>
                    <td>
                        &nbsp;<asp:TextBox ID="TextBoxDateFrom" runat="server"></asp:TextBox>
                        <rjs:PopCalendar ID="Popcalendar1" Control="TextBoxDateFrom" runat="server" RequiredDate="True"
                            RequiredDateMessage="Please enter a Required Delivery Date" />
                    </td>
                    <td>
                        To:</td>
                    <td align="right">
                        <asp:TextBox ID="TextBoxDateTo" runat="server"></asp:TextBox>
                        <rjs:PopCalendar ID="PopCalendarRequiredDate" runat="server" RequiredDate="True"
                            Control="TextBoxDateTo" RequiredDateMessage="Please enter a Required Delivery Date" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Delivery Location:</td>
                    <td>
                        <asp:ObjectDataSource ID="WarehouseObjectDataSource" runat="server" SelectMethod="GetWarehouses"
                            TypeName="Discovery.BusinessObjects.Controllers.WarehouseController">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="CodeAndDescription" Name="sortExpression" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:DropDownList ID="DropDownListDeliveryLocation" runat="server" Width="261px"
                            DataSourceID="WarehouseObjectDataSource" DataTextField="CodeAndDescription" DataValueField="Id" OnDataBound="DropDownListDeliveryLocation_DataBound1">
                        </asp:DropDownList></td>
                    <td>
                    </td>
                    <td align="right">
                        <asp:ImageButton ID="ImageButtonSearch" runat="server" OnClick="ImageButtonSearch_Click"
                            SkinID="ButtonSearch" /></td>
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
        <Discovery:DiscoveryGrid  DoHiLiteRow="False"  DetailURL=""  ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" DataSourceID="ShipmentObjectDataSource" DefaultSortExpression="" >
            <Columns>
              
            
          
          
             <asp:TemplateField  SortExpression="TripNumber"  HeaderText="Trip Number">
                        <headerstyle verticalalign="Bottom" />
               

                 <itemtemplate >
<asp:HyperLink id="lnkTrip" runat="server" Text='<%# Bind("TripNumber") %>'></asp:HyperLink> 
</itemtemplate>
                </asp:TemplateField>
                
                
            
 
<asp:BoundField DataField="StartDate" HtmlEncode="False" DataFormatString="{0:d}" HeaderText="Delivery Date" SortExpression="LeaveTime" >
    <headerstyle verticalalign="Bottom" />
</asp:BoundField>
                
                    <asp:TemplateField  SortExpression="VehicleRegistration"  HeaderText="Vehicle Registration">
                        <headerstyle verticalalign="Bottom" />
               

                 <itemtemplate >
<asp:HyperLink id="lnkVehicleRegistration" runat="server" Text='<%# Bind("VehicleRegistration") %>'></asp:HyperLink> 
</itemtemplate>
                </asp:TemplateField>
                
            
            <asp:BoundField DataField="LeaveTime" HtmlEncode="False" DataFormatString="{0:t}" HeaderText="Depart" SortExpression="LeaveTime" >
                <headerstyle verticalalign="Bottom" />
            </asp:BoundField>
            <asp:BoundField DataField="FinishTime" HtmlEncode="False" DataFormatString="{0:t}"  HeaderText="Return" SortExpression="FinishTime" >
                <headerstyle verticalalign="Bottom" />
            </asp:BoundField>
            <asp:BoundField DataField="ItemCount" HeaderText="Drops" SortExpression="ItemCount" >
                <headerstyle verticalalign="Bottom" />
            </asp:BoundField>
                <asp:TemplateField >
                    <headertemplate>
<TABLE><TBODY><TR><TD colspan="2" style="WIDTH: 200px; TEXT-ALIGN: center">Weight (kg)</TD></TR>
<TR><TD style="WIDTH: 100px; TEXT-ALIGN: left"><asp:LinkButton id="lnkWeightDeliveries" CommandName="Sort" BackColor="#6B696B" Font-Bold="True" ForeColor="White" CommandArgument="DeliveryWeight" runat="server" Text="Deliveries">
</asp:LinkButton></TD><TD style="WIDTH: 100px; TEXT-ALIGN: left"><asp:LinkButton id="lnkWeightCollections" CommandName="Sort" BackColor="#6B696B" Font-Bold="True" ForeColor="White" CommandArgument="CollectionWeight" runat="server" Text="Collections">
</asp:LinkButton></TD></TR>
</TBODY></TABLE>
</headertemplate>
               
                    <itemstyle horizontalalign="Right" />
                    <itemtemplate>
<TABLE><TBODY><TR><TD style="WIDTH: 100px" align=left><asp:Literal id="Literal1" runat="server" Text='<%# Bind("DeliveryWeight","{0:F}") %>'></asp:Literal></TD><TD style="WIDTH: 100px" align=left><asp:Literal id="Literal3" runat="server" Text='<%# Bind("CollectionWeight","{0:F}") %>'></asp:Literal></TD></TR></TBODY></TABLE>
</itemtemplate>
          
                </asp:TemplateField>
                <asp:TemplateField>
                    <headertemplate>
<TABLE><TBODY><TR><TD colspan="2" style="WIDTH: 200px; TEXT-ALIGN: center">Volume (m&#x00B3;)</TD></TR>
<TR><TD style="WIDTH: 100px; TEXT-ALIGN: left"><asp:LinkButton id="lnkVolumeDeliveries" CommandName="Sort" BackColor="#6B696B" Font-Bold="True" ForeColor="White" CommandArgument="DeliveryVolume" runat="server" Text="Deliveries">
</asp:LinkButton></TD><TD style="WIDTH: 100px; TEXT-ALIGN: left"><asp:LinkButton id="lnkVolumeCollections" CommandName="Sort" BackColor="#6B696B" Font-Bold="True" ForeColor="White" CommandArgument="CollectionVolume" runat="server" Text="Collections">
</asp:LinkButton></TD></TR>
</TBODY></TABLE>
</headertemplate>
                    <itemstyle horizontalalign="Right" />
                    <itemtemplate>
<TABLE><TBODY><TR><TD style="WIDTH: 100px" align=left><asp:Literal id="Literal2" runat="server" Text='<%# Bind("DeliveryVolume","{0:F}") %>'></asp:Literal></TD><TD style="WIDTH: 100px" align=left><asp:Literal id="Literal4" runat="server" Text='<%# Bind("CollectionVolume","{0:F}") %>'></asp:Literal></TD></TR></TBODY></TABLE>
</itemtemplate>
                </asp:TemplateField>
                
                <asp:BoundField DataField="TotalDistance"  HtmlEncode="False" DataFormatString="{0:F2}"  HeaderText="Total Distance" SortExpression="TotalDistance" >
                    <headerstyle verticalalign="Bottom" />
                </asp:BoundField>
            
                       </Columns>
          
        </Discovery:DiscoveryGrid>
        
               </contenttemplate>
        <triggers>
                    <asp:AsyncPostbackTrigger ControlID="ImageButtonSearch" EventName="Click" />
                </triggers>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="ShipmentObjectDataSource" runat="server" SelectMethod="GetTripSummaries"
        TypeName="Discovery.BusinessObjects.Controllers.TripController" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownListDeliveryLocation" DefaultValue="-1"
                Name="warehouseId" PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="TextBoxDateFrom" DefaultValue="" Name="requiredDeliveryDateFrom"
                PropertyName="Text" Type="DateTime" />
            <asp:ControlParameter ControlID="TextBoxDateTo" DefaultValue="" Name="requiredDeliveryDateTo"
                PropertyName="Text" Type="DateTime" />
            <asp:Parameter DefaultValue="RouteCode,DeliveryLocation,RequiredDeliveryDate" Name="sortExpression"
                Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

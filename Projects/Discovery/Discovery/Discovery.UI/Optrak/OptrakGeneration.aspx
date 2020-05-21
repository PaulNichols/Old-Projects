<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="OptrakGeneration.aspx.cs" Inherits=" Discovery.UI.Web.Routing.OptrakGeneration" Title="Management Server - Optrak File Generation" %>
          
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">

  <div class="PageTitle">
        Optrak File Generation</div>
        <hr />
       
 
      <asp:UpdatePanel runat="server" ID="UpdatePanel3">
           
                <ContentTemplate>
<asp:Wizard id="Wizard1" runat="server" ActiveStepIndex="0" StepStyle-VerticalAlign="Top" Width="100%" Height="490px" OnFinishButtonClick="Wizard1_FinishButtonClick" OnActiveStepChanged="Wizard1_ActiveStepChanged" OnNextButtonClick="Wizard1_NextButtonClick" OnPreviousButtonClick="Wizard1_PreviousButtonClick" OnSideBarButtonClick="Wizard1_SideBarButtonClick">
<StepStyle VerticalAlign="Top"></StepStyle>
<WizardSteps>
<asp:WizardStep runat="server" Title="Shipment Selection" StepType="Start"><TABLE><TBODY><TR><TD style="WIDTH: 246px" vAlign=top align=right>Please select a TDC&nbsp;Region to Route:</TD><TD><asp:ListBox runat="server" DataSourceID="RegionDataSource" Width="260px" DataValueField="Code" Height="185px" ID="ListBoxRegions" DataTextField="CodeAndDescription" OnDataBound="SetUserDefaultRegion_DataBound"></asp:ListBox>
 <asp:ObjectDataSource runat="server" SelectMethod="GetRegions" ID="RegionDataSource" TypeName="Discovery.BusinessObjects.Controllers.OptrakRegionController"><SelectParameters>
<asp:Parameter Type="String" DefaultValue="Code,Description" Name="sortExpression"></asp:Parameter>
</SelectParameters>
</asp:ObjectDataSource>
 &nbsp;&nbsp;<BR /><BR /><BR /></TD></TR><TR><TD style="WIDTH: 246px" align=right>Please select a period to Route Shipments for: </TD><TD><asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="RadioButtonListPeriod" ><asp:ListItem Selected="True" Value="0">Same Day</asp:ListItem>
<asp:ListItem Value="1">Next Day</asp:ListItem>
</asp:RadioButtonList>
 </TD></TR></TBODY></TABLE></asp:WizardStep>
<asp:WizardStep runat="server" Title="Selection Refinement">
                <asp:GridView runat="server" DataSourceID="ShipmentObjectDataSource" ID="GridViewShipmentsToRefine" DataKeyNames="Id" OnRowDataBound="GridViewShipmentsToRefine_RowDataBound"><Columns>
<asp:TemplateField><ItemTemplate>
                                <asp:CheckBox ID="CheckBoxRemove" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBoxRemove_CheckedChanged"  />
                            
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:d}" DataField="EstimatedDeliveryDate" SortExpression="EstimatedDeliveryDate" HeaderText="Estimated Delivery Date"></asp:BoundField>
<asp:BoundField DataField="ShipmentNumber" SortExpression="ShipmentNumber" HeaderText="Shipment ID"></asp:BoundField>
<asp:BoundField DataField="DespatchNumber" SortExpression="DespatchNumber" HeaderText="Despatch Number"></asp:BoundField>
<asp:BoundField DataField="ShipmentName" SortExpression="ShipmentName" HeaderText="Shipment Name"></asp:BoundField>
<asp:TemplateField SortExpression="PostCode" HeaderText="Shipment Post Code"><ItemTemplate>
                                <asp:Label ID="LabelPostCode"  Text='<%# Eval("ShipmentAddress.PostCode") %>' runat="server" />
                            
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="DeliveryWarehouseCode" SortExpression="DeliveryWarehouseCode" HeaderText="Warehouse"></asp:BoundField>
</Columns>
</asp:GridView>




                <asp:ObjectDataSource runat="server" SelectCountMethod="GetShipmentsByRoutingHistoryIdCount" 
                SortParameterName="sortExpression" StartRowIndexParameterName="pageIndex" MaximumRowsParameterName="maximumRows"
                SelectMethod="GetShipmentsByRoutingHistoryId" ID="ShipmentObjectDataSource" 
                EnablePaging="True" TypeName="Discovery.BusinessObjects.Controllers.RoutingController" 
                OnSelecting="ShipmentObjectDataSource_Selecting"><SelectParameters>
<asp:Parameter Type="Int32" DefaultValue="" Name="routingHistoryId"></asp:Parameter>
<asp:Parameter Type="String" DefaultValue="OpCoCode" Name="sortExpression"></asp:Parameter>
<asp:Parameter Type="Int32" DefaultValue="1" Name="pageIndex"></asp:Parameter>
<asp:Parameter Type="Int32" DefaultValue="10" Name="maximumRows"></asp:Parameter>
<asp:Parameter Type="Boolean" Name="fullyPopulate"></asp:Parameter>
</SelectParameters>
</asp:ObjectDataSource>




                <p></p>
                        <asp:Button runat="server" ID="ButtonRemove" BorderColor="#CCCCCC" BorderWidth="1px" BackColor="#FFFBFF" Text="Remove Selected Items" ForeColor="Black" BorderStyle="Solid" OnClick="Remove_Click"></asp:Button>




              
            </asp:WizardStep>
<asp:WizardStep runat="server" Title="Delivery Point Merge" StepType="Finish"><asp:MultiView runat="server" ID="MultiView1" ActiveViewIndex="0" ><asp:View runat="server" ID="View1" ><asp:GridView runat="server" DataSourceID="MergedShipmentObjectDataSource" ID="GridViewMergedPoints" DataKeyNames="SiteCode"  OnRowCommand="GridViewMergedPoints_RowCommand" OnRowDataBound="GridViewMergedPoints_RowDataBound"><Columns>
<asp:TemplateField><ItemTemplate>
                                <asp:CheckBox ID="CheckBoxMerge" AutoPostBack="True" runat="server"  OnCheckedChanged="CheckBoxMerge_CheckedChanged"  />
                            
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField SortExpression="SiteCode" HeaderText="Site Code"><ItemTemplate>
                                <asp:LinkButton ID="LinkButtonSiteCode" ToolTip="Click to view Shipments" CommandName="Detail" CommandArgument='<%# Eval("SiteCode")%>'  runat="server" Text='<%# Eval("SiteCode") + String.Format(" ({0} {2}, {1} {3})",Eval("CustomerCount"),Eval("ShipmentCount"),(int)Eval("CustomerCount")==1?"Customer":"Customers",(int)Eval("ShipmentCount")==1?"Shipment":"Shipments")%>'>
                                </asp:LinkButton>
                            
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="ShipmentName" SortExpression="ShipmentName" HeaderText="Shipment Name"></asp:BoundField>
<asp:BoundField DataField="AddressLine1" HeaderText="Address Line 1"></asp:BoundField>
<asp:BoundField DataField="PostCode" SortExpression="PAFPostCode" HeaderText="Shipment Post Code"></asp:BoundField>
<asp:BoundField DataField="DPSCode" HeaderText="DPS Code"></asp:BoundField>
<asp:BoundField DataField="DeliveryWarehouseCode" SortExpression="DeliveryWarehouseCode" HeaderText="Warehouse"></asp:BoundField>
</Columns>
</asp:GridView>
 <asp:ObjectDataSource runat="server" SelectCountMethod="GetMergedShipmentsCount" SortParameterName="sortExpression" SelectMethod="GetMergedShipments" ID="MergedShipmentObjectDataSource" EnablePaging="True" TypeName="Discovery.BusinessObjects.Controllers.RoutingController" OnSelecting="MergedShipmentObjectDataSource_Selecting"><SelectParameters>
<asp:Parameter Type="String" DefaultValue="" Name="sortExpression"></asp:Parameter>
<asp:Parameter Type="Int32" Name="startRowIndex"></asp:Parameter>
<asp:Parameter Type="Int32" Name="maximumRows"></asp:Parameter>
</SelectParameters>
</asp:ObjectDataSource>
 </asp:View>
<asp:View runat="server" ID="View2" >

<asp:GridView runat="server" ID="GridViewDeliveryPointDetail" DataKeyNames="Id" OnRowCommand="GridViewDeliveryPointDetail_RowCommand"><Columns>
<asp:TemplateField><ItemTemplate>
                               <asp:LinkButton ID="LinkButtonUnmerge" ToolTip="Click to unmerge this Shipment" CommandName="UnMerge" CommandArgument='<%# Eval("Id")%>'  runat="server" Text="Un-Merge">
                                </asp:LinkButton>
                            
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="LocationCode" HeaderText="Site Code"></asp:BoundField>
<asp:BoundField DataField="ShipmentNumber" HeaderText="Shipment Id"></asp:BoundField>
<asp:BoundField DataField="DespatchNumber" HeaderText="Despatch Number"></asp:BoundField>
<asp:BoundField DataField="ShipmentName" HeaderText="Shipment Name"></asp:BoundField>
<asp:TemplateField HeaderText="Address Line 1"><ItemTemplate>
                                    <asp:Label ID="LabelAddress1"  Text='<%# Eval("PAFAddress.Line1") %>' runat="server" />
                                
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField  HeaderText="Post Code"><ItemTemplate>
                                    <asp:Label ID="LabelPostCode"  Text='<%# Eval("PAFAddress.PostCode") %>' runat="server" />
                                
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="DPS Code"><ItemTemplate>
                                    <asp:Label ID="LabelDPSCode"  Text='<%# Eval("PAFAddress.DPS") %>' runat="server" />
                                
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="DeliveryWarehouseCode" HeaderText="Warehouse"></asp:BoundField>
</Columns>
</asp:GridView>
 </asp:View>
</asp:MultiView>
 <P></P><asp:Button runat="server" ID="ButtonMerge" BorderColor="#CCCCCC" BorderWidth="1px" BackColor="#FFFBFF" Text="Merge Selected Items" CommandName="Merge" ForeColor="Black" BorderStyle="Solid" OnClick="ButtonMerge_Click"></asp:Button>
 </asp:WizardStep>
<asp:WizardStep runat="server" AllowReturn="False" Title="Finish" StepType="Complete">
                <asp:Label runat="server" Text="Finished..." Font-Size="Large" Font-Bold="True" ID="LabelFinished"></asp:Label>




                <br />
                <br />
                Your OPTRAK files will have been generated and will be sent to the
                OPTRAK workstation.
            </asp:WizardStep>
</WizardSteps>
<FinishNavigationTemplate>
          <table>
                <tr style="">
                <td>
                    <asp:Button BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid"    BorderWidth="1px" ForeColor="Black" ID="FinishPreviousButton" runat="server" CausesValidation="False" CommandName="MovePrevious"
                Text="Previous" /></td><td>
                   
                <asp:Button BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid"    BorderWidth="1px" ForeColor="Black" ID="FinishButton" runat="server" CommandName="MoveComplete" OnClientClick="return confirm('Files will now be sent to Optrak are you sure you want to do this?');"
                Text="Finish" /></td>
                </tr></table>
                
            
            
        
</FinishNavigationTemplate>
</asp:Wizard> 
</ContentTemplate>
   </asp:UpdatePanel>
</asp:Content>


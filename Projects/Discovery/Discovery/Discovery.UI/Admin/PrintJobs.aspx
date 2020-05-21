<%@ Page Language="C#" MasterPageFile="~/Main.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="PrintJobs.aspx.cs" Inherits="Discovery.UI.Web.ReferenceData.PrintJobs"
    Title="Print Jobs" %>

<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="Discovery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">

  <asp:Timer ID="TimerControl1"  runat="server" Interval="10000">
    </asp:Timer>
    


     
    

                     <div class="PageTitle">   
        Print Jobs
 </div>
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
            <td style="width: 100px">
                Warehouse:</td>
            <td style="width: 100px">
                <asp:DropDownList ID="DropDownListWarehouses" AutoPostBack="true" DataTextField="Description" DataValueField="Id" runat="server" Width="276px" DataSourceID="WarehouseObjectDataSource" >
                </asp:DropDownList><asp:ObjectDataSource ID="WarehouseObjectDataSource" runat="server"
                    SelectMethod="GetWarehouses" TypeName="Discovery.BusinessObjects.Controllers.WarehouseController">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="Description" Name="sortExpression" Type="String" />
                        <asp:Parameter DefaultValue="false" Name="fullyPopualte" Type="Boolean" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                
            </td>
          
        </tr>
    </table>
          
    </asp:Panel>
</asp:Panel>
<!-- AJAX Extender -->
<ajaxToolkit:CollapsiblePanelExtender  ID="cpe" runat="Server" TargetControlID="CriteriaContent" ExpandControlID="CriteriaMinMax"
        CollapseControlID="CriteriaMinMax" Collapsed="False" ImageControlID="CriteriaMinMax"
        CollapsedSize="0" SuppressPostBack="true" ExpandedImage="~/Images/Container/Min.gif"
        CollapsedImage="~/Images/Container/Max.gif" />

     <br />

      <asp:UpdatePanel runat="server" ID="UpdatePanel3" >
     <triggers>
<asp:AsyncPostBackTrigger ControlID="DropDownListWarehouses" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="TimerControl1" EventName="Tick"></asp:AsyncPostBackTrigger>
</triggers>
        <contenttemplate>
  
<Discovery:DiscoveryGrid id="GridView1" runat="server" DataSourceID="PrinterJobsObjectDataSource" EnableViewState="False" DefaultSortExpression="JobId,Name" DoHiLiteRow="True" DataKeyNames="JobId" DetailURL="PrintJob.aspx" ><Columns>
<asp:BoundField DataField="PrinterName" SortExpression="PrinterName" HeaderText="Printer Name"></asp:BoundField>
<asp:BoundField DataField="Document" SortExpression="Document" HeaderText="Document"></asp:BoundField>
<asp:BoundField DataField="Owner" SortExpression="Owner" HeaderText="Owner"></asp:BoundField>
<asp:BoundField DataField="TimeSubmitted" SortExpression="TimeSubmitted" HeaderText="Time Submitted"></asp:BoundField>
<asp:BoundField DataField="JobStatus" SortExpression="JobStatus" HeaderText="Job Status"></asp:BoundField>
<asp:BoundField DataField="JobId" SortExpression="JobId" HeaderText="Job Id"></asp:BoundField>
<asp:BoundField DataField="HostPrintQueue" SortExpression="HostPrintQueue" HeaderText="Print Queue"></asp:BoundField>
</Columns>
</Discovery:DiscoveryGrid> 
 </contenttemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="PrinterJobsObjectDataSource" runat="server" SelectMethod="GetPrintJobs"
        TypeName="Discovery.BusinessObjects.Controllers.PrinterController" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownListWarehouses" Name="warehouseId" PropertyName="SelectedValue" Type="Int32" />
            <asp:Parameter DefaultValue="Name" Name="sortExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

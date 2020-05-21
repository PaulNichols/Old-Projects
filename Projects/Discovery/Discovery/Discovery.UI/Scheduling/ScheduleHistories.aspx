<%@ Page Language="C#" MasterPageFile="~/Main.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="ScheduleHistories.aspx.cs" Inherits="Discovery.UI.Web.Scheduling.ScheduleHistories"
    Title="Schedule Histories" %>

<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="Discovery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <div class="PageTitle">
        Schedule Histories
    </div>
    <hr />
    
    <asp:Panel ID="Panel1" runat="server" Width="800px" CssClass="collapsePanelContainer">
    </asp:Panel>
    
    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
        <contenttemplate>
<br /><Discovery:DiscoveryGrid id="grdScheduleHistories" runat="server" 
DataSourceID="ScheduleHistoriesDataSource" DetailURL="ScheduleHistory.aspx" AutoGenerateColumns="False" 
DoHiLiteRow="True" DataKeyNames="Id" AllowPaging="True" DefaultSortExpression="StartDate"><Columns>
<asp:BoundField DataField="StartDate" SortExpression="StartDate" HeaderText="Started"></asp:BoundField>
<asp:BoundField DataField="EndDate" SortExpression="EndDate" HeaderText="Ended"></asp:BoundField>
<asp:BoundField DataField="ElapsedTime" HeaderText="Duration (ms)"></asp:BoundField>
<asp:BoundField DataField="Succeeded" HeaderText="Succeeded"></asp:BoundField>
<asp:BoundField DataField="NextStart" SortExpression="NextStart" HeaderText="Next Start"></asp:BoundField>
<asp:BoundField DataField="LogNotes" SortExpression="LogNotes" HeaderText="Notes"></asp:BoundField>
</Columns>
</Discovery:DiscoveryGrid> 
</contenttemplate>
    </asp:UpdatePanel>

    <asp:ObjectDataSource ID="ScheduleHistoriesDataSource" runat="server"  EnablePaging="True" 
    TypeName="Discovery.Scheduling.SchedulingController" 
    SelectCountMethod="NumberOfScheduleHistoryCount" SelectMethod="GetScheduleHistories">
        <SelectParameters>
            <asp:QueryStringParameter ConvertEmptyStringToNull="False" DefaultValue="-1" Name="scheduleId"
                QueryStringField="ScheduleId" Type="Int32" />
           <asp:Parameter DefaultValue="" Name="sortExpression" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource> 
</asp:Content>

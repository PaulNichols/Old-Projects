<%@ Page Language="C#" MasterPageFile="~/Main.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="MessageAudits.aspx.cs" Inherits="Discovery.UI.Web.Admin.MessageAudits"
    Title="Message Audits" %>

<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="Discovery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <div class="PageTitle">
        Message Audit</div>
    <hr />
    <asp:Panel ID="Panel1" runat="server" Width="900px" CssClass="collapsePanelContainer">
        <asp:Panel ID="CriteriaHeader" runat="server" CssClass="collapsePanelHeader" Width="100%">
            <span class="collapsePanelTitle">Search Criteria</span><span class="collapsePanelMinMax">
                <asp:Image ID="CriteriaMinMax" ImageUrl="~/Images/Container/Min.gif" runat="server" /></span>
        </asp:Panel>
        <asp:Panel ID="CriteriaContent" runat="server" CssClass="collapsePanelContent" Height="0"
            Width="100%">
            <table id="tblFilter" runat="server">
                <tr>
                    <td>
                        Source System</td>
                    <td>
                        Destination System</td>
                    <td>
                        Type</td>
                    <td>
                        Message</td>
                    <td>
                        Received Date From
                    </td>
                    <td>
                        Received Date To</td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlSourceSystem" runat="server" AppendDataBoundItems="True"
                            DataSourceID="SourceSystemDataSource" DataTextField="SourceSystem" DataValueField="SourceSystem"
                            Width="123px">
                            <asp:ListItem Text="All" Value="All" Selected="True"></asp:ListItem>
                        </asp:DropDownList></td>
                    <td>
                        <asp:DropDownList ID="ddlDestinationSystem" runat="server" AppendDataBoundItems="True"
                            DataSourceID="DestinationSystemDataSource" DataTextField="DestinationSystem"
                            DataValueField="DestinationSystem" Width="119px">
                            <asp:ListItem Text="All" Value="All" Selected="True"></asp:ListItem>
                        </asp:DropDownList></td>
                    <td>
                        <asp:DropDownList ID="ddlType" runat="server" AppendDataBoundItems="True" DataSourceID="TypeDataSource"
                            DataTextField="Type" DataValueField="Type" Width="119px">
                            <asp:ListItem Selected="True" Text="All" Value="All"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="txtMessage" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtReceivedDateFrom" runat="server" Width="95px"></asp:TextBox>
                        <rjs:PopCalendar Buttons="[<][m][y]  [>]" BorderWidth="1px" Fade="0.5" Format="dd/mm/yyyy"
                            Move="True" CssClass="Classic" ShowWeekend="True" Shadow="True" RequiredDate="False"
                            Control="txtReceivedDateFrom" BorderStyle="Solid" BorderColor="Black" BackColor="Yellow"
                            Separator="/" ID="PopCalendar1" runat="server" Culture="en-GB English (United Kingdom)"
                            ShowMessageBox="True" WeekendMessage="blah"></rjs:PopCalendar>
                    </td>
                    <td>
                        <asp:TextBox ID="txtReceivedDateTo" runat="server" Width="95px"></asp:TextBox>
                        <rjs:PopCalendar Buttons="[<][m][y]  [>]" BorderWidth="1px" Fade="0.5" Format="dd/mm/yyyy"
                            Move="True" CssClass="Classic" ShowWeekend="True" Shadow="True" RequiredDate="False"
                            Control="txtReceivedDateTo" BorderStyle="Solid" BorderColor="Black" BackColor="Yellow"
                            Separator="/" ID="PopCalendar2" runat="server" Culture="en-GB English (United Kingdom)">
                        </rjs:PopCalendar>
                    </td>
                    <td>
                        <asp:ImageButton SkinID="ButtonSearch" ID="btnSearch" runat="server" OnClick="btnSearch_Click" /></td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>
    <!-- AJAX Extender -->
    <ajaxToolkit:CollapsiblePanelExtender  ID="cpe" runat="Server" TargetControlID="CriteriaContent"
        ExpandControlID="CriteriaMinMax" CollapseControlID="CriteriaMinMax" Collapsed="False"
        ImageControlID="CriteriaMinMax" CollapsedSize="0" SuppressPostBack="true" ExpandedImage="~/Images/Container/Min.gif"
        CollapsedImage="~/Images/Container/Max.gif" />
    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
        <contenttemplate>
<BR /><Discovery:DiscoveryGrid id="grdMessageAudits" runat="server" DataSourceID="MessageAuditEntryDataSource" AllowPaging="True" DataKeyNames="Id" DoHiLiteRow="True" AutoGenerateColumns="False" DetailURL="MessageAudit.aspx" DefaultSortExpression><Columns>
<asp:BoundField DataField="SourceSystem" SortExpression="SourceSystem" HeaderText="Source System"></asp:BoundField>
<asp:BoundField DataField="DestinationSystem" SortExpression="DestinationSystem" HeaderText="Destination System"></asp:BoundField>
<asp:BoundField DataField="Type" SortExpression="Type" HeaderText="Type"></asp:BoundField>
<asp:BoundField DataField="ReceivedDate" SortExpression="ReceivedDate" HeaderText="Received Date"></asp:BoundField>
<asp:BoundField DataField="Sequence" SortExpression="Sequence" HeaderText="Sequence"></asp:BoundField>
<asp:BoundField DataField="Label" SortExpression="Label" HeaderText="Label"></asp:BoundField>
<asp:BoundField DataField="Name" SortExpression="Name" HeaderText="Name"></asp:BoundField>
</Columns>
</Discovery:DiscoveryGrid> 
</contenttemplate>
        <triggers>
<asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="MessageAuditEntryDataSource" SelectCountMethod="NumberOfAuditEntries"
        EnablePaging="True" runat="server" TypeName="Discovery.BusinessObjects.Controllers.AuditEntryController"
        SelectMethod="GetAuditEntries" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtReceivedDateFrom" Name="strDateFrom" PropertyName="Text"
                Type="String" />
            <asp:ControlParameter ControlID="txtReceivedDateTo" Name="strDateTo" PropertyName="Text"
                Type="String" />
            <asp:ControlParameter ControlID="ddlSourceSystem" Name="sourceSystem" PropertyName="SelectedValue"
                Type="String" />
            <asp:ControlParameter ControlID="ddlDestinationSystem" Name="destinationSystem" PropertyName="SelectedValue"
                Type="String" />
            <asp:ControlParameter ControlID="ddlType" Name="type" PropertyName="SelectedValue"
                Type="String" />
            <asp:ControlParameter ControlID="txtMessage" Name="message" PropertyName="Text" Type="String" />
            <asp:Parameter DefaultValue="" Name="sortExpression" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <asp:ObjectDataSource ID="SourceSystemDataSource" runat="server" TypeName="Discovery.BusinessObjects.Controllers.AuditEntryController"
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetSourceSystemList">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="DestinationSystemDataSource" runat="server" TypeName="Discovery.BusinessObjects.Controllers.AuditEntryController"
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetDestinationSystemList">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="TypeDataSource" runat="server" TypeName="Discovery.BusinessObjects.Controllers.AuditEntryController"
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetTypeList"></asp:ObjectDataSource>
</asp:Content>

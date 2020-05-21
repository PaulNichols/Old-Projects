<%@ Page Language="C#" MasterPageFile="~/Main.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="ScheduleStatus.aspx.cs" Inherits="Discovery.UI.Web.Scheduling.ScheduleStatus" Title="Untitled Page"  %>
<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls" TagPrefix="Discovery" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <div class="PageTitle">
        Schedule Status
    </div>
    <hr/>

    <table border="0" cellspacing="1" cellpadding="3" style="background-color:Gray">
        <tr style="background-color:silver">
            <td class="Normal">Current Status:</td>
            <td><asp:label CssClass="NormalBold" ID="lblStatus" Runat="server" /></td></tr>
        <tr style="background-color:silver">
            <td class="Normal">Max Threads:</td>
            <td><asp:label CssClass="NormalBold" ID="lblMaxThreads" Runat="server" /></td></tr>
        <tr style="background-color:silver">
            <td class="Normal">Active Threads:&nbsp;</td>
            <td><asp:label CssClass="NormalBold" ID="lblActiveThreads" Runat="server" /></td></tr>
        <tr style="background-color:silver">
            <td class="Normal">Free Threads:</td>
            <td><asp:label CssClass="NormalBold" ID="lblFreeThreads" Runat="server" /></td></tr>
        <tr>
            <td class="Normal">Command:</td>
            <td><asp:linkbutton ID="btnStart" CssClass="CommandButton" Runat="server">Start</asp:linkbutton>
           &nbsp;&nbsp;<asp:linkbutton ID="btnStop" CssClass="CommandButton" Runat="server">Stop</asp:linkbutton></td></tr>
    </table>
    
    <br/>

    <asp:panel id="pnlScheduleProcessing" runat="server">
        <span class="SubHead">Items Processing</span>    
        <hr/>    
        <asp:datagrid id="dgScheduleProcessing" AlternatingItemStyle-backcolor="#CFCFCF" Runat="server" 
        Caption="This table shows the scheduled tasks that are currently running." 
        GridLines="None" BorderWidth="1" EnableViewState="false" DataKeyField="ScheduleID" 
        CellPadding="2" AutoGenerateColumns="false">
                <columns>
                    <asp:boundcolumn DataField="ScheduleID" HeaderText="Schedule ID" HeaderStyle-CssClass="NormalBoldSmall" ItemStyle-CssClass="NormalSmall" />
                    <asp:boundcolumn DataField="TypeFullName" HeaderText="Type" HeaderStyle-CssClass="NormalBoldSmall" ItemStyle-CssClass="NormalSmall" />
                    <asp:boundcolumn DataField="StartDate" HeaderText="Started" HeaderStyle-CssClass="NormalBoldSmall" ItemStyle-CssClass="NormalSmall" />
                    <asp:boundcolumn DataField="ElapsedTime" HeaderText="Duration<br/>(seconds)" HeaderStyle-CssClass="NormalBoldSmall" ItemStyle-CssClass="NormalSmall" />
                    <asp:boundcolumn DataField="ObjectDependencies" HeaderText="Object Dependencies" HeaderStyle-CssClass="NormalBoldSmall" ItemStyle-CssClass="NormalSmall" />
                    <asp:boundcolumn DataField="ScheduleSource" HeaderText="Triggered By" HeaderStyle-CssClass="NormalBoldSmall" ItemStyle-CssClass="NormalSmall" />
                    <asp:boundcolumn DataField="ThreadID" HeaderText="Thread" HeaderStyle-CssClass="NormalBoldSmall" ItemStyle-CssClass="NormalSmall" />
                    <asp:boundcolumn DataField="LogNotes" HeaderText="Notes" HeaderStyle-CssClass="NormalBoldSmall" ItemStyle-CssClass="NormalSmall" />
                    <asp:TemplateColumn HeaderText="Process Group" HeaderStyle-CssClass="NormalBoldSmall" ItemStyle-CssClass="NormalSmall">
                        <ItemTemplate>
                            <%# Eval("ProcessGroup").ToString() %>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </columns>
        </asp:datagrid>
    </asp:panel>
    
    <br/><br/>
    
    <asp:panel id="pnlScheduleQueue" runat="server">
        <span class="SubHead">Items in Queue</span><hr />
        <asp:datagrid ID="dgScheduleQueue" AlternatingItemStyle-backcolor="#CFCFCF" Runat="server" 
        Caption="This table shows the tasks that are queued up in the schedule."  BorderWidth="1" 
        EnableViewState="false" DataKeyField="ScheduleID" CellPadding="2" AutoGenerateColumns="false">
            <columns>
                <asp:boundcolumn DataField="ScheduleID" HeaderText="ID" HeaderStyle-CssClass="NormalBoldSmall" ItemStyle-CssClass="NormalSmall" />
                <asp:boundcolumn DataField="TypeFullName" HeaderText="Type" HeaderStyle-CssClass="NormalBoldSmall" ItemStyle-CssClass="NormalSmall" />
                <asp:boundcolumn DataField="NextStart" HeaderText="Next Start" HeaderStyle-CssClass="NormalBoldSmall" ItemStyle-CssClass="NormalSmall" />
                <asp:TemplateColumn HeaderText="Overdue<br/>(seconds)" HeaderStyle-CssClass="NormalBoldSmall" ItemStyle-CssClass="NormalSmall">
                    <ItemTemplate>
                        <%# GetOverdueText(Double.Parse(Eval("OverdueBy").ToString())) %>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:boundcolumn DataField="RemainingTime"  DataFormatString="{0:F0}" HeaderText="Time<br/>Remaining<br/>(sec)" HeaderStyle-CssClass="NormalBoldSmall" ItemStyle-CssClass="NormalSmall" />
                <asp:boundcolumn DataField="ObjectDependencies" HeaderText="Object Dependencies" HeaderStyle-CssClass="NormalBoldSmall" ItemStyle-CssClass="NormalSmall" />
                <asp:boundcolumn DataField="ScheduleSource" HeaderText="Triggered By" HeaderStyle-CssClass="NormalBoldSmall" ItemStyle-CssClass="NormalSmall" />
                <asp:TemplateColumn HeaderText="Process Group" HeaderStyle-CssClass="NormalBoldSmall" ItemStyle-CssClass="NormalSmall">
                    <ItemTemplate>
                        <%# (Eval("ProcessGroup").ToString() == "-1")?"Unassigned":Eval("ProcessGroup").ToString() %>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </columns>
        </asp:datagrid>
    </asp:panel>
</asp:Content>


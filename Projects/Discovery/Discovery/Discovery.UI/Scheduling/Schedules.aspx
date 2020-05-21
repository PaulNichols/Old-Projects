<%@ Page Language="C#" MasterPageFile="~/Main.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="Schedules.aspx.cs" Inherits="Discovery.UI.Web.Scheduling.Schedules" Title="Untitled Page"  %>
<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls" TagPrefix="Discovery" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <div class="PageTitle">
        Schedules
    </div>
    <hr />
    <asp:ImageButton ID="NewButton" runat="server" SkinID="NewButton" />
    <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
        <ContentTemplate>
<Discovery:DiscoveryGrid id="GridView1" runat="server" DataSourceID="SchedulesDataSource" Width="479px" Height="205px" DetailURL="Schedule.aspx" AutoGenerateColumns="False" DoHiLiteRow="True" DefaultSortExpression="TypeFullName" DataKeyNames="Id" AllowPaging="True"><Columns>
<asp:BoundField DataField="TypeFullName" SortExpression="TypeFullName" HeaderText="TypeFullName"></asp:BoundField>
<asp:CheckBoxField DataField="Enabled" SortExpression="Enabled" HeaderText="Enabled"></asp:CheckBoxField>
<asp:TemplateField HeaderText="Frequence"><ItemTemplate>
                    <%# GetTimeLapse(Int32.Parse(Eval("TimeLapse").ToString()), Eval("TimeLapseMeasurement").ToString()) %>
                
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Retry Time Lapsed"><ItemTemplate>
                    <%# GetTimeLapse(Int32.Parse(Eval("RetryTimeLapse").ToString()), Eval("RetryTimeLapseMeasurement").ToString()) %>
                
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="NextStart" SortExpression="NextStart" HeaderText="NextStart"></asp:BoundField>
<asp:TemplateField HeaderText="History"><ItemTemplate>
<asp:Button id="btnHistory" onclick="btnHistory_Click" runat="server" Text="History" __designer:wfdid="w4" CommandArgument='<%# Eval("Id") %>'></asp:Button>
</ItemTemplate>
</asp:TemplateField>
</Columns>
</Discovery:DiscoveryGrid> 
</ContentTemplate>
    </asp:UpdatePanel>
    
    <asp:ObjectDataSource ID="SchedulesDataSource" runat="server" EnablePaging="True"
            TypeName="Discovery.Scheduling.SchedulingController"
            SelectCountMethod="NumberOfScheduleCount" SelectMethod="GetSchedules" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter Name="sortExpression" Type="String" />
                    <asp:Parameter Name="startRowIndex" Type="Int32" />
                    <asp:Parameter Name="maximumRows" Type="Int32" />
                </SelectParameters>
    </asp:ObjectDataSource>    
</asp:Content>


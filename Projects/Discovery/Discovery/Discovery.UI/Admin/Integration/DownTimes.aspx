<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="DownTimes.aspx.cs"
    Inherits="Discovery.UI.Web.Admin.DownTimes" Title="Integration Connection DownTimes" %>

<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="Discovery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <div class="PageTitle">
        Connection Downtimes</div>
    <hr />
    Connections:
    <asp:DropDownList ID="DropDownListConnections" runat="server" AppendDataBoundItems="True"
        DataSourceID="ConnectionsObjectDataSource" DataTextField="Name" DataValueField="Id"
        Width="197px" AutoPostBack="True">
        <asp:ListItem Value="0">All</asp:ListItem>
    </asp:DropDownList><br />
    <asp:ObjectDataSource ID="ConnectionsObjectDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetConnections" TypeName="Discovery.Integration.IntegrationController"
        OnSelected="ConnectionsObjectDataSource_Selected">
        <SelectParameters>
            <asp:Parameter DefaultValue="Name" Name="sortExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <asp:ImageButton ID="NewButton" runat="server" SkinID="NewButton" CausesValidation="False">
    </asp:ImageButton>
    <br />
    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
        <triggers>
                    <asp:AsyncPostbackTrigger ControlID="DropDownListConnections" EventName="SelectedIndexChanged" />
                </triggers>
        <contenttemplate>
    
        <Discovery:DiscoveryGrid DetailURL="DownTime.aspx" DefaultSortExpression="Connection.Name,DayOfWeek" DataKeyNames="Id"  ID="GridView1" runat="server" DataSourceID="DownTimesDataSource"  DoHiLiteRow="True">
            <Columns>
            <asp:BoundField DataField="DayOfWeek" HeaderText="Day Of Week" SortExpression="DayOfWeek" />
            <asp:BoundField DataField="StartTime" HtmlEncode="False" DataFormatString="{0:t}"  HeaderText="Start Time" SortExpression="Start" />
            <asp:BoundField DataField="EndTime" HtmlEncode="False" DataFormatString="{0:t}"   HeaderText="End Time" SortExpression="End" />
               <asp:TemplateField HeaderText="Connection Name" SortExpression="Connection.Name">
                  <ItemTemplate>
<asp:Label id="LabelConnectionName" runat="server" Text='<%# Eval("Connection.Name") %>' __designer:wfdid="w1"></asp:Label> 
</ItemTemplate>
                </asp:TemplateField>
            
           
            </Columns>
           
           
        </Discovery:DiscoveryGrid>
        
        
   </contenttemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="DownTimesDataSource" runat="server" SelectMethod="GetDownTimes"
        TypeName="Discovery.Integration.IntegrationController" OnSelected="DownTimesDataSource_Selected">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownListConnections" Name="connectionId" PropertyName="SelectedValue"
                Type="String" />
            <asp:Parameter Name="sortExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

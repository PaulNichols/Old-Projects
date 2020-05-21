<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="ErrorHandling.aspx.cs"
    Inherits="ErrorHandling" Title="Management Server - Error Handling" %>
<%@ Import namespace="Discovery.Utility"%>

<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="Discovery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <div class="PageTitle">
        Error Handling
    </div>
    <hr />
    <asp:Panel ID="Panel1" runat="server" Width="800px" CssClass="collapsePanelContainer">
        <asp:Panel ID="CriteriaHeader" runat="server" CssClass="collapsePanelHeader" Width="100%">
            <span class="collapsePanelTitle">Search Criteria</span><span class="collapsePanelMinMax">
                <asp:Image ID="CriteriaMinMax" ImageUrl="~/Images/Container/Min.gif" runat="server" /></span>
        </asp:Panel>
        <asp:Panel ID="CriteriaContent" runat="server" CssClass="collapsePanelContent" Height="0"
            Width="100%">
            <table>
                <tr>
                    <td>
                        Opco:</td>
                    <td>
                        <asp:DropDownList ID="DropDownListOpCo" runat="server" DataSourceID="OpCosObjectDataSource"
                            DataTextField="Description" DataValueField="Code" AppendDataBoundItems="true" Width="169px">
                            <asp:ListItem Value="">-</asp:ListItem>
                        </asp:DropDownList><asp:ObjectDataSource ID="OpCosObjectDataSource" runat="server"
                            SelectMethod="GetOpCos" TypeName="Discovery.BusinessObjects.Controllers.OpcoController">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="false" Name="fullyPopulate" Type="Boolean" />
                                <asp:Parameter DefaultValue="Description" Name="sortExpression" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                    <td>
                        Category:</td>
                    <td>
                        <asp:DropDownList ID="DropDownListPolicy" runat="server" DataSourceID="PolicyObjectDataSource"
                            DataTextField="Name" AppendDataBoundItems="true" DataValueField="Name" Width="169px">
                            <asp:ListItem Value="">-</asp:ListItem>
                        </asp:DropDownList><asp:ObjectDataSource ID="PolicyObjectDataSource" runat="server"
                            SelectMethod="GetPolicies" TypeName="Discovery.BusinessObjects.Controllers.ErrorTypeController">
                        </asp:ObjectDataSource>
                    </td>
                    <td align="right">
                        <asp:ImageButton ID="ImageButtonSearch" runat="server" SkinID="ButtonSearch" OnClick="ImageButtonSearch_Click" /></td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>
    <!-- AJAX Extender -->
    <ajaxToolkit:CollapsiblePanelExtender ID="cpe" runat="Server" TargetControlID="CriteriaContent"
        ExpandControlID="CriteriaMinMax" CollapseControlID="CriteriaMinMax" Collapsed="False"
        ImageControlID="CriteriaMinMax" CollapsedSize="0" SuppressPostBack="true" ExpandedImage="~/Images/Container/Min.gif"
        CollapsedImage="~/Images/Container/Max.gif" />
    <br />
    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
        <contenttemplate>
<Discovery:DiscoveryGrid id="GridView1" runat="server"  DataKeyNames="Id,ExceptionType,OpCoCode,Policy" DefaultSortExpression="ExceptionDescription"  AllowPaging="True" OnRowClicked="GridView1_RowClicked" DataSourceID="ObjectDataSourceErrors" DetailURL="" DoHiLiteRow="True" Visible="False"><Columns>

<asp:BoundField DataField="Policy"  SortExpression="Policy" HeaderText="Category"></asp:BoundField>
<asp:BoundField DataField="ExceptionDescription"  SortExpression="ExceptionDescription" HeaderText="Exception"></asp:BoundField>
<asp:TemplateField SortExpression="Priority" HeaderText="Priority">
<ItemStyle HorizontalAlign="Center"></ItemStyle>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
<ItemTemplate>
                    <asp:Label ID="LabelPriority" runat="server" Text='<%# StringManipulation.SplitStringOnUpperCase(Eval("Priority").ToString()) %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField SortExpression="RequiresAcknowledgement" HeaderText="Requires Acknowledgement">
<ItemStyle HorizontalAlign="Center"></ItemStyle>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
<ItemTemplate>
                     <asp:Image ID="ImageRequiresAcknowledgement" runat="server" SkinID="Check" Visible='<%# Convert.ToBoolean(Eval("RequiresAcknowledgement")) %>' />
                        <asp:Image ID="ImageNotRequiresAcknowledgement" runat="server" SkinID="UnCheck" Visible='<%# !Convert.ToBoolean(Eval("RequiresAcknowledgement")) %>' />
                    
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField SortExpression="HasEmailHandler" HeaderText="OpCo Email Allowed">
<ItemStyle HorizontalAlign="Center"></ItemStyle>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
<ItemTemplate>
                     <asp:Image ID="ImageSendEmail" runat="server" SkinID="Check" Visible='<%# Convert.ToBoolean(Eval("HasEmailHandler")) %>' />
                        <asp:Image ID="ImageNotSendEmail" runat="server" SkinID="UnCheck" Visible='<%# !Convert.ToBoolean(Eval("HasEmailHandler")) %>' />
                    
</ItemTemplate>
</asp:TemplateField>
</Columns>
</Discovery:DiscoveryGrid> 
            <asp:ObjectDataSource ID="ObjectDataSourceErrors" runat="server" SelectMethod="GetErrorTypes"
                TypeName="Discovery.BusinessObjects.Controllers.ErrorTypeController">
                <SelectParameters>
                    <asp:ControlParameter ControlID="DropDownListOpCo" Name="opCoCode" PropertyName="SelectedValue"
                        Type="String" />
                    <asp:ControlParameter ControlID="DropDownListPolicy" Name="policyName" PropertyName="SelectedValue"
                        Type="String" />
                    <asp:Parameter Name="sortExpression" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
</contenttemplate>
        <triggers>
<asp:AsyncPostBackTrigger ControlID="ImageButtonSearch" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
    </asp:UpdatePanel>
  
</asp:Content>

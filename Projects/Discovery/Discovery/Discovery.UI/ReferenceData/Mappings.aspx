<%@ Page Language="C#" MasterPageFile="~/Main.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="Mappings.aspx.cs" Inherits="Discovery.UI.Web.Mapping.Mappings"
    Title="Management Server - Mapping" Theme="DiscoveryDefault" StylesheetTheme="DiscoveryDefault" %>

<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="Discovery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <div class="PageTitle">
        Mappings</div>
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
                    Source System:</td>
                <td >
                    <asp:DropDownList ID="DropDownListSourceSystems" runat="server" DataSourceID="ObjectDataSourceSystems"
                        DataTextField="Name" DataValueField="Id" Width="130px" AppendDataBoundItems="True" OnDataBound="DropDownList_DataBound">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>&nbsp;
                </td>
                <td style="width: 100px" >
                    Source Type:</td>
                <td >
                    <asp:DropDownList ID="DropDownListSourceTypes" AppendDataBoundItems="True" runat="server" DataSourceID="ObjectDataSourceTypes"
                        DataTextField="SourceType" DataValueField="SourceTypeFullName" Width="195px" OnDataBound="DropDownList_DataBound">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td >
                    Source Property:</td>
                <td>
                    <asp:DropDownList ID="DropDownListSourceProperty"  AppendDataBoundItems="True" runat="server" Width="177px" DataSourceID="ObjectDataSourceProperties" DataTextField="SourceProperty" DataValueField="SourceProperty" OnDataBound="DropDownList_DataBound">
                    <asp:ListItem></asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td>
                    Destination System:</td>
                <td>
                    <asp:DropDownList ID="DropDownListDestSystems"  AppendDataBoundItems="True" runat="server" DataSourceID="ObjectDataSourceSystems"
                        DataTextField="Name" DataValueField="Id" Width="130px" OnDataBound="DropDownList_DataBound">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList><asp:ObjectDataSource ID="ObjectDataSourceSystems" runat="server"
                        SelectMethod="GetAllMappingSystems" TypeName="Discovery.BusinessObjects.Controllers.MappingController">
                    </asp:ObjectDataSource>
                </td>
                <td>
                    Destination Type:</td>
                <td>
                    <asp:DropDownList ID="DropDownListDestTypes"  AppendDataBoundItems="True" runat="server" DataSourceID="ObjectDataSourceTypes"
                        DataTextField="DestinationType" DataValueField="DestinationTypeFullName" Width="195px" OnDataBound="DropDownList_DataBound">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList><asp:ObjectDataSource ID="ObjectDataSourceTypes" runat="server"
                        SelectMethod="GetMappingClassAssociations" TypeName="Discovery.BusinessObjects.Controllers.MappingController">
                    </asp:ObjectDataSource>
                </td>
                <td style="width: 100px">
                    Destination Property:</td>
                <td>
                    <asp:DropDownList ID="DropDownListDestProperty"  AppendDataBoundItems="True" runat="server" Width="177px" DataSourceID="ObjectDataSourceProperties" DataTextField="DestinationProperty" DataValueField="DestinationProperty" OnDataBound="DropDownList_DataBound">
                    <asp:ListItem></asp:ListItem>
                    </asp:DropDownList><asp:ObjectDataSource ID="ObjectDataSourceProperties" runat="server"
                        SelectMethod="GetMappingPropertyAssociations" TypeName="Discovery.BusinessObjects.Controllers.MappingController">
                    </asp:ObjectDataSource>
                </td>
            </tr>
            <tr>
                <td>
                    From Value:</td>
                <td>
                    <asp:TextBox ID="TextBoxFromValue" runat="server" Width="109px"></asp:TextBox></td>
                <td>
                    To Value:</td>
                <td>
                    <asp:TextBox ID="TextBoxToValue" runat="server" Width="109px"></asp:TextBox>&nbsp;
                </td>
                <td style="width: 100px">
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
        <asp:ImageButton ID="NewButton" runat="server" SkinID="NewButton" CausesValidation="False" />
    <br />
   
        <asp:UpdatePanel runat="server" ID="UpdatePanelGrid">
            <contenttemplate>
<Discovery:DiscoveryGrid id="GridView1" runat="server" DataSourceID="MappingsDataSource" DataKeyNames="Id" DefaultSortExpression DetailURL="mapping.aspx" AllowPaging="True" DoHiLiteRow="True" OnPageIndexChanged="GridView1_PageIndexChanged"><Columns>
<asp:TemplateField HeaderText="Source System">
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
                        <asp:Label ID="LabelSourceSystemName" runat="server" Text='<%# Eval("SourceSystem.Name") %>'></asp:Label>
                    
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Destination System">
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
                        <asp:Label ID="LabelDestinationSystemName" runat="server" Text='<%# Eval("DestinationSystem.Name") %>'></asp:Label>
                    
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Source Type">
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
                        <asp:Label ID="LabelMappingClassAssociationSourceType" runat="server" Text='<%# Eval("MappingPropertyAssociation.MappingClassAssociation.SourceType") %>'></asp:Label>
                    
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Destination Type">
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
                        <asp:Label ID="LabelMappingClassAssociationDestinationType" runat="server" Text='<%# Eval("MappingPropertyAssociation.MappingClassAssociation.DestinationType") %>'></asp:Label>
                    
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Source Property">
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
                        <asp:Label ID="LabelSourceProperty" runat="server" Text='<%# Eval("MappingPropertyAssociation.SourceProperty") %>'></asp:Label>
                    
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Destination Property">
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
                        <asp:Label ID="LabelMappingPropertyAssociationDestinationProperty" runat="server"   Text='<%# Eval("MappingPropertyAssociation.DestinationProperty") %>'></asp:Label>
                    
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="SourceValue" HeaderText="From Value"></asp:BoundField>
<asp:BoundField DataField="DestinationValue" HeaderText="To Value"></asp:BoundField>
</Columns>
</Discovery:DiscoveryGrid> 
</contenttemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ImageButtonSearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:ObjectDataSource ID="MappingsDataSource" runat="server" SelectMethod="GetMappings" SelectCountMethod="GetMappingsCount" EnablePaging="True"
            TypeName="Discovery.BusinessObjects.Controllers.MappingController"  OnSelecting="MappingsDataSource_Selecting">
            <SelectParameters>
                <asp:Parameter Name="mappingSearchParams" Type="Object" DefaultValue="" />
                             
            </SelectParameters>
         
            </asp:ObjectDataSource>
    
</asp:Content>

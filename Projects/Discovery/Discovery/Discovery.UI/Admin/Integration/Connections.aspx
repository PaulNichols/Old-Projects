<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Connections.aspx.cs" Inherits="Discovery.UI.Web.Admin.Connections" Title="Integration Connections" %>
<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="Discovery" %>
       
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">

 <div class="PageTitle">
    
        Connections </div>
        <hr />

                 
            

        <asp:ImageButton ID="NewButton" runat="server" SkinID="NewButton" CausesValidation="False"></asp:ImageButton>
        <br />
        <Discovery:DiscoveryGrid DetailURL="Connection.aspx" DefaultSortExpression="Name" DataKeyNames="Id"  ID="GridView1" runat="server" DataSourceID="ConnectionsDataSource"  DoHiLiteRow="True">
            <Columns>
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
             
                
                <asp:BoundField DataField="ConnectionType" HeaderText="Connection Type" SortExpression="ConnectionType" />
                <asp:BoundField DataField="ChannelType" HeaderText="Channel Type" SortExpression="ChannelType" />
                
                <asp:TemplateField HeaderText="Active">
                    <itemtemplate>
                     <asp:Image ID="ImageIsActive" runat="server" SkinID="Check" Visible='<%# Convert.ToBoolean(Eval("Active")) %>' />
                        <asp:Image ID="ImageNotActive" runat="server" SkinID="UnCheck" Visible='<%# !Convert.ToBoolean(Eval("Active")) %>' />
                    
</itemtemplate>
                    <headerstyle horizontalalign="Center" />
                    <itemstyle horizontalalign="Center" />
                </asp:TemplateField>
              
            </Columns>
           
        </Discovery:DiscoveryGrid>
        
        <asp:ObjectDataSource ID="ConnectionsDataSource" runat="server" SelectMethod="GetConnections"
            TypeName="Discovery.Integration.IntegrationController"   >
           
        </asp:ObjectDataSource>
        
</asp:Content>


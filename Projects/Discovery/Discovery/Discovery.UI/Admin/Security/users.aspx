<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="users.aspx.cs"
    Inherits="Discovery.UI.Web.Security.Users" Title="Management Server - Users Maintenance" %>

<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="Discovery" %>
<%@ Register Src="AToZ.ascx" TagName="AToZ" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Always">
        <contenttemplate>
 <div class="PageTitle">
        User Maintenance</div>
        <hr />
    
    <asp:ImageButton ID="NewButton" runat="server" SkinID="NewButton" OnClick="NewButton_Click" />
               
    <br />
    <table style="width: 778px; font-size: 8pt; font-family: Arial;">
        <tr>
            <td style="width: 50%; color: white; background-color: #336699;" valign="top">
                Users</td>
            <td valign="top" style="color: white; background-color: #336699">
                <asp:Label ID="LabelUser" runat="server" Text="Label"></asp:Label></td>
        </tr>
        <tr>
            <td valign="top">
    <uc1:AToZ id="AToZ" runat="server">
    </uc1:AToZ>
              
    <asp:GridView   ID="GridViewUsers" runat="server"  DataKeyNames="UserName"  DataSourceID="ObjectDataSource1" >
        <Columns>
            
           <asp:HyperLinkField DataNavigateUrlFields="UserName"  SortExpression="UserName" DataNavigateUrlFormatString="user.aspx?Id={0}&UrlReferrer=users.aspx?Id={0}" DataTextField="UserName" ShowHeader="False" />
            <asp:HyperLinkField DataNavigateUrlFields="UserName" DataNavigateUrlFormatString="?Id={0}"
                
                Text="Edit Roles..." />
       
        </Columns>
    </asp:GridView >
            </td>
            <td valign="top">
                <asp:CheckBoxList ID="CheckBoxListRoles" runat="server" AutoPostBack="True" DataSourceID="ObjectDataSource2"
                    DataTextField="Name" OnDataBound="CheckBoxListRoles_DataBound" OnSelectedIndexChanged="CheckBoxListRoles_SelectedIndexChanged">
                </asp:CheckBoxList>
                <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetAllRoles"
                    TypeName="Discovery.ComponentServices.Security.SecurityController">
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
        </contenttemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetAllUsers"
        TypeName="Discovery.ComponentServices.Security.SecurityController">
        <SelectParameters>
            <asp:ControlParameter ControlID="AToZ" Name="filter" PropertyName="CurrentLetter"
                Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

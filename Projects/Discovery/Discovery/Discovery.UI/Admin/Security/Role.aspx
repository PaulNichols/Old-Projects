<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Role.aspx.cs" Inherits="Discovery.UI.Web.Security.Role" Title="Mangement Server - Role Maintenance" %>

<%@ Register Src="AToZ.ascx" TagName="AToZ" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
<div class="PageTitle">
        Role Maintenance</div>
        <hr />
    <asp:HyperLink ID="HyperLinkBack" SkinID="HyperLinkBack"  runat="server">Back</asp:HyperLink><br />
    <div>
    <div>
        <asp:FormView ID="FormView1" runat="server" CellPadding="4" DataSourceID="RoleObjectDataSource"
            Font-Names="Arial" Font-Size="8pt" ForeColor="#333333" Width="37px" DataKeyNames="Name">
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <EditItemTemplate>
                <asp:Button ID="ButtonUpdate" runat="server" CausesValidation="True" CommandName="Update"
                    OnClick="ButtonUpdate_Click" Text="Update" /><asp:Button ID="ButtonUpdateCancel"
                        runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" /><br />
                <br />
                <table>
                    <tr>
                        <td style="width: 131px">
                            Role Name:</td>
                        <td style="width: 346px">
                            <asp:Label ID="LabelRoleName" runat="server" Text='<%# Bind("Name") %>'></asp:Label></td>
                    </tr>
                </table>
                <br />
            </EditItemTemplate>
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <InsertItemTemplate>
                <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                    OnClick="InsertButton_Click" Text="Insert" />
                <asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                    OnClick="InsertCancelButton_Click" Text="Cancel" />
                <br />
                <table>
                    <tr>
                        <td style="width: 108px">
                            Role Name:</td>
                        <td>
                            <asp:TextBox ID="TextBoxRoleName" runat="server" Text='<%# Bind("Name") %>' Width="252px"></asp:TextBox></td>
                    </tr>
                </table>
            </InsertItemTemplate>
            <ItemTemplate>
                &nbsp;<asp:ImageButton ID="ButtonDelete" runat="server" CommandName="Delete" SkinID="ButtonDelete" /><br />
                <br />
    <table style="font-size: 8pt; color: #000000; font-family: Arail;">
        <tr>
            <td style="width: 131px">
                Role Name:</td>
            <td style="width: 346px">
                &nbsp;<asp:Label ID="LabelRoleName" runat="server" Text='<%# Bind("Name") %>'></asp:Label></td>
        </tr>
    </table>
    <br />
    <table style="font-size: 8pt; width: 331px; font-family: Arial;">
        <tr>
            <td style="width: 50%; color: white; background-color: #336699;" valign="top">
                Users In Role:</td>
        </tr>
        <tr>
            <td valign="top">
                <uc1:AToZ ID="AToZ" runat="server" Visible="false" />
                <br />
                &nbsp;<asp:BulletedList ID="BulletedList1" runat="server" DataSourceID="ObjectDataSource2" DataTextField="UserName" DataValueField="UserName">
                </asp:BulletedList>
            </td>
        </tr>
    </table>
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetUsersInRole"
            TypeName="Discovery.ComponentServices.Security.SecurityController" UpdateMethod="AddUsersToRole">
            <UpdateParameters>
                <asp:Parameter Name="role" Type="String" />
                <asp:Parameter Name="users" Type="Object" />
            </UpdateParameters>
            <SelectParameters>
                <asp:QueryStringParameter Name="Id" QueryStringField="Role"
                    Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
            </ItemTemplate>
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        </asp:FormView>
        <asp:ObjectDataSource ID="RoleObjectDataSource" runat="server" SelectMethod="GetRole"
            TypeName="Discovery.ComponentServices.Security.SecurityController" DeleteMethod="DeleteRole" InsertMethod="CreateRole" UpdateMethod="AddUsersToRole">
            <SelectParameters>
                <asp:QueryStringParameter Name="Id" QueryStringField="Id" Type="String" />
            </SelectParameters>
            <DeleteParameters>
                <asp:Parameter Name="role" Type="String" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="role" Type="String" />
                <asp:Parameter Name="users" Type="Object" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="role" Type="String" />
            </InsertParameters>
        </asp:ObjectDataSource>
        &nbsp; &nbsp; &nbsp;&nbsp;</div>
</asp:Content>


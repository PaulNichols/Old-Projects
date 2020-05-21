<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Roles.aspx.cs" Inherits="Discovery.UI.Web.Security.Roles" Title="Management Server - Roles Maintenance" %>
<%@ Register Src="AToZ.ascx" TagName="AToZ" TagPrefix="uc1" %>
<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="Discovery" %>
       
    
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">

 <asp:UpdatePanel runat="server" ID="UpdatePanel3">
           
                <ContentTemplate>
<div class="PageTitle">
        Role Maintenance</div>
        <hr />
    Role Name:
    <asp:TextBox ID="TextBoxRoleName" runat="server"></asp:TextBox>
    <asp:LinkButton ID="NewButton" runat="server" OnClick="LinkButtonAddNew_Click">Add New Role</asp:LinkButton><br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  ControlToValidate="TextBoxRoleName"
        ErrorMessage="You must enter a Role Name">You must enter a Role Name</asp:RequiredFieldValidator><br />
        

    <table style="font-size: 8pt; width: 778px; font-family: Arial">
        <tr>
            <td style="width: 50%; color: white; background-color: #336699" valign="top">
                Roles:</td>
            <td style="width: 50%; color: white; background-color: #336699" valign="top">
                <asp:Literal ID="LiteralRoleName" runat="server"></asp:Literal>
                 Users:</td>
        </tr>
        <tr>
            <td valign="top">
                <asp:GridView   OnRowDeleted="GridView_RowDeleted" ID="GridView1"  DataKeyNames="Name" runat="server" 
                      DataSourceID="ObjectDataSource1"
                    Width="100%" OnRowCommand="GridView1_RowCommand"   >
                    <Columns>
                      

                        <asp:BoundField DataField="Name"  />

                          <asp:ButtonField CommandName="Show Users" Text="Edit Users..." />
                
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButtonDeleteRole" CausesValidation="false" runat="server" CommandArgument='<%# Bind("Name") %>'
                                    CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');" >Delete Role</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                
               
                    </Columns>
                </asp:GridView >
                
            </td>
            <td valign="top">
                <uc1:atoz id="AToZ" runat="server" Visible="false"></uc1:atoz>
                <asp:GridView OnRowDataBound="GridViewUsers_RowDataBound"  ID="GridViewUsers" runat="server" DataSourceID="ObjectDataSource3"
                    Width="383px" Visible="False" DataKeyNames="UserName" >
                    <Columns>
                        <asp:BoundField DataField="UserName" HeaderText="User Name" ReadOnly="True" SortExpression="UserName">
                            <ItemStyle Width="66%" />
                            <HeaderStyle Width="66%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="User Is In Role">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBoxUserInRole" AutoPostBack="true" runat="server" OnCheckedChanged="CheckBoxUserInRole_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView >
                Ticking a User will instantly Add or Remove them from the selected Group
                <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" SelectMethod="GetAllUsers"
                    TypeName="Discovery.ComponentServices.Security.SecurityController">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="AToZ" Name="filter" PropertyName="CurrentLetter"
                            Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
           
                </ContentTemplate>
            </asp:UpdatePanel>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetAllRoles"
        TypeName="Discovery.ComponentServices.Security.SecurityController" DataObjectTypeName="Discovery.BusinessObjects.Role" DeleteMethod="DeleteRole"></asp:ObjectDataSource>
</asp:Content>


<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="user.aspx.cs" Inherits="Discovery.UI.Web.Security.User" Title="Mangement Server - User Maintenance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
 <div class="PageTitle">
        User Maintenance</div>
        <hr />
    <asp:HyperLink ID="HyperLinkBack" SkinID="HyperLinkBack"  runat="server">Back</asp:HyperLink><br />
    <div>
        <asp:FormView ID="FormView1" EnableViewState="false" runat="server"  DataSourceID="ObjectDataSource2" DataKeyNames="UserName" OnItemCommand="FormView1_ItemCommand" OnItemUpdating="FormView1_ItemUpdating"
            >
            
            <EditItemTemplate>
                 <asp:ImageButton ID="ButtonUpdate"  runat="server" CausesValidation="True" CommandName="Update"
                OnClick="ButtonUpdate_Click" SkinID="ButtonSave" /><asp:ImageButton ID="ButtonUpdateCancel"
                    runat="server" CausesValidation="False" CommandName="Cancel" SkinID="ButtonCancel" /><br />
                <table>
                    <tr>
                        <td style="width: 131px">
                            User Name:</td>
                        <td style="width: 346px">
                            <asp:Label ID="LabelUserName" runat="server" Text='<%# Bind("UserName") %>'></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 131px">
                            Old
                            Password:</td>
                        <td style="width: 346px">
                            <asp:TextBox ID="TextBoxOldPassword" runat="server" Width="252px" Text='<%# Bind("OldPassword") %>' TextMode="Password"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 131px; height: 16px">
                            New Password:</td>
                        <td style="width: 346px; height: 16px">
                            <asp:TextBox ID="TextBoxNewPassword" runat="server" Text='<%# Bind("NewPassword") %>'
                                Width="252px" TextMode="Password"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 131px">
                            Email:</td>
                        <td style="width: 346px">
                            <asp:TextBox ID="TextBoxEmail" runat="server" Text='<%# Bind("Email") %>' Width="252px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 131px">
                            Warehouse:</td>
                        <td style="width: 346px"><asp:DropDownList ID="DropDownListWarehouse" runat="server" DataSourceID="WarehouseObjectDataSource"
                                DataTextField="Description" DataValueField="Id" Width="259px" AppendDataBoundItems="True" OnDataBound="DropDownListWarehouse_DataBound">
                                                        <asp:ListItem Value="-1">All</asp:ListItem>
                        </asp:DropDownList>
                            <asp:ObjectDataSource ID="WarehouseObjectDataSource" runat="server"
                                SelectMethod="GetWarehouses" TypeName="Discovery.BusinessObjects.Controllers.WarehouseController"></asp:ObjectDataSource>
                        </td>
                    </tr>
                      <tr>
                        <td style="width: 131px">
                            Region:</td>
                        <td style="width: 346px"><asp:DropDownList ID="DropDownListRegion" runat="server" DataSourceID="RegionObjectDataSource"
                                DataTextField="Description" DataValueField="Id" Width="259px" AppendDataBoundItems="True"  OnDataBound="DropDownListRegion_DataBound">
                                                        <asp:ListItem Value="-1">All</asp:ListItem>
                        </asp:DropDownList>
                            <asp:ObjectDataSource ID="RegionObjectDataSource" runat="server"
                                SelectMethod="GetRegions" TypeName="Discovery.BusinessObjects.Controllers.OptrakRegionController"></asp:ObjectDataSource>
                        </td>
                    </tr>
                    
                    <tr>
                        <td style="width: 131px">
                            Sales Location:</td>
                        <td style="width: 346px">
                            <asp:DropDownList ID="DropDownListSalesBranch" runat="server" DataSourceID="SalesBranchObjectDataSource"
                                DataTextField="Description" DataValueField="Id" Width="259px" AppendDataBoundItems="True"  OnDataBound="DropDownListSalesBranch_DataBound">
                               
                                <asp:ListItem Value="-1">All</asp:ListItem>
                            </asp:DropDownList><asp:ObjectDataSource ID="SalesBranchObjectDataSource" runat="server"
                                SelectMethod="GetLocations" TypeName="Discovery.BusinessObjects.Controllers.SalesLocationController">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="false" Name="fullyPopulate" Type="Boolean" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 131px">
                            Opco:</td>
                        <td style="width: 346px">
                            <asp:DropDownList ID="DropDownListOpco" runat="server" DataSourceID="OpCoObjectDataSource"
                                DataTextField="Description" DataValueField="Id" Width="259px" AppendDataBoundItems="True"  OnDataBound="DropDownListOpco_DataBound">
                               
                                <asp:ListItem Value="-1">All</asp:ListItem>
                            </asp:DropDownList><asp:ObjectDataSource ID="OpCoObjectDataSource" runat="server"
                                SelectMethod="GetOpCos" TypeName="Discovery.BusinessObjects.Controllers.OpcoController">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="false" Name="fullyPopulate" Type="Boolean" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                </table>
            </EditItemTemplate>
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <InsertItemTemplate>
                <asp:ImageButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                OnClick="InsertButton_Click" SkinID="ButtonInsert" /><asp:ImageButton ID="InsertCancelButton"
                    runat="server" CausesValidation="False" CommandName="Cancel" OnClick="InsertCancelButton_Click"
                    SkinID="ButtonCancel" />
                <br />
                <table>
                    <tr>
                        <td style="width: 235px; height: 12px;">
                            User Name:</td>
                        <td style="width: 346px; height: 12px;">
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("UserName") %>' Width="252px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 235px">
                            Password:</td>
                        <td style="width: 346px">
                            <asp:TextBox ID="TextBox2" runat="server" Width="251px" Text='<%# Bind("NewPassword") %>' TextMode="Password"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 235px">
                            Email:</td>
                        <td style="width: 346px">
                            <asp:TextBox ID="TextBoxEmail" runat="server" Text='<%# Bind("Email") %>' Width="252px"></asp:TextBox></td>
                    </tr>
              
                    <tr>
                        <td style="width: 131px">
                            Warehouse:</td>
                        <td style="width: 346px">
                            <asp:DropDownList ID="DropDownListWarehouse" runat="server" DataSourceID="WarehouseObjectDataSource"
                                DataTextField="Description" DataValueField="Id" Width="259px" AppendDataBoundItems="True" SelectedValue='<%# Bind("WarehouseId") %>'>
                                
                                <asp:ListItem Value="-1">All</asp:ListItem>
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="WarehouseObjectDataSource" runat="server"
                                SelectMethod="GetWarehouses" TypeName="Discovery.BusinessObjects.Controllers.WarehouseController"></asp:ObjectDataSource>
                        </td>
                    </tr>
                      <tr>
                        <td style="width: 131px">
                            Region:</td>
                        <td style="width: 346px">
                            <asp:DropDownList ID="DropDownListRegion" runat="server" DataSourceID="RegionObjectDataSource"
                                DataTextField="Description" DataValueField="Id" Width="259px" AppendDataBoundItems="True" SelectedValue='<%# Bind("RegionId") %>'>
                                
                                <asp:ListItem Value="-1">All</asp:ListItem>
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="RegionObjectDataSource" runat="server"
                                SelectMethod="GetRegions" TypeName="Discovery.BusinessObjects.Controllers.OptrakRegionController"></asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 131px">
                            Sales Location:</td>
                        <td style="width: 346px">
                            <asp:DropDownList ID="DropDownListSalesBranch" runat="server" DataSourceID="SalesBranchObjectDataSource"
                                DataTextField="Description" DataValueField="Id" Width="259px" AppendDataBoundItems="True" SelectedValue='<%# Bind("SalesLocationId") %>'>
                                
                                <asp:ListItem Value="-1">All</asp:ListItem>
                            </asp:DropDownList><asp:ObjectDataSource ID="SalesBranchObjectDataSource" runat="server"
                                SelectMethod="GetLocations" TypeName="Discovery.BusinessObjects.Controllers.SalesLocationController">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="false" Name="fullyPopulate" Type="Boolean" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 131px">
                            Opco:</td>
                        <td style="width: 346px">
                            <asp:DropDownList ID="DropDownListOpco" runat="server" DataSourceID="OpCoObjectDataSource"
                                DataTextField="Description" DataValueField="Id" Width="259px" AppendDataBoundItems="True" SelectedValue='<%# Bind("OpCoId") %>'>
                                
                                <asp:ListItem Value="-1">All</asp:ListItem>
                            </asp:DropDownList><asp:ObjectDataSource ID="OpCoObjectDataSource" runat="server"
                                SelectMethod="GetOpCos" TypeName="Discovery.BusinessObjects.Controllers.OpcoController">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="false" Name="fullyPopulate" Type="Boolean" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                </table>
            </InsertItemTemplate>
            <ItemTemplate>
           <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Edit" SkinID="ButtonEdit" /><asp:ImageButton
                ID="ImageButton2" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');"
                SkinID="ButtonDelete" />
                <asp:ImageButton ID="ImageButtonResetPassword" SkinID="ButtonResetPassword" 
                    runat="server" CausesValidation="False" CommandName="ResetPassword"  OnClientClick="return confirm('Are you sure you want to reset this users password?');" /><br />
              
                <table>
                    <tr>
                        <td style="width: 131px">
                            User Name:</td>
                        <td style="width: 346px">
                            <asp:Label ID="LabelUserName" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 131px">
                            Email:</td>
                        <td style="width: 346px">
                            <asp:Label ID="LabelEmail" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 131px">
                            Warehouse:</td>
                        <td style="width: 346px">
                            <asp:Label ID="LabelWarehouse" runat="server" Text='<%# Eval("Warehouse.Description") %>' OnDataBinding="LabelWarehouse_DataBinding"></asp:Label></td>
                    </tr>
                      <tr>
                        <td style="width: 131px">
                            Region:</td>
                        <td style="width: 346px">
                            <asp:Label ID="LabelRegion" runat="server" Text='<%# Eval("Region.Description") %>' OnDataBinding="LabelWarehouse_DataBinding"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 131px">
                            Sales Location:</td>
                        <td style="width: 346px">
                            <asp:Label ID="LabelSalesBranch" runat="server" OnDataBinding="LabelWarehouse_DataBinding"
                                Text='<%# Eval("SalesLocation.Description") %>'></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 131px">
                            Opco:</td>
                        <td style="width: 346px">
                            <asp:Label ID="LabelOpCo" runat="server" OnDataBinding="LabelWarehouse_DataBinding" Text='<%# Eval("OpCo.Description") %>'></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 131px">
                            Last Activity Date:</td>
                        <td style="width: 346px">
                            <asp:Label ID="LabelLastActivity" runat="server" Text='<%# Bind("LastActivityDate") %>'></asp:Label></td>
                    </tr>
                </table>
                <br />
                <table>
                    <tr>
                        <td style="width: 480px; color: white; background-color: #336699;">
                            Related Roles:</td>
                    </tr>
                    <tr>
                        <td style="width: 480px">
                            <asp:BulletedList ID="BulletedListRelatedRoles" runat="server" DataSourceID="ObjectDataSource3" DataTextField="Name" DataValueField="Name" OnDataBound="BulletedList1_DataBound">
                            </asp:BulletedList>
                            <asp:Label ID="LabelNoRelatedRoles" runat="server" Text="(None)"></asp:Label></td>
                    </tr>
                </table>
        <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" SelectMethod="GetRolesForUser"
            TypeName="Discovery.ComponentServices.Security.SecurityController">
            <SelectParameters>
                <asp:QueryStringParameter Name="userName" QueryStringField="Id" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
            </ItemTemplate>
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        </asp:FormView>
         <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetUserByName"
            TypeName="Discovery.ComponentServices.Security.SecurityController" DataObjectTypeName="Discovery.ComponentServices.Security.UserDetails" DeleteMethod="DeleteUser" InsertMethod="CreateUserAndProfile" UpdateMethod="UpdateUserAndProfile">
            <SelectParameters>
                <asp:QueryStringParameter Name="userName" QueryStringField="Id"
                    Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        &nbsp;</div>
</asp:Content>


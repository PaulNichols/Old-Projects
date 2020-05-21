<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Rule.aspx.cs" Inherits="Discovery.UI.Web.Security.Rule" Title="Rule Maintenance"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
     <div class="PageTitle">
        Authorisation Rules</div>
        <hr />
    <asp:HyperLink ID="HyperLinkBack" SkinID="HyperLinkBack"  runat="server">Back</asp:HyperLink><br />


    <asp:FormView ID="RouteFormView" runat="server" DataSourceID="RuleDataSource"
        DataKeyNames="Name" EnableViewState="False" OnItemUpdated="RouteFormView_ItemUpdated"  >
        <EditItemTemplate>
        
             <asp:ImageButton ID="ButtonUpdate" runat="server" CausesValidation="True" CommandName="Update"
                OnClick="ButtonUpdate_Click" SkinID="ButtonSave" /><asp:ImageButton ID="ButtonUpdateCancel"
                    runat="server" CausesValidation="False" CommandName="Cancel" SkinID="ButtonCancel" /><br />
            Rule: 
            <asp:Label ID="LabelName" runat="server" Text='<%# Bind("Name") %>'></asp:Label><br />
            Expression:<br />
            <asp:TextBox ID="TextBoxExpression" runat="server"  Height="49px"
                Text='<%# Bind("Expression") %>' TextMode="MultiLine" Width="379px"></asp:TextBox><br />
            <input id="Button1" type="button" value="AND" />
            <input id="Button3" type="button" value="OR" />
            <input id="Button4" type="button" value="NOT" />
            <input id="Button5" type="button" value="(" />
            <input id="Button6" type="button" value=")" />
            <input id="Button8" type="button" value="Identity" />
            <input id="Button9" type="button" value="Role" />
            <input id="Button10" type="button" value="Anonymous" /><br />
            <hr />
            <strong>Test:</strong><br />
            <table>
                <tr>
                    <td style="width: 114px" valign="top">
                        Identity:</td>
                    <td style="width: 66px">
                        Roles:</td>
                </tr>
                <tr>
                    <td style="width: 114px" valign="top">
                        Enter a Users Name:<br />
                        <asp:TextBox ID="TextBoxUserName" runat="server" ></asp:TextBox><asp:CheckBox ID="CheckBox1" runat="server" Text="Is Authenticated" Width="150px" /></td>
                    <td style="width: 66px">
                        <asp:DropDownList ID="DropDownListRoles" runat="server" DataSourceID="RolesObjectDataSource"
                            DataTextField="Name" DataValueField="Name" Width="207px">
                        </asp:DropDownList><asp:ObjectDataSource ID="RolesObjectDataSource" runat="server"
                            SelectMethod="GetAllRoles" TypeName="Discovery.ComponentServices.Security.SecurityController">
                        </asp:ObjectDataSource>
                        <asp:LinkButton ID="LinkButtonAddRole" runat="server" OnClick="LinkButtonAddRole_Click">Add Role</asp:LinkButton><br />
                        <asp:TextBox ID="TextBoxRoles" runat="server" Height="80px"
                             TextMode="MultiLine" Width="266px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 114px">
                        <asp:Button ID="ButtonTest" runat="server" Text="Test" OnClick="ButtonTest_Click" /></td>
                    <td style="width: 66px">
                        <asp:Label ID="LabelTestResult" runat="server" Text="" Width="258px"></asp:Label></td>
                </tr>
            </table>
        </EditItemTemplate>
        <InsertItemTemplate>
            <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                OnClick="InsertButton_Click" Text="Insert" />
            <asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                OnClick="InsertCancelButton_Click" Text="Cancel" />
            <br />
            Rule:<strong> </strong>
                <asp:TextBox ID="TextBoxRuleName" runat="server"  Text='<%# Bind("Name") %>'></asp:TextBox><br />
            Expression:<br />
            <asp:TextBox ID="TextBoxExpression" runat="server"  Height="49px"
                Text='<%# Bind("Expression") %>' TextMode="MultiLine" Width="379px"></asp:TextBox><br />
            <input id="Button9" type="button" value="AND" />
            <input id="Button10" type="button" value="OR" />
            <input id="Button11" type="button" value="NOT" />
            <input id="Button12" type="button" value="(" />
            <input id="Button13" type="button" value=")" />
            <input id="Button14" type="button" value="Identity" />
            <input id="Button15" type="button" value="Role" />
            <input id="Button16" type="button" value="Anonymous" /><br />
            <hr />
            <strong>Test:</strong><br />
            <table>
                <tr>
                    <td style="width: 114px" valign="top">
                        Identity:</td>
                    <td style="width: 66px">
                        Roles:</td>
                </tr>
                <tr>
                    <td style="width: 114px" valign="top">
                        Enter a Users Name:<br />
                        <asp:TextBox ID="TextBoxUserName" runat="server" ></asp:TextBox><br />
                        <asp:CheckBox ID="CheckBox1" runat="server" Text="Is Authenticated" Width="150px" /></td>
                    <td style="width: 66px">
                        <asp:DropDownList ID="DropDownListRoles" runat="server" DataSourceID="RolesObjectDataSource"
                            DataTextField="Name" DataValueField="Name" Width="207px">
                        </asp:DropDownList><asp:ObjectDataSource ID="RolesObjectDataSource" runat="server"
                            SelectMethod="GetAllRoles" TypeName="Discovery.ComponentServices.Security.SecurityController">
                        </asp:ObjectDataSource>
                        <asp:LinkButton ID="LinkButtonAddRole" runat="server" OnClick="LinkButtonAddRole_Click">Add Role</asp:LinkButton><br />
                        <asp:TextBox ID="TextBoxRoles" runat="server"  Height="80px"
                           TextMode="MultiLine" Width="266px" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 114px">
                        <asp:Button ID="Button2" runat="server" Text="Test" OnClick="ButtonTest_Click" /></td>
                    <td style="width: 66px">
                        <asp:Label ID="LabelTestResult" runat="server" Text="Not Authrorised" Width="258px"></asp:Label></td>
                </tr>
            </table>
        </InsertItemTemplate>
        <ItemTemplate>
         <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Edit" SkinID="ButtonEdit" /><br />
            <table>
                <tr>
                  <td style="width: 100px">
                        Name:</td>
                    <td style="width: 200px">
                        <asp:Label ID="LabelName" runat="server" Text='<%# Bind("Name") %>'></asp:Label></td>
                        </tr>
                        <tr>
                          <td style="width: 100px">
                        Expression:</td>
                    <td style="width: 250px">
                        <asp:Label ID="LabelExpression" runat="server" Text='<%# Bind("Expression") %>'></asp:Label></td>
                        </tr>
            </table>
        </ItemTemplate>
    </asp:FormView>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
    <asp:ObjectDataSource ID="RuleDataSource" runat="server" SelectMethod="GetRule"
        TypeName="Discovery.ComponentServices.Security.SecurityController" DataObjectTypeName="Discovery.BusinessObjects.Rule" InsertMethod="CreateRule" DeleteMethod="DeleteRule" UpdateMethod="UpdateRule" >

        <SelectParameters>
            <asp:QueryStringParameter Name="ruleName" QueryStringField="Id" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>


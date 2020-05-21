<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="login.aspx.cs"
    Inherits="Discovery.UI.Web.Security.Login" Title="Mangement Server - Login" StylesheetTheme="DiscoveryDefault"
    Theme="DiscoveryDefault" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <div>
        <table style="width: 100%" height="100%">
            <tr>
                <td align="center" valign="middle">
                    <asp:Login ID="Login2" runat="server">
                        <LayoutTemplate>
                            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                                <tbody>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:Image SkinID="LogonBanner" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <hr />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label></td>
                                        <td align="left">
                                            <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login2">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label></td>
                                        <td align="left">
                                            <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login2">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td align="left">
                                            <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2" style="color: red">
                                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="2">
                                            <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="Login2" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </LayoutTemplate>
                    </asp:Login>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

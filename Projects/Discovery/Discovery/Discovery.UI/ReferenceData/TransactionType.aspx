<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="TransactionType.aspx.cs"
    Inherits="Discovery.UI.Web.ReferenceData.TransactionType" Title="Management Server - Transaction Types" %>

<%@ Import Namespace="System.Web.UI.WebControls" %>
<%@ Import Namespace="System.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <div class="PageTitle">
        Transaction Types</div>
    <hr />
    <br />
    <asp:HyperLink ID="HyperLinkBack"  SkinID="HyperLinkBack" runat="server">Back</asp:HyperLink><br />
    <asp:FormView ID="TransactionTypeFormView" runat="server" DataSourceID="TransactionTypeDataSource"
        DataKeyNames="Id,Code"  EnableViewState="False">
        <EditItemTemplate>
            <asp:ImageButton ID="ButtonUpdate" runat="server" CausesValidation="True" CommandName="Update"
                OnClick="ButtonUpdate_Click" SkinID="ButtonSave" /><asp:ImageButton ID="ButtonUpdateCancel"
                    runat="server" CausesValidation="False" CommandName="Cancel" SkinID="ButtonCancel" /><br />
            <table id="TABLE1">
                <tr>
                    <td style="width: 100px">
                        Code:</td>
                    <td style="width: 100px">
                        <asp:TextBox ID="TextBoxCode" runat="server" Text='<%# Bind("Code") %>'></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Description:</td>
                    <td style="width: 100px">
                        <asp:TextBox ID="TextBoxDescription" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Is Stock:</td>
                    <td style="width: 100px">
                        <asp:CheckBox ID="CheckBoxIsStock" runat="server" Checked='<%# Bind("IsStock") %>' /></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Is Non Stock:</td>
                    <td style="width: 100px">
                        <asp:CheckBox ID="CheckBoxIsNonStock" runat="server" Checked='<%# Bind("IsNonStock") %>' />
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Is Collection:</td>
                    <td style="width: 100px">
                        <asp:CheckBox ID="CheckBoxIsCollection" runat="server" Checked='<%# Bind("IsCollection") %>' />
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Is Sample:</td>
                    <td style="width: 100px">
                        <asp:CheckBox ID="CheckBoxIsSample" runat="server" Checked='<%# Bind("IsSample") %>' />
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="Id" runat="server" Value='<%# Bind("Id") %>' />
            <asp:HiddenField ID="CheckSum" runat="server" Value='<%# Bind("CheckSum") %>' />
            <asp:HiddenField ID="IsArchived" runat="server" Value='<%# Bind("IsArchived") %>' />
            <asp:HiddenField ID="UpdatedBy" runat="server" Value='<%# Bind("UpdatedBy") %>' />
            <!-- AJAX Extender -->
            <ajaxToolkit:ToggleButtonExtender ID="ToggleButtonExtender1" runat="server" TargetControlID="CheckBoxIsStock"
                CheckedImageUrl="~/images/check.gif" UncheckedImageUrl="~/images/uncheck.gif"
                ImageHeight="16" ImageWidth="16" />
            <ajaxToolkit:ToggleButtonExtender ID="ToggleButtonExtender3" runat="server" TargetControlID="CheckBoxIsNonStock"
                CheckedImageUrl="~/images/check.gif" UncheckedImageUrl="~/images/uncheck.gif"
                ImageHeight="16" ImageWidth="16" />
            <ajaxToolkit:ToggleButtonExtender ID="ToggleButtonExtender4" runat="server" TargetControlID="CheckBoxIsCollection"
                CheckedImageUrl="~/images/check.gif" UncheckedImageUrl="~/images/uncheck.gif"
                ImageHeight="16" ImageWidth="16" />
            <ajaxToolkit:ToggleButtonExtender ID="ToggleButtonExtender5" runat="server" TargetControlID="CheckBoxIsSample"
                CheckedImageUrl="~/images/check.gif" UncheckedImageUrl="~/images/uncheck.gif"
                ImageHeight="16" ImageWidth="16" />
        </EditItemTemplate>
        <InsertItemTemplate>
            <asp:ImageButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                OnClick="InsertButton_Click" SkinID="ButtonInsert" /><asp:ImageButton ID="InsertCancelButton"
                    runat="server" CausesValidation="False" CommandName="Cancel" OnClick="InsertCancelButton_Click"
                    SkinID="ButtonCancel" /><br />
            <table id="Table2">
                <tr>
                    <td>
                        Code:</td>
                    <td>
                        <asp:TextBox ID="TextBoxCode" runat="server" Text='<%# Bind("Code") %>'></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        Description:</td>
                    <td>
                        <asp:TextBox ID="TextBoxDescription" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Is Stock:</td>
                    <td style="width: 100px">
                        <asp:CheckBox ID="CheckBoxIsStock" runat="server" Checked='<%# Bind("IsStock") %>' /></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Is Non Stock:</td>
                    <td style="width: 100px">
                        <asp:CheckBox ID="CheckBoxIsNonStock" runat="server" Checked='<%# Bind("IsNonStock") %>' />
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Is Collection:</td>
                    <td style="width: 100px">
                        <asp:CheckBox ID="CheckBoxIsCollection" runat="server" Checked='<%# Bind("IsCollection") %>' />
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Is Sample:</td>
                    <td style="width: 100px">
                        <asp:CheckBox ID="CheckBoxIsSample" runat="server" Checked='<%# Bind("IsSample") %>' />
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="Id" runat="server" Value='-1' />
            <asp:HiddenField ID="CheckSum" runat="server" Value='0' />
            <asp:HiddenField ID="UpdatedBy" runat="server" Value='<%# Bind("UpdatedBy") %>' />
            <!-- AJAX Extender -->
            <ajaxToolkit:ToggleButtonExtender ID="ToggleButtonExtender1" runat="server" TargetControlID="CheckBoxIsStock"
                CheckedImageUrl="~/images/check.gif" UncheckedImageUrl="~/images/uncheck.gif"
                ImageHeight="16" ImageWidth="16" />
            <ajaxToolkit:ToggleButtonExtender ID="ToggleButtonExtender3" runat="server" TargetControlID="CheckBoxIsNonStock"
                CheckedImageUrl="~/images/check.gif" UncheckedImageUrl="~/images/uncheck.gif"
                ImageHeight="16" ImageWidth="16" />
            <ajaxToolkit:ToggleButtonExtender ID="ToggleButtonExtender4" runat="server" TargetControlID="CheckBoxIsCollection"
                CheckedImageUrl="~/images/check.gif" UncheckedImageUrl="~/images/uncheck.gif"
                ImageHeight="16" ImageWidth="16" />
            <ajaxToolkit:ToggleButtonExtender ID="ToggleButtonExtender5" runat="server" TargetControlID="CheckBoxIsSample"
                CheckedImageUrl="~/images/check.gif" UncheckedImageUrl="~/images/uncheck.gif"
                ImageHeight="16" ImageWidth="16" />
        </InsertItemTemplate>
        <ItemTemplate>
            <asp:ImageButton ID="ButtonEdit" runat="server" CommandName="Edit" SkinID="ButtonEdit" /><asp:ImageButton
                ID="ButtonDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');"
                SkinID="ButtonDelete" /><br />
            <table>
                <tr>
                    <td style="width: 100px">
                        Code:</td>
                    <td style="width: 100px">
                        <asp:Label ID="LabelCode" runat="server" Text='<%# Bind("Code") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Description:</td>
                    <td style="width: 100px">
                        <asp:Label ID="LabelDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Is Stock:</td>
                    <td style="width: 100px">
                        <asp:Image ID="ImageIsStock" runat="server" ImageUrl='<%# Eval("IsStock", "~/Images/{0}") + ".gif" %>' />
                </tr>
                <tr>
                    <td style="width: 100px">
                        Is Non Stock:</td>
                    <td style="width: 100px">
                        <asp:Image ID="ImageIsNonStock" runat="server" ImageUrl='<%# Eval("IsNonStock", "~/Images/{0}") + ".gif" %>' />
                </tr>
                <tr>
                    <td style="width: 100px">
                        Is Collection:</td>
                    <td style="width: 100px">
                        <asp:Image ID="ImageIsCollection" runat="server" ImageUrl='<%# Eval("IsCollection", "~/Images/{0}") + ".gif" %>' />
                </tr>
                <tr>
                    <td style="width: 100px">
                        Is Sample:</td>
                    <td style="width: 100px">
                        <asp:Image ID="ImageIsSample" runat="server" ImageUrl='<%# Eval("IsSample", "~/Images/{0}") + ".gif" %>' />
                </tr>
            </table>
            <asp:HiddenField ID="Id" runat="server" Value='<%# Bind("Id") %>' />
        </ItemTemplate>
        <EmptyDataTemplate>
            A problem has occured and the item you requested could not be display
        </EmptyDataTemplate>
    </asp:FormView>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
    <asp:ObjectDataSource ID="TransactionTypeDataSource" runat="server" DataObjectTypeName="Discovery.BusinessObjects.TransactionType"
        DeleteMethod="DeleteTransactionType" InsertMethod="SaveTransactionType" SelectMethod="GetTransactionType"
        TypeName="Discovery.BusinessObjects.Controllers.TransactionTypeController" UpdateMethod="SaveTransactionType">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="-1" Name="transactionTypeId" QueryStringField="Id"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

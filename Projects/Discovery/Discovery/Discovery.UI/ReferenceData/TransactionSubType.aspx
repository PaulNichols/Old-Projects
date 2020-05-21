<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="TransactionSubType.aspx.cs" Inherits="Discovery.UI.Web.ReferenceData.TransactionSubType" Title="Mangement Server - Transaction Sub Types" %>
<%@ Import namespace="System.Web.UI.WebControls"%>
<%@ Import namespace="System.Web.UI"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
     <div class="PageTitle">
        Transaction Sub Types</div>
        <hr />
        <br /><asp:HyperLink ID="HyperLinkBack" SkinID="HyperLinkBack"  runat="server">Back</asp:HyperLink><br />


    <asp:FormView ID="TransactionSubTypeFormView" runat="server" DataSourceID="TransactionSubTypeDataSource"
        DataKeyNames="Id,Code"   EnableViewState="False" >
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
                        Is Normal:</td>
                    <td style="width: 100px">
                        <asp:CheckBox ID="CheckBoxIsNormal" runat="server" Checked='<%# Bind("IsNormal") %>' /></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Is Transfer:</td>
                    <td style="width: 100px">
                        <asp:CheckBox ID="CheckBoxIsTransfer" runat="server" Checked='<%# Bind("IsTransfer") %>' /></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Is Local Conversion:</td>
                    <td style="width: 100px">
                        <asp:CheckBox ID="CheckBoxIsLocalConversion" runat="server" Checked='<%# Bind("IsLocalConversion") %>' /></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Is 3rd Party Conversion:</td>
                    <td style="width: 100px">
                        <asp:CheckBox ID="CheckBoxIs3rdPartyConversion" runat="server" Checked='<%# Bind("Is3rdPartyConversion") %>' /></td>
                </tr>
            </table>
           <asp:HiddenField
                ID="Id" runat="server" Value='<%# Bind("Id") %>' />
            <asp:HiddenField ID="CheckSum" runat="server" Value='<%# Bind("CheckSum") %>' />
            <asp:HiddenField ID="IsArchived" runat="server" Value='<%# Bind("IsArchived") %>' />
            <asp:HiddenField ID="UpdatedBy" runat="server" Value='<%# Bind("UpdatedBy") %>' />
            <!-- AJAX Extender -->
            <ajaxToolkit:ToggleButtonExtender ID="ToggleButtonExtender1" runat="server" TargetControlID="CheckBoxIsNormal"
                CheckedImageUrl="~/images/check.gif" UncheckedImageUrl="~/images/uncheck.gif"
                ImageHeight="16" ImageWidth="16" />
            <ajaxToolkit:ToggleButtonExtender ID="ToggleButtonExtender2" runat="server" TargetControlID="CheckBoxIsTransfer"
                CheckedImageUrl="~/images/check.gif" UncheckedImageUrl="~/images/uncheck.gif"
                ImageHeight="16" ImageWidth="16" />
            <ajaxToolkit:ToggleButtonExtender ID="ToggleButtonExtender3" runat="server" TargetControlID="CheckBoxIsLocalConversion"
                CheckedImageUrl="~/images/check.gif" UncheckedImageUrl="~/images/uncheck.gif"
                ImageHeight="16" ImageWidth="16" />
            <ajaxToolkit:ToggleButtonExtender ID="ToggleButtonExtender4" runat="server" TargetControlID="CheckBoxIs3rdPartyConversion"
                CheckedImageUrl="~/images/check.gif" UncheckedImageUrl="~/images/uncheck.gif"
                ImageHeight="16" ImageWidth="16" />
        </EditItemTemplate>
        <InsertItemTemplate>
            <asp:ImageButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                OnClick="InsertButton_Click" SkinID="ButtonInsert" /><asp:ImageButton ID="InsertCancelButton"
                    runat="server" CausesValidation="False" CommandName="Cancel" OnClick="InsertCancelButton_Click"
                    SkinID="ButtonCancel" /><br />
            <table id="Table2" >
                <tr>
                    <td>
                        Code:</td>
                    <td style="width: 359px">
                        <asp:TextBox ID="TextBoxCode" runat="server" Text='<%# Bind("Code") %>'></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        Description:</td>
                    <td style="width: 359px">
                        <asp:TextBox ID="TextBoxDescription" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Is Normal:</td>
                    <td style="width: 100px">
                        <asp:CheckBox ID="CheckBoxIsNormal" runat="server" Checked='<%# Bind("IsNormal") %>' /></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Is Transfer:</td>
                    <td style="width: 100px">
                        <asp:CheckBox ID="CheckBoxIsTransfer" runat="server" Checked='<%# Bind("IsTransfer") %>' /></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Is Local Conversion:</td>
                    <td style="width: 100px">
                        <asp:CheckBox ID="CheckBoxIsLocalConversion" runat="server" Checked='<%# Bind("IsLocalConversion") %>' /></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Is 3rd Party Conversion:</td>
                    <td style="width: 100px">
                        <asp:CheckBox ID="CheckBoxIs3rdPartyConversion" runat="server" Checked='<%# Bind("Is3rdPartyConversion") %>' /></td>
                </tr>
            </table>
            <asp:HiddenField ID="Id" runat="server" Value='-1' />
            <asp:HiddenField ID="CheckSum" runat="server" Value='0' />
            <asp:HiddenField ID="UpdatedBy" runat="server" Value='<%# Bind("UpdatedBy") %>' />
            <!-- AJAX Extender -->
            <ajaxToolkit:ToggleButtonExtender ID="ToggleButtonExtender1" runat="server" TargetControlID="CheckBoxIsNormal"
                CheckedImageUrl="~/images/check.gif" UncheckedImageUrl="~/images/uncheck.gif"
                ImageHeight="16" ImageWidth="16" />
            <ajaxToolkit:ToggleButtonExtender ID="ToggleButtonExtender2" runat="server" TargetControlID="CheckBoxIsTransfer"
                CheckedImageUrl="~/images/check.gif" UncheckedImageUrl="~/images/uncheck.gif"
                ImageHeight="16" ImageWidth="16" />
            <ajaxToolkit:ToggleButtonExtender ID="ToggleButtonExtender3" runat="server" TargetControlID="CheckBoxIsLocalConversion"
                CheckedImageUrl="~/images/check.gif" UncheckedImageUrl="~/images/uncheck.gif"
                ImageHeight="16" ImageWidth="16" />
            <ajaxToolkit:ToggleButtonExtender ID="ToggleButtonExtender4" runat="server" TargetControlID="CheckBoxIs3rdPartyConversion"
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
                    <td style="width: 100px"><asp:Label ID="LabelCode" runat="server" Text='<%# Bind("Code") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Description:</td>
                    <td style="width: 100px"><asp:Label ID="LabelDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Is Normal:</td>
                    <td style="width: 100px">
                        <asp:Image ID="ImageIsStock" runat="server" ImageUrl='<%# Eval("IsNormal", "~/Images/{0}") + ".gif" %>' /></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Is Transfer:</td>
                    <td style="width: 100px">
                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("IsTransfer", "~/Images/{0}") + ".gif" %>' /></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Is Local Conversion:</td>
                    <td style="width: 100px">
                        <asp:Image ID="Image2" runat="server" ImageUrl='<%# Eval("IsLocalConversion", "~/Images/{0}") + ".gif" %>' /></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Is 3rd Party Conversion:</td>
                    <td style="width: 100px">
                        <asp:Image ID="Image3" runat="server" ImageUrl='<%# Eval("Is3rdPartyConversion", "~/Images/{0}") + ".gif" %>' /></td>
                </tr>
            </table>
            <asp:HiddenField ID="Id" runat="server" Value='<%# Bind("Id") %>' />
        </ItemTemplate>
        <EmptyDataTemplate>
            A problem has occured and the item you requested could not be displayed.
        </EmptyDataTemplate>
    </asp:FormView>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
    <asp:ObjectDataSource ID="TransactionSubTypeDataSource" runat="server" DataObjectTypeName="Discovery.BusinessObjects.TransactionSubType"
        DeleteMethod="DeleteTransactionSubType" InsertMethod="SaveTransactionSubType" SelectMethod="GetTransactionSubType"
        TypeName="Discovery.BusinessObjects.Controllers.TransactionSubTypeController" UpdateMethod="SaveTransactionSubType" >

        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="-1" Name="transactionSubTypeId" QueryStringField="Id"
                Type="Int32" />
                
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>


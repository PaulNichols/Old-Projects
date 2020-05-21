<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Sequence.aspx.cs" Inherits="Discovery.UI.Web.Admin.Sequence" Title="Mangement Server - Sequence Number Maintenance" %>

<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="cc1" %>
<%@ Import namespace="System.Web.UI.WebControls"%>
<%@ Import namespace="System.Web.UI"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <div class="PageTitle">
        Sequence</div>
        <hr />
        <br /> <asp:HyperLink ID="HyperLinkBack"  SkinID="HyperLinkBack" runat="server">Back</asp:HyperLink><br />


    <asp:FormView ID="SequenceFormView" OnItemInserting="FormView_ItemInserting" runat="server" DataSourceID="SequenceDataSource"
        DataKeyNames="Id"  EnableViewState="False"  >
        <EditItemTemplate>
            <asp:ImageButton ID="ButtonUpdate"  runat="server" CausesValidation="True" CommandName="Update"
                OnClick="ButtonUpdate_Click" SkinID="ButtonSave" /><asp:ImageButton ID="ButtonUpdateCancel"
                    runat="server" CausesValidation="False" CommandName="Cancel" SkinID="ButtonCancel" /><br />
            <table id="TABLE1">
                <tr>
                    <td style="width: 100px">
                        Name:</td>
                    <td style="width: 100px">
                        <asp:TextBox ID="TextBoxName" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Starting Value:</td>
                    <td style="width: 100px">
                        <cc1:DiscoveryNumericText ID="TextBoxSeed" runat="server" Text='<%# Bind("Seed") %>'></cc1:DiscoveryNumericText>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Increment Value:</td>
                    <td style="width: 100px">
                        <cc1:DiscoveryNumericText ID="TextBoxIncrement" runat="server" Text='<%# Bind("Increment") %>'></cc1:DiscoveryNumericText>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Current Value:</td>
                    <td style="width: 100px">
                        <cc1:DiscoveryNumericText ID="TextBoxCurrentValue" runat="server" Text='<%# Bind("CurrentValue") %>'></cc1:DiscoveryNumericText>
                </tr>
            </table>
           <asp:HiddenField
                ID="Id" runat="server" Value='<%# Bind("Id") %>' />
            <asp:HiddenField ID="CheckSum" runat="server" Value='<%# Bind("CheckSum") %>' />
            <asp:HiddenField ID="IsArchived" runat="server" Value='<%# Bind("IsArchived") %>' />
            <asp:HiddenField ID="UpdatedBy" runat="server" Value='<%# Bind("UpdatedBy") %>' />
        </EditItemTemplate>
        <InsertItemTemplate>
            <asp:ImageButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                OnClick="InsertButton_Click" SkinID="ButtonInsert" /><asp:ImageButton ID="InsertCancelButton"
                    runat="server" CausesValidation="False" CommandName="Cancel" OnClick="InsertCancelButton_Click"
                    SkinID="ButtonCancel" /><br />
            <table id="Table2" >
                <tr>
                    <td style="width: 100px">
                        Name:</td>
                    <td style="width: 100px">
                        <asp:TextBox ID="TextBoxName" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox></td>
                </tr>
                            <tr>
                    <td style="width: 100px">
                        Starting Value:</td>
                    <td style="width: 100px">
                        <cc1:DiscoveryNumericText ID="TextBoxSeed" runat="server" Text='<%# Bind("Seed") %>'></cc1:DiscoveryNumericText>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Increment Value:</td>
                    <td style="width: 100px">
                        <cc1:DiscoveryNumericText ID="TextBoxIncrement" runat="server" Text='<%# Bind("Increment") %>'></cc1:DiscoveryNumericText>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Current Value:</td>
                    <td style="width: 100px">
                        <cc1:DiscoveryNumericText ID="TextBoxCurrentValue" runat="server" Text='<%# Bind("CurrentValue") %>'></cc1:DiscoveryNumericText>
                </tr>
            </table>
            <asp:HiddenField
                ID="Id" runat="server" Value='-1' />
            <asp:HiddenField ID="CheckSum" runat="server" Value='0' />
            <asp:HiddenField ID="UpdatedBy" runat="server" Value='<%# Bind("UpdatedBy") %>' />
        </InsertItemTemplate>
        <ItemTemplate>
            <asp:ImageButton ID="ButtonEdit" runat="server" CommandName="Edit" SkinID="ButtonEdit" /><asp:ImageButton
                ID="ButtonDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');"
                SkinID="ButtonDelete" /><br />
            <table>
                <tr>
                    <td style="width: 100px">
                        Name:</td>
                    <td style="width: 100px">
            <asp:Label ID="LabelName" runat="server" Text='<%# Bind("Name") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Starting value:</td>
                    <td style="width: 100px">
            <asp:Label ID="LabelSeed" runat="server" Text='<%# Bind("Seed") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Increment value:</td>
                    <td style="width: 100px">
            <asp:Label ID="LabelIncrement" runat="server" Text='<%# Bind("Increment") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Current value:</td>
                    <td style="width: 100px">
            <asp:Label ID="LabelCurrentValue" runat="server" Text='<%# Bind("CurrentValue") %>'></asp:Label></td>
                </tr>
            </table><asp:HiddenField
                ID="Id" runat="server" Value='<%# Bind("Id") %>' />
        </ItemTemplate>
      
    </asp:FormView>
    
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
    <asp:ObjectDataSource ID="SequenceDataSource" runat="server" DataObjectTypeName="Discovery.BusinessObjects.Sequence"
        DeleteMethod="DeleteSequence" InsertMethod="SaveSequence" SelectMethod="GetSequence"
        TypeName="Discovery.BusinessObjects.Controllers.SequenceController" UpdateMethod="SaveSequence" OldValuesParameterFormatString="original_{0}" >
 <DeleteParameters>
     <asp:Parameter Name="sequenceId" Type="Int32" />
                </DeleteParameters>

        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="-1" Name="sequenceId" QueryStringField="id"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

